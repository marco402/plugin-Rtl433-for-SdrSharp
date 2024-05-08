/* Written by Marc Prieur (marco40_github@sfr.fr)
                                Rtl_433_Panel.cs 
                            project Rtl_433_Plugin
						         Plugin for SdrSharp
 **************************************************************************************
 Creative Commons Attrib Share-Alike License
 You are free to use/extend this library but please abide with the CC-BY-SA license:
 Attribution-NonCommercial-ShareAlike 4.0 International License
 http://creativecommons.org/licenses/by-nc-sa/4.0/

 All text above must be included in any redistribution.
  **********************************************************************************/
//#define TESTWINDOWS
using System.Reflection;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using SDRSharp.Common;
using System.Diagnostics;

namespace SDRSharp.Rtl_433
{
    public partial class Rtl_433_Panel : UserControl
    {
        public String VERSION =" V: " + FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;          // Assembly.GetEntryAssembly().GetName().Version.ToString();  //"1.5.6.3";  //update also project property version and file version
        private Boolean listViewConsoleFull = false;
        List<ListViewItem> cacheLignes;
        private Boolean recordDevice = false;
        private String nameToRecord = "";
        private Boolean radioIsStarted = false;
        TYPEFORM displayTypeForm = TYPEFORM.LISTMES;
#if TESTWINDOWS
        private Int32 cptDevicesForTest = 0;   //test device windows always ok until 143 ~ 1.3G of memory
#endif
        private const Int32 NBCOLUMN = 100;
        private enum TYPEFORM : Int32
        {
            LISTDEV,
            GRAPH,
            LISTMES
        }
        private const String FILELISTEDEVICES = "devices.txt";
        private Dictionary<String, FormDevices> listformDevice;
        private Dictionary<String, FormDevicesListMessages> listformDeviceListMessages;
        private Rtl_433_Processor Rtl_433Processor;
        private ClassInterfaceWithRtl433 ClassInterfaceWithRtl433;
        private FormListDevices formListDevice = null;
        private Boolean enabledPlugin = false;
        private ISharpControl control;
#region init class

        internal Rtl_433_Panel(ISharpControl control)
        {
            InitializeComponent();
            this.control = control;
            displayTypeForm = TYPEFORM.LISTMES;
            this.Size = new System.Drawing.Size(296, 600);   //ok for load first with 1920 beta (erase sdrsharp.layout)
            initDisplayParam();  //before initControls
            setMaxLinesConsole(1000);             // for displayParam to initControls before init Plugin
            initVirtualListView();
            initControls();
            enabledDisabledAllControls(false);
#if TESTWINDOWS
            MessageBox.Show("Version de test");
#endif
        }

#region  listViewConsole 
        private void initVirtualListView()
        {
            typeof(Control).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, listViewConsole, new object[] { true });
            this.SuspendLayout();
            SDRSharp.Rtl_433.ClassFunctionsVirtualListView.initListView(listViewConsole);
            listViewConsole.GridLines = false;
            listViewConsole.FullRowSelect = false;
            listViewConsole.View = View.Details;   //hide column header
            listViewConsole.MultiSelect = true;
            listViewConsole.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(listViewConsole_RetrieveVirtualItem);
            this.ResumeLayout(true);
        }

        internal void listViewConsole_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if (cacheLignes != null)
            {
                try
                {
                    if (e.ItemIndex >= 0)
                    {
                        ListViewItem lvi = cacheLignes[e.ItemIndex];
                        if (lvi != null)
                            e.Item = lvi;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error fct(listViewConsole_RetrieveVirtualItem)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
 
        internal Boolean WriteLine(Dictionary<String, String> listData)
        {
            String theLine = String.Empty;
            foreach (KeyValuePair<String, String> _line in listData)
                theLine += (_line.Key + _line.Value);
            return WriteLine(theLine);
        }

        private String memoLigne="";
        internal Boolean WriteLine(String theLine)
        {
            //if (theLine.Contains("rows"))
            //    memoLigne = memoLigne;
            ////#if DEBUG
            //byte[] L = Encoding.ASCII.GetBytes(theLine);

            //for (int c = 0; c < theLine.Length; c++)
            //{
            //    //if (theLine.Substring(c, 1) == "\n")        //0a
            //    //    Debug.Write("(lf)\n");
            //    //else 
            //    if (theLine.Substring(c, 1) == "\n")   //0a
            //        Debug.Write("(lf)\n");
            //    else
            //        Debug.Write("0" + L[c].ToString("x") + "(" + theLine.Substring(c, 1) + ")");
            //}
            //Debug.WriteLine("");
            //for (int c = 0; c < theLine.Length; c++)
            //    Debug.Write(theLine.Substring(c, 1));
            //Debug.WriteLine("");
            ////#endif
            int cptLine = 0;
            Boolean retour = false;
            if (cacheLignes == null)
            {
                cacheLignes = new List<ListViewItem>();
            }
            this.SuspendLayout();
            listViewConsole.BeginUpdate();
            memoLigne += theLine;
            String[] newLigne = memoLigne.Split((char)0x0a);
            memoLigne = "";
            int lastWord = 0;
            if (newLigne[newLigne.Length - 1].Length > 0)
            {
                memoLigne = newLigne[newLigne.Length - 1];
                lastWord = 1;
            }
            for (int i = 0; i < newLigne.Length - lastWord; i++)
            {
                if (newLigne[i].Length > 0)
                {
                    ListViewItem ligne = new ListViewItem(newLigne[i]);
                    cacheLignes.Add(ligne);
                    cptLine += 1;
                }
            }
            listViewConsole.VirtualListSize += cptLine;
            if (listViewConsole.VirtualListSize > maxLinesConsole - 1)
            {
                listViewConsole.ForeColor = Color.Red;
                ListViewItem ligne = new ListViewItem("You have reached the maximum number of rows provided in the console(" + maxLinesConsole.ToString() + ")");
                cacheLignes.Add(ligne);
                ligne = new ListViewItem("if necessary you can increase it in SDRSharp.config(key RTL_433_plugin.maxLinesConsole");
                cacheLignes.Add(ligne);
                listViewConsole.VirtualListSize += 2;
                listViewConsole.Columns[0].Text = "Console RTL_433---nbLigne=" + maxLinesConsole.ToString() + "/" + maxLinesConsole.ToString();
                retour = true;
             }
            listViewConsole.Columns[0].Text = "Console RTL_433---nbLigne=" + listViewConsole.VirtualListSize.ToString() + "/" + maxLinesConsole.ToString();
            if (listViewConsole.VirtualListSize > 0)
            {
                var last = listViewConsole.Items[listViewConsole.VirtualListSize - 1];
                last.EnsureVisible();
            }
            listViewConsole.EndUpdate();
            this.ResumeLayout(true);
            return retour;

        }
        private void buttonAllToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            String text = String.Empty;
            if (listViewConsole.Items.Count > 1)
            {
                for (int item = 0; item < listViewConsole.Items.Count; item++)
                    text += listViewConsole.Items[item].Text + "\n";
                Clipboard.SetText(text);
            }
        }

        private void buttonSelectToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            String text = String.Empty;
            ListView.SelectedIndexCollection col = listViewConsole.SelectedIndices;
            if (col.Count > 0)
            {
                foreach (int item in col)
                    text += listViewConsole.Items[item].Text;
                Clipboard.SetText(text);
            }
        }
        //#if DEBUG
        //private static char Chr(byte src)
        //{
        //    return (System.Text.Encoding.GetEncoding("iso-8859-1").GetChars(new byte[] {src})[0]);
        //}
        //#endif
        private void mainTableLayoutPanel_SizeChanged(object sender, EventArgs e)
        {
            listViewConsole.Columns[0].Width = listViewConsole.Width;
            listBoxHideShowDevices.BackColor = mainTableLayoutPanel.BackColor;  //Put here Too early in initcontrols.
            listBoxHideShowDevices.ForeColor = mainTableLayoutPanel.ForeColor;  //
        }
        private void buttonClearMessages_Click(object sender, EventArgs e)
        {
            clearListViewConsole();
        }
        private void clearListViewConsole()
        {
            listViewConsole.VirtualListSize = 0;
            listViewConsole.Columns[0].Text = "Console RTL_433---nbLigne=0" + "/" + maxLinesConsole.ToString();
            cacheLignes = null;
            listViewConsoleFull = false;
        }
#endregion
        private void initControls()
        {
#if TESTIME
            labelCycleTime.Visible = true;
            labelTimeCycle.Visible = true;
            labelTime433.Visible = true;
            labelTimeRtl433.Visible = true;
            ToolTip ttlabelTimeCycle = new ToolTip();
            ttlabelTimeCycle.SetToolTip(labelTimeCycle, "Red if Time cycle > 1000ms., you risk losing messages(not if source=file)");
#endif
            displayParam();
            buttonStartStop.Text = "Wait";
            buttonStartStop.Enabled = false;
            ToolTip OptionStereo = new ToolTip();
            OptionStereo.SetToolTip(checkBoxSTEREO, "Record IQ to wav file for reload with SDRSharp(Graph windows)");
            ToolTip OptionMono = new ToolTip();
            OptionMono.SetToolTip(checkBoxMONO, "Record module de IQ to wav file for display with Audacity or another viewer(Graph windows)");
            ToolTip ttcheckBoxEnabledDevicesDisabled = new ToolTip();
            ttcheckBoxEnabledDevicesDisabled.SetToolTip(checkBoxEnabledDevicesDisabled, "0:default,1:WARNING->enabled devices disabled in devices files");
            ToolTip ttcheckBoxRecordTextFile = new ToolTip();
            ttcheckBoxRecordTextFile.SetToolTip(checkBoxRecordTxtFile, "If checked, saves the data displayed on all list messages windows.\n Name = date_time_name of the device(+channel).txt.\n Warning to the free disk place.\n You can create folder Recordings.\n You can select the desired devices in the list of devices by checking show select.");
            ttcheckBoxRecordTextFile.AutoPopDelay = 10000;
            radioButtonFreq43392.Checked = true;
            listBoxHideShowDevices.Visible = true;
            ToolTip OptionVerbose = new ToolTip();
            OptionVerbose.SetToolTip(groupBoxVerbose, "WARNING -vvv and -vvvv possible bad devices for debug !");
            labelSampleRate.Text = control.RFBandwidth.ToString();
            listViewConsole.Columns[0].Text = "Console RTL_433---nbLigne=" + listViewConsole.VirtualListSize.ToString(); //  + "/" + maxLinesConsole.ToString(); maxLinesConsole not init here
            //listBoxHideShowDevices.BackColor = mainTableLayoutPanel.BackColor;  //pb heritage ? Too early here, put in Layout Resize
            //listBoxHideShowDevices.ForeColor = mainTableLayoutPanel.ForeColor;  //pb heritage ?
        }

        private void enabledDisabledAllControls(Boolean state)
        {
            enabledDisabledControlsOnStart(state);
            this.SuspendLayout();
            radioButtonListDevices.Enabled = state;
            radioButtonListMessages.Enabled = state;
            radioButtonGraph.Enabled = state;
            checkBoxMONO.Enabled = state;
            checkBoxSTEREO.Enabled = state;
            buttonCu8ToWav.Enabled = state;
            buttonDisplayParam.Enabled = state;
            buttonClearMessages.Enabled = state;
            buttonAllToClipboard.Enabled = state;
            buttonSelectToClipboard.Enabled = state;
            this.ResumeLayout(true);
        }

        private void checkBoxEnabledPlugin_CheckedChanged(object sender, EventArgs e)
        {
            //if start radio first, test _control.isplaying
            enabledPlugin = checkBoxEnabledPlugin.Checked;
            if (!enabledPlugin)
            {
                Stop(true);
                DisposePanel(false);
            }
            else
            {
                ClassInterfaceWithRtl433 = new ClassInterfaceWithRtl433(this);
                Rtl_433Processor = new Rtl_433_Processor(control, this, ClassInterfaceWithRtl433);
                setBinding();
                //not in setting 
                process_CheckBoxEnabledDevicesDisabled_CheckedChanged();
                radioIsStarted = control.IsPlaying;  //if enabled before radio is playing
                setSampleRate(control.RFBandwidth);
                setFrequency(control.Frequency);
                //setCenterFrequency(control.CenterFrequency);
                // ClassInterfaceWithRtl433.SampleRateDbl = control.RFBandwidth;
                //Rtl_433Processor.FrequencyRtl433 = control.frequency;
                //Rtl_433Processor.SampleRate = control.RFBandwidth;
                // ClassInterfaceWithRtl433.CenterFrequencyStr=control.CenterFrequency.ToString();
                //ClassInterfaceWithRtl433.FrequencyStr = frequency.ToString(); 
                enabledDisabledAllControls(enabledPlugin);
                if (radioIsStarted && enabledPlugin)
                {
                    buttonStartStop.Text = "Start";
                    buttonStartStop.Enabled = true;
                }
            }
            checkBoxMONO.Enabled = enabledPlugin;
            checkBoxSTEREO.Enabled = enabledPlugin;
        }

#endregion

#region start
        private void setBinding()
        {
            //labelCenterFrequency.DataBindings.Clear();
            //labelVersion.DataBindings.Clear();
            labelSampleRate.DataBindings.Clear();
            labelFrequency.DataBindings.Clear();

            //labelCenterFrequency.DataBindings.Add("Text", ClassInterfaceWithRtl433, "CenterFrequencyStr");
            //labelVersion.DataBindings.Add("Text", _ClassInterfaceWithRtl433, "Version");
            labelSampleRate.DataBindings.Add("Text", ClassInterfaceWithRtl433, "SampleRateStr");
            labelFrequency.DataBindings.Add("Text", ClassInterfaceWithRtl433, "FrequencyStr");
#if TESTIME
            //labelTimeRtl433.DataBindings.Clear();
            //labelTimeCycle.DataBindings.Clear();
            //labelTimeRtl433.DataBindings.Add("Text", ClassInterfaceWithRtl433, "timeForRtl433");
            //labelTimeCycle.DataBindings.Add("Text", ClassInterfaceWithRtl433, "timeCycle");
            labelTimeCycle.Text = "0";
            labelTimeRtl433.Text = "0";
#endif
        }
#if TESTIME
        internal void setTime(long timeCycleCumul, long timeForRtl433Cumul, Boolean sourceIsFile)
        {
            labelTimeCycle.Text = (timeCycleCumul).ToString() + " ms.";
            labelTimeRtl433.Text = (timeForRtl433Cumul).ToString() + " ms.";
            if (timeCycleCumul > 1000 && !sourceIsFile)
                labelTimeCycle.ForeColor = Color.Red;
            else
                labelTimeCycle.ForeColor = labelSampleRate.ForeColor;
        }
#endif
        internal void Start(Boolean senderRadio = false)
        {
            if (senderRadio)
            {
                radioIsStarted = true;
                if (enabledPlugin)
                {
                    buttonStartStop.Enabled = true;
                    buttonStartStop.Text = "Start";
                }
                else
                {
                    buttonStartStop.Enabled = false;
                    buttonStartStop.Text = "Wait";
                }
                //setSampleRate(control.RFBandwidth);
                //setFrequency(control.Frequency);
                //setCenterFrequency(control.CenterFrequency);
            }
            else
            {
                buttonStartStop.Text = "Stop";
                if (listformDevice == null)
                    listformDevice = new Dictionary<String, FormDevices>();
                if (listformDeviceListMessages == null)
                    listformDeviceListMessages = new Dictionary<String, FormDevicesListMessages>();
                clearListViewConsole();
                listViewConsole.ForeColor = this.ForeColor;
                processParameterOnStart();
                //Rtl_433Processor.Enabled = true;
                //Rtl_433Processor.Start();
                //processParameterOnStart();
                enabledDisabledControlsOnStart(false);
                List<String> ListDevicesSH = new List<String>();
                foreach (String device in listBoxHideShowDevices.SelectedItems)
                {
                    String[] part = device.Split(new char[] { '-' });
                    ListDevicesSH.Add(part[0]);
                }
                ClassInterfaceWithRtl433.setHideOrShowDevices(ListDevicesSH, radioButtonHideSelect.Checked);
                ClassInterfaceWithRtl433.setShowDevicesDisabled(checkBoxEnabledDevicesDisabled.Checked);
                //get sample rate
                ClassInterfaceWithRtl433.call_main_Rtl_433();
                Rtl_433Processor.Enabled = true;
                Rtl_433Processor.Start();
            }
            setSampleRate(control.RFBandwidth);
            setFrequency(control.Frequency);
            //setCenterFrequency(control.CenterFrequency);
        }

        private void enabledDisabledControlsOnStart(Boolean state)
        {
            this.SuspendLayout();
            //groupBoxFrequency.Enabled = state;
            //groupBoxVerbose.Enabled = state;
            //groupBoxMetadata.Enabled = state;
            //groupBoxSave.Enabled = state;
            //groupBoxHideShow.Enabled = state;
            //groupBoxDataConv.Enabled = state;
            groupBoxRecord.Enabled = true; //keep enabled for record device window
            //groupBoxOptionY.Enabled = false;

            radioButtonFreqFree.Enabled = state; //try for version from 1830 text disabled  black(no visible)
            radioButtonFreq315.Enabled = state;
            radioButtonFreq345.Enabled = state;
            radioButtonFreq43392.Enabled = state;
            radioButtonFreq868.Enabled = state;
            radioButtonFreq915.Enabled = state;

            radioButtonNoV.Enabled = state;
            radioButtonV.Enabled = state;
            radioButtonVV.Enabled = state;
            radioButtonVVV.Enabled = state;
            radioButtonVVVV.Enabled = state;

            radioButtonNoM.Enabled = state;
            radioButtonMLevel.Enabled = state;

            radioButtonSnone.Enabled = state;
            radioButtonSknown.Enabled = state;
            radioButtonSunknown.Enabled = state;
            radioButtonSall.Enabled = state;

            radioButtonHideSelect.Enabled = state;
            radioButtonShowSelect.Enabled = state;

            radioButtonDataConvCustomary.Enabled = state;
            radioButtonDataConvNative.Enabled = state;
            radioButtonDataConvSI.Enabled = state;

            //listBoxHideShowDevices.Enabled = state;
            checkBoxEnabledDevicesDisabled.Enabled = state;
            this.ResumeLayout(true);
        }

        private void processParameterOnStart()
        {
            //type of windows
            testRadioButtonListDevices();
            //frequency
            frequency = getFrequency();
            Rtl_433Processor.FrequencyRtl433 = frequency;
            Rtl_433Processor.SampleRate = control.RFBandwidth;
            //verbose
            getRadioButtonVerbose();
            //metaData
            getMetaData();
            //data Convert
            getDataConv();
            //save cu8
            getRadioButtonSaveCu8();
            //hide show devices To Start
        }

        private void buttonStartStop_Click(object sender, EventArgs e)
        {
            if (buttonStartStop.Text == "Start")
            {
                ClassInterfaceWithRtl433.startSendData();  //only by button
                Start();
            }
            else
                Stop(false);
        }

        private void testRadioButtonListDevices()
        {
            if (radioButtonListDevices.Checked)
            {
                displayTypeForm = TYPEFORM.LISTDEV;
                String directory = getDirectoryRecording();
                if (formListDevice == null && File.Exists(directory + FILELISTEDEVICES))
                {
                    openformListDevice(directory);
                    //formListDevice = new FormListDevices(this, MaxDevicesWindows * 10, NBCOLUMN);
                    //formListDevice.Show();
                    //DialogResult dialogResult = MessageBox.Show("Do you want import devices list", "Import devices list", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //if (dialogResult == DialogResult.Yes)
                    //{
                    //    formListDevice.deSerializeText(directory + FILELISTEDEVICES);
                    //}
                }
                ClassInterfaceWithRtl433.setTypeWindowGraph(false);
            }
            else if (radioButtonGraph.Checked)
            {
                displayTypeForm = TYPEFORM.GRAPH;
                ClassInterfaceWithRtl433.setTypeWindowGraph(true);
            }
            else  //TYPEFORM.LISTMES
            {
                displayTypeForm = TYPEFORM.LISTMES;
                ClassInterfaceWithRtl433.setTypeWindowGraph(false);
            }
        }

        internal void resetLabelRecord(String deviceName)
        {
            listformDevice[deviceName].resetLabelRecord();
        }
        internal void addFormDevice(Dictionary<String, String> listData, List<PointF>[] points, String[] nameGraph)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)delegate
                {
                    addFormDevice(listData, points, nameGraph);
                });
            }
            else
            {
                if (ClassInterfaceWithRtl433 == null)
                    return;

                String deviceName = getDeviceName(listData);
                if (deviceName.Trim() != "")
                {
#if TESTWINDOWS
                    cptDevicesForTest += 1;                //test device windows
#endif
                    if (radioButtonGraph.Checked)
                    {
                        if (recordDevice & deviceName == nameToRecord)
                        {
                            recordDevice = false;
                            ClassInterfaceWithRtl433.recordDevice(deviceName, getDirectoryRecording());
                            listformDevice[deviceName].resetLabelRecord();
                            //ClassInterfaceWithRtl433.setRecordDevice(deviceName, getDirectoryRecording());
                            //// listformDevice[deviceName].resetLabelRecord();
                        }
                        //lock (listformDevice)
                        //{
                        if (!listformDevice.ContainsKey(deviceName))
                        {
                            if (listformDevice.Count > MaxDevicesWindows - 1)
                                return;
                            listformDevice.Add(deviceName, new FormDevices(this));
                            if (listformDevice.Count < nbDevicesWithGraph)
                                listformDevice[deviceName].displayGraph = true;
                            listformDevice[deviceName].Text = deviceName;
                            //listformDevice[deviceName].Visible = true;
                            listformDevice[deviceName].Show();
                            listformDevice[deviceName].resetLabelRecord();  //after le load for memo...
                                                                            //if (listformDevice.Count < _nbDevicesWithGraph
                        }
                        //}
                        listformDevice[deviceName].setInfoDevice(listData);
                        if (listformDevice[deviceName].displayGraph)
                            listformDevice[deviceName].setDataGraph(points, nameGraph);
                    }
                    else if (radioButtonListDevices.Checked)
                    {
                        if (formListDevice == null)
                        {
                            openformListDevice(getDirectoryRecording());
                            //formListDevice = new FormListDevices(this, MaxDevicesWindows * 10, NBCOLUMN);
                            //formListDevice.Show();
                        }
                       else
                            formListDevice.setInfoDevice(listData);
                    }
                    else  //TYPEFORM.LISTMES
                    {
                        //lock (listformDeviceListMessages)
                        //{
                        if (!listformDeviceListMessages.ContainsKey(deviceName))
                        {
                            if (listformDeviceListMessages.Count > MaxDevicesWindows - 1)
                                return;
                            //if (radioButtonMLevel.Checked)
                            listformDeviceListMessages.Add(deviceName, new FormDevicesListMessages(this, MaxDevicesWindows * 10, deviceName, ClassInterfaceWithRtl433, checkBoxRecordTxtFile.Checked)); //+2 for debug
                                                                                                                                                                                                        //else
                                                                                                                                                                                                        //    listformDeviceListMessages.Add(deviceName, new FormDevicesListMessages(this, MaxDevicesWindows * 10, deviceName, ClassInterfaceWithRtl433,false));  //5 for -mMevel //+2 for debug
                            listformDeviceListMessages[deviceName].Text = deviceName;
                            listformDeviceListMessages[deviceName].Visible = true;
                            listformDeviceListMessages[deviceName].Show();
                        }
                        //}
                        //if ((cpt % 3)==0)
                        //    for (Int32 i=1;i<20; i++)
                        //        listData.Add(i.ToString(), i.ToString());
                        //if ((cpt > 5 & cpt<10) |(cpt > 13 & cpt<15))
                        //{
                        //    for (Int32 i=1;i<20; i++)
                        //        listData.Add(i.ToString(), i.ToString());

                        //}
                        //if (cpt == 10)
                        //    for (Int32 i = 1; i < 100; i++)
                        //        listData.Add(i.ToString(), i.ToString());

                        //    cpt = cpt; 
                        //cpt += 1;
                        listformDeviceListMessages[deviceName].setMessages(listData);
                    }
                }
             }
        }
        private void openformListDevice(String directory)
        {
            formListDevice = new FormListDevices(this, MaxDevicesWindows * 10, NBCOLUMN);
            formListDevice.Show();
            DialogResult dialogResult = MessageBox.Show("Do you want import devices list", "Import devices list", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                formListDevice.deSerializeText(directory + FILELISTEDEVICES);
            }
        }
        #endregion

        #region stop
        /// <summary>
        /// stop and clean
        /// </summary>
        /// <param name="disabledPlugin">if true:end processor</param>
        /// <param name="senderRadio"></param>
        internal void Stop(Boolean disabledPlugin, Boolean senderRadio = false)
        {
            if (ClassInterfaceWithRtl433 != null)
                ClassInterfaceWithRtl433.stopSendDataToRtl433();
            if (Rtl_433Processor != null)
                Rtl_433Processor.Stop(disabledPlugin);
            enabledDisabledControlsOnStart(checkBoxEnabledPlugin.Checked);
            if (senderRadio)
            {
                radioIsStarted = false;
                buttonStartStop.Text = "Wait";
                buttonStartStop.Enabled = false;
            }
            else
            {
                if (radioIsStarted)
                {
                    buttonStartStop.Text = "Start";
                    if (enabledPlugin)
                        buttonStartStop.Enabled = true;
                    else
                        buttonStartStop.Enabled = false;
                }
                else
                {
                    buttonStartStop.Text = "Wait";
                    buttonStartStop.Enabled = false;
                }
            }
        }

        private void DisposePanel(Boolean EndAll)
        {
            if (ClassInterfaceWithRtl433 != null)
                Stop(true);
            //clean panel, close windows
            if (listformDevice != null)
            {
                foreach (KeyValuePair<String, FormDevices> _form in listformDevice)
                {
                    listformDevice[_form.Key].CloseByProgram();
                }
                listformDevice = null;
            }
            if (listformDeviceListMessages != null)
            {
                foreach (KeyValuePair<String, FormDevicesListMessages> _form in listformDeviceListMessages)
                {
                    listformDeviceListMessages[_form.Key].CloseByProgram();
                }
                listformDeviceListMessages = null;
            }
            if (formListDevice != null)
            {
                formListDevice.Close();
                formListDevice = null;
            }

            //if (consoleIsAlive)
            //{
            //    if (Rtl_433Processor != null)
            //        Rtl_433Processor.freeConsole();
            //    if (ClassInterfaceWithRtl433 != null)
            //        ClassInterfaceWithRtl433.free_console();
            //    consoleIsAlive = false;
            //}

            //clean _Rtl_433Processor
            if (Rtl_433Processor != null)
            {
                Rtl_433Processor.Dispose();
                Rtl_433Processor = null;
            }

            if (ClassInterfaceWithRtl433 != null)
            {
                ClassInterfaceWithRtl433.Dispose();
                ClassInterfaceWithRtl433 = null;
            }
        }

#endregion

#region internal functions

        internal void setSampleRate(double SampleRate)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)delegate
                {
                    setSampleRate(SampleRate);
                });
            }
            else
            {
                labelSampleRate.Text = SampleRate.ToString();
            }
        }

        internal void setMessage(String message)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)delegate
                {
                    setMessage(message);
                });
            }
            else
            {
                if (!listViewConsoleFull)
                {
                    listViewConsoleFull = WriteLine(message);
                    //Dictionary<String, String> listMessage;
                    //listMessage = new Dictionary<String, String>();
                    //listMessage.Add(message, "");
                    //listViewConsoleFull=WriteLine(listMessage);
                    //listMessage = null;
                }
            }
        }

        internal void setListDevices(List<String> listeDevice)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)delegate
                {
                    setListDevices(listeDevice);
                });
            }
            else
            {
                this.SuspendLayout();
                listBoxHideShowDevices.Items.Clear();
                foreach (String device in listeDevice)
                {
                    listBoxHideShowDevices.Items.Add(device);
                }
                this.ResumeLayout(true);
            }
        }

        internal String getDeviceName(Dictionary<String, String> listData)
        {
            String key = "";
            String model = "";
            String protocol = "";
            String channel = "";
            String idDEvice = "";
            foreach (KeyValuePair<String, String> _line in listData)
            {
                if (_line.Key.ToUpper().Contains("CHANNEL") & channel == "" & _line.Value != "")
                {
                    channel = _line.Value;
                    key += (" Channel:" + channel);
                }
                else if (_line.Key.ToUpper().Contains("PROTOCOL") & protocol == "" & _line.Value != "")  //protect humidity contain id
                {
                    protocol = _line.Value;
                    key += " Protocol:" + protocol;
                }
                else if (_line.Key.ToUpper().Contains("MODEL") & model == "" & _line.Value != "")
                {
                    model = _line.Value;
                    key += " Model: " + model;
                }
                else if (_line.Key.ToUpper().Contains("IDS") & idDEvice == "" & _line.Value != "")  //ids for honeywell if id pb for tpms
                {
                    idDEvice = _line.Value;
                    key += " Ids: " + idDEvice;
                }
            }
#if TESTWINDOWS
            if (key.Trim() != "")   //test device windows
                key += cptDevicesForTest.ToString();   //test device windows
#endif
            return key;
        }

        internal void saveDevicesList()
        {
            if (formListDevice != null)
            {
                DialogResult dialogResult = MessageBox.Show("Do you want export devices list(devices.txt)", "Export devices list", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                    formListDevice.serializeText(getDirectoryRecording() + FILELISTEDEVICES);
            }
        }

        internal String getDirectoryRecording()
        {
            String directory = "./Recordings/";   //SDRSHARP.exe to SDRSHARP
            if (!Directory.Exists(directory))
            {
                directory = "../Recordings/";  //SDRSHARP.exe to bin
                if (!Directory.Exists(directory))
                {
                    directory = "";
                }
            }
            return directory;
        }

        //internal Boolean getRecordMONO()
        //{
        //    return checkBoxMONO.Checked;
        //}

        //internal Boolean getRecordSTEREO()
        //{
        //    return checkBoxSTEREO.Checked;
        //}

#endregion

#region private functions
        //Dictionary<String, String> listInfos;
        String listInfos;
        private void initDisplayParam()
        {
            ////listInfos = new Dictionary<String, String>();
            //lock(listData)
            //{
            listInfos="Parameters configure source\n" +
            "   -Sampling mode->quadrature sampling\n" +
            "   -Preferred Sample Rate->0.25 MSPS, imposed if record .wav\n" +
            "   -AGC:on(corresponds to auto gain with rtl433) can be manually->off.\n" +
            "   -RTL AGC:on.(not the AGC panel) can be set off if good signals.\n" +
            "   -Check frequency\n";
        }
        private void displayParam()
        {
            if (!listViewConsoleFull)
                listViewConsoleFull=WriteLine(listInfos);
        }

#endregion

#region callBack from devices form

        internal void closingOneFormDevice(String key)
        {
            //lock (listformDevice)
            //{
            if (listformDevice.ContainsKey(key))
                listformDevice.Remove(key);
            //}
        }

        internal void closingOneFormDeviceListMessages(String key)
        {
            //lock (listformDeviceListMessages)
            //{
            if (listformDeviceListMessages.ContainsKey(key))
                listformDeviceListMessages.Remove(key);
            //}
        }

        internal void closingFormListDevice()
        {
            saveDevicesList();
            formListDevice = null;
        }

        internal Boolean setRecordDevice(String name, Boolean choice)
        {
            if (choice)
            {
                if ((!checkBoxMONO.Checked && !checkBoxSTEREO.Checked) || labelSampleRate.Text != "250000")
                {
                    MessageBox.Show("Choice MONO/STEREO or record only to 250000 MSPS->use -S option", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                else
                {
                    foreach (KeyValuePair<String, FormDevices> _form in listformDevice)
                    {
                        if (_form.Key != name)
                            listformDevice[_form.Key].resetLabelRecord();
                    }
                    nameToRecord = name;
                    recordDevice = choice;
                    return true;
                }
            }
            else
            {
                recordDevice = choice;
                ClassInterfaceWithRtl433.clearRecord();
                return true;
            }
        }

#endregion

#region event panel control

        private void buttonDisplayParam_Click(object sender, EventArgs e)
        {
            displayParam();
        }

        private void buttonCu8ToWav_Click(object sender, EventArgs e)
        {
            if (!checkBoxMONO.Checked && !checkBoxSTEREO.Checked)
                MessageBox.Show("Choice MONO / STEREO (stop before)", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                OpenFileDialog openCu8 = new OpenFileDialog();
                openCu8.DefaultExt = "cu8";
                openCu8.Filter = "cu8 files|*.cu8";
                openCu8.Multiselect = true;
                if (openCu8.ShowDialog() == DialogResult.OK)
                {
                    foreach (String file in openCu8.FileNames)
                    {
                        wavRecorder.convertCu8ToWav(file, checkBoxMONO.Checked, checkBoxSTEREO.Checked, 1);
                    }
                    MessageBox.Show("Recording is completed", "Translate cu8 to wav", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                openCu8.Dispose();
            }
        }

        private void checkBoxEnabledDevicesDisabled_CheckedChanged(object sender, EventArgs e)
        {
            process_CheckBoxEnabledDevicesDisabled_CheckedChanged();
        }

        private void process_CheckBoxEnabledDevicesDisabled_CheckedChanged()
        {
            if (checkBoxEnabledDevicesDisabled.Checked)
            {
                checkBoxEnabledDevicesDisabled.Text = "Enabled devices disabled";
            }
            else
            {
                checkBoxEnabledDevicesDisabled.Text = "Default value";
            }
        }

        private void radioButtonTypeWindow_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxEnabledPlugin.Checked)
                testRadioButtonListDevices();
        }
        #endregion

        private void checkBoxSTEREO_CheckedChanged(object sender, EventArgs e)
        {
            ClassInterfaceWithRtl433.setSTEREO(checkBoxSTEREO.Checked);
        }

        private void checkBoxMONO_CheckedChanged(object sender, EventArgs e)
        {
            ClassInterfaceWithRtl433.setMONO(checkBoxMONO.Checked);
        }
    }
}
