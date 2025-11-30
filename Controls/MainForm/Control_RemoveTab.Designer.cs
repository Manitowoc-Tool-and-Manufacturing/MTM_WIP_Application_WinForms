using System.Drawing;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Controls.Shared;

namespace MTM_WIP_Application_Winforms.Controls.MainForm
{
    partial class Control_RemoveTab
    {
        #region Fields



        private System.ComponentModel.IContainer components = null;
        private GroupBox Control_RemoveTab_GroupBox_MainControl;
        internal SuggestionTextBoxWithLabel Control_RemoveTab_TextBox_Part;
        internal SuggestionTextBoxWithLabel Control_RemoveTab_TextBox_Operation;
        private Panel Control_RemoveTab_Panel_DataGridView;
        private PictureBox Control_RemoveTab_Image_NothingFound;
        private DataGridView Control_RemoveTab_DataGridView_Main;
        private ContextMenuStrip Control_RemoveTab_ContextMenu_DataGridView;
        private Button Control_RemoveTab_Button_AdvancedItemRemoval;
        private Button Control_RemoveTab_Button_Reset;
        private Button Control_RemoveTab_Button_Delete;
        private Button Control_RemoveTab_Button_Search;
        private Button Control_RemoveTab_Button_Toggle_RightPanel;
        private TableLayoutPanel Control_RemoveTab_Panel_Main;
        private Button Control_RemoveTab_Button_Undo;
        private Button Control_RemoveTab_Button_Print;
        private TableLayoutPanel Control_RemoveTab_TableLayout_Bottom;
        private Panel Control_RemoveTab_Panel_Header;
        private TableLayoutPanel Control_RemoveTab_TableLayout_Top;
        private Button Control_RemoveTab_Button_ShowAll;

        #endregion

        #region Methods


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion
        #region Component Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Control_RemoveTab_GroupBox_MainControl = new GroupBox();
            Control_RemoveTab_Panel_Main = new TableLayoutPanel();
            Control_RemoveTab_Panel_DataGridView = new Panel();
            Control_RemoveTab_Image_NothingFound = new PictureBox();
            Control_RemoveTab_DataGridView_Main = new DataGridView();
            Control_RemoveTab_ContextMenu_DataGridView = new ContextMenuStrip(components);
            Control_RemoveTab_Panel_Header = new Panel();
            Control_RemoveTab_TableLayout_Top = new TableLayoutPanel();
            Control_RemoveTab_TextBox_Part = new SuggestionTextBoxWithLabel();
            Control_RemoveTab_TextBox_Operation = new SuggestionTextBoxWithLabel();
            Control_RemoveTab_TableLayout_Bottom = new TableLayoutPanel();
            Control_RemoveTab_Button_ShowAll = new Button();
            Control_RemoveTab_Button_AdvancedItemRemoval = new Button();
            Control_RemoveTab_Button_Delete = new Button();
            Control_RemoveTab_Button_Search = new Button();
            Control_RemoveTab_Button_Toggle_RightPanel = new Button();
            Control_RemoveTab_Button_Reset = new Button();
            Control_RemoveTab_Button_Print = new Button();
            Control_RemoveTab_Button_Undo = new Button();
            Control_RemoveTab_TableLayout_SearchToggle = new TableLayoutPanel();
            Control_RemoveTab_Button_Toggle_InputPanel = new Button();
            Control_RemoveTab_Button_SearchVisual = new Button();
            Control_RemoveTab_GroupBox_MainControl.SuspendLayout();
            Control_RemoveTab_Panel_Main.SuspendLayout();
            Control_RemoveTab_Panel_DataGridView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Control_RemoveTab_Image_NothingFound).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Control_RemoveTab_DataGridView_Main).BeginInit();
            Control_RemoveTab_Panel_Header.SuspendLayout();
            Control_RemoveTab_TableLayout_Top.SuspendLayout();
            Control_RemoveTab_TableLayout_Bottom.SuspendLayout();
            Control_RemoveTab_TableLayout_SearchToggle.SuspendLayout();
            SuspendLayout();
            // 
            // Control_RemoveTab_GroupBox_MainControl
            // 
            Control_RemoveTab_GroupBox_MainControl.AutoSize = true;
            Control_RemoveTab_GroupBox_MainControl.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_RemoveTab_GroupBox_MainControl.Controls.Add(Control_RemoveTab_Panel_Main);
            Control_RemoveTab_GroupBox_MainControl.Dock = DockStyle.Fill;
            Control_RemoveTab_GroupBox_MainControl.FlatStyle = FlatStyle.Flat;
            Control_RemoveTab_GroupBox_MainControl.Font = new Font("Segoe UI Emoji", 9F);
            Control_RemoveTab_GroupBox_MainControl.Location = new Point(0, 0);
            Control_RemoveTab_GroupBox_MainControl.Name = "Control_RemoveTab_GroupBox_MainControl";
            Control_RemoveTab_GroupBox_MainControl.Size = new Size(770, 376);
            Control_RemoveTab_GroupBox_MainControl.TabIndex = 17;
            Control_RemoveTab_GroupBox_MainControl.TabStop = false;
            Control_RemoveTab_GroupBox_MainControl.Text = "Part Lookup and Remove";
            // 
            // Control_RemoveTab_Panel_Main
            // 
            Control_RemoveTab_Panel_Main.AutoSize = true;
            Control_RemoveTab_Panel_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_RemoveTab_Panel_Main.ColumnCount = 1;
            Control_RemoveTab_Panel_Main.ColumnStyles.Add(new ColumnStyle());
            Control_RemoveTab_Panel_Main.Controls.Add(Control_RemoveTab_Panel_DataGridView, 0, 1);
            Control_RemoveTab_Panel_Main.Controls.Add(Control_RemoveTab_Panel_Header, 0, 0);
            Control_RemoveTab_Panel_Main.Controls.Add(Control_RemoveTab_TableLayout_Bottom, 0, 2);
            Control_RemoveTab_Panel_Main.Dock = DockStyle.Fill;
            Control_RemoveTab_Panel_Main.Location = new Point(3, 19);
            Control_RemoveTab_Panel_Main.Name = "Control_RemoveTab_Panel_Main";
            Control_RemoveTab_Panel_Main.RowCount = 3;
            Control_RemoveTab_Panel_Main.RowStyles.Add(new RowStyle());
            Control_RemoveTab_Panel_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_RemoveTab_Panel_Main.RowStyles.Add(new RowStyle());
            Control_RemoveTab_Panel_Main.Size = new Size(764, 354);
            Control_RemoveTab_Panel_Main.TabIndex = 0;
            // 
            // Control_RemoveTab_Panel_DataGridView
            // 
            Control_RemoveTab_Panel_DataGridView.AutoSize = true;
            Control_RemoveTab_Panel_DataGridView.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_RemoveTab_Panel_DataGridView.BorderStyle = BorderStyle.FixedSingle;
            Control_RemoveTab_Panel_DataGridView.Controls.Add(Control_RemoveTab_Image_NothingFound);
            Control_RemoveTab_Panel_DataGridView.Controls.Add(Control_RemoveTab_DataGridView_Main);
            Control_RemoveTab_Panel_DataGridView.Dock = DockStyle.Fill;
            Control_RemoveTab_Panel_DataGridView.Location = new Point(3, 38);
            Control_RemoveTab_Panel_DataGridView.Name = "Control_RemoveTab_Panel_DataGridView";
            Control_RemoveTab_Panel_DataGridView.Padding = new Padding(3);
            Control_RemoveTab_Panel_DataGridView.Size = new Size(758, 269);
            Control_RemoveTab_Panel_DataGridView.TabIndex = 21;
            // 
            // Control_RemoveTab_Image_NothingFound
            // 
            Control_RemoveTab_Image_NothingFound.BackgroundImageLayout = ImageLayout.Zoom;
            Control_RemoveTab_Image_NothingFound.Dock = DockStyle.Fill;
            Control_RemoveTab_Image_NothingFound.ErrorImage = null;
            Control_RemoveTab_Image_NothingFound.Image = Properties.Resources.NothingFound;
            Control_RemoveTab_Image_NothingFound.InitialImage = null;
            Control_RemoveTab_Image_NothingFound.Location = new Point(3, 3);
            Control_RemoveTab_Image_NothingFound.Name = "Control_RemoveTab_Image_NothingFound";
            Control_RemoveTab_Image_NothingFound.Size = new Size(750, 261);
            Control_RemoveTab_Image_NothingFound.SizeMode = PictureBoxSizeMode.CenterImage;
            Control_RemoveTab_Image_NothingFound.TabIndex = 6;
            Control_RemoveTab_Image_NothingFound.TabStop = false;
            Control_RemoveTab_Image_NothingFound.Visible = false;
            // 
            // Control_RemoveTab_DataGridView_Main
            // 
            Control_RemoveTab_DataGridView_Main.AllowUserToAddRows = false;
            Control_RemoveTab_DataGridView_Main.AllowUserToDeleteRows = false;
            Control_RemoveTab_DataGridView_Main.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Control_RemoveTab_DataGridView_Main.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            Control_RemoveTab_DataGridView_Main.BorderStyle = BorderStyle.Fixed3D;
            Control_RemoveTab_DataGridView_Main.CellBorderStyle = DataGridViewCellBorderStyle.Sunken;
            Control_RemoveTab_DataGridView_Main.ClipboardCopyMode = DataGridViewClipboardCopyMode.Disable;
            Control_RemoveTab_DataGridView_Main.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken;
            Control_RemoveTab_DataGridView_Main.ColumnHeadersHeight = 34;
            Control_RemoveTab_DataGridView_Main.ContextMenuStrip = Control_RemoveTab_ContextMenu_DataGridView;
            Control_RemoveTab_DataGridView_Main.Dock = DockStyle.Fill;
            Control_RemoveTab_DataGridView_Main.EditMode = DataGridViewEditMode.EditProgrammatically;
            Control_RemoveTab_DataGridView_Main.Location = new Point(3, 3);
            Control_RemoveTab_DataGridView_Main.Name = "Control_RemoveTab_DataGridView_Main";
            Control_RemoveTab_DataGridView_Main.ReadOnly = true;
            Control_RemoveTab_DataGridView_Main.RowHeadersWidth = 62;
            Control_RemoveTab_DataGridView_Main.RowTemplate.ReadOnly = true;
            Control_RemoveTab_DataGridView_Main.RowTemplate.Resizable = DataGridViewTriState.True;
            Control_RemoveTab_DataGridView_Main.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Control_RemoveTab_DataGridView_Main.ShowCellErrors = false;
            Control_RemoveTab_DataGridView_Main.ShowCellToolTips = false;
            Control_RemoveTab_DataGridView_Main.ShowEditingIcon = false;
            Control_RemoveTab_DataGridView_Main.ShowRowErrors = false;
            Control_RemoveTab_DataGridView_Main.Size = new Size(750, 261);
            Control_RemoveTab_DataGridView_Main.StandardTab = true;
            Control_RemoveTab_DataGridView_Main.TabIndex = 4;
            // 
            // Control_RemoveTab_ContextMenu_DataGridView
            // 
            Control_RemoveTab_ContextMenu_DataGridView.Name = "Control_RemoveTab_ContextMenu_DataGridView";
            Control_RemoveTab_ContextMenu_DataGridView.Size = new Size(61, 4);
            // 
            // Control_RemoveTab_Panel_Header
            // 
            Control_RemoveTab_Panel_Header.AutoSize = true;
            Control_RemoveTab_Panel_Header.Controls.Add(Control_RemoveTab_TableLayout_Top);
            Control_RemoveTab_Panel_Header.Dock = DockStyle.Fill;
            Control_RemoveTab_Panel_Header.Location = new Point(3, 3);
            Control_RemoveTab_Panel_Header.Name = "Control_RemoveTab_Panel_Header";
            Control_RemoveTab_Panel_Header.Size = new Size(758, 29);
            Control_RemoveTab_Panel_Header.TabIndex = 22;
            // 
            // Control_RemoveTab_TableLayout_Top
            // 
            Control_RemoveTab_TableLayout_Top.AutoSize = true;
            Control_RemoveTab_TableLayout_Top.ColumnCount = 2;
            Control_RemoveTab_TableLayout_Top.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            Control_RemoveTab_TableLayout_Top.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            Control_RemoveTab_TableLayout_Top.Controls.Add(Control_RemoveTab_TextBox_Part, 0, 0);
            Control_RemoveTab_TableLayout_Top.Controls.Add(Control_RemoveTab_TextBox_Operation, 1, 0);
            Control_RemoveTab_TableLayout_Top.Dock = DockStyle.Fill;
            Control_RemoveTab_TableLayout_Top.Location = new Point(0, 0);
            Control_RemoveTab_TableLayout_Top.Name = "Control_RemoveTab_TableLayout_Top";
            Control_RemoveTab_TableLayout_Top.RowCount = 1;
            Control_RemoveTab_TableLayout_Top.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_RemoveTab_TableLayout_Top.Size = new Size(758, 29);
            Control_RemoveTab_TableLayout_Top.TabIndex = 6;
            // 
            // Control_RemoveTab_TextBox_Part
            // 
            Control_RemoveTab_TextBox_Part.AutoSize = true;
            Control_RemoveTab_TextBox_Part.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_RemoveTab_TextBox_Part.Dock = DockStyle.Fill;
            Control_RemoveTab_TextBox_Part.LabelText = "Part Number";
            Control_RemoveTab_TextBox_Part.Location = new Point(3, 3);
            Control_RemoveTab_TextBox_Part.MaxLength = 130;
            Control_RemoveTab_TextBox_Part.MinimumSize = new Size(0, 23);
            Control_RemoveTab_TextBox_Part.MinLength = 130;
            Control_RemoveTab_TextBox_Part.Name = "Control_RemoveTab_TextBox_Part";
            Control_RemoveTab_TextBox_Part.PlaceholderText = "Enter Part Number";
            Control_RemoveTab_TextBox_Part.Size = new Size(373, 23);
            Control_RemoveTab_TextBox_Part.TabIndex = 0;
            // 
            // Control_RemoveTab_TextBox_Operation
            // 
            Control_RemoveTab_TextBox_Operation.AutoSize = true;
            Control_RemoveTab_TextBox_Operation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_RemoveTab_TextBox_Operation.Dock = DockStyle.Fill;
            Control_RemoveTab_TextBox_Operation.LabelText = "Operation";
            Control_RemoveTab_TextBox_Operation.Location = new Point(382, 3);
            Control_RemoveTab_TextBox_Operation.MaxLength = 130;
            Control_RemoveTab_TextBox_Operation.MinimumSize = new Size(0, 23);
            Control_RemoveTab_TextBox_Operation.MinLength = 130;
            Control_RemoveTab_TextBox_Operation.Name = "Control_RemoveTab_TextBox_Operation";
            Control_RemoveTab_TextBox_Operation.PlaceholderText = "Enter Operation";
            Control_RemoveTab_TextBox_Operation.Size = new Size(373, 23);
            Control_RemoveTab_TextBox_Operation.TabIndex = 1;
            // 
            // Control_RemoveTab_TableLayout_Bottom
            // 
            Control_RemoveTab_TableLayout_Bottom.AutoSize = true;
            Control_RemoveTab_TableLayout_Bottom.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_RemoveTab_TableLayout_Bottom.ColumnCount = 9;
            Control_RemoveTab_TableLayout_Bottom.ColumnStyles.Add(new ColumnStyle());
            Control_RemoveTab_TableLayout_Bottom.ColumnStyles.Add(new ColumnStyle());
            Control_RemoveTab_TableLayout_Bottom.ColumnStyles.Add(new ColumnStyle());
            Control_RemoveTab_TableLayout_Bottom.ColumnStyles.Add(new ColumnStyle());
            Control_RemoveTab_TableLayout_Bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_RemoveTab_TableLayout_Bottom.ColumnStyles.Add(new ColumnStyle());
            Control_RemoveTab_TableLayout_Bottom.ColumnStyles.Add(new ColumnStyle());
            Control_RemoveTab_TableLayout_Bottom.ColumnStyles.Add(new ColumnStyle());
            Control_RemoveTab_TableLayout_Bottom.ColumnStyles.Add(new ColumnStyle());
            Control_RemoveTab_TableLayout_Bottom.Controls.Add(Control_RemoveTab_Button_ShowAll, 3, 0);
            Control_RemoveTab_TableLayout_Bottom.Controls.Add(Control_RemoveTab_Button_AdvancedItemRemoval, 2, 0);
            Control_RemoveTab_TableLayout_Bottom.Controls.Add(Control_RemoveTab_Button_Delete, 1, 0);
            Control_RemoveTab_TableLayout_Bottom.Controls.Add(Control_RemoveTab_Button_Search, 0, 0);
            Control_RemoveTab_TableLayout_Bottom.Controls.Add(Control_RemoveTab_Button_Toggle_RightPanel, 8, 0);
            Control_RemoveTab_TableLayout_Bottom.Controls.Add(Control_RemoveTab_Button_Reset, 7, 0);
            Control_RemoveTab_TableLayout_Bottom.Controls.Add(Control_RemoveTab_Button_Print, 6, 0);
            Control_RemoveTab_TableLayout_Bottom.Controls.Add(Control_RemoveTab_Button_Undo, 5, 0);
            Control_RemoveTab_TableLayout_Bottom.Controls.Add(Control_RemoveTab_TableLayout_SearchToggle, 4, 0);
            Control_RemoveTab_TableLayout_Bottom.Dock = DockStyle.Fill;
            Control_RemoveTab_TableLayout_Bottom.Location = new Point(3, 313);
            Control_RemoveTab_TableLayout_Bottom.Name = "Control_RemoveTab_TableLayout_Bottom";
            Control_RemoveTab_TableLayout_Bottom.RowCount = 1;
            Control_RemoveTab_TableLayout_Bottom.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_RemoveTab_TableLayout_Bottom.Size = new Size(758, 38);
            Control_RemoveTab_TableLayout_Bottom.TabIndex = 23;
            // 
            // Control_RemoveTab_Button_ShowAll
            // 
            Control_RemoveTab_Button_ShowAll.AutoSize = true;
            Control_RemoveTab_Button_ShowAll.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_RemoveTab_Button_ShowAll.Dock = DockStyle.Fill;
            Control_RemoveTab_Button_ShowAll.Font = new Font("Segoe UI Emoji", 9F);
            Control_RemoveTab_Button_ShowAll.Location = new Point(193, 3);
            Control_RemoveTab_Button_ShowAll.MaximumSize = new Size(0, 32);
            Control_RemoveTab_Button_ShowAll.MinimumSize = new Size(0, 32);
            Control_RemoveTab_Button_ShowAll.Name = "Control_RemoveTab_Button_ShowAll";
            Control_RemoveTab_Button_ShowAll.Size = new Size(63, 32);
            Control_RemoveTab_Button_ShowAll.TabIndex = 10;
            Control_RemoveTab_Button_ShowAll.TabStop = false;
            Control_RemoveTab_Button_ShowAll.Text = "Show All";
            Control_RemoveTab_Button_ShowAll.UseVisualStyleBackColor = true;
            Control_RemoveTab_Button_ShowAll.Click += Control_RemoveTab_Button_ShowAll_Click;
            // 
            // Control_RemoveTab_Button_AdvancedItemRemoval
            // 
            Control_RemoveTab_Button_AdvancedItemRemoval.AutoSize = true;
            Control_RemoveTab_Button_AdvancedItemRemoval.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_RemoveTab_Button_AdvancedItemRemoval.Dock = DockStyle.Fill;
            Control_RemoveTab_Button_AdvancedItemRemoval.Font = new Font("Segoe UI Emoji", 9F);
            Control_RemoveTab_Button_AdvancedItemRemoval.ForeColor = Color.DarkRed;
            Control_RemoveTab_Button_AdvancedItemRemoval.Location = new Point(117, 3);
            Control_RemoveTab_Button_AdvancedItemRemoval.MaximumSize = new Size(0, 32);
            Control_RemoveTab_Button_AdvancedItemRemoval.MinimumSize = new Size(0, 32);
            Control_RemoveTab_Button_AdvancedItemRemoval.Name = "Control_RemoveTab_Button_AdvancedItemRemoval";
            Control_RemoveTab_Button_AdvancedItemRemoval.Size = new Size(70, 32);
            Control_RemoveTab_Button_AdvancedItemRemoval.TabIndex = 5;
            Control_RemoveTab_Button_AdvancedItemRemoval.Text = "Advanced";
            Control_RemoveTab_Button_AdvancedItemRemoval.UseVisualStyleBackColor = true;
            // 
            // Control_RemoveTab_Button_Delete
            // 
            Control_RemoveTab_Button_Delete.AutoSize = true;
            Control_RemoveTab_Button_Delete.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_RemoveTab_Button_Delete.Dock = DockStyle.Fill;
            Control_RemoveTab_Button_Delete.Font = new Font("Segoe UI Emoji", 9F);
            Control_RemoveTab_Button_Delete.Location = new Point(61, 3);
            Control_RemoveTab_Button_Delete.MaximumSize = new Size(0, 32);
            Control_RemoveTab_Button_Delete.MinimumSize = new Size(0, 32);
            Control_RemoveTab_Button_Delete.Name = "Control_RemoveTab_Button_Delete";
            Control_RemoveTab_Button_Delete.Size = new Size(50, 32);
            Control_RemoveTab_Button_Delete.TabIndex = 4;
            Control_RemoveTab_Button_Delete.Text = "Delete";
            Control_RemoveTab_Button_Delete.UseVisualStyleBackColor = true;
            // 
            // Control_RemoveTab_Button_Search
            // 
            Control_RemoveTab_Button_Search.AutoSize = true;
            Control_RemoveTab_Button_Search.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_RemoveTab_Button_Search.Dock = DockStyle.Fill;
            Control_RemoveTab_Button_Search.Font = new Font("Segoe UI Emoji", 9F);
            Control_RemoveTab_Button_Search.Location = new Point(3, 3);
            Control_RemoveTab_Button_Search.MaximumSize = new Size(0, 32);
            Control_RemoveTab_Button_Search.MinimumSize = new Size(0, 32);
            Control_RemoveTab_Button_Search.Name = "Control_RemoveTab_Button_Search";
            Control_RemoveTab_Button_Search.Size = new Size(52, 32);
            Control_RemoveTab_Button_Search.TabIndex = 3;
            Control_RemoveTab_Button_Search.Text = "Search";
            Control_RemoveTab_Button_Search.UseVisualStyleBackColor = true;
            Control_RemoveTab_Button_Search.Click += Control_RemoveTab_Button_Search_Click;
            // 
            // Control_RemoveTab_Button_Toggle_RightPanel
            // 
            Control_RemoveTab_Button_Toggle_RightPanel.Dock = DockStyle.Fill;
            Control_RemoveTab_Button_Toggle_RightPanel.Font = new Font("Segoe UI Emoji", 9F);
            Control_RemoveTab_Button_Toggle_RightPanel.Location = new Point(691, 3);
            Control_RemoveTab_Button_Toggle_RightPanel.MaximumSize = new Size(64, 32);
            Control_RemoveTab_Button_Toggle_RightPanel.MinimumSize = new Size(64, 32);
            Control_RemoveTab_Button_Toggle_RightPanel.Name = "Control_RemoveTab_Button_Toggle_RightPanel";
            Control_RemoveTab_Button_Toggle_RightPanel.Size = new Size(64, 32);
            Control_RemoveTab_Button_Toggle_RightPanel.TabIndex = 9;
            Control_RemoveTab_Button_Toggle_RightPanel.Text = "➡";
            Control_RemoveTab_Button_Toggle_RightPanel.UseVisualStyleBackColor = true;
            Control_RemoveTab_Button_Toggle_RightPanel.Click += Control_RemoveTab_Button_Toggle_RightPanel_Click;
            // 
            // Control_RemoveTab_Button_Reset
            // 
            Control_RemoveTab_Button_Reset.AutoSize = true;
            Control_RemoveTab_Button_Reset.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_RemoveTab_Button_Reset.Dock = DockStyle.Fill;
            Control_RemoveTab_Button_Reset.Font = new Font("Segoe UI Emoji", 9F);
            Control_RemoveTab_Button_Reset.Location = new Point(640, 3);
            Control_RemoveTab_Button_Reset.MaximumSize = new Size(0, 32);
            Control_RemoveTab_Button_Reset.MinimumSize = new Size(0, 32);
            Control_RemoveTab_Button_Reset.Name = "Control_RemoveTab_Button_Reset";
            Control_RemoveTab_Button_Reset.Size = new Size(45, 32);
            Control_RemoveTab_Button_Reset.TabIndex = 8;
            Control_RemoveTab_Button_Reset.TabStop = false;
            Control_RemoveTab_Button_Reset.Text = "Reset";
            Control_RemoveTab_Button_Reset.UseVisualStyleBackColor = true;
            // 
            // Control_RemoveTab_Button_Print
            // 
            Control_RemoveTab_Button_Print.AutoSize = true;
            Control_RemoveTab_Button_Print.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_RemoveTab_Button_Print.Dock = DockStyle.Fill;
            Control_RemoveTab_Button_Print.Enabled = false;
            Control_RemoveTab_Button_Print.Font = new Font("Segoe UI Emoji", 9F);
            Control_RemoveTab_Button_Print.Location = new Point(592, 3);
            Control_RemoveTab_Button_Print.MaximumSize = new Size(0, 32);
            Control_RemoveTab_Button_Print.MinimumSize = new Size(0, 32);
            Control_RemoveTab_Button_Print.Name = "Control_RemoveTab_Button_Print";
            Control_RemoveTab_Button_Print.Size = new Size(42, 32);
            Control_RemoveTab_Button_Print.TabIndex = 7;
            Control_RemoveTab_Button_Print.Text = "Print";
            Control_RemoveTab_Button_Print.UseVisualStyleBackColor = true;
            // 
            // Control_RemoveTab_Button_Undo
            // 
            Control_RemoveTab_Button_Undo.AutoSize = true;
            Control_RemoveTab_Button_Undo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_RemoveTab_Button_Undo.Dock = DockStyle.Fill;
            Control_RemoveTab_Button_Undo.Enabled = false;
            Control_RemoveTab_Button_Undo.Font = new Font("Segoe UI Emoji", 9F);
            Control_RemoveTab_Button_Undo.Location = new Point(540, 3);
            Control_RemoveTab_Button_Undo.MaximumSize = new Size(0, 32);
            Control_RemoveTab_Button_Undo.MinimumSize = new Size(0, 32);
            Control_RemoveTab_Button_Undo.Name = "Control_RemoveTab_Button_Undo";
            Control_RemoveTab_Button_Undo.Size = new Size(46, 32);
            Control_RemoveTab_Button_Undo.TabIndex = 6;
            Control_RemoveTab_Button_Undo.TabStop = false;
            Control_RemoveTab_Button_Undo.Text = "Undo";
            Control_RemoveTab_Button_Undo.UseVisualStyleBackColor = true;
            Control_RemoveTab_Button_Undo.Click += Control_RemoveTab_Button_Undo_Click;
            // 
            // Control_RemoveTab_TableLayout_SearchToggle
            // 
            Control_RemoveTab_TableLayout_SearchToggle.AutoSize = true;
            Control_RemoveTab_TableLayout_SearchToggle.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_RemoveTab_TableLayout_SearchToggle.ColumnCount = 5;
            Control_RemoveTab_TableLayout_SearchToggle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            Control_RemoveTab_TableLayout_SearchToggle.ColumnStyles.Add(new ColumnStyle());
            Control_RemoveTab_TableLayout_SearchToggle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            Control_RemoveTab_TableLayout_SearchToggle.ColumnStyles.Add(new ColumnStyle());
            Control_RemoveTab_TableLayout_SearchToggle.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            Control_RemoveTab_TableLayout_SearchToggle.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            Control_RemoveTab_TableLayout_SearchToggle.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            Control_RemoveTab_TableLayout_SearchToggle.Controls.Add(Control_RemoveTab_Button_SearchVisual, 1, 0);
            Control_RemoveTab_TableLayout_SearchToggle.Controls.Add(Control_RemoveTab_Button_Toggle_InputPanel, 3, 0);
            Control_RemoveTab_TableLayout_SearchToggle.Dock = DockStyle.Fill;
            Control_RemoveTab_TableLayout_SearchToggle.Location = new Point(259, 0);
            Control_RemoveTab_TableLayout_SearchToggle.Margin = new Padding(0);
            Control_RemoveTab_TableLayout_SearchToggle.Name = "Control_RemoveTab_TableLayout_SearchToggle";
            Control_RemoveTab_TableLayout_SearchToggle.RowCount = 1;
            Control_RemoveTab_TableLayout_SearchToggle.RowStyles.Add(new RowStyle());
            Control_RemoveTab_TableLayout_SearchToggle.Size = new Size(278, 38);
            Control_RemoveTab_TableLayout_SearchToggle.TabIndex = 11;
            // 
            // Control_RemoveTab_Button_Toggle_InputPanel
            // 
            Control_RemoveTab_Button_Toggle_InputPanel.Dock = DockStyle.Fill;
            Control_RemoveTab_Button_Toggle_InputPanel.Font = new Font("Segoe UI Emoji", 9F);
            Control_RemoveTab_Button_Toggle_InputPanel.Location = new Point(171, 3);
            Control_RemoveTab_Button_Toggle_InputPanel.MaximumSize = new Size(64, 32);
            Control_RemoveTab_Button_Toggle_InputPanel.MinimumSize = new Size(64, 32);
            Control_RemoveTab_Button_Toggle_InputPanel.Name = "Control_RemoveTab_Button_Toggle_InputPanel";
            Control_RemoveTab_Button_Toggle_InputPanel.Size = new Size(64, 32);
            Control_RemoveTab_Button_Toggle_InputPanel.TabIndex = 10;
            Control_RemoveTab_Button_Toggle_InputPanel.UseVisualStyleBackColor = true;
            Control_RemoveTab_Button_Toggle_InputPanel.Click += Control_RemoveTab_Button_Toggle_InputPanel_Click;
            // 
            // Control_RemoveTab_Button_SearchVisual
            // 
            Control_RemoveTab_Button_SearchVisual.AutoSize = true;
            Control_RemoveTab_Button_SearchVisual.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_RemoveTab_Button_SearchVisual.Dock = DockStyle.Fill;
            Control_RemoveTab_Button_SearchVisual.Enabled = false;
            Control_RemoveTab_Button_SearchVisual.Font = new Font("Segoe UI Emoji", 9F);
            Control_RemoveTab_Button_SearchVisual.Location = new Point(41, 3);
            Control_RemoveTab_Button_SearchVisual.MaximumSize = new Size(0, 32);
            Control_RemoveTab_Button_SearchVisual.MinimumSize = new Size(0, 32);
            Control_RemoveTab_Button_SearchVisual.Name = "Control_RemoveTab_Button_SearchVisual";
            Control_RemoveTab_Button_SearchVisual.Size = new Size(86, 32);
            Control_RemoveTab_Button_SearchVisual.TabIndex = 17;
            Control_RemoveTab_Button_SearchVisual.Text = "Search Visual";
            // 
            // Control_RemoveTab
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            Controls.Add(Control_RemoveTab_GroupBox_MainControl);
            Name = "Control_RemoveTab";
            Size = new Size(770, 376);
            Control_RemoveTab_GroupBox_MainControl.ResumeLayout(false);
            Control_RemoveTab_GroupBox_MainControl.PerformLayout();
            Control_RemoveTab_Panel_Main.ResumeLayout(false);
            Control_RemoveTab_Panel_Main.PerformLayout();
            Control_RemoveTab_Panel_DataGridView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Control_RemoveTab_Image_NothingFound).EndInit();
            ((System.ComponentModel.ISupportInitialize)Control_RemoveTab_DataGridView_Main).EndInit();
            Control_RemoveTab_Panel_Header.ResumeLayout(false);
            Control_RemoveTab_Panel_Header.PerformLayout();
            Control_RemoveTab_TableLayout_Top.ResumeLayout(false);
            Control_RemoveTab_TableLayout_Top.PerformLayout();
            Control_RemoveTab_TableLayout_Bottom.ResumeLayout(false);
            Control_RemoveTab_TableLayout_Bottom.PerformLayout();
            Control_RemoveTab_TableLayout_SearchToggle.ResumeLayout(false);
            Control_RemoveTab_TableLayout_SearchToggle.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel Control_RemoveTab_TableLayout_SearchToggle;
        private Button Control_RemoveTab_Button_Toggle_InputPanel;
        private Button Control_RemoveTab_Button_SearchVisual;
    }
}
