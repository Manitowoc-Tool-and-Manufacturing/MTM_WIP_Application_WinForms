// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    partial class Control_About
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Control_About_TableLayout_Main = new TableLayoutPanel();
            Control_About_Panel_AppInfo = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            Control_About_Button_ViewReleaseNotes = new Button();
            Control_About_Button_Home = new Button();
            SettingsForm_Button_Help_About = new Button();
            Control_About_Label_DevInfo_Title = new Label();
            Control_About_Label_ReleaseNotes_Desc = new Label();
            Control_About_Label_DevInfo_Contact = new Label();
            Control_About_Label_DevInfo_Name = new Label();
            Control_About_Label_ReleaseNotes_Title = new Label();
            tableLayoutPanel4 = new TableLayoutPanel();
            pictureBox1 = new PictureBox();
            Control_About_Label_AppInfo_Version = new Label();
            Control_About_Label_AppInfo_Title = new Label();
            Control_About_Label_AppInfo_Owner = new Label();
            Control_About_Label_AppInfo_Copyright = new Label();
            Control_About_TableLayout_Main.SuspendLayout();
            Control_About_Panel_AppInfo.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // Control_About_TableLayout_Main
            // 
            Control_About_TableLayout_Main.ColumnCount = 3;
            Control_About_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            Control_About_TableLayout_Main.ColumnStyles.Add(new ColumnStyle());
            Control_About_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.0000076F));
            Control_About_TableLayout_Main.Controls.Add(Control_About_Panel_AppInfo, 1, 0);
            Control_About_TableLayout_Main.Dock = DockStyle.Fill;
            Control_About_TableLayout_Main.Location = new Point(0, 0);
            Control_About_TableLayout_Main.Name = "Control_About_TableLayout_Main";
            Control_About_TableLayout_Main.Padding = new Padding(10);
            Control_About_TableLayout_Main.RowCount = 2;
            Control_About_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Control_About_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            Control_About_TableLayout_Main.Size = new Size(800, 600);
            Control_About_TableLayout_Main.TabIndex = 0;
            // 
            // Control_About_Panel_AppInfo
            // 
            Control_About_Panel_AppInfo.AutoSize = true;
            Control_About_Panel_AppInfo.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_About_Panel_AppInfo.BackColor = Color.White;
            Control_About_Panel_AppInfo.BorderStyle = BorderStyle.FixedSingle;
            Control_About_Panel_AppInfo.Controls.Add(tableLayoutPanel1);
            Control_About_Panel_AppInfo.Dock = DockStyle.Fill;
            Control_About_Panel_AppInfo.Location = new Point(150, 10);
            Control_About_Panel_AppInfo.Margin = new Padding(0);
            Control_About_Panel_AppInfo.Name = "Control_About_Panel_AppInfo";
            Control_About_Panel_AppInfo.Padding = new Padding(20);
            Control_About_TableLayout_Main.SetRowSpan(Control_About_Panel_AppInfo, 2);
            Control_About_Panel_AppInfo.Size = new Size(499, 580);
            Control_About_Panel_AppInfo.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 5);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel4, 0, 4);
            tableLayoutPanel1.Controls.Add(Control_About_Label_AppInfo_Version, 0, 1);
            tableLayoutPanel1.Controls.Add(Control_About_Label_AppInfo_Title, 0, 0);
            tableLayoutPanel1.Controls.Add(Control_About_Label_AppInfo_Owner, 0, 3);
            tableLayoutPanel1.Controls.Add(Control_About_Label_AppInfo_Copyright, 0, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(20, 20);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(457, 538);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 0, 5);
            tableLayoutPanel2.Controls.Add(Control_About_Label_DevInfo_Title, 0, 0);
            tableLayoutPanel2.Controls.Add(Control_About_Label_ReleaseNotes_Desc, 0, 4);
            tableLayoutPanel2.Controls.Add(Control_About_Label_DevInfo_Contact, 0, 2);
            tableLayoutPanel2.Controls.Add(Control_About_Label_DevInfo_Name, 0, 1);
            tableLayoutPanel2.Controls.Add(Control_About_Label_ReleaseNotes_Title, 0, 3);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 334);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 7;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(451, 201);
            tableLayoutPanel2.TabIndex = 3;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.AutoSize = true;
            tableLayoutPanel3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel3.ColumnCount = 5;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.Controls.Add(Control_About_Button_ViewReleaseNotes, 1, 0);
            tableLayoutPanel3.Controls.Add(Control_About_Button_Home, 2, 0);
            tableLayoutPanel3.Controls.Add(SettingsForm_Button_Help_About, 4, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(6, 157);
            tableLayoutPanel3.Margin = new Padding(6);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(439, 38);
            tableLayoutPanel3.TabIndex = 3;
            // 
            // Control_About_Button_ViewReleaseNotes
            // 
            Control_About_Button_ViewReleaseNotes.BackColor = Color.FromArgb(0, 120, 212);
            Control_About_Button_ViewReleaseNotes.FlatStyle = FlatStyle.Flat;
            Control_About_Button_ViewReleaseNotes.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            Control_About_Button_ViewReleaseNotes.ForeColor = Color.White;
            Control_About_Button_ViewReleaseNotes.Location = new Point(3, 3);
            Control_About_Button_ViewReleaseNotes.MaximumSize = new Size(0, 32);
            Control_About_Button_ViewReleaseNotes.MinimumSize = new Size(0, 32);
            Control_About_Button_ViewReleaseNotes.Name = "Control_About_Button_ViewReleaseNotes";
            Control_About_Button_ViewReleaseNotes.Size = new Size(250, 32);
            Control_About_Button_ViewReleaseNotes.TabIndex = 2;
            Control_About_Button_ViewReleaseNotes.Text = "View Release Notes";
            Control_About_Button_ViewReleaseNotes.UseVisualStyleBackColor = false;
            Control_About_Button_ViewReleaseNotes.Click += Control_About_Button_ViewReleaseNotes_Click;
            // 
            // Control_About_Button_Home
            // 
            Control_About_Button_Home.AutoSize = true;
            Control_About_Button_Home.Location = new Point(259, 3);
            Control_About_Button_Home.MaximumSize = new Size(0, 32);
            Control_About_Button_Home.MinimumSize = new Size(0, 32);
            Control_About_Button_Home.Name = "Control_About_Button_Home";
            Control_About_Button_Home.Size = new Size(139, 32);
            Control_About_Button_Home.TabIndex = 3;
            Control_About_Button_Home.Text = "🏠 Back to Home";
            // 
            // SettingsForm_Button_Help_About
            // 
            SettingsForm_Button_Help_About.AutoSize = true;
            SettingsForm_Button_Help_About.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            SettingsForm_Button_Help_About.Location = new Point(404, 3);
            SettingsForm_Button_Help_About.MaximumSize = new Size(32, 32);
            SettingsForm_Button_Help_About.MinimumSize = new Size(32, 32);
            SettingsForm_Button_Help_About.Name = "SettingsForm_Button_Help_About";
            SettingsForm_Button_Help_About.Size = new Size(32, 32);
            SettingsForm_Button_Help_About.TabIndex = 12;
            SettingsForm_Button_Help_About.Text = "?";
            SettingsForm_Button_Help_About.UseVisualStyleBackColor = true;
            // 
            // Control_About_Label_DevInfo_Title
            // 
            Control_About_Label_DevInfo_Title.AutoSize = true;
            Control_About_Label_DevInfo_Title.Dock = DockStyle.Fill;
            Control_About_Label_DevInfo_Title.Font = new Font("Segoe UI Emoji", 14F, FontStyle.Bold);
            Control_About_Label_DevInfo_Title.ForeColor = Color.FromArgb(0, 120, 212);
            Control_About_Label_DevInfo_Title.Location = new Point(6, 6);
            Control_About_Label_DevInfo_Title.Margin = new Padding(6);
            Control_About_Label_DevInfo_Title.Name = "Control_About_Label_DevInfo_Title";
            Control_About_Label_DevInfo_Title.Size = new Size(439, 26);
            Control_About_Label_DevInfo_Title.TabIndex = 0;
            Control_About_Label_DevInfo_Title.Text = "Developer Information";
            Control_About_Label_DevInfo_Title.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_About_Label_ReleaseNotes_Desc
            // 
            Control_About_Label_ReleaseNotes_Desc.AutoSize = true;
            Control_About_Label_ReleaseNotes_Desc.Font = new Font("Segoe UI Emoji", 10F);
            Control_About_Label_ReleaseNotes_Desc.Location = new Point(3, 129);
            Control_About_Label_ReleaseNotes_Desc.Margin = new Padding(3);
            Control_About_Label_ReleaseNotes_Desc.Name = "Control_About_Label_ReleaseNotes_Desc";
            Control_About_Label_ReleaseNotes_Desc.Size = new Size(347, 19);
            Control_About_Label_ReleaseNotes_Desc.TabIndex = 1;
            Control_About_Label_ReleaseNotes_Desc.Text = "View the latest changes and updates to the application.";
            // 
            // Control_About_Label_DevInfo_Contact
            // 
            Control_About_Label_DevInfo_Contact.AutoSize = true;
            Control_About_Label_DevInfo_Contact.Dock = DockStyle.Fill;
            Control_About_Label_DevInfo_Contact.Font = new Font("Segoe UI Emoji", 10F);
            Control_About_Label_DevInfo_Contact.Location = new Point(3, 66);
            Control_About_Label_DevInfo_Contact.Margin = new Padding(3);
            Control_About_Label_DevInfo_Contact.Name = "Control_About_Label_DevInfo_Contact";
            Control_About_Label_DevInfo_Contact.Size = new Size(445, 19);
            Control_About_Label_DevInfo_Contact.TabIndex = 2;
            Control_About_Label_DevInfo_Contact.Text = "Contact: jkoll@mantoolmfg.com";
            Control_About_Label_DevInfo_Contact.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_About_Label_DevInfo_Name
            // 
            Control_About_Label_DevInfo_Name.AutoSize = true;
            Control_About_Label_DevInfo_Name.Dock = DockStyle.Fill;
            Control_About_Label_DevInfo_Name.Font = new Font("Segoe UI Emoji", 10F);
            Control_About_Label_DevInfo_Name.Location = new Point(3, 41);
            Control_About_Label_DevInfo_Name.Margin = new Padding(3);
            Control_About_Label_DevInfo_Name.Name = "Control_About_Label_DevInfo_Name";
            Control_About_Label_DevInfo_Name.Size = new Size(445, 19);
            Control_About_Label_DevInfo_Name.TabIndex = 1;
            Control_About_Label_DevInfo_Name.Text = "Developer: John Koll";
            Control_About_Label_DevInfo_Name.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_About_Label_ReleaseNotes_Title
            // 
            Control_About_Label_ReleaseNotes_Title.AutoSize = true;
            Control_About_Label_ReleaseNotes_Title.Font = new Font("Segoe UI Emoji", 14F, FontStyle.Bold);
            Control_About_Label_ReleaseNotes_Title.ForeColor = Color.FromArgb(0, 120, 212);
            Control_About_Label_ReleaseNotes_Title.Location = new Point(6, 94);
            Control_About_Label_ReleaseNotes_Title.Margin = new Padding(6);
            Control_About_Label_ReleaseNotes_Title.Name = "Control_About_Label_ReleaseNotes_Title";
            Control_About_Label_ReleaseNotes_Title.Size = new Size(143, 26);
            Control_About_Label_ReleaseNotes_Title.TabIndex = 0;
            Control_About_Label_ReleaseNotes_Title.Text = "Release Notes";
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.AutoSize = true;
            tableLayoutPanel4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel4.ColumnCount = 3;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50.0000076F));
            tableLayoutPanel4.Controls.Add(pictureBox1, 1, 1);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(3, 116);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 3;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 49.9999962F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50.0000076F));
            tableLayoutPanel4.Size = new Size(451, 212);
            tableLayoutPanel4.TabIndex = 1;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Image = Properties.Resources.MTM;
            pictureBox1.InitialImage = Properties.Resources.MTM;
            pictureBox1.Location = new Point(135, 55);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(179, 100);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // Control_About_Label_AppInfo_Version
            // 
            Control_About_Label_AppInfo_Version.AutoSize = true;
            Control_About_Label_AppInfo_Version.Dock = DockStyle.Fill;
            Control_About_Label_AppInfo_Version.Font = new Font("Segoe UI Emoji", 10F);
            Control_About_Label_AppInfo_Version.Location = new Point(3, 41);
            Control_About_Label_AppInfo_Version.Margin = new Padding(3);
            Control_About_Label_AppInfo_Version.Name = "Control_About_Label_AppInfo_Version";
            Control_About_Label_AppInfo_Version.Size = new Size(451, 19);
            Control_About_Label_AppInfo_Version.TabIndex = 1;
            Control_About_Label_AppInfo_Version.Text = "Version:";
            Control_About_Label_AppInfo_Version.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_About_Label_AppInfo_Title
            // 
            Control_About_Label_AppInfo_Title.AutoSize = true;
            Control_About_Label_AppInfo_Title.Dock = DockStyle.Fill;
            Control_About_Label_AppInfo_Title.Font = new Font("Segoe UI Emoji", 14F, FontStyle.Bold);
            Control_About_Label_AppInfo_Title.ForeColor = Color.FromArgb(0, 120, 212);
            Control_About_Label_AppInfo_Title.Location = new Point(6, 6);
            Control_About_Label_AppInfo_Title.Margin = new Padding(6);
            Control_About_Label_AppInfo_Title.Name = "Control_About_Label_AppInfo_Title";
            Control_About_Label_AppInfo_Title.Size = new Size(445, 26);
            Control_About_Label_AppInfo_Title.TabIndex = 0;
            Control_About_Label_AppInfo_Title.Text = "Application Information";
            Control_About_Label_AppInfo_Title.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_About_Label_AppInfo_Owner
            // 
            Control_About_Label_AppInfo_Owner.AutoSize = true;
            Control_About_Label_AppInfo_Owner.Dock = DockStyle.Fill;
            Control_About_Label_AppInfo_Owner.Font = new Font("Segoe UI Emoji", 10F);
            Control_About_Label_AppInfo_Owner.Location = new Point(3, 91);
            Control_About_Label_AppInfo_Owner.Margin = new Padding(3);
            Control_About_Label_AppInfo_Owner.Name = "Control_About_Label_AppInfo_Owner";
            Control_About_Label_AppInfo_Owner.Size = new Size(451, 19);
            Control_About_Label_AppInfo_Owner.TabIndex = 3;
            Control_About_Label_AppInfo_Owner.Text = "Owner: Manitowoc Tool and Manufacturing";
            Control_About_Label_AppInfo_Owner.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_About_Label_AppInfo_Copyright
            // 
            Control_About_Label_AppInfo_Copyright.AutoSize = true;
            Control_About_Label_AppInfo_Copyright.Dock = DockStyle.Fill;
            Control_About_Label_AppInfo_Copyright.Font = new Font("Segoe UI Emoji", 10F);
            Control_About_Label_AppInfo_Copyright.Location = new Point(3, 66);
            Control_About_Label_AppInfo_Copyright.Margin = new Padding(3);
            Control_About_Label_AppInfo_Copyright.Name = "Control_About_Label_AppInfo_Copyright";
            Control_About_Label_AppInfo_Copyright.Size = new Size(451, 19);
            Control_About_Label_AppInfo_Copyright.TabIndex = 2;
            Control_About_Label_AppInfo_Copyright.Text = "Copyright:";
            Control_About_Label_AppInfo_Copyright.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_About
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            Controls.Add(Control_About_TableLayout_Main);
            Name = "Control_About";
            Size = new Size(800, 600);
            Control_About_TableLayout_Main.ResumeLayout(false);
            Control_About_TableLayout_Main.PerformLayout();
            Control_About_Panel_AppInfo.ResumeLayout(false);
            Control_About_Panel_AppInfo.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel Control_About_TableLayout_Main;
        private System.Windows.Forms.Panel Control_About_Panel_AppInfo;
        private System.Windows.Forms.Label Control_About_Label_AppInfo_Title;
        private System.Windows.Forms.Label Control_About_Label_AppInfo_Version;
        private System.Windows.Forms.Label Control_About_Label_AppInfo_Copyright;
        private System.Windows.Forms.Label Control_About_Label_AppInfo_Owner;
        private System.Windows.Forms.Label Control_About_Label_DevInfo_Title;
        private System.Windows.Forms.Label Control_About_Label_DevInfo_Name;
        private System.Windows.Forms.Label Control_About_Label_DevInfo_Contact;
        private System.Windows.Forms.Label Control_About_Label_ReleaseNotes_Title;
        private System.Windows.Forms.Label Control_About_Label_ReleaseNotes_Desc;
        private System.Windows.Forms.Button Control_About_Button_ViewReleaseNotes;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel4;
        private PictureBox pictureBox1;
        private Button SettingsForm_Button_Help_About;
        private Button Control_About_Button_Home;
    }
}
