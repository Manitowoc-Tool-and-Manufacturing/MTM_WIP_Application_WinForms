using MTM_Inventory_Application.Core;
using MTM_Inventory_Application.Helpers;
using MTM_Inventory_Application.Logging;
using MTM_Inventory_Application.Models;
using MTM_Inventory_Application.Services;
using System.Text;

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

    // T072: Prompt status filter controls
    private CheckBox? _chkFilterNoPrompts;
    private CheckBox? _chkFilterNewStatus;
    private CheckBox? _chkFilterInProgressStatus;
    private CheckBox? _chkFilterShowAllStatus;

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
        
        // Performance optimization
        SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        UpdateStyles();
        
        Core_Themes.ApplyDpiScaling(this);
        Core_Themes.ApplyRuntimeLayoutAdjustments(this);
        Core_Themes.ApplyFocusHighlighting(this);
        WireUpEvents();
        InitializeAutoRefreshTimer(); // T047
        InitializePromptStatusFilters(); // T072
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
    /// Initializes the prompt status filter controls.
    /// Implements T072 - Filter by Prompt Status options.
    /// Creates checkboxes for filtering errors by prompt existence and status.
    /// </summary>
    private void InitializePromptStatusFilters()
    {
        // Create a new panel for prompt status filters
        var panelPromptFilters = new Panel
        {
            Dock = DockStyle.Top,
            Height = 35,
            Padding = new Padding(10, 5, 10, 5)
        };

        // Label
        var lblPromptStatus = new Label
        {
            Text = "Prompt Status:",
            AutoSize = true,
            Location = new Point(5, 8)
        };

        // Checkbox: Only errors without prompts
        _chkFilterNoPrompts = new CheckBox
        {
            Text = "Without Prompts",
            AutoSize = true,
            Location = new Point(110, 8),
            Checked = false
        };
        _chkFilterNoPrompts.CheckedChanged += (s, e) => OnPromptStatusFilterChanged();

        // Checkbox: Only "New" status
        _chkFilterNewStatus = new CheckBox
        {
            Text = "New",
            AutoSize = true,
            Location = new Point(240, 8),
            Checked = false
        };
        _chkFilterNewStatus.CheckedChanged += (s, e) => OnPromptStatusFilterChanged();

        // Checkbox: Only "In Progress" status
        _chkFilterInProgressStatus = new CheckBox
        {
            Text = "In Progress",
            AutoSize = true,
            Location = new Point(300, 8),
            Checked = false
        };
        _chkFilterInProgressStatus.CheckedChanged += (s, e) => OnPromptStatusFilterChanged();

        // Checkbox: Show all statuses (default)
        _chkFilterShowAllStatus = new CheckBox
        {
            Text = "Show All",
            AutoSize = true,
            Location = new Point(400, 8),
            Checked = true
        };
        _chkFilterShowAllStatus.CheckedChanged += (s, e) => OnPromptStatusFilterChanged();

        // Add controls to panel
        panelPromptFilters.Controls.Add(lblPromptStatus);
        panelPromptFilters.Controls.Add(_chkFilterNoPrompts);
        panelPromptFilters.Controls.Add(_chkFilterNewStatus);
        panelPromptFilters.Controls.Add(_chkFilterInProgressStatus);
        panelPromptFilters.Controls.Add(_chkFilterShowAllStatus);

        // Insert panel below existing filters (panelFilters)
        panelPromptFilters.Parent = panelEntryDisplay;
        panelPromptFilters.BringToFront();
        
        LoggingUtility.Log("[ViewApplicationLogsForm] Prompt status filter controls initialized");
    }

    /// <summary>
    /// Handles changes to prompt status filter checkboxes.
    /// Implements T072 - mutual exclusion logic for filter options.
    /// </summary>
    private void OnPromptStatusFilterChanged()
    {
        if (_chkFilterShowAllStatus == null || _chkFilterNoPrompts == null || 
            _chkFilterNewStatus == null || _chkFilterInProgressStatus == null)
        {
            return;
        }

        // Mutual exclusion logic: "Show All" vs specific filters
        if (_chkFilterShowAllStatus.Checked)
        {
            // If Show All is checked, uncheck specific filters
            _chkFilterNoPrompts.Checked = false;
            _chkFilterNewStatus.Checked = false;
            _chkFilterInProgressStatus.Checked = false;
        }
        else if (_chkFilterNoPrompts.Checked || _chkFilterNewStatus.Checked || _chkFilterInProgressStatus.Checked)
        {
            // If any specific filter is checked, uncheck Show All
            _chkFilterShowAllStatus.Checked = false;
        }
        else
        {
            // If all specific filters are unchecked, re-enable Show All
            _chkFilterShowAllStatus.Checked = true;
        }

        LoggingUtility.Log($"[ViewApplicationLogsForm] Prompt status filter changed: " +
            $"ShowAll={_chkFilterShowAllStatus.Checked}, NoPrompts={_chkFilterNoPrompts.Checked}, " +
            $"New={_chkFilterNewStatus.Checked}, InProgress={_chkFilterInProgressStatus.Checked}");
    }

    /// <summary>
    /// Handles auto-refresh timer tick event. Refreshes file list if enabled.
    /// Implements T047 - auto-refresh functionality.
    /// </summary>
    private async void AutoRefreshTimer_Tick(object? sender, EventArgs e)
    {
        if (!chkAutoRefresh.Checked || string.IsNullOrWhiteSpace(_selectedUsername))
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
            if (chkAutoRefresh.Checked && _autoRefreshTimer != null)
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
            // Apply theme colors to form and controls
            Core_Themes.ApplyTheme(this);
            
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
            // Save current selection before clearing
            string? previouslySelectedUser = cmbUsers.SelectedItem as string;
            
            cmbUsers.Items.Clear();

            // Get all base log directories (primary and fallback)
            string[] baseLogDirs = Helper_LogPath.GetAllBaseLogDirectories();

            if (baseLogDirs.Length == 0 || !baseLogDirs.Any(Directory.Exists))
            {
                LoggingUtility.Log($"[ViewApplicationLogsForm] No log directories found");
                Service_ErrorHandler.ShowInformation(
                    "No log directory found. Logs will be created when the application generates its first log entry.",
                    "No Logs Available");
                return;
            }

            // Enumerate user directories from all locations asynchronously
            var userDirectories = await Task.Run(() =>
            {
                var allUsers = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                foreach (var baseLogDir in baseLogDirs)
                {
                    if (Directory.Exists(baseLogDir))
                    {
                        try
                        {
                            var users = Directory.GetDirectories(baseLogDir)
                                .Select(dir => Path.GetFileName(dir))
                                .Where(name => !string.IsNullOrWhiteSpace(name));

                            foreach (var user in users)
                            {
                                allUsers.Add(user!);
                            }

                            LoggingUtility.Log($"[ViewApplicationLogsForm] Found {users.Count()} users in {baseLogDir}");
                        }
                        catch (Exception ex)
                        {
                            LoggingUtility.Log($"[ViewApplicationLogsForm] Error enumerating {baseLogDir}: {ex.Message}");
                        }
                    }
                }

                return allUsers.OrderBy(name => name).ToList();
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

            LoggingUtility.Log($"[ViewApplicationLogsForm] Loaded {userDirectories.Count} users with log directories in {stopwatch.ElapsedMilliseconds}ms from {baseLogDirs.Length} locations");

            if (cmbUsers.Items.Count > 0)
            {
                lblUserCount.Text = $"{cmbUsers.Items.Count} users";
                
                // Restore previous user selection if it still exists
                if (!string.IsNullOrWhiteSpace(previouslySelectedUser))
                {
                    int index = cmbUsers.Items.IndexOf(previouslySelectedUser);
                    if (index >= 0)
                    {
                        // Temporarily disable event to prevent reload
                        cmbUsers.SelectedIndexChanged -= cmbUsers_SelectedIndexChanged;
                        cmbUsers.SelectedIndex = index;
                        cmbUsers.SelectedIndexChanged += cmbUsers_SelectedIndexChanged;
                        
                        LoggingUtility.Log($"[ViewApplicationLogsForm] Restored user selection: {previouslySelectedUser}");
                    }
                }
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
        chkAutoRefresh.CheckedChanged += chkAutoRefresh_CheckedChanged;
        lstLogFiles.SelectedIndexChanged += lstLogFiles_SelectedIndexChanged;
        btnPrevious.Click += btnPrevious_Click;
        btnNext.Click += btnNext_Click;
        btnToggleView.Click += btnToggleView_Click;
        btnCreatePrompt.Click += btnCreatePrompt_Click; // T062: Create Prompt button
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
            // Resume auto-refresh when restored (only if checkbox is checked)
            if (chkAutoRefresh.Checked && !string.IsNullOrWhiteSpace(_selectedUsername))
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

            // Determine log format from selected file
            LogFormat logFormat = _selectedLogFile?.LogType ?? LogFormat.Normal;
            LoggingUtility.Log($"[ViewApplicationLogsForm] LoadLogFileAsync: Using LogFormat={logFormat} for parsing");

            // Load first 1000 entries (windowed loading per FR-044)
            var rawEntries = await Service_LogFileReader.LoadEntriesAsync(filePath, 0, 1000).ConfigureAwait(true);

            // Parse raw entries into Model_LogEntry objects
            foreach (var rawEntry in rawEntries)
            {
                var parsedEntry = Service_LogParser.ParseEntry(rawEntry, logFormat);
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
    /// T071: Applies color coding to file list items based on log type.
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

                // Add files in group with color coding (T071)
                foreach (var file in group.OrderByDescending(f => f.ModifiedDate))
                {
                    var item = new ListViewItem(new[]
                    {
                        file.FileName,
                        file.ModifiedDate.ToString("yyyy-MM-dd HH:mm:ss"),
                        file.FileSizeDisplay
                    });
                    item.Tag = file;

                    // Color coding removed - applied to entry display panel instead (T071)

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
    /// T056-T058: Uses structured textbox display with labeled fields.
    /// T062: Enables/disables Create Prompt button based on entry severity.
    /// </summary>
    private void ShowCurrentEntry()
    {
        if (_currentEntries == null || _currentEntries.Count == 0)
        {
            ClearEntryDisplay("No entries loaded");
            btnCreatePrompt.Enabled = false; // T062: Disable for no entries
            return;
        }

        if (_currentEntryIndex < 0 || _currentEntryIndex >= _currentEntries.Count)
        {
            LoggingUtility.Log($"[ViewApplicationLogsForm] Invalid entry index: {_currentEntryIndex}");
            ClearEntryDisplay("Invalid entry index");
            btnCreatePrompt.Enabled = false; // T062: Disable for invalid index
            return;
        }

        var entry = _currentEntries[_currentEntryIndex];

        // T062: Enable Create Prompt button only for ERROR/CRITICAL entries
        bool isErrorEntry = IsErrorOrCriticalEntry(entry);
        btnCreatePrompt.Enabled = isErrorEntry;

        // T040: Automatic raw view fallback for parse failures
        if (!entry.ParseSuccess)
        {
            _isParsedView = false;
            btnToggleView.Text = "Show Parsed View";
            tableLayoutEntryDisplay.Visible = false;
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
            // Structured view - populate individual textboxes
            tableLayoutEntryDisplay.Visible = true;
            txtRawView.Visible = false;

            // Populate structured fields based on log type
            PopulateStructuredDisplay(entry);
            
            // T071: Apply color to txtLevel AFTER text is populated
            ApplyEntryDisplayPanelBorder(entry);
        }
        else
        {
            // Raw View - show exact file content
            tableLayoutEntryDisplay.Visible = false;
            txtRawView.Visible = true;
            txtRawView.Text = $"═══ Raw Text - Entry {_currentEntryIndex + 1} of {_currentEntries.Count} ═══\n\n{entry.RawText}";
        }

        UpdateNavigationButtons();

        LoggingUtility.Log($"[ViewApplicationLogsForm] Showing entry {_currentEntryIndex + 1} of {_currentEntries.Count} in {(_isParsedView ? "parsed" : "raw")} view");
    }

    /// <summary>
    /// Clears the entry display with a specified message.
    /// </summary>
    private void ClearEntryDisplay(string message)
    {
        txtTimestamp.Text = string.Empty;
        txtLevel.Text = string.Empty;
        txtEntrySource.Text = string.Empty;
        txtEntryMessage.Text = string.Empty;
        txtEntryDetails.Text = string.Empty;
        txtRawView.Text = message;
        txtLevel.BackColor = SystemColors.Control;
        lblStatus.Text = message;
    }

    /// <summary>
    /// Populates the structured textbox display from a log entry.
    /// T057: Maps log entry fields to corresponding textboxes.
    /// </summary>
    private void PopulateStructuredDisplay(Model_LogEntry entry)
    {
        // Common fields for all log types
        txtTimestamp.Text = entry.Timestamp.ToString("yyyy-MM-dd HH:mm:ss");

        switch (entry.LogType)
        {
            case LogFormat.Normal:
                txtLevel.Text = $"{entry.Level ?? "N/A"} {GetEmojiDisplay(entry.Emoji)}";
                txtEntrySource.Text = entry.Source ?? "N/A";
                txtEntryMessage.Text = entry.Message ?? "N/A";
                
                // Format details with JSON formatting if applicable
                if (!string.IsNullOrWhiteSpace(entry.Details))
                {
                    txtEntryDetails.Text = FormatJsonDetails(entry.Details);
                }
                else
                {
                    txtEntryDetails.Text = string.Empty;
                }
                break;

            case LogFormat.ApplicationError:
                txtLevel.Text = $"ERROR - {entry.ErrorType ?? "Unknown"}";
                txtEntrySource.Text = "Application Error";
                txtEntryMessage.Text = entry.Message ?? "N/A";
                txtEntryDetails.Text = entry.StackTrace ?? string.Empty;
                break;

            case LogFormat.DatabaseError:
                txtLevel.Text = $"{entry.Severity ?? "ERROR"}";
                txtEntrySource.Text = "Database Error";
                txtEntryMessage.Text = entry.Message ?? "N/A";
                txtEntryDetails.Text = entry.StackTrace ?? string.Empty;
                break;

            case LogFormat.Unknown:
            default:
                txtLevel.Text = "UNKNOWN";
                txtEntrySource.Text = "Unknown Format";
                txtEntryMessage.Text = entry.RawText;
                txtEntryDetails.Text = string.Empty;
                break;
        }

        // Update status bar
        lblStatus.Text = $"Entry {_currentEntryIndex + 1} of {_currentEntries.Count} - {entry.LogType}";
    }

    #endregion

    #region Filtering

    #endregion

    #region Button Clicks

    /// <summary>
    /// Handles Refresh button click. Reloads current user's log files while preserving selection state.
    /// </summary>
    private async void btnRefresh_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(_selectedUsername))
        {
            // Save current state
            string? selectedFileName = lstLogFiles.SelectedItems.Count > 0 
                ? (lstLogFiles.SelectedItems[0].Tag as Model_LogFile)?.FileName 
                : null;
            int currentEntryIndex = _currentEntryIndex;
            
            // Reload files
            await LoadLogFilesAsync(_selectedUsername);
            
            // Restore file selection if it still exists
            if (!string.IsNullOrWhiteSpace(selectedFileName))
            {
                RestoreFileSelection(selectedFileName, currentEntryIndex);
            }
        }
    }

    /// <summary>
    /// Handles Auto Refresh checkbox change. Enables/disables the auto-refresh timer.
    /// </summary>
    private void chkAutoRefresh_CheckedChanged(object? sender, EventArgs e)
    {
        if (_autoRefreshTimer == null)
        {
            return;
        }

        if (chkAutoRefresh.Checked)
        {
            // Enable auto-refresh (if not minimized)
            if (WindowState != FormWindowState.Minimized)
            {
                _autoRefreshTimer.Start();
                LoggingUtility.Log("[ViewApplicationLogsForm] Auto-refresh enabled");
            }
        }
        else
        {
            // Disable auto-refresh
            _autoRefreshTimer.Stop();
            LoggingUtility.Log("[ViewApplicationLogsForm] Auto-refresh disabled");
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
        tableLayoutEntryDisplay.Visible = _isParsedView;
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

    /// <summary>
    /// Handles Create Prompt button click. Generates a Copilot prompt file from current error entry.
    /// Implements T062 - Create Prompt button functionality.
    /// Only enabled for ERROR or CRITICAL severity entries (Application/Database errors).
    /// </summary>
    private async void btnCreatePrompt_Click(object? sender, EventArgs e)
    {
        if (_currentEntries == null || _currentEntries.Count == 0)
        {
            lblStatus.Text = "No entry loaded";
            return;
        }

        if (_currentEntryIndex < 0 || _currentEntryIndex >= _currentEntries.Count)
        {
            lblStatus.Text = "Invalid entry index";
            return;
        }

        var entry = _currentEntries[_currentEntryIndex];

        try
        {
            // Extract method name from stack trace
            string? methodName = Service_PromptGenerator.ExtractMethodName(entry.StackTrace);
            
            if (string.IsNullOrWhiteSpace(methodName))
            {
                lblStatus.Text = "Could not extract method name from error";
                Service_ErrorHandler.ShowWarning(
                    "Unable to extract method name from the error's stack trace. Cannot generate prompt.",
                    "Method Name Not Found");
                return;
            }

            // T063: Check if prompt already exists
            string? promptFilePath = Helper_LogPath.GetPromptFilePath(methodName);
            
            if (promptFilePath != null && File.Exists(promptFilePath))
            {
                // Show custom dialog with options
                var result = MessageBox.Show(
                    $"A prompt fix for this error has already been generated:\n\n{methodName}\n\nDo you want to open the existing prompt file?",
                    "Prompt Already Exists",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                {
                    // Open existing prompt in default markdown editor
                    try
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = promptFilePath,
                            UseShellExecute = true
                        });
                        
                        lblStatus.Text = $"Opened existing prompt: {methodName}";
                        LoggingUtility.Log($"[ViewApplicationLogsForm] Opened existing prompt file: {promptFilePath}");
                    }
                    catch (Exception ex)
                    {
                        LoggingUtility.Log($"[ViewApplicationLogsForm] Error opening prompt file: {promptFilePath}");
                        LoggingUtility.LogApplicationError(ex);
                        Service_ErrorHandler.HandleException(ex, ErrorSeverity.Low,
                            contextData: new Dictionary<string, object> { ["FilePath"] = promptFilePath },
                            controlName: nameof(ViewApplicationLogsForm));
                    }
                }
                
                return;
            }

            // Generate the prompt
            string? promptContent = Service_PromptGenerator.GeneratePrompt(entry);
            
            if (string.IsNullOrWhiteSpace(promptContent))
            {
                lblStatus.Text = "Failed to generate prompt";
                Service_ErrorHandler.ShowWarning(
                    "Unable to generate prompt from the log entry. Check that the entry contains valid error information.",
                    "Prompt Generation Failed");
                return;
            }

            // Write prompt to file
            bool success = Service_PromptGenerator.WritePromptToFile(methodName, promptContent);
            
            if (success)
            {
                lblStatus.Text = $"Prompt created for method: {methodName}";
                Service_ErrorHandler.ShowInformation(
                    $"Copilot prompt file has been created successfully:\n\nMethod: {methodName}\nLocation: {promptFilePath ?? "Prompt Fixes directory"}",
                    "Prompt Created");
                
                LoggingUtility.Log($"[ViewApplicationLogsForm] Created prompt file for method: {methodName}");
            }
            else
            {
                lblStatus.Text = "Failed to write prompt file";
                Service_ErrorHandler.ShowWarning(
                    "The prompt was generated but could not be saved to file. Check that you have write permissions to the application directory.",
                    "File Write Failed");
            }
        }
        catch (Exception ex)
        {
            LoggingUtility.Log($"[ViewApplicationLogsForm] Error creating prompt");
            LoggingUtility.LogApplicationError(ex);
            lblStatus.Text = "Error creating prompt";
            Service_ErrorHandler.HandleException(ex, ErrorSeverity.Medium, 
                contextData: new Dictionary<string, object>
                {
                    ["Operation"] = "CreatePrompt",
                    ["EntryIndex"] = _currentEntryIndex,
                    ["LogType"] = entry.LogType.ToString()
                },
                controlName: nameof(ViewApplicationLogsForm));
        }
    }

    /// <summary>
    /// Handles Apply Filter button click. Applies active filter to current entries.
    /// Implements T072 - Filter by Prompt Status functionality.
    /// </summary>
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

        // T047: Start auto-refresh timer when user is selected (if checkbox is checked)
        if (chkAutoRefresh.Checked && _autoRefreshTimer != null && WindowState != FormWindowState.Minimized)
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
    /// T071: Adds emoji prefix to entry position label based on severity.
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

        // T071: Add emoji severity indicator to entry position label
        string emoji = GetSeverityEmoji(_currentEntries[_currentEntryIndex]);
        lblEntryPosition.Text = $"{emoji} Entry {_currentEntryIndex + 1} of {_currentEntries.Count}";
    }

    /// <summary>
    /// Restores file selection and entry position after refresh.
    /// </summary>
    /// <param name="fileName">The filename to restore selection for.</param>
    /// <param name="entryIndex">The entry index to restore.</param>
    private void RestoreFileSelection(string fileName, int entryIndex)
    {
        // Find the matching file in the ListView
        foreach (ListViewItem item in lstLogFiles.Items)
        {
            if (item.Tag is Model_LogFile logFile && 
                logFile.FileName.Equals(fileName, StringComparison.OrdinalIgnoreCase))
            {
                // Temporarily disable event to prevent reload
                lstLogFiles.SelectedIndexChanged -= lstLogFiles_SelectedIndexChanged;
                
                item.Selected = true;
                item.EnsureVisible();
                
                // Re-enable event
                lstLogFiles.SelectedIndexChanged += lstLogFiles_SelectedIndexChanged;
                
                // Restore entry position if valid
                if (entryIndex >= 0 && entryIndex < _currentEntries.Count)
                {
                    _currentEntryIndex = entryIndex;
                    ShowCurrentEntry();
                }
                
                LoggingUtility.Log($"[ViewApplicationLogsForm] Restored selection: {fileName} at entry {entryIndex}");
                break;
            }
        }
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

        txtLevel.BackColor = backColor;
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
            // Build text from structured display or raw view depending on mode
            string textToCopy;
            if (_isParsedView)
            {
                var sb = new StringBuilder();
                sb.AppendLine($"Timestamp: {txtTimestamp.Text}");
                sb.AppendLine($"Level: {txtLevel.Text}");
                sb.AppendLine($"Source: {txtEntrySource.Text}");
                sb.AppendLine($"Message: {txtEntryMessage.Text}");
                if (!string.IsNullOrWhiteSpace(txtEntryDetails.Text))
                {
                    sb.AppendLine($"Details:");
                    sb.AppendLine(txtEntryDetails.Text);
                }
                textToCopy = sb.ToString();
            }
            else
            {
                textToCopy = txtRawView.Text;
            }

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

    /// <summary>
    /// Determines if a log entry represents an ERROR or CRITICAL severity issue.
    /// Used to enable/disable the Create Prompt button (T062).
    /// </summary>
    /// <param name="entry">Log entry to check.</param>
    /// <returns>True if entry is ERROR or CRITICAL severity; false otherwise.</returns>
    private bool IsErrorOrCriticalEntry(Model_LogEntry entry)
    {
        if (entry == null)
        {
            return false;
        }

        // ApplicationError logs are always errors
        if (entry.LogType == LogFormat.ApplicationError)
        {
            return true;
        }

        // DatabaseError logs - check severity
        if (entry.LogType == LogFormat.DatabaseError)
        {
            string? severity = entry.Severity?.ToUpperInvariant();
            return severity == "ERROR" || severity == "CRITICAL";
        }

        // Normal logs - check level (though they rarely have ERROR level)
        if (entry.LogType == LogFormat.Normal)
        {
            string? level = entry.Level?.ToUpperInvariant();
            return level == "ERROR" || level == "CRITICAL";
        }

        return false;
    }

    /// <summary>
    /// Gets the appropriate emoji indicator for a log entry's severity.
    /// Implements T071 - emoji severity indicators in UI.
    /// </summary>
    /// <param name="entry">Log entry to get emoji for.</param>
    /// <returns>Emoji string (🔴 Critical, 🟠 Error, 🟡 Warning, 🔵 Info).</returns>
    private string GetSeverityEmoji(Model_LogEntry entry)
    {
        if (entry == null)
        {
            return "⚪"; // White circle for null/unknown
        }

        // ApplicationError logs are always error severity
        if (entry.LogType == LogFormat.ApplicationError)
        {
            return "🔴"; // Red circle for application errors
        }

        // DatabaseError logs - check severity
        if (entry.LogType == LogFormat.DatabaseError)
        {
            string? severity = entry.Severity?.ToUpperInvariant();
            return severity switch
            {
                "CRITICAL" => "🔴", // Red circle
                "ERROR" => "🟠",    // Orange circle
                "WARNING" => "🟡",  // Yellow circle
                _ => "🔵"           // Blue circle for info/unknown
            };
        }

        // Normal logs - check level
        if (entry.LogType == LogFormat.Normal)
        {
            string? level = entry.Level?.ToUpperInvariant();
            return level switch
            {
                "HIGH" => "🟢",     // Green circle
                "MEDIUM" => "🔵",   // Blue circle
                "LOW" => "⚪",       // White circle
                "DATA" => "🟣",     // Purple circle
                _ => "⚪"            // White circle for unknown
            };
        }

        return "⚪"; // White circle for unknown format
    }

    /// <summary>
    /// Applies background color to the Level textbox based on entry severity.
    /// Implements T071 - visual severity indicators throughout UI.
    /// Now uses intelligent level detection from enhanced logging.
    /// </summary>
    /// <param name="entry">Log entry to determine color from.</param>
    private void ApplyEntryDisplayPanelBorder(Model_LogEntry entry)
    {
        if (entry == null || txtLevel == null)
        {
            if (txtLevel != null)
                txtLevel.BackColor = SystemColors.Window;
            return;
        }

        // Get the actual text in txtLevel to determine color
        string levelText = txtLevel.Text?.ToUpperInvariant() ?? "";
        
        // Determine background color based on level text content
        Color backgroundColor = SystemColors.Window; // Default white

        // Critical/Fatal errors - Darkest Red
        if (levelText.Contains("CRITICAL") || levelText.Contains("FATAL"))
        {
            backgroundColor = Color.FromArgb(255, 180, 180);
        }
        // Errors - Red
        else if (levelText.Contains("ERROR"))
        {
            backgroundColor = Color.FromArgb(255, 200, 200);
        }
        // Warnings - Orange/Yellow
        else if (levelText.Contains("WARNING") || levelText.Contains("WARN"))
        {
            backgroundColor = Color.FromArgb(255, 240, 200);
        }
        // Success - Light Green
        else if (levelText.Contains("SUCCESS") || levelText.Contains("COMPLETED"))
        {
            backgroundColor = Color.FromArgb(200, 255, 200);
        }
        // Performance - Light Purple
        else if (levelText.Contains("PERFORMANCE"))
        {
            backgroundColor = Color.FromArgb(230, 200, 255);
        }
        // Debug - Light Gray
        else if (levelText.Contains("DEBUG") || levelText.Contains("TRACE"))
        {
            backgroundColor = Color.FromArgb(240, 240, 240);
        }
        // Info (default) - Light Blue
        else if (levelText.Contains("INFO"))
        {
            backgroundColor = Color.FromArgb(200, 220, 255);
        }
        // High priority - Green
        else if (levelText.Contains("HIGH"))
        {
            backgroundColor = Color.FromArgb(200, 255, 200);
        }
        // Medium priority - Light Blue
        else if (levelText.Contains("MEDIUM"))
        {
            backgroundColor = Color.FromArgb(200, 220, 255);
        }
        // Low priority - Very Light Gray
        else if (levelText.Contains("LOW"))
        {
            backgroundColor = Color.FromArgb(240, 240, 240);
        }
        // Data - Light Purple
        else if (levelText.Contains("DATA"))
        {
            backgroundColor = Color.FromArgb(230, 200, 255);
        }
        // Default for any other content - Light Blue
        else if (!string.IsNullOrWhiteSpace(levelText))
        {
            backgroundColor = Color.FromArgb(200, 220, 255);
        }

        // Apply background color to txtLevel textbox
        txtLevel.BackColor = backgroundColor;
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
