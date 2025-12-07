// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MTM_WIP_Application_Winforms.Controls.Visual
{
    partial class Control_VisualUserAnalytics
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
            Control_VisualUserAnalytics_TableLayout_Main = new TableLayoutPanel();
            Control_VisualUserAnalytics_TableLayout_Content = new TableLayoutPanel();
            Control_VisualUserAnalytics_FlowPanel_DateRanges = new FlowLayoutPanel();
            Control_VisualUserAnalytics_RadioButton_Today = new RadioButton();
            Control_VisualUserAnalytics_RadioButton_Week = new RadioButton();
            Control_VisualUserAnalytics_RadioButton_Month = new RadioButton();
            Control_VisualUserAnalytics_RadioButton_Custom = new RadioButton();
            Control_VisualUserAnalytics_Label_StartDate = new Label();
            Control_VisualUserAnalytics_DateTimePicker_StartDate = new DateTimePicker();
            Control_VisualUserAnalytics_Label_EndDate = new Label();
            Control_VisualUserAnalytics_DateTimePicker_EndDate = new DateTimePicker();
            Control_VisualUserAnalytics_FlowPanel_Shifts = new FlowLayoutPanel();
            Control_VisualUserAnalytics_CheckBox_Shift1 = new CheckBox();
            Control_VisualUserAnalytics_CheckBox_Shift2 = new CheckBox();
            Control_VisualUserAnalytics_CheckBox_Shift3 = new CheckBox();
            Control_VisualUserAnalytics_CheckBox_ShiftWeekend = new CheckBox();
            Control_VisualUserAnalytics_Button_LoadUsers = new Button();
            Control_VisualUserAnalytics_Button_SelectAllUsers = new Button();
            Control_VisualUserAnalytics_Button_GenerateReport = new Button();
            Control_VisualUserAnalytics_Label_UserCount = new Label();
            Control_VisualUserAnalytics_GroupBox_Instructions = new GroupBox();
            Control_VisualUserAnalytics_CheckedListBox_Instructions = new CheckedListBox();
            Control_VisualUserAnalytics_CheckedListBox_Users = new CheckedListBox();
            Control_VisualUserAnalytics_TableLayout_Main.SuspendLayout();
            Control_VisualUserAnalytics_TableLayout_Content.SuspendLayout();
            Control_VisualUserAnalytics_FlowPanel_DateRanges.SuspendLayout();
            Control_VisualUserAnalytics_FlowPanel_Shifts.SuspendLayout();
            Control_VisualUserAnalytics_GroupBox_Instructions.SuspendLayout();
            SuspendLayout();
            // 
            // Control_VisualUserAnalytics_TableLayout_Main
            // 
            Control_VisualUserAnalytics_TableLayout_Main.AutoSize = true;
            Control_VisualUserAnalytics_TableLayout_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_VisualUserAnalytics_TableLayout_Main.ColumnCount = 1;
            Control_VisualUserAnalytics_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_VisualUserAnalytics_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            Control_VisualUserAnalytics_TableLayout_Main.Controls.Add(Control_VisualUserAnalytics_TableLayout_Content, 0, 0);
            Control_VisualUserAnalytics_TableLayout_Main.Controls.Add(Control_VisualUserAnalytics_CheckedListBox_Users, 0, 1);
            Control_VisualUserAnalytics_TableLayout_Main.Dock = DockStyle.Fill;
            Control_VisualUserAnalytics_TableLayout_Main.Location = new Point(0, 0);
            Control_VisualUserAnalytics_TableLayout_Main.Name = "Control_VisualUserAnalytics_TableLayout_Main";
            Control_VisualUserAnalytics_TableLayout_Main.Padding = new Padding(10);
            Control_VisualUserAnalytics_TableLayout_Main.RowCount = 2;
            Control_VisualUserAnalytics_TableLayout_Main.RowStyles.Add(new RowStyle());
            Control_VisualUserAnalytics_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_VisualUserAnalytics_TableLayout_Main.Size = new Size(798, 556);
            Control_VisualUserAnalytics_TableLayout_Main.TabIndex = 2;
            // 
            // Control_VisualUserAnalytics_TableLayout_Content
            // 
            Control_VisualUserAnalytics_TableLayout_Content.AutoSize = true;
            Control_VisualUserAnalytics_TableLayout_Content.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_VisualUserAnalytics_TableLayout_Content.ColumnCount = 3;
            Control_VisualUserAnalytics_TableLayout_Content.ColumnStyles.Add(new ColumnStyle());
            Control_VisualUserAnalytics_TableLayout_Content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            Control_VisualUserAnalytics_TableLayout_Content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            Control_VisualUserAnalytics_TableLayout_Content.Controls.Add(Control_VisualUserAnalytics_FlowPanel_DateRanges, 0, 0);
            Control_VisualUserAnalytics_TableLayout_Content.Controls.Add(Control_VisualUserAnalytics_Label_StartDate, 0, 1);
            Control_VisualUserAnalytics_TableLayout_Content.Controls.Add(Control_VisualUserAnalytics_DateTimePicker_StartDate, 1, 1);
            Control_VisualUserAnalytics_TableLayout_Content.Controls.Add(Control_VisualUserAnalytics_Label_EndDate, 0, 2);
            Control_VisualUserAnalytics_TableLayout_Content.Controls.Add(Control_VisualUserAnalytics_DateTimePicker_EndDate, 1, 2);
            Control_VisualUserAnalytics_TableLayout_Content.Controls.Add(Control_VisualUserAnalytics_FlowPanel_Shifts, 0, 3);
            Control_VisualUserAnalytics_TableLayout_Content.Controls.Add(Control_VisualUserAnalytics_Button_LoadUsers, 0, 4);
            Control_VisualUserAnalytics_TableLayout_Content.Controls.Add(Control_VisualUserAnalytics_Button_SelectAllUsers, 0, 5);
            Control_VisualUserAnalytics_TableLayout_Content.Controls.Add(Control_VisualUserAnalytics_Button_GenerateReport, 0, 6);
            Control_VisualUserAnalytics_TableLayout_Content.Controls.Add(Control_VisualUserAnalytics_Label_UserCount, 0, 7);
            Control_VisualUserAnalytics_TableLayout_Content.Controls.Add(Control_VisualUserAnalytics_GroupBox_Instructions, 2, 0);
            Control_VisualUserAnalytics_TableLayout_Content.Dock = DockStyle.Fill;
            Control_VisualUserAnalytics_TableLayout_Content.Location = new Point(13, 13);
            Control_VisualUserAnalytics_TableLayout_Content.Margin = new Padding(3, 3, 10, 3);
            Control_VisualUserAnalytics_TableLayout_Content.Name = "Control_VisualUserAnalytics_TableLayout_Content";
            Control_VisualUserAnalytics_TableLayout_Content.RowCount = 8;
            Control_VisualUserAnalytics_TableLayout_Content.RowStyles.Add(new RowStyle());
            Control_VisualUserAnalytics_TableLayout_Content.RowStyles.Add(new RowStyle());
            Control_VisualUserAnalytics_TableLayout_Content.RowStyles.Add(new RowStyle());
            Control_VisualUserAnalytics_TableLayout_Content.RowStyles.Add(new RowStyle());
            Control_VisualUserAnalytics_TableLayout_Content.RowStyles.Add(new RowStyle());
            Control_VisualUserAnalytics_TableLayout_Content.RowStyles.Add(new RowStyle());
            Control_VisualUserAnalytics_TableLayout_Content.RowStyles.Add(new RowStyle());
            Control_VisualUserAnalytics_TableLayout_Content.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_VisualUserAnalytics_TableLayout_Content.Size = new Size(765, 255);
            Control_VisualUserAnalytics_TableLayout_Content.TabIndex = 4;
            // 
            // Control_VisualUserAnalytics_FlowPanel_DateRanges
            // 
            Control_VisualUserAnalytics_FlowPanel_DateRanges.AutoSize = true;
            Control_VisualUserAnalytics_TableLayout_Content.SetColumnSpan(Control_VisualUserAnalytics_FlowPanel_DateRanges, 2);
            Control_VisualUserAnalytics_FlowPanel_DateRanges.Controls.Add(Control_VisualUserAnalytics_RadioButton_Today);
            Control_VisualUserAnalytics_FlowPanel_DateRanges.Controls.Add(Control_VisualUserAnalytics_RadioButton_Week);
            Control_VisualUserAnalytics_FlowPanel_DateRanges.Controls.Add(Control_VisualUserAnalytics_RadioButton_Month);
            Control_VisualUserAnalytics_FlowPanel_DateRanges.Controls.Add(Control_VisualUserAnalytics_RadioButton_Custom);
            Control_VisualUserAnalytics_FlowPanel_DateRanges.Dock = DockStyle.Fill;
            Control_VisualUserAnalytics_FlowPanel_DateRanges.Location = new Point(3, 3);
            Control_VisualUserAnalytics_FlowPanel_DateRanges.Name = "Control_VisualUserAnalytics_FlowPanel_DateRanges";
            Control_VisualUserAnalytics_FlowPanel_DateRanges.Size = new Size(375, 25);
            Control_VisualUserAnalytics_FlowPanel_DateRanges.TabIndex = 0;
            // 
            // Control_VisualUserAnalytics_RadioButton_Today
            // 
            Control_VisualUserAnalytics_RadioButton_Today.AutoSize = true;
            Control_VisualUserAnalytics_RadioButton_Today.Location = new Point(3, 3);
            Control_VisualUserAnalytics_RadioButton_Today.Name = "Control_VisualUserAnalytics_RadioButton_Today";
            Control_VisualUserAnalytics_RadioButton_Today.Size = new Size(57, 19);
            Control_VisualUserAnalytics_RadioButton_Today.TabIndex = 0;
            Control_VisualUserAnalytics_RadioButton_Today.Text = "Today";
            Control_VisualUserAnalytics_RadioButton_Today.UseVisualStyleBackColor = true;
            // 
            // Control_VisualUserAnalytics_RadioButton_Week
            // 
            Control_VisualUserAnalytics_RadioButton_Week.AutoSize = true;
            Control_VisualUserAnalytics_RadioButton_Week.Location = new Point(66, 3);
            Control_VisualUserAnalytics_RadioButton_Week.Name = "Control_VisualUserAnalytics_RadioButton_Week";
            Control_VisualUserAnalytics_RadioButton_Week.Size = new Size(83, 19);
            Control_VisualUserAnalytics_RadioButton_Week.TabIndex = 1;
            Control_VisualUserAnalytics_RadioButton_Week.Text = "Last 7 Days";
            Control_VisualUserAnalytics_RadioButton_Week.UseVisualStyleBackColor = true;
            // 
            // Control_VisualUserAnalytics_RadioButton_Month
            // 
            Control_VisualUserAnalytics_RadioButton_Month.AutoSize = true;
            Control_VisualUserAnalytics_RadioButton_Month.Checked = true;
            Control_VisualUserAnalytics_RadioButton_Month.Location = new Point(155, 3);
            Control_VisualUserAnalytics_RadioButton_Month.Name = "Control_VisualUserAnalytics_RadioButton_Month";
            Control_VisualUserAnalytics_RadioButton_Month.Size = new Size(89, 19);
            Control_VisualUserAnalytics_RadioButton_Month.TabIndex = 2;
            Control_VisualUserAnalytics_RadioButton_Month.TabStop = true;
            Control_VisualUserAnalytics_RadioButton_Month.Text = "Last 30 Days";
            Control_VisualUserAnalytics_RadioButton_Month.UseVisualStyleBackColor = true;
            // 
            // Control_VisualUserAnalytics_RadioButton_Custom
            // 
            Control_VisualUserAnalytics_RadioButton_Custom.AutoSize = true;
            Control_VisualUserAnalytics_RadioButton_Custom.Location = new Point(250, 3);
            Control_VisualUserAnalytics_RadioButton_Custom.Name = "Control_VisualUserAnalytics_RadioButton_Custom";
            Control_VisualUserAnalytics_RadioButton_Custom.Size = new Size(67, 19);
            Control_VisualUserAnalytics_RadioButton_Custom.TabIndex = 3;
            Control_VisualUserAnalytics_RadioButton_Custom.Text = "Custom";
            Control_VisualUserAnalytics_RadioButton_Custom.UseVisualStyleBackColor = true;
            // 
            // Control_VisualUserAnalytics_Label_StartDate
            // 
            Control_VisualUserAnalytics_Label_StartDate.AutoSize = true;
            Control_VisualUserAnalytics_Label_StartDate.Dock = DockStyle.Fill;
            Control_VisualUserAnalytics_Label_StartDate.Location = new Point(3, 34);
            Control_VisualUserAnalytics_Label_StartDate.Margin = new Padding(3);
            Control_VisualUserAnalytics_Label_StartDate.Name = "Control_VisualUserAnalytics_Label_StartDate";
            Control_VisualUserAnalytics_Label_StartDate.Size = new Size(61, 23);
            Control_VisualUserAnalytics_Label_StartDate.TabIndex = 6;
            Control_VisualUserAnalytics_Label_StartDate.Text = "Start Date:";
            Control_VisualUserAnalytics_Label_StartDate.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Control_VisualUserAnalytics_DateTimePicker_StartDate
            // 
            Control_VisualUserAnalytics_DateTimePicker_StartDate.Dock = DockStyle.Fill;
            Control_VisualUserAnalytics_DateTimePicker_StartDate.Format = DateTimePickerFormat.Short;
            Control_VisualUserAnalytics_DateTimePicker_StartDate.Location = new Point(70, 34);
            Control_VisualUserAnalytics_DateTimePicker_StartDate.Name = "Control_VisualUserAnalytics_DateTimePicker_StartDate";
            Control_VisualUserAnalytics_DateTimePicker_StartDate.Size = new Size(308, 23);
            Control_VisualUserAnalytics_DateTimePicker_StartDate.TabIndex = 7;
            // 
            // Control_VisualUserAnalytics_Label_EndDate
            // 
            Control_VisualUserAnalytics_Label_EndDate.AutoSize = true;
            Control_VisualUserAnalytics_Label_EndDate.Dock = DockStyle.Fill;
            Control_VisualUserAnalytics_Label_EndDate.Location = new Point(3, 63);
            Control_VisualUserAnalytics_Label_EndDate.Margin = new Padding(3);
            Control_VisualUserAnalytics_Label_EndDate.Name = "Control_VisualUserAnalytics_Label_EndDate";
            Control_VisualUserAnalytics_Label_EndDate.Size = new Size(61, 23);
            Control_VisualUserAnalytics_Label_EndDate.TabIndex = 8;
            Control_VisualUserAnalytics_Label_EndDate.Text = "End Date:";
            Control_VisualUserAnalytics_Label_EndDate.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Control_VisualUserAnalytics_DateTimePicker_EndDate
            // 
            Control_VisualUserAnalytics_DateTimePicker_EndDate.Dock = DockStyle.Fill;
            Control_VisualUserAnalytics_DateTimePicker_EndDate.Format = DateTimePickerFormat.Short;
            Control_VisualUserAnalytics_DateTimePicker_EndDate.Location = new Point(70, 63);
            Control_VisualUserAnalytics_DateTimePicker_EndDate.Name = "Control_VisualUserAnalytics_DateTimePicker_EndDate";
            Control_VisualUserAnalytics_DateTimePicker_EndDate.Size = new Size(308, 23);
            Control_VisualUserAnalytics_DateTimePicker_EndDate.TabIndex = 9;
            // 
            // Control_VisualUserAnalytics_FlowPanel_Shifts
            // 
            Control_VisualUserAnalytics_FlowPanel_Shifts.AutoSize = true;
            Control_VisualUserAnalytics_TableLayout_Content.SetColumnSpan(Control_VisualUserAnalytics_FlowPanel_Shifts, 2);
            Control_VisualUserAnalytics_FlowPanel_Shifts.Controls.Add(Control_VisualUserAnalytics_CheckBox_Shift1);
            Control_VisualUserAnalytics_FlowPanel_Shifts.Controls.Add(Control_VisualUserAnalytics_CheckBox_Shift2);
            Control_VisualUserAnalytics_FlowPanel_Shifts.Controls.Add(Control_VisualUserAnalytics_CheckBox_Shift3);
            Control_VisualUserAnalytics_FlowPanel_Shifts.Controls.Add(Control_VisualUserAnalytics_CheckBox_ShiftWeekend);
            Control_VisualUserAnalytics_FlowPanel_Shifts.Dock = DockStyle.Fill;
            Control_VisualUserAnalytics_FlowPanel_Shifts.Location = new Point(3, 92);
            Control_VisualUserAnalytics_FlowPanel_Shifts.Name = "Control_VisualUserAnalytics_FlowPanel_Shifts";
            Control_VisualUserAnalytics_FlowPanel_Shifts.Size = new Size(375, 25);
            Control_VisualUserAnalytics_FlowPanel_Shifts.TabIndex = 13;
            // 
            // Control_VisualUserAnalytics_CheckBox_Shift1
            // 
            Control_VisualUserAnalytics_CheckBox_Shift1.AutoSize = true;
            Control_VisualUserAnalytics_CheckBox_Shift1.Checked = true;
            Control_VisualUserAnalytics_CheckBox_Shift1.CheckState = CheckState.Checked;
            Control_VisualUserAnalytics_CheckBox_Shift1.Location = new Point(3, 3);
            Control_VisualUserAnalytics_CheckBox_Shift1.Name = "Control_VisualUserAnalytics_CheckBox_Shift1";
            Control_VisualUserAnalytics_CheckBox_Shift1.Size = new Size(59, 19);
            Control_VisualUserAnalytics_CheckBox_Shift1.TabIndex = 0;
            Control_VisualUserAnalytics_CheckBox_Shift1.Text = "Shift 1";
            Control_VisualUserAnalytics_CheckBox_Shift1.UseVisualStyleBackColor = true;
            // 
            // Control_VisualUserAnalytics_CheckBox_Shift2
            // 
            Control_VisualUserAnalytics_CheckBox_Shift2.AutoSize = true;
            Control_VisualUserAnalytics_CheckBox_Shift2.Checked = true;
            Control_VisualUserAnalytics_CheckBox_Shift2.CheckState = CheckState.Checked;
            Control_VisualUserAnalytics_CheckBox_Shift2.Location = new Point(68, 3);
            Control_VisualUserAnalytics_CheckBox_Shift2.Name = "Control_VisualUserAnalytics_CheckBox_Shift2";
            Control_VisualUserAnalytics_CheckBox_Shift2.Size = new Size(59, 19);
            Control_VisualUserAnalytics_CheckBox_Shift2.TabIndex = 1;
            Control_VisualUserAnalytics_CheckBox_Shift2.Text = "Shift 2";
            Control_VisualUserAnalytics_CheckBox_Shift2.UseVisualStyleBackColor = true;
            // 
            // Control_VisualUserAnalytics_CheckBox_Shift3
            // 
            Control_VisualUserAnalytics_CheckBox_Shift3.AutoSize = true;
            Control_VisualUserAnalytics_CheckBox_Shift3.Checked = true;
            Control_VisualUserAnalytics_CheckBox_Shift3.CheckState = CheckState.Checked;
            Control_VisualUserAnalytics_CheckBox_Shift3.Location = new Point(133, 3);
            Control_VisualUserAnalytics_CheckBox_Shift3.Name = "Control_VisualUserAnalytics_CheckBox_Shift3";
            Control_VisualUserAnalytics_CheckBox_Shift3.Size = new Size(59, 19);
            Control_VisualUserAnalytics_CheckBox_Shift3.TabIndex = 2;
            Control_VisualUserAnalytics_CheckBox_Shift3.Text = "Shift 3";
            Control_VisualUserAnalytics_CheckBox_Shift3.UseVisualStyleBackColor = true;
            // 
            // Control_VisualUserAnalytics_CheckBox_ShiftWeekend
            // 
            Control_VisualUserAnalytics_CheckBox_ShiftWeekend.AutoSize = true;
            Control_VisualUserAnalytics_CheckBox_ShiftWeekend.Checked = true;
            Control_VisualUserAnalytics_CheckBox_ShiftWeekend.CheckState = CheckState.Checked;
            Control_VisualUserAnalytics_CheckBox_ShiftWeekend.Location = new Point(198, 3);
            Control_VisualUserAnalytics_CheckBox_ShiftWeekend.Name = "Control_VisualUserAnalytics_CheckBox_ShiftWeekend";
            Control_VisualUserAnalytics_CheckBox_ShiftWeekend.Size = new Size(75, 19);
            Control_VisualUserAnalytics_CheckBox_ShiftWeekend.TabIndex = 3;
            Control_VisualUserAnalytics_CheckBox_ShiftWeekend.Text = "Weekend";
            Control_VisualUserAnalytics_CheckBox_ShiftWeekend.UseVisualStyleBackColor = true;
            // 
            // Control_VisualUserAnalytics_Button_LoadUsers
            // 
            Control_VisualUserAnalytics_TableLayout_Content.SetColumnSpan(Control_VisualUserAnalytics_Button_LoadUsers, 2);
            Control_VisualUserAnalytics_Button_LoadUsers.Dock = DockStyle.Fill;
            Control_VisualUserAnalytics_Button_LoadUsers.Location = new Point(3, 123);
            Control_VisualUserAnalytics_Button_LoadUsers.Name = "Control_VisualUserAnalytics_Button_LoadUsers";
            Control_VisualUserAnalytics_Button_LoadUsers.Size = new Size(375, 32);
            Control_VisualUserAnalytics_Button_LoadUsers.TabIndex = 10;
            Control_VisualUserAnalytics_Button_LoadUsers.Text = "Load Users";
            Control_VisualUserAnalytics_Button_LoadUsers.UseVisualStyleBackColor = true;
            // 
            // Control_VisualUserAnalytics_Button_SelectAllUsers
            // 
            Control_VisualUserAnalytics_TableLayout_Content.SetColumnSpan(Control_VisualUserAnalytics_Button_SelectAllUsers, 2);
            Control_VisualUserAnalytics_Button_SelectAllUsers.Dock = DockStyle.Fill;
            Control_VisualUserAnalytics_Button_SelectAllUsers.Location = new Point(3, 161);
            Control_VisualUserAnalytics_Button_SelectAllUsers.Name = "Control_VisualUserAnalytics_Button_SelectAllUsers";
            Control_VisualUserAnalytics_Button_SelectAllUsers.Size = new Size(375, 32);
            Control_VisualUserAnalytics_Button_SelectAllUsers.TabIndex = 11;
            Control_VisualUserAnalytics_Button_SelectAllUsers.Text = "Select All Users";
            Control_VisualUserAnalytics_Button_SelectAllUsers.UseVisualStyleBackColor = true;
            // 
            // Control_VisualUserAnalytics_Button_GenerateReport
            // 
            Control_VisualUserAnalytics_TableLayout_Content.SetColumnSpan(Control_VisualUserAnalytics_Button_GenerateReport, 2);
            Control_VisualUserAnalytics_Button_GenerateReport.Dock = DockStyle.Fill;
            Control_VisualUserAnalytics_Button_GenerateReport.Enabled = false;
            Control_VisualUserAnalytics_Button_GenerateReport.Location = new Point(3, 199);
            Control_VisualUserAnalytics_Button_GenerateReport.Name = "Control_VisualUserAnalytics_Button_GenerateReport";
            Control_VisualUserAnalytics_Button_GenerateReport.Size = new Size(375, 32);
            Control_VisualUserAnalytics_Button_GenerateReport.TabIndex = 2;
            Control_VisualUserAnalytics_Button_GenerateReport.Text = "Generate Report";
            Control_VisualUserAnalytics_Button_GenerateReport.UseVisualStyleBackColor = true;
            // 
            // Control_VisualUserAnalytics_Label_UserCount
            // 
            Control_VisualUserAnalytics_Label_UserCount.AutoSize = true;
            Control_VisualUserAnalytics_TableLayout_Content.SetColumnSpan(Control_VisualUserAnalytics_Label_UserCount, 2);
            Control_VisualUserAnalytics_Label_UserCount.Dock = DockStyle.Fill;
            Control_VisualUserAnalytics_Label_UserCount.Location = new Point(3, 237);
            Control_VisualUserAnalytics_Label_UserCount.Margin = new Padding(3);
            Control_VisualUserAnalytics_Label_UserCount.Name = "Control_VisualUserAnalytics_Label_UserCount";
            Control_VisualUserAnalytics_Label_UserCount.Size = new Size(375, 15);
            Control_VisualUserAnalytics_Label_UserCount.TabIndex = 6;
            Control_VisualUserAnalytics_Label_UserCount.Text = "Selected: 0";
            Control_VisualUserAnalytics_Label_UserCount.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_VisualUserAnalytics_GroupBox_Instructions
            // 
            Control_VisualUserAnalytics_GroupBox_Instructions.Controls.Add(Control_VisualUserAnalytics_CheckedListBox_Instructions);
            Control_VisualUserAnalytics_GroupBox_Instructions.Dock = DockStyle.Fill;
            Control_VisualUserAnalytics_GroupBox_Instructions.Location = new Point(387, 6);
            Control_VisualUserAnalytics_GroupBox_Instructions.Margin = new Padding(6);
            Control_VisualUserAnalytics_GroupBox_Instructions.Name = "Control_VisualUserAnalytics_GroupBox_Instructions";
            Control_VisualUserAnalytics_TableLayout_Content.SetRowSpan(Control_VisualUserAnalytics_GroupBox_Instructions, 8);
            Control_VisualUserAnalytics_GroupBox_Instructions.Size = new Size(372, 243);
            Control_VisualUserAnalytics_GroupBox_Instructions.TabIndex = 12;
            Control_VisualUserAnalytics_GroupBox_Instructions.TabStop = false;
            Control_VisualUserAnalytics_GroupBox_Instructions.Text = "Using This Tab";
            // 
            // Control_VisualUserAnalytics_CheckedListBox_Instructions
            // 
            Control_VisualUserAnalytics_CheckedListBox_Instructions.Dock = DockStyle.Fill;
            Control_VisualUserAnalytics_CheckedListBox_Instructions.FormattingEnabled = true;
            Control_VisualUserAnalytics_CheckedListBox_Instructions.Items.AddRange(new object[] { "Step 1: Enter Desired Date Range", "Step 2: Click Load Users", "Step 3 (Optional): Click Select All Users", "Step 4: Select Users Below", "Step 5: Click Generate Report (Will Open a new Window)" });
            Control_VisualUserAnalytics_CheckedListBox_Instructions.Location = new Point(3, 19);
            Control_VisualUserAnalytics_CheckedListBox_Instructions.Name = "Control_VisualUserAnalytics_CheckedListBox_Instructions";
            Control_VisualUserAnalytics_CheckedListBox_Instructions.Size = new Size(366, 221);
            Control_VisualUserAnalytics_CheckedListBox_Instructions.TabIndex = 0;
            // 
            // Control_VisualUserAnalytics_CheckedListBox_Users
            // 
            Control_VisualUserAnalytics_CheckedListBox_Users.Dock = DockStyle.Fill;
            Control_VisualUserAnalytics_CheckedListBox_Users.FormattingEnabled = true;
            Control_VisualUserAnalytics_CheckedListBox_Users.Location = new Point(16, 277);
            Control_VisualUserAnalytics_CheckedListBox_Users.Margin = new Padding(6);
            Control_VisualUserAnalytics_CheckedListBox_Users.Name = "Control_VisualUserAnalytics_CheckedListBox_Users";
            Control_VisualUserAnalytics_CheckedListBox_Users.Size = new Size(766, 263);
            Control_VisualUserAnalytics_CheckedListBox_Users.TabIndex = 5;
            // 
            // Control_VisualUserAnalytics
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            Controls.Add(Control_VisualUserAnalytics_TableLayout_Main);
            Name = "Control_VisualUserAnalytics";
            Size = new Size(798, 556);
            Control_VisualUserAnalytics_TableLayout_Main.ResumeLayout(false);
            Control_VisualUserAnalytics_TableLayout_Main.PerformLayout();
            Control_VisualUserAnalytics_TableLayout_Content.ResumeLayout(false);
            Control_VisualUserAnalytics_TableLayout_Content.PerformLayout();
            Control_VisualUserAnalytics_FlowPanel_DateRanges.ResumeLayout(false);
            Control_VisualUserAnalytics_FlowPanel_DateRanges.PerformLayout();
            Control_VisualUserAnalytics_FlowPanel_Shifts.ResumeLayout(false);
            Control_VisualUserAnalytics_FlowPanel_Shifts.PerformLayout();
            Control_VisualUserAnalytics_GroupBox_Instructions.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel Control_VisualUserAnalytics_TableLayout_Main;
        private TableLayoutPanel Control_VisualUserAnalytics_TableLayout_Content;
        private FlowLayoutPanel Control_VisualUserAnalytics_FlowPanel_DateRanges;
        private RadioButton Control_VisualUserAnalytics_RadioButton_Today;
        private RadioButton Control_VisualUserAnalytics_RadioButton_Week;
        private RadioButton Control_VisualUserAnalytics_RadioButton_Month;
        private RadioButton Control_VisualUserAnalytics_RadioButton_Custom;
        private Label Control_VisualUserAnalytics_Label_StartDate;
        private DateTimePicker Control_VisualUserAnalytics_DateTimePicker_StartDate;
        private Label Control_VisualUserAnalytics_Label_EndDate;
        private DateTimePicker Control_VisualUserAnalytics_DateTimePicker_EndDate;
        private FlowLayoutPanel Control_VisualUserAnalytics_FlowPanel_Shifts;
        private CheckBox Control_VisualUserAnalytics_CheckBox_Shift1;
        private CheckBox Control_VisualUserAnalytics_CheckBox_Shift2;
        private CheckBox Control_VisualUserAnalytics_CheckBox_Shift3;
        private CheckBox Control_VisualUserAnalytics_CheckBox_ShiftWeekend;
        private Button Control_VisualUserAnalytics_Button_LoadUsers;
        private Button Control_VisualUserAnalytics_Button_SelectAllUsers;
        private Button Control_VisualUserAnalytics_Button_GenerateReport;
        private Label Control_VisualUserAnalytics_Label_UserCount;
        private GroupBox Control_VisualUserAnalytics_GroupBox_Instructions;
        private CheckedListBox Control_VisualUserAnalytics_CheckedListBox_Instructions;
        private CheckedListBox Control_VisualUserAnalytics_CheckedListBox_Users;
    }
}
