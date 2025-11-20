using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Controls.Shared;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    /// <summary>
    /// Unified control that manages adding, editing, and removing locations using the new Settings card layout.
    /// </summary>
    public partial class Control_LocationManagement : ThemedUserControl
    {
        #region Fields

        private DataRow? _selectedEditLocation;
        private DataRow? _selectedRemoveLocation;

        #endregion

        #region Properties

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the location management control.
        /// </summary>
        public Control_LocationManagement()
        {
            InitializeComponent();
            InitializeControlText();
            ConfigureInputs();
            WireUpEventHandlers();
            WireUpNavigationHandlers();
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateIssuedByLabels();
            SetEditSectionEnabled(false);
            SetRemoveSectionEnabled(false);
        }

        /// <inheritdoc />
        protected override void ApplyTheme(Model_Shared_UserUiColors theme)
        {
            base.ApplyTheme(theme);
            
            // Remove warning and confirm button

            Control_LocationManagement_Label_RemoveWarning.ForeColor = theme.ErrorColor ?? Color.Red;
            
            Color errorColor = theme.ErrorColor ?? Color.Red;
            Control_LocationManagement_Button_RemoveConfirm.ForeColor = errorColor;
            Control_LocationManagement_Button_RemoveConfirm.BackColor = Color.FromArgb(
                (errorColor.R + 255) / 2,
                (errorColor.G + 255) / 2,
                (errorColor.B + 255) / 2);
        }

        private void InitializeControlText()
        {
            // Home screen labels
            Control_LocationManagement_Label_Header.Text = "Location Management";
            Control_LocationManagement_Label_Subtitle.Text = "Add, edit, or remove locations in the system";
            
            // Suggestion controls - Add
            Control_LocationManagement_TextBox_AddLocation.TextBox.Text = string.Empty;
            Control_LocationManagement_TextBox_AddLocation.LabelText = "New Location";
            Control_LocationManagement_TextBox_AddLocation.PlaceholderText = "Enter a new Location name";            
                
            Control_LocationManagement_ComboBox_AddBuilding.TextBox.Text = "EXPO";
            Control_LocationManagement_ComboBox_AddBuilding.LabelText = "Building";
            Control_LocationManagement_ComboBox_AddBuilding.PlaceholderText = "Select Building (F4)";

            Control_LocationManagement_Label_AddIssuedByValue.Text = Model_Application_Variables.User ?? "Current User";            

            // Suggestion controls - Edit
            Control_LocationManagement_Suggestion_EditSelectLocation.TextBox.Text = string.Empty;
            Control_LocationManagement_Suggestion_EditSelectLocation.LabelText = "Select Location";
            Control_LocationManagement_Suggestion_EditSelectLocation.PlaceholderText = "Search locations (F4)";

            Control_LocationManagement_TextBox_EditNewLocation.TextBox.Text = string.Empty;            
            Control_LocationManagement_TextBox_EditNewLocation.LabelText = "New Location";
            Control_LocationManagement_TextBox_EditNewLocation.PlaceholderText = "Enter new Location name";

            Control_LocationManagement_ComboBox_EditBuilding.TextBox.Text = string.Empty;
            Control_LocationManagement_ComboBox_EditBuilding.LabelText = "Building";
            Control_LocationManagement_ComboBox_EditBuilding.PlaceholderText = "Select Building (F4)";
            
            Control_LocationManagement_Label_EditIssuedByValue.Text = string.Empty;

            // Suggestion controls - Remove
            Control_LocationManagement_Suggestion_RemoveSelectLocation.TextBox.Text = string.Empty;
            Control_LocationManagement_Suggestion_RemoveSelectLocation.LabelText = "Select Location";
            Control_LocationManagement_Suggestion_RemoveSelectLocation.PlaceholderText = "Search locations (F4)";
               
            Control_LocationManagement_Label_RemoveIssuedByValue.Text = string.Empty;
            Control_LocationManagement_Label_RemoveLocationValue.Text = string.Empty;
            Control_LocationManagement_Label_RemoveBuildingValue.Text = string.Empty;

            // Buttons
            Control_LocationManagement_Button_AddSave.Text = "Save";
            Control_LocationManagement_Button_AddClear.Text = "Clear";
            Control_LocationManagement_Button_EditSave.Text = "Save Changes";
            Control_LocationManagement_Button_EditReset.Text = "Reset";
            Control_LocationManagement_Button_RemoveConfirm.Text = "Remove";
            Control_LocationManagement_Button_RemoveCancel.Text = "Cancel";
            Control_LocationManagement_Button_Back.Text = "← Back to Selection";
            Control_LocationManagement_Button_Home.Text = "Home";
            
            // Labels
            Control_LocationManagement_Label_AddIssuedBy.Text = "Issued By";
            Control_LocationManagement_Label_EditIssuedBy.Text = "Issued By";
            Control_LocationManagement_Label_RemoveIssuedBy.Text = "Issued By";
            Control_LocationManagement_Label_RemoveLocation.Text = "Location";
            Control_LocationManagement_Label_RemoveBuilding.Text = "Building";
            Control_LocationManagement_Label_RemoveWarning.Text = "⚠️ Warning: Removal is permanent and cannot be undone.";
        }

        private void ConfigureInputs()
        {
            
            if (Control_LocationManagement_TextBox_AddLocation != null)
            {
                Control_LocationManagement_ComboBox_AddBuilding.Padding = new Padding(3);                
                Control_LocationManagement_TextBox_AddLocation.Padding = new Padding(3);
                Control_LocationManagement_TextBox_AddLocation.EnableSuggestions = false;
                Control_LocationManagement_TextBox_AddLocation.ShowF4Button = false;
                Control_LocationManagement_TextBox_AddLocation.ShowValidationColor = false;
                // Use TextBox.Enabled directly since UpdateTextBoxEnabledState requires validator
                Control_LocationManagement_TextBox_AddLocation.TextBox.Enabled = true;
                Control_LocationManagement_TextBox_AddLocation.TextBox.BackColor = System.Drawing.SystemColors.Window;
            }

            if (Control_LocationManagement_TextBox_EditNewLocation != null)
            {
                
                Control_LocationManagement_ComboBox_EditBuilding.Padding = new Padding(3);
                Control_LocationManagement_TextBox_EditNewLocation.Padding = new Padding(3);
                Control_LocationManagement_TextBox_EditNewLocation.EnableSuggestions = false;
                Control_LocationManagement_TextBox_EditNewLocation.ShowF4Button = false;
                Control_LocationManagement_TextBox_EditNewLocation.ShowValidationColor = false;
                // Use TextBox.Enabled directly since UpdateTextBoxEnabledState requires validator
                Control_LocationManagement_TextBox_EditNewLocation.TextBox.Enabled = true;
                Control_LocationManagement_TextBox_EditNewLocation.TextBox.BackColor = System.Drawing.SystemColors.Window;
            }

            Helper_SuggestionTextBox.ConfigureForLocations(
                Control_LocationManagement_Suggestion_EditSelectLocation,
                Helper_SuggestionTextBox.GetCachedLocationsAsync);

            Helper_SuggestionTextBox.ConfigureForLocations(
                Control_LocationManagement_Suggestion_RemoveSelectLocation,
                Helper_SuggestionTextBox.GetCachedLocationsAsync);
                
            Helper_SuggestionTextBox.ConfigureForBuildings(Control_LocationManagement_ComboBox_AddBuilding);
            Helper_SuggestionTextBox.ConfigureForBuildings(Control_LocationManagement_ComboBox_EditBuilding);
            
        }

        private void WireUpEventHandlers()
        {
            if (Control_LocationManagement_Suggestion_EditSelectLocation != null)
            {
                Control_LocationManagement_Suggestion_EditSelectLocation.SuggestionSelected += async (_, args) =>
                    await LoadEditLocationAsync(args.SelectedValue);
            }

            if (Control_LocationManagement_Suggestion_RemoveSelectLocation != null)
            {
                Control_LocationManagement_Suggestion_RemoveSelectLocation.SuggestionSelected += async (_, args) =>
                    await LoadRemoveLocationAsync(args.SelectedValue);
            }

            Control_LocationManagement_Button_AddSave.Click += async (_, _) => await HandleAddLocationAsync();
            Control_LocationManagement_Button_AddClear.Click += (_, _) => ClearAddSection();

            Control_LocationManagement_Button_EditSave.Click += async (_, _) => await HandleEditLocationSaveAsync();
            Control_LocationManagement_Button_EditReset.Click += (_, _) => ClearEditSection();

            Control_LocationManagement_Button_RemoveConfirm.Click += async (_, _) => await HandleRemoveLocationAsync();
            Control_LocationManagement_Button_RemoveCancel.Click += (_, _) => ClearRemoveSection();
        }

        private void WireUpNavigationHandlers()
        {
            // Home tile clicks
            Control_LocationManagement_Panel_HomeTile_Add.Click += (_, _) => ShowCard(0);
            Control_LocationManagement_Panel_HomeTile_Edit.Click += (_, _) => ShowCard(1);
            Control_LocationManagement_Panel_HomeTile_Remove.Click += (_, _) => ShowCard(2);
            
            // Make all child controls of home tiles also clickable
            foreach (Control control in Control_LocationManagement_Panel_HomeTile_Add.Controls)
            {
                WireUpTileControlClick(control, 0);
            }
            foreach (Control control in Control_LocationManagement_Panel_HomeTile_Edit.Controls)
            {
                WireUpTileControlClick(control, 1);
            }
            foreach (Control control in Control_LocationManagement_Panel_HomeTile_Remove.Controls)
            {
                WireUpTileControlClick(control, 2);
            }
            
            // Back button
            Control_LocationManagement_Button_Back.Click += (_, _) => ShowHome();

            // Back to Home button
            Control_LocationManagement_Button_Home.Click += (_, _) => BackToHomeRequested?.Invoke(this, EventArgs.Empty);
        }

        private void WireUpTileControlClick(Control control, int cardIndex)
        {
            control.Click += (_, _) => ShowCard(cardIndex);
            control.Cursor = Cursors.Hand;
            
            // Recursively wire up child controls
            foreach (Control child in control.Controls)
            {
                WireUpTileControlClick(child, cardIndex);
            }
        }

        private void ShowCard(int cardIndex)
        {
            // Hide home, show cards container and back button
            Control_LocationManagement_Panel_Home.Visible = false;
            Control_LocationManagement_TableLayoutPanel_Cards.Visible = true;
            Control_LocationManagement_TableLayoutPanel_BackButton.Visible = true;
            Control_LocationManagement_Button_Back.Visible = true;
            
            // Hide all cards first
            Control_LocationManagement_Panel_AddCard.Visible = false;
            Control_LocationManagement_Panel_EditCard.Visible = false;
            Control_LocationManagement_Panel_RemoveCard.Visible = false;
            
            // Show selected card
            switch (cardIndex)
            {
                case 0:
                    Control_LocationManagement_Panel_AddCard.Visible = true;
                    Control_LocationManagement_TextBox_AddLocation.Focus();
                    break;
                case 1:
                    Control_LocationManagement_Panel_EditCard.Visible = true;
                    Control_LocationManagement_Suggestion_EditSelectLocation.Focus();
                    break;
                case 2:
                    Control_LocationManagement_Panel_RemoveCard.Visible = true;
                    Control_LocationManagement_Suggestion_RemoveSelectLocation.Focus();
                    break;
            }
        }

        private void ShowHome()
        {
            // Clear all sections
            ClearAddSection();
            ClearEditSection();
            ClearRemoveSection();
            
            // Show home, hide cards and back button
            Control_LocationManagement_Panel_Home.Visible = true;
            Control_LocationManagement_TableLayoutPanel_Cards.Visible = false;
            Control_LocationManagement_TableLayoutPanel_BackButton.Visible = true;
            Control_LocationManagement_Button_Back.Visible = false;
        }

        private void UpdateIssuedByLabels()
        {
            string issuedBy = Model_Application_Variables.User ?? "Current User";
            Control_LocationManagement_Label_AddIssuedByValue.Text = issuedBy;
            Control_LocationManagement_Label_EditIssuedByValue.Text = issuedBy;
            Control_LocationManagement_Label_RemoveIssuedByValue.Text = issuedBy;
        }

        private async Task HandleAddLocationAsync()
        {
            string location = Control_LocationManagement_TextBox_AddLocation.Text?.Trim() ?? string.Empty;
            string building = Control_LocationManagement_ComboBox_AddBuilding.Text?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(location))
            {
                ShowWarning("Location is required before saving.");
                Control_LocationManagement_TextBox_AddLocation.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(building))
            {
                ShowWarning("Building is required before saving.");
                Control_LocationManagement_ComboBox_AddBuilding.Focus();
                return;
            }

            // Check if location already exists
            var existsResult = await Dao_Location.LocationExists(location);
            if (!existsResult.IsSuccess)
            {
                Service_ErrorHandler.HandleDatabaseError(
                    new Exception(existsResult.ErrorMessage ?? "Failed to check if location exists"),
                    contextData: new Dictionary<string, object>
                    {
                        ["Location"] = location,
                        ["User"] = Model_Application_Variables.User ?? "Unknown"
                    },
                    callerName: nameof(HandleAddLocationAsync),
                    controlName: this.Name);
                return;
            }

            if (existsResult.Data)
            {
                ShowWarning($"Location '{location}' already exists.");
                Control_LocationManagement_TextBox_AddLocation.Focus();
                Control_LocationManagement_TextBox_AddLocation.TextBox.SelectAll();
                return;
            }

            // Insert new location
            var insertResult = await Dao_Location.InsertLocation(location, building);
            if (!insertResult.IsSuccess)
            {
                Service_ErrorHandler.HandleDatabaseError(
                    new Exception(insertResult.ErrorMessage ?? "Failed to insert location"),
                    contextData: new Dictionary<string, object>
                    {
                        ["Location"] = location,
                        ["Building"] = building,
                        ["User"] = Model_Application_Variables.User ?? "Unknown"
                    },
                    callerName: nameof(HandleAddLocationAsync),
                    controlName: this.Name);
                return;
            }

            LoggingUtility.Log($"[{this.Name}] Location added successfully: Location={location}, Building={building}");
            ShowSuccess($"Location '{location}' added successfully.");
            
            // Refresh caches
            await RefreshLocationCachesAsync();
            NotifyLocationListChanged();
            
            // Clear form
            ClearAddSection();
            Control_LocationManagement_TextBox_AddLocation.Focus();
        }

        private async Task HandleEditLocationSaveAsync()
        {
            if (_selectedEditLocation == null)
            {
                ShowWarning("Please select a location to edit first.");
                return;
            }

            string originalLocation = _selectedEditLocation["Location"]?.ToString() ?? string.Empty;
            string newLocation = Control_LocationManagement_TextBox_EditNewLocation.Text?.Trim() ?? string.Empty;
            string newBuilding = Control_LocationManagement_ComboBox_EditBuilding.Text?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(newLocation))
            {
                ShowWarning("Location name is required.");
                Control_LocationManagement_TextBox_EditNewLocation.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(newBuilding))
            {
                ShowWarning("Building is required.");
                Control_LocationManagement_ComboBox_EditBuilding.Focus();
                return;
            }

            // Check for duplicates if name changed
            if (newLocation != originalLocation)
            {
                var existsResult = await Dao_Location.LocationExists(newLocation);
                if (!existsResult.IsSuccess)
                {
                    Service_ErrorHandler.HandleDatabaseError(
                        new Exception(existsResult.ErrorMessage ?? "Failed to check if location exists"),
                        contextData: new Dictionary<string, object>
                        {
                            ["OriginalLocation"] = originalLocation,
                            ["NewLocation"] = newLocation,
                            ["User"] = Model_Application_Variables.User ?? "Unknown"
                        },
                        callerName: nameof(HandleEditLocationSaveAsync),
                        controlName: this.Name);
                    return;
                }

                if (existsResult.Data)
                {
                    ShowWarning($"Location '{newLocation}' already exists.");
                    Control_LocationManagement_TextBox_EditNewLocation.Focus();
                    Control_LocationManagement_TextBox_EditNewLocation.TextBox.SelectAll();
                    return;
                }
            }

            // Update location
            var updateResult = await Dao_Location.UpdateLocation(
                originalLocation,
                newLocation,
                newBuilding);

            if (!updateResult.IsSuccess)
            {
                Service_ErrorHandler.HandleDatabaseError(
                    new Exception(updateResult.ErrorMessage ?? "Failed to update location"),
                    contextData: new Dictionary<string, object>
                    {
                        ["OriginalLocation"] = originalLocation,
                        ["NewLocation"] = newLocation,
                        ["Building"] = newBuilding,
                        ["User"] = Model_Application_Variables.User ?? "Unknown"
                    },
                    callerName: nameof(HandleEditLocationSaveAsync),
                    controlName: this.Name);
                return;
            }

            LoggingUtility.Log($"[{this.Name}] Location updated successfully: Original={originalLocation}, New={newLocation}, Building={newBuilding}");
            ShowSuccess($"Location '{newLocation}' updated successfully.");
            
            // Refresh caches
            await RefreshLocationCachesAsync();
            NotifyLocationListChanged();
            
            // Clear form
            ClearEditSection();
            Control_LocationManagement_Suggestion_EditSelectLocation.Focus();
        }

        private async Task HandleRemoveLocationAsync()
        {
            if (_selectedRemoveLocation == null)
            {
                ShowWarning("Please select a location to remove first.");
                return;
            }

            string location = _selectedRemoveLocation["Location"]?.ToString() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(location))
            {
                ShowWarning("Invalid location selection.");
                return;
            }

            // Confirm deletion
            DialogResult confirmation = Service_ErrorHandler.ShowConfirmation(
                $"Are you sure you want to remove location '{location}'?\n\nThis action cannot be undone.",
                "Confirm Location Removal");

            if (confirmation != DialogResult.Yes)
            {
                return;
            }

            // Delete location
            var deleteResult = await Dao_Location.DeleteLocation(location);
            if (!deleteResult.IsSuccess)
            {
                Service_ErrorHandler.HandleDatabaseError(
                    new Exception(deleteResult.ErrorMessage ?? "Failed to delete location"),
                    contextData: new Dictionary<string, object>
                    {
                        ["Location"] = location,
                        ["User"] = Model_Application_Variables.User ?? "Unknown"
                    },
                    callerName: nameof(HandleRemoveLocationAsync),
                    controlName: this.Name);
                return;
            }

            LoggingUtility.Log($"[{this.Name}] Location removed successfully: Location={location}");
            ShowSuccess($"Location '{location}' removed successfully.");
            
            // Refresh caches
            await RefreshLocationCachesAsync();
            NotifyLocationListChanged();
            
            // Clear form
            ClearRemoveSection();
            Control_LocationManagement_Suggestion_RemoveSelectLocation.Focus();
        }

        private async Task LoadEditLocationAsync(string locationName)
        {
            if (string.IsNullOrWhiteSpace(locationName))
            {
                SetEditSectionEnabled(false);
                return;
            }

            var result = await Dao_Location.GetLocationByName(locationName);
            if (!result.IsSuccess || result.Data == null)
            {
                Service_ErrorHandler.HandleDatabaseError(
                    new Exception(result.ErrorMessage ?? "Failed to load location details"),
                    contextData: new Dictionary<string, object>
                    {
                        ["Location"] = locationName,
                        ["User"] = Model_Application_Variables.User ?? "Unknown"
                    },
                    callerName: nameof(LoadEditLocationAsync),
                    controlName: this.Name);
                SetEditSectionEnabled(false);
                return;
            }

            _selectedEditLocation = result.Data;
            
            // Populate fields
            string location = _selectedEditLocation["Location"]?.ToString() ?? string.Empty;
            string building = _selectedEditLocation["Building"]?.ToString() ?? string.Empty;
            string issuedBy = _selectedEditLocation["IssuedBy"]?.ToString() ?? "Unknown";

            // Set location name
            Control_LocationManagement_TextBox_EditNewLocation.Text = location;
            
            // Set building dropdown
                Control_LocationManagement_ComboBox_EditBuilding.Text = building;

            // Set issued by label
                Control_LocationManagement_Label_EditIssuedByValue.Text = issuedBy;

            SetEditSectionEnabled(true);
        }

        private async Task LoadRemoveLocationAsync(string locationName)
        {
            if (string.IsNullOrWhiteSpace(locationName))
            {
                SetRemoveSectionEnabled(false);
                return;
            }

            var result = await Dao_Location.GetLocationByName(locationName);
            if (!result.IsSuccess || result.Data == null)
            {
                Service_ErrorHandler.HandleDatabaseError(
                    new Exception(result.ErrorMessage ?? "Failed to load location details"),
                    contextData: new Dictionary<string, object>
                    {
                        ["Location"] = locationName,
                        ["User"] = Model_Application_Variables.User ?? "Unknown"
                    },
                    callerName: nameof(LoadRemoveLocationAsync),
                    controlName: this.Name);
                SetRemoveSectionEnabled(false);
                return;
            }

            _selectedRemoveLocation = result.Data;
            
            // Display details
            string location = _selectedRemoveLocation["Location"]?.ToString() ?? "N/A";
            string building = _selectedRemoveLocation["Building"]?.ToString() ?? "N/A";
            string issuedBy = _selectedRemoveLocation["IssuedBy"]?.ToString() ?? "Unknown";

            Control_LocationManagement_Label_RemoveLocationValue.Text = location;
            Control_LocationManagement_Label_RemoveBuildingValue.Text = building;
            Control_LocationManagement_Label_RemoveIssuedByValue.Text = issuedBy;

            SetRemoveSectionEnabled(true);
        }

        private void SetEditSectionEnabled(bool enabled)
        {
            Control_LocationManagement_TextBox_EditNewLocation.Enabled = enabled;
            Control_LocationManagement_ComboBox_EditBuilding.Enabled = enabled;
            Control_LocationManagement_Button_EditSave.Enabled = enabled;
            Control_LocationManagement_Button_EditReset.Enabled = enabled;
        }

        private void SetRemoveSectionEnabled(bool enabled)
        {
            Control_LocationManagement_Button_RemoveConfirm.Enabled = enabled;
            Control_LocationManagement_Button_RemoveCancel.Enabled = enabled;
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.Visible = enabled;
        }

        private void ClearAddSection()
        {
            InitializeControlText();
        }

        private void ClearEditSection()
        {
            _selectedEditLocation = null;
            InitializeControlText();
            SetEditSectionEnabled(false);
        }

        private void ClearRemoveSection()
        {
            _selectedRemoveLocation = null;
            InitializeControlText();
            SetRemoveSectionEnabled(false);
        }

        private void NotifyLocationListChanged()
        {
            LocationListChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Applies privilege-based visibility and enabled states for each card.
        /// </summary>
        /// <param name="canAdd">If true, add card remains visible and interactive.</param>
        /// <param name="canEdit">If true, edit card remains visible and interactive.</param>
        /// <param name="canRemove">If true, remove card remains visible and interactive.</param>
        public void ApplyPrivileges(bool canAdd, bool canEdit, bool canRemove)
        {
            // Home tiles
            Control_LocationManagement_Panel_HomeTile_Add.Visible = canAdd;
            Control_LocationManagement_Panel_HomeTile_Edit.Visible = canEdit;
            Control_LocationManagement_Panel_HomeTile_Remove.Visible = canRemove;
            
            // Add card
            Control_LocationManagement_Panel_AddCard.Visible = canAdd;
            Control_LocationManagement_TextBox_AddLocation.Enabled = canAdd;
            Control_LocationManagement_ComboBox_AddBuilding.Enabled = canAdd;
            Control_LocationManagement_Button_AddSave.Enabled = canAdd;
            Control_LocationManagement_Button_AddClear.Enabled = canAdd;

            // Edit card
            Control_LocationManagement_Panel_EditCard.Visible = canEdit;
            Control_LocationManagement_Suggestion_EditSelectLocation.Enabled = canEdit;
            Control_LocationManagement_TextBox_EditNewLocation.Enabled = canEdit;
            Control_LocationManagement_ComboBox_EditBuilding.Enabled = canEdit;
            Control_LocationManagement_Button_EditSave.Enabled = canEdit;
            Control_LocationManagement_Button_EditReset.Enabled = canEdit;

            // Remove card
            Control_LocationManagement_Panel_RemoveCard.Visible = canRemove;
            Control_LocationManagement_Suggestion_RemoveSelectLocation.Enabled = canRemove;
            Control_LocationManagement_Button_RemoveConfirm.Enabled = canRemove && _selectedRemoveLocation != null;
            Control_LocationManagement_Button_RemoveCancel.Enabled = canRemove;

            if (!canEdit)
            {
                ClearEditSection();
            }

            if (!canRemove)
            {
                ClearRemoveSection();
            }
            
            // If user has no privileges at all, show message
            if (!canAdd && !canEdit && !canRemove)
            {
                Control_LocationManagement_Label_Subtitle.Text = "You do not have permissions to manage locations.";
                Control_LocationManagement_Panel_Home.Visible = false;
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Raised when location data has changed (add, edit, remove) so parent views can refresh caches.
        /// </summary>
        public event EventHandler? LocationListChanged;

        /// <summary>
        /// Raised when the user requests to navigate back to the main settings home.
        /// </summary>
        public event EventHandler? BackToHomeRequested;

        #endregion

        #region Helpers

        private static async Task RefreshLocationCachesAsync()
        {
            try
            {
                await Helper_SuggestionTextBox.GetCachedLocationsAsync();
                await Helper_UI_ComboBoxes.FillLocationComboBoxesAsync(new ComboBox());
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
            }
        }

        private static void ShowWarning(string message)
        {
            Service_ErrorHandler.ShowWarning(
                message,
                "Warning",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
        }

        private static void ShowSuccess(string message)
        {
            Service_ErrorHandler.ShowInformation(
                message,
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        #endregion

        #region Cleanup / Dispose

        #endregion
    }
}
