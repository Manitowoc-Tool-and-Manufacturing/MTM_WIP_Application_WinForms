namespace MTM_WIP_Application_Winforms.Services.Database
{
    /// <summary>
    /// Interface for the view that handles connection recovery UI updates.
    /// Implemented by MainForm to decouple it from Service_ConnectionRecoveryManager.
    /// </summary>
    public interface IConnectionRecoveryView
    {
        /// <summary>
        /// Gets a value indicating whether the caller must call an invoke method when making method calls to the control.
        /// </summary>
        bool InvokeRequired { get; }

        /// <summary>
        /// Executes the specified delegate on the thread that owns the control's underlying window handle.
        /// </summary>
        object? Invoke(Delegate method);

        /// <summary>
        /// Updates the signal strength indicator.
        /// </summary>
        void UpdateSignalStrength(int strength, int ping);

        /// <summary>
        /// Enables or disables the main application controls (tabs, menus, etc.).
        /// </summary>
        void SetApplicationEnabled(bool enabled);

        /// <summary>
        /// Sets the visibility of the "Ready" status text.
        /// </summary>
        void SetReadyStatusVisible(bool visible);

        /// <summary>
        /// Sets the visibility of the disconnected status indicator.
        /// </summary>
        void SetDisconnectedStatusVisible(bool visible);

        /// <summary>
        /// Sets the text of the disconnected status indicator.
        /// </summary>
        void SetDisconnectedStatusText(string text);
    }
}
