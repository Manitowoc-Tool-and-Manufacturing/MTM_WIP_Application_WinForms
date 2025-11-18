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
        private Label Control_SettingsCategoryTile_Label_Icon = null!;
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
            Control_SettingsCategoryTile_Panel_AccentBar = new Panel();
            Control_SettingsCategoryTile_Panel_Content = new Panel();
            Control_SettingsCategoryTile_Label_Icon = new Label();
            Control_SettingsCategoryTile_Label_Title = new Label();
            Control_SettingsCategoryTile_Label_Description = new Label();
            Control_SettingsCategoryTile_Panel_Main.SuspendLayout();
            Control_SettingsCategoryTile_Panel_Content.SuspendLayout();
            SuspendLayout();
            // 
            // Control_SettingsCategoryTile_Panel_Main
            // 
            Control_SettingsCategoryTile_Panel_Main.Controls.Add(Control_SettingsCategoryTile_Panel_Content);
            Control_SettingsCategoryTile_Panel_Main.Controls.Add(Control_SettingsCategoryTile_Panel_AccentBar);
            Control_SettingsCategoryTile_Panel_Main.Dock = DockStyle.Fill;
            Control_SettingsCategoryTile_Panel_Main.Location = new Point(0, 0);
            Control_SettingsCategoryTile_Panel_Main.Name = "Control_SettingsCategoryTile_Panel_Main";
            Control_SettingsCategoryTile_Panel_Main.Padding = new Padding(15);
            Control_SettingsCategoryTile_Panel_Main.Size = new Size(280, 140);
            Control_SettingsCategoryTile_Panel_Main.TabIndex = 0;
            // 
            // Control_SettingsCategoryTile_Panel_AccentBar
            // 
            Control_SettingsCategoryTile_Panel_AccentBar.BackColor = Color.FromArgb(0, 120, 212);
            Control_SettingsCategoryTile_Panel_AccentBar.Dock = DockStyle.Top;
            Control_SettingsCategoryTile_Panel_AccentBar.Location = new Point(15, 15);
            Control_SettingsCategoryTile_Panel_AccentBar.Name = "Control_SettingsCategoryTile_Panel_AccentBar";
            Control_SettingsCategoryTile_Panel_AccentBar.Size = new Size(250, 4);
            Control_SettingsCategoryTile_Panel_AccentBar.TabIndex = 0;
            // 
            // Control_SettingsCategoryTile_Panel_Content
            // 
            Control_SettingsCategoryTile_Panel_Content.Controls.Add(Control_SettingsCategoryTile_Label_Description);
            Control_SettingsCategoryTile_Panel_Content.Controls.Add(Control_SettingsCategoryTile_Label_Title);
            Control_SettingsCategoryTile_Panel_Content.Controls.Add(Control_SettingsCategoryTile_Label_Icon);
            Control_SettingsCategoryTile_Panel_Content.Dock = DockStyle.Fill;
            Control_SettingsCategoryTile_Panel_Content.Location = new Point(15, 19);
            Control_SettingsCategoryTile_Panel_Content.Name = "Control_SettingsCategoryTile_Panel_Content";
            Control_SettingsCategoryTile_Panel_Content.Padding = new Padding(0, 10, 0, 0);
            Control_SettingsCategoryTile_Panel_Content.Size = new Size(250, 106);
            Control_SettingsCategoryTile_Panel_Content.TabIndex = 1;
            // 
            // Control_SettingsCategoryTile_Label_Icon
            // 
            Control_SettingsCategoryTile_Label_Icon.AutoSize = true;
            Control_SettingsCategoryTile_Label_Icon.Font = new Font("Segoe UI Emoji", 24F);
            Control_SettingsCategoryTile_Label_Icon.Location = new Point(0, 10);
            Control_SettingsCategoryTile_Label_Icon.Name = "Control_SettingsCategoryTile_Label_Icon";
            Control_SettingsCategoryTile_Label_Icon.Size = new Size(52, 43);
            Control_SettingsCategoryTile_Label_Icon.TabIndex = 0;
            Control_SettingsCategoryTile_Label_Icon.Text = "🗄️";
            // 
            // Control_SettingsCategoryTile_Label_Title
            // 
            Control_SettingsCategoryTile_Label_Title.AutoSize = true;
            Control_SettingsCategoryTile_Label_Title.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            Control_SettingsCategoryTile_Label_Title.ForeColor = Color.FromArgb(45, 45, 45);
            Control_SettingsCategoryTile_Label_Title.Location = new Point(0, 53);
            Control_SettingsCategoryTile_Label_Title.Name = "Control_SettingsCategoryTile_Label_Title";
            Control_SettingsCategoryTile_Label_Title.Size = new Size(90, 21);
            Control_SettingsCategoryTile_Label_Title.TabIndex = 1;
            Control_SettingsCategoryTile_Label_Title.Text = "Category";
            // 
            // Control_SettingsCategoryTile_Label_Description
            // 
            Control_SettingsCategoryTile_Label_Description.AutoSize = true;
            Control_SettingsCategoryTile_Label_Description.Font = new Font("Segoe UI", 9F);
            Control_SettingsCategoryTile_Label_Description.ForeColor = Color.FromArgb(96, 94, 92);
            Control_SettingsCategoryTile_Label_Description.Location = new Point(0, 74);
            Control_SettingsCategoryTile_Label_Description.MaximumSize = new Size(240, 0);
            Control_SettingsCategoryTile_Label_Description.Name = "Control_SettingsCategoryTile_Label_Description";
            Control_SettingsCategoryTile_Label_Description.Size = new Size(110, 15);
            Control_SettingsCategoryTile_Label_Description.TabIndex = 2;
            Control_SettingsCategoryTile_Label_Description.Text = "Category description";
            // 
            // Control_SettingsCategoryTile
            // 
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            Controls.Add(Control_SettingsCategoryTile_Panel_Main);
            Cursor = Cursors.Hand;
            Margin = new Padding(10);
            Name = "Control_SettingsCategoryTile";
            Size = new Size(280, 140);
            Control_SettingsCategoryTile_Panel_Main.ResumeLayout(false);
            Control_SettingsCategoryTile_Panel_Content.ResumeLayout(false);
            Control_SettingsCategoryTile_Panel_Content.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}
