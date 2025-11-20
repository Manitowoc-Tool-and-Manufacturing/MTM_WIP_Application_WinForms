using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Controls.Shared;

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    partial class Control_PartIDManagement
    {
        #region Fields

        private IContainer components = null!;
        private TableLayoutPanel Control_PartIDManagement_TableLayout_Main = null!;
        private Label Control_PartIDManagement_Label_Header = null!;
        private Label Control_PartIDManagement_Label_Subtitle = null!;
        private Panel Control_PartIDManagement_Panel_Container = null!;
        
        private Panel Control_PartIDManagement_Panel_Home = null!;
        private TableLayoutPanel Control_PartIDManagement_TableLayout_Home = null!;
        private Panel Control_PartIDManagement_Panel_HomeTile_Add = null!;
        private TableLayoutPanel Control_PartIDManagement_TableLayout_HomeTile_Add = null!;
        private Label Control_PartIDManagement_Label_HomeTile_AddIcon = null!;
        private Label Control_PartIDManagement_Label_HomeTile_AddTitle = null!;
        private Label Control_PartIDManagement_Label_HomeTile_AddInstruction = null!;
        private Panel Control_PartIDManagement_Panel_HomeTile_Edit = null!;
        private TableLayoutPanel Control_PartIDManagement_TableLayout_HomeTile_Edit = null!;
        private Label Control_PartIDManagement_Label_HomeTile_EditIcon = null!;
        private Label Control_PartIDManagement_Label_HomeTile_EditTitle = null!;
        private Label Control_PartIDManagement_Label_HomeTile_EditInstruction = null!;
        private Panel Control_PartIDManagement_Panel_HomeTile_Remove = null!;
        private TableLayoutPanel Control_PartIDManagement_TableLayout_HomeTile_Remove = null!;
        private Label Control_PartIDManagement_Label_HomeTile_RemoveIcon = null!;
        private Label Control_PartIDManagement_Label_HomeTile_RemoveTitle = null!;
        private Label Control_PartIDManagement_Label_HomeTile_RemoveInstruction = null!;
        
        private TableLayoutPanel Control_PartIDManagement_TableLayout_Cards = null!;

        private Panel Control_PartIDManagement_Panel_AddCard = null!;
        private TableLayoutPanel Control_PartIDManagement_TableLayout_Add = null!;
        private Label Control_PartIDManagement_Label_AddIcon = null!;
        private Label Control_PartIDManagement_Label_AddTitle = null!;
        private SuggestionTextBoxWithLabel Control_PartIDManagement_Suggestion_AddPartNumber = null!;
        private SuggestionTextBoxWithLabel Control_PartIDManagement_Suggestion_AddItemType = null!;
        private Label Control_PartIDManagement_Label_AddIssuedBy = null!;
        private Label Control_PartIDManagement_Label_AddIssuedByValue = null!;
        private CheckBox Control_PartIDManagement_CheckBox_AddRequiresColorCode = null!;
        private Button Control_PartIDManagement_Button_AddSave = null!;
        private Button Control_PartIDManagement_Button_AddClear = null!;

        private Panel Control_PartIDManagement_Panel_EditCard = null!;
        private TableLayoutPanel Control_PartIDManagement_TableLayout_Edit = null!;
        private Label Control_PartIDManagement_Label_EditIcon = null!;
        private Label Control_PartIDManagement_Label_EditTitle = null!;
        private TableLayoutPanel Control_PartIDManagement_TableLayout_EditContent = null!;
        private SuggestionTextBoxWithLabel Control_PartIDManagement_Suggestion_EditSelectPart = null!;
        private SuggestionTextBoxWithLabel Control_PartIDManagement_Suggestion_EditNewPartNumber = null!;
        private SuggestionTextBoxWithLabel Control_PartIDManagement_Suggestion_EditItemType = null!;
        private Label Control_PartIDManagement_Label_EditIssuedBy = null!;
        private Label Control_PartIDManagement_Label_EditIssuedByValue = null!;
        private CheckBox Control_PartIDManagement_CheckBox_EditRequiresColorCode = null!;
        private Button Control_PartIDManagement_Button_EditSave = null!;
        private Button Control_PartIDManagement_Button_EditReset = null!;

        private Panel Control_PartIDManagement_Panel_RemoveCard = null!;
        private TableLayoutPanel Control_PartIDManagement_TableLayout_Remove = null!;
        private Label Control_PartIDManagement_Label_RemoveIcon = null!;
        private Label Control_PartIDManagement_Label_RemoveTitle = null!;
        private TableLayoutPanel Control_PartIDManagement_TableLayout_RemoveContent = null!;
        private SuggestionTextBoxWithLabel Control_PartIDManagement_Suggestion_RemoveSelectPart = null!;
        private TableLayoutPanel Control_PartIDManagement_TableLayout_RemoveDetails = null!;
        private Label Control_PartIDManagement_Label_RemoveItemNumber = null!;
        private Label Control_PartIDManagement_Label_RemoveItemNumberValue = null!;
        private Label Control_PartIDManagement_Label_RemoveCustomer = null!;
        private Label Control_PartIDManagement_Label_RemoveCustomerValue = null!;
        private Label Control_PartIDManagement_Label_RemoveDescription = null!;
        private Label Control_PartIDManagement_Label_RemoveDescriptionValue = null!;
        private Label Control_PartIDManagement_Label_RemoveType = null!;
        private Label Control_PartIDManagement_Label_RemoveTypeValue = null!;
        private Label Control_PartIDManagement_Label_RemoveIssuedBy = null!;
        private Label Control_PartIDManagement_Label_RemoveIssuedByValue = null!;
        private Label Control_PartIDManagement_Label_RemoveWarning = null!;
        private Button Control_PartIDManagement_Button_RemoveConfirm = null!;
        private Button Control_PartIDManagement_Button_RemoveCancel = null!;

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
            Control_PartIDManagement_TableLayout_Main = new TableLayoutPanel();
            Control_PartIDManagement_Panel_Container = new Panel();
            Control_PartIDManagement_TableLayout_Cards = new TableLayoutPanel();
            Control_PartIDManagement_Panel_RemoveCard = new Panel();
            Control_PartIDManagement_TableLayout_Remove = new TableLayoutPanel();
            Control_PartIDManagement_TableLayout_RemoveContent = new TableLayoutPanel();
            Control_PartIDManagement_TableLayout_RemoveActions = new TableLayoutPanel();
            Control_PartIDManagement_Button_RemoveCancel = new Button();
            Control_PartIDManagement_Button_RemoveConfirm = new Button();
            Control_PartIDManagement_Suggestion_RemoveSelectPart = new SuggestionTextBoxWithLabel();
            Control_PartIDManagement_TableLayout_RemoveDetails = new TableLayoutPanel();
            Control_PartIDManagement_Label_RemoveItemNumber = new Label();
            Control_PartIDManagement_Label_RemoveItemNumberValue = new Label();
            Control_PartIDManagement_Label_RemoveDescriptionValue = new Label();
            Control_PartIDManagement_Label_RemoveCustomerValue = new Label();
            Control_PartIDManagement_Label_RemoveCustomer = new Label();
            Control_PartIDManagement_Label_RemoveDescription = new Label();
            Control_PartIDManagement_Label_RemoveType = new Label();
            Control_PartIDManagement_Label_RemoveIssuedBy = new Label();
            Control_PartIDManagement_Label_RemoveTypeValue = new Label();
            Control_PartIDManagement_Label_RemoveIssuedByValue = new Label();
            Control_PartIDManagement_Label_RemoveWarning = new Label();
            Control_PartIDManagement_TableLayout_Title = new TableLayoutPanel();
            Control_PartIDManagement_Label_RemoveIcon = new Label();
            Control_PartIDManagement_Label_RemoveTitle = new Label();
            Control_PartIDManagement_Panel_AddCard = new Panel();
            Control_PartIDManagement_TableLayout_Add = new TableLayoutPanel();
            Control_PartIDManagement_TableLayout_AddContent = new TableLayoutPanel();
            Control_PartIDManagement_Suggestion_AddPartNumber = new SuggestionTextBoxWithLabel();
            Control_PartIDManagement_Suggestion_AddItemType = new SuggestionTextBoxWithLabel();
            Control_PartIDManagement_CheckBox_AddRequiresColorCode = new CheckBox();
            Control_PartIDManagement_TableLayout_AddActions = new TableLayoutPanel();
            Control_PartIDManagement_Button_AddSave = new Button();
            Control_PartIDManagement_Button_AddClear = new Button();
            Control_PartIDManagement_Add_TableLayout_Title = new TableLayoutPanel();
            Control_PartIDManagement_Label_AddIcon = new Label();
            Control_PartIDManagement_Label_AddTitle = new Label();
            Control_PartIDManagement_Panel_EditCard = new Panel();
            Control_PartIDManagement_TableLayout_Edit = new TableLayoutPanel();
            Control_PartIDManagement_TableLayoutPanel_Title = new TableLayoutPanel();
            Control_PartIDManagement_Label_EditIcon = new Label();
            Control_PartIDManagement_Label_EditTitle = new Label();
            Control_PartIDManagement_TableLayout_EditContent = new TableLayoutPanel();
            Control_PartIDManagement_TableLayoutPanel_EditActions = new TableLayoutPanel();
            Control_PartIDManagement_Button_EditSave = new Button();
            Control_PartIDManagement_Button_EditReset = new Button();
            Control_PartIDManagement_Suggestion_EditSelectPart = new SuggestionTextBoxWithLabel();
            Control_PartIDManagement_Suggestion_EditItemType = new SuggestionTextBoxWithLabel();
            Control_PartIDManagement_CheckBox_EditRequiresColorCode = new CheckBox();
            Control_PartIDManagement_Suggestion_EditNewPartNumber = new SuggestionTextBoxWithLabel();
            Control_PartIDManagement_Panel_Home = new Panel();
            Control_PartIDManagement_TableLayout_Home = new TableLayoutPanel();
            Control_PartIDManagement_Panel_HomeTile_Add = new Panel();
            Control_PartIDManagement_TableLayout_HomeTile_Add = new TableLayoutPanel();
            Control_PartIDManagement_Label_HomeTile_AddInstruction = new Label();
            Control_PartIDManagement_Label_HomeTile_AddTitle = new Label();
            Control_PartIDManagement_Label_HomeTile_AddIcon = new Label();
            Control_PartIDManagement_Panel_HomeTile_Edit = new Panel();
            Control_PartIDManagement_TableLayout_HomeTile_Edit = new TableLayoutPanel();
            Control_PartIDManagement_Label_HomeTile_EditIcon = new Label();
            Control_PartIDManagement_Label_HomeTile_EditTitle = new Label();
            Control_PartIDManagement_Label_HomeTile_EditInstruction = new Label();
            Control_PartIDManagement_Panel_HomeTile_Remove = new Panel();
            Control_PartIDManagement_TableLayout_HomeTile_Remove = new TableLayoutPanel();
            Control_PartIDManagement_Label_HomeTile_RemoveIcon = new Label();
            Control_PartIDManagement_Label_HomeTile_RemoveInstruction = new Label();
            Control_PartIDManagement_Label_HomeTile_RemoveTitle = new Label();
            Control_PartIDManagement_TableLayoutPanel_BackButton = new TableLayoutPanel();
            Control_PartIDManagement_Button_Back = new Button();
            Control_PartIDManagement_Label_Header = new Label();
            Control_PartIDManagement_Label_Subtitle = new Label();
            Control_PartIDManagement_Label_AddIssuedBy = new Label();
            Control_PartIDManagement_Label_AddIssuedByValue = new Label();
            Control_PartIDManagement_Label_EditIssuedBy = new Label();
            Control_PartIDManagement_Label_EditIssuedByValue = new Label();
            Control_PartIDManagement_TableLayout_Main.SuspendLayout();
            Control_PartIDManagement_Panel_Container.SuspendLayout();
            Control_PartIDManagement_TableLayout_Cards.SuspendLayout();
            Control_PartIDManagement_Panel_RemoveCard.SuspendLayout();
            Control_PartIDManagement_TableLayout_Remove.SuspendLayout();
            Control_PartIDManagement_TableLayout_RemoveContent.SuspendLayout();
            Control_PartIDManagement_TableLayout_RemoveActions.SuspendLayout();
            Control_PartIDManagement_TableLayout_RemoveDetails.SuspendLayout();
            Control_PartIDManagement_TableLayout_Title.SuspendLayout();
            Control_PartIDManagement_Panel_AddCard.SuspendLayout();
            Control_PartIDManagement_TableLayout_Add.SuspendLayout();
            Control_PartIDManagement_TableLayout_AddContent.SuspendLayout();
            Control_PartIDManagement_TableLayout_AddActions.SuspendLayout();
            Control_PartIDManagement_Add_TableLayout_Title.SuspendLayout();
            Control_PartIDManagement_Panel_EditCard.SuspendLayout();
            Control_PartIDManagement_TableLayout_Edit.SuspendLayout();
            Control_PartIDManagement_TableLayoutPanel_Title.SuspendLayout();
            Control_PartIDManagement_TableLayout_EditContent.SuspendLayout();
            Control_PartIDManagement_TableLayoutPanel_EditActions.SuspendLayout();
            Control_PartIDManagement_Panel_Home.SuspendLayout();
            Control_PartIDManagement_TableLayout_Home.SuspendLayout();
            Control_PartIDManagement_Panel_HomeTile_Add.SuspendLayout();
            Control_PartIDManagement_TableLayout_HomeTile_Add.SuspendLayout();
            Control_PartIDManagement_Panel_HomeTile_Edit.SuspendLayout();
            Control_PartIDManagement_TableLayout_HomeTile_Edit.SuspendLayout();
            Control_PartIDManagement_Panel_HomeTile_Remove.SuspendLayout();
            Control_PartIDManagement_TableLayout_HomeTile_Remove.SuspendLayout();
            Control_PartIDManagement_TableLayoutPanel_BackButton.SuspendLayout();
            SuspendLayout();
            // 
            // Control_PartIDManagement_TableLayout_Main
            // 
            Control_PartIDManagement_TableLayout_Main.AutoSize = true;
            Control_PartIDManagement_TableLayout_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_TableLayout_Main.ColumnCount = 1;
            Control_PartIDManagement_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayout_Main.Controls.Add(Control_PartIDManagement_Panel_Container, 0, 2);
            Control_PartIDManagement_TableLayout_Main.Controls.Add(Control_PartIDManagement_TableLayoutPanel_BackButton, 0, 3);
            Control_PartIDManagement_TableLayout_Main.Controls.Add(Control_PartIDManagement_Label_Header, 0, 0);
            Control_PartIDManagement_TableLayout_Main.Controls.Add(Control_PartIDManagement_Label_Subtitle, 0, 1);
            Control_PartIDManagement_TableLayout_Main.Dock = DockStyle.Fill;
            Control_PartIDManagement_TableLayout_Main.Location = new Point(0, 0);
            Control_PartIDManagement_TableLayout_Main.Name = "Control_PartIDManagement_TableLayout_Main";
            Control_PartIDManagement_TableLayout_Main.Padding = new Padding(20);
            Control_PartIDManagement_TableLayout_Main.RowCount = 4;
            Control_PartIDManagement_TableLayout_Main.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_Main.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayout_Main.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_Main.Size = new Size(492, 389);
            Control_PartIDManagement_TableLayout_Main.TabIndex = 0;
            // 
            // Control_PartIDManagement_Panel_Container
            // 
            Control_PartIDManagement_Panel_Container.Controls.Add(Control_PartIDManagement_Panel_Home);
            Control_PartIDManagement_Panel_Container.Controls.Add(Control_PartIDManagement_TableLayout_Cards);
            Control_PartIDManagement_Panel_Container.Dock = DockStyle.Fill;
            Control_PartIDManagement_Panel_Container.Location = new Point(23, 91);
            Control_PartIDManagement_Panel_Container.Name = "Control_PartIDManagement_Panel_Container";
            Control_PartIDManagement_Panel_Container.Size = new Size(446, 226);
            Control_PartIDManagement_Panel_Container.TabIndex = 2;
            // 
            // Control_PartIDManagement_TableLayout_Cards
            // 
            Control_PartIDManagement_TableLayout_Cards.AutoSize = true;
            Control_PartIDManagement_TableLayout_Cards.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_TableLayout_Cards.ColumnCount = 1;
            Control_PartIDManagement_TableLayout_Cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayout_Cards.Controls.Add(Control_PartIDManagement_Panel_RemoveCard, 0, 2);
            Control_PartIDManagement_TableLayout_Cards.Controls.Add(Control_PartIDManagement_Panel_AddCard, 0, 0);
            Control_PartIDManagement_TableLayout_Cards.Controls.Add(Control_PartIDManagement_Panel_EditCard, 0, 1);
            Control_PartIDManagement_TableLayout_Cards.Dock = DockStyle.Fill;
            Control_PartIDManagement_TableLayout_Cards.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            Control_PartIDManagement_TableLayout_Cards.Location = new Point(0, 0);
            Control_PartIDManagement_TableLayout_Cards.Name = "Control_PartIDManagement_TableLayout_Cards";
            Control_PartIDManagement_TableLayout_Cards.RowCount = 3;
            Control_PartIDManagement_TableLayout_Cards.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_Cards.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_Cards.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_Cards.Size = new Size(446, 226);
            Control_PartIDManagement_TableLayout_Cards.TabIndex = 1;
            Control_PartIDManagement_TableLayout_Cards.Visible = false;
            // 
            // Control_PartIDManagement_Panel_RemoveCard
            // 
            Control_PartIDManagement_Panel_RemoveCard.AutoSize = true;
            Control_PartIDManagement_Panel_RemoveCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_Panel_RemoveCard.BorderStyle = BorderStyle.FixedSingle;
            Control_PartIDManagement_Panel_RemoveCard.Controls.Add(Control_PartIDManagement_TableLayout_Remove);
            Control_PartIDManagement_Panel_RemoveCard.Dock = DockStyle.Fill;
            Control_PartIDManagement_Panel_RemoveCard.Location = new Point(3, 534);
            Control_PartIDManagement_Panel_RemoveCard.Name = "Control_PartIDManagement_Panel_RemoveCard";
            Control_PartIDManagement_Panel_RemoveCard.Size = new Size(440, 355);
            Control_PartIDManagement_Panel_RemoveCard.TabIndex = 2;
            // 
            // Control_PartIDManagement_TableLayout_Remove
            // 
            Control_PartIDManagement_TableLayout_Remove.AutoSize = true;
            Control_PartIDManagement_TableLayout_Remove.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_TableLayout_Remove.ColumnCount = 1;
            Control_PartIDManagement_TableLayout_Remove.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayout_Remove.Controls.Add(Control_PartIDManagement_TableLayout_RemoveContent, 0, 1);
            Control_PartIDManagement_TableLayout_Remove.Controls.Add(Control_PartIDManagement_TableLayout_Title, 0, 0);
            Control_PartIDManagement_TableLayout_Remove.Dock = DockStyle.Fill;
            Control_PartIDManagement_TableLayout_Remove.Location = new Point(0, 0);
            Control_PartIDManagement_TableLayout_Remove.Name = "Control_PartIDManagement_TableLayout_Remove";
            Control_PartIDManagement_TableLayout_Remove.Padding = new Padding(16);
            Control_PartIDManagement_TableLayout_Remove.RowCount = 2;
            Control_PartIDManagement_TableLayout_Remove.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_Remove.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayout_Remove.Size = new Size(438, 353);
            Control_PartIDManagement_TableLayout_Remove.TabIndex = 0;
            // 
            // Control_PartIDManagement_TableLayout_RemoveContent
            // 
            Control_PartIDManagement_TableLayout_RemoveContent.AutoSize = true;
            Control_PartIDManagement_TableLayout_RemoveContent.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_TableLayout_RemoveContent.ColumnCount = 1;
            Control_PartIDManagement_TableLayout_Remove.SetColumnSpan(Control_PartIDManagement_TableLayout_RemoveContent, 2);
            Control_PartIDManagement_TableLayout_RemoveContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayout_RemoveContent.Controls.Add(Control_PartIDManagement_TableLayout_RemoveActions, 0, 4);
            Control_PartIDManagement_TableLayout_RemoveContent.Controls.Add(Control_PartIDManagement_Suggestion_RemoveSelectPart, 0, 0);
            Control_PartIDManagement_TableLayout_RemoveContent.Controls.Add(Control_PartIDManagement_TableLayout_RemoveDetails, 0, 1);
            Control_PartIDManagement_TableLayout_RemoveContent.Controls.Add(Control_PartIDManagement_Label_RemoveWarning, 0, 3);
            Control_PartIDManagement_TableLayout_RemoveContent.Dock = DockStyle.Fill;
            Control_PartIDManagement_TableLayout_RemoveContent.Location = new Point(19, 84);
            Control_PartIDManagement_TableLayout_RemoveContent.Name = "Control_PartIDManagement_TableLayout_RemoveContent";
            Control_PartIDManagement_TableLayout_RemoveContent.RowCount = 5;
            Control_PartIDManagement_TableLayout_RemoveContent.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_RemoveContent.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_RemoveContent.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_RemoveContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayout_RemoveContent.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_RemoveContent.Size = new Size(400, 250);
            Control_PartIDManagement_TableLayout_RemoveContent.TabIndex = 2;
            // 
            // Control_PartIDManagement_TableLayout_RemoveActions
            // 
            Control_PartIDManagement_TableLayout_RemoveActions.AutoSize = true;
            Control_PartIDManagement_TableLayout_RemoveActions.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_TableLayout_RemoveActions.ColumnCount = 3;
            Control_PartIDManagement_TableLayout_RemoveActions.ColumnStyles.Add(new ColumnStyle());
            Control_PartIDManagement_TableLayout_RemoveActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayout_RemoveActions.ColumnStyles.Add(new ColumnStyle());
            Control_PartIDManagement_TableLayout_RemoveActions.Controls.Add(Control_PartIDManagement_Button_RemoveCancel, 2, 0);
            Control_PartIDManagement_TableLayout_RemoveActions.Controls.Add(Control_PartIDManagement_Button_RemoveConfirm, 0, 0);
            Control_PartIDManagement_TableLayout_RemoveActions.Dock = DockStyle.Fill;
            Control_PartIDManagement_TableLayout_RemoveActions.Location = new Point(3, 218);
            Control_PartIDManagement_TableLayout_RemoveActions.Name = "Control_PartIDManagement_TableLayout_RemoveActions";
            Control_PartIDManagement_TableLayout_RemoveActions.RowCount = 1;
            Control_PartIDManagement_TableLayout_RemoveActions.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_RemoveActions.Size = new Size(394, 29);
            Control_PartIDManagement_TableLayout_RemoveActions.TabIndex = 2;
            // 
            // Control_PartIDManagement_Button_RemoveCancel
            // 
            Control_PartIDManagement_Button_RemoveCancel.Location = new Point(316, 3);
            Control_PartIDManagement_Button_RemoveCancel.Name = "Control_PartIDManagement_Button_RemoveCancel";
            Control_PartIDManagement_Button_RemoveCancel.Size = new Size(75, 23);
            Control_PartIDManagement_Button_RemoveCancel.TabIndex = 0;
            // 
            // Control_PartIDManagement_Button_RemoveConfirm
            // 
            Control_PartIDManagement_Button_RemoveConfirm.Location = new Point(3, 3);
            Control_PartIDManagement_Button_RemoveConfirm.Name = "Control_PartIDManagement_Button_RemoveConfirm";
            Control_PartIDManagement_Button_RemoveConfirm.Size = new Size(75, 23);
            Control_PartIDManagement_Button_RemoveConfirm.TabIndex = 1;
            // 
            // Control_PartIDManagement_Suggestion_RemoveSelectPart
            // 
            Control_PartIDManagement_Suggestion_RemoveSelectPart.AutoSize = true;
            Control_PartIDManagement_Suggestion_RemoveSelectPart.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_Suggestion_RemoveSelectPart.BorderStyle = BorderStyle.FixedSingle;
            Control_PartIDManagement_Suggestion_RemoveSelectPart.Dock = DockStyle.Fill;
            Control_PartIDManagement_Suggestion_RemoveSelectPart.Location = new Point(3, 3);
            Control_PartIDManagement_Suggestion_RemoveSelectPart.Name = "Control_PartIDManagement_Suggestion_RemoveSelectPart";
            Control_PartIDManagement_Suggestion_RemoveSelectPart.Padding = new Padding(3);
            Control_PartIDManagement_Suggestion_RemoveSelectPart.Size = new Size(394, 31);
            Control_PartIDManagement_Suggestion_RemoveSelectPart.TabIndex = 0;
            // 
            // Control_PartIDManagement_TableLayout_RemoveDetails
            // 
            Control_PartIDManagement_TableLayout_RemoveDetails.AutoSize = true;
            Control_PartIDManagement_TableLayout_RemoveDetails.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_TableLayout_RemoveDetails.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            Control_PartIDManagement_TableLayout_RemoveDetails.ColumnCount = 2;
            Control_PartIDManagement_TableLayout_RemoveDetails.ColumnStyles.Add(new ColumnStyle());
            Control_PartIDManagement_TableLayout_RemoveDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayout_RemoveDetails.Controls.Add(Control_PartIDManagement_Label_RemoveItemNumber, 0, 0);
            Control_PartIDManagement_TableLayout_RemoveDetails.Controls.Add(Control_PartIDManagement_Label_RemoveItemNumberValue, 1, 0);
            Control_PartIDManagement_TableLayout_RemoveDetails.Controls.Add(Control_PartIDManagement_Label_RemoveDescriptionValue, 1, 2);
            Control_PartIDManagement_TableLayout_RemoveDetails.Controls.Add(Control_PartIDManagement_Label_RemoveCustomerValue, 1, 1);
            Control_PartIDManagement_TableLayout_RemoveDetails.Controls.Add(Control_PartIDManagement_Label_RemoveCustomer, 0, 1);
            Control_PartIDManagement_TableLayout_RemoveDetails.Controls.Add(Control_PartIDManagement_Label_RemoveDescription, 0, 2);
            Control_PartIDManagement_TableLayout_RemoveDetails.Controls.Add(Control_PartIDManagement_Label_RemoveType, 0, 3);
            Control_PartIDManagement_TableLayout_RemoveDetails.Controls.Add(Control_PartIDManagement_Label_RemoveIssuedBy, 0, 4);
            Control_PartIDManagement_TableLayout_RemoveDetails.Controls.Add(Control_PartIDManagement_Label_RemoveTypeValue, 1, 3);
            Control_PartIDManagement_TableLayout_RemoveDetails.Controls.Add(Control_PartIDManagement_Label_RemoveIssuedByValue, 1, 4);
            Control_PartIDManagement_TableLayout_RemoveDetails.Dock = DockStyle.Fill;
            Control_PartIDManagement_TableLayout_RemoveDetails.Location = new Point(3, 40);
            Control_PartIDManagement_TableLayout_RemoveDetails.Name = "Control_PartIDManagement_TableLayout_RemoveDetails";
            Control_PartIDManagement_TableLayout_RemoveDetails.RowCount = 5;
            Control_PartIDManagement_TableLayout_RemoveDetails.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_RemoveDetails.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_RemoveDetails.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_RemoveDetails.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_RemoveDetails.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_RemoveDetails.Size = new Size(394, 151);
            Control_PartIDManagement_TableLayout_RemoveDetails.TabIndex = 1;
            // 
            // Control_PartIDManagement_Label_RemoveItemNumber
            // 
            Control_PartIDManagement_Label_RemoveItemNumber.Dock = DockStyle.Fill;
            Control_PartIDManagement_Label_RemoveItemNumber.Location = new Point(4, 4);
            Control_PartIDManagement_Label_RemoveItemNumber.Margin = new Padding(3);
            Control_PartIDManagement_Label_RemoveItemNumber.Name = "Control_PartIDManagement_Label_RemoveItemNumber";
            Control_PartIDManagement_Label_RemoveItemNumber.Size = new Size(100, 23);
            Control_PartIDManagement_Label_RemoveItemNumber.TabIndex = 0;
            Control_PartIDManagement_Label_RemoveItemNumber.Text = "Part Number: ";
            Control_PartIDManagement_Label_RemoveItemNumber.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Control_PartIDManagement_Label_RemoveItemNumberValue
            // 
            Control_PartIDManagement_Label_RemoveItemNumberValue.Dock = DockStyle.Fill;
            Control_PartIDManagement_Label_RemoveItemNumberValue.Location = new Point(111, 4);
            Control_PartIDManagement_Label_RemoveItemNumberValue.Margin = new Padding(3);
            Control_PartIDManagement_Label_RemoveItemNumberValue.Name = "Control_PartIDManagement_Label_RemoveItemNumberValue";
            Control_PartIDManagement_Label_RemoveItemNumberValue.Size = new Size(279, 23);
            Control_PartIDManagement_Label_RemoveItemNumberValue.TabIndex = 1;
            Control_PartIDManagement_Label_RemoveItemNumberValue.Text = "{PartID}";
            Control_PartIDManagement_Label_RemoveItemNumberValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_PartIDManagement_Label_RemoveDescriptionValue
            // 
            Control_PartIDManagement_Label_RemoveDescriptionValue.Dock = DockStyle.Fill;
            Control_PartIDManagement_Label_RemoveDescriptionValue.Location = new Point(111, 64);
            Control_PartIDManagement_Label_RemoveDescriptionValue.Margin = new Padding(3);
            Control_PartIDManagement_Label_RemoveDescriptionValue.Name = "Control_PartIDManagement_Label_RemoveDescriptionValue";
            Control_PartIDManagement_Label_RemoveDescriptionValue.Size = new Size(279, 23);
            Control_PartIDManagement_Label_RemoveDescriptionValue.TabIndex = 5;
            Control_PartIDManagement_Label_RemoveDescriptionValue.Text = "{Description}";
            Control_PartIDManagement_Label_RemoveDescriptionValue.TextAlign = ContentAlignment.MiddleLeft;
            Control_PartIDManagement_Label_RemoveDescriptionValue.Visible = false;
            // 
            // Control_PartIDManagement_Label_RemoveCustomerValue
            // 
            Control_PartIDManagement_Label_RemoveCustomerValue.Dock = DockStyle.Fill;
            Control_PartIDManagement_Label_RemoveCustomerValue.Location = new Point(111, 34);
            Control_PartIDManagement_Label_RemoveCustomerValue.Margin = new Padding(3);
            Control_PartIDManagement_Label_RemoveCustomerValue.Name = "Control_PartIDManagement_Label_RemoveCustomerValue";
            Control_PartIDManagement_Label_RemoveCustomerValue.Size = new Size(279, 23);
            Control_PartIDManagement_Label_RemoveCustomerValue.TabIndex = 3;
            Control_PartIDManagement_Label_RemoveCustomerValue.Text = "{Customer}";
            Control_PartIDManagement_Label_RemoveCustomerValue.TextAlign = ContentAlignment.MiddleLeft;
            Control_PartIDManagement_Label_RemoveCustomerValue.Visible = false;
            // 
            // Control_PartIDManagement_Label_RemoveCustomer
            // 
            Control_PartIDManagement_Label_RemoveCustomer.Dock = DockStyle.Fill;
            Control_PartIDManagement_Label_RemoveCustomer.Location = new Point(4, 34);
            Control_PartIDManagement_Label_RemoveCustomer.Margin = new Padding(3);
            Control_PartIDManagement_Label_RemoveCustomer.Name = "Control_PartIDManagement_Label_RemoveCustomer";
            Control_PartIDManagement_Label_RemoveCustomer.Size = new Size(100, 23);
            Control_PartIDManagement_Label_RemoveCustomer.TabIndex = 2;
            Control_PartIDManagement_Label_RemoveCustomer.Text = "Customer: ";
            Control_PartIDManagement_Label_RemoveCustomer.TextAlign = ContentAlignment.MiddleRight;
            Control_PartIDManagement_Label_RemoveCustomer.Visible = false;
            // 
            // Control_PartIDManagement_Label_RemoveDescription
            // 
            Control_PartIDManagement_Label_RemoveDescription.Dock = DockStyle.Fill;
            Control_PartIDManagement_Label_RemoveDescription.Location = new Point(4, 64);
            Control_PartIDManagement_Label_RemoveDescription.Margin = new Padding(3);
            Control_PartIDManagement_Label_RemoveDescription.Name = "Control_PartIDManagement_Label_RemoveDescription";
            Control_PartIDManagement_Label_RemoveDescription.Size = new Size(100, 23);
            Control_PartIDManagement_Label_RemoveDescription.TabIndex = 4;
            Control_PartIDManagement_Label_RemoveDescription.Text = "Description: ";
            Control_PartIDManagement_Label_RemoveDescription.TextAlign = ContentAlignment.MiddleRight;
            Control_PartIDManagement_Label_RemoveDescription.Visible = false;
            // 
            // Control_PartIDManagement_Label_RemoveType
            // 
            Control_PartIDManagement_Label_RemoveType.Dock = DockStyle.Fill;
            Control_PartIDManagement_Label_RemoveType.Location = new Point(4, 94);
            Control_PartIDManagement_Label_RemoveType.Margin = new Padding(3);
            Control_PartIDManagement_Label_RemoveType.Name = "Control_PartIDManagement_Label_RemoveType";
            Control_PartIDManagement_Label_RemoveType.Size = new Size(100, 23);
            Control_PartIDManagement_Label_RemoveType.TabIndex = 6;
            Control_PartIDManagement_Label_RemoveType.Text = "Item Type: ";
            Control_PartIDManagement_Label_RemoveType.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Control_PartIDManagement_Label_RemoveIssuedBy
            // 
            Control_PartIDManagement_Label_RemoveIssuedBy.Dock = DockStyle.Fill;
            Control_PartIDManagement_Label_RemoveIssuedBy.Location = new Point(4, 124);
            Control_PartIDManagement_Label_RemoveIssuedBy.Margin = new Padding(3);
            Control_PartIDManagement_Label_RemoveIssuedBy.Name = "Control_PartIDManagement_Label_RemoveIssuedBy";
            Control_PartIDManagement_Label_RemoveIssuedBy.Size = new Size(100, 23);
            Control_PartIDManagement_Label_RemoveIssuedBy.TabIndex = 8;
            Control_PartIDManagement_Label_RemoveIssuedBy.Text = "Issued By: ";
            Control_PartIDManagement_Label_RemoveIssuedBy.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Control_PartIDManagement_Label_RemoveTypeValue
            // 
            Control_PartIDManagement_Label_RemoveTypeValue.Dock = DockStyle.Fill;
            Control_PartIDManagement_Label_RemoveTypeValue.Location = new Point(111, 94);
            Control_PartIDManagement_Label_RemoveTypeValue.Margin = new Padding(3);
            Control_PartIDManagement_Label_RemoveTypeValue.Name = "Control_PartIDManagement_Label_RemoveTypeValue";
            Control_PartIDManagement_Label_RemoveTypeValue.Size = new Size(279, 23);
            Control_PartIDManagement_Label_RemoveTypeValue.TabIndex = 7;
            Control_PartIDManagement_Label_RemoveTypeValue.Text = "{ItemType}";
            Control_PartIDManagement_Label_RemoveTypeValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_PartIDManagement_Label_RemoveIssuedByValue
            // 
            Control_PartIDManagement_Label_RemoveIssuedByValue.Dock = DockStyle.Fill;
            Control_PartIDManagement_Label_RemoveIssuedByValue.Location = new Point(111, 124);
            Control_PartIDManagement_Label_RemoveIssuedByValue.Margin = new Padding(3);
            Control_PartIDManagement_Label_RemoveIssuedByValue.Name = "Control_PartIDManagement_Label_RemoveIssuedByValue";
            Control_PartIDManagement_Label_RemoveIssuedByValue.Size = new Size(279, 23);
            Control_PartIDManagement_Label_RemoveIssuedByValue.TabIndex = 9;
            Control_PartIDManagement_Label_RemoveIssuedByValue.Text = "{User}";
            Control_PartIDManagement_Label_RemoveIssuedByValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_PartIDManagement_Label_RemoveWarning
            // 
            Control_PartIDManagement_Label_RemoveWarning.AutoSize = true;
            Control_PartIDManagement_Label_RemoveWarning.Dock = DockStyle.Fill;
            Control_PartIDManagement_Label_RemoveWarning.ForeColor = Color.FromArgb(192, 0, 0);
            Control_PartIDManagement_Label_RemoveWarning.Location = new Point(3, 197);
            Control_PartIDManagement_Label_RemoveWarning.Margin = new Padding(3);
            Control_PartIDManagement_Label_RemoveWarning.Name = "Control_PartIDManagement_Label_RemoveWarning";
            Control_PartIDManagement_Label_RemoveWarning.Size = new Size(394, 15);
            Control_PartIDManagement_Label_RemoveWarning.TabIndex = 2;
            Control_PartIDManagement_Label_RemoveWarning.Text = "Warning: Removal is permanent.";
            Control_PartIDManagement_Label_RemoveWarning.TextAlign = ContentAlignment.BottomLeft;
            // 
            // Control_PartIDManagement_TableLayout_Title
            // 
            Control_PartIDManagement_TableLayout_Title.AutoSize = true;
            Control_PartIDManagement_TableLayout_Title.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_TableLayout_Title.ColumnCount = 2;
            Control_PartIDManagement_TableLayout_Title.ColumnStyles.Add(new ColumnStyle());
            Control_PartIDManagement_TableLayout_Title.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayout_Title.Controls.Add(Control_PartIDManagement_Label_RemoveIcon, 0, 0);
            Control_PartIDManagement_TableLayout_Title.Controls.Add(Control_PartIDManagement_Label_RemoveTitle, 1, 0);
            Control_PartIDManagement_TableLayout_Title.Dock = DockStyle.Fill;
            Control_PartIDManagement_TableLayout_Title.Location = new Point(19, 19);
            Control_PartIDManagement_TableLayout_Title.Name = "Control_PartIDManagement_TableLayout_Title";
            Control_PartIDManagement_TableLayout_Title.RowCount = 1;
            Control_PartIDManagement_TableLayout_Title.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayout_Title.Size = new Size(400, 59);
            Control_PartIDManagement_TableLayout_Title.TabIndex = 3;
            // 
            // Control_PartIDManagement_Label_RemoveIcon
            // 
            Control_PartIDManagement_Label_RemoveIcon.AutoSize = true;
            Control_PartIDManagement_Label_RemoveIcon.Dock = DockStyle.Fill;
            Control_PartIDManagement_Label_RemoveIcon.Font = new Font("Segoe UI Emoji", 28F);
            Control_PartIDManagement_Label_RemoveIcon.Location = new Point(4, 4);
            Control_PartIDManagement_Label_RemoveIcon.Margin = new Padding(4);
            Control_PartIDManagement_Label_RemoveIcon.Name = "Control_PartIDManagement_Label_RemoveIcon";
            Control_PartIDManagement_Label_RemoveIcon.Size = new Size(74, 51);
            Control_PartIDManagement_Label_RemoveIcon.TabIndex = 0;
            Control_PartIDManagement_Label_RemoveIcon.Text = "🗑️";
            Control_PartIDManagement_Label_RemoveIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_PartIDManagement_Label_RemoveTitle
            // 
            Control_PartIDManagement_Label_RemoveTitle.AutoSize = true;
            Control_PartIDManagement_Label_RemoveTitle.Dock = DockStyle.Fill;
            Control_PartIDManagement_Label_RemoveTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            Control_PartIDManagement_Label_RemoveTitle.Location = new Point(86, 4);
            Control_PartIDManagement_Label_RemoveTitle.Margin = new Padding(4);
            Control_PartIDManagement_Label_RemoveTitle.Name = "Control_PartIDManagement_Label_RemoveTitle";
            Control_PartIDManagement_Label_RemoveTitle.Size = new Size(310, 51);
            Control_PartIDManagement_Label_RemoveTitle.TabIndex = 1;
            Control_PartIDManagement_Label_RemoveTitle.Text = "Remove Part";
            Control_PartIDManagement_Label_RemoveTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_PartIDManagement_Panel_AddCard
            // 
            Control_PartIDManagement_Panel_AddCard.AutoSize = true;
            Control_PartIDManagement_Panel_AddCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_Panel_AddCard.BorderStyle = BorderStyle.FixedSingle;
            Control_PartIDManagement_Panel_AddCard.Controls.Add(Control_PartIDManagement_TableLayout_Add);
            Control_PartIDManagement_Panel_AddCard.Dock = DockStyle.Fill;
            Control_PartIDManagement_Panel_AddCard.Location = new Point(3, 3);
            Control_PartIDManagement_Panel_AddCard.Name = "Control_PartIDManagement_Panel_AddCard";
            Control_PartIDManagement_Panel_AddCard.Size = new Size(440, 241);
            Control_PartIDManagement_Panel_AddCard.TabIndex = 0;
            // 
            // Control_PartIDManagement_TableLayout_Add
            // 
            Control_PartIDManagement_TableLayout_Add.AutoSize = true;
            Control_PartIDManagement_TableLayout_Add.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_TableLayout_Add.ColumnCount = 1;
            Control_PartIDManagement_TableLayout_Add.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayout_Add.Controls.Add(Control_PartIDManagement_TableLayout_AddContent, 0, 1);
            Control_PartIDManagement_TableLayout_Add.Controls.Add(Control_PartIDManagement_Add_TableLayout_Title, 0, 0);
            Control_PartIDManagement_TableLayout_Add.Dock = DockStyle.Fill;
            Control_PartIDManagement_TableLayout_Add.Location = new Point(0, 0);
            Control_PartIDManagement_TableLayout_Add.Name = "Control_PartIDManagement_TableLayout_Add";
            Control_PartIDManagement_TableLayout_Add.Padding = new Padding(16);
            Control_PartIDManagement_TableLayout_Add.RowCount = 2;
            Control_PartIDManagement_TableLayout_Add.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_Add.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayout_Add.Size = new Size(438, 239);
            Control_PartIDManagement_TableLayout_Add.TabIndex = 0;
            // 
            // Control_PartIDManagement_TableLayout_AddContent
            // 
            Control_PartIDManagement_TableLayout_AddContent.AutoSize = true;
            Control_PartIDManagement_TableLayout_AddContent.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_TableLayout_AddContent.ColumnCount = 1;
            Control_PartIDManagement_TableLayout_Add.SetColumnSpan(Control_PartIDManagement_TableLayout_AddContent, 2);
            Control_PartIDManagement_TableLayout_AddContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayout_AddContent.Controls.Add(Control_PartIDManagement_Suggestion_AddPartNumber, 0, 0);
            Control_PartIDManagement_TableLayout_AddContent.Controls.Add(Control_PartIDManagement_Suggestion_AddItemType, 0, 1);
            Control_PartIDManagement_TableLayout_AddContent.Controls.Add(Control_PartIDManagement_CheckBox_AddRequiresColorCode, 0, 2);
            Control_PartIDManagement_TableLayout_AddContent.Controls.Add(Control_PartIDManagement_TableLayout_AddActions, 0, 4);
            Control_PartIDManagement_TableLayout_AddContent.Dock = DockStyle.Fill;
            Control_PartIDManagement_TableLayout_AddContent.Location = new Point(19, 84);
            Control_PartIDManagement_TableLayout_AddContent.Name = "Control_PartIDManagement_TableLayout_AddContent";
            Control_PartIDManagement_TableLayout_AddContent.RowCount = 5;
            Control_PartIDManagement_TableLayout_AddContent.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_AddContent.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_AddContent.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_AddContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayout_AddContent.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_AddContent.Size = new Size(400, 136);
            Control_PartIDManagement_TableLayout_AddContent.TabIndex = 2;
            // 
            // Control_PartIDManagement_Suggestion_AddPartNumber
            // 
            Control_PartIDManagement_Suggestion_AddPartNumber.AutoSize = true;
            Control_PartIDManagement_Suggestion_AddPartNumber.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_Suggestion_AddPartNumber.BorderStyle = BorderStyle.FixedSingle;
            Control_PartIDManagement_Suggestion_AddPartNumber.Dock = DockStyle.Fill;
            Control_PartIDManagement_Suggestion_AddPartNumber.EnableSuggestions = false;
            Control_PartIDManagement_Suggestion_AddPartNumber.Location = new Point(3, 3);
            Control_PartIDManagement_Suggestion_AddPartNumber.Name = "Control_PartIDManagement_Suggestion_AddPartNumber";
            Control_PartIDManagement_Suggestion_AddPartNumber.Padding = new Padding(3);
            Control_PartIDManagement_Suggestion_AddPartNumber.ShowF4Button = false;
            Control_PartIDManagement_Suggestion_AddPartNumber.Size = new Size(394, 31);
            Control_PartIDManagement_Suggestion_AddPartNumber.TabIndex = 0;
            // 
            // Control_PartIDManagement_Suggestion_AddItemType
            // 
            Control_PartIDManagement_Suggestion_AddItemType.AutoSize = true;
            Control_PartIDManagement_Suggestion_AddItemType.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_Suggestion_AddItemType.BorderStyle = BorderStyle.FixedSingle;
            Control_PartIDManagement_Suggestion_AddItemType.Dock = DockStyle.Fill;
            Control_PartIDManagement_Suggestion_AddItemType.Location = new Point(3, 40);
            Control_PartIDManagement_Suggestion_AddItemType.Name = "Control_PartIDManagement_Suggestion_AddItemType";
            Control_PartIDManagement_Suggestion_AddItemType.Padding = new Padding(3);
            Control_PartIDManagement_Suggestion_AddItemType.Size = new Size(394, 31);
            Control_PartIDManagement_Suggestion_AddItemType.TabIndex = 1;
            // 
            // Control_PartIDManagement_CheckBox_AddRequiresColorCode
            // 
            Control_PartIDManagement_CheckBox_AddRequiresColorCode.AutoSize = true;
            Control_PartIDManagement_CheckBox_AddRequiresColorCode.Dock = DockStyle.Fill;
            Control_PartIDManagement_CheckBox_AddRequiresColorCode.Location = new Point(3, 77);
            Control_PartIDManagement_CheckBox_AddRequiresColorCode.Name = "Control_PartIDManagement_CheckBox_AddRequiresColorCode";
            Control_PartIDManagement_CheckBox_AddRequiresColorCode.Padding = new Padding(1);
            Control_PartIDManagement_CheckBox_AddRequiresColorCode.Size = new Size(394, 21);
            Control_PartIDManagement_CheckBox_AddRequiresColorCode.TabIndex = 2;
            Control_PartIDManagement_CheckBox_AddRequiresColorCode.Text = "Requires color code and work order";
            // 
            // Control_PartIDManagement_TableLayout_AddActions
            // 
            Control_PartIDManagement_TableLayout_AddActions.AutoSize = true;
            Control_PartIDManagement_TableLayout_AddActions.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_TableLayout_AddActions.ColumnCount = 3;
            Control_PartIDManagement_TableLayout_AddActions.ColumnStyles.Add(new ColumnStyle());
            Control_PartIDManagement_TableLayout_AddActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayout_AddActions.ColumnStyles.Add(new ColumnStyle());
            Control_PartIDManagement_TableLayout_AddActions.Controls.Add(Control_PartIDManagement_Button_AddSave, 0, 0);
            Control_PartIDManagement_TableLayout_AddActions.Controls.Add(Control_PartIDManagement_Button_AddClear, 2, 0);
            Control_PartIDManagement_TableLayout_AddActions.Dock = DockStyle.Fill;
            Control_PartIDManagement_TableLayout_AddActions.Location = new Point(3, 104);
            Control_PartIDManagement_TableLayout_AddActions.Name = "Control_PartIDManagement_TableLayout_AddActions";
            Control_PartIDManagement_TableLayout_AddActions.RowCount = 1;
            Control_PartIDManagement_TableLayout_AddActions.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_AddActions.Size = new Size(394, 29);
            Control_PartIDManagement_TableLayout_AddActions.TabIndex = 4;
            // 
            // Control_PartIDManagement_Button_AddSave
            // 
            Control_PartIDManagement_Button_AddSave.Location = new Point(3, 3);
            Control_PartIDManagement_Button_AddSave.Name = "Control_PartIDManagement_Button_AddSave";
            Control_PartIDManagement_Button_AddSave.Size = new Size(75, 23);
            Control_PartIDManagement_Button_AddSave.TabIndex = 1;
            // 
            // Control_PartIDManagement_Button_AddClear
            // 
            Control_PartIDManagement_Button_AddClear.Location = new Point(316, 3);
            Control_PartIDManagement_Button_AddClear.Name = "Control_PartIDManagement_Button_AddClear";
            Control_PartIDManagement_Button_AddClear.Size = new Size(75, 23);
            Control_PartIDManagement_Button_AddClear.TabIndex = 0;
            // 
            // Control_PartIDManagement_Add_TableLayout_Title
            // 
            Control_PartIDManagement_Add_TableLayout_Title.AutoSize = true;
            Control_PartIDManagement_Add_TableLayout_Title.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_Add_TableLayout_Title.ColumnCount = 2;
            Control_PartIDManagement_Add_TableLayout_Title.ColumnStyles.Add(new ColumnStyle());
            Control_PartIDManagement_Add_TableLayout_Title.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_Add_TableLayout_Title.Controls.Add(Control_PartIDManagement_Label_AddIcon, 0, 0);
            Control_PartIDManagement_Add_TableLayout_Title.Controls.Add(Control_PartIDManagement_Label_AddTitle, 1, 0);
            Control_PartIDManagement_Add_TableLayout_Title.Dock = DockStyle.Fill;
            Control_PartIDManagement_Add_TableLayout_Title.Location = new Point(19, 19);
            Control_PartIDManagement_Add_TableLayout_Title.Name = "Control_PartIDManagement_Add_TableLayout_Title";
            Control_PartIDManagement_Add_TableLayout_Title.RowCount = 1;
            Control_PartIDManagement_Add_TableLayout_Title.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_Add_TableLayout_Title.Size = new Size(400, 59);
            Control_PartIDManagement_Add_TableLayout_Title.TabIndex = 0;
            // 
            // Control_PartIDManagement_Label_AddIcon
            // 
            Control_PartIDManagement_Label_AddIcon.AutoSize = true;
            Control_PartIDManagement_Label_AddIcon.Dock = DockStyle.Fill;
            Control_PartIDManagement_Label_AddIcon.Font = new Font("Segoe UI Emoji", 28F);
            Control_PartIDManagement_Label_AddIcon.Location = new Point(4, 4);
            Control_PartIDManagement_Label_AddIcon.Margin = new Padding(4);
            Control_PartIDManagement_Label_AddIcon.Name = "Control_PartIDManagement_Label_AddIcon";
            Control_PartIDManagement_Label_AddIcon.Size = new Size(74, 51);
            Control_PartIDManagement_Label_AddIcon.TabIndex = 0;
            Control_PartIDManagement_Label_AddIcon.Text = "🆕";
            Control_PartIDManagement_Label_AddIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_PartIDManagement_Label_AddTitle
            // 
            Control_PartIDManagement_Label_AddTitle.AutoSize = true;
            Control_PartIDManagement_Label_AddTitle.Dock = DockStyle.Fill;
            Control_PartIDManagement_Label_AddTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            Control_PartIDManagement_Label_AddTitle.Location = new Point(86, 4);
            Control_PartIDManagement_Label_AddTitle.Margin = new Padding(4);
            Control_PartIDManagement_Label_AddTitle.Name = "Control_PartIDManagement_Label_AddTitle";
            Control_PartIDManagement_Label_AddTitle.Size = new Size(310, 51);
            Control_PartIDManagement_Label_AddTitle.TabIndex = 1;
            Control_PartIDManagement_Label_AddTitle.Text = "Add Part";
            Control_PartIDManagement_Label_AddTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_PartIDManagement_Panel_EditCard
            // 
            Control_PartIDManagement_Panel_EditCard.AutoSize = true;
            Control_PartIDManagement_Panel_EditCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_Panel_EditCard.BorderStyle = BorderStyle.FixedSingle;
            Control_PartIDManagement_Panel_EditCard.Controls.Add(Control_PartIDManagement_TableLayout_Edit);
            Control_PartIDManagement_Panel_EditCard.Dock = DockStyle.Fill;
            Control_PartIDManagement_Panel_EditCard.Location = new Point(3, 250);
            Control_PartIDManagement_Panel_EditCard.Name = "Control_PartIDManagement_Panel_EditCard";
            Control_PartIDManagement_Panel_EditCard.Size = new Size(440, 278);
            Control_PartIDManagement_Panel_EditCard.TabIndex = 1;
            // 
            // Control_PartIDManagement_TableLayout_Edit
            // 
            Control_PartIDManagement_TableLayout_Edit.AutoSize = true;
            Control_PartIDManagement_TableLayout_Edit.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_TableLayout_Edit.ColumnCount = 1;
            Control_PartIDManagement_TableLayout_Edit.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayout_Edit.Controls.Add(Control_PartIDManagement_TableLayoutPanel_Title, 0, 0);
            Control_PartIDManagement_TableLayout_Edit.Controls.Add(Control_PartIDManagement_TableLayout_EditContent, 0, 1);
            Control_PartIDManagement_TableLayout_Edit.Dock = DockStyle.Fill;
            Control_PartIDManagement_TableLayout_Edit.Location = new Point(0, 0);
            Control_PartIDManagement_TableLayout_Edit.Name = "Control_PartIDManagement_TableLayout_Edit";
            Control_PartIDManagement_TableLayout_Edit.Padding = new Padding(16);
            Control_PartIDManagement_TableLayout_Edit.RowCount = 2;
            Control_PartIDManagement_TableLayout_Edit.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_Edit.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayout_Edit.Size = new Size(438, 276);
            Control_PartIDManagement_TableLayout_Edit.TabIndex = 0;
            // 
            // Control_PartIDManagement_TableLayoutPanel_Title
            // 
            Control_PartIDManagement_TableLayoutPanel_Title.AutoSize = true;
            Control_PartIDManagement_TableLayoutPanel_Title.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_TableLayoutPanel_Title.ColumnCount = 2;
            Control_PartIDManagement_TableLayoutPanel_Title.ColumnStyles.Add(new ColumnStyle());
            Control_PartIDManagement_TableLayoutPanel_Title.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayoutPanel_Title.Controls.Add(Control_PartIDManagement_Label_EditIcon, 0, 0);
            Control_PartIDManagement_TableLayoutPanel_Title.Controls.Add(Control_PartIDManagement_Label_EditTitle, 1, 0);
            Control_PartIDManagement_TableLayoutPanel_Title.Dock = DockStyle.Fill;
            Control_PartIDManagement_TableLayoutPanel_Title.Location = new Point(19, 19);
            Control_PartIDManagement_TableLayoutPanel_Title.Name = "Control_PartIDManagement_TableLayoutPanel_Title";
            Control_PartIDManagement_TableLayoutPanel_Title.RowCount = 1;
            Control_PartIDManagement_TableLayoutPanel_Title.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayoutPanel_Title.Size = new Size(400, 59);
            Control_PartIDManagement_TableLayoutPanel_Title.TabIndex = 5;
            // 
            // Control_PartIDManagement_Label_EditIcon
            // 
            Control_PartIDManagement_Label_EditIcon.AutoSize = true;
            Control_PartIDManagement_Label_EditIcon.Dock = DockStyle.Fill;
            Control_PartIDManagement_Label_EditIcon.Font = new Font("Segoe UI Emoji", 28F);
            Control_PartIDManagement_Label_EditIcon.Location = new Point(4, 4);
            Control_PartIDManagement_Label_EditIcon.Margin = new Padding(4);
            Control_PartIDManagement_Label_EditIcon.Name = "Control_PartIDManagement_Label_EditIcon";
            Control_PartIDManagement_Label_EditIcon.Size = new Size(74, 51);
            Control_PartIDManagement_Label_EditIcon.TabIndex = 0;
            Control_PartIDManagement_Label_EditIcon.Text = "✏️";
            Control_PartIDManagement_Label_EditIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_PartIDManagement_Label_EditTitle
            // 
            Control_PartIDManagement_Label_EditTitle.AutoSize = true;
            Control_PartIDManagement_Label_EditTitle.Dock = DockStyle.Fill;
            Control_PartIDManagement_Label_EditTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            Control_PartIDManagement_Label_EditTitle.Location = new Point(86, 4);
            Control_PartIDManagement_Label_EditTitle.Margin = new Padding(4);
            Control_PartIDManagement_Label_EditTitle.Name = "Control_PartIDManagement_Label_EditTitle";
            Control_PartIDManagement_Label_EditTitle.Size = new Size(310, 51);
            Control_PartIDManagement_Label_EditTitle.TabIndex = 1;
            Control_PartIDManagement_Label_EditTitle.Text = "Edit Part";
            Control_PartIDManagement_Label_EditTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_PartIDManagement_TableLayout_EditContent
            // 
            Control_PartIDManagement_TableLayout_EditContent.AutoSize = true;
            Control_PartIDManagement_TableLayout_EditContent.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_TableLayout_EditContent.ColumnCount = 1;
            Control_PartIDManagement_TableLayout_Edit.SetColumnSpan(Control_PartIDManagement_TableLayout_EditContent, 2);
            Control_PartIDManagement_TableLayout_EditContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayout_EditContent.Controls.Add(Control_PartIDManagement_TableLayoutPanel_EditActions, 0, 5);
            Control_PartIDManagement_TableLayout_EditContent.Controls.Add(Control_PartIDManagement_Suggestion_EditSelectPart, 0, 0);
            Control_PartIDManagement_TableLayout_EditContent.Controls.Add(Control_PartIDManagement_Suggestion_EditItemType, 0, 2);
            Control_PartIDManagement_TableLayout_EditContent.Controls.Add(Control_PartIDManagement_CheckBox_EditRequiresColorCode, 0, 3);
            Control_PartIDManagement_TableLayout_EditContent.Controls.Add(Control_PartIDManagement_Suggestion_EditNewPartNumber, 0, 1);
            Control_PartIDManagement_TableLayout_EditContent.Dock = DockStyle.Fill;
            Control_PartIDManagement_TableLayout_EditContent.Location = new Point(19, 84);
            Control_PartIDManagement_TableLayout_EditContent.Name = "Control_PartIDManagement_TableLayout_EditContent";
            Control_PartIDManagement_TableLayout_EditContent.RowCount = 6;
            Control_PartIDManagement_TableLayout_EditContent.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_EditContent.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_EditContent.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_EditContent.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_EditContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayout_EditContent.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_EditContent.Size = new Size(400, 173);
            Control_PartIDManagement_TableLayout_EditContent.TabIndex = 2;
            // 
            // Control_PartIDManagement_TableLayoutPanel_EditActions
            // 
            Control_PartIDManagement_TableLayoutPanel_EditActions.AutoSize = true;
            Control_PartIDManagement_TableLayoutPanel_EditActions.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_TableLayoutPanel_EditActions.ColumnCount = 3;
            Control_PartIDManagement_TableLayoutPanel_EditActions.ColumnStyles.Add(new ColumnStyle());
            Control_PartIDManagement_TableLayoutPanel_EditActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayoutPanel_EditActions.ColumnStyles.Add(new ColumnStyle());
            Control_PartIDManagement_TableLayoutPanel_EditActions.Controls.Add(Control_PartIDManagement_Button_EditSave, 0, 0);
            Control_PartIDManagement_TableLayoutPanel_EditActions.Controls.Add(Control_PartIDManagement_Button_EditReset, 2, 0);
            Control_PartIDManagement_TableLayoutPanel_EditActions.Dock = DockStyle.Fill;
            Control_PartIDManagement_TableLayoutPanel_EditActions.Location = new Point(3, 141);
            Control_PartIDManagement_TableLayoutPanel_EditActions.Name = "Control_PartIDManagement_TableLayoutPanel_EditActions";
            Control_PartIDManagement_TableLayoutPanel_EditActions.RowCount = 1;
            Control_PartIDManagement_TableLayoutPanel_EditActions.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayoutPanel_EditActions.Size = new Size(394, 29);
            Control_PartIDManagement_TableLayoutPanel_EditActions.TabIndex = 6;
            // 
            // Control_PartIDManagement_Button_EditSave
            // 
            Control_PartIDManagement_Button_EditSave.Location = new Point(3, 3);
            Control_PartIDManagement_Button_EditSave.Name = "Control_PartIDManagement_Button_EditSave";
            Control_PartIDManagement_Button_EditSave.Size = new Size(75, 23);
            Control_PartIDManagement_Button_EditSave.TabIndex = 1;
            // 
            // Control_PartIDManagement_Button_EditReset
            // 
            Control_PartIDManagement_Button_EditReset.Location = new Point(316, 3);
            Control_PartIDManagement_Button_EditReset.Name = "Control_PartIDManagement_Button_EditReset";
            Control_PartIDManagement_Button_EditReset.Size = new Size(75, 23);
            Control_PartIDManagement_Button_EditReset.TabIndex = 0;
            // 
            // Control_PartIDManagement_Suggestion_EditSelectPart
            // 
            Control_PartIDManagement_Suggestion_EditSelectPart.AutoSize = true;
            Control_PartIDManagement_Suggestion_EditSelectPart.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_Suggestion_EditSelectPart.BorderStyle = BorderStyle.FixedSingle;
            Control_PartIDManagement_Suggestion_EditSelectPart.Dock = DockStyle.Fill;
            Control_PartIDManagement_Suggestion_EditSelectPart.Location = new Point(3, 3);
            Control_PartIDManagement_Suggestion_EditSelectPart.Name = "Control_PartIDManagement_Suggestion_EditSelectPart";
            Control_PartIDManagement_Suggestion_EditSelectPart.Padding = new Padding(3);
            Control_PartIDManagement_Suggestion_EditSelectPart.Size = new Size(394, 31);
            Control_PartIDManagement_Suggestion_EditSelectPart.TabIndex = 0;
            // 
            // Control_PartIDManagement_Suggestion_EditItemType
            // 
            Control_PartIDManagement_Suggestion_EditItemType.AutoSize = true;
            Control_PartIDManagement_Suggestion_EditItemType.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_Suggestion_EditItemType.BorderStyle = BorderStyle.FixedSingle;
            Control_PartIDManagement_Suggestion_EditItemType.Dock = DockStyle.Fill;
            Control_PartIDManagement_Suggestion_EditItemType.Location = new Point(3, 77);
            Control_PartIDManagement_Suggestion_EditItemType.Name = "Control_PartIDManagement_Suggestion_EditItemType";
            Control_PartIDManagement_Suggestion_EditItemType.Padding = new Padding(3);
            Control_PartIDManagement_Suggestion_EditItemType.Size = new Size(394, 31);
            Control_PartIDManagement_Suggestion_EditItemType.TabIndex = 2;
            // 
            // Control_PartIDManagement_CheckBox_EditRequiresColorCode
            // 
            Control_PartIDManagement_CheckBox_EditRequiresColorCode.AutoSize = true;
            Control_PartIDManagement_CheckBox_EditRequiresColorCode.Dock = DockStyle.Fill;
            Control_PartIDManagement_CheckBox_EditRequiresColorCode.Location = new Point(3, 114);
            Control_PartIDManagement_CheckBox_EditRequiresColorCode.Name = "Control_PartIDManagement_CheckBox_EditRequiresColorCode";
            Control_PartIDManagement_CheckBox_EditRequiresColorCode.Padding = new Padding(1);
            Control_PartIDManagement_CheckBox_EditRequiresColorCode.Size = new Size(394, 21);
            Control_PartIDManagement_CheckBox_EditRequiresColorCode.TabIndex = 3;
            Control_PartIDManagement_CheckBox_EditRequiresColorCode.Text = "Requires color code and work order";
            // 
            // Control_PartIDManagement_Suggestion_EditNewPartNumber
            // 
            Control_PartIDManagement_Suggestion_EditNewPartNumber.AutoSize = true;
            Control_PartIDManagement_Suggestion_EditNewPartNumber.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_Suggestion_EditNewPartNumber.BorderStyle = BorderStyle.FixedSingle;
            Control_PartIDManagement_Suggestion_EditNewPartNumber.Dock = DockStyle.Fill;
            Control_PartIDManagement_Suggestion_EditNewPartNumber.EnableSuggestions = false;
            Control_PartIDManagement_Suggestion_EditNewPartNumber.Location = new Point(3, 40);
            Control_PartIDManagement_Suggestion_EditNewPartNumber.Name = "Control_PartIDManagement_Suggestion_EditNewPartNumber";
            Control_PartIDManagement_Suggestion_EditNewPartNumber.Padding = new Padding(3);
            Control_PartIDManagement_Suggestion_EditNewPartNumber.ShowF4Button = false;
            Control_PartIDManagement_Suggestion_EditNewPartNumber.Size = new Size(394, 31);
            Control_PartIDManagement_Suggestion_EditNewPartNumber.TabIndex = 1;
            // 
            // Control_PartIDManagement_Panel_Home
            // 
            Control_PartIDManagement_Panel_Home.AutoSize = true;
            Control_PartIDManagement_Panel_Home.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_Panel_Home.Controls.Add(Control_PartIDManagement_TableLayout_Home);
            Control_PartIDManagement_Panel_Home.Dock = DockStyle.Fill;
            Control_PartIDManagement_Panel_Home.Location = new Point(0, 0);
            Control_PartIDManagement_Panel_Home.Name = "Control_PartIDManagement_Panel_Home";
            Control_PartIDManagement_Panel_Home.Size = new Size(446, 226);
            Control_PartIDManagement_Panel_Home.TabIndex = 0;
            // 
            // Control_PartIDManagement_TableLayout_Home
            // 
            Control_PartIDManagement_TableLayout_Home.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_TableLayout_Home.ColumnCount = 3;
            Control_PartIDManagement_TableLayout_Home.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            Control_PartIDManagement_TableLayout_Home.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            Control_PartIDManagement_TableLayout_Home.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
            Control_PartIDManagement_TableLayout_Home.Controls.Add(Control_PartIDManagement_Panel_HomeTile_Add, 0, 0);
            Control_PartIDManagement_TableLayout_Home.Controls.Add(Control_PartIDManagement_Panel_HomeTile_Edit, 1, 0);
            Control_PartIDManagement_TableLayout_Home.Controls.Add(Control_PartIDManagement_Panel_HomeTile_Remove, 2, 0);
            Control_PartIDManagement_TableLayout_Home.Dock = DockStyle.Fill;
            Control_PartIDManagement_TableLayout_Home.Location = new Point(0, 0);
            Control_PartIDManagement_TableLayout_Home.Name = "Control_PartIDManagement_TableLayout_Home";
            Control_PartIDManagement_TableLayout_Home.RowCount = 1;
            Control_PartIDManagement_TableLayout_Home.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_Home.Size = new Size(446, 226);
            Control_PartIDManagement_TableLayout_Home.TabIndex = 0;
            // 
            // Control_PartIDManagement_Panel_HomeTile_Add
            // 
            Control_PartIDManagement_Panel_HomeTile_Add.AutoSize = true;
            Control_PartIDManagement_Panel_HomeTile_Add.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_Panel_HomeTile_Add.BorderStyle = BorderStyle.FixedSingle;
            Control_PartIDManagement_Panel_HomeTile_Add.Controls.Add(Control_PartIDManagement_TableLayout_HomeTile_Add);
            Control_PartIDManagement_Panel_HomeTile_Add.Cursor = Cursors.Hand;
            Control_PartIDManagement_Panel_HomeTile_Add.Dock = DockStyle.Fill;
            Control_PartIDManagement_Panel_HomeTile_Add.Location = new Point(3, 3);
            Control_PartIDManagement_Panel_HomeTile_Add.Name = "Control_PartIDManagement_Panel_HomeTile_Add";
            Control_PartIDManagement_Panel_HomeTile_Add.Size = new Size(142, 220);
            Control_PartIDManagement_Panel_HomeTile_Add.TabIndex = 0;
            // 
            // Control_PartIDManagement_TableLayout_HomeTile_Add
            // 
            Control_PartIDManagement_TableLayout_HomeTile_Add.AutoSize = true;
            Control_PartIDManagement_TableLayout_HomeTile_Add.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_TableLayout_HomeTile_Add.ColumnCount = 1;
            Control_PartIDManagement_TableLayout_HomeTile_Add.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayout_HomeTile_Add.Controls.Add(Control_PartIDManagement_Label_HomeTile_AddInstruction, 0, 3);
            Control_PartIDManagement_TableLayout_HomeTile_Add.Controls.Add(Control_PartIDManagement_Label_HomeTile_AddTitle, 0, 1);
            Control_PartIDManagement_TableLayout_HomeTile_Add.Controls.Add(Control_PartIDManagement_Label_HomeTile_AddIcon, 0, 0);
            Control_PartIDManagement_TableLayout_HomeTile_Add.Dock = DockStyle.Fill;
            Control_PartIDManagement_TableLayout_HomeTile_Add.Location = new Point(0, 0);
            Control_PartIDManagement_TableLayout_HomeTile_Add.Name = "Control_PartIDManagement_TableLayout_HomeTile_Add";
            Control_PartIDManagement_TableLayout_HomeTile_Add.Padding = new Padding(3);
            Control_PartIDManagement_TableLayout_HomeTile_Add.RowCount = 5;
            Control_PartIDManagement_TableLayout_HomeTile_Add.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_HomeTile_Add.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_HomeTile_Add.RowStyles.Add(new RowStyle(SizeType.Percent, 49.9999962F));
            Control_PartIDManagement_TableLayout_HomeTile_Add.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_HomeTile_Add.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Control_PartIDManagement_TableLayout_HomeTile_Add.Size = new Size(140, 218);
            Control_PartIDManagement_TableLayout_HomeTile_Add.TabIndex = 0;
            // 
            // Control_PartIDManagement_Label_HomeTile_AddInstruction
            // 
            Control_PartIDManagement_Label_HomeTile_AddInstruction.AutoSize = true;
            Control_PartIDManagement_Label_HomeTile_AddInstruction.Dock = DockStyle.Fill;
            Control_PartIDManagement_Label_HomeTile_AddInstruction.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            Control_PartIDManagement_Label_HomeTile_AddInstruction.ForeColor = Color.FromArgb(100, 100, 100);
            Control_PartIDManagement_Label_HomeTile_AddInstruction.Location = new Point(6, 167);
            Control_PartIDManagement_Label_HomeTile_AddInstruction.Margin = new Padding(3);
            Control_PartIDManagement_Label_HomeTile_AddInstruction.Name = "Control_PartIDManagement_Label_HomeTile_AddInstruction";
            Control_PartIDManagement_Label_HomeTile_AddInstruction.Size = new Size(128, 30);
            Control_PartIDManagement_Label_HomeTile_AddInstruction.TabIndex = 2;
            Control_PartIDManagement_Label_HomeTile_AddInstruction.Text = "Click to add new part numbers";
            Control_PartIDManagement_Label_HomeTile_AddInstruction.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_PartIDManagement_Label_HomeTile_AddTitle
            // 
            Control_PartIDManagement_Label_HomeTile_AddTitle.AutoSize = true;
            Control_PartIDManagement_Label_HomeTile_AddTitle.Dock = DockStyle.Fill;
            Control_PartIDManagement_Label_HomeTile_AddTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            Control_PartIDManagement_Label_HomeTile_AddTitle.Location = new Point(6, 97);
            Control_PartIDManagement_Label_HomeTile_AddTitle.Margin = new Padding(3);
            Control_PartIDManagement_Label_HomeTile_AddTitle.Name = "Control_PartIDManagement_Label_HomeTile_AddTitle";
            Control_PartIDManagement_Label_HomeTile_AddTitle.Size = new Size(128, 50);
            Control_PartIDManagement_Label_HomeTile_AddTitle.TabIndex = 1;
            Control_PartIDManagement_Label_HomeTile_AddTitle.Text = "Add\r\nPart";
            Control_PartIDManagement_Label_HomeTile_AddTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_PartIDManagement_Label_HomeTile_AddIcon
            // 
            Control_PartIDManagement_Label_HomeTile_AddIcon.AutoSize = true;
            Control_PartIDManagement_Label_HomeTile_AddIcon.Dock = DockStyle.Fill;
            Control_PartIDManagement_Label_HomeTile_AddIcon.Font = new Font("Segoe UI Emoji", 48F);
            Control_PartIDManagement_Label_HomeTile_AddIcon.Location = new Point(6, 6);
            Control_PartIDManagement_Label_HomeTile_AddIcon.Margin = new Padding(3);
            Control_PartIDManagement_Label_HomeTile_AddIcon.Name = "Control_PartIDManagement_Label_HomeTile_AddIcon";
            Control_PartIDManagement_Label_HomeTile_AddIcon.Size = new Size(128, 85);
            Control_PartIDManagement_Label_HomeTile_AddIcon.TabIndex = 0;
            Control_PartIDManagement_Label_HomeTile_AddIcon.Text = "🆕";
            Control_PartIDManagement_Label_HomeTile_AddIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_PartIDManagement_Panel_HomeTile_Edit
            // 
            Control_PartIDManagement_Panel_HomeTile_Edit.AutoSize = true;
            Control_PartIDManagement_Panel_HomeTile_Edit.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_Panel_HomeTile_Edit.BorderStyle = BorderStyle.FixedSingle;
            Control_PartIDManagement_Panel_HomeTile_Edit.Controls.Add(Control_PartIDManagement_TableLayout_HomeTile_Edit);
            Control_PartIDManagement_Panel_HomeTile_Edit.Cursor = Cursors.Hand;
            Control_PartIDManagement_Panel_HomeTile_Edit.Dock = DockStyle.Fill;
            Control_PartIDManagement_Panel_HomeTile_Edit.Location = new Point(151, 3);
            Control_PartIDManagement_Panel_HomeTile_Edit.Name = "Control_PartIDManagement_Panel_HomeTile_Edit";
            Control_PartIDManagement_Panel_HomeTile_Edit.Size = new Size(142, 220);
            Control_PartIDManagement_Panel_HomeTile_Edit.TabIndex = 1;
            // 
            // Control_PartIDManagement_TableLayout_HomeTile_Edit
            // 
            Control_PartIDManagement_TableLayout_HomeTile_Edit.AutoSize = true;
            Control_PartIDManagement_TableLayout_HomeTile_Edit.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_TableLayout_HomeTile_Edit.ColumnCount = 1;
            Control_PartIDManagement_TableLayout_HomeTile_Edit.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayout_HomeTile_Edit.Controls.Add(Control_PartIDManagement_Label_HomeTile_EditIcon, 0, 0);
            Control_PartIDManagement_TableLayout_HomeTile_Edit.Controls.Add(Control_PartIDManagement_Label_HomeTile_EditTitle, 0, 1);
            Control_PartIDManagement_TableLayout_HomeTile_Edit.Controls.Add(Control_PartIDManagement_Label_HomeTile_EditInstruction, 0, 3);
            Control_PartIDManagement_TableLayout_HomeTile_Edit.Dock = DockStyle.Fill;
            Control_PartIDManagement_TableLayout_HomeTile_Edit.Location = new Point(0, 0);
            Control_PartIDManagement_TableLayout_HomeTile_Edit.Name = "Control_PartIDManagement_TableLayout_HomeTile_Edit";
            Control_PartIDManagement_TableLayout_HomeTile_Edit.Padding = new Padding(3);
            Control_PartIDManagement_TableLayout_HomeTile_Edit.RowCount = 5;
            Control_PartIDManagement_TableLayout_HomeTile_Edit.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_HomeTile_Edit.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_HomeTile_Edit.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Control_PartIDManagement_TableLayout_HomeTile_Edit.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_HomeTile_Edit.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Control_PartIDManagement_TableLayout_HomeTile_Edit.Size = new Size(140, 218);
            Control_PartIDManagement_TableLayout_HomeTile_Edit.TabIndex = 0;
            // 
            // Control_PartIDManagement_Label_HomeTile_EditIcon
            // 
            Control_PartIDManagement_Label_HomeTile_EditIcon.AutoSize = true;
            Control_PartIDManagement_Label_HomeTile_EditIcon.Dock = DockStyle.Fill;
            Control_PartIDManagement_Label_HomeTile_EditIcon.Font = new Font("Segoe UI Emoji", 48F);
            Control_PartIDManagement_Label_HomeTile_EditIcon.Location = new Point(6, 6);
            Control_PartIDManagement_Label_HomeTile_EditIcon.Margin = new Padding(3);
            Control_PartIDManagement_Label_HomeTile_EditIcon.Name = "Control_PartIDManagement_Label_HomeTile_EditIcon";
            Control_PartIDManagement_Label_HomeTile_EditIcon.Size = new Size(128, 85);
            Control_PartIDManagement_Label_HomeTile_EditIcon.TabIndex = 0;
            Control_PartIDManagement_Label_HomeTile_EditIcon.Text = "✏️";
            Control_PartIDManagement_Label_HomeTile_EditIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_PartIDManagement_Label_HomeTile_EditTitle
            // 
            Control_PartIDManagement_Label_HomeTile_EditTitle.AutoSize = true;
            Control_PartIDManagement_Label_HomeTile_EditTitle.Dock = DockStyle.Fill;
            Control_PartIDManagement_Label_HomeTile_EditTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            Control_PartIDManagement_Label_HomeTile_EditTitle.Location = new Point(6, 97);
            Control_PartIDManagement_Label_HomeTile_EditTitle.Margin = new Padding(3);
            Control_PartIDManagement_Label_HomeTile_EditTitle.Name = "Control_PartIDManagement_Label_HomeTile_EditTitle";
            Control_PartIDManagement_Label_HomeTile_EditTitle.Size = new Size(128, 50);
            Control_PartIDManagement_Label_HomeTile_EditTitle.TabIndex = 1;
            Control_PartIDManagement_Label_HomeTile_EditTitle.Text = "Edit\r\nPart";
            Control_PartIDManagement_Label_HomeTile_EditTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_PartIDManagement_Label_HomeTile_EditInstruction
            // 
            Control_PartIDManagement_Label_HomeTile_EditInstruction.AutoSize = true;
            Control_PartIDManagement_Label_HomeTile_EditInstruction.Dock = DockStyle.Fill;
            Control_PartIDManagement_Label_HomeTile_EditInstruction.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            Control_PartIDManagement_Label_HomeTile_EditInstruction.ForeColor = Color.FromArgb(100, 100, 100);
            Control_PartIDManagement_Label_HomeTile_EditInstruction.Location = new Point(6, 167);
            Control_PartIDManagement_Label_HomeTile_EditInstruction.Margin = new Padding(3);
            Control_PartIDManagement_Label_HomeTile_EditInstruction.Name = "Control_PartIDManagement_Label_HomeTile_EditInstruction";
            Control_PartIDManagement_Label_HomeTile_EditInstruction.Size = new Size(128, 30);
            Control_PartIDManagement_Label_HomeTile_EditInstruction.TabIndex = 2;
            Control_PartIDManagement_Label_HomeTile_EditInstruction.Text = "Click to edit existing parts";
            Control_PartIDManagement_Label_HomeTile_EditInstruction.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_PartIDManagement_Panel_HomeTile_Remove
            // 
            Control_PartIDManagement_Panel_HomeTile_Remove.AutoSize = true;
            Control_PartIDManagement_Panel_HomeTile_Remove.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_Panel_HomeTile_Remove.BorderStyle = BorderStyle.FixedSingle;
            Control_PartIDManagement_Panel_HomeTile_Remove.Controls.Add(Control_PartIDManagement_TableLayout_HomeTile_Remove);
            Control_PartIDManagement_Panel_HomeTile_Remove.Cursor = Cursors.Hand;
            Control_PartIDManagement_Panel_HomeTile_Remove.Dock = DockStyle.Fill;
            Control_PartIDManagement_Panel_HomeTile_Remove.Location = new Point(299, 3);
            Control_PartIDManagement_Panel_HomeTile_Remove.Name = "Control_PartIDManagement_Panel_HomeTile_Remove";
            Control_PartIDManagement_Panel_HomeTile_Remove.Size = new Size(144, 220);
            Control_PartIDManagement_Panel_HomeTile_Remove.TabIndex = 2;
            // 
            // Control_PartIDManagement_TableLayout_HomeTile_Remove
            // 
            Control_PartIDManagement_TableLayout_HomeTile_Remove.AutoSize = true;
            Control_PartIDManagement_TableLayout_HomeTile_Remove.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_TableLayout_HomeTile_Remove.ColumnCount = 1;
            Control_PartIDManagement_TableLayout_HomeTile_Remove.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayout_HomeTile_Remove.Controls.Add(Control_PartIDManagement_Label_HomeTile_RemoveIcon, 0, 0);
            Control_PartIDManagement_TableLayout_HomeTile_Remove.Controls.Add(Control_PartIDManagement_Label_HomeTile_RemoveInstruction, 0, 3);
            Control_PartIDManagement_TableLayout_HomeTile_Remove.Controls.Add(Control_PartIDManagement_Label_HomeTile_RemoveTitle, 0, 1);
            Control_PartIDManagement_TableLayout_HomeTile_Remove.Dock = DockStyle.Fill;
            Control_PartIDManagement_TableLayout_HomeTile_Remove.Location = new Point(0, 0);
            Control_PartIDManagement_TableLayout_HomeTile_Remove.Name = "Control_PartIDManagement_TableLayout_HomeTile_Remove";
            Control_PartIDManagement_TableLayout_HomeTile_Remove.Padding = new Padding(3);
            Control_PartIDManagement_TableLayout_HomeTile_Remove.RowCount = 5;
            Control_PartIDManagement_TableLayout_HomeTile_Remove.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_HomeTile_Remove.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_HomeTile_Remove.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Control_PartIDManagement_TableLayout_HomeTile_Remove.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_HomeTile_Remove.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Control_PartIDManagement_TableLayout_HomeTile_Remove.Size = new Size(142, 218);
            Control_PartIDManagement_TableLayout_HomeTile_Remove.TabIndex = 0;
            // 
            // Control_PartIDManagement_Label_HomeTile_RemoveIcon
            // 
            Control_PartIDManagement_Label_HomeTile_RemoveIcon.AutoSize = true;
            Control_PartIDManagement_Label_HomeTile_RemoveIcon.Dock = DockStyle.Fill;
            Control_PartIDManagement_Label_HomeTile_RemoveIcon.Font = new Font("Segoe UI Emoji", 48F);
            Control_PartIDManagement_Label_HomeTile_RemoveIcon.Location = new Point(6, 6);
            Control_PartIDManagement_Label_HomeTile_RemoveIcon.Margin = new Padding(3);
            Control_PartIDManagement_Label_HomeTile_RemoveIcon.Name = "Control_PartIDManagement_Label_HomeTile_RemoveIcon";
            Control_PartIDManagement_Label_HomeTile_RemoveIcon.Size = new Size(130, 85);
            Control_PartIDManagement_Label_HomeTile_RemoveIcon.TabIndex = 0;
            Control_PartIDManagement_Label_HomeTile_RemoveIcon.Text = "🗑️";
            Control_PartIDManagement_Label_HomeTile_RemoveIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_PartIDManagement_Label_HomeTile_RemoveInstruction
            // 
            Control_PartIDManagement_Label_HomeTile_RemoveInstruction.AutoSize = true;
            Control_PartIDManagement_Label_HomeTile_RemoveInstruction.Dock = DockStyle.Fill;
            Control_PartIDManagement_Label_HomeTile_RemoveInstruction.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            Control_PartIDManagement_Label_HomeTile_RemoveInstruction.ForeColor = Color.FromArgb(100, 100, 100);
            Control_PartIDManagement_Label_HomeTile_RemoveInstruction.Location = new Point(6, 167);
            Control_PartIDManagement_Label_HomeTile_RemoveInstruction.Margin = new Padding(3);
            Control_PartIDManagement_Label_HomeTile_RemoveInstruction.Name = "Control_PartIDManagement_Label_HomeTile_RemoveInstruction";
            Control_PartIDManagement_Label_HomeTile_RemoveInstruction.Size = new Size(130, 30);
            Control_PartIDManagement_Label_HomeTile_RemoveInstruction.TabIndex = 2;
            Control_PartIDManagement_Label_HomeTile_RemoveInstruction.Text = "Click to remove part numbers";
            Control_PartIDManagement_Label_HomeTile_RemoveInstruction.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_PartIDManagement_Label_HomeTile_RemoveTitle
            // 
            Control_PartIDManagement_Label_HomeTile_RemoveTitle.AutoSize = true;
            Control_PartIDManagement_Label_HomeTile_RemoveTitle.Dock = DockStyle.Fill;
            Control_PartIDManagement_Label_HomeTile_RemoveTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            Control_PartIDManagement_Label_HomeTile_RemoveTitle.Location = new Point(6, 97);
            Control_PartIDManagement_Label_HomeTile_RemoveTitle.Margin = new Padding(3);
            Control_PartIDManagement_Label_HomeTile_RemoveTitle.Name = "Control_PartIDManagement_Label_HomeTile_RemoveTitle";
            Control_PartIDManagement_Label_HomeTile_RemoveTitle.Size = new Size(130, 50);
            Control_PartIDManagement_Label_HomeTile_RemoveTitle.TabIndex = 1;
            Control_PartIDManagement_Label_HomeTile_RemoveTitle.Text = "Remove\r\nPart";
            Control_PartIDManagement_Label_HomeTile_RemoveTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_PartIDManagement_TableLayoutPanel_BackButton
            // 
            Control_PartIDManagement_TableLayoutPanel_BackButton.AutoSize = true;
            Control_PartIDManagement_TableLayoutPanel_BackButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_TableLayoutPanel_BackButton.ColumnCount = 2;
            Control_PartIDManagement_TableLayoutPanel_BackButton.ColumnStyles.Add(new ColumnStyle());
            Control_PartIDManagement_TableLayoutPanel_BackButton.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayoutPanel_BackButton.Controls.Add(Control_PartIDManagement_Button_Back, 0, 0);
            Control_PartIDManagement_TableLayoutPanel_BackButton.Dock = DockStyle.Fill;
            Control_PartIDManagement_TableLayoutPanel_BackButton.Location = new Point(23, 323);
            Control_PartIDManagement_TableLayoutPanel_BackButton.Name = "Control_PartIDManagement_TableLayoutPanel_BackButton";
            Control_PartIDManagement_TableLayoutPanel_BackButton.RowCount = 1;
            Control_PartIDManagement_TableLayoutPanel_BackButton.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayoutPanel_BackButton.Size = new Size(446, 43);
            Control_PartIDManagement_TableLayoutPanel_BackButton.TabIndex = 3;
            Control_PartIDManagement_TableLayoutPanel_BackButton.Visible = false;
            // 
            // Control_PartIDManagement_Button_Back
            // 
            Control_PartIDManagement_Button_Back.AutoSize = true;
            Control_PartIDManagement_Button_Back.Location = new Point(3, 3);
            Control_PartIDManagement_Button_Back.Name = "Control_PartIDManagement_Button_Back";
            Control_PartIDManagement_Button_Back.Padding = new Padding(16, 6, 16, 6);
            Control_PartIDManagement_Button_Back.Size = new Size(152, 37);
            Control_PartIDManagement_Button_Back.TabIndex = 0;
            Control_PartIDManagement_Button_Back.Text = "← Back to Selection";
            // 
            // Control_PartIDManagement_Label_Header
            // 
            Control_PartIDManagement_Label_Header.AutoSize = true;
            Control_PartIDManagement_Label_Header.Dock = DockStyle.Fill;
            Control_PartIDManagement_Label_Header.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            Control_PartIDManagement_Label_Header.ForeColor = Color.FromArgb(45, 45, 45);
            Control_PartIDManagement_Label_Header.Location = new Point(23, 23);
            Control_PartIDManagement_Label_Header.Margin = new Padding(3);
            Control_PartIDManagement_Label_Header.Name = "Control_PartIDManagement_Label_Header";
            Control_PartIDManagement_Label_Header.Size = new Size(446, 37);
            Control_PartIDManagement_Label_Header.TabIndex = 0;
            Control_PartIDManagement_Label_Header.Text = "Part Numbers";
            Control_PartIDManagement_Label_Header.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_PartIDManagement_Label_Subtitle
            // 
            Control_PartIDManagement_Label_Subtitle.AutoSize = true;
            Control_PartIDManagement_Label_Subtitle.Dock = DockStyle.Fill;
            Control_PartIDManagement_Label_Subtitle.Font = new Font("Segoe UI", 10F);
            Control_PartIDManagement_Label_Subtitle.ForeColor = Color.FromArgb(90, 90, 90);
            Control_PartIDManagement_Label_Subtitle.Location = new Point(23, 66);
            Control_PartIDManagement_Label_Subtitle.Margin = new Padding(3);
            Control_PartIDManagement_Label_Subtitle.Name = "Control_PartIDManagement_Label_Subtitle";
            Control_PartIDManagement_Label_Subtitle.Size = new Size(446, 19);
            Control_PartIDManagement_Label_Subtitle.TabIndex = 1;
            Control_PartIDManagement_Label_Subtitle.Text = "Select an action below to manage part numbers.";
            Control_PartIDManagement_Label_Subtitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_PartIDManagement_Label_AddIssuedBy
            // 
            Control_PartIDManagement_Label_AddIssuedBy.Location = new Point(0, 0);
            Control_PartIDManagement_Label_AddIssuedBy.Name = "Control_PartIDManagement_Label_AddIssuedBy";
            Control_PartIDManagement_Label_AddIssuedBy.Size = new Size(100, 23);
            Control_PartIDManagement_Label_AddIssuedBy.TabIndex = 0;
            // 
            // Control_PartIDManagement_Label_AddIssuedByValue
            // 
            Control_PartIDManagement_Label_AddIssuedByValue.Location = new Point(0, 0);
            Control_PartIDManagement_Label_AddIssuedByValue.Name = "Control_PartIDManagement_Label_AddIssuedByValue";
            Control_PartIDManagement_Label_AddIssuedByValue.Size = new Size(100, 23);
            Control_PartIDManagement_Label_AddIssuedByValue.TabIndex = 0;
            // 
            // Control_PartIDManagement_Label_EditIssuedBy
            // 
            Control_PartIDManagement_Label_EditIssuedBy.Location = new Point(0, 0);
            Control_PartIDManagement_Label_EditIssuedBy.Name = "Control_PartIDManagement_Label_EditIssuedBy";
            Control_PartIDManagement_Label_EditIssuedBy.Size = new Size(100, 23);
            Control_PartIDManagement_Label_EditIssuedBy.TabIndex = 0;
            // 
            // Control_PartIDManagement_Label_EditIssuedByValue
            // 
            Control_PartIDManagement_Label_EditIssuedByValue.Location = new Point(0, 0);
            Control_PartIDManagement_Label_EditIssuedByValue.Name = "Control_PartIDManagement_Label_EditIssuedByValue";
            Control_PartIDManagement_Label_EditIssuedByValue.Size = new Size(100, 23);
            Control_PartIDManagement_Label_EditIssuedByValue.TabIndex = 0;
            // 
            // Control_PartIDManagement
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(Control_PartIDManagement_TableLayout_Main);
            Name = "Control_PartIDManagement";
            Size = new Size(492, 389);
            Control_PartIDManagement_TableLayout_Main.ResumeLayout(false);
            Control_PartIDManagement_TableLayout_Main.PerformLayout();
            Control_PartIDManagement_Panel_Container.ResumeLayout(false);
            Control_PartIDManagement_Panel_Container.PerformLayout();
            Control_PartIDManagement_TableLayout_Cards.ResumeLayout(false);
            Control_PartIDManagement_TableLayout_Cards.PerformLayout();
            Control_PartIDManagement_Panel_RemoveCard.ResumeLayout(false);
            Control_PartIDManagement_Panel_RemoveCard.PerformLayout();
            Control_PartIDManagement_TableLayout_Remove.ResumeLayout(false);
            Control_PartIDManagement_TableLayout_Remove.PerformLayout();
            Control_PartIDManagement_TableLayout_RemoveContent.ResumeLayout(false);
            Control_PartIDManagement_TableLayout_RemoveContent.PerformLayout();
            Control_PartIDManagement_TableLayout_RemoveActions.ResumeLayout(false);
            Control_PartIDManagement_TableLayout_RemoveDetails.ResumeLayout(false);
            Control_PartIDManagement_TableLayout_Title.ResumeLayout(false);
            Control_PartIDManagement_TableLayout_Title.PerformLayout();
            Control_PartIDManagement_Panel_AddCard.ResumeLayout(false);
            Control_PartIDManagement_Panel_AddCard.PerformLayout();
            Control_PartIDManagement_TableLayout_Add.ResumeLayout(false);
            Control_PartIDManagement_TableLayout_Add.PerformLayout();
            Control_PartIDManagement_TableLayout_AddContent.ResumeLayout(false);
            Control_PartIDManagement_TableLayout_AddContent.PerformLayout();
            Control_PartIDManagement_TableLayout_AddActions.ResumeLayout(false);
            Control_PartIDManagement_Add_TableLayout_Title.ResumeLayout(false);
            Control_PartIDManagement_Add_TableLayout_Title.PerformLayout();
            Control_PartIDManagement_Panel_EditCard.ResumeLayout(false);
            Control_PartIDManagement_Panel_EditCard.PerformLayout();
            Control_PartIDManagement_TableLayout_Edit.ResumeLayout(false);
            Control_PartIDManagement_TableLayout_Edit.PerformLayout();
            Control_PartIDManagement_TableLayoutPanel_Title.ResumeLayout(false);
            Control_PartIDManagement_TableLayoutPanel_Title.PerformLayout();
            Control_PartIDManagement_TableLayout_EditContent.ResumeLayout(false);
            Control_PartIDManagement_TableLayout_EditContent.PerformLayout();
            Control_PartIDManagement_TableLayoutPanel_EditActions.ResumeLayout(false);
            Control_PartIDManagement_Panel_Home.ResumeLayout(false);
            Control_PartIDManagement_TableLayout_Home.ResumeLayout(false);
            Control_PartIDManagement_TableLayout_Home.PerformLayout();
            Control_PartIDManagement_Panel_HomeTile_Add.ResumeLayout(false);
            Control_PartIDManagement_Panel_HomeTile_Add.PerformLayout();
            Control_PartIDManagement_TableLayout_HomeTile_Add.ResumeLayout(false);
            Control_PartIDManagement_TableLayout_HomeTile_Add.PerformLayout();
            Control_PartIDManagement_Panel_HomeTile_Edit.ResumeLayout(false);
            Control_PartIDManagement_Panel_HomeTile_Edit.PerformLayout();
            Control_PartIDManagement_TableLayout_HomeTile_Edit.ResumeLayout(false);
            Control_PartIDManagement_TableLayout_HomeTile_Edit.PerformLayout();
            Control_PartIDManagement_Panel_HomeTile_Remove.ResumeLayout(false);
            Control_PartIDManagement_Panel_HomeTile_Remove.PerformLayout();
            Control_PartIDManagement_TableLayout_HomeTile_Remove.ResumeLayout(false);
            Control_PartIDManagement_TableLayout_HomeTile_Remove.PerformLayout();
            Control_PartIDManagement_TableLayoutPanel_BackButton.ResumeLayout(false);
            Control_PartIDManagement_TableLayoutPanel_BackButton.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TableLayoutPanel Control_PartIDManagement_TableLayout_AddContent;
        private TableLayoutPanel Control_PartIDManagement_Add_TableLayout_Title;
        private TableLayoutPanel Control_PartIDManagement_TableLayout_AddActions;
        private TableLayoutPanel Control_PartIDManagement_TableLayoutPanel_Title;
        private TableLayoutPanel Control_PartIDManagement_TableLayoutPanel_EditActions;
        private TableLayoutPanel Control_PartIDManagement_TableLayout_RemoveActions;
        private TableLayoutPanel Control_PartIDManagement_TableLayout_Title;
        private TableLayoutPanel Control_PartIDManagement_TableLayoutPanel_BackButton;
        private Button Control_PartIDManagement_Button_Back;
    }
}
