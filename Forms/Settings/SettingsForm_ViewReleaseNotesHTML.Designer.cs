namespace MTM_WIP_Application_Winforms.Forms.Settings
{
    partial class SettingsForm_ViewReleaseNotesHTML
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm_ViewReleaseNotesHTML));
            webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            panelTop = new Panel();
            btnViewComparison = new Button();
            ((System.ComponentModel.ISupportInitialize)webView21).BeginInit();
            panelTop.SuspendLayout();
            SuspendLayout();
            // 
            // webView21
            // 
            webView21.AllowExternalDrop = true;
            webView21.CreationProperties = null;
            webView21.DefaultBackgroundColor = Color.White;
            webView21.Dock = DockStyle.Fill;
            webView21.Location = new Point(0, 40);
            webView21.Name = "webView21";
            webView21.Size = new Size(800, 410);
            webView21.TabIndex = 1;
            webView21.ZoomFactor = 1D;
            // 
            // panelTop
            // 
            panelTop.Controls.Add(btnViewComparison);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(800, 40);
            panelTop.TabIndex = 0;
            // 
            // btnViewComparison
            // 
            btnViewComparison.Location = new Point(12, 8);
            btnViewComparison.Name = "btnViewComparison";
            btnViewComparison.Size = new Size(200, 23);
            btnViewComparison.TabIndex = 0;
            btnViewComparison.Text = "View Version Comparison Report";
            btnViewComparison.UseVisualStyleBackColor = true;
            btnViewComparison.Click += btnViewComparison_Click;
            // 
            // ViewReleaseNotesForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(800, 450);
            Controls.Add(webView21);
            Controls.Add(panelTop);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ViewReleaseNotesForm";
            Text = "Release Notes";
            ((System.ComponentModel.ISupportInitialize)webView21).EndInit();
            panelTop.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnViewComparison;
    }
}
