namespace MTM_WIP_Application_Winforms.Forms.Visual
{
    partial class Form_InforVisualDashboard
    {
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_InforVisualDashboard));
            InforVisualDashboard_Panel_Content = new Panel();
            InforVisualDashboard_TableLayout_Loading = new TableLayoutPanel();
            InforVisualDashboard_Label_Loading = new Label();
            InforVisualDashboard_Label_ReadOnly = new Label();
            InforVisualDashboard_Control_EmptyState = new MTM_WIP_Application_Winforms.Components.Shared.Control_EmptyState();
            InforVisualDashboard_TableLayout_Main = new TableLayoutPanel();
            InforVisualDashboard_Panel_Navigation = new Panel();
            InforVisualDashboard_TableLayout_Navigation = new TableLayoutPanel();
            InforVisualDashboard_Button_Inventory = new Button();
            InforVisualDashboard_Button_Receiving = new Button();
            InforVisualDashboard_Button_Shipping = new Button();
            InforVisualDashboard_Button_InventoryAuditing = new Button();
            InforVisualDashboard_Button_DieToolDiscovery = new Button();
            InforVisualDashboard_Button_MaterialHandlerGeneral = new Button();
            InforVisualDashboard_Button_MaterialHandlerTeam = new Button();
            InforVisualDashboard_Panel_Content.SuspendLayout();
            InforVisualDashboard_TableLayout_Loading.SuspendLayout();
            InforVisualDashboard_TableLayout_Main.SuspendLayout();
            InforVisualDashboard_Panel_Navigation.SuspendLayout();
            InforVisualDashboard_TableLayout_Navigation.SuspendLayout();
            SuspendLayout();
            // 
            // InforVisualDashboard_Panel_Content
            // 
            InforVisualDashboard_Panel_Content.BorderStyle = BorderStyle.FixedSingle;
            InforVisualDashboard_Panel_Content.AutoSize = true;
            InforVisualDashboard_Panel_Content.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            InforVisualDashboard_Panel_Content.MinimumSize = new Size(800, 600);
            InforVisualDashboard_Panel_Content.Controls.Add(InforVisualDashboard_TableLayout_Loading);
            InforVisualDashboard_Panel_Content.Controls.Add(InforVisualDashboard_Control_EmptyState);
            InforVisualDashboard_Panel_Content.Dock = DockStyle.Fill;
            InforVisualDashboard_Panel_Content.Location = new Point(160, 3);
            InforVisualDashboard_Panel_Content.Name = "InforVisualDashboard_Panel_Content";
            InforVisualDashboard_Panel_Content.Size = new Size(861, 762);
            InforVisualDashboard_Panel_Content.TabIndex = 0;
            // 
            // InforVisualDashboard_TableLayout_Loading
            // 
            InforVisualDashboard_TableLayout_Loading.AutoSize = true;
            InforVisualDashboard_TableLayout_Loading.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            InforVisualDashboard_TableLayout_Loading.ColumnCount = 1;
            InforVisualDashboard_TableLayout_Loading.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            InforVisualDashboard_TableLayout_Loading.Controls.Add(InforVisualDashboard_Label_Loading, 0, 1);
            InforVisualDashboard_TableLayout_Loading.Controls.Add(InforVisualDashboard_Label_ReadOnly, 0, 0);
            InforVisualDashboard_TableLayout_Loading.Dock = DockStyle.Fill;
            InforVisualDashboard_TableLayout_Loading.Location = new Point(0, 0);
            InforVisualDashboard_TableLayout_Loading.Name = "InforVisualDashboard_TableLayout_Loading";
            InforVisualDashboard_TableLayout_Loading.RowCount = 2;
            InforVisualDashboard_TableLayout_Loading.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            InforVisualDashboard_TableLayout_Loading.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            InforVisualDashboard_TableLayout_Loading.Size = new Size(861, 762);
            InforVisualDashboard_TableLayout_Loading.TabIndex = 4;
            // 
            // InforVisualDashboard_Label_Loading
            // 
            InforVisualDashboard_Label_Loading.Dock = DockStyle.Fill;
            InforVisualDashboard_Label_Loading.Font = new Font("Segoe UI Emoji", 14F);
            InforVisualDashboard_Label_Loading.Location = new Point(3, 155);
            InforVisualDashboard_Label_Loading.Margin = new Padding(3);
            InforVisualDashboard_Label_Loading.Name = "InforVisualDashboard_Label_Loading";
            InforVisualDashboard_Label_Loading.Size = new Size(855, 604);
            InforVisualDashboard_Label_Loading.TabIndex = 2;
            InforVisualDashboard_Label_Loading.Text = "Loading...";
            InforVisualDashboard_Label_Loading.TextAlign = ContentAlignment.MiddleCenter;
            InforVisualDashboard_Label_Loading.Visible = false;
            // 
            // InforVisualDashboard_Label_ReadOnly
            // 
            InforVisualDashboard_Label_ReadOnly.Dock = DockStyle.Fill;
            InforVisualDashboard_Label_ReadOnly.Font = new Font("Segoe UI Emoji", 14F);
            InforVisualDashboard_Label_ReadOnly.ForeColor = Color.Indigo;
            InforVisualDashboard_Label_ReadOnly.Location = new Point(3, 3);
            InforVisualDashboard_Label_ReadOnly.Margin = new Padding(3);
            InforVisualDashboard_Label_ReadOnly.Name = "InforVisualDashboard_Label_ReadOnly";
            InforVisualDashboard_Label_ReadOnly.Size = new Size(855, 146);
            InforVisualDashboard_Label_ReadOnly.TabIndex = 3;
            InforVisualDashboard_Label_ReadOnly.Text = "The Infor Visual Dashboard is a READ ONLY Tool.\r\nThis will and can not change.";
            InforVisualDashboard_Label_ReadOnly.TextAlign = ContentAlignment.MiddleCenter;
            InforVisualDashboard_Label_ReadOnly.Visible = false;
            // 
            // InforVisualDashboard_Control_EmptyState
            // 
            InforVisualDashboard_Control_EmptyState.Action = null;
            InforVisualDashboard_Control_EmptyState.ActionText = "Retry";
            InforVisualDashboard_Control_EmptyState.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            InforVisualDashboard_Control_EmptyState.Dock = DockStyle.Fill;
            InforVisualDashboard_Control_EmptyState.Image = null;
            InforVisualDashboard_Control_EmptyState.Location = new Point(0, 0);
            InforVisualDashboard_Control_EmptyState.Margin = new Padding(6);
            InforVisualDashboard_Control_EmptyState.Message = "Select a category to view data.";
            InforVisualDashboard_Control_EmptyState.Name = "InforVisualDashboard_Control_EmptyState";
            InforVisualDashboard_Control_EmptyState.Size = new Size(861, 762);
            InforVisualDashboard_Control_EmptyState.TabIndex = 1;
            // 
            // InforVisualDashboard_TableLayout_Main
            // 
            InforVisualDashboard_TableLayout_Main.AutoSize = true;
            InforVisualDashboard_TableLayout_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            InforVisualDashboard_TableLayout_Main.MinimumSize = new Size(1024, 768);
            InforVisualDashboard_TableLayout_Main.ColumnCount = 2;
            InforVisualDashboard_TableLayout_Main.ColumnStyles.Add(new ColumnStyle());
            InforVisualDashboard_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            InforVisualDashboard_TableLayout_Main.Controls.Add(InforVisualDashboard_Panel_Navigation, 0, 0);
            InforVisualDashboard_TableLayout_Main.Controls.Add(InforVisualDashboard_Panel_Content, 1, 0);
            InforVisualDashboard_TableLayout_Main.Dock = DockStyle.Fill;
            InforVisualDashboard_TableLayout_Main.Location = new Point(0, 0);
            InforVisualDashboard_TableLayout_Main.Name = "InforVisualDashboard_TableLayout_Main";
            InforVisualDashboard_TableLayout_Main.RowCount = 1;
            InforVisualDashboard_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            InforVisualDashboard_TableLayout_Main.Size = new Size(1024, 768);
            InforVisualDashboard_TableLayout_Main.TabIndex = 5;
            // 
            // InforVisualDashboard_Panel_Navigation
            // 
            InforVisualDashboard_Panel_Navigation.BorderStyle = BorderStyle.FixedSingle;
            InforVisualDashboard_Panel_Navigation.Controls.Add(InforVisualDashboard_TableLayout_Navigation);
            InforVisualDashboard_Panel_Navigation.Dock = DockStyle.Fill;
            InforVisualDashboard_Panel_Navigation.Location = new Point(3, 3);
            InforVisualDashboard_Panel_Navigation.Name = "InforVisualDashboard_Panel_Navigation";
            InforVisualDashboard_Panel_Navigation.Size = new Size(151, 762);
            InforVisualDashboard_Panel_Navigation.TabIndex = 3;
            // 
            // InforVisualDashboard_TableLayout_Navigation
            // 
            InforVisualDashboard_TableLayout_Navigation.AutoSize = true;
            InforVisualDashboard_TableLayout_Navigation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            InforVisualDashboard_TableLayout_Navigation.ColumnCount = 1;
            InforVisualDashboard_TableLayout_Navigation.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            InforVisualDashboard_TableLayout_Navigation.Controls.Add(InforVisualDashboard_Button_Inventory, 0, 0);
            InforVisualDashboard_TableLayout_Navigation.Controls.Add(InforVisualDashboard_Button_Receiving, 0, 1);
            InforVisualDashboard_TableLayout_Navigation.Controls.Add(InforVisualDashboard_Button_Shipping, 0, 2);
            InforVisualDashboard_TableLayout_Navigation.Controls.Add(InforVisualDashboard_Button_InventoryAuditing, 0, 3);
            InforVisualDashboard_TableLayout_Navigation.Controls.Add(InforVisualDashboard_Button_DieToolDiscovery, 0, 4);
            InforVisualDashboard_TableLayout_Navigation.Controls.Add(InforVisualDashboard_Button_MaterialHandlerGeneral, 0, 5);
            InforVisualDashboard_TableLayout_Navigation.Controls.Add(InforVisualDashboard_Button_MaterialHandlerTeam, 0, 6);
            InforVisualDashboard_TableLayout_Navigation.Dock = DockStyle.Fill;
            InforVisualDashboard_TableLayout_Navigation.Location = new Point(0, 0);
            InforVisualDashboard_TableLayout_Navigation.Name = "InforVisualDashboard_TableLayout_Navigation";
            InforVisualDashboard_TableLayout_Navigation.RowCount = 8;
            InforVisualDashboard_TableLayout_Navigation.RowStyles.Add(new RowStyle());
            InforVisualDashboard_TableLayout_Navigation.RowStyles.Add(new RowStyle());
            InforVisualDashboard_TableLayout_Navigation.RowStyles.Add(new RowStyle());
            InforVisualDashboard_TableLayout_Navigation.RowStyles.Add(new RowStyle());
            InforVisualDashboard_TableLayout_Navigation.RowStyles.Add(new RowStyle());
            InforVisualDashboard_TableLayout_Navigation.RowStyles.Add(new RowStyle());
            InforVisualDashboard_TableLayout_Navigation.RowStyles.Add(new RowStyle());
            InforVisualDashboard_TableLayout_Navigation.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            InforVisualDashboard_TableLayout_Navigation.Size = new Size(149, 760);
            InforVisualDashboard_TableLayout_Navigation.TabIndex = 0;
            // 
            // InforVisualDashboard_Button_Inventory
            // 
            InforVisualDashboard_Button_Inventory.BackColor = Color.White;
            InforVisualDashboard_Button_Inventory.Dock = DockStyle.Fill;
            InforVisualDashboard_Button_Inventory.FlatStyle = FlatStyle.Flat;
            InforVisualDashboard_Button_Inventory.Location = new Point(3, 3);
            InforVisualDashboard_Button_Inventory.Name = "InforVisualDashboard_Button_Inventory";
            InforVisualDashboard_Button_Inventory.Size = new Size(143, 45);
            InforVisualDashboard_Button_Inventory.TabIndex = 7;
            InforVisualDashboard_Button_Inventory.Tag = Models.Enum_VisualDashboardCategory.Inventory;
            InforVisualDashboard_Button_Inventory.Text = "Inventory";
            InforVisualDashboard_Button_Inventory.TextAlign = ContentAlignment.MiddleLeft;
            InforVisualDashboard_Button_Inventory.UseVisualStyleBackColor = false;
            // 
            // InforVisualDashboard_Button_Receiving
            // 
            InforVisualDashboard_Button_Receiving.BackColor = Color.White;
            InforVisualDashboard_Button_Receiving.Dock = DockStyle.Fill;
            InforVisualDashboard_Button_Receiving.FlatStyle = FlatStyle.Flat;
            InforVisualDashboard_Button_Receiving.Location = new Point(3, 54);
            InforVisualDashboard_Button_Receiving.Name = "InforVisualDashboard_Button_Receiving";
            InforVisualDashboard_Button_Receiving.Size = new Size(143, 45);
            InforVisualDashboard_Button_Receiving.TabIndex = 8;
            InforVisualDashboard_Button_Receiving.Tag = Models.Enum_VisualDashboardCategory.Receiving;
            InforVisualDashboard_Button_Receiving.Text = "Receiving";
            InforVisualDashboard_Button_Receiving.TextAlign = ContentAlignment.MiddleLeft;
            InforVisualDashboard_Button_Receiving.UseVisualStyleBackColor = false;
            // 
            // InforVisualDashboard_Button_Shipping
            // 
            InforVisualDashboard_Button_Shipping.BackColor = Color.White;
            InforVisualDashboard_Button_Shipping.Dock = DockStyle.Fill;
            InforVisualDashboard_Button_Shipping.FlatStyle = FlatStyle.Flat;
            InforVisualDashboard_Button_Shipping.Location = new Point(3, 105);
            InforVisualDashboard_Button_Shipping.Name = "InforVisualDashboard_Button_Shipping";
            InforVisualDashboard_Button_Shipping.Size = new Size(143, 45);
            InforVisualDashboard_Button_Shipping.TabIndex = 9;
            InforVisualDashboard_Button_Shipping.Tag = Models.Enum_VisualDashboardCategory.Shipping;
            InforVisualDashboard_Button_Shipping.Text = "Shipping";
            InforVisualDashboard_Button_Shipping.TextAlign = ContentAlignment.MiddleLeft;
            InforVisualDashboard_Button_Shipping.UseVisualStyleBackColor = false;
            InforVisualDashboard_Button_Shipping.Visible = true;
            // 
            // InforVisualDashboard_Button_InventoryAuditing
            // 
            InforVisualDashboard_Button_InventoryAuditing.BackColor = Color.White;
            InforVisualDashboard_Button_InventoryAuditing.Dock = DockStyle.Fill;
            InforVisualDashboard_Button_InventoryAuditing.FlatStyle = FlatStyle.Flat;
            InforVisualDashboard_Button_InventoryAuditing.Location = new Point(3, 156);
            InforVisualDashboard_Button_InventoryAuditing.Name = "InforVisualDashboard_Button_InventoryAuditing";
            InforVisualDashboard_Button_InventoryAuditing.Size = new Size(143, 45);
            InforVisualDashboard_Button_InventoryAuditing.TabIndex = 10;
            InforVisualDashboard_Button_InventoryAuditing.Tag = Models.Enum_VisualDashboardCategory.InventoryAuditing;
            InforVisualDashboard_Button_InventoryAuditing.Text = "Inventory Auditing";
            InforVisualDashboard_Button_InventoryAuditing.TextAlign = ContentAlignment.MiddleLeft;
            InforVisualDashboard_Button_InventoryAuditing.UseVisualStyleBackColor = false;
            // 
            // InforVisualDashboard_Button_DieToolDiscovery
            // 
            InforVisualDashboard_Button_DieToolDiscovery.BackColor = Color.White;
            InforVisualDashboard_Button_DieToolDiscovery.Dock = DockStyle.Fill;
            InforVisualDashboard_Button_DieToolDiscovery.FlatStyle = FlatStyle.Flat;
            InforVisualDashboard_Button_DieToolDiscovery.Location = new Point(3, 207);
            InforVisualDashboard_Button_DieToolDiscovery.Name = "InforVisualDashboard_Button_DieToolDiscovery";
            InforVisualDashboard_Button_DieToolDiscovery.Size = new Size(143, 45);
            InforVisualDashboard_Button_DieToolDiscovery.TabIndex = 11;
            InforVisualDashboard_Button_DieToolDiscovery.Tag = Models.Enum_VisualDashboardCategory.DieToolDiscovery;
            InforVisualDashboard_Button_DieToolDiscovery.Text = "Die Tool Discovery";
            InforVisualDashboard_Button_DieToolDiscovery.TextAlign = ContentAlignment.MiddleLeft;
            InforVisualDashboard_Button_DieToolDiscovery.UseVisualStyleBackColor = false;
            // 
            // InforVisualDashboard_Button_MaterialHandlerGeneral
            // 
            InforVisualDashboard_Button_MaterialHandlerGeneral.BackColor = Color.White;
            InforVisualDashboard_Button_MaterialHandlerGeneral.Dock = DockStyle.Fill;
            InforVisualDashboard_Button_MaterialHandlerGeneral.FlatStyle = FlatStyle.Flat;
            InforVisualDashboard_Button_MaterialHandlerGeneral.Location = new Point(3, 258);
            InforVisualDashboard_Button_MaterialHandlerGeneral.Name = "InforVisualDashboard_Button_MaterialHandlerGeneral";
            InforVisualDashboard_Button_MaterialHandlerGeneral.Size = new Size(143, 45);
            InforVisualDashboard_Button_MaterialHandlerGeneral.TabIndex = 12;
            InforVisualDashboard_Button_MaterialHandlerGeneral.Tag = Models.Enum_VisualDashboardCategory.MaterialHandlerAnalytics_General;
            InforVisualDashboard_Button_MaterialHandlerGeneral.Text = "MH Analytics (General)";
            InforVisualDashboard_Button_MaterialHandlerGeneral.TextAlign = ContentAlignment.MiddleLeft;
            InforVisualDashboard_Button_MaterialHandlerGeneral.UseVisualStyleBackColor = false;
            InforVisualDashboard_Button_MaterialHandlerGeneral.Visible = true;
            // 
            // InforVisualDashboard_Button_MaterialHandlerTeam
            // 
            InforVisualDashboard_Button_MaterialHandlerTeam.BackColor = Color.White;
            InforVisualDashboard_Button_MaterialHandlerTeam.Dock = DockStyle.Fill;
            InforVisualDashboard_Button_MaterialHandlerTeam.FlatStyle = FlatStyle.Flat;
            InforVisualDashboard_Button_MaterialHandlerTeam.Location = new Point(3, 309);
            InforVisualDashboard_Button_MaterialHandlerTeam.Name = "InforVisualDashboard_Button_MaterialHandlerTeam";
            InforVisualDashboard_Button_MaterialHandlerTeam.Size = new Size(143, 45);
            InforVisualDashboard_Button_MaterialHandlerTeam.TabIndex = 13;
            InforVisualDashboard_Button_MaterialHandlerTeam.Tag = Models.Enum_VisualDashboardCategory.MaterialHandlerAnalytics_Team;
            InforVisualDashboard_Button_MaterialHandlerTeam.Text = "MH Analytics (Team)";
            InforVisualDashboard_Button_MaterialHandlerTeam.TextAlign = ContentAlignment.MiddleLeft;
            InforVisualDashboard_Button_MaterialHandlerTeam.UseVisualStyleBackColor = false;
            InforVisualDashboard_Button_MaterialHandlerTeam.Visible = true;
            // 
            // InforVisualDashboard
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1024, 768);
            Controls.Add(InforVisualDashboard_TableLayout_Main);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(1024, 768);
            Name = "InforVisualDashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Infor Visual Dashboard";
            InforVisualDashboard_Panel_Content.ResumeLayout(false);
            InforVisualDashboard_Panel_Content.PerformLayout();
            InforVisualDashboard_TableLayout_Loading.ResumeLayout(false);
            InforVisualDashboard_TableLayout_Main.ResumeLayout(false);
            InforVisualDashboard_Panel_Navigation.ResumeLayout(false);
            InforVisualDashboard_Panel_Navigation.PerformLayout();
            InforVisualDashboard_TableLayout_Navigation.ResumeLayout(false);
            ResumeLayout(false);
        }
        private System.Windows.Forms.Panel InforVisualDashboard_Panel_Content;
        private Components.Shared.Control_EmptyState InforVisualDashboard_Control_EmptyState;
        private Label InforVisualDashboard_Label_Loading;
        private TableLayoutPanel InforVisualDashboard_TableLayout_Main;
        private Panel InforVisualDashboard_Panel_Navigation;
        private TableLayoutPanel InforVisualDashboard_TableLayout_Navigation;
        private Button InforVisualDashboard_Button_Inventory;
        private Button InforVisualDashboard_Button_Receiving;
        private Button InforVisualDashboard_Button_Shipping;
        private Button InforVisualDashboard_Button_InventoryAuditing;
        private Button InforVisualDashboard_Button_DieToolDiscovery;
        private Button InforVisualDashboard_Button_MaterialHandlerGeneral;
        private Button InforVisualDashboard_Button_MaterialHandlerTeam;
        private TableLayoutPanel InforVisualDashboard_TableLayout_Loading;
        private Label InforVisualDashboard_Label_ReadOnly;
    }
}
