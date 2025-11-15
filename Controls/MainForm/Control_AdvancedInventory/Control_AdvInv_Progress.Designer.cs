namespace MTM_WIP_Application_Winforms.Controls.MainForm.Control_AdvancedInventory
{
    partial class Control_AdvInv_Progress
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

            // Step Navigation
            this.Panel_StepNavigation = new System.Windows.Forms.Panel();
            this.Label_CurrentStep = new System.Windows.Forms.Label();

            // Progress Display Panel
            this.Panel_ProgressDisplay = new System.Windows.Forms.Panel();
            this.Label_ProgressTitle = new System.Windows.Forms.Label();
            this.ProgressBar_Overall = new System.Windows.Forms.ProgressBar();
            this.Label_OverallProgress = new System.Windows.Forms.Label();
            this.Label_CurrentItem = new System.Windows.Forms.Label();
            this.Label_TimeElapsed = new System.Windows.Forms.Label();
            this.Label_EstimatedTimeRemaining = new System.Windows.Forms.Label();

            // Transaction Log Panel
            this.Panel_TransactionLog = new System.Windows.Forms.Panel();
            this.Label_TransactionLog_Title = new System.Windows.Forms.Label();
            this.ListView_TransactionLog = new System.Windows.Forms.ListView();
            this.Label_LogSummary = new System.Windows.Forms.Label();

            // Status Summary Panel
            this.Panel_StatusSummary = new System.Windows.Forms.Panel();
            this.Label_StatusSummary_Title = new System.Windows.Forms.Label();
            this.Label_SuccessCount = new System.Windows.Forms.Label();
            this.Label_FailureCount = new System.Windows.Forms.Label();
            this.Label_SkippedCount = new System.Windows.Forms.Label();
            this.Label_TotalProcessed = new System.Windows.Forms.Label();

            // Bottom Navigation
            this.Panel_BottomNavigation = new System.Windows.Forms.Panel();
            this.Button_CancelOperation = new System.Windows.Forms.Button();
            this.Button_ViewFailedItems = new System.Windows.Forms.Button();
            this.Button_ExportLog = new System.Windows.Forms.Button();
            this.Button_Done = new System.Windows.Forms.Button();

            SuspendLayout();
            // 
            // Control_AdvInv_Progress
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.Panel_Header);
            this.Controls.Add(this.Panel_StepNavigation);
            this.Controls.Add(this.Panel_ProgressDisplay);
            this.Controls.Add(this.Panel_TransactionLog);
            this.Controls.Add(this.Panel_StatusSummary);
            this.Controls.Add(this.Panel_BottomNavigation);
            MaximumSize = new Size(800, 375);
            MinimumSize = new Size(800, 375);
            Name = "Control_AdvInv_Progress";
            Size = new Size(800, 375);
            ResumeLayout(false);
        }

        #endregion

        #region Controls

        // Header
        private System.Windows.Forms.Panel Panel_Header;
        private System.Windows.Forms.Label Label_UserSession;

        // Step Navigation
        private System.Windows.Forms.Panel Panel_StepNavigation;
        private System.Windows.Forms.Label Label_CurrentStep;

        // Progress Display
        private System.Windows.Forms.Panel Panel_ProgressDisplay;
        private System.Windows.Forms.Label Label_ProgressTitle;
        private System.Windows.Forms.ProgressBar ProgressBar_Overall;
        private System.Windows.Forms.Label Label_OverallProgress;
        private System.Windows.Forms.Label Label_CurrentItem;
        private System.Windows.Forms.Label Label_TimeElapsed;
        private System.Windows.Forms.Label Label_EstimatedTimeRemaining;

        // Transaction Log
        private System.Windows.Forms.Panel Panel_TransactionLog;
        private System.Windows.Forms.Label Label_TransactionLog_Title;
        private System.Windows.Forms.ListView ListView_TransactionLog;
        private System.Windows.Forms.Label Label_LogSummary;

        // Status Summary
        private System.Windows.Forms.Panel Panel_StatusSummary;
        private System.Windows.Forms.Label Label_StatusSummary_Title;
        private System.Windows.Forms.Label Label_SuccessCount;
        private System.Windows.Forms.Label Label_FailureCount;
        private System.Windows.Forms.Label Label_SkippedCount;
        private System.Windows.Forms.Label Label_TotalProcessed;

        // Bottom Navigation
        private System.Windows.Forms.Panel Panel_BottomNavigation;
        private System.Windows.Forms.Button Button_CancelOperation;
        private System.Windows.Forms.Button Button_ViewFailedItems;
        private System.Windows.Forms.Button Button_ExportLog;
        private System.Windows.Forms.Button Button_Done;

        #endregion
    }
}
