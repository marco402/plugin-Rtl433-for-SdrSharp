namespace SDRSharp.Rtl_433
{
    partial class FormDevicesListMessages
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
            this.listViewListMessages = new System.Windows.Forms.ListView();
            this.statusStripExport = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelExport = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelDevices = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripExport.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewListMessages
            // 
            this.listViewListMessages.Dock = System.Windows.Forms.DockStyle.Top;
            this.listViewListMessages.GridLines = true;
            this.listViewListMessages.HideSelection = false;
            this.listViewListMessages.Location = new System.Drawing.Point(0, 0);
            this.listViewListMessages.Name = "listViewListMessages";
            this.listViewListMessages.Size = new System.Drawing.Size(1034, 124);
            this.listViewListMessages.TabIndex = 0;
            this.listViewListMessages.UseCompatibleStateImageBehavior = false;
            this.listViewListMessages.View = System.Windows.Forms.View.List;
            this.listViewListMessages.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.listViewListMessages_RetrieveVirtualItem);
            // 
            // statusStripExport
            // 
            this.statusStripExport.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelExport,
            this.toolStripStatusLabelDevices});
            this.statusStripExport.Location = new System.Drawing.Point(0, 124);
            this.statusStripExport.Name = "statusStripExport";
            this.statusStripExport.Size = new System.Drawing.Size(1034, 22);
            this.statusStripExport.TabIndex = 1;
            // 
            // toolStripStatusLabelExport
            // 
            this.toolStripStatusLabelExport.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.toolStripStatusLabelExport.ForeColor = System.Drawing.Color.Cyan;
            this.toolStripStatusLabelExport.Name = "toolStripStatusLabelExport";
            this.toolStripStatusLabelExport.Size = new System.Drawing.Size(67, 17);
            this.toolStripStatusLabelExport.Text = "Export data";
            this.toolStripStatusLabelExport.Click += new System.EventHandler(this.toolStripStatusLabelExport_Click);
            // 
            // toolStripStatusLabelDevices
            // 
            this.toolStripStatusLabelDevices.ForeColor = System.Drawing.Color.Red;
            this.toolStripStatusLabelDevices.Name = "toolStripStatusLabelDevices";
            this.toolStripStatusLabelDevices.Size = new System.Drawing.Size(205, 17);
            this.toolStripStatusLabelDevices.Text = " (WARNING replace the file if it exists)";
            // 
            // FormDevicesListMessages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 146);
            this.Controls.Add(this.listViewListMessages);
            this.Controls.Add(this.statusStripExport);
            this.Name = "FormDevicesListMessages";
            this.Text = "FormDevicesListMessages";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDevicesListMessages_FormClosing);
            this.statusStripExport.ResumeLayout(false);
            this.statusStripExport.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewListMessages;
        private System.Windows.Forms.StatusStrip statusStripExport;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelExport;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelDevices;
    }
}