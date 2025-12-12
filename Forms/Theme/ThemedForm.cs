using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Core.Theming;
using MTM_WIP_Application_Winforms.Core.Theming.Interfaces;
using MTM_WIP_Application_Winforms.Services.Logging;
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

        this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
        this.AutoScaleMode = AutoScaleMode.Dpi;

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
                        // Only apply theme if theming is enabled
                        if (Model_Application_Variables.ThemeEnabled && ThemeProvider?.CurrentTheme != null)
                        {
                            ApplyTheme(ThemeProvider.CurrentTheme);
                        }
                        else if (!Model_Application_Variables.ThemeEnabled)
                        {
                            // Theming disabled - use system default colors
                            ResetToSystemColors();
                        }
                    };


                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                LoggingUtility.LogApplicationError(ex);
                // Log but don't fail - form will work without theming                
            }
        }
        else
        {

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
        // Only apply theme if theming is enabled (unless it's a preview)
        if (!Model_Application_Variables.ThemeEnabled && e.Reason != ThemeChangeReason.Preview)
        {
            return; // Theming is disabled, ignore theme changes
        }

        // Prevent cross-thread operations if handle is not created
        if (!IsHandleCreated)
        {
            return;
        }

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
    /// Resets the form and all controls to system default colors.
    /// Used when theming is disabled.
    /// </summary>
    private void ResetToSystemColors()
    {
        // Reset form to system defaults
        this.BackColor = SystemColors.Control;
        this.ForeColor = SystemColors.ControlText;

        // Recursively reset all child controls
        ResetControlToSystemColors(this);
    }

    /// <summary>
    /// Recursively resets a control and its children to system colors.
    /// </summary>
    private void ResetControlToSystemColors(Control control)
    {
        foreach (Control child in control.Controls)
        {
            if (child.IsDisposed)
                continue;

            // Reset to appropriate system color based on control type
            if (child is TextBox or ComboBox or ListBox)
            {
                child.BackColor = SystemColors.Window;
                child.ForeColor = SystemColors.WindowText;
            }
            else if (child is Button)
            {
                child.BackColor = SystemColors.Control;
                child.ForeColor = SystemColors.ControlText;
            }
            else if (child is DataGridView)
            {
                child.BackColor = SystemColors.Window;
                child.ForeColor = SystemColors.WindowText;
            }
            else
            {
                child.BackColor = SystemColors.Control;
                child.ForeColor = SystemColors.ControlText;
            }

            // Recursively reset children
            if (child.HasChildren)
            {
                ResetControlToSystemColors(child);
            }
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

                formApplier.Apply(this, theme);
            }
            else
            {
                // Fallback if no applier found

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

    private void InitializeComponent()
    {
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThemedForm));
        SuspendLayout();
        // 
        // ThemedForm
        // 
        ClientSize = new Size(284, 261);
        Name = "ThemedForm";
        ResumeLayout(false);

    }

    /// <summary>
    /// Handles the HelpRequested event (F1 key).
    /// </summary>
    protected override void OnHelpRequested(HelpEventArgs hevent)
    {
        // Prevent default help behavior
        hevent.Handled = true;

        // Show help viewer
        ShowHelp();

        base.OnHelpRequested(hevent);
    }

    /// <summary>
    /// Shows the help viewer for the current context.
    /// </summary>
    protected virtual void ShowHelp()
    {
        try
        {
            // Check if HelpViewerForm is already open
            // We use reflection or dynamic to avoid circular dependency if HelpViewerForm inherits from ThemedForm
            // But HelpViewerForm DOES inherit from ThemedForm.
            // So we can just use the type if we have the namespace.
            
            var helpForm = Application.OpenForms.OfType<Form>().FirstOrDefault(f => f.Name == "HelpViewerForm");
            
            if (helpForm == null)
            {
                // We need to instantiate HelpViewerForm.
                // Since we are in Shared, and HelpViewerForm is in Help, we might have a circular dependency if we reference it directly?
                // No, Shared is usually lower level. But HelpViewerForm inherits ThemedForm.
                // So ThemedForm -> HelpViewerForm -> ThemedForm.
                // This is fine in C# as long as they are in the same assembly.
                
                // However, to be safe and clean, maybe we should use a service or event?
                // For now, let's try to find the type by name to avoid direct reference if possible, 
                // or just assume it's available in the assembly.
                
                var helpFormType = Type.GetType("MTM_WIP_Application_Winforms.Forms.Help.HelpViewerForm");
                if (helpFormType != null)
                {
                    helpForm = Activator.CreateInstance(helpFormType) as Form;
                    if (helpForm != null)
                    {
                        helpForm.Show();
                    }
                }
            }
            else
            {
                helpForm.BringToFront();
                if (helpForm.WindowState == FormWindowState.Minimized)
                {
                    helpForm.WindowState = FormWindowState.Normal;
                }
            }

            if (helpForm != null)
            {
                // Call ShowHelp method on the form
                var method = helpForm.GetType().GetMethod("ShowHelp");
                if (method != null)
                {
                    string categoryId = GetHelpContext();
                    _ = method.Invoke(helpForm, new object?[] { categoryId, null });
                }
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
        }
    }

    /// <summary>
    /// Gets the help context ID for this form.
    /// Override in derived classes to provide specific context.
    /// </summary>
    protected virtual string GetHelpContext()
    {
        return string.Empty; // Default to index
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
