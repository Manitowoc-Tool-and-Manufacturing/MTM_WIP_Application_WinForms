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
            Form_QuickButtonEdit_Label_PartId = new System.Windows.Forms.Label();
            Form_QuickButtonEdit_Label_Operation = new System.Windows.Forms.Label();
            Form_QuickButtonEdit_Label_Quantity = new System.Windows.Forms.Label();
            Form_QuickButtonEdit_ComboBox_PartId = new System.Windows.Forms.ComboBox();
            Form_QuickButtonEdit_ComboBox_Operation = new System.Windows.Forms.ComboBox();
            Form_QuickButtonEdit_NumericUpDown_Quantity = new System.Windows.Forms.NumericUpDown();
            Form_QuickButtonEdit_Label_Status = new System.Windows.Forms.Label();
            Form_QuickButtonEdit_Button_OK = new System.Windows.Forms.Button();
            Form_QuickButtonEdit_Button_Cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)Form_QuickButtonEdit_NumericUpDown_Quantity).BeginInit();
            SuspendLayout();
            // 
            // Form_QuickButtonEdit_Label_PartId
            // 
            Form_QuickButtonEdit_Label_PartId.AutoSize = true;
            Form_QuickButtonEdit_Label_PartId.Location = new System.Drawing.Point(10, 15);
            Form_QuickButtonEdit_Label_PartId.Name = "Form_QuickButtonEdit_Label_PartId";
            Form_QuickButtonEdit_Label_PartId.Size = new System.Drawing.Size(46, 15);
            Form_QuickButtonEdit_Label_PartId.TabIndex = 0;
            Form_QuickButtonEdit_Label_PartId.Text = "Part ID:";
            // 
            // Form_QuickButtonEdit_Label_Operation
            // 
            Form_QuickButtonEdit_Label_Operation.AutoSize = true;
            Form_QuickButtonEdit_Label_Operation.Location = new System.Drawing.Point(10, 55);
            Form_QuickButtonEdit_Label_Operation.Name = "Form_QuickButtonEdit_Label_Operation";
            Form_QuickButtonEdit_Label_Operation.Size = new System.Drawing.Size(62, 15);
            Form_QuickButtonEdit_Label_Operation.TabIndex = 2;
            Form_QuickButtonEdit_Label_Operation.Text = "Operation:";
            // 
            // Form_QuickButtonEdit_Label_Quantity
            // 
            Form_QuickButtonEdit_Label_Quantity.AutoSize = true;
            Form_QuickButtonEdit_Label_Quantity.Location = new System.Drawing.Point(10, 95);
            Form_QuickButtonEdit_Label_Quantity.Name = "Form_QuickButtonEdit_Label_Quantity";
            Form_QuickButtonEdit_Label_Quantity.Size = new System.Drawing.Size(56, 15);
            Form_QuickButtonEdit_Label_Quantity.TabIndex = 4;
            Form_QuickButtonEdit_Label_Quantity.Text = "Quantity:";
            // 
            // Form_QuickButtonEdit_ComboBox_PartId
            // 
            Form_QuickButtonEdit_ComboBox_PartId.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            Form_QuickButtonEdit_ComboBox_PartId.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            Form_QuickButtonEdit_ComboBox_PartId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            Form_QuickButtonEdit_ComboBox_PartId.FormattingEnabled = true;
            Form_QuickButtonEdit_ComboBox_PartId.Location = new System.Drawing.Point(100, 10);
            Form_QuickButtonEdit_ComboBox_PartId.Name = "Form_QuickButtonEdit_ComboBox_PartId";
            Form_QuickButtonEdit_ComboBox_PartId.Size = new System.Drawing.Size(260, 23);
            Form_QuickButtonEdit_ComboBox_PartId.TabIndex = 1;
            Form_QuickButtonEdit_ComboBox_PartId.SelectedIndexChanged += Form_QuickButtonEdit_ComboBox_PartId_SelectedIndexChanged;
            Form_QuickButtonEdit_ComboBox_PartId.TextChanged += Form_QuickButtonEdit_ComboBox_PartId_TextChanged;
            // 
            // Form_QuickButtonEdit_ComboBox_Operation
            // 
            Form_QuickButtonEdit_ComboBox_Operation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            Form_QuickButtonEdit_ComboBox_Operation.FormattingEnabled = true;
            Form_QuickButtonEdit_ComboBox_Operation.Location = new System.Drawing.Point(100, 50);
            Form_QuickButtonEdit_ComboBox_Operation.Name = "Form_QuickButtonEdit_ComboBox_Operation";
            Form_QuickButtonEdit_ComboBox_Operation.Size = new System.Drawing.Size(260, 23);
            Form_QuickButtonEdit_ComboBox_Operation.TabIndex = 3;
            Form_QuickButtonEdit_ComboBox_Operation.SelectedIndexChanged += Form_QuickButtonEdit_ComboBox_Operation_SelectedIndexChanged;
            Form_QuickButtonEdit_ComboBox_Operation.TextChanged += Form_QuickButtonEdit_ComboBox_Operation_TextChanged;
            // 
            // Form_QuickButtonEdit_NumericUpDown_Quantity
            // 
            Form_QuickButtonEdit_NumericUpDown_Quantity.Location = new System.Drawing.Point(100, 90);
            Form_QuickButtonEdit_NumericUpDown_Quantity.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            Form_QuickButtonEdit_NumericUpDown_Quantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            Form_QuickButtonEdit_NumericUpDown_Quantity.Name = "Form_QuickButtonEdit_NumericUpDown_Quantity";
            Form_QuickButtonEdit_NumericUpDown_Quantity.Size = new System.Drawing.Size(120, 23);
            Form_QuickButtonEdit_NumericUpDown_Quantity.TabIndex = 5;
            Form_QuickButtonEdit_NumericUpDown_Quantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // Form_QuickButtonEdit_Label_Status
            // 
            Form_QuickButtonEdit_Label_Status.Location = new System.Drawing.Point(10, 130);
            Form_QuickButtonEdit_Label_Status.Name = "Form_QuickButtonEdit_Label_Status";
            Form_QuickButtonEdit_Label_Status.Size = new System.Drawing.Size(370, 40);
            Form_QuickButtonEdit_Label_Status.TabIndex = 6;
            Form_QuickButtonEdit_Label_Status.Text = "Loading data...";
            Form_QuickButtonEdit_Label_Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Form_QuickButtonEdit_Button_OK
            // 
            Form_QuickButtonEdit_Button_OK.Enabled = false;
            Form_QuickButtonEdit_Button_OK.Location = new System.Drawing.Point(180, 180);
            Form_QuickButtonEdit_Button_OK.Name = "Form_QuickButtonEdit_Button_OK";
            Form_QuickButtonEdit_Button_OK.Size = new System.Drawing.Size(80, 23);
            Form_QuickButtonEdit_Button_OK.TabIndex = 7;
            Form_QuickButtonEdit_Button_OK.Text = "OK";
            Form_QuickButtonEdit_Button_OK.UseVisualStyleBackColor = true;
            Form_QuickButtonEdit_Button_OK.Click += Form_QuickButtonEdit_Button_OK_Click;
            // 
            // Form_QuickButtonEdit_Button_Cancel
            // 
            Form_QuickButtonEdit_Button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Form_QuickButtonEdit_Button_Cancel.Location = new System.Drawing.Point(270, 180);
            Form_QuickButtonEdit_Button_Cancel.Name = "Form_QuickButtonEdit_Button_Cancel";
            Form_QuickButtonEdit_Button_Cancel.Size = new System.Drawing.Size(80, 23);
            Form_QuickButtonEdit_Button_Cancel.TabIndex = 8;
            Form_QuickButtonEdit_Button_Cancel.Text = "Cancel";
            Form_QuickButtonEdit_Button_Cancel.UseVisualStyleBackColor = true;
            // 
            // Form_QuickButtonEdit
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            ClientSize = new System.Drawing.Size(384, 221);
            Controls.Add(Form_QuickButtonEdit_Button_Cancel);
            Controls.Add(Form_QuickButtonEdit_Button_OK);
            Controls.Add(Form_QuickButtonEdit_Label_Status);
            Controls.Add(Form_QuickButtonEdit_NumericUpDown_Quantity);
            Controls.Add(Form_QuickButtonEdit_ComboBox_Operation);
            Controls.Add(Form_QuickButtonEdit_ComboBox_PartId);
            Controls.Add(Form_QuickButtonEdit_Label_Quantity);
            Controls.Add(Form_QuickButtonEdit_Label_Operation);
            Controls.Add(Form_QuickButtonEdit_Label_PartId);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_QuickButtonEdit";
            ShowInTaskbar = false;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
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
