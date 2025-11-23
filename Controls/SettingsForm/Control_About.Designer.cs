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
            this.Control_About_TableLayout_Main = new System.Windows.Forms.TableLayoutPanel();
            this.Control_About_Panel_AppInfo = new System.Windows.Forms.Panel();
            this.Control_About_Label_AppInfo_Owner = new System.Windows.Forms.Label();
            this.Control_About_Label_AppInfo_Copyright = new System.Windows.Forms.Label();
            this.Control_About_Label_AppInfo_Version = new System.Windows.Forms.Label();
            this.Control_About_Label_AppInfo_Title = new System.Windows.Forms.Label();
            this.Control_About_Panel_DevInfo = new System.Windows.Forms.Panel();
            this.Control_About_Label_DevInfo_Contact = new System.Windows.Forms.Label();
            this.Control_About_Label_DevInfo_Name = new System.Windows.Forms.Label();
            this.Control_About_Label_DevInfo_Title = new System.Windows.Forms.Label();
            this.Control_About_Panel_ReleaseNotes = new System.Windows.Forms.Panel();
            this.Control_About_Button_ViewReleaseNotes = new System.Windows.Forms.Button();
            this.Control_About_Label_ReleaseNotes_Desc = new System.Windows.Forms.Label();
            this.Control_About_Label_ReleaseNotes_Title = new System.Windows.Forms.Label();
            this.Control_About_TableLayout_Main.SuspendLayout();
            this.Control_About_Panel_AppInfo.SuspendLayout();
            this.Control_About_Panel_DevInfo.SuspendLayout();
            this.Control_About_Panel_ReleaseNotes.SuspendLayout();
            this.SuspendLayout();
            // 
            // Control_About_TableLayout_Main
            // 
            this.Control_About_TableLayout_Main.ColumnCount = 2;
            this.Control_About_TableLayout_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Control_About_TableLayout_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Control_About_TableLayout_Main.Controls.Add(this.Control_About_Panel_AppInfo, 0, 0);
            this.Control_About_TableLayout_Main.Controls.Add(this.Control_About_Panel_DevInfo, 1, 0);
            this.Control_About_TableLayout_Main.Controls.Add(this.Control_About_Panel_ReleaseNotes, 0, 1);
            this.Control_About_TableLayout_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Control_About_TableLayout_Main.Location = new System.Drawing.Point(0, 0);
            this.Control_About_TableLayout_Main.Name = "Control_About_TableLayout_Main";
            this.Control_About_TableLayout_Main.Padding = new System.Windows.Forms.Padding(10);
            this.Control_About_TableLayout_Main.RowCount = 2;
            this.Control_About_TableLayout_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Control_About_TableLayout_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.Control_About_TableLayout_Main.Size = new System.Drawing.Size(800, 600);
            this.Control_About_TableLayout_Main.TabIndex = 0;
            // 
            // Control_About_Panel_AppInfo
            // 
            this.Control_About_Panel_AppInfo.BackColor = System.Drawing.Color.White;
            this.Control_About_Panel_AppInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Control_About_Panel_AppInfo.Controls.Add(this.Control_About_Label_AppInfo_Owner);
            this.Control_About_Panel_AppInfo.Controls.Add(this.Control_About_Label_AppInfo_Copyright);
            this.Control_About_Panel_AppInfo.Controls.Add(this.Control_About_Label_AppInfo_Version);
            this.Control_About_Panel_AppInfo.Controls.Add(this.Control_About_Label_AppInfo_Title);
            this.Control_About_Panel_AppInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Control_About_Panel_AppInfo.Location = new System.Drawing.Point(13, 13);
            this.Control_About_Panel_AppInfo.Margin = new System.Windows.Forms.Padding(3, 3, 10, 10);
            this.Control_About_Panel_AppInfo.Name = "Control_About_Panel_AppInfo";
            this.Control_About_Panel_AppInfo.Padding = new System.Windows.Forms.Padding(20);
            this.Control_About_Panel_AppInfo.Size = new System.Drawing.Size(377, 277);
            this.Control_About_Panel_AppInfo.TabIndex = 0;
            // 
            // Control_About_Label_AppInfo_Owner
            // 
            this.Control_About_Label_AppInfo_Owner.AutoSize = true;
            this.Control_About_Label_AppInfo_Owner.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Control_About_Label_AppInfo_Owner.Location = new System.Drawing.Point(20, 120);
            this.Control_About_Label_AppInfo_Owner.Name = "Control_About_Label_AppInfo_Owner";
            this.Control_About_Label_AppInfo_Owner.Size = new System.Drawing.Size(250, 19);
            this.Control_About_Label_AppInfo_Owner.TabIndex = 3;
            this.Control_About_Label_AppInfo_Owner.Text = "Owner: Manitowoc Tool and Manufacturing";
            // 
            // Control_About_Label_AppInfo_Copyright
            // 
            this.Control_About_Label_AppInfo_Copyright.AutoSize = true;
            this.Control_About_Label_AppInfo_Copyright.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Control_About_Label_AppInfo_Copyright.Location = new System.Drawing.Point(20, 90);
            this.Control_About_Label_AppInfo_Copyright.Name = "Control_About_Label_AppInfo_Copyright";
            this.Control_About_Label_AppInfo_Copyright.Size = new System.Drawing.Size(72, 19);
            this.Control_About_Label_AppInfo_Copyright.TabIndex = 2;
            this.Control_About_Label_AppInfo_Copyright.Text = "Copyright:";
            // 
            // Control_About_Label_AppInfo_Version
            // 
            this.Control_About_Label_AppInfo_Version.AutoSize = true;
            this.Control_About_Label_AppInfo_Version.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Control_About_Label_AppInfo_Version.Location = new System.Drawing.Point(20, 60);
            this.Control_About_Label_AppInfo_Version.Name = "Control_About_Label_AppInfo_Version";
            this.Control_About_Label_AppInfo_Version.Size = new System.Drawing.Size(57, 19);
            this.Control_About_Label_AppInfo_Version.TabIndex = 1;
            this.Control_About_Label_AppInfo_Version.Text = "Version:";
            // 
            // Control_About_Label_AppInfo_Title
            // 
            this.Control_About_Label_AppInfo_Title.AutoSize = true;
            this.Control_About_Label_AppInfo_Title.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.Control_About_Label_AppInfo_Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.Control_About_Label_AppInfo_Title.Location = new System.Drawing.Point(20, 20);
            this.Control_About_Label_AppInfo_Title.Name = "Control_About_Label_AppInfo_Title";
            this.Control_About_Label_AppInfo_Title.Size = new System.Drawing.Size(228, 25);
            this.Control_About_Label_AppInfo_Title.TabIndex = 0;
            this.Control_About_Label_AppInfo_Title.Text = "Application Information";
            // 
            // Control_About_Panel_DevInfo
            // 
            this.Control_About_Panel_DevInfo.BackColor = System.Drawing.Color.White;
            this.Control_About_Panel_DevInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Control_About_Panel_DevInfo.Controls.Add(this.Control_About_Label_DevInfo_Contact);
            this.Control_About_Panel_DevInfo.Controls.Add(this.Control_About_Label_DevInfo_Name);
            this.Control_About_Panel_DevInfo.Controls.Add(this.Control_About_Label_DevInfo_Title);
            this.Control_About_Panel_DevInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Control_About_Panel_DevInfo.Location = new System.Drawing.Point(403, 13);
            this.Control_About_Panel_DevInfo.Margin = new System.Windows.Forms.Padding(3, 3, 10, 10);
            this.Control_About_Panel_DevInfo.Name = "Control_About_Panel_DevInfo";
            this.Control_About_Panel_DevInfo.Padding = new System.Windows.Forms.Padding(20);
            this.Control_About_Panel_DevInfo.Size = new System.Drawing.Size(377, 277);
            this.Control_About_Panel_DevInfo.TabIndex = 1;
            // 
            // Control_About_Label_DevInfo_Contact
            // 
            this.Control_About_Label_DevInfo_Contact.AutoSize = true;
            this.Control_About_Label_DevInfo_Contact.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Control_About_Label_DevInfo_Contact.Location = new System.Drawing.Point(20, 90);
            this.Control_About_Label_DevInfo_Contact.Name = "Control_About_Label_DevInfo_Contact";
            this.Control_About_Label_DevInfo_Contact.Size = new System.Drawing.Size(198, 19);
            this.Control_About_Label_DevInfo_Contact.TabIndex = 2;
            this.Control_About_Label_DevInfo_Contact.Text = "Contact: jkoll@mantoolmfg.com";
            // 
            // Control_About_Label_DevInfo_Name
            // 
            this.Control_About_Label_DevInfo_Name.AutoSize = true;
            this.Control_About_Label_DevInfo_Name.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Control_About_Label_DevInfo_Name.Location = new System.Drawing.Point(20, 60);
            this.Control_About_Label_DevInfo_Name.Name = "Control_About_Label_DevInfo_Name";
            this.Control_About_Label_DevInfo_Name.Size = new System.Drawing.Size(136, 19);
            this.Control_About_Label_DevInfo_Name.TabIndex = 1;
            this.Control_About_Label_DevInfo_Name.Text = "Developer: John Koll";
            // 
            // Control_About_Label_DevInfo_Title
            // 
            this.Control_About_Label_DevInfo_Title.AutoSize = true;
            this.Control_About_Label_DevInfo_Title.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.Control_About_Label_DevInfo_Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.Control_About_Label_DevInfo_Title.Location = new System.Drawing.Point(20, 20);
            this.Control_About_Label_DevInfo_Title.Name = "Control_About_Label_DevInfo_Title";
            this.Control_About_Label_DevInfo_Title.Size = new System.Drawing.Size(213, 25);
            this.Control_About_Label_DevInfo_Title.TabIndex = 0;
            this.Control_About_Label_DevInfo_Title.Text = "Developer Information";
            // 
            // Control_About_Panel_ReleaseNotes
            // 
            this.Control_About_Panel_ReleaseNotes.BackColor = System.Drawing.Color.White;
            this.Control_About_Panel_ReleaseNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Control_About_TableLayout_Main.SetColumnSpan(this.Control_About_Panel_ReleaseNotes, 2);
            this.Control_About_Panel_ReleaseNotes.Controls.Add(this.Control_About_Button_ViewReleaseNotes);
            this.Control_About_Panel_ReleaseNotes.Controls.Add(this.Control_About_Label_ReleaseNotes_Desc);
            this.Control_About_Panel_ReleaseNotes.Controls.Add(this.Control_About_Label_ReleaseNotes_Title);
            this.Control_About_Panel_ReleaseNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Control_About_Panel_ReleaseNotes.Location = new System.Drawing.Point(13, 303);
            this.Control_About_Panel_ReleaseNotes.Margin = new System.Windows.Forms.Padding(3, 3, 10, 10);
            this.Control_About_Panel_ReleaseNotes.Name = "Control_About_Panel_ReleaseNotes";
            this.Control_About_Panel_ReleaseNotes.Padding = new System.Windows.Forms.Padding(20);
            this.Control_About_Panel_ReleaseNotes.Size = new System.Drawing.Size(767, 277);
            this.Control_About_Panel_ReleaseNotes.TabIndex = 2;
            // 
            // Control_About_Button_ViewReleaseNotes
            // 
            this.Control_About_Button_ViewReleaseNotes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.Control_About_Button_ViewReleaseNotes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Control_About_Button_ViewReleaseNotes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.Control_About_Button_ViewReleaseNotes.ForeColor = System.Drawing.Color.White;
            this.Control_About_Button_ViewReleaseNotes.Location = new System.Drawing.Point(20, 100);
            this.Control_About_Button_ViewReleaseNotes.Name = "Control_About_Button_ViewReleaseNotes";
            this.Control_About_Button_ViewReleaseNotes.Size = new System.Drawing.Size(200, 40);
            this.Control_About_Button_ViewReleaseNotes.TabIndex = 2;
            this.Control_About_Button_ViewReleaseNotes.Text = "View Release Notes";
            this.Control_About_Button_ViewReleaseNotes.UseVisualStyleBackColor = false;
            this.Control_About_Button_ViewReleaseNotes.Click += new System.EventHandler(this.Control_About_Button_ViewReleaseNotes_Click);
            // 
            // Control_About_Label_ReleaseNotes_Desc
            // 
            this.Control_About_Label_ReleaseNotes_Desc.AutoSize = true;
            this.Control_About_Label_ReleaseNotes_Desc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.Control_About_Label_ReleaseNotes_Desc.Location = new System.Drawing.Point(20, 60);
            this.Control_About_Label_ReleaseNotes_Desc.Name = "Control_About_Label_ReleaseNotes_Desc";
            this.Control_About_Label_ReleaseNotes_Desc.Size = new System.Drawing.Size(350, 19);
            this.Control_About_Label_ReleaseNotes_Desc.TabIndex = 1;
            this.Control_About_Label_ReleaseNotes_Desc.Text = "View the latest changes and updates to the application.";
            // 
            // Control_About_Label_ReleaseNotes_Title
            // 
            this.Control_About_Label_ReleaseNotes_Title.AutoSize = true;
            this.Control_About_Label_ReleaseNotes_Title.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.Control_About_Label_ReleaseNotes_Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(212)))));
            this.Control_About_Label_ReleaseNotes_Title.Location = new System.Drawing.Point(20, 20);
            this.Control_About_Label_ReleaseNotes_Title.Name = "Control_About_Label_ReleaseNotes_Title";
            this.Control_About_Label_ReleaseNotes_Title.Size = new System.Drawing.Size(134, 25);
            this.Control_About_Label_ReleaseNotes_Title.TabIndex = 0;
            this.Control_About_Label_ReleaseNotes_Title.Text = "Release Notes";
            // 
            // Control_About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.Control_About_TableLayout_Main);
            this.Name = "Control_About";
            this.Size = new System.Drawing.Size(800, 600);
            this.Control_About_TableLayout_Main.ResumeLayout(false);
            this.Control_About_Panel_AppInfo.ResumeLayout(false);
            this.Control_About_Panel_AppInfo.PerformLayout();
            this.Control_About_Panel_DevInfo.ResumeLayout(false);
            this.Control_About_Panel_DevInfo.PerformLayout();
            this.Control_About_Panel_ReleaseNotes.ResumeLayout(false);
            this.Control_About_Panel_ReleaseNotes.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel Control_About_TableLayout_Main;
        private System.Windows.Forms.Panel Control_About_Panel_AppInfo;
        private System.Windows.Forms.Label Control_About_Label_AppInfo_Title;
        private System.Windows.Forms.Label Control_About_Label_AppInfo_Version;
        private System.Windows.Forms.Label Control_About_Label_AppInfo_Copyright;
        private System.Windows.Forms.Label Control_About_Label_AppInfo_Owner;
        private System.Windows.Forms.Panel Control_About_Panel_DevInfo;
        private System.Windows.Forms.Label Control_About_Label_DevInfo_Title;
        private System.Windows.Forms.Label Control_About_Label_DevInfo_Name;
        private System.Windows.Forms.Label Control_About_Label_DevInfo_Contact;
        private System.Windows.Forms.Panel Control_About_Panel_ReleaseNotes;
        private System.Windows.Forms.Label Control_About_Label_ReleaseNotes_Title;
        private System.Windows.Forms.Label Control_About_Label_ReleaseNotes_Desc;
        private System.Windows.Forms.Button Control_About_Button_ViewReleaseNotes;
    }
}
