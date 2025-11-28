using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    partial class Control_Database
    {
        #region Fields

        private IContainer components = null!;
        private TableLayoutPanel Control_Database_TableLayout_Main = null!;
        private Label Control_Database_Label_Header = null!;
        private Panel Control_Database_Panel_Content = null!;
        private Panel Control_Database_Panel_ServerCard = null!;
        private Label Control_Database_Label_ServerIcon = null!;
        private Label Control_Database_Label_ServerTitle = null!;
        private TextBox Control_Database_TextBox_Server = null!;
        private Panel Control_Database_Panel_PortCard = null!;
        private Label Control_Database_Label_PortIcon = null!;
        private Label Control_Database_Label_PortTitle = null!;
        private TextBox Control_Database_TextBox_Port = null!;
        private Panel Control_Database_Panel_DatabaseCard = null!;
        private Label Control_Database_Label_DatabaseIcon = null!;
        private Label Control_Database_Label_DatabaseTitle = null!;
        private TextBox Control_Database_TextBox_Database = null!;
        private FlowLayoutPanel Control_Database_FlowPanel_Actions = null!;
        private Button Control_Database_Button_Save = null!;
        private Button Control_Database_Button_Reset = null!;
        private Button Control_Database_Button_TestConnection = null!;

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
            Control_Database_TableLayout_Main = new TableLayoutPanel();
            Control_Database_Label_Header = new Label();
            Control_Database_Panel_Content = new Panel();
            tableLayoutPanel4 = new TableLayoutPanel();
            Control_Database_Panel_ServerCard = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            Control_Database_TextBox_Server = new TextBox();
            Control_Database_Label_ServerTitle = new Label();
            Control_Database_Label_ServerIcon = new Label();
            Control_Database_Panel_DatabaseCard = new Panel();
            tableLayoutPanel3 = new TableLayoutPanel();
            Control_Database_TextBox_Database = new TextBox();
            Control_Database_Label_DatabaseTitle = new Label();
            Control_Database_Label_DatabaseIcon = new Label();
            Control_Database_Panel_PortCard = new Panel();
            tableLayoutPanel2 = new TableLayoutPanel();
            Control_Database_TextBox_Port = new TextBox();
            Control_Database_Label_PortTitle = new Label();
            Control_Database_Label_PortIcon = new Label();
            Control_Database_FlowPanel_Actions = new FlowLayoutPanel();
            Control_Database_Button_TestConnection = new Button();
            Control_Database_Button_Reset = new Button();
            Control_Database_Button_Save = new Button();
            Control_Database_TableLayout_Main.SuspendLayout();
            Control_Database_Panel_Content.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            Control_Database_Panel_ServerCard.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            Control_Database_Panel_DatabaseCard.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            Control_Database_Panel_PortCard.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            Control_Database_FlowPanel_Actions.SuspendLayout();
            SuspendLayout();
            //
            // Control_Database_TableLayout_Main
            //
            Control_Database_TableLayout_Main.AutoSize = true;
            Control_Database_TableLayout_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_Database_TableLayout_Main.ColumnCount = 1;
            Control_Database_TableLayout_Main.ColumnStyles.Add(new ColumnStyle());
            Control_Database_TableLayout_Main.Controls.Add(Control_Database_Label_Header, 0, 0);
            Control_Database_TableLayout_Main.Controls.Add(Control_Database_Panel_Content, 0, 1);
            Control_Database_TableLayout_Main.Controls.Add(Control_Database_FlowPanel_Actions, 0, 3);
            Control_Database_TableLayout_Main.Dock = DockStyle.Fill;
            Control_Database_TableLayout_Main.Location = new Point(0, 0);
            Control_Database_TableLayout_Main.Name = "Control_Database_TableLayout_Main";
            Control_Database_TableLayout_Main.Padding = new Padding(20);
            Control_Database_TableLayout_Main.RowCount = 4;
            Control_Database_TableLayout_Main.RowStyles.Add(new RowStyle());
            Control_Database_TableLayout_Main.RowStyles.Add(new RowStyle());
            Control_Database_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_Database_TableLayout_Main.RowStyles.Add(new RowStyle());
            Control_Database_TableLayout_Main.Size = new Size(479, 371);
            Control_Database_TableLayout_Main.TabIndex = 0;
            //
            // Control_Database_Label_Header
            //
            Control_Database_Label_Header.AutoSize = true;
            Control_Database_Label_Header.Dock = DockStyle.Fill;
            Control_Database_Label_Header.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            Control_Database_Label_Header.ForeColor = Color.FromArgb(45, 45, 45);
            Control_Database_Label_Header.Location = new Point(23, 20);
            Control_Database_Label_Header.MaximumSize = new Size(400, 0);
            Control_Database_Label_Header.MinimumSize = new Size(400, 0);
            Control_Database_Label_Header.Name = "Control_Database_Label_Header";
            Control_Database_Label_Header.Size = new Size(400, 32);
            Control_Database_Label_Header.TabIndex = 0;
            Control_Database_Label_Header.Text = "Database Connection Settings";
            Control_Database_Label_Header.TextAlign = ContentAlignment.MiddleLeft;
            //
            // Control_Database_Panel_Content
            //
            Control_Database_Panel_Content.AutoSize = true;
            Control_Database_Panel_Content.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_Database_Panel_Content.Controls.Add(tableLayoutPanel4);
            Control_Database_Panel_Content.Dock = DockStyle.Fill;
            Control_Database_Panel_Content.Location = new Point(23, 55);
            Control_Database_Panel_Content.Name = "Control_Database_Panel_Content";
            Control_Database_Panel_Content.Size = new Size(433, 246);
            Control_Database_Panel_Content.TabIndex = 1;
            //
            // tableLayoutPanel4
            //
            tableLayoutPanel4.AutoSize = true;
            tableLayoutPanel4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel4.Controls.Add(Control_Database_Panel_ServerCard, 0, 0);
            tableLayoutPanel4.Controls.Add(Control_Database_Panel_DatabaseCard, 0, 2);
            tableLayoutPanel4.Controls.Add(Control_Database_Panel_PortCard, 0, 1);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(0, 0);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 3;
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.Size = new Size(433, 246);
            tableLayoutPanel4.TabIndex = 3;
            //
            // Control_Database_Panel_ServerCard
            //
            Control_Database_Panel_ServerCard.AutoSize = true;
            Control_Database_Panel_ServerCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_Database_Panel_ServerCard.BackColor = Color.White;
            Control_Database_Panel_ServerCard.BorderStyle = BorderStyle.FixedSingle;
            Control_Database_Panel_ServerCard.Controls.Add(tableLayoutPanel1);
            Control_Database_Panel_ServerCard.Dock = DockStyle.Fill;
            Control_Database_Panel_ServerCard.Location = new Point(0, 5);
            Control_Database_Panel_ServerCard.Margin = new Padding(0, 5, 0, 5);
            Control_Database_Panel_ServerCard.Name = "Control_Database_Panel_ServerCard";
            Control_Database_Panel_ServerCard.Padding = new Padding(5);
            Control_Database_Panel_ServerCard.Size = new Size(433, 72);
            Control_Database_Panel_ServerCard.TabIndex = 0;
            //
            // tableLayoutPanel1
            //
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(Control_Database_TextBox_Server, 1, 1);
            tableLayoutPanel1.Controls.Add(Control_Database_Label_ServerTitle, 1, 0);
            tableLayoutPanel1.Controls.Add(Control_Database_Label_ServerIcon, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(5, 5);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(421, 60);
            tableLayoutPanel1.TabIndex = 3;
            //
            // Control_Database_TextBox_Server
            //
            Control_Database_TextBox_Server.Dock = DockStyle.Fill;
            Control_Database_TextBox_Server.Font = new Font("Segoe UI", 11F);
            Control_Database_TextBox_Server.Location = new Point(68, 30);
            Control_Database_TextBox_Server.MaximumSize = new Size(350, 0);
            Control_Database_TextBox_Server.MinimumSize = new Size(350, 0);
            Control_Database_TextBox_Server.Name = "Control_Database_TextBox_Server";
            Control_Database_TextBox_Server.PlaceholderText = "Enter MySQL server address (e.g., localhost or IP)";
            Control_Database_TextBox_Server.Size = new Size(350, 27);
            Control_Database_TextBox_Server.TabIndex = 2;
            //
            // Control_Database_Label_ServerTitle
            //
            Control_Database_Label_ServerTitle.AutoSize = true;
            Control_Database_Label_ServerTitle.Dock = DockStyle.Fill;
            Control_Database_Label_ServerTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            Control_Database_Label_ServerTitle.ForeColor = Color.FromArgb(45, 45, 45);
            Control_Database_Label_ServerTitle.Location = new Point(68, 3);
            Control_Database_Label_ServerTitle.Margin = new Padding(3);
            Control_Database_Label_ServerTitle.MaximumSize = new Size(350, 0);
            Control_Database_Label_ServerTitle.MinimumSize = new Size(350, 0);
            Control_Database_Label_ServerTitle.Name = "Control_Database_Label_ServerTitle";
            Control_Database_Label_ServerTitle.Size = new Size(350, 21);
            Control_Database_Label_ServerTitle.TabIndex = 1;
            Control_Database_Label_ServerTitle.Text = "Server Address";
            Control_Database_Label_ServerTitle.TextAlign = ContentAlignment.BottomLeft;
            //
            // Control_Database_Label_ServerIcon
            //
            Control_Database_Label_ServerIcon.AutoSize = true;
            Control_Database_Label_ServerIcon.Dock = DockStyle.Fill;
            Control_Database_Label_ServerIcon.Font = new Font("Segoe UI Emoji", 24F);
            Control_Database_Label_ServerIcon.Location = new Point(1, 1);
            Control_Database_Label_ServerIcon.Margin = new Padding(1);
            Control_Database_Label_ServerIcon.Name = "Control_Database_Label_ServerIcon";
            tableLayoutPanel1.SetRowSpan(Control_Database_Label_ServerIcon, 2);
            Control_Database_Label_ServerIcon.Size = new Size(63, 58);
            Control_Database_Label_ServerIcon.TabIndex = 0;
            Control_Database_Label_ServerIcon.Text = "🖥️";
            Control_Database_Label_ServerIcon.TextAlign = ContentAlignment.MiddleCenter;
            //
            // Control_Database_Panel_DatabaseCard
            //
            Control_Database_Panel_DatabaseCard.AutoSize = true;
            Control_Database_Panel_DatabaseCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_Database_Panel_DatabaseCard.BackColor = Color.White;
            Control_Database_Panel_DatabaseCard.BorderStyle = BorderStyle.FixedSingle;
            Control_Database_Panel_DatabaseCard.Controls.Add(tableLayoutPanel3);
            Control_Database_Panel_DatabaseCard.Dock = DockStyle.Fill;
            Control_Database_Panel_DatabaseCard.Location = new Point(0, 169);
            Control_Database_Panel_DatabaseCard.Margin = new Padding(0, 5, 0, 5);
            Control_Database_Panel_DatabaseCard.Name = "Control_Database_Panel_DatabaseCard";
            Control_Database_Panel_DatabaseCard.Padding = new Padding(5);
            Control_Database_Panel_DatabaseCard.Size = new Size(433, 72);
            Control_Database_Panel_DatabaseCard.TabIndex = 2;
            //
            // tableLayoutPanel3
            //
            tableLayoutPanel3.AutoSize = true;
            tableLayoutPanel3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel3.ColumnCount = 2;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.Controls.Add(Control_Database_TextBox_Database, 1, 1);
            tableLayoutPanel3.Controls.Add(Control_Database_Label_DatabaseTitle, 1, 0);
            tableLayoutPanel3.Controls.Add(Control_Database_Label_DatabaseIcon, 0, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(5, 5);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.Size = new Size(421, 60);
            tableLayoutPanel3.TabIndex = 4;
            //
            // Control_Database_TextBox_Database
            //
            Control_Database_TextBox_Database.Dock = DockStyle.Fill;
            Control_Database_TextBox_Database.Font = new Font("Segoe UI", 11F);
            Control_Database_TextBox_Database.Location = new Point(68, 30);
            Control_Database_TextBox_Database.MaximumSize = new Size(350, 0);
            Control_Database_TextBox_Database.MinimumSize = new Size(350, 0);
            Control_Database_TextBox_Database.Name = "Control_Database_TextBox_Database";
            Control_Database_TextBox_Database.PlaceholderText = "Enter database name";
            Control_Database_TextBox_Database.Size = new Size(350, 27);
            Control_Database_TextBox_Database.TabIndex = 2;
            //
            // Control_Database_Label_DatabaseTitle
            //
            Control_Database_Label_DatabaseTitle.AutoSize = true;
            Control_Database_Label_DatabaseTitle.Dock = DockStyle.Fill;
            Control_Database_Label_DatabaseTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            Control_Database_Label_DatabaseTitle.ForeColor = Color.FromArgb(45, 45, 45);
            Control_Database_Label_DatabaseTitle.Location = new Point(68, 3);
            Control_Database_Label_DatabaseTitle.Margin = new Padding(3);
            Control_Database_Label_DatabaseTitle.MaximumSize = new Size(350, 0);
            Control_Database_Label_DatabaseTitle.MinimumSize = new Size(350, 0);
            Control_Database_Label_DatabaseTitle.Name = "Control_Database_Label_DatabaseTitle";
            Control_Database_Label_DatabaseTitle.Size = new Size(350, 21);
            Control_Database_Label_DatabaseTitle.TabIndex = 1;
            Control_Database_Label_DatabaseTitle.Text = "Database Name";
            Control_Database_Label_DatabaseTitle.TextAlign = ContentAlignment.BottomLeft;
            //
            // Control_Database_Label_DatabaseIcon
            //
            Control_Database_Label_DatabaseIcon.AutoSize = true;
            Control_Database_Label_DatabaseIcon.Dock = DockStyle.Fill;
            Control_Database_Label_DatabaseIcon.Font = new Font("Segoe UI Emoji", 24F);
            Control_Database_Label_DatabaseIcon.Location = new Point(1, 1);
            Control_Database_Label_DatabaseIcon.Margin = new Padding(1);
            Control_Database_Label_DatabaseIcon.Name = "Control_Database_Label_DatabaseIcon";
            tableLayoutPanel3.SetRowSpan(Control_Database_Label_DatabaseIcon, 2);
            Control_Database_Label_DatabaseIcon.Size = new Size(63, 58);
            Control_Database_Label_DatabaseIcon.TabIndex = 0;
            Control_Database_Label_DatabaseIcon.Text = "💾";
            Control_Database_Label_DatabaseIcon.TextAlign = ContentAlignment.MiddleCenter;
            //
            // Control_Database_Panel_PortCard
            //
            Control_Database_Panel_PortCard.AutoSize = true;
            Control_Database_Panel_PortCard.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_Database_Panel_PortCard.BackColor = Color.White;
            Control_Database_Panel_PortCard.BorderStyle = BorderStyle.FixedSingle;
            Control_Database_Panel_PortCard.Controls.Add(tableLayoutPanel2);
            Control_Database_Panel_PortCard.Dock = DockStyle.Fill;
            Control_Database_Panel_PortCard.Location = new Point(0, 87);
            Control_Database_Panel_PortCard.Margin = new Padding(0, 5, 0, 5);
            Control_Database_Panel_PortCard.Name = "Control_Database_Panel_PortCard";
            Control_Database_Panel_PortCard.Padding = new Padding(5);
            Control_Database_Panel_PortCard.Size = new Size(433, 72);
            Control_Database_Panel_PortCard.TabIndex = 1;
            //
            // tableLayoutPanel2
            //
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(Control_Database_TextBox_Port, 1, 1);
            tableLayoutPanel2.Controls.Add(Control_Database_Label_PortTitle, 1, 0);
            tableLayoutPanel2.Controls.Add(Control_Database_Label_PortIcon, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(5, 5);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(421, 60);
            tableLayoutPanel2.TabIndex = 4;
            //
            // Control_Database_TextBox_Port
            //
            Control_Database_TextBox_Port.Dock = DockStyle.Fill;
            Control_Database_TextBox_Port.Font = new Font("Segoe UI", 11F);
            Control_Database_TextBox_Port.Location = new Point(68, 30);
            Control_Database_TextBox_Port.MaximumSize = new Size(350, 0);
            Control_Database_TextBox_Port.MinimumSize = new Size(350, 0);
            Control_Database_TextBox_Port.Name = "Control_Database_TextBox_Port";
            Control_Database_TextBox_Port.PlaceholderText = "Enter MySQL port (default: 3306)";
            Control_Database_TextBox_Port.Size = new Size(350, 27);
            Control_Database_TextBox_Port.TabIndex = 2;
            //
            // Control_Database_Label_PortTitle
            //
            Control_Database_Label_PortTitle.AutoSize = true;
            Control_Database_Label_PortTitle.Dock = DockStyle.Fill;
            Control_Database_Label_PortTitle.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            Control_Database_Label_PortTitle.ForeColor = Color.FromArgb(45, 45, 45);
            Control_Database_Label_PortTitle.Location = new Point(68, 3);
            Control_Database_Label_PortTitle.Margin = new Padding(3);
            Control_Database_Label_PortTitle.MaximumSize = new Size(350, 0);
            Control_Database_Label_PortTitle.MinimumSize = new Size(350, 0);
            Control_Database_Label_PortTitle.Name = "Control_Database_Label_PortTitle";
            Control_Database_Label_PortTitle.Size = new Size(350, 21);
            Control_Database_Label_PortTitle.TabIndex = 1;
            Control_Database_Label_PortTitle.Text = "Port Number";
            Control_Database_Label_PortTitle.TextAlign = ContentAlignment.BottomLeft;
            //
            // Control_Database_Label_PortIcon
            //
            Control_Database_Label_PortIcon.AutoSize = true;
            Control_Database_Label_PortIcon.Dock = DockStyle.Fill;
            Control_Database_Label_PortIcon.Font = new Font("Segoe UI Emoji", 24F);
            Control_Database_Label_PortIcon.Location = new Point(1, 1);
            Control_Database_Label_PortIcon.Margin = new Padding(1);
            Control_Database_Label_PortIcon.Name = "Control_Database_Label_PortIcon";
            tableLayoutPanel2.SetRowSpan(Control_Database_Label_PortIcon, 2);
            Control_Database_Label_PortIcon.Size = new Size(63, 58);
            Control_Database_Label_PortIcon.TabIndex = 0;
            Control_Database_Label_PortIcon.Text = "🔌";
            Control_Database_Label_PortIcon.TextAlign = ContentAlignment.MiddleCenter;
            //
            // Control_Database_FlowPanel_Actions
            //
            Control_Database_FlowPanel_Actions.AutoSize = true;
            Control_Database_FlowPanel_Actions.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_Database_FlowPanel_Actions.Controls.Add(Control_Database_Button_TestConnection);
            Control_Database_FlowPanel_Actions.Controls.Add(Control_Database_Button_Reset);
            Control_Database_FlowPanel_Actions.Controls.Add(Control_Database_Button_Save);
            Control_Database_FlowPanel_Actions.Dock = DockStyle.Fill;
            Control_Database_FlowPanel_Actions.FlowDirection = FlowDirection.RightToLeft;
            Control_Database_FlowPanel_Actions.Location = new Point(23, 307);
            Control_Database_FlowPanel_Actions.Name = "Control_Database_FlowPanel_Actions";
            Control_Database_FlowPanel_Actions.Size = new Size(433, 41);
            Control_Database_FlowPanel_Actions.TabIndex = 2;
            //
            // Control_Database_Button_TestConnection
            //
            Control_Database_Button_TestConnection.BackColor = Color.FromArgb(0, 120, 215);
            Control_Database_Button_TestConnection.FlatStyle = FlatStyle.Flat;
            Control_Database_Button_TestConnection.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            Control_Database_Button_TestConnection.ForeColor = Color.White;
            Control_Database_Button_TestConnection.Location = new Point(253, 3);
            Control_Database_Button_TestConnection.Name = "Control_Database_Button_TestConnection";
            Control_Database_Button_TestConnection.Size = new Size(177, 35);
            Control_Database_Button_TestConnection.TabIndex = 2;
            Control_Database_Button_TestConnection.Text = "🔍 Test Connection";
            Control_Database_Button_TestConnection.UseVisualStyleBackColor = false;
            Control_Database_Button_TestConnection.Click += Control_Database_Button_TestConnection_Click;
            //
            // Control_Database_Button_Reset
            //
            Control_Database_Button_Reset.BackColor = Color.FromArgb(96, 94, 92);
            Control_Database_Button_Reset.FlatStyle = FlatStyle.Flat;
            Control_Database_Button_Reset.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            Control_Database_Button_Reset.ForeColor = Color.White;
            Control_Database_Button_Reset.Location = new Point(147, 3);
            Control_Database_Button_Reset.Name = "Control_Database_Button_Reset";
            Control_Database_Button_Reset.Size = new Size(100, 35);
            Control_Database_Button_Reset.TabIndex = 1;
            Control_Database_Button_Reset.Text = "Reset";
            Control_Database_Button_Reset.UseVisualStyleBackColor = false;
            Control_Database_Button_Reset.Click += Control_Database_Button_Reset_Click;
            //
            // Control_Database_Button_Save
            //
            Control_Database_Button_Save.BackColor = Color.FromArgb(16, 124, 16);
            Control_Database_Button_Save.FlatStyle = FlatStyle.Flat;
            Control_Database_Button_Save.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            Control_Database_Button_Save.ForeColor = Color.White;
            Control_Database_Button_Save.Location = new Point(41, 3);
            Control_Database_Button_Save.Name = "Control_Database_Button_Save";
            Control_Database_Button_Save.Size = new Size(100, 35);
            Control_Database_Button_Save.TabIndex = 0;
            Control_Database_Button_Save.Text = "💾 Save";
            Control_Database_Button_Save.UseVisualStyleBackColor = false;
            Control_Database_Button_Save.Click += Control_Database_Button_Save_Click;
            //
            // Control_Database
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = SystemColors.Control;
            Controls.Add(Control_Database_TableLayout_Main);
            Name = "Control_Database";
            Size = new Size(479, 371);
            Control_Database_TableLayout_Main.ResumeLayout(false);
            Control_Database_TableLayout_Main.PerformLayout();
            Control_Database_Panel_Content.ResumeLayout(false);
            Control_Database_Panel_Content.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            Control_Database_Panel_ServerCard.ResumeLayout(false);
            Control_Database_Panel_ServerCard.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            Control_Database_Panel_DatabaseCard.ResumeLayout(false);
            Control_Database_Panel_DatabaseCard.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            Control_Database_Panel_PortCard.ResumeLayout(false);
            Control_Database_Panel_PortCard.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            Control_Database_FlowPanel_Actions.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel4;
    }
}
