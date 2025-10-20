using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MTM_Inventory_Application.Tests.Integration;

/// <summary>
/// Integration tests validating stored procedure schema consistency.
/// Verifies all stored procedures follow uniform parameter naming and output conventions.
/// </summary>
[TestClass]
public class StoredProcedureValidation_Tests : BaseIntegrationTest
{
    #region Test Methods

    [TestMethod]
    [TestCategory("Integration")]
    [TestCategory("StoredProcedure")]
    [Description("Verify all stored procedures have OUT p_Status INT parameter")]
    public async Task AllStoredProcedures_Should_HaveStatusParameter()
    {
        // Arrange
        var connectionString = GetTestConnectionString();
        var proceduresWithoutStatus = new List<string>();

        // Act
        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            // Query all stored procedures
            var query = @"
                SELECT DISTINCT 
                    ROUTINE_NAME
                FROM INFORMATION_SCHEMA.ROUTINES
                WHERE ROUTINE_SCHEMA = DATABASE()
                    AND ROUTINE_TYPE = 'PROCEDURE'
                ORDER BY ROUTINE_NAME";

            using (var cmd = new MySqlCommand(query, connection))
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                var allProcedures = new List<string>();
                while (await reader.ReadAsync())
                {
                    allProcedures.Add(reader.GetString("ROUTINE_NAME"));
                }
                reader.Close();

                // Check each procedure for p_Status parameter
                foreach (var procName in allProcedures)
                {
                    var paramQuery = @"
                        SELECT COUNT(*) as ParamCount
                        FROM INFORMATION_SCHEMA.PARAMETERS
                        WHERE SPECIFIC_SCHEMA = DATABASE()
                            AND SPECIFIC_NAME = @ProcName
                            AND PARAMETER_NAME = 'p_Status'
                            AND PARAMETER_MODE = 'OUT'
                            AND DATA_TYPE = 'int'";

                    using (var paramCmd = new MySqlCommand(paramQuery, connection))
                    {
                        paramCmd.Parameters.AddWithValue("@ProcName", procName);
                        var count = Convert.ToInt32(await paramCmd.ExecuteScalarAsync());

                        if (count == 0)
                        {
                            proceduresWithoutStatus.Add(procName);
                        }
                    }
                }
            }
        }

        // Assert
        Assert.AreEqual(0, proceduresWithoutStatus.Count,
            $"Found {proceduresWithoutStatus.Count} stored procedures without OUT p_Status INT parameter: {string.Join(", ", proceduresWithoutStatus)}");
    }

    [TestMethod]
    [TestCategory("Integration")]
    [TestCategory("StoredProcedure")]
    [Description("Verify all stored procedures have OUT p_ErrorMsg VARCHAR parameter")]
    public async Task AllStoredProcedures_Should_HaveErrorMsgParameter()
    {
        // Arrange
        var connectionString = GetTestConnectionString();
        var proceduresWithoutErrorMsg = new List<string>();

        // Act
        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            // Query all stored procedures
            var query = @"
                SELECT DISTINCT 
                    ROUTINE_NAME
                FROM INFORMATION_SCHEMA.ROUTINES
                WHERE ROUTINE_SCHEMA = DATABASE()
                    AND ROUTINE_TYPE = 'PROCEDURE'
                ORDER BY ROUTINE_NAME";

            using (var cmd = new MySqlCommand(query, connection))
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                var allProcedures = new List<string>();
                while (await reader.ReadAsync())
                {
                    allProcedures.Add(reader.GetString("ROUTINE_NAME"));
                }
                reader.Close();

                // Check each procedure for p_ErrorMsg parameter
                foreach (var procName in allProcedures)
                {
                    var paramQuery = @"
                        SELECT COUNT(*) as ParamCount
                        FROM INFORMATION_SCHEMA.PARAMETERS
                        WHERE SPECIFIC_SCHEMA = DATABASE()
                            AND SPECIFIC_NAME = @ProcName
                            AND PARAMETER_NAME = 'p_ErrorMsg'
                            AND PARAMETER_MODE = 'OUT'
                            AND DATA_TYPE IN ('varchar', 'text')";

                    using (var paramCmd = new MySqlCommand(paramQuery, connection))
                    {
                        paramCmd.Parameters.AddWithValue("@ProcName", procName);
                        var count = Convert.ToInt32(await paramCmd.ExecuteScalarAsync());

                        if (count == 0)
                        {
                            proceduresWithoutErrorMsg.Add(procName);
                        }
                    }
                }
            }
        }

        // Assert
        Assert.AreEqual(0, proceduresWithoutErrorMsg.Count,
            $"Found {proceduresWithoutErrorMsg.Count} stored procedures without OUT p_ErrorMsg VARCHAR parameter: {string.Join(", ", proceduresWithoutErrorMsg)}");
    }

    [TestMethod]
    [TestCategory("Integration")]
    [TestCategory("StoredProcedure")]
    [Description("Verify stored procedures use standard parameter prefixes (p_, in_, o_)")]
    public async Task StoredProcedures_Should_UseStandardParameterPrefixes()
    {
        // Arrange
        var connectionString = GetTestConnectionString();
        var parametersWithInvalidPrefixes = new List<string>();

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
                ORDER BY SPECIFIC_NAME, ORDINAL_POSITION";

            using (var cmd = new MySqlCommand(query, connection))
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var procName = reader.GetString("ProcedureName");
                    var paramName = reader.GetString("ParameterName");

                    // Check if parameter uses standard prefix
                    if (!paramName.StartsWith("p_") && 
                        !paramName.StartsWith("in_") && 
                        !paramName.StartsWith("o_"))
                    {
                        parametersWithInvalidPrefixes.Add($"{procName}.{paramName}");
                    }
                }
            }
        }

        // Assert
        Assert.AreEqual(0, parametersWithInvalidPrefixes.Count,
            $"Found {parametersWithInvalidPrefixes.Count} parameters without standard prefix (p_, in_, o_): {string.Join(", ", parametersWithInvalidPrefixes)}");
    }

    [TestMethod]
    [TestCategory("Integration")]
    [TestCategory("StoredProcedure")]
    [Description("Get count of all stored procedures in test database")]
    public async Task TestDatabase_Should_HaveStoredProcedures()
    {
        // Arrange
        var connectionString = GetTestConnectionString();
        int procedureCount = 0;

        // Act
        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            var query = @"
                SELECT COUNT(*) as ProcCount
                FROM INFORMATION_SCHEMA.ROUTINES
                WHERE ROUTINE_SCHEMA = DATABASE()
                    AND ROUTINE_TYPE = 'PROCEDURE'";

            using (var cmd = new MySqlCommand(query, connection))
            {
                procedureCount = Convert.ToInt32(await cmd.ExecuteScalarAsync());
            }
        }

        // Assert
        Assert.IsTrue(procedureCount > 0, $"Test database should contain stored procedures, found {procedureCount}");
        Console.WriteLine($"Test database contains {procedureCount} stored procedures");
    }

    [TestMethod]
    [TestCategory("Integration")]
    [TestCategory("StoredProcedure")]
    [Description("List all stored procedures and their parameter counts for validation")]
    public async Task ListAllStoredProcedures_WithParameterCounts()
    {
        // Arrange
        var connectionString = GetTestConnectionString();
        var procedures = new Dictionary<string, int>();

        // Act
        using (var connection = new MySqlConnection(connectionString))
        {
            await connection.OpenAsync();

            var query = @"
                SELECT 
                    r.ROUTINE_NAME,
                    COUNT(p.PARAMETER_NAME) as ParamCount
                FROM INFORMATION_SCHEMA.ROUTINES r
                LEFT JOIN INFORMATION_SCHEMA.PARAMETERS p 
                    ON r.ROUTINE_SCHEMA = p.SPECIFIC_SCHEMA 
                    AND r.ROUTINE_NAME = p.SPECIFIC_NAME
                WHERE r.ROUTINE_SCHEMA = DATABASE()
                    AND r.ROUTINE_TYPE = 'PROCEDURE'
                GROUP BY r.ROUTINE_NAME
                ORDER BY r.ROUTINE_NAME";

            using (var cmd = new MySqlCommand(query, connection))
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var procName = reader.GetString("ROUTINE_NAME");
                    var paramCount = reader.GetInt32("ParamCount");
                    procedures[procName] = paramCount;
                }
            }
        }

        // Assert
        Assert.IsTrue(procedures.Count > 0, "Should find stored procedures");
        
        Console.WriteLine($"\n=== Stored Procedures ({procedures.Count} total) ===");
        foreach (var proc in procedures.OrderBy(p => p.Key))
        {
            Console.WriteLine($"  {proc.Key}: {proc.Value} parameters");
        }
    }

    #endregion
}
