namespace MTM_WIP_Application_Winforms.Forms.ViewLogs
{
    partial class BatchGenerationReportDialog
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
            tableLayoutMain = new TableLayoutPanel();
            txtSummary = new TextBox();
            dgvResults = new DataGridView();
            colMethodName = new DataGridViewTextBoxColumn();
            colAction = new DataGridViewTextBoxColumn();
            colReason = new DataGridViewTextBoxColumn();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnClose = new Button();
            btnViewPrompts = new Button();
            lblTitle = new Label();
            lblDetails = new Label();
            tableLayoutMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResults).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutMain
            // 
            tableLayoutMain.ColumnCount = 1;
            tableLayoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutMain.Controls.Add(txtSummary, 0, 3);
            tableLayoutMain.Controls.Add(dgvResults, 0, 1);
            tableLayoutMain.Controls.Add(tableLayoutPanel1, 0, 4);
            tableLayoutMain.Controls.Add(lblTitle, 0, 0);
            tableLayoutMain.Controls.Add(lblDetails, 0, 2);
            tableLayoutMain.Dock = DockStyle.Fill;
            tableLayoutMain.Location = new Point(0, 0);
            tableLayoutMain.Margin = new Padding(0);
            tableLayoutMain.Name = "tableLayoutMain";
            tableLayoutMain.RowCount = 5;
            tableLayoutMain.RowStyles.Add(new RowStyle());
            tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutMain.RowStyles.Add(new RowStyle());
            tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutMain.RowStyles.Add(new RowStyle());
            tableLayoutMain.Size = new Size(684, 361);
            tableLayoutMain.TabIndex = 0;
            // 
            // txtSummary
            // 
            txtSummary.Dock = DockStyle.Fill;
            txtSummary.Font = new Font("Consolas", 9.75F);
            txtSummary.Location = new Point(0, 185);
            txtSummary.Margin = new Padding(0, 0, 0, 6);
            txtSummary.Multiline = true;
            txtSummary.Name = "txtSummary";
            txtSummary.ReadOnly = true;
            txtSummary.ScrollBars = ScrollBars.Vertical;
            txtSummary.Size = new Size(684, 129);
            txtSummary.TabIndex = 1;
            txtSummary.TabStop = false;
            // 
            // dgvResults
            // 
            dgvResults.AllowUserToAddRows = false;
            dgvResults.AllowUserToDeleteRows = false;
            dgvResults.AllowUserToResizeRows = false;
            dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResults.Columns.AddRange(new DataGridViewColumn[] { colMethodName, colAction, colReason });
            dgvResults.Dock = DockStyle.Fill;
            dgvResults.Location = new Point(0, 27);
            dgvResults.Margin = new Padding(0, 6, 0, 6);
            dgvResults.Name = "dgvResults";
            dgvResults.ReadOnly = true;
            dgvResults.RowHeadersVisible = false;
            dgvResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvResults.Size = new Size(684, 123);
            dgvResults.TabIndex = 3;
            dgvResults.TabStop = false;
            // 
            // colMethodName
            // 
            colMethodName.HeaderText = "Method Name";
            colMethodName.Name = "colMethodName";
            colMethodName.ReadOnly = true;
            // 
            // colAction
            // 
            colAction.HeaderText = "Action";
            colAction.Name = "colAction";
            colAction.ReadOnly = true;
            // 
            // colReason
            // 
            colReason.HeaderText = "Reason";
            colReason.Name = "colReason";
            colReason.ReadOnly = true;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Controls.Add(btnClose, 0, 0);
            tableLayoutPanel1.Controls.Add(btnViewPrompts, 2, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 323);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(678, 35);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // btnClose
            // 
            btnClose.Dock = DockStyle.Fill;
            btnClose.Location = new Point(0, 0);
            btnClose.Margin = new Padding(0);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(226, 35);
            btnClose.TabIndex = 0;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // btnViewPrompts
            // 
            btnViewPrompts.Dock = DockStyle.Fill;
            btnViewPrompts.Location = new Point(452, 0);
            btnViewPrompts.Margin = new Padding(0);
            btnViewPrompts.Name = "btnViewPrompts";
            btnViewPrompts.Size = new Size(226, 35);
            btnViewPrompts.TabIndex = 1;
            btnViewPrompts.Text = "View Created Prompts";
            btnViewPrompts.UseVisualStyleBackColor = true;
            btnViewPrompts.Click += btnViewPrompts_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.Location = new Point(0, 0);
            lblTitle.Margin = new Padding(0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(684, 21);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Batch Prompt Generation Report";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblDetails
            // 
            lblDetails.AutoSize = true;
            lblDetails.Dock = DockStyle.Fill;
            lblDetails.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            lblDetails.Location = new Point(0, 162);
            lblDetails.Margin = new Padding(0, 6, 0, 6);
            lblDetails.Name = "lblDetails";
            lblDetails.Size = new Size(684, 17);
            lblDetails.TabIndex = 2;
            lblDetails.Text = "Detailed Breakdown:";
            lblDetails.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // BatchGenerationReportDialog
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(684, 361);
            Controls.Add(tableLayoutMain);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(41, 19, 41, 19);
            MaximizeBox = false;
            MaximumSize = new Size(700, 400);
            MinimizeBox = false;
            MinimumSize = new Size(700, 400);
            Name = "BatchGenerationReportDialog";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Batch Generation Report";
            Load += BatchGenerationReportDialog_Load;
            tableLayoutMain.ResumeLayout(false);
            tableLayoutMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResults).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutMain;
        private Label lblTitle;
        private TextBox txtSummary;
        private Label lblDetails;
        private DataGridView dgvResults;
        private DataGridViewTextBoxColumn colMethodName;
        private DataGridViewTextBoxColumn colAction;
        private DataGridViewTextBoxColumn colReason;
        private Button btnViewPrompts;
        private Button btnClose;
        private TableLayoutPanel tableLayoutPanel1;
    }
}
