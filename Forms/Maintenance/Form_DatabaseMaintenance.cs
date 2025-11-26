using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.Maintenance;

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
    }
}
