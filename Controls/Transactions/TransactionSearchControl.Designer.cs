using System.Drawing;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Controls.Shared;

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
            TransactionSearchControl_TableLayout_Main = new TableLayoutPanel();
            TransactionSearchControl_TableLayout_Filters = new TableLayoutPanel();
            TransactionSearchControl_TableLayout_Controls = new TableLayoutPanel();
            TransactionSearchControl_GroupBox_TransactionTypes = new GroupBox();
            TransactionSearchControl_TableLayout_TransactionTypes = new TableLayoutPanel();
            TransactionSearchControl_CheckBox_TRANSFER = new CheckBox();
            TransactionSearchControl_CheckBox_OUT = new CheckBox();
            TransactionSearchControl_CheckBox_IN = new CheckBox();
            TransactionSearchControl_GroupBox_DateRange = new GroupBox();
            TransactionSearchControl_TableLayout_DateTimePicker = new TableLayoutPanel();
            TransactionSearchControl_DateTimePicker_DateFrom = new DateTimePicker();
            TransactionSearchControl_DateTimePicker_DateTo = new DateTimePicker();
            TransactionSearchControl_Label_DateFrom = new Label();
            TransactionSearchControl_Label_DateTo = new Label();
            TransactionSearchControl_GroupBox_RadioButtons = new GroupBox();
            TransactionSearchControl_TableLayout_QuickFilters = new TableLayoutPanel();
            TransactionSearchControl_RadioButton_Custom = new RadioButton();
            TransactionSearchControl_RadioButton_Month = new RadioButton();
            TransactionSearchControl_RadioButton_Week = new RadioButton();
            TransactionSearchControl_RadioButton_Today = new RadioButton();
            TransactionSearchControl_RadioButton_Everything = new RadioButton();
            TransactionSearchControl_GroupBox_Search = new GroupBox();
            TransactionSearchControl_TableLayout_Search = new TableLayoutPanel();
            TransactionSearchControl_Suggestion_PartNumber = new SuggestionTextBoxWithLabel();
            TransactionSearchControl_Suggestion_User = new SuggestionTextBoxWithLabel();
            TransactionSearchControl_Suggestion_Operation = new SuggestionTextBoxWithLabel();
            TransactionSearchControl_Suggestion_FromLocation = new SuggestionTextBoxWithLabel();
            TransactionSearchControl_Suggestion_ToLocation = new SuggestionTextBoxWithLabel();
            TransactionSearchControl_Suggestion_Notes = new SuggestionTextBoxWithLabel();
            TransactionSearchControl_Panel_Buttons = new Panel();
            TransactionSearchControl_TableLayout_Buttons = new TableLayoutPanel();
            TransactionSearchControl_Button_Search = new Button();
            TransactionSearchControl_Button_Reset = new Button();
            TransactionSearchControl_TableLayout_Main.SuspendLayout();
            TransactionSearchControl_TableLayout_Filters.SuspendLayout();
            TransactionSearchControl_TableLayout_Controls.SuspendLayout();
            TransactionSearchControl_GroupBox_TransactionTypes.SuspendLayout();
            TransactionSearchControl_TableLayout_TransactionTypes.SuspendLayout();
            TransactionSearchControl_GroupBox_DateRange.SuspendLayout();
            TransactionSearchControl_TableLayout_DateTimePicker.SuspendLayout();
            TransactionSearchControl_GroupBox_RadioButtons.SuspendLayout();
            TransactionSearchControl_TableLayout_QuickFilters.SuspendLayout();
            TransactionSearchControl_GroupBox_Search.SuspendLayout();
            TransactionSearchControl_TableLayout_Search.SuspendLayout();
            TransactionSearchControl_Panel_Buttons.SuspendLayout();
            TransactionSearchControl_TableLayout_Buttons.SuspendLayout();
            SuspendLayout();
            //
            // TransactionSearchControl_TableLayout_Main
            //
            TransactionSearchControl_TableLayout_Main.AutoSize = true;
            TransactionSearchControl_TableLayout_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_TableLayout_Main.ColumnCount = 1;
            TransactionSearchControl_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TransactionSearchControl_TableLayout_Main.Controls.Add(TransactionSearchControl_TableLayout_Filters, 0, 1);
            TransactionSearchControl_TableLayout_Main.Dock = DockStyle.Fill;
            TransactionSearchControl_TableLayout_Main.Location = new Point(0, 0);
            TransactionSearchControl_TableLayout_Main.Margin = new Padding(0);
            TransactionSearchControl_TableLayout_Main.Name = "TransactionSearchControl_TableLayout_Main";
            TransactionSearchControl_TableLayout_Main.RowCount = 2;
            TransactionSearchControl_TableLayout_Main.RowStyles.Add(new RowStyle());
            TransactionSearchControl_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TransactionSearchControl_TableLayout_Main.Size = new Size(1082, 167);
            TransactionSearchControl_TableLayout_Main.TabIndex = 0;
            //
            // TransactionSearchControl_TableLayout_Filters
            //
            TransactionSearchControl_TableLayout_Filters.AutoSize = true;
            TransactionSearchControl_TableLayout_Filters.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_TableLayout_Filters.ColumnCount = 5;
            TransactionSearchControl_TableLayout_Filters.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_Filters.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_Filters.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_Filters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TransactionSearchControl_TableLayout_Filters.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_Filters.Controls.Add(TransactionSearchControl_TableLayout_Controls, 3, 0);
            TransactionSearchControl_TableLayout_Filters.Controls.Add(TransactionSearchControl_GroupBox_Search, 0, 0);
            TransactionSearchControl_TableLayout_Filters.Controls.Add(TransactionSearchControl_Panel_Buttons, 4, 0);
            TransactionSearchControl_TableLayout_Filters.Dock = DockStyle.Fill;
            TransactionSearchControl_TableLayout_Filters.Location = new Point(0, 0);
            TransactionSearchControl_TableLayout_Filters.Margin = new Padding(0);
            TransactionSearchControl_TableLayout_Filters.Name = "TransactionSearchControl_TableLayout_Filters";
            TransactionSearchControl_TableLayout_Filters.Padding = new Padding(2);
            TransactionSearchControl_TableLayout_Filters.RowCount = 3;
            TransactionSearchControl_TableLayout_Filters.RowStyles.Add(new RowStyle());
            TransactionSearchControl_TableLayout_Filters.RowStyles.Add(new RowStyle());
            TransactionSearchControl_TableLayout_Filters.RowStyles.Add(new RowStyle());
            TransactionSearchControl_TableLayout_Filters.Size = new Size(1082, 167);
            TransactionSearchControl_TableLayout_Filters.TabIndex = 1;
            //
            // TransactionSearchControl_TableLayout_Controls
            //
            TransactionSearchControl_TableLayout_Controls.AutoSize = true;
            TransactionSearchControl_TableLayout_Controls.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_TableLayout_Controls.ColumnCount = 1;
            TransactionSearchControl_TableLayout_Controls.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_Controls.Controls.Add(TransactionSearchControl_GroupBox_TransactionTypes, 0, 2);
            TransactionSearchControl_TableLayout_Controls.Controls.Add(TransactionSearchControl_GroupBox_DateRange, 0, 0);
            TransactionSearchControl_TableLayout_Controls.Controls.Add(TransactionSearchControl_GroupBox_RadioButtons, 0, 1);
            TransactionSearchControl_TableLayout_Controls.Dock = DockStyle.Fill;
            TransactionSearchControl_TableLayout_Controls.Location = new Point(575, 2);
            TransactionSearchControl_TableLayout_Controls.Margin = new Padding(0);
            TransactionSearchControl_TableLayout_Controls.Name = "TransactionSearchControl_TableLayout_Controls";
            TransactionSearchControl_TableLayout_Controls.RowCount = 3;
            TransactionSearchControl_TableLayout_Controls.RowStyles.Add(new RowStyle());
            TransactionSearchControl_TableLayout_Controls.RowStyles.Add(new RowStyle());
            TransactionSearchControl_TableLayout_Controls.RowStyles.Add(new RowStyle());
            TransactionSearchControl_TableLayout_Controls.Size = new Size(447, 163);
            TransactionSearchControl_TableLayout_Controls.TabIndex = 0;
            //
            // TransactionSearchControl_GroupBox_TransactionTypes
            //
            TransactionSearchControl_GroupBox_TransactionTypes.AutoSize = true;
            TransactionSearchControl_GroupBox_TransactionTypes.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_GroupBox_TransactionTypes.Controls.Add(TransactionSearchControl_TableLayout_TransactionTypes);
            TransactionSearchControl_GroupBox_TransactionTypes.Dock = DockStyle.Fill;
            TransactionSearchControl_GroupBox_TransactionTypes.Location = new Point(3, 113);
            TransactionSearchControl_GroupBox_TransactionTypes.Name = "TransactionSearchControl_GroupBox_TransactionTypes";
            TransactionSearchControl_GroupBox_TransactionTypes.Size = new Size(441, 47);
            TransactionSearchControl_GroupBox_TransactionTypes.TabIndex = 11;
            TransactionSearchControl_GroupBox_TransactionTypes.TabStop = false;
            TransactionSearchControl_GroupBox_TransactionTypes.Text = "Filter by Transaction Types";
            //
            // TransactionSearchControl_TableLayout_TransactionTypes
            //
            TransactionSearchControl_TableLayout_TransactionTypes.AutoSize = true;
            TransactionSearchControl_TableLayout_TransactionTypes.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_TableLayout_TransactionTypes.ColumnCount = 7;
            TransactionSearchControl_TableLayout_TransactionTypes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            TransactionSearchControl_TableLayout_TransactionTypes.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_TransactionTypes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            TransactionSearchControl_TableLayout_TransactionTypes.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_TransactionTypes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            TransactionSearchControl_TableLayout_TransactionTypes.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_TransactionTypes.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            TransactionSearchControl_TableLayout_TransactionTypes.Controls.Add(TransactionSearchControl_CheckBox_TRANSFER, 5, 0);
            TransactionSearchControl_TableLayout_TransactionTypes.Controls.Add(TransactionSearchControl_CheckBox_OUT, 3, 0);
            TransactionSearchControl_TableLayout_TransactionTypes.Controls.Add(TransactionSearchControl_CheckBox_IN, 1, 0);
            TransactionSearchControl_TableLayout_TransactionTypes.Dock = DockStyle.Fill;
            TransactionSearchControl_TableLayout_TransactionTypes.Location = new Point(3, 19);
            TransactionSearchControl_TableLayout_TransactionTypes.Name = "TransactionSearchControl_TableLayout_TransactionTypes";
            TransactionSearchControl_TableLayout_TransactionTypes.RowCount = 1;
            TransactionSearchControl_TableLayout_TransactionTypes.RowStyles.Add(new RowStyle());
            TransactionSearchControl_TableLayout_TransactionTypes.Size = new Size(435, 25);
            TransactionSearchControl_TableLayout_TransactionTypes.TabIndex = 0;
            //
            // TransactionSearchControl_CheckBox_TRANSFER
            //
            TransactionSearchControl_CheckBox_TRANSFER.AutoSize = true;
            TransactionSearchControl_CheckBox_TRANSFER.Checked = true;
            TransactionSearchControl_CheckBox_TRANSFER.CheckState = CheckState.Checked;
            TransactionSearchControl_CheckBox_TRANSFER.Dock = DockStyle.Fill;
            TransactionSearchControl_CheckBox_TRANSFER.Location = new Point(286, 3);
            TransactionSearchControl_CheckBox_TRANSFER.Name = "TransactionSearchControl_CheckBox_TRANSFER";
            TransactionSearchControl_CheckBox_TRANSFER.Size = new Size(82, 19);
            TransactionSearchControl_CheckBox_TRANSFER.TabIndex = 2;
            TransactionSearchControl_CheckBox_TRANSFER.Text = "TRANSFER";
            TransactionSearchControl_CheckBox_TRANSFER.UseVisualStyleBackColor = true;
            //
            // TransactionSearchControl_CheckBox_OUT
            //
            TransactionSearchControl_CheckBox_OUT.AutoSize = true;
            TransactionSearchControl_CheckBox_OUT.Checked = true;
            TransactionSearchControl_CheckBox_OUT.CheckState = CheckState.Checked;
            TransactionSearchControl_CheckBox_OUT.Dock = DockStyle.Fill;
            TransactionSearchControl_CheckBox_OUT.Location = new Point(169, 3);
            TransactionSearchControl_CheckBox_OUT.Name = "TransactionSearchControl_CheckBox_OUT";
            TransactionSearchControl_CheckBox_OUT.Size = new Size(50, 19);
            TransactionSearchControl_CheckBox_OUT.TabIndex = 1;
            TransactionSearchControl_CheckBox_OUT.Text = "OUT";
            TransactionSearchControl_CheckBox_OUT.UseVisualStyleBackColor = true;
            //
            // TransactionSearchControl_CheckBox_IN
            //
            TransactionSearchControl_CheckBox_IN.AutoSize = true;
            TransactionSearchControl_CheckBox_IN.Checked = true;
            TransactionSearchControl_CheckBox_IN.CheckState = CheckState.Checked;
            TransactionSearchControl_CheckBox_IN.Dock = DockStyle.Fill;
            TransactionSearchControl_CheckBox_IN.Location = new Point(64, 3);
            TransactionSearchControl_CheckBox_IN.Name = "TransactionSearchControl_CheckBox_IN";
            TransactionSearchControl_CheckBox_IN.Size = new Size(38, 19);
            TransactionSearchControl_CheckBox_IN.TabIndex = 0;
            TransactionSearchControl_CheckBox_IN.Text = "IN";
            TransactionSearchControl_CheckBox_IN.UseVisualStyleBackColor = true;
            //
            // TransactionSearchControl_GroupBox_DateRange
            //
            TransactionSearchControl_GroupBox_DateRange.AutoSize = true;
            TransactionSearchControl_GroupBox_DateRange.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_GroupBox_DateRange.Controls.Add(TransactionSearchControl_TableLayout_DateTimePicker);
            TransactionSearchControl_GroupBox_DateRange.Dock = DockStyle.Fill;
            TransactionSearchControl_GroupBox_DateRange.Location = new Point(3, 3);
            TransactionSearchControl_GroupBox_DateRange.Name = "TransactionSearchControl_GroupBox_DateRange";
            TransactionSearchControl_GroupBox_DateRange.Size = new Size(441, 51);
            TransactionSearchControl_GroupBox_DateRange.TabIndex = 17;
            TransactionSearchControl_GroupBox_DateRange.TabStop = false;
            TransactionSearchControl_GroupBox_DateRange.Text = "Select a Date Range (Custom Filter must be selected below)";
            //
            // TransactionSearchControl_TableLayout_DateTimePicker
            //
            TransactionSearchControl_TableLayout_DateTimePicker.AutoSize = true;
            TransactionSearchControl_TableLayout_DateTimePicker.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_TableLayout_DateTimePicker.ColumnCount = 4;
            TransactionSearchControl_TableLayout_DateTimePicker.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_DateTimePicker.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            TransactionSearchControl_TableLayout_DateTimePicker.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_DateTimePicker.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            TransactionSearchControl_TableLayout_DateTimePicker.Controls.Add(TransactionSearchControl_DateTimePicker_DateFrom, 1, 0);
            TransactionSearchControl_TableLayout_DateTimePicker.Controls.Add(TransactionSearchControl_DateTimePicker_DateTo, 3, 0);
            TransactionSearchControl_TableLayout_DateTimePicker.Controls.Add(TransactionSearchControl_Label_DateFrom, 0, 0);
            TransactionSearchControl_TableLayout_DateTimePicker.Controls.Add(TransactionSearchControl_Label_DateTo, 2, 0);
            TransactionSearchControl_TableLayout_DateTimePicker.Dock = DockStyle.Fill;
            TransactionSearchControl_TableLayout_DateTimePicker.Location = new Point(3, 19);
            TransactionSearchControl_TableLayout_DateTimePicker.Name = "TransactionSearchControl_TableLayout_DateTimePicker";
            TransactionSearchControl_TableLayout_DateTimePicker.RowCount = 1;
            TransactionSearchControl_TableLayout_DateTimePicker.RowStyles.Add(new RowStyle());
            TransactionSearchControl_TableLayout_DateTimePicker.Size = new Size(435, 29);
            TransactionSearchControl_TableLayout_DateTimePicker.TabIndex = 17;
            //
            // TransactionSearchControl_DateTimePicker_DateFrom
            //
            TransactionSearchControl_DateTimePicker_DateFrom.Dock = DockStyle.Fill;
            TransactionSearchControl_DateTimePicker_DateFrom.Format = DateTimePickerFormat.Short;
            TransactionSearchControl_DateTimePicker_DateFrom.Location = new Point(47, 3);
            TransactionSearchControl_DateTimePicker_DateFrom.MinimumSize = new Size(175, 23);
            TransactionSearchControl_DateTimePicker_DateFrom.Name = "TransactionSearchControl_DateTimePicker_DateFrom";
            TransactionSearchControl_DateTimePicker_DateFrom.Size = new Size(175, 23);
            TransactionSearchControl_DateTimePicker_DateFrom.TabIndex = 2;
            //
            // TransactionSearchControl_DateTimePicker_DateTo
            //
            TransactionSearchControl_DateTimePicker_DateTo.Dock = DockStyle.Fill;
            TransactionSearchControl_DateTimePicker_DateTo.Format = DateTimePickerFormat.Short;
            TransactionSearchControl_DateTimePicker_DateTo.Location = new Point(257, 3);
            TransactionSearchControl_DateTimePicker_DateTo.MinimumSize = new Size(175, 23);
            TransactionSearchControl_DateTimePicker_DateTo.Name = "TransactionSearchControl_DateTimePicker_DateTo";
            TransactionSearchControl_DateTimePicker_DateTo.Size = new Size(175, 23);
            TransactionSearchControl_DateTimePicker_DateTo.TabIndex = 4;
            //
            // TransactionSearchControl_Label_DateFrom
            //
            TransactionSearchControl_Label_DateFrom.AutoSize = true;
            TransactionSearchControl_Label_DateFrom.Dock = DockStyle.Fill;
            TransactionSearchControl_Label_DateFrom.Location = new Point(3, 3);
            TransactionSearchControl_Label_DateFrom.Margin = new Padding(3);
            TransactionSearchControl_Label_DateFrom.Name = "TransactionSearchControl_Label_DateFrom";
            TransactionSearchControl_Label_DateFrom.Size = new Size(38, 23);
            TransactionSearchControl_Label_DateFrom.TabIndex = 1;
            TransactionSearchControl_Label_DateFrom.Text = "From:";
            TransactionSearchControl_Label_DateFrom.TextAlign = ContentAlignment.MiddleLeft;
            //
            // TransactionSearchControl_Label_DateTo
            //
            TransactionSearchControl_Label_DateTo.AutoSize = true;
            TransactionSearchControl_Label_DateTo.Dock = DockStyle.Fill;
            TransactionSearchControl_Label_DateTo.Location = new Point(228, 3);
            TransactionSearchControl_Label_DateTo.Margin = new Padding(3);
            TransactionSearchControl_Label_DateTo.Name = "TransactionSearchControl_Label_DateTo";
            TransactionSearchControl_Label_DateTo.Size = new Size(23, 23);
            TransactionSearchControl_Label_DateTo.TabIndex = 3;
            TransactionSearchControl_Label_DateTo.Text = "To:";
            TransactionSearchControl_Label_DateTo.TextAlign = ContentAlignment.MiddleRight;
            //
            // TransactionSearchControl_GroupBox_RadioButtons
            //
            TransactionSearchControl_GroupBox_RadioButtons.AutoSize = true;
            TransactionSearchControl_GroupBox_RadioButtons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_GroupBox_RadioButtons.Controls.Add(TransactionSearchControl_TableLayout_QuickFilters);
            TransactionSearchControl_GroupBox_RadioButtons.Dock = DockStyle.Fill;
            TransactionSearchControl_GroupBox_RadioButtons.Location = new Point(3, 60);
            TransactionSearchControl_GroupBox_RadioButtons.Name = "TransactionSearchControl_GroupBox_RadioButtons";
            TransactionSearchControl_GroupBox_RadioButtons.Size = new Size(441, 47);
            TransactionSearchControl_GroupBox_RadioButtons.TabIndex = 18;
            TransactionSearchControl_GroupBox_RadioButtons.TabStop = false;
            TransactionSearchControl_GroupBox_RadioButtons.Text = "Simple Date Filter";
            //
            // TransactionSearchControl_TableLayout_QuickFilters
            //
            TransactionSearchControl_TableLayout_QuickFilters.AutoSize = true;
            TransactionSearchControl_TableLayout_QuickFilters.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_TableLayout_QuickFilters.ColumnCount = 11;
            TransactionSearchControl_TableLayout_QuickFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6666718F));
            TransactionSearchControl_TableLayout_QuickFilters.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_QuickFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6666679F));
            TransactionSearchControl_TableLayout_QuickFilters.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_QuickFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6666679F));
            TransactionSearchControl_TableLayout_QuickFilters.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_QuickFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6666679F));
            TransactionSearchControl_TableLayout_QuickFilters.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_QuickFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6666679F));
            TransactionSearchControl_TableLayout_QuickFilters.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_QuickFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6666679F));
            TransactionSearchControl_TableLayout_QuickFilters.Controls.Add(TransactionSearchControl_RadioButton_Custom, 7, 0);
            TransactionSearchControl_TableLayout_QuickFilters.Controls.Add(TransactionSearchControl_RadioButton_Month, 5, 0);
            TransactionSearchControl_TableLayout_QuickFilters.Controls.Add(TransactionSearchControl_RadioButton_Week, 3, 0);
            TransactionSearchControl_TableLayout_QuickFilters.Controls.Add(TransactionSearchControl_RadioButton_Today, 1, 0);
            TransactionSearchControl_TableLayout_QuickFilters.Controls.Add(TransactionSearchControl_RadioButton_Everything, 9, 0);
            TransactionSearchControl_TableLayout_QuickFilters.Dock = DockStyle.Fill;
            TransactionSearchControl_TableLayout_QuickFilters.Location = new Point(3, 19);
            TransactionSearchControl_TableLayout_QuickFilters.Name = "TransactionSearchControl_TableLayout_QuickFilters";
            TransactionSearchControl_TableLayout_QuickFilters.RowCount = 1;
            TransactionSearchControl_TableLayout_QuickFilters.RowStyles.Add(new RowStyle());
            TransactionSearchControl_TableLayout_QuickFilters.Size = new Size(435, 25);
            TransactionSearchControl_TableLayout_QuickFilters.TabIndex = 16;
            //
            // TransactionSearchControl_RadioButton_Custom
            //
            TransactionSearchControl_RadioButton_Custom.AutoSize = true;
            TransactionSearchControl_RadioButton_Custom.Dock = DockStyle.Fill;
            TransactionSearchControl_RadioButton_Custom.Location = new Point(249, 3);
            TransactionSearchControl_RadioButton_Custom.Name = "TransactionSearchControl_RadioButton_Custom";
            TransactionSearchControl_RadioButton_Custom.Size = new Size(67, 19);
            TransactionSearchControl_RadioButton_Custom.TabIndex = 3;
            TransactionSearchControl_RadioButton_Custom.Text = "Custom";
            TransactionSearchControl_RadioButton_Custom.UseVisualStyleBackColor = true;
            //
            // TransactionSearchControl_RadioButton_Month
            //
            TransactionSearchControl_RadioButton_Month.AutoSize = true;
            TransactionSearchControl_RadioButton_Month.Checked = true;
            TransactionSearchControl_RadioButton_Month.Dock = DockStyle.Fill;
            TransactionSearchControl_RadioButton_Month.Location = new Point(168, 3);
            TransactionSearchControl_RadioButton_Month.Name = "TransactionSearchControl_RadioButton_Month";
            TransactionSearchControl_RadioButton_Month.Size = new Size(61, 19);
            TransactionSearchControl_RadioButton_Month.TabIndex = 2;
            TransactionSearchControl_RadioButton_Month.TabStop = true;
            TransactionSearchControl_RadioButton_Month.Text = "Month";
            TransactionSearchControl_RadioButton_Month.UseVisualStyleBackColor = true;
            //
            // TransactionSearchControl_RadioButton_Week
            //
            TransactionSearchControl_RadioButton_Week.AutoSize = true;
            TransactionSearchControl_RadioButton_Week.Dock = DockStyle.Fill;
            TransactionSearchControl_RadioButton_Week.Location = new Point(94, 3);
            TransactionSearchControl_RadioButton_Week.Name = "TransactionSearchControl_RadioButton_Week";
            TransactionSearchControl_RadioButton_Week.Size = new Size(54, 19);
            TransactionSearchControl_RadioButton_Week.TabIndex = 1;
            TransactionSearchControl_RadioButton_Week.Text = "Week";
            TransactionSearchControl_RadioButton_Week.UseVisualStyleBackColor = true;
            //
            // TransactionSearchControl_RadioButton_Today
            //
            TransactionSearchControl_RadioButton_Today.AutoSize = true;
            TransactionSearchControl_RadioButton_Today.Dock = DockStyle.Fill;
            TransactionSearchControl_RadioButton_Today.Location = new Point(17, 3);
            TransactionSearchControl_RadioButton_Today.Name = "TransactionSearchControl_RadioButton_Today";
            TransactionSearchControl_RadioButton_Today.Size = new Size(57, 19);
            TransactionSearchControl_RadioButton_Today.TabIndex = 0;
            TransactionSearchControl_RadioButton_Today.Text = "Today";
            TransactionSearchControl_RadioButton_Today.UseVisualStyleBackColor = true;
            //
            // TransactionSearchControl_RadioButton_Everything
            //
            TransactionSearchControl_RadioButton_Everything.AutoSize = true;
            TransactionSearchControl_RadioButton_Everything.Dock = DockStyle.Fill;
            TransactionSearchControl_RadioButton_Everything.Location = new Point(336, 3);
            TransactionSearchControl_RadioButton_Everything.Name = "TransactionSearchControl_RadioButton_Everything";
            TransactionSearchControl_RadioButton_Everything.Size = new Size(81, 19);
            TransactionSearchControl_RadioButton_Everything.TabIndex = 4;
            TransactionSearchControl_RadioButton_Everything.Text = "Everything";
            TransactionSearchControl_RadioButton_Everything.UseVisualStyleBackColor = true;
            //
            // TransactionSearchControl_GroupBox_Search
            //
            TransactionSearchControl_GroupBox_Search.AutoSize = true;
            TransactionSearchControl_GroupBox_Search.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_TableLayout_Filters.SetColumnSpan(TransactionSearchControl_GroupBox_Search, 3);
            TransactionSearchControl_GroupBox_Search.Controls.Add(TransactionSearchControl_TableLayout_Search);
            TransactionSearchControl_GroupBox_Search.Dock = DockStyle.Fill;
            TransactionSearchControl_GroupBox_Search.Location = new Point(5, 5);
            TransactionSearchControl_GroupBox_Search.Name = "TransactionSearchControl_GroupBox_Search";
            TransactionSearchControl_GroupBox_Search.Size = new Size(567, 157);
            TransactionSearchControl_GroupBox_Search.TabIndex = 20;
            TransactionSearchControl_GroupBox_Search.TabStop = false;
            TransactionSearchControl_GroupBox_Search.Text = "Step 1: Enter Search Criteria";
            //
            // TransactionSearchControl_TableLayout_Search
            //
            TransactionSearchControl_TableLayout_Search.AutoSize = true;
            TransactionSearchControl_TableLayout_Search.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_TableLayout_Search.ColumnCount = 3;
            TransactionSearchControl_TableLayout_Search.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_Search.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_Search.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_Search.Controls.Add(TransactionSearchControl_Suggestion_PartNumber, 0, 0);
            TransactionSearchControl_TableLayout_Search.Controls.Add(TransactionSearchControl_Suggestion_User, 1, 0);
            TransactionSearchControl_TableLayout_Search.Controls.Add(TransactionSearchControl_Suggestion_Operation, 2, 0);
            TransactionSearchControl_TableLayout_Search.Controls.Add(TransactionSearchControl_Suggestion_FromLocation, 0, 1);
            TransactionSearchControl_TableLayout_Search.Controls.Add(TransactionSearchControl_Suggestion_ToLocation, 1, 1);
            TransactionSearchControl_TableLayout_Search.Controls.Add(TransactionSearchControl_Suggestion_Notes, 2, 1);
            TransactionSearchControl_TableLayout_Search.Dock = DockStyle.Fill;
            TransactionSearchControl_TableLayout_Search.Location = new Point(3, 19);
            TransactionSearchControl_TableLayout_Search.Name = "TransactionSearchControl_TableLayout_Search";
            TransactionSearchControl_TableLayout_Search.RowCount = 2;
            TransactionSearchControl_TableLayout_Search.RowStyles.Add(new RowStyle());
            TransactionSearchControl_TableLayout_Search.RowStyles.Add(new RowStyle());
            TransactionSearchControl_TableLayout_Search.Size = new Size(561, 116);
            TransactionSearchControl_TableLayout_Search.TabIndex = 0;
            //
            // TransactionSearchControl_Suggestion_PartNumber
            //
            TransactionSearchControl_Suggestion_PartNumber.AutoSize = true;
            TransactionSearchControl_Suggestion_PartNumber.Dock = DockStyle.Fill;
            TransactionSearchControl_Suggestion_PartNumber.LabelText = "🔍 Part Number";
            TransactionSearchControl_Suggestion_PartNumber.Location = new Point(3, 3);
            TransactionSearchControl_Suggestion_PartNumber.Margin = new Padding(3);
            TransactionSearchControl_Suggestion_PartNumber.MinimumSize = new Size(175, 48);
            TransactionSearchControl_Suggestion_PartNumber.Name = "TransactionSearchControl_Suggestion_PartNumber";
            TransactionSearchControl_Suggestion_PartNumber.PlaceholderText = "Enter or select part";
            TransactionSearchControl_Suggestion_PartNumber.SetF4ButtonTabStop(false);
            TransactionSearchControl_Suggestion_PartNumber.Size = new Size(181, 52);
            TransactionSearchControl_Suggestion_PartNumber.TabIndex = 1;
            //
            // TransactionSearchControl_Suggestion_User
            //
            TransactionSearchControl_Suggestion_User.AutoSize = true;
            TransactionSearchControl_Suggestion_User.Dock = DockStyle.Fill;
            TransactionSearchControl_Suggestion_User.LabelText = "👤 User";
            TransactionSearchControl_Suggestion_User.Location = new Point(190, 3);
            TransactionSearchControl_Suggestion_User.Margin = new Padding(3);
            TransactionSearchControl_Suggestion_User.MinimumSize = new Size(175, 48);
            TransactionSearchControl_Suggestion_User.Name = "TransactionSearchControl_Suggestion_User";
            TransactionSearchControl_Suggestion_User.PlaceholderText = "Leave blank for all users";
            TransactionSearchControl_Suggestion_User.SetF4ButtonTabStop(false);
            TransactionSearchControl_Suggestion_User.Size = new Size(181, 52);
            TransactionSearchControl_Suggestion_User.TabIndex = 2;
            //
            // TransactionSearchControl_Suggestion_Operation
            //
            TransactionSearchControl_Suggestion_Operation.AutoSize = true;
            TransactionSearchControl_Suggestion_Operation.Dock = DockStyle.Fill;
            TransactionSearchControl_Suggestion_Operation.LabelText = "⚙️ Operation";
            TransactionSearchControl_Suggestion_Operation.Location = new Point(377, 3);
            TransactionSearchControl_Suggestion_Operation.Margin = new Padding(3);
            TransactionSearchControl_Suggestion_Operation.MinimumSize = new Size(175, 48);
            TransactionSearchControl_Suggestion_Operation.Name = "TransactionSearchControl_Suggestion_Operation";
            TransactionSearchControl_Suggestion_Operation.PlaceholderText = "e.g., 90, 100";
            TransactionSearchControl_Suggestion_Operation.SetF4ButtonTabStop(false);
            TransactionSearchControl_Suggestion_Operation.Size = new Size(181, 52);
            TransactionSearchControl_Suggestion_Operation.TabIndex = 3;
            //
            // TransactionSearchControl_Suggestion_FromLocation
            //
            TransactionSearchControl_Suggestion_FromLocation.AutoSize = true;
            TransactionSearchControl_Suggestion_FromLocation.Dock = DockStyle.Fill;
            TransactionSearchControl_Suggestion_FromLocation.LabelText = "📍 From Location";
            TransactionSearchControl_Suggestion_FromLocation.Location = new Point(3, 61);
            TransactionSearchControl_Suggestion_FromLocation.Margin = new Padding(3);
            TransactionSearchControl_Suggestion_FromLocation.MinimumSize = new Size(175, 48);
            TransactionSearchControl_Suggestion_FromLocation.Name = "TransactionSearchControl_Suggestion_FromLocation";
            TransactionSearchControl_Suggestion_FromLocation.PlaceholderText = "Optional filter";
            TransactionSearchControl_Suggestion_FromLocation.SetF4ButtonTabStop(false);
            TransactionSearchControl_Suggestion_FromLocation.Size = new Size(181, 52);
            TransactionSearchControl_Suggestion_FromLocation.TabIndex = 4;
            //
            // TransactionSearchControl_Suggestion_ToLocation
            //
            TransactionSearchControl_Suggestion_ToLocation.AutoSize = true;
            TransactionSearchControl_Suggestion_ToLocation.Dock = DockStyle.Fill;
            TransactionSearchControl_Suggestion_ToLocation.LabelText = "📍 To Location";
            TransactionSearchControl_Suggestion_ToLocation.Location = new Point(190, 61);
            TransactionSearchControl_Suggestion_ToLocation.Margin = new Padding(3);
            TransactionSearchControl_Suggestion_ToLocation.MinimumSize = new Size(175, 48);
            TransactionSearchControl_Suggestion_ToLocation.Name = "TransactionSearchControl_Suggestion_ToLocation";
            TransactionSearchControl_Suggestion_ToLocation.PlaceholderText = "Optional filter";
            TransactionSearchControl_Suggestion_ToLocation.SetF4ButtonTabStop(false);
            TransactionSearchControl_Suggestion_ToLocation.Size = new Size(181, 52);
            TransactionSearchControl_Suggestion_ToLocation.TabIndex = 5;
            //
            // TransactionSearchControl_Suggestion_Notes
            //
            TransactionSearchControl_Suggestion_Notes.AutoSize = true;
            TransactionSearchControl_Suggestion_Notes.Dock = DockStyle.Fill;
            TransactionSearchControl_Suggestion_Notes.EnableSuggestions = false;
            TransactionSearchControl_Suggestion_Notes.LabelText = "📝 Notes Keyword";
            TransactionSearchControl_Suggestion_Notes.Location = new Point(377, 61);
            TransactionSearchControl_Suggestion_Notes.Margin = new Padding(3);
            TransactionSearchControl_Suggestion_Notes.MinimumSize = new Size(175, 48);
            TransactionSearchControl_Suggestion_Notes.Name = "TransactionSearchControl_Suggestion_Notes";
            TransactionSearchControl_Suggestion_Notes.PlaceholderText = "Partial match supported";
            TransactionSearchControl_Suggestion_Notes.SetF4ButtonTabStop(false);
            TransactionSearchControl_Suggestion_Notes.ShowF4Button = false;
            TransactionSearchControl_Suggestion_Notes.ShowValidationColor = false;
            TransactionSearchControl_Suggestion_Notes.Size = new Size(181, 52);
            TransactionSearchControl_Suggestion_Notes.TabIndex = 6;
            //
            // TransactionSearchControl_Panel_Buttons
            //
            TransactionSearchControl_Panel_Buttons.AutoSize = true;
            TransactionSearchControl_Panel_Buttons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_Panel_Buttons.Controls.Add(TransactionSearchControl_TableLayout_Buttons);
            TransactionSearchControl_Panel_Buttons.Dock = DockStyle.Fill;
            TransactionSearchControl_Panel_Buttons.Location = new Point(1025, 5);
            TransactionSearchControl_Panel_Buttons.Name = "TransactionSearchControl_Panel_Buttons";
            TransactionSearchControl_Panel_Buttons.Size = new Size(52, 157);
            TransactionSearchControl_Panel_Buttons.TabIndex = 20;
            //
            // TransactionSearchControl_TableLayout_Buttons
            //
            TransactionSearchControl_TableLayout_Buttons.AutoSize = true;
            TransactionSearchControl_TableLayout_Buttons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_TableLayout_Buttons.ColumnCount = 1;
            TransactionSearchControl_TableLayout_Buttons.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_Buttons.Controls.Add(TransactionSearchControl_Button_Search, 0, 1);
            TransactionSearchControl_TableLayout_Buttons.Controls.Add(TransactionSearchControl_Button_Reset, 0, 3);
            TransactionSearchControl_TableLayout_Buttons.Dock = DockStyle.Fill;
            TransactionSearchControl_TableLayout_Buttons.Location = new Point(0, 0);
            TransactionSearchControl_TableLayout_Buttons.Name = "TransactionSearchControl_TableLayout_Buttons";
            TransactionSearchControl_TableLayout_Buttons.Padding = new Padding(3);
            TransactionSearchControl_TableLayout_Buttons.RowCount = 5;
            TransactionSearchControl_TableLayout_Buttons.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            TransactionSearchControl_TableLayout_Buttons.RowStyles.Add(new RowStyle());
            TransactionSearchControl_TableLayout_Buttons.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            TransactionSearchControl_TableLayout_Buttons.RowStyles.Add(new RowStyle());
            TransactionSearchControl_TableLayout_Buttons.RowStyles.Add(new RowStyle(SizeType.Percent, 33.34F));
            TransactionSearchControl_TableLayout_Buttons.Size = new Size(52, 157);
            TransactionSearchControl_TableLayout_Buttons.TabIndex = 0;
            //
            // TransactionSearchControl_Button_Search
            //
            TransactionSearchControl_Button_Search.AutoSize = true;
            TransactionSearchControl_Button_Search.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_Button_Search.BackColor = Color.FromArgb(59, 130, 246);
            TransactionSearchControl_Button_Search.FlatStyle = FlatStyle.Flat;
            TransactionSearchControl_Button_Search.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            TransactionSearchControl_Button_Search.ForeColor = Color.White;
            TransactionSearchControl_Button_Search.Location = new Point(6, 20);
            TransactionSearchControl_Button_Search.MaximumSize = new Size(40, 40);
            TransactionSearchControl_Button_Search.MinimumSize = new Size(40, 40);
            TransactionSearchControl_Button_Search.Name = "TransactionSearchControl_Button_Search";
            TransactionSearchControl_Button_Search.Size = new Size(40, 40);
            TransactionSearchControl_Button_Search.TabIndex = 10;
            TransactionSearchControl_Button_Search.Text = "🔎";
            TransactionSearchControl_Button_Search.UseVisualStyleBackColor = false;
            //
            // TransactionSearchControl_Button_Reset
            //
            TransactionSearchControl_Button_Reset.AutoSize = true;
            TransactionSearchControl_Button_Reset.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_Button_Reset.BackColor = Color.FromArgb(226, 232, 240);
            TransactionSearchControl_Button_Reset.FlatStyle = FlatStyle.Flat;
            TransactionSearchControl_Button_Reset.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            TransactionSearchControl_Button_Reset.Location = new Point(6, 95);
            TransactionSearchControl_Button_Reset.MaximumSize = new Size(40, 40);
            TransactionSearchControl_Button_Reset.MinimumSize = new Size(40, 40);
            TransactionSearchControl_Button_Reset.Name = "TransactionSearchControl_Button_Reset";
            TransactionSearchControl_Button_Reset.Size = new Size(40, 40);
            TransactionSearchControl_Button_Reset.TabIndex = 15;
            TransactionSearchControl_Button_Reset.Text = "🔄";
            TransactionSearchControl_Button_Reset.UseVisualStyleBackColor = false;
            //
            // TransactionSearchControl
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(TransactionSearchControl_TableLayout_Main);
            Margin = new Padding(0);
            Name = "TransactionSearchControl";
            Size = new Size(1082, 167);
            TransactionSearchControl_TableLayout_Main.ResumeLayout(false);
            TransactionSearchControl_TableLayout_Main.PerformLayout();
            TransactionSearchControl_TableLayout_Filters.ResumeLayout(false);
            TransactionSearchControl_TableLayout_Filters.PerformLayout();
            TransactionSearchControl_TableLayout_Controls.ResumeLayout(false);
            TransactionSearchControl_TableLayout_Controls.PerformLayout();
            TransactionSearchControl_GroupBox_TransactionTypes.ResumeLayout(false);
            TransactionSearchControl_GroupBox_TransactionTypes.PerformLayout();
            TransactionSearchControl_TableLayout_TransactionTypes.ResumeLayout(false);
            TransactionSearchControl_TableLayout_TransactionTypes.PerformLayout();
            TransactionSearchControl_GroupBox_DateRange.ResumeLayout(false);
            TransactionSearchControl_GroupBox_DateRange.PerformLayout();
            TransactionSearchControl_TableLayout_DateTimePicker.ResumeLayout(false);
            TransactionSearchControl_TableLayout_DateTimePicker.PerformLayout();
            TransactionSearchControl_GroupBox_RadioButtons.ResumeLayout(false);
            TransactionSearchControl_GroupBox_RadioButtons.PerformLayout();
            TransactionSearchControl_TableLayout_QuickFilters.ResumeLayout(false);
            TransactionSearchControl_TableLayout_QuickFilters.PerformLayout();
            TransactionSearchControl_GroupBox_Search.ResumeLayout(false);
            TransactionSearchControl_GroupBox_Search.PerformLayout();
            TransactionSearchControl_TableLayout_Search.ResumeLayout(false);
            TransactionSearchControl_TableLayout_Search.PerformLayout();
            TransactionSearchControl_Panel_Buttons.ResumeLayout(false);
            TransactionSearchControl_Panel_Buttons.PerformLayout();
            TransactionSearchControl_TableLayout_Buttons.ResumeLayout(false);
            TransactionSearchControl_TableLayout_Buttons.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel TransactionSearchControl_TableLayout_Main;
        private TableLayoutPanel TransactionSearchControl_TableLayout_Filters;
        private TableLayoutPanel TransactionSearchControl_TableLayout_Controls;
        private GroupBox TransactionSearchControl_GroupBox_TransactionTypes;
        private TableLayoutPanel TransactionSearchControl_TableLayout_TransactionTypes;
        private CheckBox TransactionSearchControl_CheckBox_TRANSFER;
        private CheckBox TransactionSearchControl_CheckBox_OUT;
        private CheckBox TransactionSearchControl_CheckBox_IN;
        private GroupBox TransactionSearchControl_GroupBox_DateRange;
        private TableLayoutPanel TransactionSearchControl_TableLayout_DateTimePicker;
        private DateTimePicker TransactionSearchControl_DateTimePicker_DateFrom;
        private DateTimePicker TransactionSearchControl_DateTimePicker_DateTo;
        private Label TransactionSearchControl_Label_DateFrom;
        private Label TransactionSearchControl_Label_DateTo;
        private GroupBox TransactionSearchControl_GroupBox_RadioButtons;
        private TableLayoutPanel TransactionSearchControl_TableLayout_QuickFilters;
        private RadioButton TransactionSearchControl_RadioButton_Custom;
        private RadioButton TransactionSearchControl_RadioButton_Month;
        private RadioButton TransactionSearchControl_RadioButton_Week;
        private RadioButton TransactionSearchControl_RadioButton_Today;
        private RadioButton TransactionSearchControl_RadioButton_Everything;
        private GroupBox TransactionSearchControl_GroupBox_Search;
        private TableLayoutPanel TransactionSearchControl_TableLayout_Search;
        private SuggestionTextBoxWithLabel TransactionSearchControl_Suggestion_PartNumber;
        private SuggestionTextBoxWithLabel TransactionSearchControl_Suggestion_User;
        private SuggestionTextBoxWithLabel TransactionSearchControl_Suggestion_Operation;
        private SuggestionTextBoxWithLabel TransactionSearchControl_Suggestion_FromLocation;
        private SuggestionTextBoxWithLabel TransactionSearchControl_Suggestion_ToLocation;
        private SuggestionTextBoxWithLabel TransactionSearchControl_Suggestion_Notes;
        private Panel TransactionSearchControl_Panel_Buttons;
        private TableLayoutPanel TransactionSearchControl_TableLayout_Buttons;
        private Button TransactionSearchControl_Button_Search;
        private Button TransactionSearchControl_Button_Reset;
    }
}

