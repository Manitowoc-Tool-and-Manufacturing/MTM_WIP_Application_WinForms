using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Logging;

namespace MTM_WIP_Application_Winforms.Core;

public static class FocusUtils
{
    // Track controls where TextChanged handlers should be active (after first focus)
    private static readonly HashSet<Control> _controlsWithActiveHandlers = new();

    public static void ApplyFocusEventHandling(Control control, Model_Shared_UserUiColors colors)
    {
        if (!CanControlReceiveFocus(control))
        {
            LoggingUtility.Log($"[FocusUtils] ApplyFocusEventHandling: {control.Name} ({control.GetType().Name}) - SKIPPED (CanControlReceiveFocus=false)");
            return;
        }

        LoggingUtility.Log($"[FocusUtils] ApplyFocusEventHandling: {control.Name} ({control.GetType().Name}) - APPLYING");
        Apply(control, colors);
    }

    public static void ApplyFocusEventHandlingToControls(Control.ControlCollection controls,
        Model_Shared_UserUiColors colors)
    {
        LoggingUtility.Log($"[FocusUtils] ApplyFocusEventHandlingToControls: Processing {controls.Count} controls");
        
        foreach (Control ctrl in controls)
        {
            ApplyFocusEventHandling(ctrl, colors);
            if (ctrl.HasChildren)
            {
                ApplyFocusEventHandlingToControls(ctrl.Controls, colors);
            }
        }
    }

    private static void Control_GotFocus_Handler(object? sender, EventArgs e)
    {
        if (sender is Control ctrl && ctrl.Focused)
        {
            LoggingUtility.Log($"[FocusUtils] Control_GotFocus_Handler: {ctrl.Name} ({ctrl.GetType().Name}) - ENTERING");
            
            Model_Shared_UserUiColors colors = Core_AppThemes.GetCurrentTheme().Colors;
            Color focusBackColor = colors.ControlFocusedBackColor ?? Color.LightBlue;
            
            LoggingUtility.Log($"[FocusUtils] Setting BackColor to: {focusBackColor} for {ctrl.Name}");
            ctrl.BackColor = focusBackColor;

            switch (ctrl)
            {
                case TextBox tb: 
                    LoggingUtility.Log($"[FocusUtils] Calling SelectAll on TextBox: {tb.Name}");
                    tb.SelectAll(); 
                    break;
                case MaskedTextBox mtb: 
                    LoggingUtility.Log($"[FocusUtils] Calling SelectAll on MaskedTextBox: {mtb.Name}");
                    mtb.SelectAll(); 
                    break;
                case RichTextBox rtb: 
                    LoggingUtility.Log($"[FocusUtils] Calling SelectAll on RichTextBox: {rtb.Name}");
                    rtb.SelectAll(); 
                    break;
                case ComboBox cb when cb.DropDownStyle != ComboBoxStyle.DropDownList: 
                    LoggingUtility.Log($"[FocusUtils] Calling SelectAll on ComboBox: {cb.Name}");
                    cb.SelectAll(); 
                    break;
            }

            // Attach TextChanged handlers AFTER the current event processing completes
            // This prevents SelectAll() from triggering the handlers and clearing the highlight
            LoggingUtility.Log($"[FocusUtils] Queueing BeginInvoke to attach handlers for {ctrl.Name}");
            ctrl.BeginInvoke(new Action(() =>
            {
                lock (_controlsWithActiveHandlers)
                {
                    if (!_controlsWithActiveHandlers.Contains(ctrl))
                    {
                        LoggingUtility.Log($"[FocusUtils] BeginInvoke executing: Attaching TextChanged handlers for {ctrl.Name}");
                        _controlsWithActiveHandlers.Add(ctrl);
                        AttachTextChangedHandlers(ctrl);
                    }
                    else
                    {
                        LoggingUtility.Log($"[FocusUtils] BeginInvoke executing: Handlers already attached for {ctrl.Name}");
                    }
                }
            }));
            
            LoggingUtility.Log($"[FocusUtils] Control_GotFocus_Handler: {ctrl.Name} - EXITING");
        }
    }

    private static void Control_LostFocus_Handler(object? sender, EventArgs e)
    {
        if (sender is Control ctrl)
        {
            LoggingUtility.Log($"[FocusUtils] Control_LostFocus_Handler: {ctrl.Name} - Checking if should restore normal BackColor");
            
            // Use BeginInvoke to delay the color restoration slightly
            // This prevents the Leave event from clearing the highlight before it's visible
            ctrl.BeginInvoke(new Action(() =>
            {
                // Only restore if control no longer has focus
                if (!ctrl.Focused)
                {
                    LoggingUtility.Log($"[FocusUtils] Control_LostFocus_Handler: {ctrl.Name} - Control not focused, restoring normal BackColor");
                    
                    Model_Shared_UserUiColors colors = Core_AppThemes.GetCurrentTheme().Colors;
                    Color normalBackColor = GetControlThemeBackColor(ctrl, colors);
                    ctrl.BackColor = normalBackColor;
                    
                    LoggingUtility.Log($"[FocusUtils] BackColor set to: {normalBackColor} for {ctrl.Name}");
                }
                else
                {
                    LoggingUtility.Log($"[FocusUtils] Control_LostFocus_Handler: {ctrl.Name} - Control still focused, keeping highlight");
                }
            }));
        }
    }

    /// <summary>
    /// Removes focus highlight when user starts typing in a TextBox.
    /// </summary>
    private static void TextBox_TextChanged_Handler(object? sender, EventArgs e)
    {
        if (sender is TextBox tb)
        {
            LoggingUtility.Log($"[FocusUtils] TextBox_TextChanged_Handler: {tb.Name} - Clearing highlight");
            
            Model_Shared_UserUiColors colors = Core_AppThemes.GetCurrentTheme().Colors;
            Color normalBackColor = GetControlThemeBackColor(tb, colors);
            tb.BackColor = normalBackColor;
            
            LoggingUtility.Log($"[FocusUtils] BackColor set to: {normalBackColor} for {tb.Name}");
        }
    }

    /// <summary>
    /// Removes focus highlight when user starts typing in a RichTextBox.
    /// </summary>
    private static void RichTextBox_TextChanged_Handler(object? sender, EventArgs e)
    {
        if (sender is RichTextBox rtb)
        {
            LoggingUtility.Log($"[FocusUtils] RichTextBox_TextChanged_Handler: {rtb.Name} - Clearing highlight");
            
            Model_Shared_UserUiColors colors = Core_AppThemes.GetCurrentTheme().Colors;
            Color normalBackColor = GetControlThemeBackColor(rtb, colors);
            rtb.BackColor = normalBackColor;
        }
    }

    /// <summary>
    /// Removes focus highlight when user starts typing in a MaskedTextBox.
    /// </summary>
    private static void MaskedTextBox_TextChanged_Handler(object? sender, EventArgs e)
    {
        if (sender is MaskedTextBox mtb)
        {
            LoggingUtility.Log($"[FocusUtils] MaskedTextBox_TextChanged_Handler: {mtb.Name} - Clearing highlight");
            
            Model_Shared_UserUiColors colors = Core_AppThemes.GetCurrentTheme().Colors;
            Color normalBackColor = GetControlThemeBackColor(mtb, colors);
            mtb.BackColor = normalBackColor;
        }
    }

    /// <summary>
    /// Removes focus highlight when user changes selection in a ComboBox.
    /// </summary>
    private static void ComboBox_TextChanged_Handler(object? sender, EventArgs e)
    {
        if (sender is ComboBox cb)
        {
            LoggingUtility.Log($"[FocusUtils] ComboBox_TextChanged_Handler: {cb.Name} - Clearing highlight");
            
            Model_Shared_UserUiColors colors = Core_AppThemes.GetCurrentTheme().Colors;
            Color normalBackColor = GetControlThemeBackColor(cb, colors);
            cb.BackColor = normalBackColor;
        }
    }

    /// <summary>
    /// Removes focus highlight when user changes value in a NumericUpDown.
    /// </summary>
    private static void NumericUpDown_ValueChanged_Handler(object? sender, EventArgs e)
    {
        if (sender is NumericUpDown nud)
        {
            LoggingUtility.Log($"[FocusUtils] NumericUpDown_ValueChanged_Handler: {nud.Name} - Clearing highlight");
            
            Model_Shared_UserUiColors colors = Core_AppThemes.GetCurrentTheme().Colors;
            Color normalBackColor = GetControlThemeBackColor(nud, colors);
            nud.BackColor = normalBackColor;
        }
    }

    /// <summary>
    /// Removes focus highlight when user changes value in a DateTimePicker.
    /// </summary>
    private static void DateTimePicker_ValueChanged_Handler(object? sender, EventArgs e)
    {
        if (sender is DateTimePicker dtp)
        {
            LoggingUtility.Log($"[FocusUtils] DateTimePicker_ValueChanged_Handler: {dtp.Name} - Clearing highlight");
            
            Model_Shared_UserUiColors colors = Core_AppThemes.GetCurrentTheme().Colors;
            Color normalBackColor = GetControlThemeBackColor(dtp, colors);
            dtp.BackColor = normalBackColor;
        }
    }

    private static void TextBox_Click_SelectAll(object? sender, EventArgs e)
    {
        if (sender is TextBox tb)
        {
            tb.SelectAll();
        }
    }

    private static void ComboBox_DropDown_SelectAll(object? sender, EventArgs e)
    {
        if (sender is ComboBox cb && cb.DropDownStyle != ComboBoxStyle.DropDownList)
        {
            cb.SelectAll();
        }
    }

    /// <summary>
    /// Temporarily detaches TextChanged/ValueChanged handlers during SelectAll to prevent highlight clearing.
    /// </summary>
    private static void DetachTextChangedHandlers(Control control)
    {
        switch (control)
        {
            case TextBox tb:
                tb.TextChanged -= TextBox_TextChanged_Handler;
                break;
            case ComboBox cb:
                cb.TextChanged -= ComboBox_TextChanged_Handler;
                break;
            case RichTextBox rtb:
                rtb.TextChanged -= RichTextBox_TextChanged_Handler;
                break;
            case MaskedTextBox mtb:
                mtb.TextChanged -= MaskedTextBox_TextChanged_Handler;
                break;
            case NumericUpDown nud:
                nud.ValueChanged -= NumericUpDown_ValueChanged_Handler;
                break;
            case DateTimePicker dtp:
                dtp.ValueChanged -= DateTimePicker_ValueChanged_Handler;
                break;
        }
    }

    /// <summary>
    /// Attaches TextChanged/ValueChanged handlers to clear highlight when user types.
    /// </summary>
    private static void AttachTextChangedHandlers(Control control)
    {
        LoggingUtility.Log($"[FocusUtils] AttachTextChangedHandlers: Attaching to {control.Name} ({control.GetType().Name})");
        
        switch (control)
        {
            case TextBox tb:
                tb.TextChanged += TextBox_TextChanged_Handler;
                LoggingUtility.Log($"[FocusUtils] Attached TextChanged to TextBox: {tb.Name}");
                break;
            case ComboBox cb:
                cb.TextChanged += ComboBox_TextChanged_Handler;
                LoggingUtility.Log($"[FocusUtils] Attached TextChanged to ComboBox: {cb.Name}");
                break;
            case RichTextBox rtb:
                rtb.TextChanged += RichTextBox_TextChanged_Handler;
                LoggingUtility.Log($"[FocusUtils] Attached TextChanged to RichTextBox: {rtb.Name}");
                break;
            case MaskedTextBox mtb:
                mtb.TextChanged += MaskedTextBox_TextChanged_Handler;
                LoggingUtility.Log($"[FocusUtils] Attached TextChanged to MaskedTextBox: {mtb.Name}");
                break;
            case NumericUpDown nud:
                nud.ValueChanged += NumericUpDown_ValueChanged_Handler;
                LoggingUtility.Log($"[FocusUtils] Attached ValueChanged to NumericUpDown: {nud.Name}");
                break;
            case DateTimePicker dtp:
                dtp.ValueChanged += DateTimePicker_ValueChanged_Handler;
                LoggingUtility.Log($"[FocusUtils] Attached ValueChanged to DateTimePicker: {dtp.Name}");
                break;
        }
    }

    private static void Apply(Control control, Model_Shared_UserUiColors colors)
    {
        LoggingUtility.Log($"[FocusUtils] Apply: Starting for {control.Name} ({control.GetType().Name})");
        
        // Remove existing handlers to prevent duplicates
        // Use GotFocus/LostFocus instead of Enter/Leave because they fire even when focus is set programmatically
        control.GotFocus -= Control_GotFocus_Handler;
        control.LostFocus -= Control_LostFocus_Handler;
        
        LoggingUtility.Log($"[FocusUtils] Apply: Removed old GotFocus/LostFocus handlers for {control.Name}");
        
        // Remove control-specific handlers (in case they were attached)
        DetachTextChangedHandlers(control);
        
        // Remove Click handlers
        switch (control)
        {
            case TextBox tb:
                tb.Click -= TextBox_Click_SelectAll;
                break;
            case ComboBox cb:
                cb.DropDown -= ComboBox_DropDown_SelectAll;
                break;
        }

        // Add focus event handlers (GotFocus/LostFocus work with programmatic focus changes)
        control.GotFocus += Control_GotFocus_Handler;
        control.LostFocus += Control_LostFocus_Handler;
        
        LoggingUtility.Log($"[FocusUtils] Apply: Attached GotFocus/LostFocus handlers for {control.Name}");
        
        // Add Click handlers for SelectAll
        switch (control)
        {
            case TextBox tbx:
                tbx.Click += TextBox_Click_SelectAll;
                LoggingUtility.Log($"[FocusUtils] Apply: Attached Click handler for TextBox {tbx.Name}");
                break;
            case ComboBox cbx:
                cbx.DropDown += ComboBox_DropDown_SelectAll;
                LoggingUtility.Log($"[FocusUtils] Apply: Attached DropDown handler for ComboBox {cbx.Name}");
                break;
        }
        
        // NOTE: TextChanged/ValueChanged handlers are NOT attached here
        // They will be attached on first focus by Control_GotFocus_Handler
        
        LoggingUtility.Log($"[FocusUtils] Apply: Completed for {control.Name}");
    }

    public static bool CanControlReceiveFocus(Control control)
    {
        // Don't check TabStop - controls may have it set to false initially but still need focus highlighting
        // Only check if control is enabled and visible
        if (!control.Enabled || !control.Visible)
        {
            LoggingUtility.Log($"[FocusUtils] CanControlReceiveFocus: {control.Name} - FALSE (Enabled={control.Enabled}, Visible={control.Visible})");
            return false;
        }

        bool canFocus = control switch
        {
            CheckedListBox => false,
            TextBox => true,
            ComboBox => true,
            RichTextBox => true,
            MaskedTextBox => true,
            NumericUpDown => true,
            DateTimePicker => true,
            ListBox => false,
            TreeView => false,
            ListView => false,
            TrackBar => false,
            DomainUpDown => false,
            Button => false,
            CheckBox => false,
            RadioButton => false,
            Label => false,
            Panel => false,
            GroupBox => false,
            PictureBox => false,
            ProgressBar => false,
            _ => false
        };
        
        if (!canFocus)
        {
            LoggingUtility.Log($"[FocusUtils] CanControlReceiveFocus: {control.Name} ({control.GetType().Name}) - FALSE (control type cannot receive focus)");
        }
        
        return canFocus;
    }

    private static Color GetControlThemeBackColor(Control control, Model_Shared_UserUiColors colors) =>
        control switch
        {
            TextBox => colors.TextBoxBackColor ?? colors.ControlBackColor ?? Color.White,
            ComboBox => colors.ComboBoxBackColor ?? colors.ControlBackColor ?? Color.White,
            RichTextBox => colors.RichTextBoxBackColor ?? colors.ControlBackColor ?? Color.White,
            MaskedTextBox => colors.MaskedTextBoxBackColor ?? colors.ControlBackColor ?? Color.White,
            NumericUpDown => colors.NumericUpDownBackColor ?? colors.ControlBackColor ?? Color.White,
            DateTimePicker => colors.DateTimePickerBackColor ?? colors.ControlBackColor ?? Color.White,
            _ => colors.ControlBackColor ?? Color.White
        };

    private static Color GetControlThemeForeColor(Control control, Model_Shared_UserUiColors colors) =>
        control switch
        {
            TextBox => colors.TextBoxForeColor ?? colors.ControlForeColor ?? Color.Black,
            ComboBox => colors.ComboBoxForeColor ?? colors.ControlForeColor ?? Color.Black,
            RichTextBox => colors.RichTextBoxForeColor ?? colors.ControlForeColor ?? Color.Black,
            MaskedTextBox => colors.MaskedTextBoxForeColor ?? colors.ControlForeColor ?? Color.Black,
            NumericUpDown => colors.NumericUpDownForeColor ?? colors.ControlForeColor ?? Color.Black,
            DateTimePicker => colors.DateTimePickerForeColor ?? colors.ControlForeColor ?? Color.Black,
            _ => colors.ControlForeColor ?? Color.Black
        };
}
