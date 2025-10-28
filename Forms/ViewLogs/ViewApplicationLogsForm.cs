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
    private bool _isParsedView = true; // True = parsed fields, False = raw text
    private System.Windows.Forms.Timer? _autoRefreshTimer; // T047: Auto-refresh timer
    private bool _autoRefreshEnabled = true; // T047: Auto-refresh state

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
        InitializeAutoRefreshTimer(); // T047
    }

    /// <summary>
    /// Initializes a new instance of the ViewApplicationLogsForm with pre-selected user.
    /// </summary>
    /// <param name="username">Username to pre-select in the user dropdown.</param>
    public ViewApplicationLogsForm(string username) : this()
    {
        _selectedUsername = username;
    }

    /// <summary>
    /// Initializes the auto-refresh timer for file list updates.
    /// Implements T047 - auto-refresh timer configuration.
    /// </summary>
    private void InitializeAutoRefreshTimer()
    {
        _autoRefreshTimer = new System.Windows.Forms.Timer
        {
            Interval = 5000, // 5 seconds (FR-033, SC-007)
            Enabled = false  // Start disabled, enable after user selects a user
        };
        _autoRefreshTimer.Tick += AutoRefreshTimer_Tick;

        LoggingUtility.Log("[ViewApplicationLogsForm] Auto-refresh timer initialized with 5s interval");
    }

    /// <summary>
    /// Handles auto-refresh timer tick event. Refreshes file list if enabled.
    /// Implements T047 - auto-refresh functionality.
    /// </summary>
    private async void AutoRefreshTimer_Tick(object? sender, EventArgs e)
    {
        if (!_autoRefreshEnabled || string.IsNullOrWhiteSpace(_selectedUsername))
        {
            return;
        }

        try
        {
            // Pause timer during refresh to prevent overlap
            _autoRefreshTimer?.Stop();

            await LoadLogFilesAsync(_selectedUsername);

            LoggingUtility.Log("[ViewApplicationLogsForm] Auto-refresh completed");
        }
        catch (Exception ex)
        {
            LoggingUtility.Log("[ViewApplicationLogsForm] Auto-refresh failed");
            LoggingUtility.LogApplicationError(ex);
        }
        finally
        {
            // Resume timer if still enabled
            if (_autoRefreshEnabled && _autoRefreshTimer != null)
            {
                _autoRefreshTimer.Start();
            }
        }
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
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

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

            stopwatch.Stop();

            // T048: Performance logging - SC-001: Load user list <500ms
            if (stopwatch.ElapsedMilliseconds > 500)
            {
                LoggingUtility.Log($"[Performance] LoadUserListAsync took {stopwatch.ElapsedMilliseconds}ms for {userDirectories.Count} users (target <500ms)");
            }

            LoggingUtility.Log($"[ViewApplicationLogsForm] Loaded {userDirectories.Count} users with log directories in {stopwatch.ElapsedMilliseconds}ms");

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
        Resize += ViewApplicationLogsForm_Resize; // T047: Pause/resume timer on minimize/restore
        cmbUsers.SelectedIndexChanged += cmbUsers_SelectedIndexChanged;
        btnRefresh.Click += btnRefresh_Click;
        lstLogFiles.SelectedIndexChanged += lstLogFiles_SelectedIndexChanged;
        btnPrevious.Click += btnPrevious_Click;
        btnNext.Click += btnNext_Click;
        btnToggleView.Click += btnToggleView_Click;
        KeyDown += ViewApplicationLogsForm_KeyDown;
        KeyPreview = true; // Enable form-level keyboard handling
    }

    /// <summary>
    /// Handles form resize event. Pauses auto-refresh when minimized, resumes when restored.
    /// Implements T047 - pause logic for minimized form (FR-046).
    /// </summary>
    private void ViewApplicationLogsForm_Resize(object? sender, EventArgs e)
    {
        if (_autoRefreshTimer == null)
        {
            return;
        }

        if (WindowState == FormWindowState.Minimized)
        {
            // Pause auto-refresh when minimized
            _autoRefreshTimer.Stop();
            LoggingUtility.Log("[ViewApplicationLogsForm] Auto-refresh paused (form minimized)");
        }
        else if (WindowState == FormWindowState.Normal || WindowState == FormWindowState.Maximized)
        {
            // Resume auto-refresh when restored
            if (_autoRefreshEnabled && !string.IsNullOrWhiteSpace(_selectedUsername))
            {
                _autoRefreshTimer.Start();
                LoggingUtility.Log("[ViewApplicationLogsForm] Auto-refresh resumed (form restored)");
            }
        }
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

        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

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

            stopwatch.Stop();

            // T048: Performance logging - SC-003: Load and parse <2s for 1000 entries
            if (stopwatch.ElapsedMilliseconds > 2000)
            {
                LoggingUtility.Log($"[Performance] LoadLogFileAsync took {stopwatch.ElapsedMilliseconds}ms for {_currentEntries.Count} entries (target <2000ms)");
            }

            LoggingUtility.Log($"[ViewApplicationLogsForm] Loaded {_currentEntries.Count} entries from: {filePath} in {stopwatch.ElapsedMilliseconds}ms");
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

        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

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

            stopwatch.Stop();

            // T048: Performance logging - SC-002: Enumerate files <1s
            if (stopwatch.ElapsedMilliseconds > 1000)
            {
                LoggingUtility.Log($"[Performance] LoadLogFilesAsync took {stopwatch.ElapsedMilliseconds}ms for {_currentLogFiles.Count} files (target <1000ms)");
            }

            LoggingUtility.Log($"[ViewApplicationLogsForm] Loaded {_currentLogFiles.Count} log files for user: {username} in {stopwatch.ElapsedMilliseconds}ms");
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
            txtRawView.Text = "No entries loaded";
            txtEntryDisplay.BackColor = SystemColors.Control;
            return;
        }

        if (_currentEntryIndex < 0 || _currentEntryIndex >= _currentEntries.Count)
        {
            LoggingUtility.Log($"[ViewApplicationLogsForm] Invalid entry index: {_currentEntryIndex}");
            txtEntryDisplay.Text = "Invalid entry index";
            txtRawView.Text = "Invalid entry index";
            txtEntryDisplay.BackColor = SystemColors.Control;
            return;
        }

        var entry = _currentEntries[_currentEntryIndex];

        // T040: Automatic raw view fallback for parse failures
        if (!entry.ParseSuccess)
        {
            _isParsedView = false;
            btnToggleView.Text = "Show Parsed View";
            txtEntryDisplay.Visible = false;
            txtRawView.Visible = true;
            txtRawView.Text = $"═══ Parse Failed - Showing Raw Text ═══\n\n{entry.RawText}";
            lblStatus.Text = "Parse failed - showing raw view";
            LoggingUtility.Log($"[ViewApplicationLogsForm] Entry {_currentEntryIndex + 1} parse failed, showing raw view");
            UpdateNavigationButtons();
            return;
        }

        // Apply severity color coding (T026)
        ApplySeverityColorCoding(entry);

        // T038/T039: Handle view mode toggle
        if (_isParsedView)
        {
            // Build formatted display based on log type (Parsed View)
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
        }
        else
        {
            // Raw View - show exact file content
            txtRawView.Text = $"═══ Raw Text - Entry {_currentEntryIndex + 1} of {_currentEntries.Count} ═══\n\n{entry.RawText}";
        }

        UpdateNavigationButtons();

        LoggingUtility.Log($"[ViewApplicationLogsForm] Showing entry {_currentEntryIndex + 1} of {_currentEntries.Count} in {(_isParsedView ? "parsed" : "raw")} view");
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
    /// Handles Toggle View button click. Switches between parsed and raw view.
    /// Implements T039 - view mode toggle functionality.
    /// </summary>
    private void btnToggleView_Click(object? sender, EventArgs e)
    {
        if (_currentEntries == null || _currentEntries.Count == 0)
        {
            lblStatus.Text = "No entries loaded";
            return;
        }

        // Toggle the view mode
        _isParsedView = !_isParsedView;

        // Update button text
        btnToggleView.Text = _isParsedView ? "Show Raw View" : "Show Parsed View";

        // Toggle control visibility
        txtEntryDisplay.Visible = _isParsedView;
        txtRawView.Visible = !_isParsedView;

        // Refresh the display with current entry
        ShowCurrentEntry();

        // Update status
        lblStatus.Text = _isParsedView ? "Showing parsed view" : "Showing raw view";
        LoggingUtility.Log($"[ViewApplicationLogsForm] View toggled to {(_isParsedView ? "parsed" : "raw")}");
    }

    /// <summary>
    /// Handles Export button click (future implementation when button is added to designer).
    /// Exports current entries to text file with SaveFileDialog.
    /// Implements T041 - export entries functionality.
    /// </summary>
    private async void btnExport_Click(object? sender, EventArgs e)
    {
        if (_currentEntries == null || _currentEntries.Count == 0)
        {
            lblStatus.Text = "No entries to export";
            return;
        }

        try
        {
            // Show SaveFileDialog
            using var saveDialog = new SaveFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*",
                DefaultExt = "txt",
                FileName = $"LogExport_{DateTime.Now:yyyyMMdd_HHmmss}.txt",
                Title = "Export Log Entries"
            };

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                var stopwatch = System.Diagnostics.Stopwatch.StartNew();

                // Format entries
                string exportText = FormatEntriesForExport(_currentEntries);

                // Write asynchronously
                await File.WriteAllTextAsync(saveDialog.FileName, exportText);

                stopwatch.Stop();

                // Performance logging (SC-006: 500 entries in <1s)
                if (stopwatch.ElapsedMilliseconds > 1000 && _currentEntries.Count >= 500)
                {
                    LoggingUtility.Log($"[ViewApplicationLogsForm] WARNING: Export took {stopwatch.ElapsedMilliseconds}ms for {_currentEntries.Count} entries (target <1s)");
                }

                lblStatus.Text = $"Exported {_currentEntries.Count} entries to {Path.GetFileName(saveDialog.FileName)}";
                LoggingUtility.Log($"[ViewApplicationLogsForm] Exported {_currentEntries.Count} entries in {stopwatch.ElapsedMilliseconds}ms");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium,
                contextData: new Dictionary<string, object> 
                { 
                    ["Operation"] = "ExportEntries",
                    ["EntryCount"] = _currentEntries?.Count ?? 0
                },
                controlName: nameof(ViewApplicationLogsForm));
        }
    }

    /// <summary>
    /// Handles Open Directory button click (future implementation when button is added to designer).
    /// Opens Windows Explorer to the selected user's log directory.
    /// Implements T044 - open log directory functionality.
    /// </summary>
    private void btnOpenDirectory_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(_selectedUsername))
        {
            lblStatus.Text = "No user selected";
            return;
        }

        try
        {
            string logDirectory = Helper_LogPath.GetUserLogDirectory(_selectedUsername);

            if (!Directory.Exists(logDirectory))
            {
                lblStatus.Text = $"Log directory not found: {logDirectory}";
                LoggingUtility.Log($"[ViewApplicationLogsForm] Log directory not found for user {_selectedUsername}: {logDirectory}");
                return;
            }

            // Launch Windows Explorer to the directory
            System.Diagnostics.Process.Start("explorer.exe", logDirectory);

            lblStatus.Text = $"Opened log directory for {_selectedUsername}";
            LoggingUtility.Log($"[ViewApplicationLogsForm] Opened log directory: {logDirectory}");
        }
        catch (Exception ex)
        {
            LoggingUtility.LogApplicationError(ex);
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Low,
                contextData: new Dictionary<string, object>
                {
                    ["Operation"] = "OpenLogDirectory",
                    ["Username"] = _selectedUsername ?? "null"
                },
                controlName: nameof(ViewApplicationLogsForm));
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

        // T047: Start auto-refresh timer when user is selected
        if (_autoRefreshEnabled && _autoRefreshTimer != null && WindowState != FormWindowState.Minimized)
        {
            _autoRefreshTimer.Start();
            LoggingUtility.Log("[ViewApplicationLogsForm] Auto-refresh timer started");
        }
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
    /// Dynamically updates severity filter options based on the current log type.
    /// Implements adaptive UI for different log format severity levels (FR-024).
    /// </summary>
    /// <param name="logType">The current log format type.</param>
    /// <remarks>
    /// Normal logs: LOW, MEDIUM, HIGH, DATA
    /// DatabaseError logs: WARNING, ERROR, CRITICAL
    /// ApplicationError logs: No severity options (always ERROR)
    /// </remarks>
    private void UpdateSeverityOptions(LogFormat logType)
    {
        // Note: This method will be fully implemented when filter panel controls are added in T030
        // For now, it documents the expected behavior for each log type

        // Future implementation will show/hide checkboxes dynamically:
        // - Normal: chkSevLow, chkSevMedium, chkSevHigh, chkSevData (4 options)
        // - DatabaseError: chkSevWarning, chkSevError, chkSevCritical (3 options)
        // - ApplicationError: No checkboxes (all errors same severity)
        // - Unknown: No checkboxes

        LoggingUtility.Log($"[ViewApplicationLogsForm] Severity options updated for {logType}");
    }

    /// <summary>
    /// Populates the source component dropdown with unique source values from current entries.
    /// Adds "All Sources" option for clearing the source filter (FR-024).
    /// </summary>
    private void PopulateSourceDropdown()
    {
        // Note: This method will be fully implemented when filter panel controls are added in T030
        // For now, it documents the expected behavior

        // Future implementation will:
        // 1. Extract unique Source values using LINQ: _currentEntries.Select(e => e.Source).Where(s => !string.IsNullOrEmpty(s)).Distinct()
        // 2. Add "All Sources" as first item
        // 3. Populate cmbSourceFilter dropdown
        // 4. Set SelectedIndex to 0 ("All Sources")

        if (_currentEntries == null || _currentEntries.Count == 0)
        {
            return;
        }

        // Extract unique sources
        var uniqueSources = _currentEntries
            .Where(e => !string.IsNullOrWhiteSpace(e.Source))
            .Select(e => e.Source!)
            .Distinct()
            .OrderBy(s => s)
            .ToList();

        LoggingUtility.Log($"[ViewApplicationLogsForm] Found {uniqueSources.Count} unique source components");
    }

    /// <summary>
    /// Applies the current filter using LogEntryNavigator and updates the display.
    /// Performance target: <300ms for filtering 5000 entries to 100 (SC-005).
    /// </summary>
    private void ApplyCurrentFilter()
    {
        // Note: This method will be fully implemented when LogEntryNavigator is integrated in T034
        // For now, it documents the expected behavior

        // Future implementation will:
        // 1. Start Stopwatch for performance monitoring
        // 2. Call _navigator.ApplyFilter(_activeFilter)
        // 3. Update lblEntryPosition with filtered count
        // 4. Call ShowCurrentEntry() to display first filtered entry
        // 5. Update navigation buttons
        // 6. Log performance warning if >300ms

        LoggingUtility.Log($"[ViewApplicationLogsForm] Filter applied: {_activeFilter?.HasActiveFilters ?? false}");
    }

    /// <summary>
    /// Clears all active filters and shows all entries.
    /// Resets all filter controls to default state (FR-025).
    /// </summary>
    private void ClearAllFilters()
    {
        // Note: This method will be fully implemented when filter panel controls are added in T030
        // For now, it documents the expected behavior

        // Future implementation will:
        // 1. Create new default filter: Model_LogFilter.CreateDefault()
        // 2. Reset all UI controls:
        //    - Date pickers to null/empty
        //    - Log type dropdown to "All Types"
        //    - Uncheck all severity checkboxes
        //    - Source dropdown to "All Sources"
        //    - Clear search textbox
        // 3. Call ApplyCurrentFilter()
        // 4. Update lblStatus with "Filters cleared"

        _activeFilter = Model_LogFilter.CreateDefault();
        LoggingUtility.Log("[ViewApplicationLogsForm] All filters cleared");
    }

    /// <summary>
    /// Applies a quick filter for errors only (ApplicationError and DatabaseError with severity ERROR/CRITICAL).
    /// Implements quick filter button functionality (FR-026).
    /// </summary>
    private void ApplyErrorsOnlyFilter()
    {
        // Note: This method will be fully implemented when filter panel controls are added in T030
        // For now, it documents the expected behavior

        // Future implementation will:
        // 1. Create new filter with:
        //    - LogTypes = [ApplicationError, DatabaseError]
        //    - SeverityLevels = ["ERROR", "CRITICAL"] (for DB errors)
        // 2. Update UI controls to reflect filter
        // 3. Call ApplyCurrentFilter()
        // 4. Update lblStatus with "Showing errors only"

        LoggingUtility.Log("[ViewApplicationLogsForm] Errors only filter applied");
    }

    /// <summary>
    /// Applies a quick filter for performance-related entries (searches for "slow", "timeout", "performance").
    /// Implements quick filter button functionality (FR-026).
    /// </summary>
    private void ApplyPerformanceFilter()
    {
        // Note: This method will be fully implemented when filter panel controls are added in T030
        // For now, it documents the expected behavior

        // Future implementation will:
        // 1. Create new filter with SearchText = "slow|timeout|performance" (regex pattern)
        // 2. Update search textbox
        // 3. Call ApplyCurrentFilter()
        // 4. Update lblStatus with "Showing performance entries"

        LoggingUtility.Log("[ViewApplicationLogsForm] Performance filter applied");
    }

    /// <summary>
    /// Applies a quick filter for today's entries only.
    /// Implements quick filter button functionality (FR-026).
    /// </summary>
    private void ApplyTodayFilter()
    {
        // Note: This method will be fully implemented when filter panel controls are added in T030
        // For now, it documents the expected behavior

        // Future implementation will:
        // 1. Create new filter with:
        //    - StartDate = DateTime.Today
        //    - EndDate = DateTime.Today
        // 2. Update date picker controls
        // 3. Call ApplyCurrentFilter()
        // 4. Update lblStatus with "Showing today's entries"

        LoggingUtility.Log("[ViewApplicationLogsForm] Today filter applied");
    }

    /// <summary>
    /// Persists the current filter state for reuse when same log type is selected.
    /// Implements filter state persistence across log files of same type (FR-027).
    /// </summary>
    /// <param name="logType">The log type to persist filter for.</param>
    private void PersistFilterState(LogFormat logType)
    {
        // Note: This method will be fully implemented when filter persistence is added in T037
        // For now, it documents the expected behavior

        // Future implementation will:
        // 1. Store _activeFilter in a Dictionary<LogFormat, Model_LogFilter>
        // 2. Use Clone() method to create independent copy
        // 3. Restore filter when loading log of same type

        LoggingUtility.Log($"[ViewApplicationLogsForm] Filter state persisted for {logType}");
    }

    /// <summary>
    /// Restores previously persisted filter state for the given log type.
    /// Implements filter state persistence across log files of same type (FR-027).
    /// </summary>
    /// <param name="logType">The log type to restore filter for.</param>
    /// <returns>True if filter was restored, false if no persisted state exists.</returns>
    private bool RestoreFilterState(LogFormat logType)
    {
        // Note: This method will be fully implemented when filter persistence is added in T037
        // For now, it documents the expected behavior

        // Future implementation will:
        // 1. Check if Dictionary<LogFormat, Model_LogFilter> contains entry for logType
        // 2. If exists, clone the stored filter and apply it
        // 3. Update all UI controls to reflect restored filter
        // 4. Return true if restored, false otherwise

        LoggingUtility.Log($"[ViewApplicationLogsForm] Attempted to restore filter state for {logType}");
        return false;
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

    /// <summary>
    /// Formats log entries for export to text file.
    /// Creates structured, readable text with clear separation between entries.
    /// Implements T042 - export formatting logic.
    /// </summary>
    /// <param name="entries">List of log entries to format.</param>
    /// <returns>Formatted text string ready for file export.</returns>
    private string FormatEntriesForExport(List<Model_LogEntry> entries)
    {
        if (entries == null || entries.Count == 0)
        {
            return "No entries to export.";
        }

        var output = new System.Text.StringBuilder();
        output.AppendLine("═══════════════════════════════════════════════");
        output.AppendLine($"MTM Application Log Export");
        output.AppendLine($"Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        output.AppendLine($"Total Entries: {entries.Count}");
        output.AppendLine("═══════════════════════════════════════════════");
        output.AppendLine();

        int entryNumber = 1;
        foreach (var entry in entries)
        {
            output.AppendLine($"--- Entry {entryNumber} of {entries.Count} ---");

            switch (entry.LogType)
            {
                case LogFormat.Normal:
                    output.AppendLine($"Format: Normal Log");
                    output.AppendLine($"Timestamp: {entry.Timestamp:yyyy-MM-dd HH:mm:ss}");
                    output.AppendLine($"Level: {entry.Level ?? "N/A"}");
                    output.AppendLine($"Source: {entry.Source ?? "N/A"}");
                    output.AppendLine($"Message: {entry.Message ?? "N/A"}");
                    if (!string.IsNullOrWhiteSpace(entry.Details))
                    {
                        output.AppendLine($"Details: {entry.Details}");
                    }
                    break;

                case LogFormat.ApplicationError:
                    output.AppendLine($"Format: Application Error");
                    output.AppendLine($"Timestamp: {entry.Timestamp:yyyy-MM-dd HH:mm:ss}");
                    output.AppendLine($"Error Type: {entry.ErrorType ?? "N/A"}");
                    output.AppendLine($"Exception: {entry.Message ?? "N/A"}");
                    if (!string.IsNullOrWhiteSpace(entry.StackTrace))
                    {
                        output.AppendLine($"Stack Trace:");
                        output.AppendLine(entry.StackTrace);
                    }
                    break;

                case LogFormat.DatabaseError:
                    output.AppendLine($"Format: Database Error");
                    output.AppendLine($"Timestamp: {entry.Timestamp:yyyy-MM-dd HH:mm:ss}");
                    output.AppendLine($"Severity: {entry.Severity ?? "N/A"}");
                    output.AppendLine($"Message: {entry.Message ?? "N/A"}");
                    if (!string.IsNullOrWhiteSpace(entry.StackTrace))
                    {
                        output.AppendLine($"Stack Trace:");
                        output.AppendLine(entry.StackTrace);
                    }
                    break;

                case LogFormat.Unknown:
                default:
                    output.AppendLine($"Format: Unknown");
                    output.AppendLine($"Timestamp: {entry.Timestamp:yyyy-MM-dd HH:mm:ss}");
                    output.AppendLine($"Raw Text:");
                    output.AppendLine(entry.RawText);
                    break;
            }

            output.AppendLine();
            output.AppendLine("─────────────────────────────────────────────");
            output.AppendLine();

            entryNumber++;
        }

        output.AppendLine("═══════════════════════════════════════════════");
        output.AppendLine($"End of Export - {entries.Count} entries");
        output.AppendLine("═══════════════════════════════════════════════");

        return output.ToString();
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

            // T047/T049: Dispose auto-refresh timer
            if (_autoRefreshTimer != null)
            {
                _autoRefreshTimer.Stop();
                _autoRefreshTimer.Dispose();
                _autoRefreshTimer = null;
            }

            // Clear collections to release references
            _currentLogFiles?.Clear();
            _currentEntries?.Clear();

            // Note: Service_LogFileReader is static, no disposal needed
            // Note: Helper_LogPath is static, no disposal needed
            
            LoggingUtility.Log($"[ViewApplicationLogsForm] Form disposed, resources cleaned up");
        }

        base.Dispose(disposing);
    }

    #endregion
}
