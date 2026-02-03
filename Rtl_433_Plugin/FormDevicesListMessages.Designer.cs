using System;

namespace SDRSharp.Rtl_433
{
    public partial class FormDevicesListMessages
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        //private System.ComponentModel.IContainer components = null;
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStripExport = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelExport = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripExport.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStripExport
            // 
            this.statusStripExport.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelExport});
            this.statusStripExport.Location = new System.Drawing.Point(0, 124);
            this.statusStripExport.Name = "statusStripExport";
            this.statusStripExport.Size = new System.Drawing.Size(1034, 22);
            this.statusStripExport.TabIndex = 1;
            // 
            // toolStripStatusLabelExport
            // 
            this.toolStripStatusLabelExport.Name = "toolStripStatusLabelExport";
            this.toolStripStatusLabelExport.Size = new System.Drawing.Size(92, 17);
            this.toolStripStatusLabelExport.Text = "Export data(.txt)";
            this.toolStripStatusLabelExport.Click += new System.EventHandler(this.ToolStripStatusLabelExport_Click);
            // 
            // FormDevicesListMessages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 146);
            this.Controls.Add(this.statusStripExport);
            this.Name = "FormDevicesListMessages";
            this.Text = "FormDevicesListMessages";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormDevicesListMessages_FormClosed);
            this.statusStripExport.ResumeLayout(false);
            this.statusStripExport.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStripExport;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelExport;
    }
}