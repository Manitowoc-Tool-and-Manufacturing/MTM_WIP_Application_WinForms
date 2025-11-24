// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MTM_WIP_Application_Winforms.Controls.SettingsForm
{
    partial class Control_Shortcuts
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
            Control_Shortcuts_GroupBox_Main = new GroupBox();
            Control_Shortcuts_TableLayout_Main = new TableLayoutPanel();
            Control_Shortcuts_TableLayout_Bottom = new TableLayoutPanel();
            Control_Shortcuts_Button_Home = new Button();
            Control_Shortcuts_Button_Reset = new Button();
            Control_Shortcuts_Panel_ScrollContainer = new FlowLayoutPanel();
            Control_Shortcuts_FlowLayoutPanel_Cards = new FlowLayoutPanel();
            Control_Shortcuts_Card_Inventory = new Control_SettingsCollapsibleCard();
            Control_Shortcuts_Card_Remove = new Control_SettingsCollapsibleCard();
            Control_Shortcuts_Card_Transfer = new Control_SettingsCollapsibleCard();
            Control_Shortcuts_Card_AdvancedInventory = new Control_SettingsCollapsibleCard();
            Control_Shortcuts_Card_AdvancedRemove = new Control_SettingsCollapsibleCard();
            Control_Shortcuts_Card_QuickButtons = new Control_SettingsCollapsibleCard();
            Control_Shortcuts_Card_General = new Control_SettingsCollapsibleCard();
            Control_Shortcuts_Card_Navigation = new Control_SettingsCollapsibleCard();
            Control_Shortcuts_Card_Transactions = new Control_SettingsCollapsibleCard();
            Control_Shortcuts_TableLayout_ScrollContainer = new TableLayoutPanel();
            Control_Shortcuts_GroupBox_Main.SuspendLayout();
            Control_Shortcuts_TableLayout_Main.SuspendLayout();
            Control_Shortcuts_TableLayout_Bottom.SuspendLayout();
            Control_Shortcuts_Panel_ScrollContainer.SuspendLayout();
            Control_Shortcuts_FlowLayoutPanel_Cards.SuspendLayout();
            Control_Shortcuts_TableLayout_ScrollContainer.SuspendLayout();
            SuspendLayout();
            // 
            // Control_Shortcuts_GroupBox_Main
            // 
            Control_Shortcuts_GroupBox_Main.AutoSize = true;
            Control_Shortcuts_GroupBox_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_Shortcuts_GroupBox_Main.Controls.Add(Control_Shortcuts_TableLayout_Main);
            Control_Shortcuts_GroupBox_Main.Dock = DockStyle.Fill;
            Control_Shortcuts_GroupBox_Main.Location = new Point(0, 0);
            Control_Shortcuts_GroupBox_Main.Name = "Control_Shortcuts_GroupBox_Main";
            Control_Shortcuts_GroupBox_Main.Size = new Size(532, 705);
            Control_Shortcuts_GroupBox_Main.TabIndex = 0;
            Control_Shortcuts_GroupBox_Main.TabStop = false;
            Control_Shortcuts_GroupBox_Main.Text = "Edit Shortcuts";
            // 
            // Control_Shortcuts_TableLayout_Main
            // 
            Control_Shortcuts_TableLayout_Main.AutoSize = true;
            Control_Shortcuts_TableLayout_Main.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_Shortcuts_TableLayout_Main.ColumnCount = 1;
            Control_Shortcuts_TableLayout_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_Shortcuts_TableLayout_Main.Controls.Add(Control_Shortcuts_TableLayout_Bottom, 0, 1);
            Control_Shortcuts_TableLayout_Main.Controls.Add(Control_Shortcuts_Panel_ScrollContainer, 0, 0);
            Control_Shortcuts_TableLayout_Main.Dock = DockStyle.Fill;
            Control_Shortcuts_TableLayout_Main.Location = new Point(3, 19);
            Control_Shortcuts_TableLayout_Main.Name = "Control_Shortcuts_TableLayout_Main";
            Control_Shortcuts_TableLayout_Main.RowCount = 2;
            Control_Shortcuts_TableLayout_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_Shortcuts_TableLayout_Main.RowStyles.Add(new RowStyle());
            Control_Shortcuts_TableLayout_Main.Size = new Size(526, 683);
            Control_Shortcuts_TableLayout_Main.TabIndex = 0;
            // 
            // Control_Shortcuts_TableLayout_Bottom
            // 
            Control_Shortcuts_TableLayout_Bottom.AutoSize = true;
            Control_Shortcuts_TableLayout_Bottom.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_Shortcuts_TableLayout_Bottom.ColumnCount = 3;
            Control_Shortcuts_TableLayout_Bottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_Shortcuts_TableLayout_Bottom.ColumnStyles.Add(new ColumnStyle());
            Control_Shortcuts_TableLayout_Bottom.ColumnStyles.Add(new ColumnStyle());
            Control_Shortcuts_TableLayout_Bottom.Controls.Add(Control_Shortcuts_Button_Home, 1, 0);
            Control_Shortcuts_TableLayout_Bottom.Controls.Add(Control_Shortcuts_Button_Reset, 2, 0);
            Control_Shortcuts_TableLayout_Bottom.Dock = DockStyle.Fill;
            Control_Shortcuts_TableLayout_Bottom.Location = new Point(3, 651);
            Control_Shortcuts_TableLayout_Bottom.Name = "Control_Shortcuts_TableLayout_Bottom";
            Control_Shortcuts_TableLayout_Bottom.RowCount = 1;
            Control_Shortcuts_TableLayout_Bottom.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_Shortcuts_TableLayout_Bottom.Size = new Size(520, 29);
            Control_Shortcuts_TableLayout_Bottom.TabIndex = 2;
            // 
            // Control_Shortcuts_Button_Home
            // 
            Control_Shortcuts_Button_Home.Location = new Point(361, 3);
            Control_Shortcuts_Button_Home.Name = "Control_Shortcuts_Button_Home";
            Control_Shortcuts_Button_Home.Size = new Size(75, 23);
            Control_Shortcuts_Button_Home.TabIndex = 1;
            Control_Shortcuts_Button_Home.Text = "Home";
            Control_Shortcuts_Button_Home.UseVisualStyleBackColor = true;
            // 
            // Control_Shortcuts_Button_Reset
            // 
            Control_Shortcuts_Button_Reset.Location = new Point(442, 3);
            Control_Shortcuts_Button_Reset.Name = "Control_Shortcuts_Button_Reset";
            Control_Shortcuts_Button_Reset.Size = new Size(75, 23);
            Control_Shortcuts_Button_Reset.TabIndex = 0;
            Control_Shortcuts_Button_Reset.Text = "Reset All";
            Control_Shortcuts_Button_Reset.UseVisualStyleBackColor = true;
            // 
            // Control_Shortcuts_Panel_ScrollContainer
            // 
            Control_Shortcuts_Panel_ScrollContainer.AutoScroll = true;
            Control_Shortcuts_Panel_ScrollContainer.AutoSize = true;
            Control_Shortcuts_Panel_ScrollContainer.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_Shortcuts_Panel_ScrollContainer.Controls.Add(Control_Shortcuts_TableLayout_ScrollContainer);
            Control_Shortcuts_Panel_ScrollContainer.Dock = DockStyle.Fill;
            Control_Shortcuts_Panel_ScrollContainer.Location = new Point(3, 3);
            Control_Shortcuts_Panel_ScrollContainer.Name = "Control_Shortcuts_Panel_ScrollContainer";
            Control_Shortcuts_Panel_ScrollContainer.Size = new Size(520, 642);
            Control_Shortcuts_Panel_ScrollContainer.TabIndex = 3;
            // 
            // Control_Shortcuts_FlowLayoutPanel_Cards
            // 
            Control_Shortcuts_FlowLayoutPanel_Cards.AutoSize = true;
            Control_Shortcuts_FlowLayoutPanel_Cards.Controls.Add(Control_Shortcuts_Card_Inventory);
            Control_Shortcuts_FlowLayoutPanel_Cards.Controls.Add(Control_Shortcuts_Card_Remove);
            Control_Shortcuts_FlowLayoutPanel_Cards.Controls.Add(Control_Shortcuts_Card_Transfer);
            Control_Shortcuts_FlowLayoutPanel_Cards.Controls.Add(Control_Shortcuts_Card_AdvancedInventory);
            Control_Shortcuts_FlowLayoutPanel_Cards.Controls.Add(Control_Shortcuts_Card_AdvancedRemove);
            Control_Shortcuts_FlowLayoutPanel_Cards.Controls.Add(Control_Shortcuts_Card_QuickButtons);
            Control_Shortcuts_FlowLayoutPanel_Cards.Controls.Add(Control_Shortcuts_Card_General);
            Control_Shortcuts_FlowLayoutPanel_Cards.Controls.Add(Control_Shortcuts_Card_Navigation);
            Control_Shortcuts_FlowLayoutPanel_Cards.Controls.Add(Control_Shortcuts_Card_Transactions);
            Control_Shortcuts_FlowLayoutPanel_Cards.Dock = DockStyle.Fill;
            Control_Shortcuts_FlowLayoutPanel_Cards.Location = new Point(33, 3);
            Control_Shortcuts_FlowLayoutPanel_Cards.Name = "Control_Shortcuts_FlowLayoutPanel_Cards";
            Control_Shortcuts_FlowLayoutPanel_Cards.Size = new Size(448, 630);
            Control_Shortcuts_FlowLayoutPanel_Cards.TabIndex = 0;
            // 
            // Control_Shortcuts_Card_Inventory
            // 
            Control_Shortcuts_Card_Inventory.AccentColor = Color.FromArgb(0, 120, 212);
            Control_Shortcuts_Card_Inventory.BackColor = Color.FromArgb(200, 200, 200);
            Control_Shortcuts_Card_Inventory.CardDescription = "Shortcuts for the main Inventory tab operations.";
            Control_Shortcuts_Card_Inventory.CardIcon = "📦";
            Control_Shortcuts_Card_Inventory.CardTitle = "Inventory Tab";
            Control_Shortcuts_Card_Inventory.IsExpanded = true;
            Control_Shortcuts_Card_Inventory.Location = new Point(3, 3);
            Control_Shortcuts_Card_Inventory.MinimumSize = new Size(442, 64);
            Control_Shortcuts_Card_Inventory.Name = "Control_Shortcuts_Card_Inventory";
            Control_Shortcuts_Card_Inventory.Padding = new Padding(1);
            Control_Shortcuts_Card_Inventory.Size = new Size(442, 64);
            Control_Shortcuts_Card_Inventory.TabIndex = 9;
            // 
            // Control_Shortcuts_Card_Remove
            // 
            Control_Shortcuts_Card_Remove.AccentColor = Color.FromArgb(0, 120, 212);
            Control_Shortcuts_Card_Remove.BackColor = Color.FromArgb(200, 200, 200);
            Control_Shortcuts_Card_Remove.CardDescription = "Shortcuts for the Remove tab operations.";
            Control_Shortcuts_Card_Remove.CardIcon = "🗑️";
            Control_Shortcuts_Card_Remove.CardTitle = "Remove Tab";
            Control_Shortcuts_Card_Remove.IsExpanded = true;
            Control_Shortcuts_Card_Remove.Location = new Point(3, 73);
            Control_Shortcuts_Card_Remove.MinimumSize = new Size(442, 64);
            Control_Shortcuts_Card_Remove.Name = "Control_Shortcuts_Card_Remove";
            Control_Shortcuts_Card_Remove.Padding = new Padding(1);
            Control_Shortcuts_Card_Remove.Size = new Size(442, 64);
            Control_Shortcuts_Card_Remove.TabIndex = 10;
            // 
            // Control_Shortcuts_Card_Transfer
            // 
            Control_Shortcuts_Card_Transfer.AccentColor = Color.FromArgb(0, 120, 212);
            Control_Shortcuts_Card_Transfer.BackColor = Color.FromArgb(200, 200, 200);
            Control_Shortcuts_Card_Transfer.CardDescription = "Shortcuts for the Transfer tab operations.";
            Control_Shortcuts_Card_Transfer.CardIcon = "↔️";
            Control_Shortcuts_Card_Transfer.CardTitle = "Transfer Tab";
            Control_Shortcuts_Card_Transfer.IsExpanded = true;
            Control_Shortcuts_Card_Transfer.Location = new Point(3, 143);
            Control_Shortcuts_Card_Transfer.MinimumSize = new Size(442, 64);
            Control_Shortcuts_Card_Transfer.Name = "Control_Shortcuts_Card_Transfer";
            Control_Shortcuts_Card_Transfer.Padding = new Padding(1);
            Control_Shortcuts_Card_Transfer.Size = new Size(442, 64);
            Control_Shortcuts_Card_Transfer.TabIndex = 11;
            // 
            // Control_Shortcuts_Card_AdvancedInventory
            // 
            Control_Shortcuts_Card_AdvancedInventory.AccentColor = Color.FromArgb(0, 120, 212);
            Control_Shortcuts_Card_AdvancedInventory.BackColor = Color.FromArgb(200, 200, 200);
            Control_Shortcuts_Card_AdvancedInventory.CardDescription = "Shortcuts for Advanced Inventory management.";
            Control_Shortcuts_Card_AdvancedInventory.CardIcon = "🏭";
            Control_Shortcuts_Card_AdvancedInventory.CardTitle = "Advanced Inventory";
            Control_Shortcuts_Card_AdvancedInventory.IsExpanded = true;
            Control_Shortcuts_Card_AdvancedInventory.Location = new Point(3, 213);
            Control_Shortcuts_Card_AdvancedInventory.MinimumSize = new Size(442, 64);
            Control_Shortcuts_Card_AdvancedInventory.Name = "Control_Shortcuts_Card_AdvancedInventory";
            Control_Shortcuts_Card_AdvancedInventory.Padding = new Padding(1);
            Control_Shortcuts_Card_AdvancedInventory.Size = new Size(442, 64);
            Control_Shortcuts_Card_AdvancedInventory.TabIndex = 12;
            // 
            // Control_Shortcuts_Card_AdvancedRemove
            // 
            Control_Shortcuts_Card_AdvancedRemove.AccentColor = Color.FromArgb(0, 120, 212);
            Control_Shortcuts_Card_AdvancedRemove.BackColor = Color.FromArgb(200, 200, 200);
            Control_Shortcuts_Card_AdvancedRemove.CardDescription = "Shortcuts for Advanced Remove operations.";
            Control_Shortcuts_Card_AdvancedRemove.CardIcon = "📤";
            Control_Shortcuts_Card_AdvancedRemove.CardTitle = "Advanced Remove";
            Control_Shortcuts_Card_AdvancedRemove.IsExpanded = true;
            Control_Shortcuts_Card_AdvancedRemove.Location = new Point(3, 283);
            Control_Shortcuts_Card_AdvancedRemove.MinimumSize = new Size(442, 64);
            Control_Shortcuts_Card_AdvancedRemove.Name = "Control_Shortcuts_Card_AdvancedRemove";
            Control_Shortcuts_Card_AdvancedRemove.Padding = new Padding(1);
            Control_Shortcuts_Card_AdvancedRemove.Size = new Size(442, 64);
            Control_Shortcuts_Card_AdvancedRemove.TabIndex = 13;
            // 
            // Control_Shortcuts_Card_QuickButtons
            // 
            Control_Shortcuts_Card_QuickButtons.AccentColor = Color.FromArgb(0, 120, 212);
            Control_Shortcuts_Card_QuickButtons.BackColor = Color.FromArgb(200, 200, 200);
            Control_Shortcuts_Card_QuickButtons.CardDescription = "Customizable shortcuts for the Quick Button bar.";
            Control_Shortcuts_Card_QuickButtons.CardIcon = "⚡";
            Control_Shortcuts_Card_QuickButtons.CardTitle = "Quick Buttons";
            Control_Shortcuts_Card_QuickButtons.IsExpanded = true;
            Control_Shortcuts_Card_QuickButtons.Location = new Point(3, 353);
            Control_Shortcuts_Card_QuickButtons.MinimumSize = new Size(442, 64);
            Control_Shortcuts_Card_QuickButtons.Name = "Control_Shortcuts_Card_QuickButtons";
            Control_Shortcuts_Card_QuickButtons.Padding = new Padding(1);
            Control_Shortcuts_Card_QuickButtons.Size = new Size(442, 64);
            Control_Shortcuts_Card_QuickButtons.TabIndex = 14;
            // 
            // Control_Shortcuts_Card_General
            // 
            Control_Shortcuts_Card_General.AccentColor = Color.FromArgb(0, 120, 212);
            Control_Shortcuts_Card_General.BackColor = Color.FromArgb(200, 200, 200);
            Control_Shortcuts_Card_General.CardDescription = "General application shortcuts.";
            Control_Shortcuts_Card_General.CardIcon = "⚙️";
            Control_Shortcuts_Card_General.CardTitle = "General";
            Control_Shortcuts_Card_General.IsExpanded = true;
            Control_Shortcuts_Card_General.Location = new Point(3, 423);
            Control_Shortcuts_Card_General.MinimumSize = new Size(442, 64);
            Control_Shortcuts_Card_General.Name = "Control_Shortcuts_Card_General";
            Control_Shortcuts_Card_General.Padding = new Padding(1);
            Control_Shortcuts_Card_General.Size = new Size(442, 64);
            Control_Shortcuts_Card_General.TabIndex = 17;
            // 
            // Control_Shortcuts_Card_Navigation
            // 
            Control_Shortcuts_Card_Navigation.AccentColor = Color.FromArgb(0, 120, 212);
            Control_Shortcuts_Card_Navigation.BackColor = Color.FromArgb(200, 200, 200);
            Control_Shortcuts_Card_Navigation.CardDescription = "Global navigation shortcuts.";
            Control_Shortcuts_Card_Navigation.CardIcon = "\U0001f9ed";
            Control_Shortcuts_Card_Navigation.CardTitle = "Navigation";
            Control_Shortcuts_Card_Navigation.IsExpanded = true;
            Control_Shortcuts_Card_Navigation.Location = new Point(3, 493);
            Control_Shortcuts_Card_Navigation.MinimumSize = new Size(442, 64);
            Control_Shortcuts_Card_Navigation.Name = "Control_Shortcuts_Card_Navigation";
            Control_Shortcuts_Card_Navigation.Padding = new Padding(1);
            Control_Shortcuts_Card_Navigation.Size = new Size(442, 64);
            Control_Shortcuts_Card_Navigation.TabIndex = 15;
            // 
            // Control_Shortcuts_Card_Transactions
            // 
            Control_Shortcuts_Card_Transactions.AccentColor = Color.FromArgb(0, 120, 212);
            Control_Shortcuts_Card_Transactions.BackColor = Color.FromArgb(200, 200, 200);
            Control_Shortcuts_Card_Transactions.CardDescription = "Shortcuts for the Transaction Viewer.";
            Control_Shortcuts_Card_Transactions.CardIcon = "📝";
            Control_Shortcuts_Card_Transactions.CardTitle = "Transactions Viewer";
            Control_Shortcuts_Card_Transactions.IsExpanded = true;
            Control_Shortcuts_Card_Transactions.Location = new Point(3, 563);
            Control_Shortcuts_Card_Transactions.MinimumSize = new Size(442, 64);
            Control_Shortcuts_Card_Transactions.Name = "Control_Shortcuts_Card_Transactions";
            Control_Shortcuts_Card_Transactions.Padding = new Padding(1);
            Control_Shortcuts_Card_Transactions.Size = new Size(442, 64);
            Control_Shortcuts_Card_Transactions.TabIndex = 16;
            // 
            // Control_Shortcuts_TableLayout_ScrollContainer
            // 
            Control_Shortcuts_TableLayout_ScrollContainer.AutoSize = true;
            Control_Shortcuts_TableLayout_ScrollContainer.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Control_Shortcuts_TableLayout_ScrollContainer.ColumnCount = 3;
            Control_Shortcuts_TableLayout_ScrollContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            Control_Shortcuts_TableLayout_ScrollContainer.ColumnStyles.Add(new ColumnStyle());
            Control_Shortcuts_TableLayout_ScrollContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30F));
            Control_Shortcuts_TableLayout_ScrollContainer.Controls.Add(Control_Shortcuts_FlowLayoutPanel_Cards, 1, 0);
            Control_Shortcuts_TableLayout_ScrollContainer.Dock = DockStyle.Fill;
            Control_Shortcuts_TableLayout_ScrollContainer.Location = new Point(3, 3);
            Control_Shortcuts_TableLayout_ScrollContainer.Name = "Control_Shortcuts_TableLayout_ScrollContainer";
            Control_Shortcuts_TableLayout_ScrollContainer.RowCount = 1;
            Control_Shortcuts_TableLayout_ScrollContainer.RowStyles.Add(new RowStyle());
            Control_Shortcuts_TableLayout_ScrollContainer.Size = new Size(514, 636);
            Control_Shortcuts_TableLayout_ScrollContainer.TabIndex = 1;
            Control_Shortcuts_TableLayout_ScrollContainer.Paint += Control_Shortcuts_TableLayout_ScrollContainer_Paint;
            // 
            // Control_Shortcuts
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(Control_Shortcuts_GroupBox_Main);
            Name = "Control_Shortcuts";
            Size = new Size(532, 705);
            Control_Shortcuts_GroupBox_Main.ResumeLayout(false);
            Control_Shortcuts_GroupBox_Main.PerformLayout();
            Control_Shortcuts_TableLayout_Main.ResumeLayout(false);
            Control_Shortcuts_TableLayout_Main.PerformLayout();
            Control_Shortcuts_TableLayout_Bottom.ResumeLayout(false);
            Control_Shortcuts_Panel_ScrollContainer.ResumeLayout(false);
            Control_Shortcuts_Panel_ScrollContainer.PerformLayout();
            Control_Shortcuts_FlowLayoutPanel_Cards.ResumeLayout(false);
            Control_Shortcuts_TableLayout_ScrollContainer.ResumeLayout(false);
            Control_Shortcuts_TableLayout_ScrollContainer.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox Control_Shortcuts_GroupBox_Main;
        private TableLayoutPanel Control_Shortcuts_TableLayout_Main;
        private TableLayoutPanel Control_Shortcuts_TableLayout_Bottom;
        private Button Control_Shortcuts_Button_Reset;
        private Button Control_Shortcuts_Button_Home;
        private FlowLayoutPanel Control_Shortcuts_Panel_ScrollContainer;
        private FlowLayoutPanel Control_Shortcuts_FlowLayoutPanel_Cards;
        private Control_SettingsCollapsibleCard Control_Shortcuts_Card_General;
        private Control_SettingsCollapsibleCard Control_Shortcuts_Card_Transactions;
        private Control_SettingsCollapsibleCard Control_Shortcuts_Card_Navigation;
        private Control_SettingsCollapsibleCard Control_Shortcuts_Card_QuickButtons;
        private Control_SettingsCollapsibleCard Control_Shortcuts_Card_AdvancedRemove;
        private Control_SettingsCollapsibleCard Control_Shortcuts_Card_AdvancedInventory;
        private Control_SettingsCollapsibleCard Control_Shortcuts_Card_Transfer;
        private Control_SettingsCollapsibleCard Control_Shortcuts_Card_Remove;
        private Control_SettingsCollapsibleCard Control_Shortcuts_Card_Inventory;
        private TableLayoutPanel Control_Shortcuts_TableLayout_ScrollContainer;
    }
}
