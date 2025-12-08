namespace MTM_WIP_Application_Winforms.Forms.DeveloperTools
{
    partial class Form_DeveloperTools
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
            this.components = new System.ComponentModel.Container();
            this.dgvFeedback = new System.Windows.Forms.DataGridView();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.btnApplyFilter = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.cmsFeedback = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiUpdateStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddNotes = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAssignDeveloper = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMarkDuplicate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiViewDetails = new System.Windows.Forms.ToolStripMenuItem();
            
            ((System.ComponentModel.ISupportInitialize)(this.dgvFeedback)).BeginInit();
            this.pnlFilter.SuspendLayout();
            this.cmsFeedback.SuspendLayout();
            this.SuspendLayout();
            
            // 
            // dgvFeedback
            // 
            this.dgvFeedback.AllowUserToAddRows = false;
            this.dgvFeedback.AllowUserToDeleteRows = false;
            this.dgvFeedback.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFeedback.ContextMenuStrip = this.cmsFeedback;
            this.dgvFeedback.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFeedback.Location = new System.Drawing.Point(0, 80);
            this.dgvFeedback.Name = "dgvFeedback";
            this.dgvFeedback.ReadOnly = true;
            this.dgvFeedback.Size = new System.Drawing.Size(800, 370);
            this.dgvFeedback.TabIndex = 1;
            
            // 
            // pnlFilter
            // 
            this.pnlFilter.Controls.Add(this.cboType);
            this.pnlFilter.Controls.Add(this.lblType);
            this.pnlFilter.Controls.Add(this.cboStatus);
            this.pnlFilter.Controls.Add(this.lblStatus);
            this.pnlFilter.Controls.Add(this.btnExport);
            this.pnlFilter.Controls.Add(this.btnApplyFilter);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 0);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(800, 80);
            this.pnlFilter.TabIndex = 0;
            
            // 
            // btnApplyFilter
            // 
            this.btnApplyFilter.Location = new System.Drawing.Point(400, 25);
            this.btnApplyFilter.Name = "btnApplyFilter";
            this.btnApplyFilter.Size = new System.Drawing.Size(100, 30);
            this.btnApplyFilter.TabIndex = 4;
            this.btnApplyFilter.Text = "Apply Filter";
            this.btnApplyFilter.UseVisualStyleBackColor = true;
            this.btnApplyFilter.Click += new System.EventHandler(this.btnApplyFilter_Click);
            
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(520, 25);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(100, 30);
            this.btnExport.TabIndex = 5;
            this.btnExport.Text = "Export CSV";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(20, 15);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(42, 15);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Status:";
            
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Location = new System.Drawing.Point(20, 35);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(150, 23);
            this.cboStatus.TabIndex = 1;
            
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(200, 15);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(34, 15);
            this.lblType.TabIndex = 2;
            this.lblType.Text = "Type:";
            
            // 
            // cboType
            // 
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.FormattingEnabled = true;
            this.cboType.Location = new System.Drawing.Point(200, 35);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(150, 23);
            this.cboType.TabIndex = 3;
            
            // 
            // cmsFeedback
            // 
            this.cmsFeedback.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiUpdateStatus,
            this.tsmiAddNotes,
            this.tsmiAssignDeveloper,
            this.tsmiMarkDuplicate,
            this.tsmiViewDetails});
            this.cmsFeedback.Name = "cmsFeedback";
            this.cmsFeedback.Size = new System.Drawing.Size(181, 114);
            
            // 
            // tsmiUpdateStatus
            // 
            this.tsmiUpdateStatus.Name = "tsmiUpdateStatus";
            this.tsmiUpdateStatus.Size = new System.Drawing.Size(180, 22);
            this.tsmiUpdateStatus.Text = "Update Status";
            this.tsmiUpdateStatus.Click += new System.EventHandler(this.tsmiUpdateStatus_Click);
            
            // 
            // tsmiAddNotes
            // 
            this.tsmiAddNotes.Name = "tsmiAddNotes";
            this.tsmiAddNotes.Size = new System.Drawing.Size(180, 22);
            this.tsmiAddNotes.Text = "Add Notes";
            this.tsmiAddNotes.Click += new System.EventHandler(this.tsmiAddNotes_Click);
            
            // 
            // tsmiAssignDeveloper
            // 
            this.tsmiAssignDeveloper.Name = "tsmiAssignDeveloper";
            this.tsmiAssignDeveloper.Size = new System.Drawing.Size(180, 22);
            this.tsmiAssignDeveloper.Text = "Assign Developer";
            this.tsmiAssignDeveloper.Click += new System.EventHandler(this.tsmiAssignDeveloper_Click);
            
            // 
            // tsmiMarkDuplicate
            // 
            this.tsmiMarkDuplicate.Name = "tsmiMarkDuplicate";
            this.tsmiMarkDuplicate.Size = new System.Drawing.Size(180, 22);
            this.tsmiMarkDuplicate.Text = "Mark Duplicate";
            this.tsmiMarkDuplicate.Click += new System.EventHandler(this.tsmiMarkDuplicate_Click);
            
            // 
            // tsmiViewDetails
            // 
            this.tsmiViewDetails.Name = "tsmiViewDetails";
            this.tsmiViewDetails.Size = new System.Drawing.Size(180, 22);
            this.tsmiViewDetails.Text = "View Details";
            this.tsmiViewDetails.Click += new System.EventHandler(this.tsmiViewDetails_Click);
            
            // 
            // Form_DeveloperTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvFeedback);
            this.Controls.Add(this.pnlFilter);
            this.Name = "Form_DeveloperTools";
            this.Text = "Developer Tools - Feedback Management";
            this.Load += new System.EventHandler(this.Form_DeveloperTools_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFeedback)).EndInit();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.cmsFeedback.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvFeedback;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Button btnApplyFilter;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.ContextMenuStrip cmsFeedback;
        private System.Windows.Forms.ToolStripMenuItem tsmiUpdateStatus;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddNotes;
        private System.Windows.Forms.ToolStripMenuItem tsmiAssignDeveloper;
        private System.Windows.Forms.ToolStripMenuItem tsmiMarkDuplicate;
        private System.Windows.Forms.ToolStripMenuItem tsmiViewDetails;
    }
}
