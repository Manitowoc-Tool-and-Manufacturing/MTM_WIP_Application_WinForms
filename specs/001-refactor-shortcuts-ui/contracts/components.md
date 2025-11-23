# Component Contracts

## Control_SettingsCollapsibleCard

Public API for the new UI component.

```csharp
public partial class Control_SettingsCollapsibleCard : ThemedUserControl
{
    // Properties
    public string CardTitle { get; set; }
    public string CardDescription { get; set; }
    public string CardIcon { get; set; }
    public bool IsExpanded { get; set; }
    
    // Methods
    public void AddContent(Control content);
    public void ClearContent();
    
    // Events
    public event EventHandler<bool> ExpandedChanged;
}
```

## Service_Shortcut

Updates to the existing service contract.

```csharp
public interface IShortcutService
{
    // Existing
    Task InitializeAsync(string userName);
    Keys GetShortcutKey(string shortcutName);
    string GetShortcutDisplay(string shortcutName);
    Task<bool> UpdateShortcutAsync(string shortcutName, Keys newKeys);
    Task<bool> ResetToDefaultsAsync();
    List<Model_Shortcut> GetAllShortcuts();
    
    // New / Refined
    bool IsReservedKey(Keys keyData); // Checks for QuickButton reservation
    bool IsDuplicate(Keys keyData, string excludeShortcutName);
}
```
