namespace MTM_WIP_Application_Winforms.Controls.MainForm.Control_AdvancedInventory
{
    partial class Control_AdvInv_Main
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
            // Session Header Panel
            this.Panel_SessionHeader = new System.Windows.Forms.Panel();
            this.Label_UserSession = new System.Windows.Forms.Label();
            this.Button_SwitchToClassic = new System.Windows.Forms.Button();
            this.Button_RefreshCache = new System.Windows.Forms.Button();
            this.Button_ViewLog = new System.Windows.Forms.Button();

            // Step Navigation Panel
            this.Panel_StepNavigation = new System.Windows.Forms.Panel();
            this.Label_CurrentStep = new System.Windows.Forms.Label();

            // Mode Selection Panel
            this.Panel_ModeSelection = new System.Windows.Forms.Panel();
            this.Label_SelectMode = new System.Windows.Forms.Label();

            // Single Transaction Card
            this.Panel_SingleTransaction = new System.Windows.Forms.Panel();
            this.Label_SingleTransaction_Title = new System.Windows.Forms.Label();
            this.Label_SingleTransaction_Desc = new System.Windows.Forms.Label();
            this.Label_SingleTransaction_BestFor = new System.Windows.Forms.Label();
            this.Button_SelectSingleTransaction = new System.Windows.Forms.Button();

            // Batch Entry Card
            this.Panel_BatchEntry = new System.Windows.Forms.Panel();
            this.Label_BatchEntry_Title = new System.Windows.Forms.Label();
            this.Label_BatchEntry_Desc = new System.Windows.Forms.Label();
            this.Label_BatchEntry_BestFor = new System.Windows.Forms.Label();
            this.Button_SelectBatchEntry = new System.Windows.Forms.Button();

            // Excel Import Card
            this.Panel_ExcelImport = new System.Windows.Forms.Panel();
            this.Label_ExcelImport_Title = new System.Windows.Forms.Label();
            this.Label_ExcelImport_Desc = new System.Windows.Forms.Label();
            this.Label_ExcelImport_BestFor = new System.Windows.Forms.Label();
            this.Button_SelectExcelImport = new System.Windows.Forms.Button();

            // Quick Templates Card
            this.Panel_Templates = new System.Windows.Forms.Panel();
            this.Label_Templates_Title = new System.Windows.Forms.Label();
            this.Label_Templates_Desc = new System.Windows.Forms.Label();
            this.Label_Templates_BestFor = new System.Windows.Forms.Label();
            this.Button_SelectTemplate = new System.Windows.Forms.Button();

            // Bottom Navigation
            this.Panel_BottomNavigation = new System.Windows.Forms.Panel();
            this.Button_Back = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.Button_Help = new System.Windows.Forms.Button();
            this.Button_Next = new System.Windows.Forms.Button();

            SuspendLayout();
            // 
            // Control_AdvInv_Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.Panel_SessionHeader);
            this.Controls.Add(this.Panel_StepNavigation);
            this.Controls.Add(this.Panel_ModeSelection);
            this.Controls.Add(this.Panel_BottomNavigation);
            MaximumSize = new Size(800, 375);
            MinimumSize = new Size(800, 375);
            Name = "Control_AdvInv_Main";
            Size = new Size(800, 375);
            ResumeLayout(false);
        }

        #endregion

        #region Controls

        // Session Header
        private System.Windows.Forms.Panel Panel_SessionHeader;
        private System.Windows.Forms.Label Label_UserSession;
        private System.Windows.Forms.Button Button_SwitchToClassic;
        private System.Windows.Forms.Button Button_RefreshCache;
        private System.Windows.Forms.Button Button_ViewLog;

        // Step Navigation
        private System.Windows.Forms.Panel Panel_StepNavigation;
        private System.Windows.Forms.Label Label_CurrentStep;

        // Mode Selection
        private System.Windows.Forms.Panel Panel_ModeSelection;
        private System.Windows.Forms.Label Label_SelectMode;

        // Single Transaction Card
        private System.Windows.Forms.Panel Panel_SingleTransaction;
        private System.Windows.Forms.Label Label_SingleTransaction_Title;
        private System.Windows.Forms.Label Label_SingleTransaction_Desc;
        private System.Windows.Forms.Label Label_SingleTransaction_BestFor;
        private System.Windows.Forms.Button Button_SelectSingleTransaction;

        // Batch Entry Card
        private System.Windows.Forms.Panel Panel_BatchEntry;
        private System.Windows.Forms.Label Label_BatchEntry_Title;
        private System.Windows.Forms.Label Label_BatchEntry_Desc;
        private System.Windows.Forms.Label Label_BatchEntry_BestFor;
        private System.Windows.Forms.Button Button_SelectBatchEntry;

        // Excel Import Card
        private System.Windows.Forms.Panel Panel_ExcelImport;
        private System.Windows.Forms.Label Label_ExcelImport_Title;
        private System.Windows.Forms.Label Label_ExcelImport_Desc;
        private System.Windows.Forms.Label Label_ExcelImport_BestFor;
        private System.Windows.Forms.Button Button_SelectExcelImport;

        // Templates Card
        private System.Windows.Forms.Panel Panel_Templates;
        private System.Windows.Forms.Label Label_Templates_Title;
        private System.Windows.Forms.Label Label_Templates_Desc;
        private System.Windows.Forms.Label Label_Templates_BestFor;
        private System.Windows.Forms.Button Button_SelectTemplate;

        // Bottom Navigation
        private System.Windows.Forms.Panel Panel_BottomNavigation;
        private System.Windows.Forms.Button Button_Back;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.Button Button_Help;
        private System.Windows.Forms.Button Button_Next;

        #endregion
    }
}
