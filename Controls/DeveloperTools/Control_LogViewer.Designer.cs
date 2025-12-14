namespace MTM_WIP_Application_Winforms.Controls.DeveloperTools
{
    partial class Control_LogViewer
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
            this.Control_LogViewer_Panel_Controls = new System.Windows.Forms.Panel();
            this.Control_LogViewer_Label_Count = new System.Windows.Forms.Label();
            this.Control_LogViewer_Button_Export = new System.Windows.Forms.Button();
            this.Control_LogViewer_CheckBox_Critical = new System.Windows.Forms.CheckBox();
            this.Control_LogViewer_CheckBox_Error = new System.Windows.Forms.CheckBox();
            this.Control_LogViewer_CheckBox_Warning = new System.Windows.Forms.CheckBox();
            this.Control_LogViewer_CheckBox_Info = new System.Windows.Forms.CheckBox();
            this.Control_LogViewer_Label_Severity = new System.Windows.Forms.Label();
            this.Control_LogViewer_DateTimePicker_To = new System.Windows.Forms.DateTimePicker();
            this.Control_LogViewer_Label_To = new System.Windows.Forms.Label();
            this.Control_LogViewer_DateTimePicker_From = new System.Windows.Forms.DateTimePicker();
            this.Control_LogViewer_Label_From = new System.Windows.Forms.Label();
            this.Control_LogViewer_ComboBox_DateRange = new System.Windows.Forms.ComboBox();
            this.Control_LogViewer_Label_Date = new System.Windows.Forms.Label();
            this.Control_LogViewer_CheckBox_Regex = new System.Windows.Forms.CheckBox();
            this.Control_LogViewer_TextBox_Search = new System.Windows.Forms.TextBox();
            this.Control_LogViewer_Label_Search = new System.Windows.Forms.Label();
            this.Control_LogViewer_Label_GroupBy = new System.Windows.Forms.Label();
            this.Control_LogViewer_ComboBox_GroupBy = new System.Windows.Forms.ComboBox();
            this.Control_LogViewer_SplitContainer_Main = new System.Windows.Forms.SplitContainer();
            this.Control_LogViewer_DataGridView_Logs = new System.Windows.Forms.DataGridView();
            this.Control_LogViewer_Panel_Details = new System.Windows.Forms.Panel();
            this.Control_LogViewer_TextBox_Details = new System.Windows.Forms.TextBox();
            this.Control_LogViewer_Panel_DetailsHeader = new System.Windows.Forms.Panel();
            this.Control_LogViewer_Button_Copy = new System.Windows.Forms.Button();
            this.Control_LogViewer_Label_Details = new System.Windows.Forms.Label();
            this.Control_LogViewer_Panel_Controls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Control_LogViewer_SplitContainer_Main)).BeginInit();
            this.Control_LogViewer_SplitContainer_Main.Panel1.SuspendLayout();
            this.Control_LogViewer_SplitContainer_Main.Panel2.SuspendLayout();
            this.Control_LogViewer_SplitContainer_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Control_LogViewer_DataGridView_Logs)).BeginInit();
            this.Control_LogViewer_Panel_Details.SuspendLayout();
            this.Control_LogViewer_Panel_DetailsHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // Control_LogViewer_Panel_Controls
            // 
            this.Control_LogViewer_Panel_Controls.Controls.Add(this.Control_LogViewer_Label_Count);
            this.Control_LogViewer_Panel_Controls.Controls.Add(this.Control_LogViewer_Button_Export);
            this.Control_LogViewer_Panel_Controls.Controls.Add(this.Control_LogViewer_CheckBox_Critical);
            this.Control_LogViewer_Panel_Controls.Controls.Add(this.Control_LogViewer_CheckBox_Error);
            this.Control_LogViewer_Panel_Controls.Controls.Add(this.Control_LogViewer_CheckBox_Warning);
            this.Control_LogViewer_Panel_Controls.Controls.Add(this.Control_LogViewer_CheckBox_Info);
            this.Control_LogViewer_Panel_Controls.Controls.Add(this.Control_LogViewer_Label_Severity);
            this.Control_LogViewer_Panel_Controls.Controls.Add(this.Control_LogViewer_DateTimePicker_To);
            this.Control_LogViewer_Panel_Controls.Controls.Add(this.Control_LogViewer_Label_To);
            this.Control_LogViewer_Panel_Controls.Controls.Add(this.Control_LogViewer_DateTimePicker_From);
            this.Control_LogViewer_Panel_Controls.Controls.Add(this.Control_LogViewer_Label_From);
            this.Control_LogViewer_Panel_Controls.Controls.Add(this.Control_LogViewer_ComboBox_DateRange);
            this.Control_LogViewer_Panel_Controls.Controls.Add(this.Control_LogViewer_Label_Date);
            this.Control_LogViewer_Panel_Controls.Controls.Add(this.Control_LogViewer_CheckBox_Regex);
            this.Control_LogViewer_Panel_Controls.Controls.Add(this.Control_LogViewer_TextBox_Search);
            this.Control_LogViewer_Panel_Controls.Controls.Add(this.Control_LogViewer_Label_Search);
            this.Control_LogViewer_Panel_Controls.Controls.Add(this.Control_LogViewer_ComboBox_GroupBy);
            this.Control_LogViewer_Panel_Controls.Controls.Add(this.Control_LogViewer_Label_GroupBy);
            this.Control_LogViewer_Panel_Controls.Dock = System.Windows.Forms.DockStyle.Top;
            this.Control_LogViewer_Panel_Controls.Location = new System.Drawing.Point(0, 0);
            this.Control_LogViewer_Panel_Controls.Name = "Control_LogViewer_Panel_Controls";
            this.Control_LogViewer_Panel_Controls.Size = new System.Drawing.Size(900, 70);
            this.Control_LogViewer_Panel_Controls.TabIndex = 0;
            // 
            // Control_LogViewer_Label_GroupBy
            // 
            this.Control_LogViewer_Label_GroupBy.AutoSize = true;
            this.Control_LogViewer_Label_GroupBy.Location = new System.Drawing.Point(330, 41);
            this.Control_LogViewer_Label_GroupBy.Name = "Control_LogViewer_Label_GroupBy";
            this.Control_LogViewer_Label_GroupBy.Size = new System.Drawing.Size(59, 15);
            this.Control_LogViewer_Label_GroupBy.TabIndex = 16;
            this.Control_LogViewer_Label_GroupBy.Text = "Group By:";
            // 
            // Control_LogViewer_ComboBox_GroupBy
            // 
            this.Control_LogViewer_ComboBox_GroupBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Control_LogViewer_ComboBox_GroupBy.FormattingEnabled = true;
            this.Control_LogViewer_ComboBox_GroupBy.Location = new System.Drawing.Point(395, 38);
            this.Control_LogViewer_ComboBox_GroupBy.Name = "Control_LogViewer_ComboBox_GroupBy";
            this.Control_LogViewer_ComboBox_GroupBy.Size = new System.Drawing.Size(120, 23);
            this.Control_LogViewer_ComboBox_GroupBy.TabIndex = 17;
            // 
            // Control_LogViewer_Label_Count
            // 
            this.Control_LogViewer_Label_Count.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Control_LogViewer_Label_Count.AutoSize = true;
            this.Control_LogViewer_Label_Count.ForeColor = System.Drawing.Color.Gray;
            this.Control_LogViewer_Label_Count.Location = new System.Drawing.Point(780, 45);
            this.Control_LogViewer_Label_Count.Name = "Control_LogViewer_Label_Count";
            this.Control_LogViewer_Label_Count.Size = new System.Drawing.Size(87, 15);
            this.Control_LogViewer_Label_Count.TabIndex = 15;
            this.Control_LogViewer_Label_Count.Text = "0 entries found";
            // 
            // Control_LogViewer_Button_Export
            // 
            this.Control_LogViewer_Button_Export.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Control_LogViewer_Button_Export.Location = new System.Drawing.Point(812, 10);
            this.Control_LogViewer_Button_Export.Name = "Control_LogViewer_Button_Export";
            this.Control_LogViewer_Button_Export.Size = new System.Drawing.Size(75, 23);
            this.Control_LogViewer_Button_Export.TabIndex = 14;
            this.Control_LogViewer_Button_Export.Text = "Export";
            this.Control_LogViewer_Button_Export.UseVisualStyleBackColor = true;
            // 
            // Control_LogViewer_CheckBox_Critical
            // 
            this.Control_LogViewer_CheckBox_Critical.AutoSize = true;
            this.Control_LogViewer_CheckBox_Critical.Checked = true;
            this.Control_LogViewer_CheckBox_Critical.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Control_LogViewer_CheckBox_Critical.ForeColor = System.Drawing.Color.Red;
            this.Control_LogViewer_CheckBox_Critical.Location = new System.Drawing.Point(260, 40);
            this.Control_LogViewer_CheckBox_Critical.Name = "Control_LogViewer_CheckBox_Critical";
            this.Control_LogViewer_CheckBox_Critical.Size = new System.Drawing.Size(63, 19);
            this.Control_LogViewer_CheckBox_Critical.TabIndex = 13;
            this.Control_LogViewer_CheckBox_Critical.Text = "Critical";
            this.Control_LogViewer_CheckBox_Critical.UseVisualStyleBackColor = true;
            // 
            // Control_LogViewer_CheckBox_Error
            // 
            this.Control_LogViewer_CheckBox_Error.AutoSize = true;
            this.Control_LogViewer_CheckBox_Error.Checked = true;
            this.Control_LogViewer_CheckBox_Error.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Control_LogViewer_CheckBox_Error.ForeColor = System.Drawing.Color.Red;
            this.Control_LogViewer_CheckBox_Error.Location = new System.Drawing.Point(200, 40);
            this.Control_LogViewer_CheckBox_Error.Name = "Control_LogViewer_CheckBox_Error";
            this.Control_LogViewer_CheckBox_Error.Size = new System.Drawing.Size(51, 19);
            this.Control_LogViewer_CheckBox_Error.TabIndex = 12;
            this.Control_LogViewer_CheckBox_Error.Text = "Error";
            this.Control_LogViewer_CheckBox_Error.UseVisualStyleBackColor = true;
            // 
            // Control_LogViewer_CheckBox_Warning
            // 
            this.Control_LogViewer_CheckBox_Warning.AutoSize = true;
            this.Control_LogViewer_CheckBox_Warning.Checked = true;
            this.Control_LogViewer_CheckBox_Warning.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Control_LogViewer_CheckBox_Warning.ForeColor = System.Drawing.Color.Orange;
            this.Control_LogViewer_CheckBox_Warning.Location = new System.Drawing.Point(120, 40);
            this.Control_LogViewer_CheckBox_Warning.Name = "Control_LogViewer_CheckBox_Warning";
            this.Control_LogViewer_CheckBox_Warning.Size = new System.Drawing.Size(71, 19);
            this.Control_LogViewer_CheckBox_Warning.TabIndex = 11;
            this.Control_LogViewer_CheckBox_Warning.Text = "Warning";
            this.Control_LogViewer_CheckBox_Warning.UseVisualStyleBackColor = true;
            // 
            // Control_LogViewer_CheckBox_Info
            // 
            this.Control_LogViewer_CheckBox_Info.AutoSize = true;
            this.Control_LogViewer_CheckBox_Info.Location = new System.Drawing.Point(70, 40);
            this.Control_LogViewer_CheckBox_Info.Name = "Control_LogViewer_CheckBox_Info";
            this.Control_LogViewer_CheckBox_Info.Size = new System.Drawing.Size(47, 19);
            this.Control_LogViewer_CheckBox_Info.TabIndex = 10;
            this.Control_LogViewer_CheckBox_Info.Text = "Info";
            this.Control_LogViewer_CheckBox_Info.UseVisualStyleBackColor = true;
            // 
            // Control_LogViewer_Label_Severity
            // 
            this.Control_LogViewer_Label_Severity.AutoSize = true;
            this.Control_LogViewer_Label_Severity.Location = new System.Drawing.Point(10, 41);
            this.Control_LogViewer_Label_Severity.Name = "Control_LogViewer_Label_Severity";
            this.Control_LogViewer_Label_Severity.Size = new System.Drawing.Size(51, 15);
            this.Control_LogViewer_Label_Severity.TabIndex = 9;
            this.Control_LogViewer_Label_Severity.Text = "Severity:";
            // 
            // Control_LogViewer_DateTimePicker_To
            // 
            this.Control_LogViewer_DateTimePicker_To.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Control_LogViewer_DateTimePicker_To.Location = new System.Drawing.Point(650, 10);
            this.Control_LogViewer_DateTimePicker_To.Name = "Control_LogViewer_DateTimePicker_To";
            this.Control_LogViewer_DateTimePicker_To.Size = new System.Drawing.Size(100, 23);
            this.Control_LogViewer_DateTimePicker_To.TabIndex = 8;
            this.Control_LogViewer_DateTimePicker_To.Visible = false;
            // 
            // Control_LogViewer_Label_To
            // 
            this.Control_LogViewer_Label_To.AutoSize = true;
            this.Control_LogViewer_Label_To.Location = new System.Drawing.Point(620, 13);
            this.Control_LogViewer_Label_To.Name = "Control_LogViewer_Label_To";
            this.Control_LogViewer_Label_To.Size = new System.Drawing.Size(22, 15);
            this.Control_LogViewer_Label_To.TabIndex = 7;
            this.Control_LogViewer_Label_To.Text = "To:";
            this.Control_LogViewer_Label_To.Visible = false;
            // 
            // Control_LogViewer_DateTimePicker_From
            // 
            this.Control_LogViewer_DateTimePicker_From.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Control_LogViewer_DateTimePicker_From.Location = new System.Drawing.Point(510, 10);
            this.Control_LogViewer_DateTimePicker_From.Name = "Control_LogViewer_DateTimePicker_From";
            this.Control_LogViewer_DateTimePicker_From.Size = new System.Drawing.Size(100, 23);
            this.Control_LogViewer_DateTimePicker_From.TabIndex = 6;
            this.Control_LogViewer_DateTimePicker_From.Visible = false;
            // 
            // Control_LogViewer_Label_From
            // 
            this.Control_LogViewer_Label_From.AutoSize = true;
            this.Control_LogViewer_Label_From.Location = new System.Drawing.Point(465, 13);
            this.Control_LogViewer_Label_From.Name = "Control_LogViewer_Label_From";
            this.Control_LogViewer_Label_From.Size = new System.Drawing.Size(38, 15);
            this.Control_LogViewer_Label_From.TabIndex = 5;
            this.Control_LogViewer_Label_From.Text = "From:";
            this.Control_LogViewer_Label_From.Visible = false;
            // 
            // Control_LogViewer_ComboBox_DateRange
            // 
            this.Control_LogViewer_ComboBox_DateRange.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Control_LogViewer_ComboBox_DateRange.FormattingEnabled = true;
            this.Control_LogViewer_ComboBox_DateRange.Location = new System.Drawing.Point(330, 10);
            this.Control_LogViewer_ComboBox_DateRange.Name = "Control_LogViewer_ComboBox_DateRange";
            this.Control_LogViewer_ComboBox_DateRange.Size = new System.Drawing.Size(120, 23);
            this.Control_LogViewer_ComboBox_DateRange.TabIndex = 4;
            // 
            // Control_LogViewer_Label_Date
            // 
            this.Control_LogViewer_Label_Date.AutoSize = true;
            this.Control_LogViewer_Label_Date.Location = new System.Drawing.Point(290, 13);
            this.Control_LogViewer_Label_Date.Name = "Control_LogViewer_Label_Date";
            this.Control_LogViewer_Label_Date.Size = new System.Drawing.Size(34, 15);
            this.Control_LogViewer_Label_Date.TabIndex = 3;
            this.Control_LogViewer_Label_Date.Text = "Date:";
            // 
            // Control_LogViewer_CheckBox_Regex
            // 
            this.Control_LogViewer_CheckBox_Regex.AutoSize = true;
            this.Control_LogViewer_CheckBox_Regex.Location = new System.Drawing.Point(220, 12);
            this.Control_LogViewer_CheckBox_Regex.Name = "Control_LogViewer_CheckBox_Regex";
            this.Control_LogViewer_CheckBox_Regex.Size = new System.Drawing.Size(58, 19);
            this.Control_LogViewer_CheckBox_Regex.TabIndex = 2;
            this.Control_LogViewer_CheckBox_Regex.Text = "Regex";
            this.Control_LogViewer_CheckBox_Regex.UseVisualStyleBackColor = true;
            // 
            // Control_LogViewer_TextBox_Search
            // 
            this.Control_LogViewer_TextBox_Search.Location = new System.Drawing.Point(60, 10);
            this.Control_LogViewer_TextBox_Search.Name = "Control_LogViewer_TextBox_Search";
            this.Control_LogViewer_TextBox_Search.Size = new System.Drawing.Size(150, 23);
            this.Control_LogViewer_TextBox_Search.TabIndex = 1;
            // 
            // Control_LogViewer_Label_Search
            // 
            this.Control_LogViewer_Label_Search.AutoSize = true;
            this.Control_LogViewer_Label_Search.Location = new System.Drawing.Point(10, 13);
            this.Control_LogViewer_Label_Search.Name = "Control_LogViewer_Label_Search";
            this.Control_LogViewer_Label_Search.Size = new System.Drawing.Size(45, 15);
            this.Control_LogViewer_Label_Search.TabIndex = 0;
            this.Control_LogViewer_Label_Search.Text = "Search:";
            // 
            // Control_LogViewer_SplitContainer_Main
            // 
            this.Control_LogViewer_SplitContainer_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Control_LogViewer_SplitContainer_Main.Location = new System.Drawing.Point(0, 70);
            this.Control_LogViewer_SplitContainer_Main.Name = "Control_LogViewer_SplitContainer_Main";
            this.Control_LogViewer_SplitContainer_Main.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // Control_LogViewer_SplitContainer_Main.Panel1
            // 
            this.Control_LogViewer_SplitContainer_Main.Panel1.Controls.Add(this.Control_LogViewer_DataGridView_Logs);
            // 
            // Control_LogViewer_SplitContainer_Main.Panel2
            // 
            this.Control_LogViewer_SplitContainer_Main.Panel2.Controls.Add(this.Control_LogViewer_Panel_Details);
            this.Control_LogViewer_SplitContainer_Main.Size = new System.Drawing.Size(900, 530);
            this.Control_LogViewer_SplitContainer_Main.SplitterDistance = 350;
            this.Control_LogViewer_SplitContainer_Main.TabIndex = 1;
            // 
            // Control_LogViewer_DataGridView_Logs
            // 
            this.Control_LogViewer_DataGridView_Logs.AllowUserToAddRows = false;
            this.Control_LogViewer_DataGridView_Logs.AllowUserToDeleteRows = false;
            this.Control_LogViewer_DataGridView_Logs.AllowUserToResizeRows = false;
            this.Control_LogViewer_DataGridView_Logs.BackgroundColor = System.Drawing.Color.White;
            this.Control_LogViewer_DataGridView_Logs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Control_LogViewer_DataGridView_Logs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Control_LogViewer_DataGridView_Logs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Control_LogViewer_DataGridView_Logs.Location = new System.Drawing.Point(0, 0);
            this.Control_LogViewer_DataGridView_Logs.MultiSelect = false;
            this.Control_LogViewer_DataGridView_Logs.Name = "Control_LogViewer_DataGridView_Logs";
            this.Control_LogViewer_DataGridView_Logs.ReadOnly = true;
            this.Control_LogViewer_DataGridView_Logs.RowHeadersVisible = false;
            this.Control_LogViewer_DataGridView_Logs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Control_LogViewer_DataGridView_Logs.Size = new System.Drawing.Size(900, 350);
            this.Control_LogViewer_DataGridView_Logs.TabIndex = 0;
            // 
            // Control_LogViewer_Panel_Details
            // 
            this.Control_LogViewer_Panel_Details.Controls.Add(this.Control_LogViewer_TextBox_Details);
            this.Control_LogViewer_Panel_Details.Controls.Add(this.Control_LogViewer_Panel_DetailsHeader);
            this.Control_LogViewer_Panel_Details.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Control_LogViewer_Panel_Details.Location = new System.Drawing.Point(0, 0);
            this.Control_LogViewer_Panel_Details.Name = "Control_LogViewer_Panel_Details";
            this.Control_LogViewer_Panel_Details.Size = new System.Drawing.Size(900, 176);
            this.Control_LogViewer_Panel_Details.TabIndex = 0;
            // 
            // Control_LogViewer_TextBox_Details
            // 
            this.Control_LogViewer_TextBox_Details.BackColor = System.Drawing.Color.White;
            this.Control_LogViewer_TextBox_Details.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Control_LogViewer_TextBox_Details.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Control_LogViewer_TextBox_Details.Location = new System.Drawing.Point(0, 30);
            this.Control_LogViewer_TextBox_Details.Multiline = true;
            this.Control_LogViewer_TextBox_Details.Name = "Control_LogViewer_TextBox_Details";
            this.Control_LogViewer_TextBox_Details.ReadOnly = true;
            this.Control_LogViewer_TextBox_Details.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Control_LogViewer_TextBox_Details.Size = new System.Drawing.Size(900, 146);
            this.Control_LogViewer_TextBox_Details.TabIndex = 1;
            // 
            // Control_LogViewer_Panel_DetailsHeader
            // 
            this.Control_LogViewer_Panel_DetailsHeader.Controls.Add(this.Control_LogViewer_Button_Copy);
            this.Control_LogViewer_Panel_DetailsHeader.Controls.Add(this.Control_LogViewer_Label_Details);
            this.Control_LogViewer_Panel_DetailsHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.Control_LogViewer_Panel_DetailsHeader.Location = new System.Drawing.Point(0, 0);
            this.Control_LogViewer_Panel_DetailsHeader.Name = "Control_LogViewer_Panel_DetailsHeader";
            this.Control_LogViewer_Panel_DetailsHeader.Size = new System.Drawing.Size(900, 30);
            this.Control_LogViewer_Panel_DetailsHeader.TabIndex = 0;
            // 
            // Control_LogViewer_Button_Copy
            // 
            this.Control_LogViewer_Button_Copy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Control_LogViewer_Button_Copy.Location = new System.Drawing.Point(812, 4);
            this.Control_LogViewer_Button_Copy.Name = "Control_LogViewer_Button_Copy";
            this.Control_LogViewer_Button_Copy.Size = new System.Drawing.Size(75, 23);
            this.Control_LogViewer_Button_Copy.TabIndex = 1;
            this.Control_LogViewer_Button_Copy.Text = "Copy";
            this.Control_LogViewer_Button_Copy.UseVisualStyleBackColor = true;
            // 
            // Control_LogViewer_Label_Details
            // 
            this.Control_LogViewer_Label_Details.AutoSize = true;
            this.Control_LogViewer_Label_Details.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Control_LogViewer_Label_Details.Location = new System.Drawing.Point(10, 8);
            this.Control_LogViewer_Label_Details.Name = "Control_LogViewer_Label_Details";
            this.Control_LogViewer_Label_Details.Size = new System.Drawing.Size(71, 15);
            this.Control_LogViewer_Label_Details.TabIndex = 0;
            this.Control_LogViewer_Label_Details.Text = "Log Details";
            // 
            // Control_LogViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Control_LogViewer_SplitContainer_Main);
            this.Controls.Add(this.Control_LogViewer_Panel_Controls);
            this.Name = "Control_LogViewer";
            this.Size = new System.Drawing.Size(900, 600);
            this.Control_LogViewer_Panel_Controls.ResumeLayout(false);
            this.Control_LogViewer_Panel_Controls.PerformLayout();
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

        }

        #endregion

        private System.Windows.Forms.Panel Control_LogViewer_Panel_Controls;
        private System.Windows.Forms.Label Control_LogViewer_Label_Search;
        private System.Windows.Forms.TextBox Control_LogViewer_TextBox_Search;
        private System.Windows.Forms.CheckBox Control_LogViewer_CheckBox_Regex;
        private System.Windows.Forms.Label Control_LogViewer_Label_Date;
        private System.Windows.Forms.ComboBox Control_LogViewer_ComboBox_DateRange;
        private System.Windows.Forms.Label Control_LogViewer_Label_From;
        private System.Windows.Forms.DateTimePicker Control_LogViewer_DateTimePicker_From;
        private System.Windows.Forms.Label Control_LogViewer_Label_To;
        private System.Windows.Forms.DateTimePicker Control_LogViewer_DateTimePicker_To;
        private System.Windows.Forms.Label Control_LogViewer_Label_Severity;
        private System.Windows.Forms.CheckBox Control_LogViewer_CheckBox_Info;
        private System.Windows.Forms.CheckBox Control_LogViewer_CheckBox_Warning;
        private System.Windows.Forms.CheckBox Control_LogViewer_CheckBox_Error;
        private System.Windows.Forms.CheckBox Control_LogViewer_CheckBox_Critical;
        private System.Windows.Forms.Button Control_LogViewer_Button_Export;
        private System.Windows.Forms.Label Control_LogViewer_Label_Count;
        private System.Windows.Forms.SplitContainer Control_LogViewer_SplitContainer_Main;
        private System.Windows.Forms.DataGridView Control_LogViewer_DataGridView_Logs;
        private System.Windows.Forms.Panel Control_LogViewer_Panel_Details;
        private System.Windows.Forms.Panel Control_LogViewer_Panel_DetailsHeader;
        private System.Windows.Forms.Label Control_LogViewer_Label_Details;
        private System.Windows.Forms.Button Control_LogViewer_Button_Copy;
        private System.Windows.Forms.TextBox Control_LogViewer_TextBox_Details;
        private System.Windows.Forms.Label Control_LogViewer_Label_GroupBy;
        private System.Windows.Forms.ComboBox Control_LogViewer_ComboBox_GroupBy;
    }
}

