namespace MTM_WIP_Application_Winforms.Forms.WIPAppAnalytics
{
    partial class Form_WIPUserAnalytics
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_WIPUserAnalytics));
            Form_Analytics_TableLayout_Main = new TableLayoutPanel();
            Form_Analytics_TabControl_Main = new TabControl();
            tabPerformance = new TabPage();
            Form_Analytics_TableLayout_Performance = new TableLayoutPanel();
            grpPerformanceFilters = new GroupBox();
            Form_Analytics_TableLayout_PerfFilters = new TableLayoutPanel();
            lblDateFrom = new Label();
            dtpDateFrom = new DateTimePicker();
            lblDateTo = new Label();
            dtpDateTo = new DateTimePicker();
            btnRefreshPerformance = new Button();
            grpPerformanceInfo = new GroupBox();
            txtPerformanceInfo = new RichTextBox();
            gridPerformance = new DataGridView();
            tabQuality = new TabPage();
            Form_Analytics_TableLayout_Quality = new TableLayoutPanel();
            grpQualityFilters = new GroupBox();
            Form_Analytics_TableLayout_QualityFilters = new TableLayoutPanel();
            btnRefreshQuality = new Button();
            grpQualityInfo = new GroupBox();
            txtQualityInfo = new RichTextBox();
            gridQuality = new DataGridView();
            tabUserDetail = new TabPage();
            Form_Analytics_TableLayout_UserDetail = new TableLayoutPanel();
            grpUserSelection = new GroupBox();
            Form_Analytics_TableLayout_UserSelect = new TableLayoutPanel();
            lblUser = new Label();
            comboUsers = new ComboBox();
            btnLoadUser = new Button();
            grpUserDetailInfo = new GroupBox();
            txtUserDetailInfo = new RichTextBox();
            gridUserHistory = new DataGridView();
            tabGlossary = new TabPage();
            txtGlossary = new RichTextBox();
            lblStatus = new Label();
            progressBar = new ProgressBar();
            Form_Analytics_TableLayout_Main.SuspendLayout();
            Form_Analytics_TabControl_Main.SuspendLayout();
            tabPerformance.SuspendLayout();
            Form_Analytics_TableLayout_Performance.SuspendLayout();
            grpPerformanceFilters.SuspendLayout();
            Form_Analytics_TableLayout_PerfFilters.SuspendLayout();
            grpPerformanceInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridPerformance).BeginInit();
            tabQuality.SuspendLayout();
            Form_Analytics_TableLayout_Quality.SuspendLayout();
            grpQualityFilters.SuspendLayout();
            Form_Analytics_TableLayout_QualityFilters.SuspendLayout();
            grpQualityInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridQuality).BeginInit();
            tabUserDetail.SuspendLayout();
            Form_Analytics_TableLayout_UserDetail.SuspendLayout();
            grpUserSelection.SuspendLayout();
            Form_Analytics_TableLayout_UserSelect.SuspendLayout();
            grpUserDetailInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridUserHistory).BeginInit();
            tabGlossary.SuspendLayout();
            SuspendLayout();
            // 
            // Form_Analytics_TableLayout_Main
            // 
            Form_Analytics_TableLayout_Main.ColumnCount = 1;
            Form_Analytics_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Form_Analytics_TableLayout_Main.Controls.Add(Form_Analytics_TabControl_Main, 0, 0);
            Form_Analytics_TableLayout_Main.Controls.Add(lblStatus, 0, 1);
            Form_Analytics_TableLayout_Main.Controls.Add(progressBar, 0, 2);
            Form_Analytics_TableLayout_Main.Dock = DockStyle.Fill;
            Form_Analytics_TableLayout_Main.Location = new Point(0, 0);
            Form_Analytics_TableLayout_Main.Name = "Form_Analytics_TableLayout_Main";
            Form_Analytics_TableLayout_Main.RowCount = 3;
            Form_Analytics_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Form_Analytics_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            Form_Analytics_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Absolute, 10F));
            Form_Analytics_TableLayout_Main.Size = new Size(1000, 600);
            Form_Analytics_TableLayout_Main.TabIndex = 0;
            // 
            // Form_Analytics_TabControl_Main
            // 
            Form_Analytics_TabControl_Main.Controls.Add(tabPerformance);
            Form_Analytics_TabControl_Main.Controls.Add(tabQuality);
            Form_Analytics_TabControl_Main.Controls.Add(tabUserDetail);
            Form_Analytics_TabControl_Main.Controls.Add(tabGlossary);
            Form_Analytics_TabControl_Main.Dock = DockStyle.Fill;
            Form_Analytics_TabControl_Main.Location = new Point(3, 3);
            Form_Analytics_TabControl_Main.Name = "Form_Analytics_TabControl_Main";
            Form_Analytics_TabControl_Main.SelectedIndex = 0;
            Form_Analytics_TabControl_Main.Size = new Size(994, 559);
            Form_Analytics_TabControl_Main.TabIndex = 0;
            // 
            // tabPerformance
            // 
            tabPerformance.Controls.Add(Form_Analytics_TableLayout_Performance);
            tabPerformance.Location = new Point(4, 24);
            tabPerformance.Name = "tabPerformance";
            tabPerformance.Padding = new Padding(3);
            tabPerformance.Size = new Size(986, 531);
            tabPerformance.TabIndex = 0;
            tabPerformance.Text = "Team Performance";
            tabPerformance.UseVisualStyleBackColor = true;
            // 
            // Form_Analytics_TableLayout_Performance
            // 
            Form_Analytics_TableLayout_Performance.ColumnCount = 1;
            Form_Analytics_TableLayout_Performance.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Form_Analytics_TableLayout_Performance.Controls.Add(grpPerformanceFilters, 0, 0);
            Form_Analytics_TableLayout_Performance.Controls.Add(grpPerformanceInfo, 0, 1);
            Form_Analytics_TableLayout_Performance.Controls.Add(gridPerformance, 0, 2);
            Form_Analytics_TableLayout_Performance.Dock = DockStyle.Fill;
            Form_Analytics_TableLayout_Performance.Location = new Point(3, 3);
            Form_Analytics_TableLayout_Performance.Name = "Form_Analytics_TableLayout_Performance";
            Form_Analytics_TableLayout_Performance.RowCount = 3;
            Form_Analytics_TableLayout_Performance.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            Form_Analytics_TableLayout_Performance.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            Form_Analytics_TableLayout_Performance.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Form_Analytics_TableLayout_Performance.Size = new Size(980, 525);
            Form_Analytics_TableLayout_Performance.TabIndex = 0;
            // 
            // grpPerformanceFilters
            // 
            grpPerformanceFilters.Controls.Add(Form_Analytics_TableLayout_PerfFilters);
            grpPerformanceFilters.Dock = DockStyle.Fill;
            grpPerformanceFilters.Location = new Point(3, 3);
            grpPerformanceFilters.Name = "grpPerformanceFilters";
            grpPerformanceFilters.Size = new Size(974, 74);
            grpPerformanceFilters.TabIndex = 0;
            grpPerformanceFilters.TabStop = false;
            grpPerformanceFilters.Text = "Filters";
            // 
            // Form_Analytics_TableLayout_PerfFilters
            // 
            Form_Analytics_TableLayout_PerfFilters.ColumnCount = 5;
            Form_Analytics_TableLayout_PerfFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            Form_Analytics_TableLayout_PerfFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            Form_Analytics_TableLayout_PerfFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            Form_Analytics_TableLayout_PerfFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            Form_Analytics_TableLayout_PerfFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Form_Analytics_TableLayout_PerfFilters.Controls.Add(lblDateFrom, 0, 0);
            Form_Analytics_TableLayout_PerfFilters.Controls.Add(dtpDateFrom, 1, 0);
            Form_Analytics_TableLayout_PerfFilters.Controls.Add(lblDateTo, 2, 0);
            Form_Analytics_TableLayout_PerfFilters.Controls.Add(dtpDateTo, 3, 0);
            Form_Analytics_TableLayout_PerfFilters.Controls.Add(btnRefreshPerformance, 4, 0);
            Form_Analytics_TableLayout_PerfFilters.Dock = DockStyle.Fill;
            Form_Analytics_TableLayout_PerfFilters.Location = new Point(3, 19);
            Form_Analytics_TableLayout_PerfFilters.Name = "Form_Analytics_TableLayout_PerfFilters";
            Form_Analytics_TableLayout_PerfFilters.RowCount = 1;
            Form_Analytics_TableLayout_PerfFilters.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Form_Analytics_TableLayout_PerfFilters.Size = new Size(968, 52);
            Form_Analytics_TableLayout_PerfFilters.TabIndex = 0;
            // 
            // lblDateFrom
            // 
            lblDateFrom.Anchor = AnchorStyles.Right;
            lblDateFrom.AutoSize = true;
            lblDateFrom.Location = new Point(12, 18);
            lblDateFrom.Name = "lblDateFrom";
            lblDateFrom.Size = new Size(65, 15);
            lblDateFrom.TabIndex = 0;
            lblDateFrom.Text = "Date From:";
            // 
            // dtpDateFrom
            // 
            dtpDateFrom.Anchor = AnchorStyles.Left;
            dtpDateFrom.Format = DateTimePickerFormat.Short;
            dtpDateFrom.Location = new Point(83, 14);
            dtpDateFrom.Name = "dtpDateFrom";
            dtpDateFrom.Size = new Size(120, 23);
            dtpDateFrom.TabIndex = 1;
            // 
            // lblDateTo
            // 
            lblDateTo.Anchor = AnchorStyles.Right;
            lblDateTo.AutoSize = true;
            lblDateTo.Location = new Point(257, 18);
            lblDateTo.Name = "lblDateTo";
            lblDateTo.Size = new Size(50, 15);
            lblDateTo.TabIndex = 2;
            lblDateTo.Text = "Date To:";
            // 
            // dtpDateTo
            // 
            dtpDateTo.Anchor = AnchorStyles.Left;
            dtpDateTo.Format = DateTimePickerFormat.Short;
            dtpDateTo.Location = new Point(313, 14);
            dtpDateTo.Name = "dtpDateTo";
            dtpDateTo.Size = new Size(120, 23);
            dtpDateTo.TabIndex = 3;
            // 
            // btnRefreshPerformance
            // 
            btnRefreshPerformance.Anchor = AnchorStyles.Left;
            btnRefreshPerformance.Location = new Point(463, 11);
            btnRefreshPerformance.Name = "btnRefreshPerformance";
            btnRefreshPerformance.Size = new Size(100, 30);
            btnRefreshPerformance.TabIndex = 4;
            btnRefreshPerformance.Text = "Refresh";
            btnRefreshPerformance.UseVisualStyleBackColor = true;
            btnRefreshPerformance.Click += btnRefreshPerformance_Click;
            // 
            // grpPerformanceInfo
            // 
            grpPerformanceInfo.Controls.Add(txtPerformanceInfo);
            grpPerformanceInfo.Dock = DockStyle.Fill;
            grpPerformanceInfo.Location = new Point(3, 83);
            grpPerformanceInfo.Name = "grpPerformanceInfo";
            grpPerformanceInfo.Size = new Size(974, 94);
            grpPerformanceInfo.TabIndex = 2;
            grpPerformanceInfo.TabStop = false;
            grpPerformanceInfo.Text = "Legend & Information";
            // 
            // txtPerformanceInfo
            // 
            txtPerformanceInfo.BackColor = SystemColors.Control;
            txtPerformanceInfo.BorderStyle = BorderStyle.None;
            txtPerformanceInfo.Dock = DockStyle.Fill;
            txtPerformanceInfo.Location = new Point(3, 19);
            txtPerformanceInfo.Name = "txtPerformanceInfo";
            txtPerformanceInfo.ReadOnly = true;
            txtPerformanceInfo.Size = new Size(968, 72);
            txtPerformanceInfo.TabIndex = 0;
            txtPerformanceInfo.Text = resources.GetString("txtPerformanceInfo.Text");
            // 
            // gridPerformance
            // 
            gridPerformance.AllowUserToAddRows = false;
            gridPerformance.AllowUserToDeleteRows = false;
            gridPerformance.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridPerformance.Dock = DockStyle.Fill;
            gridPerformance.Location = new Point(3, 183);
            gridPerformance.Name = "gridPerformance";
            gridPerformance.ReadOnly = true;
            gridPerformance.Size = new Size(974, 339);
            gridPerformance.TabIndex = 1;
            // 
            // tabQuality
            // 
            tabQuality.Controls.Add(Form_Analytics_TableLayout_Quality);
            tabQuality.Location = new Point(4, 24);
            tabQuality.Name = "tabQuality";
            tabQuality.Padding = new Padding(3);
            tabQuality.Size = new Size(986, 531);
            tabQuality.TabIndex = 1;
            tabQuality.Text = "Quality & Anomalies";
            tabQuality.UseVisualStyleBackColor = true;
            // 
            // Form_Analytics_TableLayout_Quality
            // 
            Form_Analytics_TableLayout_Quality.ColumnCount = 1;
            Form_Analytics_TableLayout_Quality.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Form_Analytics_TableLayout_Quality.Controls.Add(grpQualityFilters, 0, 0);
            Form_Analytics_TableLayout_Quality.Controls.Add(grpQualityInfo, 0, 1);
            Form_Analytics_TableLayout_Quality.Controls.Add(gridQuality, 0, 2);
            Form_Analytics_TableLayout_Quality.Dock = DockStyle.Fill;
            Form_Analytics_TableLayout_Quality.Location = new Point(3, 3);
            Form_Analytics_TableLayout_Quality.Name = "Form_Analytics_TableLayout_Quality";
            Form_Analytics_TableLayout_Quality.RowCount = 3;
            Form_Analytics_TableLayout_Quality.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            Form_Analytics_TableLayout_Quality.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            Form_Analytics_TableLayout_Quality.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Form_Analytics_TableLayout_Quality.Size = new Size(980, 525);
            Form_Analytics_TableLayout_Quality.TabIndex = 0;
            // 
            // grpQualityFilters
            // 
            grpQualityFilters.Controls.Add(Form_Analytics_TableLayout_QualityFilters);
            grpQualityFilters.Dock = DockStyle.Fill;
            grpQualityFilters.Location = new Point(3, 3);
            grpQualityFilters.Name = "grpQualityFilters";
            grpQualityFilters.Size = new Size(974, 74);
            grpQualityFilters.TabIndex = 0;
            grpQualityFilters.TabStop = false;
            grpQualityFilters.Text = "Filters";
            // 
            // Form_Analytics_TableLayout_QualityFilters
            // 
            Form_Analytics_TableLayout_QualityFilters.ColumnCount = 2;
            Form_Analytics_TableLayout_QualityFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Form_Analytics_TableLayout_QualityFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            Form_Analytics_TableLayout_QualityFilters.Controls.Add(btnRefreshQuality, 1, 0);
            Form_Analytics_TableLayout_QualityFilters.Dock = DockStyle.Fill;
            Form_Analytics_TableLayout_QualityFilters.Location = new Point(3, 19);
            Form_Analytics_TableLayout_QualityFilters.Name = "Form_Analytics_TableLayout_QualityFilters";
            Form_Analytics_TableLayout_QualityFilters.RowCount = 1;
            Form_Analytics_TableLayout_QualityFilters.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Form_Analytics_TableLayout_QualityFilters.Size = new Size(968, 52);
            Form_Analytics_TableLayout_QualityFilters.TabIndex = 0;
            // 
            // btnRefreshQuality
            // 
            btnRefreshQuality.Anchor = AnchorStyles.Right;
            btnRefreshQuality.Location = new Point(865, 11);
            btnRefreshQuality.Name = "btnRefreshQuality";
            btnRefreshQuality.Size = new Size(100, 30);
            btnRefreshQuality.TabIndex = 0;
            btnRefreshQuality.Text = "Refresh";
            btnRefreshQuality.UseVisualStyleBackColor = true;
            btnRefreshQuality.Click += btnRefreshQuality_Click;
            // 
            // grpQualityInfo
            // 
            grpQualityInfo.Controls.Add(txtQualityInfo);
            grpQualityInfo.Dock = DockStyle.Fill;
            grpQualityInfo.Location = new Point(3, 83);
            grpQualityInfo.Name = "grpQualityInfo";
            grpQualityInfo.Size = new Size(974, 94);
            grpQualityInfo.TabIndex = 2;
            grpQualityInfo.TabStop = false;
            grpQualityInfo.Text = "Legend & Information";
            // 
            // txtQualityInfo
            // 
            txtQualityInfo.BackColor = SystemColors.Control;
            txtQualityInfo.BorderStyle = BorderStyle.None;
            txtQualityInfo.Dock = DockStyle.Fill;
            txtQualityInfo.Location = new Point(3, 19);
            txtQualityInfo.Name = "txtQualityInfo";
            txtQualityInfo.ReadOnly = true;
            txtQualityInfo.Size = new Size(968, 72);
            txtQualityInfo.TabIndex = 0;
            txtQualityInfo.Text = resources.GetString("txtQualityInfo.Text");
            // 
            // gridQuality
            // 
            gridQuality.AllowUserToAddRows = false;
            gridQuality.AllowUserToDeleteRows = false;
            gridQuality.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridQuality.Dock = DockStyle.Fill;
            gridQuality.Location = new Point(3, 183);
            gridQuality.Name = "gridQuality";
            gridQuality.ReadOnly = true;
            gridQuality.Size = new Size(974, 339);
            gridQuality.TabIndex = 1;
            // 
            // tabUserDetail
            // 
            tabUserDetail.Controls.Add(Form_Analytics_TableLayout_UserDetail);
            tabUserDetail.Location = new Point(4, 24);
            tabUserDetail.Name = "tabUserDetail";
            tabUserDetail.Padding = new Padding(3);
            tabUserDetail.Size = new Size(986, 531);
            tabUserDetail.TabIndex = 2;
            tabUserDetail.Text = "User Detail";
            tabUserDetail.UseVisualStyleBackColor = true;
            // 
            // Form_Analytics_TableLayout_UserDetail
            // 
            Form_Analytics_TableLayout_UserDetail.ColumnCount = 1;
            Form_Analytics_TableLayout_UserDetail.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Form_Analytics_TableLayout_UserDetail.Controls.Add(grpUserSelection, 0, 0);
            Form_Analytics_TableLayout_UserDetail.Controls.Add(grpUserDetailInfo, 0, 1);
            Form_Analytics_TableLayout_UserDetail.Controls.Add(gridUserHistory, 0, 2);
            Form_Analytics_TableLayout_UserDetail.Dock = DockStyle.Fill;
            Form_Analytics_TableLayout_UserDetail.Location = new Point(3, 3);
            Form_Analytics_TableLayout_UserDetail.Name = "Form_Analytics_TableLayout_UserDetail";
            Form_Analytics_TableLayout_UserDetail.RowCount = 3;
            Form_Analytics_TableLayout_UserDetail.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            Form_Analytics_TableLayout_UserDetail.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            Form_Analytics_TableLayout_UserDetail.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Form_Analytics_TableLayout_UserDetail.Size = new Size(980, 525);
            Form_Analytics_TableLayout_UserDetail.TabIndex = 0;
            // 
            // grpUserSelection
            // 
            grpUserSelection.Controls.Add(Form_Analytics_TableLayout_UserSelect);
            grpUserSelection.Dock = DockStyle.Fill;
            grpUserSelection.Location = new Point(3, 3);
            grpUserSelection.Name = "grpUserSelection";
            grpUserSelection.Size = new Size(974, 74);
            grpUserSelection.TabIndex = 0;
            grpUserSelection.TabStop = false;
            grpUserSelection.Text = "Select User";
            // 
            // Form_Analytics_TableLayout_UserSelect
            // 
            Form_Analytics_TableLayout_UserSelect.ColumnCount = 3;
            Form_Analytics_TableLayout_UserSelect.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            Form_Analytics_TableLayout_UserSelect.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            Form_Analytics_TableLayout_UserSelect.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Form_Analytics_TableLayout_UserSelect.Controls.Add(lblUser, 0, 0);
            Form_Analytics_TableLayout_UserSelect.Controls.Add(comboUsers, 1, 0);
            Form_Analytics_TableLayout_UserSelect.Controls.Add(btnLoadUser, 2, 0);
            Form_Analytics_TableLayout_UserSelect.Dock = DockStyle.Fill;
            Form_Analytics_TableLayout_UserSelect.Location = new Point(3, 19);
            Form_Analytics_TableLayout_UserSelect.Name = "Form_Analytics_TableLayout_UserSelect";
            Form_Analytics_TableLayout_UserSelect.RowCount = 1;
            Form_Analytics_TableLayout_UserSelect.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Form_Analytics_TableLayout_UserSelect.Size = new Size(968, 52);
            Form_Analytics_TableLayout_UserSelect.TabIndex = 0;
            // 
            // lblUser
            // 
            lblUser.Anchor = AnchorStyles.Right;
            lblUser.AutoSize = true;
            lblUser.Location = new Point(44, 18);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(33, 15);
            lblUser.TabIndex = 0;
            lblUser.Text = "User:";
            // 
            // comboUsers
            // 
            comboUsers.Anchor = AnchorStyles.Left;
            comboUsers.DropDownStyle = ComboBoxStyle.DropDownList;
            comboUsers.FormattingEnabled = true;
            comboUsers.Location = new Point(83, 14);
            comboUsers.Name = "comboUsers";
            comboUsers.Size = new Size(194, 23);
            comboUsers.TabIndex = 1;
            // 
            // btnLoadUser
            // 
            btnLoadUser.Anchor = AnchorStyles.Left;
            btnLoadUser.Location = new Point(283, 11);
            btnLoadUser.Name = "btnLoadUser";
            btnLoadUser.Size = new Size(100, 30);
            btnLoadUser.TabIndex = 2;
            btnLoadUser.Text = "Load History";
            btnLoadUser.UseVisualStyleBackColor = true;
            btnLoadUser.Click += btnLoadUser_Click;
            // 
            // grpUserDetailInfo
            // 
            grpUserDetailInfo.Controls.Add(txtUserDetailInfo);
            grpUserDetailInfo.Dock = DockStyle.Fill;
            grpUserDetailInfo.Location = new Point(3, 83);
            grpUserDetailInfo.Name = "grpUserDetailInfo";
            grpUserDetailInfo.Size = new Size(974, 94);
            grpUserDetailInfo.TabIndex = 2;
            grpUserDetailInfo.TabStop = false;
            grpUserDetailInfo.Text = "Legend & Information";
            // 
            // txtUserDetailInfo
            // 
            txtUserDetailInfo.BackColor = SystemColors.Control;
            txtUserDetailInfo.BorderStyle = BorderStyle.None;
            txtUserDetailInfo.Dock = DockStyle.Fill;
            txtUserDetailInfo.Location = new Point(3, 19);
            txtUserDetailInfo.Name = "txtUserDetailInfo";
            txtUserDetailInfo.ReadOnly = true;
            txtUserDetailInfo.Size = new Size(968, 72);
            txtUserDetailInfo.TabIndex = 0;
            txtUserDetailInfo.Text = "Select a user to view their detailed transaction history for the selected date range.\nUse this view to investigate specific 'Rapid Fire' or 'Ping Pong' events identified in the other tabs.";
            // 
            // gridUserHistory
            // 
            gridUserHistory.AllowUserToAddRows = false;
            gridUserHistory.AllowUserToDeleteRows = false;
            gridUserHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridUserHistory.Dock = DockStyle.Fill;
            gridUserHistory.Location = new Point(3, 183);
            gridUserHistory.Name = "gridUserHistory";
            gridUserHistory.ReadOnly = true;
            gridUserHistory.Size = new Size(974, 339);
            gridUserHistory.TabIndex = 1;
            // 
            // tabGlossary
            // 
            tabGlossary.Controls.Add(txtGlossary);
            tabGlossary.Location = new Point(4, 24);
            tabGlossary.Name = "tabGlossary";
            tabGlossary.Padding = new Padding(3);
            tabGlossary.Size = new Size(986, 531);
            tabGlossary.TabIndex = 3;
            tabGlossary.Text = "Glossary & Metrics";
            tabGlossary.UseVisualStyleBackColor = true;
            // 
            // txtGlossary
            // 
            txtGlossary.BackColor = SystemColors.Window;
            txtGlossary.BorderStyle = BorderStyle.None;
            txtGlossary.Dock = DockStyle.Fill;
            txtGlossary.Font = new Font("Segoe UI Emoji", 10F);
            txtGlossary.Location = new Point(3, 3);
            txtGlossary.Name = "txtGlossary";
            txtGlossary.ReadOnly = true;
            txtGlossary.Size = new Size(980, 525);
            txtGlossary.TabIndex = 0;
            txtGlossary.Text = "";
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Dock = DockStyle.Fill;
            lblStatus.Location = new Point(3, 565);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(994, 25);
            lblStatus.TabIndex = 2;
            lblStatus.Text = "Ready";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // progressBar
            // 
            progressBar.Dock = DockStyle.Fill;
            progressBar.Location = new Point(3, 593);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(994, 4);
            progressBar.TabIndex = 1;
            // 
            // Form_WIPUserAnalytics
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1000, 600);
            Controls.Add(Form_Analytics_TableLayout_Main);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form_WIPUserAnalytics";
            Text = "Material Handler Analytics";
            Form_Analytics_TableLayout_Main.ResumeLayout(false);
            Form_Analytics_TableLayout_Main.PerformLayout();
            Form_Analytics_TabControl_Main.ResumeLayout(false);
            tabPerformance.ResumeLayout(false);
            Form_Analytics_TableLayout_Performance.ResumeLayout(false);
            grpPerformanceFilters.ResumeLayout(false);
            Form_Analytics_TableLayout_PerfFilters.ResumeLayout(false);
            Form_Analytics_TableLayout_PerfFilters.PerformLayout();
            grpPerformanceInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridPerformance).EndInit();
            tabQuality.ResumeLayout(false);
            Form_Analytics_TableLayout_Quality.ResumeLayout(false);
            grpQualityFilters.ResumeLayout(false);
            Form_Analytics_TableLayout_QualityFilters.ResumeLayout(false);
            grpQualityInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridQuality).EndInit();
            tabUserDetail.ResumeLayout(false);
            Form_Analytics_TableLayout_UserDetail.ResumeLayout(false);
            grpUserSelection.ResumeLayout(false);
            Form_Analytics_TableLayout_UserSelect.ResumeLayout(false);
            Form_Analytics_TableLayout_UserSelect.PerformLayout();
            grpUserDetailInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridUserHistory).EndInit();
            tabGlossary.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel Form_Analytics_TableLayout_Main;
        private System.Windows.Forms.TabControl Form_Analytics_TabControl_Main;
        private System.Windows.Forms.TabPage tabPerformance;
        private System.Windows.Forms.TabPage tabQuality;
        private System.Windows.Forms.TabPage tabUserDetail;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblStatus;

        // Performance Tab
        private System.Windows.Forms.TableLayoutPanel Form_Analytics_TableLayout_Performance;
        private System.Windows.Forms.GroupBox grpPerformanceFilters;
        private System.Windows.Forms.TableLayoutPanel Form_Analytics_TableLayout_PerfFilters;
        private System.Windows.Forms.Label lblDateFrom;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.Label lblDateTo;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.Button btnRefreshPerformance;
        private System.Windows.Forms.DataGridView gridPerformance;

        // Quality Tab
        private System.Windows.Forms.TableLayoutPanel Form_Analytics_TableLayout_Quality;
        private System.Windows.Forms.GroupBox grpQualityFilters;
        private System.Windows.Forms.TableLayoutPanel Form_Analytics_TableLayout_QualityFilters;
        private System.Windows.Forms.Button btnRefreshQuality;
        private System.Windows.Forms.DataGridView gridQuality;

        // User Detail Tab
        private System.Windows.Forms.TableLayoutPanel Form_Analytics_TableLayout_UserDetail;
        private System.Windows.Forms.GroupBox grpUserSelection;
        private System.Windows.Forms.TableLayoutPanel Form_Analytics_TableLayout_UserSelect;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.ComboBox comboUsers;
        private System.Windows.Forms.Button btnLoadUser;
        private System.Windows.Forms.DataGridView gridUserHistory;

        // Info Controls
        private System.Windows.Forms.GroupBox grpPerformanceInfo;
        private System.Windows.Forms.RichTextBox txtPerformanceInfo;
        private System.Windows.Forms.GroupBox grpQualityInfo;
        private System.Windows.Forms.RichTextBox txtQualityInfo;
        private System.Windows.Forms.GroupBox grpUserDetailInfo;
        private System.Windows.Forms.RichTextBox txtUserDetailInfo;

        // Glossary Tab
        private System.Windows.Forms.TabPage tabGlossary;
        private System.Windows.Forms.RichTextBox txtGlossary;
    }
}
