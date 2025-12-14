using System.Drawing;
using System.Windows.Forms;

namespace MTM_WIP_Application_Winforms.Controls.DeveloperTools
{
    partial class Control_LogStatistics
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        #region Fields

        private System.ComponentModel.IContainer components = null;

        #endregion

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        #region Methods

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Control_LogStatistics_TableLayout_Main = new TableLayoutPanel();
            this.Control_LogStatistics_Panel_Total = new Panel();
            this.Control_LogStatistics_Label_Total = new Label();
            this.Control_LogStatistics_Label_TotalLabel = new Label();
            this.Control_LogStatistics_Panel_Errors = new Panel();
            this.Control_LogStatistics_Label_Errors = new Label();
            this.Control_LogStatistics_Label_ErrorsLabel = new Label();
            this.Control_LogStatistics_Panel_Warnings = new Panel();
            this.Control_LogStatistics_Label_Warnings = new Label();
            this.Control_LogStatistics_Label_WarningsLabel = new Label();
            this.Control_LogStatistics_Panel_Info = new Panel();
            this.Control_LogStatistics_Label_Info = new Label();
            this.Control_LogStatistics_Label_InfoLabel = new Label();
            this.Control_LogStatistics_TableLayout_Main.SuspendLayout();
            this.Control_LogStatistics_Panel_Total.SuspendLayout();
            this.Control_LogStatistics_Panel_Errors.SuspendLayout();
            this.Control_LogStatistics_Panel_Warnings.SuspendLayout();
            this.Control_LogStatistics_Panel_Info.SuspendLayout();
            this.SuspendLayout();
            // 
            // Control_LogStatistics_TableLayout_Main
            // 
            this.Control_LogStatistics_TableLayout_Main.ColumnCount = 4;
            this.Control_LogStatistics_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.Control_LogStatistics_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.Control_LogStatistics_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.Control_LogStatistics_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.Control_LogStatistics_TableLayout_Main.Controls.Add(this.Control_LogStatistics_Panel_Total, 0, 0);
            this.Control_LogStatistics_TableLayout_Main.Controls.Add(this.Control_LogStatistics_Panel_Errors, 1, 0);
            this.Control_LogStatistics_TableLayout_Main.Controls.Add(this.Control_LogStatistics_Panel_Warnings, 2, 0);
            this.Control_LogStatistics_TableLayout_Main.Controls.Add(this.Control_LogStatistics_Panel_Info, 3, 0);
            this.Control_LogStatistics_TableLayout_Main.Dock = DockStyle.Fill;
            this.Control_LogStatistics_TableLayout_Main.Location = new Point(0, 0);
            this.Control_LogStatistics_TableLayout_Main.Name = "Control_LogStatistics_TableLayout_Main";
            this.Control_LogStatistics_TableLayout_Main.RowCount = 1;
            this.Control_LogStatistics_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.Control_LogStatistics_TableLayout_Main.Size = new Size(600, 80);
            this.Control_LogStatistics_TableLayout_Main.TabIndex = 0;
            // 
            // Control_LogStatistics_Panel_Total
            // 
            this.Control_LogStatistics_Panel_Total.Controls.Add(this.Control_LogStatistics_Label_Total);
            this.Control_LogStatistics_Panel_Total.Controls.Add(this.Control_LogStatistics_Label_TotalLabel);
            this.Control_LogStatistics_Panel_Total.Dock = DockStyle.Fill;
            this.Control_LogStatistics_Panel_Total.Location = new Point(3, 3);
            this.Control_LogStatistics_Panel_Total.Name = "Control_LogStatistics_Panel_Total";
            this.Control_LogStatistics_Panel_Total.Size = new Size(144, 74);
            this.Control_LogStatistics_Panel_Total.TabIndex = 0;
            // 
            // Control_LogStatistics_Label_Total
            // 
            this.Control_LogStatistics_Label_Total.Dock = DockStyle.Fill;
            this.Control_LogStatistics_Label_Total.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            this.Control_LogStatistics_Label_Total.Location = new Point(0, 20);
            this.Control_LogStatistics_Label_Total.Name = "Control_LogStatistics_Label_Total";
            this.Control_LogStatistics_Label_Total.Size = new Size(144, 54);
            this.Control_LogStatistics_Label_Total.TabIndex = 1;
            this.Control_LogStatistics_Label_Total.Text = "0";
            this.Control_LogStatistics_Label_Total.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_LogStatistics_Label_TotalLabel
            // 
            this.Control_LogStatistics_Label_TotalLabel.Dock = DockStyle.Top;
            this.Control_LogStatistics_Label_TotalLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            this.Control_LogStatistics_Label_TotalLabel.ForeColor = Color.Gray;
            this.Control_LogStatistics_Label_TotalLabel.Location = new Point(0, 0);
            this.Control_LogStatistics_Label_TotalLabel.Name = "Control_LogStatistics_Label_TotalLabel";
            this.Control_LogStatistics_Label_TotalLabel.Size = new Size(144, 20);
            this.Control_LogStatistics_Label_TotalLabel.TabIndex = 0;
            this.Control_LogStatistics_Label_TotalLabel.Text = "Total Logs";
            this.Control_LogStatistics_Label_TotalLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_LogStatistics_Panel_Errors
            // 
            this.Control_LogStatistics_Panel_Errors.Controls.Add(this.Control_LogStatistics_Label_Errors);
            this.Control_LogStatistics_Panel_Errors.Controls.Add(this.Control_LogStatistics_Label_ErrorsLabel);
            this.Control_LogStatistics_Panel_Errors.Dock = DockStyle.Fill;
            this.Control_LogStatistics_Panel_Errors.Location = new Point(153, 3);
            this.Control_LogStatistics_Panel_Errors.Name = "Control_LogStatistics_Panel_Errors";
            this.Control_LogStatistics_Panel_Errors.Size = new Size(144, 74);
            this.Control_LogStatistics_Panel_Errors.TabIndex = 1;
            // 
            // Control_LogStatistics_Label_Errors
            // 
            this.Control_LogStatistics_Label_Errors.Dock = DockStyle.Fill;
            this.Control_LogStatistics_Label_Errors.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            this.Control_LogStatistics_Label_Errors.ForeColor = Color.Red;
            this.Control_LogStatistics_Label_Errors.Location = new Point(0, 20);
            this.Control_LogStatistics_Label_Errors.Name = "Control_LogStatistics_Label_Errors";
            this.Control_LogStatistics_Label_Errors.Size = new Size(144, 54);
            this.Control_LogStatistics_Label_Errors.TabIndex = 1;
            this.Control_LogStatistics_Label_Errors.Text = "0";
            this.Control_LogStatistics_Label_Errors.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_LogStatistics_Label_ErrorsLabel
            // 
            this.Control_LogStatistics_Label_ErrorsLabel.Dock = DockStyle.Top;
            this.Control_LogStatistics_Label_ErrorsLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            this.Control_LogStatistics_Label_ErrorsLabel.ForeColor = Color.Gray;
            this.Control_LogStatistics_Label_ErrorsLabel.Location = new Point(0, 0);
            this.Control_LogStatistics_Label_ErrorsLabel.Name = "Control_LogStatistics_Label_ErrorsLabel";
            this.Control_LogStatistics_Label_ErrorsLabel.Size = new Size(144, 20);
            this.Control_LogStatistics_Label_ErrorsLabel.TabIndex = 0;
            this.Control_LogStatistics_Label_ErrorsLabel.Text = "Errors";
            this.Control_LogStatistics_Label_ErrorsLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_LogStatistics_Panel_Warnings
            // 
            this.Control_LogStatistics_Panel_Warnings.Controls.Add(this.Control_LogStatistics_Label_Warnings);
            this.Control_LogStatistics_Panel_Warnings.Controls.Add(this.Control_LogStatistics_Label_WarningsLabel);
            this.Control_LogStatistics_Panel_Warnings.Dock = DockStyle.Fill;
            this.Control_LogStatistics_Panel_Warnings.Location = new Point(303, 3);
            this.Control_LogStatistics_Panel_Warnings.Name = "Control_LogStatistics_Panel_Warnings";
            this.Control_LogStatistics_Panel_Warnings.Size = new Size(144, 74);
            this.Control_LogStatistics_Panel_Warnings.TabIndex = 2;
            // 
            // Control_LogStatistics_Label_Warnings
            // 
            this.Control_LogStatistics_Label_Warnings.Dock = DockStyle.Fill;
            this.Control_LogStatistics_Label_Warnings.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            this.Control_LogStatistics_Label_Warnings.ForeColor = Color.Orange;
            this.Control_LogStatistics_Label_Warnings.Location = new Point(0, 20);
            this.Control_LogStatistics_Label_Warnings.Name = "Control_LogStatistics_Label_Warnings";
            this.Control_LogStatistics_Label_Warnings.Size = new Size(144, 54);
            this.Control_LogStatistics_Label_Warnings.TabIndex = 1;
            this.Control_LogStatistics_Label_Warnings.Text = "0";
            this.Control_LogStatistics_Label_Warnings.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_LogStatistics_Label_WarningsLabel
            // 
            this.Control_LogStatistics_Label_WarningsLabel.Dock = DockStyle.Top;
            this.Control_LogStatistics_Label_WarningsLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            this.Control_LogStatistics_Label_WarningsLabel.ForeColor = Color.Gray;
            this.Control_LogStatistics_Label_WarningsLabel.Location = new Point(0, 0);
            this.Control_LogStatistics_Label_WarningsLabel.Name = "Control_LogStatistics_Label_WarningsLabel";
            this.Control_LogStatistics_Label_WarningsLabel.Size = new Size(144, 20);
            this.Control_LogStatistics_Label_WarningsLabel.TabIndex = 0;
            this.Control_LogStatistics_Label_WarningsLabel.Text = "Warnings";
            this.Control_LogStatistics_Label_WarningsLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_LogStatistics_Panel_Info
            // 
            this.Control_LogStatistics_Panel_Info.Controls.Add(this.Control_LogStatistics_Label_Info);
            this.Control_LogStatistics_Panel_Info.Controls.Add(this.Control_LogStatistics_Label_InfoLabel);
            this.Control_LogStatistics_Panel_Info.Dock = DockStyle.Fill;
            this.Control_LogStatistics_Panel_Info.Location = new Point(453, 3);
            this.Control_LogStatistics_Panel_Info.Name = "Control_LogStatistics_Panel_Info";
            this.Control_LogStatistics_Panel_Info.Size = new Size(144, 74);
            this.Control_LogStatistics_Panel_Info.TabIndex = 3;
            // 
            // Control_LogStatistics_Label_Info
            // 
            this.Control_LogStatistics_Label_Info.Dock = DockStyle.Fill;
            this.Control_LogStatistics_Label_Info.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            this.Control_LogStatistics_Label_Info.ForeColor = Color.DodgerBlue;
            this.Control_LogStatistics_Label_Info.Location = new Point(0, 20);
            this.Control_LogStatistics_Label_Info.Name = "Control_LogStatistics_Label_Info";
            this.Control_LogStatistics_Label_Info.Size = new Size(144, 54);
            this.Control_LogStatistics_Label_Info.TabIndex = 1;
            this.Control_LogStatistics_Label_Info.Text = "0";
            this.Control_LogStatistics_Label_Info.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_LogStatistics_Label_InfoLabel
            // 
            this.Control_LogStatistics_Label_InfoLabel.Dock = DockStyle.Top;
            this.Control_LogStatistics_Label_InfoLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            this.Control_LogStatistics_Label_InfoLabel.ForeColor = Color.Gray;
            this.Control_LogStatistics_Label_InfoLabel.Location = new Point(0, 0);
            this.Control_LogStatistics_Label_InfoLabel.Name = "Control_LogStatistics_Label_InfoLabel";
            this.Control_LogStatistics_Label_InfoLabel.Size = new Size(144, 20);
            this.Control_LogStatistics_Label_InfoLabel.TabIndex = 0;
            this.Control_LogStatistics_Label_InfoLabel.Text = "Info";
            this.Control_LogStatistics_Label_InfoLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_LogStatistics
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.Control_LogStatistics_TableLayout_Main);
            this.Name = "Control_LogStatistics";
            this.Size = new Size(600, 80);
            this.Control_LogStatistics_TableLayout_Main.ResumeLayout(false);
            this.Control_LogStatistics_Panel_Total.ResumeLayout(false);
            this.Control_LogStatistics_Panel_Errors.ResumeLayout(false);
            this.Control_LogStatistics_Panel_Warnings.ResumeLayout(false);
            this.Control_LogStatistics_Panel_Info.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel Control_LogStatistics_TableLayout_Main;
        private Panel Control_LogStatistics_Panel_Total;
        private Label Control_LogStatistics_Label_Total;
        private Label Control_LogStatistics_Label_TotalLabel;
        private Panel Control_LogStatistics_Panel_Errors;
        private Label Control_LogStatistics_Label_Errors;
        private Label Control_LogStatistics_Label_ErrorsLabel;
        private Panel Control_LogStatistics_Panel_Warnings;
        private Label Control_LogStatistics_Label_Warnings;
        private Label Control_LogStatistics_Label_WarningsLabel;
        private Panel Control_LogStatistics_Panel_Info;
        private Label Control_LogStatistics_Label_Info;
        private Label Control_LogStatistics_Label_InfoLabel;
    }
}



