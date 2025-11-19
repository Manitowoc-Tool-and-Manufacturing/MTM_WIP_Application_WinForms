using System.Drawing;
using System.Windows.Forms;

namespace MTM_WIP_Application_Winforms.Controls.MainForm
{
    partial class Control_AdvancedInventory
    {
        #region Fields
        


        private System.ComponentModel.IContainer components = null;

        #endregion

        private TabControl AdvancedInventory_TabControl;
        private TabPage AdvancedInventory_TabControl_Single;
        private Button AdvancedInventory_Single_Button_Save;
        private Button AdvancedInventory_Single_Button_Reset;
        private TabPage AdvancedInventory_TabControl_MultiLoc;
        private GroupBox AdvancedInventory_MultiLoc_GroupBox_Item;
        private GroupBox AdvancedInventory_MultiLoc_GroupBox_Preview;
        private ListView AdvancedInventory_MultiLoc_ListView_Preview;
        private Button AdvancedInventory_MultiLoc_Button_SaveAll;
        private Button AdvancedInventory_MultiLoc_Button_Reset;
        private TabPage AdvancedInventory_TabControl_Import;
        private TableLayoutPanel AdvancedInventory_Import_TableLayout;
        private Panel AdvancedInventory_Import_Panel_Middle;
        private DataGridView AdvancedInventory_Import_DataGridView;
        private Button AdvancedInventory_Import_Button_OpenExcel;
        private Button AdvancedInventory_Import_Button_ImportExcel;
        private Button AdvancedInventory_Import_Button_Save;
        private GroupBox AdvancedInventory_Single_GroupBox_Right;
        private Button AdvancedInventory_Single_Button_Normal;
        private Button AdvancedInventory_Multi_Button_Normal;
        private Button AdvancedInventory_Import_Button_Normal;
        private Controls.Shared.SuggestionTextBox AdvancedInventory_Single_TextBox_Part;
        private Controls.Shared.SuggestionTextBox AdvancedInventory_Single_TextBox_Op;
        private Controls.Shared.SuggestionTextBox AdvancedInventory_Single_TextBox_Loc;
        private GroupBox AdvancedInventory_GroupBox_Main;
        private TableLayoutPanel AdvancedInventory_TableLayout_Single;
        private TableLayoutPanel AdvancedInventory_TableLayoutPanel_Multi;
        


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            AdvancedInventory_TabControl = new TabControl();
            AdvancedInventory_TabControl_Single = new TabPage();
            AdvancedInventory_TableLayout_Single = new TableLayoutPanel();
            AdvancedInventory_Single_GroupBox_Right = new GroupBox();
            AdvancedInventory_Single_TableLayout_Right = new TableLayoutPanel();
            AdvancedInventory_Single_TableLayout_LowerRight = new TableLayoutPanel();
            AdvancedInventory_Single_Button_InputToggle = new Button();
            AdvancedInventory_Single_Button_Normal = new Button();
            AdvancedInventory_Single_Button_Reset = new Button();
            AdvancedInventory_Single_Button_Save = new Button();
            AdvancedInventory_Single_Button_QuickButtonToggle = new Button();
            AdvancedInventory_Single_Panel_Preview = new Panel();
            AdvancedInventory_Single_ListView_Preview = new ListView();
            AdvancedInventory_Single_GroupBox_Left = new GroupBox();
            AdvancedInventory_Single_TableLayout_Left = new TableLayoutPanel();
            AdvancedInventory_Single_Button_LocationF4 = new Button();
            AdvancedInventory_Single_Button_OperationF4 = new Button();
            AdvancedInventory_Single_Button_PartF4 = new Button();
            AdvancedInventory_Single_Label_Part = new Label();
            AdvancedInventory_Single_Button_Send = new Button();
            AdvancedInventory_Single_TextBox_Part = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBox();
            AdvancedInventory_Single_Label_Op = new Label();
            AdvancedInventory_Single_Label_Qty = new Label();
            AdvancedInventory_Single_Label_Loc = new Label();
            AdvancedInventory_Single_TextBox_Op = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBox();
            AdvancedInventory_Single_Label_Count = new Label();
            AdvancedInventory_Single_TextBox_Qty = new TextBox();
            AdvancedInventory_Single_TextBox_Loc = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBox();
            AdvancedInventory_Single_Label_Notes = new Label();
            AdvancedInventory_Single_TextBox_Count = new TextBox();
            panel4 = new Panel();
            AdvancedInventory_Single_RichTextBox_Notes = new RichTextBox();
            AdvancedInventory_TabControl_MultiLoc = new TabPage();
            AdvancedInventory_TableLayoutPanel_Multi = new TableLayoutPanel();
            AdvancedInventory_MultiLoc_GroupBox_Preview = new GroupBox();
            AdvancedInventory_Multi_TableLayout_Right = new TableLayoutPanel();
            panel1 = new Panel();
            AdvancedInventory_MultiLoc_ListView_Preview = new ListView();
            AdvancedInventory_Multi_TableLayout_BottomRight = new TableLayoutPanel();
            AdvancedInventory_Multi_Button_InputToggle = new Button();
            AdvancedInventory_Multi_Button_Normal = new Button();
            AdvancedInventory_MultiLoc_Button_Reset = new Button();
            AdvancedInventory_MultiLoc_Button_SaveAll = new Button();
            AdvancedInventory_Multi_Button_QuickButtonToggle = new Button();
            AdvancedInventory_MultiLoc_GroupBox_Item = new GroupBox();
            AdvancedInventory_Multi_TableLayout_Left = new TableLayoutPanel();
            AdvancedInventory_MultiLoc_Button_LocationF4 = new Button();
            AdvancedInventory_MultiLoc_Button_OperationF4 = new Button();
            AdvancedInventory_MultiLoc_Label_Part = new Label();
            AdvancedInventory_MultiLoc_TextBox_Part = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBox();
            AdvancedInventory_MultiLoc_Label_Op = new Label();
            AdvancedInventory_MultiLoc_TextBox_Op = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBox();
            AdvancedInventory_MultiLoc_Label_Qty = new Label();
            AdvancedInventory_MultiLoc_Label_Notes = new Label();
            AdvancedInventory_MultiLoc_TextBox_Qty = new TextBox();
            AdvancedInventory_MultiLoc_Label_Loc = new Label();
            AdvancedInventory_MultiLoc_TextBox_Loc = new MTM_WIP_Application_Winforms.Controls.Shared.SuggestionTextBox();
            panel3 = new Panel();
            AdvancedInventory_MultiLoc_RichTextBox_Notes = new RichTextBox();
            AdvancedInventory_MultiLoc_Button_PartF4 = new Button();
            AdvancedInventory_MultiLoc_Button_AddLoc = new Button();
            AdvancedInventory_TabControl_Import = new TabPage();
            AdvancedInventory_Import_TableLayout = new TableLayoutPanel();
            AdvancedInventory_Import_Panel_Middle = new Panel();
            AdvancedInventory_Import_DataGridView = new DataGridView();
            AdvancedInventory_Import_TableLayout_Bottom = new TableLayoutPanel();
            AdvancedInventory_Import_Button_Save = new Button();
            AdvancedInventory_Import_Button_CleanSheet = new Button();
            AdvancedInventory_Import_Button_Normal = new Button();
            AdvancedInventory_Import_Button_QuickButtonToggle = new Button();
            AdvancedInventory_Import_TableLayout_Top = new TableLayoutPanel();
            AdvancedInventory_Import_Button_OpenExcel = new Button();
            AdvancedInventory_Import_Button_ImportExcel = new Button();
            AdvancedInventory_GroupBox_Main = new GroupBox();
            AdvancedInventory_TabControl.SuspendLayout();
            AdvancedInventory_TabControl_Single.SuspendLayout();
            AdvancedInventory_TableLayout_Single.SuspendLayout();
            AdvancedInventory_Single_GroupBox_Right.SuspendLayout();
            AdvancedInventory_Single_TableLayout_Right.SuspendLayout();
            AdvancedInventory_Single_TableLayout_LowerRight.SuspendLayout();
            AdvancedInventory_Single_Panel_Preview.SuspendLayout();
            AdvancedInventory_Single_GroupBox_Left.SuspendLayout();
            AdvancedInventory_Single_TableLayout_Left.SuspendLayout();
            panel4.SuspendLayout();
            AdvancedInventory_TabControl_MultiLoc.SuspendLayout();
            AdvancedInventory_TableLayoutPanel_Multi.SuspendLayout();
            AdvancedInventory_MultiLoc_GroupBox_Preview.SuspendLayout();
            AdvancedInventory_Multi_TableLayout_Right.SuspendLayout();
            panel1.SuspendLayout();
            AdvancedInventory_Multi_TableLayout_BottomRight.SuspendLayout();
            AdvancedInventory_MultiLoc_GroupBox_Item.SuspendLayout();
            AdvancedInventory_Multi_TableLayout_Left.SuspendLayout();
            panel3.SuspendLayout();
            AdvancedInventory_TabControl_Import.SuspendLayout();
            AdvancedInventory_Import_TableLayout.SuspendLayout();
            AdvancedInventory_Import_Panel_Middle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)AdvancedInventory_Import_DataGridView).BeginInit();
            AdvancedInventory_Import_TableLayout_Bottom.SuspendLayout();
            AdvancedInventory_Import_TableLayout_Top.SuspendLayout();
            AdvancedInventory_GroupBox_Main.SuspendLayout();
            SuspendLayout();
            // 
            // AdvancedInventory_TabControl
            // 
            AdvancedInventory_TabControl.Controls.Add(AdvancedInventory_TabControl_Single);
            AdvancedInventory_TabControl.Controls.Add(AdvancedInventory_TabControl_MultiLoc);
            AdvancedInventory_TabControl.Controls.Add(AdvancedInventory_TabControl_Import);
            AdvancedInventory_TabControl.Dock = DockStyle.Fill;
            AdvancedInventory_TabControl.Location = new Point(3, 19);
            AdvancedInventory_TabControl.Name = "AdvancedInventory_TabControl";
            AdvancedInventory_TabControl.SelectedIndex = 0;
            AdvancedInventory_TabControl.Size = new Size(724, 357);
            AdvancedInventory_TabControl.TabIndex = 1;
            // 
            // AdvancedInventory_TabControl_Single
            // 
            AdvancedInventory_TabControl_Single.Controls.Add(AdvancedInventory_TableLayout_Single);
            AdvancedInventory_TabControl_Single.Location = new Point(4, 24);
            AdvancedInventory_TabControl_Single.Margin = new Padding(0);
            AdvancedInventory_TabControl_Single.Name = "AdvancedInventory_TabControl_Single";
            AdvancedInventory_TabControl_Single.Size = new Size(716, 329);
            AdvancedInventory_TabControl_Single.TabIndex = 0;
            AdvancedInventory_TabControl_Single.Text = "Single Item, Multiple Times";
            // 
            // AdvancedInventory_TableLayout_Single
            // 
            AdvancedInventory_TableLayout_Single.AutoSize = true;
            AdvancedInventory_TableLayout_Single.ColumnCount = 2;
            AdvancedInventory_TableLayout_Single.ColumnStyles.Add(new ColumnStyle());
            AdvancedInventory_TableLayout_Single.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            AdvancedInventory_TableLayout_Single.Controls.Add(AdvancedInventory_Single_GroupBox_Right, 1, 0);
            AdvancedInventory_TableLayout_Single.Controls.Add(AdvancedInventory_Single_GroupBox_Left, 0, 0);
            AdvancedInventory_TableLayout_Single.Dock = DockStyle.Fill;
            AdvancedInventory_TableLayout_Single.Location = new Point(0, 0);
            AdvancedInventory_TableLayout_Single.Margin = new Padding(0);
            AdvancedInventory_TableLayout_Single.Name = "AdvancedInventory_TableLayout_Single";
            AdvancedInventory_TableLayout_Single.RowCount = 1;
            AdvancedInventory_TableLayout_Single.RowStyles.Add(new RowStyle());
            AdvancedInventory_TableLayout_Single.Size = new Size(716, 329);
            AdvancedInventory_TableLayout_Single.TabIndex = 2;
            // 
            // AdvancedInventory_Single_GroupBox_Right
            // 
            AdvancedInventory_Single_GroupBox_Right.AutoSize = true;
            AdvancedInventory_Single_GroupBox_Right.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AdvancedInventory_Single_GroupBox_Right.Controls.Add(AdvancedInventory_Single_TableLayout_Right);
            AdvancedInventory_Single_GroupBox_Right.Dock = DockStyle.Fill;
            AdvancedInventory_Single_GroupBox_Right.Location = new Point(239, 0);
            AdvancedInventory_Single_GroupBox_Right.Margin = new Padding(0);
            AdvancedInventory_Single_GroupBox_Right.Name = "AdvancedInventory_Single_GroupBox_Right";
            AdvancedInventory_Single_GroupBox_Right.Size = new Size(477, 329);
            AdvancedInventory_Single_GroupBox_Right.TabIndex = 1;
            AdvancedInventory_Single_GroupBox_Right.TabStop = false;
            AdvancedInventory_Single_GroupBox_Right.Text = "Transaction Preview";
            // 
            // AdvancedInventory_Single_TableLayout_Right
            // 
            AdvancedInventory_Single_TableLayout_Right.AutoSize = true;
            AdvancedInventory_Single_TableLayout_Right.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AdvancedInventory_Single_TableLayout_Right.ColumnCount = 1;
            AdvancedInventory_Single_TableLayout_Right.ColumnStyles.Add(new ColumnStyle());
            AdvancedInventory_Single_TableLayout_Right.Controls.Add(AdvancedInventory_Single_TableLayout_LowerRight, 0, 1);
            AdvancedInventory_Single_TableLayout_Right.Controls.Add(AdvancedInventory_Single_Panel_Preview, 0, 0);
            AdvancedInventory_Single_TableLayout_Right.Dock = DockStyle.Fill;
            AdvancedInventory_Single_TableLayout_Right.Location = new Point(3, 19);
            AdvancedInventory_Single_TableLayout_Right.Name = "AdvancedInventory_Single_TableLayout_Right";
            AdvancedInventory_Single_TableLayout_Right.RowCount = 2;
            AdvancedInventory_Single_TableLayout_Right.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            AdvancedInventory_Single_TableLayout_Right.RowStyles.Add(new RowStyle());
            AdvancedInventory_Single_TableLayout_Right.Size = new Size(471, 307);
            AdvancedInventory_Single_TableLayout_Right.TabIndex = 15;
            // 
            // AdvancedInventory_Single_TableLayout_LowerRight
            // 
            AdvancedInventory_Single_TableLayout_LowerRight.AutoSize = true;
            AdvancedInventory_Single_TableLayout_LowerRight.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AdvancedInventory_Single_TableLayout_LowerRight.ColumnCount = 6;
            AdvancedInventory_Single_TableLayout_LowerRight.ColumnStyles.Add(new ColumnStyle());
            AdvancedInventory_Single_TableLayout_LowerRight.ColumnStyles.Add(new ColumnStyle());
            AdvancedInventory_Single_TableLayout_LowerRight.ColumnStyles.Add(new ColumnStyle());
            AdvancedInventory_Single_TableLayout_LowerRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            AdvancedInventory_Single_TableLayout_LowerRight.ColumnStyles.Add(new ColumnStyle());
            AdvancedInventory_Single_TableLayout_LowerRight.ColumnStyles.Add(new ColumnStyle());
            AdvancedInventory_Single_TableLayout_LowerRight.Controls.Add(AdvancedInventory_Single_Button_InputToggle, 0, 0);
            AdvancedInventory_Single_TableLayout_LowerRight.Controls.Add(AdvancedInventory_Single_Button_Normal, 4, 0);
            AdvancedInventory_Single_TableLayout_LowerRight.Controls.Add(AdvancedInventory_Single_Button_Reset, 2, 0);
            AdvancedInventory_Single_TableLayout_LowerRight.Controls.Add(AdvancedInventory_Single_Button_Save, 1, 0);
            AdvancedInventory_Single_TableLayout_LowerRight.Controls.Add(AdvancedInventory_Single_Button_QuickButtonToggle, 5, 0);
            AdvancedInventory_Single_TableLayout_LowerRight.Dock = DockStyle.Fill;
            AdvancedInventory_Single_TableLayout_LowerRight.Location = new Point(0, 269);
            AdvancedInventory_Single_TableLayout_LowerRight.Margin = new Padding(0);
            AdvancedInventory_Single_TableLayout_LowerRight.Name = "AdvancedInventory_Single_TableLayout_LowerRight";
            AdvancedInventory_Single_TableLayout_LowerRight.RowCount = 1;
            AdvancedInventory_Single_TableLayout_LowerRight.RowStyles.Add(new RowStyle());
            AdvancedInventory_Single_TableLayout_LowerRight.Size = new Size(471, 38);
            AdvancedInventory_Single_TableLayout_LowerRight.TabIndex = 1;
            // 
            // AdvancedInventory_Single_Button_InputToggle
            // 
            AdvancedInventory_Single_Button_InputToggle.Location = new Point(3, 3);
            AdvancedInventory_Single_Button_InputToggle.MaximumSize = new Size(32, 32);
            AdvancedInventory_Single_Button_InputToggle.MinimumSize = new Size(32, 32);
            AdvancedInventory_Single_Button_InputToggle.Name = "AdvancedInventory_Single_Button_InputToggle";
            AdvancedInventory_Single_Button_InputToggle.Size = new Size(32, 32);
            AdvancedInventory_Single_Button_InputToggle.TabIndex = 15;
            // 
            // AdvancedInventory_Single_Button_Normal
            // 
            AdvancedInventory_Single_Button_Normal.ForeColor = Color.DarkRed;
            AdvancedInventory_Single_Button_Normal.Location = new Point(330, 3);
            AdvancedInventory_Single_Button_Normal.MaximumSize = new Size(100, 32);
            AdvancedInventory_Single_Button_Normal.MinimumSize = new Size(100, 32);
            AdvancedInventory_Single_Button_Normal.Name = "AdvancedInventory_Single_Button_Normal";
            AdvancedInventory_Single_Button_Normal.Size = new Size(100, 32);
            AdvancedInventory_Single_Button_Normal.TabIndex = 14;
            AdvancedInventory_Single_Button_Normal.TabStop = false;
            AdvancedInventory_Single_Button_Normal.Text = "Back to Normal";
            AdvancedInventory_Single_Button_Normal.Click += AdvancedInventory_Button_Normal_Click;
            // 
            // AdvancedInventory_Single_Button_Reset
            // 
            AdvancedInventory_Single_Button_Reset.Location = new Point(147, 3);
            AdvancedInventory_Single_Button_Reset.MaximumSize = new Size(100, 32);
            AdvancedInventory_Single_Button_Reset.MinimumSize = new Size(100, 32);
            AdvancedInventory_Single_Button_Reset.Name = "AdvancedInventory_Single_Button_Reset";
            AdvancedInventory_Single_Button_Reset.Size = new Size(100, 32);
            AdvancedInventory_Single_Button_Reset.TabIndex = 13;
            AdvancedInventory_Single_Button_Reset.TabStop = false;
            AdvancedInventory_Single_Button_Reset.Text = "Reset";
            AdvancedInventory_Single_Button_Reset.Click += AdvancedInventory_Single_Button_Reset_Click;
            // 
            // AdvancedInventory_Single_Button_Save
            // 
            AdvancedInventory_Single_Button_Save.Location = new Point(41, 3);
            AdvancedInventory_Single_Button_Save.MaximumSize = new Size(100, 32);
            AdvancedInventory_Single_Button_Save.MinimumSize = new Size(100, 32);
            AdvancedInventory_Single_Button_Save.Name = "AdvancedInventory_Single_Button_Save";
            AdvancedInventory_Single_Button_Save.Size = new Size(100, 32);
            AdvancedInventory_Single_Button_Save.TabIndex = 7;
            AdvancedInventory_Single_Button_Save.Text = "Save All";
            AdvancedInventory_Single_Button_Save.Click += AdvancedInventory_Single_Button_Save_Click;
            // 
            // AdvancedInventory_Single_Button_QuickButtonToggle
            // 
            AdvancedInventory_Single_Button_QuickButtonToggle.Location = new Point(436, 3);
            AdvancedInventory_Single_Button_QuickButtonToggle.MaximumSize = new Size(32, 32);
            AdvancedInventory_Single_Button_QuickButtonToggle.MinimumSize = new Size(32, 32);
            AdvancedInventory_Single_Button_QuickButtonToggle.Name = "AdvancedInventory_Single_Button_QuickButtonToggle";
            AdvancedInventory_Single_Button_QuickButtonToggle.Size = new Size(32, 32);
            AdvancedInventory_Single_Button_QuickButtonToggle.TabIndex = 16;
            // 
            // AdvancedInventory_Single_Panel_Preview
            // 
            AdvancedInventory_Single_Panel_Preview.AutoSize = true;
            AdvancedInventory_Single_Panel_Preview.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AdvancedInventory_Single_Panel_Preview.Controls.Add(AdvancedInventory_Single_ListView_Preview);
            AdvancedInventory_Single_Panel_Preview.Dock = DockStyle.Fill;
            AdvancedInventory_Single_Panel_Preview.Location = new Point(3, 3);
            AdvancedInventory_Single_Panel_Preview.Name = "AdvancedInventory_Single_Panel_Preview";
            AdvancedInventory_Single_Panel_Preview.Size = new Size(465, 263);
            AdvancedInventory_Single_Panel_Preview.TabIndex = 2;
            // 
            // AdvancedInventory_Single_ListView_Preview
            // 
            AdvancedInventory_Single_ListView_Preview.Alignment = ListViewAlignment.SnapToGrid;
            AdvancedInventory_Single_ListView_Preview.Dock = DockStyle.Fill;
            AdvancedInventory_Single_ListView_Preview.FullRowSelect = true;
            AdvancedInventory_Single_ListView_Preview.GridLines = true;
            AdvancedInventory_Single_ListView_Preview.Location = new Point(0, 0);
            AdvancedInventory_Single_ListView_Preview.Name = "AdvancedInventory_Single_ListView_Preview";
            AdvancedInventory_Single_ListView_Preview.Size = new Size(465, 263);
            AdvancedInventory_Single_ListView_Preview.TabIndex = 1;
            AdvancedInventory_Single_ListView_Preview.UseCompatibleStateImageBehavior = false;
            AdvancedInventory_Single_ListView_Preview.View = View.Details;
            // 
            // AdvancedInventory_Single_GroupBox_Left
            // 
            AdvancedInventory_Single_GroupBox_Left.AutoSize = true;
            AdvancedInventory_Single_GroupBox_Left.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AdvancedInventory_Single_GroupBox_Left.Controls.Add(AdvancedInventory_Single_TableLayout_Left);
            AdvancedInventory_Single_GroupBox_Left.Dock = DockStyle.Fill;
            AdvancedInventory_Single_GroupBox_Left.Location = new Point(0, 0);
            AdvancedInventory_Single_GroupBox_Left.Margin = new Padding(0);
            AdvancedInventory_Single_GroupBox_Left.Name = "AdvancedInventory_Single_GroupBox_Left";
            AdvancedInventory_Single_GroupBox_Left.Size = new Size(239, 329);
            AdvancedInventory_Single_GroupBox_Left.TabIndex = 0;
            AdvancedInventory_Single_GroupBox_Left.TabStop = false;
            AdvancedInventory_Single_GroupBox_Left.Text = "Single Item to 1 Location Multiple Times";
            // 
            // AdvancedInventory_Single_TableLayout_Left
            // 
            AdvancedInventory_Single_TableLayout_Left.AutoSize = true;
            AdvancedInventory_Single_TableLayout_Left.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AdvancedInventory_Single_TableLayout_Left.ColumnCount = 3;
            AdvancedInventory_Single_TableLayout_Left.ColumnStyles.Add(new ColumnStyle());
            AdvancedInventory_Single_TableLayout_Left.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            AdvancedInventory_Single_TableLayout_Left.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            AdvancedInventory_Single_TableLayout_Left.Controls.Add(AdvancedInventory_Single_Button_LocationF4, 2, 3);
            AdvancedInventory_Single_TableLayout_Left.Controls.Add(AdvancedInventory_Single_Button_OperationF4, 2, 1);
            AdvancedInventory_Single_TableLayout_Left.Controls.Add(AdvancedInventory_Single_Button_PartF4, 2, 0);
            AdvancedInventory_Single_TableLayout_Left.Controls.Add(AdvancedInventory_Single_Label_Part, 0, 0);
            AdvancedInventory_Single_TableLayout_Left.Controls.Add(AdvancedInventory_Single_Button_Send, 1, 6);
            AdvancedInventory_Single_TableLayout_Left.Controls.Add(AdvancedInventory_Single_TextBox_Part, 1, 0);
            AdvancedInventory_Single_TableLayout_Left.Controls.Add(AdvancedInventory_Single_Label_Op, 0, 1);
            AdvancedInventory_Single_TableLayout_Left.Controls.Add(AdvancedInventory_Single_Label_Qty, 0, 2);
            AdvancedInventory_Single_TableLayout_Left.Controls.Add(AdvancedInventory_Single_Label_Loc, 0, 3);
            AdvancedInventory_Single_TableLayout_Left.Controls.Add(AdvancedInventory_Single_TextBox_Op, 1, 1);
            AdvancedInventory_Single_TableLayout_Left.Controls.Add(AdvancedInventory_Single_Label_Count, 0, 4);
            AdvancedInventory_Single_TableLayout_Left.Controls.Add(AdvancedInventory_Single_TextBox_Qty, 1, 2);
            AdvancedInventory_Single_TableLayout_Left.Controls.Add(AdvancedInventory_Single_TextBox_Loc, 1, 3);
            AdvancedInventory_Single_TableLayout_Left.Controls.Add(AdvancedInventory_Single_Label_Notes, 0, 5);
            AdvancedInventory_Single_TableLayout_Left.Controls.Add(AdvancedInventory_Single_TextBox_Count, 1, 4);
            AdvancedInventory_Single_TableLayout_Left.Controls.Add(panel4, 1, 5);
            AdvancedInventory_Single_TableLayout_Left.Dock = DockStyle.Fill;
            AdvancedInventory_Single_TableLayout_Left.Location = new Point(3, 19);
            AdvancedInventory_Single_TableLayout_Left.Name = "AdvancedInventory_Single_TableLayout_Left";
            AdvancedInventory_Single_TableLayout_Left.RowCount = 6;
            AdvancedInventory_Single_TableLayout_Left.RowStyles.Add(new RowStyle());
            AdvancedInventory_Single_TableLayout_Left.RowStyles.Add(new RowStyle());
            AdvancedInventory_Single_TableLayout_Left.RowStyles.Add(new RowStyle());
            AdvancedInventory_Single_TableLayout_Left.RowStyles.Add(new RowStyle());
            AdvancedInventory_Single_TableLayout_Left.RowStyles.Add(new RowStyle());
            AdvancedInventory_Single_TableLayout_Left.RowStyles.Add(new RowStyle());
            AdvancedInventory_Single_TableLayout_Left.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            AdvancedInventory_Single_TableLayout_Left.RowStyles.Add(new RowStyle());
            AdvancedInventory_Single_TableLayout_Left.Size = new Size(233, 307);
            AdvancedInventory_Single_TableLayout_Left.TabIndex = 0;
            // 
            // AdvancedInventory_Single_Button_LocationF4
            // 
            AdvancedInventory_Single_Button_LocationF4.Location = new Point(206, 90);
            AdvancedInventory_Single_Button_LocationF4.Name = "AdvancedInventory_Single_Button_LocationF4";
            AdvancedInventory_Single_Button_LocationF4.Size = new Size(23, 23);
            AdvancedInventory_Single_Button_LocationF4.TabIndex = 15;
            AdvancedInventory_Single_Button_LocationF4.Text = "🔎";
            AdvancedInventory_Single_Button_LocationF4.UseVisualStyleBackColor = true;
            // 
            // AdvancedInventory_Single_Button_OperationF4
            // 
            AdvancedInventory_Single_Button_OperationF4.Location = new Point(206, 32);
            AdvancedInventory_Single_Button_OperationF4.Name = "AdvancedInventory_Single_Button_OperationF4";
            AdvancedInventory_Single_Button_OperationF4.Size = new Size(23, 23);
            AdvancedInventory_Single_Button_OperationF4.TabIndex = 13;
            AdvancedInventory_Single_Button_OperationF4.Text = "🔎";
            AdvancedInventory_Single_Button_OperationF4.UseVisualStyleBackColor = true;
            // 
            // AdvancedInventory_Single_Button_PartF4
            // 
            AdvancedInventory_Single_Button_PartF4.Location = new Point(206, 3);
            AdvancedInventory_Single_Button_PartF4.Name = "AdvancedInventory_Single_Button_PartF4";
            AdvancedInventory_Single_Button_PartF4.Size = new Size(23, 23);
            AdvancedInventory_Single_Button_PartF4.TabIndex = 12;
            AdvancedInventory_Single_Button_PartF4.Text = "🔎";
            AdvancedInventory_Single_Button_PartF4.UseVisualStyleBackColor = true;
            // 
            // AdvancedInventory_Single_Label_Part
            // 
            AdvancedInventory_Single_Label_Part.AutoEllipsis = true;
            AdvancedInventory_Single_Label_Part.AutoSize = true;
            AdvancedInventory_Single_Label_Part.Dock = DockStyle.Fill;
            AdvancedInventory_Single_Label_Part.Location = new Point(3, 3);
            AdvancedInventory_Single_Label_Part.Margin = new Padding(3);
            AdvancedInventory_Single_Label_Part.Name = "AdvancedInventory_Single_Label_Part";
            AdvancedInventory_Single_Label_Part.Size = new Size(71, 23);
            AdvancedInventory_Single_Label_Part.TabIndex = 0;
            AdvancedInventory_Single_Label_Part.Text = "Part:";
            AdvancedInventory_Single_Label_Part.TextAlign = ContentAlignment.MiddleRight;
            // 
            // AdvancedInventory_Single_Button_Send
            // 
            AdvancedInventory_Single_Button_Send.AutoSize = true;
            AdvancedInventory_Single_Button_Send.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AdvancedInventory_Single_TableLayout_Left.SetColumnSpan(AdvancedInventory_Single_Button_Send, 3);
            AdvancedInventory_Single_Button_Send.Dock = DockStyle.Fill;
            AdvancedInventory_Single_Button_Send.Location = new Point(3, 272);
            AdvancedInventory_Single_Button_Send.MaximumSize = new Size(0, 32);
            AdvancedInventory_Single_Button_Send.MinimumSize = new Size(0, 32);
            AdvancedInventory_Single_Button_Send.Name = "AdvancedInventory_Single_Button_Send";
            AdvancedInventory_Single_Button_Send.Size = new Size(227, 32);
            AdvancedInventory_Single_Button_Send.TabIndex = 1;
            AdvancedInventory_Single_Button_Send.TabStop = false;
            AdvancedInventory_Single_Button_Send.Text = "Send to Preview";
            AdvancedInventory_Single_Button_Send.Click += AdvancedInventory_Single_Button_Send_Click;
            // 
            // AdvancedInventory_Single_TextBox_Part
            // 
            AdvancedInventory_Single_TextBox_Part.AutoSize = true;
            AdvancedInventory_Single_TextBox_Part.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AdvancedInventory_Single_TextBox_Part.Dock = DockStyle.Fill;
            AdvancedInventory_Single_TextBox_Part.Location = new Point(77, 0);
            AdvancedInventory_Single_TextBox_Part.Margin = new Padding(0);
            AdvancedInventory_Single_TextBox_Part.MaximumSize = new Size(150, 0);
            AdvancedInventory_Single_TextBox_Part.MinimumSize = new Size(120, 27);
            AdvancedInventory_Single_TextBox_Part.Name = "AdvancedInventory_Single_TextBox_Part";
            AdvancedInventory_Single_TextBox_Part.PlaceholderText = "Enter Part Number";
            AdvancedInventory_Single_TextBox_Part.Size = new Size(126, 29);
            AdvancedInventory_Single_TextBox_Part.TabIndex = 1;
            // 
            // AdvancedInventory_Single_Label_Op
            // 
            AdvancedInventory_Single_Label_Op.AutoEllipsis = true;
            AdvancedInventory_Single_Label_Op.AutoSize = true;
            AdvancedInventory_Single_Label_Op.Dock = DockStyle.Fill;
            AdvancedInventory_Single_Label_Op.Location = new Point(3, 32);
            AdvancedInventory_Single_Label_Op.Margin = new Padding(3);
            AdvancedInventory_Single_Label_Op.Name = "AdvancedInventory_Single_Label_Op";
            AdvancedInventory_Single_Label_Op.Size = new Size(71, 23);
            AdvancedInventory_Single_Label_Op.TabIndex = 2;
            AdvancedInventory_Single_Label_Op.Text = "Op:";
            AdvancedInventory_Single_Label_Op.TextAlign = ContentAlignment.MiddleRight;
            // 
            // AdvancedInventory_Single_Label_Qty
            // 
            AdvancedInventory_Single_Label_Qty.AutoEllipsis = true;
            AdvancedInventory_Single_Label_Qty.AutoSize = true;
            AdvancedInventory_Single_Label_Qty.Dock = DockStyle.Fill;
            AdvancedInventory_Single_Label_Qty.Location = new Point(3, 61);
            AdvancedInventory_Single_Label_Qty.Margin = new Padding(3);
            AdvancedInventory_Single_Label_Qty.Name = "AdvancedInventory_Single_Label_Qty";
            AdvancedInventory_Single_Label_Qty.Size = new Size(71, 23);
            AdvancedInventory_Single_Label_Qty.TabIndex = 6;
            AdvancedInventory_Single_Label_Qty.Text = "Quantity:";
            AdvancedInventory_Single_Label_Qty.TextAlign = ContentAlignment.MiddleRight;
            // 
            // AdvancedInventory_Single_Label_Loc
            // 
            AdvancedInventory_Single_Label_Loc.AutoEllipsis = true;
            AdvancedInventory_Single_Label_Loc.AutoSize = true;
            AdvancedInventory_Single_Label_Loc.Dock = DockStyle.Fill;
            AdvancedInventory_Single_Label_Loc.Location = new Point(3, 90);
            AdvancedInventory_Single_Label_Loc.Margin = new Padding(3);
            AdvancedInventory_Single_Label_Loc.Name = "AdvancedInventory_Single_Label_Loc";
            AdvancedInventory_Single_Label_Loc.Size = new Size(71, 23);
            AdvancedInventory_Single_Label_Loc.TabIndex = 4;
            AdvancedInventory_Single_Label_Loc.Text = "Location:";
            AdvancedInventory_Single_Label_Loc.TextAlign = ContentAlignment.MiddleRight;
            // 
            // AdvancedInventory_Single_TextBox_Op
            // 
            AdvancedInventory_Single_TextBox_Op.AutoSize = true;
            AdvancedInventory_Single_TextBox_Op.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AdvancedInventory_Single_TextBox_Op.Dock = DockStyle.Fill;
            AdvancedInventory_Single_TextBox_Op.Location = new Point(77, 29);
            AdvancedInventory_Single_TextBox_Op.Margin = new Padding(0);
            AdvancedInventory_Single_TextBox_Op.MaximumSize = new Size(150, 0);
            AdvancedInventory_Single_TextBox_Op.MinimumSize = new Size(120, 27);
            AdvancedInventory_Single_TextBox_Op.Name = "AdvancedInventory_Single_TextBox_Op";
            AdvancedInventory_Single_TextBox_Op.PlaceholderText = "Enter Operation";
            AdvancedInventory_Single_TextBox_Op.Size = new Size(126, 29);
            AdvancedInventory_Single_TextBox_Op.TabIndex = 2;
            // 
            // AdvancedInventory_Single_Label_Count
            // 
            AdvancedInventory_Single_Label_Count.AutoEllipsis = true;
            AdvancedInventory_Single_Label_Count.AutoSize = true;
            AdvancedInventory_Single_Label_Count.Dock = DockStyle.Fill;
            AdvancedInventory_Single_Label_Count.Location = new Point(3, 119);
            AdvancedInventory_Single_Label_Count.Margin = new Padding(3);
            AdvancedInventory_Single_Label_Count.Name = "AdvancedInventory_Single_Label_Count";
            AdvancedInventory_Single_Label_Count.Size = new Size(71, 23);
            AdvancedInventory_Single_Label_Count.TabIndex = 8;
            AdvancedInventory_Single_Label_Count.Text = "How Many: ";
            AdvancedInventory_Single_Label_Count.TextAlign = ContentAlignment.MiddleRight;
            // 
            // AdvancedInventory_Single_TextBox_Qty
            // 
            AdvancedInventory_Single_TableLayout_Left.SetColumnSpan(AdvancedInventory_Single_TextBox_Qty, 2);
            AdvancedInventory_Single_TextBox_Qty.Dock = DockStyle.Fill;
            AdvancedInventory_Single_TextBox_Qty.Location = new Point(80, 61);
            AdvancedInventory_Single_TextBox_Qty.MaximumSize = new Size(150, 0);
            AdvancedInventory_Single_TextBox_Qty.Name = "AdvancedInventory_Single_TextBox_Qty";
            AdvancedInventory_Single_TextBox_Qty.PlaceholderText = "Enter Quantity";
            AdvancedInventory_Single_TextBox_Qty.Size = new Size(150, 23);
            AdvancedInventory_Single_TextBox_Qty.TabIndex = 3;
            // 
            // AdvancedInventory_Single_TextBox_Loc
            // 
            AdvancedInventory_Single_TextBox_Loc.AutoSize = true;
            AdvancedInventory_Single_TextBox_Loc.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AdvancedInventory_Single_TextBox_Loc.Dock = DockStyle.Fill;
            AdvancedInventory_Single_TextBox_Loc.Location = new Point(77, 87);
            AdvancedInventory_Single_TextBox_Loc.Margin = new Padding(0);
            AdvancedInventory_Single_TextBox_Loc.MaximumSize = new Size(150, 0);
            AdvancedInventory_Single_TextBox_Loc.MinimumSize = new Size(120, 27);
            AdvancedInventory_Single_TextBox_Loc.Name = "AdvancedInventory_Single_TextBox_Loc";
            AdvancedInventory_Single_TextBox_Loc.PlaceholderText = "Enter Location";
            AdvancedInventory_Single_TextBox_Loc.Size = new Size(126, 29);
            AdvancedInventory_Single_TextBox_Loc.TabIndex = 4;
            // 
            // AdvancedInventory_Single_Label_Notes
            // 
            AdvancedInventory_Single_Label_Notes.AutoSize = true;
            AdvancedInventory_Single_TableLayout_Left.SetColumnSpan(AdvancedInventory_Single_Label_Notes, 3);
            AdvancedInventory_Single_Label_Notes.Dock = DockStyle.Fill;
            AdvancedInventory_Single_Label_Notes.Location = new Point(3, 148);
            AdvancedInventory_Single_Label_Notes.Margin = new Padding(3);
            AdvancedInventory_Single_Label_Notes.Name = "AdvancedInventory_Single_Label_Notes";
            AdvancedInventory_Single_Label_Notes.Size = new Size(227, 15);
            AdvancedInventory_Single_Label_Notes.TabIndex = 10;
            AdvancedInventory_Single_Label_Notes.Text = "Notes";
            AdvancedInventory_Single_Label_Notes.TextAlign = ContentAlignment.BottomCenter;
            // 
            // AdvancedInventory_Single_TextBox_Count
            // 
            AdvancedInventory_Single_TableLayout_Left.SetColumnSpan(AdvancedInventory_Single_TextBox_Count, 2);
            AdvancedInventory_Single_TextBox_Count.Dock = DockStyle.Fill;
            AdvancedInventory_Single_TextBox_Count.Location = new Point(80, 119);
            AdvancedInventory_Single_TextBox_Count.MaximumSize = new Size(150, 0);
            AdvancedInventory_Single_TextBox_Count.Name = "AdvancedInventory_Single_TextBox_Count";
            AdvancedInventory_Single_TextBox_Count.PlaceholderText = "# of Transactions";
            AdvancedInventory_Single_TextBox_Count.Size = new Size(150, 23);
            AdvancedInventory_Single_TextBox_Count.TabIndex = 6;
            // 
            // panel4
            // 
            panel4.AutoSize = true;
            AdvancedInventory_Single_TableLayout_Left.SetColumnSpan(panel4, 3);
            panel4.Controls.Add(AdvancedInventory_Single_RichTextBox_Notes);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(3, 169);
            panel4.Name = "panel4";
            panel4.Size = new Size(227, 97);
            panel4.TabIndex = 11;
            // 
            // AdvancedInventory_Single_RichTextBox_Notes
            // 
            AdvancedInventory_Single_RichTextBox_Notes.Dock = DockStyle.Fill;
            AdvancedInventory_Single_RichTextBox_Notes.Location = new Point(0, 0);
            AdvancedInventory_Single_RichTextBox_Notes.Name = "AdvancedInventory_Single_RichTextBox_Notes";
            AdvancedInventory_Single_RichTextBox_Notes.Size = new Size(227, 97);
            AdvancedInventory_Single_RichTextBox_Notes.TabIndex = 5;
            AdvancedInventory_Single_RichTextBox_Notes.Text = "";
            // 
            // AdvancedInventory_TabControl_MultiLoc
            // 
            AdvancedInventory_TabControl_MultiLoc.Controls.Add(AdvancedInventory_TableLayoutPanel_Multi);
            AdvancedInventory_TabControl_MultiLoc.Location = new Point(4, 24);
            AdvancedInventory_TabControl_MultiLoc.Margin = new Padding(0);
            AdvancedInventory_TabControl_MultiLoc.Name = "AdvancedInventory_TabControl_MultiLoc";
            AdvancedInventory_TabControl_MultiLoc.Size = new Size(716, 329);
            AdvancedInventory_TabControl_MultiLoc.TabIndex = 1;
            AdvancedInventory_TabControl_MultiLoc.Text = "Same Item, Multiple Locations";
            // 
            // AdvancedInventory_TableLayoutPanel_Multi
            // 
            AdvancedInventory_TableLayoutPanel_Multi.AutoSize = true;
            AdvancedInventory_TableLayoutPanel_Multi.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AdvancedInventory_TableLayoutPanel_Multi.ColumnCount = 2;
            AdvancedInventory_TableLayoutPanel_Multi.ColumnStyles.Add(new ColumnStyle());
            AdvancedInventory_TableLayoutPanel_Multi.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            AdvancedInventory_TableLayoutPanel_Multi.Controls.Add(AdvancedInventory_MultiLoc_GroupBox_Preview, 1, 0);
            AdvancedInventory_TableLayoutPanel_Multi.Controls.Add(AdvancedInventory_MultiLoc_GroupBox_Item, 0, 0);
            AdvancedInventory_TableLayoutPanel_Multi.Dock = DockStyle.Fill;
            AdvancedInventory_TableLayoutPanel_Multi.Location = new Point(0, 0);
            AdvancedInventory_TableLayoutPanel_Multi.Margin = new Padding(0);
            AdvancedInventory_TableLayoutPanel_Multi.Name = "AdvancedInventory_TableLayoutPanel_Multi";
            AdvancedInventory_TableLayoutPanel_Multi.RowCount = 1;
            AdvancedInventory_TableLayoutPanel_Multi.RowStyles.Add(new RowStyle());
            AdvancedInventory_TableLayoutPanel_Multi.Size = new Size(716, 329);
            AdvancedInventory_TableLayoutPanel_Multi.TabIndex = 1;
            // 
            // AdvancedInventory_MultiLoc_GroupBox_Preview
            // 
            AdvancedInventory_MultiLoc_GroupBox_Preview.AutoSize = true;
            AdvancedInventory_MultiLoc_GroupBox_Preview.Controls.Add(AdvancedInventory_Multi_TableLayout_Right);
            AdvancedInventory_MultiLoc_GroupBox_Preview.Dock = DockStyle.Fill;
            AdvancedInventory_MultiLoc_GroupBox_Preview.Location = new Point(224, 0);
            AdvancedInventory_MultiLoc_GroupBox_Preview.Margin = new Padding(0);
            AdvancedInventory_MultiLoc_GroupBox_Preview.Name = "AdvancedInventory_MultiLoc_GroupBox_Preview";
            AdvancedInventory_MultiLoc_GroupBox_Preview.Size = new Size(492, 329);
            AdvancedInventory_MultiLoc_GroupBox_Preview.TabIndex = 0;
            AdvancedInventory_MultiLoc_GroupBox_Preview.TabStop = false;
            AdvancedInventory_MultiLoc_GroupBox_Preview.Text = "Transaction Preview";
            // 
            // AdvancedInventory_Multi_TableLayout_Right
            // 
            AdvancedInventory_Multi_TableLayout_Right.AutoSize = true;
            AdvancedInventory_Multi_TableLayout_Right.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AdvancedInventory_Multi_TableLayout_Right.ColumnCount = 1;
            AdvancedInventory_Multi_TableLayout_Right.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            AdvancedInventory_Multi_TableLayout_Right.Controls.Add(panel1, 0, 0);
            AdvancedInventory_Multi_TableLayout_Right.Controls.Add(AdvancedInventory_Multi_TableLayout_BottomRight, 0, 1);
            AdvancedInventory_Multi_TableLayout_Right.Dock = DockStyle.Fill;
            AdvancedInventory_Multi_TableLayout_Right.Location = new Point(3, 19);
            AdvancedInventory_Multi_TableLayout_Right.Name = "AdvancedInventory_Multi_TableLayout_Right";
            AdvancedInventory_Multi_TableLayout_Right.RowCount = 2;
            AdvancedInventory_Multi_TableLayout_Right.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            AdvancedInventory_Multi_TableLayout_Right.RowStyles.Add(new RowStyle());
            AdvancedInventory_Multi_TableLayout_Right.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            AdvancedInventory_Multi_TableLayout_Right.Size = new Size(486, 307);
            AdvancedInventory_Multi_TableLayout_Right.TabIndex = 16;
            // 
            // panel1
            // 
            panel1.AutoSize = true;
            panel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel1.Controls.Add(AdvancedInventory_MultiLoc_ListView_Preview);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(480, 263);
            panel1.TabIndex = 1;
            // 
            // AdvancedInventory_MultiLoc_ListView_Preview
            // 
            AdvancedInventory_MultiLoc_ListView_Preview.Alignment = ListViewAlignment.SnapToGrid;
            AdvancedInventory_MultiLoc_ListView_Preview.Dock = DockStyle.Fill;
            AdvancedInventory_MultiLoc_ListView_Preview.FullRowSelect = true;
            AdvancedInventory_MultiLoc_ListView_Preview.GridLines = true;
            AdvancedInventory_MultiLoc_ListView_Preview.Location = new Point(0, 0);
            AdvancedInventory_MultiLoc_ListView_Preview.Name = "AdvancedInventory_MultiLoc_ListView_Preview";
            AdvancedInventory_MultiLoc_ListView_Preview.Size = new Size(480, 263);
            AdvancedInventory_MultiLoc_ListView_Preview.TabIndex = 0;
            AdvancedInventory_MultiLoc_ListView_Preview.UseCompatibleStateImageBehavior = false;
            AdvancedInventory_MultiLoc_ListView_Preview.View = View.Details;
            // 
            // AdvancedInventory_Multi_TableLayout_BottomRight
            // 
            AdvancedInventory_Multi_TableLayout_BottomRight.AutoSize = true;
            AdvancedInventory_Multi_TableLayout_BottomRight.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AdvancedInventory_Multi_TableLayout_BottomRight.ColumnCount = 6;
            AdvancedInventory_Multi_TableLayout_BottomRight.ColumnStyles.Add(new ColumnStyle());
            AdvancedInventory_Multi_TableLayout_BottomRight.ColumnStyles.Add(new ColumnStyle());
            AdvancedInventory_Multi_TableLayout_BottomRight.ColumnStyles.Add(new ColumnStyle());
            AdvancedInventory_Multi_TableLayout_BottomRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            AdvancedInventory_Multi_TableLayout_BottomRight.ColumnStyles.Add(new ColumnStyle());
            AdvancedInventory_Multi_TableLayout_BottomRight.ColumnStyles.Add(new ColumnStyle());
            AdvancedInventory_Multi_TableLayout_BottomRight.Controls.Add(AdvancedInventory_Multi_Button_InputToggle, 0, 0);
            AdvancedInventory_Multi_TableLayout_BottomRight.Controls.Add(AdvancedInventory_Multi_Button_Normal, 4, 0);
            AdvancedInventory_Multi_TableLayout_BottomRight.Controls.Add(AdvancedInventory_MultiLoc_Button_Reset, 2, 0);
            AdvancedInventory_Multi_TableLayout_BottomRight.Controls.Add(AdvancedInventory_MultiLoc_Button_SaveAll, 1, 0);
            AdvancedInventory_Multi_TableLayout_BottomRight.Controls.Add(AdvancedInventory_Multi_Button_QuickButtonToggle, 5, 0);
            AdvancedInventory_Multi_TableLayout_BottomRight.Dock = DockStyle.Fill;
            AdvancedInventory_Multi_TableLayout_BottomRight.Location = new Point(0, 269);
            AdvancedInventory_Multi_TableLayout_BottomRight.Margin = new Padding(0);
            AdvancedInventory_Multi_TableLayout_BottomRight.Name = "AdvancedInventory_Multi_TableLayout_BottomRight";
            AdvancedInventory_Multi_TableLayout_BottomRight.RowCount = 1;
            AdvancedInventory_Multi_TableLayout_BottomRight.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            AdvancedInventory_Multi_TableLayout_BottomRight.Size = new Size(486, 38);
            AdvancedInventory_Multi_TableLayout_BottomRight.TabIndex = 0;
            // 
            // AdvancedInventory_Multi_Button_InputToggle
            // 
            AdvancedInventory_Multi_Button_InputToggle.AutoSize = true;
            AdvancedInventory_Multi_Button_InputToggle.Location = new Point(3, 3);
            AdvancedInventory_Multi_Button_InputToggle.MaximumSize = new Size(32, 32);
            AdvancedInventory_Multi_Button_InputToggle.MinimumSize = new Size(32, 32);
            AdvancedInventory_Multi_Button_InputToggle.Name = "AdvancedInventory_Multi_Button_InputToggle";
            AdvancedInventory_Multi_Button_InputToggle.Size = new Size(32, 32);
            AdvancedInventory_Multi_Button_InputToggle.TabIndex = 16;
            AdvancedInventory_Multi_Button_InputToggle.TabStop = false;
            // 
            // AdvancedInventory_Multi_Button_Normal
            // 
            AdvancedInventory_Multi_Button_Normal.AutoSize = true;
            AdvancedInventory_Multi_Button_Normal.ForeColor = Color.DarkRed;
            AdvancedInventory_Multi_Button_Normal.Location = new Point(345, 3);
            AdvancedInventory_Multi_Button_Normal.Name = "AdvancedInventory_Multi_Button_Normal";
            AdvancedInventory_Multi_Button_Normal.Size = new Size(100, 32);
            AdvancedInventory_Multi_Button_Normal.TabIndex = 15;
            AdvancedInventory_Multi_Button_Normal.TabStop = false;
            AdvancedInventory_Multi_Button_Normal.Text = "Back to Normal";
            AdvancedInventory_Multi_Button_Normal.Click += AdvancedInventory_Button_Normal_Click;
            // 
            // AdvancedInventory_MultiLoc_Button_Reset
            // 
            AdvancedInventory_MultiLoc_Button_Reset.AutoSize = true;
            AdvancedInventory_MultiLoc_Button_Reset.Location = new Point(147, 3);
            AdvancedInventory_MultiLoc_Button_Reset.MaximumSize = new Size(100, 32);
            AdvancedInventory_MultiLoc_Button_Reset.MinimumSize = new Size(100, 32);
            AdvancedInventory_MultiLoc_Button_Reset.Name = "AdvancedInventory_MultiLoc_Button_Reset";
            AdvancedInventory_MultiLoc_Button_Reset.Size = new Size(100, 32);
            AdvancedInventory_MultiLoc_Button_Reset.TabIndex = 2;
            AdvancedInventory_MultiLoc_Button_Reset.TabStop = false;
            AdvancedInventory_MultiLoc_Button_Reset.Text = "Reset";
            AdvancedInventory_MultiLoc_Button_Reset.Click += AdvancedInventory_MultiLoc_Button_Reset_Click;
            // 
            // AdvancedInventory_MultiLoc_Button_SaveAll
            // 
            AdvancedInventory_MultiLoc_Button_SaveAll.AutoSize = true;
            AdvancedInventory_MultiLoc_Button_SaveAll.Location = new Point(41, 3);
            AdvancedInventory_MultiLoc_Button_SaveAll.MaximumSize = new Size(100, 32);
            AdvancedInventory_MultiLoc_Button_SaveAll.MinimumSize = new Size(100, 32);
            AdvancedInventory_MultiLoc_Button_SaveAll.Name = "AdvancedInventory_MultiLoc_Button_SaveAll";
            AdvancedInventory_MultiLoc_Button_SaveAll.Size = new Size(100, 32);
            AdvancedInventory_MultiLoc_Button_SaveAll.TabIndex = 1;
            AdvancedInventory_MultiLoc_Button_SaveAll.TabStop = false;
            AdvancedInventory_MultiLoc_Button_SaveAll.Text = "Save All";
            AdvancedInventory_MultiLoc_Button_SaveAll.Click += AdvancedInventory_MultiLoc_Button_SaveAll_Click;
            // 
            // AdvancedInventory_Multi_Button_QuickButtonToggle
            // 
            AdvancedInventory_Multi_Button_QuickButtonToggle.AutoSize = true;
            AdvancedInventory_Multi_Button_QuickButtonToggle.Location = new Point(451, 3);
            AdvancedInventory_Multi_Button_QuickButtonToggle.MaximumSize = new Size(32, 32);
            AdvancedInventory_Multi_Button_QuickButtonToggle.MinimumSize = new Size(32, 32);
            AdvancedInventory_Multi_Button_QuickButtonToggle.Name = "AdvancedInventory_Multi_Button_QuickButtonToggle";
            AdvancedInventory_Multi_Button_QuickButtonToggle.Size = new Size(32, 32);
            AdvancedInventory_Multi_Button_QuickButtonToggle.TabIndex = 17;
            AdvancedInventory_Multi_Button_QuickButtonToggle.TabStop = false;
            // 
            // AdvancedInventory_MultiLoc_GroupBox_Item
            // 
            AdvancedInventory_MultiLoc_GroupBox_Item.AutoSize = true;
            AdvancedInventory_MultiLoc_GroupBox_Item.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AdvancedInventory_MultiLoc_GroupBox_Item.Controls.Add(AdvancedInventory_Multi_TableLayout_Left);
            AdvancedInventory_MultiLoc_GroupBox_Item.Dock = DockStyle.Fill;
            AdvancedInventory_MultiLoc_GroupBox_Item.Location = new Point(0, 0);
            AdvancedInventory_MultiLoc_GroupBox_Item.Margin = new Padding(0);
            AdvancedInventory_MultiLoc_GroupBox_Item.Name = "AdvancedInventory_MultiLoc_GroupBox_Item";
            AdvancedInventory_MultiLoc_GroupBox_Item.Size = new Size(224, 329);
            AdvancedInventory_MultiLoc_GroupBox_Item.TabIndex = 0;
            AdvancedInventory_MultiLoc_GroupBox_Item.TabStop = false;
            AdvancedInventory_MultiLoc_GroupBox_Item.Text = "Item Entry";
            // 
            // AdvancedInventory_Multi_TableLayout_Left
            // 
            AdvancedInventory_Multi_TableLayout_Left.AutoSize = true;
            AdvancedInventory_Multi_TableLayout_Left.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AdvancedInventory_Multi_TableLayout_Left.ColumnCount = 3;
            AdvancedInventory_Multi_TableLayout_Left.ColumnStyles.Add(new ColumnStyle());
            AdvancedInventory_Multi_TableLayout_Left.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            AdvancedInventory_Multi_TableLayout_Left.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            AdvancedInventory_Multi_TableLayout_Left.Controls.Add(AdvancedInventory_MultiLoc_Button_LocationF4, 2, 3);
            AdvancedInventory_Multi_TableLayout_Left.Controls.Add(AdvancedInventory_MultiLoc_Button_OperationF4, 2, 1);
            AdvancedInventory_Multi_TableLayout_Left.Controls.Add(AdvancedInventory_MultiLoc_Label_Part, 0, 0);
            AdvancedInventory_Multi_TableLayout_Left.Controls.Add(AdvancedInventory_MultiLoc_TextBox_Part, 1, 0);
            AdvancedInventory_Multi_TableLayout_Left.Controls.Add(AdvancedInventory_MultiLoc_Label_Op, 0, 1);
            AdvancedInventory_Multi_TableLayout_Left.Controls.Add(AdvancedInventory_MultiLoc_TextBox_Op, 1, 1);
            AdvancedInventory_Multi_TableLayout_Left.Controls.Add(AdvancedInventory_MultiLoc_Label_Qty, 0, 2);
            AdvancedInventory_Multi_TableLayout_Left.Controls.Add(AdvancedInventory_MultiLoc_Label_Notes, 0, 4);
            AdvancedInventory_Multi_TableLayout_Left.Controls.Add(AdvancedInventory_MultiLoc_TextBox_Qty, 1, 2);
            AdvancedInventory_Multi_TableLayout_Left.Controls.Add(AdvancedInventory_MultiLoc_Label_Loc, 0, 3);
            AdvancedInventory_Multi_TableLayout_Left.Controls.Add(AdvancedInventory_MultiLoc_TextBox_Loc, 1, 3);
            AdvancedInventory_Multi_TableLayout_Left.Controls.Add(panel3, 1, 4);
            AdvancedInventory_Multi_TableLayout_Left.Controls.Add(AdvancedInventory_MultiLoc_Button_PartF4, 2, 0);
            AdvancedInventory_Multi_TableLayout_Left.Controls.Add(AdvancedInventory_MultiLoc_Button_AddLoc, 1, 6);
            AdvancedInventory_Multi_TableLayout_Left.Dock = DockStyle.Fill;
            AdvancedInventory_Multi_TableLayout_Left.Location = new Point(3, 19);
            AdvancedInventory_Multi_TableLayout_Left.Name = "AdvancedInventory_Multi_TableLayout_Left";
            AdvancedInventory_Multi_TableLayout_Left.RowCount = 7;
            AdvancedInventory_Multi_TableLayout_Left.RowStyles.Add(new RowStyle());
            AdvancedInventory_Multi_TableLayout_Left.RowStyles.Add(new RowStyle());
            AdvancedInventory_Multi_TableLayout_Left.RowStyles.Add(new RowStyle());
            AdvancedInventory_Multi_TableLayout_Left.RowStyles.Add(new RowStyle());
            AdvancedInventory_Multi_TableLayout_Left.RowStyles.Add(new RowStyle());
            AdvancedInventory_Multi_TableLayout_Left.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            AdvancedInventory_Multi_TableLayout_Left.RowStyles.Add(new RowStyle());
            AdvancedInventory_Multi_TableLayout_Left.RowStyles.Add(new RowStyle());
            AdvancedInventory_Multi_TableLayout_Left.Size = new Size(218, 307);
            AdvancedInventory_Multi_TableLayout_Left.TabIndex = 1;
            // 
            // AdvancedInventory_MultiLoc_Button_LocationF4
            // 
            AdvancedInventory_MultiLoc_Button_LocationF4.Location = new Point(191, 90);
            AdvancedInventory_MultiLoc_Button_LocationF4.Name = "AdvancedInventory_MultiLoc_Button_LocationF4";
            AdvancedInventory_MultiLoc_Button_LocationF4.Size = new Size(23, 23);
            AdvancedInventory_MultiLoc_Button_LocationF4.TabIndex = 13;
            AdvancedInventory_MultiLoc_Button_LocationF4.Text = "🔎";
            AdvancedInventory_MultiLoc_Button_LocationF4.UseVisualStyleBackColor = true;
            // 
            // AdvancedInventory_MultiLoc_Button_OperationF4
            // 
            AdvancedInventory_MultiLoc_Button_OperationF4.Location = new Point(191, 32);
            AdvancedInventory_MultiLoc_Button_OperationF4.Name = "AdvancedInventory_MultiLoc_Button_OperationF4";
            AdvancedInventory_MultiLoc_Button_OperationF4.Size = new Size(23, 23);
            AdvancedInventory_MultiLoc_Button_OperationF4.TabIndex = 11;
            AdvancedInventory_MultiLoc_Button_OperationF4.Text = "🔎";
            AdvancedInventory_MultiLoc_Button_OperationF4.UseVisualStyleBackColor = true;
            // 
            // AdvancedInventory_MultiLoc_Label_Part
            // 
            AdvancedInventory_MultiLoc_Label_Part.AutoSize = true;
            AdvancedInventory_MultiLoc_Label_Part.Dock = DockStyle.Fill;
            AdvancedInventory_MultiLoc_Label_Part.Location = new Point(3, 3);
            AdvancedInventory_MultiLoc_Label_Part.Margin = new Padding(3);
            AdvancedInventory_MultiLoc_Label_Part.Name = "AdvancedInventory_MultiLoc_Label_Part";
            AdvancedInventory_MultiLoc_Label_Part.Size = new Size(56, 23);
            AdvancedInventory_MultiLoc_Label_Part.TabIndex = 0;
            AdvancedInventory_MultiLoc_Label_Part.Text = "Part:";
            AdvancedInventory_MultiLoc_Label_Part.TextAlign = ContentAlignment.MiddleRight;
            // 
            // AdvancedInventory_MultiLoc_TextBox_Part
            // 
            AdvancedInventory_MultiLoc_TextBox_Part.AutoSize = true;
            AdvancedInventory_MultiLoc_TextBox_Part.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AdvancedInventory_MultiLoc_TextBox_Part.Dock = DockStyle.Fill;
            AdvancedInventory_MultiLoc_TextBox_Part.Location = new Point(62, 0);
            AdvancedInventory_MultiLoc_TextBox_Part.Margin = new Padding(0);
            AdvancedInventory_MultiLoc_TextBox_Part.MaximumSize = new Size(150, 0);
            AdvancedInventory_MultiLoc_TextBox_Part.MinimumSize = new Size(120, 27);
            AdvancedInventory_MultiLoc_TextBox_Part.Name = "AdvancedInventory_MultiLoc_TextBox_Part";
            AdvancedInventory_MultiLoc_TextBox_Part.PlaceholderText = "Enter Part Number";
            AdvancedInventory_MultiLoc_TextBox_Part.Size = new Size(126, 29);
            AdvancedInventory_MultiLoc_TextBox_Part.TabIndex = 1;
            // 
            // AdvancedInventory_MultiLoc_Label_Op
            // 
            AdvancedInventory_MultiLoc_Label_Op.AutoSize = true;
            AdvancedInventory_MultiLoc_Label_Op.Dock = DockStyle.Fill;
            AdvancedInventory_MultiLoc_Label_Op.Location = new Point(3, 32);
            AdvancedInventory_MultiLoc_Label_Op.Margin = new Padding(3);
            AdvancedInventory_MultiLoc_Label_Op.Name = "AdvancedInventory_MultiLoc_Label_Op";
            AdvancedInventory_MultiLoc_Label_Op.Size = new Size(56, 23);
            AdvancedInventory_MultiLoc_Label_Op.TabIndex = 2;
            AdvancedInventory_MultiLoc_Label_Op.Text = "Op:";
            AdvancedInventory_MultiLoc_Label_Op.TextAlign = ContentAlignment.MiddleRight;
            // 
            // AdvancedInventory_MultiLoc_TextBox_Op
            // 
            AdvancedInventory_MultiLoc_TextBox_Op.AutoSize = true;
            AdvancedInventory_MultiLoc_TextBox_Op.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AdvancedInventory_MultiLoc_TextBox_Op.Dock = DockStyle.Fill;
            AdvancedInventory_MultiLoc_TextBox_Op.Location = new Point(62, 29);
            AdvancedInventory_MultiLoc_TextBox_Op.Margin = new Padding(0);
            AdvancedInventory_MultiLoc_TextBox_Op.MaximumSize = new Size(150, 0);
            AdvancedInventory_MultiLoc_TextBox_Op.MinimumSize = new Size(120, 27);
            AdvancedInventory_MultiLoc_TextBox_Op.Name = "AdvancedInventory_MultiLoc_TextBox_Op";
            AdvancedInventory_MultiLoc_TextBox_Op.PlaceholderText = "Enter Operation";
            AdvancedInventory_MultiLoc_TextBox_Op.Size = new Size(126, 29);
            AdvancedInventory_MultiLoc_TextBox_Op.TabIndex = 2;
            // 
            // AdvancedInventory_MultiLoc_Label_Qty
            // 
            AdvancedInventory_MultiLoc_Label_Qty.AutoSize = true;
            AdvancedInventory_MultiLoc_Label_Qty.Dock = DockStyle.Fill;
            AdvancedInventory_MultiLoc_Label_Qty.Location = new Point(3, 61);
            AdvancedInventory_MultiLoc_Label_Qty.Margin = new Padding(3);
            AdvancedInventory_MultiLoc_Label_Qty.Name = "AdvancedInventory_MultiLoc_Label_Qty";
            AdvancedInventory_MultiLoc_Label_Qty.Size = new Size(56, 23);
            AdvancedInventory_MultiLoc_Label_Qty.TabIndex = 4;
            AdvancedInventory_MultiLoc_Label_Qty.Text = "Quantity:";
            AdvancedInventory_MultiLoc_Label_Qty.TextAlign = ContentAlignment.MiddleRight;
            // 
            // AdvancedInventory_MultiLoc_Label_Notes
            // 
            AdvancedInventory_MultiLoc_Label_Notes.AutoSize = true;
            AdvancedInventory_Multi_TableLayout_Left.SetColumnSpan(AdvancedInventory_MultiLoc_Label_Notes, 3);
            AdvancedInventory_MultiLoc_Label_Notes.Dock = DockStyle.Fill;
            AdvancedInventory_MultiLoc_Label_Notes.Location = new Point(3, 119);
            AdvancedInventory_MultiLoc_Label_Notes.Margin = new Padding(3);
            AdvancedInventory_MultiLoc_Label_Notes.Name = "AdvancedInventory_MultiLoc_Label_Notes";
            AdvancedInventory_MultiLoc_Label_Notes.Size = new Size(212, 15);
            AdvancedInventory_MultiLoc_Label_Notes.TabIndex = 6;
            AdvancedInventory_MultiLoc_Label_Notes.Text = "Notes";
            AdvancedInventory_MultiLoc_Label_Notes.TextAlign = ContentAlignment.BottomCenter;
            // 
            // AdvancedInventory_MultiLoc_TextBox_Qty
            // 
            AdvancedInventory_Multi_TableLayout_Left.SetColumnSpan(AdvancedInventory_MultiLoc_TextBox_Qty, 2);
            AdvancedInventory_MultiLoc_TextBox_Qty.Dock = DockStyle.Fill;
            AdvancedInventory_MultiLoc_TextBox_Qty.Location = new Point(65, 61);
            AdvancedInventory_MultiLoc_TextBox_Qty.MaximumSize = new Size(150, 0);
            AdvancedInventory_MultiLoc_TextBox_Qty.Name = "AdvancedInventory_MultiLoc_TextBox_Qty";
            AdvancedInventory_MultiLoc_TextBox_Qty.PlaceholderText = "Enter Quantity";
            AdvancedInventory_MultiLoc_TextBox_Qty.Size = new Size(150, 23);
            AdvancedInventory_MultiLoc_TextBox_Qty.TabIndex = 3;
            // 
            // AdvancedInventory_MultiLoc_Label_Loc
            // 
            AdvancedInventory_MultiLoc_Label_Loc.AutoSize = true;
            AdvancedInventory_MultiLoc_Label_Loc.Dock = DockStyle.Fill;
            AdvancedInventory_MultiLoc_Label_Loc.Location = new Point(3, 90);
            AdvancedInventory_MultiLoc_Label_Loc.Margin = new Padding(3);
            AdvancedInventory_MultiLoc_Label_Loc.Name = "AdvancedInventory_MultiLoc_Label_Loc";
            AdvancedInventory_MultiLoc_Label_Loc.Size = new Size(56, 23);
            AdvancedInventory_MultiLoc_Label_Loc.TabIndex = 8;
            AdvancedInventory_MultiLoc_Label_Loc.Text = "Location:";
            AdvancedInventory_MultiLoc_Label_Loc.TextAlign = ContentAlignment.MiddleRight;
            // 
            // AdvancedInventory_MultiLoc_TextBox_Loc
            // 
            AdvancedInventory_MultiLoc_TextBox_Loc.AutoSize = true;
            AdvancedInventory_MultiLoc_TextBox_Loc.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AdvancedInventory_MultiLoc_TextBox_Loc.Dock = DockStyle.Fill;
            AdvancedInventory_MultiLoc_TextBox_Loc.Location = new Point(62, 87);
            AdvancedInventory_MultiLoc_TextBox_Loc.Margin = new Padding(0);
            AdvancedInventory_MultiLoc_TextBox_Loc.MaximumSize = new Size(150, 0);
            AdvancedInventory_MultiLoc_TextBox_Loc.MinimumSize = new Size(120, 27);
            AdvancedInventory_MultiLoc_TextBox_Loc.Name = "AdvancedInventory_MultiLoc_TextBox_Loc";
            AdvancedInventory_MultiLoc_TextBox_Loc.PlaceholderText = "Enter Location";
            AdvancedInventory_MultiLoc_TextBox_Loc.Size = new Size(126, 29);
            AdvancedInventory_MultiLoc_TextBox_Loc.TabIndex = 4;
            // 
            // panel3
            // 
            panel3.AutoSize = true;
            panel3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AdvancedInventory_Multi_TableLayout_Left.SetColumnSpan(panel3, 3);
            panel3.Controls.Add(AdvancedInventory_MultiLoc_RichTextBox_Notes);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(3, 140);
            panel3.Name = "panel3";
            panel3.Size = new Size(212, 126);
            panel3.TabIndex = 9;
            // 
            // AdvancedInventory_MultiLoc_RichTextBox_Notes
            // 
            AdvancedInventory_MultiLoc_RichTextBox_Notes.Dock = DockStyle.Fill;
            AdvancedInventory_MultiLoc_RichTextBox_Notes.Location = new Point(0, 0);
            AdvancedInventory_MultiLoc_RichTextBox_Notes.Name = "AdvancedInventory_MultiLoc_RichTextBox_Notes";
            AdvancedInventory_MultiLoc_RichTextBox_Notes.Size = new Size(212, 126);
            AdvancedInventory_MultiLoc_RichTextBox_Notes.TabIndex = 5;
            AdvancedInventory_MultiLoc_RichTextBox_Notes.Text = "";
            // 
            // AdvancedInventory_MultiLoc_Button_PartF4
            // 
            AdvancedInventory_MultiLoc_Button_PartF4.Location = new Point(191, 3);
            AdvancedInventory_MultiLoc_Button_PartF4.Name = "AdvancedInventory_MultiLoc_Button_PartF4";
            AdvancedInventory_MultiLoc_Button_PartF4.Size = new Size(23, 23);
            AdvancedInventory_MultiLoc_Button_PartF4.TabIndex = 10;
            AdvancedInventory_MultiLoc_Button_PartF4.Text = "🔎";
            AdvancedInventory_MultiLoc_Button_PartF4.UseVisualStyleBackColor = true;
            // 
            // AdvancedInventory_MultiLoc_Button_AddLoc
            // 
            AdvancedInventory_MultiLoc_Button_AddLoc.AutoSize = true;
            AdvancedInventory_MultiLoc_Button_AddLoc.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AdvancedInventory_Multi_TableLayout_Left.SetColumnSpan(AdvancedInventory_MultiLoc_Button_AddLoc, 3);
            AdvancedInventory_MultiLoc_Button_AddLoc.Dock = DockStyle.Fill;
            AdvancedInventory_MultiLoc_Button_AddLoc.Location = new Point(3, 272);
            AdvancedInventory_MultiLoc_Button_AddLoc.MaximumSize = new Size(0, 32);
            AdvancedInventory_MultiLoc_Button_AddLoc.MinimumSize = new Size(0, 32);
            AdvancedInventory_MultiLoc_Button_AddLoc.Name = "AdvancedInventory_MultiLoc_Button_AddLoc";
            AdvancedInventory_MultiLoc_Button_AddLoc.Size = new Size(212, 32);
            AdvancedInventory_MultiLoc_Button_AddLoc.TabIndex = 6;
            AdvancedInventory_MultiLoc_Button_AddLoc.Text = "Add Location";
            AdvancedInventory_MultiLoc_Button_AddLoc.Click += AdvancedInventory_MultiLoc_Button_AddLoc_Click;
            // 
            // AdvancedInventory_TabControl_Import
            // 
            AdvancedInventory_TabControl_Import.Controls.Add(AdvancedInventory_Import_TableLayout);
            AdvancedInventory_TabControl_Import.Location = new Point(4, 24);
            AdvancedInventory_TabControl_Import.Name = "AdvancedInventory_TabControl_Import";
            AdvancedInventory_TabControl_Import.Size = new Size(716, 329);
            AdvancedInventory_TabControl_Import.TabIndex = 2;
            AdvancedInventory_TabControl_Import.Text = "Import";
            // 
            // AdvancedInventory_Import_TableLayout
            // 
            AdvancedInventory_Import_TableLayout.AutoSize = true;
            AdvancedInventory_Import_TableLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AdvancedInventory_Import_TableLayout.ColumnCount = 1;
            AdvancedInventory_Import_TableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            AdvancedInventory_Import_TableLayout.Controls.Add(AdvancedInventory_Import_Panel_Middle, 0, 1);
            AdvancedInventory_Import_TableLayout.Controls.Add(AdvancedInventory_Import_TableLayout_Bottom, 0, 2);
            AdvancedInventory_Import_TableLayout.Controls.Add(AdvancedInventory_Import_TableLayout_Top, 0, 0);
            AdvancedInventory_Import_TableLayout.Dock = DockStyle.Fill;
            AdvancedInventory_Import_TableLayout.Location = new Point(0, 0);
            AdvancedInventory_Import_TableLayout.Name = "AdvancedInventory_Import_TableLayout";
            AdvancedInventory_Import_TableLayout.RowCount = 3;
            AdvancedInventory_Import_TableLayout.RowStyles.Add(new RowStyle());
            AdvancedInventory_Import_TableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            AdvancedInventory_Import_TableLayout.RowStyles.Add(new RowStyle());
            AdvancedInventory_Import_TableLayout.Size = new Size(716, 329);
            AdvancedInventory_Import_TableLayout.TabIndex = 6;
            // 
            // AdvancedInventory_Import_Panel_Middle
            // 
            AdvancedInventory_Import_Panel_Middle.AutoSize = true;
            AdvancedInventory_Import_Panel_Middle.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AdvancedInventory_Import_Panel_Middle.Controls.Add(AdvancedInventory_Import_DataGridView);
            AdvancedInventory_Import_Panel_Middle.Dock = DockStyle.Fill;
            AdvancedInventory_Import_Panel_Middle.Location = new Point(3, 47);
            AdvancedInventory_Import_Panel_Middle.Name = "AdvancedInventory_Import_Panel_Middle";
            AdvancedInventory_Import_Panel_Middle.Size = new Size(710, 235);
            AdvancedInventory_Import_Panel_Middle.TabIndex = 2;
            // 
            // AdvancedInventory_Import_DataGridView
            // 
            AdvancedInventory_Import_DataGridView.Dock = DockStyle.Fill;
            AdvancedInventory_Import_DataGridView.Location = new Point(0, 0);
            AdvancedInventory_Import_DataGridView.Name = "AdvancedInventory_Import_DataGridView";
            AdvancedInventory_Import_DataGridView.Size = new Size(710, 235);
            AdvancedInventory_Import_DataGridView.TabIndex = 0;
            // 
            // AdvancedInventory_Import_TableLayout_Bottom
            // 
            AdvancedInventory_Import_TableLayout_Bottom.AutoSize = true;
            AdvancedInventory_Import_TableLayout_Bottom.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AdvancedInventory_Import_TableLayout_Bottom.ColumnCount = 5;
            AdvancedInventory_Import_TableLayout_Bottom.ColumnStyles.Add(new ColumnStyle());
            AdvancedInventory_Import_TableLayout_Bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            AdvancedInventory_Import_TableLayout_Bottom.ColumnStyles.Add(new ColumnStyle());
            AdvancedInventory_Import_TableLayout_Bottom.ColumnStyles.Add(new ColumnStyle());
            AdvancedInventory_Import_TableLayout_Bottom.ColumnStyles.Add(new ColumnStyle());
            AdvancedInventory_Import_TableLayout_Bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            AdvancedInventory_Import_TableLayout_Bottom.Controls.Add(AdvancedInventory_Import_Button_Save, 0, 0);
            AdvancedInventory_Import_TableLayout_Bottom.Controls.Add(AdvancedInventory_Import_Button_CleanSheet, 2, 0);
            AdvancedInventory_Import_TableLayout_Bottom.Controls.Add(AdvancedInventory_Import_Button_Normal, 3, 0);
            AdvancedInventory_Import_TableLayout_Bottom.Controls.Add(AdvancedInventory_Import_Button_QuickButtonToggle, 4, 0);
            AdvancedInventory_Import_TableLayout_Bottom.Dock = DockStyle.Fill;
            AdvancedInventory_Import_TableLayout_Bottom.Location = new Point(3, 288);
            AdvancedInventory_Import_TableLayout_Bottom.Name = "AdvancedInventory_Import_TableLayout_Bottom";
            AdvancedInventory_Import_TableLayout_Bottom.RowCount = 1;
            AdvancedInventory_Import_TableLayout_Bottom.RowStyles.Add(new RowStyle());
            AdvancedInventory_Import_TableLayout_Bottom.Size = new Size(710, 38);
            AdvancedInventory_Import_TableLayout_Bottom.TabIndex = 3;
            // 
            // AdvancedInventory_Import_Button_Save
            // 
            AdvancedInventory_Import_Button_Save.Dock = DockStyle.Fill;
            AdvancedInventory_Import_Button_Save.Location = new Point(3, 3);
            AdvancedInventory_Import_Button_Save.MaximumSize = new Size(100, 32);
            AdvancedInventory_Import_Button_Save.MinimumSize = new Size(100, 32);
            AdvancedInventory_Import_Button_Save.Name = "AdvancedInventory_Import_Button_Save";
            AdvancedInventory_Import_Button_Save.Size = new Size(100, 32);
            AdvancedInventory_Import_Button_Save.TabIndex = 4;
            AdvancedInventory_Import_Button_Save.Text = "Save";
            AdvancedInventory_Import_Button_Save.Click += AdvancedInventory_Import_Button_Save_Click;
            // 
            // AdvancedInventory_Import_Button_CleanSheet
            // 
            AdvancedInventory_Import_Button_CleanSheet.Dock = DockStyle.Fill;
            AdvancedInventory_Import_Button_CleanSheet.ForeColor = Color.DarkRed;
            AdvancedInventory_Import_Button_CleanSheet.Location = new Point(463, 3);
            AdvancedInventory_Import_Button_CleanSheet.MaximumSize = new Size(100, 32);
            AdvancedInventory_Import_Button_CleanSheet.MinimumSize = new Size(100, 32);
            AdvancedInventory_Import_Button_CleanSheet.Name = "AdvancedInventory_Import_Button_CleanSheet";
            AdvancedInventory_Import_Button_CleanSheet.Size = new Size(100, 32);
            AdvancedInventory_Import_Button_CleanSheet.TabIndex = 16;
            AdvancedInventory_Import_Button_CleanSheet.TabStop = false;
            AdvancedInventory_Import_Button_CleanSheet.Text = "Clean Excel File";
            // 
            // AdvancedInventory_Import_Button_Normal
            // 
            AdvancedInventory_Import_Button_Normal.Dock = DockStyle.Fill;
            AdvancedInventory_Import_Button_Normal.ForeColor = SystemColors.ControlText;
            AdvancedInventory_Import_Button_Normal.Location = new Point(569, 3);
            AdvancedInventory_Import_Button_Normal.MaximumSize = new Size(100, 32);
            AdvancedInventory_Import_Button_Normal.MinimumSize = new Size(100, 32);
            AdvancedInventory_Import_Button_Normal.Name = "AdvancedInventory_Import_Button_Normal";
            AdvancedInventory_Import_Button_Normal.Size = new Size(100, 32);
            AdvancedInventory_Import_Button_Normal.TabIndex = 15;
            AdvancedInventory_Import_Button_Normal.TabStop = false;
            AdvancedInventory_Import_Button_Normal.Text = "Back to Normal";
            AdvancedInventory_Import_Button_Normal.Click += AdvancedInventory_Button_Normal_Click;
            // 
            // AdvancedInventory_Import_Button_QuickButtonToggle
            // 
            AdvancedInventory_Import_Button_QuickButtonToggle.Dock = DockStyle.Fill;
            AdvancedInventory_Import_Button_QuickButtonToggle.Location = new Point(675, 3);
            AdvancedInventory_Import_Button_QuickButtonToggle.MaximumSize = new Size(32, 32);
            AdvancedInventory_Import_Button_QuickButtonToggle.MinimumSize = new Size(32, 32);
            AdvancedInventory_Import_Button_QuickButtonToggle.Name = "AdvancedInventory_Import_Button_QuickButtonToggle";
            AdvancedInventory_Import_Button_QuickButtonToggle.Size = new Size(32, 32);
            AdvancedInventory_Import_Button_QuickButtonToggle.TabIndex = 17;
            // 
            // AdvancedInventory_Import_TableLayout_Top
            // 
            AdvancedInventory_Import_TableLayout_Top.AutoSize = true;
            AdvancedInventory_Import_TableLayout_Top.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AdvancedInventory_Import_TableLayout_Top.ColumnCount = 3;
            AdvancedInventory_Import_TableLayout_Top.ColumnStyles.Add(new ColumnStyle());
            AdvancedInventory_Import_TableLayout_Top.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            AdvancedInventory_Import_TableLayout_Top.ColumnStyles.Add(new ColumnStyle());
            AdvancedInventory_Import_TableLayout_Top.Controls.Add(AdvancedInventory_Import_Button_OpenExcel, 2, 0);
            AdvancedInventory_Import_TableLayout_Top.Controls.Add(AdvancedInventory_Import_Button_ImportExcel, 0, 0);
            AdvancedInventory_Import_TableLayout_Top.Dock = DockStyle.Fill;
            AdvancedInventory_Import_TableLayout_Top.Location = new Point(3, 3);
            AdvancedInventory_Import_TableLayout_Top.Name = "AdvancedInventory_Import_TableLayout_Top";
            AdvancedInventory_Import_TableLayout_Top.RowCount = 1;
            AdvancedInventory_Import_TableLayout_Top.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            AdvancedInventory_Import_TableLayout_Top.Size = new Size(710, 38);
            AdvancedInventory_Import_TableLayout_Top.TabIndex = 4;
            // 
            // AdvancedInventory_Import_Button_OpenExcel
            // 
            AdvancedInventory_Import_Button_OpenExcel.Dock = DockStyle.Fill;
            AdvancedInventory_Import_Button_OpenExcel.Location = new Point(607, 3);
            AdvancedInventory_Import_Button_OpenExcel.MaximumSize = new Size(100, 32);
            AdvancedInventory_Import_Button_OpenExcel.MinimumSize = new Size(100, 32);
            AdvancedInventory_Import_Button_OpenExcel.Name = "AdvancedInventory_Import_Button_OpenExcel";
            AdvancedInventory_Import_Button_OpenExcel.Size = new Size(100, 32);
            AdvancedInventory_Import_Button_OpenExcel.TabIndex = 0;
            AdvancedInventory_Import_Button_OpenExcel.Text = "Open Excel";
            AdvancedInventory_Import_Button_OpenExcel.Click += AdvancedInventory_Import_Button_OpenExcel_Click;
            // 
            // AdvancedInventory_Import_Button_ImportExcel
            // 
            AdvancedInventory_Import_Button_ImportExcel.Dock = DockStyle.Fill;
            AdvancedInventory_Import_Button_ImportExcel.Location = new Point(3, 3);
            AdvancedInventory_Import_Button_ImportExcel.MaximumSize = new Size(100, 32);
            AdvancedInventory_Import_Button_ImportExcel.MinimumSize = new Size(100, 32);
            AdvancedInventory_Import_Button_ImportExcel.Name = "AdvancedInventory_Import_Button_ImportExcel";
            AdvancedInventory_Import_Button_ImportExcel.Size = new Size(100, 32);
            AdvancedInventory_Import_Button_ImportExcel.TabIndex = 1;
            AdvancedInventory_Import_Button_ImportExcel.Text = "Import Excel";
            AdvancedInventory_Import_Button_ImportExcel.Click += AdvancedInventory_Import_Button_ImportExcel_Click;
            // 
            // AdvancedInventory_GroupBox_Main
            // 
            AdvancedInventory_GroupBox_Main.AutoSize = true;
            AdvancedInventory_GroupBox_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AdvancedInventory_GroupBox_Main.Controls.Add(AdvancedInventory_TabControl);
            AdvancedInventory_GroupBox_Main.Dock = DockStyle.Fill;
            AdvancedInventory_GroupBox_Main.Location = new Point(0, 0);
            AdvancedInventory_GroupBox_Main.Name = "AdvancedInventory_GroupBox_Main";
            AdvancedInventory_GroupBox_Main.Size = new Size(730, 379);
            AdvancedInventory_GroupBox_Main.TabIndex = 2;
            AdvancedInventory_GroupBox_Main.TabStop = false;
            AdvancedInventory_GroupBox_Main.Text = "Advanced Inventory Entry";
            // 
            // Control_AdvancedInventory
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            Controls.Add(AdvancedInventory_GroupBox_Main);
            Name = "Control_AdvancedInventory";
            Size = new Size(730, 379);
            AdvancedInventory_TabControl.ResumeLayout(false);
            AdvancedInventory_TabControl_Single.ResumeLayout(false);
            AdvancedInventory_TabControl_Single.PerformLayout();
            AdvancedInventory_TableLayout_Single.ResumeLayout(false);
            AdvancedInventory_TableLayout_Single.PerformLayout();
            AdvancedInventory_Single_GroupBox_Right.ResumeLayout(false);
            AdvancedInventory_Single_GroupBox_Right.PerformLayout();
            AdvancedInventory_Single_TableLayout_Right.ResumeLayout(false);
            AdvancedInventory_Single_TableLayout_Right.PerformLayout();
            AdvancedInventory_Single_TableLayout_LowerRight.ResumeLayout(false);
            AdvancedInventory_Single_Panel_Preview.ResumeLayout(false);
            AdvancedInventory_Single_GroupBox_Left.ResumeLayout(false);
            AdvancedInventory_Single_GroupBox_Left.PerformLayout();
            AdvancedInventory_Single_TableLayout_Left.ResumeLayout(false);
            AdvancedInventory_Single_TableLayout_Left.PerformLayout();
            panel4.ResumeLayout(false);
            AdvancedInventory_TabControl_MultiLoc.ResumeLayout(false);
            AdvancedInventory_TabControl_MultiLoc.PerformLayout();
            AdvancedInventory_TableLayoutPanel_Multi.ResumeLayout(false);
            AdvancedInventory_TableLayoutPanel_Multi.PerformLayout();
            AdvancedInventory_MultiLoc_GroupBox_Preview.ResumeLayout(false);
            AdvancedInventory_MultiLoc_GroupBox_Preview.PerformLayout();
            AdvancedInventory_Multi_TableLayout_Right.ResumeLayout(false);
            AdvancedInventory_Multi_TableLayout_Right.PerformLayout();
            panel1.ResumeLayout(false);
            AdvancedInventory_Multi_TableLayout_BottomRight.ResumeLayout(false);
            AdvancedInventory_Multi_TableLayout_BottomRight.PerformLayout();
            AdvancedInventory_MultiLoc_GroupBox_Item.ResumeLayout(false);
            AdvancedInventory_MultiLoc_GroupBox_Item.PerformLayout();
            AdvancedInventory_Multi_TableLayout_Left.ResumeLayout(false);
            AdvancedInventory_Multi_TableLayout_Left.PerformLayout();
            panel3.ResumeLayout(false);
            AdvancedInventory_TabControl_Import.ResumeLayout(false);
            AdvancedInventory_TabControl_Import.PerformLayout();
            AdvancedInventory_Import_TableLayout.ResumeLayout(false);
            AdvancedInventory_Import_TableLayout.PerformLayout();
            AdvancedInventory_Import_Panel_Middle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)AdvancedInventory_Import_DataGridView).EndInit();
            AdvancedInventory_Import_TableLayout_Bottom.ResumeLayout(false);
            AdvancedInventory_Import_TableLayout_Top.ResumeLayout(false);
            AdvancedInventory_GroupBox_Main.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
        private Label AdvancedInventory_Single_Label_Part;
        private Button AdvancedInventory_Single_Button_Send;
        private Label AdvancedInventory_Single_Label_Op;
        private RichTextBox AdvancedInventory_Single_RichTextBox_Notes;
        private Label AdvancedInventory_Single_Label_Loc;
        private Label AdvancedInventory_Single_Label_Notes;
        private TextBox AdvancedInventory_Single_TextBox_Count;
        private Label AdvancedInventory_Single_Label_Qty;
        private Label AdvancedInventory_Single_Label_Count;
        private TextBox AdvancedInventory_Single_TextBox_Qty;
        private GroupBox AdvancedInventory_Single_GroupBox_Left;
        private TableLayoutPanel AdvancedInventory_Single_TableLayout_Left;
        private TableLayoutPanel AdvancedInventory_Single_TableLayout_Right;
        private TableLayoutPanel AdvancedInventory_Single_TableLayout_LowerRight;
        private TableLayoutPanel AdvancedInventory_Multi_TableLayout_Right;
        private TableLayoutPanel AdvancedInventory_Multi_TableLayout_BottomRight;
        private TableLayoutPanel AdvancedInventory_Import_TableLayout_Bottom;
        private TableLayoutPanel AdvancedInventory_Import_TableLayout_Top;
        private Panel panel1;
        private Panel panel4;
        private TableLayoutPanel AdvancedInventory_Multi_TableLayout_Left;
        private Button AdvancedInventory_MultiLoc_Button_AddLoc;
        private Label AdvancedInventory_MultiLoc_Label_Part;
        private Shared.SuggestionTextBox AdvancedInventory_MultiLoc_TextBox_Part;
        private Label AdvancedInventory_MultiLoc_Label_Op;
        private Shared.SuggestionTextBox AdvancedInventory_MultiLoc_TextBox_Op;
        private Label AdvancedInventory_MultiLoc_Label_Qty;
        private Label AdvancedInventory_MultiLoc_Label_Notes;
        private TextBox AdvancedInventory_MultiLoc_TextBox_Qty;
        private Label AdvancedInventory_MultiLoc_Label_Loc;
        private Shared.SuggestionTextBox AdvancedInventory_MultiLoc_TextBox_Loc;
        private Panel panel3;
        private RichTextBox AdvancedInventory_MultiLoc_RichTextBox_Notes;
        private Button AdvancedInventory_MultiLoc_Button_LocationF4;
        private Button AdvancedInventory_MultiLoc_Button_OperationF4;
        private Button AdvancedInventory_MultiLoc_Button_PartF4;
        private Button AdvancedInventory_Single_Button_LocationF4;
        private Button AdvancedInventory_Single_Button_OperationF4;
        private Button AdvancedInventory_Single_Button_PartF4;
        private Button AdvancedInventory_Import_Button_CleanSheet;
        private Button AdvancedInventory_Single_Button_InputToggle;
        private Button AdvancedInventory_Single_Button_QuickButtonToggle;
        private Button AdvancedInventory_Multi_Button_InputToggle;
        private Button AdvancedInventory_Multi_Button_QuickButtonToggle;
        private Panel AdvancedInventory_Single_Panel_Preview;
        private ListView AdvancedInventory_Single_ListView_Preview;
        private Button AdvancedInventory_Import_Button_QuickButtonToggle;
    }

        
        #endregion
    }
