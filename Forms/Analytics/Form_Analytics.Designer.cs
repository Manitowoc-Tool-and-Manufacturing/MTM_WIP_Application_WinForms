namespace MTM_WIP_Application_Winforms.Forms.Analytics
{
    partial class Form_Analytics
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.controlAnalytics = new MTM_WIP_Application_Winforms.Controls.Visual.Control_MaterialHandlerAnalytics();
            this.SuspendLayout();
            // 
            // controlAnalytics
            // 
            this.controlAnalytics.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlAnalytics.Location = new System.Drawing.Point(0, 0);
            this.controlAnalytics.Name = "controlAnalytics";
            this.controlAnalytics.Size = new System.Drawing.Size(1000, 600);
            this.controlAnalytics.TabIndex = 0;
            // 
            // Form_Analytics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.controlAnalytics);
            this.Name = "Form_Analytics";
            this.Text = "Material Handler Analytics";
            this.ResumeLayout(false);

        }

        #endregion

        private MTM_WIP_Application_Winforms.Controls.Visual.Control_MaterialHandlerAnalytics controlAnalytics;
    }
}
