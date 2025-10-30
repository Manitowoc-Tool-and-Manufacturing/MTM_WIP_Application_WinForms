namespace MTM_WIP_Application_Winforms.Forms.ViewLogs
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
            tableLayoutMain = new TableLayoutPanel();
            panel1 = new Panel();
            lblStatus = new Label();
            panelEntryDisplay = new Panel();
            txtRawView = new TextBox();
            tableLayoutEntryDisplay = new TableLayoutPanel();
            txtTimestamp = new TextBox();
            lblEntryDetails = new Label();
            lblEntryMessage = new Label();
            lblEntrySource = new Label();
            lblEntryLevel = new Label();
            txtEntryDetails = new TextBox();
            txtLevel = new TextBox();
            txtEntryMessage = new TextBox();
            txtEntrySource = new TextBox();
            lblEntryTimestamp = new Label();
            panelUserSelection = new Panel();
            tableLayoutPanel4 = new TableLayoutPanel();
            lblSelectUser = new Label();
            cmbUsers = new ComboBox();
            btnRefresh = new Button();
            lblUserCount = new Label();
            chkAutoRefresh = new CheckBox();
            chkGroupErrors = new CheckBox();
            panelFileList = new Panel();
            tableLayoutPanel3 = new TableLayoutPanel();
            lstLogFiles = new ListView();
            colFileName = new ColumnHeader();
            colModified = new ColumnHeader();
            colSize = new ColumnHeader();
            lblLogFiles = new Label();
            lblFileCount = new Label();
            panelNavigation = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            lblEntryPosition = new Label();
            btnPrevious = new Button();
            btnNext = new Button();
            btnManagePromptStatus = new Button();
            btnCreatePrompt = new Button();
            btnToggleView = new Button();
            btnOpenPromptFolder = new Button();
            btnGenerateErrorReport = new Button();
            panelFilters = new Panel();
            tableLayoutFilters = new TableLayoutPanel();
            lblStartDate = new Label();
            dtpStartDate = new DateTimePicker();
            lblEndDate = new Label();
            dtpEndDate = new DateTimePicker();
            lblSeverity = new Label();
            panelSeverityChecks = new FlowLayoutPanel();
            chkSeverity1 = new CheckBox();
            chkSeverity2 = new CheckBox();
            chkSeverity3 = new CheckBox();
            chkSeverity4 = new CheckBox();
            btnApplyFilter = new Button();
            btnClearFilters = new Button();
            lblSource = new Label();
            cmbSource = new ComboBox();
            lblSearch = new Label();
            txtSearch = new TextBox();
            panelQuickFilters = new FlowLayoutPanel();
            btnErrorsOnly = new Button();
            btnPerformance = new Button();
            btnToday = new Button();
            tableLayoutMain.SuspendLayout();
            panel1.SuspendLayout();
            panelEntryDisplay.SuspendLayout();
            tableLayoutEntryDisplay.SuspendLayout();
            panelUserSelection.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            panelFileList.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            panelNavigation.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panelFilters.SuspendLayout();
            tableLayoutFilters.SuspendLayout();
            panelSeverityChecks.SuspendLayout();
            panelQuickFilters.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutMain
            // 
            tableLayoutMain.ColumnCount = 1;
            tableLayoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutMain.Controls.Add(panel1, 0, 4);
            tableLayoutMain.Controls.Add(panelEntryDisplay, 0, 3);
            tableLayoutMain.Controls.Add(panelUserSelection, 0, 0);
            tableLayoutMain.Controls.Add(panelFileList, 0, 1);
            tableLayoutMain.Controls.Add(panelNavigation, 0, 2);
            tableLayoutMain.Dock = DockStyle.Fill;
            tableLayoutMain.Location = new Point(0, 0);
            tableLayoutMain.Margin = new Padding(0);
            tableLayoutMain.Name = "tableLayoutMain";
            tableLayoutMain.RowCount = 5;
            tableLayoutMain.RowStyles.Add(new RowStyle());
            tableLayoutMain.RowStyles.Add(new RowStyle());
            tableLayoutMain.RowStyles.Add(new RowStyle());
            tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutMain.RowStyles.Add(new RowStyle());
            tableLayoutMain.Size = new Size(724, 801);
            tableLayoutMain.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(lblStatus);
            panel1.Location = new Point(3, 769);
            panel1.Name = "panel1";
            panel1.Size = new Size(715, 29);
            panel1.TabIndex = 4;
            // 
            // lblStatus
            // 
            lblStatus.Dock = DockStyle.Fill;
            lblStatus.Location = new Point(0, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Padding = new Padding(10, 5, 5, 5);
            lblStatus.Size = new Size(715, 29);
            lblStatus.TabIndex = 3;
            lblStatus.Text = "Ready";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelEntryDisplay
            // 
            panelEntryDisplay.Controls.Add(tableLayoutEntryDisplay);
            panelEntryDisplay.Controls.Add(txtRawView);
            panelEntryDisplay.Dock = DockStyle.Fill;
            panelEntryDisplay.Location = new Point(3, 434);
            panelEntryDisplay.Name = "panelEntryDisplay";
            panelEntryDisplay.Padding = new Padding(10);
            panelEntryDisplay.Size = new Size(718, 329);
            panelEntryDisplay.TabIndex = 2;
            // 
            // txtRawView
            // 
            txtRawView.Dock = DockStyle.Fill;
            txtRawView.Font = new Font("Consolas", 9F);
            txtRawView.Location = new Point(10, 10);
            txtRawView.Margin = new Padding(3, 5, 3, 3);
            txtRawView.Multiline = true;
            txtRawView.Name = "txtRawView";
            txtRawView.ReadOnly = true;
            txtRawView.ScrollBars = ScrollBars.Both;
            txtRawView.Size = new Size(698, 309);
            txtRawView.TabIndex = 3;
            txtRawView.Visible = false;
            // 
            // tableLayoutEntryDisplay
            // 
            tableLayoutEntryDisplay.ColumnCount = 2;
            tableLayoutEntryDisplay.ColumnStyles.Add(new ColumnStyle());
            tableLayoutEntryDisplay.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutEntryDisplay.Controls.Add(txtTimestamp, 1, 0);
            tableLayoutEntryDisplay.Controls.Add(lblEntryDetails, 0, 4);
            tableLayoutEntryDisplay.Controls.Add(lblEntryMessage, 0, 3);
            tableLayoutEntryDisplay.Controls.Add(lblEntrySource, 0, 2);
            tableLayoutEntryDisplay.Controls.Add(lblEntryLevel, 0, 1);
            tableLayoutEntryDisplay.Controls.Add(txtEntryDetails, 1, 4);
            tableLayoutEntryDisplay.Controls.Add(txtLevel, 1, 1);
            tableLayoutEntryDisplay.Controls.Add(txtEntryMessage, 1, 3);
            tableLayoutEntryDisplay.Controls.Add(txtEntrySource, 1, 2);
            tableLayoutEntryDisplay.Controls.Add(lblEntryTimestamp, 0, 0);
            tableLayoutEntryDisplay.Dock = DockStyle.Fill;
            tableLayoutEntryDisplay.Location = new Point(10, 10);
            tableLayoutEntryDisplay.Name = "tableLayoutEntryDisplay";
            tableLayoutEntryDisplay.RowCount = 5;
            tableLayoutEntryDisplay.RowStyles.Add(new RowStyle());
            tableLayoutEntryDisplay.RowStyles.Add(new RowStyle());
            tableLayoutEntryDisplay.RowStyles.Add(new RowStyle());
            tableLayoutEntryDisplay.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            tableLayoutEntryDisplay.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            tableLayoutEntryDisplay.Size = new Size(698, 309);
            tableLayoutEntryDisplay.TabIndex = 10;
            // 
            // txtTimestamp
            // 
            txtTimestamp.Dock = DockStyle.Fill;
            txtTimestamp.Font = new Font("Consolas", 9F);
            txtTimestamp.Location = new Point(79, 5);
            txtTimestamp.Margin = new Padding(3, 5, 3, 3);
            txtTimestamp.Name = "txtTimestamp";
            txtTimestamp.ReadOnly = true;
            txtTimestamp.Size = new Size(616, 22);
            txtTimestamp.TabIndex = 1;
            txtTimestamp.TabStop = false;
            // 
            // lblEntryDetails
            // 
            lblEntryDetails.AutoSize = true;
            lblEntryDetails.Dock = DockStyle.Fill;
            lblEntryDetails.Location = new Point(3, 177);
            lblEntryDetails.Name = "lblEntryDetails";
            lblEntryDetails.Size = new Size(70, 132);
            lblEntryDetails.TabIndex = 8;
            lblEntryDetails.Text = "Details:";
            lblEntryDetails.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblEntryMessage
            // 
            lblEntryMessage.AutoSize = true;
            lblEntryMessage.Dock = DockStyle.Fill;
            lblEntryMessage.Location = new Point(3, 90);
            lblEntryMessage.Name = "lblEntryMessage";
            lblEntryMessage.Size = new Size(70, 87);
            lblEntryMessage.TabIndex = 6;
            lblEntryMessage.Text = "Message:";
            lblEntryMessage.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblEntrySource
            // 
            lblEntrySource.AutoSize = true;
            lblEntrySource.Dock = DockStyle.Fill;
            lblEntrySource.Location = new Point(3, 60);
            lblEntrySource.Name = "lblEntrySource";
            lblEntrySource.Size = new Size(70, 30);
            lblEntrySource.TabIndex = 4;
            lblEntrySource.Text = "Source:";
            lblEntrySource.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblEntryLevel
            // 
            lblEntryLevel.AutoSize = true;
            lblEntryLevel.Dock = DockStyle.Fill;
            lblEntryLevel.Location = new Point(3, 30);
            lblEntryLevel.Name = "lblEntryLevel";
            lblEntryLevel.Size = new Size(70, 30);
            lblEntryLevel.TabIndex = 2;
            lblEntryLevel.Text = "Level:";
            lblEntryLevel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtEntryDetails
            // 
            txtEntryDetails.Dock = DockStyle.Fill;
            txtEntryDetails.Font = new Font("Consolas", 9F);
            txtEntryDetails.Location = new Point(79, 182);
            txtEntryDetails.Margin = new Padding(3, 5, 3, 3);
            txtEntryDetails.Multiline = true;
            txtEntryDetails.Name = "txtEntryDetails";
            txtEntryDetails.ReadOnly = true;
            txtEntryDetails.ScrollBars = ScrollBars.Vertical;
            txtEntryDetails.Size = new Size(616, 124);
            txtEntryDetails.TabIndex = 9;
            txtEntryDetails.TabStop = false;
            // 
            // txtLevel
            // 
            txtLevel.Dock = DockStyle.Fill;
            txtLevel.Font = new Font("Consolas", 9F, FontStyle.Bold);
            txtLevel.Location = new Point(79, 35);
            txtLevel.Margin = new Padding(3, 5, 3, 3);
            txtLevel.Name = "txtLevel";
            txtLevel.ReadOnly = true;
            txtLevel.Size = new Size(616, 22);
            txtLevel.TabIndex = 3;
            txtLevel.TabStop = false;
            // 
            // txtEntryMessage
            // 
            txtEntryMessage.Dock = DockStyle.Fill;
            txtEntryMessage.Font = new Font("Consolas", 9F);
            txtEntryMessage.Location = new Point(79, 95);
            txtEntryMessage.Margin = new Padding(3, 5, 3, 3);
            txtEntryMessage.Multiline = true;
            txtEntryMessage.Name = "txtEntryMessage";
            txtEntryMessage.ReadOnly = true;
            txtEntryMessage.ScrollBars = ScrollBars.Vertical;
            txtEntryMessage.Size = new Size(616, 79);
            txtEntryMessage.TabIndex = 7;
            txtEntryMessage.TabStop = false;
            // 
            // txtEntrySource
            // 
            txtEntrySource.Dock = DockStyle.Fill;
            txtEntrySource.Font = new Font("Consolas", 9F);
            txtEntrySource.Location = new Point(79, 65);
            txtEntrySource.Margin = new Padding(3, 5, 3, 3);
            txtEntrySource.Name = "txtEntrySource";
            txtEntrySource.ReadOnly = true;
            txtEntrySource.Size = new Size(616, 22);
            txtEntrySource.TabIndex = 5;
            txtEntrySource.TabStop = false;
            // 
            // lblEntryTimestamp
            // 
            lblEntryTimestamp.AutoSize = true;
            lblEntryTimestamp.Dock = DockStyle.Fill;
            lblEntryTimestamp.Location = new Point(3, 0);
            lblEntryTimestamp.Name = "lblEntryTimestamp";
            lblEntryTimestamp.Size = new Size(70, 30);
            lblEntryTimestamp.TabIndex = 0;
            lblEntryTimestamp.Text = "Timestamp:";
            lblEntryTimestamp.TextAlign = ContentAlignment.MiddleRight;
            // 
            // panelUserSelection
            // 
            panelUserSelection.Controls.Add(tableLayoutPanel4);
            panelUserSelection.Dock = DockStyle.Fill;
            panelUserSelection.Location = new Point(3, 3);
            panelUserSelection.Margin = new Padding(3, 3, 3, 5);
            panelUserSelection.Name = "panelUserSelection";
            panelUserSelection.Padding = new Padding(10);
            panelUserSelection.Size = new Size(718, 50);
            panelUserSelection.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 7;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel4.Controls.Add(lblSelectUser, 0, 0);
            tableLayoutPanel4.Controls.Add(cmbUsers, 1, 0);
            tableLayoutPanel4.Controls.Add(btnRefresh, 2, 0);
            tableLayoutPanel4.Controls.Add(lblUserCount, 3, 0);
            tableLayoutPanel4.Controls.Add(chkAutoRefresh, 6, 0);
            tableLayoutPanel4.Controls.Add(chkGroupErrors, 5, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(10, 10);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Size = new Size(698, 30);
            tableLayoutPanel4.TabIndex = 6;
            // 
            // lblSelectUser
            // 
            lblSelectUser.AutoSize = true;
            lblSelectUser.Dock = DockStyle.Fill;
            lblSelectUser.Location = new Point(3, 0);
            lblSelectUser.Name = "lblSelectUser";
            lblSelectUser.Size = new Size(67, 30);
            lblSelectUser.TabIndex = 0;
            lblSelectUser.Text = "Select User:";
            lblSelectUser.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cmbUsers
            // 
            cmbUsers.Dock = DockStyle.Fill;
            cmbUsers.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbUsers.FormattingEnabled = true;
            cmbUsers.Location = new Point(76, 3);
            cmbUsers.MinimumSize = new Size(200, 0);
            cmbUsers.Name = "cmbUsers";
            cmbUsers.Size = new Size(215, 23);
            cmbUsers.TabIndex = 1;
            // 
            // btnRefresh
            // 
            btnRefresh.Dock = DockStyle.Fill;
            btnRefresh.Location = new Point(297, 3);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(90, 24);
            btnRefresh.TabIndex = 2;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            // 
            // lblUserCount
            // 
            lblUserCount.AutoSize = true;
            lblUserCount.Dock = DockStyle.Fill;
            lblUserCount.Location = new Point(393, 0);
            lblUserCount.Name = "lblUserCount";
            lblUserCount.Size = new Size(43, 30);
            lblUserCount.TabIndex = 3;
            lblUserCount.Text = "0 users";
            lblUserCount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // chkAutoRefresh
            // 
            chkAutoRefresh.AutoSize = true;
            chkAutoRefresh.Dock = DockStyle.Fill;
            chkAutoRefresh.Location = new Point(601, 3);
            chkAutoRefresh.Name = "chkAutoRefresh";
            chkAutoRefresh.Size = new Size(94, 24);
            chkAutoRefresh.TabIndex = 4;
            chkAutoRefresh.Text = "Auto Refresh";
            chkAutoRefresh.UseVisualStyleBackColor = true;
            // 
            // chkGroupErrors
            // 
            chkGroupErrors.AutoSize = true;
            chkGroupErrors.Dock = DockStyle.Fill;
            chkGroupErrors.Location = new Point(503, 3);
            chkGroupErrors.Name = "chkGroupErrors";
            chkGroupErrors.Size = new Size(92, 24);
            chkGroupErrors.TabIndex = 5;
            chkGroupErrors.Text = "Group Errors";
            chkGroupErrors.UseVisualStyleBackColor = true;
            chkGroupErrors.Visible = false;
            // 
            // panelFileList
            // 
            panelFileList.Controls.Add(tableLayoutPanel3);
            panelFileList.Dock = DockStyle.Fill;
            panelFileList.Location = new Point(3, 61);
            panelFileList.Margin = new Padding(3, 3, 3, 5);
            panelFileList.Name = "panelFileList";
            panelFileList.Padding = new Padding(10);
            panelFileList.Size = new Size(718, 243);
            panelFileList.TabIndex = 1;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 3;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(lblLogFiles, 0, 0);
            tableLayoutPanel3.Controls.Add(lblFileCount, 1, 0);
            tableLayoutPanel3.Controls.Add(lstLogFiles, 0, 1);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(10, 10);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(698, 223);
            tableLayoutPanel3.TabIndex = 6;
            // 
            // lstLogFiles
            // 
            lstLogFiles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstLogFiles.Columns.AddRange(new ColumnHeader[] { colFileName, colModified, colSize });
            tableLayoutPanel3.SetColumnSpan(lstLogFiles, 3);
            lstLogFiles.FullRowSelect = true;
            lstLogFiles.GridLines = true;
            lstLogFiles.Location = new Point(3, 26);
            lstLogFiles.Margin = new Padding(3, 5, 3, 3);
            lstLogFiles.Name = "lstLogFiles";
            lstLogFiles.Size = new Size(692, 194);
            lstLogFiles.TabIndex = 2;
            lstLogFiles.UseCompatibleStateImageBehavior = false;
            lstLogFiles.View = View.Details;
            // 
            // colFileName
            // 
            colFileName.Text = "File Name";
            colFileName.Width = 400;
            // 
            // colModified
            // 
            colModified.Text = "Modified";
            colModified.Width = 180;
            // 
            // colSize
            // 
            colSize.Text = "Size";
            colSize.Width = 100;
            // 
            // lblLogFiles
            // 
            lblLogFiles.AutoSize = true;
            lblLogFiles.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblLogFiles.Location = new Point(3, 3);
            lblLogFiles.Margin = new Padding(3);
            lblLogFiles.Name = "lblLogFiles";
            lblLogFiles.Size = new Size(57, 15);
            lblLogFiles.TabIndex = 0;
            lblLogFiles.Text = "Log Files:";
            // 
            // lblFileCount
            // 
            lblFileCount.AutoSize = true;
            lblFileCount.Location = new Point(66, 3);
            lblFileCount.Margin = new Padding(3);
            lblFileCount.Name = "lblFileCount";
            lblFileCount.Size = new Size(37, 15);
            lblFileCount.TabIndex = 1;
            lblFileCount.Text = "0 files";
            // 
            // panelNavigation
            // 
            panelNavigation.Controls.Add(tableLayoutPanel2);
            panelNavigation.Dock = DockStyle.Fill;
            panelNavigation.Location = new Point(3, 312);
            panelNavigation.Name = "panelNavigation";
            panelNavigation.Size = new Size(718, 116);
            panelNavigation.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.Controls.Add(lblEntryPosition, 1, 0);
            tableLayoutPanel2.Controls.Add(btnPrevious, 0, 0);
            tableLayoutPanel2.Controls.Add(btnNext, 2, 0);
            tableLayoutPanel2.Controls.Add(btnManagePromptStatus, 1, 1);
            tableLayoutPanel2.Controls.Add(btnCreatePrompt, 0, 1);
            tableLayoutPanel2.Controls.Add(btnToggleView, 0, 2);
            tableLayoutPanel2.Controls.Add(btnOpenPromptFolder, 2, 1);
            tableLayoutPanel2.Controls.Add(btnGenerateErrorReport, 2, 2);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel2.Size = new Size(718, 116);
            tableLayoutPanel2.TabIndex = 8;
            // 
            // lblEntryPosition
            // 
            lblEntryPosition.AutoSize = true;
            lblEntryPosition.Dock = DockStyle.Fill;
            lblEntryPosition.Location = new Point(242, 3);
            lblEntryPosition.Margin = new Padding(3);
            lblEntryPosition.Name = "lblEntryPosition";
            lblEntryPosition.Size = new Size(233, 32);
            lblEntryPosition.TabIndex = 1;
            lblEntryPosition.Text = "No entry loaded";
            lblEntryPosition.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnPrevious
            // 
            btnPrevious.Dock = DockStyle.Fill;
            btnPrevious.Location = new Point(3, 3);
            btnPrevious.MinimumSize = new Size(80, 30);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Size = new Size(233, 32);
            btnPrevious.TabIndex = 0;
            btnPrevious.Text = "◄ Previous";
            btnPrevious.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            btnNext.Dock = DockStyle.Fill;
            btnNext.Location = new Point(481, 3);
            btnNext.MinimumSize = new Size(80, 30);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(234, 32);
            btnNext.TabIndex = 2;
            btnNext.Text = "Next ►";
            btnNext.UseVisualStyleBackColor = true;
            // 
            // btnManagePromptStatus
            // 
            btnManagePromptStatus.Dock = DockStyle.Fill;
            btnManagePromptStatus.Location = new Point(242, 41);
            btnManagePromptStatus.MinimumSize = new Size(120, 30);
            btnManagePromptStatus.Name = "btnManagePromptStatus";
            btnManagePromptStatus.Size = new Size(233, 32);
            btnManagePromptStatus.TabIndex = 5;
            btnManagePromptStatus.Text = "Manage Prompt Status";
            btnManagePromptStatus.UseVisualStyleBackColor = true;
            // 
            // btnCreatePrompt
            // 
            btnCreatePrompt.Dock = DockStyle.Fill;
            btnCreatePrompt.Enabled = false;
            btnCreatePrompt.Location = new Point(3, 41);
            btnCreatePrompt.MinimumSize = new Size(100, 30);
            btnCreatePrompt.Name = "btnCreatePrompt";
            btnCreatePrompt.Size = new Size(233, 32);
            btnCreatePrompt.TabIndex = 4;
            btnCreatePrompt.Text = "Create Prompt";
            btnCreatePrompt.UseVisualStyleBackColor = true;
            // 
            // btnToggleView
            // 
            btnToggleView.Dock = DockStyle.Fill;
            btnToggleView.Location = new Point(3, 79);
            btnToggleView.MinimumSize = new Size(100, 30);
            btnToggleView.Name = "btnToggleView";
            btnToggleView.Size = new Size(233, 34);
            btnToggleView.TabIndex = 3;
            btnToggleView.Text = "Show Raw View";
            btnToggleView.UseVisualStyleBackColor = true;
            // 
            // btnOpenPromptFolder
            // 
            btnOpenPromptFolder.Dock = DockStyle.Fill;
            btnOpenPromptFolder.Location = new Point(481, 41);
            btnOpenPromptFolder.MinimumSize = new Size(120, 30);
            btnOpenPromptFolder.Name = "btnOpenPromptFolder";
            btnOpenPromptFolder.Size = new Size(234, 32);
            btnOpenPromptFolder.TabIndex = 7;
            btnOpenPromptFolder.Text = "Open Prompt Fixes Folder";
            btnOpenPromptFolder.UseVisualStyleBackColor = true;
            // 
            // btnGenerateErrorReport
            // 
            btnGenerateErrorReport.Dock = DockStyle.Fill;
            btnGenerateErrorReport.Location = new Point(481, 79);
            btnGenerateErrorReport.MinimumSize = new Size(120, 30);
            btnGenerateErrorReport.Name = "btnGenerateErrorReport";
            btnGenerateErrorReport.Size = new Size(234, 34);
            btnGenerateErrorReport.TabIndex = 6;
            btnGenerateErrorReport.Text = "Generate Error Report";
            btnGenerateErrorReport.UseVisualStyleBackColor = true;
            // 
            // panelFilters
            // 
            panelFilters.Controls.Add(tableLayoutFilters);
            panelFilters.Dock = DockStyle.Top;
            panelFilters.Location = new Point(10, 50);
            panelFilters.Name = "panelFilters";
            panelFilters.Padding = new Padding(5);
            panelFilters.Size = new Size(1174, 90);
            panelFilters.TabIndex = 1;
            // 
            // tableLayoutFilters
            // 
            tableLayoutFilters.ColumnCount = 8;
            tableLayoutFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableLayoutFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            tableLayoutFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
            tableLayoutFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            tableLayoutFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
            tableLayoutFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 85F));
            tableLayoutFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            tableLayoutFilters.Controls.Add(lblStartDate, 0, 0);
            tableLayoutFilters.Controls.Add(dtpStartDate, 1, 0);
            tableLayoutFilters.Controls.Add(lblEndDate, 2, 0);
            tableLayoutFilters.Controls.Add(dtpEndDate, 3, 0);
            tableLayoutFilters.Controls.Add(lblSeverity, 4, 0);
            tableLayoutFilters.Controls.Add(panelSeverityChecks, 5, 0);
            tableLayoutFilters.Controls.Add(btnApplyFilter, 6, 0);
            tableLayoutFilters.Controls.Add(btnClearFilters, 7, 0);
            tableLayoutFilters.Controls.Add(lblSource, 0, 1);
            tableLayoutFilters.Controls.Add(cmbSource, 1, 1);
            tableLayoutFilters.Controls.Add(lblSearch, 2, 1);
            tableLayoutFilters.Controls.Add(txtSearch, 3, 1);
            tableLayoutFilters.Controls.Add(panelQuickFilters, 5, 1);
            tableLayoutFilters.Dock = DockStyle.Fill;
            tableLayoutFilters.Location = new Point(5, 5);
            tableLayoutFilters.Name = "tableLayoutFilters";
            tableLayoutFilters.RowCount = 2;
            tableLayoutFilters.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutFilters.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tableLayoutFilters.Size = new Size(1164, 80);
            tableLayoutFilters.TabIndex = 0;
            // 
            // lblStartDate
            // 
            lblStartDate.Anchor = AnchorStyles.Left;
            lblStartDate.AutoSize = true;
            lblStartDate.Location = new Point(3, 12);
            lblStartDate.Name = "lblStartDate";
            lblStartDate.Size = new Size(61, 15);
            lblStartDate.TabIndex = 0;
            lblStartDate.Text = "Start Date:";
            // 
            // dtpStartDate
            // 
            dtpStartDate.Anchor = AnchorStyles.Left;
            dtpStartDate.Format = DateTimePickerFormat.Short;
            dtpStartDate.Location = new Point(83, 8);
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Size = new Size(120, 23);
            dtpStartDate.TabIndex = 1;
            // 
            // lblEndDate
            // 
            lblEndDate.Anchor = AnchorStyles.Left;
            lblEndDate.AutoSize = true;
            lblEndDate.Location = new Point(213, 12);
            lblEndDate.Name = "lblEndDate";
            lblEndDate.Size = new Size(57, 15);
            lblEndDate.TabIndex = 2;
            lblEndDate.Text = "End Date:";
            // 
            // dtpEndDate
            // 
            dtpEndDate.Anchor = AnchorStyles.Left;
            dtpEndDate.Format = DateTimePickerFormat.Short;
            dtpEndDate.Location = new Point(283, 8);
            dtpEndDate.Name = "dtpEndDate";
            dtpEndDate.Size = new Size(120, 23);
            dtpEndDate.TabIndex = 3;
            // 
            // lblSeverity
            // 
            lblSeverity.Anchor = AnchorStyles.Left;
            lblSeverity.AutoSize = true;
            lblSeverity.Location = new Point(413, 12);
            lblSeverity.Name = "lblSeverity";
            lblSeverity.Size = new Size(51, 15);
            lblSeverity.TabIndex = 4;
            lblSeverity.Text = "Severity:";
            // 
            // panelSeverityChecks
            // 
            panelSeverityChecks.Anchor = AnchorStyles.Left;
            panelSeverityChecks.Controls.Add(chkSeverity1);
            panelSeverityChecks.Controls.Add(chkSeverity2);
            panelSeverityChecks.Controls.Add(chkSeverity3);
            panelSeverityChecks.Controls.Add(chkSeverity4);
            panelSeverityChecks.Location = new Point(483, 3);
            panelSeverityChecks.Name = "panelSeverityChecks";
            panelSeverityChecks.Size = new Size(300, 34);
            panelSeverityChecks.TabIndex = 5;
            // 
            // chkSeverity1
            // 
            chkSeverity1.AutoSize = true;
            chkSeverity1.Checked = true;
            chkSeverity1.CheckState = CheckState.Checked;
            chkSeverity1.Location = new Point(3, 3);
            chkSeverity1.Name = "chkSeverity1";
            chkSeverity1.Size = new Size(52, 19);
            chkSeverity1.TabIndex = 0;
            chkSeverity1.Text = "LOW";
            chkSeverity1.UseVisualStyleBackColor = true;
            // 
            // chkSeverity2
            // 
            chkSeverity2.AutoSize = true;
            chkSeverity2.Checked = true;
            chkSeverity2.CheckState = CheckState.Checked;
            chkSeverity2.Location = new Point(61, 3);
            chkSeverity2.Name = "chkSeverity2";
            chkSeverity2.Size = new Size(73, 19);
            chkSeverity2.TabIndex = 1;
            chkSeverity2.Text = "MEDIUM";
            chkSeverity2.UseVisualStyleBackColor = true;
            // 
            // chkSeverity3
            // 
            chkSeverity3.AutoSize = true;
            chkSeverity3.Checked = true;
            chkSeverity3.CheckState = CheckState.Checked;
            chkSeverity3.Location = new Point(140, 3);
            chkSeverity3.Name = "chkSeverity3";
            chkSeverity3.Size = new Size(55, 19);
            chkSeverity3.TabIndex = 2;
            chkSeverity3.Text = "HIGH";
            chkSeverity3.UseVisualStyleBackColor = true;
            // 
            // chkSeverity4
            // 
            chkSeverity4.AutoSize = true;
            chkSeverity4.Checked = true;
            chkSeverity4.CheckState = CheckState.Checked;
            chkSeverity4.Location = new Point(201, 3);
            chkSeverity4.Name = "chkSeverity4";
            chkSeverity4.Size = new Size(55, 19);
            chkSeverity4.TabIndex = 3;
            chkSeverity4.Text = "DATA";
            chkSeverity4.UseVisualStyleBackColor = true;
            // 
            // btnApplyFilter
            // 
            btnApplyFilter.Anchor = AnchorStyles.None;
            btnApplyFilter.Location = new Point(994, 5);
            btnApplyFilter.MinimumSize = new Size(75, 30);
            btnApplyFilter.Name = "btnApplyFilter";
            btnApplyFilter.Size = new Size(75, 30);
            btnApplyFilter.TabIndex = 6;
            btnApplyFilter.Text = "Apply";
            btnApplyFilter.UseVisualStyleBackColor = true;
            // 
            // btnClearFilters
            // 
            btnClearFilters.Anchor = AnchorStyles.None;
            btnClearFilters.Location = new Point(1079, 5);
            btnClearFilters.MinimumSize = new Size(75, 30);
            btnClearFilters.Name = "btnClearFilters";
            btnClearFilters.Size = new Size(80, 30);
            btnClearFilters.TabIndex = 7;
            btnClearFilters.Text = "Clear";
            btnClearFilters.UseVisualStyleBackColor = true;
            // 
            // lblSource
            // 
            lblSource.Anchor = AnchorStyles.Left;
            lblSource.AutoSize = true;
            lblSource.Location = new Point(3, 52);
            lblSource.Name = "lblSource";
            lblSource.Size = new Size(46, 15);
            lblSource.TabIndex = 8;
            lblSource.Text = "Source:";
            // 
            // cmbSource
            // 
            cmbSource.Anchor = AnchorStyles.Left;
            cmbSource.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSource.FormattingEnabled = true;
            cmbSource.Location = new Point(83, 48);
            cmbSource.Name = "cmbSource";
            cmbSource.Size = new Size(120, 23);
            cmbSource.TabIndex = 9;
            // 
            // lblSearch
            // 
            lblSearch.Anchor = AnchorStyles.Left;
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(213, 52);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(45, 15);
            lblSearch.TabIndex = 10;
            lblSearch.Text = "Search:";
            // 
            // txtSearch
            // 
            txtSearch.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutFilters.SetColumnSpan(txtSearch, 2);
            txtSearch.Location = new Point(283, 48);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search all fields...";
            txtSearch.Size = new Size(194, 23);
            txtSearch.TabIndex = 11;
            // 
            // panelQuickFilters
            // 
            panelQuickFilters.Anchor = AnchorStyles.Left;
            panelQuickFilters.Controls.Add(btnErrorsOnly);
            panelQuickFilters.Controls.Add(btnPerformance);
            panelQuickFilters.Controls.Add(btnToday);
            panelQuickFilters.Location = new Point(483, 43);
            panelQuickFilters.Name = "panelQuickFilters";
            panelQuickFilters.Size = new Size(300, 34);
            panelQuickFilters.TabIndex = 12;
            // 
            // btnErrorsOnly
            // 
            btnErrorsOnly.Location = new Point(3, 3);
            btnErrorsOnly.MinimumSize = new Size(75, 28);
            btnErrorsOnly.Name = "btnErrorsOnly";
            btnErrorsOnly.Size = new Size(90, 28);
            btnErrorsOnly.TabIndex = 0;
            btnErrorsOnly.Text = "Errors Only";
            btnErrorsOnly.UseVisualStyleBackColor = true;
            // 
            // btnPerformance
            // 
            btnPerformance.Location = new Point(99, 3);
            btnPerformance.MinimumSize = new Size(75, 28);
            btnPerformance.Name = "btnPerformance";
            btnPerformance.Size = new Size(95, 28);
            btnPerformance.TabIndex = 1;
            btnPerformance.Text = "Performance";
            btnPerformance.UseVisualStyleBackColor = true;
            // 
            // btnToday
            // 
            btnToday.Location = new Point(200, 3);
            btnToday.MinimumSize = new Size(75, 28);
            btnToday.Name = "btnToday";
            btnToday.Size = new Size(75, 28);
            btnToday.TabIndex = 2;
            btnToday.Text = "Today";
            btnToday.UseVisualStyleBackColor = true;
            // 
            // ViewApplicationLogsForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(724, 801);
            Controls.Add(tableLayoutMain);
            MaximizeBox = false;
            MaximumSize = new Size(740, 840);
            MinimizeBox = false;
            MinimumSize = new Size(740, 840);
            Name = "ViewApplicationLogsForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "View Application Logs";
            tableLayoutMain.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panelEntryDisplay.ResumeLayout(false);
            panelEntryDisplay.PerformLayout();
            tableLayoutEntryDisplay.ResumeLayout(false);
            tableLayoutEntryDisplay.PerformLayout();
            panelUserSelection.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            panelFileList.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            panelNavigation.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            panelFilters.ResumeLayout(false);
            tableLayoutFilters.ResumeLayout(false);
            tableLayoutFilters.PerformLayout();
            panelSeverityChecks.ResumeLayout(false);
            panelSeverityChecks.PerformLayout();
            panelQuickFilters.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutMain;
        private System.Windows.Forms.Panel panelUserSelection;
        private System.Windows.Forms.Label lblSelectUser;
        private System.Windows.Forms.ComboBox cmbUsers;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.CheckBox chkAutoRefresh;
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
        private System.Windows.Forms.Button btnCreatePrompt;
        private System.Windows.Forms.Button btnManagePromptStatus;
        private System.Windows.Forms.Button btnGenerateErrorReport;
        private System.Windows.Forms.Button btnOpenPromptFolder;
        private System.Windows.Forms.Label lblEntryTimestamp;
        private System.Windows.Forms.TextBox txtTimestamp;
        private System.Windows.Forms.Label lblEntryLevel;
        private System.Windows.Forms.TextBox txtLevel;
        private System.Windows.Forms.Label lblEntrySource;
        private System.Windows.Forms.TextBox txtEntrySource;
        private System.Windows.Forms.Label lblEntryMessage;
        private System.Windows.Forms.TextBox txtEntryMessage;
        private System.Windows.Forms.Label lblEntryDetails;
        private System.Windows.Forms.TextBox txtEntryDetails;
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
        private System.Windows.Forms.CheckBox chkGroupErrors;
        private TableLayoutPanel tableLayoutEntryDisplay;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel4;
        private Panel panel1;
    }
}
