using System.Drawing;
using System.Windows.Forms;

namespace MTM_WIP_Application_Winforms.Controls.DeveloperTools
{
    partial class Control_LogViewer
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        #region Fields

        private System.ComponentModel.IContainer components = null;

        #endregion

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        #region Methods

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Control_LogViewer_TableLayout_Main = new TableLayoutPanel();
            this.Control_LogViewer_TableLayout_Header = new TableLayoutPanel();
            this.Control_LogViewer_FlowLayout_Row1 = new FlowLayoutPanel();
            this.Control_LogViewer_Label_Search = new Label();
            this.Control_LogViewer_TextBox_Search = new TextBox();
            this.Control_LogViewer_CheckBox_Regex = new CheckBox();
            this.Control_LogViewer_Label_Date = new Label();
            this.Control_LogViewer_ComboBox_DateRange = new ComboBox();
            this.Control_LogViewer_Label_From = new Label();
            this.Control_LogViewer_DateTimePicker_From = new DateTimePicker();
            this.Control_LogViewer_Label_To = new Label();
            this.Control_LogViewer_DateTimePicker_To = new DateTimePicker();
            this.Control_LogViewer_Button_Export = new Button();
            this.Control_LogViewer_FlowLayout_Row2 = new FlowLayoutPanel();
            this.Control_LogViewer_Label_Severity = new Label();
            this.Control_LogViewer_CheckBox_Info = new CheckBox();
            this.Control_LogViewer_CheckBox_Warning = new CheckBox();
            this.Control_LogViewer_CheckBox_Error = new CheckBox();
            this.Control_LogViewer_CheckBox_Critical = new CheckBox();
            this.Control_LogViewer_Label_GroupBy = new Label();
            this.Control_LogViewer_ComboBox_GroupBy = new ComboBox();
            this.Control_LogViewer_Label_Count = new Label();
            this.Control_LogViewer_SplitContainer_Main = new SplitContainer();
            this.Control_LogViewer_DataGridView_Logs = new DataGridView();
            this.Control_LogViewer_Panel_Details = new Panel();
            this.Control_LogViewer_TextBox_Details = new TextBox();
            this.Control_LogViewer_Panel_DetailsHeader = new Panel();
            this.Control_LogViewer_Button_Copy = new Button();
            this.Control_LogViewer_Label_Details = new Label();
            this.Control_LogViewer_TableLayout_Main.SuspendLayout();
            this.Control_LogViewer_TableLayout_Header.SuspendLayout();
            this.Control_LogViewer_FlowLayout_Row1.SuspendLayout();
            this.Control_LogViewer_FlowLayout_Row2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Control_LogViewer_SplitContainer_Main)).BeginInit();
            this.Control_LogViewer_SplitContainer_Main.Panel1.SuspendLayout();
            this.Control_LogViewer_SplitContainer_Main.Panel2.SuspendLayout();
            this.Control_LogViewer_SplitContainer_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Control_LogViewer_DataGridView_Logs)).BeginInit();
            this.Control_LogViewer_Panel_Details.SuspendLayout();
            this.Control_LogViewer_Panel_DetailsHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // Control_LogViewer_TableLayout_Main
            // 
            this.Control_LogViewer_TableLayout_Main.ColumnCount = 1;
            this.Control_LogViewer_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.Control_LogViewer_TableLayout_Main.Controls.Add(this.Control_LogViewer_TableLayout_Header, 0, 0);
            this.Control_LogViewer_TableLayout_Main.Controls.Add(this.Control_LogViewer_SplitContainer_Main, 0, 1);
            this.Control_LogViewer_TableLayout_Main.Dock = DockStyle.Fill;
            this.Control_LogViewer_TableLayout_Main.Location = new Point(0, 0);
            this.Control_LogViewer_TableLayout_Main.Name = "Control_LogViewer_TableLayout_Main";
            this.Control_LogViewer_TableLayout_Main.RowCount = 2;
            this.Control_LogViewer_TableLayout_Main.RowStyles.Add(new RowStyle());
            this.Control_LogViewer_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.Control_LogViewer_TableLayout_Main.Size = new Size(1000, 600);
            this.Control_LogViewer_TableLayout_Main.TabIndex = 0;
            // 
            // Control_LogViewer_TableLayout_Header
            // 
            this.Control_LogViewer_TableLayout_Header.AutoSize = true;
            this.Control_LogViewer_TableLayout_Header.ColumnCount = 1;
            this.Control_LogViewer_TableLayout_Header.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.Control_LogViewer_TableLayout_Header.Controls.Add(this.Control_LogViewer_FlowLayout_Row1, 0, 0);
            this.Control_LogViewer_TableLayout_Header.Controls.Add(this.Control_LogViewer_FlowLayout_Row2, 0, 1);
            this.Control_LogViewer_TableLayout_Header.Dock = DockStyle.Top;
            this.Control_LogViewer_TableLayout_Header.Location = new Point(0, 0);
            this.Control_LogViewer_TableLayout_Header.Name = "Control_LogViewer_TableLayout_Header";
            this.Control_LogViewer_TableLayout_Header.RowCount = 2;
            this.Control_LogViewer_TableLayout_Header.RowStyles.Add(new RowStyle());
            this.Control_LogViewer_TableLayout_Header.RowStyles.Add(new RowStyle());
            this.Control_LogViewer_TableLayout_Header.Size = new Size(900, 66);
            this.Control_LogViewer_TableLayout_Header.TabIndex = 0;
            // 
            // Control_LogViewer_FlowLayout_Row1
            // 
            this.Control_LogViewer_FlowLayout_Row1.AutoSize = true;
            this.Control_LogViewer_FlowLayout_Row1.Controls.Add(this.Control_LogViewer_Label_Search);
            this.Control_LogViewer_FlowLayout_Row1.Controls.Add(this.Control_LogViewer_TextBox_Search);
            this.Control_LogViewer_FlowLayout_Row1.Controls.Add(this.Control_LogViewer_CheckBox_Regex);
            this.Control_LogViewer_FlowLayout_Row1.Controls.Add(this.Control_LogViewer_Label_Date);
            this.Control_LogViewer_FlowLayout_Row1.Controls.Add(this.Control_LogViewer_ComboBox_DateRange);
            this.Control_LogViewer_FlowLayout_Row1.Controls.Add(this.Control_LogViewer_Label_From);
            this.Control_LogViewer_FlowLayout_Row1.Controls.Add(this.Control_LogViewer_DateTimePicker_From);
            this.Control_LogViewer_FlowLayout_Row1.Controls.Add(this.Control_LogViewer_Label_To);
            this.Control_LogViewer_FlowLayout_Row1.Controls.Add(this.Control_LogViewer_DateTimePicker_To);
            this.Control_LogViewer_FlowLayout_Row1.Controls.Add(this.Control_LogViewer_Button_Export);
            this.Control_LogViewer_FlowLayout_Row1.Dock = DockStyle.Fill;
            this.Control_LogViewer_FlowLayout_Row1.Location = new Point(3, 3);
            this.Control_LogViewer_FlowLayout_Row1.Name = "Control_LogViewer_FlowLayout_Row1";
            this.Control_LogViewer_FlowLayout_Row1.Size = new Size(894, 29);
            this.Control_LogViewer_FlowLayout_Row1.TabIndex = 0;
            this.Control_LogViewer_FlowLayout_Row1.WrapContents = false;
            // 
            // Control_LogViewer_Label_Search
            // 
            this.Control_LogViewer_Label_Search.Anchor = AnchorStyles.Left;
            this.Control_LogViewer_Label_Search.AutoSize = true;
            this.Control_LogViewer_Label_Search.Location = new Point(3, 7);
            this.Control_LogViewer_Label_Search.Name = "Control_LogViewer_Label_Search";
            this.Control_LogViewer_Label_Search.Size = new Size(45, 15);
            this.Control_LogViewer_Label_Search.TabIndex = 0;
            this.Control_LogViewer_Label_Search.Text = "Search:";
            // 
            // Control_LogViewer_TextBox_Search
            // 
            this.Control_LogViewer_TextBox_Search.Location = new Point(54, 3);
            this.Control_LogViewer_TextBox_Search.Name = "Control_LogViewer_TextBox_Search";
            this.Control_LogViewer_TextBox_Search.Size = new Size(150, 23);
            this.Control_LogViewer_TextBox_Search.TabIndex = 1;
            // 
            // Control_LogViewer_CheckBox_Regex
            // 
            this.Control_LogViewer_CheckBox_Regex.Anchor = AnchorStyles.Left;
            this.Control_LogViewer_CheckBox_Regex.AutoSize = true;
            this.Control_LogViewer_CheckBox_Regex.Location = new Point(210, 5);
            this.Control_LogViewer_CheckBox_Regex.Name = "Control_LogViewer_CheckBox_Regex";
            this.Control_LogViewer_CheckBox_Regex.Size = new Size(58, 19);
            this.Control_LogViewer_CheckBox_Regex.TabIndex = 2;
            this.Control_LogViewer_CheckBox_Regex.Text = "Regex";
            this.Control_LogViewer_CheckBox_Regex.UseVisualStyleBackColor = true;
            // 
            // Control_LogViewer_Label_Date
            // 
            this.Control_LogViewer_Label_Date.Anchor = AnchorStyles.Left;
            this.Control_LogViewer_Label_Date.AutoSize = true;
            this.Control_LogViewer_Label_Date.Location = new Point(274, 7);
            this.Control_LogViewer_Label_Date.Margin = new Padding(3, 0, 3, 0);
            this.Control_LogViewer_Label_Date.Name = "Control_LogViewer_Label_Date";
            this.Control_LogViewer_Label_Date.Size = new Size(34, 15);
            this.Control_LogViewer_Label_Date.TabIndex = 3;
            this.Control_LogViewer_Label_Date.Text = "Date:";
            // 
            // Control_LogViewer_ComboBox_DateRange
            // 
            this.Control_LogViewer_ComboBox_DateRange.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Control_LogViewer_ComboBox_DateRange.FormattingEnabled = true;
            this.Control_LogViewer_ComboBox_DateRange.Location = new Point(314, 3);
            this.Control_LogViewer_ComboBox_DateRange.Name = "Control_LogViewer_ComboBox_DateRange";
            this.Control_LogViewer_ComboBox_DateRange.Size = new Size(120, 23);
            this.Control_LogViewer_ComboBox_DateRange.TabIndex = 4;
            // 
            // Control_LogViewer_Label_From
            // 
            this.Control_LogViewer_Label_From.Anchor = AnchorStyles.Left;
            this.Control_LogViewer_Label_From.AutoSize = true;
            this.Control_LogViewer_Label_From.Location = new Point(440, 7);
            this.Control_LogViewer_Label_From.Name = "Control_LogViewer_Label_From";
            this.Control_LogViewer_Label_From.Size = new Size(38, 15);
            this.Control_LogViewer_Label_From.TabIndex = 5;
            this.Control_LogViewer_Label_From.Text = "From:";
            this.Control_LogViewer_Label_From.Visible = false;
            // 
            // Control_LogViewer_DateTimePicker_From
            // 
            this.Control_LogViewer_DateTimePicker_From.Format = DateTimePickerFormat.Short;
            this.Control_LogViewer_DateTimePicker_From.Location = new Point(484, 3);
            this.Control_LogViewer_DateTimePicker_From.Name = "Control_LogViewer_DateTimePicker_From";
            this.Control_LogViewer_DateTimePicker_From.Size = new Size(100, 23);
            this.Control_LogViewer_DateTimePicker_From.TabIndex = 6;
            this.Control_LogViewer_DateTimePicker_From.Visible = false;
            // 
            // Control_LogViewer_Label_To
            // 
            this.Control_LogViewer_Label_To.Anchor = AnchorStyles.Left;
            this.Control_LogViewer_Label_To.AutoSize = true;
            this.Control_LogViewer_Label_To.Location = new Point(590, 7);
            this.Control_LogViewer_Label_To.Name = "Control_LogViewer_Label_To";
            this.Control_LogViewer_Label_To.Size = new Size(22, 15);
            this.Control_LogViewer_Label_To.TabIndex = 7;
            this.Control_LogViewer_Label_To.Text = "To:";
            this.Control_LogViewer_Label_To.Visible = false;
            // 
            // Control_LogViewer_DateTimePicker_To
            // 
            this.Control_LogViewer_DateTimePicker_To.Format = DateTimePickerFormat.Short;
            this.Control_LogViewer_DateTimePicker_To.Location = new Point(618, 3);
            this.Control_LogViewer_DateTimePicker_To.Name = "Control_LogViewer_DateTimePicker_To";
            this.Control_LogViewer_DateTimePicker_To.Size = new Size(100, 23);
            this.Control_LogViewer_DateTimePicker_To.TabIndex = 8;
            this.Control_LogViewer_DateTimePicker_To.Visible = false;
            // 
            // Control_LogViewer_Button_Export
            // 
            this.Control_LogViewer_Button_Export.Location = new Point(724, 3);
            this.Control_LogViewer_Button_Export.Name = "Control_LogViewer_Button_Export";
            this.Control_LogViewer_Button_Export.Size = new Size(75, 23);
            this.Control_LogViewer_Button_Export.TabIndex = 14;
            this.Control_LogViewer_Button_Export.Text = "Export";
            this.Control_LogViewer_Button_Export.UseVisualStyleBackColor = true;
            // 
            // Control_LogViewer_FlowLayout_Row2
            // 
            this.Control_LogViewer_FlowLayout_Row2.AutoSize = true;
            this.Control_LogViewer_FlowLayout_Row2.Controls.Add(this.Control_LogViewer_Label_Severity);
            this.Control_LogViewer_FlowLayout_Row2.Controls.Add(this.Control_LogViewer_CheckBox_Info);
            this.Control_LogViewer_FlowLayout_Row2.Controls.Add(this.Control_LogViewer_CheckBox_Warning);
            this.Control_LogViewer_FlowLayout_Row2.Controls.Add(this.Control_LogViewer_CheckBox_Error);
            this.Control_LogViewer_FlowLayout_Row2.Controls.Add(this.Control_LogViewer_CheckBox_Critical);
            this.Control_LogViewer_FlowLayout_Row2.Controls.Add(this.Control_LogViewer_Label_GroupBy);
            this.Control_LogViewer_FlowLayout_Row2.Controls.Add(this.Control_LogViewer_ComboBox_GroupBy);
            this.Control_LogViewer_FlowLayout_Row2.Controls.Add(this.Control_LogViewer_Label_Count);
            this.Control_LogViewer_FlowLayout_Row2.Dock = DockStyle.Fill;
            this.Control_LogViewer_FlowLayout_Row2.Location = new Point(3, 38);
            this.Control_LogViewer_FlowLayout_Row2.Name = "Control_LogViewer_FlowLayout_Row2";
            this.Control_LogViewer_FlowLayout_Row2.Size = new Size(894, 25);
            this.Control_LogViewer_FlowLayout_Row2.TabIndex = 1;
            this.Control_LogViewer_FlowLayout_Row2.WrapContents = false;
            // 
            // Control_LogViewer_Label_Severity
            // 
            this.Control_LogViewer_Label_Severity.Anchor = AnchorStyles.Left;
            this.Control_LogViewer_Label_Severity.AutoSize = true;
            this.Control_LogViewer_Label_Severity.Location = new Point(3, 5);
            this.Control_LogViewer_Label_Severity.Name = "Control_LogViewer_Label_Severity";
            this.Control_LogViewer_Label_Severity.Size = new Size(51, 15);
            this.Control_LogViewer_Label_Severity.TabIndex = 9;
            this.Control_LogViewer_Label_Severity.Text = "Severity:";
            // 
            // Control_LogViewer_CheckBox_Info
            // 
            this.Control_LogViewer_CheckBox_Info.Anchor = AnchorStyles.Left;
            this.Control_LogViewer_CheckBox_Info.AutoSize = true;
            this.Control_LogViewer_CheckBox_Info.Location = new Point(60, 3);
            this.Control_LogViewer_CheckBox_Info.Name = "Control_LogViewer_CheckBox_Info";
            this.Control_LogViewer_CheckBox_Info.Size = new Size(47, 19);
            this.Control_LogViewer_CheckBox_Info.TabIndex = 10;
            this.Control_LogViewer_CheckBox_Info.Text = "Info";
            this.Control_LogViewer_CheckBox_Info.UseVisualStyleBackColor = true;
            // 
            // Control_LogViewer_CheckBox_Warning
            // 
            this.Control_LogViewer_CheckBox_Warning.Anchor = AnchorStyles.Left;
            this.Control_LogViewer_CheckBox_Warning.AutoSize = true;
            this.Control_LogViewer_CheckBox_Warning.Checked = true;
            this.Control_LogViewer_CheckBox_Warning.CheckState = CheckState.Checked;
            this.Control_LogViewer_CheckBox_Warning.ForeColor = Color.Orange;
            this.Control_LogViewer_CheckBox_Warning.Location = new Point(113, 3);
            this.Control_LogViewer_CheckBox_Warning.Name = "Control_LogViewer_CheckBox_Warning";
            this.Control_LogViewer_CheckBox_Warning.Size = new Size(71, 19);
            this.Control_LogViewer_CheckBox_Warning.TabIndex = 11;
            this.Control_LogViewer_CheckBox_Warning.Text = "Warning";
            this.Control_LogViewer_CheckBox_Warning.UseVisualStyleBackColor = true;
            // 
            // Control_LogViewer_CheckBox_Error
            // 
            this.Control_LogViewer_CheckBox_Error.Anchor = AnchorStyles.Left;
            this.Control_LogViewer_CheckBox_Error.AutoSize = true;
            this.Control_LogViewer_CheckBox_Error.Checked = true;
            this.Control_LogViewer_CheckBox_Error.CheckState = CheckState.Checked;
            this.Control_LogViewer_CheckBox_Error.ForeColor = Color.Red;
            this.Control_LogViewer_CheckBox_Error.Location = new Point(190, 3);
            this.Control_LogViewer_CheckBox_Error.Name = "Control_LogViewer_CheckBox_Error";
            this.Control_LogViewer_CheckBox_Error.Size = new Size(51, 19);
            this.Control_LogViewer_CheckBox_Error.TabIndex = 12;
            this.Control_LogViewer_CheckBox_Error.Text = "Error";
            this.Control_LogViewer_CheckBox_Error.UseVisualStyleBackColor = true;
            // 
            // Control_LogViewer_CheckBox_Critical
            // 
            this.Control_LogViewer_CheckBox_Critical.Anchor = AnchorStyles.Left;
            this.Control_LogViewer_CheckBox_Critical.AutoSize = true;
            this.Control_LogViewer_CheckBox_Critical.Checked = true;
            this.Control_LogViewer_CheckBox_Critical.CheckState = CheckState.Checked;
            this.Control_LogViewer_CheckBox_Critical.ForeColor = Color.Red;
            this.Control_LogViewer_CheckBox_Critical.Location = new Point(247, 3);
            this.Control_LogViewer_CheckBox_Critical.Name = "Control_LogViewer_CheckBox_Critical";
            this.Control_LogViewer_CheckBox_Critical.Size = new Size(63, 19);
            this.Control_LogViewer_CheckBox_Critical.TabIndex = 13;
            this.Control_LogViewer_CheckBox_Critical.Text = "Critical";
            this.Control_LogViewer_CheckBox_Critical.UseVisualStyleBackColor = true;
            // 
            // Control_LogViewer_Label_GroupBy
            // 
            this.Control_LogViewer_Label_GroupBy.Anchor = AnchorStyles.Left;
            this.Control_LogViewer_Label_GroupBy.AutoSize = true;
            this.Control_LogViewer_Label_GroupBy.Location = new Point(316, 5);
            this.Control_LogViewer_Label_GroupBy.Name = "Control_LogViewer_Label_GroupBy";
            this.Control_LogViewer_Label_GroupBy.Size = new Size(59, 15);
            this.Control_LogViewer_Label_GroupBy.TabIndex = 16;
            this.Control_LogViewer_Label_GroupBy.Text = "Group By:";
            // 
            // Control_LogViewer_ComboBox_GroupBy
            // 
            this.Control_LogViewer_ComboBox_GroupBy.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Control_LogViewer_ComboBox_GroupBy.FormattingEnabled = true;
            this.Control_LogViewer_ComboBox_GroupBy.Location = new Point(381, 1);
            this.Control_LogViewer_ComboBox_GroupBy.Name = "Control_LogViewer_ComboBox_GroupBy";
            this.Control_LogViewer_ComboBox_GroupBy.Size = new Size(120, 23);
            this.Control_LogViewer_ComboBox_GroupBy.TabIndex = 17;
            // 
            // Control_LogViewer_Label_Count
            // 
            this.Control_LogViewer_Label_Count.Anchor = AnchorStyles.Left;
            this.Control_LogViewer_Label_Count.AutoSize = true;
            this.Control_LogViewer_Label_Count.ForeColor = Color.Gray;
            this.Control_LogViewer_Label_Count.Location = new Point(507, 5);
            this.Control_LogViewer_Label_Count.Name = "Control_LogViewer_Label_Count";
            this.Control_LogViewer_Label_Count.Size = new Size(87, 15);
            this.Control_LogViewer_Label_Count.TabIndex = 15;
            this.Control_LogViewer_Label_Count.Text = "0 entries found";
            // 
            // Control_LogViewer_SplitContainer_Main
            // 
            this.Control_LogViewer_SplitContainer_Main.Dock = DockStyle.Fill;
            this.Control_LogViewer_SplitContainer_Main.Location = new Point(0, 66);
            this.Control_LogViewer_SplitContainer_Main.Name = "Control_LogViewer_SplitContainer_Main";
            this.Control_LogViewer_SplitContainer_Main.Orientation = Orientation.Horizontal;
            // 
            // Control_LogViewer_SplitContainer_Main.Panel1
            // 
            this.Control_LogViewer_SplitContainer_Main.Panel1.Controls.Add(this.Control_LogViewer_DataGridView_Logs);
            // 
            // Control_LogViewer_SplitContainer_Main.Panel2
            // 
            this.Control_LogViewer_SplitContainer_Main.Panel2.Controls.Add(this.Control_LogViewer_Panel_Details);
            this.Control_LogViewer_SplitContainer_Main.Size = new Size(900, 534);
            this.Control_LogViewer_SplitContainer_Main.SplitterDistance = 352;
            this.Control_LogViewer_SplitContainer_Main.TabIndex = 1;
            // 
            // Control_LogViewer_DataGridView_Logs
            // 
            this.Control_LogViewer_DataGridView_Logs.AllowUserToAddRows = false;
            this.Control_LogViewer_DataGridView_Logs.AllowUserToDeleteRows = false;
            this.Control_LogViewer_DataGridView_Logs.AllowUserToResizeRows = false;
            this.Control_LogViewer_DataGridView_Logs.BackgroundColor = Color.White;
            this.Control_LogViewer_DataGridView_Logs.BorderStyle = BorderStyle.None;
            this.Control_LogViewer_DataGridView_Logs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Control_LogViewer_DataGridView_Logs.Dock = DockStyle.Fill;
            this.Control_LogViewer_DataGridView_Logs.Location = new Point(0, 0);
            this.Control_LogViewer_DataGridView_Logs.MultiSelect = false;
            this.Control_LogViewer_DataGridView_Logs.Name = "Control_LogViewer_DataGridView_Logs";
            this.Control_LogViewer_DataGridView_Logs.ReadOnly = true;
            this.Control_LogViewer_DataGridView_Logs.RowHeadersVisible = false;
            this.Control_LogViewer_DataGridView_Logs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.Control_LogViewer_DataGridView_Logs.Size = new Size(900, 352);
            this.Control_LogViewer_DataGridView_Logs.TabIndex = 0;
            // 
            // Control_LogViewer_Panel_Details
            // 
            this.Control_LogViewer_Panel_Details.Controls.Add(this.Control_LogViewer_TextBox_Details);
            this.Control_LogViewer_Panel_Details.Controls.Add(this.Control_LogViewer_Panel_DetailsHeader);
            this.Control_LogViewer_Panel_Details.Dock = DockStyle.Fill;
            this.Control_LogViewer_Panel_Details.Location = new Point(0, 0);
            this.Control_LogViewer_Panel_Details.Name = "Control_LogViewer_Panel_Details";
            this.Control_LogViewer_Panel_Details.Size = new Size(900, 178);
            this.Control_LogViewer_Panel_Details.TabIndex = 0;
            // 
            // Control_LogViewer_TextBox_Details
            // 
            this.Control_LogViewer_TextBox_Details.BackColor = Color.White;
            this.Control_LogViewer_TextBox_Details.Dock = DockStyle.Fill;
            this.Control_LogViewer_TextBox_Details.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            this.Control_LogViewer_TextBox_Details.Location = new Point(0, 30);
            this.Control_LogViewer_TextBox_Details.Multiline = true;
            this.Control_LogViewer_TextBox_Details.Name = "Control_LogViewer_TextBox_Details";
            this.Control_LogViewer_TextBox_Details.ReadOnly = true;
            this.Control_LogViewer_TextBox_Details.ScrollBars = ScrollBars.Vertical;
            this.Control_LogViewer_TextBox_Details.Size = new Size(900, 148);
            this.Control_LogViewer_TextBox_Details.TabIndex = 1;
            // 
            // Control_LogViewer_Panel_DetailsHeader
            // 
            this.Control_LogViewer_Panel_DetailsHeader.Controls.Add(this.Control_LogViewer_Button_Copy);
            this.Control_LogViewer_Panel_DetailsHeader.Controls.Add(this.Control_LogViewer_Label_Details);
            this.Control_LogViewer_Panel_DetailsHeader.Dock = DockStyle.Top;
            this.Control_LogViewer_Panel_DetailsHeader.Location = new Point(0, 0);
            this.Control_LogViewer_Panel_DetailsHeader.Name = "Control_LogViewer_Panel_DetailsHeader";
            this.Control_LogViewer_Panel_DetailsHeader.Size = new Size(900, 30);
            this.Control_LogViewer_Panel_DetailsHeader.TabIndex = 0;
            // 
            // Control_LogViewer_Button_Copy
            // 
            this.Control_LogViewer_Button_Copy.Anchor = ((AnchorStyles)((AnchorStyles.Top | AnchorStyles.Right)));
            this.Control_LogViewer_Button_Copy.Location = new Point(812, 4);
            this.Control_LogViewer_Button_Copy.Name = "Control_LogViewer_Button_Copy";
            this.Control_LogViewer_Button_Copy.Size = new Size(75, 23);
            this.Control_LogViewer_Button_Copy.TabIndex = 1;
            this.Control_LogViewer_Button_Copy.Text = "Copy";
            this.Control_LogViewer_Button_Copy.UseVisualStyleBackColor = true;
            // 
            // Control_LogViewer_Label_Details
            // 
            this.Control_LogViewer_Label_Details.AutoSize = true;
            this.Control_LogViewer_Label_Details.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.Control_LogViewer_Label_Details.Location = new Point(10, 8);
            this.Control_LogViewer_Label_Details.Name = "Control_LogViewer_Label_Details";
            this.Control_LogViewer_Label_Details.Size = new Size(71, 15);
            this.Control_LogViewer_Label_Details.TabIndex = 0;
            this.Control_LogViewer_Label_Details.Text = "Log Details";
            // 
            // Control_LogViewer
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.Control_LogViewer_SplitContainer_Main);
            this.Controls.Add(this.Control_LogViewer_TableLayout_Header);
            this.Name = "Control_LogViewer";
            this.Size = new Size(900, 600);
            this.Control_LogViewer_TableLayout_Header.ResumeLayout(false);
            this.Control_LogViewer_TableLayout_Header.PerformLayout();
            this.Control_LogViewer_FlowLayout_Row1.ResumeLayout(false);
            this.Control_LogViewer_FlowLayout_Row1.PerformLayout();
            this.Control_LogViewer_FlowLayout_Row2.ResumeLayout(false);
            this.Control_LogViewer_FlowLayout_Row2.PerformLayout();
            this.Control_LogViewer_SplitContainer_Main.Panel1.ResumeLayout(false);
            this.Control_LogViewer_SplitContainer_Main.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Control_LogViewer_SplitContainer_Main)).EndInit();
            this.Control_LogViewer_SplitContainer_Main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Control_LogViewer_DataGridView_Logs)).EndInit();
            this.Control_LogViewer_Panel_Details.ResumeLayout(false);
            this.Control_LogViewer_Panel_Details.PerformLayout();
            this.Control_LogViewer_Panel_DetailsHeader.ResumeLayout(false);
            this.Control_LogViewer_Panel_DetailsHeader.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TableLayoutPanel Control_LogViewer_TableLayout_Main;
        private TableLayoutPanel Control_LogViewer_TableLayout_Header;
        private FlowLayoutPanel Control_LogViewer_FlowLayout_Row1;
        private FlowLayoutPanel Control_LogViewer_FlowLayout_Row2;
        private Label Control_LogViewer_Label_Search;
        private TextBox Control_LogViewer_TextBox_Search;
        private CheckBox Control_LogViewer_CheckBox_Regex;
        private Label Control_LogViewer_Label_Date;
        private ComboBox Control_LogViewer_ComboBox_DateRange;
        private Label Control_LogViewer_Label_From;
        private DateTimePicker Control_LogViewer_DateTimePicker_From;
        private Label Control_LogViewer_Label_To;
        private DateTimePicker Control_LogViewer_DateTimePicker_To;
        private Label Control_LogViewer_Label_Severity;
        private CheckBox Control_LogViewer_CheckBox_Info;
        private CheckBox Control_LogViewer_CheckBox_Warning;
        private CheckBox Control_LogViewer_CheckBox_Error;
        private CheckBox Control_LogViewer_CheckBox_Critical;
        private Button Control_LogViewer_Button_Export;
        private Label Control_LogViewer_Label_Count;
        private SplitContainer Control_LogViewer_SplitContainer_Main;
        private DataGridView Control_LogViewer_DataGridView_Logs;
        private Panel Control_LogViewer_Panel_Details;
        private Panel Control_LogViewer_Panel_DetailsHeader;
        private Label Control_LogViewer_Label_Details;
        private Button Control_LogViewer_Button_Copy;
        private TextBox Control_LogViewer_TextBox_Details;
        private Label Control_LogViewer_Label_GroupBy;
        private ComboBox Control_LogViewer_ComboBox_GroupBy;
    }
}



