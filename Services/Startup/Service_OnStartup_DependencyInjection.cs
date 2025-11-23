using Microsoft.Extensions.DependencyInjection;
using MTM_WIP_Application_Winforms.Core.DependencyInjection;
using MTM_WIP_Application_Winforms.Data;
using MTM_WIP_Application_Winforms.Logging;

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
                LoggingUtility.Log("[Startup] Initializing dependency injection container");

                ServiceCollection services = new ServiceCollection();

                // Register theme services
                services.AddThemeServices();

                // Register Shortcut services
                services.AddSingleton<IDao_Shortcuts, Dao_Shortcuts>();
                services.AddSingleton<IShortcutService, Service_Shortcut>();

                // Build the service provider
                ServiceProvider serviceProvider = services.BuildServiceProvider();

                LoggingUtility.Log("[Startup] Dependency injection container initialized successfully");
                return serviceProvider;
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
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
