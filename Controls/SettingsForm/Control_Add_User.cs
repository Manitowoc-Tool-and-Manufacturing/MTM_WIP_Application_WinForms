using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Forms.Shared;

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    public partial class Control_Add_User : ThemedUserControl
    {
        #region Events

        public event EventHandler? UserAdded;

        #endregion

        #region Fields

        private Helper_StoredProcedureProgress? _progressHelper;

        #endregion

        #region Constructors

        public Control_Add_User()
        {
            Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
            {
                ["ControlType"] = nameof(Control_Add_User),
                ["InitializationTime"] = DateTime.Now,
                ["Thread"] = Thread.CurrentThread.ManagedThreadId
            }, nameof(Control_Add_User), nameof(Control_Add_User));

            Service_DebugTracer.TraceUIAction("ADD_USER_CONTROL_INITIALIZATION", nameof(Control_Add_User),
                new Dictionary<string, object>
                {
                    ["Phase"] = "START",
                    ["ComponentType"] = "UserControl"
                });

            InitializeComponent();

            Service_DebugTracer.TraceUIAction("THEME_APPLICATION", nameof(Control_Add_User),
                new Dictionary<string, object>
                {
                    ["DpiScaling"] = "APPLIED",
                    ["LayoutAdjustments"] = "APPLIED"
                });

            Service_DebugTracer.TraceUIAction("DEFAULT_USER_TYPE_SET", nameof(Control_Add_User),
                new Dictionary<string, object>
                {
                    ["UserType"] = "NormalUser",
                    ["DefaultSelection"] = true
                });
            Control_Add_User_RadioButton_NormalUser.Checked = true;

            // Wire up Developer radio button (roleId = 4)
            Service_DebugTracer.TraceUIAction("DEVELOPER_ROLE_WIRED", nameof(Control_Add_User),
                new Dictionary<string, object>
                {
                    ["RoleId"] = 4,
                    ["RoleType"] = "Developer"
                });

            Service_DebugTracer.TraceUIAction("KEYPRESS_EVENTS_SETUP", nameof(Control_Add_User),
                new Dictionary<string, object>
                {
                    ["TextBoxes"] = new[] { "FirstName", "LastName", "UserName", "Pin", "VisualUserName", "VisualPassword" },
                    ["KeyPressHandler"] = "NoSpaces"
                });
            Control_Add_User_TextBox_FirstName.KeyPress += Control_Add_User_TextBox_NoSpaces_KeyPress;
            Control_Add_User_TextBox_LastName.KeyPress += Control_Add_User_TextBox_NoSpaces_KeyPress;
            Control_Add_User_TextBox_UserName.KeyPress += Control_Add_User_TextBox_NoSpaces_KeyPress;
            Control_Add_User_TextBox_Pin.KeyPress += Control_Add_User_TextBox_NoSpaces_KeyPress;
            Control_Add_User_TextBox_VisualUserName.KeyPress += Control_Add_User_TextBox_NoSpaces_KeyPress;
            Control_Add_User_TextBox_VisualPassword.KeyPress += Control_Add_User_TextBox_NoSpaces_KeyPress;

            Service_DebugTracer.TraceUIAction("PASSWORD_FIELDS_SETUP", nameof(Control_Add_User),
                new Dictionary<string, object>
                {
                    ["PasswordFields"] = new[] { "Pin", "VisualPassword" },
                    ["UseSystemPasswordChar"] = true,
                    ["VisualFieldsEnabled"] = false
                });
            Control_Add_User_TextBox_Pin.UseSystemPasswordChar = true;
            Control_Add_User_TextBox_VisualPassword.UseSystemPasswordChar = true;
            Control_Add_User_TextBox_VisualUserName.Enabled = false;
            Control_Add_User_TextBox_VisualPassword.Enabled = false;

            Service_DebugTracer.TraceUIAction("VISUAL_ACCESS_EVENT_SETUP", nameof(Control_Add_User),
                new Dictionary<string, object>
                {
                    ["CheckBoxEvent"] = "VisualAccess.CheckedChanged"
                });
            Control_Add_User_CheckBox_VisualAccess.CheckedChanged +=
                Control_Add_User_CheckBox_VisualAccess_CheckedChanged;

            Service_DebugTracer.TraceUIAction("VIEW_PASSWORDS_EVENT_SETUP", nameof(Control_Add_User),
                new Dictionary<string, object>
                {
                    ["CheckBoxEvent"] = "ViewHidePasswords.CheckedChanged"
                });
            Control_Add_User_CheckBox_ViewHidePasswords.CheckedChanged +=
                Control_Add_User_CheckBox_ViewHidePasswords_CheckedChanged;

            Service_DebugTracer.TraceUIAction("ADD_USER_CONTROL_INITIALIZATION", nameof(Control_Add_User),
                new Dictionary<string, object>
                {
                    ["Phase"] = "COMPLETE",
                    ["Success"] = true
                });

            Service_DebugTracer.TraceMethodExit(null, nameof(Control_Add_User), nameof(Control_Add_User));
        }

        #endregion

        #region Public Methods

        public void SetProgressControls(ToolStripProgressBar progressBar, ToolStripStatusLabel statusLabel)
        {
            _progressHelper = Helper_StoredProcedureProgress.Create(progressBar, statusLabel,
                this.FindForm() ?? throw new InvalidOperationException("Control must be added to a form"));
        }

        #endregion

        #region Initialization

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Control_Add_User_ComboBox_Shift.Items.Clear();
            Control_Add_User_ComboBox_Shift.Items.AddRange([
                "Enter or Select Shift", "First", "Second", "Third", "Weekend"
            ]);
            Control_Add_User_ComboBox_Shift.SelectedIndex = 0;
        }

        #endregion

        #region Event Handlers

        private void Control_Add_User_TextBox_NoSpaces_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true;
            }
        }

        private void Control_Add_User_CheckBox_ViewHidePasswords_CheckedChanged(object? sender, EventArgs e)
        {
            bool show = Control_Add_User_CheckBox_ViewHidePasswords.Checked;
            Control_Add_User_TextBox_Pin.UseSystemPasswordChar = !show;
            Control_Add_User_TextBox_VisualPassword.UseSystemPasswordChar = !show;
        }

        private void Control_Add_User_CheckBox_VisualAccess_CheckedChanged(object? sender, EventArgs e)
        {
            bool enabled = Control_Add_User_CheckBox_VisualAccess.Checked;
            Control_Add_User_TextBox_VisualUserName.Enabled = enabled;
            Control_Add_User_TextBox_VisualPassword.Enabled = enabled;
            if (!enabled)
            {
                Control_Add_User_TextBox_VisualUserName.Clear();
                Control_Add_User_TextBox_VisualPassword.Clear();
            }
        }

        private async void Control_Add_User_Button_Save_Click(object sender, EventArgs e)
        {
            string userName = string.Empty; // Declare outside try block for catch access

            try
            {
                Control_Add_User_Button_Save.Enabled = false;

                ShowProgress("Initializing user creation...");
                UpdateProgress(5, "Preparing to create new user...");
                await Task.Delay(100);

                UpdateProgress(10, "Validating form data...");
                await Task.Delay(50);

                if (string.IsNullOrWhiteSpace(Control_Add_User_TextBox_FirstName.Text))
                {
                    ShowError("First name is required");
                    Control_Add_User_TextBox_FirstName.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(Control_Add_User_TextBox_LastName.Text))
                {
                    ShowError("Last name is required");
                    Control_Add_User_TextBox_LastName.Focus();
                    return;
                }

                if (Control_Add_User_ComboBox_Shift.SelectedIndex <= 0)
                {
                    ShowError("Please select a shift");
                    Control_Add_User_ComboBox_Shift.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(Control_Add_User_TextBox_UserName.Text))
                {
                    ShowError("User name is required");
                    Control_Add_User_TextBox_UserName.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(Control_Add_User_TextBox_Pin.Text))
                {
                    ShowError("Pin is required");
                    Control_Add_User_TextBox_Pin.Focus();
                    return;
                }

                userName = Control_Add_User_TextBox_UserName.Text.ToUpper();

                UpdateProgress(20, "Checking for existing user...");
                await Task.Delay(100);

                // Use enhanced DAO method with Model_Dao_Result pattern
                var userExistsResult = await Dao_User.UserExistsAsync(userName);

                if (!userExistsResult.IsSuccess)
                {
                    if (userExistsResult.Exception != null)
                    {
                        Service_ErrorHandler.HandleDatabaseError(userExistsResult.Exception,
                            contextData: new Dictionary<string, object> { ["UserName"] = userName },
                            callerName: nameof(Control_Add_User_Button_Save_Click),
                            controlName: nameof(Control_Add_User));
                    }
                    ShowError($"Error checking user existence: {userExistsResult.ErrorMessage}");
                    return;
                }

                if (userExistsResult.Data)
                {
                    ShowError("User already exists");
                    Control_Add_User_TextBox_UserName.Focus();
                    return;
                }

                UpdateProgress(30, "Processing user information...");
                string fullName = Control_Add_User_TextBox_FirstName.Text + " " +
                                  Control_Add_User_TextBox_LastName.Text;
                await Task.Delay(100);

                UpdateProgress(40, "Creating user account...");

                // Use DAO method with Model_Dao_Result pattern
                string lastShownVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? string.Empty;

                // Use default connection values for new user (they can change them later in settings)
                string wipServerAddress = string.IsNullOrWhiteSpace(Model_Shared_Users.WipServerAddress) ? "localhost" : Model_Shared_Users.WipServerAddress;
                string database = string.IsNullOrWhiteSpace(Model_Shared_Users.Database) ? "MTM_WIP_Application_Winforms" : Model_Shared_Users.Database;
                string wipServerPort = string.IsNullOrWhiteSpace(Model_Shared_Users.WipServerPort) ? "3306" : Model_Shared_Users.WipServerPort;

                var createUserResult = await Dao_User.CreateUserAsync(
                    userName,
                    fullName,
                    Control_Add_User_ComboBox_Shift.Text,
                    false, // VitsUser
                    Control_Add_User_TextBox_Pin.Text,
                    lastShownVersion,
                    "false", // HideChangeLog
                    "Default", // Theme_Name
                    9, // Theme_FontSize
                    Control_Add_User_TextBox_VisualUserName.Text,
                    Control_Add_User_TextBox_VisualPassword.Text,
                    wipServerAddress,
                    database,
                    wipServerPort
                );

                if (!createUserResult.IsSuccess)
                {
                    if (createUserResult.Exception != null)
                    {
                        Service_ErrorHandler.HandleDatabaseError(createUserResult.Exception,
                            contextData: new Dictionary<string, object> { ["UserName"] = userName },
                            callerName: nameof(Control_Add_User_Button_Save_Click),
                            controlName: nameof(Control_Add_User));
                    }
                    ShowError($"Error creating user: {createUserResult.ErrorMessage}");
                    return;
                }

                UpdateProgress(60, "Retrieving user information...");

                // Get the created user to retrieve the ID
                var getUserResult = await Dao_User.GetUserByUsernameAsync(userName);

                if (!getUserResult.IsSuccess || getUserResult.Data == null)
                {
                    ShowError("Could not retrieve new user ID after creation");
                    return;
                }

                UpdateProgress(70, "Processing user role assignment...");
                await Task.Delay(100);

                // Use correct column name 'ID' from usr_users table (not 'p_ID')
                int userId = Convert.ToInt32(getUserResult.Data["ID"]);
                int roleId = 3; // Default to Normal User
                if (Control_Add_User_RadioButton_Administrator.Checked)
                {
                    roleId = 1;
                }
                else if (Control_Add_User_RadioButton_ReadOnly.Checked)
                {
                    roleId = 2;
                }
                else if (Control_Add_User_RadioButton_Developer.Checked)
                {
                    roleId = 4;
                }

                UpdateProgress(80, "Assigning user role...");

                // Add user role assignment using DAO method
                var addRoleResult = await Dao_User.AddUserRoleAsync(userId, roleId, Environment.UserName);

                if (!addRoleResult.IsSuccess)
                {
                    if (addRoleResult.Exception != null)
                    {
                        Service_ErrorHandler.HandleDatabaseError(addRoleResult.Exception,
                            contextData: new Dictionary<string, object> { ["UserId"] = userId, ["RoleId"] = roleId },
                            callerName: nameof(Control_Add_User_Button_Save_Click),
                            controlName: nameof(Control_Add_User));
                    }
                    ShowError($"User created but role assignment failed: {addRoleResult.ErrorMessage}");
                    // Note: User was created successfully, but role assignment failed
                }

                UpdateProgress(90, "Finalizing user setup...");
                await Task.Delay(100);

                UpdateProgress(95, "Clearing form...");
                ClearForm();
                Control_Add_User_TextBox_UserName.Clear();

                ShowSuccess($"User '{fullName}' created successfully!");
                await Task.Delay(500);
                HideProgress();

                UserAdded?.Invoke(this, EventArgs.Empty);
                UpdateStatus($"User '{fullName}' added successfully!");

                Service_ErrorHandler.ShowInformation($"User '{fullName}' created successfully!", "Success");
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex,
                    contextData: new Dictionary<string, object> { ["UserName"] = userName },
                    callerName: nameof(Control_Add_User_Button_Save_Click),
                    controlName: nameof(Control_Add_User));
                ShowError($"Unexpected error creating user: {ex.Message}");
                UpdateStatus($"Error adding user: {ex.Message}");
            }
            finally
            {
                Control_Add_User_Button_Save.Enabled = true;
            }
        }

        private void Control_Add_User_Button_Clear_Click(object sender, EventArgs e) => ClearForm();

        #endregion

        #region Methods

        private void ClearForm()
        {
            Control_Add_User_TextBox_FirstName.Clear();
            Control_Add_User_TextBox_LastName.Clear();
            Control_Add_User_ComboBox_Shift.SelectedIndex = 0;
            Control_Add_User_TextBox_Pin.Clear();
            Control_Add_User_CheckBox_VisualAccess.Checked = false;
            Control_Add_User_TextBox_VisualUserName.Clear();
            Control_Add_User_TextBox_VisualPassword.Clear();
            Control_Add_User_RadioButton_ReadOnly.Checked = false;
            Control_Add_User_RadioButton_NormalUser.Checked = true;
            Control_Add_User_RadioButton_Administrator.Checked = false;
            Control_Add_User_TextBox_FirstName.Focus();
            Control_Add_User_TextBox_VisualUserName.Enabled = false;
            Control_Add_User_TextBox_VisualPassword.Enabled = false;
        }

        #endregion

        #region Progress Methods

        private void ShowProgress(string status = "Loading...")
        {
            if (_progressHelper != null)
            {
                _progressHelper.ShowProgress(status);
            }
            // No fallback needed for just showing progress spinner
        }

        private void UpdateProgress(int progress, string status)
        {
            if (_progressHelper != null)
            {
                _progressHelper.UpdateProgress(progress, status);
            }
        }

        private void HideProgress()
        {
            if (_progressHelper != null)
            {
                _progressHelper.HideProgress();
            }
        }

        private void UpdateStatus(string message)
        {
            if (_progressHelper != null)
            {
                _progressHelper.UpdateStatus(message);
            }
        }

        private void ShowError(string errorMessage)
        {
            if (_progressHelper != null)
            {
                _progressHelper.ShowError(errorMessage);
            }
            else
            {
                // Fallback if progress helper is not initialized
                Service_ErrorHandler.ShowWarning(errorMessage);
            }
        }

        private void ShowSuccess(string successMessage)
        {
            if (_progressHelper != null)
            {
                _progressHelper.ShowSuccess(successMessage);
            }
            else
            {
                // Fallback if progress helper is not initialized
                Service_ErrorHandler.ShowWarning(successMessage);
            }
        }

        #endregion
    }
}
