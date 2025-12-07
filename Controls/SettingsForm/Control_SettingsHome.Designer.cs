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
            label1 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            SettingsForm_Button_Help_General = new Button();
            Control_SettingsHome_TableLayoutPanel_Cards.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // Control_SettingsHome_TableLayoutPanel_Cards
            // 
            Control_SettingsHome_TableLayoutPanel_Cards.AutoScroll = true;
            Control_SettingsHome_TableLayoutPanel_Cards.AutoSize = true;
            Control_SettingsHome_TableLayoutPanel_Cards.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_SettingsHome_TableLayoutPanel_Cards.ColumnCount = 2;
            Control_SettingsHome_TableLayoutPanel_Cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            Control_SettingsHome_TableLayoutPanel_Cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            Control_SettingsHome_TableLayoutPanel_Cards.Controls.Add(Control_SettingsHome_Tile_Database, 0, 5);
            Control_SettingsHome_TableLayoutPanel_Cards.Controls.Add(Control_SettingsHome_Tile_About, 1, 5);
            Control_SettingsHome_TableLayoutPanel_Cards.Controls.Add(Control_SettingsHome_Tile_Shortcuts, 1, 4);
            Control_SettingsHome_TableLayoutPanel_Cards.Controls.Add(Control_SettingsHome_Tile_Theme, 0, 4);
            Control_SettingsHome_TableLayoutPanel_Cards.Controls.Add(Control_SettingsHome_Card_Operations, 0, 3);
            Control_SettingsHome_TableLayoutPanel_Cards.Controls.Add(Control_SettingsHome_Card_Locations, 1, 3);
            Control_SettingsHome_TableLayoutPanel_Cards.Controls.Add(Control_SettingsHome_Card_ItemTypes, 1, 2);
            Control_SettingsHome_TableLayoutPanel_Cards.Controls.Add(Control_SettingsHome_Card_Parts, 0, 2);
            Control_SettingsHome_TableLayoutPanel_Cards.Controls.Add(Control_SettingsHome_Card_Users, 0, 1);
            Control_SettingsHome_TableLayoutPanel_Cards.Controls.Add(tableLayoutPanel1, 0, 0);
            Control_SettingsHome_TableLayoutPanel_Cards.Dock = DockStyle.Fill;
            Control_SettingsHome_TableLayoutPanel_Cards.Location = new Point(0, 0);
            Control_SettingsHome_TableLayoutPanel_Cards.Name = "Control_SettingsHome_TableLayoutPanel_Cards";
            Control_SettingsHome_TableLayoutPanel_Cards.Padding = new Padding(3);
            Control_SettingsHome_TableLayoutPanel_Cards.RowCount = 6;
            Control_SettingsHome_TableLayoutPanel_Cards.RowStyles.Add(new RowStyle());
            Control_SettingsHome_TableLayoutPanel_Cards.RowStyles.Add(new RowStyle());
            Control_SettingsHome_TableLayoutPanel_Cards.RowStyles.Add(new RowStyle());
            Control_SettingsHome_TableLayoutPanel_Cards.RowStyles.Add(new RowStyle());
            Control_SettingsHome_TableLayoutPanel_Cards.RowStyles.Add(new RowStyle());
            Control_SettingsHome_TableLayoutPanel_Cards.RowStyles.Add(new RowStyle());
            Control_SettingsHome_TableLayoutPanel_Cards.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            Control_SettingsHome_TableLayoutPanel_Cards.Size = new Size(719, 607);
            Control_SettingsHome_TableLayoutPanel_Cards.TabIndex = 5;
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
            Control_SettingsHome_Card_Operations.Location = new Point(8, 266);
            Control_SettingsHome_Card_Operations.Margin = new Padding(5);
            Control_SettingsHome_Card_Operations.Name = "Control_SettingsHome_Card_Operations";
            Control_SettingsHome_Card_Operations.NavigationTarget = null;
            Control_SettingsHome_Card_Operations.Size = new Size(346, 97);
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
            Control_SettingsHome_Card_Users.Location = new Point(8, 52);
            Control_SettingsHome_Card_Users.Margin = new Padding(5);
            Control_SettingsHome_Card_Users.Name = "Control_SettingsHome_Card_Users";
            Control_SettingsHome_Card_Users.NavigationTarget = null;
            Control_SettingsHome_Card_Users.Size = new Size(703, 97);
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
            Control_SettingsHome_Card_Parts.Location = new Point(8, 159);
            Control_SettingsHome_Card_Parts.Margin = new Padding(5);
            Control_SettingsHome_Card_Parts.Name = "Control_SettingsHome_Card_Parts";
            Control_SettingsHome_Card_Parts.NavigationTarget = null;
            Control_SettingsHome_Card_Parts.Size = new Size(346, 97);
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
            Control_SettingsHome_Card_Locations.Location = new Point(364, 266);
            Control_SettingsHome_Card_Locations.Margin = new Padding(5);
            Control_SettingsHome_Card_Locations.Name = "Control_SettingsHome_Card_Locations";
            Control_SettingsHome_Card_Locations.NavigationTarget = null;
            Control_SettingsHome_Card_Locations.Size = new Size(347, 97);
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
            Control_SettingsHome_Card_ItemTypes.Location = new Point(364, 159);
            Control_SettingsHome_Card_ItemTypes.Margin = new Padding(5);
            Control_SettingsHome_Card_ItemTypes.Name = "Control_SettingsHome_Card_ItemTypes";
            Control_SettingsHome_Card_ItemTypes.NavigationTarget = null;
            Control_SettingsHome_Card_ItemTypes.Size = new Size(347, 97);
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
            Control_SettingsHome_Tile_Database.Location = new Point(8, 471);
            Control_SettingsHome_Tile_Database.Margin = new Padding(5);
            Control_SettingsHome_Tile_Database.Name = "Control_SettingsHome_Tile_Database";
            Control_SettingsHome_Tile_Database.NavigationTarget = "Database";
            Control_SettingsHome_Tile_Database.Size = new Size(346, 128);
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
            Control_SettingsHome_Tile_Theme.Location = new Point(8, 373);
            Control_SettingsHome_Tile_Theme.Margin = new Padding(5);
            Control_SettingsHome_Tile_Theme.Name = "Control_SettingsHome_Tile_Theme";
            Control_SettingsHome_Tile_Theme.NavigationTarget = "Theme";
            Control_SettingsHome_Tile_Theme.Size = new Size(346, 88);
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
            Control_SettingsHome_Tile_Shortcuts.Location = new Point(364, 373);
            Control_SettingsHome_Tile_Shortcuts.Margin = new Padding(5);
            Control_SettingsHome_Tile_Shortcuts.Name = "Control_SettingsHome_Tile_Shortcuts";
            Control_SettingsHome_Tile_Shortcuts.NavigationTarget = "Shortcuts";
            Control_SettingsHome_Tile_Shortcuts.Size = new Size(347, 88);
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
            Control_SettingsHome_Tile_About.Location = new Point(364, 471);
            Control_SettingsHome_Tile_About.Margin = new Padding(5);
            Control_SettingsHome_Tile_About.Name = "Control_SettingsHome_Tile_About";
            Control_SettingsHome_Tile_About.NavigationTarget = "About";
            Control_SettingsHome_Tile_About.Size = new Size(347, 128);
            Control_SettingsHome_Tile_About.TabIndex = 8;
            Control_SettingsHome_Tile_About.TileDescription = "View application information and version";
            Control_SettingsHome_Tile_About.TileIcon = "ℹ️";
            Control_SettingsHome_Tile_About.TileTitle = "About";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Segoe UI Emoji", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(3, 3);
            label1.Margin = new Padding(3);
            label1.Name = "label1";
            label1.Size = new Size(248, 32);
            label1.TabIndex = 10;
            label1.Text = "MTM WIP Application Settings";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 3;
            Control_SettingsHome_TableLayoutPanel_Cards.SetColumnSpan(tableLayoutPanel1, 2);
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(SettingsForm_Button_Help_General, 2, 0);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(6, 6);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(707, 38);
            tableLayoutPanel1.TabIndex = 11;
            // 
            // SettingsForm_Button_Help_General
            // 
            SettingsForm_Button_Help_General.AutoSize = true;
            SettingsForm_Button_Help_General.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            SettingsForm_Button_Help_General.Dock = DockStyle.Right;
            SettingsForm_Button_Help_General.Location = new Point(672, 3);
            SettingsForm_Button_Help_General.MaximumSize = new Size(32, 32);
            SettingsForm_Button_Help_General.MinimumSize = new Size(32, 32);
            SettingsForm_Button_Help_General.Name = "SettingsForm_Button_Help_General";
            SettingsForm_Button_Help_General.Size = new Size(32, 32);
            SettingsForm_Button_Help_General.TabIndex = 11;
            SettingsForm_Button_Help_General.Text = "?";
            SettingsForm_Button_Help_General.UseVisualStyleBackColor = true;
            // 
            // Control_SettingsHome
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = SystemColors.Control;
            Controls.Add(Control_SettingsHome_TableLayoutPanel_Cards);
            Name = "Control_SettingsHome";
            Size = new Size(719, 607);
            Control_SettingsHome_TableLayoutPanel_Cards.ResumeLayout(false);
            Control_SettingsHome_TableLayoutPanel_Cards.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel Control_SettingsHome_TableLayoutPanel_Cards;
        private Control_SettingsCategoryTile Control_SettingsHome_Tile_Database;
        private Control_SettingsCategoryTile Control_SettingsHome_Tile_About;
        private Control_SettingsCategoryTile Control_SettingsHome_Tile_Shortcuts;
        private Control_SettingsCategoryTile Control_SettingsHome_Tile_Theme;
        private Control_SettingsCategoryCard Control_SettingsHome_Card_Operations;
        private Control_SettingsCategoryCard Control_SettingsHome_Card_Locations;
        private Control_SettingsCategoryCard Control_SettingsHome_Card_ItemTypes;
        private Control_SettingsCategoryCard Control_SettingsHome_Card_Parts;
        private Control_SettingsCategoryCard Control_SettingsHome_Card_Users;
        private TableLayoutPanel tableLayoutPanel1;
        private Button SettingsForm_Button_Help_General;
        private Label label1;
    }
}
