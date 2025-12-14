namespace MTM_WIP_Application_Winforms.Forms.SystemHealth
{
    partial class Form_SystemHealth
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
            this.Form_SystemHealth_Panel_HealthIndicator = new System.Windows.Forms.Panel();
            this.Form_SystemHealth_Label_HealthMessage = new System.Windows.Forms.Label();
            this.Form_SystemHealth_Label_HealthIcon = new System.Windows.Forms.Label();
            this.Form_SystemHealth_GroupBox_Feedback = new System.Windows.Forms.GroupBox();
            this.Form_SystemHealth_DataGridView_Feedback = new System.Windows.Forms.DataGridView();
            this.Form_SystemHealth_Label_NoFeedback = new System.Windows.Forms.Label();
            this.Form_SystemHealth_Button_SubmitFeedback = new System.Windows.Forms.Button();
            this.Form_SystemHealth_Button_ContactSupport = new System.Windows.Forms.Button();
            this.Form_SystemHealth_Panel_Actions = new System.Windows.Forms.Panel();
            
            this.Form_SystemHealth_Panel_HealthIndicator.SuspendLayout();
            this.Form_SystemHealth_GroupBox_Feedback.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Form_SystemHealth_DataGridView_Feedback)).BeginInit();
            this.Form_SystemHealth_Panel_Actions.SuspendLayout();
            this.SuspendLayout();
            
            // 
            // Form_SystemHealth_Panel_HealthIndicator
            // 
            this.Form_SystemHealth_Panel_HealthIndicator.Controls.Add(this.Form_SystemHealth_Label_HealthMessage);
            this.Form_SystemHealth_Panel_HealthIndicator.Controls.Add(this.Form_SystemHealth_Label_HealthIcon);
            this.Form_SystemHealth_Panel_HealthIndicator.Dock = System.Windows.Forms.DockStyle.Top;
            this.Form_SystemHealth_Panel_HealthIndicator.Location = new System.Drawing.Point(0, 0);
            this.Form_SystemHealth_Panel_HealthIndicator.Name = "Form_SystemHealth_Panel_HealthIndicator";
            this.Form_SystemHealth_Panel_HealthIndicator.Size = new System.Drawing.Size(600, 100);
            this.Form_SystemHealth_Panel_HealthIndicator.TabIndex = 0;
            // 
            // Form_SystemHealth_Label_HealthMessage
            // 
            this.Form_SystemHealth_Label_HealthMessage.AutoSize = true;
            this.Form_SystemHealth_Label_HealthMessage.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.Form_SystemHealth_Label_HealthMessage.ForeColor = System.Drawing.Color.White;
            this.Form_SystemHealth_Label_HealthMessage.Location = new System.Drawing.Point(100, 35);
            this.Form_SystemHealth_Label_HealthMessage.Name = "Form_SystemHealth_Label_HealthMessage";
            this.Form_SystemHealth_Label_HealthMessage.Size = new System.Drawing.Size(200, 25);
            this.Form_SystemHealth_Label_HealthMessage.TabIndex = 1;
            this.Form_SystemHealth_Label_HealthMessage.Text = "Checking status...";
            // 
            // Form_SystemHealth_Label_HealthIcon
            // 
            this.Form_SystemHealth_Label_HealthIcon.AutoSize = true;
            this.Form_SystemHealth_Label_HealthIcon.Font = new System.Drawing.Font("Segoe UI", 36F);
            this.Form_SystemHealth_Label_HealthIcon.Location = new System.Drawing.Point(20, 15);
            this.Form_SystemHealth_Label_HealthIcon.Name = "Form_SystemHealth_Label_HealthIcon";
            this.Form_SystemHealth_Label_HealthIcon.Size = new System.Drawing.Size(60, 65);
            this.Form_SystemHealth_Label_HealthIcon.TabIndex = 0;
            this.Form_SystemHealth_Label_HealthIcon.Text = "⏳";
            // 
            // Form_SystemHealth_GroupBox_Feedback
            // 
            this.Form_SystemHealth_GroupBox_Feedback.Controls.Add(this.Form_SystemHealth_Label_NoFeedback);
            this.Form_SystemHealth_GroupBox_Feedback.Controls.Add(this.Form_SystemHealth_DataGridView_Feedback);
            this.Form_SystemHealth_GroupBox_Feedback.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Form_SystemHealth_GroupBox_Feedback.Location = new System.Drawing.Point(0, 100);
            this.Form_SystemHealth_GroupBox_Feedback.Name = "Form_SystemHealth_GroupBox_Feedback";
            this.Form_SystemHealth_GroupBox_Feedback.Padding = new System.Windows.Forms.Padding(10);
            this.Form_SystemHealth_GroupBox_Feedback.Size = new System.Drawing.Size(600, 300);
            this.Form_SystemHealth_GroupBox_Feedback.TabIndex = 1;
            this.Form_SystemHealth_GroupBox_Feedback.TabStop = false;
            this.Form_SystemHealth_GroupBox_Feedback.Text = "Your Recent Feedback";
            // 
            // Form_SystemHealth_DataGridView_Feedback
            // 
            this.Form_SystemHealth_DataGridView_Feedback.AllowUserToAddRows = false;
            this.Form_SystemHealth_DataGridView_Feedback.AllowUserToDeleteRows = false;
            this.Form_SystemHealth_DataGridView_Feedback.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Form_SystemHealth_DataGridView_Feedback.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Form_SystemHealth_DataGridView_Feedback.Location = new System.Drawing.Point(10, 26);
            this.Form_SystemHealth_DataGridView_Feedback.Name = "Form_SystemHealth_DataGridView_Feedback";
            this.Form_SystemHealth_DataGridView_Feedback.ReadOnly = true;
            this.Form_SystemHealth_DataGridView_Feedback.Size = new System.Drawing.Size(580, 264);
            this.Form_SystemHealth_DataGridView_Feedback.TabIndex = 0;
            // 
            // Form_SystemHealth_Label_NoFeedback
            // 
            this.Form_SystemHealth_Label_NoFeedback.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Form_SystemHealth_Label_NoFeedback.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Italic);
            this.Form_SystemHealth_Label_NoFeedback.Location = new System.Drawing.Point(10, 26);
            this.Form_SystemHealth_Label_NoFeedback.Name = "Form_SystemHealth_Label_NoFeedback";
            this.Form_SystemHealth_Label_NoFeedback.Size = new System.Drawing.Size(580, 264);
            this.Form_SystemHealth_Label_NoFeedback.TabIndex = 1;
            this.Form_SystemHealth_Label_NoFeedback.Text = "No feedback submitted yet.";
            this.Form_SystemHealth_Label_NoFeedback.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Form_SystemHealth_Label_NoFeedback.Visible = false;
            // 
            // Form_SystemHealth_Panel_Actions
            // 
            this.Form_SystemHealth_Panel_Actions.Controls.Add(this.Form_SystemHealth_Button_ContactSupport);
            this.Form_SystemHealth_Panel_Actions.Controls.Add(this.Form_SystemHealth_Button_SubmitFeedback);
            this.Form_SystemHealth_Panel_Actions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Form_SystemHealth_Panel_Actions.Location = new System.Drawing.Point(0, 400);
            this.Form_SystemHealth_Panel_Actions.Name = "Form_SystemHealth_Panel_Actions";
            this.Form_SystemHealth_Panel_Actions.Size = new System.Drawing.Size(600, 60);
            this.Form_SystemHealth_Panel_Actions.TabIndex = 2;
            // 
            // Form_SystemHealth_Button_SubmitFeedback
            // 
            this.Form_SystemHealth_Button_SubmitFeedback.Location = new System.Drawing.Point(150, 15);
            this.Form_SystemHealth_Button_SubmitFeedback.Name = "Form_SystemHealth_Button_SubmitFeedback";
            this.Form_SystemHealth_Button_SubmitFeedback.Size = new System.Drawing.Size(140, 30);
            this.Form_SystemHealth_Button_SubmitFeedback.TabIndex = 0;
            this.Form_SystemHealth_Button_SubmitFeedback.Text = "Submit New Feedback";
            this.Form_SystemHealth_Button_SubmitFeedback.UseVisualStyleBackColor = true;
            this.Form_SystemHealth_Button_SubmitFeedback.Click += new System.EventHandler(this.Form_SystemHealth_Button_SubmitFeedback_Click);
            // 
            // Form_SystemHealth_Button_ContactSupport
            // 
            this.Form_SystemHealth_Button_ContactSupport.Location = new System.Drawing.Point(310, 15);
            this.Form_SystemHealth_Button_ContactSupport.Name = "Form_SystemHealth_Button_ContactSupport";
            this.Form_SystemHealth_Button_ContactSupport.Size = new System.Drawing.Size(140, 30);
            this.Form_SystemHealth_Button_ContactSupport.TabIndex = 1;
            this.Form_SystemHealth_Button_ContactSupport.Text = "Contact IT Support";
            this.Form_SystemHealth_Button_ContactSupport.UseVisualStyleBackColor = true;
            this.Form_SystemHealth_Button_ContactSupport.Click += new System.EventHandler(this.Form_SystemHealth_Button_ContactSupport_Click);
            // 
            // Form_SystemHealth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 460);
            this.Controls.Add(this.Form_SystemHealth_GroupBox_Feedback);
            this.Controls.Add(this.Form_SystemHealth_Panel_Actions);
            this.Controls.Add(this.Form_SystemHealth_Panel_HealthIndicator);
            this.Name = "Form_SystemHealth";
            this.Text = "System Health";
            this.Load += new System.EventHandler(this.Form_SystemHealth_Load);
            this.Form_SystemHealth_Panel_HealthIndicator.ResumeLayout(false);
            this.Form_SystemHealth_Panel_HealthIndicator.PerformLayout();
            this.Form_SystemHealth_GroupBox_Feedback.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Form_SystemHealth_DataGridView_Feedback)).EndInit();
            this.Form_SystemHealth_Panel_Actions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Form_SystemHealth_Panel_HealthIndicator;
        private System.Windows.Forms.Label Form_SystemHealth_Label_HealthMessage;
        private System.Windows.Forms.Label Form_SystemHealth_Label_HealthIcon;
        private System.Windows.Forms.GroupBox Form_SystemHealth_GroupBox_Feedback;
        private System.Windows.Forms.DataGridView Form_SystemHealth_DataGridView_Feedback;
        private System.Windows.Forms.Label Form_SystemHealth_Label_NoFeedback;
        private System.Windows.Forms.Panel Form_SystemHealth_Panel_Actions;
        private System.Windows.Forms.Button Form_SystemHealth_Button_SubmitFeedback;
        private System.Windows.Forms.Button Form_SystemHealth_Button_ContactSupport;
    }
}
