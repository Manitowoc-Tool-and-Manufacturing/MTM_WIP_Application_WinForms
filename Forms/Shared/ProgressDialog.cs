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
public class ProgressDialog : ThemedForm
{
    private readonly ProgressBar _progressBar = new();
    private readonly Button _buttonCancel = new();
    private readonly Label _labelStatus = new();

    public CancellationTokenSource CancellationSource { get; } = new();

    public ProgressDialog(string title = "Progress")
    {
        Text = title;
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MinimizeBox = false;
        MaximizeBox = false;
        ShowInTaskbar = false;
        Width = 400;
        Height = 120;

        InitializeComponent();
        // DPI scaling and layout now handled by ThemedForm.OnLoad
    }

    private void InitializeComponent()
    {
    _progressBar.Style = ProgressBarStyle.Continuous;
    _progressBar.Dock = DockStyle.Top;
    _progressBar.Height = 24;

    _labelStatus.Text = "Working...";
    _labelStatus.Dock = DockStyle.Top;
    _labelStatus.Height = 20;
    _labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

    _buttonCancel.Text = "Cancel";
    _buttonCancel.Dock = DockStyle.Bottom;
    _buttonCancel.Height = 30; 
        _buttonCancel.Click += (s, e) => CancellationSource.Cancel();

        Controls.Add(_progressBar);
        Controls.Add(_labelStatus);
        Controls.Add(_buttonCancel);
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

        _progressBar.Value = percent;
        if (!string.IsNullOrEmpty(status))
        {
            _labelStatus.Text = status;
        }
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        base.OnFormClosed(e);
        CancellationSource.Dispose();
    }
}
