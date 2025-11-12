using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Core.Theming;
using MTM_WIP_Application_Winforms.Core.Theming.Interfaces;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;
using Microsoft.Extensions.DependencyInjection;

namespace MTM_WIP_Application_Winforms.Forms.Shared;

/// <summary>
/// Base class for forms that automatically respond to theme changes.
/// Inheriting forms receive automatic theme updates via dependency injection.
/// </summary>
public class ThemedForm : Form
{
    /// <summary>
    /// The theme provider for subscribing to theme changes.
    /// </summary>
    protected readonly IThemeProvider? ThemeProvider;

    /// <summary>
    /// The collection of theme appliers for applying themes to controls.
    /// </summary>
    protected readonly IEnumerable<IThemeApplier>? ThemeAppliers;

    private bool _subscribed;
    private bool _disposed;

    /// <summary>
    /// Parameterless constructor for designer support and non-DI scenarios.
    /// Automatically resolves theme dependencies from Program.ServiceProvider if available.
    /// </summary>
    public ThemedForm()
    {
        InitializeComponentSafe();

        // Try to resolve dependencies from static ServiceProvider
        // This happens AFTER InitializeComponent to ensure form is ready
        if (Program.ServiceProvider != null)
        {
            try
            {
                var themeProvider = Program.ServiceProvider.GetService<IThemeProvider>();
                var themeAppliers = Program.ServiceProvider.GetService<IEnumerable<IThemeApplier>>();

                if (themeProvider != null && themeAppliers != null)
                {
                    // Initialize with resolved dependencies
                    ThemeProvider = themeProvider;
                    ThemeAppliers = themeAppliers;

                    // Subscribe to theme changes
                    ThemeProvider.ThemeChanged += OnThemeChanged;
                    ThemeProvider.Subscribe(this);
                    _subscribed = true;

                    // Apply current theme immediately on Shown event (when form is first displayed)
                    this.Shown += (s, e) =>
                    {
                        if (ThemeProvider?.CurrentTheme != null)
                        {
                            ApplyTheme(ThemeProvider.CurrentTheme);
                        }
                    };

                    LoggingUtility.Log($"[ThemedForm] {this.GetType().Name} initialized with automatic theme support");
                }
                else
                {
                    LoggingUtility.Log($"[ThemedForm] {this.GetType().Name} - Theme services not available in ServiceProvider");
                }
            }
            catch (Exception ex)
            {
                // Log but don't fail - form will work without theming
                LoggingUtility.Log($"[ThemedForm] {this.GetType().Name} could not resolve theme dependencies: {ex.Message}");
            }
        }
        else
        {
            LoggingUtility.Log($"[ThemedForm] {this.GetType().Name} - Program.ServiceProvider is null (designer mode or pre-DI initialization)");
        }
    }

    /// <summary>
    /// Initializes a new instance of the ThemedForm class with dependency injection.
    /// </summary>
    /// <param name="themeProvider">The theme provider for managing theme changes.</param>
    /// <param name="themeAppliers">The collection of theme appliers for control styling.</param>
    public ThemedForm(IThemeProvider themeProvider, IEnumerable<IThemeApplier> themeAppliers)
    {
        ThemeProvider = themeProvider ?? throw new ArgumentNullException(nameof(themeProvider));
        ThemeAppliers = themeAppliers ?? throw new ArgumentNullException(nameof(themeAppliers));

        // Subscribe to theme changes
        ThemeProvider.ThemeChanged += OnThemeChanged;
        ThemeProvider.Subscribe(this);
        _subscribed = true;

        InitializeComponentSafe();

        // Apply current theme immediately
        if (ThemeProvider.CurrentTheme != null)
        {
            ApplyTheme(ThemeProvider.CurrentTheme);
        }
    }

    /// <summary>
    /// Safe initialization - override in derived classes instead of InitializeComponent.
    /// </summary>
    protected virtual void InitializeComponentSafe()
    {
        // Override in derived classes to call actual InitializeComponent()
    }

    /// <summary>
    /// Handles theme change events.
    /// </summary>
    private void OnThemeChanged(object? sender, ThemeChangedEventArgs e)
    {
        if (InvokeRequired)
        {
            Invoke(() => ApplyTheme(e.NewTheme));
        }
        else
        {
            ApplyTheme(e.NewTheme);
        }
    }

    /// <summary>
    /// Applies a theme to this form and all its controls.
    /// Override to customize theme application behavior.
    /// </summary>
    /// <param name="theme">The theme to apply.</param>
    protected virtual void ApplyTheme(Model_Shared_UserUiColors theme)
    {
        if (theme == null || ThemeAppliers == null)
        {
            return;
        }

        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        try
        {
            // Suspend layout for performance
            SuspendLayout();

            // Apply theme to the form itself using the FormThemeApplier
            var formApplier = ThemeAppliers?.FirstOrDefault(a => a.CanApply(this));
            if (formApplier != null)
            {
                LoggingUtility.Log($"[ThemedForm] Using FormThemeApplier for {Name}");
                formApplier.Apply(this, theme);
            }
            else
            {
                // Fallback if no applier found
                LoggingUtility.Log($"[ThemedForm] No applier found for {Name}, using fallback. ThemeAppliers count: {ThemeAppliers?.Count() ?? 0}");
                BackColor = theme.FormBackColor ?? theme.ControlBackColor ?? Color.White;
                ForeColor = theme.FormForeColor ?? theme.ControlForeColor ?? Color.Black;
            }

            // Apply theme to all controls recursively
            ApplyThemeToControlHierarchy(this, theme);
        }
        finally
        {
            // Resume layout
            ResumeLayout(performLayout: true);
            
            stopwatch.Stop();
            
            // Log performance warning if theme application took too long
            if (stopwatch.ElapsedMilliseconds > 100)
            {
                LoggingUtility.Log(
                    $"[Performance Warning] Theme application to form '{Name}' took {stopwatch.ElapsedMilliseconds}ms (>100ms threshold)");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(
                    $"[Theme] Applied theme to form '{Name}' in {stopwatch.ElapsedMilliseconds}ms");
            }
        }
    }

    /// <summary>
    /// Recursively applies theme to a control and its children.
    /// </summary>
    private void ApplyThemeToControlHierarchy(Control parent, Model_Shared_UserUiColors theme)
    {
        foreach (Control control in parent.Controls)
        {
            // Skip disposed controls
            if (control.IsDisposed)
            {
                continue;
            }

            // Find matching applier
            var applier = ThemeAppliers?.FirstOrDefault(a => a.CanApply(control));
            
            if (applier != null)
            {
                try
                {
                    applier.Apply(control, theme);
                }
                catch (Exception ex)
                {
                    // Log error but continue with other controls
                    System.Diagnostics.Debug.WriteLine(
                        $"Error applying theme to {control.GetType().Name}: {ex.Message}");
                }
            }

            // Recursively apply to children
            if (control.HasChildren)
            {
                ApplyThemeToControlHierarchy(control, theme);
            }
        }
    }

    /// <summary>
    /// Subscribes to Controls.ControlAdded event to handle dynamically added controls.
    /// Also applies DPI scaling, layout adjustments, and focus highlighting.
    /// </summary>
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        
        // Apply focus highlighting for better UX
        Core_Themes.ApplyFocusHighlighting(this);

        // T076: Subscribe to ControlAdded event to theme dynamically added controls
        ControlAdded += OnControlAdded;
    }

    /// <summary>
    /// T076: Handles dynamically added controls by applying current theme.
    /// </summary>
    private void OnControlAdded(object? sender, ControlEventArgs e)
    {
        if (e.Control == null || ThemeProvider?.CurrentTheme == null)
        {
            return;
        }

        try
        {
            // Apply theme to the newly added control
            ApplyThemeToControl(e.Control, ThemeProvider.CurrentTheme);
            
            // Also subscribe to any child container controls for nested additions
            if (e.Control is Panel or GroupBox or TabControl or SplitContainer)
            {
                e.Control.ControlAdded += OnControlAdded;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(
                $"Error applying theme to dynamically added control '{e.Control.Name}': {ex.Message}");
        }
    }

    /// <summary>
    /// Helper method to apply theme to a single control and its children.
    /// </summary>
    private void ApplyThemeToControl(Control control, Model_Shared_UserUiColors theme)
    {
        if (control == null || control.IsDisposed)
        {
            return;
        }

        // Find matching applier
        var applier = ThemeAppliers?.FirstOrDefault(a => a.CanApply(control));
        
        if (applier != null)
        {
            try
            {
                applier.Apply(control, theme);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"Error applying theme to {control.GetType().Name}: {ex.Message}");
            }
        }

        // Recursively apply to children
        if (control.HasChildren)
        {
            ApplyThemeToControlHierarchy(control, theme);
        }
    }

    /// <summary>
    /// Cleans up resources and unsubscribes from theme changes.
    /// </summary>
    protected override void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // T078: Unsubscribe from ControlAdded event
                ControlAdded -= OnControlAdded;
                
                // Unsubscribe from events
                if (_subscribed && ThemeProvider != null)
                {
                    ThemeProvider.ThemeChanged -= OnThemeChanged;
                    ThemeProvider.Unsubscribe(this);
                    _subscribed = false;
                }
            }

            _disposed = true;
        }

        base.Dispose(disposing);
    }
}
