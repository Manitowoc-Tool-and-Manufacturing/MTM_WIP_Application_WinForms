namespace MTM_WIP_Application_Winforms.Forms.ErrorDialog
{
    partial class EnhancedErrorDialog
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
            this.tableLayoutMain = new System.Windows.Forms.TableLayoutPanel();
            this.tabControlDetails = new System.Windows.Forms.TabControl();
            this.tabPageSummary = new System.Windows.Forms.TabPage();
            this.labelPlainEnglish = new System.Windows.Forms.Label();
            this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
            this.tabPageTechnical = new System.Windows.Forms.TabPage();
            this.richTextBoxTechnical = new System.Windows.Forms.RichTextBox();
            this.tabPageCallStack = new System.Windows.Forms.TabPage();
            this.treeViewCallStack = new System.Windows.Forms.TreeView();
            this.tableLayoutButtons = new System.Windows.Forms.TableLayoutPanel();
            this.buttonRetry = new System.Windows.Forms.Button();
            this.buttonCopyDetails = new System.Windows.Forms.Button();
            this.buttonReportIssue = new System.Windows.Forms.Button();
            this.buttonViewLogs = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutMain.SuspendLayout();
            this.tabControlDetails.SuspendLayout();
            this.tabPageSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
            this.tabPageTechnical.SuspendLayout();
            this.tabPageCallStack.SuspendLayout();
            this.tableLayoutButtons.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutMain
            // 
            this.tableLayoutMain.AutoSize = true;
            this.tableLayoutMain.ColumnCount = 1;
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutMain.Controls.Add(this.tabControlDetails, 0, 0);
            this.tableLayoutMain.Controls.Add(this.tableLayoutButtons, 0, 1);
            this.tableLayoutMain.Controls.Add(this.statusStrip, 0, 2);
            this.tableLayoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutMain.Name = "tableLayoutMain";
            this.tableLayoutMain.Padding = new System.Windows.Forms.Padding(8);
            this.tableLayoutMain.RowCount = 3;
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutMain.Size = new System.Drawing.Size(560, 400);
            this.tableLayoutMain.TabIndex = 0;
            // 
            // tabControlDetails
            // 
            this.tabControlDetails.Controls.Add(this.tabPageSummary);
            this.tabControlDetails.Controls.Add(this.tabPageTechnical);
            this.tabControlDetails.Controls.Add(this.tabPageCallStack);
            this.tabControlDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlDetails.Location = new System.Drawing.Point(11, 11);
            this.tabControlDetails.Name = "tabControlDetails";
            this.tabControlDetails.SelectedIndex = 0;
            this.tabControlDetails.Size = new System.Drawing.Size(538, 324);
            this.tabControlDetails.TabIndex = 0;
            // 
            // tabPageSummary
            // 
            this.tabPageSummary.Controls.Add(this.labelPlainEnglish);
            this.tabPageSummary.Controls.Add(this.pictureBoxIcon);
            this.tabPageSummary.BackColor = System.Drawing.Color.White;
            this.tabPageSummary.Location = new System.Drawing.Point(4, 24);
            this.tabPageSummary.Name = "tabPageSummary";
            this.tabPageSummary.Padding = new System.Windows.Forms.Padding(15);
            this.tabPageSummary.Size = new System.Drawing.Size(530, 296);
            this.tabPageSummary.TabIndex = 0;
            this.tabPageSummary.Text = "Summary";
            this.tabPageSummary.UseVisualStyleBackColor = false;
            // 
            // labelPlainEnglish
            // 
            this.labelPlainEnglish.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPlainEnglish.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelPlainEnglish.Location = new System.Drawing.Point(78, 15);
            this.labelPlainEnglish.Name = "labelPlainEnglish";
            this.labelPlainEnglish.Size = new System.Drawing.Size(437, 266);
            this.labelPlainEnglish.TabIndex = 1;
            this.labelPlainEnglish.Text = "Error description will appear here.";
            this.labelPlainEnglish.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // pictureBoxIcon
            // 
            this.pictureBoxIcon.Location = new System.Drawing.Point(15, 15);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.pictureBoxIcon.Size = new System.Drawing.Size(48, 48);
            this.pictureBoxIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxIcon.TabIndex = 0;
            this.pictureBoxIcon.TabStop = false;
            // 
            // tabPageTechnical
            // 
            this.tabPageTechnical.Controls.Add(this.richTextBoxTechnical);
            this.tabPageTechnical.Location = new System.Drawing.Point(4, 24);
            this.tabPageTechnical.Name = "tabPageTechnical";
            this.tabPageTechnical.Padding = new System.Windows.Forms.Padding(8);
            this.tabPageTechnical.Size = new System.Drawing.Size(530, 296);
            this.tabPageTechnical.TabIndex = 1;
            this.tabPageTechnical.Text = "Technical Details";
            this.tabPageTechnical.UseVisualStyleBackColor = true;
            // 
            // richTextBoxTechnical
            // 
            this.richTextBoxTechnical.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.richTextBoxTechnical.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxTechnical.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxTechnical.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.richTextBoxTechnical.Location = new System.Drawing.Point(8, 8);
            this.richTextBoxTechnical.Name = "richTextBoxTechnical";
            this.richTextBoxTechnical.ReadOnly = true;
            this.richTextBoxTechnical.Size = new System.Drawing.Size(514, 280);
            this.richTextBoxTechnical.TabIndex = 0;
            this.richTextBoxTechnical.Text = "";
            // 
            // tabPageCallStack
            // 
            this.tabPageCallStack.Controls.Add(this.treeViewCallStack);
            this.tabPageCallStack.Location = new System.Drawing.Point(4, 24);
            this.tabPageCallStack.Name = "tabPageCallStack";
            this.tabPageCallStack.Padding = new System.Windows.Forms.Padding(8);
            this.tabPageCallStack.Size = new System.Drawing.Size(530, 296);
            this.tabPageCallStack.TabIndex = 2;
            this.tabPageCallStack.Text = "Call Stack";
            this.tabPageCallStack.UseVisualStyleBackColor = true;
            // 
            // treeViewCallStack
            // 
            this.treeViewCallStack.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeViewCallStack.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewCallStack.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.treeViewCallStack.Location = new System.Drawing.Point(8, 8);
            this.treeViewCallStack.Name = "treeViewCallStack";
            this.treeViewCallStack.Size = new System.Drawing.Size(514, 280);
            this.treeViewCallStack.TabIndex = 0;
            // 
            // tableLayoutButtons
            // 
            this.tableLayoutButtons.AutoSize = true;
            this.tableLayoutButtons.ColumnCount = 5;
            this.tableLayoutButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutButtons.Controls.Add(this.buttonRetry, 0, 0);
            this.tableLayoutButtons.Controls.Add(this.buttonCopyDetails, 1, 0);
            this.tableLayoutButtons.Controls.Add(this.buttonReportIssue, 2, 0);
            this.tableLayoutButtons.Controls.Add(this.buttonViewLogs, 3, 0);
            this.tableLayoutButtons.Controls.Add(this.buttonClose, 4, 0);
            this.tableLayoutButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutButtons.Location = new System.Drawing.Point(11, 341);
            this.tableLayoutButtons.Name = "tableLayoutButtons";
            this.tableLayoutButtons.RowCount = 1;
            this.tableLayoutButtons.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutButtons.Size = new System.Drawing.Size(538, 34);
            this.tableLayoutButtons.TabIndex = 1;
            // 
            // buttonRetry
            // 
            this.buttonRetry.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.buttonRetry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonRetry.FlatAppearance.BorderSize = 0;
            this.buttonRetry.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRetry.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonRetry.ForeColor = System.Drawing.Color.White;
            this.buttonRetry.Location = new System.Drawing.Point(3, 3);
            this.buttonRetry.Name = "buttonRetry";
            this.buttonRetry.Size = new System.Drawing.Size(182, 28);
            this.buttonRetry.TabIndex = 0;
            this.buttonRetry.Text = "Retry Operation";
            this.buttonRetry.UseVisualStyleBackColor = false;
            // 
            // buttonCopyDetails
            // 
            this.buttonCopyDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.buttonCopyDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCopyDetails.FlatAppearance.BorderSize = 0;
            this.buttonCopyDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCopyDetails.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonCopyDetails.ForeColor = System.Drawing.Color.White;
            this.buttonCopyDetails.Location = new System.Drawing.Point(191, 3);
            this.buttonCopyDetails.Name = "buttonCopyDetails";
            this.buttonCopyDetails.Size = new System.Drawing.Size(101, 28);
            this.buttonCopyDetails.TabIndex = 1;
            this.buttonCopyDetails.Text = "Copy Details";
            this.buttonCopyDetails.UseVisualStyleBackColor = false;
            // 
            // buttonReportIssue
            // 
            this.buttonReportIssue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.buttonReportIssue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonReportIssue.FlatAppearance.BorderSize = 0;
            this.buttonReportIssue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonReportIssue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonReportIssue.ForeColor = System.Drawing.Color.White;
            this.buttonReportIssue.Location = new System.Drawing.Point(298, 3);
            this.buttonReportIssue.Name = "buttonReportIssue";
            this.buttonReportIssue.Size = new System.Drawing.Size(101, 28);
            this.buttonReportIssue.TabIndex = 2;
            this.buttonReportIssue.Text = "Report Issue";
            this.buttonReportIssue.UseVisualStyleBackColor = false;
            // 
            // buttonViewLogs
            // 
            this.buttonViewLogs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(125)))), ((int)(((byte)(139)))));
            this.buttonViewLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonViewLogs.FlatAppearance.BorderSize = 0;
            this.buttonViewLogs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonViewLogs.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonViewLogs.ForeColor = System.Drawing.Color.White;
            this.buttonViewLogs.Location = new System.Drawing.Point(405, 3);
            this.buttonViewLogs.Name = "buttonViewLogs";
            this.buttonViewLogs.Size = new System.Drawing.Size(74, 28);
            this.buttonViewLogs.TabIndex = 3;
            this.buttonViewLogs.Text = "View Logs";
            this.buttonViewLogs.UseVisualStyleBackColor = false;
            // 
            // buttonClose
            // 
            this.buttonClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(117)))), ((int)(((byte)(117)))));
            this.buttonClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonClose.FlatAppearance.BorderSize = 0;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonClose.ForeColor = System.Drawing.Color.White;
            this.buttonClose.Location = new System.Drawing.Point(485, 3);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(50, 28);
            this.buttonClose.TabIndex = 4;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = false;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(11, 378);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(538, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel.Text = "🔴 Connection Status";
            // 
            // EnhancedErrorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(560, 400);
            this.MaximumSize = new System.Drawing.Size(560, 400);
            this.MinimumSize = new System.Drawing.Size(560, 400);
            this.Controls.Add(this.tableLayoutMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(560, 400);
            this.Name = "EnhancedErrorDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MTM Inventory Application - Enhanced Error Dialog";
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.tableLayoutMain.ResumeLayout(false);
            this.tableLayoutMain.PerformLayout();
            this.tabControlDetails.ResumeLayout(false);
            this.tabPageSummary.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
            this.tabPageTechnical.ResumeLayout(false);
            this.tabPageCallStack.ResumeLayout(false);
            this.tableLayoutButtons.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutMain;
        private System.Windows.Forms.TabControl tabControlDetails;
        private System.Windows.Forms.TabPage tabPageSummary;
        private System.Windows.Forms.Label labelPlainEnglish;
        private System.Windows.Forms.PictureBox pictureBoxIcon;
        private System.Windows.Forms.TabPage tabPageTechnical;
        private System.Windows.Forms.RichTextBox richTextBoxTechnical;
        private System.Windows.Forms.TabPage tabPageCallStack;
        private System.Windows.Forms.TreeView treeViewCallStack;
        private System.Windows.Forms.TableLayoutPanel tableLayoutButtons;
        private System.Windows.Forms.Button buttonRetry;
        private System.Windows.Forms.Button buttonCopyDetails;
        private System.Windows.Forms.Button buttonReportIssue;
        private System.Windows.Forms.Button buttonViewLogs;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
    }
}