using System.Drawing;
using System.Windows.Forms;
using MTM_Inventory_Application.Controls.ErrorReports;

namespace MTM_Inventory_Application.Forms.ErrorReports
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
            controlErrorReportDetails = new Control_ErrorReportDetails();
            SuspendLayout();
            // 
            // controlErrorReportDetails
            // 
            controlErrorReportDetails.Dock = DockStyle.Fill;
            controlErrorReportDetails.Location = new Point(0, 0);
            controlErrorReportDetails.Name = "controlErrorReportDetails";
            controlErrorReportDetails.Size = new Size(750, 550);
            controlErrorReportDetails.TabIndex = 0;
            // 
            // Form_ErrorReportDetailsDialog
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(750, 550);
            Controls.Add(controlErrorReportDetails);
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;
            MinimizeBox = false;
            MinimumSize = new Size(650, 450);
            Name = "Form_ErrorReportDetailsDialog";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Error Report Details";
            ResumeLayout(false);
        }

        #endregion

        private Control_ErrorReportDetails controlErrorReportDetails;
    }
}
