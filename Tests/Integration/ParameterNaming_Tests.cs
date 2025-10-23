using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MTM_Inventory_Application.Tests.Integration;

/// <summary>
/// Integration tests validating stored procedure parameter naming matches C# model conventions.
/// Ensures database schema aligns with application code for consistent data mapping.
/// </summary>
[TestClass]
public class ParameterNaming_Tests : BaseIntegrationTest
{
    #region Test Methods

    [TestMethod]
    [TestCategory("Integration")]
    [TestCategory("ParameterNaming")]
    [Description("Verify stored procedure parameters use PascalCase naming after prefix removal")]
    public async Task StoredProcedureParameters_Should_UsePascalCaseAfterPrefixRemoval()
    {
        // Arrange
        var connectionString = GetTestConnectionString();
        var invalidCaseParameters = new List<string>();

        // Act
        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            var query = @"
                SELECT 
                    SPECIFIC_NAME as ProcedureName,
                    PARAMETER_NAME as ParameterName
                FROM INFORMATION_SCHEMA.PARAMETERS
                WHERE SPECIFIC_SCHEMA = DATABASE()
                    AND PARAMETER_NAME IS NOT NULL
                    AND PARAMETER_NAME != ''
                    AND PARAMETER_NAME NOT IN ('p_Status', 'p_ErrorMsg')
                ORDER BY SPECIFIC_NAME, ORDINAL_POSITION";

            using (var cmd = new MySqlCommand(query, connection))
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var procName = reader.GetString("ProcedureName");
                    var paramName = reader.GetString("ParameterName");

                    // Remove standard prefixes
                    var nameWithoutPrefix = paramName;
                    if (paramName.StartsWith("p_"))
                        nameWithoutPrefix = paramName.Substring(2);
                    else if (paramName.StartsWith("in_"))
                        nameWithoutPrefix = paramName.Substring(3);
                    else if (paramName.StartsWith("o_"))
                        nameWithoutPrefix = paramName.Substring(2);

                    // Verify first character is uppercase (PascalCase)
                    if (nameWithoutPrefix.Length > 0 && !char.IsUpper(nameWithoutPrefix[0]))
                    {
                        invalidCaseParameters.Add($"{procName}.{paramName}");
                    }
                }
            }
        }

        // Assert
        Assert.AreEqual(0, invalidCaseParameters.Count,
            $"Found {invalidCaseParameters.Count} parameters not using PascalCase after prefix: {string.Join(", ", invalidCaseParameters)}");
    }

    [TestMethod]
    [TestCategory("Integration")]
    [TestCategory("ParameterNaming")]
    [Description("Verify common parameter names match expected C# model properties")]
    public async Task CommonParameters_Should_MatchModelPropertyNames()
    {
        // Arrange
        var connectionString = GetTestConnectionString();
        var expectedMappings = new Dictionary<string, string>
        {
            { "p_UserID", "UserID" },
            { "p_PartID", "PartID" },
            { "p_Location", "Location" },
            { "p_Operation", "Operation" },
            { "p_Quantity", "Quantity" },
            { "p_PartNumber", "PartNumber" },
            { "p_LocationCode", "LocationCode" },
            { "p_OperationCode", "OperationCode" },
            { "in_InventoryID", "InventoryID" },
            { "in_TransactionID", "TransactionID" }
        };

        var foundParameters = new Dictionary<string, List<string>>();

        // Act
        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            foreach (var expected in expectedMappings)
            {
                var query = @"
                    SELECT DISTINCT SPECIFIC_NAME as ProcedureName
                    FROM INFORMATION_SCHEMA.PARAMETERS
                    WHERE SPECIFIC_SCHEMA = DATABASE()
                        AND PARAMETER_NAME = @ParamName";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@ParamName", expected.Key);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var procs = new List<string>();
                        while (await reader.ReadAsync())
                        {
                            procs.Add(reader.GetString("ProcedureName"));
                        }
                        if (procs.Count > 0)
                        {
                            foundParameters[expected.Key] = procs;
                        }
                    }
                }
            }
        }

        // Assert
        Console.WriteLine("\n=== Common Parameter Usage ===");
        foreach (var mapping in expectedMappings)
        {
            if (foundParameters.ContainsKey(mapping.Key))
            {
                Console.WriteLine($"✓ {mapping.Key} → {mapping.Value} (used in {foundParameters[mapping.Key].Count} procedures)");
            }
            else
            {
                Console.WriteLine($"  {mapping.Key} → {mapping.Value} (not found in database)");
            }
        }

        Assert.IsTrue(foundParameters.Count > 0, "Should find at least one common parameter in use");
    }

    [TestMethod]
    [TestCategory("Integration")]
    [TestCategory("ParameterNaming")]
    [Description("Verify no parameters contain underscores after prefix removal")]
    public async Task ParameterNames_Should_NotContainUnderscoresAfterPrefix()
    {
        // Arrange
        var connectionString = GetTestConnectionString();
        var parametersWithUnderscores = new List<string>();

        // Act
        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            var query = @"
                SELECT 
                    SPECIFIC_NAME as ProcedureName,
                    PARAMETER_NAME as ParameterName
                FROM INFORMATION_SCHEMA.PARAMETERS
                WHERE SPECIFIC_SCHEMA = DATABASE()
                    AND PARAMETER_NAME IS NOT NULL
                    AND PARAMETER_NAME != ''
                    AND PARAMETER_NAME NOT IN ('p_Status', 'p_ErrorMsg')
                ORDER BY SPECIFIC_NAME, ORDINAL_POSITION";

            using (var cmd = new MySqlCommand(query, connection))
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var procName = reader.GetString("ProcedureName");
                    var paramName = reader.GetString("ParameterName");

                    // Remove standard prefixes
                    var nameWithoutPrefix = paramName;
                    if (paramName.StartsWith("p_"))
                        nameWithoutPrefix = paramName.Substring(2);
                    else if (paramName.StartsWith("in_"))
                        nameWithoutPrefix = paramName.Substring(3);
                    else if (paramName.StartsWith("o_"))
                        nameWithoutPrefix = paramName.Substring(2);

                    // Check for underscores (indicates snake_case instead of PascalCase)
                    if (nameWithoutPrefix.Contains("_"))
                    {
                        parametersWithUnderscores.Add($"{procName}.{paramName}");
                    }
                }
            }
        }

        // Assert
        Assert.AreEqual(0, parametersWithUnderscores.Count,
            $"Found {parametersWithUnderscores.Count} parameters with underscores after prefix (should use PascalCase): {string.Join(", ", parametersWithUnderscores)}");
    }

    [TestMethod]
    [TestCategory("Integration")]
    [TestCategory("ParameterNaming")]
    [Description("List all unique parameter names for documentation")]
    public async Task ListAllUniqueParameterNames_ForDocumentation()
    {
        // Arrange
        var connectionString = GetTestConnectionString();
        var uniqueParameters = new HashSet<string>();

        // Act
        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            var query = @"
                SELECT DISTINCT PARAMETER_NAME as ParameterName
                FROM INFORMATION_SCHEMA.PARAMETERS
                WHERE SPECIFIC_SCHEMA = DATABASE()
                    AND PARAMETER_NAME IS NOT NULL
                    AND PARAMETER_NAME != ''
                ORDER BY PARAMETER_NAME";

            using (var cmd = new MySqlCommand(query, connection))
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    uniqueParameters.Add(reader.GetString("ParameterName"));
                }
            }
        }

        // Assert
        Assert.IsTrue(uniqueParameters.Count > 0, "Should find parameter names");

        Console.WriteLine($"\n=== Unique Parameter Names ({uniqueParameters.Count} total) ===");
        foreach (var param in uniqueParameters.OrderBy(p => p))
        {
            // Remove prefix to show C# equivalent
            var csharpName = param;
            if (param.StartsWith("p_"))
                csharpName = param.Substring(2);
            else if (param.StartsWith("in_"))
                csharpName = param.Substring(3);
            else if (param.StartsWith("o_"))
                csharpName = param.Substring(2);

            Console.WriteLine($"  {param,-30} → {csharpName}");
        }
    }

    [TestMethod]
    [TestCategory("Integration")]
    [TestCategory("ParameterNaming")]
    [Description("Verify parameter data types are appropriate for C# mapping")]
    public async Task ParameterDataTypes_Should_MapToValidCSharpTypes()
    {
        // Arrange
        var connectionString = GetTestConnectionString();
        var validTypes = new HashSet<string>
        {
            "int", "tinyint", "smallint", "mediumint", "bigint",
            "varchar", "char", "text", "mediumtext", "longtext",
            "decimal", "double", "float",
            "datetime", "date", "time", "timestamp",
            "bit", "boolean", "bool",
            "enum", "json" // enum maps to string in C#, json maps to string in C#
        };
        var invalidTypeParameters = new List<string>();

        // Act
        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            var query = @"
                SELECT 
                    SPECIFIC_NAME as ProcedureName,
                    PARAMETER_NAME as ParameterName,
                    DATA_TYPE as DataType
                FROM INFORMATION_SCHEMA.PARAMETERS
                WHERE SPECIFIC_SCHEMA = DATABASE()
                    AND PARAMETER_NAME IS NOT NULL
                    AND PARAMETER_NAME != ''
                ORDER BY SPECIFIC_NAME, ORDINAL_POSITION";

            using (var cmd = new MySqlCommand(query, connection))
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var procName = reader.GetString("ProcedureName");
                    var paramName = reader.GetString("ParameterName");
                    var dataType = reader.GetString("DataType").ToLower();

                    if (!validTypes.Contains(dataType))
                    {
                        invalidTypeParameters.Add($"{procName}.{paramName} ({dataType})");
                    }
                }
            }
        }

        // Assert
        Assert.AreEqual(0, invalidTypeParameters.Count,
            $"Found {invalidTypeParameters.Count} parameters with unusual data types: {string.Join(", ", invalidTypeParameters)}");
    }

    #endregion
}
