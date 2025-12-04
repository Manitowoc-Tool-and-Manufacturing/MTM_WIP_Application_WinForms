namespace MTM_WIP_Application_Winforms.Forms.ErrorDialog
{
    partial class Form_ReportIssue
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_ReportIssue));
            tableLayoutPanelMain = new TableLayoutPanel();
            lblErrorSummary = new Label();
            txtErrorSummary = new TextBox();
            lblUserNotes = new Label();
            txtUserNotes = new TextBox();
            tableLayoutPanelButtons = new TableLayoutPanel();
            btnSubmit = new Button();
            btnCancel = new Button();
            tableLayoutPanelMain.SuspendLayout();
            tableLayoutPanelButtons.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.ColumnCount = 1;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Controls.Add(lblErrorSummary, 0, 0);
            tableLayoutPanelMain.Controls.Add(txtErrorSummary, 0, 1);
            tableLayoutPanelMain.Controls.Add(lblUserNotes, 0, 2);
            tableLayoutPanelMain.Controls.Add(txtUserNotes, 0, 3);
            tableLayoutPanelMain.Controls.Add(tableLayoutPanelButtons, 0, 4);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.Location = new Point(0, 0);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.Padding = new Padding(10);
            tableLayoutPanelMain.RowCount = 5;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 90F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 38F));
            tableLayoutPanelMain.Size = new Size(584, 314);
            tableLayoutPanelMain.TabIndex = 0;
            // 
            // lblErrorSummary
            // 
            lblErrorSummary.AutoSize = true;
            lblErrorSummary.Dock = DockStyle.Fill;
            lblErrorSummary.Location = new Point(10, 10);
            lblErrorSummary.Margin = new Padding(0);
            lblErrorSummary.Name = "lblErrorSummary";
            lblErrorSummary.Size = new Size(564, 25);
            lblErrorSummary.TabIndex = 0;
            lblErrorSummary.Text = "Error Summary:";
            lblErrorSummary.TextAlign = ContentAlignment.BottomLeft;
            // 
            // txtErrorSummary
            // 
            txtErrorSummary.BackColor = SystemColors.Control;
            txtErrorSummary.Dock = DockStyle.Fill;
            txtErrorSummary.Location = new Point(10, 40);
            txtErrorSummary.Margin = new Padding(0, 5, 0, 5);
            txtErrorSummary.Multiline = true;
            txtErrorSummary.Name = "txtErrorSummary";
            txtErrorSummary.ReadOnly = true;
            txtErrorSummary.ScrollBars = ScrollBars.Vertical;
            txtErrorSummary.Size = new Size(564, 80);
            txtErrorSummary.TabIndex = 1;
            txtErrorSummary.TabStop = false;
            // 
            // lblUserNotes
            // 
            lblUserNotes.AutoSize = true;
            lblUserNotes.Dock = DockStyle.Fill;
            lblUserNotes.Location = new Point(10, 125);
            lblUserNotes.Margin = new Padding(0);
            lblUserNotes.Name = "lblUserNotes";
            lblUserNotes.Size = new Size(564, 25);
            lblUserNotes.TabIndex = 2;
            lblUserNotes.Text = "What were you doing when this error occurred? (optional):";
            lblUserNotes.TextAlign = ContentAlignment.BottomLeft;
            // 
            // txtUserNotes
            // 
            txtUserNotes.Dock = DockStyle.Fill;
            txtUserNotes.Location = new Point(10, 155);
            txtUserNotes.Margin = new Padding(0, 5, 0, 5);
            txtUserNotes.Multiline = true;
            txtUserNotes.Name = "txtUserNotes";
            txtUserNotes.ScrollBars = ScrollBars.Vertical;
            txtUserNotes.Size = new Size(564, 106);
            txtUserNotes.TabIndex = 3;
            // 
            // tableLayoutPanelButtons
            // 
            tableLayoutPanelButtons.ColumnCount = 3;
            tableLayoutPanelButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 81F));
            tableLayoutPanelButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 81F));
            tableLayoutPanelButtons.Controls.Add(btnSubmit, 1, 0);
            tableLayoutPanelButtons.Controls.Add(btnCancel, 2, 0);
            tableLayoutPanelButtons.Dock = DockStyle.Fill;
            tableLayoutPanelButtons.Location = new Point(10, 266);
            tableLayoutPanelButtons.Margin = new Padding(0);
            tableLayoutPanelButtons.Name = "tableLayoutPanelButtons";
            tableLayoutPanelButtons.RowCount = 1;
            tableLayoutPanelButtons.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelButtons.Size = new Size(564, 38);
            tableLayoutPanelButtons.TabIndex = 4;
            // 
            // btnSubmit
            // 
            btnSubmit.Dock = DockStyle.Fill;
            btnSubmit.Location = new Point(405, 0);
            btnSubmit.Margin = new Padding(3, 0, 3, 0);
            btnSubmit.MinimumSize = new Size(75, 28);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(75, 38);
            btnSubmit.TabIndex = 0;
            btnSubmit.Text = "Submit";
            btnSubmit.UseVisualStyleBackColor = true;
            btnSubmit.Click += btnSubmit_Click;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Dock = DockStyle.Fill;
            btnCancel.Location = new Point(486, 0);
            btnCancel.Margin = new Padding(3, 0, 0, 0);
            btnCancel.MinimumSize = new Size(75, 28);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(78, 38);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // Form_ReportIssue
            // 
            AcceptButton = btnSubmit;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = btnCancel;
            ClientSize = new Size(584, 314);
            Controls.Add(tableLayoutPanelMain);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(600, 350);
            Name = "Form_ReportIssue";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Report Issue";
            tableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanelMain.PerformLayout();
            tableLayoutPanelButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.Label lblErrorSummary;
        private System.Windows.Forms.TextBox txtErrorSummary;
        private System.Windows.Forms.Label lblUserNotes;
        private System.Windows.Forms.TextBox txtUserNotes;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelButtons;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
    }
}
