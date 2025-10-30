using System.Diagnostics;
using System.Media;
using MTM_WIP_Application_Winforms.Controls.Addons;
using MTM_WIP_Application_Winforms.Core;
using MTM_WIP_Application_Winforms.Forms.MainForm;
using MTM_WIP_Application_Winforms.Helpers;
using MySql.Data.MySqlClient;
using Timer = System.Windows.Forms.Timer;

namespace MTM_WIP_Application_Winforms.Services
{
    /// <summary>
    /// Service for managing database connection recovery and monitoring
    /// </summary>
    public class Service_ConnectionRecoveryManager
    {
    #region Fields

    private readonly MainForm _mainForm;
    private readonly Timer _reconnectTimer;
    private int _secondsUntilRetry;
    private int _consecutiveFailures;
    private bool _isReconnecting; // Prevents concurrent reconnection attempts
    private const int RetryIntervalSeconds = 5;
    private const int MaxConsecutiveFailures = 5;

    #endregion

    #region Properties

        /// <summary>
        /// Gets a value indicating whether the disconnect timer is currently active
        /// </summary>
        public bool IsDisconnectTimerActive => _reconnectTimer.Enabled;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Service_ConnectionRecoveryManager class
        /// </summary>
        /// <param name="mainForm">Reference to the main form</param>
        /// <exception cref="ArgumentNullException">Thrown when mainForm is null</exception>
        public Service_ConnectionRecoveryManager(MainForm mainForm)
        {
            _mainForm = mainForm ?? throw new ArgumentNullException(nameof(mainForm));
            _secondsUntilRetry = RetryIntervalSeconds;
            
            // Timer ticks every 1 second to update countdown
            _reconnectTimer = new Timer { Interval = 1000 };
            _reconnectTimer.Tick += async (s, e) => await ReconnectTimerTickAsync();
        }

        #endregion

        #region Connection Management Methods

        /// <summary>
        /// Handles when database connection is lost
        /// </summary>
        public void HandleConnectionLost()
        {
            if (_mainForm.InvokeRequired)
            {
                _mainForm.Invoke(new Action(HandleConnectionLost));
                return;
            }

            // CRITICAL: Check if we're already in recovery mode
            // If timer is already running, don't restart it (prevents countdown reset)
            if (_reconnectTimer.Enabled)
            {
                Debug.WriteLine("[ConnectionRecovery] HandleConnectionLost called but timer already active - ignoring duplicate call");
                return;
            }

            Debug.WriteLine("[ConnectionRecovery] HandleConnectionLost called - starting recovery mode");
            SystemSounds.Exclamation.Play();

            // Reset consecutive failures counter when connection is first lost
            _consecutiveFailures = 0;

            // Update connection strength control to show no signal
            _mainForm.MainForm_UserControl_SignalStrength.Strength = 0;
            _mainForm.MainForm_UserControl_SignalStrength.Ping = -1;
            Debug.WriteLine("[ConnectionRecovery] Connection strength set to 0");

            // Lock the main form - disable all tabs, quick buttons, and menu
            _mainForm.MainForm_TabControl.Enabled = false;
            _mainForm.MainForm_SplitContainer_Middle.Panel2.Enabled = false; // Disable quick buttons
            _mainForm.MainForm_MenuStrip.Enabled = false; // Disable menu strip
            Debug.WriteLine("[ConnectionRecovery] TabControl, QuickButtons, and MenuStrip disabled");
            
            // Hide the "Ready" status text
            _mainForm.MainForm_StatusText.Visible = false;
            Debug.WriteLine("[ConnectionRecovery] Ready status hidden");
            
            // Initialize countdown
            _secondsUntilRetry = RetryIntervalSeconds;
            UpdateDisconnectedStatusMessage();
            
            // Show disconnected status
            _mainForm.MainForm_StatusStrip_Disconnected.Visible = true;
            Debug.WriteLine("[ConnectionRecovery] Disconnected status shown");
            _reconnectTimer.Start();
            Debug.WriteLine("[ConnectionRecovery] Reconnect timer started");
        }

        /// <summary>
        /// Handles when database connection is restored
        /// </summary>
        public void HandleConnectionRestored()
        {
            if (_mainForm.InvokeRequired)
            {
                _mainForm.Invoke(new Action(HandleConnectionRestored));
                return;
            }

            Debug.WriteLine("[ConnectionRecovery] HandleConnectionRestored called");
            SystemSounds.Question.Play();
            
            // Reset consecutive failures counter on successful reconnection
            _consecutiveFailures = 0;
            Debug.WriteLine("[ConnectionRecovery] Consecutive failures reset to 0");
            
            // Unlock the main form - enable all tabs, quick buttons, and menu
            _mainForm.MainForm_TabControl.Enabled = true;
            _mainForm.MainForm_SplitContainer_Middle.Panel2.Enabled = true; // Enable quick buttons
            _mainForm.MainForm_MenuStrip.Enabled = true; // Enable menu strip
            
            // Show the "Ready" status text again
            _mainForm.MainForm_StatusText.Visible = true;
            
            // Hide disconnected status
            _mainForm.MainForm_StatusStrip_Disconnected.Visible = false;
            _reconnectTimer.Stop();
        }

        /// <summary>
        /// Updates the connection strength indicator on the main form
        /// </summary>
        public async Task UpdateConnectionStrengthAsync()
        {
            Debug.WriteLine("[ConnectionRecovery] UpdateConnectionStrengthAsync called");
            Control_ConnectionStrengthControl? signalStrength = _mainForm.MainForm_UserControl_SignalStrength;
            ToolStripStatusLabel? statusStripDisconnected = _mainForm.MainForm_StatusStrip_Disconnected;

            if (signalStrength.InvokeRequired)
            {
                await Task.Run(async () => await UpdateConnectionStrengthAsync());
                return;
            }

            (int strength, int pingMs) = await Helper_Control_MySqlSignal.GetStrengthAsync();
            Debug.WriteLine($"[ConnectionRecovery] Strength: {strength}, Ping: {pingMs}, TimerActive: {IsDisconnectTimerActive}");

            // If reconnect timer is active, we're disconnected - show 0 strength
            if (IsDisconnectTimerActive)
            {
                strength = 0;
                pingMs = -1;
                Debug.WriteLine("[ConnectionRecovery] Timer active, forcing strength to 0");
            }

            signalStrength.Strength = strength;
            signalStrength.Ping = pingMs;

            statusStripDisconnected.Visible = strength == 0;

            if (strength == 0 && !IsDisconnectTimerActive)
            {
                Debug.WriteLine("[ConnectionRecovery] Strength is 0 and timer not active, calling HandleConnectionLost");
                HandleConnectionLost();
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Timer tick handler that updates countdown and attempts reconnection
        /// </summary>
        private async Task ReconnectTimerTickAsync()
        {
            _secondsUntilRetry--;
            
            if (_secondsUntilRetry <= 0)
            {
                // Time to retry connection
                await TryReconnectAsync();
                
                // If still disconnected, reset countdown
                if (IsDisconnectTimerActive)
                {
                    _secondsUntilRetry = RetryIntervalSeconds;
                }
            }
            
            // Update status message with current countdown
            UpdateDisconnectedStatusMessage();
        }

        /// <summary>
        /// Updates the disconnected status message with attempt count
        /// </summary>
        private void UpdateDisconnectedStatusMessage()
        {
            if (_mainForm.InvokeRequired)
            {
                _mainForm.Invoke(new Action(UpdateDisconnectedStatusMessage));
                return;
            }

            _mainForm.MainForm_StatusStrip_Disconnected.Text = 
                $"Connection to server lost, attempting to reconnect... (Attempt {_consecutiveFailures + 1}/{MaxConsecutiveFailures})";
        }

        /// <summary>
        /// Attempts to reconnect to the database
        /// </summary>
        private async Task TryReconnectAsync()
        {
            // Prevent concurrent reconnection attempts
            if (_isReconnecting)
            {
                Debug.WriteLine("[ConnectionRecovery] Reconnection already in progress, skipping duplicate attempt");
                return;
            }

            _isReconnecting = true;
            try
            {
                Debug.WriteLine($"[ConnectionRecovery] Attempting reconnection (Failure #{_consecutiveFailures + 1})");
                using MySqlConnection conn = new(Core_WipAppVariables.ReConnectionString);
                await conn.OpenAsync();
                Debug.WriteLine("[ConnectionRecovery] Reconnection successful!");
                HandleConnectionRestored();
            }
            catch (Exception ex)
            {
                // Increment failure counter
                _consecutiveFailures++;
                Debug.WriteLine($"[ConnectionRecovery] Reconnection failed (Total failures: {_consecutiveFailures})");
                
                // Check if we've exceeded the maximum consecutive failures
                if (_consecutiveFailures >= MaxConsecutiveFailures)
                {
                    Debug.WriteLine($"[ConnectionRecovery] Max failures ({MaxConsecutiveFailures}) reached, showing error dialog");
                    ShowConnectionFailureError(ex);
                }
                
                // Reconnection failed, timer will try again
            }
            finally
            {
                _isReconnecting = false;
            }
        }

        /// <summary>
        /// Shows an error dialog when connection cannot be restored after multiple attempts
        /// </summary>
        private void ShowConnectionFailureError(Exception lastException)
        {
            try
            {
                // Stop the timer - we're giving up on automatic reconnection
                _reconnectTimer.Stop();
                
                var contextData = new Dictionary<string, object>
                {
                    ["ConsecutiveFailures"] = _consecutiveFailures,
                    ["RetryInterval"] = RetryIntervalSeconds,
                    ["MaxAttempts"] = MaxConsecutiveFailures,
                    ["ConnectionString"] = "Hidden for security",
                    ["Message"] = $"Database connection could not be restored after {_consecutiveFailures} attempts. The application must close."
                };
                
                // Show fatal error - application will close when user dismisses the dialog
                Service_ErrorHandler.HandleDatabaseError(
                    lastException,
                    retryAction: null, // No retry option - fatal error
                    contextData: contextData,
                    callerName: nameof(Service_ConnectionRecoveryManager),
                    controlName: "Connection Recovery",
                    methodName: "Database Connection Recovery - Fatal Error",
                    dbSeverity: Models.DatabaseErrorSeverity.Critical
                );
                
                // Force application exit after showing the error
                Application.Exit();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ConnectionRecovery] Error showing connection failure dialog: {ex.Message}");
                // Force exit even if dialog fails
                Application.Exit();
            }
        }

        #endregion
    }
}
