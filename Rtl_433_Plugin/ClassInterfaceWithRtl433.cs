
/* Written by Marc Prieur (marco40_github@sfr.fr)
                                ClassInterfaceWithRtl433.cs 
                            project Rtl_433_Plugin
						         Plugin for SdrSharp
 **************************************************************************************
 Creative Commons Attrib Share-Alike License
 You are free to use/extend this library but please abide with the CC-BY-SA license:
 Attribution-NonCommercial-ShareAlike 4.0 International License
 http://creativecommons.org/licenses/by-nc-sa/4.0/

History : V1.00 2021-04-01 - First release
          V1.10 2021-20-April

 All text above must be included in any redistribution.
  **********************************************************************************/

using System;
using System.Collections.Generic;
using System.Timers;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Linq;
using SDRSharp.Radio;
using System.IO;
using System.Text;
using System.Drawing;

namespace SDRSharp.Rtl_433
{
   public unsafe class ClassInterfaceWithRtl433 : INotifyPropertyChanged
    {
        public enum SAVEDEVICE{none,all,known,unknown};
        public event PropertyChangedEventHandler PropertyChanged;
        private byte[] dataForRs433;
        private Complex* _copyIQPtr;
        private UnsafeBuffer _copyIQBuffer;
        private Stopwatch stopw;
        private long memoDt;
        private Dictionary<String, String> listOptionsRtl433;
        public ClassInterfaceWithRtl433(Rtl_433_Panel owner)
        {
            _owner = owner;
            stopw = new Stopwatch();
            listData = new Dictionary<String, String>();
            SampleRate = "Sample rate: 0";  //no display if init to Rtl_433_panel
            setTime();
            listOptionsRtl433 = new Dictionary<String, String>();
            dataForRs433 = new byte[Rtl_433_Processor.nbByteForRts_433];
            _copyIQBuffer = UnsafeBuffer.Create(Rtl_433_Processor.nbComplexForRts_433, sizeof(Complex));
            _copyIQPtr = (Complex*)_copyIQBuffer;
            setVerbose("-v");  //for list devices details
            call_main_Rtl_433();
            }
#region options rtl_433
        public void setVerbose(String value)
        {
            if (listOptionsRtl433.ContainsKey("verbose") & value == "")
                listOptionsRtl433.Remove("verbose");
            else if (!listOptionsRtl433.ContainsKey("verbose"))
                listOptionsRtl433.Add("verbose", value);
            else
                listOptionsRtl433["verbose"] = value;
        }
        public void setAnalyze(String value)
        {
            if (listOptionsRtl433.ContainsKey("Analyze") & value == "")
                listOptionsRtl433.Remove("Analyze");
            else if (!listOptionsRtl433.ContainsKey("Analyze"))
                listOptionsRtl433.Add("Analyze", value);
            else
                listOptionsRtl433["Analyze"] = value;
        }
        public void setanalyze(String value)
        {
            if (listOptionsRtl433.ContainsKey("analyze") & value == "")
                listOptionsRtl433.Remove("analyze");
            else if (!listOptionsRtl433.ContainsKey("analyze"))
                listOptionsRtl433.Add("analyze", value);
            else
                listOptionsRtl433["analyze"] = value;
        }
        public void setProtocol(String value)
        {
            if (listOptionsRtl433.ContainsKey("Protocol") & value == "")
                listOptionsRtl433.Remove("Protocol");
            else if (!listOptionsRtl433.ContainsKey("Protocol"))
                listOptionsRtl433.Add("Protocol", value);
            else
                listOptionsRtl433["Protocol"] = value;
        }
        public void setUnknown(String value)
        {
            if (listOptionsRtl433.ContainsKey("SUnknown") & value == "")
                listOptionsRtl433.Remove("SUnknown");
            else if (!listOptionsRtl433.ContainsKey("SUnknown"))
                listOptionsRtl433.Add("SUnknown", value);
            else
                listOptionsRtl433["Protocol"] = value;
        }
        public void setHideDevices(List <string> listBoxHideDevices)
        {
            Dictionary<String, String> copyListOptionsRtl433=new Dictionary<String, String>();
            foreach (KeyValuePair<string, string> _option in listOptionsRtl433)
            {
                if (!_option.Key.Contains("hide"))
                    copyListOptionsRtl433.Add(_option.Key,_option.Value);
                else
                {
                    if (_option.Value.Contains("-R-"))
                        copyListOptionsRtl433.Add(_option.Key,"-" + _option.Value.Replace("-","")); //register protocol
                }
            }
            listOptionsRtl433.Clear();
            foreach (string device in listBoxHideDevices)
            {
                listOptionsRtl433.Add("hide" + device, "-R-" + device.Trim()); //unregister protocol
            }
            foreach (KeyValuePair<string, string> _option in copyListOptionsRtl433)
            {
                listOptionsRtl433.Add(_option.Key, _option.Value);
            }
         }
        public void setSaveDeviceOption(String value)
        {
            if (!listOptionsRtl433.ContainsKey("saveDevice"))
                listOptionsRtl433.Remove("saveDevice");
            if (!listOptionsRtl433.ContainsKey("saveDevice"))
                listOptionsRtl433.Add("saveDevice", value);
            else
                listOptionsRtl433["saveDevice"] = value;
        }
        public void setMetadata(String value)
        {
            if (!listOptionsRtl433.ContainsKey("metadata") & value == "")
                listOptionsRtl433.Remove("metadata");
            if (!listOptionsRtl433.ContainsKey("metadata"))
                listOptionsRtl433.Add("metadata", value);
            else
                listOptionsRtl433["metadata"] = value;
        }
        public void setPulse(String value)
        {
            if (!listOptionsRtl433.ContainsKey("savePulse") & value == "")
                listOptionsRtl433.Remove("savePulse");
            if (!listOptionsRtl433.ContainsKey("savePulse"))
                listOptionsRtl433.Add("savePulse", value);
            else
                listOptionsRtl433["savePulse"] = value;
        }
#endregion
#region private functions
        private readonly Rtl_433_Panel _owner;
        private System.Timers.Timer callMainTimer;
        static NativeMethods.ptrFct CBmessages;
        static NativeMethods.ptrFctInit CBinitCbData;
        private void OnCallMainTimedEvent(object source, ElapsedEventArgs e)
        {
            callMainTimer.Enabled = false;
           // setAnalyze("-A");
           // listOptionsRtl433.Add("-Analyze", "-A");
            
            //listOptionsRtl433.Add("-analyze", "-a 4");
            //listOptionsRtl433.Add("-Mprotocol", "-Mprotocol");

            string[] args = new string[listOptionsRtl433.Count()+1];  //+1 for .exe
            int counter = 0;
            args[counter] = "Rtl_433.exe";
            counter++;
            foreach (KeyValuePair<string, string> _option in listOptionsRtl433)
            {
                args[counter] = _option.Value;
                counter++;
            }
            Int32 argc = args.Length;
            CBmessages = new NativeMethods.ptrFct(_callBackMessages);
            CBinitCbData = new NativeMethods.ptrFctInit(_callBackInitCbData);
            NativeMethods.rtl_433_call_main(CBmessages, CBinitCbData,(UInt32)(_sampleRate),sizeof(byte) , argc, args);
         }
#endregion
#region public function 
        private String _timeCycle = "";
        [System.ComponentModel.Bindable(true)]
        public String timeCycle
        {
            get { return _timeCycle; }
            set
            {
                _timeCycle = value;
                OnPropertyChanged("timeCycle");
            }
        }
        private String _timeForRtl433 = "";
        [System.ComponentModel.Bindable(true)]
        public String timeForRtl433
        {
            get { return _timeForRtl433; }
            set
            {
                _timeForRtl433 = value;
                OnPropertyChanged("timeForRtl433");
            }
        }
        public void Start(bool senderRadio)
        {
            _owner.Start(senderRadio);
        }
        public void Stop(bool senderRadio)
        {
            _owner.Stop(senderRadio);
        }
        private String _sampleRateStr = "";
        [System.ComponentModel.Bindable(true)]
        public String SampleRate
        {
            get { return _sampleRateStr; }
            set
            {
                _sampleRateStr = "SampleRate: " + value;
                OnPropertyChanged("SampleRate");
            }
        }
        bool _RecordMONO = false;
        public bool RecordMONO
        {
            get { return _RecordMONO; }
            set
            {
                _RecordMONO = value;
            }
        }
        bool _RecordSTEREO = false;
        public bool RecordSTEREO
        {
            get { return _RecordSTEREO; }
            set
            {
                _RecordSTEREO = value;
            }
        }
        private double _sampleRate = 0;
        public void setSampleRate(double value)
        {
            _sampleRate = value;
            SampleRate = value.ToString();
        }
        public void recordDevice(string name)
        {
            string directory = "./Recordings";
            if (!Directory.Exists(directory))
                directory = "";
            string nameFile = directory + "/" + name.Replace(":", "_") + "_" + _frequencyLng.ToString() + "_" + _sampleRate.ToString() + "_" + DateTime.Now.Date.ToString("d").Replace("/", "_") + " " + DateTime.Now.Hour + " " + DateTime.Now.Minute + " " + DateTime.Now.Second + " ";
            if (_RecordMONO)
            {
                string _nameFile = nameFile + ((wavRecorder.recordType)wavRecorder.recordType.MONO + ".wav");
                wavRecorder.WriteBufferToWav(_nameFile, _copyIQPtr, Rtl_433_Processor.nbComplexForRts_433, _sampleRate, wavRecorder.recordType.MONO);
            }
            if (_RecordSTEREO)
            {
                string _nameFile = nameFile + ((wavRecorder.recordType)wavRecorder.recordType.STEREO + ".wav");
                wavRecorder.WriteBufferToWav(_nameFile, _copyIQPtr, Rtl_433_Processor.nbComplexForRts_433, _sampleRate, wavRecorder.recordType.STEREO);
            }
        }
        public void setSourceName(string sourceName)
        {
            sourceName = sourceName.Replace("%20", " ");
            _owner.setMessage(sourceName);
        }
        public void get_version_dll_rtl_433()
        {
            Version=(Marshal.PtrToStringAnsi(NativeMethods.IntPtr_Pa_GetVersionText()));
        }
        private String _version="";
        [System.ComponentModel.Bindable(true)]
        public String Version
        {
            get { return _version;}
            set
            {
                _version = "Version: " + value;
                OnPropertyChanged("Version");
            }
        }
        private long _frequencyLng = 0;
        public void setfrequency(long value)
        {
            _frequencyLng = value;
            frequency = "Frequency: " + _frequencyLng.ToString();
        }
        private long _centerFrequencyLng = 0;
        public void setCenterFrequency(long value)
        {
            _centerFrequencyLng = value;
            centerFrequency = "Center frequency: " + _centerFrequencyLng.ToString(); ;
        }
        private string _frequency = "";
        [System.ComponentModel.Bindable(true)]
        public String frequency
        {
            get { return _frequency; }
            set
            {
                _frequency = value;
                OnPropertyChanged("frequency");
                NativeMethods.setFrequency((UInt32)_frequencyLng);
            }
        }
        private string _centerFrequency="";
        [System.ComponentModel.Bindable(true)]
        public String centerFrequency
        {
            get { return _centerFrequency; }
            set
            {
                _centerFrequency = value;
                OnPropertyChanged("centerFrequency");
                NativeMethods.setCenterFrequency((UInt32)_centerFrequencyLng);
            }
        }
        private void OnPropertyChanged(string propertyName)
        {
            //if (PropertyChanged != null)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));//error on stop timeCycle
        }
        public void call_main_Rtl_433()
        {
            _timeCycleLng = 0;
            _timeForRtl433Lng = 0;
            _timeCycleLngMax = 0;
            _timeForRtl433LngMax = 0;
            callMainTimer = new System.Timers.Timer();
            callMainTimer.Elapsed += new ElapsedEventHandler(OnCallMainTimedEvent);
            callMainTimer.Interval = 1000;
            callMainTimer.Enabled = true;
            stopw.Restart();
        }
        private long _timeCycleLng = 0;
        private long _timeForRtl433Lng = 0;
        private long _timeCycleLngMax = 0;
        private long _timeForRtl433LngMax = 0;
        public unsafe void send_data(Complex * _IQPtr)
        {
            if (ptrCtx != IntPtr.Zero && startRtl433Ok)
            {
               float maxi = wavRecorder.getMaxi(_IQPtr, Rtl_433_Processor.nbComplexForRts_433);
               if (maxi != 0)
               { 
               Utils.Memcpy(_copyIQPtr, _IQPtr, Rtl_433_Processor.nbComplexForRts_433 * sizeof(Complex));  //memo for record
               for (var i = 0; i < Rtl_433_Processor.nbComplexForRts_433; i++)
               {
                   dataForRs433[i * 2] = System.Convert.ToByte(127 + (_IQPtr[i].Real / maxi) * 127);
                   dataForRs433[i * 2 + 1] = System.Convert.ToByte(127 + (_IQPtr[i].Imag / maxi) * 127);
               }
                stopw.Stop();
                memoDt = stopw.ElapsedMilliseconds;
                stopw.Restart();
                NativeMethods.receive_buffer_cb(dataForRs433 ,Rtl_433_Processor.nbByteForRts_433, ptrCtx);
                stopw.Stop();
                _timeCycleLng = memoDt + stopw.ElapsedMilliseconds;
                _timeForRtl433Lng = stopw.ElapsedMilliseconds;
                if (_timeCycleLng > _timeCycleLngMax)
                    _timeCycleLngMax = _timeCycleLng;
                if (_timeForRtl433Lng > _timeForRtl433LngMax)
                    _timeForRtl433LngMax = _timeForRtl433Lng;
                setTime();
                stopw.Restart();
                }
            }
        }
        private void setTime()
        {
            timeCycle = "Cycle time: " + _timeCycleLng.ToString() + " ms. max= " + _timeCycleLngMax.ToString() + " ms.";
            timeForRtl433 = "Cycle time Rtl433: " + _timeForRtl433Lng.ToString() + " ms. max= " + _timeForRtl433LngMax.ToString() + " ms.";
        }
        public void stopSdr() 
        {
            startRtl433Ok = false;
            NativeMethods.stop_sdr(ptrCtx);
        }
        public void startSendData() 
        {
            ptrCtx = IntPtr.Zero;
            startRtl433Ok = true;
        }
#endregion
#region callBack for dll_rtl_433"
        private bool startRtl433Ok = false;
        private bool synchro = false;
        private bool nameData = false;
        private string key = "";
        private Dictionary<String, String> listData;
        private bool initListDevice = false;
        private bool startListDevice = false;
        private List<string> listeDevice = new List<string>();
        private string[] nameGraph = { "Pulse data", "Analyse", "fm" };
        public unsafe void _callBackMessages([In, MarshalAs(UnmanagedType.LPStr)] string message)
        {
            if (initListDevice == false)
            {
                if (message.Contains("start devices list"))//start
                {
                    startListDevice = true;
                }

                else if (message.Contains("end devices list") && startListDevice)  //stop
                {
                    initListDevice = true;
                    _owner.setListDevices(listeDevice);
                    stopSdr();
                    listOptionsRtl433.Clear();  //for -v 
                    setanalyze("-a 4");
                    setProtocol("-Mprotocol");  //for title and key devices windows
                }
                else
                {
                    if (message.Length > 21 && message.Contains("Registering"))
                        listeDevice.Add(message.Substring(22).Replace("]", "-"));
                 }
                if (startListDevice == false)
                    _owner.setMessage(message);
                return;
            }
            if (message.Contains("**********"))//stop
            {
                if (listData.Count > 1 && synchro == true)
                {
                    Dictionary<String, String> listDataClone;
                    lock(listData)
                    {
                     listDataClone = listData.ToDictionary(elem => elem.Key, elem => elem.Value);
                    }
                    int NumGraph = 3;
                    List<PointF>[] points = new List<PointF>[NumGraph];
                    for (int i = 0; i < NumGraph ; i++)
                        points[i] = new List<PointF>();
                    //debug mode:
                    //memory test in visual studio with replay file Model_ GT-WT03 Id_175_434037000_250000_02_03_2021 12 2 52 STEREO.wav:
                    //170M with comment _owner.addFormDevice(listDataClone, points,nameGraph); and NumGraph>100-->ok

                    //with only NumGraph>100 108M avant start plugin--->147M after 80 mn.
                    //add only structCfg ok
                    // without _owner.addFormDevice ok

                    //with data frozen ok


                    if (ptrCfg != IntPtr.Zero && NumGraph>0)
                    {
                        NativeMethods.r_cfg structCfg = new NativeMethods.r_cfg();
                        structCfg = (NativeMethods.r_cfg)Marshal.PtrToStructure(ptrCfg, typeof(NativeMethods.r_cfg));
                        if (structCfg.demod != IntPtr.Zero)
                        {
                            NativeMethods.dm_state struct_demod = new NativeMethods.dm_state();
                            struct_demod = (NativeMethods.dm_state)Marshal.PtrToStructure(structCfg.demod, typeof(NativeMethods.dm_state));
                            int x = 0;
                            //if (struct_demod.pulse_data.num_pulses > 0)
                            //{
                                for (int bit = 0; bit < (struct_demod.pulse_data.num_pulses); bit++)
                                {
                                    x += struct_demod.pulse_data.pulse[bit];
                                    points[0].Add(new PointF(x, 1));
                                    points[0].Add(new PointF(x, 0));
                                    x += struct_demod.pulse_data.gap[bit];
                                    points[0].Add(new PointF(x, 0));
                                    points[0].Add(new PointF(x, 1));
                                }
                            //}
                            int endData = 0;
                            if (NumGraph > 1)
                            {
                                endData = searchZero(struct_demod.am_buf);
                                //if (endData> 0)
                                //{
                                    for (int bit = 0; bit < endData; bit++)
                                    {
                                        for (int b = bit; b < bit + 10; b++)
                                        {
                                            points[1].Add(new PointF(bit, struct_demod.am_buf[bit]));
                                        }
                                    }
                                //}
                            }
                            if (NumGraph > 2)
                            {
                               endData = searchZero(struct_demod.fm);
                               //if (endData > 0)
                               // {
                                    for (int bit = 0; bit < endData; bit++)
                                    {
                                        for (int b = bit; b < bit + 10; b++)
                                        {
                                            points[2].Add(new PointF(bit, struct_demod.fm[bit]));
                                        }
                                    }
                                }
                            //}
                        }
                        }
                    _owner.addFormDevice(listDataClone, points,nameGraph);
                }
                nameData = false;
                synchro = false;
                key = "";
                return;
            }
            if (!synchro && message.Contains("@@@@@@@@@@"))//start  
            {
                synchro = true;
                listData.Clear();
                nameData = false;
                key = message;
                return;
            }
            else if (synchro && !nameData)
            {
                key = message;
                nameData = true;
                return;
            }
            else if (synchro && nameData)
            {
                try {                    //if key do not exist
                listData.Add(key, message);
                nameData = false;
                key = "";
                return;
                }
                catch
                {
                nameData = false;
                key = "";
                return;
                }
            }
            else
                _owner.setMessage(message);
        }
        private int searchZero(short[] points)
        {
            int step = 1000;
            int start = 0;
            bool find = false;
            int i = 0;
            while (find == false && i < points.Length)
            {
                for (i = start; i < points.Length - 1; i += step)
                {
                    if (points[i] == 0 && points[i + 1] == 0)
                    {
                        start = i - step;
                        if (step == 1 || start < 0)
                        {
                            find = true;
                            break;
                        }
                        step /= 10;
                        break;
                    }
                }
            }
            if (!find) return points.Length - 1;
            return i;
        }
        private IntPtr ptrCbData= IntPtr.Zero;
        private Int32 bufNumber = 0;
        private UInt32 bufLength = 0;
        private IntPtr ptrCtx= IntPtr.Zero;
        private IntPtr ptrCfg= IntPtr.Zero;
        public void _callBackInitCbData([In, MarshalAs(UnmanagedType.FunctionPtr)] IntPtr _ptrCbData,
            [In, MarshalAs(UnmanagedType.SysInt)] Int32 _bufNumber,
            [In, MarshalAs(UnmanagedType.SysInt)] UInt32 _bufLength,
            [In, MarshalAs(UnmanagedType.FunctionPtr)] IntPtr _ptrCtx,
            [In, MarshalAs(UnmanagedType.FunctionPtr)] IntPtr _ptrCfg)
        {
            ptrCbData = _ptrCbData;
            bufNumber = _bufNumber;
            bufLength = _bufLength;
            ptrCtx = _ptrCtx;
            ptrCfg = _ptrCfg;
        }
#endregion
#region devices form
        private void CloseOneForms(String name)
        {
            List<Form> formsToClose = new List<Form>();
            foreach (Form form in Application.OpenForms)
            {
                if (form.Text == name)
                {
                    formsToClose.Add(form);
                }
            }
            formsToClose.ForEach(f => f.Close());
        }
#endregion
    }
 }
