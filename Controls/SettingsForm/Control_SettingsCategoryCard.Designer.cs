using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    partial class Control_SettingsCategoryCard
    {
        #region Fields

        private IContainer components = null!;
        private Panel Control_SettingsCategoryCard_Panel_Main = null!;
        private Panel Control_SettingsCategoryCard_Panel_AccentBar = null!;
        private Panel Control_SettingsCategoryCard_Panel_Content = null!;
        private Label Control_SettingsCategoryCard_Label_Icon = null!;
        private Label Control_SettingsCategoryCard_Label_Title = null!;
        private Label Control_SettingsCategoryCard_Label_Description = null!;
        private FlowLayoutPanel Control_SettingsCategoryCard_FlowPanel_Subcategories = null!;

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
            Control_SettingsCategoryCard_Panel_Main = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            Control_SettingsCategoryCard_FlowPanel_Subcategories = new FlowLayoutPanel();
            Control_SettingsCategoryCard_Panel_Content = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            Control_SettingsCategoryCard_Label_Title = new Label();
            Control_SettingsCategoryCard_Label_Icon = new Label();
            Control_SettingsCategoryCard_Label_Description = new Label();
            Control_SettingsCategoryCard_Panel_AccentBar = new Panel();
            Control_SettingsCategoryCard_Panel_Main.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            Control_SettingsCategoryCard_Panel_Content.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // Control_SettingsCategoryCard_Panel_Main
            // 
            Control_SettingsCategoryCard_Panel_Main.AutoSize = true;
            Control_SettingsCategoryCard_Panel_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_SettingsCategoryCard_Panel_Main.Controls.Add(tableLayoutPanel2);
            Control_SettingsCategoryCard_Panel_Main.Controls.Add(Control_SettingsCategoryCard_Panel_AccentBar);
            Control_SettingsCategoryCard_Panel_Main.Dock = DockStyle.Fill;
            Control_SettingsCategoryCard_Panel_Main.Location = new Point(0, 0);
            Control_SettingsCategoryCard_Panel_Main.Margin = new Padding(4, 4, 4, 4);
            Control_SettingsCategoryCard_Panel_Main.Name = "Control_SettingsCategoryCard_Panel_Main";
            Control_SettingsCategoryCard_Panel_Main.Padding = new Padding(4, 4, 4, 4);
            Control_SettingsCategoryCard_Panel_Main.Size = new Size(121, 139);
            Control_SettingsCategoryCard_Panel_Main.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(Control_SettingsCategoryCard_FlowPanel_Subcategories, 0, 1);
            tableLayoutPanel2.Controls.Add(Control_SettingsCategoryCard_Panel_Content, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(4, 10);
            tableLayoutPanel2.Margin = new Padding(4, 4, 4, 4);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(113, 125);
            tableLayoutPanel2.TabIndex = 3;
            // 
            // Control_SettingsCategoryCard_FlowPanel_Subcategories
            // 
            Control_SettingsCategoryCard_FlowPanel_Subcategories.AutoSize = true;
            Control_SettingsCategoryCard_FlowPanel_Subcategories.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_SettingsCategoryCard_FlowPanel_Subcategories.Dock = DockStyle.Fill;
            Control_SettingsCategoryCard_FlowPanel_Subcategories.FlowDirection = FlowDirection.TopDown;
            Control_SettingsCategoryCard_FlowPanel_Subcategories.Location = new Point(4, 117);
            Control_SettingsCategoryCard_FlowPanel_Subcategories.Margin = new Padding(4, 4, 4, 4);
            Control_SettingsCategoryCard_FlowPanel_Subcategories.Name = "Control_SettingsCategoryCard_FlowPanel_Subcategories";
            Control_SettingsCategoryCard_FlowPanel_Subcategories.Padding = new Padding(0, 4, 0, 0);
            Control_SettingsCategoryCard_FlowPanel_Subcategories.Size = new Size(105, 4);
            Control_SettingsCategoryCard_FlowPanel_Subcategories.TabIndex = 2;
            Control_SettingsCategoryCard_FlowPanel_Subcategories.WrapContents = false;
            // 
            // Control_SettingsCategoryCard_Panel_Content
            // 
            Control_SettingsCategoryCard_Panel_Content.AutoSize = true;
            Control_SettingsCategoryCard_Panel_Content.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_SettingsCategoryCard_Panel_Content.Controls.Add(tableLayoutPanel1);
            Control_SettingsCategoryCard_Panel_Content.Dock = DockStyle.Fill;
            Control_SettingsCategoryCard_Panel_Content.Location = new Point(0, 0);
            Control_SettingsCategoryCard_Panel_Content.Margin = new Padding(0);
            Control_SettingsCategoryCard_Panel_Content.Name = "Control_SettingsCategoryCard_Panel_Content";
            Control_SettingsCategoryCard_Panel_Content.Size = new Size(113, 113);
            Control_SettingsCategoryCard_Panel_Content.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(Control_SettingsCategoryCard_Label_Title, 1, 0);
            tableLayoutPanel1.Controls.Add(Control_SettingsCategoryCard_Label_Icon, 0, 0);
            tableLayoutPanel1.Controls.Add(Control_SettingsCategoryCard_Label_Description, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(113, 113);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // Control_SettingsCategoryCard_Label_Title
            // 
            Control_SettingsCategoryCard_Label_Title.Dock = DockStyle.Fill;
            Control_SettingsCategoryCard_Label_Title.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            Control_SettingsCategoryCard_Label_Title.ForeColor = Color.FromArgb(45, 45, 45);
            Control_SettingsCategoryCard_Label_Title.Location = new Point(113, 0);
            Control_SettingsCategoryCard_Label_Title.Margin = new Padding(0);
            Control_SettingsCategoryCard_Label_Title.MaximumSize = new Size(0, 54);
            Control_SettingsCategoryCard_Label_Title.MinimumSize = new Size(0, 54);
            Control_SettingsCategoryCard_Label_Title.Name = "Control_SettingsCategoryCard_Label_Title";
            Control_SettingsCategoryCard_Label_Title.Size = new Size(1, 54);
            Control_SettingsCategoryCard_Label_Title.TabIndex = 1;
            Control_SettingsCategoryCard_Label_Title.Text = "Category";
            Control_SettingsCategoryCard_Label_Title.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_SettingsCategoryCard_Label_Icon
            // 
            Control_SettingsCategoryCard_Label_Icon.AutoSize = true;
            Control_SettingsCategoryCard_Label_Icon.Dock = DockStyle.Fill;
            Control_SettingsCategoryCard_Label_Icon.Font = new Font("Segoe UI Emoji", 24F);
            Control_SettingsCategoryCard_Label_Icon.Location = new Point(4, 4);
            Control_SettingsCategoryCard_Label_Icon.Margin = new Padding(4, 4, 4, 4);
            Control_SettingsCategoryCard_Label_Icon.MaximumSize = new Size(105, 105);
            Control_SettingsCategoryCard_Label_Icon.MinimumSize = new Size(105, 105);
            Control_SettingsCategoryCard_Label_Icon.Name = "Control_SettingsCategoryCard_Label_Icon";
            Control_SettingsCategoryCard_Label_Icon.Padding = new Padding(4, 4, 4, 4);
            tableLayoutPanel1.SetRowSpan(Control_SettingsCategoryCard_Label_Icon, 2);
            Control_SettingsCategoryCard_Label_Icon.Size = new Size(105, 105);
            Control_SettingsCategoryCard_Label_Icon.TabIndex = 0;
            Control_SettingsCategoryCard_Label_Icon.Text = "👥";
            Control_SettingsCategoryCard_Label_Icon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_SettingsCategoryCard_Label_Description
            // 
            Control_SettingsCategoryCard_Label_Description.Dock = DockStyle.Fill;
            Control_SettingsCategoryCard_Label_Description.Font = new Font("Segoe UI", 9F);
            Control_SettingsCategoryCard_Label_Description.ForeColor = Color.FromArgb(96, 94, 92);
            Control_SettingsCategoryCard_Label_Description.Location = new Point(113, 54);
            Control_SettingsCategoryCard_Label_Description.Margin = new Padding(0);
            Control_SettingsCategoryCard_Label_Description.MaximumSize = new Size(0, 54);
            Control_SettingsCategoryCard_Label_Description.MinimumSize = new Size(0, 54);
            Control_SettingsCategoryCard_Label_Description.Name = "Control_SettingsCategoryCard_Label_Description";
            Control_SettingsCategoryCard_Label_Description.Size = new Size(1, 54);
            Control_SettingsCategoryCard_Label_Description.TabIndex = 2;
            Control_SettingsCategoryCard_Label_Description.Text = "Category description";
            Control_SettingsCategoryCard_Label_Description.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_SettingsCategoryCard_Panel_AccentBar
            // 
            Control_SettingsCategoryCard_Panel_AccentBar.BackColor = Color.SteelBlue;
            Control_SettingsCategoryCard_Panel_AccentBar.Dock = DockStyle.Top;
            Control_SettingsCategoryCard_Panel_AccentBar.Location = new Point(4, 4);
            Control_SettingsCategoryCard_Panel_AccentBar.Margin = new Padding(4, 4, 4, 4);
            Control_SettingsCategoryCard_Panel_AccentBar.Name = "Control_SettingsCategoryCard_Panel_AccentBar";
            Control_SettingsCategoryCard_Panel_AccentBar.Size = new Size(113, 6);
            Control_SettingsCategoryCard_Panel_AccentBar.TabIndex = 0;
            // 
            // Control_SettingsCategoryCard
            // 
            AutoScaleDimensions = new SizeF(144F, 144F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = Color.White;
            Controls.Add(Control_SettingsCategoryCard_Panel_Main);
            Cursor = Cursors.Hand;
            Margin = new Padding(8, 8, 8, 8);
            Name = "Control_SettingsCategoryCard";
            Size = new Size(121, 139);
            Control_SettingsCategoryCard_Panel_Main.ResumeLayout(false);
            Control_SettingsCategoryCard_Panel_Main.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            Control_SettingsCategoryCard_Panel_Content.ResumeLayout(false);
            Control_SettingsCategoryCard_Panel_Content.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
    }
}
