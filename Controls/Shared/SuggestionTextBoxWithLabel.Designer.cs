using System.Drawing;
using System.Windows.Forms;

namespace MTM_WIP_Application_Winforms.Controls.Shared
{
    partial class SuggestionTextBoxWithLabel
    {
        #region Fields

        private System.ComponentModel.IContainer components = null;

        #endregion

        private TableLayoutPanel SuggestionTextBoxWithLabel_TableLayout_Main;
        private Label SuggestionTextBoxWithLabel_Label_Main;
        private SuggestionTextBox SuggestionTextBoxWithLabel_TextBox_Main;
        private Button SuggestionTextBoxWithLabel_Button_F4;

        #region Methods

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

        #endregion

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            SuggestionTextBoxWithLabel_TableLayout_Main = new TableLayoutPanel();
            SuggestionTextBoxWithLabel_Label_Main = new Label();
            SuggestionTextBoxWithLabel_TextBox_Main = new SuggestionTextBox();
            SuggestionTextBoxWithLabel_Button_F4 = new Button();
            SuggestionTextBoxWithLabel_TableLayout_Main.SuspendLayout();
            SuspendLayout();
            // 
            // SuggestionTextBoxWithLabel_TableLayout_Main
            // 
            SuggestionTextBoxWithLabel_TableLayout_Main.AutoSize = true;
            SuggestionTextBoxWithLabel_TableLayout_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            SuggestionTextBoxWithLabel_TableLayout_Main.ColumnCount = 3;
            SuggestionTextBoxWithLabel_TableLayout_Main.ColumnStyles.Add(new ColumnStyle());
            SuggestionTextBoxWithLabel_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            SuggestionTextBoxWithLabel_TableLayout_Main.ColumnStyles.Add(new ColumnStyle());
            SuggestionTextBoxWithLabel_TableLayout_Main.Controls.Add(SuggestionTextBoxWithLabel_Label_Main, 0, 0);
            SuggestionTextBoxWithLabel_TableLayout_Main.Controls.Add(SuggestionTextBoxWithLabel_TextBox_Main, 1, 0);
            SuggestionTextBoxWithLabel_TableLayout_Main.Controls.Add(SuggestionTextBoxWithLabel_Button_F4, 2, 0);
            SuggestionTextBoxWithLabel_TableLayout_Main.Dock = DockStyle.Fill;
            SuggestionTextBoxWithLabel_TableLayout_Main.Location = new Point(0, 0);
            SuggestionTextBoxWithLabel_TableLayout_Main.MaximumSize = new Size(0, 23);
            SuggestionTextBoxWithLabel_TableLayout_Main.MinimumSize = new Size(0, 23);
            SuggestionTextBoxWithLabel_TableLayout_Main.Name = "SuggestionTextBoxWithLabel_TableLayout_Main";
            SuggestionTextBoxWithLabel_TableLayout_Main.RowCount = 1;
            SuggestionTextBoxWithLabel_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            SuggestionTextBoxWithLabel_TableLayout_Main.Size = new Size(320, 23);
            SuggestionTextBoxWithLabel_TableLayout_Main.TabIndex = 0;
            // 
            // SuggestionTextBoxWithLabel_Label_Main
            // 
            SuggestionTextBoxWithLabel_Label_Main.AutoSize = true;
            SuggestionTextBoxWithLabel_Label_Main.Dock = DockStyle.Fill;
            SuggestionTextBoxWithLabel_Label_Main.Font = new Font("Segoe UI Emoji", 9F);
            SuggestionTextBoxWithLabel_Label_Main.Location = new Point(0, 0);
            SuggestionTextBoxWithLabel_Label_Main.Margin = new Padding(0);
            SuggestionTextBoxWithLabel_Label_Main.MaximumSize = new Size(130, 23);
            SuggestionTextBoxWithLabel_Label_Main.MinimumSize = new Size(130, 23);
            SuggestionTextBoxWithLabel_Label_Main.Name = "SuggestionTextBoxWithLabel_Label_Main";
            SuggestionTextBoxWithLabel_Label_Main.Size = new Size(130, 23);
            SuggestionTextBoxWithLabel_Label_Main.TabIndex = 0;
            SuggestionTextBoxWithLabel_Label_Main.TextAlign = ContentAlignment.MiddleRight;
            // 
            // SuggestionTextBoxWithLabel_TextBox_Main
            // 
            SuggestionTextBoxWithLabel_TextBox_Main.AutoSize = true;
            SuggestionTextBoxWithLabel_TextBox_Main.Dock = DockStyle.Fill;
            SuggestionTextBoxWithLabel_TextBox_Main.Location = new Point(130, 0);
            SuggestionTextBoxWithLabel_TextBox_Main.Margin = new Padding(0);
            SuggestionTextBoxWithLabel_TextBox_Main.MaximumSize = new Size(0, 23);
            SuggestionTextBoxWithLabel_TextBox_Main.MinimumSize = new Size(0, 23);
            SuggestionTextBoxWithLabel_TextBox_Main.Name = "SuggestionTextBoxWithLabel_TextBox_Main";
            SuggestionTextBoxWithLabel_TextBox_Main.Size = new Size(167, 23);
            SuggestionTextBoxWithLabel_TextBox_Main.TabIndex = 1;
            // 
            // SuggestionTextBoxWithLabel_Button_F4
            // 
            SuggestionTextBoxWithLabel_Button_F4.Dock = DockStyle.Fill;
            SuggestionTextBoxWithLabel_Button_F4.Font = new Font("Segoe UI Emoji", 9F);
            SuggestionTextBoxWithLabel_Button_F4.Location = new Point(297, 0);
            SuggestionTextBoxWithLabel_Button_F4.Margin = new Padding(0);
            SuggestionTextBoxWithLabel_Button_F4.MaximumSize = new Size(23, 23);
            SuggestionTextBoxWithLabel_Button_F4.MinimumSize = new Size(23, 23);
            SuggestionTextBoxWithLabel_Button_F4.Name = "SuggestionTextBoxWithLabel_Button_F4";
            SuggestionTextBoxWithLabel_Button_F4.Size = new Size(23, 23);
            SuggestionTextBoxWithLabel_Button_F4.TabIndex = 2;
            SuggestionTextBoxWithLabel_Button_F4.TabStop = false;
            SuggestionTextBoxWithLabel_Button_F4.Text = "🔎";
            SuggestionTextBoxWithLabel_Button_F4.UseVisualStyleBackColor = true;
            // 
            // SuggestionTextBoxWithLabel
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(SuggestionTextBoxWithLabel_TableLayout_Main);
            MinimumSize = new Size(0, 23);
            Name = "SuggestionTextBoxWithLabel";
            Size = new Size(320, 23);
            SuggestionTextBoxWithLabel_TableLayout_Main.ResumeLayout(false);
            SuggestionTextBoxWithLabel_TableLayout_Main.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
