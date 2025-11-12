using MTM_WIP_Application_Winforms.Models;

namespace MTM_WIP_Application_Winforms.Core;

public static class FocusUtils
{
    public static void ApplyFocusEventHandling(Control control, Model_Shared_UserUiColors colors)
    {
        if (!CanControlReceiveFocus(control))
        {
            return;
        }

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

    private static void Control_Enter_Handler(object? sender, EventArgs e)
    {
        if (sender is Control ctrl && ctrl.Focused)
        {
            Model_Shared_UserUiColors colors = Core_AppThemes.GetCurrentTheme().Colors;
            Color focusBackColor = colors.ControlFocusedBackColor ?? Color.LightBlue;
            ctrl.BackColor = focusBackColor;

            switch (ctrl)
            {
                case TextBox tb: tb.SelectAll(); break;
                case MaskedTextBox mtb: mtb.SelectAll(); break;
                case RichTextBox rtb: rtb.SelectAll(); break;
                case ComboBox cb when cb.DropDownStyle != ComboBoxStyle.DropDownList: cb.SelectAll(); break;
            }
        }
    }

    private static void Control_Leave_Handler(object? sender, EventArgs e)
    {
        if (sender is Control ctrl)
        {
            Model_Shared_UserUiColors colors = Core_AppThemes.GetCurrentTheme().Colors;
            Color normalBackColor = GetControlThemeBackColor(ctrl, colors);
            ctrl.BackColor = normalBackColor;
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

    private static void Apply(Control control, Model_Shared_UserUiColors colors)
    {
        control.Enter -= Control_Enter_Handler;
        control.Leave -= Control_Leave_Handler;
        if (control is TextBox tb)
        {
            tb.Click -= TextBox_Click_SelectAll;
        }

        if (control is ComboBox cb)
        {
            cb.DropDown -= ComboBox_DropDown_SelectAll;
        }

        control.Enter += Control_Enter_Handler;
        control.Leave += Control_Leave_Handler;
        if (control is TextBox tbx)
        {
            tbx.Click += TextBox_Click_SelectAll;
        }

        if (control is ComboBox cbx)
        {
            cbx.DropDown += ComboBox_DropDown_SelectAll;
        }
    }

    public static bool CanControlReceiveFocus(Control control)
    {
        if (!control.Enabled || !control.Visible || !control.TabStop)
        {
            return false;
        }

        return control switch
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
