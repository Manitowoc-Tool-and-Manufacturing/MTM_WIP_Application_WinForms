namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    partial class Control_Edit_PartID
    {
        #region Fields
        


        private System.ComponentModel.IContainer components = null;

        #endregion
        private System.Windows.Forms.Label selectPartLabel;
        private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBox Control_Edit_PartID_TextBox_Part;
        private System.Windows.Forms.Label typeLabel;
        private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBox Control_Edit_PartID_TextBox_ItemType;
        private System.Windows.Forms.Label issuedByLabel;
        private System.Windows.Forms.Label issuedByValueLabel;




        #region Component Designer generated code

        private void InitializeComponent()
        {
            selectPartLabel = new Label();
            Control_Edit_PartID_TextBox_Part = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBox();
            typeLabel = new Label();
            Control_Edit_PartID_TextBox_ItemType = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBox();
            issuedByLabel = new Label();
            issuedByValueLabel = new Label();
            Control_Add_PartID_GroupBox_NewPartID = new GroupBox();
            Control_Add_PartID_TableLayout_NewPartIDEntry = new TableLayoutPanel();
            Control_Add_PartID_TableLayout_Buttons = new TableLayoutPanel();
            saveButton = new Button();
            resetButton = new Button();
            Control_Add_PartID_TableLayout_Inputs = new TableLayoutPanel();
            Control_Edit_PartID_CheckBox_RequiresColorCode = new CheckBox();
            itemNumberTextBox = new TextBox();
            label1 = new Label();
            Control_Add_PartID_GroupBox_NewPartID.SuspendLayout();
            Control_Add_PartID_TableLayout_NewPartIDEntry.SuspendLayout();
            Control_Add_PartID_TableLayout_Buttons.SuspendLayout();
            Control_Add_PartID_TableLayout_Inputs.SuspendLayout();
            SuspendLayout();
            // 
            // selectPartLabel
            // 
            selectPartLabel.AutoSize = true;
            selectPartLabel.Dock = DockStyle.Fill;
            selectPartLabel.Location = new Point(3, 3);
            selectPartLabel.Margin = new Padding(3);
            selectPartLabel.Name = "selectPartLabel";
            selectPartLabel.Size = new Size(69, 23);
            selectPartLabel.TabIndex = 1;
            selectPartLabel.Text = "Select Part:";
            selectPartLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Control_Edit_PartID_TextBox_Part
            // 
            Control_Edit_PartID_TextBox_Part.Dock = DockStyle.Fill;
            Control_Edit_PartID_TextBox_Part.Location = new Point(78, 3);
            Control_Edit_PartID_TextBox_Part.Name = "Control_Edit_PartID_TextBox_Part";
            Control_Edit_PartID_TextBox_Part.PlaceholderText = "Enter or Select Part Number (F4 to browse)";
            Control_Edit_PartID_TextBox_Part.Size = new Size(300, 23);
            Control_Edit_PartID_TextBox_Part.TabIndex = 2;
            // 
            // typeLabel
            // 
            typeLabel.AutoSize = true;
            typeLabel.Dock = DockStyle.Fill;
            typeLabel.Location = new Point(3, 61);
            typeLabel.Margin = new Padding(3);
            typeLabel.Name = "typeLabel";
            typeLabel.Size = new Size(69, 23);
            typeLabel.TabIndex = 9;
            typeLabel.Text = "Type:";
            typeLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Control_Edit_PartID_TextBox_ItemType
            // 
            Control_Edit_PartID_TextBox_ItemType.Dock = DockStyle.Fill;
            Control_Edit_PartID_TextBox_ItemType.Enabled = false;
            Control_Edit_PartID_TextBox_ItemType.Location = new Point(78, 61);
            Control_Edit_PartID_TextBox_ItemType.Name = "Control_Edit_PartID_TextBox_ItemType";
            Control_Edit_PartID_TextBox_ItemType.PlaceholderText = "Enter or Select Item Type (F4 to browse)";
            Control_Edit_PartID_TextBox_ItemType.Size = new Size(300, 23);
            Control_Edit_PartID_TextBox_ItemType.TabIndex = 10;
            // 
            // issuedByLabel
            // 
            issuedByLabel.AutoSize = true;
            issuedByLabel.Dock = DockStyle.Fill;
            issuedByLabel.Location = new Point(3, 90);
            issuedByLabel.Margin = new Padding(3);
            issuedByLabel.Name = "issuedByLabel";
            issuedByLabel.Size = new Size(69, 15);
            issuedByLabel.TabIndex = 11;
            issuedByLabel.Text = "Issued By:";
            issuedByLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // issuedByValueLabel
            // 
            issuedByValueLabel.AutoSize = true;
            issuedByValueLabel.Dock = DockStyle.Fill;
            issuedByValueLabel.Location = new Point(78, 90);
            issuedByValueLabel.Margin = new Padding(3);
            issuedByValueLabel.Name = "issuedByValueLabel";
            issuedByValueLabel.Size = new Size(300, 15);
            issuedByValueLabel.TabIndex = 12;
            issuedByValueLabel.Text = "User";
            // 
            // Control_Add_PartID_GroupBox_NewPartID
            // 
            Control_Add_PartID_GroupBox_NewPartID.AutoSize = true;
            Control_Add_PartID_GroupBox_NewPartID.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_Add_PartID_GroupBox_NewPartID.Controls.Add(Control_Add_PartID_TableLayout_NewPartIDEntry);
            Control_Add_PartID_GroupBox_NewPartID.Dock = DockStyle.Fill;
            Control_Add_PartID_GroupBox_NewPartID.Location = new Point(0, 0);
            Control_Add_PartID_GroupBox_NewPartID.Name = "Control_Add_PartID_GroupBox_NewPartID";
            Control_Add_PartID_GroupBox_NewPartID.Size = new Size(393, 198);
            Control_Add_PartID_GroupBox_NewPartID.TabIndex = 16;
            Control_Add_PartID_GroupBox_NewPartID.TabStop = false;
            Control_Add_PartID_GroupBox_NewPartID.Text = "New Part ID Entry";
            // 
            // Control_Add_PartID_TableLayout_NewPartIDEntry
            // 
            Control_Add_PartID_TableLayout_NewPartIDEntry.AutoSize = true;
            Control_Add_PartID_TableLayout_NewPartIDEntry.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_Add_PartID_TableLayout_NewPartIDEntry.ColumnCount = 1;
            Control_Add_PartID_TableLayout_NewPartIDEntry.ColumnStyles.Add(new ColumnStyle());
            Control_Add_PartID_TableLayout_NewPartIDEntry.Controls.Add(Control_Add_PartID_TableLayout_Buttons, 0, 1);
            Control_Add_PartID_TableLayout_NewPartIDEntry.Controls.Add(Control_Add_PartID_TableLayout_Inputs, 0, 0);
            Control_Add_PartID_TableLayout_NewPartIDEntry.Dock = DockStyle.Fill;
            Control_Add_PartID_TableLayout_NewPartIDEntry.Location = new Point(3, 19);
            Control_Add_PartID_TableLayout_NewPartIDEntry.Name = "Control_Add_PartID_TableLayout_NewPartIDEntry";
            Control_Add_PartID_TableLayout_NewPartIDEntry.RowCount = 2;
            Control_Add_PartID_TableLayout_NewPartIDEntry.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_Add_PartID_TableLayout_NewPartIDEntry.RowStyles.Add(new RowStyle());
            Control_Add_PartID_TableLayout_NewPartIDEntry.Size = new Size(387, 176);
            Control_Add_PartID_TableLayout_NewPartIDEntry.TabIndex = 0;
            // 
            // Control_Add_PartID_TableLayout_Buttons
            // 
            Control_Add_PartID_TableLayout_Buttons.AutoSize = true;
            Control_Add_PartID_TableLayout_Buttons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_Add_PartID_TableLayout_Buttons.ColumnCount = 4;
            Control_Add_PartID_TableLayout_Buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 95F));
            Control_Add_PartID_TableLayout_Buttons.ColumnStyles.Add(new ColumnStyle());
            Control_Add_PartID_TableLayout_Buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            Control_Add_PartID_TableLayout_Buttons.ColumnStyles.Add(new ColumnStyle());
            Control_Add_PartID_TableLayout_Buttons.Controls.Add(saveButton, 1, 0);
            Control_Add_PartID_TableLayout_Buttons.Controls.Add(resetButton, 3, 0);
            Control_Add_PartID_TableLayout_Buttons.Dock = DockStyle.Fill;
            Control_Add_PartID_TableLayout_Buttons.Location = new Point(3, 142);
            Control_Add_PartID_TableLayout_Buttons.Name = "Control_Add_PartID_TableLayout_Buttons";
            Control_Add_PartID_TableLayout_Buttons.RowCount = 1;
            Control_Add_PartID_TableLayout_Buttons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_Add_PartID_TableLayout_Buttons.Size = new Size(381, 31);
            Control_Add_PartID_TableLayout_Buttons.TabIndex = 15;
            // 
            // saveButton
            // 
            saveButton.Location = new Point(192, 3);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(66, 25);
            saveButton.TabIndex = 11;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            // 
            // resetButton
            // 
            resetButton.Location = new Point(273, 3);
            resetButton.Name = "resetButton";
            resetButton.Size = new Size(104, 25);
            resetButton.TabIndex = 12;
            resetButton.Text = "Reset";
            resetButton.UseVisualStyleBackColor = true;
            // 
            // Control_Add_PartID_TableLayout_Inputs
            // 
            Control_Add_PartID_TableLayout_Inputs.AutoSize = true;
            Control_Add_PartID_TableLayout_Inputs.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_Add_PartID_TableLayout_Inputs.ColumnCount = 2;
            Control_Add_PartID_TableLayout_Inputs.ColumnStyles.Add(new ColumnStyle());
            Control_Add_PartID_TableLayout_Inputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_Add_PartID_TableLayout_Inputs.Controls.Add(label1, 0, 1);
            Control_Add_PartID_TableLayout_Inputs.Controls.Add(Control_Edit_PartID_TextBox_ItemType, 1, 2);
            Control_Add_PartID_TableLayout_Inputs.Controls.Add(issuedByValueLabel, 1, 3);
            Control_Add_PartID_TableLayout_Inputs.Controls.Add(typeLabel, 0, 2);
            Control_Add_PartID_TableLayout_Inputs.Controls.Add(issuedByLabel, 0, 3);
            Control_Add_PartID_TableLayout_Inputs.Controls.Add(Control_Edit_PartID_CheckBox_RequiresColorCode, 0, 4);
            Control_Add_PartID_TableLayout_Inputs.Controls.Add(Control_Edit_PartID_TextBox_Part, 1, 0);
            Control_Add_PartID_TableLayout_Inputs.Controls.Add(selectPartLabel, 0, 0);
            Control_Add_PartID_TableLayout_Inputs.Controls.Add(itemNumberTextBox, 1, 1);
            Control_Add_PartID_TableLayout_Inputs.Dock = DockStyle.Fill;
            Control_Add_PartID_TableLayout_Inputs.Location = new Point(3, 3);
            Control_Add_PartID_TableLayout_Inputs.Name = "Control_Add_PartID_TableLayout_Inputs";
            Control_Add_PartID_TableLayout_Inputs.RowCount = 6;
            Control_Add_PartID_TableLayout_Inputs.RowStyles.Add(new RowStyle());
            Control_Add_PartID_TableLayout_Inputs.RowStyles.Add(new RowStyle());
            Control_Add_PartID_TableLayout_Inputs.RowStyles.Add(new RowStyle());
            Control_Add_PartID_TableLayout_Inputs.RowStyles.Add(new RowStyle());
            Control_Add_PartID_TableLayout_Inputs.RowStyles.Add(new RowStyle());
            Control_Add_PartID_TableLayout_Inputs.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_Add_PartID_TableLayout_Inputs.Size = new Size(381, 133);
            Control_Add_PartID_TableLayout_Inputs.TabIndex = 15;
            // 
            // Control_Edit_PartID_CheckBox_RequiresColorCode
            // 
            Control_Edit_PartID_CheckBox_RequiresColorCode.AutoSize = true;
            Control_Add_PartID_TableLayout_Inputs.SetColumnSpan(Control_Edit_PartID_CheckBox_RequiresColorCode, 2);
            Control_Edit_PartID_CheckBox_RequiresColorCode.Dock = DockStyle.Right;
            Control_Edit_PartID_CheckBox_RequiresColorCode.Location = new Point(167, 111);
            Control_Edit_PartID_CheckBox_RequiresColorCode.Name = "Control_Edit_PartID_CheckBox_RequiresColorCode";
            Control_Edit_PartID_CheckBox_RequiresColorCode.Size = new Size(211, 19);
            Control_Edit_PartID_CheckBox_RequiresColorCode.TabIndex = 13;
            Control_Edit_PartID_CheckBox_RequiresColorCode.Text = "Requires Color Code && Work Order";
            Control_Edit_PartID_CheckBox_RequiresColorCode.UseVisualStyleBackColor = true;
            // 
            // itemNumberTextBox
            // 
            itemNumberTextBox.Dock = DockStyle.Fill;
            itemNumberTextBox.Location = new Point(78, 32);
            itemNumberTextBox.Name = "itemNumberTextBox";
            itemNumberTextBox.Size = new Size(300, 23);
            itemNumberTextBox.TabIndex = 14;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Location = new Point(3, 32);
            label1.Margin = new Padding(3);
            label1.Name = "label1";
            label1.Size = new Size(69, 23);
            label1.TabIndex = 15;
            label1.Text = "New Name:";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Control_Edit_PartID
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(Control_Add_PartID_GroupBox_NewPartID);
            Name = "Control_Edit_PartID";
            Size = new Size(393, 198);
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

        private GroupBox Control_Add_PartID_GroupBox_NewPartID;
        private TableLayoutPanel Control_Add_PartID_TableLayout_NewPartIDEntry;
        private TableLayoutPanel Control_Add_PartID_TableLayout_Buttons;
        private Button saveButton;
        private Button resetButton;
        private TableLayoutPanel Control_Add_PartID_TableLayout_Inputs;
        private CheckBox Control_Edit_PartID_CheckBox_RequiresColorCode;
        private Label label1;
        private TextBox itemNumberTextBox;
    }

        
        #endregion
    }
