using System.Data;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    public partial class Control_Edit_User : ThemedUserControl
    {
        #region Fields

        #region Events

        public event EventHandler? UserEdited;

        #endregion

        #region Constructors

        public Control_Edit_User()
        {
            Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
            {
                ["ControlType"] = nameof(Control_Edit_User),
                ["InitializationTime"] = DateTime.Now,
                ["Thread"] = Thread.CurrentThread.ManagedThreadId
            }, nameof(Control_Edit_User), nameof(Control_Edit_User));

            Service_DebugTracer.TraceUIAction("EDIT_USER_INITIALIZATION", nameof(Control_Edit_User),
                new Dictionary<string, object>
                {
                    ["Phase"] = "START",
                    ["ComponentType"] = "UserControl"
                });

            InitializeComponent();

            Service_DebugTracer.TraceUIAction("THEME_APPLICATION", nameof(Control_Edit_User),
                new Dictionary<string, object>
                {
                    ["DpiScaling"] = "APPLIED",
                    ["LayoutAdjustments"] = "APPLIED"
                });

            Service_DebugTracer.TraceUIAction("KEYPRESS_EVENTS_SETUP", nameof(Control_Edit_User),
                new Dictionary<string, object>
                {
                    ["TextBoxes"] = new[] { "FirstName", "LastName", "Pin", "VisualUserName", "VisualPassword" },
                    ["KeyPressHandler"] = "NoSpaces"
                });
            Control_Edit_User_TextBox_FirstName.KeyPress += Control_Edit_User_TextBox_NoSpaces_KeyPress;
            Control_Edit_User_TextBox_LastName.KeyPress += Control_Edit_User_TextBox_NoSpaces_KeyPress;
            Control_Edit_User_TextBox_Pin.KeyPress += Control_Edit_User_TextBox_NoSpaces_KeyPress;
            Control_Edit_User_TextBox_VisualUserName.KeyPress += Control_Edit_User_TextBox_NoSpaces_KeyPress;
            Control_Edit_User_TextBox_VisualPassword.KeyPress += Control_Edit_User_TextBox_NoSpaces_KeyPress;

            Service_DebugTracer.TraceUIAction("PASSWORD_FIELDS_SETUP", nameof(Control_Edit_User),
                new Dictionary<string, object>
                {
                    ["PasswordFields"] = new[] { "Pin", "VisualPassword" },
                    ["UseSystemPasswordChar"] = true,
                    ["VisualUserNameEnabled"] = false
                });
            Control_Edit_User_TextBox_Pin.UseSystemPasswordChar = true;
            Control_Edit_User_TextBox_VisualPassword.UseSystemPasswordChar = true;
            Control_Edit_User_TextBox_VisualUserName.Enabled = false;
            Control_Edit_User_TextBox_VisualPassword.Enabled = false;
            Control_Edit_User_CheckBox_VisualAccess.CheckedChanged +=
                Control_Edit_User_CheckBox_VisualAccess_CheckedChanged;
            Control_Edit_User_CheckBox_ViewHidePasswords.CheckedChanged +=
                Control_Edit_User_CheckBox_ViewHidePasswords_CheckedChanged;
            Control_Edit_User_ComboBox_Users.SelectedIndexChanged +=
                Control_Edit_User_ComboBox_Users_SelectedIndexChanged;
            Control_Edit_User_Button_Save.Click += Control_Edit_User_Button_Save_Click;
            Control_Edit_User_Button_Clear.Click += Control_Edit_User_Button_Clear_Click;
        }

        #endregion

        #region Initialization

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Control_Edit_User_ComboBox_Shift.Items.Clear();
            Control_Edit_User_ComboBox_Shift.Items.AddRange(new object[] { "First", "Second", "Third", "Weekend" });
            LoadUsersAsync();
        }

        private async void LoadUsersAsync()
        {
            try
            {
                await Helper_UI_ComboBoxes.FillUserComboBoxesAsync(Control_Edit_User_ComboBox_Users);
            }
            catch (Exception e)
            {
                LoggingUtility.LogApplicationError(e);
                Service_ErrorHandler.ShowWarning($"Error loading users: {e.Message}");
            }
        }

        #endregion

        #region Event Handlers

        private void Control_Edit_User_TextBox_NoSpaces_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true;
            }
        }

        private void Control_Edit_User_CheckBox_ViewHidePasswords_CheckedChanged(object? sender, EventArgs e)
        {
            bool show = Control_Edit_User_CheckBox_ViewHidePasswords.Checked;
            Control_Edit_User_TextBox_Pin.UseSystemPasswordChar = !show;
            Control_Edit_User_TextBox_VisualPassword.UseSystemPasswordChar = !show;
        }

        private void Control_Edit_User_CheckBox_VisualAccess_CheckedChanged(object? sender, EventArgs e)
        {
            bool enabled = Control_Edit_User_CheckBox_VisualAccess.Checked;
            Control_Edit_User_TextBox_VisualUserName.Enabled = enabled;
            Control_Edit_User_TextBox_VisualPassword.Enabled = enabled;
            if (!enabled)
            {
                Control_Edit_User_TextBox_VisualUserName.Clear();
                Control_Edit_User_TextBox_VisualPassword.Clear();
            }
        }

        private async void Control_Edit_User_ComboBox_Users_SelectedIndexChanged(object? sender, EventArgs e)
        {
            // Skip if placeholder is selected
            if (Control_Edit_User_ComboBox_Users.SelectedIndex <= 0)
            {
                ClearForm();
                return;
            }

            // Extract username from DataRowView properly - don't use .Text with data-bound ComboBoxes
            string? userName = null;
            if (Control_Edit_User_ComboBox_Users.SelectedItem is DataRowView drv)
            {
                userName = drv["User"]?.ToString();
            }

            if (!string.IsNullOrWhiteSpace(userName) && !userName.StartsWith("["))
            {
                var userResult = await Dao_User.GetUserByUsernameAsync(userName);
                if (!userResult.IsSuccess || userResult.Data == null )
                {
                    Service_ErrorHandler.ShowWarning($"Error loading user: {userResult.ErrorMessage}");
                    if (userResult.Exception != null)
                        LoggingUtility.LogApplicationError(userResult.Exception);
                    return;
                }

                DataRow userRow = userResult.Data;
                string[] names = (userRow["Full Name"]?.ToString() ?? "").Split(' ');
                Control_Edit_User_TextBox_FirstName.Text = names.Length > 0 ? names[0] : "";
                Control_Edit_User_TextBox_LastName.Text = names.Length > 1 ? string.Join(" ", names.Skip(1)) : "";
                Control_Edit_User_TextBox_UserName.Text = userRow["User"]?.ToString() ?? "";
                Control_Edit_User_ComboBox_Shift.Text = userRow["Shift"]?.ToString() ?? "First";
                Control_Edit_User_TextBox_Pin.Text = userRow["Pin"]?.ToString() ?? "";
                Control_Edit_User_CheckBox_VisualAccess.Checked =
                    !string.IsNullOrWhiteSpace(userRow["VisualUserName"]?.ToString());
                Control_Edit_User_TextBox_VisualUserName.Text = userRow["VisualUserName"]?.ToString() ?? "";
                Control_Edit_User_TextBox_VisualPassword.Text = userRow["VisualPassword"]?.ToString() ?? "";

                // Use correct column name 'ID' from usr_users table (not 'p_ID')
                int userId = Convert.ToInt32(userRow["ID"]);
                var roleResult = await Dao_User.GetUserRoleIdAsync(userId);
                if (roleResult.IsSuccess)
                {
                    int roleId = roleResult.Data;
                    Control_Edit_User_RadioButton_NormalUser.Checked = roleId == 3;
                    Control_Edit_User_RadioButton_Administrator.Checked = roleId == 1;
                    Control_Edit_User_RadioButton_ReadOnly.Checked = roleId == 2;
                    Control_Edit_User_RadioButton_Developer.Checked = roleId == 4;
                }
            }
        }

        private async void Control_Edit_User_Button_Save_Click(object? sender, EventArgs e)
        {
            try
            {
                Control_Edit_User_Button_Save.Enabled = false;

                string userName = Control_Edit_User_ComboBox_Users.Text;
                if (string.IsNullOrWhiteSpace(userName) || userName.StartsWith("[") || Control_Edit_User_ComboBox_Users.SelectedIndex <= 0)
                {
                    Service_ErrorHandler.ShowWarning("Please select a user to edit.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(Control_Edit_User_TextBox_FirstName.Text))
                {
                    Service_ErrorHandler.ShowWarning("First name is required.");
                    Control_Edit_User_TextBox_FirstName.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(Control_Edit_User_TextBox_LastName.Text))
                {
                    Service_ErrorHandler.ShowWarning("Last name is required.");
                    Control_Edit_User_TextBox_LastName.Focus();
                    return;
                }

                if (Control_Edit_User_ComboBox_Shift.SelectedIndex <= -1 || Control_Edit_User_ComboBox_Shift.Text.StartsWith("Enter"))
                {
                    Service_ErrorHandler.ShowWarning("Please select a shift.");
                    Control_Edit_User_ComboBox_Shift.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(Control_Edit_User_TextBox_Pin.Text))
                {
                    Service_ErrorHandler.ShowWarning("Pin is required.");
                    Control_Edit_User_TextBox_Pin.Focus();
                    return;
                }

                // Update user with Model_Dao_Result pattern
                var updateResult = await Dao_User.UpdateUserAsync(
                    userName,
                    Control_Edit_User_TextBox_FirstName.Text + " " + Control_Edit_User_TextBox_LastName.Text,
                    Control_Edit_User_ComboBox_Shift.Text,
                    Control_Edit_User_TextBox_Pin.Text,
                    Control_Edit_User_TextBox_VisualUserName.Text,
                    Control_Edit_User_TextBox_VisualPassword.Text
                );

                if (!updateResult.IsSuccess)
                {
                    Service_ErrorHandler.ShowWarning($"Error updating user: {updateResult.ErrorMessage}");
                    if (updateResult.Exception != null)
                        LoggingUtility.LogApplicationError(updateResult.Exception);
                    return;
                }

                // Get user ID and update role
                var userResult = await Dao_User.GetUserByUsernameAsync(userName);
                if (userResult.IsSuccess && userResult.Data != null && userResult.Data.Table.Columns.Contains("ID"))
                {
                    int userId = Convert.ToInt32(userResult.Data["ID"]);
                    int newRoleId = 3;
                    if (Control_Edit_User_RadioButton_Administrator.Checked)
                    {
                        newRoleId = 1;
                    }
                    else if (Control_Edit_User_RadioButton_ReadOnly.Checked)
                    {
                        newRoleId = 2;
                    }
                    else if (Control_Edit_User_RadioButton_Developer.Checked)
                    {
                        newRoleId = 4;
                    }

                    var roleResult = await Dao_User.SetUserRoleAsync(userId, newRoleId, Environment.UserName);
                    if (!roleResult.IsSuccess)
                    {
                        Service_ErrorHandler.ShowWarning($"User updated but role change failed: {roleResult.ErrorMessage}");
                        if (roleResult.Exception != null)
                            LoggingUtility.LogApplicationError(roleResult.Exception);
                        return;
                    }
                }

                UserEdited?.Invoke(this, EventArgs.Empty);
                Service_ErrorHandler.ShowInformation("User updated successfully!", "Success");
                ClearForm();
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.ShowWarning($"Error updating user: {ex.Message}");
            }
            finally
            {
                Control_Edit_User_Button_Save.Enabled = true;
            }
        }

        private void Control_Edit_User_Button_Clear_Click(object? sender, EventArgs e) => ClearForm();

        private void ClearForm()
        {
            Control_Edit_User_ComboBox_Users.SelectedIndex = -1;
            Control_Edit_User_TextBox_FirstName.Clear();
            Control_Edit_User_TextBox_LastName.Clear();
            Control_Edit_User_TextBox_UserName.Clear();
            Control_Edit_User_TextBox_Pin.Clear();
            Control_Edit_User_TextBox_VisualUserName.Clear();
            Control_Edit_User_TextBox_VisualPassword.Clear();

            if (Control_Edit_User_ComboBox_Shift.Items.Count > 0)
                Control_Edit_User_ComboBox_Shift.SelectedIndex = 0;

            Control_Edit_User_RadioButton_NormalUser.Checked = true;
            Control_Edit_User_CheckBox_VisualAccess.Checked = false;
            Control_Edit_User_CheckBox_ViewHidePasswords.Checked = false;

            // Reset visual fields state
            Control_Edit_User_TextBox_VisualUserName.Enabled = false;
            Control_Edit_User_TextBox_VisualPassword.Enabled = false;
        }

        #endregion
    }

    #endregion
}
