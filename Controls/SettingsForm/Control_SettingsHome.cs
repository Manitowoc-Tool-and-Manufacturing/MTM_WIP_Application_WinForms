using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    /// <summary>
    /// Modern homepage control for Settings form that provides tile-based navigation.
    /// Replaces traditional TreeView with card-based interface.
    /// </summary>
    public partial class Control_SettingsHome : ThemedUserControl
    {
        #region Fields

        #endregion

        #region Events

        /// <summary>
        /// Fired when user clicks a category or subcategory tile.
        /// </summary>
        public event EventHandler<NavigationEventArgs>? NavigationRequested;

        #endregion

        #region Constructors

        public Control_SettingsHome()
        {
            InitializeComponent();
            InitializeEventHandlers();
        }

        #endregion

        #region Methods

        private void InitializeEventHandlers()
        {
            // Wire up card subcategory links for admin users (will be shown in InitializeCategories)
            Control_SettingsHome_Card_Users.AddSubcategoryLink("Add User", "Add User");
            Control_SettingsHome_Card_Users.AddSubcategoryLink("Edit User", "Edit User");
            Control_SettingsHome_Card_Users.AddSubcategoryLink("Delete User", "Delete User");

            Control_SettingsHome_Card_Parts.ClearSubcategoryLinks();
            Control_SettingsHome_Card_Parts.AddSubcategoryLink("Manage Part Numbers", "Part Numbers");

            Control_SettingsHome_Card_Operations.AddSubcategoryLink("Add Operation", "Add Operation");
            Control_SettingsHome_Card_Operations.AddSubcategoryLink("Edit Operation", "Edit Operation");
            Control_SettingsHome_Card_Operations.AddSubcategoryLink("Remove Operation", "Remove Operation");

            Control_SettingsHome_Card_Locations.AddSubcategoryLink("Add Location", "Add Location");
            Control_SettingsHome_Card_Locations.AddSubcategoryLink("Edit Location", "Edit Location");
            Control_SettingsHome_Card_Locations.AddSubcategoryLink("Remove Location", "Remove Location");

            Control_SettingsHome_Card_ItemTypes.AddSubcategoryLink("Add ItemType", "Add ItemType");
            Control_SettingsHome_Card_ItemTypes.AddSubcategoryLink("Edit ItemType", "Edit ItemType");
            Control_SettingsHome_Card_ItemTypes.AddSubcategoryLink("Remove ItemType", "Remove ItemType");
        }

        /// <summary>
        /// Initializes category tiles based on user privileges.
        /// </summary>
        public void InitializeCategories()
        {
            bool isDeveloper = Model_Application_Variables.UserTypeDeveloper;
            bool isAdmin = Model_Application_Variables.UserTypeAdmin;
            bool isNormal = Model_Application_Variables.UserTypeNormal;
            bool isReadOnly = Model_Application_Variables.UserTypeReadOnly;
            bool hasAdminAccess = isDeveloper || isAdmin;

            // Configure visibility based on privileges
            Control_SettingsHome_Tile_Database.Visible = hasAdminAccess;
            Control_SettingsHome_Card_Users.Visible = hasAdminAccess || isNormal;
            Control_SettingsHome_Card_Parts.Visible = !isReadOnly;
            Control_SettingsHome_Card_Operations.Visible = !isReadOnly;
            Control_SettingsHome_Card_Locations.Visible = !isReadOnly;
            Control_SettingsHome_Card_ItemTypes.Visible = !isReadOnly;
            Control_SettingsHome_Tile_Shortcuts.Visible = !isReadOnly;
            
            // Adjust Users card subcategories based on privileges
            if (isNormal && !hasAdminAccess)
            {
                // Normal users only see Add User
                Control_SettingsHome_Card_Users.ClearSubcategoryLinks();
                Control_SettingsHome_Card_Users.AddSubcategoryLink("Add User", "Add User");
            }
            
            // Adjust Operations subcategories
            if (!hasAdminAccess && !isReadOnly)
            {
                Control_SettingsHome_Card_Operations.ClearSubcategoryLinks();
                Control_SettingsHome_Card_Operations.AddSubcategoryLink("Add Operation", "Add Operation");
            }
            
            // Adjust Locations subcategories
            if (!hasAdminAccess && !isReadOnly)
            {
                Control_SettingsHome_Card_Locations.ClearSubcategoryLinks();
                Control_SettingsHome_Card_Locations.AddSubcategoryLink("Add Location", "Add Location");
            }
            
            // Adjust ItemTypes subcategories
            if (!hasAdminAccess && !isReadOnly)
            {
                Control_SettingsHome_Card_ItemTypes.ClearSubcategoryLinks();
                Control_SettingsHome_Card_ItemTypes.AddSubcategoryLink("Add ItemType", "Add ItemType");
            }
        }

        #endregion

        #region Event Handlers

        private void Control_SettingsHome_Tile_TileClicked(object? sender, string target)
        {
            NavigationRequested?.Invoke(this, new NavigationEventArgs(target));
        }

        private void Control_SettingsHome_Card_NavigationRequested(object? sender, string target)
        {
            NavigationRequested?.Invoke(this, new NavigationEventArgs(target));
        }

        #endregion

        #region Nested Types

        #endregion
    }

    /// <summary>
    /// Event args for navigation requests from homepage.
    /// </summary>
    public class NavigationEventArgs : EventArgs
    {
        public string Target { get; }

        public NavigationEventArgs(string target)
        {
            Target = target;
        }
    }
}
