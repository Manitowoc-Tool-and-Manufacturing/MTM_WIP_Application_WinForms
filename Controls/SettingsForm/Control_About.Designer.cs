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
            if (disposing)
            {
                // Clean up temp files when control is disposed
                CleanupTempFiles();
                
                if (components != null)
                {
                    components.Dispose();
                }
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
            Control_About_GroupBox_Main = new GroupBox();
            Control_About_TableLayout_Main = new TableLayoutPanel();
            Control_About_TableLayout_Bottom = new TableLayoutPanel();
            Control_About_TableLayout_Top = new TableLayoutPanel();
            Control_About_TableLayout_Info = new TableLayoutPanel();
            Control_About_Label_Version_Data = new Label();
            Control_About_Label_Version = new Label();
            Control_About_Label_LastUpdate_Data = new Label();
            Control_About_Label_LastUpdate = new Label();
            Control_About_Label_Copyright_Data = new Label();
            Control_About_Label_Copyright = new Label();
            Control_About_Label_Author_Data = new Label();
            Control_About_Label_Author = new Label();
            Control_About_Label_Main = new Label();
            Control_About_TableLayout_Changelog = new TableLayoutPanel();
            Control_About_Label_ChangeLog = new Label();
            Control_About_Label_WebView_ChangeLogView = new Microsoft.Web.WebView2.WinForms.WebView2();
            Control_About_GroupBox_Main.SuspendLayout();
            Control_About_TableLayout_Main.SuspendLayout();
            Control_About_TableLayout_Top.SuspendLayout();
            Control_About_TableLayout_Info.SuspendLayout();
            Control_About_TableLayout_Changelog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Control_About_Label_WebView_ChangeLogView).BeginInit();
            SuspendLayout();
            // 
            // Control_About_GroupBox_Main
            // 
            Control_About_GroupBox_Main.Controls.Add(Control_About_TableLayout_Main);
            Control_About_GroupBox_Main.Dock = DockStyle.Fill;
            Control_About_GroupBox_Main.Location = new Point(0, 0);
            Control_About_GroupBox_Main.Name = "Control_About_GroupBox_Main";
            Control_About_GroupBox_Main.Size = new Size(751, 504);
            Control_About_GroupBox_Main.TabIndex = 0;
            Control_About_GroupBox_Main.TabStop = false;
            Control_About_GroupBox_Main.Text = "About Application";
            // 
            // Control_About_TableLayout_Main
            // 
            Control_About_TableLayout_Main.AutoSize = true;
            Control_About_TableLayout_Main.ColumnCount = 1;
            Control_About_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_About_TableLayout_Main.Controls.Add(Control_About_TableLayout_Bottom, 0, 1);
            Control_About_TableLayout_Main.Controls.Add(Control_About_TableLayout_Top, 0, 0);
            Control_About_TableLayout_Main.Dock = DockStyle.Fill;
            Control_About_TableLayout_Main.Location = new Point(3, 19);
            Control_About_TableLayout_Main.Name = "Control_About_TableLayout_Main";
            Control_About_TableLayout_Main.RowCount = 2;
            Control_About_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 83.3333359F));
            Control_About_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 16.666666F));
            Control_About_TableLayout_Main.Size = new Size(745, 482);
            Control_About_TableLayout_Main.TabIndex = 0;
            // 
            // Control_About_TableLayout_Bottom
            // 
            Control_About_TableLayout_Bottom.AutoSize = true;
            Control_About_TableLayout_Bottom.ColumnCount = 3;
            Control_About_TableLayout_Bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            Control_About_TableLayout_Bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            Control_About_TableLayout_Bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333359F));
            Control_About_TableLayout_Bottom.Dock = DockStyle.Fill;
            Control_About_TableLayout_Bottom.Location = new Point(3, 404);
            Control_About_TableLayout_Bottom.Name = "Control_About_TableLayout_Bottom";
            Control_About_TableLayout_Bottom.RowCount = 1;
            Control_About_TableLayout_Bottom.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_About_TableLayout_Bottom.Size = new Size(739, 75);
            Control_About_TableLayout_Bottom.TabIndex = 0;
            // 
            // Control_About_TableLayout_Top
            // 
            Control_About_TableLayout_Top.AutoSize = true;
            Control_About_TableLayout_Top.ColumnCount = 2;
            Control_About_TableLayout_Top.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            Control_About_TableLayout_Top.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            Control_About_TableLayout_Top.Controls.Add(Control_About_TableLayout_Info, 0, 0);
            Control_About_TableLayout_Top.Controls.Add(Control_About_TableLayout_Changelog, 1, 0);
            Control_About_TableLayout_Top.Dock = DockStyle.Fill;
            Control_About_TableLayout_Top.Location = new Point(3, 3);
            Control_About_TableLayout_Top.Name = "Control_About_TableLayout_Top";
            Control_About_TableLayout_Top.RowCount = 1;
            Control_About_TableLayout_Top.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_About_TableLayout_Top.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            Control_About_TableLayout_Top.Size = new Size(739, 395);
            Control_About_TableLayout_Top.TabIndex = 1;
            // 
            // Control_About_TableLayout_Info
            // 
            Control_About_TableLayout_Info.ColumnCount = 2;
            Control_About_TableLayout_Info.ColumnStyles.Add(new ColumnStyle());
            Control_About_TableLayout_Info.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_About_TableLayout_Info.Controls.Add(Control_About_Label_Version_Data, 1, 4);
            Control_About_TableLayout_Info.Controls.Add(Control_About_Label_Version, 0, 4);
            Control_About_TableLayout_Info.Controls.Add(Control_About_Label_LastUpdate_Data, 1, 3);
            Control_About_TableLayout_Info.Controls.Add(Control_About_Label_LastUpdate, 0, 3);
            Control_About_TableLayout_Info.Controls.Add(Control_About_Label_Copyright_Data, 1, 2);
            Control_About_TableLayout_Info.Controls.Add(Control_About_Label_Copyright, 0, 2);
            Control_About_TableLayout_Info.Controls.Add(Control_About_Label_Author_Data, 1, 1);
            Control_About_TableLayout_Info.Controls.Add(Control_About_Label_Author, 0, 1);
            Control_About_TableLayout_Info.Controls.Add(Control_About_Label_Main, 0, 0);
            Control_About_TableLayout_Info.Location = new Point(3, 3);
            Control_About_TableLayout_Info.Name = "Control_About_TableLayout_Info";
            Control_About_TableLayout_Info.RowCount = 6;
            Control_About_TableLayout_Info.RowStyles.Add(new RowStyle());
            Control_About_TableLayout_Info.RowStyles.Add(new RowStyle());
            Control_About_TableLayout_Info.RowStyles.Add(new RowStyle());
            Control_About_TableLayout_Info.RowStyles.Add(new RowStyle());
            Control_About_TableLayout_Info.RowStyles.Add(new RowStyle());
            Control_About_TableLayout_Info.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_About_TableLayout_Info.Size = new Size(360, 389);
            Control_About_TableLayout_Info.TabIndex = 0;
            // 
            // Control_About_Label_Version_Data
            // 
            Control_About_Label_Version_Data.AutoSize = true;
            Control_About_Label_Version_Data.Dock = DockStyle.Fill;
            Control_About_Label_Version_Data.Font = new Font("Segoe UI", 9F);
            Control_About_Label_Version_Data.Location = new Point(127, 95);
            Control_About_Label_Version_Data.Margin = new Padding(3);
            Control_About_Label_Version_Data.Name = "Control_About_Label_Version_Data";
            Control_About_Label_Version_Data.Size = new Size(230, 17);
            Control_About_Label_Version_Data.TabIndex = 8;
            Control_About_Label_Version_Data.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_About_Label_Version
            // 
            Control_About_Label_Version.AutoSize = true;
            Control_About_Label_Version.Dock = DockStyle.Fill;
            Control_About_Label_Version.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Control_About_Label_Version.Location = new Point(3, 95);
            Control_About_Label_Version.Margin = new Padding(3);
            Control_About_Label_Version.Name = "Control_About_Label_Version";
            Control_About_Label_Version.Size = new Size(118, 17);
            Control_About_Label_Version.TabIndex = 7;
            Control_About_Label_Version.Text = "Version:";
            Control_About_Label_Version.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Control_About_Label_LastUpdate_Data
            // 
            Control_About_Label_LastUpdate_Data.AutoSize = true;
            Control_About_Label_LastUpdate_Data.Dock = DockStyle.Fill;
            Control_About_Label_LastUpdate_Data.Font = new Font("Segoe UI", 9F);
            Control_About_Label_LastUpdate_Data.Location = new Point(127, 72);
            Control_About_Label_LastUpdate_Data.Margin = new Padding(3);
            Control_About_Label_LastUpdate_Data.Name = "Control_About_Label_LastUpdate_Data";
            Control_About_Label_LastUpdate_Data.Size = new Size(230, 17);
            Control_About_Label_LastUpdate_Data.TabIndex = 6;
            Control_About_Label_LastUpdate_Data.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_About_Label_LastUpdate
            // 
            Control_About_Label_LastUpdate.AutoSize = true;
            Control_About_Label_LastUpdate.Dock = DockStyle.Fill;
            Control_About_Label_LastUpdate.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Control_About_Label_LastUpdate.Location = new Point(3, 72);
            Control_About_Label_LastUpdate.Margin = new Padding(3);
            Control_About_Label_LastUpdate.Name = "Control_About_Label_LastUpdate";
            Control_About_Label_LastUpdate.Size = new Size(118, 17);
            Control_About_Label_LastUpdate.TabIndex = 5;
            Control_About_Label_LastUpdate.Text = "Last Updated On:";
            Control_About_Label_LastUpdate.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Control_About_Label_Copyright_Data
            // 
            Control_About_Label_Copyright_Data.AutoSize = true;
            Control_About_Label_Copyright_Data.Dock = DockStyle.Fill;
            Control_About_Label_Copyright_Data.Font = new Font("Segoe UI", 9F);
            Control_About_Label_Copyright_Data.Location = new Point(127, 49);
            Control_About_Label_Copyright_Data.Margin = new Padding(3);
            Control_About_Label_Copyright_Data.Name = "Control_About_Label_Copyright_Data";
            Control_About_Label_Copyright_Data.Size = new Size(230, 17);
            Control_About_Label_Copyright_Data.TabIndex = 4;
            Control_About_Label_Copyright_Data.Text = "Manitowoc Tool and Manufacturing";
            Control_About_Label_Copyright_Data.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_About_Label_Copyright
            // 
            Control_About_Label_Copyright.AutoSize = true;
            Control_About_Label_Copyright.Dock = DockStyle.Fill;
            Control_About_Label_Copyright.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Control_About_Label_Copyright.Location = new Point(3, 49);
            Control_About_Label_Copyright.Margin = new Padding(3);
            Control_About_Label_Copyright.Name = "Control_About_Label_Copyright";
            Control_About_Label_Copyright.Size = new Size(118, 17);
            Control_About_Label_Copyright.TabIndex = 3;
            Control_About_Label_Copyright.Text = "Copyright Owner:";
            Control_About_Label_Copyright.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Control_About_Label_Author_Data
            // 
            Control_About_Label_Author_Data.AutoSize = true;
            Control_About_Label_Author_Data.Dock = DockStyle.Fill;
            Control_About_Label_Author_Data.Font = new Font("Segoe UI", 9F);
            Control_About_Label_Author_Data.Location = new Point(127, 26);
            Control_About_Label_Author_Data.Margin = new Padding(3);
            Control_About_Label_Author_Data.Name = "Control_About_Label_Author_Data";
            Control_About_Label_Author_Data.Size = new Size(230, 17);
            Control_About_Label_Author_Data.TabIndex = 2;
            Control_About_Label_Author_Data.Text = "John Koll";
            Control_About_Label_Author_Data.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_About_Label_Author
            // 
            Control_About_Label_Author.AutoSize = true;
            Control_About_Label_Author.Dock = DockStyle.Fill;
            Control_About_Label_Author.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Control_About_Label_Author.Location = new Point(3, 26);
            Control_About_Label_Author.Margin = new Padding(3);
            Control_About_Label_Author.Name = "Control_About_Label_Author";
            Control_About_Label_Author.Size = new Size(118, 17);
            Control_About_Label_Author.TabIndex = 1;
            Control_About_Label_Author.Text = "Author:";
            Control_About_Label_Author.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Control_About_Label_Main
            // 
            Control_About_Label_Main.AutoSize = true;
            Control_About_TableLayout_Info.SetColumnSpan(Control_About_Label_Main, 2);
            Control_About_Label_Main.Dock = DockStyle.Fill;
            Control_About_Label_Main.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            Control_About_Label_Main.Location = new Point(3, 3);
            Control_About_Label_Main.Margin = new Padding(3);
            Control_About_Label_Main.Name = "Control_About_Label_Main";
            Control_About_Label_Main.Size = new Size(354, 17);
            Control_About_Label_Main.TabIndex = 0;
            Control_About_Label_Main.Text = "Manitowoc Tool and Manufacturing WIP Application";
            Control_About_Label_Main.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_About_TableLayout_Changelog
            // 
            Control_About_TableLayout_Changelog.AutoSize = true;
            Control_About_TableLayout_Changelog.ColumnCount = 1;
            Control_About_TableLayout_Changelog.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_About_TableLayout_Changelog.Controls.Add(Control_About_Label_ChangeLog, 0, 0);
            Control_About_TableLayout_Changelog.Controls.Add(Control_About_Label_WebView_ChangeLogView, 0, 1);
            Control_About_TableLayout_Changelog.Dock = DockStyle.Fill;
            Control_About_TableLayout_Changelog.Location = new Point(372, 3);
            Control_About_TableLayout_Changelog.Name = "Control_About_TableLayout_Changelog";
            Control_About_TableLayout_Changelog.RowCount = 2;
            Control_About_TableLayout_Changelog.RowStyles.Add(new RowStyle());
            Control_About_TableLayout_Changelog.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_About_TableLayout_Changelog.Size = new Size(364, 389);
            Control_About_TableLayout_Changelog.TabIndex = 1;
            // 
            // Control_About_Label_ChangeLog
            // 
            Control_About_Label_ChangeLog.AutoSize = true;
            Control_About_TableLayout_Changelog.SetColumnSpan(Control_About_Label_ChangeLog, 2);
            Control_About_Label_ChangeLog.Dock = DockStyle.Fill;
            Control_About_Label_ChangeLog.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point, 0);
            Control_About_Label_ChangeLog.Location = new Point(3, 3);
            Control_About_Label_ChangeLog.Margin = new Padding(3);
            Control_About_Label_ChangeLog.Name = "Control_About_Label_ChangeLog";
            Control_About_Label_ChangeLog.Size = new Size(358, 17);
            Control_About_Label_ChangeLog.TabIndex = 1;
            Control_About_Label_ChangeLog.Text = "Latest Changes";
            Control_About_Label_ChangeLog.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Control_About_Label_WebView_ChangeLogView
            // 
            Control_About_Label_WebView_ChangeLogView.AllowExternalDrop = false;
            Control_About_Label_WebView_ChangeLogView.CreationProperties = null;
            Control_About_Label_WebView_ChangeLogView.DefaultBackgroundColor = Color.White;
            Control_About_Label_WebView_ChangeLogView.Dock = DockStyle.Fill;
            Control_About_Label_WebView_ChangeLogView.Location = new Point(3, 26);
            Control_About_Label_WebView_ChangeLogView.Name = "Control_About_Label_WebView_ChangeLogView";
            Control_About_Label_WebView_ChangeLogView.Size = new Size(358, 360);
            Control_About_Label_WebView_ChangeLogView.TabIndex = 2;
            Control_About_Label_WebView_ChangeLogView.ZoomFactor = 1D;
            // 
            // Control_About
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            Controls.Add(Control_About_GroupBox_Main);
            Name = "Control_About";
            Size = new Size(751, 504);
            Control_About_GroupBox_Main.ResumeLayout(false);
            Control_About_GroupBox_Main.PerformLayout();
            Control_About_TableLayout_Main.ResumeLayout(false);
            Control_About_TableLayout_Main.PerformLayout();
            Control_About_TableLayout_Top.ResumeLayout(false);
            Control_About_TableLayout_Top.PerformLayout();
            Control_About_TableLayout_Info.ResumeLayout(false);
            Control_About_TableLayout_Info.PerformLayout();
            Control_About_TableLayout_Changelog.ResumeLayout(false);
            Control_About_TableLayout_Changelog.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Control_About_Label_WebView_ChangeLogView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox Control_About_GroupBox_Main;
        private TableLayoutPanel Control_About_TableLayout_Main;
        private TableLayoutPanel Control_About_TableLayout_Bottom;
        private TableLayoutPanel Control_About_TableLayout_Top;
        private TableLayoutPanel Control_About_TableLayout_Info;
        private Label Control_About_Label_Version_Data;
        private Label Control_About_Label_Version;
        private Label Control_About_Label_LastUpdate_Data;
        private Label Control_About_Label_LastUpdate;
        private Label Control_About_Label_Copyright_Data;
        private Label Control_About_Label_Copyright;
        private Label Control_About_Label_Author_Data;
        private Label Control_About_Label_Author;
        private Label Control_About_Label_Main;
        private TableLayoutPanel Control_About_TableLayout_Changelog;
        private Label Control_About_Label_ChangeLog;
        private Microsoft.Web.WebView2.WinForms.WebView2 Control_About_Label_WebView_ChangeLogView;
    }
}
