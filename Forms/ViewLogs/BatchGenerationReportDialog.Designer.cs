namespace MTM_Inventory_Application.Forms.ViewLogs
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
            lblTitle = new Label();
            txtSummary = new TextBox();
            lblDetails = new Label();
            dgvResults = new DataGridView();
            colMethodName = new DataGridViewTextBoxColumn();
            colAction = new DataGridViewTextBoxColumn();
            colReason = new DataGridViewTextBoxColumn();
            flowLayoutButtons = new FlowLayoutPanel();
            btnViewPrompts = new Button();
            btnClose = new Button();
            tableLayoutMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResults).BeginInit();
            flowLayoutButtons.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutMain
            // 
            tableLayoutMain.ColumnCount = 1;
            tableLayoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutMain.Controls.Add(lblTitle, 0, 0);
            tableLayoutMain.Controls.Add(txtSummary, 0, 1);
            tableLayoutMain.Controls.Add(lblDetails, 0, 2);
            tableLayoutMain.Controls.Add(dgvResults, 0, 3);
            tableLayoutMain.Controls.Add(flowLayoutButtons, 0, 4);
            tableLayoutMain.Dock = DockStyle.Fill;
            tableLayoutMain.Location = new Point(0, 0);
            tableLayoutMain.Name = "tableLayoutMain";
            tableLayoutMain.Padding = new Padding(10);
            tableLayoutMain.RowCount = 5;
            tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 150F));
            tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutMain.Size = new Size(700, 550);
            tableLayoutMain.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.Location = new Point(13, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(674, 35);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Batch Prompt Generation Report";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtSummary
            // 
            txtSummary.Dock = DockStyle.Fill;
            txtSummary.Font = new Font("Consolas", 9.75F);
            txtSummary.Location = new Point(13, 48);
            txtSummary.Margin = new Padding(3, 3, 3, 5);
            txtSummary.Multiline = true;
            txtSummary.Name = "txtSummary";
            txtSummary.ReadOnly = true;
            txtSummary.ScrollBars = ScrollBars.Vertical;
            txtSummary.Size = new Size(674, 142);
            txtSummary.TabIndex = 1;
            txtSummary.TabStop = false;
            // 
            // lblDetails
            // 
            lblDetails.AutoSize = true;
            lblDetails.Dock = DockStyle.Fill;
            lblDetails.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            lblDetails.Location = new Point(13, 195);
            lblDetails.Name = "lblDetails";
            lblDetails.Size = new Size(674, 30);
            lblDetails.TabIndex = 2;
            lblDetails.Text = "Detailed Breakdown:";
            lblDetails.TextAlign = ContentAlignment.MiddleLeft;
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
            dgvResults.Location = new Point(13, 228);
            dgvResults.Margin = new Padding(3, 3, 3, 5);
            dgvResults.Name = "dgvResults";
            dgvResults.ReadOnly = true;
            dgvResults.RowHeadersVisible = false;
            dgvResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvResults.Size = new Size(674, 257);
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
            // flowLayoutButtons
            // 
            flowLayoutButtons.Controls.Add(btnClose);
            flowLayoutButtons.Controls.Add(btnViewPrompts);
            flowLayoutButtons.Dock = DockStyle.Fill;
            flowLayoutButtons.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutButtons.Location = new Point(13, 493);
            flowLayoutButtons.Name = "flowLayoutButtons";
            flowLayoutButtons.Padding = new Padding(0, 5, 0, 0);
            flowLayoutButtons.Size = new Size(674, 44);
            flowLayoutButtons.TabIndex = 4;
            // 
            // btnViewPrompts
            // 
            btnViewPrompts.Location = new Point(457, 8);
            btnViewPrompts.MinimumSize = new Size(100, 30);
            btnViewPrompts.Name = "btnViewPrompts";
            btnViewPrompts.Size = new Size(150, 30);
            btnViewPrompts.TabIndex = 1;
            btnViewPrompts.Text = "View Created Prompts";
            btnViewPrompts.UseVisualStyleBackColor = true;
            btnViewPrompts.Click += btnViewPrompts_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new Point(613, 8);
            btnClose.MinimumSize = new Size(100, 30);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(100, 30);
            btnClose.TabIndex = 0;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // BatchGenerationReportDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(700, 550);
            Controls.Add(tableLayoutMain);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(700, 550);
            Name = "BatchGenerationReportDialog";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Batch Generation Report";
            Load += BatchGenerationReportDialog_Load;
            tableLayoutMain.ResumeLayout(false);
            tableLayoutMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResults).EndInit();
            flowLayoutButtons.ResumeLayout(false);
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
        private FlowLayoutPanel flowLayoutButtons;
        private Button btnViewPrompts;
        private Button btnClose;
    }
}
