namespace MTM_WIP_Application_Winforms.Forms.Visual
{
    partial class InforVisualDashboard
    {
        private void InitializeComponent()
        {
            panelSidebar = new Panel();
            flowLayoutPanelSidebar = new FlowLayoutPanel();
            btnInventory = new Button();
            btnReceiving = new Button();
            btnShipping = new Button();
            btnInventoryAuditing = new Button();
            btnDieToolDiscovery = new Button();
            btnMaterialHandlerGeneral = new Button();
            btnMaterialHandlerTeam = new Button();
            panelContent = new Panel();
            controlEmptyState = new MTM_WIP_Application_Winforms.Controls.Shared.Control_EmptyState();
            labelLoading = new Label();
            panelSidebar.SuspendLayout();
            flowLayoutPanelSidebar.SuspendLayout();
            panelContent.SuspendLayout();
            SuspendLayout();
            // 
            // panelSidebar
            // 
            panelSidebar.BackColor = Color.FromArgb(240, 240, 240);
            panelSidebar.Controls.Add(flowLayoutPanelSidebar);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 0);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Padding = new Padding(10);
            panelSidebar.Size = new Size(220, 700);
            panelSidebar.TabIndex = 1;
            // 
            // flowLayoutPanelSidebar
            // 
            flowLayoutPanelSidebar.Controls.Add(btnInventory);
            flowLayoutPanelSidebar.Controls.Add(btnReceiving);
            flowLayoutPanelSidebar.Controls.Add(btnShipping);
            flowLayoutPanelSidebar.Controls.Add(btnInventoryAuditing);
            flowLayoutPanelSidebar.Controls.Add(btnDieToolDiscovery);
            flowLayoutPanelSidebar.Controls.Add(btnMaterialHandlerGeneral);
            flowLayoutPanelSidebar.Controls.Add(btnMaterialHandlerTeam);
            flowLayoutPanelSidebar.Dock = DockStyle.Fill;
            flowLayoutPanelSidebar.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanelSidebar.Location = new Point(10, 10);
            flowLayoutPanelSidebar.Name = "flowLayoutPanelSidebar";
            flowLayoutPanelSidebar.Size = new Size(200, 680);
            flowLayoutPanelSidebar.TabIndex = 0;
            // 
            // btnInventory
            // 
            btnInventory.BackColor = Color.White;
            btnInventory.FlatStyle = FlatStyle.Flat;
            btnInventory.Location = new Point(0, 0);
            btnInventory.Margin = new Padding(0, 0, 0, 10);
            btnInventory.Name = "btnInventory";
            btnInventory.Size = new Size(200, 45);
            btnInventory.TabIndex = 0;
            btnInventory.Tag = Models.Enum_VisualDashboardCategory.Inventory;
            btnInventory.Text = "Inventory";
            btnInventory.TextAlign = ContentAlignment.MiddleLeft;
            btnInventory.UseVisualStyleBackColor = false;
            btnInventory.Click += CategoryButton_Click;
            // 
            // btnReceiving
            // 
            btnReceiving.BackColor = Color.White;
            btnReceiving.FlatStyle = FlatStyle.Flat;
            btnReceiving.Location = new Point(0, 55);
            btnReceiving.Margin = new Padding(0, 0, 0, 10);
            btnReceiving.Name = "btnReceiving";
            btnReceiving.Size = new Size(200, 45);
            btnReceiving.TabIndex = 1;
            btnReceiving.Tag = Models.Enum_VisualDashboardCategory.Receiving;
            btnReceiving.Text = "Receiving";
            btnReceiving.TextAlign = ContentAlignment.MiddleLeft;
            btnReceiving.UseVisualStyleBackColor = false;
            btnReceiving.Click += CategoryButton_Click;
            // 
            // btnShipping
            // 
            btnShipping.BackColor = Color.White;
            btnShipping.FlatStyle = FlatStyle.Flat;
            btnShipping.Location = new Point(0, 110);
            btnShipping.Margin = new Padding(0, 0, 0, 10);
            btnShipping.Name = "btnShipping";
            btnShipping.Size = new Size(200, 45);
            btnShipping.TabIndex = 2;
            btnShipping.Tag = Models.Enum_VisualDashboardCategory.Shipping;
            btnShipping.Text = "Shipping";
            btnShipping.TextAlign = ContentAlignment.MiddleLeft;
            btnShipping.UseVisualStyleBackColor = false;
            btnShipping.Click += CategoryButton_Click;
            // 
            // btnInventoryAuditing
            // 
            btnInventoryAuditing.BackColor = Color.White;
            btnInventoryAuditing.FlatStyle = FlatStyle.Flat;
            btnInventoryAuditing.Location = new Point(0, 165);
            btnInventoryAuditing.Margin = new Padding(0, 0, 0, 10);
            btnInventoryAuditing.Name = "btnInventoryAuditing";
            btnInventoryAuditing.Size = new Size(200, 45);
            btnInventoryAuditing.TabIndex = 3;
            btnInventoryAuditing.Tag = Models.Enum_VisualDashboardCategory.InventoryAuditing;
            btnInventoryAuditing.Text = "Inventory Auditing";
            btnInventoryAuditing.TextAlign = ContentAlignment.MiddleLeft;
            btnInventoryAuditing.UseVisualStyleBackColor = false;
            btnInventoryAuditing.Click += CategoryButton_Click;
            // 
            // btnDieToolDiscovery
            // 
            btnDieToolDiscovery.BackColor = Color.White;
            btnDieToolDiscovery.FlatStyle = FlatStyle.Flat;
            btnDieToolDiscovery.Location = new Point(0, 220);
            btnDieToolDiscovery.Margin = new Padding(0, 0, 0, 10);
            btnDieToolDiscovery.Name = "btnDieToolDiscovery";
            btnDieToolDiscovery.Size = new Size(200, 45);
            btnDieToolDiscovery.TabIndex = 4;
            btnDieToolDiscovery.Tag = Models.Enum_VisualDashboardCategory.DieToolDiscovery;
            btnDieToolDiscovery.Text = "Die Tool Discovery";
            btnDieToolDiscovery.TextAlign = ContentAlignment.MiddleLeft;
            btnDieToolDiscovery.UseVisualStyleBackColor = false;
            btnDieToolDiscovery.Click += CategoryButton_Click;
            // 
            // btnMaterialHandlerGeneral
            // 
            btnMaterialHandlerGeneral.BackColor = Color.White;
            btnMaterialHandlerGeneral.FlatStyle = FlatStyle.Flat;
            btnMaterialHandlerGeneral.Location = new Point(0, 275);
            btnMaterialHandlerGeneral.Margin = new Padding(0, 0, 0, 10);
            btnMaterialHandlerGeneral.Name = "btnMaterialHandlerGeneral";
            btnMaterialHandlerGeneral.Size = new Size(200, 45);
            btnMaterialHandlerGeneral.TabIndex = 5;
            btnMaterialHandlerGeneral.Tag = Models.Enum_VisualDashboardCategory.MaterialHandlerAnalytics_General;
            btnMaterialHandlerGeneral.Text = "MH Analytics (General)";
            btnMaterialHandlerGeneral.TextAlign = ContentAlignment.MiddleLeft;
            btnMaterialHandlerGeneral.UseVisualStyleBackColor = false;
            btnMaterialHandlerGeneral.Click += CategoryButton_Click;
            // 
            // btnMaterialHandlerTeam
            // 
            btnMaterialHandlerTeam.BackColor = Color.White;
            btnMaterialHandlerTeam.FlatStyle = FlatStyle.Flat;
            btnMaterialHandlerTeam.Location = new Point(0, 330);
            btnMaterialHandlerTeam.Margin = new Padding(0, 0, 0, 10);
            btnMaterialHandlerTeam.Name = "btnMaterialHandlerTeam";
            btnMaterialHandlerTeam.Size = new Size(200, 45);
            btnMaterialHandlerTeam.TabIndex = 6;
            btnMaterialHandlerTeam.Tag = Models.Enum_VisualDashboardCategory.MaterialHandlerAnalytics_Team;
            btnMaterialHandlerTeam.Text = "MH Analytics (Team)";
            btnMaterialHandlerTeam.TextAlign = ContentAlignment.MiddleLeft;
            btnMaterialHandlerTeam.UseVisualStyleBackColor = false;
            btnMaterialHandlerTeam.Click += CategoryButton_Click;
            // 
            // panelContent
            // 
            panelContent.AutoSize = true;
            panelContent.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panelContent.Controls.Add(controlEmptyState);
            panelContent.Controls.Add(labelLoading);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(220, 0);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(880, 700);
            panelContent.TabIndex = 0;
            // 
            // controlEmptyState
            // 
            controlEmptyState.Action = null;
            controlEmptyState.ActionText = "Retry";
            controlEmptyState.AutoSize = true;
            controlEmptyState.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            controlEmptyState.Dock = DockStyle.Fill;
            controlEmptyState.Image = null;
            controlEmptyState.Location = new Point(0, 0);
            controlEmptyState.Message = "Select a category to view data.";
            controlEmptyState.Name = "controlEmptyState";
            controlEmptyState.Size = new Size(880, 700);
            controlEmptyState.TabIndex = 1;
            // 
            // labelLoading
            // 
            labelLoading.Dock = DockStyle.Fill;
            labelLoading.Font = new Font("Segoe UI Emoji", 14F);
            labelLoading.Location = new Point(0, 0);
            labelLoading.Name = "labelLoading";
            labelLoading.Size = new Size(880, 700);
            labelLoading.TabIndex = 2;
            labelLoading.Text = "Loading...";
            labelLoading.TextAlign = ContentAlignment.MiddleCenter;
            labelLoading.Visible = false;
            // 
            // InforVisualDashboard
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1100, 700);
            Controls.Add(panelContent);
            Controls.Add(panelSidebar);
            Name = "InforVisualDashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Infor Visual Dashboard";
            panelSidebar.ResumeLayout(false);
            flowLayoutPanelSidebar.ResumeLayout(false);
            panelContent.ResumeLayout(false);
            panelContent.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Panel panelContent;
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
    }
}
