using MTM_WIP_Application_Winforms.Forms.Shared;

namespace MTM_WIP_Application_Winforms.Controls.Shared
{
    partial class Control_ProgressBarUserControl : ThemedUserControl
    {
        #region Fields
        


        private System.ComponentModel.IContainer components = null;
        
        #endregion
        
        #region Methods


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            
            if (Tag is System.Windows.Forms.Timer timer)
            {
                timer.Stop();
                timer.Dispose();
                Tag = null;
            }
            
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // Control_ProgressBarUserControl
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BorderStyle = BorderStyle.FixedSingle;
            Name = "Control_ProgressBarUserControl";
            Size = new Size(0, 0);
            ResumeLayout(false);
        }

        #endregion
    }

        
        #endregion
    }
