#!/usr/bin/env node

import { Server } from "@modelcontextprotocol/sdk/server/index.js";
import { StdioServerTransport } from "@modelcontextprotocol/sdk/server/stdio.js";
import {
  CallToolRequestSchema,
  ListToolsRequestSchema,
  Tool,
} from "@modelcontextprotocol/sdk/types.js";
import * as fs from "fs";
import * as path from "path";

// Tool implementations
import { checkChecklists } from "./tools/check-checklists.js";
import { validateDaoPatterns } from "./tools/validate-dao-patterns.js";
import { analyzeStoredProcedures } from "./tools/analyze-stored-procedures.js";
import { compareDatabases } from "./tools/compare-databases.js";
import { generateDaoWrapper } from "./tools/generate-dao-wrapper.js";
import { validateErrorHandling } from "./tools/validate-error-handling.js";
import { analyzeDependencies } from "./tools/analyze-dependencies.js";
import { checkXmlDocs } from "./tools/check-xml-docs.js";
import { suggestRefactoring } from "./tools/suggest-refactoring.js";
import { generateUnitTests } from "./tools/generate-unit-tests.js";
import { analyzePerformance } from "./tools/analyze-performance.js";
import { checkSecurity } from "./tools/check-security.js";

// Speckit tools
import { parseTasks } from "./tools/speckit/parse-tasks.js";
import { loadInstructions } from "./tools/speckit/load-instructions.js";
import { markTaskComplete } from "./tools/speckit/mark-task-complete.js";
import { validateBuild } from "./tools/speckit/validate-build.js";
import { verifyIgnoreFiles } from "./tools/speckit/verify-ignore-files.js";
import { analyzeSpecContext } from "./tools/speckit/analyze-spec-context.js";

const server = new Server(
  {
    name: "mtm-workflow-mcp",
    version: "1.0.0",
  },
  {
    capabilities: {
      tools: {},
    },
  }
);

// Define available tools
const tools: Tool[] = [
  {
    name: "check_checklists",
    description:
      "Analyze markdown checklist files in a directory to determine completion status. Counts total, completed, and incomplete checklist items. Returns a summary table and overall pass/fail status.",
    inputSchema: {
      type: "object",
      properties: {
        checklist_dir: {
          type: "string",
          description: "Absolute path to directory containing checklist markdown files",
        },
      },
      required: ["checklist_dir"],
    },
  },
  {
    name: "validate_dao_patterns",
    description:
      "Validate C# DAO files for compliance with MTM coding standards. Checks for: proper region organization, async/await patterns, Helper_Database_StoredProcedure usage, error handling with Service_ErrorHandler, XML documentation. Returns validation results with specific issues found.",
    inputSchema: {
      type: "object",
      properties: {
        dao_dir: {
          type: "string",
          description: "Absolute path to directory containing DAO C# files",
        },
        recursive: {
          type: "boolean",
          description: "Whether to search subdirectories recursively",
          default: true,
        },
      },
      required: ["dao_dir"],
    },
  },
  {
    name: "analyze_stored_procedures",
    description:
      "Scan SQL stored procedure files for compliance with MTM standards. Checks for: required output parameters (p_Status, p_ErrorMsg), transaction management, error handling, parameter naming conventions (p_ prefix), and potential SQL injection vulnerabilities.",
    inputSchema: {
      type: "object",
      properties: {
        procedures_dir: {
          type: "string",
          description: "Absolute path to directory containing SQL procedure files",
        },
        recursive: {
          type: "boolean",
          description: "Whether to search subdirectories recursively",
          default: true,
        },
      },
      required: ["procedures_dir"],
    },
  },
  {
    name: "compare_databases",
    description:
      "Detect schema drift between Current and Updated database directories. Identifies added, removed, and modified tables and stored procedures. Useful for validating database migration changes.",
    inputSchema: {
      type: "object",
      properties: {
        current_dir: {
          type: "string",
          description: "Absolute path to current database directory",
        },
        updated_dir: {
          type: "string",
          description: "Absolute path to updated database directory",
        },
      },
      required: ["current_dir", "updated_dir"],
    },
  },
  {
    name: "generate_dao_wrapper",
    description:
      "Auto-generate C# DAO wrapper code from a stored procedure file. Parses procedure parameters and creates a complete DAO method with proper Helper_Database_StoredProcedure usage, error handling, and XML documentation.",
    inputSchema: {
      type: "object",
      properties: {
        procedure_file: {
          type: "string",
          description: "Absolute path to SQL stored procedure file",
        },
        output_dir: {
          type: "string",
          description: "Optional: Directory to write generated DAO file",
        },
      },
      required: ["procedure_file"],
    },
  },
  {
    name: "validate_error_handling",
    description:
      "Check C# source files for proper error handling patterns. Identifies usage of MessageBox.Show (anti-pattern), validates Service_ErrorHandler usage, checks try-catch blocks, and ensures async methods have proper error handling.",
    inputSchema: {
      type: "object",
      properties: {
        source_dir: {
          type: "string",
          description: "Absolute path to source code directory",
        },
        recursive: {
          type: "boolean",
          description: "Whether to search subdirectories recursively",
          default: true,
        },
      },
      required: ["source_dir"],
    },
  },
  {
    name: "analyze_dependencies",
    description:
      "Map stored procedure call hierarchies and dependency graphs. Identifies root procedures (entry points), leaf procedures (terminal operations), circular dependencies, and call depth statistics. Generates visual dependency tree.",
    inputSchema: {
      type: "object",
      properties: {
        procedures_dir: {
          type: "string",
          description: "Absolute path to directory containing SQL procedure files",
        },
      },
      required: ["procedures_dir"],
    },
  },
  {
    name: "check_xml_docs",
    description:
      "Validate XML documentation coverage for C# code. Scans public classes, methods, properties, and fields for <summary> tags. Reports coverage percentage and identifies undocumented members.",
    inputSchema: {
      type: "object",
      properties: {
        source_dir: {
          type: "string",
          description: "Absolute path to source code directory",
        },
        recursive: {
          type: "boolean",
          description: "Whether to search subdirectories recursively",
          default: true,
        },
        min_coverage: {
          type: "number",
          description: "Minimum required coverage percentage (default: 80)",
          default: 80,
        },
      },
      required: ["source_dir"],
    },
  },
  {
    name: "suggest_refactoring",
    description:
      "AI-powered refactoring suggestions for C# and SQL code. Identifies code smells, complexity issues, performance problems, and maintainability concerns. Provides prioritized recommendations with examples.",
    inputSchema: {
      type: "object",
      properties: {
        source_dir: {
          type: "string",
          description: "Absolute path to source code directory",
        },
        recursive: {
          type: "boolean",
          description: "Whether to search subdirectories recursively",
          default: true,
        },
        file_type: {
          type: "string",
          description: "Type of files to analyze: csharp, sql, or all",
          enum: ["csharp", "sql", "all"],
          default: "all",
        },
      },
      required: ["source_dir"],
    },
  },
  {
    name: "generate_unit_tests",
    description:
      "Auto-generate unit test scaffolding for C# classes. Analyzes public methods and creates test class with happy path, error case, and edge case tests. Supports xUnit, NUnit, and MSTest frameworks.",
    inputSchema: {
      type: "object",
      properties: {
        source_file: {
          type: "string",
          description: "Absolute path to C# source file to generate tests for",
        },
        output_dir: {
          type: "string",
          description: "Optional: Directory to write generated test file",
        },
        test_framework: {
          type: "string",
          description: "Test framework: xunit, nunit, or mstest (default: xunit)",
          enum: ["xunit", "nunit", "mstest"],
          default: "xunit",
        },
      },
      required: ["source_file"],
    },
  },
  {
    name: "analyze_performance",
    description:
      "Identify performance bottlenecks in C# code. Checks for N+1 queries, blocking async operations, UI thread blocking, inefficient LINQ, string concatenation issues, and memory leaks. Provides performance score and recommendations.",
    inputSchema: {
      type: "object",
      properties: {
        source_dir: {
          type: "string",
          description: "Absolute path to source code directory",
        },
        recursive: {
          type: "boolean",
          description: "Whether to search subdirectories recursively",
          default: true,
        },
        focus: {
          type: "string",
          description: "Focus area: database, ui, or all (default: all)",
          enum: ["database", "ui", "all"],
          default: "all",
        },
      },
      required: ["source_dir"],
    },
  },
  {
    name: "check_security",
    description:
      "Security vulnerability scanner for C# and SQL code. Detects SQL injection, hardcoded credentials, path traversal, weak cryptography, command injection, missing authorization, and other OWASP vulnerabilities. Maps to CWE IDs.",
    inputSchema: {
      type: "object",
      properties: {
        source_dir: {
          type: "string",
          description: "Absolute path to source code directory",
        },
        recursive: {
          type: "boolean",
          description: "Whether to search subdirectories recursively",
          default: true,
        },
        scan_type: {
          type: "string",
          description: "Scan type: code, config, or all (default: all)",
          enum: ["code", "config", "all"],
          default: "all",
        },
      },
      required: ["source_dir"],
    },
  },
  // Speckit implementation tools
  {
    name: "parse_tasks",
    description:
      "Parse tasks.md file and extract structured task information. Returns task phases, completion status, next actionable tasks, and dependencies. Essential for understanding implementation workflow.",
    inputSchema: {
      type: "object",
      properties: {
        tasks_file: {
          type: "string",
          description: "Absolute path to tasks.md file",
        },
      },
      required: ["tasks_file"],
    },
  },
  {
    name: "load_instructions",
    description:
      "Load and analyze instruction file references from tasks.md. Identifies which instruction files are needed for which tasks, verifies file existence, and loads content for context. Critical for applying correct patterns during implementation.",
    inputSchema: {
      type: "object",
      properties: {
        tasks_file: {
          type: "string",
          description: "Absolute path to tasks.md file",
        },
        instructions_dir: {
          type: "string",
          description: "Absolute path to .github/instructions directory",
        },
      },
      required: ["tasks_file", "instructions_dir"],
    },
  },
  {
    name: "mark_task_complete",
    description:
      "Mark one or more tasks as complete in tasks.md file. Automatically updates task status from [ ] to [X] and adds completion timestamp and notes. Essential for tracking implementation progress.",
    inputSchema: {
      type: "object",
      properties: {
        tasks_file: {
          type: "string",
          description: "Absolute path to tasks.md file",
        },
        task_ids: {
          type: "array",
          items: { type: "string" },
          description: "Array of task IDs to mark complete (e.g., ['T100', 'T101'])",
        },
        note: {
          type: "string",
          description: "Optional note about task completion",
        },
      },
      required: ["tasks_file", "task_ids"],
    },
  },
  {
    name: "validate_build",
    description:
      "Run dotnet build and validate compilation. Checks for errors, warnings, and optionally runs tests. Returns detailed build output with error/warning counts. Use after completing implementation tasks to ensure code compiles.",
    inputSchema: {
      type: "object",
      properties: {
        workspace_root: {
          type: "string",
          description: "Absolute path to workspace root",
        },
        project_file: {
          type: "string",
          description: "Optional: Absolute path to .csproj file (auto-detected if omitted)",
        },
        run_tests: {
          type: "boolean",
          description: "Whether to run tests after build (default: false)",
          default: false,
        },
        check_errors: {
          type: "boolean",
          description: "Whether to scan for compilation errors (default: true)",
          default: true,
        },
      },
      required: ["workspace_root"],
    },
  },
  {
    name: "verify_ignore_files",
    description:
      "Verify .gitignore and other ignore files exist and contain essential patterns. Checks for missing critical patterns based on detected technology stack. Creates or updates ignore files with recommended patterns.",
    inputSchema: {
      type: "object",
      properties: {
        workspace_root: {
          type: "string",
          description: "Absolute path to workspace root",
        },
        tech_stack: {
          type: "array",
          items: { type: "string" },
          description: "Optional: Technology stack tags (e.g., ['csharp', 'dotnet', 'mysql'])",
        },
      },
      required: ["workspace_root"],
    },
  },
  {
    name: "analyze_spec_context",
    description:
      "Analyze feature specification directory and extract implementation context. Scans for standard spec files (spec.md, plan.md, tasks.md, etc.), extracts tech stack, entities, and contracts. Provides recommendations for missing documentation.",
    inputSchema: {
      type: "object",
      properties: {
        feature_dir: {
          type: "string",
          description: "Absolute path to feature specification directory",
        },
      },
      required: ["feature_dir"],
    },
  },
];

// Register tool handlers
server.setRequestHandler(ListToolsRequestSchema, async () => ({
  tools,
}));

server.setRequestHandler(CallToolRequestSchema, async (request) => {
  const { name, arguments: args } = request.params;

  try {
    switch (name) {
      case "check_checklists":
        return await checkChecklists(args as { checklist_dir: string });

      case "validate_dao_patterns":
        return await validateDaoPatterns(
          args as { dao_dir: string; recursive?: boolean }
        );

      case "analyze_stored_procedures":
        return await analyzeStoredProcedures(
          args as { procedures_dir: string; recursive?: boolean }
        );

      case "compare_databases":
        return await compareDatabases(
          args as { current_dir: string; updated_dir: string }
        );

      case "generate_dao_wrapper":
        return await generateDaoWrapper(
          args as { procedure_file: string; output_dir?: string }
        );

      case "validate_error_handling":
        return await validateErrorHandling(
          args as { source_dir: string; recursive?: boolean }
        );

      case "analyze_dependencies":
        return await analyzeDependencies(
          args as { procedures_dir: string }
        );

      case "check_xml_docs":
        return await checkXmlDocs(
          args as { source_dir: string; recursive?: boolean; min_coverage?: number }
        );

      case "suggest_refactoring":
        return await suggestRefactoring(
          args as { source_dir: string; recursive?: boolean; file_type?: "csharp" | "sql" | "all" }
        );

      case "generate_unit_tests":
        return await generateUnitTests(
          args as { source_file: string; output_dir?: string; test_framework?: "xunit" | "nunit" | "mstest" }
        );

      case "analyze_performance":
        return await analyzePerformance(
          args as { source_dir: string; recursive?: boolean; focus?: "database" | "ui" | "all" }
        );

      case "check_security":
        return await checkSecurity(
          args as { source_dir: string; recursive?: boolean; scan_type?: "code" | "config" | "all" }
        );

      // Speckit tools
      case "parse_tasks":
        return await parseTasks(args as { tasks_file: string });

      case "load_instructions":
        return await loadInstructions(
          args as { tasks_file: string; instructions_dir: string }
        );

      case "mark_task_complete":
        return await markTaskComplete(
          args as { tasks_file: string; task_ids: string[]; note?: string }
        );

      case "validate_build":
        return await validateBuild(
          args as { workspace_root: string; project_file?: string; run_tests?: boolean; check_errors?: boolean }
        );

      case "verify_ignore_files":
        return await verifyIgnoreFiles(
          args as { workspace_root: string; tech_stack?: string[] }
        );

      case "analyze_spec_context":
        return await analyzeSpecContext(args as { feature_dir: string });

      default:
        throw new Error(`Unknown tool: ${name}`);
    }
  } catch (error) {
    const message = error instanceof Error ? error.message : String(error);
    return {
      content: [
        {
          type: "text",
          text: `Error executing ${name}: ${message}`,
        },
      ],
      isError: true,
    };
  }
});

// Start the server
async function main() {
  const transport = new StdioServerTransport();
  await server.connect(transport);
  console.error("MTM Workflow MCP Server running on stdio");
}

main().catch((error) => {
  console.error("Fatal error in main():", error);
  process.exit(1);
});
