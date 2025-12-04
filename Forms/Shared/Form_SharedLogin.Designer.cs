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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_SharedLogin));
            labelUsername = new Label();
            textBoxUsername = new TextBox();
            labelPin = new Label();
            textBoxPin = new TextBox();
            buttonLogin = new Button();
            buttonCancel = new Button();
            SuspendLayout();
            // 
            // labelUsername
            // 
            labelUsername.AutoSize = true;
            labelUsername.Location = new Point(30, 30);
            labelUsername.Name = "labelUsername";
            labelUsername.Size = new Size(63, 15);
            labelUsername.TabIndex = 0;
            labelUsername.Text = "Username:";
            // 
            // textBoxUsername
            // 
            textBoxUsername.Location = new Point(30, 48);
            textBoxUsername.Name = "textBoxUsername";
            textBoxUsername.Size = new Size(200, 23);
            textBoxUsername.TabIndex = 1;
            // 
            // labelPin
            // 
            labelPin.AutoSize = true;
            labelPin.Location = new Point(30, 80);
            labelPin.Name = "labelPin";
            labelPin.Size = new Size(29, 15);
            labelPin.TabIndex = 2;
            labelPin.Text = "PIN:";
            // 
            // textBoxPin
            // 
            textBoxPin.Location = new Point(30, 98);
            textBoxPin.Name = "textBoxPin";
            textBoxPin.PasswordChar = '*';
            textBoxPin.Size = new Size(200, 23);
            textBoxPin.TabIndex = 3;
            // 
            // buttonLogin
            // 
            buttonLogin.Location = new Point(30, 140);
            buttonLogin.Name = "buttonLogin";
            buttonLogin.Size = new Size(90, 30);
            buttonLogin.TabIndex = 4;
            buttonLogin.Text = "Login";
            buttonLogin.UseVisualStyleBackColor = true;
            buttonLogin.Click += buttonLogin_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Location = new Point(140, 140);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(90, 30);
            buttonCancel.TabIndex = 5;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // Form_SharedLogin
            // 
            AcceptButton = buttonLogin;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = buttonCancel;
            ClientSize = new Size(264, 201);
            Controls.Add(buttonCancel);
            Controls.Add(buttonLogin);
            Controls.Add(textBoxPin);
            Controls.Add(labelPin);
            Controls.Add(textBoxUsername);
            Controls.Add(labelUsername);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form_SharedLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Shared Workstation Login";
            ResumeLayout(false);
            PerformLayout();

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
