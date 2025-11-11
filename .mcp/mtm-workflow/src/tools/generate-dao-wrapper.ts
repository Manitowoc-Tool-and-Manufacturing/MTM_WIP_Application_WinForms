import * as fs from "fs";
import * as path from "path";

interface DaoGenerationResult {
  procedure_name: string;
  dao_class_name: string;
  dao_method_name: string;
  parameters: Array<{ name: string; type: string; direction: string }>;
  generated_code: string;
}

export async function generateDaoWrapper(args: {
  procedure_file: string;
  output_dir?: string;
}): Promise<{ content: Array<{ type: string; text: string }> }> {
  const { procedure_file, output_dir } = args;

  if (!fs.existsSync(procedure_file)) {
    throw new Error(`Stored procedure file not found: ${procedure_file}`);
  }

  const content = fs.readFileSync(procedure_file, "utf-8");
  const result = parseProcedureAndGenerateDao(content, procedure_file);

  let message = `
## DAO Wrapper Generated

### Stored Procedure
**Name:** \`${result.procedure_name}\`
**File:** \`${path.basename(procedure_file)}\`

### Generated DAO
**Class:** \`${result.dao_class_name}\`
**Method:** \`${result.dao_method_name}\`

### Parameters
${result.parameters
  .map(
    (p) =>
      `- **${p.name}** (${p.direction}): \`${p.type}\``
  )
  .join("\n")}

---

### Generated Code

\`\`\`csharp
${result.generated_code}
\`\`\`

`;

  if (output_dir) {
    const outputPath = path.join(output_dir, `${result.dao_class_name}.cs`);
    const fullClass = generateFullDaoClass(result);
    
    if (fs.existsSync(outputPath)) {
      message += `\n⚠️ **Warning:** File \`${outputPath}\` already exists. Code not written to avoid overwrite.\n`;
    } else {
      fs.writeFileSync(outputPath, fullClass, "utf-8");
      message += `\n✅ **Success:** DAO written to \`${outputPath}\`\n`;
    }
  } else {
    message += `\n💡 **Tip:** Provide \`output_dir\` parameter to write DAO file automatically.\n`;
  }

  return {
    content: [{ type: "text", text: message }],
  };
}

function parseProcedureAndGenerateDao(
  content: string,
  filePath: string
): DaoGenerationResult {
  // Extract procedure name - handle DEFINER clause
  // Match: CREATE [DEFINER=...] PROCEDURE `procedure_name` or procedure_name
  const procMatch = content.match(/CREATE\s+(?:DEFINER=`[^`]+`@`[^`]+`\s+)?PROCEDURE\s+`?([a-zA-Z0-9_]+)`?/i);
  const procedureName = procMatch
    ? procMatch[1]
    : path.basename(filePath, ".sql");

  // Extract parameters
  const paramRegex =
    /(IN|OUT|INOUT)\s+p_(\w+)\s+(INT|VARCHAR|DECIMAL|DATE|DATETIME|TEXT|BOOLEAN|BIGINT)(\(\d+\))?/gi;
  const parameters: Array<{ name: string; type: string; direction: string }> =
    [];

  let match;
  while ((match = paramRegex.exec(content)) !== null) {
    parameters.push({
      name: match[2], // Without p_ prefix
      type: mapMySqlTypeToCSharp(match[3]),
      direction: match[1].toUpperCase(),
    });
  }

  // Generate DAO class and method names
  const parts = procedureName.split("_");
  const domain = parts[0]; // e.g., "inv", "usr", "sys"
  const entity = parts[1]; // e.g., "inventory", "user"

  const daoClassName = `Dao_${capitalizeFirst(entity || domain)}`;
  const methodName = parts.slice(2).map(capitalizeFirst).join("") + "Async";

  const generatedCode = generateDaoMethod(
    procedureName,
    methodName,
    parameters
  );

  return {
    procedure_name: procedureName,
    dao_class_name: daoClassName,
    dao_method_name: methodName,
    parameters,
    generated_code: generatedCode,
  };
}

function mapMySqlTypeToCSharp(mysqlType: string): string {
  const typeMap: { [key: string]: string } = {
    INT: "int",
    BIGINT: "long",
    VARCHAR: "string",
    TEXT: "string",
    DECIMAL: "decimal",
    DATE: "DateTime",
    DATETIME: "DateTime",
    BOOLEAN: "bool",
  };

  return typeMap[mysqlType.toUpperCase()] || "object";
}

function capitalizeFirst(str: string): string {
  return str.charAt(0).toUpperCase() + str.slice(1).toLowerCase();
}

function generateDaoMethod(
  procedureName: string,
  methodName: string,
  parameters: Array<{ name: string; type: string; direction: string }>
): string {
  // Filter IN parameters for method signature
  const inParams = parameters.filter((p) => p.direction === "IN");

  // Generate method signature
  const paramList = inParams
    .map((p) => `${p.type} ${p.name}`)
    .join(", ");

  // Generate parameter dictionary
  const paramDict = inParams
    .map((p) => `    ["${p.name}"] = ${p.name}`)
    .join(",\n");

  return `/// <summary>
/// Executes ${procedureName} stored procedure.
/// </summary>
${inParams.map((p) => `/// <param name="${p.name}">The ${p.name} parameter.</param>`).join("\n")}
/// <returns>A Model_Dao_Result containing status, error message, and optional payload.</returns>
public static async Task<Model_Dao_Result> ${methodName}(${paramList})
{
    var parameters = new Dictionary<string, object>
    {
${paramDict}
    };

    var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatus(
        Helper_Database_Variables.GetConnectionString(),
        "${procedureName}",
        parameters,
        useAsync: true);

    if (result.IsSuccess)
    {
        // TODO: Map DataTable to appropriate model
        return new Model_Dao_Result
        {
            Success = true,
            Data = result.Payload,
            Message = result.StatusMessage
        };
    }
    else
    {
        LoggingUtility.LogApplicationError(result.Exception, result.StatusMessage);
        return new Model_Dao_Result
        {
            Success = false,
            Message = result.StatusMessage,
            Exception = result.Exception
        };
    }
}`;
}

function generateFullDaoClass(result: DaoGenerationResult): string {
  return `using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Data;

/// <summary>
/// Data access object for ${result.dao_class_name.replace("Dao_", "")} operations.
/// Auto-generated from ${result.procedure_name} stored procedure.
/// </summary>
public static class ${result.dao_class_name}
{
    #region Fields
    #endregion

    #region Properties
    #endregion

    #region Database Operations

    ${result.generated_code}

    #endregion

    #region Helpers
    #endregion
}
`;
}
