using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    partial class Control_SettingsCategoryTile
    {
        #region Fields

        private IContainer components = null!;
        private Panel Control_SettingsCategoryTile_Panel_Main = null!;
        private Panel Control_SettingsCategoryTile_Panel_AccentBar = null!;
        private Panel Control_SettingsCategoryTile_Panel_Content = null!;
        private Label Control_SettingsCategoryTile_Label_Title = null!;
        private Label Control_SettingsCategoryTile_Label_Description = null!;

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
            Control_SettingsCategoryTile_Panel_Main = new Panel();
            Control_SettingsCategoryTile_Panel_Content = new Panel();
            Control_SettingsCategoryTile_TableLayoutPanel_Content = new TableLayoutPanel();
            Control_SettingsCategoryTile_Label_Title = new Label();
            Control_SettingsCategoryTile_TableLayoutPanel_Icon = new TableLayoutPanel();
            Control_SettingsCategoryTile_Label_Icon = new Label();
            Control_SettingsCategoryTile_Label_Description = new Label();
            Control_SettingsCategoryTile_Panel_AccentBar = new Panel();
            Control_SettingsCategoryTile_Panel_Main.SuspendLayout();
            Control_SettingsCategoryTile_Panel_Content.SuspendLayout();
            Control_SettingsCategoryTile_TableLayoutPanel_Content.SuspendLayout();
            Control_SettingsCategoryTile_TableLayoutPanel_Icon.SuspendLayout();
            SuspendLayout();
            // 
            // Control_SettingsCategoryTile_Panel_Main
            // 
            Control_SettingsCategoryTile_Panel_Main.AutoSize = true;
            Control_SettingsCategoryTile_Panel_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_SettingsCategoryTile_Panel_Main.Controls.Add(Control_SettingsCategoryTile_Panel_Content);
            Control_SettingsCategoryTile_Panel_Main.Controls.Add(Control_SettingsCategoryTile_Panel_AccentBar);
            Control_SettingsCategoryTile_Panel_Main.Dock = DockStyle.Fill;
            Control_SettingsCategoryTile_Panel_Main.Location = new Point(0, 0);
            Control_SettingsCategoryTile_Panel_Main.Name = "Control_SettingsCategoryTile_Panel_Main";
            Control_SettingsCategoryTile_Panel_Main.Padding = new Padding(3);
            Control_SettingsCategoryTile_Panel_Main.Size = new Size(225, 110);
            Control_SettingsCategoryTile_Panel_Main.TabIndex = 0;
            // 
            // Control_SettingsCategoryTile_Panel_Content
            // 
            Control_SettingsCategoryTile_Panel_Content.AutoSize = true;
            Control_SettingsCategoryTile_Panel_Content.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_SettingsCategoryTile_Panel_Content.Controls.Add(Control_SettingsCategoryTile_TableLayoutPanel_Content);
            Control_SettingsCategoryTile_Panel_Content.Dock = DockStyle.Fill;
            Control_SettingsCategoryTile_Panel_Content.Location = new Point(15, 19);
            Control_SettingsCategoryTile_Panel_Content.Name = "Control_SettingsCategoryTile_Panel_Content";
            Control_SettingsCategoryTile_Panel_Content.Size = new Size(195, 76);
            Control_SettingsCategoryTile_Panel_Content.TabIndex = 1;
            // 
            // Control_SettingsCategoryTile_TableLayoutPanel_Content
            // 
            Control_SettingsCategoryTile_TableLayoutPanel_Content.AutoSize = true;
            Control_SettingsCategoryTile_TableLayoutPanel_Content.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_SettingsCategoryTile_TableLayoutPanel_Content.ColumnCount = 2;
            Control_SettingsCategoryTile_TableLayoutPanel_Content.ColumnStyles.Add(new ColumnStyle());
            Control_SettingsCategoryTile_TableLayoutPanel_Content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_SettingsCategoryTile_TableLayoutPanel_Content.Controls.Add(Control_SettingsCategoryTile_Label_Title, 1, 0);
            Control_SettingsCategoryTile_TableLayoutPanel_Content.Controls.Add(Control_SettingsCategoryTile_TableLayoutPanel_Icon, 0, 0);
            Control_SettingsCategoryTile_TableLayoutPanel_Content.Controls.Add(Control_SettingsCategoryTile_Label_Description, 1, 1);
            Control_SettingsCategoryTile_TableLayoutPanel_Content.Dock = DockStyle.Fill;
            Control_SettingsCategoryTile_TableLayoutPanel_Content.Location = new Point(0, 0);
            Control_SettingsCategoryTile_TableLayoutPanel_Content.Name = "Control_SettingsCategoryTile_TableLayoutPanel_Content";
            Control_SettingsCategoryTile_TableLayoutPanel_Content.RowCount = 2;
            Control_SettingsCategoryTile_TableLayoutPanel_Content.RowStyles.Add(new RowStyle());
            Control_SettingsCategoryTile_TableLayoutPanel_Content.RowStyles.Add(new RowStyle());
            Control_SettingsCategoryTile_TableLayoutPanel_Content.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            Control_SettingsCategoryTile_TableLayoutPanel_Content.Size = new Size(195, 76);
            Control_SettingsCategoryTile_TableLayoutPanel_Content.TabIndex = 3;
            // 
            // Control_SettingsCategoryTile_Label_Title
            // 
            Control_SettingsCategoryTile_Label_Title.AutoSize = true;
            Control_SettingsCategoryTile_Label_Title.Dock = DockStyle.Fill;
            Control_SettingsCategoryTile_Label_Title.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            Control_SettingsCategoryTile_Label_Title.ForeColor = Color.FromArgb(45, 45, 45);
            Control_SettingsCategoryTile_Label_Title.Location = new Point(77, 1);
            Control_SettingsCategoryTile_Label_Title.Margin = new Padding(0);
            Control_SettingsCategoryTile_Label_Title.MaximumSize = new Size(0, 36);
            Control_SettingsCategoryTile_Label_Title.MinimumSize = new Size(0, 36);
            Control_SettingsCategoryTile_Label_Title.Name = "Control_SettingsCategoryTile_Label_Title";
            Control_SettingsCategoryTile_Label_Title.Size = new Size(117, 36);
            Control_SettingsCategoryTile_Label_Title.TabIndex = 1;
            Control_SettingsCategoryTile_Label_Title.Text = "Category";
            Control_SettingsCategoryTile_Label_Title.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_SettingsCategoryTile_TableLayoutPanel_Icon
            // 
            Control_SettingsCategoryTile_TableLayoutPanel_Icon.AutoSize = true;
            Control_SettingsCategoryTile_TableLayoutPanel_Icon.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_SettingsCategoryTile_TableLayoutPanel_Icon.ColumnCount = 1;
            Control_SettingsCategoryTile_TableLayoutPanel_Icon.ColumnStyles.Add(new ColumnStyle());
            Control_SettingsCategoryTile_TableLayoutPanel_Icon.Controls.Add(Control_SettingsCategoryTile_Label_Icon, 0, 0);
            Control_SettingsCategoryTile_TableLayoutPanel_Icon.Dock = DockStyle.Fill;
            Control_SettingsCategoryTile_TableLayoutPanel_Icon.Location = new Point(3, 3);
            Control_SettingsCategoryTile_TableLayoutPanel_Icon.Name = "Control_SettingsCategoryTile_TableLayoutPanel_Icon";
            Control_SettingsCategoryTile_TableLayoutPanel_Icon.RowCount = 1;
            Control_SettingsCategoryTile_TableLayoutPanel_Content.SetRowSpan(Control_SettingsCategoryTile_TableLayoutPanel_Icon, 2);
            Control_SettingsCategoryTile_TableLayoutPanel_Icon.RowStyles.Add(new RowStyle());
            Control_SettingsCategoryTile_TableLayoutPanel_Icon.Size = new Size(70, 70);
            Control_SettingsCategoryTile_TableLayoutPanel_Icon.TabIndex = 3;
            // 
            // Control_SettingsCategoryTile_Label_Icon
            // 
            Control_SettingsCategoryTile_Label_Icon.AutoSize = true;
            Control_SettingsCategoryTile_Label_Icon.Dock = DockStyle.Fill;
            Control_SettingsCategoryTile_Label_Icon.Font = new Font("Segoe UI Emoji", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Control_SettingsCategoryTile_Label_Icon.Location = new Point(0, 0);
            Control_SettingsCategoryTile_Label_Icon.Margin = new Padding(0);
            Control_SettingsCategoryTile_Label_Icon.MaximumSize = new Size(70, 70);
            Control_SettingsCategoryTile_Label_Icon.MinimumSize = new Size(70, 70);
            Control_SettingsCategoryTile_Label_Icon.Name = "Control_SettingsCategoryTile_Label_Icon";
            Control_SettingsCategoryTile_Label_Icon.Size = new Size(70, 70);
            Control_SettingsCategoryTile_Label_Icon.TabIndex = 2;
            Control_SettingsCategoryTile_Label_Icon.Text = "🗄️";
            Control_SettingsCategoryTile_Label_Icon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_SettingsCategoryTile_Label_Description
            // 
            Control_SettingsCategoryTile_Label_Description.AutoSize = true;
            Control_SettingsCategoryTile_Label_Description.Dock = DockStyle.Fill;
            Control_SettingsCategoryTile_Label_Description.Font = new Font("Segoe UI", 9F);
            Control_SettingsCategoryTile_Label_Description.ForeColor = Color.FromArgb(96, 94, 92);
            Control_SettingsCategoryTile_Label_Description.Location = new Point(77, 39);
            Control_SettingsCategoryTile_Label_Description.Margin = new Padding(0);
            Control_SettingsCategoryTile_Label_Description.MaximumSize = new Size(0, 36);
            Control_SettingsCategoryTile_Label_Description.MinimumSize = new Size(0, 36);
            Control_SettingsCategoryTile_Label_Description.Name = "Control_SettingsCategoryTile_Label_Description";
            Control_SettingsCategoryTile_Label_Description.Size = new Size(117, 36);
            Control_SettingsCategoryTile_Label_Description.TabIndex = 2;
            Control_SettingsCategoryTile_Label_Description.Text = "Category description";
            Control_SettingsCategoryTile_Label_Description.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_SettingsCategoryTile_Panel_AccentBar
            // 
            Control_SettingsCategoryTile_Panel_AccentBar.BackColor = Color.FromArgb(0, 120, 212);
            Control_SettingsCategoryTile_Panel_AccentBar.Dock = DockStyle.Top;
            Control_SettingsCategoryTile_Panel_AccentBar.Location = new Point(15, 15);
            Control_SettingsCategoryTile_Panel_AccentBar.Name = "Control_SettingsCategoryTile_Panel_AccentBar";
            Control_SettingsCategoryTile_Panel_AccentBar.Size = new Size(195, 4);
            Control_SettingsCategoryTile_Panel_AccentBar.TabIndex = 0;
            // 
            // Control_SettingsCategoryTile
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.White;
            Controls.Add(Control_SettingsCategoryTile_Panel_Main);
            Cursor = Cursors.Hand;
            Margin = new Padding(5);
            Name = "Control_SettingsCategoryTile";
            Size = new Size(225, 110);
            Control_SettingsCategoryTile_Panel_Main.ResumeLayout(false);
            Control_SettingsCategoryTile_Panel_Main.PerformLayout();
            Control_SettingsCategoryTile_Panel_Content.ResumeLayout(false);
            Control_SettingsCategoryTile_Panel_Content.PerformLayout();
            Control_SettingsCategoryTile_TableLayoutPanel_Content.ResumeLayout(false);
            Control_SettingsCategoryTile_TableLayoutPanel_Content.PerformLayout();
            Control_SettingsCategoryTile_TableLayoutPanel_Icon.ResumeLayout(false);
            Control_SettingsCategoryTile_TableLayoutPanel_Icon.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel Control_SettingsCategoryTile_TableLayoutPanel_Content;
        private TableLayoutPanel Control_SettingsCategoryTile_TableLayoutPanel_Icon;
        private Label Control_SettingsCategoryTile_Label_Icon;
    }
}
