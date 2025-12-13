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
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.btnApplyFilter = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnRepairData = new System.Windows.Forms.Button();
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.pnlDetails = new System.Windows.Forms.Panel();
            this.tlpDetails = new System.Windows.Forms.TableLayoutPanel();
            this.lblId = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.lblType2 = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.TextBox();
            this.lblStatus2 = new System.Windows.Forms.Label();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.txtCategory = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblSteps = new System.Windows.Forms.Label();
            this.txtSteps = new System.Windows.Forms.TextBox();
            this.lblExpected = new System.Windows.Forms.Label();
            this.txtExpected = new System.Windows.Forms.TextBox();
            this.lblActual = new System.Windows.Forms.Label();
            this.txtActual = new System.Windows.Forms.TextBox();
            this.lblJustification = new System.Windows.Forms.Label();
            this.txtJustification = new System.Windows.Forms.TextBox();
            this.lblAffected = new System.Windows.Forms.Label();
            this.txtAffected = new System.Windows.Forms.TextBox();
            this.lblLoc1 = new System.Windows.Forms.Label();
            this.txtLoc1 = new System.Windows.Forms.TextBox();
            this.lblLoc2 = new System.Windows.Forms.Label();
            this.txtLoc2 = new System.Windows.Forms.TextBox();
            this.lblConsistency = new System.Windows.Forms.Label();
            this.txtConsistency = new System.Windows.Forms.TextBox();
            
            this.pnlFilter.SuspendLayout();
            this.cmsFeedback.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.pnlDetails.SuspendLayout();
            this.tlpDetails.SuspendLayout();
            this.SuspendLayout();
            
            // 
            // pnlFilter
            // 
            this.pnlFilter.Controls.Add(this.cboType);
            this.pnlFilter.Controls.Add(this.lblType);
            this.pnlFilter.Controls.Add(this.cboStatus);
            this.pnlFilter.Controls.Add(this.lblStatus);
            this.pnlFilter.Controls.Add(this.btnExport);
            this.pnlFilter.Controls.Add(this.btnApplyFilter);
            this.pnlFilter.Controls.Add(this.btnRepairData);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 0);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1000, 80);
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
            // btnRepairData
            // 
            this.btnRepairData.Location = new System.Drawing.Point(640, 25);
            this.btnRepairData.Name = "btnRepairData";
            this.btnRepairData.Size = new System.Drawing.Size(100, 30);
            this.btnRepairData.TabIndex = 6;
            this.btnRepairData.Text = "Repair Data";
            this.btnRepairData.UseVisualStyleBackColor = true;
            this.btnRepairData.Click += new System.EventHandler(this.btnRepairData_Click);
            
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
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 80);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlDetails);
            this.splitContainer1.Size = new System.Drawing.Size(1000, 520);
            this.splitContainer1.SplitterDistance = 350;
            this.splitContainer1.TabIndex = 1;
            
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.ContextMenuStrip = this.cmsFeedback;
            this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvList.Location = new System.Drawing.Point(0, 0);
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(350, 520);
            this.dgvList.TabIndex = 0;
            this.dgvList.SelectionChanged += new System.EventHandler(this.dgvList_SelectionChanged);
            
            // 
            // pnlDetails
            // 
            this.pnlDetails.AutoScroll = true;
            this.pnlDetails.Controls.Add(this.tlpDetails);
            this.pnlDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetails.Location = new System.Drawing.Point(0, 0);
            this.pnlDetails.Name = "pnlDetails";
            this.pnlDetails.Padding = new System.Windows.Forms.Padding(10);
            this.pnlDetails.Size = new System.Drawing.Size(646, 520);
            this.pnlDetails.TabIndex = 0;
            
            // 
            // tlpDetails
            // 
            this.tlpDetails.AutoSize = true;
            this.tlpDetails.ColumnCount = 2;
            this.tlpDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpDetails.Controls.Add(this.lblId, 0, 0);
            this.tlpDetails.Controls.Add(this.txtId, 1, 0);
            this.tlpDetails.Controls.Add(this.lblDate, 0, 1);
            this.tlpDetails.Controls.Add(this.txtDate, 1, 1);
            this.tlpDetails.Controls.Add(this.lblUser, 0, 2);
            this.tlpDetails.Controls.Add(this.txtUser, 1, 2);
            this.tlpDetails.Controls.Add(this.lblType2, 0, 3);
            this.tlpDetails.Controls.Add(this.txtType, 1, 3);
            this.tlpDetails.Controls.Add(this.lblStatus2, 0, 4);
            this.tlpDetails.Controls.Add(this.txtStatus, 1, 4);
            this.tlpDetails.Controls.Add(this.lblCategory, 0, 5);
            this.tlpDetails.Controls.Add(this.txtCategory, 1, 5);
            this.tlpDetails.Controls.Add(this.lblTitle, 0, 6);
            this.tlpDetails.Controls.Add(this.txtTitle, 1, 6);
            this.tlpDetails.Controls.Add(this.lblDescription, 0, 7);
            this.tlpDetails.Controls.Add(this.txtDescription, 1, 7);
            this.tlpDetails.Controls.Add(this.lblSteps, 0, 8);
            this.tlpDetails.Controls.Add(this.txtSteps, 1, 8);
            this.tlpDetails.Controls.Add(this.lblExpected, 0, 9);
            this.tlpDetails.Controls.Add(this.txtExpected, 1, 9);
            this.tlpDetails.Controls.Add(this.lblActual, 0, 10);
            this.tlpDetails.Controls.Add(this.txtActual, 1, 10);
            this.tlpDetails.Controls.Add(this.lblJustification, 0, 11);
            this.tlpDetails.Controls.Add(this.txtJustification, 1, 11);
            this.tlpDetails.Controls.Add(this.lblAffected, 0, 12);
            this.tlpDetails.Controls.Add(this.txtAffected, 1, 12);
            this.tlpDetails.Controls.Add(this.lblLoc1, 0, 13);
            this.tlpDetails.Controls.Add(this.txtLoc1, 1, 13);
            this.tlpDetails.Controls.Add(this.lblLoc2, 0, 14);
            this.tlpDetails.Controls.Add(this.txtLoc2, 1, 14);
            this.tlpDetails.Controls.Add(this.lblConsistency, 0, 15);
            this.tlpDetails.Controls.Add(this.txtConsistency, 1, 15);
            this.tlpDetails.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpDetails.Location = new System.Drawing.Point(10, 10);
            this.tlpDetails.Name = "tlpDetails";
            this.tlpDetails.RowCount = 16;
            this.tlpDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDetails.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpDetails.Size = new System.Drawing.Size(626, 600);
            this.tlpDetails.TabIndex = 0;
            
            // Helper method to configure labels
            void ConfigureLabel(System.Windows.Forms.Label lbl, string text)
            {
                lbl.AutoSize = true;
                lbl.Dock = System.Windows.Forms.DockStyle.Fill;
                lbl.Location = new System.Drawing.Point(3, 0);
                lbl.Name = "lbl" + text.Replace(" ", "");
                lbl.Size = new System.Drawing.Size(114, 29);
                lbl.TabIndex = 0;
                lbl.Text = text + ":";
                lbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            }

            // Helper method to configure textboxes
            void ConfigureTextBox(System.Windows.Forms.TextBox txt, string name, bool multiline = false)
            {
                txt.Dock = System.Windows.Forms.DockStyle.Fill;
                txt.Location = new System.Drawing.Point(123, 3);
                txt.Name = name;
                txt.ReadOnly = true;
                txt.Size = new System.Drawing.Size(500, 23);
                txt.TabIndex = 1;
                if (multiline)
                {
                    txt.Multiline = true;
                    txt.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
                    txt.Height = 80;
                }
            }

            ConfigureLabel(this.lblId, "ID");
            ConfigureTextBox(this.txtId, "txtId");

            ConfigureLabel(this.lblDate, "Date");
            ConfigureTextBox(this.txtDate, "txtDate");

            ConfigureLabel(this.lblUser, "User");
            ConfigureTextBox(this.txtUser, "txtUser");

            ConfigureLabel(this.lblType2, "Type");
            ConfigureTextBox(this.txtType, "txtType");

            ConfigureLabel(this.lblStatus2, "Status");
            ConfigureTextBox(this.txtStatus, "txtStatus");

            ConfigureLabel(this.lblCategory, "Category");
            ConfigureTextBox(this.txtCategory, "txtCategory");

            ConfigureLabel(this.lblTitle, "Title");
            ConfigureTextBox(this.txtTitle, "txtTitle");

            ConfigureLabel(this.lblDescription, "Description");
            ConfigureTextBox(this.txtDescription, "txtDescription", true);

            ConfigureLabel(this.lblSteps, "Steps");
            ConfigureTextBox(this.txtSteps, "txtSteps", true);

            ConfigureLabel(this.lblExpected, "Expected");
            ConfigureTextBox(this.txtExpected, "txtExpected", true);

            ConfigureLabel(this.lblActual, "Actual");
            ConfigureTextBox(this.txtActual, "txtActual", true);

            ConfigureLabel(this.lblJustification, "Justification");
            ConfigureTextBox(this.txtJustification, "txtJustification", true);

            ConfigureLabel(this.lblAffected, "Affected");
            ConfigureTextBox(this.txtAffected, "txtAffected", true);

            ConfigureLabel(this.lblLoc1, "Location 1");
            ConfigureTextBox(this.txtLoc1, "txtLoc1");

            ConfigureLabel(this.lblLoc2, "Location 2");
            ConfigureTextBox(this.txtLoc2, "txtLoc2");

            ConfigureLabel(this.lblConsistency, "Consistency");
            ConfigureTextBox(this.txtConsistency, "txtConsistency", true);

            // 
            // Form_DeveloperTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.pnlFilter);
            this.Name = "Form_DeveloperTools";
            this.Text = "Developer Tools - Feedback Management";
            this.Load += new System.EventHandler(this.Form_DeveloperTools_Load);
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.cmsFeedback.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.pnlDetails.ResumeLayout(false);
            this.pnlDetails.PerformLayout();
            this.tlpDetails.ResumeLayout(false);
            this.tlpDetails.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Button btnApplyFilter;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnRepairData;
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
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.Panel pnlDetails;
        private System.Windows.Forms.TableLayoutPanel tlpDetails;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label lblType2;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.Label lblStatus2;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.TextBox txtCategory;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblSteps;
        private System.Windows.Forms.TextBox txtSteps;
        private System.Windows.Forms.Label lblExpected;
        private System.Windows.Forms.TextBox txtExpected;
        private System.Windows.Forms.Label lblActual;
        private System.Windows.Forms.TextBox txtActual;
        private System.Windows.Forms.Label lblJustification;
        private System.Windows.Forms.TextBox txtJustification;
        private System.Windows.Forms.Label lblAffected;
        private System.Windows.Forms.TextBox txtAffected;
        private System.Windows.Forms.Label lblLoc1;
        private System.Windows.Forms.TextBox txtLoc1;
        private System.Windows.Forms.Label lblLoc2;
        private System.Windows.Forms.TextBox txtLoc2;
        private System.Windows.Forms.Label lblConsistency;
        private System.Windows.Forms.TextBox txtConsistency;
    }
}
