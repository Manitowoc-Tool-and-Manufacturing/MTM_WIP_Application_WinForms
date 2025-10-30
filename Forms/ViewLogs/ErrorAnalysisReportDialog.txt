namespace MTM_WIP_Application_Winforms.Forms.ViewLogs
{
    partial class ErrorAnalysisReportDialog
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
            panelBottom = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnCancel = new Button();
            btnClose = new Button();
            btnRefresh = new Button();
            btnCopyToClipboard = new Button();
            btnExportHtml = new Button();
            btnExportCsv = new Button();
            panelReport = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            txtReport = new TextBox();
            progressBar = new ProgressBar();
            lblStatus = new Label();
            panelTop = new Panel();
            lblTitle = new Label();
            tableLayoutMain.SuspendLayout();
            panelBottom.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panelReport.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panelTop.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutMain
            // 
            tableLayoutMain.ColumnCount = 1;
            tableLayoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutMain.Controls.Add(panelBottom, 0, 2);
            tableLayoutMain.Controls.Add(panelReport, 0, 1);
            tableLayoutMain.Controls.Add(panelTop, 0, 0);
            tableLayoutMain.Dock = DockStyle.Fill;
            tableLayoutMain.Location = new Point(6, 6);
            tableLayoutMain.Margin = new Padding(0);
            tableLayoutMain.Name = "tableLayoutMain";
            tableLayoutMain.RowCount = 3;
            tableLayoutMain.RowStyles.Add(new RowStyle());
            tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutMain.RowStyles.Add(new RowStyle());
            tableLayoutMain.Size = new Size(672, 349);
            tableLayoutMain.TabIndex = 0;
            // 
            // panelBottom
            // 
            panelBottom.Controls.Add(tableLayoutPanel1);
            panelBottom.Dock = DockStyle.Fill;
            panelBottom.Location = new Point(0, 283);
            panelBottom.Margin = new Padding(0);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(672, 66);
            panelBottom.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Controls.Add(btnCancel, 0, 0);
            tableLayoutPanel1.Controls.Add(btnClose, 2, 0);
            tableLayoutPanel1.Controls.Add(btnRefresh, 1, 0);
            tableLayoutPanel1.Controls.Add(btnCopyToClipboard, 2, 1);
            tableLayoutPanel1.Controls.Add(btnExportHtml, 0, 1);
            tableLayoutPanel1.Controls.Add(btnExportCsv, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(672, 66);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // btnCancel
            // 
            btnCancel.Dock = DockStyle.Fill;
            btnCancel.Enabled = false;
            btnCancel.Location = new Point(0, 0);
            btnCancel.Margin = new Padding(0);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(224, 33);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += BtnCancel_Click;
            // 
            // btnClose
            // 
            btnClose.Dock = DockStyle.Fill;
            btnClose.Location = new Point(448, 0);
            btnClose.Margin = new Padding(0);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(224, 33);
            btnClose.TabIndex = 7;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += BtnClose_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Dock = DockStyle.Fill;
            btnRefresh.Location = new Point(224, 0);
            btnRefresh.Margin = new Padding(0);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(224, 33);
            btnRefresh.TabIndex = 5;
            btnRefresh.Text = "Refresh";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += BtnRefresh_Click;
            // 
            // btnCopyToClipboard
            // 
            btnCopyToClipboard.Dock = DockStyle.Fill;
            btnCopyToClipboard.Location = new Point(448, 33);
            btnCopyToClipboard.Margin = new Padding(0);
            btnCopyToClipboard.Name = "btnCopyToClipboard";
            btnCopyToClipboard.Size = new Size(224, 33);
            btnCopyToClipboard.TabIndex = 4;
            btnCopyToClipboard.Text = "Copy to Clipboard";
            btnCopyToClipboard.UseVisualStyleBackColor = true;
            btnCopyToClipboard.Click += BtnCopyToClipboard_Click;
            // 
            // btnExportHtml
            // 
            btnExportHtml.Dock = DockStyle.Fill;
            btnExportHtml.Location = new Point(0, 33);
            btnExportHtml.Margin = new Padding(0);
            btnExportHtml.Name = "btnExportHtml";
            btnExportHtml.Size = new Size(224, 33);
            btnExportHtml.TabIndex = 2;
            btnExportHtml.Text = "Export HTML";
            btnExportHtml.UseVisualStyleBackColor = true;
            btnExportHtml.Click += BtnExportHtml_Click;
            // 
            // btnExportCsv
            // 
            btnExportCsv.Dock = DockStyle.Fill;
            btnExportCsv.Location = new Point(224, 33);
            btnExportCsv.Margin = new Padding(0);
            btnExportCsv.Name = "btnExportCsv";
            btnExportCsv.Size = new Size(224, 33);
            btnExportCsv.TabIndex = 3;
            btnExportCsv.Text = "Export CSV (Zip)";
            btnExportCsv.UseVisualStyleBackColor = true;
            btnExportCsv.Click += BtnExportCsv_Click;
            // 
            // panelReport
            // 
            panelReport.Controls.Add(tableLayoutPanel2);
            panelReport.Dock = DockStyle.Fill;
            panelReport.Location = new Point(0, 28);
            panelReport.Margin = new Padding(0);
            panelReport.Name = "panelReport";
            panelReport.Size = new Size(672, 255);
            panelReport.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(txtReport, 0, 0);
            tableLayoutPanel2.Controls.Add(progressBar, 0, 1);
            tableLayoutPanel2.Controls.Add(lblStatus, 1, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(672, 255);
            tableLayoutPanel2.TabIndex = 2;
            // 
            // txtReport
            // 
            txtReport.BackColor = Color.White;
            tableLayoutPanel2.SetColumnSpan(txtReport, 2);
            txtReport.Dock = DockStyle.Fill;
            txtReport.Font = new Font("Consolas", 9F);
            txtReport.Location = new Point(0, 0);
            txtReport.Margin = new Padding(0);
            txtReport.Multiline = true;
            txtReport.Name = "txtReport";
            txtReport.ReadOnly = true;
            txtReport.ScrollBars = ScrollBars.Both;
            txtReport.Size = new Size(672, 228);
            txtReport.TabIndex = 0;
            txtReport.WordWrap = false;
            // 
            // progressBar
            // 
            progressBar.Dock = DockStyle.Fill;
            progressBar.Location = new Point(6, 234);
            progressBar.Margin = new Padding(6);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(609, 15);
            progressBar.TabIndex = 0;
            progressBar.Visible = false;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Dock = DockStyle.Fill;
            lblStatus.Location = new Point(627, 234);
            lblStatus.Margin = new Padding(6);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(39, 15);
            lblStatus.TabIndex = 1;
            lblStatus.Text = "Ready";
            lblStatus.TextAlign = ContentAlignment.MiddleRight;
            // 
            // panelTop
            // 
            panelTop.Controls.Add(lblTitle);
            panelTop.Dock = DockStyle.Fill;
            panelTop.Location = new Point(0, 0);
            panelTop.Margin = new Padding(0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(672, 28);
            panelTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(0, 0);
            lblTitle.Margin = new Padding(0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(672, 28);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Error Analysis Report";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ErrorAnalysisReportDialog
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(684, 361);
            Controls.Add(tableLayoutMain);
            Margin = new Padding(41, 19, 41, 19);
            Name = "ErrorAnalysisReportDialog";
            Padding = new Padding(6);
            StartPosition = FormStartPosition.CenterParent;
            Text = "Error Analysis Report";
            Load += ErrorAnalysisReportDialog_Load;
            tableLayoutMain.ResumeLayout(false);
            panelBottom.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            panelReport.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            panelTop.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutMain;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelReport;
        private System.Windows.Forms.TextBox txtReport;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnExportHtml;
        private System.Windows.Forms.Button btnExportCsv;
        private System.Windows.Forms.Button btnCopyToClipboard;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnClose;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
    }
}
