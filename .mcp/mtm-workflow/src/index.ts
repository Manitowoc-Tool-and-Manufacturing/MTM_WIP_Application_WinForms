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
