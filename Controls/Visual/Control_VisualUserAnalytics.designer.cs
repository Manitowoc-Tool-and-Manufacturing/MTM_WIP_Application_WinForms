// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MTM_WIP_Application_Winforms.Controls.Visual
{
    partial class Control_VisualUserAnalytics
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private TableLayoutPanel Control_VisualUserAnalytics_TableLayout_Main;
        private Panel Control_VisualUserAnalytics_Panel_Card;
        private Label Control_VisualUserAnalytics_Label_Title;
        private Label Control_VisualUserAnalytics_Label_Subtitle;
        private Label Control_VisualUserAnalytics_Label_Details;
        private Label Control_VisualUserAnalytics_Label_Access;
        private Button Control_VisualUserAnalytics_Button_Open;

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

        /// <summary>
        /// Required method for Designer support.
        /// </summary>
        private void InitializeComponent()
        {
            Control_VisualUserAnalytics_TableLayout_Main = new TableLayoutPanel();
            Control_VisualUserAnalytics_Panel_Card = new Panel();
            Control_VisualUserAnalytics_Button_Open = new Button();
            Control_VisualUserAnalytics_Label_Access = new Label();
            Control_VisualUserAnalytics_Label_Details = new Label();
            Control_VisualUserAnalytics_Label_Subtitle = new Label();
            Control_VisualUserAnalytics_Label_Title = new Label();
            Control_VisualUserAnalytics_TableLayout_Main.SuspendLayout();
            Control_VisualUserAnalytics_Panel_Card.SuspendLayout();
            SuspendLayout();
            // 
            // Control_VisualUserAnalytics_TableLayout_Main
            // 
            Control_VisualUserAnalytics_TableLayout_Main.ColumnCount = 1;
            Control_VisualUserAnalytics_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_VisualUserAnalytics_TableLayout_Main.Controls.Add(Control_VisualUserAnalytics_Panel_Card, 0, 0);
            Control_VisualUserAnalytics_TableLayout_Main.Dock = DockStyle.Fill;
            Control_VisualUserAnalytics_TableLayout_Main.Location = new Point(0, 0);
            Control_VisualUserAnalytics_TableLayout_Main.Name = "Control_VisualUserAnalytics_TableLayout_Main";
            Control_VisualUserAnalytics_TableLayout_Main.Padding = new Padding(28);
            Control_VisualUserAnalytics_TableLayout_Main.RowCount = 1;
            Control_VisualUserAnalytics_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_VisualUserAnalytics_TableLayout_Main.Size = new Size(900, 560);
            Control_VisualUserAnalytics_TableLayout_Main.TabIndex = 0;
            // 
            // Control_VisualUserAnalytics_Panel_Card
            // 
            Control_VisualUserAnalytics_Panel_Card.BorderStyle = BorderStyle.FixedSingle;
            Control_VisualUserAnalytics_Panel_Card.Controls.Add(Control_VisualUserAnalytics_Button_Open);
            Control_VisualUserAnalytics_Panel_Card.Controls.Add(Control_VisualUserAnalytics_Label_Access);
            Control_VisualUserAnalytics_Panel_Card.Controls.Add(Control_VisualUserAnalytics_Label_Details);
            Control_VisualUserAnalytics_Panel_Card.Controls.Add(Control_VisualUserAnalytics_Label_Subtitle);
            Control_VisualUserAnalytics_Panel_Card.Controls.Add(Control_VisualUserAnalytics_Label_Title);
            Control_VisualUserAnalytics_Panel_Card.Dock = DockStyle.Fill;
            Control_VisualUserAnalytics_Panel_Card.Location = new Point(31, 31);
            Control_VisualUserAnalytics_Panel_Card.Name = "Control_VisualUserAnalytics_Panel_Card";
            Control_VisualUserAnalytics_Panel_Card.Padding = new Padding(36);
            Control_VisualUserAnalytics_Panel_Card.Size = new Size(838, 498);
            Control_VisualUserAnalytics_Panel_Card.TabIndex = 0;
            // 
            // Control_VisualUserAnalytics_Button_Open
            // 
            Control_VisualUserAnalytics_Button_Open.Location = new Point(38, 382);
            Control_VisualUserAnalytics_Button_Open.Name = "Control_VisualUserAnalytics_Button_Open";
            Control_VisualUserAnalytics_Button_Open.Size = new Size(224, 44);
            Control_VisualUserAnalytics_Button_Open.TabIndex = 4;
            Control_VisualUserAnalytics_Button_Open.Text = "Open Analytics Workspace";
            Control_VisualUserAnalytics_Button_Open.UseVisualStyleBackColor = true;
            Control_VisualUserAnalytics_Button_Open.Click += Control_VisualUserAnalytics_Button_Open_Click;
            // 
            // Control_VisualUserAnalytics_Label_Access
            // 
            Control_VisualUserAnalytics_Label_Access.Location = new Point(38, 290);
            Control_VisualUserAnalytics_Label_Access.MaximumSize = new Size(720, 0);
            Control_VisualUserAnalytics_Label_Access.Name = "Control_VisualUserAnalytics_Label_Access";
            Control_VisualUserAnalytics_Label_Access.Size = new Size(720, 54);
            Control_VisualUserAnalytics_Label_Access.TabIndex = 3;
            Control_VisualUserAnalytics_Label_Access.Text = "Access: Team summary, user drill-down, print, and export are enabled for admin and developer roles.";
            // 
            // Control_VisualUserAnalytics_Label_Details
            // 
            Control_VisualUserAnalytics_Label_Details.Location = new Point(38, 162);
            Control_VisualUserAnalytics_Label_Details.MaximumSize = new Size(720, 0);
            Control_VisualUserAnalytics_Label_Details.Name = "Control_VisualUserAnalytics_Label_Details";
            Control_VisualUserAnalytics_Label_Details.Size = new Size(720, 102);
            Control_VisualUserAnalytics_Label_Details.TabIndex = 2;
            Control_VisualUserAnalytics_Label_Details.Text = "The new analytics page loads a single transaction feed, treats advanced inventory and advanced remove the same as single actions, and lets you switch between team context and individual detail without a separate score model.";
            // 
            // Control_VisualUserAnalytics_Label_Subtitle
            // 
            Control_VisualUserAnalytics_Label_Subtitle.Location = new Point(38, 84);
            Control_VisualUserAnalytics_Label_Subtitle.MaximumSize = new Size(720, 0);
            Control_VisualUserAnalytics_Label_Subtitle.Name = "Control_VisualUserAnalytics_Label_Subtitle";
            Control_VisualUserAnalytics_Label_Subtitle.Size = new Size(720, 54);
            Control_VisualUserAnalytics_Label_Subtitle.TabIndex = 1;
            Control_VisualUserAnalytics_Label_Subtitle.Text = "Open the unified analytics workspace to review team activity, drill into individuals, and print or export summary views.";
            // 
            // Control_VisualUserAnalytics_Label_Title
            // 
            Control_VisualUserAnalytics_Label_Title.AutoSize = true;
            Control_VisualUserAnalytics_Label_Title.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Control_VisualUserAnalytics_Label_Title.Location = new Point(35, 34);
            Control_VisualUserAnalytics_Label_Title.Name = "Control_VisualUserAnalytics_Label_Title";
            Control_VisualUserAnalytics_Label_Title.Size = new Size(251, 37);
            Control_VisualUserAnalytics_Label_Title.TabIndex = 0;
            Control_VisualUserAnalytics_Label_Title.Text = "User & Team Analytics";
            // 
            // Control_VisualUserAnalytics
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(Control_VisualUserAnalytics_TableLayout_Main);
            Name = "Control_VisualUserAnalytics";
            Size = new Size(900, 560);
            Control_VisualUserAnalytics_TableLayout_Main.ResumeLayout(false);
            Control_VisualUserAnalytics_Panel_Card.ResumeLayout(false);
            Control_VisualUserAnalytics_Panel_Card.PerformLayout();
            ResumeLayout(false);
        }
    }
}
