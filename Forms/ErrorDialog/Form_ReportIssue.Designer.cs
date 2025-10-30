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
            tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            lblErrorSummary = new System.Windows.Forms.Label();
            txtErrorSummary = new System.Windows.Forms.TextBox();
            lblUserNotes = new System.Windows.Forms.Label();
            txtUserNotes = new System.Windows.Forms.TextBox();
            tableLayoutPanelButtons = new System.Windows.Forms.TableLayoutPanel();
            btnSubmit = new System.Windows.Forms.Button();
            btnCancel = new System.Windows.Forms.Button();
            tableLayoutPanelMain.SuspendLayout();
            tableLayoutPanelButtons.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.ColumnCount = 1;
            tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanelMain.Controls.Add(lblErrorSummary, 0, 0);
            tableLayoutPanelMain.Controls.Add(txtErrorSummary, 0, 1);
            tableLayoutPanelMain.Controls.Add(lblUserNotes, 0, 2);
            tableLayoutPanelMain.Controls.Add(txtUserNotes, 0, 3);
            tableLayoutPanelMain.Controls.Add(tableLayoutPanelButtons, 0, 4);
            tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.Padding = new System.Windows.Forms.Padding(10);
            tableLayoutPanelMain.RowCount = 5;
            tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            tableLayoutPanelMain.Size = new System.Drawing.Size(584, 314);
            tableLayoutPanelMain.TabIndex = 0;
            // 
            // lblErrorSummary
            // 
            lblErrorSummary.AutoSize = true;
            lblErrorSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            lblErrorSummary.Location = new System.Drawing.Point(10, 10);
            lblErrorSummary.Margin = new System.Windows.Forms.Padding(0);
            lblErrorSummary.Name = "lblErrorSummary";
            lblErrorSummary.Size = new System.Drawing.Size(564, 25);
            lblErrorSummary.TabIndex = 0;
            lblErrorSummary.Text = "Error Summary:";
            lblErrorSummary.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txtErrorSummary
            // 
            txtErrorSummary.BackColor = System.Drawing.SystemColors.Control;
            txtErrorSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            txtErrorSummary.Location = new System.Drawing.Point(10, 40);
            txtErrorSummary.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            txtErrorSummary.Multiline = true;
            txtErrorSummary.Name = "txtErrorSummary";
            txtErrorSummary.ReadOnly = true;
            txtErrorSummary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            txtErrorSummary.Size = new System.Drawing.Size(564, 80);
            txtErrorSummary.TabIndex = 1;
            txtErrorSummary.TabStop = false;
            // 
            // lblUserNotes
            // 
            lblUserNotes.AutoSize = true;
            lblUserNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            lblUserNotes.Location = new System.Drawing.Point(10, 125);
            lblUserNotes.Margin = new System.Windows.Forms.Padding(0);
            lblUserNotes.Name = "lblUserNotes";
            lblUserNotes.Size = new System.Drawing.Size(564, 25);
            lblUserNotes.TabIndex = 2;
            lblUserNotes.Text = "What were you doing when this error occurred? (optional):";
            lblUserNotes.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // txtUserNotes
            // 
            txtUserNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            txtUserNotes.Location = new System.Drawing.Point(10, 155);
            txtUserNotes.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            txtUserNotes.Multiline = true;
            txtUserNotes.Name = "txtUserNotes";
            txtUserNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            txtUserNotes.Size = new System.Drawing.Size(564, 111);
            txtUserNotes.TabIndex = 3;
            // 
            // tableLayoutPanelButtons
            // 
            tableLayoutPanelButtons.ColumnCount = 3;
            tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            tableLayoutPanelButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            tableLayoutPanelButtons.Controls.Add(btnSubmit, 1, 0);
            tableLayoutPanelButtons.Controls.Add(btnCancel, 2, 0);
            tableLayoutPanelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanelButtons.Location = new System.Drawing.Point(10, 271);
            tableLayoutPanelButtons.Margin = new System.Windows.Forms.Padding(0);
            tableLayoutPanelButtons.Name = "tableLayoutPanelButtons";
            tableLayoutPanelButtons.RowCount = 1;
            tableLayoutPanelButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanelButtons.Size = new System.Drawing.Size(564, 33);
            tableLayoutPanelButtons.TabIndex = 4;
            // 
            // btnSubmit
            // 
            btnSubmit.Dock = System.Windows.Forms.DockStyle.Fill;
            btnSubmit.Location = new System.Drawing.Point(405, 0);
            btnSubmit.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            btnSubmit.MinimumSize = new System.Drawing.Size(75, 28);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new System.Drawing.Size(75, 33);
            btnSubmit.TabIndex = 0;
            btnSubmit.Text = "Submit";
            btnSubmit.UseVisualStyleBackColor = true;
            btnSubmit.Click += new System.EventHandler(btnSubmit_Click);
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            btnCancel.Location = new System.Drawing.Point(486, 0);
            btnCancel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            btnCancel.MinimumSize = new System.Drawing.Size(75, 28);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(78, 33);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += new System.EventHandler(btnCancel_Click);
            // 
            // Form_ReportIssue
            // 
            AcceptButton = btnSubmit;
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            CancelButton = btnCancel;
            ClientSize = new System.Drawing.Size(584, 314);
            Controls.Add(tableLayoutPanelMain);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new System.Drawing.Size(600, 350);
            Name = "Form_ReportIssue";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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
