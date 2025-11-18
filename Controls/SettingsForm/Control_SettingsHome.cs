using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    /// <summary>
    /// Modern homepage control for Settings form that provides tile-based navigation.
    /// Replaces traditional TreeView with card-based interface.
    /// </summary>
    public partial class Control_SettingsHome : UserControl
    {
        #region Fields

        private readonly Dictionary<string, Control> _categoryControls = new();

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
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes category tiles based on user privileges.
        /// </summary>
        public void InitializeCategories()
        {
            Control_SettingsHome_FlowPanel_Tiles?.Controls.Clear();
            _categoryControls.Clear();

            bool isDeveloper = Model_Application_Variables.UserTypeDeveloper;
            bool isAdmin = Model_Application_Variables.UserTypeAdmin;
            bool isNormal = Model_Application_Variables.UserTypeNormal;
            bool isReadOnly = Model_Application_Variables.UserTypeReadOnly;
            bool hasAdminAccess = isDeveloper || isAdmin;

            // Database
            if (hasAdminAccess)
            {
                AddCategoryTile("Database", "Database", "Configure database connection and settings",
                    Color.FromArgb(0, 120, 212), "🗄️");
            }

            // Users
            if (hasAdminAccess)
            {
                var userSubcategories = new List<SubcategoryInfo>
                {
                    new("Add User", "Add User", "Create new user accounts"),
                    new("Edit User", "Edit User", "Modify existing user accounts"),
                    new("Delete User", "Delete User", "Remove user accounts")
                };
                AddCategoryTile("Users", null, "Manage user accounts and permissions",
                    Color.FromArgb(16, 124, 16), "👥", userSubcategories);
            }
            else if (isNormal)
            {
                var userSubcategories = new List<SubcategoryInfo>
                {
                    new("Add User", "Add User", "Create new user accounts")
                };
                AddCategoryTile("Users", null, "Manage user accounts",
                    Color.FromArgb(16, 124, 16), "👥", userSubcategories);
            }

            // Part Numbers
            if (!isReadOnly)
            {
                var partSubcategories = new List<SubcategoryInfo>
                {
                    new("Add Part Number", "Add Part Number", "Add new part numbers to inventory")
                };

                if (hasAdminAccess)
                {
                    partSubcategories.Add(new("Edit Part Number", "Edit Part Number", "Modify existing part numbers"));
                    partSubcategories.Add(new("Remove Part Number", "Remove Part Number", "Delete part numbers"));
                }

                AddCategoryTile("Part Numbers", null, "Manage inventory part numbers",
                    Color.FromArgb(232, 17, 35), "📦", partSubcategories);
            }

            // Operations
            if (!isReadOnly)
            {
                var operationSubcategories = new List<SubcategoryInfo>
                {
                    new("Add Operation", "Add Operation", "Add new operation codes")
                };

                if (hasAdminAccess)
                {
                    operationSubcategories.Add(new("Edit Operation", "Edit Operation", "Modify existing operations"));
                    operationSubcategories.Add(new("Remove Operation", "Remove Operation", "Delete operation codes"));
                }

                AddCategoryTile("Operations", null, "Manage operation codes",
                    Color.FromArgb(255, 140, 0), "⚙️", operationSubcategories);
            }

            // Locations
            if (!isReadOnly)
            {
                var locationSubcategories = new List<SubcategoryInfo>
                {
                    new("Add Location", "Add Location", "Add new locations")
                };

                if (hasAdminAccess)
                {
                    locationSubcategories.Add(new("Edit Location", "Edit Location", "Modify existing locations"));
                    locationSubcategories.Add(new("Remove Location", "Remove Location", "Delete locations"));
                }

                AddCategoryTile("Locations", null, "Manage storage locations",
                    Color.FromArgb(142, 68, 173), "📍", locationSubcategories);
            }

            // ItemTypes
            if (!isReadOnly)
            {
                var itemTypeSubcategories = new List<SubcategoryInfo>
                {
                    new("Add ItemType", "Add ItemType", "Add new item types")
                };

                if (hasAdminAccess)
                {
                    itemTypeSubcategories.Add(new("Edit ItemType", "Edit ItemType", "Modify existing item types"));
                    itemTypeSubcategories.Add(new("Remove ItemType", "Remove ItemType", "Delete item types"));
                }

                AddCategoryTile("ItemTypes", null, "Manage item type classifications",
                    Color.FromArgb(0, 153, 188), "🏷️", itemTypeSubcategories);
            }

            // Theme
            AddCategoryTile("Theme", "Theme", "Customize application appearance and colors",
                Color.FromArgb(104, 33, 122), "🎨");

            // Shortcuts
            if (!isReadOnly)
            {
                AddCategoryTile("Shortcuts", "Shortcuts", "Configure keyboard shortcuts",
                    Color.FromArgb(0, 99, 177), "⌨️");
            }

            // About
            AddCategoryTile("About", "About", "View application information and version",
                Color.FromArgb(76, 74, 72), "ℹ️");
        }

        /// <summary>
        /// Creates and adds a category tile to the homepage.
        /// </summary>
        private void AddCategoryTile(string title, string? navigationTarget, string description,
            Color accentColor, string icon, List<SubcategoryInfo>? subcategories = null)
        {
            if (subcategories?.Any() == true)
            {
                // Use card for categories with subcategories
                var card = new Control_SettingsCategoryCard
                {
                    CardTitle = title,
                    CardDescription = description,
                    CardIcon = icon,
                    AccentColor = accentColor
                };
                card.NavigationRequested += (s, target) => NavigationRequested?.Invoke(this, new NavigationEventArgs(target));
                
                foreach (var subcat in subcategories)
                {
                    card.AddSubcategoryLink(subcat.Title, subcat.NavigationTarget);
                }
                
                _categoryControls[title] = card;
                Control_SettingsHome_FlowPanel_Tiles?.Controls.Add(card);
            }
            else
            {
                // Use simple tile for categories without subcategories
                var tile = new Control_SettingsCategoryTile
                {
                    TileTitle = title,
                    TileDescription = description,
                    TileIcon = icon,
                    AccentColor = accentColor,
                    NavigationTarget = navigationTarget
                };
                tile.TileClicked += (s, target) => NavigationRequested?.Invoke(this, new NavigationEventArgs(target));
                
                _categoryControls[title] = tile;
                Control_SettingsHome_FlowPanel_Tiles?.Controls.Add(tile);
            }
        }

        #endregion

        #region Nested Types

        /// <summary>
        /// Subcategory information for tile rendering.
        /// </summary>
        private record SubcategoryInfo(string Title, string NavigationTarget, string Description);

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
