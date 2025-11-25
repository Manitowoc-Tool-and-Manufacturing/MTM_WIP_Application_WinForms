using System.Diagnostics.CodeAnalysis;
using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Forms.Splash
{
    public partial class SplashScreenForm : Form
    {
        #region Constructors

        public SplashScreenForm()
        {
            Service_DebugTracer.TraceMethodEntry(new Dictionary<string, object>
            {
                ["FormType"] = nameof(SplashScreenForm),
                ["Version"] = Model_Application_Variables.Version ?? "4.6.0.0",
                ["InitializationTime"] = DateTime.Now,
                ["Thread"] = Thread.CurrentThread.ManagedThreadId
            }, nameof(SplashScreenForm), nameof(SplashScreenForm));

            System.Diagnostics.Debug.WriteLine("[DEBUG] [SplashScreenForm.ctor] Constructing SplashScreenForm...");

            Service_DebugTracer.TraceUIAction("SPLASH_FORM_INITIALIZATION", nameof(SplashScreenForm),
                new Dictionary<string, object>
                {
                    ["Phase"] = "START",
                    ["ComponentType"] = "SplashScreenForm"
                });

            InitializeSplashComponents();

            Service_DebugTracer.TraceUIAction("THEME_APPLICATION", nameof(SplashScreenForm),
                new Dictionary<string, object>
                {
                    ["AutoScaleMode"] = "Dpi",
                    ["DpiScaling"] = "APPLIED",
                    ["LayoutAdjustments"] = "APPLIED"
                });

            
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;

            Service_DebugTracer.TraceBusinessLogic("UI_COLORS_APPLICATION",
                inputData: new
                {
                    UserBackColor = Model_Application_Variables.UserUiColors?.FormBackColor,
                    UserForeColor = Model_Application_Variables.UserUiColors?.LabelForeColor,
                    Version = Model_Application_Variables.Version
                },
                outputData: new
                {
                    AppliedBackColor = Model_Application_Variables.UserUiColors?.FormBackColor ?? BackColor,
                    AppliedForeColor = Model_Application_Variables.UserUiColors?.LabelForeColor,
                    VersionText = $"Version {Model_Application_Variables.Version ?? "4.6.0.0"}"
                });
            BackColor = Model_Application_Variables.UserUiColors?.FormBackColor ?? BackColor;
            _titleLabel!.ForeColor = Model_Application_Variables.UserUiColors?.LabelForeColor ?? _titleLabel.ForeColor;
            _versionLabel!.ForeColor = Model_Application_Variables.UserUiColors?.LabelForeColor ?? _versionLabel.ForeColor;
            _versionLabel.Text = $"Version {Model_Application_Variables.Version ?? "4.6.0.0"}";

            Service_DebugTracer.TraceUIAction("THEME_APPLIED", nameof(SplashScreenForm));
            // MIGRATION NOTE: SplashScreenForm should be migrated to ThemedForm base class
            // For now, we apply theme colors manually without using Core_Themes.ApplyTheme()
            // which would override the colors we just set above.
            // TODO: Migrate SplashScreenForm to inherit from ThemedForm

            Service_DebugTracer.TraceUIAction("SPLASH_FORM_INITIALIZATION", nameof(SplashScreenForm),
                new Dictionary<string, object>
                {
                    ["Phase"] = "COMPLETE",
                    ["Success"] = true
                });

            Service_DebugTracer.TraceMethodExit(null, nameof(SplashScreenForm), nameof(SplashScreenForm));
            System.Diagnostics.Debug.WriteLine("[DEBUG] [SplashScreenForm.ctor] SplashScreenForm constructed.");
        }

        #endregion

        #region Methods

        [MemberNotNull(nameof(_progressControl), nameof(_logoBox), nameof(_mainPanel))]
        private void InitializeSplashComponents()
        {
            // InitializeComponent initializes designer-generated controls.
            InitializeComponent();

            // Ensure required designer components are instantiated; otherwise throw to satisfy MemberNotNull contract.
            if (_progressControl is null)
                throw new InvalidOperationException("_progressControl was not initialized by InitializeComponent().");
            if (_logoBox is null)
                throw new InvalidOperationException("_logoBox was not initialized by InitializeComponent().");
            if (_mainPanel is null)
                throw new InvalidOperationException("_mainPanel was not initialized by InitializeComponent().");
        }

        public void ShowSplash()
        {
            Service_DebugTracer.TraceMethodEntry(null, nameof(ShowSplash), nameof(SplashScreenForm));

            Service_DebugTracer.TraceUIAction("SPLASH_SCREEN_SHOW", nameof(SplashScreenForm),
                new Dictionary<string, object>
                {
                    ["Action"] = "Show",
                    ["ProgressControlVisible"] = true
                });

            System.Diagnostics.Debug.WriteLine("[DEBUG] [SplashScreenForm.ShowSplash] Showing splash screen...");
            Show();
            _progressControl!.ShowProgress();
            Application.DoEvents();
            System.Diagnostics.Debug.WriteLine("[DEBUG] [SplashScreenForm.ShowSplash] Splash screen shown.");

            Service_DebugTracer.TraceMethodExit(null, nameof(ShowSplash), nameof(SplashScreenForm));
        }

        public void UpdateProgress(int progress, string status)
        {
            System.Diagnostics.Debug.WriteLine(
                $"[DEBUG] [SplashScreenForm.UpdateProgress] Progress: {progress}, Status: {status}");
            _progressControl!.UpdateProgress(progress, status);
            Application.DoEvents();
        }

        #endregion
    }
}
