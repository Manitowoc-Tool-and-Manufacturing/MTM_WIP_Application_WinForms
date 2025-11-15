namespace MTM_WIP_Application_Winforms.Controls.MainForm.Control_AdvancedInventory
{
    partial class Control_AdvInv_SingleEntry
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

            // Transaction Details Panel
            this.Panel_TransactionDetails = new System.Windows.Forms.Panel();
            this.Label_TransactionDetails_Title = new System.Windows.Forms.Label();
            this.Button_Help = new System.Windows.Forms.Button();

            // Part Number Field
            this.Label_PartNumber = new System.Windows.Forms.Label();
            this.SuggestionTextBox_Part = new Controls.Shared.SuggestionTextBox();
            this.Button_Part_F4 = new System.Windows.Forms.Button();
            this.Label_Part_Status = new System.Windows.Forms.Label();
            this.Panel_Part_ColorCodeWarning = new System.Windows.Forms.Panel();
            this.Label_ColorCodeWarning = new System.Windows.Forms.Label();
            this.Button_SwitchToColorEntry = new System.Windows.Forms.Button();

            // Operation Field
            this.Label_Operation = new System.Windows.Forms.Label();
            this.SuggestionTextBox_Operation = new Controls.Shared.SuggestionTextBox();
            this.Button_Operation_F4 = new System.Windows.Forms.Button();
            this.Label_Operation_Status = new System.Windows.Forms.Label();

            // Location Field
            this.Label_Location = new System.Windows.Forms.Label();
            this.SuggestionTextBox_Location = new Controls.Shared.SuggestionTextBox();
            this.Button_Location_F4 = new System.Windows.Forms.Button();
            this.Label_Location_Status = new System.Windows.Forms.Label();

            // Quantity Field
            this.Label_Quantity = new System.Windows.Forms.Label();
            this.TextBox_Quantity = new System.Windows.Forms.TextBox();
            this.Label_Quantity_Status = new System.Windows.Forms.Label();

            // Notes Field
            this.Label_Notes = new System.Windows.Forms.Label();
            this.RichTextBox_Notes = new System.Windows.Forms.RichTextBox();

            // Tips Panel
            this.Panel_Tips = new System.Windows.Forms.Panel();
            this.Label_Tips = new System.Windows.Forms.Label();

            // Bottom Navigation
            this.Panel_BottomNavigation = new System.Windows.Forms.Panel();
            this.Button_Back = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.Button_ReviewAndSave = new System.Windows.Forms.Button();

            SuspendLayout();
            // 
            // Control_AdvInv_SingleEntry
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.Panel_Header);
            this.Controls.Add(this.Panel_StepNavigation);
            this.Controls.Add(this.Panel_TransactionDetails);
            this.Controls.Add(this.Panel_Tips);
            this.Controls.Add(this.Panel_BottomNavigation);
            MaximumSize = new Size(800, 375);
            MinimumSize = new Size(800, 375);
            Name = "Control_AdvInv_SingleEntry";
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

        // Transaction Details
        private System.Windows.Forms.Panel Panel_TransactionDetails;
        private System.Windows.Forms.Label Label_TransactionDetails_Title;
        private System.Windows.Forms.Button Button_Help;

        // Part Number
        private System.Windows.Forms.Label Label_PartNumber;
        private Controls.Shared.SuggestionTextBox SuggestionTextBox_Part;
        private System.Windows.Forms.Button Button_Part_F4;
        private System.Windows.Forms.Label Label_Part_Status;
        private System.Windows.Forms.Panel Panel_Part_ColorCodeWarning;
        private System.Windows.Forms.Label Label_ColorCodeWarning;
        private System.Windows.Forms.Button Button_SwitchToColorEntry;

        // Operation
        private System.Windows.Forms.Label Label_Operation;
        private Controls.Shared.SuggestionTextBox SuggestionTextBox_Operation;
        private System.Windows.Forms.Button Button_Operation_F4;
        private System.Windows.Forms.Label Label_Operation_Status;

        // Location
        private System.Windows.Forms.Label Label_Location;
        private Controls.Shared.SuggestionTextBox SuggestionTextBox_Location;
        private System.Windows.Forms.Button Button_Location_F4;
        private System.Windows.Forms.Label Label_Location_Status;

        // Quantity
        private System.Windows.Forms.Label Label_Quantity;
        private System.Windows.Forms.TextBox TextBox_Quantity;
        private System.Windows.Forms.Label Label_Quantity_Status;

        // Notes
        private System.Windows.Forms.Label Label_Notes;
        private System.Windows.Forms.RichTextBox RichTextBox_Notes;

        // Tips
        private System.Windows.Forms.Panel Panel_Tips;
        private System.Windows.Forms.Label Label_Tips;

        // Bottom Navigation
        private System.Windows.Forms.Panel Panel_BottomNavigation;
        private System.Windows.Forms.Button Button_Back;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.Button Button_ReviewAndSave;

        #endregion
    }
}
