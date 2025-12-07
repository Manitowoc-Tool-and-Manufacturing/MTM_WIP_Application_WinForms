using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Components.Shared;

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    partial class Control_ItemTypeManagement
    {
        #region Fields

        private IContainer components = null!;
        private TableLayoutPanel Control_ItemTypeManagement_TableLayout_Main = null!;
        private Label Control_ItemTypeManagement_Label_Header = null!;
        private Label Control_ItemTypeManagement_Label_Subtitle = null!;
        private Panel Control_ItemTypeManagement_Panel_Container = null!;
        
        private Panel Control_ItemTypeManagement_Panel_Home = null!;
        private TableLayoutPanel Control_ItemTypeManagement_TableLayout_Home = null!;
        private Panel Control_ItemTypeManagement_Panel_HomeTile_Add = null!;
        private TableLayoutPanel Control_ItemTypeManagement_TableLayout_HomeTile_Add = null!;
        private Label Control_ItemTypeManagement_Label_HomeTile_AddIcon = null!;
        private Label Control_ItemTypeManagement_Label_HomeTile_AddTitle = null!;
        private Label Control_ItemTypeManagement_Label_HomeTile_AddInstruction = null!;
        private Panel Control_ItemTypeManagement_Panel_HomeTile_Edit = null!;
        private TableLayoutPanel Control_ItemTypeManagement_TableLayout_HomeTile_Edit = null!;
        private Label Control_ItemTypeManagement_Label_HomeTile_EditIcon = null!;
        private Label Control_ItemTypeManagement_Label_HomeTile_EditTitle = null!;
        private Label Control_ItemTypeManagement_Label_HomeTile_EditInstruction = null!;
        private Panel Control_ItemTypeManagement_Panel_HomeTile_Remove = null!;
        private TableLayoutPanel Control_ItemTypeManagement_TableLayout_HomeTile_Remove = null!;
        private Label Control_ItemTypeManagement_Label_HomeTile_RemoveIcon = null!;
        private Label Control_ItemTypeManagement_Label_HomeTile_RemoveTitle = null!;
        private Label Control_ItemTypeManagement_Label_HomeTile_RemoveInstruction = null!;
        
        private TableLayoutPanel Control_ItemTypeManagement_TableLayout_Cards = null!;
        private TableLayoutPanel Control_ItemTypeManagement_TableLayout_BackButton = null!;
        private Button Control_ItemTypeManagement_Button_Back = null!;

        private Panel Control_ItemTypeManagement_Panel_AddCard = null!;
        private TableLayoutPanel Control_ItemTypeManagement_TableLayout_Add = null!;
        private Label Control_ItemTypeManagement_Label_AddIcon = null!;
        private Label Control_ItemTypeManagement_Label_AddTitle = null!;
        private TableLayoutPanel Control_ItemTypeManagement_TableLayout_AddContent = null!;
        private Component_SuggestionTextBoxWithLabel Control_ItemTypeManagement_TextBox_AddItemType = null!;
        private Label Control_ItemTypeManagement_Label_AddIssuedBy = null!;
        private Label Control_ItemTypeManagement_Label_AddIssuedByValue = null!;
        private TableLayoutPanel Control_ItemTypeManagement_TableLayout_AddActions = null!;
        private Button Control_ItemTypeManagement_Button_AddSave = null!;
        private Button Control_ItemTypeManagement_Button_AddClear = null!;

        private Panel Control_ItemTypeManagement_Panel_EditCard = null!;
        private TableLayoutPanel Control_ItemTypeManagement_TableLayout_Edit = null!;
        private Label Control_ItemTypeManagement_Label_EditIcon = null!;
        private Label Control_ItemTypeManagement_Label_EditTitle = null!;
        private TableLayoutPanel Control_ItemTypeManagement_TableLayout_EditContent = null!;
        private Component_SuggestionTextBoxWithLabel Control_ItemTypeManagement_Suggestion_EditSelectItemType = null!;
        private Component_SuggestionTextBoxWithLabel Control_ItemTypeManagement_TextBox_EditNewItemType = null!;
        private Label Control_ItemTypeManagement_Label_EditIssuedBy = null!;
        private Label Control_ItemTypeManagement_Label_EditIssuedByValue = null!;
        private TableLayoutPanel Control_ItemTypeManagement_TableLayout_EditActions = null!;
        private Button Control_ItemTypeManagement_Button_EditSave = null!;
        private Button Control_ItemTypeManagement_Button_EditReset = null!;

        private Panel Control_ItemTypeManagement_Panel_RemoveCard = null!;
        private TableLayoutPanel Control_ItemTypeManagement_TableLayout_Remove = null!;
        private Label Control_ItemTypeManagement_Label_RemoveIcon = null!;
        private Label Control_ItemTypeManagement_Label_RemoveTitle = null!;
        private TableLayoutPanel Control_ItemTypeManagement_TableLayout_RemoveContent = null!;
        private Component_SuggestionTextBoxWithLabel Control_ItemTypeManagement_Suggestion_RemoveSelectItemType = null!;
        private TableLayoutPanel Control_ItemTypeManagement_TableLayout_RemoveDetails = null!;
        private Label Control_ItemTypeManagement_Label_RemoveItemType = null!;
        private Label Control_ItemTypeManagement_Label_RemoveItemTypeValue = null!;
        private Label Control_ItemTypeManagement_Label_RemoveIssuedBy = null!;
        private Label Control_ItemTypeManagement_Label_RemoveIssuedByValue = null!;
        private Label Control_ItemTypeManagement_Label_RemoveWarning = null!;
        private TableLayoutPanel Control_ItemTypeManagement_TableLayout_RemoveActions = null!;
        private Button Control_ItemTypeManagement_Button_RemoveConfirm = null!;
        private Button Control_ItemTypeManagement_Button_RemoveCancel = null!;

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
            Control_ItemTypeManagement_TableLayout_Main = new TableLayoutPanel();
            Control_ItemTypeManagement_Label_Header = new Label();
            Control_ItemTypeManagement_Label_Subtitle = new Label();
            Control_ItemTypeManagement_Panel_Container = new Panel();
            Control_ItemTypeManagement_Panel_Home = new Panel();
            Control_ItemTypeManagement_TableLayout_Home = new TableLayoutPanel();
            Control_ItemTypeManagement_Panel_HomeTile_Add = new Panel();
            Control_ItemTypeManagement_TableLayout_HomeTile_Add = new TableLayoutPanel();
            Control_ItemTypeManagement_Label_HomeTile_AddIcon = new Label();
            Control_ItemTypeManagement_Label_HomeTile_AddTitle = new Label();
            Control_ItemTypeManagement_Label_HomeTile_AddInstruction = new Label();
            Control_ItemTypeManagement_Panel_HomeTile_Edit = new Panel();
            Control_ItemTypeManagement_TableLayout_HomeTile_Edit = new TableLayoutPanel();
            Control_ItemTypeManagement_Label_HomeTile_EditIcon = new Label();
            Control_ItemTypeManagement_Label_HomeTile_EditTitle = new Label();
            Control_ItemTypeManagement_Label_HomeTile_EditInstruction = new Label();
            Control_ItemTypeManagement_Panel_HomeTile_Remove = new Panel();
            Control_ItemTypeManagement_TableLayout_HomeTile_Remove = new TableLayoutPanel();
            Control_ItemTypeManagement_Label_HomeTile_RemoveIcon = new Label();
            Control_ItemTypeManagement_Label_HomeTile_RemoveTitle = new Label();
            Control_ItemTypeManagement_Label_HomeTile_RemoveInstruction = new Label();
            Control_ItemTypeManagement_TableLayout_Cards = new TableLayoutPanel();
            Control_ItemTypeManagement_Panel_AddCard = new Panel();
            Control_ItemTypeManagement_TableLayout_Add = new TableLayoutPanel();
            Control_ItemTypeManagement_TableLayout_AddHeader = new TableLayoutPanel();
            Control_ItemTypeManagement_Label_AddIcon = new Label();
            Control_ItemTypeManagement_Label_AddTitle = new Label();
            Control_ItemTypeManagement_TableLayout_AddContent = new TableLayoutPanel();
            Control_ItemTypeManagement_TextBox_AddItemType = new Component_SuggestionTextBoxWithLabel();
            Control_ItemTypeManagement_TableLayout_AddActions = new TableLayoutPanel();
            Control_ItemTypeManagement_Button_AddSave = new Button();
            Control_ItemTypeManagement_Button_AddClear = new Button();
            Control_ItemTypeManagement_Panel_EditCard = new Panel();
            Control_ItemTypeManagement_TableLayout_Edit = new TableLayoutPanel();
            Control_ItemTypeManagement_TableLayout_EditHeader = new TableLayoutPanel();
            Control_ItemTypeManagement_Label_EditIcon = new Label();
            Control_ItemTypeManagement_Label_EditTitle = new Label();
            Control_ItemTypeManagement_TableLayout_EditContent = new TableLayoutPanel();
            Control_ItemTypeManagement_Suggestion_EditSelectItemType = new Component_SuggestionTextBoxWithLabel();
            Control_ItemTypeManagement_TextBox_EditNewItemType = new Component_SuggestionTextBoxWithLabel();
            Control_ItemTypeManagement_TableLayout_EditActions = new TableLayoutPanel();
            Control_ItemTypeManagement_Button_EditSave = new Button();
            Control_ItemTypeManagement_Button_EditReset = new Button();
            Control_ItemTypeManagement_Panel_RemoveCard = new Panel();
            Control_ItemTypeManagement_TableLayout_Remove = new TableLayoutPanel();
            Control_ItemTypeManagement_TableLayout_RemoveContent = new TableLayoutPanel();
            Control_ItemTypeManagement_Suggestion_RemoveSelectItemType = new Component_SuggestionTextBoxWithLabel();
            Control_ItemTypeManagement_TableLayout_RemoveDetails = new TableLayoutPanel();
            Control_ItemTypeManagement_Label_RemoveItemType = new Label();
            Control_ItemTypeManagement_Label_RemoveItemTypeValue = new Label();
            Control_ItemTypeManagement_Label_RemoveIssuedBy = new Label();
            Control_ItemTypeManagement_Label_RemoveIssuedByValue = new Label();
            Control_ItemTypeManagement_Label_RemoveWarning = new Label();
            Control_ItemTypeManagement_TableLayout_RemoveActions = new TableLayoutPanel();
            Control_ItemTypeManagement_Button_RemoveConfirm = new Button();
            Control_ItemTypeManagement_Button_RemoveCancel = new Button();
            Control_ItemTypeManagement_TableLayout_RemoveHeader = new TableLayoutPanel();
            Control_ItemTypeManagement_Label_RemoveIcon = new Label();
            Control_ItemTypeManagement_Label_RemoveTitle = new Label();
            Control_ItemTypeManagement_TableLayout_BackButton = new TableLayoutPanel();
            Control_ItemTypeManagement_Button_Back = new Button();
            Control_ItemTypeManagement_Button_Home = new Button();
            Control_ItemTypeManagement_Label_AddIssuedBy = new Label();
            Control_ItemTypeManagement_Label_AddIssuedByValue = new Label();
            Control_ItemTypeManagement_Label_EditIssuedBy = new Label();
            Control_ItemTypeManagement_Label_EditIssuedByValue = new Label();
            SettingsForm_Button_Help_ItemTypes = new Button();
            Control_ItemTypeManagement_TableLayout_Main.SuspendLayout();
            Control_ItemTypeManagement_Panel_Container.SuspendLayout();
            Control_ItemTypeManagement_Panel_Home.SuspendLayout();
            Control_ItemTypeManagement_TableLayout_Home.SuspendLayout();
            Control_ItemTypeManagement_Panel_HomeTile_Add.SuspendLayout();
            Control_ItemTypeManagement_TableLayout_HomeTile_Add.SuspendLayout();
            Control_ItemTypeManagement_Panel_HomeTile_Edit.SuspendLayout();
            Control_ItemTypeManagement_TableLayout_HomeTile_Edit.SuspendLayout();
            Control_ItemTypeManagement_Panel_HomeTile_Remove.SuspendLayout();
            Control_ItemTypeManagement_TableLayout_HomeTile_Remove.SuspendLayout();
            Control_ItemTypeManagement_TableLayout_Cards.SuspendLayout();
            Control_ItemTypeManagement_Panel_AddCard.SuspendLayout();
            Control_ItemTypeManagement_TableLayout_Add.SuspendLayout();
            Control_ItemTypeManagement_TableLayout_AddHeader.SuspendLayout();
            Control_ItemTypeManagement_TableLayout_AddContent.SuspendLayout();
            Control_ItemTypeManagement_TableLayout_AddActions.SuspendLayout();
            Control_ItemTypeManagement_Panel_EditCard.SuspendLayout();
            Control_ItemTypeManagement_TableLayout_Edit.SuspendLayout();
            Control_ItemTypeManagement_TableLayout_EditHeader.SuspendLayout();
            Control_ItemTypeManagement_TableLayout_EditContent.SuspendLayout();
            Control_ItemTypeManagement_TableLayout_EditActions.SuspendLayout();
            Control_ItemTypeManagement_Panel_RemoveCard.SuspendLayout();
            Control_ItemTypeManagement_TableLayout_Remove.SuspendLayout();
            Control_ItemTypeManagement_TableLayout_RemoveContent.SuspendLayout();
            Control_ItemTypeManagement_TableLayout_RemoveDetails.SuspendLayout();
            Control_ItemTypeManagement_TableLayout_RemoveActions.SuspendLayout();
            Control_ItemTypeManagement_TableLayout_RemoveHeader.SuspendLayout();
            Control_ItemTypeManagement_TableLayout_BackButton.SuspendLayout();
            SuspendLayout();
            // 
            // Control_ItemTypeManagement_TableLayout_Main
            // 
            Control_ItemTypeManagement_TableLayout_Main.AutoSize = true;
            Control_ItemTypeManagement_TableLayout_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ItemTypeManagement_TableLayout_Main.ColumnCount = 1;
            Control_ItemTypeManagement_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_ItemTypeManagement_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            Control_ItemTypeManagement_TableLayout_Main.Controls.Add(Control_ItemTypeManagement_Label_Header, 0, 0);
            Control_ItemTypeManagement_TableLayout_Main.Controls.Add(Control_ItemTypeManagement_Label_Subtitle, 0, 1);
            Control_ItemTypeManagement_TableLayout_Main.Controls.Add(Control_ItemTypeManagement_Panel_Container, 0, 2);
            Control_ItemTypeManagement_TableLayout_Main.Controls.Add(Control_ItemTypeManagement_TableLayout_BackButton, 0, 3);
            Control_ItemTypeManagement_TableLayout_Main.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_TableLayout_Main.Location = new Point(0, 0);
            Control_ItemTypeManagement_TableLayout_Main.Name = "Control_ItemTypeManagement_TableLayout_Main";
            Control_ItemTypeManagement_TableLayout_Main.Padding = new Padding(20);
            Control_ItemTypeManagement_TableLayout_Main.RowCount = 4;
            Control_ItemTypeManagement_TableLayout_Main.RowStyles.Add(new RowStyle());
            Control_ItemTypeManagement_TableLayout_Main.RowStyles.Add(new RowStyle());
            Control_ItemTypeManagement_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_ItemTypeManagement_TableLayout_Main.RowStyles.Add(new RowStyle());
            Control_ItemTypeManagement_TableLayout_Main.Size = new Size(492, 383);
            Control_ItemTypeManagement_TableLayout_Main.TabIndex = 0;
            // 
            // Control_ItemTypeManagement_Label_Header
            // 
            Control_ItemTypeManagement_Label_Header.AutoSize = true;
            Control_ItemTypeManagement_Label_Header.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_Label_Header.Font = new Font("Segoe UI Emoji", 20F, FontStyle.Bold);
            Control_ItemTypeManagement_Label_Header.ForeColor = Color.FromArgb(45, 45, 45);
            Control_ItemTypeManagement_Label_Header.Location = new Point(23, 23);
            Control_ItemTypeManagement_Label_Header.Margin = new Padding(3);
            Control_ItemTypeManagement_Label_Header.Name = "Control_ItemTypeManagement_Label_Header";
            Control_ItemTypeManagement_Label_Header.Size = new Size(446, 36);
            Control_ItemTypeManagement_Label_Header.TabIndex = 0;
            Control_ItemTypeManagement_Label_Header.Text = "Item Types";
            // 
            // Control_ItemTypeManagement_Label_Subtitle
            // 
            Control_ItemTypeManagement_Label_Subtitle.AutoSize = true;
            Control_ItemTypeManagement_Label_Subtitle.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_Label_Subtitle.Font = new Font("Segoe UI Emoji", 10F);
            Control_ItemTypeManagement_Label_Subtitle.ForeColor = Color.FromArgb(90, 90, 90);
            Control_ItemTypeManagement_Label_Subtitle.Location = new Point(23, 65);
            Control_ItemTypeManagement_Label_Subtitle.Margin = new Padding(3);
            Control_ItemTypeManagement_Label_Subtitle.Name = "Control_ItemTypeManagement_Label_Subtitle";
            Control_ItemTypeManagement_Label_Subtitle.Size = new Size(446, 19);
            Control_ItemTypeManagement_Label_Subtitle.TabIndex = 1;
            Control_ItemTypeManagement_Label_Subtitle.Text = "Select an action below to manage item types.";
            // 
            // Control_ItemTypeManagement_Panel_Container
            // 
            Control_ItemTypeManagement_Panel_Container.Controls.Add(Control_ItemTypeManagement_Panel_Home);
            Control_ItemTypeManagement_Panel_Container.Controls.Add(Control_ItemTypeManagement_TableLayout_Cards);
            Control_ItemTypeManagement_Panel_Container.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_Panel_Container.Location = new Point(23, 90);
            Control_ItemTypeManagement_Panel_Container.Name = "Control_ItemTypeManagement_Panel_Container";
            Control_ItemTypeManagement_Panel_Container.Size = new Size(446, 226);
            Control_ItemTypeManagement_Panel_Container.TabIndex = 2;
            // 
            // Control_ItemTypeManagement_Panel_Home
            // 
            Control_ItemTypeManagement_Panel_Home.AutoSize = true;
            Control_ItemTypeManagement_Panel_Home.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ItemTypeManagement_Panel_Home.Controls.Add(Control_ItemTypeManagement_TableLayout_Home);
            Control_ItemTypeManagement_Panel_Home.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_Panel_Home.Location = new Point(0, 0);
            Control_ItemTypeManagement_Panel_Home.Name = "Control_ItemTypeManagement_Panel_Home";
            Control_ItemTypeManagement_Panel_Home.Size = new Size(446, 226);
            Control_ItemTypeManagement_Panel_Home.TabIndex = 0;
            // 
            // Control_ItemTypeManagement_TableLayout_Home
            // 
            Control_ItemTypeManagement_TableLayout_Home.AutoSize = true;
            Control_ItemTypeManagement_TableLayout_Home.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ItemTypeManagement_TableLayout_Home.ColumnCount = 3;
            Control_ItemTypeManagement_TableLayout_Home.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            Control_ItemTypeManagement_TableLayout_Home.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            Control_ItemTypeManagement_TableLayout_Home.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
            Control_ItemTypeManagement_TableLayout_Home.Controls.Add(Control_ItemTypeManagement_Panel_HomeTile_Add, 0, 0);
            Control_ItemTypeManagement_TableLayout_Home.Controls.Add(Control_ItemTypeManagement_Panel_HomeTile_Edit, 1, 0);
            Control_ItemTypeManagement_TableLayout_Home.Controls.Add(Control_ItemTypeManagement_Panel_HomeTile_Remove, 2, 0);
            Control_ItemTypeManagement_TableLayout_Home.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_TableLayout_Home.Location = new Point(0, 0);
            Control_ItemTypeManagement_TableLayout_Home.Name = "Control_ItemTypeManagement_TableLayout_Home";
            Control_ItemTypeManagement_TableLayout_Home.RowCount = 1;
            Control_ItemTypeManagement_TableLayout_Home.RowStyles.Add(new RowStyle());
            Control_ItemTypeManagement_TableLayout_Home.Size = new Size(446, 226);
            Control_ItemTypeManagement_TableLayout_Home.TabIndex = 0;
            // 
            // Control_ItemTypeManagement_Panel_HomeTile_Add
            // 
            Control_ItemTypeManagement_Panel_HomeTile_Add.AutoSize = true;
            Control_ItemTypeManagement_Panel_HomeTile_Add.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ItemTypeManagement_Panel_HomeTile_Add.BorderStyle = BorderStyle.FixedSingle;
            Control_ItemTypeManagement_Panel_HomeTile_Add.Controls.Add(Control_ItemTypeManagement_TableLayout_HomeTile_Add);
            Control_ItemTypeManagement_Panel_HomeTile_Add.Cursor = Cursors.Hand;
            Control_ItemTypeManagement_Panel_HomeTile_Add.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_Panel_HomeTile_Add.Location = new Point(3, 3);
            Control_ItemTypeManagement_Panel_HomeTile_Add.Name = "Control_ItemTypeManagement_Panel_HomeTile_Add";
            Control_ItemTypeManagement_Panel_HomeTile_Add.Size = new Size(142, 220);
            Control_ItemTypeManagement_Panel_HomeTile_Add.TabIndex = 0;
            // 
            // Control_ItemTypeManagement_TableLayout_HomeTile_Add
            // 
            Control_ItemTypeManagement_TableLayout_HomeTile_Add.AutoSize = true;
            Control_ItemTypeManagement_TableLayout_HomeTile_Add.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ItemTypeManagement_TableLayout_HomeTile_Add.ColumnCount = 1;
            Control_ItemTypeManagement_TableLayout_HomeTile_Add.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_ItemTypeManagement_TableLayout_HomeTile_Add.Controls.Add(Control_ItemTypeManagement_Label_HomeTile_AddIcon, 0, 0);
            Control_ItemTypeManagement_TableLayout_HomeTile_Add.Controls.Add(Control_ItemTypeManagement_Label_HomeTile_AddTitle, 0, 1);
            Control_ItemTypeManagement_TableLayout_HomeTile_Add.Controls.Add(Control_ItemTypeManagement_Label_HomeTile_AddInstruction, 0, 3);
            Control_ItemTypeManagement_TableLayout_HomeTile_Add.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_TableLayout_HomeTile_Add.Location = new Point(0, 0);
            Control_ItemTypeManagement_TableLayout_HomeTile_Add.Name = "Control_ItemTypeManagement_TableLayout_HomeTile_Add";
            Control_ItemTypeManagement_TableLayout_HomeTile_Add.Padding = new Padding(3);
            Control_ItemTypeManagement_TableLayout_HomeTile_Add.RowCount = 5;
            Control_ItemTypeManagement_TableLayout_HomeTile_Add.RowStyles.Add(new RowStyle());
            Control_ItemTypeManagement_TableLayout_HomeTile_Add.RowStyles.Add(new RowStyle());
            Control_ItemTypeManagement_TableLayout_HomeTile_Add.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Control_ItemTypeManagement_TableLayout_HomeTile_Add.RowStyles.Add(new RowStyle());
            Control_ItemTypeManagement_TableLayout_HomeTile_Add.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Control_ItemTypeManagement_TableLayout_HomeTile_Add.Size = new Size(140, 218);
            Control_ItemTypeManagement_TableLayout_HomeTile_Add.TabIndex = 0;
            // 
            // Control_ItemTypeManagement_Label_HomeTile_AddIcon
            // 
            Control_ItemTypeManagement_Label_HomeTile_AddIcon.AutoSize = true;
            Control_ItemTypeManagement_Label_HomeTile_AddIcon.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_Label_HomeTile_AddIcon.Font = new Font("Segoe UI Emoji", 48F);
            Control_ItemTypeManagement_Label_HomeTile_AddIcon.Location = new Point(6, 6);
            Control_ItemTypeManagement_Label_HomeTile_AddIcon.Margin = new Padding(3);
            Control_ItemTypeManagement_Label_HomeTile_AddIcon.Name = "Control_ItemTypeManagement_Label_HomeTile_AddIcon";
            Control_ItemTypeManagement_Label_HomeTile_AddIcon.Size = new Size(128, 85);
            Control_ItemTypeManagement_Label_HomeTile_AddIcon.TabIndex = 0;
            Control_ItemTypeManagement_Label_HomeTile_AddIcon.Text = "🆕";
            Control_ItemTypeManagement_Label_HomeTile_AddIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_ItemTypeManagement_Label_HomeTile_AddTitle
            // 
            Control_ItemTypeManagement_Label_HomeTile_AddTitle.AutoSize = true;
            Control_ItemTypeManagement_Label_HomeTile_AddTitle.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_Label_HomeTile_AddTitle.Font = new Font("Segoe UI Emoji", 14F, FontStyle.Bold);
            Control_ItemTypeManagement_Label_HomeTile_AddTitle.Location = new Point(6, 97);
            Control_ItemTypeManagement_Label_HomeTile_AddTitle.Margin = new Padding(3);
            Control_ItemTypeManagement_Label_HomeTile_AddTitle.Name = "Control_ItemTypeManagement_Label_HomeTile_AddTitle";
            Control_ItemTypeManagement_Label_HomeTile_AddTitle.Size = new Size(128, 52);
            Control_ItemTypeManagement_Label_HomeTile_AddTitle.TabIndex = 1;
            Control_ItemTypeManagement_Label_HomeTile_AddTitle.Text = "Add\r\nType";
            Control_ItemTypeManagement_Label_HomeTile_AddTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_ItemTypeManagement_Label_HomeTile_AddInstruction
            // 
            Control_ItemTypeManagement_Label_HomeTile_AddInstruction.AutoSize = true;
            Control_ItemTypeManagement_Label_HomeTile_AddInstruction.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_Label_HomeTile_AddInstruction.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Italic);
            Control_ItemTypeManagement_Label_HomeTile_AddInstruction.ForeColor = Color.FromArgb(100, 100, 100);
            Control_ItemTypeManagement_Label_HomeTile_AddInstruction.Location = new Point(6, 167);
            Control_ItemTypeManagement_Label_HomeTile_AddInstruction.Margin = new Padding(3);
            Control_ItemTypeManagement_Label_HomeTile_AddInstruction.Name = "Control_ItemTypeManagement_Label_HomeTile_AddInstruction";
            Control_ItemTypeManagement_Label_HomeTile_AddInstruction.Size = new Size(128, 32);
            Control_ItemTypeManagement_Label_HomeTile_AddInstruction.TabIndex = 2;
            Control_ItemTypeManagement_Label_HomeTile_AddInstruction.Text = "Click to add new item types";
            Control_ItemTypeManagement_Label_HomeTile_AddInstruction.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_ItemTypeManagement_Panel_HomeTile_Edit
            // 
            Control_ItemTypeManagement_Panel_HomeTile_Edit.AutoSize = true;
            Control_ItemTypeManagement_Panel_HomeTile_Edit.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ItemTypeManagement_Panel_HomeTile_Edit.BorderStyle = BorderStyle.FixedSingle;
            Control_ItemTypeManagement_Panel_HomeTile_Edit.Controls.Add(Control_ItemTypeManagement_TableLayout_HomeTile_Edit);
            Control_ItemTypeManagement_Panel_HomeTile_Edit.Cursor = Cursors.Hand;
            Control_ItemTypeManagement_Panel_HomeTile_Edit.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_Panel_HomeTile_Edit.Location = new Point(151, 3);
            Control_ItemTypeManagement_Panel_HomeTile_Edit.Name = "Control_ItemTypeManagement_Panel_HomeTile_Edit";
            Control_ItemTypeManagement_Panel_HomeTile_Edit.Size = new Size(142, 220);
            Control_ItemTypeManagement_Panel_HomeTile_Edit.TabIndex = 1;
            // 
            // Control_ItemTypeManagement_TableLayout_HomeTile_Edit
            // 
            Control_ItemTypeManagement_TableLayout_HomeTile_Edit.AutoSize = true;
            Control_ItemTypeManagement_TableLayout_HomeTile_Edit.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ItemTypeManagement_TableLayout_HomeTile_Edit.ColumnCount = 1;
            Control_ItemTypeManagement_TableLayout_HomeTile_Edit.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_ItemTypeManagement_TableLayout_HomeTile_Edit.Controls.Add(Control_ItemTypeManagement_Label_HomeTile_EditIcon, 0, 0);
            Control_ItemTypeManagement_TableLayout_HomeTile_Edit.Controls.Add(Control_ItemTypeManagement_Label_HomeTile_EditTitle, 0, 1);
            Control_ItemTypeManagement_TableLayout_HomeTile_Edit.Controls.Add(Control_ItemTypeManagement_Label_HomeTile_EditInstruction, 0, 3);
            Control_ItemTypeManagement_TableLayout_HomeTile_Edit.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_TableLayout_HomeTile_Edit.Location = new Point(0, 0);
            Control_ItemTypeManagement_TableLayout_HomeTile_Edit.Name = "Control_ItemTypeManagement_TableLayout_HomeTile_Edit";
            Control_ItemTypeManagement_TableLayout_HomeTile_Edit.Padding = new Padding(3);
            Control_ItemTypeManagement_TableLayout_HomeTile_Edit.RowCount = 5;
            Control_ItemTypeManagement_TableLayout_HomeTile_Edit.RowStyles.Add(new RowStyle());
            Control_ItemTypeManagement_TableLayout_HomeTile_Edit.RowStyles.Add(new RowStyle());
            Control_ItemTypeManagement_TableLayout_HomeTile_Edit.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Control_ItemTypeManagement_TableLayout_HomeTile_Edit.RowStyles.Add(new RowStyle());
            Control_ItemTypeManagement_TableLayout_HomeTile_Edit.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Control_ItemTypeManagement_TableLayout_HomeTile_Edit.Size = new Size(140, 218);
            Control_ItemTypeManagement_TableLayout_HomeTile_Edit.TabIndex = 0;
            // 
            // Control_ItemTypeManagement_Label_HomeTile_EditIcon
            // 
            Control_ItemTypeManagement_Label_HomeTile_EditIcon.AutoSize = true;
            Control_ItemTypeManagement_Label_HomeTile_EditIcon.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_Label_HomeTile_EditIcon.Font = new Font("Segoe UI Emoji", 48F);
            Control_ItemTypeManagement_Label_HomeTile_EditIcon.Location = new Point(6, 6);
            Control_ItemTypeManagement_Label_HomeTile_EditIcon.Margin = new Padding(3);
            Control_ItemTypeManagement_Label_HomeTile_EditIcon.Name = "Control_ItemTypeManagement_Label_HomeTile_EditIcon";
            Control_ItemTypeManagement_Label_HomeTile_EditIcon.Size = new Size(128, 85);
            Control_ItemTypeManagement_Label_HomeTile_EditIcon.TabIndex = 0;
            Control_ItemTypeManagement_Label_HomeTile_EditIcon.Text = "✏️";
            Control_ItemTypeManagement_Label_HomeTile_EditIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_ItemTypeManagement_Label_HomeTile_EditTitle
            // 
            Control_ItemTypeManagement_Label_HomeTile_EditTitle.AutoSize = true;
            Control_ItemTypeManagement_Label_HomeTile_EditTitle.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_Label_HomeTile_EditTitle.Font = new Font("Segoe UI Emoji", 14F, FontStyle.Bold);
            Control_ItemTypeManagement_Label_HomeTile_EditTitle.Location = new Point(6, 97);
            Control_ItemTypeManagement_Label_HomeTile_EditTitle.Margin = new Padding(3);
            Control_ItemTypeManagement_Label_HomeTile_EditTitle.Name = "Control_ItemTypeManagement_Label_HomeTile_EditTitle";
            Control_ItemTypeManagement_Label_HomeTile_EditTitle.Size = new Size(128, 52);
            Control_ItemTypeManagement_Label_HomeTile_EditTitle.TabIndex = 1;
            Control_ItemTypeManagement_Label_HomeTile_EditTitle.Text = "Edit\r\nType";
            Control_ItemTypeManagement_Label_HomeTile_EditTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_ItemTypeManagement_Label_HomeTile_EditInstruction
            // 
            Control_ItemTypeManagement_Label_HomeTile_EditInstruction.AutoSize = true;
            Control_ItemTypeManagement_Label_HomeTile_EditInstruction.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_Label_HomeTile_EditInstruction.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Italic);
            Control_ItemTypeManagement_Label_HomeTile_EditInstruction.ForeColor = Color.FromArgb(100, 100, 100);
            Control_ItemTypeManagement_Label_HomeTile_EditInstruction.Location = new Point(6, 167);
            Control_ItemTypeManagement_Label_HomeTile_EditInstruction.Margin = new Padding(3);
            Control_ItemTypeManagement_Label_HomeTile_EditInstruction.Name = "Control_ItemTypeManagement_Label_HomeTile_EditInstruction";
            Control_ItemTypeManagement_Label_HomeTile_EditInstruction.Size = new Size(128, 32);
            Control_ItemTypeManagement_Label_HomeTile_EditInstruction.TabIndex = 2;
            Control_ItemTypeManagement_Label_HomeTile_EditInstruction.Text = "Click to edit existing item types";
            Control_ItemTypeManagement_Label_HomeTile_EditInstruction.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_ItemTypeManagement_Panel_HomeTile_Remove
            // 
            Control_ItemTypeManagement_Panel_HomeTile_Remove.AutoSize = true;
            Control_ItemTypeManagement_Panel_HomeTile_Remove.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ItemTypeManagement_Panel_HomeTile_Remove.BorderStyle = BorderStyle.FixedSingle;
            Control_ItemTypeManagement_Panel_HomeTile_Remove.Controls.Add(Control_ItemTypeManagement_TableLayout_HomeTile_Remove);
            Control_ItemTypeManagement_Panel_HomeTile_Remove.Cursor = Cursors.Hand;
            Control_ItemTypeManagement_Panel_HomeTile_Remove.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_Panel_HomeTile_Remove.Location = new Point(299, 3);
            Control_ItemTypeManagement_Panel_HomeTile_Remove.Name = "Control_ItemTypeManagement_Panel_HomeTile_Remove";
            Control_ItemTypeManagement_Panel_HomeTile_Remove.Size = new Size(144, 220);
            Control_ItemTypeManagement_Panel_HomeTile_Remove.TabIndex = 2;
            // 
            // Control_ItemTypeManagement_TableLayout_HomeTile_Remove
            // 
            Control_ItemTypeManagement_TableLayout_HomeTile_Remove.AutoSize = true;
            Control_ItemTypeManagement_TableLayout_HomeTile_Remove.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ItemTypeManagement_TableLayout_HomeTile_Remove.ColumnCount = 1;
            Control_ItemTypeManagement_TableLayout_HomeTile_Remove.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_ItemTypeManagement_TableLayout_HomeTile_Remove.Controls.Add(Control_ItemTypeManagement_Label_HomeTile_RemoveIcon, 0, 0);
            Control_ItemTypeManagement_TableLayout_HomeTile_Remove.Controls.Add(Control_ItemTypeManagement_Label_HomeTile_RemoveTitle, 0, 1);
            Control_ItemTypeManagement_TableLayout_HomeTile_Remove.Controls.Add(Control_ItemTypeManagement_Label_HomeTile_RemoveInstruction, 0, 3);
            Control_ItemTypeManagement_TableLayout_HomeTile_Remove.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_TableLayout_HomeTile_Remove.Location = new Point(0, 0);
            Control_ItemTypeManagement_TableLayout_HomeTile_Remove.Name = "Control_ItemTypeManagement_TableLayout_HomeTile_Remove";
            Control_ItemTypeManagement_TableLayout_HomeTile_Remove.Padding = new Padding(3);
            Control_ItemTypeManagement_TableLayout_HomeTile_Remove.RowCount = 5;
            Control_ItemTypeManagement_TableLayout_HomeTile_Remove.RowStyles.Add(new RowStyle());
            Control_ItemTypeManagement_TableLayout_HomeTile_Remove.RowStyles.Add(new RowStyle());
            Control_ItemTypeManagement_TableLayout_HomeTile_Remove.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Control_ItemTypeManagement_TableLayout_HomeTile_Remove.RowStyles.Add(new RowStyle());
            Control_ItemTypeManagement_TableLayout_HomeTile_Remove.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Control_ItemTypeManagement_TableLayout_HomeTile_Remove.Size = new Size(142, 218);
            Control_ItemTypeManagement_TableLayout_HomeTile_Remove.TabIndex = 0;
            // 
            // Control_ItemTypeManagement_Label_HomeTile_RemoveIcon
            // 
            Control_ItemTypeManagement_Label_HomeTile_RemoveIcon.AutoSize = true;
            Control_ItemTypeManagement_Label_HomeTile_RemoveIcon.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_Label_HomeTile_RemoveIcon.Font = new Font("Segoe UI Emoji", 48F);
            Control_ItemTypeManagement_Label_HomeTile_RemoveIcon.Location = new Point(6, 6);
            Control_ItemTypeManagement_Label_HomeTile_RemoveIcon.Margin = new Padding(3);
            Control_ItemTypeManagement_Label_HomeTile_RemoveIcon.Name = "Control_ItemTypeManagement_Label_HomeTile_RemoveIcon";
            Control_ItemTypeManagement_Label_HomeTile_RemoveIcon.Size = new Size(130, 85);
            Control_ItemTypeManagement_Label_HomeTile_RemoveIcon.TabIndex = 0;
            Control_ItemTypeManagement_Label_HomeTile_RemoveIcon.Text = "🗑️";
            Control_ItemTypeManagement_Label_HomeTile_RemoveIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_ItemTypeManagement_Label_HomeTile_RemoveTitle
            // 
            Control_ItemTypeManagement_Label_HomeTile_RemoveTitle.AutoSize = true;
            Control_ItemTypeManagement_Label_HomeTile_RemoveTitle.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_Label_HomeTile_RemoveTitle.Font = new Font("Segoe UI Emoji", 14F, FontStyle.Bold);
            Control_ItemTypeManagement_Label_HomeTile_RemoveTitle.Location = new Point(6, 97);
            Control_ItemTypeManagement_Label_HomeTile_RemoveTitle.Margin = new Padding(3);
            Control_ItemTypeManagement_Label_HomeTile_RemoveTitle.Name = "Control_ItemTypeManagement_Label_HomeTile_RemoveTitle";
            Control_ItemTypeManagement_Label_HomeTile_RemoveTitle.Size = new Size(130, 52);
            Control_ItemTypeManagement_Label_HomeTile_RemoveTitle.TabIndex = 1;
            Control_ItemTypeManagement_Label_HomeTile_RemoveTitle.Text = "Remove\r\nType";
            Control_ItemTypeManagement_Label_HomeTile_RemoveTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_ItemTypeManagement_Label_HomeTile_RemoveInstruction
            // 
            Control_ItemTypeManagement_Label_HomeTile_RemoveInstruction.AutoSize = true;
            Control_ItemTypeManagement_Label_HomeTile_RemoveInstruction.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_Label_HomeTile_RemoveInstruction.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Italic);
            Control_ItemTypeManagement_Label_HomeTile_RemoveInstruction.ForeColor = Color.FromArgb(100, 100, 100);
            Control_ItemTypeManagement_Label_HomeTile_RemoveInstruction.Location = new Point(6, 167);
            Control_ItemTypeManagement_Label_HomeTile_RemoveInstruction.Margin = new Padding(3);
            Control_ItemTypeManagement_Label_HomeTile_RemoveInstruction.Name = "Control_ItemTypeManagement_Label_HomeTile_RemoveInstruction";
            Control_ItemTypeManagement_Label_HomeTile_RemoveInstruction.Size = new Size(130, 32);
            Control_ItemTypeManagement_Label_HomeTile_RemoveInstruction.TabIndex = 2;
            Control_ItemTypeManagement_Label_HomeTile_RemoveInstruction.Text = "Click to remove item types";
            Control_ItemTypeManagement_Label_HomeTile_RemoveInstruction.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_ItemTypeManagement_TableLayout_Cards
            // 
            Control_ItemTypeManagement_TableLayout_Cards.AutoSize = true;
            Control_ItemTypeManagement_TableLayout_Cards.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ItemTypeManagement_TableLayout_Cards.ColumnCount = 1;
            Control_ItemTypeManagement_TableLayout_Cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_ItemTypeManagement_TableLayout_Cards.Controls.Add(Control_ItemTypeManagement_Panel_AddCard, 0, 0);
            Control_ItemTypeManagement_TableLayout_Cards.Controls.Add(Control_ItemTypeManagement_Panel_EditCard, 0, 1);
            Control_ItemTypeManagement_TableLayout_Cards.Controls.Add(Control_ItemTypeManagement_Panel_RemoveCard, 0, 2);
            Control_ItemTypeManagement_TableLayout_Cards.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_TableLayout_Cards.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            Control_ItemTypeManagement_TableLayout_Cards.Location = new Point(0, 0);
            Control_ItemTypeManagement_TableLayout_Cards.Name = "Control_ItemTypeManagement_TableLayout_Cards";
            Control_ItemTypeManagement_TableLayout_Cards.RowCount = 3;
            Control_ItemTypeManagement_TableLayout_Cards.RowStyles.Add(new RowStyle());
            Control_ItemTypeManagement_TableLayout_Cards.RowStyles.Add(new RowStyle());
            Control_ItemTypeManagement_TableLayout_Cards.RowStyles.Add(new RowStyle());
            Control_ItemTypeManagement_TableLayout_Cards.Size = new Size(446, 226);
            Control_ItemTypeManagement_TableLayout_Cards.TabIndex = 1;
            Control_ItemTypeManagement_TableLayout_Cards.Visible = false;
            // 
            // Control_ItemTypeManagement_Panel_AddCard
            // 
            Control_ItemTypeManagement_Panel_AddCard.AutoSize = true;
            Control_ItemTypeManagement_Panel_AddCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ItemTypeManagement_Panel_AddCard.BorderStyle = BorderStyle.FixedSingle;
            Control_ItemTypeManagement_Panel_AddCard.Controls.Add(Control_ItemTypeManagement_TableLayout_Add);
            Control_ItemTypeManagement_Panel_AddCard.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_Panel_AddCard.Location = new Point(3, 3);
            Control_ItemTypeManagement_Panel_AddCard.Name = "Control_ItemTypeManagement_Panel_AddCard";
            Control_ItemTypeManagement_Panel_AddCard.Size = new Size(440, 175);
            Control_ItemTypeManagement_Panel_AddCard.TabIndex = 0;
            // 
            // Control_ItemTypeManagement_TableLayout_Add
            // 
            Control_ItemTypeManagement_TableLayout_Add.AutoSize = true;
            Control_ItemTypeManagement_TableLayout_Add.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ItemTypeManagement_TableLayout_Add.ColumnCount = 1;
            Control_ItemTypeManagement_TableLayout_Add.ColumnStyles.Add(new ColumnStyle());
            Control_ItemTypeManagement_TableLayout_Add.Controls.Add(Control_ItemTypeManagement_TableLayout_AddHeader, 0, 0);
            Control_ItemTypeManagement_TableLayout_Add.Controls.Add(Control_ItemTypeManagement_TableLayout_AddContent, 0, 1);
            Control_ItemTypeManagement_TableLayout_Add.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_TableLayout_Add.Location = new Point(0, 0);
            Control_ItemTypeManagement_TableLayout_Add.Name = "Control_ItemTypeManagement_TableLayout_Add";
            Control_ItemTypeManagement_TableLayout_Add.Padding = new Padding(16);
            Control_ItemTypeManagement_TableLayout_Add.RowCount = 2;
            Control_ItemTypeManagement_TableLayout_Add.RowStyles.Add(new RowStyle());
            Control_ItemTypeManagement_TableLayout_Add.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_ItemTypeManagement_TableLayout_Add.Size = new Size(438, 173);
            Control_ItemTypeManagement_TableLayout_Add.TabIndex = 0;
            // 
            // Control_ItemTypeManagement_TableLayout_AddHeader
            // 
            Control_ItemTypeManagement_TableLayout_AddHeader.AutoSize = true;
            Control_ItemTypeManagement_TableLayout_AddHeader.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ItemTypeManagement_TableLayout_AddHeader.ColumnCount = 2;
            Control_ItemTypeManagement_TableLayout_AddHeader.ColumnStyles.Add(new ColumnStyle());
            Control_ItemTypeManagement_TableLayout_AddHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_ItemTypeManagement_TableLayout_AddHeader.Controls.Add(Control_ItemTypeManagement_Label_AddIcon, 0, 0);
            Control_ItemTypeManagement_TableLayout_AddHeader.Controls.Add(Control_ItemTypeManagement_Label_AddTitle, 1, 0);
            Control_ItemTypeManagement_TableLayout_AddHeader.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_TableLayout_AddHeader.Location = new Point(19, 19);
            Control_ItemTypeManagement_TableLayout_AddHeader.Name = "Control_ItemTypeManagement_TableLayout_AddHeader";
            Control_ItemTypeManagement_TableLayout_AddHeader.RowCount = 1;
            Control_ItemTypeManagement_TableLayout_AddHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_ItemTypeManagement_TableLayout_AddHeader.Size = new Size(400, 57);
            Control_ItemTypeManagement_TableLayout_AddHeader.TabIndex = 2;
            // 
            // Control_ItemTypeManagement_Label_AddIcon
            // 
            Control_ItemTypeManagement_Label_AddIcon.AutoSize = true;
            Control_ItemTypeManagement_Label_AddIcon.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_Label_AddIcon.Font = new Font("Segoe UI Emoji", 28F);
            Control_ItemTypeManagement_Label_AddIcon.Location = new Point(3, 3);
            Control_ItemTypeManagement_Label_AddIcon.Margin = new Padding(3);
            Control_ItemTypeManagement_Label_AddIcon.Name = "Control_ItemTypeManagement_Label_AddIcon";
            Control_ItemTypeManagement_Label_AddIcon.Size = new Size(74, 51);
            Control_ItemTypeManagement_Label_AddIcon.TabIndex = 0;
            Control_ItemTypeManagement_Label_AddIcon.Text = "🆕";
            Control_ItemTypeManagement_Label_AddIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_ItemTypeManagement_Label_AddTitle
            // 
            Control_ItemTypeManagement_Label_AddTitle.AutoSize = true;
            Control_ItemTypeManagement_Label_AddTitle.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_Label_AddTitle.Font = new Font("Segoe UI Emoji", 16F, FontStyle.Bold);
            Control_ItemTypeManagement_Label_AddTitle.Location = new Point(83, 3);
            Control_ItemTypeManagement_Label_AddTitle.Margin = new Padding(3);
            Control_ItemTypeManagement_Label_AddTitle.Name = "Control_ItemTypeManagement_Label_AddTitle";
            Control_ItemTypeManagement_Label_AddTitle.Size = new Size(314, 51);
            Control_ItemTypeManagement_Label_AddTitle.TabIndex = 1;
            Control_ItemTypeManagement_Label_AddTitle.Text = "Add Item Type";
            Control_ItemTypeManagement_Label_AddTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_ItemTypeManagement_TableLayout_AddContent
            // 
            Control_ItemTypeManagement_TableLayout_AddContent.AutoSize = true;
            Control_ItemTypeManagement_TableLayout_AddContent.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ItemTypeManagement_TableLayout_AddContent.ColumnCount = 1;
            Control_ItemTypeManagement_TableLayout_Add.SetColumnSpan(Control_ItemTypeManagement_TableLayout_AddContent, 2);
            Control_ItemTypeManagement_TableLayout_AddContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_ItemTypeManagement_TableLayout_AddContent.Controls.Add(Control_ItemTypeManagement_TextBox_AddItemType, 0, 0);
            Control_ItemTypeManagement_TableLayout_AddContent.Controls.Add(Control_ItemTypeManagement_TableLayout_AddActions, 0, 1);
            Control_ItemTypeManagement_TableLayout_AddContent.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_TableLayout_AddContent.Location = new Point(19, 82);
            Control_ItemTypeManagement_TableLayout_AddContent.Name = "Control_ItemTypeManagement_TableLayout_AddContent";
            Control_ItemTypeManagement_TableLayout_AddContent.RowCount = 2;
            Control_ItemTypeManagement_TableLayout_AddContent.RowStyles.Add(new RowStyle());
            Control_ItemTypeManagement_TableLayout_AddContent.RowStyles.Add(new RowStyle());
            Control_ItemTypeManagement_TableLayout_AddContent.Size = new Size(400, 72);
            Control_ItemTypeManagement_TableLayout_AddContent.TabIndex = 2;
            // 
            // Control_ItemTypeManagement_TextBox_AddItemType
            // 
            Control_ItemTypeManagement_TextBox_AddItemType.AutoSize = true;
            Control_ItemTypeManagement_TextBox_AddItemType.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ItemTypeManagement_TextBox_AddItemType.BorderStyle = BorderStyle.FixedSingle;
            Control_ItemTypeManagement_TextBox_AddItemType.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_TextBox_AddItemType.EnableSuggestions = false;
            Control_ItemTypeManagement_TextBox_AddItemType.Location = new Point(3, 3);
            Control_ItemTypeManagement_TextBox_AddItemType.MaxLength = 130;
            Control_ItemTypeManagement_TextBox_AddItemType.MinimumSize = new Size(0, 23);
            Control_ItemTypeManagement_TextBox_AddItemType.MinLength = 130;
            Control_ItemTypeManagement_TextBox_AddItemType.Name = "Control_ItemTypeManagement_TextBox_AddItemType";
            Control_ItemTypeManagement_TextBox_AddItemType.Padding = new Padding(3);
            Control_ItemTypeManagement_TextBox_AddItemType.ShowF4Button = false;
            Control_ItemTypeManagement_TextBox_AddItemType.Size = new Size(394, 31);
            Control_ItemTypeManagement_TextBox_AddItemType.TabIndex = 0;
            // 
            // Control_ItemTypeManagement_TableLayout_AddActions
            // 
            Control_ItemTypeManagement_TableLayout_AddActions.AutoSize = true;
            Control_ItemTypeManagement_TableLayout_AddActions.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ItemTypeManagement_TableLayout_AddActions.ColumnCount = 3;
            Control_ItemTypeManagement_TableLayout_AddActions.ColumnStyles.Add(new ColumnStyle());
            Control_ItemTypeManagement_TableLayout_AddActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_ItemTypeManagement_TableLayout_AddActions.ColumnStyles.Add(new ColumnStyle());
            Control_ItemTypeManagement_TableLayout_AddActions.Controls.Add(Control_ItemTypeManagement_Button_AddSave, 0, 0);
            Control_ItemTypeManagement_TableLayout_AddActions.Controls.Add(Control_ItemTypeManagement_Button_AddClear, 2, 0);
            Control_ItemTypeManagement_TableLayout_AddActions.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_TableLayout_AddActions.Location = new Point(3, 40);
            Control_ItemTypeManagement_TableLayout_AddActions.Name = "Control_ItemTypeManagement_TableLayout_AddActions";
            Control_ItemTypeManagement_TableLayout_AddActions.RowCount = 1;
            Control_ItemTypeManagement_TableLayout_AddActions.RowStyles.Add(new RowStyle());
            Control_ItemTypeManagement_TableLayout_AddActions.Size = new Size(394, 29);
            Control_ItemTypeManagement_TableLayout_AddActions.TabIndex = 1;
            // 
            // Control_ItemTypeManagement_Button_AddSave
            // 
            Control_ItemTypeManagement_Button_AddSave.Location = new Point(3, 3);
            Control_ItemTypeManagement_Button_AddSave.Name = "Control_ItemTypeManagement_Button_AddSave";
            Control_ItemTypeManagement_Button_AddSave.Size = new Size(75, 23);
            Control_ItemTypeManagement_Button_AddSave.TabIndex = 0;
            // 
            // Control_ItemTypeManagement_Button_AddClear
            // 
            Control_ItemTypeManagement_Button_AddClear.Location = new Point(316, 3);
            Control_ItemTypeManagement_Button_AddClear.Name = "Control_ItemTypeManagement_Button_AddClear";
            Control_ItemTypeManagement_Button_AddClear.Size = new Size(75, 23);
            Control_ItemTypeManagement_Button_AddClear.TabIndex = 1;
            // 
            // Control_ItemTypeManagement_Panel_EditCard
            // 
            Control_ItemTypeManagement_Panel_EditCard.AutoSize = true;
            Control_ItemTypeManagement_Panel_EditCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ItemTypeManagement_Panel_EditCard.BorderStyle = BorderStyle.FixedSingle;
            Control_ItemTypeManagement_Panel_EditCard.Controls.Add(Control_ItemTypeManagement_TableLayout_Edit);
            Control_ItemTypeManagement_Panel_EditCard.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_Panel_EditCard.Location = new Point(3, 184);
            Control_ItemTypeManagement_Panel_EditCard.Name = "Control_ItemTypeManagement_Panel_EditCard";
            Control_ItemTypeManagement_Panel_EditCard.Size = new Size(440, 226);
            Control_ItemTypeManagement_Panel_EditCard.TabIndex = 1;
            // 
            // Control_ItemTypeManagement_TableLayout_Edit
            // 
            Control_ItemTypeManagement_TableLayout_Edit.AutoSize = true;
            Control_ItemTypeManagement_TableLayout_Edit.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ItemTypeManagement_TableLayout_Edit.ColumnCount = 1;
            Control_ItemTypeManagement_TableLayout_Edit.ColumnStyles.Add(new ColumnStyle());
            Control_ItemTypeManagement_TableLayout_Edit.Controls.Add(Control_ItemTypeManagement_TableLayout_EditHeader, 0, 0);
            Control_ItemTypeManagement_TableLayout_Edit.Controls.Add(Control_ItemTypeManagement_TableLayout_EditContent, 0, 1);
            Control_ItemTypeManagement_TableLayout_Edit.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_TableLayout_Edit.Location = new Point(0, 0);
            Control_ItemTypeManagement_TableLayout_Edit.Name = "Control_ItemTypeManagement_TableLayout_Edit";
            Control_ItemTypeManagement_TableLayout_Edit.Padding = new Padding(16);
            Control_ItemTypeManagement_TableLayout_Edit.RowCount = 2;
            Control_ItemTypeManagement_TableLayout_Edit.RowStyles.Add(new RowStyle());
            Control_ItemTypeManagement_TableLayout_Edit.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_ItemTypeManagement_TableLayout_Edit.Size = new Size(438, 224);
            Control_ItemTypeManagement_TableLayout_Edit.TabIndex = 0;
            // 
            // Control_ItemTypeManagement_TableLayout_EditHeader
            // 
            Control_ItemTypeManagement_TableLayout_EditHeader.AutoSize = true;
            Control_ItemTypeManagement_TableLayout_EditHeader.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ItemTypeManagement_TableLayout_EditHeader.ColumnCount = 2;
            Control_ItemTypeManagement_TableLayout_EditHeader.ColumnStyles.Add(new ColumnStyle());
            Control_ItemTypeManagement_TableLayout_EditHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_ItemTypeManagement_TableLayout_EditHeader.Controls.Add(Control_ItemTypeManagement_Label_EditIcon, 0, 0);
            Control_ItemTypeManagement_TableLayout_EditHeader.Controls.Add(Control_ItemTypeManagement_Label_EditTitle, 1, 0);
            Control_ItemTypeManagement_TableLayout_EditHeader.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_TableLayout_EditHeader.Location = new Point(19, 19);
            Control_ItemTypeManagement_TableLayout_EditHeader.Name = "Control_ItemTypeManagement_TableLayout_EditHeader";
            Control_ItemTypeManagement_TableLayout_EditHeader.RowCount = 1;
            Control_ItemTypeManagement_TableLayout_EditHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_ItemTypeManagement_TableLayout_EditHeader.Size = new Size(400, 57);
            Control_ItemTypeManagement_TableLayout_EditHeader.TabIndex = 4;
            // 
            // Control_ItemTypeManagement_Label_EditIcon
            // 
            Control_ItemTypeManagement_Label_EditIcon.AutoSize = true;
            Control_ItemTypeManagement_Label_EditIcon.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_Label_EditIcon.Font = new Font("Segoe UI Emoji", 28F);
            Control_ItemTypeManagement_Label_EditIcon.Location = new Point(3, 3);
            Control_ItemTypeManagement_Label_EditIcon.Margin = new Padding(3);
            Control_ItemTypeManagement_Label_EditIcon.Name = "Control_ItemTypeManagement_Label_EditIcon";
            Control_ItemTypeManagement_Label_EditIcon.Size = new Size(74, 51);
            Control_ItemTypeManagement_Label_EditIcon.TabIndex = 0;
            Control_ItemTypeManagement_Label_EditIcon.Text = "✏️";
            Control_ItemTypeManagement_Label_EditIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_ItemTypeManagement_Label_EditTitle
            // 
            Control_ItemTypeManagement_Label_EditTitle.AutoSize = true;
            Control_ItemTypeManagement_Label_EditTitle.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_Label_EditTitle.Font = new Font("Segoe UI Emoji", 16F, FontStyle.Bold);
            Control_ItemTypeManagement_Label_EditTitle.Location = new Point(83, 3);
            Control_ItemTypeManagement_Label_EditTitle.Margin = new Padding(3);
            Control_ItemTypeManagement_Label_EditTitle.Name = "Control_ItemTypeManagement_Label_EditTitle";
            Control_ItemTypeManagement_Label_EditTitle.Size = new Size(314, 51);
            Control_ItemTypeManagement_Label_EditTitle.TabIndex = 1;
            Control_ItemTypeManagement_Label_EditTitle.Text = "Edit Item Type";
            Control_ItemTypeManagement_Label_EditTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_ItemTypeManagement_TableLayout_EditContent
            // 
            Control_ItemTypeManagement_TableLayout_EditContent.AutoSize = true;
            Control_ItemTypeManagement_TableLayout_EditContent.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ItemTypeManagement_TableLayout_EditContent.ColumnCount = 1;
            Control_ItemTypeManagement_TableLayout_Edit.SetColumnSpan(Control_ItemTypeManagement_TableLayout_EditContent, 2);
            Control_ItemTypeManagement_TableLayout_EditContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_ItemTypeManagement_TableLayout_EditContent.Controls.Add(Control_ItemTypeManagement_Suggestion_EditSelectItemType, 0, 0);
            Control_ItemTypeManagement_TableLayout_EditContent.Controls.Add(Control_ItemTypeManagement_TextBox_EditNewItemType, 0, 1);
            Control_ItemTypeManagement_TableLayout_EditContent.Controls.Add(Control_ItemTypeManagement_TableLayout_EditActions, 0, 2);
            Control_ItemTypeManagement_TableLayout_EditContent.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_TableLayout_EditContent.Location = new Point(19, 82);
            Control_ItemTypeManagement_TableLayout_EditContent.Name = "Control_ItemTypeManagement_TableLayout_EditContent";
            Control_ItemTypeManagement_TableLayout_EditContent.RowCount = 3;
            Control_ItemTypeManagement_TableLayout_EditContent.RowStyles.Add(new RowStyle());
            Control_ItemTypeManagement_TableLayout_EditContent.RowStyles.Add(new RowStyle());
            Control_ItemTypeManagement_TableLayout_EditContent.RowStyles.Add(new RowStyle());
            Control_ItemTypeManagement_TableLayout_EditContent.Size = new Size(400, 123);
            Control_ItemTypeManagement_TableLayout_EditContent.TabIndex = 2;
            // 
            // Control_ItemTypeManagement_Suggestion_EditSelectItemType
            // 
            Control_ItemTypeManagement_Suggestion_EditSelectItemType.AutoSize = true;
            Control_ItemTypeManagement_Suggestion_EditSelectItemType.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ItemTypeManagement_Suggestion_EditSelectItemType.BorderStyle = BorderStyle.FixedSingle;
            Control_ItemTypeManagement_Suggestion_EditSelectItemType.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_Suggestion_EditSelectItemType.LabelText = "Select Item Type";
            Control_ItemTypeManagement_Suggestion_EditSelectItemType.Location = new Point(3, 3);
            Control_ItemTypeManagement_Suggestion_EditSelectItemType.Margin = new Padding(3, 3, 3, 10);
            Control_ItemTypeManagement_Suggestion_EditSelectItemType.MaxLength = 130;
            Control_ItemTypeManagement_Suggestion_EditSelectItemType.MinimumSize = new Size(0, 23);
            Control_ItemTypeManagement_Suggestion_EditSelectItemType.MinLength = 130;
            Control_ItemTypeManagement_Suggestion_EditSelectItemType.Name = "Control_ItemTypeManagement_Suggestion_EditSelectItemType";
            Control_ItemTypeManagement_Suggestion_EditSelectItemType.Padding = new Padding(3);
            Control_ItemTypeManagement_Suggestion_EditSelectItemType.PlaceholderText = "Search item types (F4)";
            Control_ItemTypeManagement_Suggestion_EditSelectItemType.Size = new Size(394, 31);
            Control_ItemTypeManagement_Suggestion_EditSelectItemType.TabIndex = 0;
            // 
            // Control_ItemTypeManagement_TextBox_EditNewItemType
            // 
            Control_ItemTypeManagement_TextBox_EditNewItemType.AutoSize = true;
            Control_ItemTypeManagement_TextBox_EditNewItemType.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ItemTypeManagement_TextBox_EditNewItemType.BorderStyle = BorderStyle.FixedSingle;
            Control_ItemTypeManagement_TextBox_EditNewItemType.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_TextBox_EditNewItemType.EnableSuggestions = false;
            Control_ItemTypeManagement_TextBox_EditNewItemType.LabelText = "New Item Type";
            Control_ItemTypeManagement_TextBox_EditNewItemType.Location = new Point(3, 47);
            Control_ItemTypeManagement_TextBox_EditNewItemType.Margin = new Padding(3, 3, 3, 10);
            Control_ItemTypeManagement_TextBox_EditNewItemType.MaxLength = 130;
            Control_ItemTypeManagement_TextBox_EditNewItemType.MinimumSize = new Size(0, 23);
            Control_ItemTypeManagement_TextBox_EditNewItemType.MinLength = 130;
            Control_ItemTypeManagement_TextBox_EditNewItemType.Name = "Control_ItemTypeManagement_TextBox_EditNewItemType";
            Control_ItemTypeManagement_TextBox_EditNewItemType.Padding = new Padding(3);
            Control_ItemTypeManagement_TextBox_EditNewItemType.PlaceholderText = "Enter new item type";
            Control_ItemTypeManagement_TextBox_EditNewItemType.ShowF4Button = false;
            Control_ItemTypeManagement_TextBox_EditNewItemType.Size = new Size(394, 31);
            Control_ItemTypeManagement_TextBox_EditNewItemType.TabIndex = 1;
            // 
            // Control_ItemTypeManagement_TableLayout_EditActions
            // 
            Control_ItemTypeManagement_TableLayout_EditActions.AutoSize = true;
            Control_ItemTypeManagement_TableLayout_EditActions.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ItemTypeManagement_TableLayout_EditActions.ColumnCount = 3;
            Control_ItemTypeManagement_TableLayout_EditActions.ColumnStyles.Add(new ColumnStyle());
            Control_ItemTypeManagement_TableLayout_EditActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_ItemTypeManagement_TableLayout_EditActions.ColumnStyles.Add(new ColumnStyle());
            Control_ItemTypeManagement_TableLayout_EditActions.Controls.Add(Control_ItemTypeManagement_Button_EditSave, 0, 0);
            Control_ItemTypeManagement_TableLayout_EditActions.Controls.Add(Control_ItemTypeManagement_Button_EditReset, 2, 0);
            Control_ItemTypeManagement_TableLayout_EditActions.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_TableLayout_EditActions.Location = new Point(3, 91);
            Control_ItemTypeManagement_TableLayout_EditActions.Name = "Control_ItemTypeManagement_TableLayout_EditActions";
            Control_ItemTypeManagement_TableLayout_EditActions.RowCount = 1;
            Control_ItemTypeManagement_TableLayout_EditActions.RowStyles.Add(new RowStyle());
            Control_ItemTypeManagement_TableLayout_EditActions.Size = new Size(394, 29);
            Control_ItemTypeManagement_TableLayout_EditActions.TabIndex = 2;
            // 
            // Control_ItemTypeManagement_Button_EditSave
            // 
            Control_ItemTypeManagement_Button_EditSave.Location = new Point(3, 3);
            Control_ItemTypeManagement_Button_EditSave.Name = "Control_ItemTypeManagement_Button_EditSave";
            Control_ItemTypeManagement_Button_EditSave.Size = new Size(75, 23);
            Control_ItemTypeManagement_Button_EditSave.TabIndex = 0;
            // 
            // Control_ItemTypeManagement_Button_EditReset
            // 
            Control_ItemTypeManagement_Button_EditReset.Location = new Point(316, 3);
            Control_ItemTypeManagement_Button_EditReset.Name = "Control_ItemTypeManagement_Button_EditReset";
            Control_ItemTypeManagement_Button_EditReset.Size = new Size(75, 23);
            Control_ItemTypeManagement_Button_EditReset.TabIndex = 1;
            // 
            // Control_ItemTypeManagement_Panel_RemoveCard
            // 
            Control_ItemTypeManagement_Panel_RemoveCard.AutoSize = true;
            Control_ItemTypeManagement_Panel_RemoveCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ItemTypeManagement_Panel_RemoveCard.Controls.Add(Control_ItemTypeManagement_TableLayout_Remove);
            Control_ItemTypeManagement_Panel_RemoveCard.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_Panel_RemoveCard.Location = new Point(3, 416);
            Control_ItemTypeManagement_Panel_RemoveCard.Name = "Control_ItemTypeManagement_Panel_RemoveCard";
            Control_ItemTypeManagement_Panel_RemoveCard.Size = new Size(440, 252);
            Control_ItemTypeManagement_Panel_RemoveCard.TabIndex = 2;
            // 
            // Control_ItemTypeManagement_TableLayout_Remove
            // 
            Control_ItemTypeManagement_TableLayout_Remove.AutoSize = true;
            Control_ItemTypeManagement_TableLayout_Remove.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ItemTypeManagement_TableLayout_Remove.ColumnCount = 1;
            Control_ItemTypeManagement_TableLayout_Remove.ColumnStyles.Add(new ColumnStyle());
            Control_ItemTypeManagement_TableLayout_Remove.Controls.Add(Control_ItemTypeManagement_TableLayout_RemoveContent, 0, 1);
            Control_ItemTypeManagement_TableLayout_Remove.Controls.Add(Control_ItemTypeManagement_TableLayout_RemoveHeader, 0, 0);
            Control_ItemTypeManagement_TableLayout_Remove.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_TableLayout_Remove.Location = new Point(0, 0);
            Control_ItemTypeManagement_TableLayout_Remove.Name = "Control_ItemTypeManagement_TableLayout_Remove";
            Control_ItemTypeManagement_TableLayout_Remove.Padding = new Padding(16);
            Control_ItemTypeManagement_TableLayout_Remove.RowCount = 2;
            Control_ItemTypeManagement_TableLayout_Remove.RowStyles.Add(new RowStyle());
            Control_ItemTypeManagement_TableLayout_Remove.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_ItemTypeManagement_TableLayout_Remove.Size = new Size(440, 252);
            Control_ItemTypeManagement_TableLayout_Remove.TabIndex = 0;
            // 
            // Control_ItemTypeManagement_TableLayout_RemoveContent
            // 
            Control_ItemTypeManagement_TableLayout_RemoveContent.AutoSize = true;
            Control_ItemTypeManagement_TableLayout_RemoveContent.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ItemTypeManagement_TableLayout_RemoveContent.ColumnCount = 1;
            Control_ItemTypeManagement_TableLayout_Remove.SetColumnSpan(Control_ItemTypeManagement_TableLayout_RemoveContent, 2);
            Control_ItemTypeManagement_TableLayout_RemoveContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_ItemTypeManagement_TableLayout_RemoveContent.Controls.Add(Control_ItemTypeManagement_Suggestion_RemoveSelectItemType, 0, 0);
            Control_ItemTypeManagement_TableLayout_RemoveContent.Controls.Add(Control_ItemTypeManagement_TableLayout_RemoveDetails, 0, 1);
            Control_ItemTypeManagement_TableLayout_RemoveContent.Controls.Add(Control_ItemTypeManagement_Label_RemoveWarning, 0, 3);
            Control_ItemTypeManagement_TableLayout_RemoveContent.Controls.Add(Control_ItemTypeManagement_TableLayout_RemoveActions, 4, 3);
            Control_ItemTypeManagement_TableLayout_RemoveContent.Dock = DockStyle.Top;
            Control_ItemTypeManagement_TableLayout_RemoveContent.Location = new Point(19, 82);
            Control_ItemTypeManagement_TableLayout_RemoveContent.Name = "Control_ItemTypeManagement_TableLayout_RemoveContent";
            Control_ItemTypeManagement_TableLayout_RemoveContent.RowCount = 5;
            Control_ItemTypeManagement_TableLayout_RemoveContent.RowStyles.Add(new RowStyle());
            Control_ItemTypeManagement_TableLayout_RemoveContent.RowStyles.Add(new RowStyle());
            Control_ItemTypeManagement_TableLayout_RemoveContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_ItemTypeManagement_TableLayout_RemoveContent.RowStyles.Add(new RowStyle());
            Control_ItemTypeManagement_TableLayout_RemoveContent.RowStyles.Add(new RowStyle());
            Control_ItemTypeManagement_TableLayout_RemoveContent.Size = new Size(402, 151);
            Control_ItemTypeManagement_TableLayout_RemoveContent.TabIndex = 2;
            // 
            // Control_ItemTypeManagement_Suggestion_RemoveSelectItemType
            // 
            Control_ItemTypeManagement_Suggestion_RemoveSelectItemType.AutoSize = true;
            Control_ItemTypeManagement_Suggestion_RemoveSelectItemType.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ItemTypeManagement_Suggestion_RemoveSelectItemType.BorderStyle = BorderStyle.FixedSingle;
            Control_ItemTypeManagement_Suggestion_RemoveSelectItemType.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_Suggestion_RemoveSelectItemType.LabelText = "Select Item Type";
            Control_ItemTypeManagement_Suggestion_RemoveSelectItemType.Location = new Point(3, 3);
            Control_ItemTypeManagement_Suggestion_RemoveSelectItemType.Margin = new Padding(3, 3, 3, 10);
            Control_ItemTypeManagement_Suggestion_RemoveSelectItemType.MaxLength = 130;
            Control_ItemTypeManagement_Suggestion_RemoveSelectItemType.MinimumSize = new Size(0, 23);
            Control_ItemTypeManagement_Suggestion_RemoveSelectItemType.MinLength = 130;
            Control_ItemTypeManagement_Suggestion_RemoveSelectItemType.Name = "Control_ItemTypeManagement_Suggestion_RemoveSelectItemType";
            Control_ItemTypeManagement_Suggestion_RemoveSelectItemType.Padding = new Padding(3);
            Control_ItemTypeManagement_Suggestion_RemoveSelectItemType.PlaceholderText = "Search item types (F4)";
            Control_ItemTypeManagement_Suggestion_RemoveSelectItemType.Size = new Size(396, 31);
            Control_ItemTypeManagement_Suggestion_RemoveSelectItemType.TabIndex = 0;
            // 
            // Control_ItemTypeManagement_TableLayout_RemoveDetails
            // 
            Control_ItemTypeManagement_TableLayout_RemoveDetails.AutoSize = true;
            Control_ItemTypeManagement_TableLayout_RemoveDetails.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ItemTypeManagement_TableLayout_RemoveDetails.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            Control_ItemTypeManagement_TableLayout_RemoveDetails.ColumnCount = 2;
            Control_ItemTypeManagement_TableLayout_RemoveDetails.ColumnStyles.Add(new ColumnStyle());
            Control_ItemTypeManagement_TableLayout_RemoveDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_ItemTypeManagement_TableLayout_RemoveDetails.Controls.Add(Control_ItemTypeManagement_Label_RemoveItemType, 0, 0);
            Control_ItemTypeManagement_TableLayout_RemoveDetails.Controls.Add(Control_ItemTypeManagement_Label_RemoveItemTypeValue, 1, 0);
            Control_ItemTypeManagement_TableLayout_RemoveDetails.Controls.Add(Control_ItemTypeManagement_Label_RemoveIssuedBy, 0, 1);
            Control_ItemTypeManagement_TableLayout_RemoveDetails.Controls.Add(Control_ItemTypeManagement_Label_RemoveIssuedByValue, 1, 1);
            Control_ItemTypeManagement_TableLayout_RemoveDetails.Dock = DockStyle.Top;
            Control_ItemTypeManagement_TableLayout_RemoveDetails.Location = new Point(3, 47);
            Control_ItemTypeManagement_TableLayout_RemoveDetails.Name = "Control_ItemTypeManagement_TableLayout_RemoveDetails";
            Control_ItemTypeManagement_TableLayout_RemoveDetails.RowCount = 2;
            Control_ItemTypeManagement_TableLayout_RemoveDetails.RowStyles.Add(new RowStyle());
            Control_ItemTypeManagement_TableLayout_RemoveDetails.RowStyles.Add(new RowStyle());
            Control_ItemTypeManagement_TableLayout_RemoveDetails.Size = new Size(396, 45);
            Control_ItemTypeManagement_TableLayout_RemoveDetails.TabIndex = 1;
            // 
            // Control_ItemTypeManagement_Label_RemoveItemType
            // 
            Control_ItemTypeManagement_Label_RemoveItemType.AutoSize = true;
            Control_ItemTypeManagement_Label_RemoveItemType.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_Label_RemoveItemType.Location = new Point(4, 4);
            Control_ItemTypeManagement_Label_RemoveItemType.Margin = new Padding(3);
            Control_ItemTypeManagement_Label_RemoveItemType.Name = "Control_ItemTypeManagement_Label_RemoveItemType";
            Control_ItemTypeManagement_Label_RemoveItemType.Size = new Size(62, 15);
            Control_ItemTypeManagement_Label_RemoveItemType.TabIndex = 0;
            Control_ItemTypeManagement_Label_RemoveItemType.Text = "Item Type:";
            Control_ItemTypeManagement_Label_RemoveItemType.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Control_ItemTypeManagement_Label_RemoveItemTypeValue
            // 
            Control_ItemTypeManagement_Label_RemoveItemTypeValue.AutoSize = true;
            Control_ItemTypeManagement_Label_RemoveItemTypeValue.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_Label_RemoveItemTypeValue.Location = new Point(73, 4);
            Control_ItemTypeManagement_Label_RemoveItemTypeValue.Margin = new Padding(3);
            Control_ItemTypeManagement_Label_RemoveItemTypeValue.Name = "Control_ItemTypeManagement_Label_RemoveItemTypeValue";
            Control_ItemTypeManagement_Label_RemoveItemTypeValue.Size = new Size(319, 15);
            Control_ItemTypeManagement_Label_RemoveItemTypeValue.TabIndex = 1;
            Control_ItemTypeManagement_Label_RemoveItemTypeValue.Text = "{Value}";
            // 
            // Control_ItemTypeManagement_Label_RemoveIssuedBy
            // 
            Control_ItemTypeManagement_Label_RemoveIssuedBy.AutoSize = true;
            Control_ItemTypeManagement_Label_RemoveIssuedBy.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_Label_RemoveIssuedBy.Location = new Point(4, 26);
            Control_ItemTypeManagement_Label_RemoveIssuedBy.Margin = new Padding(3);
            Control_ItemTypeManagement_Label_RemoveIssuedBy.Name = "Control_ItemTypeManagement_Label_RemoveIssuedBy";
            Control_ItemTypeManagement_Label_RemoveIssuedBy.Size = new Size(62, 15);
            Control_ItemTypeManagement_Label_RemoveIssuedBy.TabIndex = 2;
            Control_ItemTypeManagement_Label_RemoveIssuedBy.Text = "Issued By:";
            Control_ItemTypeManagement_Label_RemoveIssuedBy.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Control_ItemTypeManagement_Label_RemoveIssuedByValue
            // 
            Control_ItemTypeManagement_Label_RemoveIssuedByValue.AutoSize = true;
            Control_ItemTypeManagement_Label_RemoveIssuedByValue.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_Label_RemoveIssuedByValue.Location = new Point(73, 26);
            Control_ItemTypeManagement_Label_RemoveIssuedByValue.Margin = new Padding(3);
            Control_ItemTypeManagement_Label_RemoveIssuedByValue.Name = "Control_ItemTypeManagement_Label_RemoveIssuedByValue";
            Control_ItemTypeManagement_Label_RemoveIssuedByValue.Size = new Size(319, 15);
            Control_ItemTypeManagement_Label_RemoveIssuedByValue.TabIndex = 3;
            Control_ItemTypeManagement_Label_RemoveIssuedByValue.Text = "{Value}";
            // 
            // Control_ItemTypeManagement_Label_RemoveWarning
            // 
            Control_ItemTypeManagement_Label_RemoveWarning.AutoSize = true;
            Control_ItemTypeManagement_Label_RemoveWarning.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_Label_RemoveWarning.ForeColor = Color.FromArgb(192, 0, 0);
            Control_ItemTypeManagement_Label_RemoveWarning.Location = new Point(3, 98);
            Control_ItemTypeManagement_Label_RemoveWarning.Margin = new Padding(3);
            Control_ItemTypeManagement_Label_RemoveWarning.Name = "Control_ItemTypeManagement_Label_RemoveWarning";
            Control_ItemTypeManagement_Label_RemoveWarning.Size = new Size(396, 15);
            Control_ItemTypeManagement_Label_RemoveWarning.TabIndex = 2;
            Control_ItemTypeManagement_Label_RemoveWarning.Text = "Warning: Removal is permanent.";
            // 
            // Control_ItemTypeManagement_TableLayout_RemoveActions
            // 
            Control_ItemTypeManagement_TableLayout_RemoveActions.AutoSize = true;
            Control_ItemTypeManagement_TableLayout_RemoveActions.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ItemTypeManagement_TableLayout_RemoveActions.ColumnCount = 3;
            Control_ItemTypeManagement_TableLayout_RemoveActions.ColumnStyles.Add(new ColumnStyle());
            Control_ItemTypeManagement_TableLayout_RemoveActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_ItemTypeManagement_TableLayout_RemoveActions.ColumnStyles.Add(new ColumnStyle());
            Control_ItemTypeManagement_TableLayout_RemoveActions.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            Control_ItemTypeManagement_TableLayout_RemoveActions.Controls.Add(Control_ItemTypeManagement_Button_RemoveConfirm, 0, 0);
            Control_ItemTypeManagement_TableLayout_RemoveActions.Controls.Add(Control_ItemTypeManagement_Button_RemoveCancel, 2, 0);
            Control_ItemTypeManagement_TableLayout_RemoveActions.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_TableLayout_RemoveActions.Location = new Point(3, 119);
            Control_ItemTypeManagement_TableLayout_RemoveActions.Name = "Control_ItemTypeManagement_TableLayout_RemoveActions";
            Control_ItemTypeManagement_TableLayout_RemoveActions.RowCount = 1;
            Control_ItemTypeManagement_TableLayout_RemoveActions.RowStyles.Add(new RowStyle());
            Control_ItemTypeManagement_TableLayout_RemoveActions.Size = new Size(396, 29);
            Control_ItemTypeManagement_TableLayout_RemoveActions.TabIndex = 3;
            // 
            // Control_ItemTypeManagement_Button_RemoveConfirm
            // 
            Control_ItemTypeManagement_Button_RemoveConfirm.Location = new Point(3, 3);
            Control_ItemTypeManagement_Button_RemoveConfirm.Name = "Control_ItemTypeManagement_Button_RemoveConfirm";
            Control_ItemTypeManagement_Button_RemoveConfirm.Size = new Size(75, 23);
            Control_ItemTypeManagement_Button_RemoveConfirm.TabIndex = 0;
            // 
            // Control_ItemTypeManagement_Button_RemoveCancel
            // 
            Control_ItemTypeManagement_Button_RemoveCancel.Location = new Point(318, 3);
            Control_ItemTypeManagement_Button_RemoveCancel.Name = "Control_ItemTypeManagement_Button_RemoveCancel";
            Control_ItemTypeManagement_Button_RemoveCancel.Size = new Size(75, 23);
            Control_ItemTypeManagement_Button_RemoveCancel.TabIndex = 1;
            // 
            // Control_ItemTypeManagement_TableLayout_RemoveHeader
            // 
            Control_ItemTypeManagement_TableLayout_RemoveHeader.AutoSize = true;
            Control_ItemTypeManagement_TableLayout_RemoveHeader.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ItemTypeManagement_TableLayout_RemoveHeader.ColumnCount = 2;
            Control_ItemTypeManagement_TableLayout_RemoveHeader.ColumnStyles.Add(new ColumnStyle());
            Control_ItemTypeManagement_TableLayout_RemoveHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_ItemTypeManagement_TableLayout_RemoveHeader.Controls.Add(Control_ItemTypeManagement_Label_RemoveIcon, 0, 0);
            Control_ItemTypeManagement_TableLayout_RemoveHeader.Controls.Add(Control_ItemTypeManagement_Label_RemoveTitle, 1, 0);
            Control_ItemTypeManagement_TableLayout_RemoveHeader.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_TableLayout_RemoveHeader.Location = new Point(19, 19);
            Control_ItemTypeManagement_TableLayout_RemoveHeader.Name = "Control_ItemTypeManagement_TableLayout_RemoveHeader";
            Control_ItemTypeManagement_TableLayout_RemoveHeader.RowCount = 1;
            Control_ItemTypeManagement_TableLayout_RemoveHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_ItemTypeManagement_TableLayout_RemoveHeader.Size = new Size(402, 57);
            Control_ItemTypeManagement_TableLayout_RemoveHeader.TabIndex = 3;
            // 
            // Control_ItemTypeManagement_Label_RemoveIcon
            // 
            Control_ItemTypeManagement_Label_RemoveIcon.AutoSize = true;
            Control_ItemTypeManagement_Label_RemoveIcon.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_Label_RemoveIcon.Font = new Font("Segoe UI Emoji", 28F);
            Control_ItemTypeManagement_Label_RemoveIcon.Location = new Point(3, 3);
            Control_ItemTypeManagement_Label_RemoveIcon.Margin = new Padding(3);
            Control_ItemTypeManagement_Label_RemoveIcon.Name = "Control_ItemTypeManagement_Label_RemoveIcon";
            Control_ItemTypeManagement_Label_RemoveIcon.Size = new Size(74, 51);
            Control_ItemTypeManagement_Label_RemoveIcon.TabIndex = 0;
            Control_ItemTypeManagement_Label_RemoveIcon.Text = "🗑️";
            Control_ItemTypeManagement_Label_RemoveIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_ItemTypeManagement_Label_RemoveTitle
            // 
            Control_ItemTypeManagement_Label_RemoveTitle.AutoSize = true;
            Control_ItemTypeManagement_Label_RemoveTitle.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_Label_RemoveTitle.Font = new Font("Segoe UI Emoji", 16F, FontStyle.Bold);
            Control_ItemTypeManagement_Label_RemoveTitle.Location = new Point(83, 3);
            Control_ItemTypeManagement_Label_RemoveTitle.Margin = new Padding(3);
            Control_ItemTypeManagement_Label_RemoveTitle.Name = "Control_ItemTypeManagement_Label_RemoveTitle";
            Control_ItemTypeManagement_Label_RemoveTitle.Size = new Size(316, 51);
            Control_ItemTypeManagement_Label_RemoveTitle.TabIndex = 1;
            Control_ItemTypeManagement_Label_RemoveTitle.Text = "Remove Item Type";
            Control_ItemTypeManagement_Label_RemoveTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_ItemTypeManagement_TableLayout_BackButton
            // 
            Control_ItemTypeManagement_TableLayout_BackButton.AutoSize = true;
            Control_ItemTypeManagement_TableLayout_BackButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_ItemTypeManagement_TableLayout_BackButton.ColumnCount = 4;
            Control_ItemTypeManagement_TableLayout_BackButton.ColumnStyles.Add(new ColumnStyle());
            Control_ItemTypeManagement_TableLayout_BackButton.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_ItemTypeManagement_TableLayout_BackButton.ColumnStyles.Add(new ColumnStyle());
            Control_ItemTypeManagement_TableLayout_BackButton.ColumnStyles.Add(new ColumnStyle());
            Control_ItemTypeManagement_TableLayout_BackButton.Controls.Add(Control_ItemTypeManagement_Button_Back, 0, 0);
            Control_ItemTypeManagement_TableLayout_BackButton.Controls.Add(Control_ItemTypeManagement_Button_Home, 2, 0);
            Control_ItemTypeManagement_TableLayout_BackButton.Controls.Add(SettingsForm_Button_Help_ItemTypes, 3, 0);
            Control_ItemTypeManagement_TableLayout_BackButton.Dock = DockStyle.Fill;
            Control_ItemTypeManagement_TableLayout_BackButton.Location = new Point(23, 322);
            Control_ItemTypeManagement_TableLayout_BackButton.Name = "Control_ItemTypeManagement_TableLayout_BackButton";
            Control_ItemTypeManagement_TableLayout_BackButton.RowStyles.Add(new RowStyle());
            Control_ItemTypeManagement_TableLayout_BackButton.Size = new Size(446, 38);
            Control_ItemTypeManagement_TableLayout_BackButton.TabIndex = 2;
            // 
            // Control_ItemTypeManagement_Button_Back
            // 
            Control_ItemTypeManagement_Button_Back.AutoSize = true;
            Control_ItemTypeManagement_Button_Back.Location = new Point(3, 3);
            Control_ItemTypeManagement_Button_Back.MaximumSize = new Size(0, 32);
            Control_ItemTypeManagement_Button_Back.MinimumSize = new Size(0, 32);
            Control_ItemTypeManagement_Button_Back.Name = "Control_ItemTypeManagement_Button_Back";
            Control_ItemTypeManagement_Button_Back.Padding = new Padding(16, 6, 16, 6);
            Control_ItemTypeManagement_Button_Back.Size = new Size(152, 32);
            Control_ItemTypeManagement_Button_Back.TabIndex = 0;
            Control_ItemTypeManagement_Button_Back.Text = "← Back to Selection";
            Control_ItemTypeManagement_Button_Back.Visible = false;
            // 
            // Control_ItemTypeManagement_Button_Home
            // 
            Control_ItemTypeManagement_Button_Home.AutoSize = true;
            Control_ItemTypeManagement_Button_Home.Location = new Point(266, 3);
            Control_ItemTypeManagement_Button_Home.MaximumSize = new Size(0, 32);
            Control_ItemTypeManagement_Button_Home.MinimumSize = new Size(0, 32);
            Control_ItemTypeManagement_Button_Home.Name = "Control_ItemTypeManagement_Button_Home";
            Control_ItemTypeManagement_Button_Home.Padding = new Padding(16, 6, 16, 6);
            Control_ItemTypeManagement_Button_Home.Size = new Size(139, 32);
            Control_ItemTypeManagement_Button_Home.TabIndex = 1;
            Control_ItemTypeManagement_Button_Home.Text = "🏠 Back to Home";
            // 
            // Control_ItemTypeManagement_Label_AddIssuedBy
            // 
            Control_ItemTypeManagement_Label_AddIssuedBy.Location = new Point(0, 0);
            Control_ItemTypeManagement_Label_AddIssuedBy.Name = "Control_ItemTypeManagement_Label_AddIssuedBy";
            Control_ItemTypeManagement_Label_AddIssuedBy.Size = new Size(100, 23);
            Control_ItemTypeManagement_Label_AddIssuedBy.TabIndex = 0;
            // 
            // Control_ItemTypeManagement_Label_AddIssuedByValue
            // 
            Control_ItemTypeManagement_Label_AddIssuedByValue.Location = new Point(0, 0);
            Control_ItemTypeManagement_Label_AddIssuedByValue.Name = "Control_ItemTypeManagement_Label_AddIssuedByValue";
            Control_ItemTypeManagement_Label_AddIssuedByValue.Size = new Size(100, 23);
            Control_ItemTypeManagement_Label_AddIssuedByValue.TabIndex = 0;
            // 
            // Control_ItemTypeManagement_Label_EditIssuedBy
            // 
            Control_ItemTypeManagement_Label_EditIssuedBy.Location = new Point(0, 0);
            Control_ItemTypeManagement_Label_EditIssuedBy.Name = "Control_ItemTypeManagement_Label_EditIssuedBy";
            Control_ItemTypeManagement_Label_EditIssuedBy.Size = new Size(100, 23);
            Control_ItemTypeManagement_Label_EditIssuedBy.TabIndex = 0;
            // 
            // Control_ItemTypeManagement_Label_EditIssuedByValue
            // 
            Control_ItemTypeManagement_Label_EditIssuedByValue.Location = new Point(0, 0);
            Control_ItemTypeManagement_Label_EditIssuedByValue.Name = "Control_ItemTypeManagement_Label_EditIssuedByValue";
            Control_ItemTypeManagement_Label_EditIssuedByValue.Size = new Size(100, 23);
            Control_ItemTypeManagement_Label_EditIssuedByValue.TabIndex = 0;
            // 
            // SettingsForm_Button_Help_ItemTypes
            // 
            SettingsForm_Button_Help_ItemTypes.AutoSize = true;
            SettingsForm_Button_Help_ItemTypes.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            SettingsForm_Button_Help_ItemTypes.Dock = DockStyle.Fill;
            SettingsForm_Button_Help_ItemTypes.Location = new Point(411, 3);
            SettingsForm_Button_Help_ItemTypes.MaximumSize = new Size(32, 32);
            SettingsForm_Button_Help_ItemTypes.MinimumSize = new Size(32, 32);
            SettingsForm_Button_Help_ItemTypes.Name = "SettingsForm_Button_Help_ItemTypes";
            SettingsForm_Button_Help_ItemTypes.Size = new Size(32, 32);
            SettingsForm_Button_Help_ItemTypes.TabIndex = 14;
            SettingsForm_Button_Help_ItemTypes.Text = "?";
            SettingsForm_Button_Help_ItemTypes.UseVisualStyleBackColor = true;
            // 
            // Control_ItemTypeManagement
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(Control_ItemTypeManagement_TableLayout_Main);
            Name = "Control_ItemTypeManagement";
            Size = new Size(492, 383);
            Control_ItemTypeManagement_TableLayout_Main.ResumeLayout(false);
            Control_ItemTypeManagement_TableLayout_Main.PerformLayout();
            Control_ItemTypeManagement_Panel_Container.ResumeLayout(false);
            Control_ItemTypeManagement_Panel_Container.PerformLayout();
            Control_ItemTypeManagement_Panel_Home.ResumeLayout(false);
            Control_ItemTypeManagement_Panel_Home.PerformLayout();
            Control_ItemTypeManagement_TableLayout_Home.ResumeLayout(false);
            Control_ItemTypeManagement_TableLayout_Home.PerformLayout();
            Control_ItemTypeManagement_Panel_HomeTile_Add.ResumeLayout(false);
            Control_ItemTypeManagement_Panel_HomeTile_Add.PerformLayout();
            Control_ItemTypeManagement_TableLayout_HomeTile_Add.ResumeLayout(false);
            Control_ItemTypeManagement_TableLayout_HomeTile_Add.PerformLayout();
            Control_ItemTypeManagement_Panel_HomeTile_Edit.ResumeLayout(false);
            Control_ItemTypeManagement_Panel_HomeTile_Edit.PerformLayout();
            Control_ItemTypeManagement_TableLayout_HomeTile_Edit.ResumeLayout(false);
            Control_ItemTypeManagement_TableLayout_HomeTile_Edit.PerformLayout();
            Control_ItemTypeManagement_Panel_HomeTile_Remove.ResumeLayout(false);
            Control_ItemTypeManagement_Panel_HomeTile_Remove.PerformLayout();
            Control_ItemTypeManagement_TableLayout_HomeTile_Remove.ResumeLayout(false);
            Control_ItemTypeManagement_TableLayout_HomeTile_Remove.PerformLayout();
            Control_ItemTypeManagement_TableLayout_Cards.ResumeLayout(false);
            Control_ItemTypeManagement_TableLayout_Cards.PerformLayout();
            Control_ItemTypeManagement_Panel_AddCard.ResumeLayout(false);
            Control_ItemTypeManagement_Panel_AddCard.PerformLayout();
            Control_ItemTypeManagement_TableLayout_Add.ResumeLayout(false);
            Control_ItemTypeManagement_TableLayout_Add.PerformLayout();
            Control_ItemTypeManagement_TableLayout_AddHeader.ResumeLayout(false);
            Control_ItemTypeManagement_TableLayout_AddHeader.PerformLayout();
            Control_ItemTypeManagement_TableLayout_AddContent.ResumeLayout(false);
            Control_ItemTypeManagement_TableLayout_AddContent.PerformLayout();
            Control_ItemTypeManagement_TableLayout_AddActions.ResumeLayout(false);
            Control_ItemTypeManagement_Panel_EditCard.ResumeLayout(false);
            Control_ItemTypeManagement_Panel_EditCard.PerformLayout();
            Control_ItemTypeManagement_TableLayout_Edit.ResumeLayout(false);
            Control_ItemTypeManagement_TableLayout_Edit.PerformLayout();
            Control_ItemTypeManagement_TableLayout_EditHeader.ResumeLayout(false);
            Control_ItemTypeManagement_TableLayout_EditHeader.PerformLayout();
            Control_ItemTypeManagement_TableLayout_EditContent.ResumeLayout(false);
            Control_ItemTypeManagement_TableLayout_EditContent.PerformLayout();
            Control_ItemTypeManagement_TableLayout_EditActions.ResumeLayout(false);
            Control_ItemTypeManagement_Panel_RemoveCard.ResumeLayout(false);
            Control_ItemTypeManagement_Panel_RemoveCard.PerformLayout();
            Control_ItemTypeManagement_TableLayout_Remove.ResumeLayout(false);
            Control_ItemTypeManagement_TableLayout_Remove.PerformLayout();
            Control_ItemTypeManagement_TableLayout_RemoveContent.ResumeLayout(false);
            Control_ItemTypeManagement_TableLayout_RemoveContent.PerformLayout();
            Control_ItemTypeManagement_TableLayout_RemoveDetails.ResumeLayout(false);
            Control_ItemTypeManagement_TableLayout_RemoveDetails.PerformLayout();
            Control_ItemTypeManagement_TableLayout_RemoveActions.ResumeLayout(false);
            Control_ItemTypeManagement_TableLayout_RemoveHeader.ResumeLayout(false);
            Control_ItemTypeManagement_TableLayout_RemoveHeader.PerformLayout();
            Control_ItemTypeManagement_TableLayout_BackButton.ResumeLayout(false);
            Control_ItemTypeManagement_TableLayout_BackButton.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TableLayoutPanel Control_ItemTypeManagement_TableLayout_EditHeader;
        private TableLayoutPanel Control_ItemTypeManagement_TableLayout_RemoveHeader;
        private TableLayoutPanel Control_ItemTypeManagement_TableLayout_AddHeader;
        private Button Control_ItemTypeManagement_Button_Home;
        private Button SettingsForm_Button_Help_ItemTypes;
    }
}
