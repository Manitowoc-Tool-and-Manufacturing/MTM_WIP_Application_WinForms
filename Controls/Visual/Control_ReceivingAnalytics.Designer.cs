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
            Control_ReceivingAnalytics_Panel_DataGridView = new Panel();
            Control_ReceivingAnalytics_DataGridView_Results = new DataGridView();
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons = new TableLayoutPanel();
            Control_ReceivingAnalytics_Button_Search = new Button();
            Control_ReceivingAnalytics_Button_Analytics = new Button();
            Control_ReceivingAnalytics_CheckBox_ShowOutsideService = new CheckBox();
            Control_ReceivingAnalytics_CheckBox_ShowConsignment = new CheckBox();
            Control_ReceivingAnalytics_CheckBox_ShowInternal = new CheckBox();
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_DateType = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            Control_ReceivingAnalytics_CheckBox_ShowClosed = new CheckBox();
            Control_ReceivingAnalytics_CheckBox_ShowPartial = new CheckBox();
            Control_ReceivingAnalytics_CheckBox_ShowOpen = new CheckBox();
            Control_ReceivingAnalytics_CheckBox_ShowOnTime = new CheckBox();
            Control_ReceivingAnalytics_CheckBox_ShowLate = new CheckBox();
            Control_ReceivingAnalytics_CheckBox_ShowWithPartID = new CheckBox();
            Control_ReceivingAnalytics_CheckBox_ShowMMC = new CheckBox();
            Control_ReceivingAnalytics_CheckBox_ShowMMF = new CheckBox();
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange = new TableLayoutPanel();
            Control_ReceivingAnalytics_DateTimePicker_StartDate = new DateTimePicker();
            Control_ReceivingAnalytics_Label_DateRangeTo = new Label();
            Control_ReceivingAnalytics_DateTimePicker_EndDate = new DateTimePicker();
            Control_ReceivingAnalytics_Label_DateRange = new Label();
            Control_ReceivingAnalytics_TableLayoutPanel_WeekButtons = new TableLayoutPanel();
            Control_ReceivingAnalytics_Button_NextWeek = new Button();
            Control_ReceivingAnalytics_Button_PreviousWeek = new Button();
            Control_ReceivingAnalytics_Button_CurrentWeek = new Button();
            Control_ReceivingAnalytics_TableLayoutPanel_Main = new TableLayoutPanel();
            Control_ReceivingAnalytics_Panel_Results = new Panel();
            Control_ReceivingAnalytics_TableLayoutPanel_Results = new TableLayoutPanel();
            tableLayoutPanel1 = new TableLayoutPanel();
            Control_ReceivingAnalytics_Panel_Buttons = new Panel();
            Control_ReceivingAnalytics_Panel_SideBarMain = new Panel();
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarMain = new TableLayoutPanel();
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeMain = new TableLayoutPanel();
            Control_ReceivingAnalytics_Panel_DateRangeHeader = new Panel();
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeHeader = new TableLayoutPanel();
            Control_ReceivingAnalytics_Button_DateRangeHeader = new Button();
            Control_ReceivingAnalytics_Panel_DateRangeContents = new Panel();
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeContents = new TableLayoutPanel();
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersMain = new TableLayoutPanel();
            Control_ReceivingAnalytics_Panel_FiltersContents = new Panel();
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersContents = new TableLayoutPanel();
            Control_ReceivingAnalytics_Panel_FiltersHeader = new Panel();
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersHeader = new TableLayoutPanel();
            Control_ReceivingAnalytics_Button_FiltersHeader = new Button();
            Control_ReceivingAnalytics_Label_SideBarFilters = new Label();
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesMain = new TableLayoutPanel();
            Control_ReceivingAnalytics_Panel_POStatesHeader = new Panel();
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesHeader = new TableLayoutPanel();
            Control_ReceivingAnalytics_Button_POStatesHeader = new Button();
            Control_ReceivingAnalytics_Label_SideBarPOStates = new Label();
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesContents = new Panel();
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarPOStates = new TableLayoutPanel();
            Control_ReceivingAnalytics_Panel_OutsideScopeMain = new TableLayoutPanel();
            Control_ReceivingAnalytics_Panel_OutsideScopeHeader = new Panel();
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeHeader = new TableLayoutPanel();
            Control_ReceivingAnalytics_Button_OutsideScopeHeader = new Button();
            Control_ReceivingAnalytics_Label_OutsideScopeHeader = new Label();
            Control_ReceivingAnalytics_Panel_OutsideScopeContents = new Panel();
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeContents = new TableLayoutPanel();
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesMain = new TableLayoutPanel();
            Control_ReceivingAnalytics_Panel_DeliveryStatusHeader = new Panel();
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesHeader = new TableLayoutPanel();
            Control_ReceivingAnalytics_Button_DeliveryStatesHeader = new Button();
            Control_ReceivingAnalytics_Label_DeliveryStatesHeader = new Label();
            Control_ReceivingAnalytics_Panel_DeliveryStatesContents = new Panel();
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesContents = new TableLayoutPanel();
            Control_ReceivingAnalytics_Panel_ReceivingScopeMain = new TableLayoutPanel();
            Control_ReceivingAnalytics_Panel_ReceivingScopeHeader = new Panel();
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeHeader = new TableLayoutPanel();
            Control_ReceivingAnalytics_Button_ReceivingScopeHeader = new Button();
            Control_ReceivingAnalytics_Label_ReceivingScopeHeader = new Label();
            Control_ReceivingAnalytics_Panel_ReceivingScopeContents = new Panel();
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeContents = new TableLayoutPanel();
            Control_ReceivingAnalytics_Panel_CheckBoxes_Legend.SuspendLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.SuspendLayout();
            Control_ReceivingAnalytics_Panel_DataGridView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Control_ReceivingAnalytics_DataGridView_Results).BeginInit();
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.SuspendLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.SuspendLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_WeekButtons.SuspendLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_Main.SuspendLayout();
            Control_ReceivingAnalytics_Panel_Results.SuspendLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_Results.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            Control_ReceivingAnalytics_Panel_Buttons.SuspendLayout();
            Control_ReceivingAnalytics_Panel_SideBarMain.SuspendLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarMain.SuspendLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeMain.SuspendLayout();
            Control_ReceivingAnalytics_Panel_DateRangeHeader.SuspendLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeHeader.SuspendLayout();
            Control_ReceivingAnalytics_Panel_DateRangeContents.SuspendLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeContents.SuspendLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersMain.SuspendLayout();
            Control_ReceivingAnalytics_Panel_FiltersContents.SuspendLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersContents.SuspendLayout();
            Control_ReceivingAnalytics_Panel_FiltersHeader.SuspendLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersHeader.SuspendLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesMain.SuspendLayout();
            Control_ReceivingAnalytics_Panel_POStatesHeader.SuspendLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesHeader.SuspendLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesContents.SuspendLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarPOStates.SuspendLayout();
            Control_ReceivingAnalytics_Panel_OutsideScopeMain.SuspendLayout();
            Control_ReceivingAnalytics_Panel_OutsideScopeHeader.SuspendLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeHeader.SuspendLayout();
            Control_ReceivingAnalytics_Panel_OutsideScopeContents.SuspendLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeContents.SuspendLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesMain.SuspendLayout();
            Control_ReceivingAnalytics_Panel_DeliveryStatusHeader.SuspendLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesHeader.SuspendLayout();
            Control_ReceivingAnalytics_Panel_DeliveryStatesContents.SuspendLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesContents.SuspendLayout();
            Control_ReceivingAnalytics_Panel_ReceivingScopeMain.SuspendLayout();
            Control_ReceivingAnalytics_Panel_ReceivingScopeHeader.SuspendLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeHeader.SuspendLayout();
            Control_ReceivingAnalytics_Panel_ReceivingScopeContents.SuspendLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeContents.SuspendLayout();
            SuspendLayout();
            // 
            // Control_ReceivingAnalytics_Panel_CheckBoxes_Legend
            // 
            Control_ReceivingAnalytics_Panel_CheckBoxes_Legend.AutoSize = true;
            Control_ReceivingAnalytics_Panel_CheckBoxes_Legend.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_Panel_CheckBoxes_Legend.BorderStyle = BorderStyle.FixedSingle;
            Control_ReceivingAnalytics_Panel_CheckBoxes_Legend.Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_Legend);
            Control_ReceivingAnalytics_Panel_CheckBoxes_Legend.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Panel_CheckBoxes_Legend.Location = new Point(3, 830);
            Control_ReceivingAnalytics_Panel_CheckBoxes_Legend.Name = "Control_ReceivingAnalytics_Panel_CheckBoxes_Legend";
            Control_ReceivingAnalytics_Panel_CheckBoxes_Legend.Padding = new Padding(5);
            Control_ReceivingAnalytics_Panel_CheckBoxes_Legend.Size = new Size(1176, 34);
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
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.Size = new Size(1164, 22);
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.TabIndex = 11;
            // 
            // Control_ReceivingAnalytics_Label_LegendOnTime
            // 
            Control_ReceivingAnalytics_Label_LegendOnTime.AutoSize = true;
            Control_ReceivingAnalytics_Label_LegendOnTime.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Label_LegendOnTime.Location = new Point(279, 3);
            Control_ReceivingAnalytics_Label_LegendOnTime.Margin = new Padding(3);
            Control_ReceivingAnalytics_Label_LegendOnTime.Name = "Control_ReceivingAnalytics_Label_LegendOnTime";
            Control_ReceivingAnalytics_Label_LegendOnTime.Size = new Size(93, 16);
            Control_ReceivingAnalytics_Label_LegendOnTime.TabIndex = 8;
            Control_ReceivingAnalytics_Label_LegendOnTime.Text = "On Time / Open";
            Control_ReceivingAnalytics_Label_LegendOnTime.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_ReceivingAnalytics_Label_LegendPartial
            // 
            Control_ReceivingAnalytics_Label_LegendPartial.AutoSize = true;
            Control_ReceivingAnalytics_Label_LegendPartial.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Label_LegendPartial.Location = new Point(212, 3);
            Control_ReceivingAnalytics_Label_LegendPartial.Margin = new Padding(3);
            Control_ReceivingAnalytics_Label_LegendPartial.Name = "Control_ReceivingAnalytics_Label_LegendPartial";
            Control_ReceivingAnalytics_Label_LegendPartial.Size = new Size(40, 16);
            Control_ReceivingAnalytics_Label_LegendPartial.TabIndex = 6;
            Control_ReceivingAnalytics_Label_LegendPartial.Text = "Partial";
            Control_ReceivingAnalytics_Label_LegendPartial.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_ReceivingAnalytics_Label_LegendLate
            // 
            Control_ReceivingAnalytics_Label_LegendLate.AutoSize = true;
            Control_ReceivingAnalytics_Label_LegendLate.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Label_LegendLate.Location = new Point(156, 3);
            Control_ReceivingAnalytics_Label_LegendLate.Margin = new Padding(3);
            Control_ReceivingAnalytics_Label_LegendLate.Name = "Control_ReceivingAnalytics_Label_LegendLate";
            Control_ReceivingAnalytics_Label_LegendLate.Size = new Size(29, 16);
            Control_ReceivingAnalytics_Label_LegendLate.TabIndex = 4;
            Control_ReceivingAnalytics_Label_LegendLate.Text = "Late";
            Control_ReceivingAnalytics_Label_LegendLate.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_ReceivingAnalytics_Label_LegendClosed
            // 
            Control_ReceivingAnalytics_Label_LegendClosed.AutoSize = true;
            Control_ReceivingAnalytics_Label_LegendClosed.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Label_LegendClosed.Location = new Point(86, 3);
            Control_ReceivingAnalytics_Label_LegendClosed.Margin = new Padding(3);
            Control_ReceivingAnalytics_Label_LegendClosed.Name = "Control_ReceivingAnalytics_Label_LegendClosed";
            Control_ReceivingAnalytics_Label_LegendClosed.Size = new Size(43, 16);
            Control_ReceivingAnalytics_Label_LegendClosed.TabIndex = 2;
            Control_ReceivingAnalytics_Label_LegendClosed.Text = "Closed";
            Control_ReceivingAnalytics_Label_LegendClosed.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_ReceivingAnalytics_Panel_LegendClosed
            // 
            Control_ReceivingAnalytics_Panel_LegendClosed.BackColor = Color.FromArgb(200, 255, 200);
            Control_ReceivingAnalytics_Panel_LegendClosed.BorderStyle = BorderStyle.FixedSingle;
            Control_ReceivingAnalytics_Panel_LegendClosed.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Panel_LegendClosed.Location = new Point(65, 3);
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
            Control_ReceivingAnalytics_Panel_LegendLate.Location = new Point(135, 3);
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
            Control_ReceivingAnalytics_Panel_LegendPartial.Location = new Point(191, 3);
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
            Control_ReceivingAnalytics_Panel_LegendOnTime.Location = new Point(258, 3);
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
            Control_ReceivingAnalytics_Label_LegendTitle.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Bold);
            Control_ReceivingAnalytics_Label_LegendTitle.Location = new Point(3, 3);
            Control_ReceivingAnalytics_Label_LegendTitle.Margin = new Padding(3);
            Control_ReceivingAnalytics_Label_LegendTitle.Name = "Control_ReceivingAnalytics_Label_LegendTitle";
            Control_ReceivingAnalytics_Label_LegendTitle.Size = new Size(56, 16);
            Control_ReceivingAnalytics_Label_LegendTitle.TabIndex = 0;
            Control_ReceivingAnalytics_Label_LegendTitle.Text = "Legend:";
            Control_ReceivingAnalytics_Label_LegendTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_ReceivingAnalytics_Panel_DataGridView
            // 
            Control_ReceivingAnalytics_Panel_DataGridView.AutoSize = true;
            Control_ReceivingAnalytics_Panel_DataGridView.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_Panel_DataGridView.Controls.Add(Control_ReceivingAnalytics_DataGridView_Results);
            Control_ReceivingAnalytics_Panel_DataGridView.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Panel_DataGridView.Location = new Point(3, 3);
            Control_ReceivingAnalytics_Panel_DataGridView.Name = "Control_ReceivingAnalytics_Panel_DataGridView";
            Control_ReceivingAnalytics_Panel_DataGridView.Size = new Size(1176, 821);
            Control_ReceivingAnalytics_Panel_DataGridView.TabIndex = 3;
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
            Control_ReceivingAnalytics_DataGridView_Results.Location = new Point(0, 0);
            Control_ReceivingAnalytics_DataGridView_Results.Name = "Control_ReceivingAnalytics_DataGridView_Results";
            Control_ReceivingAnalytics_DataGridView_Results.ReadOnly = true;
            Control_ReceivingAnalytics_DataGridView_Results.RowHeadersVisible = false;
            Control_ReceivingAnalytics_DataGridView_Results.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Control_ReceivingAnalytics_DataGridView_Results.Size = new Size(1176, 821);
            Control_ReceivingAnalytics_DataGridView_Results.TabIndex = 2;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_Buttons
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.AutoSize = true;
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.ColumnCount = 2;
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.Controls.Add(Control_ReceivingAnalytics_Button_Search, 0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.Controls.Add(Control_ReceivingAnalytics_Button_Analytics, 1, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.Location = new Point(0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.Name = "Control_ReceivingAnalytics_TableLayoutPanel_Buttons";
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.RowCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.Size = new Size(296, 46);
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.TabIndex = 25;
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
            Control_ReceivingAnalytics_Button_Search.MaximumSize = new Size(0, 40);
            Control_ReceivingAnalytics_Button_Search.MinimumSize = new Size(0, 40);
            Control_ReceivingAnalytics_Button_Search.Name = "Control_ReceivingAnalytics_Button_Search";
            Control_ReceivingAnalytics_Button_Search.Size = new Size(142, 40);
            Control_ReceivingAnalytics_Button_Search.TabIndex = 10;
            Control_ReceivingAnalytics_Button_Search.Text = "Search";
            Control_ReceivingAnalytics_Button_Search.UseVisualStyleBackColor = false;
            // 
            // Control_ReceivingAnalytics_Button_Analytics
            // 
            Control_ReceivingAnalytics_Button_Analytics.AutoSize = true;
            Control_ReceivingAnalytics_Button_Analytics.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_Button_Analytics.BackColor = Color.Teal;
            Control_ReceivingAnalytics_Button_Analytics.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Button_Analytics.FlatStyle = FlatStyle.Flat;
            Control_ReceivingAnalytics_Button_Analytics.ForeColor = Color.White;
            Control_ReceivingAnalytics_Button_Analytics.Location = new Point(151, 3);
            Control_ReceivingAnalytics_Button_Analytics.MaximumSize = new Size(0, 40);
            Control_ReceivingAnalytics_Button_Analytics.MinimumSize = new Size(0, 40);
            Control_ReceivingAnalytics_Button_Analytics.Name = "Control_ReceivingAnalytics_Button_Analytics";
            Control_ReceivingAnalytics_Button_Analytics.Size = new Size(142, 40);
            Control_ReceivingAnalytics_Button_Analytics.TabIndex = 12;
            Control_ReceivingAnalytics_Button_Analytics.Text = "Receiving Analytics";
            Control_ReceivingAnalytics_Button_Analytics.UseVisualStyleBackColor = false;
            // 
            // Control_ReceivingAnalytics_CheckBox_ShowOutsideService
            // 
            Control_ReceivingAnalytics_CheckBox_ShowOutsideService.AutoSize = true;
            Control_ReceivingAnalytics_CheckBox_ShowOutsideService.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_CheckBox_ShowOutsideService.Location = new Point(3, 3);
            Control_ReceivingAnalytics_CheckBox_ShowOutsideService.Name = "Control_ReceivingAnalytics_CheckBox_ShowOutsideService";
            Control_ReceivingAnalytics_CheckBox_ShowOutsideService.Size = new Size(276, 19);
            Control_ReceivingAnalytics_CheckBox_ShowOutsideService.TabIndex = 9;
            Control_ReceivingAnalytics_CheckBox_ShowOutsideService.Text = "Show Outside Service";
            Control_ReceivingAnalytics_CheckBox_ShowOutsideService.UseVisualStyleBackColor = true;
            // 
            // Control_ReceivingAnalytics_CheckBox_ShowConsignment
            // 
            Control_ReceivingAnalytics_CheckBox_ShowConsignment.AutoSize = true;
            Control_ReceivingAnalytics_CheckBox_ShowConsignment.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_CheckBox_ShowConsignment.Location = new Point(3, 28);
            Control_ReceivingAnalytics_CheckBox_ShowConsignment.Name = "Control_ReceivingAnalytics_CheckBox_ShowConsignment";
            Control_ReceivingAnalytics_CheckBox_ShowConsignment.Size = new Size(276, 19);
            Control_ReceivingAnalytics_CheckBox_ShowConsignment.TabIndex = 7;
            Control_ReceivingAnalytics_CheckBox_ShowConsignment.Text = "Show Consignment";
            Control_ReceivingAnalytics_CheckBox_ShowConsignment.UseVisualStyleBackColor = true;
            // 
            // Control_ReceivingAnalytics_CheckBox_ShowInternal
            // 
            Control_ReceivingAnalytics_CheckBox_ShowInternal.AutoSize = true;
            Control_ReceivingAnalytics_CheckBox_ShowInternal.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_CheckBox_ShowInternal.Location = new Point(3, 53);
            Control_ReceivingAnalytics_CheckBox_ShowInternal.Name = "Control_ReceivingAnalytics_CheckBox_ShowInternal";
            Control_ReceivingAnalytics_CheckBox_ShowInternal.Size = new Size(276, 19);
            Control_ReceivingAnalytics_CheckBox_ShowInternal.TabIndex = 8;
            Control_ReceivingAnalytics_CheckBox_ShowInternal.Text = "Show Internal Orders";
            Control_ReceivingAnalytics_CheckBox_ShowInternal.UseVisualStyleBackColor = true;
            // 
            // Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber
            // 
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber.AutoSize = true;
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber.LabelVisible = "False";
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber.Location = new Point(3, 3);
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber.MaximumSize = new Size(275, 23);
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber.MaxLength = 100;
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber.MinimumSize = new Size(275, 23);
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber.MinLength = 100;
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber.Name = "Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber";
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber.PlaceholderText = "Search By Part Number";
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber.Size = new Size(275, 23);
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber.TabIndex = 12;
            // 
            // Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier
            // 
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier.AutoSize = true;
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier.LabelVisible = "False";
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier.Location = new Point(3, 60);
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier.MaximumSize = new Size(275, 23);
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier.MaxLength = 100;
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier.MinimumSize = new Size(275, 23);
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier.MinLength = 100;
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier.Name = "Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier";
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier.PlaceholderText = "Search By Supplier";
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier.Size = new Size(275, 23);
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier.TabIndex = 12;
            // 
            // Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier
            // 
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier.AutoSize = true;
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier.LabelVisible = "False";
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier.Location = new Point(3, 88);
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier.MaximumSize = new Size(275, 23);
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier.MaxLength = 100;
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier.MinimumSize = new Size(275, 23);
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier.MinLength = 100;
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier.Name = "Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier";
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier.PlaceholderText = "Search By Carrier (LTL)";
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier.Size = new Size(275, 23);
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier.TabIndex = 12;
            // 
            // Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber
            // 
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber.AutoSize = true;
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber.LabelVisible = "False";
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber.Location = new Point(3, 32);
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber.MaximumSize = new Size(275, 23);
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber.MaxLength = 100;
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber.MinimumSize = new Size(275, 23);
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber.MinLength = 100;
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber.Name = "Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber";
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber.PlaceholderText = "Search By PO Number";
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber.Size = new Size(275, 23);
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber.TabIndex = 14;
            // 
            // Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_DateType
            // 
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_DateType.AutoSize = true;
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_DateType.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_DateType.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_DateType.LabelVisible = "False";
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_DateType.Location = new Point(3, 116);
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_DateType.MaximumSize = new Size(275, 23);
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_DateType.MaxLength = 100;
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_DateType.MinimumSize = new Size(275, 23);
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_DateType.MinLength = 100;
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_DateType.Name = "Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_DateType";
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_DateType.PlaceholderText = "Sort By Date Type";
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_DateType.Size = new Size(275, 23);
            Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_DateType.TabIndex = 5;
            // 
            // Control_ReceivingAnalytics_CheckBox_ShowClosed
            // 
            Control_ReceivingAnalytics_CheckBox_ShowClosed.AutoSize = true;
            Control_ReceivingAnalytics_CheckBox_ShowClosed.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_CheckBox_ShowClosed.Location = new Point(3, 53);
            Control_ReceivingAnalytics_CheckBox_ShowClosed.Name = "Control_ReceivingAnalytics_CheckBox_ShowClosed";
            Control_ReceivingAnalytics_CheckBox_ShowClosed.Size = new Size(276, 19);
            Control_ReceivingAnalytics_CheckBox_ShowClosed.TabIndex = 6;
            Control_ReceivingAnalytics_CheckBox_ShowClosed.Text = "Show Closed";
            Control_ReceivingAnalytics_CheckBox_ShowClosed.UseVisualStyleBackColor = true;
            // 
            // Control_ReceivingAnalytics_CheckBox_ShowPartial
            // 
            Control_ReceivingAnalytics_CheckBox_ShowPartial.AutoSize = true;
            Control_ReceivingAnalytics_CheckBox_ShowPartial.Checked = true;
            Control_ReceivingAnalytics_CheckBox_ShowPartial.CheckState = CheckState.Checked;
            Control_ReceivingAnalytics_CheckBox_ShowPartial.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_CheckBox_ShowPartial.Location = new Point(3, 28);
            Control_ReceivingAnalytics_CheckBox_ShowPartial.Name = "Control_ReceivingAnalytics_CheckBox_ShowPartial";
            Control_ReceivingAnalytics_CheckBox_ShowPartial.Size = new Size(276, 19);
            Control_ReceivingAnalytics_CheckBox_ShowPartial.TabIndex = 16;
            Control_ReceivingAnalytics_CheckBox_ShowPartial.Text = "Show Partial";
            Control_ReceivingAnalytics_CheckBox_ShowPartial.UseVisualStyleBackColor = true;
            // 
            // Control_ReceivingAnalytics_CheckBox_ShowOpen
            // 
            Control_ReceivingAnalytics_CheckBox_ShowOpen.AutoSize = true;
            Control_ReceivingAnalytics_CheckBox_ShowOpen.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_CheckBox_ShowOpen.Location = new Point(3, 3);
            Control_ReceivingAnalytics_CheckBox_ShowOpen.Name = "Control_ReceivingAnalytics_CheckBox_ShowOpen";
            Control_ReceivingAnalytics_CheckBox_ShowOpen.Size = new Size(276, 19);
            Control_ReceivingAnalytics_CheckBox_ShowOpen.TabIndex = 19;
            Control_ReceivingAnalytics_CheckBox_ShowOpen.Text = "Show Open";
            Control_ReceivingAnalytics_CheckBox_ShowOpen.UseVisualStyleBackColor = true;
            // 
            // Control_ReceivingAnalytics_CheckBox_ShowOnTime
            // 
            Control_ReceivingAnalytics_CheckBox_ShowOnTime.AutoSize = true;
            Control_ReceivingAnalytics_CheckBox_ShowOnTime.Checked = true;
            Control_ReceivingAnalytics_CheckBox_ShowOnTime.CheckState = CheckState.Checked;
            Control_ReceivingAnalytics_CheckBox_ShowOnTime.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_CheckBox_ShowOnTime.Location = new Point(3, 3);
            Control_ReceivingAnalytics_CheckBox_ShowOnTime.Name = "Control_ReceivingAnalytics_CheckBox_ShowOnTime";
            Control_ReceivingAnalytics_CheckBox_ShowOnTime.Size = new Size(276, 19);
            Control_ReceivingAnalytics_CheckBox_ShowOnTime.TabIndex = 17;
            Control_ReceivingAnalytics_CheckBox_ShowOnTime.Text = "Show On Time";
            Control_ReceivingAnalytics_CheckBox_ShowOnTime.UseVisualStyleBackColor = true;
            // 
            // Control_ReceivingAnalytics_CheckBox_ShowLate
            // 
            Control_ReceivingAnalytics_CheckBox_ShowLate.AutoSize = true;
            Control_ReceivingAnalytics_CheckBox_ShowLate.Checked = true;
            Control_ReceivingAnalytics_CheckBox_ShowLate.CheckState = CheckState.Checked;
            Control_ReceivingAnalytics_CheckBox_ShowLate.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_CheckBox_ShowLate.Location = new Point(3, 28);
            Control_ReceivingAnalytics_CheckBox_ShowLate.Name = "Control_ReceivingAnalytics_CheckBox_ShowLate";
            Control_ReceivingAnalytics_CheckBox_ShowLate.Size = new Size(276, 19);
            Control_ReceivingAnalytics_CheckBox_ShowLate.TabIndex = 15;
            Control_ReceivingAnalytics_CheckBox_ShowLate.Text = "Show Late";
            Control_ReceivingAnalytics_CheckBox_ShowLate.UseVisualStyleBackColor = true;
            // 
            // Control_ReceivingAnalytics_CheckBox_ShowWithPartID
            // 
            Control_ReceivingAnalytics_CheckBox_ShowWithPartID.AutoSize = true;
            Control_ReceivingAnalytics_CheckBox_ShowWithPartID.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_CheckBox_ShowWithPartID.Location = new Point(3, 3);
            Control_ReceivingAnalytics_CheckBox_ShowWithPartID.Name = "Control_ReceivingAnalytics_CheckBox_ShowWithPartID";
            Control_ReceivingAnalytics_CheckBox_ShowWithPartID.Size = new Size(276, 19);
            Control_ReceivingAnalytics_CheckBox_ShowWithPartID.TabIndex = 18;
            Control_ReceivingAnalytics_CheckBox_ShowWithPartID.Text = "Only Part Numbers";
            Control_ReceivingAnalytics_CheckBox_ShowWithPartID.UseVisualStyleBackColor = true;
            // 
            // Control_ReceivingAnalytics_CheckBox_ShowMMC
            // 
            Control_ReceivingAnalytics_CheckBox_ShowMMC.AutoSize = true;
            Control_ReceivingAnalytics_CheckBox_ShowMMC.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_CheckBox_ShowMMC.Location = new Point(3, 28);
            Control_ReceivingAnalytics_CheckBox_ShowMMC.Name = "Control_ReceivingAnalytics_CheckBox_ShowMMC";
            Control_ReceivingAnalytics_CheckBox_ShowMMC.Size = new Size(276, 19);
            Control_ReceivingAnalytics_CheckBox_ShowMMC.TabIndex = 20;
            Control_ReceivingAnalytics_CheckBox_ShowMMC.Text = "Only Coils (MMC)";
            Control_ReceivingAnalytics_CheckBox_ShowMMC.UseVisualStyleBackColor = true;
            // 
            // Control_ReceivingAnalytics_CheckBox_ShowMMF
            // 
            Control_ReceivingAnalytics_CheckBox_ShowMMF.AutoSize = true;
            Control_ReceivingAnalytics_CheckBox_ShowMMF.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_CheckBox_ShowMMF.Location = new Point(3, 53);
            Control_ReceivingAnalytics_CheckBox_ShowMMF.Name = "Control_ReceivingAnalytics_CheckBox_ShowMMF";
            Control_ReceivingAnalytics_CheckBox_ShowMMF.Size = new Size(276, 19);
            Control_ReceivingAnalytics_CheckBox_ShowMMF.TabIndex = 21;
            Control_ReceivingAnalytics_CheckBox_ShowMMF.Text = "Only Flat Stock (MMF)";
            Control_ReceivingAnalytics_CheckBox_ShowMMF.UseVisualStyleBackColor = true;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_DateRange
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.AutoSize = true;
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.ColumnCount = 3;
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.Controls.Add(Control_ReceivingAnalytics_DateTimePicker_StartDate, 0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.Controls.Add(Control_ReceivingAnalytics_Label_DateRangeTo, 1, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.Controls.Add(Control_ReceivingAnalytics_DateTimePicker_EndDate, 2, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.Location = new Point(3, 3);
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.Name = "Control_ReceivingAnalytics_TableLayoutPanel_DateRange";
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.RowCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.Size = new Size(276, 29);
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.TabIndex = 21;
            // 
            // Control_ReceivingAnalytics_DateTimePicker_StartDate
            // 
            Control_ReceivingAnalytics_DateTimePicker_StartDate.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_DateTimePicker_StartDate.Format = DateTimePickerFormat.Short;
            Control_ReceivingAnalytics_DateTimePicker_StartDate.Location = new Point(3, 3);
            Control_ReceivingAnalytics_DateTimePicker_StartDate.Name = "Control_ReceivingAnalytics_DateTimePicker_StartDate";
            Control_ReceivingAnalytics_DateTimePicker_StartDate.Size = new Size(120, 23);
            Control_ReceivingAnalytics_DateTimePicker_StartDate.TabIndex = 1;
            // 
            // Control_ReceivingAnalytics_Label_DateRangeTo
            // 
            Control_ReceivingAnalytics_Label_DateRangeTo.AutoSize = true;
            Control_ReceivingAnalytics_Label_DateRangeTo.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Label_DateRangeTo.Location = new Point(129, 3);
            Control_ReceivingAnalytics_Label_DateRangeTo.Margin = new Padding(3);
            Control_ReceivingAnalytics_Label_DateRangeTo.Name = "Control_ReceivingAnalytics_Label_DateRangeTo";
            Control_ReceivingAnalytics_Label_DateRangeTo.Size = new Size(18, 23);
            Control_ReceivingAnalytics_Label_DateRangeTo.TabIndex = 2;
            Control_ReceivingAnalytics_Label_DateRangeTo.Text = "to";
            Control_ReceivingAnalytics_Label_DateRangeTo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_ReceivingAnalytics_DateTimePicker_EndDate
            // 
            Control_ReceivingAnalytics_DateTimePicker_EndDate.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_DateTimePicker_EndDate.Format = DateTimePickerFormat.Short;
            Control_ReceivingAnalytics_DateTimePicker_EndDate.Location = new Point(153, 3);
            Control_ReceivingAnalytics_DateTimePicker_EndDate.Name = "Control_ReceivingAnalytics_DateTimePicker_EndDate";
            Control_ReceivingAnalytics_DateTimePicker_EndDate.Size = new Size(120, 23);
            Control_ReceivingAnalytics_DateTimePicker_EndDate.TabIndex = 3;
            // 
            // Control_ReceivingAnalytics_Label_DateRange
            // 
            Control_ReceivingAnalytics_Label_DateRange.AutoSize = true;
            Control_ReceivingAnalytics_Label_DateRange.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Label_DateRange.Font = new Font("Segoe UI Emoji", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Control_ReceivingAnalytics_Label_DateRange.Location = new Point(3, 3);
            Control_ReceivingAnalytics_Label_DateRange.Margin = new Padding(3);
            Control_ReceivingAnalytics_Label_DateRange.Name = "Control_ReceivingAnalytics_Label_DateRange";
            Control_ReceivingAnalytics_Label_DateRange.Size = new Size(210, 30);
            Control_ReceivingAnalytics_Label_DateRange.TabIndex = 0;
            Control_ReceivingAnalytics_Label_DateRange.Text = "📅 Date Range";
            Control_ReceivingAnalytics_Label_DateRange.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_WeekButtons
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_WeekButtons.AutoSize = true;
            Control_ReceivingAnalytics_TableLayoutPanel_WeekButtons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_TableLayoutPanel_WeekButtons.ColumnCount = 7;
            Control_ReceivingAnalytics_TableLayoutPanel_WeekButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.9999924F));
            Control_ReceivingAnalytics_TableLayoutPanel_WeekButtons.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_WeekButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25.0000038F));
            Control_ReceivingAnalytics_TableLayoutPanel_WeekButtons.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_WeekButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25.0000038F));
            Control_ReceivingAnalytics_TableLayoutPanel_WeekButtons.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_WeekButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25.0000038F));
            Control_ReceivingAnalytics_TableLayoutPanel_WeekButtons.Controls.Add(Control_ReceivingAnalytics_Button_NextWeek, 5, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_WeekButtons.Controls.Add(Control_ReceivingAnalytics_Button_PreviousWeek, 1, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_WeekButtons.Controls.Add(Control_ReceivingAnalytics_Button_CurrentWeek, 3, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_WeekButtons.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_WeekButtons.Location = new Point(3, 38);
            Control_ReceivingAnalytics_TableLayoutPanel_WeekButtons.Name = "Control_ReceivingAnalytics_TableLayoutPanel_WeekButtons";
            Control_ReceivingAnalytics_TableLayoutPanel_WeekButtons.RowCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_WeekButtons.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_WeekButtons.Size = new Size(276, 33);
            Control_ReceivingAnalytics_TableLayoutPanel_WeekButtons.TabIndex = 26;
            // 
            // Control_ReceivingAnalytics_Button_NextWeek
            // 
            Control_ReceivingAnalytics_Button_NextWeek.AutoSize = true;
            Control_ReceivingAnalytics_Button_NextWeek.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_Button_NextWeek.FlatStyle = FlatStyle.Flat;
            Control_ReceivingAnalytics_Button_NextWeek.Location = new Point(198, 3);
            Control_ReceivingAnalytics_Button_NextWeek.Name = "Control_ReceivingAnalytics_Button_NextWeek";
            Control_ReceivingAnalytics_Button_NextWeek.Size = new Size(58, 27);
            Control_ReceivingAnalytics_Button_NextWeek.TabIndex = 14;
            Control_ReceivingAnalytics_Button_NextWeek.Text = "Next 🡲";
            Control_ReceivingAnalytics_Button_NextWeek.UseVisualStyleBackColor = false;
            // 
            // Control_ReceivingAnalytics_Button_PreviousWeek
            // 
            Control_ReceivingAnalytics_Button_PreviousWeek.AutoSize = true;
            Control_ReceivingAnalytics_Button_PreviousWeek.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_Button_PreviousWeek.FlatStyle = FlatStyle.Flat;
            Control_ReceivingAnalytics_Button_PreviousWeek.Location = new Point(18, 3);
            Control_ReceivingAnalytics_Button_PreviousWeek.Name = "Control_ReceivingAnalytics_Button_PreviousWeek";
            Control_ReceivingAnalytics_Button_PreviousWeek.Size = new Size(79, 27);
            Control_ReceivingAnalytics_Button_PreviousWeek.TabIndex = 13;
            Control_ReceivingAnalytics_Button_PreviousWeek.Text = "🡰 Previous";
            Control_ReceivingAnalytics_Button_PreviousWeek.UseVisualStyleBackColor = false;
            // 
            // Control_ReceivingAnalytics_Button_CurrentWeek
            // 
            Control_ReceivingAnalytics_Button_CurrentWeek.AutoSize = true;
            Control_ReceivingAnalytics_Button_CurrentWeek.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_Button_CurrentWeek.FlatStyle = FlatStyle.Flat;
            Control_ReceivingAnalytics_Button_CurrentWeek.Location = new Point(118, 3);
            Control_ReceivingAnalytics_Button_CurrentWeek.Name = "Control_ReceivingAnalytics_Button_CurrentWeek";
            Control_ReceivingAnalytics_Button_CurrentWeek.Size = new Size(59, 27);
            Control_ReceivingAnalytics_Button_CurrentWeek.TabIndex = 15;
            Control_ReceivingAnalytics_Button_CurrentWeek.Text = "Current";
            Control_ReceivingAnalytics_Button_CurrentWeek.UseVisualStyleBackColor = false;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_Main
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_Main.ColumnCount = 2;
            Control_ReceivingAnalytics_TableLayoutPanel_Main.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_ReceivingAnalytics_TableLayoutPanel_Main.Controls.Add(Control_ReceivingAnalytics_Panel_Results, 1, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_Main.Controls.Add(tableLayoutPanel1, 0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_Main.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_Main.Location = new Point(0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_Main.Name = "Control_ReceivingAnalytics_TableLayoutPanel_Main";
            Control_ReceivingAnalytics_TableLayoutPanel_Main.RowCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_Main.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_Main.Size = new Size(1500, 875);
            Control_ReceivingAnalytics_TableLayoutPanel_Main.TabIndex = 0;
            // 
            // Control_ReceivingAnalytics_Panel_Results
            // 
            Control_ReceivingAnalytics_Panel_Results.AutoSize = true;
            Control_ReceivingAnalytics_Panel_Results.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_Panel_Results.BorderStyle = BorderStyle.FixedSingle;
            Control_ReceivingAnalytics_Panel_Results.Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_Results);
            Control_ReceivingAnalytics_Panel_Results.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Panel_Results.Location = new Point(313, 3);
            Control_ReceivingAnalytics_Panel_Results.Name = "Control_ReceivingAnalytics_Panel_Results";
            Control_ReceivingAnalytics_Panel_Results.Size = new Size(1184, 869);
            Control_ReceivingAnalytics_Panel_Results.TabIndex = 1;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_Results
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_Results.AutoSize = true;
            Control_ReceivingAnalytics_TableLayoutPanel_Results.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_TableLayoutPanel_Results.ColumnCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_Results.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_Results.Controls.Add(Control_ReceivingAnalytics_Panel_DataGridView, 0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_Results.Controls.Add(Control_ReceivingAnalytics_Panel_CheckBoxes_Legend, 0, 1);
            Control_ReceivingAnalytics_TableLayoutPanel_Results.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_Results.Location = new Point(0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_Results.Name = "Control_ReceivingAnalytics_TableLayoutPanel_Results";
            Control_ReceivingAnalytics_TableLayoutPanel_Results.RowCount = 2;
            Control_ReceivingAnalytics_TableLayoutPanel_Results.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_ReceivingAnalytics_TableLayoutPanel_Results.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_Results.Size = new Size(1182, 867);
            Control_ReceivingAnalytics_TableLayoutPanel_Results.TabIndex = 31;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(Control_ReceivingAnalytics_Panel_Buttons, 0, 0);
            tableLayoutPanel1.Controls.Add(Control_ReceivingAnalytics_Panel_SideBarMain, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(304, 869);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // Control_ReceivingAnalytics_Panel_Buttons
            // 
            Control_ReceivingAnalytics_Panel_Buttons.AutoSize = true;
            Control_ReceivingAnalytics_Panel_Buttons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_Panel_Buttons.BorderStyle = BorderStyle.FixedSingle;
            Control_ReceivingAnalytics_Panel_Buttons.Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_Buttons);
            Control_ReceivingAnalytics_Panel_Buttons.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Panel_Buttons.Location = new Point(3, 3);
            Control_ReceivingAnalytics_Panel_Buttons.Name = "Control_ReceivingAnalytics_Panel_Buttons";
            Control_ReceivingAnalytics_Panel_Buttons.Size = new Size(298, 48);
            Control_ReceivingAnalytics_Panel_Buttons.TabIndex = 29;
            // 
            // Control_ReceivingAnalytics_Panel_SideBarMain
            // 
            Control_ReceivingAnalytics_Panel_SideBarMain.AutoSize = true;
            Control_ReceivingAnalytics_Panel_SideBarMain.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_Panel_SideBarMain.BorderStyle = BorderStyle.FixedSingle;
            Control_ReceivingAnalytics_Panel_SideBarMain.Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_SideBarMain);
            Control_ReceivingAnalytics_Panel_SideBarMain.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Panel_SideBarMain.Location = new Point(3, 57);
            Control_ReceivingAnalytics_Panel_SideBarMain.Name = "Control_ReceivingAnalytics_Panel_SideBarMain";
            Control_ReceivingAnalytics_Panel_SideBarMain.Size = new Size(298, 809);
            Control_ReceivingAnalytics_Panel_SideBarMain.TabIndex = 30;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_SideBarMain
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarMain.AutoSize = true;
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarMain.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarMain.ColumnCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarMain.Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_DateRangeMain, 0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarMain.Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_FiltersMain, 0, 1);
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarMain.Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_POStatesMain, 0, 2);
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarMain.Controls.Add(Control_ReceivingAnalytics_Panel_OutsideScopeMain, 0, 5);
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarMain.Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesMain, 0, 3);
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarMain.Controls.Add(Control_ReceivingAnalytics_Panel_ReceivingScopeMain, 0, 4);
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarMain.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarMain.Location = new Point(0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarMain.Name = "Control_ReceivingAnalytics_TableLayoutPanel_SideBarMain";
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarMain.RowCount = 8;
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarMain.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarMain.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarMain.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarMain.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarMain.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarMain.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarMain.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarMain.Size = new Size(296, 807);
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarMain.TabIndex = 31;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_DateRangeMain
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeMain.AutoSize = true;
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeMain.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeMain.ColumnCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeMain.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeMain.Controls.Add(Control_ReceivingAnalytics_Panel_DateRangeHeader, 0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeMain.Controls.Add(Control_ReceivingAnalytics_Panel_DateRangeContents, 0, 1);
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeMain.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeMain.Location = new Point(3, 3);
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeMain.Name = "Control_ReceivingAnalytics_TableLayoutPanel_DateRangeMain";
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeMain.RowCount = 2;
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeMain.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeMain.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeMain.Size = new Size(290, 120);
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeMain.TabIndex = 19;
            // 
            // Control_ReceivingAnalytics_Panel_DateRangeHeader
            // 
            Control_ReceivingAnalytics_Panel_DateRangeHeader.AutoSize = true;
            Control_ReceivingAnalytics_Panel_DateRangeHeader.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_Panel_DateRangeHeader.BorderStyle = BorderStyle.FixedSingle;
            Control_ReceivingAnalytics_Panel_DateRangeHeader.Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_DateRangeHeader);
            Control_ReceivingAnalytics_Panel_DateRangeHeader.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Panel_DateRangeHeader.Location = new Point(3, 3);
            Control_ReceivingAnalytics_Panel_DateRangeHeader.Margin = new Padding(3, 3, 3, 0);
            Control_ReceivingAnalytics_Panel_DateRangeHeader.Name = "Control_ReceivingAnalytics_Panel_DateRangeHeader";
            Control_ReceivingAnalytics_Panel_DateRangeHeader.Size = new Size(284, 38);
            Control_ReceivingAnalytics_Panel_DateRangeHeader.TabIndex = 22;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_DateRangeHeader
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeHeader.AutoSize = true;
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeHeader.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeHeader.ColumnCount = 2;
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeHeader.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeHeader.Controls.Add(Control_ReceivingAnalytics_Button_DateRangeHeader, 1, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeHeader.Controls.Add(Control_ReceivingAnalytics_Label_DateRange, 0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeHeader.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeHeader.Location = new Point(0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeHeader.Name = "Control_ReceivingAnalytics_TableLayoutPanel_DateRangeHeader";
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeHeader.RowCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeHeader.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeHeader.Size = new Size(282, 36);
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeHeader.TabIndex = 17;
            // 
            // Control_ReceivingAnalytics_Button_DateRangeHeader
            // 
            Control_ReceivingAnalytics_Button_DateRangeHeader.AutoSize = true;
            Control_ReceivingAnalytics_Button_DateRangeHeader.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_Button_DateRangeHeader.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Button_DateRangeHeader.FlatStyle = FlatStyle.Flat;
            Control_ReceivingAnalytics_Button_DateRangeHeader.Location = new Point(219, 3);
            Control_ReceivingAnalytics_Button_DateRangeHeader.MaximumSize = new Size(60, 30);
            Control_ReceivingAnalytics_Button_DateRangeHeader.MinimumSize = new Size(60, 30);
            Control_ReceivingAnalytics_Button_DateRangeHeader.Name = "Control_ReceivingAnalytics_Button_DateRangeHeader";
            Control_ReceivingAnalytics_Button_DateRangeHeader.Size = new Size(60, 30);
            Control_ReceivingAnalytics_Button_DateRangeHeader.TabIndex = 12;
            Control_ReceivingAnalytics_Button_DateRangeHeader.Text = "🡱";
            Control_ReceivingAnalytics_Button_DateRangeHeader.UseVisualStyleBackColor = false;
            // 
            // Control_ReceivingAnalytics_Panel_DateRangeContents
            // 
            Control_ReceivingAnalytics_Panel_DateRangeContents.AutoSize = true;
            Control_ReceivingAnalytics_Panel_DateRangeContents.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_Panel_DateRangeContents.BorderStyle = BorderStyle.FixedSingle;
            Control_ReceivingAnalytics_Panel_DateRangeContents.Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_DateRangeContents);
            Control_ReceivingAnalytics_Panel_DateRangeContents.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Panel_DateRangeContents.Location = new Point(3, 41);
            Control_ReceivingAnalytics_Panel_DateRangeContents.Margin = new Padding(3, 0, 3, 3);
            Control_ReceivingAnalytics_Panel_DateRangeContents.Name = "Control_ReceivingAnalytics_Panel_DateRangeContents";
            Control_ReceivingAnalytics_Panel_DateRangeContents.Size = new Size(284, 76);
            Control_ReceivingAnalytics_Panel_DateRangeContents.TabIndex = 23;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_DateRangeContents
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeContents.AutoSize = true;
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeContents.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeContents.ColumnCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeContents.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeContents.Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_DateRange, 0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeContents.Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_WeekButtons, 0, 1);
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeContents.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeContents.Location = new Point(0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeContents.Name = "Control_ReceivingAnalytics_TableLayoutPanel_DateRangeContents";
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeContents.RowCount = 2;
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeContents.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeContents.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeContents.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeContents.Size = new Size(282, 74);
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeContents.TabIndex = 1;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_FiltersMain
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersMain.AutoSize = true;
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersMain.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersMain.ColumnCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersMain.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersMain.Controls.Add(Control_ReceivingAnalytics_Panel_FiltersContents, 0, 1);
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersMain.Controls.Add(Control_ReceivingAnalytics_Panel_FiltersHeader, 0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersMain.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersMain.Location = new Point(3, 129);
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersMain.Name = "Control_ReceivingAnalytics_TableLayoutPanel_FiltersMain";
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersMain.RowCount = 2;
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersMain.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersMain.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersMain.Size = new Size(290, 191);
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersMain.TabIndex = 23;
            // 
            // Control_ReceivingAnalytics_Panel_FiltersContents
            // 
            Control_ReceivingAnalytics_Panel_FiltersContents.AutoSize = true;
            Control_ReceivingAnalytics_Panel_FiltersContents.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_Panel_FiltersContents.BorderStyle = BorderStyle.FixedSingle;
            Control_ReceivingAnalytics_Panel_FiltersContents.Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_FiltersContents);
            Control_ReceivingAnalytics_Panel_FiltersContents.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Panel_FiltersContents.Location = new Point(3, 41);
            Control_ReceivingAnalytics_Panel_FiltersContents.Margin = new Padding(3, 0, 3, 3);
            Control_ReceivingAnalytics_Panel_FiltersContents.Name = "Control_ReceivingAnalytics_Panel_FiltersContents";
            Control_ReceivingAnalytics_Panel_FiltersContents.Size = new Size(284, 147);
            Control_ReceivingAnalytics_Panel_FiltersContents.TabIndex = 25;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_FiltersContents
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersContents.AutoSize = true;
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersContents.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersContents.ColumnCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersContents.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersContents.Controls.Add(Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier, 0, 3);
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersContents.Controls.Add(Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Supplier, 0, 2);
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersContents.Controls.Add(Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PONumber, 0, 1);
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersContents.Controls.Add(Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_DateType, 0, 4);
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersContents.Controls.Add(Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber, 0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersContents.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersContents.Location = new Point(0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersContents.Name = "Control_ReceivingAnalytics_TableLayoutPanel_FiltersContents";
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersContents.RowCount = 5;
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersContents.RowStyles.Add(new RowStyle(SizeType.Percent, 20.0000019F));
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersContents.RowStyles.Add(new RowStyle(SizeType.Percent, 19.9999981F));
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersContents.RowStyles.Add(new RowStyle(SizeType.Percent, 19.9999981F));
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersContents.RowStyles.Add(new RowStyle(SizeType.Percent, 19.9999981F));
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersContents.RowStyles.Add(new RowStyle(SizeType.Percent, 19.9999981F));
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersContents.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersContents.Size = new Size(282, 145);
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersContents.TabIndex = 2;
            // 
            // Control_ReceivingAnalytics_Panel_FiltersHeader
            // 
            Control_ReceivingAnalytics_Panel_FiltersHeader.AutoSize = true;
            Control_ReceivingAnalytics_Panel_FiltersHeader.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_Panel_FiltersHeader.BorderStyle = BorderStyle.FixedSingle;
            Control_ReceivingAnalytics_Panel_FiltersHeader.Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_FiltersHeader);
            Control_ReceivingAnalytics_Panel_FiltersHeader.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Panel_FiltersHeader.Location = new Point(3, 3);
            Control_ReceivingAnalytics_Panel_FiltersHeader.Margin = new Padding(3, 3, 3, 0);
            Control_ReceivingAnalytics_Panel_FiltersHeader.Name = "Control_ReceivingAnalytics_Panel_FiltersHeader";
            Control_ReceivingAnalytics_Panel_FiltersHeader.Size = new Size(284, 38);
            Control_ReceivingAnalytics_Panel_FiltersHeader.TabIndex = 24;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_FiltersHeader
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersHeader.AutoSize = true;
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersHeader.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersHeader.ColumnCount = 2;
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersHeader.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersHeader.Controls.Add(Control_ReceivingAnalytics_Button_FiltersHeader, 1, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersHeader.Controls.Add(Control_ReceivingAnalytics_Label_SideBarFilters, 0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersHeader.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersHeader.Location = new Point(0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersHeader.Name = "Control_ReceivingAnalytics_TableLayoutPanel_FiltersHeader";
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersHeader.RowCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersHeader.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersHeader.Size = new Size(282, 36);
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersHeader.TabIndex = 22;
            // 
            // Control_ReceivingAnalytics_Button_FiltersHeader
            // 
            Control_ReceivingAnalytics_Button_FiltersHeader.AutoSize = true;
            Control_ReceivingAnalytics_Button_FiltersHeader.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_Button_FiltersHeader.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Button_FiltersHeader.FlatStyle = FlatStyle.Flat;
            Control_ReceivingAnalytics_Button_FiltersHeader.Location = new Point(219, 3);
            Control_ReceivingAnalytics_Button_FiltersHeader.MaximumSize = new Size(60, 30);
            Control_ReceivingAnalytics_Button_FiltersHeader.MinimumSize = new Size(60, 30);
            Control_ReceivingAnalytics_Button_FiltersHeader.Name = "Control_ReceivingAnalytics_Button_FiltersHeader";
            Control_ReceivingAnalytics_Button_FiltersHeader.Size = new Size(60, 30);
            Control_ReceivingAnalytics_Button_FiltersHeader.TabIndex = 12;
            Control_ReceivingAnalytics_Button_FiltersHeader.Text = "🡱";
            Control_ReceivingAnalytics_Button_FiltersHeader.UseVisualStyleBackColor = false;
            // 
            // Control_ReceivingAnalytics_Label_SideBarFilters
            // 
            Control_ReceivingAnalytics_Label_SideBarFilters.AutoSize = true;
            Control_ReceivingAnalytics_Label_SideBarFilters.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Label_SideBarFilters.Font = new Font("Segoe UI Emoji", 9.75F);
            Control_ReceivingAnalytics_Label_SideBarFilters.Location = new Point(3, 3);
            Control_ReceivingAnalytics_Label_SideBarFilters.Margin = new Padding(3);
            Control_ReceivingAnalytics_Label_SideBarFilters.Name = "Control_ReceivingAnalytics_Label_SideBarFilters";
            Control_ReceivingAnalytics_Label_SideBarFilters.Size = new Size(210, 30);
            Control_ReceivingAnalytics_Label_SideBarFilters.TabIndex = 15;
            Control_ReceivingAnalytics_Label_SideBarFilters.Text = "📋 Filters";
            Control_ReceivingAnalytics_Label_SideBarFilters.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_POStatesMain
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesMain.AutoSize = true;
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesMain.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesMain.ColumnCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesMain.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesMain.Controls.Add(Control_ReceivingAnalytics_Panel_POStatesHeader, 0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesMain.Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_POStatesContents, 0, 1);
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesMain.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesMain.Location = new Point(3, 326);
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesMain.Name = "Control_ReceivingAnalytics_TableLayoutPanel_POStatesMain";
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesMain.RowCount = 2;
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesMain.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesMain.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesMain.Size = new Size(290, 96);
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesMain.TabIndex = 25;
            // 
            // Control_ReceivingAnalytics_Panel_POStatesHeader
            // 
            Control_ReceivingAnalytics_Panel_POStatesHeader.AutoSize = true;
            Control_ReceivingAnalytics_Panel_POStatesHeader.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_Panel_POStatesHeader.BorderStyle = BorderStyle.FixedSingle;
            Control_ReceivingAnalytics_Panel_POStatesHeader.Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_POStatesHeader);
            Control_ReceivingAnalytics_Panel_POStatesHeader.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Panel_POStatesHeader.Location = new Point(3, 3);
            Control_ReceivingAnalytics_Panel_POStatesHeader.Margin = new Padding(3, 3, 3, 0);
            Control_ReceivingAnalytics_Panel_POStatesHeader.Name = "Control_ReceivingAnalytics_Panel_POStatesHeader";
            Control_ReceivingAnalytics_Panel_POStatesHeader.Size = new Size(284, 38);
            Control_ReceivingAnalytics_Panel_POStatesHeader.TabIndex = 27;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_POStatesHeader
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesHeader.AutoSize = true;
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesHeader.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesHeader.ColumnCount = 2;
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesHeader.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesHeader.Controls.Add(Control_ReceivingAnalytics_Button_POStatesHeader, 1, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesHeader.Controls.Add(Control_ReceivingAnalytics_Label_SideBarPOStates, 0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesHeader.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesHeader.Location = new Point(0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesHeader.Name = "Control_ReceivingAnalytics_TableLayoutPanel_POStatesHeader";
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesHeader.RowCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesHeader.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesHeader.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesHeader.Size = new Size(282, 36);
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesHeader.TabIndex = 24;
            // 
            // Control_ReceivingAnalytics_Button_POStatesHeader
            // 
            Control_ReceivingAnalytics_Button_POStatesHeader.AutoSize = true;
            Control_ReceivingAnalytics_Button_POStatesHeader.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_Button_POStatesHeader.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Button_POStatesHeader.FlatStyle = FlatStyle.Flat;
            Control_ReceivingAnalytics_Button_POStatesHeader.Location = new Point(219, 3);
            Control_ReceivingAnalytics_Button_POStatesHeader.MaximumSize = new Size(60, 30);
            Control_ReceivingAnalytics_Button_POStatesHeader.MinimumSize = new Size(60, 30);
            Control_ReceivingAnalytics_Button_POStatesHeader.Name = "Control_ReceivingAnalytics_Button_POStatesHeader";
            Control_ReceivingAnalytics_Button_POStatesHeader.Size = new Size(60, 30);
            Control_ReceivingAnalytics_Button_POStatesHeader.TabIndex = 12;
            Control_ReceivingAnalytics_Button_POStatesHeader.Text = "🡱";
            Control_ReceivingAnalytics_Button_POStatesHeader.UseVisualStyleBackColor = false;
            // 
            // Control_ReceivingAnalytics_Label_SideBarPOStates
            // 
            Control_ReceivingAnalytics_Label_SideBarPOStates.AutoSize = true;
            Control_ReceivingAnalytics_Label_SideBarPOStates.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Label_SideBarPOStates.Font = new Font("Segoe UI Emoji", 9.75F);
            Control_ReceivingAnalytics_Label_SideBarPOStates.Location = new Point(3, 3);
            Control_ReceivingAnalytics_Label_SideBarPOStates.Margin = new Padding(3);
            Control_ReceivingAnalytics_Label_SideBarPOStates.Name = "Control_ReceivingAnalytics_Label_SideBarPOStates";
            Control_ReceivingAnalytics_Label_SideBarPOStates.Size = new Size(210, 30);
            Control_ReceivingAnalytics_Label_SideBarPOStates.TabIndex = 16;
            Control_ReceivingAnalytics_Label_SideBarPOStates.Text = "🔘 PO States";
            Control_ReceivingAnalytics_Label_SideBarPOStates.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_POStatesContents
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesContents.AutoSize = true;
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesContents.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesContents.BorderStyle = BorderStyle.FixedSingle;
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesContents.Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_SideBarPOStates);
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesContents.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesContents.Location = new Point(3, 41);
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesContents.Margin = new Padding(3, 0, 3, 3);
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesContents.Name = "Control_ReceivingAnalytics_TableLayoutPanel_POStatesContents";
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesContents.Size = new Size(284, 52);
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesContents.TabIndex = 26;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_SideBarPOStates
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarPOStates.AutoSize = true;
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarPOStates.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarPOStates.ColumnCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarPOStates.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarPOStates.Controls.Add(Control_ReceivingAnalytics_CheckBox_ShowOnTime, 0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarPOStates.Controls.Add(Control_ReceivingAnalytics_CheckBox_ShowLate, 0, 1);
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarPOStates.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarPOStates.Location = new Point(0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarPOStates.Name = "Control_ReceivingAnalytics_TableLayoutPanel_SideBarPOStates";
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarPOStates.RowCount = 2;
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarPOStates.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarPOStates.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarPOStates.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarPOStates.Size = new Size(282, 50);
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarPOStates.TabIndex = 3;
            // 
            // Control_ReceivingAnalytics_Panel_OutsideScopeMain
            // 
            Control_ReceivingAnalytics_Panel_OutsideScopeMain.AutoSize = true;
            Control_ReceivingAnalytics_Panel_OutsideScopeMain.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_Panel_OutsideScopeMain.ColumnCount = 1;
            Control_ReceivingAnalytics_Panel_OutsideScopeMain.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_Panel_OutsideScopeMain.Controls.Add(Control_ReceivingAnalytics_Panel_OutsideScopeHeader, 0, 0);
            Control_ReceivingAnalytics_Panel_OutsideScopeMain.Controls.Add(Control_ReceivingAnalytics_Panel_OutsideScopeContents, 0, 1);
            Control_ReceivingAnalytics_Panel_OutsideScopeMain.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Panel_OutsideScopeMain.Location = new Point(3, 682);
            Control_ReceivingAnalytics_Panel_OutsideScopeMain.Name = "Control_ReceivingAnalytics_Panel_OutsideScopeMain";
            Control_ReceivingAnalytics_Panel_OutsideScopeMain.RowCount = 2;
            Control_ReceivingAnalytics_Panel_OutsideScopeMain.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_Panel_OutsideScopeMain.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_Panel_OutsideScopeMain.Size = new Size(290, 121);
            Control_ReceivingAnalytics_Panel_OutsideScopeMain.TabIndex = 28;
            // 
            // Control_ReceivingAnalytics_Panel_OutsideScopeHeader
            // 
            Control_ReceivingAnalytics_Panel_OutsideScopeHeader.AutoSize = true;
            Control_ReceivingAnalytics_Panel_OutsideScopeHeader.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_Panel_OutsideScopeHeader.BorderStyle = BorderStyle.FixedSingle;
            Control_ReceivingAnalytics_Panel_OutsideScopeHeader.Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeHeader);
            Control_ReceivingAnalytics_Panel_OutsideScopeHeader.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Panel_OutsideScopeHeader.Location = new Point(3, 3);
            Control_ReceivingAnalytics_Panel_OutsideScopeHeader.Margin = new Padding(3, 3, 3, 0);
            Control_ReceivingAnalytics_Panel_OutsideScopeHeader.Name = "Control_ReceivingAnalytics_Panel_OutsideScopeHeader";
            Control_ReceivingAnalytics_Panel_OutsideScopeHeader.Size = new Size(284, 38);
            Control_ReceivingAnalytics_Panel_OutsideScopeHeader.TabIndex = 27;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeHeader
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeHeader.AutoSize = true;
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeHeader.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeHeader.ColumnCount = 2;
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeHeader.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeHeader.Controls.Add(Control_ReceivingAnalytics_Button_OutsideScopeHeader, 1, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeHeader.Controls.Add(Control_ReceivingAnalytics_Label_OutsideScopeHeader, 0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeHeader.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeHeader.Location = new Point(0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeHeader.Name = "Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeHeader";
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeHeader.RowCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeHeader.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeHeader.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeHeader.Size = new Size(282, 36);
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeHeader.TabIndex = 24;
            // 
            // Control_ReceivingAnalytics_Button_OutsideScopeHeader
            // 
            Control_ReceivingAnalytics_Button_OutsideScopeHeader.AutoSize = true;
            Control_ReceivingAnalytics_Button_OutsideScopeHeader.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_Button_OutsideScopeHeader.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Button_OutsideScopeHeader.FlatStyle = FlatStyle.Flat;
            Control_ReceivingAnalytics_Button_OutsideScopeHeader.Location = new Point(219, 3);
            Control_ReceivingAnalytics_Button_OutsideScopeHeader.MaximumSize = new Size(60, 30);
            Control_ReceivingAnalytics_Button_OutsideScopeHeader.MinimumSize = new Size(60, 30);
            Control_ReceivingAnalytics_Button_OutsideScopeHeader.Name = "Control_ReceivingAnalytics_Button_OutsideScopeHeader";
            Control_ReceivingAnalytics_Button_OutsideScopeHeader.Size = new Size(60, 30);
            Control_ReceivingAnalytics_Button_OutsideScopeHeader.TabIndex = 12;
            Control_ReceivingAnalytics_Button_OutsideScopeHeader.Text = "🡱";
            Control_ReceivingAnalytics_Button_OutsideScopeHeader.UseVisualStyleBackColor = false;
            // 
            // Control_ReceivingAnalytics_Label_OutsideScopeHeader
            // 
            Control_ReceivingAnalytics_Label_OutsideScopeHeader.AutoSize = true;
            Control_ReceivingAnalytics_Label_OutsideScopeHeader.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Label_OutsideScopeHeader.Font = new Font("Segoe UI Emoji", 9.75F);
            Control_ReceivingAnalytics_Label_OutsideScopeHeader.Location = new Point(3, 3);
            Control_ReceivingAnalytics_Label_OutsideScopeHeader.Margin = new Padding(3);
            Control_ReceivingAnalytics_Label_OutsideScopeHeader.Name = "Control_ReceivingAnalytics_Label_OutsideScopeHeader";
            Control_ReceivingAnalytics_Label_OutsideScopeHeader.Size = new Size(210, 30);
            Control_ReceivingAnalytics_Label_OutsideScopeHeader.TabIndex = 16;
            Control_ReceivingAnalytics_Label_OutsideScopeHeader.Text = "🔘 Outside Scope";
            Control_ReceivingAnalytics_Label_OutsideScopeHeader.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_ReceivingAnalytics_Panel_OutsideScopeContents
            // 
            Control_ReceivingAnalytics_Panel_OutsideScopeContents.AutoSize = true;
            Control_ReceivingAnalytics_Panel_OutsideScopeContents.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_Panel_OutsideScopeContents.BorderStyle = BorderStyle.FixedSingle;
            Control_ReceivingAnalytics_Panel_OutsideScopeContents.Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeContents);
            Control_ReceivingAnalytics_Panel_OutsideScopeContents.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Panel_OutsideScopeContents.Location = new Point(3, 41);
            Control_ReceivingAnalytics_Panel_OutsideScopeContents.Margin = new Padding(3, 0, 3, 3);
            Control_ReceivingAnalytics_Panel_OutsideScopeContents.Name = "Control_ReceivingAnalytics_Panel_OutsideScopeContents";
            Control_ReceivingAnalytics_Panel_OutsideScopeContents.Size = new Size(284, 77);
            Control_ReceivingAnalytics_Panel_OutsideScopeContents.TabIndex = 26;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeContents
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeContents.AutoSize = true;
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeContents.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeContents.ColumnCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeContents.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeContents.Controls.Add(Control_ReceivingAnalytics_CheckBox_ShowOutsideService, 0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeContents.Controls.Add(Control_ReceivingAnalytics_CheckBox_ShowConsignment, 0, 1);
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeContents.Controls.Add(Control_ReceivingAnalytics_CheckBox_ShowInternal, 0, 2);
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeContents.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeContents.Location = new Point(0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeContents.Name = "Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeContents";
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeContents.RowCount = 3;
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeContents.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeContents.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeContents.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeContents.Size = new Size(282, 75);
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeContents.TabIndex = 3;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesMain
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesMain.AutoSize = true;
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesMain.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesMain.ColumnCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesMain.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesMain.Controls.Add(Control_ReceivingAnalytics_Panel_DeliveryStatusHeader, 0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesMain.Controls.Add(Control_ReceivingAnalytics_Panel_DeliveryStatesContents, 0, 1);
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesMain.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesMain.Location = new Point(3, 428);
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesMain.Name = "Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesMain";
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesMain.RowCount = 2;
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesMain.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesMain.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesMain.Size = new Size(290, 121);
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesMain.TabIndex = 26;
            // 
            // Control_ReceivingAnalytics_Panel_DeliveryStatusHeader
            // 
            Control_ReceivingAnalytics_Panel_DeliveryStatusHeader.AutoSize = true;
            Control_ReceivingAnalytics_Panel_DeliveryStatusHeader.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_Panel_DeliveryStatusHeader.BorderStyle = BorderStyle.FixedSingle;
            Control_ReceivingAnalytics_Panel_DeliveryStatusHeader.Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesHeader);
            Control_ReceivingAnalytics_Panel_DeliveryStatusHeader.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Panel_DeliveryStatusHeader.Location = new Point(3, 3);
            Control_ReceivingAnalytics_Panel_DeliveryStatusHeader.Margin = new Padding(3, 3, 3, 0);
            Control_ReceivingAnalytics_Panel_DeliveryStatusHeader.Name = "Control_ReceivingAnalytics_Panel_DeliveryStatusHeader";
            Control_ReceivingAnalytics_Panel_DeliveryStatusHeader.Size = new Size(284, 38);
            Control_ReceivingAnalytics_Panel_DeliveryStatusHeader.TabIndex = 27;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesHeader
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesHeader.AutoSize = true;
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesHeader.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesHeader.ColumnCount = 2;
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesHeader.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesHeader.Controls.Add(Control_ReceivingAnalytics_Button_DeliveryStatesHeader, 1, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesHeader.Controls.Add(Control_ReceivingAnalytics_Label_DeliveryStatesHeader, 0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesHeader.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesHeader.Location = new Point(0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesHeader.Name = "Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesHeader";
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesHeader.RowCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesHeader.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesHeader.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesHeader.Size = new Size(282, 36);
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesHeader.TabIndex = 24;
            // 
            // Control_ReceivingAnalytics_Button_DeliveryStatesHeader
            // 
            Control_ReceivingAnalytics_Button_DeliveryStatesHeader.AutoSize = true;
            Control_ReceivingAnalytics_Button_DeliveryStatesHeader.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_Button_DeliveryStatesHeader.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Button_DeliveryStatesHeader.FlatStyle = FlatStyle.Flat;
            Control_ReceivingAnalytics_Button_DeliveryStatesHeader.Location = new Point(219, 3);
            Control_ReceivingAnalytics_Button_DeliveryStatesHeader.MaximumSize = new Size(60, 30);
            Control_ReceivingAnalytics_Button_DeliveryStatesHeader.MinimumSize = new Size(60, 30);
            Control_ReceivingAnalytics_Button_DeliveryStatesHeader.Name = "Control_ReceivingAnalytics_Button_DeliveryStatesHeader";
            Control_ReceivingAnalytics_Button_DeliveryStatesHeader.Size = new Size(60, 30);
            Control_ReceivingAnalytics_Button_DeliveryStatesHeader.TabIndex = 12;
            Control_ReceivingAnalytics_Button_DeliveryStatesHeader.Text = "🡱";
            Control_ReceivingAnalytics_Button_DeliveryStatesHeader.UseVisualStyleBackColor = false;
            // 
            // Control_ReceivingAnalytics_Label_DeliveryStatesHeader
            // 
            Control_ReceivingAnalytics_Label_DeliveryStatesHeader.AutoSize = true;
            Control_ReceivingAnalytics_Label_DeliveryStatesHeader.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Label_DeliveryStatesHeader.Font = new Font("Segoe UI Emoji", 9.75F);
            Control_ReceivingAnalytics_Label_DeliveryStatesHeader.Location = new Point(3, 3);
            Control_ReceivingAnalytics_Label_DeliveryStatesHeader.Margin = new Padding(3);
            Control_ReceivingAnalytics_Label_DeliveryStatesHeader.Name = "Control_ReceivingAnalytics_Label_DeliveryStatesHeader";
            Control_ReceivingAnalytics_Label_DeliveryStatesHeader.Size = new Size(210, 30);
            Control_ReceivingAnalytics_Label_DeliveryStatesHeader.TabIndex = 16;
            Control_ReceivingAnalytics_Label_DeliveryStatesHeader.Text = "🔘 Delivery States";
            Control_ReceivingAnalytics_Label_DeliveryStatesHeader.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_ReceivingAnalytics_Panel_DeliveryStatesContents
            // 
            Control_ReceivingAnalytics_Panel_DeliveryStatesContents.AutoSize = true;
            Control_ReceivingAnalytics_Panel_DeliveryStatesContents.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_Panel_DeliveryStatesContents.BorderStyle = BorderStyle.FixedSingle;
            Control_ReceivingAnalytics_Panel_DeliveryStatesContents.Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesContents);
            Control_ReceivingAnalytics_Panel_DeliveryStatesContents.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Panel_DeliveryStatesContents.Location = new Point(3, 41);
            Control_ReceivingAnalytics_Panel_DeliveryStatesContents.Margin = new Padding(3, 0, 3, 3);
            Control_ReceivingAnalytics_Panel_DeliveryStatesContents.Name = "Control_ReceivingAnalytics_Panel_DeliveryStatesContents";
            Control_ReceivingAnalytics_Panel_DeliveryStatesContents.Size = new Size(284, 77);
            Control_ReceivingAnalytics_Panel_DeliveryStatesContents.TabIndex = 26;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesContents
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesContents.AutoSize = true;
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesContents.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesContents.ColumnCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesContents.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesContents.Controls.Add(Control_ReceivingAnalytics_CheckBox_ShowOpen, 0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesContents.Controls.Add(Control_ReceivingAnalytics_CheckBox_ShowClosed, 0, 2);
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesContents.Controls.Add(Control_ReceivingAnalytics_CheckBox_ShowPartial, 0, 1);
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesContents.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesContents.Location = new Point(0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesContents.Name = "Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesContents";
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesContents.RowCount = 3;
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesContents.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesContents.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesContents.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesContents.Size = new Size(282, 75);
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesContents.TabIndex = 3;
            // 
            // Control_ReceivingAnalytics_Panel_ReceivingScopeMain
            // 
            Control_ReceivingAnalytics_Panel_ReceivingScopeMain.AutoSize = true;
            Control_ReceivingAnalytics_Panel_ReceivingScopeMain.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_Panel_ReceivingScopeMain.ColumnCount = 1;
            Control_ReceivingAnalytics_Panel_ReceivingScopeMain.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_Panel_ReceivingScopeMain.Controls.Add(Control_ReceivingAnalytics_Panel_ReceivingScopeHeader, 0, 0);
            Control_ReceivingAnalytics_Panel_ReceivingScopeMain.Controls.Add(Control_ReceivingAnalytics_Panel_ReceivingScopeContents, 0, 1);
            Control_ReceivingAnalytics_Panel_ReceivingScopeMain.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Panel_ReceivingScopeMain.Location = new Point(3, 555);
            Control_ReceivingAnalytics_Panel_ReceivingScopeMain.Name = "Control_ReceivingAnalytics_Panel_ReceivingScopeMain";
            Control_ReceivingAnalytics_Panel_ReceivingScopeMain.RowCount = 2;
            Control_ReceivingAnalytics_Panel_ReceivingScopeMain.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_Panel_ReceivingScopeMain.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_Panel_ReceivingScopeMain.Size = new Size(290, 121);
            Control_ReceivingAnalytics_Panel_ReceivingScopeMain.TabIndex = 27;
            // 
            // Control_ReceivingAnalytics_Panel_ReceivingScopeHeader
            // 
            Control_ReceivingAnalytics_Panel_ReceivingScopeHeader.AutoSize = true;
            Control_ReceivingAnalytics_Panel_ReceivingScopeHeader.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_Panel_ReceivingScopeHeader.BorderStyle = BorderStyle.FixedSingle;
            Control_ReceivingAnalytics_Panel_ReceivingScopeHeader.Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeHeader);
            Control_ReceivingAnalytics_Panel_ReceivingScopeHeader.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Panel_ReceivingScopeHeader.Location = new Point(3, 3);
            Control_ReceivingAnalytics_Panel_ReceivingScopeHeader.Margin = new Padding(3, 3, 3, 0);
            Control_ReceivingAnalytics_Panel_ReceivingScopeHeader.Name = "Control_ReceivingAnalytics_Panel_ReceivingScopeHeader";
            Control_ReceivingAnalytics_Panel_ReceivingScopeHeader.Size = new Size(284, 38);
            Control_ReceivingAnalytics_Panel_ReceivingScopeHeader.TabIndex = 27;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeHeader
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeHeader.AutoSize = true;
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeHeader.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeHeader.ColumnCount = 2;
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeHeader.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeHeader.Controls.Add(Control_ReceivingAnalytics_Button_ReceivingScopeHeader, 1, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeHeader.Controls.Add(Control_ReceivingAnalytics_Label_ReceivingScopeHeader, 0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeHeader.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeHeader.Location = new Point(0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeHeader.Name = "Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeHeader";
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeHeader.RowCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeHeader.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeHeader.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeHeader.Size = new Size(282, 36);
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeHeader.TabIndex = 24;
            // 
            // Control_ReceivingAnalytics_Button_ReceivingScopeHeader
            // 
            Control_ReceivingAnalytics_Button_ReceivingScopeHeader.AutoSize = true;
            Control_ReceivingAnalytics_Button_ReceivingScopeHeader.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_Button_ReceivingScopeHeader.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Button_ReceivingScopeHeader.FlatStyle = FlatStyle.Flat;
            Control_ReceivingAnalytics_Button_ReceivingScopeHeader.Location = new Point(219, 3);
            Control_ReceivingAnalytics_Button_ReceivingScopeHeader.MaximumSize = new Size(60, 30);
            Control_ReceivingAnalytics_Button_ReceivingScopeHeader.MinimumSize = new Size(60, 30);
            Control_ReceivingAnalytics_Button_ReceivingScopeHeader.Name = "Control_ReceivingAnalytics_Button_ReceivingScopeHeader";
            Control_ReceivingAnalytics_Button_ReceivingScopeHeader.Size = new Size(60, 30);
            Control_ReceivingAnalytics_Button_ReceivingScopeHeader.TabIndex = 12;
            Control_ReceivingAnalytics_Button_ReceivingScopeHeader.Text = "🡱";
            Control_ReceivingAnalytics_Button_ReceivingScopeHeader.UseVisualStyleBackColor = false;
            // 
            // Control_ReceivingAnalytics_Label_ReceivingScopeHeader
            // 
            Control_ReceivingAnalytics_Label_ReceivingScopeHeader.AutoSize = true;
            Control_ReceivingAnalytics_Label_ReceivingScopeHeader.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Label_ReceivingScopeHeader.Font = new Font("Segoe UI Emoji", 9.75F);
            Control_ReceivingAnalytics_Label_ReceivingScopeHeader.Location = new Point(3, 3);
            Control_ReceivingAnalytics_Label_ReceivingScopeHeader.Margin = new Padding(3);
            Control_ReceivingAnalytics_Label_ReceivingScopeHeader.Name = "Control_ReceivingAnalytics_Label_ReceivingScopeHeader";
            Control_ReceivingAnalytics_Label_ReceivingScopeHeader.Size = new Size(210, 30);
            Control_ReceivingAnalytics_Label_ReceivingScopeHeader.TabIndex = 16;
            Control_ReceivingAnalytics_Label_ReceivingScopeHeader.Text = "🔘 Receiving Scope";
            Control_ReceivingAnalytics_Label_ReceivingScopeHeader.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_ReceivingAnalytics_Panel_ReceivingScopeContents
            // 
            Control_ReceivingAnalytics_Panel_ReceivingScopeContents.AutoSize = true;
            Control_ReceivingAnalytics_Panel_ReceivingScopeContents.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_Panel_ReceivingScopeContents.BorderStyle = BorderStyle.FixedSingle;
            Control_ReceivingAnalytics_Panel_ReceivingScopeContents.Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeContents);
            Control_ReceivingAnalytics_Panel_ReceivingScopeContents.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_Panel_ReceivingScopeContents.Location = new Point(3, 41);
            Control_ReceivingAnalytics_Panel_ReceivingScopeContents.Margin = new Padding(3, 0, 3, 3);
            Control_ReceivingAnalytics_Panel_ReceivingScopeContents.Name = "Control_ReceivingAnalytics_Panel_ReceivingScopeContents";
            Control_ReceivingAnalytics_Panel_ReceivingScopeContents.Size = new Size(284, 77);
            Control_ReceivingAnalytics_Panel_ReceivingScopeContents.TabIndex = 26;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeContents
            // 
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeContents.AutoSize = true;
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeContents.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeContents.ColumnCount = 1;
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeContents.ColumnStyles.Add(new ColumnStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeContents.Controls.Add(Control_ReceivingAnalytics_CheckBox_ShowWithPartID, 0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeContents.Controls.Add(Control_ReceivingAnalytics_CheckBox_ShowMMC, 0, 1);
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeContents.Controls.Add(Control_ReceivingAnalytics_CheckBox_ShowMMF, 0, 2);
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeContents.Dock = DockStyle.Fill;
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeContents.Location = new Point(0, 0);
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeContents.Name = "Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeContents";
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeContents.RowCount = 3;
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeContents.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeContents.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeContents.RowStyles.Add(new RowStyle());
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeContents.Size = new Size(282, 75);
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeContents.TabIndex = 3;
            // 
            // Control_ReceivingAnalytics
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(Control_ReceivingAnalytics_TableLayoutPanel_Main);
            MinimumSize = new Size(1500, 875);
            Name = "Control_ReceivingAnalytics";
            Size = new Size(1500, 875);
            Control_ReceivingAnalytics_Panel_CheckBoxes_Legend.ResumeLayout(false);
            Control_ReceivingAnalytics_Panel_CheckBoxes_Legend.PerformLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_Legend.PerformLayout();
            Control_ReceivingAnalytics_Panel_DataGridView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Control_ReceivingAnalytics_DataGridView_Results).EndInit();
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_Buttons.PerformLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_DateRange.PerformLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_WeekButtons.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_WeekButtons.PerformLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_Main.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_Main.PerformLayout();
            Control_ReceivingAnalytics_Panel_Results.ResumeLayout(false);
            Control_ReceivingAnalytics_Panel_Results.PerformLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_Results.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_Results.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            Control_ReceivingAnalytics_Panel_Buttons.ResumeLayout(false);
            Control_ReceivingAnalytics_Panel_Buttons.PerformLayout();
            Control_ReceivingAnalytics_Panel_SideBarMain.ResumeLayout(false);
            Control_ReceivingAnalytics_Panel_SideBarMain.PerformLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarMain.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarMain.PerformLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeMain.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeMain.PerformLayout();
            Control_ReceivingAnalytics_Panel_DateRangeHeader.ResumeLayout(false);
            Control_ReceivingAnalytics_Panel_DateRangeHeader.PerformLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeHeader.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeHeader.PerformLayout();
            Control_ReceivingAnalytics_Panel_DateRangeContents.ResumeLayout(false);
            Control_ReceivingAnalytics_Panel_DateRangeContents.PerformLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeContents.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_DateRangeContents.PerformLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersMain.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersMain.PerformLayout();
            Control_ReceivingAnalytics_Panel_FiltersContents.ResumeLayout(false);
            Control_ReceivingAnalytics_Panel_FiltersContents.PerformLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersContents.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersContents.PerformLayout();
            Control_ReceivingAnalytics_Panel_FiltersHeader.ResumeLayout(false);
            Control_ReceivingAnalytics_Panel_FiltersHeader.PerformLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersHeader.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_FiltersHeader.PerformLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesMain.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesMain.PerformLayout();
            Control_ReceivingAnalytics_Panel_POStatesHeader.ResumeLayout(false);
            Control_ReceivingAnalytics_Panel_POStatesHeader.PerformLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesHeader.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesHeader.PerformLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesContents.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_POStatesContents.PerformLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarPOStates.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_SideBarPOStates.PerformLayout();
            Control_ReceivingAnalytics_Panel_OutsideScopeMain.ResumeLayout(false);
            Control_ReceivingAnalytics_Panel_OutsideScopeMain.PerformLayout();
            Control_ReceivingAnalytics_Panel_OutsideScopeHeader.ResumeLayout(false);
            Control_ReceivingAnalytics_Panel_OutsideScopeHeader.PerformLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeHeader.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeHeader.PerformLayout();
            Control_ReceivingAnalytics_Panel_OutsideScopeContents.ResumeLayout(false);
            Control_ReceivingAnalytics_Panel_OutsideScopeContents.PerformLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeContents.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeContents.PerformLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesMain.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesMain.PerformLayout();
            Control_ReceivingAnalytics_Panel_DeliveryStatusHeader.ResumeLayout(false);
            Control_ReceivingAnalytics_Panel_DeliveryStatusHeader.PerformLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesHeader.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesHeader.PerformLayout();
            Control_ReceivingAnalytics_Panel_DeliveryStatesContents.ResumeLayout(false);
            Control_ReceivingAnalytics_Panel_DeliveryStatesContents.PerformLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesContents.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesContents.PerformLayout();
            Control_ReceivingAnalytics_Panel_ReceivingScopeMain.ResumeLayout(false);
            Control_ReceivingAnalytics_Panel_ReceivingScopeMain.PerformLayout();
            Control_ReceivingAnalytics_Panel_ReceivingScopeHeader.ResumeLayout(false);
            Control_ReceivingAnalytics_Panel_ReceivingScopeHeader.PerformLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeHeader.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeHeader.PerformLayout();
            Control_ReceivingAnalytics_Panel_ReceivingScopeContents.ResumeLayout(false);
            Control_ReceivingAnalytics_Panel_ReceivingScopeContents.PerformLayout();
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeContents.ResumeLayout(false);
            Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeContents.PerformLayout();
            ResumeLayout(false);

        }

        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_Main;
        private System.Windows.Forms.Panel Control_ReceivingAnalytics_Panel_Filters;
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
        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_Carrier;
        private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_Carrier;
        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_PartNumber;
        private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel Control_ReceivingAnalytics_SuggestionTextBoxWithLabel_PartNumber;
        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_Buttons;
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
        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_WeekButtons;
        private Button Control_ReceivingAnalytics_Button_NextWeek;
        private Button Control_ReceivingAnalytics_Button_PreviousWeek;
        private Button Control_ReceivingAnalytics_Button_CurrentWeek;
        private Panel Control_ReceivingAnalytics_Panel_DataGridView;
        private DataGridView Control_ReceivingAnalytics_DataGridView_Results;
        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_MainX;
        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_DateRangeContents;
        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_FiltersContents;
        private Label Control_ReceivingAnalytics_Label_SideBarFilters;
        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_SideBarPOStates;
        private Label Control_ReceivingAnalytics_Label_SideBarPOStates;
        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_DateRangeHeader;
        private Button Control_ReceivingAnalytics_Button_DateRangeHeader;
        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_DateRangeMain;
        private Panel Control_ReceivingAnalytics_Panel_DateRangeHeader;
        private Panel Control_ReceivingAnalytics_Panel_DateRangeContents;
        private Panel Control_ReceivingAnalytics_Panel_FiltersHeader;
        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_FiltersMain;
        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_FiltersHeader;
        private Button Control_ReceivingAnalytics_Button_FiltersHeader;
        private Panel Control_ReceivingAnalytics_Panel_FiltersContents;
        private Panel Control_ReceivingAnalytics_TableLayoutPanel_POStatesContents;
        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_POStatesMain;
        private Panel Control_ReceivingAnalytics_Panel_POStatesHeader;
        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_POStatesHeader;
        private Button Control_ReceivingAnalytics_Button_POStatesHeader;
        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesMain;
        private Panel Control_ReceivingAnalytics_Panel_DeliveryStatusHeader;
        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesHeader;
        private Button Control_ReceivingAnalytics_Button_DeliveryStatesHeader;
        private Label Control_ReceivingAnalytics_Label_DeliveryStatesHeader;
        private Panel Control_ReceivingAnalytics_Panel_DeliveryStatesContents;
        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_DeliveryStatesContents;
        private TableLayoutPanel Control_ReceivingAnalytics_Panel_ReceivingScopeMain;
        private Panel Control_ReceivingAnalytics_Panel_ReceivingScopeHeader;
        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeHeader;
        private Button Control_ReceivingAnalytics_Button_ReceivingScopeHeader;
        private Label Control_ReceivingAnalytics_Label_ReceivingScopeHeader;
        private Panel Control_ReceivingAnalytics_Panel_ReceivingScopeContents;
        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeContents;
        private TableLayoutPanel Control_ReceivingAnalytics_Panel_OutsideScopeMain;
        private Panel Control_ReceivingAnalytics_Panel_OutsideScopeHeader;
        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeHeader;
        private Button Control_ReceivingAnalytics_Button_OutsideScopeHeader;
        private Label Control_ReceivingAnalytics_Label_OutsideScopeHeader;
        private Panel Control_ReceivingAnalytics_Panel_OutsideScopeContents;
        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_OutsideScopeContents;
        private Panel Control_ReceivingAnalytics_Panel_Buttons;
        private Panel Control_ReceivingAnalytics_Panel_SideBarMain;
        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_SideBarMain;
        private TableLayoutPanel Control_ReceivingAnalytics_TableLayoutPanel_Results;
        private Panel Control_ReceivingAnalytics_Panel_Results;
        private TableLayoutPanel tableLayoutPanel1;
    }
}
