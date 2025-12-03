namespace MTM_WIP_Application_Winforms.Forms.Shared
{
    partial class SuggestionOverlayForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            suggestionListBox = new ListBox();
            lblMatchCount = new Label();
            SuspendLayout();
            // 
            // suggestionListBox
            // 
            suggestionListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            suggestionListBox.BorderStyle = BorderStyle.FixedSingle;
            suggestionListBox.FormattingEnabled = true;
            suggestionListBox.IntegralHeight = false;
            suggestionListBox.ItemHeight = 15;
            suggestionListBox.Location = new Point(0, 27);
            suggestionListBox.Margin = new Padding(0);
            suggestionListBox.Name = "suggestionListBox";
            suggestionListBox.Size = new Size(400, 265);
            suggestionListBox.TabIndex = 0;
            suggestionListBox.DoubleClick += suggestionListBox_DoubleClick;
            // 
            // lblMatchCount
            // 
            lblMatchCount.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblMatchCount.BackColor = SystemColors.Control;
            lblMatchCount.BorderStyle = BorderStyle.FixedSingle;
            lblMatchCount.Font = new Font("Segoe UI Emoji", 9F, FontStyle.Bold);
            lblMatchCount.ForeColor = Color.FromArgb(64, 64, 64);
            lblMatchCount.Location = new Point(0, 0);
            lblMatchCount.Margin = new Padding(0);
            lblMatchCount.Name = "lblMatchCount";
            lblMatchCount.Padding = new Padding(5, 2, 5, 2);
            lblMatchCount.Size = new Size(400, 28);
            lblMatchCount.TabIndex = 1;
            lblMatchCount.Text = "0 matches found";
            lblMatchCount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // SuggestionOverlayForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            ClientSize = new Size(400, 295);
            Controls.Add(lblMatchCount);
            Controls.Add(suggestionListBox);
            FormBorderStyle = FormBorderStyle.None;
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SuggestionOverlayForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.Manual;
            Text = "Suggestions";
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox suggestionListBox;
        private System.Windows.Forms.Label lblMatchCount;
    }
}
