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
            this.colEntries = new System.Windows.Forms.ColumnHeader();
            this.lblFileCount = new System.Windows.Forms.Label();
            this.lblLogFiles = new System.Windows.Forms.Label();
            this.panelEntryDisplay = new System.Windows.Forms.Panel();
            this.panelNavigation = new System.Windows.Forms.Panel();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblEntryPosition = new System.Windows.Forms.Label();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.txtEntryDisplay = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.tableLayoutMain.SuspendLayout();
            this.panelUserSelection.SuspendLayout();
            this.panelFileList.SuspendLayout();
            this.panelEntryDisplay.SuspendLayout();
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
            this.colSize,
            this.colEntries});
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
            // colEntries
            // 
            this.colEntries.Text = "Entries";
            this.colEntries.Width = 80;
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
            this.panelEntryDisplay.Controls.Add(this.txtEntryDisplay);
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
            // txtEntryDisplay
            // 
            this.txtEntryDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEntryDisplay.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtEntryDisplay.Location = new System.Drawing.Point(13, 56);
            this.txtEntryDisplay.Multiline = true;
            this.txtEntryDisplay.Name = "txtEntryDisplay";
            this.txtEntryDisplay.ReadOnly = true;
            this.txtEntryDisplay.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEntryDisplay.Size = new System.Drawing.Size(1168, 280);
            this.txtEntryDisplay.TabIndex = 2;
            this.txtEntryDisplay.Text = "Select a file to view entries";
            this.txtEntryDisplay.WordWrap = true;
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
        private System.Windows.Forms.ColumnHeader colEntries;
        private System.Windows.Forms.Label lblFileCount;
        private System.Windows.Forms.Panel panelEntryDisplay;
        private System.Windows.Forms.Panel panelNavigation;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Label lblEntryPosition;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.TextBox txtEntryDisplay;
        private System.Windows.Forms.Label lblStatus;
    }
}
