using Microsoft.Web.WebView2.Core;
using MTM_WIP_Application_Winforms.Forms.Help;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;
using MTM_WIP_Application_Winforms.Services.Analytics;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MTM_WIP_Application_Winforms.Forms.Visual
{
    /// <summary>
    /// Displays the role-aware analytics workspace in WebView2.
    /// </summary>
    public partial class Form_AnalyticsViewer : ThemedForm
    {
        #region Fields
        private readonly Service_Analytics _analyticsService;
        private bool _isLoaded;
        private string? _tempFilePath;
        private Button? Form_AnalyticsViewer_Button_Help;
        #endregion

        #region Properties
        // Intentionally left blank.
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Form_AnalyticsViewer"/> class.
        /// </summary>
        public Form_AnalyticsViewer()
        {
            InitializeComponent();
            InitializeHelpButton();
            _analyticsService = new Service_Analytics();
        }
        #endregion

        #region Methods
        /// <inheritdoc />
        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (_isLoaded)
            {
                return;
            }

            _isLoaded = true;
            await LoadAnalyticsAsync();
        }

        /// <inheritdoc />
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            if (string.IsNullOrWhiteSpace(_tempFilePath) || !File.Exists(_tempFilePath))
            {
                return;
            }

            try
            {
                File.Delete(_tempFilePath);
            }
            catch
            {
                // Ignore temp file cleanup issues.
            }
        }

        private async Task LoadAnalyticsAsync()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                Enabled = false;

                var snapshotResult = await _analyticsService.GetAnalyticsSnapshotAsync();
                if (!snapshotResult.IsSuccess || snapshotResult.Data == null)
                {
                    Service_ErrorHandler.ShowUserError(snapshotResult.ErrorMessage);
                    Close();
                    return;
                }

                Text = snapshotResult.Data.Title;
                await EnsureWebViewInitializedAsync();

                string htmlContent = await File.ReadAllTextAsync(GetTemplatePath());
                string serializedData = JsonConvert.SerializeObject(
                    snapshotResult.Data,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        DateFormatString = "yyyy-MM-ddTHH:mm:ss",
                        NullValueHandling = NullValueHandling.Ignore,
                    }
                );

                htmlContent = htmlContent.Replace(
                    "window.__ANALYTICS_DATA__ = null;",
                    $"window.__ANALYTICS_DATA__ = {serializedData};"
                );

                _tempFilePath = Path.Combine(
                    Path.GetTempPath(),
                    $"MTM_Analytics_{Guid.NewGuid():N}.html"
                );

                await File.WriteAllTextAsync(_tempFilePath, htmlContent);
                webView.Source = new Uri(_tempFilePath);
            }
            catch (Exception ex)
            {
                Service_ErrorHandler.HandleException(
                    ex,
                    Enum_ErrorSeverity.Medium,
                    callerName: nameof(LoadAnalyticsAsync),
                    controlName: Name
                );
                Close();
            }
            finally
            {
                Enabled = true;
                Cursor = Cursors.Default;
            }
        }
        #endregion

        #region Events
        // Intentionally left blank.
        #endregion

        #region Helpers
        private async Task EnsureWebViewInitializedAsync()
        {
            string userDataFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "MTM",
                "WebView2"
            );

            CoreWebView2Environment environment = await CoreWebView2Environment.CreateAsync(
                null,
                userDataFolder
            );

            await webView.EnsureCoreWebView2Async(environment);
        }

        private static string GetTemplatePath()
        {
            string deployedPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Resources",
                "Html",
                "VisualUserAnalytics_Enhanced.html"
            );

            if (File.Exists(deployedPath))
            {
                return deployedPath;
            }

            string sourcePath = Path.GetFullPath(
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    @"..\..\..\Resources\Html\VisualUserAnalytics_Enhanced.html"
                )
            );

            if (File.Exists(sourcePath))
            {
                return sourcePath;
            }

            throw new FileNotFoundException("Analytics template could not be found.", deployedPath);
        }

        private void InitializeHelpButton()
        {
            Form_AnalyticsViewer_Button_Help = new Button
            {
                Name = "Form_AnalyticsViewer_Button_Help",
                Text = "?",
                Size = new Size(24, 24),
                Location = new Point(Width - 40, 5),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
            };

            Form_AnalyticsViewer_Button_Help.Click += (_, _) =>
            {
                var helpForm = new HelpViewerForm();
                helpForm.Show();
                helpForm.ShowHelp("analytics-reporting", "user-performance");
            };

            Controls.Add(Form_AnalyticsViewer_Button_Help);
            Form_AnalyticsViewer_Button_Help.BringToFront();
        }
        #endregion

        #region Cleanup / Dispose
        // Intentionally left blank.
        #endregion
    }
}
