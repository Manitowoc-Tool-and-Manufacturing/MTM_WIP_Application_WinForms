using MTM_WIP_Application_Winforms.Services;

namespace MTM_WIP_Application_Winforms.Helpers;

internal class Helper_UI_Shortcuts
{
    #region Shortcut Helper

    /// <summary>
    /// The function `ToShortcutString` in C# converts a `Keys` enum value into a string representation of
    /// keyboard shortcut keys.
    /// </summary>
    /// <param name="Keys">The `ToShortcutString` method you provided is used to convert a `Keys` enum value
    /// into a string representation of a keyboard shortcut. The `Keys` enum represents key codes for
    /// keyboard keys.</param>
    /// <returns>
    /// The `ToShortcutString` method takes a `Keys` enum as input and returns a string representation of
    /// the shortcut keys combination. It checks for modifier keys (Ctrl, Shift, Alt) and the main key
    /// pressed, handling special cases like numeric keys, arrow keys, and common keys like Enter, Delete,
    /// and Escape. The method then joins the parts together with " + " separator and returns the
    /// </returns>
    public static string ToShortcutString(Keys keys)
    {
        if (keys == Keys.None) return "";
        var parts = new List<string>();

        // Modifiers
        if (keys.HasFlag(Keys.Control)) parts.Add("Ctrl");
        if (keys.HasFlag(Keys.Shift)) parts.Add("Shift");
        if (keys.HasFlag(Keys.Alt)) parts.Add("Alt");

        // Remove modifiers to get the main key
        var keyOnly = keys & ~Keys.Control & ~Keys.Shift & ~Keys.Alt;
        if (keyOnly != Keys.None)
        {
            // Handle D0-D9 as "0"-"9"
            if (keyOnly >= Keys.D0 && keyOnly <= Keys.D9)
            {
                parts.Add(((int)keyOnly - (int)Keys.D0).ToString());
            }
            // Handle NumPad0-NumPad9 as "Num0"-"Num9"
            else if (keyOnly >= Keys.NumPad0 && keyOnly <= Keys.NumPad9)
            {
                parts.Add("Num" + ((int)keyOnly - (int)Keys.NumPad0));
            }
            // Handle arrows and other common keys
            else if (keyOnly == Keys.Left) parts.Add("Left");
            else if (keyOnly == Keys.Right) parts.Add("Right");
            else if (keyOnly == Keys.Up) parts.Add("Up");
            else if (keyOnly == Keys.Down) parts.Add("Down");
            else if (keyOnly == Keys.Enter) parts.Add("Enter");
            else if (keyOnly == Keys.Delete) parts.Add("Delete");
            else if (keyOnly == Keys.Escape) parts.Add("Esc");
            else parts.Add(keyOnly.ToString());
        }

        return string.Join(" + ", parts);
    }

    /// <summary>
    /// Gets the display string for a shortcut using the service if available, otherwise falls back to the default keys.
    /// </summary>
    public static string GetShortcutDisplay(string shortcutName, IShortcutService? service, Keys defaultKeys)
    {
        if (service != null)
        {
            var display = service.GetShortcutDisplay(shortcutName);
            if (!string.IsNullOrEmpty(display)) return display;
        }
        return ToShortcutString(defaultKeys);
    }

    #endregion
}
