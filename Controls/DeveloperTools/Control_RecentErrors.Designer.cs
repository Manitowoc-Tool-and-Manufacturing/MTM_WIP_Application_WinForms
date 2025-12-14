namespace MTM_WIP_Application_Winforms.Controls.DeveloperTools
{
    partial class Control_RecentErrors
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Control_RecentErrors_DataGridView_Errors = new System.Windows.Forms.DataGridView();
            this.Control_RecentErrors_Label_Title = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Control_RecentErrors_DataGridView_Errors)).BeginInit();
            this.SuspendLayout();
            // 
            // Control_RecentErrors_DataGridView_Errors
            // 
            this.Control_RecentErrors_DataGridView_Errors.AllowUserToAddRows = false;
            this.Control_RecentErrors_DataGridView_Errors.AllowUserToDeleteRows = false;
            this.Control_RecentErrors_DataGridView_Errors.AllowUserToResizeRows = false;
            this.Control_RecentErrors_DataGridView_Errors.BackgroundColor = System.Drawing.Color.White;
            this.Control_RecentErrors_DataGridView_Errors.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Control_RecentErrors_DataGridView_Errors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Control_RecentErrors_DataGridView_Errors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Control_RecentErrors_DataGridView_Errors.Location = new System.Drawing.Point(0, 25);
            this.Control_RecentErrors_DataGridView_Errors.MultiSelect = false;
            this.Control_RecentErrors_DataGridView_Errors.Name = "Control_RecentErrors_DataGridView_Errors";
            this.Control_RecentErrors_DataGridView_Errors.ReadOnly = true;
            this.Control_RecentErrors_DataGridView_Errors.RowHeadersVisible = false;
            this.Control_RecentErrors_DataGridView_Errors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Control_RecentErrors_DataGridView_Errors.Size = new System.Drawing.Size(600, 275);
            this.Control_RecentErrors_DataGridView_Errors.TabIndex = 1;
            // 
            // Control_RecentErrors_Label_Title
            // 
            this.Control_RecentErrors_Label_Title.Dock = System.Windows.Forms.DockStyle.Top;
            this.Control_RecentErrors_Label_Title.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Control_RecentErrors_Label_Title.Location = new System.Drawing.Point(0, 0);
            this.Control_RecentErrors_Label_Title.Name = "Control_RecentErrors_Label_Title";
            this.Control_RecentErrors_Label_Title.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.Control_RecentErrors_Label_Title.Size = new System.Drawing.Size(600, 25);
            this.Control_RecentErrors_Label_Title.TabIndex = 0;
            this.Control_RecentErrors_Label_Title.Text = "Recent Critical Errors";
            this.Control_RecentErrors_Label_Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Control_RecentErrors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Control_RecentErrors_DataGridView_Errors);
            this.Controls.Add(this.Control_RecentErrors_Label_Title);
            this.Name = "Control_RecentErrors";
            this.Size = new System.Drawing.Size(600, 300);
            ((System.ComponentModel.ISupportInitialize)(this.Control_RecentErrors_DataGridView_Errors)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView Control_RecentErrors_DataGridView_Errors;
        private System.Windows.Forms.Label Control_RecentErrors_Label_Title;
    }
}

