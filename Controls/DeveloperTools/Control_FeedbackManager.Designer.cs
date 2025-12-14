using System.Drawing;
using System.Windows.Forms;

namespace MTM_WIP_Application_Winforms.Controls.DeveloperTools
{
    partial class Control_FeedbackManager
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        #region Fields

        private System.ComponentModel.IContainer components = null;

        #endregion

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        #region Methods

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Control_FeedbackManager_Panel_Controls = new Panel();
            this.Control_FeedbackManager_Label_Count = new Label();
            this.Control_FeedbackManager_Button_Refresh = new Button();
            this.Control_FeedbackManager_ComboBox_Status = new ComboBox();
            this.Control_FeedbackManager_Label_Status = new Label();
            this.Control_FeedbackManager_Label_Type = new Label();
            this.Control_FeedbackManager_ComboBox_Type = new ComboBox();
            this.Control_FeedbackManager_Label_Date = new Label();
            this.Control_FeedbackManager_DateTimePicker_Start = new DateTimePicker();
            this.Control_FeedbackManager_DateTimePicker_End = new DateTimePicker();
            this.Control_FeedbackManager_Button_MarkNew = new Button();
            this.Control_FeedbackManager_Button_MarkOpen = new Button();
            this.Control_FeedbackManager_Button_MarkInProgress = new Button();
            this.Control_FeedbackManager_Button_MarkResolved = new Button();
            this.Control_FeedbackManager_Button_MarkClosed = new Button();
            this.Control_FeedbackManager_DataGridView_Feedback = new DataGridView();
            this.Control_FeedbackManager_Panel_Controls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Control_FeedbackManager_DataGridView_Feedback)).BeginInit();
            this.SuspendLayout();
            // 
            // Control_FeedbackManager_Panel_Controls
            // 
            this.Control_FeedbackManager_Panel_Controls.Controls.Add(this.Control_FeedbackManager_Label_Count);
            this.Control_FeedbackManager_Panel_Controls.Controls.Add(this.Control_FeedbackManager_Button_MarkNew);
            this.Control_FeedbackManager_Panel_Controls.Controls.Add(this.Control_FeedbackManager_Button_MarkOpen);
            this.Control_FeedbackManager_Panel_Controls.Controls.Add(this.Control_FeedbackManager_Button_MarkInProgress);
            this.Control_FeedbackManager_Panel_Controls.Controls.Add(this.Control_FeedbackManager_Button_MarkResolved);
            this.Control_FeedbackManager_Panel_Controls.Controls.Add(this.Control_FeedbackManager_Button_MarkClosed);
            this.Control_FeedbackManager_Panel_Controls.Controls.Add(this.Control_FeedbackManager_Button_Refresh);
            this.Control_FeedbackManager_Panel_Controls.Controls.Add(this.Control_FeedbackManager_DateTimePicker_End);
            this.Control_FeedbackManager_Panel_Controls.Controls.Add(this.Control_FeedbackManager_DateTimePicker_Start);
            this.Control_FeedbackManager_Panel_Controls.Controls.Add(this.Control_FeedbackManager_Label_Date);
            this.Control_FeedbackManager_Panel_Controls.Controls.Add(this.Control_FeedbackManager_ComboBox_Type);
            this.Control_FeedbackManager_Panel_Controls.Controls.Add(this.Control_FeedbackManager_Label_Type);
            this.Control_FeedbackManager_Panel_Controls.Controls.Add(this.Control_FeedbackManager_ComboBox_Status);
            this.Control_FeedbackManager_Panel_Controls.Controls.Add(this.Control_FeedbackManager_Label_Status);
            this.Control_FeedbackManager_Panel_Controls.Dock = DockStyle.Top;
            this.Control_FeedbackManager_Panel_Controls.Location = new Point(0, 0);
            this.Control_FeedbackManager_Panel_Controls.Name = "Control_FeedbackManager_Panel_Controls";
            this.Control_FeedbackManager_Panel_Controls.Size = new Size(1000, 80);
            this.Control_FeedbackManager_Panel_Controls.TabIndex = 0;
            // 
            // Control_FeedbackManager_Label_Count
            // 
            this.Control_FeedbackManager_Label_Count.AutoSize = true;
            this.Control_FeedbackManager_Label_Count.Location = new Point(750, 18);
            this.Control_FeedbackManager_Label_Count.Name = "Control_FeedbackManager_Label_Count";
            this.Control_FeedbackManager_Label_Count.Size = new Size(12, 15);
            this.Control_FeedbackManager_Label_Count.TabIndex = 3;
            this.Control_FeedbackManager_Label_Count.Text = "-";
            // 
            // Control_FeedbackManager_Label_Type
            // 
            this.Control_FeedbackManager_Label_Type.AutoSize = true;
            this.Control_FeedbackManager_Label_Type.Location = new Point(165, 17);
            this.Control_FeedbackManager_Label_Type.Name = "Control_FeedbackManager_Label_Type";
            this.Control_FeedbackManager_Label_Type.Size = new Size(34, 15);
            this.Control_FeedbackManager_Label_Type.TabIndex = 4;
            this.Control_FeedbackManager_Label_Type.Text = "Type:";
            // 
            // Control_FeedbackManager_ComboBox_Type
            // 
            this.Control_FeedbackManager_ComboBox_Type.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Control_FeedbackManager_ComboBox_Type.FormattingEnabled = true;
            this.Control_FeedbackManager_ComboBox_Type.Location = new Point(200, 14);
            this.Control_FeedbackManager_ComboBox_Type.Name = "Control_FeedbackManager_ComboBox_Type";
            this.Control_FeedbackManager_ComboBox_Type.Size = new Size(100, 23);
            this.Control_FeedbackManager_ComboBox_Type.TabIndex = 5;
            // 
            // Control_FeedbackManager_Label_Date
            // 
            this.Control_FeedbackManager_Label_Date.AutoSize = true;
            this.Control_FeedbackManager_Label_Date.Location = new Point(310, 17);
            this.Control_FeedbackManager_Label_Date.Name = "Control_FeedbackManager_Label_Date";
            this.Control_FeedbackManager_Label_Date.Size = new Size(34, 15);
            this.Control_FeedbackManager_Label_Date.TabIndex = 6;
            this.Control_FeedbackManager_Label_Date.Text = "Date:";
            // 
            // Control_FeedbackManager_DateTimePicker_Start
            // 
            this.Control_FeedbackManager_DateTimePicker_Start.Format = DateTimePickerFormat.Short;
            this.Control_FeedbackManager_DateTimePicker_Start.Location = new Point(345, 14);
            this.Control_FeedbackManager_DateTimePicker_Start.Name = "Control_FeedbackManager_DateTimePicker_Start";
            this.Control_FeedbackManager_DateTimePicker_Start.Size = new Size(100, 23);
            this.Control_FeedbackManager_DateTimePicker_Start.TabIndex = 7;
            this.Control_FeedbackManager_DateTimePicker_Start.ShowCheckBox = true;
            this.Control_FeedbackManager_DateTimePicker_Start.Checked = false;
            // 
            // Control_FeedbackManager_DateTimePicker_End
            // 
            this.Control_FeedbackManager_DateTimePicker_End.Format = DateTimePickerFormat.Short;
            this.Control_FeedbackManager_DateTimePicker_End.Location = new Point(450, 14);
            this.Control_FeedbackManager_DateTimePicker_End.Name = "Control_FeedbackManager_DateTimePicker_End";
            this.Control_FeedbackManager_DateTimePicker_End.Size = new Size(100, 23);
            this.Control_FeedbackManager_DateTimePicker_End.TabIndex = 8;
            this.Control_FeedbackManager_DateTimePicker_End.ShowCheckBox = true;
            this.Control_FeedbackManager_DateTimePicker_End.Checked = false;
            // 
            // Control_FeedbackManager_Button_MarkNew
            // 
            this.Control_FeedbackManager_Button_MarkNew.Location = new Point(13, 45);
            this.Control_FeedbackManager_Button_MarkNew.Name = "Control_FeedbackManager_Button_MarkNew";
            this.Control_FeedbackManager_Button_MarkNew.Size = new Size(80, 23);
            this.Control_FeedbackManager_Button_MarkNew.TabIndex = 9;
            this.Control_FeedbackManager_Button_MarkNew.Text = "Mark New";
            this.Control_FeedbackManager_Button_MarkNew.UseVisualStyleBackColor = true;
            // 
            // Control_FeedbackManager_Button_MarkOpen
            // 
            this.Control_FeedbackManager_Button_MarkOpen.Location = new Point(99, 45);
            this.Control_FeedbackManager_Button_MarkOpen.Name = "Control_FeedbackManager_Button_MarkOpen";
            this.Control_FeedbackManager_Button_MarkOpen.Size = new Size(80, 23);
            this.Control_FeedbackManager_Button_MarkOpen.TabIndex = 10;
            this.Control_FeedbackManager_Button_MarkOpen.Text = "Mark Open";
            this.Control_FeedbackManager_Button_MarkOpen.UseVisualStyleBackColor = true;
            // 
            // Control_FeedbackManager_Button_MarkInProgress
            // 
            this.Control_FeedbackManager_Button_MarkInProgress.Location = new Point(185, 45);
            this.Control_FeedbackManager_Button_MarkInProgress.Name = "Control_FeedbackManager_Button_MarkInProgress";
            this.Control_FeedbackManager_Button_MarkInProgress.Size = new Size(100, 23);
            this.Control_FeedbackManager_Button_MarkInProgress.TabIndex = 11;
            this.Control_FeedbackManager_Button_MarkInProgress.Text = "Mark In Progress";
            this.Control_FeedbackManager_Button_MarkInProgress.UseVisualStyleBackColor = true;
            // 
            // Control_FeedbackManager_Button_MarkResolved
            // 
            this.Control_FeedbackManager_Button_MarkResolved.Location = new Point(291, 45);
            this.Control_FeedbackManager_Button_MarkResolved.Name = "Control_FeedbackManager_Button_MarkResolved";
            this.Control_FeedbackManager_Button_MarkResolved.Size = new Size(100, 23);
            this.Control_FeedbackManager_Button_MarkResolved.TabIndex = 12;
            this.Control_FeedbackManager_Button_MarkResolved.Text = "Mark Resolved";
            this.Control_FeedbackManager_Button_MarkResolved.UseVisualStyleBackColor = true;
            // 
            // Control_FeedbackManager_Button_MarkClosed
            // 
            this.Control_FeedbackManager_Button_MarkClosed.Location = new Point(397, 45);
            this.Control_FeedbackManager_Button_MarkClosed.Name = "Control_FeedbackManager_Button_MarkClosed";
            this.Control_FeedbackManager_Button_MarkClosed.Size = new Size(80, 23);
            this.Control_FeedbackManager_Button_MarkClosed.TabIndex = 13;
            this.Control_FeedbackManager_Button_MarkClosed.Text = "Mark Closed";
            this.Control_FeedbackManager_Button_MarkClosed.UseVisualStyleBackColor = true;
            // 
            // Control_FeedbackManager_Button_Refresh
            // 
            this.Control_FeedbackManager_Button_Refresh.Location = new Point(560, 14);
            this.Control_FeedbackManager_Button_Refresh.Name = "Control_FeedbackManager_Button_Refresh";
            this.Control_FeedbackManager_Button_Refresh.Size = new Size(75, 23);
            this.Control_FeedbackManager_Button_Refresh.TabIndex = 2;
            this.Control_FeedbackManager_Button_Refresh.Text = "Refresh";
            this.Control_FeedbackManager_Button_Refresh.UseVisualStyleBackColor = true;
            // 
            // Control_FeedbackManager_ComboBox_Status
            // 
            this.Control_FeedbackManager_ComboBox_Status.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Control_FeedbackManager_ComboBox_Status.FormattingEnabled = true;
            this.Control_FeedbackManager_ComboBox_Status.Location = new Point(60, 14);
            this.Control_FeedbackManager_ComboBox_Status.Name = "Control_FeedbackManager_ComboBox_Status";
            this.Control_FeedbackManager_ComboBox_Status.Size = new Size(121, 23);
            this.Control_FeedbackManager_ComboBox_Status.TabIndex = 1;
            // 
            // Control_FeedbackManager_Label_Status
            // 
            this.Control_FeedbackManager_Label_Status.AutoSize = true;
            this.Control_FeedbackManager_Label_Status.Location = new Point(13, 17);
            this.Control_FeedbackManager_Label_Status.Name = "Control_FeedbackManager_Label_Status";
            this.Control_FeedbackManager_Label_Status.Size = new Size(42, 15);
            this.Control_FeedbackManager_Label_Status.TabIndex = 0;
            this.Control_FeedbackManager_Label_Status.Text = "Status:";
            // 
            // Control_FeedbackManager_DataGridView_Feedback
            // 
            this.Control_FeedbackManager_DataGridView_Feedback.AllowUserToAddRows = false;
            this.Control_FeedbackManager_DataGridView_Feedback.AllowUserToDeleteRows = false;
            this.Control_FeedbackManager_DataGridView_Feedback.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Control_FeedbackManager_DataGridView_Feedback.Dock = DockStyle.Fill;
            this.Control_FeedbackManager_DataGridView_Feedback.Location = new Point(0, 80);
            this.Control_FeedbackManager_DataGridView_Feedback.Name = "Control_FeedbackManager_DataGridView_Feedback";
            this.Control_FeedbackManager_DataGridView_Feedback.ReadOnly = true;
            this.Control_FeedbackManager_DataGridView_Feedback.RowTemplate.Height = 25;
            this.Control_FeedbackManager_DataGridView_Feedback.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.Control_FeedbackManager_DataGridView_Feedback.Size = new Size(800, 450);
            this.Control_FeedbackManager_DataGridView_Feedback.TabIndex = 1;
            // 
            // Control_FeedbackManager
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.Control_FeedbackManager_DataGridView_Feedback);
            this.Controls.Add(this.Control_FeedbackManager_Panel_Controls);
            this.Name = "Control_FeedbackManager";
            this.Size = new Size(800, 500);
            this.Control_FeedbackManager_Panel_Controls.ResumeLayout(false);
            this.Control_FeedbackManager_Panel_Controls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Control_FeedbackManager_DataGridView_Feedback)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel Control_FeedbackManager_Panel_Controls;
        private Label Control_FeedbackManager_Label_Status;
        private ComboBox Control_FeedbackManager_ComboBox_Status;
        private Button Control_FeedbackManager_Button_Refresh;
        private Label Control_FeedbackManager_Label_Count;
        private DataGridView Control_FeedbackManager_DataGridView_Feedback;
        private Label Control_FeedbackManager_Label_Type;
        private ComboBox Control_FeedbackManager_ComboBox_Type;
        private Label Control_FeedbackManager_Label_Date;
        private DateTimePicker Control_FeedbackManager_DateTimePicker_Start;
        private DateTimePicker Control_FeedbackManager_DateTimePicker_End;
        private Button Control_FeedbackManager_Button_MarkNew;
        private Button Control_FeedbackManager_Button_MarkOpen;
        private Button Control_FeedbackManager_Button_MarkInProgress;
        private Button Control_FeedbackManager_Button_MarkResolved;
        private Button Control_FeedbackManager_Button_MarkClosed;
    }
}



