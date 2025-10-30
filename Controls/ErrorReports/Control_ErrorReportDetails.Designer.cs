using System.Drawing;
using System.Windows.Forms;

namespace MTM_WIP_Application_Winforms.Controls.ErrorReports
{
    partial class Control_ErrorReportDetails
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tableLayoutMain = new TableLayoutPanel();
            lblHeader = new Label();
            tableLayoutMetadata = new TableLayoutPanel();
            lblReportId = new Label();
            txtReportId = new TextBox();
            lblReportDate = new Label();
            txtReportDate = new TextBox();
            lblUserName = new Label();
            txtUserName = new TextBox();
            lblMachineName = new Label();
            txtMachineName = new TextBox();
            lblAppVersion = new Label();
            txtAppVersion = new TextBox();
            lblErrorType = new Label();
            txtErrorType = new TextBox();
            lblStatus = new Label();
            txtStatus = new TextBox();
            lblReviewedBy = new Label();
            txtReviewedBy = new TextBox();
            lblReviewedDate = new Label();
            txtReviewedDate = new TextBox();
            grpErrorSummary = new GroupBox();
            txtErrorSummary = new TextBox();
            grpUserNotes = new GroupBox();
            txtUserNotes = new TextBox();
            tableLayoutDetails = new TableLayoutPanel();
            grpDeveloperNotes = new GroupBox();
            txtDeveloperNotes = new TextBox();
            grpTechnicalDetails = new GroupBox();
            txtTechnicalDetails = new RichTextBox();
            grpCallStack = new GroupBox();
            txtCallStack = new RichTextBox();
            flowButtons = new FlowLayoutPanel();
            btnExportReport = new Button();
            btnCopyAll = new Button();
            btnMarkResolved = new Button();
            btnMarkReviewed = new Button();
            tableLayoutMain.SuspendLayout();
            tableLayoutMetadata.SuspendLayout();
            grpErrorSummary.SuspendLayout();
            grpUserNotes.SuspendLayout();
            tableLayoutDetails.SuspendLayout();
            grpDeveloperNotes.SuspendLayout();
            grpTechnicalDetails.SuspendLayout();
            grpCallStack.SuspendLayout();
            flowButtons.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutMain
            // 
            tableLayoutMain.ColumnCount = 1;
            tableLayoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutMain.Controls.Add(lblHeader, 0, 0);
            tableLayoutMain.Controls.Add(tableLayoutMetadata, 0, 1);
            tableLayoutMain.Controls.Add(grpErrorSummary, 0, 2);
            tableLayoutMain.Controls.Add(grpUserNotes, 0, 3);
            tableLayoutMain.Controls.Add(tableLayoutDetails, 0, 4);
            tableLayoutMain.Controls.Add(flowButtons, 0, 5);
            tableLayoutMain.Dock = DockStyle.Fill;
            tableLayoutMain.Location = new Point(0, 0);
            tableLayoutMain.Margin = new Padding(4, 3, 4, 3);
            tableLayoutMain.Name = "tableLayoutMain";
            tableLayoutMain.RowCount = 6;
            tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F));
            tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 150F));
            tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutMain.Size = new Size(960, 680);
            tableLayoutMain.TabIndex = 0;
            // 
            // lblHeader
            // 
            lblHeader.AutoSize = true;
            lblHeader.Dock = DockStyle.Fill;
            lblHeader.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblHeader.Location = new Point(4, 0);
            lblHeader.Margin = new Padding(4, 0, 4, 8);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(952, 21);
            lblHeader.TabIndex = 0;
            lblHeader.Text = "Error Report Details";
            // 
            // tableLayoutMetadata
            // 
            tableLayoutMetadata.ColumnCount = 4;
            tableLayoutMetadata.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tableLayoutMetadata.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutMetadata.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tableLayoutMetadata.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutMetadata.Controls.Add(lblReportId, 0, 0);
            tableLayoutMetadata.Controls.Add(txtReportId, 1, 0);
            tableLayoutMetadata.Controls.Add(lblReportDate, 2, 0);
            tableLayoutMetadata.Controls.Add(txtReportDate, 3, 0);
            tableLayoutMetadata.Controls.Add(lblUserName, 0, 1);
            tableLayoutMetadata.Controls.Add(txtUserName, 1, 1);
            tableLayoutMetadata.Controls.Add(lblMachineName, 2, 1);
            tableLayoutMetadata.Controls.Add(txtMachineName, 3, 1);
            tableLayoutMetadata.Controls.Add(lblAppVersion, 0, 2);
            tableLayoutMetadata.Controls.Add(txtAppVersion, 1, 2);
            tableLayoutMetadata.Controls.Add(lblErrorType, 2, 2);
            tableLayoutMetadata.Controls.Add(txtErrorType, 3, 2);
            tableLayoutMetadata.Controls.Add(lblStatus, 0, 3);
            tableLayoutMetadata.Controls.Add(txtStatus, 1, 3);
            tableLayoutMetadata.Controls.Add(lblReviewedBy, 2, 3);
            tableLayoutMetadata.Controls.Add(txtReviewedBy, 3, 3);
            tableLayoutMetadata.Controls.Add(lblReviewedDate, 0, 4);
            tableLayoutMetadata.Controls.Add(txtReviewedDate, 1, 4);
            tableLayoutMetadata.Dock = DockStyle.Fill;
            tableLayoutMetadata.Location = new Point(4, 29);
            tableLayoutMetadata.Margin = new Padding(4, 0, 4, 8);
            tableLayoutMetadata.Name = "tableLayoutMetadata";
            tableLayoutMetadata.RowCount = 5;
            tableLayoutMetadata.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutMetadata.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutMetadata.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutMetadata.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutMetadata.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutMetadata.Size = new Size(952, 140);
            tableLayoutMetadata.TabIndex = 1;
            // 
            // lblReportId
            // 
            lblReportId.Anchor = AnchorStyles.Left;
            lblReportId.AutoSize = true;
            lblReportId.Margin = new Padding(0, 0, 6, 6);
            lblReportId.Name = "lblReportId";
            lblReportId.Size = new Size(63, 15);
            lblReportId.Text = "Report ID:";
            // 
            // txtReportId
            // 
            txtReportId.BackColor = Color.White;
            txtReportId.BorderStyle = BorderStyle.FixedSingle;
            txtReportId.Dock = DockStyle.Fill;
            txtReportId.Margin = new Padding(0, 0, 12, 6);
            txtReportId.Name = "txtReportId";
            txtReportId.ReadOnly = true;
            txtReportId.TabStop = false;
            // 
            // lblReportDate
            // 
            lblReportDate.Anchor = AnchorStyles.Left;
            lblReportDate.AutoSize = true;
            lblReportDate.Margin = new Padding(0, 0, 6, 6);
            lblReportDate.Name = "lblReportDate";
            lblReportDate.Size = new Size(75, 15);
            lblReportDate.Text = "Date / Time:";
            // 
            // txtReportDate
            // 
            txtReportDate.BackColor = Color.White;
            txtReportDate.BorderStyle = BorderStyle.FixedSingle;
            txtReportDate.Dock = DockStyle.Fill;
            txtReportDate.Margin = new Padding(0, 0, 0, 6);
            txtReportDate.Name = "txtReportDate";
            txtReportDate.ReadOnly = true;
            txtReportDate.TabStop = false;
            // 
            // lblUserName
            // 
            lblUserName.Anchor = AnchorStyles.Left;
            lblUserName.AutoSize = true;
            lblUserName.Margin = new Padding(0, 0, 6, 6);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(68, 15);
            lblUserName.Text = "User Name:";
            // 
            // txtUserName
            // 
            txtUserName.BackColor = Color.White;
            txtUserName.BorderStyle = BorderStyle.FixedSingle;
            txtUserName.Dock = DockStyle.Fill;
            txtUserName.Margin = new Padding(0, 0, 12, 6);
            txtUserName.Name = "txtUserName";
            txtUserName.ReadOnly = true;
            txtUserName.TabStop = false;
            // 
            // lblMachineName
            // 
            lblMachineName.Anchor = AnchorStyles.Left;
            lblMachineName.AutoSize = true;
            lblMachineName.Margin = new Padding(0, 0, 6, 6);
            lblMachineName.Name = "lblMachineName";
            lblMachineName.Size = new Size(86, 15);
            lblMachineName.Text = "Machine Name:";
            // 
            // txtMachineName
            // 
            txtMachineName.BackColor = Color.White;
            txtMachineName.BorderStyle = BorderStyle.FixedSingle;
            txtMachineName.Dock = DockStyle.Fill;
            txtMachineName.Margin = new Padding(0, 0, 0, 6);
            txtMachineName.Name = "txtMachineName";
            txtMachineName.ReadOnly = true;
            txtMachineName.TabStop = false;
            // 
            // lblAppVersion
            // 
            lblAppVersion.Anchor = AnchorStyles.Left;
            lblAppVersion.AutoSize = true;
            lblAppVersion.Margin = new Padding(0, 0, 6, 6);
            lblAppVersion.Name = "lblAppVersion";
            lblAppVersion.Size = new Size(76, 15);
            lblAppVersion.Text = "App Version:";
            // 
            // txtAppVersion
            // 
            txtAppVersion.BackColor = Color.White;
            txtAppVersion.BorderStyle = BorderStyle.FixedSingle;
            txtAppVersion.Dock = DockStyle.Fill;
            txtAppVersion.Margin = new Padding(0, 0, 12, 6);
            txtAppVersion.Name = "txtAppVersion";
            txtAppVersion.ReadOnly = true;
            txtAppVersion.TabStop = false;
            // 
            // lblErrorType
            // 
            lblErrorType.Anchor = AnchorStyles.Left;
            lblErrorType.AutoSize = true;
            lblErrorType.Margin = new Padding(0, 0, 6, 6);
            lblErrorType.Name = "lblErrorType";
            lblErrorType.Size = new Size(63, 15);
            lblErrorType.Text = "Error Type:";
            // 
            // txtErrorType
            // 
            txtErrorType.BackColor = Color.White;
            txtErrorType.BorderStyle = BorderStyle.FixedSingle;
            txtErrorType.Dock = DockStyle.Fill;
            txtErrorType.Margin = new Padding(0, 0, 0, 6);
            txtErrorType.Name = "txtErrorType";
            txtErrorType.ReadOnly = true;
            txtErrorType.TabStop = false;
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Left;
            lblStatus.AutoSize = true;
            lblStatus.Margin = new Padding(0, 0, 6, 6);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(42, 15);
            lblStatus.Text = "Status:";
            // 
            // txtStatus
            // 
            txtStatus.BackColor = Color.White;
            txtStatus.BorderStyle = BorderStyle.FixedSingle;
            txtStatus.Dock = DockStyle.Fill;
            txtStatus.Margin = new Padding(0, 0, 12, 6);
            txtStatus.Name = "txtStatus";
            txtStatus.ReadOnly = true;
            txtStatus.TabStop = false;
            // 
            // lblReviewedBy
            // 
            lblReviewedBy.Anchor = AnchorStyles.Left;
            lblReviewedBy.AutoSize = true;
            lblReviewedBy.Margin = new Padding(0, 0, 6, 6);
            lblReviewedBy.Name = "lblReviewedBy";
            lblReviewedBy.Size = new Size(77, 15);
            lblReviewedBy.Text = "Reviewed By:";
            // 
            // txtReviewedBy
            // 
            txtReviewedBy.BackColor = Color.White;
            txtReviewedBy.BorderStyle = BorderStyle.FixedSingle;
            txtReviewedBy.Dock = DockStyle.Fill;
            txtReviewedBy.Margin = new Padding(0, 0, 0, 6);
            txtReviewedBy.Name = "txtReviewedBy";
            txtReviewedBy.ReadOnly = true;
            txtReviewedBy.TabStop = false;
            // 
            // lblReviewedDate
            // 
            lblReviewedDate.Anchor = AnchorStyles.Left;
            lblReviewedDate.AutoSize = true;
            lblReviewedDate.Margin = new Padding(0, 0, 6, 0);
            lblReviewedDate.Name = "lblReviewedDate";
            lblReviewedDate.Size = new Size(87, 15);
            lblReviewedDate.Text = "Reviewed Date:";
            // 
            // txtReviewedDate
            // 
            txtReviewedDate.BackColor = Color.White;
            txtReviewedDate.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutMetadata.SetColumnSpan(txtReviewedDate, 3);
            txtReviewedDate.Dock = DockStyle.Fill;
            txtReviewedDate.Margin = new Padding(0, 0, 0, 0);
            txtReviewedDate.Name = "txtReviewedDate";
            txtReviewedDate.ReadOnly = true;
            txtReviewedDate.TabStop = false;
            // 
            // grpErrorSummary
            // 
            grpErrorSummary.Controls.Add(txtErrorSummary);
            grpErrorSummary.Dock = DockStyle.Fill;
            grpErrorSummary.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            grpErrorSummary.Location = new Point(4, 177);
            grpErrorSummary.Margin = new Padding(4, 0, 4, 8);
            grpErrorSummary.Name = "grpErrorSummary";
            grpErrorSummary.Padding = new Padding(8);
            grpErrorSummary.Size = new Size(952, 112);
            grpErrorSummary.TabIndex = 2;
            grpErrorSummary.TabStop = false;
            grpErrorSummary.Text = "Error Summary";
            // 
            // txtErrorSummary
            // 
            txtErrorSummary.BackColor = Color.White;
            txtErrorSummary.BorderStyle = BorderStyle.FixedSingle;
            txtErrorSummary.Dock = DockStyle.Fill;
            txtErrorSummary.Margin = new Padding(0);
            txtErrorSummary.Multiline = true;
            txtErrorSummary.Name = "txtErrorSummary";
            txtErrorSummary.ReadOnly = true;
            txtErrorSummary.ScrollBars = ScrollBars.Vertical;
            txtErrorSummary.TabStop = false;
            // 
            // grpUserNotes
            // 
            grpUserNotes.Controls.Add(txtUserNotes);
            grpUserNotes.Dock = DockStyle.Fill;
            grpUserNotes.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            grpUserNotes.Location = new Point(4, 297);
            grpUserNotes.Margin = new Padding(4, 0, 4, 8);
            grpUserNotes.Name = "grpUserNotes";
            grpUserNotes.Padding = new Padding(8);
            grpUserNotes.Size = new Size(952, 142);
            grpUserNotes.TabIndex = 3;
            grpUserNotes.TabStop = false;
            grpUserNotes.Text = "═══ User Notes (What they were doing): ═══";
            // 
            // txtUserNotes
            // 
            txtUserNotes.BackColor = Color.FromArgb(255, 250, 205);
            txtUserNotes.BorderStyle = BorderStyle.FixedSingle;
            txtUserNotes.Dock = DockStyle.Fill;
            txtUserNotes.Margin = new Padding(0);
            txtUserNotes.Multiline = true;
            txtUserNotes.Name = "txtUserNotes";
            txtUserNotes.ReadOnly = true;
            txtUserNotes.ScrollBars = ScrollBars.Vertical;
            txtUserNotes.TabStop = false;
            // 
            // tableLayoutDetails
            // 
            tableLayoutDetails.ColumnCount = 2;
            tableLayoutDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutDetails.Controls.Add(grpDeveloperNotes, 0, 0);
            tableLayoutDetails.Controls.Add(grpTechnicalDetails, 0, 1);
            tableLayoutDetails.Controls.Add(grpCallStack, 1, 1);
            tableLayoutDetails.Dock = DockStyle.Fill;
            tableLayoutDetails.Location = new Point(4, 447);
            tableLayoutDetails.Margin = new Padding(4, 0, 4, 8);
            tableLayoutDetails.Name = "tableLayoutDetails";
            tableLayoutDetails.RowCount = 2;
            tableLayoutDetails.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F));
            tableLayoutDetails.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutDetails.Size = new Size(952, 170);
            tableLayoutDetails.TabIndex = 4;
            // 
            // grpDeveloperNotes
            // 
            grpDeveloperNotes.Controls.Add(txtDeveloperNotes);
            tableLayoutDetails.SetColumnSpan(grpDeveloperNotes, 2);
            grpDeveloperNotes.Dock = DockStyle.Fill;
            grpDeveloperNotes.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            grpDeveloperNotes.Location = new Point(0, 0);
            grpDeveloperNotes.Margin = new Padding(0, 0, 0, 8);
            grpDeveloperNotes.Name = "grpDeveloperNotes";
            grpDeveloperNotes.Padding = new Padding(8);
            grpDeveloperNotes.Size = new Size(952, 112);
            grpDeveloperNotes.TabIndex = 0;
            grpDeveloperNotes.TabStop = false;
            grpDeveloperNotes.Text = "Developer Notes";
            // 
            // txtDeveloperNotes
            // 
            txtDeveloperNotes.BackColor = Color.White;
            txtDeveloperNotes.BorderStyle = BorderStyle.FixedSingle;
            txtDeveloperNotes.Dock = DockStyle.Fill;
            txtDeveloperNotes.Margin = new Padding(0);
            txtDeveloperNotes.Multiline = true;
            txtDeveloperNotes.Name = "txtDeveloperNotes";
            txtDeveloperNotes.ReadOnly = true;
            txtDeveloperNotes.ScrollBars = ScrollBars.Vertical;
            txtDeveloperNotes.TabStop = false;
            // 
            // grpTechnicalDetails
            // 
            grpTechnicalDetails.Controls.Add(txtTechnicalDetails);
            grpTechnicalDetails.Dock = DockStyle.Fill;
            grpTechnicalDetails.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            grpTechnicalDetails.Location = new Point(0, 120);
            grpTechnicalDetails.Margin = new Padding(0, 0, 4, 0);
            grpTechnicalDetails.Name = "grpTechnicalDetails";
            grpTechnicalDetails.Padding = new Padding(8);
            grpTechnicalDetails.Size = new Size(472, 50);
            grpTechnicalDetails.TabIndex = 1;
            grpTechnicalDetails.TabStop = false;
            grpTechnicalDetails.Text = "Technical Details";
            // 
            // txtTechnicalDetails
            // 
            txtTechnicalDetails.BorderStyle = BorderStyle.FixedSingle;
            txtTechnicalDetails.DetectUrls = false;
            txtTechnicalDetails.Dock = DockStyle.Fill;
            txtTechnicalDetails.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtTechnicalDetails.Margin = new Padding(0);
            txtTechnicalDetails.Name = "txtTechnicalDetails";
            txtTechnicalDetails.ReadOnly = true;
            txtTechnicalDetails.ScrollBars = RichTextBoxScrollBars.Both;
            txtTechnicalDetails.WordWrap = false;
            txtTechnicalDetails.TabStop = false;
            // 
            // grpCallStack
            // 
            grpCallStack.Controls.Add(txtCallStack);
            grpCallStack.Dock = DockStyle.Fill;
            grpCallStack.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            grpCallStack.Location = new Point(476, 120);
            grpCallStack.Margin = new Padding(4, 0, 0, 0);
            grpCallStack.Name = "grpCallStack";
            grpCallStack.Padding = new Padding(8);
            grpCallStack.Size = new Size(476, 50);
            grpCallStack.TabIndex = 2;
            grpCallStack.TabStop = false;
            grpCallStack.Text = "Call Stack";
            // 
            // txtCallStack
            // 
            txtCallStack.BorderStyle = BorderStyle.FixedSingle;
            txtCallStack.DetectUrls = false;
            txtCallStack.Dock = DockStyle.Fill;
            txtCallStack.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point);
            txtCallStack.Margin = new Padding(0);
            txtCallStack.Name = "txtCallStack";
            txtCallStack.ReadOnly = true;
            txtCallStack.ScrollBars = RichTextBoxScrollBars.Both;
            txtCallStack.WordWrap = false;
            txtCallStack.TabStop = false;
            // 
            // flowButtons
            // 
            flowButtons.AutoSize = true;
            flowButtons.Controls.Add(btnExportReport);
            flowButtons.Controls.Add(btnCopyAll);
            flowButtons.Controls.Add(btnMarkResolved);
            flowButtons.Controls.Add(btnMarkReviewed);
            flowButtons.Dock = DockStyle.Fill;
            flowButtons.FlowDirection = FlowDirection.RightToLeft;
            flowButtons.Location = new Point(4, 625);
            flowButtons.Margin = new Padding(4, 0, 4, 0);
            flowButtons.Name = "flowButtons";
            flowButtons.Padding = new Padding(0, 8, 0, 8);
            flowButtons.Size = new Size(952, 55);
            flowButtons.TabIndex = 5;
            // 
            // btnExportReport
            // 
            btnExportReport.AutoSize = true;
            btnExportReport.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnExportReport.Location = new Point(832, 8);
            btnExportReport.Margin = new Padding(4, 0, 0, 0);
            btnExportReport.Name = "btnExportReport";
            btnExportReport.Padding = new Padding(12, 6, 12, 6);
            btnExportReport.Size = new Size(120, 34);
            btnExportReport.TabIndex = 0;
            btnExportReport.Text = "Export Report";
            btnExportReport.UseVisualStyleBackColor = true;
            btnExportReport.Click += btnExportReport_Click;
            // 
            // btnCopyAll
            // 
            btnCopyAll.AutoSize = true;
            btnCopyAll.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnCopyAll.Location = new Point(708, 8);
            btnCopyAll.Margin = new Padding(4, 0, 4, 0);
            btnCopyAll.Name = "btnCopyAll";
            btnCopyAll.Padding = new Padding(12, 6, 12, 6);
            btnCopyAll.Size = new Size(110, 34);
            btnCopyAll.TabIndex = 1;
            btnCopyAll.Text = "Copy All Details";
            btnCopyAll.UseVisualStyleBackColor = true;
            btnCopyAll.Click += btnCopyAll_Click;
            // 
            // btnMarkResolved
            // 
            btnMarkResolved.AutoSize = true;
            btnMarkResolved.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnMarkResolved.Location = new Point(584, 8);
            btnMarkResolved.Margin = new Padding(4, 0, 4, 0);
            btnMarkResolved.Name = "btnMarkResolved";
            btnMarkResolved.Padding = new Padding(12, 6, 12, 6);
            btnMarkResolved.Size = new Size(116, 34);
            btnMarkResolved.TabIndex = 2;
            btnMarkResolved.Text = "Mark as Resolved";
            btnMarkResolved.UseVisualStyleBackColor = true;
            btnMarkResolved.Visible = false;
            btnMarkResolved.Click += btnMarkResolved_Click;
            // 
            // btnMarkReviewed
            // 
            btnMarkReviewed.AutoSize = true;
            btnMarkReviewed.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnMarkReviewed.Location = new Point(444, 8);
            btnMarkReviewed.Margin = new Padding(4, 0, 4, 0);
            btnMarkReviewed.Name = "btnMarkReviewed";
            btnMarkReviewed.Padding = new Padding(12, 6, 12, 6);
            btnMarkReviewed.Size = new Size(132, 34);
            btnMarkReviewed.TabIndex = 3;
            btnMarkReviewed.Text = "Mark as Reviewed";
            btnMarkReviewed.UseVisualStyleBackColor = true;
            btnMarkReviewed.Visible = false;
            btnMarkReviewed.Click += btnMarkReviewed_Click;
            // 
            // Control_ErrorReportDetails
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            Controls.Add(tableLayoutMain);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Control_ErrorReportDetails";
            Size = new Size(960, 680);
            tableLayoutMain.ResumeLayout(false);
            tableLayoutMain.PerformLayout();
            tableLayoutMetadata.ResumeLayout(false);
            tableLayoutMetadata.PerformLayout();
            grpErrorSummary.ResumeLayout(false);
            grpErrorSummary.PerformLayout();
            grpUserNotes.ResumeLayout(false);
            grpUserNotes.PerformLayout();
            tableLayoutDetails.ResumeLayout(false);
            grpDeveloperNotes.ResumeLayout(false);
            grpDeveloperNotes.PerformLayout();
            grpTechnicalDetails.ResumeLayout(false);
            grpCallStack.ResumeLayout(false);
            flowButtons.ResumeLayout(false);
            flowButtons.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutMain = null!;
        private Label lblHeader = null!;
        private TableLayoutPanel tableLayoutMetadata = null!;
        private Label lblReportId = null!;
        private TextBox txtReportId = null!;
        private Label lblReportDate = null!;
        private TextBox txtReportDate = null!;
        private Label lblUserName = null!;
        private TextBox txtUserName = null!;
        private Label lblMachineName = null!;
        private TextBox txtMachineName = null!;
        private Label lblAppVersion = null!;
        private TextBox txtAppVersion = null!;
        private Label lblErrorType = null!;
        private TextBox txtErrorType = null!;
        private Label lblStatus = null!;
        private TextBox txtStatus = null!;
        private Label lblReviewedBy = null!;
        private TextBox txtReviewedBy = null!;
        private Label lblReviewedDate = null!;
        private TextBox txtReviewedDate = null!;
        private GroupBox grpErrorSummary = null!;
        private TextBox txtErrorSummary = null!;
        private GroupBox grpUserNotes = null!;
        private TextBox txtUserNotes = null!;
        private TableLayoutPanel tableLayoutDetails = null!;
        private GroupBox grpDeveloperNotes = null!;
        private TextBox txtDeveloperNotes = null!;
        private GroupBox grpTechnicalDetails = null!;
        private RichTextBox txtTechnicalDetails = null!;
        private GroupBox grpCallStack = null!;
        private RichTextBox txtCallStack = null!;
        private FlowLayoutPanel flowButtons = null!;
        private Button btnExportReport = null!;
        private Button btnCopyAll = null!;
        private Button btnMarkResolved = null!;
        private Button btnMarkReviewed = null!;
    }
}
