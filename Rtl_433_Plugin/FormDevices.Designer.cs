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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDevices));
            this.statusStripDevices = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelNbMessages = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelPeriodeCurrent = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelPeriodeMax = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripSplitLabelRecordOneShoot = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelDisplayCurves = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelFreezeData = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanelDeviceData = new System.Windows.Forms.TableLayoutPanel();
            this.plotterDisplayExDevices = new GraphLib.PlotterDisplayEx();
            this.statusStripDevices.SuspendLayout();
            this.tableLayoutPanelDeviceData.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStripDevices
            // 
            this.statusStripDevices.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.statusStripDevices.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelNbMessages,
            this.toolStripStatusLabelPeriodeCurrent,
            this.toolStripStatusLabelPeriodeMax,
            this.toolStripSplitLabelRecordOneShoot,
            this.toolStripStatusLabelDisplayCurves,
            this.toolStripStatusLabelFreezeData});
            this.statusStripDevices.Location = new System.Drawing.Point(0, 141);
            this.statusStripDevices.Name = "statusStripDevices";
            this.statusStripDevices.Size = new System.Drawing.Size(1171, 22);
            this.statusStripDevices.TabIndex = 0;
            this.statusStripDevices.Text = "statusStrip1";
            // 
            // toolStripStatusLabelNbMessages
            // 
            this.toolStripStatusLabelNbMessages.BackColor = System.Drawing.SystemColors.Desktop;
            this.toolStripStatusLabelNbMessages.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.toolStripStatusLabelNbMessages.Name = "toolStripStatusLabelNbMessages";
            this.toolStripStatusLabelNbMessages.Size = new System.Drawing.Size(13, 17);
            this.toolStripStatusLabelNbMessages.Text = "0";
            // 
            // toolStripStatusLabelPeriodeCurrent
            // 
            this.toolStripStatusLabelPeriodeCurrent.BackColor = System.Drawing.SystemColors.Desktop;
            this.toolStripStatusLabelPeriodeCurrent.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.toolStripStatusLabelPeriodeCurrent.Name = "toolStripStatusLabelPeriodeCurrent";
            this.toolStripStatusLabelPeriodeCurrent.Size = new System.Drawing.Size(13, 17);
            this.toolStripStatusLabelPeriodeCurrent.Text = "0";
            // 
            // toolStripStatusLabelPeriodeMax
            // 
            this.toolStripStatusLabelPeriodeMax.BackColor = System.Drawing.SystemColors.Desktop;
            this.toolStripStatusLabelPeriodeMax.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.toolStripStatusLabelPeriodeMax.Name = "toolStripStatusLabelPeriodeMax";
            this.toolStripStatusLabelPeriodeMax.Size = new System.Drawing.Size(13, 17);
            this.toolStripStatusLabelPeriodeMax.Text = "0";
            // 
            // toolStripSplitLabelRecordOneShoot
            // 
            this.toolStripSplitLabelRecordOneShoot.ActiveLinkColor = System.Drawing.Color.White;
            this.toolStripSplitLabelRecordOneShoot.BackColor = System.Drawing.SystemColors.Desktop;
            this.toolStripSplitLabelRecordOneShoot.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStripSplitLabelRecordOneShoot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSplitLabelRecordOneShoot.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.toolStripSplitLabelRecordOneShoot.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitLabelRecordOneShoot.Image")));
            this.toolStripSplitLabelRecordOneShoot.ImageTransparentColor = System.Drawing.Color.White;
            this.toolStripSplitLabelRecordOneShoot.LinkColor = System.Drawing.Color.White;
            this.toolStripSplitLabelRecordOneShoot.Name = "toolStripSplitLabelRecordOneShoot";
            this.toolStripSplitLabelRecordOneShoot.Size = new System.Drawing.Size(100, 17);
            this.toolStripSplitLabelRecordOneShoot.Text = "Record one shoot";
            this.toolStripSplitLabelRecordOneShoot.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.toolStripSplitLabelRecordOneShoot.VisitedLinkColor = System.Drawing.Color.White;
            this.toolStripSplitLabelRecordOneShoot.Click += new System.EventHandler(this.toolStripSplitLabelRecordOneShoot_Click);
            // 
            // toolStripStatusLabelDisplayCurves
            // 
            this.toolStripStatusLabelDisplayCurves.BackColor = System.Drawing.SystemColors.Desktop;
            this.toolStripStatusLabelDisplayCurves.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabelDisplayCurves.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.toolStripStatusLabelDisplayCurves.Name = "toolStripStatusLabelDisplayCurves";
            this.toolStripStatusLabelDisplayCurves.Size = new System.Drawing.Size(100, 17);
            this.toolStripStatusLabelDisplayCurves.Text = "No display curves";
            this.toolStripStatusLabelDisplayCurves.Click += new System.EventHandler(this.toolStripStatusLabelDisplayCurves_Click);
            // 
            // toolStripStatusLabelFreezeData
            // 
            this.toolStripStatusLabelFreezeData.BackColor = System.Drawing.SystemColors.Desktop;
            this.toolStripStatusLabelFreezeData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabelFreezeData.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.toolStripStatusLabelFreezeData.Name = "toolStripStatusLabelFreezeData";
            this.toolStripStatusLabelFreezeData.Size = new System.Drawing.Size(105, 17);
            this.toolStripStatusLabelFreezeData.Text = "Freeze data graphs";
            this.toolStripStatusLabelFreezeData.Click += new System.EventHandler(this.toolStripStatusLabelFreezeData_Click);
            // 
            // tableLayoutPanelDeviceData
            // 
            this.tableLayoutPanelDeviceData.AutoSize = true;
            this.tableLayoutPanelDeviceData.BackColor = System.Drawing.SystemColors.Desktop;
            this.tableLayoutPanelDeviceData.ColumnCount = 5;
            this.tableLayoutPanelDeviceData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelDeviceData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelDeviceData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelDeviceData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelDeviceData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelDeviceData.Controls.Add(this.plotterDisplayExDevices, 0, 0);
            this.tableLayoutPanelDeviceData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelDeviceData.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelDeviceData.Name = "tableLayoutPanelDeviceData";
            this.tableLayoutPanelDeviceData.RowCount = 1;
            this.tableLayoutPanelDeviceData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 249F));
            this.tableLayoutPanelDeviceData.Size = new System.Drawing.Size(1171, 141);
            this.tableLayoutPanelDeviceData.TabIndex = 1;
            // 
            // plotterDisplayExDevices
            // 
            this.plotterDisplayExDevices.BackColor = System.Drawing.SystemColors.Desktop;
            this.plotterDisplayExDevices.BackgroundColorBot = System.Drawing.Color.White;
            this.plotterDisplayExDevices.BackgroundColorTop = System.Drawing.Color.White;
            this.plotterDisplayExDevices.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tableLayoutPanelDeviceData.SetColumnSpan(this.plotterDisplayExDevices, 5);
            this.plotterDisplayExDevices.DashedGridColor = System.Drawing.Color.DarkGray;
            this.plotterDisplayExDevices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plotterDisplayExDevices.DoubleBuffering = true;
            this.plotterDisplayExDevices.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.plotterDisplayExDevices.Location = new System.Drawing.Point(3, 3);
            this.plotterDisplayExDevices.Name = "plotterDisplayExDevices";
            this.plotterDisplayExDevices.Size = new System.Drawing.Size(1165, 243);
            this.plotterDisplayExDevices.SolidGridColor = System.Drawing.Color.DarkGray;
            this.plotterDisplayExDevices.TabIndex = 2;
            // 
            // FormDevices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.ClientSize = new System.Drawing.Size(1171, 163);
            this.Controls.Add(this.tableLayoutPanelDeviceData);
            this.Controls.Add(this.statusStripDevices);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Name = "FormDevices";
            this.Text = "FormDevices";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormDevices_FormClosed);
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
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelDisplayCurves;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelFreezeData;
    }
}