using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    public partial class Control_Database : UserControl
    {
        public event EventHandler? DatabaseSettingsUpdated;
        public event EventHandler<string>? StatusMessageChanged;

        public Control_Database()
        {
            InitializeComponent();

            // Apply comprehensive DPI scaling and runtime layout adjustments
            Core_Themes.ApplyDpiScaling(this);
            Core_Themes.ApplyRuntimeLayoutAdjustments(this);

            // Wire up button events
            Control_Database_Button_Save.Click += SaveButton_Click;
            Control_Database_Button_Reset.Click += ResetButton_Click;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadDatabaseSettingsAsync();
        }

        private async Task LoadDatabaseSettingsAsync()
        {
            try
            {
                string user = Model_AppVariables.User;

                var serverResult = await Dao_User.GetWipServerAddressAsync(user);
                var portResult = await Dao_User.GetWipServerPortAsync(user);
                var databaseResult = await Dao_User.GetDatabaseAsync(user);

                if (serverResult.IsSuccess && portResult.IsSuccess && databaseResult.IsSuccess)
                {
                    Control_Database_TextBox_Server.Text = serverResult.Data ?? "localhost";
                    Control_Database_TextBox_Port.Text = portResult.Data ?? "3306";
                    Control_Database_TextBox_Database.Text = databaseResult.Data ?? "MTM_WIP_Application_Winforms";

                    StatusMessageChanged?.Invoke(this, "Database settings loaded successfully.");
                }
                else
                {
                    // Handle database errors gracefully
                    var errors = new List<string>();
                    if (!serverResult.IsSuccess) errors.Add($"Server: {serverResult.ErrorMessage}");
                    if (!portResult.IsSuccess) errors.Add($"Port: {portResult.ErrorMessage}");
                    if (!databaseResult.IsSuccess) errors.Add($"Database: {databaseResult.ErrorMessage}");

                    Service_ErrorHandler.HandleDatabaseError(
                        new Exception($"Failed to load database settings: {string.Join("; ", errors)}"),
                        contextData: new Dictionary<string, object> { ["User"] = user },
                        callerName: nameof(LoadDatabaseSettingsAsync),
                        controlName: nameof(Control_Database)
                    );

                    // Set default values as fallback
                    Control_Database_TextBox_Server.Text = "localhost";
                    Control_Database_TextBox_Port.Text = "3306";
                    Control_Database_TextBox_Database.Text = "MTM_WIP_Application_Winforms";

                    StatusMessageChanged?.Invoke(this, $"Error loading database settings: {string.Join("; ", errors)}");
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object> { ["User"] = Model_AppVariables.User },
                    callerName: nameof(LoadDatabaseSettingsAsync),
                    controlName: nameof(Control_Database));

                // Set default values as fallback
                Control_Database_TextBox_Server.Text = "localhost";
                Control_Database_TextBox_Port.Text = "3306";
                Control_Database_TextBox_Database.Text = "MTM_WIP_Application_Winforms";
            }
        }

        private async Task SaveDatabaseSettingsAsync()
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(Control_Database_TextBox_Server.Text))
                {
                    Service_ErrorHandler.HandleValidationError("Server address is required.", "Server",
                        callerName: nameof(SaveDatabaseSettingsAsync),
                        controlName: nameof(Control_Database));
                    return;
                }

                if (string.IsNullOrWhiteSpace(Control_Database_TextBox_Port.Text))
                {
                    Service_ErrorHandler.HandleValidationError("Port is required.", "Port",
                        callerName: nameof(SaveDatabaseSettingsAsync),
                        controlName: nameof(Control_Database));
                    return;
                }

                if (!int.TryParse(Control_Database_TextBox_Port.Text, out int port) || port <= 0 || port > 65535)
                {
                    Service_ErrorHandler.HandleValidationError("Port must be a valid number between 1 and 65535.", "Port",
                        callerName: nameof(SaveDatabaseSettingsAsync),
                        controlName: nameof(Control_Database));
                    return;
                }

                if (string.IsNullOrWhiteSpace(Control_Database_TextBox_Database.Text))
                {
                    Service_ErrorHandler.HandleValidationError("Database name is required.", "Database",
                        callerName: nameof(SaveDatabaseSettingsAsync),
                        controlName: nameof(Control_Database));
                    return;
                }

                string user = Model_AppVariables.User;

                var serverResult = await Dao_User.SetWipServerAddressAsync(user, Control_Database_TextBox_Server.Text.Trim());
                var databaseResult = await Dao_User.SetDatabaseAsync(user, Control_Database_TextBox_Database.Text.Trim());
                var portResult = await Dao_User.SetWipServerPortAsync(user, Control_Database_TextBox_Port.Text.Trim());

                if (serverResult.IsSuccess && databaseResult.IsSuccess && portResult.IsSuccess)
                {
                    DatabaseSettingsUpdated?.Invoke(this, EventArgs.Empty);
                    StatusMessageChanged?.Invoke(this,
                        "Database settings saved successfully. Restart application for changes to take effect.");
                }
                else
                {
                    // Handle database errors gracefully
                    var errors = new List<string>();
                    if (!serverResult.IsSuccess) errors.Add($"Server: {serverResult.ErrorMessage}");
                    if (!databaseResult.IsSuccess) errors.Add($"Database: {databaseResult.ErrorMessage}");
                    if (!portResult.IsSuccess) errors.Add($"Port: {portResult.ErrorMessage}");

                    Service_ErrorHandler.HandleDatabaseError(
                        new Exception($"Failed to save database settings: {string.Join("; ", errors)}"),
                        contextData: new Dictionary<string, object>
                        {
                            ["User"] = user,
                            ["Server"] = Control_Database_TextBox_Server.Text.Trim(),
                            ["Port"] = Control_Database_TextBox_Port.Text.Trim(),
                            ["Database"] = Control_Database_TextBox_Database.Text.Trim()
                        },
                        callerName: nameof(SaveDatabaseSettingsAsync),
                        controlName: nameof(Control_Database)
                    );

                    StatusMessageChanged?.Invoke(this, $"Error saving database settings: {string.Join("; ", errors)}");
                }
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object>
                    {
                        ["User"] = Model_AppVariables.User,
                        ["Server"] = Control_Database_TextBox_Server.Text.Trim(),
                        ["Port"] = Control_Database_TextBox_Port.Text.Trim(),
                        ["Database"] = Control_Database_TextBox_Database.Text.Trim()
                    },
                    callerName: nameof(SaveDatabaseSettingsAsync),
                    controlName: nameof(Control_Database));
            }
        }

        private async void SaveButton_Click(object? sender, EventArgs e)
        {
            try
            {
                Control_Database_Button_Save.Enabled = false;
                await SaveDatabaseSettingsAsync();
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object> { ["User"] = Model_AppVariables.User },
                    callerName: nameof(SaveButton_Click),
                    controlName: nameof(Control_Database));
            }
            finally
            {
                Control_Database_Button_Save.Enabled = true;
            }
        }

        private async void ResetButton_Click(object? sender, EventArgs e)
        {
            try
            {
                Control_Database_Button_Reset.Enabled = false;

                // Reset to default values
                Control_Database_TextBox_Server.Text = "localhost";
                Control_Database_TextBox_Port.Text = "3306";
                Control_Database_TextBox_Database.Text = "MTM_WIP_Application_Winforms";

                StatusMessageChanged?.Invoke(this, "Database settings reset to defaults.");
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium,
                    contextData: new Dictionary<string, object> { ["User"] = Model_AppVariables.User },
                    callerName: nameof(ResetButton_Click),
                    controlName: nameof(Control_Database));
            }
            finally
            {
                Control_Database_Button_Reset.Enabled = true;
            }
        }
    }
}
