namespace SDRSharp.Rtl_433
{
    partial class FormConsole
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConsole));
            this.panel1 = new System.Windows.Forms.Panel();
            this.listViewConsole = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStripConsole = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copySelectedItemsToClipboardToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAllItemsToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripConsole = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonCopySelectedItemToClipboard = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonCopyAllItemsToClipboard = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAlwaysOnTop = new System.Windows.Forms.ToolStripButton();
            this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copySelectedItemsToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.contextMenuStripConsole.SuspendLayout();
            this.toolStripConsole.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listViewConsole);
            this.panel1.Controls.Add(this.toolStripConsole);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // listViewConsole
            // 
            this.listViewConsole.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewConsole.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listViewConsole.ContextMenuStrip = this.contextMenuStripConsole;
            resources.ApplyResources(this.listViewConsole, "listViewConsole");
            this.listViewConsole.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewConsole.HideSelection = false;
            this.listViewConsole.Name = "listViewConsole";
            this.listViewConsole.UseCompatibleStateImageBehavior = false;
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // contextMenuStripConsole
            // 
            this.contextMenuStripConsole.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copySelectedItemsToClipboardToolStripMenuItem1,
            this.copyAllItemsToClipboardToolStripMenuItem});
            this.contextMenuStripConsole.Name = "contextMenuStripConsole";
            resources.ApplyResources(this.contextMenuStripConsole, "contextMenuStripConsole");
            // 
            // copySelectedItemsToClipboardToolStripMenuItem1
            // 
            this.copySelectedItemsToClipboardToolStripMenuItem1.Name = "copySelectedItemsToClipboardToolStripMenuItem1";
            resources.ApplyResources(this.copySelectedItemsToClipboardToolStripMenuItem1, "copySelectedItemsToClipboardToolStripMenuItem1");
            this.copySelectedItemsToClipboardToolStripMenuItem1.Click += new System.EventHandler(this.copySelectedItemsToClipboardToolStripMenuItem1_Click);
            // 
            // copyAllItemsToClipboardToolStripMenuItem
            // 
            this.copyAllItemsToClipboardToolStripMenuItem.Name = "copyAllItemsToClipboardToolStripMenuItem";
            resources.ApplyResources(this.copyAllItemsToClipboardToolStripMenuItem, "copyAllItemsToClipboardToolStripMenuItem");
            this.copyAllItemsToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyAllItemsToClipboardToolStripMenuItem_Click);
            // 
            // toolStripConsole
            // 
            this.toolStripConsole.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonCopySelectedItemToClipboard,
            this.toolStripButtonCopyAllItemsToClipboard,
            this.toolStripButtonAlwaysOnTop});
            this.toolStripConsole.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            resources.ApplyResources(this.toolStripConsole, "toolStripConsole");
            this.toolStripConsole.Name = "toolStripConsole";
            // 
            // toolStripButtonCopySelectedItemToClipboard
            // 
            this.toolStripButtonCopySelectedItemToClipboard.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.toolStripButtonCopySelectedItemToClipboard, "toolStripButtonCopySelectedItemToClipboard");
            this.toolStripButtonCopySelectedItemToClipboard.Name = "toolStripButtonCopySelectedItemToClipboard";
            this.toolStripButtonCopySelectedItemToClipboard.Click += new System.EventHandler(this.copySelectedItemsToClipboardToolStripMenuItem1_Click);
            // 
            // toolStripButtonCopyAllItemsToClipboard
            // 
            this.toolStripButtonCopyAllItemsToClipboard.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.toolStripButtonCopyAllItemsToClipboard, "toolStripButtonCopyAllItemsToClipboard");
            this.toolStripButtonCopyAllItemsToClipboard.Name = "toolStripButtonCopyAllItemsToClipboard";
            this.toolStripButtonCopyAllItemsToClipboard.Click += new System.EventHandler(this.copyAllItemsToClipboardToolStripMenuItem_Click);
            // 
            // toolStripButtonAlwaysOnTop
            // 
            this.toolStripButtonAlwaysOnTop.Checked = true;
            this.toolStripButtonAlwaysOnTop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripButtonAlwaysOnTop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            resources.ApplyResources(this.toolStripButtonAlwaysOnTop, "toolStripButtonAlwaysOnTop");
            this.toolStripButtonAlwaysOnTop.Name = "toolStripButtonAlwaysOnTop";
            this.toolStripButtonAlwaysOnTop.Click += new System.EventHandler(this.toolStripButtonAlwaysOnTop_Click);
            // 
            // copyToClipboardToolStripMenuItem
            // 
            this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            resources.ApplyResources(this.copyToClipboardToolStripMenuItem, "copyToClipboardToolStripMenuItem");
            // 
            // copySelectedItemsToClipboardToolStripMenuItem
            // 
            this.copySelectedItemsToClipboardToolStripMenuItem.Name = "copySelectedItemsToClipboardToolStripMenuItem";
            resources.ApplyResources(this.copySelectedItemsToClipboardToolStripMenuItem, "copySelectedItemsToClipboardToolStripMenuItem");
            // 
            // FormConsole
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "FormConsole";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormConsole_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormConsole_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuStripConsole.ResumeLayout(false);
            this.toolStripConsole.ResumeLayout(false);
            this.toolStripConsole.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView listViewConsole;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripConsole;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyAllItemsToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copySelectedItemsToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copySelectedItemsToClipboardToolStripMenuItem1;
        private System.Windows.Forms.ToolStrip toolStripConsole;
        private System.Windows.Forms.ToolStripButton toolStripButtonCopySelectedItemToClipboard;
        private System.Windows.Forms.ToolStripButton toolStripButtonCopyAllItemsToClipboard;
        private System.Windows.Forms.ToolStripButton toolStripButtonAlwaysOnTop;
    }
}

