namespace MTM_WIP_Application_Winforms.Forms.Shared
{
    partial class Form_QuickButtonEdit
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_QuickButtonEdit));
            Form_QuickButtonEdit_Label_PartId = new Label();
            Form_QuickButtonEdit_Label_Operation = new Label();
            Form_QuickButtonEdit_Label_Quantity = new Label();
            Form_QuickButtonEdit_ComboBox_PartId = new ComboBox();
            Form_QuickButtonEdit_ComboBox_Operation = new ComboBox();
            Form_QuickButtonEdit_NumericUpDown_Quantity = new NumericUpDown();
            Form_QuickButtonEdit_Label_Status = new Label();
            Form_QuickButtonEdit_Button_OK = new Button();
            Form_QuickButtonEdit_Button_Cancel = new Button();
            ((System.ComponentModel.ISupportInitialize)Form_QuickButtonEdit_NumericUpDown_Quantity).BeginInit();
            SuspendLayout();
            // 
            // Form_QuickButtonEdit_Label_PartId
            // 
            Form_QuickButtonEdit_Label_PartId.AutoSize = true;
            Form_QuickButtonEdit_Label_PartId.Location = new Point(10, 15);
            Form_QuickButtonEdit_Label_PartId.Name = "Form_QuickButtonEdit_Label_PartId";
            Form_QuickButtonEdit_Label_PartId.Size = new Size(45, 15);
            Form_QuickButtonEdit_Label_PartId.TabIndex = 0;
            Form_QuickButtonEdit_Label_PartId.Text = "Part ID:";
            // 
            // Form_QuickButtonEdit_Label_Operation
            // 
            Form_QuickButtonEdit_Label_Operation.AutoSize = true;
            Form_QuickButtonEdit_Label_Operation.Location = new Point(10, 55);
            Form_QuickButtonEdit_Label_Operation.Name = "Form_QuickButtonEdit_Label_Operation";
            Form_QuickButtonEdit_Label_Operation.Size = new Size(63, 15);
            Form_QuickButtonEdit_Label_Operation.TabIndex = 2;
            Form_QuickButtonEdit_Label_Operation.Text = "Operation:";
            // 
            // Form_QuickButtonEdit_Label_Quantity
            // 
            Form_QuickButtonEdit_Label_Quantity.AutoSize = true;
            Form_QuickButtonEdit_Label_Quantity.Location = new Point(10, 95);
            Form_QuickButtonEdit_Label_Quantity.Name = "Form_QuickButtonEdit_Label_Quantity";
            Form_QuickButtonEdit_Label_Quantity.Size = new Size(56, 15);
            Form_QuickButtonEdit_Label_Quantity.TabIndex = 4;
            Form_QuickButtonEdit_Label_Quantity.Text = "Quantity:";
            // 
            // Form_QuickButtonEdit_ComboBox_PartId
            // 
            Form_QuickButtonEdit_ComboBox_PartId.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            Form_QuickButtonEdit_ComboBox_PartId.AutoCompleteSource = AutoCompleteSource.ListItems;
            Form_QuickButtonEdit_ComboBox_PartId.FormattingEnabled = true;
            Form_QuickButtonEdit_ComboBox_PartId.Location = new Point(100, 10);
            Form_QuickButtonEdit_ComboBox_PartId.Name = "Form_QuickButtonEdit_ComboBox_PartId";
            Form_QuickButtonEdit_ComboBox_PartId.Size = new Size(260, 23);
            Form_QuickButtonEdit_ComboBox_PartId.TabIndex = 1;
            Form_QuickButtonEdit_ComboBox_PartId.SelectedIndexChanged += Form_QuickButtonEdit_ComboBox_PartId_SelectedIndexChanged;
            Form_QuickButtonEdit_ComboBox_PartId.TextChanged += Form_QuickButtonEdit_ComboBox_PartId_TextChanged;
            // 
            // Form_QuickButtonEdit_ComboBox_Operation
            // 
            Form_QuickButtonEdit_ComboBox_Operation.DropDownStyle = ComboBoxStyle.DropDownList;
            Form_QuickButtonEdit_ComboBox_Operation.FormattingEnabled = true;
            Form_QuickButtonEdit_ComboBox_Operation.Location = new Point(100, 50);
            Form_QuickButtonEdit_ComboBox_Operation.Name = "Form_QuickButtonEdit_ComboBox_Operation";
            Form_QuickButtonEdit_ComboBox_Operation.Size = new Size(260, 23);
            Form_QuickButtonEdit_ComboBox_Operation.TabIndex = 3;
            Form_QuickButtonEdit_ComboBox_Operation.SelectedIndexChanged += Form_QuickButtonEdit_ComboBox_Operation_SelectedIndexChanged;
            Form_QuickButtonEdit_ComboBox_Operation.TextChanged += Form_QuickButtonEdit_ComboBox_Operation_TextChanged;
            // 
            // Form_QuickButtonEdit_NumericUpDown_Quantity
            // 
            Form_QuickButtonEdit_NumericUpDown_Quantity.Location = new Point(100, 90);
            Form_QuickButtonEdit_NumericUpDown_Quantity.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            Form_QuickButtonEdit_NumericUpDown_Quantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            Form_QuickButtonEdit_NumericUpDown_Quantity.Name = "Form_QuickButtonEdit_NumericUpDown_Quantity";
            Form_QuickButtonEdit_NumericUpDown_Quantity.Size = new Size(120, 23);
            Form_QuickButtonEdit_NumericUpDown_Quantity.TabIndex = 5;
            Form_QuickButtonEdit_NumericUpDown_Quantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // Form_QuickButtonEdit_Label_Status
            // 
            Form_QuickButtonEdit_Label_Status.Location = new Point(10, 130);
            Form_QuickButtonEdit_Label_Status.Name = "Form_QuickButtonEdit_Label_Status";
            Form_QuickButtonEdit_Label_Status.Size = new Size(370, 40);
            Form_QuickButtonEdit_Label_Status.TabIndex = 6;
            Form_QuickButtonEdit_Label_Status.Text = "Loading data...";
            Form_QuickButtonEdit_Label_Status.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Form_QuickButtonEdit_Button_OK
            // 
            Form_QuickButtonEdit_Button_OK.Enabled = false;
            Form_QuickButtonEdit_Button_OK.Location = new Point(180, 180);
            Form_QuickButtonEdit_Button_OK.Name = "Form_QuickButtonEdit_Button_OK";
            Form_QuickButtonEdit_Button_OK.Size = new Size(80, 23);
            Form_QuickButtonEdit_Button_OK.TabIndex = 7;
            Form_QuickButtonEdit_Button_OK.Text = "OK";
            Form_QuickButtonEdit_Button_OK.UseVisualStyleBackColor = true;
            Form_QuickButtonEdit_Button_OK.Click += Form_QuickButtonEdit_Button_OK_Click;
            // 
            // Form_QuickButtonEdit_Button_Cancel
            // 
            Form_QuickButtonEdit_Button_Cancel.DialogResult = DialogResult.Cancel;
            Form_QuickButtonEdit_Button_Cancel.Location = new Point(270, 180);
            Form_QuickButtonEdit_Button_Cancel.Name = "Form_QuickButtonEdit_Button_Cancel";
            Form_QuickButtonEdit_Button_Cancel.Size = new Size(80, 23);
            Form_QuickButtonEdit_Button_Cancel.TabIndex = 8;
            Form_QuickButtonEdit_Button_Cancel.Text = "Cancel";
            Form_QuickButtonEdit_Button_Cancel.UseVisualStyleBackColor = true;
            // 
            // Form_QuickButtonEdit
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(384, 221);
            Controls.Add(Form_QuickButtonEdit_Button_Cancel);
            Controls.Add(Form_QuickButtonEdit_Button_OK);
            Controls.Add(Form_QuickButtonEdit_Label_Status);
            Controls.Add(Form_QuickButtonEdit_NumericUpDown_Quantity);
            Controls.Add(Form_QuickButtonEdit_ComboBox_Operation);
            Controls.Add(Form_QuickButtonEdit_ComboBox_PartId);
            Controls.Add(Form_QuickButtonEdit_Label_Quantity);
            Controls.Add(Form_QuickButtonEdit_Label_Operation);
            Controls.Add(Form_QuickButtonEdit_Label_PartId);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_QuickButtonEdit";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Edit Quick Button";
            ((System.ComponentModel.ISupportInitialize)Form_QuickButtonEdit_NumericUpDown_Quantity).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label Form_QuickButtonEdit_Label_PartId;
        private System.Windows.Forms.Label Form_QuickButtonEdit_Label_Operation;
        private System.Windows.Forms.Label Form_QuickButtonEdit_Label_Quantity;
        private System.Windows.Forms.ComboBox Form_QuickButtonEdit_ComboBox_PartId;
        private System.Windows.Forms.ComboBox Form_QuickButtonEdit_ComboBox_Operation;
        private System.Windows.Forms.NumericUpDown Form_QuickButtonEdit_NumericUpDown_Quantity;
        private System.Windows.Forms.Label Form_QuickButtonEdit_Label_Status;
        private System.Windows.Forms.Button Form_QuickButtonEdit_Button_OK;
        private System.Windows.Forms.Button Form_QuickButtonEdit_Button_Cancel;
    }
}
