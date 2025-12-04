namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    partial class Control_Developer_ParameterPrefixMaintenance
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgvOverrides = new System.Windows.Forms.DataGridView();
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panelDetails = new System.Windows.Forms.Panel();
            this.lblDetailsTitle = new System.Windows.Forms.Label();
            this.txtProcedureName = new System.Windows.Forms.TextBox();
            this.txtParameterName = new System.Windows.Forms.TextBox();
            this.txtOverridePrefix = new System.Windows.Forms.TextBox();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.lblProcedureName = new System.Windows.Forms.Label();
            this.lblParameterName = new System.Windows.Forms.Label();
            this.lblOverridePrefix = new System.Windows.Forms.Label();
            this.lblReason = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOverrides)).BeginInit();
            this.panelToolbar.SuspendLayout();
            this.panelDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.dgvOverrides, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelToolbar, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelDetails, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 600);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dgvOverrides
            // 
            this.dgvOverrides.AllowUserToAddRows = false;
            this.dgvOverrides.AllowUserToDeleteRows = false;
            this.dgvOverrides.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOverrides.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOverrides.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOverrides.Location = new System.Drawing.Point(3, 53);
            this.dgvOverrides.MultiSelect = false;
            this.dgvOverrides.Name = "dgvOverrides";
            this.dgvOverrides.ReadOnly = true;
            this.dgvOverrides.RowHeadersVisible = false;
            this.dgvOverrides.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOverrides.Size = new System.Drawing.Size(794, 324);
            this.dgvOverrides.TabIndex = 0;
            this.dgvOverrides.SelectionChanged += new System.EventHandler(this.DgvOverrides_SelectionChanged);
            // 
            // panelToolbar
            // 
            this.panelToolbar.Controls.Add(this.btnRefresh);
            this.panelToolbar.Controls.Add(this.btnDelete);
            this.panelToolbar.Controls.Add(this.btnEdit);
            this.panelToolbar.Controls.Add(this.btnAdd);
            this.panelToolbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelToolbar.Location = new System.Drawing.Point(3, 3);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Size = new System.Drawing.Size(794, 44);
            this.panelToolbar.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(10, 10);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(120, 30);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add Override";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Enabled = false;
            this.btnEdit.Location = new System.Drawing.Point(140, 10);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(120, 30);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Edit Override";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(270, 10);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(120, 30);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete Override";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(400, 10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 30);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // panelDetails
            // 
            this.panelDetails.Controls.Add(this.lblReason);
            this.panelDetails.Controls.Add(this.lblOverridePrefix);
            this.panelDetails.Controls.Add(this.lblParameterName);
            this.panelDetails.Controls.Add(this.lblProcedureName);
            this.panelDetails.Controls.Add(this.txtReason);
            this.panelDetails.Controls.Add(this.txtOverridePrefix);
            this.panelDetails.Controls.Add(this.txtParameterName);
            this.panelDetails.Controls.Add(this.txtProcedureName);
            this.panelDetails.Controls.Add(this.lblDetailsTitle);
            this.panelDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDetails.Location = new System.Drawing.Point(3, 383);
            this.panelDetails.Name = "panelDetails";
            this.panelDetails.Size = new System.Drawing.Size(794, 214);
            this.panelDetails.TabIndex = 2;
            // 
            // lblDetailsTitle
            // 
            this.lblDetailsTitle.AutoSize = true;
            this.lblDetailsTitle.Font = new System.Drawing.Font("Segoe UI Emoji", 10F, System.Drawing.FontStyle.Bold);
            this.lblDetailsTitle.Location = new System.Drawing.Point(10, 10);
            this.lblDetailsTitle.Name = "lblDetailsTitle";
            this.lblDetailsTitle.Size = new System.Drawing.Size(115, 19);
            this.lblDetailsTitle.TabIndex = 0;
            this.lblDetailsTitle.Text = "Override Details";
            // 
            // lblProcedureName
            // 
            this.lblProcedureName.AutoSize = true;
            this.lblProcedureName.Location = new System.Drawing.Point(10, 45);
            this.lblProcedureName.Name = "lblProcedureName";
            this.lblProcedureName.Size = new System.Drawing.Size(97, 15);
            this.lblProcedureName.TabIndex = 1;
            this.lblProcedureName.Text = "Procedure Name:";
            // 
            // txtProcedureName
            // 
            this.txtProcedureName.Location = new System.Drawing.Point(150, 42);
            this.txtProcedureName.Name = "txtProcedureName";
            this.txtProcedureName.ReadOnly = true;
            this.txtProcedureName.Size = new System.Drawing.Size(300, 23);
            this.txtProcedureName.TabIndex = 2;
            // 
            // lblParameterName
            // 
            this.lblParameterName.AutoSize = true;
            this.lblParameterName.Location = new System.Drawing.Point(10, 75);
            this.lblParameterName.Name = "lblParameterName";
            this.lblParameterName.Size = new System.Drawing.Size(98, 15);
            this.lblParameterName.TabIndex = 3;
            this.lblParameterName.Text = "Parameter Name:";
            // 
            // txtParameterName
            // 
            this.txtParameterName.Location = new System.Drawing.Point(150, 72);
            this.txtParameterName.Name = "txtParameterName";
            this.txtParameterName.ReadOnly = true;
            this.txtParameterName.Size = new System.Drawing.Size(300, 23);
            this.txtParameterName.TabIndex = 4;
            // 
            // lblOverridePrefix
            // 
            this.lblOverridePrefix.AutoSize = true;
            this.lblOverridePrefix.Location = new System.Drawing.Point(10, 105);
            this.lblOverridePrefix.Name = "lblOverridePrefix";
            this.lblOverridePrefix.Size = new System.Drawing.Size(88, 15);
            this.lblOverridePrefix.TabIndex = 5;
            this.lblOverridePrefix.Text = "Override Prefix:";
            // 
            // txtOverridePrefix
            // 
            this.txtOverridePrefix.Location = new System.Drawing.Point(150, 102);
            this.txtOverridePrefix.Name = "txtOverridePrefix";
            this.txtOverridePrefix.ReadOnly = true;
            this.txtOverridePrefix.Size = new System.Drawing.Size(100, 23);
            this.txtOverridePrefix.TabIndex = 6;
            // 
            // lblReason
            // 
            this.lblReason.AutoSize = true;
            this.lblReason.Location = new System.Drawing.Point(10, 135);
            this.lblReason.Name = "lblReason";
            this.lblReason.Size = new System.Drawing.Size(47, 15);
            this.lblReason.TabIndex = 7;
            this.lblReason.Text = "Reason:";
            // 
            // txtReason
            // 
            this.txtReason.Location = new System.Drawing.Point(150, 132);
            this.txtReason.Multiline = true;
            this.txtReason.Name = "txtReason";
            this.txtReason.ReadOnly = true;
            this.txtReason.Size = new System.Drawing.Size(630, 70);
            this.txtReason.TabIndex = 8;
            // 
            // Control_Developer_ParameterPrefixMaintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Control_Developer_ParameterPrefixMaintenance";
            this.Size = new System.Drawing.Size(800, 600);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOverrides)).EndInit();
            this.panelToolbar.ResumeLayout(false);
            this.panelDetails.ResumeLayout(false);
            this.panelDetails.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgvOverrides;
        private System.Windows.Forms.Panel panelToolbar;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Panel panelDetails;
        private System.Windows.Forms.Label lblDetailsTitle;
        private System.Windows.Forms.Label lblProcedureName;
        private System.Windows.Forms.TextBox txtProcedureName;
        private System.Windows.Forms.Label lblParameterName;
        private System.Windows.Forms.TextBox txtParameterName;
        private System.Windows.Forms.Label lblOverridePrefix;
        private System.Windows.Forms.TextBox txtOverridePrefix;
        private System.Windows.Forms.Label lblReason;
        private System.Windows.Forms.TextBox txtReason;
    }
}
