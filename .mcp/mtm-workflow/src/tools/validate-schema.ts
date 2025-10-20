import * as fs from "fs";
import * as path from "path";
import mysql from "mysql2/promise";

interface ConnectionConfig {
  host?: string;
  port?: number;
  user?: string;
  password?: string;
  database?: string;
}

interface SchemaValidatorConfig {
  connection?: ConnectionConfig;
  snapshot_file: string;
  include_tables?: string[];
  exclude_tables?: string[];
  fail_on_missing_table?: boolean;
  fail_on_missing_column?: boolean;
  fail_on_type_mismatch?: boolean;
}

interface ValidateSchemaArgs {
  config_file: string;
  host?: string;
  port?: number;
  user?: string;
  password?: string;
  database?: string;
}

interface SnapshotTable {
  TABLE_SCHEMA: string;
  TABLE_NAME: string;
  [key: string]: unknown;
}

interface SnapshotColumn {
  TABLE_SCHEMA: string;
  TABLE_NAME: string;
  COLUMN_NAME: string;
  COLUMN_TYPE: string;
  IS_NULLABLE: string;
  COLUMN_DEFAULT: unknown;
  DATA_TYPE: string;
}

export async function validateSchema(
  args: ValidateSchemaArgs
): Promise<{ content: Array<{ type: "text"; text: string }>; isError?: boolean }> {
  const { config, baseDir } = await loadSchemaConfig(args.config_file);
  const snapshot = await loadSnapshot(config.snapshot_file, baseDir);

  const connectionInfo = buildConnection(config.connection, args, snapshot.metadata?.database);

  const tablesToCheck = determineTablesToCheck(
    snapshot.tables,
    config.include_tables,
    config.exclude_tables,
    connectionInfo.database
  );

  const columnsByTable = buildColumnsMap(snapshot.columns);

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

  const lines: string[] = [];
  let hasFailure = false;

  try {
    for (const table of tablesToCheck) {
      const tableResult = await validateTable(
        pool,
        table,
        columnsByTable.get(table) ?? [],
        config
      );
      lines.push(...tableResult.messages);
      if (tableResult.failed) {
        hasFailure = true;
      }
    }
  } finally {
    await pool.end();
  }

  if (hasFailure) {
    lines.unshift("❌ Schema validation found issues.");
  } else {
    lines.unshift("✅ Schema validation passed.");
  }

  return {
    content: [
      {
        type: "text",
        text: lines.join("\n"),
      },
    ],
    isError: hasFailure,
  };
}

async function loadSchemaConfig(configFile: string): Promise<{ config: SchemaValidatorConfig; baseDir: string }> {
  const resolved = path.resolve(configFile);
  if (!fs.existsSync(resolved)) {
    throw new Error(`Config file not found: ${resolved}`);
  }
  const raw = fs.readFileSync(resolved, "utf-8");
  try {
    return { config: JSON.parse(raw) as SchemaValidatorConfig, baseDir: path.dirname(resolved) };
  } catch (error) {
    const message = error instanceof Error ? error.message : String(error);
    throw new Error(`Failed to parse JSON config: ${message}`);
  }
}

async function loadSnapshot(snapshotFile: string, baseDir: string) {
  const resolved = path.resolve(baseDir, snapshotFile);
  if (!fs.existsSync(resolved)) {
    throw new Error(`Snapshot file not found: ${resolved}`);
  }
  const raw = fs.readFileSync(resolved, "utf-8");
  try {
    return JSON.parse(raw) as {
      metadata?: { database?: string };
      tables: SnapshotTable[];
      columns: SnapshotColumn[];
    };
  } catch (error) {
    const message = error instanceof Error ? error.message : String(error);
    throw new Error(`Failed to parse snapshot JSON: ${message}`);
  }
}

function buildConnection(
  connection: ConnectionConfig | undefined,
  overrides: ValidateSchemaArgs,
  snapshotDatabase?: string
) {
  return {
    host: overrides.host ?? connection?.host ?? "127.0.0.1",
    port: overrides.port ?? connection?.port ?? 3306,
    user: overrides.user ?? connection?.user ?? "root",
    password: overrides.password ?? connection?.password ?? "root",
    database: overrides.database ?? connection?.database ?? snapshotDatabase ?? "mtm_wip_application_winforms_test",
  };
}

function determineTablesToCheck(
  snapshotTables: SnapshotTable[],
  include: string[] | undefined,
  exclude: string[] | undefined,
  database: string
) {
  const normalizedInclude = include?.map((t) => t.toLowerCase());
  const normalizedExclude = new Set(exclude?.map((t) => t.toLowerCase()) ?? []);

  const tables = snapshotTables
    .filter((table) => table.TABLE_SCHEMA.toLowerCase() === database.toLowerCase())
    .map((table) => table.TABLE_NAME);

  const filtered = normalizedInclude
    ? tables.filter((table) => normalizedInclude.includes(table.toLowerCase()))
    : tables;

  return filtered.filter((table) => !normalizedExclude.has(table.toLowerCase()));
}

function buildColumnsMap(columns: SnapshotColumn[]) {
  const map = new Map<string, SnapshotColumn[]>();
  for (const column of columns) {
    const key = `${column.TABLE_SCHEMA}.${column.TABLE_NAME}`;
    if (!map.has(key)) {
      map.set(key, []);
    }
    map.get(key)!.push(column);
  }
  return map;
}

async function validateTable(
  pool: mysql.Pool,
  tableName: string,
  expectedColumns: SnapshotColumn[],
  config: SchemaValidatorConfig
): Promise<{ failed: boolean; messages: string[] }> {
  const messages: string[] = [];
  const tableExists = await doesTableExist(pool, tableName);

  let failed = false;
  const header = `Table ${tableName}`;

  if (!tableExists) {
    messages.push(`${header}: MISSING`);
    if (config.fail_on_missing_table !== false) {
      failed = true;
    }
    return { failed, messages };
  }

  messages.push(`${header}: present`);

  if (expectedColumns.length === 0) {
    return { failed, messages };
  }

  const [rows] = await pool.query<mysql.RowDataPacket[]>(
    `SELECT COLUMN_NAME, COLUMN_TYPE, IS_NULLABLE, DATA_TYPE FROM information_schema.columns WHERE table_schema = DATABASE() AND table_name = ?`,
    [tableName]
  );

  const liveColumns = new Map<string, mysql.RowDataPacket>();
  for (const row of rows) {
    liveColumns.set(String(row.COLUMN_NAME).toLowerCase(), row);
  }

  for (const expectedColumn of expectedColumns) {
    if (expectedColumn.TABLE_NAME !== tableName) {
      continue;
    }
    const live = liveColumns.get(expectedColumn.COLUMN_NAME.toLowerCase());
    if (!live) {
      messages.push(`  - Missing column: ${expectedColumn.COLUMN_NAME}`);
      if (config.fail_on_missing_column !== false) {
        failed = true;
      }
      continue;
    }

    const typeMatches = normalizeType(String(live.COLUMN_TYPE)) === normalizeType(expectedColumn.COLUMN_TYPE);
    const nullableMatches = String(live.IS_NULLABLE) === String(expectedColumn.IS_NULLABLE);

    if (!typeMatches || !nullableMatches) {
      const diffs: string[] = [];
      if (!typeMatches) {
        diffs.push(`type expected ${expectedColumn.COLUMN_TYPE}, found ${live.COLUMN_TYPE}`);
      }
      if (!nullableMatches) {
        diffs.push(`nullability expected ${expectedColumn.IS_NULLABLE}, found ${live.IS_NULLABLE}`);
      }
      messages.push(`  - Column mismatch: ${expectedColumn.COLUMN_NAME} (${diffs.join(", ")})`);
      if (config.fail_on_type_mismatch !== false) {
        failed = true;
      }
    }
  }

  return { failed, messages };
}

async function doesTableExist(pool: mysql.Pool, table: string): Promise<boolean> {
  const [rows] = await pool.query<mysql.RowDataPacket[]>(
    `SELECT 1 FROM information_schema.tables WHERE table_schema = DATABASE() AND table_name = ? LIMIT 1`,
    [table]
  );
  return rows.length > 0;
}

function normalizeType(type: string): string {
  return type.replace(/\s+/g, "").toLowerCase();
}
