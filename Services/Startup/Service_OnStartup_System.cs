using System.Diagnostics;
using MTM_WIP_Application_Winforms.Models;

/// <summary>
/// Provides system-level initialization services for the application startup process.
/// Handles configuration of debugging tools, Windows Forms environment settings, and system tracing.
/// </summary>
namespace MTM_WIP_Application_Winforms.Services.Startup
{
    public static class Service_OnStartup_System
    {
        /// <summary>
        /// Initializes the debugging infrastructure based on the build configuration.
        /// </summary>
        /// <remarks>
        /// <para>
        /// In <c>DEBUG</c> builds, this sets the trace level to <see cref="Enum_DebugLevel.High"/> and enables development mode settings.
        /// In <c>RELEASE</c> builds, it sets the trace level to <see cref="Enum_DebugLevel.Medium"/> and uses default configurations.
        /// </para>
        /// <para>
        /// This method also logs an initial "APPLICATION_STARTUP" trace event containing system metadata 
        /// (OS version, processor count, memory usage, etc.).
        /// </para>
        /// </remarks>
        public static void InitializeDebugging()
        {
            #if DEBUG
            Service_DebugTracer.Initialize(Enum_DebugLevel.High);
            Service_DebugConfiguration.InitializeDefaults();
            Service_DebugConfiguration.SetDevelopmentMode();
            #else
            Service_DebugTracer.Initialize(Enum_DebugLevel.Medium);
            Service_DebugConfiguration.InitializeDefaults();
            #endif

            Service_DebugTracer.TraceUIAction("APPLICATION_STARTUP", "Program", new Dictionary<string, object>
            {
                ["StartupTime"] = DateTime.Now,
                ["IsDebugMode"] = Debugger.IsAttached,
                ["OSVersion"] = Environment.OSVersion.ToString(),
                ["ProcessorCount"] = Environment.ProcessorCount,
                ["WorkingSet"] = Environment.WorkingSet
            });
        }

        /// <summary>
        /// Configures the essential Windows Forms application settings required before creating any UI forms.
        /// </summary>
        /// <returns>
        /// <c>true</c> if initialization succeeds; otherwise, <c>false</c>.
        /// </returns>
        /// <remarks>
        /// This method sets up:
        /// <list type="bullet">
        /// <item><description>High DPI mode (PerMonitorV2)</description></item>
        /// <item><description>Application configuration via <see cref="ApplicationConfiguration"/></description></item>
        /// <item><description>Visual styles</description></item>
        /// <item><description>Compatible text rendering defaults</description></item>
        /// </list>
        /// If an exception occurs, a fatal error dialog is shown to the user explaining potential causes.
        /// </remarks>
        public static bool InitializeWinForms()
        {
            try
            {
                Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
                ApplicationConfiguration.Initialize();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Startup Error] Windows Forms initialization failed: {ex.Message}");
                Service_OnStartup_AppLifecycle.ShowFatalError("Windows Forms Initialization Error",
                    $"Failed to initialize Windows Forms:\n\n{ex.Message}\n\n" +
                    "This may be caused by:\n" +
                    "• Missing .NET runtime components\n" +
                    "• System display configuration issues\n" +
                    "• Insufficient system permissions\n\n" +
                    "Please contact your system administrator.");
                return false;
            }
        }

        /// <summary>
        /// Configures the standard .NET <see cref="System.Diagnostics.Trace"/> listeners.
        /// </summary>
        /// <remarks>
        /// Clears existing listeners and adds a <see cref="DefaultTraceListener"/>. 
        /// It also enables <see cref="Trace.AutoFlush"/> to ensure logs are written immediately.
        /// If setup fails, a warning is written to the Console, but execution continues.
        /// </remarks>
        public static void SetupTrace()
        {
             try
            {
                Trace.Listeners.Clear();
                Trace.Listeners.Add(new DefaultTraceListener());
                Trace.AutoFlush = true;
                Console.WriteLine("[Trace] [Main] Application starting...");
                Trace.WriteLine("[Trace] [Main] Application starting...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Warning] Trace setup failed: {ex.Message}");
            }
        }
    }
}
