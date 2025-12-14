using System.Drawing;
using System.Windows.Forms;

namespace MTM_WIP_Application_Winforms.Forms.DeveloperTools
{
    partial class DeveloperToolsForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DeveloperToolsForm_ToolStrip_Main = new ToolStrip();
            this.DeveloperToolsForm_Button_Refresh = new ToolStripButton();
            this.DeveloperToolsForm_TabControl_Main = new TabControl();
            this.DeveloperToolsForm_TabPage_Dashboard = new TabPage();
            this.DeveloperToolsForm_TableLayout_Dashboard = new TableLayoutPanel();
            this.DeveloperToolsForm_Control_SystemHealth = new MTM_WIP_Application_Winforms.Controls.DeveloperTools.Control_SystemHealth();
            this.DeveloperToolsForm_Control_LogStatistics = new MTM_WIP_Application_Winforms.Controls.DeveloperTools.Control_LogStatistics();
            this.DeveloperToolsForm_Control_RecentErrors = new MTM_WIP_Application_Winforms.Controls.DeveloperTools.Control_RecentErrors();
            this.DeveloperToolsForm_TabPage_Logs = new TabPage();
            this.DeveloperToolsForm_Control_LogViewer = new MTM_WIP_Application_Winforms.Controls.DeveloperTools.Control_LogViewer();
            this.DeveloperToolsForm_TabPage_Feedback = new TabPage();
            this.DeveloperToolsForm_Control_FeedbackManager = new MTM_WIP_Application_Winforms.Controls.DeveloperTools.Control_FeedbackManager();
            this.DeveloperToolsForm_TabPage_SystemInfo = new TabPage();
            this.DeveloperToolsForm_Control_SystemInfo = new MTM_WIP_Application_Winforms.Controls.DeveloperTools.Control_SystemInfo();
            this.DeveloperToolsForm_ToolStrip_Main.SuspendLayout();
            this.DeveloperToolsForm_TabControl_Main.SuspendLayout();
            this.DeveloperToolsForm_TabPage_Dashboard.SuspendLayout();
            this.DeveloperToolsForm_TableLayout_Dashboard.SuspendLayout();
            this.DeveloperToolsForm_TabPage_Logs.SuspendLayout();
            this.SuspendLayout();
            // 
            // DeveloperToolsForm_ToolStrip_Main
            // 
            this.DeveloperToolsForm_ToolStrip_Main.Items.AddRange(new ToolStripItem[] {
            this.DeveloperToolsForm_Button_Refresh});
            this.DeveloperToolsForm_ToolStrip_Main.Location = new Point(0, 0);
            this.DeveloperToolsForm_ToolStrip_Main.Name = "DeveloperToolsForm_ToolStrip_Main";
            this.DeveloperToolsForm_ToolStrip_Main.Size = new Size(800, 25);
            this.DeveloperToolsForm_ToolStrip_Main.TabIndex = 0;
            this.DeveloperToolsForm_ToolStrip_Main.Text = "toolStrip1";
            // 
            // DeveloperToolsForm_Button_Refresh
            // 
            this.DeveloperToolsForm_Button_Refresh.ImageTransparentColor = Color.Magenta;
            this.DeveloperToolsForm_Button_Refresh.Name = "DeveloperToolsForm_Button_Refresh";
            this.DeveloperToolsForm_Button_Refresh.Size = new Size(50, 22);
            this.DeveloperToolsForm_Button_Refresh.Text = "Refresh";
            this.DeveloperToolsForm_Button_Refresh.Click += new System.EventHandler(this.DeveloperToolsForm_Button_Refresh_Click);
            // 
            // DeveloperToolsForm_TabControl_Main
            // 
            this.DeveloperToolsForm_TabControl_Main.Controls.Add(this.DeveloperToolsForm_TabPage_Dashboard);
            this.DeveloperToolsForm_TabControl_Main.Controls.Add(this.DeveloperToolsForm_TabPage_Logs);
            this.DeveloperToolsForm_TabControl_Main.Controls.Add(this.DeveloperToolsForm_TabPage_Feedback);
            this.DeveloperToolsForm_TabControl_Main.Controls.Add(this.DeveloperToolsForm_TabPage_SystemInfo);
            this.DeveloperToolsForm_TabControl_Main.Dock = DockStyle.Fill;
            this.DeveloperToolsForm_TabControl_Main.Location = new Point(0, 25);
            this.DeveloperToolsForm_TabControl_Main.Name = "DeveloperToolsForm_TabControl_Main";
            this.DeveloperToolsForm_TabControl_Main.SelectedIndex = 0;
            this.DeveloperToolsForm_TabControl_Main.Size = new Size(800, 425);
            this.DeveloperToolsForm_TabControl_Main.TabIndex = 1;
            // 
            // DeveloperToolsForm_TabPage_Dashboard
            // 
            this.DeveloperToolsForm_TabPage_Dashboard.Controls.Add(this.DeveloperToolsForm_TableLayout_Dashboard);
            this.DeveloperToolsForm_TabPage_Dashboard.Location = new Point(4, 24);
            this.DeveloperToolsForm_TabPage_Dashboard.Name = "DeveloperToolsForm_TabPage_Dashboard";
            this.DeveloperToolsForm_TabPage_Dashboard.Padding = new Padding(3);
            this.DeveloperToolsForm_TabPage_Dashboard.Size = new Size(792, 397);
            this.DeveloperToolsForm_TabPage_Dashboard.TabIndex = 0;
            this.DeveloperToolsForm_TabPage_Dashboard.Text = "Dashboard";
            this.DeveloperToolsForm_TabPage_Dashboard.UseVisualStyleBackColor = true;
            // 
            // DeveloperToolsForm_TableLayout_Dashboard
            // 
            this.DeveloperToolsForm_TableLayout_Dashboard.ColumnCount = 1;
            this.DeveloperToolsForm_TableLayout_Dashboard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.DeveloperToolsForm_TableLayout_Dashboard.Controls.Add(this.DeveloperToolsForm_Control_SystemHealth, 0, 0);
            this.DeveloperToolsForm_TableLayout_Dashboard.Controls.Add(this.DeveloperToolsForm_Control_LogStatistics, 0, 1);
            this.DeveloperToolsForm_TableLayout_Dashboard.Controls.Add(this.DeveloperToolsForm_Control_RecentErrors, 0, 2);
            this.DeveloperToolsForm_TableLayout_Dashboard.Dock = DockStyle.Fill;
            this.DeveloperToolsForm_TableLayout_Dashboard.Location = new Point(3, 3);
            this.DeveloperToolsForm_TableLayout_Dashboard.Name = "DeveloperToolsForm_TableLayout_Dashboard";
            this.DeveloperToolsForm_TableLayout_Dashboard.RowCount = 3;
            this.DeveloperToolsForm_TableLayout_Dashboard.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            this.DeveloperToolsForm_TableLayout_Dashboard.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
            this.DeveloperToolsForm_TableLayout_Dashboard.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.DeveloperToolsForm_TableLayout_Dashboard.Size = new Size(786, 416);
            this.DeveloperToolsForm_TableLayout_Dashboard.TabIndex = 0;
            // 
            // DeveloperToolsForm_Control_SystemHealth
            // 
            this.DeveloperToolsForm_Control_SystemHealth.Dock = DockStyle.Fill;
            this.DeveloperToolsForm_Control_SystemHealth.Location = new Point(3, 3);
            this.DeveloperToolsForm_Control_SystemHealth.Name = "DeveloperToolsForm_Control_SystemHealth";
            this.DeveloperToolsForm_Control_SystemHealth.Size = new Size(780, 74);
            this.DeveloperToolsForm_Control_SystemHealth.TabIndex = 0;
            // 
            // DeveloperToolsForm_Control_LogStatistics
            // 
            this.DeveloperToolsForm_Control_LogStatistics.Dock = DockStyle.Fill;
            this.DeveloperToolsForm_Control_LogStatistics.Location = new Point(3, 83);
            this.DeveloperToolsForm_Control_LogStatistics.Name = "DeveloperToolsForm_Control_LogStatistics";
            this.DeveloperToolsForm_Control_LogStatistics.Size = new Size(780, 84);
            this.DeveloperToolsForm_Control_LogStatistics.TabIndex = 1;
            // 
            // DeveloperToolsForm_Control_RecentErrors
            // 
            this.DeveloperToolsForm_Control_RecentErrors.Dock = DockStyle.Fill;
            this.DeveloperToolsForm_Control_RecentErrors.Location = new Point(3, 173);
            this.DeveloperToolsForm_Control_RecentErrors.Name = "DeveloperToolsForm_Control_RecentErrors";
            this.DeveloperToolsForm_Control_RecentErrors.Size = new Size(780, 240);
            this.DeveloperToolsForm_Control_RecentErrors.TabIndex = 2;
            // 
            // DeveloperToolsForm_TabPage_Logs
            // 
            this.DeveloperToolsForm_TabPage_Logs.Controls.Add(this.DeveloperToolsForm_Control_LogViewer);
            this.DeveloperToolsForm_TabPage_Logs.Location = new Point(4, 24);
            this.DeveloperToolsForm_TabPage_Logs.Name = "DeveloperToolsForm_TabPage_Logs";
            this.DeveloperToolsForm_TabPage_Logs.Padding = new Padding(3);
            this.DeveloperToolsForm_TabPage_Logs.Size = new Size(792, 397);
            this.DeveloperToolsForm_TabPage_Logs.TabIndex = 1;
            this.DeveloperToolsForm_TabPage_Logs.Text = "Logs";
            this.DeveloperToolsForm_TabPage_Logs.UseVisualStyleBackColor = true;
            // 
            // DeveloperToolsForm_Control_LogViewer
            // 
            this.DeveloperToolsForm_Control_LogViewer.Dock = DockStyle.Fill;
            this.DeveloperToolsForm_Control_LogViewer.Location = new Point(3, 3);
            this.DeveloperToolsForm_Control_LogViewer.Name = "DeveloperToolsForm_Control_LogViewer";
            this.DeveloperToolsForm_Control_LogViewer.Size = new Size(786, 391);
            this.DeveloperToolsForm_Control_LogViewer.TabIndex = 0;
            // 
            // DeveloperToolsForm_TabPage_Feedback
            // 
            this.DeveloperToolsForm_TabPage_Feedback.Controls.Add(this.DeveloperToolsForm_Control_FeedbackManager);
            this.DeveloperToolsForm_TabPage_Feedback.Location = new Point(4, 24);
            this.DeveloperToolsForm_TabPage_Feedback.Name = "DeveloperToolsForm_TabPage_Feedback";
            this.DeveloperToolsForm_TabPage_Feedback.Padding = new Padding(3);
            this.DeveloperToolsForm_TabPage_Feedback.Size = new Size(792, 422);
            this.DeveloperToolsForm_TabPage_Feedback.TabIndex = 2;
            this.DeveloperToolsForm_TabPage_Feedback.Text = "Feedback";
            this.DeveloperToolsForm_TabPage_Feedback.UseVisualStyleBackColor = true;
            // 
            // DeveloperToolsForm_Control_FeedbackManager
            // 
            this.DeveloperToolsForm_Control_FeedbackManager.Dock = DockStyle.Fill;
            this.DeveloperToolsForm_Control_FeedbackManager.Location = new Point(3, 3);
            this.DeveloperToolsForm_Control_FeedbackManager.Name = "DeveloperToolsForm_Control_FeedbackManager";
            this.DeveloperToolsForm_Control_FeedbackManager.Size = new Size(786, 416);
            this.DeveloperToolsForm_Control_FeedbackManager.TabIndex = 0;
            // 
            // DeveloperToolsForm_TabPage_SystemInfo
            // 
            this.DeveloperToolsForm_TabPage_SystemInfo.Controls.Add(this.DeveloperToolsForm_Control_SystemInfo);
            this.DeveloperToolsForm_TabPage_SystemInfo.Location = new Point(4, 24);
            this.DeveloperToolsForm_TabPage_SystemInfo.Name = "DeveloperToolsForm_TabPage_SystemInfo";
            this.DeveloperToolsForm_TabPage_SystemInfo.Padding = new Padding(3);
            this.DeveloperToolsForm_TabPage_SystemInfo.Size = new Size(792, 422);
            this.DeveloperToolsForm_TabPage_SystemInfo.TabIndex = 3;
            this.DeveloperToolsForm_TabPage_SystemInfo.Text = "System Info";
            this.DeveloperToolsForm_TabPage_SystemInfo.UseVisualStyleBackColor = true;
            // 
            // DeveloperToolsForm_Control_SystemInfo
            // 
            this.DeveloperToolsForm_Control_SystemInfo.Dock = DockStyle.Fill;
            this.DeveloperToolsForm_Control_SystemInfo.Location = new Point(3, 3);
            this.DeveloperToolsForm_Control_SystemInfo.Name = "DeveloperToolsForm_Control_SystemInfo";
            this.DeveloperToolsForm_Control_SystemInfo.Size = new Size(786, 416);
            this.DeveloperToolsForm_Control_SystemInfo.TabIndex = 0;
            // 
            // DeveloperToolsForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(800, 450);
            this.Controls.Add(this.DeveloperToolsForm_TabControl_Main);
            this.Controls.Add(this.DeveloperToolsForm_ToolStrip_Main);
            this.Name = "DeveloperToolsForm";
            this.Text = "Developer Tools";
            this.Load += new System.EventHandler(this.DeveloperToolsForm_Load);
            this.DeveloperToolsForm_ToolStrip_Main.ResumeLayout(false);
            this.DeveloperToolsForm_ToolStrip_Main.PerformLayout();
            this.DeveloperToolsForm_TabControl_Main.ResumeLayout(false);
            this.DeveloperToolsForm_TabPage_Dashboard.ResumeLayout(false);
            this.DeveloperToolsForm_TableLayout_Dashboard.ResumeLayout(false);
            this.DeveloperToolsForm_TabPage_Logs.ResumeLayout(false);
            this.DeveloperToolsForm_TabPage_Feedback.ResumeLayout(false);
            this.DeveloperToolsForm_TabPage_SystemInfo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStrip DeveloperToolsForm_ToolStrip_Main;
        private ToolStripButton DeveloperToolsForm_Button_Refresh;
        private TabControl DeveloperToolsForm_TabControl_Main;
        private TabPage DeveloperToolsForm_TabPage_Dashboard;
        private TabPage DeveloperToolsForm_TabPage_Logs;
        private TabPage DeveloperToolsForm_TabPage_Feedback;
        private TabPage DeveloperToolsForm_TabPage_SystemInfo;
        private TableLayoutPanel DeveloperToolsForm_TableLayout_Dashboard;
        private MTM_WIP_Application_Winforms.Controls.DeveloperTools.Control_SystemHealth DeveloperToolsForm_Control_SystemHealth;
        private MTM_WIP_Application_Winforms.Controls.DeveloperTools.Control_LogStatistics DeveloperToolsForm_Control_LogStatistics;
        private MTM_WIP_Application_Winforms.Controls.DeveloperTools.Control_LogViewer DeveloperToolsForm_Control_LogViewer;
        private MTM_WIP_Application_Winforms.Controls.DeveloperTools.Control_RecentErrors DeveloperToolsForm_Control_RecentErrors;
        private MTM_WIP_Application_Winforms.Controls.DeveloperTools.Control_SystemInfo DeveloperToolsForm_Control_SystemInfo;
        private MTM_WIP_Application_Winforms.Controls.DeveloperTools.Control_FeedbackManager DeveloperToolsForm_Control_FeedbackManager;
    }
}



