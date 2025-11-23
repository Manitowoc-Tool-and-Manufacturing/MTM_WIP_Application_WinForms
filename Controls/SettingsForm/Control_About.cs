using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Forms.Settings;
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    /// <summary>
    /// Control for displaying application information and changelog.
    /// </summary>
    public partial class Control_About : ThemedUserControl
    {
        #region Fields

        public event EventHandler<string>? StatusMessageChanged;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Control_About"/> class.
        /// </summary>
        public Control_About()
        {
            InitializeComponent();
            Control_About_LoadControl();
        }

        #endregion

        #region Initialization

        private void Control_About_LoadControl()
        {
            LoggingUtility.Log("[Control_About] Loading control started");
            try
            {
                // Load Version Info
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                Control_About_Label_AppInfo_Version.Text = $"Version: {version?.ToString() ?? "Unknown"}";
                
                // Copyright and Owner are static in designer, but can be dynamic if needed
                Control_About_Label_AppInfo_Copyright.Text = $"Copyright © {DateTime.Now.Year}";
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium);
            }
        }

        #endregion

        #region Event Handlers

        private void Control_About_Button_ViewReleaseNotes_Click(object sender, EventArgs e)
        {
            try
            {
                string releaseNotesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RELEASE_NOTES.json");
                
                // If not found in bin, check project root (for dev environment)
                if (!File.Exists(releaseNotesPath))
                {
                    string projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
                    string devPath = Path.Combine(projectRoot, "RELEASE_NOTES.json");
                    if (File.Exists(devPath))
                    {
                        releaseNotesPath = devPath;
                    }
                }

                if (File.Exists(releaseNotesPath))
                {
                    string jsonContent = File.ReadAllText(releaseNotesPath);
                    using (var form = new ViewReleaseNotesForm(jsonContent))
                    {
                        form.ShowDialog(this);
                    }
                }
                else
                {
                    Service_ErrorHandler.ShowWarning("Release notes file not found.");
                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                Service_ErrorHandler.HandleException(ex, Enum_ErrorSeverity.Medium);
            }
        }

        #endregion

        #region Cleanup Methods

        /// <summary>
        /// Static method to clean up temp files at application shutdown
        /// </summary>
        public static void CleanupAllTempFiles()
        {
            // No temp files to clean up in new version
        }
        
        internal void CleanupTempFiles()
        {
             // No temp files to clean up in new version
        }

        #endregion
    }
}
