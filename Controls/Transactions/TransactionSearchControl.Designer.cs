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
            TransactionSearchControl_Button_Export = new Button();
            TransactionSearchControl_Button_InfoPanel = new Button();
            TransactionSearchControl_Button_Print = new Button();
            TransactionSearchControl_Button_Reset = new Button();
            TransactionSearchControl_Button_Search = new Button();
            TransactionSearchControl_CheckBox_IN = new CheckBox();
            TransactionSearchControl_CheckBox_OUT = new CheckBox();
            TransactionSearchControl_CheckBox_TRANSFER = new CheckBox();
            TransactionSearchControl_DateTimePicker_DateFrom = new DateTimePicker();
            TransactionSearchControl_DateTimePicker_DateTo = new DateTimePicker();
            TransactionSearchControl_GroupBox_RadioButtons = new GroupBox();
            TransactionSearchControl_TableLayout_QuickFilters = new TableLayoutPanel();
            TransactionSearchControl_RadioButton_Custom = new RadioButton();
            TransactionSearchControl_RadioButton_Month = new RadioButton();
            TransactionSearchControl_RadioButton_Week = new RadioButton();
            TransactionSearchControl_RadioButton_Today = new RadioButton();
            TransactionSearchControl_RadioButton_Everything = new RadioButton();
            TransactionSearchControl_GroupBox_Right = new GroupBox();
            TransactionSearchControl_TableLayout_Right = new TableLayoutPanel();
            TransactionSearchControl_TableLayout_DateToFrom = new TableLayoutPanel();
            TransactionSearchControl_Label_DateFrom = new Label();
            TransactionSearchControl_Label_DateTo = new Label();
            TransactionSearchControl_TableLayout_Options = new TableLayoutPanel();
            TransactionSearchControl_GroupBox_TransactionTypes = new GroupBox();
            TransactionSearchControl_TableLayout_TransactionTypes = new TableLayoutPanel();
            TransactionSearchControl_GroupBox_Search = new GroupBox();
            TransactionSearchControl_TableLayout_Left = new TableLayoutPanel();
            TransactionSearchControl_TableLayout_Search = new TableLayoutPanel();
            TransactionSearchControl_Suggestion_Operation = new SuggestionTextBoxWithLabel();
            TransactionSearchControl_Suggestion_User = new SuggestionTextBoxWithLabel();
            TransactionSearchControl_Suggestion_FromLocation = new SuggestionTextBoxWithLabel();
            TransactionSearchControl_Suggestion_Notes = new SuggestionTextBoxWithLabel();
            TransactionSearchControl_Suggestion_PartNumber = new SuggestionTextBoxWithLabel();
            TransactionSearchControl_Suggestion_ToLocation = new SuggestionTextBoxWithLabel();
            TransactionSearchControl_Panel_Buttons = new Panel();
            TransactionSearchControl_TableLayoutPanel_LeftLow = new TableLayoutPanel();
            TransactionSearchControl_TableLayout_Buttons = new TableLayoutPanel();
            TransactionSearchControl_TableLayout_Filters = new TableLayoutPanel();
            TransactionSearchControl_GroupBox_RadioButtons.SuspendLayout();
            TransactionSearchControl_TableLayout_QuickFilters.SuspendLayout();
            TransactionSearchControl_GroupBox_Right.SuspendLayout();
            TransactionSearchControl_TableLayout_Right.SuspendLayout();
            TransactionSearchControl_TableLayout_DateToFrom.SuspendLayout();
            TransactionSearchControl_TableLayout_Options.SuspendLayout();
            TransactionSearchControl_GroupBox_TransactionTypes.SuspendLayout();
            TransactionSearchControl_TableLayout_TransactionTypes.SuspendLayout();
            TransactionSearchControl_GroupBox_Search.SuspendLayout();
            TransactionSearchControl_TableLayout_Left.SuspendLayout();
            TransactionSearchControl_TableLayout_Search.SuspendLayout();
            TransactionSearchControl_Panel_Buttons.SuspendLayout();
            TransactionSearchControl_TableLayoutPanel_LeftLow.SuspendLayout();
            TransactionSearchControl_TableLayout_Buttons.SuspendLayout();
            TransactionSearchControl_TableLayout_Filters.SuspendLayout();
            SuspendLayout();
            // 
            // TransactionSearchControl_Button_Export
            // 
            TransactionSearchControl_Button_Export.AutoSize = true;
            TransactionSearchControl_Button_Export.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_Button_Export.BackColor = Color.LightCyan;
            TransactionSearchControl_Button_Export.FlatStyle = FlatStyle.Flat;
            TransactionSearchControl_Button_Export.Font = new Font("Segoe UI Emoji", 11.25F, FontStyle.Bold);
            TransactionSearchControl_Button_Export.ForeColor = Color.Black;
            TransactionSearchControl_Button_Export.Location = new Point(175, 3);
            TransactionSearchControl_Button_Export.MaximumSize = new Size(40, 40);
            TransactionSearchControl_Button_Export.MinimumSize = new Size(40, 40);
            TransactionSearchControl_Button_Export.Name = "TransactionSearchControl_Button_Export";
            TransactionSearchControl_Button_Export.Size = new Size(40, 40);
            TransactionSearchControl_Button_Export.TabIndex = 17;
            TransactionSearchControl_Button_Export.Text = "📊";
            TransactionSearchControl_Button_Export.UseVisualStyleBackColor = false;
            // 
            // TransactionSearchControl_Button_InfoPanel
            // 
            TransactionSearchControl_Button_InfoPanel.AutoSize = true;
            TransactionSearchControl_Button_InfoPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_Button_InfoPanel.BackColor = Color.FromArgb(248, 250, 252);
            TransactionSearchControl_Button_InfoPanel.FlatStyle = FlatStyle.Flat;
            TransactionSearchControl_Button_InfoPanel.Font = new Font("Segoe UI Emoji", 11.25F, FontStyle.Bold);
            TransactionSearchControl_Button_InfoPanel.Location = new Point(558, 3);
            TransactionSearchControl_Button_InfoPanel.MaximumSize = new Size(80, 40);
            TransactionSearchControl_Button_InfoPanel.MinimumSize = new Size(80, 40);
            TransactionSearchControl_Button_InfoPanel.Name = "TransactionSearchControl_Button_InfoPanel";
            TransactionSearchControl_Button_InfoPanel.Size = new Size(80, 40);
            TransactionSearchControl_Button_InfoPanel.TabIndex = 16;
            TransactionSearchControl_Button_InfoPanel.UseVisualStyleBackColor = false;
            // 
            // TransactionSearchControl_Button_Print
            // 
            TransactionSearchControl_Button_Print.AutoSize = true;
            TransactionSearchControl_Button_Print.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_Button_Print.BackColor = Color.LightCyan;
            TransactionSearchControl_Button_Print.FlatStyle = FlatStyle.Flat;
            TransactionSearchControl_Button_Print.Font = new Font("Segoe UI Emoji", 11.25F, FontStyle.Bold);
            TransactionSearchControl_Button_Print.ForeColor = Color.Black;
            TransactionSearchControl_Button_Print.Location = new Point(109, 3);
            TransactionSearchControl_Button_Print.MaximumSize = new Size(40, 40);
            TransactionSearchControl_Button_Print.MinimumSize = new Size(40, 40);
            TransactionSearchControl_Button_Print.Name = "TransactionSearchControl_Button_Print";
            TransactionSearchControl_Button_Print.Size = new Size(40, 40);
            TransactionSearchControl_Button_Print.TabIndex = 18;
            TransactionSearchControl_Button_Print.Text = "🖨️";
            TransactionSearchControl_Button_Print.UseVisualStyleBackColor = false;
            // 
            // TransactionSearchControl_Button_Reset
            // 
            TransactionSearchControl_Button_Reset.AutoSize = true;
            TransactionSearchControl_Button_Reset.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_Button_Reset.BackColor = Color.RosyBrown;
            TransactionSearchControl_Button_Reset.FlatStyle = FlatStyle.Flat;
            TransactionSearchControl_Button_Reset.Font = new Font("Segoe UI Emoji", 11.25F, FontStyle.Bold);
            TransactionSearchControl_Button_Reset.Location = new Point(241, 3);
            TransactionSearchControl_Button_Reset.MaximumSize = new Size(40, 40);
            TransactionSearchControl_Button_Reset.MinimumSize = new Size(40, 40);
            TransactionSearchControl_Button_Reset.Name = "TransactionSearchControl_Button_Reset";
            TransactionSearchControl_Button_Reset.Size = new Size(40, 40);
            TransactionSearchControl_Button_Reset.TabIndex = 8;
            TransactionSearchControl_Button_Reset.Text = "🔄";
            TransactionSearchControl_Button_Reset.UseVisualStyleBackColor = false;
            // 
            // TransactionSearchControl_Button_Search
            // 
            TransactionSearchControl_Button_Search.AutoSize = true;
            TransactionSearchControl_Button_Search.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_Button_Search.BackColor = Color.SteelBlue;
            TransactionSearchControl_Button_Search.FlatStyle = FlatStyle.Flat;
            TransactionSearchControl_Button_Search.Font = new Font("Segoe UI Emoji", 11.25F, FontStyle.Bold);
            TransactionSearchControl_Button_Search.ForeColor = Color.Black;
            TransactionSearchControl_Button_Search.Location = new Point(43, 3);
            TransactionSearchControl_Button_Search.MaximumSize = new Size(40, 40);
            TransactionSearchControl_Button_Search.MinimumSize = new Size(40, 40);
            TransactionSearchControl_Button_Search.Name = "TransactionSearchControl_Button_Search";
            TransactionSearchControl_Button_Search.Size = new Size(40, 40);
            TransactionSearchControl_Button_Search.TabIndex = 7;
            TransactionSearchControl_Button_Search.Text = "🔎";
            TransactionSearchControl_Button_Search.UseVisualStyleBackColor = false;
            // 
            // TransactionSearchControl_CheckBox_IN
            // 
            TransactionSearchControl_CheckBox_IN.AutoSize = true;
            TransactionSearchControl_CheckBox_IN.Checked = true;
            TransactionSearchControl_CheckBox_IN.CheckState = CheckState.Checked;
            TransactionSearchControl_CheckBox_IN.Dock = DockStyle.Fill;
            TransactionSearchControl_CheckBox_IN.Location = new Point(45, 3);
            TransactionSearchControl_CheckBox_IN.Name = "TransactionSearchControl_CheckBox_IN";
            TransactionSearchControl_CheckBox_IN.Size = new Size(38, 19);
            TransactionSearchControl_CheckBox_IN.TabIndex = 0;
            TransactionSearchControl_CheckBox_IN.Text = "IN";
            TransactionSearchControl_CheckBox_IN.UseVisualStyleBackColor = true;
            // 
            // TransactionSearchControl_CheckBox_OUT
            // 
            TransactionSearchControl_CheckBox_OUT.AutoSize = true;
            TransactionSearchControl_CheckBox_OUT.Checked = true;
            TransactionSearchControl_CheckBox_OUT.CheckState = CheckState.Checked;
            TransactionSearchControl_CheckBox_OUT.Dock = DockStyle.Fill;
            TransactionSearchControl_CheckBox_OUT.Location = new Point(131, 3);
            TransactionSearchControl_CheckBox_OUT.Name = "TransactionSearchControl_CheckBox_OUT";
            TransactionSearchControl_CheckBox_OUT.Size = new Size(50, 19);
            TransactionSearchControl_CheckBox_OUT.TabIndex = 1;
            TransactionSearchControl_CheckBox_OUT.Text = "OUT";
            TransactionSearchControl_CheckBox_OUT.UseVisualStyleBackColor = true;
            // 
            // TransactionSearchControl_CheckBox_TRANSFER
            // 
            TransactionSearchControl_CheckBox_TRANSFER.AutoSize = true;
            TransactionSearchControl_CheckBox_TRANSFER.Checked = true;
            TransactionSearchControl_CheckBox_TRANSFER.CheckState = CheckState.Checked;
            TransactionSearchControl_CheckBox_TRANSFER.Dock = DockStyle.Fill;
            TransactionSearchControl_CheckBox_TRANSFER.Location = new Point(229, 3);
            TransactionSearchControl_CheckBox_TRANSFER.Name = "TransactionSearchControl_CheckBox_TRANSFER";
            TransactionSearchControl_CheckBox_TRANSFER.Size = new Size(82, 19);
            TransactionSearchControl_CheckBox_TRANSFER.TabIndex = 2;
            TransactionSearchControl_CheckBox_TRANSFER.Text = "TRANSFER";
            TransactionSearchControl_CheckBox_TRANSFER.UseVisualStyleBackColor = true;
            // 
            // TransactionSearchControl_DateTimePicker_DateFrom
            // 
            TransactionSearchControl_DateTimePicker_DateFrom.Dock = DockStyle.Fill;
            TransactionSearchControl_DateTimePicker_DateFrom.Format = DateTimePickerFormat.Short;
            TransactionSearchControl_DateTimePicker_DateFrom.Location = new Point(49, 4);
            TransactionSearchControl_DateTimePicker_DateFrom.Name = "TransactionSearchControl_DateTimePicker_DateFrom";
            TransactionSearchControl_DateTimePicker_DateFrom.Size = new Size(316, 23);
            TransactionSearchControl_DateTimePicker_DateFrom.TabIndex = 2;
            // 
            // TransactionSearchControl_DateTimePicker_DateTo
            // 
            TransactionSearchControl_DateTimePicker_DateTo.Dock = DockStyle.Fill;
            TransactionSearchControl_DateTimePicker_DateTo.Format = DateTimePickerFormat.Short;
            TransactionSearchControl_DateTimePicker_DateTo.Location = new Point(49, 34);
            TransactionSearchControl_DateTimePicker_DateTo.Name = "TransactionSearchControl_DateTimePicker_DateTo";
            TransactionSearchControl_DateTimePicker_DateTo.Size = new Size(316, 23);
            TransactionSearchControl_DateTimePicker_DateTo.TabIndex = 4;
            // 
            // TransactionSearchControl_GroupBox_RadioButtons
            // 
            TransactionSearchControl_GroupBox_RadioButtons.AutoSize = true;
            TransactionSearchControl_GroupBox_RadioButtons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_TableLayout_Options.SetColumnSpan(TransactionSearchControl_GroupBox_RadioButtons, 2);
            TransactionSearchControl_GroupBox_RadioButtons.Controls.Add(TransactionSearchControl_TableLayout_QuickFilters);
            TransactionSearchControl_GroupBox_RadioButtons.Dock = DockStyle.Fill;
            TransactionSearchControl_GroupBox_RadioButtons.Location = new Point(3, 3);
            TransactionSearchControl_GroupBox_RadioButtons.Name = "TransactionSearchControl_GroupBox_RadioButtons";
            TransactionSearchControl_GroupBox_RadioButtons.Size = new Size(363, 47);
            TransactionSearchControl_GroupBox_RadioButtons.TabIndex = 18;
            TransactionSearchControl_GroupBox_RadioButtons.TabStop = false;
            TransactionSearchControl_GroupBox_RadioButtons.Text = "Simple Date Filter";
            // 
            // TransactionSearchControl_TableLayout_QuickFilters
            // 
            TransactionSearchControl_TableLayout_QuickFilters.AutoSize = true;
            TransactionSearchControl_TableLayout_QuickFilters.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_TableLayout_QuickFilters.ColumnCount = 11;
            TransactionSearchControl_TableLayout_QuickFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6666679F));
            TransactionSearchControl_TableLayout_QuickFilters.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_QuickFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6666641F));
            TransactionSearchControl_TableLayout_QuickFilters.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_QuickFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6666641F));
            TransactionSearchControl_TableLayout_QuickFilters.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_QuickFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6666641F));
            TransactionSearchControl_TableLayout_QuickFilters.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_QuickFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6666641F));
            TransactionSearchControl_TableLayout_QuickFilters.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_QuickFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 16.6666641F));
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
            TransactionSearchControl_TableLayout_QuickFilters.Size = new Size(357, 25);
            TransactionSearchControl_TableLayout_QuickFilters.TabIndex = 16;
            // 
            // TransactionSearchControl_RadioButton_Custom
            // 
            TransactionSearchControl_RadioButton_Custom.AutoSize = true;
            TransactionSearchControl_RadioButton_Custom.Dock = DockStyle.Fill;
            TransactionSearchControl_RadioButton_Custom.Location = new Point(197, 3);
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
            TransactionSearchControl_RadioButton_Month.Location = new Point(129, 3);
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
            TransactionSearchControl_RadioButton_Week.Location = new Point(68, 3);
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
            TransactionSearchControl_RadioButton_Today.Location = new Point(4, 3);
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
            TransactionSearchControl_RadioButton_Everything.Location = new Point(271, 3);
            TransactionSearchControl_RadioButton_Everything.Name = "TransactionSearchControl_RadioButton_Everything";
            TransactionSearchControl_RadioButton_Everything.Size = new Size(81, 19);
            TransactionSearchControl_RadioButton_Everything.TabIndex = 4;
            TransactionSearchControl_RadioButton_Everything.Text = "Everything";
            TransactionSearchControl_RadioButton_Everything.UseVisualStyleBackColor = true;
            // 
            // TransactionSearchControl_GroupBox_Right
            // 
            TransactionSearchControl_GroupBox_Right.AutoSize = true;
            TransactionSearchControl_GroupBox_Right.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_GroupBox_Right.Controls.Add(TransactionSearchControl_TableLayout_Right);
            TransactionSearchControl_GroupBox_Right.Dock = DockStyle.Fill;
            TransactionSearchControl_GroupBox_Right.Location = new Point(686, 5);
            TransactionSearchControl_GroupBox_Right.Name = "TransactionSearchControl_GroupBox_Right";
            TransactionSearchControl_GroupBox_Right.Size = new Size(379, 197);
            TransactionSearchControl_GroupBox_Right.TabIndex = 22;
            TransactionSearchControl_GroupBox_Right.TabStop = false;
            TransactionSearchControl_GroupBox_Right.Text = "Select a Date Range";
            // 
            // TransactionSearchControl_TableLayout_Right
            // 
            TransactionSearchControl_TableLayout_Right.AutoSize = true;
            TransactionSearchControl_TableLayout_Right.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_TableLayout_Right.ColumnCount = 1;
            TransactionSearchControl_TableLayout_Right.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_Right.Controls.Add(TransactionSearchControl_TableLayout_DateToFrom, 0, 0);
            TransactionSearchControl_TableLayout_Right.Controls.Add(TransactionSearchControl_TableLayout_Options, 0, 1);
            TransactionSearchControl_TableLayout_Right.Dock = DockStyle.Fill;
            TransactionSearchControl_TableLayout_Right.Location = new Point(3, 19);
            TransactionSearchControl_TableLayout_Right.Name = "TransactionSearchControl_TableLayout_Right";
            TransactionSearchControl_TableLayout_Right.RowCount = 2;
            TransactionSearchControl_TableLayout_Right.RowStyles.Add(new RowStyle());
            TransactionSearchControl_TableLayout_Right.RowStyles.Add(new RowStyle());
            TransactionSearchControl_TableLayout_Right.Size = new Size(373, 175);
            TransactionSearchControl_TableLayout_Right.TabIndex = 17;
            // 
            // TransactionSearchControl_TableLayout_DateToFrom
            // 
            TransactionSearchControl_TableLayout_DateToFrom.AutoSize = true;
            TransactionSearchControl_TableLayout_DateToFrom.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_TableLayout_DateToFrom.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            TransactionSearchControl_TableLayout_DateToFrom.ColumnCount = 2;
            TransactionSearchControl_TableLayout_DateToFrom.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_DateToFrom.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_DateToFrom.Controls.Add(TransactionSearchControl_Label_DateFrom, 0, 0);
            TransactionSearchControl_TableLayout_DateToFrom.Controls.Add(TransactionSearchControl_Label_DateTo, 0, 1);
            TransactionSearchControl_TableLayout_DateToFrom.Controls.Add(TransactionSearchControl_DateTimePicker_DateTo, 1, 1);
            TransactionSearchControl_TableLayout_DateToFrom.Controls.Add(TransactionSearchControl_DateTimePicker_DateFrom, 1, 0);
            TransactionSearchControl_TableLayout_DateToFrom.Dock = DockStyle.Fill;
            TransactionSearchControl_TableLayout_DateToFrom.Location = new Point(2, 2);
            TransactionSearchControl_TableLayout_DateToFrom.Margin = new Padding(2);
            TransactionSearchControl_TableLayout_DateToFrom.Name = "TransactionSearchControl_TableLayout_DateToFrom";
            TransactionSearchControl_TableLayout_DateToFrom.RowCount = 2;
            TransactionSearchControl_TableLayout_DateToFrom.RowStyles.Add(new RowStyle());
            TransactionSearchControl_TableLayout_DateToFrom.RowStyles.Add(new RowStyle());
            TransactionSearchControl_TableLayout_DateToFrom.Size = new Size(369, 61);
            TransactionSearchControl_TableLayout_DateToFrom.TabIndex = 0;
            // 
            // TransactionSearchControl_Label_DateFrom
            // 
            TransactionSearchControl_Label_DateFrom.AutoSize = true;
            TransactionSearchControl_Label_DateFrom.Dock = DockStyle.Fill;
            TransactionSearchControl_Label_DateFrom.Location = new Point(4, 4);
            TransactionSearchControl_Label_DateFrom.Margin = new Padding(3);
            TransactionSearchControl_Label_DateFrom.Name = "TransactionSearchControl_Label_DateFrom";
            TransactionSearchControl_Label_DateFrom.Size = new Size(38, 23);
            TransactionSearchControl_Label_DateFrom.TabIndex = 1;
            TransactionSearchControl_Label_DateFrom.Text = "From:";
            TransactionSearchControl_Label_DateFrom.TextAlign = ContentAlignment.MiddleRight;
            // 
            // TransactionSearchControl_Label_DateTo
            // 
            TransactionSearchControl_Label_DateTo.AutoSize = true;
            TransactionSearchControl_Label_DateTo.Dock = DockStyle.Fill;
            TransactionSearchControl_Label_DateTo.Location = new Point(4, 34);
            TransactionSearchControl_Label_DateTo.Margin = new Padding(3);
            TransactionSearchControl_Label_DateTo.Name = "TransactionSearchControl_Label_DateTo";
            TransactionSearchControl_Label_DateTo.Size = new Size(38, 23);
            TransactionSearchControl_Label_DateTo.TabIndex = 3;
            TransactionSearchControl_Label_DateTo.Text = "To:";
            TransactionSearchControl_Label_DateTo.TextAlign = ContentAlignment.MiddleRight;
            // 
            // TransactionSearchControl_TableLayout_Options
            // 
            TransactionSearchControl_TableLayout_Options.AutoSize = true;
            TransactionSearchControl_TableLayout_Options.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_TableLayout_Options.ColumnCount = 1;
            TransactionSearchControl_TableLayout_Options.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_Options.Controls.Add(TransactionSearchControl_GroupBox_TransactionTypes, 0, 1);
            TransactionSearchControl_TableLayout_Options.Controls.Add(TransactionSearchControl_GroupBox_RadioButtons, 0, 0);
            TransactionSearchControl_TableLayout_Options.Dock = DockStyle.Fill;
            TransactionSearchControl_TableLayout_Options.Location = new Point(2, 67);
            TransactionSearchControl_TableLayout_Options.Margin = new Padding(2);
            TransactionSearchControl_TableLayout_Options.Name = "TransactionSearchControl_TableLayout_Options";
            TransactionSearchControl_TableLayout_Options.RowCount = 3;
            TransactionSearchControl_TableLayout_Options.RowStyles.Add(new RowStyle());
            TransactionSearchControl_TableLayout_Options.RowStyles.Add(new RowStyle());
            TransactionSearchControl_TableLayout_Options.RowStyles.Add(new RowStyle());
            TransactionSearchControl_TableLayout_Options.Size = new Size(369, 106);
            TransactionSearchControl_TableLayout_Options.TabIndex = 1;
            // 
            // TransactionSearchControl_GroupBox_TransactionTypes
            // 
            TransactionSearchControl_GroupBox_TransactionTypes.AutoSize = true;
            TransactionSearchControl_GroupBox_TransactionTypes.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_TableLayout_Options.SetColumnSpan(TransactionSearchControl_GroupBox_TransactionTypes, 2);
            TransactionSearchControl_GroupBox_TransactionTypes.Controls.Add(TransactionSearchControl_TableLayout_TransactionTypes);
            TransactionSearchControl_GroupBox_TransactionTypes.Dock = DockStyle.Fill;
            TransactionSearchControl_GroupBox_TransactionTypes.Location = new Point(3, 56);
            TransactionSearchControl_GroupBox_TransactionTypes.Name = "TransactionSearchControl_GroupBox_TransactionTypes";
            TransactionSearchControl_GroupBox_TransactionTypes.Size = new Size(363, 47);
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
            TransactionSearchControl_TableLayout_TransactionTypes.Size = new Size(357, 25);
            TransactionSearchControl_TableLayout_TransactionTypes.TabIndex = 0;
            // 
            // TransactionSearchControl_GroupBox_Search
            // 
            TransactionSearchControl_GroupBox_Search.AutoSize = true;
            TransactionSearchControl_GroupBox_Search.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_GroupBox_Search.Controls.Add(TransactionSearchControl_TableLayout_Left);
            TransactionSearchControl_GroupBox_Search.Dock = DockStyle.Fill;
            TransactionSearchControl_GroupBox_Search.FlatStyle = FlatStyle.Flat;
            TransactionSearchControl_GroupBox_Search.Location = new Point(5, 5);
            TransactionSearchControl_GroupBox_Search.Name = "TransactionSearchControl_GroupBox_Search";
            TransactionSearchControl_GroupBox_Search.Size = new Size(675, 197);
            TransactionSearchControl_GroupBox_Search.TabIndex = 21;
            TransactionSearchControl_GroupBox_Search.TabStop = false;
            TransactionSearchControl_GroupBox_Search.Text = "Step 1: Enter Search Criteria";
            // 
            // TransactionSearchControl_TableLayout_Left
            // 
            TransactionSearchControl_TableLayout_Left.AutoSize = true;
            TransactionSearchControl_TableLayout_Left.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_TableLayout_Left.ColumnCount = 1;
            TransactionSearchControl_TableLayout_Left.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_Left.Controls.Add(TransactionSearchControl_TableLayout_Search, 0, 0);
            TransactionSearchControl_TableLayout_Left.Controls.Add(TransactionSearchControl_Panel_Buttons, 0, 2);
            TransactionSearchControl_TableLayout_Left.Dock = DockStyle.Fill;
            TransactionSearchControl_TableLayout_Left.Location = new Point(3, 19);
            TransactionSearchControl_TableLayout_Left.Margin = new Padding(2);
            TransactionSearchControl_TableLayout_Left.Name = "TransactionSearchControl_TableLayout_Left";
            TransactionSearchControl_TableLayout_Left.RowCount = 3;
            TransactionSearchControl_TableLayout_Left.RowStyles.Add(new RowStyle());
            TransactionSearchControl_TableLayout_Left.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TransactionSearchControl_TableLayout_Left.RowStyles.Add(new RowStyle());
            TransactionSearchControl_TableLayout_Left.Size = new Size(669, 175);
            TransactionSearchControl_TableLayout_Left.TabIndex = 0;
            // 
            // TransactionSearchControl_TableLayout_Search
            // 
            TransactionSearchControl_TableLayout_Search.AutoSize = true;
            TransactionSearchControl_TableLayout_Search.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_TableLayout_Search.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            TransactionSearchControl_TableLayout_Search.ColumnCount = 2;
            TransactionSearchControl_TableLayout_Search.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            TransactionSearchControl_TableLayout_Search.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            TransactionSearchControl_TableLayout_Search.Controls.Add(TransactionSearchControl_Suggestion_Operation, 0, 2);
            TransactionSearchControl_TableLayout_Search.Controls.Add(TransactionSearchControl_Suggestion_User, 0, 0);
            TransactionSearchControl_TableLayout_Search.Controls.Add(TransactionSearchControl_Suggestion_FromLocation, 1, 0);
            TransactionSearchControl_TableLayout_Search.Controls.Add(TransactionSearchControl_Suggestion_Notes, 1, 2);
            TransactionSearchControl_TableLayout_Search.Controls.Add(TransactionSearchControl_Suggestion_PartNumber, 0, 1);
            TransactionSearchControl_TableLayout_Search.Controls.Add(TransactionSearchControl_Suggestion_ToLocation, 1, 1);
            TransactionSearchControl_TableLayout_Search.Dock = DockStyle.Fill;
            TransactionSearchControl_TableLayout_Search.Location = new Point(3, 3);
            TransactionSearchControl_TableLayout_Search.Name = "TransactionSearchControl_TableLayout_Search";
            TransactionSearchControl_TableLayout_Search.RowCount = 3;
            TransactionSearchControl_TableLayout_Search.RowStyles.Add(new RowStyle());
            TransactionSearchControl_TableLayout_Search.RowStyles.Add(new RowStyle());
            TransactionSearchControl_TableLayout_Search.RowStyles.Add(new RowStyle());
            TransactionSearchControl_TableLayout_Search.Size = new Size(663, 103);
            TransactionSearchControl_TableLayout_Search.TabIndex = 1;
            // 
            // TransactionSearchControl_Suggestion_Operation
            // 
            TransactionSearchControl_Suggestion_Operation.AutoSize = true;
            TransactionSearchControl_Suggestion_Operation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_Suggestion_Operation.Dock = DockStyle.Fill;
            TransactionSearchControl_Suggestion_Operation.Font = new Font("Segoe UI Emoji", 9F);
            TransactionSearchControl_Suggestion_Operation.LabelText = "⚙️ Operation";
            TransactionSearchControl_Suggestion_Operation.Location = new Point(5, 73);
            TransactionSearchControl_Suggestion_Operation.Margin = new Padding(4);
            TransactionSearchControl_Suggestion_Operation.MinimumSize = new Size(320, 23);
            TransactionSearchControl_Suggestion_Operation.Name = "TransactionSearchControl_Suggestion_Operation";
            TransactionSearchControl_Suggestion_Operation.Padding = new Padding(1);
            TransactionSearchControl_Suggestion_Operation.PlaceholderText = "e.g., 90, 100";
            TransactionSearchControl_Suggestion_Operation.Size = new Size(322, 25);
            TransactionSearchControl_Suggestion_Operation.TabIndex = 3;
            // 
            // TransactionSearchControl_Suggestion_User
            // 
            TransactionSearchControl_Suggestion_User.AutoSize = true;
            TransactionSearchControl_Suggestion_User.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_Suggestion_User.Dock = DockStyle.Fill;
            TransactionSearchControl_Suggestion_User.Font = new Font("Segoe UI Emoji", 9F);
            TransactionSearchControl_Suggestion_User.LabelText = "👤 User";
            TransactionSearchControl_Suggestion_User.Location = new Point(5, 5);
            TransactionSearchControl_Suggestion_User.Margin = new Padding(4);
            TransactionSearchControl_Suggestion_User.MinimumSize = new Size(320, 23);
            TransactionSearchControl_Suggestion_User.Name = "TransactionSearchControl_Suggestion_User";
            TransactionSearchControl_Suggestion_User.Padding = new Padding(1);
            TransactionSearchControl_Suggestion_User.PlaceholderText = "Leave blank for all users";
            TransactionSearchControl_Suggestion_User.Size = new Size(322, 25);
            TransactionSearchControl_Suggestion_User.TabIndex = 1;
            // 
            // TransactionSearchControl_Suggestion_FromLocation
            // 
            TransactionSearchControl_Suggestion_FromLocation.AutoSize = true;
            TransactionSearchControl_Suggestion_FromLocation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_Suggestion_FromLocation.Dock = DockStyle.Fill;
            TransactionSearchControl_Suggestion_FromLocation.Font = new Font("Segoe UI Emoji", 9F);
            TransactionSearchControl_Suggestion_FromLocation.LabelText = "📍 From Location";
            TransactionSearchControl_Suggestion_FromLocation.Location = new Point(336, 5);
            TransactionSearchControl_Suggestion_FromLocation.Margin = new Padding(4);
            TransactionSearchControl_Suggestion_FromLocation.MinimumSize = new Size(320, 23);
            TransactionSearchControl_Suggestion_FromLocation.Name = "TransactionSearchControl_Suggestion_FromLocation";
            TransactionSearchControl_Suggestion_FromLocation.Padding = new Padding(1);
            TransactionSearchControl_Suggestion_FromLocation.PlaceholderText = "Optional filter";
            TransactionSearchControl_Suggestion_FromLocation.Size = new Size(322, 25);
            TransactionSearchControl_Suggestion_FromLocation.TabIndex = 4;
            // 
            // TransactionSearchControl_Suggestion_Notes
            // 
            TransactionSearchControl_Suggestion_Notes.AutoSize = true;
            TransactionSearchControl_Suggestion_Notes.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_Suggestion_Notes.Dock = DockStyle.Fill;
            TransactionSearchControl_Suggestion_Notes.EnableSuggestions = false;
            TransactionSearchControl_Suggestion_Notes.Font = new Font("Segoe UI Emoji", 9F);
            TransactionSearchControl_Suggestion_Notes.LabelText = "📝 Notes Keyword";
            TransactionSearchControl_Suggestion_Notes.Location = new Point(336, 73);
            TransactionSearchControl_Suggestion_Notes.Margin = new Padding(4);
            TransactionSearchControl_Suggestion_Notes.MinimumSize = new Size(320, 23);
            TransactionSearchControl_Suggestion_Notes.Name = "TransactionSearchControl_Suggestion_Notes";
            TransactionSearchControl_Suggestion_Notes.Padding = new Padding(1);
            TransactionSearchControl_Suggestion_Notes.PlaceholderText = "Partial match supported";
            TransactionSearchControl_Suggestion_Notes.ShowF4Button = false;
            TransactionSearchControl_Suggestion_Notes.ShowValidationColor = false;
            TransactionSearchControl_Suggestion_Notes.Size = new Size(322, 25);
            TransactionSearchControl_Suggestion_Notes.TabIndex = 6;
            // 
            // TransactionSearchControl_Suggestion_PartNumber
            // 
            TransactionSearchControl_Suggestion_PartNumber.AutoSize = true;
            TransactionSearchControl_Suggestion_PartNumber.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_Suggestion_PartNumber.Dock = DockStyle.Fill;
            TransactionSearchControl_Suggestion_PartNumber.Font = new Font("Segoe UI Emoji", 9F);
            TransactionSearchControl_Suggestion_PartNumber.LabelText = "🔍 Part Number";
            TransactionSearchControl_Suggestion_PartNumber.Location = new Point(5, 39);
            TransactionSearchControl_Suggestion_PartNumber.Margin = new Padding(4);
            TransactionSearchControl_Suggestion_PartNumber.MinimumSize = new Size(320, 23);
            TransactionSearchControl_Suggestion_PartNumber.Name = "TransactionSearchControl_Suggestion_PartNumber";
            TransactionSearchControl_Suggestion_PartNumber.Padding = new Padding(1);
            TransactionSearchControl_Suggestion_PartNumber.PlaceholderText = "Enter or select part";
            TransactionSearchControl_Suggestion_PartNumber.Size = new Size(322, 25);
            TransactionSearchControl_Suggestion_PartNumber.TabIndex = 2;
            // 
            // TransactionSearchControl_Suggestion_ToLocation
            // 
            TransactionSearchControl_Suggestion_ToLocation.AutoSize = true;
            TransactionSearchControl_Suggestion_ToLocation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_Suggestion_ToLocation.Dock = DockStyle.Fill;
            TransactionSearchControl_Suggestion_ToLocation.Font = new Font("Segoe UI Emoji", 9F);
            TransactionSearchControl_Suggestion_ToLocation.LabelText = "📍 To Location";
            TransactionSearchControl_Suggestion_ToLocation.Location = new Point(336, 39);
            TransactionSearchControl_Suggestion_ToLocation.Margin = new Padding(4);
            TransactionSearchControl_Suggestion_ToLocation.MinimumSize = new Size(320, 23);
            TransactionSearchControl_Suggestion_ToLocation.Name = "TransactionSearchControl_Suggestion_ToLocation";
            TransactionSearchControl_Suggestion_ToLocation.PlaceholderText = "Optional filter";
            TransactionSearchControl_Suggestion_ToLocation.Size = new Size(322, 25);
            TransactionSearchControl_Suggestion_ToLocation.TabIndex = 5;
            // 
            // TransactionSearchControl_Panel_Buttons
            // 
            TransactionSearchControl_Panel_Buttons.AutoSize = true;
            TransactionSearchControl_Panel_Buttons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_Panel_Buttons.BorderStyle = BorderStyle.FixedSingle;
            TransactionSearchControl_Panel_Buttons.Controls.Add(TransactionSearchControl_TableLayoutPanel_LeftLow);
            TransactionSearchControl_Panel_Buttons.Dock = DockStyle.Fill;
            TransactionSearchControl_Panel_Buttons.Location = new Point(3, 124);
            TransactionSearchControl_Panel_Buttons.Name = "TransactionSearchControl_Panel_Buttons";
            TransactionSearchControl_Panel_Buttons.Size = new Size(663, 48);
            TransactionSearchControl_Panel_Buttons.TabIndex = 5;
            // 
            // TransactionSearchControl_TableLayoutPanel_LeftLow
            // 
            TransactionSearchControl_TableLayoutPanel_LeftLow.AutoSize = true;
            TransactionSearchControl_TableLayoutPanel_LeftLow.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_TableLayoutPanel_LeftLow.ColumnCount = 1;
            TransactionSearchControl_TableLayoutPanel_LeftLow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TransactionSearchControl_TableLayoutPanel_LeftLow.Controls.Add(TransactionSearchControl_TableLayout_Buttons, 0, 0);
            TransactionSearchControl_TableLayoutPanel_LeftLow.Dock = DockStyle.Fill;
            TransactionSearchControl_TableLayoutPanel_LeftLow.Location = new Point(0, 0);
            TransactionSearchControl_TableLayoutPanel_LeftLow.Margin = new Padding(0);
            TransactionSearchControl_TableLayoutPanel_LeftLow.Name = "TransactionSearchControl_TableLayoutPanel_LeftLow";
            TransactionSearchControl_TableLayoutPanel_LeftLow.RowCount = 2;
            TransactionSearchControl_TableLayoutPanel_LeftLow.RowStyles.Add(new RowStyle());
            TransactionSearchControl_TableLayoutPanel_LeftLow.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TransactionSearchControl_TableLayoutPanel_LeftLow.Size = new Size(661, 46);
            TransactionSearchControl_TableLayoutPanel_LeftLow.TabIndex = 0;
            // 
            // TransactionSearchControl_TableLayout_Buttons
            // 
            TransactionSearchControl_TableLayout_Buttons.AutoSize = true;
            TransactionSearchControl_TableLayout_Buttons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_TableLayout_Buttons.ColumnCount = 11;
            TransactionSearchControl_TableLayout_Buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
            TransactionSearchControl_TableLayout_Buttons.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_Buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            TransactionSearchControl_TableLayout_Buttons.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_Buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            TransactionSearchControl_TableLayout_Buttons.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_Buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            TransactionSearchControl_TableLayout_Buttons.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_Buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TransactionSearchControl_TableLayout_Buttons.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_Buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            TransactionSearchControl_TableLayout_Buttons.Controls.Add(TransactionSearchControl_Button_InfoPanel, 9, 0);
            TransactionSearchControl_TableLayout_Buttons.Controls.Add(TransactionSearchControl_Button_Export, 5, 0);
            TransactionSearchControl_TableLayout_Buttons.Controls.Add(TransactionSearchControl_Button_Print, 3, 0);
            TransactionSearchControl_TableLayout_Buttons.Controls.Add(TransactionSearchControl_Button_Search, 1, 0);
            TransactionSearchControl_TableLayout_Buttons.Controls.Add(TransactionSearchControl_Button_Reset, 7, 0);
            TransactionSearchControl_TableLayout_Buttons.Dock = DockStyle.Fill;
            TransactionSearchControl_TableLayout_Buttons.Location = new Point(0, 0);
            TransactionSearchControl_TableLayout_Buttons.Margin = new Padding(0);
            TransactionSearchControl_TableLayout_Buttons.Name = "TransactionSearchControl_TableLayout_Buttons";
            TransactionSearchControl_TableLayout_Buttons.RowCount = 1;
            TransactionSearchControl_TableLayout_Buttons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            TransactionSearchControl_TableLayout_Buttons.Size = new Size(661, 46);
            TransactionSearchControl_TableLayout_Buttons.TabIndex = 0;
            // 
            // TransactionSearchControl_TableLayout_Filters
            // 
            TransactionSearchControl_TableLayout_Filters.AutoSize = true;
            TransactionSearchControl_TableLayout_Filters.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_TableLayout_Filters.ColumnCount = 2;
            TransactionSearchControl_TableLayout_Filters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            TransactionSearchControl_TableLayout_Filters.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_Filters.Controls.Add(TransactionSearchControl_GroupBox_Right, 1, 0);
            TransactionSearchControl_TableLayout_Filters.Controls.Add(TransactionSearchControl_GroupBox_Search, 0, 0);
            TransactionSearchControl_TableLayout_Filters.Dock = DockStyle.Fill;
            TransactionSearchControl_TableLayout_Filters.Location = new Point(0, 0);
            TransactionSearchControl_TableLayout_Filters.Margin = new Padding(0);
            TransactionSearchControl_TableLayout_Filters.Name = "TransactionSearchControl_TableLayout_Filters";
            TransactionSearchControl_TableLayout_Filters.Padding = new Padding(2);
            TransactionSearchControl_TableLayout_Filters.RowCount = 1;
            TransactionSearchControl_TableLayout_Filters.RowStyles.Add(new RowStyle());
            TransactionSearchControl_TableLayout_Filters.Size = new Size(1070, 207);
            TransactionSearchControl_TableLayout_Filters.TabIndex = 2;
            // 
            // TransactionSearchControl
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(TransactionSearchControl_TableLayout_Filters);
            Margin = new Padding(0);
            Name = "TransactionSearchControl";
            Size = new Size(1070, 207);
            TransactionSearchControl_GroupBox_RadioButtons.ResumeLayout(false);
            TransactionSearchControl_GroupBox_RadioButtons.PerformLayout();
            TransactionSearchControl_TableLayout_QuickFilters.ResumeLayout(false);
            TransactionSearchControl_TableLayout_QuickFilters.PerformLayout();
            TransactionSearchControl_GroupBox_Right.ResumeLayout(false);
            TransactionSearchControl_GroupBox_Right.PerformLayout();
            TransactionSearchControl_TableLayout_Right.ResumeLayout(false);
            TransactionSearchControl_TableLayout_Right.PerformLayout();
            TransactionSearchControl_TableLayout_DateToFrom.ResumeLayout(false);
            TransactionSearchControl_TableLayout_DateToFrom.PerformLayout();
            TransactionSearchControl_TableLayout_Options.ResumeLayout(false);
            TransactionSearchControl_TableLayout_Options.PerformLayout();
            TransactionSearchControl_GroupBox_TransactionTypes.ResumeLayout(false);
            TransactionSearchControl_GroupBox_TransactionTypes.PerformLayout();
            TransactionSearchControl_TableLayout_TransactionTypes.ResumeLayout(false);
            TransactionSearchControl_TableLayout_TransactionTypes.PerformLayout();
            TransactionSearchControl_GroupBox_Search.ResumeLayout(false);
            TransactionSearchControl_GroupBox_Search.PerformLayout();
            TransactionSearchControl_TableLayout_Left.ResumeLayout(false);
            TransactionSearchControl_TableLayout_Left.PerformLayout();
            TransactionSearchControl_TableLayout_Search.ResumeLayout(false);
            TransactionSearchControl_TableLayout_Search.PerformLayout();
            TransactionSearchControl_Panel_Buttons.ResumeLayout(false);
            TransactionSearchControl_Panel_Buttons.PerformLayout();
            TransactionSearchControl_TableLayoutPanel_LeftLow.ResumeLayout(false);
            TransactionSearchControl_TableLayoutPanel_LeftLow.PerformLayout();
            TransactionSearchControl_TableLayout_Buttons.ResumeLayout(false);
            TransactionSearchControl_TableLayout_Buttons.PerformLayout();
            TransactionSearchControl_TableLayout_Filters.ResumeLayout(false);
            TransactionSearchControl_TableLayout_Filters.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel TransactionSearchControl_TableLayout_Filters;
        private GroupBox TransactionSearchControl_GroupBox_Right;
        private TableLayoutPanel TransactionSearchControl_TableLayout_Right;
        private GroupBox TransactionSearchControl_GroupBox_Search;
        private DateTimePicker TransactionSearchControl_DateTimePicker_DateFrom;
        private Label TransactionSearchControl_Label_DateFrom;
        private DateTimePicker TransactionSearchControl_DateTimePicker_DateTo;
        private Label TransactionSearchControl_Label_DateTo;
        private GroupBox TransactionSearchControl_GroupBox_TransactionTypes;
        private TableLayoutPanel TransactionSearchControl_TableLayout_TransactionTypes;
        private CheckBox TransactionSearchControl_CheckBox_TRANSFER;
        private CheckBox TransactionSearchControl_CheckBox_OUT;
        private CheckBox TransactionSearchControl_CheckBox_IN;
        private GroupBox TransactionSearchControl_GroupBox_RadioButtons;
        private TableLayoutPanel TransactionSearchControl_TableLayout_QuickFilters;
        private RadioButton TransactionSearchControl_RadioButton_Custom;
        private RadioButton TransactionSearchControl_RadioButton_Month;
        private RadioButton TransactionSearchControl_RadioButton_Week;
        private RadioButton TransactionSearchControl_RadioButton_Today;
        private RadioButton TransactionSearchControl_RadioButton_Everything;
        private TableLayoutPanel TransactionSearchControl_TableLayout_DateToFrom;
        private TableLayoutPanel TransactionSearchControl_TableLayout_Options;
        private TableLayoutPanel TransactionSearchControl_TableLayout_Left;
        private TableLayoutPanel TransactionSearchControl_TableLayout_Search;
        private SuggestionTextBoxWithLabel TransactionSearchControl_Suggestion_PartNumber;
        private SuggestionTextBoxWithLabel TransactionSearchControl_Suggestion_FromLocation;
        private SuggestionTextBoxWithLabel TransactionSearchControl_Suggestion_User;
        private SuggestionTextBoxWithLabel TransactionSearchControl_Suggestion_ToLocation;
        private SuggestionTextBoxWithLabel TransactionSearchControl_Suggestion_Operation;
        private SuggestionTextBoxWithLabel TransactionSearchControl_Suggestion_Notes;
        private Panel TransactionSearchControl_Panel_Buttons;
        private TableLayoutPanel TransactionSearchControl_TableLayoutPanel_LeftLow;
        private TableLayoutPanel TransactionSearchControl_TableLayout_Buttons;
        private Button TransactionSearchControl_Button_Search;
        private Button TransactionSearchControl_Button_Reset;
        private Button TransactionSearchControl_Button_InfoPanel;
        private Button TransactionSearchControl_Button_Export;
        private Button TransactionSearchControl_Button_Print;
    }
}

