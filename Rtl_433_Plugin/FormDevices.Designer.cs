namespace SDRSharp.Rtl_433
{
    partial class FormDevices
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
            this.statusStripDevices = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelNbMessages = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelPeriodeCurrent = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelPeriodeMax = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSplitLabelRecordOneShoot = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelFreezeData = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanelDeviceData = new System.Windows.Forms.TableLayoutPanel();
            this.listViewListMessages = new System.Windows.Forms.ListView();
            this.plotterDisplayExDevices = new GraphLib.PlotterDisplayEx();
            this.statusStripDevices.SuspendLayout();
            this.tableLayoutPanelDeviceData.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStripDevices
            // 
            this.statusStripDevices.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelNbMessages,
            this.toolStripStatusLabelPeriodeCurrent,
            this.toolStripStatusLabelPeriodeMax,
            this.toolStripSplitLabelRecordOneShoot,
            this.toolStripStatusLabelFreezeData});
            this.statusStripDevices.Location = new System.Drawing.Point(0, 239);
            this.statusStripDevices.Name = "statusStripDevices";
            this.statusStripDevices.Size = new System.Drawing.Size(750, 22);
            this.statusStripDevices.TabIndex = 0;
            this.statusStripDevices.Text = "statusStrip1";
            // 
            // toolStripStatusLabelNbMessages
            // 
            this.toolStripStatusLabelNbMessages.Name = "toolStripStatusLabelNbMessages";
            this.toolStripStatusLabelNbMessages.Size = new System.Drawing.Size(13, 17);
            this.toolStripStatusLabelNbMessages.Text = "0";
            // 
            // toolStripStatusLabelPeriodeCurrent
            // 
            this.toolStripStatusLabelPeriodeCurrent.Name = "toolStripStatusLabelPeriodeCurrent";
            this.toolStripStatusLabelPeriodeCurrent.Size = new System.Drawing.Size(13, 17);
            this.toolStripStatusLabelPeriodeCurrent.Text = "0";
            // 
            // toolStripStatusLabelPeriodeMax
            // 
            this.toolStripStatusLabelPeriodeMax.Name = "toolStripStatusLabelPeriodeMax";
            this.toolStripStatusLabelPeriodeMax.Size = new System.Drawing.Size(13, 17);
            this.toolStripStatusLabelPeriodeMax.Text = "0";
            // 
            // toolStripSplitLabelRecordOneShoot
            // 
            this.toolStripSplitLabelRecordOneShoot.ActiveLinkColor = System.Drawing.Color.White;
            this.toolStripSplitLabelRecordOneShoot.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStripSplitLabelRecordOneShoot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSplitLabelRecordOneShoot.ImageTransparentColor = System.Drawing.Color.White;
            this.toolStripSplitLabelRecordOneShoot.LinkColor = System.Drawing.Color.White;
            this.toolStripSplitLabelRecordOneShoot.Name = "toolStripSplitLabelRecordOneShoot";
            this.toolStripSplitLabelRecordOneShoot.Size = new System.Drawing.Size(100, 17);
            this.toolStripSplitLabelRecordOneShoot.Text = "Record one shoot";
            this.toolStripSplitLabelRecordOneShoot.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.toolStripSplitLabelRecordOneShoot.VisitedLinkColor = System.Drawing.Color.White;
            this.toolStripSplitLabelRecordOneShoot.Click += new System.EventHandler(this.ToolStripSplitLabelRecordOneShoot_Click);
            // 
            // toolStripStatusLabelFreezeData
            // 
            this.toolStripStatusLabelFreezeData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabelFreezeData.Name = "toolStripStatusLabelFreezeData";
            this.toolStripStatusLabelFreezeData.Size = new System.Drawing.Size(79, 17);
            this.toolStripStatusLabelFreezeData.Text = "Freeze graphs";
            this.toolStripStatusLabelFreezeData.Click += new System.EventHandler(this.ToolStripStatusLabelFreezeData_Click);
            // 
            // tableLayoutPanelDeviceData
            // 
            this.tableLayoutPanelDeviceData.ColumnCount = 1;
            this.tableLayoutPanelDeviceData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelDeviceData.Controls.Add(this.listViewListMessages, 0, 1);
            this.tableLayoutPanelDeviceData.Controls.Add(this.plotterDisplayExDevices, 0, 0);
            this.tableLayoutPanelDeviceData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelDeviceData.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelDeviceData.Name = "tableLayoutPanelDeviceData";
            this.tableLayoutPanelDeviceData.RowCount = 2;
            this.tableLayoutPanelDeviceData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanelDeviceData.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelDeviceData.Size = new System.Drawing.Size(750, 239);
            this.tableLayoutPanelDeviceData.TabIndex = 1;
            // 
            // listViewListMessages
            // 
            this.listViewListMessages.Dock = System.Windows.Forms.DockStyle.Top;
            this.listViewListMessages.GridLines = true;
            this.listViewListMessages.HideSelection = false;
            this.listViewListMessages.Location = new System.Drawing.Point(3, 303);
            this.listViewListMessages.Name = "listViewListMessages";
            this.listViewListMessages.Size = new System.Drawing.Size(1034, 114);
            this.listViewListMessages.TabIndex = 3;
            this.listViewListMessages.UseCompatibleStateImageBehavior = false;
            this.listViewListMessages.View = System.Windows.Forms.View.List;
            this.listViewListMessages.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.listViewListMessages_RetrieveVirtualItem);
            // 
            // plotterDisplayExDevices
            // 
            this.plotterDisplayExDevices.BackColor = System.Drawing.Color.Transparent;
            this.plotterDisplayExDevices.BackgroundColorBot = System.Drawing.Color.White;
            this.plotterDisplayExDevices.BackgroundColorTop = System.Drawing.Color.White;
            this.plotterDisplayExDevices.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.plotterDisplayExDevices.DashedGridColor = System.Drawing.Color.DarkGray;
            this.plotterDisplayExDevices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotterDisplayExDevices.DoubleBuffering = true;
            this.plotterDisplayExDevices.Location = new System.Drawing.Point(3, 3);
            this.plotterDisplayExDevices.Name = "plotterDisplayExDevices";
            this.plotterDisplayExDevices.Size = new System.Drawing.Size(1034, 294);
            this.plotterDisplayExDevices.SolidGridColor = System.Drawing.Color.DarkGray;
            this.plotterDisplayExDevices.TabIndex = 2;
            // 
            // FormDevices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(750, 261);
            this.Controls.Add(this.tableLayoutPanelDeviceData);
            this.Controls.Add(this.statusStripDevices);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "FormDevices";
            this.Text = "FormDevices";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormDevices_FormClosed);
            this.ResizeEnd += new System.EventHandler(this.FormDevices_ResizeEnd);
            this.statusStripDevices.ResumeLayout(false);
            this.statusStripDevices.PerformLayout();
            this.tableLayoutPanelDeviceData.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.StatusStrip statusStripDevices;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelNbMessages;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelPeriodeCurrent;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelPeriodeMax;
        private System.Windows.Forms.ToolStripStatusLabel toolStripSplitLabelRecordOneShoot;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDeviceData;
        private GraphLib.PlotterDisplayEx plotterDisplayExDevices;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelFreezeData;
        private System.Windows.Forms.ListView listViewListMessages;
    }
}