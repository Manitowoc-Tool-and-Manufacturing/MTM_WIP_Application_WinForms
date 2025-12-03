namespace MTM_WIP_Application_Winforms.Forms.Settings
{
    partial class Dialog_EditParameterOverride
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dialog_EditParameterOverride));
            lblProcedureName = new Label();
            txtProcedureName = new TextBox();
            lblParameterName = new Label();
            txtParameterName = new TextBox();
            lblOverridePrefix = new Label();
            txtOverridePrefix = new TextBox();
            lblReason = new Label();
            txtReason = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            lblStatus = new Label();
            lblInfo = new Label();
            lblCreatedInfo = new Label();
            lblModifiedInfo = new Label();
            SuspendLayout();
            // 
            // lblProcedureName
            // 
            lblProcedureName.AutoSize = true;
            lblProcedureName.Location = new Point(12, 70);
            lblProcedureName.Name = "lblProcedureName";
            lblProcedureName.Size = new Size(99, 15);
            lblProcedureName.TabIndex = 0;
            lblProcedureName.Text = "Procedure Name:";
            // 
            // txtProcedureName
            // 
            txtProcedureName.Location = new Point(12, 88);
            txtProcedureName.Name = "txtProcedureName";
            txtProcedureName.Size = new Size(460, 23);
            txtProcedureName.TabIndex = 1;
            txtProcedureName.TextChanged += TxtProcedureName_TextChanged;
            // 
            // lblParameterName
            // 
            lblParameterName.AutoSize = true;
            lblParameterName.Location = new Point(12, 120);
            lblParameterName.Name = "lblParameterName";
            lblParameterName.Size = new Size(99, 15);
            lblParameterName.TabIndex = 2;
            lblParameterName.Text = "Parameter Name:";
            // 
            // txtParameterName
            // 
            txtParameterName.Location = new Point(12, 138);
            txtParameterName.Name = "txtParameterName";
            txtParameterName.Size = new Size(460, 23);
            txtParameterName.TabIndex = 3;
            // 
            // lblOverridePrefix
            // 
            lblOverridePrefix.AutoSize = true;
            lblOverridePrefix.Location = new Point(12, 170);
            lblOverridePrefix.Name = "lblOverridePrefix";
            lblOverridePrefix.Size = new Size(210, 15);
            lblOverridePrefix.TabIndex = 4;
            lblOverridePrefix.Text = "Override Prefix (leave empty for none):";
            // 
            // txtOverridePrefix
            // 
            txtOverridePrefix.Location = new Point(12, 188);
            txtOverridePrefix.MaxLength = 10;
            txtOverridePrefix.Name = "txtOverridePrefix";
            txtOverridePrefix.Size = new Size(200, 23);
            txtOverridePrefix.TabIndex = 5;
            // 
            // lblReason
            // 
            lblReason.AutoSize = true;
            lblReason.Location = new Point(12, 220);
            lblReason.Name = "lblReason";
            lblReason.Size = new Size(48, 15);
            lblReason.TabIndex = 6;
            lblReason.Text = "Reason:";
            // 
            // txtReason
            // 
            txtReason.Location = new Point(12, 238);
            txtReason.Multiline = true;
            txtReason.Name = "txtReason";
            txtReason.ScrollBars = ScrollBars.Vertical;
            txtReason.Size = new Size(460, 80);
            txtReason.TabIndex = 7;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(316, 380);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 30);
            btnSave.TabIndex = 8;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += BtnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(397, 380);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 30);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += BtnCancel_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.ForeColor = Color.Blue;
            lblStatus.Location = new Point(12, 387);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(39, 15);
            lblStatus.TabIndex = 10;
            lblStatus.Text = "Ready";
            lblStatus.Visible = false;
            // 
            // lblInfo
            // 
            lblInfo.Location = new Point(12, 9);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(460, 16);
            lblInfo.TabIndex = 11;
            lblInfo.Text = "Edit parameter prefix override. Use autocomplete to select from existing procedures and parameters.";
            // 
            // lblCreatedInfo
            // 
            lblCreatedInfo.AutoSize = true;
            lblCreatedInfo.ForeColor = Color.Gray;
            lblCreatedInfo.Location = new Point(12, 32);
            lblCreatedInfo.Name = "lblCreatedInfo";
            lblCreatedInfo.Size = new Size(112, 15);
            lblCreatedInfo.TabIndex = 12;
            lblCreatedInfo.Text = "Created: [audit trail]";
            // 
            // lblModifiedInfo
            // 
            lblModifiedInfo.AutoSize = true;
            lblModifiedInfo.ForeColor = Color.Gray;
            lblModifiedInfo.Location = new Point(12, 50);
            lblModifiedInfo.Name = "lblModifiedInfo";
            lblModifiedInfo.Size = new Size(119, 15);
            lblModifiedInfo.TabIndex = 13;
            lblModifiedInfo.Text = "Modified: [audit trail]";
            lblModifiedInfo.Visible = false;
            // 
            // Dialog_EditParameterOverride
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = btnCancel;
            ClientSize = new Size(484, 421);
            Controls.Add(lblModifiedInfo);
            Controls.Add(lblCreatedInfo);
            Controls.Add(lblInfo);
            Controls.Add(lblStatus);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(txtReason);
            Controls.Add(lblReason);
            Controls.Add(txtOverridePrefix);
            Controls.Add(lblOverridePrefix);
            Controls.Add(txtParameterName);
            Controls.Add(lblParameterName);
            Controls.Add(txtProcedureName);
            Controls.Add(lblProcedureName);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Dialog_EditParameterOverride";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Edit Parameter Prefix Override";
            Load += Dialog_EditParameterOverride_Load;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProcedureName;
        private System.Windows.Forms.TextBox txtProcedureName;
        private System.Windows.Forms.Label lblParameterName;
        private System.Windows.Forms.TextBox txtParameterName;
        private System.Windows.Forms.Label lblOverridePrefix;
        private System.Windows.Forms.TextBox txtOverridePrefix;
        private System.Windows.Forms.Label lblReason;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblCreatedInfo;
        private System.Windows.Forms.Label lblModifiedInfo;
    }
}
