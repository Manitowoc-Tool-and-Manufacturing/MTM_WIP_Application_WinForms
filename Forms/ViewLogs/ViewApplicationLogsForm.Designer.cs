namespace MTM_Inventory_Application.Forms.ViewLogs
{
    partial class ViewApplicationLogsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutMain = new System.Windows.Forms.TableLayoutPanel();
            this.panelUserSelection = new System.Windows.Forms.Panel();
            this.lblUserCount = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cmbUsers = new System.Windows.Forms.ComboBox();
            this.lblSelectUser = new System.Windows.Forms.Label();
            this.panelFileList = new System.Windows.Forms.Panel();
            this.lstLogFiles = new System.Windows.Forms.ListView();
            this.colFileName = new System.Windows.Forms.ColumnHeader();
            this.colModified = new System.Windows.Forms.ColumnHeader();
            this.colSize = new System.Windows.Forms.ColumnHeader();
            this.lblFileCount = new System.Windows.Forms.Label();
            this.lblLogFiles = new System.Windows.Forms.Label();
            this.panelEntryDisplay = new System.Windows.Forms.Panel();
            this.panelNavigation = new System.Windows.Forms.Panel();
            this.btnToggleView = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblEntryPosition = new System.Windows.Forms.Label();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.txtRawView = new System.Windows.Forms.TextBox();
            this.txtEntryDisplay = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panelFilters = new System.Windows.Forms.Panel();
            this.tableLayoutFilters = new System.Windows.Forms.TableLayoutPanel();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblSeverity = new System.Windows.Forms.Label();
            this.panelSeverityChecks = new System.Windows.Forms.FlowLayoutPanel();
            this.chkSeverity1 = new System.Windows.Forms.CheckBox();
            this.chkSeverity2 = new System.Windows.Forms.CheckBox();
            this.chkSeverity3 = new System.Windows.Forms.CheckBox();
            this.chkSeverity4 = new System.Windows.Forms.CheckBox();
            this.lblSource = new System.Windows.Forms.Label();
            this.cmbSource = new System.Windows.Forms.ComboBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnApplyFilter = new System.Windows.Forms.Button();
            this.btnClearFilters = new System.Windows.Forms.Button();
            this.panelQuickFilters = new System.Windows.Forms.FlowLayoutPanel();
            this.btnErrorsOnly = new System.Windows.Forms.Button();
            this.btnPerformance = new System.Windows.Forms.Button();
            this.btnToday = new System.Windows.Forms.Button();
            this.tableLayoutMain.SuspendLayout();
            this.panelUserSelection.SuspendLayout();
            this.panelFileList.SuspendLayout();
            this.panelEntryDisplay.SuspendLayout();
            this.panelNavigation.SuspendLayout();
            this.panelFilters.SuspendLayout();
            this.tableLayoutFilters.SuspendLayout();
            this.panelSeverityChecks.SuspendLayout();
            this.panelQuickFilters.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutMain
            // 
            this.tableLayoutMain.ColumnCount = 1;
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutMain.Controls.Add(this.panelUserSelection, 0, 0);
            this.tableLayoutMain.Controls.Add(this.panelFileList, 0, 1);
            this.tableLayoutMain.Controls.Add(this.panelEntryDisplay, 0, 2);
            this.tableLayoutMain.Controls.Add(this.lblStatus, 0, 3);
            this.tableLayoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutMain.Name = "tableLayoutMain";
            this.tableLayoutMain.RowCount = 4;
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutMain.Size = new System.Drawing.Size(1200, 800);
            this.tableLayoutMain.TabIndex = 0;
            // 
            // panelUserSelection
            // 
            this.panelUserSelection.Controls.Add(this.lblUserCount);
            this.panelUserSelection.Controls.Add(this.btnRefresh);
            this.panelUserSelection.Controls.Add(this.cmbUsers);
            this.panelUserSelection.Controls.Add(this.lblSelectUser);
            this.panelUserSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelUserSelection.Location = new System.Drawing.Point(3, 3);
            this.panelUserSelection.Name = "panelUserSelection";
            this.panelUserSelection.Padding = new System.Windows.Forms.Padding(10);
            this.panelUserSelection.Size = new System.Drawing.Size(1194, 54);
            this.panelUserSelection.TabIndex = 0;
            // 
            // lblUserCount
            // 
            this.lblUserCount.AutoSize = true;
            this.lblUserCount.Location = new System.Drawing.Point(450, 18);
            this.lblUserCount.Name = "lblUserCount";
            this.lblUserCount.Size = new System.Drawing.Size(45, 15);
            this.lblUserCount.TabIndex = 3;
            this.lblUserCount.Text = "0 users";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(350, 13);
            this.btnRefresh.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 30);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // cmbUsers
            // 
            this.cmbUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUsers.FormattingEnabled = true;
            this.cmbUsers.Location = new System.Drawing.Point(100, 15);
            this.cmbUsers.MinimumSize = new System.Drawing.Size(200, 0);
            this.cmbUsers.Name = "cmbUsers";
            this.cmbUsers.Size = new System.Drawing.Size(240, 23);
            this.cmbUsers.TabIndex = 1;
            // 
            // lblSelectUser
            // 
            this.lblSelectUser.AutoSize = true;
            this.lblSelectUser.Location = new System.Drawing.Point(13, 18);
            this.lblSelectUser.Name = "lblSelectUser";
            this.lblSelectUser.Size = new System.Drawing.Size(70, 15);
            this.lblSelectUser.TabIndex = 0;
            this.lblSelectUser.Text = "Select User:";
            // 
            // panelFileList
            // 
            this.panelFileList.Controls.Add(this.lstLogFiles);
            this.panelFileList.Controls.Add(this.lblFileCount);
            this.panelFileList.Controls.Add(this.lblLogFiles);
            this.panelFileList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFileList.Location = new System.Drawing.Point(3, 63);
            this.panelFileList.Name = "panelFileList";
            this.panelFileList.Padding = new System.Windows.Forms.Padding(10);
            this.panelFileList.Size = new System.Drawing.Size(1194, 349);
            this.panelFileList.TabIndex = 1;
            // 
            // lstLogFiles
            // 
            this.lstLogFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstLogFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFileName,
            this.colModified,
            this.colSize});
            this.lstLogFiles.FullRowSelect = true;
            this.lstLogFiles.GridLines = true;
            this.lstLogFiles.HideSelection = false;
            this.lstLogFiles.Location = new System.Drawing.Point(13, 40);
            this.lstLogFiles.MinimumSize = new System.Drawing.Size(400, 200);
            this.lstLogFiles.Name = "lstLogFiles";
            this.lstLogFiles.Size = new System.Drawing.Size(1168, 296);
            this.lstLogFiles.TabIndex = 2;
            this.lstLogFiles.UseCompatibleStateImageBehavior = false;
            this.lstLogFiles.View = System.Windows.Forms.View.Details;
            // 
            // colFileName
            // 
            this.colFileName.Text = "File Name";
            this.colFileName.Width = 400;
            // 
            // colModified
            // 
            this.colModified.Text = "Modified";
            this.colModified.Width = 180;
            // 
            // colSize
            // 
            this.colSize.Text = "Size";
            this.colSize.Width = 100;
            // 
            // lblFileCount
            // 
            this.lblFileCount.AutoSize = true;
            this.lblFileCount.Location = new System.Drawing.Point(100, 15);
            this.lblFileCount.Name = "lblFileCount";
            this.lblFileCount.Size = new System.Drawing.Size(39, 15);
            this.lblFileCount.TabIndex = 1;
            this.lblFileCount.Text = "0 files";
            // 
            // lblLogFiles
            // 
            this.lblLogFiles.AutoSize = true;
            this.lblLogFiles.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLogFiles.Location = new System.Drawing.Point(13, 15);
            this.lblLogFiles.Name = "lblLogFiles";
            this.lblLogFiles.Size = new System.Drawing.Size(59, 15);
            this.lblLogFiles.TabIndex = 0;
            this.lblLogFiles.Text = "Log Files:";
            // 
            // panelEntryDisplay
            // 
            this.panelEntryDisplay.Controls.Add(this.txtRawView);
            this.panelEntryDisplay.Controls.Add(this.txtEntryDisplay);
            this.panelEntryDisplay.Controls.Add(this.panelFilters);
            this.panelEntryDisplay.Controls.Add(this.panelNavigation);
            this.panelEntryDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEntryDisplay.Location = new System.Drawing.Point(3, 418);
            this.panelEntryDisplay.Name = "panelEntryDisplay";
            this.panelEntryDisplay.Padding = new System.Windows.Forms.Padding(10);
            this.panelEntryDisplay.Size = new System.Drawing.Size(1194, 349);
            this.panelEntryDisplay.TabIndex = 2;
            // 
            // panelNavigation
            // 
            this.panelNavigation.Controls.Add(this.btnToggleView);
            this.panelNavigation.Controls.Add(this.btnPrevious);
            this.panelNavigation.Controls.Add(this.lblEntryPosition);
            this.panelNavigation.Controls.Add(this.btnNext);
            this.panelNavigation.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelNavigation.Location = new System.Drawing.Point(10, 10);
            this.panelNavigation.Name = "panelNavigation";
            this.panelNavigation.Size = new System.Drawing.Size(1174, 40);
            this.panelNavigation.TabIndex = 0;
            // 
            // btnPrevious
            // 
            this.btnPrevious.Location = new System.Drawing.Point(3, 5);
            this.btnPrevious.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(90, 30);
            this.btnPrevious.TabIndex = 0;
            this.btnPrevious.Text = "◄ Previous";
            this.btnPrevious.UseVisualStyleBackColor = true;
            // 
            // lblEntryPosition
            // 
            this.lblEntryPosition.AutoSize = true;
            this.lblEntryPosition.Location = new System.Drawing.Point(100, 12);
            this.lblEntryPosition.Name = "lblEntryPosition";
            this.lblEntryPosition.Size = new System.Drawing.Size(73, 15);
            this.lblEntryPosition.TabIndex = 1;
            this.lblEntryPosition.Text = "No entry loaded";
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(200, 5);
            this.btnNext.MinimumSize = new System.Drawing.Size(80, 30);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(90, 30);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "Next ►";
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // btnToggleView
            // 
            this.btnToggleView.Location = new System.Drawing.Point(300, 5);
            this.btnToggleView.MinimumSize = new System.Drawing.Size(100, 30);
            this.btnToggleView.Name = "btnToggleView";
            this.btnToggleView.Size = new System.Drawing.Size(120, 30);
            this.btnToggleView.TabIndex = 3;
            this.btnToggleView.Text = "Show Raw View";
            this.btnToggleView.UseVisualStyleBackColor = true;
            // 
            // panelFilters
            // 
            this.panelFilters.Controls.Add(this.tableLayoutFilters);
            this.panelFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilters.Location = new System.Drawing.Point(10, 50);
            this.panelFilters.Name = "panelFilters";
            this.panelFilters.Padding = new System.Windows.Forms.Padding(5);
            this.panelFilters.Size = new System.Drawing.Size(1174, 90);
            this.panelFilters.TabIndex = 1;
            //
            // tableLayoutFilters
            // 
            this.tableLayoutFilters.ColumnCount = 8;
            this.tableLayoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tableLayoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutFilters.Controls.Add(this.lblStartDate, 0, 0);
            this.tableLayoutFilters.Controls.Add(this.dtpStartDate, 1, 0);
            this.tableLayoutFilters.Controls.Add(this.lblEndDate, 2, 0);
            this.tableLayoutFilters.Controls.Add(this.dtpEndDate, 3, 0);
            this.tableLayoutFilters.Controls.Add(this.lblSeverity, 4, 0);
            this.tableLayoutFilters.Controls.Add(this.panelSeverityChecks, 5, 0);
            this.tableLayoutFilters.Controls.Add(this.btnApplyFilter, 6, 0);
            this.tableLayoutFilters.Controls.Add(this.btnClearFilters, 7, 0);
            this.tableLayoutFilters.Controls.Add(this.lblSource, 0, 1);
            this.tableLayoutFilters.Controls.Add(this.cmbSource, 1, 1);
            this.tableLayoutFilters.Controls.Add(this.lblSearch, 2, 1);
            this.tableLayoutFilters.Controls.Add(this.txtSearch, 3, 1);
            this.tableLayoutFilters.Controls.Add(this.panelQuickFilters, 5, 1);
            this.tableLayoutFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutFilters.Location = new System.Drawing.Point(5, 5);
            this.tableLayoutFilters.Name = "tableLayoutFilters";
            this.tableLayoutFilters.RowCount = 2;
            this.tableLayoutFilters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutFilters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutFilters.Size = new System.Drawing.Size(1164, 80);
            this.tableLayoutFilters.TabIndex = 0;
            // 
            // lblStartDate
            // 
            this.lblStartDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Location = new System.Drawing.Point(3, 12);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(61, 15);
            this.lblStartDate.TabIndex = 0;
            this.lblStartDate.Text = "Start Date:";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(83, 8);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(120, 23);
            this.dtpStartDate.TabIndex = 1;
            // 
            // lblEndDate
            // 
            this.lblEndDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Location = new System.Drawing.Point(213, 12);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(57, 15);
            this.lblEndDate.TabIndex = 2;
            this.lblEndDate.Text = "End Date:";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(283, 8);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(120, 23);
            this.dtpEndDate.TabIndex = 3;
            // 
            // lblSeverity
            // 
            this.lblSeverity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSeverity.AutoSize = true;
            this.lblSeverity.Location = new System.Drawing.Point(413, 12);
            this.lblSeverity.Name = "lblSeverity";
            this.lblSeverity.Size = new System.Drawing.Size(52, 15);
            this.lblSeverity.TabIndex = 4;
            this.lblSeverity.Text = "Severity:";
            // 
            // panelSeverityChecks
            // 
            this.panelSeverityChecks.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.panelSeverityChecks.Controls.Add(this.chkSeverity1);
            this.panelSeverityChecks.Controls.Add(this.chkSeverity2);
            this.panelSeverityChecks.Controls.Add(this.chkSeverity3);
            this.panelSeverityChecks.Controls.Add(this.chkSeverity4);
            this.panelSeverityChecks.Location = new System.Drawing.Point(483, 3);
            this.panelSeverityChecks.Name = "panelSeverityChecks";
            this.panelSeverityChecks.Size = new System.Drawing.Size(300, 34);
            this.panelSeverityChecks.TabIndex = 5;
            // 
            // chkSeverity1
            // 
            this.chkSeverity1.AutoSize = true;
            this.chkSeverity1.Checked = true;
            this.chkSeverity1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSeverity1.Location = new System.Drawing.Point(3, 3);
            this.chkSeverity1.Name = "chkSeverity1";
            this.chkSeverity1.Size = new System.Drawing.Size(53, 19);
            this.chkSeverity1.TabIndex = 0;
            this.chkSeverity1.Text = "LOW";
            this.chkSeverity1.UseVisualStyleBackColor = true;
            // 
            // chkSeverity2
            // 
            this.chkSeverity2.AutoSize = true;
            this.chkSeverity2.Checked = true;
            this.chkSeverity2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSeverity2.Location = new System.Drawing.Point(62, 3);
            this.chkSeverity2.Name = "chkSeverity2";
            this.chkSeverity2.Size = new System.Drawing.Size(77, 19);
            this.chkSeverity2.TabIndex = 1;
            this.chkSeverity2.Text = "MEDIUM";
            this.chkSeverity2.UseVisualStyleBackColor = true;
            // 
            // chkSeverity3
            // 
            this.chkSeverity3.AutoSize = true;
            this.chkSeverity3.Checked = true;
            this.chkSeverity3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSeverity3.Location = new System.Drawing.Point(145, 3);
            this.chkSeverity3.Name = "chkSeverity3";
            this.chkSeverity3.Size = new System.Drawing.Size(56, 19);
            this.chkSeverity3.TabIndex = 2;
            this.chkSeverity3.Text = "HIGH";
            this.chkSeverity3.UseVisualStyleBackColor = true;
            // 
            // chkSeverity4
            // 
            this.chkSeverity4.AutoSize = true;
            this.chkSeverity4.Checked = true;
            this.chkSeverity4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSeverity4.Location = new System.Drawing.Point(207, 3);
            this.chkSeverity4.Name = "chkSeverity4";
            this.chkSeverity4.Size = new System.Drawing.Size(55, 19);
            this.chkSeverity4.TabIndex = 3;
            this.chkSeverity4.Text = "DATA";
            this.chkSeverity4.UseVisualStyleBackColor = true;
            // 
            // btnApplyFilter
            // 
            this.btnApplyFilter.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnApplyFilter.Location = new System.Drawing.Point(992, 5);
            this.btnApplyFilter.MinimumSize = new System.Drawing.Size(75, 30);
            this.btnApplyFilter.Name = "btnApplyFilter";
            this.btnApplyFilter.Size = new System.Drawing.Size(75, 30);
            this.btnApplyFilter.TabIndex = 6;
            this.btnApplyFilter.Text = "Apply";
            this.btnApplyFilter.UseVisualStyleBackColor = true;
            // 
            // btnClearFilters
            // 
            this.btnClearFilters.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClearFilters.Location = new System.Drawing.Point(1077, 5);
            this.btnClearFilters.MinimumSize = new System.Drawing.Size(75, 30);
            this.btnClearFilters.Name = "btnClearFilters";
            this.btnClearFilters.Size = new System.Drawing.Size(80, 30);
            this.btnClearFilters.TabIndex = 7;
            this.btnClearFilters.Text = "Clear";
            this.btnClearFilters.UseVisualStyleBackColor = true;
            // 
            // lblSource
            // 
            this.lblSource.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSource.AutoSize = true;
            this.lblSource.Location = new System.Drawing.Point(3, 52);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(46, 15);
            this.lblSource.TabIndex = 8;
            this.lblSource.Text = "Source:";
            // 
            // cmbSource
            // 
            this.cmbSource.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cmbSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSource.FormattingEnabled = true;
            this.cmbSource.Location = new System.Drawing.Point(83, 48);
            this.cmbSource.Name = "cmbSource";
            this.cmbSource.Size = new System.Drawing.Size(120, 23);
            this.cmbSource.TabIndex = 9;
            // 
            // lblSearch
            // 
            this.lblSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(213, 52);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(45, 15);
            this.lblSearch.TabIndex = 10;
            this.lblSearch.Text = "Search:";
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutFilters.SetColumnSpan(this.txtSearch, 2);
            this.txtSearch.Location = new System.Drawing.Point(283, 48);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderText = "Search all fields...";
            this.txtSearch.Size = new System.Drawing.Size(194, 23);
            this.txtSearch.TabIndex = 11;
            // 
            // panelQuickFilters
            // 
            this.panelQuickFilters.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.panelQuickFilters.Controls.Add(this.btnErrorsOnly);
            this.panelQuickFilters.Controls.Add(this.btnPerformance);
            this.panelQuickFilters.Controls.Add(this.btnToday);
            this.panelQuickFilters.Location = new System.Drawing.Point(483, 43);
            this.panelQuickFilters.Name = "panelQuickFilters";
            this.panelQuickFilters.Size = new System.Drawing.Size(300, 34);
            this.panelQuickFilters.TabIndex = 12;
            // 
            // btnErrorsOnly
            // 
            this.btnErrorsOnly.Location = new System.Drawing.Point(3, 3);
            this.btnErrorsOnly.MinimumSize = new System.Drawing.Size(75, 28);
            this.btnErrorsOnly.Name = "btnErrorsOnly";
            this.btnErrorsOnly.Size = new System.Drawing.Size(90, 28);
            this.btnErrorsOnly.TabIndex = 0;
            this.btnErrorsOnly.Text = "Errors Only";
            this.btnErrorsOnly.UseVisualStyleBackColor = true;
            // 
            // btnPerformance
            // 
            this.btnPerformance.Location = new System.Drawing.Point(99, 3);
            this.btnPerformance.MinimumSize = new System.Drawing.Size(75, 28);
            this.btnPerformance.Name = "btnPerformance";
            this.btnPerformance.Size = new System.Drawing.Size(95, 28);
            this.btnPerformance.TabIndex = 1;
            this.btnPerformance.Text = "Performance";
            this.btnPerformance.UseVisualStyleBackColor = true;
            // 
            // btnToday
            // 
            this.btnToday.Location = new System.Drawing.Point(200, 3);
            this.btnToday.MinimumSize = new System.Drawing.Size(75, 28);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(75, 28);
            this.btnToday.TabIndex = 2;
            this.btnToday.Text = "Today";
            this.btnToday.UseVisualStyleBackColor = true;
            // 
            // txtEntryDisplay
            // 
            this.txtEntryDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEntryDisplay.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtEntryDisplay.Location = new System.Drawing.Point(13, 146);
            this.txtEntryDisplay.Multiline = true;
            this.txtEntryDisplay.Name = "txtEntryDisplay";
            this.txtEntryDisplay.ReadOnly = true;
            this.txtEntryDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEntryDisplay.Size = new System.Drawing.Size(1168, 190);
            this.txtEntryDisplay.TabIndex = 2;
            this.txtEntryDisplay.Text = "Select a file to view entries";
            this.txtEntryDisplay.WordWrap = true;
            // 
            // txtRawView
            // 
            this.txtRawView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRawView.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtRawView.Location = new System.Drawing.Point(13, 146);
            this.txtRawView.Multiline = true;
            this.txtRawView.Name = "txtRawView";
            this.txtRawView.ReadOnly = true;
            this.txtRawView.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRawView.Size = new System.Drawing.Size(1168, 190);
            this.txtRawView.TabIndex = 3;
            this.txtRawView.Visible = false;
            this.txtRawView.WordWrap = true;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Location = new System.Drawing.Point(3, 770);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Padding = new System.Windows.Forms.Padding(10, 5, 5, 5);
            this.lblStatus.Size = new System.Drawing.Size(1194, 30);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Ready";
            // 
            // ViewApplicationLogsForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.tableLayoutMain);
            this.MinimumSize = new System.Drawing.Size(1200, 800);
            this.Name = "ViewApplicationLogsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "View Application Logs";
            this.tableLayoutMain.ResumeLayout(false);
            this.tableLayoutMain.PerformLayout();
            this.panelUserSelection.ResumeLayout(false);
            this.panelUserSelection.PerformLayout();
            this.panelFileList.ResumeLayout(false);
            this.panelFileList.PerformLayout();
            this.panelEntryDisplay.ResumeLayout(false);
            this.panelEntryDisplay.PerformLayout();
            this.panelNavigation.ResumeLayout(false);
            this.panelNavigation.PerformLayout();
            this.panelFilters.ResumeLayout(false);
            this.tableLayoutFilters.ResumeLayout(false);
            this.tableLayoutFilters.PerformLayout();
            this.panelSeverityChecks.ResumeLayout(false);
            this.panelSeverityChecks.PerformLayout();
            this.panelQuickFilters.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutMain;
        private System.Windows.Forms.Panel panelUserSelection;
        private System.Windows.Forms.Label lblSelectUser;
        private System.Windows.Forms.ComboBox cmbUsers;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblUserCount;
        private System.Windows.Forms.Panel panelFileList;
        private System.Windows.Forms.Label lblLogFiles;
        private System.Windows.Forms.ListView lstLogFiles;
        private System.Windows.Forms.ColumnHeader colFileName;
        private System.Windows.Forms.ColumnHeader colModified;
        private System.Windows.Forms.ColumnHeader colSize;
        private System.Windows.Forms.Label lblFileCount;
        private System.Windows.Forms.Panel panelEntryDisplay;
        private System.Windows.Forms.Panel panelNavigation;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Label lblEntryPosition;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnToggleView;
        private System.Windows.Forms.TextBox txtEntryDisplay;
        private System.Windows.Forms.TextBox txtRawView;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel panelFilters;
        private System.Windows.Forms.TableLayoutPanel tableLayoutFilters;
        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblSeverity;
        private System.Windows.Forms.FlowLayoutPanel panelSeverityChecks;
        private System.Windows.Forms.CheckBox chkSeverity1;
        private System.Windows.Forms.CheckBox chkSeverity2;
        private System.Windows.Forms.CheckBox chkSeverity3;
        private System.Windows.Forms.CheckBox chkSeverity4;
        private System.Windows.Forms.Label lblSource;
        private System.Windows.Forms.ComboBox cmbSource;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnApplyFilter;
        private System.Windows.Forms.Button btnClearFilters;
        private System.Windows.Forms.FlowLayoutPanel panelQuickFilters;
        private System.Windows.Forms.Button btnErrorsOnly;
        private System.Windows.Forms.Button btnPerformance;
        private System.Windows.Forms.Button btnToday;
    }
}
