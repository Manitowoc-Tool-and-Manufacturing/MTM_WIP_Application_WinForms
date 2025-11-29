using MTM_WIP_Application_Winforms.Forms.Shared;

namespace MTM_WIP_Application_Winforms.Controls.Visual
{
    partial class Control_ReceivingAnalytics
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            Control_ReceivingAnalytics_TableLayoutPanel_Main = new TableLayoutPanel();
            Control_ReceivingAnalytics_Panel_Filters = new Panel();
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            Control_ReceivingAnalytics_CheckBox_ShowWithPartID = new CheckBox();
            Control_ReceivingAnalytics_CheckBox_ShowOnTime = new CheckBox();
            Control_ReceivingAnalytics_CheckBox_ShowPartial = new CheckBox();
            Control_ReceivingAnalytics_CheckBox_ShowLate = new CheckBox();
            Control_ReceivingAnalytics_Button_Search = new Button();
            Control_ReceivingAnalytics_CheckBox_ShowOutsideService = new CheckBox();
            Control_ReceivingAnalytics_CheckBox_ShowInternal = new CheckBox();
            Control_ReceivingAnalytics_CheckBox_ShowConsignment = new CheckBox();
            Control_ReceivingAnalytics_CheckBox_ShowClosed = new CheckBox();
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_DateType = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            Control_ReceivingAnalytics_DateTimePicker_EndDate = new DateTimePicker();
            Control_ReceivingAnalytics_Label_DateRangeTo = new Label();
            Control_ReceivingAnalytics_DateTimePicker_StartDate = new DateTimePicker();
            Control_ReceivingAnalytics_Label_DateRange = new Label();
            Control_ReceivingAnalytics_DataGridView_Results = new DataGridView();
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes = new TableLayoutPanel();
            Control_ReceivingAnalytics_TableLayoutPanel_PONumber = new TableLayoutPanel();
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange = new TableLayoutPanel();
            Control_ReceivingAnalytics_TableLayoutPanel_FilterBy = new TableLayoutPanel();
            Control_ReceivingAnalytics_TableLayoutPanel_Vendor = new TableLayoutPanel();
            Control_ReceivingAnalytics_TableLayoutPanel_Filters = new TableLayoutPanel();
            Control_ReceivingAnalytics_CheckBox_ShowOpen = new CheckBox();
            Control_ReceivingAnalytics_CheckBox_ShowMMC = new CheckBox();
            Control_ReceivingAnalytics_CheckBox_ShowMMF = new CheckBox();
            Control_ReceivingAnalytics_Label_POState = new Label();
            Control_ReceivingAnalytics_Label_DeliveryStatus = new Label();
            Control_ReceivingAnalytics_Label_NotReceiving = new Label();
            Control_ReceivingAnalytics_Label_OnlyReceiving = new Label();
            Control_ReceivingAnalytics_TableLayoutPanel_Options = new TableLayoutPanel();
            Control_ReceivingAnalytics_TableLayoutPanel_PartNumber = new TableLayoutPanel();
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            Control_ReceivingAnalytics_TableLayoutPanel_Carrier = new TableLayoutPanel();
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons = new TableLayoutPanel();
            Control_ReceivingAnalytics_Button_ToggleOptions = new Button();
            Control_ReceivingAnalytics_Button_Analytics = new Button();
            Control_ReceivingAnalytics_Panel_CheckBoxes_Legend = new Panel();
            Control_ReceivingAnalytics_TableLayoutPanel_Legend = new TableLayoutPanel();
            Control_ReceivingAnalytics_Label_LegendOnTime = new Label();
            Control_ReceivingAnalytics_Label_LegendPartial = new Label();
            Control_ReceivingAnalytics_Label_LegendLate = new Label();
            Control_ReceivingAnalytics_Label_LegendClosed = new Label();
            Control_ReceivingAnalytics_Panel_LegendClosed = new Panel();
            Control_ReceivingAnalytics_Panel_LegendLate = new Panel();
            Control_ReceivingAnalytics_Panel_LegendPartial = new Panel();
            Control_ReceivingAnalytics_Panel_LegendOnTime = new Panel();
            Control_ReceivingAnalytics_Label_LegendTitle = new Label();
            Control_ReceivingAnalytics_TableLayoutPanel_Main.SuspendLayout();
            Control_ReceivingAnalytics_Panel_Filters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Control_ReceivingAnalytics_DataGridView_Results).BeginInit();
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.SuspendLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_PONumber.SuspendLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.SuspendLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_FilterBy.SuspendLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_Vendor.SuspendLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_Filters.SuspendLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_Options.SuspendLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_PartNumber.SuspendLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_Carrier.SuspendLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.SuspendLayout();
            Control_ReceivingAnalytics_Panel_CheckBoxes_Legend.SuspendLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.SuspendLayout();
            SuspendLayout();
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_Main
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_Main.AutoSize = true;
            Control_ReceivingAnalytics_TableLayoutPanel_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_TableLayoutPanel_Main.ColumnCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_ReceivingAnalytics_TableLayoutPanel_Main.Controls.Add(Control_ReceivingAnalytics_Panel_Filters, 0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_Main.Controls.Add(Control_ReceivingAnalytics_DataGridView_Results, 0, 1);
            Control_ReceivingAnalytics_TableLayoutPanel_Main.Controls.Add(Control_ReceivingAnalytics_Panel_CheckBoxes_Legend, 0, 2);
            Control_ReceivingAnalytics_TableLayoutPanel_Main.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_Main.Location = new Point(0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_Main.Name = "Control_ReceivingAnalytics_TableLayoutPanel_Main";
            Control_ReceivingAnalytics_TableLayoutPanel_Main.RowCount = 3;
            Control_ReceivingAnalytics_TableLayoutPanel_Main.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_ReceivingAnalytics_TableLayoutPanel_Main.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_Main.Size = new Size(1000, 600);
            Control_ReceivingAnalytics_TableLayoutPanel_Main.TabIndex = 0;
            // 
            // Control_ReceivingAnalytics_Panel_Filters
            // 
            Control_ReceivingAnalytics_Panel_Filters.AutoSize = true;
            Control_ReceivingAnalytics_Panel_Filters.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_Panel_Filters.Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_Options);
            Control_ReceivingAnalytics_Panel_Filters.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Panel_Filters.Location = new Point(3, 3);
            Control_ReceivingAnalytics_Panel_Filters.Name = "Control_ReceivingAnalytics_Panel_Filters";
            Control_ReceivingAnalytics_Panel_Filters.Size = new Size(994, 226);
            Control_ReceivingAnalytics_Panel_Filters.TabIndex = 0;
            // 
            // Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber
            // 
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber.Location = new Point(3, 3);
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber.Name = "Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber";
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber.Size = new Size(317, 23);
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber.TabIndex = 14;
            // 
            // Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier
            // 
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier.Location = new Point(3, 3);
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier.Name = "Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier";
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier.Size = new Size(317, 23);
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier.TabIndex = 12;
            // 
            // Control_ReceivingAnalytics_CheckBox_ShowWithPartID
            // 
            Control_ReceivingAnalytics_CheckBox_ShowWithPartID.AutoSize = true;
            Control_ReceivingAnalytics_CheckBox_ShowWithPartID.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_CheckBox_ShowWithPartID.Location = new Point(497, 28);
            Control_ReceivingAnalytics_CheckBox_ShowWithPartID.Name = "Control_ReceivingAnalytics_CheckBox_ShowWithPartID";
            Control_ReceivingAnalytics_CheckBox_ShowWithPartID.Size = new Size(241, 19);
            Control_ReceivingAnalytics_CheckBox_ShowWithPartID.TabIndex = 18;
            Control_ReceivingAnalytics_CheckBox_ShowWithPartID.Text = "Only Part Numbers";
            Control_ReceivingAnalytics_CheckBox_ShowWithPartID.UseVisualStyleBackColor = true;
            // 
            // Control_ReceivingAnalytics_CheckBox_ShowOnTime
            // 
            Control_ReceivingAnalytics_CheckBox_ShowOnTime.AutoSize = true;
            Control_ReceivingAnalytics_CheckBox_ShowOnTime.Checked = true;
            Control_ReceivingAnalytics_CheckBox_ShowOnTime.CheckState = CheckState.Checked;
            Control_ReceivingAnalytics_CheckBox_ShowOnTime.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_CheckBox_ShowOnTime.Location = new Point(250, 28);
            Control_ReceivingAnalytics_CheckBox_ShowOnTime.Name = "Control_ReceivingAnalytics_CheckBox_ShowOnTime";
            Control_ReceivingAnalytics_CheckBox_ShowOnTime.Size = new Size(241, 19);
            Control_ReceivingAnalytics_CheckBox_ShowOnTime.TabIndex = 17;
            Control_ReceivingAnalytics_CheckBox_ShowOnTime.Text = "Show On Time";
            Control_ReceivingAnalytics_CheckBox_ShowOnTime.UseVisualStyleBackColor = true;
            // 
            // Control_ReceivingAnalytics_CheckBox_ShowPartial
            // 
            Control_ReceivingAnalytics_CheckBox_ShowPartial.AutoSize = true;
            Control_ReceivingAnalytics_CheckBox_ShowPartial.Checked = true;
            Control_ReceivingAnalytics_CheckBox_ShowPartial.CheckState = CheckState.Checked;
            Control_ReceivingAnalytics_CheckBox_ShowPartial.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_CheckBox_ShowPartial.Location = new Point(3, 53);
            Control_ReceivingAnalytics_CheckBox_ShowPartial.Name = "Control_ReceivingAnalytics_CheckBox_ShowPartial";
            Control_ReceivingAnalytics_CheckBox_ShowPartial.Size = new Size(241, 19);
            Control_ReceivingAnalytics_CheckBox_ShowPartial.TabIndex = 16;
            Control_ReceivingAnalytics_CheckBox_ShowPartial.Text = "Show Partial";
            Control_ReceivingAnalytics_CheckBox_ShowPartial.UseVisualStyleBackColor = true;
            // 
            // Control_ReceivingAnalytics_CheckBox_ShowLate
            // 
            Control_ReceivingAnalytics_CheckBox_ShowLate.AutoSize = true;
            Control_ReceivingAnalytics_CheckBox_ShowLate.Checked = true;
            Control_ReceivingAnalytics_CheckBox_ShowLate.CheckState = CheckState.Checked;
            Control_ReceivingAnalytics_CheckBox_ShowLate.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_CheckBox_ShowLate.Location = new Point(250, 53);
            Control_ReceivingAnalytics_CheckBox_ShowLate.Name = "Control_ReceivingAnalytics_CheckBox_ShowLate";
            Control_ReceivingAnalytics_CheckBox_ShowLate.Size = new Size(241, 19);
            Control_ReceivingAnalytics_CheckBox_ShowLate.TabIndex = 15;
            Control_ReceivingAnalytics_CheckBox_ShowLate.Text = "Show Late";
            Control_ReceivingAnalytics_CheckBox_ShowLate.UseVisualStyleBackColor = true;
            // 
            // Control_ReceivingAnalytics_Button_Search
            // 
            Control_ReceivingAnalytics_Button_Search.AutoSize = true;
            Control_ReceivingAnalytics_Button_Search.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_Button_Search.BackColor = Color.FromArgb(0, 120, 215);
            Control_ReceivingAnalytics_Button_Search.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Button_Search.FlatStyle = FlatStyle.Flat;
            Control_ReceivingAnalytics_Button_Search.ForeColor = Color.White;
            Control_ReceivingAnalytics_Button_Search.Location = new Point(3, 3);
            Control_ReceivingAnalytics_Button_Search.MaximumSize = new Size(0, 32);
            Control_ReceivingAnalytics_Button_Search.MinimumSize = new Size(0, 32);
            Control_ReceivingAnalytics_Button_Search.Name = "Control_ReceivingAnalytics_Button_Search";
            Control_ReceivingAnalytics_Button_Search.Size = new Size(944, 32);
            Control_ReceivingAnalytics_Button_Search.TabIndex = 10;
            Control_ReceivingAnalytics_Button_Search.Text = "Search";
            Control_ReceivingAnalytics_Button_Search.UseVisualStyleBackColor = false;
            // 
            // Control_ReceivingAnalytics_CheckBox_ShowOutsideService
            // 
            Control_ReceivingAnalytics_CheckBox_ShowOutsideService.AutoSize = true;
            Control_ReceivingAnalytics_CheckBox_ShowOutsideService.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_CheckBox_ShowOutsideService.Location = new Point(744, 28);
            Control_ReceivingAnalytics_CheckBox_ShowOutsideService.Name = "Control_ReceivingAnalytics_CheckBox_ShowOutsideService";
            Control_ReceivingAnalytics_CheckBox_ShowOutsideService.Size = new Size(241, 19);
            Control_ReceivingAnalytics_CheckBox_ShowOutsideService.TabIndex = 9;
            Control_ReceivingAnalytics_CheckBox_ShowOutsideService.Text = "Show Outside Service";
            Control_ReceivingAnalytics_CheckBox_ShowOutsideService.UseVisualStyleBackColor = true;
            // 
            // Control_ReceivingAnalytics_CheckBox_ShowInternal
            // 
            Control_ReceivingAnalytics_CheckBox_ShowInternal.AutoSize = true;
            Control_ReceivingAnalytics_CheckBox_ShowInternal.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_CheckBox_ShowInternal.Location = new Point(744, 78);
            Control_ReceivingAnalytics_CheckBox_ShowInternal.Name = "Control_ReceivingAnalytics_CheckBox_ShowInternal";
            Control_ReceivingAnalytics_CheckBox_ShowInternal.Size = new Size(241, 19);
            Control_ReceivingAnalytics_CheckBox_ShowInternal.TabIndex = 8;
            Control_ReceivingAnalytics_CheckBox_ShowInternal.Text = "Show Internal Orders";
            Control_ReceivingAnalytics_CheckBox_ShowInternal.UseVisualStyleBackColor = true;
            // 
            // Control_ReceivingAnalytics_CheckBox_ShowConsignment
            // 
            Control_ReceivingAnalytics_CheckBox_ShowConsignment.AutoSize = true;
            Control_ReceivingAnalytics_CheckBox_ShowConsignment.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_CheckBox_ShowConsignment.Location = new Point(744, 53);
            Control_ReceivingAnalytics_CheckBox_ShowConsignment.Name = "Control_ReceivingAnalytics_CheckBox_ShowConsignment";
            Control_ReceivingAnalytics_CheckBox_ShowConsignment.Size = new Size(241, 19);
            Control_ReceivingAnalytics_CheckBox_ShowConsignment.TabIndex = 7;
            Control_ReceivingAnalytics_CheckBox_ShowConsignment.Text = "Show Consignment";
            Control_ReceivingAnalytics_CheckBox_ShowConsignment.UseVisualStyleBackColor = true;
            // 
            // Control_ReceivingAnalytics_CheckBox_ShowClosed
            // 
            Control_ReceivingAnalytics_CheckBox_ShowClosed.AutoSize = true;
            Control_ReceivingAnalytics_CheckBox_ShowClosed.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_CheckBox_ShowClosed.Location = new Point(3, 78);
            Control_ReceivingAnalytics_CheckBox_ShowClosed.Name = "Control_ReceivingAnalytics_CheckBox_ShowClosed";
            Control_ReceivingAnalytics_CheckBox_ShowClosed.Size = new Size(241, 19);
            Control_ReceivingAnalytics_CheckBox_ShowClosed.TabIndex = 6;
            Control_ReceivingAnalytics_CheckBox_ShowClosed.Text = "Show Closed";
            Control_ReceivingAnalytics_CheckBox_ShowClosed.UseVisualStyleBackColor = true;
            // 
            // Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_DateType
            // 
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_DateType.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_DateType.Location = new Point(3, 3);
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_DateType.Name = "Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_DateType";
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_DateType.Size = new Size(317, 23);
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_DateType.TabIndex = 5;
            // 
            // Control_ReceivingAnalytics_DateTimePicker_EndDate
            // 
            Control_ReceivingAnalytics_DateTimePicker_EndDate.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_DateTimePicker_EndDate.Format = DateTimePickerFormat.Short;
            Control_ReceivingAnalytics_DateTimePicker_EndDate.Location = new Point(214, 3);
            Control_ReceivingAnalytics_DateTimePicker_EndDate.Name = "Control_ReceivingAnalytics_DateTimePicker_EndDate";
            Control_ReceivingAnalytics_DateTimePicker_EndDate.Size = new Size(106, 23);
            Control_ReceivingAnalytics_DateTimePicker_EndDate.TabIndex = 3;
            // 
            // Control_ReceivingAnalytics_Label_DateRangeTo
            // 
            Control_ReceivingAnalytics_Label_DateRangeTo.AutoSize = true;
            Control_ReceivingAnalytics_Label_DateRangeTo.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Label_DateRangeTo.Location = new Point(190, 3);
            Control_ReceivingAnalytics_Label_DateRangeTo.Margin = new Padding(3);
            Control_ReceivingAnalytics_Label_DateRangeTo.Name = "Control_ReceivingAnalytics_Label_DateRangeTo";
            Control_ReceivingAnalytics_Label_DateRangeTo.Size = new Size(18, 23);
            Control_ReceivingAnalytics_Label_DateRangeTo.TabIndex = 2;
            Control_ReceivingAnalytics_Label_DateRangeTo.Text = "to";
            Control_ReceivingAnalytics_Label_DateRangeTo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_ReceivingAnalytics_DateTimePicker_StartDate
            // 
            Control_ReceivingAnalytics_DateTimePicker_StartDate.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_DateTimePicker_StartDate.Format = DateTimePickerFormat.Short;
            Control_ReceivingAnalytics_DateTimePicker_StartDate.Location = new Point(79, 3);
            Control_ReceivingAnalytics_DateTimePicker_StartDate.Name = "Control_ReceivingAnalytics_DateTimePicker_StartDate";
            Control_ReceivingAnalytics_DateTimePicker_StartDate.Size = new Size(105, 23);
            Control_ReceivingAnalytics_DateTimePicker_StartDate.TabIndex = 1;
            // 
            // Control_ReceivingAnalytics_Label_DateRange
            // 
            Control_ReceivingAnalytics_Label_DateRange.AutoSize = true;
            Control_ReceivingAnalytics_Label_DateRange.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Label_DateRange.Location = new Point(3, 3);
            Control_ReceivingAnalytics_Label_DateRange.Margin = new Padding(3);
            Control_ReceivingAnalytics_Label_DateRange.MaximumSize = new Size(70, 23);
            Control_ReceivingAnalytics_Label_DateRange.MinimumSize = new Size(70, 23);
            Control_ReceivingAnalytics_Label_DateRange.Name = "Control_ReceivingAnalytics_Label_DateRange";
            Control_ReceivingAnalytics_Label_DateRange.Size = new Size(70, 23);
            Control_ReceivingAnalytics_Label_DateRange.TabIndex = 0;
            Control_ReceivingAnalytics_Label_DateRange.Text = "Date Range:";
            Control_ReceivingAnalytics_Label_DateRange.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Control_ReceivingAnalytics_DataGridView_Results
            // 
            Control_ReceivingAnalytics_DataGridView_Results.AllowUserToAddRows = false;
            Control_ReceivingAnalytics_DataGridView_Results.AllowUserToDeleteRows = false;
            Control_ReceivingAnalytics_DataGridView_Results.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Control_ReceivingAnalytics_DataGridView_Results.BackgroundColor = Color.White;
            Control_ReceivingAnalytics_DataGridView_Results.BorderStyle = BorderStyle.None;
            Control_ReceivingAnalytics_DataGridView_Results.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Control_ReceivingAnalytics_DataGridView_Results.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_DataGridView_Results.Location = new Point(3, 235);
            Control_ReceivingAnalytics_DataGridView_Results.Name = "Control_ReceivingAnalytics_DataGridView_Results";
            Control_ReceivingAnalytics_DataGridView_Results.ReadOnly = true;
            Control_ReceivingAnalytics_DataGridView_Results.RowHeadersVisible = false;
            Control_ReceivingAnalytics_DataGridView_Results.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Control_ReceivingAnalytics_DataGridView_Results.Size = new Size(994, 323);
            Control_ReceivingAnalytics_DataGridView_Results.TabIndex = 1;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.AutoSize = true;
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.ColumnCount = 4;
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.Controls.Add(Control_ReceivingAnalytics_Label_DeliveryStatus, 1, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.Controls.Add(Control_ReceivingAnalytics_CheckBox_ShowClosed, 0, 3);
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.Controls.Add(Control_ReceivingAnalytics_CheckBox_ShowPartial, 0, 2);
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.Controls.Add(Control_ReceivingAnalytics_CheckBox_ShowOpen, 0, 1);
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.Controls.Add(Control_ReceivingAnalytics_CheckBox_ShowOnTime, 1, 1);
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.Controls.Add(Control_ReceivingAnalytics_CheckBox_ShowLate, 1, 2);
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.Controls.Add(Control_ReceivingAnalytics_Label_POState, 0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.Controls.Add(Control_ReceivingAnalytics_CheckBox_ShowWithPartID, 2, 1);
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.Controls.Add(Control_ReceivingAnalytics_CheckBox_ShowOutsideService, 3, 1);
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.Controls.Add(Control_ReceivingAnalytics_CheckBox_ShowMMC, 2, 2);
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.Controls.Add(Control_ReceivingAnalytics_CheckBox_ShowConsignment, 3, 2);
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.Controls.Add(Control_ReceivingAnalytics_CheckBox_ShowMMF, 2, 3);
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.Controls.Add(Control_ReceivingAnalytics_CheckBox_ShowInternal, 3, 3);
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.Controls.Add(Control_ReceivingAnalytics_Label_OnlyReceiving, 2, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.Controls.Add(Control_ReceivingAnalytics_Label_NotReceiving, 3, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.Location = new Point(3, 79);
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.Name = "Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes";
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.RowCount = 4;
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.Size = new Size(988, 100);
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.TabIndex = 19;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_PONumber
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_PONumber.AutoSize = true;
            Control_ReceivingAnalytics_TableLayoutPanel_PONumber.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_TableLayoutPanel_PONumber.ColumnCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_PONumber.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_PONumber.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            Control_ReceivingAnalytics_TableLayoutPanel_PONumber.Controls.Add(Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber, 0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_PONumber.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_PONumber.Location = new Point(332, 38);
            Control_ReceivingAnalytics_TableLayoutPanel_PONumber.Name = "Control_ReceivingAnalytics_TableLayoutPanel_PONumber";
            Control_ReceivingAnalytics_TableLayoutPanel_PONumber.RowCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_PONumber.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_PONumber.Size = new Size(323, 29);
            Control_ReceivingAnalytics_TableLayoutPanel_PONumber.TabIndex = 20;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_DateRange
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.AutoSize = true;
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.ColumnCount = 4;
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.Controls.Add(Control_ReceivingAnalytics_DateTimePicker_StartDate, 1, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.Controls.Add(Control_ReceivingAnalytics_Label_DateRange, 0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.Controls.Add(Control_ReceivingAnalytics_Label_DateRangeTo, 2, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.Controls.Add(Control_ReceivingAnalytics_DateTimePicker_EndDate, 3, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.Location = new Point(3, 3);
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.Name = "Control_ReceivingAnalytics_TableLayoutPanel_DateRange";
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.RowCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.Size = new Size(323, 29);
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.TabIndex = 21;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_FilterBy
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_FilterBy.AutoSize = true;
            Control_ReceivingAnalytics_TableLayoutPanel_FilterBy.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_TableLayoutPanel_FilterBy.ColumnCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_FilterBy.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_FilterBy.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            Control_ReceivingAnalytics_TableLayoutPanel_FilterBy.Controls.Add(Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_DateType, 0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_FilterBy.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_FilterBy.Location = new Point(3, 38);
            Control_ReceivingAnalytics_TableLayoutPanel_FilterBy.Name = "Control_ReceivingAnalytics_TableLayoutPanel_FilterBy";
            Control_ReceivingAnalytics_TableLayoutPanel_FilterBy.RowCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_FilterBy.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_FilterBy.Size = new Size(323, 29);
            Control_ReceivingAnalytics_TableLayoutPanel_FilterBy.TabIndex = 22;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_Vendor
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_Vendor.AutoSize = true;
            Control_ReceivingAnalytics_TableLayoutPanel_Vendor.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_TableLayoutPanel_Vendor.ColumnCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_Vendor.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_Vendor.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            Control_ReceivingAnalytics_TableLayoutPanel_Vendor.Controls.Add(Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier, 0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_Vendor.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_Vendor.Location = new Point(332, 3);
            Control_ReceivingAnalytics_TableLayoutPanel_Vendor.Name = "Control_ReceivingAnalytics_TableLayoutPanel_Vendor";
            Control_ReceivingAnalytics_TableLayoutPanel_Vendor.RowCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_Vendor.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_Vendor.Size = new Size(323, 29);
            Control_ReceivingAnalytics_TableLayoutPanel_Vendor.TabIndex = 23;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_Filters
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_Filters.AutoSize = true;
            Control_ReceivingAnalytics_TableLayoutPanel_Filters.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_TableLayoutPanel_Filters.ColumnCount = 3;
            Control_ReceivingAnalytics_TableLayoutPanel_Filters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            Control_ReceivingAnalytics_TableLayoutPanel_Filters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            Control_ReceivingAnalytics_TableLayoutPanel_Filters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            Control_ReceivingAnalytics_TableLayoutPanel_Filters.Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_Carrier, 2, 1);
            Control_ReceivingAnalytics_TableLayoutPanel_Filters.Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_PartNumber, 2, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_Filters.Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_PONumber, 1, 1);
            Control_ReceivingAnalytics_TableLayoutPanel_Filters.Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_DateRange, 0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_Filters.Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_FilterBy, 0, 1);
            Control_ReceivingAnalytics_TableLayoutPanel_Filters.Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_Vendor, 1, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_Filters.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_Filters.Location = new Point(3, 3);
            Control_ReceivingAnalytics_TableLayoutPanel_Filters.Name = "Control_ReceivingAnalytics_TableLayoutPanel_Filters";
            Control_ReceivingAnalytics_TableLayoutPanel_Filters.RowCount = 2;
            Control_ReceivingAnalytics_TableLayoutPanel_Filters.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_Filters.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_Filters.Size = new Size(988, 70);
            Control_ReceivingAnalytics_TableLayoutPanel_Filters.TabIndex = 24;
            // 
            // Control_ReceivingAnalytics_CheckBox_ShowOpen
            // 
            Control_ReceivingAnalytics_CheckBox_ShowOpen.AutoSize = true;
            Control_ReceivingAnalytics_CheckBox_ShowOpen.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_CheckBox_ShowOpen.Location = new Point(3, 28);
            Control_ReceivingAnalytics_CheckBox_ShowOpen.Name = "Control_ReceivingAnalytics_CheckBox_ShowOpen";
            Control_ReceivingAnalytics_CheckBox_ShowOpen.Size = new Size(241, 19);
            Control_ReceivingAnalytics_CheckBox_ShowOpen.TabIndex = 19;
            Control_ReceivingAnalytics_CheckBox_ShowOpen.Text = "Show Open";
            Control_ReceivingAnalytics_CheckBox_ShowOpen.UseVisualStyleBackColor = true;
            // 
            // Control_ReceivingAnalytics_CheckBox_ShowMMC
            // 
            Control_ReceivingAnalytics_CheckBox_ShowMMC.AutoSize = true;
            Control_ReceivingAnalytics_CheckBox_ShowMMC.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_CheckBox_ShowMMC.Location = new Point(497, 53);
            Control_ReceivingAnalytics_CheckBox_ShowMMC.Name = "Control_ReceivingAnalytics_CheckBox_ShowMMC";
            Control_ReceivingAnalytics_CheckBox_ShowMMC.Size = new Size(241, 19);
            Control_ReceivingAnalytics_CheckBox_ShowMMC.TabIndex = 20;
            Control_ReceivingAnalytics_CheckBox_ShowMMC.Text = "Only Coils (MMC)";
            Control_ReceivingAnalytics_CheckBox_ShowMMC.UseVisualStyleBackColor = true;
            // 
            // Control_ReceivingAnalytics_CheckBox_ShowMMF
            // 
            Control_ReceivingAnalytics_CheckBox_ShowMMF.AutoSize = true;
            Control_ReceivingAnalytics_CheckBox_ShowMMF.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_CheckBox_ShowMMF.Location = new Point(497, 78);
            Control_ReceivingAnalytics_CheckBox_ShowMMF.Name = "Control_ReceivingAnalytics_CheckBox_ShowMMF";
            Control_ReceivingAnalytics_CheckBox_ShowMMF.Size = new Size(241, 19);
            Control_ReceivingAnalytics_CheckBox_ShowMMF.TabIndex = 21;
            Control_ReceivingAnalytics_CheckBox_ShowMMF.Text = "Only Flat Stock (MMF)";
            Control_ReceivingAnalytics_CheckBox_ShowMMF.UseVisualStyleBackColor = true;
            // 
            // Control_ReceivingAnalytics_Label_POState
            // 
            Control_ReceivingAnalytics_Label_POState.AutoSize = true;
            Control_ReceivingAnalytics_Label_POState.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Label_POState.Font = new Font("Segoe UI", 9F, FontStyle.Underline, GraphicsUnit.Point, 0);
            Control_ReceivingAnalytics_Label_POState.Location = new Point(3, 3);
            Control_ReceivingAnalytics_Label_POState.Margin = new Padding(3);
            Control_ReceivingAnalytics_Label_POState.Name = "Control_ReceivingAnalytics_Label_POState";
            Control_ReceivingAnalytics_Label_POState.Size = new Size(241, 19);
            Control_ReceivingAnalytics_Label_POState.TabIndex = 22;
            Control_ReceivingAnalytics_Label_POState.Text = "PO States";
            Control_ReceivingAnalytics_Label_POState.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_ReceivingAnalytics_Label_DeliveryStatus
            // 
            Control_ReceivingAnalytics_Label_DeliveryStatus.AutoSize = true;
            Control_ReceivingAnalytics_Label_DeliveryStatus.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Label_DeliveryStatus.Font = new Font("Segoe UI", 9F, FontStyle.Underline, GraphicsUnit.Point, 0);
            Control_ReceivingAnalytics_Label_DeliveryStatus.Location = new Point(250, 3);
            Control_ReceivingAnalytics_Label_DeliveryStatus.Margin = new Padding(3);
            Control_ReceivingAnalytics_Label_DeliveryStatus.Name = "Control_ReceivingAnalytics_Label_DeliveryStatus";
            Control_ReceivingAnalytics_Label_DeliveryStatus.Size = new Size(241, 19);
            Control_ReceivingAnalytics_Label_DeliveryStatus.TabIndex = 23;
            Control_ReceivingAnalytics_Label_DeliveryStatus.Text = "Delivery Status";
            Control_ReceivingAnalytics_Label_DeliveryStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_ReceivingAnalytics_Label_NotReceiving
            // 
            Control_ReceivingAnalytics_Label_NotReceiving.AutoSize = true;
            Control_ReceivingAnalytics_Label_NotReceiving.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Label_NotReceiving.Font = new Font("Segoe UI", 9F, FontStyle.Underline, GraphicsUnit.Point, 0);
            Control_ReceivingAnalytics_Label_NotReceiving.Location = new Point(744, 3);
            Control_ReceivingAnalytics_Label_NotReceiving.Margin = new Padding(3);
            Control_ReceivingAnalytics_Label_NotReceiving.Name = "Control_ReceivingAnalytics_Label_NotReceiving";
            Control_ReceivingAnalytics_Label_NotReceiving.Size = new Size(241, 19);
            Control_ReceivingAnalytics_Label_NotReceiving.TabIndex = 24;
            Control_ReceivingAnalytics_Label_NotReceiving.Text = "Outside Receiving Scope";
            Control_ReceivingAnalytics_Label_NotReceiving.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_ReceivingAnalytics_Label_OnlyReceiving
            // 
            Control_ReceivingAnalytics_Label_OnlyReceiving.AutoSize = true;
            Control_ReceivingAnalytics_Label_OnlyReceiving.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Label_OnlyReceiving.Font = new Font("Segoe UI", 9F, FontStyle.Underline, GraphicsUnit.Point, 0);
            Control_ReceivingAnalytics_Label_OnlyReceiving.Location = new Point(497, 3);
            Control_ReceivingAnalytics_Label_OnlyReceiving.Margin = new Padding(3);
            Control_ReceivingAnalytics_Label_OnlyReceiving.Name = "Control_ReceivingAnalytics_Label_OnlyReceiving";
            Control_ReceivingAnalytics_Label_OnlyReceiving.Size = new Size(241, 19);
            Control_ReceivingAnalytics_Label_OnlyReceiving.TabIndex = 25;
            Control_ReceivingAnalytics_Label_OnlyReceiving.Text = "Only Receiving Scope";
            Control_ReceivingAnalytics_Label_OnlyReceiving.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_Options
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_Options.AutoSize = true;
            Control_ReceivingAnalytics_TableLayoutPanel_Options.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_TableLayoutPanel_Options.ColumnCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_Options.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_Options.Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_Filters, 0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_Options.Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes, 0, 1);
            Control_ReceivingAnalytics_TableLayoutPanel_Options.Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_Buttons, 0, 2);
            Control_ReceivingAnalytics_TableLayoutPanel_Options.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_Options.Location = new Point(0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_Options.Name = "Control_ReceivingAnalytics_TableLayoutPanel_Options";
            Control_ReceivingAnalytics_TableLayoutPanel_Options.RowCount = 3;
            Control_ReceivingAnalytics_TableLayoutPanel_Options.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_Options.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_Options.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_Options.Size = new Size(994, 226);
            Control_ReceivingAnalytics_TableLayoutPanel_Options.TabIndex = 25;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_PartNumber
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_PartNumber.AutoSize = true;
            Control_ReceivingAnalytics_TableLayoutPanel_PartNumber.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_TableLayoutPanel_PartNumber.ColumnCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_PartNumber.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_PartNumber.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            Control_ReceivingAnalytics_TableLayoutPanel_PartNumber.Controls.Add(Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber, 0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_PartNumber.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_PartNumber.Location = new Point(661, 3);
            Control_ReceivingAnalytics_TableLayoutPanel_PartNumber.Name = "Control_ReceivingAnalytics_TableLayoutPanel_PartNumber";
            Control_ReceivingAnalytics_TableLayoutPanel_PartNumber.RowCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_PartNumber.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_PartNumber.Size = new Size(324, 29);
            Control_ReceivingAnalytics_TableLayoutPanel_PartNumber.TabIndex = 24;
            // 
            // Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber
            // 
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber.Location = new Point(3, 3);
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber.Name = "Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber";
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber.Size = new Size(318, 23);
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber.TabIndex = 12;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_Carrier
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_Carrier.AutoSize = true;
            Control_ReceivingAnalytics_TableLayoutPanel_Carrier.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_TableLayoutPanel_Carrier.ColumnCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_Carrier.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_Carrier.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            Control_ReceivingAnalytics_TableLayoutPanel_Carrier.Controls.Add(Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier, 0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_Carrier.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_Carrier.Location = new Point(661, 38);
            Control_ReceivingAnalytics_TableLayoutPanel_Carrier.Name = "Control_ReceivingAnalytics_TableLayoutPanel_Carrier";
            Control_ReceivingAnalytics_TableLayoutPanel_Carrier.RowCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_Carrier.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_Carrier.Size = new Size(324, 29);
            Control_ReceivingAnalytics_TableLayoutPanel_Carrier.TabIndex = 25;
            // 
            // Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier
            // 
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier.Location = new Point(3, 3);
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier.Name = "Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier";
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier.Size = new Size(318, 23);
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier.TabIndex = 12;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_Buttons
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.AutoSize = true;
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.ColumnCount = 3;
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.Controls.Add(Control_ReceivingAnalytics_Button_ToggleOptions, 2, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.Controls.Add(Control_ReceivingAnalytics_Button_Search, 0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.Controls.Add(Control_ReceivingAnalytics_Button_Analytics, 1, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.Location = new Point(3, 185);
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.Name = "Control_ReceivingAnalytics_TableLayoutPanel_Buttons";
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.RowCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.Size = new Size(988, 38);
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.TabIndex = 25;
            // 
            // Control_ReceivingAnalytics_Button_ToggleOptions
            // 
            Control_ReceivingAnalytics_Button_ToggleOptions.AutoSize = true;
            Control_ReceivingAnalytics_Button_ToggleOptions.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_Button_ToggleOptions.BackColor = Color.SteelBlue;
            Control_ReceivingAnalytics_Button_ToggleOptions.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Button_ToggleOptions.FlatStyle = FlatStyle.Flat;
            Control_ReceivingAnalytics_Button_ToggleOptions.ForeColor = Color.White;
            Control_ReceivingAnalytics_Button_ToggleOptions.Location = new Point(953, 3);
            Control_ReceivingAnalytics_Button_ToggleOptions.MaximumSize = new Size(32, 32);
            Control_ReceivingAnalytics_Button_ToggleOptions.MinimumSize = new Size(32, 32);
            Control_ReceivingAnalytics_Button_ToggleOptions.Name = "Control_ReceivingAnalytics_Button_ToggleOptions";
            Control_ReceivingAnalytics_Button_ToggleOptions.Size = new Size(32, 32);
            Control_ReceivingAnalytics_Button_ToggleOptions.TabIndex = 11;
            Control_ReceivingAnalytics_Button_ToggleOptions.UseVisualStyleBackColor = false;
            // 
            // Control_ReceivingAnalytics_Button_Analytics
            // 
            Control_ReceivingAnalytics_Button_Analytics.AutoSize = true;
            Control_ReceivingAnalytics_Button_Analytics.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_Button_Analytics.BackColor = Color.Teal;
            Control_ReceivingAnalytics_Button_Analytics.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Button_Analytics.FlatStyle = FlatStyle.Flat;
            Control_ReceivingAnalytics_Button_Analytics.ForeColor = Color.White;
            Control_ReceivingAnalytics_Button_Analytics.Location = new Point(758, 3);
            Control_ReceivingAnalytics_Button_Analytics.MaximumSize = new Size(0, 32);
            Control_ReceivingAnalytics_Button_Analytics.MinimumSize = new Size(0, 32);
            Control_ReceivingAnalytics_Button_Analytics.Name = "Control_ReceivingAnalytics_Button_Analytics";
            Control_ReceivingAnalytics_Button_Analytics.Size = new Size(189, 32);
            Control_ReceivingAnalytics_Button_Analytics.TabIndex = 12;
            Control_ReceivingAnalytics_Button_Analytics.Text = "Receiving Analytics";
            Control_ReceivingAnalytics_Button_Analytics.UseVisualStyleBackColor = false;
            // 
            // Control_ReceivingAnalytics_Panel_CheckBoxes_Legend
            // 
            Control_ReceivingAnalytics_Panel_CheckBoxes_Legend.AutoSize = true;
            Control_ReceivingAnalytics_Panel_CheckBoxes_Legend.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_Panel_CheckBoxes_Legend.BorderStyle = BorderStyle.FixedSingle;
            Control_ReceivingAnalytics_Panel_CheckBoxes_Legend.Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_Legend);
            Control_ReceivingAnalytics_Panel_CheckBoxes_Legend.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Panel_CheckBoxes_Legend.Location = new Point(3, 564);
            Control_ReceivingAnalytics_Panel_CheckBoxes_Legend.Name = "Control_ReceivingAnalytics_Panel_CheckBoxes_Legend";
            Control_ReceivingAnalytics_Panel_CheckBoxes_Legend.Padding = new Padding(5);
            Control_ReceivingAnalytics_Panel_CheckBoxes_Legend.Size = new Size(994, 33);
            Control_ReceivingAnalytics_Panel_CheckBoxes_Legend.TabIndex = 2;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_Legend
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.AutoSize = true;
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.ColumnCount = 10;
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.Controls.Add(Control_ReceivingAnalytics_Label_LegendOnTime, 8, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.Controls.Add(Control_ReceivingAnalytics_Label_LegendPartial, 6, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.Controls.Add(Control_ReceivingAnalytics_Label_LegendLate, 4, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.Controls.Add(Control_ReceivingAnalytics_Label_LegendClosed, 2, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.Controls.Add(Control_ReceivingAnalytics_Panel_LegendClosed, 1, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.Controls.Add(Control_ReceivingAnalytics_Panel_LegendLate, 3, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.Controls.Add(Control_ReceivingAnalytics_Panel_LegendPartial, 5, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.Controls.Add(Control_ReceivingAnalytics_Panel_LegendOnTime, 7, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.Controls.Add(Control_ReceivingAnalytics_Label_LegendTitle, 0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.Location = new Point(5, 5);
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.Name = "Control_ReceivingAnalytics_TableLayoutPanel_Legend";
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.RowCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.Size = new Size(982, 21);
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.TabIndex = 11;
            // 
            // Control_ReceivingAnalytics_Label_LegendOnTime
            // 
            Control_ReceivingAnalytics_Label_LegendOnTime.AutoSize = true;
            Control_ReceivingAnalytics_Label_LegendOnTime.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Label_LegendOnTime.Location = new Point(274, 3);
            Control_ReceivingAnalytics_Label_LegendOnTime.Margin = new Padding(3);
            Control_ReceivingAnalytics_Label_LegendOnTime.Name = "Control_ReceivingAnalytics_Label_LegendOnTime";
            Control_ReceivingAnalytics_Label_LegendOnTime.Size = new Size(93, 15);
            Control_ReceivingAnalytics_Label_LegendOnTime.TabIndex = 8;
            Control_ReceivingAnalytics_Label_LegendOnTime.Text = "On Time / Open";
            Control_ReceivingAnalytics_Label_LegendOnTime.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_ReceivingAnalytics_Label_LegendPartial
            // 
            Control_ReceivingAnalytics_Label_LegendPartial.AutoSize = true;
            Control_ReceivingAnalytics_Label_LegendPartial.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Label_LegendPartial.Location = new Point(207, 3);
            Control_ReceivingAnalytics_Label_LegendPartial.Margin = new Padding(3);
            Control_ReceivingAnalytics_Label_LegendPartial.Name = "Control_ReceivingAnalytics_Label_LegendPartial";
            Control_ReceivingAnalytics_Label_LegendPartial.Size = new Size(40, 15);
            Control_ReceivingAnalytics_Label_LegendPartial.TabIndex = 6;
            Control_ReceivingAnalytics_Label_LegendPartial.Text = "Partial";
            Control_ReceivingAnalytics_Label_LegendPartial.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_ReceivingAnalytics_Label_LegendLate
            // 
            Control_ReceivingAnalytics_Label_LegendLate.AutoSize = true;
            Control_ReceivingAnalytics_Label_LegendLate.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Label_LegendLate.Location = new Point(151, 3);
            Control_ReceivingAnalytics_Label_LegendLate.Margin = new Padding(3);
            Control_ReceivingAnalytics_Label_LegendLate.Name = "Control_ReceivingAnalytics_Label_LegendLate";
            Control_ReceivingAnalytics_Label_LegendLate.Size = new Size(29, 15);
            Control_ReceivingAnalytics_Label_LegendLate.TabIndex = 4;
            Control_ReceivingAnalytics_Label_LegendLate.Text = "Late";
            Control_ReceivingAnalytics_Label_LegendLate.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_ReceivingAnalytics_Label_LegendClosed
            // 
            Control_ReceivingAnalytics_Label_LegendClosed.AutoSize = true;
            Control_ReceivingAnalytics_Label_LegendClosed.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Label_LegendClosed.Location = new Point(81, 3);
            Control_ReceivingAnalytics_Label_LegendClosed.Margin = new Padding(3);
            Control_ReceivingAnalytics_Label_LegendClosed.Name = "Control_ReceivingAnalytics_Label_LegendClosed";
            Control_ReceivingAnalytics_Label_LegendClosed.Size = new Size(43, 15);
            Control_ReceivingAnalytics_Label_LegendClosed.TabIndex = 2;
            Control_ReceivingAnalytics_Label_LegendClosed.Text = "Closed";
            Control_ReceivingAnalytics_Label_LegendClosed.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_ReceivingAnalytics_Panel_LegendClosed
            // 
            Control_ReceivingAnalytics_Panel_LegendClosed.BackColor = Color.FromArgb(200, 255, 200);
            Control_ReceivingAnalytics_Panel_LegendClosed.BorderStyle = BorderStyle.FixedSingle;
            Control_ReceivingAnalytics_Panel_LegendClosed.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Panel_LegendClosed.Location = new Point(60, 3);
            Control_ReceivingAnalytics_Panel_LegendClosed.MaximumSize = new Size(15, 15);
            Control_ReceivingAnalytics_Panel_LegendClosed.MinimumSize = new Size(15, 15);
            Control_ReceivingAnalytics_Panel_LegendClosed.Name = "Control_ReceivingAnalytics_Panel_LegendClosed";
            Control_ReceivingAnalytics_Panel_LegendClosed.Size = new Size(15, 15);
            Control_ReceivingAnalytics_Panel_LegendClosed.TabIndex = 1;
            // 
            // Control_ReceivingAnalytics_Panel_LegendLate
            // 
            Control_ReceivingAnalytics_Panel_LegendLate.BackColor = Color.FromArgb(255, 200, 200);
            Control_ReceivingAnalytics_Panel_LegendLate.BorderStyle = BorderStyle.FixedSingle;
            Control_ReceivingAnalytics_Panel_LegendLate.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Panel_LegendLate.Location = new Point(130, 3);
            Control_ReceivingAnalytics_Panel_LegendLate.MaximumSize = new Size(15, 15);
            Control_ReceivingAnalytics_Panel_LegendLate.MinimumSize = new Size(15, 15);
            Control_ReceivingAnalytics_Panel_LegendLate.Name = "Control_ReceivingAnalytics_Panel_LegendLate";
            Control_ReceivingAnalytics_Panel_LegendLate.Size = new Size(15, 15);
            Control_ReceivingAnalytics_Panel_LegendLate.TabIndex = 3;
            // 
            // Control_ReceivingAnalytics_Panel_LegendPartial
            // 
            Control_ReceivingAnalytics_Panel_LegendPartial.BackColor = Color.FromArgb(255, 255, 200);
            Control_ReceivingAnalytics_Panel_LegendPartial.BorderStyle = BorderStyle.FixedSingle;
            Control_ReceivingAnalytics_Panel_LegendPartial.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Panel_LegendPartial.Location = new Point(186, 3);
            Control_ReceivingAnalytics_Panel_LegendPartial.MaximumSize = new Size(15, 15);
            Control_ReceivingAnalytics_Panel_LegendPartial.MinimumSize = new Size(15, 15);
            Control_ReceivingAnalytics_Panel_LegendPartial.Name = "Control_ReceivingAnalytics_Panel_LegendPartial";
            Control_ReceivingAnalytics_Panel_LegendPartial.Size = new Size(15, 15);
            Control_ReceivingAnalytics_Panel_LegendPartial.TabIndex = 5;
            // 
            // Control_ReceivingAnalytics_Panel_LegendOnTime
            // 
            Control_ReceivingAnalytics_Panel_LegendOnTime.BackColor = Color.FromArgb(200, 240, 255);
            Control_ReceivingAnalytics_Panel_LegendOnTime.BorderStyle = BorderStyle.FixedSingle;
            Control_ReceivingAnalytics_Panel_LegendOnTime.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Panel_LegendOnTime.Location = new Point(253, 3);
            Control_ReceivingAnalytics_Panel_LegendOnTime.MaximumSize = new Size(15, 15);
            Control_ReceivingAnalytics_Panel_LegendOnTime.MinimumSize = new Size(15, 15);
            Control_ReceivingAnalytics_Panel_LegendOnTime.Name = "Control_ReceivingAnalytics_Panel_LegendOnTime";
            Control_ReceivingAnalytics_Panel_LegendOnTime.Size = new Size(15, 15);
            Control_ReceivingAnalytics_Panel_LegendOnTime.TabIndex = 7;
            // 
            // Control_ReceivingAnalytics_Label_LegendTitle
            // 
            Control_ReceivingAnalytics_Label_LegendTitle.AutoSize = true;
            Control_ReceivingAnalytics_Label_LegendTitle.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Label_LegendTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Control_ReceivingAnalytics_Label_LegendTitle.Location = new Point(3, 3);
            Control_ReceivingAnalytics_Label_LegendTitle.Margin = new Padding(3);
            Control_ReceivingAnalytics_Label_LegendTitle.Name = "Control_ReceivingAnalytics_Label_LegendTitle";
            Control_ReceivingAnalytics_Label_LegendTitle.Size = new Size(51, 15);
            Control_ReceivingAnalytics_Label_LegendTitle.TabIndex = 0;
            Control_ReceivingAnalytics_Label_LegendTitle.Text = "Legend:";
            Control_ReceivingAnalytics_Label_LegendTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_ReceivingAnalytics
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_Main);
            Name = "Control_ReceivingAnalytics";
            Size = new Size(1000, 600);
            Control_ReceivingAnalytics_TableLayoutPanel_Main.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_Main.PerformLayout();
            Control_ReceivingAnalytics_Panel_Filters.ResumeLayout(false);
            Control_ReceivingAnalytics_Panel_Filters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Control_ReceivingAnalytics_DataGridView_Results).EndInit();
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes.PerformLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_PONumber.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_PONumber.PerformLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.PerformLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_FilterBy.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_Vendor.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_Vendor.PerformLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_Filters.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_Filters.PerformLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_Options.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_Options.PerformLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_PartNumber.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_PartNumber.PerformLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_Carrier.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_Carrier.PerformLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.PerformLayout();
            Control_ReceivingAnalytics_Panel_CheckBoxes_Legend.ResumeLayout(false);
            Control_ReceivingAnalytics_Panel_CheckBoxes_Legend.PerformLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_Main;
        private System.Windows.Forms.Panel Control_ReceivingAnalytics_Panel_Filters;
        private System.Windows.Forms.DataGridView Control_ReceivingAnalytics_DataGridView_Results;
        private System.Windows.Forms.Label Control_ReceivingAnalytics_Label_DateRange;
        private System.Windows.Forms.DateTimePicker Control_ReceivingAnalytics_DateTimePicker_StartDate;
        private System.Windows.Forms.Label Control_ReceivingAnalytics_Label_DateRangeTo;
        private System.Windows.Forms.DateTimePicker Control_ReceivingAnalytics_DateTimePicker_EndDate;
        private System.Windows.Forms.CheckBox Control_ReceivingAnalytics_CheckBox_ShowClosed;
        private System.Windows.Forms.CheckBox Control_ReceivingAnalytics_CheckBox_ShowConsignment;
        private System.Windows.Forms.CheckBox Control_ReceivingAnalytics_CheckBox_ShowInternal;
        private System.Windows.Forms.CheckBox Control_ReceivingAnalytics_CheckBox_ShowOutsideService;
        private System.Windows.Forms.Button Control_ReceivingAnalytics_Button_Search;
        private System.Windows.Forms.CheckBox Control_ReceivingAnalytics_CheckBox_ShowLate;
        private System.Windows.Forms.CheckBox Control_ReceivingAnalytics_CheckBox_ShowPartial;
        private System.Windows.Forms.CheckBox Control_ReceivingAnalytics_CheckBox_ShowOnTime;
        private System.Windows.Forms.CheckBox Control_ReceivingAnalytics_CheckBox_ShowWithPartID;
        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_DateRange;
        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_PONumber;
        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_CheckBoxes;
        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_Vendor;
        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_FilterBy;
        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_Filters;
        private CheckBox Control_ReceivingAnalytics_CheckBox_ShowOpen;
        private CheckBox Control_ReceivingAnalytics_CheckBox_ShowMMC;
        private CheckBox Control_ReceivingAnalytics_CheckBox_ShowMMF;
        private Label Control_ReceivingAnalytics_Label_POState;
        private Label Control_ReceivingAnalytics_Label_DeliveryStatus;
        private Label Control_ReceivingAnalytics_Label_OnlyReceiving;
        private Label Control_ReceivingAnalytics_Label_NotReceiving;
        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_Carrier;
        private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier;
        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_PartNumber;
        private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber;
        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_Buttons;
        private Button Control_ReceivingAnalytics_Button_ToggleOptions;
        private Panel Control_ReceivingAnalytics_Panel_CheckBoxes_Legend;
        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_Legend;
        private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber;
        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_Options;
        private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier;
        private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_DateType;
        private Label Control_ReceivingAnalytics_Label_LegendOnTime;
        private Label Control_ReceivingAnalytics_Label_LegendPartial;
        private Label Control_ReceivingAnalytics_Label_LegendLate;
        private Label Control_ReceivingAnalytics_Label_LegendClosed;
        private Panel Control_ReceivingAnalytics_Panel_LegendClosed;
        private Panel Control_ReceivingAnalytics_Panel_LegendLate;
        private Panel Control_ReceivingAnalytics_Panel_LegendPartial;
        private Panel Control_ReceivingAnalytics_Panel_LegendOnTime;
        private Label Control_ReceivingAnalytics_Label_LegendTitle;
        private Button Control_ReceivingAnalytics_Button_Analytics;
    }
}
