namespace MTM_WIP_Application_Winforms.Controls.MainForm.Control_AdvancedInventory
{
    partial class Control_AdvInv_BatchEntry
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

            // Base Transaction Panel (Locked)
            this.Panel_BaseTransaction = new System.Windows.Forms.Panel();
            this.Label_BaseTransaction_Title = new System.Windows.Forms.Label();
            this.Label_PartLocked = new System.Windows.Forms.Label();
            this.Label_PartValue = new System.Windows.Forms.Label();
            this.Button_ChangePart = new System.Windows.Forms.Button();
            this.Label_OpLocked = new System.Windows.Forms.Label();
            this.Label_OpValue = new System.Windows.Forms.Label();

            // Add Location Panel
            this.Panel_AddLocation = new System.Windows.Forms.Panel();
            this.Label_AddLocation_Title = new System.Windows.Forms.Label();

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
            this.TextBox_Notes = new System.Windows.Forms.TextBox();

            // Add Location Buttons
            this.Button_AddToList = new System.Windows.Forms.Button();
            this.Button_ResetFields = new System.Windows.Forms.Button();

            // Staged Locations Panel
            this.Panel_StagedLocations = new System.Windows.Forms.Panel();
            this.Label_StagedLocations_Title = new System.Windows.Forms.Label();
            this.Label_TotalQuantity = new System.Windows.Forms.Label();
            this.ListView_Locations = new System.Windows.Forms.ListView();
            this.Button_EditSelected = new System.Windows.Forms.Button();
            this.Button_DeleteSelected = new System.Windows.Forms.Button();

            // Preview Summary Panel
            this.Panel_Summary = new System.Windows.Forms.Panel();
            this.Label_Summary_Title = new System.Windows.Forms.Label();
            this.Label_Summary_Part = new System.Windows.Forms.Label();
            this.Label_Summary_Op = new System.Windows.Forms.Label();
            this.Label_Summary_Locations = new System.Windows.Forms.Label();
            this.Label_Summary_Quantity = new System.Windows.Forms.Label();
            this.Button_Review = new System.Windows.Forms.Button();
            this.Button_Export = new System.Windows.Forms.Button();

            // Bottom Navigation
            this.Panel_BottomNavigation = new System.Windows.Forms.Panel();
            this.Button_ClearAll = new System.Windows.Forms.Button();
            this.Button_Back = new System.Windows.Forms.Button();
            this.Label_Tips = new System.Windows.Forms.Label();
            this.Button_SaveBatch = new System.Windows.Forms.Button();

            SuspendLayout();
            // 
            // Control_AdvInv_BatchEntry
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.Panel_Header);
            this.Controls.Add(this.Panel_StepNavigation);
            this.Controls.Add(this.Panel_BaseTransaction);
            this.Controls.Add(this.Panel_AddLocation);
            this.Controls.Add(this.Panel_StagedLocations);
            this.Controls.Add(this.Panel_Summary);
            this.Controls.Add(this.Panel_BottomNavigation);
            MaximumSize = new Size(800, 375);
            MinimumSize = new Size(800, 375);
            Name = "Control_AdvInv_BatchEntry";
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

        // Base Transaction
        private System.Windows.Forms.Panel Panel_BaseTransaction;
        private System.Windows.Forms.Label Label_BaseTransaction_Title;
        private System.Windows.Forms.Label Label_PartLocked;
        private System.Windows.Forms.Label Label_PartValue;
        private System.Windows.Forms.Button Button_ChangePart;
        private System.Windows.Forms.Label Label_OpLocked;
        private System.Windows.Forms.Label Label_OpValue;

        // Add Location
        private System.Windows.Forms.Panel Panel_AddLocation;
        private System.Windows.Forms.Label Label_AddLocation_Title;
        private System.Windows.Forms.Label Label_Location;
        private Controls.Shared.SuggestionTextBox SuggestionTextBox_Location;
        private System.Windows.Forms.Button Button_Location_F4;
        private System.Windows.Forms.Label Label_Location_Status;
        private System.Windows.Forms.Label Label_Quantity;
        private System.Windows.Forms.TextBox TextBox_Quantity;
        private System.Windows.Forms.Label Label_Quantity_Status;
        private System.Windows.Forms.Label Label_Notes;
        private System.Windows.Forms.TextBox TextBox_Notes;
        private System.Windows.Forms.Button Button_AddToList;
        private System.Windows.Forms.Button Button_ResetFields;

        // Staged Locations
        private System.Windows.Forms.Panel Panel_StagedLocations;
        private System.Windows.Forms.Label Label_StagedLocations_Title;
        private System.Windows.Forms.Label Label_TotalQuantity;
        private System.Windows.Forms.ListView ListView_Locations;
        private System.Windows.Forms.Button Button_EditSelected;
        private System.Windows.Forms.Button Button_DeleteSelected;

        // Summary
        private System.Windows.Forms.Panel Panel_Summary;
        private System.Windows.Forms.Label Label_Summary_Title;
        private System.Windows.Forms.Label Label_Summary_Part;
        private System.Windows.Forms.Label Label_Summary_Op;
        private System.Windows.Forms.Label Label_Summary_Locations;
        private System.Windows.Forms.Label Label_Summary_Quantity;
        private System.Windows.Forms.Button Button_Review;
        private System.Windows.Forms.Button Button_Export;

        // Bottom Navigation
        private System.Windows.Forms.Panel Panel_BottomNavigation;
        private System.Windows.Forms.Button Button_ClearAll;
        private System.Windows.Forms.Button Button_Back;
        private System.Windows.Forms.Label Label_Tips;
        private System.Windows.Forms.Button Button_SaveBatch;

        #endregion
    }
}
