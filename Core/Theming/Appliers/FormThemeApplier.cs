using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MTM_WIP_Application_Winforms.Core.Theming.Interfaces;
using MTM_WIP_Application_Winforms.Logging;
using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Core.Theming.Appliers;

/// <summary>
/// Applies themes to Form controls and recursively to all child controls.
/// Enhanced with null checks, recursive traversal, and fallback handling (T073-T075).
/// </summary>
public class FormThemeApplier : ThemeApplierBase
{
    public FormThemeApplier(ILogger<FormThemeApplier>? logger) : base(logger!)
    {
    }

    public override bool CanApply(Control control)
    {
        // T073: Can apply to Forms and UserControls (base Control containers)
        return control is Form or UserControl;
    }

    public override void Apply(Control control, Model_Shared_UserUiColors theme)
    {
        // T074: Null check for disposed controls
        if (control == null || control.IsDisposed)
        {
            Logger?.LogDebug("Skipping disposed or null control");
            return;
        }

        if (control is not (Form or UserControl))
        {
            return;
        }

        try
        {
            // Apply theme to the form/user control itself
            // Use ControlBackColor as fallback if FormBackColor is not set
            var backColor = GetColorOrDefault(theme.FormBackColor ?? theme.ControlBackColor, Color.White);
            var foreColor = GetColorOrDefault(theme.FormForeColor ?? theme.ControlForeColor, Color.Black);

            // DEBUG: Log what colors we're actually using



            control.BackColor = backColor;
            control.ForeColor = foreColor;

            Logger?.LogDebug("Applied theme to {ControlType} '{ControlName}' - BackColor: {BackColor}, ForeColor: {ForeColor}",
                control.GetType().Name, control.Name, backColor, foreColor);

            // T073: Recursively traverse all children, grandchildren, etc.
            // Get appliers from ServiceProvider to avoid circular dependency
            var appliers = Program.ServiceProvider?.GetService<IEnumerable<IThemeApplier>>();
            if (appliers != null)
            {
                ApplyThemeToControlHierarchy(control, theme, appliers);
            }
        }
        catch (Exception ex)
        {
            HandleApplyError(ex, control, theme);
        }
    }

    /// <summary>
    /// T073: Recursively traverses and applies theme to all children, grandchildren, etc.
    /// T074: Includes null checks for disposed controls.
    /// T075: Uses fallback applier for controls without matching applier.
    /// </summary>
    private void ApplyThemeToControlHierarchy(Control parent, Model_Shared_UserUiColors theme, IEnumerable<IThemeApplier> appliers)
    {
        if (parent == null || parent.IsDisposed || !parent.HasChildren)
        {
            return;
        }

        foreach (Control child in parent.Controls)
        {
            // T074: Null check for disposed controls - skip gracefully
            if (child == null || child.IsDisposed)
            {
                Logger?.LogDebug("Skipping disposed child control");
                continue;
            }

            try
            {
                // T079: Apply theme even to invisible controls so they're correct when made visible
                // Find matching applier for this control type
                var applier = appliers.FirstOrDefault(a => a != this && a.CanApply(child));

                if (applier != null)
                {
                    // Apply theme using the specific applier (regardless of visibility)
                    applier.Apply(child, theme);
                }
                else
                {
                    // T075: Log controls without matching applier and use fallback
                    Logger?.LogDebug("No specific applier found for {ControlType} '{ControlName}', using common styles fallback",
                        child.GetType().Name, child.Name);

                    // Apply common styles as fallback (regardless of visibility)
                    ApplyCommonStyles(child, theme);
                }

                // Recursively apply to children (all levels deep, even if parent invisible)
                if (child.HasChildren)
                {
                    ApplyThemeToControlHierarchy(child, theme, appliers);
                }
            }
            catch (ObjectDisposedException)
            {
                // T074: Control was disposed during traversal - skip gracefully
                Logger?.LogDebug("Control '{ControlName}' was disposed during theme application", child.Name);
            }
            catch (Exception ex)
            {
                // Log error but continue with other controls
                LoggingUtility.LogApplicationError(ex);
                Logger?.LogError(ex, "Error applying theme to child control '{ControlName}' of type {ControlType}",
                    child.Name, child.GetType().Name);
            }
        }
    }
}
