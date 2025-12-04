namespace MTM_WIP_Application_Winforms.Forms.Analytics
{
    partial class Form_Analytics
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
            this.Form_Analytics_TableLayout_Main = new System.Windows.Forms.TableLayoutPanel();
            this.Form_Analytics_TabControl_Main = new System.Windows.Forms.TabControl();
            this.tabPerformance = new System.Windows.Forms.TabPage();
            this.Form_Analytics_TableLayout_Performance = new System.Windows.Forms.TableLayoutPanel();
            this.grpPerformanceFilters = new System.Windows.Forms.GroupBox();
            this.Form_Analytics_TableLayout_PerfFilters = new System.Windows.Forms.TableLayoutPanel();
            this.lblDateFrom = new System.Windows.Forms.Label();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.lblDateTo = new System.Windows.Forms.Label();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.btnRefreshPerformance = new System.Windows.Forms.Button();
            this.grpPerformanceInfo = new System.Windows.Forms.GroupBox();
            this.txtPerformanceInfo = new System.Windows.Forms.RichTextBox();
            this.gridPerformance = new System.Windows.Forms.DataGridView();
            this.tabQuality = new System.Windows.Forms.TabPage();
            this.Form_Analytics_TableLayout_Quality = new System.Windows.Forms.TableLayoutPanel();
            this.grpQualityFilters = new System.Windows.Forms.GroupBox();
            this.Form_Analytics_TableLayout_QualityFilters = new System.Windows.Forms.TableLayoutPanel();
            this.btnRefreshQuality = new System.Windows.Forms.Button();
            this.grpQualityInfo = new System.Windows.Forms.GroupBox();
            this.txtQualityInfo = new System.Windows.Forms.RichTextBox();
            this.gridQuality = new System.Windows.Forms.DataGridView();
            this.tabUserDetail = new System.Windows.Forms.TabPage();
            this.Form_Analytics_TableLayout_UserDetail = new System.Windows.Forms.TableLayoutPanel();
            this.grpUserSelection = new System.Windows.Forms.GroupBox();
            this.Form_Analytics_TableLayout_UserSelect = new System.Windows.Forms.TableLayoutPanel();
            this.lblUser = new System.Windows.Forms.Label();
            this.comboUsers = new System.Windows.Forms.ComboBox();
            this.btnLoadUser = new System.Windows.Forms.Button();
            this.grpUserDetailInfo = new System.Windows.Forms.GroupBox();
            this.txtUserDetailInfo = new System.Windows.Forms.RichTextBox();
            this.gridUserHistory = new System.Windows.Forms.DataGridView();
            this.tabGlossary = new System.Windows.Forms.TabPage();
            this.txtGlossary = new System.Windows.Forms.RichTextBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();

            this.Form_Analytics_TableLayout_Main.SuspendLayout();
            this.Form_Analytics_TabControl_Main.SuspendLayout();
            this.tabPerformance.SuspendLayout();
            this.Form_Analytics_TableLayout_Performance.SuspendLayout();
            this.grpPerformanceFilters.SuspendLayout();
            this.Form_Analytics_TableLayout_PerfFilters.SuspendLayout();
            this.grpPerformanceInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPerformance)).BeginInit();
            this.tabQuality.SuspendLayout();
            this.Form_Analytics_TableLayout_Quality.SuspendLayout();
            this.grpQualityFilters.SuspendLayout();
            this.Form_Analytics_TableLayout_QualityFilters.SuspendLayout();
            this.grpQualityInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridQuality)).BeginInit();
            this.tabUserDetail.SuspendLayout();
            this.Form_Analytics_TableLayout_UserDetail.SuspendLayout();
            this.grpUserSelection.SuspendLayout();
            this.Form_Analytics_TableLayout_UserSelect.SuspendLayout();
            this.grpUserDetailInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridUserHistory)).BeginInit();
            this.SuspendLayout();

            // 
            // Form_Analytics_TableLayout_Main
            // 
            this.Form_Analytics_TableLayout_Main.ColumnCount = 1;
            this.Form_Analytics_TableLayout_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Form_Analytics_TableLayout_Main.Controls.Add(this.Form_Analytics_TabControl_Main, 0, 0);
            this.Form_Analytics_TableLayout_Main.Controls.Add(this.lblStatus, 0, 1);
            this.Form_Analytics_TableLayout_Main.Controls.Add(this.progressBar, 0, 2);
            this.Form_Analytics_TableLayout_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Form_Analytics_TableLayout_Main.Location = new System.Drawing.Point(0, 0);
            this.Form_Analytics_TableLayout_Main.Name = "Form_Analytics_TableLayout_Main";
            this.Form_Analytics_TableLayout_Main.RowCount = 3;
            this.Form_Analytics_TableLayout_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Form_Analytics_TableLayout_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.Form_Analytics_TableLayout_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.Form_Analytics_TableLayout_Main.Size = new System.Drawing.Size(1000, 600);
            this.Form_Analytics_TableLayout_Main.TabIndex = 0;

            // 
            // Form_Analytics_TabControl_Main
            // 
            this.Form_Analytics_TabControl_Main.Controls.Add(this.tabPerformance);
            this.Form_Analytics_TabControl_Main.Controls.Add(this.tabQuality);
            this.Form_Analytics_TabControl_Main.Controls.Add(this.tabUserDetail);
            this.Form_Analytics_TabControl_Main.Controls.Add(this.tabGlossary);
            this.Form_Analytics_TabControl_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Form_Analytics_TabControl_Main.Location = new System.Drawing.Point(3, 3);
            this.Form_Analytics_TabControl_Main.Name = "Form_Analytics_TabControl_Main";
            this.Form_Analytics_TabControl_Main.SelectedIndex = 0;
            this.Form_Analytics_TabControl_Main.Size = new System.Drawing.Size(994, 559);
            this.Form_Analytics_TabControl_Main.TabIndex = 0;

            // 
            // tabPerformance
            // 
            this.tabPerformance.Controls.Add(this.Form_Analytics_TableLayout_Performance);
            this.tabPerformance.Location = new System.Drawing.Point(4, 24);
            this.tabPerformance.Name = "tabPerformance";
            this.tabPerformance.Padding = new System.Windows.Forms.Padding(3);
            this.tabPerformance.Size = new System.Drawing.Size(986, 531);
            this.tabPerformance.TabIndex = 0;
            this.tabPerformance.Text = "Team Performance";
            this.tabPerformance.UseVisualStyleBackColor = true;

            // ...existing code...

            // 
            // tabGlossary
            // 
            this.tabGlossary.Controls.Add(this.txtGlossary);
            this.tabGlossary.Location = new System.Drawing.Point(4, 24);
            this.tabGlossary.Name = "tabGlossary";
            this.tabGlossary.Padding = new System.Windows.Forms.Padding(3);
            this.tabGlossary.Size = new System.Drawing.Size(986, 531);
            this.tabGlossary.TabIndex = 3;
            this.tabGlossary.Text = "Glossary & Metrics";
            this.tabGlossary.UseVisualStyleBackColor = true;

            // 
            // txtGlossary
            // 
            this.txtGlossary.BackColor = System.Drawing.SystemColors.Window;
            this.txtGlossary.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtGlossary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGlossary.Font = new System.Drawing.Font("Segoe UI Emoji", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtGlossary.Location = new System.Drawing.Point(3, 3);
            this.txtGlossary.Name = "txtGlossary";
            this.txtGlossary.ReadOnly = true;
            this.txtGlossary.Size = new System.Drawing.Size(980, 525);
            this.txtGlossary.TabIndex = 0;
            this.txtGlossary.Text = "";

            // 
            // Form_Analytics_TableLayout_Performance
            // 
            this.Form_Analytics_TableLayout_Performance.ColumnCount = 1;
            this.Form_Analytics_TableLayout_Performance.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Form_Analytics_TableLayout_Performance.Controls.Add(this.grpPerformanceFilters, 0, 0);
            this.Form_Analytics_TableLayout_Performance.Controls.Add(this.grpPerformanceInfo, 0, 1);
            this.Form_Analytics_TableLayout_Performance.Controls.Add(this.gridPerformance, 0, 2);
            this.Form_Analytics_TableLayout_Performance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Form_Analytics_TableLayout_Performance.Location = new System.Drawing.Point(3, 3);
            this.Form_Analytics_TableLayout_Performance.Name = "Form_Analytics_TableLayout_Performance";
            this.Form_Analytics_TableLayout_Performance.RowCount = 3;
            this.Form_Analytics_TableLayout_Performance.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.Form_Analytics_TableLayout_Performance.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.Form_Analytics_TableLayout_Performance.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Form_Analytics_TableLayout_Performance.Size = new System.Drawing.Size(980, 525);
            this.Form_Analytics_TableLayout_Performance.TabIndex = 0;

            // 
            // grpPerformanceInfo
            // 
            this.grpPerformanceInfo.Controls.Add(this.txtPerformanceInfo);
            this.grpPerformanceInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpPerformanceInfo.Location = new System.Drawing.Point(3, 83);
            this.grpPerformanceInfo.Name = "grpPerformanceInfo";
            this.grpPerformanceInfo.Size = new System.Drawing.Size(974, 94);
            this.grpPerformanceInfo.TabIndex = 2;
            this.grpPerformanceInfo.TabStop = false;
            this.grpPerformanceInfo.Text = "Legend & Information";

            // 
            // txtPerformanceInfo
            // 
            this.txtPerformanceInfo.BackColor = System.Drawing.SystemColors.Control;
            this.txtPerformanceInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPerformanceInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPerformanceInfo.Location = new System.Drawing.Point(3, 19);
            this.txtPerformanceInfo.Name = "txtPerformanceInfo";
            this.txtPerformanceInfo.ReadOnly = true;
            this.txtPerformanceInfo.Size = new System.Drawing.Size(968, 72);
            this.txtPerformanceInfo.TabIndex = 0;
            this.txtPerformanceInfo.Text = "Total Tx: Total transactions performed.\nTotal Qty: Sum of quantities moved.\nUnique Parts: Number of distinct part numbers handled.\nQuality Score: Calculated score (0-100) based on efficiency and accuracy. Deductions for Rapid Fire, Ping Pong, and Off-Shift transactions.";

            // 
            // grpPerformanceFilters
            // 
            this.grpPerformanceFilters.Controls.Add(this.Form_Analytics_TableLayout_PerfFilters);
            this.grpPerformanceFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpPerformanceFilters.Location = new System.Drawing.Point(3, 3);
            this.grpPerformanceFilters.Name = "grpPerformanceFilters";
            this.grpPerformanceFilters.Size = new System.Drawing.Size(974, 74);
            this.grpPerformanceFilters.TabIndex = 0;
            this.grpPerformanceFilters.TabStop = false;
            this.grpPerformanceFilters.Text = "Filters";

            // 
            // Form_Analytics_TableLayout_PerfFilters
            // 
            this.Form_Analytics_TableLayout_PerfFilters.ColumnCount = 5;
            this.Form_Analytics_TableLayout_PerfFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.Form_Analytics_TableLayout_PerfFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.Form_Analytics_TableLayout_PerfFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.Form_Analytics_TableLayout_PerfFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.Form_Analytics_TableLayout_PerfFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Form_Analytics_TableLayout_PerfFilters.Controls.Add(this.lblDateFrom, 0, 0);
            this.Form_Analytics_TableLayout_PerfFilters.Controls.Add(this.dtpDateFrom, 1, 0);
            this.Form_Analytics_TableLayout_PerfFilters.Controls.Add(this.lblDateTo, 2, 0);
            this.Form_Analytics_TableLayout_PerfFilters.Controls.Add(this.dtpDateTo, 3, 0);
            this.Form_Analytics_TableLayout_PerfFilters.Controls.Add(this.btnRefreshPerformance, 4, 0);
            this.Form_Analytics_TableLayout_PerfFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Form_Analytics_TableLayout_PerfFilters.Location = new System.Drawing.Point(3, 19);
            this.Form_Analytics_TableLayout_PerfFilters.Name = "Form_Analytics_TableLayout_PerfFilters";
            this.Form_Analytics_TableLayout_PerfFilters.RowCount = 1;
            this.Form_Analytics_TableLayout_PerfFilters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Form_Analytics_TableLayout_PerfFilters.Size = new System.Drawing.Size(968, 52);
            this.Form_Analytics_TableLayout_PerfFilters.TabIndex = 0;

            // 
            // lblDateFrom
            // 
            this.lblDateFrom.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDateFrom.AutoSize = true;
            this.lblDateFrom.Location = new System.Drawing.Point(13, 18);
            this.lblDateFrom.Name = "lblDateFrom";
            this.lblDateFrom.Size = new System.Drawing.Size(64, 15);
            this.lblDateFrom.TabIndex = 0;
            this.lblDateFrom.Text = "Date From:";

            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateFrom.Location = new System.Drawing.Point(83, 14);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.Size = new System.Drawing.Size(120, 23);
            this.dtpDateFrom.TabIndex = 1;

            // 
            // lblDateTo
            // 
            this.lblDateTo.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblDateTo.AutoSize = true;
            this.lblDateTo.Location = new System.Drawing.Point(249, 18);
            this.lblDateTo.Name = "lblDateTo";
            this.lblDateTo.Size = new System.Drawing.Size(48, 15);
            this.lblDateTo.TabIndex = 2;
            this.lblDateTo.Text = "Date To:";

            // 
            // dtpDateTo
            // 
            this.dtpDateTo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateTo.Location = new System.Drawing.Point(313, 14);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.Size = new System.Drawing.Size(120, 23);
            this.dtpDateTo.TabIndex = 3;

            // 
            // btnRefreshPerformance
            // 
            this.btnRefreshPerformance.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnRefreshPerformance.Location = new System.Drawing.Point(463, 11);
            this.btnRefreshPerformance.Name = "btnRefreshPerformance";
            this.btnRefreshPerformance.Size = new System.Drawing.Size(100, 30);
            this.btnRefreshPerformance.TabIndex = 4;
            this.btnRefreshPerformance.Text = "Refresh";
            this.btnRefreshPerformance.UseVisualStyleBackColor = true;
            this.btnRefreshPerformance.Click += new System.EventHandler(this.btnRefreshPerformance_Click);

            // 
            // gridPerformance
            // 
            this.gridPerformance.AllowUserToAddRows = false;
            this.gridPerformance.AllowUserToDeleteRows = false;
            this.gridPerformance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPerformance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridPerformance.Location = new System.Drawing.Point(3, 83);
            this.gridPerformance.Name = "gridPerformance";
            this.gridPerformance.ReadOnly = true;
            this.gridPerformance.RowTemplate.Height = 25;
            this.gridPerformance.Size = new System.Drawing.Size(974, 439);
            this.gridPerformance.TabIndex = 1;

            // 
            // tabQuality
            // 
            this.tabQuality.Controls.Add(this.Form_Analytics_TableLayout_Quality);
            this.tabQuality.Location = new System.Drawing.Point(4, 24);
            this.tabQuality.Name = "tabQuality";
            this.tabQuality.Padding = new System.Windows.Forms.Padding(3);
            this.tabQuality.Size = new System.Drawing.Size(986, 531);
            this.tabQuality.TabIndex = 1;
            this.tabQuality.Text = "Quality & Anomalies";
            this.tabQuality.UseVisualStyleBackColor = true;

            // 
            // Form_Analytics_TableLayout_Quality
            // 
            this.Form_Analytics_TableLayout_Quality.ColumnCount = 1;
            this.Form_Analytics_TableLayout_Quality.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Form_Analytics_TableLayout_Quality.Controls.Add(this.grpQualityFilters, 0, 0);
            this.Form_Analytics_TableLayout_Quality.Controls.Add(this.grpQualityInfo, 0, 1);
            this.Form_Analytics_TableLayout_Quality.Controls.Add(this.gridQuality, 0, 2);
            this.Form_Analytics_TableLayout_Quality.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Form_Analytics_TableLayout_Quality.Location = new System.Drawing.Point(3, 3);
            this.Form_Analytics_TableLayout_Quality.Name = "Form_Analytics_TableLayout_Quality";
            this.Form_Analytics_TableLayout_Quality.RowCount = 3;
            this.Form_Analytics_TableLayout_Quality.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.Form_Analytics_TableLayout_Quality.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.Form_Analytics_TableLayout_Quality.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Form_Analytics_TableLayout_Quality.Size = new System.Drawing.Size(980, 525);
            this.Form_Analytics_TableLayout_Quality.TabIndex = 0;

            // 
            // grpQualityInfo
            // 
            this.grpQualityInfo.Controls.Add(this.txtQualityInfo);
            this.grpQualityInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpQualityInfo.Location = new System.Drawing.Point(3, 83);
            this.grpQualityInfo.Name = "grpQualityInfo";
            this.grpQualityInfo.Size = new System.Drawing.Size(974, 94);
            this.grpQualityInfo.TabIndex = 2;
            this.grpQualityInfo.TabStop = false;
            this.grpQualityInfo.Text = "Legend & Information";

            // 
            // txtQualityInfo
            // 
            this.txtQualityInfo.BackColor = System.Drawing.SystemColors.Control;
            this.txtQualityInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtQualityInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtQualityInfo.Location = new System.Drawing.Point(3, 19);
            this.txtQualityInfo.Name = "txtQualityInfo";
            this.txtQualityInfo.ReadOnly = true;
            this.txtQualityInfo.Size = new System.Drawing.Size(968, 72);
            this.txtQualityInfo.TabIndex = 0;
            this.txtQualityInfo.Text = "Rapid Fire: Transactions occurring less than 10 seconds apart. May indicate blind scanning.\nPing Pong: Moving a part from A to B, then back to A within 20 minutes.\nOff Shift: Transactions performed outside assigned shift hours.\nQuality Score: 100 - ((RapidFire * 0.5 + PingPong * 5 + OffShift * 2) / TotalTx * 100)";

            // 
            // grpQualityFilters
            // 
            this.grpQualityFilters.Controls.Add(this.Form_Analytics_TableLayout_QualityFilters);
            this.grpQualityFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpQualityFilters.Location = new System.Drawing.Point(3, 3);
            this.grpQualityFilters.Name = "grpQualityFilters";
            this.grpQualityFilters.Size = new System.Drawing.Size(974, 74);
            this.grpQualityFilters.TabIndex = 0;
            this.grpQualityFilters.TabStop = false;
            this.grpQualityFilters.Text = "Filters";

            // 
            // Form_Analytics_TableLayout_QualityFilters
            // 
            this.Form_Analytics_TableLayout_QualityFilters.ColumnCount = 2;
            this.Form_Analytics_TableLayout_QualityFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Form_Analytics_TableLayout_QualityFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.Form_Analytics_TableLayout_QualityFilters.Controls.Add(this.btnRefreshQuality, 1, 0);
            this.Form_Analytics_TableLayout_QualityFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Form_Analytics_TableLayout_QualityFilters.Location = new System.Drawing.Point(3, 19);
            this.Form_Analytics_TableLayout_QualityFilters.Name = "Form_Analytics_TableLayout_QualityFilters";
            this.Form_Analytics_TableLayout_QualityFilters.RowCount = 1;
            this.Form_Analytics_TableLayout_QualityFilters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Form_Analytics_TableLayout_QualityFilters.Size = new System.Drawing.Size(968, 52);
            this.Form_Analytics_TableLayout_QualityFilters.TabIndex = 0;

            // 
            // btnRefreshQuality
            // 
            this.btnRefreshQuality.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnRefreshQuality.Location = new System.Drawing.Point(865, 11);
            this.btnRefreshQuality.Name = "btnRefreshQuality";
            this.btnRefreshQuality.Size = new System.Drawing.Size(100, 30);
            this.btnRefreshQuality.TabIndex = 0;
            this.btnRefreshQuality.Text = "Refresh";
            this.btnRefreshQuality.UseVisualStyleBackColor = true;
            this.btnRefreshQuality.Click += new System.EventHandler(this.btnRefreshQuality_Click);

            // 
            // gridQuality
            // 
            this.gridQuality.AllowUserToAddRows = false;
            this.gridQuality.AllowUserToDeleteRows = false;
            this.gridQuality.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridQuality.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridQuality.Location = new System.Drawing.Point(3, 83);
            this.gridQuality.Name = "gridQuality";
            this.gridQuality.ReadOnly = true;
            this.gridQuality.RowTemplate.Height = 25;
            this.gridQuality.Size = new System.Drawing.Size(974, 439);
            this.gridQuality.TabIndex = 1;

            // 
            // tabUserDetail
            // 
            this.tabUserDetail.Controls.Add(this.Form_Analytics_TableLayout_UserDetail);
            this.tabUserDetail.Location = new System.Drawing.Point(4, 24);
            this.tabUserDetail.Name = "tabUserDetail";
            this.tabUserDetail.Padding = new System.Windows.Forms.Padding(3);
            this.tabUserDetail.Size = new System.Drawing.Size(986, 531);
            this.tabUserDetail.TabIndex = 2;
            this.tabUserDetail.Text = "User Detail";
            this.tabUserDetail.UseVisualStyleBackColor = true;

            // 
            // Form_Analytics_TableLayout_UserDetail
            // 
            this.Form_Analytics_TableLayout_UserDetail.ColumnCount = 1;
            this.Form_Analytics_TableLayout_UserDetail.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Form_Analytics_TableLayout_UserDetail.Controls.Add(this.grpUserSelection, 0, 0);
            this.Form_Analytics_TableLayout_UserDetail.Controls.Add(this.grpUserDetailInfo, 0, 1);
            this.Form_Analytics_TableLayout_UserDetail.Controls.Add(this.gridUserHistory, 0, 2);
            this.Form_Analytics_TableLayout_UserDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Form_Analytics_TableLayout_UserDetail.Location = new System.Drawing.Point(3, 3);
            this.Form_Analytics_TableLayout_UserDetail.Name = "Form_Analytics_TableLayout_UserDetail";
            this.Form_Analytics_TableLayout_UserDetail.RowCount = 3;
            this.Form_Analytics_TableLayout_UserDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.Form_Analytics_TableLayout_UserDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.Form_Analytics_TableLayout_UserDetail.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Form_Analytics_TableLayout_UserDetail.Size = new System.Drawing.Size(980, 525);
            this.Form_Analytics_TableLayout_UserDetail.TabIndex = 0;

            // 
            // grpUserDetailInfo
            // 
            this.grpUserDetailInfo.Controls.Add(this.txtUserDetailInfo);
            this.grpUserDetailInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpUserDetailInfo.Location = new System.Drawing.Point(3, 83);
            this.grpUserDetailInfo.Name = "grpUserDetailInfo";
            this.grpUserDetailInfo.Size = new System.Drawing.Size(974, 94);
            this.grpUserDetailInfo.TabIndex = 2;
            this.grpUserDetailInfo.TabStop = false;
            this.grpUserDetailInfo.Text = "Legend & Information";

            // 
            // txtUserDetailInfo
            // 
            this.txtUserDetailInfo.BackColor = System.Drawing.SystemColors.Control;
            this.txtUserDetailInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUserDetailInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUserDetailInfo.Location = new System.Drawing.Point(3, 19);
            this.txtUserDetailInfo.Name = "txtUserDetailInfo";
            this.txtUserDetailInfo.ReadOnly = true;
            this.txtUserDetailInfo.Size = new System.Drawing.Size(968, 72);
            this.txtUserDetailInfo.TabIndex = 0;
            this.txtUserDetailInfo.Text = "Select a user to view their detailed transaction history for the selected date range.\nUse this view to investigate specific 'Rapid Fire' or 'Ping Pong' events identified in the other tabs.";

            // 
            // grpUserSelection
            // 
            this.grpUserSelection.Controls.Add(this.Form_Analytics_TableLayout_UserSelect);
            this.grpUserSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpUserSelection.Location = new System.Drawing.Point(3, 3);
            this.grpUserSelection.Name = "grpUserSelection";
            this.grpUserSelection.Size = new System.Drawing.Size(974, 74);
            this.grpUserSelection.TabIndex = 0;
            this.grpUserSelection.TabStop = false;
            this.grpUserSelection.Text = "Select User";

            // 
            // Form_Analytics_TableLayout_UserSelect
            // 
            this.Form_Analytics_TableLayout_UserSelect.ColumnCount = 3;
            this.Form_Analytics_TableLayout_UserSelect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.Form_Analytics_TableLayout_UserSelect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.Form_Analytics_TableLayout_UserSelect.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Form_Analytics_TableLayout_UserSelect.Controls.Add(this.lblUser, 0, 0);
            this.Form_Analytics_TableLayout_UserSelect.Controls.Add(this.comboUsers, 1, 0);
            this.Form_Analytics_TableLayout_UserSelect.Controls.Add(this.btnLoadUser, 2, 0);
            this.Form_Analytics_TableLayout_UserSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Form_Analytics_TableLayout_UserSelect.Location = new System.Drawing.Point(3, 19);
            this.Form_Analytics_TableLayout_UserSelect.Name = "Form_Analytics_TableLayout_UserSelect";
            this.Form_Analytics_TableLayout_UserSelect.RowCount = 1;
            this.Form_Analytics_TableLayout_UserSelect.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Form_Analytics_TableLayout_UserSelect.Size = new System.Drawing.Size(968, 52);
            this.Form_Analytics_TableLayout_UserSelect.TabIndex = 0;

            // 
            // lblUser
            // 
            this.lblUser.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(44, 18);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(33, 15);
            this.lblUser.TabIndex = 0;
            this.lblUser.Text = "User:";

            // 
            // comboUsers
            // 
            this.comboUsers.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboUsers.FormattingEnabled = true;
            this.comboUsers.Location = new System.Drawing.Point(83, 14);
            this.comboUsers.Name = "comboUsers";
            this.comboUsers.Size = new System.Drawing.Size(194, 23);
            this.comboUsers.TabIndex = 1;

            // 
            // btnLoadUser
            // 
            this.btnLoadUser.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnLoadUser.Location = new System.Drawing.Point(283, 11);
            this.btnLoadUser.Name = "btnLoadUser";
            this.btnLoadUser.Size = new System.Drawing.Size(100, 30);
            this.btnLoadUser.TabIndex = 2;
            this.btnLoadUser.Text = "Load History";
            this.btnLoadUser.UseVisualStyleBackColor = true;
            this.btnLoadUser.Click += new System.EventHandler(this.btnLoadUser_Click);

            // 
            // gridUserHistory
            // 
            this.gridUserHistory.AllowUserToAddRows = false;
            this.gridUserHistory.AllowUserToDeleteRows = false;
            this.gridUserHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridUserHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridUserHistory.Location = new System.Drawing.Point(3, 83);
            this.gridUserHistory.Name = "gridUserHistory";
            this.gridUserHistory.ReadOnly = true;
            this.gridUserHistory.RowTemplate.Height = 25;
            this.gridUserHistory.Size = new System.Drawing.Size(974, 439);
            this.gridUserHistory.TabIndex = 1;

            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.progressBar.Location = new System.Drawing.Point(3, 593);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(994, 4);
            this.progressBar.TabIndex = 1;

            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Location = new System.Drawing.Point(3, 565);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(994, 25);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Ready";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // Form_Analytics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.Form_Analytics_TableLayout_Main);
            this.Name = "Form_Analytics";
            this.Text = "Material Handler Analytics";
            this.Form_Analytics_TableLayout_Main.ResumeLayout(false);
            this.Form_Analytics_TableLayout_Main.PerformLayout();
            this.Form_Analytics_TabControl_Main.ResumeLayout(false);
            this.tabPerformance.ResumeLayout(false);
            this.Form_Analytics_TableLayout_Performance.ResumeLayout(false);
            this.grpPerformanceFilters.ResumeLayout(false);
            this.Form_Analytics_TableLayout_PerfFilters.ResumeLayout(false);
            this.Form_Analytics_TableLayout_PerfFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridPerformance)).EndInit();
            this.tabQuality.ResumeLayout(false);
            this.Form_Analytics_TableLayout_Quality.ResumeLayout(false);
            this.grpQualityFilters.ResumeLayout(false);
            this.Form_Analytics_TableLayout_QualityFilters.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridQuality)).EndInit();
            this.tabUserDetail.ResumeLayout(false);
            this.Form_Analytics_TableLayout_UserDetail.ResumeLayout(false);
            this.grpUserSelection.ResumeLayout(false);
            this.Form_Analytics_TableLayout_UserSelect.ResumeLayout(false);
            this.Form_Analytics_TableLayout_UserSelect.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridUserHistory)).EndInit();
            this.ResumeLayout(false);

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
