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
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabMigration = new System.Windows.Forms.TabPage();
            this.grpSingleTable = new System.Windows.Forms.GroupBox();
            this.btnMigrateSelected = new System.Windows.Forms.Button();
            this.checkedListBoxTables = new System.Windows.Forms.CheckedListBox();
            this.grpBulkMigration = new System.Windows.Forms.GroupBox();
            this.chkInventory = new System.Windows.Forms.CheckBox();
            this.chkUsers = new System.Windows.Forms.CheckBox();
            this.chkMasterData = new System.Windows.Forms.CheckBox();
            this.btnMigrateBulk = new System.Windows.Forms.Button();
            this.tabMaintenance = new System.Windows.Forms.TabPage();
            this.grpHealth = new System.Windows.Forms.GroupBox();
            this.btnCheckHealth = new System.Windows.Forms.Button();
            this.btnOptimize = new System.Windows.Forms.Button();
            this.grpCleanup = new System.Windows.Forms.GroupBox();
            this.btnTruncateLogs = new System.Windows.Forms.Button();
            this.btnCleanTestData = new System.Windows.Forms.Button();
            this.grpBackup = new System.Windows.Forms.GroupBox();
            this.btnBackup = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.tabControlMain.SuspendLayout();
            this.tabMigration.SuspendLayout();
            this.grpSingleTable.SuspendLayout();
            this.grpBulkMigration.SuspendLayout();
            this.tabMaintenance.SuspendLayout();
            this.grpHealth.SuspendLayout();
            this.grpCleanup.SuspendLayout();
            this.grpBackup.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabMigration);
            this.tabControlMain.Controls.Add(this.tabMaintenance);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(800, 300);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabMigration
            // 
            this.tabMigration.Controls.Add(this.grpSingleTable);
            this.tabMigration.Controls.Add(this.grpBulkMigration);
            this.tabMigration.Location = new System.Drawing.Point(4, 24);
            this.tabMigration.Name = "tabMigration";
            this.tabMigration.Padding = new System.Windows.Forms.Padding(3);
            this.tabMigration.Size = new System.Drawing.Size(792, 272);
            this.tabMigration.TabIndex = 0;
            this.tabMigration.Text = "Migration";
            this.tabMigration.UseVisualStyleBackColor = true;
            // 
            // grpSingleTable
            // 
            this.grpSingleTable.Controls.Add(this.btnMigrateSelected);
            this.grpSingleTable.Controls.Add(this.checkedListBoxTables);
            this.grpSingleTable.Location = new System.Drawing.Point(300, 20);
            this.grpSingleTable.Name = "grpSingleTable";
            this.grpSingleTable.Size = new System.Drawing.Size(280, 230);
            this.grpSingleTable.TabIndex = 1;
            this.grpSingleTable.TabStop = false;
            this.grpSingleTable.Text = "Single Table Migration";
            // 
            // btnMigrateSelected
            // 
            this.btnMigrateSelected.Location = new System.Drawing.Point(20, 180);
            this.btnMigrateSelected.Name = "btnMigrateSelected";
            this.btnMigrateSelected.Size = new System.Drawing.Size(240, 30);
            this.btnMigrateSelected.TabIndex = 1;
            this.btnMigrateSelected.Text = "Migrate Selected Tables";
            this.btnMigrateSelected.UseVisualStyleBackColor = true;
            this.btnMigrateSelected.Click += new System.EventHandler(this.btnMigrateSelected_Click);
            // 
            // checkedListBoxTables
            // 
            this.checkedListBoxTables.FormattingEnabled = true;
            this.checkedListBoxTables.Location = new System.Drawing.Point(20, 30);
            this.checkedListBoxTables.Name = "checkedListBoxTables";
            this.checkedListBoxTables.Size = new System.Drawing.Size(240, 130);
            this.checkedListBoxTables.TabIndex = 0;
            // 
            // grpBulkMigration
            // 
            this.grpBulkMigration.Controls.Add(this.chkInventory);
            this.grpBulkMigration.Controls.Add(this.chkUsers);
            this.grpBulkMigration.Controls.Add(this.chkMasterData);
            this.grpBulkMigration.Controls.Add(this.btnMigrateBulk);
            this.grpBulkMigration.Location = new System.Drawing.Point(20, 20);
            this.grpBulkMigration.Name = "grpBulkMigration";
            this.grpBulkMigration.Size = new System.Drawing.Size(250, 230);
            this.grpBulkMigration.TabIndex = 0;
            this.grpBulkMigration.TabStop = false;
            this.grpBulkMigration.Text = "Bulk Migration";
            // 
            // chkInventory
            // 
            this.chkInventory.AutoSize = true;
            this.chkInventory.Checked = true;
            this.chkInventory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInventory.Location = new System.Drawing.Point(20, 100);
            this.chkInventory.Name = "chkInventory";
            this.chkInventory.Size = new System.Drawing.Size(180, 19);
            this.chkInventory.TabIndex = 3;
            this.chkInventory.Text = "Inventory && Transactions";
            this.chkInventory.UseVisualStyleBackColor = true;
            // 
            // chkUsers
            // 
            this.chkUsers.AutoSize = true;
            this.chkUsers.Checked = true;
            this.chkUsers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUsers.Location = new System.Drawing.Point(20, 70);
            this.chkUsers.Name = "chkUsers";
            this.chkUsers.Size = new System.Drawing.Size(100, 19);
            this.chkUsers.TabIndex = 2;
            this.chkUsers.Text = "Users && Roles";
            this.chkUsers.UseVisualStyleBackColor = true;
            // 
            // chkMasterData
            // 
            this.chkMasterData.AutoSize = true;
            this.chkMasterData.Checked = true;
            this.chkMasterData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMasterData.Location = new System.Drawing.Point(20, 40);
            this.chkMasterData.Name = "chkMasterData";
            this.chkMasterData.Size = new System.Drawing.Size(90, 19);
            this.chkMasterData.TabIndex = 1;
            this.chkMasterData.Text = "Master Data";
            this.chkMasterData.UseVisualStyleBackColor = true;
            // 
            // btnMigrateBulk
            // 
            this.btnMigrateBulk.Location = new System.Drawing.Point(20, 180);
            this.btnMigrateBulk.Name = "btnMigrateBulk";
            this.btnMigrateBulk.Size = new System.Drawing.Size(200, 30);
            this.btnMigrateBulk.TabIndex = 0;
            this.btnMigrateBulk.Text = "Start Bulk Migration";
            this.btnMigrateBulk.UseVisualStyleBackColor = true;
            this.btnMigrateBulk.Click += new System.EventHandler(this.btnMigrateBulk_Click);
            // 
            // tabMaintenance
            // 
            this.tabMaintenance.Controls.Add(this.grpHealth);
            this.tabMaintenance.Controls.Add(this.grpCleanup);
            this.tabMaintenance.Controls.Add(this.grpBackup);
            this.tabMaintenance.Location = new System.Drawing.Point(4, 24);
            this.tabMaintenance.Name = "tabMaintenance";
            this.tabMaintenance.Padding = new System.Windows.Forms.Padding(3);
            this.tabMaintenance.Size = new System.Drawing.Size(792, 272);
            this.tabMaintenance.TabIndex = 1;
            this.tabMaintenance.Text = "Maintenance";
            this.tabMaintenance.UseVisualStyleBackColor = true;
            // 
            // grpHealth
            // 
            this.grpHealth.Controls.Add(this.btnCheckHealth);
            this.grpHealth.Controls.Add(this.btnOptimize);
            this.grpHealth.Location = new System.Drawing.Point(520, 20);
            this.grpHealth.Name = "grpHealth";
            this.grpHealth.Size = new System.Drawing.Size(240, 230);
            this.grpHealth.TabIndex = 2;
            this.grpHealth.TabStop = false;
            this.grpHealth.Text = "Health && Diagnostics";
            // 
            // btnCheckHealth
            // 
            this.btnCheckHealth.Location = new System.Drawing.Point(20, 80);
            this.btnCheckHealth.Name = "btnCheckHealth";
            this.btnCheckHealth.Size = new System.Drawing.Size(200, 30);
            this.btnCheckHealth.TabIndex = 1;
            this.btnCheckHealth.Text = "Check Table Rows";
            this.btnCheckHealth.UseVisualStyleBackColor = true;
            this.btnCheckHealth.Click += new System.EventHandler(this.btnCheckHealth_Click);
            // 
            // btnOptimize
            // 
            this.btnOptimize.Location = new System.Drawing.Point(20, 40);
            this.btnOptimize.Name = "btnOptimize";
            this.btnOptimize.Size = new System.Drawing.Size(200, 30);
            this.btnOptimize.TabIndex = 0;
            this.btnOptimize.Text = "Optimize Tables";
            this.btnOptimize.UseVisualStyleBackColor = true;
            this.btnOptimize.Click += new System.EventHandler(this.btnOptimize_Click);
            // 
            // grpCleanup
            // 
            this.grpCleanup.Controls.Add(this.btnTruncateLogs);
            this.grpCleanup.Controls.Add(this.btnCleanTestData);
            this.grpCleanup.Location = new System.Drawing.Point(270, 20);
            this.grpCleanup.Name = "grpCleanup";
            this.grpCleanup.Size = new System.Drawing.Size(240, 230);
            this.grpCleanup.TabIndex = 1;
            this.grpCleanup.TabStop = false;
            this.grpCleanup.Text = "Cleanup";
            // 
            // btnTruncateLogs
            // 
            this.btnTruncateLogs.Location = new System.Drawing.Point(20, 80);
            this.btnTruncateLogs.Name = "btnTruncateLogs";
            this.btnTruncateLogs.Size = new System.Drawing.Size(200, 30);
            this.btnTruncateLogs.TabIndex = 1;
            this.btnTruncateLogs.Text = "Clear Error Logs";
            this.btnTruncateLogs.UseVisualStyleBackColor = true;
            this.btnTruncateLogs.Click += new System.EventHandler(this.btnTruncateLogs_Click);
            // 
            // btnCleanTestData
            // 
            this.btnCleanTestData.Location = new System.Drawing.Point(20, 40);
            this.btnCleanTestData.Name = "btnCleanTestData";
            this.btnCleanTestData.Size = new System.Drawing.Size(200, 30);
            this.btnCleanTestData.TabIndex = 0;
            this.btnCleanTestData.Text = "Dump Test Data";
            this.btnCleanTestData.UseVisualStyleBackColor = true;
            this.btnCleanTestData.Click += new System.EventHandler(this.btnCleanTestData_Click);
            // 
            // grpBackup
            // 
            this.grpBackup.Controls.Add(this.btnBackup);
            this.grpBackup.Location = new System.Drawing.Point(20, 20);
            this.grpBackup.Name = "grpBackup";
            this.grpBackup.Size = new System.Drawing.Size(240, 230);
            this.grpBackup.TabIndex = 0;
            this.grpBackup.TabStop = false;
            this.grpBackup.Text = "Backup";
            // 
            // btnBackup
            // 
            this.btnBackup.Location = new System.Drawing.Point(20, 40);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(200, 30);
            this.btnBackup.TabIndex = 0;
            this.btnBackup.Text = "Backup Database";
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtLog.Location = new System.Drawing.Point(0, 300);
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(800, 130);
            this.txtLog.TabIndex = 1;
            this.txtLog.Text = "";
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.Location = new System.Drawing.Point(0, 430);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(800, 20);
            this.progressBar.TabIndex = 2;
            // 
            // Form_DatabaseMaintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.progressBar);
            this.Name = "Form_DatabaseMaintenance";
            this.Text = "Database Maintenance & Migration";
            this.tabControlMain.ResumeLayout(false);
            this.tabMigration.ResumeLayout(false);
            this.grpSingleTable.ResumeLayout(false);
            this.grpBulkMigration.ResumeLayout(false);
            this.grpBulkMigration.PerformLayout();
            this.tabMaintenance.ResumeLayout(false);
            this.grpHealth.ResumeLayout(false);
            this.grpCleanup.ResumeLayout(false);
            this.grpBackup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabMigration;
        private System.Windows.Forms.TabPage tabMaintenance;
        private System.Windows.Forms.GroupBox grpBulkMigration;
        private System.Windows.Forms.Button btnMigrateBulk;
        private System.Windows.Forms.CheckBox chkInventory;
        private System.Windows.Forms.CheckBox chkUsers;
        private System.Windows.Forms.CheckBox chkMasterData;
        private System.Windows.Forms.GroupBox grpSingleTable;
        private System.Windows.Forms.CheckedListBox checkedListBoxTables;
        private System.Windows.Forms.Button btnMigrateSelected;
        private System.Windows.Forms.GroupBox grpBackup;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.GroupBox grpCleanup;
        private System.Windows.Forms.Button btnCleanTestData;
        private System.Windows.Forms.Button btnTruncateLogs;
        private System.Windows.Forms.GroupBox grpHealth;
        private System.Windows.Forms.Button btnOptimize;
        private System.Windows.Forms.Button btnCheckHealth;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}
