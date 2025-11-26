using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MTM_WIP_Application_Winforms.Controls.Shared
{
    /// <summary>
    /// Designer partial for <see cref="SuggestionTextBox"/> enabling WinForms designer support.
    /// </summary>
    public partial class SuggestionTextBox
    {
        #region Fields

        private IContainer components = null!;
        private Panel SuggestionTextBox_Panel_Host;
        private TextBox SuggestionTextBox_TextBox;

        #endregion

        #region Methods

        private void InitializeComponent()
        {
            SuggestionTextBox_Panel_Host = new Panel();
            SuggestionTextBox_TextBox = new TextBox();
            SuggestionTextBox_Panel_Host.SuspendLayout();
            SuspendLayout();
            // 
            // SuggestionTextBox_Panel_Host
            // 
            SuggestionTextBox_Panel_Host.AutoSize = true;
            SuggestionTextBox_Panel_Host.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            SuggestionTextBox_Panel_Host.Controls.Add(SuggestionTextBox_TextBox);
            SuggestionTextBox_Panel_Host.Dock = DockStyle.Fill;
            SuggestionTextBox_Panel_Host.Location = new Point(0, 0);
            SuggestionTextBox_Panel_Host.Margin = new Padding(0);
            SuggestionTextBox_Panel_Host.MinimumSize = new Size(0, 27);
            SuggestionTextBox_Panel_Host.Name = "SuggestionTextBox_Panel_Host";
            SuggestionTextBox_Panel_Host.Size = new Size(150, 27);
            SuggestionTextBox_Panel_Host.TabIndex = 0;
            // 
            // SuggestionTextBox_TextBox
            // 
            SuggestionTextBox_TextBox.BorderStyle = BorderStyle.FixedSingle;
            SuggestionTextBox_TextBox.Dock = DockStyle.Fill;
            SuggestionTextBox_TextBox.Location = new Point(0, 0);
            SuggestionTextBox_TextBox.Margin = new Padding(0);
            SuggestionTextBox_TextBox.MinimumSize = new Size(0, 27);
            SuggestionTextBox_TextBox.Name = "SuggestionTextBox_TextBox";
            SuggestionTextBox_TextBox.Size = new Size(150, 27);
            SuggestionTextBox_TextBox.TabIndex = 0;
            // 
            // SuggestionTextBox
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BackColor = SystemColors.Window;
            Controls.Add(SuggestionTextBox_Panel_Host);
            Margin = new Padding(0);
            MinimumSize = new Size(150, 27);
            Name = "SuggestionTextBox";
            Size = new Size(150, 27);
            SuggestionTextBox_Panel_Host.ResumeLayout(false);
            SuggestionTextBox_Panel_Host.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
