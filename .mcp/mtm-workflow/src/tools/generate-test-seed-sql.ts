import * as fs from "fs";
import * as path from "path";

type SeedValue = string | number | boolean | null | { raw: string };

interface SeedInsert {
  table: string;
  columns: string[];
  rows: SeedValue[][];
  comment?: string;
}

interface CleanupInstruction {
  statement?: string;
  table?: string;
  where?: string;
  comment?: string;
}

interface SeedConfig {
  database?: string;
  variables?: Record<string, string>;
  transaction?: boolean;
  cleanup?: (string | CleanupInstruction)[];
  inserts?: SeedInsert[];
  statements?: string[];
  tablesTouched?: string[];
}

interface GenerateTestSeedSqlArgs {
  output_sql?: string;
  config_file?: string;
}

export async function generateTestSeedSql(
  args: GenerateTestSeedSqlArgs
): Promise<{ content: Array<{ type: string; text: string }> }> {
  if (!args.config_file) {
    throw new Error(
      "config_file is required. Provide a JSON configuration describing database, cleanup, and seed data."
    );
  }

  const config = await loadConfig(args.config_file);

  const scriptLines: string[] = [];
  scriptLines.push(
    "-- Auto-generated integration test seed script",
    `-- Generated on ${new Date().toISOString()}`
  );

  if (config.database) {
      scriptLines.push(`USE ${escapeIdentifier(config.database)};`);
  }

  const useTransaction = config.transaction !== false;
  if (useTransaction) {
    scriptLines.push("START TRANSACTION;");
  }

  appendVariableStatements(scriptLines, config.variables ?? {});
  appendCleanup(scriptLines, config.cleanup ?? []);
  appendInsertStatements(scriptLines, config.inserts ?? []);
  appendCustomStatements(scriptLines, config.statements ?? []);

  if (useTransaction) {
    scriptLines.push("COMMIT;");
  }

  const script = scriptLines.join("\n") + "\n";

  let outputMessage = script;

  if (args.output_sql) {
    const targetPath = path.resolve(args.output_sql);
    fs.mkdirSync(path.dirname(targetPath), { recursive: true });
    fs.writeFileSync(targetPath, script, "utf-8");
    outputMessage = `Seed script written to ${targetPath}`;
  }

  const tablesTouched = buildTablesTouched(config);
  const instructionsLines: string[] = [
    "## Integration Test Seed Script",
    "",
    outputMessage,
    "",
    "### Apply with MySQL CLI",
    "",
    "```powershell",
    `mysql --host=localhost --user=root --password=root${config.database ? ` --database=${config.database}` : ""} < path\\to\\seed.sql`,
    "```",
    "",
  ];

  if (tablesTouched.length > 0) {
    instructionsLines.push("Tables touched:");
    for (const table of tablesTouched) {
      instructionsLines.push(`- ${table}`);
    }
  }

  const instructions = instructionsLines.join("\n");

  return {
    content: [
      {
        type: "text",
        text: instructions,
      },
    ],
  };
}

async function loadConfig(configFile: string): Promise<SeedConfig> {
  const resolved = path.resolve(configFile);
  if (!fs.existsSync(resolved)) {
    throw new Error(`Config file not found: ${resolved}`);
  }

  const fileContent = fs.readFileSync(resolved, "utf-8");
  try {
    return JSON.parse(fileContent) as SeedConfig;
  } catch (error) {
    const message = error instanceof Error ? error.message : String(error);
    throw new Error(`Failed to parse JSON config: ${message}`);
  }
}

function appendVariableStatements(
  scriptLines: string[],
  variables: Record<string, string>
) {
  const entries = Object.entries(variables);
  if (entries.length === 0) {
    return;
  }

  scriptLines.push("", "-- Variable initialization");
  for (const [name, expression] of entries) {
    if (!/^@?[A-Za-z0-9_]+$/.test(name)) {
      throw new Error(`Invalid variable name '${name}'. Use alphanumeric/underscore, optionally prefixed with @.`);
    }
    scriptLines.push(`SET ${name.startsWith("@") ? name : `@${name}`} := ${expression};`);
  }
}

function appendCleanup(
  scriptLines: string[],
  cleanup: (string | CleanupInstruction)[]
) {
  if (cleanup.length === 0) {
    return;
  }

  scriptLines.push("", "-- Cleanup prior test data");

  for (const entry of cleanup) {
    if (typeof entry === "string") {
      scriptLines.push(entry.endsWith(";") ? entry : `${entry};`);
      continue;
    }

    if (entry.comment) {
      scriptLines.push(`-- ${entry.comment}`);
    }

    if (entry.statement) {
      scriptLines.push(entry.statement.endsWith(";") ? entry.statement : `${entry.statement};`);
      continue;
    }

    if (entry.table && entry.where) {
      scriptLines.push(
        `DELETE FROM ${escapeIdentifier(entry.table)} WHERE ${entry.where.endsWith(";") ? entry.where.slice(0, -1) : entry.where};`
      );
      continue;
    }

    throw new Error(
      "Cleanup entries must be either raw SQL strings or objects with statement/table+where."
    );
  }
}

function appendInsertStatements(scriptLines: string[], inserts: SeedInsert[]) {
  for (const insert of inserts) {
    if (!insert.table || !insert.columns || !insert.rows) {
      throw new Error(
        "Insert definitions require 'table', 'columns', and 'rows'."
      );
    }

    const columns = insert.columns.map(escapeIdentifier).join(", ");
    const values = insert.rows.map((row) => formatRow(row)).join(",\n");

    scriptLines.push("");
    if (insert.comment) {
      scriptLines.push(`-- ${insert.comment}`);
    }

    scriptLines.push(
      `INSERT INTO ${escapeIdentifier(insert.table)} (${columns})`,
      "VALUES",
      `${values};`
    );
  }
}

function appendCustomStatements(scriptLines: string[], statements: string[]) {
  if (statements.length === 0) {
    return;
  }

  scriptLines.push("", "-- Additional statements");
  for (const statement of statements) {
    scriptLines.push(statement.endsWith(";") ? statement : `${statement};`);
  }
}

function formatRow(row: SeedValue[]): string {
  const formatted = row.map((value) => formatValue(value)).join(", ");
  return `  (${formatted})`;
}

function formatValue(value: SeedValue): string {
  if (value === null) {
    return "NULL";
  }

  if (typeof value === "number") {
    return value.toString();
  }

  if (typeof value === "boolean") {
    return value ? "1" : "0";
  }

  if (typeof value === "string") {
    return `'${escapeSql(value)}'`;
  }

  if (typeof value === "object" && "raw" in value) {
    return value.raw;
  }

  throw new Error(`Unsupported seed value: ${JSON.stringify(value)}`);
}

function escapeSql(value: string): string {
  return value.replace(/'/g, "''");
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

function buildTablesTouched(config: SeedConfig): string[] {
  const tables = new Set<string>();
  for (const insert of config.inserts ?? []) {
    tables.add(insert.table);
  }
  for (const table of config.tablesTouched ?? []) {
    tables.add(table);
  }
  return Array.from(tables);
}
