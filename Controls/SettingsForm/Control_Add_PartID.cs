using System.Data;
using System.Reflection;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Helpers;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    public partial class Control_Add_PartID : UserControl
    {
        #region Fields

        #region Events

        public event EventHandler? PartAdded;

        #endregion

        #region Constructors

        public Control_Add_PartID()
        {
            Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
            {
                ["ControlType"] = nameof(Control_Add_PartID),
                ["InitializationTime"] = DateTime.Now,
                ["Thread"] = Thread.CurrentThread.ManagedThreadId
            }, nameof(Control_Add_PartID), nameof(Control_Add_PartID));

            Service_DebugTracer.TraceUIAction("ADD_PARTID_INITIALIZATION", nameof(Control_Add_PartID),
                new Dictionary<string, object>
                {
                    ["Phase"] = "START",
                    ["ComponentType"] = "UserControl"
                });

            InitializeComponent();

            Service_DebugTracer.TraceUIAction("PART_TYPES_LOADING", nameof(Control_Add_PartID),
                new Dictionary<string, object> { ["DataSource"] = "ItemTypes" });
            LoadPartTypes();

            Service_DebugTracer.TraceMethodExit(null, nameof(Control_Add_PartID), nameof(Control_Add_PartID));
        }

        #endregion

        #region Initialization

        private async void LoadPartTypes()
        {
            try
            {
                await Helper_UI_ComboBoxes.FillItemTypeComboBoxesAsync(Control_Add_PartID_ComboBox_ItemType);

                // Set default item to "WIP" if it exists
                for (int i = 0; i < Control_Add_PartID_ComboBox_ItemType.Items.Count; i++)
                {
                    if (Control_Add_PartID_ComboBox_ItemType.Items[i]?.ToString()
                            ?.Equals("WIP", StringComparison.OrdinalIgnoreCase) == true)
                    {
                        Control_Add_PartID_ComboBox_ItemType.SelectedIndex = i;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"Error loading part types: {ex.Message}", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (issuedByValueLabel != null)
            {
                issuedByValueLabel.Text = Model_Application_Variables.User ?? "Current User";
            }
        }

        #endregion

        #region Event Handlers

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemNumberTextBox.Text))
                {
                    MessageBox.Show(@"Item Number is required.", @"Validation Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    itemNumberTextBox.Focus();
                    return;
                }

                if (Control_Add_PartID_ComboBox_ItemType.SelectedIndex < 0)
                {
                    MessageBox.Show(@"Please select a part type.", @"Validation Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    Control_Add_PartID_ComboBox_ItemType.Focus();
                    return;
                }

                var existsResult = await Dao_Part.PartExistsAsync(itemNumberTextBox.Text.Trim());
                if (!existsResult.IsSuccess)
                {
                    MessageBox.Show($@"Error checking part existence: {existsResult.ErrorMessage}", @"Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (existsResult.Data)
                {
                    MessageBox.Show($@"Part number '{itemNumberTextBox.Text.Trim()}' already exists.",
                        @"Duplicate Part Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    itemNumberTextBox.Focus();
                    return;
                }

                var addResult = await AddPartAsync();
                if (!addResult.IsSuccess)
                {
                    MessageBox.Show($@"Error adding part: {addResult.ErrorMessage}", @"Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                MessageBox.Show(@"Part added successfully!", @"Success", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                
                // If RequiresColorCode was checked, reload the cache
                if (Control_Add_PartID_CheckBox_RequiresColorCode.Checked)
                {
                    await Model_Application_Variables.ReloadColorCodePartsAsync();
                    LoggingUtility.Log($"[Control_Add_PartID] ColorCodeParts cache reloaded after adding part with RequiresColorCode=true");
                }
                
                ClearForm();
                PartAdded?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"Error adding part: {ex.Message}", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private async Task<Model_Dao_Result> AddPartAsync()
        {
            string itemNumber = itemNumberTextBox.Text.Trim();
            string issuedBy = Model_Application_Variables.User;
            string type = Control_Add_PartID_ComboBox_ItemType.Text ?? string.Empty;
            bool requiresColorCode = Control_Add_PartID_CheckBox_RequiresColorCode.Checked;
            
            return await Dao_Part.CreatePartAsync(itemNumber, string.Empty, string.Empty, issuedBy, type, requiresColorCode);
        }

        private void CancelButton_Click(object sender, EventArgs e) => ClearForm();

        #endregion

        #region Methods

        private void ClearForm()
        {
            itemNumberTextBox.Clear();
            Control_Add_PartID_CheckBox_RequiresColorCode.Checked = false;

            // Set ComboBox to "WIP" if it exists
            for (int i = 0; i < Control_Add_PartID_ComboBox_ItemType.Items.Count; i++)
            {
                if (Control_Add_PartID_ComboBox_ItemType.Items[i]?.ToString()
                        ?.Equals("WIP", StringComparison.OrdinalIgnoreCase) == true)
                {
                    Control_Add_PartID_ComboBox_ItemType.SelectedIndex = i;
                    break;
                }
            }

            itemNumberTextBox.Focus();
        }

        // Helper class for ComboBox items
        private class ComboBoxItem
        {
            public string Display { get; set; }
            public string FullName { get; set; }
            public string FileName { get; set; }
            public override string ToString() => Display;
        }

        #endregion

        #endregion
    }
}
