namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    partial class Control_Edit_PartID
    {        
        private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel Control_Edit_PartID_SuggestionBox_Part;
        private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel Control_Edit_PartID_SuggestionBox_ItemType;
        private System.Windows.Forms.Label issuedByLabel;
        private System.Windows.Forms.Label issuedByValueLabel;
        private System.Windows.Forms.GroupBox Control_Add_PartID_GroupBox_NewPartID;
        private System.Windows.Forms.TableLayoutPanel Control_Add_PartID_TableLayout_NewPartIDEntry;
        private System.Windows.Forms.TableLayoutPanel Control_Add_PartID_TableLayout_Buttons;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.TableLayoutPanel Control_Add_PartID_TableLayout_Inputs;
        private System.Windows.Forms.CheckBox Control_Edit_PartID_CheckBox_RequiresColorCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox itemNumberTextBox;

        #region Component Designer generated code

        private void InitializeComponent()
        {
            Control_Edit_PartID_SuggestionBox_Part = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            Control_Edit_PartID_SuggestionBox_ItemType = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            issuedByLabel = new System.Windows.Forms.Label();
            issuedByValueLabel = new System.Windows.Forms.Label();
            Control_Add_PartID_GroupBox_NewPartID = new System.Windows.Forms.GroupBox();
            Control_Add_PartID_TableLayout_NewPartIDEntry = new System.Windows.Forms.TableLayoutPanel();
            Control_Add_PartID_TableLayout_Buttons = new System.Windows.Forms.TableLayoutPanel();
            saveButton = new System.Windows.Forms.Button();
            resetButton = new System.Windows.Forms.Button();
            Control_Add_PartID_TableLayout_Inputs = new System.Windows.Forms.TableLayoutPanel();
            Control_Edit_PartID_CheckBox_RequiresColorCode = new System.Windows.Forms.CheckBox();
            itemNumberTextBox = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            Control_Add_PartID_GroupBox_NewPartID.SuspendLayout();
            Control_Add_PartID_TableLayout_NewPartIDEntry.SuspendLayout();
            Control_Add_PartID_TableLayout_Buttons.SuspendLayout();
            Control_Add_PartID_TableLayout_Inputs.SuspendLayout();
            SuspendLayout();
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
            Control_Edit_PartID_SuggestionBox_Part.Size = new System.Drawing.Size(375, 23);
            Control_Edit_PartID_SuggestionBox_Part.TabIndex = 0;
            Control_Edit_PartID_SuggestionBox_Part.ValidatorType = "";
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
            Control_Edit_PartID_SuggestionBox_ItemType.Size = new System.Drawing.Size(375, 23);
            Control_Edit_PartID_SuggestionBox_ItemType.TabIndex = 2;
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
            issuedByLabel.TabIndex = 11;
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
            issuedByValueLabel.Size = new System.Drawing.Size(300, 15);
            issuedByValueLabel.TabIndex = 12;
            issuedByValueLabel.Text = "User";
            // 
            // Control_Add_PartID_GroupBox_NewPartID
            // 
            Control_Add_PartID_GroupBox_NewPartID.AutoSize = true;
            Control_Add_PartID_GroupBox_NewPartID.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            Control_Add_PartID_GroupBox_NewPartID.Controls.Add(Control_Add_PartID_TableLayout_NewPartIDEntry);
            Control_Add_PartID_GroupBox_NewPartID.Dock = System.Windows.Forms.DockStyle.Fill;
            Control_Add_PartID_GroupBox_NewPartID.Location = new System.Drawing.Point(0, 0);
            Control_Add_PartID_GroupBox_NewPartID.Name = "Control_Add_PartID_GroupBox_NewPartID";
            Control_Add_PartID_GroupBox_NewPartID.Size = new System.Drawing.Size(393, 198);
            Control_Add_PartID_GroupBox_NewPartID.TabIndex = 16;
            Control_Add_PartID_GroupBox_NewPartID.TabStop = false;
            Control_Add_PartID_GroupBox_NewPartID.Text = "Edit Part ID";
            // 
            // Control_Add_PartID_TableLayout_NewPartIDEntry
            // 
            Control_Add_PartID_TableLayout_NewPartIDEntry.AutoSize = true;
            Control_Add_PartID_TableLayout_NewPartIDEntry.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            Control_Add_PartID_TableLayout_NewPartIDEntry.ColumnCount = 1;
            Control_Add_PartID_TableLayout_NewPartIDEntry.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            Control_Add_PartID_TableLayout_NewPartIDEntry.Controls.Add(Control_Add_PartID_TableLayout_Buttons, 0, 1);
            Control_Add_PartID_TableLayout_NewPartIDEntry.Controls.Add(Control_Add_PartID_TableLayout_Inputs, 0, 0);
            Control_Add_PartID_TableLayout_NewPartIDEntry.Dock = System.Windows.Forms.DockStyle.Fill;
            Control_Add_PartID_TableLayout_NewPartIDEntry.Location = new System.Drawing.Point(3, 19);
            Control_Add_PartID_TableLayout_NewPartIDEntry.Name = "Control_Add_PartID_TableLayout_NewPartIDEntry";
            Control_Add_PartID_TableLayout_NewPartIDEntry.RowCount = 2;
            Control_Add_PartID_TableLayout_NewPartIDEntry.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            Control_Add_PartID_TableLayout_NewPartIDEntry.RowStyles.Add(new System.Windows.Forms.RowStyle());
            Control_Add_PartID_TableLayout_NewPartIDEntry.Size = new System.Drawing.Size(387, 176);
            Control_Add_PartID_TableLayout_NewPartIDEntry.TabIndex = 0;
            // 
            // Control_Add_PartID_TableLayout_Buttons
            // 
            Control_Add_PartID_TableLayout_Buttons.AutoSize = true;
            Control_Add_PartID_TableLayout_Buttons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            Control_Add_PartID_TableLayout_Buttons.ColumnCount = 4;
            Control_Add_PartID_TableLayout_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 95F));
            Control_Add_PartID_TableLayout_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            Control_Add_PartID_TableLayout_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5F));
            Control_Add_PartID_TableLayout_Buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            Control_Add_PartID_TableLayout_Buttons.Controls.Add(saveButton, 1, 0);
            Control_Add_PartID_TableLayout_Buttons.Controls.Add(resetButton, 3, 0);
            Control_Add_PartID_TableLayout_Buttons.Dock = System.Windows.Forms.DockStyle.Fill;
            Control_Add_PartID_TableLayout_Buttons.Location = new System.Drawing.Point(3, 142);
            Control_Add_PartID_TableLayout_Buttons.Name = "Control_Add_PartID_TableLayout_Buttons";
            Control_Add_PartID_TableLayout_Buttons.RowCount = 1;
            Control_Add_PartID_TableLayout_Buttons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            Control_Add_PartID_TableLayout_Buttons.Size = new System.Drawing.Size(381, 31);
            Control_Add_PartID_TableLayout_Buttons.TabIndex = 15;
            // 
            // saveButton
            // 
            saveButton.Location = new System.Drawing.Point(192, 3);
            saveButton.Name = "saveButton";
            saveButton.Size = new System.Drawing.Size(66, 25);
            saveButton.TabIndex = 11;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            // 
            // resetButton
            // 
            resetButton.Location = new System.Drawing.Point(273, 3);
            resetButton.Name = "resetButton";
            resetButton.Size = new System.Drawing.Size(104, 25);
            resetButton.TabIndex = 12;
            resetButton.Text = "Reset";
            resetButton.UseVisualStyleBackColor = true;
            // 
            // Control_Add_PartID_TableLayout_Inputs
            // 
            Control_Add_PartID_TableLayout_Inputs.AutoSize = true;
            Control_Add_PartID_TableLayout_Inputs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            Control_Add_PartID_TableLayout_Inputs.ColumnCount = 2;
            Control_Add_PartID_TableLayout_Inputs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            Control_Add_PartID_TableLayout_Inputs.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            Control_Add_PartID_TableLayout_Inputs.Controls.Add(label1, 0, 1);
            Control_Add_PartID_TableLayout_Inputs.Controls.Add(Control_Edit_PartID_SuggestionBox_ItemType, 0, 2);
            Control_Add_PartID_TableLayout_Inputs.SetColumnSpan(Control_Edit_PartID_SuggestionBox_ItemType, 2);
            Control_Add_PartID_TableLayout_Inputs.Controls.Add(issuedByValueLabel, 1, 3);
            Control_Add_PartID_TableLayout_Inputs.Controls.Add(issuedByLabel, 0, 3);
            Control_Add_PartID_TableLayout_Inputs.Controls.Add(Control_Edit_PartID_CheckBox_RequiresColorCode, 0, 4);
            Control_Add_PartID_TableLayout_Inputs.Controls.Add(Control_Edit_PartID_SuggestionBox_Part, 0, 0);
            Control_Add_PartID_TableLayout_Inputs.SetColumnSpan(Control_Edit_PartID_SuggestionBox_Part, 2);
            Control_Add_PartID_TableLayout_Inputs.Controls.Add(itemNumberTextBox, 1, 1);
            Control_Add_PartID_TableLayout_Inputs.Dock = System.Windows.Forms.DockStyle.Fill;
            Control_Add_PartID_TableLayout_Inputs.Location = new System.Drawing.Point(3, 3);
            Control_Add_PartID_TableLayout_Inputs.Name = "Control_Add_PartID_TableLayout_Inputs";
            Control_Add_PartID_TableLayout_Inputs.RowCount = 6;
            Control_Add_PartID_TableLayout_Inputs.RowStyles.Add(new System.Windows.Forms.RowStyle());
            Control_Add_PartID_TableLayout_Inputs.RowStyles.Add(new System.Windows.Forms.RowStyle());
            Control_Add_PartID_TableLayout_Inputs.RowStyles.Add(new System.Windows.Forms.RowStyle());
            Control_Add_PartID_TableLayout_Inputs.RowStyles.Add(new System.Windows.Forms.RowStyle());
            Control_Add_PartID_TableLayout_Inputs.RowStyles.Add(new System.Windows.Forms.RowStyle());
            Control_Add_PartID_TableLayout_Inputs.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            Control_Add_PartID_TableLayout_Inputs.Size = new System.Drawing.Size(381, 133);
            Control_Add_PartID_TableLayout_Inputs.TabIndex = 15;
            // 
            // Control_Edit_PartID_CheckBox_RequiresColorCode
            // 
            Control_Edit_PartID_CheckBox_RequiresColorCode.AutoSize = true;
            Control_Add_PartID_TableLayout_Inputs.SetColumnSpan(Control_Edit_PartID_CheckBox_RequiresColorCode, 2);
            Control_Edit_PartID_CheckBox_RequiresColorCode.Dock = System.Windows.Forms.DockStyle.Right;
            Control_Edit_PartID_CheckBox_RequiresColorCode.Location = new System.Drawing.Point(167, 111);
            Control_Edit_PartID_CheckBox_RequiresColorCode.Name = "Control_Edit_PartID_CheckBox_RequiresColorCode";
            Control_Edit_PartID_CheckBox_RequiresColorCode.Size = new System.Drawing.Size(211, 19);
            Control_Edit_PartID_CheckBox_RequiresColorCode.TabIndex = 13;
            Control_Edit_PartID_CheckBox_RequiresColorCode.Text = "Requires Color Code && Work Order";
            Control_Edit_PartID_CheckBox_RequiresColorCode.UseVisualStyleBackColor = true;
            // 
            // itemNumberTextBox
            // 
            itemNumberTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            itemNumberTextBox.Location = new System.Drawing.Point(78, 32);
            itemNumberTextBox.Name = "itemNumberTextBox";
            itemNumberTextBox.Size = new System.Drawing.Size(300, 23);
            itemNumberTextBox.TabIndex = 14;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = System.Windows.Forms.DockStyle.Fill;
            label1.Location = new System.Drawing.Point(3, 32);
            label1.Margin = new System.Windows.Forms.Padding(3);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(69, 23);
            label1.TabIndex = 15;
            label1.Text = "New Name:";
            label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Control_Edit_PartID
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            Controls.Add(Control_Add_PartID_GroupBox_NewPartID);
            Name = "Control_Edit_PartID";
            Size = new System.Drawing.Size(393, 198);
            Control_Add_PartID_GroupBox_NewPartID.ResumeLayout(false);
            Control_Add_PartID_GroupBox_NewPartID.PerformLayout();
            Control_Add_PartID_TableLayout_NewPartIDEntry.ResumeLayout(false);
            Control_Add_PartID_TableLayout_NewPartIDEntry.PerformLayout();
            Control_Add_PartID_TableLayout_Buttons.ResumeLayout(false);
            Control_Add_PartID_TableLayout_Inputs.ResumeLayout(false);
            Control_Add_PartID_TableLayout_Inputs.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
