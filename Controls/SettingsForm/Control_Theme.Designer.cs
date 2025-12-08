// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    partial class Control_Theme
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
            Control_Themes_GroupBox_Main = new GroupBox();
            Control_Themes_TableLayout_Main = new TableLayoutPanel();
            Control_Themes_ComboBox_Theme = new ComboBox();
            Control_Themes_Button_Preview = new Button();
            Control_Themes_CheckBox_ShowTotalSummaryPanel = new CheckBox();
            Control_Themes_Button_Save = new Button();
            groupBox1 = new GroupBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            Control_Themes_CheckBox_AutoExpandPanels = new CheckBox();
            Control_Themes_CheckBox_EnableAnimations = new CheckBox();
            Control_Themes_CheckBox_EnableTheming = new CheckBox();
            tableLayoutPanel2 = new TableLayoutPanel();
            Control_Themes_Button_Home = new Button();
            SettingsForm_Button_Help_Theme = new Button();
            tableLayoutPanel3 = new TableLayoutPanel();
            Control_Themes_GroupBox_Main.SuspendLayout();
            Control_Themes_TableLayout_Main.SuspendLayout();
            groupBox1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            SuspendLayout();
            // 
            // Control_Themes_GroupBox_Main
            // 
            Control_Themes_GroupBox_Main.AutoSize = true;
            Control_Themes_GroupBox_Main.Controls.Add(Control_Themes_TableLayout_Main);
            Control_Themes_GroupBox_Main.Dock = DockStyle.Fill;
            Control_Themes_GroupBox_Main.Location = new Point(3, 3);
            Control_Themes_GroupBox_Main.Name = "Control_Themes_GroupBox_Main";
            Control_Themes_GroupBox_Main.Size = new Size(624, 51);
            Control_Themes_GroupBox_Main.TabIndex = 3;
            Control_Themes_GroupBox_Main.TabStop = false;
            Control_Themes_GroupBox_Main.Text = "Select A Theme";
            // 
            // Control_Themes_TableLayout_Main
            // 
            Control_Themes_TableLayout_Main.AutoSize = true;
            Control_Themes_TableLayout_Main.ColumnCount = 2;
            Control_Themes_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_Themes_TableLayout_Main.ColumnStyles.Add(new ColumnStyle());
            Control_Themes_TableLayout_Main.Controls.Add(Control_Themes_ComboBox_Theme, 0, 0);
            Control_Themes_TableLayout_Main.Controls.Add(Control_Themes_Button_Preview, 1, 0);
            Control_Themes_TableLayout_Main.Dock = DockStyle.Fill;
            Control_Themes_TableLayout_Main.Location = new Point(3, 19);
            Control_Themes_TableLayout_Main.Name = "Control_Themes_TableLayout_Main";
            Control_Themes_TableLayout_Main.RowCount = 1;
            Control_Themes_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_Themes_TableLayout_Main.Size = new Size(618, 29);
            Control_Themes_TableLayout_Main.TabIndex = 3;
            // 
            // Control_Themes_ComboBox_Theme
            // 
            Control_Themes_ComboBox_Theme.Dock = DockStyle.Fill;
            Control_Themes_ComboBox_Theme.DropDownStyle = ComboBoxStyle.DropDownList;
            Control_Themes_ComboBox_Theme.FormattingEnabled = true;
            Control_Themes_ComboBox_Theme.Location = new Point(3, 3);
            Control_Themes_ComboBox_Theme.Name = "Control_Themes_ComboBox_Theme";
            Control_Themes_ComboBox_Theme.Size = new Size(506, 23);
            Control_Themes_ComboBox_Theme.TabIndex = 1;
            // 
            // Control_Themes_Button_Preview
            // 
            Control_Themes_Button_Preview.AutoSize = true;
            Control_Themes_Button_Preview.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_Themes_Button_Preview.BackColor = Color.FromArgb(200, 200, 200);
            Control_Themes_Button_Preview.FlatStyle = FlatStyle.Flat;
            Control_Themes_Button_Preview.Font = new Font("Segoe UI Semibold", 8.75F);
            Control_Themes_Button_Preview.ForeColor = Color.Black;
            Control_Themes_Button_Preview.Location = new Point(515, 3);
            Control_Themes_Button_Preview.MaximumSize = new Size(0, 23);
            Control_Themes_Button_Preview.MinimumSize = new Size(100, 23);
            Control_Themes_Button_Preview.Name = "Control_Themes_Button_Preview";
            Control_Themes_Button_Preview.Size = new Size(100, 23);
            Control_Themes_Button_Preview.TabIndex = 2;
            Control_Themes_Button_Preview.Text = "Preview";
            Control_Themes_Button_Preview.UseVisualStyleBackColor = true;
            // 
            // Control_Themes_CheckBox_ShowTotalSummaryPanel
            // 
            Control_Themes_CheckBox_ShowTotalSummaryPanel.AutoSize = true;
            Control_Themes_CheckBox_ShowTotalSummaryPanel.Location = new Point(3, 57);
            Control_Themes_CheckBox_ShowTotalSummaryPanel.Name = "Control_Themes_CheckBox_ShowTotalSummaryPanel";
            Control_Themes_CheckBox_ShowTotalSummaryPanel.Size = new Size(180, 24);
            Control_Themes_CheckBox_ShowTotalSummaryPanel.TabIndex = 10;
            Control_Themes_CheckBox_ShowTotalSummaryPanel.Text = "Show Total Summary Panel";
            Control_Themes_CheckBox_ShowTotalSummaryPanel.UseVisualStyleBackColor = true;
            // 
            // Control_Themes_Button_Save
            // 
            Control_Themes_Button_Save.AutoSize = true;
            Control_Themes_Button_Save.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_Themes_Button_Save.BackColor = Color.FromArgb(200, 200, 200);
            Control_Themes_Button_Save.FlatStyle = FlatStyle.Flat;
            Control_Themes_Button_Save.Font = new Font("Segoe UI Semibold", 10F);
            Control_Themes_Button_Save.ForeColor = Color.Black;
            Control_Themes_Button_Save.Location = new Point(3, 3);
            Control_Themes_Button_Save.MaximumSize = new Size(100, 32);
            Control_Themes_Button_Save.MinimumSize = new Size(100, 32);
            Control_Themes_Button_Save.Name = "Control_Themes_Button_Save";
            Control_Themes_Button_Save.Size = new Size(100, 32);
            Control_Themes_Button_Save.TabIndex = 3;
            Control_Themes_Button_Save.Text = "Save";
            Control_Themes_Button_Save.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.AutoSize = true;
            groupBox1.Controls.Add(tableLayoutPanel1);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(3, 60);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(624, 97);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "UI Settings";
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(Control_Themes_CheckBox_AutoExpandPanels, 0, 2);
            tableLayoutPanel1.Controls.Add(Control_Themes_CheckBox_EnableAnimations, 0, 1);
            tableLayoutPanel1.Controls.Add(Control_Themes_CheckBox_EnableTheming, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 19);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(618, 75);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // Control_Themes_CheckBox_AutoExpandPanels
            // 
            Control_Themes_CheckBox_AutoExpandPanels.AutoSize = true;
            Control_Themes_CheckBox_AutoExpandPanels.Checked = true;
            Control_Themes_CheckBox_AutoExpandPanels.CheckState = CheckState.Checked;
            Control_Themes_CheckBox_AutoExpandPanels.Dock = DockStyle.Fill;
            Control_Themes_CheckBox_AutoExpandPanels.Location = new Point(3, 53);
            Control_Themes_CheckBox_AutoExpandPanels.Name = "Control_Themes_CheckBox_AutoExpandPanels";
            Control_Themes_CheckBox_AutoExpandPanels.Size = new Size(612, 19);
            Control_Themes_CheckBox_AutoExpandPanels.TabIndex = 5;
            Control_Themes_CheckBox_AutoExpandPanels.Text = "Auto-Expand Panels on Reset/Search";
            Control_Themes_CheckBox_AutoExpandPanels.UseVisualStyleBackColor = true;
            // 
            // Control_Themes_CheckBox_EnableAnimations
            // 
            Control_Themes_CheckBox_EnableAnimations.AutoSize = true;
            Control_Themes_CheckBox_EnableAnimations.Checked = true;
            Control_Themes_CheckBox_EnableAnimations.CheckState = CheckState.Checked;
            Control_Themes_CheckBox_EnableAnimations.Dock = DockStyle.Fill;
            Control_Themes_CheckBox_EnableAnimations.Location = new Point(3, 28);
            Control_Themes_CheckBox_EnableAnimations.Name = "Control_Themes_CheckBox_EnableAnimations";
            Control_Themes_CheckBox_EnableAnimations.Size = new Size(612, 19);
            Control_Themes_CheckBox_EnableAnimations.TabIndex = 4;
            Control_Themes_CheckBox_EnableAnimations.Text = "Enable UI Animations";
            Control_Themes_CheckBox_EnableAnimations.UseVisualStyleBackColor = true;
            // 
            // Control_Themes_CheckBox_EnableTheming
            // 
            Control_Themes_CheckBox_EnableTheming.AutoSize = true;
            Control_Themes_CheckBox_EnableTheming.Checked = true;
            Control_Themes_CheckBox_EnableTheming.CheckState = CheckState.Checked;
            Control_Themes_CheckBox_EnableTheming.Dock = DockStyle.Fill;
            Control_Themes_CheckBox_EnableTheming.Location = new Point(3, 3);
            Control_Themes_CheckBox_EnableTheming.Name = "Control_Themes_CheckBox_EnableTheming";
            Control_Themes_CheckBox_EnableTheming.Size = new Size(612, 19);
            Control_Themes_CheckBox_EnableTheming.TabIndex = 0;
            Control_Themes_CheckBox_EnableTheming.Text = "Enable Theme System (colors only, DPI scaling always on)";
            Control_Themes_CheckBox_EnableTheming.UseVisualStyleBackColor = true;
            Control_Themes_CheckBox_EnableTheming.Visible = false;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.ColumnCount = 4;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(Control_Themes_Button_Save, 0, 0);
            tableLayoutPanel2.Controls.Add(SettingsForm_Button_Help_Theme, 3, 0);
            tableLayoutPanel2.Controls.Add(Control_Themes_Button_Home, 2, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 171);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(624, 38);
            tableLayoutPanel2.TabIndex = 8;
            // 
            // Control_Themes_Button_Home
            // 
            Control_Themes_Button_Home.AutoSize = true;
            Control_Themes_Button_Home.Location = new Point(444, 3);
            Control_Themes_Button_Home.MaximumSize = new Size(0, 32);
            Control_Themes_Button_Home.MinimumSize = new Size(0, 32);
            Control_Themes_Button_Home.Name = "Control_Themes_Button_Home";
            Control_Themes_Button_Home.Size = new Size(139, 32);
            Control_Themes_Button_Home.TabIndex = 4;
            Control_Themes_Button_Home.Text = "🏠 Back to Home";
            // 
            // SettingsForm_Button_Help_Theme
            // 
            SettingsForm_Button_Help_Theme.AutoSize = true;
            SettingsForm_Button_Help_Theme.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            SettingsForm_Button_Help_Theme.Location = new Point(589, 3);
            SettingsForm_Button_Help_Theme.MaximumSize = new Size(32, 32);
            SettingsForm_Button_Help_Theme.MinimumSize = new Size(32, 32);
            SettingsForm_Button_Help_Theme.Name = "SettingsForm_Button_Help_Theme";
            SettingsForm_Button_Help_Theme.Size = new Size(32, 32);
            SettingsForm_Button_Help_Theme.TabIndex = 13;
            SettingsForm_Button_Help_Theme.Text = "?";
            SettingsForm_Button_Help_Theme.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.AutoSize = true;
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(tableLayoutPanel2, 0, 3);
            tableLayoutPanel3.Controls.Add(Control_Themes_GroupBox_Main, 0, 0);
            tableLayoutPanel3.Controls.Add(groupBox1, 0, 1);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(0, 0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 4;
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.Size = new Size(630, 212);
            tableLayoutPanel3.TabIndex = 9;
            // 
            // Control_Theme
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            Controls.Add(tableLayoutPanel3);
            Name = "Control_Theme";
            Size = new Size(630, 212);
            Control_Themes_GroupBox_Main.ResumeLayout(false);
            Control_Themes_GroupBox_Main.PerformLayout();
            Control_Themes_TableLayout_Main.ResumeLayout(false);
            Control_Themes_TableLayout_Main.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox Control_Themes_GroupBox_Main;
        private TableLayoutPanel Control_Themes_TableLayout_Main;
        private Button Control_Themes_Button_Preview;
        private ComboBox Control_Themes_ComboBox_Theme;
        private Button Control_Themes_Button_Save;
        private GroupBox groupBox1;
        private TableLayoutPanel tableLayoutPanel1;
        private CheckBox Control_Themes_CheckBox_AutoExpandPanels;
        private CheckBox Control_Themes_CheckBox_EnableAnimations;
        private CheckBox Control_Themes_CheckBox_EnableTheming;
        private TableLayoutPanel tableLayoutPanel2;
        private Button SettingsForm_Button_Help_Theme;
        private Button Control_Themes_Button_Home;
        private TableLayoutPanel tableLayoutPanel3;
    }
}
