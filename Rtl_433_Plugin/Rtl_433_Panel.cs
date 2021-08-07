
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
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
//using System.Threading;
using System.Diagnostics;
using System.IO;
using GraphLib;
//#if NOTESTFORMLISTMESSAGES
//                    addFormDevice(listData, points, nameGraph);
//#else
//addFormDeviceListMessages(listData);
//#endif
namespace SDRSharp.Rtl_433
{
    public partial class Rtl_433_Panel : UserControl
    {
        #region private
        private const int NBCOLUMN = 100;
        private enum TYPEFORM :Int32
        {
            LISTDEV,
            GRAPH,
            LISTMES
        }
        private const String FILELISTEDEVICES = "devices.txt";
        private OpenFileDialog openCu8;
        private Dictionary<String,FormDevices> listformDevice ;
        private Dictionary<String, FormDevicesListMessages> listformDeviceListMessages;
        private Rtl_433_Processor _Rtl_433Processor;
        private ClassInterfaceWithRtl433 _ClassInterfaceWithRtl433;
        private FormListDevices formListDevice = null;
        private void displayParam()
        {
        richTextBoxMessages.Clear();
        richTextBoxMessages.AppendText("Parameters configure source\n");
        richTextBoxMessages.AppendText("sampling mode->\nquadrature sampling\n");
        richTextBoxMessages.AppendText("Preferred Sample Rate->\n0.25 MSPS, imposed if record .wav\n");
        richTextBoxMessages.AppendText("Tuner AGC:on(corresponds to auto gain with rtl433) can be manually-> off.\n");
        richTextBoxMessages.AppendText("RTL AGC:on.(not the AGC panel) can be set off if good signals.\n");
        }
        #endregion
        #region public functions
        public Rtl_433_Panel( Rtl_433_Processor rtl_433Processor)
        {
            InitializeComponent();
            this.openCu8 = new OpenFileDialog();
            this.openCu8.DefaultExt = "cu8";
            this.openCu8.Filter = "cu8 files|*.cu8";
            _Rtl_433Processor = rtl_433Processor;
            _ClassInterfaceWithRtl433 = new ClassInterfaceWithRtl433(this);
            _ClassInterfaceWithRtl433.Version = string.Empty;
#if MSGBOXDEBUG
            _ClassInterfaceWithRtl433.get_version_dll_rtl_433();
            Utilities.getVersion();
#endif
            labelVersion.DataBindings.Add("Text", _ClassInterfaceWithRtl433, "Version");
            labelSampleRate.DataBindings.Add("Text", _ClassInterfaceWithRtl433, "SampleRateStr");
            //labelTimeRtl433.DataBindings.Add("Text", _ClassInterfaceWithRtl433, "timeForRtl433");
            //labelTimeCycle.DataBindings.Add("Text", _ClassInterfaceWithRtl433, "timeCycle");
            labelFrequency.DataBindings.Add("Text", _ClassInterfaceWithRtl433, "frequency");
            labelCenterFrequency.DataBindings.Add("Text", _ClassInterfaceWithRtl433, "centerFrequency");
            displayParam();
            //this.buttonStartStop.TextBoxElement.TextBoxItem.TextBoxControl.Cursor = Cursors.Arrow;
            //this.buttonStartStop.RootElement.UseDefaultDisabledPaint = false;
            //radioButtonMbits.Visible = false;  //not useful
            buttonStartStop.Text = "Start";

            buttonStartStop.Enabled = false;
            listformDevice = new Dictionary<string, FormDevices>() ;
            listformDeviceListMessages = new Dictionary<string, FormDevicesListMessages>() ;
            _Rtl_433Processor.SetClassInterfaceWithRtl433(_ClassInterfaceWithRtl433);
            ToolTip OptionStereo = new ToolTip();
            OptionStereo.SetToolTip(checkBoxSTEREO, "Record IQ to wav file for reload with SDRSharp");
            ToolTip OptionMono = new ToolTip();
            OptionMono.SetToolTip(checkBoxMONO, "Record module de IQ to wav file for display with Audacity or another viewer");
            radioButtonFreq43392.Checked = true;
            listBoxHideDevices.Visible = true;
            richTextBoxMessages.MaxLength = 5000;
#if TESTWINDOWS
            MessageBox.Show("Version de test");
#endif
        }
        public void setSampleRate(double SampleRate)
        {
            if (base.InvokeRequired)
            {
                base.BeginInvoke((Action)delegate
                {
                    setSampleRate(SampleRate);
                });
            }
            else
            {
                labelSampleRate.Text = "Sample rate: " + SampleRate.ToString();
            }
        }
        public void setMessage(String message)
        {
            if (base.InvokeRequired)
            {
                base.BeginInvoke((Action)delegate
                {
                    setMessage(message);
                });
            }
            else
            {
                richTextBoxMessages.AppendText(message + "\n");
            }
        }
        private bool onlyOneCall = false;
        public void setListDevices(List<string> listeDevice)
        {
            if (base.InvokeRequired)
            {
                base.BeginInvoke((Action)delegate
                {
                    setListDevices(listeDevice);
                });
            }
            else
            {
                this.SuspendLayout();
                foreach (string device in listeDevice)
                {
                    listBoxHideDevices.Items.Add(device);
                }
                this.ResumeLayout();
            }
        }
        public string getDeviceName(Dictionary<String, String> listData)
        {
            string key = "";
            string model = "";
            string protocol = "";
            string channel = "";
            foreach (KeyValuePair<string, string> _line in listData)
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
            }
#if TESTWINDOWS
            if (key.Trim() != "")   //test device windows
                key += cptDevicesForTest.ToString();   //test device windows
#endif
            return key;
        }

        TYPEFORM displayTypeForm = TYPEFORM.LISTMES;
#if TESTWINDOWS
        private int cptDevicesForTest = 0;   //test device windows always ok until 143 ~ 1.3G of memory
#endif
        public void addFormDevice(Dictionary<String, String> listData, List<PointF>[] points, string[] nameGraph)
        {
            if (base.InvokeRequired)
            {
                base.BeginInvoke((Action)delegate
                {
                    addFormDevice(listData, points, nameGraph);
                });
            }
            else
            {
                string deviceName = getDeviceName(listData);
                if (deviceName.Trim() != "")
                {
#if TESTWINDOWS
                    cptDevicesForTest += 1;                //test device windows
#endif
                    if (displayTypeForm==TYPEFORM.GRAPH)
                    {
                        if (recordDevice & deviceName == nameToRecord)
                        {
                            recordDevice = false;
                            _ClassInterfaceWithRtl433.recordDevice(deviceName);
                        }
                        lock (listformDevice)
                        {
                            if (!listformDevice.ContainsKey(deviceName))
                            {
                                if (listformDevice.Count > _MaxDevicesWindows-1)
                                    return;
                                listformDevice.Add(deviceName, new FormDevices(this));
                                if (listformDevice.Count < _nbDevicesWithGraph)
                                    listformDevice[deviceName].displayGraph = true;
                                listformDevice[deviceName].Text = deviceName;
                                listformDevice[deviceName].Visible = true;
                                listformDevice[deviceName].Show();
                                listformDevice[deviceName].resetLabelRecord();  //after le load for memo...
                                                                    //if (listformDevice.Count < _nbDevicesWithGraph
                            }
                        }
                        
                        listformDevice[deviceName].setInfoDevice(listData);
                    
                        if (listformDevice[deviceName].displayGraph)
                            listformDevice[deviceName].setDataGraph(points, nameGraph);
                    }
                    else if(displayTypeForm == TYPEFORM.LISTDEV)
                    {
                        if (formListDevice == null)
                        {
                            formListDevice = new FormListDevices(this, _MaxDevicesWindows*10, NBCOLUMN);
                            formListDevice.Show();
                        }
                        formListDevice.setInfoDevice(listData);
                    }
                    else  //TYPEFORM.LISTMES
                    {
                        //if (recordDevice & deviceName == nameToRecord)
                        //{
                        //    recordDevice = false;
                        //    _ClassInterfaceWithRtl433.recordDevice(deviceName);
                        //}
                        //if (FormDevicesListMessages == null)
                        //{
                        //    FormDevicesListMessages = new FormDevicesListMessages(this, _MaxDevicesWindows * 10, NBCOLUMN);
                        //    FormDevicesListMessages.Show();
                        //}
                        lock (listformDeviceListMessages)
                        {
                            if (!listformDeviceListMessages.ContainsKey(deviceName))
                            {
                                if (listformDeviceListMessages.Count > _MaxDevicesWindows - 1)
                                    return;
                                if (radioButtonMLevel.Checked)
                                    listformDeviceListMessages.Add(deviceName, new FormDevicesListMessages(this, _MaxDevicesWindows*10, listData.Count + 1 +2 , deviceName, _ClassInterfaceWithRtl433)); //+2 for debug
                                else
                                    listformDeviceListMessages.Add(deviceName, new FormDevicesListMessages(this, _MaxDevicesWindows * 10, listData.Count + 1 + 5 +2 , deviceName, _ClassInterfaceWithRtl433));  //5 for -mMevel //+2 for debug
                                listformDeviceListMessages[deviceName].Text = deviceName;
                                listformDeviceListMessages[deviceName].Visible = true;
                                listformDeviceListMessages[deviceName].Show();
                             }
                        }
         
                        listformDeviceListMessages[deviceName].setMessages(listData);

                        //listformDeviceListMessages[deviceName].resetLabelRecord();  //after le load for memo...
                                                                                    //if (listformDeviceListMessages.Count < _nbDevicesWithGraph)

                    }
                }
                else
                {
                    foreach (KeyValuePair<string, string> _line in listData)
                    {
                        Console.Write(_line.Key);
                        Console.WriteLine("  " + _line.Value);
                    }
                }
            }
        }
        public void Start(bool senderRadio = false)
        {
            if (senderRadio)
            {
                buttonStartStop.Enabled = true;
                buttonStartStop.Text = "Start";
            }
            else
            {
                buttonStartStop.Text = "Stop";
                if (!onlyOneCall)
                {
                    listBoxHideDevices.Visible = true;
                    //_ClassInterfaceWithRtl433.get_version_dll_rtl_433();
                    richTextBoxMessages.Clear();

                    //_ClassInterfaceWithRtl433.timeCycle = "Cycle time: 0";
                    //_ClassInterfaceWithRtl433.timeForRtl433 = "Cycle time Rtl433: 0";
                    onlyOneCall = true;
                }
                _Rtl_433Processor.openConsole();
                enabledDisabledControlsOnStart(false);
                List<string> ListDevicesSH = new List<string>();
                foreach (string device in listBoxHideDevices.SelectedItems)
                {
                    string[] part = device.Split(new char[] { '-' });
                    ListDevicesSH.Add(part[0]);
                }
                _ClassInterfaceWithRtl433.setHideOrShowDevices(ListDevicesSH, radioButtonHideSelect.Checked);
                _ClassInterfaceWithRtl433.call_main_Rtl_433();
            }
        }
        public void Stop(bool senderRadio = false)
        {
            buttonStartStop.Text = "Start";
            _ClassInterfaceWithRtl433.stopSdr();
            enabledDisabledControlsOnStart(true);
            if (senderRadio)
                buttonStartStop.Enabled = false;
        }
        private void enabledDisabledControlsOnStart(bool state)
        {
            groupBoxVerbose.Enabled = state;
            groupBoxMetadata.Enabled = state;
            groupBoxRecord.Enabled = state;
            groupBoxDataConv.Enabled = state;
            listBoxHideDevices.Enabled = state;
            _Rtl_433Processor.Enabled = !state;
            _Rtl_433Processor.EnableRtl433 = !state;
        }
        public void saveDevicesList()
        {
            if (formListDevice != null )
            {
                DialogResult dialogResult = MessageBox.Show("Do you want export devices list(devices.txt)", "Export devices list", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    string directory = _ClassInterfaceWithRtl433.getDirectoryRecording();
                    formListDevice.serializeText(directory + FILELISTEDEVICES);
                }
             }
        }
#endregion
#region callBack from devices form
        public void closingOneFormDevice(String key)
        {
            lock (listformDevice)
            {
                if (listformDevice.ContainsKey(key))
                    listformDevice.Remove(key);
            }
        }
        public void closingOneFormDeviceListMessages(String key)
        {
            lock (listformDeviceListMessages)
            {
                if (listformDeviceListMessages.ContainsKey(key))
                    listformDeviceListMessages.Remove(key);
            }
        }
        public void closingFormListDevice()
        {
            saveDevicesList();
            formListDevice = null;
        }
        private bool recordDevice = false;
        private string nameToRecord = "";
        public bool setRecordDevice(string name, bool choice)
        {
            if (choice)
            {
                if ((_ClassInterfaceWithRtl433.RecordMONO == false & _ClassInterfaceWithRtl433.RecordSTEREO == false) || _ClassInterfaceWithRtl433.SampleRateDbl > 250000.0)
                {
                    MessageBox.Show("Choice MONO/STEREO or record only to 0.25 MSPS->use -S option", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                else
                {
                    foreach (KeyValuePair<string, FormDevices> _form in listformDevice)
                    {                         if (_form.Key != name)
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
                return true;
            }
        }
#endregion
#region event panel control
        private void buttonDisplayParam_Click(object sender, EventArgs e)
        {
            displayParam();
        }
        private void richTextBoxMessages_TextChanged(object sender, EventArgs e)
        {
            richTextBoxMessages.SelectionStart = richTextBoxMessages.Text.Length;
            richTextBoxMessages.ScrollToCaret();
        }
        private void buttonClearMessages_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            richTextBoxMessages.Clear();
            _ClassInterfaceWithRtl433.CleartimeCycleMax();
            this.ResumeLayout();
        }
        private void checkBoxMONO_CheckedChanged(object sender, EventArgs e)
        {
            _ClassInterfaceWithRtl433.RecordMONO=checkBoxMONO.Checked;
        }
        private void checkBoxSTEREO_CheckedChanged(object sender, EventArgs e)
        {
            _ClassInterfaceWithRtl433.RecordSTEREO=checkBoxSTEREO.Checked;

        }
        private void buttonCu8ToWav_Click(object sender, EventArgs e)
        {
            if (_ClassInterfaceWithRtl433.RecordMONO == false & _ClassInterfaceWithRtl433.RecordSTEREO == false)
                MessageBox.Show("Choice MONO / STEREO", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (this.openCu8.ShowDialog() == DialogResult.OK)
                {
                     wavRecorder.convertCu8ToWav(openCu8.FileName, _ClassInterfaceWithRtl433.RecordMONO, _ClassInterfaceWithRtl433.RecordSTEREO,1);
                }
            }
        }
        private void buttonStartStop_Click(object sender, EventArgs e)
        {
            if (buttonStartStop.Text == "Start")
            {
                Start();
                _ClassInterfaceWithRtl433.startSendData();  //only by button
            }
            else
                Stop();
        }
        
        private void radioButtonListDevices_CheckedChanged(object sender, EventArgs e)
        {
            //displayListDevices = radioButtonListDevices.Checked;
            if (radioButtonListDevices.Checked)
            {
                displayTypeForm = TYPEFORM.LISTDEV;
                string directory = _ClassInterfaceWithRtl433.getDirectoryRecording();
                if (formListDevice == null && File.Exists(directory + FILELISTEDEVICES))
                {
                    formListDevice = new FormListDevices(this, _MaxDevicesWindows * 10, NBCOLUMN);
                    formListDevice.Show();
                    DialogResult dialogResult = MessageBox.Show("Do you want import devices list", "Import devices list", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {

                        formListDevice.deSerializeText(directory + FILELISTEDEVICES);
                    }
                }
            }
            else if (radioButtonGraph.Checked)
            {
                displayTypeForm = TYPEFORM.GRAPH;
            }
           else  //TYPEFORM.LISTMES
            {
                displayTypeForm = TYPEFORM.LISTMES;
            }
        }
#endregion
    }
}
