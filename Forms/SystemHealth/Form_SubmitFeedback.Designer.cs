namespace MTM_WIP_Application_Winforms.Forms.SystemHealth
{
    partial class Form_SubmitFeedback
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
            Form_SubmitFeedback_TableLayout_Main = new TableLayoutPanel();
            Form_SubmitFeedback_Label_Type = new Label();
            Form_SubmitFeedback_ComboBox_Type = new ComboBox();
            Form_SubmitFeedback_Label_Category = new Label();
            Form_SubmitFeedback_ComboBox_Category = new ComboBox();
            Form_SubmitFeedback_Label_Priority = new Label();
            Form_SubmitFeedback_ComboBox_Priority = new ComboBox();
            Form_SubmitFeedback_Label_Severity = new Label();
            Form_SubmitFeedback_ComboBox_Severity = new ComboBox();
            Form_SubmitFeedback_Label_Title = new Label();
            Form_SubmitFeedback_TextBox_Title = new TextBox();
            Form_SubmitFeedback_Label_Description = new Label();
            Form_SubmitFeedback_TextBox_Description = new TextBox();
            Form_SubmitFeedback_FlowPanel_Actions = new FlowLayoutPanel();
            Form_SubmitFeedback_Button_Cancel = new Button();
            Form_SubmitFeedback_Button_Submit = new Button();
            Form_SubmitFeedback_TableLayout_Main.SuspendLayout();
            Form_SubmitFeedback_FlowPanel_Actions.SuspendLayout();
            SuspendLayout();
            // 
            // Form_SubmitFeedback_TableLayout_Main
            // 
            Form_SubmitFeedback_TableLayout_Main.ColumnCount = 2;
            Form_SubmitFeedback_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            Form_SubmitFeedback_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Form_SubmitFeedback_TableLayout_Main.Controls.Add(Form_SubmitFeedback_Label_Type, 0, 0);
            Form_SubmitFeedback_TableLayout_Main.Controls.Add(Form_SubmitFeedback_ComboBox_Type, 1, 0);
            Form_SubmitFeedback_TableLayout_Main.Controls.Add(Form_SubmitFeedback_Label_Category, 0, 1);
            Form_SubmitFeedback_TableLayout_Main.Controls.Add(Form_SubmitFeedback_ComboBox_Category, 1, 1);
            Form_SubmitFeedback_TableLayout_Main.Controls.Add(Form_SubmitFeedback_Label_Priority, 0, 2);
            Form_SubmitFeedback_TableLayout_Main.Controls.Add(Form_SubmitFeedback_ComboBox_Priority, 1, 2);
            Form_SubmitFeedback_TableLayout_Main.Controls.Add(Form_SubmitFeedback_Label_Severity, 0, 3);
            Form_SubmitFeedback_TableLayout_Main.Controls.Add(Form_SubmitFeedback_ComboBox_Severity, 1, 3);
            Form_SubmitFeedback_TableLayout_Main.Controls.Add(Form_SubmitFeedback_Label_Title, 0, 4);
            Form_SubmitFeedback_TableLayout_Main.Controls.Add(Form_SubmitFeedback_TextBox_Title, 1, 4);
            Form_SubmitFeedback_TableLayout_Main.Controls.Add(Form_SubmitFeedback_Label_Description, 0, 5);
            Form_SubmitFeedback_TableLayout_Main.Controls.Add(Form_SubmitFeedback_TextBox_Description, 1, 5);
            Form_SubmitFeedback_TableLayout_Main.Controls.Add(Form_SubmitFeedback_FlowPanel_Actions, 1, 6);
            Form_SubmitFeedback_TableLayout_Main.Dock = DockStyle.Fill;
            Form_SubmitFeedback_TableLayout_Main.Location = new Point(0, 0);
            Form_SubmitFeedback_TableLayout_Main.Name = "Form_SubmitFeedback_TableLayout_Main";
            Form_SubmitFeedback_TableLayout_Main.Padding = new Padding(10);
            Form_SubmitFeedback_TableLayout_Main.RowCount = 7;
            Form_SubmitFeedback_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            Form_SubmitFeedback_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            Form_SubmitFeedback_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            Form_SubmitFeedback_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            Form_SubmitFeedback_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            Form_SubmitFeedback_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Form_SubmitFeedback_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            Form_SubmitFeedback_TableLayout_Main.Size = new Size(500, 400);
            Form_SubmitFeedback_TableLayout_Main.TabIndex = 0;
            // 
            // Form_SubmitFeedback_Label_Type
            // 
            Form_SubmitFeedback_Label_Type.Anchor = AnchorStyles.Left;
            Form_SubmitFeedback_Label_Type.AutoSize = true;
            Form_SubmitFeedback_Label_Type.Location = new Point(13, 17);
            Form_SubmitFeedback_Label_Type.Name = "Form_SubmitFeedback_Label_Type";
            Form_SubmitFeedback_Label_Type.Size = new Size(35, 15);
            Form_SubmitFeedback_Label_Type.TabIndex = 0;
            Form_SubmitFeedback_Label_Type.Text = "Type:";
            // 
            // Form_SubmitFeedback_ComboBox_Type
            // 
            Form_SubmitFeedback_ComboBox_Type.Dock = DockStyle.Fill;
            Form_SubmitFeedback_ComboBox_Type.DropDownStyle = ComboBoxStyle.DropDownList;
            Form_SubmitFeedback_ComboBox_Type.FormattingEnabled = true;
            Form_SubmitFeedback_ComboBox_Type.Location = new Point(113, 13);
            Form_SubmitFeedback_ComboBox_Type.Name = "Form_SubmitFeedback_ComboBox_Type";
            Form_SubmitFeedback_ComboBox_Type.Size = new Size(374, 23);
            Form_SubmitFeedback_ComboBox_Type.TabIndex = 1;
            // 
            // Form_SubmitFeedback_Label_Category
            // 
            Form_SubmitFeedback_Label_Category.Anchor = AnchorStyles.Left;
            Form_SubmitFeedback_Label_Category.AutoSize = true;
            Form_SubmitFeedback_Label_Category.Location = new Point(13, 47);
            Form_SubmitFeedback_Label_Category.Name = "Form_SubmitFeedback_Label_Category";
            Form_SubmitFeedback_Label_Category.Size = new Size(58, 15);
            Form_SubmitFeedback_Label_Category.TabIndex = 2;
            Form_SubmitFeedback_Label_Category.Text = "Category:";
            // 
            // Form_SubmitFeedback_ComboBox_Category
            // 
            Form_SubmitFeedback_ComboBox_Category.Dock = DockStyle.Fill;
            Form_SubmitFeedback_ComboBox_Category.DropDownStyle = ComboBoxStyle.DropDownList;
            Form_SubmitFeedback_ComboBox_Category.FormattingEnabled = true;
            Form_SubmitFeedback_ComboBox_Category.Location = new Point(113, 43);
            Form_SubmitFeedback_ComboBox_Category.Name = "Form_SubmitFeedback_ComboBox_Category";
            Form_SubmitFeedback_ComboBox_Category.Size = new Size(374, 23);
            Form_SubmitFeedback_ComboBox_Category.TabIndex = 3;
            // 
            // Form_SubmitFeedback_Label_Priority
            // 
            Form_SubmitFeedback_Label_Priority.Anchor = AnchorStyles.Left;
            Form_SubmitFeedback_Label_Priority.AutoSize = true;
            Form_SubmitFeedback_Label_Priority.Location = new Point(13, 77);
            Form_SubmitFeedback_Label_Priority.Name = "Form_SubmitFeedback_Label_Priority";
            Form_SubmitFeedback_Label_Priority.Size = new Size(48, 15);
            Form_SubmitFeedback_Label_Priority.TabIndex = 4;
            Form_SubmitFeedback_Label_Priority.Text = "Priority:";
            // 
            // Form_SubmitFeedback_ComboBox_Priority
            // 
            Form_SubmitFeedback_ComboBox_Priority.Dock = DockStyle.Fill;
            Form_SubmitFeedback_ComboBox_Priority.DropDownStyle = ComboBoxStyle.DropDownList;
            Form_SubmitFeedback_ComboBox_Priority.FormattingEnabled = true;
            Form_SubmitFeedback_ComboBox_Priority.Location = new Point(113, 73);
            Form_SubmitFeedback_ComboBox_Priority.Name = "Form_SubmitFeedback_ComboBox_Priority";
            Form_SubmitFeedback_ComboBox_Priority.Size = new Size(374, 23);
            Form_SubmitFeedback_ComboBox_Priority.TabIndex = 5;
            // 
            // Form_SubmitFeedback_Label_Severity
            // 
            Form_SubmitFeedback_Label_Severity.Anchor = AnchorStyles.Left;
            Form_SubmitFeedback_Label_Severity.AutoSize = true;
            Form_SubmitFeedback_Label_Severity.Location = new Point(13, 107);
            Form_SubmitFeedback_Label_Severity.Name = "Form_SubmitFeedback_Label_Severity";
            Form_SubmitFeedback_Label_Severity.Size = new Size(51, 15);
            Form_SubmitFeedback_Label_Severity.TabIndex = 6;
            Form_SubmitFeedback_Label_Severity.Text = "Severity:";
            // 
            // Form_SubmitFeedback_ComboBox_Severity
            // 
            Form_SubmitFeedback_ComboBox_Severity.Dock = DockStyle.Fill;
            Form_SubmitFeedback_ComboBox_Severity.DropDownStyle = ComboBoxStyle.DropDownList;
            Form_SubmitFeedback_ComboBox_Severity.FormattingEnabled = true;
            Form_SubmitFeedback_ComboBox_Severity.Location = new Point(113, 103);
            Form_SubmitFeedback_ComboBox_Severity.Name = "Form_SubmitFeedback_ComboBox_Severity";
            Form_SubmitFeedback_ComboBox_Severity.Size = new Size(374, 23);
            Form_SubmitFeedback_ComboBox_Severity.TabIndex = 7;
            // 
            // Form_SubmitFeedback_Label_Title
            // 
            Form_SubmitFeedback_Label_Title.Anchor = AnchorStyles.Left;
            Form_SubmitFeedback_Label_Title.AutoSize = true;
            Form_SubmitFeedback_Label_Title.Location = new Point(13, 137);
            Form_SubmitFeedback_Label_Title.Name = "Form_SubmitFeedback_Label_Title";
            Form_SubmitFeedback_Label_Title.Size = new Size(33, 15);
            Form_SubmitFeedback_Label_Title.TabIndex = 8;
            Form_SubmitFeedback_Label_Title.Text = "Title:";
            // 
            // Form_SubmitFeedback_TextBox_Title
            // 
            Form_SubmitFeedback_TextBox_Title.Dock = DockStyle.Fill;
            Form_SubmitFeedback_TextBox_Title.Location = new Point(113, 133);
            Form_SubmitFeedback_TextBox_Title.Name = "Form_SubmitFeedback_TextBox_Title";
            Form_SubmitFeedback_TextBox_Title.Size = new Size(374, 23);
            Form_SubmitFeedback_TextBox_Title.TabIndex = 9;
            // 
            // Form_SubmitFeedback_Label_Description
            // 
            Form_SubmitFeedback_Label_Description.Anchor = AnchorStyles.Top;
            Form_SubmitFeedback_Label_Description.AutoSize = true;
            Form_SubmitFeedback_Label_Description.Location = new Point(25, 160);
            Form_SubmitFeedback_Label_Description.Name = "Form_SubmitFeedback_Label_Description";
            Form_SubmitFeedback_Label_Description.Padding = new Padding(0, 5, 0, 0);
            Form_SubmitFeedback_Label_Description.Size = new Size(70, 20);
            Form_SubmitFeedback_Label_Description.TabIndex = 10;
            Form_SubmitFeedback_Label_Description.Text = "Description:";
            // 
            // Form_SubmitFeedback_TextBox_Description
            // 
            Form_SubmitFeedback_TextBox_Description.Dock = DockStyle.Fill;
            Form_SubmitFeedback_TextBox_Description.Location = new Point(113, 163);
            Form_SubmitFeedback_TextBox_Description.Multiline = true;
            Form_SubmitFeedback_TextBox_Description.Name = "Form_SubmitFeedback_TextBox_Description";
            Form_SubmitFeedback_TextBox_Description.ScrollBars = ScrollBars.Vertical;
            Form_SubmitFeedback_TextBox_Description.Size = new Size(374, 184);
            Form_SubmitFeedback_TextBox_Description.TabIndex = 11;
            // 
            // Form_SubmitFeedback_FlowPanel_Actions
            // 
            Form_SubmitFeedback_FlowPanel_Actions.Controls.Add(Form_SubmitFeedback_Button_Cancel);
            Form_SubmitFeedback_FlowPanel_Actions.Controls.Add(Form_SubmitFeedback_Button_Submit);
            Form_SubmitFeedback_FlowPanel_Actions.Dock = DockStyle.Fill;
            Form_SubmitFeedback_FlowPanel_Actions.FlowDirection = FlowDirection.RightToLeft;
            Form_SubmitFeedback_FlowPanel_Actions.Location = new Point(113, 353);
            Form_SubmitFeedback_FlowPanel_Actions.Name = "Form_SubmitFeedback_FlowPanel_Actions";
            Form_SubmitFeedback_FlowPanel_Actions.Size = new Size(374, 34);
            Form_SubmitFeedback_FlowPanel_Actions.TabIndex = 12;
            // 
            // Form_SubmitFeedback_Button_Cancel
            // 
            Form_SubmitFeedback_Button_Cancel.Location = new Point(296, 3);
            Form_SubmitFeedback_Button_Cancel.Name = "Form_SubmitFeedback_Button_Cancel";
            Form_SubmitFeedback_Button_Cancel.Size = new Size(75, 23);
            Form_SubmitFeedback_Button_Cancel.TabIndex = 1;
            Form_SubmitFeedback_Button_Cancel.Text = "Cancel";
            Form_SubmitFeedback_Button_Cancel.UseVisualStyleBackColor = true;
            Form_SubmitFeedback_Button_Cancel.Click += Form_SubmitFeedback_Button_Cancel_Click;
            // 
            // Form_SubmitFeedback_Button_Submit
            // 
            Form_SubmitFeedback_Button_Submit.Location = new Point(215, 3);
            Form_SubmitFeedback_Button_Submit.Name = "Form_SubmitFeedback_Button_Submit";
            Form_SubmitFeedback_Button_Submit.Size = new Size(75, 23);
            Form_SubmitFeedback_Button_Submit.TabIndex = 0;
            Form_SubmitFeedback_Button_Submit.Text = "Submit";
            Form_SubmitFeedback_Button_Submit.UseVisualStyleBackColor = true;
            Form_SubmitFeedback_Button_Submit.Click += Form_SubmitFeedback_Button_Submit_Click;
            // 
            // Form_SubmitFeedback
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(500, 400);
            Controls.Add(Form_SubmitFeedback_TableLayout_Main);
            Name = "Form_SubmitFeedback";
            Text = "Submit Feedback";
            Form_SubmitFeedback_TableLayout_Main.ResumeLayout(false);
            Form_SubmitFeedback_TableLayout_Main.PerformLayout();
            Form_SubmitFeedback_FlowPanel_Actions.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel Form_SubmitFeedback_TableLayout_Main;
        private System.Windows.Forms.Label Form_SubmitFeedback_Label_Type;
        private System.Windows.Forms.ComboBox Form_SubmitFeedback_ComboBox_Type;
        private System.Windows.Forms.Label Form_SubmitFeedback_Label_Category;
        private System.Windows.Forms.ComboBox Form_SubmitFeedback_ComboBox_Category;
        private System.Windows.Forms.Label Form_SubmitFeedback_Label_Priority;
        private System.Windows.Forms.ComboBox Form_SubmitFeedback_ComboBox_Priority;
        private System.Windows.Forms.Label Form_SubmitFeedback_Label_Severity;
        private System.Windows.Forms.ComboBox Form_SubmitFeedback_ComboBox_Severity;
        private System.Windows.Forms.Label Form_SubmitFeedback_Label_Title;
        private System.Windows.Forms.TextBox Form_SubmitFeedback_TextBox_Title;
        private System.Windows.Forms.Label Form_SubmitFeedback_Label_Description;
        private System.Windows.Forms.TextBox Form_SubmitFeedback_TextBox_Description;
        private System.Windows.Forms.FlowLayoutPanel Form_SubmitFeedback_FlowPanel_Actions;
        private System.Windows.Forms.Button Form_SubmitFeedback_Button_Cancel;
        private System.Windows.Forms.Button Form_SubmitFeedback_Button_Submit;
    }
}
