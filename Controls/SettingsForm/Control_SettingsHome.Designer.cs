using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    partial class Control_SettingsHome
    {
        #region Fields

        private IContainer components = null!;

        #endregion

        #region Methods

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            flowLayoutPanel1 = new FlowLayoutPanel();
            Control_SettingsHome_TableLayoutPanel_Cards = new TableLayoutPanel();
            Control_SettingsHome_Card_Operations = new Control_SettingsCategoryCard();
            Control_SettingsHome_Card_Users = new Control_SettingsCategoryCard();
            Control_SettingsHome_Card_Parts = new Control_SettingsCategoryCard();
            Control_SettingsHome_Card_Locations = new Control_SettingsCategoryCard();
            Control_SettingsHome_Card_ItemTypes = new Control_SettingsCategoryCard();
            Control_SettingsHome_Tile_Database = new Control_SettingsCategoryTile();
            Control_SettingsHome_Tile_Theme = new Control_SettingsCategoryTile();
            Control_SettingsHome_Tile_Shortcuts = new Control_SettingsCategoryTile();
            Control_SettingsHome_Tile_About = new Control_SettingsCategoryTile();
            flowLayoutPanel1.SuspendLayout();
            Control_SettingsHome_TableLayoutPanel_Cards.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Controls.Add(Control_SettingsHome_TableLayoutPanel_Cards);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(780, 423);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // Control_SettingsHome_TableLayoutPanel_Cards
            // 
            Control_SettingsHome_TableLayoutPanel_Cards.AutoScroll = true;
            Control_SettingsHome_TableLayoutPanel_Cards.AutoSize = true;
            Control_SettingsHome_TableLayoutPanel_Cards.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_SettingsHome_TableLayoutPanel_Cards.ColumnCount = 2;
            Control_SettingsHome_TableLayoutPanel_Cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            Control_SettingsHome_TableLayoutPanel_Cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            Control_SettingsHome_TableLayoutPanel_Cards.Controls.Add(Control_SettingsHome_Card_Operations, 0, 2);
            Control_SettingsHome_TableLayoutPanel_Cards.Controls.Add(Control_SettingsHome_Card_Users, 0, 0);
            Control_SettingsHome_TableLayoutPanel_Cards.Controls.Add(Control_SettingsHome_Card_Parts, 0, 1);
            Control_SettingsHome_TableLayoutPanel_Cards.Controls.Add(Control_SettingsHome_Card_Locations, 1, 2);
            Control_SettingsHome_TableLayoutPanel_Cards.Controls.Add(Control_SettingsHome_Card_ItemTypes, 1, 1);
            Control_SettingsHome_TableLayoutPanel_Cards.Controls.Add(Control_SettingsHome_Tile_Database, 0, 4);
            Control_SettingsHome_TableLayoutPanel_Cards.Controls.Add(Control_SettingsHome_Tile_Theme, 0, 3);
            Control_SettingsHome_TableLayoutPanel_Cards.Controls.Add(Control_SettingsHome_Tile_Shortcuts, 1, 3);
            Control_SettingsHome_TableLayoutPanel_Cards.Controls.Add(Control_SettingsHome_Tile_About, 1, 4);
            Control_SettingsHome_TableLayoutPanel_Cards.Dock = DockStyle.Fill;
            Control_SettingsHome_TableLayoutPanel_Cards.Location = new Point(3, 3);
            Control_SettingsHome_TableLayoutPanel_Cards.Name = "Control_SettingsHome_TableLayoutPanel_Cards";
            Control_SettingsHome_TableLayoutPanel_Cards.Padding = new Padding(3);
            Control_SettingsHome_TableLayoutPanel_Cards.RowCount = 5;
            Control_SettingsHome_TableLayoutPanel_Cards.RowStyles.Add(new RowStyle());
            Control_SettingsHome_TableLayoutPanel_Cards.RowStyles.Add(new RowStyle());
            Control_SettingsHome_TableLayoutPanel_Cards.RowStyles.Add(new RowStyle());
            Control_SettingsHome_TableLayoutPanel_Cards.RowStyles.Add(new RowStyle());
            Control_SettingsHome_TableLayoutPanel_Cards.RowStyles.Add(new RowStyle());
            Control_SettingsHome_TableLayoutPanel_Cards.Size = new Size(740, 589);
            Control_SettingsHome_TableLayoutPanel_Cards.TabIndex = 4;
            // 
            // Control_SettingsHome_Card_Operations
            // 
            Control_SettingsHome_Card_Operations.AccentColor = Color.FromArgb(255, 140, 0);
            Control_SettingsHome_Card_Operations.AutoSize = true;
            Control_SettingsHome_Card_Operations.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_SettingsHome_Card_Operations.BackColor = Color.White;
            Control_SettingsHome_Card_Operations.BorderStyle = BorderStyle.FixedSingle;
            Control_SettingsHome_Card_Operations.CardDescription = "Add, edit, or remove operation codes";
            Control_SettingsHome_Card_Operations.CardIcon = "⚙️";
            Control_SettingsHome_Card_Operations.CardTitle = "Operations";
            Control_SettingsHome_Card_Operations.Cursor = Cursors.Hand;
            Control_SettingsHome_Card_Operations.Dock = DockStyle.Fill;
            Control_SettingsHome_Card_Operations.Location = new Point(8, 234);
            Control_SettingsHome_Card_Operations.Margin = new Padding(5);
            Control_SettingsHome_Card_Operations.Name = "Control_SettingsHome_Card_Operations";
            Control_SettingsHome_Card_Operations.Size = new Size(357, 103);
            Control_SettingsHome_Card_Operations.TabIndex = 3;
            // 
            // Control_SettingsHome_Card_Users
            // 
            Control_SettingsHome_Card_Users.AccentColor = Color.FromArgb(16, 124, 16);
            Control_SettingsHome_Card_Users.AutoSize = true;
            Control_SettingsHome_Card_Users.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_SettingsHome_Card_Users.BackColor = Color.White;
            Control_SettingsHome_Card_Users.BorderStyle = BorderStyle.FixedSingle;
            Control_SettingsHome_Card_Users.CardDescription = "Manage user accounts and permissions";
            Control_SettingsHome_Card_Users.CardIcon = "👥";
            Control_SettingsHome_Card_Users.CardTitle = "Users";
            Control_SettingsHome_TableLayoutPanel_Cards.SetColumnSpan(Control_SettingsHome_Card_Users, 2);
            Control_SettingsHome_Card_Users.Cursor = Cursors.Hand;
            Control_SettingsHome_Card_Users.Dock = DockStyle.Fill;
            Control_SettingsHome_Card_Users.Location = new Point(8, 8);
            Control_SettingsHome_Card_Users.Margin = new Padding(5);
            Control_SettingsHome_Card_Users.Name = "Control_SettingsHome_Card_Users";
            Control_SettingsHome_Card_Users.Size = new Size(724, 103);
            Control_SettingsHome_Card_Users.TabIndex = 1;
            // 
            // Control_SettingsHome_Card_Parts
            // 
            Control_SettingsHome_Card_Parts.AccentColor = Color.FromArgb(232, 17, 35);
            Control_SettingsHome_Card_Parts.AutoSize = true;
            Control_SettingsHome_Card_Parts.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_SettingsHome_Card_Parts.BackColor = Color.White;
            Control_SettingsHome_Card_Parts.BorderStyle = BorderStyle.FixedSingle;
            Control_SettingsHome_Card_Parts.CardDescription = "Add, edit, or remove part numbers";
            Control_SettingsHome_Card_Parts.CardIcon = "📦";
            Control_SettingsHome_Card_Parts.CardTitle = "Part Numbers";
            Control_SettingsHome_Card_Parts.Cursor = Cursors.Hand;
            Control_SettingsHome_Card_Parts.Dock = DockStyle.Fill;
            Control_SettingsHome_Card_Parts.Location = new Point(8, 121);
            Control_SettingsHome_Card_Parts.Margin = new Padding(5);
            Control_SettingsHome_Card_Parts.Name = "Control_SettingsHome_Card_Parts";
            Control_SettingsHome_Card_Parts.Size = new Size(357, 103);
            Control_SettingsHome_Card_Parts.TabIndex = 2;
            // 
            // Control_SettingsHome_Card_Locations
            // 
            Control_SettingsHome_Card_Locations.AccentColor = Color.FromArgb(142, 68, 173);
            Control_SettingsHome_Card_Locations.AutoSize = true;
            Control_SettingsHome_Card_Locations.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_SettingsHome_Card_Locations.BackColor = Color.White;
            Control_SettingsHome_Card_Locations.BorderStyle = BorderStyle.FixedSingle;
            Control_SettingsHome_Card_Locations.CardDescription = "Add, edit, or remove storage locations";
            Control_SettingsHome_Card_Locations.CardIcon = "📍";
            Control_SettingsHome_Card_Locations.CardTitle = "Locations";
            Control_SettingsHome_Card_Locations.Cursor = Cursors.Hand;
            Control_SettingsHome_Card_Locations.Dock = DockStyle.Fill;
            Control_SettingsHome_Card_Locations.Location = new Point(375, 234);
            Control_SettingsHome_Card_Locations.Margin = new Padding(5);
            Control_SettingsHome_Card_Locations.Name = "Control_SettingsHome_Card_Locations";
            Control_SettingsHome_Card_Locations.Size = new Size(357, 103);
            Control_SettingsHome_Card_Locations.TabIndex = 4;
            // 
            // Control_SettingsHome_Card_ItemTypes
            // 
            Control_SettingsHome_Card_ItemTypes.AccentColor = Color.FromArgb(0, 153, 188);
            Control_SettingsHome_Card_ItemTypes.AutoSize = true;
            Control_SettingsHome_Card_ItemTypes.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_SettingsHome_Card_ItemTypes.BackColor = Color.White;
            Control_SettingsHome_Card_ItemTypes.BorderStyle = BorderStyle.FixedSingle;
            Control_SettingsHome_Card_ItemTypes.CardDescription = "Add, edit, or remove item type classifications";
            Control_SettingsHome_Card_ItemTypes.CardIcon = "🏷️";
            Control_SettingsHome_Card_ItemTypes.CardTitle = "ItemTypes";
            Control_SettingsHome_Card_ItemTypes.Cursor = Cursors.Hand;
            Control_SettingsHome_Card_ItemTypes.Dock = DockStyle.Fill;
            Control_SettingsHome_Card_ItemTypes.Location = new Point(375, 121);
            Control_SettingsHome_Card_ItemTypes.Margin = new Padding(5);
            Control_SettingsHome_Card_ItemTypes.Name = "Control_SettingsHome_Card_ItemTypes";
            Control_SettingsHome_Card_ItemTypes.Size = new Size(357, 103);
            Control_SettingsHome_Card_ItemTypes.TabIndex = 5;
            // 
            // Control_SettingsHome_Tile_Database
            // 
            Control_SettingsHome_Tile_Database.AccentColor = Color.FromArgb(0, 120, 212);
            Control_SettingsHome_Tile_Database.AutoSize = true;
            Control_SettingsHome_Tile_Database.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_SettingsHome_Tile_Database.BackColor = Color.White;
            Control_SettingsHome_Tile_Database.BorderStyle = BorderStyle.FixedSingle;
            Control_SettingsHome_Tile_Database.Cursor = Cursors.Hand;
            Control_SettingsHome_Tile_Database.Dock = DockStyle.Fill;
            Control_SettingsHome_Tile_Database.Location = new Point(8, 469);
            Control_SettingsHome_Tile_Database.Margin = new Padding(5);
            Control_SettingsHome_Tile_Database.Name = "Control_SettingsHome_Tile_Database";
            Control_SettingsHome_Tile_Database.NavigationTarget = "Database";
            Control_SettingsHome_Tile_Database.Size = new Size(357, 112);
            Control_SettingsHome_Tile_Database.TabIndex = 0;
            Control_SettingsHome_Tile_Database.TileDescription = "Configure database connection and settings";
            Control_SettingsHome_Tile_Database.TileIcon = "🗄️";
            Control_SettingsHome_Tile_Database.TileTitle = "Database";
            // 
            // Control_SettingsHome_Tile_Theme
            // 
            Control_SettingsHome_Tile_Theme.AccentColor = Color.FromArgb(104, 33, 122);
            Control_SettingsHome_Tile_Theme.AutoSize = true;
            Control_SettingsHome_Tile_Theme.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_SettingsHome_Tile_Theme.BackColor = Color.White;
            Control_SettingsHome_Tile_Theme.BorderStyle = BorderStyle.FixedSingle;
            Control_SettingsHome_Tile_Theme.Cursor = Cursors.Hand;
            Control_SettingsHome_Tile_Theme.Dock = DockStyle.Fill;
            Control_SettingsHome_Tile_Theme.Location = new Point(8, 347);
            Control_SettingsHome_Tile_Theme.Margin = new Padding(5);
            Control_SettingsHome_Tile_Theme.Name = "Control_SettingsHome_Tile_Theme";
            Control_SettingsHome_Tile_Theme.NavigationTarget = "Theme";
            Control_SettingsHome_Tile_Theme.Size = new Size(357, 112);
            Control_SettingsHome_Tile_Theme.TabIndex = 6;
            Control_SettingsHome_Tile_Theme.TileDescription = "Customize application appearance and colors";
            Control_SettingsHome_Tile_Theme.TileIcon = "🎨";
            Control_SettingsHome_Tile_Theme.TileTitle = "Theme";
            // 
            // Control_SettingsHome_Tile_Shortcuts
            // 
            Control_SettingsHome_Tile_Shortcuts.AccentColor = Color.FromArgb(0, 99, 177);
            Control_SettingsHome_Tile_Shortcuts.AutoSize = true;
            Control_SettingsHome_Tile_Shortcuts.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_SettingsHome_Tile_Shortcuts.BackColor = Color.White;
            Control_SettingsHome_Tile_Shortcuts.BorderStyle = BorderStyle.FixedSingle;
            Control_SettingsHome_Tile_Shortcuts.Cursor = Cursors.Hand;
            Control_SettingsHome_Tile_Shortcuts.Dock = DockStyle.Fill;
            Control_SettingsHome_Tile_Shortcuts.Location = new Point(375, 347);
            Control_SettingsHome_Tile_Shortcuts.Margin = new Padding(5);
            Control_SettingsHome_Tile_Shortcuts.Name = "Control_SettingsHome_Tile_Shortcuts";
            Control_SettingsHome_Tile_Shortcuts.NavigationTarget = "Shortcuts";
            Control_SettingsHome_Tile_Shortcuts.Size = new Size(357, 112);
            Control_SettingsHome_Tile_Shortcuts.TabIndex = 7;
            Control_SettingsHome_Tile_Shortcuts.TileDescription = "Configure keyboard shortcuts";
            Control_SettingsHome_Tile_Shortcuts.TileIcon = "⌨️";
            Control_SettingsHome_Tile_Shortcuts.TileTitle = "Shortcuts";
            // 
            // Control_SettingsHome_Tile_About
            // 
            Control_SettingsHome_Tile_About.AccentColor = Color.FromArgb(76, 74, 72);
            Control_SettingsHome_Tile_About.AutoSize = true;
            Control_SettingsHome_Tile_About.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_SettingsHome_Tile_About.BackColor = Color.White;
            Control_SettingsHome_Tile_About.BorderStyle = BorderStyle.FixedSingle;
            Control_SettingsHome_Tile_About.Cursor = Cursors.Hand;
            Control_SettingsHome_Tile_About.Dock = DockStyle.Fill;
            Control_SettingsHome_Tile_About.Location = new Point(375, 469);
            Control_SettingsHome_Tile_About.Margin = new Padding(5);
            Control_SettingsHome_Tile_About.Name = "Control_SettingsHome_Tile_About";
            Control_SettingsHome_Tile_About.NavigationTarget = "About";
            Control_SettingsHome_Tile_About.Size = new Size(357, 112);
            Control_SettingsHome_Tile_About.TabIndex = 8;
            Control_SettingsHome_Tile_About.TileDescription = "View application information and version";
            Control_SettingsHome_Tile_About.TileIcon = "ℹ️";
            Control_SettingsHome_Tile_About.TileTitle = "About";
            // 
            // Control_SettingsHome
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            BackColor = SystemColors.Control;
            Controls.Add(flowLayoutPanel1);
            Name = "Control_SettingsHome";
            Size = new Size(780, 423);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            Control_SettingsHome_TableLayoutPanel_Cards.ResumeLayout(false);
            Control_SettingsHome_TableLayoutPanel_Cards.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private TableLayoutPanel Control_SettingsHome_TableLayoutPanel_Cards;
        private Control_SettingsCategoryCard Control_SettingsHome_Card_Operations;
        private Control_SettingsCategoryCard Control_SettingsHome_Card_Users;
        private Control_SettingsCategoryCard Control_SettingsHome_Card_Parts;
        private Control_SettingsCategoryCard Control_SettingsHome_Card_Locations;
        private Control_SettingsCategoryCard Control_SettingsHome_Card_ItemTypes;
        private Control_SettingsCategoryTile Control_SettingsHome_Tile_Database;
        private Control_SettingsCategoryTile Control_SettingsHome_Tile_Theme;
        private Control_SettingsCategoryTile Control_SettingsHome_Tile_Shortcuts;
        private Control_SettingsCategoryTile Control_SettingsHome_Tile_About;
    }
}
