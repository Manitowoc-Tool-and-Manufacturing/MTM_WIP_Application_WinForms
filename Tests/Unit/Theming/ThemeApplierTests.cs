using MTM_WIP_Application_Winforms.Core.Theming.Appliers;
using MTM_WIP_Application_Winforms.Core.Theming.Interfaces;
using MTM_WIP_Application_Winforms.Models;
using Xunit;

namespace MTM_WIP_Application_Winforms.Tests.Unit.Theming;

/// <summary>
/// Unit tests for theme applier implementations.
/// Tests T070-T071: Verify control type coverage and applier matching.
/// </summary>
public class ThemeApplierTests
{
    private readonly Model_Shared_UserUiColors _testTheme;
    private readonly List<IThemeApplier> _allAppliers;

    public ThemeApplierTests()
    {
        // Create a test theme with distinct colors for verification
        _testTheme = new Model_Shared_UserUiColors
        {
            ControlBackColor = Color.FromArgb(10, 10, 10),
            ControlForeColor = Color.FromArgb(20, 20, 20),
            ButtonBackColor = Color.FromArgb(30, 30, 30),
            ButtonForeColor = Color.FromArgb(40, 40, 40),
            TextBoxBackColor = Color.FromArgb(50, 50, 50),
            TextBoxForeColor = Color.FromArgb(60, 60, 60),
            DataGridBackColor = Color.FromArgb(70, 70, 70),
            DataGridForeColor = Color.FromArgb(80, 80, 80)
        };

        // Instantiate all theme appliers
        _allAppliers = new List<IThemeApplier>
        {
            new ButtonThemeApplier(null),
            new CheckBoxThemeApplier(null),
            new ComboBoxThemeApplier(null),
            new DataGridThemeApplier(null),
            new FormThemeApplier(null),
            new GroupBoxThemeApplier(null),
            new LabelThemeApplier(null),
            new ListBoxThemeApplier(null),
            new MenuStripThemeApplier(null),
            new PanelThemeApplier(null),
            new RadioButtonThemeApplier(null),
            new SplitContainerThemeApplier(null),
            new StatusStripThemeApplier(null),
            new TabControlThemeApplier(null),
            new TextBoxThemeApplier(null),
            new ToolStripThemeApplier(null),
            new TreeViewThemeApplier(null)
        };
    }

    /// <summary>
    /// T070: Verify each applier's CanApply returns true for its target control type.
    /// </summary>
    [Fact]
    public void CanApply_ForEachControlType_ShouldReturnTrueForCorrectType()
    {
        // Arrange & Act & Assert
        Assert.True(new ButtonThemeApplier(null).CanApply(new Button()));
        Assert.True(new CheckBoxThemeApplier(null).CanApply(new CheckBox()));
        Assert.True(new ComboBoxThemeApplier(null).CanApply(new ComboBox()));
        Assert.True(new DataGridThemeApplier(null).CanApply(new DataGridView()));
        Assert.True(new FormThemeApplier(null).CanApply(new Form()));
        Assert.True(new GroupBoxThemeApplier(null).CanApply(new GroupBox()));
        Assert.True(new LabelThemeApplier(null).CanApply(new Label()));
        Assert.True(new ListBoxThemeApplier(null).CanApply(new ListBox()));
        Assert.True(new MenuStripThemeApplier(null).CanApply(new MenuStrip()));
        Assert.True(new PanelThemeApplier(null).CanApply(new Panel()));
        Assert.True(new RadioButtonThemeApplier(null).CanApply(new RadioButton()));
        Assert.True(new SplitContainerThemeApplier(null).CanApply(new SplitContainer()));
        Assert.True(new StatusStripThemeApplier(null).CanApply(new StatusStrip()));
        Assert.True(new TabControlThemeApplier(null).CanApply(new TabControl()));
        Assert.True(new TextBoxThemeApplier(null).CanApply(new TextBox()));
        Assert.True(new ToolStripThemeApplier(null).CanApply(new ToolStrip()));
        Assert.True(new TreeViewThemeApplier(null).CanApply(new TreeView()));
    }

    /// <summary>
    /// T070: Verify appliers return false for incompatible control types.
    /// </summary>
    [Fact]
    public void CanApply_ForWrongControlType_ShouldReturnFalse()
    {
        // Arrange
        var buttonApplier = new ButtonThemeApplier(null);
        var textBox = new TextBox();

        // Act
        var result = buttonApplier.CanApply(textBox);

        // Assert
        Assert.False(result);
    }

    /// <summary>
    /// T071: Verify all common WinForms control types have a matching applier.
    /// </summary>
    [Theory]
    [InlineData(typeof(Button))]
    [InlineData(typeof(CheckBox))]
    [InlineData(typeof(ComboBox))]
    [InlineData(typeof(DataGridView))]
    [InlineData(typeof(Form))]
    [InlineData(typeof(GroupBox))]
    [InlineData(typeof(Label))]
    [InlineData(typeof(ListBox))]
    [InlineData(typeof(MenuStrip))]
    [InlineData(typeof(Panel))]
    [InlineData(typeof(RadioButton))]
    [InlineData(typeof(SplitContainer))]
    [InlineData(typeof(StatusStrip))]
    [InlineData(typeof(TabControl))]
    [InlineData(typeof(TextBox))]
    [InlineData(typeof(ToolStrip))]
    [InlineData(typeof(TreeView))]
    public void AllControlTypes_ShouldHaveApplier(Type controlType)
    {
        // Arrange
        var control = (Control)Activator.CreateInstance(controlType)!;

        // Act
        var matchingApplier = _allAppliers.FirstOrDefault(a => a.CanApply(control));

        // Assert
        Assert.NotNull(matchingApplier);
    }

    /// <summary>
    /// T071: Verify UserControl and custom controls have fallback handling.
    /// </summary>
    [Fact]
    public void UserControl_ShouldHaveFormApplierAsFallback()
    {
        // Arrange
        var userControl = new UserControl();

        // Act
        var matchingApplier = _allAppliers.FirstOrDefault(a => a.CanApply(userControl));

        // Assert - FormThemeApplier should handle UserControl as a fallback
        Assert.NotNull(matchingApplier);
        Assert.IsType<FormThemeApplier>(matchingApplier);
    }

    /// <summary>
    /// Verify Button applier correctly applies theme colors.
    /// </summary>
    [Fact]
    public void ButtonThemeApplier_ShouldApplyCorrectColors()
    {
        // Arrange
        var button = new Button();
        var applier = new ButtonThemeApplier(null);

        // Act
        applier.Apply(button, _testTheme);

        // Assert
        Assert.Equal(_testTheme.ButtonBackColor, button.BackColor);
        Assert.Equal(_testTheme.ButtonForeColor, button.ForeColor);
    }

    /// <summary>
    /// Verify TextBox applier correctly applies theme colors.
    /// </summary>
    [Fact]
    public void TextBoxThemeApplier_ShouldApplyCorrectColors()
    {
        // Arrange
        var textBox = new TextBox();
        var applier = new TextBoxThemeApplier(null);

        // Act
        applier.Apply(textBox, _testTheme);

        // Assert
        Assert.Equal(_testTheme.TextBoxBackColor, textBox.BackColor);
        Assert.Equal(_testTheme.TextBoxForeColor, textBox.ForeColor);
    }

    /// <summary>
    /// Verify DataGridView applier correctly applies theme colors.
    /// </summary>
    [Fact]
    public void DataGridThemeApplier_ShouldApplyCorrectColors()
    {
        // Arrange
        var dataGrid = new DataGridView();
        var applier = new DataGridThemeApplier(null);

        // Act
        applier.Apply(dataGrid, _testTheme);

        // Assert
        Assert.Equal(_testTheme.DataGridBackColor, dataGrid.BackgroundColor);
        Assert.Equal(_testTheme.DataGridForeColor, dataGrid.ForeColor);
    }

    /// <summary>
    /// Verify appliers handle null theme gracefully.
    /// </summary>
    [Fact]
    public void Apply_WithNullTheme_ShouldNotThrow()
    {
        // Arrange
        var button = new Button();
        var applier = new ButtonThemeApplier(null);

        // Act & Assert - Should not throw
        var exception = Record.Exception(() => applier.Apply(button, null!));
        Assert.Null(exception);
    }

    /// <summary>
    /// Verify appliers handle disposed controls gracefully.
    /// </summary>
    [Fact]
    public void Apply_WithDisposedControl_ShouldNotThrow()
    {
        // Arrange
        var button = new Button();
        button.Dispose();
        var applier = new ButtonThemeApplier(null);

        // Act & Assert - Should not throw
        var exception = Record.Exception(() => applier.Apply(button, _testTheme));
        Assert.Null(exception);
    }
}
