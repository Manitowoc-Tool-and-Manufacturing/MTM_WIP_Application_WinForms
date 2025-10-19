import * as fs from "fs";
import * as path from "path";
import mysql from "mysql2/promise";

interface ProcedureConfigEntry {
  file: string;
  database?: string;
  name?: string;
  apply?: boolean;
  comment?: string;
}

interface ProcedureInstallConfig {
  connection?: {
    host?: string;
    port?: number;
    user?: string;
    password?: string;
    database?: string;
  };
  procedures: ProcedureConfigEntry[];
  dryRun?: boolean;
}

interface InstallStoredProceduresArgs {
  config_file: string;
  host?: string;
  port?: number;
  user?: string;
  password?: string;
  database?: string;
  dry_run?: boolean;
}

interface ProcedureStatus {
  entry: ProcedureConfigEntry;
  name: string;
  database: string;
  exists: boolean;
  drifted: boolean;
  message: string;
  applied: boolean;
}

export async function installStoredProcedures(
  args: InstallStoredProceduresArgs
): Promise<{ content: Array<{ type: "text"; text: string }>; isError?: boolean }> {
  const { config, baseDir } = await loadConfig(args.config_file);

  if (!config.procedures || config.procedures.length === 0) {
    throw new Error("No procedures defined in configuration file.");
  }

  const connectionInfo = buildConnectionInfo(config.connection, args);
  const dryRun = args.dry_run ?? config.dryRun ?? false;

  const pool = await mysql.createPool({
    host: connectionInfo.host,
    port: connectionInfo.port,
    user: connectionInfo.user,
    password: connectionInfo.password,
    database: connectionInfo.database,
    waitForConnections: true,
    connectionLimit: 4,
    queueLimit: 0,
  });

  const statuses: ProcedureStatus[] = [];

  try {
    for (const entry of config.procedures) {
      const status = await processProcedureEntry(
        pool,
        entry,
        connectionInfo,
        baseDir,
        dryRun
      );
      statuses.push(status);
    }
  } finally {
    await pool.end();
  }

  const lines: string[] = [];
  let hasFailures = false;

  lines.push(dryRun ? "🛈 Dry run: no changes applied." : "Stored procedure installation summary:", "");

  for (const status of statuses) {
    const prefix = status.drifted || !status.exists ? "⚠️" : "✅";
    const appliedTxt = dryRun ? "(dry run)" : status.applied ? "(applied)" : "(skipped)";
    lines.push(
      `${prefix} ${status.database}.${status.name} ${appliedTxt} - ${status.message}`
    );
    if (status.drifted && !dryRun && !status.applied) {
      hasFailures = true;
    }
  }

  const overallPass = !hasFailures;
  if (!overallPass) {
    lines.unshift("❌ Stored procedure installation encountered issues.");
  } else {
    lines.unshift("✅ Stored procedure installation completed.");
  }

  return {
    content: [
      {
        type: "text",
        text: lines.join("\n"),
      },
    ],
    isError: !overallPass,
  };
}

async function loadConfig(configFile: string): Promise<{ config: ProcedureInstallConfig; baseDir: string }> {
  const resolved = path.resolve(configFile);
  if (!fs.existsSync(resolved)) {
    throw new Error(`Config file not found: ${resolved}`);
  }

  const raw = fs.readFileSync(resolved, "utf-8");
  try {
    const config = JSON.parse(raw) as ProcedureInstallConfig;
    const baseDir = path.dirname(resolved);
    return { config, baseDir };
  } catch (error) {
    const message = error instanceof Error ? error.message : String(error);
    throw new Error(`Failed to parse JSON config: ${message}`);
  }
}

function buildConnectionInfo(
  connection: ProcedureInstallConfig["connection"],
  overrides: InstallStoredProceduresArgs
) {
  return {
    host: overrides.host ?? connection?.host ?? "127.0.0.1",
    port: overrides.port ?? connection?.port ?? 3306,
    user: overrides.user ?? connection?.user ?? "root",
    password: overrides.password ?? connection?.password ?? "root",
    database: overrides.database ?? connection?.database ?? "mtm_wip_application_winforms_test",
  };
}

async function processProcedureEntry(
  pool: mysql.Pool,
  entry: ProcedureConfigEntry,
  connection: ReturnType<typeof buildConnectionInfo>,
  baseDir: string,
  dryRun: boolean
): Promise<ProcedureStatus> {
  const filePath = path.resolve(baseDir, entry.file);
  if (!fs.existsSync(filePath)) {
    return {
      entry,
      name: entry.name ?? path.basename(filePath, path.extname(filePath)),
      database: entry.database ?? connection.database,
      exists: false,
      drifted: true,
      message: `SQL file missing at ${filePath}`,
      applied: false,
    };
  }

  const fileContent = fs.readFileSync(filePath, "utf-8");
  const parsed = parseProcedureScript(fileContent, entry.name);
  const targetDatabase = entry.database ?? connection.database;

  const connectionForQuery = targetDatabase
    ? { ...connection, database: targetDatabase }
    : connection;

  const existing = await fetchExistingProcedure(
    pool,
    parsed.name,
    targetDatabase
  );

  const drifted = determineDrift(existing, parsed);

  let applied = false;
  let message = "";

  if (!existing) {
    message = drifted ? "Procedure missing" : "Procedure present";
  } else if (drifted) {
    message = "Procedure drift detected";
  } else {
    message = "Procedure up-to-date";
  }

  const shouldApply = !dryRun && (entry.apply ?? true) && drifted;

  if (shouldApply) {
    await applyProcedure(pool, parsed, targetDatabase);
    applied = true;
    message = "Procedure applied";
  }

  return {
    entry,
    name: parsed.name,
    database: targetDatabase,
    exists: Boolean(existing),
    drifted,
    message,
    applied,
  };
}

interface ParsedProcedure {
  name: string;
  body: string;
  dropStatement?: string;
  createStatement: string;
  parameterList?: string;
}

function parseProcedureScript(script: string, explicitName?: string): ParsedProcedure {
  let sanitized = script.replace(/\r/g, "");
  sanitized = sanitized.replace(/\/\*[\s\S]*?\*\//g, "");
  const lines = sanitized
    .split(/\n/)
    .filter((line) => !line.trim().startsWith("--"))
    .filter((line) => !/^DELIMITER\s+/i.test(line.trim()));

  sanitized = lines.join("\n");
  sanitized = sanitized.replace(/\s+$/gm, "");
  sanitized = sanitized.replace(/END\s*\$\$/gi, "END;");
  sanitized = sanitized.replace(/\$\$/g, ";");
  sanitized = sanitized.replace(/END\s*\/\//gi, "END;");
  sanitized = sanitized.replace(/\/\//g, ";");

  const dropMatch = sanitized.match(/DROP\s+PROCEDURE[\s\S]+?;/i);
  const dropStatement = dropMatch ? dropMatch[0].trim() : undefined;

  const createMatch = sanitized.match(/CREATE\s+(?:DEFINER=`[^`]+`@`[^`]+`\s+)?PROCEDURE[\s\S]+END\s*;?/i);
  if (!createMatch) {
    throw new Error("Unable to locate CREATE PROCEDURE statement in script.");
  }

  const createStatement = createMatch[0].trim();
  const nameMatch = createStatement.match(/PROCEDURE\s+`?([A-Za-z0-9_]+)`?/i);
  const name = explicitName ?? (nameMatch ? nameMatch[1] : undefined);
  if (!name) {
    throw new Error("Unable to determine procedure name from script.");
  }

  const parameterMatch = createStatement.match(/PROCEDURE\s+`?[A-Za-z0-9_]+`?\s*\(([^)]*)\)/i);
  const parameterList = parameterMatch ? parameterMatch[1].trim() : undefined;

  const bodyMatch = createStatement.match(/BEGIN[\s\S]*END\s*;?/i);
  const body = bodyMatch ? bodyMatch[0] : "";

  return {
    name,
    body,
    dropStatement,
    createStatement,
    parameterList,
  };
}

interface ExistingProcedureMeta {
  definition: string;
  parameters: string;
}

async function fetchExistingProcedure(
  pool: mysql.Pool,
  name: string,
  database: string
): Promise<ExistingProcedureMeta | undefined> {
  const [routineRows] = await pool.query<mysql.RowDataPacket[]>(
    `SELECT ROUTINE_DEFINITION FROM information_schema.routines WHERE ROUTINE_SCHEMA = ? AND ROUTINE_NAME = ? AND ROUTINE_TYPE = 'PROCEDURE'`,
    [database, name]
  );

  if (routineRows.length === 0) {
    return undefined;
  }

  const [parameterRows] = await pool.query<mysql.RowDataPacket[]>(
    `SELECT PARAMETER_MODE, PARAMETER_NAME, DTD_IDENTIFIER FROM information_schema.parameters WHERE SPECIFIC_SCHEMA = ? AND SPECIFIC_NAME = ? ORDER BY ORDINAL_POSITION`,
    [database, name]
  );

  const parameters = parameterRows
    .map((row) => {
      const mode = row.PARAMETER_MODE ? `${row.PARAMETER_MODE} ` : "";
      const namePart = row.PARAMETER_NAME ? `${row.PARAMETER_NAME} ` : "";
      return `${mode}${namePart}${row.DTD_IDENTIFIER ?? ""}`.trim();
    })
    .join(", ");

  return {
    definition: String(routineRows[0].ROUTINE_DEFINITION ?? ""),
    parameters,
  };
}

function determineDrift(
  existing: ExistingProcedureMeta | undefined,
  parsed: ParsedProcedure
) {
  if (!existing) {
    return true;
  }

  const normalizedExisting = normalizeWhitespace(existing.definition ?? "");
  const normalizedBody = normalizeWhitespace(parsed.body);

  if (normalizedExisting !== normalizedBody) {
    return true;
  }

  if (parsed.parameterList) {
    const existingParams = normalizeWhitespace(existing.parameters ?? "");
    const expectedParams = normalizeWhitespace(parsed.parameterList ?? "");
    if (existingParams !== expectedParams) {
      return true;
    }
  }

  return false;
}

async function applyProcedure(
  pool: mysql.Pool,
  parsed: ParsedProcedure,
  database: string
) {
  const connection = await pool.getConnection();
  try {
    await connection.query(`USE \`${database}\``);
    if (parsed.dropStatement) {
      await connection.query(parsed.dropStatement);
    } else {
      await connection.query(`DROP PROCEDURE IF EXISTS \`${parsed.name}\``);
    }
    await connection.query(parsed.createStatement);
  } finally {
    connection.release();
  }
}

function normalizeWhitespace(value: string): string {
  return value.replace(/\s+/g, " ").trim().toLowerCase();
}
