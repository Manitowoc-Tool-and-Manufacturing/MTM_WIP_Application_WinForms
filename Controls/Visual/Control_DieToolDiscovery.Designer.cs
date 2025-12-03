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
            txtSearch = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBoxWithLabel();
            btnSearch = new Button();
            panel1 = new Panel();
            gridResults = new DataGridView();
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
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(txtSearch, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(760, 29);
            tableLayoutPanel2.TabIndex = 6;
            // 
            // txtSearch
            // 
            txtSearch.AutoSize = true;
            txtSearch.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            txtSearch.Dock = DockStyle.Fill;
            txtSearch.EnableSuggestions = true;
            txtSearch.LabelText = "Search Term";
            txtSearch.LabelVisible = "False";
            txtSearch.Location = new Point(3, 3);
            txtSearch.Margin = new Padding(3, 3, 3, 3);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Enter Part Number or Die Number";
            txtSearch.ShowF4Button = true;
            txtSearch.Size = new Size(754, 23);
            txtSearch.TabIndex = 3;
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
            txtCoilSearch.AutoSize = true;
            txtCoilSearch.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            txtCoilSearch.Dock = DockStyle.Fill;
            txtCoilSearch.LabelVisible = "False";
            txtCoilSearch.Location = new Point(6, 6);
            txtCoilSearch.Margin = new Padding(6);
            txtCoilSearch.MaxLength = 130;
            txtCoilSearch.MinimumSize = new Size(0, 23);
            txtCoilSearch.MinLength = 130;
            txtCoilSearch.Name = "txtCoilSearch";
            txtCoilSearch.PlaceholderText = "Coil / Flatstock Number";
            txtCoilSearch.Size = new Size(760, 23);
            txtCoilSearch.TabIndex = 3;
            // 
            // btnCoilSearch
            // 
            btnCoilSearch.Dock = DockStyle.Fill;
            btnCoilSearch.Location = new Point(6, 41);
            btnCoilSearch.Margin = new Padding(6);
            btnCoilSearch.Name = "btnCoilSearch";
            btnCoilSearch.Size = new Size(760, 30);
            btnCoilSearch.TabIndex = 4;
            btnCoilSearch.Text = "Search";
            btnCoilSearch.UseVisualStyleBackColor = true;
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
            tableLayoutCoilDetails.Location = new Point(3, 80);
            tableLayoutCoilDetails.Name = "tableLayoutCoilDetails";
            tableLayoutCoilDetails.RowCount = 6;
            tableLayoutCoilDetails.RowStyles.Add(new RowStyle());
            tableLayoutCoilDetails.RowStyles.Add(new RowStyle());
            tableLayoutCoilDetails.RowStyles.Add(new RowStyle());
            tableLayoutCoilDetails.RowStyles.Add(new RowStyle());
            tableLayoutCoilDetails.RowStyles.Add(new RowStyle());
            tableLayoutCoilDetails.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutCoilDetails.Size = new Size(766, 219);
            tableLayoutCoilDetails.TabIndex = 5;
            // 
            // txtThickness
            // 
            txtThickness.AutoSize = true;
            txtThickness.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            txtThickness.Dock = DockStyle.Fill;
            txtThickness.Enabled = false;
            txtThickness.EnableSuggestions = false;
            txtThickness.LabelText = "Thickness";
            txtThickness.LabelVisible = "True";
            txtThickness.Location = new Point(6, 6);
            txtThickness.Margin = new Padding(6);
            txtThickness.MaxLength = 130;
            txtThickness.MinimumSize = new Size(0, 23);
            txtThickness.MinLength = 130;
            txtThickness.Name = "txtThickness";
            txtThickness.ShowF4Button = false;
            txtThickness.Size = new Size(371, 23);
            txtThickness.TabIndex = 10;
            // 
            // txtWidth
            // 
            txtWidth.AutoSize = true;
            txtWidth.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            txtWidth.Dock = DockStyle.Fill;
            txtWidth.Enabled = false;
            txtWidth.EnableSuggestions = false;
            txtWidth.LabelText = "Width";
            txtWidth.LabelVisible = "True";
            txtWidth.Location = new Point(389, 6);
            txtWidth.Margin = new Padding(6);
            txtWidth.MaxLength = 130;
            txtWidth.MinimumSize = new Size(0, 23);
            txtWidth.MinLength = 130;
            txtWidth.Name = "txtWidth";
            txtWidth.ShowF4Button = false;
            txtWidth.Size = new Size(371, 23);
            txtWidth.TabIndex = 11;
            // 
            // txtLength
            // 
            txtLength.AutoSize = true;
            txtLength.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            txtLength.Dock = DockStyle.Fill;
            txtLength.Enabled = false;
            txtLength.EnableSuggestions = false;
            txtLength.LabelText = "Length";
            txtLength.LabelVisible = "True";
            txtLength.Location = new Point(6, 41);
            txtLength.Margin = new Padding(6);
            txtLength.MaxLength = 130;
            txtLength.MinimumSize = new Size(0, 23);
            txtLength.MinLength = 130;
            txtLength.Name = "txtLength";
            txtLength.ShowF4Button = false;
            txtLength.Size = new Size(371, 23);
            txtLength.TabIndex = 12;
            // 
            // txtGauge
            // 
            txtGauge.AutoSize = true;
            txtGauge.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            txtGauge.Dock = DockStyle.Fill;
            txtGauge.Enabled = false;
            txtGauge.EnableSuggestions = false;
            txtGauge.LabelText = "Ga.";
            txtGauge.LabelVisible = "True";
            txtGauge.Location = new Point(389, 41);
            txtGauge.Margin = new Padding(6);
            txtGauge.MaxLength = 130;
            txtGauge.MinimumSize = new Size(0, 23);
            txtGauge.MinLength = 130;
            txtGauge.Name = "txtGauge";
            txtGauge.ShowF4Button = false;
            txtGauge.Size = new Size(371, 23);
            txtGauge.TabIndex = 13;
            // 
            // txtWhereUsed
            // 
            txtWhereUsed.AutoSize = true;
            txtWhereUsed.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            txtWhereUsed.Dock = DockStyle.Fill;
            txtWhereUsed.Enabled = false;
            txtWhereUsed.EnableSuggestions = false;
            txtWhereUsed.LabelText = "Where Used";
            txtWhereUsed.LabelVisible = "True";
            txtWhereUsed.Location = new Point(6, 76);
            txtWhereUsed.Margin = new Padding(6);
            txtWhereUsed.MaxLength = 130;
            txtWhereUsed.MinimumSize = new Size(0, 23);
            txtWhereUsed.MinLength = 130;
            txtWhereUsed.Name = "txtWhereUsed";
            txtWhereUsed.ShowF4Button = false;
            txtWhereUsed.Size = new Size(371, 23);
            txtWhereUsed.TabIndex = 14;
            // 
            // txtProgression
            // 
            txtProgression.AutoSize = true;
            txtProgression.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            txtProgression.Dock = DockStyle.Fill;
            txtProgression.Enabled = false;
            txtProgression.EnableSuggestions = false;
            txtProgression.LabelText = "Progression";
            txtProgression.LabelVisible = "True";
            txtProgression.Location = new Point(389, 76);
            txtProgression.Margin = new Padding(6);
            txtProgression.MaxLength = 130;
            txtProgression.MinimumSize = new Size(0, 23);
            txtProgression.MinLength = 130;
            txtProgression.Name = "txtProgression";
            txtProgression.ShowF4Button = false;
            txtProgression.Size = new Size(371, 23);
            txtProgression.TabIndex = 15;
            // 
            // txtCustomer
            // 
            txtCustomer.AutoSize = true;
            txtCustomer.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            txtCustomer.Dock = DockStyle.Fill;
            txtCustomer.Enabled = false;
            txtCustomer.EnableSuggestions = false;
            txtCustomer.LabelText = "Customer";
            txtCustomer.LabelVisible = "True";
            txtCustomer.Location = new Point(6, 111);
            txtCustomer.Margin = new Padding(6);
            txtCustomer.MaxLength = 130;
            txtCustomer.MinimumSize = new Size(0, 23);
            txtCustomer.MinLength = 130;
            txtCustomer.Name = "txtCustomer";
            txtCustomer.ShowF4Button = false;
            txtCustomer.Size = new Size(371, 23);
            txtCustomer.TabIndex = 16;
            // 
            // txtScrapLocation
            // 
            txtScrapLocation.AutoSize = true;
            txtScrapLocation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            txtScrapLocation.Dock = DockStyle.Fill;
            txtScrapLocation.Enabled = false;
            txtScrapLocation.EnableSuggestions = false;
            txtScrapLocation.LabelText = "Scrap Location";
            txtScrapLocation.LabelVisible = "True";
            txtScrapLocation.Location = new Point(389, 111);
            txtScrapLocation.Margin = new Padding(6);
            txtScrapLocation.MaxLength = 130;
            txtScrapLocation.MinimumSize = new Size(0, 23);
            txtScrapLocation.MinLength = 130;
            txtScrapLocation.Name = "txtScrapLocation";
            txtScrapLocation.ShowF4Button = false;
            txtScrapLocation.Size = new Size(371, 23);
            txtScrapLocation.TabIndex = 17;
            // 
            // txtGenericType
            // 
            txtGenericType.AutoSize = true;
            txtGenericType.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            txtGenericType.Dock = DockStyle.Fill;
            txtGenericType.Enabled = false;
            txtGenericType.EnableSuggestions = false;
            txtGenericType.LabelText = "Generic Type";
            txtGenericType.LabelVisible = "True";
            txtGenericType.Location = new Point(6, 146);
            txtGenericType.Margin = new Padding(6);
            txtGenericType.MaxLength = 130;
            txtGenericType.MinimumSize = new Size(0, 23);
            txtGenericType.MinLength = 130;
            txtGenericType.Name = "txtGenericType";
            txtGenericType.ShowF4Button = false;
            txtGenericType.Size = new Size(371, 23);
            txtGenericType.TabIndex = 18;
            // 
            // txtDetailedType
            // 
            txtDetailedType.AutoSize = true;
            txtDetailedType.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            txtDetailedType.Dock = DockStyle.Fill;
            txtDetailedType.Enabled = false;
            txtDetailedType.EnableSuggestions = false;
            txtDetailedType.LabelText = "Detailed Type";
            txtDetailedType.LabelVisible = "True";
            txtDetailedType.Location = new Point(389, 146);
            txtDetailedType.Margin = new Padding(6);
            txtDetailedType.MaxLength = 130;
            txtDetailedType.MinimumSize = new Size(0, 23);
            txtDetailedType.MinLength = 130;
            txtDetailedType.Name = "txtDetailedType";
            txtDetailedType.ShowF4Button = false;
            txtDetailedType.Size = new Size(371, 23);
            txtDetailedType.TabIndex = 19;
            // 
            // Control_DieToolDiscovery
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
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
            tabCoilFlatstock.ResumeLayout(false);
            tableLayoutCoil.ResumeLayout(false);
            tableLayoutCoil.PerformLayout();
            tableLayoutCoilDetails.ResumeLayout(false);
            tableLayoutCoilDetails.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabLocator;
        private System.Windows.Forms.GroupBox grpSearch;
        private Shared.SuggestionTextBoxWithLabel txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel panel1;
        private DataGridView gridResults;

        private System.Windows.Forms.TabPage tabCoilFlatstock;
        private TableLayoutPanel tableLayoutCoil;
        private Shared.SuggestionTextBoxWithLabel txtCoilSearch;
        private Button btnCoilSearch;
        private TableLayoutPanel tableLayoutCoilDetails;
        private Shared.SuggestionTextBoxWithLabel txtThickness;
        private Shared.SuggestionTextBoxWithLabel txtWidth;
        private Shared.SuggestionTextBoxWithLabel txtLength;
        private Shared.SuggestionTextBoxWithLabel txtGauge;
        private Shared.SuggestionTextBoxWithLabel txtWhereUsed;
        private Shared.SuggestionTextBoxWithLabel txtProgression;
        private Shared.SuggestionTextBoxWithLabel txtCustomer;
        private Shared.SuggestionTextBoxWithLabel txtScrapLocation;
        private Shared.SuggestionTextBoxWithLabel txtGenericType;
        private Shared.SuggestionTextBoxWithLabel txtDetailedType;
    }
}
