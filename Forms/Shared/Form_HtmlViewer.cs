using System;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;
using MTM_WIP_Application_Winforms.Forms.Shared;

namespace MTM_WIP_Application_Winforms.Forms.Shared
{
    public partial class Form_HtmlViewer : ThemedForm
    {
        private WebView2 _webView;
        private string? _tempFilePath;

        public Form_HtmlViewer(string title, string htmlContent)
        {
            InitializeComponent();
            this.Text = title;
            
            _webView = new WebView2();
            _webView.Dock = DockStyle.Fill;
            this.Controls.Add(_webView);
            
            InitializeWebView(htmlContent);
        }

        public Form_HtmlViewer(string title, Uri sourceUri, string? tempFilePath = null)
        {
            InitializeComponent();
            this.Text = title;
            _tempFilePath = tempFilePath;

            _webView = new WebView2();
            _webView.Dock = DockStyle.Fill;
            this.Controls.Add(_webView);

            InitializeWebViewUri(sourceUri);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            if (!string.IsNullOrEmpty(_tempFilePath) && System.IO.File.Exists(_tempFilePath))
            {
                try
                {
                    System.IO.File.Delete(_tempFilePath);
                }
                catch { /* Ignore cleanup errors */ }
            }
        }

        private async void InitializeWebViewUri(Uri uri)
        {
            await EnsureWebViewInitialized();
            _webView.Source = uri;
        }

        private async void InitializeWebView(string htmlContent)
        {
            await EnsureWebViewInitialized();
            _webView.NavigateToString(htmlContent);
        }

        private async System.Threading.Tasks.Task EnsureWebViewInitialized()
        {
            // Configure WebView2 to use a local user data folder to prevent file locking issues
            string userDataFolder = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), 
                "MTM", 
                "WebView2");
            
            var env = await Microsoft.Web.WebView2.Core.CoreWebView2Environment.CreateAsync(null, userDataFolder);
            await _webView.EnsureCoreWebView2Async(env);
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