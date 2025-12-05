using MTM_WIP_Application_Winforms.Models.Enums;

namespace MTM_WIP_Application_Winforms.Controls.Visual
{
    partial class Control_InventoryAudit
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
            mainLayout = new TableLayoutPanel();
            _tabControl = new TabControl();
            _tabLifecycle = new TabPage();
            pnlLifecycle = new TableLayoutPanel();
            _txtSearchBy = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            _dataGridView = new DataGridView();
            tableLayoutPanel1 = new TableLayoutPanel();
            lblLifecycleStart = new Label();
            _dtpLifecycleStart = new DateTimePicker();
            lblLifecycleEnd = new Label();
            _dtpLifecycleEnd = new DateTimePicker();
            tableLayoutPanel3 = new TableLayoutPanel();
            _btnExport = new Button();
            _btnSearch = new Button();
            _txtLifecyclePart = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            _tabUserAnalytics = new TabPage();
            pnlUserAnalytics = new TableLayoutPanel();
            tableLayoutPanel4 = new TableLayoutPanel();
            lblAnalyticsStart = new Label();
            _dtpAnalyticsStart = new DateTimePicker();
            lblAnalyticsEnd = new Label();
            _dtpAnalyticsEnd = new DateTimePicker();
            _btnLoadUsers = new Button();
            _lblUserCount = new Label();
            _btnGenerateReport = new Button();
            _btnSelectAllUsers = new Button();
            groupBox1 = new GroupBox();
            _ProcessUserAnalytics = new CheckedListBox();
            _clbUsers = new CheckedListBox();

            _flpLifecycleDateRanges = new FlowLayoutPanel();
            _rbLifecycleToday = new RadioButton();
            _rbLifecycleWeek = new RadioButton();
            _rbLifecycleMonth = new RadioButton();
            _rbLifecycleCustom = new RadioButton();

            _flpAnalyticsDateRanges = new FlowLayoutPanel();
            _rbAnalyticsToday = new RadioButton();
            _rbAnalyticsWeek = new RadioButton();
            _rbAnalyticsMonth = new RadioButton();
            _rbAnalyticsCustom = new RadioButton();

            _flpAnalyticsShifts = new FlowLayoutPanel();
            _cbShift1 = new CheckBox();
            _cbShift2 = new CheckBox();
            _cbShift3 = new CheckBox();
            _cbShiftWeekend = new CheckBox();

            mainLayout.SuspendLayout();
            _tabControl.SuspendLayout();
            _tabLifecycle.SuspendLayout();
            pnlLifecycle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dataGridView).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            _tabUserAnalytics.SuspendLayout();
            pnlUserAnalytics.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // mainLayout
            // 
            mainLayout.AutoSize = true;
            mainLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            mainLayout.ColumnCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.Controls.Add(_tabControl, 0, 0);
            mainLayout.Location = new Point(0, 0);
            mainLayout.Name = "mainLayout";
            mainLayout.RowCount = 3;
            mainLayout.RowStyles.Add(new RowStyle());
            mainLayout.RowStyles.Add(new RowStyle());
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.Size = new Size(800, 506);
            mainLayout.TabIndex = 0;
            // 
            // _tabControl
            // 
            _tabControl.Controls.Add(_tabLifecycle);
            _tabControl.Controls.Add(_tabUserAnalytics);
            _tabControl.Dock = DockStyle.Fill;
            _tabControl.Location = new Point(3, 3);
            _tabControl.Name = "_tabControl";
            _tabControl.SelectedIndex = 0;
            _tabControl.Size = new Size(794, 500);
            _tabControl.TabIndex = 0;
            // 
            // _tabLifecycle
            // 
            _tabLifecycle.Controls.Add(pnlLifecycle);
            _tabLifecycle.Location = new Point(4, 24);
            _tabLifecycle.Name = "_tabLifecycle";
            _tabLifecycle.Padding = new Padding(3);
            _tabLifecycle.Size = new Size(786, 472);
            _tabLifecycle.TabIndex = 0;
            _tabLifecycle.Text = "History Viewer";
            _tabLifecycle.UseVisualStyleBackColor = true;
            // 
            // pnlLifecycle
            // 
            pnlLifecycle.AutoSize = true;
            pnlLifecycle.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlLifecycle.ColumnCount = 1;
            pnlLifecycle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            pnlLifecycle.Controls.Add(_txtSearchBy, 0, 0);
            pnlLifecycle.Controls.Add(_txtLifecyclePart, 0, 1);
            pnlLifecycle.Controls.Add(_flpLifecycleDateRanges, 0, 2);
            pnlLifecycle.Controls.Add(tableLayoutPanel1, 0, 3);
            pnlLifecycle.Controls.Add(tableLayoutPanel3, 0, 4);
            pnlLifecycle.Controls.Add(_dataGridView, 0, 5);
            pnlLifecycle.Dock = DockStyle.Fill;
            pnlLifecycle.Location = new Point(3, 3);
            pnlLifecycle.Name = "pnlLifecycle";
            pnlLifecycle.Padding = new Padding(10);
            pnlLifecycle.RowCount = 6;
            pnlLifecycle.RowStyles.Add(new RowStyle());
            pnlLifecycle.RowStyles.Add(new RowStyle());
            pnlLifecycle.RowStyles.Add(new RowStyle());
            pnlLifecycle.RowStyles.Add(new RowStyle());
            pnlLifecycle.RowStyles.Add(new RowStyle());
            pnlLifecycle.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            pnlLifecycle.Size = new Size(780, 466);
            pnlLifecycle.TabIndex = 0;
            // 
            // _flpLifecycleDateRanges
            // 
            _flpLifecycleDateRanges.AutoSize = true;
            _flpLifecycleDateRanges.Controls.Add(_rbLifecycleToday);
            _flpLifecycleDateRanges.Controls.Add(_rbLifecycleWeek);
            _flpLifecycleDateRanges.Controls.Add(_rbLifecycleMonth);
            _flpLifecycleDateRanges.Controls.Add(_rbLifecycleCustom);
            _flpLifecycleDateRanges.Dock = DockStyle.Fill;
            _flpLifecycleDateRanges.Location = new Point(13, 71);
            _flpLifecycleDateRanges.Name = "_flpLifecycleDateRanges";
            _flpLifecycleDateRanges.Size = new Size(754, 29);
            _flpLifecycleDateRanges.TabIndex = 3;
            // 
            // _rbLifecycleToday
            // 
            _rbLifecycleToday.AutoSize = true;
            _rbLifecycleToday.Location = new Point(3, 3);
            _rbLifecycleToday.Name = "_rbLifecycleToday";
            _rbLifecycleToday.Size = new Size(56, 19);
            _rbLifecycleToday.TabIndex = 0;
            _rbLifecycleToday.Text = "Today";
            _rbLifecycleToday.UseVisualStyleBackColor = true;
            // 
            // _rbLifecycleWeek
            // 
            _rbLifecycleWeek.AutoSize = true;
            _rbLifecycleWeek.Location = new Point(65, 3);
            _rbLifecycleWeek.Name = "_rbLifecycleWeek";
            _rbLifecycleWeek.Size = new Size(80, 19);
            _rbLifecycleWeek.TabIndex = 1;
            _rbLifecycleWeek.Text = "Last 7 Days";
            _rbLifecycleWeek.UseVisualStyleBackColor = true;
            // 
            // _rbLifecycleMonth
            // 
            _rbLifecycleMonth.AutoSize = true;
            _rbLifecycleMonth.Checked = true;
            _rbLifecycleMonth.Location = new Point(151, 3);
            _rbLifecycleMonth.Name = "_rbLifecycleMonth";
            _rbLifecycleMonth.Size = new Size(86, 19);
            _rbLifecycleMonth.TabIndex = 2;
            _rbLifecycleMonth.TabStop = true;
            _rbLifecycleMonth.Text = "Last 30 Days";
            _rbLifecycleMonth.UseVisualStyleBackColor = true;
            // 
            // _rbLifecycleCustom
            // 
            _rbLifecycleCustom.AutoSize = true;
            _rbLifecycleCustom.Location = new Point(243, 3);
            _rbLifecycleCustom.Name = "_rbLifecycleCustom";
            _rbLifecycleCustom.Size = new Size(67, 19);
            _rbLifecycleCustom.TabIndex = 3;
            _rbLifecycleCustom.Text = "Custom";
            _rbLifecycleCustom.UseVisualStyleBackColor = true;
            // 
            // _txtSearchBy
            // 
            _txtSearchBy.AutoSize = true;
            _txtSearchBy.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _txtSearchBy.Dock = DockStyle.Fill;
            _txtSearchBy.LabelText = "Search By";
            _txtSearchBy.LabelVisibility = Enum_LabelVisibility.Hidden;
            _txtSearchBy.Location = new Point(13, 13);
            _txtSearchBy.MaxLength = 130;
            _txtSearchBy.MinimumSize = new Size(0, 23);
            _txtSearchBy.MinLength = 130;
            _txtSearchBy.Name = "_txtSearchBy";
            _txtSearchBy.PlaceholderText = "Enter Search Filter";
            _txtSearchBy.ShowValidationColor = false;
            _txtSearchBy.Size = new Size(754, 23);
            _txtSearchBy.TabIndex = 2;
            _txtSearchBy.ValidatorType = null;
            _txtSearchBy.Visible = false;
            // 
            // _dataGridView
            // 
            _dataGridView.AllowUserToAddRows = false;
            _dataGridView.AllowUserToDeleteRows = false;
            _dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _dataGridView.Dock = DockStyle.Fill;
            _dataGridView.Location = new Point(13, 181);
            _dataGridView.Name = "_dataGridView";
            _dataGridView.ReadOnly = true;
            _dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dataGridView.Size = new Size(754, 272);
            _dataGridView.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.0000076F));
            tableLayoutPanel1.Controls.Add(lblLifecycleStart, 0, 0);
            tableLayoutPanel1.Controls.Add(_dtpLifecycleStart, 0, 1);
            tableLayoutPanel1.Controls.Add(lblLifecycleEnd, 1, 1);
            tableLayoutPanel1.Controls.Add(_dtpLifecycleEnd, 2, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(13, 71);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(754, 52);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // lblLifecycleStart
            // 
            lblLifecycleStart.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(lblLifecycleStart, 3);
            lblLifecycleStart.Dock = DockStyle.Fill;
            lblLifecycleStart.Font = new Font("Segoe UI Emoji", 9.75F);
            lblLifecycleStart.Location = new Point(3, 3);
            lblLifecycleStart.Margin = new Padding(3);
            lblLifecycleStart.Name = "lblLifecycleStart";
            lblLifecycleStart.Size = new Size(748, 17);
            lblLifecycleStart.TabIndex = 0;
            lblLifecycleStart.Text = "📅 Date Range";
            lblLifecycleStart.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _dtpLifecycleStart
            // 
            _dtpLifecycleStart.Dock = DockStyle.Fill;
            _dtpLifecycleStart.Format = DateTimePickerFormat.Short;
            _dtpLifecycleStart.Location = new Point(3, 26);
            _dtpLifecycleStart.Name = "_dtpLifecycleStart";
            _dtpLifecycleStart.Size = new Size(357, 23);
            _dtpLifecycleStart.TabIndex = 1;
            // 
            // lblLifecycleEnd
            // 
            lblLifecycleEnd.AutoSize = true;
            lblLifecycleEnd.Dock = DockStyle.Fill;
            lblLifecycleEnd.Location = new Point(366, 26);
            lblLifecycleEnd.Margin = new Padding(3);
            lblLifecycleEnd.Name = "lblLifecycleEnd";
            lblLifecycleEnd.Size = new Size(20, 23);
            lblLifecycleEnd.TabIndex = 2;
            lblLifecycleEnd.Text = "To";
            lblLifecycleEnd.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _dtpLifecycleEnd
            // 
            _dtpLifecycleEnd.Dock = DockStyle.Fill;
            _dtpLifecycleEnd.Format = DateTimePickerFormat.Short;
            _dtpLifecycleEnd.Location = new Point(392, 26);
            _dtpLifecycleEnd.Name = "_dtpLifecycleEnd";
            _dtpLifecycleEnd.Size = new Size(359, 23);
            _dtpLifecycleEnd.TabIndex = 3;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.AutoSize = true;
            tableLayoutPanel3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel3.ColumnCount = 3;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(_btnExport, 1, 0);
            tableLayoutPanel3.Controls.Add(_btnSearch, 0, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(13, 129);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(754, 46);
            tableLayoutPanel3.TabIndex = 2;
            // 
            // _btnExport
            // 
            _btnExport.AutoSize = true;
            _btnExport.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _btnExport.Dock = DockStyle.Fill;
            _btnExport.Location = new Point(109, 3);
            _btnExport.MaximumSize = new Size(100, 40);
            _btnExport.MinimumSize = new Size(100, 40);
            _btnExport.Name = "_btnExport";
            _btnExport.Size = new Size(100, 40);
            _btnExport.TabIndex = 1;
            _btnExport.Text = "Export to Excel";
            _btnExport.UseVisualStyleBackColor = true;
            // 
            // _btnSearch
            // 
            _btnSearch.AutoSize = true;
            _btnSearch.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _btnSearch.Dock = DockStyle.Fill;
            _btnSearch.Location = new Point(3, 3);
            _btnSearch.MaximumSize = new Size(100, 40);
            _btnSearch.MinimumSize = new Size(100, 40);
            _btnSearch.Name = "_btnSearch";
            _btnSearch.Size = new Size(100, 40);
            _btnSearch.TabIndex = 0;
            _btnSearch.Text = "Search";
            _btnSearch.UseVisualStyleBackColor = true;
            // 
            // _txtLifecyclePart
            // 
            _txtLifecyclePart.AutoSize = true;
            _txtLifecyclePart.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _txtLifecyclePart.Dock = DockStyle.Fill;
            _txtLifecyclePart.LabelText = "Enter Part ID";
            _txtLifecyclePart.LabelVisibility = Enum_LabelVisibility.Hidden;
            _txtLifecyclePart.Location = new Point(13, 42);
            _txtLifecyclePart.MaxLength = 130;
            _txtLifecyclePart.MinimumSize = new Size(0, 23);
            _txtLifecyclePart.MinLength = 130;
            _txtLifecyclePart.Name = "_txtLifecyclePart";
            _txtLifecyclePart.PlaceholderText = "Enter Part Number";
            _txtLifecyclePart.ShowValidationColor = false;
            _txtLifecyclePart.Size = new Size(754, 23);
            _txtLifecyclePart.TabIndex = 0;
            _txtLifecyclePart.ValidatorType = null;
            // 
            // _tabUserAnalytics
            // 
            _tabUserAnalytics.Controls.Add(pnlUserAnalytics);
            _tabUserAnalytics.Location = new Point(4, 24);
            _tabUserAnalytics.Name = "_tabUserAnalytics";
            _tabUserAnalytics.Padding = new Padding(3);
            _tabUserAnalytics.Size = new Size(786, 472);
            _tabUserAnalytics.TabIndex = 6;
            _tabUserAnalytics.Text = "User Analytics";
            _tabUserAnalytics.UseVisualStyleBackColor = true;
            // 
            // pnlUserAnalytics
            // 
            pnlUserAnalytics.AutoSize = true;
            pnlUserAnalytics.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlUserAnalytics.ColumnCount = 1;
            pnlUserAnalytics.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            pnlUserAnalytics.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            pnlUserAnalytics.Controls.Add(tableLayoutPanel4, 0, 0);
            pnlUserAnalytics.Controls.Add(_clbUsers, 0, 1);
            pnlUserAnalytics.Dock = DockStyle.Fill;
            pnlUserAnalytics.Location = new Point(3, 3);
            pnlUserAnalytics.Name = "pnlUserAnalytics";
            pnlUserAnalytics.Padding = new Padding(10);
            pnlUserAnalytics.RowCount = 2;
            pnlUserAnalytics.RowStyles.Add(new RowStyle());
            pnlUserAnalytics.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            pnlUserAnalytics.Size = new Size(780, 466);
            pnlUserAnalytics.TabIndex = 1;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.AutoSize = true;
            tableLayoutPanel4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel4.ColumnCount = 3;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tableLayoutPanel4.Controls.Add(_flpAnalyticsDateRanges, 0, 0);
            tableLayoutPanel4.SetColumnSpan(_flpAnalyticsDateRanges, 2);
            tableLayoutPanel4.Controls.Add(lblAnalyticsStart, 0, 1);
            tableLayoutPanel4.Controls.Add(_dtpAnalyticsStart, 1, 1);
            tableLayoutPanel4.Controls.Add(lblAnalyticsEnd, 0, 2);
            tableLayoutPanel4.Controls.Add(_dtpAnalyticsEnd, 1, 2);
            tableLayoutPanel4.Controls.Add(_flpAnalyticsShifts, 0, 3);
            tableLayoutPanel4.SetColumnSpan(_flpAnalyticsShifts, 2);
            tableLayoutPanel4.Controls.Add(_btnLoadUsers, 0, 4);
            tableLayoutPanel4.SetColumnSpan(_btnLoadUsers, 2);
            tableLayoutPanel4.Controls.Add(_btnSelectAllUsers, 0, 5);
            tableLayoutPanel4.SetColumnSpan(_btnSelectAllUsers, 2);
            tableLayoutPanel4.Controls.Add(_btnGenerateReport, 0, 6);
            tableLayoutPanel4.SetColumnSpan(_btnGenerateReport, 2);
            tableLayoutPanel4.Controls.Add(_lblUserCount, 0, 7);
            tableLayoutPanel4.SetColumnSpan(_lblUserCount, 2);
            tableLayoutPanel4.Controls.Add(groupBox1, 2, 0);
            tableLayoutPanel4.SetRowSpan(groupBox1, 8);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(13, 13);
            tableLayoutPanel4.Margin = new Padding(3, 3, 10, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 8;
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Size = new Size(747, 193);
            tableLayoutPanel4.TabIndex = 4;
            // 
            // _flpAnalyticsDateRanges
            // 
            _flpAnalyticsDateRanges.AutoSize = true;
            _flpAnalyticsDateRanges.Controls.Add(_rbAnalyticsToday);
            _flpAnalyticsDateRanges.Controls.Add(_rbAnalyticsWeek);
            _flpAnalyticsDateRanges.Controls.Add(_rbAnalyticsMonth);
            _flpAnalyticsDateRanges.Controls.Add(_rbAnalyticsCustom);
            _flpAnalyticsDateRanges.Dock = DockStyle.Fill;
            _flpAnalyticsDateRanges.Location = new Point(3, 3);
            _flpAnalyticsDateRanges.Name = "_flpAnalyticsDateRanges";
            _flpAnalyticsDateRanges.Size = new Size(367, 29);
            _flpAnalyticsDateRanges.TabIndex = 0;
            // 
            // _rbAnalyticsToday
            // 
            _rbAnalyticsToday.AutoSize = true;
            _rbAnalyticsToday.Location = new Point(3, 3);
            _rbAnalyticsToday.Name = "_rbAnalyticsToday";
            _rbAnalyticsToday.Size = new Size(56, 19);
            _rbAnalyticsToday.TabIndex = 0;
            _rbAnalyticsToday.Text = "Today";
            _rbAnalyticsToday.UseVisualStyleBackColor = true;
            // 
            // _rbAnalyticsWeek
            // 
            _rbAnalyticsWeek.AutoSize = true;
            _rbAnalyticsWeek.Location = new Point(65, 3);
            _rbAnalyticsWeek.Name = "_rbAnalyticsWeek";
            _rbAnalyticsWeek.Size = new Size(80, 19);
            _rbAnalyticsWeek.TabIndex = 1;
            _rbAnalyticsWeek.Text = "Last 7 Days";
            _rbAnalyticsWeek.UseVisualStyleBackColor = true;
            // 
            // _rbAnalyticsMonth
            // 
            _rbAnalyticsMonth.AutoSize = true;
            _rbAnalyticsMonth.Checked = true;
            _rbAnalyticsMonth.Location = new Point(151, 3);
            _rbAnalyticsMonth.Name = "_rbAnalyticsMonth";
            _rbAnalyticsMonth.Size = new Size(86, 19);
            _rbAnalyticsMonth.TabIndex = 2;
            _rbAnalyticsMonth.TabStop = true;
            _rbAnalyticsMonth.Text = "Last 30 Days";
            _rbAnalyticsMonth.UseVisualStyleBackColor = true;
            // 
            // _rbAnalyticsCustom
            // 
            _rbAnalyticsCustom.AutoSize = true;
            _rbAnalyticsCustom.Location = new Point(243, 3);
            _rbAnalyticsCustom.Name = "_rbAnalyticsCustom";
            _rbAnalyticsCustom.Size = new Size(67, 19);
            _rbAnalyticsCustom.TabIndex = 3;
            _rbAnalyticsCustom.Text = "Custom";
            _rbAnalyticsCustom.UseVisualStyleBackColor = true;
            // 
            // _flpAnalyticsShifts
            // 
            _flpAnalyticsShifts.AutoSize = true;
            _flpAnalyticsShifts.Controls.Add(_cbShift1);
            _flpAnalyticsShifts.Controls.Add(_cbShift2);
            _flpAnalyticsShifts.Controls.Add(_cbShift3);
            _flpAnalyticsShifts.Controls.Add(_cbShiftWeekend);
            _flpAnalyticsShifts.Dock = DockStyle.Fill;
            _flpAnalyticsShifts.Location = new Point(3, 90);
            _flpAnalyticsShifts.Name = "_flpAnalyticsShifts";
            _flpAnalyticsShifts.Size = new Size(367, 29);
            _flpAnalyticsShifts.TabIndex = 13;
            // 
            // _cbShift1
            // 
            _cbShift1.AutoSize = true;
            _cbShift1.Checked = true;
            _cbShift1.CheckState = CheckState.Checked;
            _cbShift1.Location = new Point(3, 3);
            _cbShift1.Name = "_cbShift1";
            _cbShift1.Size = new Size(60, 19);
            _cbShift1.TabIndex = 0;
            _cbShift1.Text = "Shift 1";
            _cbShift1.UseVisualStyleBackColor = true;
            // 
            // _cbShift2
            // 
            _cbShift2.AutoSize = true;
            _cbShift2.Checked = true;
            _cbShift2.CheckState = CheckState.Checked;
            _cbShift2.Location = new Point(69, 3);
            _cbShift2.Name = "_cbShift2";
            _cbShift2.Size = new Size(60, 19);
            _cbShift2.TabIndex = 1;
            _cbShift2.Text = "Shift 2";
            _cbShift2.UseVisualStyleBackColor = true;
            // 
            // _cbShift3
            // 
            _cbShift3.AutoSize = true;
            _cbShift3.Checked = true;
            _cbShift3.CheckState = CheckState.Checked;
            _cbShift3.Location = new Point(135, 3);
            _cbShift3.Name = "_cbShift3";
            _cbShift3.Size = new Size(60, 19);
            _cbShift3.TabIndex = 2;
            _cbShift3.Text = "Shift 3";
            _cbShift3.UseVisualStyleBackColor = true;
            // 
            // _cbShiftWeekend
            // 
            _cbShiftWeekend.AutoSize = true;
            _cbShiftWeekend.Checked = true;
            _cbShiftWeekend.CheckState = CheckState.Checked;
            _cbShiftWeekend.Location = new Point(201, 3);
            _cbShiftWeekend.Name = "_cbShiftWeekend";
            _cbShiftWeekend.Size = new Size(76, 19);
            _cbShiftWeekend.TabIndex = 3;
            _cbShiftWeekend.Text = "Weekend";
            _cbShiftWeekend.UseVisualStyleBackColor = true;
            // 
            // lblAnalyticsStart
            // 
            lblAnalyticsStart.AutoSize = true;
            lblAnalyticsStart.Dock = DockStyle.Fill;
            lblAnalyticsStart.Location = new Point(3, 3);
            lblAnalyticsStart.Margin = new Padding(3);
            lblAnalyticsStart.Name = "lblAnalyticsStart";
            lblAnalyticsStart.Size = new Size(61, 23);
            lblAnalyticsStart.TabIndex = 6;
            lblAnalyticsStart.Text = "Start Date:";
            lblAnalyticsStart.TextAlign = ContentAlignment.MiddleRight;
            // 
            // _dtpAnalyticsStart
            // 
            _dtpAnalyticsStart.Dock = DockStyle.Fill;
            _dtpAnalyticsStart.Format = DateTimePickerFormat.Short;
            _dtpAnalyticsStart.Location = new Point(70, 3);
            _dtpAnalyticsStart.Name = "_dtpAnalyticsStart";
            _dtpAnalyticsStart.Size = new Size(300, 23);
            _dtpAnalyticsStart.TabIndex = 7;
            // 
            // lblAnalyticsEnd
            // 
            lblAnalyticsEnd.AutoSize = true;
            lblAnalyticsEnd.Dock = DockStyle.Fill;
            lblAnalyticsEnd.Location = new Point(3, 32);
            lblAnalyticsEnd.Margin = new Padding(3);
            lblAnalyticsEnd.Name = "lblAnalyticsEnd";
            lblAnalyticsEnd.Size = new Size(61, 23);
            lblAnalyticsEnd.TabIndex = 8;
            lblAnalyticsEnd.Text = "End Date:";
            lblAnalyticsEnd.TextAlign = ContentAlignment.MiddleRight;
            // 
            // _dtpAnalyticsEnd
            // 
            _dtpAnalyticsEnd.Dock = DockStyle.Fill;
            _dtpAnalyticsEnd.Format = DateTimePickerFormat.Short;
            _dtpAnalyticsEnd.Location = new Point(70, 32);
            _dtpAnalyticsEnd.Name = "_dtpAnalyticsEnd";
            _dtpAnalyticsEnd.Size = new Size(300, 23);
            _dtpAnalyticsEnd.TabIndex = 9;
            // 
            // _btnLoadUsers
            // 
            tableLayoutPanel4.SetColumnSpan(_btnLoadUsers, 2);
            _btnLoadUsers.Dock = DockStyle.Fill;
            _btnLoadUsers.Location = new Point(3, 61);
            _btnLoadUsers.Name = "_btnLoadUsers";
            _btnLoadUsers.Size = new Size(367, 32);
            _btnLoadUsers.TabIndex = 10;
            _btnLoadUsers.Text = "Load Users";
            _btnLoadUsers.UseVisualStyleBackColor = true;
            // 
            // _lblUserCount
            // 
            _lblUserCount.AutoSize = true;
            tableLayoutPanel4.SetColumnSpan(_lblUserCount, 2);
            _lblUserCount.Dock = DockStyle.Fill;
            _lblUserCount.Location = new Point(3, 175);
            _lblUserCount.Margin = new Padding(3);
            _lblUserCount.Name = "_lblUserCount";
            _lblUserCount.Size = new Size(367, 15);
            _lblUserCount.TabIndex = 6;
            _lblUserCount.Text = "Selected: 0";
            _lblUserCount.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _btnGenerateReport
            // 
            tableLayoutPanel4.SetColumnSpan(_btnGenerateReport, 2);
            _btnGenerateReport.Dock = DockStyle.Fill;
            _btnGenerateReport.Enabled = false;
            _btnGenerateReport.Location = new Point(3, 137);
            _btnGenerateReport.Name = "_btnGenerateReport";
            _btnGenerateReport.Size = new Size(367, 32);
            _btnGenerateReport.TabIndex = 2;
            _btnGenerateReport.Text = "Generate Report";
            _btnGenerateReport.UseVisualStyleBackColor = true;
            // 
            // _btnSelectAllUsers
            // 
            tableLayoutPanel4.SetColumnSpan(_btnSelectAllUsers, 2);
            _btnSelectAllUsers.Dock = DockStyle.Fill;
            _btnSelectAllUsers.Location = new Point(3, 99);
            _btnSelectAllUsers.Name = "_btnSelectAllUsers";
            _btnSelectAllUsers.Size = new Size(367, 32);
            _btnSelectAllUsers.TabIndex = 11;
            _btnSelectAllUsers.Text = "Select All Users";
            _btnSelectAllUsers.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(_ProcessUserAnalytics);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(379, 6);
            groupBox1.Margin = new Padding(6);
            groupBox1.Name = "groupBox1";
            tableLayoutPanel4.SetRowSpan(groupBox1, 8);
            groupBox1.Size = new Size(362, 181);
            groupBox1.TabIndex = 12;
            groupBox1.TabStop = false;
            groupBox1.Text = "Using This Tab";
            // 
            // _ProcessUserAnalytics
            // 
            _ProcessUserAnalytics.Dock = DockStyle.Fill;
            _ProcessUserAnalytics.FormattingEnabled = true;
            _ProcessUserAnalytics.Items.AddRange(new object[] { "Step 1: Enter Desired Date Range", "Step 2: Click Load Users", "Step 3 (Optional): Click Select All Users", "Step 4: Select Users Below", "Step 5: Click Generate Report (Will Open a new Window)" });
            _ProcessUserAnalytics.Location = new Point(3, 19);
            _ProcessUserAnalytics.Name = "_ProcessUserAnalytics";
            _ProcessUserAnalytics.Size = new Size(356, 159);
            _ProcessUserAnalytics.TabIndex = 0;
            // 
            // _clbUsers
            // 
            _clbUsers.Dock = DockStyle.Fill;
            _clbUsers.FormattingEnabled = true;
            _clbUsers.Location = new Point(16, 215);
            _clbUsers.Margin = new Padding(6);
            _clbUsers.Name = "_clbUsers";
            _clbUsers.Size = new Size(748, 235);
            _clbUsers.TabIndex = 5;
            // 
            // Control_InventoryAudit
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(mainLayout);
            Name = "Control_InventoryAudit";
            Size = new Size(803, 509);
            mainLayout.ResumeLayout(false);
            _tabControl.ResumeLayout(false);
            _tabLifecycle.ResumeLayout(false);
            _tabLifecycle.PerformLayout();
            pnlLifecycle.ResumeLayout(false);
            pnlLifecycle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_dataGridView).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            _tabUserAnalytics.ResumeLayout(false);
            _tabUserAnalytics.PerformLayout();
            pnlUserAnalytics.ResumeLayout(false);
            pnlUserAnalytics.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainLayout;
        private System.Windows.Forms.TabControl _tabControl;
        private System.Windows.Forms.TabPage _tabLifecycle;
        private System.Windows.Forms.TableLayoutPanel pnlLifecycle;
        private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel _txtLifecyclePart;
        private System.Windows.Forms.Label lblLifecycleStart;
        private System.Windows.Forms.DateTimePicker _dtpLifecycleStart;
        private System.Windows.Forms.Label lblLifecycleEnd;
        private System.Windows.Forms.DateTimePicker _dtpLifecycleEnd;
        private System.Windows.Forms.TabPage _tabUserAnalytics;
        private System.Windows.Forms.Button _btnSearch;
        private System.Windows.Forms.Button _btnExport;
        private System.Windows.Forms.DataGridView _dataGridView;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel3;
        private Shared.SuggestionTextBoxWithLabel _txtSearchBy;
        private TableLayoutPanel pnlUserAnalytics;
        private TableLayoutPanel tableLayoutPanel4;
        private Label lblAnalyticsStart;
        private DateTimePicker _dtpAnalyticsStart;
        private Label lblAnalyticsEnd;
        private DateTimePicker _dtpAnalyticsEnd;
        private Label _lblUserCount;
        private Button _btnGenerateReport;
        private Button _btnSelectAllUsers;
        private Button _btnLoadUsers;
        private CheckedListBox _clbUsers;
        private GroupBox groupBox1;
        private CheckedListBox _ProcessUserAnalytics;

        // Lifecycle Date Ranges
        private FlowLayoutPanel _flpLifecycleDateRanges;
        private RadioButton _rbLifecycleToday;
        private RadioButton _rbLifecycleWeek;
        private RadioButton _rbLifecycleMonth;
        private RadioButton _rbLifecycleCustom;

        // Analytics Date Ranges
        private FlowLayoutPanel _flpAnalyticsDateRanges;
        private RadioButton _rbAnalyticsToday;
        private RadioButton _rbAnalyticsWeek;
        private RadioButton _rbAnalyticsMonth;
        private RadioButton _rbAnalyticsCustom;

        // Analytics Shift Filters
        private FlowLayoutPanel _flpAnalyticsShifts;
        private CheckBox _cbShift1;
        private CheckBox _cbShift2;
        private CheckBox _cbShift3;
        private CheckBox _cbShiftWeekend;
    }
}
