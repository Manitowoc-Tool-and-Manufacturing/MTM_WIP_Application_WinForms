namespace MTM_WIP_Application_Winforms.Controls.MainForm.Control_AdvInv
{
    partial class Control_AdvInv_Review
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
            this.Label_Mode = new System.Windows.Forms.Label();

            // Step Navigation
            this.Panel_StepNavigation = new System.Windows.Forms.Panel();
            this.Label_CurrentStep = new System.Windows.Forms.Label();

            // Pre-Save Validation Panel
            this.Panel_Validation = new System.Windows.Forms.Panel();
            this.Label_Validation_Title = new System.Windows.Forms.Label();
            this.Label_ValidationStatus_AllValid = new System.Windows.Forms.Label();
            this.Label_ValidationStatus_NoDuplicates = new System.Windows.Forms.Label();
            this.Label_ValidationStatus_VolumeWarning = new System.Windows.Forms.Label();
            this.Label_ValidationStatus_DatabaseConnection = new System.Windows.Forms.Label();
            this.Button_DryRunValidation = new System.Windows.Forms.Button();

            // Transaction Preview Panel
            this.Panel_TransactionPreview = new System.Windows.Forms.Panel();
            this.Label_TransactionPreview_Title = new System.Windows.Forms.Label();
            this.DataGridView_Preview = new System.Windows.Forms.DataGridView();
            this.Label_Summary_TotalTransactions = new System.Windows.Forms.Label();
            this.Label_Summary_TotalQuantity = new System.Windows.Forms.Label();
            this.Label_Summary_EstimatedTime = new System.Windows.Forms.Label();

            // Final Confirmation Panel
            this.Panel_Confirmation = new System.Windows.Forms.Panel();
            this.Label_Confirmation_Title = new System.Windows.Forms.Label();
            this.Label_Confirmation_WillCreate = new System.Windows.Forms.Label();
            this.Label_Confirmation_WillUpdate = new System.Windows.Forms.Label();
            this.Label_Confirmation_WillLog = new System.Windows.Forms.Label();
            this.CheckBox_EmailNotification = new System.Windows.Forms.CheckBox();
            this.CheckBox_PrintSummary = new System.Windows.Forms.CheckBox();

            // Bottom Navigation
            this.Panel_BottomNavigation = new System.Windows.Forms.Panel();
            this.Button_BackToEdit = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.Button_SaveAll = new System.Windows.Forms.Button();

            SuspendLayout();
            // 
            // Control_AdvInv_Review
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.Panel_Header);
            this.Controls.Add(this.Panel_StepNavigation);
            this.Controls.Add(this.Panel_Validation);
            this.Controls.Add(this.Panel_TransactionPreview);
            this.Controls.Add(this.Panel_Confirmation);
            this.Controls.Add(this.Panel_BottomNavigation);
            MaximumSize = new Size(800, 375);
            MinimumSize = new Size(800, 375);
            Name = "Control_AdvInv_Review";
            Size = new Size(800, 375);
            ResumeLayout(false);
        }

        #endregion

        #region Controls

        // Header
        private System.Windows.Forms.Panel Panel_Header;
        private System.Windows.Forms.Label Label_UserSession;
        private System.Windows.Forms.Label Label_Mode;

        // Step Navigation
        private System.Windows.Forms.Panel Panel_StepNavigation;
        private System.Windows.Forms.Label Label_CurrentStep;

        // Validation
        private System.Windows.Forms.Panel Panel_Validation;
        private System.Windows.Forms.Label Label_Validation_Title;
        private System.Windows.Forms.Label Label_ValidationStatus_AllValid;
        private System.Windows.Forms.Label Label_ValidationStatus_NoDuplicates;
        private System.Windows.Forms.Label Label_ValidationStatus_VolumeWarning;
        private System.Windows.Forms.Label Label_ValidationStatus_DatabaseConnection;
        private System.Windows.Forms.Button Button_DryRunValidation;

        // Transaction Preview
        private System.Windows.Forms.Panel Panel_TransactionPreview;
        private System.Windows.Forms.Label Label_TransactionPreview_Title;
        private System.Windows.Forms.DataGridView DataGridView_Preview;
        private System.Windows.Forms.Label Label_Summary_TotalTransactions;
        private System.Windows.Forms.Label Label_Summary_TotalQuantity;
        private System.Windows.Forms.Label Label_Summary_EstimatedTime;

        // Confirmation
        private System.Windows.Forms.Panel Panel_Confirmation;
        private System.Windows.Forms.Label Label_Confirmation_Title;
        private System.Windows.Forms.Label Label_Confirmation_WillCreate;
        private System.Windows.Forms.Label Label_Confirmation_WillUpdate;
        private System.Windows.Forms.Label Label_Confirmation_WillLog;
        private System.Windows.Forms.CheckBox CheckBox_EmailNotification;
        private System.Windows.Forms.CheckBox CheckBox_PrintSummary;

        // Bottom Navigation
        private System.Windows.Forms.Panel Panel_BottomNavigation;
        private System.Windows.Forms.Button Button_BackToEdit;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.Button Button_SaveAll;

        #endregion
    }
}
