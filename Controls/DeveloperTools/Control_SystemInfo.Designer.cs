using System.Drawing;
using System.Windows.Forms;

namespace MTM_WIP_Application_Winforms.Controls.DeveloperTools
{
    partial class Control_SystemInfo
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        #region Fields

        private System.ComponentModel.IContainer components = null;

        #endregion

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Control_SystemInfo_GroupBox_Environment = new GroupBox();
            this.Control_SystemInfo_Label_AppVersionValue = new Label();
            this.Control_SystemInfo_Label_AppVersion = new Label();
            this.Control_SystemInfo_Label_UserValue = new Label();
            this.Control_SystemInfo_Label_User = new Label();
            this.Control_SystemInfo_Label_MachineValue = new Label();
            this.Control_SystemInfo_Label_Machine = new Label();
            this.Control_SystemInfo_Label_DotNetValue = new Label();
            this.Control_SystemInfo_Label_DotNet = new Label();
            this.Control_SystemInfo_Label_OSValue = new Label();
            this.Control_SystemInfo_Label_OS = new Label();
            this.Control_SystemInfo_GroupBox_Performance = new GroupBox();
            this.Control_SystemInfo_Label_UptimeValue = new Label();
            this.Control_SystemInfo_Label_Uptime = new Label();
            this.Control_SystemInfo_Label_HandlesValue = new Label();
            this.Control_SystemInfo_Label_Handles = new Label();
            this.Control_SystemInfo_Label_ThreadsValue = new Label();
            this.Control_SystemInfo_Label_Threads = new Label();
            this.Control_SystemInfo_Label_MemoryValue = new Label();
            this.Control_SystemInfo_Label_Memory = new Label();
            this.Control_SystemInfo_GroupBox_Database = new GroupBox();
            this.Control_SystemInfo_Button_TestConnection = new Button();
            this.Control_SystemInfo_Button_RefreshDb = new Button();
            this.Control_SystemInfo_Label_LastQueryValue = new Label();
            this.Control_SystemInfo_Label_LastQuery = new Label();
            this.Control_SystemInfo_Label_DbStatusValue = new Label();
            this.Control_SystemInfo_Label_DbStatus = new Label();
            this.Control_SystemInfo_Label_Connections = new Label();
            this.Control_SystemInfo_Label_ConnectionsValue = new Label();
            this.Control_SystemInfo_Label_DbUptime = new Label();
            this.Control_SystemInfo_Label_DbUptimeValue = new Label();
            this.Control_SystemInfo_Label_DbVersion = new Label();
            this.Control_SystemInfo_Label_DbVersionValue = new Label();
            this.Control_SystemInfo_Label_Queries = new Label();
            this.Control_SystemInfo_Label_QueriesValue = new Label();
            this.Control_SystemInfo_GroupBox_Environment.SuspendLayout();
            this.Control_SystemInfo_GroupBox_Performance.SuspendLayout();
            this.Control_SystemInfo_GroupBox_Database.SuspendLayout();
            this.SuspendLayout();
            // 
            // Control_SystemInfo_GroupBox_Environment
            // 
            this.Control_SystemInfo_GroupBox_Environment.Controls.Add(this.Control_SystemInfo_Label_AppVersionValue);
            this.Control_SystemInfo_GroupBox_Environment.Controls.Add(this.Control_SystemInfo_Label_AppVersion);
            this.Control_SystemInfo_GroupBox_Environment.Controls.Add(this.Control_SystemInfo_Label_UserValue);
            this.Control_SystemInfo_GroupBox_Environment.Controls.Add(this.Control_SystemInfo_Label_User);
            this.Control_SystemInfo_GroupBox_Environment.Controls.Add(this.Control_SystemInfo_Label_MachineValue);
            this.Control_SystemInfo_GroupBox_Environment.Controls.Add(this.Control_SystemInfo_Label_Machine);
            this.Control_SystemInfo_GroupBox_Environment.Controls.Add(this.Control_SystemInfo_Label_DotNetValue);
            this.Control_SystemInfo_GroupBox_Environment.Controls.Add(this.Control_SystemInfo_Label_DotNet);
            this.Control_SystemInfo_GroupBox_Environment.Controls.Add(this.Control_SystemInfo_Label_OSValue);
            this.Control_SystemInfo_GroupBox_Environment.Controls.Add(this.Control_SystemInfo_Label_OS);
            this.Control_SystemInfo_GroupBox_Environment.Location = new Point(20, 20);
            this.Control_SystemInfo_GroupBox_Environment.Name = "Control_SystemInfo_GroupBox_Environment";
            this.Control_SystemInfo_GroupBox_Environment.Size = new Size(350, 200);
            this.Control_SystemInfo_GroupBox_Environment.TabIndex = 0;
            this.Control_SystemInfo_GroupBox_Environment.TabStop = false;
            this.Control_SystemInfo_GroupBox_Environment.Text = "Environment";
            // 
            // Control_SystemInfo_Label_AppVersionValue
            // 
            this.Control_SystemInfo_Label_AppVersionValue.AutoSize = true;
            this.Control_SystemInfo_Label_AppVersionValue.Location = new Point(120, 140);
            this.Control_SystemInfo_Label_AppVersionValue.Name = "Control_SystemInfo_Label_AppVersionValue";
            this.Control_SystemInfo_Label_AppVersionValue.Size = new Size(12, 15);
            this.Control_SystemInfo_Label_AppVersionValue.TabIndex = 9;
            this.Control_SystemInfo_Label_AppVersionValue.Text = "-";
            // 
            // Control_SystemInfo_Label_AppVersion
            // 
            this.Control_SystemInfo_Label_AppVersion.AutoSize = true;
            this.Control_SystemInfo_Label_AppVersion.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.Control_SystemInfo_Label_AppVersion.Location = new Point(20, 140);
            this.Control_SystemInfo_Label_AppVersion.Name = "Control_SystemInfo_Label_AppVersion";
            this.Control_SystemInfo_Label_AppVersion.Size = new Size(77, 15);
            this.Control_SystemInfo_Label_AppVersion.TabIndex = 8;
            this.Control_SystemInfo_Label_AppVersion.Text = "App Version:";
            // 
            // Control_SystemInfo_Label_UserValue
            // 
            this.Control_SystemInfo_Label_UserValue.AutoSize = true;
            this.Control_SystemInfo_Label_UserValue.Location = new Point(120, 110);
            this.Control_SystemInfo_Label_UserValue.Name = "Control_SystemInfo_Label_UserValue";
            this.Control_SystemInfo_Label_UserValue.Size = new Size(12, 15);
            this.Control_SystemInfo_Label_UserValue.TabIndex = 7;
            this.Control_SystemInfo_Label_UserValue.Text = "-";
            // 
            // Control_SystemInfo_Label_User
            // 
            this.Control_SystemInfo_Label_User.AutoSize = true;
            this.Control_SystemInfo_Label_User.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.Control_SystemInfo_Label_User.Location = new Point(20, 110);
            this.Control_SystemInfo_Label_User.Name = "Control_SystemInfo_Label_User";
            this.Control_SystemInfo_Label_User.Size = new Size(36, 15);
            this.Control_SystemInfo_Label_User.TabIndex = 6;
            this.Control_SystemInfo_Label_User.Text = "User:";
            // 
            // Control_SystemInfo_Label_MachineValue
            // 
            this.Control_SystemInfo_Label_MachineValue.AutoSize = true;
            this.Control_SystemInfo_Label_MachineValue.Location = new Point(120, 80);
            this.Control_SystemInfo_Label_MachineValue.Name = "Control_SystemInfo_Label_MachineValue";
            this.Control_SystemInfo_Label_MachineValue.Size = new Size(12, 15);
            this.Control_SystemInfo_Label_MachineValue.TabIndex = 5;
            this.Control_SystemInfo_Label_MachineValue.Text = "-";
            // 
            // Control_SystemInfo_Label_Machine
            // 
            this.Control_SystemInfo_Label_Machine.AutoSize = true;
            this.Control_SystemInfo_Label_Machine.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.Control_SystemInfo_Label_Machine.Location = new Point(20, 80);
            this.Control_SystemInfo_Label_Machine.Name = "Control_SystemInfo_Label_Machine";
            this.Control_SystemInfo_Label_Machine.Size = new Size(57, 15);
            this.Control_SystemInfo_Label_Machine.TabIndex = 4;
            this.Control_SystemInfo_Label_Machine.Text = "Machine:";
            // 
            // Control_SystemInfo_Label_DotNetValue
            // 
            this.Control_SystemInfo_Label_DotNetValue.AutoSize = true;
            this.Control_SystemInfo_Label_DotNetValue.Location = new Point(120, 50);
            this.Control_SystemInfo_Label_DotNetValue.Name = "Control_SystemInfo_Label_DotNetValue";
            this.Control_SystemInfo_Label_DotNetValue.Size = new Size(12, 15);
            this.Control_SystemInfo_Label_DotNetValue.TabIndex = 3;
            this.Control_SystemInfo_Label_DotNetValue.Text = "-";
            // 
            // Control_SystemInfo_Label_DotNet
            // 
            this.Control_SystemInfo_Label_DotNet.AutoSize = true;
            this.Control_SystemInfo_Label_DotNet.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.Control_SystemInfo_Label_DotNet.Location = new Point(20, 50);
            this.Control_SystemInfo_Label_DotNet.Name = "Control_SystemInfo_Label_DotNet";
            this.Control_SystemInfo_Label_DotNet.Size = new Size(37, 15);
            this.Control_SystemInfo_Label_DotNet.TabIndex = 2;
            this.Control_SystemInfo_Label_DotNet.Text = ".NET:";
            // 
            // Control_SystemInfo_Label_OSValue
            // 
            this.Control_SystemInfo_Label_OSValue.AutoSize = true;
            this.Control_SystemInfo_Label_OSValue.Location = new Point(120, 20);
            this.Control_SystemInfo_Label_OSValue.Name = "Control_SystemInfo_Label_OSValue";
            this.Control_SystemInfo_Label_OSValue.Size = new Size(12, 15);
            this.Control_SystemInfo_Label_OSValue.TabIndex = 1;
            this.Control_SystemInfo_Label_OSValue.Text = "-";
            // 
            // Control_SystemInfo_Label_OS
            // 
            this.Control_SystemInfo_Label_OS.AutoSize = true;
            this.Control_SystemInfo_Label_OS.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.Control_SystemInfo_Label_OS.Location = new Point(20, 20);
            this.Control_SystemInfo_Label_OS.Name = "Control_SystemInfo_Label_OS";
            this.Control_SystemInfo_Label_OS.Size = new Size(25, 15);
            this.Control_SystemInfo_Label_OS.TabIndex = 0;
            this.Control_SystemInfo_Label_OS.Text = "OS:";
            // 
            // Control_SystemInfo_GroupBox_Performance
            // 
            this.Control_SystemInfo_GroupBox_Performance.Controls.Add(this.Control_SystemInfo_Label_UptimeValue);
            this.Control_SystemInfo_GroupBox_Performance.Controls.Add(this.Control_SystemInfo_Label_Uptime);
            this.Control_SystemInfo_GroupBox_Performance.Controls.Add(this.Control_SystemInfo_Label_HandlesValue);
            this.Control_SystemInfo_GroupBox_Performance.Controls.Add(this.Control_SystemInfo_Label_Handles);
            this.Control_SystemInfo_GroupBox_Performance.Controls.Add(this.Control_SystemInfo_Label_ThreadsValue);
            this.Control_SystemInfo_GroupBox_Performance.Controls.Add(this.Control_SystemInfo_Label_Threads);
            this.Control_SystemInfo_GroupBox_Performance.Controls.Add(this.Control_SystemInfo_Label_MemoryValue);
            this.Control_SystemInfo_GroupBox_Performance.Controls.Add(this.Control_SystemInfo_Label_Memory);
            this.Control_SystemInfo_GroupBox_Performance.Location = new Point(390, 20);
            this.Control_SystemInfo_GroupBox_Performance.Name = "Control_SystemInfo_GroupBox_Performance";
            this.Control_SystemInfo_GroupBox_Performance.Size = new Size(350, 200);
            this.Control_SystemInfo_GroupBox_Performance.TabIndex = 1;
            this.Control_SystemInfo_GroupBox_Performance.TabStop = false;
            this.Control_SystemInfo_GroupBox_Performance.Text = "Performance";
            // 
            // Control_SystemInfo_Label_UptimeValue
            // 
            this.Control_SystemInfo_Label_UptimeValue.AutoSize = true;
            this.Control_SystemInfo_Label_UptimeValue.Location = new Point(120, 110);
            this.Control_SystemInfo_Label_UptimeValue.Name = "Control_SystemInfo_Label_UptimeValue";
            this.Control_SystemInfo_Label_UptimeValue.Size = new Size(12, 15);
            this.Control_SystemInfo_Label_UptimeValue.TabIndex = 7;
            this.Control_SystemInfo_Label_UptimeValue.Text = "-";
            // 
            // Control_SystemInfo_Label_Uptime
            // 
            this.Control_SystemInfo_Label_Uptime.AutoSize = true;
            this.Control_SystemInfo_Label_Uptime.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.Control_SystemInfo_Label_Uptime.Location = new Point(20, 110);
            this.Control_SystemInfo_Label_Uptime.Name = "Control_SystemInfo_Label_Uptime";
            this.Control_SystemInfo_Label_Uptime.Size = new Size(50, 15);
            this.Control_SystemInfo_Label_Uptime.TabIndex = 6;
            this.Control_SystemInfo_Label_Uptime.Text = "Uptime:";
            // 
            // Control_SystemInfo_Label_HandlesValue
            // 
            this.Control_SystemInfo_Label_HandlesValue.AutoSize = true;
            this.Control_SystemInfo_Label_HandlesValue.Location = new Point(120, 80);
            this.Control_SystemInfo_Label_HandlesValue.Name = "Control_SystemInfo_Label_HandlesValue";
            this.Control_SystemInfo_Label_HandlesValue.Size = new Size(12, 15);
            this.Control_SystemInfo_Label_HandlesValue.TabIndex = 5;
            this.Control_SystemInfo_Label_HandlesValue.Text = "-";
            // 
            // Control_SystemInfo_Label_Handles
            // 
            this.Control_SystemInfo_Label_Handles.AutoSize = true;
            this.Control_SystemInfo_Label_Handles.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.Control_SystemInfo_Label_Handles.Location = new Point(20, 80);
            this.Control_SystemInfo_Label_Handles.Name = "Control_SystemInfo_Label_Handles";
            this.Control_SystemInfo_Label_Handles.Size = new Size(54, 15);
            this.Control_SystemInfo_Label_Handles.TabIndex = 4;
            this.Control_SystemInfo_Label_Handles.Text = "Handles:";
            // 
            // Control_SystemInfo_Label_ThreadsValue
            // 
            this.Control_SystemInfo_Label_ThreadsValue.AutoSize = true;
            this.Control_SystemInfo_Label_ThreadsValue.Location = new Point(120, 50);
            this.Control_SystemInfo_Label_ThreadsValue.Name = "Control_SystemInfo_Label_ThreadsValue";
            this.Control_SystemInfo_Label_ThreadsValue.Size = new Size(12, 15);
            this.Control_SystemInfo_Label_ThreadsValue.TabIndex = 3;
            this.Control_SystemInfo_Label_ThreadsValue.Text = "-";
            // 
            // Control_SystemInfo_Label_Threads
            // 
            this.Control_SystemInfo_Label_Threads.AutoSize = true;
            this.Control_SystemInfo_Label_Threads.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.Control_SystemInfo_Label_Threads.Location = new Point(20, 50);
            this.Control_SystemInfo_Label_Threads.Name = "Control_SystemInfo_Label_Threads";
            this.Control_SystemInfo_Label_Threads.Size = new Size(54, 15);
            this.Control_SystemInfo_Label_Threads.TabIndex = 2;
            this.Control_SystemInfo_Label_Threads.Text = "Threads:";
            // 
            // Control_SystemInfo_Label_MemoryValue
            // 
            this.Control_SystemInfo_Label_MemoryValue.AutoSize = true;
            this.Control_SystemInfo_Label_MemoryValue.Location = new Point(120, 20);
            this.Control_SystemInfo_Label_MemoryValue.Name = "Control_SystemInfo_Label_MemoryValue";
            this.Control_SystemInfo_Label_MemoryValue.Size = new Size(12, 15);
            this.Control_SystemInfo_Label_MemoryValue.TabIndex = 1;
            this.Control_SystemInfo_Label_MemoryValue.Text = "-";
            // 
            // Control_SystemInfo_Label_Memory
            // 
            this.Control_SystemInfo_Label_Memory.AutoSize = true;
            this.Control_SystemInfo_Label_Memory.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.Control_SystemInfo_Label_Memory.Location = new Point(20, 20);
            this.Control_SystemInfo_Label_Memory.Name = "Control_SystemInfo_Label_Memory";
            this.Control_SystemInfo_Label_Memory.Size = new Size(57, 15);
            this.Control_SystemInfo_Label_Memory.TabIndex = 0;
            this.Control_SystemInfo_Label_Memory.Text = "Memory:";
            // 
            // Control_SystemInfo_GroupBox_Database
            // 
            this.Control_SystemInfo_GroupBox_Database.Controls.Add(this.Control_SystemInfo_Label_QueriesValue);
            this.Control_SystemInfo_GroupBox_Database.Controls.Add(this.Control_SystemInfo_Label_Queries);
            this.Control_SystemInfo_GroupBox_Database.Controls.Add(this.Control_SystemInfo_Label_DbVersionValue);
            this.Control_SystemInfo_GroupBox_Database.Controls.Add(this.Control_SystemInfo_Label_DbVersion);
            this.Control_SystemInfo_GroupBox_Database.Controls.Add(this.Control_SystemInfo_Label_DbUptimeValue);
            this.Control_SystemInfo_GroupBox_Database.Controls.Add(this.Control_SystemInfo_Label_DbUptime);
            this.Control_SystemInfo_GroupBox_Database.Controls.Add(this.Control_SystemInfo_Label_ConnectionsValue);
            this.Control_SystemInfo_GroupBox_Database.Controls.Add(this.Control_SystemInfo_Label_Connections);
            this.Control_SystemInfo_GroupBox_Database.Controls.Add(this.Control_SystemInfo_Button_RefreshDb);
            this.Control_SystemInfo_GroupBox_Database.Controls.Add(this.Control_SystemInfo_Button_TestConnection);
            this.Control_SystemInfo_GroupBox_Database.Controls.Add(this.Control_SystemInfo_Label_LastQueryValue);
            this.Control_SystemInfo_GroupBox_Database.Controls.Add(this.Control_SystemInfo_Label_LastQuery);
            this.Control_SystemInfo_GroupBox_Database.Controls.Add(this.Control_SystemInfo_Label_DbStatusValue);
            this.Control_SystemInfo_GroupBox_Database.Controls.Add(this.Control_SystemInfo_Label_DbStatus);
            this.Control_SystemInfo_GroupBox_Database.Location = new Point(20, 240);
            this.Control_SystemInfo_GroupBox_Database.Name = "Control_SystemInfo_GroupBox_Database";
            this.Control_SystemInfo_GroupBox_Database.Size = new Size(350, 220);
            this.Control_SystemInfo_GroupBox_Database.TabIndex = 2;
            this.Control_SystemInfo_GroupBox_Database.TabStop = false;
            this.Control_SystemInfo_GroupBox_Database.Text = "Database";
            // 
            // Control_SystemInfo_Label_Connections
            // 
            this.Control_SystemInfo_Label_Connections.AutoSize = true;
            this.Control_SystemInfo_Label_Connections.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.Control_SystemInfo_Label_Connections.Location = new Point(20, 80);
            this.Control_SystemInfo_Label_Connections.Name = "Control_SystemInfo_Label_Connections";
            this.Control_SystemInfo_Label_Connections.Size = new Size(77, 15);
            this.Control_SystemInfo_Label_Connections.TabIndex = 5;
            this.Control_SystemInfo_Label_Connections.Text = "Connections:";
            // 
            // Control_SystemInfo_Label_ConnectionsValue
            // 
            this.Control_SystemInfo_Label_ConnectionsValue.AutoSize = true;
            this.Control_SystemInfo_Label_ConnectionsValue.Location = new Point(120, 80);
            this.Control_SystemInfo_Label_ConnectionsValue.Name = "Control_SystemInfo_Label_ConnectionsValue";
            this.Control_SystemInfo_Label_ConnectionsValue.Size = new Size(12, 15);
            this.Control_SystemInfo_Label_ConnectionsValue.TabIndex = 6;
            this.Control_SystemInfo_Label_ConnectionsValue.Text = "-";
            // 
            // Control_SystemInfo_Label_DbUptime
            // 
            this.Control_SystemInfo_Label_DbUptime.AutoSize = true;
            this.Control_SystemInfo_Label_DbUptime.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.Control_SystemInfo_Label_DbUptime.Location = new Point(20, 110);
            this.Control_SystemInfo_Label_DbUptime.Name = "Control_SystemInfo_Label_DbUptime";
            this.Control_SystemInfo_Label_DbUptime.Size = new Size(50, 15);
            this.Control_SystemInfo_Label_DbUptime.TabIndex = 7;
            this.Control_SystemInfo_Label_DbUptime.Text = "Uptime:";
            // 
            // Control_SystemInfo_Label_DbUptimeValue
            // 
            this.Control_SystemInfo_Label_DbUptimeValue.AutoSize = true;
            this.Control_SystemInfo_Label_DbUptimeValue.Location = new Point(120, 110);
            this.Control_SystemInfo_Label_DbUptimeValue.Name = "Control_SystemInfo_Label_DbUptimeValue";
            this.Control_SystemInfo_Label_DbUptimeValue.Size = new Size(12, 15);
            this.Control_SystemInfo_Label_DbUptimeValue.TabIndex = 8;
            this.Control_SystemInfo_Label_DbUptimeValue.Text = "-";
            // 
            // Control_SystemInfo_Label_DbVersion
            // 
            this.Control_SystemInfo_Label_DbVersion.AutoSize = true;
            this.Control_SystemInfo_Label_DbVersion.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.Control_SystemInfo_Label_DbVersion.Location = new Point(20, 140);
            this.Control_SystemInfo_Label_DbVersion.Name = "Control_SystemInfo_Label_DbVersion";
            this.Control_SystemInfo_Label_DbVersion.Size = new Size(51, 15);
            this.Control_SystemInfo_Label_DbVersion.TabIndex = 9;
            this.Control_SystemInfo_Label_DbVersion.Text = "Version:";
            // 
            // Control_SystemInfo_Label_DbVersionValue
            // 
            this.Control_SystemInfo_Label_DbVersionValue.AutoSize = true;
            this.Control_SystemInfo_Label_DbVersionValue.Location = new Point(120, 140);
            this.Control_SystemInfo_Label_DbVersionValue.Name = "Control_SystemInfo_Label_DbVersionValue";
            this.Control_SystemInfo_Label_DbVersionValue.Size = new Size(12, 15);
            this.Control_SystemInfo_Label_DbVersionValue.TabIndex = 10;
            this.Control_SystemInfo_Label_DbVersionValue.Text = "-";
            // 
            // Control_SystemInfo_Label_Queries
            // 
            this.Control_SystemInfo_Label_Queries.AutoSize = true;
            this.Control_SystemInfo_Label_Queries.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.Control_SystemInfo_Label_Queries.Location = new Point(20, 170); // Adjusted Y
            this.Control_SystemInfo_Label_Queries.Name = "Control_SystemInfo_Label_Queries";
            this.Control_SystemInfo_Label_Queries.Size = new Size(52, 15);
            this.Control_SystemInfo_Label_Queries.TabIndex = 11;
            this.Control_SystemInfo_Label_Queries.Text = "Queries:";
            // 
            // Control_SystemInfo_Label_QueriesValue
            // 
            this.Control_SystemInfo_Label_QueriesValue.AutoSize = true;
            this.Control_SystemInfo_Label_QueriesValue.Location = new Point(120, 170); // Adjusted Y
            this.Control_SystemInfo_Label_QueriesValue.Name = "Control_SystemInfo_Label_QueriesValue";
            this.Control_SystemInfo_Label_QueriesValue.Size = new Size(12, 15);
            this.Control_SystemInfo_Label_QueriesValue.TabIndex = 12;
            this.Control_SystemInfo_Label_QueriesValue.Text = "-";
            // 
            // Control_SystemInfo_Button_TestConnection
            // 
            this.Control_SystemInfo_Button_TestConnection.Location = new Point(20, 200); // Adjusted Y
            this.Control_SystemInfo_Button_TestConnection.Name = "Control_SystemInfo_Button_TestConnection";
            this.Control_SystemInfo_Button_TestConnection.Size = new Size(120, 30);
            this.Control_SystemInfo_Button_TestConnection.TabIndex = 4;
            this.Control_SystemInfo_Button_TestConnection.Text = "Test Connection";
            this.Control_SystemInfo_Button_TestConnection.UseVisualStyleBackColor = true;
            this.Control_SystemInfo_Button_TestConnection.Click += new System.EventHandler(this.BtnTestConnection_Click);
            // 
            // Control_SystemInfo_Button_RefreshDb
            // 
            this.Control_SystemInfo_Button_RefreshDb.Location = new Point(150, 200);
            this.Control_SystemInfo_Button_RefreshDb.Name = "Control_SystemInfo_Button_RefreshDb";
            this.Control_SystemInfo_Button_RefreshDb.Size = new Size(120, 30);
            this.Control_SystemInfo_Button_RefreshDb.TabIndex = 14;
            this.Control_SystemInfo_Button_RefreshDb.Text = "Update Stats";
            this.Control_SystemInfo_Button_RefreshDb.UseVisualStyleBackColor = true;
            this.Control_SystemInfo_Button_RefreshDb.Click += new System.EventHandler(this.BtnRefreshDb_Click);
            // 
            // Control_SystemInfo_Label_LastQueryValue
            // 
            this.Control_SystemInfo_Label_LastQueryValue.AutoSize = true;
            this.Control_SystemInfo_Label_LastQueryValue.Location = new Point(120, 50);
            this.Control_SystemInfo_Label_LastQueryValue.Name = "Control_SystemInfo_Label_LastQueryValue";
            this.Control_SystemInfo_Label_LastQueryValue.Size = new Size(12, 15);
            this.Control_SystemInfo_Label_LastQueryValue.TabIndex = 3;
            this.Control_SystemInfo_Label_LastQueryValue.Text = "-";
            // 
            // Control_SystemInfo_Label_LastQuery
            // 
            this.Control_SystemInfo_Label_LastQuery.AutoSize = true;
            this.Control_SystemInfo_Label_LastQuery.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.Control_SystemInfo_Label_LastQuery.Location = new Point(20, 50);
            this.Control_SystemInfo_Label_LastQuery.Name = "Control_SystemInfo_Label_LastQuery";
            this.Control_SystemInfo_Label_LastQuery.Size = new Size(69, 15);
            this.Control_SystemInfo_Label_LastQuery.TabIndex = 2;
            this.Control_SystemInfo_Label_LastQuery.Text = "Last Query:";
            // 
            // Control_SystemInfo_Label_DbStatusValue
            // 
            this.Control_SystemInfo_Label_DbStatusValue.AutoSize = true;
            this.Control_SystemInfo_Label_DbStatusValue.Location = new Point(120, 20);
            this.Control_SystemInfo_Label_DbStatusValue.Name = "Control_SystemInfo_Label_DbStatusValue";
            this.Control_SystemInfo_Label_DbStatusValue.Size = new Size(12, 15);
            this.Control_SystemInfo_Label_DbStatusValue.TabIndex = 1;
            this.Control_SystemInfo_Label_DbStatusValue.Text = "-";
            // 
            // Control_SystemInfo_Label_DbStatus
            // 
            this.Control_SystemInfo_Label_DbStatus.AutoSize = true;
            this.Control_SystemInfo_Label_DbStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            this.Control_SystemInfo_Label_DbStatus.Location = new Point(20, 20);
            this.Control_SystemInfo_Label_DbStatus.Name = "Control_SystemInfo_Label_DbStatus";
            this.Control_SystemInfo_Label_DbStatus.Size = new Size(45, 15);
            this.Control_SystemInfo_Label_DbStatus.TabIndex = 0;
            this.Control_SystemInfo_Label_DbStatus.Text = "Status:";
            // 
            // Control_SystemInfo
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.Control_SystemInfo_GroupBox_Database);
            this.Controls.Add(this.Control_SystemInfo_GroupBox_Performance);
            this.Controls.Add(this.Control_SystemInfo_GroupBox_Environment);
            this.Name = "Control_SystemInfo";
            this.Size = new Size(800, 500);
            this.Control_SystemInfo_GroupBox_Environment.ResumeLayout(false);
            this.Control_SystemInfo_GroupBox_Environment.PerformLayout();
            this.Control_SystemInfo_GroupBox_Performance.ResumeLayout(false);
            this.Control_SystemInfo_GroupBox_Performance.PerformLayout();
            this.Control_SystemInfo_GroupBox_Database.ResumeLayout(false);
            this.Control_SystemInfo_GroupBox_Database.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox Control_SystemInfo_GroupBox_Environment;
        private Label Control_SystemInfo_Label_OS;
        private Label Control_SystemInfo_Label_OSValue;
        private Label Control_SystemInfo_Label_DotNet;
        private Label Control_SystemInfo_Label_DotNetValue;
        private Label Control_SystemInfo_Label_Machine;
        private Label Control_SystemInfo_Label_MachineValue;
        private Label Control_SystemInfo_Label_User;
        private Label Control_SystemInfo_Label_UserValue;
        private Label Control_SystemInfo_Label_AppVersion;
        private Label Control_SystemInfo_Label_AppVersionValue;
        private GroupBox Control_SystemInfo_GroupBox_Performance;
        private Label Control_SystemInfo_Label_Memory;
        private Label Control_SystemInfo_Label_MemoryValue;
        private Label Control_SystemInfo_Label_Threads;
        private Label Control_SystemInfo_Label_ThreadsValue;
        private Label Control_SystemInfo_Label_Handles;
        private Label Control_SystemInfo_Label_HandlesValue;
        private Label Control_SystemInfo_Label_Uptime;
        private Label Control_SystemInfo_Label_UptimeValue;
        private GroupBox Control_SystemInfo_GroupBox_Database;
        private Label Control_SystemInfo_Label_DbStatus;
        private Label Control_SystemInfo_Label_DbStatusValue;
        private Label Control_SystemInfo_Label_LastQuery;
        private Label Control_SystemInfo_Label_LastQueryValue;
        private Label Control_SystemInfo_Label_Connections;
        private Label Control_SystemInfo_Label_ConnectionsValue;
        private Label Control_SystemInfo_Label_DbUptime;
        private Label Control_SystemInfo_Label_DbUptimeValue;
        private Label Control_SystemInfo_Label_DbVersion;
        private Label Control_SystemInfo_Label_DbVersionValue;
        private Label Control_SystemInfo_Label_Queries;
        private Label Control_SystemInfo_Label_QueriesValue;
        private Button Control_SystemInfo_Button_TestConnection;
        private Button Control_SystemInfo_Button_RefreshDb;
    }
}



