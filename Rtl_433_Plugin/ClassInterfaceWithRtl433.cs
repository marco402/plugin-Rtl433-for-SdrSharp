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
//#define noV1632   two case ok with 1632 and file _Protocol_1_Model__Silvercrest-Remote_433920000hz_250k_08_02_2025_10_47_38.wav
using SDRSharp.Radio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

namespace SDRSharp.Rtl_433
{
    enum Package_types
    {
        PULSE_DATA_OOK = 1,
        PULSE_DATA_FSK = 2,
    };
    internal partial class ClassInterfaceWithRtl433 : INotifyPropertyChanged, IDisposable
    {
#region declarations
        internal float[] memoDataIQForRs433=new float[NativeMethods.SIZE_BUFFER_MEMO_COMPLEX_IQ];
        private Int32 ptrMemoDataForRs433 = 0;
        public event PropertyChangedEventHandler PropertyChanged;
        static NativeMethods.ptrReceiveMessagesCallback CBmessages;
        static NativeMethods.ptrReceiveStructDevicesCallback CBStructDevices;
        static NativeMethods.ptrFctInit CBinitCbData;
        private Thread threadCallMainRTL_433;
        private Dictionary<String, String> listOptionsRtl433;
        private Rtl_433_Panel panelRtl_433;
        private Int64 frequencyLng = 0;
        private Int64 centerFrequencyLng = 0;
        private String frequencyStr = "";
        private String centerFrequencyStr = "";
        private UInt32 EnabledDevicesDisabled = 0;             //=1=> treat devices with disabled =1
        private readonly List<String> listeDevice = new List<String>();
        private IntPtr ptrCtx = IntPtr.Zero;
        private IntPtr ptrDemod = IntPtr.Zero;
        private Boolean sendDataToRtl433 = false;
        private Boolean initListDevice = false;
        private Boolean startListDevice = false;
        private UInt32 memo_EnabledListDevices = 0;
#if DEBUG
        private Stopwatch stopw;
        private Stopwatch stopwRtl433;
        private float _timeForRtl433Float = 0;
        private float memoDt;
        private float _timeCycleCumul = 0;
        private float _timeForRtl433Cumul = 0;
        private float _timeDisplayCumul = 0;
        private Int32 nbCycle50000 = 0;
        private float nbCycleFor1Sec = 0;
#endif
        private Boolean sourceIsFile = false;
        private String sampleRateStr = "";
        private Double sampleRate = 0;
        private ClassTraceGraphe myClassTraceGraphe=null;
#endregion

#region public function 
        internal ClassInterfaceWithRtl433(Rtl_433_Panel owner)
        {
            Debug.WriteLine($"ClassInterfaceWithRtl433 {owner}");
#if DEBUG
            stopw = new Stopwatch();
            stopwRtl433 = new Stopwatch();
#endif
            this.panelRtl_433 = owner;
            SampleRateStr = "0";  //no display if init to Rtl_433_panel
            InitOptionsRTL433();
            myClassTraceGraphe = new ClassTraceGraphe();
          }

        internal void ThreadCallMainRtl433(object parameter)
        {
            ptrMemoDataForRs433 = 0;
            String[] args = GetArg();
            Int32 argc = args.Length;
            CBmessages = new NativeMethods.ptrReceiveMessagesCallback(CallBackMessages);
            CBStructDevices = new NativeMethods.ptrReceiveStructDevicesCallback(CallBackDevices);
            CBinitCbData = new NativeMethods.ptrFctInit(CallBackInitCbData);
            UInt32 sampleRateDecim = (UInt32)(Math.Round(sampleRate / Rtl_433_Processor.decimation)); //see for pass samplerate to float in RTL_433
            NativeMethods.rtl_433_call_main(CBmessages, CBinitCbData, CBStructDevices, sampleRateDecim, sizeof(byte), (UInt32)EnabledDevicesDisabled, this.sourceIsFile, argc, args);
            args = null;
        }
        internal void Start(Boolean senderRadio)
        {
            panelRtl_433.Start(senderRadio);
        }
        //private String sourceName="";
        internal void SetSourceName(String sourceName, Boolean sourceIsFile)
        {
            this.sourceIsFile = sourceIsFile;
            sourceName = sourceName.Replace("%20", " ");
            //this.sourceName = sourceName.Replace("%20", " ");
            panelRtl_433.SetMessage(sourceName + "\n");
            panelRtl_433.SetSourceType(sourceIsFile);
        }
        internal void SetFrequency(Int64 value)
        {
            frequencyLng = value;
            FrequencyStr = value.ToString();
            if (panelRtl_433 != null)
                panelRtl_433.SetFrequency(frequencyLng);
        }
        internal void SetCenterFrequency(Int64 value)
        {
            centerFrequencyLng = value;
            CenterFrequencyStr = value.ToString();
        }
        [System.ComponentModel.Bindable(true)]
        public String FrequencyStr          //no internal if binding
        {
            get { return frequencyStr; }
            set
            {
                frequencyStr = value;
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
        internal void Call_main_Rtl_433()
        {
#if DEBUG
            _timeCycleCumul = 0;
            _timeForRtl433Cumul = 0;
            nbCycle50000 = 0;
#endif
            if (ptrCtx != IntPtr.Zero)
                NativeMethods.stop_sdr(ptrCtx);
            if (!initListDevice)     //need reload list devices
                panelRtl_433.SetOptionVerboseInit();
//#if DEBUG
//            SetOption("analyze", "-A", true);
//#endif
            threadCallMainRTL_433 = null;

            threadCallMainRTL_433 = new Thread(ThreadCallMainRtl433)
                {
                    Name = "thread_MAIN_RTL_433"
                };

            threadCallMainRTL_433.Start();
        }
#if TESTBOUCLEREPLAYMARC
        private Int32 cptBoucle=500;
        private String memoFilePB = "";
#endif
        internal unsafe void Send_data(Complex* _IQPtr, Int32 len)
        {
////#if DEBUG
////            int deb = 0;
////            if (Math.Abs(_IQPtr[0].Real + 0.7165354)<0.00001)
////                deb = deb;
////#endif
#if TESTBOUCLEREPLAYMARC
            cptBoucle--;
            if (cptBoucle == 0)
            {
                if(memoFilePB!="")
                {
                Trace.WriteLine("time-out:  " + this.sourceName);
                    //copy to pbFiles
                }
                memoFilePB = this.sourceName;
                panelRtl_433.controlStopRadio();
                cptBoucle = 500;  //50
            }
#endif
            Int32 maxiBuffer = NativeMethods.SIZE_BUFFER_MEMO_COMPLEX_IQ-1;  //for time
            if (ptrCtx != IntPtr.Zero && sendDataToRtl433)
            {
                float coefficient = ClassUtils.GetMaxiPtrComplex(_IQPtr, len);
//#if DEBUG
//                if (coefficient != 1f)
//                    Debug.WriteLine("coefficient!=1-->" + coefficient.ToString());
//#endif
                if (coefficient > 0.0)
                {
                    Thread.BeginCriticalRegion();
                    Int32 len2 = len * 2;
                    byte[] dataForRs433 = new byte[len2];
#if V1632
                    if (ptrMemoDataForRs433 + len2 > NativeMethods.SIZE_BUFFER_MEMO_COMPLEX_IQ)
                    {
                        Int32 lenEnd = NativeMethods.SIZE_BUFFER_MEMO_COMPLEX_IQ - ptrMemoDataForRs433;
                        ClassUtils.ComplexToFloatArray(_IQPtr, memoDataIQForRs433, ptrMemoDataForRs433, lenEnd);
                        Int32 lenStart = len2 - lenEnd;
                        ClassUtils.ComplexToFloatArray(_IQPtr, memoDataIQForRs433, 0, lenStart);
                    }
                    else
                        ClassUtils.ComplexToFloatArray(_IQPtr, memoDataIQForRs433, ptrMemoDataForRs433, len2);
#else
                    // R and I inversed
                   //int pp = ptrMemoDataForRs433;
                   if (ptrMemoDataForRs433 + len2 > NativeMethods.SIZE_BUFFER_MEMO_COMPLEX_IQ)
                    {
                        Int32 lenEnd = (NativeMethods.SIZE_BUFFER_MEMO_COMPLEX_IQ - ptrMemoDataForRs433)/2;
                        ClassUtils.ComplexToFloatArray(_IQPtr, memoDataIQForRs433, ptrMemoDataForRs433, lenEnd);

                        ////for (int p = 0;p < lenEnd;p++)
                        ////{
                        ////    memoDataIQForRs433[pp++] = _IQPtr[p].Imag;
                        ////    memoDataIQForRs433[pp++] = _IQPtr[p].Real;
                        ////}
                        Int32 lenStart = len - lenEnd;
                        ////for (int p = 0;p < lenStart; p++)
                        ////{
                        ////    memoDataIQForRs433[p++] = _IQPtr[p].Imag;
                        ////    memoDataIQForRs433[p++] = _IQPtr[p].Real;
                        ////}
                        ClassUtils.ComplexToFloatArray(_IQPtr, memoDataIQForRs433, 0, lenStart);
                    }
                    else
                        ////for (int p = 0;p < len; p++)
                        ////{
                        ////     memoDataIQForRs433[pp++] = _IQPtr[p].Imag;
                        ////     memoDataIQForRs433[pp++] = _IQPtr[p].Real;
                        //// }
                        ClassUtils.ComplexToFloatArray(_IQPtr, memoDataIQForRs433, ptrMemoDataForRs433, len2);

#if DEBUG
//if(ptrMemoDataForRs433>127000*2)
//                    {
//                        String message = "";
//                        for (int i = 0; i < 100; i++)
//                            message += ($"buffer[{i}]={memoDataIQForRs433[i]}\n");

//                        //panelRtl_433.SetMessage(($"buffer[{900}].Real={buffer[900].Real}  buffer[{900}].Imag={buffer[900].Imag}\n"));
//                        for (int i = 126800*2; i < 126900*2; i++)
//                            message += ($"buffer[{i}]={memoDataIQForRs433[i]}\n");
//                        Debug.WriteLine(message);
//                    }

#endif

#endif
                    //Int32 j = 0;
                    //Int32 i = 0;
                    //coefficient = (coefficient * 2f) ;  //val for 1
                    //for (i = 0; i < len; i++)
                    //{
                    //    j = i * 2;

                    //    try
                    //    {
                    //        dataForRs433[j] = Convert.ToByte(127+memoDataIQForRs433[ptrMemoDataForRs433] * coefficient);
                    //        dataForRs433[j + 1] = Convert.ToByte(127 + memoDataIQForRs433[ptrMemoDataForRs433 + 1] * coefficient);
                    //    }
                    //    catch
                    //    {
                    //        dataForRs433[j] = 0;
                    //        dataForRs433[j + 1] = 0;
                    //        Debug.WriteLine("classInterface-> " + (127 + memoDataIQForRs433[ptrMemoDataForRs433] * coefficient));
                    //        Debug.WriteLine("classInterface-> " + (127 + memoDataIQForRs433[ptrMemoDataForRs433 + 1] * coefficient));
                    //    }
                    //    ptrMemoDataForRs433 += 2;
                    //    if (ptrMemoDataForRs433 > maxiBuffer)
                    //        ptrMemoDataForRs433 = 0;
                    //    }


                    for (int j = 0; j < len2;)
                    {
                        try
                        {
                            //Debug.WriteLine(j+" "+ptrMemoDataForRs433);
                            dataForRs433[j++] = System.Convert.ToByte(ClassConst.FLOATTOBYTE + (memoDataIQForRs433[ptrMemoDataForRs433++] / coefficient) * ClassConst.FLOATTOBYTE);
                        }
                        catch
                        {
                            if ((ClassConst.FLOATTOBYTE + (memoDataIQForRs433[ptrMemoDataForRs433-1] / coefficient) * ClassConst.FLOATTOBYTE) < 0)
                                dataForRs433[j-1] = 0;
                            else
                                dataForRs433[j-1] = 255;

                            Debug.WriteLine("classInterface-> " + (ClassConst.FLOATTOBYTE + (memoDataIQForRs433[ptrMemoDataForRs433++] / coefficient) * ClassConst.FLOATTOBYTE));
                        }
               //try
               //         {
               //             dataForRs433[j++] = System.Convert.ToByte(127.5f + (memoDataIQForRs433[ptrMemoDataForRs433++] / coefficient) * 127.5f);
               //         }
               //         catch
               //         {
               //             dataForRs433[j++] = 0;
               //             Debug.WriteLine("classInterface-> " + (127.5f + (memoDataIQForRs433[ptrMemoDataForRs433++] / coefficient) * 127.5f));
               //         }
                        //ptrMemoDataForRs433 += 2;
                            if (ptrMemoDataForRs433 > maxiBuffer)
                                ptrMemoDataForRs433 = 0;
                    }
                    //if (i != len)
                    //        Debug.WriteLine("  ClassInterfaceWithRtl433->i != len");

                    Thread.EndCriticalRegion();
#if DEBUG
                    stopw.Stop();
                    memoDt = stopw.ElapsedMilliseconds;
                    _timeForRtl433Float = 0;
                    stopwRtl433.Restart();
#endif
                    NativeMethods.receive_buffer_cb(dataForRs433, (UInt32)len * 2, ptrCtx);
                    dataForRs433 = null;
#if DEBUG
                    stopwRtl433.Stop();
                    _timeForRtl433Float += stopwRtl433.ElapsedMilliseconds;


                    float timeCycle = memoDt;
                    _timeCycleCumul += timeCycle;
                    _timeForRtl433Cumul += _timeForRtl433Float;
                    nbCycle50000 ++;
                    _timeDisplayCumul+= panelRtl_433.TimeDisplay;
                    if (nbCycle50000 >= nbCycleFor1Sec)
                    {
                        if (panelRtl_433 != null)  //pb on stop dispose
                        {
                            nbCycle50000 = 0;
                            float delta = nbCycleFor1Sec - (float)Math.Floor(nbCycleFor1Sec);
                            _timeDisplayCumul = (_timeDisplayCumul - panelRtl_433.TimeDisplay) + panelRtl_433.TimeDisplay * delta;
                            _timeForRtl433Cumul = (_timeForRtl433Cumul - _timeForRtl433Float) + _timeForRtl433Float * delta;
                            _timeCycleCumul = (_timeCycleCumul - timeCycle) + (timeCycle * delta)+_timeDisplayCumul+_timeForRtl433Cumul;
                            panelRtl_433.SetTime(_timeCycleCumul, _timeForRtl433Cumul,_timeDisplayCumul ,sourceIsFile);
                        }
                        _timeCycleCumul = 0;
                        _timeForRtl433Cumul = 0;
                        _timeDisplayCumul = 0;
                    }
                    if (stopw != null)
                        stopw.Restart();
#endif
                }
            }
        }
        internal void StopSendDataToRtl433()
        {
            sendDataToRtl433 = false;
        }
        internal void StartSendData()
        {
            ptrCtx = IntPtr.Zero;
            sendDataToRtl433 = true;
        }
        [System.ComponentModel.Bindable(true)]
        public String SampleRateStr          //no internal if binding
        {
            get { return sampleRateStr; }
            set
            {
                sampleRateStr = value;
                OnPropertyChanged("SampleRateStr");
            }
        }
        internal double SampleRateDbl
        {
            get { return sampleRate; }
            set
            {
                sampleRate = value;
                SampleRateStr = value.ToString();
#if DEBUG
                nbCycleFor1Sec = (float)((SampleRateDbl / Rtl_433_Processor.nbByteForRtl433));
#endif
            }
        }
#endregion
#region private functions
        private void OnPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));//error on stop timeCycle
        }
#endregion

#region callBack for dll_rtl_433"
        private Dictionary<String, String> FillsListDataClone(NativeMethods.R_deviceToPlugin info)
        {
            Dictionary<String, String> listData = new Dictionary<String, String>();
            String Key;
            String Value;
#if ANALYZE
            String memoModulation ="";
            Boolean Analyz = false;
#endif
            //Int32 tiret = 1;
            //
            for (Int32 i = 0; i < info.nbInfosDevice; i++)
            {
                IntPtr strPtrKey = (IntPtr)Marshal.PtrToStructure(info.Key_Device, typeof(IntPtr));
                Key = ClassUtils.FirstCharToUpper(Marshal.PtrToStringAnsi(strPtrKey));    //mainly for channel or Channel...limit nb column
                info.Key_Device = new IntPtr(info.Key_Device.ToInt64() + IntPtr.Size);
                //add : to end and delete spaces before it
                Key = Key.TrimEnd(' ');
                Key = Key.TrimEnd(':');
                Key = Key.TrimEnd(' ');
                Key = Key.Insert(Key.Length, ":");
 
                 IntPtr strPtrValue = (IntPtr)Marshal.PtrToStructure(info.Value_Device, typeof(IntPtr));
                Value = (Marshal.PtrToStringAnsi(strPtrValue));
#if ANALYZE
                if (Key == "Channel:" && Value == "9999")
                {
                    Analyz = true;
                    //continue;
                }
                if (Key == "Modulation:")
                {
                    memoModulation = Value;
                    //continue;
                }
#endif
                info.Value_Device = new IntPtr(info.Value_Device.ToInt64() + IntPtr.Size);
                if (Key == "Modulation:")
                    Value = getModulation(Value);
               
                try
                {
                    listData.Add(Key, Value);
                }
                catch
                {
                    //Key = Key.PadRight(tiret+Key.Length, '-');
                    //listData.Add(Key, Value);
                    //tiret += 1;
                }

            }
#if ANALYZE
            listData.Add("Raw Message:", info.row_bits);

            //if (Analyz)
            //{
                listData["Channel:"] = memoModulation;  // listData["Period:"];
                listData.Add("Raw Message\\:", info.row_bitsBarre);
            //}
#endif
                //else
                //{ }
                //    listData["Modulation:"] = getModulation(listData["Modulation:"]);
                return listData;
        }

        private String getModulation(String modulation)
        {
            String Modulation = "";
            switch(modulation)
            {
                case "3":
                    Modulation = "OOK_MANCHESTER";
                    break;
                case "4":
                    Modulation = "OOK_PCM or RZ";
                    break;
                case "5":
                    Modulation = "OOK_PPM";
                    break;
                case "6":
                    Modulation = "OOK_PWM";
                    break;
                case "8":
                    Modulation = "OOK_PIWM_RAW";
                    break;
                case "9":
                    Modulation = "OOK_DMC";
                    break;
                case "10":
                    Modulation = "OOK_PWM_OSV1";
                    break;
                case "11":
                    Modulation = "OOK_PIWM_DC";
                    break;
                case "12":
                    Modulation = "OOK_NRZS";
                    break;
                case "16":
                    Modulation = "FSK_PULSE_PCM";
                    break;
                case "17":
                    Modulation = "FSK_PWM";
                    break;
                case "18":
                    Modulation = "FSK_MANCHESTER";
                    break;
                default:
                    Modulation = "unknown modulation";
                    break;
            }
            return Modulation;
        }

        private Boolean selectFormGraph;
        internal Boolean SetSelectFormGraph
        {
            set
            {
                selectFormGraph = value;
#if DEBUG
                _timeCycleCumul = 0;
                _timeForRtl433Cumul = 0;
#endif
            }
        }
        internal void CallBackDevices(IntPtr ptrInfosToPlugin)
        {
            if (panelRtl_433 == null)
                return;
#if DEBUG
            stopwRtl433.Stop();
            _timeForRtl433Float = stopwRtl433.ElapsedMilliseconds;
#endif
            NativeMethods.R_deviceToPlugin info = new NativeMethods.R_deviceToPlugin();
            try
            {
                info = (NativeMethods.R_deviceToPlugin)Marshal.PtrToStructure(ptrInfosToPlugin, typeof(NativeMethods.R_deviceToPlugin));
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message + "  ClassInterfaceWithRtl433->_callBackDevices", "Error ptrInfosToPlugin");
                return;
            }
            Dictionary<String, String> listData = FillsListDataClone(info);

            //*************************GRAPH***********************************************

            //if (info.lenForRecord == 0)
            //{
            //    panelRtl_433.SetMessage("info.lenForRecord = 0 for " + GetProtocol(listData) + "\n");
            //    panelRtl_433. TreatForms(listData);
            //    return;
            //}
            if (selectFormGraph)
            {
                //util for treatGraph with one parameter
                List<PointF>[] points = null;
                double SampleRateDecime = SampleRateDbl / Rtl_433_Processor.decimation;

                points = myClassTraceGraphe.TreatGraph(info, listData, SampleRateDecime, ptrDemod, frequencyStr,out string[] nameGraph, memoDataIQForRs433, ptrMemoDataForRs433, out float[] dataIQForRecord, panelRtl_433);
                if (points!=null)
                {
#if ANALYZE
                    panelRtl_433.TreatForms(listData);
#else
                    panelRtl_433.TreatForms(listData, points,nameGraph, dataIQForRecord, (Int32)SampleRateDecime, FrequencyStr);
#endif
                }
                else
                {
#if DEBUG
                    panelRtl_433.SetMessage("(points[0].Count = 0 for " + GetProtocol(listData) + "\n");
#endif
                    panelRtl_433.TreatForms(listData);
                }
            }
            else
            {
                panelRtl_433.TreatForms(listData);
            }
#if DEBUG
            stopwRtl433.Restart();
#endif
        }
        internal void CallBackMessages([In, MarshalAs(UnmanagedType.LPStr)] String message)
        {
            if (panelRtl_433 == null)
                return;
            //init devices list to listeDevice
            if (!initListDevice)
            {
                if (message.Contains("start devices list"))//start
                {
                    listeDevice.Clear();
                    startListDevice = true;
                }

                else if (message.Contains("end devices list") && startListDevice)  //stop
                {
                    initListDevice = true;
                    panelRtl_433.SetListDevices(listeDevice);
                }
                else
                {
                    if (message.Length > 21 && message.Contains("Registering"))
                        listeDevice.Add(message.Substring(22).Replace("]", "-"));
                }
                if (!startListDevice)
                    panelRtl_433.SetMessage(message);
                return;
            }
            panelRtl_433.SetMessage(message);  //all other messages to listViewConsole 
        }

        internal void CallBackInitCbData([In, MarshalAs(UnmanagedType.FunctionPtr)] IntPtr _ptrCbData,
            [In, MarshalAs(UnmanagedType.FunctionPtr)] IntPtr _ptrCtx,
            [In, MarshalAs(UnmanagedType.FunctionPtr)] IntPtr _ptrDemod)
        {
            //ptrCbData = _ptrCbData;
            ptrCtx = _ptrCtx;
            ptrDemod = _ptrDemod;
        }
#endregion
        private static String GetProtocol(Dictionary<String, String> listData)
        {
            string protocol = "";
            foreach (KeyValuePair<String, String> _line in listData)
            {
                if (_line.Key.ToUpper().Contains("MODEL"))
                {
                    protocol = _line.Value;
                }
            }
            return protocol;
        }
#region devices form
        public void Dispose()
        {
            Debug.WriteLine("ClassInterfaceWithRtl433->Dispose");
            Dispose(true);

        }
        protected virtual void Dispose(Boolean disposing)
        {
            if (disposing)
            {

                if (threadCallMainRTL_433 != null)
                {
                    if (threadCallMainRTL_433.IsAlive)
                        threadCallMainRTL_433.Join(1000);
                    threadCallMainRTL_433 = null;
                }
                if (panelRtl_433 != null)
                    panelRtl_433 = null;
                if (myClassTraceGraphe != null)
                    myClassTraceGraphe = null;
                memoDataIQForRs433 = null;
                GC.SuppressFinalize(this);
#if DEBUG
                stopw = null;
                stopwRtl433 = null;
#endif
            }
        }
#endregion
    }
}
