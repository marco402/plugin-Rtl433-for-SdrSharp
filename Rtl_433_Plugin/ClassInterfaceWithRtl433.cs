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
#define xxxSAMPGRAD
#define xxxTESTMEMORY

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Windows.Forms;
using System.Linq;
using SDRSharp.Radio;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Diagnostics;

namespace SDRSharp.Rtl_433

{
    public unsafe class ClassInterfaceWithRtl433 : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        internal enum SAVEDEVICE { none, all, known, unknown };

        static NativeMethods.ptrReceiveMessagesCallback CBmessages;
        static NativeMethods.ptrReceiveRecordOrder CBReceiveRecordOrder;
        static NativeMethods.ptrFctInit CBinitCbData;
        static NativeMethods.r_cfg structCfg = new NativeMethods.r_cfg();
        static NativeMethods.dm_state struct_demod = new NativeMethods.dm_state();

        private Thread threadCallMainRTL_433;
        private Byte[] dataForRs433;
        private Complex*[] copyIQPtr;
        private UnsafeBuffer[] copyIQBuffer;
        private Dictionary<String, String> listOptionsRtl433;
        private Rtl_433_Panel owner;

        //private String timeForRtl433 = "";
        private String sampleRateStr = "";
        private Double sampleRate = 0;

        private Int64 frequencyLng = 0;
        private Int64 centerFrequencyLng = 0;
        private String frequencyStr = "";
        private String centerFrequencyStr = "";
        private UInt32 EnabledDevicesDisabled = 0;
        private String key = "";
        private Dictionary<String, String> listData;

        private List<String> listeDevice = new List<String>();
        private String[] nameGraph = { "Pulse data", "Analyse", "fm" }; 

        //private Int32 bufNumber = 0;
        //private UInt32 bufLength = 0;
        private IntPtr ptrCbData = IntPtr.Zero;
        private IntPtr ptrCtx = IntPtr.Zero;
        private IntPtr ptrCfg = IntPtr.Zero;

        private Boolean typeWindowGraph = false;
        private Boolean sendDataToRtl433 = false;
        private Boolean synchro = false;
        private Boolean nameData = false;
        private Boolean initListDevice = false;
        private Boolean startListDevice = false;
        private uint memo_EnabledListDevices = 0;
#if TESTIME
        private Stopwatch stopw;
        //private Stopwatch stopwTotalTime;
        private long memoDt;
        private long memoDtTotalTime = 0;
        private Int64 _timeForRtl433Lng = 0;
        private Int64 _timeCycleCumul = 0;
        private Int64 _timeForRtl433Cumul = 0;
        private Int32 nbCycle50000 = 0;
        private DateTime memoDate= DateTime.Now;
        private Int32 nbCycleFor1Sec = 0;
#endif
#if TESTMEMORY
        private Process currentProcess;
#endif

        private Int32 searchZero(short[] points)
        {
            Int32 step = 1000;
            Int32 start = 0;
            Boolean find = false;
            Int32 i = 0;
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
        internal ClassInterfaceWithRtl433(Rtl_433_Panel owner)
        {
#if TESTIME
            stopw = new Stopwatch();
            //stopwTotalTime = new Stopwatch();
#endif
#if TESTMEMORY
            currentProcess = Process.GetCurrentProcess();
#endif
            this.owner = owner;
            listData = new Dictionary<String, String>();
            SampleRateStr = "0";  //no display if init to Rtl_433_panel
            listOptionsRtl433 = new Dictionary<String, String>();
            dataForRs433 = new byte[Rtl_433_Processor.NBBYTEFORRTS_433];
            copyIQBuffer = new UnsafeBuffer[Rtl_433_Processor.NBBUFFERFORRTS_433];
            copyIQPtr = new Complex*[Rtl_433_Processor.NBBUFFERFORRTS_433];
            copyIQBuffer[0] = UnsafeBuffer.Create(Rtl_433_Processor.NBCOMPLEXFORRTS_433, sizeof(Complex));
            copyIQPtr[0] = (Complex*)copyIQBuffer[0];
            for (Int32 i = 1; i < Rtl_433_Processor.NBBUFFERFORRTS_433; i++)
            {
                copyIQBuffer[i] = UnsafeBuffer.Create(Rtl_433_Processor.NBCOMPLEXFORRTS_433, sizeof(Complex));
                copyIQPtr[i] = (Complex*)copyIQBuffer[i];
            }
            setOption("verbose","-v");                    //setVerbose("-v");  //for list devices details
                                                          // _owner.setMessage(Application.ProductVersion);  version sdrSharp
                                                          //call_main_Rtl_433(false);
            setOptionUniqueKey("-a 4",true);         //for graph
            setOptionUniqueKey("-MProtocol",true);  //for title and key devices windows
        }
#region options rtl_433
        internal void setOptionUniqueKey(String key,Boolean state)
        {
            if (listOptionsRtl433.ContainsKey(key))
                listOptionsRtl433.Remove(key);
            if (state)
                listOptionsRtl433.Add(key, key);
        }
        internal void setHideOrShowDevices(List<String> listBoxSelectedDevices, Boolean hideShowDevices)
        {
            Dictionary<String, String> copyListOptionsRtl433 = new Dictionary<String, String>();
            foreach (KeyValuePair<String, String> _option in listOptionsRtl433)
            {
                if (!(_option.Key.Contains("hide") || _option.Key.Contains("show")))
                    //keep all key not egal hide or show
                    copyListOptionsRtl433.Add(_option.Key, _option.Value);    //keep all key not egal hide or show
             }
            //-R device<protocol> show protocol
            //-R device<-1> hide protocol
            //without -R show all protocol

            listOptionsRtl433.Clear();

            foreach (KeyValuePair<String, String> _option in copyListOptionsRtl433)
            {
                listOptionsRtl433.Add(_option.Key, _option.Value);
            }
            copyListOptionsRtl433.Clear();
            foreach (String device in listBoxSelectedDevices)
            { 
                if (hideShowDevices)
                {
                   listOptionsRtl433.Add("hide" + device, "-R -" + device.Trim()); //hide protocol
                }
                else
                {
                   listOptionsRtl433.Add("show" + device, "-R " + device.Trim()); //show protocol
                }
            }
            foreach (KeyValuePair<String, String> _option in copyListOptionsRtl433)
            {
                listOptionsRtl433.Add(_option.Key, _option.Value);
            }
            copyListOptionsRtl433.Clear();
            copyListOptionsRtl433 = null;
        }

        internal void setShowDevicesDisabled( Boolean showDevicesDisabled)
        {
            if (showDevicesDisabled)
                EnabledDevicesDisabled = 1;
            else
                EnabledDevicesDisabled = 0;

            if (memo_EnabledListDevices!= EnabledDevicesDisabled)
            {
            initListDevice = false;     //need reload list devices
            }
            memo_EnabledListDevices = EnabledDevicesDisabled;
        }

        internal void setOption(String Key,String value)
        {
            if (listOptionsRtl433.ContainsKey(Key))
                listOptionsRtl433.Remove(Key);
            if(!value.Contains("No "))
                listOptionsRtl433.Add(Key, value);
        }
#endregion
#region private functions

        internal void ProcessCallMainRtl433(object parameter)
        {
            String[] args = new String[listOptionsRtl433.Count()+1];  //+1 for .exe

            args[0] = "Rtl_433.exe"; // + @"\";
            Int32 counter = 1;
            owner.setMessage("-----------RTL433 OPTIONS --------------\n");
            foreach (KeyValuePair<String, String> _option in listOptionsRtl433)
            {
                owner.setMessage(_option.Value+"\n");
                args[counter] = _option.Value;
                counter++;
            }
            owner.setMessage("------------------------------------------\n");
            Int32 argc = args.Length;
            CBmessages = new NativeMethods.ptrReceiveMessagesCallback(_callBackMessages);
            //CBReceiveRecordOrder = new NativeMethods.ptrReceiveRecordOrder(_callBackReceiveRecordOrder);
            CBinitCbData = new NativeMethods.ptrFctInit(_callBackInitCbData);
//#if CONSOLEFORM
//            NativeMethods.rtl_433_call_main(CBmessages, CBinitCbData, CBReceiveRecordOrder, (UInt32)(sampleRate), sizeof(byte), (UInt32)EnabledDevicesDisabled, argc, args, false);
//# else
            NativeMethods.rtl_433_call_main(CBmessages, CBinitCbData, CBReceiveRecordOrder, (UInt32)(sampleRate), sizeof(byte), (UInt32)EnabledDevicesDisabled, argc, args);  //, withConsole
//#endif
        }
#endregion
#region public function 
//#if TESTIME
        //private String _timeCycle = "";
        //[System.ComponentModel.Bindable(true)]
        //public String timeCycle          //no internal if binding
        //{
        //    get { return _timeCycle; }
        //    set
        //    {
        //        _timeCycle = value;
        //        OnPropertyChanged("timeCycle");
        //    }
        //}

        //[System.ComponentModel.Bindable(true)]
        //public String TimeForRtl433          //no internal if binding
        //{
        //    get { return timeForRtl433; }
        //    set
        //    {
        //        timeForRtl433 = value;
        //        OnPropertyChanged("TimeForRtl433");
        //    }
        //}
//#endif
        internal void Start(Boolean senderRadio)
        {
            owner.Start(senderRadio);
        }
        internal void Stop(Boolean senderRadio)
        {
            owner.Stop(false,senderRadio);
        }

        [System.ComponentModel.Bindable(true)]
        public String SampleRateStr          //no internal if binding
        {
            get { return sampleRateStr; }
            set
            {
                sampleRateStr =  value;
                OnPropertyChanged("SampleRate");
            }
        }

        internal double SampleRateDbl
        {
            get  { return sampleRate; }
            set
            {
            sampleRate = value;
            SampleRateStr = value.ToString();
#if TESTIME
            nbCycleFor1Sec = (Int32)((SampleRateDbl / Rtl_433_Processor.NBBYTEFORRTS_433));
#endif
            }
        }
        private String nameFile = String.Empty;
        private String nameRecord = String.Empty;
        internal  void setRecordDevice(String nameRecord, String directory )
        {
            this.nameRecord = nameRecord;
            nameFile = directory  + nameRecord.Replace(":", "_") + "_" + frequencyLng.ToString() + "_" + sampleRate.ToString() + "_" + DateTime.Now.Date.ToString("d").Replace("/", "_") + " " + DateTime.Now.Hour + " " + DateTime.Now.Minute + " " + DateTime.Now.Second + " ";
        }
        //marc ajout 2/1/2023
        internal void recordDevice(String name, String directory)
        {
            String nameFile = directory + name.Replace(":", "_") + "_" + frequencyLng.ToString() + "_" + sampleRate.ToString() + "_" + DateTime.Now.Date.ToString("d").Replace("/", "_") + " " + DateTime.Now.Hour + " " + DateTime.Now.Minute + " " + DateTime.Now.Second + " ";
            if (this.mono)
            {
                String _nameFile = nameFile + ((wavRecorder.recordType)wavRecorder.recordType.MONO + ".wav");
                wavRecorder.WriteBufferToWav(_nameFile, copyIQPtr, Rtl_433_Processor.NBCOMPLEXFORRTS_433, sampleRate, wavRecorder.recordType.MONO);
            }
            if (this.stereo)
            {
                String _nameFile = nameFile + ((wavRecorder.recordType)wavRecorder.recordType.STEREO + ".wav");
                wavRecorder.WriteBufferToWav(_nameFile, copyIQPtr, Rtl_433_Processor.NBCOMPLEXFORRTS_433, sampleRate, wavRecorder.recordType.STEREO);
            }
        }
        //fin ajout
        internal void setTypeWindowGraph(Boolean typeWindowGraph)
        {
            this.typeWindowGraph = typeWindowGraph;
#if TESTIME
            _timeCycleCumul = 0;
            _timeForRtl433Cumul = 0;
            memoDtTotalTime = 0;
#endif
        }
        private Boolean sourceIsFile = false;
        internal void setSourceName(String sourceName,Boolean sourceIsFile)
        {
            this.sourceIsFile = sourceIsFile;
            sourceName = sourceName.Replace("%20", " ");
            owner.setMessage(sourceName);
        }
#if MSGBOXDEBUG
        internal void get_version_dll_rtl_433()
        {
            String VersionC = Marshal.PtrToStringAnsi(NativeMethods.IntPtr_Pa_GetVersionText());
            MessageBox.Show("version rtl_433.dll " + VersionC, "start plugin RTL433", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //String filename = @".\xSDRSharp.Rtl_433.dll";
            //Assembly assem = Assembly.ReflectionOnlyLoadFrom(filename);
            //AssemblyName assemName = assem.GetName();
            //Version ver = assemName.Version;

            //if (VersionC != _VERSION)
            //     MessageBox.Show("version rtl_433.dll not égal to SDRSharp.Rtl_433.dll" + VersionC + "<->" + _VERSION, "SDRSharp.Rtl_433.dll");
            //Version = VersionC;  //
            //Version = "Version plugin: " + _VERSION;
        }
#endif
        //private String _version = "";
        //[System.ComponentModel.Bindable(true)]
        //public String Version
        //{
        //    get { return _version; }
        //    set
        //    {
        //        _version =  _VERSION;
        //        OnPropertyChanged("Version");
        //    }
        //}

        internal void setFrequency(Int64 value)
        {
            frequencyLng = value;
            FrequencyStr =  value.ToString();
        }

        internal void setCenterFrequency(Int64 value)
        {
            centerFrequencyLng = value;
            CenterFrequencyStr =  value.ToString(); 
        }

        [System.ComponentModel.Bindable(true)]
        public String FrequencyStr          //no internal if binding
        {
            get { return frequencyStr; }
            set
            {
                frequencyStr =  value;
                OnPropertyChanged("FrequencyStr");
                NativeMethods.setFrequency((UInt32)frequencyLng);
            }
        }
 
        [System.ComponentModel.Bindable(true)]
        public String CenterFrequencyStr          //no internal if binding
        {
            get { return centerFrequencyStr; }
            set
            {
                centerFrequencyStr = value;
                OnPropertyChanged("CenterFrequencyStr");
                NativeMethods.setCenterFrequency((UInt32)centerFrequencyLng);
            }
        }

        private void OnPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));//error on stop timeCycle
        }

        internal void call_main_Rtl_433()
        {
#if TESTIME
            _timeForRtl433Lng = 0;
            _timeCycleCumul = 0;
            _timeForRtl433Cumul = 0;
            nbCycle50000 = 0;
            memoDtTotalTime = 0;
#endif
            NativeMethods.stop_sdr(ptrCtx);
            if (!initListDevice)     //need reload list devices
                owner.setOptionVerboseInit();
            threadCallMainRTL_433 = new Thread(ProcessCallMainRtl433);
            threadCallMainRTL_433.Name = "thread_MAIN_RTL_433";
            threadCallMainRTL_433.Start();
        }
        //private int cptTest = 0;
        internal  void send_data(Complex * _IQPtr)
        {
            if (ptrCtx != IntPtr.Zero && sendDataToRtl433)
            {
                float maxi = wavRecorder.getMaxi(_IQPtr, Rtl_433_Processor.NBCOMPLEXFORRTS_433);
                if (maxi > 0)
                {
                    //l'enregistrement a partir de la fenêtre device est a revoir
                    //cette fonction send_data envoi 50000 byte a rtl433 a chaque fois.
                    //pour rtl433,2 bytes=1 échantillon=>25000 échantillons a chaque fois.

                    //si sample rate SDRSharp =250000/sec 1 cycle doit être < 100ms.
                    //si sample rate=1024000/sec 1 cycle doit être <25ms
                    //si sample rate=2048000/sec 1 cycle doit être <12.5ms
                    Thread.BeginCriticalRegion();
                        if (typeWindowGraph)  //warning if record waiting window device
                        {
                            Utils.Memcpy(copyIQPtr[0], _IQPtr, Rtl_433_Processor.NBCOMPLEXFORRTS_433 * sizeof(Complex));  //memo for record
                            for (Int32 i = Rtl_433_Processor.NBBUFFERFORRTS_433 - 1; i > 0; i--)
                                Utils.Memcpy(copyIQPtr[i], copyIQPtr[i - 1], Rtl_433_Processor.NBCOMPLEXFORRTS_433 * sizeof(Complex));  //memo for record
                        }
                        for (Int32 i = 0; i < Rtl_433_Processor.NBCOMPLEXFORRTS_433; i++)
                        {
                            dataForRs433[i * 2] = System.Convert.ToByte(127 + (_IQPtr[i].Real / maxi) * 127);
                            dataForRs433[i * 2 + 1] = System.Convert.ToByte(127 + (_IQPtr[i].Imag / maxi) * 127);
                        }
                    Thread.EndCriticalRegion();
#if TESTIME
                    stopw.Stop();
                    memoDt = stopw.ElapsedMilliseconds;
                    stopw.Restart();
#endif
                  //  Console.WriteLine("**********************receive_buffer_cb");
                    NativeMethods.receive_buffer_cb(dataForRs433, Rtl_433_Processor.NBBYTEFORRTS_433, ptrCtx);   //take time->pb memory
#if TESTIME

                    stopw.Stop();
                   
                    _timeForRtl433Lng = stopw.ElapsedMilliseconds;
                    _timeCycleCumul += (memoDt + _timeForRtl433Lng);
                    _timeForRtl433Cumul += _timeForRtl433Lng;
                     nbCycle50000 += 1;

                    if(nbCycle50000>= nbCycleFor1Sec)
                    {
                        //stopwTotalTime.Stop();
                        //Console.WriteLine(((Rtl_433_Processor.NBBYTEFORRTS_433*1000*nbCycle50000)/ stopwTotalTime.ElapsedMilliseconds).ToString());
                        //stopwTotalTime.Restart();
                        nbCycle50000 = 0;
                        if(owner!=null)  //pb on stop dispose
                            owner.setTime(_timeCycleCumul,_timeForRtl433Cumul, sourceIsFile);
                        _timeCycleCumul =0;
                        _timeForRtl433Cumul =0;
                    }
                    stopw.Restart();
#endif
                }
            }
            //if (!sendDataToRtl433)
            //    NativeMethods.stop_sdr(ptrCtx);
        }
        //#if TESTIME
        //        private void  setTime()
        //        {
        //            timeCycle =  (_timeCycleCumul ).ToString() + " ms.";
        //            timeForRtl433 =  (_timeForRtl433Cumul ).ToString() + " ms." ;
        //        }
        //#endif
//#if CONSOLEFORM
//        private Boolean withConsole = true;
//#else
        //private Boolean withConsole = false;
        //public void setWithConsole(Boolean withConsole)
        //{
        //    this.withConsole = withConsole;
        //}
//#endif
        internal void stopSendDataToRtl433() 
        {
            sendDataToRtl433 = false;
        }
        //internal void free_console() 
        //{
        //   NativeMethods.free_console();   //can error if debug try release or other version SDRSharp
        //}
        internal void CleartimeCycleMax()
        {
            //_timeCycleLngMax = 0;
            //_timeForRtl433LngMax = 0;
            ////setTime();
        }

        internal void startSendData()
        {
//#if TESTIME
            //stopwTotalTime.Restart();
//#endif
            ptrCtx = IntPtr.Zero;
            sendDataToRtl433 = true;
        }

        //public UInt32 setEnabledListDevices
        //{
        //    set
        //    {
        //        _EnabledListDevices = value;      //need reload list devices
        //        initListDevice = false;
        //        _owner.setOptionVerboseInit();
        //    }
        //}
        //private  String[] =new String _Yoption[10] ; code to v 1.5.4.4
        //public void setYoption(String option)
        //{
        //    _Yoption = option;      //need reload list devices
        //}
#endregion
#region callBack for dll_rtl_433"
        //internal void _callBackReceiveRecordOrder([In, MarshalAs(UnmanagedType.LPStr)] String message)
        //{
        //    int i = 0;
        //}


        internal  void _callBackMessages([In, MarshalAs(UnmanagedType.LPStr)] String message)
        {
            if (owner == null)
                return;
            if (initListDevice == false)
            {
                if (message.Contains("start devices list"))//start
                {
                    listeDevice.Clear();
                    startListDevice = true;
                }

                else if (message.Contains("end devices list") && startListDevice)  //stop
                {
                     initListDevice = true;
                    owner.setListDevices(listeDevice);
                }
                else
                {
                    if (message.Length > 21 && message.Contains("Registering"))
                        listeDevice.Add(message.Substring(22).Replace("]", "-"));
                }
                if (startListDevice == false)
                    owner.setMessage(message);
                return;
            }
                //return;
            if (message.Contains("**********"))//stop
            {
                if (listData.Count > 1 && synchro == true)
                {
                    Dictionary<String, String> listDataClone= new Dictionary<String, String>();
                    //lock(listData)
                    //{
                     listDataClone = listData.ToDictionary(elem => elem.Key, elem => elem.Value);
                    //}
                    //*************************GRAPH***********************************************

                    if(typeWindowGraph)
                    {
                        Int32 NumGraph =  3;
                        List<PointF>[] points = new List<PointF>[NumGraph];
                        for (Int32 i = 0; i < NumGraph ; i++)
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

                        double samples_per_us = 1000000.0 / sampleRate;
                        if (ptrCfg != IntPtr.Zero && NumGraph>0)
                        {
                            if (structCfg.demod == IntPtr.Zero)
                            {
                                try
                                {
                                    structCfg = (NativeMethods.r_cfg)Marshal.PtrToStructure(ptrCfg, typeof(NativeMethods.r_cfg));
                                }
                                catch (Exception e)
                                {
                                    MessageBox.Show(e.Message + "  ClassInterfaceWithRtl433->_callBackMessages", "Error structCfg", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            if (structCfg.demod != IntPtr.Zero)
                            {
                                //if (struct_demod.am_analyze == IntPtr.Zero)  //1.5.4.4 comment 3 lines
                                //{
                                NativeMethods.dm_state struct_demod = new NativeMethods.dm_state();//1.5.4.4 add this line
                                try
                                    {
                                        struct_demod = (NativeMethods.dm_state)Marshal.PtrToStructure(structCfg.demod, typeof(NativeMethods.dm_state));
                                    }
                                    catch (Exception e)
                                    {
                                        MessageBox.Show(e.Message + "  ClassInterfaceWithRtl433->_callBackMessages", "Error struct_demod", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                //}
#if SAMPGRAD
                                ////if ((mono || stereo || raw) && nameRecord != String.Empty)
                                ////{
                                    if (struct_demod.ptr_samp_grab != IntPtr.Zero)
                                    {
                                        NativeMethods.samp_grab struct_demod_samp_grab = new NativeMethods.samp_grab();
                                        try
                                        {
                                            struct_demod_samp_grab = (NativeMethods.samp_grab)Marshal.PtrToStructure(struct_demod.ptr_samp_grab, typeof(NativeMethods.samp_grab));
                                            if (struct_demod_samp_grab.sg_buf != null)
                                            {
                                                if (struct_demod_samp_grab.sg_size > 0)
                                                {
                                                //for record,receive device but struct_demod.frame_end_ago < 25000
                                                if (struct_demod.frame_start_ago > 0 && struct_demod.frame_end_ago > 25000)
                                                    {
                                                        if (samp_grab_write(struct_demod_samp_grab, struct_demod.frame_start_ago, struct_demod.frame_end_ago))
                                                        {
                                                            owner.resetLabelRecord(nameRecord);
                                                            clearRecord();
                                                        }
                                                    }
                                                }
                                                else
                                                 Console.WriteLine("**********************struct_demod_samp_grab.sg_size=0");
                                            }
                                            else
                                                Console.WriteLine("**********************struct_demod_samp_grab.sg_buf=0");
                                        }
                                        catch (Exception e)
                                        {
                                            MessageBox.Show(e.Message + "**********************ClassInterfaceWithRtl433->_callBackMessages", "Error struct_demod", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                    else
                                        Console.WriteLine("**********************struct_demod.ptr_samp_grab=0");
                                ////}
#endif
                                Int32 x = 0;
                                if (struct_demod.pulse_data.num_pulses > 0)
                                {
                                    for (Int32 bit = 0; bit < (struct_demod.pulse_data.num_pulses); bit++)
                                    {
                                        x += (Int32)(struct_demod.pulse_data.pulse[bit] * samples_per_us);
                                        points[0].Add(new PointF(x, 1));
                                        points[0].Add(new PointF(x, 0));
                                        x += (Int32)(struct_demod.pulse_data.gap[bit] * samples_per_us);
                                        points[0].Add(new PointF(x, 0));
                                        points[0].Add(new PointF(x, 1));
                                    }
                                }
                                else if (struct_demod.fsk_pulse_data.num_pulses > 0)
                                {
                                    for (Int32 bit = 0; bit < (struct_demod.fsk_pulse_data.num_pulses); bit++)
                                    {
                                        x += (Int32)(struct_demod.fsk_pulse_data.pulse[bit] * samples_per_us);
                                        points[0].Add(new PointF(x, 1));
                                        points[0].Add(new PointF(x, 0));
                                        x += (Int32)(struct_demod.fsk_pulse_data.gap[bit] * samples_per_us);
                                        points[0].Add(new PointF(x, 0));
                                        points[0].Add(new PointF(x, 1));
                                    }
                                }
                                 if (NumGraph > 1)
                                {
                                    Int32 endData = searchZero(struct_demod.am_buf);
                                    for (Int32 bit = 0; bit < endData; bit++)
                                    {
                                        for (Int32 b = bit; b < bit + 10; b++)
                                        {
                                            points[1].Add(new PointF(bit, (Int32)(struct_demod.am_buf[bit] * samples_per_us)));  //out of memory
                                        }
                                    }
                                }
                                if (NumGraph > 2)
                                {
                                    Int32 endData = searchZero(struct_demod.fm);
                                    for (Int32 bit = 0; bit < endData; bit++)
                                    {
                                        for (Int32 b = bit; b < bit + 10; b++)
                                        {
                                            points[2].Add(new PointF(bit, (Int32)(struct_demod.fm[bit] * samples_per_us)));
                                        }
                                    }
                                }
                            }
                        }
                        owner.addFormDevice(listDataClone, points,nameGraph);
                        //Console.WriteLine("**********************addFormDevice");
#if TESTMEMORY
                        Console.WriteLine($"{currentProcess} -");
                        Console.WriteLine("-------------------------------------");

                        //Console.WriteLine($"  Physical memory usage     : {currentProcess.WorkingSet64}");
                        //Console.WriteLine($"  Base priority             : {currentProcess.BasePriority}");
                        //Console.WriteLine($"  Priority class            : {currentProcess.PriorityClass}");
                        //Console.WriteLine($"  User processor time       : {currentProcess.UserProcessorTime}");
                        //Console.WriteLine($"  Privileged processor time : {currentProcess.PrivilegedProcessorTime}");
                        //Console.WriteLine($"  Total processor time      : {currentProcess.TotalProcessorTime}");
                        Console.WriteLine($"  Paged system memory size  : {currentProcess.PagedSystemMemorySize64}");
                        Console.WriteLine($"  Paged memory size         : {currentProcess.PagedMemorySize64}");
                        Console.WriteLine($"  Private memory size       : {currentProcess.PrivateMemorySize64}");
                        Console.WriteLine($"  Non paged memory size     : {currentProcess.NonpagedSystemMemorySize64}");
                         Console.WriteLine($"  Virtual memory size       : {currentProcess.VirtualMemorySize64}");
                        //if (currentProcess.Responding)
                        //{
                        //    Console.WriteLine("Status = Running");
                        //}
                        //else
                        //{
                        //    Console.WriteLine("Status = Not Responding");
                        //}
                // Display peak memory statistics for the process.
                Console.WriteLine($"  Peak physical memory usage : { currentProcess.PeakPagedMemorySize64}");
                Console.WriteLine($"  Peak paged memory usage    : { currentProcess.PeakVirtualMemorySize64}");
                Console.WriteLine($"  Peak virtual memory usage  : {currentProcess.PeakWorkingSet64}");
#endif
                    }
                    else
                    {
                        owner.addFormDevice(listDataClone, null, nameGraph);
                        //Console.WriteLine("**********************addFormDevice");
                        //Console.WriteLine(cptTest);
                        //cptTest = 0;
                    }
                    //*************************END GRAPH***********************************************
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
                owner.setMessage(message);
        }

        internal void _callBackInitCbData([In, MarshalAs(UnmanagedType.FunctionPtr)] IntPtr _ptrCbData,
            //[In, MarshalAs(UnmanagedType.SysInt)] Int32 _bufNumber,
            //[In, MarshalAs(UnmanagedType.SysUInt)] UInt32 _bufLength,
            [In, MarshalAs(UnmanagedType.FunctionPtr)] IntPtr _ptrCtx,
            [In, MarshalAs(UnmanagedType.FunctionPtr)] IntPtr _ptrCfg)
        {
            ptrCbData = _ptrCbData;
            //bufNumber = _bufNumber;
            //bufLength = _bufLength;
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

        public void Dispose()
        {
             Dispose(true);
        }
        protected virtual void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (threadCallMainRTL_433!=null)
                {
                    if (threadCallMainRTL_433.IsAlive)
                        threadCallMainRTL_433.Join(1000);
                    threadCallMainRTL_433 = null;
                }
                if (owner != null)
                     owner = null;
                //copyIQPtr = null;
                //copyIQBuffer = null;
                GC.SuppressFinalize(this);
#if TESTIME
                stopw = null;
                //stopwTotalTime = null;
#endif
            }
         }
#endregion
        //private static Byte[] CopyTabToDest(IntPtr Source, UInt32 len)
        //{
        //    Byte[] Dest = new byte[len];
        //    // If the Target is nothing, immediate return of nothing.
        //    if (Source == null) return null;

        //    // get an instance of the WINAPI class that
        //    // holds the API functions. 
        //   // WINAPI API = new WINAPI();

        //    // Declare an IntPtr which will hold a memory address, don't
        //    // panic here! Just keep going! 
        //    //IntPtr p_objTarget;

        //    try
        //    {
        //        // Call AllocHGlobal to allocate enough memory on the heap
        //        // for a 'Target' object. AllocHGlobal returns a pointer
        //        // to this memory, which is needed for the next call. 
        //        //p_objTarget = Marshal.AllocHGlobal(Marshal.SizeOf(Target));

        //        // To copy Target to the heap, use StructureToPtr
        //        // as in the following line of code. This is useful
        //        // for API calls that require prefilled structures.
        //        //Marshal.StructureToPtr(Target, p_objTarget, true);

        //        // Use CopyMemory to take the data from the source string
        //        // (Source) and copy it to the block of memory on the
        //        // heap (which, coincidentally is the same size as the
        //        // source string, we defined sizes in our class definition).
        //        NativeMethods.CopyMemory(Dest, Source, len);

        //        // Now, tell the Marshaler to copy the data on the heap
        //        // (the results of CopyMemory) into our instance of the
        //        // Target object (clsTest).
        //        //Marshal.PtrToStructure(p_objTarget, Target);

        //        // Free the memory that was allocated on the heap, otherwise
        //        // you will create a memory leak. 
        //        //Marshal.FreeHGlobal(p_objTarget);
        //    }
        //    //catch (System.OutOfMemoryException ex)
        //    //{
        //    //    // An exception could occur if the system is out of
        //    //    // memory and the block of heap memory could not be
        //    //    // set aside for you. 
        //    //    Console.WriteLine("Exception Caught: " + ex.Message);
        //    //    //CoughUpCookies(ex);
        //    //}
        //    catch (Exception e)
        //    {
        //        // General exception caught, show the message and move on...
        //        //CoughUpCookies(e);
        //        Console.WriteLine("Exception Caught: " + e.Message);
        //    }

        //    // Free resources assigned to our instance of the WINAPI class 
        //    //API = null;

        //    // send the results back for printing 
        //    return Dest;   //   (Object)Target;

        //}
//#if SAMPGRAD
        internal void clearRecord()
        {
            nameFile = String.Empty;
            nameRecord = String.Empty;
        }


        private Boolean mono = false;
        internal void setMONO(Boolean mono)
        {
            this.mono = mono;
        }
        private Boolean stereo = false;
        internal void setSTEREO(Boolean stereo)
        {
            this.stereo = stereo;
        }

        private Boolean raw = false;
        internal void setRAW(Boolean raw)
        {
            this.raw = raw;
        }
#if SAMPGRAD
        //internal void setNameFile(String nameFile, String directory,String name)
        //{
        //    //this.nameFile = nameFile;
        //    this.nameFile = directory + name.Replace(":", "_") + "_" + frequencyLng.ToString() + "_" + sampleRate.ToString() + "_" + DateTime.Now.Date.ToString("d").Replace("/", "_") + " " + DateTime.Now.Hour + " " + DateTime.Now.Minute + " " + DateTime.Now.Second + " ";
        //    if (mono)
        //        this.nameFile = this.nameFile + ((wavRecorder.recordType)wavRecorder.recordType.MONO + ".wav");
        //    if (stereo)
        //       this.nameFile = this.nameFile + ((wavRecorder.recordType)wavRecorder.recordType.STEREO + ".wav");
        //}
        private const UInt32 BLOCK_SIZE = (128 * 1024); /* bytes */
        /// <summary>
        /// function  samp_grab_write() from samp_grab.c 
        /// </summary>
        /// <param name="struct_demod_samp_grab"></param>
        /// <param name="frame_start_ago"></param>
        /// <param name="frame_end_ago"></param>
        /// <returns></returns>
        private unsafe Boolean samp_grab_write(NativeMethods.samp_grab struct_demod_samp_grab, UInt32 frame_start_ago, UInt32 frame_end_ago)
        {
            //Protocol_3 Model_ Prologue-TH Channel_1_433920000_250000 512k en cu8 2049k en .wav OK en .raw(source vasili) ok
            //Protocol_138 OK
            //Protocol_196 OK
            UInt32 frame_pad = 25000 / 8; // this could also be a fixed value, e.g. 10000 samples
            UInt32 start_padded = frame_start_ago + frame_pad;
            UInt32 end_padded = frame_end_ago - frame_pad;
            UInt32 len_padded = start_padded - end_padded;
            UInt32 end_pos = 0;
            UInt32 start_pos = 0;
            UInt32  wlen=0;
            UInt32 wrest=0;
            UInt32 signal_bsize=0;
            signal_bsize = 2 * len_padded;
            signal_bsize += BLOCK_SIZE - (signal_bsize % BLOCK_SIZE);
            if (signal_bsize > struct_demod_samp_grab.sg_len)
            {
                //fprintf(stderr, "Signal bigger than buffer, signal = %u > buffer %u !!\n", signal_bsize, g->sg_len);
                signal_bsize = struct_demod_samp_grab.sg_len;
            }
            end_pos = (UInt32)Marshal.ReadInt32(struct_demod_samp_grab.sample_size) * end_padded;
            if (struct_demod_samp_grab.sg_index >= end_pos)
                end_pos = struct_demod_samp_grab.sg_index - end_pos;
            else
                end_pos = struct_demod_samp_grab.sg_size - end_pos + struct_demod_samp_grab.sg_index;
            if (end_pos >= signal_bsize)
                start_pos = end_pos - signal_bsize;
            else
                start_pos = struct_demod_samp_grab.sg_size - signal_bsize + end_pos;
            wlen = signal_bsize;
            wrest = 0;
            if (start_pos > struct_demod_samp_grab.sg_size)
            {
                Console.WriteLine("start_pos > struct_demod_samp_grab.sg_size");
                return false;
            }
            if (start_pos + signal_bsize > struct_demod_samp_grab.sg_size)
            {
                wlen = struct_demod_samp_grab.sg_size - start_pos;
                wrest = signal_bsize - wlen;
            }
            if (wrest > 0)
                wrest = wrest;
            return false;
            String format = Marshal.ReadInt32(struct_demod_samp_grab.sample_size) == 2 ? "cu8" : "cs16";
            double freq_mhz = frequencyLng / 1000000.0;
            double rate_khz = sampleRate / 1000.0;
            Byte[] buffer_samp_grab = CopyTabToDest((IntPtr)((UInt32)struct_demod_samp_grab.sg_buf+start_pos),  wlen);

            //****************save file to cu8****************
            //String nameFile = "Test" + "_" + freq_mhz.ToString() + "_" + rate_khz.ToString() + "_" + DateTime.Now.Date.ToString("d").Replace("/", "_") + " " + DateTime.Now.Hour + " " + DateTime.Now.Minute + " " + DateTime.Now.Second + " ";
            //nameFile += ((wavRecorder.recordType)wavRecorder.recordType.STEREO + ".wav");
            
                wavRecorder.writeCu8ToWav(nameFile, buffer_samp_grab, mono, stereo,raw);
            
            //wavRecorder.writeByte(nameFile + ".raw", buffer_samp_grab);//for debug
            //if (stereo)
            //    wavRecorder.writeCu8ToWav(nameFile, buffer_samp_grab, owner.getRecordSTEREO(), owner.getRecordSTEREO());
            //****************end save file to cu8************
            return true;
        }
#endif
    }
}
