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
        private TableLayoutPanel Control_LocationManagement_TableLayout_HomeTile_Add = null!;
        private Label Control_LocationManagement_Label_HomeTile_AddIcon = null!;
        private Label Control_LocationManagement_Label_HomeTile_AddTitle = null!;
        private Label Control_LocationManagement_Label_HomeTile_AddInstruction = null!;
        private Panel Control_LocationManagement_Panel_HomeTile_Edit = null!;
        private TableLayoutPanel Control_LocationManagement_TableLayout_HomeTile_Edit = null!;
        private Label Control_LocationManagement_Label_HomeTile_EditIcon = null!;
        private Label Control_LocationManagement_Label_HomeTile_EditTitle = null!;
        private Label Control_LocationManagement_Label_HomeTile_EditInstruction = null!;
        private Panel Control_LocationManagement_Panel_HomeTile_Remove = null!;
        private TableLayoutPanel Control_LocationManagement_TableLayout_HomeTile_Remove = null!;
        private Label Control_LocationManagement_Label_HomeTile_RemoveIcon = null!;
        private Label Control_LocationManagement_Label_HomeTile_RemoveTitle = null!;
        private Label Control_LocationManagement_Label_HomeTile_RemoveInstruction = null!;
        
        private TableLayoutPanel Control_LocationManagement_TableLayoutPanel_Cards = null!;
        private TableLayoutPanel Control_LocationManagement_TableLayoutPanel_BackButton = null!;
        private Button Control_LocationManagement_Button_Back = null!;

        private Panel Control_LocationManagement_Panel_AddCard = null!;
        private TableLayoutPanel Control_LocationManagement_TableLayoutPanel_Add = null!;
        private Label Control_LocationManagement_Label_AddIcon = null!;
        private Label Control_LocationManagement_Label_AddTitle = null!;
        private TableLayoutPanel Control_LocationManagement_TableLayoutPanel_AddContent = null!;
        private SuggestionTextBoxWithLabel Control_LocationManagement_TextBox_AddLocation = null!;
        private SuggestionTextBoxWithLabel Control_LocationManagement_ComboBox_AddBuilding = null!;
        private Label Control_LocationManagement_Label_AddIssuedBy = null!;
        private Label Control_LocationManagement_Label_AddIssuedByValue = null!;
        private TableLayoutPanel Control_LocationManagement_TableLayoutPanel_AddActions = null!;
        private Button Control_LocationManagement_Button_AddSave = null!;
        private Button Control_LocationManagement_Button_AddClear = null!;

        private Panel Control_LocationManagement_Panel_EditCard = null!;
        private TableLayoutPanel Control_LocationManagement_TableLayoutPanel_Edit = null!;
        private Label Control_LocationManagement_Label_EditIcon = null!;
        private Label Control_LocationManagement_Label_EditTitle = null!;
        private SuggestionTextBoxWithLabel Control_LocationManagement_Suggestion_EditSelectLocation = null!;
        private SuggestionTextBoxWithLabel Control_LocationManagement_TextBox_EditNewLocation = null!;
        private SuggestionTextBoxWithLabel Control_LocationManagement_ComboBox_EditBuilding = null!;
        private Label Control_LocationManagement_Label_EditIssuedBy = null!;
        private Label Control_LocationManagement_Label_EditIssuedByValue = null!;

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
            Control_LocationManagement_TableLayoutPanel_Main = new TableLayoutPanel();
            Control_LocationManagement_Label_Header = new Label();
            Control_LocationManagement_Label_Subtitle = new Label();
            Control_LocationManagement_Panel_Container = new Panel();
            Control_LocationManagement_Panel_Home = new Panel();
            Control_LocationManagement_TableLayoutPanel_Home = new TableLayoutPanel();
            Control_LocationManagement_Panel_HomeTile_Add = new Panel();
            Control_LocationManagement_TableLayout_HomeTile_Add = new TableLayoutPanel();
            Control_LocationManagement_Label_HomeTile_AddIcon = new Label();
            Control_LocationManagement_Label_HomeTile_AddTitle = new Label();
            Control_LocationManagement_Label_HomeTile_AddInstruction = new Label();
            Control_LocationManagement_Panel_HomeTile_Remove = new Panel();
            Control_LocationManagement_TableLayout_HomeTile_Remove = new TableLayoutPanel();
            Control_LocationManagement_Label_HomeTile_RemoveIcon = new Label();
            Control_LocationManagement_Label_HomeTile_RemoveTitle = new Label();
            Control_LocationManagement_Label_HomeTile_RemoveInstruction = new Label();
            Control_LocationManagement_Panel_HomeTile_Edit = new Panel();
            Control_LocationManagement_TableLayout_HomeTile_Edit = new TableLayoutPanel();
            Control_LocationManagement_Label_HomeTile_EditIcon = new Label();
            Control_LocationManagement_Label_HomeTile_EditTitle = new Label();
            Control_LocationManagement_Label_HomeTile_EditInstruction = new Label();
            Control_LocationManagement_TableLayoutPanel_Cards = new TableLayoutPanel();
            Control_LocationManagement_Panel_AddCard = new Panel();
            Control_LocationManagement_TableLayoutPanel_Add = new TableLayoutPanel();
            tableLayoutPanel1 = new TableLayoutPanel();
            Control_LocationManagement_Label_AddIcon = new Label();
            Control_LocationManagement_Label_AddTitle = new Label();
            Control_LocationManagement_TableLayoutPanel_AddContent = new TableLayoutPanel();
            label1 = new Label();
            Control_LocationManagement_ComboBox_AddBuilding = new SuggestionTextBoxWithLabel();
            Control_LocationManagement_TextBox_AddLocation = new SuggestionTextBoxWithLabel();
            label2 = new Label();
            Control_LocationManagement_TableLayoutPanel_AddActions = new TableLayoutPanel();
            Control_LocationManagement_Button_AddSave = new Button();
            Control_LocationManagement_Button_AddClear = new Button();
            Control_LocationManagement_Panel_EditCard = new Panel();
            Control_LocationManagement_TableLayoutPanel_Edit = new TableLayoutPanel();
            Control_LocationManagement_TableLayoutPanel_EditContent = new TableLayoutPanel();
            Control_LocationManagement_TableLayoutPanel_EditActions = new TableLayoutPanel();
            Control_LocationManagement_Button_EditSave = new Button();
            Control_LocationManagement_Button_EditReset = new Button();
            Control_LocationManagement_ComboBox_EditBuilding = new SuggestionTextBoxWithLabel();
            Control_LocationManagement_TextBox_EditNewLocation = new SuggestionTextBoxWithLabel();
            Control_LocationManagement_Suggestion_EditSelectLocation = new SuggestionTextBoxWithLabel();
            label3 = new Label();
            label4 = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            Control_LocationManagement_Label_EditIcon = new Label();
            Control_LocationManagement_Label_EditTitle = new Label();
            Control_LocationManagement_Panel_RemoveCard = new Panel();
            Control_LocationManagement_TableLayoutPanel_Remove = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            Control_LocationManagement_Label_RemoveTitle = new Label();
            Control_LocationManagement_Label_RemoveIcon = new Label();
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
            Control_LocationManagement_TableLayoutPanel_BackButton = new TableLayoutPanel();
            Control_LocationManagement_Button_Back = new Button();
            Control_LocationManagement_Label_AddIssuedBy = new Label();
            Control_LocationManagement_Label_AddIssuedByValue = new Label();
            Control_LocationManagement_Label_EditIssuedBy = new Label();
            Control_LocationManagement_Label_EditIssuedByValue = new Label();
            Control_LocationManagement_Button_Home = new Button();
            Control_LocationManagement_TableLayoutPanel_Main.SuspendLayout();
            Control_LocationManagement_Panel_Container.SuspendLayout();
            Control_LocationManagement_Panel_Home.SuspendLayout();
            Control_LocationManagement_TableLayoutPanel_Home.SuspendLayout();
            Control_LocationManagement_Panel_HomeTile_Add.SuspendLayout();
            Control_LocationManagement_TableLayout_HomeTile_Add.SuspendLayout();
            Control_LocationManagement_Panel_HomeTile_Remove.SuspendLayout();
            Control_LocationManagement_TableLayout_HomeTile_Remove.SuspendLayout();
            Control_LocationManagement_Panel_HomeTile_Edit.SuspendLayout();
            Control_LocationManagement_TableLayout_HomeTile_Edit.SuspendLayout();
            Control_LocationManagement_TableLayoutPanel_Cards.SuspendLayout();
            Control_LocationManagement_Panel_AddCard.SuspendLayout();
            Control_LocationManagement_TableLayoutPanel_Add.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            Control_LocationManagement_TableLayoutPanel_AddContent.SuspendLayout();
            Control_LocationManagement_TableLayoutPanel_AddActions.SuspendLayout();
            Control_LocationManagement_Panel_EditCard.SuspendLayout();
            Control_LocationManagement_TableLayoutPanel_Edit.SuspendLayout();
            Control_LocationManagement_TableLayoutPanel_EditContent.SuspendLayout();
            Control_LocationManagement_TableLayoutPanel_EditActions.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            Control_LocationManagement_Panel_RemoveCard.SuspendLayout();
            Control_LocationManagement_TableLayoutPanel_Remove.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            Control_LocationManagement_TableLayoutPanel_RemoveContent.SuspendLayout();
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.SuspendLayout();
            Control_LocationManagement_TableLayoutPanel_RemoveActions.SuspendLayout();
            Control_LocationManagement_TableLayoutPanel_BackButton.SuspendLayout();
            SuspendLayout();
            // 
            // Control_LocationManagement_TableLayoutPanel_Main
            // 
            Control_LocationManagement_TableLayoutPanel_Main.AutoSize = true;
            Control_LocationManagement_TableLayoutPanel_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_TableLayoutPanel_Main.ColumnCount = 1;
            Control_LocationManagement_TableLayoutPanel_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_LocationManagement_TableLayoutPanel_Main.Controls.Add(Control_LocationManagement_Label_Header, 0, 0);
            Control_LocationManagement_TableLayoutPanel_Main.Controls.Add(Control_LocationManagement_Label_Subtitle, 0, 1);
            Control_LocationManagement_TableLayoutPanel_Main.Controls.Add(Control_LocationManagement_Panel_Container, 0, 2);
            Control_LocationManagement_TableLayoutPanel_Main.Controls.Add(Control_LocationManagement_TableLayoutPanel_BackButton, 0, 3);
            Control_LocationManagement_TableLayoutPanel_Main.Dock = DockStyle.Fill;
            Control_LocationManagement_TableLayoutPanel_Main.Location = new Point(0, 0);
            Control_LocationManagement_TableLayoutPanel_Main.Name = "Control_LocationManagement_TableLayoutPanel_Main";
            Control_LocationManagement_TableLayoutPanel_Main.Padding = new Padding(20);
            Control_LocationManagement_TableLayoutPanel_Main.RowCount = 4;
            Control_LocationManagement_TableLayoutPanel_Main.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_Main.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_LocationManagement_TableLayoutPanel_Main.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_Main.Size = new Size(492, 389);
            Control_LocationManagement_TableLayoutPanel_Main.TabIndex = 0;
            // 
            // Control_LocationManagement_Label_Header
            // 
            Control_LocationManagement_Label_Header.AutoSize = true;
            Control_LocationManagement_Label_Header.Dock = DockStyle.Fill;
            Control_LocationManagement_Label_Header.Font = new Font("Segoe UI Emoji", 20F, FontStyle.Bold);
            Control_LocationManagement_Label_Header.ForeColor = Color.FromArgb(45, 45, 45);
            Control_LocationManagement_Label_Header.Location = new Point(23, 23);
            Control_LocationManagement_Label_Header.Margin = new Padding(3);
            Control_LocationManagement_Label_Header.Name = "Control_LocationManagement_Label_Header";
            Control_LocationManagement_Label_Header.Size = new Size(446, 37);
            Control_LocationManagement_Label_Header.TabIndex = 0;
            Control_LocationManagement_Label_Header.Text = "Locations";
            // 
            // Control_LocationManagement_Label_Subtitle
            // 
            Control_LocationManagement_Label_Subtitle.AutoSize = true;
            Control_LocationManagement_Label_Subtitle.Dock = DockStyle.Fill;
            Control_LocationManagement_Label_Subtitle.Font = new Font("Segoe UI Emoji", 10F);
            Control_LocationManagement_Label_Subtitle.ForeColor = Color.FromArgb(90, 90, 90);
            Control_LocationManagement_Label_Subtitle.Location = new Point(23, 66);
            Control_LocationManagement_Label_Subtitle.Margin = new Padding(3);
            Control_LocationManagement_Label_Subtitle.Name = "Control_LocationManagement_Label_Subtitle";
            Control_LocationManagement_Label_Subtitle.Size = new Size(446, 19);
            Control_LocationManagement_Label_Subtitle.TabIndex = 1;
            Control_LocationManagement_Label_Subtitle.Text = "Select an action below to manage locations.";
            // 
            // Control_LocationManagement_Panel_Container
            // 
            Control_LocationManagement_Panel_Container.Controls.Add(Control_LocationManagement_Panel_Home);
            Control_LocationManagement_Panel_Container.Controls.Add(Control_LocationManagement_TableLayoutPanel_Cards);
            Control_LocationManagement_Panel_Container.Dock = DockStyle.Fill;
            Control_LocationManagement_Panel_Container.Location = new Point(23, 91);
            Control_LocationManagement_Panel_Container.Name = "Control_LocationManagement_Panel_Container";
            Control_LocationManagement_Panel_Container.Size = new Size(446, 226);
            Control_LocationManagement_Panel_Container.TabIndex = 2;
            // 
            // Control_LocationManagement_Panel_Home
            // 
            Control_LocationManagement_Panel_Home.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_Panel_Home.Controls.Add(Control_LocationManagement_TableLayoutPanel_Home);
            Control_LocationManagement_Panel_Home.Dock = DockStyle.Fill;
            Control_LocationManagement_Panel_Home.Location = new Point(0, 0);
            Control_LocationManagement_Panel_Home.Name = "Control_LocationManagement_Panel_Home";
            Control_LocationManagement_Panel_Home.Size = new Size(446, 226);
            Control_LocationManagement_Panel_Home.TabIndex = 0;
            // 
            // Control_LocationManagement_TableLayoutPanel_Home
            // 
            Control_LocationManagement_TableLayoutPanel_Home.ColumnCount = 3;
            Control_LocationManagement_TableLayoutPanel_Home.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            Control_LocationManagement_TableLayoutPanel_Home.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            Control_LocationManagement_TableLayoutPanel_Home.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
            Control_LocationManagement_TableLayoutPanel_Home.Controls.Add(Control_LocationManagement_Panel_HomeTile_Add, 0, 0);
            Control_LocationManagement_TableLayoutPanel_Home.Controls.Add(Control_LocationManagement_Panel_HomeTile_Remove, 2, 0);
            Control_LocationManagement_TableLayoutPanel_Home.Controls.Add(Control_LocationManagement_Panel_HomeTile_Edit, 1, 0);
            Control_LocationManagement_TableLayoutPanel_Home.Dock = DockStyle.Fill;
            Control_LocationManagement_TableLayoutPanel_Home.Location = new Point(0, 0);
            Control_LocationManagement_TableLayoutPanel_Home.Name = "Control_LocationManagement_TableLayoutPanel_Home";
            Control_LocationManagement_TableLayoutPanel_Home.RowCount = 1;
            Control_LocationManagement_TableLayoutPanel_Home.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_Home.Size = new Size(446, 226);
            Control_LocationManagement_TableLayoutPanel_Home.TabIndex = 0;
            // 
            // Control_LocationManagement_Panel_HomeTile_Add
            // 
            Control_LocationManagement_Panel_HomeTile_Add.AutoSize = true;
            Control_LocationManagement_Panel_HomeTile_Add.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_Panel_HomeTile_Add.BorderStyle = BorderStyle.FixedSingle;
            Control_LocationManagement_Panel_HomeTile_Add.Controls.Add(Control_LocationManagement_TableLayout_HomeTile_Add);
            Control_LocationManagement_Panel_HomeTile_Add.Cursor = Cursors.Hand;
            Control_LocationManagement_Panel_HomeTile_Add.Dock = DockStyle.Fill;
            Control_LocationManagement_Panel_HomeTile_Add.Location = new Point(3, 3);
            Control_LocationManagement_Panel_HomeTile_Add.Name = "Control_LocationManagement_Panel_HomeTile_Add";
            Control_LocationManagement_Panel_HomeTile_Add.Size = new Size(142, 220);
            Control_LocationManagement_Panel_HomeTile_Add.TabIndex = 0;
            // 
            // Control_LocationManagement_TableLayout_HomeTile_Add
            // 
            Control_LocationManagement_TableLayout_HomeTile_Add.AutoSize = true;
            Control_LocationManagement_TableLayout_HomeTile_Add.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_TableLayout_HomeTile_Add.ColumnCount = 1;
            Control_LocationManagement_TableLayout_HomeTile_Add.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_LocationManagement_TableLayout_HomeTile_Add.Controls.Add(Control_LocationManagement_Label_HomeTile_AddIcon, 0, 0);
            Control_LocationManagement_TableLayout_HomeTile_Add.Controls.Add(Control_LocationManagement_Label_HomeTile_AddTitle, 0, 1);
            Control_LocationManagement_TableLayout_HomeTile_Add.Controls.Add(Control_LocationManagement_Label_HomeTile_AddInstruction, 0, 3);
            Control_LocationManagement_TableLayout_HomeTile_Add.Dock = DockStyle.Fill;
            Control_LocationManagement_TableLayout_HomeTile_Add.Location = new Point(0, 0);
            Control_LocationManagement_TableLayout_HomeTile_Add.Name = "Control_LocationManagement_TableLayout_HomeTile_Add";
            Control_LocationManagement_TableLayout_HomeTile_Add.Padding = new Padding(3);
            Control_LocationManagement_TableLayout_HomeTile_Add.RowCount = 5;
            Control_LocationManagement_TableLayout_HomeTile_Add.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayout_HomeTile_Add.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayout_HomeTile_Add.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Control_LocationManagement_TableLayout_HomeTile_Add.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayout_HomeTile_Add.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Control_LocationManagement_TableLayout_HomeTile_Add.Size = new Size(140, 218);
            Control_LocationManagement_TableLayout_HomeTile_Add.TabIndex = 0;
            // 
            // Control_LocationManagement_Label_HomeTile_AddIcon
            // 
            Control_LocationManagement_Label_HomeTile_AddIcon.AutoSize = true;
            Control_LocationManagement_Label_HomeTile_AddIcon.Dock = DockStyle.Fill;
            Control_LocationManagement_Label_HomeTile_AddIcon.Font = new Font("Segoe UI Emoji", 48F);
            Control_LocationManagement_Label_HomeTile_AddIcon.Location = new Point(6, 6);
            Control_LocationManagement_Label_HomeTile_AddIcon.Margin = new Padding(3);
            Control_LocationManagement_Label_HomeTile_AddIcon.Name = "Control_LocationManagement_Label_HomeTile_AddIcon";
            Control_LocationManagement_Label_HomeTile_AddIcon.Size = new Size(128, 85);
            Control_LocationManagement_Label_HomeTile_AddIcon.TabIndex = 0;
            Control_LocationManagement_Label_HomeTile_AddIcon.Text = "🆕";
            Control_LocationManagement_Label_HomeTile_AddIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_LocationManagement_Label_HomeTile_AddTitle
            // 
            Control_LocationManagement_Label_HomeTile_AddTitle.AutoSize = true;
            Control_LocationManagement_Label_HomeTile_AddTitle.Dock = DockStyle.Fill;
            Control_LocationManagement_Label_HomeTile_AddTitle.Font = new Font("Segoe UI Emoji", 14F, FontStyle.Bold);
            Control_LocationManagement_Label_HomeTile_AddTitle.Location = new Point(6, 97);
            Control_LocationManagement_Label_HomeTile_AddTitle.Margin = new Padding(3);
            Control_LocationManagement_Label_HomeTile_AddTitle.Name = "Control_LocationManagement_Label_HomeTile_AddTitle";
            Control_LocationManagement_Label_HomeTile_AddTitle.Size = new Size(128, 50);
            Control_LocationManagement_Label_HomeTile_AddTitle.TabIndex = 1;
            Control_LocationManagement_Label_HomeTile_AddTitle.Text = "Add\r\nLocation";
            Control_LocationManagement_Label_HomeTile_AddTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_LocationManagement_Label_HomeTile_AddInstruction
            // 
            Control_LocationManagement_Label_HomeTile_AddInstruction.AutoSize = true;
            Control_LocationManagement_Label_HomeTile_AddInstruction.Dock = DockStyle.Fill;
            Control_LocationManagement_Label_HomeTile_AddInstruction.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Italic);
            Control_LocationManagement_Label_HomeTile_AddInstruction.ForeColor = Color.FromArgb(100, 100, 100);
            Control_LocationManagement_Label_HomeTile_AddInstruction.Location = new Point(6, 167);
            Control_LocationManagement_Label_HomeTile_AddInstruction.Margin = new Padding(3);
            Control_LocationManagement_Label_HomeTile_AddInstruction.Name = "Control_LocationManagement_Label_HomeTile_AddInstruction";
            Control_LocationManagement_Label_HomeTile_AddInstruction.Size = new Size(128, 30);
            Control_LocationManagement_Label_HomeTile_AddInstruction.TabIndex = 2;
            Control_LocationManagement_Label_HomeTile_AddInstruction.Text = "Click to add new locations";
            Control_LocationManagement_Label_HomeTile_AddInstruction.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_LocationManagement_Panel_HomeTile_Remove
            // 
            Control_LocationManagement_Panel_HomeTile_Remove.AutoSize = true;
            Control_LocationManagement_Panel_HomeTile_Remove.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_Panel_HomeTile_Remove.BorderStyle = BorderStyle.FixedSingle;
            Control_LocationManagement_Panel_HomeTile_Remove.Controls.Add(Control_LocationManagement_TableLayout_HomeTile_Remove);
            Control_LocationManagement_Panel_HomeTile_Remove.Cursor = Cursors.Hand;
            Control_LocationManagement_Panel_HomeTile_Remove.Dock = DockStyle.Fill;
            Control_LocationManagement_Panel_HomeTile_Remove.Location = new Point(299, 3);
            Control_LocationManagement_Panel_HomeTile_Remove.Name = "Control_LocationManagement_Panel_HomeTile_Remove";
            Control_LocationManagement_Panel_HomeTile_Remove.Size = new Size(144, 220);
            Control_LocationManagement_Panel_HomeTile_Remove.TabIndex = 2;
            // 
            // Control_LocationManagement_TableLayout_HomeTile_Remove
            // 
            Control_LocationManagement_TableLayout_HomeTile_Remove.AutoSize = true;
            Control_LocationManagement_TableLayout_HomeTile_Remove.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_TableLayout_HomeTile_Remove.ColumnCount = 1;
            Control_LocationManagement_TableLayout_HomeTile_Remove.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_LocationManagement_TableLayout_HomeTile_Remove.Controls.Add(Control_LocationManagement_Label_HomeTile_RemoveIcon, 0, 0);
            Control_LocationManagement_TableLayout_HomeTile_Remove.Controls.Add(Control_LocationManagement_Label_HomeTile_RemoveTitle, 0, 1);
            Control_LocationManagement_TableLayout_HomeTile_Remove.Controls.Add(Control_LocationManagement_Label_HomeTile_RemoveInstruction, 0, 3);
            Control_LocationManagement_TableLayout_HomeTile_Remove.Dock = DockStyle.Fill;
            Control_LocationManagement_TableLayout_HomeTile_Remove.Location = new Point(0, 0);
            Control_LocationManagement_TableLayout_HomeTile_Remove.Name = "Control_LocationManagement_TableLayout_HomeTile_Remove";
            Control_LocationManagement_TableLayout_HomeTile_Remove.Padding = new Padding(3);
            Control_LocationManagement_TableLayout_HomeTile_Remove.RowCount = 5;
            Control_LocationManagement_TableLayout_HomeTile_Remove.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayout_HomeTile_Remove.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayout_HomeTile_Remove.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Control_LocationManagement_TableLayout_HomeTile_Remove.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayout_HomeTile_Remove.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Control_LocationManagement_TableLayout_HomeTile_Remove.Size = new Size(142, 218);
            Control_LocationManagement_TableLayout_HomeTile_Remove.TabIndex = 0;
            // 
            // Control_LocationManagement_Label_HomeTile_RemoveIcon
            // 
            Control_LocationManagement_Label_HomeTile_RemoveIcon.AutoSize = true;
            Control_LocationManagement_Label_HomeTile_RemoveIcon.Dock = DockStyle.Fill;
            Control_LocationManagement_Label_HomeTile_RemoveIcon.Font = new Font("Segoe UI Emoji", 48F);
            Control_LocationManagement_Label_HomeTile_RemoveIcon.Location = new Point(6, 6);
            Control_LocationManagement_Label_HomeTile_RemoveIcon.Margin = new Padding(3);
            Control_LocationManagement_Label_HomeTile_RemoveIcon.Name = "Control_LocationManagement_Label_HomeTile_RemoveIcon";
            Control_LocationManagement_Label_HomeTile_RemoveIcon.Size = new Size(130, 85);
            Control_LocationManagement_Label_HomeTile_RemoveIcon.TabIndex = 0;
            Control_LocationManagement_Label_HomeTile_RemoveIcon.Text = "🗑️";
            Control_LocationManagement_Label_HomeTile_RemoveIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_LocationManagement_Label_HomeTile_RemoveTitle
            // 
            Control_LocationManagement_Label_HomeTile_RemoveTitle.AutoSize = true;
            Control_LocationManagement_Label_HomeTile_RemoveTitle.Dock = DockStyle.Fill;
            Control_LocationManagement_Label_HomeTile_RemoveTitle.Font = new Font("Segoe UI Emoji", 14F, FontStyle.Bold);
            Control_LocationManagement_Label_HomeTile_RemoveTitle.Location = new Point(6, 97);
            Control_LocationManagement_Label_HomeTile_RemoveTitle.Margin = new Padding(3);
            Control_LocationManagement_Label_HomeTile_RemoveTitle.Name = "Control_LocationManagement_Label_HomeTile_RemoveTitle";
            Control_LocationManagement_Label_HomeTile_RemoveTitle.Size = new Size(130, 50);
            Control_LocationManagement_Label_HomeTile_RemoveTitle.TabIndex = 1;
            Control_LocationManagement_Label_HomeTile_RemoveTitle.Text = "Remove\r\nLocation";
            Control_LocationManagement_Label_HomeTile_RemoveTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_LocationManagement_Label_HomeTile_RemoveInstruction
            // 
            Control_LocationManagement_Label_HomeTile_RemoveInstruction.AutoSize = true;
            Control_LocationManagement_Label_HomeTile_RemoveInstruction.Dock = DockStyle.Fill;
            Control_LocationManagement_Label_HomeTile_RemoveInstruction.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Italic);
            Control_LocationManagement_Label_HomeTile_RemoveInstruction.ForeColor = Color.FromArgb(100, 100, 100);
            Control_LocationManagement_Label_HomeTile_RemoveInstruction.Location = new Point(6, 167);
            Control_LocationManagement_Label_HomeTile_RemoveInstruction.Margin = new Padding(3);
            Control_LocationManagement_Label_HomeTile_RemoveInstruction.Name = "Control_LocationManagement_Label_HomeTile_RemoveInstruction";
            Control_LocationManagement_Label_HomeTile_RemoveInstruction.Size = new Size(130, 30);
            Control_LocationManagement_Label_HomeTile_RemoveInstruction.TabIndex = 2;
            Control_LocationManagement_Label_HomeTile_RemoveInstruction.Text = "Click to remove locations";
            Control_LocationManagement_Label_HomeTile_RemoveInstruction.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_LocationManagement_Panel_HomeTile_Edit
            // 
            Control_LocationManagement_Panel_HomeTile_Edit.AutoSize = true;
            Control_LocationManagement_Panel_HomeTile_Edit.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_Panel_HomeTile_Edit.BorderStyle = BorderStyle.FixedSingle;
            Control_LocationManagement_Panel_HomeTile_Edit.Controls.Add(Control_LocationManagement_TableLayout_HomeTile_Edit);
            Control_LocationManagement_Panel_HomeTile_Edit.Cursor = Cursors.Hand;
            Control_LocationManagement_Panel_HomeTile_Edit.Dock = DockStyle.Fill;
            Control_LocationManagement_Panel_HomeTile_Edit.Location = new Point(151, 3);
            Control_LocationManagement_Panel_HomeTile_Edit.Name = "Control_LocationManagement_Panel_HomeTile_Edit";
            Control_LocationManagement_Panel_HomeTile_Edit.Size = new Size(142, 220);
            Control_LocationManagement_Panel_HomeTile_Edit.TabIndex = 1;
            // 
            // Control_LocationManagement_TableLayout_HomeTile_Edit
            // 
            Control_LocationManagement_TableLayout_HomeTile_Edit.AutoSize = true;
            Control_LocationManagement_TableLayout_HomeTile_Edit.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_TableLayout_HomeTile_Edit.ColumnCount = 1;
            Control_LocationManagement_TableLayout_HomeTile_Edit.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_LocationManagement_TableLayout_HomeTile_Edit.Controls.Add(Control_LocationManagement_Label_HomeTile_EditIcon, 0, 0);
            Control_LocationManagement_TableLayout_HomeTile_Edit.Controls.Add(Control_LocationManagement_Label_HomeTile_EditTitle, 0, 1);
            Control_LocationManagement_TableLayout_HomeTile_Edit.Controls.Add(Control_LocationManagement_Label_HomeTile_EditInstruction, 0, 3);
            Control_LocationManagement_TableLayout_HomeTile_Edit.Dock = DockStyle.Fill;
            Control_LocationManagement_TableLayout_HomeTile_Edit.Location = new Point(0, 0);
            Control_LocationManagement_TableLayout_HomeTile_Edit.Name = "Control_LocationManagement_TableLayout_HomeTile_Edit";
            Control_LocationManagement_TableLayout_HomeTile_Edit.Padding = new Padding(3);
            Control_LocationManagement_TableLayout_HomeTile_Edit.RowCount = 5;
            Control_LocationManagement_TableLayout_HomeTile_Edit.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayout_HomeTile_Edit.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayout_HomeTile_Edit.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Control_LocationManagement_TableLayout_HomeTile_Edit.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayout_HomeTile_Edit.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Control_LocationManagement_TableLayout_HomeTile_Edit.Size = new Size(140, 218);
            Control_LocationManagement_TableLayout_HomeTile_Edit.TabIndex = 0;
            // 
            // Control_LocationManagement_Label_HomeTile_EditIcon
            // 
            Control_LocationManagement_Label_HomeTile_EditIcon.AutoSize = true;
            Control_LocationManagement_Label_HomeTile_EditIcon.Dock = DockStyle.Fill;
            Control_LocationManagement_Label_HomeTile_EditIcon.Font = new Font("Segoe UI Emoji", 48F);
            Control_LocationManagement_Label_HomeTile_EditIcon.Location = new Point(6, 6);
            Control_LocationManagement_Label_HomeTile_EditIcon.Margin = new Padding(3);
            Control_LocationManagement_Label_HomeTile_EditIcon.Name = "Control_LocationManagement_Label_HomeTile_EditIcon";
            Control_LocationManagement_Label_HomeTile_EditIcon.Size = new Size(128, 85);
            Control_LocationManagement_Label_HomeTile_EditIcon.TabIndex = 0;
            Control_LocationManagement_Label_HomeTile_EditIcon.Text = "✏️";
            Control_LocationManagement_Label_HomeTile_EditIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_LocationManagement_Label_HomeTile_EditTitle
            // 
            Control_LocationManagement_Label_HomeTile_EditTitle.AutoSize = true;
            Control_LocationManagement_Label_HomeTile_EditTitle.Dock = DockStyle.Fill;
            Control_LocationManagement_Label_HomeTile_EditTitle.Font = new Font("Segoe UI Emoji", 14F, FontStyle.Bold);
            Control_LocationManagement_Label_HomeTile_EditTitle.Location = new Point(6, 97);
            Control_LocationManagement_Label_HomeTile_EditTitle.Margin = new Padding(3);
            Control_LocationManagement_Label_HomeTile_EditTitle.Name = "Control_LocationManagement_Label_HomeTile_EditTitle";
            Control_LocationManagement_Label_HomeTile_EditTitle.Size = new Size(128, 50);
            Control_LocationManagement_Label_HomeTile_EditTitle.TabIndex = 1;
            Control_LocationManagement_Label_HomeTile_EditTitle.Text = "Edit\r\nLocation";
            Control_LocationManagement_Label_HomeTile_EditTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_LocationManagement_Label_HomeTile_EditInstruction
            // 
            Control_LocationManagement_Label_HomeTile_EditInstruction.AutoSize = true;
            Control_LocationManagement_Label_HomeTile_EditInstruction.Dock = DockStyle.Fill;
            Control_LocationManagement_Label_HomeTile_EditInstruction.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Italic);
            Control_LocationManagement_Label_HomeTile_EditInstruction.ForeColor = Color.FromArgb(100, 100, 100);
            Control_LocationManagement_Label_HomeTile_EditInstruction.Location = new Point(6, 167);
            Control_LocationManagement_Label_HomeTile_EditInstruction.Margin = new Padding(3);
            Control_LocationManagement_Label_HomeTile_EditInstruction.Name = "Control_LocationManagement_Label_HomeTile_EditInstruction";
            Control_LocationManagement_Label_HomeTile_EditInstruction.Size = new Size(128, 30);
            Control_LocationManagement_Label_HomeTile_EditInstruction.TabIndex = 2;
            Control_LocationManagement_Label_HomeTile_EditInstruction.Text = "Click to edit existing locations";
            Control_LocationManagement_Label_HomeTile_EditInstruction.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_LocationManagement_TableLayoutPanel_Cards
            // 
            Control_LocationManagement_TableLayoutPanel_Cards.AutoSize = true;
            Control_LocationManagement_TableLayoutPanel_Cards.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_TableLayoutPanel_Cards.ColumnCount = 1;
            Control_LocationManagement_TableLayoutPanel_Cards.ColumnStyles.Add(new ColumnStyle());
            Control_LocationManagement_TableLayoutPanel_Cards.Controls.Add(Control_LocationManagement_Panel_AddCard, 0, 0);
            Control_LocationManagement_TableLayoutPanel_Cards.Controls.Add(Control_LocationManagement_Panel_EditCard, 0, 1);
            Control_LocationManagement_TableLayoutPanel_Cards.Controls.Add(Control_LocationManagement_Panel_RemoveCard, 0, 2);
            Control_LocationManagement_TableLayoutPanel_Cards.Dock = DockStyle.Fill;
            Control_LocationManagement_TableLayoutPanel_Cards.Location = new Point(0, 0);
            Control_LocationManagement_TableLayoutPanel_Cards.Name = "Control_LocationManagement_TableLayoutPanel_Cards";
            Control_LocationManagement_TableLayoutPanel_Cards.RowCount = 3;
            Control_LocationManagement_TableLayoutPanel_Cards.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_Cards.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_Cards.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_Cards.Size = new Size(446, 226);
            Control_LocationManagement_TableLayoutPanel_Cards.TabIndex = 1;
            Control_LocationManagement_TableLayoutPanel_Cards.Visible = false;
            // 
            // Control_LocationManagement_Panel_AddCard
            // 
            Control_LocationManagement_Panel_AddCard.AutoSize = true;
            Control_LocationManagement_Panel_AddCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_Panel_AddCard.BorderStyle = BorderStyle.FixedSingle;
            Control_LocationManagement_Panel_AddCard.Controls.Add(Control_LocationManagement_TableLayoutPanel_Add);
            Control_LocationManagement_Panel_AddCard.Dock = DockStyle.Fill;
            Control_LocationManagement_Panel_AddCard.Location = new Point(3, 3);
            Control_LocationManagement_Panel_AddCard.Name = "Control_LocationManagement_Panel_AddCard";
            Control_LocationManagement_Panel_AddCard.Size = new Size(440, 196);
            Control_LocationManagement_Panel_AddCard.TabIndex = 0;
            // 
            // Control_LocationManagement_TableLayoutPanel_Add
            // 
            Control_LocationManagement_TableLayoutPanel_Add.AutoSize = true;
            Control_LocationManagement_TableLayoutPanel_Add.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_TableLayoutPanel_Add.ColumnCount = 1;
            Control_LocationManagement_TableLayoutPanel_Add.ColumnStyles.Add(new ColumnStyle());
            Control_LocationManagement_TableLayoutPanel_Add.Controls.Add(tableLayoutPanel1, 0, 0);
            Control_LocationManagement_TableLayoutPanel_Add.Controls.Add(Control_LocationManagement_TableLayoutPanel_AddContent, 0, 1);
            Control_LocationManagement_TableLayoutPanel_Add.Dock = DockStyle.Fill;
            Control_LocationManagement_TableLayoutPanel_Add.Location = new Point(0, 0);
            Control_LocationManagement_TableLayoutPanel_Add.Name = "Control_LocationManagement_TableLayoutPanel_Add";
            Control_LocationManagement_TableLayoutPanel_Add.Padding = new Padding(16);
            Control_LocationManagement_TableLayoutPanel_Add.RowCount = 2;
            Control_LocationManagement_TableLayoutPanel_Add.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_Add.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_Add.Size = new Size(438, 194);
            Control_LocationManagement_TableLayoutPanel_Add.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(Control_LocationManagement_Label_AddIcon, 0, 0);
            tableLayoutPanel1.Controls.Add(Control_LocationManagement_Label_AddTitle, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(19, 19);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(400, 57);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // Control_LocationManagement_Label_AddIcon
            // 
            Control_LocationManagement_Label_AddIcon.AutoSize = true;
            Control_LocationManagement_Label_AddIcon.Dock = DockStyle.Fill;
            Control_LocationManagement_Label_AddIcon.Font = new Font("Segoe UI Emoji", 28F);
            Control_LocationManagement_Label_AddIcon.Location = new Point(3, 3);
            Control_LocationManagement_Label_AddIcon.Margin = new Padding(3);
            Control_LocationManagement_Label_AddIcon.Name = "Control_LocationManagement_Label_AddIcon";
            Control_LocationManagement_Label_AddIcon.Size = new Size(74, 51);
            Control_LocationManagement_Label_AddIcon.TabIndex = 0;
            Control_LocationManagement_Label_AddIcon.Text = "🏢";
            Control_LocationManagement_Label_AddIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_LocationManagement_Label_AddTitle
            // 
            Control_LocationManagement_Label_AddTitle.AutoSize = true;
            Control_LocationManagement_Label_AddTitle.Dock = DockStyle.Fill;
            Control_LocationManagement_Label_AddTitle.Font = new Font("Segoe UI Emoji", 16F, FontStyle.Bold);
            Control_LocationManagement_Label_AddTitle.Location = new Point(83, 3);
            Control_LocationManagement_Label_AddTitle.Margin = new Padding(3);
            Control_LocationManagement_Label_AddTitle.Name = "Control_LocationManagement_Label_AddTitle";
            Control_LocationManagement_Label_AddTitle.Size = new Size(314, 51);
            Control_LocationManagement_Label_AddTitle.TabIndex = 1;
            Control_LocationManagement_Label_AddTitle.Text = "Add Location";
            Control_LocationManagement_Label_AddTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_LocationManagement_TableLayoutPanel_AddContent
            // 
            Control_LocationManagement_TableLayoutPanel_AddContent.AutoSize = true;
            Control_LocationManagement_TableLayoutPanel_AddContent.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_TableLayoutPanel_AddContent.ColumnCount = 1;
            Control_LocationManagement_TableLayoutPanel_Add.SetColumnSpan(Control_LocationManagement_TableLayoutPanel_AddContent, 2);
            Control_LocationManagement_TableLayoutPanel_AddContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_LocationManagement_TableLayoutPanel_AddContent.Controls.Add(Control_LocationManagement_ComboBox_AddBuilding, 0, 1);
            Control_LocationManagement_TableLayoutPanel_AddContent.Controls.Add(Control_LocationManagement_TextBox_AddLocation, 0, 0);
            Control_LocationManagement_TableLayoutPanel_AddContent.Controls.Add(Control_LocationManagement_TableLayoutPanel_AddActions, 0, 2);
            Control_LocationManagement_TableLayoutPanel_AddContent.Dock = DockStyle.Fill;
            Control_LocationManagement_TableLayoutPanel_AddContent.Location = new Point(19, 82);
            Control_LocationManagement_TableLayoutPanel_AddContent.Name = "Control_LocationManagement_TableLayoutPanel_AddContent";
            Control_LocationManagement_TableLayoutPanel_AddContent.RowCount = 3;
            Control_LocationManagement_TableLayoutPanel_AddContent.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_AddContent.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_AddContent.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_AddContent.Size = new Size(400, 93);
            Control_LocationManagement_TableLayoutPanel_AddContent.TabIndex = 2;
            // 
            // Control_LocationManagement_ComboBox_AddBuilding
            // 
            Control_LocationManagement_ComboBox_AddBuilding.Dock = DockStyle.Fill;
            Control_LocationManagement_ComboBox_AddBuilding.Location = new Point(3, 40);
            Control_LocationManagement_ComboBox_AddBuilding.Name = "Control_LocationManagement_ComboBox_AddBuilding";
            Control_LocationManagement_ComboBox_AddBuilding.Size = new Size(394, 31);
            Control_LocationManagement_ComboBox_AddBuilding.TabIndex = 1;
            Control_LocationManagement_ComboBox_AddBuilding.Padding = new Padding(3);
            // 
            // Control_LocationManagement_TextBox_AddLocation
            // 
            Control_LocationManagement_TextBox_AddLocation.Dock = DockStyle.Fill;
            Control_LocationManagement_TextBox_AddLocation.Location = new Point(3, 3);
            Control_LocationManagement_TextBox_AddLocation.Name = "Control_LocationManagement_TextBox_AddLocation";
            Control_LocationManagement_TextBox_AddLocation.Size = new Size(394, 31);
            Control_LocationManagement_TextBox_AddLocation.TabIndex = 0;
            Control_LocationManagement_TextBox_AddLocation.Padding = new Padding(3);
            // 
            // Control_LocationManagement_TableLayoutPanel_AddActions
            // 
            Control_LocationManagement_TableLayoutPanel_AddActions.AutoSize = true;
            Control_LocationManagement_TableLayoutPanel_AddActions.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_TableLayoutPanel_AddActions.ColumnCount = 3;
            Control_LocationManagement_TableLayoutPanel_AddContent.SetColumnSpan(Control_LocationManagement_TableLayoutPanel_AddActions, 2);
            Control_LocationManagement_TableLayoutPanel_AddActions.ColumnStyles.Add(new ColumnStyle());
            Control_LocationManagement_TableLayoutPanel_AddActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_LocationManagement_TableLayoutPanel_AddActions.ColumnStyles.Add(new ColumnStyle());
            Control_LocationManagement_TableLayoutPanel_AddActions.Controls.Add(Control_LocationManagement_Button_AddSave, 0, 0);
            Control_LocationManagement_TableLayoutPanel_AddActions.Controls.Add(Control_LocationManagement_Button_AddClear, 2, 0);
            Control_LocationManagement_TableLayoutPanel_AddActions.Dock = DockStyle.Fill;
            Control_LocationManagement_TableLayoutPanel_AddActions.Location = new Point(3, 61);
            Control_LocationManagement_TableLayoutPanel_AddActions.Name = "Control_LocationManagement_TableLayoutPanel_AddActions";
            Control_LocationManagement_TableLayoutPanel_AddActions.RowCount = 1;
            Control_LocationManagement_TableLayoutPanel_AddActions.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_AddActions.Size = new Size(394, 29);
            Control_LocationManagement_TableLayoutPanel_AddActions.TabIndex = 2;
            // 
            // Control_LocationManagement_Button_AddSave
            // 
            Control_LocationManagement_Button_AddSave.AutoSize = true;
            Control_LocationManagement_Button_AddSave.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_Button_AddSave.Location = new Point(3, 3);
            Control_LocationManagement_Button_AddSave.MinimumSize = new Size(75, 23);
            Control_LocationManagement_Button_AddSave.Name = "Control_LocationManagement_Button_AddSave";
            Control_LocationManagement_Button_AddSave.Size = new Size(75, 23);
            Control_LocationManagement_Button_AddSave.TabIndex = 0;
            // 
            // Control_LocationManagement_Button_AddClear
            // 
            Control_LocationManagement_Button_AddClear.AutoSize = true;
            Control_LocationManagement_Button_AddClear.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_Button_AddClear.Location = new Point(316, 3);
            Control_LocationManagement_Button_AddClear.MinimumSize = new Size(75, 23);
            Control_LocationManagement_Button_AddClear.Name = "Control_LocationManagement_Button_AddClear";
            Control_LocationManagement_Button_AddClear.Size = new Size(75, 23);
            Control_LocationManagement_Button_AddClear.TabIndex = 1;
            // 
            // Control_LocationManagement_Panel_EditCard
            // 
            Control_LocationManagement_Panel_EditCard.AutoSize = true;
            Control_LocationManagement_Panel_EditCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_Panel_EditCard.BorderStyle = BorderStyle.FixedSingle;
            Control_LocationManagement_Panel_EditCard.Controls.Add(Control_LocationManagement_TableLayoutPanel_Edit);
            Control_LocationManagement_Panel_EditCard.Dock = DockStyle.Fill;
            Control_LocationManagement_Panel_EditCard.Location = new Point(3, 205);
            Control_LocationManagement_Panel_EditCard.Name = "Control_LocationManagement_Panel_EditCard";
            Control_LocationManagement_Panel_EditCard.Size = new Size(440, 221);
            Control_LocationManagement_Panel_EditCard.TabIndex = 1;
            // 
            // Control_LocationManagement_TableLayoutPanel_Edit
            // 
            Control_LocationManagement_TableLayoutPanel_Edit.AutoSize = true;
            Control_LocationManagement_TableLayoutPanel_Edit.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_TableLayoutPanel_Edit.ColumnCount = 1;
            Control_LocationManagement_TableLayoutPanel_Edit.ColumnStyles.Add(new ColumnStyle());
            Control_LocationManagement_TableLayoutPanel_Edit.Controls.Add(Control_LocationManagement_TableLayoutPanel_EditContent, 0, 1);
            Control_LocationManagement_TableLayoutPanel_Edit.Controls.Add(tableLayoutPanel2, 0, 0);
            Control_LocationManagement_TableLayoutPanel_Edit.Dock = DockStyle.Fill;
            Control_LocationManagement_TableLayoutPanel_Edit.Location = new Point(0, 0);
            Control_LocationManagement_TableLayoutPanel_Edit.Name = "Control_LocationManagement_TableLayoutPanel_Edit";
            Control_LocationManagement_TableLayoutPanel_Edit.Padding = new Padding(16);
            Control_LocationManagement_TableLayoutPanel_Edit.RowCount = 2;
            Control_LocationManagement_TableLayoutPanel_Edit.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_Edit.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_Edit.Size = new Size(438, 219);
            Control_LocationManagement_TableLayoutPanel_Edit.TabIndex = 0;
            // 
            // Control_LocationManagement_TableLayoutPanel_EditContent
            // 
            Control_LocationManagement_TableLayoutPanel_EditContent.AutoSize = true;
            Control_LocationManagement_TableLayoutPanel_EditContent.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_TableLayoutPanel_EditContent.ColumnCount = 1;
            Control_LocationManagement_TableLayoutPanel_Edit.SetColumnSpan(Control_LocationManagement_TableLayoutPanel_EditContent, 2);
            Control_LocationManagement_TableLayoutPanel_EditContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_LocationManagement_TableLayoutPanel_EditContent.Controls.Add(Control_LocationManagement_TableLayoutPanel_EditActions, 0, 3);
            Control_LocationManagement_TableLayoutPanel_EditContent.Controls.Add(Control_LocationManagement_ComboBox_EditBuilding, 0, 2);
            Control_LocationManagement_TableLayoutPanel_EditContent.Controls.Add(Control_LocationManagement_TextBox_EditNewLocation, 0, 1);
            Control_LocationManagement_TableLayoutPanel_EditContent.Controls.Add(Control_LocationManagement_Suggestion_EditSelectLocation, 0, 0);
            Control_LocationManagement_TableLayoutPanel_EditContent.Dock = DockStyle.Fill;
            Control_LocationManagement_TableLayoutPanel_EditContent.Location = new Point(19, 82);
            Control_LocationManagement_TableLayoutPanel_EditContent.Name = "Control_LocationManagement_TableLayoutPanel_EditContent";
            Control_LocationManagement_TableLayoutPanel_EditContent.RowCount = 4;
            Control_LocationManagement_TableLayoutPanel_EditContent.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_EditContent.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_EditContent.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_EditContent.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_EditContent.Size = new Size(400, 118);
            Control_LocationManagement_TableLayoutPanel_EditContent.TabIndex = 2;
            // 
            // Control_LocationManagement_TableLayoutPanel_EditActions
            // 
            Control_LocationManagement_TableLayoutPanel_EditActions.AutoSize = true;
            Control_LocationManagement_TableLayoutPanel_EditActions.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_TableLayoutPanel_EditActions.ColumnCount = 3;
            Control_LocationManagement_TableLayoutPanel_EditContent.SetColumnSpan(Control_LocationManagement_TableLayoutPanel_EditActions, 1);
            Control_LocationManagement_TableLayoutPanel_EditActions.ColumnStyles.Add(new ColumnStyle());
            Control_LocationManagement_TableLayoutPanel_EditActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_LocationManagement_TableLayoutPanel_EditActions.ColumnStyles.Add(new ColumnStyle());
            Control_LocationManagement_TableLayoutPanel_EditActions.Controls.Add(Control_LocationManagement_Button_EditSave, 0, 0);
            Control_LocationManagement_TableLayoutPanel_EditActions.Controls.Add(Control_LocationManagement_Button_EditReset, 2, 0);
            Control_LocationManagement_TableLayoutPanel_EditActions.Dock = DockStyle.Fill;
            Control_LocationManagement_TableLayoutPanel_EditActions.Location = new Point(0, 89);
            Control_LocationManagement_TableLayoutPanel_EditActions.Margin = new Padding(0);
            Control_LocationManagement_TableLayoutPanel_EditActions.Name = "Control_LocationManagement_TableLayoutPanel_EditActions";
            Control_LocationManagement_TableLayoutPanel_EditActions.RowCount = 1;
            Control_LocationManagement_TableLayoutPanel_EditActions.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_EditActions.Size = new Size(400, 29);
            Control_LocationManagement_TableLayoutPanel_EditActions.TabIndex = 3;
            // 
            // Control_LocationManagement_Button_EditSave
            // 
            Control_LocationManagement_Button_EditSave.AutoSize = true;
            Control_LocationManagement_Button_EditSave.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_Button_EditSave.Location = new Point(3, 3);
            Control_LocationManagement_Button_EditSave.MinimumSize = new Size(75, 23);
            Control_LocationManagement_Button_EditSave.Name = "Control_LocationManagement_Button_EditSave";
            Control_LocationManagement_Button_EditSave.Size = new Size(75, 23);
            Control_LocationManagement_Button_EditSave.TabIndex = 0;
            // 
            // Control_LocationManagement_Button_EditReset
            // 
            Control_LocationManagement_Button_EditReset.AutoSize = true;
            Control_LocationManagement_Button_EditReset.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_Button_EditReset.Location = new Point(322, 3);
            Control_LocationManagement_Button_EditReset.MinimumSize = new Size(75, 23);
            Control_LocationManagement_Button_EditReset.Name = "Control_LocationManagement_Button_EditReset";
            Control_LocationManagement_Button_EditReset.Size = new Size(75, 23);
            Control_LocationManagement_Button_EditReset.TabIndex = 1;
            // 
            // Control_LocationManagement_ComboBox_EditBuilding
            // 
            Control_LocationManagement_ComboBox_EditBuilding.Dock = DockStyle.Fill;
            Control_LocationManagement_ComboBox_EditBuilding.Location = new Point(3, 77);
            Control_LocationManagement_ComboBox_EditBuilding.Name = "Control_LocationManagement_ComboBox_EditBuilding";
            Control_LocationManagement_ComboBox_EditBuilding.Size = new Size(394, 31);
            Control_LocationManagement_ComboBox_EditBuilding.TabIndex = 2;
            Control_LocationManagement_ComboBox_EditBuilding.Padding = new Padding(3);
            // 
            // Control_LocationManagement_TextBox_EditNewLocation
            // 
            Control_LocationManagement_TextBox_EditNewLocation.Dock = DockStyle.Fill;
            Control_LocationManagement_TextBox_EditNewLocation.Location = new Point(3, 40);
            Control_LocationManagement_TextBox_EditNewLocation.Name = "Control_LocationManagement_TextBox_EditNewLocation";
            Control_LocationManagement_TextBox_EditNewLocation.Size = new Size(394, 31);
            Control_LocationManagement_TextBox_EditNewLocation.TabIndex = 1;
            Control_LocationManagement_TextBox_EditNewLocation.Padding = new Padding(3);
            // 
            // Control_LocationManagement_Suggestion_EditSelectLocation
            // 
            Control_LocationManagement_Suggestion_EditSelectLocation.AutoSize = true;
            Control_LocationManagement_Suggestion_EditSelectLocation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_Suggestion_EditSelectLocation.BorderStyle = BorderStyle.FixedSingle;
            Control_LocationManagement_TableLayoutPanel_EditContent.SetColumnSpan(Control_LocationManagement_Suggestion_EditSelectLocation, 1);
            Control_LocationManagement_Suggestion_EditSelectLocation.Dock = DockStyle.Fill;
            Control_LocationManagement_Suggestion_EditSelectLocation.Location = new Point(0, 0);
            Control_LocationManagement_Suggestion_EditSelectLocation.Margin = new Padding(0);
            Control_LocationManagement_Suggestion_EditSelectLocation.Name = "Control_LocationManagement_Suggestion_EditSelectLocation";
            Control_LocationManagement_Suggestion_EditSelectLocation.Padding = new Padding(3);
            Control_LocationManagement_Suggestion_EditSelectLocation.Size = new Size(400, 31);
            Control_LocationManagement_Suggestion_EditSelectLocation.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(Control_LocationManagement_Label_EditIcon, 0, 0);
            tableLayoutPanel2.Controls.Add(Control_LocationManagement_Label_EditTitle, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(19, 19);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(400, 57);
            tableLayoutPanel2.TabIndex = 3;
            // 
            // Control_LocationManagement_Label_EditIcon
            // 
            Control_LocationManagement_Label_EditIcon.AutoSize = true;
            Control_LocationManagement_Label_EditIcon.Dock = DockStyle.Fill;
            Control_LocationManagement_Label_EditIcon.Font = new Font("Segoe UI Emoji", 28F);
            Control_LocationManagement_Label_EditIcon.Location = new Point(3, 3);
            Control_LocationManagement_Label_EditIcon.Margin = new Padding(3);
            Control_LocationManagement_Label_EditIcon.Name = "Control_LocationManagement_Label_EditIcon";
            Control_LocationManagement_Label_EditIcon.Size = new Size(74, 51);
            Control_LocationManagement_Label_EditIcon.TabIndex = 0;
            Control_LocationManagement_Label_EditIcon.Text = "✏️";
            Control_LocationManagement_Label_EditIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_LocationManagement_Label_EditTitle
            // 
            Control_LocationManagement_Label_EditTitle.AutoSize = true;
            Control_LocationManagement_Label_EditTitle.Dock = DockStyle.Fill;
            Control_LocationManagement_Label_EditTitle.Font = new Font("Segoe UI Emoji", 16F, FontStyle.Bold);
            Control_LocationManagement_Label_EditTitle.Location = new Point(83, 3);
            Control_LocationManagement_Label_EditTitle.Margin = new Padding(3);
            Control_LocationManagement_Label_EditTitle.Name = "Control_LocationManagement_Label_EditTitle";
            Control_LocationManagement_Label_EditTitle.Size = new Size(314, 51);
            Control_LocationManagement_Label_EditTitle.TabIndex = 1;
            Control_LocationManagement_Label_EditTitle.Text = "Edit Location";
            Control_LocationManagement_Label_EditTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_LocationManagement_Panel_RemoveCard
            // 
            Control_LocationManagement_Panel_RemoveCard.AutoSize = true;
            Control_LocationManagement_Panel_RemoveCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_Panel_RemoveCard.BorderStyle = BorderStyle.FixedSingle;
            Control_LocationManagement_Panel_RemoveCard.Controls.Add(Control_LocationManagement_TableLayoutPanel_Remove);
            Control_LocationManagement_Panel_RemoveCard.Dock = DockStyle.Fill;
            Control_LocationManagement_Panel_RemoveCard.Location = new Point(3, 432);
            Control_LocationManagement_Panel_RemoveCard.Name = "Control_LocationManagement_Panel_RemoveCard";
            Control_LocationManagement_Panel_RemoveCard.Size = new Size(440, 251);
            Control_LocationManagement_Panel_RemoveCard.TabIndex = 2;
            // 
            // Control_LocationManagement_TableLayoutPanel_Remove
            // 
            Control_LocationManagement_TableLayoutPanel_Remove.AutoSize = true;
            Control_LocationManagement_TableLayoutPanel_Remove.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_TableLayoutPanel_Remove.ColumnCount = 2;
            Control_LocationManagement_TableLayoutPanel_Remove.ColumnStyles.Add(new ColumnStyle());
            Control_LocationManagement_TableLayoutPanel_Remove.ColumnStyles.Add(new ColumnStyle());
            Control_LocationManagement_TableLayoutPanel_Remove.Controls.Add(tableLayoutPanel3, 0, 0);
            Control_LocationManagement_TableLayoutPanel_Remove.Controls.Add(Control_LocationManagement_TableLayoutPanel_RemoveContent, 0, 1);
            Control_LocationManagement_TableLayoutPanel_Remove.Dock = DockStyle.Fill;
            Control_LocationManagement_TableLayoutPanel_Remove.Location = new Point(0, 0);
            Control_LocationManagement_TableLayoutPanel_Remove.Name = "Control_LocationManagement_TableLayoutPanel_Remove";
            Control_LocationManagement_TableLayoutPanel_Remove.Padding = new Padding(16);
            Control_LocationManagement_TableLayoutPanel_Remove.RowCount = 2;
            Control_LocationManagement_TableLayoutPanel_Remove.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_Remove.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_Remove.Size = new Size(438, 249);
            Control_LocationManagement_TableLayoutPanel_Remove.TabIndex = 0;
            // 
            // Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeHeader
            // 
            tableLayoutPanel3.AutoSize = true;
            tableLayoutPanel3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel3.ColumnCount = 2;
            Control_LocationManagement_TableLayoutPanel_Remove.SetColumnSpan(tableLayoutPanel3, 2);
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.Controls.Add(Control_LocationManagement_Label_RemoveTitle, 1, 0);
            tableLayoutPanel3.Controls.Add(Control_LocationManagement_Label_RemoveIcon, 0, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(16, 16);
            tableLayoutPanel3.Margin = new Padding(0);
            tableLayoutPanel3.Name = "Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeHeader";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.Size = new Size(406, 57);
            tableLayoutPanel3.TabIndex = 1;
            // 
            // Control_LocationManagement_Label_RemoveTitle
            // 
            Control_LocationManagement_Label_RemoveTitle.AutoSize = true;
            Control_LocationManagement_Label_RemoveTitle.Dock = DockStyle.Fill;
            Control_LocationManagement_Label_RemoveTitle.Font = new Font("Segoe UI Emoji", 16F, FontStyle.Bold);
            Control_LocationManagement_Label_RemoveTitle.Location = new Point(83, 3);
            Control_LocationManagement_Label_RemoveTitle.Margin = new Padding(3);
            Control_LocationManagement_Label_RemoveTitle.Name = "Control_LocationManagement_Label_RemoveTitle";
            Control_LocationManagement_Label_RemoveTitle.Size = new Size(320, 51);
            Control_LocationManagement_Label_RemoveTitle.TabIndex = 1;
            Control_LocationManagement_Label_RemoveTitle.Text = "Remove Location";
            Control_LocationManagement_Label_RemoveTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_LocationManagement_Label_RemoveIcon
            // 
            Control_LocationManagement_Label_RemoveIcon.AutoSize = true;
            Control_LocationManagement_Label_RemoveIcon.Dock = DockStyle.Fill;
            Control_LocationManagement_Label_RemoveIcon.Font = new Font("Segoe UI Emoji", 28F);
            Control_LocationManagement_Label_RemoveIcon.Location = new Point(3, 3);
            Control_LocationManagement_Label_RemoveIcon.Margin = new Padding(3);
            Control_LocationManagement_Label_RemoveIcon.Name = "Control_LocationManagement_Label_RemoveIcon";
            Control_LocationManagement_Label_RemoveIcon.Size = new Size(74, 51);
            Control_LocationManagement_Label_RemoveIcon.TabIndex = 0;
            Control_LocationManagement_Label_RemoveIcon.Text = "🗑️";
            Control_LocationManagement_Label_RemoveIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_LocationManagement_TableLayoutPanel_RemoveContent
            // 
            Control_LocationManagement_TableLayoutPanel_RemoveContent.AutoSize = true;
            Control_LocationManagement_TableLayoutPanel_RemoveContent.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_TableLayoutPanel_RemoveContent.ColumnCount = 1;
            Control_LocationManagement_TableLayoutPanel_Remove.SetColumnSpan(Control_LocationManagement_TableLayoutPanel_RemoveContent, 2);
            Control_LocationManagement_TableLayoutPanel_RemoveContent.ColumnStyles.Add(new ColumnStyle());
            Control_LocationManagement_TableLayoutPanel_RemoveContent.Controls.Add(Control_LocationManagement_Suggestion_RemoveSelectLocation, 0, 0);
            Control_LocationManagement_TableLayoutPanel_RemoveContent.Controls.Add(Control_LocationManagement_TableLayoutPanel_RemoveDetails, 0, 1);
            Control_LocationManagement_TableLayoutPanel_RemoveContent.Controls.Add(Control_LocationManagement_Label_RemoveWarning, 0, 2);
            Control_LocationManagement_TableLayoutPanel_RemoveContent.Controls.Add(Control_LocationManagement_TableLayoutPanel_RemoveActions, 0, 4);
            Control_LocationManagement_TableLayoutPanel_RemoveContent.Dock = DockStyle.Fill;
            Control_LocationManagement_TableLayoutPanel_RemoveContent.Location = new Point(19, 76);
            Control_LocationManagement_TableLayoutPanel_RemoveContent.Name = "Control_LocationManagement_TableLayoutPanel_RemoveContent";
            Control_LocationManagement_TableLayoutPanel_RemoveContent.RowCount = 5;
            Control_LocationManagement_TableLayoutPanel_RemoveContent.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_RemoveContent.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_RemoveContent.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_RemoveContent.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_RemoveContent.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_RemoveContent.Size = new Size(400, 154);
            Control_LocationManagement_TableLayoutPanel_RemoveContent.TabIndex = 2;
            // 
            // Control_LocationManagement_Suggestion_RemoveSelectLocation
            // 
            Control_LocationManagement_Suggestion_RemoveSelectLocation.AutoSize = true;
            Control_LocationManagement_Suggestion_RemoveSelectLocation.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_Suggestion_RemoveSelectLocation.BorderStyle = BorderStyle.FixedSingle;
            Control_LocationManagement_Suggestion_RemoveSelectLocation.Dock = DockStyle.Fill;
            Control_LocationManagement_Suggestion_RemoveSelectLocation.Location = new Point(0, 0);
            Control_LocationManagement_Suggestion_RemoveSelectLocation.Margin = new Padding(0);
            Control_LocationManagement_Suggestion_RemoveSelectLocation.Name = "Control_LocationManagement_Suggestion_RemoveSelectLocation";
            Control_LocationManagement_Suggestion_RemoveSelectLocation.Padding = new Padding(3);
            Control_LocationManagement_Suggestion_RemoveSelectLocation.Size = new Size(400, 31);
            Control_LocationManagement_Suggestion_RemoveSelectLocation.TabIndex = 0;
            // 
            // Control_LocationManagement_TableLayoutPanel_RemoveDetails
            // 
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.AutoSize = true;
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.ColumnCount = 2;
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.ColumnStyles.Add(new ColumnStyle());
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.ColumnStyles.Add(new ColumnStyle());
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.Controls.Add(Control_LocationManagement_Label_RemoveLocation, 0, 0);
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.Controls.Add(Control_LocationManagement_Label_RemoveLocationValue, 1, 0);
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.Controls.Add(Control_LocationManagement_Label_RemoveBuilding, 0, 1);
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.Controls.Add(Control_LocationManagement_Label_RemoveBuildingValue, 1, 1);
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.Controls.Add(Control_LocationManagement_Label_RemoveIssuedBy, 0, 2);
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.Controls.Add(Control_LocationManagement_Label_RemoveIssuedByValue, 1, 2);
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.Dock = DockStyle.Fill;
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.Location = new Point(3, 34);
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.Name = "Control_LocationManagement_TableLayoutPanel_RemoveDetails";
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.RowCount = 3;
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.RowStyles.Add(new RowStyle());
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.Size = new Size(394, 67);
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.TabIndex = 1;
            // 
            // Control_LocationManagement_Label_RemoveLocation
            // 
            Control_LocationManagement_Label_RemoveLocation.AutoSize = true;
            Control_LocationManagement_Label_RemoveLocation.Dock = DockStyle.Fill;
            Control_LocationManagement_Label_RemoveLocation.Location = new Point(4, 4);
            Control_LocationManagement_Label_RemoveLocation.Margin = new Padding(3);
            Control_LocationManagement_Label_RemoveLocation.Name = "Control_LocationManagement_Label_RemoveLocation";
            Control_LocationManagement_Label_RemoveLocation.Size = new Size(59, 15);
            Control_LocationManagement_Label_RemoveLocation.TabIndex = 0;
            Control_LocationManagement_Label_RemoveLocation.Text = "Location:";
            Control_LocationManagement_Label_RemoveLocation.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Control_LocationManagement_Label_RemoveLocationValue
            // 
            Control_LocationManagement_Label_RemoveLocationValue.AutoSize = true;
            Control_LocationManagement_Label_RemoveLocationValue.Dock = DockStyle.Fill;
            Control_LocationManagement_Label_RemoveLocationValue.Location = new Point(70, 4);
            Control_LocationManagement_Label_RemoveLocationValue.Margin = new Padding(3);
            Control_LocationManagement_Label_RemoveLocationValue.Name = "Control_LocationManagement_Label_RemoveLocationValue";
            Control_LocationManagement_Label_RemoveLocationValue.Size = new Size(320, 15);
            Control_LocationManagement_Label_RemoveLocationValue.TabIndex = 1;
            Control_LocationManagement_Label_RemoveLocationValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_LocationManagement_Label_RemoveBuilding
            // 
            Control_LocationManagement_Label_RemoveBuilding.AutoSize = true;
            Control_LocationManagement_Label_RemoveBuilding.Dock = DockStyle.Fill;
            Control_LocationManagement_Label_RemoveBuilding.Location = new Point(4, 26);
            Control_LocationManagement_Label_RemoveBuilding.Margin = new Padding(3);
            Control_LocationManagement_Label_RemoveBuilding.Name = "Control_LocationManagement_Label_RemoveBuilding";
            Control_LocationManagement_Label_RemoveBuilding.Size = new Size(59, 15);
            Control_LocationManagement_Label_RemoveBuilding.TabIndex = 2;
            Control_LocationManagement_Label_RemoveBuilding.Text = "Building:";
            Control_LocationManagement_Label_RemoveBuilding.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Control_LocationManagement_Label_RemoveBuildingValue
            // 
            Control_LocationManagement_Label_RemoveBuildingValue.AutoSize = true;
            Control_LocationManagement_Label_RemoveBuildingValue.Dock = DockStyle.Fill;
            Control_LocationManagement_Label_RemoveBuildingValue.Location = new Point(70, 26);
            Control_LocationManagement_Label_RemoveBuildingValue.Margin = new Padding(3);
            Control_LocationManagement_Label_RemoveBuildingValue.Name = "Control_LocationManagement_Label_RemoveBuildingValue";
            Control_LocationManagement_Label_RemoveBuildingValue.Size = new Size(320, 15);
            Control_LocationManagement_Label_RemoveBuildingValue.TabIndex = 3;
            Control_LocationManagement_Label_RemoveBuildingValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_LocationManagement_Label_RemoveIssuedBy
            // 
            Control_LocationManagement_Label_RemoveIssuedBy.AutoSize = true;
            Control_LocationManagement_Label_RemoveIssuedBy.Dock = DockStyle.Fill;
            Control_LocationManagement_Label_RemoveIssuedBy.Location = new Point(4, 48);
            Control_LocationManagement_Label_RemoveIssuedBy.Margin = new Padding(3);
            Control_LocationManagement_Label_RemoveIssuedBy.Name = "Control_LocationManagement_Label_RemoveIssuedBy";
            Control_LocationManagement_Label_RemoveIssuedBy.Size = new Size(59, 15);
            Control_LocationManagement_Label_RemoveIssuedBy.TabIndex = 4;
            Control_LocationManagement_Label_RemoveIssuedBy.Text = "Issued by:";
            Control_LocationManagement_Label_RemoveIssuedBy.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Control_LocationManagement_Label_RemoveIssuedByValue
            // 
            Control_LocationManagement_Label_RemoveIssuedByValue.AutoSize = true;
            Control_LocationManagement_Label_RemoveIssuedByValue.Dock = DockStyle.Fill;
            Control_LocationManagement_Label_RemoveIssuedByValue.Location = new Point(70, 48);
            Control_LocationManagement_Label_RemoveIssuedByValue.Margin = new Padding(3);
            Control_LocationManagement_Label_RemoveIssuedByValue.Name = "Control_LocationManagement_Label_RemoveIssuedByValue";
            Control_LocationManagement_Label_RemoveIssuedByValue.Size = new Size(320, 15);
            Control_LocationManagement_Label_RemoveIssuedByValue.TabIndex = 5;
            Control_LocationManagement_Label_RemoveIssuedByValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_LocationManagement_Label_RemoveWarning
            // 
            Control_LocationManagement_Label_RemoveWarning.AutoSize = true;
            Control_LocationManagement_Label_RemoveWarning.Dock = DockStyle.Fill;
            Control_LocationManagement_Label_RemoveWarning.ForeColor = Color.FromArgb(192, 0, 0);
            Control_LocationManagement_Label_RemoveWarning.Location = new Point(3, 107);
            Control_LocationManagement_Label_RemoveWarning.Margin = new Padding(3);
            Control_LocationManagement_Label_RemoveWarning.Name = "Control_LocationManagement_Label_RemoveWarning";
            Control_LocationManagement_Label_RemoveWarning.Size = new Size(394, 15);
            Control_LocationManagement_Label_RemoveWarning.TabIndex = 2;
            Control_LocationManagement_Label_RemoveWarning.Text = "Warning: Removal is permanent.";
            Control_LocationManagement_Label_RemoveWarning.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_LocationManagement_TableLayoutPanel_RemoveActions
            // 
            Control_LocationManagement_TableLayoutPanel_RemoveActions.AutoSize = true;
            Control_LocationManagement_TableLayoutPanel_RemoveActions.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_TableLayoutPanel_RemoveActions.ColumnCount = 3;
            Control_LocationManagement_TableLayoutPanel_RemoveActions.ColumnStyles.Add(new ColumnStyle());
            Control_LocationManagement_TableLayoutPanel_RemoveActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_LocationManagement_TableLayoutPanel_RemoveActions.ColumnStyles.Add(new ColumnStyle());
            Control_LocationManagement_TableLayoutPanel_RemoveActions.Controls.Add(Control_LocationManagement_Button_RemoveConfirm, 0, 0);
            Control_LocationManagement_TableLayoutPanel_RemoveActions.Controls.Add(Control_LocationManagement_Button_RemoveCancel, 2, 0);
            Control_LocationManagement_TableLayoutPanel_RemoveActions.Dock = DockStyle.Fill;
            Control_LocationManagement_TableLayoutPanel_RemoveActions.Location = new Point(0, 125);
            Control_LocationManagement_TableLayoutPanel_RemoveActions.Margin = new Padding(0);
            Control_LocationManagement_TableLayoutPanel_RemoveActions.Name = "Control_LocationManagement_TableLayoutPanel_RemoveActions";
            Control_LocationManagement_TableLayoutPanel_RemoveActions.RowCount = 1;
            Control_LocationManagement_TableLayoutPanel_RemoveActions.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_LocationManagement_TableLayoutPanel_RemoveActions.Size = new Size(400, 29);
            Control_LocationManagement_TableLayoutPanel_RemoveActions.TabIndex = 3;
            // 
            // Control_LocationManagement_Button_RemoveConfirm
            // 
            Control_LocationManagement_Button_RemoveConfirm.AutoSize = true;
            Control_LocationManagement_Button_RemoveConfirm.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_Button_RemoveConfirm.Location = new Point(3, 3);
            Control_LocationManagement_Button_RemoveConfirm.MinimumSize = new Size(75, 23);
            Control_LocationManagement_Button_RemoveConfirm.Name = "Control_LocationManagement_Button_RemoveConfirm";
            Control_LocationManagement_Button_RemoveConfirm.Size = new Size(75, 23);
            Control_LocationManagement_Button_RemoveConfirm.TabIndex = 0;
            // 
            // Control_LocationManagement_Button_RemoveCancel
            // 
            Control_LocationManagement_Button_RemoveCancel.AutoSize = true;
            Control_LocationManagement_Button_RemoveCancel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_Button_RemoveCancel.Location = new Point(322, 3);
            Control_LocationManagement_Button_RemoveCancel.MinimumSize = new Size(75, 23);
            Control_LocationManagement_Button_RemoveCancel.Name = "Control_LocationManagement_Button_RemoveCancel";
            Control_LocationManagement_Button_RemoveCancel.Size = new Size(75, 23);
            Control_LocationManagement_Button_RemoveCancel.TabIndex = 1;
            // 
            // Control_LocationManagement_TableLayoutPanel_BackButton
            // 
            Control_LocationManagement_TableLayoutPanel_BackButton.AutoSize = true;
            Control_LocationManagement_TableLayoutPanel_BackButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_LocationManagement_TableLayoutPanel_BackButton.ColumnCount = 3;
            Control_LocationManagement_TableLayoutPanel_BackButton.ColumnStyles.Add(new ColumnStyle());
            Control_LocationManagement_TableLayoutPanel_BackButton.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_LocationManagement_TableLayoutPanel_BackButton.ColumnStyles.Add(new ColumnStyle());
            Control_LocationManagement_TableLayoutPanel_BackButton.Controls.Add(Control_LocationManagement_Button_Home, 2, 0);
            Control_LocationManagement_TableLayoutPanel_BackButton.Controls.Add(Control_LocationManagement_Button_Back, 0, 0);
            Control_LocationManagement_TableLayoutPanel_BackButton.Dock = DockStyle.Fill;
            Control_LocationManagement_TableLayoutPanel_BackButton.Location = new Point(23, 323);
            Control_LocationManagement_TableLayoutPanel_BackButton.Name = "Control_LocationManagement_TableLayoutPanel_BackButton";
            Control_LocationManagement_TableLayoutPanel_BackButton.RowCount = 1;
            Control_LocationManagement_TableLayoutPanel_BackButton.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_LocationManagement_TableLayoutPanel_BackButton.Size = new Size(446, 43);
            Control_LocationManagement_TableLayoutPanel_BackButton.TabIndex = 0;
            // 
            // Control_LocationManagement_Button_Back
            // 
            Control_LocationManagement_Button_Back.AutoSize = true;
            Control_LocationManagement_Button_Back.Location = new Point(3, 3);
            Control_LocationManagement_Button_Back.Name = "Control_LocationManagement_Button_Back";
            Control_LocationManagement_Button_Back.Padding = new Padding(16, 6, 16, 6);
            Control_LocationManagement_Button_Back.Size = new Size(152, 37);
            Control_LocationManagement_Button_Back.TabIndex = 0;
            Control_LocationManagement_Button_Back.Text = "← Back to Selection";
            Control_LocationManagement_Button_Back.Visible = false;
            // 
            // Control_LocationManagement_Label_AddIssuedBy
            // 
            Control_LocationManagement_Label_AddIssuedBy.AutoSize = true;
            Control_LocationManagement_Label_AddIssuedBy.Location = new Point(0, 0);
            Control_LocationManagement_Label_AddIssuedBy.Name = "Control_LocationManagement_Label_AddIssuedBy";
            Control_LocationManagement_Label_AddIssuedBy.Size = new Size(100, 23);
            Control_LocationManagement_Label_AddIssuedBy.TabIndex = 0;
            Control_LocationManagement_Label_AddIssuedBy.Text = "Issued By:";
            // 
            // Control_LocationManagement_Label_AddIssuedByValue
            // 
            Control_LocationManagement_Label_AddIssuedByValue.AutoSize = true;
            Control_LocationManagement_Label_AddIssuedByValue.Location = new Point(0, 0);
            Control_LocationManagement_Label_AddIssuedByValue.Name = "Control_LocationManagement_Label_AddIssuedByValue";
            Control_LocationManagement_Label_AddIssuedByValue.Size = new Size(100, 23);
            Control_LocationManagement_Label_AddIssuedByValue.TabIndex = 0;
            // 
            // Control_LocationManagement_Label_EditIssuedBy
            // 
            Control_LocationManagement_Label_EditIssuedBy.AutoSize = true;
            Control_LocationManagement_Label_EditIssuedBy.Location = new Point(0, 0);
            Control_LocationManagement_Label_EditIssuedBy.Name = "Control_LocationManagement_Label_EditIssuedBy";
            Control_LocationManagement_Label_EditIssuedBy.Size = new Size(100, 23);
            Control_LocationManagement_Label_EditIssuedBy.TabIndex = 0;
            // 
            // Control_LocationManagement_Label_EditIssuedByValue
            // 
            Control_LocationManagement_Label_EditIssuedByValue.AutoSize = true;
            Control_LocationManagement_Label_EditIssuedByValue.Location = new Point(0, 0);
            Control_LocationManagement_Label_EditIssuedByValue.Name = "Control_LocationManagement_Label_EditIssuedByValue";
            Control_LocationManagement_Label_EditIssuedByValue.Size = new Size(100, 23);
            Control_LocationManagement_Label_EditIssuedByValue.TabIndex = 0;
            // 
            // Control_LocationManagement_Button_Home
            // 
            Control_LocationManagement_Button_Home.AutoSize = true;
            Control_LocationManagement_Button_Home.Location = new Point(304, 3);
            Control_LocationManagement_Button_Home.Name = "Control_LocationManagement_Button_Home";
            Control_LocationManagement_Button_Home.Padding = new Padding(16, 6, 16, 6);
            Control_LocationManagement_Button_Home.Size = new Size(139, 37);
            Control_LocationManagement_Button_Home.TabIndex = 2;
            Control_LocationManagement_Button_Home.Text = "🏠 Back to Home";
            // 
            // Control_LocationManagement
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(Control_LocationManagement_TableLayoutPanel_Main);
            Name = "Control_LocationManagement";
            Size = new Size(492, 389);
            Control_LocationManagement_TableLayoutPanel_Main.ResumeLayout(false);
            Control_LocationManagement_TableLayoutPanel_Main.PerformLayout();
            Control_LocationManagement_Panel_Container.ResumeLayout(false);
            Control_LocationManagement_Panel_Container.PerformLayout();
            Control_LocationManagement_Panel_Home.ResumeLayout(false);
            Control_LocationManagement_TableLayoutPanel_Home.ResumeLayout(false);
            Control_LocationManagement_TableLayoutPanel_Home.PerformLayout();
            Control_LocationManagement_Panel_HomeTile_Add.ResumeLayout(false);
            Control_LocationManagement_Panel_HomeTile_Add.PerformLayout();
            Control_LocationManagement_TableLayout_HomeTile_Add.ResumeLayout(false);
            Control_LocationManagement_TableLayout_HomeTile_Add.PerformLayout();
            Control_LocationManagement_Panel_HomeTile_Remove.ResumeLayout(false);
            Control_LocationManagement_Panel_HomeTile_Remove.PerformLayout();
            Control_LocationManagement_TableLayout_HomeTile_Remove.ResumeLayout(false);
            Control_LocationManagement_TableLayout_HomeTile_Remove.PerformLayout();
            Control_LocationManagement_Panel_HomeTile_Edit.ResumeLayout(false);
            Control_LocationManagement_Panel_HomeTile_Edit.PerformLayout();
            Control_LocationManagement_TableLayout_HomeTile_Edit.ResumeLayout(false);
            Control_LocationManagement_TableLayout_HomeTile_Edit.PerformLayout();
            Control_LocationManagement_TableLayoutPanel_Cards.ResumeLayout(false);
            Control_LocationManagement_TableLayoutPanel_Cards.PerformLayout();
            Control_LocationManagement_Panel_AddCard.ResumeLayout(false);
            Control_LocationManagement_Panel_AddCard.PerformLayout();
            Control_LocationManagement_TableLayoutPanel_Add.ResumeLayout(false);
            Control_LocationManagement_TableLayoutPanel_Add.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            Control_LocationManagement_TableLayoutPanel_AddContent.ResumeLayout(false);
            Control_LocationManagement_TableLayoutPanel_AddContent.PerformLayout();
            Control_LocationManagement_TableLayoutPanel_AddActions.ResumeLayout(false);
            Control_LocationManagement_TableLayoutPanel_AddActions.PerformLayout();
            Control_LocationManagement_Panel_EditCard.ResumeLayout(false);
            Control_LocationManagement_Panel_EditCard.PerformLayout();
            Control_LocationManagement_TableLayoutPanel_Edit.ResumeLayout(false);
            Control_LocationManagement_TableLayoutPanel_Edit.PerformLayout();
            Control_LocationManagement_TableLayoutPanel_EditContent.ResumeLayout(false);
            Control_LocationManagement_TableLayoutPanel_EditContent.PerformLayout();
            Control_LocationManagement_TableLayoutPanel_EditActions.ResumeLayout(false);
            Control_LocationManagement_TableLayoutPanel_EditActions.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            Control_LocationManagement_Panel_RemoveCard.ResumeLayout(false);
            Control_LocationManagement_Panel_RemoveCard.PerformLayout();
            Control_LocationManagement_TableLayoutPanel_Remove.ResumeLayout(false);
            Control_LocationManagement_TableLayoutPanel_Remove.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            Control_LocationManagement_TableLayoutPanel_RemoveContent.ResumeLayout(false);
            Control_LocationManagement_TableLayoutPanel_RemoveContent.PerformLayout();
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.ResumeLayout(false);
            Control_LocationManagement_TableLayoutPanel_RemoveDetails.PerformLayout();
            Control_LocationManagement_TableLayoutPanel_RemoveActions.ResumeLayout(false);
            Control_LocationManagement_TableLayoutPanel_RemoveActions.PerformLayout();
            Control_LocationManagement_TableLayoutPanel_BackButton.ResumeLayout(false);
            Control_LocationManagement_TableLayoutPanel_BackButton.PerformLayout();
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
            content.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            
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
        private Label label1;
        private Label label2;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel Control_LocationManagement_TableLayoutPanel_EditContent;
        private TableLayoutPanel Control_LocationManagement_TableLayoutPanel_EditActions;
        private Button Control_LocationManagement_Button_EditSave;
        private Button Control_LocationManagement_Button_EditReset;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label3;
        private Label label4;
        private TableLayoutPanel tableLayoutPanel3;
        private Button Control_LocationManagement_Button_Home;
    }
}
