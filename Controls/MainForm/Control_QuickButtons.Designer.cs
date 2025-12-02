using System.Drawing;
using System.Windows.Forms;

using MTM_WIP_Application_Winforms.Forms.Shared;

namespace MTM_WIP_Application_Winforms.Controls.MainForm
{
    partial class Control_QuickButtons : ThemedUserControl
    {
        #region Fields
        


        private System.ComponentModel.IContainer components = null;

        #endregion

        private TableLayoutPanel Control_QuickButtons_TableLayoutPanel_Main;
        private ToolTip Control_QuickButtons_Tooltip;
        private ContextMenuStrip Control_QuickButtons_ContextMenu;
        private ToolStripMenuItem menuItemRemove;
        private ToolStripMenuItem menuItemReorder;
        private ToolStripMenuItem menuItemEdit;
        

        
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

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Control_QuickButtons_TableLayoutPanel_Main = new TableLayoutPanel();
            Control_QuickButtons_Tooltip = new ToolTip(components);
            Control_QuickButtons_ContextMenu = new ContextMenuStrip(components);
            menuItemEdit = new ToolStripMenuItem();
            menuItemRemove = new ToolStripMenuItem();
            menuItemReorder = new ToolStripMenuItem();
            Control_QuickButtons_ContextMenu.SuspendLayout();
            SuspendLayout();
            // 
            // Control_QuickButtons_TableLayoutPanel_Main
            // 
            Control_QuickButtons_TableLayoutPanel_Main.ColumnCount = 1;
            Control_QuickButtons_TableLayoutPanel_Main.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            Control_QuickButtons_TableLayoutPanel_Main.Dock = DockStyle.Fill;
            Control_QuickButtons_TableLayoutPanel_Main.Location = new Point(0, 0);
            Control_QuickButtons_TableLayoutPanel_Main.Name = "Control_QuickButtons_TableLayoutPanel_Main";
            Control_QuickButtons_TableLayoutPanel_Main.RowCount = 11;
            Control_QuickButtons_TableLayoutPanel_Main.RowStyles.Add(new RowStyle());
            Control_QuickButtons_TableLayoutPanel_Main.RowStyles.Add(new RowStyle());
            Control_QuickButtons_TableLayoutPanel_Main.RowStyles.Add(new RowStyle());
            Control_QuickButtons_TableLayoutPanel_Main.RowStyles.Add(new RowStyle());
            Control_QuickButtons_TableLayoutPanel_Main.RowStyles.Add(new RowStyle());
            Control_QuickButtons_TableLayoutPanel_Main.RowStyles.Add(new RowStyle());
            Control_QuickButtons_TableLayoutPanel_Main.RowStyles.Add(new RowStyle());
            Control_QuickButtons_TableLayoutPanel_Main.RowStyles.Add(new RowStyle());
            Control_QuickButtons_TableLayoutPanel_Main.RowStyles.Add(new RowStyle());
            Control_QuickButtons_TableLayoutPanel_Main.RowStyles.Add(new RowStyle());
            Control_QuickButtons_TableLayoutPanel_Main.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Control_QuickButtons_TableLayoutPanel_Main.Size = new Size(236, 520);
            Control_QuickButtons_TableLayoutPanel_Main.TabIndex = 0;
            // 
            // Control_QuickButtons_ContextMenu
            // 
            Control_QuickButtons_ContextMenu.Items.AddRange(new ToolStripItem[] { menuItemEdit, menuItemRemove, menuItemReorder });
            Control_QuickButtons_ContextMenu.Name = "Control_QuickButtons_ContextMenu";
            Control_QuickButtons_ContextMenu.Size = new Size(149, 76);
            // 
            // menuItemEdit
            // 
            menuItemEdit.Name = "menuItemEdit";
            menuItemEdit.Size = new Size(148, 22);
            menuItemEdit.Text = "Edit";
            // 
            // menuItemRemove
            // 
            menuItemRemove.Name = "menuItemRemove";
            menuItemRemove.Size = new Size(148, 22);
            menuItemRemove.Text = "Remove";
            // 
            // menuItemReorder
            // 
            menuItemReorder.Name = "menuItemReorder";
            menuItemReorder.Size = new Size(148, 22);
            menuItemReorder.Text = "Change Order";
            // 
            // Control_QuickButtons
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            AutoSize = true;
            Controls.Add(Control_QuickButtons_TableLayoutPanel_Main);
            Name = "Control_QuickButtons";
            Size = new Size(236, 520);
            Control_QuickButtons_ContextMenu.ResumeLayout(false);
            ResumeLayout(false);
        }
    }

        
        #endregion
    }
