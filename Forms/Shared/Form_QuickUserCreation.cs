using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.Logging;

namespace MTM_WIP_Application_Winforms.Forms.Shared
{
    /// <summary>
    /// Quick user creation form shown at startup when current Windows user is not found in database.
    /// Simplified version of Control_Add_User with fixed Normal User role.
    /// </summary>
    public partial class Form_QuickUserCreation : ThemedForm
    {
        #region Fields

        private const int NORMAL_USER_ROLE_ID = 3;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Form_QuickUserCreation form.
        /// </summary>
        public Form_QuickUserCreation()
        {
            InitializeComponent();
            InitializeFormSettings();
        }

        #endregion

        #region Initialization

        private void InitializeFormSettings()
        {
            // Set form properties
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Wire up key press events to prevent spaces
            Form_QuickUserCreation_TextBox_FirstName.KeyPress += TextBox_NoSpaces_KeyPress;
            Form_QuickUserCreation_TextBox_LastName.KeyPress += TextBox_NoSpaces_KeyPress;
            Form_QuickUserCreation_TextBox_UserName.KeyPress += TextBox_NoSpaces_KeyPress;
            Form_QuickUserCreation_TextBox_Pin.KeyPress += TextBox_NoSpaces_KeyPress;
            Form_QuickUserCreation_TextBox_VisualUserName.KeyPress += TextBox_NoSpaces_KeyPress;
            Form_QuickUserCreation_TextBox_VisualPassword.KeyPress += TextBox_NoSpaces_KeyPress;

            // Set password fields
            Form_QuickUserCreation_TextBox_Pin.UseSystemPasswordChar = true;
            Form_QuickUserCreation_TextBox_VisualPassword.UseSystemPasswordChar = true;

            // Disable Visual fields by default
            Form_QuickUserCreation_TextBox_VisualUserName.Enabled = false;
            Form_QuickUserCreation_TextBox_VisualPassword.Enabled = false;

            // Wire up checkbox events
            Form_QuickUserCreation_CheckBox_VisualAccess.CheckedChanged += CheckBox_VisualAccess_CheckedChanged;
            Form_QuickUserCreation_CheckBox_ViewPasswords.CheckedChanged += CheckBox_ViewPasswords_CheckedChanged;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            // Populate shift combo box
            Form_QuickUserCreation_ComboBox_Shift.Items.Clear();
            Form_QuickUserCreation_ComboBox_Shift.Items.AddRange(new object[]
            {
                "Select Shift",
                "First",
                "Second",
                "Third",
                "Weekend"
            });
            Form_QuickUserCreation_ComboBox_Shift.SelectedIndex = 0;

            // Set focus to first name
            Form_QuickUserCreation_TextBox_FirstName.Focus();

            LoggingUtility.Log($"[Form_QuickUserCreation] Form loaded for user: {Model_Application_Variables.User}");
        }

        #endregion

        #region Event Handlers

        private void TextBox_NoSpaces_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true;
            }
        }

        private void CheckBox_ViewPasswords_CheckedChanged(object? sender, EventArgs e)
        {
            bool show = Form_QuickUserCreation_CheckBox_ViewPasswords.Checked;
            Form_QuickUserCreation_TextBox_Pin.UseSystemPasswordChar = !show;
            Form_QuickUserCreation_TextBox_VisualPassword.UseSystemPasswordChar = !show;
        }

        private void CheckBox_VisualAccess_CheckedChanged(object? sender, EventArgs e)
        {
            bool enabled = Form_QuickUserCreation_CheckBox_VisualAccess.Checked;
            Form_QuickUserCreation_TextBox_VisualUserName.Enabled = enabled;
            Form_QuickUserCreation_TextBox_VisualPassword.Enabled = enabled;

            if (!enabled)
            {
                Form_QuickUserCreation_TextBox_VisualUserName.Clear();
                Form_QuickUserCreation_TextBox_VisualPassword.Clear();
            }
        }

        private async void Form_QuickUserCreation_Button_CreateUser_Click(object sender, EventArgs e)
        {
            await CreateUserAsync();
        }

        private void Form_QuickUserCreation_Button_Exit_Click(object sender, EventArgs e)
        {
            LoggingUtility.Log("[Form_QuickUserCreation] User chose to exit application");
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion

        #region Methods

        private async Task CreateUserAsync()
        {
            string userName = string.Empty;

            try
            {
                // Disable buttons during operation
                Form_QuickUserCreation_Button_CreateUser.Enabled = false;
                Form_QuickUserCreation_Button_Exit.Enabled = false;

                // Validate inputs
                if (string.IsNullOrWhiteSpace(Form_QuickUserCreation_TextBox_FirstName.Text))
                {
                    Service_ErrorHandler.HandleValidationError("First name is required", "First Name",
                        callerName: nameof(CreateUserAsync), controlName: this.Name);
                    Form_QuickUserCreation_TextBox_FirstName.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(Form_QuickUserCreation_TextBox_LastName.Text))
                {
                    Service_ErrorHandler.HandleValidationError("Last name is required", "Last Name",
                        callerName: nameof(CreateUserAsync), controlName: this.Name);
                    Form_QuickUserCreation_TextBox_LastName.Focus();
                    return;
                }

                if (Form_QuickUserCreation_ComboBox_Shift.SelectedIndex <= 0)
                {
                    Service_ErrorHandler.HandleValidationError("Please select a shift", "Shift",
                        callerName: nameof(CreateUserAsync), controlName: this.Name);
                    Form_QuickUserCreation_ComboBox_Shift.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(Form_QuickUserCreation_TextBox_UserName.Text))
                {
                    Service_ErrorHandler.HandleValidationError("User name is required", "User Name",
                        callerName: nameof(CreateUserAsync), controlName: this.Name);
                    Form_QuickUserCreation_TextBox_UserName.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(Form_QuickUserCreation_TextBox_Pin.Text))
                {
                    Service_ErrorHandler.HandleValidationError("PIN is required", "PIN",
                        callerName: nameof(CreateUserAsync), controlName: this.Name);
                    Form_QuickUserCreation_TextBox_Pin.Focus();
                    return;
                }

                userName = Form_QuickUserCreation_TextBox_UserName.Text.ToUpper();

                LoggingUtility.Log($"[Form_QuickUserCreation] Creating user: {userName}");

                // Check if user already exists
                var userExistsResult = await Dao_User.UserExistsAsync(userName);

                if (!userExistsResult.IsSuccess)
                {
                    if (userExistsResult.Exception != null)
                    {
                        Service_ErrorHandler.HandleDatabaseError(userExistsResult.Exception,
                            contextData: new Dictionary<string, object> { ["UserName"] = userName },
                            callerName: nameof(CreateUserAsync),
                            controlName: this.Name);
                    }
                    Service_ErrorHandler.ShowUserError($"Error checking user existence: {userExistsResult.ErrorMessage}");
                    return;
                }

                if (userExistsResult.Data)
                {
                    Service_ErrorHandler.ShowUserError("User already exists");
                    Form_QuickUserCreation_TextBox_UserName.Focus();
                    return;
                }

                // Prepare user data
                string fullName = Form_QuickUserCreation_TextBox_FirstName.Text + " " +
                                  Form_QuickUserCreation_TextBox_LastName.Text;
                string lastShownVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? string.Empty;

                // Use default connection values for new user
                string wipServerAddress = string.IsNullOrWhiteSpace(Model_Shared_Users.WipServerAddress) ? "172.16.1.104" : Model_Shared_Users.WipServerAddress;
                string database = string.IsNullOrWhiteSpace(Model_Shared_Users.Database) ? "MTM_WIP_Application_Winforms" : Model_Shared_Users.Database;
                string wipServerPort = string.IsNullOrWhiteSpace(Model_Shared_Users.WipServerPort) ? "3306" : Model_Shared_Users.WipServerPort;

                // Set Visual credentials - use defaults if not entered
                string visualUserName = "SHOP2";
                string visualPassword = "SHOP";

                if (Form_QuickUserCreation_CheckBox_VisualAccess.Checked)
                {
                    if (!string.IsNullOrWhiteSpace(Form_QuickUserCreation_TextBox_VisualUserName.Text))
                    {
                        visualUserName = Form_QuickUserCreation_TextBox_VisualUserName.Text;
                    }

                    if (!string.IsNullOrWhiteSpace(Form_QuickUserCreation_TextBox_VisualPassword.Text))
                    {
                        visualPassword = Form_QuickUserCreation_TextBox_VisualPassword.Text;
                    }
                }

                // Create user
                var createUserResult = await Dao_User.CreateUserAsync(
                    userName,
                    fullName,
                    Form_QuickUserCreation_ComboBox_Shift.Text,
                    false, // VitsUser
                    Form_QuickUserCreation_TextBox_Pin.Text,
                    lastShownVersion,
                    "false", // HideChangeLog
                    "Default", // Theme_Name
                    9, // Theme_FontSize
                    visualUserName,
                    visualPassword,
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
                            callerName: nameof(CreateUserAsync),
                            controlName: this.Name);
                    }
                    Service_ErrorHandler.ShowUserError($"Error creating user: {createUserResult.ErrorMessage}");
                    return;
                }

                // Get the created user to retrieve the ID
                var getUserResult = await Dao_User.GetUserByUsernameAsync(userName);

                if (!getUserResult.IsSuccess || getUserResult.Data == null)
                {
                    Service_ErrorHandler.ShowUserError("Could not retrieve new user ID after creation");
                    return;
                }

                // Get user ID
                int userId = Convert.ToInt32(getUserResult.Data["ID"]);

                // Assign Normal User role (roleId = 3)
                var addRoleResult = await Dao_User.AddUserRoleAsync(userId, NORMAL_USER_ROLE_ID, Environment.UserName);

                if (!addRoleResult.IsSuccess)
                {
                    if (addRoleResult.Exception != null)
                    {
                        Service_ErrorHandler.HandleDatabaseError(addRoleResult.Exception,
                            contextData: new Dictionary<string, object> { ["UserId"] = userId, ["RoleId"] = NORMAL_USER_ROLE_ID },
                            callerName: nameof(CreateUserAsync),
                            controlName: this.Name);
                    }
                    Service_ErrorHandler.ShowUserError($"User created but role assignment failed: {addRoleResult.ErrorMessage}");
                    // Note: User was created successfully, but role assignment failed
                }

                LoggingUtility.Log($"[Form_QuickUserCreation] User created successfully: {fullName} ({userName})");

                Service_ErrorHandler.ShowInformation($"User '{fullName}' created successfully!\n\nThe application will now continue loading.", "User Created", controlName: this.Name);

                // Update Model_Application_Variables to use the new user
                Model_Application_Variables.User = userName;

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.High,
                    contextData: new Dictionary<string, object> { ["UserName"] = userName },
                    callerName: nameof(CreateUserAsync),
                    controlName: this.Name);
                Service_ErrorHandler.ShowUserError($"Unexpected error creating user: {ex.Message}");
            }
            finally
            {
                Form_QuickUserCreation_Button_CreateUser.Enabled = true;
                Form_QuickUserCreation_Button_Exit.Enabled = true;
            }
        }

        #endregion
    }
}
