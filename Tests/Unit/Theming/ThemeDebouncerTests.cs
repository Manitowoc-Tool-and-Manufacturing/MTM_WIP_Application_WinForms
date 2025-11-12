using Microsoft.Extensions.Logging;
using Moq;
using MTM_WIP_Application_Winforms.Core.Theming;
using Xunit;

namespace MTM_WIP_Application_Winforms.Tests.Unit.Theming;

/// <summary>
/// Unit tests for ThemeDebouncer class.
/// </summary>
public class ThemeDebouncerTests : IDisposable
{
    private readonly Mock<ILogger<ThemeDebouncer>> _mockLogger;
    private readonly ThemeDebouncer _debouncer;
    private bool _disposed;

    public ThemeDebouncerTests()
    {
        _mockLogger = new Mock<ILogger<ThemeDebouncer>>();
        _debouncer = new ThemeDebouncer(_mockLogger.Object, thresholdMs: 100);
    }

    [Fact]
    public async Task RapidChanges_ShouldOnlyApplyFinal()
    {
        // Arrange
        int callbackCount = 0;
        string? appliedTheme = null;

        Task Callback(string themeName, ThemeChangeReason reason, string? userId)
        {
            callbackCount++;
            appliedTheme = themeName;
            return Task.CompletedTask;
        }

        // Act - Trigger 3 rapid changes within threshold
        _debouncer.Debounce("Theme1", ThemeChangeReason.UserSelection, null, Callback);
        await Task.Delay(30);
        _debouncer.Debounce("Theme2", ThemeChangeReason.UserSelection, null, Callback);
        await Task.Delay(30);
        _debouncer.Debounce("Theme3", ThemeChangeReason.UserSelection, null, Callback);

        // Wait for debounce threshold to expire
        await Task.Delay(150);

        // Assert - Only the final theme should be applied
        Assert.Equal(1, callbackCount);
        Assert.Equal("Theme3", appliedTheme);
    }

    [Fact]
    public async Task SlowChanges_ShouldApplyEach()
    {
        // Arrange
        int callbackCount = 0;
        var appliedThemes = new List<string>();

        Task Callback(string themeName, ThemeChangeReason reason, string? userId)
        {
            callbackCount++;
            appliedThemes.Add(themeName);
            return Task.CompletedTask;
        }

        // Act - Trigger changes with delays exceeding threshold
        _debouncer.Debounce("Theme1", ThemeChangeReason.UserSelection, null, Callback);
        await Task.Delay(150); // Wait for threshold

        _debouncer.Debounce("Theme2", ThemeChangeReason.UserSelection, null, Callback);
        await Task.Delay(150);

        // Assert - Both themes should be applied
        Assert.Equal(2, callbackCount);
        Assert.Contains("Theme1", appliedThemes);
        Assert.Contains("Theme2", appliedThemes);
    }

    [Fact]
    public async Task FlushAsync_ShouldApplyImmediately()
    {
        // Arrange
        int callbackCount = 0;
        string? appliedTheme = null;

        Task Callback(string themeName, ThemeChangeReason reason, string? userId)
        {
            callbackCount++;
            appliedTheme = themeName;
            return Task.CompletedTask;
        }

        // Act
        _debouncer.Debounce("FlushTest", ThemeChangeReason.UserSelection, null, Callback);
        await _debouncer.FlushAsync(); // Flush immediately without waiting

        // Assert
        Assert.Equal(1, callbackCount);
        Assert.Equal("FlushTest", appliedTheme);
    }

    public void Dispose()
    {
        if (_disposed) return;

        _debouncer?.Dispose();

        _disposed = true;
        GC.SuppressFinalize(this);
    }
}
