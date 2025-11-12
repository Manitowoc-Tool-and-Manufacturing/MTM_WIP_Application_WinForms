using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MTM_WIP_Application_Winforms.Core.Theming;
using MTM_WIP_Application_Winforms.Core.Theming.Appliers;
using MTM_WIP_Application_Winforms.Core.Theming.Interfaces;

namespace MTM_WIP_Application_Winforms.Core.DependencyInjection;

/// <summary>
/// Extension methods for configuring theme services in the dependency injection container.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds theme services to the service collection.
    /// Registers IThemeProvider, IThemeStore, ThemeDebouncer, and all 17 IThemeApplier implementations as singletons.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <returns>The service collection for method chaining.</returns>
    public static IServiceCollection AddThemeServices(this IServiceCollection services)
    {
        // Register logging if not already registered
        if (!services.Any(sd => sd.ServiceType == typeof(ILoggerFactory)))
        {
            services.AddLogging(builder =>
            {
                builder.AddConsole();
            });
        }

        // Register core theme services as singletons
        services.AddSingleton<IThemeStore, ThemeStore>();
        services.AddSingleton<ThemeDebouncer>();
        services.AddSingleton<IThemeProvider, ThemeManager>();

        // Register all 17 IThemeApplier implementations as singletons
        services.AddSingleton<IThemeApplier, FormThemeApplier>();
        services.AddSingleton<IThemeApplier, DataGridThemeApplier>();
        services.AddSingleton<IThemeApplier, ButtonThemeApplier>();
        services.AddSingleton<IThemeApplier, TextBoxThemeApplier>();
        services.AddSingleton<IThemeApplier, LabelThemeApplier>();
        services.AddSingleton<IThemeApplier, PanelThemeApplier>();
        services.AddSingleton<IThemeApplier, ComboBoxThemeApplier>();
        services.AddSingleton<IThemeApplier, CheckBoxThemeApplier>();
        services.AddSingleton<IThemeApplier, RadioButtonThemeApplier>();
        services.AddSingleton<IThemeApplier, GroupBoxThemeApplier>();
        services.AddSingleton<IThemeApplier, TabControlThemeApplier>();
        services.AddSingleton<IThemeApplier, ListBoxThemeApplier>();
        services.AddSingleton<IThemeApplier, TreeViewThemeApplier>();
        services.AddSingleton<IThemeApplier, MenuStripThemeApplier>();
        services.AddSingleton<IThemeApplier, StatusStripThemeApplier>();
        services.AddSingleton<IThemeApplier, ToolStripThemeApplier>();
        services.AddSingleton<IThemeApplier, SplitContainerThemeApplier>();

        return services;
    }
}
