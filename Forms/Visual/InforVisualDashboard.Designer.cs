namespace MTM_WIP_Application_Winforms.Forms.Visual
{
    partial class InforVisualDashboard
    {
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InforVisualDashboard));
            panelContent = new Panel();
            labelLoading = new Label();
            controlEmptyState = new MTM_WIP_Application_Winforms.Controls.Shared.Control_EmptyState();
            tableLayoutPanelMain = new TableLayoutPanel();
            panelNavigation = new Panel();
            tableLayoutPanelNavigation = new TableLayoutPanel();
            btnInventory = new Button();
            btnReceiving = new Button();
            btnShipping = new Button();
            btnInventoryAuditing = new Button();
            btnDieToolDiscovery = new Button();
            btnMaterialHandlerGeneral = new Button();
            btnMaterialHandlerTeam = new Button();
            labelReadOnly = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            panelContent.SuspendLayout();
            tableLayoutPanelMain.SuspendLayout();
            panelNavigation.SuspendLayout();
            tableLayoutPanelNavigation.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panelContent
            // 
            panelContent.AutoSize = true;
            panelContent.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panelContent.Controls.Add(tableLayoutPanel1);
            panelContent.Controls.Add(controlEmptyState);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(160, 3);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(586, 521);
            panelContent.TabIndex = 0;
            // 
            // labelLoading
            // 
            labelLoading.Dock = DockStyle.Fill;
            labelLoading.Font = new Font("Segoe UI Emoji", 14F);
            labelLoading.Location = new Point(3, 107);
            labelLoading.Margin = new Padding(3);
            labelLoading.Name = "labelLoading";
            labelLoading.Size = new Size(580, 411);
            labelLoading.TabIndex = 2;
            labelLoading.Text = "Loading...";
            labelLoading.TextAlign = ContentAlignment.MiddleCenter;
            labelLoading.Visible = false;
            // 
            // controlEmptyState
            // 
            controlEmptyState.Action = null;
            controlEmptyState.ActionText = "Retry";
            controlEmptyState.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            controlEmptyState.Dock = DockStyle.Fill;
            controlEmptyState.Image = null;
            controlEmptyState.Location = new Point(0, 0);
            controlEmptyState.Margin = new Padding(6);
            controlEmptyState.Message = "Select a category to view data.";
            controlEmptyState.Name = "controlEmptyState";
            controlEmptyState.Size = new Size(586, 521);
            controlEmptyState.TabIndex = 1;
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.AutoSize = true;
            tableLayoutPanelMain.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanelMain.ColumnCount = 2;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Controls.Add(panelNavigation, 0, 0);
            tableLayoutPanelMain.Controls.Add(panelContent, 1, 0);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.Location = new Point(0, 0);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 1;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Size = new Size(749, 527);
            tableLayoutPanelMain.TabIndex = 5;
            // 
            // panelNavigation
            // 
            panelNavigation.AutoSize = true;
            panelNavigation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panelNavigation.BorderStyle = BorderStyle.FixedSingle;
            panelNavigation.Controls.Add(tableLayoutPanelNavigation);
            panelNavigation.Dock = DockStyle.Fill;
            panelNavigation.Location = new Point(3, 3);
            panelNavigation.Name = "panelNavigation";
            panelNavigation.Size = new Size(151, 521);
            panelNavigation.TabIndex = 3;
            // 
            // tableLayoutPanelNavigation
            // 
            tableLayoutPanelNavigation.AutoSize = true;
            tableLayoutPanelNavigation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanelNavigation.ColumnCount = 1;
            tableLayoutPanelNavigation.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelNavigation.Controls.Add(btnInventory, 0, 0);
            tableLayoutPanelNavigation.Controls.Add(btnReceiving, 0, 1);
            tableLayoutPanelNavigation.Controls.Add(btnShipping, 0, 2);
            tableLayoutPanelNavigation.Controls.Add(btnInventoryAuditing, 0, 3);
            tableLayoutPanelNavigation.Controls.Add(btnDieToolDiscovery, 0, 4);
            tableLayoutPanelNavigation.Controls.Add(btnMaterialHandlerGeneral, 0, 5);
            tableLayoutPanelNavigation.Controls.Add(btnMaterialHandlerTeam, 0, 6);
            tableLayoutPanelNavigation.Dock = DockStyle.Fill;
            tableLayoutPanelNavigation.Location = new Point(0, 0);
            tableLayoutPanelNavigation.Name = "tableLayoutPanelNavigation";
            tableLayoutPanelNavigation.RowCount = 8;
            tableLayoutPanelNavigation.RowStyles.Add(new RowStyle());
            tableLayoutPanelNavigation.RowStyles.Add(new RowStyle());
            tableLayoutPanelNavigation.RowStyles.Add(new RowStyle());
            tableLayoutPanelNavigation.RowStyles.Add(new RowStyle());
            tableLayoutPanelNavigation.RowStyles.Add(new RowStyle());
            tableLayoutPanelNavigation.RowStyles.Add(new RowStyle());
            tableLayoutPanelNavigation.RowStyles.Add(new RowStyle());
            tableLayoutPanelNavigation.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelNavigation.Size = new Size(149, 519);
            tableLayoutPanelNavigation.TabIndex = 0;
            // 
            // btnInventory
            // 
            btnInventory.BackColor = Color.White;
            btnInventory.Dock = DockStyle.Fill;
            btnInventory.FlatStyle = FlatStyle.Flat;
            btnInventory.Location = new Point(3, 3);
            btnInventory.Name = "btnInventory";
            btnInventory.Size = new Size(143, 45);
            btnInventory.TabIndex = 7;
            btnInventory.Tag = Models.Enum_VisualDashboardCategory.Inventory;
            btnInventory.Text = "Inventory";
            btnInventory.TextAlign = ContentAlignment.MiddleLeft;
            btnInventory.UseVisualStyleBackColor = false;
            // 
            // btnReceiving
            // 
            btnReceiving.BackColor = Color.White;
            btnReceiving.Dock = DockStyle.Fill;
            btnReceiving.FlatStyle = FlatStyle.Flat;
            btnReceiving.Location = new Point(3, 54);
            btnReceiving.Name = "btnReceiving";
            btnReceiving.Size = new Size(143, 45);
            btnReceiving.TabIndex = 8;
            btnReceiving.Tag = Models.Enum_VisualDashboardCategory.Receiving;
            btnReceiving.Text = "Receiving";
            btnReceiving.TextAlign = ContentAlignment.MiddleLeft;
            btnReceiving.UseVisualStyleBackColor = false;
            // 
            // btnShipping
            // 
            btnShipping.BackColor = Color.White;
            btnShipping.Dock = DockStyle.Fill;
            btnShipping.FlatStyle = FlatStyle.Flat;
            btnShipping.Location = new Point(3, 105);
            btnShipping.Name = "btnShipping";
            btnShipping.Size = new Size(143, 45);
            btnShipping.TabIndex = 9;
            btnShipping.Tag = Models.Enum_VisualDashboardCategory.Shipping;
            btnShipping.Text = "Shipping";
            btnShipping.TextAlign = ContentAlignment.MiddleLeft;
            btnShipping.UseVisualStyleBackColor = false;
            btnShipping.Visible = false;
            // 
            // btnInventoryAuditing
            // 
            btnInventoryAuditing.BackColor = Color.White;
            btnInventoryAuditing.Dock = DockStyle.Fill;
            btnInventoryAuditing.FlatStyle = FlatStyle.Flat;
            btnInventoryAuditing.Location = new Point(3, 156);
            btnInventoryAuditing.Name = "btnInventoryAuditing";
            btnInventoryAuditing.Size = new Size(143, 45);
            btnInventoryAuditing.TabIndex = 10;
            btnInventoryAuditing.Tag = Models.Enum_VisualDashboardCategory.InventoryAuditing;
            btnInventoryAuditing.Text = "Inventory Auditing";
            btnInventoryAuditing.TextAlign = ContentAlignment.MiddleLeft;
            btnInventoryAuditing.UseVisualStyleBackColor = false;
            // 
            // btnDieToolDiscovery
            // 
            btnDieToolDiscovery.BackColor = Color.White;
            btnDieToolDiscovery.Dock = DockStyle.Fill;
            btnDieToolDiscovery.FlatStyle = FlatStyle.Flat;
            btnDieToolDiscovery.Location = new Point(3, 207);
            btnDieToolDiscovery.Name = "btnDieToolDiscovery";
            btnDieToolDiscovery.Size = new Size(143, 45);
            btnDieToolDiscovery.TabIndex = 11;
            btnDieToolDiscovery.Tag = Models.Enum_VisualDashboardCategory.DieToolDiscovery;
            btnDieToolDiscovery.Text = "Die Tool Discovery";
            btnDieToolDiscovery.TextAlign = ContentAlignment.MiddleLeft;
            btnDieToolDiscovery.UseVisualStyleBackColor = false;
            // 
            // btnMaterialHandlerGeneral
            // 
            btnMaterialHandlerGeneral.BackColor = Color.White;
            btnMaterialHandlerGeneral.Dock = DockStyle.Fill;
            btnMaterialHandlerGeneral.FlatStyle = FlatStyle.Flat;
            btnMaterialHandlerGeneral.Location = new Point(3, 258);
            btnMaterialHandlerGeneral.Name = "btnMaterialHandlerGeneral";
            btnMaterialHandlerGeneral.Size = new Size(143, 45);
            btnMaterialHandlerGeneral.TabIndex = 12;
            btnMaterialHandlerGeneral.Tag = Models.Enum_VisualDashboardCategory.MaterialHandlerAnalytics_General;
            btnMaterialHandlerGeneral.Text = "MH Analytics (General)";
            btnMaterialHandlerGeneral.TextAlign = ContentAlignment.MiddleLeft;
            btnMaterialHandlerGeneral.UseVisualStyleBackColor = false;
            btnMaterialHandlerGeneral.Visible = false;
            // 
            // btnMaterialHandlerTeam
            // 
            btnMaterialHandlerTeam.BackColor = Color.White;
            btnMaterialHandlerTeam.Dock = DockStyle.Fill;
            btnMaterialHandlerTeam.FlatStyle = FlatStyle.Flat;
            btnMaterialHandlerTeam.Location = new Point(3, 309);
            btnMaterialHandlerTeam.Name = "btnMaterialHandlerTeam";
            btnMaterialHandlerTeam.Size = new Size(143, 45);
            btnMaterialHandlerTeam.TabIndex = 13;
            btnMaterialHandlerTeam.Tag = Models.Enum_VisualDashboardCategory.MaterialHandlerAnalytics_Team;
            btnMaterialHandlerTeam.Text = "MH Analytics (Team)";
            btnMaterialHandlerTeam.TextAlign = ContentAlignment.MiddleLeft;
            btnMaterialHandlerTeam.UseVisualStyleBackColor = false;
            btnMaterialHandlerTeam.Visible = false;
            // 
            // labelReadOnly
            // 
            labelReadOnly.Dock = DockStyle.Fill;
            labelReadOnly.Font = new Font("Segoe UI Emoji", 14F);
            labelReadOnly.ForeColor = Color.Indigo;
            labelReadOnly.Location = new Point(3, 3);
            labelReadOnly.Margin = new Padding(3);
            labelReadOnly.Name = "labelReadOnly";
            labelReadOnly.Size = new Size(580, 98);
            labelReadOnly.TabIndex = 3;
            labelReadOnly.Text = "The Infor Visual Dashboard is a READ ONLY Tool.\r\nThis will and can not change.";
            labelReadOnly.TextAlign = ContentAlignment.MiddleCenter;
            labelReadOnly.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(labelLoading, 0, 1);
            tableLayoutPanel1.Controls.Add(labelReadOnly, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            tableLayoutPanel1.Size = new Size(586, 521);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // InforVisualDashboard
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            ClientSize = new Size(749, 527);
            Controls.Add(tableLayoutPanelMain);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "InforVisualDashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Infor Visual Dashboard";
            panelContent.ResumeLayout(false);
            panelContent.PerformLayout();
            tableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanelMain.PerformLayout();
            panelNavigation.ResumeLayout(false);
            panelNavigation.PerformLayout();
            tableLayoutPanelNavigation.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
        private System.Windows.Forms.Panel panelContent;
        private Controls.Shared.Control_EmptyState controlEmptyState;
        private Label labelLoading;
        private TableLayoutPanel tableLayoutPanelMain;
        private Panel panelNavigation;
        private TableLayoutPanel tableLayoutPanelNavigation;
        private Button btnInventory;
        private Button btnReceiving;
        private Button btnShipping;
        private Button btnInventoryAuditing;
        private Button btnDieToolDiscovery;
        private Button btnMaterialHandlerGeneral;
        private Button btnMaterialHandlerTeam;
        private TableLayoutPanel tableLayoutPanel1;
        private Label labelReadOnly;
    }
}
