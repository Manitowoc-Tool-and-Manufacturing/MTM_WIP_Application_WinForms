namespace MTM_WIP_Application_Winforms.Forms.ViewLogs
{
    partial class PromptStatusManagerDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tlpMain = new TableLayoutPanel();
            dgvPromptStatuses = new DataGridView();
            pnlButtons = new Panel();
            btnClose = new Button();
            btnSave = new Button();
            btnRefresh = new Button();
            lblStatus = new Label();
            tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPromptStatuses).BeginInit();
            pnlButtons.SuspendLayout();
            SuspendLayout();
            // 
            // tlpMain
            // 
            tlpMain.ColumnCount = 1;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpMain.Controls.Add(dgvPromptStatuses, 0, 0);
            tlpMain.Controls.Add(pnlButtons, 0, 1);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Location = new Point(0, 0);
            tlpMain.Name = "tlpMain";
            tlpMain.Padding = new Padding(10);
            tlpMain.RowCount = 2;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tlpMain.Size = new Size(684, 361);
            tlpMain.TabIndex = 0;
            // 
            // dgvPromptStatuses
            // 
            dgvPromptStatuses.AllowUserToAddRows = false;
            dgvPromptStatuses.AllowUserToDeleteRows = false;
            dgvPromptStatuses.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPromptStatuses.Dock = DockStyle.Fill;
            dgvPromptStatuses.Location = new Point(13, 13);
            dgvPromptStatuses.MultiSelect = false;
            dgvPromptStatuses.Name = "dgvPromptStatuses";
            dgvPromptStatuses.RowHeadersVisible = false;
            dgvPromptStatuses.RowHeadersWidth = 51;
            dgvPromptStatuses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPromptStatuses.Size = new Size(658, 285);
            dgvPromptStatuses.TabIndex = 0;
            // 
            // pnlButtons
            // 
            pnlButtons.Controls.Add(btnClose);
            pnlButtons.Controls.Add(btnSave);
            pnlButtons.Controls.Add(btnRefresh);
            pnlButtons.Controls.Add(lblStatus);
            pnlButtons.Dock = DockStyle.Fill;
            pnlButtons.Location = new Point(13, 304);
            pnlButtons.Name = "pnlButtons";
            pnlButtons.Size = new Size(658, 44);
            pnlButtons.TabIndex = 1;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.Location = new Point(558, 6);
            btnClose.MinimumSize = new Size(90, 32);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(90, 32);
            btnClose.TabIndex = 3;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSave.Enabled = false;
            btnSave.Location = new Point(462, 6);
            btnSave.MinimumSize = new Size(90, 32);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(90, 32);
            btnSave.TabIndex = 2;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRefresh.Location = new Point(366, 6);
            btnRefresh.MinimumSize = new Size(90, 32);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(90, 32);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(10, 13);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(39, 15);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "Ready";
            // 
            // PromptStatusManagerDialog
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(684, 361);
            Controls.Add(tlpMain);
            MaximizeBox = false;
            MaximumSize = new Size(700, 400);
            MinimizeBox = false;
            MinimumSize = new Size(700, 400);
            Name = "PromptStatusManagerDialog";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Manage Prompt Status";
            tlpMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPromptStatuses).EndInit();
            pnlButtons.ResumeLayout(false);
            pnlButtons.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.DataGridView dgvPromptStatuses;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblStatus;
    }
}
