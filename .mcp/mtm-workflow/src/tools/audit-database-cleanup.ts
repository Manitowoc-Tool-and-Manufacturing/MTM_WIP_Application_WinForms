import * as path from "path";
import * as fs from "fs";
import mysql from "mysql2/promise";

interface CleanupRule {
  table: string;
  where: string;
  comment?: string;
  preview_limit?: number;
  auto_delete?: boolean;
}

interface CleanupConfig {
  connection?: {
    host?: string;
    port?: number;
    user?: string;
    password?: string;
    database?: string;
  };
  rules: CleanupRule[];
  dry_run?: boolean;
}

interface AuditDatabaseCleanupArgs {
  config_file: string;
  host?: string;
  port?: number;
  user?: string;
  password?: string;
  database?: string;
  dry_run?: boolean;
}

export async function auditDatabaseCleanup(
  args: AuditDatabaseCleanupArgs
): Promise<{ content: Array<{ type: "text"; text: string }>; isError?: boolean }> {
  if (!args.config_file) {
    throw new Error("config_file is required.");
  }

  const config = await loadCleanupConfig(args.config_file);
  if (!config.rules || config.rules.length === 0) {
    throw new Error("No cleanup rules defined in config file.");
  }

  const connectionInfo = buildConnection(config.connection, args);
  const dryRun = args.dry_run ?? config.dry_run ?? true;

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
  let sawDeletions = false;

  try {
    for (const rule of config.rules) {
      const result = await processRule(pool, rule, dryRun);
      lines.push(...result.messages);
      if (result.deletedRows > 0) {
        sawDeletions = true;
      }
    }
  } finally {
    await pool.end();
  }

  if (sawDeletions) {
    lines.unshift(dryRun ? "⚠️ Dry run detected rows that would be deleted." : "✅ Cleanup executed deletions.");
  } else {
    lines.unshift("✅ No deletions required.");
  }

  return {
    content: [
      {
        type: "text",
        text: lines.join("\n"),
      },
    ],
    isError: false,
  };
}

async function loadCleanupConfig(configFile: string): Promise<CleanupConfig> {
  const resolved = path.resolve(configFile);
  if (!fs.existsSync(resolved)) {
    throw new Error(`Config file not found: ${resolved}`);
  }
  const raw = fs.readFileSync(resolved, "utf-8");
  try {
    return JSON.parse(raw) as CleanupConfig;
  } catch (error) {
    const message = error instanceof Error ? error.message : String(error);
    throw new Error(`Failed to parse JSON config: ${message}`);
  }
}

function buildConnection(
  connection: CleanupConfig["connection"],
  overrides: AuditDatabaseCleanupArgs
) {
  return {
    host: overrides.host ?? connection?.host ?? "127.0.0.1",
    port: overrides.port ?? connection?.port ?? 3306,
    user: overrides.user ?? connection?.user ?? "root",
    password: overrides.password ?? connection?.password ?? "root",
    database: overrides.database ?? connection?.database ?? "mtm_wip_application_winforms_test",
  };
}

async function processRule(
  pool: mysql.Pool,
  rule: CleanupRule,
  dryRun: boolean
): Promise<{ deletedRows: number; messages: string[] }> {
  const messages: string[] = [];
  const header = rule.comment ? `${rule.table} (${rule.comment})` : rule.table;

  const countQuery = `SELECT COUNT(*) AS count FROM ${escapeIdentifier(rule.table)} WHERE ${rule.where}`;
  const [countRows] = await pool.query<mysql.RowDataPacket[]>(countQuery);
  const count = Number(countRows[0]?.count ?? 0);

  messages.push(`${header}: ${count} matching rows.`);

  if (count > 0 && rule.preview_limit && rule.preview_limit > 0) {
    const previewQuery = `SELECT * FROM ${escapeIdentifier(rule.table)} WHERE ${rule.where} LIMIT ${rule.preview_limit}`;
    const [previewRows] = await pool.query<mysql.RowDataPacket[]>(previewQuery);
    messages.push(
      `  Preview (${previewRows.length} rows): ${JSON.stringify(previewRows, null, 2)}`
    );
  }

  if (!dryRun && rule.auto_delete && count > 0) {
    const deleteQuery = `DELETE FROM ${escapeIdentifier(rule.table)} WHERE ${rule.where}`;
    const [deleteResult] = await pool.query<mysql.ResultSetHeader>(deleteQuery);
    messages.push(`  Deleted ${deleteResult.affectedRows} rows.`);
    return { deletedRows: deleteResult.affectedRows, messages };
  }

  if (dryRun && rule.auto_delete && count > 0) {
    messages.push("  Dry run: deletion skipped.");
  }

  return { deletedRows: 0, messages };
}

function escapeIdentifier(identifier: string): string {
  if (!/^[A-Za-z0-9_.]+$/.test(identifier)) {
    throw new Error(
      `Identifier '${identifier}' contains invalid characters. Use alphanumeric/underscore (schema.table supported).`
    );
  }
  if (identifier.includes(".")) {
    return identifier
      .split(".")
      .map((segment) => `\`${segment}\``)
      .join(".");
  }
  return `\`${identifier}\``;
}
