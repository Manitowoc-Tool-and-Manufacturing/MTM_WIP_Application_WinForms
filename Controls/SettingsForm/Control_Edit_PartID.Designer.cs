namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    partial class Control_Edit_PartID
    {        
        private System.ComponentModel.IContainer components = null;

        // Main Layout Controls
        private System.Windows.Forms.TableLayoutPanel Control_Edit_PartID_TableLayout_Main;
        private System.Windows.Forms.Label Control_Edit_PartID_Label_Header;
        private System.Windows.Forms.Panel Control_Edit_PartID_Panel_Content;
        private System.Windows.Forms.FlowLayoutPanel Control_Edit_PartID_FlowPanel_Actions;

        // Input Layout Controls
        private System.Windows.Forms.TableLayoutPanel Control_Edit_PartID_TableLayout_Inputs;
        
        // Inputs
        private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel Control_Edit_PartID_SuggestionBox_Part;
        private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel Control_Edit_PartID_SuggestionBox_ItemType;
        private System.Windows.Forms.Label issuedByLabel;
        private System.Windows.Forms.Label issuedByValueLabel;
        private System.Windows.Forms.CheckBox Control_Edit_PartID_CheckBox_RequiresColorCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox itemNumberTextBox;
        
        // Buttons
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button resetButton;

        #region Component Designer generated code

        private void InitializeComponent()
        {
            Control_Edit_PartID_TableLayout_Main = new System.Windows.Forms.TableLayoutPanel();
            Control_Edit_PartID_Label_Header = new System.Windows.Forms.Label();
            Control_Edit_PartID_Panel_Content = new System.Windows.Forms.Panel();
            Control_Edit_PartID_TableLayout_Inputs = new System.Windows.Forms.TableLayoutPanel();
            Control_Edit_PartID_SuggestionBox_Part = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            Control_Edit_PartID_SuggestionBox_ItemType = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            label1 = new System.Windows.Forms.Label();
            itemNumberTextBox = new System.Windows.Forms.TextBox();
            issuedByLabel = new System.Windows.Forms.Label();
            issuedByValueLabel = new System.Windows.Forms.Label();
            Control_Edit_PartID_CheckBox_RequiresColorCode = new System.Windows.Forms.CheckBox();
            Control_Edit_PartID_FlowPanel_Actions = new System.Windows.Forms.FlowLayoutPanel();
            saveButton = new System.Windows.Forms.Button();
            resetButton = new System.Windows.Forms.Button();

            Control_Edit_PartID_TableLayout_Main.SuspendLayout();
            Control_Edit_PartID_Panel_Content.SuspendLayout();
            Control_Edit_PartID_TableLayout_Inputs.SuspendLayout();
            Control_Edit_PartID_FlowPanel_Actions.SuspendLayout();
            SuspendLayout();

            // 
            // Control_Edit_PartID_TableLayout_Main
            // 
            Control_Edit_PartID_TableLayout_Main.AutoSize = true;
            Control_Edit_PartID_TableLayout_Main.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            Control_Edit_PartID_TableLayout_Main.ColumnCount = 1;
            Control_Edit_PartID_TableLayout_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            Control_Edit_PartID_TableLayout_Main.Controls.Add(Control_Edit_PartID_Label_Header, 0, 0);
            Control_Edit_PartID_TableLayout_Main.Controls.Add(Control_Edit_PartID_Panel_Content, 0, 1);
            Control_Edit_PartID_TableLayout_Main.Controls.Add(Control_Edit_PartID_FlowPanel_Actions, 0, 2);
            Control_Edit_PartID_TableLayout_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            Control_Edit_PartID_TableLayout_Main.Location = new System.Drawing.Point(0, 0);
            Control_Edit_PartID_TableLayout_Main.Name = "Control_Edit_PartID_TableLayout_Main";
            Control_Edit_PartID_TableLayout_Main.Padding = new System.Windows.Forms.Padding(20);
            Control_Edit_PartID_TableLayout_Main.RowCount = 3;
            Control_Edit_PartID_TableLayout_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            Control_Edit_PartID_TableLayout_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            Control_Edit_PartID_TableLayout_Main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            Control_Edit_PartID_TableLayout_Main.Size = new System.Drawing.Size(479, 371);
            Control_Edit_PartID_TableLayout_Main.TabIndex = 0;

            // 
            // Control_Edit_PartID_Label_Header
            // 
            Control_Edit_PartID_Label_Header.AutoSize = true;
            Control_Edit_PartID_Label_Header.Dock = System.Windows.Forms.DockStyle.Fill;
            Control_Edit_PartID_Label_Header.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            Control_Edit_PartID_Label_Header.ForeColor = System.Drawing.Color.FromArgb(45, 45, 45);
            Control_Edit_PartID_Label_Header.Location = new System.Drawing.Point(23, 20);
            Control_Edit_PartID_Label_Header.Margin = new System.Windows.Forms.Padding(3, 0, 3, 20);
            Control_Edit_PartID_Label_Header.Name = "Control_Edit_PartID_Label_Header";
            Control_Edit_PartID_Label_Header.Size = new System.Drawing.Size(433, 32);
            Control_Edit_PartID_Label_Header.TabIndex = 0;
            Control_Edit_PartID_Label_Header.Text = "Edit Part ID";
            Control_Edit_PartID_Label_Header.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // Control_Edit_PartID_Panel_Content
            // 
            Control_Edit_PartID_Panel_Content.AutoSize = true;
            Control_Edit_PartID_Panel_Content.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            Control_Edit_PartID_Panel_Content.Controls.Add(Control_Edit_PartID_TableLayout_Inputs);
            Control_Edit_PartID_Panel_Content.Dock = System.Windows.Forms.DockStyle.Fill;
            Control_Edit_PartID_Panel_Content.Location = new System.Drawing.Point(23, 75);
            Control_Edit_PartID_Panel_Content.Name = "Control_Edit_PartID_Panel_Content";
            Control_Edit_PartID_Panel_Content.Size = new System.Drawing.Size(433, 246);
            Control_Edit_PartID_Panel_Content.TabIndex = 1;

            // 
            // Control_Edit_PartID_TableLayout_Inputs
            // 
            Control_Edit_PartID_TableLayout_Inputs.AutoSize = true;
            Control_Edit_PartID_TableLayout_Inputs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            Control_Edit_PartID_TableLayout_Inputs.ColumnCount = 2;
            Control_Edit_PartID_TableLayout_Inputs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            Control_Edit_PartID_TableLayout_Inputs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            
            // Row 0: Part ID Suggestion
            Control_Edit_PartID_TableLayout_Inputs.Controls.Add(Control_Edit_PartID_SuggestionBox_Part, 0, 0);
            Control_Edit_PartID_TableLayout_Inputs.SetColumnSpan(Control_Edit_PartID_SuggestionBox_Part, 2);
            
            // Row 1: New Name (Label + TextBox)
            Control_Edit_PartID_TableLayout_Inputs.Controls.Add(label1, 0, 1);
            Control_Edit_PartID_TableLayout_Inputs.Controls.Add(itemNumberTextBox, 1, 1);
            
            // Row 2: Item Type Suggestion
            Control_Edit_PartID_TableLayout_Inputs.Controls.Add(Control_Edit_PartID_SuggestionBox_ItemType, 0, 2);
            Control_Edit_PartID_TableLayout_Inputs.SetColumnSpan(Control_Edit_PartID_SuggestionBox_ItemType, 2);
            
            // Row 3: Issued By
            Control_Edit_PartID_TableLayout_Inputs.Controls.Add(issuedByLabel, 0, 3);
            Control_Edit_PartID_TableLayout_Inputs.Controls.Add(issuedByValueLabel, 1, 3);
            
            // Row 4: CheckBox
            Control_Edit_PartID_TableLayout_Inputs.Controls.Add(Control_Edit_PartID_CheckBox_RequiresColorCode, 0, 4);
            Control_Edit_PartID_TableLayout_Inputs.SetColumnSpan(Control_Edit_PartID_CheckBox_RequiresColorCode, 2);

            Control_Edit_PartID_TableLayout_Inputs.Dock = System.Windows.Forms.DockStyle.Top;
            Control_Edit_PartID_TableLayout_Inputs.Location = new System.Drawing.Point(0, 0);
            Control_Edit_PartID_TableLayout_Inputs.Name = "Control_Edit_PartID_TableLayout_Inputs";
            Control_Edit_PartID_TableLayout_Inputs.RowCount = 5;
            Control_Edit_PartID_TableLayout_Inputs.RowStyles.Add(new System.Windows.Forms.RowStyle());
            Control_Edit_PartID_TableLayout_Inputs.RowStyles.Add(new System.Windows.Forms.RowStyle());
            Control_Edit_PartID_TableLayout_Inputs.RowStyles.Add(new System.Windows.Forms.RowStyle());
            Control_Edit_PartID_TableLayout_Inputs.RowStyles.Add(new System.Windows.Forms.RowStyle());
            Control_Edit_PartID_TableLayout_Inputs.RowStyles.Add(new System.Windows.Forms.RowStyle());
            Control_Edit_PartID_TableLayout_Inputs.Size = new System.Drawing.Size(433, 150);
            Control_Edit_PartID_TableLayout_Inputs.TabIndex = 0;

            // 
            // Control_Edit_PartID_SuggestionBox_Part
            // 
            Control_Edit_PartID_SuggestionBox_Part.Dock = System.Windows.Forms.DockStyle.Fill;
            Control_Edit_PartID_SuggestionBox_Part.EnableSuggestions = true;
            Control_Edit_PartID_SuggestionBox_Part.LabelText = "Select Part";
            Control_Edit_PartID_SuggestionBox_Part.Location = new System.Drawing.Point(3, 3);
            Control_Edit_PartID_SuggestionBox_Part.Name = "Control_Edit_PartID_SuggestionBox_Part";
            Control_Edit_PartID_SuggestionBox_Part.PlaceholderText = "Enter or Select Part Number (F4 to browse)";
            Control_Edit_PartID_SuggestionBox_Part.ShowF4Button = true;
            Control_Edit_PartID_SuggestionBox_Part.ShowValidationColor = true;
            Control_Edit_PartID_SuggestionBox_Part.Size = new System.Drawing.Size(427, 23);
            Control_Edit_PartID_SuggestionBox_Part.TabIndex = 0;
            Control_Edit_PartID_SuggestionBox_Part.ValidatorType = "";

            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = System.Windows.Forms.DockStyle.Fill;
            label1.Location = new System.Drawing.Point(3, 32);
            label1.Margin = new System.Windows.Forms.Padding(3);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(69, 23);
            label1.TabIndex = 1;
            label1.Text = "New Name:";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // 
            // itemNumberTextBox
            // 
            itemNumberTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            itemNumberTextBox.Location = new System.Drawing.Point(78, 32);
            itemNumberTextBox.Name = "itemNumberTextBox";
            itemNumberTextBox.Size = new System.Drawing.Size(352, 23);
            itemNumberTextBox.TabIndex = 2;

            // 
            // Control_Edit_PartID_SuggestionBox_ItemType
            // 
            Control_Edit_PartID_SuggestionBox_ItemType.Dock = System.Windows.Forms.DockStyle.Fill;
            Control_Edit_PartID_SuggestionBox_ItemType.Enabled = false;
            Control_Edit_PartID_SuggestionBox_ItemType.EnableSuggestions = true;
            Control_Edit_PartID_SuggestionBox_ItemType.LabelText = "Type";
            Control_Edit_PartID_SuggestionBox_ItemType.Location = new System.Drawing.Point(3, 61);
            Control_Edit_PartID_SuggestionBox_ItemType.Name = "Control_Edit_PartID_SuggestionBox_ItemType";
            Control_Edit_PartID_SuggestionBox_ItemType.PlaceholderText = "Enter or Select Item Type (F4 to browse)";
            Control_Edit_PartID_SuggestionBox_ItemType.ShowF4Button = true;
            Control_Edit_PartID_SuggestionBox_ItemType.ShowValidationColor = true;
            Control_Edit_PartID_SuggestionBox_ItemType.Size = new System.Drawing.Size(427, 23);
            Control_Edit_PartID_SuggestionBox_ItemType.TabIndex = 3;
            Control_Edit_PartID_SuggestionBox_ItemType.ValidatorType = "";

            // 
            // issuedByLabel
            // 
            issuedByLabel.AutoSize = true;
            issuedByLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            issuedByLabel.Location = new System.Drawing.Point(3, 90);
            issuedByLabel.Margin = new System.Windows.Forms.Padding(3);
            issuedByLabel.Name = "issuedByLabel";
            issuedByLabel.Size = new System.Drawing.Size(69, 15);
            issuedByLabel.TabIndex = 4;
            issuedByLabel.Text = "Issued By:";
            issuedByLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // 
            // issuedByValueLabel
            // 
            issuedByValueLabel.AutoSize = true;
            issuedByValueLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            issuedByValueLabel.Location = new System.Drawing.Point(78, 90);
            issuedByValueLabel.Margin = new System.Windows.Forms.Padding(3);
            issuedByValueLabel.Name = "issuedByValueLabel";
            issuedByValueLabel.Size = new System.Drawing.Size(352, 15);
            issuedByValueLabel.TabIndex = 5;
            issuedByValueLabel.Text = "User";
            issuedByValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // 
            // Control_Edit_PartID_CheckBox_RequiresColorCode
            // 
            Control_Edit_PartID_CheckBox_RequiresColorCode.AutoSize = true;
            Control_Edit_PartID_CheckBox_RequiresColorCode.Dock = System.Windows.Forms.DockStyle.Fill;
            Control_Edit_PartID_CheckBox_RequiresColorCode.Location = new System.Drawing.Point(3, 111);
            Control_Edit_PartID_CheckBox_RequiresColorCode.Name = "Control_Edit_PartID_CheckBox_RequiresColorCode";
            Control_Edit_PartID_CheckBox_RequiresColorCode.Size = new System.Drawing.Size(427, 19);
            Control_Edit_PartID_CheckBox_RequiresColorCode.TabIndex = 6;
            Control_Edit_PartID_CheckBox_RequiresColorCode.Text = "Requires Color Code && Work Order";
            Control_Edit_PartID_CheckBox_RequiresColorCode.UseVisualStyleBackColor = true;

            // 
            // Control_Edit_PartID_FlowPanel_Actions
            // 
            Control_Edit_PartID_FlowPanel_Actions.AutoSize = true;
            Control_Edit_PartID_FlowPanel_Actions.Controls.Add(resetButton);
            Control_Edit_PartID_FlowPanel_Actions.Controls.Add(saveButton);
            Control_Edit_PartID_FlowPanel_Actions.Dock = System.Windows.Forms.DockStyle.Fill;
            Control_Edit_PartID_FlowPanel_Actions.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            Control_Edit_PartID_FlowPanel_Actions.Location = new System.Drawing.Point(23, 327);
            Control_Edit_PartID_FlowPanel_Actions.Name = "Control_Edit_PartID_FlowPanel_Actions";
            Control_Edit_PartID_FlowPanel_Actions.Size = new System.Drawing.Size(433, 31);
            Control_Edit_PartID_FlowPanel_Actions.TabIndex = 2;

            // 
            // saveButton
            // 
            saveButton.Location = new System.Drawing.Point(364, 3);
            saveButton.Name = "saveButton";
            saveButton.Size = new System.Drawing.Size(66, 25);
            saveButton.TabIndex = 0;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;

            // 
            // resetButton
            // 
            resetButton.Location = new System.Drawing.Point(254, 3);
            resetButton.Name = "resetButton";
            resetButton.Size = new System.Drawing.Size(104, 25);
            resetButton.TabIndex = 1;
            resetButton.Text = "Reset";
            resetButton.UseVisualStyleBackColor = true;

            // 
            // Control_Edit_PartID
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            Controls.Add(Control_Edit_PartID_TableLayout_Main);
            Name = "Control_Edit_PartID";
            Size = new System.Drawing.Size(479, 371);
            Control_Edit_PartID_TableLayout_Main.ResumeLayout(false);
            Control_Edit_PartID_TableLayout_Main.PerformLayout();
            Control_Edit_PartID_Panel_Content.ResumeLayout(false);
            Control_Edit_PartID_Panel_Content.PerformLayout();
            Control_Edit_PartID_TableLayout_Inputs.ResumeLayout(false);
            Control_Edit_PartID_TableLayout_Inputs.PerformLayout();
            Control_Edit_PartID_FlowPanel_Actions.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
