using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTM_WIP_Application_Winforms.Core;

namespace MTM_WIP_Application_Winforms.Forms.Shared;

/// <summary>
/// Simple cancelable progress dialog used for long-running export/preview operations.
/// Migrated to ThemedForm for automatic DPI scaling and theme support.
/// </summary>
public partial class ProgressDialog : ThemedForm
{
    public CancellationTokenSource CancellationSource { get; } = new();
    public ProgressDialog() : this("Progress")
    {
    }

    public ProgressDialog(string title)
    {
        InitializeComponent();

        // Apply window properties after designer init for VS compatibility
        Text = title;
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MinimizeBox = false;
        MaximizeBox = false;
        ShowInTaskbar = false;

        // Configure embedded progress control to match Splash look
        _progressControl.EnableCancel(true);
        _progressControl.CancelRequested += () => CancellationSource.Cancel();
    }

    public void SetProgress(int percent, string? status = null)
    {
        if (percent < 0) percent = 0;
        if (percent > 100) percent = 100;
        if (InvokeRequired)
        {
            BeginInvoke(new Action(() => SetProgress(percent, status)));
            return;
        }

        _progressControl.UpdateProgress(percent, status);
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        base.OnFormClosed(e);
        CancellationSource.Dispose();
    }
}
