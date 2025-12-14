namespace MTM_WIP_Application_Winforms.Controls.DeveloperTools
{
    partial class Control_SystemHealth
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
            this.Control_SystemHealth_Panel_StatusColor = new System.Windows.Forms.Panel();
            this.Control_SystemHealth_Label_Status = new System.Windows.Forms.Label();
            this.Control_SystemHealth_Label_ErrorCount = new System.Windows.Forms.Label();
            this.Control_SystemHealth_Label_WarningCount = new System.Windows.Forms.Label();
            this.Control_SystemHealth_Label_LastCheck = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Control_SystemHealth_Panel_StatusColor
            // 
            this.Control_SystemHealth_Panel_StatusColor.BackColor = System.Drawing.Color.ForestGreen;
            this.Control_SystemHealth_Panel_StatusColor.Location = new System.Drawing.Point(15, 15);
            this.Control_SystemHealth_Panel_StatusColor.Name = "Control_SystemHealth_Panel_StatusColor";
            this.Control_SystemHealth_Panel_StatusColor.Size = new System.Drawing.Size(20, 20);
            this.Control_SystemHealth_Panel_StatusColor.TabIndex = 0;
            // 
            // Control_SystemHealth_Label_Status
            // 
            this.Control_SystemHealth_Label_Status.AutoSize = true;
            this.Control_SystemHealth_Label_Status.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Control_SystemHealth_Label_Status.ForeColor = System.Drawing.Color.ForestGreen;
            this.Control_SystemHealth_Label_Status.Location = new System.Drawing.Point(45, 13);
            this.Control_SystemHealth_Label_Status.Name = "Control_SystemHealth_Label_Status";
            this.Control_SystemHealth_Label_Status.Size = new System.Drawing.Size(214, 21);
            this.Control_SystemHealth_Label_Status.TabIndex = 1;
            this.Control_SystemHealth_Label_Status.Text = "System Operating Normally";
            // 
            // Control_SystemHealth_Label_ErrorCount
            // 
            this.Control_SystemHealth_Label_ErrorCount.AutoSize = true;
            this.Control_SystemHealth_Label_ErrorCount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Control_SystemHealth_Label_ErrorCount.Location = new System.Drawing.Point(46, 40);
            this.Control_SystemHealth_Label_ErrorCount.Name = "Control_SystemHealth_Label_ErrorCount";
            this.Control_SystemHealth_Label_ErrorCount.Size = new System.Drawing.Size(54, 17);
            this.Control_SystemHealth_Label_ErrorCount.TabIndex = 2;
            this.Control_SystemHealth_Label_ErrorCount.Text = "0 Errors";
            // 
            // Control_SystemHealth_Label_WarningCount
            // 
            this.Control_SystemHealth_Label_WarningCount.AutoSize = true;
            this.Control_SystemHealth_Label_WarningCount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Control_SystemHealth_Label_WarningCount.Location = new System.Drawing.Point(120, 40);
            this.Control_SystemHealth_Label_WarningCount.Name = "Control_SystemHealth_Label_WarningCount";
            this.Control_SystemHealth_Label_WarningCount.Size = new System.Drawing.Size(74, 17);
            this.Control_SystemHealth_Label_WarningCount.TabIndex = 3;
            this.Control_SystemHealth_Label_WarningCount.Text = "0 Warnings";
            // 
            // Control_SystemHealth_Label_LastCheck
            // 
            this.Control_SystemHealth_Label_LastCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Control_SystemHealth_Label_LastCheck.AutoSize = true;
            this.Control_SystemHealth_Label_LastCheck.ForeColor = System.Drawing.Color.Gray;
            this.Control_SystemHealth_Label_LastCheck.Location = new System.Drawing.Point(250, 18);
            this.Control_SystemHealth_Label_LastCheck.Name = "Control_SystemHealth_Label_LastCheck";
            this.Control_SystemHealth_Label_LastCheck.Size = new System.Drawing.Size(115, 15);
            this.Control_SystemHealth_Label_LastCheck.TabIndex = 4;
            this.Control_SystemHealth_Label_LastCheck.Text = "Last Checked: 12:00";
            // 
            // Control_SystemHealth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Control_SystemHealth_Label_LastCheck);
            this.Controls.Add(this.Control_SystemHealth_Label_WarningCount);
            this.Controls.Add(this.Control_SystemHealth_Label_ErrorCount);
            this.Controls.Add(this.Control_SystemHealth_Label_Status);
            this.Controls.Add(this.Control_SystemHealth_Panel_StatusColor);
            this.Name = "Control_SystemHealth";
            this.Size = new System.Drawing.Size(400, 75);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Control_SystemHealth_Panel_StatusColor;
        private System.Windows.Forms.Label Control_SystemHealth_Label_Status;
        private System.Windows.Forms.Label Control_SystemHealth_Label_ErrorCount;
        private System.Windows.Forms.Label Control_SystemHealth_Label_WarningCount;
        private System.Windows.Forms.Label Control_SystemHealth_Label_LastCheck;
    }
}

