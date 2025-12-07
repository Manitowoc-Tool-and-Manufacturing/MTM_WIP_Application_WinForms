using System.ComponentModel;
using System.Windows.Forms;

namespace MTM_WIP_Application_Winforms.Forms.Shared
{
    partial class ProgressDialog
    {
        private IContainer components = null;
        private MTM_WIP_Application_Winforms.Components.Shared.Component_ProgressBarUserControl _progressControl;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(ProgressDialog));
            _progressControl = new MTM_WIP_Application_Winforms.Components.Shared.Component_ProgressBarUserControl();
            SuspendLayout();
            // 
            // _progressControl
            // 
            _progressControl.AutoSize = true;
            _progressControl.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            _progressControl.BorderStyle = BorderStyle.FixedSingle;
            _progressControl.Dock = DockStyle.Fill;
            _progressControl.Location = new Point(0, 0);
            _progressControl.Margin = new Padding(6);
            _progressControl.Name = "_progressControl";
            _progressControl.Size = new Size(390, 123);
            _progressControl.TabIndex = 0;
            // 
            // ProgressDialog
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(390, 123);
            ControlBox = false;
            Controls.Add(_progressControl);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ProgressDialog";
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
