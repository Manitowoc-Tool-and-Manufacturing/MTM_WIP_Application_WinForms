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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnhancedErrorDialog));
            tableLayoutMain = new TableLayoutPanel();
            tabControlDetails = new TabControl();
            tabPageSummary = new TabPage();
            labelPlainEnglish = new Label();
            pictureBoxIcon = new PictureBox();
            tabPageTechnical = new TabPage();
            richTextBoxTechnical = new RichTextBox();
            tabPageCallStack = new TabPage();
            treeViewCallStack = new TreeView();
            tableLayoutButtons = new TableLayoutPanel();
            buttonRetry = new Button();
            buttonCopyDetails = new Button();
            buttonReportIssue = new Button();
            buttonViewLogs = new Button();
            buttonClose = new Button();
            statusStrip = new StatusStrip();
            toolStripStatusLabel = new ToolStripStatusLabel();
            tableLayoutMain.SuspendLayout();
            tabControlDetails.SuspendLayout();
            tabPageSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxIcon).BeginInit();
            tabPageTechnical.SuspendLayout();
            tabPageCallStack.SuspendLayout();
            tableLayoutButtons.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutMain
            // 
            tableLayoutMain.AutoSize = true;
            tableLayoutMain.ColumnCount = 1;
            tableLayoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutMain.Controls.Add(tabControlDetails, 0, 0);
            tableLayoutMain.Controls.Add(tableLayoutButtons, 0, 1);
            tableLayoutMain.Controls.Add(statusStrip, 0, 2);
            tableLayoutMain.Dock = DockStyle.Fill;
            tableLayoutMain.Location = new Point(0, 0);
            tableLayoutMain.Name = "tableLayoutMain";
            tableLayoutMain.Padding = new Padding(8);
            tableLayoutMain.RowCount = 3;
            tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutMain.RowStyles.Add(new RowStyle());
            tableLayoutMain.RowStyles.Add(new RowStyle());
            tableLayoutMain.Size = new Size(544, 361);
            tableLayoutMain.TabIndex = 0;
            // 
            // tabControlDetails
            // 
            tabControlDetails.Controls.Add(tabPageSummary);
            tabControlDetails.Controls.Add(tabPageTechnical);
            tabControlDetails.Controls.Add(tabPageCallStack);
            tabControlDetails.Dock = DockStyle.Fill;
            tabControlDetails.Location = new Point(11, 11);
            tabControlDetails.Name = "tabControlDetails";
            tabControlDetails.SelectedIndex = 0;
            tabControlDetails.Size = new Size(522, 277);
            tabControlDetails.TabIndex = 0;
            // 
            // tabPageSummary
            // 
            tabPageSummary.BackColor = Color.White;
            tabPageSummary.Controls.Add(labelPlainEnglish);
            tabPageSummary.Controls.Add(pictureBoxIcon);
            tabPageSummary.Location = new Point(4, 24);
            tabPageSummary.Name = "tabPageSummary";
            tabPageSummary.Padding = new Padding(15);
            tabPageSummary.Size = new Size(514, 249);
            tabPageSummary.TabIndex = 0;
            tabPageSummary.Text = "Summary";
            // 
            // labelPlainEnglish
            // 
            labelPlainEnglish.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            labelPlainEnglish.Font = new Font("Segoe UI Emoji", 10F);
            labelPlainEnglish.Location = new Point(78, 15);
            labelPlainEnglish.Name = "labelPlainEnglish";
            labelPlainEnglish.Size = new Size(421, 227);
            labelPlainEnglish.TabIndex = 1;
            labelPlainEnglish.Text = "Error description will appear here.";
            // 
            // pictureBoxIcon
            // 
            pictureBoxIcon.Location = new Point(15, 15);
            pictureBoxIcon.Name = "pictureBoxIcon";
            pictureBoxIcon.Size = new Size(48, 48);
            pictureBoxIcon.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBoxIcon.TabIndex = 0;
            pictureBoxIcon.TabStop = false;
            // 
            // tabPageTechnical
            // 
            tabPageTechnical.Controls.Add(richTextBoxTechnical);
            tabPageTechnical.Location = new Point(4, 24);
            tabPageTechnical.Name = "tabPageTechnical";
            tabPageTechnical.Padding = new Padding(8);
            tabPageTechnical.Size = new Size(514, 249);
            tabPageTechnical.TabIndex = 1;
            tabPageTechnical.Text = "Technical Details";
            tabPageTechnical.UseVisualStyleBackColor = true;
            // 
            // richTextBoxTechnical
            // 
            richTextBoxTechnical.BackColor = Color.FromArgb(248, 249, 250);
            richTextBoxTechnical.BorderStyle = BorderStyle.None;
            richTextBoxTechnical.Dock = DockStyle.Fill;
            richTextBoxTechnical.Font = new Font("Consolas", 9F);
            richTextBoxTechnical.Location = new Point(8, 8);
            richTextBoxTechnical.Name = "richTextBoxTechnical";
            richTextBoxTechnical.ReadOnly = true;
            richTextBoxTechnical.Size = new Size(498, 233);
            richTextBoxTechnical.TabIndex = 0;
            richTextBoxTechnical.Text = "";
            // 
            // tabPageCallStack
            // 
            tabPageCallStack.Controls.Add(treeViewCallStack);
            tabPageCallStack.Location = new Point(4, 24);
            tabPageCallStack.Name = "tabPageCallStack";
            tabPageCallStack.Padding = new Padding(8);
            tabPageCallStack.Size = new Size(514, 249);
            tabPageCallStack.TabIndex = 2;
            tabPageCallStack.Text = "Call Stack";
            tabPageCallStack.UseVisualStyleBackColor = true;
            // 
            // treeViewCallStack
            // 
            treeViewCallStack.BorderStyle = BorderStyle.None;
            treeViewCallStack.Dock = DockStyle.Fill;
            treeViewCallStack.Font = new Font("Segoe UI Emoji", 9F);
            treeViewCallStack.Location = new Point(8, 8);
            treeViewCallStack.Name = "treeViewCallStack";
            treeViewCallStack.Size = new Size(498, 233);
            treeViewCallStack.TabIndex = 0;
            // 
            // tableLayoutButtons
            // 
            tableLayoutButtons.AutoSize = true;
            tableLayoutButtons.ColumnCount = 5;
            tableLayoutButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tableLayoutButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
            tableLayoutButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutButtons.Controls.Add(buttonRetry, 0, 0);
            tableLayoutButtons.Controls.Add(buttonCopyDetails, 1, 0);
            tableLayoutButtons.Controls.Add(buttonReportIssue, 2, 0);
            tableLayoutButtons.Controls.Add(buttonViewLogs, 3, 0);
            tableLayoutButtons.Controls.Add(buttonClose, 4, 0);
            tableLayoutButtons.Dock = DockStyle.Fill;
            tableLayoutButtons.Location = new Point(11, 294);
            tableLayoutButtons.Name = "tableLayoutButtons";
            tableLayoutButtons.RowCount = 1;
            tableLayoutButtons.RowStyles.Add(new RowStyle());
            tableLayoutButtons.Size = new Size(522, 34);
            tableLayoutButtons.TabIndex = 1;
            // 
            // buttonRetry
            // 
            buttonRetry.BackColor = Color.FromArgb(76, 175, 80);
            buttonRetry.Dock = DockStyle.Fill;
            buttonRetry.FlatAppearance.BorderSize = 0;
            buttonRetry.FlatStyle = FlatStyle.Flat;
            buttonRetry.Font = new Font("Segoe UI Emoji", 9F);
            buttonRetry.ForeColor = Color.White;
            buttonRetry.Location = new Point(3, 3);
            buttonRetry.Name = "buttonRetry";
            buttonRetry.Size = new Size(176, 28);
            buttonRetry.TabIndex = 0;
            buttonRetry.Text = "Retry Operation";
            buttonRetry.UseVisualStyleBackColor = false;
            // 
            // buttonCopyDetails
            // 
            buttonCopyDetails.BackColor = Color.FromArgb(33, 150, 243);
            buttonCopyDetails.Dock = DockStyle.Fill;
            buttonCopyDetails.FlatAppearance.BorderSize = 0;
            buttonCopyDetails.FlatStyle = FlatStyle.Flat;
            buttonCopyDetails.Font = new Font("Segoe UI Emoji", 9F);
            buttonCopyDetails.ForeColor = Color.White;
            buttonCopyDetails.Location = new Point(185, 3);
            buttonCopyDetails.Name = "buttonCopyDetails";
            buttonCopyDetails.Size = new Size(98, 28);
            buttonCopyDetails.TabIndex = 1;
            buttonCopyDetails.Text = "Copy Details";
            buttonCopyDetails.UseVisualStyleBackColor = false;
            // 
            // buttonReportIssue
            // 
            buttonReportIssue.BackColor = Color.FromArgb(255, 152, 0);
            buttonReportIssue.Dock = DockStyle.Fill;
            buttonReportIssue.FlatAppearance.BorderSize = 0;
            buttonReportIssue.FlatStyle = FlatStyle.Flat;
            buttonReportIssue.Font = new Font("Segoe UI Emoji", 9F);
            buttonReportIssue.ForeColor = Color.White;
            buttonReportIssue.Location = new Point(289, 3);
            buttonReportIssue.Name = "buttonReportIssue";
            buttonReportIssue.Size = new Size(98, 28);
            buttonReportIssue.TabIndex = 2;
            buttonReportIssue.Text = "Report Issue";
            buttonReportIssue.UseVisualStyleBackColor = false;
            // 
            // buttonViewLogs
            // 
            buttonViewLogs.BackColor = Color.FromArgb(96, 125, 139);
            buttonViewLogs.Dock = DockStyle.Fill;
            buttonViewLogs.FlatAppearance.BorderSize = 0;
            buttonViewLogs.FlatStyle = FlatStyle.Flat;
            buttonViewLogs.Font = new Font("Segoe UI Emoji", 9F);
            buttonViewLogs.ForeColor = Color.White;
            buttonViewLogs.Location = new Point(393, 3);
            buttonViewLogs.Name = "buttonViewLogs";
            buttonViewLogs.Size = new Size(72, 28);
            buttonViewLogs.TabIndex = 3;
            buttonViewLogs.Text = "View Logs";
            buttonViewLogs.UseVisualStyleBackColor = false;
            buttonViewLogs.Visible = false;
            // 
            // buttonClose
            // 
            buttonClose.BackColor = Color.FromArgb(117, 117, 117);
            buttonClose.Dock = DockStyle.Fill;
            buttonClose.FlatAppearance.BorderSize = 0;
            buttonClose.FlatStyle = FlatStyle.Flat;
            buttonClose.Font = new Font("Segoe UI Emoji", 9F);
            buttonClose.ForeColor = Color.White;
            buttonClose.Location = new Point(471, 3);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(48, 28);
            buttonClose.TabIndex = 4;
            buttonClose.Text = "Close";
            buttonClose.UseVisualStyleBackColor = false;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel });
            statusStrip.Location = new Point(8, 331);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(528, 22);
            statusStrip.TabIndex = 2;
            statusStrip.Text = "statusStrip";
            // 
            // toolStripStatusLabel
            // 
            toolStripStatusLabel.Name = "toolStripStatusLabel";
            toolStripStatusLabel.Size = new Size(119, 17);
            toolStripStatusLabel.Text = "🔴 Connection Status";
            // 
            // EnhancedErrorDialog
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(544, 361);
            Controls.Add(tableLayoutMain);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MaximumSize = new Size(560, 400);
            MinimizeBox = false;
            MinimumSize = new Size(560, 400);
            Name = "EnhancedErrorDialog";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MTM Inventory Application - Enhanced Error Dialog";
            tableLayoutMain.ResumeLayout(false);
            tableLayoutMain.PerformLayout();
            tabControlDetails.ResumeLayout(false);
            tabPageSummary.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBoxIcon).EndInit();
            tabPageTechnical.ResumeLayout(false);
            tabPageCallStack.ResumeLayout(false);
            tableLayoutButtons.ResumeLayout(false);
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

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