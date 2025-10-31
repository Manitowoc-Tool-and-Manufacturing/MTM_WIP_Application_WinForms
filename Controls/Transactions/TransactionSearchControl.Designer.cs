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
            tableLayoutMain = new TableLayoutPanel();
            lblTitle = new Label();
            tableLayoutFilters = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            cboPartNumber = new ComboBox();
            lblPartNumber = new Label();
            tableLayoutPanel4 = new TableLayoutPanel();
            cboUser = new ComboBox();
            lblUser = new Label();
            tableLayoutPanel3 = new TableLayoutPanel();
            lblOperation = new Label();
            txtOperation = new TextBox();
            tableLayoutPanel5 = new TableLayoutPanel();
            cboFromLocation = new ComboBox();
            lblFromLocation = new Label();
            tableLayoutPanel7 = new TableLayoutPanel();
            lblNotes = new Label();
            txtNotes = new TextBox();
            panel1 = new Panel();
            grpTransactionTypes = new GroupBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            chkTRANSFER = new CheckBox();
            chkIN = new CheckBox();
            chkOUT = new CheckBox();
            tableLayoutPanel6 = new TableLayoutPanel();
            cboToLocation = new ComboBox();
            lblToLocation = new Label();
            panel2 = new Panel();
            grpDateRange = new GroupBox();
            tableLayoutDateRange = new TableLayoutPanel();
            rdoCustom = new RadioButton();
            lblDateFrom = new Label();
            rdoMonth = new RadioButton();
            dtpDateTo = new DateTimePicker();
            rdoWeek = new RadioButton();
            lblDateTo = new Label();
            rdoToday = new RadioButton();
            dtpDateFrom = new DateTimePicker();
            panel3 = new Panel();
            tableLayoutPanel8 = new TableLayoutPanel();
            btnSearch = new Button();
            btnReset = new Button();
            tableLayoutMain.SuspendLayout();
            tableLayoutFilters.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel7.SuspendLayout();
            panel1.SuspendLayout();
            grpTransactionTypes.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            panel2.SuspendLayout();
            grpDateRange.SuspendLayout();
            tableLayoutDateRange.SuspendLayout();
            panel3.SuspendLayout();
            tableLayoutPanel8.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutMain
            // 
            tableLayoutMain.AutoSize = true;
            tableLayoutMain.ColumnCount = 1;
            tableLayoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutMain.Controls.Add(lblTitle, 0, 0);
            tableLayoutMain.Controls.Add(tableLayoutFilters, 0, 1);
            tableLayoutMain.Location = new Point(0, 0);
            tableLayoutMain.Margin = new Padding(0);
            tableLayoutMain.Name = "tableLayoutMain";
            tableLayoutMain.RowCount = 2;
            tableLayoutMain.RowStyles.Add(new RowStyle());
            tableLayoutMain.RowStyles.Add(new RowStyle());
            tableLayoutMain.Size = new Size(2457, 881);
            tableLayoutMain.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTitle.Location = new Point(0, 0);
            lblTitle.Margin = new Padding(0);
            lblTitle.Name = "lblTitle";
            lblTitle.Padding = new Padding(27, 13, 27, 13);
            lblTitle.Size = new Size(162, 45);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Search Criteria";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tableLayoutFilters
            // 
            tableLayoutFilters.ColumnCount = 3;
            tableLayoutFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutFilters.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutFilters.Controls.Add(tableLayoutPanel4, 1, 0);
            tableLayoutFilters.Controls.Add(tableLayoutPanel3, 2, 0);
            tableLayoutFilters.Controls.Add(tableLayoutPanel5, 0, 1);
            tableLayoutFilters.Controls.Add(tableLayoutPanel7, 2, 1);
            tableLayoutFilters.Controls.Add(panel1, 0, 2);
            tableLayoutFilters.Controls.Add(tableLayoutPanel6, 1, 1);
            tableLayoutFilters.Controls.Add(panel2, 0, 3);
            tableLayoutFilters.Controls.Add(panel3, 0, 4);
            tableLayoutFilters.Location = new Point(0, 45);
            tableLayoutFilters.Margin = new Padding(0);
            tableLayoutFilters.Name = "tableLayoutFilters";
            tableLayoutFilters.RowCount = 5;
            tableLayoutFilters.RowStyles.Add(new RowStyle());
            tableLayoutFilters.RowStyles.Add(new RowStyle());
            tableLayoutFilters.RowStyles.Add(new RowStyle(SizeType.Percent, 27.29167F));
            tableLayoutFilters.RowStyles.Add(new RowStyle(SizeType.Percent, 39.375F));
            tableLayoutFilters.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutFilters.Size = new Size(2457, 836);
            tableLayoutFilters.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(cboPartNumber, 0, 1);
            tableLayoutPanel2.Controls.Add(lblPartNumber, 0, 0);
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(200, 41);
            tableLayoutPanel2.TabIndex = 17;
            // 
            // cboPartNumber
            // 
            cboPartNumber.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboPartNumber.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboPartNumber.Dock = DockStyle.Fill;
            cboPartNumber.FormattingEnabled = true;
            cboPartNumber.Location = new Point(0, 15);
            cboPartNumber.Margin = new Padding(0);
            cboPartNumber.Name = "cboPartNumber";
            cboPartNumber.Size = new Size(200, 23);
            cboPartNumber.TabIndex = 1;
            // 
            // lblPartNumber
            // 
            lblPartNumber.AutoSize = true;
            lblPartNumber.Dock = DockStyle.Fill;
            lblPartNumber.Location = new Point(0, 0);
            lblPartNumber.Margin = new Padding(0);
            lblPartNumber.Name = "lblPartNumber";
            lblPartNumber.Size = new Size(200, 15);
            lblPartNumber.TabIndex = 0;
            lblPartNumber.Text = "🔍 Part Number";
            lblPartNumber.TextAlign = ContentAlignment.BottomLeft;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Controls.Add(cboUser, 0, 1);
            tableLayoutPanel4.Controls.Add(lblUser, 0, 0);
            tableLayoutPanel4.Location = new Point(617, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Size = new Size(200, 41);
            tableLayoutPanel4.TabIndex = 18;
            // 
            // cboUser
            // 
            cboUser.Dock = DockStyle.Fill;
            cboUser.DropDownStyle = ComboBoxStyle.DropDownList;
            cboUser.FormattingEnabled = true;
            cboUser.Location = new Point(0, 15);
            cboUser.Margin = new Padding(0);
            cboUser.Name = "cboUser";
            cboUser.Size = new Size(200, 23);
            cboUser.TabIndex = 3;
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Dock = DockStyle.Fill;
            lblUser.Location = new Point(0, 0);
            lblUser.Margin = new Padding(0);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(200, 15);
            lblUser.TabIndex = 2;
            lblUser.Text = "👤 User";
            lblUser.TextAlign = ContentAlignment.BottomLeft;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(lblOperation, 0, 0);
            tableLayoutPanel3.Controls.Add(txtOperation, 0, 1);
            tableLayoutPanel3.Location = new Point(1231, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(200, 41);
            tableLayoutPanel3.TabIndex = 18;
            // 
            // lblOperation
            // 
            lblOperation.AutoSize = true;
            lblOperation.Dock = DockStyle.Fill;
            lblOperation.Location = new Point(0, 0);
            lblOperation.Margin = new Padding(0);
            lblOperation.Name = "lblOperation";
            lblOperation.Size = new Size(200, 15);
            lblOperation.TabIndex = 8;
            lblOperation.Text = "⚙️ Operation";
            lblOperation.TextAlign = ContentAlignment.BottomLeft;
            // 
            // txtOperation
            // 
            txtOperation.Dock = DockStyle.Fill;
            txtOperation.Location = new Point(0, 15);
            txtOperation.Margin = new Padding(0);
            txtOperation.Name = "txtOperation";
            txtOperation.PlaceholderText = "e.g., 90, 100";
            txtOperation.Size = new Size(200, 23);
            txtOperation.TabIndex = 9;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 1;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Controls.Add(cboFromLocation, 0, 1);
            tableLayoutPanel5.Controls.Add(lblFromLocation, 0, 0);
            tableLayoutPanel5.Location = new Point(3, 50);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 2;
            tableLayoutPanel5.RowStyles.Add(new RowStyle());
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Size = new Size(200, 41);
            tableLayoutPanel5.TabIndex = 18;
            // 
            // cboFromLocation
            // 
            cboFromLocation.Dock = DockStyle.Fill;
            cboFromLocation.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFromLocation.FormattingEnabled = true;
            cboFromLocation.Location = new Point(0, 15);
            cboFromLocation.Margin = new Padding(0);
            cboFromLocation.Name = "cboFromLocation";
            cboFromLocation.Size = new Size(200, 23);
            cboFromLocation.TabIndex = 5;
            // 
            // lblFromLocation
            // 
            lblFromLocation.AutoSize = true;
            lblFromLocation.Dock = DockStyle.Fill;
            lblFromLocation.Location = new Point(0, 0);
            lblFromLocation.Margin = new Padding(0);
            lblFromLocation.Name = "lblFromLocation";
            lblFromLocation.Size = new Size(200, 15);
            lblFromLocation.TabIndex = 4;
            lblFromLocation.Text = "📍 From Location";
            lblFromLocation.TextAlign = ContentAlignment.BottomLeft;
            // 
            // tableLayoutPanel7
            // 
            tableLayoutPanel7.ColumnCount = 1;
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel7.Controls.Add(lblNotes, 0, 0);
            tableLayoutPanel7.Controls.Add(txtNotes, 0, 1);
            tableLayoutPanel7.Location = new Point(1231, 50);
            tableLayoutPanel7.Name = "tableLayoutPanel7";
            tableLayoutPanel7.RowCount = 2;
            tableLayoutPanel7.RowStyles.Add(new RowStyle());
            tableLayoutPanel7.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel7.Size = new Size(200, 41);
            tableLayoutPanel7.TabIndex = 19;
            // 
            // lblNotes
            // 
            lblNotes.AutoSize = true;
            lblNotes.Dock = DockStyle.Fill;
            lblNotes.Location = new Point(0, 0);
            lblNotes.Margin = new Padding(0);
            lblNotes.Name = "lblNotes";
            lblNotes.Size = new Size(200, 15);
            lblNotes.TabIndex = 13;
            lblNotes.Text = "📝 Notes Keyword";
            lblNotes.TextAlign = ContentAlignment.BottomLeft;
            // 
            // txtNotes
            // 
            tableLayoutPanel7.SetColumnSpan(txtNotes, 2);
            txtNotes.Dock = DockStyle.Fill;
            txtNotes.Location = new Point(0, 15);
            txtNotes.Margin = new Padding(0);
            txtNotes.Name = "txtNotes";
            txtNotes.PlaceholderText = "Partial match supported";
            txtNotes.Size = new Size(200, 23);
            txtNotes.TabIndex = 14;
            // 
            // panel1
            // 
            panel1.Controls.Add(grpTransactionTypes);
            panel1.Location = new Point(3, 97);
            panel1.Name = "panel1";
            panel1.Size = new Size(200, 84);
            panel1.TabIndex = 20;
            // 
            // grpTransactionTypes
            // 
            grpTransactionTypes.Controls.Add(tableLayoutPanel1);
            grpTransactionTypes.Location = new Point(18, 0);
            grpTransactionTypes.Margin = new Padding(0);
            grpTransactionTypes.Name = "grpTransactionTypes";
            grpTransactionTypes.Padding = new Padding(0);
            grpTransactionTypes.Size = new Size(298, 52);
            grpTransactionTypes.TabIndex = 11;
            grpTransactionTypes.TabStop = false;
            grpTransactionTypes.Text = "Transaction Types";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Controls.Add(chkTRANSFER, 2, 0);
            tableLayoutPanel1.Controls.Add(chkIN, 0, 0);
            tableLayoutPanel1.Controls.Add(chkOUT, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 16);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(298, 36);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // chkTRANSFER
            // 
            chkTRANSFER.AutoSize = true;
            chkTRANSFER.Checked = true;
            chkTRANSFER.CheckState = CheckState.Checked;
            chkTRANSFER.Dock = DockStyle.Fill;
            chkTRANSFER.Location = new Point(198, 0);
            chkTRANSFER.Margin = new Padding(0);
            chkTRANSFER.Name = "chkTRANSFER";
            chkTRANSFER.Size = new Size(100, 36);
            chkTRANSFER.TabIndex = 2;
            chkTRANSFER.Text = "TRANSFER";
            chkTRANSFER.UseVisualStyleBackColor = true;
            // 
            // chkIN
            // 
            chkIN.AutoSize = true;
            chkIN.Checked = true;
            chkIN.CheckState = CheckState.Checked;
            chkIN.Dock = DockStyle.Fill;
            chkIN.Location = new Point(0, 0);
            chkIN.Margin = new Padding(0);
            chkIN.Name = "chkIN";
            chkIN.Size = new Size(99, 36);
            chkIN.TabIndex = 0;
            chkIN.Text = "IN";
            chkIN.UseVisualStyleBackColor = true;
            // 
            // chkOUT
            // 
            chkOUT.AutoSize = true;
            chkOUT.Checked = true;
            chkOUT.CheckState = CheckState.Checked;
            chkOUT.Dock = DockStyle.Fill;
            chkOUT.Location = new Point(99, 0);
            chkOUT.Margin = new Padding(0);
            chkOUT.Name = "chkOUT";
            chkOUT.Size = new Size(99, 36);
            chkOUT.TabIndex = 1;
            chkOUT.Text = "OUT";
            chkOUT.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.ColumnCount = 1;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.Controls.Add(cboToLocation, 0, 1);
            tableLayoutPanel6.Controls.Add(lblToLocation, 0, 0);
            tableLayoutPanel6.Location = new Point(617, 50);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 2;
            tableLayoutPanel6.RowStyles.Add(new RowStyle());
            tableLayoutPanel6.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.Size = new Size(200, 41);
            tableLayoutPanel6.TabIndex = 18;
            // 
            // cboToLocation
            // 
            cboToLocation.Dock = DockStyle.Fill;
            cboToLocation.DropDownStyle = ComboBoxStyle.DropDownList;
            cboToLocation.FormattingEnabled = true;
            cboToLocation.Location = new Point(0, 15);
            cboToLocation.Margin = new Padding(0);
            cboToLocation.Name = "cboToLocation";
            cboToLocation.Size = new Size(200, 23);
            cboToLocation.TabIndex = 7;
            // 
            // lblToLocation
            // 
            lblToLocation.AutoSize = true;
            lblToLocation.Dock = DockStyle.Fill;
            lblToLocation.Location = new Point(0, 0);
            lblToLocation.Margin = new Padding(0);
            lblToLocation.Name = "lblToLocation";
            lblToLocation.Size = new Size(200, 15);
            lblToLocation.TabIndex = 6;
            lblToLocation.Text = "📍 To Location";
            lblToLocation.TextAlign = ContentAlignment.BottomLeft;
            // 
            // panel2
            // 
            panel2.Controls.Add(grpDateRange);
            panel2.Location = new Point(3, 299);
            panel2.Name = "panel2";
            panel2.Size = new Size(550, 180);
            panel2.TabIndex = 21;
            // 
            // grpDateRange
            // 
            grpDateRange.Controls.Add(tableLayoutDateRange);
            grpDateRange.Location = new Point(39, 25);
            grpDateRange.Margin = new Padding(0);
            grpDateRange.Name = "grpDateRange";
            grpDateRange.Padding = new Padding(0);
            grpDateRange.Size = new Size(420, 74);
            grpDateRange.TabIndex = 12;
            grpDateRange.TabStop = false;
            grpDateRange.Text = "Date Range";
            // 
            // tableLayoutDateRange
            // 
            tableLayoutDateRange.ColumnCount = 4;
            tableLayoutDateRange.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutDateRange.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutDateRange.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutDateRange.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutDateRange.Controls.Add(rdoCustom, 3, 1);
            tableLayoutDateRange.Controls.Add(lblDateFrom, 0, 0);
            tableLayoutDateRange.Controls.Add(rdoMonth, 2, 1);
            tableLayoutDateRange.Controls.Add(dtpDateTo, 1, 0);
            tableLayoutDateRange.Controls.Add(rdoWeek, 1, 1);
            tableLayoutDateRange.Controls.Add(lblDateTo, 2, 0);
            tableLayoutDateRange.Controls.Add(rdoToday, 0, 1);
            tableLayoutDateRange.Controls.Add(dtpDateFrom, 3, 0);
            tableLayoutDateRange.Dock = DockStyle.Fill;
            tableLayoutDateRange.Location = new Point(0, 16);
            tableLayoutDateRange.Margin = new Padding(0);
            tableLayoutDateRange.Name = "tableLayoutDateRange";
            tableLayoutDateRange.RowCount = 2;
            tableLayoutDateRange.RowStyles.Add(new RowStyle());
            tableLayoutDateRange.RowStyles.Add(new RowStyle());
            tableLayoutDateRange.Size = new Size(420, 58);
            tableLayoutDateRange.TabIndex = 0;
            // 
            // rdoCustom
            // 
            rdoCustom.AutoSize = true;
            rdoCustom.Dock = DockStyle.Top;
            rdoCustom.Location = new Point(315, 23);
            rdoCustom.Margin = new Padding(0);
            rdoCustom.Name = "rdoCustom";
            rdoCustom.Size = new Size(105, 19);
            rdoCustom.TabIndex = 3;
            rdoCustom.Text = "Custom";
            rdoCustom.UseVisualStyleBackColor = true;
            // 
            // lblDateFrom
            // 
            lblDateFrom.AutoSize = true;
            lblDateFrom.Dock = DockStyle.Fill;
            lblDateFrom.Location = new Point(0, 0);
            lblDateFrom.Margin = new Padding(0);
            lblDateFrom.Name = "lblDateFrom";
            lblDateFrom.Size = new Size(105, 23);
            lblDateFrom.TabIndex = 1;
            lblDateFrom.Text = "From:";
            lblDateFrom.TextAlign = ContentAlignment.MiddleRight;
            // 
            // rdoMonth
            // 
            rdoMonth.AutoSize = true;
            rdoMonth.Checked = true;
            rdoMonth.Dock = DockStyle.Top;
            rdoMonth.Location = new Point(210, 23);
            rdoMonth.Margin = new Padding(0);
            rdoMonth.Name = "rdoMonth";
            rdoMonth.Size = new Size(105, 19);
            rdoMonth.TabIndex = 2;
            rdoMonth.TabStop = true;
            rdoMonth.Text = "Month";
            rdoMonth.UseVisualStyleBackColor = true;
            // 
            // dtpDateTo
            // 
            dtpDateTo.Dock = DockStyle.Fill;
            dtpDateTo.Format = DateTimePickerFormat.Short;
            dtpDateTo.Location = new Point(105, 0);
            dtpDateTo.Margin = new Padding(0);
            dtpDateTo.Name = "dtpDateTo";
            dtpDateTo.Size = new Size(105, 23);
            dtpDateTo.TabIndex = 4;
            // 
            // rdoWeek
            // 
            rdoWeek.AutoSize = true;
            rdoWeek.Dock = DockStyle.Top;
            rdoWeek.Location = new Point(105, 23);
            rdoWeek.Margin = new Padding(0);
            rdoWeek.Name = "rdoWeek";
            rdoWeek.Size = new Size(105, 19);
            rdoWeek.TabIndex = 1;
            rdoWeek.Text = "Week";
            rdoWeek.UseVisualStyleBackColor = true;
            // 
            // lblDateTo
            // 
            lblDateTo.AutoSize = true;
            lblDateTo.Dock = DockStyle.Fill;
            lblDateTo.Location = new Point(210, 0);
            lblDateTo.Margin = new Padding(0);
            lblDateTo.Name = "lblDateTo";
            lblDateTo.Size = new Size(105, 23);
            lblDateTo.TabIndex = 3;
            lblDateTo.Text = "To:";
            lblDateTo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // rdoToday
            // 
            rdoToday.AutoSize = true;
            rdoToday.Dock = DockStyle.Top;
            rdoToday.Location = new Point(0, 23);
            rdoToday.Margin = new Padding(0);
            rdoToday.Name = "rdoToday";
            rdoToday.Size = new Size(105, 19);
            rdoToday.TabIndex = 0;
            rdoToday.Text = "Today";
            rdoToday.UseVisualStyleBackColor = true;
            // 
            // dtpDateFrom
            // 
            dtpDateFrom.Dock = DockStyle.Fill;
            dtpDateFrom.Format = DateTimePickerFormat.Short;
            dtpDateFrom.Location = new Point(315, 0);
            dtpDateFrom.Margin = new Padding(0);
            dtpDateFrom.Name = "dtpDateFrom";
            dtpDateFrom.Size = new Size(105, 23);
            dtpDateFrom.TabIndex = 2;
            // 
            // panel3
            // 
            panel3.Controls.Add(tableLayoutPanel8);
            panel3.Location = new Point(3, 591);
            panel3.Name = "panel3";
            panel3.Size = new Size(459, 159);
            panel3.TabIndex = 22;
            // 
            // tableLayoutPanel8
            // 
            tableLayoutPanel8.ColumnCount = 3;
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel8.Controls.Add(btnSearch, 0, 0);
            tableLayoutPanel8.Controls.Add(btnReset, 2, 0);
            tableLayoutPanel8.Location = new Point(3, 3);
            tableLayoutPanel8.Name = "tableLayoutPanel8";
            tableLayoutPanel8.RowCount = 1;
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel8.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel8.Size = new Size(367, 100);
            tableLayoutPanel8.TabIndex = 0;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.FromArgb(59, 130, 246);
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(0, 0);
            btnSearch.Margin = new Padding(0);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(173, 55);
            btnSearch.TabIndex = 10;
            btnSearch.Text = "🔎 Search (F5)";
            btnSearch.UseVisualStyleBackColor = false;
            // 
            // btnReset
            // 
            btnReset.BackColor = Color.FromArgb(226, 232, 240);
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnReset.Location = new Point(193, 0);
            btnReset.Margin = new Padding(0);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(174, 55);
            btnReset.TabIndex = 15;
            btnReset.UseVisualStyleBackColor = false;
            // 
            // TransactionSearchControl
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(tableLayoutMain);
            Margin = new Padding(0);
            MinimumSize = new Size(1000, 881);
            Name = "TransactionSearchControl";
            Size = new Size(2457, 881);
            tableLayoutMain.ResumeLayout(false);
            tableLayoutMain.PerformLayout();
            tableLayoutFilters.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            tableLayoutPanel7.ResumeLayout(false);
            tableLayoutPanel7.PerformLayout();
            panel1.ResumeLayout(false);
            grpTransactionTypes.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel6.PerformLayout();
            panel2.ResumeLayout(false);
            grpDateRange.ResumeLayout(false);
            tableLayoutDateRange.ResumeLayout(false);
            tableLayoutDateRange.PerformLayout();
            panel3.ResumeLayout(false);
            tableLayoutPanel8.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

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
        private System.Windows.Forms.CheckBox chkIN;
        private System.Windows.Forms.CheckBox chkOUT;
        private System.Windows.Forms.CheckBox chkTRANSFER;
        private System.Windows.Forms.GroupBox grpDateRange;
        private System.Windows.Forms.TableLayoutPanel tableLayoutDateRange;
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
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel7;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel6;
        private TableLayoutPanel tableLayoutPanel5;
        private TableLayoutPanel tableLayoutPanel4;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private TableLayoutPanel tableLayoutPanel8;
    }
}
