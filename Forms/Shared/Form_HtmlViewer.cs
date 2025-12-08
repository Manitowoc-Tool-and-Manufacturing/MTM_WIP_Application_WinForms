using Microsoft.Web.WebView2.WinForms;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_HtmlViewer));
            SuspendLayout();
            // 
            // Form_HtmlViewer
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(1000, 800);
            Name = "Form_HtmlViewer";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Viewer";
            ResumeLayout(false);
        }
    }
}
