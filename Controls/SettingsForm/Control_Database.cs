using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    /// <summary>
    /// Database connection settings control.
    /// Allows administrators and developers to configure MySQL connection parameters.
    /// </summary>
    public partial class Control_Database : ThemedUserControl
    {
        #region Events

        /// <summary>
        /// Fired when database settings are successfully updated.
        /// </summary>
        public event EventHandler? DatabaseSettingsUpdated;

        /// <summary>
        /// Fired when status message needs to be displayed.
        /// </summary>
        public event EventHandler<string>? StatusMessageChanged;

        public event EventHandler? BackToHomeRequested;

        #endregion

        #region Constructors

        public Control_Database()
        {
            InitializeComponent();
            Control_Database_Button_Home.Click += (_, _) => BackToHomeRequested?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Methods

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadDatabaseSettingsAsync();
        }

        /// <summary>
        /// Loads current database settings from user profile.
        /// </summary>
        private async Task LoadDatabaseSettingsAsync()
        {
            try
            {
                string user = Model_Application_Variables.User;

                // Load all database settings in parallel
                var serverTask = Dao_User.GetWipServerAddressAsync(user);
                var portTask = Dao_User.GetWipServerPortAsync(user);
                var databaseTask = Dao_User.GetDatabaseAsync(user);

                await Task.WhenAll(serverTask, portTask, databaseTask);

                var serverResult = await serverTask;
                var portResult = await portTask;
                var databaseResult = await databaseTask;

                // Update UI with loaded values (or defaults)
                Control_Database_TextBox_Server.Text = serverResult.IsSuccess && !string.IsNullOrWhiteSpace(serverResult.Data)
                    ? serverResult.Data
                    : "172.16.1.104";

                Control_Database_TextBox_Port.Text = portResult.IsSuccess && !string.IsNullOrWhiteSpace(portResult.Data)
                    ? portResult.Data
                    : "3306";

                Control_Database_TextBox_Database.Text = databaseResult.IsSuccess && !string.IsNullOrWhiteSpace(databaseResult.Data)
                    ? databaseResult.Data
                    : "mtm_wip_application_winforms";

                // Update Active Connection Label
                string activeDb = Model_Shared_Users.Database ?? "Unknown";
                string activeServer = Model_Shared_Users.WipServerAddress;
                Control_Database_Label_ActiveConnection.Text = $"Active Connection: {activeDb} on {activeServer}";

                if (activeDb.Contains("test", StringComparison.OrdinalIgnoreCase))
                {
                    Control_Database_Label_ActiveConnection.ForeColor = Color.OrangeRed;
                    Control_Database_Label_ActiveConnection.Text += " (TEST MODE)";
                }
                else
                {
                    Control_Database_Label_ActiveConnection.ForeColor = Color.DimGray;
                }

                StatusMessageChanged?.Invoke(this, "Database settings loaded.");
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object> { ["User"] = Model_Application_Variables.User },
                    callerName: nameof(LoadDatabaseSettingsAsync),
                    controlName: nameof(Control_Database));

                // Set default values as fallback
                Control_Database_TextBox_Server.Text = "172.16.1.104";
                Control_Database_TextBox_Port.Text = "3306";
                Control_Database_TextBox_Database.Text = "mtm_wip_application_winforms";

                StatusMessageChanged?.Invoke(this, "Error loading database settings. Defaults loaded.");
            }
        }

        /// <summary>
        /// Saves database settings to user profile.
        /// </summary>
        private async Task SaveDatabaseSettingsAsync()
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(Control_Database_TextBox_Server.Text))
                {
                    Service_ErrorHandler.HandleValidationError("Server address cannot be empty.", "Server Address",
                        callerName: nameof(SaveDatabaseSettingsAsync),
                        controlName: nameof(Control_Database));
                    Control_Database_TextBox_Server.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(Control_Database_TextBox_Port.Text))
                {
                    Service_ErrorHandler.HandleValidationError("Port number cannot be empty.", "Port",
                        callerName: nameof(SaveDatabaseSettingsAsync),
                        controlName: nameof(Control_Database));
                    Control_Database_TextBox_Port.Focus();
                    return;
                }

                if (!int.TryParse(Control_Database_TextBox_Port.Text, out int port) || port <= 0 || port > 65535)
                {
                    Service_ErrorHandler.HandleValidationError("Port must be a number between 1 and 65535.", "Port",
                        callerName: nameof(SaveDatabaseSettingsAsync),
                        controlName: nameof(Control_Database));
                    Control_Database_TextBox_Port.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(Control_Database_TextBox_Database.Text))
                {
                    Service_ErrorHandler.HandleValidationError("Database name cannot be empty.", "Database Name",
                        callerName: nameof(SaveDatabaseSettingsAsync),
                        controlName: nameof(Control_Database));
                    Control_Database_TextBox_Database.Focus();
                    return;
                }

                string user = Model_Application_Variables.User;

                // Save all settings in parallel
                var serverTask = Dao_User.SetWipServerAddressAsync(user, Control_Database_TextBox_Server.Text.Trim());
                var portTask = Dao_User.SetWipServerPortAsync(user, Control_Database_TextBox_Port.Text.Trim());
                var databaseTask = Dao_User.SetDatabaseAsync(user, Control_Database_TextBox_Database.Text.Trim());

                await Task.WhenAll(serverTask, portTask, databaseTask);

                var serverResult = await serverTask;
                var portResult = await portTask;
                var databaseResult = await databaseTask;

                if (serverResult.IsSuccess && portResult.IsSuccess && databaseResult.IsSuccess)
                {
                    DatabaseSettingsUpdated?.Invoke(this, EventArgs.Empty);
                    StatusMessageChanged?.Invoke(this, "Database settings saved. Restart application for changes to take effect.");

                    Service_ErrorHandler.ShowInformation(
                        "Database settings saved successfully!\n\n" +
                        "⚠️ Important: You must restart the application for these changes to take effect.",
                        "Database Settings Saved");
                }
                else
                {
                    // Show first error that occurred
                    string errorMessage = !serverResult.IsSuccess ? serverResult.ErrorMessage
                        : !portResult.IsSuccess ? portResult.ErrorMessage
                        : databaseResult.ErrorMessage;

                    MessageBox.Show($"Failed to save database settings:\n{errorMessage}",
                        "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    StatusMessageChanged?.Invoke(this, "Error saving database settings.");
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object>
                    {
                        ["User"] = Model_Application_Variables.User,
                        ["Server"] = Control_Database_TextBox_Server.Text,
                        ["Port"] = Control_Database_TextBox_Port.Text,
                        ["Database"] = Control_Database_TextBox_Database.Text
                    },
                    callerName: nameof(SaveDatabaseSettingsAsync),
                    controlName: nameof(Control_Database));
            }
        }

        /// <summary>
        /// Tests database connectivity with current settings.
        /// </summary>
        private async Task TestConnectionAsync()
        {
            try
            {
                Control_Database_Button_TestConnection.Enabled = false;
                Control_Database_Button_TestConnection.Text = "Testing...";

                // Show progress
                StatusMessageChanged?.Invoke(this, "Testing database connections...");

                // 1. Test Proposed Settings (from TextBoxes)
                string testServer = Control_Database_TextBox_Server.Text.Trim();
                string testPort = Control_Database_TextBox_Port.Text.Trim();
                string testDatabase = Control_Database_TextBox_Database.Text.Trim();

                // Validate inputs
                if (string.IsNullOrWhiteSpace(testServer) || string.IsNullOrWhiteSpace(testPort) || string.IsNullOrWhiteSpace(testDatabase))
                {
                    MessageBox.Show(
                        "Please fill in all database connection fields before testing.",
                        "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string proposedConnectionString = Helper_Database_Variables.GetConnectionString(
                    testServer,
                    testDatabase,
                    Model_Application_Variables.DatabaseUser,
                    Model_Application_Variables.DatabasePassword);

                var proposedResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                    proposedConnectionString,
                    "usr_users_Exists",
                    new Dictionary<string, object> { ["User"] = Model_Application_Variables.User });

                // 2. Test Active Connection (Current Runtime)
                string activeConnectionString = Helper_Database_Variables.GetConnectionString(
                    null, // Use default from Model_Shared_Users
                    null, // Use default from Model_Shared_Users
                    Model_Application_Variables.DatabaseUser,
                    Model_Application_Variables.DatabasePassword);

                var activeResult = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(
                    activeConnectionString,
                    "usr_users_Exists",
                    new Dictionary<string, object> { ["User"] = Model_Application_Variables.User });

                // 3. Construct Result Message
                var sb = new System.Text.StringBuilder();
                bool overallSuccess = proposedResult.IsSuccess && activeResult.IsSuccess;

                sb.AppendLine("Connection Test Results:");
                sb.AppendLine("--------------------------------------------------");

                // Proposed Results
                sb.AppendLine("1. Proposed Settings (TextBoxes):");
                sb.AppendLine($"   Server: {testServer}");
                sb.AppendLine($"   Database: {testDatabase}");
                if (proposedResult.IsSuccess)
                    sb.AppendLine("   Status: ✅ SUCCESS");
                else
                    sb.AppendLine($"   Status: ❌ FAILED ({proposedResult.ErrorMessage})");

                sb.AppendLine();

                // Active Results
                sb.AppendLine("2. Active Runtime Connection:");
                sb.AppendLine($"   Server: {Model_Shared_Users.WipServerAddress}");
                sb.AppendLine($"   Database: {Model_Shared_Users.Database}");
                if (activeResult.IsSuccess)
                    sb.AppendLine("   Status: ✅ SUCCESS");
                else
                    sb.AppendLine($"   Status: ❌ FAILED ({activeResult.ErrorMessage})");

                // Show Message
                if (overallSuccess)
                {
                    Service_ErrorHandler.ShowInformation(sb.ToString(), "Connection Tests Passed");
                    StatusMessageChanged?.Invoke(this, "All connection tests passed.");
                }
                else
                {
                    MessageBox.Show(sb.ToString(), "Connection Tests Completed with Errors",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    StatusMessageChanged?.Invoke(this, "Connection tests completed with errors.");
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object>
                    {
                        ["Server"] = Control_Database_TextBox_Server.Text,
                        ["Port"] = Control_Database_TextBox_Port.Text,
                        ["Database"] = Control_Database_TextBox_Database.Text
                    },
                    callerName: nameof(TestConnectionAsync),
                    controlName: nameof(Control_Database));
            }
            finally
            {
                Control_Database_Button_TestConnection.Enabled = true;
                Control_Database_Button_TestConnection.Text = "Test Connection";
            }
        }

        #endregion

        #region Event Handlers

        private async void Control_Database_Button_Save_Click(object sender, EventArgs e)
        {
            try
            {
                Control_Database_Button_Save.Enabled = false;
                await SaveDatabaseSettingsAsync();
            }
            finally
            {
                Control_Database_Button_Save.Enabled = true;
            }
        }

        private void Control_Database_Button_Reset_Click(object sender, EventArgs e)
        {
            // Reset to production defaults
            Control_Database_TextBox_Server.Text = "172.16.1.104";
            Control_Database_TextBox_Port.Text = "3306";
            Control_Database_TextBox_Database.Text = "mtm_wip_application_winforms";

            StatusMessageChanged?.Invoke(this, "Settings reset to defaults. Click Save to apply.");
        }

        private async void Control_Database_Button_TestConnection_Click(object sender, EventArgs e)
        {
            await TestConnectionAsync();
        }

        #endregion
    }
}
