using System.Data;
using Microsoft.Extensions.DependencyInjection;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.Visual;
using MTM_WIP_Application_Winforms.Services.Analytics;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Models.Analytics;
using MTM_WIP_Application_Winforms.Models.Enums;
using Newtonsoft.Json;
using MTM_WIP_Application_Winforms.Services.Logging;

namespace MTM_WIP_Application_Winforms.Controls.Visual
{
    public partial class Control_VisualUserAnalytics : ThemedUserControl
    {
        #region Fields
        private readonly IService_VisualDatabase? _visualService;
        private readonly IDao_VisualAnalytics? _daoVisualAnalytics;
        private readonly IService_UserShiftLogic? _userShiftLogicService;
        private Dictionary<string, int> _userShifts = new Dictionary<string, int>();
        private Dictionary<string, string> _userNames = new Dictionary<string, string>();
        #endregion

        #region Constructors
        public Control_VisualUserAnalytics()
        {
            InitializeComponent();
            _visualService = Program.ServiceProvider?.GetService<IService_VisualDatabase>();
            _daoVisualAnalytics = Program.ServiceProvider?.GetService<IDao_VisualAnalytics>();
            _userShiftLogicService = Program.ServiceProvider?.GetService<IService_UserShiftLogic>();

            // Initialize Analytics Workflow List
            Control_VisualUserAnalytics_CheckedListBox_Instructions.SelectionMode = SelectionMode.None;

            WireUpEvents();
            InitializeDefaultValues();
            ApplyPrivileges();
        }
        #endregion

        #region Initialization
        private void InitializeDefaultValues()
        {
            // Analytics Defaults
            Control_VisualUserAnalytics_RadioButton_Month.Checked = true;
            SetDateRange(Control_VisualUserAnalytics_DateTimePicker_StartDate, Control_VisualUserAnalytics_DateTimePicker_EndDate, "Month");
            
            UpdateAnalyticsWorkflow();
        }

        private void WireUpEvents()
        {
            // Analytics Date Range Events
            Control_VisualUserAnalytics_RadioButton_Today.CheckedChanged += (s, e) => OnAnalyticsDateRangeChanged();
            Control_VisualUserAnalytics_RadioButton_Week.CheckedChanged += (s, e) => OnAnalyticsDateRangeChanged();
            Control_VisualUserAnalytics_RadioButton_Month.CheckedChanged += (s, e) => OnAnalyticsDateRangeChanged();
            Control_VisualUserAnalytics_RadioButton_Custom.CheckedChanged += (s, e) => OnAnalyticsDateRangeChanged();

            // User Analytics Events
            Control_VisualUserAnalytics_Button_LoadUsers.Click += async (s, e) => await LoadUsersForAnalyticsAsync();
            Control_VisualUserAnalytics_Button_SelectAllUsers.Click += (s, e) => SelectAllUsers();
            Control_VisualUserAnalytics_Button_GenerateReport.Click += async (s, e) => await GenerateAnalyticsReportAsync();
            Control_VisualUserAnalytics_CheckedListBox_Users.ItemCheck += (s, e) => 
            {
                // Delay check to allow ItemCheck to complete
                this.BeginInvoke(new Action(() => 
                {
                    UpdateUserSelectionState();
                    UpdateAnalyticsWorkflow();
                }));
            };
            
            Control_VisualUserAnalytics_DateTimePicker_StartDate.ValueChanged += (s, e) => UpdateAnalyticsWorkflow();
            Control_VisualUserAnalytics_DateTimePicker_EndDate.ValueChanged += (s, e) => UpdateAnalyticsWorkflow();
        }

        private void ApplyPrivileges()
        {
            // Accessible to all users, but data loading is restricted for non-admins
            Control_VisualUserAnalytics_TableLayout_Main.Visible = true;
        }
        #endregion

        #region Methods

        private void OnAnalyticsDateRangeChanged()
        {
            if (Control_VisualUserAnalytics_RadioButton_Today.Checked) SetDateRange(Control_VisualUserAnalytics_DateTimePicker_StartDate, Control_VisualUserAnalytics_DateTimePicker_EndDate, "Today");
            else if (Control_VisualUserAnalytics_RadioButton_Week.Checked) SetDateRange(Control_VisualUserAnalytics_DateTimePicker_StartDate, Control_VisualUserAnalytics_DateTimePicker_EndDate, "Week");
            else if (Control_VisualUserAnalytics_RadioButton_Month.Checked) SetDateRange(Control_VisualUserAnalytics_DateTimePicker_StartDate, Control_VisualUserAnalytics_DateTimePicker_EndDate, "Month");
            else if (Control_VisualUserAnalytics_RadioButton_Custom.Checked) SetDateRange(Control_VisualUserAnalytics_DateTimePicker_StartDate, Control_VisualUserAnalytics_DateTimePicker_EndDate, "Custom");
        }

        private void SetDateRange(DateTimePicker dtpStart, DateTimePicker dtpEnd, string rangeType)
        {
            bool isCustom = rangeType == "Custom";
            dtpStart.Enabled = isCustom;
            dtpEnd.Enabled = isCustom;

            if (isCustom) return;

            DateTime end = DateTime.Today.AddDays(1).AddSeconds(-1);
            DateTime start = DateTime.Today;

            switch (rangeType)
            {
                case "Today":
                    start = DateTime.Today;
                    break;
                case "Week":
                    start = DateTime.Today.AddDays(-7);
                    break;
                case "Month":
                    start = DateTime.Today.AddDays(-30);
                    break;
            }

            dtpStart.Value = start;
            dtpEnd.Value = end;
        }

        private void UpdateAnalyticsWorkflow()
        {
            // Step 1: Enter Desired Date Range (Always true if dates valid)
            SetWorkflowStep(0, true);

            // Step 2: Click Load Users (True if users loaded)
            bool usersLoaded = Control_VisualUserAnalytics_CheckedListBox_Users.Items.Count > 0;
            SetWorkflowStep(1, usersLoaded);

            // Step 3: Click Select All Users (Optional - True if all selected)
            bool allSelected = usersLoaded && Control_VisualUserAnalytics_CheckedListBox_Users.CheckedItems.Count == Control_VisualUserAnalytics_CheckedListBox_Users.Items.Count;
            SetWorkflowStep(2, allSelected);

            // Step 4: Select Users Below (True if any selected)
            bool anySelected = Control_VisualUserAnalytics_CheckedListBox_Users.CheckedItems.Count > 0;
            SetWorkflowStep(3, anySelected);

            // Step 5: Click Generate Report (Handled in GenerateAnalyticsReportAsync)
        }

        private void SetWorkflowStep(int index, bool isChecked)
        {
            if (index >= 0 && index < Control_VisualUserAnalytics_CheckedListBox_Instructions.Items.Count)
            {
                Control_VisualUserAnalytics_CheckedListBox_Instructions.SetItemChecked(index, isChecked);
            }
        }

        private async Task LoadUsersForAnalyticsAsync()
        {
            if (_visualService == null) return;

            try
            {
                Control_VisualUserAnalytics_Button_LoadUsers.Enabled = false;
                Control_VisualUserAnalytics_Button_LoadUsers.Text = "Loading...";
                Control_VisualUserAnalytics_CheckedListBox_Users.Items.Clear();

                var start = Control_VisualUserAnalytics_DateTimePicker_StartDate.Value;
                var end = Control_VisualUserAnalytics_DateTimePicker_EndDate.Value;

                // 1. Get Active Users from Visual
                var result = await _visualService.GetDistinctUsersForAnalyticsAsync(start, end);
                if (!result.IsSuccess)
                {
                    Service_ErrorHandler.ShowError(result.ErrorMessage);
                    return;
                }

                // 2. Get WIP Users for Shift Mapping
                var wipUsersResult = await Dao_User.GetAllUsersAsync();
                var wipUsers = new List<DataRow>();
                if (wipUsersResult.IsSuccess && wipUsersResult.Data != null)
                {
                    foreach (DataRow row in wipUsersResult.Data.Rows)
                    {
                        wipUsers.Add(row);
                    }
                }

                // 3. Get Shift/Name Metadata from sys_visual (Fallback)
                if (_daoVisualAnalytics != null)
                {
                    var metaResult = await _daoVisualAnalytics.GetSysVisualDataAsync();
                    if (metaResult.IsSuccess && metaResult.Data != null)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(metaResult.Data.JsonShiftData))
                                _userShifts = JsonConvert.DeserializeObject<Dictionary<string, int>>(metaResult.Data.JsonShiftData) ?? new Dictionary<string, int>();
                            
                            if (!string.IsNullOrEmpty(metaResult.Data.JsonUserFullNames))
                                _userNames = JsonConvert.DeserializeObject<Dictionary<string, string>>(metaResult.Data.JsonUserFullNames) ?? new Dictionary<string, string>();
                        }
                        catch { /* Ignore JSON errors */ }
                    }
                }

                // 4. Filter and Populate
                var activeUsers = result.Data ?? new List<string>();
                var selectedShifts = new HashSet<int>();
                if (Control_VisualUserAnalytics_CheckBox_Shift1.Checked) selectedShifts.Add(1);
                if (Control_VisualUserAnalytics_CheckBox_Shift2.Checked) selectedShifts.Add(2);
                if (Control_VisualUserAnalytics_CheckBox_Shift3.Checked) selectedShifts.Add(3);
                if (Control_VisualUserAnalytics_CheckBox_ShiftWeekend.Checked) selectedShifts.Add(4);
                
                bool hasFullAccess = Model_Application_Variables.UserTypeAdmin || Model_Application_Variables.UserTypeDeveloper;
                string currentUser = Model_Application_Variables.User;

                foreach (var userId in activeUsers)
                {
                    // If not admin/dev, only add if it matches current user
                    if (!hasFullAccess)
                    {
                        // Check if this Visual User ID matches the current logged-in WIP User
                        // We need to find the WIP user record for this Visual ID to be sure, 
                        // or use the IsUserMatch heuristic.
                        
                        // Find matching WIP user first to be accurate
                        var matchingWipUserForCheck = wipUsers.FirstOrDefault(r => IsUserMatch(userId, r["User"]?.ToString() ?? ""));
                        
                        // If we found a match, check if it's the current user
                        if (matchingWipUserForCheck != null)
                        {
                            string wipUserName = matchingWipUserForCheck["User"]?.ToString() ?? "";
                            if (!string.Equals(wipUserName, currentUser, StringComparison.OrdinalIgnoreCase))
                            {
                                continue; // Skip other users
                            }
                        }
                        else
                        {
                            // If no WIP match found, we can't verify ownership, so skip to be safe
                            // OR check if the Visual ID looks like the current user (heuristic)
                            if (!IsUserMatch(userId, currentUser))
                            {
                                continue;
                            }
                        }
                    }

                    int shift = 0;

                    // Try to find shift from WIP Users first
                    var matchingWipUser = wipUsers.FirstOrDefault(r => IsUserMatch(userId, r["User"]?.ToString() ?? ""));
                    if (matchingWipUser != null)
                    {
                        string shiftStr = matchingWipUser["Shift"]?.ToString() ?? "";
                        if (int.TryParse(shiftStr, out int s)) shift = s;
                        else if (shiftStr.StartsWith("First")) shift = 1;
                        else if (shiftStr.StartsWith("Second")) shift = 2;
                        else if (shiftStr.StartsWith("Third")) shift = 3;
                        else if (shiftStr.StartsWith("Weekend")) shift = 4;
                    }

                    // Fallback to Visual Metadata if not found in WIP
                    if (shift == 0 && _userShifts.ContainsKey(userId))
                    {
                        shift = _userShifts[userId];
                    }
                    
                    // Filter by shift
                    // If shift is 0 (unknown), include it if Shift 1 is checked (fallback)
                    if (shift != 0 && !selectedShifts.Contains(shift)) continue;
                    if (shift == 0 && !Control_VisualUserAnalytics_CheckBox_Shift1.Checked) continue;

                    string displayName = userId;
                    if (_userNames.ContainsKey(userId))
                    {
                        displayName = $"{_userNames[userId]} ({userId})";
                    }
                    else if (matchingWipUser != null)
                    {
                        displayName = $"{matchingWipUser["Full Name"]} ({userId})";
                    }

                    Control_VisualUserAnalytics_CheckedListBox_Users.Items.Add(new UserDisplayItem { UserId = userId, DisplayName = displayName });
                }

                UpdateUserSelectionState();
                UpdateAnalyticsWorkflow();
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, controlName: this.Name);
            }
            finally
            {
                Control_VisualUserAnalytics_Button_LoadUsers.Enabled = true;
                Control_VisualUserAnalytics_Button_LoadUsers.Text = "Load Users";
            }
        }

        private bool IsUserMatch(string visualUser, string wipUser)
        {
            if (string.IsNullOrEmpty(visualUser) || visualUser.Length < 5) return false;
            
            char firstInitial = visualUser[0];
            string lastPart = visualUser.Substring(1); // First 4 of last name (assuming 5 chars total)
            
            // Check if WIP User starts with First Initial AND contains Last Part
            // Example: MSAMZ (Visual) vs MIKESAMZ (WIP)
            // M matches M
            // SAMZ is in MIKESAMZ
            
            return wipUser.StartsWith(firstInitial.ToString(), StringComparison.OrdinalIgnoreCase) &&
                   wipUser.IndexOf(lastPart, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void UpdateUserSelectionState()
        {
            int count = Control_VisualUserAnalytics_CheckedListBox_Users.CheckedItems.Count;
            Control_VisualUserAnalytics_Label_UserCount.Text = $"Selected: {count}";
            
            if (count > 10)
            {
                var userUiColors = Model_Application_Variables.UserUiColors;
                var colorWarning = userUiColors?.WarningColor;
                Control_VisualUserAnalytics_Label_UserCount.ForeColor = colorWarning ?? Color.OrangeRed;
                Control_VisualUserAnalytics_Label_UserCount.Text += " (Slow)";
                Control_VisualUserAnalytics_Button_GenerateReport.Enabled = true;
            }
            else
            {
                // Control_VisualUserAnalytics_Label_UserCount.ForeColor = Color.Black; // Removed to allow theme system to manage color
                Control_VisualUserAnalytics_Button_GenerateReport.Enabled = count > 0;
            }
        }

        private void SelectAllUsers()
        {
            bool allChecked = Control_VisualUserAnalytics_CheckedListBox_Users.CheckedItems.Count == Control_VisualUserAnalytics_CheckedListBox_Users.Items.Count;
            for (int i = 0; i < Control_VisualUserAnalytics_CheckedListBox_Users.Items.Count; i++)
            {
                Control_VisualUserAnalytics_CheckedListBox_Users.SetItemChecked(i, !allChecked);
            }
            UpdateUserSelectionState();
            UpdateAnalyticsWorkflow();
        }

        private async Task GenerateAnalyticsReportAsync()
        {
            if (_visualService == null || _userShiftLogicService == null) return;

            try
            {
                Control_VisualUserAnalytics_Button_GenerateReport.Enabled = false;
                Control_VisualUserAnalytics_Button_GenerateReport.Text = "Generating...";

                var selectedUsers = new HashSet<string>();
                foreach (var item in Control_VisualUserAnalytics_CheckedListBox_Users.CheckedItems)
                {
                    if (item is UserDisplayItem userItem)
                    {
                        selectedUsers.Add(userItem.UserId);
                    }
                    else
                    {
                        selectedUsers.Add(item?.ToString() ?? "");
                    }
                }

                var start = Control_VisualUserAnalytics_DateTimePicker_StartDate.Value;
                var end = Control_VisualUserAnalytics_DateTimePicker_EndDate.Value;

                // Use the new scoring logic service
                var result = await _userShiftLogicService.CalculateMaterialHandlerScoresAsync(start, end);

                if (result.IsSuccess && result.Data != null)
                {
                    SetWorkflowStep(4, true); // Step 5 complete
                    
                    // Filter by selected users
                    var filteredData = result.Data
                        .Where(x => selectedUsers.Contains(x.UserName))
                        .ToList();

                    if (filteredData.Count == 0)
                    {
                        Service_ErrorHandler.ShowInformation("No data found for the selected users and date range.");
                    }
                    else
                    {
                        // Open the viewer form with the Enhanced HTML template
                        var viewer = new MTM_WIP_Application_Winforms.Forms.Visual.Form_AnalyticsViewer(
                            filteredData, 
                            "VisualUserAnalytics_Enhanced.html");
                        viewer.Show();
                    }
                }
                else
                {
                    Service_ErrorHandler.ShowError(result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium, controlName: this.Name);
            }
            finally
            {
                Control_VisualUserAnalytics_Button_GenerateReport.Enabled = true;
                Control_VisualUserAnalytics_Button_GenerateReport.Text = "Generate Report";
            }
        }
        #endregion
    }

    public class UserDisplayItem
    {
        public string UserId { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public override string ToString() => DisplayName;
    }
}
