using MTM_Inventory_Application.Core;
using MTM_Inventory_Application.Helpers;
using MTM_Inventory_Application.Logging;
using MTM_Inventory_Application.Models;
using MTM_Inventory_Application.Services;

namespace MTM_Inventory_Application.Forms.ViewLogs;

/// <summary>
/// Form for viewing application logs with user selection, file browsing, and parsed entry display.
/// Supports filtering, navigation, and export of log entries across Normal, ApplicationError, and DatabaseError formats.
/// </summary>
public partial class ViewApplicationLogsForm : Form
{
    #region Fields

    private List<Model_LogFile> _currentLogFiles = new();
    private List<Model_LogEntry> _currentEntries = new();
    private int _currentEntryIndex = 0;
    private Model_LogFilter _activeFilter = Model_LogFilter.CreateDefault();
    private string? _selectedUsername;
    private Model_LogFile? _selectedLogFile;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the currently selected username for log viewing.
    /// </summary>
    public string? SelectedUsername => _selectedUsername;

    /// <summary>
    /// Gets the currently loaded log file.
    /// </summary>
    public Model_LogFile? SelectedLogFile => _selectedLogFile;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the ViewApplicationLogsForm.
    /// </summary>
    public ViewApplicationLogsForm()
    {
        InitializeComponent();
        Core_Themes.ApplyDpiScaling(this);
        Core_Themes.ApplyRuntimeLayoutAdjustments(this);
        WireUpEvents();
    }

    /// <summary>
    /// Initializes a new instance of the ViewApplicationLogsForm with pre-selected user.
    /// </summary>
    /// <param name="username">Username to pre-select in the user dropdown.</param>
    public ViewApplicationLogsForm(string username) : this()
    {
        _selectedUsername = username;
    }

    #endregion

    #region Initialization

    /// <summary>
    /// Handles form load event. Populates user list asynchronously.
    /// </summary>
    private async void ViewApplicationLogsForm_Load(object sender, EventArgs e)
    {
        try
        {
            await LoadUserListAsync();

            // If username was pre-selected via constructor, select it now
            if (!string.IsNullOrWhiteSpace(_selectedUsername))
            {
                for (int i = 0; i < cmbUsers.Items.Count; i++)
                {
                    if (cmbUsers.Items[i]?.ToString() == _selectedUsername)
                    {
                        cmbUsers.SelectedIndex = i;
                        break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[ViewApplicationLogsForm] Error during form load");
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium, controlName: nameof(ViewApplicationLogsForm));
        }
    }

    /// <summary>
    /// Loads the list of users who have log directories asynchronously.
    /// </summary>
    private async Task LoadUserListAsync()
    {
        try
        {
            cmbUsers.Items.Clear();

            // Get base log directory
            string baseLogDir = Helper_LogPath.GetBaseLogDirectory();

            if (!Directory.Exists(baseLogDir))
            {
                LoggingUtility.Log($"[ViewApplicationLogsForm] Base log directory does not exist: {baseLogDir}");
                Service_ErrorHandler.ShowInformation(
                    "No log directory found. Logs will be created when the application generates its first log entry.",
                    "No Logs Available");
                return;
            }

            // Enumerate user directories asynchronously
            var userDirectories = await Task.Run(() =>
            {
                return Directory.GetDirectories(baseLogDir)
                    .Select(dir => Path.GetFileName(dir))
                    .Where(name => !string.IsNullOrWhiteSpace(name))
                    .OrderBy(name => name)
                    .ToList();
            }).ConfigureAwait(true);

            // Populate combo box on UI thread
            foreach (var username in userDirectories)
            {
                cmbUsers.Items.Add(username);
            }

            LoggingUtility.Log($"[ViewApplicationLogsForm] Loaded {userDirectories.Count} users with log directories");

            if (cmbUsers.Items.Count > 0)
            {
                lblUserCount.Text = $"{cmbUsers.Items.Count} users";
            }
            else
            {
                lblUserCount.Text = "No users found";
            }
        }
        catch (UnauthorizedAccessException ex)
        {
            LoggingUtility.Log($"[ViewApplicationLogsForm] Access denied loading user list");
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium,
                contextData: new Dictionary<string, object> { ["Operation"] = "LoadUserList" },
                controlName: nameof(ViewApplicationLogsForm));
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[ViewApplicationLogsForm] Error loading user list");
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium, controlName: nameof(ViewApplicationLogsForm));
        }
    }

    /// <summary>
    /// Wires up event handlers for form controls.
    /// </summary>
    private void WireUpEvents()
    {
        Load += ViewApplicationLogsForm_Load;
        cmbUsers.SelectedIndexChanged += cmbUsers_SelectedIndexChanged;
        btnRefresh.Click += btnRefresh_Click;
        lstLogFiles.SelectedIndexChanged += lstLogFiles_SelectedIndexChanged;
        btnPrevious.Click += btnPrevious_Click;
        btnNext.Click += btnNext_Click;
        KeyDown += ViewApplicationLogsForm_KeyDown;
        KeyPreview = true; // Enable form-level keyboard handling
    }

    #endregion

    #region File Operations

    /// <summary>
    /// Loads log file entries asynchronously and displays the first entry.
    /// </summary>
    /// <param name="filePath">Absolute path to the log file.</param>
    private async Task LoadLogFileAsync(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            LoggingUtility.Log($"[ViewApplicationLogsForm] LoadLogFileAsync called with empty filePath");
            return;
        }

        try
        {
            _currentEntries.Clear();
            _currentEntryIndex = 0;

            lblStatus.Text = $"Loading entries from {Path.GetFileName(filePath)}...";
            Application.DoEvents();

            // Load first 1000 entries (windowed loading per FR-044)
            var rawEntries = await Service_LogFileReader.LoadEntriesAsync(filePath, 0, 1000).ConfigureAwait(true);

            // Parse raw entries into Model_LogEntry objects
            foreach (var rawEntry in rawEntries)
            {
                var parsedEntry = Service_LogParser.ParseEntry(rawEntry);
                _currentEntries.Add(parsedEntry);
            }

            if (_currentEntries.Count > 0)
            {
                _currentEntryIndex = 0;
                ShowCurrentEntry();
                lblStatus.Text = $"Loaded {_currentEntries.Count} entries from {Path.GetFileName(filePath)}";
            }
            else
            {
                lblStatus.Text = $"No entries found in {Path.GetFileName(filePath)}";
                Service_ErrorHandler.ShowInformation("The selected log file contains no entries.", "Empty File");
            }

            LoggingUtility.Log($"[ViewApplicationLogsForm] Loaded {_currentEntries.Count} entries from: {filePath}");
        }
        catch (UnauthorizedAccessException ex)
        {
            LoggingUtility.Log($"[ViewApplicationLogsForm] Access denied loading file: {filePath}");
            LoggingUtility.LogApplicationError(ex);
            lblStatus.Text = "Access denied";
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium,
                contextData: new Dictionary<string, object> { ["FilePath"] = filePath },
                controlName: nameof(ViewApplicationLogsForm));
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[ViewApplicationLogsForm] Error loading file: {filePath}");
            LoggingUtility.LogApplicationError(ex);
            lblStatus.Text = "Error loading file";
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium, controlName: nameof(ViewApplicationLogsForm));
        }
    }

    /// <summary>
    /// Loads log files for the specified username asynchronously.
    /// </summary>
    /// <param name="username">Username to load log files for.</param>
    private async Task LoadLogFilesAsync(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            LoggingUtility.Log($"[ViewApplicationLogsForm] LoadLogFilesAsync called with empty username");
            return;
        }

        try
        {
            lstLogFiles.Items.Clear();
            _currentLogFiles.Clear();

            lblStatus.Text = $"Loading log files for {username}...";
            Application.DoEvents();

            // Enumerate log files
            _currentLogFiles = await Service_LogFileReader.EnumerateLogFilesAsync(username).ConfigureAwait(true);

            // Group by log type and populate list
            var groupedFiles = _currentLogFiles.GroupBy(f => f.LogType).OrderBy(g => g.Key);

            foreach (var group in groupedFiles)
            {
                // Add group header
                string groupHeader = group.Key switch
                {
                    LogFormat.Normal => "═══ Normal Logs ═══",
                    LogFormat.ApplicationError => "═══ Application Errors ═══",
                    LogFormat.DatabaseError => "═══ Database Errors ═══",
                    _ => "═══ Unknown Format ═══"
                };

                var headerItem = new ListViewItem(groupHeader);
                headerItem.Font = new Font(lstLogFiles.Font, FontStyle.Bold);
                headerItem.ForeColor = Color.DarkBlue;
                headerItem.Tag = null; // No associated file
                lstLogFiles.Items.Add(headerItem);

                // Add files in group
                foreach (var file in group.OrderByDescending(f => f.ModifiedDate))
                {
                    var item = new ListViewItem(new[]
                    {
                        file.FileName,
                        file.ModifiedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                        file.FileSizeDisplay,
                        file.EntryCount?.ToString() ?? "?"
                    });
                    item.Tag = file;
                    lstLogFiles.Items.Add(item);
                }
            }

            lblStatus.Text = $"Loaded {_currentLogFiles.Count} log files for {username}";
            lblFileCount.Text = $"{_currentLogFiles.Count} files";

            LoggingUtility.Log($"[ViewApplicationLogsForm] Loaded {_currentLogFiles.Count} log files for user: {username}");
        }
        catch (UnauthorizedAccessException ex)
        {
            LoggingUtility.Log($"[ViewApplicationLogsForm] Access denied loading log files for: {username}");
            LoggingUtility.LogApplicationError(ex);
            lblStatus.Text = "Access denied";
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium,
                contextData: new Dictionary<string, object> { ["Username"] = username },
                controlName: nameof(ViewApplicationLogsForm));
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[ViewApplicationLogsForm] Error loading log files for: {username}");
            LoggingUtility.LogApplicationError(ex);
            lblStatus.Text = "Error loading files";
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium, controlName: nameof(ViewApplicationLogsForm));
        }
    }

    #endregion

    #region Entry Navigation

    /// <summary>
    /// Displays the current log entry in the entry display panel with formatting enhancements.
    /// </summary>
    private void ShowCurrentEntry()
    {
        if (_currentEntries == null || _currentEntries.Count == 0)
        {
            txtEntryDisplay.Text = "No entries loaded";
            txtEntryDisplay.BackColor = SystemColors.Control;
            return;
        }

        if (_currentEntryIndex < 0 || _currentEntryIndex >= _currentEntries.Count)
        {
            LoggingUtility.Log($"[ViewApplicationLogsForm] Invalid entry index: {_currentEntryIndex}");
            txtEntryDisplay.Text = "Invalid entry index";
            txtEntryDisplay.BackColor = SystemColors.Control;
            return;
        }

        var entry = _currentEntries[_currentEntryIndex];

        // Apply severity color coding (T026)
        ApplySeverityColorCoding(entry);

        // Build formatted display based on log type
        var display = new System.Text.StringBuilder();
        display.AppendLine($"═══ Entry {_currentEntryIndex + 1} of {_currentEntries.Count} ═══");
        display.AppendLine($"Format: {entry.LogType}");
        display.AppendLine();

        switch (entry.LogType)
        {
            case LogFormat.Normal:
                display.AppendLine($"Timestamp: {entry.Timestamp:yyyy-MM-dd HH:mm:ss}");
                display.AppendLine($"Level: {entry.Level ?? "N/A"} {GetEmojiDisplay(entry.Emoji)}"); // T028
                display.AppendLine($"Source: {entry.Source ?? "N/A"}");
                display.AppendLine($"Message: {entry.Message ?? "N/A"}");
                if (!string.IsNullOrWhiteSpace(entry.Details))
                {
                    display.AppendLine($"Details:");
                    display.AppendLine(FormatJsonDetails(entry.Details)); // T027
                }
                break;

            case LogFormat.ApplicationError:
                display.AppendLine($"Timestamp: {entry.Timestamp:yyyy-MM-dd HH:mm:ss}");
                display.AppendLine($"Error Type: {entry.Level ?? "N/A"}");
                display.AppendLine($"Exception: {entry.Message ?? "N/A"}");
                if (!string.IsNullOrWhiteSpace(entry.StackTrace))
                {
                    display.AppendLine();
                    display.AppendLine("Stack Trace:");
                    display.AppendLine(entry.StackTrace);
                }
                break;

            case LogFormat.DatabaseError:
                display.AppendLine($"Timestamp: {entry.Timestamp:yyyy-MM-dd HH:mm:ss}");
                display.AppendLine($"Severity: {entry.Level ?? "N/A"}");
                display.AppendLine($"Message: {entry.Message ?? "N/A"}");
                if (!string.IsNullOrWhiteSpace(entry.StackTrace))
                {
                    display.AppendLine();
                    display.AppendLine("Stack Trace:");
                    display.AppendLine(entry.StackTrace);
                }
                break;

            case LogFormat.Unknown:
            default:
                display.AppendLine($"Raw Entry:");
                display.AppendLine(entry.RawText);
                break;
        }

        txtEntryDisplay.Text = display.ToString();

        UpdateNavigationButtons();

        LoggingUtility.Log($"[ViewApplicationLogsForm] Showing entry {_currentEntryIndex + 1} of {_currentEntries.Count}");
    }

    #endregion

    #region Filtering

    #endregion

    #region Button Clicks

    /// <summary>
    /// Handles Refresh button click. Reloads current user's log files.
    /// </summary>
    private async void btnRefresh_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(_selectedUsername))
        {
            await LoadLogFilesAsync(_selectedUsername);
        }
    }

    /// <summary>
    /// Handles Previous button click. Navigates to the previous log entry.
    /// </summary>
    private void btnPrevious_Click(object? sender, EventArgs e)
    {
        if (_currentEntries == null || _currentEntries.Count == 0)
        {
            return;
        }

        if (_currentEntryIndex > 0)
        {
            _currentEntryIndex--;
            ShowCurrentEntry();
            UpdateNavigationButtons();
        }
    }

    /// <summary>
    /// Handles Next button click. Navigates to the next log entry.
    /// </summary>
    private void btnNext_Click(object? sender, EventArgs e)
    {
        if (_currentEntries == null || _currentEntries.Count == 0)
        {
            return;
        }

        if (_currentEntryIndex < _currentEntries.Count - 1)
        {
            _currentEntryIndex++;
            ShowCurrentEntry();
            UpdateNavigationButtons();
        }
    }

    /// <summary>
    /// Handles form-level keyboard shortcuts including Ctrl+C for copy.
    /// </summary>
    private void ViewApplicationLogsForm_KeyDown(object? sender, KeyEventArgs e)
    {
        // Ctrl+C - Copy current entry to clipboard
        if (e.Control && e.KeyCode == Keys.C)
        {
            CopyCurrentEntry();
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
    }

    #endregion

    #region ComboBox & UI Events

    /// <summary>
    /// Handles user selection change event. Loads log files for selected user.
    /// </summary>
    private async void cmbUsers_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbUsers.SelectedItem == null)
        {
            return;
        }

        string username = cmbUsers.SelectedItem.ToString() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(username))
        {
            LoggingUtility.Log($"[ViewApplicationLogsForm] Invalid username selected");
            return;
        }

        // Validate username via Helper_LogPath
        string? userDirectory = Helper_LogPath.GetUserLogDirectory(username);
        if (userDirectory == null)
        {
            LoggingUtility.Log($"[ViewApplicationLogsForm] Username validation failed: {username}");
            Service_ErrorHandler.HandleValidationError("Invalid username selected.", nameof(cmbUsers));
            return;
        }

        _selectedUsername = username;
        await LoadLogFilesAsync(username);
    }

    /// <summary>
    /// Handles log file selection change event. Loads entries from selected file.
    /// </summary>
    private async void lstLogFiles_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstLogFiles.SelectedItems.Count == 0)
        {
            return;
        }

        var selectedItem = lstLogFiles.SelectedItems[0];

        // Skip group headers (Tag is null)
        if (selectedItem.Tag == null)
        {
            return;
        }

        var logFile = selectedItem.Tag as Model_LogFile;
        if (logFile == null)
        {
            return;
        }

        _selectedLogFile = logFile;

        try
        {
            await LoadLogFileAsync(logFile.FilePath);
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[ViewApplicationLogsForm] Unexpected error in file selection handler");
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium, controlName: nameof(ViewApplicationLogsForm));
        }
    }

    #endregion

    #region Helpers

    /// <summary>
    /// Updates the state of navigation buttons based on current entry position.
    /// </summary>
    private void UpdateNavigationButtons()
    {
        if (_currentEntries == null || _currentEntries.Count == 0)
        {
            btnPrevious.Enabled = false;
            btnNext.Enabled = false;
            lblEntryPosition.Text = "No entries loaded";
            return;
        }

        btnPrevious.Enabled = _currentEntryIndex > 0;
        btnNext.Enabled = _currentEntryIndex < _currentEntries.Count - 1;
        lblEntryPosition.Text = $"Entry {_currentEntryIndex + 1} of {_currentEntries.Count}";
    }

    /// <summary>
    /// Applies severity-based color coding to the entry display label.
    /// Implements FR-015 severity color coding specification.
    /// </summary>
    /// <param name="entry">The log entry to analyze for color coding.</param>
    private void ApplySeverityColorCoding(Model_LogEntry entry)
    {
        Color backColor = SystemColors.Control; // Default

        switch (entry.LogType)
        {
            case LogFormat.Normal:
                // Normal log color coding based on level
                backColor = entry.Level?.ToUpperInvariant() switch
                {
                    "LOW" => Color.LightGray,
                    "MEDIUM" => Color.LightBlue,
                    "HIGH" => Color.LightGreen,
                    "DATA" => Color.LightCyan,
                    _ => SystemColors.Control
                };
                break;

            case LogFormat.ApplicationError:
                // Application errors are always red
                backColor = Color.LightCoral;
                break;

            case LogFormat.DatabaseError:
                // Database error color coding based on severity
                backColor = entry.Level?.ToUpperInvariant() switch
                {
                    "WARNING" => Color.LightYellow,
                    "ERROR" => Color.LightCoral,
                    "CRITICAL" => Color.IndianRed,
                    _ => SystemColors.Control
                };
                break;

            case LogFormat.Unknown:
            default:
                backColor = Color.LightGray;
                break;
        }

        txtEntryDisplay.BackColor = backColor;
    }

    /// <summary>
    /// Formats JSON details string with proper indentation for display.
    /// Handles parse failures gracefully by returning the original string.
    /// </summary>
    /// <param name="jsonString">The JSON string to format.</param>
    /// <returns>Formatted JSON string or original if parsing fails.</returns>
    private string FormatJsonDetails(string jsonString)
    {
        if (string.IsNullOrWhiteSpace(jsonString))
        {
            return string.Empty;
        }

        try
        {
            // Attempt to parse and reformat JSON with indentation
            using var jsonDoc = System.Text.Json.JsonDocument.Parse(jsonString);
            var options = new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true
            };
            return System.Text.Json.JsonSerializer.Serialize(jsonDoc.RootElement, options);
        }
        catch (System.Text.Json.JsonException)
        {
            // If JSON parsing fails, return original string
            return jsonString;
        }
    }

    /// <summary>
    /// Gets emoji display string with text fallback for systems without emoji font support.
    /// Implements FR-016 emoji indicator display with fallback.
    /// </summary>
    /// <param name="emoji">The emoji character to display.</param>
    /// <returns>Emoji string or text fallback.</returns>
    private string GetEmojiDisplay(string? emoji)
    {
        if (string.IsNullOrWhiteSpace(emoji))
        {
            return string.Empty;
        }

        // Map common emojis to text fallbacks
        var emojiMap = new Dictionary<string, string>
        {
            { "✅", "[SUCCESS]" },
            { "⏱", "[TIMER]" },
            { "🗄", "[DATABASE]" },
            { "📤", "[SEND]" },
            { "📥", "[RECEIVE]" },
            { "⚠", "[WARNING]" },
            { "❌", "[ERROR]" },
            { "🔍", "[SEARCH]" },
            { "📊", "[DATA]" },
            { "🔧", "[CONFIG]" }
        };

        // Try to detect if emoji rendering is supported
        // For simplicity, assume emoji is supported and provide fallback in parentheses
        if (emojiMap.TryGetValue(emoji, out var fallback))
        {
            return $"{emoji} {fallback}";
        }

        return emoji;
    }

    /// <summary>
    /// Copies the current log entry to the clipboard.
    /// Implements T043 - Copy Entry functionality via Ctrl+C shortcut.
    /// </summary>
    private void CopyCurrentEntry()
    {
        if (_currentEntries == null || _currentEntries.Count == 0)
        {
            return;
        }

        if (_currentEntryIndex < 0 || _currentEntryIndex >= _currentEntries.Count)
        {
            return;
        }

        try
        {
            // Get the text currently displayed in the textbox
            string textToCopy = txtEntryDisplay.Text;

            if (!string.IsNullOrWhiteSpace(textToCopy))
            {
                Clipboard.SetText(textToCopy);
                lblStatus.Text = "Entry copied to clipboard";
                LoggingUtility.Log($"[ViewApplicationLogsForm] Entry {_currentEntryIndex + 1} copied to clipboard");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[ViewApplicationLogsForm] Error copying to clipboard");
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Low,
                contextData: new Dictionary<string, object> { ["Operation"] = "CopyEntry" },
                controlName: nameof(ViewApplicationLogsForm));
        }
    }

    #endregion

    #region Cleanup

    /// <summary>
    /// Clean up any resources being used.
    /// Ensures proper disposal of form resources, collections, and prevents memory leaks.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Dispose designer components
            components?.Dispose();

            // Clear collections to release references
            _currentLogFiles?.Clear();
            _currentEntries?.Clear();

            // Note: Service_LogFileReader is static, no disposal needed
            // Note: Helper_LogPath is static, no disposal needed
            // Future: If auto-refresh timer is added (T047), dispose it here
            
            LoggingUtility.Log($"[ViewApplicationLogsForm] Form disposed, resources cleaned up");
        }

        base.Dispose(disposing);
    }

    #endregion
}
