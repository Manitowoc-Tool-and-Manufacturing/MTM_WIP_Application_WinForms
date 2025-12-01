namespace MTM_WIP_Application_Winforms.Controls.Visual
{
    partial class Control_DieToolDiscovery
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControlMain = new TabControl();
            tabLocator = new TabPage();
            grpSearch = new GroupBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            txtSearch = new TextBox();
            lblSearch = new Label();
            btnSearch = new Button();
            panel1 = new Panel();
            gridResults = new DataGridView();

            // Coil Tab Initialization
            tabCoilFlatstock = new TabPage();
            tableLayoutCoil = new TableLayoutPanel();
            txtCoilSearch = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            btnCoilSearch = new Button();
            tableLayoutCoilDetails = new TableLayoutPanel();
            txtThickness = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            txtWidth = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            txtLength = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            txtGauge = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            txtWhereUsed = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            txtProgression = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            txtCustomer = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            txtScrapLocation = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            txtGenericType = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            txtDetailedType = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();

            tabControlMain.SuspendLayout();
            tabLocator.SuspendLayout();
            grpSearch.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridResults).BeginInit();
            tabCoilFlatstock.SuspendLayout();
            tableLayoutCoil.SuspendLayout();
            tableLayoutCoilDetails.SuspendLayout();
            SuspendLayout();
            // 
            // tabControlMain
            // 
            tabControlMain.Controls.Add(tabLocator);
            tabControlMain.Controls.Add(tabCoilFlatstock);
            tabControlMain.Dock = DockStyle.Fill;
            tabControlMain.Location = new Point(0, 0);
            tabControlMain.Name = "tabControlMain";
            tabControlMain.SelectedIndex = 0;
            tabControlMain.Size = new Size(800, 350);
            tabControlMain.TabIndex = 0;
            // 
            // tabLocator
            // 
            tabLocator.Controls.Add(grpSearch);
            tabLocator.Location = new Point(4, 24);
            tabLocator.Name = "tabLocator";
            tabLocator.Padding = new Padding(10);
            tabLocator.Size = new Size(792, 322);
            tabLocator.TabIndex = 0;
            tabLocator.Text = "Die Locator";
            tabLocator.UseVisualStyleBackColor = true;
            // 
            // grpSearch
            // 
            grpSearch.AutoSize = true;
            grpSearch.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            grpSearch.Controls.Add(tableLayoutPanel1);
            grpSearch.Dock = DockStyle.Fill;
            grpSearch.Location = new Point(10, 10);
            grpSearch.Name = "grpSearch";
            grpSearch.Size = new Size(772, 302);
            grpSearch.TabIndex = 0;
            grpSearch.TabStop = false;
            grpSearch.Text = "Search Criteria";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(btnSearch, 0, 1);
            tableLayoutPanel1.Controls.Add(panel1, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 19);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(766, 280);
            tableLayoutPanel1.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(txtSearch, 1, 0);
            tableLayoutPanel2.Controls.Add(lblSearch, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(760, 29);
            tableLayoutPanel2.TabIndex = 6;
            // 
            // txtSearch
            // 
            txtSearch.Dock = DockStyle.Fill;
            txtSearch.Location = new Point(84, 3);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(673, 23);
            txtSearch.TabIndex = 3;
            txtSearch.KeyDown += txtSearch_KeyDown;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Dock = DockStyle.Fill;
            lblSearch.Location = new Point(3, 3);
            lblSearch.Margin = new Padding(3);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(75, 23);
            lblSearch.TabIndex = 2;
            lblSearch.Text = "Search Term:";
            lblSearch.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnSearch
            // 
            btnSearch.AutoSize = true;
            btnSearch.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnSearch.Dock = DockStyle.Fill;
            btnSearch.Location = new Point(3, 38);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(760, 25);
            btnSearch.TabIndex = 4;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // panel1
            // 
            panel1.AutoSize = true;
            panel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(gridResults);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 69);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(10);
            panel1.Size = new Size(760, 208);
            panel1.TabIndex = 7;
            // 
            // gridResults
            // 
            gridResults.AllowUserToAddRows = false;
            gridResults.AllowUserToDeleteRows = false;
            gridResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridResults.Dock = DockStyle.Fill;
            gridResults.Location = new Point(10, 10);
            gridResults.Name = "gridResults";
            gridResults.ReadOnly = true;
            gridResults.Size = new Size(738, 186);
            gridResults.TabIndex = 2;
            // 
            // tabCoilFlatstock
            // 
            tabCoilFlatstock.Controls.Add(tableLayoutCoil);
            tabCoilFlatstock.Location = new Point(4, 24);
            tabCoilFlatstock.Name = "tabCoilFlatstock";
            tabCoilFlatstock.Padding = new Padding(10);
            tabCoilFlatstock.Size = new Size(792, 322);
            tabCoilFlatstock.TabIndex = 1;
            tabCoilFlatstock.Text = "Coil/Flatstock Search";
            tabCoilFlatstock.UseVisualStyleBackColor = true;
            // 
            // tableLayoutCoil
            // 
            tableLayoutCoil.ColumnCount = 1;
            tableLayoutCoil.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutCoil.Controls.Add(txtCoilSearch, 0, 0);
            tableLayoutCoil.Controls.Add(btnCoilSearch, 0, 1);
            tableLayoutCoil.Controls.Add(tableLayoutCoilDetails, 0, 2);
            tableLayoutCoil.Dock = DockStyle.Fill;
            tableLayoutCoil.Location = new Point(10, 10);
            tableLayoutCoil.Name = "tableLayoutCoil";
            tableLayoutCoil.RowCount = 3;
            tableLayoutCoil.RowStyles.Add(new RowStyle());
            tableLayoutCoil.RowStyles.Add(new RowStyle());
            tableLayoutCoil.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutCoil.Size = new Size(772, 302);
            tableLayoutCoil.TabIndex = 0;
            // 
            // txtCoilSearch
            // 
            txtCoilSearch.Dock = DockStyle.Fill;
            txtCoilSearch.LabelText = "Part Number (MMC/MMF)";
            txtCoilSearch.Location = new Point(3, 3);
            txtCoilSearch.Name = "txtCoilSearch";
            txtCoilSearch.Size = new Size(766, 45);
            txtCoilSearch.TabIndex = 0;
            // 
            // btnCoilSearch
            // 
            btnCoilSearch.Dock = DockStyle.Fill;
            btnCoilSearch.Location = new Point(3, 54);
            btnCoilSearch.Name = "btnCoilSearch";
            btnCoilSearch.Size = new Size(766, 30);
            btnCoilSearch.TabIndex = 1;
            btnCoilSearch.Text = "Search";
            btnCoilSearch.UseVisualStyleBackColor = true;
            btnCoilSearch.Click += btnCoilSearch_Click;
            // 
            // tableLayoutCoilDetails
            // 
            tableLayoutCoilDetails.ColumnCount = 2;
            tableLayoutCoilDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutCoilDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutCoilDetails.Controls.Add(txtThickness, 0, 0);
            tableLayoutCoilDetails.Controls.Add(txtWidth, 1, 0);
            tableLayoutCoilDetails.Controls.Add(txtLength, 0, 1);
            tableLayoutCoilDetails.Controls.Add(txtGauge, 1, 1);
            tableLayoutCoilDetails.Controls.Add(txtWhereUsed, 0, 2);
            tableLayoutCoilDetails.Controls.Add(txtProgression, 1, 2);
            tableLayoutCoilDetails.Controls.Add(txtCustomer, 0, 3);
            tableLayoutCoilDetails.Controls.Add(txtScrapLocation, 1, 3);
            tableLayoutCoilDetails.Controls.Add(txtGenericType, 0, 4);
            tableLayoutCoilDetails.Controls.Add(txtDetailedType, 1, 4);
            tableLayoutCoilDetails.Dock = DockStyle.Fill;
            tableLayoutCoilDetails.Location = new Point(3, 90);
            tableLayoutCoilDetails.Name = "tableLayoutCoilDetails";
            tableLayoutCoilDetails.RowCount = 5;
            tableLayoutCoilDetails.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutCoilDetails.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutCoilDetails.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutCoilDetails.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutCoilDetails.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutCoilDetails.Size = new Size(766, 209);
            tableLayoutCoilDetails.TabIndex = 2;
            // 
            // txtThickness
            // 
            txtThickness.Dock = DockStyle.Fill;
            txtThickness.LabelText = "Thickness";
            txtThickness.Location = new Point(3, 3);
            txtThickness.Name = "txtThickness";
            txtThickness.Size = new Size(377, 35);
            txtThickness.TabIndex = 0;
            txtThickness.Enabled = false;
            txtThickness.ShowF4Button = false;
            txtThickness.EnableSuggestions = false;
            // 
            // txtWidth
            // 
            txtWidth.Dock = DockStyle.Fill;
            txtWidth.LabelText = "Width";
            txtWidth.Location = new Point(386, 3);
            txtWidth.Name = "txtWidth";
            txtWidth.Size = new Size(377, 35);
            txtWidth.TabIndex = 1;
            txtWidth.Enabled = false;
            txtWidth.ShowF4Button = false;
            txtWidth.EnableSuggestions = false;
            // 
            // txtLength
            // 
            txtLength.Dock = DockStyle.Fill;
            txtLength.LabelText = "Length";
            txtLength.Location = new Point(3, 44);
            txtLength.Name = "txtLength";
            txtLength.Size = new Size(377, 35);
            txtLength.TabIndex = 2;
            txtLength.Enabled = false;
            txtLength.ShowF4Button = false;
            txtLength.EnableSuggestions = false;
            // 
            // txtGauge
            // 
            txtGauge.Dock = DockStyle.Fill;
            txtGauge.LabelText = "Ga.";
            txtGauge.Location = new Point(386, 44);
            txtGauge.Name = "txtGauge";
            txtGauge.Size = new Size(377, 35);
            txtGauge.TabIndex = 3;
            txtGauge.Enabled = false;
            txtGauge.ShowF4Button = false;
            txtGauge.EnableSuggestions = false;
            // 
            // txtWhereUsed
            // 
            txtWhereUsed.Dock = DockStyle.Fill;
            txtWhereUsed.LabelText = "Where Used";
            txtWhereUsed.Location = new Point(3, 85);
            txtWhereUsed.Name = "txtWhereUsed";
            txtWhereUsed.Size = new Size(377, 35);
            txtWhereUsed.TabIndex = 4;
            txtWhereUsed.Enabled = false;
            txtWhereUsed.ShowF4Button = false;
            txtWhereUsed.EnableSuggestions = false;
            // 
            // txtProgression
            // 
            txtProgression.Dock = DockStyle.Fill;
            txtProgression.LabelText = "Progression";
            txtProgression.Location = new Point(386, 85);
            txtProgression.Name = "txtProgression";
            txtProgression.Size = new Size(377, 35);
            txtProgression.TabIndex = 5;
            txtProgression.Enabled = false;
            txtProgression.ShowF4Button = false;
            txtProgression.EnableSuggestions = false;
            // 
            // txtCustomer
            // 
            txtCustomer.Dock = DockStyle.Fill;
            txtCustomer.LabelText = "Customer";
            txtCustomer.Location = new Point(3, 126);
            txtCustomer.Name = "txtCustomer";
            txtCustomer.Size = new Size(377, 35);
            txtCustomer.TabIndex = 6;
            txtCustomer.Enabled = false;
            txtCustomer.ShowF4Button = false;
            txtCustomer.EnableSuggestions = false;
            // 
            // txtScrapLocation
            // 
            txtScrapLocation.Dock = DockStyle.Fill;
            txtScrapLocation.LabelText = "Scrap Location";
            txtScrapLocation.Location = new Point(386, 126);
            txtScrapLocation.Name = "txtScrapLocation";
            txtScrapLocation.Size = new Size(377, 35);
            txtScrapLocation.TabIndex = 7;
            txtScrapLocation.Enabled = false;
            txtScrapLocation.ShowF4Button = false;
            txtScrapLocation.EnableSuggestions = false;
            // 
            // txtGenericType
            // 
            txtGenericType.Dock = DockStyle.Fill;
            txtGenericType.LabelText = "Generic Type";
            txtGenericType.Location = new Point(3, 167);
            txtGenericType.Name = "txtGenericType";
            txtGenericType.Size = new Size(377, 39);
            txtGenericType.TabIndex = 8;
            txtGenericType.Enabled = false;
            txtGenericType.ShowF4Button = false;
            txtGenericType.EnableSuggestions = false;
            // 
            // txtDetailedType
            // 
            txtDetailedType.Dock = DockStyle.Fill;
            txtDetailedType.LabelText = "Detailed Type";
            txtDetailedType.Location = new Point(386, 167);
            txtDetailedType.Name = "txtDetailedType";
            txtDetailedType.Size = new Size(377, 39);
            txtDetailedType.TabIndex = 9;
            txtDetailedType.Enabled = false;
            txtDetailedType.ShowF4Button = false;
            txtDetailedType.EnableSuggestions = false;
            // 
            // Control_DieToolDiscovery
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(tabControlMain);
            MinimumSize = new Size(800, 350);
            Name = "Control_DieToolDiscovery";
            Size = new Size(800, 350);
            tabControlMain.ResumeLayout(false);
            tabLocator.ResumeLayout(false);
            tabLocator.PerformLayout();
            grpSearch.ResumeLayout(false);
            grpSearch.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridResults).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabLocator;
        private System.Windows.Forms.GroupBox grpSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel panel1;
        private DataGridView gridResults;

        private System.Windows.Forms.TabPage tabCoilFlatstock;
        private System.Windows.Forms.TableLayoutPanel tableLayoutCoil;
        private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel txtCoilSearch;
        private System.Windows.Forms.Button btnCoilSearch;
        private System.Windows.Forms.TableLayoutPanel tableLayoutCoilDetails;
        private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel txtThickness;
        private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel txtWidth;
        private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel txtLength;
        private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel txtGauge;
        private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel txtWhereUsed;
        private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel txtProgression;
        private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel txtCustomer;
        private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel txtScrapLocation;
        private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel txtGenericType;
        private MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel txtDetailedType;
    }
}
