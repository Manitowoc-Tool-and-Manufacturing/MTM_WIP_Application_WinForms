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
            _txtLifecyclePart = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            lblLifecycleStart = new Label();
            _dtpLifecycleStart = new DateTimePicker();
            lblLifecycleEnd = new Label();
            _dtpLifecycleEnd = new DateTimePicker();
            _tabByPart = new TabPage();
            pnlByPart = new TableLayoutPanel();
            _txtByPartPart = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            _tabByUser = new TabPage();
            pnlByUser = new TableLayoutPanel();
            _txtByUserUser = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            lblByUserStart = new Label();
            _dtpByUserStart = new DateTimePicker();
            lblByUserEnd = new Label();
            _dtpByUserEnd = new DateTimePicker();
            _tabByWO = new TabPage();
            pnlByWO = new TableLayoutPanel();
            _txtByWOWO = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            _tabByCO = new TabPage();
            pnlByCO = new TableLayoutPanel();
            _txtByCOCO = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            _tabByPO = new TabPage();
            pnlByPO = new TableLayoutPanel();
            _txtByPOPO = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            _tabUserAnalytics = new TabPage();
            pnlUserAnalytics = new TableLayoutPanel();
            tableLayoutPanel4 = new TableLayoutPanel();
            lblAnalyticsStart = new Label();
            _dtpAnalyticsStart = new DateTimePicker();
            lblAnalyticsEnd = new Label();
            _dtpAnalyticsEnd = new DateTimePicker();
            _btnLoadUsers = new Button();
            _clbUsers = new CheckedListBox();
            _btnGenerateReport = new Button();
            _lblUserCount = new Label();
            _btnSearch = new Button();
            _btnExport = new Button();
            _dataGridView = new DataGridView();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            mainLayout.SuspendLayout();
            _tabControl.SuspendLayout();
            _tabLifecycle.SuspendLayout();
            pnlLifecycle.SuspendLayout();
            _tabByPart.SuspendLayout();
            pnlByPart.SuspendLayout();
            _tabByUser.SuspendLayout();
            pnlByUser.SuspendLayout();
            _tabByWO.SuspendLayout();
            pnlByWO.SuspendLayout();
            _tabByCO.SuspendLayout();
            pnlByCO.SuspendLayout();
            _tabByPO.SuspendLayout();
            pnlByPO.SuspendLayout();
            _tabUserAnalytics.SuspendLayout();
            pnlUserAnalytics.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dataGridView).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // mainLayout
            // 
            mainLayout.AutoSize = true;
            mainLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            mainLayout.ColumnCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.Controls.Add(_tabControl, 0, 0);
            mainLayout.Controls.Add(_dataGridView, 0, 2);
            mainLayout.Controls.Add(tableLayoutPanel3, 0, 1);
            mainLayout.Location = new Point(0, 0);
            mainLayout.Name = "mainLayout";
            mainLayout.RowCount = 3;
            mainLayout.RowStyles.Add(new RowStyle());
            mainLayout.RowStyles.Add(new RowStyle());
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.Size = new Size(800, 612);
            mainLayout.TabIndex = 0;
            // 
            // _tabControl
            // 
            _tabControl.Controls.Add(_tabLifecycle);
            _tabControl.Controls.Add(_tabByPart);
            _tabControl.Controls.Add(_tabByUser);
            _tabControl.Controls.Add(_tabByWO);
            _tabControl.Controls.Add(_tabByCO);
            _tabControl.Controls.Add(_tabByPO);
            _tabControl.Controls.Add(_tabUserAnalytics);
            _tabControl.Dock = DockStyle.Fill;
            _tabControl.Location = new Point(3, 3);
            _tabControl.Name = "_tabControl";
            _tabControl.SelectedIndex = 0;
            _tabControl.Size = new Size(794, 200);
            _tabControl.TabIndex = 0;
            // 
            // _tabLifecycle
            // 
            _tabLifecycle.Controls.Add(pnlLifecycle);
            _tabLifecycle.Location = new Point(4, 24);
            _tabLifecycle.Name = "_tabLifecycle";
            _tabLifecycle.Padding = new Padding(3);
            _tabLifecycle.Size = new Size(786, 106);
            _tabLifecycle.TabIndex = 0;
            _tabLifecycle.Text = "Lifecycle View";
            _tabLifecycle.UseVisualStyleBackColor = true;
            // 
            // pnlLifecycle
            // 
            pnlLifecycle.AutoSize = true;
            pnlLifecycle.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlLifecycle.ColumnCount = 2;
            pnlLifecycle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            pnlLifecycle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            pnlLifecycle.Controls.Add(tableLayoutPanel1, 0, 1);
            pnlLifecycle.Controls.Add(_txtLifecyclePart, 0, 0);
            pnlLifecycle.Dock = DockStyle.Fill;
            pnlLifecycle.Location = new Point(3, 3);
            pnlLifecycle.Name = "pnlLifecycle";
            pnlLifecycle.Padding = new Padding(10);
            pnlLifecycle.RowCount = 2;
            pnlLifecycle.RowStyles.Add(new RowStyle());
            pnlLifecycle.RowStyles.Add(new RowStyle());
            pnlLifecycle.Size = new Size(780, 100);
            pnlLifecycle.TabIndex = 0;
            // 
            // _txtLifecyclePart
            // 
            _txtLifecyclePart.AutoSize = true;
            _txtLifecyclePart.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _txtLifecyclePart.Dock = DockStyle.Fill;
            _txtLifecyclePart.LabelText = "Part ID";
            _txtLifecyclePart.LabelVisible = "False";
            _txtLifecyclePart.Location = new Point(13, 13);
            _txtLifecyclePart.MaxLength = 130;
            _txtLifecyclePart.MinimumSize = new Size(0, 23);
            _txtLifecyclePart.MinLength = 130;
            _txtLifecyclePart.Name = "_txtLifecyclePart";
            _txtLifecyclePart.PlaceholderText = "Enter Part Number";
            _txtLifecyclePart.ShowF4Button = false;
            _txtLifecyclePart.ShowValidationColor = false;
            _txtLifecyclePart.Size = new Size(374, 23);
            _txtLifecyclePart.TabIndex = 0;
            _txtLifecyclePart.ValidatorType = null;
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
            lblLifecycleStart.Size = new Size(368, 17);
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
            _dtpLifecycleStart.Size = new Size(167, 23);
            _dtpLifecycleStart.TabIndex = 1;
            // 
            // lblLifecycleEnd
            // 
            lblLifecycleEnd.AutoSize = true;
            lblLifecycleEnd.Dock = DockStyle.Fill;
            lblLifecycleEnd.Location = new Point(176, 26);
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
            _dtpLifecycleEnd.Location = new Point(202, 26);
            _dtpLifecycleEnd.Name = "_dtpLifecycleEnd";
            _dtpLifecycleEnd.Size = new Size(169, 23);
            _dtpLifecycleEnd.TabIndex = 3;
            // 
            // _tabByPart
            // 
            _tabByPart.Controls.Add(pnlByPart);
            _tabByPart.Location = new Point(4, 24);
            _tabByPart.Name = "_tabByPart";
            _tabByPart.Padding = new Padding(3);
            _tabByPart.Size = new Size(786, 106);
            _tabByPart.TabIndex = 1;
            _tabByPart.Text = "By Part Number";
            _tabByPart.UseVisualStyleBackColor = true;
            // 
            // pnlByPart
            // 
            pnlByPart.AutoSize = true;
            pnlByPart.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlByPart.ColumnCount = 2;
            pnlByPart.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            pnlByPart.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            pnlByPart.Controls.Add(_txtByPartPart, 0, 0);
            pnlByPart.Dock = DockStyle.Fill;
            pnlByPart.Location = new Point(3, 3);
            pnlByPart.Name = "pnlByPart";
            pnlByPart.Padding = new Padding(10);
            pnlByPart.RowCount = 2;
            pnlByPart.RowStyles.Add(new RowStyle());
            pnlByPart.RowStyles.Add(new RowStyle());
            pnlByPart.Size = new Size(780, 100);
            pnlByPart.TabIndex = 0;
            // 
            // _txtByPartPart
            // 
            _txtByPartPart.AutoSize = true;
            _txtByPartPart.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _txtByPartPart.Dock = DockStyle.Fill;
            _txtByPartPart.LabelText = "Part ID";
            _txtByPartPart.LabelVisible = "False";
            _txtByPartPart.Location = new Point(13, 13);
            _txtByPartPart.MaxLength = 130;
            _txtByPartPart.MinimumSize = new Size(0, 23);
            _txtByPartPart.MinLength = 130;
            _txtByPartPart.Name = "_txtByPartPart";
            _txtByPartPart.PlaceholderText = "Enter Part Number";
            _txtByPartPart.ShowF4Button = false;
            _txtByPartPart.ShowValidationColor = false;
            _txtByPartPart.Size = new Size(374, 23);
            _txtByPartPart.TabIndex = 0;
            _txtByPartPart.ValidatorType = null;
            // 
            // _tabByUser
            // 
            _tabByUser.Controls.Add(pnlByUser);
            _tabByUser.Location = new Point(4, 24);
            _tabByUser.Name = "_tabByUser";
            _tabByUser.Padding = new Padding(3);
            _tabByUser.Size = new Size(786, 106);
            _tabByUser.TabIndex = 2;
            _tabByUser.Text = "By User";
            _tabByUser.UseVisualStyleBackColor = true;
            // 
            // pnlByUser
            // 
            pnlByUser.AutoSize = true;
            pnlByUser.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlByUser.ColumnCount = 2;
            pnlByUser.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            pnlByUser.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            pnlByUser.Controls.Add(_txtByUserUser, 0, 0);
            pnlByUser.Controls.Add(tableLayoutPanel2, 0, 1);
            pnlByUser.Dock = DockStyle.Fill;
            pnlByUser.Location = new Point(3, 3);
            pnlByUser.Name = "pnlByUser";
            pnlByUser.Padding = new Padding(10);
            pnlByUser.RowCount = 2;
            pnlByUser.RowStyles.Add(new RowStyle());
            pnlByUser.RowStyles.Add(new RowStyle());
            pnlByUser.Size = new Size(780, 100);
            pnlByUser.TabIndex = 0;
            // 
            // _txtByUserUser
            // 
            _txtByUserUser.AutoSize = true;
            _txtByUserUser.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _txtByUserUser.Dock = DockStyle.Fill;
            _txtByUserUser.LabelText = "User ID";
            _txtByUserUser.LabelVisible = "False";
            _txtByUserUser.Location = new Point(13, 13);
            _txtByUserUser.MaxLength = 130;
            _txtByUserUser.MinimumSize = new Size(0, 23);
            _txtByUserUser.MinLength = 130;
            _txtByUserUser.Name = "_txtByUserUser";
            _txtByUserUser.PlaceholderText = "Enter User Login (5 Letters)";
            _txtByUserUser.ShowF4Button = false;
            _txtByUserUser.ShowValidationColor = false;
            _txtByUserUser.Size = new Size(374, 23);
            _txtByUserUser.TabIndex = 0;
            _txtByUserUser.ValidatorType = null;
            // 
            // lblByUserStart
            // 
            lblByUserStart.AutoSize = true;
            tableLayoutPanel2.SetColumnSpan(lblByUserStart, 3);
            lblByUserStart.Dock = DockStyle.Fill;
            lblByUserStart.Font = new Font("Segoe UI Emoji", 9.75F);
            lblByUserStart.Location = new Point(3, 3);
            lblByUserStart.Margin = new Padding(3);
            lblByUserStart.Name = "lblByUserStart";
            lblByUserStart.Size = new Size(368, 17);
            lblByUserStart.TabIndex = 0;
            lblByUserStart.Text = "📅 Date Range";
            lblByUserStart.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // _dtpByUserStart
            // 
            _dtpByUserStart.Dock = DockStyle.Fill;
            _dtpByUserStart.Format = DateTimePickerFormat.Short;
            _dtpByUserStart.Location = new Point(3, 26);
            _dtpByUserStart.Name = "_dtpByUserStart";
            _dtpByUserStart.Size = new Size(167, 23);
            _dtpByUserStart.TabIndex = 1;
            // 
            // lblByUserEnd
            // 
            lblByUserEnd.AutoSize = true;
            lblByUserEnd.Dock = DockStyle.Fill;
            lblByUserEnd.Location = new Point(176, 26);
            lblByUserEnd.Margin = new Padding(3);
            lblByUserEnd.Name = "lblByUserEnd";
            lblByUserEnd.Size = new Size(20, 23);
            lblByUserEnd.TabIndex = 2;
            lblByUserEnd.Text = "To";
            lblByUserEnd.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _dtpByUserEnd
            // 
            _dtpByUserEnd.Dock = DockStyle.Fill;
            _dtpByUserEnd.Format = DateTimePickerFormat.Short;
            _dtpByUserEnd.Location = new Point(202, 26);
            _dtpByUserEnd.Name = "_dtpByUserEnd";
            _dtpByUserEnd.Size = new Size(169, 23);
            _dtpByUserEnd.TabIndex = 3;
            // 
            // _tabByWO
            // 
            _tabByWO.Controls.Add(pnlByWO);
            _tabByWO.Location = new Point(4, 24);
            _tabByWO.Name = "_tabByWO";
            _tabByWO.Padding = new Padding(3);
            _tabByWO.Size = new Size(786, 106);
            _tabByWO.TabIndex = 3;
            _tabByWO.Text = "By Work Order";
            _tabByWO.UseVisualStyleBackColor = true;
            // 
            // pnlByWO
            // 
            pnlByWO.AutoSize = true;
            pnlByWO.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlByWO.ColumnCount = 2;
            pnlByWO.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            pnlByWO.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            pnlByWO.Controls.Add(_txtByWOWO, 0, 0);
            pnlByWO.Dock = DockStyle.Fill;
            pnlByWO.Location = new Point(3, 3);
            pnlByWO.Name = "pnlByWO";
            pnlByWO.Padding = new Padding(10);
            pnlByWO.RowCount = 2;
            pnlByWO.RowStyles.Add(new RowStyle());
            pnlByWO.RowStyles.Add(new RowStyle());
            pnlByWO.Size = new Size(780, 100);
            pnlByWO.TabIndex = 0;
            // 
            // _txtByWOWO
            // 
            _txtByWOWO.AutoSize = true;
            _txtByWOWO.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _txtByWOWO.Dock = DockStyle.Fill;
            _txtByWOWO.LabelText = "Work Order";
            _txtByWOWO.LabelVisible = "False";
            _txtByWOWO.Location = new Point(13, 13);
            _txtByWOWO.MaxLength = 130;
            _txtByWOWO.MinimumSize = new Size(0, 23);
            _txtByWOWO.MinLength = 130;
            _txtByWOWO.Name = "_txtByWOWO";
            _txtByWOWO.PlaceholderText = "Enter Full Work Order (WO-xxxxxx)";
            _txtByWOWO.ShowF4Button = false;
            _txtByWOWO.ShowValidationColor = false;
            _txtByWOWO.Size = new Size(374, 23);
            _txtByWOWO.TabIndex = 0;
            _txtByWOWO.ValidatorType = null;
            // 
            // _tabByCO
            // 
            _tabByCO.Controls.Add(pnlByCO);
            _tabByCO.Location = new Point(4, 24);
            _tabByCO.Name = "_tabByCO";
            _tabByCO.Padding = new Padding(3);
            _tabByCO.Size = new Size(786, 106);
            _tabByCO.TabIndex = 4;
            _tabByCO.Text = "By Customer Order";
            _tabByCO.UseVisualStyleBackColor = true;
            // 
            // pnlByCO
            // 
            pnlByCO.AutoSize = true;
            pnlByCO.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlByCO.ColumnCount = 2;
            pnlByCO.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            pnlByCO.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            pnlByCO.Controls.Add(_txtByCOCO, 0, 0);
            pnlByCO.Dock = DockStyle.Fill;
            pnlByCO.Location = new Point(3, 3);
            pnlByCO.Name = "pnlByCO";
            pnlByCO.Padding = new Padding(10);
            pnlByCO.RowCount = 2;
            pnlByCO.RowStyles.Add(new RowStyle());
            pnlByCO.RowStyles.Add(new RowStyle());
            pnlByCO.Size = new Size(780, 100);
            pnlByCO.TabIndex = 0;
            // 
            // _txtByCOCO
            // 
            _txtByCOCO.AutoSize = true;
            _txtByCOCO.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _txtByCOCO.Dock = DockStyle.Fill;
            _txtByCOCO.LabelText = "Customer Order";
            _txtByCOCO.LabelVisible = "False";
            _txtByCOCO.Location = new Point(13, 13);
            _txtByCOCO.MaxLength = 130;
            _txtByCOCO.MinimumSize = new Size(0, 23);
            _txtByCOCO.MinLength = 130;
            _txtByCOCO.Name = "_txtByCOCO";
            _txtByCOCO.PlaceholderText = "Enter Full Customer Order (CO-xxxxxx)";
            _txtByCOCO.ShowF4Button = false;
            _txtByCOCO.ShowValidationColor = false;
            _txtByCOCO.Size = new Size(374, 23);
            _txtByCOCO.TabIndex = 0;
            _txtByCOCO.ValidatorType = null;
            // 
            // _tabByPO
            // 
            _tabByPO.Controls.Add(pnlByPO);
            _tabByPO.Location = new Point(4, 24);
            _tabByPO.Name = "_tabByPO";
            _tabByPO.Padding = new Padding(3);
            _tabByPO.Size = new Size(786, 106);
            _tabByPO.TabIndex = 5;
            _tabByPO.Text = "By PO Number";
            _tabByPO.UseVisualStyleBackColor = true;
            // 
            // pnlByPO
            // 
            pnlByPO.AutoSize = true;
            pnlByPO.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlByPO.ColumnCount = 2;
            pnlByPO.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            pnlByPO.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            pnlByPO.Controls.Add(_txtByPOPO, 0, 0);
            pnlByPO.Dock = DockStyle.Fill;
            pnlByPO.Location = new Point(3, 3);
            pnlByPO.Name = "pnlByPO";
            pnlByPO.Padding = new Padding(10);
            pnlByPO.RowCount = 2;
            pnlByPO.RowStyles.Add(new RowStyle());
            pnlByPO.RowStyles.Add(new RowStyle());
            pnlByPO.Size = new Size(780, 100);
            pnlByPO.TabIndex = 0;
            // 
            // _txtByPOPO
            // 
            _txtByPOPO.AutoSize = true;
            _txtByPOPO.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _txtByPOPO.Dock = DockStyle.Fill;
            _txtByPOPO.LabelText = "PO Number";
            _txtByPOPO.LabelVisible = "False";
            _txtByPOPO.Location = new Point(13, 13);
            _txtByPOPO.MaxLength = 130;
            _txtByPOPO.MinimumSize = new Size(0, 23);
            _txtByPOPO.MinLength = 130;
            _txtByPOPO.Name = "_txtByPOPO";
            _txtByPOPO.PlaceholderText = "Enter Full Purchase Order (PO-xxxxxx)";
            _txtByPOPO.ShowF4Button = false;
            _txtByPOPO.ShowValidationColor = false;
            _txtByPOPO.Size = new Size(374, 23);
            _txtByPOPO.TabIndex = 0;
            _txtByPOPO.ValidatorType = null;
            // 
            // _tabUserAnalytics
            // 
            _tabUserAnalytics.Controls.Add(pnlUserAnalytics);
            _tabUserAnalytics.Location = new Point(4, 24);
            _tabUserAnalytics.Name = "_tabUserAnalytics";
            _tabUserAnalytics.Padding = new Padding(3);
            _tabUserAnalytics.Size = new Size(786, 172);
            _tabUserAnalytics.TabIndex = 6;
            _tabUserAnalytics.Text = "User Analytics";
            _tabUserAnalytics.UseVisualStyleBackColor = true;
            // 
            // pnlUserAnalytics
            // 
            pnlUserAnalytics.ColumnCount = 3;
            pnlUserAnalytics.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            pnlUserAnalytics.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            pnlUserAnalytics.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            pnlUserAnalytics.Controls.Add(tableLayoutPanel4, 0, 0);
            pnlUserAnalytics.Controls.Add(_clbUsers, 1, 0);
            pnlUserAnalytics.Controls.Add(_btnGenerateReport, 2, 0);
            pnlUserAnalytics.Controls.Add(_lblUserCount, 1, 1);
            pnlUserAnalytics.Dock = DockStyle.Fill;
            pnlUserAnalytics.Location = new Point(3, 3);
            pnlUserAnalytics.Name = "pnlUserAnalytics";
            pnlUserAnalytics.Padding = new Padding(10);
            pnlUserAnalytics.RowCount = 2;
            pnlUserAnalytics.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            pnlUserAnalytics.RowStyles.Add(new RowStyle());
            pnlUserAnalytics.Size = new Size(780, 166);
            pnlUserAnalytics.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.AutoSize = true;
            tableLayoutPanel4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Controls.Add(lblAnalyticsStart, 0, 0);
            tableLayoutPanel4.Controls.Add(_dtpAnalyticsStart, 0, 1);
            tableLayoutPanel4.Controls.Add(lblAnalyticsEnd, 1, 0);
            tableLayoutPanel4.Controls.Add(_dtpAnalyticsEnd, 1, 1);
            tableLayoutPanel4.Controls.Add(_btnLoadUsers, 0, 2);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(13, 13);
            tableLayoutPanel4.Margin = new Padding(3, 3, 10, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 3;
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.Size = new Size(298, 125);
            tableLayoutPanel4.TabIndex = 0;
            // 
            // lblAnalyticsStart
            // 
            lblAnalyticsStart.AutoSize = true;
            lblAnalyticsStart.Dock = DockStyle.Fill;
            lblAnalyticsStart.Location = new Point(3, 3);
            lblAnalyticsStart.Margin = new Padding(3);
            lblAnalyticsStart.Name = "lblAnalyticsStart";
            lblAnalyticsStart.Size = new Size(143, 15);
            lblAnalyticsStart.TabIndex = 0;
            lblAnalyticsStart.Text = "Start Date";
            // 
            // _dtpAnalyticsStart
            // 
            _dtpAnalyticsStart.Dock = DockStyle.Fill;
            _dtpAnalyticsStart.Format = DateTimePickerFormat.Short;
            _dtpAnalyticsStart.Location = new Point(3, 24);
            _dtpAnalyticsStart.Name = "_dtpAnalyticsStart";
            _dtpAnalyticsStart.Size = new Size(143, 23);
            _dtpAnalyticsStart.TabIndex = 1;
            // 
            // lblAnalyticsEnd
            // 
            lblAnalyticsEnd.AutoSize = true;
            lblAnalyticsEnd.Dock = DockStyle.Fill;
            lblAnalyticsEnd.Location = new Point(152, 3);
            lblAnalyticsEnd.Margin = new Padding(3);
            lblAnalyticsEnd.Name = "lblAnalyticsEnd";
            lblAnalyticsEnd.Size = new Size(143, 15);
            lblAnalyticsEnd.TabIndex = 2;
            lblAnalyticsEnd.Text = "End Date";
            // 
            // _dtpAnalyticsEnd
            // 
            _dtpAnalyticsEnd.Dock = DockStyle.Fill;
            _dtpAnalyticsEnd.Format = DateTimePickerFormat.Short;
            _dtpAnalyticsEnd.Location = new Point(152, 24);
            _dtpAnalyticsEnd.Name = "_dtpAnalyticsEnd";
            _dtpAnalyticsEnd.Size = new Size(143, 23);
            _dtpAnalyticsEnd.TabIndex = 3;
            // 
            // _btnLoadUsers
            // 
            tableLayoutPanel4.SetColumnSpan(_btnLoadUsers, 2);
            _btnLoadUsers.Dock = DockStyle.Fill;
            _btnLoadUsers.Location = new Point(3, 53);
            _btnLoadUsers.Name = "_btnLoadUsers";
            _btnLoadUsers.Size = new Size(292, 30);
            _btnLoadUsers.TabIndex = 4;
            _btnLoadUsers.Text = "Load Users";
            _btnLoadUsers.UseVisualStyleBackColor = true;
            // 
            // _clbUsers
            // 
            _clbUsers.Dock = DockStyle.Fill;
            _clbUsers.FormattingEnabled = true;
            _clbUsers.Location = new Point(317, 13);
            _clbUsers.Margin = new Padding(3, 3, 3, 10);
            _clbUsers.Name = "_clbUsers";
            _clbUsers.Size = new Size(298, 125);
            _clbUsers.TabIndex = 1;
            // 
            // _btnGenerateReport
            // 
            _btnGenerateReport.Dock = DockStyle.Fill;
            _btnGenerateReport.Enabled = false;
            _btnGenerateReport.Location = new Point(621, 13);
            _btnGenerateReport.Margin = new Padding(3, 3, 3, 10);
            _btnGenerateReport.Name = "_btnGenerateReport";
            _btnGenerateReport.Size = new Size(146, 125);
            _btnGenerateReport.TabIndex = 2;
            _btnGenerateReport.Text = "Generate Report";
            _btnGenerateReport.UseVisualStyleBackColor = true;
            // 
            // _lblUserCount
            // 
            _lblUserCount.AutoSize = true;
            _lblUserCount.Dock = DockStyle.Fill;
            _lblUserCount.Location = new Point(317, 148);
            _lblUserCount.Name = "_lblUserCount";
            _lblUserCount.Size = new Size(298, 15);
            _lblUserCount.TabIndex = 3;
            _lblUserCount.Text = "Selected: 0 / 10";
            _lblUserCount.TextAlign = ContentAlignment.MiddleCenter;
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
            // _dataGridView
            // 
            _dataGridView.AllowUserToAddRows = false;
            _dataGridView.AllowUserToDeleteRows = false;
            _dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _dataGridView.Dock = DockStyle.Fill;
            _dataGridView.Location = new Point(3, 195);
            _dataGridView.Name = "_dataGridView";
            _dataGridView.ReadOnly = true;
            _dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dataGridView.Size = new Size(794, 414);
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
            tableLayoutPanel1.Location = new Point(13, 42);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(374, 52);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.0000076F));
            tableLayoutPanel2.Controls.Add(_dtpByUserEnd, 2, 1);
            tableLayoutPanel2.Controls.Add(lblByUserEnd, 1, 1);
            tableLayoutPanel2.Controls.Add(_dtpByUserStart, 0, 1);
            tableLayoutPanel2.Controls.Add(lblByUserStart, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(13, 42);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(374, 52);
            tableLayoutPanel2.TabIndex = 2;
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
            tableLayoutPanel3.Location = new Point(3, 143);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(794, 46);
            tableLayoutPanel3.TabIndex = 2;
            // 
            // Control_InventoryAudit
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(mainLayout);
            Name = "Control_InventoryAudit";
            Size = new Size(803, 615);
            mainLayout.ResumeLayout(false);
            mainLayout.PerformLayout();
            _tabControl.ResumeLayout(false);
            _tabLifecycle.ResumeLayout(false);
            _tabLifecycle.PerformLayout();
            pnlLifecycle.ResumeLayout(false);
            pnlLifecycle.PerformLayout();
            _tabByPart.ResumeLayout(false);
            _tabByPart.PerformLayout();
            pnlByPart.ResumeLayout(false);
            pnlByPart.PerformLayout();
            _tabByUser.ResumeLayout(false);
            _tabByUser.PerformLayout();
            pnlByUser.ResumeLayout(false);
            pnlByUser.PerformLayout();
            _tabByWO.ResumeLayout(false);
            _tabByWO.PerformLayout();
            pnlByWO.ResumeLayout(false);
            pnlByWO.PerformLayout();
            _tabByCO.ResumeLayout(false);
            _tabByCO.PerformLayout();
            pnlByCO.ResumeLayout(false);
            pnlByCO.PerformLayout();
            _tabByPO.ResumeLayout(false);
            _tabByPO.PerformLayout();
            pnlByPO.ResumeLayout(false);
            pnlByPO.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_dataGridView).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
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
        private System.Windows.Forms.TabPage _tabByPart;
        private System.Windows.Forms.TableLayoutPanel pnlByPart;
        private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel _txtByPartPart;
        private System.Windows.Forms.TabPage _tabByUser;
        private System.Windows.Forms.TableLayoutPanel pnlByUser;
        private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel _txtByUserUser;
        private System.Windows.Forms.Label lblByUserStart;
        private System.Windows.Forms.DateTimePicker _dtpByUserStart;
        private System.Windows.Forms.Label lblByUserEnd;
        private System.Windows.Forms.DateTimePicker _dtpByUserEnd;
        private System.Windows.Forms.TabPage _tabByWO;
        private System.Windows.Forms.TableLayoutPanel pnlByWO;
        private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel _txtByWOWO;
        private System.Windows.Forms.TabPage _tabByCO;
        private System.Windows.Forms.TableLayoutPanel pnlByCO;
        private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel _txtByCOCO;
        private System.Windows.Forms.TabPage _tabByPO;
        private System.Windows.Forms.TableLayoutPanel pnlByPO;
        private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel _txtByPOPO;
        private System.Windows.Forms.TabPage _tabUserAnalytics;
        private System.Windows.Forms.TableLayoutPanel pnlUserAnalytics;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label lblAnalyticsStart;
        private System.Windows.Forms.DateTimePicker _dtpAnalyticsStart;
        private System.Windows.Forms.Label lblAnalyticsEnd;
        private System.Windows.Forms.DateTimePicker _dtpAnalyticsEnd;
        private System.Windows.Forms.Button _btnLoadUsers;
        private System.Windows.Forms.CheckedListBox _clbUsers;
        private System.Windows.Forms.Button _btnGenerateReport;
        private System.Windows.Forms.Label _lblUserCount;
        private System.Windows.Forms.Button _btnSearch;
        private System.Windows.Forms.Button _btnExport;
        private System.Windows.Forms.DataGridView _dataGridView;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
    }
}
