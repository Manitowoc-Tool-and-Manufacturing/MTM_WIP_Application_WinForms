using System.Drawing;
using System.Windows.Forms;
using MTM_Inventory_Application.Controls.ErrorReports;

namespace MTM_Inventory_Application.Forms.ErrorReports
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
            SuspendLayout();
            // 
            // controlErrorReportsGrid
            // 
            controlErrorReportsGrid.Dock = DockStyle.Fill;
            controlErrorReportsGrid.Location = new Point(0, 0);
            controlErrorReportsGrid.Name = "controlErrorReportsGrid";
            controlErrorReportsGrid.Size = new Size(1000, 600);
            controlErrorReportsGrid.TabIndex = 0;
            // 
            // Form_ViewErrorReports
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1000, 600);
            Controls.Add(controlErrorReportsGrid);
            MinimumSize = new Size(800, 500);
            Name = "Form_ViewErrorReports";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "View Error Reports";
            ResumeLayout(false);
        }

        #endregion

        private Control_ErrorReportsGrid controlErrorReportsGrid;
    }
}
