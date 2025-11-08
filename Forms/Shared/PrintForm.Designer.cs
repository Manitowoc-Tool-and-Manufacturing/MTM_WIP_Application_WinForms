namespace MTM_WIP_Application_Winforms.Forms.Shared
{
    partial class PrintForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PrintForm_TabControl_Main = new System.Windows.Forms.TabControl();
            this.PrintForm_TabPage_Preview = new System.Windows.Forms.TabPage();
            this.PrintForm_PrintPreviewControl_Main = new System.Windows.Forms.PrintPreviewControl();
            this.PrintForm_Panel_PreviewToolbar = new System.Windows.Forms.Panel();
            this.PrintForm_Button_ZoomOut = new System.Windows.Forms.Button();
            this.PrintForm_Button_ZoomIn = new System.Windows.Forms.Button();
            this.PrintForm_ComboBox_Zoom = new System.Windows.Forms.ComboBox();
            this.PrintForm_Button_FirstPage = new System.Windows.Forms.Button();
            this.PrintForm_Button_PreviousPage = new System.Windows.Forms.Button();
            this.PrintForm_Label_PageInfo = new System.Windows.Forms.Label();
            this.PrintForm_Button_NextPage = new System.Windows.Forms.Button();
            this.PrintForm_Button_LastPage = new System.Windows.Forms.Button();
            this.PrintForm_TabPage_Settings = new System.Windows.Forms.TabPage();
            this.PrintForm_GroupBox_Printer = new System.Windows.Forms.GroupBox();
            this.PrintForm_ComboBox_Printer = new System.Windows.Forms.ComboBox();
            this.PrintForm_GroupBox_ColorMode = new System.Windows.Forms.GroupBox();
            this.PrintForm_RadioButton_Color = new System.Windows.Forms.RadioButton();
            this.PrintForm_RadioButton_BlackWhite = new System.Windows.Forms.RadioButton();
            this.PrintForm_GroupBox_Orientation = new System.Windows.Forms.GroupBox();
            this.PrintForm_RadioButton_Portrait = new System.Windows.Forms.RadioButton();
            this.PrintForm_RadioButton_Landscape = new System.Windows.Forms.RadioButton();
            this.PrintForm_GroupBox_PageRange = new System.Windows.Forms.GroupBox();
            this.PrintForm_RadioButton_AllPages = new System.Windows.Forms.RadioButton();
            this.PrintForm_RadioButton_CurrentPage = new System.Windows.Forms.RadioButton();
            this.PrintForm_RadioButton_PageRange = new System.Windows.Forms.RadioButton();
            this.PrintForm_NumericUpDown_FromPage = new System.Windows.Forms.NumericUpDown();
            this.PrintForm_NumericUpDown_ToPage = new System.Windows.Forms.NumericUpDown();
            this.PrintForm_Label_PageFrom = new System.Windows.Forms.Label();
            this.PrintForm_Label_PageTo = new System.Windows.Forms.Label();
            this.PrintForm_GroupBox_Export = new System.Windows.Forms.GroupBox();
            this.PrintForm_CheckBox_ExportPdf = new System.Windows.Forms.CheckBox();
            this.PrintForm_CheckBox_ExportExcel = new System.Windows.Forms.CheckBox();
            this.PrintForm_CheckBox_ExportImage = new System.Windows.Forms.CheckBox();
            this.PrintForm_Button_ExportSettings = new System.Windows.Forms.Button();
            this.PrintForm_TabPage_Columns = new System.Windows.Forms.TabPage();
            this.PrintForm_CheckedListBox_Columns = new System.Windows.Forms.CheckedListBox();
            this.PrintForm_Panel_ColumnButtons = new System.Windows.Forms.Panel();
            this.PrintForm_Button_SelectAll = new System.Windows.Forms.Button();
            this.PrintForm_Button_DeselectAll = new System.Windows.Forms.Button();
            this.PrintForm_Button_MoveUp = new System.Windows.Forms.Button();
            this.PrintForm_Button_MoveDown = new System.Windows.Forms.Button();
            this.PrintForm_GroupBox_Filters = new System.Windows.Forms.GroupBox();
            this.PrintForm_ComboBox_FilterColumn = new System.Windows.Forms.ComboBox();
            this.PrintForm_ComboBox_FilterType = new System.Windows.Forms.ComboBox();
            this.PrintForm_TextBox_FilterValue = new System.Windows.Forms.TextBox();
            this.PrintForm_DateTimePicker_DateFrom = new System.Windows.Forms.DateTimePicker();
            this.PrintForm_DateTimePicker_DateTo = new System.Windows.Forms.DateTimePicker();
            this.PrintForm_Button_AddFilter = new System.Windows.Forms.Button();
            this.PrintForm_ListBox_ActiveFilters = new System.Windows.Forms.ListBox();
            this.PrintForm_Button_RemoveFilter = new System.Windows.Forms.Button();
            this.PrintForm_Button_ClearFilters = new System.Windows.Forms.Button();
            this.PrintForm_Panel_Buttons = new System.Windows.Forms.Panel();
            this.PrintForm_Button_Print = new System.Windows.Forms.Button();
            this.PrintForm_Button_Cancel = new System.Windows.Forms.Button();
            this.PrintForm_GroupBox_Presets = new System.Windows.Forms.GroupBox();
            this.PrintForm_ComboBox_Presets = new System.Windows.Forms.ComboBox();
            this.PrintForm_Button_SavePreset = new System.Windows.Forms.Button();
            this.PrintForm_Button_DeletePreset = new System.Windows.Forms.Button();
            this.PrintForm_StatusStrip_Main = new System.Windows.Forms.StatusStrip();
            this.PrintForm_ToolStripStatusLabel_Status = new System.Windows.Forms.ToolStripStatusLabel();
            
            this.PrintForm_TabControl_Main.SuspendLayout();
            this.PrintForm_TabPage_Preview.SuspendLayout();
            this.PrintForm_Panel_PreviewToolbar.SuspendLayout();
            this.PrintForm_TabPage_Settings.SuspendLayout();
            this.PrintForm_GroupBox_Printer.SuspendLayout();
            this.PrintForm_GroupBox_ColorMode.SuspendLayout();
            this.PrintForm_GroupBox_Orientation.SuspendLayout();
            this.PrintForm_GroupBox_PageRange.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PrintForm_NumericUpDown_FromPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PrintForm_NumericUpDown_ToPage)).BeginInit();
            this.PrintForm_GroupBox_Export.SuspendLayout();
            this.PrintForm_TabPage_Columns.SuspendLayout();
            this.PrintForm_Panel_ColumnButtons.SuspendLayout();
            this.PrintForm_GroupBox_Filters.SuspendLayout();
            this.PrintForm_Panel_Buttons.SuspendLayout();
            this.PrintForm_GroupBox_Presets.SuspendLayout();
            this.PrintForm_StatusStrip_Main.SuspendLayout();
            this.SuspendLayout();
            
            // 
            // PrintForm_TabControl_Main
            // 
            this.PrintForm_TabControl_Main.Controls.Add(this.PrintForm_TabPage_Preview);
            this.PrintForm_TabControl_Main.Controls.Add(this.PrintForm_TabPage_Settings);
            this.PrintForm_TabControl_Main.Controls.Add(this.PrintForm_TabPage_Columns);
            this.PrintForm_TabControl_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PrintForm_TabControl_Main.Location = new System.Drawing.Point(0, 0);
            this.PrintForm_TabControl_Main.Name = "PrintForm_TabControl_Main";
            this.PrintForm_TabControl_Main.SelectedIndex = 0;
            this.PrintForm_TabControl_Main.Size = new System.Drawing.Size(1184, 681);
            this.PrintForm_TabControl_Main.TabIndex = 0;
            
            // 
            // PrintForm_TabPage_Preview
            // 
            this.PrintForm_TabPage_Preview.Controls.Add(this.PrintForm_PrintPreviewControl_Main);
            this.PrintForm_TabPage_Preview.Controls.Add(this.PrintForm_Panel_PreviewToolbar);
            this.PrintForm_TabPage_Preview.Location = new System.Drawing.Point(4, 24);
            this.PrintForm_TabPage_Preview.Name = "PrintForm_TabPage_Preview";
            this.PrintForm_TabPage_Preview.Padding = new System.Windows.Forms.Padding(3);
            this.PrintForm_TabPage_Preview.Size = new System.Drawing.Size(1176, 653);
            this.PrintForm_TabPage_Preview.TabIndex = 0;
            this.PrintForm_TabPage_Preview.Text = "Preview";
            this.PrintForm_TabPage_Preview.UseVisualStyleBackColor = true;
            
            // 
            // PrintForm_PrintPreviewControl_Main
            // 
            this.PrintForm_PrintPreviewControl_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PrintForm_PrintPreviewControl_Main.Location = new System.Drawing.Point(3, 43);
            this.PrintForm_PrintPreviewControl_Main.Name = "PrintForm_PrintPreviewControl_Main";
            this.PrintForm_PrintPreviewControl_Main.Size = new System.Drawing.Size(1170, 607);
            this.PrintForm_PrintPreviewControl_Main.TabIndex = 0;
            this.PrintForm_PrintPreviewControl_Main.Zoom = 1D;
            
            // 
            // PrintForm_Panel_PreviewToolbar
            // 
            this.PrintForm_Panel_PreviewToolbar.Controls.Add(this.PrintForm_Button_ZoomOut);
            this.PrintForm_Panel_PreviewToolbar.Controls.Add(this.PrintForm_Button_ZoomIn);
            this.PrintForm_Panel_PreviewToolbar.Controls.Add(this.PrintForm_ComboBox_Zoom);
            this.PrintForm_Panel_PreviewToolbar.Controls.Add(this.PrintForm_Button_FirstPage);
            this.PrintForm_Panel_PreviewToolbar.Controls.Add(this.PrintForm_Button_PreviousPage);
            this.PrintForm_Panel_PreviewToolbar.Controls.Add(this.PrintForm_Label_PageInfo);
            this.PrintForm_Panel_PreviewToolbar.Controls.Add(this.PrintForm_Button_NextPage);
            this.PrintForm_Panel_PreviewToolbar.Controls.Add(this.PrintForm_Button_LastPage);
            this.PrintForm_Panel_PreviewToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.PrintForm_Panel_PreviewToolbar.Location = new System.Drawing.Point(3, 3);
            this.PrintForm_Panel_PreviewToolbar.Name = "PrintForm_Panel_PreviewToolbar";
            this.PrintForm_Panel_PreviewToolbar.Size = new System.Drawing.Size(1170, 40);
            this.PrintForm_Panel_PreviewToolbar.TabIndex = 1;
            
            // Zoom controls
            this.PrintForm_Button_ZoomOut.Location = new System.Drawing.Point(10, 8);
            this.PrintForm_Button_ZoomOut.Name = "PrintForm_Button_ZoomOut";
            this.PrintForm_Button_ZoomOut.Size = new System.Drawing.Size(30, 25);
            this.PrintForm_Button_ZoomOut.TabIndex = 0;
            this.PrintForm_Button_ZoomOut.Text = "-";
            this.PrintForm_Button_ZoomOut.UseVisualStyleBackColor = true;
            
            this.PrintForm_ComboBox_Zoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PrintForm_ComboBox_Zoom.Items.AddRange(new object[] { "25%", "50%", "75%", "100%", "150%", "200%" });
            this.PrintForm_ComboBox_Zoom.Location = new System.Drawing.Point(45, 9);
            this.PrintForm_ComboBox_Zoom.Name = "PrintForm_ComboBox_Zoom";
            this.PrintForm_ComboBox_Zoom.Size = new System.Drawing.Size(80, 23);
            this.PrintForm_ComboBox_Zoom.TabIndex = 1;
            this.PrintForm_ComboBox_Zoom.SelectedIndex = 3; // 100%
            
            this.PrintForm_Button_ZoomIn.Location = new System.Drawing.Point(130, 8);
            this.PrintForm_Button_ZoomIn.Name = "PrintForm_Button_ZoomIn";
            this.PrintForm_Button_ZoomIn.Size = new System.Drawing.Size(30, 25);
            this.PrintForm_Button_ZoomIn.TabIndex = 2;
            this.PrintForm_Button_ZoomIn.Text = "+";
            this.PrintForm_Button_ZoomIn.UseVisualStyleBackColor = true;
            
            // Page navigation
            this.PrintForm_Button_FirstPage.Location = new System.Drawing.Point(180, 8);
            this.PrintForm_Button_FirstPage.Name = "PrintForm_Button_FirstPage";
            this.PrintForm_Button_FirstPage.Size = new System.Drawing.Size(60, 25);
            this.PrintForm_Button_FirstPage.TabIndex = 3;
            this.PrintForm_Button_FirstPage.Text = "⏮ First";
            this.PrintForm_Button_FirstPage.UseVisualStyleBackColor = true;
            
            this.PrintForm_Button_PreviousPage.Location = new System.Drawing.Point(245, 8);
            this.PrintForm_Button_PreviousPage.Name = "PrintForm_Button_PreviousPage";
            this.PrintForm_Button_PreviousPage.Size = new System.Drawing.Size(60, 25);
            this.PrintForm_Button_PreviousPage.TabIndex = 4;
            this.PrintForm_Button_PreviousPage.Text = "◀ Prev";
            this.PrintForm_Button_PreviousPage.UseVisualStyleBackColor = true;
            
            this.PrintForm_Label_PageInfo.AutoSize = true;
            this.PrintForm_Label_PageInfo.Location = new System.Drawing.Point(310, 12);
            this.PrintForm_Label_PageInfo.Name = "PrintForm_Label_PageInfo";
            this.PrintForm_Label_PageInfo.Size = new System.Drawing.Size(80, 15);
            this.PrintForm_Label_PageInfo.TabIndex = 5;
            this.PrintForm_Label_PageInfo.Text = "Page 1 of 1";
            this.PrintForm_Label_PageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            
            this.PrintForm_Button_NextPage.Location = new System.Drawing.Point(395, 8);
            this.PrintForm_Button_NextPage.Name = "PrintForm_Button_NextPage";
            this.PrintForm_Button_NextPage.Size = new System.Drawing.Size(60, 25);
            this.PrintForm_Button_NextPage.TabIndex = 6;
            this.PrintForm_Button_NextPage.Text = "Next ▶";
            this.PrintForm_Button_NextPage.UseVisualStyleBackColor = true;
            
            this.PrintForm_Button_LastPage.Location = new System.Drawing.Point(460, 8);
            this.PrintForm_Button_LastPage.Name = "PrintForm_Button_LastPage";
            this.PrintForm_Button_LastPage.Size = new System.Drawing.Size(60, 25);
            this.PrintForm_Button_LastPage.TabIndex = 7;
            this.PrintForm_Button_LastPage.Text = "Last ⏭";
            this.PrintForm_Button_LastPage.UseVisualStyleBackColor = true;
            
            // Settings Tab - will continue in next part due to length
            this.PrintForm_TabPage_Settings.Location = new System.Drawing.Point(4, 24);
            this.PrintForm_TabPage_Settings.Name = "PrintForm_TabPage_Settings";
            this.PrintForm_TabPage_Settings.Padding = new System.Windows.Forms.Padding(3);
            this.PrintForm_TabPage_Settings.Size = new System.Drawing.Size(1176, 653);
            this.PrintForm_TabPage_Settings.TabIndex = 1;
            this.PrintForm_TabPage_Settings.Text = "Settings";
            this.PrintForm_TabPage_Settings.UseVisualStyleBackColor = true;
            
            // GroupBox - Printer
            this.PrintForm_GroupBox_Printer.Controls.Add(this.PrintForm_ComboBox_Printer);
            this.PrintForm_GroupBox_Printer.Controls.Add(this.PrintForm_GroupBox_ColorMode);
            this.PrintForm_GroupBox_Printer.Controls.Add(this.PrintForm_GroupBox_Orientation);
            this.PrintForm_GroupBox_Printer.Location = new System.Drawing.Point(15, 15);
            this.PrintForm_GroupBox_Printer.Name = "PrintForm_GroupBox_Printer";
            this.PrintForm_GroupBox_Printer.Size = new System.Drawing.Size(350, 180);
            this.PrintForm_GroupBox_Printer.TabIndex = 0;
            this.PrintForm_GroupBox_Printer.TabStop = false;
            this.PrintForm_GroupBox_Printer.Text = "Printer";
            
            this.PrintForm_ComboBox_Printer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PrintForm_ComboBox_Printer.FormattingEnabled = true;
            this.PrintForm_ComboBox_Printer.Location = new System.Drawing.Point(15, 30);
            this.PrintForm_ComboBox_Printer.Name = "PrintForm_ComboBox_Printer";
            this.PrintForm_ComboBox_Printer.Size = new System.Drawing.Size(320, 23);
            this.PrintForm_ComboBox_Printer.TabIndex = 0;
            
            // GroupBox - Color Mode
            this.PrintForm_GroupBox_ColorMode.Controls.Add(this.PrintForm_RadioButton_Color);
            this.PrintForm_GroupBox_ColorMode.Controls.Add(this.PrintForm_RadioButton_BlackWhite);
            this.PrintForm_GroupBox_ColorMode.Location = new System.Drawing.Point(15, 70);
            this.PrintForm_GroupBox_ColorMode.Name = "PrintForm_GroupBox_ColorMode";
            this.PrintForm_GroupBox_ColorMode.Size = new System.Drawing.Size(150, 95);
            this.PrintForm_GroupBox_ColorMode.TabIndex = 1;
            this.PrintForm_GroupBox_ColorMode.TabStop = false;
            this.PrintForm_GroupBox_ColorMode.Text = "Color Mode";
            
            this.PrintForm_RadioButton_Color.AutoSize = true;
            this.PrintForm_RadioButton_Color.Checked = true;
            this.PrintForm_RadioButton_Color.Location = new System.Drawing.Point(15, 30);
            this.PrintForm_RadioButton_Color.Name = "PrintForm_RadioButton_Color";
            this.PrintForm_RadioButton_Color.Size = new System.Drawing.Size(56, 19);
            this.PrintForm_RadioButton_Color.TabIndex = 0;
            this.PrintForm_RadioButton_Color.TabStop = true;
            this.PrintForm_RadioButton_Color.Text = "Color";
            this.PrintForm_RadioButton_Color.UseVisualStyleBackColor = true;
            
            this.PrintForm_RadioButton_BlackWhite.AutoSize = true;
            this.PrintForm_RadioButton_BlackWhite.Location = new System.Drawing.Point(15, 60);
            this.PrintForm_RadioButton_BlackWhite.Name = "PrintForm_RadioButton_BlackWhite";
            this.PrintForm_RadioButton_BlackWhite.Size = new System.Drawing.Size(107, 19);
            this.PrintForm_RadioButton_BlackWhite.TabIndex = 1;
            this.PrintForm_RadioButton_BlackWhite.Text = "Black && White";
            this.PrintForm_RadioButton_BlackWhite.UseVisualStyleBackColor = true;
            
            // GroupBox - Orientation
            this.PrintForm_GroupBox_Orientation.Controls.Add(this.PrintForm_RadioButton_Portrait);
            this.PrintForm_GroupBox_Orientation.Controls.Add(this.PrintForm_RadioButton_Landscape);
            this.PrintForm_GroupBox_Orientation.Location = new System.Drawing.Point(185, 70);
            this.PrintForm_GroupBox_Orientation.Name = "PrintForm_GroupBox_Orientation";
            this.PrintForm_GroupBox_Orientation.Size = new System.Drawing.Size(150, 95);
            this.PrintForm_GroupBox_Orientation.TabIndex = 2;
            this.PrintForm_GroupBox_Orientation.TabStop = false;
            this.PrintForm_GroupBox_Orientation.Text = "Orientation";
            
            this.PrintForm_RadioButton_Portrait.AutoSize = true;
            this.PrintForm_RadioButton_Portrait.Checked = true;
            this.PrintForm_RadioButton_Portrait.Location = new System.Drawing.Point(15, 30);
            this.PrintForm_RadioButton_Portrait.Name = "PrintForm_RadioButton_Portrait";
            this.PrintForm_RadioButton_Portrait.Size = new System.Drawing.Size(66, 19);
            this.PrintForm_RadioButton_Portrait.TabIndex = 0;
            this.PrintForm_RadioButton_Portrait.TabStop = true;
            this.PrintForm_RadioButton_Portrait.Text = "Portrait";
            this.PrintForm_RadioButton_Portrait.UseVisualStyleBackColor = true;
            
            this.PrintForm_RadioButton_Landscape.AutoSize = true;
            this.PrintForm_RadioButton_Landscape.Location = new System.Drawing.Point(15, 60);
            this.PrintForm_RadioButton_Landscape.Name = "PrintForm_RadioButton_Landscape";
            this.PrintForm_RadioButton_Landscape.Size = new System.Drawing.Size(82, 19);
            this.PrintForm_RadioButton_Landscape.TabIndex = 1;
            this.PrintForm_RadioButton_Landscape.Text = "Landscape";
            this.PrintForm_RadioButton_Landscape.UseVisualStyleBackColor = true;
            
            // GroupBox - Page Range
            this.PrintForm_GroupBox_PageRange.Controls.Add(this.PrintForm_RadioButton_AllPages);
            this.PrintForm_GroupBox_PageRange.Controls.Add(this.PrintForm_RadioButton_CurrentPage);
            this.PrintForm_GroupBox_PageRange.Controls.Add(this.PrintForm_RadioButton_PageRange);
            this.PrintForm_GroupBox_PageRange.Controls.Add(this.PrintForm_NumericUpDown_FromPage);
            this.PrintForm_GroupBox_PageRange.Controls.Add(this.PrintForm_NumericUpDown_ToPage);
            this.PrintForm_GroupBox_PageRange.Controls.Add(this.PrintForm_Label_PageFrom);
            this.PrintForm_GroupBox_PageRange.Controls.Add(this.PrintForm_Label_PageTo);
            this.PrintForm_GroupBox_PageRange.Location = new System.Drawing.Point(385, 15);
            this.PrintForm_GroupBox_PageRange.Name = "PrintForm_GroupBox_PageRange";
            this.PrintForm_GroupBox_PageRange.Size = new System.Drawing.Size(300, 180);
            this.PrintForm_GroupBox_PageRange.TabIndex = 1;
            this.PrintForm_GroupBox_PageRange.TabStop = false;
            this.PrintForm_GroupBox_PageRange.Text = "Page Range";
            
            this.PrintForm_RadioButton_AllPages.AutoSize = true;
            this.PrintForm_RadioButton_AllPages.Checked = true;
            this.PrintForm_RadioButton_AllPages.Location = new System.Drawing.Point(15, 30);
            this.PrintForm_RadioButton_AllPages.Name = "PrintForm_RadioButton_AllPages";
            this.PrintForm_RadioButton_AllPages.Size = new System.Drawing.Size(75, 19);
            this.PrintForm_RadioButton_AllPages.TabIndex = 0;
            this.PrintForm_RadioButton_AllPages.TabStop = true;
            this.PrintForm_RadioButton_AllPages.Text = "All Pages";
            this.PrintForm_RadioButton_AllPages.UseVisualStyleBackColor = true;
            
            this.PrintForm_RadioButton_CurrentPage.AutoSize = true;
            this.PrintForm_RadioButton_CurrentPage.Location = new System.Drawing.Point(15, 60);
            this.PrintForm_RadioButton_CurrentPage.Name = "PrintForm_RadioButton_CurrentPage";
            this.PrintForm_RadioButton_CurrentPage.Size = new System.Drawing.Size(98, 19);
            this.PrintForm_RadioButton_CurrentPage.TabIndex = 1;
            this.PrintForm_RadioButton_CurrentPage.Text = "Current Page";
            this.PrintForm_RadioButton_CurrentPage.UseVisualStyleBackColor = true;
            
            this.PrintForm_RadioButton_PageRange.AutoSize = true;
            this.PrintForm_RadioButton_PageRange.Location = new System.Drawing.Point(15, 90);
            this.PrintForm_RadioButton_PageRange.Name = "PrintForm_RadioButton_PageRange";
            this.PrintForm_RadioButton_PageRange.Size = new System.Drawing.Size(57, 19);
            this.PrintForm_RadioButton_PageRange.TabIndex = 2;
            this.PrintForm_RadioButton_PageRange.Text = "Pages";
            this.PrintForm_RadioButton_PageRange.UseVisualStyleBackColor = true;
            
            this.PrintForm_Label_PageFrom.AutoSize = true;
            this.PrintForm_Label_PageFrom.Location = new System.Drawing.Point(35, 120);
            this.PrintForm_Label_PageFrom.Name = "PrintForm_Label_PageFrom";
            this.PrintForm_Label_PageFrom.Size = new System.Drawing.Size(38, 15);
            this.PrintForm_Label_PageFrom.TabIndex = 3;
            this.PrintForm_Label_PageFrom.Text = "From:";
            
            this.PrintForm_NumericUpDown_FromPage.Location = new System.Drawing.Point(80, 118);
            this.PrintForm_NumericUpDown_FromPage.Minimum = 1M;
            this.PrintForm_NumericUpDown_FromPage.Name = "PrintForm_NumericUpDown_FromPage";
            this.PrintForm_NumericUpDown_FromPage.Size = new System.Drawing.Size(70, 23);
            this.PrintForm_NumericUpDown_FromPage.TabIndex = 4;
            this.PrintForm_NumericUpDown_FromPage.Value = 1M;
            
            this.PrintForm_Label_PageTo.AutoSize = true;
            this.PrintForm_Label_PageTo.Location = new System.Drawing.Point(160, 120);
            this.PrintForm_Label_PageTo.Name = "PrintForm_Label_PageTo";
            this.PrintForm_Label_PageTo.Size = new System.Drawing.Size(22, 15);
            this.PrintForm_Label_PageTo.TabIndex = 5;
            this.PrintForm_Label_PageTo.Text = "To:";
            
            this.PrintForm_NumericUpDown_ToPage.Location = new System.Drawing.Point(188, 118);
            this.PrintForm_NumericUpDown_ToPage.Minimum = 1M;
            this.PrintForm_NumericUpDown_ToPage.Name = "PrintForm_NumericUpDown_ToPage";
            this.PrintForm_NumericUpDown_ToPage.Size = new System.Drawing.Size(70, 23);
            this.PrintForm_NumericUpDown_ToPage.TabIndex = 6;
            this.PrintForm_NumericUpDown_ToPage.Value = 1M;
            
            // GroupBox - Export
            this.PrintForm_GroupBox_Export.Controls.Add(this.PrintForm_CheckBox_ExportPdf);
            this.PrintForm_GroupBox_Export.Controls.Add(this.PrintForm_CheckBox_ExportExcel);
            this.PrintForm_GroupBox_Export.Controls.Add(this.PrintForm_CheckBox_ExportImage);
            this.PrintForm_GroupBox_Export.Controls.Add(this.PrintForm_Button_ExportSettings);
            this.PrintForm_GroupBox_Export.Location = new System.Drawing.Point(700, 15);
            this.PrintForm_GroupBox_Export.Name = "PrintForm_GroupBox_Export";
            this.PrintForm_GroupBox_Export.Size = new System.Drawing.Size(250, 180);
            this.PrintForm_GroupBox_Export.TabIndex = 2;
            this.PrintForm_GroupBox_Export.TabStop = false;
            this.PrintForm_GroupBox_Export.Text = "Export Options";
            
            this.PrintForm_CheckBox_ExportPdf.AutoSize = true;
            this.PrintForm_CheckBox_ExportPdf.Location = new System.Drawing.Point(15, 30);
            this.PrintForm_CheckBox_ExportPdf.Name = "PrintForm_CheckBox_ExportPdf";
            this.PrintForm_CheckBox_ExportPdf.Size = new System.Drawing.Size(96, 19);
            this.PrintForm_CheckBox_ExportPdf.TabIndex = 0;
            this.PrintForm_CheckBox_ExportPdf.Text = "Export to PDF";
            this.PrintForm_CheckBox_ExportPdf.UseVisualStyleBackColor = true;
            
            this.PrintForm_CheckBox_ExportExcel.AutoSize = true;
            this.PrintForm_CheckBox_ExportExcel.Location = new System.Drawing.Point(15, 60);
            this.PrintForm_CheckBox_ExportExcel.Name = "PrintForm_CheckBox_ExportExcel";
            this.PrintForm_CheckBox_ExportExcel.Size = new System.Drawing.Size(105, 19);
            this.PrintForm_CheckBox_ExportExcel.TabIndex = 1;
            this.PrintForm_CheckBox_ExportExcel.Text = "Export to Excel";
            this.PrintForm_CheckBox_ExportExcel.UseVisualStyleBackColor = true;
            
            this.PrintForm_CheckBox_ExportImage.AutoSize = true;
            this.PrintForm_CheckBox_ExportImage.Location = new System.Drawing.Point(15, 90);
            this.PrintForm_CheckBox_ExportImage.Name = "PrintForm_CheckBox_ExportImage";
            this.PrintForm_CheckBox_ExportImage.Size = new System.Drawing.Size(110, 19);
            this.PrintForm_CheckBox_ExportImage.TabIndex = 2;
            this.PrintForm_CheckBox_ExportImage.Text = "Export to Image";
            this.PrintForm_CheckBox_ExportImage.UseVisualStyleBackColor = true;
            
            this.PrintForm_Button_ExportSettings.Location = new System.Drawing.Point(15, 130);
            this.PrintForm_Button_ExportSettings.Name = "PrintForm_Button_ExportSettings";
            this.PrintForm_Button_ExportSettings.Size = new System.Drawing.Size(120, 30);
            this.PrintForm_Button_ExportSettings.TabIndex = 3;
            this.PrintForm_Button_ExportSettings.Text = "Export Now...";
            this.PrintForm_Button_ExportSettings.UseVisualStyleBackColor = true;
            
            // Columns Tab
            this.PrintForm_TabPage_Columns.Controls.Add(this.PrintForm_CheckedListBox_Columns);
            this.PrintForm_TabPage_Columns.Controls.Add(this.PrintForm_Panel_ColumnButtons);
            this.PrintForm_TabPage_Columns.Controls.Add(this.PrintForm_GroupBox_Filters);
            this.PrintForm_TabPage_Columns.Location = new System.Drawing.Point(4, 24);
            this.PrintForm_TabPage_Columns.Name = "PrintForm_TabPage_Columns";
            this.PrintForm_TabPage_Columns.Size = new System.Drawing.Size(1176, 653);
            this.PrintForm_TabPage_Columns.TabIndex = 2;
            this.PrintForm_TabPage_Columns.Text = "Columns & Filters";
            this.PrintForm_TabPage_Columns.UseVisualStyleBackColor = true;
            
            // CheckedListBox for columns
            this.PrintForm_CheckedListBox_Columns.FormattingEnabled = true;
            this.PrintForm_CheckedListBox_Columns.Location = new System.Drawing.Point(15, 15);
            this.PrintForm_CheckedListBox_Columns.Name = "PrintForm_CheckedListBox_Columns";
            this.PrintForm_CheckedListBox_Columns.Size = new System.Drawing.Size(300, 400);
            this.PrintForm_CheckedListBox_Columns.TabIndex = 0;
            
            // Column buttons panel
            this.PrintForm_Panel_ColumnButtons.Controls.Add(this.PrintForm_Button_SelectAll);
            this.PrintForm_Panel_ColumnButtons.Controls.Add(this.PrintForm_Button_DeselectAll);
            this.PrintForm_Panel_ColumnButtons.Controls.Add(this.PrintForm_Button_MoveUp);
            this.PrintForm_Panel_ColumnButtons.Controls.Add(this.PrintForm_Button_MoveDown);
            this.PrintForm_Panel_ColumnButtons.Location = new System.Drawing.Point(330, 15);
            this.PrintForm_Panel_ColumnButtons.Name = "PrintForm_Panel_ColumnButtons";
            this.PrintForm_Panel_ColumnButtons.Size = new System.Drawing.Size(120, 200);
            this.PrintForm_Panel_ColumnButtons.TabIndex = 1;
            
            this.PrintForm_Button_SelectAll.Location = new System.Drawing.Point(5, 5);
            this.PrintForm_Button_SelectAll.Name = "PrintForm_Button_SelectAll";
            this.PrintForm_Button_SelectAll.Size = new System.Drawing.Size(110, 30);
            this.PrintForm_Button_SelectAll.TabIndex = 0;
            this.PrintForm_Button_SelectAll.Text = "Select All";
            this.PrintForm_Button_SelectAll.UseVisualStyleBackColor = true;
            
            this.PrintForm_Button_DeselectAll.Location = new System.Drawing.Point(5, 40);
            this.PrintForm_Button_DeselectAll.Name = "PrintForm_Button_DeselectAll";
            this.PrintForm_Button_DeselectAll.Size = new System.Drawing.Size(110, 30);
            this.PrintForm_Button_DeselectAll.TabIndex = 1;
            this.PrintForm_Button_DeselectAll.Text = "Deselect All";
            this.PrintForm_Button_DeselectAll.UseVisualStyleBackColor = true;
            
            this.PrintForm_Button_MoveUp.Location = new System.Drawing.Point(5, 90);
            this.PrintForm_Button_MoveUp.Name = "PrintForm_Button_MoveUp";
            this.PrintForm_Button_MoveUp.Size = new System.Drawing.Size(110, 30);
            this.PrintForm_Button_MoveUp.TabIndex = 2;
            this.PrintForm_Button_MoveUp.Text = "Move Up ↑";
            this.PrintForm_Button_MoveUp.UseVisualStyleBackColor = true;
            
            this.PrintForm_Button_MoveDown.Location = new System.Drawing.Point(5, 125);
            this.PrintForm_Button_MoveDown.Name = "PrintForm_Button_MoveDown";
            this.PrintForm_Button_MoveDown.Size = new System.Drawing.Size(110, 30);
            this.PrintForm_Button_MoveDown.TabIndex = 3;
            this.PrintForm_Button_MoveDown.Text = "Move Down ↓";
            this.PrintForm_Button_MoveDown.UseVisualStyleBackColor = true;
            
            // Filters GroupBox
            this.PrintForm_GroupBox_Filters.Controls.Add(this.PrintForm_ComboBox_FilterColumn);
            this.PrintForm_GroupBox_Filters.Controls.Add(this.PrintForm_ComboBox_FilterType);
            this.PrintForm_GroupBox_Filters.Controls.Add(this.PrintForm_TextBox_FilterValue);
            this.PrintForm_GroupBox_Filters.Controls.Add(this.PrintForm_DateTimePicker_DateFrom);
            this.PrintForm_GroupBox_Filters.Controls.Add(this.PrintForm_DateTimePicker_DateTo);
            this.PrintForm_GroupBox_Filters.Controls.Add(this.PrintForm_Button_AddFilter);
            this.PrintForm_GroupBox_Filters.Controls.Add(this.PrintForm_ListBox_ActiveFilters);
            this.PrintForm_GroupBox_Filters.Controls.Add(this.PrintForm_Button_RemoveFilter);
            this.PrintForm_GroupBox_Filters.Controls.Add(this.PrintForm_Button_ClearFilters);
            this.PrintForm_GroupBox_Filters.Location = new System.Drawing.Point(470, 15);
            this.PrintForm_GroupBox_Filters.Name = "PrintForm_GroupBox_Filters";
            this.PrintForm_GroupBox_Filters.Size = new System.Drawing.Size(680, 400);
            this.PrintForm_GroupBox_Filters.TabIndex = 2;
            this.PrintForm_GroupBox_Filters.TabStop = false;
            this.PrintForm_GroupBox_Filters.Text = "Filters";
            
            this.PrintForm_ComboBox_FilterColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PrintForm_ComboBox_FilterColumn.FormattingEnabled = true;
            this.PrintForm_ComboBox_FilterColumn.Location = new System.Drawing.Point(15, 30);
            this.PrintForm_ComboBox_FilterColumn.Name = "PrintForm_ComboBox_FilterColumn";
            this.PrintForm_ComboBox_FilterColumn.Size = new System.Drawing.Size(200, 23);
            this.PrintForm_ComboBox_FilterColumn.TabIndex = 0;
            
            this.PrintForm_ComboBox_FilterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PrintForm_ComboBox_FilterType.Items.AddRange(new object[] { "Contains", "Equals", "Date Range", "Show Selected Rows" });
            this.PrintForm_ComboBox_FilterType.Location = new System.Drawing.Point(230, 30);
            this.PrintForm_ComboBox_FilterType.Name = "PrintForm_ComboBox_FilterType";
            this.PrintForm_ComboBox_FilterType.Size = new System.Drawing.Size(150, 23);
            this.PrintForm_ComboBox_FilterType.TabIndex = 1;
            this.PrintForm_ComboBox_FilterType.SelectedIndex = 0;
            
            this.PrintForm_TextBox_FilterValue.Location = new System.Drawing.Point(15, 65);
            this.PrintForm_TextBox_FilterValue.Name = "PrintForm_TextBox_FilterValue";
            this.PrintForm_TextBox_FilterValue.Size = new System.Drawing.Size(365, 23);
            this.PrintForm_TextBox_FilterValue.TabIndex = 2;
            
            this.PrintForm_DateTimePicker_DateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.PrintForm_DateTimePicker_DateFrom.Location = new System.Drawing.Point(15, 100);
            this.PrintForm_DateTimePicker_DateFrom.Name = "PrintForm_DateTimePicker_DateFrom";
            this.PrintForm_DateTimePicker_DateFrom.Size = new System.Drawing.Size(150, 23);
            this.PrintForm_DateTimePicker_DateFrom.TabIndex = 3;
            this.PrintForm_DateTimePicker_DateFrom.Visible = false;
            
            this.PrintForm_DateTimePicker_DateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.PrintForm_DateTimePicker_DateTo.Location = new System.Drawing.Point(230, 100);
            this.PrintForm_DateTimePicker_DateTo.Name = "PrintForm_DateTimePicker_DateTo";
            this.PrintForm_DateTimePicker_DateTo.Size = new System.Drawing.Size(150, 23);
            this.PrintForm_DateTimePicker_DateTo.TabIndex = 4;
            this.PrintForm_DateTimePicker_DateTo.Visible = false;
            
            this.PrintForm_Button_AddFilter.Location = new System.Drawing.Point(400, 30);
            this.PrintForm_Button_AddFilter.Name = "PrintForm_Button_AddFilter";
            this.PrintForm_Button_AddFilter.Size = new System.Drawing.Size(100, 30);
            this.PrintForm_Button_AddFilter.TabIndex = 5;
            this.PrintForm_Button_AddFilter.Text = "Add Filter";
            this.PrintForm_Button_AddFilter.UseVisualStyleBackColor = true;
            
            this.PrintForm_ListBox_ActiveFilters.FormattingEnabled = true;
            this.PrintForm_ListBox_ActiveFilters.ItemHeight = 15;
            this.PrintForm_ListBox_ActiveFilters.Location = new System.Drawing.Point(15, 140);
            this.PrintForm_ListBox_ActiveFilters.Name = "PrintForm_ListBox_ActiveFilters";
            this.PrintForm_ListBox_ActiveFilters.Size = new System.Drawing.Size(650, 199);
            this.PrintForm_ListBox_ActiveFilters.TabIndex = 6;
            
            this.PrintForm_Button_RemoveFilter.Location = new System.Drawing.Point(15, 350);
            this.PrintForm_Button_RemoveFilter.Name = "PrintForm_Button_RemoveFilter";
            this.PrintForm_Button_RemoveFilter.Size = new System.Drawing.Size(120, 30);
            this.PrintForm_Button_RemoveFilter.TabIndex = 7;
            this.PrintForm_Button_RemoveFilter.Text = "Remove Selected";
            this.PrintForm_Button_RemoveFilter.UseVisualStyleBackColor = true;
            
            this.PrintForm_Button_ClearFilters.Location = new System.Drawing.Point(145, 350);
            this.PrintForm_Button_ClearFilters.Name = "PrintForm_Button_ClearFilters";
            this.PrintForm_Button_ClearFilters.Size = new System.Drawing.Size(100, 30);
            this.PrintForm_Button_ClearFilters.TabIndex = 8;
            this.PrintForm_Button_ClearFilters.Text = "Clear All";
            this.PrintForm_Button_ClearFilters.UseVisualStyleBackColor = true;
            
            // Presets GroupBox (on Settings tab)
            this.PrintForm_GroupBox_Presets.Controls.Add(this.PrintForm_ComboBox_Presets);
            this.PrintForm_GroupBox_Presets.Controls.Add(this.PrintForm_Button_SavePreset);
            this.PrintForm_GroupBox_Presets.Controls.Add(this.PrintForm_Button_DeletePreset);
            this.PrintForm_GroupBox_Presets.Location = new System.Drawing.Point(15, 210);
            this.PrintForm_GroupBox_Presets.Name = "PrintForm_GroupBox_Presets";
            this.PrintForm_GroupBox_Presets.Size = new System.Drawing.Size(935, 80);
            this.PrintForm_GroupBox_Presets.TabIndex = 3;
            this.PrintForm_GroupBox_Presets.TabStop = false;
            this.PrintForm_GroupBox_Presets.Text = "Presets";
            
            // Add all GroupBoxes to Settings tab
            this.PrintForm_TabPage_Settings.Controls.Add(this.PrintForm_GroupBox_Printer);
            this.PrintForm_TabPage_Settings.Controls.Add(this.PrintForm_GroupBox_PageRange);
            this.PrintForm_TabPage_Settings.Controls.Add(this.PrintForm_GroupBox_Export);
            this.PrintForm_TabPage_Settings.Controls.Add(this.PrintForm_GroupBox_Presets);
            
            this.PrintForm_ComboBox_Presets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PrintForm_ComboBox_Presets.FormattingEnabled = true;
            this.PrintForm_ComboBox_Presets.Location = new System.Drawing.Point(15, 30);
            this.PrintForm_ComboBox_Presets.Name = "PrintForm_ComboBox_Presets";
            this.PrintForm_ComboBox_Presets.Size = new System.Drawing.Size(500, 23);
            this.PrintForm_ComboBox_Presets.TabIndex = 0;
            
            this.PrintForm_Button_SavePreset.Location = new System.Drawing.Point(530, 28);
            this.PrintForm_Button_SavePreset.Name = "PrintForm_Button_SavePreset";
            this.PrintForm_Button_SavePreset.Size = new System.Drawing.Size(120, 27);
            this.PrintForm_Button_SavePreset.TabIndex = 1;
            this.PrintForm_Button_SavePreset.Text = "Save Preset...";
            this.PrintForm_Button_SavePreset.UseVisualStyleBackColor = true;
            
            this.PrintForm_Button_DeletePreset.Location = new System.Drawing.Point(660, 28);
            this.PrintForm_Button_DeletePreset.Name = "PrintForm_Button_DeletePreset";
            this.PrintForm_Button_DeletePreset.Size = new System.Drawing.Size(120, 27);
            this.PrintForm_Button_DeletePreset.TabIndex = 2;
            this.PrintForm_Button_DeletePreset.Text = "Delete Preset";
            this.PrintForm_Button_DeletePreset.UseVisualStyleBackColor = true;
            
            // Bottom button panel
            this.PrintForm_Panel_Buttons.Controls.Add(this.PrintForm_Button_Print);
            this.PrintForm_Panel_Buttons.Controls.Add(this.PrintForm_Button_Cancel);
            this.PrintForm_Panel_Buttons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PrintForm_Panel_Buttons.Location = new System.Drawing.Point(0, 706);
            this.PrintForm_Panel_Buttons.Name = "PrintForm_Panel_Buttons";
            this.PrintForm_Panel_Buttons.Size = new System.Drawing.Size(1184, 50);
            this.PrintForm_Panel_Buttons.TabIndex = 1;
            
            this.PrintForm_Button_Print.Location = new System.Drawing.Point(950, 10);
            this.PrintForm_Button_Print.Name = "PrintForm_Button_Print";
            this.PrintForm_Button_Print.Size = new System.Drawing.Size(100, 32);
            this.PrintForm_Button_Print.TabIndex = 0;
            this.PrintForm_Button_Print.Text = "Print";
            this.PrintForm_Button_Print.UseVisualStyleBackColor = true;
            
            this.PrintForm_Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.PrintForm_Button_Cancel.Location = new System.Drawing.Point(1060, 10);
            this.PrintForm_Button_Cancel.Name = "PrintForm_Button_Cancel";
            this.PrintForm_Button_Cancel.Size = new System.Drawing.Size(100, 32);
            this.PrintForm_Button_Cancel.TabIndex = 1;
            this.PrintForm_Button_Cancel.Text = "Cancel";
            this.PrintForm_Button_Cancel.UseVisualStyleBackColor = true;
            
            // Status Strip
            this.PrintForm_StatusStrip_Main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.PrintForm_ToolStripStatusLabel_Status});
            this.PrintForm_StatusStrip_Main.Location = new System.Drawing.Point(0, 681);
            this.PrintForm_StatusStrip_Main.Name = "PrintForm_StatusStrip_Main";
            this.PrintForm_StatusStrip_Main.Size = new System.Drawing.Size(1184, 25);
            this.PrintForm_StatusStrip_Main.TabIndex = 2;
            
            this.PrintForm_ToolStripStatusLabel_Status.Name = "PrintForm_ToolStripStatusLabel_Status";
            this.PrintForm_ToolStripStatusLabel_Status.Size = new System.Drawing.Size(39, 20);
            this.PrintForm_ToolStripStatusLabel_Status.Text = "Ready";
            
            // 
            // PrintForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.PrintForm_Button_Cancel;
            this.ClientSize = new System.Drawing.Size(1184, 756);
            this.Controls.Add(this.PrintForm_TabControl_Main);
            this.Controls.Add(this.PrintForm_StatusStrip_Main);
            this.Controls.Add(this.PrintForm_Panel_Buttons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "PrintForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Print Configuration";
            
            // Wire up event handlers
            this.PrintForm_Button_Print.Click += new System.EventHandler(this.PrintForm_Button_Print_Click);
            this.PrintForm_Button_Cancel.Click += new System.EventHandler(this.PrintForm_Button_Cancel_Click);
            this.PrintForm_Button_ExportSettings.Click += new System.EventHandler(this.PrintForm_Button_ExportSettings_Click);
            this.PrintForm_Button_SelectAll.Click += new System.EventHandler(this.PrintForm_Button_SelectAll_Click);
            this.PrintForm_Button_DeselectAll.Click += new System.EventHandler(this.PrintForm_Button_DeselectAll_Click);
            this.PrintForm_Button_MoveUp.Click += new System.EventHandler(this.PrintForm_Button_MoveUp_Click);
            this.PrintForm_Button_MoveDown.Click += new System.EventHandler(this.PrintForm_Button_MoveDown_Click);
            this.PrintForm_Button_AddFilter.Click += new System.EventHandler(this.PrintForm_Button_AddFilter_Click);
            this.PrintForm_Button_RemoveFilter.Click += new System.EventHandler(this.PrintForm_Button_RemoveFilter_Click);
            this.PrintForm_Button_ClearFilters.Click += new System.EventHandler(this.PrintForm_Button_ClearFilters_Click);
            this.PrintForm_Button_FirstPage.Click += new System.EventHandler(this.PrintForm_Button_FirstPage_Click);
            this.PrintForm_Button_PreviousPage.Click += new System.EventHandler(this.PrintForm_Button_PreviousPage_Click);
            this.PrintForm_Button_NextPage.Click += new System.EventHandler(this.PrintForm_Button_NextPage_Click);
            this.PrintForm_Button_LastPage.Click += new System.EventHandler(this.PrintForm_Button_LastPage_Click);
            this.PrintForm_Button_ZoomIn.Click += new System.EventHandler(this.PrintForm_Button_ZoomIn_Click);
            this.PrintForm_Button_ZoomOut.Click += new System.EventHandler(this.PrintForm_Button_ZoomOut_Click);
            this.PrintForm_Button_SavePreset.Click += new System.EventHandler(this.PrintForm_Button_SavePreset_Click);
            this.PrintForm_Button_DeletePreset.Click += new System.EventHandler(this.PrintForm_Button_DeletePreset_Click);
            this.PrintForm_ComboBox_Zoom.SelectedIndexChanged += new System.EventHandler(this.PrintForm_ComboBox_Zoom_SelectedIndexChanged);
            this.PrintForm_ComboBox_Printer.SelectedIndexChanged += new System.EventHandler(this.PrintForm_ComboBox_Printer_SelectedIndexChanged);
            this.PrintForm_ComboBox_FilterType.SelectedIndexChanged += new System.EventHandler(this.PrintForm_ComboBox_FilterType_SelectedIndexChanged);
            this.PrintForm_ComboBox_Presets.SelectedIndexChanged += new System.EventHandler(this.PrintForm_ComboBox_Presets_SelectedIndexChanged);
            this.PrintForm_RadioButton_Landscape.CheckedChanged += new System.EventHandler(this.PrintForm_RadioButton_Landscape_CheckedChanged);
            this.PrintForm_RadioButton_Color.CheckedChanged += new System.EventHandler(this.PrintForm_RadioButton_Color_CheckedChanged);
            this.PrintForm_RadioButton_AllPages.CheckedChanged += new System.EventHandler(this.PageRange_CheckedChanged);
            this.PrintForm_RadioButton_CurrentPage.CheckedChanged += new System.EventHandler(this.PageRange_CheckedChanged);
            this.PrintForm_RadioButton_PageRange.CheckedChanged += new System.EventHandler(this.PageRange_CheckedChanged);
            this.PrintForm_CheckedListBox_Columns.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.PrintForm_CheckedListBox_Columns_ItemCheck);
            
            this.PrintForm_TabControl_Main.ResumeLayout(false);
            this.PrintForm_TabPage_Preview.ResumeLayout(false);
            this.PrintForm_Panel_PreviewToolbar.ResumeLayout(false);
            this.PrintForm_Panel_PreviewToolbar.PerformLayout();
            this.PrintForm_TabPage_Settings.ResumeLayout(false);
            this.PrintForm_GroupBox_ColorMode.ResumeLayout(false);
            this.PrintForm_GroupBox_ColorMode.PerformLayout();
            this.PrintForm_GroupBox_Orientation.ResumeLayout(false);
            this.PrintForm_GroupBox_Orientation.PerformLayout();
            this.PrintForm_GroupBox_Printer.ResumeLayout(false);
            this.PrintForm_GroupBox_Printer.PerformLayout();
            this.PrintForm_GroupBox_PageRange.ResumeLayout(false);
            this.PrintForm_GroupBox_PageRange.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PrintForm_NumericUpDown_FromPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PrintForm_NumericUpDown_ToPage)).EndInit();
            this.PrintForm_GroupBox_Export.ResumeLayout(false);
            this.PrintForm_GroupBox_Export.PerformLayout();
            this.PrintForm_TabPage_Columns.ResumeLayout(false);
            this.PrintForm_Panel_ColumnButtons.ResumeLayout(false);
            this.PrintForm_GroupBox_Filters.ResumeLayout(false);
            this.PrintForm_GroupBox_Filters.PerformLayout();
            this.PrintForm_Panel_Buttons.ResumeLayout(false);
            this.PrintForm_GroupBox_Presets.ResumeLayout(false);
            this.PrintForm_StatusStrip_Main.ResumeLayout(false);
            this.PrintForm_StatusStrip_Main.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TabControl PrintForm_TabControl_Main;
        private System.Windows.Forms.TabPage PrintForm_TabPage_Preview;
        private System.Windows.Forms.PrintPreviewControl PrintForm_PrintPreviewControl_Main;
        private System.Windows.Forms.Panel PrintForm_Panel_PreviewToolbar;
        private System.Windows.Forms.Button PrintForm_Button_ZoomOut;
        private System.Windows.Forms.Button PrintForm_Button_ZoomIn;
        private System.Windows.Forms.ComboBox PrintForm_ComboBox_Zoom;
        private System.Windows.Forms.Button PrintForm_Button_FirstPage;
        private System.Windows.Forms.Button PrintForm_Button_PreviousPage;
        private System.Windows.Forms.Label PrintForm_Label_PageInfo;
        private System.Windows.Forms.Button PrintForm_Button_NextPage;
        private System.Windows.Forms.Button PrintForm_Button_LastPage;
        private System.Windows.Forms.TabPage PrintForm_TabPage_Settings;
        private System.Windows.Forms.GroupBox PrintForm_GroupBox_Printer;
        private System.Windows.Forms.ComboBox PrintForm_ComboBox_Printer;
        private System.Windows.Forms.GroupBox PrintForm_GroupBox_ColorMode;
        private System.Windows.Forms.RadioButton PrintForm_RadioButton_Color;
        private System.Windows.Forms.RadioButton PrintForm_RadioButton_BlackWhite;
        private System.Windows.Forms.GroupBox PrintForm_GroupBox_Orientation;
        private System.Windows.Forms.RadioButton PrintForm_RadioButton_Portrait;
        private System.Windows.Forms.RadioButton PrintForm_RadioButton_Landscape;
        private System.Windows.Forms.GroupBox PrintForm_GroupBox_PageRange;
        private System.Windows.Forms.RadioButton PrintForm_RadioButton_AllPages;
        private System.Windows.Forms.RadioButton PrintForm_RadioButton_CurrentPage;
        private System.Windows.Forms.RadioButton PrintForm_RadioButton_PageRange;
        private System.Windows.Forms.NumericUpDown PrintForm_NumericUpDown_FromPage;
        private System.Windows.Forms.NumericUpDown PrintForm_NumericUpDown_ToPage;
        private System.Windows.Forms.Label PrintForm_Label_PageFrom;
        private System.Windows.Forms.Label PrintForm_Label_PageTo;
        private System.Windows.Forms.GroupBox PrintForm_GroupBox_Export;
        private System.Windows.Forms.CheckBox PrintForm_CheckBox_ExportPdf;
        private System.Windows.Forms.CheckBox PrintForm_CheckBox_ExportExcel;
        private System.Windows.Forms.CheckBox PrintForm_CheckBox_ExportImage;
        private System.Windows.Forms.Button PrintForm_Button_ExportSettings;
        private System.Windows.Forms.TabPage PrintForm_TabPage_Columns;
        private System.Windows.Forms.CheckedListBox PrintForm_CheckedListBox_Columns;
        private System.Windows.Forms.Panel PrintForm_Panel_ColumnButtons;
        private System.Windows.Forms.Button PrintForm_Button_SelectAll;
        private System.Windows.Forms.Button PrintForm_Button_DeselectAll;
        private System.Windows.Forms.Button PrintForm_Button_MoveUp;
        private System.Windows.Forms.Button PrintForm_Button_MoveDown;
        private System.Windows.Forms.GroupBox PrintForm_GroupBox_Filters;
        private System.Windows.Forms.ComboBox PrintForm_ComboBox_FilterColumn;
        private System.Windows.Forms.ComboBox PrintForm_ComboBox_FilterType;
        private System.Windows.Forms.TextBox PrintForm_TextBox_FilterValue;
        private System.Windows.Forms.DateTimePicker PrintForm_DateTimePicker_DateFrom;
        private System.Windows.Forms.DateTimePicker PrintForm_DateTimePicker_DateTo;
        private System.Windows.Forms.Button PrintForm_Button_AddFilter;
        private System.Windows.Forms.ListBox PrintForm_ListBox_ActiveFilters;
        private System.Windows.Forms.Button PrintForm_Button_RemoveFilter;
        private System.Windows.Forms.Button PrintForm_Button_ClearFilters;
        private System.Windows.Forms.Panel PrintForm_Panel_Buttons;
        private System.Windows.Forms.Button PrintForm_Button_Print;
        private System.Windows.Forms.Button PrintForm_Button_Cancel;
        private System.Windows.Forms.GroupBox PrintForm_GroupBox_Presets;
        private System.Windows.Forms.ComboBox PrintForm_ComboBox_Presets;
        private System.Windows.Forms.Button PrintForm_Button_SavePreset;
        private System.Windows.Forms.Button PrintForm_Button_DeletePreset;
        private System.Windows.Forms.StatusStrip PrintForm_StatusStrip_Main;
        private System.Windows.Forms.ToolStripStatusLabel PrintForm_ToolStripStatusLabel_Status;
    }
}
