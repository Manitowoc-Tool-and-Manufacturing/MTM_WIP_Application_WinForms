using Microsoft.Web.WebView2.Core;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Services.Logging;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Forms.Help;

namespace MTM_WIP_Application_Winforms.Forms.Settings
{
    public partial class SettingsForm_ViewReleaseNotesHTML : ThemedForm
    {
        private readonly string _jsonContent;
        private bool _isShowingComparison = false;

        public SettingsForm_ViewReleaseNotesHTML(string jsonContent)
        {
            InitializeComponent();
            InitializeHelpButton();
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
    #region Helpers

    private Button? Form_ReleaseNotes_Button_Help;

    private void InitializeHelpButton()
    {
        Form_ReleaseNotes_Button_Help = new Button();
        Form_ReleaseNotes_Button_Help.Name = "Form_ReleaseNotes_Button_Help";
        Form_ReleaseNotes_Button_Help.Text = "?";
        Form_ReleaseNotes_Button_Help.Size = new Size(24, 24);
        Form_ReleaseNotes_Button_Help.Location = new Point(panelTop.Width - 30, 8); 
        Form_ReleaseNotes_Button_Help.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        Form_ReleaseNotes_Button_Help.Click += (s, e) => 
        {
            HelpViewerForm.GetInstance().BringToFrontAndNavigate("getting-started", "release-notes");
        };
        
        panelTop.Controls.Add(Form_ReleaseNotes_Button_Help);
        Form_ReleaseNotes_Button_Help.BringToFront();
    }

    #endregion

    }
}
