using Microsoft.Extensions.DependencyInjection;
using MTM_WIP_Application_Winforms.Core.DependencyInjection;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Services.DeveloperTools;
using MTM_WIP_Application_Winforms.Services.ErrorHandling;
using MTM_WIP_Application_Winforms.Services.Logging;

namespace MTM_WIP_Application_Winforms.Services.Startup
{
    /// <summary>
    /// Handles the initialization and configuration of the Dependency Injection container at application startup.
    /// </summary>
    public static class Service_OnStartup_DependencyInjection
    {
        #region Methods

        /// <summary>
        /// Configures the application services and builds the service provider.
        /// </summary>
        /// <returns>The configured <see cref="IServiceProvider"/> ready for dependency injection.</returns>
        /// <exception cref="Exception">Thrown when the service provider cannot be built or services fail to register.</exception>
        public static IServiceProvider ConfigureServices()
        {
            try
            {
                // LoggingUtility.Log("[Startup] Initializing dependency injection container"); // Cannot log yet as service not initialized

                ServiceCollection services = new ServiceCollection();

                // Register Core Services
                services.AddSingleton<ILoggingService, Service_Logging>();
                services.AddSingleton<IService_ErrorHandler, Service_ErrorHandler>();

                // Register theme services
                services.AddThemeServices();

                // Register Shortcut services
                services.AddSingleton<IDao_Shortcuts, Dao_Shortcuts>();
                services.AddSingleton<IShortcutService, Service_Shortcut>();

                // Register Visual Database Service
                services.AddTransient<MTM_WIP_Application_Winforms.Services.Visual.IService_VisualDatabase, MTM_WIP_Application_Winforms.Services.Visual.Service_VisualDatabase>();

                // Register Analytics Services
                services.AddTransient<MTM_WIP_Application_Winforms.Data.IDao_VisualAnalytics, MTM_WIP_Application_Winforms.Data.Dao_VisualAnalytics>();
                services.AddTransient<MTM_WIP_Application_Winforms.Services.Analytics.IService_UserShiftLogic, MTM_WIP_Application_Winforms.Services.Analytics.Service_UserShiftLogic>();

                // Register Feedback Services
                services.AddTransient<IDao_UserFeedback, Dao_UserFeedback>();
                services.AddTransient<IDao_UserFeedbackComments, Dao_UserFeedbackComments>();
                services.AddTransient<IDao_WindowFormMapping, Dao_WindowFormMapping>();
                services.AddTransient<IDao_UserControlMapping, Dao_UserControlMapping>();
                services.AddTransient<MTM_WIP_Application_Winforms.Services.IService_FeedbackManager, MTM_WIP_Application_Winforms.Services.Service_FeedbackManager>();

                // Register Developer Tools Services
                services.AddTransient<IDao_DeveloperTools, Dao_DeveloperTools>();
                services.AddTransient<IService_DeveloperTools, Service_DeveloperTools>();

                // Build the service provider
                ServiceProvider serviceProvider = services.BuildServiceProvider();

                // Initialize static instances for backward compatibility
                var loggingService = serviceProvider.GetRequiredService<ILoggingService>();
                Service_Logging.Instance = loggingService;

                var errorHandler = serviceProvider.GetRequiredService<IService_ErrorHandler>();
                Service_ErrorHandler.Instance = errorHandler;

                LoggingUtility.Log("[Startup] Dependency injection container initialized successfully");
                return serviceProvider;
            }
            catch (Exception ex)
            {
                // LoggingUtility.LogApplicationError(ex); // Cannot log if service failed to initialize
                Service_OnStartup_AppLifecycle.ShowFatalError("Dependency Injection Error",
                    $"Failed to initialize application services:\n\n{ex.Message}\n\n" +
                    "The application cannot continue without proper service configuration.\n" +
                    "Please contact your system administrator.");
                throw;
            }
        }

        #endregion
    }
}
