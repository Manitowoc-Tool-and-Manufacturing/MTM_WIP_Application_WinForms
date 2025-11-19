using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    public partial class Control_Add_Location : ThemedUserControl
    {
        #region Events

        public event EventHandler? LocationAdded;

        #endregion

        #region Constructors

        public Control_Add_Location()
        {
            InitializeComponent();
            LoadBuildingOptions();
        }

        #endregion

        #region Initialization

        private void LoadBuildingOptions()
        {
            Control_Add_Location_ComboBox_Building.Items.Clear();
            Control_Add_Location_ComboBox_Building.Items.Add("[ Select Building ]");
            Control_Add_Location_ComboBox_Building.Items.Add("Expo");
            Control_Add_Location_ComboBox_Building.Items.Add("Vits");
            Control_Add_Location_ComboBox_Building.SelectedIndex = 0;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (Control_Add_Location_Label_IssuedByValue != null)
            {
                Control_Add_Location_Label_IssuedByValue.Text = Model_Application_Variables.User ?? "Current User";
            }
        }

        #endregion

        #region Event Handlers

        private async void Control_Add_Location_Button_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Control_Add_Location_TextBox_Location.Text))
                {
                    Service_ErrorHandler.HandleValidationError("Location is required.", "Location");
                    Control_Add_Location_TextBox_Location.Focus();
                    return;
                }

                if (Control_Add_Location_ComboBox_Building.SelectedIndex <= 0 ||
                    Control_Add_Location_ComboBox_Building.Text == "Select Building")
                {
                    Service_ErrorHandler.HandleValidationError("Building is required.", "Building");
                    Control_Add_Location_ComboBox_Building.Focus();
                    return;
                }

                string location = Control_Add_Location_TextBox_Location.Text.Trim();
                string building = Control_Add_Location_ComboBox_Building.Text ?? string.Empty;

                var existsResult = await Dao_Location.LocationExists(location);
                if (!existsResult.IsSuccess)
                {
                    if (existsResult.Exception != null)
                    {
                        Service_ErrorHandler.HandleDatabaseError(existsResult.Exception,
                            contextData: new Dictionary<string, object> { ["Location"] = location },
                            callerName: nameof(Control_Add_Location_Button_Save_Click),
                            controlName: nameof(Control_Add_Location));
                    }
                    else
                    {
                        Service_ErrorHandler.HandleException(new Exception(existsResult.ErrorMessage ?? "Database operation failed"),
                            contextData: new Dictionary<string, object> { ["Location"] = location },
                            callerName: nameof(Control_Add_Location_Button_Save_Click),
                            controlName: nameof(Control_Add_Location));
                    }
                    return;
                }
                
                if (existsResult.Data)
                {
                    Service_ErrorHandler.ShowWarning($@"Location '{location}' already exists.", "Duplicate Location");
                    Control_Add_Location_TextBox_Location.Focus();
                    return;
                }

                var insertResult = await Dao_Location.InsertLocation(location, building);
                if (!insertResult.IsSuccess)
                {
                    if (insertResult.Exception != null)
                    {
                        Service_ErrorHandler.HandleDatabaseError(insertResult.Exception,
                            contextData: new Dictionary<string, object> { ["Location"] = location, ["Building"] = building },
                            callerName: nameof(Control_Add_Location_Button_Save_Click),
                            controlName: nameof(Control_Add_Location));
                    }
                    else
                    {
                        Service_ErrorHandler.HandleException(new Exception(insertResult.ErrorMessage ?? "Database operation failed"),
                            contextData: new Dictionary<string, object> { ["Location"] = location, ["Building"] = building },
                            callerName: nameof(Control_Add_Location_Button_Save_Click),
                            controlName: nameof(Control_Add_Location));
                    }
                    return;
                }
                
                ClearForm();
                LocationAdded?.Invoke(this, EventArgs.Empty);
                Service_ErrorHandler.ShowInformation(@"Location added successfully!", @"Success",
                    controlName: nameof(Control_Add_Location));
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(ex,
                    callerName: nameof(Control_Add_Location_Button_Save_Click),
                    controlName: nameof(Control_Add_Location));
            }
        }

        private void Control_Add_Location_Button_Clear_Click(object sender, EventArgs e) => ClearForm();

        #endregion

        #region Methods

        private void ClearForm()
        {
            Control_Add_Location_TextBox_Location.Clear();
            Control_Add_Location_ComboBox_Building.SelectedIndex = 0;
            Control_Add_Location_TextBox_Location.Focus();
        }

        #endregion
    }
}
