using System.Drawing;
using System.Windows.Forms;

namespace MTM_WIP_Application_Winforms.Controls.DeveloperTools
{
    partial class Control_SystemHealth
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
            this.Control_SystemHealth_TableLayout_Main = new TableLayoutPanel();
            this.Control_SystemHealth_Panel_StatusColor = new Panel();
            this.Control_SystemHealth_FlowLayout_Status = new FlowLayoutPanel();
            this.Control_SystemHealth_Label_Status = new Label();
            this.Control_SystemHealth_FlowLayout_Counts = new FlowLayoutPanel();
            this.Control_SystemHealth_Label_ErrorCount = new Label();
            this.Control_SystemHealth_Label_WarningCount = new Label();
            this.Control_SystemHealth_Label_LastCheck = new Label();
            this.Control_SystemHealth_TableLayout_Main.SuspendLayout();
            this.Control_SystemHealth_FlowLayout_Status.SuspendLayout();
            this.Control_SystemHealth_FlowLayout_Counts.SuspendLayout();
            this.SuspendLayout();
            // 
            // Control_SystemHealth_TableLayout_Main
            // 
            this.Control_SystemHealth_TableLayout_Main.ColumnCount = 3;
            this.Control_SystemHealth_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            this.Control_SystemHealth_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.Control_SystemHealth_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            this.Control_SystemHealth_TableLayout_Main.Controls.Add(this.Control_SystemHealth_Panel_StatusColor, 0, 0);
            this.Control_SystemHealth_TableLayout_Main.Controls.Add(this.Control_SystemHealth_FlowLayout_Status, 1, 0);
            this.Control_SystemHealth_TableLayout_Main.Controls.Add(this.Control_SystemHealth_Label_LastCheck, 2, 0);
            this.Control_SystemHealth_TableLayout_Main.Dock = DockStyle.Fill;
            this.Control_SystemHealth_TableLayout_Main.Location = new Point(0, 0);
            this.Control_SystemHealth_TableLayout_Main.Name = "Control_SystemHealth_TableLayout_Main";
            this.Control_SystemHealth_TableLayout_Main.RowCount = 1;
            this.Control_SystemHealth_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.Control_SystemHealth_TableLayout_Main.Size = new Size(400, 75);
            this.Control_SystemHealth_TableLayout_Main.TabIndex = 0;
            // 
            // Control_SystemHealth_Panel_StatusColor
            // 
            this.Control_SystemHealth_Panel_StatusColor.Anchor = AnchorStyles.None;
            this.Control_SystemHealth_Panel_StatusColor.BackColor = Color.ForestGreen;
            this.Control_SystemHealth_Panel_StatusColor.Location = new Point(5, 27);
            this.Control_SystemHealth_Panel_StatusColor.Name = "Control_SystemHealth_Panel_StatusColor";
            this.Control_SystemHealth_Panel_StatusColor.Size = new Size(20, 20);
            this.Control_SystemHealth_Panel_StatusColor.TabIndex = 0;
            // 
            // Control_SystemHealth_FlowLayout_Status
            // 
            this.Control_SystemHealth_FlowLayout_Status.Anchor = AnchorStyles.Left;
            this.Control_SystemHealth_FlowLayout_Status.AutoSize = true;
            this.Control_SystemHealth_FlowLayout_Status.Controls.Add(this.Control_SystemHealth_Label_Status);
            this.Control_SystemHealth_FlowLayout_Status.Controls.Add(this.Control_SystemHealth_FlowLayout_Counts);
            this.Control_SystemHealth_FlowLayout_Status.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.Control_SystemHealth_FlowLayout_Status.Location = new Point(33, 14);
            this.Control_SystemHealth_FlowLayout_Status.Name = "Control_SystemHealth_FlowLayout_Status";
            this.Control_SystemHealth_FlowLayout_Status.Size = new Size(214, 46);
            this.Control_SystemHealth_FlowLayout_Status.TabIndex = 1;
            // 
            // Control_SystemHealth_Label_Status
            // 
            this.Control_SystemHealth_Label_Status.AutoSize = true;
            this.Control_SystemHealth_Label_Status.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            this.Control_SystemHealth_Label_Status.ForeColor = Color.ForestGreen;
            this.Control_SystemHealth_Label_Status.Location = new Point(3, 0);
            this.Control_SystemHealth_Label_Status.Name = "Control_SystemHealth_Label_Status";
            this.Control_SystemHealth_Label_Status.Size = new Size(214, 21);
            this.Control_SystemHealth_Label_Status.TabIndex = 0;
            this.Control_SystemHealth_Label_Status.Text = "System Operating Normally";
            // 
            // Control_SystemHealth_FlowLayout_Counts
            // 
            this.Control_SystemHealth_FlowLayout_Counts.AutoSize = true;
            this.Control_SystemHealth_FlowLayout_Counts.Controls.Add(this.Control_SystemHealth_Label_ErrorCount);
            this.Control_SystemHealth_FlowLayout_Counts.Controls.Add(this.Control_SystemHealth_Label_WarningCount);
            this.Control_SystemHealth_FlowLayout_Counts.Location = new Point(3, 24);
            this.Control_SystemHealth_FlowLayout_Counts.Name = "Control_SystemHealth_FlowLayout_Counts";
            this.Control_SystemHealth_FlowLayout_Counts.Size = new Size(136, 19);
            this.Control_SystemHealth_FlowLayout_Counts.TabIndex = 1;
            // 
            // Control_SystemHealth_Label_ErrorCount
            // 
            this.Control_SystemHealth_Label_ErrorCount.AutoSize = true;
            this.Control_SystemHealth_Label_ErrorCount.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            this.Control_SystemHealth_Label_ErrorCount.Location = new Point(3, 0);
            this.Control_SystemHealth_Label_ErrorCount.Name = "Control_SystemHealth_Label_ErrorCount";
            this.Control_SystemHealth_Label_ErrorCount.Size = new Size(54, 17);
            this.Control_SystemHealth_Label_ErrorCount.TabIndex = 0;
            this.Control_SystemHealth_Label_ErrorCount.Text = "0 Errors";
            // 
            // Control_SystemHealth_Label_WarningCount
            // 
            this.Control_SystemHealth_Label_WarningCount.AutoSize = true;
            this.Control_SystemHealth_Label_WarningCount.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            this.Control_SystemHealth_Label_WarningCount.Location = new Point(63, 0);
            this.Control_SystemHealth_Label_WarningCount.Margin = new Padding(3, 0, 3, 0);
            this.Control_SystemHealth_Label_WarningCount.Name = "Control_SystemHealth_Label_WarningCount";
            this.Control_SystemHealth_Label_WarningCount.Size = new Size(74, 17);
            this.Control_SystemHealth_Label_WarningCount.TabIndex = 1;
            this.Control_SystemHealth_Label_WarningCount.Text = "0 Warnings";
            // 
            // Control_SystemHealth_Label_LastCheck
            // 
            this.Control_SystemHealth_Label_LastCheck.Anchor = AnchorStyles.Right;
            this.Control_SystemHealth_Label_LastCheck.AutoSize = true;
            this.Control_SystemHealth_Label_LastCheck.ForeColor = Color.Gray;
            this.Control_SystemHealth_Label_LastCheck.Location = new Point(282, 30);
            this.Control_SystemHealth_Label_LastCheck.Name = "Control_SystemHealth_Label_LastCheck";
            this.Control_SystemHealth_Label_LastCheck.Size = new Size(115, 15);
            this.Control_SystemHealth_Label_LastCheck.TabIndex = 2;
            this.Control_SystemHealth_Label_LastCheck.Text = "Last Checked: 12:00";
            // 
            // Control_SystemHealth
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.Control_SystemHealth_TableLayout_Main);
            this.Name = "Control_SystemHealth";
            this.Size = new Size(400, 75);
            this.Control_SystemHealth_TableLayout_Main.ResumeLayout(false);
            this.Control_SystemHealth_TableLayout_Main.PerformLayout();
            this.Control_SystemHealth_FlowLayout_Status.ResumeLayout(false);
            this.Control_SystemHealth_FlowLayout_Status.PerformLayout();
            this.Control_SystemHealth_FlowLayout_Counts.ResumeLayout(false);
            this.Control_SystemHealth_FlowLayout_Counts.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel Control_SystemHealth_TableLayout_Main;
        private FlowLayoutPanel Control_SystemHealth_FlowLayout_Status;
        private FlowLayoutPanel Control_SystemHealth_FlowLayout_Counts;
        private Panel Control_SystemHealth_Panel_StatusColor;
        private Label Control_SystemHealth_Label_Status;
        private Label Control_SystemHealth_Label_ErrorCount;
        private Label Control_SystemHealth_Label_WarningCount;
        private Label Control_SystemHealth_Label_LastCheck;
    }
}



