namespace MTM_WIP_Application_Winforms.Controls.Visual
{
    partial class Control_DieToolDiscovery
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
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabLocator = new System.Windows.Forms.TabPage();
            this.gridResults = new System.Windows.Forms.DataGridView();
            this.grpSearch = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.radioSearchByDie = new System.Windows.Forms.RadioButton();
            this.radioSearchByPart = new System.Windows.Forms.RadioButton();
            this.tabDiscovery = new System.Windows.Forms.TabPage();
            this.tabControlMain.SuspendLayout();
            this.tabLocator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridResults)).BeginInit();
            this.grpSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabLocator);
            this.tabControlMain.Controls.Add(this.tabDiscovery);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(800, 600);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabLocator
            // 
            this.tabLocator.Controls.Add(this.gridResults);
            this.tabLocator.Controls.Add(this.grpSearch);
            this.tabLocator.Location = new System.Drawing.Point(4, 24);
            this.tabLocator.Name = "tabLocator";
            this.tabLocator.Padding = new System.Windows.Forms.Padding(10);
            this.tabLocator.Size = new System.Drawing.Size(792, 572);
            this.tabLocator.TabIndex = 0;
            this.tabLocator.Text = "Die Locator";
            this.tabLocator.UseVisualStyleBackColor = true;
            // 
            // gridResults
            // 
            this.gridResults.AllowUserToAddRows = false;
            this.gridResults.AllowUserToDeleteRows = false;
            this.gridResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridResults.Location = new System.Drawing.Point(10, 110);
            this.gridResults.Name = "gridResults";
            this.gridResults.ReadOnly = true;
            this.gridResults.RowTemplate.Height = 25;
            this.gridResults.Size = new System.Drawing.Size(772, 452);
            this.gridResults.TabIndex = 1;
            // 
            // grpSearch
            // 
            this.grpSearch.Controls.Add(this.btnSearch);
            this.grpSearch.Controls.Add(this.txtSearch);
            this.grpSearch.Controls.Add(this.lblSearch);
            this.grpSearch.Controls.Add(this.radioSearchByDie);
            this.grpSearch.Controls.Add(this.radioSearchByPart);
            this.grpSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpSearch.Location = new System.Drawing.Point(10, 10);
            this.grpSearch.Name = "grpSearch";
            this.grpSearch.Size = new System.Drawing.Size(772, 100);
            this.grpSearch.TabIndex = 0;
            this.grpSearch.TabStop = false;
            this.grpSearch.Text = "Search Criteria";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(450, 50);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 25);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(120, 52);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(300, 23);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(20, 55);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(90, 15);
            this.lblSearch.TabIndex = 2;
            this.lblSearch.Text = "Search Term:";
            // 
            // radioSearchByDie
            // 
            this.radioSearchByDie.AutoSize = true;
            this.radioSearchByDie.Location = new System.Drawing.Point(200, 25);
            this.radioSearchByDie.Name = "radioSearchByDie";
            this.radioSearchByDie.Size = new System.Drawing.Size(160, 19);
            this.radioSearchByDie.TabIndex = 1;
            this.radioSearchByDie.Text = "Search by Die Number (FGT)";
            this.radioSearchByDie.UseVisualStyleBackColor = true;
            // 
            // radioSearchByPart
            // 
            this.radioSearchByPart.AutoSize = true;
            this.radioSearchByPart.Checked = true;
            this.radioSearchByPart.Location = new System.Drawing.Point(20, 25);
            this.radioSearchByPart.Name = "radioSearchByPart";
            this.radioSearchByPart.Size = new System.Drawing.Size(150, 19);
            this.radioSearchByPart.TabIndex = 0;
            this.radioSearchByPart.TabStop = true;
            this.radioSearchByPart.Text = "Search by Part Number";
            this.radioSearchByPart.UseVisualStyleBackColor = true;
            // 
            // tabDiscovery
            // 
            this.tabDiscovery.Location = new System.Drawing.Point(4, 24);
            this.tabDiscovery.Name = "tabDiscovery";
            this.tabDiscovery.Padding = new System.Windows.Forms.Padding(3);
            this.tabDiscovery.Size = new System.Drawing.Size(792, 572);
            this.tabDiscovery.TabIndex = 1;
            this.tabDiscovery.Text = "Discovery";
            this.tabDiscovery.UseVisualStyleBackColor = true;
            // 
            // Control_DieToolDiscovery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlMain);
            this.Name = "Control_DieToolDiscovery";
            this.Size = new System.Drawing.Size(800, 600);
            this.tabControlMain.ResumeLayout(false);
            this.tabLocator.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridResults)).EndInit();
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabLocator;
        private System.Windows.Forms.TabPage tabDiscovery;
        private System.Windows.Forms.GroupBox grpSearch;
        private System.Windows.Forms.RadioButton radioSearchByDie;
        private System.Windows.Forms.RadioButton radioSearchByPart;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView gridResults;
    }
}
