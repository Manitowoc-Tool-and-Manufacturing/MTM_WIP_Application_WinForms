using System.Drawing;
using System.Windows.Forms;

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    partial class Control_Edit_User
    {
        #region Fields
        private System.ComponentModel.IContainer components = null;
        #endregion

        private ComboBox Control_Edit_User_ComboBox_Users;
        private Label Control_Edit_User_Label_SelectUser;
        private GroupBox Control_Edit_User_GroupBox_UserInfo;
        private Label Control_Edit_User_Label_FirstName;
        private TextBox Control_Edit_User_TextBox_FirstName;
        private Label Control_Edit_User_Label_LastName;
        private TextBox Control_Edit_User_TextBox_LastName;
        private Label Control_Edit_User_Label_UserName;
        private TextBox Control_Edit_User_TextBox_UserName;
        private Label Control_Edit_User_Label_Shift;
        private ComboBox Control_Edit_User_ComboBox_Shift;
        private Label Control_Edit_User_Label_Pin;
        private TextBox Control_Edit_User_TextBox_Pin;
        private GroupBox Control_Edit_User_GroupBox_VisualInfo;
        private CheckBox Control_Edit_User_CheckBox_VisualAccess;
        private Label Control_Edit_User_Label_VisualUserName;
        private TextBox Control_Edit_User_TextBox_VisualUserName;
        private Label Control_Edit_User_Label_VisualPassword;
        private TextBox Control_Edit_User_TextBox_VisualPassword;
        private GroupBox Control_Edit_User_GroupBox_UserPrivileges;
        private RadioButton Control_Edit_User_RadioButton_ReadOnly;
        private RadioButton Control_Edit_User_RadioButton_NormalUser;
        private RadioButton Control_Edit_User_RadioButton_Administrator;
        private Label Control_Edit_User_Label_UserNameWarning;
        private Button Control_Edit_User_Button_Save;
        private Button Control_Edit_User_Button_Clear;
        private CheckBox Control_Edit_User_CheckBox_ViewHidePasswords;

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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            Control_Edit_User_ComboBox_Users = new ComboBox();
            Control_Edit_User_Label_SelectUser = new Label();
            Control_Edit_User_GroupBox_UserInfo = new GroupBox();
            tableLayoutPanel4 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            Control_Edit_User_Label_FirstName = new Label();
            Control_Edit_User_Label_LastName = new Label();
            Control_Edit_User_ComboBox_Shift = new ComboBox();
            Control_Edit_User_TextBox_LastName = new TextBox();
            Control_Edit_User_TextBox_FirstName = new TextBox();
            Control_Edit_User_Label_Shift = new Label();
            Control_Edit_User_GroupBox_Creds = new GroupBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            Control_Edit_User_Label_UserName = new Label();
            Control_Edit_User_Label_UserNameWarning = new Label();
            Control_Edit_User_Label_Pin = new Label();
            Control_Edit_User_TextBox_UserName = new TextBox();
            Control_Edit_User_TextBox_Pin = new TextBox();
            Control_Edit_User_GroupBox_VisualInfo = new GroupBox();
            tableLayoutPanel8 = new TableLayoutPanel();
            Control_Edit_User_CheckBox_VisualAccess = new CheckBox();
            Control_Edit_User_Label_VisualPassword = new Label();
            Control_Edit_User_Label_VisualUserName = new Label();
            Control_Edit_User_TextBox_VisualUserName = new TextBox();
            Control_Edit_User_TextBox_VisualPassword = new TextBox();
            Control_Edit_User_GroupBox_UserPrivileges = new GroupBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            Control_Edit_User_RadioButton_ReadOnly = new RadioButton();
            Control_Edit_User_RadioButton_NormalUser = new RadioButton();
            Control_Edit_User_RadioButton_Administrator = new RadioButton();
            Control_Edit_User_RadioButton_Developer = new RadioButton();
            Control_Edit_User_CheckBox_ViewHidePasswords = new CheckBox();
            Control_Edit_User_Button_Clear = new Button();
            Control_Edit_User_Button_Save = new Button();
            tableLayoutPanel5 = new TableLayoutPanel();
            tableLayoutPanel6 = new TableLayoutPanel();
            tableLayoutPanel7 = new TableLayoutPanel();
            Control_Edit_User_GroupBox_UserInfo.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            Control_Edit_User_GroupBox_Creds.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            Control_Edit_User_GroupBox_VisualInfo.SuspendLayout();
            tableLayoutPanel8.SuspendLayout();
            Control_Edit_User_GroupBox_UserPrivileges.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            tableLayoutPanel7.SuspendLayout();
            SuspendLayout();
            //
            // Control_Edit_User_ComboBox_Users
            //
            Control_Edit_User_ComboBox_Users.AutoCompleteMode = AutoCompleteMode.Suggest;
            Control_Edit_User_ComboBox_Users.AutoCompleteSource = AutoCompleteSource.ListItems;
            Control_Edit_User_ComboBox_Users.Dock = DockStyle.Fill;
            Control_Edit_User_ComboBox_Users.Location = new Point(76, 3);
            Control_Edit_User_ComboBox_Users.MaxDropDownItems = 6;
            Control_Edit_User_ComboBox_Users.Name = "Control_Edit_User_ComboBox_Users";
            Control_Edit_User_ComboBox_Users.Size = new Size(410, 23);
            Control_Edit_User_ComboBox_Users.Sorted = true;
            Control_Edit_User_ComboBox_Users.TabIndex = 0;
            //
            // Control_Edit_User_Label_SelectUser
            //
            Control_Edit_User_Label_SelectUser.AutoSize = true;
            Control_Edit_User_Label_SelectUser.Dock = DockStyle.Fill;
            Control_Edit_User_Label_SelectUser.Location = new Point(3, 3);
            Control_Edit_User_Label_SelectUser.Margin = new Padding(3);
            Control_Edit_User_Label_SelectUser.Name = "Control_Edit_User_Label_SelectUser";
            Control_Edit_User_Label_SelectUser.Size = new Size(67, 23);
            Control_Edit_User_Label_SelectUser.TabIndex = 0;
            Control_Edit_User_Label_SelectUser.TabStop = false;
            Control_Edit_User_Label_SelectUser.Text = "Select User:";
            Control_Edit_User_Label_SelectUser.TextAlign = ContentAlignment.MiddleRight;
            //
            // Control_Edit_User_GroupBox_UserInfo
            //
            Control_Edit_User_GroupBox_UserInfo.AutoSize = true;
            Control_Edit_User_GroupBox_UserInfo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_Edit_User_GroupBox_UserInfo.Controls.Add(tableLayoutPanel4);
            Control_Edit_User_GroupBox_UserInfo.Dock = DockStyle.Fill;
            Control_Edit_User_GroupBox_UserInfo.Location = new Point(3, 38);
            Control_Edit_User_GroupBox_UserInfo.Name = "Control_Edit_User_GroupBox_UserInfo";
            Control_Edit_User_GroupBox_UserInfo.Size = new Size(364, 226);
            Control_Edit_User_GroupBox_UserInfo.TabIndex = 1;
            Control_Edit_User_GroupBox_UserInfo.TabStop = false;
            Control_Edit_User_GroupBox_UserInfo.Text = "User Information";
            //
            // Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeContents
            //
            tableLayoutPanel4.AutoSize = true;
            tableLayoutPanel4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel4.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel4.Controls.Add(Control_Edit_User_GroupBox_Creds, 0, 1);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(3, 19);
            tableLayoutPanel4.Name = "Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeContents";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.Size = new Size(358, 204);
            tableLayoutPanel4.TabIndex = 14;
            //
            // tableLayoutPanel2
            //
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(Control_Edit_User_Label_FirstName, 0, 0);
            tableLayoutPanel2.Controls.Add(Control_Edit_User_Label_LastName, 0, 1);
            tableLayoutPanel2.Controls.Add(Control_Edit_User_ComboBox_Shift, 1, 2);
            tableLayoutPanel2.Controls.Add(Control_Edit_User_TextBox_LastName, 1, 1);
            tableLayoutPanel2.Controls.Add(Control_Edit_User_TextBox_FirstName, 1, 0);
            tableLayoutPanel2.Controls.Add(Control_Edit_User_Label_Shift, 0, 2);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.Padding = new Padding(2);
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(352, 91);
            tableLayoutPanel2.TabIndex = 0;
            //
            // Control_Edit_User_Label_FirstName
            //
            Control_Edit_User_Label_FirstName.AutoSize = true;
            Control_Edit_User_Label_FirstName.Dock = DockStyle.Fill;
            Control_Edit_User_Label_FirstName.Location = new Point(5, 5);
            Control_Edit_User_Label_FirstName.Margin = new Padding(3);
            Control_Edit_User_Label_FirstName.Name = "Control_Edit_User_Label_FirstName";
            Control_Edit_User_Label_FirstName.Size = new Size(67, 23);
            Control_Edit_User_Label_FirstName.TabIndex = 0;
            Control_Edit_User_Label_FirstName.TabStop = false;
            Control_Edit_User_Label_FirstName.Text = "First Name:";
            Control_Edit_User_Label_FirstName.TextAlign = ContentAlignment.MiddleRight;
            //
            // Control_Edit_User_Label_LastName
            //
            Control_Edit_User_Label_LastName.AutoSize = true;
            Control_Edit_User_Label_LastName.Dock = DockStyle.Fill;
            Control_Edit_User_Label_LastName.Location = new Point(5, 34);
            Control_Edit_User_Label_LastName.Margin = new Padding(3);
            Control_Edit_User_Label_LastName.Name = "Control_Edit_User_Label_LastName";
            Control_Edit_User_Label_LastName.Size = new Size(67, 23);
            Control_Edit_User_Label_LastName.TabIndex = 2;
            Control_Edit_User_Label_LastName.TabStop = false;
            Control_Edit_User_Label_LastName.Text = "Last Name:";
            Control_Edit_User_Label_LastName.TextAlign = ContentAlignment.MiddleRight;
            //
            // Control_Edit_User_ComboBox_Shift
            //
            Control_Edit_User_ComboBox_Shift.Dock = DockStyle.Fill;
            Control_Edit_User_ComboBox_Shift.DropDownStyle = ComboBoxStyle.DropDownList;
            Control_Edit_User_ComboBox_Shift.Location = new Point(78, 63);
            Control_Edit_User_ComboBox_Shift.Name = "Control_Edit_User_ComboBox_Shift";
            Control_Edit_User_ComboBox_Shift.Size = new Size(269, 23);
            Control_Edit_User_ComboBox_Shift.TabIndex = 2;
            //
            // Control_Edit_User_TextBox_LastName
            //
            Control_Edit_User_TextBox_LastName.Dock = DockStyle.Fill;
            Control_Edit_User_TextBox_LastName.Location = new Point(78, 34);
            Control_Edit_User_TextBox_LastName.Name = "Control_Edit_User_TextBox_LastName";
            Control_Edit_User_TextBox_LastName.Size = new Size(269, 23);
            Control_Edit_User_TextBox_LastName.TabIndex = 1;
            //
            // Control_Edit_User_TextBox_FirstName
            //
            Control_Edit_User_TextBox_FirstName.Dock = DockStyle.Fill;
            Control_Edit_User_TextBox_FirstName.Location = new Point(78, 5);
            Control_Edit_User_TextBox_FirstName.Name = "Control_Edit_User_TextBox_FirstName";
            Control_Edit_User_TextBox_FirstName.Size = new Size(269, 23);
            Control_Edit_User_TextBox_FirstName.TabIndex = 0;
            //
            // Control_Edit_User_Label_Shift
            //
            Control_Edit_User_Label_Shift.AutoSize = true;
            Control_Edit_User_Label_Shift.Dock = DockStyle.Fill;
            Control_Edit_User_Label_Shift.Location = new Point(5, 63);
            Control_Edit_User_Label_Shift.Margin = new Padding(3);
            Control_Edit_User_Label_Shift.Name = "Control_Edit_User_Label_Shift";
            Control_Edit_User_Label_Shift.Size = new Size(67, 23);
            Control_Edit_User_Label_Shift.TabIndex = 6;
            Control_Edit_User_Label_Shift.TabStop = false;
            Control_Edit_User_Label_Shift.Text = "Shift:";
            Control_Edit_User_Label_Shift.TextAlign = ContentAlignment.MiddleRight;
            //
            // Control_Edit_User_GroupBox_Creds
            //
            Control_Edit_User_GroupBox_Creds.AutoSize = true;
            Control_Edit_User_GroupBox_Creds.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_Edit_User_GroupBox_Creds.Controls.Add(tableLayoutPanel3);
            Control_Edit_User_GroupBox_Creds.Dock = DockStyle.Fill;
            Control_Edit_User_GroupBox_Creds.Location = new Point(3, 100);
            Control_Edit_User_GroupBox_Creds.Name = "Control_Edit_User_GroupBox_Creds";
            Control_Edit_User_GroupBox_Creds.Size = new Size(352, 101);
            Control_Edit_User_GroupBox_Creds.TabIndex = 1;
            Control_Edit_User_GroupBox_Creds.TabStop = false;
            Control_Edit_User_GroupBox_Creds.Text = "Login Information";
            //
            // Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeHeader
            //
            tableLayoutPanel3.AutoSize = true;
            tableLayoutPanel3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(Control_Edit_User_Label_UserName, 0, 0);
            tableLayoutPanel3.Controls.Add(Control_Edit_User_Label_UserNameWarning, 0, 2);
            tableLayoutPanel3.Controls.Add(Control_Edit_User_Label_Pin, 0, 1);
            tableLayoutPanel3.Controls.Add(Control_Edit_User_TextBox_UserName, 1, 0);
            tableLayoutPanel3.Controls.Add(Control_Edit_User_TextBox_Pin, 1, 1);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 19);
            tableLayoutPanel3.Name = "Control_ReceivingAnalytics_TableLayoutPanel_ReceivingScopeHeader";
            tableLayoutPanel3.RowCount = 3;
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.Size = new Size(346, 79);
            tableLayoutPanel3.TabIndex = 0;
            //
            // Control_Edit_User_Label_UserName
            //
            Control_Edit_User_Label_UserName.AutoSize = true;
            Control_Edit_User_Label_UserName.Dock = DockStyle.Fill;
            Control_Edit_User_Label_UserName.Location = new Point(3, 3);
            Control_Edit_User_Label_UserName.Margin = new Padding(3);
            Control_Edit_User_Label_UserName.Name = "Control_Edit_User_Label_UserName";
            Control_Edit_User_Label_UserName.Size = new Size(68, 23);
            Control_Edit_User_Label_UserName.TabIndex = 4;
            Control_Edit_User_Label_UserName.TabStop = false;
            Control_Edit_User_Label_UserName.Text = "User Name:";
            Control_Edit_User_Label_UserName.TextAlign = ContentAlignment.MiddleRight;
            //
            // Control_Edit_User_Label_UserNameWarning
            //
            Control_Edit_User_Label_UserNameWarning.AutoSize = true;
            tableLayoutPanel3.SetColumnSpan(Control_Edit_User_Label_UserNameWarning, 2);
            Control_Edit_User_Label_UserNameWarning.Dock = DockStyle.Fill;
            Control_Edit_User_Label_UserNameWarning.ForeColor = Color.Red;
            Control_Edit_User_Label_UserNameWarning.Location = new Point(3, 61);
            Control_Edit_User_Label_UserNameWarning.Margin = new Padding(3);
            Control_Edit_User_Label_UserNameWarning.Name = "Control_Edit_User_Label_UserNameWarning";
            Control_Edit_User_Label_UserNameWarning.Size = new Size(340, 15);
            Control_Edit_User_Label_UserNameWarning.TabIndex = 10;
            Control_Edit_User_Label_UserNameWarning.TabStop = false;
            Control_Edit_User_Label_UserNameWarning.Text = "User Name cannot be changed.";
            Control_Edit_User_Label_UserNameWarning.TextAlign = ContentAlignment.MiddleCenter;
            //
            // Control_Edit_User_Label_Pin
            //
            Control_Edit_User_Label_Pin.AutoSize = true;
            Control_Edit_User_Label_Pin.Dock = DockStyle.Fill;
            Control_Edit_User_Label_Pin.Location = new Point(3, 32);
            Control_Edit_User_Label_Pin.Margin = new Padding(3);
            Control_Edit_User_Label_Pin.Name = "Control_Edit_User_Label_Pin";
            Control_Edit_User_Label_Pin.Size = new Size(68, 23);
            Control_Edit_User_Label_Pin.TabIndex = 8;
            Control_Edit_User_Label_Pin.TabStop = false;
            Control_Edit_User_Label_Pin.Text = "Pin:";
            Control_Edit_User_Label_Pin.TextAlign = ContentAlignment.MiddleRight;
            //
            // Control_Edit_User_TextBox_UserName
            //
            Control_Edit_User_TextBox_UserName.Dock = DockStyle.Fill;
            Control_Edit_User_TextBox_UserName.Location = new Point(77, 3);
            Control_Edit_User_TextBox_UserName.Name = "Control_Edit_User_TextBox_UserName";
            Control_Edit_User_TextBox_UserName.ReadOnly = true;
            Control_Edit_User_TextBox_UserName.Size = new Size(266, 23);
            Control_Edit_User_TextBox_UserName.TabIndex = 0;
            //
            // Control_Edit_User_TextBox_Pin
            //
            Control_Edit_User_TextBox_Pin.Dock = DockStyle.Fill;
            Control_Edit_User_TextBox_Pin.Location = new Point(77, 32);
            Control_Edit_User_TextBox_Pin.Name = "Control_Edit_User_TextBox_Pin";
            Control_Edit_User_TextBox_Pin.Size = new Size(266, 23);
            Control_Edit_User_TextBox_Pin.TabIndex = 1;
            //
            // Control_Edit_User_GroupBox_VisualInfo
            //
            Control_Edit_User_GroupBox_VisualInfo.AutoSize = true;
            Control_Edit_User_GroupBox_VisualInfo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel5.SetColumnSpan(Control_Edit_User_GroupBox_VisualInfo, 2);
            Control_Edit_User_GroupBox_VisualInfo.Controls.Add(tableLayoutPanel8);
            Control_Edit_User_GroupBox_VisualInfo.Dock = DockStyle.Fill;
            Control_Edit_User_GroupBox_VisualInfo.Location = new Point(3, 270);
            Control_Edit_User_GroupBox_VisualInfo.Name = "Control_Edit_User_GroupBox_VisualInfo";
            Control_Edit_User_GroupBox_VisualInfo.Size = new Size(489, 105);
            Control_Edit_User_GroupBox_VisualInfo.TabIndex = 3;
            Control_Edit_User_GroupBox_VisualInfo.TabStop = false;
            Control_Edit_User_GroupBox_VisualInfo.Text = "Infor VISUAL Information";
            //
            // tableLayoutPanel8
            //
            tableLayoutPanel8.AutoSize = true;
            tableLayoutPanel8.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel8.ColumnCount = 2;
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel8.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel8.Controls.Add(Control_Edit_User_CheckBox_VisualAccess, 0, 0);
            tableLayoutPanel8.Controls.Add(Control_Edit_User_Label_VisualPassword, 0, 2);
            tableLayoutPanel8.Controls.Add(Control_Edit_User_Label_VisualUserName, 0, 1);
            tableLayoutPanel8.Controls.Add(Control_Edit_User_TextBox_VisualUserName, 1, 1);
            tableLayoutPanel8.Controls.Add(Control_Edit_User_TextBox_VisualPassword, 1, 2);
            tableLayoutPanel8.Dock = DockStyle.Fill;
            tableLayoutPanel8.Location = new Point(3, 19);
            tableLayoutPanel8.Name = "tableLayoutPanel8";
            tableLayoutPanel8.RowCount = 3;
            tableLayoutPanel8.RowStyles.Add(new RowStyle());
            tableLayoutPanel8.RowStyles.Add(new RowStyle());
            tableLayoutPanel8.RowStyles.Add(new RowStyle());
            tableLayoutPanel8.Size = new Size(483, 83);
            tableLayoutPanel8.TabIndex = 5;
            //
            // Control_Edit_User_CheckBox_VisualAccess
            //
            Control_Edit_User_CheckBox_VisualAccess.AutoSize = true;
            tableLayoutPanel8.SetColumnSpan(Control_Edit_User_CheckBox_VisualAccess, 2);
            Control_Edit_User_CheckBox_VisualAccess.Dock = DockStyle.Fill;
            Control_Edit_User_CheckBox_VisualAccess.Location = new Point(3, 3);
            Control_Edit_User_CheckBox_VisualAccess.Name = "Control_Edit_User_CheckBox_VisualAccess";
            Control_Edit_User_CheckBox_VisualAccess.Size = new Size(477, 19);
            Control_Edit_User_CheckBox_VisualAccess.TabIndex = 0;
            Control_Edit_User_CheckBox_VisualAccess.Text = "Visual Access?";
            Control_Edit_User_CheckBox_VisualAccess.UseVisualStyleBackColor = true;
            //
            // Control_Edit_User_Label_VisualPassword
            //
            Control_Edit_User_Label_VisualPassword.AutoSize = true;
            Control_Edit_User_Label_VisualPassword.Dock = DockStyle.Fill;
            Control_Edit_User_Label_VisualPassword.Location = new Point(3, 57);
            Control_Edit_User_Label_VisualPassword.Margin = new Padding(3);
            Control_Edit_User_Label_VisualPassword.Name = "Control_Edit_User_Label_VisualPassword";
            Control_Edit_User_Label_VisualPassword.Size = new Size(68, 23);
            Control_Edit_User_Label_VisualPassword.TabIndex = 3;
            Control_Edit_User_Label_VisualPassword.TabStop = false;
            Control_Edit_User_Label_VisualPassword.Text = "Password:";
            Control_Edit_User_Label_VisualPassword.TextAlign = ContentAlignment.MiddleRight;
            //
            // Control_Edit_User_Label_VisualUserName
            //
            Control_Edit_User_Label_VisualUserName.AutoSize = true;
            Control_Edit_User_Label_VisualUserName.Dock = DockStyle.Fill;
            Control_Edit_User_Label_VisualUserName.Location = new Point(3, 28);
            Control_Edit_User_Label_VisualUserName.Margin = new Padding(3);
            Control_Edit_User_Label_VisualUserName.Name = "Control_Edit_User_Label_VisualUserName";
            Control_Edit_User_Label_VisualUserName.Size = new Size(68, 23);
            Control_Edit_User_Label_VisualUserName.TabIndex = 1;
            Control_Edit_User_Label_VisualUserName.TabStop = false;
            Control_Edit_User_Label_VisualUserName.Text = "User Name:";
            Control_Edit_User_Label_VisualUserName.TextAlign = ContentAlignment.MiddleRight;
            //
            // Control_Edit_User_TextBox_VisualUserName
            //
            Control_Edit_User_TextBox_VisualUserName.Dock = DockStyle.Fill;
            Control_Edit_User_TextBox_VisualUserName.Location = new Point(77, 28);
            Control_Edit_User_TextBox_VisualUserName.Name = "Control_Edit_User_TextBox_VisualUserName";
            Control_Edit_User_TextBox_VisualUserName.Size = new Size(403, 23);
            Control_Edit_User_TextBox_VisualUserName.TabIndex = 1;
            //
            // Control_Edit_User_TextBox_VisualPassword
            //
            Control_Edit_User_TextBox_VisualPassword.Dock = DockStyle.Fill;
            Control_Edit_User_TextBox_VisualPassword.Location = new Point(77, 57);
            Control_Edit_User_TextBox_VisualPassword.Name = "Control_Edit_User_TextBox_VisualPassword";
            Control_Edit_User_TextBox_VisualPassword.Size = new Size(403, 23);
            Control_Edit_User_TextBox_VisualPassword.TabIndex = 2;
            //
            // Control_Edit_User_GroupBox_UserPrivileges
            //
            Control_Edit_User_GroupBox_UserPrivileges.AutoSize = true;
            Control_Edit_User_GroupBox_UserPrivileges.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_Edit_User_GroupBox_UserPrivileges.Controls.Add(tableLayoutPanel1);
            Control_Edit_User_GroupBox_UserPrivileges.Dock = DockStyle.Fill;
            Control_Edit_User_GroupBox_UserPrivileges.Location = new Point(373, 38);
            Control_Edit_User_GroupBox_UserPrivileges.Name = "Control_Edit_User_GroupBox_UserPrivileges";
            Control_Edit_User_GroupBox_UserPrivileges.Size = new Size(119, 226);
            Control_Edit_User_GroupBox_UserPrivileges.TabIndex = 11;
            Control_Edit_User_GroupBox_UserPrivileges.TabStop = false;
            Control_Edit_User_GroupBox_UserPrivileges.Text = "User Privileges";
            //
            // tableLayoutPanel1
            //
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(Control_Edit_User_RadioButton_ReadOnly, 0, 1);
            tableLayoutPanel1.Controls.Add(Control_Edit_User_RadioButton_NormalUser, 0, 3);
            tableLayoutPanel1.Controls.Add(Control_Edit_User_RadioButton_Administrator, 0, 5);
            tableLayoutPanel1.Controls.Add(Control_Edit_User_RadioButton_Developer, 0, 7);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 19);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 9;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.Size = new Size(113, 204);
            tableLayoutPanel1.TabIndex = 3;
            //
            // Control_Edit_User_RadioButton_ReadOnly
            //
            Control_Edit_User_RadioButton_ReadOnly.AutoSize = true;
            Control_Edit_User_RadioButton_ReadOnly.Dock = DockStyle.Fill;
            Control_Edit_User_RadioButton_ReadOnly.Location = new Point(3, 23);
            Control_Edit_User_RadioButton_ReadOnly.Name = "Control_Edit_User_RadioButton_ReadOnly";
            Control_Edit_User_RadioButton_ReadOnly.Size = new Size(107, 19);
            Control_Edit_User_RadioButton_ReadOnly.TabIndex = 6;
            Control_Edit_User_RadioButton_ReadOnly.TabStop = true;
            Control_Edit_User_RadioButton_ReadOnly.Text = "Read-Only User";
            Control_Edit_User_RadioButton_ReadOnly.UseVisualStyleBackColor = true;
            //
            // Control_Edit_User_RadioButton_NormalUser
            //
            Control_Edit_User_RadioButton_NormalUser.AutoSize = true;
            Control_Edit_User_RadioButton_NormalUser.Dock = DockStyle.Fill;
            Control_Edit_User_RadioButton_NormalUser.Location = new Point(3, 68);
            Control_Edit_User_RadioButton_NormalUser.Name = "Control_Edit_User_RadioButton_NormalUser";
            Control_Edit_User_RadioButton_NormalUser.Size = new Size(107, 19);
            Control_Edit_User_RadioButton_NormalUser.TabIndex = 7;
            Control_Edit_User_RadioButton_NormalUser.TabStop = true;
            Control_Edit_User_RadioButton_NormalUser.Text = "Normal User";
            Control_Edit_User_RadioButton_NormalUser.UseVisualStyleBackColor = true;
            //
            // Control_Edit_User_RadioButton_Administrator
            //
            Control_Edit_User_RadioButton_Administrator.AutoSize = true;
            Control_Edit_User_RadioButton_Administrator.Dock = DockStyle.Fill;
            Control_Edit_User_RadioButton_Administrator.Location = new Point(3, 113);
            Control_Edit_User_RadioButton_Administrator.Name = "Control_Edit_User_RadioButton_Administrator";
            Control_Edit_User_RadioButton_Administrator.Size = new Size(107, 19);
            Control_Edit_User_RadioButton_Administrator.TabIndex = 8;
            Control_Edit_User_RadioButton_Administrator.TabStop = true;
            Control_Edit_User_RadioButton_Administrator.Text = "Administrator";
            Control_Edit_User_RadioButton_Administrator.UseVisualStyleBackColor = true;
            //
            // Control_Edit_User_RadioButton_Developer
            //
            Control_Edit_User_RadioButton_Developer.AutoSize = true;
            Control_Edit_User_RadioButton_Developer.Dock = DockStyle.Fill;
            Control_Edit_User_RadioButton_Developer.Location = new Point(3, 158);
            Control_Edit_User_RadioButton_Developer.Name = "Control_Edit_User_RadioButton_Developer";
            Control_Edit_User_RadioButton_Developer.Size = new Size(107, 19);
            Control_Edit_User_RadioButton_Developer.TabIndex = 9;
            Control_Edit_User_RadioButton_Developer.TabStop = true;
            Control_Edit_User_RadioButton_Developer.Text = "Developer";
            Control_Edit_User_RadioButton_Developer.UseVisualStyleBackColor = true;
            //
            // Control_Edit_User_CheckBox_ViewHidePasswords
            //
            Control_Edit_User_CheckBox_ViewHidePasswords.AutoSize = true;
            Control_Edit_User_CheckBox_ViewHidePasswords.Dock = DockStyle.Fill;
            Control_Edit_User_CheckBox_ViewHidePasswords.Location = new Point(3, 3);
            Control_Edit_User_CheckBox_ViewHidePasswords.Name = "Control_Edit_User_CheckBox_ViewHidePasswords";
            Control_Edit_User_CheckBox_ViewHidePasswords.Size = new Size(141, 25);
            Control_Edit_User_CheckBox_ViewHidePasswords.TabIndex = 2;
            Control_Edit_User_CheckBox_ViewHidePasswords.TabStop = false;
            Control_Edit_User_CheckBox_ViewHidePasswords.Text = "Show Password Fields";
            Control_Edit_User_CheckBox_ViewHidePasswords.UseVisualStyleBackColor = true;
            //
            // Control_Edit_User_Button_Clear
            //
            Control_Edit_User_Button_Clear.AutoSize = true;
            Control_Edit_User_Button_Clear.Location = new Point(330, 3);
            Control_Edit_User_Button_Clear.Name = "Control_Edit_User_Button_Clear";
            Control_Edit_User_Button_Clear.Size = new Size(75, 25);
            Control_Edit_User_Button_Clear.TabIndex = 1;
            Control_Edit_User_Button_Clear.TabStop = false;
            Control_Edit_User_Button_Clear.Text = "Clear";
            Control_Edit_User_Button_Clear.UseVisualStyleBackColor = true;
            //
            // Control_Edit_User_Button_Save
            //
            Control_Edit_User_Button_Save.AutoSize = true;
            Control_Edit_User_Button_Save.Location = new Point(411, 3);
            Control_Edit_User_Button_Save.Name = "Control_Edit_User_Button_Save";
            Control_Edit_User_Button_Save.Size = new Size(75, 25);
            Control_Edit_User_Button_Save.TabIndex = 13;
            Control_Edit_User_Button_Save.Text = "Save";
            Control_Edit_User_Button_Save.UseVisualStyleBackColor = true;
            //
            // tableLayoutPanel5
            //
            tableLayoutPanel5.ColumnCount = 2;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel5.Controls.Add(tableLayoutPanel6, 0, 0);
            tableLayoutPanel5.Controls.Add(Control_Edit_User_GroupBox_VisualInfo, 0, 2);
            tableLayoutPanel5.Controls.Add(Control_Edit_User_GroupBox_UserInfo, 0, 1);
            tableLayoutPanel5.Controls.Add(Control_Edit_User_GroupBox_UserPrivileges, 1, 1);
            tableLayoutPanel5.Controls.Add(tableLayoutPanel7, 0, 4);
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Location = new Point(0, 0);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 5;
            tableLayoutPanel5.RowStyles.Add(new RowStyle());
            tableLayoutPanel5.RowStyles.Add(new RowStyle());
            tableLayoutPanel5.RowStyles.Add(new RowStyle());
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.RowStyles.Add(new RowStyle());
            tableLayoutPanel5.Size = new Size(495, 415);
            tableLayoutPanel5.TabIndex = 13;
            //
            // tableLayoutPanel6
            //
            tableLayoutPanel6.AutoSize = true;
            tableLayoutPanel6.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel6.ColumnCount = 2;
            tableLayoutPanel5.SetColumnSpan(tableLayoutPanel6, 2);
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel6.Controls.Add(Control_Edit_User_Label_SelectUser, 0, 0);
            tableLayoutPanel6.Controls.Add(Control_Edit_User_ComboBox_Users, 1, 0);
            tableLayoutPanel6.Dock = DockStyle.Fill;
            tableLayoutPanel6.Location = new Point(3, 3);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 1;
            tableLayoutPanel6.RowStyles.Add(new RowStyle());
            tableLayoutPanel6.Size = new Size(489, 29);
            tableLayoutPanel6.TabIndex = 14;
            //
            // tableLayoutPanel7
            //
            tableLayoutPanel7.AutoSize = true;
            tableLayoutPanel7.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel7.ColumnCount = 4;
            tableLayoutPanel5.SetColumnSpan(tableLayoutPanel7, 2);
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel7.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel7.Controls.Add(Control_Edit_User_Button_Save, 3, 0);
            tableLayoutPanel7.Controls.Add(Control_Edit_User_CheckBox_ViewHidePasswords, 0, 0);
            tableLayoutPanel7.Controls.Add(Control_Edit_User_Button_Clear, 2, 0);
            tableLayoutPanel7.Dock = DockStyle.Fill;
            tableLayoutPanel7.Location = new Point(3, 381);
            tableLayoutPanel7.Name = "tableLayoutPanel7";
            tableLayoutPanel7.RowCount = 1;
            tableLayoutPanel7.RowStyles.Add(new RowStyle());
            tableLayoutPanel7.Size = new Size(489, 31);
            tableLayoutPanel7.TabIndex = 0;
            //
            // Control_Edit_User
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            Controls.Add(tableLayoutPanel5);
            Name = "Control_Edit_User";
            Size = new Size(495, 415);
            Control_Edit_User_GroupBox_UserInfo.ResumeLayout(false);
            Control_Edit_User_GroupBox_UserInfo.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            Control_Edit_User_GroupBox_Creds.ResumeLayout(false);
            Control_Edit_User_GroupBox_Creds.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            Control_Edit_User_GroupBox_VisualInfo.ResumeLayout(false);
            Control_Edit_User_GroupBox_VisualInfo.PerformLayout();
            tableLayoutPanel8.ResumeLayout(false);
            tableLayoutPanel8.PerformLayout();
            Control_Edit_User_GroupBox_UserPrivileges.ResumeLayout(false);
            Control_Edit_User_GroupBox_UserPrivileges.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel6.PerformLayout();
            tableLayoutPanel7.ResumeLayout(false);
            tableLayoutPanel7.PerformLayout();
            ResumeLayout(false);
        }
        #endregion

        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel1;
        private RadioButton Control_Edit_User_RadioButton_Developer;
        private TableLayoutPanel tableLayoutPanel4;
        private GroupBox Control_Edit_User_GroupBox_Creds;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel5;
        private TableLayoutPanel tableLayoutPanel6;
        private TableLayoutPanel tableLayoutPanel7;
        private TableLayoutPanel tableLayoutPanel8;
    }
}
