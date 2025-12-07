namespace MTM_WIP_Application_Winforms.Forms.Maintenance
{
    partial class Form_DatabaseMaintenance
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_DatabaseMaintenance));
            Form_DatabaseMaintenance_TableLayout_Main = new TableLayoutPanel();
            Form_DatabaseMaintenance_TabControl_Main = new TabControl();
            tabMigration = new TabPage();
            Form_DatabaseMaintenance_TableLayout_Migration = new TableLayoutPanel();
            grpBulkMigration = new GroupBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            chkMasterData = new CheckBox();
            btnMigrateBulk = new Button();
            chkUsers = new CheckBox();
            chkInventory = new CheckBox();
            grpSingleTable = new GroupBox();
            Form_DatabaseMaintenance_TableLayout_Single = new TableLayoutPanel();
            checkedListBoxTables = new CheckedListBox();
            btnMigrateSelected = new Button();
            tabMaintenance = new TabPage();
            Form_DatabaseMaintenance_TableLayout_Maintenance = new TableLayoutPanel();
            grpBackup = new GroupBox();
            Form_DatabaseMaintenance_TableLayoutPanel_Backup = new TableLayoutPanel();
            btnBackup = new Button();
            grpCleanup = new GroupBox();
            Form_DatabaseMaintenance_TableLayoutPanel_Cleanup = new TableLayoutPanel();
            btnArchiveLogs = new Button();
            btnCleanTestData = new Button();
            btnFactoryReset = new Button();
            btnTruncateLogs = new Button();
            grpHealth = new GroupBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnSchema = new Button();
            btnIntegrity = new Button();
            btnTableSizes = new Button();
            btnConnections = new Button();
            btnCheckHealth = new Button();
            btnOptimize = new Button();
            grpVisual = new GroupBox();
            tableLayoutPanelVisual = new TableLayoutPanel();
            btnUpdateUserShifts = new Button();
            btnUpdateUserNames = new Button();
            txtLog = new RichTextBox();
            progressBar = new ProgressBar();
            Form_DatabaseMaintenance_TableLayout_Main.SuspendLayout();
            Form_DatabaseMaintenance_TabControl_Main.SuspendLayout();
            tabMigration.SuspendLayout();
            Form_DatabaseMaintenance_TableLayout_Migration.SuspendLayout();
            grpBulkMigration.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            grpSingleTable.SuspendLayout();
            Form_DatabaseMaintenance_TableLayout_Single.SuspendLayout();
            tabMaintenance.SuspendLayout();
            Form_DatabaseMaintenance_TableLayout_Maintenance.SuspendLayout();
            grpBackup.SuspendLayout();
            Form_DatabaseMaintenance_TableLayoutPanel_Backup.SuspendLayout();
            grpCleanup.SuspendLayout();
            Form_DatabaseMaintenance_TableLayoutPanel_Cleanup.SuspendLayout();
            grpHealth.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            grpVisual.SuspendLayout();
            tableLayoutPanelVisual.SuspendLayout();
            SuspendLayout();
            // 
            // Form_DatabaseMaintenance_TableLayout_Main
            // 
            Form_DatabaseMaintenance_TableLayout_Main.ColumnCount = 1;
            Form_DatabaseMaintenance_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Form_DatabaseMaintenance_TableLayout_Main.Controls.Add(Form_DatabaseMaintenance_TabControl_Main, 0, 0);
            Form_DatabaseMaintenance_TableLayout_Main.Controls.Add(txtLog, 0, 1);
            Form_DatabaseMaintenance_TableLayout_Main.Controls.Add(progressBar, 0, 2);
            Form_DatabaseMaintenance_TableLayout_Main.Dock = DockStyle.Fill;
            Form_DatabaseMaintenance_TableLayout_Main.Location = new Point(0, 0);
            Form_DatabaseMaintenance_TableLayout_Main.Name = "Form_DatabaseMaintenance_TableLayout_Main";
            Form_DatabaseMaintenance_TableLayout_Main.RowCount = 3;
            Form_DatabaseMaintenance_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            Form_DatabaseMaintenance_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            Form_DatabaseMaintenance_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            Form_DatabaseMaintenance_TableLayout_Main.Size = new Size(1003, 600);
            Form_DatabaseMaintenance_TableLayout_Main.TabIndex = 0;
            // 
            // Form_DatabaseMaintenance_TabControl_Main
            // 
            Form_DatabaseMaintenance_TabControl_Main.Controls.Add(tabMigration);
            Form_DatabaseMaintenance_TabControl_Main.Controls.Add(tabMaintenance);
            Form_DatabaseMaintenance_TabControl_Main.Dock = DockStyle.Fill;
            Form_DatabaseMaintenance_TabControl_Main.Location = new Point(3, 3);
            Form_DatabaseMaintenance_TabControl_Main.Name = "Form_DatabaseMaintenance_TabControl_Main";
            Form_DatabaseMaintenance_TabControl_Main.SelectedIndex = 0;
            Form_DatabaseMaintenance_TabControl_Main.Size = new Size(997, 339);
            Form_DatabaseMaintenance_TabControl_Main.TabIndex = 0;
            // 
            // tabMigration
            // 
            tabMigration.Controls.Add(Form_DatabaseMaintenance_TableLayout_Migration);
            tabMigration.Location = new Point(4, 24);
            tabMigration.Name = "tabMigration";
            tabMigration.Padding = new Padding(3);
            tabMigration.Size = new Size(989, 311);
            tabMigration.TabIndex = 0;
            tabMigration.Text = "Migration";
            tabMigration.UseVisualStyleBackColor = true;
            // 
            // Form_DatabaseMaintenance_TableLayout_Migration
            // 
            Form_DatabaseMaintenance_TableLayout_Migration.ColumnCount = 2;
            Form_DatabaseMaintenance_TableLayout_Migration.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            Form_DatabaseMaintenance_TableLayout_Migration.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            Form_DatabaseMaintenance_TableLayout_Migration.Controls.Add(grpBulkMigration, 0, 0);
            Form_DatabaseMaintenance_TableLayout_Migration.Controls.Add(grpSingleTable, 1, 0);
            Form_DatabaseMaintenance_TableLayout_Migration.Dock = DockStyle.Fill;
            Form_DatabaseMaintenance_TableLayout_Migration.Location = new Point(3, 3);
            Form_DatabaseMaintenance_TableLayout_Migration.Name = "Form_DatabaseMaintenance_TableLayout_Migration";
            Form_DatabaseMaintenance_TableLayout_Migration.RowCount = 1;
            Form_DatabaseMaintenance_TableLayout_Migration.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Form_DatabaseMaintenance_TableLayout_Migration.Size = new Size(983, 305);
            Form_DatabaseMaintenance_TableLayout_Migration.TabIndex = 0;
            // 
            // grpBulkMigration
            // 
            grpBulkMigration.Controls.Add(tableLayoutPanel2);
            grpBulkMigration.Dock = DockStyle.Fill;
            grpBulkMigration.Location = new Point(3, 3);
            grpBulkMigration.Name = "grpBulkMigration";
            grpBulkMigration.Size = new Size(387, 299);
            grpBulkMigration.TabIndex = 0;
            grpBulkMigration.TabStop = false;
            grpBulkMigration.Text = "Bulk Migration";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33333F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            tableLayoutPanel2.Controls.Add(chkMasterData, 0, 0);
            tableLayoutPanel2.Controls.Add(btnMigrateBulk, 0, 1);
            tableLayoutPanel2.Controls.Add(chkUsers, 1, 0);
            tableLayoutPanel2.Controls.Add(chkInventory, 2, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 19);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(381, 277);
            tableLayoutPanel2.TabIndex = 4;
            // 
            // chkMasterData
            // 
            chkMasterData.AutoSize = true;
            chkMasterData.CheckAlign = ContentAlignment.BottomCenter;
            chkMasterData.Dock = DockStyle.Fill;
            chkMasterData.Location = new Point(3, 3);
            chkMasterData.Name = "chkMasterData";
            chkMasterData.Size = new Size(120, 33);
            chkMasterData.TabIndex = 0;
            chkMasterData.Text = "Master Data";
            chkMasterData.TextAlign = ContentAlignment.TopCenter;
            chkMasterData.UseVisualStyleBackColor = true;
            // 
            // btnMigrateBulk
            // 
            btnMigrateBulk.AutoSize = true;
            tableLayoutPanel2.SetColumnSpan(btnMigrateBulk, 3);
            btnMigrateBulk.Dock = DockStyle.Fill;
            btnMigrateBulk.Location = new Point(40, 69);
            btnMigrateBulk.Margin = new Padding(40, 30, 40, 30);
            btnMigrateBulk.Name = "btnMigrateBulk";
            btnMigrateBulk.Size = new Size(301, 178);
            btnMigrateBulk.TabIndex = 3;
            btnMigrateBulk.Text = "Start Bulk Migration";
            btnMigrateBulk.UseVisualStyleBackColor = true;
            btnMigrateBulk.Click += btnMigrateBulk_Click;
            // 
            // chkUsers
            // 
            chkUsers.AutoSize = true;
            chkUsers.CheckAlign = ContentAlignment.BottomCenter;
            chkUsers.Dock = DockStyle.Fill;
            chkUsers.Location = new Point(129, 3);
            chkUsers.Name = "chkUsers";
            chkUsers.Size = new Size(121, 33);
            chkUsers.TabIndex = 1;
            chkUsers.Text = "Users && Roles";
            chkUsers.TextAlign = ContentAlignment.TopCenter;
            chkUsers.UseVisualStyleBackColor = true;
            // 
            // chkInventory
            // 
            chkInventory.AutoSize = true;
            chkInventory.CheckAlign = ContentAlignment.BottomCenter;
            chkInventory.Dock = DockStyle.Fill;
            chkInventory.Location = new Point(256, 3);
            chkInventory.Name = "chkInventory";
            chkInventory.Size = new Size(122, 33);
            chkInventory.TabIndex = 2;
            chkInventory.Text = "Inventory";
            chkInventory.TextAlign = ContentAlignment.TopCenter;
            chkInventory.UseVisualStyleBackColor = true;
            // 
            // grpSingleTable
            // 
            grpSingleTable.Controls.Add(Form_DatabaseMaintenance_TableLayout_Single);
            grpSingleTable.Dock = DockStyle.Fill;
            grpSingleTable.Location = new Point(396, 3);
            grpSingleTable.Name = "grpSingleTable";
            grpSingleTable.Size = new Size(584, 299);
            grpSingleTable.TabIndex = 1;
            grpSingleTable.TabStop = false;
            grpSingleTable.Text = "Single Table Migration";
            // 
            // Form_DatabaseMaintenance_TableLayout_Single
            // 
            Form_DatabaseMaintenance_TableLayout_Single.ColumnCount = 1;
            Form_DatabaseMaintenance_TableLayout_Single.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Form_DatabaseMaintenance_TableLayout_Single.Controls.Add(checkedListBoxTables, 0, 0);
            Form_DatabaseMaintenance_TableLayout_Single.Controls.Add(btnMigrateSelected, 0, 1);
            Form_DatabaseMaintenance_TableLayout_Single.Dock = DockStyle.Fill;
            Form_DatabaseMaintenance_TableLayout_Single.Location = new Point(3, 19);
            Form_DatabaseMaintenance_TableLayout_Single.Name = "Form_DatabaseMaintenance_TableLayout_Single";
            Form_DatabaseMaintenance_TableLayout_Single.RowCount = 2;
            Form_DatabaseMaintenance_TableLayout_Single.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Form_DatabaseMaintenance_TableLayout_Single.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            Form_DatabaseMaintenance_TableLayout_Single.Size = new Size(578, 277);
            Form_DatabaseMaintenance_TableLayout_Single.TabIndex = 0;
            // 
            // checkedListBoxTables
            // 
            checkedListBoxTables.Dock = DockStyle.Fill;
            checkedListBoxTables.FormattingEnabled = true;
            checkedListBoxTables.Location = new Point(3, 3);
            checkedListBoxTables.Name = "checkedListBoxTables";
            checkedListBoxTables.Size = new Size(572, 226);
            checkedListBoxTables.TabIndex = 0;
            // 
            // btnMigrateSelected
            // 
            btnMigrateSelected.Dock = DockStyle.Fill;
            btnMigrateSelected.Location = new Point(16, 235);
            btnMigrateSelected.Margin = new Padding(16, 3, 16, 3);
            btnMigrateSelected.Name = "btnMigrateSelected";
            btnMigrateSelected.Size = new Size(546, 39);
            btnMigrateSelected.TabIndex = 1;
            btnMigrateSelected.Text = "Migrate Selected Tables";
            btnMigrateSelected.UseVisualStyleBackColor = true;
            btnMigrateSelected.Click += btnMigrateSelected_Click;
            // 
            // tabMaintenance
            // 
            tabMaintenance.Controls.Add(Form_DatabaseMaintenance_TableLayout_Maintenance);
            tabMaintenance.Location = new Point(4, 24);
            tabMaintenance.Name = "tabMaintenance";
            tabMaintenance.Padding = new Padding(3);
            tabMaintenance.Size = new Size(989, 311);
            tabMaintenance.TabIndex = 1;
            tabMaintenance.Text = "Maintenance";
            tabMaintenance.UseVisualStyleBackColor = true;
            // 
            // Form_DatabaseMaintenance_TableLayout_Maintenance
            // 
            Form_DatabaseMaintenance_TableLayout_Maintenance.AutoSize = true;
            Form_DatabaseMaintenance_TableLayout_Maintenance.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Form_DatabaseMaintenance_TableLayout_Maintenance.ColumnCount = 3;
            Form_DatabaseMaintenance_TableLayout_Maintenance.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            Form_DatabaseMaintenance_TableLayout_Maintenance.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            Form_DatabaseMaintenance_TableLayout_Maintenance.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
            Form_DatabaseMaintenance_TableLayout_Maintenance.Controls.Add(grpBackup, 0, 0);
            Form_DatabaseMaintenance_TableLayout_Maintenance.Controls.Add(grpCleanup, 1, 0);
            Form_DatabaseMaintenance_TableLayout_Maintenance.Controls.Add(grpHealth, 2, 0);
            Form_DatabaseMaintenance_TableLayout_Maintenance.Controls.Add(grpVisual, 0, 1);
            Form_DatabaseMaintenance_TableLayout_Maintenance.Dock = DockStyle.Fill;
            Form_DatabaseMaintenance_TableLayout_Maintenance.Location = new Point(3, 3);
            Form_DatabaseMaintenance_TableLayout_Maintenance.Name = "Form_DatabaseMaintenance_TableLayout_Maintenance";
            Form_DatabaseMaintenance_TableLayout_Maintenance.RowCount = 2;
            Form_DatabaseMaintenance_TableLayout_Maintenance.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            Form_DatabaseMaintenance_TableLayout_Maintenance.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            Form_DatabaseMaintenance_TableLayout_Maintenance.Size = new Size(983, 305);
            Form_DatabaseMaintenance_TableLayout_Maintenance.TabIndex = 0;
            // 
            // grpBackup
            // 
            grpBackup.AutoSize = true;
            grpBackup.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            grpBackup.Controls.Add(Form_DatabaseMaintenance_TableLayoutPanel_Backup);
            grpBackup.Dock = DockStyle.Fill;
            grpBackup.Location = new Point(3, 3);
            grpBackup.Name = "grpBackup";
            grpBackup.Size = new Size(321, 207);
            grpBackup.TabIndex = 0;
            grpBackup.TabStop = false;
            grpBackup.Text = "Backup";
            // 
            // Form_DatabaseMaintenance_TableLayoutPanel_Backup
            // 
            Form_DatabaseMaintenance_TableLayoutPanel_Backup.AutoSize = true;
            Form_DatabaseMaintenance_TableLayoutPanel_Backup.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Form_DatabaseMaintenance_TableLayoutPanel_Backup.ColumnCount = 1;
            Form_DatabaseMaintenance_TableLayoutPanel_Backup.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Form_DatabaseMaintenance_TableLayoutPanel_Backup.Controls.Add(btnBackup, 0, 0);
            Form_DatabaseMaintenance_TableLayoutPanel_Backup.Dock = DockStyle.Fill;
            Form_DatabaseMaintenance_TableLayoutPanel_Backup.Location = new Point(3, 19);
            Form_DatabaseMaintenance_TableLayoutPanel_Backup.Name = "Form_DatabaseMaintenance_TableLayoutPanel_Backup";
            Form_DatabaseMaintenance_TableLayoutPanel_Backup.RowCount = 1;
            Form_DatabaseMaintenance_TableLayoutPanel_Backup.RowStyles.Add(new RowStyle());
            Form_DatabaseMaintenance_TableLayoutPanel_Backup.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            Form_DatabaseMaintenance_TableLayoutPanel_Backup.Size = new Size(315, 185);
            Form_DatabaseMaintenance_TableLayoutPanel_Backup.TabIndex = 1;
            // 
            // btnBackup
            // 
            btnBackup.AutoSize = true;
            btnBackup.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnBackup.Dock = DockStyle.Fill;
            btnBackup.Location = new Point(40, 40);
            btnBackup.Margin = new Padding(40);
            btnBackup.Name = "btnBackup";
            btnBackup.Size = new Size(235, 105);
            btnBackup.TabIndex = 0;
            btnBackup.Text = "Backup Database";
            btnBackup.UseVisualStyleBackColor = true;
            btnBackup.Click += btnBackup_Click;
            // 
            // grpCleanup
            // 
            grpCleanup.AutoSize = true;
            grpCleanup.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            grpCleanup.Controls.Add(Form_DatabaseMaintenance_TableLayoutPanel_Cleanup);
            grpCleanup.Dock = DockStyle.Fill;
            grpCleanup.Location = new Point(330, 3);
            grpCleanup.Name = "grpCleanup";
            grpCleanup.Size = new Size(321, 207);
            grpCleanup.TabIndex = 1;
            grpCleanup.TabStop = false;
            grpCleanup.Text = "Cleanup";
            // 
            // Form_DatabaseMaintenance_TableLayoutPanel_Cleanup
            // 
            Form_DatabaseMaintenance_TableLayoutPanel_Cleanup.AutoSize = true;
            Form_DatabaseMaintenance_TableLayoutPanel_Cleanup.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Form_DatabaseMaintenance_TableLayoutPanel_Cleanup.ColumnCount = 2;
            Form_DatabaseMaintenance_TableLayoutPanel_Cleanup.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            Form_DatabaseMaintenance_TableLayoutPanel_Cleanup.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            Form_DatabaseMaintenance_TableLayoutPanel_Cleanup.Controls.Add(btnArchiveLogs, 1, 1);
            Form_DatabaseMaintenance_TableLayoutPanel_Cleanup.Controls.Add(btnCleanTestData, 0, 0);
            Form_DatabaseMaintenance_TableLayoutPanel_Cleanup.Controls.Add(btnFactoryReset, 0, 2);
            Form_DatabaseMaintenance_TableLayoutPanel_Cleanup.Controls.Add(btnTruncateLogs, 0, 1);
            Form_DatabaseMaintenance_TableLayoutPanel_Cleanup.Dock = DockStyle.Fill;
            Form_DatabaseMaintenance_TableLayoutPanel_Cleanup.Location = new Point(3, 19);
            Form_DatabaseMaintenance_TableLayoutPanel_Cleanup.Name = "Form_DatabaseMaintenance_TableLayoutPanel_Cleanup";
            Form_DatabaseMaintenance_TableLayoutPanel_Cleanup.RowCount = 3;
            Form_DatabaseMaintenance_TableLayoutPanel_Cleanup.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            Form_DatabaseMaintenance_TableLayoutPanel_Cleanup.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            Form_DatabaseMaintenance_TableLayoutPanel_Cleanup.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            Form_DatabaseMaintenance_TableLayoutPanel_Cleanup.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            Form_DatabaseMaintenance_TableLayoutPanel_Cleanup.Size = new Size(315, 185);
            Form_DatabaseMaintenance_TableLayoutPanel_Cleanup.TabIndex = 4;
            // 
            // btnArchiveLogs
            // 
            btnArchiveLogs.AutoSize = true;
            btnArchiveLogs.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnArchiveLogs.Dock = DockStyle.Fill;
            btnArchiveLogs.Location = new Point(177, 81);
            btnArchiveLogs.Margin = new Padding(20);
            btnArchiveLogs.Name = "btnArchiveLogs";
            btnArchiveLogs.Size = new Size(118, 21);
            btnArchiveLogs.TabIndex = 2;
            btnArchiveLogs.Text = "Archive && Clear Logs";
            btnArchiveLogs.UseVisualStyleBackColor = true;
            btnArchiveLogs.Click += btnArchiveLogs_Click;
            // 
            // btnCleanTestData
            // 
            btnCleanTestData.AutoSize = true;
            btnCleanTestData.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Form_DatabaseMaintenance_TableLayoutPanel_Cleanup.SetColumnSpan(btnCleanTestData, 2);
            btnCleanTestData.Dock = DockStyle.Fill;
            btnCleanTestData.Location = new Point(20, 20);
            btnCleanTestData.Margin = new Padding(20);
            btnCleanTestData.Name = "btnCleanTestData";
            btnCleanTestData.Size = new Size(275, 21);
            btnCleanTestData.TabIndex = 0;
            btnCleanTestData.Text = "Dump Test Data";
            btnCleanTestData.UseVisualStyleBackColor = true;
            btnCleanTestData.Click += btnCleanTestData_Click;
            // 
            // btnFactoryReset
            // 
            btnFactoryReset.AutoSize = true;
            btnFactoryReset.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnFactoryReset.BackColor = Color.MistyRose;
            Form_DatabaseMaintenance_TableLayoutPanel_Cleanup.SetColumnSpan(btnFactoryReset, 2);
            btnFactoryReset.Dock = DockStyle.Fill;
            btnFactoryReset.Location = new Point(20, 142);
            btnFactoryReset.Margin = new Padding(20);
            btnFactoryReset.Name = "btnFactoryReset";
            btnFactoryReset.Size = new Size(275, 23);
            btnFactoryReset.TabIndex = 3;
            btnFactoryReset.Text = "Factory Reset (Wipe Inventory)";
            btnFactoryReset.UseVisualStyleBackColor = false;
            btnFactoryReset.Click += btnFactoryReset_Click;
            // 
            // btnTruncateLogs
            // 
            btnTruncateLogs.AutoSize = true;
            btnTruncateLogs.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnTruncateLogs.Dock = DockStyle.Fill;
            btnTruncateLogs.Location = new Point(20, 81);
            btnTruncateLogs.Margin = new Padding(20);
            btnTruncateLogs.Name = "btnTruncateLogs";
            btnTruncateLogs.Size = new Size(117, 21);
            btnTruncateLogs.TabIndex = 1;
            btnTruncateLogs.Text = "Clear Error Logs";
            btnTruncateLogs.UseVisualStyleBackColor = true;
            btnTruncateLogs.Click += btnTruncateLogs_Click;
            // 
            // grpHealth
            // 
            grpHealth.AutoSize = true;
            grpHealth.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            grpHealth.Controls.Add(tableLayoutPanel1);
            grpHealth.Dock = DockStyle.Fill;
            grpHealth.Location = new Point(657, 3);
            grpHealth.Name = "grpHealth";
            grpHealth.Size = new Size(323, 207);
            grpHealth.TabIndex = 2;
            grpHealth.TabStop = false;
            grpHealth.Text = "Health && Diagnostics";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(btnSchema, 1, 2);
            tableLayoutPanel1.Controls.Add(btnIntegrity, 1, 1);
            tableLayoutPanel1.Controls.Add(btnTableSizes, 0, 2);
            tableLayoutPanel1.Controls.Add(btnConnections, 1, 0);
            tableLayoutPanel1.Controls.Add(btnCheckHealth, 0, 1);
            tableLayoutPanel1.Controls.Add(btnOptimize, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 19);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Size = new Size(317, 185);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // btnSchema
            // 
            btnSchema.AutoSize = true;
            btnSchema.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnSchema.Dock = DockStyle.Fill;
            btnSchema.Location = new Point(178, 142);
            btnSchema.Margin = new Padding(20);
            btnSchema.Name = "btnSchema";
            btnSchema.Size = new Size(119, 23);
            btnSchema.TabIndex = 5;
            btnSchema.Text = "Validate Schema";
            btnSchema.UseVisualStyleBackColor = true;
            btnSchema.Click += btnSchema_Click;
            // 
            // btnIntegrity
            // 
            btnIntegrity.AutoSize = true;
            btnIntegrity.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnIntegrity.Dock = DockStyle.Fill;
            btnIntegrity.Location = new Point(178, 81);
            btnIntegrity.Margin = new Padding(20);
            btnIntegrity.Name = "btnIntegrity";
            btnIntegrity.Size = new Size(119, 21);
            btnIntegrity.TabIndex = 3;
            btnIntegrity.Text = "Verify Integrity";
            btnIntegrity.UseVisualStyleBackColor = true;
            btnIntegrity.Click += btnIntegrity_Click;
            // 
            // btnTableSizes
            // 
            btnTableSizes.AutoSize = true;
            btnTableSizes.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnTableSizes.Dock = DockStyle.Fill;
            btnTableSizes.Location = new Point(20, 142);
            btnTableSizes.Margin = new Padding(20);
            btnTableSizes.Name = "btnTableSizes";
            btnTableSizes.Size = new Size(118, 23);
            btnTableSizes.TabIndex = 2;
            btnTableSizes.Text = "Analyze Table Sizes";
            btnTableSizes.UseVisualStyleBackColor = true;
            btnTableSizes.Click += btnTableSizes_Click;
            // 
            // btnConnections
            // 
            btnConnections.AutoSize = true;
            btnConnections.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnConnections.Dock = DockStyle.Fill;
            btnConnections.Location = new Point(178, 20);
            btnConnections.Margin = new Padding(20);
            btnConnections.Name = "btnConnections";
            btnConnections.Size = new Size(119, 21);
            btnConnections.TabIndex = 4;
            btnConnections.Text = "Monitor Connections";
            btnConnections.UseVisualStyleBackColor = true;
            btnConnections.Click += btnConnections_Click;
            // 
            // btnCheckHealth
            // 
            btnCheckHealth.AutoSize = true;
            btnCheckHealth.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnCheckHealth.Dock = DockStyle.Fill;
            btnCheckHealth.Location = new Point(20, 81);
            btnCheckHealth.Margin = new Padding(20);
            btnCheckHealth.Name = "btnCheckHealth";
            btnCheckHealth.Size = new Size(118, 21);
            btnCheckHealth.TabIndex = 1;
            btnCheckHealth.Text = "Check Table Rows";
            btnCheckHealth.UseVisualStyleBackColor = true;
            btnCheckHealth.Click += btnCheckHealth_Click;
            // 
            // btnOptimize
            // 
            btnOptimize.AutoSize = true;
            btnOptimize.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnOptimize.Dock = DockStyle.Fill;
            btnOptimize.Location = new Point(20, 20);
            btnOptimize.Margin = new Padding(20);
            btnOptimize.Name = "btnOptimize";
            btnOptimize.Size = new Size(118, 21);
            btnOptimize.TabIndex = 0;
            btnOptimize.Text = "Optimize Tables";
            btnOptimize.UseVisualStyleBackColor = true;
            btnOptimize.Click += btnOptimize_Click;
            // 
            // grpVisual
            // 
            Form_DatabaseMaintenance_TableLayout_Maintenance.SetColumnSpan(grpVisual, 3);
            grpVisual.Controls.Add(tableLayoutPanelVisual);
            grpVisual.Dock = DockStyle.Fill;
            grpVisual.Location = new Point(3, 216);
            grpVisual.Name = "grpVisual";
            grpVisual.Size = new Size(977, 86);
            grpVisual.TabIndex = 3;
            grpVisual.TabStop = false;
            grpVisual.Text = "Infor Visual Integration";
            // 
            // tableLayoutPanelVisual
            // 
            tableLayoutPanelVisual.ColumnCount = 2;
            tableLayoutPanelVisual.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelVisual.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelVisual.Controls.Add(btnUpdateUserShifts, 0, 0);
            tableLayoutPanelVisual.Controls.Add(btnUpdateUserNames, 1, 0);
            tableLayoutPanelVisual.Dock = DockStyle.Fill;
            tableLayoutPanelVisual.Location = new Point(3, 19);
            tableLayoutPanelVisual.Name = "tableLayoutPanelVisual";
            tableLayoutPanelVisual.RowCount = 1;
            tableLayoutPanelVisual.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelVisual.Size = new Size(971, 64);
            tableLayoutPanelVisual.TabIndex = 0;
            // 
            // btnUpdateUserShifts
            // 
            btnUpdateUserShifts.Dock = DockStyle.Fill;
            btnUpdateUserShifts.Location = new Point(20, 10);
            btnUpdateUserShifts.Margin = new Padding(20, 10, 20, 10);
            btnUpdateUserShifts.Name = "btnUpdateUserShifts";
            btnUpdateUserShifts.Size = new Size(445, 44);
            btnUpdateUserShifts.TabIndex = 0;
            btnUpdateUserShifts.Text = "Update User Shifts (Last 50 Trans)";
            btnUpdateUserShifts.UseVisualStyleBackColor = true;
            btnUpdateUserShifts.Click += btnUpdateUserShifts_Click;
            // 
            // btnUpdateUserNames
            // 
            btnUpdateUserNames.Dock = DockStyle.Fill;
            btnUpdateUserNames.Location = new Point(505, 10);
            btnUpdateUserNames.Margin = new Padding(20, 10, 20, 10);
            btnUpdateUserNames.Name = "btnUpdateUserNames";
            btnUpdateUserNames.Size = new Size(446, 44);
            btnUpdateUserNames.TabIndex = 1;
            btnUpdateUserNames.Text = "Update User Names (From Visual)";
            btnUpdateUserNames.UseVisualStyleBackColor = true;
            btnUpdateUserNames.Click += btnUpdateUserNames_Click;
            // 
            // txtLog
            // 
            txtLog.Dock = DockStyle.Fill;
            txtLog.Font = new Font("Consolas", 9F);
            txtLog.Location = new Point(3, 348);
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.Size = new Size(997, 224);
            txtLog.TabIndex = 1;
            txtLog.Text = "";
            // 
            // progressBar
            // 
            progressBar.Dock = DockStyle.Fill;
            progressBar.Location = new Point(3, 578);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(997, 19);
            progressBar.TabIndex = 2;
            // 
            // Form_DatabaseMaintenance
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1003, 600);
            Controls.Add(Form_DatabaseMaintenance_TableLayout_Main);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form_DatabaseMaintenance";
            Text = "Database Maintenance & Migration";
            Form_DatabaseMaintenance_TableLayout_Main.ResumeLayout(false);
            Form_DatabaseMaintenance_TabControl_Main.ResumeLayout(false);
            tabMigration.ResumeLayout(false);
            Form_DatabaseMaintenance_TableLayout_Migration.ResumeLayout(false);
            grpBulkMigration.ResumeLayout(false);
            grpBulkMigration.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            grpSingleTable.ResumeLayout(false);
            Form_DatabaseMaintenance_TableLayout_Single.ResumeLayout(false);
            tabMaintenance.ResumeLayout(false);
            tabMaintenance.PerformLayout();
            Form_DatabaseMaintenance_TableLayout_Maintenance.ResumeLayout(false);
            Form_DatabaseMaintenance_TableLayout_Maintenance.PerformLayout();
            grpBackup.ResumeLayout(false);
            grpBackup.PerformLayout();
            Form_DatabaseMaintenance_TableLayoutPanel_Backup.ResumeLayout(false);
            Form_DatabaseMaintenance_TableLayoutPanel_Backup.PerformLayout();
            grpCleanup.ResumeLayout(false);
            grpCleanup.PerformLayout();
            Form_DatabaseMaintenance_TableLayoutPanel_Cleanup.ResumeLayout(false);
            Form_DatabaseMaintenance_TableLayoutPanel_Cleanup.PerformLayout();
            grpHealth.ResumeLayout(false);
            grpHealth.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            grpVisual.ResumeLayout(false);
            tableLayoutPanelVisual.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel Form_DatabaseMaintenance_TableLayout_Main;
        private System.Windows.Forms.TabControl Form_DatabaseMaintenance_TabControl_Main;
        private System.Windows.Forms.TabPage tabMigration;
        private System.Windows.Forms.TableLayoutPanel Form_DatabaseMaintenance_TableLayout_Migration;
        private System.Windows.Forms.GroupBox grpBulkMigration;
        private System.Windows.Forms.CheckBox chkMasterData;
        private System.Windows.Forms.CheckBox chkUsers;
        private System.Windows.Forms.CheckBox chkInventory;
        private System.Windows.Forms.Button btnMigrateBulk;
        private System.Windows.Forms.GroupBox grpSingleTable;
        private System.Windows.Forms.TableLayoutPanel Form_DatabaseMaintenance_TableLayout_Single;
        private System.Windows.Forms.CheckedListBox checkedListBoxTables;
        private System.Windows.Forms.Button btnMigrateSelected;
        private System.Windows.Forms.TabPage tabMaintenance;
        private System.Windows.Forms.TableLayoutPanel Form_DatabaseMaintenance_TableLayout_Maintenance;
        private System.Windows.Forms.GroupBox grpBackup;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.GroupBox grpCleanup;
        private System.Windows.Forms.Button btnCleanTestData;
        private System.Windows.Forms.Button btnTruncateLogs;
        private System.Windows.Forms.Button btnArchiveLogs;
        private System.Windows.Forms.Button btnFactoryReset;
        private System.Windows.Forms.GroupBox grpHealth;
        private System.Windows.Forms.Button btnOptimize;
        private System.Windows.Forms.Button btnCheckHealth;
        private System.Windows.Forms.Button btnTableSizes;
        private System.Windows.Forms.Button btnIntegrity;
        private System.Windows.Forms.Button btnConnections;
        private System.Windows.Forms.Button btnSchema;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.ProgressBar progressBar;
        private TableLayoutPanel Form_DatabaseMaintenance_TableLayoutPanel_Backup;
        private TableLayoutPanel Form_DatabaseMaintenance_TableLayoutPanel_Cleanup;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private GroupBox grpVisual;
        private TableLayoutPanel tableLayoutPanelVisual;
        private Button btnUpdateUserShifts;
        private Button btnUpdateUserNames;
    }
}
