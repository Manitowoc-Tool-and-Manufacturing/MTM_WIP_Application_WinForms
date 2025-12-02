namespace MTM_WIP_Application_Winforms.Forms.Shared
{
    partial class Form_SharedLogin
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
            this.labelUsername = new System.Windows.Forms.Label();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.labelPin = new System.Windows.Forms.Label();
            this.textBoxPin = new System.Windows.Forms.TextBox();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelUsername
            // 
            this.labelUsername.AutoSize = true;
            this.labelUsername.Location = new System.Drawing.Point(30, 30);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(63, 15);
            this.labelUsername.TabIndex = 0;
            this.labelUsername.Text = "Username:";
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(30, 48);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(200, 23);
            this.textBoxUsername.TabIndex = 1;
            // 
            // labelPin
            // 
            this.labelPin.AutoSize = true;
            this.labelPin.Location = new System.Drawing.Point(30, 80);
            this.labelPin.Name = "labelPin";
            this.labelPin.Size = new System.Drawing.Size(29, 15);
            this.labelPin.TabIndex = 2;
            this.labelPin.Text = "PIN:";
            // 
            // textBoxPin
            // 
            this.textBoxPin.Location = new System.Drawing.Point(30, 98);
            this.textBoxPin.Name = "textBoxPin";
            this.textBoxPin.PasswordChar = '*';
            this.textBoxPin.Size = new System.Drawing.Size(200, 23);
            this.textBoxPin.TabIndex = 3;
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(30, 140);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(90, 30);
            this.buttonLogin.TabIndex = 4;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(140, 140);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(90, 30);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // Form_SharedLogin
            // 
            this.AcceptButton = this.buttonLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(264, 201);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.textBoxPin);
            this.Controls.Add(this.labelPin);
            this.Controls.Add(this.textBoxUsername);
            this.Controls.Add(this.labelUsername);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_SharedLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Shared Workstation Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Label labelPin;
        private System.Windows.Forms.TextBox textBoxPin;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Button buttonCancel;
    }
}
