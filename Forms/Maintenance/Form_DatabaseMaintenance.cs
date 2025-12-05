using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.Analytics;
using MTM_WIP_Application_Winforms.Services.Maintenance;
using Microsoft.Extensions.DependencyInjection;

namespace MTM_WIP_Application_Winforms.Forms.Maintenance
{
    public partial class Form_DatabaseMaintenance : ThemedForm
    {
        public Form_DatabaseMaintenance()
        {
            InitializeComponent();
            InitializeSingleTableList();
        }

        private void InitializeSingleTableList()
        {
            checkedListBoxTables.Items.Add("md_part_ids");
            checkedListBoxTables.Items.Add("md_locations");
            checkedListBoxTables.Items.Add("md_operation_numbers");
            checkedListBoxTables.Items.Add("md_item_types");
            checkedListBoxTables.Items.Add("usr_users");
            checkedListBoxTables.Items.Add("inv_inventory");
            checkedListBoxTables.Items.Add("inv_transaction");
        }

        private void Log(string message)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action(() => Log(message)));
                return;
            }

            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}{Environment.NewLine}");
            txtLog.ScrollToCaret();
        }

        private async void btnMigrateBulk_Click(object sender, EventArgs e)
        {
            if (Service_ErrorHandler.ShowConfirmation("This will overwrite data in the target database. Ensure you have a backup.\n\nContinue?", "Bulk Migration Warning") != DialogResult.Yes)
                return;

            try
            {
                progressBar.Value = 0;
                btnMigrateBulk.Enabled = false;
                Log("Starting Bulk Migration...");

                if (chkMasterData.Checked)
                {
                    Log("Migrating Master Data...");
                    var script = await File.ReadAllTextAsync(Path.Combine(Application.StartupPath, "Database", "Migration", "01_Migrate_MasterData.sql"));
                    var result = await Service_Migration.RunMigrationScriptAsync("01_Migrate_MasterData", script);
                    Log(result.IsSuccess ? "Master Data: Success" : $"Master Data: Failed - {result.ErrorMessage}");
                }
                progressBar.Value = 33;

                if (chkUsers.Checked)
                {
                    Log("Migrating Users...");
                    var script = await File.ReadAllTextAsync(Path.Combine(Application.StartupPath, "Database", "Migration", "02_Migrate_Users.sql"));
                    var result = await Service_Migration.RunMigrationScriptAsync("02_Migrate_Users", script);
                    Log(result.IsSuccess ? "Users: Success" : $"Users: Failed - {result.ErrorMessage}");
                }
                progressBar.Value = 66;

                if (chkInventory.Checked)
                {
                    Log("Migrating Inventory...");
                    var script = await File.ReadAllTextAsync(Path.Combine(Application.StartupPath, "Database", "Migration", "03_Migrate_Inventory.sql"));
                    var result = await Service_Migration.RunMigrationScriptAsync("03_Migrate_Inventory", script);
                    Log(result.IsSuccess ? "Inventory: Success" : $"Inventory: Failed - {result.ErrorMessage}");
                }
                progressBar.Value = 100;

                Log("Bulk Migration Completed.");
                Service_ErrorHandler.ShowInformation("Bulk Migration Completed.", "Success");
            }
            catch (Exception ex)
            {
                Log($"Error: {ex.Message}");
            }
            finally
            {
                btnMigrateBulk.Enabled = true;
            }
        }

        private async void btnMigrateSelected_Click(object sender, EventArgs e)
        {
            if (checkedListBoxTables.CheckedItems.Count == 0)
            {
                Service_ErrorHandler.ShowWarning("Please select at least one table.", "Selection Required");
                return;
            }

            if (Service_ErrorHandler.ShowConfirmation("This will overwrite data in the selected tables. Continue?", "Single Table Migration") != DialogResult.Yes)
                return;

            try
            {
                progressBar.Value = 0;
                btnMigrateSelected.Enabled = false;
                int total = checkedListBoxTables.CheckedItems.Count;
                int current = 0;

                foreach (string tableName in checkedListBoxTables.CheckedItems)
                {
                    Log($"Migrating table: {tableName}...");
                    var result = await Service_Migration.MigrateSingleTableAsync(tableName);
                    Log(result.IsSuccess ? $"{tableName}: Success" : $"{tableName}: Failed - {result.ErrorMessage}");
                    
                    current++;
                    progressBar.Value = (int)((double)current / total * 100);
                }

                Log("Selected Table Migration Completed.");
            }
            catch (Exception ex)
            {
                Log($"Error: {ex.Message}");
            }
            finally
            {
                btnMigrateSelected.Enabled = true;
            }
        }

        private async void btnBackup_Click(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                Log("Starting Backup...");
                progressBar.Style = ProgressBarStyle.Marquee;
                
                var result = await Service_Migration.BackupDatabaseAsync(fbd.SelectedPath);
                
                progressBar.Style = ProgressBarStyle.Blocks;
                progressBar.Value = 100;

                if (result.IsSuccess)
                {
                    Log($"Backup Success: {result.Data}");
                    Service_ErrorHandler.ShowInformation($"Backup saved to:\n{result.Data}", "Backup Complete");
                }
                else
                {
                    Log($"Backup Failed: {result.ErrorMessage}");
                    Service_ErrorHandler.ShowUserError(result.ErrorMessage);
                }
            }
        }

        private async void btnCleanTestData_Click(object sender, EventArgs e)
        {
            if (Service_ErrorHandler.ShowConfirmation("This will DELETE all inventory and transactions created by 'JOHNK' or containing 'test'.\n\nThis cannot be undone.\n\nProceed?", "Clean Test Data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            Log("Cleaning Test Data...");
            var result = await Service_Migration.CleanTestDataAsync();
            
            if (result.IsSuccess)
            {
                Log($"Cleanup Success. Rows deleted: {result.Data}");
                Service_ErrorHandler.ShowInformation($"Deleted {result.Data} test records.", "Cleanup Complete");
            }
            else
            {
                Log($"Cleanup Failed: {result.ErrorMessage}");
            }
        }

        private async void btnOptimize_Click(object sender, EventArgs e)
        {
            Log("Optimizing Tables...");
            var result = await Service_Migration.OptimizeTablesAsync();
            Log(result.IsSuccess ? "Optimization Success" : $"Optimization Failed: {result.ErrorMessage}");
        }

        private async void btnTruncateLogs_Click(object sender, EventArgs e)
        {
            if (Service_ErrorHandler.ShowConfirmation("Clear all error logs?", "Confirm") == DialogResult.Yes)
            {
                Log("Clearing Logs...");
                var result = await Service_Migration.TruncateLogsAsync();
                Log(result.IsSuccess ? "Logs Cleared" : $"Failed: {result.ErrorMessage}");
            }
        }

        private async void btnCheckHealth_Click(object sender, EventArgs e)
        {
            Log("Checking Table Row Counts...");
            var result = await Service_Migration.GetTableRowCountsAsync();
            if (result.IsSuccess && result.Data != null)
            {
                foreach (var kvp in result.Data)
                {
                    Log($"{kvp.Key}: {kvp.Value} rows");
                }
            }
            else
            {
                Log($"Check Failed: {result.ErrorMessage}");
            }
        }

        private async void btnTableSizes_Click(object sender, EventArgs e)
        {
            Log("Analyzing Table Sizes...");
            var result = await Service_Migration.GetTableSizesAsync();
            if (result.IsSuccess && result.Data != null)
            {
                // Create a temporary grid to show results if needed, or just log
                // For now, logging to the rich text box
                Log(string.Format("{0,-30} {1,10}", "Table Name", "Size (MB)"));
                Log(new string('-', 45));
                
                foreach (var row in result.Data)
                {
                    Log(string.Format("{0,-30} {1,10}", row["Table"], row["SizeMB"]));
                }
            }
            else
            {
                Log($"Analysis Failed: {result.ErrorMessage}");
            }
        }

        private async void btnIntegrity_Click(object sender, EventArgs e)
        {
            Log("Verifying Table Integrity...");
            var result = await Service_Migration.CheckTableIntegrityAsync();
            if (result.IsSuccess && result.Data != null)
            {
                foreach (var kvp in result.Data)
                {
                    string status = kvp.Value;
                    if (status == "OK")
                        Log($"{kvp.Key}: OK");
                    else
                        Log($"{kvp.Key}: {status}"); // Shows error if not OK
                }
            }
            else
            {
                Log($"Integrity Check Failed: {result.ErrorMessage}");
            }
        }

        private async void btnConnections_Click(object sender, EventArgs e)
        {
            Log("Monitoring Active Connections...");
            var result = await Service_Migration.GetActiveConnectionsAsync();
            if (result.IsSuccess && result.Data != null)
            {
                foreach (var conn in result.Data)
                {
                    Log(conn);
                }
            }
            else
            {
                Log($"Monitor Failed: {result.ErrorMessage}");
            }
        }

        private async void btnSchema_Click(object sender, EventArgs e)
        {
            Log("Validating Database Schema...");
            var result = await Service_Migration.ValidateSchemaAsync();
            if (result.IsSuccess)
            {
                if (result.Data == null || result.Data.Count == 0)
                {
                    Log("Schema Validation: PASS (All required tables and columns found)");
                }
                else
                {
                    Log("Schema Validation: FAIL");
                    foreach (var issue in result.Data)
                    {
                        Log($" - {issue}");
                    }
                }
            }
            else
            {
                Log($"Validation Failed: {result.ErrorMessage}");
            }
        }

        private async void btnArchiveLogs_Click(object sender, EventArgs e)
        {
            using var fbd = new FolderBrowserDialog();
            fbd.Description = "Select Archive Destination";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                Log("Archiving Logs...");
                var result = await Service_Migration.ArchiveAndClearLogsAsync(fbd.SelectedPath);
                if (result.IsSuccess)
                {
                    Log($"Logs Archived to: {result.Data}");
                    Log("Log Table Truncated.");
                    Service_ErrorHandler.ShowInformation($"Logs archived and cleared successfully.\nFile: {result.Data}", "Archive Complete");
                }
                else
                {
                    Log($"Archive Failed: {result.ErrorMessage}");
                }
            }
        }

        private async void btnFactoryReset_Click(object sender, EventArgs e)
        {
            // Warning 1
            if (Service_ErrorHandler.ShowConfirmation(
                "Are you sure you want to perform a Factory Reset?\n\nThis will DELETE ALL INVENTORY and TRANSACTION history.\nMaster Data (Parts, Users) will be kept.", 
                "Factory Reset - Warning 1/3", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Warning) != DialogResult.Yes) return;

            // Warning 2
            if (Service_ErrorHandler.ShowConfirmation(
                "THIS IS PERMANENT.\n\nAll current inventory counts and movement history will be lost forever.\n\nAre you absolutely sure?", 
                "Factory Reset - Warning 2/3", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Error) != DialogResult.Yes) return;

            // Warning 3
            if (Service_ErrorHandler.ShowConfirmation(
                "FINAL CHANCE.\n\nClick YES to wipe the database.\nClick NO to cancel.", 
                "Factory Reset - Warning 3/3", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Stop) != DialogResult.Yes) return;

            Log("PERFORMING FACTORY RESET...");
            var result = await Service_Migration.FactoryResetAsync();
            
            if (result.IsSuccess)
            {
                Log("FACTORY RESET COMPLETE.");
                Service_ErrorHandler.ShowInformation("Factory Reset Complete.\nAll inventory data has been wiped.", "Reset Successful");
            }
            else
            {
                Log($"RESET FAILED: {result.ErrorMessage}");
                Service_ErrorHandler.ShowUserError($"Reset Failed: {result.ErrorMessage}");
            }
        }

        private async void btnUpdateUserShifts_Click(object sender, EventArgs e)
        {
            try
            {
                Log("Calculating User Shifts...");
                btnUpdateUserShifts.Enabled = false;

                if (Program.ServiceProvider == null)
                {
                    Log("Error: Service Provider not initialized.");
                    return;
                }

                var service = Program.ServiceProvider.GetRequiredService<IService_UserShiftLogic>();
                var shiftsResult = await service.CalculateAllUserShiftsAsync();

                if (!shiftsResult.IsSuccess)
                {
                    Log($"Calculation Failed: {shiftsResult.ErrorMessage}");
                    return;
                }

                Log($"Calculated shifts for {(shiftsResult.Data != null ? shiftsResult.Data.Count : 0)} users.");

                // Read existing data first to preserve names
                var dao = Program.ServiceProvider.GetRequiredService<Data.IDao_VisualAnalytics>();
                var existingDataResult = await dao.GetSysVisualDataAsync();
                
                Dictionary<string, string> names = new Dictionary<string, string>();
                if (existingDataResult.IsSuccess && existingDataResult.Data != null && !string.IsNullOrEmpty(existingDataResult.Data.JsonUserFullNames))
                {
                    try 
                    {
                        names = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(existingDataResult.Data.JsonUserFullNames) 
                                ?? new Dictionary<string, string>();
                    }
                    catch { /* ignore json error */ }
                }

                var saveResult = await service.SaveVisualMetadataAsync(shiftsResult.Data ?? new Dictionary<string, int>(), names);

                if (saveResult.IsSuccess)
                {
                    Log("User Shifts Updated Successfully.");
                    Service_ErrorHandler.ShowInformation("User shifts updated based on last 50 transactions.", "Success");
                }
                else
                {
                    Log($"Save Failed: {saveResult.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                Log($"Error: {ex.Message}");
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, controlName: nameof(Form_DatabaseMaintenance));
            }
            finally
            {
                btnUpdateUserShifts.Enabled = true;
            }
        }

        private async void btnUpdateUserNames_Click(object sender, EventArgs e)
        {
            try
            {
                Log("Fetching User Names from Visual...");
                btnUpdateUserNames.Enabled = false;

                if (Program.ServiceProvider == null)
                {
                    Log("Error: Service Provider not initialized.");
                    return;
                }

                var service = Program.ServiceProvider.GetRequiredService<IService_UserShiftLogic>();
                var namesResult = await service.FetchUserFullNamesAsync();

                if (!namesResult.IsSuccess)
                {
                    Log($"Fetch Failed: {namesResult.ErrorMessage}");
                    return;
                }

                var fetchedCount = namesResult.Data?.Count ?? 0;
                Log($"Fetched {fetchedCount} user names.");

                // Read existing shifts to preserve them
                var dao = Program.ServiceProvider.GetRequiredService<Data.IDao_VisualAnalytics>();
                var existingDataResult = await dao.GetSysVisualDataAsync();
                
                Dictionary<string, int> shifts = new Dictionary<string, int>();
                if (existingDataResult.IsSuccess && existingDataResult.Data != null && !string.IsNullOrEmpty(existingDataResult.Data.JsonShiftData))
                {
                    try 
                    {
                        shifts = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, int>>(existingDataResult.Data.JsonShiftData) 
                                 ?? new Dictionary<string, int>();
                    }
                    catch { /* ignore json error */ }
                }

                var namesDict = namesResult.Data ?? new Dictionary<string, string>();
                var saveResult = await service.SaveVisualMetadataAsync(shifts, namesDict);

                if (saveResult.IsSuccess)
                {
                    Log("User Names Updated Successfully.");
                    Service_ErrorHandler.ShowInformation("User names updated from Visual database.", "Success");
                }
                else
                {
                    Log($"Save Failed: {saveResult.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                Log($"Error: {ex.Message}");
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, controlName: nameof(Form_DatabaseMaintenance));
            }
            finally
            {
                btnUpdateUserNames.Enabled = true;
            }
        }
    }
}
