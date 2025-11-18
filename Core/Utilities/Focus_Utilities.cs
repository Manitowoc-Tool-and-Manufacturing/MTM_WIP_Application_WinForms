using MTM_WIP_Application_Winforms.Models;
using MTM_WIP_Application_Winforms.Logging;

namespace MTM_WIP_Application_Winforms.Core;

public static class FocusUtils
{
    // Track controls where TextChanged handlers should be active (after first focus)
    private static readonly HashSet<Control> _controlsWithActiveHandlers = new();

    public static void ApplyFocusEventHandling(Control control, Model_Shared_UserUiColors colors)
    {
        Apply(control, colors);
    }

    public static void ApplyFocusEventHandlingToControls(Control.ControlCollection controls,
        Model_Shared_UserUiColors colors)
    {
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


            Model_Shared_UserUiColors colors = Core_AppThemes.GetCurrentTheme().Colors;
            Color focusBackColor = colors.ControlFocusedBackColor ?? Color.LightBlue;


            ctrl.BackColor = focusBackColor;

            switch (ctrl)
            {
                case TextBox tb:

                    tb.SelectAll();
                    break;
                case MaskedTextBox mtb:

                    mtb.SelectAll();
                    break;
                case RichTextBox rtb:

                    rtb.SelectAll();
                    break;
                case ComboBox cb when cb.DropDownStyle != ComboBoxStyle.DropDownList:

                    cb.SelectAll();
                    break;
            }

            // Attach TextChanged handlers AFTER the current event processing completes
            // This prevents SelectAll() from triggering the handlers and clearing the highlight

            ctrl.BeginInvoke(new Action(() =>
            {
                lock (_controlsWithActiveHandlers)
                {
                    if (!_controlsWithActiveHandlers.Contains(ctrl))
                    {

                        _controlsWithActiveHandlers.Add(ctrl);
                        AttachTextChangedHandlers(ctrl);
                    }
                    else
                    {

                    }
                }
            }));


        }
    }

    private static void Control_LostFocus_Handler(object? sender, EventArgs e)
    {
        if (sender is Control ctrl)
        {


            // Use BeginInvoke to delay the color restoration slightly
            // This prevents the Leave event from clearing the highlight before it's visible
            ctrl.BeginInvoke(new Action(() =>
            {
                // Only restore if control no longer has focus
                if (!ctrl.Focused)
                {


                    Model_Shared_UserUiColors colors = Core_AppThemes.GetCurrentTheme().Colors;
                    Color normalBackColor = GetControlThemeBackColor(ctrl, colors);
                    ctrl.BackColor = normalBackColor;


                }
                else
                {

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


            Model_Shared_UserUiColors colors = Core_AppThemes.GetCurrentTheme().Colors;
            Color normalBackColor = GetControlThemeBackColor(tb, colors);
            tb.BackColor = normalBackColor;


        }
    }

    /// <summary>
    /// Removes focus highlight when user starts typing in a RichTextBox.
    /// </summary>
    private static void RichTextBox_TextChanged_Handler(object? sender, EventArgs e)
    {
        if (sender is RichTextBox rtb)
        {


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


        switch (control)
        {
            case TextBox tb:
                tb.TextChanged += TextBox_TextChanged_Handler;

                break;
            case ComboBox cb:
                cb.TextChanged += ComboBox_TextChanged_Handler;

                break;
            case RichTextBox rtb:
                rtb.TextChanged += RichTextBox_TextChanged_Handler;

                break;
            case MaskedTextBox mtb:
                mtb.TextChanged += MaskedTextBox_TextChanged_Handler;

                break;
            case NumericUpDown nud:
                nud.ValueChanged += NumericUpDown_ValueChanged_Handler;

                break;
            case DateTimePicker dtp:
                dtp.ValueChanged += DateTimePicker_ValueChanged_Handler;

                break;
        }
    }

    private static void Apply(Control control, Model_Shared_UserUiColors colors)
    {


        // Remove existing handlers to prevent duplicates
        // Use GotFocus/LostFocus instead of Enter/Leave because they fire even when focus is set programmatically
        control.GotFocus -= Control_GotFocus_Handler;
        control.LostFocus -= Control_LostFocus_Handler;



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



        // Add Click handlers for SelectAll
        switch (control)
        {
            case TextBox tbx:
                tbx.Click += TextBox_Click_SelectAll;

                break;
            case ComboBox cbx:
                cbx.DropDown += ComboBox_DropDown_SelectAll;

                break;
        }

        // NOTE: TextChanged/ValueChanged handlers are NOT attached here
        // They will be attached on first focus by Control_GotFocus_Handler


    }

    public static bool CanControlReceiveFocus(Control control)
    {
        // Don't check TabStop - controls may have it set to false initially but still need focus highlighting
        // Only check if control is enabled and visible
        if (!control.Enabled || !control.Visible)
        {

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
