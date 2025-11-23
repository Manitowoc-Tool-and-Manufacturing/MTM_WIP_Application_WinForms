namespace MTM_WIP_Application_Winforms.Forms.Settings
{
    partial class ViewReleaseNotesForm
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
            this.webView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnViewComparison = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).BeginInit();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // webView21
            // 
            this.webView21.AllowExternalDrop = true;
            this.webView21.CreationProperties = null;
            this.webView21.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView21.Location = new System.Drawing.Point(0, 40);
            this.webView21.Name = "webView21";
            this.webView21.Size = new System.Drawing.Size(800, 410);
            this.webView21.TabIndex = 1;
            this.webView21.ZoomFactor = 1D;
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.btnViewComparison);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(800, 40);
            this.panelTop.TabIndex = 0;
            // 
            // btnViewComparison
            // 
            this.btnViewComparison.Location = new System.Drawing.Point(12, 8);
            this.btnViewComparison.Name = "btnViewComparison";
            this.btnViewComparison.Size = new System.Drawing.Size(200, 23);
            this.btnViewComparison.TabIndex = 0;
            this.btnViewComparison.Text = "View Version Comparison Report";
            this.btnViewComparison.UseVisualStyleBackColor = true;
            this.btnViewComparison.Click += new System.EventHandler(this.btnViewComparison_Click);
            // 
            // ViewReleaseNotesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.webView21);
            this.Controls.Add(this.panelTop);
            this.Name = "ViewReleaseNotesForm";
            this.Text = "Release Notes";
            ((System.ComponentModel.ISupportInitialize)(this.webView21)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnViewComparison;
    }
}
