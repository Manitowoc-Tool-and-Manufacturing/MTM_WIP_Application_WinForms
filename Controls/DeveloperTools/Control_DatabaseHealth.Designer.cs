using MTM_WIP_Application_Winforms.Controls;

namespace MTM_WIP_Application_Winforms.Controls.DeveloperTools
{
    partial class Control_DatabaseHealth
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
            this.Control_DatabaseHealth_Panel_Header = new System.Windows.Forms.Panel();
            this.Control_DatabaseHealth_Button_Refresh = new System.Windows.Forms.Button();
            this.Control_DatabaseHealth_Label_Title = new System.Windows.Forms.Label();
            this.Control_DatabaseHealth_Panel_Status = new System.Windows.Forms.Panel();
            this.Control_DatabaseHealth_Label_ActiveConnections = new System.Windows.Forms.Label();
            this.Control_DatabaseHealth_Label_ConnectionStatus = new System.Windows.Forms.Label();
            this.Control_DatabaseHealth_DataGridView_Tables = new System.Windows.Forms.DataGridView();
            this.Control_DatabaseHealth_Panel_Header.SuspendLayout();
            this.Control_DatabaseHealth_Panel_Status.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Control_DatabaseHealth_DataGridView_Tables)).BeginInit();
            this.SuspendLayout();
            // 
            // Control_DatabaseHealth_Panel_Header
            // 
            this.Control_DatabaseHealth_Panel_Header.Controls.Add(this.Control_DatabaseHealth_Button_Refresh);
            this.Control_DatabaseHealth_Panel_Header.Controls.Add(this.Control_DatabaseHealth_Label_Title);
            this.Control_DatabaseHealth_Panel_Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.Control_DatabaseHealth_Panel_Header.Location = new System.Drawing.Point(0, 0);
            this.Control_DatabaseHealth_Panel_Header.Name = "Control_DatabaseHealth_Panel_Header";
            this.Control_DatabaseHealth_Panel_Header.Size = new System.Drawing.Size(800, 50);
            this.Control_DatabaseHealth_Panel_Header.TabIndex = 0;
            // 
            // Control_DatabaseHealth_Button_Refresh
            // 
            this.Control_DatabaseHealth_Button_Refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Control_DatabaseHealth_Button_Refresh.Location = new System.Drawing.Point(710, 13);
            this.Control_DatabaseHealth_Button_Refresh.Name = "Control_DatabaseHealth_Button_Refresh";
            this.Control_DatabaseHealth_Button_Refresh.Size = new System.Drawing.Size(75, 23);
            this.Control_DatabaseHealth_Button_Refresh.TabIndex = 1;
            this.Control_DatabaseHealth_Button_Refresh.Text = "Refresh";
            this.Control_DatabaseHealth_Button_Refresh.UseVisualStyleBackColor = true;
            this.Control_DatabaseHealth_Button_Refresh.Click += new System.EventHandler(this.Control_DatabaseHealth_Button_Refresh_Click);
            // 
            // Control_DatabaseHealth_Label_Title
            // 
            this.Control_DatabaseHealth_Label_Title.AutoSize = true;
            this.Control_DatabaseHealth_Label_Title.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Control_DatabaseHealth_Label_Title.Location = new System.Drawing.Point(13, 13);
            this.Control_DatabaseHealth_Label_Title.Name = "Control_DatabaseHealth_Label_Title";
            this.Control_DatabaseHealth_Label_Title.Size = new System.Drawing.Size(135, 21);
            this.Control_DatabaseHealth_Label_Title.TabIndex = 0;
            this.Control_DatabaseHealth_Label_Title.Text = "Database Health";
            // 
            // Control_DatabaseHealth_Panel_Status
            // 
            this.Control_DatabaseHealth_Panel_Status.Controls.Add(this.Control_DatabaseHealth_Label_ActiveConnections);
            this.Control_DatabaseHealth_Panel_Status.Controls.Add(this.Control_DatabaseHealth_Label_ConnectionStatus);
            this.Control_DatabaseHealth_Panel_Status.Dock = System.Windows.Forms.DockStyle.Top;
            this.Control_DatabaseHealth_Panel_Status.Location = new System.Drawing.Point(0, 50);
            this.Control_DatabaseHealth_Panel_Status.Name = "Control_DatabaseHealth_Panel_Status";
            this.Control_DatabaseHealth_Panel_Status.Size = new System.Drawing.Size(800, 60);
            this.Control_DatabaseHealth_Panel_Status.TabIndex = 1;
            // 
            // Control_DatabaseHealth_Label_ActiveConnections
            // 
            this.Control_DatabaseHealth_Label_ActiveConnections.AutoSize = true;
            this.Control_DatabaseHealth_Label_ActiveConnections.Location = new System.Drawing.Point(13, 35);
            this.Control_DatabaseHealth_Label_ActiveConnections.Name = "Control_DatabaseHealth_Label_ActiveConnections";
            this.Control_DatabaseHealth_Label_ActiveConnections.Size = new System.Drawing.Size(112, 15);
            this.Control_DatabaseHealth_Label_ActiveConnections.TabIndex = 1;
            this.Control_DatabaseHealth_Label_ActiveConnections.Text = "Active Connections: -";
            // 
            // Control_DatabaseHealth_Label_ConnectionStatus
            // 
            this.Control_DatabaseHealth_Label_ConnectionStatus.AutoSize = true;
            this.Control_DatabaseHealth_Label_ConnectionStatus.Location = new System.Drawing.Point(13, 10);
            this.Control_DatabaseHealth_Label_ConnectionStatus.Name = "Control_DatabaseHealth_Label_ConnectionStatus";
            this.Control_DatabaseHealth_Label_ConnectionStatus.Size = new System.Drawing.Size(112, 15);
            this.Control_DatabaseHealth_Label_ConnectionStatus.TabIndex = 0;
            this.Control_DatabaseHealth_Label_ConnectionStatus.Text = "Connection Status: -";
            // 
            // Control_DatabaseHealth_DataGridView_Tables
            // 
            this.Control_DatabaseHealth_DataGridView_Tables.AllowUserToAddRows = false;
            this.Control_DatabaseHealth_DataGridView_Tables.AllowUserToDeleteRows = false;
            this.Control_DatabaseHealth_DataGridView_Tables.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Control_DatabaseHealth_DataGridView_Tables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Control_DatabaseHealth_DataGridView_Tables.Location = new System.Drawing.Point(0, 110);
            this.Control_DatabaseHealth_DataGridView_Tables.Name = "Control_DatabaseHealth_DataGridView_Tables";
            this.Control_DatabaseHealth_DataGridView_Tables.ReadOnly = true;
            this.Control_DatabaseHealth_DataGridView_Tables.RowTemplate.Height = 25;
            this.Control_DatabaseHealth_DataGridView_Tables.Size = new System.Drawing.Size(800, 490);
            this.Control_DatabaseHealth_DataGridView_Tables.TabIndex = 2;
            // 
            // Control_DatabaseHealth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Control_DatabaseHealth_DataGridView_Tables);
            this.Controls.Add(this.Control_DatabaseHealth_Panel_Status);
            this.Controls.Add(this.Control_DatabaseHealth_Panel_Header);
            this.Name = "Control_DatabaseHealth";
            this.Size = new System.Drawing.Size(800, 600);
            this.Control_DatabaseHealth_Panel_Header.ResumeLayout(false);
            this.Control_DatabaseHealth_Panel_Header.PerformLayout();
            this.Control_DatabaseHealth_Panel_Status.ResumeLayout(false);
            this.Control_DatabaseHealth_Panel_Status.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Control_DatabaseHealth_DataGridView_Tables)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Control_DatabaseHealth_Panel_Header;
        private System.Windows.Forms.Label Control_DatabaseHealth_Label_Title;
        private System.Windows.Forms.Button Control_DatabaseHealth_Button_Refresh;
        private System.Windows.Forms.Panel Control_DatabaseHealth_Panel_Status;
        private System.Windows.Forms.Label Control_DatabaseHealth_Label_ConnectionStatus;
        private System.Windows.Forms.Label Control_DatabaseHealth_Label_ActiveConnections;
        private System.Windows.Forms.DataGridView Control_DatabaseHealth_DataGridView_Tables;
    }
}
