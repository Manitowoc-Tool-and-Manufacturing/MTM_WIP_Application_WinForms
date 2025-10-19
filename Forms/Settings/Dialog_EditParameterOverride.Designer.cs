namespace MTM_Inventory_Application.Forms.Settings
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
            this.lblProcedureName = new System.Windows.Forms.Label();
            this.txtProcedureName = new System.Windows.Forms.TextBox();
            this.lblParameterName = new System.Windows.Forms.Label();
            this.txtParameterName = new System.Windows.Forms.TextBox();
            this.lblOverridePrefix = new System.Windows.Forms.Label();
            this.txtOverridePrefix = new System.Windows.Forms.TextBox();
            this.lblReason = new System.Windows.Forms.Label();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblCreatedInfo = new System.Windows.Forms.Label();
            this.lblModifiedInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblProcedureName
            // 
            this.lblProcedureName.AutoSize = true;
            this.lblProcedureName.Location = new System.Drawing.Point(12, 70);
            this.lblProcedureName.Name = "lblProcedureName";
            this.lblProcedureName.Size = new System.Drawing.Size(98, 15);
            this.lblProcedureName.TabIndex = 0;
            this.lblProcedureName.Text = "Procedure Name:";
            // 
            // txtProcedureName
            // 
            this.txtProcedureName.Location = new System.Drawing.Point(12, 88);
            this.txtProcedureName.Name = "txtProcedureName";
            this.txtProcedureName.Size = new System.Drawing.Size(460, 23);
            this.txtProcedureName.TabIndex = 1;
            this.txtProcedureName.TextChanged += new System.EventHandler(this.TxtProcedureName_TextChanged);
            // 
            // lblParameterName
            // 
            this.lblParameterName.AutoSize = true;
            this.lblParameterName.Location = new System.Drawing.Point(12, 120);
            this.lblParameterName.Name = "lblParameterName";
            this.lblParameterName.Size = new System.Drawing.Size(98, 15);
            this.lblParameterName.TabIndex = 2;
            this.lblParameterName.Text = "Parameter Name:";
            // 
            // txtParameterName
            // 
            this.txtParameterName.Location = new System.Drawing.Point(12, 138);
            this.txtParameterName.Name = "txtParameterName";
            this.txtParameterName.Size = new System.Drawing.Size(460, 23);
            this.txtParameterName.TabIndex = 3;
            // 
            // lblOverridePrefix
            // 
            this.lblOverridePrefix.AutoSize = true;
            this.lblOverridePrefix.Location = new System.Drawing.Point(12, 170);
            this.lblOverridePrefix.Name = "lblOverridePrefix";
            this.lblOverridePrefix.Size = new System.Drawing.Size(212, 15);
            this.lblOverridePrefix.TabIndex = 4;
            this.lblOverridePrefix.Text = "Override Prefix (leave empty for none):";
            // 
            // txtOverridePrefix
            // 
            this.txtOverridePrefix.Location = new System.Drawing.Point(12, 188);
            this.txtOverridePrefix.MaxLength = 10;
            this.txtOverridePrefix.Name = "txtOverridePrefix";
            this.txtOverridePrefix.Size = new System.Drawing.Size(200, 23);
            this.txtOverridePrefix.TabIndex = 5;
            // 
            // lblReason
            // 
            this.lblReason.AutoSize = true;
            this.lblReason.Location = new System.Drawing.Point(12, 220);
            this.lblReason.Name = "lblReason";
            this.lblReason.Size = new System.Drawing.Size(48, 15);
            this.lblReason.TabIndex = 6;
            this.lblReason.Text = "Reason:";
            // 
            // txtReason
            // 
            this.txtReason.Location = new System.Drawing.Point(12, 238);
            this.txtReason.Multiline = true;
            this.txtReason.Name = "txtReason";
            this.txtReason.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtReason.Size = new System.Drawing.Size(460, 80);
            this.txtReason.TabIndex = 7;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(316, 380);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(397, 380);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.ForeColor = System.Drawing.Color.Blue;
            this.lblStatus.Location = new System.Drawing.Point(12, 387);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(39, 15);
            this.lblStatus.TabIndex = 10;
            this.lblStatus.Text = "Ready";
            this.lblStatus.Visible = false;
            // 
            // lblInfo
            // 
            this.lblInfo.Location = new System.Drawing.Point(12, 9);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(460, 16);
            this.lblInfo.TabIndex = 11;
            this.lblInfo.Text = "Edit parameter prefix override. Use autocomplete to select from existing procedure" +
    "s and parameters.";
            // 
            // lblCreatedInfo
            // 
            this.lblCreatedInfo.AutoSize = true;
            this.lblCreatedInfo.ForeColor = System.Drawing.Color.Gray;
            this.lblCreatedInfo.Location = new System.Drawing.Point(12, 32);
            this.lblCreatedInfo.Name = "lblCreatedInfo";
            this.lblCreatedInfo.Size = new System.Drawing.Size(107, 15);
            this.lblCreatedInfo.TabIndex = 12;
            this.lblCreatedInfo.Text = "Created: [audit trail]";
            // 
            // lblModifiedInfo
            // 
            this.lblModifiedInfo.AutoSize = true;
            this.lblModifiedInfo.ForeColor = System.Drawing.Color.Gray;
            this.lblModifiedInfo.Location = new System.Drawing.Point(12, 50);
            this.lblModifiedInfo.Name = "lblModifiedInfo";
            this.lblModifiedInfo.Size = new System.Drawing.Size(118, 15);
            this.lblModifiedInfo.TabIndex = 13;
            this.lblModifiedInfo.Text = "Modified: [audit trail]";
            this.lblModifiedInfo.Visible = false;
            // 
            // Dialog_EditParameterOverride
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(484, 421);
            this.Controls.Add(this.lblModifiedInfo);
            this.Controls.Add(this.lblCreatedInfo);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtReason);
            this.Controls.Add(this.lblReason);
            this.Controls.Add(this.txtOverridePrefix);
            this.Controls.Add(this.lblOverridePrefix);
            this.Controls.Add(this.txtParameterName);
            this.Controls.Add(this.lblParameterName);
            this.Controls.Add(this.txtProcedureName);
            this.Controls.Add(this.lblProcedureName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Dialog_EditParameterOverride";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Parameter Prefix Override";
            this.Load += new System.EventHandler(this.Dialog_EditParameterOverride_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

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
