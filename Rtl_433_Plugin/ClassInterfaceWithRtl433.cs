
/* Written by Marc Prieur (marco40_github@sfr.fr)
                                ClassInterfaceWithRtl433.cs 
                            project Rtl_433_Plugin
						         Plugin for SdrSharp
 **************************************************************************************
 Creative Commons Attrib Share-Alike License
 You are free to use/extend this library but please abide with the CC-BY-SA license:
 Attribution-NonCommercial-ShareAlike 4.0 International License
 http://creativecommons.org/licenses/by-nc-sa/4.0/

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
using System.Drawing;

namespace SDRSharp.Rtl_433
{
   public unsafe class ClassInterfaceWithRtl433 : INotifyPropertyChanged
    {
        private const string _VERSION = "1.5.3.0";
        public enum SAVEDEVICE{none,all,known,unknown};
        public event PropertyChangedEventHandler PropertyChanged;
        private byte[] dataForRs433;
        private Complex*[] _copyIQPtr;
        private UnsafeBuffer[] _copyIQBuffer;
        private Stopwatch stopw;
        private long memoDt;
        private Dictionary<String, String> listOptionsRtl433;
        public ClassInterfaceWithRtl433(Rtl_433_Panel owner)
        {
            _owner = owner;
            stopw = new Stopwatch();
            listData = new Dictionary<String, String>();
            SampleRateStr = "Sample rate: 0";  //no display if init to Rtl_433_panel
            ////setTime();
            listOptionsRtl433 = new Dictionary<String, String>();
            dataForRs433 = new byte[Rtl_433_Processor.NBBYTEFORRTS_433];
            _copyIQBuffer = new UnsafeBuffer[Rtl_433_Processor.NBBUFFERFORRTS_433];
            _copyIQPtr = new Complex*[Rtl_433_Processor.NBBUFFERFORRTS_433];
            _copyIQBuffer[0] = UnsafeBuffer.Create(Rtl_433_Processor.NBCOMPLEXFORRTS_433, sizeof(Complex));
            _copyIQPtr[0] = (Complex*)_copyIQBuffer[0];
            for (int i = 1; i < Rtl_433_Processor.NBBUFFERFORRTS_433; i++)
            {
                _copyIQBuffer[i] = UnsafeBuffer.Create(Rtl_433_Processor.NBCOMPLEXFORRTS_433, sizeof(Complex));
                _copyIQPtr[i] = (Complex*)_copyIQBuffer[i];
            }
            setOption("verbose","-v");                    //setVerbose("-v");  //for list devices details
                                                          // _owner.setMessage(Application.ProductVersion);  version sdrSharp
                                                          //call_main_Rtl_433(false);
            setanalyze("-a 4");
            setProtocol("-Mprotocol");  //for title and key devices windows
        }
        #region options rtl_433
        /// <summary>
        /// For display Graphic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void setanalyze(String value)
        {
            if (listOptionsRtl433.ContainsKey("analyze") & value == "")
                listOptionsRtl433.Remove("analyze");
            else if (!listOptionsRtl433.ContainsKey("analyze"))
                listOptionsRtl433.Add("analyze", value);
            else
                listOptionsRtl433["analyze"] = value;
        }
        /// <summary>
        /// specific key dif metadata(MbitsOrLevel) for title and key devices windows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void setProtocol(String value)
        {
            if (listOptionsRtl433.ContainsKey("MProtocol") & value == "")
                listOptionsRtl433.Remove("MProtocol");
            else if (!listOptionsRtl433.ContainsKey("MProtocol"))
                listOptionsRtl433.Add("MProtocol", value);
            else
                listOptionsRtl433["MProtocol"] = value;
        }
        public void setHideOrShowDevices(List <string> listBoxHideDevices,bool hide)
        {

            //1-supprimer tous les hide
 
            foreach (KeyValuePair<string, string> _option in listOptionsRtl433)  //error on modifie collection  no error v 1811??
            {
                if (_option.Key.Contains("hide"))
                    listOptionsRtl433.Remove(_option.Key);
            }
            //1-supprimer tous les show
            foreach (KeyValuePair<string, string> _option in listOptionsRtl433)  //error on modifie collection  no error v 1811??
            {
                if (_option.Key.Contains("show"))
                    listOptionsRtl433.Remove(_option.Key);
            }
            //3-ajouter tous les select
            if(hide)
            {
                foreach (string device in listBoxHideDevices)
                {
                    listOptionsRtl433.Add("hide" + device, "-R -" + device.Trim()); //hide protocol
                }
            }
            else
            {
                foreach (string device in listBoxHideDevices)
                {
                    listOptionsRtl433.Add("show" + device, "-R " + device.Trim()); //show protocol
                }
            }
            ////Dictionary<String, String> copyListOptionsRtl433=new Dictionary<String, String>();
            ////foreach (KeyValuePair<string, string> _option in listOptionsRtl433)
            ////{
            ////    if (!_option.Key.Contains("hide"))
            ////        copyListOptionsRtl433.Add(_option.Key,_option.Value);
            ////    else
            ////    {
            ////        if (_option.Value.Contains("-R-"))
            ////            copyListOptionsRtl433.Add(_option.Key,"-" + _option.Value.Replace("-","")); //register protocol
            ////    }
            ////}
            ////listOptionsRtl433.Clear();
            ////foreach (string device in listBoxHideDevices)
            ////{
            ////    listOptionsRtl433.Add("hide" + device, "-R-" + device.Trim()); //unregister protocol
            ////}
            ////foreach (KeyValuePair<string, string> _option in copyListOptionsRtl433)
            ////{
            ////    listOptionsRtl433.Add(_option.Key, _option.Value);
            ////}
        }
        public void setOption(String Key,String value)
        {
            if (listOptionsRtl433.ContainsKey(Key))
                listOptionsRtl433.Remove(Key);
            if(!value.Contains("No "))
                listOptionsRtl433.Add(Key, value);
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
            string[] args = new string[listOptionsRtl433.Count()+1];  //+1 for .exe
            int counter = 0;
            args[counter] = "Rtl_433.exe";
            counter++;
            _owner.setMessage("-----------RTL433 OPTIONS --------------");
            foreach (KeyValuePair<string, string> _option in listOptionsRtl433)
            {
                _owner.setMessage(_option.Value);
                args[counter] = _option.Value;
                counter++;
            }
            _owner.setMessage("------------------------------------------");
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
        public String SampleRateStr
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
        public double SampleRateDbl
        {
            get  { return _sampleRate; }
            set
            {
            _sampleRate = value;
            SampleRateStr = value.ToString();
            }
        }
        public string getDirectoryRecording()
        {
            string directory = "./Recordings/";   //SDRSHARP.exe to SDRSHARP
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
        public void recordDevice(string name)
        {
            string directory = getDirectoryRecording();
            string nameFile = directory  + name.Replace(":", "_") + "_" + _frequencyLng.ToString() + "_" + _sampleRate.ToString() + "_" + DateTime.Now.Date.ToString("d").Replace("/", "_") + " " + DateTime.Now.Hour + " " + DateTime.Now.Minute + " " + DateTime.Now.Second + " ";
            if (_RecordMONO)
            {
                string _nameFile = nameFile + ((wavRecorder.recordType)wavRecorder.recordType.MONO + ".wav");
                wavRecorder.WriteBufferToWav(_nameFile, _copyIQPtr, Rtl_433_Processor.NBCOMPLEXFORRTS_433,  _sampleRate, wavRecorder.recordType.MONO);
            }
            if (_RecordSTEREO)
            {
                string _nameFile = nameFile + ((wavRecorder.recordType)wavRecorder.recordType.STEREO + ".wav");
                wavRecorder.WriteBufferToWav(_nameFile, _copyIQPtr, Rtl_433_Processor.NBCOMPLEXFORRTS_433,  _sampleRate, wavRecorder.recordType.STEREO);
            }
        }
        public void setSourceName(string sourceName)
        {
            sourceName = sourceName.Replace("%20", " ");
            _owner.setMessage(sourceName);
        }
        public void get_version_dll_rtl_433()
        {
            string VersionC = Marshal.PtrToStringAnsi(NativeMethods.IntPtr_Pa_GetVersionText());
            MessageBox.Show("version rtl_433.dll " + VersionC, "start plugin RTL433", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //string filename = @".\xSDRSharp.Rtl_433.dll";
            //Assembly assem = Assembly.ReflectionOnlyLoadFrom(filename);
            //AssemblyName assemName = assem.GetName();
            //Version ver = assemName.Version;

            //if (VersionC != _VERSION)
            //     MessageBox.Show("version rtl_433.dll not égal to SDRSharp.Rtl_433.dll" + VersionC + "<->" + _VERSION, "SDRSharp.Rtl_433.dll");
            //Version = VersionC;  //
            //Version = "Version plugin: " + _VERSION;
        }
        private String _version="";
        [System.ComponentModel.Bindable(true)]
        public String Version
        {
            get { return _version;}
            set
            {
                _version = "Version plugin: " + _VERSION;
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
               float maxi = wavRecorder.getMaxi(_IQPtr, Rtl_433_Processor.NBCOMPLEXFORRTS_433);
               if (maxi > 0)
               { 

               Utils.Memcpy(_copyIQPtr[0], _IQPtr, Rtl_433_Processor.NBCOMPLEXFORRTS_433 * sizeof(Complex));  //memo for record
               for (int i = Rtl_433_Processor.NBBUFFERFORRTS_433-1; i > 0; i--)
                   Utils.Memcpy(_copyIQPtr[i], _copyIQPtr[i - 1], Rtl_433_Processor.NBCOMPLEXFORRTS_433 * sizeof(Complex));  //memo for record
               for (int i = 0; i < Rtl_433_Processor.NBCOMPLEXFORRTS_433; i++)
               {
                   dataForRs433[i * 2] = System.Convert.ToByte(127 + (_IQPtr[i].Real / maxi) * 127);
                   dataForRs433[i * 2 + 1] = System.Convert.ToByte(127 + (_IQPtr[i].Imag / maxi) * 127);
               }
                stopw.Stop();
                memoDt = stopw.ElapsedMilliseconds;
                stopw.Restart();
                NativeMethods.receive_buffer_cb(dataForRs433 ,Rtl_433_Processor.NBBYTEFORRTS_433, ptrCtx);
                stopw.Stop();
                _timeCycleLng = memoDt + stopw.ElapsedMilliseconds;
                _timeForRtl433Lng = stopw.ElapsedMilliseconds;
                if (_timeCycleLng > _timeCycleLngMax)
                    _timeCycleLngMax = _timeCycleLng;
                if (_timeForRtl433Lng > _timeForRtl433LngMax)
                    _timeForRtl433LngMax = _timeForRtl433Lng;
                ////setTime();
                stopw.Restart();
                }
            }
        }
        ////private void setTime()
        ////{
        ////    timeCycle = "Cycle time: " + _timeCycleLng.ToString() + " ms. max= " + _timeCycleLngMax.ToString() + " ms.";
        ////    timeForRtl433 = "Cycle time Rtl433: " + _timeForRtl433Lng.ToString() + " ms. max= " + _timeForRtl433LngMax.ToString() + " ms.";
        ////}
        public void stopSdr() 
        {
            startRtl433Ok = false;
            NativeMethods.stop_sdr(ptrCtx);
        }
        public void CleartimeCycleMax()
        {
            _timeCycleLngMax = 0;
            _timeForRtl433LngMax = 0;
            ////setTime();
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
            //string message = cloneMessage.Substring(0, cloneMessage.Length);
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
                    //stopSdr();
                    //listOptionsRtl433.Clear();  //for -v 
                    //setanalyze("-a 4");
                    //setProtocol("-Mprotocol");  //for title and key devices windows
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
                //return;
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
                    //debug version:error outofmemory to 100 devices forms
                    //release version error create handle to 200 devices forms



                    //debug mode:
                    //memory test in visual studio with replay file Model_ GT-WT03 Id_175_434037000_250000_02_03_2021 12 2 52 STEREO.wav:
                    //170M with comment _owner.addFormDevice(listDataClone, points,nameGraph); and NumGraph>100-->ok

                    //with only NumGraph>100 108M avant start plugin--->147M after 80 mn.
                    //add only structCfg ok
                    // without _owner.addFormDevice ok

                    //with data frozen ok

                    double samples_per_us = 1000000.0 / _sampleRate;
                    if (ptrCfg != IntPtr.Zero && NumGraph>0)
                    {
 
                        NativeMethods.r_cfg structCfg = new NativeMethods.r_cfg();
                         try {    
                        structCfg = (NativeMethods.r_cfg)Marshal.PtrToStructure(ptrCfg, typeof(NativeMethods.r_cfg));
                         }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message + "  ClassInterfaceWithRtl433->_callBackMessages", "Error structCfg", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }         
                        if (structCfg.demod != IntPtr.Zero)
                        {
                            NativeMethods.dm_state struct_demod = new NativeMethods.dm_state();
                         try {
                            struct_demod = (NativeMethods.dm_state)Marshal.PtrToStructure(structCfg.demod, typeof(NativeMethods.dm_state));
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message + "  ClassInterfaceWithRtl433->_callBackMessages", "Error struct_demod", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                            int x = 0;
                            
                            if (struct_demod.pulse_data.num_pulses == 0)
                                x = 0;
                             if (struct_demod.pulse_data.num_pulses > 0)
                            {
                                for (int bit = 0; bit < (struct_demod.pulse_data.num_pulses); bit++)
                                {
                                    x += (int) (struct_demod.pulse_data.pulse[bit] * samples_per_us);
                                    points[0].Add(new PointF(x, 1));
                                    points[0].Add(new PointF(x, 0));
                                    x += (int) (struct_demod.pulse_data.gap[bit] * samples_per_us);
                                    points[0].Add(new PointF(x, 0));
                                    points[0].Add(new PointF(x, 1));
                                }
                            }
                            else
                            {
                                for (int bit = 0; bit < (struct_demod.fsk_pulse_data.num_pulses); bit++)
                                {
                                    x += (int)(struct_demod.fsk_pulse_data.pulse[bit] * samples_per_us);
                                    points[0].Add(new PointF(x, 1));
                                    points[0].Add(new PointF(x, 0));
                                    x += (int) (struct_demod.fsk_pulse_data.gap[bit] * samples_per_us);
                                    points[0].Add(new PointF(x, 0));
                                    points[0].Add(new PointF(x, 1));
                                }
                            }
                            
                            if (NumGraph > 1)
                            {
                                int endData = searchZero(struct_demod.am_buf);
                                //if (endData> 0)
                                //{
                                    for (int bit = 0; bit < endData; bit++)
                                    {
                                        for (int b = bit; b < bit + 10; b++)
                                        {
                                            points[1].Add(new PointF(bit, (int)(struct_demod.am_buf[bit] * samples_per_us)));
                                        }
                                    }
                                //}
                            }
                            if (NumGraph > 2)
                            {
                               int endData = searchZero(struct_demod.fm);
                               //if (endData > 0)
                               // {
                                    for (int bit = 0; bit < endData; bit++)
                                    {
                                        for (int b = bit; b < bit + 10; b++)
                                        {
                                            points[2].Add(new PointF(bit, (int)(struct_demod.fm[bit] * samples_per_us)));
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
                listData.Add( key.Substring(0, 1).ToUpper()+key.Substring(1), message);
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
            [In, MarshalAs(UnmanagedType.SysUInt)] UInt32 _bufLength,
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
