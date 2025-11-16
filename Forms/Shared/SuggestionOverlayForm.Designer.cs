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
            this.suggestionListBox = new System.Windows.Forms.ListBox();
            this.lblMatchCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // suggestionListBox
            // 
            this.suggestionListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.suggestionListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.suggestionListBox.FormattingEnabled = true;
            this.suggestionListBox.IntegralHeight = false;
            this.suggestionListBox.ItemHeight = 18;
            this.suggestionListBox.Location = new System.Drawing.Point(1, 30);
            this.suggestionListBox.Name = "suggestionListBox";
            this.suggestionListBox.Size = new System.Drawing.Size(398, 268);
            this.suggestionListBox.TabIndex = 0;
            this.suggestionListBox.DoubleClick += new System.EventHandler(this.suggestionListBox_DoubleClick);
            // 
            // lblMatchCount
            // 
            this.lblMatchCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMatchCount.BackColor = System.Drawing.SystemColors.Control;
            this.lblMatchCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMatchCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMatchCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblMatchCount.Location = new System.Drawing.Point(1, 1);
            this.lblMatchCount.Name = "lblMatchCount";
            this.lblMatchCount.Padding = new System.Windows.Forms.Padding(5, 2, 5, 2);
            this.lblMatchCount.Size = new System.Drawing.Size(398, 28);
            this.lblMatchCount.TabIndex = 1;
            this.lblMatchCount.Text = "0 matches found";
            this.lblMatchCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SuggestionOverlayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.lblMatchCount);
            this.Controls.Add(this.suggestionListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SuggestionOverlayForm";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Suggestions";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox suggestionListBox;
        private System.Windows.Forms.Label lblMatchCount;
    }
}
