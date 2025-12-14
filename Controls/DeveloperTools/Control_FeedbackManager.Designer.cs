namespace MTM_WIP_Application_Winforms.Controls.DeveloperTools
{
    partial class Control_FeedbackManager
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
            this.Control_FeedbackManager_Panel_Controls = new System.Windows.Forms.Panel();
            this.Control_FeedbackManager_Label_Count = new System.Windows.Forms.Label();
            this.Control_FeedbackManager_Button_Refresh = new System.Windows.Forms.Button();
            this.Control_FeedbackManager_ComboBox_Status = new System.Windows.Forms.ComboBox();
            this.Control_FeedbackManager_Label_Status = new System.Windows.Forms.Label();
            this.Control_FeedbackManager_DataGridView_Feedback = new System.Windows.Forms.DataGridView();
            this.Control_FeedbackManager_Panel_Controls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Control_FeedbackManager_DataGridView_Feedback)).BeginInit();
            this.SuspendLayout();
            // 
            // Control_FeedbackManager_Panel_Controls
            // 
            this.Control_FeedbackManager_Panel_Controls.Controls.Add(this.Control_FeedbackManager_Label_Count);
            this.Control_FeedbackManager_Panel_Controls.Controls.Add(this.Control_FeedbackManager_Button_Refresh);
            this.Control_FeedbackManager_Panel_Controls.Controls.Add(this.Control_FeedbackManager_ComboBox_Status);
            this.Control_FeedbackManager_Panel_Controls.Controls.Add(this.Control_FeedbackManager_Label_Status);
            this.Control_FeedbackManager_Panel_Controls.Dock = System.Windows.Forms.DockStyle.Top;
            this.Control_FeedbackManager_Panel_Controls.Location = new System.Drawing.Point(0, 0);
            this.Control_FeedbackManager_Panel_Controls.Name = "Control_FeedbackManager_Panel_Controls";
            this.Control_FeedbackManager_Panel_Controls.Size = new System.Drawing.Size(800, 50);
            this.Control_FeedbackManager_Panel_Controls.TabIndex = 0;
            // 
            // Control_FeedbackManager_Label_Count
            // 
            this.Control_FeedbackManager_Label_Count.AutoSize = true;
            this.Control_FeedbackManager_Label_Count.Location = new System.Drawing.Point(300, 18);
            this.Control_FeedbackManager_Label_Count.Name = "Control_FeedbackManager_Label_Count";
            this.Control_FeedbackManager_Label_Count.Size = new System.Drawing.Size(12, 15);
            this.Control_FeedbackManager_Label_Count.TabIndex = 3;
            this.Control_FeedbackManager_Label_Count.Text = "-";
            // 
            // Control_FeedbackManager_Button_Refresh
            // 
            this.Control_FeedbackManager_Button_Refresh.Location = new System.Drawing.Point(200, 14);
            this.Control_FeedbackManager_Button_Refresh.Name = "Control_FeedbackManager_Button_Refresh";
            this.Control_FeedbackManager_Button_Refresh.Size = new System.Drawing.Size(75, 23);
            this.Control_FeedbackManager_Button_Refresh.TabIndex = 2;
            this.Control_FeedbackManager_Button_Refresh.Text = "Refresh";
            this.Control_FeedbackManager_Button_Refresh.UseVisualStyleBackColor = true;
            // 
            // Control_FeedbackManager_ComboBox_Status
            // 
            this.Control_FeedbackManager_ComboBox_Status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Control_FeedbackManager_ComboBox_Status.FormattingEnabled = true;
            this.Control_FeedbackManager_ComboBox_Status.Location = new System.Drawing.Point(60, 14);
            this.Control_FeedbackManager_ComboBox_Status.Name = "Control_FeedbackManager_ComboBox_Status";
            this.Control_FeedbackManager_ComboBox_Status.Size = new System.Drawing.Size(121, 23);
            this.Control_FeedbackManager_ComboBox_Status.TabIndex = 1;
            // 
            // Control_FeedbackManager_Label_Status
            // 
            this.Control_FeedbackManager_Label_Status.AutoSize = true;
            this.Control_FeedbackManager_Label_Status.Location = new System.Drawing.Point(13, 17);
            this.Control_FeedbackManager_Label_Status.Name = "Control_FeedbackManager_Label_Status";
            this.Control_FeedbackManager_Label_Status.Size = new System.Drawing.Size(42, 15);
            this.Control_FeedbackManager_Label_Status.TabIndex = 0;
            this.Control_FeedbackManager_Label_Status.Text = "Status:";
            // 
            // Control_FeedbackManager_DataGridView_Feedback
            // 
            this.Control_FeedbackManager_DataGridView_Feedback.AllowUserToAddRows = false;
            this.Control_FeedbackManager_DataGridView_Feedback.AllowUserToDeleteRows = false;
            this.Control_FeedbackManager_DataGridView_Feedback.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Control_FeedbackManager_DataGridView_Feedback.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Control_FeedbackManager_DataGridView_Feedback.Location = new System.Drawing.Point(0, 50);
            this.Control_FeedbackManager_DataGridView_Feedback.Name = "Control_FeedbackManager_DataGridView_Feedback";
            this.Control_FeedbackManager_DataGridView_Feedback.ReadOnly = true;
            this.Control_FeedbackManager_DataGridView_Feedback.RowTemplate.Height = 25;
            this.Control_FeedbackManager_DataGridView_Feedback.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Control_FeedbackManager_DataGridView_Feedback.Size = new System.Drawing.Size(800, 450);
            this.Control_FeedbackManager_DataGridView_Feedback.TabIndex = 1;
            // 
            // Control_FeedbackManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Control_FeedbackManager_DataGridView_Feedback);
            this.Controls.Add(this.Control_FeedbackManager_Panel_Controls);
            this.Name = "Control_FeedbackManager";
            this.Size = new System.Drawing.Size(800, 500);
            this.Control_FeedbackManager_Panel_Controls.ResumeLayout(false);
            this.Control_FeedbackManager_Panel_Controls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Control_FeedbackManager_DataGridView_Feedback)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Control_FeedbackManager_Panel_Controls;
        private System.Windows.Forms.Label Control_FeedbackManager_Label_Status;
        private System.Windows.Forms.ComboBox Control_FeedbackManager_ComboBox_Status;
        private System.Windows.Forms.Button Control_FeedbackManager_Button_Refresh;
        private System.Windows.Forms.Label Control_FeedbackManager_Label_Count;
        private System.Windows.Forms.DataGridView Control_FeedbackManager_DataGridView_Feedback;
    }
}

