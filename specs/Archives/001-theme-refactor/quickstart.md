# Quickstart Guide: Theme System Refactoring

**Feature**: Theme System Refactoring  
**Date**: 2025-11-11  
**Audience**: Developers working on MTM WIP Application

## Overview

This guide helps you get started with the refactored theme system. It covers:
- Setting up dependency injection
- Creating themed forms
- Applying themes to controls
- Migrating existing forms
- Testing theme functionality

---

## Prerequisites

- **MTM WIP Application** codebase cloned
- **Visual Studio 2022** or later
- **.NET 8.0 SDK** installed
- **NuGet packages**: Microsoft.Extensions.DependencyInjection 8.0+

---

## 1. Setup Dependency Injection

### Step 1: Install NuGet Package

```bash
dotnet add package Microsoft.Extensions.DependencyInjection --version 8.0.0
```

### Step 2: Configure Services in Program.cs

```csharp
using Microsoft.Extensions.DependencyInjection;
using MTM_WIP_Application_Winforms.Core.DependencyInjection;

namespace MTM_WIP_Application_Winforms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            
            // Create service collection
            var services = new ServiceCollection();
            
            // Register theme services
            services.AddThemeServices();
            
            // Register logging
            services.AddLogging();
            
            // Register forms (transient - new instance each time)
            services.AddTransient<MainForm>();
            
            // Build service provider
            var serviceProvider = services.BuildServiceProvider();
            
            // Resolve main form with dependencies
            var mainForm = serviceProvider.GetRequiredService<MainForm>();
            
            Application.Run(mainForm);
        }
    }
}
```

**What this does**:
- Creates a dependency injection container
- Registers all theme services (IThemeProvider, IThemeApplier implementations)
- Resolves MainForm with all dependencies automatically injected

---

## 2. Creating a New Themed Form

### Step 1: Inherit from ThemedForm

```csharp
using MTM_WIP_Application_Winforms.Forms.Shared;
using MTM_WIP_Application_Winforms.Core.Theming.Interfaces;

namespace MTM_WIP_Application_Winforms.Forms.MyFeature
{
    public partial class MyNewForm : ThemedForm
    {
        // Constructor with dependency injection
        public MyNewForm(
            IThemeProvider themeProvider,
            IEnumerable<IThemeApplier> themeAppliers)
            : base(themeProvider, themeAppliers)
        {
            InitializeComponent();
            
            // Apply initial theme
            ApplyTheme(themeProvider.CurrentTheme);
        }
        
        // Optional: Override theme application for custom behavior
        protected override void ApplyTheme(AppTheme theme)
        {
            base.ApplyTheme(theme); // Call base to apply default theming
            
            // Add form-specific theming here
            myCustomPanel.BorderStyle = theme.Name == "Dark" 
                ? BorderStyle.FixedSingle 
                : BorderStyle.None;
        }
    }
}
```

**Benefits**:
- Form automatically subscribes to theme changes
- Theme updates propagate automatically
- No manual cleanup required (handled in base.Dispose())

### Step 2: Designer File Compatibility

If using Visual Studio Designer, keep the designer file unchanged. The constructor signature will be different from the designer-generated code, so add a parameterless constructor for design-time:

```csharp
public partial class MyNewForm : ThemedForm
{
    // Design-time constructor (used by Visual Studio Designer)
    public MyNewForm() : base(null!, null!)
    {
        InitializeComponent();
    }
    
    // Runtime constructor (used by DI container)
    public MyNewForm(
        IThemeProvider themeProvider,
        IEnumerable<IThemeApplier> themeAppliers)
        : base(themeProvider, themeAppliers)
    {
        InitializeComponent();
        ApplyTheme(themeProvider.CurrentTheme);
    }
}
```

---

## 3. Migrating an Existing Form

### Before Migration

```csharp
public partial class ExistingForm : Form
{
    public ExistingForm()
    {
        InitializeComponent();
        
        // Old static approach
        Core_Themes.ApplyTheme(this);
    }
    
    private void OnSettingsChanged()
    {
        // Manual re-application
        Core_Themes.ApplyTheme(this);
    }
}
```

### After Migration

```csharp
public partial class ExistingForm : ThemedForm
{
    // Add DI constructor
    public ExistingForm(
        IThemeProvider themeProvider,
        IEnumerable<IThemeApplier> themeAppliers)
        : base(themeProvider, themeAppliers)
    {
        InitializeComponent();
        
        // Initial theme applied automatically by ThemedForm
    }
    
    // Remove manual ApplyTheme calls - handled automatically
    private void OnSettingsChanged()
    {
        // Theme updates happen automatically via event subscription
        // No manual re-application needed!
    }
}
```

**Migration Checklist**:
- [ ] Change base class from `Form` to `ThemedForm`
- [ ] Add DI constructor with `IThemeProvider` and `IEnumerable<IThemeApplier>`
- [ ] Remove all `Core_Themes.ApplyTheme()` calls
- [ ] Register form in DI container (`services.AddTransient<ExistingForm>()`)
- [ ] Test theme changes work automatically

---

## 4. Changing Themes Programmatically

### Option 1: Via IThemeProvider (Recommended)

```csharp
public partial class SettingsForm : ThemedForm
{
    private readonly IThemeProvider _themeProvider;
    
    public SettingsForm(
        IThemeProvider themeProvider,
        IEnumerable<IThemeApplier> themeAppliers)
        : base(themeProvider, themeAppliers)
    {
        _themeProvider = themeProvider;
        InitializeComponent();
        
        // Populate theme dropdown
        comboBoxThemes.DataSource = _themeProvider.AvailableThemes
            .Select(t => t.Name)
            .ToList();
    }
    
    private async void btnApplyTheme_Click(object sender, EventArgs e)
    {
        string selectedTheme = comboBoxThemes.SelectedItem.ToString();
        
        // Change theme - all subscribed forms update automatically
        bool success = await _themeProvider.SetThemeAsync(selectedTheme);
        
        if (success)
        {
            
        }
        else
        {
            Service_ErrorHandler.HandleException(
                new InvalidOperationException($"Failed to load theme '{selectedTheme}'"),
                Enum_ErrorSeverity.Medium);
        }
    }
}
```

### Option 2: Via User Preference (Persistent)

```csharp
private async void btnSaveThemePreference_Click(object sender, EventArgs e)
{
    string userId = Model_Application_Variables.UserId;
    string selectedTheme = comboBoxThemes.SelectedItem.ToString();
    
    // Save to database
    var result = await Dao_User.SetThemeNameAsync(userId, selectedTheme);
    
    if (result.IsSuccess)
    {
        // Apply theme
        await _themeProvider.SetThemeAsync(selectedTheme);
        
        
    }
    else
    {
        Service_ErrorHandler.HandleDatabaseError(
            new Exception(result.ErrorMessage),
            Enum_ErrorSeverity.Medium);
    }
}
```

---

## 5. Creating a Custom Theme Applier

### Step 1: Implement IThemeApplier

```csharp
using MTM_WIP_Application_Winforms.Core.Theming.Interfaces;
using MTM_WIP_Application_Winforms.Core.Theming.Appliers;

namespace MTM_WIP_Application_Winforms.Core.Theming.Appliers
{
    public class CustomControlThemeApplier : ThemeApplierBase
    {
        public CustomControlThemeApplier(ILogger<CustomControlThemeApplier> logger)
            : base(logger)
        {
        }
        
        public override bool CanApply(Control control)
        {
            return control is MyCustomControl;
        }
        
        public override void Apply(Control control, AppTheme theme)
        {
            if (control is not MyCustomControl customControl)
                return;
            
            try
            {
                // Apply theme-specific styling
                customControl.BorderColor = theme.Colors.BorderColor ?? Color.Gray;
                customControl.HighlightColor = theme.Colors.AccentColor ?? Color.Blue;
                customControl.ShadowColor = theme.Name == "Dark" 
                    ? Color.FromArgb(50, 0, 0, 0) 
                    : Color.FromArgb(30, 0, 0, 0);
                
                // Apply common styles
                ApplyCommonStyles(customControl, theme);
                
                Logger.LogInformation(
                    "Applied theme '{ThemeName}' to {ControlType}",
                    theme.Name,
                    control.GetType().Name);
            }
            catch (Exception ex)
            {
                HandleApplyError(ex, control, theme);
            }
        }
    }
}
```

### Step 2: Register in DI

```csharp
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddThemeServices(this IServiceCollection services)
    {
        // ... existing registrations ...
        
        // Register custom applier
        services.AddSingleton<IThemeApplier, CustomControlThemeApplier>();
        
        return services;
    }
}
```

---

## 6. Testing Theme Functionality

### Unit Test Example

```csharp
using Xunit;
using Moq;
using MTM_WIP_Application_Winforms.Core.Theming;
using MTM_WIP_Application_Winforms.Core.Theming.Interfaces;

namespace MTM_WIP_Application_Winforms.Tests.Unit.Theming
{
    public class ThemeManagerTests
    {
        [Fact]
        public async Task SetThemeAsync_ShouldNotifySubscribers()
        {
            // Arrange
            var mockStore = new Mock<IThemeStore>();
            var mockLogger = new Mock<ILogger<ThemeManager>>();
            var testTheme = CreateTestTheme("TestTheme");
            
            mockStore.Setup(s => s.GetThemeAsync("TestTheme"))
                .ReturnsAsync(testTheme);
            
            var themeManager = new ThemeManager(mockStore.Object, mockLogger.Object);
            
            bool eventRaised = false;
            themeManager.ThemeChanged += (s, e) =>
            {
                eventRaised = true;
                Assert.Equal("TestTheme", e.NewTheme.Name);
            };
            
            // Act
            await themeManager.SetThemeAsync("TestTheme");
            
            // Assert
            Assert.True(eventRaised);
        }
        
        private static AppTheme CreateTestTheme(string name)
        {
            return new AppTheme
            {
                Name = name,
                Colors = new Model_Shared_UserUiColors { /* ... */ }
            };
        }
    }
}
```

### Integration Test Example

```csharp
[Fact]
public async Task ThemeChange_ShouldUpdateAllOpenForms()
{
    // Arrange
    var serviceProvider = CreateTestServiceProvider();
    var themeProvider = serviceProvider.GetRequiredService<IThemeProvider>();
    
    var form1 = serviceProvider.GetRequiredService<TestForm>();
    var form2 = serviceProvider.GetRequiredService<TestForm>();
    
    form1.Show();
    form2.Show();
    
    Color expectedBackColor = Color.FromArgb(30, 30, 30);
    
    // Act
    await themeProvider.SetThemeAsync("Dark");
    await Task.Delay(150); // Allow event propagation
    
    // Assert
    Assert.Equal(expectedBackColor, form1.BackColor);
    Assert.Equal(expectedBackColor, form2.BackColor);
    
    // Cleanup
    form1.Close();
    form2.Close();
}
```

---

## 7. Common Patterns

### Pattern: Conditional Theming

```csharp
protected override void ApplyTheme(AppTheme theme)
{
    base.ApplyTheme(theme);
    
    // Different styling based on theme
    if (theme.Name == "HighContrast")
    {
        labelTitle.Font = new Font(labelTitle.Font, FontStyle.Bold);
        buttonPrimary.FlatStyle = FlatStyle.Standard;
    }
    else
    {
        labelTitle.Font = new Font(labelTitle.Font, FontStyle.Regular);
        buttonPrimary.FlatStyle = FlatStyle.Flat;
    }
}
```

### Pattern: Preview Window (Isolated Theme)

```csharp
public partial class ThemePreviewForm : Form
{
    private readonly IThemeStore _themeStore;
    
    public ThemePreviewForm(IThemeStore themeStore)
    {
        _themeStore = themeStore;
        InitializeComponent();
        
        // DON'T subscribe to global theme changes
        // Preview is isolated from main application theme
    }
    
    public async Task PreviewThemeAsync(string themeName)
    {
        var theme = await _themeStore.GetThemeAsync(themeName);
        
        // Apply theme locally without affecting other forms
        BackColor = theme.Colors.FormBackColor ?? Color.White;
        // ... apply other properties
    }
}
```

### Pattern: Debouncing Rapid Changes

```csharp
// ThemeManager handles debouncing automatically
// Multiple rapid calls within 300ms result in single application

private async void trackBarBrightness_Scroll(object sender, EventArgs e)
{
    // Each scroll event triggers theme change request
    // ThemeManager debounces - only applies after 300ms of no changes
    await _themeProvider.SetThemeAsync(GetThemeForBrightness(trackBarBrightness.Value));
}
```

---

## 8. Troubleshooting

### Error Classification Guide

**Critical Failures** (user notified via Service_ErrorHandler):
- Cannot load default theme (application cannot function)
- Fatal exceptions during theme initialization
- Complete loss of theme system functionality

**Recoverable Errors** (handled silently, logged only):
- Individual theme load failure (falls back to default theme)
- Missing theme preference in database (uses default)
- Corrupted theme data for non-active theme
- Transient network errors loading theme definitions

Per FR-012 and MTM Constitution Principle I, the system uses Service_ErrorHandler with appropriate severity:
- Critical failures → `Enum_ErrorSeverity.High` (shows dialog with retry option)
- Recoverable errors → Logged via `LoggingUtility.LogApplicationError()` (silent to user)

---

### Problem: Form doesn't receive theme updates

**Solution**: Verify form inherits from `ThemedForm` and base constructor is called:

```csharp
public MyForm(IThemeProvider themeProvider, IEnumerable<IThemeApplier> themeAppliers)
    : base(themeProvider, themeAppliers) // ← Must call base constructor
{
    InitializeComponent();
}
```

### Problem: Designer shows errors after adding DI constructor

**Solution**: Add parameterless constructor for design-time:

```csharp
// Design-time only
public MyForm() : base(null!, null!)
{
    InitializeComponent();
}

// Runtime (used by DI)
public MyForm(IThemeProvider provider, IEnumerable<IThemeApplier> appliers)
    : base(provider, appliers)
{
    InitializeComponent();
    ApplyTheme(provider.CurrentTheme);
}
```

### Problem: Theme changes slow (> 100ms)

**Solution**: Profile with Stopwatch to identify bottleneck:

```csharp
protected override void ApplyTheme(AppTheme theme)
{
    var sw = Stopwatch.StartNew();
    base.ApplyTheme(theme);
    sw.Stop();
    
    if (sw.ElapsedMilliseconds > 100)
    {
        LoggingUtility.Log(
            $"WARNING: Theme application took {sw.ElapsedMilliseconds}ms for {Name}");
    }
}
```

### Problem: Memory leak - forms not getting collected

**Solution**: Ensure Dispose() is called and base.Dispose() cleans up subscriptions:

```csharp
protected override void Dispose(bool disposing)
{
    if (disposing)
    {
        // Custom cleanup here
    }
    
    base.Dispose(disposing); // ← Must call base to unsubscribe
}
```

---

## 9. Next Steps

- **Read**: [data-model.md](./data-model.md) for entity relationships
- **Read**: [contracts/README.md](./contracts/README.md) for interface details
- **Read**: [research.md](./research.md) for architectural decisions
- **Implement**: Start migrating forms one-by-one to new system
- **Test**: Run unit and integration tests to verify functionality

---

## Summary

✅ **Setup**: Install Microsoft.Extensions.DependencyInjection, configure Program.cs  
✅ **Create**: Inherit from ThemedForm for new forms  
✅ **Migrate**: Change existing forms to ThemedForm, add DI constructor  
✅ **Apply**: Use IThemeProvider.SetThemeAsync() to change themes  
✅ **Extend**: Create custom IThemeApplier for specialized controls  
✅ **Test**: Write unit and integration tests to verify behavior

**Ready to start refactoring!** 🎨
