namespace MTM_WIP_Application_Winforms.Controls.Transactions
{
    partial class TransactionSearchControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tableLayoutFilters = new System.Windows.Forms.TableLayoutPanel();
            this.lblPartNumber = new System.Windows.Forms.Label();
            this.cboPartNumber = new System.Windows.Forms.ComboBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.cboUser = new System.Windows.Forms.ComboBox();
            this.lblFromLocation = new System.Windows.Forms.Label();
            this.cboFromLocation = new System.Windows.Forms.ComboBox();
            this.lblToLocation = new System.Windows.Forms.Label();
            this.cboToLocation = new System.Windows.Forms.ComboBox();
            this.lblOperation = new System.Windows.Forms.Label();
            this.txtOperation = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.grpTransactionTypes = new System.Windows.Forms.GroupBox();
            this.flowTransactionTypes = new System.Windows.Forms.FlowLayoutPanel();
            this.chkIN = new System.Windows.Forms.CheckBox();
            this.chkOUT = new System.Windows.Forms.CheckBox();
            this.chkTRANSFER = new System.Windows.Forms.CheckBox();
            this.grpDateRange = new System.Windows.Forms.GroupBox();
            this.tableLayoutDateRange = new System.Windows.Forms.TableLayoutPanel();
            this.flowQuickFilters = new System.Windows.Forms.FlowLayoutPanel();
            this.rdoToday = new System.Windows.Forms.RadioButton();
            this.rdoWeek = new System.Windows.Forms.RadioButton();
            this.rdoMonth = new System.Windows.Forms.RadioButton();
            this.rdoCustom = new System.Windows.Forms.RadioButton();
            this.lblDateFrom = new System.Windows.Forms.Label();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.lblDateTo = new System.Windows.Forms.Label();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.tableLayoutMain.SuspendLayout();
            this.tableLayoutFilters.SuspendLayout();
            this.grpTransactionTypes.SuspendLayout();
            this.flowTransactionTypes.SuspendLayout();
            this.grpDateRange.SuspendLayout();
            this.tableLayoutDateRange.SuspendLayout();
            this.flowQuickFilters.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutMain
            // 
            this.tableLayoutMain.ColumnCount = 1;
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutMain.Controls.Add(this.lblTitle, 0, 0);
            this.tableLayoutMain.Controls.Add(this.tableLayoutFilters, 0, 1);
            this.tableLayoutMain.AutoSize = true;
            this.tableLayoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutMain.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutMain.Name = "tableLayoutMain";
            this.tableLayoutMain.Padding = new System.Windows.Forms.Padding(3);
            this.tableLayoutMain.RowCount = 2;
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutMain.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(3, 3);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(2);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Search Criteria";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tableLayoutFilters
            // 
            this.tableLayoutFilters.ColumnCount = 6;
            this.tableLayoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 18F));
            this.tableLayoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16F));
            this.tableLayoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tableLayoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14F));
            this.tableLayoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableLayoutFilters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26F));
            this.tableLayoutFilters.Controls.Add(this.lblPartNumber, 0, 0);
            this.tableLayoutFilters.Controls.Add(this.cboPartNumber, 0, 1);
            this.tableLayoutFilters.Controls.Add(this.lblUser, 1, 0);
            this.tableLayoutFilters.Controls.Add(this.cboUser, 1, 1);
            this.tableLayoutFilters.Controls.Add(this.lblFromLocation, 2, 0);
            this.tableLayoutFilters.Controls.Add(this.cboFromLocation, 2, 1);
            this.tableLayoutFilters.Controls.Add(this.lblToLocation, 3, 0);
            this.tableLayoutFilters.Controls.Add(this.cboToLocation, 3, 1);
            this.tableLayoutFilters.Controls.Add(this.lblOperation, 4, 0);
            this.tableLayoutFilters.Controls.Add(this.txtOperation, 4, 1);
            this.tableLayoutFilters.Controls.Add(this.btnSearch, 5, 1);
            this.tableLayoutFilters.Controls.Add(this.grpTransactionTypes, 0, 2);
            this.tableLayoutFilters.SetRowSpan(this.grpTransactionTypes, 2);
            this.tableLayoutFilters.Controls.Add(this.grpDateRange, 1, 2);
            this.tableLayoutFilters.SetColumnSpan(this.grpDateRange, 2);
            this.tableLayoutFilters.SetRowSpan(this.grpDateRange, 2);
            this.tableLayoutFilters.Controls.Add(this.lblNotes, 3, 2);
            this.tableLayoutFilters.SetColumnSpan(this.lblNotes, 2);
            this.tableLayoutFilters.Controls.Add(this.txtNotes, 3, 3);
            this.tableLayoutFilters.SetColumnSpan(this.txtNotes, 2);
            this.tableLayoutFilters.Controls.Add(this.btnReset, 5, 3);
            this.tableLayoutFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutFilters.Location = new System.Drawing.Point(3, 25);
            this.tableLayoutFilters.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutFilters.Name = "tableLayoutFilters";
            this.tableLayoutFilters.RowCount = 4;
            this.tableLayoutFilters.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutFilters.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutFilters.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutFilters.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutFilters.TabIndex = 1;
            // 
            // Row 1 Labels and Controls
            // 
            this.lblPartNumber.AutoSize = true;
            this.lblPartNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPartNumber.Location = new System.Drawing.Point(3, 0);
            this.lblPartNumber.Name = "lblPartNumber";
            this.lblPartNumber.Size = new System.Drawing.Size(223, 24);
            this.lblPartNumber.TabIndex = 0;
            this.lblPartNumber.Text = "🔍 Part Number";
            this.lblPartNumber.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // cboPartNumber
            // 
            this.cboPartNumber.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboPartNumber.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboPartNumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboPartNumber.FormattingEnabled = true;
            this.cboPartNumber.Location = new System.Drawing.Point(5, 29);
            this.cboPartNumber.Margin = new System.Windows.Forms.Padding(2);
            this.cboPartNumber.Name = "cboPartNumber";
            this.cboPartNumber.Size = new System.Drawing.Size(219, 23);
            this.cboPartNumber.TabIndex = 1;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUser.Location = new System.Drawing.Point(232, 0);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(197, 24);
            this.lblUser.TabIndex = 2;
            this.lblUser.Text = "👤 User";
            this.lblUser.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // cboUser
            // 
            this.cboUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUser.FormattingEnabled = true;
            this.cboUser.Location = new System.Drawing.Point(234, 29);
            this.cboUser.Margin = new System.Windows.Forms.Padding(2);
            this.cboUser.Name = "cboUser";
            this.cboUser.Size = new System.Drawing.Size(193, 23);
            this.cboUser.TabIndex = 3;
            // 
            // lblFromLocation
            // 
            this.lblFromLocation.AutoSize = true;
            this.lblFromLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFromLocation.Location = new System.Drawing.Point(435, 0);
            this.lblFromLocation.Name = "lblFromLocation";
            this.lblFromLocation.Size = new System.Drawing.Size(172, 24);
            this.lblFromLocation.TabIndex = 4;
            this.lblFromLocation.Text = "📍 From Location";
            this.lblFromLocation.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // cboFromLocation
            // 
            this.cboFromLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboFromLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFromLocation.FormattingEnabled = true;
            this.cboFromLocation.Location = new System.Drawing.Point(437, 29);
            this.cboFromLocation.Margin = new System.Windows.Forms.Padding(2);
            this.cboFromLocation.Name = "cboFromLocation";
            this.cboFromLocation.Size = new System.Drawing.Size(168, 23);
            this.cboFromLocation.TabIndex = 5;
            // 
            // lblToLocation
            // 
            this.lblToLocation.AutoSize = true;
            this.lblToLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblToLocation.Location = new System.Drawing.Point(613, 0);
            this.lblToLocation.Name = "lblToLocation";
            this.lblToLocation.Size = new System.Drawing.Size(172, 24);
            this.lblToLocation.TabIndex = 6;
            this.lblToLocation.Text = "📍 To Location";
            this.lblToLocation.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // cboToLocation
            // 
            this.cboToLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cboToLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboToLocation.FormattingEnabled = true;
            this.cboToLocation.Location = new System.Drawing.Point(615, 29);
            this.cboToLocation.Margin = new System.Windows.Forms.Padding(2);
            this.cboToLocation.Name = "cboToLocation";
            this.cboToLocation.Size = new System.Drawing.Size(168, 23);
            this.cboToLocation.TabIndex = 7;
            // 
            // lblOperation
            // 
            this.lblOperation.AutoSize = true;
            this.lblOperation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOperation.Location = new System.Drawing.Point(791, 0);
            this.lblOperation.Name = "lblOperation";
            this.lblOperation.Size = new System.Drawing.Size(146, 24);
            this.lblOperation.TabIndex = 8;
            this.lblOperation.Text = "⚙️ Operation";
            this.lblOperation.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txtOperation
            // 
            this.txtOperation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOperation.Location = new System.Drawing.Point(793, 29);
            this.txtOperation.Margin = new System.Windows.Forms.Padding(2);
            this.txtOperation.Name = "txtOperation";
            this.txtOperation.PlaceholderText = "e.g., 90, 100";
            this.txtOperation.Size = new System.Drawing.Size(142, 23);
            this.txtOperation.TabIndex = 9;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(59, 130, 246);
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(945, 29);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(324, 30);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "🔎 Search (F5)";
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // grpTransactionTypes
            // 
            this.grpTransactionTypes.Controls.Add(this.flowTransactionTypes);
            this.grpTransactionTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpTransactionTypes.Location = new System.Drawing.Point(3, 67);
            this.grpTransactionTypes.Name = "grpTransactionTypes";
            this.grpTransactionTypes.Size = new System.Drawing.Size(223, 44);
            this.grpTransactionTypes.TabIndex = 11;
            this.grpTransactionTypes.TabStop = false;
            this.grpTransactionTypes.Text = "Transaction Types";
            // 
            // flowTransactionTypes
            // 
            this.flowTransactionTypes.Controls.Add(this.chkIN);
            this.flowTransactionTypes.Controls.Add(this.chkOUT);
            this.flowTransactionTypes.Controls.Add(this.chkTRANSFER);
            this.flowTransactionTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowTransactionTypes.Location = new System.Drawing.Point(3, 19);
            this.flowTransactionTypes.Name = "flowTransactionTypes";
            this.flowTransactionTypes.Size = new System.Drawing.Size(217, 22);
            this.flowTransactionTypes.TabIndex = 0;
            // 
            // chkIN
            // 
            this.chkIN.AutoSize = true;
            this.chkIN.Checked = true;
            this.chkIN.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIN.Location = new System.Drawing.Point(3, 3);
            this.chkIN.Name = "chkIN";
            this.chkIN.Size = new System.Drawing.Size(40, 19);
            this.chkIN.TabIndex = 0;
            this.chkIN.Text = "IN";
            this.chkIN.UseVisualStyleBackColor = true;
            // 
            // chkOUT
            // 
            this.chkOUT.AutoSize = true;
            this.chkOUT.Checked = true;
            this.chkOUT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOUT.Location = new System.Drawing.Point(49, 3);
            this.chkOUT.Name = "chkOUT";
            this.chkOUT.Size = new System.Drawing.Size(51, 19);
            this.chkOUT.TabIndex = 1;
            this.chkOUT.Text = "OUT";
            this.chkOUT.UseVisualStyleBackColor = true;
            // 
            // chkTRANSFER
            // 
            this.chkTRANSFER.AutoSize = true;
            this.chkTRANSFER.Checked = true;
            this.chkTRANSFER.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTRANSFER.Location = new System.Drawing.Point(106, 3);
            this.chkTRANSFER.Name = "chkTRANSFER";
            this.chkTRANSFER.Size = new System.Drawing.Size(90, 19);
            this.chkTRANSFER.TabIndex = 2;
            this.chkTRANSFER.Text = "TRANSFER";
            this.chkTRANSFER.UseVisualStyleBackColor = true;
            // 
            // grpDateRange
            // 
            this.grpDateRange.Controls.Add(this.tableLayoutDateRange);
            this.grpDateRange.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDateRange.Location = new System.Drawing.Point(232, 67);
            this.grpDateRange.Name = "grpDateRange";
            this.grpDateRange.Size = new System.Drawing.Size(375, 44);
            this.grpDateRange.TabIndex = 12;
            this.grpDateRange.TabStop = false;
            this.grpDateRange.Text = "Date Range";
            // 
            // tableLayoutDateRange
            // 
            this.tableLayoutDateRange.ColumnCount = 4;
            this.tableLayoutDateRange.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutDateRange.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutDateRange.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutDateRange.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutDateRange.Controls.Add(this.flowQuickFilters, 0, 0);
            this.tableLayoutDateRange.SetColumnSpan(this.flowQuickFilters, 4);
            this.tableLayoutDateRange.Controls.Add(this.lblDateFrom, 0, 1);
            this.tableLayoutDateRange.Controls.Add(this.dtpDateFrom, 1, 1);
            this.tableLayoutDateRange.Controls.Add(this.lblDateTo, 2, 1);
            this.tableLayoutDateRange.Controls.Add(this.dtpDateTo, 3, 1);
            this.tableLayoutDateRange.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutDateRange.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutDateRange.Name = "tableLayoutDateRange";
            this.tableLayoutDateRange.RowCount = 2;
            this.tableLayoutDateRange.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutDateRange.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutDateRange.Size = new System.Drawing.Size(369, 22);
            this.tableLayoutDateRange.TabIndex = 0;
            // 
            // flowQuickFilters
            // 
            this.flowQuickFilters.Controls.Add(this.rdoToday);
            this.flowQuickFilters.Controls.Add(this.rdoWeek);
            this.flowQuickFilters.Controls.Add(this.rdoMonth);
            this.flowQuickFilters.Controls.Add(this.rdoCustom);
            this.flowQuickFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowQuickFilters.Location = new System.Drawing.Point(0, 0);
            this.flowQuickFilters.Margin = new System.Windows.Forms.Padding(0);
            this.flowQuickFilters.Name = "flowQuickFilters";
            this.flowQuickFilters.Size = new System.Drawing.Size(369, 30);
            this.flowQuickFilters.TabIndex = 0;
            // 
            // rdoToday
            // 
            this.rdoToday.AutoSize = true;
            this.rdoToday.Location = new System.Drawing.Point(3, 3);
            this.rdoToday.Name = "rdoToday";
            this.rdoToday.Size = new System.Drawing.Size(57, 19);
            this.rdoToday.TabIndex = 0;
            this.rdoToday.Text = "Today";
            this.rdoToday.UseVisualStyleBackColor = true;
            // 
            // rdoWeek
            // 
            this.rdoWeek.AutoSize = true;
            this.rdoWeek.Location = new System.Drawing.Point(66, 3);
            this.rdoWeek.Name = "rdoWeek";
            this.rdoWeek.Size = new System.Drawing.Size(54, 19);
            this.rdoWeek.TabIndex = 1;
            this.rdoWeek.Text = "Week";
            this.rdoWeek.UseVisualStyleBackColor = true;
            // 
            // rdoMonth
            // 
            this.rdoMonth.AutoSize = true;
            this.rdoMonth.Checked = true;
            this.rdoMonth.Location = new System.Drawing.Point(126, 3);
            this.rdoMonth.Name = "rdoMonth";
            this.rdoMonth.Size = new System.Drawing.Size(62, 19);
            this.rdoMonth.TabIndex = 2;
            this.rdoMonth.TabStop = true;
            this.rdoMonth.Text = "Month";
            this.rdoMonth.UseVisualStyleBackColor = true;
            // 
            // rdoCustom
            // 
            this.rdoCustom.AutoSize = true;
            this.rdoCustom.Location = new System.Drawing.Point(194, 3);
            this.rdoCustom.Name = "rdoCustom";
            this.rdoCustom.Size = new System.Drawing.Size(69, 19);
            this.rdoCustom.TabIndex = 3;
            this.rdoCustom.Text = "Custom";
            this.rdoCustom.UseVisualStyleBackColor = true;
            // 
            // lblDateFrom
            // 
            this.lblDateFrom.AutoSize = true;
            this.lblDateFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDateFrom.Location = new System.Drawing.Point(3, 30);
            this.lblDateFrom.Name = "lblDateFrom";
            this.lblDateFrom.Size = new System.Drawing.Size(86, 0);
            this.lblDateFrom.TabIndex = 1;
            this.lblDateFrom.Text = "From:";
            this.lblDateFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateFrom.Location = new System.Drawing.Point(95, 33);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.Size = new System.Drawing.Size(86, 23);
            this.dtpDateFrom.TabIndex = 2;
            // 
            // lblDateTo
            // 
            this.lblDateTo.AutoSize = true;
            this.lblDateTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDateTo.Location = new System.Drawing.Point(187, 30);
            this.lblDateTo.Name = "lblDateTo";
            this.lblDateTo.Size = new System.Drawing.Size(86, 0);
            this.lblDateTo.TabIndex = 3;
            this.lblDateTo.Text = "To:";
            this.lblDateTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateTo.Location = new System.Drawing.Point(279, 33);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.Size = new System.Drawing.Size(87, 23);
            this.dtpDateTo.TabIndex = 4;
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNotes.Location = new System.Drawing.Point(613, 64);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(322, 24);
            this.lblNotes.TabIndex = 13;
            this.lblNotes.Text = "📝 Notes Keyword";
            this.lblNotes.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txtNotes
            // 
            this.txtNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNotes.Location = new System.Drawing.Point(615, 93);
            this.txtNotes.Margin = new System.Windows.Forms.Padding(2);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.PlaceholderText = "Partial match supported";
            this.txtNotes.Size = new System.Drawing.Size(318, 23);
            this.txtNotes.TabIndex = 14;
            // 
            // btnReset
            // 
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(226, 232, 240);
            this.btnReset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnReset.Location = new System.Drawing.Point(945, 93);
            this.btnReset.Margin = new System.Windows.Forms.Padding(2);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(324, 16);
            this.btnReset.TabIndex = 15;
            this.btnReset.Text = "🔄 Reset (Ctrl+R)";
            this.btnReset.UseVisualStyleBackColor = false;
            // 
            // TransactionSearchControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutMain);
            this.MinimumSize = new System.Drawing.Size(600, 120);
            this.Name = "TransactionSearchControl";
            this.tableLayoutMain.ResumeLayout(false);
            this.tableLayoutMain.PerformLayout();
            this.tableLayoutFilters.ResumeLayout(false);
            this.tableLayoutFilters.PerformLayout();
            this.grpTransactionTypes.ResumeLayout(false);
            this.flowTransactionTypes.ResumeLayout(false);
            this.flowTransactionTypes.PerformLayout();
            this.grpDateRange.ResumeLayout(false);
            this.tableLayoutDateRange.ResumeLayout(false);
            this.tableLayoutDateRange.PerformLayout();
            this.flowQuickFilters.ResumeLayout(false);
            this.flowQuickFilters.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutMain;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutFilters;
        private System.Windows.Forms.Label lblPartNumber;
        private System.Windows.Forms.ComboBox cboPartNumber;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.ComboBox cboUser;
        private System.Windows.Forms.Label lblFromLocation;
        private System.Windows.Forms.ComboBox cboFromLocation;
        private System.Windows.Forms.Label lblToLocation;
        private System.Windows.Forms.ComboBox cboToLocation;
        private System.Windows.Forms.Label lblOperation;
        private System.Windows.Forms.TextBox txtOperation;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.GroupBox grpTransactionTypes;
        private System.Windows.Forms.FlowLayoutPanel flowTransactionTypes;
        private System.Windows.Forms.CheckBox chkIN;
        private System.Windows.Forms.CheckBox chkOUT;
        private System.Windows.Forms.CheckBox chkTRANSFER;
        private System.Windows.Forms.GroupBox grpDateRange;
        private System.Windows.Forms.TableLayoutPanel tableLayoutDateRange;
        private System.Windows.Forms.FlowLayoutPanel flowQuickFilters;
        private System.Windows.Forms.RadioButton rdoToday;
        private System.Windows.Forms.RadioButton rdoWeek;
        private System.Windows.Forms.RadioButton rdoMonth;
        private System.Windows.Forms.RadioButton rdoCustom;
        private System.Windows.Forms.Label lblDateFrom;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.Label lblDateTo;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Button btnReset;
    }
}
