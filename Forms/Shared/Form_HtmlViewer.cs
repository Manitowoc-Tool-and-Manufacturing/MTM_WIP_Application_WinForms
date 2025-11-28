using System;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;
using MTM_WIP_Application_Winforms.Forms.Shared;

namespace MTM_WIP_Application_Winforms.Forms.Shared
{
    public partial class Form_HtmlViewer : ThemedForm
    {
        private WebView2 _webView;

        public Form_HtmlViewer(string title, string htmlContent)
        {
            InitializeComponent();
            this.Text = title;
            
            _webView = new WebView2();
            _webView.Dock = DockStyle.Fill;
            this.Controls.Add(_webView);
            
            InitializeWebView(htmlContent);
        }

        private async void InitializeWebView(string htmlContent)
        {
            await _webView.EnsureCoreWebView2Async();
            _webView.NavigateToString(htmlContent);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form_HtmlViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 800);
            this.Name = "Form_HtmlViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Viewer";
            this.ResumeLayout(false);
        }
    }
}