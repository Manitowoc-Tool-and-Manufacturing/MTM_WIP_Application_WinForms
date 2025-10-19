import * as fs from "fs";
import * as path from "path";
import mysql from "mysql2/promise";

interface SeedValueMap {
  [column: string]: string | number | boolean | null;
}

interface VerificationStep {
  table: string;
  where?: string;
  select?: string[];
  expectedRows?: SeedValueMap[];
  exactCount?: number;
  minCount?: number;
  maxCount?: number;
  unordered?: boolean;
  comment?: string;
}

interface VerificationConfig {
  database?: string;
  connection?: {
    host?: string;
    port?: number;
    user?: string;
    password?: string;
    database?: string;
  };
  verification?: VerificationStep[];
}

interface VerifyTestSeedArgs {
  config_file: string;
  host?: string;
  port?: number;
  user?: string;
  password?: string;
  database?: string;
}

export async function verifyTestSeed(
  args: VerifyTestSeedArgs
): Promise<{ content: Array<{ type: "text"; text: string }>; isError?: boolean }> {
  if (!args.config_file) {
    throw new Error("config_file is required. Provide the JSON configuration used for seeding.");
  }

  const config = await loadVerificationConfig(args.config_file);
  if (!config.verification || config.verification.length === 0) {
    throw new Error("No verification steps defined in configuration (verification array is missing or empty).");
  }

  const connectionConfig = buildConnectionConfig(config, args);

  const pool = await mysql.createPool({
    host: connectionConfig.host,
    port: connectionConfig.port,
    user: connectionConfig.user,
    password: connectionConfig.password,
    database: connectionConfig.database,
    waitForConnections: true,
    connectionLimit: 4,
    queueLimit: 0,
  });

  const stepSummaries: string[] = [];
  let overallPass = true;

  try {
    for (const step of config.verification) {
      const result = await executeVerificationStep(pool, step, connectionConfig.database);
      stepSummaries.push(formatStepResult(result));
      if (!result.passed) {
        overallPass = false;
      }
    }
  } finally {
    await pool.end();
  }

  const summaryHeader = overallPass ? "✅ Verification PASSED" : "❌ Verification FAILED";
  const message = [summaryHeader, "", ...stepSummaries].join("\n");

  return {
    content: [
      {
        type: "text",
        text: message,
      },
    ],
    isError: !overallPass,
  };
}

async function loadVerificationConfig(configFile: string): Promise<VerificationConfig> {
  const resolved = path.resolve(configFile);
  if (!fs.existsSync(resolved)) {
    throw new Error(`Config file not found: ${resolved}`);
  }

  const fileContent = fs.readFileSync(resolved, "utf-8");
  try {
    return JSON.parse(fileContent) as VerificationConfig;
  } catch (error) {
    const message = error instanceof Error ? error.message : String(error);
    throw new Error(`Failed to parse JSON config: ${message}`);
  }
}

function buildConnectionConfig(
  config: VerificationConfig,
  args: VerifyTestSeedArgs
) {
  const connection = config.connection ?? {};
  return {
    host: args.host ?? connection.host ?? "127.0.0.1",
    port: args.port ?? connection.port ?? 3306,
    user: args.user ?? connection.user ?? "root",
    password: args.password ?? connection.password ?? "root",
    database:
      args.database ?? connection.database ?? config.database ?? "mtm_wip_application_winforms_test",
  };
}

interface StepResult {
  step: VerificationStep;
  passed: boolean;
  details: string[];
}

function formatStepResult(result: StepResult): string {
  return `${result.details.join("\n")}`;
}

async function executeVerificationStep(
  pool: mysql.Pool,
  step: VerificationStep,
  defaultDatabase: string
): Promise<StepResult> {
  const details: string[] = [];
  const tableIdentifier = escapeIdentifier(step.table);
  const selectFields = step.select && step.select.length > 0
    ? step.select.map(escapeIdentifier).join(", ")
    : "*";
  const whereClause = step.where ? ` WHERE ${step.where}` : "";
  const query = `SELECT ${selectFields} FROM ${tableIdentifier}${whereClause}`;

  const [rows] = await pool.query<mysql.RowDataPacket[]>(query);
  const rowCount = rows.length;
  let passed = true;

  if (typeof step.exactCount === "number") {
    if (rowCount !== step.exactCount) {
      passed = false;
      details.push(
        `Expected exact count ${step.exactCount}, but found ${rowCount}.`
      );
    } else {
      details.push(`Exact count ${step.exactCount} satisfied.`);
    }
  }

  if (typeof step.minCount === "number") {
    if (rowCount < step.minCount) {
      passed = false;
      details.push(`Expected at least ${step.minCount} rows, but found ${rowCount}.`);
    } else {
      details.push(`Minimum count ${step.minCount} satisfied.`);
    }
  }

  if (typeof step.maxCount === "number") {
    if (rowCount > step.maxCount) {
      passed = false;
      details.push(`Expected at most ${step.maxCount} rows, but found ${rowCount}.`);
    } else {
      details.push(`Maximum count ${step.maxCount} satisfied.`);
    }
  }

  if (step.expectedRows && step.expectedRows.length > 0) {
    const comparison = compareRows(rows, step.expectedRows, step.unordered === true);
    if (!comparison.passed) {
      passed = false;
    }
    details.push(...comparison.messages);
  }

  if (passed) {
    details.unshift("✅ Passed");
  } else {
    details.unshift("❌ Failed");
  }

  const header = step.comment
    ? `Table ${step.table} (${step.comment})`
    : `Table ${step.table}`;
  details.unshift(header);

  if (!step.comment) {
    details.splice(1, 0, `Query: ${query}`);
  } else {
    details.splice(2, 0, `Query: ${query}`);
  }

  return { step, passed, details };
}

function compareRows(
  actualRows: mysql.RowDataPacket[],
  expectedRows: SeedValueMap[],
  unordered: boolean
): { passed: boolean; messages: string[] } {
  if (unordered) {
    return compareRowsUnordered(actualRows, expectedRows);
  }

  if (expectedRows.length !== actualRows.length) {
    return {
      passed: false,
      messages: [
        `Expected ${expectedRows.length} rows to compare in order, but found ${actualRows.length}.`,
        `Actual rows: ${JSON.stringify(actualRows, null, 2)}`,
      ],
    };
  }

  const messages: string[] = [];
  let passed = true;

  for (let i = 0; i < expectedRows.length; i++) {
    const expected = expectedRows[i];
    const actual = actualRows[i];
    const rowComparison = compareRow(actual, expected, i);
    if (!rowComparison.passed) {
      passed = false;
    }
    messages.push(...rowComparison.messages);
  }

  if (passed) {
    messages.unshift("Expected rows matched in order.");
  }

  return { passed, messages };
}

function compareRowsUnordered(
  actualRows: mysql.RowDataPacket[],
  expectedRows: SeedValueMap[]
): { passed: boolean; messages: string[] } {
  const remaining = [...actualRows];
  const messages: string[] = [];
  let passed = true;

  for (const expected of expectedRows) {
    const matchIndex = remaining.findIndex((row) => rowMatches(row, expected));
    if (matchIndex === -1) {
      passed = false;
      messages.push(`No match found for expected row: ${JSON.stringify(expected)}`);
    } else {
      remaining.splice(matchIndex, 1);
    }
  }

  if (passed) {
    messages.unshift("Expected rows found (unordered).");
  } else {
    messages.push(`Actual rows: ${JSON.stringify(actualRows, null, 2)}`);
  }

  return { passed, messages };
}

function compareRow(
  actual: mysql.RowDataPacket,
  expected: SeedValueMap,
  index: number
): { passed: boolean; messages: string[] } {
  const messages: string[] = [];
  let passed = true;

  for (const [column, expectedValue] of Object.entries(expected)) {
    const actualValue = actual[column];
    if (!valuesEqual(actualValue, expectedValue)) {
      passed = false;
      messages.push(
        `Row ${index}: column '${column}' mismatch. Expected ${formatValue(expectedValue)}, got ${formatValue(actualValue)}.`
      );
    }
  }

  if (passed) {
    messages.push(`Row ${index} matched expected values.`);
  }

  return { passed, messages };
}

function rowMatches(row: mysql.RowDataPacket, expected: SeedValueMap): boolean {
  return Object.entries(expected).every(([column, value]) => valuesEqual(row[column], value));
}

function valuesEqual(actual: unknown, expected: unknown): boolean {
  if (actual === null || actual === undefined) {
    return expected === null || expected === undefined;
  }

  if (typeof expected === "number") {
    const numericActual = typeof actual === "number" ? actual : Number(actual);
    return numericActual === expected;
  }

  if (typeof expected === "boolean") {
    if (typeof actual === "boolean") {
      return actual === expected;
    }
    if (typeof actual === "number") {
      return (actual !== 0) === expected;
    }
    return String(actual).toLowerCase() === String(expected).toLowerCase();
  }

  return String(actual) === String(expected);
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

function formatValue(value: unknown): string {
  if (value === null) {
    return "NULL";
  }
  if (typeof value === "string") {
    return `'${value}'`;
  }
  return String(value);
}
``