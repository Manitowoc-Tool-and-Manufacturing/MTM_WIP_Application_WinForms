namespace MTM_WIP_Application_Winforms.Forms.Visual
{
    partial class InforVisualDashboard
    {
        private void InitializeComponent()
        {
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.labelTitle = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.textBoxFilter = new System.Windows.Forms.TextBox();
            this.labelFilter = new System.Windows.Forms.Label();
            this.dataGridViewResults = new System.Windows.Forms.DataGridView();
            this.controlEmptyState = new MTM_WIP_Application_Winforms.Controls.Shared.Control_EmptyState();
            this.labelLoading = new System.Windows.Forms.Label();
            this.flowLayoutPanelSidebar = new System.Windows.Forms.FlowLayoutPanel();
            
            this.btnInventory = new System.Windows.Forms.Button();
            this.btnReceiving = new System.Windows.Forms.Button();
            this.btnShipping = new System.Windows.Forms.Button();
            this.btnInventoryAuditing = new System.Windows.Forms.Button();
            this.btnDieToolDiscovery = new System.Windows.Forms.Button();
            this.btnMaterialHandlerGeneral = new System.Windows.Forms.Button();
            this.btnMaterialHandlerTeam = new System.Windows.Forms.Button();

            this.panelSidebar.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).BeginInit();
            this.flowLayoutPanelSidebar.SuspendLayout();
            this.SuspendLayout();

            // panelSidebar
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Width = 220;
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.panelSidebar.Padding = new System.Windows.Forms.Padding(10);
            this.panelSidebar.Controls.Add(this.flowLayoutPanelSidebar);
            
            // flowLayoutPanelSidebar
            this.flowLayoutPanelSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelSidebar.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelSidebar.Controls.Add(this.btnInventory);
            this.flowLayoutPanelSidebar.Controls.Add(this.btnReceiving);
            this.flowLayoutPanelSidebar.Controls.Add(this.btnShipping);
            this.flowLayoutPanelSidebar.Controls.Add(this.btnInventoryAuditing);
            this.flowLayoutPanelSidebar.Controls.Add(this.btnDieToolDiscovery);
            this.flowLayoutPanelSidebar.Controls.Add(this.btnMaterialHandlerGeneral);
            this.flowLayoutPanelSidebar.Controls.Add(this.btnMaterialHandlerTeam);

            // Buttons Styling
            System.Drawing.Size btnSize = new System.Drawing.Size(200, 45);
            System.Windows.Forms.Padding btnMargin = new System.Windows.Forms.Padding(0, 0, 0, 10);
            System.Drawing.Color btnBackColor = System.Drawing.Color.White;
            System.Windows.Forms.FlatStyle btnFlatStyle = System.Windows.Forms.FlatStyle.Flat;

            this.btnInventory.Text = "Inventory";
            this.btnInventory.Size = btnSize;
            this.btnInventory.Margin = btnMargin;
            this.btnInventory.BackColor = btnBackColor;
            this.btnInventory.FlatStyle = btnFlatStyle;
            this.btnInventory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInventory.Click += new System.EventHandler(this.CategoryButton_Click);
            this.btnInventory.Tag = MTM_WIP_Application_Winforms.Models.Enum_VisualDashboardCategory.Inventory;

            this.btnReceiving.Text = "Receiving";
            this.btnReceiving.Size = btnSize;
            this.btnReceiving.Margin = btnMargin;
            this.btnReceiving.BackColor = btnBackColor;
            this.btnReceiving.FlatStyle = btnFlatStyle;
            this.btnReceiving.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReceiving.Click += new System.EventHandler(this.CategoryButton_Click);
            this.btnReceiving.Tag = MTM_WIP_Application_Winforms.Models.Enum_VisualDashboardCategory.Receiving;

            this.btnShipping.Text = "Shipping";
            this.btnShipping.Size = btnSize;
            this.btnShipping.Margin = btnMargin;
            this.btnShipping.BackColor = btnBackColor;
            this.btnShipping.FlatStyle = btnFlatStyle;
            this.btnShipping.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnShipping.Click += new System.EventHandler(this.CategoryButton_Click);
            this.btnShipping.Tag = MTM_WIP_Application_Winforms.Models.Enum_VisualDashboardCategory.Shipping;

            this.btnInventoryAuditing.Text = "Inventory Auditing";
            this.btnInventoryAuditing.Size = btnSize;
            this.btnInventoryAuditing.Margin = btnMargin;
            this.btnInventoryAuditing.BackColor = btnBackColor;
            this.btnInventoryAuditing.FlatStyle = btnFlatStyle;
            this.btnInventoryAuditing.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInventoryAuditing.Click += new System.EventHandler(this.CategoryButton_Click);
            this.btnInventoryAuditing.Tag = MTM_WIP_Application_Winforms.Models.Enum_VisualDashboardCategory.InventoryAuditing;

            this.btnDieToolDiscovery.Text = "Die Tool Discovery";
            this.btnDieToolDiscovery.Size = btnSize;
            this.btnDieToolDiscovery.Margin = btnMargin;
            this.btnDieToolDiscovery.BackColor = btnBackColor;
            this.btnDieToolDiscovery.FlatStyle = btnFlatStyle;
            this.btnDieToolDiscovery.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDieToolDiscovery.Click += new System.EventHandler(this.CategoryButton_Click);
            this.btnDieToolDiscovery.Tag = MTM_WIP_Application_Winforms.Models.Enum_VisualDashboardCategory.DieToolDiscovery;

            this.btnMaterialHandlerGeneral.Text = "MH Analytics (General)";
            this.btnMaterialHandlerGeneral.Size = btnSize;
            this.btnMaterialHandlerGeneral.Margin = btnMargin;
            this.btnMaterialHandlerGeneral.BackColor = btnBackColor;
            this.btnMaterialHandlerGeneral.FlatStyle = btnFlatStyle;
            this.btnMaterialHandlerGeneral.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMaterialHandlerGeneral.Click += new System.EventHandler(this.CategoryButton_Click);
            this.btnMaterialHandlerGeneral.Tag = MTM_WIP_Application_Winforms.Models.Enum_VisualDashboardCategory.MaterialHandlerAnalytics_General;

            this.btnMaterialHandlerTeam.Text = "MH Analytics (Team)";
            this.btnMaterialHandlerTeam.Size = btnSize;
            this.btnMaterialHandlerTeam.Margin = btnMargin;
            this.btnMaterialHandlerTeam.BackColor = btnBackColor;
            this.btnMaterialHandlerTeam.FlatStyle = btnFlatStyle;
            this.btnMaterialHandlerTeam.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMaterialHandlerTeam.Click += new System.EventHandler(this.CategoryButton_Click);
            this.btnMaterialHandlerTeam.Tag = MTM_WIP_Application_Winforms.Models.Enum_VisualDashboardCategory.MaterialHandlerAnalytics_Team;

            // panelContent
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Controls.Add(this.dataGridViewResults);
            this.panelContent.Controls.Add(this.controlEmptyState);
            this.panelContent.Controls.Add(this.labelLoading);
            this.panelContent.Controls.Add(this.panelHeader);

            // panelHeader
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Height = 60;
            this.panelHeader.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.panelHeader.Controls.Add(this.btnExport);
            this.panelHeader.Controls.Add(this.textBoxFilter);
            this.panelHeader.Controls.Add(this.labelFilter);
            this.panelHeader.Controls.Add(this.labelTitle);

            // labelTitle
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.labelTitle.Text = "Dashboard";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // labelFilter
            this.labelFilter.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelFilter.AutoSize = true;
            this.labelFilter.Text = "Filter:";
            this.labelFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.labelFilter.Padding = new System.Windows.Forms.Padding(0, 12, 5, 0);

            // textBoxFilter
            this.textBoxFilter.Dock = System.Windows.Forms.DockStyle.Right;
            this.textBoxFilter.Width = 200;
            this.textBoxFilter.Height = 30;
            this.textBoxFilter.Margin = new System.Windows.Forms.Padding(0, 10, 10, 10);
            this.textBoxFilter.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);

            // btnExport
            this.btnExport.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnExport.Text = "Export to Excel";
            this.btnExport.Width = 120;
            this.btnExport.Height = 40;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            this.btnExport.Visible = false;

            // Control_ReceivingAnalytics_DataGridView_Results
            this.dataGridViewResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewResults.Visible = false;
            this.dataGridViewResults.AllowUserToAddRows = false;
            this.dataGridViewResults.AllowUserToDeleteRows = false;
            this.dataGridViewResults.ReadOnly = true;
            this.dataGridViewResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewResults.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewResults.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewResults.RowHeadersVisible = false;

            // controlEmptyState
            this.controlEmptyState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlEmptyState.Visible = true;
            this.controlEmptyState.Message = "Select a category to view data.";

            // labelLoading
            this.labelLoading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelLoading.Text = "Loading...";
            this.labelLoading.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.labelLoading.Visible = false;

            // Form
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelSidebar);
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Text = "Infor Visual Dashboard";

            this.panelSidebar.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResults)).EndInit();
            this.flowLayoutPanelSidebar.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.DataGridView dataGridViewResults;
        private MTM_WIP_Application_Winforms.Controls.Shared.Control_EmptyState controlEmptyState;
        private System.Windows.Forms.Label labelLoading;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSidebar;
        private System.Windows.Forms.Button btnInventory;
        private System.Windows.Forms.Button btnReceiving;
        private System.Windows.Forms.Button btnShipping;
        private System.Windows.Forms.Button btnInventoryAuditing;
        private System.Windows.Forms.Button btnDieToolDiscovery;
        private System.Windows.Forms.Button btnMaterialHandlerGeneral;
        private System.Windows.Forms.Button btnMaterialHandlerTeam;
        private System.Windows.Forms.TextBox textBoxFilter;
        private System.Windows.Forms.Label labelFilter;
    }
}
