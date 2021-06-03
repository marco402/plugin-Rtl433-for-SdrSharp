namespace SDRSharp.Rtl_433
{
    partial class Rtl_433_Panel
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
            this.mainTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxDataConv = new System.Windows.Forms.GroupBox();
            this.radioButtonDataConvCustomary = new System.Windows.Forms.RadioButton();
            this.radioButtonDataConvSI = new System.Windows.Forms.RadioButton();
            this.radioButtonDataConvNative = new System.Windows.Forms.RadioButton();
            this.labelTimeCycle = new System.Windows.Forms.Label();
            this.labelTimeRtl433 = new System.Windows.Forms.Label();
            this.groupBoxMetadata = new System.Windows.Forms.GroupBox();
            this.radioButtonMLevel = new System.Windows.Forms.RadioButton();
            this.radioButtonNoM = new System.Windows.Forms.RadioButton();
            this.richTextBoxMessages = new System.Windows.Forms.RichTextBox();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelSampleRate = new System.Windows.Forms.Label();
            this.labelFrequency = new System.Windows.Forms.Label();
            this.labelCenterFrequency = new System.Windows.Forms.Label();
            this.groupBoxRecord = new System.Windows.Forms.GroupBox();
            this.checkBoxMONO = new System.Windows.Forms.CheckBox();
            this.checkBoxSTEREO = new System.Windows.Forms.CheckBox();
            this.groupBoxFrequency = new System.Windows.Forms.GroupBox();
            this.radioButtonFreq915 = new System.Windows.Forms.RadioButton();
            this.radioButtonFreq868 = new System.Windows.Forms.RadioButton();
            this.radioButtonFreq43392 = new System.Windows.Forms.RadioButton();
            this.radioButtonFreq345 = new System.Windows.Forms.RadioButton();
            this.radioButtonFreq315 = new System.Windows.Forms.RadioButton();
            this.radioButtonFreqFree = new System.Windows.Forms.RadioButton();
            this.groupBoxVerbose = new System.Windows.Forms.GroupBox();
            this.radioButtonNoV = new System.Windows.Forms.RadioButton();
            this.radioButtonVVV = new System.Windows.Forms.RadioButton();
            this.radioButtonVV = new System.Windows.Forms.RadioButton();
            this.radioButtonV = new System.Windows.Forms.RadioButton();
            this.radioButtonVvvv = new System.Windows.Forms.RadioButton();
            this.buttonClearMessages = new System.Windows.Forms.Button();
            this.buttonDisplayParam = new System.Windows.Forms.Button();
            this.buttonCu8ToWav = new System.Windows.Forms.Button();
            this.groupBoxSave = new System.Windows.Forms.GroupBox();
            this.radioButtonSnone = new System.Windows.Forms.RadioButton();
            this.radioButtonSunknown = new System.Windows.Forms.RadioButton();
            this.radioButtonSknown = new System.Windows.Forms.RadioButton();
            this.radioButtonSall = new System.Windows.Forms.RadioButton();
            this.groupBoxR = new System.Windows.Forms.GroupBox();
            this.listBoxHideDevices = new System.Windows.Forms.ListBox();
            this.buttonStartStop = new System.Windows.Forms.Button();
            this.labelHideDevices = new System.Windows.Forms.Label();
            this.mainTableLayoutPanel.SuspendLayout();
            this.groupBoxDataConv.SuspendLayout();
            this.groupBoxMetadata.SuspendLayout();
            this.groupBoxRecord.SuspendLayout();
            this.groupBoxFrequency.SuspendLayout();
            this.groupBoxVerbose.SuspendLayout();
            this.groupBoxSave.SuspendLayout();
            this.groupBoxR.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTableLayoutPanel
            // 
            this.mainTableLayoutPanel.AutoScroll = true;
            this.mainTableLayoutPanel.AutoSize = true;
            this.mainTableLayoutPanel.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.mainTableLayoutPanel.ColumnCount = 2;
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxDataConv, 0, 8);
            this.mainTableLayoutPanel.Controls.Add(this.labelTimeCycle, 0, 13);
            this.mainTableLayoutPanel.Controls.Add(this.labelTimeRtl433, 0, 13);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxMetadata, 1, 7);
            this.mainTableLayoutPanel.Controls.Add(this.richTextBoxMessages, 0, 12);
            this.mainTableLayoutPanel.Controls.Add(this.labelVersion, 0, 3);
            this.mainTableLayoutPanel.Controls.Add(this.labelSampleRate, 0, 4);
            this.mainTableLayoutPanel.Controls.Add(this.labelFrequency, 0, 5);
            this.mainTableLayoutPanel.Controls.Add(this.labelCenterFrequency, 0, 6);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxRecord, 1, 2);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxFrequency, 0, 1);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxVerbose, 0, 7);
            this.mainTableLayoutPanel.Controls.Add(this.buttonClearMessages, 1, 11);
            this.mainTableLayoutPanel.Controls.Add(this.buttonDisplayParam, 0, 11);
            this.mainTableLayoutPanel.Controls.Add(this.buttonCu8ToWav, 0, 2);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxSave, 1, 8);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxR, 0, 10);
            this.mainTableLayoutPanel.Controls.Add(this.buttonStartStop, 1, 0);
            this.mainTableLayoutPanel.Controls.Add(this.labelHideDevices, 0, 9);
            this.mainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            this.mainTableLayoutPanel.RowCount = 15;
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 11F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainTableLayoutPanel.Size = new System.Drawing.Size(251, 1093);
            this.mainTableLayoutPanel.TabIndex = 1;
            // 
            // groupBoxDataConv
            // 
            this.groupBoxDataConv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxDataConv.BackColor = System.Drawing.SystemColors.ControlDark;
            this.groupBoxDataConv.Controls.Add(this.radioButtonDataConvCustomary);
            this.groupBoxDataConv.Controls.Add(this.radioButtonDataConvSI);
            this.groupBoxDataConv.Controls.Add(this.radioButtonDataConvNative);
            this.groupBoxDataConv.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBoxDataConv.Location = new System.Drawing.Point(3, 453);
            this.groupBoxDataConv.Name = "groupBoxDataConv";
            this.groupBoxDataConv.Size = new System.Drawing.Size(102, 114);
            this.groupBoxDataConv.TabIndex = 20;
            this.groupBoxDataConv.TabStop = false;
            this.groupBoxDataConv.Text = "Data Conv(-C)";
            // 
            // radioButtonDataConvCustomary
            // 
            this.radioButtonDataConvCustomary.AutoSize = true;
            this.radioButtonDataConvCustomary.Location = new System.Drawing.Point(9, 65);
            this.radioButtonDataConvCustomary.Name = "radioButtonDataConvCustomary";
            this.radioButtonDataConvCustomary.Size = new System.Drawing.Size(83, 17);
            this.radioButtonDataConvCustomary.TabIndex = 11;
            this.radioButtonDataConvCustomary.Tag = "dataconv";
            this.radioButtonDataConvCustomary.Text = "-Ccustomary";
            this.radioButtonDataConvCustomary.UseVisualStyleBackColor = true;
            this.radioButtonDataConvCustomary.CheckedChanged += new System.EventHandler(this.radioButtonOptionsRtl433_CheckedChanged);
            // 
            // radioButtonDataConvSI
            // 
            this.radioButtonDataConvSI.AutoSize = true;
            this.radioButtonDataConvSI.Location = new System.Drawing.Point(9, 42);
            this.radioButtonDataConvSI.Name = "radioButtonDataConvSI";
            this.radioButtonDataConvSI.Size = new System.Drawing.Size(42, 17);
            this.radioButtonDataConvSI.TabIndex = 10;
            this.radioButtonDataConvSI.Tag = "dataconv";
            this.radioButtonDataConvSI.Text = "-Csi";
            this.radioButtonDataConvSI.UseVisualStyleBackColor = true;
            this.radioButtonDataConvSI.CheckedChanged += new System.EventHandler(this.radioButtonOptionsRtl433_CheckedChanged);
            // 
            // radioButtonDataConvNative
            // 
            this.radioButtonDataConvNative.AutoSize = true;
            this.radioButtonDataConvNative.Checked = true;
            this.radioButtonDataConvNative.Location = new System.Drawing.Point(9, 19);
            this.radioButtonDataConvNative.Name = "radioButtonDataConvNative";
            this.radioButtonDataConvNative.Size = new System.Drawing.Size(64, 17);
            this.radioButtonDataConvNative.TabIndex = 9;
            this.radioButtonDataConvNative.TabStop = true;
            this.radioButtonDataConvNative.Tag = "dataconv";
            this.radioButtonDataConvNative.Text = "-Cnative";
            this.radioButtonDataConvNative.UseVisualStyleBackColor = true;
            this.radioButtonDataConvNative.CheckedChanged += new System.EventHandler(this.radioButtonOptionsRtl433_CheckedChanged);
            // 
            // labelTimeCycle
            // 
            this.labelTimeCycle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelTimeCycle.AutoSize = true;
            this.mainTableLayoutPanel.SetColumnSpan(this.labelTimeCycle, 2);
            this.labelTimeCycle.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labelTimeCycle.Location = new System.Drawing.Point(3, 1082);
            this.labelTimeCycle.Name = "labelTimeCycle";
            this.labelTimeCycle.Size = new System.Drawing.Size(55, 11);
            this.labelTimeCycle.TabIndex = 15;
            this.labelTimeCycle.Text = "Cycle time";
            this.labelTimeCycle.Visible = false;
            // 
            // labelTimeRtl433
            // 
            this.labelTimeRtl433.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelTimeRtl433.AutoSize = true;
            this.mainTableLayoutPanel.SetColumnSpan(this.labelTimeRtl433, 2);
            this.labelTimeRtl433.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labelTimeRtl433.Location = new System.Drawing.Point(3, 1061);
            this.labelTimeRtl433.Name = "labelTimeRtl433";
            this.labelTimeRtl433.Size = new System.Drawing.Size(89, 13);
            this.labelTimeRtl433.TabIndex = 16;
            this.labelTimeRtl433.Text = "Cycle time Rtl433";
            this.labelTimeRtl433.Visible = false;
            // 
            // groupBoxMetadata
            // 
            this.groupBoxMetadata.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxMetadata.Controls.Add(this.radioButtonMLevel);
            this.groupBoxMetadata.Controls.Add(this.radioButtonNoM);
            this.groupBoxMetadata.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBoxMetadata.Location = new System.Drawing.Point(113, 303);
            this.groupBoxMetadata.Name = "groupBoxMetadata";
            this.groupBoxMetadata.Size = new System.Drawing.Size(99, 144);
            this.groupBoxMetadata.TabIndex = 10;
            this.groupBoxMetadata.TabStop = false;
            this.groupBoxMetadata.Text = "metaData(-M)";
            // 
            // radioButtonMLevel
            // 
            this.radioButtonMLevel.AutoSize = true;
            this.radioButtonMLevel.Location = new System.Drawing.Point(6, 42);
            this.radioButtonMLevel.Name = "radioButtonMLevel";
            this.radioButtonMLevel.Size = new System.Drawing.Size(59, 17);
            this.radioButtonMLevel.TabIndex = 8;
            this.radioButtonMLevel.Tag = "MbitsOrLevel";
            this.radioButtonMLevel.Text = "-Mlevel";
            this.radioButtonMLevel.UseVisualStyleBackColor = true;
            this.radioButtonMLevel.CheckedChanged += new System.EventHandler(this.radioButtonOptionsRtl433_CheckedChanged);
            // 
            // radioButtonNoM
            // 
            this.radioButtonNoM.AutoSize = true;
            this.radioButtonNoM.Checked = true;
            this.radioButtonNoM.Location = new System.Drawing.Point(6, 19);
            this.radioButtonNoM.Name = "radioButtonNoM";
            this.radioButtonNoM.Size = new System.Drawing.Size(54, 17);
            this.radioButtonNoM.TabIndex = 8;
            this.radioButtonNoM.TabStop = true;
            this.radioButtonNoM.Tag = "MbitsOrLevel";
            this.radioButtonNoM.Text = "No -M";
            this.radioButtonNoM.UseVisualStyleBackColor = true;
            this.radioButtonNoM.CheckedChanged += new System.EventHandler(this.radioButtonOptionsRtl433_CheckedChanged);
            // 
            // richTextBoxMessages
            // 
            this.richTextBoxMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTableLayoutPanel.SetColumnSpan(this.richTextBoxMessages, 2);
            this.richTextBoxMessages.Location = new System.Drawing.Point(3, 762);
            this.richTextBoxMessages.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.richTextBoxMessages.MaxLength = 3000;
            this.richTextBoxMessages.Name = "richTextBoxMessages";
            this.richTextBoxMessages.ReadOnly = true;
            this.richTextBoxMessages.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.richTextBoxMessages.Size = new System.Drawing.Size(245, 286);
            this.richTextBoxMessages.TabIndex = 5;
            this.richTextBoxMessages.Text = "";
            this.richTextBoxMessages.TextChanged += new System.EventHandler(this.richTextBoxMessages_TextChanged);
            // 
            // labelVersion
            // 
            this.labelVersion.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelVersion.AutoSize = true;
            this.mainTableLayoutPanel.SetColumnSpan(this.labelVersion, 2);
            this.labelVersion.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labelVersion.Location = new System.Drawing.Point(3, 223);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(55, 13);
            this.labelVersion.TabIndex = 4;
            this.labelVersion.Text = "Version dll";
            // 
            // labelSampleRate
            // 
            this.labelSampleRate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelSampleRate.AutoSize = true;
            this.mainTableLayoutPanel.SetColumnSpan(this.labelSampleRate, 2);
            this.labelSampleRate.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labelSampleRate.Location = new System.Drawing.Point(3, 243);
            this.labelSampleRate.Name = "labelSampleRate";
            this.labelSampleRate.Size = new System.Drawing.Size(68, 13);
            this.labelSampleRate.TabIndex = 6;
            this.labelSampleRate.Text = "Sample Rate";
            // 
            // labelFrequency
            // 
            this.labelFrequency.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelFrequency.AutoSize = true;
            this.mainTableLayoutPanel.SetColumnSpan(this.labelFrequency, 2);
            this.labelFrequency.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labelFrequency.Location = new System.Drawing.Point(3, 263);
            this.labelFrequency.Name = "labelFrequency";
            this.labelFrequency.Size = new System.Drawing.Size(57, 13);
            this.labelFrequency.TabIndex = 6;
            this.labelFrequency.Text = "Frequency";
            // 
            // labelCenterFrequency
            // 
            this.labelCenterFrequency.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelCenterFrequency.AutoSize = true;
            this.mainTableLayoutPanel.SetColumnSpan(this.labelCenterFrequency, 2);
            this.labelCenterFrequency.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.labelCenterFrequency.Location = new System.Drawing.Point(3, 283);
            this.labelCenterFrequency.Name = "labelCenterFrequency";
            this.labelCenterFrequency.Size = new System.Drawing.Size(91, 13);
            this.labelCenterFrequency.TabIndex = 6;
            this.labelCenterFrequency.Text = "Center Frequency";
            // 
            // groupBoxRecord
            // 
            this.groupBoxRecord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxRecord.BackColor = System.Drawing.SystemColors.ControlDark;
            this.groupBoxRecord.Controls.Add(this.checkBoxMONO);
            this.groupBoxRecord.Controls.Add(this.checkBoxSTEREO);
            this.groupBoxRecord.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBoxRecord.Location = new System.Drawing.Point(113, 133);
            this.groupBoxRecord.Name = "groupBoxRecord";
            this.groupBoxRecord.Size = new System.Drawing.Size(99, 84);
            this.groupBoxRecord.TabIndex = 12;
            this.groupBoxRecord.TabStop = false;
            this.groupBoxRecord.Text = "Record";
            // 
            // checkBoxMONO
            // 
            this.checkBoxMONO.AutoSize = true;
            this.checkBoxMONO.Location = new System.Drawing.Point(6, 51);
            this.checkBoxMONO.Name = "checkBoxMONO";
            this.checkBoxMONO.Size = new System.Drawing.Size(59, 17);
            this.checkBoxMONO.TabIndex = 0;
            this.checkBoxMONO.Text = "MONO";
            this.checkBoxMONO.UseVisualStyleBackColor = true;
            this.checkBoxMONO.CheckedChanged += new System.EventHandler(this.checkBoxMONO_CheckedChanged);
            // 
            // checkBoxSTEREO
            // 
            this.checkBoxSTEREO.AutoSize = true;
            this.checkBoxSTEREO.Location = new System.Drawing.Point(6, 20);
            this.checkBoxSTEREO.Name = "checkBoxSTEREO";
            this.checkBoxSTEREO.Size = new System.Drawing.Size(70, 17);
            this.checkBoxSTEREO.TabIndex = 0;
            this.checkBoxSTEREO.Text = "STEREO";
            this.checkBoxSTEREO.UseVisualStyleBackColor = true;
            this.checkBoxSTEREO.CheckedChanged += new System.EventHandler(this.checkBoxSTEREO_CheckedChanged);
            // 
            // groupBoxFrequency
            // 
            this.groupBoxFrequency.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.mainTableLayoutPanel.SetColumnSpan(this.groupBoxFrequency, 2);
            this.groupBoxFrequency.Controls.Add(this.radioButtonFreq915);
            this.groupBoxFrequency.Controls.Add(this.radioButtonFreq868);
            this.groupBoxFrequency.Controls.Add(this.radioButtonFreq43392);
            this.groupBoxFrequency.Controls.Add(this.radioButtonFreq345);
            this.groupBoxFrequency.Controls.Add(this.radioButtonFreq315);
            this.groupBoxFrequency.Controls.Add(this.radioButtonFreqFree);
            this.groupBoxFrequency.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBoxFrequency.Location = new System.Drawing.Point(3, 33);
            this.groupBoxFrequency.Name = "groupBoxFrequency";
            this.groupBoxFrequency.Size = new System.Drawing.Size(209, 94);
            this.groupBoxFrequency.TabIndex = 13;
            this.groupBoxFrequency.TabStop = false;
            this.groupBoxFrequency.Text = "Frequency";
            // 
            // radioButtonFreq915
            // 
            this.radioButtonFreq915.AutoSize = true;
            this.radioButtonFreq915.Location = new System.Drawing.Point(101, 65);
            this.radioButtonFreq915.Name = "radioButtonFreq915";
            this.radioButtonFreq915.Size = new System.Drawing.Size(66, 17);
            this.radioButtonFreq915.TabIndex = 5;
            this.radioButtonFreq915.Tag = "915000000";
            this.radioButtonFreq915.Text = "915 Mhz";
            this.radioButtonFreq915.UseVisualStyleBackColor = true;
            this.radioButtonFreq915.CheckedChanged += new System.EventHandler(this.radioButtonFreq_CheckedChanged);
            // 
            // radioButtonFreq868
            // 
            this.radioButtonFreq868.AutoSize = true;
            this.radioButtonFreq868.Location = new System.Drawing.Point(101, 42);
            this.radioButtonFreq868.Name = "radioButtonFreq868";
            this.radioButtonFreq868.Size = new System.Drawing.Size(66, 17);
            this.radioButtonFreq868.TabIndex = 4;
            this.radioButtonFreq868.Tag = "868000000";
            this.radioButtonFreq868.Text = "868 Mhz";
            this.radioButtonFreq868.UseVisualStyleBackColor = true;
            this.radioButtonFreq868.CheckedChanged += new System.EventHandler(this.radioButtonFreq_CheckedChanged);
            // 
            // radioButtonFreq43392
            // 
            this.radioButtonFreq43392.AutoSize = true;
            this.radioButtonFreq43392.Location = new System.Drawing.Point(101, 19);
            this.radioButtonFreq43392.Name = "radioButtonFreq43392";
            this.radioButtonFreq43392.Size = new System.Drawing.Size(81, 17);
            this.radioButtonFreq43392.TabIndex = 3;
            this.radioButtonFreq43392.Tag = "433920000";
            this.radioButtonFreq43392.Text = "433.92 Mhz";
            this.radioButtonFreq43392.UseVisualStyleBackColor = true;
            this.radioButtonFreq43392.CheckedChanged += new System.EventHandler(this.radioButtonFreq_CheckedChanged);
            // 
            // radioButtonFreq345
            // 
            this.radioButtonFreq345.AutoSize = true;
            this.radioButtonFreq345.Location = new System.Drawing.Point(9, 65);
            this.radioButtonFreq345.Name = "radioButtonFreq345";
            this.radioButtonFreq345.Size = new System.Drawing.Size(66, 17);
            this.radioButtonFreq345.TabIndex = 2;
            this.radioButtonFreq345.Tag = "345000000";
            this.radioButtonFreq345.Text = "345 Mhz";
            this.radioButtonFreq345.UseVisualStyleBackColor = true;
            this.radioButtonFreq345.CheckedChanged += new System.EventHandler(this.radioButtonFreq_CheckedChanged);
            // 
            // radioButtonFreq315
            // 
            this.radioButtonFreq315.AutoSize = true;
            this.radioButtonFreq315.Location = new System.Drawing.Point(9, 42);
            this.radioButtonFreq315.Name = "radioButtonFreq315";
            this.radioButtonFreq315.Size = new System.Drawing.Size(66, 17);
            this.radioButtonFreq315.TabIndex = 1;
            this.radioButtonFreq315.Tag = "315000000";
            this.radioButtonFreq315.Text = "315 Mhz";
            this.radioButtonFreq315.UseVisualStyleBackColor = true;
            this.radioButtonFreq315.CheckedChanged += new System.EventHandler(this.radioButtonFreq_CheckedChanged);
            // 
            // radioButtonFreqFree
            // 
            this.radioButtonFreqFree.AutoSize = true;
            this.radioButtonFreqFree.Location = new System.Drawing.Point(9, 19);
            this.radioButtonFreqFree.Name = "radioButtonFreqFree";
            this.radioButtonFreqFree.Size = new System.Drawing.Size(46, 17);
            this.radioButtonFreqFree.TabIndex = 0;
            this.radioButtonFreqFree.Tag = "0";
            this.radioButtonFreqFree.Text = "Free";
            this.radioButtonFreqFree.UseVisualStyleBackColor = true;
            this.radioButtonFreqFree.CheckedChanged += new System.EventHandler(this.radioButtonFreq_CheckedChanged);
            // 
            // groupBoxVerbose
            // 
            this.groupBoxVerbose.Controls.Add(this.radioButtonNoV);
            this.groupBoxVerbose.Controls.Add(this.radioButtonVVV);
            this.groupBoxVerbose.Controls.Add(this.radioButtonVV);
            this.groupBoxVerbose.Controls.Add(this.radioButtonV);
            this.groupBoxVerbose.Controls.Add(this.radioButtonVvvv);
            this.groupBoxVerbose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxVerbose.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBoxVerbose.Location = new System.Drawing.Point(3, 303);
            this.groupBoxVerbose.Name = "groupBoxVerbose";
            this.groupBoxVerbose.Size = new System.Drawing.Size(104, 144);
            this.groupBoxVerbose.TabIndex = 9;
            this.groupBoxVerbose.TabStop = false;
            this.groupBoxVerbose.Text = "verbose(-v)";
            // 
            // radioButtonNoV
            // 
            this.radioButtonNoV.AutoSize = true;
            this.radioButtonNoV.Checked = true;
            this.radioButtonNoV.Location = new System.Drawing.Point(9, 19);
            this.radioButtonNoV.Name = "radioButtonNoV";
            this.radioButtonNoV.Size = new System.Drawing.Size(51, 17);
            this.radioButtonNoV.TabIndex = 8;
            this.radioButtonNoV.TabStop = true;
            this.radioButtonNoV.Tag = "verbose";
            this.radioButtonNoV.Text = "No -v";
            this.radioButtonNoV.UseVisualStyleBackColor = true;
            this.radioButtonNoV.CheckedChanged += new System.EventHandler(this.radioButtonOptionsRtl433_CheckedChanged);
            // 
            // radioButtonVVV
            // 
            this.radioButtonVVV.AutoSize = true;
            this.radioButtonVVV.Location = new System.Drawing.Point(9, 89);
            this.radioButtonVVV.Name = "radioButtonVVV";
            this.radioButtonVVV.Size = new System.Drawing.Size(46, 17);
            this.radioButtonVVV.TabIndex = 8;
            this.radioButtonVVV.Tag = "verbose";
            this.radioButtonVVV.Text = "-vvv";
            this.radioButtonVVV.UseVisualStyleBackColor = true;
            this.radioButtonVVV.CheckedChanged += new System.EventHandler(this.radioButtonOptionsRtl433_CheckedChanged);
            // 
            // radioButtonVV
            // 
            this.radioButtonVV.AutoSize = true;
            this.radioButtonVV.Location = new System.Drawing.Point(9, 68);
            this.radioButtonVV.Name = "radioButtonVV";
            this.radioButtonVV.Size = new System.Drawing.Size(40, 17);
            this.radioButtonVV.TabIndex = 8;
            this.radioButtonVV.Tag = "verbose";
            this.radioButtonVV.Text = "-vv";
            this.radioButtonVV.UseVisualStyleBackColor = true;
            this.radioButtonVV.CheckedChanged += new System.EventHandler(this.radioButtonOptionsRtl433_CheckedChanged);
            // 
            // radioButtonV
            // 
            this.radioButtonV.AutoSize = true;
            this.radioButtonV.Location = new System.Drawing.Point(9, 42);
            this.radioButtonV.Name = "radioButtonV";
            this.radioButtonV.Size = new System.Drawing.Size(34, 17);
            this.radioButtonV.TabIndex = 8;
            this.radioButtonV.Tag = "verbose";
            this.radioButtonV.Text = "-v";
            this.radioButtonV.UseVisualStyleBackColor = true;
            this.radioButtonV.CheckedChanged += new System.EventHandler(this.radioButtonOptionsRtl433_CheckedChanged);
            // 
            // radioButtonVvvv
            // 
            this.radioButtonVvvv.AutoSize = true;
            this.radioButtonVvvv.Location = new System.Drawing.Point(9, 112);
            this.radioButtonVvvv.Name = "radioButtonVvvv";
            this.radioButtonVvvv.Size = new System.Drawing.Size(52, 17);
            this.radioButtonVvvv.TabIndex = 8;
            this.radioButtonVvvv.Tag = "verbose";
            this.radioButtonVvvv.Text = "-vvvv";
            this.radioButtonVvvv.UseVisualStyleBackColor = true;
            this.radioButtonVvvv.CheckedChanged += new System.EventHandler(this.radioButtonOptionsRtl433_CheckedChanged);
            // 
            // buttonClearMessages
            // 
            this.buttonClearMessages.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonClearMessages.AutoSize = true;
            this.buttonClearMessages.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonClearMessages.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonClearMessages.Location = new System.Drawing.Point(113, 733);
            this.buttonClearMessages.Name = "buttonClearMessages";
            this.buttonClearMessages.Size = new System.Drawing.Size(92, 23);
            this.buttonClearMessages.TabIndex = 7;
            this.buttonClearMessages.Text = "Clear Messages";
            this.buttonClearMessages.UseVisualStyleBackColor = false;
            this.buttonClearMessages.Click += new System.EventHandler(this.buttonClearMessages_Click);
            // 
            // buttonDisplayParam
            // 
            this.buttonDisplayParam.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonDisplayParam.AutoSize = true;
            this.buttonDisplayParam.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonDisplayParam.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonDisplayParam.Location = new System.Drawing.Point(3, 733);
            this.buttonDisplayParam.Name = "buttonDisplayParam";
            this.buttonDisplayParam.Size = new System.Drawing.Size(87, 23);
            this.buttonDisplayParam.TabIndex = 7;
            this.buttonDisplayParam.Text = "Display Param";
            this.buttonDisplayParam.UseVisualStyleBackColor = false;
            this.buttonDisplayParam.Click += new System.EventHandler(this.buttonDisplayParam_Click);
            // 
            // buttonCu8ToWav
            // 
            this.buttonCu8ToWav.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonCu8ToWav.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonCu8ToWav.Location = new System.Drawing.Point(3, 133);
            this.buttonCu8ToWav.Name = "buttonCu8ToWav";
            this.buttonCu8ToWav.Size = new System.Drawing.Size(104, 28);
            this.buttonCu8ToWav.TabIndex = 14;
            this.buttonCu8ToWav.Text = ".cu8 to .wav";
            this.buttonCu8ToWav.UseVisualStyleBackColor = false;
            this.buttonCu8ToWav.Click += new System.EventHandler(this.buttonCu8ToWav_Click);
            // 
            // groupBoxSave
            // 
            this.groupBoxSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxSave.Controls.Add(this.radioButtonSnone);
            this.groupBoxSave.Controls.Add(this.radioButtonSunknown);
            this.groupBoxSave.Controls.Add(this.radioButtonSknown);
            this.groupBoxSave.Controls.Add(this.radioButtonSall);
            this.groupBoxSave.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBoxSave.Location = new System.Drawing.Point(113, 453);
            this.groupBoxSave.Name = "groupBoxSave";
            this.groupBoxSave.Size = new System.Drawing.Size(99, 114);
            this.groupBoxSave.TabIndex = 9;
            this.groupBoxSave.TabStop = false;
            this.groupBoxSave.Text = "save(-S)";
            // 
            // radioButtonSnone
            // 
            this.radioButtonSnone.AutoSize = true;
            this.radioButtonSnone.Checked = true;
            this.radioButtonSnone.Location = new System.Drawing.Point(9, 19);
            this.radioButtonSnone.Name = "radioButtonSnone";
            this.radioButtonSnone.Size = new System.Drawing.Size(52, 17);
            this.radioButtonSnone.TabIndex = 8;
            this.radioButtonSnone.TabStop = true;
            this.radioButtonSnone.Tag = "saveDevice";
            this.radioButtonSnone.Text = "No -S";
            this.radioButtonSnone.UseVisualStyleBackColor = true;
            this.radioButtonSnone.CheckedChanged += new System.EventHandler(this.radioButtonOptionsRtl433_CheckedChanged);
            // 
            // radioButtonSunknown
            // 
            this.radioButtonSunknown.AutoSize = true;
            this.radioButtonSunknown.Location = new System.Drawing.Point(9, 89);
            this.radioButtonSunknown.Name = "radioButtonSunknown";
            this.radioButtonSunknown.Size = new System.Drawing.Size(79, 17);
            this.radioButtonSunknown.TabIndex = 8;
            this.radioButtonSunknown.Tag = "saveDevice";
            this.radioButtonSunknown.Text = "-Sunknown";
            this.radioButtonSunknown.UseVisualStyleBackColor = true;
            this.radioButtonSunknown.CheckedChanged += new System.EventHandler(this.radioButtonOptionsRtl433_CheckedChanged);
            // 
            // radioButtonSknown
            // 
            this.radioButtonSknown.AutoSize = true;
            this.radioButtonSknown.Location = new System.Drawing.Point(9, 68);
            this.radioButtonSknown.Name = "radioButtonSknown";
            this.radioButtonSknown.Size = new System.Drawing.Size(67, 17);
            this.radioButtonSknown.TabIndex = 8;
            this.radioButtonSknown.Tag = "saveDevice";
            this.radioButtonSknown.Text = "-Sknown";
            this.radioButtonSknown.UseVisualStyleBackColor = true;
            this.radioButtonSknown.CheckedChanged += new System.EventHandler(this.radioButtonOptionsRtl433_CheckedChanged);
            // 
            // radioButtonSall
            // 
            this.radioButtonSall.AutoSize = true;
            this.radioButtonSall.Location = new System.Drawing.Point(9, 42);
            this.radioButtonSall.Name = "radioButtonSall";
            this.radioButtonSall.Size = new System.Drawing.Size(45, 17);
            this.radioButtonSall.TabIndex = 8;
            this.radioButtonSall.Tag = "saveDevice";
            this.radioButtonSall.Text = "-Sall";
            this.radioButtonSall.UseVisualStyleBackColor = true;
            this.radioButtonSall.CheckedChanged += new System.EventHandler(this.radioButtonOptionsRtl433_CheckedChanged);
            // 
            // groupBoxR
            // 
            this.groupBoxR.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTableLayoutPanel.SetColumnSpan(this.groupBoxR, 2);
            this.groupBoxR.Controls.Add(this.listBoxHideDevices);
            this.groupBoxR.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBoxR.Location = new System.Drawing.Point(3, 603);
            this.groupBoxR.Name = "groupBoxR";
            this.groupBoxR.Size = new System.Drawing.Size(245, 124);
            this.groupBoxR.TabIndex = 9;
            this.groupBoxR.TabStop = false;
            this.groupBoxR.Text = "hide devices(-R)";
            // 
            // listBoxHideDevices
            // 
            this.listBoxHideDevices.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.listBoxHideDevices.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxHideDevices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxHideDevices.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.listBoxHideDevices.FormattingEnabled = true;
            this.listBoxHideDevices.HorizontalScrollbar = true;
            this.listBoxHideDevices.Location = new System.Drawing.Point(3, 16);
            this.listBoxHideDevices.Name = "listBoxHideDevices";
            this.listBoxHideDevices.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBoxHideDevices.Size = new System.Drawing.Size(239, 105);
            this.listBoxHideDevices.TabIndex = 3;
            // 
            // buttonStartStop
            // 
            this.buttonStartStop.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.buttonStartStop.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.buttonStartStop.Location = new System.Drawing.Point(113, 3);
            this.buttonStartStop.Name = "buttonStartStop";
            this.buttonStartStop.Size = new System.Drawing.Size(102, 24);
            this.buttonStartStop.TabIndex = 19;
            this.buttonStartStop.UseVisualStyleBackColor = false;
            this.buttonStartStop.Click += new System.EventHandler(this.buttonStartStop_Click);
            // 
            // labelHideDevices
            // 
            this.labelHideDevices.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelHideDevices.AutoSize = true;
            this.mainTableLayoutPanel.SetColumnSpan(this.labelHideDevices, 2);
            this.labelHideDevices.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHideDevices.ForeColor = System.Drawing.Color.Blue;
            this.labelHideDevices.Location = new System.Drawing.Point(3, 577);
            this.labelHideDevices.Name = "labelHideDevices";
            this.labelHideDevices.Size = new System.Drawing.Size(173, 15);
            this.labelHideDevices.TabIndex = 15;
            this.labelHideDevices.Text = "start once to fill  the devices list";
            this.labelHideDevices.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Rtl_433_Panel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = true;
            this.Controls.Add(this.mainTableLayoutPanel);
            this.Name = "Rtl_433_Panel";
            this.Size = new System.Drawing.Size(251, 1093);
            this.mainTableLayoutPanel.ResumeLayout(false);
            this.mainTableLayoutPanel.PerformLayout();
            this.groupBoxDataConv.ResumeLayout(false);
            this.groupBoxDataConv.PerformLayout();
            this.groupBoxMetadata.ResumeLayout(false);
            this.groupBoxMetadata.PerformLayout();
            this.groupBoxRecord.ResumeLayout(false);
            this.groupBoxRecord.PerformLayout();
            this.groupBoxFrequency.ResumeLayout(false);
            this.groupBoxFrequency.PerformLayout();
            this.groupBoxVerbose.ResumeLayout(false);
            this.groupBoxVerbose.PerformLayout();
            this.groupBoxSave.ResumeLayout(false);
            this.groupBoxSave.PerformLayout();
            this.groupBoxR.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.TableLayoutPanel mainTableLayoutPanel;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelSampleRate;
        private System.Windows.Forms.Button buttonClearMessages;
        private System.Windows.Forms.Button buttonDisplayParam;
        private System.Windows.Forms.GroupBox groupBoxVerbose;
        private System.Windows.Forms.RadioButton radioButtonNoV;
        private System.Windows.Forms.RadioButton radioButtonV;
        private System.Windows.Forms.RadioButton radioButtonVvvv;
        private System.Windows.Forms.Label labelFrequency;
        private System.Windows.Forms.Label labelCenterFrequency;
        private System.Windows.Forms.GroupBox groupBoxMetadata;
        private System.Windows.Forms.RadioButton radioButtonMLevel;
        private System.Windows.Forms.RadioButton radioButtonNoM;
        private System.Windows.Forms.RadioButton radioButtonVVV;
        private System.Windows.Forms.RadioButton radioButtonVV;
        private System.Windows.Forms.GroupBox groupBoxFrequency;
        private System.Windows.Forms.RadioButton radioButtonFreq915;
        private System.Windows.Forms.RadioButton radioButtonFreq868;
        private System.Windows.Forms.RadioButton radioButtonFreq43392;
        private System.Windows.Forms.RadioButton radioButtonFreq345;
        private System.Windows.Forms.RadioButton radioButtonFreq315;
        private System.Windows.Forms.RadioButton radioButtonFreqFree;
        private System.Windows.Forms.RichTextBox richTextBoxMessages;
        private System.Windows.Forms.GroupBox groupBoxRecord;
        private System.Windows.Forms.CheckBox checkBoxMONO;
        private System.Windows.Forms.CheckBox checkBoxSTEREO;
        private System.Windows.Forms.Button buttonCu8ToWav;
        private System.Windows.Forms.GroupBox groupBoxSave;
        private System.Windows.Forms.RadioButton radioButtonSnone;
        private System.Windows.Forms.RadioButton radioButtonSunknown;
        private System.Windows.Forms.RadioButton radioButtonSknown;
        private System.Windows.Forms.RadioButton radioButtonSall;
        private System.Windows.Forms.GroupBox groupBoxR;
        private System.Windows.Forms.ListBox listBoxHideDevices;
        private System.Windows.Forms.Label labelTimeCycle;
        private System.Windows.Forms.Label labelTimeRtl433;
        private System.Windows.Forms.Button buttonStartStop;
        private System.Windows.Forms.GroupBox groupBoxDataConv;
        private System.Windows.Forms.RadioButton radioButtonDataConvCustomary;
        private System.Windows.Forms.RadioButton radioButtonDataConvSI;
        private System.Windows.Forms.RadioButton radioButtonDataConvNative;
        private System.Windows.Forms.Label labelHideDevices;
    }
}