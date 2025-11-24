using System.Drawing;
using System.Windows.Forms;

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    partial class Control_Add_User
    {
        #region Fields

        private System.ComponentModel.IContainer components = null;

        #endregion

        private System.Windows.Forms.GroupBox Control_Add_User_GroupBox_UserInfo;
        private System.Windows.Forms.Label Control_Add_User_Label_FirstName;
        private System.Windows.Forms.TextBox Control_Add_User_TextBox_FirstName;
        private System.Windows.Forms.Label Control_Add_User_Label_LastName;
        private System.Windows.Forms.TextBox Control_Add_User_TextBox_LastName;
        private System.Windows.Forms.Label Control_Add_User_Label_UserName;
        private System.Windows.Forms.TextBox Control_Add_User_TextBox_UserName;
        private System.Windows.Forms.Label Control_Add_User_Label_Shift;
        private System.Windows.Forms.ComboBox Control_Add_User_ComboBox_Shift;
        private System.Windows.Forms.Label Control_Add_User_Label_Pin;
        private System.Windows.Forms.TextBox Control_Add_User_TextBox_Pin;
        private System.Windows.Forms.GroupBox Control_Add_User_GroupBox_VisualInfo;
        private System.Windows.Forms.CheckBox Control_Add_User_CheckBox_VisualAccess;
        private System.Windows.Forms.Label Control_Add_User_Label_VisualUserName;
        private System.Windows.Forms.TextBox Control_Add_User_TextBox_VisualUserName;
        private System.Windows.Forms.Label Control_Add_User_Label_VisualPassword;
        private System.Windows.Forms.TextBox Control_Add_User_TextBox_VisualPassword;
        private System.Windows.Forms.GroupBox Control_Add_User_GroupBox_UserPrivileges;
        private System.Windows.Forms.RadioButton Control_Add_User_RadioButton_ReadOnly;
        private System.Windows.Forms.RadioButton Control_Add_User_RadioButton_NormalUser;
        private System.Windows.Forms.RadioButton Control_Add_User_RadioButton_Administrator;
        private Label Control_Add_User_Label_UserNameWarning;
        private Button Control_Add_User_Button_Save;
        private Button Control_Add_User_Button_Clear;
        private CheckBox Control_Add_User_CheckBox_ViewHidePasswords;

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

        private void InitializeComponent()
        {
            Control_Add_User_GroupBox_UserInfo = new GroupBox();
            Control_Add_User_TableLayout_UserInfo = new TableLayoutPanel();
            Control_Add_User_TableLayoutPanel_UserInfo = new TableLayoutPanel();
            Control_Add_User_ComboBox_Shift = new ComboBox();
            Control_Add_User_TextBox_LastName = new TextBox();
            Control_Add_User_TextBox_FirstName = new TextBox();
            Control_Add_User_Label_FirstName = new Label();
            Control_Add_User_Label_LastName = new Label();
            Control_Add_User_Label_Shift = new Label();
            Control_Add_User_GroupBox_LoginInfo = new GroupBox();
            Control_Add_User_TableLayoutPanel_LoginInfo = new TableLayoutPanel();
            Control_Add_User_TextBox_Pin = new TextBox();
            Control_Add_User_Label_UserNameWarning = new Label();
            Control_Add_User_TextBox_UserName = new TextBox();
            Control_Add_User_Label_UserName = new Label();
            Control_Add_User_Label_Pin = new Label();
            Control_Add_User_GroupBox_VisualInfo = new GroupBox();
            Control_Add_User_TableLayoutPanel_VisualInfo = new TableLayoutPanel();
            Control_Add_User_TextBox_VisualPassword = new TextBox();
            Control_Add_User_TextBox_VisualUserName = new TextBox();
            Control_Add_User_Label_VisualUserName = new Label();
            Control_Add_User_Label_VisualPassword = new Label();
            Control_Add_User_CheckBox_VisualAccess = new CheckBox();
            Control_Add_User_GroupBox_UserPrivileges = new GroupBox();
            Control_Add_User_TableLayoutPanel_AdminPanel = new TableLayoutPanel();
            Control_Add_User_RadioButton_Administrator = new RadioButton();
            Control_Add_User_RadioButton_NormalUser = new RadioButton();
            Control_Add_User_RadioButton_ReadOnly = new RadioButton();
            Control_Add_User_RadioButton_Developer = new RadioButton();
            Control_Add_User_CheckBox_ViewHidePasswords = new CheckBox();
            Control_Add_User_Button_Clear = new Button();
            Control_Add_User_Button_Save = new Button();
            Control_Add_User_TableLayoutPanel_Buttons = new TableLayoutPanel();
            Control_Add_User_TableLayoutPanel_Master = new TableLayoutPanel();
            Control_Add_User_GroupBox_UserInfo.SuspendLayout();
            Control_Add_User_TableLayout_UserInfo.SuspendLayout();
            Control_Add_User_TableLayoutPanel_UserInfo.SuspendLayout();
            Control_Add_User_GroupBox_LoginInfo.SuspendLayout();
            Control_Add_User_TableLayoutPanel_LoginInfo.SuspendLayout();
            Control_Add_User_GroupBox_VisualInfo.SuspendLayout();
            Control_Add_User_TableLayoutPanel_VisualInfo.SuspendLayout();
            Control_Add_User_GroupBox_UserPrivileges.SuspendLayout();
            Control_Add_User_TableLayoutPanel_AdminPanel.SuspendLayout();
            Control_Add_User_TableLayoutPanel_Buttons.SuspendLayout();
            Control_Add_User_TableLayoutPanel_Master.SuspendLayout();
            SuspendLayout();
            //
            // Control_Add_User_GroupBox_UserInfo
            //
            Control_Add_User_GroupBox_UserInfo.AutoSize = true;
            Control_Add_User_GroupBox_UserInfo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_Add_User_GroupBox_UserInfo.Controls.Add(Control_Add_User_TableLayout_UserInfo);
            Control_Add_User_GroupBox_UserInfo.Dock = DockStyle.Fill;
            Control_Add_User_GroupBox_UserInfo.Location = new Point(3, 3);
            Control_Add_User_GroupBox_UserInfo.Name = "Control_Add_User_GroupBox_UserInfo";
            Control_Add_User_GroupBox_UserInfo.Size = new Size(354, 236);
            Control_Add_User_GroupBox_UserInfo.TabIndex = 0;
            Control_Add_User_GroupBox_UserInfo.TabStop = false;
            Control_Add_User_GroupBox_UserInfo.Text = "User Information";
            //
            // Control_Add_User_TableLayout_UserInfo
            //
            Control_Add_User_TableLayout_UserInfo.AutoSize = true;
            Control_Add_User_TableLayout_UserInfo.ColumnCount = 1;
            Control_Add_User_TableLayout_UserInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_Add_User_TableLayout_UserInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            Control_Add_User_TableLayout_UserInfo.Controls.Add(Control_Add_User_TableLayoutPanel_UserInfo, 0, 0);
            Control_Add_User_TableLayout_UserInfo.Controls.Add(Control_Add_User_GroupBox_LoginInfo, 0, 1);
            Control_Add_User_TableLayout_UserInfo.Dock = DockStyle.Fill;
            Control_Add_User_TableLayout_UserInfo.Location = new Point(3, 19);
            Control_Add_User_TableLayout_UserInfo.Name = "tableLayoutPanel5";
            Control_Add_User_TableLayout_UserInfo.RowCount = 2;
            Control_Add_User_TableLayout_UserInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Control_Add_User_TableLayout_UserInfo.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Control_Add_User_TableLayout_UserInfo.Size = new Size(348, 214);
            Control_Add_User_TableLayout_UserInfo.TabIndex = 0;
            //
            // Control_Add_User_TableLayoutPanel_UserInfo
            //
            Control_Add_User_TableLayoutPanel_UserInfo.AutoSize = true;
            Control_Add_User_TableLayoutPanel_UserInfo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_Add_User_TableLayoutPanel_UserInfo.ColumnCount = 2;
            Control_Add_User_TableLayoutPanel_UserInfo.ColumnStyles.Add(new ColumnStyle());
            Control_Add_User_TableLayoutPanel_UserInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_Add_User_TableLayoutPanel_UserInfo.Controls.Add(Control_Add_User_ComboBox_Shift, 1, 2);
            Control_Add_User_TableLayoutPanel_UserInfo.Controls.Add(Control_Add_User_TextBox_LastName, 1, 1);
            Control_Add_User_TableLayoutPanel_UserInfo.Controls.Add(Control_Add_User_TextBox_FirstName, 1, 0);
            Control_Add_User_TableLayoutPanel_UserInfo.Controls.Add(Control_Add_User_Label_FirstName, 0, 0);
            Control_Add_User_TableLayoutPanel_UserInfo.Controls.Add(Control_Add_User_Label_LastName, 0, 1);
            Control_Add_User_TableLayoutPanel_UserInfo.Controls.Add(Control_Add_User_Label_Shift, 0, 2);
            Control_Add_User_TableLayoutPanel_UserInfo.Dock = DockStyle.Fill;
            Control_Add_User_TableLayoutPanel_UserInfo.Location = new Point(3, 3);
            Control_Add_User_TableLayoutPanel_UserInfo.Name = "Control_Add_User_TableLayoutPanel_UserInfo";
            Control_Add_User_TableLayoutPanel_UserInfo.RowCount = 3;
            Control_Add_User_TableLayoutPanel_UserInfo.RowStyles.Add(new RowStyle());
            Control_Add_User_TableLayoutPanel_UserInfo.RowStyles.Add(new RowStyle());
            Control_Add_User_TableLayoutPanel_UserInfo.RowStyles.Add(new RowStyle());
            Control_Add_User_TableLayoutPanel_UserInfo.Size = new Size(342, 101);
            Control_Add_User_TableLayoutPanel_UserInfo.TabIndex = 0;
            //
            // Control_Add_User_ComboBox_Shift
            //
            Control_Add_User_ComboBox_Shift.Dock = DockStyle.Fill;
            Control_Add_User_ComboBox_Shift.DropDownStyle = ComboBoxStyle.DropDownList;
            Control_Add_User_ComboBox_Shift.Location = new Point(76, 61);
            Control_Add_User_ComboBox_Shift.Name = "Control_Add_User_ComboBox_Shift";
            Control_Add_User_ComboBox_Shift.Size = new Size(263, 23);
            Control_Add_User_ComboBox_Shift.TabIndex = 2;
            //
            // Control_Add_User_TextBox_LastName
            //
            Control_Add_User_TextBox_LastName.Dock = DockStyle.Fill;
            Control_Add_User_TextBox_LastName.Location = new Point(76, 32);
            Control_Add_User_TextBox_LastName.Name = "Control_Add_User_TextBox_LastName";
            Control_Add_User_TextBox_LastName.Size = new Size(263, 23);
            Control_Add_User_TextBox_LastName.TabIndex = 1;
            //
            // Control_Add_User_TextBox_FirstName
            //
            Control_Add_User_TextBox_FirstName.Dock = DockStyle.Fill;
            Control_Add_User_TextBox_FirstName.Location = new Point(76, 3);
            Control_Add_User_TextBox_FirstName.Name = "Control_Add_User_TextBox_FirstName";
            Control_Add_User_TextBox_FirstName.Size = new Size(263, 23);
            Control_Add_User_TextBox_FirstName.TabIndex = 0;
            //
            // Control_Add_User_Label_FirstName
            //
            Control_Add_User_Label_FirstName.AutoSize = true;
            Control_Add_User_Label_FirstName.Dock = DockStyle.Fill;
            Control_Add_User_Label_FirstName.Location = new Point(3, 3);
            Control_Add_User_Label_FirstName.Margin = new Padding(3);
            Control_Add_User_Label_FirstName.Name = "Control_Add_User_Label_FirstName";
            Control_Add_User_Label_FirstName.Size = new Size(67, 23);
            Control_Add_User_Label_FirstName.TabIndex = 0;
            Control_Add_User_Label_FirstName.TabStop = false;
            Control_Add_User_Label_FirstName.Text = "First Name:";
            //
            // Control_Add_User_Label_LastName
            //
            Control_Add_User_Label_LastName.AutoSize = true;
            Control_Add_User_Label_LastName.Dock = DockStyle.Fill;
            Control_Add_User_Label_LastName.Location = new Point(3, 32);
            Control_Add_User_Label_LastName.Margin = new Padding(3);
            Control_Add_User_Label_LastName.Name = "Control_Add_User_Label_LastName";
            Control_Add_User_Label_LastName.Size = new Size(67, 23);
            Control_Add_User_Label_LastName.TabIndex = 2;
            Control_Add_User_Label_LastName.TabStop = false;
            Control_Add_User_Label_LastName.Text = "Last Name:";
            //
            // Control_Add_User_Label_Shift
            //
            Control_Add_User_Label_Shift.AutoSize = true;
            Control_Add_User_Label_Shift.Dock = DockStyle.Fill;
            Control_Add_User_Label_Shift.Location = new Point(3, 61);
            Control_Add_User_Label_Shift.Margin = new Padding(3);
            Control_Add_User_Label_Shift.Name = "Control_Add_User_Label_Shift";
            Control_Add_User_Label_Shift.Size = new Size(67, 37);
            Control_Add_User_Label_Shift.TabIndex = 6;
            Control_Add_User_Label_Shift.TabStop = false;
            Control_Add_User_Label_Shift.Text = "Shift:";
            //
            // Control_Add_User_GroupBox_LoginInfo
            //
            Control_Add_User_GroupBox_LoginInfo.AutoSize = true;
            Control_Add_User_GroupBox_LoginInfo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_Add_User_GroupBox_LoginInfo.Controls.Add(Control_Add_User_TableLayoutPanel_LoginInfo);
            Control_Add_User_GroupBox_LoginInfo.Dock = DockStyle.Fill;
            Control_Add_User_GroupBox_LoginInfo.Location = new Point(3, 110);
            Control_Add_User_GroupBox_LoginInfo.Name = "Control_Add_User_GroupBox_LoginInfo";
            Control_Add_User_GroupBox_LoginInfo.Size = new Size(342, 101);
            Control_Add_User_GroupBox_LoginInfo.TabIndex = 1;
            Control_Add_User_GroupBox_LoginInfo.TabStop = false;
            Control_Add_User_GroupBox_LoginInfo.Text = "Login Information";
            //
            // Control_Add_User_TableLayoutPanel_LoginInfo
            //
            Control_Add_User_TableLayoutPanel_LoginInfo.AutoSize = true;
            Control_Add_User_TableLayoutPanel_LoginInfo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_Add_User_TableLayoutPanel_LoginInfo.ColumnCount = 2;
            Control_Add_User_TableLayoutPanel_LoginInfo.ColumnStyles.Add(new ColumnStyle());
            Control_Add_User_TableLayoutPanel_LoginInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_Add_User_TableLayoutPanel_LoginInfo.Controls.Add(Control_Add_User_TextBox_Pin, 1, 1);
            Control_Add_User_TableLayoutPanel_LoginInfo.Controls.Add(Control_Add_User_Label_UserNameWarning, 0, 2);
            Control_Add_User_TableLayoutPanel_LoginInfo.Controls.Add(Control_Add_User_TextBox_UserName, 1, 0);
            Control_Add_User_TableLayoutPanel_LoginInfo.Controls.Add(Control_Add_User_Label_UserName, 0, 0);
            Control_Add_User_TableLayoutPanel_LoginInfo.Controls.Add(Control_Add_User_Label_Pin, 0, 1);
            Control_Add_User_TableLayoutPanel_LoginInfo.Dock = DockStyle.Fill;
            Control_Add_User_TableLayoutPanel_LoginInfo.Location = new Point(3, 19);
            Control_Add_User_TableLayoutPanel_LoginInfo.Name = "Control_Add_User_TableLayoutPanel_LoginInfo";
            Control_Add_User_TableLayoutPanel_LoginInfo.RowCount = 3;
            Control_Add_User_TableLayoutPanel_LoginInfo.RowStyles.Add(new RowStyle());
            Control_Add_User_TableLayoutPanel_LoginInfo.RowStyles.Add(new RowStyle());
            Control_Add_User_TableLayoutPanel_LoginInfo.RowStyles.Add(new RowStyle());
            Control_Add_User_TableLayoutPanel_LoginInfo.Size = new Size(336, 79);
            Control_Add_User_TableLayoutPanel_LoginInfo.TabIndex = 0;
            //
            // Control_Add_User_TextBox_Pin
            //
            Control_Add_User_TextBox_Pin.Dock = DockStyle.Fill;
            Control_Add_User_TextBox_Pin.Location = new Point(77, 32);
            Control_Add_User_TextBox_Pin.Name = "Control_Add_User_TextBox_Pin";
            Control_Add_User_TextBox_Pin.Size = new Size(256, 23);
            Control_Add_User_TextBox_Pin.TabIndex = 1;
            //
            // Control_Add_User_Label_UserNameWarning
            //
            Control_Add_User_Label_UserNameWarning.AutoSize = true;
            Control_Add_User_TableLayoutPanel_LoginInfo.SetColumnSpan(Control_Add_User_Label_UserNameWarning, 2);
            Control_Add_User_Label_UserNameWarning.Dock = DockStyle.Fill;
            Control_Add_User_Label_UserNameWarning.ForeColor = Color.Red;
            Control_Add_User_Label_UserNameWarning.Location = new Point(3, 61);
            Control_Add_User_Label_UserNameWarning.Margin = new Padding(3);
            Control_Add_User_Label_UserNameWarning.Name = "Control_Add_User_Label_UserNameWarning";
            Control_Add_User_Label_UserNameWarning.Size = new Size(330, 15);
            Control_Add_User_Label_UserNameWarning.TabIndex = 10;
            Control_Add_User_Label_UserNameWarning.TabStop = false;
            Control_Add_User_Label_UserNameWarning.TextAlign = ContentAlignment.MiddleCenter;
            //
            // Control_Add_User_TextBox_UserName
            //
            Control_Add_User_TextBox_UserName.Dock = DockStyle.Fill;
            Control_Add_User_TextBox_UserName.Location = new Point(77, 3);
            Control_Add_User_TextBox_UserName.Name = "Control_Add_User_TextBox_UserName";
            Control_Add_User_TextBox_UserName.Size = new Size(256, 23);
            Control_Add_User_TextBox_UserName.TabIndex = 0;
            //
            // Control_Add_User_Label_UserName
            //
            Control_Add_User_Label_UserName.AutoSize = true;
            Control_Add_User_Label_UserName.Dock = DockStyle.Fill;
            Control_Add_User_Label_UserName.Location = new Point(3, 3);
            Control_Add_User_Label_UserName.Margin = new Padding(3);
            Control_Add_User_Label_UserName.Name = "Control_Add_User_Label_UserName";
            Control_Add_User_Label_UserName.Size = new Size(68, 23);
            Control_Add_User_Label_UserName.TabIndex = 4;
            Control_Add_User_Label_UserName.TabStop = false;
            Control_Add_User_Label_UserName.Text = "User Name:";
            Control_Add_User_Label_UserName.TextAlign = ContentAlignment.MiddleRight;
            //
            // Control_Add_User_Label_Pin
            //
            Control_Add_User_Label_Pin.AutoSize = true;
            Control_Add_User_Label_Pin.Dock = DockStyle.Fill;
            Control_Add_User_Label_Pin.Location = new Point(3, 32);
            Control_Add_User_Label_Pin.Margin = new Padding(3);
            Control_Add_User_Label_Pin.Name = "Control_Add_User_Label_Pin";
            Control_Add_User_Label_Pin.Size = new Size(68, 23);
            Control_Add_User_Label_Pin.TabIndex = 8;
            Control_Add_User_Label_Pin.TabStop = false;
            Control_Add_User_Label_Pin.Text = "Pin:";
            Control_Add_User_Label_Pin.TextAlign = ContentAlignment.MiddleRight;
            //
            // Control_Add_User_GroupBox_VisualInfo
            //
            Control_Add_User_GroupBox_VisualInfo.AutoSize = true;
            Control_Add_User_GroupBox_VisualInfo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_Add_User_TableLayoutPanel_Master.SetColumnSpan(Control_Add_User_GroupBox_VisualInfo, 2);
            Control_Add_User_GroupBox_VisualInfo.Controls.Add(Control_Add_User_TableLayoutPanel_VisualInfo);
            Control_Add_User_GroupBox_VisualInfo.Dock = DockStyle.Fill;
            Control_Add_User_GroupBox_VisualInfo.Location = new Point(3, 245);
            Control_Add_User_GroupBox_VisualInfo.Name = "Control_Add_User_GroupBox_VisualInfo";
            Control_Add_User_GroupBox_VisualInfo.Size = new Size(479, 105);
            Control_Add_User_GroupBox_VisualInfo.TabIndex = 2;
            Control_Add_User_GroupBox_VisualInfo.TabStop = false;
            Control_Add_User_GroupBox_VisualInfo.Text = "Infor VISUAL Information";
            //
            // Control_Add_User_TableLayoutPanel_VisualInfo
            //
            Control_Add_User_TableLayoutPanel_VisualInfo.AutoSize = true;
            Control_Add_User_TableLayoutPanel_VisualInfo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_Add_User_TableLayoutPanel_VisualInfo.ColumnCount = 2;
            Control_Add_User_TableLayoutPanel_VisualInfo.ColumnStyles.Add(new ColumnStyle());
            Control_Add_User_TableLayoutPanel_VisualInfo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_Add_User_TableLayoutPanel_VisualInfo.Controls.Add(Control_Add_User_TextBox_VisualPassword, 1, 2);
            Control_Add_User_TableLayoutPanel_VisualInfo.Controls.Add(Control_Add_User_TextBox_VisualUserName, 1, 1);
            Control_Add_User_TableLayoutPanel_VisualInfo.Controls.Add(Control_Add_User_Label_VisualUserName, 0, 1);
            Control_Add_User_TableLayoutPanel_VisualInfo.Controls.Add(Control_Add_User_Label_VisualPassword, 0, 2);
            Control_Add_User_TableLayoutPanel_VisualInfo.Controls.Add(Control_Add_User_CheckBox_VisualAccess, 0, 0);
            Control_Add_User_TableLayoutPanel_VisualInfo.Dock = DockStyle.Fill;
            Control_Add_User_TableLayoutPanel_VisualInfo.Location = new Point(3, 19);
            Control_Add_User_TableLayoutPanel_VisualInfo.Name = "Control_Add_User_TableLayoutPanel_VisualInfo";
            Control_Add_User_TableLayoutPanel_VisualInfo.RowCount = 3;
            Control_Add_User_TableLayoutPanel_VisualInfo.RowStyles.Add(new RowStyle());
            Control_Add_User_TableLayoutPanel_VisualInfo.RowStyles.Add(new RowStyle());
            Control_Add_User_TableLayoutPanel_VisualInfo.RowStyles.Add(new RowStyle());
            Control_Add_User_TableLayoutPanel_VisualInfo.Size = new Size(473, 83);
            Control_Add_User_TableLayoutPanel_VisualInfo.TabIndex = 15;
            //
            // Control_Add_User_TextBox_VisualPassword
            //
            Control_Add_User_TextBox_VisualPassword.Dock = DockStyle.Fill;
            Control_Add_User_TextBox_VisualPassword.Location = new Point(77, 57);
            Control_Add_User_TextBox_VisualPassword.Name = "Control_Add_User_TextBox_VisualPassword";
            Control_Add_User_TextBox_VisualPassword.Size = new Size(393, 23);
            Control_Add_User_TextBox_VisualPassword.TabIndex = 2;
            //
            // Control_Add_User_TextBox_VisualUserName
            //
            Control_Add_User_TextBox_VisualUserName.Dock = DockStyle.Fill;
            Control_Add_User_TextBox_VisualUserName.Location = new Point(77, 28);
            Control_Add_User_TextBox_VisualUserName.Name = "Control_Add_User_TextBox_VisualUserName";
            Control_Add_User_TextBox_VisualUserName.Size = new Size(393, 23);
            Control_Add_User_TextBox_VisualUserName.TabIndex = 1;
            //
            // Control_Add_User_Label_VisualUserName
            //
            Control_Add_User_Label_VisualUserName.AutoSize = true;
            Control_Add_User_Label_VisualUserName.Dock = DockStyle.Fill;
            Control_Add_User_Label_VisualUserName.Location = new Point(3, 28);
            Control_Add_User_Label_VisualUserName.Margin = new Padding(3);
            Control_Add_User_Label_VisualUserName.Name = "Control_Add_User_Label_VisualUserName";
            Control_Add_User_Label_VisualUserName.Size = new Size(68, 23);
            Control_Add_User_Label_VisualUserName.TabIndex = 1;
            Control_Add_User_Label_VisualUserName.TabStop = false;
            Control_Add_User_Label_VisualUserName.Text = "User Name:";
            Control_Add_User_Label_VisualUserName.TextAlign = ContentAlignment.MiddleRight;
            //
            // Control_Add_User_Label_VisualPassword
            //
            Control_Add_User_Label_VisualPassword.AutoSize = true;
            Control_Add_User_Label_VisualPassword.Dock = DockStyle.Fill;
            Control_Add_User_Label_VisualPassword.Location = new Point(3, 57);
            Control_Add_User_Label_VisualPassword.Margin = new Padding(3);
            Control_Add_User_Label_VisualPassword.Name = "Control_Add_User_Label_VisualPassword";
            Control_Add_User_Label_VisualPassword.Size = new Size(68, 23);
            Control_Add_User_Label_VisualPassword.TabIndex = 3;
            Control_Add_User_Label_VisualPassword.TabStop = false;
            Control_Add_User_Label_VisualPassword.Text = "Password:";
            Control_Add_User_Label_VisualPassword.TextAlign = ContentAlignment.MiddleRight;
            //
            // Control_Add_User_CheckBox_VisualAccess
            //
            Control_Add_User_CheckBox_VisualAccess.AutoSize = true;
            Control_Add_User_TableLayoutPanel_VisualInfo.SetColumnSpan(Control_Add_User_CheckBox_VisualAccess, 2);
            Control_Add_User_CheckBox_VisualAccess.Dock = DockStyle.Fill;
            Control_Add_User_CheckBox_VisualAccess.Location = new Point(3, 3);
            Control_Add_User_CheckBox_VisualAccess.Name = "Control_Add_User_CheckBox_VisualAccess";
            Control_Add_User_CheckBox_VisualAccess.Size = new Size(467, 19);
            Control_Add_User_CheckBox_VisualAccess.TabIndex = 0;
            Control_Add_User_CheckBox_VisualAccess.Text = "Visual Access?";
            Control_Add_User_CheckBox_VisualAccess.UseVisualStyleBackColor = true;
            //
            // Control_Add_User_GroupBox_UserPrivileges
            //
            Control_Add_User_GroupBox_UserPrivileges.AutoSize = true;
            Control_Add_User_GroupBox_UserPrivileges.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_Add_User_GroupBox_UserPrivileges.Controls.Add(Control_Add_User_TableLayoutPanel_AdminPanel);
            Control_Add_User_GroupBox_UserPrivileges.Dock = DockStyle.Fill;
            Control_Add_User_GroupBox_UserPrivileges.Location = new Point(363, 3);
            Control_Add_User_GroupBox_UserPrivileges.Name = "Control_Add_User_GroupBox_UserPrivileges";
            Control_Add_User_GroupBox_UserPrivileges.Size = new Size(119, 236);
            Control_Add_User_GroupBox_UserPrivileges.TabIndex = 1;
            Control_Add_User_GroupBox_UserPrivileges.TabStop = false;
            Control_Add_User_GroupBox_UserPrivileges.Text = "User Privileges";
            //
            // tableLayoutPanel1
            //
            Control_Add_User_TableLayoutPanel_AdminPanel.AutoSize = true;
            Control_Add_User_TableLayoutPanel_AdminPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_Add_User_TableLayoutPanel_AdminPanel.ColumnCount = 1;
            Control_Add_User_TableLayoutPanel_AdminPanel.ColumnStyles.Add(new ColumnStyle());
            Control_Add_User_TableLayoutPanel_AdminPanel.Controls.Add(Control_Add_User_RadioButton_Administrator, 0, 5);
            Control_Add_User_TableLayoutPanel_AdminPanel.Controls.Add(Control_Add_User_RadioButton_NormalUser, 0, 3);
            Control_Add_User_TableLayoutPanel_AdminPanel.Controls.Add(Control_Add_User_RadioButton_ReadOnly, 0, 1);
            Control_Add_User_TableLayoutPanel_AdminPanel.Controls.Add(Control_Add_User_RadioButton_Developer, 0, 7);
            Control_Add_User_TableLayoutPanel_AdminPanel.Dock = DockStyle.Fill;
            Control_Add_User_TableLayoutPanel_AdminPanel.Location = new Point(3, 19);
            Control_Add_User_TableLayoutPanel_AdminPanel.Name = "Control_Add_User_TableLayoutPanel_AdminPanel";
            Control_Add_User_TableLayoutPanel_AdminPanel.RowCount = 9;
            Control_Add_User_TableLayoutPanel_AdminPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 19.9999981F));
            Control_Add_User_TableLayoutPanel_AdminPanel.RowStyles.Add(new RowStyle());
            Control_Add_User_TableLayoutPanel_AdminPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            Control_Add_User_TableLayoutPanel_AdminPanel.RowStyles.Add(new RowStyle());
            Control_Add_User_TableLayoutPanel_AdminPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            Control_Add_User_TableLayoutPanel_AdminPanel.RowStyles.Add(new RowStyle());
            Control_Add_User_TableLayoutPanel_AdminPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            Control_Add_User_TableLayoutPanel_AdminPanel.RowStyles.Add(new RowStyle());
            Control_Add_User_TableLayoutPanel_AdminPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            Control_Add_User_TableLayoutPanel_AdminPanel.Size = new Size(113, 214);
            Control_Add_User_TableLayoutPanel_AdminPanel.TabIndex = 13;
            //
            // Control_Add_User_RadioButton_Administrator
            //
            Control_Add_User_RadioButton_Administrator.AutoSize = true;
            Control_Add_User_RadioButton_Administrator.Dock = DockStyle.Fill;
            Control_Add_User_RadioButton_Administrator.Location = new Point(3, 119);
            Control_Add_User_RadioButton_Administrator.Name = "Control_Add_User_RadioButton_Administrator";
            Control_Add_User_RadioButton_Administrator.Size = new Size(107, 19);
            Control_Add_User_RadioButton_Administrator.TabIndex = 2;
            Control_Add_User_RadioButton_Administrator.TabStop = true;
            Control_Add_User_RadioButton_Administrator.Text = "Administrator";
            Control_Add_User_RadioButton_Administrator.UseVisualStyleBackColor = true;
            //
            // Control_Add_User_RadioButton_NormalUser
            //
            Control_Add_User_RadioButton_NormalUser.AutoSize = true;
            Control_Add_User_RadioButton_NormalUser.Dock = DockStyle.Fill;
            Control_Add_User_RadioButton_NormalUser.Location = new Point(3, 72);
            Control_Add_User_RadioButton_NormalUser.Name = "Control_Add_User_RadioButton_NormalUser";
            Control_Add_User_RadioButton_NormalUser.Size = new Size(107, 19);
            Control_Add_User_RadioButton_NormalUser.TabIndex = 1;
            Control_Add_User_RadioButton_NormalUser.TabStop = true;
            Control_Add_User_RadioButton_NormalUser.Text = "Normal User";
            Control_Add_User_RadioButton_NormalUser.UseVisualStyleBackColor = true;
            //
            // Control_Add_User_RadioButton_ReadOnly
            //
            Control_Add_User_RadioButton_ReadOnly.AutoSize = true;
            Control_Add_User_RadioButton_ReadOnly.Dock = DockStyle.Fill;
            Control_Add_User_RadioButton_ReadOnly.Location = new Point(3, 25);
            Control_Add_User_RadioButton_ReadOnly.Name = "Control_Add_User_RadioButton_ReadOnly";
            Control_Add_User_RadioButton_ReadOnly.Size = new Size(107, 19);
            Control_Add_User_RadioButton_ReadOnly.TabIndex = 0;
            Control_Add_User_RadioButton_ReadOnly.TabStop = true;
            Control_Add_User_RadioButton_ReadOnly.Text = "Read-Only User";
            Control_Add_User_RadioButton_ReadOnly.UseVisualStyleBackColor = true;
            //
            // Control_Add_User_RadioButton_Developer
            //
            Control_Add_User_RadioButton_Developer.AutoSize = true;
            Control_Add_User_RadioButton_Developer.Dock = DockStyle.Fill;
            Control_Add_User_RadioButton_Developer.Location = new Point(3, 166);
            Control_Add_User_RadioButton_Developer.Name = "Control_Add_User_RadioButton_Developer";
            Control_Add_User_RadioButton_Developer.Size = new Size(107, 19);
            Control_Add_User_RadioButton_Developer.TabIndex = 3;
            Control_Add_User_RadioButton_Developer.TabStop = true;
            Control_Add_User_RadioButton_Developer.Text = "Developer";
            Control_Add_User_RadioButton_Developer.UseVisualStyleBackColor = true;
            //
            // Control_Add_User_CheckBox_ViewHidePasswords
            //
            Control_Add_User_CheckBox_ViewHidePasswords.AutoSize = true;
            Control_Add_User_CheckBox_ViewHidePasswords.Dock = DockStyle.Fill;
            Control_Add_User_CheckBox_ViewHidePasswords.Location = new Point(3, 3);
            Control_Add_User_CheckBox_ViewHidePasswords.Name = "Control_Add_User_CheckBox_ViewHidePasswords";
            Control_Add_User_CheckBox_ViewHidePasswords.Size = new Size(141, 23);
            Control_Add_User_CheckBox_ViewHidePasswords.TabIndex = 2;
            Control_Add_User_CheckBox_ViewHidePasswords.TabStop = false;
            Control_Add_User_CheckBox_ViewHidePasswords.Text = "Show Password Fields";
            Control_Add_User_CheckBox_ViewHidePasswords.UseVisualStyleBackColor = true;
            //
            // Control_Add_User_Button_Clear
            //
            Control_Add_User_Button_Clear.Location = new Point(320, 3);
            Control_Add_User_Button_Clear.Name = "Control_Add_User_Button_Clear";
            Control_Add_User_Button_Clear.Size = new Size(75, 23);
            Control_Add_User_Button_Clear.TabIndex = 1;
            Control_Add_User_Button_Clear.TabStop = false;
            Control_Add_User_Button_Clear.Text = "Clear";
            Control_Add_User_Button_Clear.UseVisualStyleBackColor = true;
            Control_Add_User_Button_Clear.Click += Control_Add_User_Button_Clear_Click;
            //
            // Control_Add_User_Button_Save
            //
            Control_Add_User_Button_Save.Location = new Point(401, 3);
            Control_Add_User_Button_Save.Name = "Control_Add_User_Button_Save";
            Control_Add_User_Button_Save.Size = new Size(75, 23);
            Control_Add_User_Button_Save.TabIndex = 0;
            Control_Add_User_Button_Save.Text = "Save";
            Control_Add_User_Button_Save.UseVisualStyleBackColor = true;
            Control_Add_User_Button_Save.Click += Control_Add_User_Button_Save_Click;
            //
            // tableLayoutPanel4
            //
            Control_Add_User_TableLayoutPanel_Buttons.AutoSize = true;
            Control_Add_User_TableLayoutPanel_Buttons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_Add_User_TableLayoutPanel_Buttons.ColumnCount = 4;
            Control_Add_User_TableLayoutPanel_Master.SetColumnSpan(Control_Add_User_TableLayoutPanel_Buttons, 2);
            Control_Add_User_TableLayoutPanel_Buttons.ColumnStyles.Add(new ColumnStyle());
            Control_Add_User_TableLayoutPanel_Buttons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_Add_User_TableLayoutPanel_Buttons.ColumnStyles.Add(new ColumnStyle());
            Control_Add_User_TableLayoutPanel_Buttons.ColumnStyles.Add(new ColumnStyle());
            Control_Add_User_TableLayoutPanel_Buttons.Controls.Add(Control_Add_User_Button_Save, 3, 0);
            Control_Add_User_TableLayoutPanel_Buttons.Controls.Add(Control_Add_User_Button_Clear, 2, 0);
            Control_Add_User_TableLayoutPanel_Buttons.Controls.Add(Control_Add_User_CheckBox_ViewHidePasswords, 0, 0);
            Control_Add_User_TableLayoutPanel_Buttons.Dock = DockStyle.Fill;
            Control_Add_User_TableLayoutPanel_Buttons.Location = new Point(3, 356);
            Control_Add_User_TableLayoutPanel_Buttons.Name = "Control_Add_User_TableLayoutPanel_Buttons";
            Control_Add_User_TableLayoutPanel_Buttons.RowCount = 1;
            Control_Add_User_TableLayoutPanel_Buttons.RowStyles.Add(new RowStyle());
            Control_Add_User_TableLayoutPanel_Buttons.Size = new Size(479, 29);
            Control_Add_User_TableLayoutPanel_Buttons.TabIndex = 3;
            //
            // tableLayoutPanel7
            //
            Control_Add_User_TableLayoutPanel_Master.AutoSize = true;
            Control_Add_User_TableLayoutPanel_Master.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_Add_User_TableLayoutPanel_Master.ColumnCount = 2;
            Control_Add_User_TableLayoutPanel_Master.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_Add_User_TableLayoutPanel_Master.ColumnStyles.Add(new ColumnStyle());
            Control_Add_User_TableLayoutPanel_Master.Controls.Add(Control_Add_User_GroupBox_UserInfo, 0, 0);
            Control_Add_User_TableLayoutPanel_Master.Controls.Add(Control_Add_User_TableLayoutPanel_Buttons, 0, 3);
            Control_Add_User_TableLayoutPanel_Master.Controls.Add(Control_Add_User_GroupBox_UserPrivileges, 1, 0);
            Control_Add_User_TableLayoutPanel_Master.Controls.Add(Control_Add_User_GroupBox_VisualInfo, 0, 1);
            Control_Add_User_TableLayoutPanel_Master.Dock = DockStyle.Fill;
            Control_Add_User_TableLayoutPanel_Master.Location = new Point(0, 0);
            Control_Add_User_TableLayoutPanel_Master.Name = "Control_Add_User_TableLayoutPanel_Master";
            Control_Add_User_TableLayoutPanel_Master.RowCount = 4;
            Control_Add_User_TableLayoutPanel_Master.RowStyles.Add(new RowStyle());
            Control_Add_User_TableLayoutPanel_Master.RowStyles.Add(new RowStyle());
            Control_Add_User_TableLayoutPanel_Master.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_Add_User_TableLayoutPanel_Master.RowStyles.Add(new RowStyle());
            Control_Add_User_TableLayoutPanel_Master.Size = new Size(485, 388);
            Control_Add_User_TableLayoutPanel_Master.TabIndex = 17;
            //
            // Control_Add_User
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(Control_Add_User_TableLayoutPanel_Master);
            Name = "Control_Add_User";
            Size = new Size(485, 388);
            Control_Add_User_GroupBox_UserInfo.ResumeLayout(false);
            Control_Add_User_GroupBox_UserInfo.PerformLayout();
            Control_Add_User_TableLayout_UserInfo.ResumeLayout(false);
            Control_Add_User_TableLayout_UserInfo.PerformLayout();
            Control_Add_User_TableLayoutPanel_UserInfo.ResumeLayout(false);
            Control_Add_User_TableLayoutPanel_UserInfo.PerformLayout();
            Control_Add_User_GroupBox_LoginInfo.ResumeLayout(false);
            Control_Add_User_GroupBox_LoginInfo.PerformLayout();
            Control_Add_User_TableLayoutPanel_LoginInfo.ResumeLayout(false);
            Control_Add_User_TableLayoutPanel_LoginInfo.PerformLayout();
            Control_Add_User_GroupBox_VisualInfo.ResumeLayout(false);
            Control_Add_User_GroupBox_VisualInfo.PerformLayout();
            Control_Add_User_TableLayoutPanel_VisualInfo.ResumeLayout(false);
            Control_Add_User_TableLayoutPanel_VisualInfo.PerformLayout();
            Control_Add_User_GroupBox_UserPrivileges.ResumeLayout(false);
            Control_Add_User_GroupBox_UserPrivileges.PerformLayout();
            Control_Add_User_TableLayoutPanel_AdminPanel.ResumeLayout(false);
            Control_Add_User_TableLayoutPanel_AdminPanel.PerformLayout();
            Control_Add_User_TableLayoutPanel_Buttons.ResumeLayout(false);
            Control_Add_User_TableLayoutPanel_Buttons.PerformLayout();
            Control_Add_User_TableLayoutPanel_Master.ResumeLayout(false);
            Control_Add_User_TableLayoutPanel_Master.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private TableLayoutPanel Control_Add_User_TableLayoutPanel_AdminPanel;
        private RadioButton Control_Add_User_RadioButton_Developer;
        private TableLayoutPanel Control_Add_User_TableLayoutPanel_LoginInfo;
        private TableLayoutPanel Control_Add_User_TableLayoutPanel_VisualInfo;
        private TableLayoutPanel Control_Add_User_TableLayoutPanel_Buttons;
        private GroupBox Control_Add_User_GroupBox_LoginInfo;
        private TableLayoutPanel Control_Add_User_TableLayout_UserInfo;
        private TableLayoutPanel Control_Add_User_TableLayoutPanel_UserInfo;
        private TableLayoutPanel Control_Add_User_TableLayoutPanel_Master;
    }
}
