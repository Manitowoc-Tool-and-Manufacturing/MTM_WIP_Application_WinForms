using System.Drawing;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Controls.Addons;
using MTM_WIP_Application_Winforms.Controls.MainForm;

using MTM_WIP_Application_Winforms.Forms.Shared;

namespace MTM_WIP_Application_Winforms.Forms.MainForm
{
    public partial class MainForm : ThemedForm
    {
        #region Fields



        private System.ComponentModel.IContainer components = null;

        #endregion

        public MenuStrip MainForm_MenuStrip;
        private ToolStripMenuItem MainForm_MenuStrip_File;
        private ToolStripMenuItem MainForm_MenuStrip_File_Settings;
        private ToolStripMenuItem MainForm_MenuStrip_Exit;
        private ToolStripMenuItem MainForm_MenuStrip_View;
        private ToolStripMenuItem MainForm_MenuStrip_View_PersonalHistory;
        private ToolStripMenuItem MainForm_MenuStrip_View_SystemHealth;
        private ToolStripSeparator MainForm_MenuStrip_View_Separator2;
        private ToolStripMenuItem MainForm_MenuStrip_Visual;
        private ToolStripMenuItem MainForm_MenuStrip_Visual_Inventory;
        private ToolStripMenuItem MainForm_MenuStrip_Visual_Receiving;
        private ToolStripMenuItem MainForm_MenuStrip_Visual_DieTool;
        private ToolStripMenuItem MainForm_MenuStrip_Visual_Audit;
        private ToolStripMenuItem MainForm_MenuStrip_Visual_Header_Inventory;
        private ToolStripMenuItem MainForm_MenuStrip_Visual_Header_Departments;
        private ToolStripMenuItem MainForm_MenuStrip_Visual_Header_Tools;
        private ToolStripMenuItem MainForm_MenuStrip_Visual_Header_Analytics;
        private ToolStripSeparator MainForm_MenuStrip_Visual_Separator1;
        private ToolStripSeparator MainForm_MenuStrip_Visual_Separator2;
        private ToolStripSeparator MainForm_MenuStrip_Visual_Separator3;
        private ToolStripMenuItem MainForm_MenuStrip_Visual_UserAnalytics;

        private StatusStrip MainForm_StatusStrip;
        public ToolStripStatusLabel MainForm_StatusStrip_SavedStatus;
        public ToolStripStatusLabel MainForm_StatusStrip_Disconnected;
        private ToolStripProgressBar MainForm_ProgressBar;
        public ToolStripStatusLabel MainForm_StatusText;

        private System.Drawing.Printing.PrintDocument MainForm_Inventory_PrintDocument;
        private PrintPreviewDialog MainForm_Inventory_PrintDialog;

        private System.Windows.Forms.Timer MainForm_Last10_Timer;
        public Control_ConnectionStrengthControl MainForm_UserControl_SignalStrength;
        private TableLayoutPanel MainForm_TableLayout;
        public SplitContainer MainForm_SplitContainer_Middle;
        public TabControl MainForm_TabControl;
        private TabPage MainForm_TabPage_Inventory;
        private TabPage MainForm_TabPage_Remove;
        private TabPage MainForm_TabPage_Transfer;
        public Control_RemoveTab MainForm_UserControl_RemoveTab;
        public Control_TransferTab MainForm_UserControl_TransferTab;
        public Control_InventoryTab MainForm_UserControl_InventoryTab;
        public Control_AdvancedInventory MainForm_UserControl_AdvancedInventory;
        public Control_AdvancedRemove MainForm_UserControl_AdvancedRemove;
        public Control_QuickButtons MainForm_UserControl_QuickButtons;



        #region Methods


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        #endregion
        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            MainForm_MenuStrip = new MenuStrip();
            MainForm_MenuStrip_File = new ToolStripMenuItem();
            MainForm_MenuStrip_File_Settings = new ToolStripMenuItem();
            MainForm_MenuStrip_Exit = new ToolStripMenuItem();
            MainForm_MenuStrip_View = new ToolStripMenuItem();
            MainForm_MenuStrip_View_PersonalHistory = new ToolStripMenuItem();
            MainForm_MenuStrip_View_SystemHealth = new ToolStripMenuItem();
            MainForm_MenuStrip_View_Separator2 = new ToolStripSeparator();
            MainForm_MenuStrip_Visual = new ToolStripMenuItem();
            MainForm_MenuStrip_Visual_Inventory = new ToolStripMenuItem();
            MainForm_MenuStrip_Visual_Receiving = new ToolStripMenuItem();
            MainForm_MenuStrip_Visual_DieTool = new ToolStripMenuItem();
            MainForm_MenuStrip_Visual_Audit = new ToolStripMenuItem();
            MainForm_MenuStrip_Visual_Header_Inventory = new ToolStripMenuItem();
            MainForm_MenuStrip_Visual_Header_Departments = new ToolStripMenuItem();
            MainForm_MenuStrip_Visual_Header_Tools = new ToolStripMenuItem();
            MainForm_MenuStrip_Visual_Header_Analytics = new ToolStripMenuItem();
            MainForm_MenuStrip_Visual_Separator1 = new ToolStripSeparator();
            MainForm_MenuStrip_Visual_Separator2 = new ToolStripSeparator();
            MainForm_MenuStrip_Visual_Separator3 = new ToolStripSeparator();
            MainForm_MenuStrip_Visual_UserAnalytics = new ToolStripMenuItem();
            MainForm_MenuStrip_Development = new ToolStripMenuItem();
            MainForm_MenuStrip_Development_Header_Tools = new ToolStripMenuItem();
            MainForm_MenuStrip_Development_Dashboard = new ToolStripMenuItem();
            MainForm_MenuStrip_Development_Logs = new ToolStripMenuItem();
            MainForm_MenuStrip_Development_Database = new ToolStripMenuItem();
            MainForm_MenuStrip_Development_SystemInfo = new ToolStripMenuItem();
            MainForm_MenuStrip_Development_Feedback = new ToolStripMenuItem();
            MainForm_MenuStrip_Development_Separator1 = new ToolStripSeparator();
            MainForm_MenuStrip_Development_Header_Maintenance = new ToolStripMenuItem();
            MainForm_MenuStrip_Help = new ToolStripMenuItem();
            MainForm_MenuStrip_Help_ViewHelp = new ToolStripMenuItem();
            MainForm_MenuStrip_Help_ViewReleaseNotes = new ToolStripMenuItem();
            MainForm_MenuStrip_Help_About = new ToolStripMenuItem();
            MainForm_StatusStrip = new StatusStrip();
            MainForm_ProgressBar = new ToolStripProgressBar();
            MainForm_StatusText = new ToolStripStatusLabel();
            MainForm_StatusStrip_SavedStatus = new ToolStripStatusLabel();
            MainForm_StatusStrip_Disconnected = new ToolStripStatusLabel();
            MainForm_Inventory_PrintDocument = new System.Drawing.Printing.PrintDocument();
            MainForm_Inventory_PrintDialog = new PrintPreviewDialog();
            MainForm_ToolTip = new ToolTip(components);
            MainForm_Last10_Timer = new System.Windows.Forms.Timer(components);
            MainForm_UserControl_SignalStrength = new Control_ConnectionStrengthControl();
            MainForm_TableLayout = new TableLayoutPanel();
            MainForm_SplitContainer_Middle = new SplitContainer();
            MainForm_TabControl = new TabControl();
            MainForm_TabPage_Inventory = new TabPage();
            MainForm_UserControl_InventoryTab = new Control_InventoryTab();
            MainForm_UserControl_AdvancedInventory = new Control_AdvancedInventory();
            MainForm_TabPage_Remove = new TabPage();
            MainForm_UserControl_RemoveTab = new Control_RemoveTab();
            MainForm_UserControl_AdvancedRemove = new Control_AdvancedRemove();
            MainForm_TabPage_Transfer = new TabPage();
            MainForm_UserControl_TransferTab = new Control_TransferTab();
            MainForm_UserControl_QuickButtons = new Control_QuickButtons();
            tableLayoutPanel1 = new TableLayoutPanel();
            MainForm_MenuStrip.SuspendLayout();
            MainForm_StatusStrip.SuspendLayout();
            MainForm_TableLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)MainForm_SplitContainer_Middle).BeginInit();
            MainForm_SplitContainer_Middle.Panel1.SuspendLayout();
            MainForm_SplitContainer_Middle.Panel2.SuspendLayout();
            MainForm_SplitContainer_Middle.SuspendLayout();
            MainForm_TabControl.SuspendLayout();
            MainForm_TabPage_Inventory.SuspendLayout();
            MainForm_TabPage_Remove.SuspendLayout();
            MainForm_TabPage_Transfer.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // MainForm_MenuStrip
            // 
            MainForm_MenuStrip.Dock = DockStyle.Fill;
            MainForm_MenuStrip.ImageScalingSize = new Size(24, 24);
            MainForm_MenuStrip.Items.AddRange(new ToolStripItem[] { MainForm_MenuStrip_File, MainForm_MenuStrip_View, MainForm_MenuStrip_Visual, MainForm_MenuStrip_Development, MainForm_MenuStrip_Help });
            MainForm_MenuStrip.Location = new Point(0, 0);
            MainForm_MenuStrip.Name = "MainForm_MenuStrip";
            MainForm_MenuStrip.Padding = new Padding(0);
            MainForm_MenuStrip.Size = new Size(950, 24);
            MainForm_MenuStrip.TabIndex = 1;
            // 
            // MainForm_MenuStrip_File
            // 
            MainForm_MenuStrip_File.DropDownItems.AddRange(new ToolStripItem[] { MainForm_MenuStrip_File_Settings, MainForm_MenuStrip_Exit });
            MainForm_MenuStrip_File.Name = "MainForm_MenuStrip_File";
            MainForm_MenuStrip_File.Size = new Size(37, 24);
            MainForm_MenuStrip_File.Text = "File";
            // 
            // MainForm_MenuStrip_File_Settings
            // 
            MainForm_MenuStrip_File_Settings.Name = "MainForm_MenuStrip_File_Settings";
            MainForm_MenuStrip_File_Settings.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            MainForm_MenuStrip_File_Settings.Size = new Size(188, 22);
            MainForm_MenuStrip_File_Settings.Text = "⚙️ Settings";
            MainForm_MenuStrip_File_Settings.Click += MainForm_MenuStrip_File_Settings_Click;
            // 
            // MainForm_MenuStrip_Exit
            // 
            MainForm_MenuStrip_Exit.Name = "MainForm_MenuStrip_Exit";
            MainForm_MenuStrip_Exit.ShortcutKeys = Keys.Alt | Keys.F4;
            MainForm_MenuStrip_Exit.Size = new Size(188, 22);
            MainForm_MenuStrip_Exit.Text = "Exit";
            MainForm_MenuStrip_Exit.Click += MainForm_MenuStrip_Exit_Click;
            // 
            // MainForm_MenuStrip_View
            // 
            MainForm_MenuStrip_View.DropDownItems.AddRange(new ToolStripItem[] { MainForm_MenuStrip_View_PersonalHistory, MainForm_MenuStrip_View_SystemHealth, MainForm_MenuStrip_View_Separator2 });
            MainForm_MenuStrip_View.Name = "MainForm_MenuStrip_View";
            MainForm_MenuStrip_View.Size = new Size(44, 24);
            MainForm_MenuStrip_View.Text = "View";
            // 
            // MainForm_MenuStrip_View_PersonalHistory
            // 
            MainForm_MenuStrip_View_PersonalHistory.Name = "MainForm_MenuStrip_View_PersonalHistory";
            MainForm_MenuStrip_View_PersonalHistory.ShortcutKeys = Keys.Control | Keys.H;
            MainForm_MenuStrip_View_PersonalHistory.Size = new Size(219, 22);
            MainForm_MenuStrip_View_PersonalHistory.Text = "📜 Transaction History";
            MainForm_MenuStrip_View_PersonalHistory.Click += MainForm_MenuStrip_View_PersonalHistory_Click;
            // 
            // MainForm_MenuStrip_View_SystemHealth
            // 
            MainForm_MenuStrip_View_SystemHealth.Name = "MainForm_MenuStrip_View_SystemHealth";
            MainForm_MenuStrip_View_SystemHealth.ShortcutKeys = Keys.Control | Keys.Shift | Keys.H;
            MainForm_MenuStrip_View_SystemHealth.Size = new Size(228, 22);
            MainForm_MenuStrip_View_SystemHealth.Text = "🏥 System Health";
            MainForm_MenuStrip_View_SystemHealth.Click += MainForm_MenuStrip_View_SystemHealth_Click;
            // 
            // MainForm_MenuStrip_View_Separator2
            // 
            MainForm_MenuStrip_View_Separator2.Name = "MainForm_MenuStrip_View_Separator2";
            MainForm_MenuStrip_View_Separator2.Size = new Size(216, 6);
            // 
            // MainForm_MenuStrip_Visual
            // 
            MainForm_MenuStrip_Visual.DropDownItems.AddRange(new ToolStripItem[] { MainForm_MenuStrip_Visual_Header_Inventory, MainForm_MenuStrip_Visual_Inventory, MainForm_MenuStrip_Visual_Audit, MainForm_MenuStrip_Visual_Separator1, MainForm_MenuStrip_Visual_Header_Departments, MainForm_MenuStrip_Visual_Receiving, MainForm_MenuStrip_Visual_Separator2, MainForm_MenuStrip_Visual_Header_Tools, MainForm_MenuStrip_Visual_DieTool, MainForm_MenuStrip_Visual_Separator3, MainForm_MenuStrip_Visual_Header_Analytics, MainForm_MenuStrip_Visual_UserAnalytics });
            MainForm_MenuStrip_Visual.Name = "MainForm_MenuStrip_Visual";
            MainForm_MenuStrip_Visual.Size = new Size(50, 24);
            MainForm_MenuStrip_Visual.Text = "Visual";
            // 
            // MainForm_MenuStrip_Visual_Header_Inventory
            // 
            MainForm_MenuStrip_Visual_Header_Inventory.Enabled = false;
            MainForm_MenuStrip_Visual_Header_Inventory.Name = "MainForm_MenuStrip_Visual_Header_Inventory";
            MainForm_MenuStrip_Visual_Header_Inventory.Size = new Size(220, 22);
            MainForm_MenuStrip_Visual_Header_Inventory.Text = "Inventory Management";
            // 
            // MainForm_MenuStrip_Visual_Inventory
            // 
            MainForm_MenuStrip_Visual_Inventory.Name = "MainForm_MenuStrip_Visual_Inventory";
            MainForm_MenuStrip_Visual_Inventory.Size = new Size(220, 22);
            MainForm_MenuStrip_Visual_Inventory.Text = "📦 Visual Inventory";
            MainForm_MenuStrip_Visual_Inventory.Click += MainForm_MenuStrip_Visual_Inventory_Click;
            // 
            // MainForm_MenuStrip_Visual_Audit
            // 
            MainForm_MenuStrip_Visual_Audit.Name = "MainForm_MenuStrip_Visual_Audit";
            MainForm_MenuStrip_Visual_Audit.Size = new Size(220, 22);
            MainForm_MenuStrip_Visual_Audit.Text = "📋 Visual Inventory History";
            MainForm_MenuStrip_Visual_Audit.Click += MainForm_MenuStrip_Visual_Audit_Click;
            // 
            // MainForm_MenuStrip_Visual_Separator1
            // 
            MainForm_MenuStrip_Visual_Separator1.Name = "MainForm_MenuStrip_Visual_Separator1";
            MainForm_MenuStrip_Visual_Separator1.Size = new Size(217, 6);
            // 
            // MainForm_MenuStrip_Visual_Header_Departments
            // 
            MainForm_MenuStrip_Visual_Header_Departments.Enabled = false;
            MainForm_MenuStrip_Visual_Header_Departments.Name = "MainForm_MenuStrip_Visual_Header_Departments";
            MainForm_MenuStrip_Visual_Header_Departments.Size = new Size(220, 22);
            MainForm_MenuStrip_Visual_Header_Departments.Text = "Departments";
            // 
            // MainForm_MenuStrip_Visual_Receiving
            // 
            MainForm_MenuStrip_Visual_Receiving.Name = "MainForm_MenuStrip_Visual_Receiving";
            MainForm_MenuStrip_Visual_Receiving.Size = new Size(220, 22);
            MainForm_MenuStrip_Visual_Receiving.Text = "📥 Receiving Department";
            MainForm_MenuStrip_Visual_Receiving.Click += MainForm_MenuStrip_Visual_Receiving_Click;
            // 
            // MainForm_MenuStrip_Visual_Separator2
            // 
            MainForm_MenuStrip_Visual_Separator2.Name = "MainForm_MenuStrip_Visual_Separator2";
            MainForm_MenuStrip_Visual_Separator2.Size = new Size(217, 6);
            // 
            // MainForm_MenuStrip_Visual_Header_Tools
            // 
            MainForm_MenuStrip_Visual_Header_Tools.Enabled = false;
            MainForm_MenuStrip_Visual_Header_Tools.Name = "MainForm_MenuStrip_Visual_Header_Tools";
            MainForm_MenuStrip_Visual_Header_Tools.Size = new Size(220, 22);
            MainForm_MenuStrip_Visual_Header_Tools.Text = "Tools";
            // 
            // MainForm_MenuStrip_Visual_DieTool
            // 
            MainForm_MenuStrip_Visual_DieTool.Name = "MainForm_MenuStrip_Visual_DieTool";
            MainForm_MenuStrip_Visual_DieTool.Size = new Size(220, 22);
            MainForm_MenuStrip_Visual_DieTool.Text = "🛠️ Die && Part Information";
            MainForm_MenuStrip_Visual_DieTool.Click += MainForm_MenuStrip_Visual_DieTool_Click;
            // 
            // MainForm_MenuStrip_Visual_Separator3
            // 
            MainForm_MenuStrip_Visual_Separator3.Name = "MainForm_MenuStrip_Visual_Separator3";
            MainForm_MenuStrip_Visual_Separator3.Size = new Size(217, 6);
            // 
            // MainForm_MenuStrip_Visual_Header_Analytics
            // 
            MainForm_MenuStrip_Visual_Header_Analytics.Enabled = false;
            MainForm_MenuStrip_Visual_Header_Analytics.Name = "MainForm_MenuStrip_Visual_Header_Analytics";
            MainForm_MenuStrip_Visual_Header_Analytics.Size = new Size(220, 22);
            MainForm_MenuStrip_Visual_Header_Analytics.Text = "Analytics";
            // 
            // MainForm_MenuStrip_Visual_UserAnalytics
            // 
            MainForm_MenuStrip_Visual_UserAnalytics.Name = "MainForm_MenuStrip_Visual_UserAnalytics";
            MainForm_MenuStrip_Visual_UserAnalytics.Size = new Size(220, 22);
            MainForm_MenuStrip_Visual_UserAnalytics.Text = "📈 Visual User Analytics";
            MainForm_MenuStrip_Visual_UserAnalytics.Click += MainForm_MenuStrip_Visual_UserAnalytics_Click;
            // 
            // MainForm_MenuStrip_Development
            // 
            MainForm_MenuStrip_Development.DropDownItems.AddRange(new ToolStripItem[] { MainForm_MenuStrip_Development_Header_Tools, MainForm_MenuStrip_Development_Dashboard, MainForm_MenuStrip_Development_Logs, MainForm_MenuStrip_Development_Database, MainForm_MenuStrip_Development_SystemInfo, MainForm_MenuStrip_Development_Feedback, MainForm_MenuStrip_Development_Separator1, MainForm_MenuStrip_Development_Header_Maintenance });
            MainForm_MenuStrip_Development.Name = "MainForm_MenuStrip_Development";
            MainForm_MenuStrip_Development.Size = new Size(90, 24);
            MainForm_MenuStrip_Development.Text = "Development";
            // 
            // MainForm_MenuStrip_Development_Header_Tools
            // 
            MainForm_MenuStrip_Development_Header_Tools.Enabled = false;
            MainForm_MenuStrip_Development_Header_Tools.Name = "MainForm_MenuStrip_Development_Header_Tools";
            MainForm_MenuStrip_Development_Header_Tools.Size = new Size(278, 22);
            MainForm_MenuStrip_Development_Header_Tools.Text = "Developer Tools";
            // 
            // MainForm_MenuStrip_Development_Dashboard
            // 
            MainForm_MenuStrip_Development_Dashboard.Name = "MainForm_MenuStrip_Development_Dashboard";
            MainForm_MenuStrip_Development_Dashboard.Size = new Size(278, 22);
            MainForm_MenuStrip_Development_Dashboard.Text = "📊 Dashboard";
            MainForm_MenuStrip_Development_Dashboard.Click += MainForm_MenuStrip_Development_Dashboard_Click;
            // 
            // MainForm_MenuStrip_Development_Logs
            // 
            MainForm_MenuStrip_Development_Logs.Name = "MainForm_MenuStrip_Development_Logs";
            MainForm_MenuStrip_Development_Logs.Size = new Size(278, 22);
            MainForm_MenuStrip_Development_Logs.Text = "📄 Logs";
            MainForm_MenuStrip_Development_Logs.Click += MainForm_MenuStrip_Development_Logs_Click;
            // 
            // MainForm_MenuStrip_Development_Database
            // 
            MainForm_MenuStrip_Development_Database.Name = "MainForm_MenuStrip_Development_Database";
            MainForm_MenuStrip_Development_Database.Size = new Size(278, 22);
            MainForm_MenuStrip_Development_Database.Text = "🗄️ Database";
            MainForm_MenuStrip_Development_Database.Click += MainForm_MenuStrip_Development_Database_Click;
            // 
            // MainForm_MenuStrip_Development_SystemInfo
            // 
            MainForm_MenuStrip_Development_SystemInfo.Name = "MainForm_MenuStrip_Development_SystemInfo";
            MainForm_MenuStrip_Development_SystemInfo.Size = new Size(278, 22);
            MainForm_MenuStrip_Development_SystemInfo.Text = "ℹ️ System Info";
            MainForm_MenuStrip_Development_SystemInfo.Click += MainForm_MenuStrip_Development_SystemInfo_Click;
            // 
            // MainForm_MenuStrip_Development_Feedback
            // 
            MainForm_MenuStrip_Development_Feedback.Name = "MainForm_MenuStrip_Development_Feedback";
            MainForm_MenuStrip_Development_Feedback.Size = new Size(278, 22);
            MainForm_MenuStrip_Development_Feedback.Text = "💬 Feedback";
            MainForm_MenuStrip_Development_Feedback.Click += MainForm_MenuStrip_Development_Feedback_Click;
            // 
            // MainForm_MenuStrip_Development_Separator1
            // 
            MainForm_MenuStrip_Development_Separator1.Name = "MainForm_MenuStrip_Development_Separator1";
            MainForm_MenuStrip_Development_Separator1.Size = new Size(275, 6);
            // 
            // MainForm_MenuStrip_Development_Header_Maintenance
            // 
            MainForm_MenuStrip_Development_Header_Maintenance.Enabled = false;
            MainForm_MenuStrip_Development_Header_Maintenance.Name = "MainForm_MenuStrip_Development_Header_Maintenance";
            MainForm_MenuStrip_Development_Header_Maintenance.Size = new Size(278, 22);
            MainForm_MenuStrip_Development_Header_Maintenance.Text = "Maintenance";
            // 
            // MainForm_MenuStrip_Help
            // 
            MainForm_MenuStrip_Help.DropDownItems.AddRange(new ToolStripItem[] { MainForm_MenuStrip_Help_ViewHelp, MainForm_MenuStrip_Help_ViewReleaseNotes, MainForm_MenuStrip_Help_About });
            MainForm_MenuStrip_Help.Name = "MainForm_MenuStrip_Help";
            MainForm_MenuStrip_Help.Size = new Size(44, 24);
            MainForm_MenuStrip_Help.Text = "Help";
            MainForm_MenuStrip_Help.Visible = true;
            // 
            // MainForm_MenuStrip_Help_ViewHelp
            // 
            MainForm_MenuStrip_Help_ViewHelp.Name = "MainForm_MenuStrip_Help_ViewHelp";
            MainForm_MenuStrip_Help_ViewHelp.ShortcutKeys = Keys.F1;
            MainForm_MenuStrip_Help_ViewHelp.Size = new Size(257, 22);
            MainForm_MenuStrip_Help_ViewHelp.Text = "📖 View Help";
            MainForm_MenuStrip_Help_ViewHelp.Click += MainForm_MenuStrip_Help_ViewHelp_Click;
            // 
            // MainForm_MenuStrip_Help_ViewReleaseNotes
            // 
            MainForm_MenuStrip_Help_ViewReleaseNotes.Name = "MainForm_MenuStrip_Help_ViewReleaseNotes";
            MainForm_MenuStrip_Help_ViewReleaseNotes.Size = new Size(257, 22);
            MainForm_MenuStrip_Help_ViewReleaseNotes.Text = "📝 Release Notes";
            MainForm_MenuStrip_Help_ViewReleaseNotes.Click += MainForm_MenuStrip_Help_ReleaseNotes_Click;
            // 
            // MainForm_MenuStrip_Help_About
            // 
            MainForm_MenuStrip_Help_About.Name = "MainForm_MenuStrip_Help_About";
            MainForm_MenuStrip_Help_About.Size = new Size(257, 22);
            MainForm_MenuStrip_Help_About.Text = "ℹ️ About";
            MainForm_MenuStrip_Help_About.Click += MainForm_MenuStrip_Help_About_Click;
            // 
            // MainForm_StatusStrip
            // 
            MainForm_StatusStrip.BackColor = SystemColors.Control;
            MainForm_StatusStrip.Dock = DockStyle.Fill;
            MainForm_StatusStrip.ImageScalingSize = new Size(24, 24);
            MainForm_StatusStrip.Items.AddRange(new ToolStripItem[] { MainForm_ProgressBar, MainForm_StatusText, MainForm_StatusStrip_SavedStatus, MainForm_StatusStrip_Disconnected });
            MainForm_StatusStrip.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            MainForm_StatusStrip.Location = new Point(0, 0);
            MainForm_StatusStrip.Name = "MainForm_StatusStrip";
            MainForm_StatusStrip.Size = new Size(891, 31);
            MainForm_StatusStrip.SizingGrip = false;
            MainForm_StatusStrip.TabIndex = 18;
            // 
            // MainForm_ProgressBar
            // 
            MainForm_ProgressBar.Name = "MainForm_ProgressBar";
            MainForm_ProgressBar.Size = new Size(100, 25);
            MainForm_ProgressBar.Visible = false;
            // 
            // MainForm_StatusText
            // 
            MainForm_StatusText.Name = "MainForm_StatusText";
            MainForm_StatusText.Size = new Size(39, 26);
            MainForm_StatusText.Text = "Ready";
            // 
            // MainForm_StatusStrip_SavedStatus
            // 
            MainForm_StatusStrip_SavedStatus.Name = "MainForm_StatusStrip_SavedStatus";
            MainForm_StatusStrip_SavedStatus.Size = new Size(0, 26);
            // 
            // MainForm_StatusStrip_Disconnected
            // 
            MainForm_StatusStrip_Disconnected.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold | FontStyle.Italic);
            MainForm_StatusStrip_Disconnected.Name = "MainForm_StatusStrip_Disconnected";
            MainForm_StatusStrip_Disconnected.Size = new Size(240, 26);
            MainForm_StatusStrip_Disconnected.Text = "Disconnected from Server, please standby...";
            MainForm_StatusStrip_Disconnected.Visible = false;
            // 
            // MainForm_Inventory_PrintDialog
            // 
            MainForm_Inventory_PrintDialog.AutoScrollMargin = new Size(0, 0);
            MainForm_Inventory_PrintDialog.AutoScrollMinSize = new Size(0, 0);
            MainForm_Inventory_PrintDialog.ClientSize = new Size(400, 300);
            MainForm_Inventory_PrintDialog.Enabled = true;
            MainForm_Inventory_PrintDialog.Icon = (Icon)resources.GetObject("MainForm_Inventory_PrintDialog.Icon");
            MainForm_Inventory_PrintDialog.Name = "MainForm_Inventory_PrintDialog";
            MainForm_Inventory_PrintDialog.Visible = false;
            // 
            // MainForm_Last10_Timer
            // 
            MainForm_Last10_Timer.Enabled = true;
            MainForm_Last10_Timer.Interval = 15000;
            // 
            // MainForm_UserControl_SignalStrength
            // 
            MainForm_UserControl_SignalStrength.BackColor = Color.FromArgb(45, 45, 48);
            MainForm_UserControl_SignalStrength.BackgroundImageLayout = ImageLayout.None;
            MainForm_UserControl_SignalStrength.Dock = DockStyle.Fill;
            MainForm_UserControl_SignalStrength.Location = new Point(891, 0);
            MainForm_UserControl_SignalStrength.Margin = new Padding(0);
            MainForm_UserControl_SignalStrength.Name = "MainForm_UserControl_SignalStrength";
            MainForm_UserControl_SignalStrength.Ping = -1;
            MainForm_UserControl_SignalStrength.Size = new Size(53, 31);
            MainForm_UserControl_SignalStrength.Strength = 0;
            MainForm_UserControl_SignalStrength.TabIndex = 0;
            MainForm_UserControl_SignalStrength.TabStop = false;
            // 
            // MainForm_TableLayout
            // 
            MainForm_TableLayout.AutoSize = true;
            MainForm_TableLayout.ColumnCount = 1;
            MainForm_TableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            MainForm_TableLayout.Controls.Add(MainForm_MenuStrip, 0, 0);
            MainForm_TableLayout.Controls.Add(MainForm_SplitContainer_Middle, 0, 1);
            MainForm_TableLayout.Controls.Add(tableLayoutPanel1, 0, 2);
            MainForm_TableLayout.Dock = DockStyle.Fill;
            MainForm_TableLayout.Location = new Point(0, 0);
            MainForm_TableLayout.Margin = new Padding(0);
            MainForm_TableLayout.Name = "MainForm_TableLayout";
            MainForm_TableLayout.RowCount = 3;
            MainForm_TableLayout.RowStyles.Add(new RowStyle());
            MainForm_TableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            MainForm_TableLayout.RowStyles.Add(new RowStyle());
            MainForm_TableLayout.Size = new Size(950, 600);
            MainForm_TableLayout.TabIndex = 94;
            // 
            // MainForm_SplitContainer_Middle
            // 
            MainForm_SplitContainer_Middle.Dock = DockStyle.Fill;
            MainForm_SplitContainer_Middle.Location = new Point(0, 24);
            MainForm_SplitContainer_Middle.Margin = new Padding(0);
            MainForm_SplitContainer_Middle.Name = "MainForm_SplitContainer_Middle";
            // 
            // MainForm_SplitContainer_Middle.Panel1
            // 
            MainForm_SplitContainer_Middle.Panel1.Controls.Add(MainForm_TabControl);
            MainForm_SplitContainer_Middle.Panel1.Margin = new Padding(3);
            // 
            // MainForm_SplitContainer_Middle.Panel2
            // 
            MainForm_SplitContainer_Middle.Panel2.Controls.Add(MainForm_UserControl_QuickButtons);
            MainForm_SplitContainer_Middle.Size = new Size(950, 539);
            MainForm_SplitContainer_Middle.SplitterDistance = 794;
            MainForm_SplitContainer_Middle.TabIndex = 93;
            MainForm_SplitContainer_Middle.TabStop = false;
            // 
            // MainForm_TabControl
            // 
            MainForm_TabControl.Controls.Add(MainForm_TabPage_Inventory);
            MainForm_TabControl.Controls.Add(MainForm_TabPage_Remove);
            MainForm_TabControl.Controls.Add(MainForm_TabPage_Transfer);
            MainForm_TabControl.Dock = DockStyle.Fill;
            MainForm_TabControl.HotTrack = true;
            MainForm_TabControl.Location = new Point(0, 0);
            MainForm_TabControl.Margin = new Padding(0);
            MainForm_TabControl.Multiline = true;
            MainForm_TabControl.Name = "MainForm_TabControl";
            MainForm_TabControl.SelectedIndex = 0;
            MainForm_TabControl.ShowToolTips = true;
            MainForm_TabControl.Size = new Size(794, 539);
            MainForm_TabControl.TabIndex = 91;
            MainForm_TabControl.TabStop = false;
            // 
            // MainForm_TabPage_Inventory
            // 
            MainForm_TabPage_Inventory.Controls.Add(MainForm_UserControl_InventoryTab);
            MainForm_TabPage_Inventory.Controls.Add(MainForm_UserControl_AdvancedInventory);
            MainForm_TabPage_Inventory.Location = new Point(4, 24);
            MainForm_TabPage_Inventory.Margin = new Padding(0);
            MainForm_TabPage_Inventory.Name = "MainForm_TabPage_Inventory";
            MainForm_TabPage_Inventory.Size = new Size(786, 511);
            MainForm_TabPage_Inventory.TabIndex = 0;
            MainForm_TabPage_Inventory.Text = "New";
            MainForm_TabPage_Inventory.UseVisualStyleBackColor = true;
            // 
            // MainForm_UserControl_InventoryTab
            // 
            MainForm_UserControl_InventoryTab.AutoSize = true;
            MainForm_UserControl_InventoryTab.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            MainForm_UserControl_InventoryTab.BackColor = SystemColors.Control;
            MainForm_UserControl_InventoryTab.Dock = DockStyle.Fill;
            MainForm_UserControl_InventoryTab.Location = new Point(0, 0);
            MainForm_UserControl_InventoryTab.Margin = new Padding(3, 4, 3, 4);
            MainForm_UserControl_InventoryTab.Name = "MainForm_UserControl_InventoryTab";
            MainForm_UserControl_InventoryTab.Size = new Size(786, 511);
            MainForm_UserControl_InventoryTab.TabIndex = 0;
            // 
            // MainForm_UserControl_AdvancedInventory
            // 
            MainForm_UserControl_AdvancedInventory.AutoSize = true;
            MainForm_UserControl_AdvancedInventory.Dock = DockStyle.Fill;
            MainForm_UserControl_AdvancedInventory.Location = new Point(0, 0);
            MainForm_UserControl_AdvancedInventory.Margin = new Padding(3, 4, 3, 4);
            MainForm_UserControl_AdvancedInventory.Name = "MainForm_UserControl_AdvancedInventory";
            MainForm_UserControl_AdvancedInventory.Size = new Size(786, 511);
            MainForm_UserControl_AdvancedInventory.TabIndex = 1;
            MainForm_UserControl_AdvancedInventory.Visible = false;
            // 
            // MainForm_TabPage_Remove
            // 
            MainForm_TabPage_Remove.Controls.Add(MainForm_UserControl_RemoveTab);
            MainForm_TabPage_Remove.Controls.Add(MainForm_UserControl_AdvancedRemove);
            MainForm_TabPage_Remove.Location = new Point(4, 24);
            MainForm_TabPage_Remove.Margin = new Padding(0);
            MainForm_TabPage_Remove.Name = "MainForm_TabPage_Remove";
            MainForm_TabPage_Remove.Size = new Size(192, 72);
            MainForm_TabPage_Remove.TabIndex = 1;
            MainForm_TabPage_Remove.Text = "Remove";
            MainForm_TabPage_Remove.UseVisualStyleBackColor = true;
            // 
            // MainForm_UserControl_RemoveTab
            // 
            MainForm_UserControl_RemoveTab.AutoSize = true;
            MainForm_UserControl_RemoveTab.Dock = DockStyle.Fill;
            MainForm_UserControl_RemoveTab.Location = new Point(0, 0);
            MainForm_UserControl_RemoveTab.Margin = new Padding(3, 4, 3, 4);
            MainForm_UserControl_RemoveTab.Name = "MainForm_UserControl_RemoveTab";
            MainForm_UserControl_RemoveTab.Padding = new Padding(3);
            MainForm_UserControl_RemoveTab.Size = new Size(192, 72);
            MainForm_UserControl_RemoveTab.TabIndex = 0;
            // 
            // MainForm_UserControl_AdvancedRemove
            // 
            MainForm_UserControl_AdvancedRemove.AutoSize = true;
            MainForm_UserControl_AdvancedRemove.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            MainForm_UserControl_AdvancedRemove.Dock = DockStyle.Fill;
            MainForm_UserControl_AdvancedRemove.Location = new Point(0, 0);
            MainForm_UserControl_AdvancedRemove.Margin = new Padding(4);
            MainForm_UserControl_AdvancedRemove.Name = "MainForm_UserControl_AdvancedRemove";
            MainForm_UserControl_AdvancedRemove.Size = new Size(192, 72);
            MainForm_UserControl_AdvancedRemove.TabIndex = 1;
            MainForm_UserControl_AdvancedRemove.Visible = false;
            // 
            // MainForm_TabPage_Transfer
            // 
            MainForm_TabPage_Transfer.Controls.Add(MainForm_UserControl_TransferTab);
            MainForm_TabPage_Transfer.Location = new Point(4, 24);
            MainForm_TabPage_Transfer.Margin = new Padding(0);
            MainForm_TabPage_Transfer.Name = "MainForm_TabPage_Transfer";
            MainForm_TabPage_Transfer.Size = new Size(192, 72);
            MainForm_TabPage_Transfer.TabIndex = 2;
            MainForm_TabPage_Transfer.Text = "Transfer";
            MainForm_TabPage_Transfer.UseVisualStyleBackColor = true;
            // 
            // MainForm_UserControl_TransferTab
            // 
            MainForm_UserControl_TransferTab.AutoSize = true;
            MainForm_UserControl_TransferTab.Dock = DockStyle.Fill;
            MainForm_UserControl_TransferTab.Location = new Point(0, 0);
            MainForm_UserControl_TransferTab.Margin = new Padding(0);
            MainForm_UserControl_TransferTab.Name = "MainForm_UserControl_TransferTab";
            MainForm_UserControl_TransferTab.Size = new Size(192, 72);
            MainForm_UserControl_TransferTab.TabIndex = 0;
            // 
            // MainForm_UserControl_QuickButtons
            // 
            MainForm_UserControl_QuickButtons.AutoSize = true;
            MainForm_UserControl_QuickButtons.Dock = DockStyle.Fill;
            MainForm_UserControl_QuickButtons.Location = new Point(0, 0);
            MainForm_UserControl_QuickButtons.Margin = new Padding(0);
            MainForm_UserControl_QuickButtons.Name = "MainForm_UserControl_QuickButtons";
            MainForm_UserControl_QuickButtons.Padding = new Padding(3);
            MainForm_UserControl_QuickButtons.Size = new Size(152, 539);
            MainForm_UserControl_QuickButtons.TabIndex = 0;
            MainForm_UserControl_QuickButtons.TabStop = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(MainForm_StatusStrip, 0, 0);
            tableLayoutPanel1.Controls.Add(MainForm_UserControl_SignalStrength, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 566);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(944, 31);
            tableLayoutPanel1.TabIndex = 94;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(950, 600);
            Controls.Add(MainForm_TableLayout);
            Icon = (Icon)resources.GetObject("$this.Icon");
            IsMdiContainer = true;
            KeyPreview = true;
            MainMenuStrip = MainForm_MenuStrip;
            Name = "MainForm";
            SizeGripStyle = SizeGripStyle.Show;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Manitowoc Tool and Manufacturing | WIP Inventory System |";
            MainForm_MenuStrip.ResumeLayout(false);
            MainForm_MenuStrip.PerformLayout();
            MainForm_StatusStrip.ResumeLayout(false);
            MainForm_StatusStrip.PerformLayout();
            MainForm_TableLayout.ResumeLayout(false);
            MainForm_TableLayout.PerformLayout();
            MainForm_SplitContainer_Middle.Panel1.ResumeLayout(false);
            MainForm_SplitContainer_Middle.Panel2.ResumeLayout(false);
            MainForm_SplitContainer_Middle.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)MainForm_SplitContainer_Middle).EndInit();
            MainForm_SplitContainer_Middle.ResumeLayout(false);
            MainForm_TabControl.ResumeLayout(false);
            MainForm_TabPage_Inventory.ResumeLayout(false);
            MainForm_TabPage_Inventory.PerformLayout();
            MainForm_TabPage_Remove.ResumeLayout(false);
            MainForm_TabPage_Remove.PerformLayout();
            MainForm_TabPage_Transfer.ResumeLayout(false);
            MainForm_TabPage_Transfer.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }
        private TableLayoutPanel tableLayoutPanel1;
        internal ToolTip MainForm_ToolTip;
        private ToolStripMenuItem MainForm_MenuStrip_Development;
        private ToolStripMenuItem MainForm_MenuStrip_Development_Header_Tools;
        private ToolStripMenuItem MainForm_MenuStrip_Development_Dashboard;
        private ToolStripMenuItem MainForm_MenuStrip_Development_Logs;
        private ToolStripMenuItem MainForm_MenuStrip_Development_Database;
        private ToolStripMenuItem MainForm_MenuStrip_Development_SystemInfo;
        private ToolStripMenuItem MainForm_MenuStrip_Development_Feedback;
        private ToolStripSeparator MainForm_MenuStrip_Development_Separator1;
        private ToolStripMenuItem MainForm_MenuStrip_Development_Header_Maintenance;
        private ToolStripMenuItem MainForm_MenuStrip_Help;
        private ToolStripMenuItem MainForm_MenuStrip_Help_ViewHelp;
        private ToolStripMenuItem MainForm_MenuStrip_Help_ViewReleaseNotes;
        private ToolStripMenuItem MainForm_MenuStrip_Help_About;

    }


    #endregion
}
