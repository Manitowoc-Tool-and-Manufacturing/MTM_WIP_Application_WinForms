using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    public partial class Control_Add_ItemType : UserControl
    {
        #region Events

        public event EventHandler? ItemTypeAdded;

        #endregion

        #region Constructors

                public Control_Add_ItemType()
        {
            InitializeComponent();
        }

        #endregion

        #region Initialization

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (Control_Add_ItemType_Label_IssuedByValue != null)
            {
                Control_Add_ItemType_Label_IssuedByValue.Text = Model_Application_Variables.User ?? "Current User";
            }
        }

        #endregion

        #region Event Handlers

        private async void Control_Add_ItemType_Button_Save_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Control_Add_ItemType_TextBox_ItemType.Text))
                {
                    MessageBox.Show(@"PartID is required.", @"Validation Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    Control_Add_ItemType_TextBox_ItemType.Focus();
                    return;
                }

                string itemType = Control_Add_ItemType_TextBox_ItemType.Text.Trim();

                var existsResult = await Dao_ItemType.ItemTypeExists(itemType);
                if (!existsResult.IsSuccess)
                {
                    MessageBox.Show($@"Error checking PartID: {existsResult.ErrorMessage}", @"Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (existsResult.Data)
                {
                    MessageBox.Show($@"PartID '{itemType}' already exists.", @"Duplicate PartID", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    Control_Add_ItemType_TextBox_ItemType.Focus();
                    return;
                }

                var insertResult = await Dao_ItemType.InsertItemType(itemType, Model_Application_Variables.User ?? "Current User");
                if (!insertResult.IsSuccess)
                {
                    MessageBox.Show($@"Error adding PartID: {insertResult.ErrorMessage}", @"Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ClearForm();
                ItemTypeAdded?.Invoke(this, EventArgs.Empty);
                MessageBox.Show(@"PartID added successfully!", @"Success", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"Error adding PartID: {ex.Message}", @"Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void Control_Add_ItemType_Button_Clear_Click(object sender, EventArgs e) => ClearForm();

        #endregion

        #region Methods

        private void ClearForm()
        {
            Control_Add_ItemType_TextBox_ItemType.Clear();
            Control_Add_ItemType_TextBox_ItemType.Focus();
        }

        #endregion
    }
}
