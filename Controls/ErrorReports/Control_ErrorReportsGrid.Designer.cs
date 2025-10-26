using System.ComponentModel;

namespace MTM_Inventory_Application.Controls.ErrorReports
{
    partial class Control_ErrorReportsGrid
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutFilters = new System.Windows.Forms.TableLayoutPanel();
            this.lblDateFrom = new System.Windows.Forms.Label();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.lblDateTo = new System.Windows.Forms.Label();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.lblUser = new System.Windows.Forms.Label();
            this.cboUser = new System.Windows.Forms.ComboBox();
            this.lblMachine = new System.Windows.Forms.Label();
            this.cboMachine = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnApplyFilters = new System.Windows.Forms.Button();
            this.btnClearFilters = new System.Windows.Forms.Button();
            this.dgvErrorReports = new System.Windows.Forms.DataGridView();
            this.colReportId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReportDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMachineName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colErrorType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colErrorSummary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblResultCount = new System.Windows.Forms.Label();
            this.tableLayoutPanel.SuspendLayout();
            this.tableLayoutFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvErrorReports)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.tableLayoutFilters, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.dgvErrorReports, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.lblResultCount, 0, 2);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutPanel.Size = new System.Drawing.Size(900, 540);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // tableLayoutFilters
            // 
            this.tableLayoutFilters.ColumnCount = 8;
            this.tableLayoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize));
            this.tableLayoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutFilters.Controls.Add(this.lblDateFrom, 0, 0);
            this.tableLayoutFilters.Controls.Add(this.dtpDateFrom, 1, 0);
            this.tableLayoutFilters.Controls.Add(this.lblDateTo, 2, 0);
            this.tableLayoutFilters.Controls.Add(this.dtpDateTo, 3, 0);
            this.tableLayoutFilters.Controls.Add(this.lblUser, 4, 0);
            this.tableLayoutFilters.Controls.Add(this.cboUser, 5, 0);
            this.tableLayoutFilters.Controls.Add(this.lblMachine, 0, 1);
            this.tableLayoutFilters.Controls.Add(this.cboMachine, 1, 1);
            this.tableLayoutFilters.Controls.Add(this.lblStatus, 2, 1);
            this.tableLayoutFilters.Controls.Add(this.cboStatus, 3, 1);
            this.tableLayoutFilters.Controls.Add(this.lblSearch, 4, 1);
            this.tableLayoutFilters.Controls.Add(this.txtSearch, 5, 1);
            this.tableLayoutFilters.Controls.Add(this.btnApplyFilters, 6, 1);
            this.tableLayoutFilters.Controls.Add(this.btnClearFilters, 7, 1);
            this.tableLayoutFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutFilters.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutFilters.Name = "tableLayoutFilters";
            this.tableLayoutFilters.Padding = new System.Windows.Forms.Padding(4);
            this.tableLayoutFilters.RowCount = 2;
            this.tableLayoutFilters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutFilters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutFilters.Size = new System.Drawing.Size(894, 80);
            this.tableLayoutFilters.TabIndex = 0;
            // 
            // lblDateFrom
            // 
            this.lblDateFrom.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDateFrom.AutoSize = true;
            this.lblDateFrom.Location = new System.Drawing.Point(7, 10);
            this.lblDateFrom.Name = "lblDateFrom";
            this.lblDateFrom.Size = new System.Drawing.Size(47, 20);
            this.lblDateFrom.TabIndex = 0;
            this.lblDateFrom.Text = "From:";
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.dtpDateFrom.Checked = false;
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateFrom.Location = new System.Drawing.Point(60, 7);
            this.dtpDateFrom.Margin = new System.Windows.Forms.Padding(3);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.ShowCheckBox = true;
            this.dtpDateFrom.Size = new System.Drawing.Size(200, 27);
            this.dtpDateFrom.TabIndex = 1;
            // 
            // lblDateTo
            // 
            this.lblDateTo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblDateTo.AutoSize = true;
            this.lblDateTo.Location = new System.Drawing.Point(266, 10);
            this.lblDateTo.Name = "lblDateTo";
            this.lblDateTo.Size = new System.Drawing.Size(27, 20);
            this.lblDateTo.TabIndex = 2;
            this.lblDateTo.Text = "To:";
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.dtpDateTo.Checked = false;
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateTo.Location = new System.Drawing.Point(299, 7);
            this.dtpDateTo.Margin = new System.Windows.Forms.Padding(3);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.ShowCheckBox = true;
            this.dtpDateTo.Size = new System.Drawing.Size(200, 27);
            this.dtpDateTo.TabIndex = 3;
            // 
            // lblUser
            // 
            this.lblUser.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(505, 10);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(40, 20);
            this.lblUser.TabIndex = 4;
            this.lblUser.Text = "User:";
            // 
            // cboUser
            // 
            this.cboUser.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.cboUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUser.FormattingEnabled = true;
            this.cboUser.Location = new System.Drawing.Point(551, 7);
            this.cboUser.Margin = new System.Windows.Forms.Padding(3);
            this.cboUser.Name = "cboUser";
            this.cboUser.Size = new System.Drawing.Size(200, 28);
            this.cboUser.TabIndex = 5;
            // 
            // lblMachine
            // 
            this.lblMachine.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblMachine.AutoSize = true;
            this.lblMachine.Location = new System.Drawing.Point(7, 50);
            this.lblMachine.Name = "lblMachine";
            this.lblMachine.Size = new System.Drawing.Size(68, 20);
            this.lblMachine.TabIndex = 6;
            this.lblMachine.Text = "Machine:";
            // 
            // cboMachine
            // 
            this.cboMachine.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.cboMachine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMachine.FormattingEnabled = true;
            this.cboMachine.Location = new System.Drawing.Point(81, 47);
            this.cboMachine.Margin = new System.Windows.Forms.Padding(3);
            this.cboMachine.Name = "cboMachine";
            this.cboMachine.Size = new System.Drawing.Size(200, 28);
            this.cboMachine.TabIndex = 7;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(287, 50);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(52, 20);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "Status:";
            // 
            // cboStatus
            // 
            this.cboStatus.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Location = new System.Drawing.Point(345, 47);
            this.cboStatus.Margin = new System.Windows.Forms.Padding(3);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(200, 28);
            this.cboStatus.TabIndex = 9;
            // 
            // lblSearch
            // 
            this.lblSearch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(551, 50);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(56, 20);
            this.lblSearch.TabIndex = 10;
            this.lblSearch.Text = "Search:";
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.txtSearch.Location = new System.Drawing.Point(613, 47);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(3);
            this.txtSearch.MaxLength = 200;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderText = "Summary, notes, technical details";
            this.txtSearch.Size = new System.Drawing.Size(200, 27);
            this.txtSearch.TabIndex = 11;
            // 
            // btnApplyFilters
            // 
            this.btnApplyFilters.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnApplyFilters.Location = new System.Drawing.Point(757, 45);
            this.btnApplyFilters.Margin = new System.Windows.Forms.Padding(3);
            this.btnApplyFilters.Name = "btnApplyFilters";
            this.btnApplyFilters.Size = new System.Drawing.Size(75, 32);
            this.btnApplyFilters.TabIndex = 12;
            this.btnApplyFilters.Text = "Apply";
            this.btnApplyFilters.UseVisualStyleBackColor = true;
            this.btnApplyFilters.Click += new System.EventHandler(this.btnApplyFilters_Click);
            // 
            // btnClearFilters
            // 
            this.btnClearFilters.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnClearFilters.Location = new System.Drawing.Point(841, 45);
            this.btnClearFilters.Margin = new System.Windows.Forms.Padding(3);
            this.btnClearFilters.Name = "btnClearFilters";
            this.btnClearFilters.Size = new System.Drawing.Size(75, 32);
            this.btnClearFilters.TabIndex = 13;
            this.btnClearFilters.Text = "Clear";
            this.btnClearFilters.UseVisualStyleBackColor = true;
            this.btnClearFilters.Click += new System.EventHandler(this.btnClearFilters_Click);
            // 
            // dgvErrorReports
            // 
            this.dgvErrorReports.AllowUserToAddRows = false;
            this.dgvErrorReports.AllowUserToDeleteRows = false;
            this.dgvErrorReports.AllowUserToResizeRows = false;
            this.dgvErrorReports.AutoGenerateColumns = false;
            this.dgvErrorReports.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvErrorReports.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvErrorReports.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvErrorReports.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colReportId,
            this.colReportDate,
            this.colUserName,
            this.colMachineName,
            this.colErrorType,
            this.colErrorSummary,
            this.colStatus});
            this.dgvErrorReports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvErrorReports.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvErrorReports.Location = new System.Drawing.Point(3, 9);
            this.dgvErrorReports.MultiSelect = false;
            this.dgvErrorReports.Name = "dgvErrorReports";
            this.dgvErrorReports.ReadOnly = true;
            this.dgvErrorReports.RowHeadersVisible = false;
            this.dgvErrorReports.RowTemplate.Height = 24;
            this.dgvErrorReports.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvErrorReports.Size = new System.Drawing.Size(894, 501);
            this.dgvErrorReports.TabIndex = 1;
            this.dgvErrorReports.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvErrorReports_CellDoubleClick);
            this.dgvErrorReports.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvErrorReports_CellFormatting);
            // 
            // colReportId
            // 
            this.colReportId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colReportId.DataPropertyName = "ReportID";
            this.colReportId.FillWeight = 12F;
            this.colReportId.HeaderText = "Report ID";
            this.colReportId.MinimumWidth = 80;
            this.colReportId.Name = "colReportId";
            this.colReportId.ReadOnly = true;
            this.colReportId.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colReportDate
            // 
            this.colReportDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colReportDate.DataPropertyName = "ReportDate";
            this.colReportDate.DefaultCellStyle = new System.Windows.Forms.DataGridViewCellStyle { Format = "g" };
            this.colReportDate.FillWeight = 20F;
            this.colReportDate.HeaderText = "Reported";
            this.colReportDate.MinimumWidth = 120;
            this.colReportDate.Name = "colReportDate";
            this.colReportDate.ReadOnly = true;
            this.colReportDate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colUserName
            // 
            this.colUserName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colUserName.DataPropertyName = "UserName";
            this.colUserName.FillWeight = 15F;
            this.colUserName.HeaderText = "User";
            this.colUserName.MinimumWidth = 120;
            this.colUserName.Name = "colUserName";
            this.colUserName.ReadOnly = true;
            this.colUserName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colMachineName
            // 
            this.colMachineName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colMachineName.DataPropertyName = "MachineName";
            this.colMachineName.FillWeight = 15F;
            this.colMachineName.HeaderText = "Machine";
            this.colMachineName.MinimumWidth = 120;
            this.colMachineName.Name = "colMachineName";
            this.colMachineName.ReadOnly = true;
            this.colMachineName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colErrorType
            // 
            this.colErrorType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colErrorType.DataPropertyName = "ErrorType";
            this.colErrorType.FillWeight = 18F;
            this.colErrorType.HeaderText = "Error Type";
            this.colErrorType.MinimumWidth = 150;
            this.colErrorType.Name = "colErrorType";
            this.colErrorType.ReadOnly = true;
            this.colErrorType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colErrorSummary
            // 
            this.colErrorSummary.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colErrorSummary.DataPropertyName = "ErrorSummary";
            this.colErrorSummary.FillWeight = 35F;
            this.colErrorSummary.HeaderText = "Summary";
            this.colErrorSummary.MinimumWidth = 200;
            this.colErrorSummary.Name = "colErrorSummary";
            this.colErrorSummary.ReadOnly = true;
            this.colErrorSummary.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // colStatus
            // 
            this.colStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colStatus.DataPropertyName = "Status";
            this.colStatus.FillWeight = 10F;
            this.colStatus.HeaderText = "Status";
            this.colStatus.MinimumWidth = 100;
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            this.colStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // lblResultCount
            // 
            this.lblResultCount.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblResultCount.AutoSize = true;
            this.lblResultCount.Location = new System.Drawing.Point(3, 517);
            this.lblResultCount.Margin = new System.Windows.Forms.Padding(3, 4, 3, 8);
            this.lblResultCount.Name = "lblResultCount";
            this.lblResultCount.Size = new System.Drawing.Size(149, 20);
            this.lblResultCount.TabIndex = 2;
            this.lblResultCount.Text = "Showing 0 reports";
            // 
            // Control_ErrorReportsGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "Control_ErrorReportsGrid";
            this.Size = new System.Drawing.Size(900, 540);
            this.Load += new System.EventHandler(this.Control_ErrorReportsGrid_Load);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.tableLayoutFilters.ResumeLayout(false);
            this.tableLayoutFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvErrorReports)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutFilters;
        private System.Windows.Forms.DataGridView dgvErrorReports;
        private System.Windows.Forms.Label lblResultCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReportId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReportDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMachineName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colErrorType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colErrorSummary;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.Label lblDateFrom;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.Label lblDateTo;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.ComboBox cboUser;
        private System.Windows.Forms.Label lblMachine;
        private System.Windows.Forms.ComboBox cboMachine;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnApplyFilters;
        private System.Windows.Forms.Button btnClearFilters;
    }
}
