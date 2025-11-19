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
        private Panel Control_PartIDManagement_Panel_HomeTile_Edit = null!;
        private Panel Control_PartIDManagement_Panel_HomeTile_Remove = null!;
        
        private TableLayoutPanel Control_PartIDManagement_TableLayout_Cards = null!;
        private FlowLayoutPanel Control_PartIDManagement_FlowPanel_BackButton = null!;
        private Button Control_PartIDManagement_Button_Back = null!;

        private Panel Control_PartIDManagement_Panel_AddCard = null!;
        private TableLayoutPanel Control_PartIDManagement_TableLayout_Add = null!;
        private Label Control_PartIDManagement_Label_AddIcon = null!;
        private Label Control_PartIDManagement_Label_AddTitle = null!;
        private TableLayoutPanel Control_PartIDManagement_TableLayout_AddContent = null!;
        private SuggestionTextBoxWithLabel Control_PartIDManagement_Suggestion_AddPartNumber = null!;
        private SuggestionTextBoxWithLabel Control_PartIDManagement_Suggestion_AddItemType = null!;
        private Label Control_PartIDManagement_Label_AddIssuedBy = null!;
        private Label Control_PartIDManagement_Label_AddIssuedByValue = null!;
        private CheckBox Control_PartIDManagement_CheckBox_AddRequiresColorCode = null!;
        private FlowLayoutPanel Control_PartIDManagement_FlowPanel_AddActions = null!;
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
        private FlowLayoutPanel Control_PartIDManagement_FlowPanel_EditActions = null!;
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
        private FlowLayoutPanel Control_PartIDManagement_FlowPanel_RemoveActions = null!;
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
            components = new Container();
            Control_PartIDManagement_TableLayout_Main = new TableLayoutPanel();
            Control_PartIDManagement_Label_Header = new Label();
            Control_PartIDManagement_Label_Subtitle = new Label();
            Control_PartIDManagement_Panel_Container = new Panel();
            
            Control_PartIDManagement_Panel_Home = new Panel();
            Control_PartIDManagement_TableLayout_Home = new TableLayoutPanel();
            Control_PartIDManagement_Panel_HomeTile_Add = new Panel();
            Control_PartIDManagement_Panel_HomeTile_Edit = new Panel();
            Control_PartIDManagement_Panel_HomeTile_Remove = new Panel();
            
            Control_PartIDManagement_TableLayout_Cards = new TableLayoutPanel();
            Control_PartIDManagement_FlowPanel_BackButton = new FlowLayoutPanel();
            Control_PartIDManagement_Button_Back = new Button();
            Control_PartIDManagement_Panel_AddCard = new Panel();
            Control_PartIDManagement_TableLayout_Add = new TableLayoutPanel();
            Control_PartIDManagement_Label_AddIcon = new Label();
            Control_PartIDManagement_Label_AddTitle = new Label();
            Control_PartIDManagement_TableLayout_AddContent = new TableLayoutPanel();
            Control_PartIDManagement_Suggestion_AddPartNumber = new SuggestionTextBoxWithLabel();
            Control_PartIDManagement_Suggestion_AddItemType = new SuggestionTextBoxWithLabel();
            Control_PartIDManagement_Label_AddIssuedBy = new Label();
            Control_PartIDManagement_Label_AddIssuedByValue = new Label();
            Control_PartIDManagement_CheckBox_AddRequiresColorCode = new CheckBox();
            Control_PartIDManagement_FlowPanel_AddActions = new FlowLayoutPanel();
            Control_PartIDManagement_Button_AddSave = new Button();
            Control_PartIDManagement_Button_AddClear = new Button();
            Control_PartIDManagement_Panel_EditCard = new Panel();
            Control_PartIDManagement_TableLayout_Edit = new TableLayoutPanel();
            Control_PartIDManagement_Label_EditIcon = new Label();
            Control_PartIDManagement_Label_EditTitle = new Label();
            Control_PartIDManagement_TableLayout_EditContent = new TableLayoutPanel();
            Control_PartIDManagement_Suggestion_EditSelectPart = new SuggestionTextBoxWithLabel();
            Control_PartIDManagement_Suggestion_EditNewPartNumber = new SuggestionTextBoxWithLabel();
            Control_PartIDManagement_Suggestion_EditItemType = new SuggestionTextBoxWithLabel();
            Control_PartIDManagement_Label_EditIssuedBy = new Label();
            Control_PartIDManagement_Label_EditIssuedByValue = new Label();
            Control_PartIDManagement_CheckBox_EditRequiresColorCode = new CheckBox();
            Control_PartIDManagement_FlowPanel_EditActions = new FlowLayoutPanel();
            Control_PartIDManagement_Button_EditSave = new Button();
            Control_PartIDManagement_Button_EditReset = new Button();
            Control_PartIDManagement_Panel_RemoveCard = new Panel();
            Control_PartIDManagement_TableLayout_Remove = new TableLayoutPanel();
            Control_PartIDManagement_Label_RemoveIcon = new Label();
            Control_PartIDManagement_Label_RemoveTitle = new Label();
            Control_PartIDManagement_TableLayout_RemoveContent = new TableLayoutPanel();
            Control_PartIDManagement_Suggestion_RemoveSelectPart = new SuggestionTextBoxWithLabel();
            Control_PartIDManagement_TableLayout_RemoveDetails = new TableLayoutPanel();
            Control_PartIDManagement_Label_RemoveItemNumber = new Label();
            Control_PartIDManagement_Label_RemoveItemNumberValue = new Label();
            Control_PartIDManagement_Label_RemoveCustomer = new Label();
            Control_PartIDManagement_Label_RemoveCustomerValue = new Label();
            Control_PartIDManagement_Label_RemoveDescription = new Label();
            Control_PartIDManagement_Label_RemoveDescriptionValue = new Label();
            Control_PartIDManagement_Label_RemoveType = new Label();
            Control_PartIDManagement_Label_RemoveTypeValue = new Label();
            Control_PartIDManagement_Label_RemoveIssuedBy = new Label();
            Control_PartIDManagement_Label_RemoveIssuedByValue = new Label();
            Control_PartIDManagement_Label_RemoveWarning = new Label();
            Control_PartIDManagement_FlowPanel_RemoveActions = new FlowLayoutPanel();
            Control_PartIDManagement_Button_RemoveConfirm = new Button();
            Control_PartIDManagement_Button_RemoveCancel = new Button();
            Control_PartIDManagement_TableLayout_Main.SuspendLayout();
            Control_PartIDManagement_Panel_Container.SuspendLayout();
            Control_PartIDManagement_Panel_Home.SuspendLayout();
            Control_PartIDManagement_TableLayout_Home.SuspendLayout();
            Control_PartIDManagement_TableLayout_Cards.SuspendLayout();
            Control_PartIDManagement_Panel_AddCard.SuspendLayout();
            Control_PartIDManagement_TableLayout_Add.SuspendLayout();
            Control_PartIDManagement_TableLayout_AddContent.SuspendLayout();
            Control_PartIDManagement_FlowPanel_AddActions.SuspendLayout();
            Control_PartIDManagement_Panel_EditCard.SuspendLayout();
            Control_PartIDManagement_TableLayout_Edit.SuspendLayout();
            Control_PartIDManagement_TableLayout_EditContent.SuspendLayout();
            Control_PartIDManagement_FlowPanel_EditActions.SuspendLayout();
            Control_PartIDManagement_Panel_RemoveCard.SuspendLayout();
            Control_PartIDManagement_TableLayout_Remove.SuspendLayout();
            Control_PartIDManagement_TableLayout_RemoveContent.SuspendLayout();
            Control_PartIDManagement_TableLayout_RemoveDetails.SuspendLayout();
            Control_PartIDManagement_FlowPanel_RemoveActions.SuspendLayout();
            SuspendLayout();

            // Control_PartIDManagement_TableLayout_Main
            Control_PartIDManagement_TableLayout_Main.AutoSize = true;
            Control_PartIDManagement_TableLayout_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_TableLayout_Main.ColumnCount = 1;
            Control_PartIDManagement_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayout_Main.Controls.Add(Control_PartIDManagement_Label_Header, 0, 0);
            Control_PartIDManagement_TableLayout_Main.Controls.Add(Control_PartIDManagement_Label_Subtitle, 0, 1);
            Control_PartIDManagement_TableLayout_Main.Controls.Add(Control_PartIDManagement_Panel_Container, 0, 2);
            Control_PartIDManagement_TableLayout_Main.Dock = DockStyle.Fill;
            Control_PartIDManagement_TableLayout_Main.Padding = new Padding(20);
            Control_PartIDManagement_TableLayout_Main.RowCount = 3;
            Control_PartIDManagement_TableLayout_Main.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_Main.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // Control_PartIDManagement_Label_Header
            Control_PartIDManagement_Label_Header.AutoSize = true;
            Control_PartIDManagement_Label_Header.Dock = DockStyle.Fill;
            Control_PartIDManagement_Label_Header.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point);
            Control_PartIDManagement_Label_Header.ForeColor = Color.FromArgb(45, 45, 45);
            Control_PartIDManagement_Label_Header.Text = "Part Numbers";

            // Control_PartIDManagement_Label_Subtitle
            Control_PartIDManagement_Label_Subtitle.AutoSize = true;
            Control_PartIDManagement_Label_Subtitle.Dock = DockStyle.Fill;
            Control_PartIDManagement_Label_Subtitle.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            Control_PartIDManagement_Label_Subtitle.ForeColor = Color.FromArgb(90, 90, 90);
            Control_PartIDManagement_Label_Subtitle.Margin = new Padding(3, 6, 3, 15);
            Control_PartIDManagement_Label_Subtitle.Text = "Select an action below to manage part numbers.";

            // Control_PartIDManagement_Panel_Container
            Control_PartIDManagement_Panel_Container.AutoSize = true;
            Control_PartIDManagement_Panel_Container.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_Panel_Container.Dock = DockStyle.Fill;
            Control_PartIDManagement_Panel_Container.Controls.Add(Control_PartIDManagement_Panel_Home);
            Control_PartIDManagement_Panel_Container.Controls.Add(Control_PartIDManagement_TableLayout_Cards);
            Control_PartIDManagement_Panel_Container.Controls.Add(Control_PartIDManagement_FlowPanel_BackButton);

            // Control_PartIDManagement_Panel_Home
            Control_PartIDManagement_Panel_Home.AutoSize = true;
            Control_PartIDManagement_Panel_Home.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_Panel_Home.Dock = DockStyle.Fill;
            Control_PartIDManagement_Panel_Home.Controls.Add(Control_PartIDManagement_TableLayout_Home);
            Control_PartIDManagement_Panel_Home.Visible = true;

            // Control_PartIDManagement_TableLayout_Home
            Control_PartIDManagement_TableLayout_Home.AutoSize = true;
            Control_PartIDManagement_TableLayout_Home.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_TableLayout_Home.ColumnCount = 3;
            Control_PartIDManagement_TableLayout_Home.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            Control_PartIDManagement_TableLayout_Home.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            Control_PartIDManagement_TableLayout_Home.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
            Control_PartIDManagement_TableLayout_Home.Controls.Add(Control_PartIDManagement_Panel_HomeTile_Add, 0, 0);
            Control_PartIDManagement_TableLayout_Home.Controls.Add(Control_PartIDManagement_Panel_HomeTile_Edit, 1, 0);
            Control_PartIDManagement_TableLayout_Home.Controls.Add(Control_PartIDManagement_Panel_HomeTile_Remove, 2, 0);
            Control_PartIDManagement_TableLayout_Home.Dock = DockStyle.Fill;
            Control_PartIDManagement_TableLayout_Home.RowCount = 1;
            Control_PartIDManagement_TableLayout_Home.RowStyles.Add(new RowStyle());

            ConfigureHomeTile(Control_PartIDManagement_Panel_HomeTile_Add, "🆕", "Add Part", "Create new part numbers", Color.FromArgb(16, 124, 16), 0);
            ConfigureHomeTile(Control_PartIDManagement_Panel_HomeTile_Edit, "✏️", "Edit Part", "Modify existing parts", Color.FromArgb(0, 120, 215), 1);
            ConfigureHomeTile(Control_PartIDManagement_Panel_HomeTile_Remove, "🗑️", "Remove Part", "Delete part numbers", Color.FromArgb(194, 57, 52), 2);

            // Control_PartIDManagement_FlowPanel_BackButton
            Control_PartIDManagement_FlowPanel_BackButton.AutoSize = true;
            Control_PartIDManagement_FlowPanel_BackButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_FlowPanel_BackButton.Dock = DockStyle.Bottom;
            Control_PartIDManagement_FlowPanel_BackButton.FlowDirection = FlowDirection.LeftToRight;
            Control_PartIDManagement_FlowPanel_BackButton.Padding = new Padding(0, 12, 0, 0);
            Control_PartIDManagement_FlowPanel_BackButton.Controls.Add(Control_PartIDManagement_Button_Back);
            Control_PartIDManagement_FlowPanel_BackButton.Visible = false;

            Control_PartIDManagement_Button_Back.AutoSize = true;
            Control_PartIDManagement_Button_Back.Text = "← Back to Selection";
            Control_PartIDManagement_Button_Back.Padding = new Padding(16, 6, 16, 6);
            ConfigureSecondaryButton(Control_PartIDManagement_Button_Back, "← Back to Selection");

            // Control_PartIDManagement_TableLayout_Cards
            Control_PartIDManagement_TableLayout_Cards.AutoSize = true;
            Control_PartIDManagement_TableLayout_Cards.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_TableLayout_Cards.ColumnCount = 1;
            Control_PartIDManagement_TableLayout_Cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayout_Cards.Controls.Add(Control_PartIDManagement_Panel_AddCard, 0, 0);
            Control_PartIDManagement_TableLayout_Cards.Controls.Add(Control_PartIDManagement_Panel_EditCard, 0, 1);
            Control_PartIDManagement_TableLayout_Cards.Controls.Add(Control_PartIDManagement_Panel_RemoveCard, 0, 2);
            Control_PartIDManagement_TableLayout_Cards.Dock = DockStyle.Fill;
            Control_PartIDManagement_TableLayout_Cards.Visible = false;
            Control_PartIDManagement_TableLayout_Cards.RowCount = 3;
            Control_PartIDManagement_TableLayout_Cards.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_Cards.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_Cards.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_Cards.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;

            // Add Card Panel
            ConfigureCardPanel(Control_PartIDManagement_Panel_AddCard, Color.FromArgb(16, 124, 16));
            Control_PartIDManagement_TableLayout_Add.ColumnCount = 2;
            Control_PartIDManagement_TableLayout_Add.ColumnStyles.Add(new ColumnStyle());
            Control_PartIDManagement_TableLayout_Add.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayout_Add.Controls.Add(Control_PartIDManagement_Label_AddIcon, 0, 0);
            Control_PartIDManagement_TableLayout_Add.Controls.Add(Control_PartIDManagement_Label_AddTitle, 1, 0);
            Control_PartIDManagement_TableLayout_Add.Controls.Add(Control_PartIDManagement_TableLayout_AddContent, 0, 1);
            Control_PartIDManagement_TableLayout_Add.SetColumnSpan(Control_PartIDManagement_TableLayout_AddContent, 2);
            Control_PartIDManagement_TableLayout_Add.AutoSize = true;
            Control_PartIDManagement_TableLayout_Add.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_TableLayout_Add.Dock = DockStyle.Fill;
            Control_PartIDManagement_TableLayout_Add.RowCount = 2;
            Control_PartIDManagement_TableLayout_Add.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_Add.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_Add.Padding = new Padding(16);

            Control_PartIDManagement_Label_AddIcon.AutoSize = true;
            Control_PartIDManagement_Label_AddIcon.Font = new Font("Segoe UI Emoji", 28F, FontStyle.Regular, GraphicsUnit.Point);
            Control_PartIDManagement_Label_AddIcon.Text = "🆕";
            Control_PartIDManagement_Label_AddIcon.Margin = new Padding(0, 0, 12, 0);
            Control_PartIDManagement_Label_AddIcon.TextAlign = ContentAlignment.MiddleCenter;

            Control_PartIDManagement_Label_AddTitle.AutoSize = true;
            Control_PartIDManagement_Label_AddTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            Control_PartIDManagement_Label_AddTitle.Text = "Add Part";
            Control_PartIDManagement_Label_AddTitle.TextAlign = ContentAlignment.MiddleLeft;

            Control_PartIDManagement_TableLayout_AddContent.AutoSize = true;
            Control_PartIDManagement_TableLayout_AddContent.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_TableLayout_AddContent.ColumnCount = 1;
            Control_PartIDManagement_TableLayout_AddContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayout_AddContent.Controls.Add(Control_PartIDManagement_Suggestion_AddPartNumber, 0, 0);
            Control_PartIDManagement_TableLayout_AddContent.Controls.Add(Control_PartIDManagement_Suggestion_AddItemType, 0, 1);
            Control_PartIDManagement_TableLayout_AddContent.Controls.Add(CreateKeyValueRow(Control_PartIDManagement_Label_AddIssuedBy, Control_PartIDManagement_Label_AddIssuedByValue), 0, 2);
            Control_PartIDManagement_TableLayout_AddContent.Controls.Add(Control_PartIDManagement_CheckBox_AddRequiresColorCode, 0, 3);
            Control_PartIDManagement_TableLayout_AddContent.Controls.Add(Control_PartIDManagement_FlowPanel_AddActions, 0, 4);
            Control_PartIDManagement_TableLayout_AddContent.Dock = DockStyle.Top;
            Control_PartIDManagement_TableLayout_AddContent.RowCount = 5;
            Control_PartIDManagement_TableLayout_AddContent.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_AddContent.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_AddContent.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_AddContent.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_AddContent.RowStyles.Add(new RowStyle());

            ConfigureSuggestion(Control_PartIDManagement_Suggestion_AddPartNumber, "Part Number", "Enter part number (F4 optional)");
            Control_PartIDManagement_Suggestion_AddPartNumber.EnableSuggestions = false;
            Control_PartIDManagement_Suggestion_AddPartNumber.ShowValidationColor = true;

            ConfigureSuggestion(Control_PartIDManagement_Suggestion_AddItemType, "Item Type", "Search item types (F4)");

            ConfigureKeyValueLabel(Control_PartIDManagement_Label_AddIssuedBy, "Issued By");
            ConfigureKeyValueValue(Control_PartIDManagement_Label_AddIssuedByValue);

            Control_PartIDManagement_CheckBox_AddRequiresColorCode.AutoSize = true;
            Control_PartIDManagement_CheckBox_AddRequiresColorCode.Text = "Requires color code and work order";

            ConfigureActionPanel(Control_PartIDManagement_FlowPanel_AddActions);
            ConfigurePrimaryButton(Control_PartIDManagement_Button_AddSave, "Save");
            ConfigureSecondaryButton(Control_PartIDManagement_Button_AddClear, "Clear");
            Control_PartIDManagement_FlowPanel_AddActions.Controls.Add(Control_PartIDManagement_Button_AddClear);
            Control_PartIDManagement_FlowPanel_AddActions.Controls.Add(Control_PartIDManagement_Button_AddSave);

            Control_PartIDManagement_Panel_AddCard.Controls.Add(Control_PartIDManagement_TableLayout_Add);

            // Edit Card Panel
            ConfigureCardPanel(Control_PartIDManagement_Panel_EditCard, Color.FromArgb(0, 120, 215));
            Control_PartIDManagement_TableLayout_Edit.ColumnCount = 2;
            Control_PartIDManagement_TableLayout_Edit.ColumnStyles.Add(new ColumnStyle());
            Control_PartIDManagement_TableLayout_Edit.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayout_Edit.Controls.Add(Control_PartIDManagement_Label_EditIcon, 0, 0);
            Control_PartIDManagement_TableLayout_Edit.Controls.Add(Control_PartIDManagement_Label_EditTitle, 1, 0);
            Control_PartIDManagement_TableLayout_Edit.Controls.Add(Control_PartIDManagement_TableLayout_EditContent, 0, 1);
            Control_PartIDManagement_TableLayout_Edit.SetColumnSpan(Control_PartIDManagement_TableLayout_EditContent, 2);
            Control_PartIDManagement_TableLayout_Edit.AutoSize = true;
            Control_PartIDManagement_TableLayout_Edit.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_TableLayout_Edit.Dock = DockStyle.Fill;
            Control_PartIDManagement_TableLayout_Edit.RowCount = 2;
            Control_PartIDManagement_TableLayout_Edit.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_Edit.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_Edit.Padding = new Padding(16);

            Control_PartIDManagement_Label_EditIcon.AutoSize = true;
            Control_PartIDManagement_Label_EditIcon.Font = new Font("Segoe UI Emoji", 28F);
            Control_PartIDManagement_Label_EditIcon.Text = "✏️";
            Control_PartIDManagement_Label_EditIcon.Margin = new Padding(0, 0, 12, 0);

            Control_PartIDManagement_Label_EditTitle.AutoSize = true;
            Control_PartIDManagement_Label_EditTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            Control_PartIDManagement_Label_EditTitle.Text = "Edit Part";

            Control_PartIDManagement_TableLayout_EditContent.AutoSize = true;
            Control_PartIDManagement_TableLayout_EditContent.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_TableLayout_EditContent.ColumnCount = 1;
            Control_PartIDManagement_TableLayout_EditContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayout_EditContent.Controls.Add(Control_PartIDManagement_Suggestion_EditSelectPart, 0, 0);
            Control_PartIDManagement_TableLayout_EditContent.Controls.Add(Control_PartIDManagement_Suggestion_EditNewPartNumber, 0, 1);
            Control_PartIDManagement_TableLayout_EditContent.Controls.Add(Control_PartIDManagement_Suggestion_EditItemType, 0, 2);
            Control_PartIDManagement_TableLayout_EditContent.Controls.Add(CreateKeyValueRow(Control_PartIDManagement_Label_EditIssuedBy, Control_PartIDManagement_Label_EditIssuedByValue), 0, 3);
            Control_PartIDManagement_TableLayout_EditContent.Controls.Add(Control_PartIDManagement_CheckBox_EditRequiresColorCode, 0, 4);
            Control_PartIDManagement_TableLayout_EditContent.Controls.Add(Control_PartIDManagement_FlowPanel_EditActions, 0, 5);
            Control_PartIDManagement_TableLayout_EditContent.Dock = DockStyle.Top;
            Control_PartIDManagement_TableLayout_EditContent.RowCount = 6;
            Control_PartIDManagement_TableLayout_EditContent.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_EditContent.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_EditContent.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_EditContent.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_EditContent.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_EditContent.RowStyles.Add(new RowStyle());

            ConfigureSuggestion(Control_PartIDManagement_Suggestion_EditSelectPart, "Select Part", "Search parts (F4)");
            ConfigureSuggestion(Control_PartIDManagement_Suggestion_EditNewPartNumber, "New Part Number", "Enter new value");
            Control_PartIDManagement_Suggestion_EditNewPartNumber.EnableSuggestions = false;
            ConfigureSuggestion(Control_PartIDManagement_Suggestion_EditItemType, "Item Type", "Search item types (F4)");

            ConfigureKeyValueLabel(Control_PartIDManagement_Label_EditIssuedBy, "Issued By");
            ConfigureKeyValueValue(Control_PartIDManagement_Label_EditIssuedByValue);

            Control_PartIDManagement_CheckBox_EditRequiresColorCode.AutoSize = true;
            Control_PartIDManagement_CheckBox_EditRequiresColorCode.Text = "Requires color code and work order";

            ConfigureActionPanel(Control_PartIDManagement_FlowPanel_EditActions);
            ConfigurePrimaryButton(Control_PartIDManagement_Button_EditSave, "Save Changes");
            ConfigureSecondaryButton(Control_PartIDManagement_Button_EditReset, "Reset");
            Control_PartIDManagement_FlowPanel_EditActions.Controls.Add(Control_PartIDManagement_Button_EditReset);
            Control_PartIDManagement_FlowPanel_EditActions.Controls.Add(Control_PartIDManagement_Button_EditSave);

            Control_PartIDManagement_Panel_EditCard.Controls.Add(Control_PartIDManagement_TableLayout_Edit);

            // Remove Card Panel
            ConfigureCardPanel(Control_PartIDManagement_Panel_RemoveCard, Color.FromArgb(194, 57, 52));
            Control_PartIDManagement_TableLayout_Remove.ColumnCount = 2;
            Control_PartIDManagement_TableLayout_Remove.ColumnStyles.Add(new ColumnStyle());
            Control_PartIDManagement_TableLayout_Remove.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayout_Remove.Controls.Add(Control_PartIDManagement_Label_RemoveIcon, 0, 0);
            Control_PartIDManagement_TableLayout_Remove.Controls.Add(Control_PartIDManagement_Label_RemoveTitle, 1, 0);
            Control_PartIDManagement_TableLayout_Remove.Controls.Add(Control_PartIDManagement_TableLayout_RemoveContent, 0, 1);
            Control_PartIDManagement_TableLayout_Remove.SetColumnSpan(Control_PartIDManagement_TableLayout_RemoveContent, 2);
            Control_PartIDManagement_TableLayout_Remove.AutoSize = true;
            Control_PartIDManagement_TableLayout_Remove.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_TableLayout_Remove.Dock = DockStyle.Fill;
            Control_PartIDManagement_TableLayout_Remove.RowCount = 2;
            Control_PartIDManagement_TableLayout_Remove.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_Remove.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_Remove.Padding = new Padding(16);

            Control_PartIDManagement_Label_RemoveIcon.AutoSize = true;
            Control_PartIDManagement_Label_RemoveIcon.Font = new Font("Segoe UI Emoji", 28F);
            Control_PartIDManagement_Label_RemoveIcon.Text = "🗑️";
            Control_PartIDManagement_Label_RemoveIcon.Margin = new Padding(0, 0, 12, 0);

            Control_PartIDManagement_Label_RemoveTitle.AutoSize = true;
            Control_PartIDManagement_Label_RemoveTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            Control_PartIDManagement_Label_RemoveTitle.Text = "Remove Part";

            Control_PartIDManagement_TableLayout_RemoveContent.AutoSize = true;
            Control_PartIDManagement_TableLayout_RemoveContent.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_TableLayout_RemoveContent.ColumnCount = 1;
            Control_PartIDManagement_TableLayout_RemoveContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_PartIDManagement_TableLayout_RemoveContent.Controls.Add(Control_PartIDManagement_Suggestion_RemoveSelectPart, 0, 0);
            Control_PartIDManagement_TableLayout_RemoveContent.Controls.Add(Control_PartIDManagement_TableLayout_RemoveDetails, 0, 1);
            Control_PartIDManagement_TableLayout_RemoveContent.Controls.Add(Control_PartIDManagement_Label_RemoveWarning, 0, 2);
            Control_PartIDManagement_TableLayout_RemoveContent.Controls.Add(Control_PartIDManagement_FlowPanel_RemoveActions, 0, 3);
            Control_PartIDManagement_TableLayout_RemoveContent.Dock = DockStyle.Top;
            Control_PartIDManagement_TableLayout_RemoveContent.RowCount = 4;
            Control_PartIDManagement_TableLayout_RemoveContent.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_RemoveContent.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_RemoveContent.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_RemoveContent.RowStyles.Add(new RowStyle());

            ConfigureSuggestion(Control_PartIDManagement_Suggestion_RemoveSelectPart, "Select Part", "Search parts (F4)");

            Control_PartIDManagement_TableLayout_RemoveDetails.AutoSize = true;
            Control_PartIDManagement_TableLayout_RemoveDetails.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_PartIDManagement_TableLayout_RemoveDetails.ColumnCount = 2;
            Control_PartIDManagement_TableLayout_RemoveDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            Control_PartIDManagement_TableLayout_RemoveDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            Control_PartIDManagement_TableLayout_RemoveDetails.Dock = DockStyle.Top;
            Control_PartIDManagement_TableLayout_RemoveDetails.RowCount = 5;
            Control_PartIDManagement_TableLayout_RemoveDetails.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_RemoveDetails.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_RemoveDetails.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_RemoveDetails.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_RemoveDetails.RowStyles.Add(new RowStyle());
            Control_PartIDManagement_TableLayout_RemoveDetails.Controls.Add(Control_PartIDManagement_Label_RemoveItemNumber, 0, 0);
            Control_PartIDManagement_TableLayout_RemoveDetails.Controls.Add(Control_PartIDManagement_Label_RemoveItemNumberValue, 1, 0);
            Control_PartIDManagement_TableLayout_RemoveDetails.Controls.Add(Control_PartIDManagement_Label_RemoveCustomer, 0, 1);
            Control_PartIDManagement_TableLayout_RemoveDetails.Controls.Add(Control_PartIDManagement_Label_RemoveCustomerValue, 1, 1);
            Control_PartIDManagement_TableLayout_RemoveDetails.Controls.Add(Control_PartIDManagement_Label_RemoveDescription, 0, 2);
            Control_PartIDManagement_TableLayout_RemoveDetails.Controls.Add(Control_PartIDManagement_Label_RemoveDescriptionValue, 1, 2);
            Control_PartIDManagement_TableLayout_RemoveDetails.Controls.Add(Control_PartIDManagement_Label_RemoveType, 0, 3);
            Control_PartIDManagement_TableLayout_RemoveDetails.Controls.Add(Control_PartIDManagement_Label_RemoveTypeValue, 1, 3);
            Control_PartIDManagement_TableLayout_RemoveDetails.Controls.Add(Control_PartIDManagement_Label_RemoveIssuedBy, 0, 4);
            Control_PartIDManagement_TableLayout_RemoveDetails.Controls.Add(Control_PartIDManagement_Label_RemoveIssuedByValue, 1, 4);

            ConfigureKeyValueLabel(Control_PartIDManagement_Label_RemoveItemNumber, "Item Number");
            ConfigureKeyValueValue(Control_PartIDManagement_Label_RemoveItemNumberValue);
            ConfigureKeyValueLabel(Control_PartIDManagement_Label_RemoveCustomer, "Customer");
            ConfigureKeyValueValue(Control_PartIDManagement_Label_RemoveCustomerValue);
            ConfigureKeyValueLabel(Control_PartIDManagement_Label_RemoveDescription, "Description");
            Control_PartIDManagement_Label_RemoveDescriptionValue.AutoSize = true;
            Control_PartIDManagement_Label_RemoveDescriptionValue.MaximumSize = new Size(0, 0);
            ConfigureKeyValueLabel(Control_PartIDManagement_Label_RemoveType, "Item Type");
            ConfigureKeyValueValue(Control_PartIDManagement_Label_RemoveTypeValue);
            ConfigureKeyValueLabel(Control_PartIDManagement_Label_RemoveIssuedBy, "Issued By");
            ConfigureKeyValueValue(Control_PartIDManagement_Label_RemoveIssuedByValue);

            Control_PartIDManagement_Label_RemoveWarning.AutoSize = true;
            Control_PartIDManagement_Label_RemoveWarning.ForeColor = Color.FromArgb(192, 0, 0);
            Control_PartIDManagement_Label_RemoveWarning.Text = "Warning: Removal is permanent.";
            Control_PartIDManagement_Label_RemoveWarning.Margin = new Padding(0, 12, 0, 0);

            ConfigureActionPanel(Control_PartIDManagement_FlowPanel_RemoveActions);
            ConfigureDangerButton(Control_PartIDManagement_Button_RemoveConfirm, "Remove");
            ConfigureSecondaryButton(Control_PartIDManagement_Button_RemoveCancel, "Cancel");
            Control_PartIDManagement_FlowPanel_RemoveActions.Controls.Add(Control_PartIDManagement_Button_RemoveCancel);
            Control_PartIDManagement_FlowPanel_RemoveActions.Controls.Add(Control_PartIDManagement_Button_RemoveConfirm);

            Control_PartIDManagement_Panel_RemoveCard.Controls.Add(Control_PartIDManagement_TableLayout_Remove);

            // Arrange cards in table layout
            Control_PartIDManagement_TableLayout_Cards.Controls.SetChildIndex(Control_PartIDManagement_Panel_AddCard, 0);
            Control_PartIDManagement_TableLayout_Cards.Controls.SetChildIndex(Control_PartIDManagement_Panel_EditCard, 1);
            Control_PartIDManagement_TableLayout_Cards.Controls.SetChildIndex(Control_PartIDManagement_Panel_RemoveCard, 2);

            // Control_PartIDManagement
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(Control_PartIDManagement_TableLayout_Main);
            Name = "Control_PartIDManagement";
            Size = new Size(700, 900);
            Control_PartIDManagement_TableLayout_Main.ResumeLayout(false);
            Control_PartIDManagement_TableLayout_Main.PerformLayout();
            Control_PartIDManagement_TableLayout_Cards.ResumeLayout(false);
            Control_PartIDManagement_Panel_AddCard.ResumeLayout(false);
            Control_PartIDManagement_TableLayout_Add.ResumeLayout(false);
            Control_PartIDManagement_TableLayout_Add.PerformLayout();
            Control_PartIDManagement_TableLayout_AddContent.ResumeLayout(false);
            Control_PartIDManagement_TableLayout_AddContent.PerformLayout();
            Control_PartIDManagement_FlowPanel_AddActions.ResumeLayout(false);
            Control_PartIDManagement_Panel_EditCard.ResumeLayout(false);
            Control_PartIDManagement_TableLayout_Edit.ResumeLayout(false);
            Control_PartIDManagement_TableLayout_Edit.PerformLayout();
            Control_PartIDManagement_TableLayout_EditContent.ResumeLayout(false);
            Control_PartIDManagement_TableLayout_EditContent.PerformLayout();
            Control_PartIDManagement_FlowPanel_EditActions.ResumeLayout(false);
            Control_PartIDManagement_Panel_RemoveCard.ResumeLayout(false);
            Control_PartIDManagement_TableLayout_Remove.ResumeLayout(false);
            Control_PartIDManagement_TableLayout_Remove.PerformLayout();
            Control_PartIDManagement_TableLayout_RemoveContent.ResumeLayout(false);
            Control_PartIDManagement_TableLayout_RemoveContent.PerformLayout();
            Control_PartIDManagement_TableLayout_RemoveDetails.ResumeLayout(false);
            Control_PartIDManagement_TableLayout_RemoveDetails.PerformLayout();
            Control_PartIDManagement_FlowPanel_RemoveActions.ResumeLayout(false);
            Control_PartIDManagement_TableLayout_Home.ResumeLayout(false);
            Control_PartIDManagement_TableLayout_Home.PerformLayout();
            Control_PartIDManagement_Panel_Home.ResumeLayout(false);
            Control_PartIDManagement_Panel_Home.PerformLayout();
            Control_PartIDManagement_FlowPanel_BackButton.ResumeLayout(false);
            Control_PartIDManagement_FlowPanel_BackButton.PerformLayout();
            Control_PartIDManagement_Panel_Container.ResumeLayout(false);
            Control_PartIDManagement_Panel_Container.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private static void ConfigureCardPanel(Panel panel, Color accentColor)
        {
            panel.AutoSize = true;
            panel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel.BackColor = Color.FromArgb(250, 250, 250);
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Margin = new Padding(0, 0, 0, 20);
            panel.Padding = new Padding(0);
            panel.Dock = DockStyle.Top;
            
            // Add accent bar at top
            Panel accentBar = new Panel
            {
                Height = 4,
                Dock = DockStyle.Top,
                BackColor = accentColor
            };
            panel.Controls.Add(accentBar);
            accentBar.BringToFront();
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

        private static void ConfigureSuggestion(SuggestionTextBoxWithLabel control, string labelText, string placeholder)
        {
            control.AutoSize = true;
            control.Dock = DockStyle.Top;
            control.LabelText = labelText;
            control.PlaceholderText = placeholder;
            control.ShowValidationColor = true;
        }

        private static TableLayoutPanel CreateKeyValueRow(Label keyLabel, Label valueLabel)
        {
            TableLayoutPanel panel = new()
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 2,
                Dock = DockStyle.Top
            };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            panel.Controls.Add(keyLabel, 0, 0);
            panel.Controls.Add(valueLabel, 1, 0);
            return panel;
        }

        private static void ConfigureKeyValueLabel(Label label, string text)
        {
            label.AutoSize = true;
            label.ForeColor = Color.FromArgb(90, 90, 90);
            label.Margin = new Padding(0, 6, 12, 0);
            label.Text = text + ":";
            label.TextAlign = ContentAlignment.MiddleLeft;
        }

        private static void ConfigureKeyValueValue(Label label)
        {
            label.AutoSize = true;
            label.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            label.Margin = new Padding(0, 6, 0, 0);
        }

        private static void ConfigureActionPanel(FlowLayoutPanel panel)
        {
            panel.AutoSize = true;
            panel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel.Dock = DockStyle.Top;
            panel.FlowDirection = FlowDirection.RightToLeft;
            panel.Margin = new Padding(0, 12, 0, 0);
        }

        private static void ConfigurePrimaryButton(Button button, string text)
        {
            button.AutoSize = true;
            button.BackColor = Color.FromArgb(0, 120, 215);
            button.ForeColor = Color.White;
            button.Padding = new Padding(16, 6, 16, 6);
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Text = text;
        }

        private static void ConfigureSecondaryButton(Button button, string text)
        {
            button.AutoSize = true;
            button.BackColor = Color.WhiteSmoke;
            button.ForeColor = Color.FromArgb(45, 45, 45);
            button.Padding = new Padding(16, 6, 16, 6);
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = Color.Gainsboro;
            button.Text = text;
        }

        private static void ConfigureDangerButton(Button button, string text)
        {
            button.AutoSize = true;
            button.BackColor = Color.FromArgb(194, 57, 52);
            button.ForeColor = Color.White;
            button.Padding = new Padding(16, 6, 16, 6);
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Text = text;
        }

        #endregion
    }
}
