using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SDRSharp.Rtl_433
{
    partial class Rtl_433_Panel : IDisposable
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(Boolean disposing)   //
        {
            Debug.WriteLine("designer->FreeRessources");
            FreeRessources();
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
            this.groupBoxFrequency = new System.Windows.Forms.GroupBox();
            this.radioButtonFreq915 = new System.Windows.Forms.RadioButton();
            this.radioButtonFreq868 = new System.Windows.Forms.RadioButton();
            this.radioButtonFreq43392 = new System.Windows.Forms.RadioButton();
            this.radioButtonFreq345 = new System.Windows.Forms.RadioButton();
            this.radioButtonFreq315 = new System.Windows.Forms.RadioButton();
            this.radioButtonFreqFree = new System.Windows.Forms.RadioButton();
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
            this.groupBoxEnabledPlugin = new System.Windows.Forms.GroupBox();
            this.checkBoxEnabledPlugin = new System.Windows.Forms.CheckBox();
            this.buttonStartStop = new System.Windows.Forms.Button();
            this.groupBoxConsole = new System.Windows.Forms.GroupBox();
            this.buttonSelectToClipboard = new System.Windows.Forms.Button();
            this.buttonAllToClipboard = new System.Windows.Forms.Button();
            this.buttonDisplayParam = new System.Windows.Forms.Button();
            this.buttonClearMessages = new System.Windows.Forms.Button();
            this.buttonCu8ToWav = new System.Windows.Forms.Button();
            this.groupBoxInfos = new System.Windows.Forms.GroupBox();
            this.labelTimeDisplay = new System.Windows.Forms.Label();
            this.labelTimeDisplayWindows = new System.Windows.Forms.Label();
            this.labelSampleRateTxt = new System.Windows.Forms.Label();
            this.labelTimeCycle = new System.Windows.Forms.Label();
            this.labelFrequencyTxt = new System.Windows.Forms.Label();
            this.labelSampleRate = new System.Windows.Forms.Label();
            this.labelFrequency = new System.Windows.Forms.Label();
            this.labelCycleTime = new System.Windows.Forms.Label();
            this.labelTime433 = new System.Windows.Forms.Label();
            this.labelTimeRtl433 = new System.Windows.Forms.Label();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listViewConsole = new System.Windows.Forms.ListView();
            this.mainTableLayoutPanel.SuspendLayout();
            this.groupBoxDataConv.SuspendLayout();
            this.groupBoxFrequency.SuspendLayout();
            this.groupBoxSave.SuspendLayout();
            this.groupBoxR.SuspendLayout();
            this.groupBoxHideShow.SuspendLayout();
            this.groupBoxSelectTypeForm.SuspendLayout();
            this.groupBoxEnabledPlugin.SuspendLayout();
            this.groupBoxConsole.SuspendLayout();
            this.groupBoxInfos.SuspendLayout();
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
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxFrequency, 1, 2);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxSave, 1, 6);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxR, 0, 9);
            this.mainTableLayoutPanel.Controls.Add(this.labelHideDevices, 0, 7);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxHideShow, 0, 8);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxSelectTypeForm, 0, 1);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxEnabledPlugin, 0, 0);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxConsole, 0, 11);
            this.mainTableLayoutPanel.Controls.Add(this.buttonCu8ToWav, 0, 3);
            this.mainTableLayoutPanel.Controls.Add(this.groupBoxInfos, 0, 2);
            this.mainTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            this.mainTableLayoutPanel.RowCount = 13;
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 147F));
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayoutPanel.Size = new System.Drawing.Size(316, 1337);
            this.mainTableLayoutPanel.TabIndex = 1;
            this.mainTableLayoutPanel.SizeChanged += new System.EventHandler(this.MainTableLayoutPanel_SizeChanged);
            // 
            // groupBoxDataConv
            // 
            this.groupBoxDataConv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxDataConv.Controls.Add(this.radioButtonDataConvCustomary);
            this.groupBoxDataConv.Controls.Add(this.radioButtonDataConvSI);
            this.groupBoxDataConv.Controls.Add(this.radioButtonDataConvNative);
            this.groupBoxDataConv.Location = new System.Drawing.Point(3, 217);
            this.groupBoxDataConv.Name = "groupBoxDataConv";
            this.groupBoxDataConv.Size = new System.Drawing.Size(124, 113);
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
            this.groupBoxFrequency.Size = new System.Drawing.Size(160, 89);
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
            // groupBoxSave
            // 
            this.groupBoxSave.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxSave.Controls.Add(this.radioButtonSnone);
            this.groupBoxSave.Controls.Add(this.radioButtonSunknown);
            this.groupBoxSave.Controls.Add(this.radioButtonSknown);
            this.groupBoxSave.Controls.Add(this.radioButtonSall);
            this.groupBoxSave.Location = new System.Drawing.Point(153, 217);
            this.groupBoxSave.Name = "groupBoxSave";
            this.groupBoxSave.Size = new System.Drawing.Size(160, 113);
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
            this.groupBoxR.Location = new System.Drawing.Point(3, 392);
            this.groupBoxR.Name = "groupBoxR";
            this.groupBoxR.Size = new System.Drawing.Size(310, 141);
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
            this.listBoxHideShowDevices.Size = new System.Drawing.Size(304, 122);
            this.listBoxHideShowDevices.TabIndex = 3;
            // 
            // labelHideDevices
            // 
            this.labelHideDevices.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelHideDevices.AutoSize = true;
            this.mainTableLayoutPanel.SetColumnSpan(this.labelHideDevices, 2);
            this.labelHideDevices.Location = new System.Drawing.Point(3, 333);
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
            this.groupBoxHideShow.Location = new System.Drawing.Point(3, 349);
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
            this.radioButtonListMessages.CheckedChanged += new System.EventHandler(this.RadioButtonTypeWindow_CheckedChanged);
            // 
            // radioButtonListDevices
            // 
            this.radioButtonListDevices.Location = new System.Drawing.Point(183, 11);
            this.radioButtonListDevices.Name = "radioButtonListDevices";
            this.radioButtonListDevices.Size = new System.Drawing.Size(99, 17);
            this.radioButtonListDevices.TabIndex = 0;
            this.radioButtonListDevices.Text = "List devices";
            this.radioButtonListDevices.UseVisualStyleBackColor = true;
            this.radioButtonListDevices.CheckedChanged += new System.EventHandler(this.RadioButtonTypeWindow_CheckedChanged);
            // 
            // radioButtonGraph
            // 
            this.radioButtonGraph.Location = new System.Drawing.Point(115, 11);
            this.radioButtonGraph.Name = "radioButtonGraph";
            this.radioButtonGraph.Size = new System.Drawing.Size(66, 17);
            this.radioButtonGraph.TabIndex = 0;
            this.radioButtonGraph.Text = "Graph";
            this.radioButtonGraph.UseVisualStyleBackColor = true;
            this.radioButtonGraph.CheckedChanged += new System.EventHandler(this.RadioButtonTypeWindow_CheckedChanged);
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
            this.checkBoxEnabledPlugin.CheckedChanged += new System.EventHandler(this.CheckBoxEnabledPlugin_CheckedChanged);
            // 
            // buttonStartStop
            // 
            this.buttonStartStop.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonStartStop.Location = new System.Drawing.Point(183, 11);
            this.buttonStartStop.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.buttonStartStop.Name = "buttonStartStop";
            this.buttonStartStop.Size = new System.Drawing.Size(98, 22);
            this.buttonStartStop.TabIndex = 19;
            this.buttonStartStop.Text = "Wait Radio";
            this.buttonStartStop.UseVisualStyleBackColor = false;
            this.buttonStartStop.Click += new System.EventHandler(this.ButtonStartStop_Click);
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
            this.groupBoxConsole.Location = new System.Drawing.Point(3, 539);
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
            this.buttonSelectToClipboard.Click += new System.EventHandler(this.ButtonSelectToClipboard_Click);
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
            this.buttonAllToClipboard.Click += new System.EventHandler(this.ButtonAllToClipboard_Click);
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
            this.buttonDisplayParam.Click += new System.EventHandler(this.ButtonDisplayParam_Click);
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
            this.buttonClearMessages.Click += new System.EventHandler(this.ButtonClearMessages_Click);
            // 
            // buttonCu8ToWav
            // 
            this.buttonCu8ToWav.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonCu8ToWav.Location = new System.Drawing.Point(23, 183);
            this.buttonCu8ToWav.Name = "buttonCu8ToWav";
            this.buttonCu8ToWav.Size = new System.Drawing.Size(104, 28);
            this.buttonCu8ToWav.TabIndex = 14;
            this.buttonCu8ToWav.Text = ".cu8 to .wav";
            this.buttonCu8ToWav.UseVisualStyleBackColor = false;
            this.buttonCu8ToWav.Click += new System.EventHandler(this.ButtonCu8ToWav_Click);
            // 
            // groupBoxInfos
            // 
            this.groupBoxInfos.Controls.Add(this.labelTimeDisplay);
            this.groupBoxInfos.Controls.Add(this.labelTimeDisplayWindows);
            this.groupBoxInfos.Controls.Add(this.labelSampleRateTxt);
            this.groupBoxInfos.Controls.Add(this.labelTimeCycle);
            this.groupBoxInfos.Controls.Add(this.labelFrequencyTxt);
            this.groupBoxInfos.Controls.Add(this.labelSampleRate);
            this.groupBoxInfos.Controls.Add(this.labelFrequency);
            this.groupBoxInfos.Controls.Add(this.labelCycleTime);
            this.groupBoxInfos.Controls.Add(this.labelTime433);
            this.groupBoxInfos.Controls.Add(this.labelTimeRtl433);
            this.groupBoxInfos.Location = new System.Drawing.Point(3, 88);
            this.groupBoxInfos.Name = "groupBoxInfos";
            this.groupBoxInfos.Size = new System.Drawing.Size(144, 89);
            this.groupBoxInfos.TabIndex = 31;
            this.groupBoxInfos.TabStop = false;
            // 
            // labelTimeDisplay
            // 
            this.labelTimeDisplay.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelTimeDisplay.AutoSize = true;
            this.labelTimeDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTimeDisplay.Location = new System.Drawing.Point(85, 61);
            this.labelTimeDisplay.Name = "labelTimeDisplay";
            this.labelTimeDisplay.Size = new System.Drawing.Size(13, 13);
            this.labelTimeDisplay.TabIndex = 32;
            this.labelTimeDisplay.Text = "0";
            // 
            // labelTimeDisplayWindows
            // 
            this.labelTimeDisplayWindows.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelTimeDisplayWindows.AutoSize = true;
            this.labelTimeDisplayWindows.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTimeDisplayWindows.Location = new System.Drawing.Point(6, 61);
            this.labelTimeDisplayWindows.Name = "labelTimeDisplayWindows";
            this.labelTimeDisplayWindows.Size = new System.Drawing.Size(69, 13);
            this.labelTimeDisplayWindows.TabIndex = 31;
            this.labelTimeDisplayWindows.Text = "Time display :";
            // 
            // labelSampleRateTxt
            // 
            this.labelSampleRateTxt.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelSampleRateTxt.AutoSize = true;
            this.labelSampleRateTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSampleRateTxt.Location = new System.Drawing.Point(6, 22);
            this.labelSampleRateTxt.Name = "labelSampleRateTxt";
            this.labelSampleRateTxt.Size = new System.Drawing.Size(75, 13);
            this.labelSampleRateTxt.TabIndex = 6;
            this.labelSampleRateTxt.Text = "Sample Rate/s";
            // 
            // labelTimeCycle
            // 
            this.labelTimeCycle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelTimeCycle.AutoSize = true;
            this.labelTimeCycle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTimeCycle.Location = new System.Drawing.Point(85, 34);
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
            this.labelFrequencyTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFrequencyTxt.Location = new System.Drawing.Point(6, 9);
            this.labelFrequencyTxt.Name = "labelFrequencyTxt";
            this.labelFrequencyTxt.Size = new System.Drawing.Size(73, 13);
            this.labelFrequencyTxt.TabIndex = 6;
            this.labelFrequencyTxt.Text = "Frequency(hz)";
            // 
            // labelSampleRate
            // 
            this.labelSampleRate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelSampleRate.AutoSize = true;
            this.labelSampleRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSampleRate.Location = new System.Drawing.Point(85, 21);
            this.labelSampleRate.Name = "labelSampleRate";
            this.labelSampleRate.Size = new System.Drawing.Size(13, 13);
            this.labelSampleRate.TabIndex = 6;
            this.labelSampleRate.Text = "0";
            // 
            // labelFrequency
            // 
            this.labelFrequency.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelFrequency.AutoSize = true;
            this.labelFrequency.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFrequency.Location = new System.Drawing.Point(85, 9);
            this.labelFrequency.Name = "labelFrequency";
            this.labelFrequency.Size = new System.Drawing.Size(13, 13);
            this.labelFrequency.TabIndex = 6;
            this.labelFrequency.Text = "0";
            // 
            // labelCycleTime
            // 
            this.labelCycleTime.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelCycleTime.AutoSize = true;
            this.labelCycleTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCycleTime.Location = new System.Drawing.Point(6, 35);
            this.labelCycleTime.Name = "labelCycleTime";
            this.labelCycleTime.Size = new System.Drawing.Size(65, 13);
            this.labelCycleTime.TabIndex = 29;
            this.labelCycleTime.Text = "Cycle time/s:";
            this.labelCycleTime.Visible = false;
            // 
            // labelTime433
            // 
            this.labelTime433.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelTime433.AutoSize = true;
            this.labelTime433.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTime433.Location = new System.Drawing.Point(6, 48);
            this.labelTime433.Name = "labelTime433";
            this.labelTime433.Size = new System.Drawing.Size(78, 13);
            this.labelTime433.TabIndex = 30;
            this.labelTime433.Text = "Time RTL433/s:";
            this.labelTime433.Visible = false;
            // 
            // labelTimeRtl433
            // 
            this.labelTimeRtl433.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelTimeRtl433.AutoSize = true;
            this.labelTimeRtl433.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTimeRtl433.Location = new System.Drawing.Point(85, 47);
            this.labelTimeRtl433.Name = "labelTimeRtl433";
            this.labelTimeRtl433.Size = new System.Drawing.Size(13, 13);
            this.labelTimeRtl433.TabIndex = 16;
            this.labelTimeRtl433.Text = "0";
            this.labelTimeRtl433.Visible = false;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "RTL_433 messages ";
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
            this.listViewConsole.Location = new System.Drawing.Point(3, 604);
            this.listViewConsole.Name = "listViewConsole";
            this.listViewConsole.Size = new System.Drawing.Size(310, 730);
            this.listViewConsole.TabIndex = 33;
            this.listViewConsole.UseCompatibleStateImageBehavior = false;
            // 
            // Rtl_433_Panel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.Controls.Add(this.mainTableLayoutPanel);
            this.Name = "Rtl_433_Panel";
            this.Size = new System.Drawing.Size(316, 1337);
            this.mainTableLayoutPanel.ResumeLayout(false);
            this.mainTableLayoutPanel.PerformLayout();
            this.groupBoxDataConv.ResumeLayout(false);
            this.groupBoxFrequency.ResumeLayout(false);
            this.groupBoxSave.ResumeLayout(false);
            this.groupBoxR.ResumeLayout(false);
            this.groupBoxHideShow.ResumeLayout(false);
            this.groupBoxSelectTypeForm.ResumeLayout(false);
            this.groupBoxEnabledPlugin.ResumeLayout(false);
            this.groupBoxEnabledPlugin.PerformLayout();
            this.groupBoxConsole.ResumeLayout(false);
            this.groupBoxConsole.PerformLayout();
            this.groupBoxInfos.ResumeLayout(false);
            this.groupBoxInfos.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.TableLayoutPanel mainTableLayoutPanel;
        private System.Windows.Forms.Label labelSampleRateTxt;
        private System.Windows.Forms.Button buttonClearMessages;
        private System.Windows.Forms.Button buttonDisplayParam;
        private System.Windows.Forms.Label labelFrequencyTxt;
        private System.Windows.Forms.GroupBox groupBoxFrequency;
        private System.Windows.Forms.RadioButton radioButtonFreq915;
        private System.Windows.Forms.RadioButton radioButtonFreq868;
        private System.Windows.Forms.RadioButton radioButtonFreq43392;
        private System.Windows.Forms.RadioButton radioButtonFreq345;
        private System.Windows.Forms.RadioButton radioButtonFreq315;
        private System.Windows.Forms.RadioButton radioButtonFreqFree;
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
        private System.Windows.Forms.CheckBox checkBoxEnabledPlugin;
        private System.Windows.Forms.Label labelSampleRate;
        private System.Windows.Forms.Label labelFrequency;
        private System.Windows.Forms.Label labelCycleTime;
        private System.Windows.Forms.Label labelTime433;
        private System.Windows.Forms.GroupBox groupBoxInfos;
        private System.Windows.Forms.GroupBox groupBoxEnabledPlugin;
        private System.Windows.Forms.GroupBox groupBoxConsole;
        private System.Windows.Forms.Button buttonSelectToClipboard;
        private System.Windows.Forms.Button buttonAllToClipboard;
        private System.Windows.Forms.Label labelTimeDisplay;
        private System.Windows.Forms.Label labelTimeDisplayWindows;
        private System.Windows.Forms.ListView listViewConsole;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}
