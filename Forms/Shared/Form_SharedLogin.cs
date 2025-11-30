using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Forms.Shared
{
    /// <summary>
    /// Modal dialog for authenticating users on shared workstations (SHOP2/MTMDC).
    /// </summary>
    public partial class Form_SharedLogin : ThemedForm
    {
        #region Fields

        private int _attempts = 0;
        private const int MaxAttempts = 3;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the validated username after successful login.
        /// </summary>
        public string ValidatedUsername { get; private set; } = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Form_SharedLogin"/> class.
        /// </summary>
        public Form_SharedLogin()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handles the Login button click event.
        /// </summary>
        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                buttonLogin.Enabled = false;
                buttonCancel.Enabled = false;

                // 1. Validate Input
                string username = textBoxUsername.Text.Trim();
                string pin = textBoxPin.Text.Trim();

                var userValidation = Service_Validation_Core.ValidateRequired(username, "Username");
                if (!userValidation.IsValid)
                {
                    Service_ErrorHandler.ShowUserError(userValidation.ErrorMessage);
                    textBoxUsername.Focus();
                    return;
                }

                var pinValidation = Service_Validation_Core.ValidateRequired(pin, "PIN");
                if (!pinValidation.IsValid)
                {
                    Service_ErrorHandler.ShowUserError(pinValidation.ErrorMessage);
                    textBoxPin.Focus();
                    return;
                }

                // 2. Verify Credentials
                var userResult = await Dao_User.GetUserByUsernameAsync(username);
                if (!userResult.IsSuccess || userResult.Data == null)
                {
                    HandleFailedLogin("Invalid username or PIN.");
                    return;
                }

                // Check PIN
                // Assuming PIN is stored in "Pin" column
                string storedPin = userResult.Data["Pin"]?.ToString() ?? string.Empty;
                
                // Simple string comparison (assuming plain text PIN as per legacy DB)
                if (storedPin != pin)
                {
                    HandleFailedLogin("Invalid username or PIN.");
                    return;
                }

                // 3. Success
                ValidatedUsername = username;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.High,
                    contextData: new Dictionary<string, object> { ["MethodName"] = "buttonLogin_Click" },
                    callerName: "Form_SharedLogin_Login",
                    controlName: "Form_SharedLogin");
            }
            finally
            {
                buttonLogin.Enabled = true;
                buttonCancel.Enabled = true;
            }
        }

        /// <summary>
        /// Handles failed login attempts and lockout logic.
        /// </summary>
        private void HandleFailedLogin(string message)
        {
            _attempts++;
            if (_attempts >= MaxAttempts)
            {
                Service_ErrorHandler.ShowUserError("Maximum login attempts exceeded. Application will close.");
                this.DialogResult = DialogResult.Abort;
                this.Close();
            }
            else
            {
                Service_ErrorHandler.ShowUserError($"{message}\nAttempts remaining: {MaxAttempts - _attempts}");
                textBoxPin.Clear();
                textBoxPin.Focus();
            }
        }

        /// <summary>
        /// Handles the Cancel button click event.
        /// </summary>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion
    }
}
