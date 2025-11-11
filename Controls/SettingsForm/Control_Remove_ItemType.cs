using System.Data;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    public partial class Control_Remove_ItemType : UserControl
    {
        #region Events

        public event EventHandler? ItemTypeRemoved;

        #endregion

        #region Fields

        private DataRow? _currentItemType;

        #endregion

        #region Constructors

                public Control_Remove_ItemType()
        {
            InitializeComponent();
            Core_Themes.ApplyDpiScaling(this);
        }

        #endregion

        #region Initialization

        protected override async void OnLoad(EventArgs e)
        {
            try
            {
                base.OnLoad(e);
                if (issuedByValueLabel != null)
                {
                    issuedByValueLabel.Text = Model_Application_Variables.User ?? "Current User";
                }

                LoadItemTypes();
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex,
                    "SettingsForm / RemoveItemTypeControl_OnLoadOverRide");
            }
        }

        private async void LoadItemTypes()
        {
            try
            {
                await Helper_UI_ComboBoxes.FillItemTypeComboBoxesAsync(itemTypesComboBox);
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"Error loading part types: {ex.Message}", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Event Handlers

        private async void ItemTypesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (itemTypesComboBox.SelectedIndex <= 0)
            {
                ClearForm();
                SetFormEnabled(false);
                return;
            }

            try
            {
                string selectedType = itemTypesComboBox.Text ?? string.Empty;
                var getResult = await Dao_ItemType.GetItemTypeByName(selectedType);
                if (!getResult.IsSuccess)
                {
                    MessageBox.Show($@"Error loading ItemType: {getResult.ErrorMessage}", @"Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _currentItemType = getResult.Data;
                if (_currentItemType != null)
                {
                    LoadItemTypeData();
                    SetFormEnabled(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"Error loading ItemType data: {ex.Message}", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async void RemoveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentItemType == null)
                {
                    MessageBox.Show(@"No ItemType selected.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string? itemType = _currentItemType["ItemType"]?.ToString();
                if (string.IsNullOrEmpty(itemType))
                {
                    MessageBox.Show(@"Invalid ItemType selected.", @"Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                DialogResult result = MessageBox.Show(
                    $"Are you sure you want to remove the ItemType '{itemType}'?{Environment.NewLine}{Environment.NewLine}This action cannot be undone.",
                    "Confirm Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    var deleteResult = await Dao_ItemType.DeleteItemType(itemType);
                    if (!deleteResult.IsSuccess)
                    {
                        MessageBox.Show($@"Error removing ItemType: {deleteResult.ErrorMessage}", @"Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    LoadItemTypes();
                    ClearForm();
                    SetFormEnabled(false);
                    ItemTypeRemoved?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(@"ItemType removed successfully.", @"Success", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"Error removing ItemType: {ex.Message}", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            ClearForm();
            SetFormEnabled(false);
            itemTypesComboBox.SelectedIndex = 0;
        }

        #endregion

        #region Methods

        private void LoadItemTypeData()
        {
            if (_currentItemType == null)
            {
                return;
            }

            itemTypeValueLabel.Text = _currentItemType["ItemType"]?.ToString() ?? string.Empty;
            originalIssuedByValueLabel.Text = _currentItemType["IssuedBy"]?.ToString() ?? string.Empty;
        }

        private void SetFormEnabled(bool enabled) => removeButton.Enabled = enabled;

        private void ClearForm()
        {
            itemTypeValueLabel.Text = string.Empty;
            originalIssuedByValueLabel.Text = string.Empty;
        }

        #endregion
    }
}
