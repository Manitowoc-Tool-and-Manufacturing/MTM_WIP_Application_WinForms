using Microsoft.Web.WebView2.Core;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Forms.Settings
{
    public partial class ViewReleaseNotesForm : ThemedForm
    {
        private readonly string _jsonContent;
        private bool _isShowingComparison = false;

        public ViewReleaseNotesForm(string jsonContent)
        {
            InitializeComponent();
            _jsonContent = jsonContent;
            InitializeWebView();
        }

        private async void InitializeWebView()
        {
            try
            {
                await webView21.EnsureCoreWebView2Async();
                webView21.CoreWebView2.NavigationStarting += CoreWebView2_NavigationStarting;
                DisplayReleaseNotes();
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Models.Enum_ErrorSeverity.Medium);
            }
        }

        private void CoreWebView2_NavigationStarting(object? sender, CoreWebView2NavigationStartingEventArgs e)
        {
            // Handle navigation to local files
            if (e.Uri.EndsWith("release-notes.html", StringComparison.OrdinalIgnoreCase))
            {
                e.Cancel = true;
                this.BeginInvoke(new Action(DisplayReleaseNotes));
            }
        }

        private void btnViewComparison_Click(object sender, EventArgs e)
        {
            if (_isShowingComparison)
            {
                DisplayReleaseNotes();
                btnViewComparison.Text = "View Version Comparison Report";
                _isShowingComparison = false;
            }
            else
            {
                DisplayVersionComparison();
                btnViewComparison.Text = "Back to Release Notes";
                _isShowingComparison = true;
            }
        }

        private void DisplayVersionComparison()
        {
            try
            {
                string? htmlPath = GetFilePath(@"Documentation\ReleaseNotes\version-comparison.html");
                if (!string.IsNullOrEmpty(htmlPath))
                {
                    string htmlContent = File.ReadAllText(htmlPath);
                    webView21.NavigateToString(htmlContent);
                }
                else
                {
                    webView21.NavigateToString("<html><body><h1>Error: Version comparison report not found</h1></body></html>");
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                webView21.NavigateToString("<html><body><h1>Error loading version comparison</h1><p>" + ex.Message + "</p></body></html>");
            }
        }

        private void DisplayReleaseNotes()
        {
            try
            {
                // Load HTML Template
                string? htmlPath = GetFilePath(@"Documentation\ReleaseNotes\release-notes.html");
                string htmlContent = "<html><body><h1>Error: Template not found</h1></body></html>";

                if (!string.IsNullOrEmpty(htmlPath))
                {
                    htmlContent = File.ReadAllText(htmlPath);
                }

                // Load Watermark Image
                string? imagePath = GetFilePath(@"Resources\MTM.png");
                string base64Image = "";
                if (!string.IsNullOrEmpty(imagePath))
                {
                    byte[] imageBytes = File.ReadAllBytes(imagePath);
                    base64Image = Convert.ToBase64String(imageBytes);
                }

                // Inject Data
                htmlContent = htmlContent.Replace("{{RELEASE_NOTES_JSON}}", _jsonContent);
                htmlContent = htmlContent.Replace("{{WATERMARK_BASE64}}", base64Image);

                webView21.NavigateToString(htmlContent);
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                webView21.NavigateToString("<html><body><h1>Error loading release notes</h1><p>" + ex.Message + "</p></body></html>");
            }
        }

        private string? GetFilePath(string relativePath)
        {
            // Check bin directory
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
            if (File.Exists(path)) return path;

            // Check project root (for dev)
            string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
            path = Path.Combine(projectRoot, relativePath);
            if (File.Exists(path)) return path;

            return null;
        }
    }
}
