namespace MTM_WIP_Application_Winforms.Controls.MainForm.Control_AdvancedInventory
{
    partial class Control_AdvInv_ExcelImport
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
            // Session Header
            this.Panel_Header = new System.Windows.Forms.Panel();
            this.Label_UserSession = new System.Windows.Forms.Label();
            this.Button_SwitchToManualEntry = new System.Windows.Forms.Button();

            // Excel File Management Panel
            this.Panel_ExcelFileManagement = new System.Windows.Forms.Panel();
            this.Label_FileManagement_Title = new System.Windows.Forms.Label();
            this.Label_TemplateFile = new System.Windows.Forms.Label();
            this.Label_FileStatus = new System.Windows.Forms.Label();
            this.Button_OpenInExcel = new System.Windows.Forms.Button();
            this.Button_ImportNow = new System.Windows.Forms.Button();
            this.Button_DownloadTemplate = new System.Windows.Forms.Button();

            // Validation Results Panel (Collapsible)
            this.Panel_ValidationResults = new System.Windows.Forms.Panel();
            this.Label_ValidationResults_Title = new System.Windows.Forms.Label();
            this.Label_IssueCount = new System.Windows.Forms.Label();
            this.Button_ExpandValidation = new System.Windows.Forms.Button();
            this.DataGridView_ValidationIssues = new System.Windows.Forms.DataGridView();

            // Data Preview Panel
            this.Panel_DataPreview = new System.Windows.Forms.Panel();
            this.Label_DataPreview_Title = new System.Windows.Forms.Label();
            this.Label_PreviewSummary = new System.Windows.Forms.Label();
            this.DataGridView_Preview = new System.Windows.Forms.DataGridView();

            // Bottom Navigation
            this.Panel_BottomNavigation = new System.Windows.Forms.Panel();
            this.Button_SkipInvalidAndSave = new System.Windows.Forms.Button();
            this.Button_FixInExcel = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.Label_Tips = new System.Windows.Forms.Label();

            SuspendLayout();
            // 
            // Control_AdvInv_ExcelImport
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.Panel_Header);
            this.Controls.Add(this.Panel_ExcelFileManagement);
            this.Controls.Add(this.Panel_ValidationResults);
            this.Controls.Add(this.Panel_DataPreview);
            this.Controls.Add(this.Panel_BottomNavigation);
            MaximumSize = new Size(800, 375);
            MinimumSize = new Size(800, 375);
            Name = "Control_AdvInv_ExcelImport";
            Size = new Size(800, 375);
            ResumeLayout(false);
        }

        #endregion

        #region Controls

        // Header
        private System.Windows.Forms.Panel Panel_Header;
        private System.Windows.Forms.Label Label_UserSession;
        private System.Windows.Forms.Button Button_SwitchToManualEntry;

        // Excel File Management
        private System.Windows.Forms.Panel Panel_ExcelFileManagement;
        private System.Windows.Forms.Label Label_FileManagement_Title;
        private System.Windows.Forms.Label Label_TemplateFile;
        private System.Windows.Forms.Label Label_FileStatus;
        private System.Windows.Forms.Button Button_OpenInExcel;
        private System.Windows.Forms.Button Button_ImportNow;
        private System.Windows.Forms.Button Button_DownloadTemplate;

        // Validation Results
        private System.Windows.Forms.Panel Panel_ValidationResults;
        private System.Windows.Forms.Label Label_ValidationResults_Title;
        private System.Windows.Forms.Label Label_IssueCount;
        private System.Windows.Forms.Button Button_ExpandValidation;
        private System.Windows.Forms.DataGridView DataGridView_ValidationIssues;

        // Data Preview
        private System.Windows.Forms.Panel Panel_DataPreview;
        private System.Windows.Forms.Label Label_DataPreview_Title;
        private System.Windows.Forms.Label Label_PreviewSummary;
        private System.Windows.Forms.DataGridView DataGridView_Preview;

        // Bottom Navigation
        private System.Windows.Forms.Panel Panel_BottomNavigation;
        private System.Windows.Forms.Button Button_SkipInvalidAndSave;
        private System.Windows.Forms.Button Button_FixInExcel;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.Label Label_Tips;

        #endregion
    }
}
