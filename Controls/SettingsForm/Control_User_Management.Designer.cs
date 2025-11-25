using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Controls.Shared;

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    partial class Control_User_Management
    {
        #region Fields

        private IContainer components = null!;
        private TableLayoutPanel Control_User_Management_TableLayout_Main = null!;
        private Panel Control_User_Management_Panel_Container = null!;
        
        private Panel Control_User_Management_Panel_Home = null!;
        private TableLayoutPanel Control_User_Management_TableLayout_Home = null!;
        
        // Home Tiles
        private Panel Control_User_Management_Panel_HomeTile_Add = null!;
        private TableLayoutPanel Control_User_Management_TableLayout_HomeTile_Add = null!;
        private Label Control_User_Management_Label_HomeTile_AddIcon = null!;
        private Label Control_User_Management_Label_HomeTile_AddTitle = null!;
        private Label Control_User_Management_Label_HomeTile_AddInstruction = null!;
        
        private Panel Control_User_Management_Panel_HomeTile_Edit = null!;
        private TableLayoutPanel Control_User_Management_TableLayout_HomeTile_Edit = null!;
        private Label Control_User_Management_Label_HomeTile_EditIcon = null!;
        private Label Control_User_Management_Label_HomeTile_EditTitle = null!;
        private Label Control_User_Management_Label_HomeTile_EditInstruction = null!;
        
        private Panel Control_User_Management_Panel_HomeTile_Remove = null!;
        private TableLayoutPanel Control_User_Management_TableLayout_HomeTile_Remove = null!;
        private Label Control_User_Management_Label_HomeTile_RemoveIcon = null!;
        private Label Control_User_Management_Label_HomeTile_RemoveTitle = null!;
        private Label Control_User_Management_Label_HomeTile_RemoveInstruction = null!;
        private TableLayoutPanel Control_User_Management_TableLayout_BackButton = null!;
        private Button Control_User_Management_Button_Back = null!;
        private Button Control_User_Management_Button_Home = null!;

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
            Control_User_Management_TableLayout_Main = new TableLayoutPanel();
            Control_User_Management_Panel_Container = new Panel();
            Control_User_Management_Panel_Home = new Panel();
            Control_User_Management_TableLayout_Home = new TableLayoutPanel();
            Control_User_Management_Panel_HomeTile_Add = new Panel();
            Control_User_Management_TableLayout_HomeTile_Add = new TableLayoutPanel();
            Control_User_Management_Label_HomeTile_AddIcon = new Label();
            Control_User_Management_Label_HomeTile_AddTitle = new Label();
            Control_User_Management_Label_HomeTile_AddInstruction = new Label();
            Control_User_Management_Panel_HomeTile_Edit = new Panel();
            Control_User_Management_TableLayout_HomeTile_Edit = new TableLayoutPanel();
            Control_User_Management_Label_HomeTile_EditIcon = new Label();
            Control_User_Management_Label_HomeTile_EditTitle = new Label();
            Control_User_Management_Label_HomeTile_EditInstruction = new Label();
            Control_User_Management_Panel_HomeTile_Remove = new Panel();
            Control_User_Management_TableLayout_HomeTile_Remove = new TableLayoutPanel();
            Control_User_Management_Label_HomeTile_RemoveIcon = new Label();
            Control_User_Management_Label_HomeTile_RemoveTitle = new Label();
            Control_User_Management_Label_HomeTile_RemoveInstruction = new Label();
            Control_User_Management_TableLayout_BackButton = new TableLayoutPanel();
            Control_User_Management_Button_Back = new Button();
            Control_User_Management_Button_Home = new Button();
            Control_User_Management_Panel_Add = new Panel();
            Control_User_Management_Panel_Edit = new Panel();
            Control_User_Management_Panel_Remove = new Panel();
            Control_User_Management_TableLayout_EditCard = new TableLayoutPanel();
            Control_User_Management_Control_EditUser = new Control_Edit_User();
            Control_User_Management_TableLayout_RemoveCard = new TableLayoutPanel();
            Control_User_Management_Control_RemoveUser = new Control_Remove_User();
            Control_User_Management_TableLayout_AddCard = new TableLayoutPanel();
            Control_User_Management_Control_AddUser = new Control_Add_User();
            Control_User_Management_TableLayout_Main.SuspendLayout();
            Control_User_Management_Panel_Container.SuspendLayout();
            Control_User_Management_Panel_Home.SuspendLayout();
            Control_User_Management_TableLayout_Home.SuspendLayout();
            Control_User_Management_Panel_HomeTile_Add.SuspendLayout();
            Control_User_Management_TableLayout_HomeTile_Add.SuspendLayout();
            Control_User_Management_Panel_HomeTile_Edit.SuspendLayout();
            Control_User_Management_TableLayout_HomeTile_Edit.SuspendLayout();
            Control_User_Management_Panel_HomeTile_Remove.SuspendLayout();
            Control_User_Management_TableLayout_HomeTile_Remove.SuspendLayout();
            Control_User_Management_TableLayout_BackButton.SuspendLayout();
            Control_User_Management_Panel_Add.SuspendLayout();
            Control_User_Management_Panel_Edit.SuspendLayout();
            Control_User_Management_Panel_Remove.SuspendLayout();
            Control_User_Management_TableLayout_EditCard.SuspendLayout();
            Control_User_Management_TableLayout_RemoveCard.SuspendLayout();
            Control_User_Management_TableLayout_AddCard.SuspendLayout();
            SuspendLayout();
            // 
            // Control_User_Management_TableLayout_Main
            // 
            Control_User_Management_TableLayout_Main.AutoSize = true;
            Control_User_Management_TableLayout_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_User_Management_TableLayout_Main.ColumnCount = 1;
            Control_User_Management_TableLayout_Main.ColumnStyles.Add(new ColumnStyle());
            Control_User_Management_TableLayout_Main.Controls.Add(Control_User_Management_Panel_Container, 0, 2);
            Control_User_Management_TableLayout_Main.Controls.Add(Control_User_Management_TableLayout_BackButton, 0, 3);
            Control_User_Management_TableLayout_Main.Dock = DockStyle.Fill;
            Control_User_Management_TableLayout_Main.Location = new Point(0, 0);
            Control_User_Management_TableLayout_Main.Name = "Control_User_Management_TableLayout_Main";
            Control_User_Management_TableLayout_Main.RowCount = 4;
            Control_User_Management_TableLayout_Main.RowStyles.Add(new RowStyle());
            Control_User_Management_TableLayout_Main.RowStyles.Add(new RowStyle());
            Control_User_Management_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_User_Management_TableLayout_Main.RowStyles.Add(new RowStyle());
            Control_User_Management_TableLayout_Main.Size = new Size(530, 493);
            Control_User_Management_TableLayout_Main.TabIndex = 0;
            // 
            // Control_User_Management_Panel_Container
            // 
            Control_User_Management_Panel_Container.Controls.Add(Control_User_Management_Panel_Home);
            Control_User_Management_Panel_Container.Controls.Add(Control_User_Management_Panel_Edit);
            Control_User_Management_Panel_Container.Controls.Add(Control_User_Management_Panel_Add);
            Control_User_Management_Panel_Container.Controls.Add(Control_User_Management_Panel_Remove);
            Control_User_Management_Panel_Container.Dock = DockStyle.Fill;
            Control_User_Management_Panel_Container.Location = new Point(3, 3);
            Control_User_Management_Panel_Container.Name = "Control_User_Management_Panel_Container";
            Control_User_Management_Panel_Container.Size = new Size(524, 444);
            Control_User_Management_Panel_Container.TabIndex = 2;
            // 
            // Control_User_Management_Panel_Home
            // 
            Control_User_Management_Panel_Home.AutoSize = true;
            Control_User_Management_Panel_Home.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_User_Management_Panel_Home.Controls.Add(Control_User_Management_TableLayout_Home);
            Control_User_Management_Panel_Home.Dock = DockStyle.Fill;
            Control_User_Management_Panel_Home.Location = new Point(0, 0);
            Control_User_Management_Panel_Home.Name = "Control_User_Management_Panel_Home";
            Control_User_Management_Panel_Home.Size = new Size(524, 444);
            Control_User_Management_Panel_Home.TabIndex = 0;
            // 
            // Control_User_Management_TableLayout_Home
            // 
            Control_User_Management_TableLayout_Home.AutoSize = true;
            Control_User_Management_TableLayout_Home.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_User_Management_TableLayout_Home.ColumnCount = 3;
            Control_User_Management_TableLayout_Home.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            Control_User_Management_TableLayout_Home.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            Control_User_Management_TableLayout_Home.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            Control_User_Management_TableLayout_Home.Controls.Add(Control_User_Management_Panel_HomeTile_Add, 0, 0);
            Control_User_Management_TableLayout_Home.Controls.Add(Control_User_Management_Panel_HomeTile_Edit, 1, 0);
            Control_User_Management_TableLayout_Home.Controls.Add(Control_User_Management_Panel_HomeTile_Remove, 2, 0);
            Control_User_Management_TableLayout_Home.Dock = DockStyle.Fill;
            Control_User_Management_TableLayout_Home.Location = new Point(0, 0);
            Control_User_Management_TableLayout_Home.Name = "Control_User_Management_TableLayout_Home";
            Control_User_Management_TableLayout_Home.RowCount = 1;
            Control_User_Management_TableLayout_Home.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_User_Management_TableLayout_Home.Size = new Size(524, 444);
            Control_User_Management_TableLayout_Home.TabIndex = 0;
            // 
            // Control_User_Management_Panel_HomeTile_Add
            // 
            Control_User_Management_Panel_HomeTile_Add.BorderStyle = BorderStyle.FixedSingle;
            Control_User_Management_Panel_HomeTile_Add.Controls.Add(Control_User_Management_TableLayout_HomeTile_Add);
            Control_User_Management_Panel_HomeTile_Add.Cursor = Cursors.Hand;
            Control_User_Management_Panel_HomeTile_Add.Dock = DockStyle.Fill;
            Control_User_Management_Panel_HomeTile_Add.Location = new Point(3, 3);
            Control_User_Management_Panel_HomeTile_Add.Name = "Control_User_Management_Panel_HomeTile_Add";
            Control_User_Management_Panel_HomeTile_Add.Size = new Size(168, 438);
            Control_User_Management_Panel_HomeTile_Add.TabIndex = 0;
            // 
            // Control_User_Management_TableLayout_HomeTile_Add
            // 
            Control_User_Management_TableLayout_HomeTile_Add.AutoSize = true;
            Control_User_Management_TableLayout_HomeTile_Add.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_User_Management_TableLayout_HomeTile_Add.ColumnCount = 1;
            Control_User_Management_TableLayout_HomeTile_Add.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_User_Management_TableLayout_HomeTile_Add.Controls.Add(Control_User_Management_Label_HomeTile_AddIcon, 0, 0);
            Control_User_Management_TableLayout_HomeTile_Add.Controls.Add(Control_User_Management_Label_HomeTile_AddTitle, 0, 1);
            Control_User_Management_TableLayout_HomeTile_Add.Controls.Add(Control_User_Management_Label_HomeTile_AddInstruction, 0, 3);
            Control_User_Management_TableLayout_HomeTile_Add.Dock = DockStyle.Fill;
            Control_User_Management_TableLayout_HomeTile_Add.Location = new Point(0, 0);
            Control_User_Management_TableLayout_HomeTile_Add.Name = "Control_User_Management_TableLayout_HomeTile_Add";
            Control_User_Management_TableLayout_HomeTile_Add.Padding = new Padding(3);
            Control_User_Management_TableLayout_HomeTile_Add.RowCount = 5;
            Control_User_Management_TableLayout_HomeTile_Add.RowStyles.Add(new RowStyle());
            Control_User_Management_TableLayout_HomeTile_Add.RowStyles.Add(new RowStyle());
            Control_User_Management_TableLayout_HomeTile_Add.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Control_User_Management_TableLayout_HomeTile_Add.RowStyles.Add(new RowStyle());
            Control_User_Management_TableLayout_HomeTile_Add.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Control_User_Management_TableLayout_HomeTile_Add.Size = new Size(166, 436);
            Control_User_Management_TableLayout_HomeTile_Add.TabIndex = 0;
            // 
            // Control_User_Management_Label_HomeTile_AddIcon
            // 
            Control_User_Management_Label_HomeTile_AddIcon.AutoSize = true;
            Control_User_Management_Label_HomeTile_AddIcon.Dock = DockStyle.Fill;
            Control_User_Management_Label_HomeTile_AddIcon.Font = new Font("Segoe UI Emoji", 48F);
            Control_User_Management_Label_HomeTile_AddIcon.Location = new Point(6, 6);
            Control_User_Management_Label_HomeTile_AddIcon.Margin = new Padding(3);
            Control_User_Management_Label_HomeTile_AddIcon.Name = "Control_User_Management_Label_HomeTile_AddIcon";
            Control_User_Management_Label_HomeTile_AddIcon.Size = new Size(154, 85);
            Control_User_Management_Label_HomeTile_AddIcon.TabIndex = 0;
            Control_User_Management_Label_HomeTile_AddIcon.Text = "➕";
            Control_User_Management_Label_HomeTile_AddIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_User_Management_Label_HomeTile_AddTitle
            // 
            Control_User_Management_Label_HomeTile_AddTitle.AutoSize = true;
            Control_User_Management_Label_HomeTile_AddTitle.Dock = DockStyle.Fill;
            Control_User_Management_Label_HomeTile_AddTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            Control_User_Management_Label_HomeTile_AddTitle.ForeColor = Color.FromArgb(46, 204, 113);
            Control_User_Management_Label_HomeTile_AddTitle.Location = new Point(6, 97);
            Control_User_Management_Label_HomeTile_AddTitle.Margin = new Padding(3);
            Control_User_Management_Label_HomeTile_AddTitle.Name = "Control_User_Management_Label_HomeTile_AddTitle";
            Control_User_Management_Label_HomeTile_AddTitle.Size = new Size(154, 60);
            Control_User_Management_Label_HomeTile_AddTitle.TabIndex = 1;
            Control_User_Management_Label_HomeTile_AddTitle.Text = "Add\r\nUser";
            Control_User_Management_Label_HomeTile_AddTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_User_Management_Label_HomeTile_AddInstruction
            // 
            Control_User_Management_Label_HomeTile_AddInstruction.AutoSize = true;
            Control_User_Management_Label_HomeTile_AddInstruction.Dock = DockStyle.Fill;
            Control_User_Management_Label_HomeTile_AddInstruction.Font = new Font("Segoe UI", 9F);
            Control_User_Management_Label_HomeTile_AddInstruction.ForeColor = Color.Gray;
            Control_User_Management_Label_HomeTile_AddInstruction.Location = new Point(6, 281);
            Control_User_Management_Label_HomeTile_AddInstruction.Margin = new Padding(3);
            Control_User_Management_Label_HomeTile_AddInstruction.Name = "Control_User_Management_Label_HomeTile_AddInstruction";
            Control_User_Management_Label_HomeTile_AddInstruction.Size = new Size(154, 30);
            Control_User_Management_Label_HomeTile_AddInstruction.TabIndex = 2;
            Control_User_Management_Label_HomeTile_AddInstruction.Text = "Create a new user account with specific permissions.";
            Control_User_Management_Label_HomeTile_AddInstruction.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_User_Management_Panel_HomeTile_Edit
            // 
            Control_User_Management_Panel_HomeTile_Edit.BorderStyle = BorderStyle.FixedSingle;
            Control_User_Management_Panel_HomeTile_Edit.Controls.Add(Control_User_Management_TableLayout_HomeTile_Edit);
            Control_User_Management_Panel_HomeTile_Edit.Cursor = Cursors.Hand;
            Control_User_Management_Panel_HomeTile_Edit.Dock = DockStyle.Fill;
            Control_User_Management_Panel_HomeTile_Edit.Location = new Point(177, 3);
            Control_User_Management_Panel_HomeTile_Edit.Name = "Control_User_Management_Panel_HomeTile_Edit";
            Control_User_Management_Panel_HomeTile_Edit.Size = new Size(168, 438);
            Control_User_Management_Panel_HomeTile_Edit.TabIndex = 1;
            // 
            // Control_User_Management_TableLayout_HomeTile_Edit
            // 
            Control_User_Management_TableLayout_HomeTile_Edit.AutoSize = true;
            Control_User_Management_TableLayout_HomeTile_Edit.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_User_Management_TableLayout_HomeTile_Edit.ColumnCount = 1;
            Control_User_Management_TableLayout_HomeTile_Edit.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_User_Management_TableLayout_HomeTile_Edit.Controls.Add(Control_User_Management_Label_HomeTile_EditIcon, 0, 0);
            Control_User_Management_TableLayout_HomeTile_Edit.Controls.Add(Control_User_Management_Label_HomeTile_EditTitle, 0, 1);
            Control_User_Management_TableLayout_HomeTile_Edit.Controls.Add(Control_User_Management_Label_HomeTile_EditInstruction, 0, 3);
            Control_User_Management_TableLayout_HomeTile_Edit.Dock = DockStyle.Fill;
            Control_User_Management_TableLayout_HomeTile_Edit.Location = new Point(0, 0);
            Control_User_Management_TableLayout_HomeTile_Edit.Name = "Control_User_Management_TableLayout_HomeTile_Edit";
            Control_User_Management_TableLayout_HomeTile_Edit.Padding = new Padding(3);
            Control_User_Management_TableLayout_HomeTile_Edit.RowCount = 5;
            Control_User_Management_TableLayout_HomeTile_Edit.RowStyles.Add(new RowStyle());
            Control_User_Management_TableLayout_HomeTile_Edit.RowStyles.Add(new RowStyle());
            Control_User_Management_TableLayout_HomeTile_Edit.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Control_User_Management_TableLayout_HomeTile_Edit.RowStyles.Add(new RowStyle());
            Control_User_Management_TableLayout_HomeTile_Edit.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Control_User_Management_TableLayout_HomeTile_Edit.Size = new Size(166, 436);
            Control_User_Management_TableLayout_HomeTile_Edit.TabIndex = 0;
            // 
            // Control_User_Management_Label_HomeTile_EditIcon
            // 
            Control_User_Management_Label_HomeTile_EditIcon.AutoSize = true;
            Control_User_Management_Label_HomeTile_EditIcon.Dock = DockStyle.Fill;
            Control_User_Management_Label_HomeTile_EditIcon.Font = new Font("Segoe UI Emoji", 48F);
            Control_User_Management_Label_HomeTile_EditIcon.Location = new Point(6, 6);
            Control_User_Management_Label_HomeTile_EditIcon.Margin = new Padding(3);
            Control_User_Management_Label_HomeTile_EditIcon.Name = "Control_User_Management_Label_HomeTile_EditIcon";
            Control_User_Management_Label_HomeTile_EditIcon.Size = new Size(154, 85);
            Control_User_Management_Label_HomeTile_EditIcon.TabIndex = 0;
            Control_User_Management_Label_HomeTile_EditIcon.Text = "✏️";
            Control_User_Management_Label_HomeTile_EditIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_User_Management_Label_HomeTile_EditTitle
            // 
            Control_User_Management_Label_HomeTile_EditTitle.AutoSize = true;
            Control_User_Management_Label_HomeTile_EditTitle.Dock = DockStyle.Fill;
            Control_User_Management_Label_HomeTile_EditTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            Control_User_Management_Label_HomeTile_EditTitle.ForeColor = Color.FromArgb(52, 152, 219);
            Control_User_Management_Label_HomeTile_EditTitle.Location = new Point(6, 97);
            Control_User_Management_Label_HomeTile_EditTitle.Margin = new Padding(3);
            Control_User_Management_Label_HomeTile_EditTitle.Name = "Control_User_Management_Label_HomeTile_EditTitle";
            Control_User_Management_Label_HomeTile_EditTitle.Size = new Size(154, 60);
            Control_User_Management_Label_HomeTile_EditTitle.TabIndex = 1;
            Control_User_Management_Label_HomeTile_EditTitle.Text = "Edit\r\nUser";
            Control_User_Management_Label_HomeTile_EditTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_User_Management_Label_HomeTile_EditInstruction
            // 
            Control_User_Management_Label_HomeTile_EditInstruction.AutoSize = true;
            Control_User_Management_Label_HomeTile_EditInstruction.Dock = DockStyle.Fill;
            Control_User_Management_Label_HomeTile_EditInstruction.Font = new Font("Segoe UI", 9F);
            Control_User_Management_Label_HomeTile_EditInstruction.ForeColor = Color.Gray;
            Control_User_Management_Label_HomeTile_EditInstruction.Location = new Point(6, 281);
            Control_User_Management_Label_HomeTile_EditInstruction.Margin = new Padding(3);
            Control_User_Management_Label_HomeTile_EditInstruction.Name = "Control_User_Management_Label_HomeTile_EditInstruction";
            Control_User_Management_Label_HomeTile_EditInstruction.Size = new Size(154, 30);
            Control_User_Management_Label_HomeTile_EditInstruction.TabIndex = 2;
            Control_User_Management_Label_HomeTile_EditInstruction.Text = "Modify details of an existing user account.";
            Control_User_Management_Label_HomeTile_EditInstruction.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_User_Management_Panel_HomeTile_Remove
            // 
            Control_User_Management_Panel_HomeTile_Remove.BorderStyle = BorderStyle.FixedSingle;
            Control_User_Management_Panel_HomeTile_Remove.Controls.Add(Control_User_Management_TableLayout_HomeTile_Remove);
            Control_User_Management_Panel_HomeTile_Remove.Cursor = Cursors.Hand;
            Control_User_Management_Panel_HomeTile_Remove.Dock = DockStyle.Fill;
            Control_User_Management_Panel_HomeTile_Remove.Location = new Point(351, 3);
            Control_User_Management_Panel_HomeTile_Remove.Name = "Control_User_Management_Panel_HomeTile_Remove";
            Control_User_Management_Panel_HomeTile_Remove.Size = new Size(170, 438);
            Control_User_Management_Panel_HomeTile_Remove.TabIndex = 2;
            // 
            // Control_User_Management_TableLayout_HomeTile_Remove
            // 
            Control_User_Management_TableLayout_HomeTile_Remove.AutoSize = true;
            Control_User_Management_TableLayout_HomeTile_Remove.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_User_Management_TableLayout_HomeTile_Remove.ColumnCount = 1;
            Control_User_Management_TableLayout_HomeTile_Remove.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_User_Management_TableLayout_HomeTile_Remove.Controls.Add(Control_User_Management_Label_HomeTile_RemoveIcon, 0, 0);
            Control_User_Management_TableLayout_HomeTile_Remove.Controls.Add(Control_User_Management_Label_HomeTile_RemoveTitle, 0, 1);
            Control_User_Management_TableLayout_HomeTile_Remove.Controls.Add(Control_User_Management_Label_HomeTile_RemoveInstruction, 0, 3);
            Control_User_Management_TableLayout_HomeTile_Remove.Dock = DockStyle.Fill;
            Control_User_Management_TableLayout_HomeTile_Remove.Location = new Point(0, 0);
            Control_User_Management_TableLayout_HomeTile_Remove.Name = "Control_User_Management_TableLayout_HomeTile_Remove";
            Control_User_Management_TableLayout_HomeTile_Remove.Padding = new Padding(3);
            Control_User_Management_TableLayout_HomeTile_Remove.RowCount = 5;
            Control_User_Management_TableLayout_HomeTile_Remove.RowStyles.Add(new RowStyle());
            Control_User_Management_TableLayout_HomeTile_Remove.RowStyles.Add(new RowStyle());
            Control_User_Management_TableLayout_HomeTile_Remove.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Control_User_Management_TableLayout_HomeTile_Remove.RowStyles.Add(new RowStyle());
            Control_User_Management_TableLayout_HomeTile_Remove.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Control_User_Management_TableLayout_HomeTile_Remove.Size = new Size(168, 436);
            Control_User_Management_TableLayout_HomeTile_Remove.TabIndex = 0;
            // 
            // Control_User_Management_Label_HomeTile_RemoveIcon
            // 
            Control_User_Management_Label_HomeTile_RemoveIcon.AutoSize = true;
            Control_User_Management_Label_HomeTile_RemoveIcon.Dock = DockStyle.Fill;
            Control_User_Management_Label_HomeTile_RemoveIcon.Font = new Font("Segoe UI Emoji", 48F);
            Control_User_Management_Label_HomeTile_RemoveIcon.Location = new Point(6, 6);
            Control_User_Management_Label_HomeTile_RemoveIcon.Margin = new Padding(3);
            Control_User_Management_Label_HomeTile_RemoveIcon.Name = "Control_User_Management_Label_HomeTile_RemoveIcon";
            Control_User_Management_Label_HomeTile_RemoveIcon.Size = new Size(156, 85);
            Control_User_Management_Label_HomeTile_RemoveIcon.TabIndex = 0;
            Control_User_Management_Label_HomeTile_RemoveIcon.Text = "🗑️";
            Control_User_Management_Label_HomeTile_RemoveIcon.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_User_Management_Label_HomeTile_RemoveTitle
            // 
            Control_User_Management_Label_HomeTile_RemoveTitle.AutoSize = true;
            Control_User_Management_Label_HomeTile_RemoveTitle.Dock = DockStyle.Fill;
            Control_User_Management_Label_HomeTile_RemoveTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            Control_User_Management_Label_HomeTile_RemoveTitle.ForeColor = Color.FromArgb(231, 76, 60);
            Control_User_Management_Label_HomeTile_RemoveTitle.Location = new Point(6, 97);
            Control_User_Management_Label_HomeTile_RemoveTitle.Margin = new Padding(3);
            Control_User_Management_Label_HomeTile_RemoveTitle.Name = "Control_User_Management_Label_HomeTile_RemoveTitle";
            Control_User_Management_Label_HomeTile_RemoveTitle.Size = new Size(156, 60);
            Control_User_Management_Label_HomeTile_RemoveTitle.TabIndex = 1;
            Control_User_Management_Label_HomeTile_RemoveTitle.Text = "Remove\r\nUser";
            Control_User_Management_Label_HomeTile_RemoveTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_User_Management_Label_HomeTile_RemoveInstruction
            // 
            Control_User_Management_Label_HomeTile_RemoveInstruction.AutoSize = true;
            Control_User_Management_Label_HomeTile_RemoveInstruction.Dock = DockStyle.Fill;
            Control_User_Management_Label_HomeTile_RemoveInstruction.Font = new Font("Segoe UI", 9F);
            Control_User_Management_Label_HomeTile_RemoveInstruction.ForeColor = Color.Gray;
            Control_User_Management_Label_HomeTile_RemoveInstruction.Location = new Point(6, 281);
            Control_User_Management_Label_HomeTile_RemoveInstruction.Margin = new Padding(3);
            Control_User_Management_Label_HomeTile_RemoveInstruction.Name = "Control_User_Management_Label_HomeTile_RemoveInstruction";
            Control_User_Management_Label_HomeTile_RemoveInstruction.Size = new Size(156, 30);
            Control_User_Management_Label_HomeTile_RemoveInstruction.TabIndex = 2;
            Control_User_Management_Label_HomeTile_RemoveInstruction.Text = "Permanently delete a user account.";
            Control_User_Management_Label_HomeTile_RemoveInstruction.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_User_Management_TableLayout_BackButton
            // 
            Control_User_Management_TableLayout_BackButton.AutoSize = true;
            Control_User_Management_TableLayout_BackButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_User_Management_TableLayout_BackButton.ColumnCount = 3;
            Control_User_Management_TableLayout_BackButton.ColumnStyles.Add(new ColumnStyle());
            Control_User_Management_TableLayout_BackButton.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_User_Management_TableLayout_BackButton.ColumnStyles.Add(new ColumnStyle());
            Control_User_Management_TableLayout_BackButton.Controls.Add(Control_User_Management_Button_Back, 0, 0);
            Control_User_Management_TableLayout_BackButton.Controls.Add(Control_User_Management_Button_Home, 2, 0);
            Control_User_Management_TableLayout_BackButton.Dock = DockStyle.Fill;
            Control_User_Management_TableLayout_BackButton.Location = new Point(3, 453);
            Control_User_Management_TableLayout_BackButton.Name = "Control_User_Management_TableLayout_BackButton";
            Control_User_Management_TableLayout_BackButton.RowCount = 1;
            Control_User_Management_TableLayout_BackButton.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_User_Management_TableLayout_BackButton.Size = new Size(524, 37);
            Control_User_Management_TableLayout_BackButton.TabIndex = 3;
            // 
            // Control_User_Management_Button_Back
            // 
            Control_User_Management_Button_Back.AutoSize = true;
            Control_User_Management_Button_Back.FlatStyle = FlatStyle.Flat;
            Control_User_Management_Button_Back.Font = new Font("Segoe UI", 10F);
            Control_User_Management_Button_Back.Location = new Point(3, 3);
            Control_User_Management_Button_Back.Name = "Control_User_Management_Button_Back";
            Control_User_Management_Button_Back.Size = new Size(65, 31);
            Control_User_Management_Button_Back.TabIndex = 0;
            Control_User_Management_Button_Back.Text = "← Back";
            Control_User_Management_Button_Back.UseVisualStyleBackColor = true;
            Control_User_Management_Button_Back.Visible = false;
            // 
            // Control_User_Management_Button_Home
            // 
            Control_User_Management_Button_Home.AutoSize = true;
            Control_User_Management_Button_Home.FlatStyle = FlatStyle.Flat;
            Control_User_Management_Button_Home.Font = new Font("Segoe UI", 10F);
            Control_User_Management_Button_Home.Location = new Point(456, 3);
            Control_User_Management_Button_Home.Name = "Control_User_Management_Button_Home";
            Control_User_Management_Button_Home.Size = new Size(65, 31);
            Control_User_Management_Button_Home.TabIndex = 1;
            Control_User_Management_Button_Home.Text = "🏠 Home";
            Control_User_Management_Button_Home.UseVisualStyleBackColor = true;
            // 
            // Control_User_Management_Panel_Add
            // 
            Control_User_Management_Panel_Add.AutoSize = true;
            Control_User_Management_Panel_Add.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_User_Management_Panel_Add.Controls.Add(Control_User_Management_TableLayout_AddCard);
            Control_User_Management_Panel_Add.Dock = DockStyle.Fill;
            Control_User_Management_Panel_Add.Location = new Point(0, 0);
            Control_User_Management_Panel_Add.Name = "Control_User_Management_Panel_Add";
            Control_User_Management_Panel_Add.Size = new Size(524, 444);
            Control_User_Management_Panel_Add.TabIndex = 1;
            // 
            // Control_User_Management_Panel_Edit
            // 
            Control_User_Management_Panel_Edit.AutoSize = true;
            Control_User_Management_Panel_Edit.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_User_Management_Panel_Edit.Controls.Add(Control_User_Management_TableLayout_EditCard);
            Control_User_Management_Panel_Edit.Dock = DockStyle.Fill;
            Control_User_Management_Panel_Edit.Location = new Point(0, 0);
            Control_User_Management_Panel_Edit.Name = "Control_User_Management_Panel_Edit";
            Control_User_Management_Panel_Edit.Size = new Size(524, 444);
            Control_User_Management_Panel_Edit.TabIndex = 2;
            // 
            // Control_User_Management_Panel_Remove
            // 
            Control_User_Management_Panel_Remove.AutoSize = true;
            Control_User_Management_Panel_Remove.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_User_Management_Panel_Remove.Controls.Add(Control_User_Management_TableLayout_RemoveCard);
            Control_User_Management_Panel_Remove.Dock = DockStyle.Fill;
            Control_User_Management_Panel_Remove.Location = new Point(0, 0);
            Control_User_Management_Panel_Remove.Name = "Control_User_Management_Panel_Remove";
            Control_User_Management_Panel_Remove.Size = new Size(524, 444);
            Control_User_Management_Panel_Remove.TabIndex = 3;
            // 
            // Control_User_Management_TableLayout_EditCard
            // 
            Control_User_Management_TableLayout_EditCard.AutoSize = true;
            Control_User_Management_TableLayout_EditCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_User_Management_TableLayout_EditCard.ColumnCount = 1;
            Control_User_Management_TableLayout_EditCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_User_Management_TableLayout_EditCard.Controls.Add(Control_User_Management_Control_EditUser, 0, 0);
            Control_User_Management_TableLayout_EditCard.Dock = DockStyle.Fill;
            Control_User_Management_TableLayout_EditCard.Location = new Point(0, 0);
            Control_User_Management_TableLayout_EditCard.Name = "Control_User_Management_TableLayout_EditCard";
            Control_User_Management_TableLayout_EditCard.RowCount = 1;
            Control_User_Management_TableLayout_EditCard.RowStyles.Add(new RowStyle());
            Control_User_Management_TableLayout_EditCard.Size = new Size(524, 444);
            Control_User_Management_TableLayout_EditCard.TabIndex = 8;
            // 
            // Control_User_Management_Control_EditUser
            // 
            Control_User_Management_Control_EditUser.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_User_Management_Control_EditUser.Dock = DockStyle.Fill;
            Control_User_Management_Control_EditUser.Location = new Point(3, 3);
            Control_User_Management_Control_EditUser.Name = "Control_User_Management_Control_EditUser";
            Control_User_Management_Control_EditUser.Size = new Size(518, 438);
            Control_User_Management_Control_EditUser.TabIndex = 5;
            // 
            // Control_User_Management_TableLayout_RemoveCard
            // 
            Control_User_Management_TableLayout_RemoveCard.AutoSize = true;
            Control_User_Management_TableLayout_RemoveCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_User_Management_TableLayout_RemoveCard.ColumnCount = 1;
            Control_User_Management_TableLayout_RemoveCard.ColumnStyles.Add(new ColumnStyle());
            Control_User_Management_TableLayout_RemoveCard.Controls.Add(Control_User_Management_Control_RemoveUser, 0, 0);
            Control_User_Management_TableLayout_RemoveCard.Dock = DockStyle.Fill;
            Control_User_Management_TableLayout_RemoveCard.Location = new Point(0, 0);
            Control_User_Management_TableLayout_RemoveCard.Name = "Control_User_Management_TableLayout_RemoveCard";
            Control_User_Management_TableLayout_RemoveCard.RowCount = 1;
            Control_User_Management_TableLayout_RemoveCard.RowStyles.Add(new RowStyle());
            Control_User_Management_TableLayout_RemoveCard.Size = new Size(524, 444);
            Control_User_Management_TableLayout_RemoveCard.TabIndex = 7;
            // 
            // Control_User_Management_Control_RemoveUser
            // 
            Control_User_Management_Control_RemoveUser.AutoSize = true;
            Control_User_Management_Control_RemoveUser.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_User_Management_Control_RemoveUser.Dock = DockStyle.Fill;
            Control_User_Management_Control_RemoveUser.Location = new Point(3, 3);
            Control_User_Management_Control_RemoveUser.Name = "Control_User_Management_Control_RemoveUser";
            Control_User_Management_Control_RemoveUser.Size = new Size(518, 438);
            Control_User_Management_Control_RemoveUser.TabIndex = 6;
            // 
            // Control_User_Management_TableLayout_AddCard
            // 
            Control_User_Management_TableLayout_AddCard.AutoSize = true;
            Control_User_Management_TableLayout_AddCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_User_Management_TableLayout_AddCard.ColumnCount = 1;
            Control_User_Management_TableLayout_AddCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_User_Management_TableLayout_AddCard.Controls.Add(Control_User_Management_Control_AddUser, 0, 0);
            Control_User_Management_TableLayout_AddCard.Dock = DockStyle.Fill;
            Control_User_Management_TableLayout_AddCard.Location = new Point(0, 0);
            Control_User_Management_TableLayout_AddCard.Name = "Control_User_Management_TableLayout_AddCard";
            Control_User_Management_TableLayout_AddCard.RowCount = 1;
            Control_User_Management_TableLayout_AddCard.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_User_Management_TableLayout_AddCard.Size = new Size(524, 444);
            Control_User_Management_TableLayout_AddCard.TabIndex = 9;
            // 
            // Control_User_Management_Control_AddUser
            // 
            Control_User_Management_Control_AddUser.Dock = DockStyle.Fill;
            Control_User_Management_Control_AddUser.Location = new Point(3, 3);
            Control_User_Management_Control_AddUser.Name = "Control_User_Management_Control_AddUser";
            Control_User_Management_Control_AddUser.Size = new Size(518, 438);
            Control_User_Management_Control_AddUser.TabIndex = 4;
            // 
            // Control_User_Management
            // 
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(Control_User_Management_TableLayout_Main);
            Name = "Control_User_Management";
            Size = new Size(530, 493);
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            Control_User_Management_TableLayout_Main.ResumeLayout(false);
            Control_User_Management_TableLayout_Main.PerformLayout();
            Control_User_Management_Panel_Container.ResumeLayout(false);
            Control_User_Management_Panel_Container.PerformLayout();
            Control_User_Management_Panel_Home.ResumeLayout(false);
            Control_User_Management_Panel_Home.PerformLayout();
            Control_User_Management_TableLayout_Home.ResumeLayout(false);
            Control_User_Management_Panel_HomeTile_Add.ResumeLayout(false);
            Control_User_Management_Panel_HomeTile_Add.PerformLayout();
            Control_User_Management_TableLayout_HomeTile_Add.ResumeLayout(false);
            Control_User_Management_TableLayout_HomeTile_Add.PerformLayout();
            Control_User_Management_Panel_HomeTile_Edit.ResumeLayout(false);
            Control_User_Management_Panel_HomeTile_Edit.PerformLayout();
            Control_User_Management_TableLayout_HomeTile_Edit.ResumeLayout(false);
            Control_User_Management_TableLayout_HomeTile_Edit.PerformLayout();
            Control_User_Management_Panel_HomeTile_Remove.ResumeLayout(false);
            Control_User_Management_Panel_HomeTile_Remove.PerformLayout();
            Control_User_Management_TableLayout_HomeTile_Remove.ResumeLayout(false);
            Control_User_Management_TableLayout_HomeTile_Remove.PerformLayout();
            Control_User_Management_TableLayout_BackButton.ResumeLayout(false);
            Control_User_Management_TableLayout_BackButton.PerformLayout();
            Control_User_Management_Panel_Add.ResumeLayout(false);
            Control_User_Management_Panel_Add.PerformLayout();
            Control_User_Management_Panel_Edit.ResumeLayout(false);
            Control_User_Management_Panel_Edit.PerformLayout();
            Control_User_Management_Panel_Remove.ResumeLayout(false);
            Control_User_Management_Panel_Remove.PerformLayout();
            Control_User_Management_TableLayout_EditCard.ResumeLayout(false);
            Control_User_Management_TableLayout_RemoveCard.ResumeLayout(false);
            Control_User_Management_TableLayout_RemoveCard.PerformLayout();
            Control_User_Management_TableLayout_AddCard.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel Control_User_Management_Panel_Remove;
        private Panel Control_User_Management_Panel_Edit;
        private Panel Control_User_Management_Panel_Add;
        private TableLayoutPanel Control_User_Management_TableLayout_RemoveCard;
        private Control_Remove_User Control_User_Management_Control_RemoveUser;
        private TableLayoutPanel Control_User_Management_TableLayout_EditCard;
        private Control_Edit_User Control_User_Management_Control_EditUser;
        private TableLayoutPanel Control_User_Management_TableLayout_AddCard;
        private Control_Add_User Control_User_Management_Control_AddUser;
    }
}