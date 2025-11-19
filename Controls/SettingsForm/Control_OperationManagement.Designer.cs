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
        private Panel Control_OperationManagement_Panel_HomeTile_Edit = null!;
        private Panel Control_OperationManagement_Panel_HomeTile_Remove = null!;
        
        private TableLayoutPanel Control_OperationManagement_TableLayout_Cards = null!;
        private FlowLayoutPanel Control_OperationManagement_FlowPanel_BackButton = null!;
        private Button Control_OperationManagement_Button_Back = null!;

        private Panel Control_OperationManagement_Panel_AddCard = null!;
        private TableLayoutPanel Control_OperationManagement_TableLayout_Add = null!;
        private Label Control_OperationManagement_Label_AddIcon = null!;
        private Label Control_OperationManagement_Label_AddTitle = null!;
        private TableLayoutPanel Control_OperationManagement_TableLayout_AddContent = null!;
        private SuggestionTextBoxWithLabel Control_OperationManagement_TextBox_AddOperation = null!;
        private Label Control_OperationManagement_Label_AddIssuedBy = null!;
        private Label Control_OperationManagement_Label_AddIssuedByValue = null!;
        private FlowLayoutPanel Control_OperationManagement_FlowPanel_AddActions = null!;
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
        private FlowLayoutPanel Control_OperationManagement_FlowPanel_EditActions = null!;
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
        private FlowLayoutPanel Control_OperationManagement_FlowPanel_RemoveActions = null!;
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
            components = new Container();
            Control_OperationManagement_TableLayout_Main = new TableLayoutPanel();
            Control_OperationManagement_Label_Header = new Label();
            Control_OperationManagement_Label_Subtitle = new Label();
            Control_OperationManagement_Panel_Container = new Panel();
            
            Control_OperationManagement_Panel_Home = new Panel();
            Control_OperationManagement_TableLayout_Home = new TableLayoutPanel();
            Control_OperationManagement_Panel_HomeTile_Add = new Panel();
            Control_OperationManagement_Panel_HomeTile_Edit = new Panel();
            Control_OperationManagement_Panel_HomeTile_Remove = new Panel();
            
            Control_OperationManagement_TableLayout_Cards = new TableLayoutPanel();
            Control_OperationManagement_FlowPanel_BackButton = new FlowLayoutPanel();
            Control_OperationManagement_Button_Back = new Button();
            Control_OperationManagement_Panel_AddCard = new Panel();
            Control_OperationManagement_TableLayout_Add = new TableLayoutPanel();
            Control_OperationManagement_Label_AddIcon = new Label();
            Control_OperationManagement_Label_AddTitle = new Label();
            Control_OperationManagement_TableLayout_AddContent = new TableLayoutPanel();
            Control_OperationManagement_TextBox_AddOperation = new SuggestionTextBoxWithLabel();
            Control_OperationManagement_Label_AddIssuedBy = new Label();
            Control_OperationManagement_Label_AddIssuedByValue = new Label();
            Control_OperationManagement_FlowPanel_AddActions = new FlowLayoutPanel();
            Control_OperationManagement_Button_AddSave = new Button();
            Control_OperationManagement_Button_AddClear = new Button();
            Control_OperationManagement_Panel_EditCard = new Panel();
            Control_OperationManagement_TableLayout_Edit = new TableLayoutPanel();
            Control_OperationManagement_Label_EditIcon = new Label();
            Control_OperationManagement_Label_EditTitle = new Label();
            Control_OperationManagement_TableLayout_EditContent = new TableLayoutPanel();
            Control_OperationManagement_Suggestion_EditSelectOperation = new SuggestionTextBoxWithLabel();
            Control_OperationManagement_TextBox_EditNewOperation = new SuggestionTextBoxWithLabel();
            Control_OperationManagement_Label_EditIssuedBy = new Label();
            Control_OperationManagement_Label_EditIssuedByValue = new Label();
            Control_OperationManagement_FlowPanel_EditActions = new FlowLayoutPanel();
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
            Control_OperationManagement_FlowPanel_RemoveActions = new FlowLayoutPanel();
            Control_OperationManagement_Button_RemoveConfirm = new Button();
            Control_OperationManagement_Button_RemoveCancel = new Button();
            Control_OperationManagement_TableLayout_Main.SuspendLayout();
            Control_OperationManagement_Panel_Container.SuspendLayout();
            Control_OperationManagement_Panel_Home.SuspendLayout();
            Control_OperationManagement_TableLayout_Home.SuspendLayout();
            Control_OperationManagement_TableLayout_Cards.SuspendLayout();
            Control_OperationManagement_Panel_AddCard.SuspendLayout();
            Control_OperationManagement_TableLayout_Add.SuspendLayout();
            Control_OperationManagement_TableLayout_AddContent.SuspendLayout();
            Control_OperationManagement_FlowPanel_AddActions.SuspendLayout();
            Control_OperationManagement_Panel_EditCard.SuspendLayout();
            Control_OperationManagement_TableLayout_Edit.SuspendLayout();
            Control_OperationManagement_TableLayout_EditContent.SuspendLayout();
            Control_OperationManagement_FlowPanel_EditActions.SuspendLayout();
            Control_OperationManagement_Panel_RemoveCard.SuspendLayout();
            Control_OperationManagement_TableLayout_Remove.SuspendLayout();
            Control_OperationManagement_TableLayout_RemoveContent.SuspendLayout();
            Control_OperationManagement_TableLayout_RemoveDetails.SuspendLayout();
            Control_OperationManagement_FlowPanel_RemoveActions.SuspendLayout();
            SuspendLayout();

            // Control_OperationManagement_TableLayout_Main
            Control_OperationManagement_TableLayout_Main.AutoSize = true;
            Control_OperationManagement_TableLayout_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_Main.ColumnCount = 1;
            Control_OperationManagement_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_Main.Controls.Add(Control_OperationManagement_Label_Header, 0, 0);
            Control_OperationManagement_TableLayout_Main.Controls.Add(Control_OperationManagement_Label_Subtitle, 0, 1);
            Control_OperationManagement_TableLayout_Main.Controls.Add(Control_OperationManagement_Panel_Container, 0, 2);
            Control_OperationManagement_TableLayout_Main.Dock = DockStyle.Fill;
            Control_OperationManagement_TableLayout_Main.Padding = new Padding(20);
            Control_OperationManagement_TableLayout_Main.RowCount = 3;
            Control_OperationManagement_TableLayout_Main.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_Main.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // Control_OperationManagement_Label_Header
            Control_OperationManagement_Label_Header.AutoSize = true;
            Control_OperationManagement_Label_Header.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_Header.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point);
            Control_OperationManagement_Label_Header.ForeColor = Color.FromArgb(45, 45, 45);
            Control_OperationManagement_Label_Header.Text = "Operation Codes";

            // Control_OperationManagement_Label_Subtitle
            Control_OperationManagement_Label_Subtitle.AutoSize = true;
            Control_OperationManagement_Label_Subtitle.Dock = DockStyle.Fill;
            Control_OperationManagement_Label_Subtitle.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            Control_OperationManagement_Label_Subtitle.ForeColor = Color.FromArgb(90, 90, 90);
            Control_OperationManagement_Label_Subtitle.Margin = new Padding(3, 6, 3, 15);
            Control_OperationManagement_Label_Subtitle.Text = "Select an action below to manage operation codes.";

            // Control_OperationManagement_Panel_Container
            Control_OperationManagement_Panel_Container.AutoSize = true;
            Control_OperationManagement_Panel_Container.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_Panel_Container.Dock = DockStyle.Fill;
            Control_OperationManagement_Panel_Container.Controls.Add(Control_OperationManagement_Panel_Home);
            Control_OperationManagement_Panel_Container.Controls.Add(Control_OperationManagement_TableLayout_Cards);
            Control_OperationManagement_Panel_Container.Controls.Add(Control_OperationManagement_FlowPanel_BackButton);

            // Control_OperationManagement_Panel_Home
            Control_OperationManagement_Panel_Home.AutoSize = true;
            Control_OperationManagement_Panel_Home.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_Panel_Home.Dock = DockStyle.Fill;
            Control_OperationManagement_Panel_Home.Controls.Add(Control_OperationManagement_TableLayout_Home);
            Control_OperationManagement_Panel_Home.Visible = true;

            // Control_OperationManagement_TableLayout_Home
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
            Control_OperationManagement_TableLayout_Home.RowCount = 1;
            Control_OperationManagement_TableLayout_Home.RowStyles.Add(new RowStyle());

            ConfigureHomeTile(Control_OperationManagement_Panel_HomeTile_Add, "🆕", "Add Part", "Create new operation codes", Color.FromArgb(16, 124, 16), 0);
            ConfigureHomeTile(Control_OperationManagement_Panel_HomeTile_Edit, "✏️", "Edit Part", "Modify existing parts", Color.FromArgb(0, 120, 215), 1);
            ConfigureHomeTile(Control_OperationManagement_Panel_HomeTile_Remove, "🗑️", "Remove Part", "Delete operation codes", Color.FromArgb(194, 57, 52), 2);

            // Control_OperationManagement_FlowPanel_BackButton
            Control_OperationManagement_FlowPanel_BackButton.AutoSize = true;
            Control_OperationManagement_FlowPanel_BackButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_FlowPanel_BackButton.Dock = DockStyle.Bottom;
            Control_OperationManagement_FlowPanel_BackButton.FlowDirection = FlowDirection.LeftToRight;
            Control_OperationManagement_FlowPanel_BackButton.Padding = new Padding(0, 12, 0, 0);
            Control_OperationManagement_FlowPanel_BackButton.Controls.Add(Control_OperationManagement_Button_Back);
            Control_OperationManagement_FlowPanel_BackButton.Visible = false;

            Control_OperationManagement_Button_Back.AutoSize = true;
            Control_OperationManagement_Button_Back.Text = "← Back to Selection";
            Control_OperationManagement_Button_Back.Padding = new Padding(16, 6, 16, 6);
            ConfigureSecondaryButton(Control_OperationManagement_Button_Back, "← Back to Selection");

            // Control_OperationManagement_TableLayout_Cards
            Control_OperationManagement_TableLayout_Cards.AutoSize = true;
            Control_OperationManagement_TableLayout_Cards.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_Cards.ColumnCount = 1;
            Control_OperationManagement_TableLayout_Cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_Cards.Controls.Add(Control_OperationManagement_Panel_AddCard, 0, 0);
            Control_OperationManagement_TableLayout_Cards.Controls.Add(Control_OperationManagement_Panel_EditCard, 0, 1);
            Control_OperationManagement_TableLayout_Cards.Controls.Add(Control_OperationManagement_Panel_RemoveCard, 0, 2);
            Control_OperationManagement_TableLayout_Cards.Dock = DockStyle.Fill;
            Control_OperationManagement_TableLayout_Cards.Visible = false;
            Control_OperationManagement_TableLayout_Cards.RowCount = 3;
            Control_OperationManagement_TableLayout_Cards.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_Cards.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_Cards.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_Cards.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;

            // Add Card Panel
            ConfigureCardPanel(Control_OperationManagement_Panel_AddCard, Color.FromArgb(16, 124, 16));
            Control_OperationManagement_TableLayout_Add.ColumnCount = 2;
            Control_OperationManagement_TableLayout_Add.ColumnStyles.Add(new ColumnStyle());
            Control_OperationManagement_TableLayout_Add.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_Add.Controls.Add(Control_OperationManagement_Label_AddIcon, 0, 0);
            Control_OperationManagement_TableLayout_Add.Controls.Add(Control_OperationManagement_Label_AddTitle, 1, 0);
            Control_OperationManagement_TableLayout_Add.Controls.Add(Control_OperationManagement_TableLayout_AddContent, 0, 1);
            Control_OperationManagement_TableLayout_Add.SetColumnSpan(Control_OperationManagement_TableLayout_AddContent, 2);
            Control_OperationManagement_TableLayout_Add.AutoSize = true;
            Control_OperationManagement_TableLayout_Add.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_Add.Dock = DockStyle.Fill;
            Control_OperationManagement_TableLayout_Add.RowCount = 2;
            Control_OperationManagement_TableLayout_Add.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_Add.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_Add.Padding = new Padding(16);

            Control_OperationManagement_Label_AddIcon.AutoSize = true;
            Control_OperationManagement_Label_AddIcon.Font = new Font("Segoe UI Emoji", 28F, FontStyle.Regular, GraphicsUnit.Point);
            Control_OperationManagement_Label_AddIcon.Text = "🆕";
            Control_OperationManagement_Label_AddIcon.Margin = new Padding(0, 0, 12, 0);
            Control_OperationManagement_Label_AddIcon.TextAlign = ContentAlignment.MiddleCenter;

            Control_OperationManagement_Label_AddTitle.AutoSize = true;
            Control_OperationManagement_Label_AddTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            Control_OperationManagement_Label_AddTitle.Text = "Add Part";
            Control_OperationManagement_Label_AddTitle.TextAlign = ContentAlignment.MiddleLeft;

            Control_OperationManagement_TableLayout_AddContent.AutoSize = true;
            Control_OperationManagement_TableLayout_AddContent.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_AddContent.ColumnCount = 1;
            Control_OperationManagement_TableLayout_AddContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_AddContent.Controls.Add(Control_OperationManagement_TextBox_AddOperation, 0, 0);
            Control_OperationManagement_TableLayout_AddContent.Controls.Add(CreateKeyValueRow(Control_OperationManagement_Label_AddIssuedBy, Control_OperationManagement_Label_AddIssuedByValue), 0, 1);
            Control_OperationManagement_TableLayout_AddContent.Controls.Add(Control_OperationManagement_FlowPanel_AddActions, 0, 2);
            Control_OperationManagement_TableLayout_AddContent.Dock = DockStyle.Top;
            Control_OperationManagement_TableLayout_AddContent.RowCount = 3;
            Control_OperationManagement_TableLayout_AddContent.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_AddContent.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_AddContent.RowStyles.Add(new RowStyle());

            ConfigureSuggestion(Control_OperationManagement_TextBox_AddOperation, "Operation", "Enter operation number");
            Control_OperationManagement_TextBox_AddOperation.EnableSuggestions = false;
            Control_OperationManagement_TextBox_AddOperation.ShowValidationColor = true;
            Control_OperationManagement_TextBox_AddOperation.TextBox.Enabled = true;

            ConfigureKeyValueLabel(Control_OperationManagement_Label_AddIssuedBy, "Issued By");

            ConfigureActionPanel(Control_OperationManagement_FlowPanel_AddActions);
            ConfigurePrimaryButton(Control_OperationManagement_Button_AddSave, "Save");
            ConfigureSecondaryButton(Control_OperationManagement_Button_AddClear, "Clear");
            Control_OperationManagement_FlowPanel_AddActions.Controls.Add(Control_OperationManagement_Button_AddClear);
            Control_OperationManagement_FlowPanel_AddActions.Controls.Add(Control_OperationManagement_Button_AddSave);

            Control_OperationManagement_Panel_AddCard.Controls.Add(Control_OperationManagement_TableLayout_Add);

            // Edit Card Panel
            ConfigureCardPanel(Control_OperationManagement_Panel_EditCard, Color.FromArgb(0, 120, 215));
            Control_OperationManagement_TableLayout_Edit.ColumnCount = 2;
            Control_OperationManagement_TableLayout_Edit.ColumnStyles.Add(new ColumnStyle());
            Control_OperationManagement_TableLayout_Edit.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_Edit.Controls.Add(Control_OperationManagement_Label_EditIcon, 0, 0);
            Control_OperationManagement_TableLayout_Edit.Controls.Add(Control_OperationManagement_Label_EditTitle, 1, 0);
            Control_OperationManagement_TableLayout_Edit.Controls.Add(Control_OperationManagement_TableLayout_EditContent, 0, 1);
            Control_OperationManagement_TableLayout_Edit.SetColumnSpan(Control_OperationManagement_TableLayout_EditContent, 2);
            Control_OperationManagement_TableLayout_Edit.AutoSize = true;
            Control_OperationManagement_TableLayout_Edit.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_Edit.Dock = DockStyle.Fill;
            Control_OperationManagement_TableLayout_Edit.RowCount = 2;
            Control_OperationManagement_TableLayout_Edit.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_Edit.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_Edit.Padding = new Padding(16);

            Control_OperationManagement_Label_EditIcon.AutoSize = true;
            Control_OperationManagement_Label_EditIcon.Font = new Font("Segoe UI Emoji", 28F);
            Control_OperationManagement_Label_EditIcon.Text = "✏️";
            Control_OperationManagement_Label_EditIcon.Margin = new Padding(0, 0, 12, 0);

            Control_OperationManagement_Label_EditTitle.AutoSize = true;
            Control_OperationManagement_Label_EditTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            Control_OperationManagement_Label_EditTitle.Text = "Edit Part";

            Control_OperationManagement_TableLayout_EditContent.AutoSize = true;
            Control_OperationManagement_TableLayout_EditContent.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_EditContent.ColumnCount = 1;
            Control_OperationManagement_TableLayout_EditContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_EditContent.Controls.Add(Control_OperationManagement_Suggestion_EditSelectOperation, 0, 0);
            Control_OperationManagement_TableLayout_EditContent.Controls.Add(Control_OperationManagement_TextBox_EditNewOperation, 0, 1);
            Control_OperationManagement_TableLayout_EditContent.Controls.Add(CreateKeyValueRow(Control_OperationManagement_Label_EditIssuedBy, Control_OperationManagement_Label_EditIssuedByValue), 0, 3);
            Control_OperationManagement_TableLayout_EditContent.Controls.Add(Control_OperationManagement_FlowPanel_EditActions, 0, 5);
            Control_OperationManagement_TableLayout_EditContent.Dock = DockStyle.Top;
            Control_OperationManagement_TableLayout_EditContent.RowCount = 6;
            Control_OperationManagement_TableLayout_EditContent.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_EditContent.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_EditContent.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_EditContent.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_EditContent.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_EditContent.RowStyles.Add(new RowStyle());

            ConfigureSuggestion(Control_OperationManagement_Suggestion_EditSelectOperation, "Select Operation", "Search operations (F4)");
            ConfigureSuggestion(Control_OperationManagement_TextBox_EditNewOperation, "New Operation", "Enter new value");
            Control_OperationManagement_TextBox_EditNewOperation.EnableSuggestions = false;

            ConfigureKeyValueLabel(Control_OperationManagement_Label_EditIssuedBy, "Issued By");
            ConfigureKeyValueValue(Control_OperationManagement_Label_EditIssuedByValue);

            ConfigureActionPanel(Control_OperationManagement_FlowPanel_EditActions);
            ConfigurePrimaryButton(Control_OperationManagement_Button_EditSave, "Save Changes");
            ConfigureSecondaryButton(Control_OperationManagement_Button_EditReset, "Reset");
            Control_OperationManagement_FlowPanel_EditActions.Controls.Add(Control_OperationManagement_Button_EditReset);
            Control_OperationManagement_FlowPanel_EditActions.Controls.Add(Control_OperationManagement_Button_EditSave);

            Control_OperationManagement_Panel_EditCard.Controls.Add(Control_OperationManagement_TableLayout_Edit);

            // Remove Card Panel
            ConfigureCardPanel(Control_OperationManagement_Panel_RemoveCard, Color.FromArgb(194, 57, 52));
            Control_OperationManagement_TableLayout_Remove.ColumnCount = 2;
            Control_OperationManagement_TableLayout_Remove.ColumnStyles.Add(new ColumnStyle());
            Control_OperationManagement_TableLayout_Remove.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_Remove.Controls.Add(Control_OperationManagement_Label_RemoveIcon, 0, 0);
            Control_OperationManagement_TableLayout_Remove.Controls.Add(Control_OperationManagement_Label_RemoveTitle, 1, 0);
            Control_OperationManagement_TableLayout_Remove.Controls.Add(Control_OperationManagement_TableLayout_RemoveContent, 0, 1);
            Control_OperationManagement_TableLayout_Remove.SetColumnSpan(Control_OperationManagement_TableLayout_RemoveContent, 2);
            Control_OperationManagement_TableLayout_Remove.AutoSize = true;
            Control_OperationManagement_TableLayout_Remove.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_Remove.Dock = DockStyle.Fill;
            Control_OperationManagement_TableLayout_Remove.RowCount = 2;
            Control_OperationManagement_TableLayout_Remove.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_Remove.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_Remove.Padding = new Padding(16);

            Control_OperationManagement_Label_RemoveIcon.AutoSize = true;
            Control_OperationManagement_Label_RemoveIcon.Font = new Font("Segoe UI Emoji", 28F);
            Control_OperationManagement_Label_RemoveIcon.Text = "🗑️";
            Control_OperationManagement_Label_RemoveIcon.Margin = new Padding(0, 0, 12, 0);

            Control_OperationManagement_Label_RemoveTitle.AutoSize = true;
            Control_OperationManagement_Label_RemoveTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            Control_OperationManagement_Label_RemoveTitle.Text = "Remove Part";

            Control_OperationManagement_TableLayout_RemoveContent.AutoSize = true;
            Control_OperationManagement_TableLayout_RemoveContent.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_RemoveContent.ColumnCount = 1;
            Control_OperationManagement_TableLayout_RemoveContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_OperationManagement_TableLayout_RemoveContent.Controls.Add(Control_OperationManagement_Suggestion_RemoveSelectOperation, 0, 0);
            Control_OperationManagement_TableLayout_RemoveContent.Controls.Add(Control_OperationManagement_TableLayout_RemoveDetails, 0, 1);
            Control_OperationManagement_TableLayout_RemoveContent.Controls.Add(Control_OperationManagement_Label_RemoveWarning, 0, 2);
            Control_OperationManagement_TableLayout_RemoveContent.Controls.Add(Control_OperationManagement_FlowPanel_RemoveActions, 0, 3);
            Control_OperationManagement_TableLayout_RemoveContent.Dock = DockStyle.Top;
            Control_OperationManagement_TableLayout_RemoveContent.RowCount = 4;
            Control_OperationManagement_TableLayout_RemoveContent.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_RemoveContent.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_RemoveContent.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_RemoveContent.RowStyles.Add(new RowStyle());

            ConfigureSuggestion(Control_OperationManagement_Suggestion_RemoveSelectOperation, "Select Operation", "Search operations (F4)");

            Control_OperationManagement_TableLayout_RemoveDetails.AutoSize = true;
            Control_OperationManagement_TableLayout_RemoveDetails.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_OperationManagement_TableLayout_RemoveDetails.ColumnCount = 2;
            Control_OperationManagement_TableLayout_RemoveDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            Control_OperationManagement_TableLayout_RemoveDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            Control_OperationManagement_TableLayout_RemoveDetails.Dock = DockStyle.Top;
            Control_OperationManagement_TableLayout_RemoveDetails.RowCount = 2;
            Control_OperationManagement_TableLayout_RemoveDetails.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_RemoveDetails.RowStyles.Add(new RowStyle());
            Control_OperationManagement_TableLayout_RemoveDetails.Controls.Add(Control_OperationManagement_Label_RemoveOperation, 0, 0);
            Control_OperationManagement_TableLayout_RemoveDetails.Controls.Add(Control_OperationManagement_Label_RemoveOperationValue, 1, 0);
            Control_OperationManagement_TableLayout_RemoveDetails.Controls.Add(Control_OperationManagement_Label_RemoveIssuedBy, 0, 1);
            Control_OperationManagement_TableLayout_RemoveDetails.Controls.Add(Control_OperationManagement_Label_RemoveIssuedByValue, 1, 1);

            ConfigureKeyValueLabel(Control_OperationManagement_Label_RemoveOperation, "Operation:");
            ConfigureKeyValueValue(Control_OperationManagement_Label_RemoveOperationValue);
            ConfigureKeyValueLabel(Control_OperationManagement_Label_RemoveIssuedBy, "Issued By:");
            ConfigureKeyValueValue(Control_OperationManagement_Label_RemoveIssuedByValue);

            Control_OperationManagement_Label_RemoveWarning.AutoSize = true;
            Control_OperationManagement_Label_RemoveWarning.ForeColor = Color.FromArgb(192, 0, 0);
            Control_OperationManagement_Label_RemoveWarning.Text = "Warning: Removal is permanent.";
            Control_OperationManagement_Label_RemoveWarning.Margin = new Padding(0, 12, 0, 0);

            ConfigureActionPanel(Control_OperationManagement_FlowPanel_RemoveActions);
            ConfigureDangerButton(Control_OperationManagement_Button_RemoveConfirm, "Remove");
            ConfigureSecondaryButton(Control_OperationManagement_Button_RemoveCancel, "Cancel");
            Control_OperationManagement_FlowPanel_RemoveActions.Controls.Add(Control_OperationManagement_Button_RemoveCancel);
            Control_OperationManagement_FlowPanel_RemoveActions.Controls.Add(Control_OperationManagement_Button_RemoveConfirm);

            Control_OperationManagement_Panel_RemoveCard.Controls.Add(Control_OperationManagement_TableLayout_Remove);

            // Arrange cards in table layout
            Control_OperationManagement_TableLayout_Cards.Controls.SetChildIndex(Control_OperationManagement_Panel_AddCard, 0);
            Control_OperationManagement_TableLayout_Cards.Controls.SetChildIndex(Control_OperationManagement_Panel_EditCard, 1);
            Control_OperationManagement_TableLayout_Cards.Controls.SetChildIndex(Control_OperationManagement_Panel_RemoveCard, 2);

            // Control_OperationManagement
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(Control_OperationManagement_TableLayout_Main);
            Name = "Control_OperationManagement";
            Size = new Size(700, 900);
            Control_OperationManagement_TableLayout_Main.ResumeLayout(false);
            Control_OperationManagement_TableLayout_Main.PerformLayout();
            Control_OperationManagement_TableLayout_Cards.ResumeLayout(false);
            Control_OperationManagement_Panel_AddCard.ResumeLayout(false);
            Control_OperationManagement_TableLayout_Add.ResumeLayout(false);
            Control_OperationManagement_TableLayout_Add.PerformLayout();
            Control_OperationManagement_TableLayout_AddContent.ResumeLayout(false);
            Control_OperationManagement_TableLayout_AddContent.PerformLayout();
            Control_OperationManagement_FlowPanel_AddActions.ResumeLayout(false);
            Control_OperationManagement_Panel_EditCard.ResumeLayout(false);
            Control_OperationManagement_TableLayout_Edit.ResumeLayout(false);
            Control_OperationManagement_TableLayout_Edit.PerformLayout();
            Control_OperationManagement_TableLayout_EditContent.ResumeLayout(false);
            Control_OperationManagement_TableLayout_EditContent.PerformLayout();
            Control_OperationManagement_FlowPanel_EditActions.ResumeLayout(false);
            Control_OperationManagement_Panel_RemoveCard.ResumeLayout(false);
            Control_OperationManagement_TableLayout_Remove.ResumeLayout(false);
            Control_OperationManagement_TableLayout_Remove.PerformLayout();
            Control_OperationManagement_TableLayout_RemoveContent.ResumeLayout(false);
            Control_OperationManagement_TableLayout_RemoveContent.PerformLayout();
            Control_OperationManagement_TableLayout_RemoveDetails.ResumeLayout(false);
            Control_OperationManagement_TableLayout_RemoveDetails.PerformLayout();
            Control_OperationManagement_FlowPanel_RemoveActions.ResumeLayout(false);
            Control_OperationManagement_TableLayout_Home.ResumeLayout(false);
            Control_OperationManagement_TableLayout_Home.PerformLayout();
            Control_OperationManagement_Panel_Home.ResumeLayout(false);
            Control_OperationManagement_Panel_Home.PerformLayout();
            Control_OperationManagement_FlowPanel_BackButton.ResumeLayout(false);
            Control_OperationManagement_FlowPanel_BackButton.PerformLayout();
            Control_OperationManagement_Panel_Container.ResumeLayout(false);
            Control_OperationManagement_Panel_Container.PerformLayout();
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
