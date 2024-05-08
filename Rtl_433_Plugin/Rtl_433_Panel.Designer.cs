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
            this.listViewConsole = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBoxDataConv = new System.Windows.Forms.GroupBox();
            this.radioButtonDataConvCustomary = new System.Windows.Forms.RadioButton();
            this.radioButtonDataConvSI = new System.Windows.Forms.RadioButton();
            this.radioButtonDataConvNative = new System.Windows.Forms.RadioButton();
            this.groupBoxMetadata = new System.Windows.Forms.GroupBox();
            this.radioButtonMLevel = new System.Windows.Forms.RadioButton();
            this.radioButtonNoM = new System.Windows.Forms.RadioButton();
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
            this.radioButtonVVVV = new System.Windows.Forms.RadioButton();
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
            this.groupBoxInfos = new System.Windows.Forms.GroupBox();
            this.labelSampleRateTxt = new System.Windows.Forms.Label();
            this.labelTimeCycle = new System.Windows.Forms.Label();
            this.labelFrequencyTxt = new System.Windows.Forms.Label();
            this.labelSampleRate = new System.Windows.Forms.Label();
            this.labelFrequency = new System.Windows.Forms.Label();
            this.labelCycleTime = new System.Windows.Forms.Label();
            this.labelTime433 = new System.Windows.Forms.Label();
            this.labelTimeRtl433 = new System.Windows.Forms.Label();
            this.groupBoxRecordTxtFile = new System.Windows.Forms.GroupBox();
            this.labelWarningRecordTextFile = new System.Windows.Forms.Label();
            this.checkBoxRecordTxtFile = new System.Windows.Forms.CheckBox();
            this.groupBoxEnabledPlugin = new System.Windows.Forms.GroupBox();
            this.checkBoxEnabledPlugin = new System.Windows.Forms.CheckBox();
            this.buttonStartStop = new System.Windows.Forms.Button();
            this.groupBoxConsole = new System.Windows.Forms.GroupBox();
            this.buttonSelectToClipboard = new System.Windows.Forms.Button();
            this.buttonAllToClipboard = new System.Windows.Forms.Button();
            this.buttonDisplayParam = new System.Windows.Forms.Button();
            this.buttonClearMessages = new System.Windows.Forms.Button();
            this.buttonCu8ToWav = new System.Windows.Forms.Button();
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
            this.groupBoxInfos.SuspendLayout();
            this.groupBoxRecordTxtFile.SuspendLayout();
            this.groupBoxEnabledPlugin.SuspendLayout();
            this.groupBoxConsole.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTableLayoutPanel
            // 
            this.mainTableLayoutPanel.AutoScroll = true;
            this.mainTableLayoutPanel.AutoSize = true;
            this.mainTableLayoutPanel.CausesValidation = false;
            this.mainTableLayoutPanel.ColumnCount = 2;
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.mainTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.mainTableLayoutPanel.Controls.Add(this.listViewConsole, 0, 12);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxDataConv, 0, 6);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxMetadata, 1, 5);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxRecord, 1, 3);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxFrequency, 1, 2);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxVerbose, 0, 5);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxSave, 1, 6);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxR, 0, 9);
            this.mainTableLayoutPanel.Controls.Add(this.labelHideDevices, 0, 7);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxHideShow, 0, 8);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxSelectTypeForm, 0, 1);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxEnabledDisabledDevices, 0, 10);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxInfos, 0, 4);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxRecordTxtFile, 0, 2);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxEnabledPlugin, 0, 0);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxConsole, 0, 11);
            this.mainTableLayoutPanel.Controls.Add(this.buttonCu8ToWav, 0, 3);
            this.mainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            this.mainTableLayoutPanel.RowCount = 13;
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.Size = new System.Drawing.Size(296, 1276);
            this.mainTableLayoutPanel.TabIndex = 1;
            this.mainTableLayoutPanel.SizeChanged += new System.EventHandler(this.mainTableLayoutPanel_SizeChanged);
            // 
            // listViewConsole
            // 
            this.listViewConsole.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listViewConsole.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.mainTableLayoutPanel.SetColumnSpan(this.listViewConsole, 2);
            this.listViewConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewConsole.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewConsole.HideSelection = false;
            this.listViewConsole.Location = new System.Drawing.Point(3, 950);
            this.listViewConsole.Name = "listViewConsole";
            this.listViewConsole.Size = new System.Drawing.Size(290, 323);
            this.listViewConsole.TabIndex = 33;
            this.listViewConsole.UseCompatibleStateImageBehavior = false;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "RTL_433 messages ";
            // 
            // groupBoxDataConv
            // 
            this.groupBoxDataConv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxDataConv.Controls.Add(this.radioButtonDataConvCustomary);
            this.groupBoxDataConv.Controls.Add(this.radioButtonDataConvSI);
            this.groupBoxDataConv.Controls.Add(this.radioButtonDataConvNative);
            this.groupBoxDataConv.Location = new System.Drawing.Point(3, 533);
            this.groupBoxDataConv.Name = "groupBoxDataConv";
            this.groupBoxDataConv.Size = new System.Drawing.Size(144, 113);
            this.groupBoxDataConv.TabIndex = 20;
            this.groupBoxDataConv.TabStop = false;
            this.groupBoxDataConv.Text = "Data Conv(-C)";
            // 
            // radioButtonDataConvCustomary
            // 
            this.radioButtonDataConvCustomary.Location = new System.Drawing.Point(9, 77);
            this.radioButtonDataConvCustomary.Name = "radioButtonDataConvCustomary";
            this.radioButtonDataConvCustomary.Size = new System.Drawing.Size(111, 17);
            this.radioButtonDataConvCustomary.TabIndex = 11;
            this.radioButtonDataConvCustomary.Tag = "dataconv";
            this.radioButtonDataConvCustomary.Text = "-Ccustomary";
            this.radioButtonDataConvCustomary.UseVisualStyleBackColor = true;
            // 
            // radioButtonDataConvSI
            // 
            this.radioButtonDataConvSI.Location = new System.Drawing.Point(9, 54);
            this.radioButtonDataConvSI.Name = "radioButtonDataConvSI";
            this.radioButtonDataConvSI.Size = new System.Drawing.Size(42, 17);
            this.radioButtonDataConvSI.TabIndex = 10;
            this.radioButtonDataConvSI.Tag = "dataconv";
            this.radioButtonDataConvSI.Text = "-Csi";
            this.radioButtonDataConvSI.UseVisualStyleBackColor = true;
            // 
            // radioButtonDataConvNative
            // 
            this.radioButtonDataConvNative.Checked = true;
            this.radioButtonDataConvNative.Location = new System.Drawing.Point(9, 31);
            this.radioButtonDataConvNative.Name = "radioButtonDataConvNative";
            this.radioButtonDataConvNative.Size = new System.Drawing.Size(64, 17);
            this.radioButtonDataConvNative.TabIndex = 9;
            this.radioButtonDataConvNative.TabStop = true;
            this.radioButtonDataConvNative.Tag = "dataconv";
            this.radioButtonDataConvNative.Text = "-Cnative";
            this.radioButtonDataConvNative.UseVisualStyleBackColor = true;
            // 
            // groupBoxMetadata
            // 
            this.groupBoxMetadata.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxMetadata.Controls.Add(this.radioButtonMLevel);
            this.groupBoxMetadata.Controls.Add(this.radioButtonNoM);
            this.groupBoxMetadata.Location = new System.Drawing.Point(153, 392);
            this.groupBoxMetadata.Name = "groupBoxMetadata";
            this.groupBoxMetadata.Size = new System.Drawing.Size(140, 135);
            this.groupBoxMetadata.TabIndex = 10;
            this.groupBoxMetadata.TabStop = false;
            this.groupBoxMetadata.Text = "metaData(-M)";
            // 
            // radioButtonMLevel
            // 
            this.radioButtonMLevel.Location = new System.Drawing.Point(11, 42);
            this.radioButtonMLevel.Name = "radioButtonMLevel";
            this.radioButtonMLevel.Size = new System.Drawing.Size(59, 17);
            this.radioButtonMLevel.TabIndex = 8;
            this.radioButtonMLevel.Tag = "MbitsOrLevel";
            this.radioButtonMLevel.Text = "-Mlevel";
            this.radioButtonMLevel.UseVisualStyleBackColor = false;
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
            // 
            // groupBoxRecord
            // 
            this.groupBoxRecord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxRecord.Controls.Add(this.checkBoxMONO);
            this.groupBoxRecord.Controls.Add(this.checkBoxSTEREO);
            this.groupBoxRecord.Location = new System.Drawing.Point(153, 197);
            this.groupBoxRecord.Name = "groupBoxRecord";
            this.groupBoxRecord.Size = new System.Drawing.Size(140, 65);
            this.groupBoxRecord.TabIndex = 12;
            this.groupBoxRecord.TabStop = false;
            this.groupBoxRecord.Text = "Record";
            // 
            // checkBoxMONO
            // 
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
            this.groupBoxFrequency.Controls.Add(this.radioButtonFreq915);
            this.groupBoxFrequency.Controls.Add(this.radioButtonFreq868);
            this.groupBoxFrequency.Controls.Add(this.radioButtonFreq43392);
            this.groupBoxFrequency.Controls.Add(this.radioButtonFreq345);
            this.groupBoxFrequency.Controls.Add(this.radioButtonFreq315);
            this.groupBoxFrequency.Controls.Add(this.radioButtonFreqFree);
            this.groupBoxFrequency.Location = new System.Drawing.Point(153, 88);
            this.groupBoxFrequency.Name = "groupBoxFrequency";
            this.groupBoxFrequency.Size = new System.Drawing.Size(140, 103);
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
            // 
            // radioButtonFreq43392
            // 
            this.radioButtonFreq43392.Checked = true;
            this.radioButtonFreq43392.Location = new System.Drawing.Point(56, 19);
            this.radioButtonFreq43392.Name = "radioButtonFreq43392";
            this.radioButtonFreq43392.Size = new System.Drawing.Size(81, 17);
            this.radioButtonFreq43392.TabIndex = 3;
            this.radioButtonFreq43392.TabStop = true;
            this.radioButtonFreq43392.Tag = "433920000";
            this.radioButtonFreq43392.Text = "433.92 Mhz";
            this.radioButtonFreq43392.UseVisualStyleBackColor = true;
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
            // 
            // radioButtonFreqFree
            // 
            this.radioButtonFreqFree.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.radioButtonFreqFree.Location = new System.Drawing.Point(6, 19);
            this.radioButtonFreqFree.Name = "radioButtonFreqFree";
            this.radioButtonFreqFree.Size = new System.Drawing.Size(46, 17);
            this.radioButtonFreqFree.TabIndex = 0;
            this.radioButtonFreqFree.Tag = "0";
            this.radioButtonFreqFree.Text = "Free";
            this.radioButtonFreqFree.UseVisualStyleBackColor = false;
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
            this.groupBoxVerbose.Location = new System.Drawing.Point(3, 392);
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
            // 
            // radioButtonVVV
            // 
            this.radioButtonVVV.Location = new System.Drawing.Point(9, 88);
            this.radioButtonVVV.Name = "radioButtonVVV";
            this.radioButtonVVV.Size = new System.Drawing.Size(46, 17);
            this.radioButtonVVV.TabIndex = 8;
            this.radioButtonVVV.Tag = "verbose";
            this.radioButtonVVV.Text = "-vvv";
            this.radioButtonVVV.UseVisualStyleBackColor = true;
            // 
            // radioButtonVV
            // 
            this.radioButtonVV.Location = new System.Drawing.Point(9, 65);
            this.radioButtonVV.Name = "radioButtonVV";
            this.radioButtonVV.Size = new System.Drawing.Size(40, 17);
            this.radioButtonVV.TabIndex = 8;
            this.radioButtonVV.Tag = "verbose";
            this.radioButtonVV.Text = "-vv";
            this.radioButtonVV.UseVisualStyleBackColor = true;
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
            // 
            // radioButtonVVVV
            // 
            this.radioButtonVVVV.Location = new System.Drawing.Point(9, 111);
            this.radioButtonVVVV.Name = "radioButtonVVVV";
            this.radioButtonVVVV.Size = new System.Drawing.Size(52, 17);
            this.radioButtonVVVV.TabIndex = 8;
            this.radioButtonVVVV.Tag = "verbose";
            this.radioButtonVVVV.Text = "-vvvv";
            this.radioButtonVVVV.UseVisualStyleBackColor = true;
            // 
            // groupBoxSave
            // 
            this.groupBoxSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxSave.Controls.Add(this.radioButtonSnone);
            this.groupBoxSave.Controls.Add(this.radioButtonSunknown);
            this.groupBoxSave.Controls.Add(this.radioButtonSknown);
            this.groupBoxSave.Controls.Add(this.radioButtonSall);
            this.groupBoxSave.Location = new System.Drawing.Point(153, 533);
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
            // 
            // radioButtonSunknown
            // 
            this.radioButtonSunknown.Location = new System.Drawing.Point(9, 88);
            this.radioButtonSunknown.Name = "radioButtonSunknown";
            this.radioButtonSunknown.Size = new System.Drawing.Size(100, 17);
            this.radioButtonSunknown.TabIndex = 8;
            this.radioButtonSunknown.Tag = "saveDevice";
            this.radioButtonSunknown.Text = "-Sunknown";
            this.radioButtonSunknown.UseVisualStyleBackColor = false;
            // 
            // radioButtonSknown
            // 
            this.radioButtonSknown.Location = new System.Drawing.Point(9, 65);
            this.radioButtonSknown.Name = "radioButtonSknown";
            this.radioButtonSknown.Size = new System.Drawing.Size(67, 17);
            this.radioButtonSknown.TabIndex = 8;
            this.radioButtonSknown.Tag = "saveDevice";
            this.radioButtonSknown.Text = "-Sknown";
            this.radioButtonSknown.UseVisualStyleBackColor = false;
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
            // 
            // groupBoxR
            // 
            this.groupBoxR.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxR.AutoSize = true;
            this.mainTableLayoutPanel.SetColumnSpan(this.groupBoxR, 2);
            this.groupBoxR.Controls.Add(this.listBoxHideShowDevices);
            this.groupBoxR.Location = new System.Drawing.Point(3, 708);
            this.groupBoxR.Name = "groupBoxR";
            this.groupBoxR.Size = new System.Drawing.Size(290, 124);
            this.groupBoxR.TabIndex = 9;
            this.groupBoxR.TabStop = false;
            this.groupBoxR.Text = "hide show devices(-R)";
            // 
            // listBoxHideShowDevices
            // 
            this.listBoxHideShowDevices.BackColor = System.Drawing.SystemColors.Control;
            this.listBoxHideShowDevices.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxHideShowDevices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxHideShowDevices.ForeColor = System.Drawing.SystemColors.ControlText;
            this.listBoxHideShowDevices.FormattingEnabled = true;
            this.listBoxHideShowDevices.HorizontalScrollbar = true;
            this.listBoxHideShowDevices.Location = new System.Drawing.Point(3, 16);
            this.listBoxHideShowDevices.Name = "listBoxHideShowDevices";
            this.listBoxHideShowDevices.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBoxHideShowDevices.Size = new System.Drawing.Size(284, 105);
            this.listBoxHideShowDevices.TabIndex = 3;
            // 
            // labelHideDevices
            // 
            this.labelHideDevices.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelHideDevices.AutoSize = true;
            this.mainTableLayoutPanel.SetColumnSpan(this.labelHideDevices, 2);
            this.labelHideDevices.Location = new System.Drawing.Point(3, 649);
            this.labelHideDevices.Name = "labelHideDevices";
            this.labelHideDevices.Size = new System.Drawing.Size(154, 13);
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
            this.groupBoxHideShow.Location = new System.Drawing.Point(3, 665);
            this.groupBoxHideShow.Name = "groupBoxHideShow";
            this.groupBoxHideShow.Size = new System.Drawing.Size(290, 37);
            this.groupBoxHideShow.TabIndex = 21;
            this.groupBoxHideShow.TabStop = false;
            // 
            // radioButtonShowSelect
            // 
            this.radioButtonShowSelect.Location = new System.Drawing.Point(115, 14);
            this.radioButtonShowSelect.Name = "radioButtonShowSelect";
            this.radioButtonShowSelect.Size = new System.Drawing.Size(111, 17);
            this.radioButtonShowSelect.TabIndex = 0;
            this.radioButtonShowSelect.Text = "Show select";
            this.radioButtonShowSelect.UseVisualStyleBackColor = true;
            // 
            // radioButtonHideSelect
            // 
            this.radioButtonHideSelect.Checked = true;
            this.radioButtonHideSelect.Location = new System.Drawing.Point(6, 14);
            this.radioButtonHideSelect.Name = "radioButtonHideSelect";
            this.radioButtonHideSelect.Size = new System.Drawing.Size(103, 17);
            this.radioButtonHideSelect.TabIndex = 0;
            this.radioButtonHideSelect.TabStop = true;
            this.radioButtonHideSelect.Text = "Hide select";
            this.radioButtonHideSelect.UseVisualStyleBackColor = true;
            // 
            // groupBoxSelectTypeForm
            // 
            this.groupBoxSelectTypeForm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.mainTableLayoutPanel.SetColumnSpan(this.groupBoxSelectTypeForm, 2);
            this.groupBoxSelectTypeForm.Controls.Add(this.radioButtonListMessages);
            this.groupBoxSelectTypeForm.Controls.Add(this.radioButtonListDevices);
            this.groupBoxSelectTypeForm.Controls.Add(this.radioButtonGraph);
            this.groupBoxSelectTypeForm.Location = new System.Drawing.Point(3, 48);
            this.groupBoxSelectTypeForm.Name = "groupBoxSelectTypeForm";
            this.groupBoxSelectTypeForm.Size = new System.Drawing.Size(290, 34);
            this.groupBoxSelectTypeForm.TabIndex = 22;
            this.groupBoxSelectTypeForm.TabStop = false;
            // 
            // radioButtonListMessages
            // 
            this.radioButtonListMessages.Checked = true;
            this.radioButtonListMessages.Location = new System.Drawing.Point(4, 11);
            this.radioButtonListMessages.Name = "radioButtonListMessages";
            this.radioButtonListMessages.Size = new System.Drawing.Size(105, 17);
            this.radioButtonListMessages.TabIndex = 1;
            this.radioButtonListMessages.TabStop = true;
            this.radioButtonListMessages.Text = "List messages";
            this.radioButtonListMessages.UseVisualStyleBackColor = true;
            this.radioButtonListMessages.CheckedChanged += new System.EventHandler(this.radioButtonTypeWindow_CheckedChanged);
            // 
            // radioButtonListDevices
            // 
            this.radioButtonListDevices.Location = new System.Drawing.Point(183, 11);
            this.radioButtonListDevices.Name = "radioButtonListDevices";
            this.radioButtonListDevices.Size = new System.Drawing.Size(99, 17);
            this.radioButtonListDevices.TabIndex = 0;
            this.radioButtonListDevices.Text = "List devices";
            this.radioButtonListDevices.UseVisualStyleBackColor = true;
            this.radioButtonListDevices.CheckedChanged += new System.EventHandler(this.radioButtonTypeWindow_CheckedChanged);
            // 
            // radioButtonGraph
            // 
            this.radioButtonGraph.Location = new System.Drawing.Point(115, 11);
            this.radioButtonGraph.Name = "radioButtonGraph";
            this.radioButtonGraph.Size = new System.Drawing.Size(66, 17);
            this.radioButtonGraph.TabIndex = 0;
            this.radioButtonGraph.Text = "Graph";
            this.radioButtonGraph.UseVisualStyleBackColor = true;
            this.radioButtonGraph.CheckedChanged += new System.EventHandler(this.radioButtonTypeWindow_CheckedChanged);
            // 
            // groupBoxEnabledDisabledDevices
            // 
            this.groupBoxEnabledDisabledDevices.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.mainTableLayoutPanel.SetColumnSpan(this.groupBoxEnabledDisabledDevices, 2);
            this.groupBoxEnabledDisabledDevices.Controls.Add(this.checkBoxEnabledDevicesDisabled);
            this.groupBoxEnabledDisabledDevices.Location = new System.Drawing.Point(3, 838);
            this.groupBoxEnabledDisabledDevices.Name = "groupBoxEnabledDisabledDevices";
            this.groupBoxEnabledDisabledDevices.Size = new System.Drawing.Size(290, 41);
            this.groupBoxEnabledDisabledDevices.TabIndex = 26;
            this.groupBoxEnabledDisabledDevices.TabStop = false;
            this.groupBoxEnabledDisabledDevices.Text = "Enabled devices disabled";
            // 
            // checkBoxEnabledDevicesDisabled
            // 
            this.checkBoxEnabledDevicesDisabled.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxEnabledDevicesDisabled.Location = new System.Drawing.Point(9, 19);
            this.checkBoxEnabledDevicesDisabled.Margin = new System.Windows.Forms.Padding(10);
            this.checkBoxEnabledDevicesDisabled.Name = "checkBoxEnabledDevicesDisabled";
            this.checkBoxEnabledDevicesDisabled.Padding = new System.Windows.Forms.Padding(10);
            this.checkBoxEnabledDevicesDisabled.Size = new System.Drawing.Size(217, 16);
            this.checkBoxEnabledDevicesDisabled.TabIndex = 25;
            this.checkBoxEnabledDevicesDisabled.Text = "Default";
            this.checkBoxEnabledDevicesDisabled.UseVisualStyleBackColor = true;
            this.checkBoxEnabledDevicesDisabled.CheckedChanged += new System.EventHandler(this.checkBoxEnabledDevicesDisabled_CheckedChanged);
            // 
            // groupBoxInfos
            // 
            this.groupBoxInfos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.mainTableLayoutPanel.SetColumnSpan(this.groupBoxInfos, 2);
            this.groupBoxInfos.Controls.Add(this.labelSampleRateTxt);
            this.groupBoxInfos.Controls.Add(this.labelTimeCycle);
            this.groupBoxInfos.Controls.Add(this.labelFrequencyTxt);
            this.groupBoxInfos.Controls.Add(this.labelSampleRate);
            this.groupBoxInfos.Controls.Add(this.labelFrequency);
            this.groupBoxInfos.Controls.Add(this.labelCycleTime);
            this.groupBoxInfos.Controls.Add(this.labelTime433);
            this.groupBoxInfos.Controls.Add(this.labelTimeRtl433);
            this.groupBoxInfos.Location = new System.Drawing.Point(3, 268);
            this.groupBoxInfos.Name = "groupBoxInfos";
            this.groupBoxInfos.Size = new System.Drawing.Size(290, 118);
            this.groupBoxInfos.TabIndex = 31;
            this.groupBoxInfos.TabStop = false;
            // 
            // labelSampleRateTxt
            // 
            this.labelSampleRateTxt.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelSampleRateTxt.AutoSize = true;
            this.labelSampleRateTxt.Location = new System.Drawing.Point(11, 40);
            this.labelSampleRateTxt.Name = "labelSampleRateTxt";
            this.labelSampleRateTxt.Size = new System.Drawing.Size(68, 13);
            this.labelSampleRateTxt.TabIndex = 6;
            this.labelSampleRateTxt.Text = "Sample Rate";
            // 
            // labelTimeCycle
            // 
            this.labelTimeCycle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelTimeCycle.AutoSize = true;
            this.labelTimeCycle.Location = new System.Drawing.Point(113, 66);
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
            this.labelFrequencyTxt.Location = new System.Drawing.Point(11, 16);
            this.labelFrequencyTxt.Name = "labelFrequencyTxt";
            this.labelFrequencyTxt.Size = new System.Drawing.Size(57, 13);
            this.labelFrequencyTxt.TabIndex = 6;
            this.labelFrequencyTxt.Text = "Frequency";
            // 
            // labelSampleRate
            // 
            this.labelSampleRate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelSampleRate.AutoSize = true;
            this.labelSampleRate.Location = new System.Drawing.Point(113, 40);
            this.labelSampleRate.Name = "labelSampleRate";
            this.labelSampleRate.Size = new System.Drawing.Size(13, 13);
            this.labelSampleRate.TabIndex = 6;
            this.labelSampleRate.Text = "0";
            // 
            // labelFrequency
            // 
            this.labelFrequency.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelFrequency.AutoSize = true;
            this.labelFrequency.Location = new System.Drawing.Point(113, 16);
            this.labelFrequency.Name = "labelFrequency";
            this.labelFrequency.Size = new System.Drawing.Size(13, 13);
            this.labelFrequency.TabIndex = 6;
            this.labelFrequency.Text = "0";
            // 
            // labelCycleTime
            // 
            this.labelCycleTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelCycleTime.AutoSize = true;
            this.labelCycleTime.Location = new System.Drawing.Point(11, 66);
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
            this.labelTime433.Location = new System.Drawing.Point(11, 89);
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
            this.labelTimeRtl433.Location = new System.Drawing.Point(113, 89);
            this.labelTimeRtl433.Name = "labelTimeRtl433";
            this.labelTimeRtl433.Size = new System.Drawing.Size(13, 13);
            this.labelTimeRtl433.TabIndex = 16;
            this.labelTimeRtl433.Text = "0";
            this.labelTimeRtl433.Visible = false;
            // 
            // groupBoxRecordTxtFile
            // 
            this.groupBoxRecordTxtFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxRecordTxtFile.Controls.Add(this.labelWarningRecordTextFile);
            this.groupBoxRecordTxtFile.Controls.Add(this.checkBoxRecordTxtFile);
            this.groupBoxRecordTxtFile.Location = new System.Drawing.Point(3, 88);
            this.groupBoxRecordTxtFile.Name = "groupBoxRecordTxtFile";
            this.groupBoxRecordTxtFile.Size = new System.Drawing.Size(144, 103);
            this.groupBoxRecordTxtFile.TabIndex = 32;
            this.groupBoxRecordTxtFile.TabStop = false;
            this.groupBoxRecordTxtFile.Text = "Record  text file";
            // 
            // labelWarningRecordTextFile
            // 
            this.labelWarningRecordTextFile.AutoSize = true;
            this.labelWarningRecordTextFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelWarningRecordTextFile.Location = new System.Drawing.Point(20, 39);
            this.labelWarningRecordTextFile.Name = "labelWarningRecordTextFile";
            this.labelWarningRecordTextFile.Size = new System.Drawing.Size(93, 54);
            this.labelWarningRecordTextFile.TabIndex = 1;
            this.labelWarningRecordTextFile.Text = "Warning to space\r\ndisk if checked.\r\nOnly if Windows\r\nList messages";
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
            // groupBoxEnabledPlugin
            // 
            this.groupBoxEnabledPlugin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.mainTableLayoutPanel.SetColumnSpan(this.groupBoxEnabledPlugin, 2);
            this.groupBoxEnabledPlugin.Controls.Add(this.checkBoxEnabledPlugin);
            this.groupBoxEnabledPlugin.Controls.Add(this.buttonStartStop);
            this.groupBoxEnabledPlugin.Location = new System.Drawing.Point(3, 3);
            this.groupBoxEnabledPlugin.Name = "groupBoxEnabledPlugin";
            this.groupBoxEnabledPlugin.Size = new System.Drawing.Size(290, 39);
            this.groupBoxEnabledPlugin.TabIndex = 34;
            this.groupBoxEnabledPlugin.TabStop = false;
            // 
            // checkBoxEnabledPlugin
            // 
            this.checkBoxEnabledPlugin.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkBoxEnabledPlugin.AutoSize = true;
            this.checkBoxEnabledPlugin.Location = new System.Drawing.Point(3, 16);
            this.checkBoxEnabledPlugin.Name = "checkBoxEnabledPlugin";
            this.checkBoxEnabledPlugin.Size = new System.Drawing.Size(96, 17);
            this.checkBoxEnabledPlugin.TabIndex = 28;
            this.checkBoxEnabledPlugin.Text = "Enabled plugin";
            this.checkBoxEnabledPlugin.UseVisualStyleBackColor = true;
            this.checkBoxEnabledPlugin.CheckedChanged += new System.EventHandler(this.checkBoxEnabledPlugin_CheckedChanged);
            // 
            // buttonStartStop
            // 
            this.buttonStartStop.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonStartStop.Location = new System.Drawing.Point(160, 14);
            this.buttonStartStop.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.buttonStartStop.Name = "buttonStartStop";
            this.buttonStartStop.Size = new System.Drawing.Size(124, 22);
            this.buttonStartStop.TabIndex = 19;
            this.buttonStartStop.Text = "Wait Radio";
            this.buttonStartStop.UseVisualStyleBackColor = false;
            this.buttonStartStop.Click += new System.EventHandler(this.buttonStartStop_Click);
            // 
            // groupBoxConsole
            // 
            this.groupBoxConsole.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.mainTableLayoutPanel.SetColumnSpan(this.groupBoxConsole, 4);
            this.groupBoxConsole.Controls.Add(this.buttonSelectToClipboard);
            this.groupBoxConsole.Controls.Add(this.buttonAllToClipboard);
            this.groupBoxConsole.Controls.Add(this.buttonDisplayParam);
            this.groupBoxConsole.Controls.Add(this.buttonClearMessages);
            this.groupBoxConsole.Location = new System.Drawing.Point(3, 885);
            this.groupBoxConsole.Name = "groupBoxConsole";
            this.groupBoxConsole.Size = new System.Drawing.Size(290, 59);
            this.groupBoxConsole.TabIndex = 35;
            this.groupBoxConsole.TabStop = false;
            // 
            // buttonSelectToClipboard
            // 
            this.buttonSelectToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonSelectToClipboard.AutoSize = true;
            this.buttonSelectToClipboard.Location = new System.Drawing.Point(215, 13);
            this.buttonSelectToClipboard.Name = "buttonSelectToClipboard";
            this.buttonSelectToClipboard.Size = new System.Drawing.Size(61, 40);
            this.buttonSelectToClipboard.TabIndex = 9;
            this.buttonSelectToClipboard.Text = "Select to\r\nclipboard";
            this.buttonSelectToClipboard.UseVisualStyleBackColor = false;
            this.buttonSelectToClipboard.Click += new System.EventHandler(this.buttonSelectToClipboard_Click);
            // 
            // buttonAllToClipboard
            // 
            this.buttonAllToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAllToClipboard.AutoSize = true;
            this.buttonAllToClipboard.Location = new System.Drawing.Point(147, 13);
            this.buttonAllToClipboard.Name = "buttonAllToClipboard";
            this.buttonAllToClipboard.Size = new System.Drawing.Size(61, 40);
            this.buttonAllToClipboard.TabIndex = 8;
            this.buttonAllToClipboard.Text = "   All to \r\nclipboard";
            this.buttonAllToClipboard.UseVisualStyleBackColor = false;
            this.buttonAllToClipboard.Click += new System.EventHandler(this.buttonAllToClipboard_Click);
            // 
            // buttonDisplayParam
            // 
            this.buttonDisplayParam.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDisplayParam.AutoSize = true;
            this.buttonDisplayParam.Location = new System.Drawing.Point(4, 13);
            this.buttonDisplayParam.Name = "buttonDisplayParam";
            this.buttonDisplayParam.Size = new System.Drawing.Size(61, 40);
            this.buttonDisplayParam.TabIndex = 7;
            this.buttonDisplayParam.Text = "Display\r\n Param";
            this.buttonDisplayParam.UseVisualStyleBackColor = false;
            this.buttonDisplayParam.Click += new System.EventHandler(this.buttonDisplayParam_Click);
            // 
            // buttonClearMessages
            // 
            this.buttonClearMessages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonClearMessages.AutoSize = true;
            this.buttonClearMessages.Location = new System.Drawing.Point(72, 13);
            this.buttonClearMessages.Name = "buttonClearMessages";
            this.buttonClearMessages.Size = new System.Drawing.Size(68, 40);
            this.buttonClearMessages.TabIndex = 7;
            this.buttonClearMessages.Text = "    Clear\r\n Messages";
            this.buttonClearMessages.UseVisualStyleBackColor = false;
            this.buttonClearMessages.Click += new System.EventHandler(this.buttonClearMessages_Click);
            // 
            // buttonCu8ToWav
            // 
            this.buttonCu8ToWav.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonCu8ToWav.Location = new System.Drawing.Point(23, 215);
            this.buttonCu8ToWav.Name = "buttonCu8ToWav";
            this.buttonCu8ToWav.Size = new System.Drawing.Size(104, 28);
            this.buttonCu8ToWav.TabIndex = 14;
            this.buttonCu8ToWav.Text = ".cu8 to .wav";
            this.buttonCu8ToWav.UseVisualStyleBackColor = false;
            this.buttonCu8ToWav.Click += new System.EventHandler(this.buttonCu8ToWav_Click);
            // 
            // Rtl_433_Panel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = true;
            this.Controls.Add(this.mainTableLayoutPanel);
            this.Name = "Rtl_433_Panel";
            this.Size = new System.Drawing.Size(296, 1276);
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
            this.groupBoxInfos.ResumeLayout(false);
            this.groupBoxInfos.PerformLayout();
            this.groupBoxRecordTxtFile.ResumeLayout(false);
            this.groupBoxRecordTxtFile.PerformLayout();
            this.groupBoxEnabledPlugin.ResumeLayout(false);
            this.groupBoxEnabledPlugin.PerformLayout();
            this.groupBoxConsole.ResumeLayout(false);
            this.groupBoxConsole.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.TableLayoutPanel mainTableLayoutPanel;
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
        private System.Windows.Forms.CheckBox checkBoxEnabledPlugin;
        private System.Windows.Forms.Label labelSampleRate;
        private System.Windows.Forms.Label labelFrequency;
        private System.Windows.Forms.Label labelCycleTime;
        private System.Windows.Forms.Label labelTime433;
        private System.Windows.Forms.GroupBox groupBoxInfos;
        private System.Windows.Forms.GroupBox groupBoxRecordTxtFile;
        private System.Windows.Forms.Label labelWarningRecordTextFile;
        private System.Windows.Forms.CheckBox checkBoxRecordTxtFile;
        private System.Windows.Forms.ListView listViewConsole;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.GroupBox groupBoxEnabledPlugin;
        private System.Windows.Forms.GroupBox groupBoxConsole;
        private System.Windows.Forms.Button buttonSelectToClipboard;
        private System.Windows.Forms.Button buttonAllToClipboard;
    }
}
