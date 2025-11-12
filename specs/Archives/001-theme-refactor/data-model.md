# Data Model: Theme System Refactoring

**Feature**: Theme System Refactoring  
**Date**: 2025-11-11  
**Status**: Complete

## Overview

This document defines the data entities, relationships, and state transitions for the refactored theme system. The existing database schema remains unchanged - this focuses on the in-memory object model and service layer architecture.

---

## Core Entities

### 1. AppTheme

**Purpose**: Represents a complete visual style configuration

**Source**: Existing class in `Core_AppThemes.cs` - **NO CHANGES REQUIRED**

**Properties**:
```csharp
public class AppTheme
{
    public string Name { get; set; }                      // Unique identifier (e.g., "Dark", "Light", "HighContrast")
    public Model_Shared_UserUiColors Colors { get; set; } // Complete color palette
    public Font? FormFont { get; set; }                   // Optional custom font
    public DateTime LastModified { get; set; }            // For cache invalidation
}
```

**Validation Rules**:
- Name must be unique across all themes
- Name cannot be null or empty
- Colors object must be fully populated (no null colors for standard controls)
- Maximum 50 themes supported in system

**Relationships**:
- **One-to-Many** with UserThemePreference (one theme → many users)
- **One-to-Many** with FormRegistration (one theme → many active forms)

**State Transitions**:
```
[Loaded from DB] → [Cached in ThemeStore] → [Applied to Controls]
                      ↓
                [Invalidated] → [Reloaded from DB]
```

---

### 2. UserThemePreference

**Purpose**: Associates a user with their selected theme

**Storage**: MySQL table `user_preferences` (existing, unchanged)

**Properties**:
```csharp
public class UserThemePreference
{
    public string UserId { get; set; }        // User identifier (login name)
    public string ThemeName { get; set; }     // Reference to AppTheme.Name
    public DateTime LastUpdated { get; set; } // Timestamp of last change
}
```

**Validation Rules**:
- UserId must exist in user system
- ThemeName must reference valid AppTheme
- One preference per user (UserId is primary key)
- Defaults to "Default" theme if no preference stored

**Relationships**:
- **Many-to-One** with AppTheme (many users → one theme)

**State Transitions**:
```
[No Preference] → [User Selects Theme] → [Saved to DB] → [Applied on Login]
                                              ↓
                                         [Changed] → [Re-saved] → [Applied Immediately]
```

---

### 3. FormRegistration

**Purpose**: Tracks which forms are currently subscribed to theme change notifications

**Storage**: In-memory only (transient, lifetime of application session)

**Properties**:
```csharp
public class FormRegistration
{
    public WeakReference<Form> FormReference { get; set; } // Weak reference prevents memory leaks
    public string FormName { get; set; }                   // For logging/debugging
    public AppTheme CurrentTheme { get; set; }             // Last applied theme (for caching)
    public DateTime SubscribedAt { get; set; }             // Subscription timestamp
    public int UpdateCount { get; set; }                   // How many theme updates received
}
```

**Validation Rules**:
- FormReference must be valid (not collected by GC)
- CurrentTheme must match ThemeManager.CurrentTheme after updates
- Cleanup required when form disposed

**Relationships**:
- **Many-to-One** with ThemeManager (many forms → one manager)
- **Many-to-One** with AppTheme (many forms → one active theme)

**State Transitions**:
```
[Form Created] → [Subscribes to ThemeManager] → [Receives Initial Theme]
                                                      ↓
                                                [Theme Changed] → [Receives Update]
                                                      ↓
                                                [Form Disposed] → [Unsubscribes] → [Removed from Registration]
```

---

### 4. ControlThemeMapping

**Purpose**: Defines which IThemeApplier strategy applies to each control type

**Storage**: In-memory dictionary populated during DI registration

**Properties**:
```csharp
public class ControlThemeMapping
{
    public Type ControlType { get; set; }           // e.g., typeof(DataGridView)
    public Type ApplierType { get; set; }           // e.g., typeof(DataGridThemeApplier)
    public IThemeApplier ApplierInstance { get; set; } // Singleton instance from DI
    public int Priority { get; set; }               // For inheritance resolution
}
```

**Validation Rules**:
- ControlType must inherit from System.Windows.Forms.Control
- ApplierType must implement IThemeApplier
- No duplicate mappings for same ControlType
- Priority used when multiple appliers could match (e.g., Button : Control)

**Relationships**:
- **Managed By** ThemeManager (composition)
- **One-to-One** with IThemeApplier implementation

**Lookup Algorithm**:
```csharp
// 1. Exact type match (fastest)
if (_mappings.TryGetValue(control.GetType(), out var applier))
    return applier;

// 2. Walk inheritance hierarchy
Type? baseType = control.GetType().BaseType;
while (baseType != null && baseType != typeof(object))
{
    if (_mappings.TryGetValue(baseType, out applier))
    {
        // Cache for future lookups
        _mappings[control.GetType()] = applier;
        return applier;
    }
    baseType = baseType.BaseType;
}

// 3. Fallback to default applier
return _defaultApplier;
```

---

### 5. ThemeChangeEvent

**Purpose**: Encapsulates theme change notification with metadata

**Storage**: Transient (event args, lifetime of event propagation)

**Properties**:
```csharp
public class ThemeChangedEventArgs : EventArgs
{
    public AppTheme OldTheme { get; init; }         // Previous theme
    public AppTheme NewTheme { get; init; }         // New theme being applied
    public string UserId { get; init; }             // User who initiated change
    public DateTime ChangedAt { get; init; }        // Timestamp of change
    public ThemeChangeReason Reason { get; init; }  // Why theme changed
}

public enum ThemeChangeReason
{
    UserSelection,      // User explicitly chose new theme
    Login,              // Theme loaded on user login
    DpiChange,          // DPI scaling triggered re-application
    SystemDefault,      // Fallback to default theme (error recovery)
    Preview             // Preview window theme change
}
```

**Validation Rules**:
- OldTheme and NewTheme must be valid AppTheme instances
- UserId must match currently logged-in user (except SystemDefault)
- ChangedAt must be current timestamp (within 1 second)

**Relationships**:
- **Published By** ThemeManager
- **Consumed By** All subscribed forms/controls

---

## Entity Relationships Diagram

```
┌─────────────────┐         ┌──────────────────────┐
│   AppTheme      │◄────────│ UserThemePreference  │
│                 │ 1     * │                      │
│ - Name          │         │ - UserId             │
│ - Colors        │         │ - ThemeName          │
│ - FormFont      │         └──────────────────────┘
└────────┬────────┘
         │ 1
         │
         │ *
┌────────▼────────┐         ┌──────────────────────┐
│ FormRegistration│         │  ThemeManager        │
│                 │ *     1 │                      │
│ - FormRef       ├────────►│ - CurrentTheme       │
│ - CurrentTheme  │         │ - Subscribers        │
│ - UpdateCount   │         │ - Debouncer          │
└─────────────────┘         └──────────┬───────────┘
                                       │ 1
                                       │
                                       │ *
                            ┌──────────▼───────────┐
                            │ ControlThemeMapping  │
                            │                      │
                            │ - ControlType        │
                            │ - ApplierInstance    │
                            └──────────────────────┘
```

---

## Data Flow Diagrams

### Theme Change Flow

```
User Clicks Theme →  ThemeManager.SetThemeAsync(themeName)
                              ↓
                     Debouncer.DebounceChange(300ms)
                              ↓
                     ThemeStore.GetThemeAsync(themeName)
                              ↓
                     Database Query (Dao_User)
                              ↓
                     AppTheme Loaded & Cached
                              ↓
                     ThemeChanged Event Raised
                              ↓
            ┌─────────────────┴─────────────────┐
            ↓                                   ↓
    Form1.OnThemeChanged              Form2.OnThemeChanged
            ↓                                   ↓
    FormThemeApplier.Apply            FormThemeApplier.Apply
            ↓                                   ↓
    Traverse Controls                 Traverse Controls
            ↓                                   ↓
    IThemeApplier.Apply               IThemeApplier.Apply
     (per control type)                (per control type)
            ↓                                   ↓
    Control Colors Updated            Control Colors Updated
            ↓                                   ↓
    form.Invalidate()                 form.Invalidate()
```

### Form Subscription Flow

```
Form Constructor Called
        ↓
ThemedForm Base Constructor
        ↓
ThemeManager.Subscribe(this)
        ↓
FormRegistration Created
        ↓
WeakReference<Form> Stored
        ↓
ThemeChanged Event Subscribed
        ↓
Initial Theme Applied (if visible)
        ↓
Form.Shown Event
        ↓
Apply Current Theme
        ↓
... Form Lifetime ...
        ↓
Form.Dispose() Called
        ↓
ThemeManager.Unsubscribe(this)
        ↓
FormRegistration Removed
        ↓
WeakReference Collected by GC
```

---

## State Machines

### ThemeManager State Machine

```
┌──────────┐
│  Created │
└────┬─────┘
     │ Initialize()
     ↓
┌────┴──────────┐
│  Initialized  │
└───┬───────────┘
    │ SetThemeAsync(name)
    ↓
┌───┴──────────┐
│  Debouncing  │ ◄──┐ Rapid changes reset timer
└───┬──────────┘    │
    │ 300ms elapsed │
    ↓               │
┌───┴──────────┐    │
│  Loading     │────┘ New change request
└───┬──────────┘
    │ Theme loaded
    ↓
┌───┴──────────┐
│  Notifying   │
└───┬──────────┘
    │ Events dispatched
    ↓
┌───┴──────────┐
│   Active     │ ◄──┐ Loop
└───────────────┘    │
    │ Next change    │
    └────────────────┘
```

### Form Lifecycle with Theme Subscription

```
┌──────────────┐
│  Constructed │
└──────┬───────┘
       │ Show()
       ↓
┌──────┴───────┐
│  Subscribing │
└──────┬───────┘
       │ Subscription complete
       ↓
┌──────┴──────────┐
│  Theme Synced   │ ◄──┐ Theme changes
└──────┬──────────┘    │ Receive updates
       │                │
       ↓                │
┌──────┴──────────┐    │
│  Active/Visible │────┘
└──────┬──────────┘
       │ Close() / Dispose()
       ↓
┌──────┴────────────┐
│  Unsubscribing    │
└──────┬────────────┘
       │ Cleanup complete
       ↓
┌──────┴───────┐
│   Disposed   │
└──────────────┘
```

---

## Validation & Constraints

### Business Rules

1. **Theme Uniqueness**: AppTheme.Name must be unique across system (enforced by database)

2. **User Preference Integrity**: UserThemePreference.ThemeName must reference existing AppTheme

3. **Subscription Cleanup**: FormRegistration must be removed when form disposed (memory leak prevention)

4. **Cache Coherence**: FormRegistration.CurrentTheme must match ThemeManager.CurrentTheme after updates

5. **Debounce Timing**: Multiple theme changes within 300ms window result in single application

6. **Weak Reference Validity**: FormRegistration.FormReference must be checked for GC collection before use

### Performance Constraints

1. **Theme Change Latency**: Total time from SetThemeAsync() to all forms updated < 100ms

2. **Memory Overhead**: FormRegistration collection growth < 10% of baseline memory

3. **Event Propagation**: ThemeChanged event dispatch < 10ms

4. **Control Application**: Individual IThemeApplier.Apply() < 5ms per control

5. **Cache Hit Rate**: Theme lookup cache hit rate > 90%

---

## Data Access Patterns

### Read Operations

- **Get User Theme Preference**: `Dao_User.GetThemeNameAsync(userId)` → Model_Dao_Result<string>
- **Load Theme Definition**: `Core_AppThemes.GetTheme(themeName)` → AppTheme
- **List Available Themes**: `Core_AppThemes.GetThemeNames()` → List<string>

### Write Operations

- **Save User Preference**: `Dao_User.SetThemeNameAsync(userId, themeName)` → Model_Dao_Result<bool>
- **Cache Theme**: `ThemeStore._themeCache[themeName] = appTheme` (in-memory)
- **Register Form**: `ThemeManager._subscribers.Add(new FormRegistration(...))` (in-memory)

### Caching Strategy

```csharp
// Two-level cache
public class ThemeStore : IThemeStore
{
    // L1: In-memory theme cache (entire AppTheme objects)
    private readonly Dictionary<string, AppTheme> _themeCache = new();
    
    // L2: Applied theme cache (per-control, last applied)
    private readonly Dictionary<Control, AppTheme> _appliedCache = new();
    
    public async Task<AppTheme> GetThemeAsync(string themeName)
    {
        // Check L1 cache
        if (_themeCache.TryGetValue(themeName, out var cached))
            return cached;
        
        // Load from database
        var theme = Core_AppThemes.GetTheme(themeName);
        if (theme == null)
        {
            await Core_AppThemes.LoadThemesFromDatabaseAsync();
            theme = Core_AppThemes.GetTheme(themeName);
        }
        
        // Populate L1 cache
        if (theme != null)
            _themeCache[themeName] = theme;
        
        return theme ?? Core_AppThemes.GetTheme("Default");
    }
}
```

---

## Migration Considerations

### Data Migration

**NO DATABASE SCHEMA CHANGES REQUIRED**

Existing tables remain unchanged:
- `themes` table structure unchanged
- `user_preferences` table structure unchanged
- Stored procedures unchanged

### Object Model Compatibility

**Backward Compatibility Maintained**:
- Existing `AppTheme` class unchanged
- Existing `Model_Shared_UserUiColors` unchanged
- Existing DAO methods (`Dao_User.GetThemeNameAsync`) unchanged

**New Wrapper Classes**:
- `ThemeStore` wraps `Core_AppThemes` for DI compatibility
- `FormRegistration` is new (in-memory only)
- `ControlThemeMapping` is new (in-memory only)
- `ThemeChangedEventArgs` is new (event args)

---

## Summary

**Key Entities**: 5 (AppTheme, UserThemePreference, FormRegistration, ControlThemeMapping, ThemeChangeEvent)

**Relationships**: 4 primary relationships defining theme ownership, user preferences, form subscriptions, and applier mappings

**State Machines**: 2 (ThemeManager lifecycle, Form subscription lifecycle)

**Data Flow**: Event-driven architecture with observer pattern for automatic propagation

**Storage**: MySQL for theme definitions/preferences, in-memory for transient state

**Performance**: Optimized with two-level caching and weak reference tracking

**All data model design complete** - ready to proceed to contract definitions.
