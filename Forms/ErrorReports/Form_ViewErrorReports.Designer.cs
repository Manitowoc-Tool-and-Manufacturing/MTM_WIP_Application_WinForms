using System.Drawing;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Controls.ErrorReports;
using MTM_WIP_Application_Winforms.Forms.Shared;

namespace MTM_WIP_Application_Winforms.Forms.ErrorReports
{
    partial class Form_ViewErrorReports : ThemedForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_ViewErrorReports));
            controlErrorReportsGrid = new Control_ErrorReportsGrid();
            panelExportButtons = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnChangeFolder = new Button();
            Form_ViewErrorReports_Button_Help = new Button();
            btnExportSelected = new Button();
            btnExportExcel = new Button();
            btnExportCsv = new Button();
            panelExportButtons.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // controlErrorReportsGrid
            // 
            controlErrorReportsGrid.Dock = DockStyle.Fill;
            controlErrorReportsGrid.Location = new Point(0, 0);
            controlErrorReportsGrid.Name = "controlErrorReportsGrid";
            controlErrorReportsGrid.Size = new Size(1000, 562);
            controlErrorReportsGrid.TabIndex = 0;
            // 
            // panelExportButtons
            // 
            panelExportButtons.AutoSize = true;
            panelExportButtons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panelExportButtons.Controls.Add(tableLayoutPanel1);
            panelExportButtons.Dock = DockStyle.Bottom;
            panelExportButtons.Location = new Point(0, 562);
            panelExportButtons.Name = "panelExportButtons";
            panelExportButtons.Size = new Size(1000, 38);
            panelExportButtons.TabIndex = 1;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 6;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(btnChangeFolder, 3, 0);
            tableLayoutPanel1.Controls.Add(Form_ViewErrorReports_Button_Help, 5, 0);
            tableLayoutPanel1.Controls.Add(btnExportSelected, 2, 0);
            tableLayoutPanel1.Controls.Add(btnExportExcel, 1, 0);
            tableLayoutPanel1.Controls.Add(btnExportCsv, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1000, 38);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // btnChangeFolder
            // 
            btnChangeFolder.AutoSize = true;
            btnChangeFolder.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnChangeFolder.Dock = DockStyle.Top;
            btnChangeFolder.Location = new Point(299, 3);
            btnChangeFolder.MaximumSize = new Size(0, 32);
            btnChangeFolder.MinimumSize = new Size(0, 32);
            btnChangeFolder.Name = "btnChangeFolder";
            btnChangeFolder.Size = new Size(94, 32);
            btnChangeFolder.TabIndex = 3;
            btnChangeFolder.Text = "Change Folder";
            btnChangeFolder.UseVisualStyleBackColor = true;
            // 
            // Form_ViewErrorReports_Button_Help
            // 
            Form_ViewErrorReports_Button_Help.AutoSize = true;
            Form_ViewErrorReports_Button_Help.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Form_ViewErrorReports_Button_Help.Dock = DockStyle.Fill;
            Form_ViewErrorReports_Button_Help.Location = new Point(965, 3);
            Form_ViewErrorReports_Button_Help.MaximumSize = new Size(32, 32);
            Form_ViewErrorReports_Button_Help.MinimumSize = new Size(32, 32);
            Form_ViewErrorReports_Button_Help.Name = "Form_ViewErrorReports_Button_Help";
            Form_ViewErrorReports_Button_Help.Size = new Size(32, 32);
            Form_ViewErrorReports_Button_Help.TabIndex = 4;
            Form_ViewErrorReports_Button_Help.Text = "?";
            Form_ViewErrorReports_Button_Help.UseVisualStyleBackColor = true;
            // 
            // btnExportSelected
            // 
            btnExportSelected.AutoSize = true;
            btnExportSelected.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnExportSelected.Dock = DockStyle.Top;
            btnExportSelected.Enabled = false;
            btnExportSelected.Location = new Point(196, 3);
            btnExportSelected.MaximumSize = new Size(0, 32);
            btnExportSelected.MinimumSize = new Size(0, 32);
            btnExportSelected.Name = "btnExportSelected";
            btnExportSelected.Size = new Size(97, 32);
            btnExportSelected.TabIndex = 2;
            btnExportSelected.Text = "Export Selected";
            btnExportSelected.UseVisualStyleBackColor = true;
            // 
            // btnExportExcel
            // 
            btnExportExcel.AutoSize = true;
            btnExportExcel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnExportExcel.Dock = DockStyle.Top;
            btnExportExcel.Location = new Point(97, 3);
            btnExportExcel.MaximumSize = new Size(0, 32);
            btnExportExcel.MinimumSize = new Size(0, 32);
            btnExportExcel.Name = "btnExportExcel";
            btnExportExcel.Size = new Size(93, 32);
            btnExportExcel.TabIndex = 1;
            btnExportExcel.Text = "Export to Excel";
            btnExportExcel.UseVisualStyleBackColor = true;
            // 
            // btnExportCsv
            // 
            btnExportCsv.AutoSize = true;
            btnExportCsv.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnExportCsv.Dock = DockStyle.Top;
            btnExportCsv.Location = new Point(3, 3);
            btnExportCsv.MaximumSize = new Size(0, 32);
            btnExportCsv.MinimumSize = new Size(0, 32);
            btnExportCsv.Name = "btnExportCsv";
            btnExportCsv.Size = new Size(88, 32);
            btnExportCsv.TabIndex = 0;
            btnExportCsv.Text = "Export to CSV";
            btnExportCsv.UseVisualStyleBackColor = true;
            // 
            // Form_ViewErrorReports
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1000, 600);
            Controls.Add(controlErrorReportsGrid);
            Controls.Add(panelExportButtons);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(800, 500);
            Name = "Form_ViewErrorReports";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "View Error Reports";
            panelExportButtons.ResumeLayout(false);
            panelExportButtons.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Control_ErrorReportsGrid controlErrorReportsGrid;
        private Panel panelExportButtons;
        private Button btnExportCsv;
        private Button btnExportExcel;
        private Button btnExportSelected;
        private Button btnChangeFolder;
        private TableLayoutPanel tableLayoutPanel1;
        private Button Form_ViewErrorReports_Button_Help;
    }
}
