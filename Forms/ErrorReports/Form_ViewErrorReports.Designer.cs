using System.Drawing;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Controls.ErrorReports;

namespace MTM_WIP_Application_Winforms.Forms.ErrorReports
{
    partial class Form_ViewErrorReports : Form
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
            controlErrorReportsGrid = new Control_ErrorReportsGrid();
            panelExportButtons = new Panel();
            btnExportCsv = new Button();
            btnExportExcel = new Button();
            btnExportSelected = new Button();
            SuspendLayout();
            // 
            // controlErrorReportsGrid
            // 
            controlErrorReportsGrid.Dock = DockStyle.Fill;
            controlErrorReportsGrid.Location = new Point(0, 0);
            controlErrorReportsGrid.Name = "controlErrorReportsGrid";
            controlErrorReportsGrid.Size = new Size(1000, 560);
            controlErrorReportsGrid.TabIndex = 0;
            // 
            // panelExportButtons
            // 
            panelExportButtons.Dock = DockStyle.Bottom;
            panelExportButtons.Location = new Point(0, 560);
            panelExportButtons.Name = "panelExportButtons";
            panelExportButtons.Size = new Size(1000, 40);
            panelExportButtons.TabIndex = 1;
            panelExportButtons.Controls.Add(btnExportCsv);
            panelExportButtons.Controls.Add(btnExportExcel);
            panelExportButtons.Controls.Add(btnExportSelected);
            // 
            // btnExportCsv
            // 
            btnExportCsv.Location = new Point(10, 8);
            btnExportCsv.Name = "btnExportCsv";
            btnExportCsv.Size = new Size(120, 25);
            btnExportCsv.TabIndex = 0;
            btnExportCsv.Text = "Export to CSV";
            btnExportCsv.UseVisualStyleBackColor = true;
            // 
            // btnExportExcel
            // 
            btnExportExcel.Location = new Point(140, 8);
            btnExportExcel.Name = "btnExportExcel";
            btnExportExcel.Size = new Size(120, 25);
            btnExportExcel.TabIndex = 1;
            btnExportExcel.Text = "Export to Excel";
            btnExportExcel.UseVisualStyleBackColor = true;
            // 
            // btnExportSelected
            // 
            btnExportSelected.Location = new Point(270, 8);
            btnExportSelected.Name = "btnExportSelected";
            btnExportSelected.Size = new Size(120, 25);
            btnExportSelected.TabIndex = 2;
            btnExportSelected.Text = "Export Selected";
            btnExportSelected.UseVisualStyleBackColor = true;
            btnExportSelected.Enabled = false;
            // 
            // Form_ViewErrorReports
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1000, 600);
            Controls.Add(controlErrorReportsGrid);
            Controls.Add(panelExportButtons);
            MinimumSize = new Size(800, 500);
            Name = "Form_ViewErrorReports";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "View Error Reports";
            ResumeLayout(false);
        }

        #endregion

        private Control_ErrorReportsGrid controlErrorReportsGrid;
        private Panel panelExportButtons;
        private Button btnExportCsv;
        private Button btnExportExcel;
        private Button btnExportSelected;
    }
}
