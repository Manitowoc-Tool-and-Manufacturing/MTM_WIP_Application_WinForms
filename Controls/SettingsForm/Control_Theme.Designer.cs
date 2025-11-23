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

        private CheckBox Control_Themes_CheckBox_EnableAnimations;
        private CheckBox Control_Themes_CheckBox_AutoExpandPanels;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Control_Themes_GroupBox_Main = new GroupBox();
            Control_Themes_TableLayout_Main = new TableLayoutPanel();
            Control_Themes_CheckBox_EnableTheming = new CheckBox();
            Control_Themes_CheckBox_EnableAnimations = new CheckBox();
            Control_Themes_CheckBox_AutoExpandPanels = new CheckBox();
            Control_Themes_Button_Preview = new Button();
            Control_Themes_ComboBox_Theme = new ComboBox();
            Control_Themes_Button_Save = new Button();
            Control_Themes_GroupBox_Main.SuspendLayout();
            Control_Themes_TableLayout_Main.SuspendLayout();
            SuspendLayout();
            //
            // Control_Themes_GroupBox_Main
            //
            Control_Themes_GroupBox_Main.AutoSize = true;
            Control_Themes_GroupBox_Main.Controls.Add(Control_Themes_TableLayout_Main);
            Control_Themes_GroupBox_Main.Dock = DockStyle.Fill;
            Control_Themes_GroupBox_Main.Location = new Point(0, 0);
            Control_Themes_GroupBox_Main.Name = "Control_Themes_GroupBox_Main";
            Control_Themes_GroupBox_Main.Size = new Size(277, 135);
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
            Control_Themes_TableLayout_Main.Controls.Add(Control_Themes_Button_Preview, 1, 1);
            Control_Themes_TableLayout_Main.Controls.Add(Control_Themes_ComboBox_Theme, 0, 1);
            Control_Themes_TableLayout_Main.Controls.Add(Control_Themes_CheckBox_EnableAnimations, 0, 2);
            Control_Themes_TableLayout_Main.Controls.Add(Control_Themes_CheckBox_AutoExpandPanels, 0, 3);            Control_Themes_TableLayout_Main.Controls.Add(Control_Themes_CheckBox_AutoExpandPanels, 0, 3);            Control_Themes_TableLayout_Main.Controls.Add(Control_Themes_Button_Save, 1, 5);
            Control_Themes_TableLayout_Main.Dock = DockStyle.Fill;
            Control_Themes_TableLayout_Main.Location = new Point(3, 19);
            Control_Themes_TableLayout_Main.Name = "Control_Themes_TableLayout_Main";
            Control_Themes_TableLayout_Main.RowCount = 6;
            Control_Themes_TableLayout_Main.RowStyles.Add(new RowStyle());
            Control_Themes_TableLayout_Main.RowStyles.Add(new RowStyle());
            Control_Themes_TableLayout_Main.RowStyles.Add(new RowStyle());
            Control_Themes_TableLayout_Main.RowStyles.Add(new RowStyle());
            Control_Themes_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_Themes_TableLayout_Main.RowStyles.Add(new RowStyle());
            Control_Themes_TableLayout_Main.Size = new Size(271, 145);
            Control_Themes_TableLayout_Main.TabIndex = 3;
            //
            // Control_Themes_CheckBox_EnableTheming
            //
            Control_Themes_CheckBox_EnableTheming.AutoSize = true;
            Control_Themes_TableLayout_Main.SetColumnSpan(Control_Themes_CheckBox_EnableTheming, 2);
            Control_Themes_CheckBox_EnableTheming.Checked = true;
            Control_Themes_CheckBox_EnableTheming.CheckState = CheckState.Checked;
            Control_Themes_CheckBox_EnableTheming.Dock = DockStyle.Fill;
            Control_Themes_CheckBox_EnableTheming.Location = new Point(3, 3);
            Control_Themes_CheckBox_EnableTheming.Name = "Control_Themes_CheckBox_EnableTheming";
            Control_Themes_CheckBox_EnableTheming.Size = new Size(265, 19);
            Control_Themes_CheckBox_EnableTheming.TabIndex = 0;
            Control_Themes_CheckBox_EnableTheming.Text = "Enable Theme System (colors only, DPI scaling always on)";
            Control_Themes_CheckBox_EnableTheming.UseVisualStyleBackColor = true;
            Control_Themes_CheckBox_EnableTheming.Visible = false;
            //
            // Control_Themes_CheckBox_EnableAnimations
            //
            Control_Themes_CheckBox_EnableAnimations.AutoSize = true;
            Control_Themes_TableLayout_Main.SetColumnSpan(Control_Themes_CheckBox_EnableAnimations, 2);
            Control_Themes_CheckBox_EnableAnimations.Checked = true;
            Control_Themes_CheckBox_EnableAnimations.CheckState = CheckState.Checked;
            Control_Themes_CheckBox_EnableAnimations.Dock = DockStyle.Fill;
            Control_Themes_CheckBox_EnableAnimations.Location = new Point(3, 57);
            Control_Themes_CheckBox_EnableAnimations.Name = "Control_Themes_CheckBox_EnableAnimations";
            Control_Themes_CheckBox_EnableAnimations.Size = new Size(265, 19);
            Control_Themes_CheckBox_EnableAnimations.TabIndex = 4;
            Control_Themes_CheckBox_EnableAnimations.Text = "Enable UI Animations";
            Control_Themes_CheckBox_EnableAnimations.UseVisualStyleBackColor = true;
            //
            // Control_Themes_CheckBox_AutoExpandPanels
            //
            Control_Themes_CheckBox_AutoExpandPanels.AutoSize = true;
            Control_Themes_TableLayout_Main.SetColumnSpan(Control_Themes_CheckBox_AutoExpandPanels, 2);
            Control_Themes_CheckBox_AutoExpandPanels.Checked = true;
            Control_Themes_CheckBox_AutoExpandPanels.CheckState = CheckState.Checked;
            Control_Themes_CheckBox_AutoExpandPanels.Dock = DockStyle.Fill;
            Control_Themes_CheckBox_AutoExpandPanels.Location = new Point(3, 82);
            Control_Themes_CheckBox_AutoExpandPanels.Name = "Control_Themes_CheckBox_AutoExpandPanels";
            Control_Themes_CheckBox_AutoExpandPanels.Size = new Size(265, 19);
            Control_Themes_CheckBox_AutoExpandPanels.TabIndex = 5;
            Control_Themes_CheckBox_AutoExpandPanels.Text = "Auto-Expand Panels on Reset/Search";
            Control_Themes_CheckBox_AutoExpandPanels.UseVisualStyleBackColor = true;
            //
            // Control_Themes_Button_Preview
            //
            Control_Themes_Button_Preview.Location = new Point(193, 28);
            Control_Themes_Button_Preview.Name = "Control_Themes_Button_Preview";
            Control_Themes_Button_Preview.Size = new Size(75, 23);
            Control_Themes_Button_Preview.TabIndex = 2;
            Control_Themes_Button_Preview.Text = "Preview";
            Control_Themes_Button_Preview.UseVisualStyleBackColor = true;
            //
            // Control_Themes_ComboBox_Theme
            //
            Control_Themes_ComboBox_Theme.DropDownStyle = ComboBoxStyle.DropDownList;
            Control_Themes_ComboBox_Theme.FormattingEnabled = true;
            Control_Themes_ComboBox_Theme.Location = new Point(3, 28);
            Control_Themes_ComboBox_Theme.Name = "Control_Themes_ComboBox_Theme";
            Control_Themes_ComboBox_Theme.Size = new Size(184, 23);
            Control_Themes_ComboBox_Theme.TabIndex = 1;
            //
            // Control_Themes_Button_Save
            //
            Control_Themes_Button_Save.Location = new Point(193, 94);
            Control_Themes_Button_Save.Name = "Control_Themes_Button_Save";
            Control_Themes_Button_Save.Size = new Size(75, 23);
            Control_Themes_Button_Save.TabIndex = 3;
            Control_Themes_Button_Save.Text = "Save";
            Control_Themes_Button_Save.UseVisualStyleBackColor = true;
            //
            // Control_Theme
            //
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(Control_Themes_GroupBox_Main);
            Name = "Control_Theme";
            Size = new Size(277, 80);
            Control_Themes_GroupBox_Main.ResumeLayout(false);
            Control_Themes_GroupBox_Main.PerformLayout();
            Control_Themes_TableLayout_Main.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox Control_Themes_GroupBox_Main;
        private TableLayoutPanel Control_Themes_TableLayout_Main;
        private CheckBox Control_Themes_CheckBox_EnableTheming;
        private Button Control_Themes_Button_Preview;
        private ComboBox Control_Themes_ComboBox_Theme;
        private Button Control_Themes_Button_Save;
    }
}
