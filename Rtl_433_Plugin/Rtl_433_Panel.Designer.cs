using System;
using System.Collections.Generic;

namespace SDRSharp.Rtl_433
{
    partial class Rtl_433_Panel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(Boolean disposing)
        {
            //first plugin.close call by  SDRSharp.MainForm
             DisposePanel(disposing);
             GC.SuppressFinalize(this);
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
            this.groupBoxMetadata = new System.Windows.Forms.GroupBox();
            this.radioButtonMLevel = new System.Windows.Forms.RadioButton();
            this.radioButtonNoM = new System.Windows.Forms.RadioButton();
            this.richTextBoxMessages = new System.Windows.Forms.RichTextBox();
            this.groupBoxRecord = new System.Windows.Forms.GroupBox();
            this.checkBoxRaw = new System.Windows.Forms.CheckBox();
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
            this.radioButtonVVVV = new System.Windows.Forms.RadioButton();
            this.buttonClearMessages = new System.Windows.Forms.Button();
            this.buttonDisplayParam = new System.Windows.Forms.Button();
            this.groupBoxSave = new System.Windows.Forms.GroupBox();
            this.radioButtonSnone = new System.Windows.Forms.RadioButton();
            this.radioButtonSunknown = new System.Windows.Forms.RadioButton();
            this.radioButtonSknown = new System.Windows.Forms.RadioButton();
            this.radioButtonSall = new System.Windows.Forms.RadioButton();
            this.groupBoxR = new System.Windows.Forms.GroupBox();
            this.listBoxHideShowDevices = new System.Windows.Forms.ListBox();
            this.labelHideDevices = new System.Windows.Forms.Label();
            this.groupBoxHideShow = new System.Windows.Forms.GroupBox();
            this.radioButtonShowSelect = new System.Windows.Forms.RadioButton();
            this.radioButtonHideSelect = new System.Windows.Forms.RadioButton();
            this.groupBoxSelectTypeForm = new System.Windows.Forms.GroupBox();
            this.radioButtonListMessages = new System.Windows.Forms.RadioButton();
            this.radioButtonListDevices = new System.Windows.Forms.RadioButton();
            this.radioButtonGraph = new System.Windows.Forms.RadioButton();
            this.groupBoxEnabledDisabledDevices = new System.Windows.Forms.GroupBox();
            this.checkBoxEnabledDevicesDisabled = new System.Windows.Forms.CheckBox();
            this.groupBoxOptionY = new System.Windows.Forms.GroupBox();
            this.radioButtonYMinmax = new System.Windows.Forms.RadioButton();
            this.radioButtonYClassic = new System.Windows.Forms.RadioButton();
            this.radioButtonYAuto = new System.Windows.Forms.RadioButton();
            this.numericUpDownMinimumSNRPulses = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownMinimumDetectionLevel = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownPulseDetectionLevel = new System.Windows.Forms.NumericUpDown();
            this.checkBoxYAmpest = new System.Windows.Forms.CheckBox();
            this.checkBoxYSquelch = new System.Windows.Forms.CheckBox();
            this.checkBoxYAutoLevel = new System.Windows.Forms.CheckBox();
            this.checkBoxYMinimumSNRPulses = new System.Windows.Forms.CheckBox();
            this.checkBoxYMinimumDetectionLevelPulses = new System.Windows.Forms.CheckBox();
            this.checkBoxYPulsesDetectionLevel = new System.Windows.Forms.CheckBox();
            this.checkBoxEnabledPlugin = new System.Windows.Forms.CheckBox();
            this.groupBoxInfos = new System.Windows.Forms.GroupBox();
            this.labelSampleRateTxt = new System.Windows.Forms.Label();
            this.labelTimeCycle = new System.Windows.Forms.Label();
            this.labelFrequencyTxt = new System.Windows.Forms.Label();
            this.labelVersionTxt = new System.Windows.Forms.Label();
            this.labelSampleRate = new System.Windows.Forms.Label();
            this.labelFrequency = new System.Windows.Forms.Label();
            this.labelCycleTime = new System.Windows.Forms.Label();
            this.labelTime433 = new System.Windows.Forms.Label();
            this.labelTimeRtl433 = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.buttonStartStop = new System.Windows.Forms.Button();
            this.buttonCu8ToWav = new System.Windows.Forms.Button();
            this.groupBoxRecordTxtFile = new System.Windows.Forms.GroupBox();
            this.checkBoxRecordTxtFile = new System.Windows.Forms.CheckBox();
            this.labelWarningRecordTextFile = new System.Windows.Forms.Label();
            this.mainTableLayoutPanel.SuspendLayout();
            this.groupBoxDataConv.SuspendLayout();
            this.groupBoxMetadata.SuspendLayout();
            this.groupBoxRecord.SuspendLayout();
            this.groupBoxFrequency.SuspendLayout();
            this.groupBoxVerbose.SuspendLayout();
            this.groupBoxSave.SuspendLayout();
            this.groupBoxR.SuspendLayout();
            this.groupBoxHideShow.SuspendLayout();
            this.groupBoxSelectTypeForm.SuspendLayout();
            this.groupBoxEnabledDisabledDevices.SuspendLayout();
            this.groupBoxOptionY.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinimumSNRPulses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinimumDetectionLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPulseDetectionLevel)).BeginInit();
            this.groupBoxInfos.SuspendLayout();
            this.groupBoxRecordTxtFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTableLayoutPanel
            // 
            this.mainTableLayoutPanel.AutoScroll = true;
            this.mainTableLayoutPanel.AutoSize = true;
            this.mainTableLayoutPanel.BackColor = System.Drawing.SystemColors.Desktop;
            this.mainTableLayoutPanel.CausesValidation = false;
            this.mainTableLayoutPanel.ColumnCount = 2;
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxDataConv, 0, 6);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxMetadata, 1, 5);
            this.mainTableLayoutPanel.Controls.Add(this.richTextBoxMessages, 0, 12);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxRecord, 1, 3);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxFrequency, 1, 2);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxVerbose, 0, 5);
            this.mainTableLayoutPanel.Controls.Add(this.buttonClearMessages, 1, 11);
            this.mainTableLayoutPanel.Controls.Add(this.buttonDisplayParam, 0, 11);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxSave, 1, 6);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxR, 0, 9);
            this.mainTableLayoutPanel.Controls.Add(this.labelHideDevices, 0, 7);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxHideShow, 0, 8);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxSelectTypeForm, 0, 1);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxEnabledDisabledDevices, 0, 10);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxOptionY, 0, 13);
            this.mainTableLayoutPanel.Controls.Add(this.checkBoxEnabledPlugin, 0, 0);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxInfos, 0, 4);
            this.mainTableLayoutPanel.Controls.Add(this.buttonStartStop, 1, 0);
            this.mainTableLayoutPanel.Controls.Add(this.buttonCu8ToWav, 0, 3);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxRecordTxtFile, 0, 2);
            this.mainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayoutPanel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.mainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            this.mainTableLayoutPanel.RowCount = 14;
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 122F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 87F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 144F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 141F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 119F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 126F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.mainTableLayoutPanel.Size = new System.Drawing.Size(296, 1443);
            this.mainTableLayoutPanel.TabIndex = 1;
            this.mainTableLayoutPanel.CellPaint += new System.Windows.Forms.TableLayoutCellPaintEventHandler(this.mainTableLayoutPanel_CellPaint);
            // 
            // groupBoxDataConv
            // 
            this.groupBoxDataConv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDataConv.BackColor = System.Drawing.SystemColors.Desktop;
            this.groupBoxDataConv.Controls.Add(this.radioButtonDataConvCustomary);
            this.groupBoxDataConv.Controls.Add(this.radioButtonDataConvSI);
            this.groupBoxDataConv.Controls.Add(this.radioButtonDataConvNative);
            this.groupBoxDataConv.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBoxDataConv.Location = new System.Drawing.Point(3, 567);
            this.groupBoxDataConv.Name = "groupBoxDataConv";
            this.groupBoxDataConv.Size = new System.Drawing.Size(144, 113);
            this.groupBoxDataConv.TabIndex = 20;
            this.groupBoxDataConv.TabStop = false;
            this.groupBoxDataConv.Text = "Data Conv(-C)";
            // 
            // radioButtonDataConvCustomary
            // 
            this.radioButtonDataConvCustomary.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.radioButtonDataConvCustomary.Location = new System.Drawing.Point(9, 65);
            this.radioButtonDataConvCustomary.Name = "radioButtonDataConvCustomary";
            this.radioButtonDataConvCustomary.Size = new System.Drawing.Size(83, 17);
            this.radioButtonDataConvCustomary.TabIndex = 11;
            this.radioButtonDataConvCustomary.Tag = "dataconv";
            this.radioButtonDataConvCustomary.Text = "-Ccustomary";
            this.radioButtonDataConvCustomary.UseVisualStyleBackColor = true;
            this.radioButtonDataConvCustomary.Paint += new System.Windows.Forms.PaintEventHandler(this.radioButton_Paint);
            // 
            // radioButtonDataConvSI
            // 
            this.radioButtonDataConvSI.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.radioButtonDataConvSI.Location = new System.Drawing.Point(9, 42);
            this.radioButtonDataConvSI.Name = "radioButtonDataConvSI";
            this.radioButtonDataConvSI.Size = new System.Drawing.Size(42, 17);
            this.radioButtonDataConvSI.TabIndex = 10;
            this.radioButtonDataConvSI.Tag = "dataconv";
            this.radioButtonDataConvSI.Text = "-Csi";
            this.radioButtonDataConvSI.UseVisualStyleBackColor = true;
            this.radioButtonDataConvSI.Paint += new System.Windows.Forms.PaintEventHandler(this.radioButton_Paint);
            // 
            // radioButtonDataConvNative
            // 
            this.radioButtonDataConvNative.Checked = true;
            this.radioButtonDataConvNative.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.radioButtonDataConvNative.Location = new System.Drawing.Point(9, 19);
            this.radioButtonDataConvNative.Name = "radioButtonDataConvNative";
            this.radioButtonDataConvNative.Size = new System.Drawing.Size(64, 17);
            this.radioButtonDataConvNative.TabIndex = 9;
            this.radioButtonDataConvNative.TabStop = true;
            this.radioButtonDataConvNative.Tag = "dataconv";
            this.radioButtonDataConvNative.Text = "-Cnative";
            this.radioButtonDataConvNative.UseVisualStyleBackColor = true;
            this.radioButtonDataConvNative.Paint += new System.Windows.Forms.PaintEventHandler(this.radioButton_Paint);
            // 
            // groupBoxMetadata
            // 
            this.groupBoxMetadata.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxMetadata.Controls.Add(this.radioButtonMLevel);
            this.groupBoxMetadata.Controls.Add(this.radioButtonNoM);
            this.groupBoxMetadata.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBoxMetadata.Location = new System.Drawing.Point(153, 426);
            this.groupBoxMetadata.Name = "groupBoxMetadata";
            this.groupBoxMetadata.Size = new System.Drawing.Size(140, 135);
            this.groupBoxMetadata.TabIndex = 10;
            this.groupBoxMetadata.TabStop = false;
            this.groupBoxMetadata.Text = "metaData(-M)";
            // 
            // radioButtonMLevel
            // 
            this.radioButtonMLevel.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.radioButtonMLevel.Location = new System.Drawing.Point(11, 42);
            this.radioButtonMLevel.Name = "radioButtonMLevel";
            this.radioButtonMLevel.Size = new System.Drawing.Size(59, 17);
            this.radioButtonMLevel.TabIndex = 8;
            this.radioButtonMLevel.Tag = "MbitsOrLevel";
            this.radioButtonMLevel.Text = "-Mlevel";
            this.radioButtonMLevel.UseVisualStyleBackColor = false;
            this.radioButtonMLevel.Paint += new System.Windows.Forms.PaintEventHandler(this.radioButton_Paint);
            // 
            // radioButtonNoM
            // 
            this.radioButtonNoM.Checked = true;
            this.radioButtonNoM.Location = new System.Drawing.Point(11, 19);
            this.radioButtonNoM.Name = "radioButtonNoM";
            this.radioButtonNoM.Size = new System.Drawing.Size(54, 17);
            this.radioButtonNoM.TabIndex = 8;
            this.radioButtonNoM.TabStop = true;
            this.radioButtonNoM.Tag = "MbitsOrLevel";
            this.radioButtonNoM.Text = "No -M";
            this.radioButtonNoM.UseVisualStyleBackColor = false;
            this.radioButtonNoM.Paint += new System.Windows.Forms.PaintEventHandler(this.radioButton_Paint);
            // 
            // richTextBoxMessages
            // 
            this.richTextBoxMessages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxMessages.BackColor = System.Drawing.SystemColors.Desktop;
            this.mainTableLayoutPanel.SetColumnSpan(this.richTextBoxMessages, 2);
            this.richTextBoxMessages.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.richTextBoxMessages.Location = new System.Drawing.Point(3, 959);
            this.richTextBoxMessages.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.richTextBoxMessages.MaxLength = 3000;
            this.richTextBoxMessages.Name = "richTextBoxMessages";
            this.richTextBoxMessages.ReadOnly = true;
            this.richTextBoxMessages.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.richTextBoxMessages.Size = new System.Drawing.Size(290, 257);
            this.richTextBoxMessages.TabIndex = 5;
            this.richTextBoxMessages.Text = "";
            // 
            // groupBoxRecord
            // 
            this.groupBoxRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxRecord.BackColor = System.Drawing.SystemColors.Desktop;
            this.groupBoxRecord.Controls.Add(this.checkBoxRaw);
            this.groupBoxRecord.Controls.Add(this.checkBoxMONO);
            this.groupBoxRecord.Controls.Add(this.checkBoxSTEREO);
            this.groupBoxRecord.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBoxRecord.Location = new System.Drawing.Point(153, 195);
            this.groupBoxRecord.Name = "groupBoxRecord";
            this.groupBoxRecord.Size = new System.Drawing.Size(140, 81);
            this.groupBoxRecord.TabIndex = 12;
            this.groupBoxRecord.TabStop = false;
            this.groupBoxRecord.Text = "Record";
            // 
            // checkBoxRaw
            // 
            this.checkBoxRaw.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.checkBoxRaw.Location = new System.Drawing.Point(6, 61);
            this.checkBoxRaw.Name = "checkBoxRaw";
            this.checkBoxRaw.Size = new System.Drawing.Size(59, 17);
            this.checkBoxRaw.TabIndex = 0;
            this.checkBoxRaw.Text = "RAW";
            this.checkBoxRaw.UseVisualStyleBackColor = true;
            this.checkBoxRaw.Visible = false;
            this.checkBoxRaw.CheckedChanged += new System.EventHandler(this.checkBoxRaw_CheckedChanged);
            // 
            // checkBoxMONO
            // 
            this.checkBoxMONO.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.checkBoxMONO.Location = new System.Drawing.Point(6, 39);
            this.checkBoxMONO.Name = "checkBoxMONO";
            this.checkBoxMONO.Size = new System.Drawing.Size(59, 17);
            this.checkBoxMONO.TabIndex = 0;
            this.checkBoxMONO.Text = "MONO";
            this.checkBoxMONO.UseVisualStyleBackColor = true;
            this.checkBoxMONO.CheckedChanged += new System.EventHandler(this.checkBoxMONO_CheckedChanged);
            // 
            // checkBoxSTEREO
            // 
            this.checkBoxSTEREO.ForeColor = System.Drawing.SystemColors.ControlLightLight;
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
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxFrequency.Controls.Add(this.radioButtonFreq915);
            this.groupBoxFrequency.Controls.Add(this.radioButtonFreq868);
            this.groupBoxFrequency.Controls.Add(this.radioButtonFreq43392);
            this.groupBoxFrequency.Controls.Add(this.radioButtonFreq345);
            this.groupBoxFrequency.Controls.Add(this.radioButtonFreq315);
            this.groupBoxFrequency.Controls.Add(this.radioButtonFreqFree);
            this.groupBoxFrequency.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBoxFrequency.Location = new System.Drawing.Point(153, 73);
            this.groupBoxFrequency.Name = "groupBoxFrequency";
            this.groupBoxFrequency.Size = new System.Drawing.Size(140, 116);
            this.groupBoxFrequency.TabIndex = 13;
            this.groupBoxFrequency.TabStop = false;
            this.groupBoxFrequency.Text = "Frequency";
            // 
            // radioButtonFreq915
            // 
            this.radioButtonFreq915.Location = new System.Drawing.Point(71, 65);
            this.radioButtonFreq915.Name = "radioButtonFreq915";
            this.radioButtonFreq915.Size = new System.Drawing.Size(66, 17);
            this.radioButtonFreq915.TabIndex = 5;
            this.radioButtonFreq915.Tag = "915000000";
            this.radioButtonFreq915.Text = "915 Mhz";
            this.radioButtonFreq915.UseVisualStyleBackColor = true;
            this.radioButtonFreq915.Paint += new System.Windows.Forms.PaintEventHandler(this.radioButton_Paint);
            // 
            // radioButtonFreq868
            // 
            this.radioButtonFreq868.Location = new System.Drawing.Point(71, 42);
            this.radioButtonFreq868.Name = "radioButtonFreq868";
            this.radioButtonFreq868.Size = new System.Drawing.Size(66, 17);
            this.radioButtonFreq868.TabIndex = 4;
            this.radioButtonFreq868.Tag = "868000000";
            this.radioButtonFreq868.Text = "868 Mhz";
            this.radioButtonFreq868.UseVisualStyleBackColor = true;
            this.radioButtonFreq868.Paint += new System.Windows.Forms.PaintEventHandler(this.radioButton_Paint);
            // 
            // radioButtonFreq43392
            // 
            this.radioButtonFreq43392.Location = new System.Drawing.Point(56, 19);
            this.radioButtonFreq43392.Name = "radioButtonFreq43392";
            this.radioButtonFreq43392.Size = new System.Drawing.Size(81, 17);
            this.radioButtonFreq43392.TabIndex = 3;
            this.radioButtonFreq43392.Tag = "433920000";
            this.radioButtonFreq43392.Text = "433.92 Mhz";
            this.radioButtonFreq43392.UseVisualStyleBackColor = true;
            this.radioButtonFreq43392.Paint += new System.Windows.Forms.PaintEventHandler(this.radioButton_Paint);
            // 
            // radioButtonFreq345
            // 
            this.radioButtonFreq345.Location = new System.Drawing.Point(6, 65);
            this.radioButtonFreq345.Name = "radioButtonFreq345";
            this.radioButtonFreq345.Size = new System.Drawing.Size(66, 17);
            this.radioButtonFreq345.TabIndex = 2;
            this.radioButtonFreq345.Tag = "345000000";
            this.radioButtonFreq345.Text = "345 Mhz";
            this.radioButtonFreq345.UseVisualStyleBackColor = true;
            this.radioButtonFreq345.Paint += new System.Windows.Forms.PaintEventHandler(this.radioButton_Paint);
            // 
            // radioButtonFreq315
            // 
            this.radioButtonFreq315.Location = new System.Drawing.Point(6, 42);
            this.radioButtonFreq315.Name = "radioButtonFreq315";
            this.radioButtonFreq315.Size = new System.Drawing.Size(66, 17);
            this.radioButtonFreq315.TabIndex = 1;
            this.radioButtonFreq315.Tag = "315000000";
            this.radioButtonFreq315.Text = "315 Mhz";
            this.radioButtonFreq315.UseVisualStyleBackColor = true;
            this.radioButtonFreq315.Paint += new System.Windows.Forms.PaintEventHandler(this.radioButton_Paint);
            // 
            // radioButtonFreqFree
            // 
            this.radioButtonFreqFree.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.radioButtonFreqFree.Location = new System.Drawing.Point(6, 19);
            this.radioButtonFreqFree.Name = "radioButtonFreqFree";
            this.radioButtonFreqFree.Size = new System.Drawing.Size(46, 17);
            this.radioButtonFreqFree.TabIndex = 0;
            this.radioButtonFreqFree.Tag = "0";
            this.radioButtonFreqFree.Text = "Free";
            this.radioButtonFreqFree.UseVisualStyleBackColor = true;
            this.radioButtonFreqFree.Paint += new System.Windows.Forms.PaintEventHandler(this.radioButton_Paint);
            // 
            // groupBoxVerbose
            // 
            this.groupBoxVerbose.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxVerbose.Controls.Add(this.radioButtonNoV);
            this.groupBoxVerbose.Controls.Add(this.radioButtonVVV);
            this.groupBoxVerbose.Controls.Add(this.radioButtonVV);
            this.groupBoxVerbose.Controls.Add(this.radioButtonV);
            this.groupBoxVerbose.Controls.Add(this.radioButtonVVVV);
            this.groupBoxVerbose.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBoxVerbose.Location = new System.Drawing.Point(3, 426);
            this.groupBoxVerbose.Name = "groupBoxVerbose";
            this.groupBoxVerbose.Size = new System.Drawing.Size(144, 135);
            this.groupBoxVerbose.TabIndex = 9;
            this.groupBoxVerbose.TabStop = false;
            this.groupBoxVerbose.Text = "verbose(-v)";
            // 
            // radioButtonNoV
            // 
            this.radioButtonNoV.Checked = true;
            this.radioButtonNoV.Location = new System.Drawing.Point(9, 19);
            this.radioButtonNoV.Name = "radioButtonNoV";
            this.radioButtonNoV.Size = new System.Drawing.Size(51, 17);
            this.radioButtonNoV.TabIndex = 8;
            this.radioButtonNoV.TabStop = true;
            this.radioButtonNoV.Tag = "verbose";
            this.radioButtonNoV.Text = "No -v";
            this.radioButtonNoV.UseVisualStyleBackColor = true;
            this.radioButtonNoV.Paint += new System.Windows.Forms.PaintEventHandler(this.radioButton_Paint);
            // 
            // radioButtonVVV
            // 
            this.radioButtonVVV.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.radioButtonVVV.Location = new System.Drawing.Point(9, 89);
            this.radioButtonVVV.Name = "radioButtonVVV";
            this.radioButtonVVV.Size = new System.Drawing.Size(46, 17);
            this.radioButtonVVV.TabIndex = 8;
            this.radioButtonVVV.Tag = "verbose";
            this.radioButtonVVV.Text = "-vvv";
            this.radioButtonVVV.UseVisualStyleBackColor = true;
            this.radioButtonVVV.Paint += new System.Windows.Forms.PaintEventHandler(this.radioButton_Paint);
            // 
            // radioButtonVV
            // 
            this.radioButtonVV.Location = new System.Drawing.Point(9, 68);
            this.radioButtonVV.Name = "radioButtonVV";
            this.radioButtonVV.Size = new System.Drawing.Size(40, 17);
            this.radioButtonVV.TabIndex = 8;
            this.radioButtonVV.Tag = "verbose";
            this.radioButtonVV.Text = "-vv";
            this.radioButtonVV.UseVisualStyleBackColor = true;
            this.radioButtonVV.Paint += new System.Windows.Forms.PaintEventHandler(this.radioButton_Paint);
            // 
            // radioButtonV
            // 
            this.radioButtonV.Location = new System.Drawing.Point(9, 42);
            this.radioButtonV.Name = "radioButtonV";
            this.radioButtonV.Size = new System.Drawing.Size(34, 17);
            this.radioButtonV.TabIndex = 8;
            this.radioButtonV.Tag = "verbose";
            this.radioButtonV.Text = "-v";
            this.radioButtonV.UseVisualStyleBackColor = true;
            this.radioButtonV.Paint += new System.Windows.Forms.PaintEventHandler(this.radioButton_Paint);
            // 
            // radioButtonVVVV
            // 
            this.radioButtonVVVV.Location = new System.Drawing.Point(9, 112);
            this.radioButtonVVVV.Name = "radioButtonVVVV";
            this.radioButtonVVVV.Size = new System.Drawing.Size(52, 17);
            this.radioButtonVVVV.TabIndex = 8;
            this.radioButtonVVVV.Tag = "verbose";
            this.radioButtonVVVV.Text = "-vvvv";
            this.radioButtonVVVV.UseVisualStyleBackColor = true;
            this.radioButtonVVVV.Paint += new System.Windows.Forms.PaintEventHandler(this.radioButton_Paint);
            // 
            // buttonClearMessages
            // 
            this.buttonClearMessages.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonClearMessages.AutoSize = true;
            this.buttonClearMessages.BackColor = System.Drawing.SystemColors.Desktop;
            this.buttonClearMessages.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonClearMessages.Location = new System.Drawing.Point(165, 929);
            this.buttonClearMessages.Name = "buttonClearMessages";
            this.buttonClearMessages.Size = new System.Drawing.Size(115, 23);
            this.buttonClearMessages.TabIndex = 7;
            this.buttonClearMessages.Text = "Clear Messages";
            this.buttonClearMessages.UseVisualStyleBackColor = false;
            this.buttonClearMessages.Click += new System.EventHandler(this.buttonClearMessages_Click);
            // 
            // buttonDisplayParam
            // 
            this.buttonDisplayParam.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonDisplayParam.AutoSize = true;
            this.buttonDisplayParam.BackColor = System.Drawing.SystemColors.Desktop;
            this.buttonDisplayParam.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonDisplayParam.Location = new System.Drawing.Point(31, 929);
            this.buttonDisplayParam.Name = "buttonDisplayParam";
            this.buttonDisplayParam.Size = new System.Drawing.Size(87, 23);
            this.buttonDisplayParam.TabIndex = 7;
            this.buttonDisplayParam.Text = "Display Param";
            this.buttonDisplayParam.UseVisualStyleBackColor = false;
            this.buttonDisplayParam.Click += new System.EventHandler(this.buttonDisplayParam_Click);
            // 
            // groupBoxSave
            // 
            this.groupBoxSave.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxSave.Controls.Add(this.radioButtonSnone);
            this.groupBoxSave.Controls.Add(this.radioButtonSunknown);
            this.groupBoxSave.Controls.Add(this.radioButtonSknown);
            this.groupBoxSave.Controls.Add(this.radioButtonSall);
            this.groupBoxSave.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBoxSave.Location = new System.Drawing.Point(153, 567);
            this.groupBoxSave.Name = "groupBoxSave";
            this.groupBoxSave.Size = new System.Drawing.Size(140, 113);
            this.groupBoxSave.TabIndex = 9;
            this.groupBoxSave.TabStop = false;
            this.groupBoxSave.Text = "save(-S)";
            // 
            // radioButtonSnone
            // 
            this.radioButtonSnone.Checked = true;
            this.radioButtonSnone.Location = new System.Drawing.Point(9, 19);
            this.radioButtonSnone.Name = "radioButtonSnone";
            this.radioButtonSnone.Size = new System.Drawing.Size(52, 17);
            this.radioButtonSnone.TabIndex = 8;
            this.radioButtonSnone.TabStop = true;
            this.radioButtonSnone.Tag = "saveDevice";
            this.radioButtonSnone.Text = "No -S";
            this.radioButtonSnone.UseVisualStyleBackColor = false;
            this.radioButtonSnone.Paint += new System.Windows.Forms.PaintEventHandler(this.radioButton_Paint);
            // 
            // radioButtonSunknown
            // 
            this.radioButtonSunknown.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.radioButtonSunknown.Location = new System.Drawing.Point(9, 89);
            this.radioButtonSunknown.Name = "radioButtonSunknown";
            this.radioButtonSunknown.Size = new System.Drawing.Size(100, 17);
            this.radioButtonSunknown.TabIndex = 8;
            this.radioButtonSunknown.Tag = "saveDevice";
            this.radioButtonSunknown.Text = "-Sunknown";
            this.radioButtonSunknown.UseVisualStyleBackColor = false;
            this.radioButtonSunknown.Paint += new System.Windows.Forms.PaintEventHandler(this.radioButton_Paint);
            // 
            // radioButtonSknown
            // 
            this.radioButtonSknown.Location = new System.Drawing.Point(9, 68);
            this.radioButtonSknown.Name = "radioButtonSknown";
            this.radioButtonSknown.Size = new System.Drawing.Size(67, 17);
            this.radioButtonSknown.TabIndex = 8;
            this.radioButtonSknown.Tag = "saveDevice";
            this.radioButtonSknown.Text = "-Sknown";
            this.radioButtonSknown.UseVisualStyleBackColor = false;
            this.radioButtonSknown.Paint += new System.Windows.Forms.PaintEventHandler(this.radioButton_Paint);
            // 
            // radioButtonSall
            // 
            this.radioButtonSall.Location = new System.Drawing.Point(9, 42);
            this.radioButtonSall.Name = "radioButtonSall";
            this.radioButtonSall.Size = new System.Drawing.Size(100, 17);
            this.radioButtonSall.TabIndex = 8;
            this.radioButtonSall.Tag = "saveDevice";
            this.radioButtonSall.Text = "-Sall";
            this.radioButtonSall.UseVisualStyleBackColor = false;
            this.radioButtonSall.Paint += new System.Windows.Forms.PaintEventHandler(this.radioButton_Paint);
            // 
            // groupBoxR
            // 
            this.groupBoxR.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTableLayoutPanel.SetColumnSpan(this.groupBoxR, 2);
            this.groupBoxR.Controls.Add(this.listBoxHideShowDevices);
            this.groupBoxR.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBoxR.Location = new System.Drawing.Point(3, 755);
            this.groupBoxR.Name = "groupBoxR";
            this.groupBoxR.Size = new System.Drawing.Size(290, 120);
            this.groupBoxR.TabIndex = 9;
            this.groupBoxR.TabStop = false;
            this.groupBoxR.Text = "hide show devices(-R)";
            // 
            // listBoxHideShowDevices
            // 
            this.listBoxHideShowDevices.BackColor = System.Drawing.SystemColors.Desktop;
            this.listBoxHideShowDevices.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxHideShowDevices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxHideShowDevices.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.listBoxHideShowDevices.FormattingEnabled = true;
            this.listBoxHideShowDevices.HorizontalScrollbar = true;
            this.listBoxHideShowDevices.Location = new System.Drawing.Point(3, 16);
            this.listBoxHideShowDevices.Name = "listBoxHideShowDevices";
            this.listBoxHideShowDevices.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBoxHideShowDevices.Size = new System.Drawing.Size(284, 101);
            this.listBoxHideShowDevices.TabIndex = 3;
            // 
            // labelHideDevices
            // 
            this.labelHideDevices.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelHideDevices.AutoSize = true;
            this.mainTableLayoutPanel.SetColumnSpan(this.labelHideDevices, 2);
            this.labelHideDevices.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHideDevices.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelHideDevices.Location = new System.Drawing.Point(3, 688);
            this.labelHideDevices.Name = "labelHideDevices";
            this.labelHideDevices.Size = new System.Drawing.Size(173, 15);
            this.labelHideDevices.TabIndex = 15;
            this.labelHideDevices.Text = "start once to fill  the devices list";
            this.labelHideDevices.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBoxHideShow
            // 
            this.groupBoxHideShow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.mainTableLayoutPanel.SetColumnSpan(this.groupBoxHideShow, 2);
            this.groupBoxHideShow.Controls.Add(this.radioButtonShowSelect);
            this.groupBoxHideShow.Controls.Add(this.radioButtonHideSelect);
            this.groupBoxHideShow.Location = new System.Drawing.Point(3, 712);
            this.groupBoxHideShow.Name = "groupBoxHideShow";
            this.groupBoxHideShow.Size = new System.Drawing.Size(260, 37);
            this.groupBoxHideShow.TabIndex = 21;
            this.groupBoxHideShow.TabStop = false;
            // 
            // radioButtonShowSelect
            // 
            this.radioButtonShowSelect.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.radioButtonShowSelect.Location = new System.Drawing.Point(115, 14);
            this.radioButtonShowSelect.Name = "radioButtonShowSelect";
            this.radioButtonShowSelect.Size = new System.Drawing.Size(83, 17);
            this.radioButtonShowSelect.TabIndex = 0;
            this.radioButtonShowSelect.Text = "Show select";
            this.radioButtonShowSelect.UseVisualStyleBackColor = true;
            this.radioButtonShowSelect.Paint += new System.Windows.Forms.PaintEventHandler(this.radioButton_Paint);
            // 
            // radioButtonHideSelect
            // 
            this.radioButtonHideSelect.Checked = true;
            this.radioButtonHideSelect.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.radioButtonHideSelect.Location = new System.Drawing.Point(6, 14);
            this.radioButtonHideSelect.Name = "radioButtonHideSelect";
            this.radioButtonHideSelect.Size = new System.Drawing.Size(78, 17);
            this.radioButtonHideSelect.TabIndex = 0;
            this.radioButtonHideSelect.TabStop = true;
            this.radioButtonHideSelect.Text = "Hide select";
            this.radioButtonHideSelect.UseVisualStyleBackColor = true;
            this.radioButtonHideSelect.Paint += new System.Windows.Forms.PaintEventHandler(this.radioButton_Paint);
            // 
            // groupBoxSelectTypeForm
            // 
            this.groupBoxSelectTypeForm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.mainTableLayoutPanel.SetColumnSpan(this.groupBoxSelectTypeForm, 2);
            this.groupBoxSelectTypeForm.Controls.Add(this.radioButtonListMessages);
            this.groupBoxSelectTypeForm.Controls.Add(this.radioButtonListDevices);
            this.groupBoxSelectTypeForm.Controls.Add(this.radioButtonGraph);
            this.groupBoxSelectTypeForm.Location = new System.Drawing.Point(3, 33);
            this.groupBoxSelectTypeForm.Name = "groupBoxSelectTypeForm";
            this.groupBoxSelectTypeForm.Size = new System.Drawing.Size(260, 34);
            this.groupBoxSelectTypeForm.TabIndex = 22;
            this.groupBoxSelectTypeForm.TabStop = false;
            // 
            // radioButtonListMessages
            // 
            this.radioButtonListMessages.Checked = true;
            this.radioButtonListMessages.Location = new System.Drawing.Point(4, 11);
            this.radioButtonListMessages.Name = "radioButtonListMessages";
            this.radioButtonListMessages.Size = new System.Drawing.Size(91, 17);
            this.radioButtonListMessages.TabIndex = 1;
            this.radioButtonListMessages.TabStop = true;
            this.radioButtonListMessages.Text = "List messages";
            this.radioButtonListMessages.UseVisualStyleBackColor = true;
            this.radioButtonListMessages.CheckedChanged += new System.EventHandler(this.radioButtonTypeWindow_CheckedChanged);
            this.radioButtonListMessages.Paint += new System.Windows.Forms.PaintEventHandler(this.radioButton_Paint);
            // 
            // radioButtonListDevices
            // 
            this.radioButtonListDevices.Location = new System.Drawing.Point(173, 11);
            this.radioButtonListDevices.Name = "radioButtonListDevices";
            this.radioButtonListDevices.Size = new System.Drawing.Size(81, 17);
            this.radioButtonListDevices.TabIndex = 0;
            this.radioButtonListDevices.Text = "List devices";
            this.radioButtonListDevices.UseVisualStyleBackColor = true;
            this.radioButtonListDevices.CheckedChanged += new System.EventHandler(this.radioButtonTypeWindow_CheckedChanged);
            this.radioButtonListDevices.Paint += new System.Windows.Forms.PaintEventHandler(this.radioButton_Paint);
            // 
            // radioButtonGraph
            // 
            this.radioButtonGraph.Location = new System.Drawing.Point(101, 11);
            this.radioButtonGraph.Name = "radioButtonGraph";
            this.radioButtonGraph.Size = new System.Drawing.Size(54, 17);
            this.radioButtonGraph.TabIndex = 0;
            this.radioButtonGraph.Text = "Graph";
            this.radioButtonGraph.UseVisualStyleBackColor = true;
            this.radioButtonGraph.CheckedChanged += new System.EventHandler(this.radioButtonTypeWindow_CheckedChanged);
            this.radioButtonGraph.Paint += new System.Windows.Forms.PaintEventHandler(this.radioButton_Paint);
            // 
            // groupBoxEnabledDisabledDevices
            // 
            this.groupBoxEnabledDisabledDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTableLayoutPanel.SetColumnSpan(this.groupBoxEnabledDisabledDevices, 2);
            this.groupBoxEnabledDisabledDevices.Controls.Add(this.checkBoxEnabledDevicesDisabled);
            this.groupBoxEnabledDisabledDevices.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBoxEnabledDisabledDevices.Location = new System.Drawing.Point(3, 881);
            this.groupBoxEnabledDisabledDevices.Name = "groupBoxEnabledDisabledDevices";
            this.groupBoxEnabledDisabledDevices.Size = new System.Drawing.Size(290, 41);
            this.groupBoxEnabledDisabledDevices.TabIndex = 26;
            this.groupBoxEnabledDisabledDevices.TabStop = false;
            this.groupBoxEnabledDisabledDevices.Text = "Enabled devices disabled";
            // 
            // checkBoxEnabledDevicesDisabled
            // 
            this.checkBoxEnabledDevicesDisabled.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.checkBoxEnabledDevicesDisabled.Location = new System.Drawing.Point(13, 18);
            this.checkBoxEnabledDevicesDisabled.Name = "checkBoxEnabledDevicesDisabled";
            this.checkBoxEnabledDevicesDisabled.Size = new System.Drawing.Size(82, 17);
            this.checkBoxEnabledDevicesDisabled.TabIndex = 25;
            this.checkBoxEnabledDevicesDisabled.Text = "Default";
            this.checkBoxEnabledDevicesDisabled.UseVisualStyleBackColor = true;
            this.checkBoxEnabledDevicesDisabled.CheckedChanged += new System.EventHandler(this.checkBoxEnabledDevicesDisabled_CheckedChanged);
            // 
            // groupBoxOptionY
            // 
            this.groupBoxOptionY.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTableLayoutPanel.SetColumnSpan(this.groupBoxOptionY, 2);
            this.groupBoxOptionY.Controls.Add(this.radioButtonYMinmax);
            this.groupBoxOptionY.Controls.Add(this.radioButtonYClassic);
            this.groupBoxOptionY.Controls.Add(this.radioButtonYAuto);
            this.groupBoxOptionY.Controls.Add(this.numericUpDownMinimumSNRPulses);
            this.groupBoxOptionY.Controls.Add(this.numericUpDownMinimumDetectionLevel);
            this.groupBoxOptionY.Controls.Add(this.numericUpDownPulseDetectionLevel);
            this.groupBoxOptionY.Controls.Add(this.checkBoxYAmpest);
            this.groupBoxOptionY.Controls.Add(this.checkBoxYSquelch);
            this.groupBoxOptionY.Controls.Add(this.checkBoxYAutoLevel);
            this.groupBoxOptionY.Controls.Add(this.checkBoxYMinimumSNRPulses);
            this.groupBoxOptionY.Controls.Add(this.checkBoxYMinimumDetectionLevelPulses);
            this.groupBoxOptionY.Controls.Add(this.checkBoxYPulsesDetectionLevel);
            this.groupBoxOptionY.Location = new System.Drawing.Point(3, 1224);
            this.groupBoxOptionY.Name = "groupBoxOptionY";
            this.groupBoxOptionY.Size = new System.Drawing.Size(290, 216);
            this.groupBoxOptionY.TabIndex = 27;
            this.groupBoxOptionY.TabStop = false;
            this.groupBoxOptionY.Text = "options -Y";
            // 
            // radioButtonYMinmax
            // 
            this.radioButtonYMinmax.AutoSize = true;
            this.radioButtonYMinmax.Location = new System.Drawing.Point(13, 59);
            this.radioButtonYMinmax.Name = "radioButtonYMinmax";
            this.radioButtonYMinmax.Size = new System.Drawing.Size(153, 17);
            this.radioButtonYMinmax.TabIndex = 2;
            this.radioButtonYMinmax.TabStop = true;
            this.radioButtonYMinmax.Tag = "-Yminmax";
            this.radioButtonYMinmax.Text = "FSK pulse detector minmax";
            this.radioButtonYMinmax.UseVisualStyleBackColor = true;
            this.radioButtonYMinmax.CheckedChanged += new System.EventHandler(this.radioButtonYFSK_CheckedChanged);
            // 
            // radioButtonYClassic
            // 
            this.radioButtonYClassic.AutoSize = true;
            this.radioButtonYClassic.Location = new System.Drawing.Point(13, 39);
            this.radioButtonYClassic.Name = "radioButtonYClassic";
            this.radioButtonYClassic.Size = new System.Drawing.Size(150, 17);
            this.radioButtonYClassic.TabIndex = 2;
            this.radioButtonYClassic.TabStop = true;
            this.radioButtonYClassic.Tag = "-Yclassic";
            this.radioButtonYClassic.Text = "FSK pulse detector classic";
            this.radioButtonYClassic.UseVisualStyleBackColor = true;
            this.radioButtonYClassic.CheckedChanged += new System.EventHandler(this.radioButtonYFSK_CheckedChanged);
            // 
            // radioButtonYAuto
            // 
            this.radioButtonYAuto.AutoSize = true;
            this.radioButtonYAuto.Location = new System.Drawing.Point(13, 19);
            this.radioButtonYAuto.Name = "radioButtonYAuto";
            this.radioButtonYAuto.Size = new System.Drawing.Size(139, 17);
            this.radioButtonYAuto.TabIndex = 2;
            this.radioButtonYAuto.TabStop = true;
            this.radioButtonYAuto.Tag = "-Yauto";
            this.radioButtonYAuto.Text = "FSK pulse detector auto";
            this.radioButtonYAuto.UseVisualStyleBackColor = true;
            this.radioButtonYAuto.CheckedChanged += new System.EventHandler(this.radioButtonYFSK_CheckedChanged);
            // 
            // numericUpDownMinimumSNRPulses
            // 
            this.numericUpDownMinimumSNRPulses.Location = new System.Drawing.Point(148, 116);
            this.numericUpDownMinimumSNRPulses.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.numericUpDownMinimumSNRPulses.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownMinimumSNRPulses.Name = "numericUpDownMinimumSNRPulses";
            this.numericUpDownMinimumSNRPulses.Size = new System.Drawing.Size(64, 20);
            this.numericUpDownMinimumSNRPulses.TabIndex = 1;
            this.numericUpDownMinimumSNRPulses.Tag = "-Yminsnr";
            this.numericUpDownMinimumSNRPulses.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.numericUpDownMinimumSNRPulses.ValueChanged += new System.EventHandler(this.numericUpDownFSK_ValueChanged);
            // 
            // numericUpDownMinimumDetectionLevel
            // 
            this.numericUpDownMinimumDetectionLevel.Location = new System.Drawing.Point(148, 96);
            this.numericUpDownMinimumDetectionLevel.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericUpDownMinimumDetectionLevel.Minimum = new decimal(new int[] {
            99,
            0,
            0,
            -2147483648});
            this.numericUpDownMinimumDetectionLevel.Name = "numericUpDownMinimumDetectionLevel";
            this.numericUpDownMinimumDetectionLevel.Size = new System.Drawing.Size(64, 20);
            this.numericUpDownMinimumDetectionLevel.TabIndex = 1;
            this.numericUpDownMinimumDetectionLevel.Tag = "-Yminlevel";
            this.numericUpDownMinimumDetectionLevel.Value = new decimal(new int[] {
            12,
            0,
            0,
            -2147483648});
            this.numericUpDownMinimumDetectionLevel.ValueChanged += new System.EventHandler(this.numericUpDownFSK_ValueChanged);
            // 
            // numericUpDownPulseDetectionLevel
            // 
            this.numericUpDownPulseDetectionLevel.Location = new System.Drawing.Point(148, 76);
            this.numericUpDownPulseDetectionLevel.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericUpDownPulseDetectionLevel.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            -2147483648});
            this.numericUpDownPulseDetectionLevel.Name = "numericUpDownPulseDetectionLevel";
            this.numericUpDownPulseDetectionLevel.Size = new System.Drawing.Size(64, 20);
            this.numericUpDownPulseDetectionLevel.TabIndex = 1;
            this.numericUpDownPulseDetectionLevel.Tag = "-Ylevel";
            this.numericUpDownPulseDetectionLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericUpDownPulseDetectionLevel.ValueChanged += new System.EventHandler(this.numericUpDownFSK_ValueChanged);
            // 
            // checkBoxYAmpest
            // 
            this.checkBoxYAmpest.AutoSize = true;
            this.checkBoxYAmpest.Location = new System.Drawing.Point(13, 179);
            this.checkBoxYAmpest.Name = "checkBoxYAmpest";
            this.checkBoxYAmpest.Size = new System.Drawing.Size(61, 17);
            this.checkBoxYAmpest.TabIndex = 0;
            this.checkBoxYAmpest.Tag = "ampest or magest";
            this.checkBoxYAmpest.Text = "Ampest";
            this.checkBoxYAmpest.UseVisualStyleBackColor = true;
            this.checkBoxYAmpest.CheckedChanged += new System.EventHandler(this.checkBoxY_CheckedChanged);
            // 
            // checkBoxYSquelch
            // 
            this.checkBoxYSquelch.AutoSize = true;
            this.checkBoxYSquelch.Location = new System.Drawing.Point(13, 159);
            this.checkBoxYSquelch.Name = "checkBoxYSquelch";
            this.checkBoxYSquelch.Size = new System.Drawing.Size(65, 17);
            this.checkBoxYSquelch.TabIndex = 0;
            this.checkBoxYSquelch.Tag = "-Ysquelch";
            this.checkBoxYSquelch.Text = "Squelch";
            this.checkBoxYSquelch.UseVisualStyleBackColor = true;
            this.checkBoxYSquelch.CheckedChanged += new System.EventHandler(this.checkBoxY_CheckedChanged);
            // 
            // checkBoxYAutoLevel
            // 
            this.checkBoxYAutoLevel.AutoSize = true;
            this.checkBoxYAutoLevel.Location = new System.Drawing.Point(13, 139);
            this.checkBoxYAutoLevel.Name = "checkBoxYAutoLevel";
            this.checkBoxYAutoLevel.Size = new System.Drawing.Size(73, 17);
            this.checkBoxYAutoLevel.TabIndex = 0;
            this.checkBoxYAutoLevel.Tag = "-Yautolevel";
            this.checkBoxYAutoLevel.Text = "Auto level";
            this.checkBoxYAutoLevel.UseVisualStyleBackColor = true;
            this.checkBoxYAutoLevel.CheckedChanged += new System.EventHandler(this.checkBoxY_CheckedChanged);
            // 
            // checkBoxYMinimumSNRPulses
            // 
            this.checkBoxYMinimumSNRPulses.AutoSize = true;
            this.checkBoxYMinimumSNRPulses.Location = new System.Drawing.Point(13, 119);
            this.checkBoxYMinimumSNRPulses.Name = "checkBoxYMinimumSNRPulses";
            this.checkBoxYMinimumSNRPulses.Size = new System.Drawing.Size(129, 17);
            this.checkBoxYMinimumSNRPulses.TabIndex = 0;
            this.checkBoxYMinimumSNRPulses.Tag = "-Yminsnr";
            this.checkBoxYMinimumSNRPulses.Text = "Minimum SNR  pulses";
            this.checkBoxYMinimumSNRPulses.UseVisualStyleBackColor = true;
            this.checkBoxYMinimumSNRPulses.CheckedChanged += new System.EventHandler(this.checkBoxY_CheckedChanged);
            // 
            // checkBoxYMinimumDetectionLevelPulses
            // 
            this.checkBoxYMinimumDetectionLevelPulses.AutoSize = true;
            this.checkBoxYMinimumDetectionLevelPulses.Location = new System.Drawing.Point(13, 99);
            this.checkBoxYMinimumDetectionLevelPulses.Name = "checkBoxYMinimumDetectionLevelPulses";
            this.checkBoxYMinimumDetectionLevelPulses.Size = new System.Drawing.Size(134, 17);
            this.checkBoxYMinimumDetectionLevelPulses.TabIndex = 0;
            this.checkBoxYMinimumDetectionLevelPulses.Tag = "-Yminlevel";
            this.checkBoxYMinimumDetectionLevelPulses.Text = "Min detect level pulses";
            this.checkBoxYMinimumDetectionLevelPulses.UseVisualStyleBackColor = true;
            this.checkBoxYMinimumDetectionLevelPulses.CheckedChanged += new System.EventHandler(this.checkBoxY_CheckedChanged);
            // 
            // checkBoxYPulsesDetectionLevel
            // 
            this.checkBoxYPulsesDetectionLevel.AutoSize = true;
            this.checkBoxYPulsesDetectionLevel.Location = new System.Drawing.Point(13, 79);
            this.checkBoxYPulsesDetectionLevel.Name = "checkBoxYPulsesDetectionLevel";
            this.checkBoxYPulsesDetectionLevel.Size = new System.Drawing.Size(129, 17);
            this.checkBoxYPulsesDetectionLevel.TabIndex = 0;
            this.checkBoxYPulsesDetectionLevel.Tag = "-Ylevel";
            this.checkBoxYPulsesDetectionLevel.Text = "Pulses detection level";
            this.checkBoxYPulsesDetectionLevel.UseVisualStyleBackColor = true;
            this.checkBoxYPulsesDetectionLevel.CheckedChanged += new System.EventHandler(this.checkBoxY_CheckedChanged);
            // 
            // checkBoxEnabledPlugin
            // 
            this.checkBoxEnabledPlugin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxEnabledPlugin.Location = new System.Drawing.Point(3, 3);
            this.checkBoxEnabledPlugin.Name = "checkBoxEnabledPlugin";
            this.checkBoxEnabledPlugin.Size = new System.Drawing.Size(144, 24);
            this.checkBoxEnabledPlugin.TabIndex = 28;
            this.checkBoxEnabledPlugin.Text = "Enabled plugin";
            this.checkBoxEnabledPlugin.UseVisualStyleBackColor = true;
            this.checkBoxEnabledPlugin.CheckedChanged += new System.EventHandler(this.checkBoxEnabledPlugin_CheckedChanged);
            // 
            // groupBoxInfos
            // 
            this.groupBoxInfos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTableLayoutPanel.SetColumnSpan(this.groupBoxInfos, 2);
            this.groupBoxInfos.Controls.Add(this.labelSampleRateTxt);
            this.groupBoxInfos.Controls.Add(this.labelTimeCycle);
            this.groupBoxInfos.Controls.Add(this.labelFrequencyTxt);
            this.groupBoxInfos.Controls.Add(this.labelVersionTxt);
            this.groupBoxInfos.Controls.Add(this.labelSampleRate);
            this.groupBoxInfos.Controls.Add(this.labelFrequency);
            this.groupBoxInfos.Controls.Add(this.labelCycleTime);
            this.groupBoxInfos.Controls.Add(this.labelTime433);
            this.groupBoxInfos.Controls.Add(this.labelTimeRtl433);
            this.groupBoxInfos.Controls.Add(this.labelVersion);
            this.groupBoxInfos.Location = new System.Drawing.Point(3, 282);
            this.groupBoxInfos.Name = "groupBoxInfos";
            this.groupBoxInfos.Size = new System.Drawing.Size(290, 138);
            this.groupBoxInfos.TabIndex = 31;
            this.groupBoxInfos.TabStop = false;
            // 
            // labelSampleRateTxt
            // 
            this.labelSampleRateTxt.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelSampleRateTxt.AutoSize = true;
            this.labelSampleRateTxt.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelSampleRateTxt.Location = new System.Drawing.Point(11, 36);
            this.labelSampleRateTxt.Name = "labelSampleRateTxt";
            this.labelSampleRateTxt.Size = new System.Drawing.Size(68, 13);
            this.labelSampleRateTxt.TabIndex = 6;
            this.labelSampleRateTxt.Text = "Sample Rate";
            // 
            // labelTimeCycle
            // 
            this.labelTimeCycle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelTimeCycle.AutoSize = true;
            this.labelTimeCycle.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelTimeCycle.Location = new System.Drawing.Point(113, 61);
            this.labelTimeCycle.Name = "labelTimeCycle";
            this.labelTimeCycle.Size = new System.Drawing.Size(13, 13);
            this.labelTimeCycle.TabIndex = 15;
            this.labelTimeCycle.Text = "0";
            this.labelTimeCycle.Visible = false;
            // 
            // labelFrequencyTxt
            // 
            this.labelFrequencyTxt.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelFrequencyTxt.AutoSize = true;
            this.labelFrequencyTxt.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelFrequencyTxt.Location = new System.Drawing.Point(13, 13);
            this.labelFrequencyTxt.Name = "labelFrequencyTxt";
            this.labelFrequencyTxt.Size = new System.Drawing.Size(57, 13);
            this.labelFrequencyTxt.TabIndex = 6;
            this.labelFrequencyTxt.Text = "Frequency";
            // 
            // labelVersionTxt
            // 
            this.labelVersionTxt.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelVersionTxt.AutoSize = true;
            this.labelVersionTxt.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelVersionTxt.Location = new System.Drawing.Point(13, 111);
            this.labelVersionTxt.Name = "labelVersionTxt";
            this.labelVersionTxt.Size = new System.Drawing.Size(73, 13);
            this.labelVersionTxt.TabIndex = 4;
            this.labelVersionTxt.Text = "Version plugin";
            // 
            // labelSampleRate
            // 
            this.labelSampleRate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelSampleRate.AutoSize = true;
            this.labelSampleRate.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelSampleRate.Location = new System.Drawing.Point(113, 36);
            this.labelSampleRate.Name = "labelSampleRate";
            this.labelSampleRate.Size = new System.Drawing.Size(13, 13);
            this.labelSampleRate.TabIndex = 6;
            this.labelSampleRate.Text = "0";
            // 
            // labelFrequency
            // 
            this.labelFrequency.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelFrequency.AutoSize = true;
            this.labelFrequency.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelFrequency.Location = new System.Drawing.Point(113, 13);
            this.labelFrequency.Name = "labelFrequency";
            this.labelFrequency.Size = new System.Drawing.Size(13, 13);
            this.labelFrequency.TabIndex = 6;
            this.labelFrequency.Text = "0";
            // 
            // labelCycleTime
            // 
            this.labelCycleTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelCycleTime.AutoSize = true;
            this.labelCycleTime.Location = new System.Drawing.Point(11, 61);
            this.labelCycleTime.Name = "labelCycleTime";
            this.labelCycleTime.Size = new System.Drawing.Size(68, 13);
            this.labelCycleTime.TabIndex = 29;
            this.labelCycleTime.Text = "Cycle time/s:";
            this.labelCycleTime.Visible = false;
            // 
            // labelTime433
            // 
            this.labelTime433.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelTime433.AutoSize = true;
            this.labelTime433.Location = new System.Drawing.Point(11, 86);
            this.labelTime433.Name = "labelTime433";
            this.labelTime433.Size = new System.Drawing.Size(85, 13);
            this.labelTime433.TabIndex = 30;
            this.labelTime433.Text = "Time RTL433/s:";
            this.labelTime433.Visible = false;
            // 
            // labelTimeRtl433
            // 
            this.labelTimeRtl433.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelTimeRtl433.AutoSize = true;
            this.labelTimeRtl433.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelTimeRtl433.Location = new System.Drawing.Point(113, 86);
            this.labelTimeRtl433.Name = "labelTimeRtl433";
            this.labelTimeRtl433.Size = new System.Drawing.Size(13, 13);
            this.labelTimeRtl433.TabIndex = 16;
            this.labelTimeRtl433.Text = "0";
            this.labelTimeRtl433.Visible = false;
            // 
            // labelVersion
            // 
            this.labelVersion.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelVersion.AutoSize = true;
            this.labelVersion.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelVersion.Location = new System.Drawing.Point(113, 111);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(13, 13);
            this.labelVersion.TabIndex = 4;
            this.labelVersion.Text = "0";
            // 
            // buttonStartStop
            // 
            this.buttonStartStop.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonStartStop.BackColor = System.Drawing.SystemColors.Desktop;
            this.buttonStartStop.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonStartStop.Location = new System.Drawing.Point(161, 3);
            this.buttonStartStop.Name = "buttonStartStop";
            this.buttonStartStop.Size = new System.Drawing.Size(124, 24);
            this.buttonStartStop.TabIndex = 19;
            this.buttonStartStop.Text = "Wait Radio";
            this.buttonStartStop.UseVisualStyleBackColor = false;
            this.buttonStartStop.Click += new System.EventHandler(this.buttonStartStop_Click);
            this.buttonStartStop.Paint += new System.Windows.Forms.PaintEventHandler(this.button_Paint);
            // 
            // buttonCu8ToWav
            // 
            this.buttonCu8ToWav.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonCu8ToWav.BackColor = System.Drawing.SystemColors.Desktop;
            this.buttonCu8ToWav.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonCu8ToWav.Location = new System.Drawing.Point(23, 221);
            this.buttonCu8ToWav.Name = "buttonCu8ToWav";
            this.buttonCu8ToWav.Size = new System.Drawing.Size(104, 28);
            this.buttonCu8ToWav.TabIndex = 14;
            this.buttonCu8ToWav.Text = ".cu8 to .wav";
            this.buttonCu8ToWav.UseVisualStyleBackColor = false;
            this.buttonCu8ToWav.Click += new System.EventHandler(this.buttonCu8ToWav_Click);
            // 
            // groupBoxRecordTxtFile
            // 
            this.groupBoxRecordTxtFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxRecordTxtFile.Controls.Add(this.labelWarningRecordTextFile);
            this.groupBoxRecordTxtFile.Controls.Add(this.checkBoxRecordTxtFile);
            this.groupBoxRecordTxtFile.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.groupBoxRecordTxtFile.Location = new System.Drawing.Point(3, 73);
            this.groupBoxRecordTxtFile.Name = "groupBoxRecordTxtFile";
            this.groupBoxRecordTxtFile.Size = new System.Drawing.Size(144, 116);
            this.groupBoxRecordTxtFile.TabIndex = 32;
            this.groupBoxRecordTxtFile.TabStop = false;
            this.groupBoxRecordTxtFile.Text = "Record  text file";
            // 
            // checkBoxRecordTxtFile
            // 
            this.checkBoxRecordTxtFile.AutoSize = true;
            this.checkBoxRecordTxtFile.Location = new System.Drawing.Point(3, 19);
            this.checkBoxRecordTxtFile.Name = "checkBoxRecordTxtFile";
            this.checkBoxRecordTxtFile.Size = new System.Drawing.Size(61, 17);
            this.checkBoxRecordTxtFile.TabIndex = 0;
            this.checkBoxRecordTxtFile.Text = "Record";
            this.checkBoxRecordTxtFile.UseVisualStyleBackColor = true;
            // 
            // labelWarningRecordTextFile
            // 
            this.labelWarningRecordTextFile.AutoSize = true;
            this.labelWarningRecordTextFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelWarningRecordTextFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWarningRecordTextFile.ForeColor = System.Drawing.Color.DarkOrange;
            this.labelWarningRecordTextFile.Location = new System.Drawing.Point(20, 39);
            this.labelWarningRecordTextFile.Name = "labelWarningRecordTextFile";
            this.labelWarningRecordTextFile.Size = new System.Drawing.Size(104, 62);
            this.labelWarningRecordTextFile.TabIndex = 1;
            this.labelWarningRecordTextFile.Text = "Warning to space\r\ndisk if checked.\r\nOnly if Windows\r\nList messages";
            // 
            // Rtl_433_Panel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = true;
            this.Controls.Add(this.mainTableLayoutPanel);
            this.Name = "Rtl_433_Panel";
            this.Size = new System.Drawing.Size(296, 1443);
            this.mainTableLayoutPanel.ResumeLayout(false);
            this.mainTableLayoutPanel.PerformLayout();
            this.groupBoxDataConv.ResumeLayout(false);
            this.groupBoxMetadata.ResumeLayout(false);
            this.groupBoxRecord.ResumeLayout(false);
            this.groupBoxFrequency.ResumeLayout(false);
            this.groupBoxVerbose.ResumeLayout(false);
            this.groupBoxSave.ResumeLayout(false);
            this.groupBoxR.ResumeLayout(false);
            this.groupBoxHideShow.ResumeLayout(false);
            this.groupBoxSelectTypeForm.ResumeLayout(false);
            this.groupBoxEnabledDisabledDevices.ResumeLayout(false);
            this.groupBoxOptionY.ResumeLayout(false);
            this.groupBoxOptionY.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinimumSNRPulses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinimumDetectionLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPulseDetectionLevel)).EndInit();
            this.groupBoxInfos.ResumeLayout(false);
            this.groupBoxInfos.PerformLayout();
            this.groupBoxRecordTxtFile.ResumeLayout(false);
            this.groupBoxRecordTxtFile.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.TableLayoutPanel mainTableLayoutPanel;
        private System.Windows.Forms.Label labelVersionTxt;
        private System.Windows.Forms.Label labelSampleRateTxt;
        private System.Windows.Forms.Button buttonClearMessages;
        private System.Windows.Forms.Button buttonDisplayParam;
        private System.Windows.Forms.GroupBox groupBoxVerbose;
        private System.Windows.Forms.RadioButton radioButtonNoV;
        private System.Windows.Forms.RadioButton radioButtonV;
        private System.Windows.Forms.RadioButton radioButtonVVVV;
        private System.Windows.Forms.Label labelFrequencyTxt;
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
        private System.Windows.Forms.ListBox listBoxHideShowDevices;
        private System.Windows.Forms.Label labelTimeCycle;
        private System.Windows.Forms.Label labelTimeRtl433;
        private System.Windows.Forms.Button buttonStartStop;
        private System.Windows.Forms.GroupBox groupBoxDataConv;
        private System.Windows.Forms.RadioButton radioButtonDataConvCustomary;
        private System.Windows.Forms.RadioButton radioButtonDataConvSI;
        private System.Windows.Forms.RadioButton radioButtonDataConvNative;
        private System.Windows.Forms.Label labelHideDevices;
        private System.Windows.Forms.GroupBox groupBoxHideShow;
        private System.Windows.Forms.RadioButton radioButtonHideSelect;
        private System.Windows.Forms.RadioButton radioButtonShowSelect;
        private System.Windows.Forms.GroupBox groupBoxSelectTypeForm;
        private System.Windows.Forms.RadioButton radioButtonListDevices;
        private System.Windows.Forms.RadioButton radioButtonGraph;
        private System.Windows.Forms.RadioButton radioButtonListMessages;
        private System.Windows.Forms.CheckBox checkBoxEnabledDevicesDisabled;
        private System.Windows.Forms.GroupBox groupBoxEnabledDisabledDevices;
        private System.Windows.Forms.GroupBox groupBoxOptionY;
        private System.Windows.Forms.NumericUpDown numericUpDownPulseDetectionLevel;
        private System.Windows.Forms.CheckBox checkBoxYAmpest;
        private System.Windows.Forms.CheckBox checkBoxYSquelch;
        private System.Windows.Forms.CheckBox checkBoxYAutoLevel;
        private System.Windows.Forms.CheckBox checkBoxYMinimumSNRPulses;
        private System.Windows.Forms.CheckBox checkBoxYMinimumDetectionLevelPulses;
        private System.Windows.Forms.CheckBox checkBoxYPulsesDetectionLevel;
        private System.Windows.Forms.NumericUpDown numericUpDownMinimumSNRPulses;
        private System.Windows.Forms.NumericUpDown numericUpDownMinimumDetectionLevel;
        private System.Windows.Forms.RadioButton radioButtonYMinmax;
        private System.Windows.Forms.RadioButton radioButtonYClassic;
        private System.Windows.Forms.RadioButton radioButtonYAuto;
        private System.Windows.Forms.CheckBox checkBoxEnabledPlugin;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelSampleRate;
        private System.Windows.Forms.Label labelFrequency;
        private System.Windows.Forms.Label labelCycleTime;
        private System.Windows.Forms.Label labelTime433;
        private System.Windows.Forms.GroupBox groupBoxInfos;
        private System.Windows.Forms.CheckBox checkBoxRaw;
        private System.Windows.Forms.GroupBox groupBoxRecordTxtFile;
        private System.Windows.Forms.Label labelWarningRecordTextFile;
        private System.Windows.Forms.CheckBox checkBoxRecordTxtFile;
    }
}