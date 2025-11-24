using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    /// <summary>
    /// Unified control that manages adding, editing, and removing users using the new Settings card layout.
    /// </summary>
    public partial class Control_User_Management : ThemedUserControl
    {

        #region Events

        /// <summary>
        /// Raised when the user requests to navigate back to the main settings home.
        /// </summary>
        public event EventHandler? BackToHomeRequested;

        /// <summary>
        /// Raised when a user is added, edited, or removed.
        /// </summary>
        public event EventHandler? UserListChanged;

        #endregion

        #region Constructors

        public Control_User_Management()
        {
            InitializeComponent();
            InitializeCustomStyles();
            WireEvents();
        }

        #endregion

        #region Methods

        private void InitializeCustomStyles()
        {
            // Apply initial theme
            ApplyTheme(Model_Application_Variables.UserUiColors);
        }

        private void WireEvents()
        {
            // Home Tile Events
            Control_User_Management_Panel_HomeTile_Add.Click += (s, e) => SwitchToMode("Add");
            Control_User_Management_Label_HomeTile_AddIcon.Click += (s, e) => SwitchToMode("Add");
            Control_User_Management_Label_HomeTile_AddTitle.Click += (s, e) => SwitchToMode("Add");
            Control_User_Management_Label_HomeTile_AddInstruction.Click += (s, e) => SwitchToMode("Add");

            Control_User_Management_Panel_HomeTile_Edit.Click += (s, e) => SwitchToMode("Edit");
            Control_User_Management_Label_HomeTile_EditIcon.Click += (s, e) => SwitchToMode("Edit");
            Control_User_Management_Label_HomeTile_EditTitle.Click += (s, e) => SwitchToMode("Edit");
            Control_User_Management_Label_HomeTile_EditInstruction.Click += (s, e) => SwitchToMode("Edit");

            Control_User_Management_Panel_HomeTile_Remove.Click += (s, e) => SwitchToMode("Remove");
            Control_User_Management_Label_HomeTile_RemoveIcon.Click += (s, e) => SwitchToMode("Remove");
            Control_User_Management_Label_HomeTile_RemoveTitle.Click += (s, e) => SwitchToMode("Remove");
            Control_User_Management_Label_HomeTile_RemoveInstruction.Click += (s, e) => SwitchToMode("Remove");

            // Back Button
            Control_User_Management_Button_Back.Click += (s, e) => SwitchToMode("None");

            // Home Button
            Control_User_Management_Button_Home.Click += (s, e) => BackToHomeRequested?.Invoke(this, EventArgs.Empty);

            // Embedded Control Events
            Control_User_Management_Control_AddUser.UserAdded += (s, e) => 
            {
                SwitchToMode("None");
                UserListChanged?.Invoke(this, EventArgs.Empty);
            };
            Control_User_Management_Control_EditUser.UserEdited += (s, e) => 
            {
                SwitchToMode("None");
                UserListChanged?.Invoke(this, EventArgs.Empty);
            };
            Control_User_Management_Control_RemoveUser.UserRemoved += (s, e) => 
            {
                SwitchToMode("None");
                UserListChanged?.Invoke(this, EventArgs.Empty);
            };
        }



        private void SwitchToMode(string mode)
        {
            // Hide all panels first
            Control_User_Management_Panel_Home.Visible = false;
            Control_User_Management_Panel_Add.Visible = false;
            Control_User_Management_Panel_Edit.Visible = false;
            Control_User_Management_Panel_Remove.Visible = false;

            if (mode == "None")
            {
                // Return to home view
                Control_User_Management_Panel_Home.Visible = true;
                Control_User_Management_Button_Back.Visible = false;
            }
            else if (mode == "Add")
            {
                Control_User_Management_Panel_Add.Visible = true;
                Control_User_Management_Button_Back.Visible = true;
            }
            else if (mode == "Edit")
            {
                Control_User_Management_Panel_Edit.Visible = true;
                Control_User_Management_Button_Back.Visible = true;
            }
            else if (mode == "Remove")
            {
                Control_User_Management_Panel_Remove.Visible = true;
                Control_User_Management_Button_Back.Visible = true;
            }
        }

        #endregion
    }
}
