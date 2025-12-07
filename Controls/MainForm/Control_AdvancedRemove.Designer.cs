using System.Drawing;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Components.Shared;
using MTM_WIP_Application_Winforms.Models.Enums;

namespace MTM_WIP_Application_Winforms.Controls.MainForm
{
    partial class Control_AdvancedRemove
    {
        #region Fields



        private System.ComponentModel.IContainer components = null;

        #endregion

        private System.Windows.Forms.GroupBox Control_AdvancedRemove_GroupBox_Main;
        private System.Windows.Forms.TableLayoutPanel Control_AdvancedRemove_TableLayout_Main;
        private System.Windows.Forms.Label Control_AdvancedRemove_Label_Op;
        private System.Windows.Forms.Label Control_AdvancedRemove_Label_Loc;
        private System.Windows.Forms.Label Control_AdvancedRemove_Label_Qty;
        private System.Windows.Forms.TextBox Control_AdvancedRemove_TextBox_QtyMin;
        private System.Windows.Forms.Label Control_AdvancedRemove_Label_QtyDash;
        private System.Windows.Forms.TextBox Control_AdvancedRemove_TextBox_QtyMax;
        private System.Windows.Forms.DateTimePicker Control_AdvancedRemove_DateTimePicker_From;
        private System.Windows.Forms.Label Control_AdvancedRemove_Label_DateDash;
        private System.Windows.Forms.DateTimePicker Control_AdvancedRemove_DateTimePicker_To;
        private System.Windows.Forms.Label Control_AdvancedRemove_Label_Notes;
        private System.Windows.Forms.TextBox Control_AdvancedRemove_TextBox_Notes;
        private System.Windows.Forms.DataGridView Control_AdvancedRemove_DataGridView_Results;
        private Panel Control_AdvancedRemove_Panel_DGV;
        internal Component_SuggestionTextBoxWithLabel Control_AdvancedRemove_SuggestionBox_User;
        private PictureBox Control_AdvancedRemove_Image_NothingFound;
        private Button Control_AdvancedRemove_Button_Undo;
        private Button Control_AdvancedRemove_Button_Delete;
        private Button Control_AdvancedRemove_Button_Search;
        private TableLayoutPanel Control_AdvancedRemove_TableLayout_Inputs;
        private Button Control_AdvancedRemove_Button_SidePanel;


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
            Control_AdvancedRemove_GroupBox_Main = new GroupBox();
            Control_AdvancedRemove_TableLayout_Main = new TableLayoutPanel();
            Control_AdvancedRemove_TableLayout_Bottom_Buttons = new TableLayoutPanel();
            MainForm_Button_Help_AdvancedRemove = new Button();
            Control_AdvancedRemove_Button_SidePanel = new Button();
            Control_AdvancedRemove_Button_Normal = new Button();
            Control_AdvancedRemove_Button_Print = new Button();
            Control_AdvancedRemove_Button_Reset = new Button();
            Control_AdvancedRemove_Button_QuickButtonToggle = new Button();
            Control_AdvancedRemove_TableLayout_TopRow = new TableLayoutPanel();
            Control_AdvancedRemove_Panel_DGV = new Panel();
            Control_AdvancedRemove_Image_NothingFound = new PictureBox();
            Control_AdvancedRemove_DataGridView_Results = new DataGridView();
            Control_AdvancedRemove_Panel_Inputs = new Panel();
            Control_AdvancedRemove_TableLayout_Inputs = new TableLayoutPanel();
            Control_AdvancedRemove_TextBox_Location = new TextBox();
            Control_AdvancedRemove_TextBox_Part = new TextBox();
            Control_AdvancedRemove_Label_Loc = new Label();
            Control_AdvancedRemove_Label_Op = new Label();
            Control_AdvancedRemove_TextBox_Operation = new TextBox();
            Control_AdvancedRemove_Label_Part = new Label();
            Control_AdvancedRemove_TableLayout_Buttons_Left = new TableLayoutPanel();
            Control_AdvancedRemove_Button_SearchVisual = new Button();
            Control_AdvancedRemove_Button_Search = new Button();
            Control_AdvancedRemove_Button_Delete = new Button();
            Control_AdvancedRemove_Button_Undo = new Button();
            Control_AdvancedRemove_TextBox_Notes = new TextBox();
            Control_AdvancedRemove_Label_Notes = new Label();
            Control_AdvancedRemove_Label_Qty = new Label();
            Control_AdvancedRemove_TableLayout_Quantity = new TableLayoutPanel();
            Control_AdvancedRemove_TextBox_QtyMin = new TextBox();
            Control_AdvancedRemove_TextBox_QtyMax = new TextBox();
            Control_AdvancedRemove_Label_QtyDash = new Label();
            Control_AdvancedRemove_Label_DateRange = new Label();
            Control_AdvancedRemove_TableLayout_DateRange = new TableLayoutPanel();
            Control_AdvancedRemove_DateTimePicker_To = new DateTimePicker();
            Control_AdvancedRemove_DateTimePicker_From = new DateTimePicker();
            Control_AdvancedRemove_Label_DateDash = new Label();
            TransactionSearchControl_TableLayout_QuickFilters = new TableLayoutPanel();
            Control_AdvancedRemove_RadioButton_Month = new RadioButton();
            Control_AdvancedRemove_RadioButton_Today = new RadioButton();
            Control_AdvancedRemove_RadioButton_Week = new RadioButton();
            Control_AdvancedRemove_RadioButton_Everything = new RadioButton();
            Control_AdvancedRemove_RadioButton_Custom = new RadioButton();
            Control_AdvancedRemove_SuggestionBox_User = new Component_SuggestionTextBoxWithLabel();
            Control_AdvancedRemove_GroupBox_Main.SuspendLayout();
            Control_AdvancedRemove_TableLayout_Main.SuspendLayout();
            Control_AdvancedRemove_TableLayout_Bottom_Buttons.SuspendLayout();
            Control_AdvancedRemove_TableLayout_TopRow.SuspendLayout();
            Control_AdvancedRemove_Panel_DGV.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Control_AdvancedRemove_Image_NothingFound).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Control_AdvancedRemove_DataGridView_Results).BeginInit();
            Control_AdvancedRemove_Panel_Inputs.SuspendLayout();
            Control_AdvancedRemove_TableLayout_Inputs.SuspendLayout();
            Control_AdvancedRemove_TableLayout_Buttons_Left.SuspendLayout();
            Control_AdvancedRemove_TableLayout_Quantity.SuspendLayout();
            Control_AdvancedRemove_TableLayout_DateRange.SuspendLayout();
            TransactionSearchControl_TableLayout_QuickFilters.SuspendLayout();
            SuspendLayout();
            // 
            // Control_AdvancedRemove_GroupBox_Main
            // 
            Control_AdvancedRemove_GroupBox_Main.AutoSize = true;
            Control_AdvancedRemove_GroupBox_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_AdvancedRemove_GroupBox_Main.Controls.Add(Control_AdvancedRemove_TableLayout_Main);
            Control_AdvancedRemove_GroupBox_Main.Dock = DockStyle.Fill;
            Control_AdvancedRemove_GroupBox_Main.Location = new Point(0, 0);
            Control_AdvancedRemove_GroupBox_Main.Name = "Control_AdvancedRemove_GroupBox_Main";
            Control_AdvancedRemove_GroupBox_Main.Size = new Size(800, 434);
            Control_AdvancedRemove_GroupBox_Main.TabIndex = 0;
            Control_AdvancedRemove_GroupBox_Main.TabStop = false;
            Control_AdvancedRemove_GroupBox_Main.Text = "Advanced Inventory Removal";
            // 
            // Control_AdvancedRemove_TableLayout_Main
            // 
            Control_AdvancedRemove_TableLayout_Main.ColumnCount = 1;
            Control_AdvancedRemove_TableLayout_Main.ColumnStyles.Add(new ColumnStyle());
            Control_AdvancedRemove_TableLayout_Main.Controls.Add(Control_AdvancedRemove_TableLayout_Bottom_Buttons, 0, 1);
            Control_AdvancedRemove_TableLayout_Main.Controls.Add(Control_AdvancedRemove_TableLayout_TopRow, 0, 0);
            Control_AdvancedRemove_TableLayout_Main.Dock = DockStyle.Fill;
            Control_AdvancedRemove_TableLayout_Main.Location = new Point(3, 19);
            Control_AdvancedRemove_TableLayout_Main.Name = "Control_AdvancedRemove_TableLayout_Main";
            Control_AdvancedRemove_TableLayout_Main.RowCount = 2;
            Control_AdvancedRemove_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_AdvancedRemove_TableLayout_Main.RowStyles.Add(new RowStyle());
            Control_AdvancedRemove_TableLayout_Main.Size = new Size(794, 412);
            Control_AdvancedRemove_TableLayout_Main.TabIndex = 1;
            // 
            // Control_AdvancedRemove_TableLayout_Bottom_Buttons
            // 
            Control_AdvancedRemove_TableLayout_Bottom_Buttons.AutoSize = true;
            Control_AdvancedRemove_TableLayout_Bottom_Buttons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_AdvancedRemove_TableLayout_Bottom_Buttons.ColumnCount = 7;
            Control_AdvancedRemove_TableLayout_Bottom_Buttons.ColumnStyles.Add(new ColumnStyle());
            Control_AdvancedRemove_TableLayout_Bottom_Buttons.ColumnStyles.Add(new ColumnStyle());
            Control_AdvancedRemove_TableLayout_Bottom_Buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_AdvancedRemove_TableLayout_Bottom_Buttons.ColumnStyles.Add(new ColumnStyle());
            Control_AdvancedRemove_TableLayout_Bottom_Buttons.ColumnStyles.Add(new ColumnStyle());
            Control_AdvancedRemove_TableLayout_Bottom_Buttons.ColumnStyles.Add(new ColumnStyle());
            Control_AdvancedRemove_TableLayout_Bottom_Buttons.ColumnStyles.Add(new ColumnStyle());
            Control_AdvancedRemove_TableLayout_Bottom_Buttons.Controls.Add(MainForm_Button_Help_AdvancedRemove, 5, 0);
            Control_AdvancedRemove_TableLayout_Bottom_Buttons.Controls.Add(Control_AdvancedRemove_Button_SidePanel, 0, 0);
            Control_AdvancedRemove_TableLayout_Bottom_Buttons.Controls.Add(Control_AdvancedRemove_Button_Normal, 4, 0);
            Control_AdvancedRemove_TableLayout_Bottom_Buttons.Controls.Add(Control_AdvancedRemove_Button_Print, 3, 0);
            Control_AdvancedRemove_TableLayout_Bottom_Buttons.Controls.Add(Control_AdvancedRemove_Button_Reset, 1, 0);
            Control_AdvancedRemove_TableLayout_Bottom_Buttons.Controls.Add(Control_AdvancedRemove_Button_QuickButtonToggle, 6, 0);
            Control_AdvancedRemove_TableLayout_Bottom_Buttons.Dock = DockStyle.Fill;
            Control_AdvancedRemove_TableLayout_Bottom_Buttons.Location = new Point(3, 371);
            Control_AdvancedRemove_TableLayout_Bottom_Buttons.Name = "Control_AdvancedRemove_TableLayout_Bottom_Buttons";
            Control_AdvancedRemove_TableLayout_Bottom_Buttons.RowCount = 1;
            Control_AdvancedRemove_TableLayout_Bottom_Buttons.RowStyles.Add(new RowStyle());
            Control_AdvancedRemove_TableLayout_Bottom_Buttons.Size = new Size(788, 38);
            Control_AdvancedRemove_TableLayout_Bottom_Buttons.TabIndex = 0;
            // 
            // MainForm_Button_Help_AdvancedRemove
            // 
            MainForm_Button_Help_AdvancedRemove.Location = new Point(683, 3);
            MainForm_Button_Help_AdvancedRemove.MaximumSize = new Size(32, 32);
            MainForm_Button_Help_AdvancedRemove.MinimumSize = new Size(32, 32);
            MainForm_Button_Help_AdvancedRemove.Name = "MainForm_Button_Help_AdvancedRemove";
            MainForm_Button_Help_AdvancedRemove.Size = new Size(32, 32);
            MainForm_Button_Help_AdvancedRemove.TabIndex = 1004;
            MainForm_Button_Help_AdvancedRemove.Text = "?";
            MainForm_Button_Help_AdvancedRemove.UseVisualStyleBackColor = true;
            // 
            // Control_AdvancedRemove_Button_SidePanel
            // 
            Control_AdvancedRemove_Button_SidePanel.AutoSize = true;
            Control_AdvancedRemove_Button_SidePanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_AdvancedRemove_Button_SidePanel.Dock = DockStyle.Fill;
            Control_AdvancedRemove_Button_SidePanel.Font = new Font("Segoe UI Emoji", 9F);
            Control_AdvancedRemove_Button_SidePanel.Location = new Point(3, 3);
            Control_AdvancedRemove_Button_SidePanel.MaximumSize = new Size(64, 32);
            Control_AdvancedRemove_Button_SidePanel.MinimumSize = new Size(64, 32);
            Control_AdvancedRemove_Button_SidePanel.Name = "Control_AdvancedRemove_Button_SidePanel";
            Control_AdvancedRemove_Button_SidePanel.Size = new Size(64, 32);
            Control_AdvancedRemove_Button_SidePanel.TabIndex = 10;
            Control_AdvancedRemove_Button_SidePanel.TabStop = false;
            Control_AdvancedRemove_Button_SidePanel.UseVisualStyleBackColor = true;
            Control_AdvancedRemove_Button_SidePanel.Click += Control_AdvancedRemove_Button_SidePanel_Click;
            // 
            // Control_AdvancedRemove_Button_Normal
            // 
            Control_AdvancedRemove_Button_Normal.AutoSize = true;
            Control_AdvancedRemove_Button_Normal.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_AdvancedRemove_Button_Normal.Dock = DockStyle.Fill;
            Control_AdvancedRemove_Button_Normal.Font = new Font("Segoe UI Emoji", 9F);
            Control_AdvancedRemove_Button_Normal.ForeColor = Color.DarkRed;
            Control_AdvancedRemove_Button_Normal.Location = new Point(557, 3);
            Control_AdvancedRemove_Button_Normal.MaximumSize = new Size(120, 32);
            Control_AdvancedRemove_Button_Normal.MinimumSize = new Size(120, 32);
            Control_AdvancedRemove_Button_Normal.Name = "Control_AdvancedRemove_Button_Normal";
            Control_AdvancedRemove_Button_Normal.Size = new Size(120, 32);
            Control_AdvancedRemove_Button_Normal.TabIndex = 15;
            Control_AdvancedRemove_Button_Normal.TabStop = false;
            Control_AdvancedRemove_Button_Normal.Text = "Back to Normal";
            // 
            // Control_AdvancedRemove_Button_Print
            // 
            Control_AdvancedRemove_Button_Print.AutoSize = true;
            Control_AdvancedRemove_Button_Print.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_AdvancedRemove_Button_Print.Dock = DockStyle.Fill;
            Control_AdvancedRemove_Button_Print.Enabled = false;
            Control_AdvancedRemove_Button_Print.Font = new Font("Segoe UI Emoji", 9F);
            Control_AdvancedRemove_Button_Print.Location = new Point(451, 3);
            Control_AdvancedRemove_Button_Print.MaximumSize = new Size(100, 32);
            Control_AdvancedRemove_Button_Print.MinimumSize = new Size(100, 32);
            Control_AdvancedRemove_Button_Print.Name = "Control_AdvancedRemove_Button_Print";
            Control_AdvancedRemove_Button_Print.Size = new Size(100, 32);
            Control_AdvancedRemove_Button_Print.TabIndex = 16;
            Control_AdvancedRemove_Button_Print.TabStop = false;
            Control_AdvancedRemove_Button_Print.Text = "Print";
            // 
            // Control_AdvancedRemove_Button_Reset
            // 
            Control_AdvancedRemove_Button_Reset.AutoSize = true;
            Control_AdvancedRemove_Button_Reset.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_AdvancedRemove_Button_Reset.Dock = DockStyle.Fill;
            Control_AdvancedRemove_Button_Reset.Font = new Font("Segoe UI Emoji", 9F);
            Control_AdvancedRemove_Button_Reset.Location = new Point(73, 3);
            Control_AdvancedRemove_Button_Reset.MaximumSize = new Size(100, 32);
            Control_AdvancedRemove_Button_Reset.MinimumSize = new Size(100, 32);
            Control_AdvancedRemove_Button_Reset.Name = "Control_AdvancedRemove_Button_Reset";
            Control_AdvancedRemove_Button_Reset.Size = new Size(100, 32);
            Control_AdvancedRemove_Button_Reset.TabIndex = 15;
            Control_AdvancedRemove_Button_Reset.Text = "Reset";
            // 
            // Control_AdvancedRemove_Button_QuickButtonToggle
            // 
            Control_AdvancedRemove_Button_QuickButtonToggle.AutoSize = true;
            Control_AdvancedRemove_Button_QuickButtonToggle.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_AdvancedRemove_Button_QuickButtonToggle.Dock = DockStyle.Fill;
            Control_AdvancedRemove_Button_QuickButtonToggle.Font = new Font("Segoe UI Emoji", 9F);
            Control_AdvancedRemove_Button_QuickButtonToggle.Location = new Point(721, 3);
            Control_AdvancedRemove_Button_QuickButtonToggle.MaximumSize = new Size(64, 32);
            Control_AdvancedRemove_Button_QuickButtonToggle.MinimumSize = new Size(64, 32);
            Control_AdvancedRemove_Button_QuickButtonToggle.Name = "Control_AdvancedRemove_Button_QuickButtonToggle";
            Control_AdvancedRemove_Button_QuickButtonToggle.Size = new Size(64, 32);
            Control_AdvancedRemove_Button_QuickButtonToggle.TabIndex = 17;
            Control_AdvancedRemove_Button_QuickButtonToggle.TabStop = false;
            Control_AdvancedRemove_Button_QuickButtonToggle.UseVisualStyleBackColor = true;
            // 
            // Control_AdvancedRemove_TableLayout_TopRow
            // 
            Control_AdvancedRemove_TableLayout_TopRow.AutoSize = true;
            Control_AdvancedRemove_TableLayout_TopRow.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_AdvancedRemove_TableLayout_TopRow.ColumnCount = 2;
            Control_AdvancedRemove_TableLayout_TopRow.ColumnStyles.Add(new ColumnStyle());
            Control_AdvancedRemove_TableLayout_TopRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_AdvancedRemove_TableLayout_TopRow.Controls.Add(Control_AdvancedRemove_Panel_DGV, 1, 0);
            Control_AdvancedRemove_TableLayout_TopRow.Controls.Add(Control_AdvancedRemove_Panel_Inputs, 0, 0);
            Control_AdvancedRemove_TableLayout_TopRow.Dock = DockStyle.Fill;
            Control_AdvancedRemove_TableLayout_TopRow.Location = new Point(3, 3);
            Control_AdvancedRemove_TableLayout_TopRow.Name = "Control_AdvancedRemove_TableLayout_TopRow";
            Control_AdvancedRemove_TableLayout_TopRow.RowCount = 1;
            Control_AdvancedRemove_TableLayout_TopRow.RowStyles.Add(new RowStyle());
            Control_AdvancedRemove_TableLayout_TopRow.Size = new Size(788, 362);
            Control_AdvancedRemove_TableLayout_TopRow.TabIndex = 1;
            // 
            // Control_AdvancedRemove_Panel_DGV
            // 
            Control_AdvancedRemove_Panel_DGV.AutoSize = true;
            Control_AdvancedRemove_Panel_DGV.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_AdvancedRemove_Panel_DGV.BorderStyle = BorderStyle.FixedSingle;
            Control_AdvancedRemove_Panel_DGV.Controls.Add(Control_AdvancedRemove_Image_NothingFound);
            Control_AdvancedRemove_Panel_DGV.Controls.Add(Control_AdvancedRemove_DataGridView_Results);
            Control_AdvancedRemove_Panel_DGV.Dock = DockStyle.Fill;
            Control_AdvancedRemove_Panel_DGV.Location = new Point(309, 3);
            Control_AdvancedRemove_Panel_DGV.Name = "Control_AdvancedRemove_Panel_DGV";
            Control_AdvancedRemove_Panel_DGV.Padding = new Padding(3);
            Control_AdvancedRemove_Panel_DGV.Size = new Size(476, 356);
            Control_AdvancedRemove_Panel_DGV.TabIndex = 0;
            // 
            // Control_AdvancedRemove_Image_NothingFound
            // 
            Control_AdvancedRemove_Image_NothingFound.BackColor = Color.White;
            Control_AdvancedRemove_Image_NothingFound.BorderStyle = BorderStyle.FixedSingle;
            Control_AdvancedRemove_Image_NothingFound.Dock = DockStyle.Fill;
            Control_AdvancedRemove_Image_NothingFound.ErrorImage = null;
            Control_AdvancedRemove_Image_NothingFound.Image = Properties.Resources.NothingFound;
            Control_AdvancedRemove_Image_NothingFound.InitialImage = null;
            Control_AdvancedRemove_Image_NothingFound.Location = new Point(3, 3);
            Control_AdvancedRemove_Image_NothingFound.Name = "Control_AdvancedRemove_Image_NothingFound";
            Control_AdvancedRemove_Image_NothingFound.Size = new Size(468, 348);
            Control_AdvancedRemove_Image_NothingFound.SizeMode = PictureBoxSizeMode.CenterImage;
            Control_AdvancedRemove_Image_NothingFound.TabIndex = 0;
            Control_AdvancedRemove_Image_NothingFound.TabStop = false;
            Control_AdvancedRemove_Image_NothingFound.Visible = false;
            // 
            // Control_AdvancedRemove_DataGridView_Results
            // 
            Control_AdvancedRemove_DataGridView_Results.AllowUserToAddRows = false;
            Control_AdvancedRemove_DataGridView_Results.AllowUserToDeleteRows = false;
            Control_AdvancedRemove_DataGridView_Results.Dock = DockStyle.Fill;
            Control_AdvancedRemove_DataGridView_Results.Location = new Point(3, 3);
            Control_AdvancedRemove_DataGridView_Results.Name = "Control_AdvancedRemove_DataGridView_Results";
            Control_AdvancedRemove_DataGridView_Results.ReadOnly = true;
            Control_AdvancedRemove_DataGridView_Results.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Control_AdvancedRemove_DataGridView_Results.Size = new Size(468, 348);
            Control_AdvancedRemove_DataGridView_Results.TabIndex = 1;
            Control_AdvancedRemove_DataGridView_Results.TabStop = false;
            // 
            // Control_AdvancedRemove_Panel_Inputs
            // 
            Control_AdvancedRemove_Panel_Inputs.AutoSize = true;
            Control_AdvancedRemove_Panel_Inputs.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_AdvancedRemove_Panel_Inputs.BorderStyle = BorderStyle.FixedSingle;
            Control_AdvancedRemove_Panel_Inputs.Controls.Add(Control_AdvancedRemove_TableLayout_Inputs);
            Control_AdvancedRemove_Panel_Inputs.Dock = DockStyle.Fill;
            Control_AdvancedRemove_Panel_Inputs.Location = new Point(3, 3);
            Control_AdvancedRemove_Panel_Inputs.Name = "Control_AdvancedRemove_Panel_Inputs";
            Control_AdvancedRemove_Panel_Inputs.Size = new Size(300, 356);
            Control_AdvancedRemove_Panel_Inputs.TabIndex = 1;
            // 
            // Control_AdvancedRemove_TableLayout_Inputs
            // 
            Control_AdvancedRemove_TableLayout_Inputs.AutoSize = true;
            Control_AdvancedRemove_TableLayout_Inputs.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_AdvancedRemove_TableLayout_Inputs.ColumnCount = 2;
            Control_AdvancedRemove_TableLayout_Inputs.ColumnStyles.Add(new ColumnStyle());
            Control_AdvancedRemove_TableLayout_Inputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_AdvancedRemove_TableLayout_Inputs.Controls.Add(Control_AdvancedRemove_TextBox_Location, 1, 1);
            Control_AdvancedRemove_TableLayout_Inputs.Controls.Add(Control_AdvancedRemove_TextBox_Part, 1, 0);
            Control_AdvancedRemove_TableLayout_Inputs.Controls.Add(Control_AdvancedRemove_Label_Loc, 0, 1);
            Control_AdvancedRemove_TableLayout_Inputs.Controls.Add(Control_AdvancedRemove_Label_Op, 0, 2);
            Control_AdvancedRemove_TableLayout_Inputs.Controls.Add(Control_AdvancedRemove_TextBox_Operation, 1, 2);
            Control_AdvancedRemove_TableLayout_Inputs.Controls.Add(Control_AdvancedRemove_Label_Part, 0, 0);
            Control_AdvancedRemove_TableLayout_Inputs.Controls.Add(Control_AdvancedRemove_TableLayout_Buttons_Left, 0, 10);
            Control_AdvancedRemove_TableLayout_Inputs.Controls.Add(Control_AdvancedRemove_TextBox_Notes, 1, 3);
            Control_AdvancedRemove_TableLayout_Inputs.Controls.Add(Control_AdvancedRemove_Label_Notes, 0, 3);
            Control_AdvancedRemove_TableLayout_Inputs.Controls.Add(Control_AdvancedRemove_Label_Qty, 0, 4);
            Control_AdvancedRemove_TableLayout_Inputs.Controls.Add(Control_AdvancedRemove_TableLayout_Quantity, 1, 4);
            Control_AdvancedRemove_TableLayout_Inputs.Controls.Add(Control_AdvancedRemove_Label_DateRange, 0, 5);
            Control_AdvancedRemove_TableLayout_Inputs.Controls.Add(Control_AdvancedRemove_TableLayout_DateRange, 0, 6);
            Control_AdvancedRemove_TableLayout_Inputs.Controls.Add(TransactionSearchControl_TableLayout_QuickFilters, 0, 7);
            Control_AdvancedRemove_TableLayout_Inputs.Controls.Add(Control_AdvancedRemove_SuggestionBox_User, 0, 9);
            Control_AdvancedRemove_TableLayout_Inputs.Dock = DockStyle.Fill;
            Control_AdvancedRemove_TableLayout_Inputs.Location = new Point(0, 0);
            Control_AdvancedRemove_TableLayout_Inputs.MaximumSize = new Size(298, 0);
            Control_AdvancedRemove_TableLayout_Inputs.Name = "Control_AdvancedRemove_TableLayout_Inputs";
            Control_AdvancedRemove_TableLayout_Inputs.RowCount = 11;
            Control_AdvancedRemove_TableLayout_Inputs.RowStyles.Add(new RowStyle());
            Control_AdvancedRemove_TableLayout_Inputs.RowStyles.Add(new RowStyle());
            Control_AdvancedRemove_TableLayout_Inputs.RowStyles.Add(new RowStyle());
            Control_AdvancedRemove_TableLayout_Inputs.RowStyles.Add(new RowStyle());
            Control_AdvancedRemove_TableLayout_Inputs.RowStyles.Add(new RowStyle());
            Control_AdvancedRemove_TableLayout_Inputs.RowStyles.Add(new RowStyle());
            Control_AdvancedRemove_TableLayout_Inputs.RowStyles.Add(new RowStyle());
            Control_AdvancedRemove_TableLayout_Inputs.RowStyles.Add(new RowStyle());
            Control_AdvancedRemove_TableLayout_Inputs.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_AdvancedRemove_TableLayout_Inputs.RowStyles.Add(new RowStyle());
            Control_AdvancedRemove_TableLayout_Inputs.RowStyles.Add(new RowStyle());
            Control_AdvancedRemove_TableLayout_Inputs.Size = new Size(298, 354);
            Control_AdvancedRemove_TableLayout_Inputs.TabIndex = 0;
            // 
            // Control_AdvancedRemove_TextBox_Location
            // 
            Control_AdvancedRemove_TextBox_Location.Dock = DockStyle.Fill;
            Control_AdvancedRemove_TextBox_Location.Font = new Font("Segoe UI Emoji", 9F);
            Control_AdvancedRemove_TextBox_Location.Location = new Point(65, 32);
            Control_AdvancedRemove_TextBox_Location.Name = "Control_AdvancedRemove_TextBox_Location";
            Control_AdvancedRemove_TextBox_Location.PlaceholderText = "Enter Location";
            Control_AdvancedRemove_TextBox_Location.Size = new Size(230, 23);
            Control_AdvancedRemove_TextBox_Location.TabIndex = 2;
            // 
            // Control_AdvancedRemove_TextBox_Part
            // 
            Control_AdvancedRemove_TextBox_Part.Dock = DockStyle.Fill;
            Control_AdvancedRemove_TextBox_Part.Font = new Font("Segoe UI Emoji", 9F);
            Control_AdvancedRemove_TextBox_Part.Location = new Point(65, 3);
            Control_AdvancedRemove_TextBox_Part.Name = "Control_AdvancedRemove_TextBox_Part";
            Control_AdvancedRemove_TextBox_Part.PlaceholderText = "Enter Part Number";
            Control_AdvancedRemove_TextBox_Part.Size = new Size(230, 23);
            Control_AdvancedRemove_TextBox_Part.TabIndex = 1;
            // 
            // Control_AdvancedRemove_Label_Loc
            // 
            Control_AdvancedRemove_Label_Loc.AutoSize = true;
            Control_AdvancedRemove_Label_Loc.Dock = DockStyle.Fill;
            Control_AdvancedRemove_Label_Loc.Font = new Font("Segoe UI Emoji", 9F);
            Control_AdvancedRemove_Label_Loc.Location = new Point(3, 32);
            Control_AdvancedRemove_Label_Loc.Margin = new Padding(3);
            Control_AdvancedRemove_Label_Loc.Name = "Control_AdvancedRemove_Label_Loc";
            Control_AdvancedRemove_Label_Loc.Size = new Size(56, 23);
            Control_AdvancedRemove_Label_Loc.TabIndex = 3;
            Control_AdvancedRemove_Label_Loc.Text = "Location:";
            Control_AdvancedRemove_Label_Loc.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Control_AdvancedRemove_Label_Op
            // 
            Control_AdvancedRemove_Label_Op.AutoSize = true;
            Control_AdvancedRemove_Label_Op.Dock = DockStyle.Fill;
            Control_AdvancedRemove_Label_Op.Font = new Font("Segoe UI Emoji", 9F);
            Control_AdvancedRemove_Label_Op.Location = new Point(3, 61);
            Control_AdvancedRemove_Label_Op.Margin = new Padding(3);
            Control_AdvancedRemove_Label_Op.Name = "Control_AdvancedRemove_Label_Op";
            Control_AdvancedRemove_Label_Op.Size = new Size(56, 23);
            Control_AdvancedRemove_Label_Op.TabIndex = 4;
            Control_AdvancedRemove_Label_Op.Text = "Op:";
            Control_AdvancedRemove_Label_Op.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Control_AdvancedRemove_TextBox_Operation
            // 
            Control_AdvancedRemove_TextBox_Operation.Dock = DockStyle.Fill;
            Control_AdvancedRemove_TextBox_Operation.Font = new Font("Segoe UI Emoji", 9F);
            Control_AdvancedRemove_TextBox_Operation.Location = new Point(65, 61);
            Control_AdvancedRemove_TextBox_Operation.Name = "Control_AdvancedRemove_TextBox_Operation";
            Control_AdvancedRemove_TextBox_Operation.PlaceholderText = "Enter Operation";
            Control_AdvancedRemove_TextBox_Operation.Size = new Size(230, 23);
            Control_AdvancedRemove_TextBox_Operation.TabIndex = 3;
            // 
            // Control_AdvancedRemove_Label_Part
            // 
            Control_AdvancedRemove_Label_Part.AutoSize = true;
            Control_AdvancedRemove_Label_Part.Dock = DockStyle.Fill;
            Control_AdvancedRemove_Label_Part.Font = new Font("Segoe UI Emoji", 9F);
            Control_AdvancedRemove_Label_Part.Location = new Point(3, 3);
            Control_AdvancedRemove_Label_Part.Margin = new Padding(3);
            Control_AdvancedRemove_Label_Part.Name = "Control_AdvancedRemove_Label_Part";
            Control_AdvancedRemove_Label_Part.Size = new Size(56, 23);
            Control_AdvancedRemove_Label_Part.TabIndex = 5;
            Control_AdvancedRemove_Label_Part.Text = "Part ID:";
            Control_AdvancedRemove_Label_Part.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Control_AdvancedRemove_TableLayout_Buttons_Left
            // 
            Control_AdvancedRemove_TableLayout_Buttons_Left.AutoSize = true;
            Control_AdvancedRemove_TableLayout_Buttons_Left.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_AdvancedRemove_TableLayout_Buttons_Left.ColumnCount = 7;
            Control_AdvancedRemove_TableLayout_Inputs.SetColumnSpan(Control_AdvancedRemove_TableLayout_Buttons_Left, 2);
            Control_AdvancedRemove_TableLayout_Buttons_Left.ColumnStyles.Add(new ColumnStyle());
            Control_AdvancedRemove_TableLayout_Buttons_Left.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            Control_AdvancedRemove_TableLayout_Buttons_Left.ColumnStyles.Add(new ColumnStyle());
            Control_AdvancedRemove_TableLayout_Buttons_Left.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            Control_AdvancedRemove_TableLayout_Buttons_Left.ColumnStyles.Add(new ColumnStyle());
            Control_AdvancedRemove_TableLayout_Buttons_Left.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            Control_AdvancedRemove_TableLayout_Buttons_Left.ColumnStyles.Add(new ColumnStyle());
            Control_AdvancedRemove_TableLayout_Buttons_Left.Controls.Add(Control_AdvancedRemove_Button_SearchVisual, 6, 0);
            Control_AdvancedRemove_TableLayout_Buttons_Left.Controls.Add(Control_AdvancedRemove_Button_Search, 0, 0);
            Control_AdvancedRemove_TableLayout_Buttons_Left.Controls.Add(Control_AdvancedRemove_Button_Delete, 4, 0);
            Control_AdvancedRemove_TableLayout_Buttons_Left.Controls.Add(Control_AdvancedRemove_Button_Undo, 2, 0);
            Control_AdvancedRemove_TableLayout_Buttons_Left.Dock = DockStyle.Fill;
            Control_AdvancedRemove_TableLayout_Buttons_Left.Location = new Point(3, 313);
            Control_AdvancedRemove_TableLayout_Buttons_Left.MaximumSize = new Size(0, 38);
            Control_AdvancedRemove_TableLayout_Buttons_Left.MinimumSize = new Size(0, 38);
            Control_AdvancedRemove_TableLayout_Buttons_Left.Name = "Control_AdvancedRemove_TableLayout_Buttons_Left";
            Control_AdvancedRemove_TableLayout_Buttons_Left.RowCount = 1;
            Control_AdvancedRemove_TableLayout_Buttons_Left.RowStyles.Add(new RowStyle());
            Control_AdvancedRemove_TableLayout_Buttons_Left.Size = new Size(292, 38);
            Control_AdvancedRemove_TableLayout_Buttons_Left.TabIndex = 6;
            // 
            // Control_AdvancedRemove_Button_SearchVisual
            // 
            Control_AdvancedRemove_Button_SearchVisual.AutoSize = true;
            Control_AdvancedRemove_Button_SearchVisual.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_AdvancedRemove_Button_SearchVisual.Dock = DockStyle.Fill;
            Control_AdvancedRemove_Button_SearchVisual.Enabled = false;
            Control_AdvancedRemove_Button_SearchVisual.Font = new Font("Segoe UI Emoji", 9F);
            Control_AdvancedRemove_Button_SearchVisual.Location = new Point(202, 3);
            Control_AdvancedRemove_Button_SearchVisual.MaximumSize = new Size(0, 32);
            Control_AdvancedRemove_Button_SearchVisual.MinimumSize = new Size(0, 32);
            Control_AdvancedRemove_Button_SearchVisual.Name = "Control_AdvancedRemove_Button_SearchVisual";
            Control_AdvancedRemove_Button_SearchVisual.Size = new Size(87, 32);
            Control_AdvancedRemove_Button_SearchVisual.TabIndex = 16;
            Control_AdvancedRemove_Button_SearchVisual.Text = "Search Visual";
            // 
            // Control_AdvancedRemove_Button_Search
            // 
            Control_AdvancedRemove_Button_Search.AutoSize = true;
            Control_AdvancedRemove_Button_Search.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_AdvancedRemove_Button_Search.Dock = DockStyle.Fill;
            Control_AdvancedRemove_Button_Search.Font = new Font("Segoe UI Emoji", 9F);
            Control_AdvancedRemove_Button_Search.Location = new Point(3, 3);
            Control_AdvancedRemove_Button_Search.MaximumSize = new Size(0, 32);
            Control_AdvancedRemove_Button_Search.MinimumSize = new Size(0, 32);
            Control_AdvancedRemove_Button_Search.Name = "Control_AdvancedRemove_Button_Search";
            Control_AdvancedRemove_Button_Search.Size = new Size(52, 32);
            Control_AdvancedRemove_Button_Search.TabIndex = 13;
            Control_AdvancedRemove_Button_Search.Text = "Search";
            // 
            // Control_AdvancedRemove_Button_Delete
            // 
            Control_AdvancedRemove_Button_Delete.AutoSize = true;
            Control_AdvancedRemove_Button_Delete.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_AdvancedRemove_Button_Delete.Dock = DockStyle.Fill;
            Control_AdvancedRemove_Button_Delete.Font = new Font("Segoe UI Emoji", 9F);
            Control_AdvancedRemove_Button_Delete.Location = new Point(135, 3);
            Control_AdvancedRemove_Button_Delete.MaximumSize = new Size(0, 32);
            Control_AdvancedRemove_Button_Delete.MinimumSize = new Size(0, 32);
            Control_AdvancedRemove_Button_Delete.Name = "Control_AdvancedRemove_Button_Delete";
            Control_AdvancedRemove_Button_Delete.Size = new Size(50, 32);
            Control_AdvancedRemove_Button_Delete.TabIndex = 14;
            Control_AdvancedRemove_Button_Delete.Text = "Delete";
            // 
            // Control_AdvancedRemove_Button_Undo
            // 
            Control_AdvancedRemove_Button_Undo.AutoSize = true;
            Control_AdvancedRemove_Button_Undo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_AdvancedRemove_Button_Undo.Dock = DockStyle.Fill;
            Control_AdvancedRemove_Button_Undo.Enabled = false;
            Control_AdvancedRemove_Button_Undo.Font = new Font("Segoe UI Emoji", 9F);
            Control_AdvancedRemove_Button_Undo.Location = new Point(72, 3);
            Control_AdvancedRemove_Button_Undo.MaximumSize = new Size(0, 32);
            Control_AdvancedRemove_Button_Undo.MinimumSize = new Size(0, 32);
            Control_AdvancedRemove_Button_Undo.Name = "Control_AdvancedRemove_Button_Undo";
            Control_AdvancedRemove_Button_Undo.Size = new Size(46, 32);
            Control_AdvancedRemove_Button_Undo.TabIndex = 15;
            Control_AdvancedRemove_Button_Undo.TabStop = false;
            Control_AdvancedRemove_Button_Undo.Text = "Undo";
            // 
            // Control_AdvancedRemove_TextBox_Notes
            // 
            Control_AdvancedRemove_TextBox_Notes.Dock = DockStyle.Fill;
            Control_AdvancedRemove_TextBox_Notes.Font = new Font("Segoe UI Emoji", 9F);
            Control_AdvancedRemove_TextBox_Notes.Location = new Point(65, 90);
            Control_AdvancedRemove_TextBox_Notes.Name = "Control_AdvancedRemove_TextBox_Notes";
            Control_AdvancedRemove_TextBox_Notes.PlaceholderText = "Enter Notes";
            Control_AdvancedRemove_TextBox_Notes.Size = new Size(230, 23);
            Control_AdvancedRemove_TextBox_Notes.TabIndex = 4;
            // 
            // Control_AdvancedRemove_Label_Notes
            // 
            Control_AdvancedRemove_Label_Notes.AutoSize = true;
            Control_AdvancedRemove_Label_Notes.Dock = DockStyle.Fill;
            Control_AdvancedRemove_Label_Notes.Font = new Font("Segoe UI Emoji", 9F);
            Control_AdvancedRemove_Label_Notes.Location = new Point(3, 90);
            Control_AdvancedRemove_Label_Notes.Margin = new Padding(3);
            Control_AdvancedRemove_Label_Notes.Name = "Control_AdvancedRemove_Label_Notes";
            Control_AdvancedRemove_Label_Notes.Size = new Size(56, 23);
            Control_AdvancedRemove_Label_Notes.TabIndex = 7;
            Control_AdvancedRemove_Label_Notes.Text = "Notes:";
            Control_AdvancedRemove_Label_Notes.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Control_AdvancedRemove_Label_Qty
            // 
            Control_AdvancedRemove_Label_Qty.AutoSize = true;
            Control_AdvancedRemove_Label_Qty.Dock = DockStyle.Fill;
            Control_AdvancedRemove_Label_Qty.Font = new Font("Segoe UI Emoji", 9F);
            Control_AdvancedRemove_Label_Qty.Location = new Point(3, 119);
            Control_AdvancedRemove_Label_Qty.Margin = new Padding(3);
            Control_AdvancedRemove_Label_Qty.Name = "Control_AdvancedRemove_Label_Qty";
            Control_AdvancedRemove_Label_Qty.Size = new Size(56, 29);
            Control_AdvancedRemove_Label_Qty.TabIndex = 8;
            Control_AdvancedRemove_Label_Qty.Text = "Quantity:";
            Control_AdvancedRemove_Label_Qty.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Control_AdvancedRemove_TableLayout_Quantity
            // 
            Control_AdvancedRemove_TableLayout_Quantity.AutoSize = true;
            Control_AdvancedRemove_TableLayout_Quantity.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_AdvancedRemove_TableLayout_Quantity.ColumnCount = 3;
            Control_AdvancedRemove_TableLayout_Quantity.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            Control_AdvancedRemove_TableLayout_Quantity.ColumnStyles.Add(new ColumnStyle());
            Control_AdvancedRemove_TableLayout_Quantity.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            Control_AdvancedRemove_TableLayout_Quantity.Controls.Add(Control_AdvancedRemove_TextBox_QtyMin, 0, 0);
            Control_AdvancedRemove_TableLayout_Quantity.Controls.Add(Control_AdvancedRemove_TextBox_QtyMax, 2, 0);
            Control_AdvancedRemove_TableLayout_Quantity.Controls.Add(Control_AdvancedRemove_Label_QtyDash, 1, 0);
            Control_AdvancedRemove_TableLayout_Quantity.Dock = DockStyle.Fill;
            Control_AdvancedRemove_TableLayout_Quantity.Location = new Point(65, 119);
            Control_AdvancedRemove_TableLayout_Quantity.Name = "Control_AdvancedRemove_TableLayout_Quantity";
            Control_AdvancedRemove_TableLayout_Quantity.RowCount = 1;
            Control_AdvancedRemove_TableLayout_Quantity.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_AdvancedRemove_TableLayout_Quantity.Size = new Size(230, 29);
            Control_AdvancedRemove_TableLayout_Quantity.TabIndex = 9;
            // 
            // Control_AdvancedRemove_TextBox_QtyMin
            // 
            Control_AdvancedRemove_TextBox_QtyMin.Dock = DockStyle.Fill;
            Control_AdvancedRemove_TextBox_QtyMin.Font = new Font("Segoe UI Emoji", 9F);
            Control_AdvancedRemove_TextBox_QtyMin.Location = new Point(3, 3);
            Control_AdvancedRemove_TextBox_QtyMin.Name = "Control_AdvancedRemove_TextBox_QtyMin";
            Control_AdvancedRemove_TextBox_QtyMin.PlaceholderText = "Minimum Qty";
            Control_AdvancedRemove_TextBox_QtyMin.Size = new Size(100, 23);
            Control_AdvancedRemove_TextBox_QtyMin.TabIndex = 5;
            // 
            // Control_AdvancedRemove_TextBox_QtyMax
            // 
            Control_AdvancedRemove_TextBox_QtyMax.Dock = DockStyle.Fill;
            Control_AdvancedRemove_TextBox_QtyMax.Font = new Font("Segoe UI Emoji", 9F);
            Control_AdvancedRemove_TextBox_QtyMax.Location = new Point(127, 3);
            Control_AdvancedRemove_TextBox_QtyMax.Name = "Control_AdvancedRemove_TextBox_QtyMax";
            Control_AdvancedRemove_TextBox_QtyMax.PlaceholderText = "Maximum Qty";
            Control_AdvancedRemove_TextBox_QtyMax.Size = new Size(100, 23);
            Control_AdvancedRemove_TextBox_QtyMax.TabIndex = 6;
            // 
            // Control_AdvancedRemove_Label_QtyDash
            // 
            Control_AdvancedRemove_Label_QtyDash.AutoSize = true;
            Control_AdvancedRemove_Label_QtyDash.Dock = DockStyle.Fill;
            Control_AdvancedRemove_Label_QtyDash.Location = new Point(109, 3);
            Control_AdvancedRemove_Label_QtyDash.Margin = new Padding(3);
            Control_AdvancedRemove_Label_QtyDash.Name = "Control_AdvancedRemove_Label_QtyDash";
            Control_AdvancedRemove_Label_QtyDash.Size = new Size(12, 23);
            Control_AdvancedRemove_Label_QtyDash.TabIndex = 7;
            Control_AdvancedRemove_Label_QtyDash.Text = "-";
            Control_AdvancedRemove_Label_QtyDash.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_AdvancedRemove_Label_DateRange
            // 
            Control_AdvancedRemove_Label_DateRange.AutoSize = true;
            Control_AdvancedRemove_TableLayout_Inputs.SetColumnSpan(Control_AdvancedRemove_Label_DateRange, 2);
            Control_AdvancedRemove_Label_DateRange.Dock = DockStyle.Fill;
            Control_AdvancedRemove_Label_DateRange.Font = new Font("Segoe UI Emoji", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Control_AdvancedRemove_Label_DateRange.Location = new Point(3, 154);
            Control_AdvancedRemove_Label_DateRange.Margin = new Padding(3);
            Control_AdvancedRemove_Label_DateRange.Name = "Control_AdvancedRemove_Label_DateRange";
            Control_AdvancedRemove_Label_DateRange.Size = new Size(292, 17);
            Control_AdvancedRemove_Label_DateRange.TabIndex = 10;
            Control_AdvancedRemove_Label_DateRange.Text = "Date Range";
            Control_AdvancedRemove_Label_DateRange.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_AdvancedRemove_TableLayout_DateRange
            // 
            Control_AdvancedRemove_TableLayout_DateRange.AutoSize = true;
            Control_AdvancedRemove_TableLayout_DateRange.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_AdvancedRemove_TableLayout_DateRange.ColumnCount = 7;
            Control_AdvancedRemove_TableLayout_Inputs.SetColumnSpan(Control_AdvancedRemove_TableLayout_DateRange, 2);
            Control_AdvancedRemove_TableLayout_DateRange.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.9999943F));
            Control_AdvancedRemove_TableLayout_DateRange.ColumnStyles.Add(new ColumnStyle());
            Control_AdvancedRemove_TableLayout_DateRange.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25.0000019F));
            Control_AdvancedRemove_TableLayout_DateRange.ColumnStyles.Add(new ColumnStyle());
            Control_AdvancedRemove_TableLayout_DateRange.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25.0000019F));
            Control_AdvancedRemove_TableLayout_DateRange.ColumnStyles.Add(new ColumnStyle());
            Control_AdvancedRemove_TableLayout_DateRange.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25.0000019F));
            Control_AdvancedRemove_TableLayout_DateRange.Controls.Add(Control_AdvancedRemove_DateTimePicker_To, 5, 0);
            Control_AdvancedRemove_TableLayout_DateRange.Controls.Add(Control_AdvancedRemove_DateTimePicker_From, 1, 0);
            Control_AdvancedRemove_TableLayout_DateRange.Controls.Add(Control_AdvancedRemove_Label_DateDash, 2, 0);
            Control_AdvancedRemove_TableLayout_DateRange.Dock = DockStyle.Fill;
            Control_AdvancedRemove_TableLayout_DateRange.Location = new Point(3, 177);
            Control_AdvancedRemove_TableLayout_DateRange.Name = "Control_AdvancedRemove_TableLayout_DateRange";
            Control_AdvancedRemove_TableLayout_DateRange.RowCount = 1;
            Control_AdvancedRemove_TableLayout_DateRange.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_AdvancedRemove_TableLayout_DateRange.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            Control_AdvancedRemove_TableLayout_DateRange.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            Control_AdvancedRemove_TableLayout_DateRange.Size = new Size(292, 29);
            Control_AdvancedRemove_TableLayout_DateRange.TabIndex = 11;
            // 
            // Control_AdvancedRemove_DateTimePicker_To
            // 
            Control_AdvancedRemove_DateTimePicker_To.Dock = DockStyle.Fill;
            Control_AdvancedRemove_DateTimePicker_To.Font = new Font("Segoe UI Emoji", 9F);
            Control_AdvancedRemove_DateTimePicker_To.Format = DateTimePickerFormat.Short;
            Control_AdvancedRemove_DateTimePicker_To.Location = new Point(169, 3);
            Control_AdvancedRemove_DateTimePicker_To.MaximumSize = new Size(100, 23);
            Control_AdvancedRemove_DateTimePicker_To.MinimumSize = new Size(100, 23);
            Control_AdvancedRemove_DateTimePicker_To.Name = "Control_AdvancedRemove_DateTimePicker_To";
            Control_AdvancedRemove_DateTimePicker_To.Size = new Size(100, 23);
            Control_AdvancedRemove_DateTimePicker_To.TabIndex = 0;
            Control_AdvancedRemove_DateTimePicker_To.TabStop = false;
            // 
            // Control_AdvancedRemove_DateTimePicker_From
            // 
            Control_AdvancedRemove_DateTimePicker_From.Dock = DockStyle.Fill;
            Control_AdvancedRemove_DateTimePicker_From.Font = new Font("Segoe UI Emoji", 9F);
            Control_AdvancedRemove_DateTimePicker_From.Format = DateTimePickerFormat.Short;
            Control_AdvancedRemove_DateTimePicker_From.Location = new Point(21, 3);
            Control_AdvancedRemove_DateTimePicker_From.MaximumSize = new Size(100, 23);
            Control_AdvancedRemove_DateTimePicker_From.MinimumSize = new Size(100, 23);
            Control_AdvancedRemove_DateTimePicker_From.Name = "Control_AdvancedRemove_DateTimePicker_From";
            Control_AdvancedRemove_DateTimePicker_From.Size = new Size(100, 23);
            Control_AdvancedRemove_DateTimePicker_From.TabIndex = 1;
            Control_AdvancedRemove_DateTimePicker_From.TabStop = false;
            // 
            // Control_AdvancedRemove_Label_DateDash
            // 
            Control_AdvancedRemove_Label_DateDash.AutoSize = true;
            Control_AdvancedRemove_TableLayout_DateRange.SetColumnSpan(Control_AdvancedRemove_Label_DateDash, 3);
            Control_AdvancedRemove_Label_DateDash.Dock = DockStyle.Fill;
            Control_AdvancedRemove_Label_DateDash.Location = new Point(127, 3);
            Control_AdvancedRemove_Label_DateDash.Margin = new Padding(3);
            Control_AdvancedRemove_Label_DateDash.Name = "Control_AdvancedRemove_Label_DateDash";
            Control_AdvancedRemove_Label_DateDash.Size = new Size(36, 23);
            Control_AdvancedRemove_Label_DateDash.TabIndex = 2;
            Control_AdvancedRemove_Label_DateDash.Text = "-";
            Control_AdvancedRemove_Label_DateDash.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // TransactionSearchControl_TableLayout_QuickFilters
            // 
            TransactionSearchControl_TableLayout_QuickFilters.AutoSize = true;
            TransactionSearchControl_TableLayout_QuickFilters.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TransactionSearchControl_TableLayout_QuickFilters.ColumnCount = 7;
            Control_AdvancedRemove_TableLayout_Inputs.SetColumnSpan(TransactionSearchControl_TableLayout_QuickFilters, 2);
            TransactionSearchControl_TableLayout_QuickFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.9999924F));
            TransactionSearchControl_TableLayout_QuickFilters.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_QuickFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25.0000038F));
            TransactionSearchControl_TableLayout_QuickFilters.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_QuickFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25.0000038F));
            TransactionSearchControl_TableLayout_QuickFilters.ColumnStyles.Add(new ColumnStyle());
            TransactionSearchControl_TableLayout_QuickFilters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25.0000038F));
            TransactionSearchControl_TableLayout_QuickFilters.Controls.Add(Control_AdvancedRemove_RadioButton_Month, 5, 0);
            TransactionSearchControl_TableLayout_QuickFilters.Controls.Add(Control_AdvancedRemove_RadioButton_Today, 1, 0);
            TransactionSearchControl_TableLayout_QuickFilters.Controls.Add(Control_AdvancedRemove_RadioButton_Week, 1, 1);
            TransactionSearchControl_TableLayout_QuickFilters.Controls.Add(Control_AdvancedRemove_RadioButton_Everything, 3, 0);
            TransactionSearchControl_TableLayout_QuickFilters.Controls.Add(Control_AdvancedRemove_RadioButton_Custom, 5, 1);
            TransactionSearchControl_TableLayout_QuickFilters.Dock = DockStyle.Fill;
            TransactionSearchControl_TableLayout_QuickFilters.Location = new Point(3, 212);
            TransactionSearchControl_TableLayout_QuickFilters.Name = "TransactionSearchControl_TableLayout_QuickFilters";
            TransactionSearchControl_TableLayout_QuickFilters.RowCount = 1;
            TransactionSearchControl_TableLayout_QuickFilters.RowStyles.Add(new RowStyle());
            TransactionSearchControl_TableLayout_QuickFilters.RowStyles.Add(new RowStyle());
            TransactionSearchControl_TableLayout_QuickFilters.Size = new Size(292, 50);
            TransactionSearchControl_TableLayout_QuickFilters.TabIndex = 12;
            // 
            // Control_AdvancedRemove_RadioButton_Month
            // 
            Control_AdvancedRemove_RadioButton_Month.AutoSize = true;
            Control_AdvancedRemove_RadioButton_Month.Checked = true;
            Control_AdvancedRemove_RadioButton_Month.Dock = DockStyle.Left;
            Control_AdvancedRemove_RadioButton_Month.Location = new Point(202, 3);
            Control_AdvancedRemove_RadioButton_Month.Name = "Control_AdvancedRemove_RadioButton_Month";
            Control_AdvancedRemove_RadioButton_Month.Size = new Size(61, 19);
            Control_AdvancedRemove_RadioButton_Month.TabIndex = 10;
            Control_AdvancedRemove_RadioButton_Month.TabStop = true;
            Control_AdvancedRemove_RadioButton_Month.Text = "Month";
            Control_AdvancedRemove_RadioButton_Month.UseVisualStyleBackColor = true;
            // 
            // Control_AdvancedRemove_RadioButton_Today
            // 
            Control_AdvancedRemove_RadioButton_Today.AutoSize = true;
            Control_AdvancedRemove_RadioButton_Today.CheckAlign = ContentAlignment.MiddleRight;
            Control_AdvancedRemove_RadioButton_Today.Dock = DockStyle.Right;
            Control_AdvancedRemove_RadioButton_Today.Location = new Point(22, 3);
            Control_AdvancedRemove_RadioButton_Today.Name = "Control_AdvancedRemove_RadioButton_Today";
            Control_AdvancedRemove_RadioButton_Today.Size = new Size(57, 19);
            Control_AdvancedRemove_RadioButton_Today.TabIndex = 8;
            Control_AdvancedRemove_RadioButton_Today.Text = "Today";
            Control_AdvancedRemove_RadioButton_Today.UseVisualStyleBackColor = true;
            // 
            // Control_AdvancedRemove_RadioButton_Week
            // 
            Control_AdvancedRemove_RadioButton_Week.AutoSize = true;
            Control_AdvancedRemove_RadioButton_Week.CheckAlign = ContentAlignment.MiddleRight;
            Control_AdvancedRemove_RadioButton_Week.Dock = DockStyle.Right;
            Control_AdvancedRemove_RadioButton_Week.Location = new Point(25, 28);
            Control_AdvancedRemove_RadioButton_Week.Name = "Control_AdvancedRemove_RadioButton_Week";
            Control_AdvancedRemove_RadioButton_Week.Size = new Size(54, 19);
            Control_AdvancedRemove_RadioButton_Week.TabIndex = 9;
            Control_AdvancedRemove_RadioButton_Week.Text = "Week";
            Control_AdvancedRemove_RadioButton_Week.UseVisualStyleBackColor = true;
            // 
            // Control_AdvancedRemove_RadioButton_Everything
            // 
            Control_AdvancedRemove_RadioButton_Everything.AutoSize = true;
            Control_AdvancedRemove_RadioButton_Everything.CheckAlign = ContentAlignment.BottomCenter;
            Control_AdvancedRemove_RadioButton_Everything.Dock = DockStyle.Fill;
            Control_AdvancedRemove_RadioButton_Everything.Location = new Point(104, 3);
            Control_AdvancedRemove_RadioButton_Everything.Name = "Control_AdvancedRemove_RadioButton_Everything";
            Control_AdvancedRemove_RadioButton_Everything.Padding = new Padding(3, 0, 3, 3);
            TransactionSearchControl_TableLayout_QuickFilters.SetRowSpan(Control_AdvancedRemove_RadioButton_Everything, 2);
            Control_AdvancedRemove_RadioButton_Everything.Size = new Size(73, 44);
            Control_AdvancedRemove_RadioButton_Everything.TabIndex = 12;
            Control_AdvancedRemove_RadioButton_Everything.Text = "Everything";
            Control_AdvancedRemove_RadioButton_Everything.TextAlign = ContentAlignment.MiddleCenter;
            Control_AdvancedRemove_RadioButton_Everything.UseVisualStyleBackColor = true;
            // 
            // Control_AdvancedRemove_RadioButton_Custom
            // 
            Control_AdvancedRemove_RadioButton_Custom.AutoSize = true;
            Control_AdvancedRemove_RadioButton_Custom.Dock = DockStyle.Left;
            Control_AdvancedRemove_RadioButton_Custom.Location = new Point(202, 28);
            Control_AdvancedRemove_RadioButton_Custom.Name = "Control_AdvancedRemove_RadioButton_Custom";
            Control_AdvancedRemove_RadioButton_Custom.Size = new Size(67, 19);
            Control_AdvancedRemove_RadioButton_Custom.TabIndex = 11;
            Control_AdvancedRemove_RadioButton_Custom.Text = "Custom";
            Control_AdvancedRemove_RadioButton_Custom.UseVisualStyleBackColor = true;
            // 
            // Control_AdvancedRemove_SuggestionBox_User
            // 
            Control_AdvancedRemove_SuggestionBox_User.AutoSize = true;
            Control_AdvancedRemove_SuggestionBox_User.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_AdvancedRemove_TableLayout_Inputs.SetColumnSpan(Control_AdvancedRemove_SuggestionBox_User, 2);
            Control_AdvancedRemove_SuggestionBox_User.Dock = DockStyle.Fill;
            Control_AdvancedRemove_SuggestionBox_User.LabelText = "User";
            Control_AdvancedRemove_SuggestionBox_User.LabelVisibility = Enum_LabelVisibility.Hidden;
            Control_AdvancedRemove_SuggestionBox_User.Location = new Point(3, 284);
            Control_AdvancedRemove_SuggestionBox_User.MaxLength = 130;
            Control_AdvancedRemove_SuggestionBox_User.MinimumSize = new Size(0, 23);
            Control_AdvancedRemove_SuggestionBox_User.MinLength = 130;
            Control_AdvancedRemove_SuggestionBox_User.Name = "Control_AdvancedRemove_SuggestionBox_User";
            Control_AdvancedRemove_SuggestionBox_User.PlaceholderText = "Select or Enter User";
            Control_AdvancedRemove_SuggestionBox_User.Size = new Size(292, 23);
            Control_AdvancedRemove_SuggestionBox_User.TabIndex = 7;
            // 
            // Control_AdvancedRemove
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(Control_AdvancedRemove_GroupBox_Main);
            Name = "Control_AdvancedRemove";
            Size = new Size(800, 434);
            Control_AdvancedRemove_GroupBox_Main.ResumeLayout(false);
            Control_AdvancedRemove_TableLayout_Main.ResumeLayout(false);
            Control_AdvancedRemove_TableLayout_Main.PerformLayout();
            Control_AdvancedRemove_TableLayout_Bottom_Buttons.ResumeLayout(false);
            Control_AdvancedRemove_TableLayout_Bottom_Buttons.PerformLayout();
            Control_AdvancedRemove_TableLayout_TopRow.ResumeLayout(false);
            Control_AdvancedRemove_TableLayout_TopRow.PerformLayout();
            Control_AdvancedRemove_Panel_DGV.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Control_AdvancedRemove_Image_NothingFound).EndInit();
            ((System.ComponentModel.ISupportInitialize)Control_AdvancedRemove_DataGridView_Results).EndInit();
            Control_AdvancedRemove_Panel_Inputs.ResumeLayout(false);
            Control_AdvancedRemove_Panel_Inputs.PerformLayout();
            Control_AdvancedRemove_TableLayout_Inputs.ResumeLayout(false);
            Control_AdvancedRemove_TableLayout_Inputs.PerformLayout();
            Control_AdvancedRemove_TableLayout_Buttons_Left.ResumeLayout(false);
            Control_AdvancedRemove_TableLayout_Buttons_Left.PerformLayout();
            Control_AdvancedRemove_TableLayout_Quantity.ResumeLayout(false);
            Control_AdvancedRemove_TableLayout_Quantity.PerformLayout();
            Control_AdvancedRemove_TableLayout_DateRange.ResumeLayout(false);
            Control_AdvancedRemove_TableLayout_DateRange.PerformLayout();
            TransactionSearchControl_TableLayout_QuickFilters.ResumeLayout(false);
            TransactionSearchControl_TableLayout_QuickFilters.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
        private TextBox Control_AdvancedRemove_TextBox_Location;
        private TextBox Control_AdvancedRemove_TextBox_Part;
        private TextBox Control_AdvancedRemove_TextBox_Operation;
        private Label Control_AdvancedRemove_Label_Part;
        private TableLayoutPanel Control_AdvancedRemove_TableLayout_Quantity;
        private TableLayoutPanel Control_AdvancedRemove_TableLayout_DateRange;
        private TableLayoutPanel Control_AdvancedRemove_TableLayout_Buttons_Left;
        private TableLayoutPanel Control_AdvancedRemove_TableLayout_TopRow;
        private Panel Control_AdvancedRemove_Panel_Inputs;
        private TableLayoutPanel Control_AdvancedRemove_TableLayout_Bottom_Buttons;
        private Button Control_AdvancedRemove_Button_Normal;
        private Button Control_AdvancedRemove_Button_Print;
        private Button Control_AdvancedRemove_Button_Reset;
        private Label Control_AdvancedRemove_Label_DateRange;
        private TableLayoutPanel TransactionSearchControl_TableLayout_QuickFilters;
        private RadioButton Control_AdvancedRemove_RadioButton_Custom;
        private RadioButton Control_AdvancedRemove_RadioButton_Month;
        private RadioButton Control_AdvancedRemove_RadioButton_Week;
        private RadioButton Control_AdvancedRemove_RadioButton_Today;
        private RadioButton Control_AdvancedRemove_RadioButton_Everything;
        private Button Control_AdvancedRemove_Button_QuickButtonToggle;
        private Button Control_AdvancedRemove_Button_SearchVisual;
        private Button MainForm_Button_Help_AdvancedRemove;
    }


        #endregion
    }
