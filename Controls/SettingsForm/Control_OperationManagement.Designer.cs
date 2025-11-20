using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Controls.Shared;

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
        private SuggestionTextBoxWithLabel Control_OperationManagement_TextBox_AddOperation = null!;
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
        private SuggestionTextBoxWithLabel Control_OperationManagement_Suggestion_EditSelectOperation = null!;
        private SuggestionTextBoxWithLabel Control_OperationManagement_TextBox_EditNewOperation = null!;
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
        private SuggestionTextBoxWithLabel Control_OperationManagement_Suggestion_RemoveSelectOperation = null!;
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
            Control_OperationManagement_Panel_Container = new Panel();
            Control_OperationManagement_TableLayout_Cards = new TableLayoutPanel();
            Control_OperationManagement_Panel_AddCard = new Panel();
            Control_OperationManagement_TableLayout_Add = new TableLayoutPanel();
            Control_OperationManagement_Label_AddIcon = new Label();
            Control_OperationManagement_Label_AddTitle = new Label();
            Control_OperationManagement_TableLayout_AddContent = new TableLayoutPanel();
            Control_OperationManagement_TextBox_AddOperation = new SuggestionTextBoxWithLabel();
            Control_OperationManagement_TableLayout_AddActions = new TableLayoutPanel();
            Control_OperationManagement_Button_AddSave = new Button();
            Control_OperationManagement_Button_AddClear = new Button();
            Control_OperationManagement_Panel_EditCard = new Panel();
            Control_OperationManagement_TableLayout_Edit = new TableLayoutPanel();
            Control_OperationManagement_Label_EditIcon = new Label();
            Control_OperationManagement_Label_EditTitle = new Label();
            Control_OperationManagement_TableLayout_EditContent = new TableLayoutPanel();
            Control_OperationManagement_Suggestion_EditSelectOperation = new SuggestionTextBoxWithLabel();
            Control_OperationManagement_TextBox_EditNewOperation = new SuggestionTextBoxWithLabel();
            Control_OperationManagement_TableLayout_EditActions = new TableLayoutPanel();
            Control_OperationManagement_Button_EditSave = new Button();
            Control_OperationManagement_Button_EditReset = new Button();
            Control_OperationManagement_Panel_RemoveCard = new Panel();
            Control_OperationManagement_TableLayout_Remove = new TableLayoutPanel();
            Control_OperationManagement_Label_RemoveIcon = new Label();
            Control_OperationManagement_Label_RemoveTitle = new Label();
            Control_OperationManagement_TableLayout_RemoveContent = new TableLayoutPanel();
            Control_OperationManagement_Suggestion_RemoveSelectOperation = new SuggestionTextBoxWithLabel();
            Control_OperationManagement_TableLayout_RemoveDetails = new TableLayoutPanel();
            Control_OperationManagement_Label_RemoveOperation = new Label();
            Control_OperationManagement_Label_RemoveOperationValue = new Label();
            Control_OperationManagement_Label_RemoveIssuedBy = new Label();
            Control_OperationManagement_Label_RemoveIssuedByValue = new Label();
            Control_OperationManagement_Label_RemoveWarning = new Label();
            Control_OperationManagement_TableLayout_RemoveActions = new TableLayoutPanel();
            Control_OperationManagement_Button_RemoveConfirm = new Button();
            Control_OperationManagement_Button_RemoveCancel = new Button();
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
            Control_OperationManagement_TableLayout_BackButton = new TableLayoutPanel();
            Control_OperationManagement_Button_Back = new Button();
            Control_OperationManagement_Label_AddIssuedBy = new Label();
            Control_OperationManagement_Label_AddIssuedByValue = new Label();
            Control_OperationManagement_Label_EditIssuedBy = new Label();
            Control_OperationManagement_Label_EditIssuedByValue = new Label();
            Control_OperationManagement_TableLayout_Main.SuspendLayout();
            Control_OperationManagement_Panel_Container.SuspendLayout();
            Control_OperationManagement_TableLayout_Cards.SuspendLayout();
            Control_OperationManagement_Panel_AddCard.SuspendLayout();
            Control_OperationManagement_TableLayout_Add.SuspendLayout();
            Control_OperationManagement_TableLayout_AddContent.SuspendLayout();
            Control_OperationManagement_TableLayout_AddActions.SuspendLayout();
            Control_OperationManagement_Panel_EditCard.SuspendLayout();
            Control_OperationManagement_TableLayout_Edit.SuspendLayout();
            Control_OperationManagement_TableLayout_EditContent.SuspendLayout();
            Control_OperationManagement_TableLayout_EditActions.SuspendLayout();
            Control_OperationManagement_Panel_RemoveCard.SuspendLayout();
            Control_OperationManagement_TableLayout_Remove.SuspendLayout();
            Control_OperationManagement_TableLayout_RemoveContent.SuspendLayout();
            Control_OperationManagement_TableLayout_RemoveDetails.SuspendLayout();
            Control_OperationManagement_TableLayout_RemoveActions.SuspendLayout();
            Control_OperationManagement_Panel_Home.SuspendLayout();
            Control_OperationManagement_TableLayout_Home.SuspendLayout();
            Control_OperationManagement_Panel_HomeTile_Add.SuspendLayout();
            Control_OperationManagement_TableLayout_HomeTile_Add.SuspendLayout();
            Control_OperationManagement_Panel_HomeTile_Edit.SuspendLayout();
            Control_OperationManagement_TableLayout_HomeTile_Edit.SuspendLayout();
            Control_OperationManagement_Panel_HomeTile_Remove.SuspendLayout();
            Control_OperationManagement_TableLayout_HomeTile_Remove.SuspendLayout();
            Control_OperationManagement_TableLayout_BackButton.SuspendLayout();
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
            Control_OperationManagement_TableLayout_Main.Controls.Add(Control_OperationManagement_Panel_Container, 0, 2);
            Control_OperationManagement_TableLayout_Main.Controls.Add(Control_OperationManagement_TableLayout_BackButton, 0, 3);
            Control_OperationManagement_TableLayout_Main.Dock = DockStyle.Fill;
            Control_OperationManagement_TableLayout_Main.Location = new Point(0, 0);
            Control_OperationManagement_TableLayout_Main.Name = "Control_OperationManagement_TableLayout_Main";
            Control_OperationManagement_TableLayout_Main.Padding = new Padding(20);
            Control_OperationManagement_TableLayout_Main.RowCount = 4;
            Control_OperationManagement_TableLayout_Main.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_Main.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_Main.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_Main.Size = new Size(406, 785);
            Control_OperationManagement_TableLayout_Main.TabIndex = 0;
            // 
            // Control_OperationManagement_Label_Header
            // 
            Control_OperationManagement_Label_Header.AutoSize = true;
            Control_OperationManagement_Label_Header.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_Header.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            Control_OperationManagement_Label_Header.ForeColor = Color.FromArgb(45, 45, 45);
            Control_OperationManagement_Label_Header.Location = new Point(23, 23);
            Control_OperationManagement_Label_Header.Margin = new Padding(3);
            Control_OperationManagement_Label_Header.Name = "Control_OperationManagement_Label_Header";
            Control_OperationManagement_Label_Header.Size = new Size(360, 37);
            Control_OperationManagement_Label_Header.TabIndex = 0;
            Control_OperationManagement_Label_Header.Text = "Operation Codes";
            // 
            // Control_OperationManagement_Label_Subtitle
            // 
            Control_OperationManagement_Label_Subtitle.AutoSize = true;
            Control_OperationManagement_Label_Subtitle.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_Subtitle.Font = new Font("Segoe UI", 10F);
            Control_OperationManagement_Label_Subtitle.ForeColor = Color.FromArgb(90, 90, 90);
            Control_OperationManagement_Label_Subtitle.Location = new Point(23, 66);
            Control_OperationManagement_Label_Subtitle.Margin = new Padding(3);
            Control_OperationManagement_Label_Subtitle.Name = "Control_OperationManagement_Label_Subtitle";
            Control_OperationManagement_Label_Subtitle.Size = new Size(360, 19);
            Control_OperationManagement_Label_Subtitle.TabIndex = 1;
            Control_OperationManagement_Label_Subtitle.Text = "Select an action below to manage operation codes.";
            // 
            // Control_OperationManagement_Panel_Container
            // 
            Control_OperationManagement_Panel_Container.AutoSize = true;
            Control_OperationManagement_Panel_Container.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_Panel_Container.Controls.Add(Control_OperationManagement_Panel_Home);
            Control_OperationManagement_Panel_Container.Controls.Add(Control_OperationManagement_TableLayout_Cards);
            Control_OperationManagement_Panel_Container.Dock = DockStyle.Fill;
            Control_OperationManagement_Panel_Container.Location = new Point(23, 91);
            Control_OperationManagement_Panel_Container.Name = "Control_OperationManagement_Panel_Container";
            Control_OperationManagement_Panel_Container.Size = new Size(360, 622);
            Control_OperationManagement_Panel_Container.TabIndex = 2;
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
            Control_OperationManagement_TableLayout_Cards.Size = new Size(360, 622);
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
            Control_OperationManagement_Panel_AddCard.Size = new Size(354, 163);
            Control_OperationManagement_Panel_AddCard.TabIndex = 0;
            // 
            // Control_OperationManagement_TableLayout_Add
            // 
            Control_OperationManagement_TableLayout_Add.AutoSize = true;
            Control_OperationManagement_TableLayout_Add.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_Add.ColumnCount = 2;
            Control_OperationManagement_TableLayout_Add.ColumnStyles.Add(new ColumnStyle());
            Control_OperationManagement_TableLayout_Add.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_Add.Controls.Add(Control_OperationManagement_Label_AddIcon, 0, 0);
            Control_OperationManagement_TableLayout_Add.Controls.Add(Control_OperationManagement_Label_AddTitle, 1, 0);
            Control_OperationManagement_TableLayout_Add.Controls.Add(Control_OperationManagement_TableLayout_AddContent, 0, 1);
            Control_OperationManagement_TableLayout_Add.Dock = DockStyle.Fill;
            Control_OperationManagement_TableLayout_Add.Location = new Point(0, 0);
            Control_OperationManagement_TableLayout_Add.Name = "Control_OperationManagement_TableLayout_Add";
            Control_OperationManagement_TableLayout_Add.Padding = new Padding(16);
            Control_OperationManagement_TableLayout_Add.RowCount = 2;
            Control_OperationManagement_TableLayout_Add.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_Add.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_Add.Size = new Size(352, 161);
            Control_OperationManagement_TableLayout_Add.TabIndex = 0;
            // 
            // Control_OperationManagement_Label_AddIcon
            // 
            Control_OperationManagement_Label_AddIcon.AutoSize = true;
            Control_OperationManagement_Label_AddIcon.Font = new Font("Segoe UI Emoji", 28F);
            Control_OperationManagement_Label_AddIcon.Location = new Point(16, 16);
            Control_OperationManagement_Label_AddIcon.Margin = new Padding(0, 0, 12, 0);
            Control_OperationManagement_Label_AddIcon.Name = "Control_OperationManagement_Label_AddIcon";
            Control_OperationManagement_Label_AddIcon.Size = new Size(74, 51);
            Control_OperationManagement_Label_AddIcon.TabIndex = 0;
            Control_OperationManagement_Label_AddIcon.Text = "🆕";
            Control_OperationManagement_Label_AddIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_OperationManagement_Label_AddTitle
            // 
            Control_OperationManagement_Label_AddTitle.AutoSize = true;
            Control_OperationManagement_Label_AddTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            Control_OperationManagement_Label_AddTitle.Location = new Point(105, 16);
            Control_OperationManagement_Label_AddTitle.Name = "Control_OperationManagement_Label_AddTitle";
            Control_OperationManagement_Label_AddTitle.Size = new Size(167, 30);
            Control_OperationManagement_Label_AddTitle.TabIndex = 1;
            Control_OperationManagement_Label_AddTitle.Text = "Add Operation";
            Control_OperationManagement_Label_AddTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_OperationManagement_TableLayout_AddContent
            // 
            Control_OperationManagement_TableLayout_AddContent.AutoSize = true;
            Control_OperationManagement_TableLayout_AddContent.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_AddContent.ColumnCount = 1;
            Control_OperationManagement_TableLayout_Add.SetColumnSpan(Control_OperationManagement_TableLayout_AddContent, 2);
            Control_OperationManagement_TableLayout_AddContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_AddContent.Controls.Add(Control_OperationManagement_TextBox_AddOperation, 0, 0);
            Control_OperationManagement_TableLayout_AddContent.Controls.Add(Control_OperationManagement_TableLayout_AddActions, 0, 1);
            Control_OperationManagement_TableLayout_AddContent.Dock = DockStyle.Fill;
            Control_OperationManagement_TableLayout_AddContent.Location = new Point(19, 70);
            Control_OperationManagement_TableLayout_AddContent.Name = "Control_OperationManagement_TableLayout_AddContent";
            Control_OperationManagement_TableLayout_AddContent.RowCount = 2;
            Control_OperationManagement_TableLayout_AddContent.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_AddContent.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_AddContent.Size = new Size(314, 72);
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
            Control_OperationManagement_TextBox_AddOperation.Name = "Control_OperationManagement_TextBox_AddOperation";
            Control_OperationManagement_TextBox_AddOperation.Padding = new Padding(3);
            Control_OperationManagement_TextBox_AddOperation.ShowF4Button = false;
            Control_OperationManagement_TextBox_AddOperation.Size = new Size(308, 31);
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
            Control_OperationManagement_TableLayout_AddActions.Size = new Size(308, 29);
            Control_OperationManagement_TableLayout_AddActions.TabIndex = 1;
            // 
            // Control_OperationManagement_Button_AddSave
            // 
            Control_OperationManagement_Button_AddSave.Location = new Point(3, 3);
            Control_OperationManagement_Button_AddSave.Name = "Control_OperationManagement_Button_AddSave";
            Control_OperationManagement_Button_AddSave.Size = new Size(75, 23);
            Control_OperationManagement_Button_AddSave.TabIndex = 0;
            // 
            // Control_OperationManagement_Button_AddClear
            // 
            Control_OperationManagement_Button_AddClear.Location = new Point(230, 3);
            Control_OperationManagement_Button_AddClear.Name = "Control_OperationManagement_Button_AddClear";
            Control_OperationManagement_Button_AddClear.Size = new Size(75, 23);
            Control_OperationManagement_Button_AddClear.TabIndex = 1;
            // 
            // Control_OperationManagement_Panel_EditCard
            // 
            Control_OperationManagement_Panel_EditCard.AutoSize = true;
            Control_OperationManagement_Panel_EditCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_Panel_EditCard.BorderStyle = BorderStyle.FixedSingle;
            Control_OperationManagement_Panel_EditCard.Controls.Add(Control_OperationManagement_TableLayout_Edit);
            Control_OperationManagement_Panel_EditCard.Dock = DockStyle.Fill;
            Control_OperationManagement_Panel_EditCard.Location = new Point(3, 172);
            Control_OperationManagement_Panel_EditCard.Name = "Control_OperationManagement_Panel_EditCard";
            Control_OperationManagement_Panel_EditCard.Size = new Size(354, 200);
            Control_OperationManagement_Panel_EditCard.TabIndex = 1;
            // 
            // Control_OperationManagement_TableLayout_Edit
            // 
            Control_OperationManagement_TableLayout_Edit.AutoSize = true;
            Control_OperationManagement_TableLayout_Edit.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_Edit.ColumnCount = 2;
            Control_OperationManagement_TableLayout_Edit.ColumnStyles.Add(new ColumnStyle());
            Control_OperationManagement_TableLayout_Edit.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_Edit.Controls.Add(Control_OperationManagement_Label_EditIcon, 0, 0);
            Control_OperationManagement_TableLayout_Edit.Controls.Add(Control_OperationManagement_Label_EditTitle, 1, 0);
            Control_OperationManagement_TableLayout_Edit.Controls.Add(Control_OperationManagement_TableLayout_EditContent, 0, 1);
            Control_OperationManagement_TableLayout_Edit.Dock = DockStyle.Fill;
            Control_OperationManagement_TableLayout_Edit.Location = new Point(0, 0);
            Control_OperationManagement_TableLayout_Edit.Name = "Control_OperationManagement_TableLayout_Edit";
            Control_OperationManagement_TableLayout_Edit.Padding = new Padding(16);
            Control_OperationManagement_TableLayout_Edit.RowCount = 2;
            Control_OperationManagement_TableLayout_Edit.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_Edit.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_Edit.Size = new Size(352, 198);
            Control_OperationManagement_TableLayout_Edit.TabIndex = 0;
            // 
            // Control_OperationManagement_Label_EditIcon
            // 
            Control_OperationManagement_Label_EditIcon.AutoSize = true;
            Control_OperationManagement_Label_EditIcon.Font = new Font("Segoe UI Emoji", 28F);
            Control_OperationManagement_Label_EditIcon.Location = new Point(16, 16);
            Control_OperationManagement_Label_EditIcon.Margin = new Padding(0, 0, 12, 0);
            Control_OperationManagement_Label_EditIcon.Name = "Control_OperationManagement_Label_EditIcon";
            Control_OperationManagement_Label_EditIcon.Size = new Size(74, 51);
            Control_OperationManagement_Label_EditIcon.TabIndex = 0;
            Control_OperationManagement_Label_EditIcon.Text = "✏️";
            // 
            // Control_OperationManagement_Label_EditTitle
            // 
            Control_OperationManagement_Label_EditTitle.AutoSize = true;
            Control_OperationManagement_Label_EditTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            Control_OperationManagement_Label_EditTitle.Location = new Point(105, 16);
            Control_OperationManagement_Label_EditTitle.Name = "Control_OperationManagement_Label_EditTitle";
            Control_OperationManagement_Label_EditTitle.Size = new Size(165, 30);
            Control_OperationManagement_Label_EditTitle.TabIndex = 1;
            Control_OperationManagement_Label_EditTitle.Text = "Edit Operation";
            // 
            // Control_OperationManagement_TableLayout_EditContent
            // 
            Control_OperationManagement_TableLayout_EditContent.AutoSize = true;
            Control_OperationManagement_TableLayout_EditContent.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_EditContent.ColumnCount = 1;
            Control_OperationManagement_TableLayout_Edit.SetColumnSpan(Control_OperationManagement_TableLayout_EditContent, 2);
            Control_OperationManagement_TableLayout_EditContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_EditContent.Controls.Add(Control_OperationManagement_Suggestion_EditSelectOperation, 0, 0);
            Control_OperationManagement_TableLayout_EditContent.Controls.Add(Control_OperationManagement_TextBox_EditNewOperation, 0, 1);
            Control_OperationManagement_TableLayout_EditContent.Controls.Add(Control_OperationManagement_TableLayout_EditActions, 0, 2);
            Control_OperationManagement_TableLayout_EditContent.Dock = DockStyle.Fill;
            Control_OperationManagement_TableLayout_EditContent.Location = new Point(19, 70);
            Control_OperationManagement_TableLayout_EditContent.Name = "Control_OperationManagement_TableLayout_EditContent";
            Control_OperationManagement_TableLayout_EditContent.RowCount = 3;
            Control_OperationManagement_TableLayout_EditContent.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_EditContent.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_EditContent.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_EditContent.Size = new Size(314, 109);
            Control_OperationManagement_TableLayout_EditContent.TabIndex = 2;
            // 
            // Control_OperationManagement_Suggestion_EditSelectOperation
            // 
            Control_OperationManagement_Suggestion_EditSelectOperation.AutoSize = true;
            Control_OperationManagement_Suggestion_EditSelectOperation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_Suggestion_EditSelectOperation.BorderStyle = BorderStyle.FixedSingle;
            Control_OperationManagement_Suggestion_EditSelectOperation.Dock = DockStyle.Fill;
            Control_OperationManagement_Suggestion_EditSelectOperation.LabelText = "Select Operation";
            Control_OperationManagement_Suggestion_EditSelectOperation.Margin = new Padding(3, 3, 3, 10);
            Control_OperationManagement_Suggestion_EditSelectOperation.PlaceholderText = "Search operations (F4)";
            Control_OperationManagement_Suggestion_EditSelectOperation.Name = "Control_OperationManagement_Suggestion_EditSelectOperation";
            Control_OperationManagement_Suggestion_EditSelectOperation.Padding = new Padding(3);
            Control_OperationManagement_Suggestion_EditSelectOperation.Size = new Size(308, 31);
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
            Control_OperationManagement_TextBox_EditNewOperation.Margin = new Padding(3, 3, 3, 10);
            Control_OperationManagement_TextBox_EditNewOperation.PlaceholderText = "Enter new operation code";
            Control_OperationManagement_TextBox_EditNewOperation.Name = "Control_OperationManagement_TextBox_EditNewOperation";
            Control_OperationManagement_TextBox_EditNewOperation.Padding = new Padding(3);
            Control_OperationManagement_TextBox_EditNewOperation.ShowF4Button = false;
            Control_OperationManagement_TextBox_EditNewOperation.Size = new Size(308, 31);
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
            Control_OperationManagement_TableLayout_EditActions.Location = new Point(3, 77);
            Control_OperationManagement_TableLayout_EditActions.Name = "Control_OperationManagement_TableLayout_EditActions";
            Control_OperationManagement_TableLayout_EditActions.RowCount = 1;
            Control_OperationManagement_TableLayout_EditActions.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_EditActions.Size = new Size(308, 29);
            Control_OperationManagement_TableLayout_EditActions.TabIndex = 2;
            // 
            // Control_OperationManagement_Button_EditSave
            // 
            Control_OperationManagement_Button_EditSave.Location = new Point(3, 3);
            Control_OperationManagement_Button_EditSave.Name = "Control_OperationManagement_Button_EditSave";
            Control_OperationManagement_Button_EditSave.Size = new Size(75, 23);
            Control_OperationManagement_Button_EditSave.TabIndex = 0;
            // 
            // Control_OperationManagement_Button_EditReset
            // 
            Control_OperationManagement_Button_EditReset.Location = new Point(230, 3);
            Control_OperationManagement_Button_EditReset.Name = "Control_OperationManagement_Button_EditReset";
            Control_OperationManagement_Button_EditReset.Size = new Size(75, 23);
            Control_OperationManagement_Button_EditReset.TabIndex = 1;
            // 
            // Control_OperationManagement_Panel_RemoveCard
            // 
            Control_OperationManagement_Panel_RemoveCard.AutoSize = true;
            Control_OperationManagement_Panel_RemoveCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_Panel_RemoveCard.Controls.Add(Control_OperationManagement_TableLayout_Remove);
            Control_OperationManagement_Panel_RemoveCard.Dock = DockStyle.Fill;
            Control_OperationManagement_Panel_RemoveCard.Location = new Point(3, 378);
            Control_OperationManagement_Panel_RemoveCard.Name = "Control_OperationManagement_Panel_RemoveCard";
            Control_OperationManagement_Panel_RemoveCard.Size = new Size(354, 241);
            Control_OperationManagement_Panel_RemoveCard.TabIndex = 2;
            // 
            // Control_OperationManagement_TableLayout_Remove
            // 
            Control_OperationManagement_TableLayout_Remove.AutoSize = true;
            Control_OperationManagement_TableLayout_Remove.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_Remove.ColumnCount = 2;
            Control_OperationManagement_TableLayout_Remove.ColumnStyles.Add(new ColumnStyle());
            Control_OperationManagement_TableLayout_Remove.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_Remove.Controls.Add(Control_OperationManagement_Label_RemoveIcon, 0, 0);
            Control_OperationManagement_TableLayout_Remove.Controls.Add(Control_OperationManagement_Label_RemoveTitle, 1, 0);
            Control_OperationManagement_TableLayout_Remove.Controls.Add(Control_OperationManagement_TableLayout_RemoveContent, 0, 1);
            Control_OperationManagement_TableLayout_Remove.Dock = DockStyle.Fill;
            Control_OperationManagement_TableLayout_Remove.Location = new Point(0, 0);
            Control_OperationManagement_TableLayout_Remove.Name = "Control_OperationManagement_TableLayout_Remove";
            Control_OperationManagement_TableLayout_Remove.Padding = new Padding(16);
            Control_OperationManagement_TableLayout_Remove.RowCount = 2;
            Control_OperationManagement_TableLayout_Remove.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_Remove.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_Remove.Size = new Size(354, 241);
            Control_OperationManagement_TableLayout_Remove.TabIndex = 0;
            // 
            // Control_OperationManagement_Label_RemoveIcon
            // 
            Control_OperationManagement_Label_RemoveIcon.AutoSize = true;
            Control_OperationManagement_Label_RemoveIcon.Font = new Font("Segoe UI Emoji", 28F);
            Control_OperationManagement_Label_RemoveIcon.Location = new Point(16, 16);
            Control_OperationManagement_Label_RemoveIcon.Margin = new Padding(0, 0, 12, 0);
            Control_OperationManagement_Label_RemoveIcon.Name = "Control_OperationManagement_Label_RemoveIcon";
            Control_OperationManagement_Label_RemoveIcon.Size = new Size(74, 51);
            Control_OperationManagement_Label_RemoveIcon.TabIndex = 0;
            Control_OperationManagement_Label_RemoveIcon.Text = "🗑️";
            // 
            // Control_OperationManagement_Label_RemoveTitle
            // 
            Control_OperationManagement_Label_RemoveTitle.AutoSize = true;
            Control_OperationManagement_Label_RemoveTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            Control_OperationManagement_Label_RemoveTitle.Location = new Point(105, 16);
            Control_OperationManagement_Label_RemoveTitle.Name = "Control_OperationManagement_Label_RemoveTitle";
            Control_OperationManagement_Label_RemoveTitle.Size = new Size(206, 30);
            Control_OperationManagement_Label_RemoveTitle.TabIndex = 1;
            Control_OperationManagement_Label_RemoveTitle.Text = "Remove Operation";
            // 
            // Control_OperationManagement_TableLayout_RemoveContent
            // 
            Control_OperationManagement_TableLayout_RemoveContent.AutoSize = true;
            Control_OperationManagement_TableLayout_RemoveContent.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_RemoveContent.ColumnCount = 1;
            Control_OperationManagement_TableLayout_Remove.SetColumnSpan(Control_OperationManagement_TableLayout_RemoveContent, 2);
            Control_OperationManagement_TableLayout_RemoveContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_RemoveContent.Controls.Add(Control_OperationManagement_Suggestion_RemoveSelectOperation, 0, 0);
            Control_OperationManagement_TableLayout_RemoveContent.Controls.Add(Control_OperationManagement_TableLayout_RemoveDetails, 0, 1);
            Control_OperationManagement_TableLayout_RemoveContent.Controls.Add(Control_OperationManagement_Label_RemoveWarning, 0, 3);
            Control_OperationManagement_TableLayout_RemoveContent.Controls.Add(Control_OperationManagement_TableLayout_RemoveActions, 4, 3);
            Control_OperationManagement_TableLayout_RemoveContent.Dock = DockStyle.Top;
            Control_OperationManagement_TableLayout_RemoveContent.Location = new Point(19, 70);
            Control_OperationManagement_TableLayout_RemoveContent.Name = "Control_OperationManagement_TableLayout_RemoveContent";
            Control_OperationManagement_TableLayout_RemoveContent.RowCount = 5;
            Control_OperationManagement_TableLayout_RemoveContent.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_RemoveContent.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_RemoveContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_RemoveContent.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_RemoveContent.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_RemoveContent.Size = new Size(316, 144);
            Control_OperationManagement_TableLayout_RemoveContent.TabIndex = 2;
            // 
            // Control_OperationManagement_Suggestion_RemoveSelectOperation
            // 
            Control_OperationManagement_Suggestion_RemoveSelectOperation.AutoSize = true;
            Control_OperationManagement_Suggestion_RemoveSelectOperation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_Suggestion_RemoveSelectOperation.BorderStyle = BorderStyle.FixedSingle;
            Control_OperationManagement_Suggestion_RemoveSelectOperation.Dock = DockStyle.Fill;
            Control_OperationManagement_Suggestion_RemoveSelectOperation.LabelText = "Select Operation";
            Control_OperationManagement_Suggestion_RemoveSelectOperation.Margin = new Padding(3, 3, 3, 10);
            Control_OperationManagement_Suggestion_RemoveSelectOperation.PlaceholderText = "Search operations (F4)";
            Control_OperationManagement_Suggestion_RemoveSelectOperation.Name = "Control_OperationManagement_Suggestion_RemoveSelectOperation";
            Control_OperationManagement_Suggestion_RemoveSelectOperation.Padding = new Padding(3);
            Control_OperationManagement_Suggestion_RemoveSelectOperation.Size = new Size(310, 31);
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
            Control_OperationManagement_TableLayout_RemoveDetails.Location = new Point(3, 40);
            Control_OperationManagement_TableLayout_RemoveDetails.Name = "Control_OperationManagement_TableLayout_RemoveDetails";
            Control_OperationManagement_TableLayout_RemoveDetails.RowCount = 2;
            Control_OperationManagement_TableLayout_RemoveDetails.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_RemoveDetails.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_RemoveDetails.Size = new Size(310, 45);
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
            Control_OperationManagement_Label_RemoveOperationValue.Size = new Size(232, 15);
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
            Control_OperationManagement_Label_RemoveIssuedByValue.Size = new Size(232, 15);
            Control_OperationManagement_Label_RemoveIssuedByValue.TabIndex = 3;
            Control_OperationManagement_Label_RemoveIssuedByValue.Text = "{Value}";
            // 
            // Control_OperationManagement_Label_RemoveWarning
            // 
            Control_OperationManagement_Label_RemoveWarning.AutoSize = true;
            Control_OperationManagement_Label_RemoveWarning.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_RemoveWarning.ForeColor = Color.FromArgb(192, 0, 0);
            Control_OperationManagement_Label_RemoveWarning.Location = new Point(3, 91);
            Control_OperationManagement_Label_RemoveWarning.Margin = new Padding(3);
            Control_OperationManagement_Label_RemoveWarning.Name = "Control_OperationManagement_Label_RemoveWarning";
            Control_OperationManagement_Label_RemoveWarning.Size = new Size(310, 15);
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
            Control_OperationManagement_TableLayout_RemoveActions.Controls.Add(Control_OperationManagement_Button_RemoveCancel, 2, 0);
            Control_OperationManagement_TableLayout_RemoveActions.Dock = DockStyle.Fill;
            Control_OperationManagement_TableLayout_RemoveActions.Location = new Point(3, 112);
            Control_OperationManagement_TableLayout_RemoveActions.Name = "Control_OperationManagement_TableLayout_RemoveActions";
            Control_OperationManagement_TableLayout_RemoveActions.RowCount = 1;
            Control_OperationManagement_TableLayout_RemoveActions.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_RemoveActions.Size = new Size(310, 29);
            Control_OperationManagement_TableLayout_RemoveActions.TabIndex = 3;
            // 
            // Control_OperationManagement_Button_RemoveConfirm
            // 
            Control_OperationManagement_Button_RemoveConfirm.Location = new Point(3, 3);
            Control_OperationManagement_Button_RemoveConfirm.Name = "Control_OperationManagement_Button_RemoveConfirm";
            Control_OperationManagement_Button_RemoveConfirm.Size = new Size(75, 23);
            Control_OperationManagement_Button_RemoveConfirm.TabIndex = 0;
            // 
            // Control_OperationManagement_Button_RemoveCancel
            // 
            Control_OperationManagement_Button_RemoveCancel.Location = new Point(232, 3);
            Control_OperationManagement_Button_RemoveCancel.Name = "Control_OperationManagement_Button_RemoveCancel";
            Control_OperationManagement_Button_RemoveCancel.Size = new Size(75, 23);
            Control_OperationManagement_Button_RemoveCancel.TabIndex = 1;
            // 
            // Control_OperationManagement_Panel_Home
            // 
            Control_OperationManagement_Panel_Home.AutoSize = true;
            Control_OperationManagement_Panel_Home.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_Panel_Home.Controls.Add(Control_OperationManagement_TableLayout_Home);
            Control_OperationManagement_Panel_Home.Dock = DockStyle.Fill;
            Control_OperationManagement_Panel_Home.Location = new Point(0, 0);
            Control_OperationManagement_Panel_Home.Name = "Control_OperationManagement_Panel_Home";
            Control_OperationManagement_Panel_Home.Size = new Size(360, 622);
            Control_OperationManagement_Panel_Home.TabIndex = 0;
            // 
            // Control_OperationManagement_TableLayout_Home
            // 
            Control_OperationManagement_TableLayout_Home.AutoSize = true;
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
            Control_OperationManagement_TableLayout_Home.Size = new Size(360, 622);
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
            Control_OperationManagement_Panel_HomeTile_Add.Size = new Size(113, 616);
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
            Control_OperationManagement_TableLayout_HomeTile_Add.Controls.Add(Control_OperationManagement_Label_HomeTile_AddInstruction, 0, 2);
            Control_OperationManagement_TableLayout_HomeTile_Add.Dock = DockStyle.Fill;
            Control_OperationManagement_TableLayout_HomeTile_Add.Padding = new Padding(20);
            Control_OperationManagement_TableLayout_HomeTile_Add.RowCount = 3;
            Control_OperationManagement_TableLayout_HomeTile_Add.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_HomeTile_Add.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_HomeTile_Add.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_HomeTile_Add.TabIndex = 0;
            // 
            // Control_OperationManagement_Label_HomeTile_AddIcon
            // 
            Control_OperationManagement_Label_HomeTile_AddIcon.AutoSize = true;
            Control_OperationManagement_Label_HomeTile_AddIcon.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_HomeTile_AddIcon.Font = new Font("Segoe UI Emoji", 48F, FontStyle.Regular, GraphicsUnit.Point);
            Control_OperationManagement_Label_HomeTile_AddIcon.Text = "🆕";
            Control_OperationManagement_Label_HomeTile_AddIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_OperationManagement_Label_HomeTile_AddTitle
            // 
            Control_OperationManagement_Label_HomeTile_AddTitle.AutoSize = true;
            Control_OperationManagement_Label_HomeTile_AddTitle.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_HomeTile_AddTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            Control_OperationManagement_Label_HomeTile_AddTitle.Margin = new Padding(0, 10, 0, 5);
            Control_OperationManagement_Label_HomeTile_AddTitle.Text = "Add Operation";
            Control_OperationManagement_Label_HomeTile_AddTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_OperationManagement_Label_HomeTile_AddInstruction
            // 
            Control_OperationManagement_Label_HomeTile_AddInstruction.AutoSize = true;
            Control_OperationManagement_Label_HomeTile_AddInstruction.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_HomeTile_AddInstruction.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point);
            Control_OperationManagement_Label_HomeTile_AddInstruction.ForeColor = Color.FromArgb(100, 100, 100);
            Control_OperationManagement_Label_HomeTile_AddInstruction.Text = "Click to add new operation codes";
            Control_OperationManagement_Label_HomeTile_AddInstruction.TextAlign = ContentAlignment.TopCenter;
            // 
            // Control_OperationManagement_Panel_HomeTile_Edit
            // 
            Control_OperationManagement_Panel_HomeTile_Edit.AutoSize = true;
            Control_OperationManagement_Panel_HomeTile_Edit.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_Panel_HomeTile_Edit.BorderStyle = BorderStyle.FixedSingle;
            Control_OperationManagement_Panel_HomeTile_Edit.Controls.Add(Control_OperationManagement_TableLayout_HomeTile_Edit);
            Control_OperationManagement_Panel_HomeTile_Edit.Cursor = Cursors.Hand;
            Control_OperationManagement_Panel_HomeTile_Edit.Dock = DockStyle.Fill;
            Control_OperationManagement_Panel_HomeTile_Edit.Location = new Point(122, 3);
            Control_OperationManagement_Panel_HomeTile_Edit.Name = "Control_OperationManagement_Panel_HomeTile_Edit";
            Control_OperationManagement_Panel_HomeTile_Edit.Size = new Size(113, 616);
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
            Control_OperationManagement_TableLayout_HomeTile_Edit.Controls.Add(Control_OperationManagement_Label_HomeTile_EditInstruction, 0, 2);
            Control_OperationManagement_TableLayout_HomeTile_Edit.Dock = DockStyle.Fill;
            Control_OperationManagement_TableLayout_HomeTile_Edit.Padding = new Padding(20);
            Control_OperationManagement_TableLayout_HomeTile_Edit.RowCount = 3;
            Control_OperationManagement_TableLayout_HomeTile_Edit.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_HomeTile_Edit.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_HomeTile_Edit.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_HomeTile_Edit.TabIndex = 0;
            // 
            // Control_OperationManagement_Label_HomeTile_EditIcon
            // 
            Control_OperationManagement_Label_HomeTile_EditIcon.AutoSize = true;
            Control_OperationManagement_Label_HomeTile_EditIcon.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_HomeTile_EditIcon.Font = new Font("Segoe UI Emoji", 48F, FontStyle.Regular, GraphicsUnit.Point);
            Control_OperationManagement_Label_HomeTile_EditIcon.Text = "✏️";
            Control_OperationManagement_Label_HomeTile_EditIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_OperationManagement_Label_HomeTile_EditTitle
            // 
            Control_OperationManagement_Label_HomeTile_EditTitle.AutoSize = true;
            Control_OperationManagement_Label_HomeTile_EditTitle.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_HomeTile_EditTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            Control_OperationManagement_Label_HomeTile_EditTitle.Margin = new Padding(0, 10, 0, 5);
            Control_OperationManagement_Label_HomeTile_EditTitle.Text = "Edit Operation";
            Control_OperationManagement_Label_HomeTile_EditTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_OperationManagement_Label_HomeTile_EditInstruction
            // 
            Control_OperationManagement_Label_HomeTile_EditInstruction.AutoSize = true;
            Control_OperationManagement_Label_HomeTile_EditInstruction.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_HomeTile_EditInstruction.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point);
            Control_OperationManagement_Label_HomeTile_EditInstruction.ForeColor = Color.FromArgb(100, 100, 100);
            Control_OperationManagement_Label_HomeTile_EditInstruction.Text = "Click to edit existing operations";
            Control_OperationManagement_Label_HomeTile_EditInstruction.TextAlign = ContentAlignment.TopCenter;
            // 
            // Control_OperationManagement_Panel_HomeTile_Remove
            // 
            Control_OperationManagement_Panel_HomeTile_Remove.AutoSize = true;
            Control_OperationManagement_Panel_HomeTile_Remove.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_Panel_HomeTile_Remove.BorderStyle = BorderStyle.FixedSingle;
            Control_OperationManagement_Panel_HomeTile_Remove.Controls.Add(Control_OperationManagement_TableLayout_HomeTile_Remove);
            Control_OperationManagement_Panel_HomeTile_Remove.Cursor = Cursors.Hand;
            Control_OperationManagement_Panel_HomeTile_Remove.Dock = DockStyle.Fill;
            Control_OperationManagement_Panel_HomeTile_Remove.Location = new Point(241, 3);
            Control_OperationManagement_Panel_HomeTile_Remove.Name = "Control_OperationManagement_Panel_HomeTile_Remove";
            Control_OperationManagement_Panel_HomeTile_Remove.Size = new Size(116, 616);
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
            Control_OperationManagement_TableLayout_HomeTile_Remove.Controls.Add(Control_OperationManagement_Label_HomeTile_RemoveInstruction, 0, 2);
            Control_OperationManagement_TableLayout_HomeTile_Remove.Dock = DockStyle.Fill;
            Control_OperationManagement_TableLayout_HomeTile_Remove.Padding = new Padding(20);
            Control_OperationManagement_TableLayout_HomeTile_Remove.RowCount = 3;
            Control_OperationManagement_TableLayout_HomeTile_Remove.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_HomeTile_Remove.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_HomeTile_Remove.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_HomeTile_Remove.TabIndex = 0;
            // 
            // Control_OperationManagement_Label_HomeTile_RemoveIcon
            // 
            Control_OperationManagement_Label_HomeTile_RemoveIcon.AutoSize = true;
            Control_OperationManagement_Label_HomeTile_RemoveIcon.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_HomeTile_RemoveIcon.Font = new Font("Segoe UI Emoji", 48F, FontStyle.Regular, GraphicsUnit.Point);
            Control_OperationManagement_Label_HomeTile_RemoveIcon.Text = "🗑️";
            Control_OperationManagement_Label_HomeTile_RemoveIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_OperationManagement_Label_HomeTile_RemoveTitle
            // 
            Control_OperationManagement_Label_HomeTile_RemoveTitle.AutoSize = true;
            Control_OperationManagement_Label_HomeTile_RemoveTitle.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_HomeTile_RemoveTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            Control_OperationManagement_Label_HomeTile_RemoveTitle.Margin = new Padding(0, 10, 0, 5);
            Control_OperationManagement_Label_HomeTile_RemoveTitle.Text = "Remove Operation";
            Control_OperationManagement_Label_HomeTile_RemoveTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_OperationManagement_Label_HomeTile_RemoveInstruction
            // 
            Control_OperationManagement_Label_HomeTile_RemoveInstruction.AutoSize = true;
            Control_OperationManagement_Label_HomeTile_RemoveInstruction.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_HomeTile_RemoveInstruction.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point);
            Control_OperationManagement_Label_HomeTile_RemoveInstruction.ForeColor = Color.FromArgb(100, 100, 100);
            Control_OperationManagement_Label_HomeTile_RemoveInstruction.Text = "Click to remove operation codes";
            Control_OperationManagement_Label_HomeTile_RemoveInstruction.TextAlign = ContentAlignment.TopCenter;
            // 
            // Control_OperationManagement_TableLayout_BackButton
            // 
            Control_OperationManagement_TableLayout_BackButton.AutoSize = true;
            Control_OperationManagement_TableLayout_BackButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_BackButton.ColumnCount = 2;
            Control_OperationManagement_TableLayout_BackButton.ColumnStyles.Add(new ColumnStyle());
            Control_OperationManagement_TableLayout_BackButton.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_BackButton.Controls.Add(Control_OperationManagement_Button_Back, 0, 0);
            Control_OperationManagement_TableLayout_BackButton.Dock = DockStyle.Fill;
            Control_OperationManagement_TableLayout_BackButton.Location = new Point(23, 719);
            Control_OperationManagement_TableLayout_BackButton.Name = "Control_OperationManagement_TableLayout_BackButton";
            Control_OperationManagement_TableLayout_BackButton.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_BackButton.Size = new Size(360, 43);
            Control_OperationManagement_TableLayout_BackButton.TabIndex = 2;
            Control_OperationManagement_TableLayout_BackButton.Visible = false;
            // 
            // Control_OperationManagement_Button_Back
            // 
            Control_OperationManagement_Button_Back.AutoSize = true;
            Control_OperationManagement_Button_Back.Location = new Point(3, 3);
            Control_OperationManagement_Button_Back.Name = "Control_OperationManagement_Button_Back";
            Control_OperationManagement_Button_Back.Padding = new Padding(16, 6, 16, 6);
            Control_OperationManagement_Button_Back.Size = new Size(152, 37);
            Control_OperationManagement_Button_Back.TabIndex = 0;
            Control_OperationManagement_Button_Back.Text = "← Back to Selection";
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
            Size = new Size(406, 785);
            Control_OperationManagement_TableLayout_Main.ResumeLayout(false);
            Control_OperationManagement_TableLayout_Main.PerformLayout();
            Control_OperationManagement_Panel_Container.ResumeLayout(false);
            Control_OperationManagement_Panel_Container.PerformLayout();
            Control_OperationManagement_TableLayout_Cards.ResumeLayout(false);
            Control_OperationManagement_TableLayout_Cards.PerformLayout();
            Control_OperationManagement_Panel_AddCard.ResumeLayout(false);
            Control_OperationManagement_Panel_AddCard.PerformLayout();
            Control_OperationManagement_TableLayout_Add.ResumeLayout(false);
            Control_OperationManagement_TableLayout_Add.PerformLayout();
            Control_OperationManagement_TableLayout_AddContent.ResumeLayout(false);
            Control_OperationManagement_TableLayout_AddContent.PerformLayout();
            Control_OperationManagement_TableLayout_AddActions.ResumeLayout(false);
            Control_OperationManagement_Panel_EditCard.ResumeLayout(false);
            Control_OperationManagement_Panel_EditCard.PerformLayout();
            Control_OperationManagement_TableLayout_Edit.ResumeLayout(false);
            Control_OperationManagement_TableLayout_Edit.PerformLayout();
            Control_OperationManagement_TableLayout_EditContent.ResumeLayout(false);
            Control_OperationManagement_TableLayout_EditContent.PerformLayout();
            Control_OperationManagement_TableLayout_EditActions.ResumeLayout(false);
            Control_OperationManagement_Panel_RemoveCard.ResumeLayout(false);
            Control_OperationManagement_Panel_RemoveCard.PerformLayout();
            Control_OperationManagement_TableLayout_Remove.ResumeLayout(false);
            Control_OperationManagement_TableLayout_Remove.PerformLayout();
            Control_OperationManagement_TableLayout_RemoveContent.ResumeLayout(false);
            Control_OperationManagement_TableLayout_RemoveContent.PerformLayout();
            Control_OperationManagement_TableLayout_RemoveDetails.ResumeLayout(false);
            Control_OperationManagement_TableLayout_RemoveDetails.PerformLayout();
            Control_OperationManagement_TableLayout_RemoveActions.ResumeLayout(false);
            Control_OperationManagement_Panel_Home.ResumeLayout(false);
            Control_OperationManagement_Panel_Home.PerformLayout();
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
            Control_OperationManagement_TableLayout_BackButton.ResumeLayout(false);
            Control_OperationManagement_TableLayout_BackButton.PerformLayout();
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
                Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 10, 0, 5)
            };
            
            Label descLabel = new Label
            {
                Text = description,
                Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point),
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
    }
}
