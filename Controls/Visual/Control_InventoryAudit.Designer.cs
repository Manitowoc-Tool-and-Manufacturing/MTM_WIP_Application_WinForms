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
            Control_InventoryAudit_TableLayout_Main = new TableLayoutPanel();
            Control_InventoryAudit_TableLayout_Lifecycle = new TableLayoutPanel();
            Control_InventoryAudit_SuggestionTextBox_SearchBy = new MTM_WIP_Application_Winforms.Components.Shared.Component_SuggestionTextBoxWithLabel();
            Control_InventoryAudit_SuggestionTextBox_LifecyclePart = new MTM_WIP_Application_Winforms.Components.Shared.Component_SuggestionTextBoxWithLabel();
            Control_InventoryAudit_TableLayout_Dates = new TableLayoutPanel();
            Control_InventoryAudit_Label_StartDate = new Label();
            Control_InventoryAudit_DateTimePicker_StartDate = new DateTimePicker();
            Control_InventoryAudit_Label_EndDate = new Label();
            Control_InventoryAudit_DateTimePicker_EndDate = new DateTimePicker();
            Control_InventoryAudit_TableLayout_Buttons = new TableLayoutPanel();
            Control_InventoryAudit_Button_Export = new Button();
            Control_InventoryAudit_Button_Search = new Button();
            Control_InventoryAudit_Panel_Results = new Panel();
            Control_InventoryAudit_DataGridView_Results = new DataGridView();
            tableLayoutPanel1 = new TableLayoutPanel();
            Control_InventoryAudit_RadioButton_Custom = new RadioButton();
            Control_InventoryAudit_RadioButton_Month = new RadioButton();
            Control_InventoryAudit_RadioButton_Week = new RadioButton();
            Control_InventoryAudit_RadioButton_Today = new RadioButton();
            Control_InventoryAudit_TableLayout_Main.SuspendLayout();
            Control_InventoryAudit_TableLayout_Lifecycle.SuspendLayout();
            Control_InventoryAudit_TableLayout_Dates.SuspendLayout();
            Control_InventoryAudit_TableLayout_Buttons.SuspendLayout();
            Control_InventoryAudit_Panel_Results.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Control_InventoryAudit_DataGridView_Results).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // Control_InventoryAudit_TableLayout_Main
            // 
            Control_InventoryAudit_TableLayout_Main.AutoSize = true;
            Control_InventoryAudit_TableLayout_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_InventoryAudit_TableLayout_Main.ColumnCount = 1;
            Control_InventoryAudit_TableLayout_Main.ColumnStyles.Add(new ColumnStyle());
            Control_InventoryAudit_TableLayout_Main.Controls.Add(Control_InventoryAudit_TableLayout_Lifecycle, 0, 0);
            Control_InventoryAudit_TableLayout_Main.Dock = DockStyle.Fill;
            Control_InventoryAudit_TableLayout_Main.Location = new Point(0, 0);
            Control_InventoryAudit_TableLayout_Main.Name = "Control_InventoryAudit_TableLayout_Main";
            Control_InventoryAudit_TableLayout_Main.RowCount = 1;
            Control_InventoryAudit_TableLayout_Main.RowStyles.Add(new RowStyle());
            Control_InventoryAudit_TableLayout_Main.Size = new Size(354, 231);
            Control_InventoryAudit_TableLayout_Main.TabIndex = 0;
            // 
            // Control_InventoryAudit_TableLayout_Lifecycle
            // 
            Control_InventoryAudit_TableLayout_Lifecycle.AutoSize = true;
            Control_InventoryAudit_TableLayout_Lifecycle.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_InventoryAudit_TableLayout_Lifecycle.ColumnCount = 1;
            Control_InventoryAudit_TableLayout_Lifecycle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_InventoryAudit_TableLayout_Lifecycle.Controls.Add(Control_InventoryAudit_SuggestionTextBox_SearchBy, 0, 0);
            Control_InventoryAudit_TableLayout_Lifecycle.Controls.Add(Control_InventoryAudit_SuggestionTextBox_LifecyclePart, 0, 1);
            Control_InventoryAudit_TableLayout_Lifecycle.Controls.Add(Control_InventoryAudit_TableLayout_Dates, 0, 3);
            Control_InventoryAudit_TableLayout_Lifecycle.Controls.Add(Control_InventoryAudit_TableLayout_Buttons, 0, 4);
            Control_InventoryAudit_TableLayout_Lifecycle.Controls.Add(Control_InventoryAudit_Panel_Results, 0, 5);
            Control_InventoryAudit_TableLayout_Lifecycle.Controls.Add(tableLayoutPanel1, 0, 2);
            Control_InventoryAudit_TableLayout_Lifecycle.Dock = DockStyle.Fill;
            Control_InventoryAudit_TableLayout_Lifecycle.Location = new Point(3, 3);
            Control_InventoryAudit_TableLayout_Lifecycle.Name = "Control_InventoryAudit_TableLayout_Lifecycle";
            Control_InventoryAudit_TableLayout_Lifecycle.Padding = new Padding(10);
            Control_InventoryAudit_TableLayout_Lifecycle.RowCount = 6;
            Control_InventoryAudit_TableLayout_Lifecycle.RowStyles.Add(new RowStyle());
            Control_InventoryAudit_TableLayout_Lifecycle.RowStyles.Add(new RowStyle());
            Control_InventoryAudit_TableLayout_Lifecycle.RowStyles.Add(new RowStyle());
            Control_InventoryAudit_TableLayout_Lifecycle.RowStyles.Add(new RowStyle());
            Control_InventoryAudit_TableLayout_Lifecycle.RowStyles.Add(new RowStyle());
            Control_InventoryAudit_TableLayout_Lifecycle.RowStyles.Add(new RowStyle());
            Control_InventoryAudit_TableLayout_Lifecycle.Size = new Size(348, 225);
            Control_InventoryAudit_TableLayout_Lifecycle.TabIndex = 1;
            // 
            // Control_InventoryAudit_SuggestionTextBox_SearchBy
            // 
            Control_InventoryAudit_SuggestionTextBox_SearchBy.AutoSize = true;
            Control_InventoryAudit_SuggestionTextBox_SearchBy.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_InventoryAudit_SuggestionTextBox_SearchBy.Dock = DockStyle.Fill;
            Control_InventoryAudit_SuggestionTextBox_SearchBy.LabelText = "Search By";
            Control_InventoryAudit_SuggestionTextBox_SearchBy.LabelVisibility = Enum_LabelVisibility.Hidden;
            Control_InventoryAudit_SuggestionTextBox_SearchBy.Location = new Point(13, 13);
            Control_InventoryAudit_SuggestionTextBox_SearchBy.MaxLength = 130;
            Control_InventoryAudit_SuggestionTextBox_SearchBy.MinimumSize = new Size(100, 23);
            Control_InventoryAudit_SuggestionTextBox_SearchBy.MinLength = 130;
            Control_InventoryAudit_SuggestionTextBox_SearchBy.Name = "Control_InventoryAudit_SuggestionTextBox_SearchBy";
            Control_InventoryAudit_SuggestionTextBox_SearchBy.PlaceholderText = "Enter Search Filter";
            Control_InventoryAudit_SuggestionTextBox_SearchBy.ShowValidationColor = false;
            Control_InventoryAudit_SuggestionTextBox_SearchBy.Size = new Size(322, 23);
            Control_InventoryAudit_SuggestionTextBox_SearchBy.TabIndex = 1;
            Control_InventoryAudit_SuggestionTextBox_SearchBy.ValidatorType = null;
            Control_InventoryAudit_SuggestionTextBox_SearchBy.Visible = false;
            // 
            // Control_InventoryAudit_SuggestionTextBox_LifecyclePart
            // 
            Control_InventoryAudit_SuggestionTextBox_LifecyclePart.AutoSize = true;
            Control_InventoryAudit_SuggestionTextBox_LifecyclePart.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_InventoryAudit_SuggestionTextBox_LifecyclePart.Dock = DockStyle.Fill;
            Control_InventoryAudit_SuggestionTextBox_LifecyclePart.LabelText = "Enter Part ID";
            Control_InventoryAudit_SuggestionTextBox_LifecyclePart.LabelVisibility = Enum_LabelVisibility.Hidden;
            Control_InventoryAudit_SuggestionTextBox_LifecyclePart.Location = new Point(13, 42);
            Control_InventoryAudit_SuggestionTextBox_LifecyclePart.MaxLength = 130;
            Control_InventoryAudit_SuggestionTextBox_LifecyclePart.MinimumSize = new Size(100, 23);
            Control_InventoryAudit_SuggestionTextBox_LifecyclePart.MinLength = 130;
            Control_InventoryAudit_SuggestionTextBox_LifecyclePart.Name = "Control_InventoryAudit_SuggestionTextBox_LifecyclePart";
            Control_InventoryAudit_SuggestionTextBox_LifecyclePart.PlaceholderText = "Enter Part Number";
            Control_InventoryAudit_SuggestionTextBox_LifecyclePart.ShowValidationColor = false;
            Control_InventoryAudit_SuggestionTextBox_LifecyclePart.Size = new Size(322, 23);
            Control_InventoryAudit_SuggestionTextBox_LifecyclePart.TabIndex = 2;
            Control_InventoryAudit_SuggestionTextBox_LifecyclePart.ValidatorType = null;
            // 
            // Control_InventoryAudit_TableLayout_Dates
            // 
            Control_InventoryAudit_TableLayout_Dates.AutoSize = true;
            Control_InventoryAudit_TableLayout_Dates.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_InventoryAudit_TableLayout_Dates.ColumnCount = 3;
            Control_InventoryAudit_TableLayout_Dates.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            Control_InventoryAudit_TableLayout_Dates.ColumnStyles.Add(new ColumnStyle());
            Control_InventoryAudit_TableLayout_Dates.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.0000076F));
            Control_InventoryAudit_TableLayout_Dates.Controls.Add(Control_InventoryAudit_Label_StartDate, 0, 0);
            Control_InventoryAudit_TableLayout_Dates.Controls.Add(Control_InventoryAudit_DateTimePicker_StartDate, 0, 1);
            Control_InventoryAudit_TableLayout_Dates.Controls.Add(Control_InventoryAudit_Label_EndDate, 1, 1);
            Control_InventoryAudit_TableLayout_Dates.Controls.Add(Control_InventoryAudit_DateTimePicker_EndDate, 2, 1);
            Control_InventoryAudit_TableLayout_Dates.Dock = DockStyle.Fill;
            Control_InventoryAudit_TableLayout_Dates.Location = new Point(13, 102);
            Control_InventoryAudit_TableLayout_Dates.Name = "Control_InventoryAudit_TableLayout_Dates";
            Control_InventoryAudit_TableLayout_Dates.RowCount = 2;
            Control_InventoryAudit_TableLayout_Dates.RowStyles.Add(new RowStyle());
            Control_InventoryAudit_TableLayout_Dates.RowStyles.Add(new RowStyle());
            Control_InventoryAudit_TableLayout_Dates.Size = new Size(322, 52);
            Control_InventoryAudit_TableLayout_Dates.TabIndex = 1;
            // 
            // Control_InventoryAudit_Label_StartDate
            // 
            Control_InventoryAudit_Label_StartDate.AutoSize = true;
            Control_InventoryAudit_TableLayout_Dates.SetColumnSpan(Control_InventoryAudit_Label_StartDate, 3);
            Control_InventoryAudit_Label_StartDate.Dock = DockStyle.Fill;
            Control_InventoryAudit_Label_StartDate.Font = new Font("Segoe UI Emoji", 9.75F);
            Control_InventoryAudit_Label_StartDate.Location = new Point(3, 3);
            Control_InventoryAudit_Label_StartDate.Margin = new Padding(3);
            Control_InventoryAudit_Label_StartDate.Name = "Control_InventoryAudit_Label_StartDate";
            Control_InventoryAudit_Label_StartDate.Size = new Size(316, 17);
            Control_InventoryAudit_Label_StartDate.TabIndex = 0;
            Control_InventoryAudit_Label_StartDate.Text = "📅 Date Range";
            Control_InventoryAudit_Label_StartDate.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_InventoryAudit_DateTimePicker_StartDate
            // 
            Control_InventoryAudit_DateTimePicker_StartDate.Dock = DockStyle.Fill;
            Control_InventoryAudit_DateTimePicker_StartDate.Format = DateTimePickerFormat.Short;
            Control_InventoryAudit_DateTimePicker_StartDate.Location = new Point(3, 26);
            Control_InventoryAudit_DateTimePicker_StartDate.MaximumSize = new Size(0, 23);
            Control_InventoryAudit_DateTimePicker_StartDate.MinimumSize = new Size(100, 23);
            Control_InventoryAudit_DateTimePicker_StartDate.Name = "Control_InventoryAudit_DateTimePicker_StartDate";
            Control_InventoryAudit_DateTimePicker_StartDate.Size = new Size(141, 23);
            Control_InventoryAudit_DateTimePicker_StartDate.TabIndex = 7;
            // 
            // Control_InventoryAudit_Label_EndDate
            // 
            Control_InventoryAudit_Label_EndDate.AutoSize = true;
            Control_InventoryAudit_Label_EndDate.Dock = DockStyle.Fill;
            Control_InventoryAudit_Label_EndDate.Location = new Point(150, 26);
            Control_InventoryAudit_Label_EndDate.Margin = new Padding(3);
            Control_InventoryAudit_Label_EndDate.Name = "Control_InventoryAudit_Label_EndDate";
            Control_InventoryAudit_Label_EndDate.Size = new Size(20, 23);
            Control_InventoryAudit_Label_EndDate.TabIndex = 2;
            Control_InventoryAudit_Label_EndDate.Text = "To";
            Control_InventoryAudit_Label_EndDate.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_InventoryAudit_DateTimePicker_EndDate
            // 
            Control_InventoryAudit_DateTimePicker_EndDate.Dock = DockStyle.Fill;
            Control_InventoryAudit_DateTimePicker_EndDate.Format = DateTimePickerFormat.Short;
            Control_InventoryAudit_DateTimePicker_EndDate.Location = new Point(176, 26);
            Control_InventoryAudit_DateTimePicker_EndDate.MaximumSize = new Size(0, 23);
            Control_InventoryAudit_DateTimePicker_EndDate.MinimumSize = new Size(100, 23);
            Control_InventoryAudit_DateTimePicker_EndDate.Name = "Control_InventoryAudit_DateTimePicker_EndDate";
            Control_InventoryAudit_DateTimePicker_EndDate.Size = new Size(143, 23);
            Control_InventoryAudit_DateTimePicker_EndDate.TabIndex = 8;
            // 
            // Control_InventoryAudit_TableLayout_Buttons
            // 
            Control_InventoryAudit_TableLayout_Buttons.AutoSize = true;
            Control_InventoryAudit_TableLayout_Buttons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_InventoryAudit_TableLayout_Buttons.ColumnCount = 3;
            Control_InventoryAudit_TableLayout_Buttons.ColumnStyles.Add(new ColumnStyle());
            Control_InventoryAudit_TableLayout_Buttons.ColumnStyles.Add(new ColumnStyle());
            Control_InventoryAudit_TableLayout_Buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_InventoryAudit_TableLayout_Buttons.Controls.Add(Control_InventoryAudit_Button_Export, 1, 0);
            Control_InventoryAudit_TableLayout_Buttons.Controls.Add(Control_InventoryAudit_Button_Search, 0, 0);
            Control_InventoryAudit_TableLayout_Buttons.Dock = DockStyle.Fill;
            Control_InventoryAudit_TableLayout_Buttons.Location = new Point(13, 160);
            Control_InventoryAudit_TableLayout_Buttons.Name = "Control_InventoryAudit_TableLayout_Buttons";
            Control_InventoryAudit_TableLayout_Buttons.RowCount = 1;
            Control_InventoryAudit_TableLayout_Buttons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_InventoryAudit_TableLayout_Buttons.Size = new Size(322, 46);
            Control_InventoryAudit_TableLayout_Buttons.TabIndex = 2;
            // 
            // Control_InventoryAudit_Button_Export
            // 
            Control_InventoryAudit_Button_Export.AutoSize = true;
            Control_InventoryAudit_Button_Export.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_InventoryAudit_Button_Export.Dock = DockStyle.Fill;
            Control_InventoryAudit_Button_Export.Location = new Point(109, 3);
            Control_InventoryAudit_Button_Export.MaximumSize = new Size(100, 40);
            Control_InventoryAudit_Button_Export.MinimumSize = new Size(100, 40);
            Control_InventoryAudit_Button_Export.Name = "Control_InventoryAudit_Button_Export";
            Control_InventoryAudit_Button_Export.Size = new Size(100, 40);
            Control_InventoryAudit_Button_Export.TabIndex = 1;
            Control_InventoryAudit_Button_Export.TabStop = false;
            Control_InventoryAudit_Button_Export.Text = "Export to Excel";
            Control_InventoryAudit_Button_Export.UseVisualStyleBackColor = true;
            // 
            // Control_InventoryAudit_Button_Search
            // 
            Control_InventoryAudit_Button_Search.AutoSize = true;
            Control_InventoryAudit_Button_Search.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_InventoryAudit_Button_Search.Dock = DockStyle.Fill;
            Control_InventoryAudit_Button_Search.Location = new Point(3, 3);
            Control_InventoryAudit_Button_Search.MaximumSize = new Size(100, 40);
            Control_InventoryAudit_Button_Search.MinimumSize = new Size(100, 40);
            Control_InventoryAudit_Button_Search.Name = "Control_InventoryAudit_Button_Search";
            Control_InventoryAudit_Button_Search.Size = new Size(100, 40);
            Control_InventoryAudit_Button_Search.TabIndex = 9;
            Control_InventoryAudit_Button_Search.Text = "Search";
            Control_InventoryAudit_Button_Search.UseVisualStyleBackColor = true;
            // 
            // Control_InventoryAudit_Panel_Results
            // 
            Control_InventoryAudit_Panel_Results.AutoSize = true;
            Control_InventoryAudit_Panel_Results.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_InventoryAudit_Panel_Results.Controls.Add(Control_InventoryAudit_DataGridView_Results);
            Control_InventoryAudit_Panel_Results.Dock = DockStyle.Fill;
            Control_InventoryAudit_Panel_Results.Location = new Point(13, 212);
            Control_InventoryAudit_Panel_Results.Name = "Control_InventoryAudit_Panel_Results";
            Control_InventoryAudit_Panel_Results.Size = new Size(322, 1);
            Control_InventoryAudit_Panel_Results.TabIndex = 4;
            // 
            // Control_InventoryAudit_DataGridView_Results
            // 
            Control_InventoryAudit_DataGridView_Results.AllowUserToAddRows = false;
            Control_InventoryAudit_DataGridView_Results.AllowUserToDeleteRows = false;
            Control_InventoryAudit_DataGridView_Results.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Control_InventoryAudit_DataGridView_Results.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Control_InventoryAudit_DataGridView_Results.Dock = DockStyle.Fill;
            Control_InventoryAudit_DataGridView_Results.Location = new Point(0, 0);
            Control_InventoryAudit_DataGridView_Results.Name = "Control_InventoryAudit_DataGridView_Results";
            Control_InventoryAudit_DataGridView_Results.ReadOnly = true;
            Control_InventoryAudit_DataGridView_Results.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Control_InventoryAudit_DataGridView_Results.Size = new Size(322, 1);
            Control_InventoryAudit_DataGridView_Results.TabIndex = 10;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 6;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.0000076F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 49.9999962F));
            tableLayoutPanel1.Controls.Add(Control_InventoryAudit_RadioButton_Custom, 4, 0);
            tableLayoutPanel1.Controls.Add(Control_InventoryAudit_RadioButton_Month, 3, 0);
            tableLayoutPanel1.Controls.Add(Control_InventoryAudit_RadioButton_Week, 2, 0);
            tableLayoutPanel1.Controls.Add(Control_InventoryAudit_RadioButton_Today, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(13, 71);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(322, 25);
            tableLayoutPanel1.TabIndex = 5;
            // 
            // Control_InventoryAudit_RadioButton_Custom
            // 
            Control_InventoryAudit_RadioButton_Custom.AutoSize = true;
            Control_InventoryAudit_RadioButton_Custom.Location = new Point(251, 3);
            Control_InventoryAudit_RadioButton_Custom.Name = "Control_InventoryAudit_RadioButton_Custom";
            Control_InventoryAudit_RadioButton_Custom.Size = new Size(67, 19);
            Control_InventoryAudit_RadioButton_Custom.TabIndex = 6;
            Control_InventoryAudit_RadioButton_Custom.TabStop = true;
            Control_InventoryAudit_RadioButton_Custom.Text = "Custom";
            Control_InventoryAudit_RadioButton_Custom.UseVisualStyleBackColor = true;
            // 
            // Control_InventoryAudit_RadioButton_Month
            // 
            Control_InventoryAudit_RadioButton_Month.AutoSize = true;
            Control_InventoryAudit_RadioButton_Month.Checked = true;
            Control_InventoryAudit_RadioButton_Month.Location = new Point(156, 3);
            Control_InventoryAudit_RadioButton_Month.Name = "Control_InventoryAudit_RadioButton_Month";
            Control_InventoryAudit_RadioButton_Month.Size = new Size(89, 19);
            Control_InventoryAudit_RadioButton_Month.TabIndex = 5;
            Control_InventoryAudit_RadioButton_Month.TabStop = true;
            Control_InventoryAudit_RadioButton_Month.Text = "Last 30 Days";
            Control_InventoryAudit_RadioButton_Month.UseVisualStyleBackColor = true;
            // 
            // Control_InventoryAudit_RadioButton_Week
            // 
            Control_InventoryAudit_RadioButton_Week.AutoSize = true;
            Control_InventoryAudit_RadioButton_Week.Location = new Point(67, 3);
            Control_InventoryAudit_RadioButton_Week.Name = "Control_InventoryAudit_RadioButton_Week";
            Control_InventoryAudit_RadioButton_Week.Size = new Size(83, 19);
            Control_InventoryAudit_RadioButton_Week.TabIndex = 4;
            Control_InventoryAudit_RadioButton_Week.TabStop = true;
            Control_InventoryAudit_RadioButton_Week.Text = "Last 7 Days";
            Control_InventoryAudit_RadioButton_Week.UseVisualStyleBackColor = true;
            // 
            // Control_InventoryAudit_RadioButton_Today
            // 
            Control_InventoryAudit_RadioButton_Today.AutoSize = true;
            Control_InventoryAudit_RadioButton_Today.Location = new Point(4, 3);
            Control_InventoryAudit_RadioButton_Today.Name = "Control_InventoryAudit_RadioButton_Today";
            Control_InventoryAudit_RadioButton_Today.Size = new Size(57, 19);
            Control_InventoryAudit_RadioButton_Today.TabIndex = 3;
            Control_InventoryAudit_RadioButton_Today.TabStop = true;
            Control_InventoryAudit_RadioButton_Today.Text = "Today";
            Control_InventoryAudit_RadioButton_Today.UseVisualStyleBackColor = true;
            // 
            // Control_InventoryAudit
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(Control_InventoryAudit_TableLayout_Main);
            MaximumSize = new Size(950, 550);
            Name = "Control_InventoryAudit";
            Size = new Size(354, 231);
            Control_InventoryAudit_TableLayout_Main.ResumeLayout(false);
            Control_InventoryAudit_TableLayout_Main.PerformLayout();
            Control_InventoryAudit_TableLayout_Lifecycle.ResumeLayout(false);
            Control_InventoryAudit_TableLayout_Lifecycle.PerformLayout();
            Control_InventoryAudit_TableLayout_Dates.ResumeLayout(false);
            Control_InventoryAudit_TableLayout_Dates.PerformLayout();
            Control_InventoryAudit_TableLayout_Buttons.ResumeLayout(false);
            Control_InventoryAudit_TableLayout_Buttons.PerformLayout();
            Control_InventoryAudit_Panel_Results.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Control_InventoryAudit_DataGridView_Results).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel Control_InventoryAudit_TableLayout_Main;
        private TableLayoutPanel Control_InventoryAudit_TableLayout_Lifecycle;
        private Components.Shared.Component_SuggestionTextBoxWithLabel Control_InventoryAudit_SuggestionTextBox_SearchBy;
        private Components.Shared.Component_SuggestionTextBoxWithLabel Control_InventoryAudit_SuggestionTextBox_LifecyclePart;
        private TableLayoutPanel Control_InventoryAudit_TableLayout_Dates;
        private Label Control_InventoryAudit_Label_StartDate;
        private DateTimePicker Control_InventoryAudit_DateTimePicker_StartDate;
        private Label Control_InventoryAudit_Label_EndDate;
        private DateTimePicker Control_InventoryAudit_DateTimePicker_EndDate;
        private TableLayoutPanel Control_InventoryAudit_TableLayout_Buttons;
        private Button Control_InventoryAudit_Button_Export;
        private Button Control_InventoryAudit_Button_Search;
        private Panel Control_InventoryAudit_Panel_Results;
        private DataGridView Control_InventoryAudit_DataGridView_Results;
        private TableLayoutPanel tableLayoutPanel1;
        private RadioButton Control_InventoryAudit_RadioButton_Custom;
        private RadioButton Control_InventoryAudit_RadioButton_Month;
        private RadioButton Control_InventoryAudit_RadioButton_Week;
        private RadioButton Control_InventoryAudit_RadioButton_Today;
    }
}
