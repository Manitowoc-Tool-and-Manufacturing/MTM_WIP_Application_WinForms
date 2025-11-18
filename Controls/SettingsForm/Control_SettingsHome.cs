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

        private readonly Dictionary<string, CategoryTile> _categoryTiles = new();
        private TableLayoutPanel? _mainLayout;
        private Label? _headerLabel;
        private FlowLayoutPanel? _tilesContainer;

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
            BuildModernLayout();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Builds the modern card-based homepage layout.
        /// </summary>
        private void BuildModernLayout()
        {
            this.SuspendLayout();

            // Main container
            _mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Padding = new Padding(20),
                BackColor = SystemColors.Control
            };
            _mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F)); // Header
            _mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Tiles

            // Header
            _headerLabel = new Label
            {
                Text = "Settings",
                Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.FromArgb(45, 45, 45)
            };
            _mainLayout.Controls.Add(_headerLabel, 0, 0);

            // Tiles container
            _tilesContainer = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(0),
                WrapContents = true,
                FlowDirection = FlowDirection.LeftToRight
            };
            _mainLayout.Controls.Add(_tilesContainer, 0, 1);

            this.Controls.Add(_mainLayout);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        /// <summary>
        /// Initializes category tiles based on user privileges.
        /// </summary>
        public void InitializeCategories()
        {
            _tilesContainer?.Controls.Clear();
            _categoryTiles.Clear();

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
            var tile = new CategoryTile(title, navigationTarget, description, accentColor, icon, subcategories);
            tile.NavigationRequested += (s, e) => NavigationRequested?.Invoke(this, e);
            _categoryTiles[title] = tile;
            _tilesContainer?.Controls.Add(tile);
        }

        #endregion

        #region Nested Types

        /// <summary>
        /// Represents a modern card-style category tile.
        /// </summary>
        private class CategoryTile : Panel
        {
            private readonly string? _navigationTarget;
            private readonly List<SubcategoryInfo>? _subcategories;
            private Panel? _subcategoryPanel;
            private bool _expanded;

            public event EventHandler<NavigationEventArgs>? NavigationRequested;

            public CategoryTile(string title, string? navigationTarget, string description,
                Color accentColor, string icon, List<SubcategoryInfo>? subcategories = null)
            {
                _navigationTarget = navigationTarget;
                _subcategories = subcategories;

                this.Size = new Size(280, subcategories?.Any() == true ? 180 : 140);
                this.Margin = new Padding(10);
                this.BackColor = Color.White;
                this.BorderStyle = BorderStyle.None;
                this.Cursor = Cursors.Hand;

                // Paint border manually for modern look
                this.Paint += (s, e) =>
                {
                    using var pen = new Pen(Color.FromArgb(200, 200, 200), 1);
                    e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
                };

                var mainContent = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    RowCount = 3,
                    ColumnCount = 1,
                    Padding = new Padding(15)
                };
                mainContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 8F)); // Accent bar
                mainContent.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // Content
                mainContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Subcategories
                mainContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

                // Accent bar
                var accentBar = new Panel
                {
                    Height = 4,
                    Dock = DockStyle.Top,
                    BackColor = accentColor,
                    Margin = new Padding(0, 0, 0, 10)
                };
                mainContent.Controls.Add(accentBar, 0, 0);

                // Content area
                var contentPanel = new Panel
                {
                    AutoSize = true,
                    Dock = DockStyle.Top
                };

                var iconLabel = new Label
                {
                    Text = icon,
                    Font = new Font("Segoe UI Emoji", 24F),
                    AutoSize = true,
                    Location = new Point(0, 0)
                };
                contentPanel.Controls.Add(iconLabel);

                var titleLabel = new Label
                {
                    Text = title,
                    Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold),
                    AutoSize = true,
                    Location = new Point(0, 35),
                    ForeColor = Color.FromArgb(45, 45, 45)
                };
                contentPanel.Controls.Add(titleLabel);

                var descLabel = new Label
                {
                    Text = description,
                    Font = new Font("Segoe UI", 9F),
                    AutoSize = true,
                    MaximumSize = new Size(240, 0),
                    Location = new Point(0, 58),
                    ForeColor = Color.FromArgb(96, 94, 92)
                };
                contentPanel.Controls.Add(descLabel);

                contentPanel.Height = 85;
                mainContent.Controls.Add(contentPanel, 0, 1);

                // Subcategories
                if (subcategories?.Any() == true)
                {
                    _subcategoryPanel = new FlowLayoutPanel
                    {
                        Dock = DockStyle.Top,
                        AutoSize = true,
                        FlowDirection = FlowDirection.TopDown,
                        WrapContents = false,
                        Padding = new Padding(0, 5, 0, 0)
                    };

                    foreach (var subcat in subcategories)
                    {
                        var subcatLink = new LinkLabel
                        {
                            Text = $"→ {subcat.Title}",
                            Font = new Font("Segoe UI", 9F),
                            AutoSize = true,
                            Margin = new Padding(0, 2, 0, 2),
                            LinkColor = accentColor,
                            ActiveLinkColor = Color.FromArgb(
                                Math.Max(0, accentColor.R - 30),
                                Math.Max(0, accentColor.G - 30),
                                Math.Max(0, accentColor.B - 30)),
                            Tag = subcat.NavigationTarget
                        };
                        subcatLink.LinkClicked += SubcategoryLink_Clicked;
                        _subcategoryPanel.Controls.Add(subcatLink);
                    }

                    mainContent.Controls.Add(_subcategoryPanel, 0, 2);
                }

                this.Controls.Add(mainContent);

                // Hover effect
                this.MouseEnter += (s, e) => this.BackColor = Color.FromArgb(250, 250, 250);
                this.MouseLeave += (s, e) => this.BackColor = Color.White;

                // Click handler for direct navigation
                if (_navigationTarget != null)
                {
                    this.Click += (s, e) => NavigationRequested?.Invoke(this,
                        new NavigationEventArgs(_navigationTarget));
                    contentPanel.Click += (s, e) => NavigationRequested?.Invoke(this,
                        new NavigationEventArgs(_navigationTarget));
                    iconLabel.Click += (s, e) => NavigationRequested?.Invoke(this,
                        new NavigationEventArgs(_navigationTarget));
                    titleLabel.Click += (s, e) => NavigationRequested?.Invoke(this,
                        new NavigationEventArgs(_navigationTarget));
                    descLabel.Click += (s, e) => NavigationRequested?.Invoke(this,
                        new NavigationEventArgs(_navigationTarget));
                }
            }

            private void SubcategoryLink_Clicked(object? sender, LinkLabelLinkClickedEventArgs e)
            {
                if (sender is LinkLabel link && link.Tag is string target)
                {
                    NavigationRequested?.Invoke(this, new NavigationEventArgs(target));
                }
            }
        }

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
