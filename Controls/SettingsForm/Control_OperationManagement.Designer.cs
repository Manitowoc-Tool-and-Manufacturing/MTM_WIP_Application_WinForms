using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Components.Shared;

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    partial class Control_OperationManagement
    {
        #region Fields

        private IContainer components = null!;
        private TableLayoutPanel Control_OperationManagement_TableLayout_Main = null!;
        private Label Control_OperationManagement_Label_Header = null!;
        private Label Control_OperationManagement_Label_Subtitle = null!;
        private Panel Control_OperationManagement_Panel_Container = null!;
        
        private Panel Control_OperationManagement_Panel_Home = null!;
        private TableLayoutPanel Control_OperationManagement_TableLayout_Home = null!;
        private Panel Control_OperationManagement_Panel_HomeTile_Add = null!;
        private TableLayoutPanel Control_OperationManagement_TableLayout_HomeTile_Add = null!;
        private Label Control_OperationManagement_Label_HomeTile_AddIcon = null!;
        private Label Control_OperationManagement_Label_HomeTile_AddTitle = null!;
        private Label Control_OperationManagement_Label_HomeTile_AddInstruction = null!;
        private Panel Control_OperationManagement_Panel_HomeTile_Edit = null!;
        private TableLayoutPanel Control_OperationManagement_TableLayout_HomeTile_Edit = null!;
        private Label Control_OperationManagement_Label_HomeTile_EditIcon = null!;
        private Label Control_OperationManagement_Label_HomeTile_EditTitle = null!;
        private Label Control_OperationManagement_Label_HomeTile_EditInstruction = null!;
        private Panel Control_OperationManagement_Panel_HomeTile_Remove = null!;
        private TableLayoutPanel Control_OperationManagement_TableLayout_HomeTile_Remove = null!;
        private Label Control_OperationManagement_Label_HomeTile_RemoveIcon = null!;
        private Label Control_OperationManagement_Label_HomeTile_RemoveTitle = null!;
        private Label Control_OperationManagement_Label_HomeTile_RemoveInstruction = null!;
        
        private TableLayoutPanel Control_OperationManagement_TableLayout_Cards = null!;
        private TableLayoutPanel Control_OperationManagement_TableLayout_BackButton = null!;
        private Button Control_OperationManagement_Button_Back = null!;

        private Panel Control_OperationManagement_Panel_AddCard = null!;
        private TableLayoutPanel Control_OperationManagement_TableLayout_Add = null!;
        private Label Control_OperationManagement_Label_AddIcon = null!;
        private Label Control_OperationManagement_Label_AddTitle = null!;
        private TableLayoutPanel Control_OperationManagement_TableLayout_AddContent = null!;
        private Component_SuggestionTextBoxWithLabel Control_OperationManagement_TextBox_AddOperation = null!;
        private Label Control_OperationManagement_Label_AddIssuedBy = null!;
        private Label Control_OperationManagement_Label_AddIssuedByValue = null!;
        private TableLayoutPanel Control_OperationManagement_TableLayout_AddActions = null!;
        private Button Control_OperationManagement_Button_AddSave = null!;
        private Button Control_OperationManagement_Button_AddClear = null!;

        private Panel Control_OperationManagement_Panel_EditCard = null!;
        private TableLayoutPanel Control_OperationManagement_TableLayout_Edit = null!;
        private Label Control_OperationManagement_Label_EditIcon = null!;
        private Label Control_OperationManagement_Label_EditTitle = null!;
        private TableLayoutPanel Control_OperationManagement_TableLayout_EditContent = null!;
        private Component_SuggestionTextBoxWithLabel Control_OperationManagement_Suggestion_EditSelectOperation = null!;
        private Component_SuggestionTextBoxWithLabel Control_OperationManagement_TextBox_EditNewOperation = null!;
        private Label Control_OperationManagement_Label_EditIssuedBy = null!;
        private Label Control_OperationManagement_Label_EditIssuedByValue = null!;
        private TableLayoutPanel Control_OperationManagement_TableLayout_EditActions = null!;
        private Button Control_OperationManagement_Button_EditSave = null!;
        private Button Control_OperationManagement_Button_EditReset = null!;

        private Panel Control_OperationManagement_Panel_RemoveCard = null!;
        private TableLayoutPanel Control_OperationManagement_TableLayout_Remove = null!;
        private Label Control_OperationManagement_Label_RemoveIcon = null!;
        private Label Control_OperationManagement_Label_RemoveTitle = null!;
        private TableLayoutPanel Control_OperationManagement_TableLayout_RemoveContent = null!;
        private Component_SuggestionTextBoxWithLabel Control_OperationManagement_Suggestion_RemoveSelectOperation = null!;
        private TableLayoutPanel Control_OperationManagement_TableLayout_RemoveDetails = null!;
        private Label Control_OperationManagement_Label_RemoveOperation = null!;
        private Label Control_OperationManagement_Label_RemoveOperationValue = null!;
        private Label Control_OperationManagement_Label_RemoveIssuedBy = null!;
        private Label Control_OperationManagement_Label_RemoveIssuedByValue = null!;
        private Label Control_OperationManagement_Label_RemoveWarning = null!;
        private TableLayoutPanel Control_OperationManagement_TableLayout_RemoveActions = null!;
        private Button Control_OperationManagement_Button_RemoveConfirm = null!;
        private Button Control_OperationManagement_Button_RemoveCancel = null!;

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

        private void InitializeComponent()
        {
            Control_OperationManagement_TableLayout_Main = new TableLayoutPanel();
            Control_OperationManagement_Label_Header = new Label();
            Control_OperationManagement_Label_Subtitle = new Label();
            Control_OperationManagement_TableLayout_BackButton = new TableLayoutPanel();
            SettingsForm_Button_Help_Operations = new Button();
            Control_OperationManagement_Button_Home = new Button();
            Control_OperationManagement_Button_Back = new Button();
            Control_OperationManagement_Panel_Container = new Panel();
            Control_OperationManagement_Panel_Home = new Panel();
            Control_OperationManagement_TableLayout_Home = new TableLayoutPanel();
            Control_OperationManagement_Panel_HomeTile_Add = new Panel();
            Control_OperationManagement_TableLayout_HomeTile_Add = new TableLayoutPanel();
            Control_OperationManagement_Label_HomeTile_AddIcon = new Label();
            Control_OperationManagement_Label_HomeTile_AddTitle = new Label();
            Control_OperationManagement_Label_HomeTile_AddInstruction = new Label();
            Control_OperationManagement_Panel_HomeTile_Edit = new Panel();
            Control_OperationManagement_TableLayout_HomeTile_Edit = new TableLayoutPanel();
            Control_OperationManagement_Label_HomeTile_EditIcon = new Label();
            Control_OperationManagement_Label_HomeTile_EditTitle = new Label();
            Control_OperationManagement_Label_HomeTile_EditInstruction = new Label();
            Control_OperationManagement_Panel_HomeTile_Remove = new Panel();
            Control_OperationManagement_TableLayout_HomeTile_Remove = new TableLayoutPanel();
            Control_OperationManagement_Label_HomeTile_RemoveIcon = new Label();
            Control_OperationManagement_Label_HomeTile_RemoveTitle = new Label();
            Control_OperationManagement_Label_HomeTile_RemoveInstruction = new Label();
            Control_OperationManagement_TableLayout_Cards = new TableLayoutPanel();
            Control_OperationManagement_Panel_AddCard = new Panel();
            Control_OperationManagement_TableLayout_Add = new TableLayoutPanel();
            Control_OperationManagement_TableLayout_AddHeader = new TableLayoutPanel();
            Control_OperationManagement_Label_AddTitle = new Label();
            Control_OperationManagement_Label_AddIcon = new Label();
            Control_OperationManagement_TableLayout_AddContent = new TableLayoutPanel();
            Control_OperationManagement_TextBox_AddOperation = new Component_SuggestionTextBoxWithLabel();
            Control_OperationManagement_TableLayout_AddActions = new TableLayoutPanel();
            Control_OperationManagement_Button_AddSave = new Button();
            Control_OperationManagement_Button_AddClear = new Button();
            Control_OperationManagement_Panel_EditCard = new Panel();
            Control_OperationManagement_TableLayout_Edit = new TableLayoutPanel();
            Control_OperationManagement_TableLayout_EditHeader = new TableLayoutPanel();
            Control_OperationManagement_Label_EditIcon = new Label();
            Control_OperationManagement_Label_EditTitle = new Label();
            Control_OperationManagement_TableLayout_EditContent = new TableLayoutPanel();
            Control_OperationManagement_Suggestion_EditSelectOperation = new Component_SuggestionTextBoxWithLabel();
            Control_OperationManagement_TextBox_EditNewOperation = new Component_SuggestionTextBoxWithLabel();
            Control_OperationManagement_TableLayout_EditActions = new TableLayoutPanel();
            Control_OperationManagement_Button_EditSave = new Button();
            Control_OperationManagement_Button_EditReset = new Button();
            Control_OperationManagement_Panel_RemoveCard = new Panel();
            Control_OperationManagement_TableLayout_Remove = new TableLayoutPanel();
            Control_OperationManagement_TableLayout_RemoveHeader = new TableLayoutPanel();
            Control_OperationManagement_Label_RemoveTitle = new Label();
            Control_OperationManagement_Label_RemoveIcon = new Label();
            Control_OperationManagement_TableLayout_RemoveContent = new TableLayoutPanel();
            Control_OperationManagement_Suggestion_RemoveSelectOperation = new Component_SuggestionTextBoxWithLabel();
            Control_OperationManagement_TableLayout_RemoveDetails = new TableLayoutPanel();
            Control_OperationManagement_Label_RemoveOperation = new Label();
            Control_OperationManagement_Label_RemoveOperationValue = new Label();
            Control_OperationManagement_Label_RemoveIssuedBy = new Label();
            Control_OperationManagement_Label_RemoveIssuedByValue = new Label();
            Control_OperationManagement_Label_RemoveWarning = new Label();
            Control_OperationManagement_TableLayout_RemoveActions = new TableLayoutPanel();
            Control_OperationManagement_Button_RemoveConfirm = new Button();
            Control_OperationManagement_Button_RemoveCancel = new Button();
            Control_OperationManagement_Label_AddIssuedBy = new Label();
            Control_OperationManagement_Label_AddIssuedByValue = new Label();
            Control_OperationManagement_Label_EditIssuedBy = new Label();
            Control_OperationManagement_Label_EditIssuedByValue = new Label();
            Control_OperationManagement_TableLayout_Main.SuspendLayout();
            Control_OperationManagement_TableLayout_BackButton.SuspendLayout();
            Control_OperationManagement_Panel_Container.SuspendLayout();
            Control_OperationManagement_Panel_Home.SuspendLayout();
            Control_OperationManagement_TableLayout_Home.SuspendLayout();
            Control_OperationManagement_Panel_HomeTile_Add.SuspendLayout();
            Control_OperationManagement_TableLayout_HomeTile_Add.SuspendLayout();
            Control_OperationManagement_Panel_HomeTile_Edit.SuspendLayout();
            Control_OperationManagement_TableLayout_HomeTile_Edit.SuspendLayout();
            Control_OperationManagement_Panel_HomeTile_Remove.SuspendLayout();
            Control_OperationManagement_TableLayout_HomeTile_Remove.SuspendLayout();
            Control_OperationManagement_TableLayout_Cards.SuspendLayout();
            Control_OperationManagement_Panel_AddCard.SuspendLayout();
            Control_OperationManagement_TableLayout_Add.SuspendLayout();
            Control_OperationManagement_TableLayout_AddHeader.SuspendLayout();
            Control_OperationManagement_TableLayout_AddContent.SuspendLayout();
            Control_OperationManagement_TableLayout_AddActions.SuspendLayout();
            Control_OperationManagement_Panel_EditCard.SuspendLayout();
            Control_OperationManagement_TableLayout_Edit.SuspendLayout();
            Control_OperationManagement_TableLayout_EditHeader.SuspendLayout();
            Control_OperationManagement_TableLayout_EditContent.SuspendLayout();
            Control_OperationManagement_TableLayout_EditActions.SuspendLayout();
            Control_OperationManagement_Panel_RemoveCard.SuspendLayout();
            Control_OperationManagement_TableLayout_Remove.SuspendLayout();
            Control_OperationManagement_TableLayout_RemoveHeader.SuspendLayout();
            Control_OperationManagement_TableLayout_RemoveContent.SuspendLayout();
            Control_OperationManagement_TableLayout_RemoveDetails.SuspendLayout();
            Control_OperationManagement_TableLayout_RemoveActions.SuspendLayout();
            SuspendLayout();
            // 
            // Control_OperationManagement_TableLayout_Main
            // 
            Control_OperationManagement_TableLayout_Main.AutoSize = true;
            Control_OperationManagement_TableLayout_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_Main.ColumnCount = 1;
            Control_OperationManagement_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            Control_OperationManagement_TableLayout_Main.Controls.Add(Control_OperationManagement_Label_Header, 0, 0);
            Control_OperationManagement_TableLayout_Main.Controls.Add(Control_OperationManagement_Label_Subtitle, 0, 1);
            Control_OperationManagement_TableLayout_Main.Controls.Add(Control_OperationManagement_TableLayout_BackButton, 0, 3);
            Control_OperationManagement_TableLayout_Main.Controls.Add(Control_OperationManagement_Panel_Container, 0, 2);
            Control_OperationManagement_TableLayout_Main.Dock = DockStyle.Fill;
            Control_OperationManagement_TableLayout_Main.Location = new Point(0, 0);
            Control_OperationManagement_TableLayout_Main.Name = "Control_OperationManagement_TableLayout_Main";
            Control_OperationManagement_TableLayout_Main.Padding = new Padding(20);
            Control_OperationManagement_TableLayout_Main.RowCount = 4;
            Control_OperationManagement_TableLayout_Main.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_Main.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_Main.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_Main.Size = new Size(492, 383);
            Control_OperationManagement_TableLayout_Main.TabIndex = 0;
            // 
            // Control_OperationManagement_Label_Header
            // 
            Control_OperationManagement_Label_Header.AutoSize = true;
            Control_OperationManagement_Label_Header.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_Header.Font = new Font("Segoe UI Emoji", 20F, FontStyle.Bold);
            Control_OperationManagement_Label_Header.ForeColor = Color.FromArgb(45, 45, 45);
            Control_OperationManagement_Label_Header.Location = new Point(23, 23);
            Control_OperationManagement_Label_Header.Margin = new Padding(3);
            Control_OperationManagement_Label_Header.Name = "Control_OperationManagement_Label_Header";
            Control_OperationManagement_Label_Header.Size = new Size(446, 36);
            Control_OperationManagement_Label_Header.TabIndex = 0;
            Control_OperationManagement_Label_Header.Text = "Operation Codes";
            // 
            // Control_OperationManagement_Label_Subtitle
            // 
            Control_OperationManagement_Label_Subtitle.AutoSize = true;
            Control_OperationManagement_Label_Subtitle.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_Subtitle.Font = new Font("Segoe UI Emoji", 10F);
            Control_OperationManagement_Label_Subtitle.ForeColor = Color.FromArgb(90, 90, 90);
            Control_OperationManagement_Label_Subtitle.Location = new Point(23, 65);
            Control_OperationManagement_Label_Subtitle.Margin = new Padding(3);
            Control_OperationManagement_Label_Subtitle.Name = "Control_OperationManagement_Label_Subtitle";
            Control_OperationManagement_Label_Subtitle.Size = new Size(446, 19);
            Control_OperationManagement_Label_Subtitle.TabIndex = 1;
            Control_OperationManagement_Label_Subtitle.Text = "Select an action below to manage operation codes.";
            // 
            // Control_OperationManagement_TableLayout_BackButton
            // 
            Control_OperationManagement_TableLayout_BackButton.AutoSize = true;
            Control_OperationManagement_TableLayout_BackButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_BackButton.ColumnCount = 4;
            Control_OperationManagement_TableLayout_BackButton.ColumnStyles.Add(new ColumnStyle());
            Control_OperationManagement_TableLayout_BackButton.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_BackButton.ColumnStyles.Add(new ColumnStyle());
            Control_OperationManagement_TableLayout_BackButton.ColumnStyles.Add(new ColumnStyle());
            Control_OperationManagement_TableLayout_BackButton.Controls.Add(SettingsForm_Button_Help_Operations, 3, 0);
            Control_OperationManagement_TableLayout_BackButton.Controls.Add(Control_OperationManagement_Button_Home, 2, 0);
            Control_OperationManagement_TableLayout_BackButton.Controls.Add(Control_OperationManagement_Button_Back, 0, 0);
            Control_OperationManagement_TableLayout_BackButton.Dock = DockStyle.Fill;
            Control_OperationManagement_TableLayout_BackButton.Location = new Point(23, 322);
            Control_OperationManagement_TableLayout_BackButton.Name = "Control_OperationManagement_TableLayout_BackButton";
            Control_OperationManagement_TableLayout_BackButton.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_BackButton.Size = new Size(446, 38);
            Control_OperationManagement_TableLayout_BackButton.TabIndex = 2;
            // 
            // SettingsForm_Button_Help_Operations
            // 
            SettingsForm_Button_Help_Operations.AutoSize = true;
            SettingsForm_Button_Help_Operations.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            SettingsForm_Button_Help_Operations.Dock = DockStyle.Fill;
            SettingsForm_Button_Help_Operations.Location = new Point(411, 3);
            SettingsForm_Button_Help_Operations.MaximumSize = new Size(32, 32);
            SettingsForm_Button_Help_Operations.MinimumSize = new Size(32, 32);
            SettingsForm_Button_Help_Operations.Name = "SettingsForm_Button_Help_Operations";
            SettingsForm_Button_Help_Operations.Size = new Size(32, 32);
            SettingsForm_Button_Help_Operations.TabIndex = 16;
            SettingsForm_Button_Help_Operations.Text = "?";
            SettingsForm_Button_Help_Operations.UseVisualStyleBackColor = true;
            // 
            // Control_OperationManagement_Button_Home
            // 
            Control_OperationManagement_Button_Home.AutoSize = true;
            Control_OperationManagement_Button_Home.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_Button_Home.BackColor = Color.FromArgb(0, 120, 212);
            Control_OperationManagement_Button_Home.Cursor = Cursors.Hand;
            Control_OperationManagement_Button_Home.Dock = DockStyle.Fill;
            Control_OperationManagement_Button_Home.FlatStyle = FlatStyle.Flat;
            Control_OperationManagement_Button_Home.Font = new Font("Segoe UI Semibold", 10F);
            Control_OperationManagement_Button_Home.ForeColor = Color.White;
            Control_OperationManagement_Button_Home.Location = new Point(274, 3);
            Control_OperationManagement_Button_Home.Name = "Control_OperationManagement_Button_Home";
            Control_OperationManagement_Button_Home.Size = new Size(131, 32);
            Control_OperationManagement_Button_Home.TabIndex = 2;
            Control_OperationManagement_Button_Home.Text = "🏠 Back to Home";
            Control_OperationManagement_Button_Home.UseVisualStyleBackColor = false;
            // 
            // Control_OperationManagement_Button_Back
            // 
            Control_OperationManagement_Button_Back.AutoSize = true;
            Control_OperationManagement_Button_Back.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_Button_Back.BackColor = Color.FromArgb(96, 94, 92);
            Control_OperationManagement_Button_Back.Cursor = Cursors.Hand;
            Control_OperationManagement_Button_Back.Dock = DockStyle.Fill;
            Control_OperationManagement_Button_Back.FlatStyle = FlatStyle.Flat;
            Control_OperationManagement_Button_Back.Font = new Font("Segoe UI Semibold", 10F);
            Control_OperationManagement_Button_Back.ForeColor = Color.White;
            Control_OperationManagement_Button_Back.Location = new Point(3, 3);
            Control_OperationManagement_Button_Back.Name = "Control_OperationManagement_Button_Back";
            Control_OperationManagement_Button_Back.Size = new Size(147, 32);
            Control_OperationManagement_Button_Back.TabIndex = 0;
            Control_OperationManagement_Button_Back.Text = "⬅ Back to Selection";
            Control_OperationManagement_Button_Back.UseVisualStyleBackColor = false;
            Control_OperationManagement_Button_Back.Visible = false;
            // 
            // Control_OperationManagement_Panel_Container
            // 
            Control_OperationManagement_Panel_Container.Controls.Add(Control_OperationManagement_Panel_Home);
            Control_OperationManagement_Panel_Container.Controls.Add(Control_OperationManagement_TableLayout_Cards);
            Control_OperationManagement_Panel_Container.Dock = DockStyle.Fill;
            Control_OperationManagement_Panel_Container.Location = new Point(23, 90);
            Control_OperationManagement_Panel_Container.Name = "Control_OperationManagement_Panel_Container";
            Control_OperationManagement_Panel_Container.Size = new Size(446, 226);
            Control_OperationManagement_Panel_Container.TabIndex = 2;
            // 
            // Control_OperationManagement_Panel_Home
            // 
            Control_OperationManagement_Panel_Home.Controls.Add(Control_OperationManagement_TableLayout_Home);
            Control_OperationManagement_Panel_Home.Dock = DockStyle.Fill;
            Control_OperationManagement_Panel_Home.Location = new Point(0, 0);
            Control_OperationManagement_Panel_Home.Name = "Control_OperationManagement_Panel_Home";
            Control_OperationManagement_Panel_Home.Size = new Size(446, 226);
            Control_OperationManagement_Panel_Home.TabIndex = 0;
            // 
            // Control_OperationManagement_TableLayout_Home
            // 
            Control_OperationManagement_TableLayout_Home.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_Home.ColumnCount = 3;
            Control_OperationManagement_TableLayout_Home.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            Control_OperationManagement_TableLayout_Home.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            Control_OperationManagement_TableLayout_Home.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
            Control_OperationManagement_TableLayout_Home.Controls.Add(Control_OperationManagement_Panel_HomeTile_Add, 0, 0);
            Control_OperationManagement_TableLayout_Home.Controls.Add(Control_OperationManagement_Panel_HomeTile_Edit, 1, 0);
            Control_OperationManagement_TableLayout_Home.Controls.Add(Control_OperationManagement_Panel_HomeTile_Remove, 2, 0);
            Control_OperationManagement_TableLayout_Home.Dock = DockStyle.Fill;
            Control_OperationManagement_TableLayout_Home.Location = new Point(0, 0);
            Control_OperationManagement_TableLayout_Home.Name = "Control_OperationManagement_TableLayout_Home";
            Control_OperationManagement_TableLayout_Home.RowCount = 1;
            Control_OperationManagement_TableLayout_Home.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_Home.Size = new Size(446, 226);
            Control_OperationManagement_TableLayout_Home.TabIndex = 0;
            // 
            // Control_OperationManagement_Panel_HomeTile_Add
            // 
            Control_OperationManagement_Panel_HomeTile_Add.AutoSize = true;
            Control_OperationManagement_Panel_HomeTile_Add.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_Panel_HomeTile_Add.BorderStyle = BorderStyle.FixedSingle;
            Control_OperationManagement_Panel_HomeTile_Add.Controls.Add(Control_OperationManagement_TableLayout_HomeTile_Add);
            Control_OperationManagement_Panel_HomeTile_Add.Cursor = Cursors.Hand;
            Control_OperationManagement_Panel_HomeTile_Add.Dock = DockStyle.Fill;
            Control_OperationManagement_Panel_HomeTile_Add.Location = new Point(3, 3);
            Control_OperationManagement_Panel_HomeTile_Add.Name = "Control_OperationManagement_Panel_HomeTile_Add";
            Control_OperationManagement_Panel_HomeTile_Add.Size = new Size(142, 220);
            Control_OperationManagement_Panel_HomeTile_Add.TabIndex = 0;
            // 
            // Control_OperationManagement_TableLayout_HomeTile_Add
            // 
            Control_OperationManagement_TableLayout_HomeTile_Add.AutoSize = true;
            Control_OperationManagement_TableLayout_HomeTile_Add.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_HomeTile_Add.ColumnCount = 1;
            Control_OperationManagement_TableLayout_HomeTile_Add.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_HomeTile_Add.Controls.Add(Control_OperationManagement_Label_HomeTile_AddIcon, 0, 0);
            Control_OperationManagement_TableLayout_HomeTile_Add.Controls.Add(Control_OperationManagement_Label_HomeTile_AddTitle, 0, 1);
            Control_OperationManagement_TableLayout_HomeTile_Add.Controls.Add(Control_OperationManagement_Label_HomeTile_AddInstruction, 0, 3);
            Control_OperationManagement_TableLayout_HomeTile_Add.Dock = DockStyle.Fill;
            Control_OperationManagement_TableLayout_HomeTile_Add.Location = new Point(0, 0);
            Control_OperationManagement_TableLayout_HomeTile_Add.Name = "Control_OperationManagement_TableLayout_HomeTile_Add";
            Control_OperationManagement_TableLayout_HomeTile_Add.Padding = new Padding(3);
            Control_OperationManagement_TableLayout_HomeTile_Add.RowCount = 5;
            Control_OperationManagement_TableLayout_HomeTile_Add.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_HomeTile_Add.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_HomeTile_Add.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Control_OperationManagement_TableLayout_HomeTile_Add.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_HomeTile_Add.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Control_OperationManagement_TableLayout_HomeTile_Add.Size = new Size(140, 218);
            Control_OperationManagement_TableLayout_HomeTile_Add.TabIndex = 0;
            // 
            // Control_OperationManagement_Label_HomeTile_AddIcon
            // 
            Control_OperationManagement_Label_HomeTile_AddIcon.AutoSize = true;
            Control_OperationManagement_Label_HomeTile_AddIcon.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_HomeTile_AddIcon.Font = new Font("Segoe UI Emoji", 48F);
            Control_OperationManagement_Label_HomeTile_AddIcon.Location = new Point(6, 6);
            Control_OperationManagement_Label_HomeTile_AddIcon.Margin = new Padding(3);
            Control_OperationManagement_Label_HomeTile_AddIcon.Name = "Control_OperationManagement_Label_HomeTile_AddIcon";
            Control_OperationManagement_Label_HomeTile_AddIcon.Size = new Size(128, 85);
            Control_OperationManagement_Label_HomeTile_AddIcon.TabIndex = 0;
            Control_OperationManagement_Label_HomeTile_AddIcon.Text = "🆕";
            Control_OperationManagement_Label_HomeTile_AddIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_OperationManagement_Label_HomeTile_AddTitle
            // 
            Control_OperationManagement_Label_HomeTile_AddTitle.AutoSize = true;
            Control_OperationManagement_Label_HomeTile_AddTitle.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_HomeTile_AddTitle.Font = new Font("Segoe UI Emoji", 14F, FontStyle.Bold);
            Control_OperationManagement_Label_HomeTile_AddTitle.Location = new Point(6, 97);
            Control_OperationManagement_Label_HomeTile_AddTitle.Margin = new Padding(3);
            Control_OperationManagement_Label_HomeTile_AddTitle.Name = "Control_OperationManagement_Label_HomeTile_AddTitle";
            Control_OperationManagement_Label_HomeTile_AddTitle.Size = new Size(128, 52);
            Control_OperationManagement_Label_HomeTile_AddTitle.TabIndex = 1;
            Control_OperationManagement_Label_HomeTile_AddTitle.Text = "Add\r\nOperation";
            Control_OperationManagement_Label_HomeTile_AddTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_OperationManagement_Label_HomeTile_AddInstruction
            // 
            Control_OperationManagement_Label_HomeTile_AddInstruction.AutoSize = true;
            Control_OperationManagement_Label_HomeTile_AddInstruction.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_HomeTile_AddInstruction.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Italic);
            Control_OperationManagement_Label_HomeTile_AddInstruction.ForeColor = Color.FromArgb(100, 100, 100);
            Control_OperationManagement_Label_HomeTile_AddInstruction.Location = new Point(6, 167);
            Control_OperationManagement_Label_HomeTile_AddInstruction.Margin = new Padding(3);
            Control_OperationManagement_Label_HomeTile_AddInstruction.Name = "Control_OperationManagement_Label_HomeTile_AddInstruction";
            Control_OperationManagement_Label_HomeTile_AddInstruction.Size = new Size(128, 32);
            Control_OperationManagement_Label_HomeTile_AddInstruction.TabIndex = 2;
            Control_OperationManagement_Label_HomeTile_AddInstruction.Text = "Click to add new operation codes";
            Control_OperationManagement_Label_HomeTile_AddInstruction.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_OperationManagement_Panel_HomeTile_Edit
            // 
            Control_OperationManagement_Panel_HomeTile_Edit.AutoSize = true;
            Control_OperationManagement_Panel_HomeTile_Edit.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_Panel_HomeTile_Edit.BorderStyle = BorderStyle.FixedSingle;
            Control_OperationManagement_Panel_HomeTile_Edit.Controls.Add(Control_OperationManagement_TableLayout_HomeTile_Edit);
            Control_OperationManagement_Panel_HomeTile_Edit.Cursor = Cursors.Hand;
            Control_OperationManagement_Panel_HomeTile_Edit.Dock = DockStyle.Fill;
            Control_OperationManagement_Panel_HomeTile_Edit.Location = new Point(151, 3);
            Control_OperationManagement_Panel_HomeTile_Edit.Name = "Control_OperationManagement_Panel_HomeTile_Edit";
            Control_OperationManagement_Panel_HomeTile_Edit.Size = new Size(142, 220);
            Control_OperationManagement_Panel_HomeTile_Edit.TabIndex = 1;
            // 
            // Control_OperationManagement_TableLayout_HomeTile_Edit
            // 
            Control_OperationManagement_TableLayout_HomeTile_Edit.AutoSize = true;
            Control_OperationManagement_TableLayout_HomeTile_Edit.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_HomeTile_Edit.ColumnCount = 1;
            Control_OperationManagement_TableLayout_HomeTile_Edit.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_HomeTile_Edit.Controls.Add(Control_OperationManagement_Label_HomeTile_EditIcon, 0, 0);
            Control_OperationManagement_TableLayout_HomeTile_Edit.Controls.Add(Control_OperationManagement_Label_HomeTile_EditTitle, 0, 1);
            Control_OperationManagement_TableLayout_HomeTile_Edit.Controls.Add(Control_OperationManagement_Label_HomeTile_EditInstruction, 0, 3);
            Control_OperationManagement_TableLayout_HomeTile_Edit.Dock = DockStyle.Fill;
            Control_OperationManagement_TableLayout_HomeTile_Edit.Location = new Point(0, 0);
            Control_OperationManagement_TableLayout_HomeTile_Edit.Name = "Control_OperationManagement_TableLayout_HomeTile_Edit";
            Control_OperationManagement_TableLayout_HomeTile_Edit.Padding = new Padding(3);
            Control_OperationManagement_TableLayout_HomeTile_Edit.RowCount = 5;
            Control_OperationManagement_TableLayout_HomeTile_Edit.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_HomeTile_Edit.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_HomeTile_Edit.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Control_OperationManagement_TableLayout_HomeTile_Edit.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_HomeTile_Edit.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Control_OperationManagement_TableLayout_HomeTile_Edit.Size = new Size(140, 218);
            Control_OperationManagement_TableLayout_HomeTile_Edit.TabIndex = 0;
            // 
            // Control_OperationManagement_Label_HomeTile_EditIcon
            // 
            Control_OperationManagement_Label_HomeTile_EditIcon.AutoSize = true;
            Control_OperationManagement_Label_HomeTile_EditIcon.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_HomeTile_EditIcon.Font = new Font("Segoe UI Emoji", 48F);
            Control_OperationManagement_Label_HomeTile_EditIcon.Location = new Point(6, 6);
            Control_OperationManagement_Label_HomeTile_EditIcon.Margin = new Padding(3);
            Control_OperationManagement_Label_HomeTile_EditIcon.Name = "Control_OperationManagement_Label_HomeTile_EditIcon";
            Control_OperationManagement_Label_HomeTile_EditIcon.Size = new Size(128, 85);
            Control_OperationManagement_Label_HomeTile_EditIcon.TabIndex = 0;
            Control_OperationManagement_Label_HomeTile_EditIcon.Text = "✏️";
            Control_OperationManagement_Label_HomeTile_EditIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_OperationManagement_Label_HomeTile_EditTitle
            // 
            Control_OperationManagement_Label_HomeTile_EditTitle.AutoSize = true;
            Control_OperationManagement_Label_HomeTile_EditTitle.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_HomeTile_EditTitle.Font = new Font("Segoe UI Emoji", 14F, FontStyle.Bold);
            Control_OperationManagement_Label_HomeTile_EditTitle.Location = new Point(6, 97);
            Control_OperationManagement_Label_HomeTile_EditTitle.Margin = new Padding(3);
            Control_OperationManagement_Label_HomeTile_EditTitle.Name = "Control_OperationManagement_Label_HomeTile_EditTitle";
            Control_OperationManagement_Label_HomeTile_EditTitle.Size = new Size(128, 52);
            Control_OperationManagement_Label_HomeTile_EditTitle.TabIndex = 1;
            Control_OperationManagement_Label_HomeTile_EditTitle.Text = "Edit\r\nOperation";
            Control_OperationManagement_Label_HomeTile_EditTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_OperationManagement_Label_HomeTile_EditInstruction
            // 
            Control_OperationManagement_Label_HomeTile_EditInstruction.AutoSize = true;
            Control_OperationManagement_Label_HomeTile_EditInstruction.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_HomeTile_EditInstruction.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Italic);
            Control_OperationManagement_Label_HomeTile_EditInstruction.ForeColor = Color.FromArgb(100, 100, 100);
            Control_OperationManagement_Label_HomeTile_EditInstruction.Location = new Point(6, 167);
            Control_OperationManagement_Label_HomeTile_EditInstruction.Margin = new Padding(3);
            Control_OperationManagement_Label_HomeTile_EditInstruction.Name = "Control_OperationManagement_Label_HomeTile_EditInstruction";
            Control_OperationManagement_Label_HomeTile_EditInstruction.Size = new Size(128, 32);
            Control_OperationManagement_Label_HomeTile_EditInstruction.TabIndex = 2;
            Control_OperationManagement_Label_HomeTile_EditInstruction.Text = "Click to edit existing operations";
            Control_OperationManagement_Label_HomeTile_EditInstruction.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_OperationManagement_Panel_HomeTile_Remove
            // 
            Control_OperationManagement_Panel_HomeTile_Remove.AutoSize = true;
            Control_OperationManagement_Panel_HomeTile_Remove.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_Panel_HomeTile_Remove.BorderStyle = BorderStyle.FixedSingle;
            Control_OperationManagement_Panel_HomeTile_Remove.Controls.Add(Control_OperationManagement_TableLayout_HomeTile_Remove);
            Control_OperationManagement_Panel_HomeTile_Remove.Cursor = Cursors.Hand;
            Control_OperationManagement_Panel_HomeTile_Remove.Dock = DockStyle.Fill;
            Control_OperationManagement_Panel_HomeTile_Remove.Location = new Point(299, 3);
            Control_OperationManagement_Panel_HomeTile_Remove.Name = "Control_OperationManagement_Panel_HomeTile_Remove";
            Control_OperationManagement_Panel_HomeTile_Remove.Size = new Size(144, 220);
            Control_OperationManagement_Panel_HomeTile_Remove.TabIndex = 2;
            // 
            // Control_OperationManagement_TableLayout_HomeTile_Remove
            // 
            Control_OperationManagement_TableLayout_HomeTile_Remove.AutoSize = true;
            Control_OperationManagement_TableLayout_HomeTile_Remove.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_HomeTile_Remove.ColumnCount = 1;
            Control_OperationManagement_TableLayout_HomeTile_Remove.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_HomeTile_Remove.Controls.Add(Control_OperationManagement_Label_HomeTile_RemoveIcon, 0, 0);
            Control_OperationManagement_TableLayout_HomeTile_Remove.Controls.Add(Control_OperationManagement_Label_HomeTile_RemoveTitle, 0, 1);
            Control_OperationManagement_TableLayout_HomeTile_Remove.Controls.Add(Control_OperationManagement_Label_HomeTile_RemoveInstruction, 0, 3);
            Control_OperationManagement_TableLayout_HomeTile_Remove.Dock = DockStyle.Fill;
            Control_OperationManagement_TableLayout_HomeTile_Remove.Location = new Point(0, 0);
            Control_OperationManagement_TableLayout_HomeTile_Remove.Name = "Control_OperationManagement_TableLayout_HomeTile_Remove";
            Control_OperationManagement_TableLayout_HomeTile_Remove.Padding = new Padding(3);
            Control_OperationManagement_TableLayout_HomeTile_Remove.RowCount = 5;
            Control_OperationManagement_TableLayout_HomeTile_Remove.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_HomeTile_Remove.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_HomeTile_Remove.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Control_OperationManagement_TableLayout_HomeTile_Remove.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_HomeTile_Remove.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Control_OperationManagement_TableLayout_HomeTile_Remove.Size = new Size(142, 218);
            Control_OperationManagement_TableLayout_HomeTile_Remove.TabIndex = 0;
            // 
            // Control_OperationManagement_Label_HomeTile_RemoveIcon
            // 
            Control_OperationManagement_Label_HomeTile_RemoveIcon.AutoSize = true;
            Control_OperationManagement_Label_HomeTile_RemoveIcon.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_HomeTile_RemoveIcon.Font = new Font("Segoe UI Emoji", 48F);
            Control_OperationManagement_Label_HomeTile_RemoveIcon.Location = new Point(6, 6);
            Control_OperationManagement_Label_HomeTile_RemoveIcon.Margin = new Padding(3);
            Control_OperationManagement_Label_HomeTile_RemoveIcon.Name = "Control_OperationManagement_Label_HomeTile_RemoveIcon";
            Control_OperationManagement_Label_HomeTile_RemoveIcon.Size = new Size(130, 85);
            Control_OperationManagement_Label_HomeTile_RemoveIcon.TabIndex = 0;
            Control_OperationManagement_Label_HomeTile_RemoveIcon.Text = "🗑️";
            Control_OperationManagement_Label_HomeTile_RemoveIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_OperationManagement_Label_HomeTile_RemoveTitle
            // 
            Control_OperationManagement_Label_HomeTile_RemoveTitle.AutoSize = true;
            Control_OperationManagement_Label_HomeTile_RemoveTitle.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_HomeTile_RemoveTitle.Font = new Font("Segoe UI Emoji", 14F, FontStyle.Bold);
            Control_OperationManagement_Label_HomeTile_RemoveTitle.Location = new Point(6, 97);
            Control_OperationManagement_Label_HomeTile_RemoveTitle.Margin = new Padding(3);
            Control_OperationManagement_Label_HomeTile_RemoveTitle.Name = "Control_OperationManagement_Label_HomeTile_RemoveTitle";
            Control_OperationManagement_Label_HomeTile_RemoveTitle.Size = new Size(130, 52);
            Control_OperationManagement_Label_HomeTile_RemoveTitle.TabIndex = 1;
            Control_OperationManagement_Label_HomeTile_RemoveTitle.Text = "Remove\r\nOperation";
            Control_OperationManagement_Label_HomeTile_RemoveTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_OperationManagement_Label_HomeTile_RemoveInstruction
            // 
            Control_OperationManagement_Label_HomeTile_RemoveInstruction.AutoSize = true;
            Control_OperationManagement_Label_HomeTile_RemoveInstruction.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_HomeTile_RemoveInstruction.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Italic);
            Control_OperationManagement_Label_HomeTile_RemoveInstruction.ForeColor = Color.FromArgb(100, 100, 100);
            Control_OperationManagement_Label_HomeTile_RemoveInstruction.Location = new Point(6, 167);
            Control_OperationManagement_Label_HomeTile_RemoveInstruction.Margin = new Padding(3);
            Control_OperationManagement_Label_HomeTile_RemoveInstruction.Name = "Control_OperationManagement_Label_HomeTile_RemoveInstruction";
            Control_OperationManagement_Label_HomeTile_RemoveInstruction.Size = new Size(130, 32);
            Control_OperationManagement_Label_HomeTile_RemoveInstruction.TabIndex = 2;
            Control_OperationManagement_Label_HomeTile_RemoveInstruction.Text = "Click to remove operation codes";
            Control_OperationManagement_Label_HomeTile_RemoveInstruction.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_OperationManagement_TableLayout_Cards
            // 
            Control_OperationManagement_TableLayout_Cards.AutoSize = true;
            Control_OperationManagement_TableLayout_Cards.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_Cards.ColumnCount = 1;
            Control_OperationManagement_TableLayout_Cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_Cards.Controls.Add(Control_OperationManagement_Panel_AddCard, 0, 0);
            Control_OperationManagement_TableLayout_Cards.Controls.Add(Control_OperationManagement_Panel_EditCard, 0, 1);
            Control_OperationManagement_TableLayout_Cards.Controls.Add(Control_OperationManagement_Panel_RemoveCard, 0, 2);
            Control_OperationManagement_TableLayout_Cards.Dock = DockStyle.Fill;
            Control_OperationManagement_TableLayout_Cards.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            Control_OperationManagement_TableLayout_Cards.Location = new Point(0, 0);
            Control_OperationManagement_TableLayout_Cards.Name = "Control_OperationManagement_TableLayout_Cards";
            Control_OperationManagement_TableLayout_Cards.RowCount = 3;
            Control_OperationManagement_TableLayout_Cards.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_Cards.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_Cards.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_Cards.Size = new Size(446, 226);
            Control_OperationManagement_TableLayout_Cards.TabIndex = 1;
            Control_OperationManagement_TableLayout_Cards.Visible = false;
            // 
            // Control_OperationManagement_Panel_AddCard
            // 
            Control_OperationManagement_Panel_AddCard.AutoSize = true;
            Control_OperationManagement_Panel_AddCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_Panel_AddCard.BorderStyle = BorderStyle.FixedSingle;
            Control_OperationManagement_Panel_AddCard.Controls.Add(Control_OperationManagement_TableLayout_Add);
            Control_OperationManagement_Panel_AddCard.Dock = DockStyle.Fill;
            Control_OperationManagement_Panel_AddCard.Location = new Point(3, 3);
            Control_OperationManagement_Panel_AddCard.Name = "Control_OperationManagement_Panel_AddCard";
            Control_OperationManagement_Panel_AddCard.Size = new Size(440, 184);
            Control_OperationManagement_Panel_AddCard.TabIndex = 0;
            // 
            // Control_OperationManagement_TableLayout_Add
            // 
            Control_OperationManagement_TableLayout_Add.AutoSize = true;
            Control_OperationManagement_TableLayout_Add.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_Add.ColumnCount = 1;
            Control_OperationManagement_TableLayout_Add.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_Add.Controls.Add(Control_OperationManagement_TableLayout_AddHeader, 0, 0);
            Control_OperationManagement_TableLayout_Add.Controls.Add(Control_OperationManagement_TableLayout_AddContent, 0, 1);
            Control_OperationManagement_TableLayout_Add.Dock = DockStyle.Fill;
            Control_OperationManagement_TableLayout_Add.Location = new Point(0, 0);
            Control_OperationManagement_TableLayout_Add.Name = "Control_OperationManagement_TableLayout_Add";
            Control_OperationManagement_TableLayout_Add.Padding = new Padding(16);
            Control_OperationManagement_TableLayout_Add.RowCount = 2;
            Control_OperationManagement_TableLayout_Add.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_Add.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_Add.Size = new Size(438, 182);
            Control_OperationManagement_TableLayout_Add.TabIndex = 0;
            // 
            // Control_OperationManagement_TableLayout_AddHeader
            // 
            Control_OperationManagement_TableLayout_AddHeader.AutoSize = true;
            Control_OperationManagement_TableLayout_AddHeader.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_AddHeader.ColumnCount = 2;
            Control_OperationManagement_TableLayout_AddHeader.ColumnStyles.Add(new ColumnStyle());
            Control_OperationManagement_TableLayout_AddHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_AddHeader.Controls.Add(Control_OperationManagement_Label_AddTitle, 1, 0);
            Control_OperationManagement_TableLayout_AddHeader.Controls.Add(Control_OperationManagement_Label_AddIcon, 0, 0);
            Control_OperationManagement_TableLayout_AddHeader.Dock = DockStyle.Fill;
            Control_OperationManagement_TableLayout_AddHeader.Location = new Point(19, 19);
            Control_OperationManagement_TableLayout_AddHeader.Name = "Control_OperationManagement_TableLayout_AddHeader";
            Control_OperationManagement_TableLayout_AddHeader.RowCount = 1;
            Control_OperationManagement_TableLayout_AddHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_AddHeader.Size = new Size(400, 57);
            Control_OperationManagement_TableLayout_AddHeader.TabIndex = 0;
            // 
            // Control_OperationManagement_Label_AddTitle
            // 
            Control_OperationManagement_Label_AddTitle.AutoSize = true;
            Control_OperationManagement_Label_AddTitle.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_AddTitle.Font = new Font("Segoe UI Emoji", 16F, FontStyle.Bold);
            Control_OperationManagement_Label_AddTitle.Location = new Point(83, 3);
            Control_OperationManagement_Label_AddTitle.Margin = new Padding(3);
            Control_OperationManagement_Label_AddTitle.Name = "Control_OperationManagement_Label_AddTitle";
            Control_OperationManagement_Label_AddTitle.Size = new Size(314, 51);
            Control_OperationManagement_Label_AddTitle.TabIndex = 1;
            Control_OperationManagement_Label_AddTitle.Text = "Add Operation";
            Control_OperationManagement_Label_AddTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_OperationManagement_Label_AddIcon
            // 
            Control_OperationManagement_Label_AddIcon.AutoSize = true;
            Control_OperationManagement_Label_AddIcon.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_AddIcon.Font = new Font("Segoe UI Emoji", 28F);
            Control_OperationManagement_Label_AddIcon.Location = new Point(3, 3);
            Control_OperationManagement_Label_AddIcon.Margin = new Padding(3);
            Control_OperationManagement_Label_AddIcon.Name = "Control_OperationManagement_Label_AddIcon";
            Control_OperationManagement_Label_AddIcon.Size = new Size(74, 51);
            Control_OperationManagement_Label_AddIcon.TabIndex = 0;
            Control_OperationManagement_Label_AddIcon.Text = "🆕";
            Control_OperationManagement_Label_AddIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_OperationManagement_TableLayout_AddContent
            // 
            Control_OperationManagement_TableLayout_AddContent.AutoSize = true;
            Control_OperationManagement_TableLayout_AddContent.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_AddContent.ColumnCount = 1;
            Control_OperationManagement_TableLayout_AddContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_AddContent.Controls.Add(Control_OperationManagement_TextBox_AddOperation, 0, 0);
            Control_OperationManagement_TableLayout_AddContent.Controls.Add(Control_OperationManagement_TableLayout_AddActions, 0, 1);
            Control_OperationManagement_TableLayout_AddContent.Dock = DockStyle.Fill;
            Control_OperationManagement_TableLayout_AddContent.Location = new Point(19, 82);
            Control_OperationManagement_TableLayout_AddContent.Name = "Control_OperationManagement_TableLayout_AddContent";
            Control_OperationManagement_TableLayout_AddContent.RowCount = 2;
            Control_OperationManagement_TableLayout_AddContent.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_AddContent.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_AddContent.Size = new Size(400, 81);
            Control_OperationManagement_TableLayout_AddContent.TabIndex = 2;
            // 
            // Control_OperationManagement_TextBox_AddOperation
            // 
            Control_OperationManagement_TextBox_AddOperation.AutoSize = true;
            Control_OperationManagement_TextBox_AddOperation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TextBox_AddOperation.BorderStyle = BorderStyle.FixedSingle;
            Control_OperationManagement_TextBox_AddOperation.Dock = DockStyle.Fill;
            Control_OperationManagement_TextBox_AddOperation.EnableSuggestions = false;
            Control_OperationManagement_TextBox_AddOperation.Location = new Point(3, 3);
            Control_OperationManagement_TextBox_AddOperation.MaxLength = 130;
            Control_OperationManagement_TextBox_AddOperation.MinimumSize = new Size(0, 23);
            Control_OperationManagement_TextBox_AddOperation.MinLength = 130;
            Control_OperationManagement_TextBox_AddOperation.Name = "Control_OperationManagement_TextBox_AddOperation";
            Control_OperationManagement_TextBox_AddOperation.Padding = new Padding(3);
            Control_OperationManagement_TextBox_AddOperation.ShowF4Button = false;
            Control_OperationManagement_TextBox_AddOperation.Size = new Size(394, 31);
            Control_OperationManagement_TextBox_AddOperation.TabIndex = 0;
            // 
            // Control_OperationManagement_TableLayout_AddActions
            // 
            Control_OperationManagement_TableLayout_AddActions.AutoSize = true;
            Control_OperationManagement_TableLayout_AddActions.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_AddActions.ColumnCount = 3;
            Control_OperationManagement_TableLayout_AddActions.ColumnStyles.Add(new ColumnStyle());
            Control_OperationManagement_TableLayout_AddActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_AddActions.ColumnStyles.Add(new ColumnStyle());
            Control_OperationManagement_TableLayout_AddActions.Controls.Add(Control_OperationManagement_Button_AddSave, 0, 0);
            Control_OperationManagement_TableLayout_AddActions.Controls.Add(Control_OperationManagement_Button_AddClear, 2, 0);
            Control_OperationManagement_TableLayout_AddActions.Dock = DockStyle.Fill;
            Control_OperationManagement_TableLayout_AddActions.Location = new Point(3, 40);
            Control_OperationManagement_TableLayout_AddActions.Name = "Control_OperationManagement_TableLayout_AddActions";
            Control_OperationManagement_TableLayout_AddActions.RowCount = 1;
            Control_OperationManagement_TableLayout_AddActions.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_AddActions.Size = new Size(394, 38);
            Control_OperationManagement_TableLayout_AddActions.TabIndex = 1;
            // 
            // Control_OperationManagement_Button_AddSave
            // 
            Control_OperationManagement_Button_AddSave.AutoSize = true;
            Control_OperationManagement_Button_AddSave.BackColor = Color.FromArgb(16, 124, 16);
            Control_OperationManagement_Button_AddSave.Cursor = Cursors.Hand;
            Control_OperationManagement_Button_AddSave.FlatStyle = FlatStyle.Flat;
            Control_OperationManagement_Button_AddSave.Font = new Font("Segoe UI Semibold", 10F);
            Control_OperationManagement_Button_AddSave.ForeColor = Color.White;
            Control_OperationManagement_Button_AddSave.Location = new Point(3, 3);
            Control_OperationManagement_Button_AddSave.Name = "Control_OperationManagement_Button_AddSave";
            Control_OperationManagement_Button_AddSave.Size = new Size(140, 32);
            Control_OperationManagement_Button_AddSave.TabIndex = 0;
            Control_OperationManagement_Button_AddSave.Text = "💾 Save";
            Control_OperationManagement_Button_AddSave.UseVisualStyleBackColor = false;
            // 
            // Control_OperationManagement_Button_AddClear
            // 
            Control_OperationManagement_Button_AddClear.AutoSize = true;
            Control_OperationManagement_Button_AddClear.BackColor = Color.FromArgb(96, 94, 92);
            Control_OperationManagement_Button_AddClear.Cursor = Cursors.Hand;
            Control_OperationManagement_Button_AddClear.FlatStyle = FlatStyle.Flat;
            Control_OperationManagement_Button_AddClear.Font = new Font("Segoe UI Semibold", 10F);
            Control_OperationManagement_Button_AddClear.ForeColor = Color.White;
            Control_OperationManagement_Button_AddClear.Location = new Point(291, 3);
            Control_OperationManagement_Button_AddClear.Name = "Control_OperationManagement_Button_AddClear";
            Control_OperationManagement_Button_AddClear.Size = new Size(100, 32);
            Control_OperationManagement_Button_AddClear.TabIndex = 1;
            Control_OperationManagement_Button_AddClear.Text = "Clear";
            Control_OperationManagement_Button_AddClear.UseVisualStyleBackColor = false;
            // 
            // Control_OperationManagement_Panel_EditCard
            // 
            Control_OperationManagement_Panel_EditCard.AutoSize = true;
            Control_OperationManagement_Panel_EditCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_Panel_EditCard.BorderStyle = BorderStyle.FixedSingle;
            Control_OperationManagement_Panel_EditCard.Controls.Add(Control_OperationManagement_TableLayout_Edit);
            Control_OperationManagement_Panel_EditCard.Dock = DockStyle.Fill;
            Control_OperationManagement_Panel_EditCard.Location = new Point(3, 193);
            Control_OperationManagement_Panel_EditCard.Name = "Control_OperationManagement_Panel_EditCard";
            Control_OperationManagement_Panel_EditCard.Size = new Size(440, 234);
            Control_OperationManagement_Panel_EditCard.TabIndex = 1;
            // 
            // Control_OperationManagement_TableLayout_Edit
            // 
            Control_OperationManagement_TableLayout_Edit.AutoSize = true;
            Control_OperationManagement_TableLayout_Edit.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_Edit.ColumnCount = 1;
            Control_OperationManagement_TableLayout_Edit.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_Edit.Controls.Add(Control_OperationManagement_TableLayout_EditHeader, 0, 0);
            Control_OperationManagement_TableLayout_Edit.Controls.Add(Control_OperationManagement_TableLayout_EditContent, 0, 1);
            Control_OperationManagement_TableLayout_Edit.Dock = DockStyle.Fill;
            Control_OperationManagement_TableLayout_Edit.Location = new Point(0, 0);
            Control_OperationManagement_TableLayout_Edit.Name = "Control_OperationManagement_TableLayout_Edit";
            Control_OperationManagement_TableLayout_Edit.Padding = new Padding(16);
            Control_OperationManagement_TableLayout_Edit.RowCount = 2;
            Control_OperationManagement_TableLayout_Edit.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_Edit.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_Edit.Size = new Size(438, 232);
            Control_OperationManagement_TableLayout_Edit.TabIndex = 0;
            // 
            // Control_OperationManagement_TableLayout_EditHeader
            // 
            Control_OperationManagement_TableLayout_EditHeader.AutoSize = true;
            Control_OperationManagement_TableLayout_EditHeader.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_EditHeader.ColumnCount = 2;
            Control_OperationManagement_TableLayout_EditHeader.ColumnStyles.Add(new ColumnStyle());
            Control_OperationManagement_TableLayout_EditHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_EditHeader.Controls.Add(Control_OperationManagement_Label_EditIcon, 0, 0);
            Control_OperationManagement_TableLayout_EditHeader.Controls.Add(Control_OperationManagement_Label_EditTitle, 1, 0);
            Control_OperationManagement_TableLayout_EditHeader.Dock = DockStyle.Fill;
            Control_OperationManagement_TableLayout_EditHeader.Location = new Point(19, 19);
            Control_OperationManagement_TableLayout_EditHeader.Name = "Control_OperationManagement_TableLayout_EditHeader";
            Control_OperationManagement_TableLayout_EditHeader.RowCount = 1;
            Control_OperationManagement_TableLayout_EditHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_EditHeader.Size = new Size(400, 57);
            Control_OperationManagement_TableLayout_EditHeader.TabIndex = 0;
            // 
            // Control_OperationManagement_Label_EditIcon
            // 
            Control_OperationManagement_Label_EditIcon.AutoSize = true;
            Control_OperationManagement_Label_EditIcon.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_EditIcon.Font = new Font("Segoe UI Emoji", 28F);
            Control_OperationManagement_Label_EditIcon.Location = new Point(3, 3);
            Control_OperationManagement_Label_EditIcon.Margin = new Padding(3);
            Control_OperationManagement_Label_EditIcon.Name = "Control_OperationManagement_Label_EditIcon";
            Control_OperationManagement_Label_EditIcon.Size = new Size(74, 51);
            Control_OperationManagement_Label_EditIcon.TabIndex = 0;
            Control_OperationManagement_Label_EditIcon.Text = "✏️";
            Control_OperationManagement_Label_EditIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_OperationManagement_Label_EditTitle
            // 
            Control_OperationManagement_Label_EditTitle.AutoSize = true;
            Control_OperationManagement_Label_EditTitle.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_EditTitle.Font = new Font("Segoe UI Emoji", 16F, FontStyle.Bold);
            Control_OperationManagement_Label_EditTitle.Location = new Point(83, 3);
            Control_OperationManagement_Label_EditTitle.Margin = new Padding(3);
            Control_OperationManagement_Label_EditTitle.Name = "Control_OperationManagement_Label_EditTitle";
            Control_OperationManagement_Label_EditTitle.Size = new Size(314, 51);
            Control_OperationManagement_Label_EditTitle.TabIndex = 1;
            Control_OperationManagement_Label_EditTitle.Text = "Edit Operation";
            Control_OperationManagement_Label_EditTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_OperationManagement_TableLayout_EditContent
            // 
            Control_OperationManagement_TableLayout_EditContent.AutoSize = true;
            Control_OperationManagement_TableLayout_EditContent.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_EditContent.ColumnCount = 1;
            Control_OperationManagement_TableLayout_EditContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_EditContent.Controls.Add(Control_OperationManagement_Suggestion_EditSelectOperation, 0, 0);
            Control_OperationManagement_TableLayout_EditContent.Controls.Add(Control_OperationManagement_TextBox_EditNewOperation, 0, 1);
            Control_OperationManagement_TableLayout_EditContent.Controls.Add(Control_OperationManagement_TableLayout_EditActions, 0, 2);
            Control_OperationManagement_TableLayout_EditContent.Dock = DockStyle.Fill;
            Control_OperationManagement_TableLayout_EditContent.Location = new Point(19, 82);
            Control_OperationManagement_TableLayout_EditContent.Name = "Control_OperationManagement_TableLayout_EditContent";
            Control_OperationManagement_TableLayout_EditContent.RowCount = 3;
            Control_OperationManagement_TableLayout_EditContent.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_EditContent.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_EditContent.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_EditContent.Size = new Size(400, 131);
            Control_OperationManagement_TableLayout_EditContent.TabIndex = 2;
            // 
            // Control_OperationManagement_Suggestion_EditSelectOperation
            // 
            Control_OperationManagement_Suggestion_EditSelectOperation.AutoSize = true;
            Control_OperationManagement_Suggestion_EditSelectOperation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_Suggestion_EditSelectOperation.BorderStyle = BorderStyle.FixedSingle;
            Control_OperationManagement_Suggestion_EditSelectOperation.Dock = DockStyle.Fill;
            Control_OperationManagement_Suggestion_EditSelectOperation.LabelText = "Select Operation";
            Control_OperationManagement_Suggestion_EditSelectOperation.Location = new Point(3, 3);
            Control_OperationManagement_Suggestion_EditSelectOperation.Margin = new Padding(3, 3, 3, 10);
            Control_OperationManagement_Suggestion_EditSelectOperation.MaxLength = 130;
            Control_OperationManagement_Suggestion_EditSelectOperation.MinimumSize = new Size(0, 23);
            Control_OperationManagement_Suggestion_EditSelectOperation.MinLength = 130;
            Control_OperationManagement_Suggestion_EditSelectOperation.Name = "Control_OperationManagement_Suggestion_EditSelectOperation";
            Control_OperationManagement_Suggestion_EditSelectOperation.Padding = new Padding(3);
            Control_OperationManagement_Suggestion_EditSelectOperation.PlaceholderText = "Search operations (F4)";
            Control_OperationManagement_Suggestion_EditSelectOperation.Size = new Size(394, 31);
            Control_OperationManagement_Suggestion_EditSelectOperation.TabIndex = 0;
            // 
            // Control_OperationManagement_TextBox_EditNewOperation
            // 
            Control_OperationManagement_TextBox_EditNewOperation.AutoSize = true;
            Control_OperationManagement_TextBox_EditNewOperation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TextBox_EditNewOperation.BorderStyle = BorderStyle.FixedSingle;
            Control_OperationManagement_TextBox_EditNewOperation.Dock = DockStyle.Fill;
            Control_OperationManagement_TextBox_EditNewOperation.EnableSuggestions = false;
            Control_OperationManagement_TextBox_EditNewOperation.LabelText = "New Operation Code";
            Control_OperationManagement_TextBox_EditNewOperation.Location = new Point(3, 47);
            Control_OperationManagement_TextBox_EditNewOperation.Margin = new Padding(3, 3, 3, 10);
            Control_OperationManagement_TextBox_EditNewOperation.MaxLength = 130;
            Control_OperationManagement_TextBox_EditNewOperation.MinimumSize = new Size(0, 23);
            Control_OperationManagement_TextBox_EditNewOperation.MinLength = 130;
            Control_OperationManagement_TextBox_EditNewOperation.Name = "Control_OperationManagement_TextBox_EditNewOperation";
            Control_OperationManagement_TextBox_EditNewOperation.Padding = new Padding(3);
            Control_OperationManagement_TextBox_EditNewOperation.PlaceholderText = "Enter new operation code";
            Control_OperationManagement_TextBox_EditNewOperation.ShowF4Button = false;
            Control_OperationManagement_TextBox_EditNewOperation.Size = new Size(394, 31);
            Control_OperationManagement_TextBox_EditNewOperation.TabIndex = 1;
            // 
            // Control_OperationManagement_TableLayout_EditActions
            // 
            Control_OperationManagement_TableLayout_EditActions.AutoSize = true;
            Control_OperationManagement_TableLayout_EditActions.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_EditActions.ColumnCount = 3;
            Control_OperationManagement_TableLayout_EditActions.ColumnStyles.Add(new ColumnStyle());
            Control_OperationManagement_TableLayout_EditActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_EditActions.ColumnStyles.Add(new ColumnStyle());
            Control_OperationManagement_TableLayout_EditActions.Controls.Add(Control_OperationManagement_Button_EditSave, 0, 0);
            Control_OperationManagement_TableLayout_EditActions.Controls.Add(Control_OperationManagement_Button_EditReset, 2, 0);
            Control_OperationManagement_TableLayout_EditActions.Dock = DockStyle.Fill;
            Control_OperationManagement_TableLayout_EditActions.Location = new Point(3, 91);
            Control_OperationManagement_TableLayout_EditActions.Name = "Control_OperationManagement_TableLayout_EditActions";
            Control_OperationManagement_TableLayout_EditActions.RowCount = 1;
            Control_OperationManagement_TableLayout_EditActions.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_EditActions.Size = new Size(394, 37);
            Control_OperationManagement_TableLayout_EditActions.TabIndex = 2;
            // 
            // Control_OperationManagement_Button_EditSave
            // 
            Control_OperationManagement_Button_EditSave.AutoSize = true;
            Control_OperationManagement_Button_EditSave.BackColor = Color.FromArgb(16, 124, 16);
            Control_OperationManagement_Button_EditSave.Cursor = Cursors.Hand;
            Control_OperationManagement_Button_EditSave.FlatStyle = FlatStyle.Flat;
            Control_OperationManagement_Button_EditSave.Location = new Point(3, 3);
            Control_OperationManagement_Button_EditSave.Name = "Control_OperationManagement_Button_EditSave";
            Control_OperationManagement_Button_EditSave.Size = new Size(75, 23);
            Control_OperationManagement_Button_EditSave.TabIndex = 0;
            Control_OperationManagement_Button_EditSave.UseVisualStyleBackColor = false;
            // 
            // Control_OperationManagement_Button_EditReset
            // 
            Control_OperationManagement_Button_EditReset.AutoSize = true;
            Control_OperationManagement_Button_EditReset.BackColor = Color.FromArgb(96, 94, 92);
            Control_OperationManagement_Button_EditReset.Cursor = Cursors.Hand;
            Control_OperationManagement_Button_EditReset.FlatStyle = FlatStyle.Flat;
            Control_OperationManagement_Button_EditReset.Font = new Font("Segoe UI Semibold", 10F);
            Control_OperationManagement_Button_EditReset.ForeColor = Color.White;
            Control_OperationManagement_Button_EditReset.Location = new Point(316, 3);
            Control_OperationManagement_Button_EditReset.Name = "Control_OperationManagement_Button_EditReset";
            Control_OperationManagement_Button_EditReset.Size = new Size(75, 31);
            Control_OperationManagement_Button_EditReset.TabIndex = 1;
            Control_OperationManagement_Button_EditReset.Text = "Reset";
            Control_OperationManagement_Button_EditReset.UseVisualStyleBackColor = false;
            // 
            // Control_OperationManagement_Panel_RemoveCard
            // 
            Control_OperationManagement_Panel_RemoveCard.AutoSize = true;
            Control_OperationManagement_Panel_RemoveCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_Panel_RemoveCard.BorderStyle = BorderStyle.FixedSingle;
            Control_OperationManagement_Panel_RemoveCard.Controls.Add(Control_OperationManagement_TableLayout_Remove);
            Control_OperationManagement_Panel_RemoveCard.Dock = DockStyle.Fill;
            Control_OperationManagement_Panel_RemoveCard.Location = new Point(3, 433);
            Control_OperationManagement_Panel_RemoveCard.Name = "Control_OperationManagement_Panel_RemoveCard";
            Control_OperationManagement_Panel_RemoveCard.Size = new Size(440, 245);
            Control_OperationManagement_Panel_RemoveCard.TabIndex = 2;
            // 
            // Control_OperationManagement_TableLayout_Remove
            // 
            Control_OperationManagement_TableLayout_Remove.AutoSize = true;
            Control_OperationManagement_TableLayout_Remove.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_Remove.ColumnCount = 1;
            Control_OperationManagement_TableLayout_Remove.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_Remove.Controls.Add(Control_OperationManagement_TableLayout_RemoveHeader, 0, 0);
            Control_OperationManagement_TableLayout_Remove.Controls.Add(Control_OperationManagement_TableLayout_RemoveContent, 0, 1);
            Control_OperationManagement_TableLayout_Remove.Dock = DockStyle.Fill;
            Control_OperationManagement_TableLayout_Remove.Location = new Point(0, 0);
            Control_OperationManagement_TableLayout_Remove.Name = "Control_OperationManagement_TableLayout_Remove";
            Control_OperationManagement_TableLayout_Remove.Padding = new Padding(16);
            Control_OperationManagement_TableLayout_Remove.RowCount = 2;
            Control_OperationManagement_TableLayout_Remove.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_Remove.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_Remove.Size = new Size(438, 243);
            Control_OperationManagement_TableLayout_Remove.TabIndex = 0;
            // 
            // Control_OperationManagement_TableLayout_RemoveHeader
            // 
            Control_OperationManagement_TableLayout_RemoveHeader.AutoSize = true;
            Control_OperationManagement_TableLayout_RemoveHeader.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_RemoveHeader.ColumnCount = 2;
            Control_OperationManagement_TableLayout_RemoveHeader.ColumnStyles.Add(new ColumnStyle());
            Control_OperationManagement_TableLayout_RemoveHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_RemoveHeader.Controls.Add(Control_OperationManagement_Label_RemoveTitle, 1, 0);
            Control_OperationManagement_TableLayout_RemoveHeader.Controls.Add(Control_OperationManagement_Label_RemoveIcon, 0, 0);
            Control_OperationManagement_TableLayout_RemoveHeader.Dock = DockStyle.Fill;
            Control_OperationManagement_TableLayout_RemoveHeader.Location = new Point(19, 19);
            Control_OperationManagement_TableLayout_RemoveHeader.Name = "Control_OperationManagement_TableLayout_RemoveHeader";
            Control_OperationManagement_TableLayout_RemoveHeader.RowCount = 1;
            Control_OperationManagement_TableLayout_RemoveHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_RemoveHeader.Size = new Size(400, 57);
            Control_OperationManagement_TableLayout_RemoveHeader.TabIndex = 0;
            // 
            // Control_OperationManagement_Label_RemoveTitle
            // 
            Control_OperationManagement_Label_RemoveTitle.AutoSize = true;
            Control_OperationManagement_Label_RemoveTitle.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_RemoveTitle.Font = new Font("Segoe UI Emoji", 16F, FontStyle.Bold);
            Control_OperationManagement_Label_RemoveTitle.Location = new Point(83, 3);
            Control_OperationManagement_Label_RemoveTitle.Margin = new Padding(3);
            Control_OperationManagement_Label_RemoveTitle.Name = "Control_OperationManagement_Label_RemoveTitle";
            Control_OperationManagement_Label_RemoveTitle.Size = new Size(314, 51);
            Control_OperationManagement_Label_RemoveTitle.TabIndex = 1;
            Control_OperationManagement_Label_RemoveTitle.Text = "Remove Operation";
            Control_OperationManagement_Label_RemoveTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_OperationManagement_Label_RemoveIcon
            // 
            Control_OperationManagement_Label_RemoveIcon.AutoSize = true;
            Control_OperationManagement_Label_RemoveIcon.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_RemoveIcon.Font = new Font("Segoe UI Emoji", 28F);
            Control_OperationManagement_Label_RemoveIcon.Location = new Point(3, 3);
            Control_OperationManagement_Label_RemoveIcon.Margin = new Padding(3);
            Control_OperationManagement_Label_RemoveIcon.Name = "Control_OperationManagement_Label_RemoveIcon";
            Control_OperationManagement_Label_RemoveIcon.Size = new Size(74, 51);
            Control_OperationManagement_Label_RemoveIcon.TabIndex = 0;
            Control_OperationManagement_Label_RemoveIcon.Text = "🗑️";
            Control_OperationManagement_Label_RemoveIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_OperationManagement_TableLayout_RemoveContent
            // 
            Control_OperationManagement_TableLayout_RemoveContent.AutoSize = true;
            Control_OperationManagement_TableLayout_RemoveContent.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_RemoveContent.ColumnCount = 1;
            Control_OperationManagement_TableLayout_RemoveContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_RemoveContent.Controls.Add(Control_OperationManagement_Suggestion_RemoveSelectOperation, 0, 0);
            Control_OperationManagement_TableLayout_RemoveContent.Controls.Add(Control_OperationManagement_TableLayout_RemoveDetails, 0, 1);
            Control_OperationManagement_TableLayout_RemoveContent.Controls.Add(Control_OperationManagement_Label_RemoveWarning, 0, 3);
            Control_OperationManagement_TableLayout_RemoveContent.Controls.Add(Control_OperationManagement_TableLayout_RemoveActions, 4, 3);
            Control_OperationManagement_TableLayout_RemoveContent.Dock = DockStyle.Top;
            Control_OperationManagement_TableLayout_RemoveContent.Location = new Point(19, 82);
            Control_OperationManagement_TableLayout_RemoveContent.Name = "Control_OperationManagement_TableLayout_RemoveContent";
            Control_OperationManagement_TableLayout_RemoveContent.RowCount = 5;
            Control_OperationManagement_TableLayout_RemoveContent.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_RemoveContent.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_RemoveContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_RemoveContent.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_RemoveContent.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_RemoveContent.Size = new Size(400, 142);
            Control_OperationManagement_TableLayout_RemoveContent.TabIndex = 2;
            // 
            // Control_OperationManagement_Suggestion_RemoveSelectOperation
            // 
            Control_OperationManagement_Suggestion_RemoveSelectOperation.AutoSize = true;
            Control_OperationManagement_Suggestion_RemoveSelectOperation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_Suggestion_RemoveSelectOperation.BorderStyle = BorderStyle.FixedSingle;
            Control_OperationManagement_Suggestion_RemoveSelectOperation.Dock = DockStyle.Fill;
            Control_OperationManagement_Suggestion_RemoveSelectOperation.LabelText = "Select Operation";
            Control_OperationManagement_Suggestion_RemoveSelectOperation.Location = new Point(3, 3);
            Control_OperationManagement_Suggestion_RemoveSelectOperation.Margin = new Padding(3, 3, 3, 10);
            Control_OperationManagement_Suggestion_RemoveSelectOperation.MaxLength = 130;
            Control_OperationManagement_Suggestion_RemoveSelectOperation.MinimumSize = new Size(0, 23);
            Control_OperationManagement_Suggestion_RemoveSelectOperation.MinLength = 130;
            Control_OperationManagement_Suggestion_RemoveSelectOperation.Name = "Control_OperationManagement_Suggestion_RemoveSelectOperation";
            Control_OperationManagement_Suggestion_RemoveSelectOperation.Padding = new Padding(3);
            Control_OperationManagement_Suggestion_RemoveSelectOperation.PlaceholderText = "Search operations (F4)";
            Control_OperationManagement_Suggestion_RemoveSelectOperation.Size = new Size(394, 31);
            Control_OperationManagement_Suggestion_RemoveSelectOperation.TabIndex = 0;
            // 
            // Control_OperationManagement_TableLayout_RemoveDetails
            // 
            Control_OperationManagement_TableLayout_RemoveDetails.AutoSize = true;
            Control_OperationManagement_TableLayout_RemoveDetails.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_RemoveDetails.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            Control_OperationManagement_TableLayout_RemoveDetails.ColumnCount = 2;
            Control_OperationManagement_TableLayout_RemoveDetails.ColumnStyles.Add(new ColumnStyle());
            Control_OperationManagement_TableLayout_RemoveDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_RemoveDetails.Controls.Add(Control_OperationManagement_Label_RemoveOperation, 0, 0);
            Control_OperationManagement_TableLayout_RemoveDetails.Controls.Add(Control_OperationManagement_Label_RemoveOperationValue, 1, 0);
            Control_OperationManagement_TableLayout_RemoveDetails.Controls.Add(Control_OperationManagement_Label_RemoveIssuedBy, 0, 1);
            Control_OperationManagement_TableLayout_RemoveDetails.Controls.Add(Control_OperationManagement_Label_RemoveIssuedByValue, 1, 1);
            Control_OperationManagement_TableLayout_RemoveDetails.Dock = DockStyle.Top;
            Control_OperationManagement_TableLayout_RemoveDetails.Location = new Point(3, 47);
            Control_OperationManagement_TableLayout_RemoveDetails.Name = "Control_OperationManagement_TableLayout_RemoveDetails";
            Control_OperationManagement_TableLayout_RemoveDetails.RowCount = 2;
            Control_OperationManagement_TableLayout_RemoveDetails.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_RemoveDetails.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_RemoveDetails.Size = new Size(394, 45);
            Control_OperationManagement_TableLayout_RemoveDetails.TabIndex = 1;
            // 
            // Control_OperationManagement_Label_RemoveOperation
            // 
            Control_OperationManagement_Label_RemoveOperation.AutoSize = true;
            Control_OperationManagement_Label_RemoveOperation.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_RemoveOperation.Location = new Point(4, 4);
            Control_OperationManagement_Label_RemoveOperation.Margin = new Padding(3);
            Control_OperationManagement_Label_RemoveOperation.Name = "Control_OperationManagement_Label_RemoveOperation";
            Control_OperationManagement_Label_RemoveOperation.Size = new Size(63, 15);
            Control_OperationManagement_Label_RemoveOperation.TabIndex = 0;
            Control_OperationManagement_Label_RemoveOperation.Text = "Operation:";
            Control_OperationManagement_Label_RemoveOperation.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Control_OperationManagement_Label_RemoveOperationValue
            // 
            Control_OperationManagement_Label_RemoveOperationValue.AutoSize = true;
            Control_OperationManagement_Label_RemoveOperationValue.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_RemoveOperationValue.Location = new Point(74, 4);
            Control_OperationManagement_Label_RemoveOperationValue.Margin = new Padding(3);
            Control_OperationManagement_Label_RemoveOperationValue.Name = "Control_OperationManagement_Label_RemoveOperationValue";
            Control_OperationManagement_Label_RemoveOperationValue.Size = new Size(316, 15);
            Control_OperationManagement_Label_RemoveOperationValue.TabIndex = 1;
            Control_OperationManagement_Label_RemoveOperationValue.Text = "{Value}";
            // 
            // Control_OperationManagement_Label_RemoveIssuedBy
            // 
            Control_OperationManagement_Label_RemoveIssuedBy.AutoSize = true;
            Control_OperationManagement_Label_RemoveIssuedBy.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_RemoveIssuedBy.Location = new Point(4, 26);
            Control_OperationManagement_Label_RemoveIssuedBy.Margin = new Padding(3);
            Control_OperationManagement_Label_RemoveIssuedBy.Name = "Control_OperationManagement_Label_RemoveIssuedBy";
            Control_OperationManagement_Label_RemoveIssuedBy.Size = new Size(63, 15);
            Control_OperationManagement_Label_RemoveIssuedBy.TabIndex = 2;
            Control_OperationManagement_Label_RemoveIssuedBy.Text = "Issued By:";
            Control_OperationManagement_Label_RemoveIssuedBy.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Control_OperationManagement_Label_RemoveIssuedByValue
            // 
            Control_OperationManagement_Label_RemoveIssuedByValue.AutoSize = true;
            Control_OperationManagement_Label_RemoveIssuedByValue.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_RemoveIssuedByValue.Location = new Point(74, 26);
            Control_OperationManagement_Label_RemoveIssuedByValue.Margin = new Padding(3);
            Control_OperationManagement_Label_RemoveIssuedByValue.Name = "Control_OperationManagement_Label_RemoveIssuedByValue";
            Control_OperationManagement_Label_RemoveIssuedByValue.Size = new Size(316, 15);
            Control_OperationManagement_Label_RemoveIssuedByValue.TabIndex = 3;
            Control_OperationManagement_Label_RemoveIssuedByValue.Text = "{Value}";
            // 
            // Control_OperationManagement_Label_RemoveWarning
            // 
            Control_OperationManagement_Label_RemoveWarning.AutoSize = true;
            Control_OperationManagement_Label_RemoveWarning.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_RemoveWarning.ForeColor = Color.FromArgb(192, 0, 0);
            Control_OperationManagement_Label_RemoveWarning.Location = new Point(3, 98);
            Control_OperationManagement_Label_RemoveWarning.Margin = new Padding(3);
            Control_OperationManagement_Label_RemoveWarning.Name = "Control_OperationManagement_Label_RemoveWarning";
            Control_OperationManagement_Label_RemoveWarning.Size = new Size(394, 15);
            Control_OperationManagement_Label_RemoveWarning.TabIndex = 2;
            Control_OperationManagement_Label_RemoveWarning.Text = "Warning: Removal is permanent.";
            // 
            // Control_OperationManagement_TableLayout_RemoveActions
            // 
            Control_OperationManagement_TableLayout_RemoveActions.AutoSize = true;
            Control_OperationManagement_TableLayout_RemoveActions.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_RemoveActions.ColumnCount = 3;
            Control_OperationManagement_TableLayout_RemoveActions.ColumnStyles.Add(new ColumnStyle());
            Control_OperationManagement_TableLayout_RemoveActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_RemoveActions.ColumnStyles.Add(new ColumnStyle());
            Control_OperationManagement_TableLayout_RemoveActions.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            Control_OperationManagement_TableLayout_RemoveActions.Controls.Add(Control_OperationManagement_Button_RemoveConfirm, 0, 0);
            Control_OperationManagement_TableLayout_RemoveActions.Location = new Point(3, 119);
            Control_OperationManagement_TableLayout_RemoveActions.Name = "Control_OperationManagement_TableLayout_RemoveActions";
            Control_OperationManagement_TableLayout_RemoveActions.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            Control_OperationManagement_TableLayout_RemoveActions.Size = new Size(81, 20);
            Control_OperationManagement_TableLayout_RemoveActions.TabIndex = 3;
            // 
            // Control_OperationManagement_Button_RemoveConfirm
            // 
            Control_OperationManagement_Button_RemoveConfirm.AutoSize = true;
            Control_OperationManagement_Button_RemoveConfirm.BackColor = Color.FromArgb(231, 76, 60);
            Control_OperationManagement_Button_RemoveConfirm.Cursor = Cursors.Hand;
            Control_OperationManagement_Button_RemoveConfirm.FlatStyle = FlatStyle.Flat;
            Control_OperationManagement_Button_RemoveConfirm.Location = new Point(3, 3);
            Control_OperationManagement_Button_RemoveConfirm.Name = "Control_OperationManagement_Button_RemoveConfirm";
            Control_OperationManagement_Button_RemoveConfirm.Size = new Size(75, 14);
            Control_OperationManagement_Button_RemoveConfirm.TabIndex = 0;
            Control_OperationManagement_Button_RemoveConfirm.UseVisualStyleBackColor = false;
            // 
            // Control_OperationManagement_Button_RemoveCancel
            // 
            Control_OperationManagement_Button_RemoveCancel.AutoSize = true;
            Control_OperationManagement_Button_RemoveCancel.BackColor = Color.FromArgb(96, 94, 92);
            Control_OperationManagement_Button_RemoveCancel.Cursor = Cursors.Hand;
            Control_OperationManagement_Button_RemoveCancel.FlatStyle = FlatStyle.Flat;
            Control_OperationManagement_Button_RemoveCancel.Font = new Font("Segoe UI Semibold", 10F);
            Control_OperationManagement_Button_RemoveCancel.ForeColor = Color.White;
            Control_OperationManagement_Button_RemoveCancel.Location = new Point(316, 3);
            Control_OperationManagement_Button_RemoveCancel.Name = "Control_OperationManagement_Button_RemoveCancel";
            Control_OperationManagement_Button_RemoveCancel.Size = new Size(75, 23);
            Control_OperationManagement_Button_RemoveCancel.TabIndex = 1;
            Control_OperationManagement_Button_RemoveCancel.Text = "Cancel";
            Control_OperationManagement_Button_RemoveCancel.UseVisualStyleBackColor = false;
            // 
            // Control_OperationManagement_Label_AddIssuedBy
            // 
            Control_OperationManagement_Label_AddIssuedBy.Location = new Point(0, 0);
            Control_OperationManagement_Label_AddIssuedBy.Name = "Control_OperationManagement_Label_AddIssuedBy";
            Control_OperationManagement_Label_AddIssuedBy.Size = new Size(100, 23);
            Control_OperationManagement_Label_AddIssuedBy.TabIndex = 0;
            // 
            // Control_OperationManagement_Label_AddIssuedByValue
            // 
            Control_OperationManagement_Label_AddIssuedByValue.Location = new Point(0, 0);
            Control_OperationManagement_Label_AddIssuedByValue.Name = "Control_OperationManagement_Label_AddIssuedByValue";
            Control_OperationManagement_Label_AddIssuedByValue.Size = new Size(100, 23);
            Control_OperationManagement_Label_AddIssuedByValue.TabIndex = 0;
            // 
            // Control_OperationManagement_Label_EditIssuedBy
            // 
            Control_OperationManagement_Label_EditIssuedBy.Location = new Point(0, 0);
            Control_OperationManagement_Label_EditIssuedBy.Name = "Control_OperationManagement_Label_EditIssuedBy";
            Control_OperationManagement_Label_EditIssuedBy.Size = new Size(100, 23);
            Control_OperationManagement_Label_EditIssuedBy.TabIndex = 0;
            // 
            // Control_OperationManagement_Label_EditIssuedByValue
            // 
            Control_OperationManagement_Label_EditIssuedByValue.Location = new Point(0, 0);
            Control_OperationManagement_Label_EditIssuedByValue.Name = "Control_OperationManagement_Label_EditIssuedByValue";
            Control_OperationManagement_Label_EditIssuedByValue.Size = new Size(100, 23);
            Control_OperationManagement_Label_EditIssuedByValue.TabIndex = 0;
            // 
            // Control_OperationManagement
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(Control_OperationManagement_TableLayout_Main);
            Name = "Control_OperationManagement";
            Size = new Size(492, 383);
            Control_OperationManagement_TableLayout_Main.ResumeLayout(false);
            Control_OperationManagement_TableLayout_Main.PerformLayout();
            Control_OperationManagement_TableLayout_BackButton.ResumeLayout(false);
            Control_OperationManagement_TableLayout_BackButton.PerformLayout();
            Control_OperationManagement_Panel_Container.ResumeLayout(false);
            Control_OperationManagement_Panel_Container.PerformLayout();
            Control_OperationManagement_Panel_Home.ResumeLayout(false);
            Control_OperationManagement_TableLayout_Home.ResumeLayout(false);
            Control_OperationManagement_TableLayout_Home.PerformLayout();
            Control_OperationManagement_Panel_HomeTile_Add.ResumeLayout(false);
            Control_OperationManagement_Panel_HomeTile_Add.PerformLayout();
            Control_OperationManagement_TableLayout_HomeTile_Add.ResumeLayout(false);
            Control_OperationManagement_TableLayout_HomeTile_Add.PerformLayout();
            Control_OperationManagement_Panel_HomeTile_Edit.ResumeLayout(false);
            Control_OperationManagement_Panel_HomeTile_Edit.PerformLayout();
            Control_OperationManagement_TableLayout_HomeTile_Edit.ResumeLayout(false);
            Control_OperationManagement_TableLayout_HomeTile_Edit.PerformLayout();
            Control_OperationManagement_Panel_HomeTile_Remove.ResumeLayout(false);
            Control_OperationManagement_Panel_HomeTile_Remove.PerformLayout();
            Control_OperationManagement_TableLayout_HomeTile_Remove.ResumeLayout(false);
            Control_OperationManagement_TableLayout_HomeTile_Remove.PerformLayout();
            Control_OperationManagement_TableLayout_Cards.ResumeLayout(false);
            Control_OperationManagement_TableLayout_Cards.PerformLayout();
            Control_OperationManagement_Panel_AddCard.ResumeLayout(false);
            Control_OperationManagement_Panel_AddCard.PerformLayout();
            Control_OperationManagement_TableLayout_Add.ResumeLayout(false);
            Control_OperationManagement_TableLayout_Add.PerformLayout();
            Control_OperationManagement_TableLayout_AddHeader.ResumeLayout(false);
            Control_OperationManagement_TableLayout_AddHeader.PerformLayout();
            Control_OperationManagement_TableLayout_AddContent.ResumeLayout(false);
            Control_OperationManagement_TableLayout_AddContent.PerformLayout();
            Control_OperationManagement_TableLayout_AddActions.ResumeLayout(false);
            Control_OperationManagement_TableLayout_AddActions.PerformLayout();
            Control_OperationManagement_Panel_EditCard.ResumeLayout(false);
            Control_OperationManagement_Panel_EditCard.PerformLayout();
            Control_OperationManagement_TableLayout_Edit.ResumeLayout(false);
            Control_OperationManagement_TableLayout_Edit.PerformLayout();
            Control_OperationManagement_TableLayout_EditHeader.ResumeLayout(false);
            Control_OperationManagement_TableLayout_EditHeader.PerformLayout();
            Control_OperationManagement_TableLayout_EditContent.ResumeLayout(false);
            Control_OperationManagement_TableLayout_EditContent.PerformLayout();
            Control_OperationManagement_TableLayout_EditActions.ResumeLayout(false);
            Control_OperationManagement_TableLayout_EditActions.PerformLayout();
            Control_OperationManagement_Panel_RemoveCard.ResumeLayout(false);
            Control_OperationManagement_Panel_RemoveCard.PerformLayout();
            Control_OperationManagement_TableLayout_Remove.ResumeLayout(false);
            Control_OperationManagement_TableLayout_Remove.PerformLayout();
            Control_OperationManagement_TableLayout_RemoveHeader.ResumeLayout(false);
            Control_OperationManagement_TableLayout_RemoveHeader.PerformLayout();
            Control_OperationManagement_TableLayout_RemoveContent.ResumeLayout(false);
            Control_OperationManagement_TableLayout_RemoveContent.PerformLayout();
            Control_OperationManagement_TableLayout_RemoveDetails.ResumeLayout(false);
            Control_OperationManagement_TableLayout_RemoveDetails.PerformLayout();
            Control_OperationManagement_TableLayout_RemoveActions.ResumeLayout(false);
            Control_OperationManagement_TableLayout_RemoveActions.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }


        private static void ConfigureHomeTile(Panel panel, string icon, string title, string description, Color accentColor, int column)
        {
            panel.AutoSize = true;
            panel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel.BackColor = Color.White;
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Cursor = Cursors.Hand;
            panel.Dock = DockStyle.Fill;
            panel.Margin = new Padding(column == 0 ? 0 : 5, 0, column == 2 ? 0 : 5, 0);
            panel.Padding = new Padding(0);
            panel.Tag = column; // 0=Add, 1=Edit, 2=Remove
            
            // Accent bar
            Panel accentBar = new Panel
            {
                Height = 4,
                Dock = DockStyle.Top,
                BackColor = accentColor
            };
            panel.Controls.Add(accentBar);
            
            // Content container
            TableLayoutPanel content = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                RowCount = 3,
                ColumnCount = 1
            };
            content.RowStyles.Add(new RowStyle());
            content.RowStyles.Add(new RowStyle());
            content.RowStyles.Add(new RowStyle());
            content.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            
            Label iconLabel = new Label
            {
                Text = icon,
                Font = new Font("Segoe UI Emoji", 48F, FontStyle.Regular, GraphicsUnit.Point),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            
            Label titleLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI Emoji", 14F, FontStyle.Bold, GraphicsUnit.Point),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 10, 0, 5)
            };
            
            Label descLabel = new Label
            {
                Text = description,
                Font = new Font("Segoe UI Emoji", 9F, FontStyle.Regular, GraphicsUnit.Point),
                ForeColor = Color.FromArgb(90, 90, 90),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            
            content.Controls.Add(iconLabel, 0, 0);
            content.Controls.Add(titleLabel, 0, 1);
            content.Controls.Add(descLabel, 0, 2);
            panel.Controls.Add(content);
        }

        #endregion
        private TableLayoutPanel Control_OperationManagement_TableLayout_AddHeader;
        private TableLayoutPanel Control_OperationManagement_TableLayout_EditHeader;
        private TableLayoutPanel Control_OperationManagement_TableLayout_RemoveHeader;
        private Button Control_OperationManagement_Button_Home;
        private Button SettingsForm_Button_Help_Operations;
    }
}
