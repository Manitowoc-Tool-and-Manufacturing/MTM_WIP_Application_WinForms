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
            Control_LogStatistics_TableLayout_Main = new TableLayoutPanel();
            Control_LogStatistics_Panel_Total = new Panel();
            Control_LogStatistics_Label_Total = new Label();
            Control_LogStatistics_Label_TotalLabel = new Label();
            Control_LogStatistics_Panel_Errors = new Panel();
            Control_LogStatistics_Label_Errors = new Label();
            Control_LogStatistics_Label_ErrorsLabel = new Label();
            Control_LogStatistics_Panel_Warnings = new Panel();
            Control_LogStatistics_Label_Warnings = new Label();
            Control_LogStatistics_Label_WarningsLabel = new Label();
            Control_LogStatistics_Panel_Info = new Panel();
            Control_LogStatistics_Label_Info = new Label();
            Control_LogStatistics_Label_InfoLabel = new Label();
            Control_LogStatistics_Panel_Sync = new Panel();
            Control_LogStatistics_TableLayoutPanel_Sync = new TableLayoutPanel();
            Control_LogStatistics_Label_SyncStatus = new Label();
            Control_LogStatistics_Button_Sync = new Button();
            Control_LogStatistics_Button_Purge = new Button();
            Control_LogStatistics_ProgressBar_Sync = new ProgressBar();
            Control_LogStatistics_TableLayout_Total = new TableLayoutPanel();
            Control_LogStatistics_TableLayout_Errors = new TableLayoutPanel();
            Control_LogStatistics_TableLayout_Warnings = new TableLayoutPanel();
            Control_LogStatistics_TableLayout_Info = new TableLayoutPanel();
            Control_LogStatistics_TableLayout_Main.SuspendLayout();
            Control_LogStatistics_Panel_Total.SuspendLayout();
            Control_LogStatistics_Panel_Errors.SuspendLayout();
            Control_LogStatistics_Panel_Warnings.SuspendLayout();
            Control_LogStatistics_Panel_Info.SuspendLayout();
            Control_LogStatistics_Panel_Sync.SuspendLayout();
            Control_LogStatistics_TableLayoutPanel_Sync.SuspendLayout();
            Control_LogStatistics_TableLayout_Total.SuspendLayout();
            Control_LogStatistics_TableLayout_Errors.SuspendLayout();
            Control_LogStatistics_TableLayout_Warnings.SuspendLayout();
            Control_LogStatistics_TableLayout_Info.SuspendLayout();
            SuspendLayout();
            // 
            // Control_LogStatistics_TableLayout_Main
            // 
            Control_LogStatistics_TableLayout_Main.AutoSize = true;
            Control_LogStatistics_TableLayout_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LogStatistics_TableLayout_Main.ColumnCount = 5;
            Control_LogStatistics_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            Control_LogStatistics_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            Control_LogStatistics_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            Control_LogStatistics_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            Control_LogStatistics_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            Control_LogStatistics_TableLayout_Main.Controls.Add(Control_LogStatistics_Panel_Total, 0, 0);
            Control_LogStatistics_TableLayout_Main.Controls.Add(Control_LogStatistics_Panel_Errors, 1, 0);
            Control_LogStatistics_TableLayout_Main.Controls.Add(Control_LogStatistics_Panel_Warnings, 2, 0);
            Control_LogStatistics_TableLayout_Main.Controls.Add(Control_LogStatistics_Panel_Info, 3, 0);
            Control_LogStatistics_TableLayout_Main.Controls.Add(Control_LogStatistics_Panel_Sync, 4, 0);
            Control_LogStatistics_TableLayout_Main.Dock = DockStyle.Fill;
            Control_LogStatistics_TableLayout_Main.Location = new Point(0, 0);
            Control_LogStatistics_TableLayout_Main.Name = "Control_LogStatistics_TableLayout_Main";
            Control_LogStatistics_TableLayout_Main.RowCount = 1;
            Control_LogStatistics_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_LogStatistics_TableLayout_Main.Size = new Size(600, 111);
            Control_LogStatistics_TableLayout_Main.TabIndex = 0;
            // 
            // Control_LogStatistics_Panel_Total
            // 
            Control_LogStatistics_Panel_Total.Controls.Add(Control_LogStatistics_TableLayout_Total);
            Control_LogStatistics_Panel_Total.Dock = DockStyle.Fill;
            Control_LogStatistics_Panel_Total.Location = new Point(3, 3);
            Control_LogStatistics_Panel_Total.Name = "Control_LogStatistics_Panel_Total";
            Control_LogStatistics_Panel_Total.Size = new Size(114, 105);
            Control_LogStatistics_Panel_Total.TabIndex = 0;
            // 
            // Control_LogStatistics_Label_Total
            // 
            Control_LogStatistics_Label_Total.Dock = DockStyle.Fill;
            Control_LogStatistics_Label_Total.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            Control_LogStatistics_Label_Total.Location = new Point(3, 29);
            Control_LogStatistics_Label_Total.Margin = new Padding(3);
            Control_LogStatistics_Label_Total.Name = "Control_LogStatistics_Label_Total";
            Control_LogStatistics_Label_Total.Size = new Size(108, 73);
            Control_LogStatistics_Label_Total.TabIndex = 1;
            Control_LogStatistics_Label_Total.Text = "0";
            Control_LogStatistics_Label_Total.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_LogStatistics_Label_TotalLabel
            // 
            Control_LogStatistics_Label_TotalLabel.Dock = DockStyle.Fill;
            Control_LogStatistics_Label_TotalLabel.Font = new Font("Segoe UI", 9F);
            Control_LogStatistics_Label_TotalLabel.ForeColor = Color.Gray;
            Control_LogStatistics_Label_TotalLabel.Location = new Point(3, 3);
            Control_LogStatistics_Label_TotalLabel.Margin = new Padding(3);
            Control_LogStatistics_Label_TotalLabel.Name = "Control_LogStatistics_Label_TotalLabel";
            Control_LogStatistics_Label_TotalLabel.Size = new Size(108, 20);
            Control_LogStatistics_Label_TotalLabel.TabIndex = 0;
            Control_LogStatistics_Label_TotalLabel.Text = "Total Logs";
            Control_LogStatistics_Label_TotalLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_LogStatistics_Panel_Errors
            // 
            Control_LogStatistics_Panel_Errors.Controls.Add(Control_LogStatistics_TableLayout_Errors);
            Control_LogStatistics_Panel_Errors.Dock = DockStyle.Fill;
            Control_LogStatistics_Panel_Errors.Location = new Point(123, 3);
            Control_LogStatistics_Panel_Errors.Name = "Control_LogStatistics_Panel_Errors";
            Control_LogStatistics_Panel_Errors.Size = new Size(114, 105);
            Control_LogStatistics_Panel_Errors.TabIndex = 1;
            // 
            // Control_LogStatistics_Label_Errors
            // 
            Control_LogStatistics_Label_Errors.Dock = DockStyle.Fill;
            Control_LogStatistics_Label_Errors.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            Control_LogStatistics_Label_Errors.ForeColor = Color.Red;
            Control_LogStatistics_Label_Errors.Location = new Point(3, 29);
            Control_LogStatistics_Label_Errors.Margin = new Padding(3);
            Control_LogStatistics_Label_Errors.Name = "Control_LogStatistics_Label_Errors";
            Control_LogStatistics_Label_Errors.Size = new Size(108, 73);
            Control_LogStatistics_Label_Errors.TabIndex = 1;
            Control_LogStatistics_Label_Errors.Text = "0";
            Control_LogStatistics_Label_Errors.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_LogStatistics_Label_ErrorsLabel
            // 
            Control_LogStatistics_Label_ErrorsLabel.Dock = DockStyle.Fill;
            Control_LogStatistics_Label_ErrorsLabel.Font = new Font("Segoe UI", 9F);
            Control_LogStatistics_Label_ErrorsLabel.ForeColor = Color.Gray;
            Control_LogStatistics_Label_ErrorsLabel.Location = new Point(3, 3);
            Control_LogStatistics_Label_ErrorsLabel.Margin = new Padding(3);
            Control_LogStatistics_Label_ErrorsLabel.Name = "Control_LogStatistics_Label_ErrorsLabel";
            Control_LogStatistics_Label_ErrorsLabel.Size = new Size(108, 20);
            Control_LogStatistics_Label_ErrorsLabel.TabIndex = 0;
            Control_LogStatistics_Label_ErrorsLabel.Text = "Errors";
            Control_LogStatistics_Label_ErrorsLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_LogStatistics_Panel_Warnings
            // 
            Control_LogStatistics_Panel_Warnings.Controls.Add(Control_LogStatistics_TableLayout_Warnings);
            Control_LogStatistics_Panel_Warnings.Dock = DockStyle.Fill;
            Control_LogStatistics_Panel_Warnings.Location = new Point(243, 3);
            Control_LogStatistics_Panel_Warnings.Name = "Control_LogStatistics_Panel_Warnings";
            Control_LogStatistics_Panel_Warnings.Size = new Size(114, 105);
            Control_LogStatistics_Panel_Warnings.TabIndex = 2;
            // 
            // Control_LogStatistics_Label_Warnings
            // 
            Control_LogStatistics_Label_Warnings.Dock = DockStyle.Fill;
            Control_LogStatistics_Label_Warnings.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            Control_LogStatistics_Label_Warnings.ForeColor = Color.Orange;
            Control_LogStatistics_Label_Warnings.Location = new Point(3, 29);
            Control_LogStatistics_Label_Warnings.Margin = new Padding(3);
            Control_LogStatistics_Label_Warnings.Name = "Control_LogStatistics_Label_Warnings";
            Control_LogStatistics_Label_Warnings.Size = new Size(108, 73);
            Control_LogStatistics_Label_Warnings.TabIndex = 1;
            Control_LogStatistics_Label_Warnings.Text = "0";
            Control_LogStatistics_Label_Warnings.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_LogStatistics_Label_WarningsLabel
            // 
            Control_LogStatistics_Label_WarningsLabel.Dock = DockStyle.Fill;
            Control_LogStatistics_Label_WarningsLabel.Font = new Font("Segoe UI", 9F);
            Control_LogStatistics_Label_WarningsLabel.ForeColor = Color.Gray;
            Control_LogStatistics_Label_WarningsLabel.Location = new Point(3, 3);
            Control_LogStatistics_Label_WarningsLabel.Margin = new Padding(3);
            Control_LogStatistics_Label_WarningsLabel.Name = "Control_LogStatistics_Label_WarningsLabel";
            Control_LogStatistics_Label_WarningsLabel.Size = new Size(108, 20);
            Control_LogStatistics_Label_WarningsLabel.TabIndex = 0;
            Control_LogStatistics_Label_WarningsLabel.Text = "Warnings";
            Control_LogStatistics_Label_WarningsLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_LogStatistics_Panel_Info
            // 
            Control_LogStatistics_Panel_Info.Controls.Add(Control_LogStatistics_TableLayout_Info);
            Control_LogStatistics_Panel_Info.Dock = DockStyle.Fill;
            Control_LogStatistics_Panel_Info.Location = new Point(363, 3);
            Control_LogStatistics_Panel_Info.Name = "Control_LogStatistics_Panel_Info";
            Control_LogStatistics_Panel_Info.Size = new Size(114, 105);
            Control_LogStatistics_Panel_Info.TabIndex = 3;
            // 
            // Control_LogStatistics_Label_Info
            // 
            Control_LogStatistics_Label_Info.Dock = DockStyle.Fill;
            Control_LogStatistics_Label_Info.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            Control_LogStatistics_Label_Info.ForeColor = Color.DodgerBlue;
            Control_LogStatistics_Label_Info.Location = new Point(3, 29);
            Control_LogStatistics_Label_Info.Margin = new Padding(3);
            Control_LogStatistics_Label_Info.Name = "Control_LogStatistics_Label_Info";
            Control_LogStatistics_Label_Info.Size = new Size(108, 73);
            Control_LogStatistics_Label_Info.TabIndex = 1;
            Control_LogStatistics_Label_Info.Text = "0";
            Control_LogStatistics_Label_Info.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_LogStatistics_Label_InfoLabel
            // 
            Control_LogStatistics_Label_InfoLabel.Dock = DockStyle.Fill;
            Control_LogStatistics_Label_InfoLabel.Font = new Font("Segoe UI", 9F);
            Control_LogStatistics_Label_InfoLabel.ForeColor = Color.Gray;
            Control_LogStatistics_Label_InfoLabel.Location = new Point(3, 3);
            Control_LogStatistics_Label_InfoLabel.Margin = new Padding(3);
            Control_LogStatistics_Label_InfoLabel.Name = "Control_LogStatistics_Label_InfoLabel";
            Control_LogStatistics_Label_InfoLabel.Size = new Size(108, 20);
            Control_LogStatistics_Label_InfoLabel.TabIndex = 0;
            Control_LogStatistics_Label_InfoLabel.Text = "Info";
            Control_LogStatistics_Label_InfoLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_LogStatistics_Panel_Sync
            // 
            Control_LogStatistics_Panel_Sync.Controls.Add(Control_LogStatistics_TableLayoutPanel_Sync);
            Control_LogStatistics_Panel_Sync.Dock = DockStyle.Fill;
            Control_LogStatistics_Panel_Sync.Location = new Point(483, 3);
            Control_LogStatistics_Panel_Sync.Name = "Control_LogStatistics_Panel_Sync";
            Control_LogStatistics_Panel_Sync.Size = new Size(114, 105);
            Control_LogStatistics_Panel_Sync.TabIndex = 4;
            // 
            // Control_LogStatistics_TableLayoutPanel_Sync
            // 
            Control_LogStatistics_TableLayoutPanel_Sync.AutoSize = true;
            Control_LogStatistics_TableLayoutPanel_Sync.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LogStatistics_TableLayoutPanel_Sync.ColumnCount = 1;
            Control_LogStatistics_TableLayoutPanel_Sync.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_LogStatistics_TableLayoutPanel_Sync.Controls.Add(Control_LogStatistics_Label_SyncStatus, 0, 3);
            Control_LogStatistics_TableLayoutPanel_Sync.Controls.Add(Control_LogStatistics_Button_Sync, 0, 0);
            Control_LogStatistics_TableLayoutPanel_Sync.Controls.Add(Control_LogStatistics_Button_Purge, 0, 1);
            Control_LogStatistics_TableLayoutPanel_Sync.Controls.Add(Control_LogStatistics_ProgressBar_Sync, 0, 2);
            Control_LogStatistics_TableLayoutPanel_Sync.Dock = DockStyle.Fill;
            Control_LogStatistics_TableLayoutPanel_Sync.Location = new Point(0, 0);
            Control_LogStatistics_TableLayoutPanel_Sync.Name = "Control_LogStatistics_TableLayoutPanel_Sync";
            Control_LogStatistics_TableLayoutPanel_Sync.RowCount = 4;
            Control_LogStatistics_TableLayoutPanel_Sync.RowStyles.Add(new RowStyle());
            Control_LogStatistics_TableLayoutPanel_Sync.RowStyles.Add(new RowStyle());
            Control_LogStatistics_TableLayoutPanel_Sync.RowStyles.Add(new RowStyle());
            Control_LogStatistics_TableLayoutPanel_Sync.RowStyles.Add(new RowStyle());
            Control_LogStatistics_TableLayoutPanel_Sync.Size = new Size(114, 105);
            Control_LogStatistics_TableLayoutPanel_Sync.TabIndex = 4;
            // 
            // Control_LogStatistics_Label_SyncStatus
            // 
            Control_LogStatistics_Label_SyncStatus.AutoSize = true;
            Control_LogStatistics_Label_SyncStatus.Dock = DockStyle.Fill;
            Control_LogStatistics_Label_SyncStatus.Font = new Font("Segoe UI", 8F);
            Control_LogStatistics_Label_SyncStatus.ForeColor = Color.Gray;
            Control_LogStatistics_Label_SyncStatus.Location = new Point(3, 77);
            Control_LogStatistics_Label_SyncStatus.Margin = new Padding(3);
            Control_LogStatistics_Label_SyncStatus.Name = "Control_LogStatistics_Label_SyncStatus";
            Control_LogStatistics_Label_SyncStatus.Size = new Size(108, 25);
            Control_LogStatistics_Label_SyncStatus.TabIndex = 3;
            Control_LogStatistics_Label_SyncStatus.Text = "Ready";
            Control_LogStatistics_Label_SyncStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_LogStatistics_Button_Sync
            // 
            Control_LogStatistics_Button_Sync.Dock = DockStyle.Fill;
            Control_LogStatistics_Button_Sync.Location = new Point(3, 3);
            Control_LogStatistics_Button_Sync.Name = "Control_LogStatistics_Button_Sync";
            Control_LogStatistics_Button_Sync.Size = new Size(108, 23);
            Control_LogStatistics_Button_Sync.TabIndex = 0;
            Control_LogStatistics_Button_Sync.Text = "Sync Logs";
            Control_LogStatistics_Button_Sync.UseVisualStyleBackColor = true;
            Control_LogStatistics_Button_Sync.Click += Control_LogStatistics_Button_Sync_Click;
            // 
            // Control_LogStatistics_Button_Purge
            // 
            Control_LogStatistics_Button_Purge.Dock = DockStyle.Fill;
            Control_LogStatistics_Button_Purge.Location = new Point(3, 32);
            Control_LogStatistics_Button_Purge.Name = "Control_LogStatistics_Button_Purge";
            Control_LogStatistics_Button_Purge.Size = new Size(108, 23);
            Control_LogStatistics_Button_Purge.TabIndex = 1;
            Control_LogStatistics_Button_Purge.Text = "Purge Logs";
            Control_LogStatistics_Button_Purge.UseVisualStyleBackColor = true;
            Control_LogStatistics_Button_Purge.Click += Control_LogStatistics_Button_Purge_Click;
            // 
            // Control_LogStatistics_ProgressBar_Sync
            // 
            Control_LogStatistics_ProgressBar_Sync.Dock = DockStyle.Fill;
            Control_LogStatistics_ProgressBar_Sync.Location = new Point(3, 61);
            Control_LogStatistics_ProgressBar_Sync.Name = "Control_LogStatistics_ProgressBar_Sync";
            Control_LogStatistics_ProgressBar_Sync.Size = new Size(108, 10);
            Control_LogStatistics_ProgressBar_Sync.TabIndex = 2;
            // 
            // Control_LogStatistics_TableLayout_Total
            // 
            Control_LogStatistics_TableLayout_Total.AutoSize = true;
            Control_LogStatistics_TableLayout_Total.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LogStatistics_TableLayout_Total.ColumnCount = 1;
            Control_LogStatistics_TableLayout_Total.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_LogStatistics_TableLayout_Total.Controls.Add(Control_LogStatistics_Label_TotalLabel, 0, 0);
            Control_LogStatistics_TableLayout_Total.Controls.Add(Control_LogStatistics_Label_Total, 0, 1);
            Control_LogStatistics_TableLayout_Total.Dock = DockStyle.Fill;
            Control_LogStatistics_TableLayout_Total.Location = new Point(0, 0);
            Control_LogStatistics_TableLayout_Total.Name = "Control_LogStatistics_TableLayout_Total";
            Control_LogStatistics_TableLayout_Total.RowCount = 2;
            Control_LogStatistics_TableLayout_Total.RowStyles.Add(new RowStyle());
            Control_LogStatistics_TableLayout_Total.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_LogStatistics_TableLayout_Total.Size = new Size(114, 105);
            Control_LogStatistics_TableLayout_Total.TabIndex = 1;
            // 
            // Control_LogStatistics_TableLayout_Errors
            // 
            Control_LogStatistics_TableLayout_Errors.AutoSize = true;
            Control_LogStatistics_TableLayout_Errors.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LogStatistics_TableLayout_Errors.ColumnCount = 1;
            Control_LogStatistics_TableLayout_Errors.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_LogStatistics_TableLayout_Errors.Controls.Add(Control_LogStatistics_Label_ErrorsLabel, 0, 0);
            Control_LogStatistics_TableLayout_Errors.Controls.Add(Control_LogStatistics_Label_Errors, 0, 1);
            Control_LogStatistics_TableLayout_Errors.Dock = DockStyle.Fill;
            Control_LogStatistics_TableLayout_Errors.Location = new Point(0, 0);
            Control_LogStatistics_TableLayout_Errors.Name = "Control_LogStatistics_TableLayout_Errors";
            Control_LogStatistics_TableLayout_Errors.RowCount = 2;
            Control_LogStatistics_TableLayout_Errors.RowStyles.Add(new RowStyle());
            Control_LogStatistics_TableLayout_Errors.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_LogStatistics_TableLayout_Errors.Size = new Size(114, 105);
            Control_LogStatistics_TableLayout_Errors.TabIndex = 2;
            // 
            // Control_LogStatistics_TableLayout_Warnings
            // 
            Control_LogStatistics_TableLayout_Warnings.AutoSize = true;
            Control_LogStatistics_TableLayout_Warnings.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LogStatistics_TableLayout_Warnings.ColumnCount = 1;
            Control_LogStatistics_TableLayout_Warnings.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_LogStatistics_TableLayout_Warnings.Controls.Add(Control_LogStatistics_Label_WarningsLabel, 0, 0);
            Control_LogStatistics_TableLayout_Warnings.Controls.Add(Control_LogStatistics_Label_Warnings, 0, 1);
            Control_LogStatistics_TableLayout_Warnings.Dock = DockStyle.Fill;
            Control_LogStatistics_TableLayout_Warnings.Location = new Point(0, 0);
            Control_LogStatistics_TableLayout_Warnings.Name = "Control_LogStatistics_TableLayout_Warnings";
            Control_LogStatistics_TableLayout_Warnings.RowCount = 2;
            Control_LogStatistics_TableLayout_Warnings.RowStyles.Add(new RowStyle());
            Control_LogStatistics_TableLayout_Warnings.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_LogStatistics_TableLayout_Warnings.Size = new Size(114, 105);
            Control_LogStatistics_TableLayout_Warnings.TabIndex = 3;
            // 
            // Control_LogStatistics_TableLayout_Info
            // 
            Control_LogStatistics_TableLayout_Info.AutoSize = true;
            Control_LogStatistics_TableLayout_Info.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LogStatistics_TableLayout_Info.ColumnCount = 1;
            Control_LogStatistics_TableLayout_Info.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_LogStatistics_TableLayout_Info.Controls.Add(Control_LogStatistics_Label_InfoLabel, 0, 0);
            Control_LogStatistics_TableLayout_Info.Controls.Add(Control_LogStatistics_Label_Info, 0, 1);
            Control_LogStatistics_TableLayout_Info.Dock = DockStyle.Fill;
            Control_LogStatistics_TableLayout_Info.Location = new Point(0, 0);
            Control_LogStatistics_TableLayout_Info.Name = "Control_LogStatistics_TableLayout_Info";
            Control_LogStatistics_TableLayout_Info.RowCount = 2;
            Control_LogStatistics_TableLayout_Info.RowStyles.Add(new RowStyle());
            Control_LogStatistics_TableLayout_Info.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_LogStatistics_TableLayout_Info.Size = new Size(114, 105);
            Control_LogStatistics_TableLayout_Info.TabIndex = 4;
            // 
            // Control_LogStatistics
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(Control_LogStatistics_TableLayout_Main);
            Name = "Control_LogStatistics";
            Size = new Size(600, 111);
            Control_LogStatistics_TableLayout_Main.ResumeLayout(false);
            Control_LogStatistics_Panel_Total.ResumeLayout(false);
            Control_LogStatistics_Panel_Total.PerformLayout();
            Control_LogStatistics_Panel_Errors.ResumeLayout(false);
            Control_LogStatistics_Panel_Errors.PerformLayout();
            Control_LogStatistics_Panel_Warnings.ResumeLayout(false);
            Control_LogStatistics_Panel_Warnings.PerformLayout();
            Control_LogStatistics_Panel_Info.ResumeLayout(false);
            Control_LogStatistics_Panel_Info.PerformLayout();
            Control_LogStatistics_Panel_Sync.ResumeLayout(false);
            Control_LogStatistics_Panel_Sync.PerformLayout();
            Control_LogStatistics_TableLayoutPanel_Sync.ResumeLayout(false);
            Control_LogStatistics_TableLayoutPanel_Sync.PerformLayout();
            Control_LogStatistics_TableLayout_Total.ResumeLayout(false);
            Control_LogStatistics_TableLayout_Errors.ResumeLayout(false);
            Control_LogStatistics_TableLayout_Warnings.ResumeLayout(false);
            Control_LogStatistics_TableLayout_Info.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

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
        private Panel Control_LogStatistics_Panel_Sync;
        private Button Control_LogStatistics_Button_Sync;
        private Button Control_LogStatistics_Button_Purge;
        private ProgressBar Control_LogStatistics_ProgressBar_Sync;
        private Label Control_LogStatistics_Label_SyncStatus;
        private TableLayoutPanel Control_LogStatistics_TableLayoutPanel_Sync;
        private TableLayoutPanel Control_LogStatistics_TableLayout_Total;
        private TableLayoutPanel Control_LogStatistics_TableLayout_Errors;
        private TableLayoutPanel Control_LogStatistics_TableLayout_Warnings;
        private TableLayoutPanel Control_LogStatistics_TableLayout_Info;
    }
}



