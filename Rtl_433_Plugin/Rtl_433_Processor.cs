/* Written by Marc Prieur (marco40_github@sfr.fr)
                               Rtl_433_Processor.cs 
                           project Rtl_433_Plugin
                                Plugin for SdrSharp
**************************************************************************************
Creative Commons Attrib Share-Alike License
You are free to use/extend this library but please abide with the CC-BY-SA license:
Attribution-NonCommercial-ShareAlike 4.0 International License
http://creativecommons.org/licenses/by-nc-sa/4.0/

All text above must be included in any redistribution.
 ************************************************************************************/

#define noTESTDECIMATORSDRSharp   //no ok and only decim pair

using SDRSharp.Common;
using SDRSharp.Radio;
using SDRSharp.FrontEnds.Airspy;
using SDRSharp.FrontEnds.AirspyHF;
using SDRSharp.FrontEnds.SpyServer;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using ThreadState = System.Threading.ThreadState;

namespace SDRSharp.Rtl_433
{
    public unsafe class Rtl_433_Processor :IIQProcessor,  IStreamProcessor, IBaseProcessor , IDisposable
    {
        #region declaration
        internal const Int32 NBBYTEFORRTL_433 =  131072*2;    //*2->idem RTL_433      for lower time cycle (16 * 32 * 512) 262144 idem rtl433   I and Q  
        internal static Int32 nbByteForRtl433 = NBBYTEFORRTL_433;
        internal static Int32 nbByteForRtl433AfterDecime = nbByteForRtl433;
        internal static Int32 nbComplexForRtl433 = NBBYTEFORRTL_433 / sizeof(float);
        internal static Int32 nbComplexForRtl433AfterDecime = nbComplexForRtl433;
        internal static Int32 decimation = 1;
        private UnsafeBuffer IQBuffer;
        private static Complex* IQPtr;
        private Thread processThreadRtl433;
        private ClassInterfaceWithRtl433 ClassInterfaceWithRtl433;
        private readonly ComplexFifoStream floatStreamComplex = new ComplexFifoStream(BlockMode.BlockingRead);
        private ISharpControl control;
        private Rtl_433_Panel panelRtl_433;
        private Boolean sourceIsFile = false;
        private Boolean terminated = true;
        private Boolean _Enabled = false;
        private Double sampleRate = 0.0;
        private Int64 frequencyRtl433 = 0;
        private Int64 frequency = 0;
        private Int64 centerFrequency = 0;
#if TESTDECIMATORSDRSharp
        private ComplexDecimator _decimator = null;
#endif

#endregion

#region class
        internal Rtl_433_Processor(ISharpControl control, Rtl_433_Panel panelRtl_433, ClassInterfaceWithRtl433 classInterfaceWithRtl433)
        {
            this.control = control;
            this.panelRtl_433 = panelRtl_433;
            this.ClassInterfaceWithRtl433 = classInterfaceWithRtl433;
            this.control.PropertyChanged += NotifyPropertyChangedHandler;
            this.control.RegisterStreamHook(this, ProcessorType.RawIQ);  //-->process
        }
         internal Int64 FrequencyRtl433
        {
            get
            {
                return frequencyRtl433;
            }
            set
            {
                frequencyRtl433 = value;
                SetFrequency();
            }
        }
#endregion

#region internal functions

        /// <summary>
        /// Without this thread neither floatStreamComplex decode is ok but no acces windows.
        /// setFrequency
        /// setSourceName
        /// floatStreamComplex.Open();
        /// start thread processor
        /// </summary>
        internal void Start()
        {
//#if DEBUG
//            NbComplex = 0;
//#endif
            floatStreamComplex.Open();               //->run process - also if stop - stop if disabled plugin
            SetSourceName();
            terminated = false;
            if (processThreadRtl433 == null)
            {
                processThreadRtl433 = new Thread(ThreadToRtl433)
                {
                    Name = "Thread_Process_Rtl433",
                    Priority = ThreadPriority.Normal
                };
            }
            //if (processThreadRtl433.ThreadState == ThreadState.Unstarted)
                processThreadRtl433.Start();
        }
        /// <summary>
        /// if (frequencyRtl433 > 0)
        /// set control.TuningStyle and control.SetFrequency
        /// </summary>
        private Int32 testSize = 0;
        internal void InitBuffer(Int32 ComplexSize, Boolean clean)
        {
            if (IQBuffer != null)
                ((IDisposable)IQBuffer).Dispose();
            IQPtr = null;
            if (!clean)
            {
                IQBuffer = UnsafeBuffer.Create(ComplexSize, sizeof(Complex));
                IQPtr = (Complex*)IQBuffer;
                testSize = ComplexSize;
                //Trace.WriteLine("testSize  " + testSize);
            }
        }

        /// <summary>
        /// set typeSourceFile=true if control.SourceName Contains(".WAV")
        /// set SourceName to ClassInterfaceWithRtl433
        /// </summary>
        internal void SetSourceName()
        {
            string file = "\n";
            sourceIsFile = false;
            if(control.Source==null)  //old version
            {
                sourceIsFile = control.SourceIsWaveFile;
                file = control.SourceName + "\n";
            }
            else if (control.Source.ToString().Contains("WAVEFileIO") && control.Source is IFrontendController)
            {
                 sourceIsFile = true;
                //**************************
                //////IFrontendController c = null;
                //////if (control.Source != null)
                //////{
                //////    IFrontendController frontendController = (IFrontendController)control.Source;
                //////    c = frontendController;
                //////}
                //**************************

                //I didn't manage to implement for recents versions 1921(not have SDRSharp.FrontEnds)
                //espion control.Source give FileName and FileLength
                //Source	{SDRSharp.FrontEnds.FilePlayer.WAVEFileIO}	object {SDRSharp.FrontEnds.FilePlayer.WAVEFileIO}
            }
            //else-->USB
            ClassInterfaceWithRtl433.SetSourceName(file, sourceIsFile);
            nbByteForRtl433 = NBBYTEFORRTL_433;
            nbComplexForRtl433 = nbByteForRtl433 / 2;
            nbByteForRtl433AfterDecime = nbByteForRtl433;
            nbComplexForRtl433AfterDecime = nbComplexForRtl433;
            decimation = (Int32)(control.RFBandwidth / 250000);  //-------->pb with knx_rf_g001_1024k_868320000hz_27_12_2024_10_54_50.wav ok if decimation=1
            decimation = 1;    //no comment for supress decimation      no perfect with decimate???
            nbByteForRtl433 *= (Int32)decimation;
            Thread.BeginCriticalRegion();
            nbComplexForRtl433 *= (Int32)decimation;
            InitBuffer(nbComplexForRtl433, false);
            Thread.EndCriticalRegion();
        }
        /// <summary>
        /// terminated = true for stop thread processor
        /// </summary>
        internal void Stop()
        {
            terminated = true;
            floatStreamComplex.Flush();
            floatStreamComplex.Close();
        }

#endregion

#region private function
        private void SetFrequency()
        {
            if (frequencyRtl433 > 0)
            {
                control.TuningStyle = TuningStyle.Center;
                control.ResetFrequency(frequencyRtl433);
            }
        }
        private Int32 total = 0;
        private void ThreadToRtl433()  //startup thread
        {
#if !(DEBUG && TESTSTARTWITHOUTRADIO)
            while (control.IsPlaying && !terminated && floatStreamComplex != null)  //normal case
#else
            while (!terminated && floatStreamComplex != null)  //normal case
#endif
            {
                //dotnet9 x64 source=iqFile player stop soft SDRSharp  on stop radio or change file with or without sleep
                //no debug info if attach SDRSharp
                //if (sourceIsFile)
                //{
                //    try
                //    {
                //        Thread.Sleep(1000);
                //    }
                //    catch (ThreadInterruptedException)
                //    {
                //        Debug.WriteLine("Thread sleep interrupted");
                //    }
                //}
                Int32 nbComplexToAdd = 0;
                while (control.IsPlaying && total < nbComplexForRtl433 && !terminated)
                {
                    nbComplexToAdd = (Int32)(nbComplexForRtl433) - total;
                    total += floatStreamComplex.Read(IQPtr, total, nbComplexToAdd);
                    if (nbComplexForRtl433 != testSize)
                        Trace.WriteLine(nbComplexForRtl433.ToString()+"  " +testSize.ToString());
                    if (total > testSize)
                        Trace.WriteLine(total.ToString());   //a voir en test boucle, voir si nbComplexForRtl433 change avant testSize
                }
                Thread.BeginCriticalRegion();
#if !(DEBUG && TESTSTARTWITHOUTRADIO)                       //for start without radio
                if (!control.IsPlaying || terminated  || floatStreamComplex == null)
                    break;
#else
                if (terminated)
                    break;
#endif

#if TESTDECIMATORSDRSharp
                Int32 lenOut = 0;
                ////****************************************ComplexDecimator not OK perhaps low pass filter ? **********************************
                /// warning only pair decimation-->sample rate 1024k for 250k->4
                if (_decimator == null)
                    _decimator = new ComplexDecimator(decimation);
                lenOut = _decimator.Process(IQPtr, total);
                ClassInterfaceWithRtl433.send_data(IQPtr, lenOut); //len->total
                ////*********************************************************************************
#elif DECIM    //if DECIM add classDecimation.c sometime problem with 868Mhz and sample rate 1000k ex:bresser...
                Int32 lenOut = 0;
                Complex* dataIQdecimatePtr = ClassDecimation.DecimateMax(IQPtr, decimation, nbComplexForRtl433, ref lenOut);  //len->total

//#if DEBUG
               
//              for (Int32 j = 0; j < nbComplexForRtl433; j++)  //for calc if 1--0.99 if -1-->-0.99 ???
//                    if(IQPtr[j].Imag>0.9)
//                    {
//                       for (Int32 i=0; i < 1000; i++)
//                            Debug.WriteLine($"{IQPtr[i + j].Imag} \t {IQPtr[i + j].Real}" );
//                       for (Int32 i = 0; i < 250; i++)
//                        { 
//                            Debug.WriteLine($"{dataIQdecimatePtr[i + j].Imag} \t {dataIQdecimatePtr[i + j].Real}" );
//                            Debug.WriteLine("");
//                            Debug.WriteLine("");
//                            Debug.WriteLine("");
//                        }
//                        break;
//                    }
//

                //Trace.WriteLine("ok    " + lenOut.ToString() + "   " + total.ToString());
                ClassInterfaceWithRtl433.Send_data(dataIQdecimatePtr, lenOut); //len->total
#else
                ClassInterfaceWithRtl433.Send_data(IQPtr, nbComplexForRtl433); //len->total
#endif

                Thread.EndCriticalRegion();
                total = 0;
            }   //while
            floatStreamComplex.Flush();
            processThreadRtl433 = null;
#if TESTDECIMATORSDRSharp
            _decimator = null;
#endif
        }
        //private Int32 GetFreq(String fileName)
        //{
        //    Match match = Regex.Match(fileName, "([0-9,.]+)kHz", RegexOptions.IgnoreCase);
        //    if (match.Success)
        //    {
        //        return Int32.Parse(match.Groups[1].Value) * 1000;
        //    }
        //    else
        //    {
        //        match = Regex.Match(fileName, "([\\-0-9]+)Hz", RegexOptions.IgnoreCase);
        //        return ((!match.Success) ? 0 : Int32.Parse(match.Groups[1].Value));
        //    }
        //}
        //private Int32 getSizeDataComplex(String fileName)
        //{
        //    Match match = Regex.Match(fileName, "([0-9,.]+)b_", RegexOptions.IgnoreCase);
        //    if (match.Success)
        //        return Int32.Parse(match.Groups[1].Value);
        //    else
        //        return NBBYTEFORRTL_433;
        //}
#endregion

#region interfaces
        //private Int32 sleep = 0;
        public double SampleRate         //IStreamProcessor
        {
            get { return sampleRate; }
            set
            {
                if (sampleRate != value)
                {
                    sampleRate = value;
                    if (ClassInterfaceWithRtl433 != null)
                        ClassInterfaceWithRtl433.SampleRateDbl = sampleRate;
                    //sleep = 100;  //also delays with stuffing in the replay file
                }
            }
        }
        /// <summary>
        /// read by sdrsharp run data in process if true
        /// </summary>
        public Boolean Enabled
        {
            get
            {
                return _Enabled;
            }
            set
            {
                _Enabled = value;
            }
        }  //IBaseProcessor
           /// <summary>
           /// WARNING:if modifie sleep for .wav,verify:
           /// -version 1632 and version > 1632
           /// -memory evolution
           /// -redim window graph
           /// -all with install\Recordings\g001_433.92M_250k_STEREO.wav and install\Recordings\g092_868M_2048k_STEREO.wav
           /// </summary>
           /// <param name="buffer"></param>
           /// <param name="length"></param>
           /// le sleep dans process permet de ralentir le producteur, regle le probleme  en replay avec Protocol_153 Model_ Cotech-367959_433920khz_250k_33524b_13_09_2024 11 52 16 STEREO.wav(33524 complex) limité au minimum de data
           /// mais plantage sdrsharp si changement de fichier sans arreter radio.
           /// essai d'ajouter des informations 00 (WriteBufferToWav)pour ralentir les données utiles, si ok voir le sleep au niveau consommateur--->ok a 131072 complex sans sleep(Protocol_153 Model_ Cotech-367959_433920khz_250k_131072b_16_09_2024 16 41 47 STEREO.wav).
           /// Atlas_433932hz_250k_131071b_STEREO.wav ok sans sleep producteur(process) et consommateur(ThreadToRtl433)(131071 complex) 

        //SDRSharp 1632 en baseband file player avec un fichier de 1000 data de 16bits-->500 short-->250 complex, les data se repetent sans bourrage length always =65536
        //wav stereo:la voie gauche=real et la voie droite=imag

        //[method: CLSCompliant(false)]

//#if DEBUG
//        public Int32 NbComplex = 0;
//#endif
        public void Process(Complex* buffer, Int32 length)    //IIQProcessor
        {
//#if DEBUG
            //Debug.WriteLine(length);
            ////if (NbComplex<150000 && buffer[0].Real!=0)
            ////{ 
            ////String message = "";
            //////panelRtl_433.SetMessage(($"buffer[{900}].Real={buffer[900].Real}  buffer[{900}].Imag={buffer[900].Imag}\n"));
            ////for (int i = 0; i < length; i++)
            //// message += ($"buffer[{NbComplex + i}].Real={buffer[i].Real}  buffer[{NbComplex + i}].Imag={buffer[i].Imag}\n");
            ////Debug.WriteLine(message);
            ////NbComplex += length;
            ////}
            //message = "";
            //for (int i = 32768; i < 32768+1000; i++)
            //    message += ($"buffer[{i}].Real={buffer[i].Real}  buffer[{i}].Imag={buffer[i].Imag}\n");

            //Debug.WriteLine(message);
            //MessageBox.Show(message);
//#endif
            ////#if DEBUG
            ////           for (int dd=0;dd<length;dd++)
            ////if ((Math.Abs(buffer[dd].Real + 0.716535449f)<0.0000000001f) && (Math.Abs(buffer[dd].Imag + 0.417322844)<0.000000001f))
            ////               SYNCHRO = SYNCHRO;

            //if (SYNCHRO == 0f)
            //    SYNCHRO = buffer[0].Real;
            //if (buffer[0].Real == SYNCHRO)
            //    SYNCHRO = SYNCHRO;
            ////#endif
            if (control.IsPlaying && !terminated && floatStreamComplex != null)
                //if(!sourceIsFile)
                 floatStreamComplex.Write(buffer, length);
                //else
                //{
             //else
            //{
            //    Int32 newLen = 0;
            //    //Complex* newBuffer[length];
            //    Complex* newBufferPtr;

            //    UnsafeBuffer newBuffer = UnsafeBuffer.Create(length, sizeof(Complex));
            //    newBufferPtr = (Complex*)IQBuffer;

            //    for (int i = 0; i < length; i++)
            //        if (!(buffer[i].Real==0 && buffer[i].Imag==0))
            //        {
            //            newBufferPtr[newLen] = new Complex(buffer[i].Real, buffer[i].Imag);
            //            newLen++;
            //        }
            //    if (newLen != length)
            //        newLen = newLen;
            // floatStreamComplex.Write(newBufferPtr, newLen);
            //}
        }

        private void NotifyPropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            if (sender is MainForm && panelRtl_433.enabledPlugin)
            {
                switch (e.PropertyName)
                {
                    case "StartRadio":

                        if (panelRtl_433 != null)
#if !TESTBOUCLEREPLAYMARC
                            panelRtl_433.Start(true);
#else
                         panelRtl_433._buttonStartStop(true);
#endif
                        break;
                    case "StopRadio":
                        if (panelRtl_433 != null)
                            panelRtl_433.Stop(true);      //false
                        break;
                    case "CenterFrequency":
                        centerFrequency = (sender as MainForm).CenterFrequency;
                        if (ClassInterfaceWithRtl433 != null)
                            ClassInterfaceWithRtl433.SetCenterFrequency(centerFrequency);
                        break;
                    case "Frequency":
                        frequency = (sender as MainForm).Frequency;
                        //ClassInterfaceWithRtl433.setSourceName((sender as MainForm).SourceName + "\n", sourceIsFile);
                        if (ClassInterfaceWithRtl433 != null)
                            ClassInterfaceWithRtl433.SetFrequency(frequency);
                        break;
                    case "SourceName":
                        string _SourceName = (sender as MainForm).SourceName;
#if TESTBOUCLEREPLAYMARC

                        Trace.WriteLine(_SourceName);

                        ClassInterfaceWithRtl433.setSourceName(_SourceName, true);
#endif
                        break;
                    //    //Console.();(_SourceName);
                    //    typeSourceFile = false;
                    //    object t = control.Source;
                    //    if (control.SourceIsWaveFile)
                    //    {
                    //        typeSourceFile = true;
                    //    }
                    //    break;
                    //        ClassInterfaceWithRtl433.setTypeInputFile(true);
                    //        ClassInterfaceWithRtl433.setSourceName(control.SourceName);
                    //    }
                    //    else
                    //        ClassInterfaceWithRtl433.setTypeInputFile(false);

                    //    break;
                    // case "TuningStyle":
                    //     _SourceName = (sender as MainForm).SourceName;
                    //     break;
                    // case "Zoom":
                    //     _SourceName = (sender as MainForm).SourceName;
                    //     break;
                    //case "IFOffset":
                    //     _SourceName = (sender as MainForm).SourceName;
                    //     break;
                    //case "TunableBandwidth":
                    //    Int32 test = (sender as MainForm).TunableBandwidth;
                    //     break;
                    // case "SAttack":
                    //     float test1 = (sender as MainForm).SAttack;
                    //     break;
                    // case "SDecay":
                    //     float test2 = (sender as MainForm).SDecay;
                    //     break;
                    // case "WAttack":
                    //     float test3 = (sender as MainForm).WAttack;
                    //     break;
                    // case "WDecay":
                    //     float test4 = (sender as MainForm).WDecay;
                    //     break;
                    default:
                        break;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
        private void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                if (processThreadRtl433 != null)
                {
                    processThreadRtl433.Join(1000);
                    //WARNING VS with SDRSharp 1632 don't stop:ok with exe
                    processThreadRtl433 = null;
                }
                CleanProcessor(false);
                GC.SuppressFinalize(this);
            }
        }
        private void CleanProcessor(Boolean cleanAll)
        {
            InitBuffer(0, true);
            if (floatStreamComplex != null)
            {
                floatStreamComplex.Flush();
                floatStreamComplex.Close();
            }
            Enabled = false;
            if (cleanAll)
            {
                ClassInterfaceWithRtl433 = null;
                control = null;
                panelRtl_433 = null;
            }
        }
#endregion
    }
}


