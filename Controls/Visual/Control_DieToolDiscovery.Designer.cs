using MTM_WIP_Application_Winforms.Models.Enums;

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
            Control_DieToolDiscovery_TabControl_Main = new TabControl();
            Control_DieToolDiscovery_TabPage_Locator = new TabPage();
            Control_DieToolDiscovery_GroupBox_Search = new GroupBox();
            Control_DieToolDiscovery_TableLayoutPanel_Main = new TableLayoutPanel();
            Control_DieToolDiscovery_FlowLayoutPanel_SearchType = new FlowLayoutPanel();
            Control_DieToolDiscovery_RadioButton_SearchByPart = new RadioButton();
            Control_DieToolDiscovery_RadioButton_SearchByDie = new RadioButton();
            Control_DieToolDiscovery_TableLayoutPanel_Search = new TableLayoutPanel();
            Control_DieToolDiscovery_SuggestionBox_Search = new Components.Shared.Component_SuggestionTextBoxWithLabel();
            Control_DieToolDiscovery_Button_Search = new Button();
            Control_DieToolDiscovery_Button_WhereUsed = new Button();
            Control_DieToolDiscovery_Panel_Grid = new Panel();
            Control_DieToolDiscovery_DataGridView_Results = new DataGridView();
            Control_DieToolDiscovery_TabPage_CoilFlatstock = new TabPage();
            Control_DieToolDiscovery_TableLayoutPanel_Coil = new TableLayoutPanel();
            Control_DieToolDiscovery_SuggestionBox_CoilSearch = new Components.Shared.Component_SuggestionTextBoxWithLabel();
            Control_DieToolDiscovery_Button_CoilSearch = new Button();
            Control_DieToolDiscovery_TableLayoutPanel_CoilDetails = new TableLayoutPanel();
            Control_DieToolDiscovery_SuggestionBox_Thickness = new Components.Shared.Component_SuggestionTextBoxWithLabel();
            Control_DieToolDiscovery_SuggestionBox_Width = new Components.Shared.Component_SuggestionTextBoxWithLabel();
            Control_DieToolDiscovery_SuggestionBox_Length = new Components.Shared.Component_SuggestionTextBoxWithLabel();
            Control_DieToolDiscovery_SuggestionBox_Gauge = new Components.Shared.Component_SuggestionTextBoxWithLabel();
            Control_DieToolDiscovery_SuggestionBox_WhereUsed = new Components.Shared.Component_SuggestionTextBoxWithLabel();
            Control_DieToolDiscovery_SuggestionBox_Progression = new Components.Shared.Component_SuggestionTextBoxWithLabel();
            Control_DieToolDiscovery_SuggestionBox_Customer = new Components.Shared.Component_SuggestionTextBoxWithLabel();
            Control_DieToolDiscovery_SuggestionBox_ScrapLocation = new Components.Shared.Component_SuggestionTextBoxWithLabel();
            Control_DieToolDiscovery_SuggestionBox_GenericType = new Components.Shared.Component_SuggestionTextBoxWithLabel();
            Control_DieToolDiscovery_SuggestionBox_DetailedType = new Components.Shared.Component_SuggestionTextBoxWithLabel();
            Control_DieToolDiscovery_SuggestionBox_AutoIssueLocation = new Components.Shared.Component_SuggestionTextBoxWithLabel();
            Control_DieToolDiscovery_TabControl_Main.SuspendLayout();
            Control_DieToolDiscovery_TabPage_Locator.SuspendLayout();
            Control_DieToolDiscovery_GroupBox_Search.SuspendLayout();
            Control_DieToolDiscovery_TableLayoutPanel_Main.SuspendLayout();
            Control_DieToolDiscovery_FlowLayoutPanel_SearchType.SuspendLayout();
            Control_DieToolDiscovery_TableLayoutPanel_Search.SuspendLayout();
            Control_DieToolDiscovery_Panel_Grid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Control_DieToolDiscovery_DataGridView_Results).BeginInit();
            Control_DieToolDiscovery_TabPage_CoilFlatstock.SuspendLayout();
            Control_DieToolDiscovery_TableLayoutPanel_Coil.SuspendLayout();
            Control_DieToolDiscovery_TableLayoutPanel_CoilDetails.SuspendLayout();
            SuspendLayout();
            // 
            // Control_DieToolDiscovery_TabControl_Main
            // 
            Control_DieToolDiscovery_TabControl_Main.Controls.Add(Control_DieToolDiscovery_TabPage_Locator);
            Control_DieToolDiscovery_TabControl_Main.Controls.Add(Control_DieToolDiscovery_TabPage_CoilFlatstock);
            Control_DieToolDiscovery_TabControl_Main.Dock = DockStyle.Fill;
            Control_DieToolDiscovery_TabControl_Main.Location = new Point(0, 0);
            Control_DieToolDiscovery_TabControl_Main.Name = "Control_DieToolDiscovery_TabControl_Main";
            Control_DieToolDiscovery_TabControl_Main.SelectedIndex = 0;
            Control_DieToolDiscovery_TabControl_Main.Size = new Size(800, 350);
            Control_DieToolDiscovery_TabControl_Main.TabIndex = 0;
            // 
            // Control_DieToolDiscovery_TabPage_Locator
            // 
            Control_DieToolDiscovery_TabPage_Locator.Controls.Add(Control_DieToolDiscovery_GroupBox_Search);
            Control_DieToolDiscovery_TabPage_Locator.Location = new Point(4, 24);
            Control_DieToolDiscovery_TabPage_Locator.Name = "Control_DieToolDiscovery_TabPage_Locator";
            Control_DieToolDiscovery_TabPage_Locator.Padding = new Padding(10);
            Control_DieToolDiscovery_TabPage_Locator.Size = new Size(792, 322);
            Control_DieToolDiscovery_TabPage_Locator.TabIndex = 0;
            Control_DieToolDiscovery_TabPage_Locator.Text = "Die Locator";
            Control_DieToolDiscovery_TabPage_Locator.UseVisualStyleBackColor = true;
            // 
            // Control_DieToolDiscovery_GroupBox_Search
            // 
            Control_DieToolDiscovery_GroupBox_Search.AutoSize = true;
            Control_DieToolDiscovery_GroupBox_Search.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_DieToolDiscovery_GroupBox_Search.Controls.Add(Control_DieToolDiscovery_TableLayoutPanel_Main);
            Control_DieToolDiscovery_GroupBox_Search.Dock = DockStyle.Fill;
            Control_DieToolDiscovery_GroupBox_Search.Location = new Point(10, 10);
            Control_DieToolDiscovery_GroupBox_Search.Name = "Control_DieToolDiscovery_GroupBox_Search";
            Control_DieToolDiscovery_GroupBox_Search.Size = new Size(772, 302);
            Control_DieToolDiscovery_GroupBox_Search.TabIndex = 0;
            Control_DieToolDiscovery_GroupBox_Search.TabStop = false;
            Control_DieToolDiscovery_GroupBox_Search.Text = "Search Criteria";
            // 
            // Control_DieToolDiscovery_TableLayoutPanel_Main
            // 
            Control_DieToolDiscovery_TableLayoutPanel_Main.AutoSize = true;
            Control_DieToolDiscovery_TableLayoutPanel_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_DieToolDiscovery_TableLayoutPanel_Main.ColumnCount = 2;
            Control_DieToolDiscovery_TableLayoutPanel_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            Control_DieToolDiscovery_TableLayoutPanel_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            Control_DieToolDiscovery_TableLayoutPanel_Main.Controls.Add(Control_DieToolDiscovery_FlowLayoutPanel_SearchType, 0, 0);
            Control_DieToolDiscovery_TableLayoutPanel_Main.SetColumnSpan(Control_DieToolDiscovery_FlowLayoutPanel_SearchType, 2);
            Control_DieToolDiscovery_TableLayoutPanel_Main.Controls.Add(Control_DieToolDiscovery_TableLayoutPanel_Search, 0, 1);
            Control_DieToolDiscovery_TableLayoutPanel_Main.SetColumnSpan(Control_DieToolDiscovery_TableLayoutPanel_Search, 2);
            Control_DieToolDiscovery_TableLayoutPanel_Main.Controls.Add(Control_DieToolDiscovery_Button_Search, 0, 2);
            Control_DieToolDiscovery_TableLayoutPanel_Main.Controls.Add(Control_DieToolDiscovery_Button_WhereUsed, 1, 2);
            Control_DieToolDiscovery_TableLayoutPanel_Main.Controls.Add(Control_DieToolDiscovery_Panel_Grid, 0, 3);
            Control_DieToolDiscovery_TableLayoutPanel_Main.SetColumnSpan(Control_DieToolDiscovery_Panel_Grid, 2);
            Control_DieToolDiscovery_TableLayoutPanel_Main.Dock = DockStyle.Fill;
            Control_DieToolDiscovery_TableLayoutPanel_Main.Location = new Point(3, 19);
            Control_DieToolDiscovery_TableLayoutPanel_Main.Name = "Control_DieToolDiscovery_TableLayoutPanel_Main";
            Control_DieToolDiscovery_TableLayoutPanel_Main.RowCount = 4;
            Control_DieToolDiscovery_TableLayoutPanel_Main.RowStyles.Add(new RowStyle());
            Control_DieToolDiscovery_TableLayoutPanel_Main.RowStyles.Add(new RowStyle());
            Control_DieToolDiscovery_TableLayoutPanel_Main.RowStyles.Add(new RowStyle());
            Control_DieToolDiscovery_TableLayoutPanel_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_DieToolDiscovery_TableLayoutPanel_Main.Size = new Size(766, 280);
            Control_DieToolDiscovery_TableLayoutPanel_Main.TabIndex = 5;
            // 
            // Control_DieToolDiscovery_FlowLayoutPanel_SearchType
            // 
            Control_DieToolDiscovery_FlowLayoutPanel_SearchType.AutoSize = true;
            Control_DieToolDiscovery_FlowLayoutPanel_SearchType.Controls.Add(Control_DieToolDiscovery_RadioButton_SearchByPart);
            Control_DieToolDiscovery_FlowLayoutPanel_SearchType.Controls.Add(Control_DieToolDiscovery_RadioButton_SearchByDie);
            Control_DieToolDiscovery_FlowLayoutPanel_SearchType.Dock = DockStyle.Fill;
            Control_DieToolDiscovery_FlowLayoutPanel_SearchType.Location = new Point(3, 3);
            Control_DieToolDiscovery_FlowLayoutPanel_SearchType.Name = "Control_DieToolDiscovery_FlowLayoutPanel_SearchType";
            Control_DieToolDiscovery_FlowLayoutPanel_SearchType.Size = new Size(760, 25);
            Control_DieToolDiscovery_FlowLayoutPanel_SearchType.TabIndex = 0;
            // 
            // Control_DieToolDiscovery_RadioButton_SearchByPart
            // 
            Control_DieToolDiscovery_RadioButton_SearchByPart.AutoSize = true;
            Control_DieToolDiscovery_RadioButton_SearchByPart.Checked = true;
            Control_DieToolDiscovery_RadioButton_SearchByPart.Location = new Point(3, 3);
            Control_DieToolDiscovery_RadioButton_SearchByPart.Name = "Control_DieToolDiscovery_RadioButton_SearchByPart";
            Control_DieToolDiscovery_RadioButton_SearchByPart.Size = new Size(143, 19);
            Control_DieToolDiscovery_RadioButton_SearchByPart.TabIndex = 0;
            Control_DieToolDiscovery_RadioButton_SearchByPart.TabStop = true;
            Control_DieToolDiscovery_RadioButton_SearchByPart.Text = "Search by Part Number";
            Control_DieToolDiscovery_RadioButton_SearchByPart.UseVisualStyleBackColor = true;
            // 
            // Control_DieToolDiscovery_RadioButton_SearchByDie
            // 
            Control_DieToolDiscovery_RadioButton_SearchByDie.AutoSize = true;
            Control_DieToolDiscovery_RadioButton_SearchByDie.Location = new Point(152, 3);
            Control_DieToolDiscovery_RadioButton_SearchByDie.Name = "Control_DieToolDiscovery_RadioButton_SearchByDie";
            Control_DieToolDiscovery_RadioButton_SearchByDie.Size = new Size(139, 19);
            Control_DieToolDiscovery_RadioButton_SearchByDie.TabIndex = 1;
            Control_DieToolDiscovery_RadioButton_SearchByDie.Text = "Search by Die Number";
            Control_DieToolDiscovery_RadioButton_SearchByDie.UseVisualStyleBackColor = true;
            // 
            // Control_DieToolDiscovery_TableLayoutPanel_Search
            // 
            Control_DieToolDiscovery_TableLayoutPanel_Search.AutoSize = true;
            Control_DieToolDiscovery_TableLayoutPanel_Search.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_DieToolDiscovery_TableLayoutPanel_Search.ColumnCount = 1;
            Control_DieToolDiscovery_TableLayoutPanel_Search.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_DieToolDiscovery_TableLayoutPanel_Search.Controls.Add(Control_DieToolDiscovery_SuggestionBox_Search, 0, 0);
            Control_DieToolDiscovery_TableLayoutPanel_Search.Dock = DockStyle.Fill;
            Control_DieToolDiscovery_TableLayoutPanel_Search.Location = new Point(3, 34);
            Control_DieToolDiscovery_TableLayoutPanel_Search.Name = "Control_DieToolDiscovery_TableLayoutPanel_Search";
            Control_DieToolDiscovery_TableLayoutPanel_Search.RowCount = 1;
            Control_DieToolDiscovery_TableLayoutPanel_Search.RowStyles.Add(new RowStyle());
            Control_DieToolDiscovery_TableLayoutPanel_Search.Size = new Size(760, 29);
            Control_DieToolDiscovery_TableLayoutPanel_Search.TabIndex = 6;
            // 
            // Control_DieToolDiscovery_SuggestionBox_Search
            // 
            Control_DieToolDiscovery_SuggestionBox_Search.AutoSize = true;
            Control_DieToolDiscovery_SuggestionBox_Search.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_DieToolDiscovery_SuggestionBox_Search.Dock = DockStyle.Fill;
            Control_DieToolDiscovery_SuggestionBox_Search.EnableSuggestions = true;
            Control_DieToolDiscovery_SuggestionBox_Search.LabelText = "Search Term";
            Control_DieToolDiscovery_SuggestionBox_Search.LabelVisibility = Enum_LabelVisibility.Hidden;
            Control_DieToolDiscovery_SuggestionBox_Search.Location = new Point(3, 3);
            Control_DieToolDiscovery_SuggestionBox_Search.Margin = new Padding(3, 3, 3, 3);
            Control_DieToolDiscovery_SuggestionBox_Search.Name = "Control_DieToolDiscovery_SuggestionBox_Search";
            Control_DieToolDiscovery_SuggestionBox_Search.PlaceholderText = "Enter Part Number or Die Number";
            Control_DieToolDiscovery_SuggestionBox_Search.ShowF4Button = true;
            Control_DieToolDiscovery_SuggestionBox_Search.Size = new Size(754, 23);
            Control_DieToolDiscovery_SuggestionBox_Search.TabIndex = 3;
            // 
            // Control_DieToolDiscovery_Button_Search
            // 
            Control_DieToolDiscovery_Button_Search.AutoSize = true;
            Control_DieToolDiscovery_Button_Search.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_DieToolDiscovery_Button_Search.Dock = DockStyle.Fill;
            Control_DieToolDiscovery_Button_Search.Location = new Point(3, 69);
            Control_DieToolDiscovery_Button_Search.Name = "Control_DieToolDiscovery_Button_Search";
            Control_DieToolDiscovery_Button_Search.Size = new Size(377, 25);
            Control_DieToolDiscovery_Button_Search.TabIndex = 4;
            Control_DieToolDiscovery_Button_Search.Text = "Search";
            Control_DieToolDiscovery_Button_Search.UseVisualStyleBackColor = true;
            // 
            // Control_DieToolDiscovery_Button_WhereUsed
            // 
            Control_DieToolDiscovery_Button_WhereUsed.AutoSize = true;
            Control_DieToolDiscovery_Button_WhereUsed.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_DieToolDiscovery_Button_WhereUsed.Dock = DockStyle.Fill;
            Control_DieToolDiscovery_Button_WhereUsed.Location = new Point(386, 69);
            Control_DieToolDiscovery_Button_WhereUsed.Name = "Control_DieToolDiscovery_Button_WhereUsed";
            Control_DieToolDiscovery_Button_WhereUsed.Size = new Size(377, 25);
            Control_DieToolDiscovery_Button_WhereUsed.TabIndex = 8;
            Control_DieToolDiscovery_Button_WhereUsed.Text = "Where Used";
            Control_DieToolDiscovery_Button_WhereUsed.UseVisualStyleBackColor = true;
            // 
            // Control_DieToolDiscovery_Panel_Grid
            // 
            Control_DieToolDiscovery_Panel_Grid.AutoSize = true;
            Control_DieToolDiscovery_Panel_Grid.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_DieToolDiscovery_Panel_Grid.BorderStyle = BorderStyle.FixedSingle;
            Control_DieToolDiscovery_Panel_Grid.Controls.Add(Control_DieToolDiscovery_DataGridView_Results);
            Control_DieToolDiscovery_Panel_Grid.Dock = DockStyle.Fill;
            Control_DieToolDiscovery_Panel_Grid.Location = new Point(3, 100);
            Control_DieToolDiscovery_Panel_Grid.Name = "Control_DieToolDiscovery_Panel_Grid";
            Control_DieToolDiscovery_Panel_Grid.Padding = new Padding(10);
            Control_DieToolDiscovery_Panel_Grid.Size = new Size(760, 177);
            Control_DieToolDiscovery_Panel_Grid.TabIndex = 7;
            // 
            // Control_DieToolDiscovery_DataGridView_Results
            // 
            Control_DieToolDiscovery_DataGridView_Results.AllowUserToAddRows = false;
            Control_DieToolDiscovery_DataGridView_Results.AllowUserToDeleteRows = false;
            Control_DieToolDiscovery_DataGridView_Results.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Control_DieToolDiscovery_DataGridView_Results.Dock = DockStyle.Fill;
            Control_DieToolDiscovery_DataGridView_Results.Location = new Point(10, 10);
            Control_DieToolDiscovery_DataGridView_Results.Name = "Control_DieToolDiscovery_DataGridView_Results";
            Control_DieToolDiscovery_DataGridView_Results.ReadOnly = true;
            Control_DieToolDiscovery_DataGridView_Results.Size = new Size(738, 186);
            Control_DieToolDiscovery_DataGridView_Results.TabIndex = 2;
            // 
            // Control_DieToolDiscovery_TabPage_CoilFlatstock
            // 
            Control_DieToolDiscovery_TabPage_CoilFlatstock.Controls.Add(Control_DieToolDiscovery_TableLayoutPanel_Coil);
            Control_DieToolDiscovery_TabPage_CoilFlatstock.Location = new Point(4, 24);
            Control_DieToolDiscovery_TabPage_CoilFlatstock.Name = "Control_DieToolDiscovery_TabPage_CoilFlatstock";
            Control_DieToolDiscovery_TabPage_CoilFlatstock.Padding = new Padding(10);
            Control_DieToolDiscovery_TabPage_CoilFlatstock.Size = new Size(792, 322);
            Control_DieToolDiscovery_TabPage_CoilFlatstock.TabIndex = 1;
            Control_DieToolDiscovery_TabPage_CoilFlatstock.Text = "Coil/Flatstock Search";
            Control_DieToolDiscovery_TabPage_CoilFlatstock.UseVisualStyleBackColor = true;
            // 
            // Control_DieToolDiscovery_TableLayoutPanel_Coil
            // 
            Control_DieToolDiscovery_TableLayoutPanel_Coil.ColumnCount = 1;
            Control_DieToolDiscovery_TableLayoutPanel_Coil.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_DieToolDiscovery_TableLayoutPanel_Coil.Controls.Add(Control_DieToolDiscovery_SuggestionBox_CoilSearch, 0, 0);
            Control_DieToolDiscovery_TableLayoutPanel_Coil.Controls.Add(Control_DieToolDiscovery_Button_CoilSearch, 0, 1);
            Control_DieToolDiscovery_TableLayoutPanel_Coil.Controls.Add(Control_DieToolDiscovery_TableLayoutPanel_CoilDetails, 0, 2);
            Control_DieToolDiscovery_TableLayoutPanel_Coil.Dock = DockStyle.Fill;
            Control_DieToolDiscovery_TableLayoutPanel_Coil.Location = new Point(10, 10);
            Control_DieToolDiscovery_TableLayoutPanel_Coil.Name = "Control_DieToolDiscovery_TableLayoutPanel_Coil";
            Control_DieToolDiscovery_TableLayoutPanel_Coil.RowCount = 3;
            Control_DieToolDiscovery_TableLayoutPanel_Coil.RowStyles.Add(new RowStyle());
            Control_DieToolDiscovery_TableLayoutPanel_Coil.RowStyles.Add(new RowStyle());
            Control_DieToolDiscovery_TableLayoutPanel_Coil.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_DieToolDiscovery_TableLayoutPanel_Coil.Size = new Size(772, 302);
            Control_DieToolDiscovery_TableLayoutPanel_Coil.TabIndex = 0;
            // 
            // Control_DieToolDiscovery_SuggestionBox_CoilSearch
            // 
            Control_DieToolDiscovery_SuggestionBox_CoilSearch.AutoSize = true;
            Control_DieToolDiscovery_SuggestionBox_CoilSearch.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_DieToolDiscovery_SuggestionBox_CoilSearch.Dock = DockStyle.Fill;
            Control_DieToolDiscovery_SuggestionBox_CoilSearch.LabelVisibility = Enum_LabelVisibility.Hidden;
            Control_DieToolDiscovery_SuggestionBox_CoilSearch.Location = new Point(6, 6);
            Control_DieToolDiscovery_SuggestionBox_CoilSearch.Margin = new Padding(6);
            Control_DieToolDiscovery_SuggestionBox_CoilSearch.MaxLength = 130;
            Control_DieToolDiscovery_SuggestionBox_CoilSearch.MinimumSize = new Size(0, 23);
            Control_DieToolDiscovery_SuggestionBox_CoilSearch.MinLength = 130;
            Control_DieToolDiscovery_SuggestionBox_CoilSearch.Name = "Control_DieToolDiscovery_SuggestionBox_CoilSearch";
            Control_DieToolDiscovery_SuggestionBox_CoilSearch.PlaceholderText = "Coil / Flatstock Number";
            Control_DieToolDiscovery_SuggestionBox_CoilSearch.Size = new Size(760, 23);
            Control_DieToolDiscovery_SuggestionBox_CoilSearch.TabIndex = 3;
            // 
            // Control_DieToolDiscovery_Button_CoilSearch
            // 
            Control_DieToolDiscovery_Button_CoilSearch.Dock = DockStyle.Fill;
            Control_DieToolDiscovery_Button_CoilSearch.Location = new Point(6, 41);
            Control_DieToolDiscovery_Button_CoilSearch.Margin = new Padding(6);
            Control_DieToolDiscovery_Button_CoilSearch.Name = "Control_DieToolDiscovery_Button_CoilSearch";
            Control_DieToolDiscovery_Button_CoilSearch.Size = new Size(760, 30);
            Control_DieToolDiscovery_Button_CoilSearch.TabIndex = 4;
            Control_DieToolDiscovery_Button_CoilSearch.Text = "Search";
            Control_DieToolDiscovery_Button_CoilSearch.UseVisualStyleBackColor = true;
            // 
            // Control_DieToolDiscovery_TableLayoutPanel_CoilDetails
            // 
            Control_DieToolDiscovery_TableLayoutPanel_CoilDetails.ColumnCount = 2;
            Control_DieToolDiscovery_TableLayoutPanel_CoilDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            Control_DieToolDiscovery_TableLayoutPanel_CoilDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            Control_DieToolDiscovery_TableLayoutPanel_CoilDetails.Controls.Add(Control_DieToolDiscovery_SuggestionBox_Thickness, 0, 0);
            Control_DieToolDiscovery_TableLayoutPanel_CoilDetails.Controls.Add(Control_DieToolDiscovery_SuggestionBox_Width, 1, 0);
            Control_DieToolDiscovery_TableLayoutPanel_CoilDetails.Controls.Add(Control_DieToolDiscovery_SuggestionBox_Length, 0, 1);
            Control_DieToolDiscovery_TableLayoutPanel_CoilDetails.Controls.Add(Control_DieToolDiscovery_SuggestionBox_Gauge, 1, 1);
            Control_DieToolDiscovery_TableLayoutPanel_CoilDetails.Controls.Add(Control_DieToolDiscovery_SuggestionBox_WhereUsed, 0, 2);
            Control_DieToolDiscovery_TableLayoutPanel_CoilDetails.Controls.Add(Control_DieToolDiscovery_SuggestionBox_Progression, 1, 2);
            Control_DieToolDiscovery_TableLayoutPanel_CoilDetails.Controls.Add(Control_DieToolDiscovery_SuggestionBox_Customer, 0, 3);
            Control_DieToolDiscovery_TableLayoutPanel_CoilDetails.Controls.Add(Control_DieToolDiscovery_SuggestionBox_ScrapLocation, 1, 3);
            Control_DieToolDiscovery_TableLayoutPanel_CoilDetails.Controls.Add(Control_DieToolDiscovery_SuggestionBox_GenericType, 0, 4);
            Control_DieToolDiscovery_TableLayoutPanel_CoilDetails.Controls.Add(Control_DieToolDiscovery_SuggestionBox_DetailedType, 1, 4);
            Control_DieToolDiscovery_TableLayoutPanel_CoilDetails.Controls.Add(Control_DieToolDiscovery_SuggestionBox_AutoIssueLocation, 0, 5);
            Control_DieToolDiscovery_TableLayoutPanel_CoilDetails.Dock = DockStyle.Fill;
            Control_DieToolDiscovery_TableLayoutPanel_CoilDetails.Location = new Point(3, 80);
            Control_DieToolDiscovery_TableLayoutPanel_CoilDetails.Name = "Control_DieToolDiscovery_TableLayoutPanel_CoilDetails";
            Control_DieToolDiscovery_TableLayoutPanel_CoilDetails.RowCount = 7;
            Control_DieToolDiscovery_TableLayoutPanel_CoilDetails.RowStyles.Add(new RowStyle());
            Control_DieToolDiscovery_TableLayoutPanel_CoilDetails.RowStyles.Add(new RowStyle());
            Control_DieToolDiscovery_TableLayoutPanel_CoilDetails.RowStyles.Add(new RowStyle());
            Control_DieToolDiscovery_TableLayoutPanel_CoilDetails.RowStyles.Add(new RowStyle());
            Control_DieToolDiscovery_TableLayoutPanel_CoilDetails.RowStyles.Add(new RowStyle());
            Control_DieToolDiscovery_TableLayoutPanel_CoilDetails.RowStyles.Add(new RowStyle());
            Control_DieToolDiscovery_TableLayoutPanel_CoilDetails.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_DieToolDiscovery_TableLayoutPanel_CoilDetails.Size = new Size(766, 219);
            Control_DieToolDiscovery_TableLayoutPanel_CoilDetails.TabIndex = 5;
            // 
            // Control_DieToolDiscovery_SuggestionBox_Thickness
            // 
            Control_DieToolDiscovery_SuggestionBox_Thickness.AutoSize = true;
            Control_DieToolDiscovery_SuggestionBox_Thickness.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_DieToolDiscovery_SuggestionBox_Thickness.Dock = DockStyle.Fill;
            Control_DieToolDiscovery_SuggestionBox_Thickness.Enabled = false;
            Control_DieToolDiscovery_SuggestionBox_Thickness.EnableSuggestions = false;
            Control_DieToolDiscovery_SuggestionBox_Thickness.LabelText = "Thickness";
            Control_DieToolDiscovery_SuggestionBox_Thickness.LabelVisibility = Enum_LabelVisibility.Visible;
            Control_DieToolDiscovery_SuggestionBox_Thickness.Location = new Point(6, 6);
            Control_DieToolDiscovery_SuggestionBox_Thickness.Margin = new Padding(6);
            Control_DieToolDiscovery_SuggestionBox_Thickness.MaxLength = 130;
            Control_DieToolDiscovery_SuggestionBox_Thickness.MinimumSize = new Size(0, 23);
            Control_DieToolDiscovery_SuggestionBox_Thickness.MinLength = 130;
            Control_DieToolDiscovery_SuggestionBox_Thickness.Name = "Control_DieToolDiscovery_SuggestionBox_Thickness";
            Control_DieToolDiscovery_SuggestionBox_Thickness.ShowF4Button = false;
            Control_DieToolDiscovery_SuggestionBox_Thickness.Size = new Size(371, 23);
            Control_DieToolDiscovery_SuggestionBox_Thickness.TabIndex = 10;
            // 
            // Control_DieToolDiscovery_SuggestionBox_Width
            // 
            Control_DieToolDiscovery_SuggestionBox_Width.AutoSize = true;
            Control_DieToolDiscovery_SuggestionBox_Width.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_DieToolDiscovery_SuggestionBox_Width.Dock = DockStyle.Fill;
            Control_DieToolDiscovery_SuggestionBox_Width.Enabled = false;
            Control_DieToolDiscovery_SuggestionBox_Width.EnableSuggestions = false;
            Control_DieToolDiscovery_SuggestionBox_Width.LabelText = "Width";
            Control_DieToolDiscovery_SuggestionBox_Width.LabelVisibility = Enum_LabelVisibility.Visible;
            Control_DieToolDiscovery_SuggestionBox_Width.Location = new Point(389, 6);
            Control_DieToolDiscovery_SuggestionBox_Width.Margin = new Padding(6);
            Control_DieToolDiscovery_SuggestionBox_Width.MaxLength = 130;
            Control_DieToolDiscovery_SuggestionBox_Width.MinimumSize = new Size(0, 23);
            Control_DieToolDiscovery_SuggestionBox_Width.MinLength = 130;
            Control_DieToolDiscovery_SuggestionBox_Width.Name = "Control_DieToolDiscovery_SuggestionBox_Width";
            Control_DieToolDiscovery_SuggestionBox_Width.ShowF4Button = false;
            Control_DieToolDiscovery_SuggestionBox_Width.Size = new Size(371, 23);
            Control_DieToolDiscovery_SuggestionBox_Width.TabIndex = 11;
            // 
            // Control_DieToolDiscovery_SuggestionBox_Length
            // 
            Control_DieToolDiscovery_SuggestionBox_Length.AutoSize = true;
            Control_DieToolDiscovery_SuggestionBox_Length.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_DieToolDiscovery_SuggestionBox_Length.Dock = DockStyle.Fill;
            Control_DieToolDiscovery_SuggestionBox_Length.Enabled = false;
            Control_DieToolDiscovery_SuggestionBox_Length.EnableSuggestions = false;
            Control_DieToolDiscovery_SuggestionBox_Length.LabelText = "Length";
            Control_DieToolDiscovery_SuggestionBox_Length.LabelVisibility = Enum_LabelVisibility.Visible;
            Control_DieToolDiscovery_SuggestionBox_Length.Location = new Point(6, 41);
            Control_DieToolDiscovery_SuggestionBox_Length.Margin = new Padding(6);
            Control_DieToolDiscovery_SuggestionBox_Length.MaxLength = 130;
            Control_DieToolDiscovery_SuggestionBox_Length.MinimumSize = new Size(0, 23);
            Control_DieToolDiscovery_SuggestionBox_Length.MinLength = 130;
            Control_DieToolDiscovery_SuggestionBox_Length.Name = "Control_DieToolDiscovery_SuggestionBox_Length";
            Control_DieToolDiscovery_SuggestionBox_Length.ShowF4Button = false;
            Control_DieToolDiscovery_SuggestionBox_Length.Size = new Size(371, 23);
            Control_DieToolDiscovery_SuggestionBox_Length.TabIndex = 12;
            // 
            // Control_DieToolDiscovery_SuggestionBox_Gauge
            // 
            Control_DieToolDiscovery_SuggestionBox_Gauge.AutoSize = true;
            Control_DieToolDiscovery_SuggestionBox_Gauge.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_DieToolDiscovery_SuggestionBox_Gauge.Dock = DockStyle.Fill;
            Control_DieToolDiscovery_SuggestionBox_Gauge.Enabled = false;
            Control_DieToolDiscovery_SuggestionBox_Gauge.EnableSuggestions = false;
            Control_DieToolDiscovery_SuggestionBox_Gauge.LabelText = "Ga.";
            Control_DieToolDiscovery_SuggestionBox_Gauge.LabelVisibility = Enum_LabelVisibility.Visible;
            Control_DieToolDiscovery_SuggestionBox_Gauge.Location = new Point(389, 41);
            Control_DieToolDiscovery_SuggestionBox_Gauge.Margin = new Padding(6);
            Control_DieToolDiscovery_SuggestionBox_Gauge.MaxLength = 130;
            Control_DieToolDiscovery_SuggestionBox_Gauge.MinimumSize = new Size(0, 23);
            Control_DieToolDiscovery_SuggestionBox_Gauge.MinLength = 130;
            Control_DieToolDiscovery_SuggestionBox_Gauge.Name = "Control_DieToolDiscovery_SuggestionBox_Gauge";
            Control_DieToolDiscovery_SuggestionBox_Gauge.ShowF4Button = false;
            Control_DieToolDiscovery_SuggestionBox_Gauge.Size = new Size(371, 23);
            Control_DieToolDiscovery_SuggestionBox_Gauge.TabIndex = 13;
            // 
            // Control_DieToolDiscovery_SuggestionBox_WhereUsed
            // 
            Control_DieToolDiscovery_SuggestionBox_WhereUsed.AutoSize = true;
            Control_DieToolDiscovery_SuggestionBox_WhereUsed.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_DieToolDiscovery_SuggestionBox_WhereUsed.Dock = DockStyle.Fill;
            Control_DieToolDiscovery_SuggestionBox_WhereUsed.Enabled = false;
            Control_DieToolDiscovery_SuggestionBox_WhereUsed.EnableSuggestions = false;
            Control_DieToolDiscovery_SuggestionBox_WhereUsed.LabelText = "Where Used";
            Control_DieToolDiscovery_SuggestionBox_WhereUsed.LabelVisibility = Enum_LabelVisibility.Visible;
            Control_DieToolDiscovery_SuggestionBox_WhereUsed.Location = new Point(6, 76);
            Control_DieToolDiscovery_SuggestionBox_WhereUsed.Margin = new Padding(6);
            Control_DieToolDiscovery_SuggestionBox_WhereUsed.MaxLength = 130;
            Control_DieToolDiscovery_SuggestionBox_WhereUsed.MinimumSize = new Size(0, 23);
            Control_DieToolDiscovery_SuggestionBox_WhereUsed.MinLength = 130;
            Control_DieToolDiscovery_SuggestionBox_WhereUsed.Name = "Control_DieToolDiscovery_SuggestionBox_WhereUsed";
            Control_DieToolDiscovery_SuggestionBox_WhereUsed.ShowF4Button = false;
            Control_DieToolDiscovery_SuggestionBox_WhereUsed.Size = new Size(371, 23);
            Control_DieToolDiscovery_SuggestionBox_WhereUsed.TabIndex = 14;
            // 
            // Control_DieToolDiscovery_SuggestionBox_Progression
            // 
            Control_DieToolDiscovery_SuggestionBox_Progression.AutoSize = true;
            Control_DieToolDiscovery_SuggestionBox_Progression.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_DieToolDiscovery_SuggestionBox_Progression.Dock = DockStyle.Fill;
            Control_DieToolDiscovery_SuggestionBox_Progression.Enabled = false;
            Control_DieToolDiscovery_SuggestionBox_Progression.EnableSuggestions = false;
            Control_DieToolDiscovery_SuggestionBox_Progression.LabelText = "Progression";
            Control_DieToolDiscovery_SuggestionBox_Progression.LabelVisibility = Enum_LabelVisibility.Visible;
            Control_DieToolDiscovery_SuggestionBox_Progression.Location = new Point(389, 76);
            Control_DieToolDiscovery_SuggestionBox_Progression.Margin = new Padding(6);
            Control_DieToolDiscovery_SuggestionBox_Progression.MaxLength = 130;
            Control_DieToolDiscovery_SuggestionBox_Progression.MinimumSize = new Size(0, 23);
            Control_DieToolDiscovery_SuggestionBox_Progression.MinLength = 130;
            Control_DieToolDiscovery_SuggestionBox_Progression.Name = "Control_DieToolDiscovery_SuggestionBox_Progression";
            Control_DieToolDiscovery_SuggestionBox_Progression.ShowF4Button = false;
            Control_DieToolDiscovery_SuggestionBox_Progression.Size = new Size(371, 23);
            Control_DieToolDiscovery_SuggestionBox_Progression.TabIndex = 15;
            // 
            // Control_DieToolDiscovery_SuggestionBox_Customer
            // 
            Control_DieToolDiscovery_SuggestionBox_Customer.AutoSize = true;
            Control_DieToolDiscovery_SuggestionBox_Customer.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_DieToolDiscovery_SuggestionBox_Customer.Dock = DockStyle.Fill;
            Control_DieToolDiscovery_SuggestionBox_Customer.Enabled = false;
            Control_DieToolDiscovery_SuggestionBox_Customer.EnableSuggestions = false;
            Control_DieToolDiscovery_SuggestionBox_Customer.LabelText = "Customer";
            Control_DieToolDiscovery_SuggestionBox_Customer.LabelVisibility = Enum_LabelVisibility.Visible;
            Control_DieToolDiscovery_SuggestionBox_Customer.Location = new Point(6, 111);
            Control_DieToolDiscovery_SuggestionBox_Customer.Margin = new Padding(6);
            Control_DieToolDiscovery_SuggestionBox_Customer.MaxLength = 130;
            Control_DieToolDiscovery_SuggestionBox_Customer.MinimumSize = new Size(0, 23);
            Control_DieToolDiscovery_SuggestionBox_Customer.MinLength = 130;
            Control_DieToolDiscovery_SuggestionBox_Customer.Name = "Control_DieToolDiscovery_SuggestionBox_Customer";
            Control_DieToolDiscovery_SuggestionBox_Customer.ShowF4Button = false;
            Control_DieToolDiscovery_SuggestionBox_Customer.Size = new Size(371, 23);
            Control_DieToolDiscovery_SuggestionBox_Customer.TabIndex = 16;
            // 
            // Control_DieToolDiscovery_SuggestionBox_ScrapLocation
            // 
            Control_DieToolDiscovery_SuggestionBox_ScrapLocation.AutoSize = true;
            Control_DieToolDiscovery_SuggestionBox_ScrapLocation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_DieToolDiscovery_SuggestionBox_ScrapLocation.Dock = DockStyle.Fill;
            Control_DieToolDiscovery_SuggestionBox_ScrapLocation.Enabled = false;
            Control_DieToolDiscovery_SuggestionBox_ScrapLocation.EnableSuggestions = false;
            Control_DieToolDiscovery_SuggestionBox_ScrapLocation.LabelText = "Scrap Location";
            Control_DieToolDiscovery_SuggestionBox_ScrapLocation.LabelVisibility = Enum_LabelVisibility.Visible;
            Control_DieToolDiscovery_SuggestionBox_ScrapLocation.Location = new Point(389, 111);
            Control_DieToolDiscovery_SuggestionBox_ScrapLocation.Margin = new Padding(6);
            Control_DieToolDiscovery_SuggestionBox_ScrapLocation.MaxLength = 130;
            Control_DieToolDiscovery_SuggestionBox_ScrapLocation.MinimumSize = new Size(0, 23);
            Control_DieToolDiscovery_SuggestionBox_ScrapLocation.MinLength = 130;
            Control_DieToolDiscovery_SuggestionBox_ScrapLocation.Name = "Control_DieToolDiscovery_SuggestionBox_ScrapLocation";
            Control_DieToolDiscovery_SuggestionBox_ScrapLocation.ShowF4Button = false;
            Control_DieToolDiscovery_SuggestionBox_ScrapLocation.Size = new Size(371, 23);
            Control_DieToolDiscovery_SuggestionBox_ScrapLocation.TabIndex = 17;
            // 
            // Control_DieToolDiscovery_SuggestionBox_GenericType
            // 
            Control_DieToolDiscovery_SuggestionBox_GenericType.AutoSize = true;
            Control_DieToolDiscovery_SuggestionBox_GenericType.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_DieToolDiscovery_SuggestionBox_GenericType.Dock = DockStyle.Fill;
            Control_DieToolDiscovery_SuggestionBox_GenericType.Enabled = false;
            Control_DieToolDiscovery_SuggestionBox_GenericType.EnableSuggestions = false;
            Control_DieToolDiscovery_SuggestionBox_GenericType.LabelText = "Generic Type";
            Control_DieToolDiscovery_SuggestionBox_GenericType.LabelVisibility = Enum_LabelVisibility.Visible;
            Control_DieToolDiscovery_SuggestionBox_GenericType.Location = new Point(6, 146);
            Control_DieToolDiscovery_SuggestionBox_GenericType.Margin = new Padding(6);
            Control_DieToolDiscovery_SuggestionBox_GenericType.MaxLength = 130;
            Control_DieToolDiscovery_SuggestionBox_GenericType.MinimumSize = new Size(0, 23);
            Control_DieToolDiscovery_SuggestionBox_GenericType.MinLength = 130;
            Control_DieToolDiscovery_SuggestionBox_GenericType.Name = "Control_DieToolDiscovery_SuggestionBox_GenericType";
            Control_DieToolDiscovery_SuggestionBox_GenericType.ShowF4Button = false;
            Control_DieToolDiscovery_SuggestionBox_GenericType.Size = new Size(371, 23);
            Control_DieToolDiscovery_SuggestionBox_GenericType.TabIndex = 18;
            // 
            // Control_DieToolDiscovery_SuggestionBox_DetailedType
            // 
            Control_DieToolDiscovery_SuggestionBox_DetailedType.AutoSize = true;
            Control_DieToolDiscovery_SuggestionBox_DetailedType.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_DieToolDiscovery_SuggestionBox_DetailedType.Dock = DockStyle.Fill;
            Control_DieToolDiscovery_SuggestionBox_DetailedType.Enabled = false;
            Control_DieToolDiscovery_SuggestionBox_DetailedType.EnableSuggestions = false;
            Control_DieToolDiscovery_SuggestionBox_DetailedType.LabelText = "Detailed Type";
            Control_DieToolDiscovery_SuggestionBox_DetailedType.LabelVisibility = Enum_LabelVisibility.Visible;
            Control_DieToolDiscovery_SuggestionBox_DetailedType.Location = new Point(389, 146);
            Control_DieToolDiscovery_SuggestionBox_DetailedType.Margin = new Padding(6);
            Control_DieToolDiscovery_SuggestionBox_DetailedType.MaxLength = 130;
            Control_DieToolDiscovery_SuggestionBox_DetailedType.MinimumSize = new Size(0, 23);
            Control_DieToolDiscovery_SuggestionBox_DetailedType.MinLength = 130;
            Control_DieToolDiscovery_SuggestionBox_DetailedType.Name = "Control_DieToolDiscovery_SuggestionBox_DetailedType";
            Control_DieToolDiscovery_SuggestionBox_DetailedType.ShowF4Button = false;
            Control_DieToolDiscovery_SuggestionBox_DetailedType.Size = new Size(371, 23);
            Control_DieToolDiscovery_SuggestionBox_DetailedType.TabIndex = 19;
            // 
            // Control_DieToolDiscovery_SuggestionBox_AutoIssueLocation
            // 
            Control_DieToolDiscovery_SuggestionBox_AutoIssueLocation.AutoSize = true;
            Control_DieToolDiscovery_SuggestionBox_AutoIssueLocation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_DieToolDiscovery_SuggestionBox_AutoIssueLocation.Dock = DockStyle.Fill;
            Control_DieToolDiscovery_SuggestionBox_AutoIssueLocation.Enabled = false;
            Control_DieToolDiscovery_SuggestionBox_AutoIssueLocation.EnableSuggestions = false;
            Control_DieToolDiscovery_SuggestionBox_AutoIssueLocation.LabelText = "Auto-Issue Location";
            Control_DieToolDiscovery_SuggestionBox_AutoIssueLocation.LabelVisibility = Enum_LabelVisibility.Visible;
            Control_DieToolDiscovery_SuggestionBox_AutoIssueLocation.Location = new Point(6, 181);
            Control_DieToolDiscovery_SuggestionBox_AutoIssueLocation.Margin = new Padding(6);
            Control_DieToolDiscovery_SuggestionBox_AutoIssueLocation.MaxLength = 130;
            Control_DieToolDiscovery_SuggestionBox_AutoIssueLocation.MinimumSize = new Size(0, 23);
            Control_DieToolDiscovery_SuggestionBox_AutoIssueLocation.MinLength = 130;
            Control_DieToolDiscovery_SuggestionBox_AutoIssueLocation.Name = "Control_DieToolDiscovery_SuggestionBox_AutoIssueLocation";
            Control_DieToolDiscovery_SuggestionBox_AutoIssueLocation.ShowF4Button = false;
            Control_DieToolDiscovery_SuggestionBox_AutoIssueLocation.Size = new Size(371, 23);
            Control_DieToolDiscovery_SuggestionBox_AutoIssueLocation.TabIndex = 20;
            // 
            // Control_DieToolDiscovery
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(Control_DieToolDiscovery_TabControl_Main);
            MinimumSize = new Size(800, 350);
            Name = "Control_DieToolDiscovery";
            Size = new Size(800, 350);
            Control_DieToolDiscovery_TabControl_Main.ResumeLayout(false);
            Control_DieToolDiscovery_TabPage_Locator.ResumeLayout(false);
            Control_DieToolDiscovery_TabPage_Locator.PerformLayout();
            Control_DieToolDiscovery_GroupBox_Search.ResumeLayout(false);
            Control_DieToolDiscovery_GroupBox_Search.PerformLayout();
            Control_DieToolDiscovery_TableLayoutPanel_Main.ResumeLayout(false);
            Control_DieToolDiscovery_TableLayoutPanel_Main.PerformLayout();
            Control_DieToolDiscovery_FlowLayoutPanel_SearchType.ResumeLayout(false);
            Control_DieToolDiscovery_FlowLayoutPanel_SearchType.PerformLayout();
            Control_DieToolDiscovery_TableLayoutPanel_Search.ResumeLayout(false);
            Control_DieToolDiscovery_TableLayoutPanel_Search.PerformLayout();
            Control_DieToolDiscovery_Panel_Grid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Control_DieToolDiscovery_DataGridView_Results).EndInit();
            Control_DieToolDiscovery_TabPage_CoilFlatstock.ResumeLayout(false);
            Control_DieToolDiscovery_TableLayoutPanel_Coil.ResumeLayout(false);
            Control_DieToolDiscovery_TableLayoutPanel_Coil.PerformLayout();
            Control_DieToolDiscovery_TableLayoutPanel_CoilDetails.ResumeLayout(false);
            Control_DieToolDiscovery_TableLayoutPanel_CoilDetails.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl Control_DieToolDiscovery_TabControl_Main;
        private System.Windows.Forms.TabPage Control_DieToolDiscovery_TabPage_Locator;
        private System.Windows.Forms.GroupBox Control_DieToolDiscovery_GroupBox_Search;
        private Components.Shared.Component_SuggestionTextBoxWithLabel Control_DieToolDiscovery_SuggestionBox_Search;
        private System.Windows.Forms.Button Control_DieToolDiscovery_Button_Search;
        private TableLayoutPanel Control_DieToolDiscovery_TableLayoutPanel_Main;
        private TableLayoutPanel Control_DieToolDiscovery_TableLayoutPanel_Search;
        private Panel Control_DieToolDiscovery_Panel_Grid;
        private DataGridView Control_DieToolDiscovery_DataGridView_Results;

        private System.Windows.Forms.TabPage Control_DieToolDiscovery_TabPage_CoilFlatstock;
        private TableLayoutPanel Control_DieToolDiscovery_TableLayoutPanel_Coil;
        private Components.Shared.Component_SuggestionTextBoxWithLabel Control_DieToolDiscovery_SuggestionBox_CoilSearch;
        private Button Control_DieToolDiscovery_Button_CoilSearch;
        private TableLayoutPanel Control_DieToolDiscovery_TableLayoutPanel_CoilDetails;
        private Components.Shared.Component_SuggestionTextBoxWithLabel Control_DieToolDiscovery_SuggestionBox_Thickness;
        private Components.Shared.Component_SuggestionTextBoxWithLabel Control_DieToolDiscovery_SuggestionBox_Width;
        private Components.Shared.Component_SuggestionTextBoxWithLabel Control_DieToolDiscovery_SuggestionBox_Length;
        private Components.Shared.Component_SuggestionTextBoxWithLabel Control_DieToolDiscovery_SuggestionBox_Gauge;
        private Components.Shared.Component_SuggestionTextBoxWithLabel Control_DieToolDiscovery_SuggestionBox_WhereUsed;
        private Components.Shared.Component_SuggestionTextBoxWithLabel Control_DieToolDiscovery_SuggestionBox_Progression;
        private Components.Shared.Component_SuggestionTextBoxWithLabel Control_DieToolDiscovery_SuggestionBox_Customer;
        private Components.Shared.Component_SuggestionTextBoxWithLabel Control_DieToolDiscovery_SuggestionBox_ScrapLocation;
        private Components.Shared.Component_SuggestionTextBoxWithLabel Control_DieToolDiscovery_SuggestionBox_GenericType;
        private Components.Shared.Component_SuggestionTextBoxWithLabel Control_DieToolDiscovery_SuggestionBox_DetailedType;
        private Components.Shared.Component_SuggestionTextBoxWithLabel Control_DieToolDiscovery_SuggestionBox_AutoIssueLocation;
        private System.Windows.Forms.RadioButton Control_DieToolDiscovery_RadioButton_SearchByPart;
        private System.Windows.Forms.RadioButton Control_DieToolDiscovery_RadioButton_SearchByDie;
        private System.Windows.Forms.FlowLayoutPanel Control_DieToolDiscovery_FlowLayoutPanel_SearchType;
        private System.Windows.Forms.Button Control_DieToolDiscovery_Button_WhereUsed;
    }
}
