using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Models;
using MySql.Data.MySqlClient;

namespace MTM_WIP_Application_Winforms.Services.Maintenance
{
    public static class Service_Migration
    {
        private const string MysqlDumpPath = @"C:\MAMP\bin\mysql\bin\mysqldump.exe";

        #region Migration Methods

        public static async Task<Model_Dao_Result<bool>> RunMigrationScriptAsync(string scriptName, string sqlContent)
        {
            try
            {
                LoggingUtility.Log($"[Migration] Starting script: {scriptName}");

                var connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);
                using var connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();

                using var command = new MySqlCommand(sqlContent, connection);
                command.CommandTimeout = 300; // 5 minutes for large migrations
                await command.ExecuteNonQueryAsync();

                LoggingUtility.Log($"[Migration] Completed script: {scriptName}");
                return Model_Dao_Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return Model_Dao_Result<bool>.Failure($"Migration failed for {scriptName}: {ex.Message}");
            }
        }

        public static async Task<Model_Dao_Result<bool>> MigrateSingleTableAsync(string tableName)
        {
            try
            {
                string sql = GetMigrationSqlForTable(tableName);
                if (string.IsNullOrEmpty(sql))
                    return Model_Dao_Result<bool>.Failure($"No migration logic defined for table: {tableName}");

                return await RunMigrationScriptAsync($"Single_Table_{tableName}", sql);
            }
            catch (Exception ex)
            {
                return Model_Dao_Result<bool>.Failure(ex);
            }
        }

        private static string GetMigrationSqlForTable(string tableName)
        {
            // Dynamic SQL generation based on table name
            // This mimics the logic in the bulk scripts but for single tables
            return tableName switch
            {
                "md_part_ids" => @"
                    SET FOREIGN_KEY_CHECKS = 0;
                    TRUNCATE TABLE mtm_wip_application_winforms.md_part_ids;
                    INSERT INTO mtm_wip_application_winforms.md_part_ids (PartID, Customer, Description, IssuedBy, ItemType, Operations)
                    SELECT PartID, Customer, Description, IssuedBy, ItemType, Operations FROM mtm_wip_application.md_part_ids;
                    SET FOREIGN_KEY_CHECKS = 1;",

                "md_locations" => @"
                    SET FOREIGN_KEY_CHECKS = 0;
                    TRUNCATE TABLE mtm_wip_application_winforms.md_locations;
                    INSERT INTO mtm_wip_application_winforms.md_locations (Location, Building, IssuedBy)
                    SELECT Location, Building, IssuedBy FROM mtm_wip_application.md_locations;
                    SET FOREIGN_KEY_CHECKS = 1;",

                "md_operation_numbers" => @"
                    SET FOREIGN_KEY_CHECKS = 0;
                    TRUNCATE TABLE mtm_wip_application_winforms.md_operation_numbers;
                    INSERT INTO mtm_wip_application_winforms.md_operation_numbers (Operation, IssuedBy)
                    SELECT Operation, IssuedBy FROM mtm_wip_application.md_operation_numbers;
                    SET FOREIGN_KEY_CHECKS = 1;",

                "md_item_types" => @"
                    SET FOREIGN_KEY_CHECKS = 0;
                    TRUNCATE TABLE mtm_wip_application_winforms.md_item_types;
                    INSERT INTO mtm_wip_application_winforms.md_item_types (ItemType, IssuedBy)
                    SELECT ItemType, IssuedBy FROM mtm_wip_application.md_item_types;
                    SET FOREIGN_KEY_CHECKS = 1;",

                "usr_users" => @"
                    SET FOREIGN_KEY_CHECKS = 0;
                    INSERT IGNORE INTO mtm_wip_application_winforms.usr_users (User, `Full Name`, Shift, VitsUser, Pin, LastShownVersion, HideChangeLog)
                    SELECT User, `Full Name`, Shift, VitsUser, Pin, LastShownVersion, HideChangeLog FROM mtm_wip_application.usr_users;
                    SET FOREIGN_KEY_CHECKS = 1;",

                "inv_inventory" => @"
                    SET FOREIGN_KEY_CHECKS = 0;
                    TRUNCATE TABLE mtm_wip_application_winforms.inv_inventory;
                    INSERT INTO mtm_wip_application_winforms.inv_inventory (PartID, Location, Operation, Quantity, ItemType, ReceiveDate, LastUpdated, User, BatchNumber, Notes, ColorCode, WorkOrder)
                    SELECT PartID, Location, Operation, Quantity, ItemType, ReceiveDate, LastUpdated, User, BatchNumber, Notes, 'UNKNOWN', 'UNKNOWN'
                    FROM mtm_wip_application.inv_inventory;
                    SET FOREIGN_KEY_CHECKS = 1;",

                "inv_transaction" => @"
                    SET FOREIGN_KEY_CHECKS = 0;
                    TRUNCATE TABLE mtm_wip_application_winforms.inv_transaction;
                    INSERT INTO mtm_wip_application_winforms.inv_transaction (TransactionType, BatchNumber, PartID, FromLocation, ToLocation, Operation, Quantity, Notes, User, ItemType, ReceiveDate, ColorCode, WorkOrder)
                    SELECT TransactionType, BatchNumber, PartID, FromLocation, ToLocation, Operation, Quantity, Notes, User, ItemType, ReceiveDate, 'Unknown', 'Unknown'
                    FROM mtm_wip_application.inv_transaction;
                    SET FOREIGN_KEY_CHECKS = 1;",

                _ => string.Empty
            };
        }

        #endregion

        #region Maintenance Methods

        public static async Task<Model_Dao_Result<int>> CleanTestDataAsync()
        {
            try
            {
                string sql = @"
                    DELETE FROM inv_inventory
                    WHERE User = 'JOHNK'
                       OR PartID LIKE '%test%'
                       OR Location LIKE '%test%'
                       OR Operation LIKE '%test%';

                    DELETE FROM inv_transaction
                    WHERE User = 'JOHNK'
                       OR PartID LIKE '%test%'
                       OR FromLocation LIKE '%test%'
                       OR ToLocation LIKE '%test%'
                       OR Operation LIKE '%test%';
                ";

                return await ExecuteNonQueryAndReturnRowsAsync(sql);
            }
            catch (Exception ex)
            {
                return Model_Dao_Result<int>.Failure(ex);
            }
        }

        public static async Task<Model_Dao_Result<bool>> OptimizeTablesAsync()
        {
            try
            {
                // Get all tables
                var tables = new[] {
                    "inv_inventory", "inv_transaction", "md_part_ids", "md_locations",
                    "md_operation_numbers", "log_error", "sys_last_10_transactions"
                };

                string sql = $"OPTIMIZE TABLE {string.Join(", ", tables)};";
                await RunMigrationScriptAsync("Optimize_Tables", sql);

                return Model_Dao_Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Model_Dao_Result<bool>.Failure(ex);
            }
        }

        public static async Task<Model_Dao_Result<bool>> TruncateLogsAsync()
        {
            try
            {
                string sql = "TRUNCATE TABLE log_error;";
                await RunMigrationScriptAsync("Truncate_Logs", sql);
                return Model_Dao_Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Model_Dao_Result<bool>.Failure(ex);
            }
        }

        public static async Task<Model_Dao_Result<Dictionary<string, long>>> GetTableRowCountsAsync()
        {
            try
            {
                var counts = new Dictionary<string, long>();
                var tables = new[] { "inv_inventory", "inv_transaction", "md_part_ids", "md_locations", "usr_users" };

                var connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);
                using var connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();

                foreach (var table in tables)
                {
                    using var cmd = new MySqlCommand($"SELECT COUNT(*) FROM {table}", connection);
                    var count = Convert.ToInt64(await cmd.ExecuteScalarAsync());
                    counts[table] = count;
                }

                return Model_Dao_Result<Dictionary<string, long>>.Success(counts);
            }
            catch (Exception ex)
            {
                return Model_Dao_Result<Dictionary<string, long>>.Failure(ex);
            }
        }

        public static async Task<Model_Dao_Result<List<Dictionary<string, object>>>> GetTableSizesAsync()
        {
            try
            {
                string sql = @"
                    SELECT
                        table_name AS `Table`,
                        round(((data_length + index_length) / 1024 / 1024), 2) AS `SizeMB`
                    FROM information_schema.TABLES
                    WHERE table_schema = 'mtm_wip_application_winforms'
                    ORDER BY (data_length + index_length) DESC;";

                var connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);
                using var connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();

                using var cmd = new MySqlCommand(sql, connection);
                using var reader = await cmd.ExecuteReaderAsync();

                var results = new List<Dictionary<string, object>>();
                while (await reader.ReadAsync())
                {
                    results.Add(new Dictionary<string, object>
                    {
                        { "Table", reader["Table"] },
                        { "SizeMB", reader["SizeMB"] }
                    });
                }

                return Model_Dao_Result<List<Dictionary<string, object>>>.Success(results);
            }
            catch (Exception ex)
            {
                return Model_Dao_Result<List<Dictionary<string, object>>>.Failure(ex);
            }
        }

        public static async Task<Model_Dao_Result<Dictionary<string, string>>> CheckTableIntegrityAsync()
        {
            try
            {
                var results = new Dictionary<string, string>();
                var tables = new[] { "inv_inventory", "inv_transaction", "md_part_ids", "md_locations", "usr_users" };

                var connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);
                using var connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();

                foreach (var table in tables)
                {
                    using var cmd = new MySqlCommand($"CHECK TABLE {table}", connection);
                    using var reader = await cmd.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        // Msg_text column usually contains OK or error
                        results[table] = reader["Msg_text"].ToString() ?? "Unknown";
                    }
                }

                return Model_Dao_Result<Dictionary<string, string>>.Success(results);
            }
            catch (Exception ex)
            {
                return Model_Dao_Result<Dictionary<string, string>>.Failure(ex);
            }
        }

        public static async Task<Model_Dao_Result<List<string>>> GetActiveConnectionsAsync()
        {
            try
            {
                var connections = new List<string>();
                string sql = "SHOW PROCESSLIST";

                var connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);
                using var connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();

                using var cmd = new MySqlCommand(sql, connection);
                using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    string user = reader["User"].ToString() ?? "Unknown";
                    string host = reader["Host"].ToString() ?? "Unknown";
                    string db = reader["db"].ToString() ?? "None";
                    string command = reader["Command"].ToString() ?? "Unknown";
                    string time = reader["Time"].ToString() ?? "0";
                    string state = reader["State"].ToString() ?? "";

                    connections.Add($"User: {user} | Host: {host} | DB: {db} | Cmd: {command} | Time: {time}s | State: {state}");
                }

                return Model_Dao_Result<List<string>>.Success(connections);
            }
            catch (Exception ex)
            {
                return Model_Dao_Result<List<string>>.Failure(ex);
            }
        }

        public static async Task<Model_Dao_Result<List<string>>> ValidateSchemaAsync()
        {
            try
            {
                var issues = new List<string>();
                var requiredTables = new[] {
                    "inv_inventory", "inv_transaction", "md_part_ids", "md_locations",
                    "md_operation_numbers", "md_item_types", "usr_users", "log_error"
                };

                var connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);
                using var connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();

                // Check tables
                foreach (var table in requiredTables)
                {
                    using var cmd = new MySqlCommand($"SHOW TABLES LIKE '{table}'", connection);
                    var result = await cmd.ExecuteScalarAsync();
                    if (result == null)
                    {
                        issues.Add($"Missing Table: {table}");
                    }
                }

                // Check critical columns
                var criticalColumns = new Dictionary<string, string[]>
                {
                    { "inv_inventory", new[] { "ColorCode", "WorkOrder" } },
                    { "inv_transaction", new[] { "ColorCode", "WorkOrder" } }
                };

                foreach (var kvp in criticalColumns)
                {
                    foreach (var col in kvp.Value)
                    {
                        using var cmd = new MySqlCommand($"SHOW COLUMNS FROM {kvp.Key} LIKE '{col}'", connection);
                        var result = await cmd.ExecuteScalarAsync();
                        if (result == null)
                        {
                            issues.Add($"Missing Column: {kvp.Key}.{col}");
                        }
                    }
                }

                return Model_Dao_Result<List<string>>.Success(issues);
            }
            catch (Exception ex)
            {
                return Model_Dao_Result<List<string>>.Failure(ex);
            }
        }

        public static async Task<Model_Dao_Result<string>> ArchiveAndClearLogsAsync(string destinationPath)
        {
            try
            {
                // 1. Export logs to CSV
                string fileName = $"error_logs_archive_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
                string fullPath = Path.Combine(destinationPath, fileName);

                string sql = "SELECT * FROM log_error INTO OUTFILE '" + fullPath.Replace("\\", "/") + "' FIELDS TERMINATED BY ',' ENCLOSED BY '\"' LINES TERMINATED BY '\\n';";

                // Note: SELECT INTO OUTFILE writes to the SERVER'S filesystem.
                // If client and server are different, this won't work as expected for client path.
                // For local dev (MAMP), it works if permissions allow.
                // Safer approach for client app: Read data, write file in C#.

                var connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);
                using var connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();

                using (var cmd = new MySqlCommand("SELECT * FROM log_error", connection))
                using (var reader = await cmd.ExecuteReaderAsync())
                using (var writer = new StreamWriter(fullPath))
                {
                    // Write header
                    await writer.WriteLineAsync("ErrorID,Timestamp,User,Message,StackTrace,Source");

                    while (await reader.ReadAsync())
                    {
                        var line = $"{reader["ErrorID"]},{reader["Timestamp"]},{reader["User"]},\"{reader["Message"]}\",\"{reader["StackTrace"]}\",{reader["Source"]}";
                        await writer.WriteLineAsync(line);
                    }
                }

                // 2. Truncate table
                await TruncateLogsAsync();

                return Model_Dao_Result<string>.Success(data: fullPath);
            }
            catch (Exception ex)
            {
                return Model_Dao_Result<string>.Failure(ex);
            }
        }

        public static async Task<Model_Dao_Result<bool>> FactoryResetAsync()
        {
            try
            {
                string sql = @"
                    SET FOREIGN_KEY_CHECKS = 0;
                    TRUNCATE TABLE inv_inventory;
                    TRUNCATE TABLE inv_transaction;
                    TRUNCATE TABLE sys_last_10_transactions;
                    SET FOREIGN_KEY_CHECKS = 1;
                ";

                await RunMigrationScriptAsync("Factory_Reset", sql);
                return Model_Dao_Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Model_Dao_Result<bool>.Failure(ex);
            }
        }

        private static async Task<Model_Dao_Result<int>> ExecuteNonQueryAndReturnRowsAsync(string sql)
        {
            try
            {
                var connectionString = Helper_Database_Variables.GetConnectionString(null, null, null, null);
                using var connection = new MySqlConnection(connectionString);
                await connection.OpenAsync();

                using var command = new MySqlCommand(sql, connection);
                int rows = await command.ExecuteNonQueryAsync();

                return Model_Dao_Result<int>.Success(rows, "Operation completed", rows);
            }
            catch (Exception ex)
            {
                return Model_Dao_Result<int>.Failure(ex);
            }
        }

        #endregion

        #region Backup Methods

        public static async Task<Model_Dao_Result<string>> BackupDatabaseAsync(string destinationPath)
        {
            try
            {
                if (!File.Exists(MysqlDumpPath))
                {
                    return Model_Dao_Result<string>.Failure("mysqldump.exe not found at " + MysqlDumpPath);
                }

                var dbName = "mtm_wip_application_winforms";
                var user = "root";
                var pass = "root"; // Should come from config ideally
                var host = "172.16.1.104";
                var port = "3306";

                var fileName = $"backup_{dbName}_{DateTime.Now:yyyyMMdd_HHmmss}.sql";
                var fullPath = Path.Combine(destinationPath, fileName);

                var arguments = $"-h {host} -P {port} -u {user} -p{pass} {dbName} -r \"{fullPath}\"";

                var psi = new ProcessStartInfo
                {
                    FileName = MysqlDumpPath,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using var process = Process.Start(psi);
                if (process == null) return Model_Dao_Result<string>.Failure("Failed to start mysqldump process");

                var error = await process.StandardError.ReadToEndAsync();
                await process.WaitForExitAsync();

                if (process.ExitCode != 0)
                {
                    return Model_Dao_Result<string>.Failure($"Backup failed: {error}");
                }

                return Model_Dao_Result<string>.Success(fullPath, "Backup created successfully");
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                return Model_Dao_Result<string>.Failure($"Backup exception: {ex.Message}");
            }
        }
    }
        # endregion
}
