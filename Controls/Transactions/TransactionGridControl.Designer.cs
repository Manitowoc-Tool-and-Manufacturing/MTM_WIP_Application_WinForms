namespace MTM_WIP_Application_Winforms.Controls.Transactions
{
    partial class TransactionGridControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutMain = new System.Windows.Forms.TableLayoutPanel();
            this.dgvTransactions = new System.Windows.Forms.DataGridView();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.btnPrevious = new System.Windows.Forms.ToolStripButton();
            this.btnNext = new System.Windows.Forms.ToolStripButton();
            this.lblPageIndicator = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblRecordCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.txtGoToPage = new System.Windows.Forms.ToolStripTextBox();
            this.btnGoToPage = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutMain
            // 
            this.tableLayoutMain.ColumnCount = 1;
            this.tableLayoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutMain.Controls.Add(this.dgvTransactions, 0, 0);
            this.tableLayoutMain.Controls.Add(this.statusStrip, 0, 1);
            this.tableLayoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutMain.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutMain.Name = "tableLayoutMain";
            this.tableLayoutMain.RowCount = 2;
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutMain.Size = new System.Drawing.Size(880, 490);
            this.tableLayoutMain.TabIndex = 0;
            // 
            // dgvTransactions
            // 
            this.dgvTransactions.AllowUserToAddRows = false;
            this.dgvTransactions.AllowUserToDeleteRows = false;
            this.dgvTransactions.AllowUserToOrderColumns = true;
            this.dgvTransactions.AllowUserToResizeRows = false;
            this.dgvTransactions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgvTransactions.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvTransactions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTransactions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTransactions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTransactions.Location = new System.Drawing.Point(10, 10);
            this.dgvTransactions.Margin = new System.Windows.Forms.Padding(10);
            this.dgvTransactions.MinimumSize = new System.Drawing.Size(400, 200);
            this.dgvTransactions.MultiSelect = false;
            this.dgvTransactions.Name = "dgvTransactions";
            this.dgvTransactions.ReadOnly = true;
            this.dgvTransactions.RowHeadersVisible = false;
            this.dgvTransactions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTransactions.Size = new System.Drawing.Size(860, 430);
            this.dgvTransactions.TabIndex = 0;
            // 
            // statusStrip
            // 
            this.statusStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPrevious,
            this.btnNext,
            this.lblPageIndicator,
            this.toolStripSeparator1,
            this.lblRecordCount,
            this.txtGoToPage,
            this.btnGoToPage});
            this.statusStrip.Location = new System.Drawing.Point(0, 450);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(5, 0, 10, 0);
            this.statusStrip.Size = new System.Drawing.Size(880, 40);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // btnPrevious
            // 
            this.btnPrevious.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnPrevious.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(80, 38);
            this.btnPrevious.Text = "← Previous";
            this.btnPrevious.ToolTipText = "Go to previous page";
            // 
            // btnNext
            // 
            this.btnNext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnNext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(60, 38);
            this.btnNext.Text = "Next →";
            this.btnNext.ToolTipText = "Go to next page";
            // 
            // lblPageIndicator
            // 
            this.lblPageIndicator.Name = "lblPageIndicator";
            this.lblPageIndicator.Size = new System.Drawing.Size(79, 35);
            this.lblPageIndicator.Text = "Page 1 of 1";
            this.lblPageIndicator.Margin = new System.Windows.Forms.Padding(15, 3, 15, 2);
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(69, 35);
            this.lblRecordCount.Text = "0 records";
            this.lblRecordCount.Spring = true;
            this.lblRecordCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
            // 
            // txtGoToPage
            // 
            this.txtGoToPage.Name = "txtGoToPage";
            this.txtGoToPage.Size = new System.Drawing.Size(60, 40);
            this.txtGoToPage.ToolTipText = "Enter page number and press Enter";
            this.txtGoToPage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // btnGoToPage
            // 
            this.btnGoToPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnGoToPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGoToPage.Name = "btnGoToPage";
            this.btnGoToPage.Size = new System.Drawing.Size(35, 38);
            this.btnGoToPage.Text = "Go";
            this.btnGoToPage.ToolTipText = "Jump to entered page number";
            // 
            // TransactionGridControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.tableLayoutMain);
            this.MinimumSize = new System.Drawing.Size(400, 250);
            this.Name = "TransactionGridControl";
            this.Size = new System.Drawing.Size(880, 490);
            this.tableLayoutMain.ResumeLayout(false);
            this.tableLayoutMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutMain;
        private System.Windows.Forms.DataGridView dgvTransactions;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripButton btnPrevious;
        private System.Windows.Forms.ToolStripButton btnNext;
        private System.Windows.Forms.ToolStripStatusLabel lblPageIndicator;
        private System.Windows.Forms.ToolStripStatusLabel lblRecordCount;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox txtGoToPage;
        private System.Windows.Forms.ToolStripButton btnGoToPage;
    }
}
