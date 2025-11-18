using System.ComponentModel;

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    partial class Control_SettingsHome
    {
        #region Fields

        private IContainer components = null!;

        #endregion

        #region Methods

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            SuspendLayout();
            // 
            // Control_SettingsHome
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            Name = "Control_SettingsHome";
            Size = new System.Drawing.Size(800, 600);
            ResumeLayout(false);
        }

        #endregion
    }
}
