# Interface Contracts: Theme System Refactoring

**Feature**: Theme System Refactoring  
**Date**: 2025-11-11  
**Status**: Complete

This document defines all public interfaces (contracts) for the refactored theme system.

---

## Core Interfaces

### IThemeProvider

**Purpose**: Central theme management abstraction - provides theme access and change notifications

**Namespace**: `MTM_WIP_Application_Winforms.Core.Theming.Interfaces`

```csharp
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MTM_WIP_Application_Winforms.Core.Theming.Interfaces
{
    /// <summary>
    /// Provides theme management capabilities including theme retrieval,
    /// selection, and change notifications.
    /// </summary>
    public interface IThemeProvider
    {
        /// <summary>
        /// Gets the currently active theme for the application.
        /// </summary>
        AppTheme CurrentTheme { get; }
        
        /// <summary>
        /// Gets a read-only list of all available themes.
        /// </summary>
        IReadOnlyList<AppTheme> AvailableThemes { get; }
        
        /// <summary>
        /// Asynchronously sets the active theme by name.
        /// </summary>
        /// <param name="themeName">Name of the theme to activate</param>
        /// <returns>True if theme was successfully changed, false otherwise</returns>
        /// <exception cref="ArgumentNullException">If themeName is null or empty</exception>
        Task<bool> SetThemeAsync(string themeName);
        
        /// <summary>
        /// Asynchronously loads themes from the database.
        /// Called on application startup or when theme list needs refresh.
        /// </summary>
        Task LoadThemesFromDatabaseAsync();
        
        /// <summary>
        /// Event raised when the active theme changes.
        /// Subscribers receive ThemeChangedEventArgs with old and new theme.
        /// </summary>
        event EventHandler<ThemeChangedEventArgs>? ThemeChanged;
        
        /// <summary>
        /// Subscribes a form to automatic theme change notifications.
        /// Form will receive updates via ThemeChanged event.
        /// </summary>
        /// <param name="form">Form to subscribe</param>
        void Subscribe(Form form);
        
        /// <summary>
        /// Unsubscribes a form from theme change notifications.
        /// Should be called in Form.Dispose() to prevent memory leaks.
        /// </summary>
        /// <param name="form">Form to unsubscribe</param>
        void Unsubscribe(Form form);
    }
}
```

**Implementation Notes**:
- ThemeManager implements this interface
- CurrentTheme returns cached theme (no database hit)
- SetThemeAsync triggers debouncing logic (300ms)
- ThemeChanged event uses WeakReference for subscribers
- Subscribe/Unsubscribe manage FormRegistration collection

---

### IThemeApplier

**Purpose**: Strategy interface for applying themes to specific control types

**Namespace**: `MTM_WIP_Application_Winforms.Core.Theming.Interfaces`

```csharp
using System.Windows.Forms;

namespace MTM_WIP_Application_Winforms.Core.Theming.Interfaces
{
    /// <summary>
    /// Defines a strategy for applying theme colors and styles to a specific control type.
    /// Implementations handle control-specific styling logic.
    /// </summary>
    public interface IThemeApplier
    {
        /// <summary>
        /// Determines whether this applier can handle the given control.
        /// </summary>
        /// <param name="control">Control to check</param>
        /// <returns>True if this applier can theme the control, false otherwise</returns>
        bool CanApply(Control control);
        
        /// <summary>
        /// Applies theme colors and styles to the given control.
        /// Should only be called if CanApply returns true.
        /// </summary>
        /// <param name="control">Control to theme</param>
        /// <param name="theme">Theme to apply</param>
        void Apply(Control control, AppTheme theme);
    }
}
```

**Implementation Strategy**:
- One concrete implementation per control type
- Examples: DataGridThemeApplier, ButtonThemeApplier, TextBoxThemeApplier
- CanApply uses `is` type checking
- Apply casts to specific type and sets properties

**Example Implementation**:
```csharp
public class DataGridThemeApplier : ThemeApplierBase
{
    public override bool CanApply(Control control) => control is DataGridView;
    
    public override void Apply(Control control, AppTheme theme)
    {
        if (control is not DataGridView grid) return;
        
        grid.BackgroundColor = theme.Colors.DataGridBackColor ?? Color.White;
        grid.ForeColor = theme.Colors.DataGridForeColor ?? Color.Black;
        grid.GridColor = theme.Colors.DataGridGridColor ?? Color.LightGray;
        // ... 15+ more DataGridView-specific properties
    }
}
```

---

### IThemeStore

**Purpose**: Abstraction for theme data access - loads themes from database and caches

**Namespace**: `MTM_WIP_Application_Winforms.Core.Theming.Interfaces`

```csharp
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MTM_WIP_Application_Winforms.Core.Theming.Interfaces
{
    /// <summary>
    /// Provides access to theme definitions with caching support.
    /// Abstracts the underlying storage mechanism (database, files, etc.).
    /// </summary>
    public interface IThemeStore
    {
        /// <summary>
        /// Asynchronously retrieves a theme by name.
        /// Returns cached theme if available, otherwise loads from database.
        /// </summary>
        /// <param name="themeName">Name of theme to retrieve</param>
        /// <returns>The requested theme, or default theme if not found</returns>
        Task<AppTheme> GetThemeAsync(string themeName);
        
        /// <summary>
        /// Asynchronously retrieves all available themes.
        /// Returns cached list if available, otherwise loads from database.
        /// </summary>
        /// <returns>Read-only list of all themes</returns>
        Task<IReadOnlyList<AppTheme>> GetAllThemesAsync();
        
        /// <summary>
        /// Asynchronously loads theme definitions from the database.
        /// Refreshes the internal cache.
        /// </summary>
        Task LoadFromDatabaseAsync();
        
        /// <summary>
        /// Clears the theme cache, forcing next retrieval to reload from database.
        /// </summary>
        void InvalidateCache();
    }
}
```

**Implementation Notes**:
- ThemeStore wraps existing Core_AppThemes static class
- Implements two-level caching (theme objects + applied state)
- GetThemeAsync returns default theme as fallback
- LoadFromDatabaseAsync delegates to Core_AppThemes.LoadThemesFromDatabaseAsync()

---

## Event Arguments

### ThemeChangedEventArgs

**Purpose**: Carries information about a theme change event

**Namespace**: `MTM_WIP_Application_Winforms.Core.Theming`

```csharp
using System;

namespace MTM_WIP_Application_Winforms.Core.Theming
{
    /// <summary>
    /// Event arguments for theme change notifications.
    /// Contains old and new theme plus metadata about the change.
    /// </summary>
    public class ThemeChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the theme that was previously active.
        /// </summary>
        public AppTheme OldTheme { get; init; }
        
        /// <summary>
        /// Gets the newly activated theme.
        /// </summary>
        public AppTheme NewTheme { get; init; }
        
        /// <summary>
        /// Gets the user ID who initiated the theme change.
        /// </summary>
        public string UserId { get; init; }
        
        /// <summary>
        /// Gets the timestamp when the theme change occurred.
        /// </summary>
        public DateTime ChangedAt { get; init; }
        
        /// <summary>
        /// Gets the reason why the theme changed.
        /// </summary>
        public ThemeChangeReason Reason { get; init; }
        
        /// <summary>
        /// Initializes a new instance of ThemeChangedEventArgs.
        /// </summary>
        public ThemeChangedEventArgs(
            AppTheme oldTheme,
            AppTheme newTheme,
            string userId,
            ThemeChangeReason reason)
        {
            OldTheme = oldTheme ?? throw new ArgumentNullException(nameof(oldTheme));
            NewTheme = newTheme ?? throw new ArgumentNullException(nameof(newTheme));
            UserId = userId ?? throw new ArgumentNullException(nameof(userId));
            ChangedAt = DateTime.Now;
            Reason = reason;
        }
    }
    
    /// <summary>
    /// Enumerates reasons why a theme change occurred.
    /// </summary>
    public enum ThemeChangeReason
    {
        /// <summary>User explicitly selected a new theme in settings.</summary>
        UserSelection,
        
        /// <summary>Theme loaded when user logged in.</summary>
        Login,
        
        /// <summary>DPI scaling triggered theme re-application.</summary>
        DpiChange,
        
        /// <summary>System fell back to default theme due to error.</summary>
        SystemDefault,
        
        /// <summary>Preview window theme change (doesn't affect main theme).</summary>
        Preview
    }
}
```

---

## Base Classes

### ThemeApplierBase

**Purpose**: Abstract base class providing common functionality for IThemeApplier implementations

**Namespace**: `MTM_WIP_Application_Winforms.Core.Theming.Appliers`

```csharp
using Microsoft.Extensions.Logging;
using System.Windows.Forms;

namespace MTM_WIP_Application_Winforms.Core.Theming.Appliers
{
    /// <summary>
    /// Base class for theme applier implementations.
    /// Provides common functionality like logging and error handling.
    /// </summary>
    public abstract class ThemeApplierBase : IThemeApplier
    {
        protected ILogger Logger { get; }
        
        protected ThemeApplierBase(ILogger logger)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        /// <summary>
        /// Determines whether this applier can handle the given control.
        /// Must be overridden by derived classes.
        /// </summary>
        public abstract bool CanApply(Control control);
        
        /// <summary>
        /// Applies theme to the control.
        /// Must be overridden by derived classes.
        /// </summary>
        public abstract void Apply(Control control, AppTheme theme);
        
        /// <summary>
        /// Applies common styling properties shared by most controls.
        /// </summary>
        protected virtual void ApplyCommonStyles(Control control, AppTheme theme)
        {
            if (control == null || theme == null) return;
            
            control.BackColor = theme.Colors.ControlBackColor ?? Color.White;
            control.ForeColor = theme.Colors.ControlForeColor ?? Color.Black;
            
            if (theme.FormFont != null)
            {
                control.Font = new Font(
                    theme.FormFont.FontFamily,
                    control.Font.Size,
                    control.Font.Style);
            }
        }
        
        /// <summary>
        /// Handles errors during theme application.
        /// Logs error and continues (doesn't throw).
        /// </summary>
        protected void HandleApplyError(Exception ex, Control control, AppTheme theme)
        {
            Logger.LogError(ex, 
                "Error applying theme '{ThemeName}' to control '{ControlType}' named '{ControlName}'",
                theme.Name,
                control.GetType().Name,
                control.Name);
            
            LoggingUtility.LogApplicationError(ex);
        }
    }
}
```

---

### ThemedForm

**Purpose**: Base form class that automatically subscribes to theme changes

**Namespace**: `MTM_WIP_Application_Winforms.Forms.Shared`

```csharp
using MTM_WIP_Application_Winforms.Core.Theming.Interfaces;
using System.Windows.Forms;

namespace MTM_WIP_Application_Winforms.Forms.Shared
{
    /// <summary>
    /// Base form class that automatically subscribes to theme change notifications.
    /// Forms inheriting from this class will receive automatic theme updates.
    /// </summary>
    public class ThemedForm : Form
    {
        protected IThemeProvider ThemeProvider { get; }
        protected IEnumerable<IThemeApplier> ThemeAppliers { get; }
        
        /// <summary>
        /// Initializes a new instance of ThemedForm with dependency injection.
        /// </summary>
        /// <param name="themeProvider">Theme provider service</param>
        /// <param name="themeAppliers">Collection of theme applier strategies</param>
        public ThemedForm(
            IThemeProvider themeProvider,
            IEnumerable<IThemeApplier> themeAppliers)
        {
            ThemeProvider = themeProvider ?? throw new ArgumentNullException(nameof(themeProvider));
            ThemeAppliers = themeAppliers ?? throw new ArgumentNullException(nameof(themeAppliers));
            
            // Subscribe to theme changes
            ThemeProvider.Subscribe(this);
            ThemeProvider.ThemeChanged += OnThemeChanged;
        }
        
        /// <summary>
        /// Handles theme change notifications.
        /// </summary>
        protected virtual void OnThemeChanged(object? sender, ThemeChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(() => OnThemeChanged(sender, e));
                return;
            }
            
            ApplyTheme(e.NewTheme);
        }
        
        /// <summary>
        /// Applies a theme to this form and all its controls.
        /// </summary>
        protected virtual void ApplyTheme(AppTheme theme)
        {
            SuspendLayout();
            
            try
            {
                // Apply theme to form itself
                BackColor = theme.Colors.FormBackColor ?? Color.White;
                ForeColor = theme.Colors.FormForeColor ?? Color.Black;
                
                if (theme.FormFont != null)
                {
                    Font = theme.FormFont;
                }
                
                // Apply theme to all child controls
                ApplyThemeToControlHierarchy(Controls, theme);
            }
            finally
            {
                ResumeLayout(performLayout: true);
                Invalidate(invalidateChildren: true);
            }
        }
        
        /// <summary>
        /// Recursively applies theme to control hierarchy.
        /// </summary>
        private void ApplyThemeToControlHierarchy(Control.ControlCollection controls, AppTheme theme)
        {
            foreach (Control control in controls)
            {
                // Find appropriate applier for this control type
                var applier = ThemeAppliers.FirstOrDefault(a => a.CanApply(control));
                
                if (applier != null)
                {
                    applier.Apply(control, theme);
                }
                
                // Recurse into child controls
                if (control.HasChildren)
                {
                    ApplyThemeToControlHierarchy(control.Controls, theme);
                }
            }
        }
        
        /// <summary>
        /// Cleans up theme subscriptions when form is disposed.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Unsubscribe from theme changes to prevent memory leaks
                ThemeProvider.ThemeChanged -= OnThemeChanged;
                ThemeProvider.Unsubscribe(this);
            }
            
            base.Dispose(disposing);
        }
    }
}
```

---

### ThemedUserControl

**Purpose**: Base user control class with automatic theme subscription

**Namespace**: `MTM_WIP_Application_Winforms.Forms.Shared`

```csharp
using MTM_WIP_Application_Winforms.Core.Theming.Interfaces;
using System.Windows.Forms;

namespace MTM_WIP_Application_Winforms.Forms.Shared
{
    /// <summary>
    /// Base user control class that automatically subscribes to theme changes.
    /// User controls inheriting from this class will receive automatic theme updates.
    /// </summary>
    public class ThemedUserControl : UserControl
    {
        protected IThemeProvider ThemeProvider { get; }
        protected IEnumerable<IThemeApplier> ThemeAppliers { get; }
        
        public ThemedUserControl(
            IThemeProvider themeProvider,
            IEnumerable<IThemeApplier> themeAppliers)
        {
            ThemeProvider = themeProvider ?? throw new ArgumentNullException(nameof(themeProvider));
            ThemeAppliers = themeAppliers ?? throw new ArgumentNullException(nameof(themeAppliers));
            
            // Subscribe to theme changes
            ThemeProvider.ThemeChanged += OnThemeChanged;
        }
        
        protected virtual void OnThemeChanged(object? sender, ThemeChangedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(() => OnThemeChanged(sender, e));
                return;
            }
            
            ApplyTheme(e.NewTheme);
        }
        
        protected virtual void ApplyTheme(AppTheme theme)
        {
            SuspendLayout();
            
            try
            {
                BackColor = theme.Colors.ControlBackColor ?? Color.White;
                ForeColor = theme.Colors.ControlForeColor ?? Color.Black;
                
                ApplyThemeToControls(Controls, theme);
            }
            finally
            {
                ResumeLayout(performLayout: true);
                Invalidate(invalidateChildren: true);
            }
        }
        
        private void ApplyThemeToControls(Control.ControlCollection controls, AppTheme theme)
        {
            foreach (Control control in controls)
            {
                var applier = ThemeAppliers.FirstOrDefault(a => a.CanApply(control));
                applier?.Apply(control, theme);
                
                if (control.HasChildren)
                {
                    ApplyThemeToControls(control.Controls, theme);
                }
            }
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ThemeProvider.ThemeChanged -= OnThemeChanged;
            }
            
            base.Dispose(disposing);
        }
    }
}
```

---

## Dependency Injection Extensions

### ServiceCollectionExtensions

**Purpose**: Extension method for registering all theme services

**Namespace**: `MTM_WIP_Application_Winforms.Core.DependencyInjection`

```csharp
using Microsoft.Extensions.DependencyInjection;
using MTM_WIP_Application_Winforms.Core.Theming;
using MTM_WIP_Application_Winforms.Core.Theming.Interfaces;
using MTM_WIP_Application_Winforms.Core.Theming.Appliers;

namespace MTM_WIP_Application_Winforms.Core.DependencyInjection
{
    /// <summary>
    /// Extension methods for configuring theme services in the DI container.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds all theme-related services to the dependency injection container.
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <returns>Service collection for method chaining</returns>
        public static IServiceCollection AddThemeServices(this IServiceCollection services)
        {
            // Core services (singletons)
            services.AddSingleton<IThemeStore, ThemeStore>();
            services.AddSingleton<IThemeProvider, ThemeManager>();
            
            // Theme appliers (one per control type)
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
            // ... Add remaining appliers for all control types
            
            return services;
        }
    }
}
```

---

## Contract Summary

| Interface | Purpose | Key Methods | Lifetime |
|-----------|---------|-------------|----------|
| IThemeProvider | Theme management | SetThemeAsync, Subscribe, Unsubscribe | Singleton |
| IThemeApplier | Control styling strategy | CanApply, Apply | Singleton |
| IThemeStore | Theme data access | GetThemeAsync, LoadFromDatabaseAsync | Singleton |

| Base Class | Purpose | Derived By | Constructor DI |
|------------|---------|------------|----------------|
| ThemeApplierBase | Common applier logic | All *ThemeApplier classes | ILogger |
| ThemedForm | Auto-subscribing form | All new forms | IThemeProvider, IEnumerable<IThemeApplier> |
| ThemedUserControl | Auto-subscribing control | All new controls | IThemeProvider, IEnumerable<IThemeApplier> |

| Event Args | Purpose | Properties |
|------------|---------|------------|
| ThemeChangedEventArgs | Theme change notification | OldTheme, NewTheme, UserId, ChangedAt, Reason |

**All contracts defined** - ready to proceed to quickstart guide.
