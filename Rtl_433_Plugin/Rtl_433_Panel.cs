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
#define noTESTWINDOWS    //for test memory
#define noTESTRECURSIF                  //used for copy one file for each srcFolder to dstFolder   used for zip download on RTL433 and convert CU8 to Wav and test with TESTBOUCLEREPLAYMARC
#define noTESTBATCH                     //genere file batch
using SDRSharp.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
namespace SDRSharp.Rtl_433
{
    public partial class Rtl_433_Panel : UserControl  //no internal for this partial vs2017? 
    {
        #region declaration
        private Boolean listViewConsoleFull = false;
        internal String VERSION = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).ProductVersion;          // Assembly.GetEntryAssembly().GetName().Version.ToString();  //"1.5.6.3";  //update also project property version and file version
        List<ListViewItem> cacheLignes;
        private Boolean radioIsStarted = false;
#if TESTWINDOWS
        private Int32 cptDevicesForTest = 0;   //test device windows always ok until 143 ~ 1.3G of memory
#endif
        private Stopwatch stopwDisplay;
        private Rtl_433_Processor Rtl_433Processor;
        private ClassInterfaceWithRtl433 ClassInterfaceWithRtl433;
        public Boolean enabledPlugin = false;
        private readonly ISharpControl control;
        private Boolean sourceIsFile = false;
        #endregion
        #region class Rtl_433_Panel
        internal Rtl_433_Panel(ISharpControl control)
        {
            InitializeComponent();
            this.control = control;
            InitDisplayParam();  //before initControls
            ClassUtils.MaxLinesConsole = 1000;             // for displayParam to initControls before init Plugin
            InitVirtualListView();
            InitControls();
            EnabledDisabledAllControls(false);
            buttonCu8ToWav.Enabled = true;   //decal plugin first click
 
            //with 1920 version move scrollbarre at the first click when the plugin is anchored ???
            //same if disabled with the designer
           radioButtonSnone.Enabled = true;

#if TESTWINDOWS
            MessageBox.Show("Version de test");
#endif
        }
        #region  listViewConsole 
        private void InitVirtualListView()
        {
            typeof(Control).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, listViewConsole, new object[] { true });
            this.SuspendLayout();
            SDRSharp.Rtl_433.ClassFunctionsVirtualListView.InitListView(listViewConsole);
            listViewConsole.GridLines = false;
            listViewConsole.FullRowSelect = false;
            listViewConsole.View = View.Details;   //hide column header
            listViewConsole.MultiSelect = true;
            listViewConsole.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(ListViewConsole_RetrieveVirtualItem);
            this.ResumeLayout(true);
        }
        internal void ListViewConsole_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
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
                    Debug.WriteLine(ex.Message, "Error fct(listViewConsole_RetrieveVirtualItem)");
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
        private String memoLigne = "";
        internal Boolean WriteLine(String theLine)
        {
            Int32 cptLine = 0;
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
            Int32 lastWord = 0;
            if (newLigne[newLigne.Length - 1].Length > 0)
            {
                memoLigne = newLigne[newLigne.Length - 1];
                lastWord = 1;
            }
            for (Int32 i = 0; i < newLigne.Length - lastWord; i++)
            {
                if (newLigne[i].Length > 0)
                {
                    ListViewItem ligne = new ListViewItem(newLigne[i]);
                    cacheLignes.Add(ligne);
                    cptLine ++;
                }
            }
            listViewConsole.VirtualListSize += cptLine;
            if (listViewConsole.VirtualListSize > ClassUtils.MaxLinesConsole - 1)
            {
                listViewConsole.ForeColor = Color.Red;
                ListViewItem ligne = new ListViewItem("You have reached the maximum number of rows provided in the console(" + ClassUtils.MaxLinesConsole.ToString() + ")");
                cacheLignes.Add(ligne);
                ligne = new ListViewItem("if necessary you can increase it in SDRSharp.config(key RTL_433_plugin.maxLinesConsole");
                cacheLignes.Add(ligne);
                listViewConsole.VirtualListSize += 2;
                listViewConsole.Columns[0].Text = "Console RTL_433---nbLigne=" + ClassUtils.MaxLinesConsole.ToString() + "/" + ClassUtils.MaxLinesConsole.ToString();
                retour = true;
            }
            listViewConsole.Columns[0].Text = "Console RTL_433---nbLigne=" + listViewConsole.VirtualListSize.ToString() + "/" + ClassUtils.MaxLinesConsole.ToString();
            if (listViewConsole.VirtualListSize > 0)
            {
                var last = listViewConsole.Items[listViewConsole.VirtualListSize - 1];
                last.EnsureVisible();
            }
            listViewConsole.EndUpdate();
            this.ResumeLayout(true);
            return retour;

        }
        private void ButtonAllToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            String text = String.Empty;
            if (listViewConsole.Items.Count > 1)
            {
                for (Int32 item = 0; item < listViewConsole.Items.Count; item++)
                    text += listViewConsole.Items[item].Text + "\n";
                Clipboard.SetText(text);
            }
        }
        private void ButtonSelectToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            String text = String.Empty;
            ListView.SelectedIndexCollection col = listViewConsole.SelectedIndices;
            if (col.Count > 0)
            {
                foreach (Int32 item in col)
                    text += listViewConsole.Items[item].Text;
                Clipboard.SetText(text);
            }
        }
        private void MainTableLayoutPanel_SizeChanged(object sender, EventArgs e)
        {
            listViewConsole.Columns[0].Width = listViewConsole.Width;
            listBoxHideShowDevices.BackColor = mainTableLayoutPanel.BackColor;  //Put here Too early in initcontrols.
            listBoxHideShowDevices.ForeColor = mainTableLayoutPanel.ForeColor;  //
        }
        private void ButtonClearMessages_Click(object sender, EventArgs e)
        {
            ClearListViewConsole();
        }
        private void ClearListViewConsole()
        {
            listViewConsole.VirtualListSize = 0;
            listViewConsole.Columns[0].Text = "Console RTL_433---nbLigne=0" + "/" + ClassUtils.MaxLinesConsole.ToString();
            cacheLignes = null;
            listViewConsoleFull = false;
        }
        #endregion
        private void InitControls()
        {
#if DEBUG
            labelCycleTime.Visible = true;
            labelTime433.Visible = true;
            labelTimeDisplay.Visible = true;
            labelTimeCycle.Visible = true;
            labelTime433.Visible = true;
            labelTimeRtl433.Visible = true;
            labelTimeDisplayWindows.Visible = true;
            ToolTip ttlabelTimeCycle = new ToolTip();
            ttlabelTimeCycle.SetToolTip(labelTimeCycle, "Red if Time cycle > 1000ms., you risk losing messages(not if source=file)");
#else
            labelCycleTime.Visible = false;
            labelTime433.Visible = false;
            labelTimeDisplay.Visible = false;
            labelTimeCycle.Visible = false;
            labelTime433.Visible = false;
            labelTimeRtl433.Visible = false;
           labelTimeDisplayWindows.Visible = false;
#endif
            DisplayParam();

#if DEBUG && TESTSTARTWITHOUTRADIO
            //#endif
            //#if DEBUG && !TESTSTARTWITHOUTRADIO
            buttonStartStop.Text = "Start";
            buttonStartStop.Enabled = true;
            EnabledDisabledAllControls(true);
#else
            buttonStartStop.Text = "Wait";  //normal case
            buttonStartStop.Enabled = false;
#endif
            //radioButtonWav.Checked = true;
            ToolTip ttlabelFrequency = new ToolTip();
            ttlabelFrequency.SetToolTip(labelFrequency, "If Orange:F<300Mhz or F>1000Mhz.");
            ttlabelFrequency.AutoPopDelay = 10000;
            //ToolTip ttlabelSampleRate = new ToolTip();
            //ttlabelSampleRate.SetToolTip(labelSampleRate, "If Orange:Sample rate is not at 250000.");
            //ttlabelSampleRate.AutoPopDelay = 10000;

            radioButtonFreq43392.Checked = true;
            listBoxHideShowDevices.Visible = true;
            labelSampleRate.Text = control.RFBandwidth.ToString();
            listViewConsole.Columns[0].Text = "Console RTL_433---nbLigne=" + listViewConsole.VirtualListSize.ToString(); //  + "/" + maxLinesConsole.ToString(); maxLinesConsole not init here
            checkBoxEnabledPlugin.Text = "Enabled plugin (" + VERSION + ")";
        }
        private void EnabledDisabledAllControls(Boolean state)
        {
            EnabledDisabledControlsOnStart(state);
            this.SuspendLayout();
            radioButtonListDevices.Enabled = state;
            radioButtonListMessages.Enabled = state;
            radioButtonGraph.Enabled = state;
            buttonCu8ToWav.Enabled = state;
            buttonDisplayParam.Enabled = state;
            buttonClearMessages.Enabled = state;
            buttonAllToClipboard.Enabled = state;
            buttonSelectToClipboard.Enabled = state;
            this.ResumeLayout(true);
        }
        private void FreeRessources()
        {
             Stop(false);
            //first plugin.close call by  SDRSharp.MainForm
            if (myClassFormListMessages != null)
            {
                myClassFormListMessages.Close();
                myClassFormListMessages = null;
            }
            if (myClassFormDevices != null)
            {
                myClassFormDevices.Close();
                myClassFormDevices = null;
            }
            if (myClassFormDevicesList != null)
            {
                myClassFormDevicesList.Close();
                myClassFormDevicesList = null;
            }
            if (ClassInterfaceWithRtl433 != null)
            {
                ClassInterfaceWithRtl433.Dispose();
                ClassInterfaceWithRtl433 = null;
            }
            if (Rtl_433Processor != null)
            {
                Rtl_433Processor.Dispose();
                Rtl_433Processor = null;
            }
            if (stopwDisplay != null)
                stopwDisplay = null;
            GC.SuppressFinalize(this);
        }
        private void CheckBoxEnabledPlugin_CheckedChanged(object sender, EventArgs e)
        {
            //if start radio first, test _control.isplaying
            enabledPlugin = checkBoxEnabledPlugin.Checked;
            if (!enabledPlugin)
            {
                checkBoxEnabledPlugin.Text = "Enabled plugin (" + VERSION + ")";
                Debug.WriteLine("panel->FreeRessources");
                FreeRessources();
            }
            else
            {

                ClassInterfaceWithRtl433 = new ClassInterfaceWithRtl433(this);
                Rtl_433Processor = new Rtl_433_Processor(control, this, ClassInterfaceWithRtl433);
                Rtl_433Processor.SetSourceName();
                SetBinding();
                //not in setting 
#if !(DEBUG && TESTSTARTWITHOUTRADIO)
                radioIsStarted = control.IsPlaying;  //if enabled before radio is playing: normal case
#endif
                SetSampleRate(control.RFBandwidth);
                SetFrequency(control.Frequency);
                EnabledDisabledAllControls(enabledPlugin);
                if (radioIsStarted && enabledPlugin)
                {
                    buttonStartStop.Text = "Start";
                    buttonStartStop.Enabled = true;
                    //pluginStarted = false;
                }
                checkBoxEnabledPlugin.Text = "Disabled plugin";
                ClassUtils.BackColor = this.BackColor;
                ClassUtils.ForeColor = this.ForeColor;
                ClassUtils.Cursor = this.Cursor;
                ClassUtils.Font = this.Font;
                //ClassUtils.Wav = true;
                //ClassUtils.Raw = false;
            }
        }
#endregion
#region start
        private void SetBinding()
        {
            labelSampleRate.DataBindings.Clear();
            labelFrequency.DataBindings.Clear();
            labelSampleRate.DataBindings.Add("Text", ClassInterfaceWithRtl433, "SampleRateStr");
            labelFrequency.DataBindings.Add("Text", ClassInterfaceWithRtl433, "FrequencyStr");
#if DEBUG
            labelTimeCycle.Text = "0";
            labelTimeRtl433.Text = "0";
            labelTimeDisplay.Text = "0";
#endif
        }
#if DEBUG
        internal void SetTime(float timeCycleCumul, float timeForRtl433Cumul,float _timeDisplayCumul, Boolean sourceIsFile)
        {
            try    //for crash when close sdrsharp
            {
                labelTimeCycle.Text = ((int)timeCycleCumul).ToString() + " ms.";
                labelTimeRtl433.Text = ((int)timeForRtl433Cumul).ToString() + " ms.";
                //if (timeCycleCumul > 1000 && !sourceIsFile)
                //    labelTimeCycle.ForeColor = Color.Red;
                //else
                //    labelTimeCycle.ForeColor = labelSampleRate.ForeColor;
                labelTimeDisplay.Text = ((int)_timeDisplayCumul).ToString() + " ms.";
            }
            catch
            { }
        }
#endif
        internal void Start(Boolean senderRadio = false)
        {
            if (senderRadio)
            {
                radioIsStarted = true;
#if !(DEBUG && TESTSTARTWITHOUTRADIO)     //no change stat button start/stop: normal case
                if (enabledPlugin)
                {
                    buttonStartStop.Enabled = true;
                    buttonStartStop.Text = "Start";
                }
                else
                {
                    buttonStartStop.Enabled = false;
                    buttonStartStop.Text = "Wait";
                    EnabledDisabledControlsOnStart(true);
                }
                SetSampleRate(control.RFBandwidth);
                SetFrequency(control.Frequency);
#endif
            }
            else
            {
                if (stopwDisplay == null)
                    stopwDisplay = new Stopwatch();
                buttonStartStop.Text = "Stop";
                ClearListViewConsole();
                listViewConsole.ForeColor = this.ForeColor;
                ProcessParameterOnStart();  //call Rtl_433Processor.SampleRate--->decimation 
                EnabledDisabledControlsOnStart(false);
                List<String> ListDevicesSH = new List<String>();
                foreach (String device in listBoxHideShowDevices.SelectedItems)
                {
                    String[] part = device.Split(new char[] { '-' });
                    ListDevicesSH.Add(part[0]);
                }
                ClassInterfaceWithRtl433.SetHideOrShowDevices(ListDevicesSH, radioButtonHideSelect.Checked);
                // put this if option ClassInterfaceWithRtl433.SetShowDevicesDisabled(false);
                ClassInterfaceWithRtl433.Call_main_Rtl_433();
                Rtl_433Processor.Enabled = true;
                Rtl_433Processor.Start();   //call setSourceName() init nbByteForRtl433 and nbComplexForRtl
            }
        }
        private void EnabledDisabledControlsOnStart(Boolean state)
        {
            this.SuspendLayout();
            
            radioButtonFreqFree.Enabled = state; //try for version from 1830 text disabled  black(no visible)
            radioButtonFreq315.Enabled = state;
            radioButtonFreq345.Enabled = state;
            radioButtonFreq43392.Enabled = state;
            radioButtonFreq868.Enabled = state;
            radioButtonFreq915.Enabled = state;
            radioButtonSnone.Enabled = state;
            radioButtonSknown.Enabled = state;
            radioButtonSunknown.Enabled = state;
            radioButtonSall.Enabled = state;
            radioButtonHideSelect.Enabled = state;
            radioButtonShowSelect.Enabled = state;
            radioButtonDataConvCustomary.Enabled = state;
            radioButtonDataConvNative.Enabled = state;
            radioButtonDataConvSI.Enabled = state;
            this.ResumeLayout(true);
        }
        private void ProcessParameterOnStart()
        {
            frequency = GetFrequency();
            Rtl_433Processor.FrequencyRtl433 = frequency;
            Rtl_433Processor.SampleRate = control.RFBandwidth;
            Rtl_433Processor.SetSourceName();
            //data Convert
            GetDataConv();
            //metaData
            ClassInterfaceWithRtl433.SetOption("MbitsOrLevel", "-Mlevel");
            //save cu8
            GetRadioButtonSaveCu8();
            //hide show devices To Start
        }
        private void ButtonStartStop_Click(object sender, EventArgs e)
        {
            ButtonStartStop();
        }
        internal void ButtonStartStop(Boolean disabledPlugin = false)
        {
            if (buttonStartStop.Text == "Start" || disabledPlugin)
            {
                InitPanel();
                ClassInterfaceWithRtl433.StartSendData();  //only by button

                if (radioButtonListDevices.Checked && myClassFormDevicesList == null)
                {
                    myClassFormDevicesList = new ClassFormDevicesList(this);
                    myClassFormDevicesList.ChooseFormListDevice = true;
                    shownFormDeviceList = true;
                    //myClassFormDevicesList.    ChooseformDevicesList = true;
                }
                Start();
            }
            else
                Stop(disabledPlugin);
        }
        internal float TimeDisplay { get; set; }
#endregion
#region stop
        /// <summary>
        /// stop and clean
        /// </summary>
        /// <param name="disabledPlugin">if true:end processor</param>
        /// <param name="senderRadio"></param>
        ///from stop radio.NotifyPropertyChangedHandler:disabledPlugin=false,senderRadio=true
        ///from plugin.close:disabledPlugin=true,senderRadio=false
        ///from disposePanel:disabledPlugin=true,senderRadio=false
        ///from buttonStartStop_Click and != start:disabledPlugin=false,senderRadio=false
        internal void Stop(Boolean senderRadio = false)
        {
#if (DEBUG && TESTSTARTWITHOUTRADIO)
            return;
#else
            if (ClassInterfaceWithRtl433 != null)
                ClassInterfaceWithRtl433.StopSendDataToRtl433();
            //if (myClassFormDevices != null)
            //    myClassFormDevices.Stop();
            if (Rtl_433Processor != null)
                Rtl_433Processor.Stop();

            if (senderRadio)                   //normal case
            {
                radioIsStarted = false;
                buttonStartStop.Text = "Wait";
                buttonStartStop.Enabled = false;
                EnabledDisabledControlsOnStart(true);    //stop by radio
            }
            else
            {
                if (radioIsStarted)
                {
                    buttonStartStop.Text = "Start";
                    buttonStartStop.Enabled = enabledPlugin;
//#if !TESTBOUCLEREPLAYMARC
//                    enabledDisabledControlsOnStart(false);                    //voir false for TESTBOUCLEREPLAYMARC
//#else
                    EnabledDisabledControlsOnStart(true);                    //voir false for TESTBOUCLEREPLAYMARC
//#endif
                }                             //normal case
                else
                {
                    buttonStartStop.Text = "Wait";
                    buttonStartStop.Enabled = false;
                    EnabledDisabledControlsOnStart(true);
                }
            }
#endif
        }
#endregion
#region internal functions

        public Boolean PluginIsRun
        {
            get {
                if(buttonStartStop.Text == "Stop")
                    return true;
                else
                    return false;
            }

        }


        internal void SetSampleRate(double SampleRate)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)delegate
                {
                    SetSampleRate(SampleRate);
                });
            }
            else
            {
                labelSampleRate.Text = SampleRate.ToString();

                //if (SampleRate > 250000)
                //    labelSampleRate.ForeColor = Color.Orange;
                //else
                //    labelSampleRate.ForeColor = labelSampleRateTxt.ForeColor;
            }
        }
        internal void SetMessage(String message)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)delegate
                {
                    SetMessage(message);
                });
            }
            else
            {
                if (!listViewConsoleFull)
                    listViewConsoleFull = WriteLine(message);
            }
        }
        internal void SetListDevices(List<String> listeDevice)
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)delegate
                {
                    SetListDevices(listeDevice);
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
        //private String sourceName = "";
        internal void SetSourceType(Boolean sourceIsFile)
        {
            //this.sourceName = sourceName;
            this.sourceIsFile = sourceIsFile;
        }
#endregion
#region private functions
        String listInfos;
        private void InitDisplayParam()
        {
            listInfos = "Parameters configure source\n" +
            "   -Sampling mode->quadrature sampling\n" +
            "   -Sample Rate->0.25 MSPS or more for certain devices FSK or f > 433Mhz...\n" +
            "   -AGC:on(corresponds to auto gain with rtl433) can be manually->off.\n" +
            "   -RTL AGC:on.(not the AGC panel) can be set off if good signals.\n" +
            "   -Check frequency\n\n" ;

        }
        private void DisplayParam()
        {
            if (!listViewConsoleFull)
                listViewConsoleFull = WriteLine(listInfos);
        }

#endregion
#region event panel control
        private void ButtonDisplayParam_Click(object sender, EventArgs e)
        {
            DisplayParam();
        }
        private void ButtonCu8ToWav_Click(object sender, EventArgs e)
        {
            Int32 cptPb = 0;
#if !TESTRECURSIF && !TESTBATCH
            using (OpenFileDialog openCu8 = new OpenFileDialog())
            {
                openCu8.DefaultExt = "cu8";
                openCu8.Filter = "cu8 files|*.cu8";
                openCu8.Multiselect = true;

                if (openCu8.ShowDialog() == DialogResult.OK)
                {
                    foreach (String file in openCu8.FileNames)
                    {
                        Int32 sampleRate = ClassUtils.ConvertCu8ToWav(file);
                        if (sampleRate == -1)
                        {
                            MessageBox.Show("No sample rate detected in the file name: _sample rate+k " + file, "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cptPb ++;
                        }
                    }
                    if (cptPb == 0)
                        MessageBox.Show("Translate is completed", "Translate cu8 to wav", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Translate is NOT completed  " + (openCu8.FileNames.Length - cptPb).ToString() + "/" + openCu8.FileNames.Length.ToString() + " ok files", "Translate cu8 to wav", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                openCu8.Dispose();
            }
#elif TESTRECURSIF && !TESTBATCH
            try
            {
                //Set a variable to the My Documents path.
                string srcPath = "C:\\marc\\tnt\\fichiers_cu8_et_wav\\fichiers_cu8\\rtl_433_tests-master\\tests";   // Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string dstPath = "C:\\marc\\tnt\\fichiers_cu8_et_wav\\fichiers_cu8\\rtl_433_tests-master\\rtl_433_tests-master";
                Int32 lenPath = srcPath.Length+1;
                var files = from file in Directory.EnumerateFiles(srcPath, "*.cu8", SearchOption.AllDirectories)
                            //from line in File.ReadLines(file)
                            //where line.Contains(".cu8")
                            select new
                            {
                                File = file,
                                //Line = line
                            };
                String memoDirectory = "";
                Int32 cptFile = 0;
                foreach (var f in files)
                {
                    try
                    {
                        String directory = Path.GetDirectoryName(f.File);
                        if (!(memoDirectory == directory))
                        {

                            String newFile = directory.Substring(lenPath).Replace("\\", "_") + "_" + Path.GetFileName($"{f.File}");
                            Debug.WriteLine(newFile);
                            Debug.WriteLine(dstPath + "\\" + newFile);

                            File.Copy($"{f.File}", dstPath + "\\" + newFile);
                            memoDirectory = directory;
                            cptFile ++;
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }
                Debug.WriteLine(cptFile.ToString());


            }
            catch (UnauthorizedAccessException uAEx)
            {
                Debug.WriteLine(uAEx.Message);
            }
            catch (PathTooLongException pathEx)
            {
                Debug.WriteLine(pathEx.Message);
            }
            if (cptPb == 0)
                MessageBox.Show("Translate is completed", "Translate cu8 to wav", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Translate is NOT completed", "Translate cu8 to wav", MessageBoxButtons.OK, MessageBoxIcon.Warning);
#elif TESTBATCH

            //generate batch file for replay with RTL_433 console mode->dstFile
            //put srcPath,dstFile 
            //and PathRtl433_EXE where you are compiled RTL_433
            //run SDRSharp, enabled plugin and cu8 to Wav
            //open Console to PathRtl433_EXE
            //run AllFiles.bat

            string srcPath = "C:\\marc\\tnt\\fichiers_cu8_et_wav\\fichiers_cu8\\rtl_433_tests-master\\rtl_433_tests-master";   // Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string dstFile = "C:\\marc\\tnt\\rtl_433\\rtl_433-master_06052024\\vs17_32\\Debug\\FilesRTL433AllOK.bat ";
            var files = from file in Directory.EnumerateFiles(srcPath, "*.cu8", SearchOption.AllDirectories)
                        select new
                        {
                            File = file,
                        };
            Int32 cptFile = 0;
            try
            {
                using (Stream stream = new FileStream(dstFile, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    String Line = "";
                    String PathRtl433_EXE = "C:\\marc\\tnt\\rtl_433\\rtl_433-master_06052024\\vs17_32\\Debug\\rtl_433";
                    
                            StreamWriter str = new StreamWriter(stream);
                    str.WriteLine("cls");
                    str.WriteLine("REM: "+DateTime.Now);
                    foreach (var file in files)
                    {
                        Int32 sampleRate = 0;
                        Int32 sampleRateFromFileName = wavRecorder.getSampleRateFromName(file.File); //lacrosse_g2750_915M_1000k.cu8,9_ford-unlock002.cu8
                        if (sampleRateFromFileName == -1)
                        {
                            sampleRateFromFileName = 250;
                            sampleRate = 250000;
                            //MessageBox.Show("No sample rate detected in the file name", "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //return -1;
                        }
                        else
                            sampleRate = sampleRateFromFileName;

                        String Option = " -s " + sampleRate.ToString() + " -C si -r ";

                        Line = PathRtl433_EXE + Option + file.File;
                        str.WriteLine(Line);
                        cptFile ++;
                        if(cptFile%20==0)
                            str.WriteLine("Pause");
                    }
                    str.Flush();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            MessageBox.Show("Copyfile is completed for "+ cptFile.ToString() + " files", "Translate cu8 nameFile with options to batch file", MessageBoxButtons.OK, MessageBoxIcon.Information);
#endif
        }

        private void RadioButtonTypeWindow_CheckedChanged(object sender, EventArgs e)
        {
            SetRadioButtonTypeWindow();
        }

        private void InitPanel()
        {
            SetRadioButtonTypeWindow();
            //SetRadioButtonRaw();
            //SetRecordText = checkBoxRecordTxtFile.Checked;
        }

        private void SetRadioButtonTypeWindow()
        {
           if (checkBoxEnabledPlugin.Checked)
            {
                SetSelectFormGraph = radioButtonGraph.Checked;
                ClassInterfaceWithRtl433.SetSelectFormGraph = radioButtonGraph.Checked;
                //SetSelectFormListMessages = radioButtonListMessages.Checked;
                SetSelectFormListDevice = radioButtonListDevices.Checked;
                if (myClassFormDevicesList != null)
                    myClassFormDevicesList.ChooseFormListDevice = radioButtonListDevices.Checked;
            }
        }


        //private void RadioButtonRaw_CheckedChanged(object sender, EventArgs e)
        //{
        //    SetRadioButtonRaw();
        //}
        //private void SetRadioButtonRaw ()
        //{
        //    //setWav=radioButtonWav.Checked; 
        //    //setRaw=radioButtonRaw.Checked;
        //    ClassUtils.Wav=true; //wav and raw exclusif
        //    ClassUtils.Raw=false;
        //}
        //private void CheckBoxRecordTxtFile_CheckedChanged(object sender, EventArgs e)
        //{
        //    SetRecordText = checkBoxRecordTxtFile.Checked;
        //}
#endregion
    }
}
