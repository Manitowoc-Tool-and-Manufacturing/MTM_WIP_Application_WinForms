import * as fs from "fs";
import * as path from "path";

interface TestScaffold {
  file: string;
  class_name: string;
  test_class_name: string;
  test_methods: TestMethod[];
  generated_code: string;
}

interface TestMethod {
  name: string;
  method_under_test: string;
  test_type: "happy_path" | "error_case" | "edge_case";
  description: string;
}

export async function generateUnitTests(args: {
  source_file: string;
  output_dir?: string;
  test_framework?: "xunit" | "nunit" | "mstest";
}): Promise<{ content: Array<{ type: string; text: string }> }> {
  const { source_file, output_dir, test_framework = "xunit" } = args;

  if (!fs.existsSync(source_file)) {
    throw new Error(`Source file not found: ${source_file}`);
  }

  if (!source_file.endsWith(".cs")) {
    throw new Error("Only C# files are currently supported");
  }

  const scaffold = generateTestScaffold(source_file, test_framework);

  let message = `
## Unit Test Scaffolding Generated

### Source File
**File:** \`${path.basename(source_file)}\`
**Class:** \`${scaffold.class_name}\`
**Test Framework:** ${test_framework}

### Generated Test Class
**Test Class:** \`${scaffold.test_class_name}\`
**Test Methods:** ${scaffold.test_methods.length}

---

### Test Methods

${scaffold.test_methods
  .map(
    (tm) =>
      `- **${tm.name}** (${tm.test_type})\n  Tests: ${tm.method_under_test}\n  ${tm.description}`
  )
  .join("\n\n")}

---

### Generated Code

\`\`\`csharp
${scaffold.generated_code}
\`\`\`

`;

  if (output_dir) {
    const testFileName = scaffold.test_class_name + ".cs";
    const outputPath = path.join(output_dir, testFileName);

    if (fs.existsSync(outputPath)) {
      message += `\n⚠️ **Warning:** File \`${outputPath}\` already exists. Code not written to avoid overwrite.\n`;
    } else {
      fs.mkdirSync(output_dir, { recursive: true });
      fs.writeFileSync(outputPath, scaffold.generated_code, "utf-8");
      message += `\n✅ **Success:** Test file written to \`${outputPath}\`\n`;
    }
  } else {
    message += `\n💡 **Tip:** Provide \`output_dir\` parameter to write test file automatically.\n`;
  }

  return {
    content: [{ type: "text", text: message }],
  };
}

function generateTestScaffold(
  filePath: string,
  framework: "xunit" | "nunit" | "mstest"
): TestScaffold {
  const content = fs.readFileSync(filePath, "utf-8");
  const lines = content.split("\n");

  // Extract class name
  const classMatch = content.match(/(?:public|internal)\s+(?:static\s+)?class\s+(\w+)/i);
  const className = classMatch ? classMatch[1] : "UnknownClass";

  // Extract public methods
  const methods: Array<{ name: string; isAsync: boolean; returnType: string }> = [];
  const methodRegex =
    /(?:public|internal)\s+(static\s+)?(async\s+)?([\w<>]+)\s+(\w+)\s*\([^)]*\)/gi;
  let match;

  while ((match = methodRegex.exec(content)) !== null) {
    const isAsync = !!match[2];
    const returnType = match[3];
    const methodName = match[4];

    if (
      !methodName.startsWith("_") &&
      methodName !== className &&
      !methodName.includes("Dispose")
    ) {
      methods.push({ name: methodName, isAsync, returnType });
    }
  }

  // Generate test methods
  const testMethods: TestMethod[] = [];

  for (const method of methods) {
    // Happy path test
    testMethods.push({
      name: `${method.name}_ValidInput_ReturnsSuccess`,
      method_under_test: method.name,
      test_type: "happy_path",
      description: `Verifies ${method.name} succeeds with valid input`,
    });

    // Error case test
    testMethods.push({
      name: `${method.name}_InvalidInput_ReturnsError`,
      method_under_test: method.name,
      test_type: "error_case",
      description: `Verifies ${method.name} handles invalid input correctly`,
    });

    // Edge case test
    if (method.name.includes("Get") || method.name.includes("Fetch")) {
      testMethods.push({
        name: `${method.name}_NoDataFound_ReturnsEmpty`,
        method_under_test: method.name,
        test_type: "edge_case",
        description: `Verifies ${method.name} handles empty result sets`,
      });
    }
  }

  const generatedCode = generateTestCode(className, testMethods, framework);

  return {
    file: path.basename(filePath),
    class_name: className,
    test_class_name: `${className}Tests`,
    test_methods: testMethods,
    generated_code: generatedCode,
  };
}

function generateTestCode(
  className: string,
  testMethods: TestMethod[],
  framework: "xunit" | "nunit" | "mstest"
): string {
  const testAttribute = getTestAttribute(framework);
  const factAttribute = getFactAttribute(framework);
  const setupAttribute = getSetupAttribute(framework);

  let code = `using System;
using System.Threading.Tasks;
${getFrameworkUsings(framework)}
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Tests;

/// <summary>
/// Unit tests for ${className} class.
/// Auto-generated test scaffolding - implement test logic as needed.
/// </summary>
${testAttribute}
public class ${className}Tests
{
    #region Setup

    ${setupAttribute}
    public void Setup()
    {
        // Initialize test dependencies
        // TODO: Setup mocks, test data, etc.
    }

    #endregion

    #region Tests

`;

  for (const testMethod of testMethods) {
    const isAsync = testMethod.method_under_test.includes("Async");
    const asyncKeyword = isAsync ? "async " : "";
    const taskReturn = isAsync ? "Task" : "void";

    code += `    ${factAttribute}
    public ${asyncKeyword}${taskReturn} ${testMethod.name}()
    {
        // Arrange
        // TODO: Setup test data and mocks

        // Act
        // TODO: Call ${testMethod.method_under_test}

        // Assert
        // TODO: Verify expected behavior
        ${getAssertSyntax(framework, "NotNull", "result")}
    }

`;
  }

  code += `    #endregion
}
`;

  return code;
}

function getFrameworkUsings(framework: "xunit" | "nunit" | "mstest"): string {
  switch (framework) {
    case "xunit":
      return "using Xunit;";
    case "nunit":
      return "using NUnit.Framework;";
    case "mstest":
      return "using Microsoft.VisualStudio.TestTools.UnitTesting;";
  }
}

function getTestAttribute(framework: "xunit" | "nunit" | "mstest"): string {
  switch (framework) {
    case "xunit":
      return "";
    case "nunit":
      return "[TestFixture]";
    case "mstest":
      return "[TestClass]";
  }
}

function getFactAttribute(framework: "xunit" | "nunit" | "mstest"): string {
  switch (framework) {
    case "xunit":
      return "[Fact]";
    case "nunit":
      return "[Test]";
    case "mstest":
      return "[TestMethod]";
  }
}

function getSetupAttribute(framework: "xunit" | "nunit" | "mstest"): string {
  switch (framework) {
    case "xunit":
      return "public";
    case "nunit":
      return "[SetUp]";
    case "mstest":
      return "[TestInitialize]";
  }
}

function getAssertSyntax(
  framework: "xunit" | "nunit" | "mstest",
  assertType: string,
  value: string
): string {
  switch (framework) {
    case "xunit":
      return `Assert.${assertType}(${value});`;
    case "nunit":
      return `Assert.That(${value}, Is.${assertType});`;
    case "mstest":
      return `Assert.Is${assertType}(${value});`;
  }
}
