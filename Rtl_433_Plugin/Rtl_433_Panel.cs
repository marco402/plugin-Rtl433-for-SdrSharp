
/* Written by Marc Prieur (marco40_github@sfr.fr)
                                Rtl_433_Panel.cs 
                            project Rtl_433_Plugin
						         Plugin for SdrSharp
 **************************************************************************************
 Creative Commons Attrib Share-Alike License
 You are free to use/extend this library but please abide with the CC-BY-SA license:
 Attribution-NonCommercial-ShareAlike 4.0 International License
 http://creativecommons.org/licenses/by-nc-sa/4.0/

//History : V1.00 2021-04-01 - First release
//          V1.10 2021-20-April

 All text above must be included in any redistribution.
  **********************************************************************************/

using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
//using System.Threading;
using System.Diagnostics;

namespace SDRSharp.Rtl_433
{
    public partial class Rtl_433_Panel : UserControl
    {
#region private
        //private struct structFormDevices
        //{
        //    public string key;
        //    public bool displayGraph;
        //}
        private OpenFileDialog openCu8;
        private Dictionary<String,FormDevices> listformDevice ; 
        private Rtl_433_Processor _Rtl_433Processor;
        private ClassInterfaceWithRtl433 _ClassInterfaceWithRtl433;
        private void displayParam()
        {
        richTextBoxMessages.Clear();
        richTextBoxMessages.AppendText("Parameters configure source\n");
        richTextBoxMessages.AppendText("sampling mode->\nquadrature sampling\n");
        richTextBoxMessages.AppendText("Preferred Sample Rate->\n0.25 MSPS, imposed if record .wav\n");
        richTextBoxMessages.AppendText("RTL AGC->on(not panel AGC)\n");
        richTextBoxMessages.AppendText("Tuner AGC->on(not panel AGC)\n\n");
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
            _ClassInterfaceWithRtl433.get_version_dll_rtl_433();
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
            _Rtl_433Processor.SetClassInterfaceWithRtl433(_ClassInterfaceWithRtl433);
            ToolTip OptionStereo = new ToolTip();
            OptionStereo.SetToolTip(checkBoxSTEREO, "Record IQ to wav file for reload with SDRSharp");
            ToolTip OptionMono = new ToolTip();
            OptionMono.SetToolTip(checkBoxMONO, "Record module de IQ to wav file for display with Audacity or another viewer");
            radioButtonFreq43392.Checked = true;
            listBoxHideDevices.Visible = true;
            richTextBoxMessages.MaxLength = 5000;
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
                richTextBoxMessages.AppendText(message + "\n");
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

        //private int cptDevicesForTest = 0;   //test device windows always ok until 143 ~ 1.3G of memory
        public void addFormDevice(Dictionary<String, String> listData, List<PointF>[] points,string[] nameGraph)     
        {
            if (base.InvokeRequired)
            {
                base.BeginInvoke((Action)delegate
                {
                    addFormDevice(listData,points,nameGraph);
                });
            }
            else
            {
                string key = "";
                string model = "";
                string protocol = "";
                string channel = "";
                foreach (KeyValuePair<string, string> _line in listData)
                {
                    if (_line.Key.ToUpper().Contains("CHANNEL") & channel=="" & _line.Value != "")
                    {
                        channel = _line.Value;
                        key += (" Channel:" + channel);
                    }
                    else if (_line.Key.ToUpper().Contains("PROTOCOL") & protocol == "" & _line.Value != "")  //protect humidity contain id
                    {
                        protocol = _line.Value;
                        key += " Protocol:" + protocol;
                    }
                    else if (_line.Key.ToUpper().Contains("MODEL") & model=="" & _line.Value != "")
                    {
                        model = _line.Value;
                        key +=" Model: " + model;
                    }
                }
                if (key != "")
                {
                    //key += cptDevicesForTest.ToString();   //test device windows
                    //cptDevicesForTest += 1;                //test device windows
                        if (recordDevice & key == nameToRecord)
                        {
                            recordDevice = false;
                            _ClassInterfaceWithRtl433.recordDevice(key);
                        }
                        lock (listformDevice)
                        {
                            if (!listformDevice.ContainsKey(key))
                            {
                            if (listformDevice.Count > _MaxDevicesWindows)
                                return;
                             listformDevice.Add(key, new FormDevices(this));
                            if(listformDevice.Count <_nbDevicesWithGraph)
                                listformDevice[key].displayGraph = true;
                            }
                        }
                        listformDevice[key].Text = key;
                        listformDevice[key].setInfoDevice(listData);
                        listformDevice[key].Visible = true;
                        listformDevice[key].Show();
                        listformDevice[key].resetLabelRecord();  //after le load for memo...
                        //if (listformDevice.Count < _nbDevicesWithGraph)
                        if(listformDevice[key].displayGraph)
                            listformDevice[key].setDataGraph(points, nameGraph); 
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
                List<string> HideDevices = new List<string>();
                foreach (string device in listBoxHideDevices.SelectedItems)
                {
                    string[] part = device.Split(new char[] { '-' });
                    HideDevices.Add(part[0]);
                }
                _ClassInterfaceWithRtl433.setHideDevices(HideDevices);
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
        //private bool displayCurves = false;
        //public void setDisplayCurves(string name, bool choice)
        //{
        //    if (choice)
        //    {
        //        foreach (KeyValuePair<string, FormDevices> _form in listformDevice)
        //        {
        //            if (_form.Key != name)
        //                listformDevice[_form.Key].resetDisplayCurves();
        //        }
        //        nameToRecord = name;
        //    }
        //    displayCurves = choice;
        //}
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
        #endregion
    }
}
