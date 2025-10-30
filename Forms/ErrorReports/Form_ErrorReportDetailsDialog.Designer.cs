using System.Drawing;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Controls.ErrorReports;

namespace MTM_WIP_Application_Winforms.Forms.ErrorReports
{
    partial class Form_ErrorReportDetailsDialog : Form
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
            tableLayoutPanelMain = new TableLayoutPanel();
            controlErrorReportDetails = new Control_ErrorReportDetails();
            tableLayoutPanelMain.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.ColumnCount = 1;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Controls.Add(controlErrorReportDetails, 0, 0);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.Location = new Point(0, 0);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.Padding = new Padding(10);
            tableLayoutPanelMain.RowCount = 1;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Size = new Size(750, 550);
            tableLayoutPanelMain.TabIndex = 0;
            // 
            // controlErrorReportDetails
            // 
            controlErrorReportDetails.Dock = DockStyle.Fill;
            controlErrorReportDetails.Location = new Point(10, 10);
            controlErrorReportDetails.Margin = new Padding(0);
            controlErrorReportDetails.Name = "controlErrorReportDetails";
            controlErrorReportDetails.Size = new Size(730, 530);
            controlErrorReportDetails.TabIndex = 0;
            // 
            // Form_ErrorReportDetailsDialog
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(750, 550);
            Controls.Add(tableLayoutPanelMain);
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;
            MinimizeBox = false;
            MinimumSize = new Size(670, 470);
            Name = "Form_ErrorReportDetailsDialog";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Error Report Details";
            tableLayoutPanelMain.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanelMain;
        private Control_ErrorReportDetails controlErrorReportDetails;
    }
}
