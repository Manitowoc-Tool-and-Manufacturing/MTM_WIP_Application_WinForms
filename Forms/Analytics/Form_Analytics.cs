using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services.Analytics;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.Logging;

namespace MTM_WIP_Application_Winforms.Forms.Analytics
{
    public partial class Form_Analytics : ThemedForm
    {
        private readonly Service_Analytics _analyticsService;

        public Form_Analytics()
        {
            InitializeComponent();
            _analyticsService = new Service_Analytics();
            
            // Set default dates
            dtpDateFrom.Value = DateTime.Today.AddDays(-7);
            dtpDateTo.Value = DateTime.Today;

            // Initialize grids
            InitializeGrids();
            
            // Initialize Glossary
            InitializeGlossary();
            
            // Load initial data
            this.Shown += async (s, e) => await LoadDataAsync();
        }

        private void InitializeGlossary()
        {
            txtGlossary.Text = 
@"MATERIAL HANDLER ANALYTICS GLOSSARY

METRICS DEFINITIONS:

1. Total Tx (Total Transactions)
   The total number of inventory movements recorded by the user within the selected date range.
   Includes IN, OUT, and TRANSFER operations.

2. Total Qty (Total Quantity)
   The sum of all quantities moved across all transactions.

3. Unique Parts
   The number of distinct Part IDs handled by the user. A higher number indicates exposure to a wider variety of inventory.

4. Rapid Fire
   Count of transactions occurring less than 10 seconds apart for the SAME part.
   - Why it matters: Often caused by ""batch entry"" (entering a list of written transactions) or ""blind entry"" (not verifying).
   - Note: Bulk operations (Advanced Inventory, Bulk Remove) are excluded (transactions < 2 seconds apart).
   - Impact on Score: Moderate penalty.

5. Ping Pong
   Count of events where a part is moved from Location A to Location B, and then back to Location A within 20 minutes.
   - Why it matters: This often indicates a mistake was made and immediately corrected, or confusion about where the part belongs.
   - Impact on Score: High penalty.

6. Off Shift (Outside Shift)
   Count of transactions performed outside the user's assigned shift hours.
   - First Shift: 06:00 - 14:00
   - Second Shift: 14:00 - 22:00
   - Third Shift: 22:00 - 06:00
   - Weekend Shift: 06:00 - 18:00 (Fri, Sat, Sun, Mon)
   - Why it matters: Tracks work done outside standard hours.
   - Impact on Score: None (Considered positive/overtime).

7. Quality Score
   A calculated metric (0-100) representing the overall reliability of the material handler.
   Formula: 100 - ((RapidFire * 0.5 + PingPong * 5) / TotalTx * 100)
   - 90-100: Excellent
   - 80-89: Good
   - Below 80: Needs Review

DATA REFRESH:
- Use the date pickers at the top of the Performance tab to filter data.
- Click 'Refresh' to apply the date filter.
- Data is retrieved directly from the live database (inv_transaction table).
";
        }

        private void InitializeGrids()
        {
            // Performance Grid
            gridPerformance.AutoGenerateColumns = false;
            gridPerformance.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "UserName", HeaderText = "User", Width = 100 });
            gridPerformance.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FullName", HeaderText = "Name", Width = 150 });
            gridPerformance.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Shift", HeaderText = "Shift", Width = 80 });
            gridPerformance.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TotalTransactions", HeaderText = "Total Tx", Width = 80 });
            gridPerformance.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TotalQuantity", HeaderText = "Total Qty", Width = 100 });
            gridPerformance.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "UniqueParts", HeaderText = "Unique Parts", Width = 100 });
            gridPerformance.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "RapidFireCount", HeaderText = "Rapid Fire", Width = 80 });
            gridPerformance.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PingPongCount", HeaderText = "Ping Pong", Width = 80 });
            gridPerformance.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "OutsideShiftCount", HeaderText = "Off Shift", Width = 80 });
            gridPerformance.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "QualityScore", HeaderText = "Score", Width = 80, DefaultCellStyle = new DataGridViewCellStyle { Format = "N1" } });

            // Quality Grid (Reusing same structure for now, but could be different)
            gridQuality.AutoGenerateColumns = false;
            gridQuality.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "UserName", HeaderText = "User", Width = 100 });
            gridQuality.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "RapidFireCount", HeaderText = "Rapid Fire", Width = 100 });
            gridQuality.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PingPongCount", HeaderText = "Ping Pong", Width = 100 });
            gridQuality.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "OutsideShiftCount", HeaderText = "Off Shift", Width = 100 });
            gridQuality.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "QualityScore", HeaderText = "Score", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "N1" } });

            // User History Grid
            gridUserHistory.AutoGenerateColumns = false;
            gridUserHistory.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DateTime", HeaderText = "Time", Width = 140 });
            gridUserHistory.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TransactionType", HeaderText = "Type", Width = 80 });
            gridUserHistory.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "PartID", HeaderText = "Part", Width = 120 });
            gridUserHistory.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "Qty", Width = 80 });
            gridUserHistory.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "FromLocation", HeaderText = "From", Width = 100 });
            gridUserHistory.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ToLocation", HeaderText = "To", Width = 100 });
        }

        private async Task LoadDataAsync()
        {
            try
            {
                UpdateStatus("Loading analytics data...", 0);
                
                // Load Users for ComboBox
                var usersResult = await _analyticsService.GetAllUserNamesAsync();
                if (usersResult.IsSuccess && usersResult.Data != null)
                {
                    comboUsers.DataSource = usersResult.Data;
                }

                await RefreshPerformanceAsync();
                
                UpdateStatus("Ready", 100);
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium);
                UpdateStatus("Error loading data", 0);
            }
        }

        private async Task RefreshPerformanceAsync()
        {
            UpdateStatus("Analyzing performance...", 30);
            
            var start = dtpDateFrom.Value.Date;
            var end = dtpDateTo.Value.Date.AddDays(1).AddSeconds(-1);

            // Log for debugging
            LoggingUtility.LogApplicationInfo($"Refreshing Analytics: {start} to {end}");

            var result = await _analyticsService.GetTeamPerformanceAsync(start, end);
            
            if (result.IsSuccess)
            {
                // Clear first to ensure UI update
                gridPerformance.DataSource = null;
                gridQuality.DataSource = null;

                gridPerformance.DataSource = result.Data;
                gridQuality.DataSource = result.Data; // Using same data for now, just different view
                
                UpdateStatus($"Loaded {result.Data?.Count ?? 0} records", 100);
            }
            else
            {
                Service_ErrorHandler.ShowUserError(result.ErrorMessage);
                UpdateStatus("Error loading data", 0);
            }
        }

        private void UpdateStatus(string message, int progress)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateStatus(message, progress)));
                return;
            }
            
            lblStatus.Text = message;
            progressBar.Value = progress;
        }

        private async void btnRefreshPerformance_Click(object sender, EventArgs e)
        {
            await RefreshPerformanceAsync();
        }

        private async void btnRefreshQuality_Click(object sender, EventArgs e)
        {
            await RefreshPerformanceAsync();
        }

        private async void btnLoadUser_Click(object sender, EventArgs e)
        {
            if (comboUsers.SelectedItem == null) return;
            
            string? user = comboUsers.SelectedItem.ToString();
            if (string.IsNullOrEmpty(user)) return;

            UpdateStatus($"Loading history for {user}...", 30);

            // Use Analytics Service with raw SQL instead of DAO
            var result = await _analyticsService.GetUserHistoryAsync(
                user,
                dtpDateFrom.Value.Date,
                dtpDateTo.Value.Date.AddDays(1).AddSeconds(-1)
            );

            if (result.IsSuccess)
            {
                gridUserHistory.DataSource = result.Data;
            }
            else
            {
                Service_ErrorHandler.ShowUserError(result.ErrorMessage);
            }
            
            UpdateStatus("Ready", 100);
        }
    }
}
