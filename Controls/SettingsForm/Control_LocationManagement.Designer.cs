using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Controls.Shared;

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    partial class Control_LocationManagement
    {
        #region Fields

        private IContainer components = null!;
        private TableLayoutPanel Control_LocationManagement_TableLayoutPanel_Main = null!;
        private Label Control_LocationManagement_Label_Header = null!;
        private Label Control_LocationManagement_Label_Subtitle = null!;
        private Panel Control_LocationManagement_Panel_Container = null!;
        
        private Panel Control_LocationManagement_Panel_Home = null!;
        private TableLayoutPanel Control_LocationManagement_TableLayoutPanel_Home = null!;
        private Panel Control_LocationManagement_Panel_HomeTile_Add = null!;
        private Panel Control_LocationManagement_Panel_HomeTile_Edit = null!;
        private Panel Control_LocationManagement_Panel_HomeTile_Remove = null!;
        
        private TableLayoutPanel Control_LocationManagement_TableLayoutPanel_Cards = null!;
        private TableLayoutPanel Control_LocationManagement_TableLayoutPanel_BackButton = null!;
        private Button Control_LocationManagement_Button_Back = null!;

        private Panel Control_LocationManagement_Panel_AddCard = null!;
        private TableLayoutPanel Control_LocationManagement_TableLayoutPanel_Add = null!;
        private Label Control_LocationManagement_Label_AddIcon = null!;
        private Label Control_LocationManagement_Label_AddTitle = null!;
        private TableLayoutPanel Control_LocationManagement_TableLayoutPanel_AddContent = null!;
        private TextBox Control_LocationManagement_TextBox_AddLocation = null!;
        private ComboBox Control_LocationManagement_ComboBox_AddBuilding = null!;
        private Label Control_LocationManagement_Label_AddIssuedBy = null!;
        private Label Control_LocationManagement_Label_AddIssuedByValue = null!;
        private TableLayoutPanel Control_LocationManagement_TableLayoutPanel_AddActions = null!;
        private Button Control_LocationManagement_Button_AddSave = null!;
        private Button Control_LocationManagement_Button_AddClear = null!;

        private Panel Control_LocationManagement_Panel_EditCard = null!;
        private TableLayoutPanel Control_LocationManagement_TableLayoutPanel_Edit = null!;
        private Label Control_LocationManagement_Label_EditIcon = null!;
        private Label Control_LocationManagement_Label_EditTitle = null!;
        private TableLayoutPanel Control_LocationManagement_TableLayoutPanel_EditContent = null!;
        private SuggestionTextBoxWithLabel Control_LocationManagement_Suggestion_EditSelectLocation = null!;
        private TextBox Control_LocationManagement_TextBox_EditNewLocation = null!;
        private ComboBox Control_LocationManagement_ComboBox_EditBuilding = null!;
        private Label Control_LocationManagement_Label_EditIssuedBy = null!;
        private Label Control_LocationManagement_Label_EditIssuedByValue = null!;
        private TableLayoutPanel Control_LocationManagement_TableLayoutPanel_EditActions = null!;
        private Button Control_LocationManagement_Button_EditSave = null!;
        private Button Control_LocationManagement_Button_EditReset = null!;

        private Panel Control_LocationManagement_Panel_RemoveCard = null!;
        private TableLayoutPanel Control_LocationManagement_TableLayoutPanel_Remove = null!;
        private Label Control_LocationManagement_Label_RemoveIcon = null!;
        private Label Control_LocationManagement_Label_RemoveTitle = null!;
        private TableLayoutPanel Control_LocationManagement_TableLayoutPanel_RemoveContent = null!;
        private SuggestionTextBoxWithLabel Control_LocationManagement_Suggestion_RemoveSelectLocation = null!;
        private TableLayoutPanel Control_LocationManagement_TableLayoutPanel_RemoveDetails = null!;
        private Label Control_LocationManagement_Label_RemoveLocation = null!;
        private Label Control_LocationManagement_Label_RemoveLocationValue = null!;
        private Label Control_LocationManagement_Label_RemoveBuilding = null!;
        private Label Control_LocationManagement_Label_RemoveBuildingValue = null!;
        private Label Control_LocationManagement_Label_RemoveIssuedBy = null!;
        private Label Control_LocationManagement_Label_RemoveIssuedByValue = null!;
        private Label Control_LocationManagement_Label_RemoveWarning = null!;
        private TableLayoutPanel Control_LocationManagement_TableLayoutPanel_RemoveActions = null!;
        private Button Control_LocationManagement_Button_RemoveConfirm = null!;
        private Button Control_LocationManagement_Button_RemoveCancel = null!;

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
            Control_LocationManagement_TableLayoutPanel_Main = new TableLayoutPanel();
            Control_LocationManagement_Label_Header = new Label();
            Control_LocationManagement_Label_Subtitle = new Label();
            Control_LocationManagement_Panel_Container = new Panel();
            
            Control_LocationManagement_Panel_Home = new Panel();
            Control_LocationManagement_TableLayoutPanel_Home = new TableLayoutPanel();
            Control_LocationManagement_Panel_HomeTile_Add = new Panel();
            Control_LocationManagement_Panel_HomeTile_Edit = new Panel();
            Control_LocationManagement_Panel_HomeTile_Remove = new Panel();
            
            Control_LocationManagement_TableLayoutPanel_Cards = new TableLayoutPanel();
            Control_LocationManagement_TableLayoutPanel_BackButton = new TableLayoutPanel();
            Control_LocationManagement_Button_Back = new Button();
            Control_LocationManagement_Panel_AddCard = new Panel();
            Control_LocationManagement_TableLayoutPanel_Add = new TableLayoutPanel();
            Control_LocationManagement_Label_AddIcon = new Label();
            Control_LocationManagement_Label_AddTitle = new Label();
            Control_LocationManagement_TableLayoutPanel_AddContent = new TableLayoutPanel();
            Control_LocationManagement_TextBox_AddLocation = new TextBox();
            Control_LocationManagement_ComboBox_AddBuilding = new ComboBox();
            Control_LocationManagement_Label_AddIssuedBy = new Label();
            Control_LocationManagement_Label_AddIssuedByValue = new Label();
            Control_LocationManagement_TableLayoutPanel_AddActions = new TableLayoutPanel();
            Control_LocationManagement_Button_AddSave = new Button();
            Control_LocationManagement_Button_AddClear = new Button();
            Control_LocationManagement_Panel_EditCard = new Panel();
            Control_LocationManagement_TableLayoutPanel_Edit = new TableLayoutPanel();
            Control_LocationManagement_Label_EditIcon = new Label();
            Control_LocationManagement_Label_EditTitle = new Label();
            Control_LocationManagement_TableLayoutPanel_EditContent = new TableLayoutPanel();
            Control_LocationManagement_Suggestion_EditSelectLocation = new SuggestionTextBoxWithLabel();
            Control_LocationManagement_TextBox_EditNewLocation = new TextBox();
            Control_LocationManagement_ComboBox_EditBuilding = new ComboBox();
            Control_LocationManagement_Label_EditIssuedBy = new Label();
            Control_LocationManagement_Label_EditIssuedByValue = new Label();
            Control_LocationManagement_TableLayoutPanel_EditActions = new TableLayoutPanel();
            Control_LocationManagement_Button_EditSave = new Button();
            Control_LocationManagement_Button_EditReset = new Button();
            Control_LocationManagement_Panel_RemoveCard = new Panel();
            Control_LocationManagement_TableLayoutPanel_Remove = new TableLayoutPanel();
            Control_LocationManagement_Label_RemoveIcon = new Label();
            Control_LocationManagement_Label_RemoveTitle = new Label();
            Control_LocationManagement_TableLayoutPanel_RemoveContent = new TableLayoutPanel();
            Control_LocationManagement_Suggestion_RemoveSelectLocation = new SuggestionTextBoxWithLabel();
            Control_LocationManagement_TableLayoutPanel_RemoveDetails = new TableLayoutPanel();
            Control_LocationManagement_Label_RemoveLocation = new Label();
            Control_LocationManagement_Label_RemoveLocationValue = new Label();
            Control_LocationManagement_Label_RemoveBuilding = new Label();
            Control_LocationManagement_Label_RemoveBuildingValue = new Label();
            Control_LocationManagement_Label_RemoveIssuedBy = new Label();
            Control_LocationManagement_Label_RemoveIssuedByValue = new Label();
            Control_LocationManagement_Label_RemoveWarning = new Label();
            Control_LocationManagement_TableLayoutPanel_RemoveActions = new TableLayoutPanel();
            Control_LocationManagement_Button_RemoveConfirm = new Button();
            Control_LocationManagement_Button_RemoveCancel = new Button();
            Control_LocationManagement_TableLayoutPanel_Main.SuspendLayout();
            Control_LocationManagement_Panel_Container.SuspendLayout();
            Control_LocationManagement_Panel_Home.SuspendLayout();
            Control_LocationManagement_TableLayoutPanel_Home.SuspendLayout();
            Control_LocationManagement_TableLayoutPanel_Cards.SuspendLayout();
            Control_LocationManagement_Panel_AddCard.SuspendLayout();
            Control_LocationManagement_TableLayoutPanel_Add.SuspendLayout();
            Control_LocationManagement_TableLayoutPanel_AddContent.SuspendLayout();
            Control_LocationManagement_TableLayoutPanel_AddActions.SuspendLayout();
            Control_LocationManagement_Panel_EditCard.SuspendLayout();
            Control_LocationManagement_TableLayoutPanel_Edit.SuspendLayout();
            Control_LocationManagement_TableLayoutPanel_EditContent.SuspendLayout();
            Control_LocationManagement_TableLayoutPanel_EditActions.SuspendLayout();
            Control_LocationManagement_Panel_RemoveCard.SuspendLayout();
            Control_LocationManagement_TableLayoutPanel_Remove.SuspendLayout();
            Control_LocationManagement_TableLayoutPanel_RemoveContent.SuspendLayout();
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.SuspendLayout();
            Control_LocationManagement_TableLayoutPanel_RemoveActions.SuspendLayout();
            Control_LocationManagement_TableLayoutPanel_BackButton.SuspendLayout();
            SuspendLayout();

            // Control_LocationManagement_TableLayoutPanel_Main
            Control_LocationManagement_TableLayoutPanel_Main.AutoSize = true;
            Control_LocationManagement_TableLayoutPanel_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_TableLayoutPanel_Main.ColumnCount = 1;
            Control_LocationManagement_TableLayoutPanel_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_LocationManagement_TableLayoutPanel_Main.Controls.Add(Control_LocationManagement_Label_Header, 0, 0);
            Control_LocationManagement_TableLayoutPanel_Main.Controls.Add(Control_LocationManagement_Label_Subtitle, 0, 1);
            Control_LocationManagement_TableLayoutPanel_Main.Controls.Add(Control_LocationManagement_Panel_Container, 0, 2);
            Control_LocationManagement_TableLayoutPanel_Main.Dock = DockStyle.Fill;
            Control_LocationManagement_TableLayoutPanel_Main.Padding = new Padding(20);
            Control_LocationManagement_TableLayoutPanel_Main.RowCount = 3;
            Control_LocationManagement_TableLayoutPanel_Main.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_Main.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // Control_LocationManagement_Label_Header
            Control_LocationManagement_Label_Header.AutoSize = true;
            Control_LocationManagement_Label_Header.Dock = DockStyle.Fill;
            Control_LocationManagement_Label_Header.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point);
            Control_LocationManagement_Label_Header.ForeColor = Color.FromArgb(45, 45, 45);
            Control_LocationManagement_Label_Header.Text = "Locations";

            // Control_LocationManagement_Label_Subtitle
            Control_LocationManagement_Label_Subtitle.AutoSize = true;
            Control_LocationManagement_Label_Subtitle.Dock = DockStyle.Fill;
            Control_LocationManagement_Label_Subtitle.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            Control_LocationManagement_Label_Subtitle.ForeColor = Color.FromArgb(90, 90, 90);
            Control_LocationManagement_Label_Subtitle.Margin = new Padding(3, 6, 3, 15);
            Control_LocationManagement_Label_Subtitle.Text = "Select an action below to manage locations.";

            // Control_LocationManagement_Panel_Container
            Control_LocationManagement_Panel_Container.AutoSize = true;
            Control_LocationManagement_Panel_Container.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_Panel_Container.Dock = DockStyle.Fill;
            Control_LocationManagement_Panel_Container.Controls.Add(Control_LocationManagement_Panel_Home);
            Control_LocationManagement_Panel_Container.Controls.Add(Control_LocationManagement_TableLayoutPanel_Cards);

            // Control_LocationManagement_Panel_Home
            Control_LocationManagement_Panel_Home.AutoSize = true;
            Control_LocationManagement_Panel_Home.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_Panel_Home.Dock = DockStyle.Fill;
            Control_LocationManagement_Panel_Home.Controls.Add(Control_LocationManagement_TableLayoutPanel_Home);
            Control_LocationManagement_Panel_Home.Visible = true;

            // Control_LocationManagement_TableLayoutPanel_Home
            Control_LocationManagement_TableLayoutPanel_Home.AutoSize = true;
            Control_LocationManagement_TableLayoutPanel_Home.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_TableLayoutPanel_Home.ColumnCount = 3;
            Control_LocationManagement_TableLayoutPanel_Home.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            Control_LocationManagement_TableLayoutPanel_Home.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            Control_LocationManagement_TableLayoutPanel_Home.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
            Control_LocationManagement_TableLayoutPanel_Home.Controls.Add(Control_LocationManagement_Panel_HomeTile_Add, 0, 0);
            Control_LocationManagement_TableLayoutPanel_Home.Controls.Add(Control_LocationManagement_Panel_HomeTile_Edit, 1, 0);
            Control_LocationManagement_TableLayoutPanel_Home.Controls.Add(Control_LocationManagement_Panel_HomeTile_Remove, 2, 0);
            Control_LocationManagement_TableLayoutPanel_Home.Dock = DockStyle.Fill;
            Control_LocationManagement_TableLayoutPanel_Home.RowCount = 1;
            Control_LocationManagement_TableLayoutPanel_Home.RowStyles.Add(new RowStyle());

            ConfigureHomeTile(Control_LocationManagement_Panel_HomeTile_Add, "🏢", "Add Location", "Create new locations", Color.FromArgb(16, 124, 16), 0);
            ConfigureHomeTile(Control_LocationManagement_Panel_HomeTile_Edit, "✏️", "Edit Location", "Modify existing locations", Color.FromArgb(0, 120, 215), 1);
            ConfigureHomeTile(Control_LocationManagement_Panel_HomeTile_Remove, "🗑️", "Remove Location", "Delete locations", Color.FromArgb(194, 57, 52), 2);

            // Control_LocationManagement_TableLayoutPanel_BackButton
            Control_LocationManagement_TableLayoutPanel_BackButton.AutoSize = true;
            Control_LocationManagement_TableLayoutPanel_BackButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_TableLayoutPanel_BackButton.ColumnCount = 2;
            Control_LocationManagement_TableLayoutPanel_BackButton.ColumnStyles.Add(new ColumnStyle());
            Control_LocationManagement_TableLayoutPanel_BackButton.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_LocationManagement_TableLayoutPanel_BackButton.Controls.Add(Control_LocationManagement_Button_Back, 0, 0);
            Control_LocationManagement_TableLayoutPanel_BackButton.Dock = DockStyle.Fill;
            Control_LocationManagement_TableLayoutPanel_BackButton.RowCount = 1;
            Control_LocationManagement_TableLayoutPanel_BackButton.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_LocationManagement_TableLayoutPanel_BackButton.Visible = false;

            Control_LocationManagement_Button_Back.AutoSize = true;
            Control_LocationManagement_Button_Back.Padding = new Padding(16, 6, 16, 6);

            // Control_LocationManagement_TableLayoutPanel_Cards
            Control_LocationManagement_TableLayoutPanel_Cards.AutoSize = true;
            Control_LocationManagement_TableLayoutPanel_Cards.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_TableLayoutPanel_Cards.ColumnCount = 1;
            Control_LocationManagement_TableLayoutPanel_Cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_LocationManagement_TableLayoutPanel_Cards.Controls.Add(Control_LocationManagement_Panel_AddCard, 0, 0);
            Control_LocationManagement_TableLayoutPanel_Cards.Controls.Add(Control_LocationManagement_Panel_EditCard, 0, 1);
            Control_LocationManagement_TableLayoutPanel_Cards.Controls.Add(Control_LocationManagement_Panel_RemoveCard, 0, 2);
            Control_LocationManagement_TableLayoutPanel_Cards.Dock = DockStyle.Fill;
            Control_LocationManagement_TableLayoutPanel_Cards.Visible = false;
            Control_LocationManagement_TableLayoutPanel_Cards.RowCount = 3;
            Control_LocationManagement_TableLayoutPanel_Cards.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_Cards.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_Cards.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_Cards.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;

            // Add Card Panel
            Control_LocationManagement_TableLayoutPanel_Add.ColumnCount = 2;
            Control_LocationManagement_TableLayoutPanel_Add.ColumnStyles.Add(new ColumnStyle());
            Control_LocationManagement_TableLayoutPanel_Add.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_LocationManagement_TableLayoutPanel_Add.Controls.Add(Control_LocationManagement_Label_AddIcon, 0, 0);
            Control_LocationManagement_TableLayoutPanel_Add.Controls.Add(Control_LocationManagement_Label_AddTitle, 1, 0);
            Control_LocationManagement_TableLayoutPanel_Add.Controls.Add(Control_LocationManagement_TableLayoutPanel_AddContent, 0, 1);
            Control_LocationManagement_TableLayoutPanel_Add.SetColumnSpan(Control_LocationManagement_TableLayoutPanel_AddContent, 2);
            Control_LocationManagement_TableLayoutPanel_Add.AutoSize = true;
            Control_LocationManagement_TableLayoutPanel_Add.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_TableLayoutPanel_Add.Dock = DockStyle.Fill;
            Control_LocationManagement_TableLayoutPanel_Add.RowCount = 2;
            Control_LocationManagement_TableLayoutPanel_Add.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_Add.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_Add.Padding = new Padding(16);

            Control_LocationManagement_Label_AddIcon.AutoSize = true;
            Control_LocationManagement_Label_AddIcon.Font = new Font("Segoe UI Emoji", 28F, FontStyle.Regular, GraphicsUnit.Point);
            Control_LocationManagement_Label_AddIcon.Text = "🏢";
            Control_LocationManagement_Label_AddIcon.Margin = new Padding(0, 0, 12, 0);
            Control_LocationManagement_Label_AddIcon.TextAlign = ContentAlignment.MiddleCenter;

            Control_LocationManagement_Label_AddTitle.AutoSize = true;
            Control_LocationManagement_Label_AddTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            Control_LocationManagement_Label_AddTitle.Text = "Add Location";
            Control_LocationManagement_Label_AddTitle.TextAlign = ContentAlignment.MiddleLeft;

            Control_LocationManagement_TableLayoutPanel_AddContent.AutoSize = true;
            Control_LocationManagement_TableLayoutPanel_AddContent.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_TableLayoutPanel_AddContent.ColumnCount = 1;
            Control_LocationManagement_TableLayoutPanel_AddContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_LocationManagement_TableLayoutPanel_AddContent.Controls.Add(Control_LocationManagement_TextBox_AddLocation, 0, 0);
            Control_LocationManagement_TableLayoutPanel_AddContent.Controls.Add(Control_LocationManagement_ComboBox_AddBuilding, 0, 1);
            Control_LocationManagement_TableLayoutPanel_AddContent.Controls.Add(Control_LocationManagement_TableLayoutPanel_AddActions, 0, 2);
            Control_LocationManagement_TableLayoutPanel_AddContent.Dock = DockStyle.Fill;
            Control_LocationManagement_TableLayoutPanel_AddContent.RowCount = 3;
            Control_LocationManagement_TableLayoutPanel_AddContent.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_AddContent.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_AddContent.RowStyles.Add(new RowStyle());

            Control_LocationManagement_TextBox_AddLocation.Dock = DockStyle.Fill;
            Control_LocationManagement_TextBox_AddLocation.Margin = new Padding(3, 3, 3, 10);
            
            Control_LocationManagement_ComboBox_AddBuilding.Dock = DockStyle.Fill;
            Control_LocationManagement_ComboBox_AddBuilding.DropDownStyle = ComboBoxStyle.DropDownList;
            Control_LocationManagement_ComboBox_AddBuilding.Margin = new Padding(3, 3, 3, 10);

            Control_LocationManagement_TableLayoutPanel_AddActions.AutoSize = true;
            Control_LocationManagement_TableLayoutPanel_AddActions.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_TableLayoutPanel_AddActions.ColumnCount = 3;
            Control_LocationManagement_TableLayoutPanel_AddActions.ColumnStyles.Add(new ColumnStyle());
            Control_LocationManagement_TableLayoutPanel_AddActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_LocationManagement_TableLayoutPanel_AddActions.ColumnStyles.Add(new ColumnStyle());
            Control_LocationManagement_TableLayoutPanel_AddActions.Controls.Add(Control_LocationManagement_Button_AddSave, 0, 0);
            Control_LocationManagement_TableLayoutPanel_AddActions.Controls.Add(Control_LocationManagement_Button_AddClear, 2, 0);
            Control_LocationManagement_TableLayoutPanel_AddActions.Dock = DockStyle.Fill;
            Control_LocationManagement_TableLayoutPanel_AddActions.RowCount = 1;
            Control_LocationManagement_TableLayoutPanel_AddActions.RowStyles.Add(new RowStyle());

            Control_LocationManagement_Button_AddSave.Location = new Point(3, 3);
            Control_LocationManagement_Button_AddSave.Size = new Size(75, 23);

            Control_LocationManagement_Button_AddClear.Size = new Size(75, 23);

            Control_LocationManagement_Panel_AddCard.Controls.Add(Control_LocationManagement_TableLayoutPanel_Add);

            // Edit Card Panel
            Control_LocationManagement_TableLayoutPanel_Edit.ColumnCount = 2;
            Control_LocationManagement_TableLayoutPanel_Edit.ColumnStyles.Add(new ColumnStyle());
            Control_LocationManagement_TableLayoutPanel_Edit.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_LocationManagement_TableLayoutPanel_Edit.Controls.Add(Control_LocationManagement_Label_EditIcon, 0, 0);
            Control_LocationManagement_TableLayoutPanel_Edit.Controls.Add(Control_LocationManagement_Label_EditTitle, 1, 0);
            Control_LocationManagement_TableLayoutPanel_Edit.Controls.Add(Control_LocationManagement_TableLayoutPanel_EditContent, 0, 1);
            Control_LocationManagement_TableLayoutPanel_Edit.SetColumnSpan(Control_LocationManagement_TableLayoutPanel_EditContent, 2);
            Control_LocationManagement_TableLayoutPanel_Edit.AutoSize = true;
            Control_LocationManagement_TableLayoutPanel_Edit.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_TableLayoutPanel_Edit.Dock = DockStyle.Fill;
            Control_LocationManagement_TableLayoutPanel_Edit.RowCount = 2;
            Control_LocationManagement_TableLayoutPanel_Edit.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_Edit.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_Edit.Padding = new Padding(16);

            Control_LocationManagement_Label_EditIcon.AutoSize = true;
            Control_LocationManagement_Label_EditIcon.Font = new Font("Segoe UI Emoji", 28F);
            Control_LocationManagement_Label_EditIcon.Text = "✏️";
            Control_LocationManagement_Label_EditIcon.Margin = new Padding(0, 0, 12, 0);

            Control_LocationManagement_Label_EditTitle.AutoSize = true;
            Control_LocationManagement_Label_EditTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            Control_LocationManagement_Label_EditTitle.Text = "Edit Location";

            Control_LocationManagement_TableLayoutPanel_EditContent.AutoSize = true;
            Control_LocationManagement_TableLayoutPanel_EditContent.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_TableLayoutPanel_EditContent.ColumnCount = 1;
            Control_LocationManagement_TableLayoutPanel_EditContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_LocationManagement_TableLayoutPanel_EditContent.Controls.Add(Control_LocationManagement_Suggestion_EditSelectLocation, 0, 0);
            Control_LocationManagement_TableLayoutPanel_EditContent.Controls.Add(Control_LocationManagement_TextBox_EditNewLocation, 0, 1);
            Control_LocationManagement_TableLayoutPanel_EditContent.Controls.Add(Control_LocationManagement_ComboBox_EditBuilding, 0, 2);
            Control_LocationManagement_TableLayoutPanel_EditContent.Controls.Add(Control_LocationManagement_TableLayoutPanel_EditActions, 0, 3);
            Control_LocationManagement_TableLayoutPanel_EditContent.Dock = DockStyle.Fill;
            Control_LocationManagement_TableLayoutPanel_EditContent.RowCount = 4;
            Control_LocationManagement_TableLayoutPanel_EditContent.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_EditContent.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_EditContent.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_EditContent.RowStyles.Add(new RowStyle());

            Control_LocationManagement_Suggestion_EditSelectLocation.AutoSize = true;
            Control_LocationManagement_Suggestion_EditSelectLocation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_Suggestion_EditSelectLocation.BorderStyle = BorderStyle.FixedSingle;
            Control_LocationManagement_Suggestion_EditSelectLocation.Dock = DockStyle.Fill;
            Control_LocationManagement_Suggestion_EditSelectLocation.Margin = new Padding(3, 3, 3, 10);

            Control_LocationManagement_TextBox_EditNewLocation.Dock = DockStyle.Fill;
            Control_LocationManagement_TextBox_EditNewLocation.Margin = new Padding(3, 3, 3, 10);

            Control_LocationManagement_ComboBox_EditBuilding.Dock = DockStyle.Fill;
            Control_LocationManagement_ComboBox_EditBuilding.DropDownStyle = ComboBoxStyle.DropDownList;
            Control_LocationManagement_ComboBox_EditBuilding.Margin = new Padding(3, 3, 3, 10);

            Control_LocationManagement_TableLayoutPanel_EditActions.AutoSize = true;
            Control_LocationManagement_TableLayoutPanel_EditActions.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_TableLayoutPanel_EditActions.ColumnCount = 3;
            Control_LocationManagement_TableLayoutPanel_EditActions.ColumnStyles.Add(new ColumnStyle());
            Control_LocationManagement_TableLayoutPanel_EditActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_LocationManagement_TableLayoutPanel_EditActions.ColumnStyles.Add(new ColumnStyle());
            Control_LocationManagement_TableLayoutPanel_EditActions.Controls.Add(Control_LocationManagement_Button_EditSave, 0, 0);
            Control_LocationManagement_TableLayoutPanel_EditActions.Controls.Add(Control_LocationManagement_Button_EditReset, 2, 0);
            Control_LocationManagement_TableLayoutPanel_EditActions.Dock = DockStyle.Fill;
            Control_LocationManagement_TableLayoutPanel_EditActions.RowCount = 1;
            Control_LocationManagement_TableLayoutPanel_EditActions.RowStyles.Add(new RowStyle());

            Control_LocationManagement_Button_EditSave.Location = new Point(3, 3);
            Control_LocationManagement_Button_EditSave.Size = new Size(75, 23);

            Control_LocationManagement_Button_EditReset.Size = new Size(75, 23);

            Control_LocationManagement_Panel_EditCard.Controls.Add(Control_LocationManagement_TableLayoutPanel_Edit);

            // Remove Card Panel
            Control_LocationManagement_TableLayoutPanel_Remove.ColumnCount = 2;
            Control_LocationManagement_TableLayoutPanel_Remove.ColumnStyles.Add(new ColumnStyle());
            Control_LocationManagement_TableLayoutPanel_Remove.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_LocationManagement_TableLayoutPanel_Remove.Controls.Add(Control_LocationManagement_Label_RemoveIcon, 0, 0);
            Control_LocationManagement_TableLayoutPanel_Remove.Controls.Add(Control_LocationManagement_Label_RemoveTitle, 1, 0);
            Control_LocationManagement_TableLayoutPanel_Remove.Controls.Add(Control_LocationManagement_TableLayoutPanel_RemoveContent, 0, 1);
            Control_LocationManagement_TableLayoutPanel_Remove.SetColumnSpan(Control_LocationManagement_TableLayoutPanel_RemoveContent, 2);
            Control_LocationManagement_TableLayoutPanel_Remove.AutoSize = true;
            Control_LocationManagement_TableLayoutPanel_Remove.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_TableLayoutPanel_Remove.Dock = DockStyle.Fill;
            Control_LocationManagement_TableLayoutPanel_Remove.RowCount = 2;
            Control_LocationManagement_TableLayoutPanel_Remove.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_Remove.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_Remove.Padding = new Padding(16);

            Control_LocationManagement_Label_RemoveIcon.AutoSize = true;
            Control_LocationManagement_Label_RemoveIcon.Font = new Font("Segoe UI Emoji", 28F);
            Control_LocationManagement_Label_RemoveIcon.Text = "🗑️";
            Control_LocationManagement_Label_RemoveIcon.Margin = new Padding(0, 0, 12, 0);

            Control_LocationManagement_Label_RemoveTitle.AutoSize = true;
            Control_LocationManagement_Label_RemoveTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            Control_LocationManagement_Label_RemoveTitle.Text = "Remove Location";

            Control_LocationManagement_TableLayoutPanel_RemoveContent.AutoSize = true;
            Control_LocationManagement_TableLayoutPanel_RemoveContent.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_TableLayoutPanel_RemoveContent.ColumnCount = 1;
            Control_LocationManagement_TableLayoutPanel_RemoveContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_LocationManagement_TableLayoutPanel_RemoveContent.Controls.Add(Control_LocationManagement_Suggestion_RemoveSelectLocation, 0, 0);
            Control_LocationManagement_TableLayoutPanel_RemoveContent.Controls.Add(Control_LocationManagement_TableLayoutPanel_RemoveDetails, 0, 1);
            Control_LocationManagement_TableLayoutPanel_RemoveContent.Controls.Add(Control_LocationManagement_Label_RemoveWarning, 0, 2);
            Control_LocationManagement_TableLayoutPanel_RemoveContent.Controls.Add(Control_LocationManagement_TableLayoutPanel_RemoveActions, 0, 3);
            Control_LocationManagement_TableLayoutPanel_RemoveContent.Dock = DockStyle.Top;
            Control_LocationManagement_TableLayoutPanel_RemoveContent.RowCount = 4;
            Control_LocationManagement_TableLayoutPanel_RemoveContent.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_RemoveContent.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_RemoveContent.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_RemoveContent.RowStyles.Add(new RowStyle());


            Control_LocationManagement_TableLayoutPanel_RemoveDetails.AutoSize = true;
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.ColumnCount = 2;
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F));
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.Dock = DockStyle.Top;
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.RowCount = 3;
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.Controls.Add(Control_LocationManagement_Label_RemoveLocation, 0, 0);
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.Controls.Add(Control_LocationManagement_Label_RemoveLocationValue, 1, 0);
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.Controls.Add(Control_LocationManagement_Label_RemoveBuilding, 0, 1);
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.Controls.Add(Control_LocationManagement_Label_RemoveBuildingValue, 1, 1);
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.Controls.Add(Control_LocationManagement_Label_RemoveIssuedBy, 0, 2);
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.Controls.Add(Control_LocationManagement_Label_RemoveIssuedByValue, 1, 2);

            Control_LocationManagement_Label_RemoveWarning.AutoSize = true;
            Control_LocationManagement_Label_RemoveWarning.ForeColor = Color.FromArgb(192, 0, 0);
            Control_LocationManagement_Label_RemoveWarning.Text = "Warning: Removal is permanent.";
            Control_LocationManagement_Label_RemoveWarning.Margin = new Padding(0, 12, 0, 0);

            Control_LocationManagement_Suggestion_RemoveSelectLocation.AutoSize = true;
            Control_LocationManagement_Suggestion_RemoveSelectLocation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_Suggestion_RemoveSelectLocation.BorderStyle = BorderStyle.FixedSingle;
            Control_LocationManagement_Suggestion_RemoveSelectLocation.Dock = DockStyle.Fill;
            Control_LocationManagement_Suggestion_RemoveSelectLocation.Margin = new Padding(3, 3, 3, 10);

            Control_LocationManagement_TableLayoutPanel_RemoveActions.AutoSize = true;
            Control_LocationManagement_TableLayoutPanel_RemoveActions.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_TableLayoutPanel_RemoveActions.ColumnCount = 3;
            Control_LocationManagement_TableLayoutPanel_RemoveActions.ColumnStyles.Add(new ColumnStyle());
            Control_LocationManagement_TableLayoutPanel_RemoveActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_LocationManagement_TableLayoutPanel_RemoveActions.ColumnStyles.Add(new ColumnStyle());
            Control_LocationManagement_TableLayoutPanel_RemoveActions.Controls.Add(Control_LocationManagement_Button_RemoveConfirm, 0, 0);
            Control_LocationManagement_TableLayoutPanel_RemoveActions.Controls.Add(Control_LocationManagement_Button_RemoveCancel, 2, 0);
            Control_LocationManagement_TableLayoutPanel_RemoveActions.Dock = DockStyle.Fill;
            Control_LocationManagement_TableLayoutPanel_RemoveActions.RowCount = 1;
            Control_LocationManagement_TableLayoutPanel_RemoveActions.RowStyles.Add(new RowStyle());

            Control_LocationManagement_Button_RemoveConfirm.Location = new Point(3, 3);
            Control_LocationManagement_Button_RemoveConfirm.Size = new Size(75, 23);

            Control_LocationManagement_Button_RemoveCancel.Size = new Size(75, 23);

            Control_LocationManagement_Panel_RemoveCard.Controls.Add(Control_LocationManagement_TableLayoutPanel_Remove);

            // Arrange cards in table layout
            Control_LocationManagement_TableLayoutPanel_Cards.Controls.SetChildIndex(Control_LocationManagement_Panel_AddCard, 0);
            Control_LocationManagement_TableLayoutPanel_Cards.Controls.SetChildIndex(Control_LocationManagement_Panel_EditCard, 1);
            Control_LocationManagement_TableLayoutPanel_Cards.Controls.SetChildIndex(Control_LocationManagement_Panel_RemoveCard, 2);

            // Control_LocationManagement
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(Control_LocationManagement_TableLayoutPanel_Main);
            Name = "Control_LocationManagement";
            Size = new Size(700, 900);
            Control_LocationManagement_TableLayoutPanel_Main.ResumeLayout(false);
            Control_LocationManagement_TableLayoutPanel_Main.PerformLayout();
            Control_LocationManagement_TableLayoutPanel_Cards.ResumeLayout(false);
            Control_LocationManagement_Panel_AddCard.ResumeLayout(false);
            Control_LocationManagement_TableLayoutPanel_Add.ResumeLayout(false);
            Control_LocationManagement_TableLayoutPanel_Add.PerformLayout();
            Control_LocationManagement_TableLayoutPanel_AddContent.ResumeLayout(false);
            Control_LocationManagement_TableLayoutPanel_AddContent.PerformLayout();
            Control_LocationManagement_TableLayoutPanel_AddActions.ResumeLayout(false);
            Control_LocationManagement_Panel_EditCard.ResumeLayout(false);
            Control_LocationManagement_TableLayoutPanel_Edit.ResumeLayout(false);
            Control_LocationManagement_TableLayoutPanel_Edit.PerformLayout();
            Control_LocationManagement_TableLayoutPanel_EditContent.ResumeLayout(false);
            Control_LocationManagement_TableLayoutPanel_EditContent.PerformLayout();
            Control_LocationManagement_TableLayoutPanel_EditActions.ResumeLayout(false);
            Control_LocationManagement_Panel_RemoveCard.ResumeLayout(false);
            Control_LocationManagement_TableLayoutPanel_Remove.ResumeLayout(false);
            Control_LocationManagement_TableLayoutPanel_Remove.PerformLayout();
            Control_LocationManagement_TableLayoutPanel_RemoveContent.ResumeLayout(false);
            Control_LocationManagement_TableLayoutPanel_RemoveContent.PerformLayout();
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.ResumeLayout(false);
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.PerformLayout();
            Control_LocationManagement_TableLayoutPanel_RemoveActions.ResumeLayout(false);
            Control_LocationManagement_TableLayoutPanel_Home.ResumeLayout(false);
            Control_LocationManagement_TableLayoutPanel_Home.PerformLayout();
            Control_LocationManagement_Panel_Home.ResumeLayout(false);
            Control_LocationManagement_Panel_Home.PerformLayout();
            Control_LocationManagement_TableLayoutPanel_BackButton.ResumeLayout(false);
            Control_LocationManagement_TableLayoutPanel_BackButton.PerformLayout();
            Control_LocationManagement_Panel_Container.ResumeLayout(false);
            Control_LocationManagement_Panel_Container.PerformLayout();
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
