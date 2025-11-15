namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    partial class Control_Add_PartID
    {
        #region Fields

        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label itemNumberLabel;
        private System.Windows.Forms.TextBox itemNumberTextBox;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.ComboBox Control_Add_PartID_ComboBox_ItemType;
        private System.Windows.Forms.Label issuedByLabel;
        private System.Windows.Forms.Label issuedByValueLabel;
        private System.Windows.Forms.CheckBox Control_Add_PartID_CheckBox_RequiresColorCode;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button cancelButton;

        #endregion

        #region Methods

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Component Designer generated code

        private void InitializeComponent()
        {
            itemNumberLabel = new Label();
            itemNumberTextBox = new TextBox();
            typeLabel = new Label();
            Control_Add_PartID_ComboBox_ItemType = new ComboBox();
            issuedByLabel = new Label();
            issuedByValueLabel = new Label();
            Control_Add_PartID_CheckBox_RequiresColorCode = new CheckBox();
            saveButton = new Button();
            cancelButton = new Button();
            Control_Add_PartID_GroupBox_NewPartID = new GroupBox();
            Control_Add_PartID_TableLayout_NewPartIDEntry = new TableLayoutPanel();
            Control_Add_PartID_TableLayout_Inputs = new TableLayoutPanel();
            Control_Add_PartID_TableLayout_Buttons = new TableLayoutPanel();
            Control_Add_PartID_GroupBox_NewPartID.SuspendLayout();
            Control_Add_PartID_TableLayout_NewPartIDEntry.SuspendLayout();
            Control_Add_PartID_TableLayout_Inputs.SuspendLayout();
            Control_Add_PartID_TableLayout_Buttons.SuspendLayout();
            SuspendLayout();
            // 
            // itemNumberLabel
            // 
            itemNumberLabel.AutoSize = true;
            itemNumberLabel.Dock = DockStyle.Fill;
            itemNumberLabel.Location = new Point(3, 3);
            itemNumberLabel.Margin = new Padding(3);
            itemNumberLabel.Name = "itemNumberLabel";
            itemNumberLabel.Size = new Size(81, 23);
            itemNumberLabel.TabIndex = 1;
            itemNumberLabel.Text = "Item Number:";
            itemNumberLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // itemNumberTextBox
            // 
            itemNumberTextBox.Dock = DockStyle.Fill;
            itemNumberTextBox.Location = new Point(90, 3);
            itemNumberTextBox.Name = "itemNumberTextBox";
            itemNumberTextBox.Size = new Size(343, 23);
            itemNumberTextBox.TabIndex = 2;
            // 
            // typeLabel
            // 
            typeLabel.AutoSize = true;
            typeLabel.Dock = DockStyle.Fill;
            typeLabel.Location = new Point(3, 32);
            typeLabel.Margin = new Padding(3);
            typeLabel.Name = "typeLabel";
            typeLabel.Size = new Size(81, 23);
            typeLabel.TabIndex = 7;
            typeLabel.Text = "Type:";
            typeLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Control_Add_PartID_ComboBox_ItemType
            // 
            Control_Add_PartID_ComboBox_ItemType.Dock = DockStyle.Fill;
            Control_Add_PartID_ComboBox_ItemType.DropDownStyle = ComboBoxStyle.DropDownList;
            Control_Add_PartID_ComboBox_ItemType.FormattingEnabled = true;
            Control_Add_PartID_ComboBox_ItemType.Location = new Point(90, 32);
            Control_Add_PartID_ComboBox_ItemType.Name = "Control_Add_PartID_ComboBox_ItemType";
            Control_Add_PartID_ComboBox_ItemType.Size = new Size(343, 23);
            Control_Add_PartID_ComboBox_ItemType.TabIndex = 8;
            // 
            // issuedByLabel
            // 
            issuedByLabel.AutoSize = true;
            issuedByLabel.Dock = DockStyle.Fill;
            issuedByLabel.Location = new Point(3, 61);
            issuedByLabel.Margin = new Padding(3);
            issuedByLabel.Name = "issuedByLabel";
            issuedByLabel.Size = new Size(81, 15);
            issuedByLabel.TabIndex = 9;
            issuedByLabel.Text = "Issued By:";
            issuedByLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // issuedByValueLabel
            // 
            issuedByValueLabel.AutoSize = true;
            issuedByValueLabel.Dock = DockStyle.Fill;
            issuedByValueLabel.Location = new Point(90, 61);
            issuedByValueLabel.Margin = new Padding(3);
            issuedByValueLabel.Name = "issuedByValueLabel";
            issuedByValueLabel.Size = new Size(343, 15);
            issuedByValueLabel.TabIndex = 10;
            issuedByValueLabel.Text = "Current User";
            issuedByValueLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_Add_PartID_CheckBox_RequiresColorCode
            // 
            Control_Add_PartID_CheckBox_RequiresColorCode.AutoSize = true;
            Control_Add_PartID_TableLayout_Inputs.SetColumnSpan(Control_Add_PartID_CheckBox_RequiresColorCode, 2);
            Control_Add_PartID_CheckBox_RequiresColorCode.Dock = DockStyle.Right;
            Control_Add_PartID_CheckBox_RequiresColorCode.Location = new Point(222, 82);
            Control_Add_PartID_CheckBox_RequiresColorCode.Name = "Control_Add_PartID_CheckBox_RequiresColorCode";
            Control_Add_PartID_CheckBox_RequiresColorCode.Size = new Size(211, 19);
            Control_Add_PartID_CheckBox_RequiresColorCode.TabIndex = 13;
            Control_Add_PartID_CheckBox_RequiresColorCode.Text = "Requires Color Code && Work Order";
            Control_Add_PartID_CheckBox_RequiresColorCode.UseVisualStyleBackColor = true;
            // 
            // saveButton
            // 
            saveButton.Location = new Point(244, 3);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(66, 25);
            saveButton.TabIndex = 11;
            saveButton.Text = "Save";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += SaveButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(328, 3);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(104, 25);
            cancelButton.TabIndex = 12;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += CancelButton_Click;
            // 
            // Control_Add_PartID_GroupBox_NewPartID
            // 
            Control_Add_PartID_GroupBox_NewPartID.AutoSize = true;
            Control_Add_PartID_GroupBox_NewPartID.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_Add_PartID_GroupBox_NewPartID.Controls.Add(Control_Add_PartID_TableLayout_NewPartIDEntry);
            Control_Add_PartID_GroupBox_NewPartID.Dock = DockStyle.Fill;
            Control_Add_PartID_GroupBox_NewPartID.Location = new Point(0, 0);
            Control_Add_PartID_GroupBox_NewPartID.Name = "Control_Add_PartID_GroupBox_NewPartID";
            Control_Add_PartID_GroupBox_NewPartID.Size = new Size(448, 169);
            Control_Add_PartID_GroupBox_NewPartID.TabIndex = 13;
            Control_Add_PartID_GroupBox_NewPartID.TabStop = false;
            Control_Add_PartID_GroupBox_NewPartID.Text = "New Part ID Entry";
            // 
            // Control_Add_PartID_TableLayout_NewPartIDEntry
            // 
            Control_Add_PartID_TableLayout_NewPartIDEntry.AutoSize = true;
            Control_Add_PartID_TableLayout_NewPartIDEntry.ColumnCount = 1;
            Control_Add_PartID_TableLayout_NewPartIDEntry.ColumnStyles.Add(new ColumnStyle());
            Control_Add_PartID_TableLayout_NewPartIDEntry.Controls.Add(Control_Add_PartID_TableLayout_Buttons, 0, 1);
            Control_Add_PartID_TableLayout_NewPartIDEntry.Controls.Add(Control_Add_PartID_TableLayout_Inputs, 0, 0);
            Control_Add_PartID_TableLayout_NewPartIDEntry.Dock = DockStyle.Fill;
            Control_Add_PartID_TableLayout_NewPartIDEntry.Location = new Point(3, 19);
            Control_Add_PartID_TableLayout_NewPartIDEntry.Name = "Control_Add_PartID_TableLayout_NewPartIDEntry";
            Control_Add_PartID_TableLayout_NewPartIDEntry.RowCount = 2;
            Control_Add_PartID_TableLayout_NewPartIDEntry.RowStyles.Add(new RowStyle());
            Control_Add_PartID_TableLayout_NewPartIDEntry.RowStyles.Add(new RowStyle());
            Control_Add_PartID_TableLayout_NewPartIDEntry.Size = new Size(442, 147);
            Control_Add_PartID_TableLayout_NewPartIDEntry.TabIndex = 0;
            // 
            // Control_Add_PartID_TableLayout_Inputs
            // 
            Control_Add_PartID_TableLayout_Inputs.AutoSize = true;
            Control_Add_PartID_TableLayout_Inputs.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_Add_PartID_TableLayout_Inputs.ColumnCount = 2;
            Control_Add_PartID_TableLayout_Inputs.ColumnStyles.Add(new ColumnStyle());
            Control_Add_PartID_TableLayout_Inputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_Add_PartID_TableLayout_Inputs.Controls.Add(itemNumberLabel, 0, 0);
            Control_Add_PartID_TableLayout_Inputs.Controls.Add(typeLabel, 0, 1);
            Control_Add_PartID_TableLayout_Inputs.Controls.Add(issuedByLabel, 0, 2);
            Control_Add_PartID_TableLayout_Inputs.Controls.Add(issuedByValueLabel, 1, 2);
            Control_Add_PartID_TableLayout_Inputs.Controls.Add(Control_Add_PartID_ComboBox_ItemType, 1, 1);
            Control_Add_PartID_TableLayout_Inputs.Controls.Add(Control_Add_PartID_CheckBox_RequiresColorCode, 0, 3);
            Control_Add_PartID_TableLayout_Inputs.Controls.Add(itemNumberTextBox, 1, 0);
            Control_Add_PartID_TableLayout_Inputs.Dock = DockStyle.Fill;
            Control_Add_PartID_TableLayout_Inputs.Location = new Point(3, 3);
            Control_Add_PartID_TableLayout_Inputs.Name = "Control_Add_PartID_TableLayout_Inputs";
            Control_Add_PartID_TableLayout_Inputs.RowCount = 5;
            Control_Add_PartID_TableLayout_Inputs.RowStyles.Add(new RowStyle());
            Control_Add_PartID_TableLayout_Inputs.RowStyles.Add(new RowStyle());
            Control_Add_PartID_TableLayout_Inputs.RowStyles.Add(new RowStyle());
            Control_Add_PartID_TableLayout_Inputs.RowStyles.Add(new RowStyle());
            Control_Add_PartID_TableLayout_Inputs.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_Add_PartID_TableLayout_Inputs.Size = new Size(436, 104);
            Control_Add_PartID_TableLayout_Inputs.TabIndex = 15;
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
            Control_Add_PartID_TableLayout_Buttons.Controls.Add(cancelButton, 3, 0);
            Control_Add_PartID_TableLayout_Buttons.Dock = DockStyle.Fill;
            Control_Add_PartID_TableLayout_Buttons.Location = new Point(3, 113);
            Control_Add_PartID_TableLayout_Buttons.Name = "Control_Add_PartID_TableLayout_Buttons";
            Control_Add_PartID_TableLayout_Buttons.RowCount = 1;
            Control_Add_PartID_TableLayout_Buttons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_Add_PartID_TableLayout_Buttons.Size = new Size(436, 31);
            Control_Add_PartID_TableLayout_Buttons.TabIndex = 15;
            // 
            // Control_Add_PartID
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(Control_Add_PartID_GroupBox_NewPartID);
            Name = "Control_Add_PartID";
            Size = new Size(448, 169);
            Control_Add_PartID_GroupBox_NewPartID.ResumeLayout(false);
            Control_Add_PartID_GroupBox_NewPartID.PerformLayout();
            Control_Add_PartID_TableLayout_NewPartIDEntry.ResumeLayout(false);
            Control_Add_PartID_TableLayout_NewPartIDEntry.PerformLayout();
            Control_Add_PartID_TableLayout_Inputs.ResumeLayout(false);
            Control_Add_PartID_TableLayout_Inputs.PerformLayout();
            Control_Add_PartID_TableLayout_Buttons.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox Control_Add_PartID_GroupBox_NewPartID;
        private TableLayoutPanel Control_Add_PartID_TableLayout_NewPartIDEntry;
        private TableLayoutPanel Control_Add_PartID_TableLayout_Inputs;
        private TableLayoutPanel Control_Add_PartID_TableLayout_Buttons;
    }
}
