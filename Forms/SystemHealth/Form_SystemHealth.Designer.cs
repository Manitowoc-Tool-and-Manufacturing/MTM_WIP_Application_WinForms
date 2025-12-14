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
            
            this.Form_SystemHealth_Label_UserFullName = new System.Windows.Forms.Label();
            this.Form_SystemHealth_TableLayout_FeedbackDetails = new System.Windows.Forms.TableLayoutPanel();
            this.Form_SystemHealth_Button_Previous = new System.Windows.Forms.Button();
            this.Form_SystemHealth_Button_Next = new System.Windows.Forms.Button();
            
            this.Form_SystemHealth_TextBox_FeedbackType = new System.Windows.Forms.TextBox();
            this.Form_SystemHealth_TextBox_SubmissionDate = new System.Windows.Forms.TextBox();
            this.Form_SystemHealth_TextBox_ActiveSection = new System.Windows.Forms.TextBox();
            this.Form_SystemHealth_TextBox_Category = new System.Windows.Forms.TextBox();
            this.Form_SystemHealth_TextBox_Severity = new System.Windows.Forms.TextBox();
            this.Form_SystemHealth_TextBox_Priority = new System.Windows.Forms.TextBox();
            this.Form_SystemHealth_TextBox_Title = new System.Windows.Forms.TextBox();
            this.Form_SystemHealth_TextBox_Description = new System.Windows.Forms.TextBox();
            this.Form_SystemHealth_TextBox_Status = new System.Windows.Forms.TextBox();

            this.Form_SystemHealth_TextBox_FeedbackType.ReadOnly = true;
            this.Form_SystemHealth_TextBox_FeedbackType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Form_SystemHealth_TextBox_SubmissionDate.ReadOnly = true;
            this.Form_SystemHealth_TextBox_SubmissionDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Form_SystemHealth_TextBox_ActiveSection.ReadOnly = true;
            this.Form_SystemHealth_TextBox_ActiveSection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Form_SystemHealth_TextBox_Category.ReadOnly = true;
            this.Form_SystemHealth_TextBox_Category.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Form_SystemHealth_TextBox_Severity.ReadOnly = true;
            this.Form_SystemHealth_TextBox_Severity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Form_SystemHealth_TextBox_Priority.ReadOnly = true;
            this.Form_SystemHealth_TextBox_Priority.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Form_SystemHealth_TextBox_Title.ReadOnly = true;
            this.Form_SystemHealth_TextBox_Title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Form_SystemHealth_TextBox_Description.ReadOnly = true;
            this.Form_SystemHealth_TextBox_Description.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Form_SystemHealth_TextBox_Description.Multiline = true;
            this.Form_SystemHealth_TextBox_Description.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Form_SystemHealth_TextBox_Status.ReadOnly = true;
            this.Form_SystemHealth_TextBox_Status.Dock = System.Windows.Forms.DockStyle.Fill;

            this.Form_SystemHealth_Panel_Actions = new System.Windows.Forms.Panel();
            this.Form_SystemHealth_Button_SubmitFeedback = new System.Windows.Forms.Button();
            
            this.Form_SystemHealth_Panel_HealthIndicator.SuspendLayout();
            this.Form_SystemHealth_GroupBox_Feedback.SuspendLayout();
            this.Form_SystemHealth_TableLayout_FeedbackDetails.SuspendLayout();
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
            this.Form_SystemHealth_GroupBox_Feedback.Controls.Add(this.Form_SystemHealth_Button_Next);
            this.Form_SystemHealth_GroupBox_Feedback.Controls.Add(this.Form_SystemHealth_Button_Previous);
            this.Form_SystemHealth_GroupBox_Feedback.Controls.Add(this.Form_SystemHealth_TableLayout_FeedbackDetails);
            this.Form_SystemHealth_GroupBox_Feedback.Controls.Add(this.Form_SystemHealth_Label_UserFullName);
            this.Form_SystemHealth_GroupBox_Feedback.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Form_SystemHealth_GroupBox_Feedback.Location = new System.Drawing.Point(0, 100);
            this.Form_SystemHealth_GroupBox_Feedback.Name = "Form_SystemHealth_GroupBox_Feedback";
            this.Form_SystemHealth_GroupBox_Feedback.Padding = new System.Windows.Forms.Padding(10);
            this.Form_SystemHealth_GroupBox_Feedback.Size = new System.Drawing.Size(600, 300);
            this.Form_SystemHealth_GroupBox_Feedback.TabIndex = 1;
            this.Form_SystemHealth_GroupBox_Feedback.TabStop = false;
            this.Form_SystemHealth_GroupBox_Feedback.Text = "Your Recent Feedback";
            // 
            // Form_SystemHealth_Label_UserFullName
            // 
            this.Form_SystemHealth_Label_UserFullName.AutoSize = true;
            this.Form_SystemHealth_Label_UserFullName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.Form_SystemHealth_Label_UserFullName.Location = new System.Drawing.Point(13, 25);
            this.Form_SystemHealth_Label_UserFullName.Name = "Form_SystemHealth_Label_UserFullName";
            this.Form_SystemHealth_Label_UserFullName.Size = new System.Drawing.Size(100, 19);
            this.Form_SystemHealth_Label_UserFullName.TabIndex = 0;
            this.Form_SystemHealth_Label_UserFullName.Text = "User: Loading...";
            // 
            // Form_SystemHealth_TableLayout_FeedbackDetails
            // 
            this.Form_SystemHealth_TableLayout_FeedbackDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Form_SystemHealth_TableLayout_FeedbackDetails.ColumnCount = 4;
            this.Form_SystemHealth_TableLayout_FeedbackDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.Form_SystemHealth_TableLayout_FeedbackDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Form_SystemHealth_TableLayout_FeedbackDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.Form_SystemHealth_TableLayout_FeedbackDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Form_SystemHealth_TableLayout_FeedbackDetails.Controls.Add(new System.Windows.Forms.Label() { Text = "Type:", TextAlign = System.Drawing.ContentAlignment.MiddleLeft, Dock = System.Windows.Forms.DockStyle.Fill, AutoSize = true }, 0, 0);
            this.Form_SystemHealth_TableLayout_FeedbackDetails.Controls.Add(this.Form_SystemHealth_TextBox_FeedbackType, 1, 0);
            this.Form_SystemHealth_TableLayout_FeedbackDetails.Controls.Add(new System.Windows.Forms.Label() { Text = "Date:", TextAlign = System.Drawing.ContentAlignment.MiddleLeft, Dock = System.Windows.Forms.DockStyle.Fill, AutoSize = true }, 2, 0);
            this.Form_SystemHealth_TableLayout_FeedbackDetails.Controls.Add(this.Form_SystemHealth_TextBox_SubmissionDate, 3, 0);
            
            this.Form_SystemHealth_TableLayout_FeedbackDetails.Controls.Add(new System.Windows.Forms.Label() { Text = "Section:", TextAlign = System.Drawing.ContentAlignment.MiddleLeft, Dock = System.Windows.Forms.DockStyle.Fill, AutoSize = true }, 0, 1);
            this.Form_SystemHealth_TableLayout_FeedbackDetails.Controls.Add(this.Form_SystemHealth_TextBox_ActiveSection, 1, 1);
            this.Form_SystemHealth_TableLayout_FeedbackDetails.Controls.Add(new System.Windows.Forms.Label() { Text = "Category:", TextAlign = System.Drawing.ContentAlignment.MiddleLeft, Dock = System.Windows.Forms.DockStyle.Fill, AutoSize = true }, 2, 1);
            this.Form_SystemHealth_TableLayout_FeedbackDetails.Controls.Add(this.Form_SystemHealth_TextBox_Category, 3, 1);

            this.Form_SystemHealth_TableLayout_FeedbackDetails.Controls.Add(new System.Windows.Forms.Label() { Text = "Severity:", TextAlign = System.Drawing.ContentAlignment.MiddleLeft, Dock = System.Windows.Forms.DockStyle.Fill, AutoSize = true }, 0, 2);
            this.Form_SystemHealth_TableLayout_FeedbackDetails.Controls.Add(this.Form_SystemHealth_TextBox_Severity, 1, 2);
            this.Form_SystemHealth_TableLayout_FeedbackDetails.Controls.Add(new System.Windows.Forms.Label() { Text = "Priority:", TextAlign = System.Drawing.ContentAlignment.MiddleLeft, Dock = System.Windows.Forms.DockStyle.Fill, AutoSize = true }, 2, 2);
            this.Form_SystemHealth_TableLayout_FeedbackDetails.Controls.Add(this.Form_SystemHealth_TextBox_Priority, 3, 2);

            this.Form_SystemHealth_TableLayout_FeedbackDetails.Controls.Add(new System.Windows.Forms.Label() { Text = "Title:", TextAlign = System.Drawing.ContentAlignment.MiddleLeft, Dock = System.Windows.Forms.DockStyle.Fill, AutoSize = true }, 0, 3);
            this.Form_SystemHealth_TableLayout_FeedbackDetails.Controls.Add(this.Form_SystemHealth_TextBox_Title, 1, 3);
            this.Form_SystemHealth_TableLayout_FeedbackDetails.Controls.Add(new System.Windows.Forms.Label() { Text = "Status:", TextAlign = System.Drawing.ContentAlignment.MiddleLeft, Dock = System.Windows.Forms.DockStyle.Fill, AutoSize = true }, 2, 3);
            this.Form_SystemHealth_TableLayout_FeedbackDetails.Controls.Add(this.Form_SystemHealth_TextBox_Status, 3, 3);

            this.Form_SystemHealth_TableLayout_FeedbackDetails.Controls.Add(new System.Windows.Forms.Label() { Text = "Description:", TextAlign = System.Drawing.ContentAlignment.MiddleLeft, Dock = System.Windows.Forms.DockStyle.Top, AutoSize = true }, 0, 4);
            this.Form_SystemHealth_TableLayout_FeedbackDetails.Controls.Add(this.Form_SystemHealth_TextBox_Description, 1, 4);
            this.Form_SystemHealth_TableLayout_FeedbackDetails.SetColumnSpan(this.Form_SystemHealth_TextBox_Description, 3);

            this.Form_SystemHealth_TableLayout_FeedbackDetails.Location = new System.Drawing.Point(13, 50);
            this.Form_SystemHealth_TableLayout_FeedbackDetails.Name = "Form_SystemHealth_TableLayout_FeedbackDetails";
            this.Form_SystemHealth_TableLayout_FeedbackDetails.RowCount = 5;
            this.Form_SystemHealth_TableLayout_FeedbackDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.Form_SystemHealth_TableLayout_FeedbackDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.Form_SystemHealth_TableLayout_FeedbackDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.Form_SystemHealth_TableLayout_FeedbackDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.Form_SystemHealth_TableLayout_FeedbackDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Form_SystemHealth_TableLayout_FeedbackDetails.Size = new System.Drawing.Size(574, 200);
            this.Form_SystemHealth_TableLayout_FeedbackDetails.TabIndex = 1;
            // 
            // Form_SystemHealth_Button_Previous
            // 
            this.Form_SystemHealth_Button_Previous.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Form_SystemHealth_Button_Previous.Location = new System.Drawing.Point(13, 260);
            this.Form_SystemHealth_Button_Previous.Name = "Form_SystemHealth_Button_Previous";
            this.Form_SystemHealth_Button_Previous.Size = new System.Drawing.Size(75, 23);
            this.Form_SystemHealth_Button_Previous.TabIndex = 2;
            this.Form_SystemHealth_Button_Previous.Text = "< Previous";
            this.Form_SystemHealth_Button_Previous.UseVisualStyleBackColor = true;
            this.Form_SystemHealth_Button_Previous.Click += new System.EventHandler(this.Form_SystemHealth_Button_Previous_Click);
            // 
            // Form_SystemHealth_Button_Next
            // 
            this.Form_SystemHealth_Button_Next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Form_SystemHealth_Button_Next.Location = new System.Drawing.Point(512, 260);
            this.Form_SystemHealth_Button_Next.Name = "Form_SystemHealth_Button_Next";
            this.Form_SystemHealth_Button_Next.Size = new System.Drawing.Size(75, 23);
            this.Form_SystemHealth_Button_Next.TabIndex = 3;
            this.Form_SystemHealth_Button_Next.Text = "Next >";
            this.Form_SystemHealth_Button_Next.UseVisualStyleBackColor = true;
            this.Form_SystemHealth_Button_Next.Click += new System.EventHandler(this.Form_SystemHealth_Button_Next_Click);
            // 
            // Form_SystemHealth_Panel_Actions
            // 
            this.Form_SystemHealth_Panel_Actions.Controls.Add(this.Form_SystemHealth_Button_SubmitFeedback);
            this.Form_SystemHealth_Panel_Actions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Form_SystemHealth_Panel_Actions.Location = new System.Drawing.Point(0, 400);
            this.Form_SystemHealth_Panel_Actions.Name = "Form_SystemHealth_Panel_Actions";
            this.Form_SystemHealth_Panel_Actions.Size = new System.Drawing.Size(600, 60);
            this.Form_SystemHealth_Panel_Actions.TabIndex = 2;
            // 
            // Form_SystemHealth_Button_SubmitFeedback
            // 
            this.Form_SystemHealth_Button_SubmitFeedback.Location = new System.Drawing.Point(230, 15);
            this.Form_SystemHealth_Button_SubmitFeedback.Name = "Form_SystemHealth_Button_SubmitFeedback";
            this.Form_SystemHealth_Button_SubmitFeedback.Size = new System.Drawing.Size(140, 30);
            this.Form_SystemHealth_Button_SubmitFeedback.TabIndex = 0;
            this.Form_SystemHealth_Button_SubmitFeedback.Text = "Submit New Feedback";
            this.Form_SystemHealth_Button_SubmitFeedback.UseVisualStyleBackColor = true;
            this.Form_SystemHealth_Button_SubmitFeedback.Click += new System.EventHandler(this.Form_SystemHealth_Button_SubmitFeedback_Click);
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
            this.Form_SystemHealth_GroupBox_Feedback.PerformLayout();
            this.Form_SystemHealth_TableLayout_FeedbackDetails.ResumeLayout(false);
            this.Form_SystemHealth_TableLayout_FeedbackDetails.PerformLayout();
            this.Form_SystemHealth_Panel_Actions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Form_SystemHealth_Panel_HealthIndicator;
        private System.Windows.Forms.Label Form_SystemHealth_Label_HealthMessage;
        private System.Windows.Forms.Label Form_SystemHealth_Label_HealthIcon;
        private System.Windows.Forms.GroupBox Form_SystemHealth_GroupBox_Feedback;
        private System.Windows.Forms.Panel Form_SystemHealth_Panel_Actions;
        private System.Windows.Forms.Button Form_SystemHealth_Button_SubmitFeedback;
        private System.Windows.Forms.Label Form_SystemHealth_Label_UserFullName;
        private System.Windows.Forms.TableLayoutPanel Form_SystemHealth_TableLayout_FeedbackDetails;
        private System.Windows.Forms.TextBox Form_SystemHealth_TextBox_FeedbackType;
        private System.Windows.Forms.TextBox Form_SystemHealth_TextBox_SubmissionDate;
        private System.Windows.Forms.TextBox Form_SystemHealth_TextBox_ActiveSection;
        private System.Windows.Forms.TextBox Form_SystemHealth_TextBox_Category;
        private System.Windows.Forms.TextBox Form_SystemHealth_TextBox_Severity;
        private System.Windows.Forms.TextBox Form_SystemHealth_TextBox_Priority;
        private System.Windows.Forms.TextBox Form_SystemHealth_TextBox_Title;
        private System.Windows.Forms.TextBox Form_SystemHealth_TextBox_Description;
        private System.Windows.Forms.TextBox Form_SystemHealth_TextBox_Status;
        private System.Windows.Forms.Button Form_SystemHealth_Button_Previous;
        private System.Windows.Forms.Button Form_SystemHealth_Button_Next;
    }
}
