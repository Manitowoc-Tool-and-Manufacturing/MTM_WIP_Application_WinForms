using System.Drawing;
using System.Windows.Forms;

namespace MTM_WIP_Application_Winforms.Controls.DeveloperTools
{
    partial class Control_RecentErrors
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        #region Fields

        private System.ComponentModel.IContainer components = null;

        #endregion

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        #region Methods

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Control_RecentErrors_DataGridView_Errors = new DataGridView();
            this.Control_RecentErrors_Label_Title = new Label();
            ((System.ComponentModel.ISupportInitialize)(this.Control_RecentErrors_DataGridView_Errors)).BeginInit();
            this.SuspendLayout();
            // 
            // Control_RecentErrors_DataGridView_Errors
            // 
            this.Control_RecentErrors_DataGridView_Errors.AllowUserToAddRows = false;
            this.Control_RecentErrors_DataGridView_Errors.AllowUserToDeleteRows = false;
            this.Control_RecentErrors_DataGridView_Errors.AllowUserToResizeRows = false;
            this.Control_RecentErrors_DataGridView_Errors.BackgroundColor = Color.White;
            this.Control_RecentErrors_DataGridView_Errors.BorderStyle = BorderStyle.None;
            this.Control_RecentErrors_DataGridView_Errors.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Control_RecentErrors_DataGridView_Errors.Dock = DockStyle.Fill;
            this.Control_RecentErrors_DataGridView_Errors.Location = new Point(0, 25);
            this.Control_RecentErrors_DataGridView_Errors.MultiSelect = false;
            this.Control_RecentErrors_DataGridView_Errors.Name = "Control_RecentErrors_DataGridView_Errors";
            this.Control_RecentErrors_DataGridView_Errors.ReadOnly = true;
            this.Control_RecentErrors_DataGridView_Errors.RowHeadersVisible = false;
            this.Control_RecentErrors_DataGridView_Errors.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.Control_RecentErrors_DataGridView_Errors.Size = new Size(600, 275);
            this.Control_RecentErrors_DataGridView_Errors.TabIndex = 1;
            // 
            // Control_RecentErrors_Label_Title
            // 
            this.Control_RecentErrors_Label_Title.Dock = DockStyle.Top;
            this.Control_RecentErrors_Label_Title.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            this.Control_RecentErrors_Label_Title.Location = new Point(0, 0);
            this.Control_RecentErrors_Label_Title.Name = "Control_RecentErrors_Label_Title";
            this.Control_RecentErrors_Label_Title.Padding = new Padding(5, 0, 0, 0);
            this.Control_RecentErrors_Label_Title.Size = new Size(600, 25);
            this.Control_RecentErrors_Label_Title.TabIndex = 0;
            this.Control_RecentErrors_Label_Title.Text = "Recent Critical Errors";
            this.Control_RecentErrors_Label_Title.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // Control_RecentErrors
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.Control_RecentErrors_DataGridView_Errors);
            this.Controls.Add(this.Control_RecentErrors_Label_Title);
            this.Name = "Control_RecentErrors";
            this.Size = new Size(600, 300);
            ((System.ComponentModel.ISupportInitialize)(this.Control_RecentErrors_DataGridView_Errors)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView Control_RecentErrors_DataGridView_Errors;
        private Label Control_RecentErrors_Label_Title;
    }
}



