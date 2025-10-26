namespace MTM_Inventory_Application.Forms.ErrorDialog
{
    partial class Form_ReportIssue
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
            this.lblErrorSummary = new System.Windows.Forms.Label();
            this.txtErrorSummary = new System.Windows.Forms.TextBox();
            this.lblUserNotes = new System.Windows.Forms.Label();
            this.txtUserNotes = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblErrorSummary
            // 
            this.lblErrorSummary.AutoSize = true;
            this.lblErrorSummary.Location = new System.Drawing.Point(12, 12);
            this.lblErrorSummary.Name = "lblErrorSummary";
            this.lblErrorSummary.Size = new System.Drawing.Size(89, 15);
            this.lblErrorSummary.TabIndex = 0;
            this.lblErrorSummary.Text = "Error Summary:";
            // 
            // txtErrorSummary
            // 
            this.txtErrorSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtErrorSummary.BackColor = System.Drawing.SystemColors.Control;
            this.txtErrorSummary.Location = new System.Drawing.Point(12, 30);
            this.txtErrorSummary.Multiline = true;
            this.txtErrorSummary.Name = "txtErrorSummary";
            this.txtErrorSummary.ReadOnly = true;
            this.txtErrorSummary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtErrorSummary.Size = new System.Drawing.Size(560, 80);
            this.txtErrorSummary.TabIndex = 1;
            this.txtErrorSummary.TabStop = false;
            // 
            // lblUserNotes
            // 
            this.lblUserNotes.AutoSize = true;
            this.lblUserNotes.Location = new System.Drawing.Point(12, 120);
            this.lblUserNotes.Name = "lblUserNotes";
            this.lblUserNotes.Size = new System.Drawing.Size(346, 15);
            this.lblUserNotes.TabIndex = 2;
            this.lblUserNotes.Text = "What were you doing when this error occurred? (optional):";
            // 
            // txtUserNotes
            // 
            this.txtUserNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserNotes.Location = new System.Drawing.Point(12, 138);
            this.txtUserNotes.Multiline = true;
            this.txtUserNotes.Name = "txtUserNotes";
            this.txtUserNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtUserNotes.Size = new System.Drawing.Size(560, 120);
            this.txtUserNotes.TabIndex = 3;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.Location = new System.Drawing.Point(416, 274);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 28);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(497, 274);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 28);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // Form_ReportIssue
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(584, 314);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.txtUserNotes);
            this.Controls.Add(this.lblUserNotes);
            this.Controls.Add(this.txtErrorSummary);
            this.Controls.Add(this.lblErrorSummary);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_ReportIssue";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Report Issue";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblErrorSummary;
        private System.Windows.Forms.TextBox txtErrorSummary;
        private System.Windows.Forms.Label lblUserNotes;
        private System.Windows.Forms.TextBox txtUserNotes;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
    }
}
