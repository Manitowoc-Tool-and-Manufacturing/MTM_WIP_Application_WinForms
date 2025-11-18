using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    partial class Control_SettingsHome
    {
        #region Fields

        private IContainer components = null!;
        private TableLayoutPanel Control_SettingsHome_TableLayout_Main = null!;
        private Label Control_SettingsHome_Label_Header = null!;
        private FlowLayoutPanel Control_SettingsHome_FlowPanel_Tiles = null!;

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
            Control_SettingsHome_TableLayout_Main = new TableLayoutPanel();
            Control_SettingsHome_Label_Header = new Label();
            Control_SettingsHome_FlowPanel_Tiles = new FlowLayoutPanel();
            Control_SettingsHome_TableLayout_Main.SuspendLayout();
            SuspendLayout();
            // 
            // Control_SettingsHome_TableLayout_Main
            // 
            Control_SettingsHome_TableLayout_Main.ColumnCount = 1;
            Control_SettingsHome_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_SettingsHome_TableLayout_Main.Controls.Add(Control_SettingsHome_Label_Header, 0, 0);
            Control_SettingsHome_TableLayout_Main.Controls.Add(Control_SettingsHome_FlowPanel_Tiles, 0, 1);
            Control_SettingsHome_TableLayout_Main.Dock = DockStyle.Fill;
            Control_SettingsHome_TableLayout_Main.Location = new Point(0, 0);
            Control_SettingsHome_TableLayout_Main.Name = "Control_SettingsHome_TableLayout_Main";
            Control_SettingsHome_TableLayout_Main.Padding = new Padding(20);
            Control_SettingsHome_TableLayout_Main.RowCount = 2;
            Control_SettingsHome_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            Control_SettingsHome_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_SettingsHome_TableLayout_Main.Size = new Size(800, 600);
            Control_SettingsHome_TableLayout_Main.TabIndex = 0;
            // 
            // Control_SettingsHome_Label_Header
            // 
            Control_SettingsHome_Label_Header.Dock = DockStyle.Fill;
            Control_SettingsHome_Label_Header.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            Control_SettingsHome_Label_Header.ForeColor = Color.FromArgb(45, 45, 45);
            Control_SettingsHome_Label_Header.Location = new Point(23, 20);
            Control_SettingsHome_Label_Header.Name = "Control_SettingsHome_Label_Header";
            Control_SettingsHome_Label_Header.Size = new Size(754, 80);
            Control_SettingsHome_Label_Header.TabIndex = 0;
            Control_SettingsHome_Label_Header.Text = "Settings";
            Control_SettingsHome_Label_Header.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_SettingsHome_FlowPanel_Tiles
            // 
            Control_SettingsHome_FlowPanel_Tiles.AutoScroll = true;
            Control_SettingsHome_FlowPanel_Tiles.Dock = DockStyle.Fill;
            Control_SettingsHome_FlowPanel_Tiles.FlowDirection = FlowDirection.LeftToRight;
            Control_SettingsHome_FlowPanel_Tiles.Location = new Point(23, 103);
            Control_SettingsHome_FlowPanel_Tiles.Name = "Control_SettingsHome_FlowPanel_Tiles";
            Control_SettingsHome_FlowPanel_Tiles.Size = new Size(754, 477);
            Control_SettingsHome_FlowPanel_Tiles.TabIndex = 1;
            Control_SettingsHome_FlowPanel_Tiles.WrapContents = true;
            // 
            // Control_SettingsHome
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = SystemColors.Control;
            Controls.Add(Control_SettingsHome_TableLayout_Main);
            Name = "Control_SettingsHome";
            Size = new Size(800, 600);
            Control_SettingsHome_TableLayout_Main.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
    }
}
