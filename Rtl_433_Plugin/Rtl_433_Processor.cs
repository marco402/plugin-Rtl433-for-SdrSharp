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
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SDRSharp.Common;
using SDRSharp.Radio;
namespace SDRSharp.Rtl_433
{
    public unsafe class Rtl_433_Processor : IIQProcessor, IStreamProcessor, IBaseProcessor, IDisposable
    {
        internal const Int32 NBBYTEFORRTS_433 = 50000;  // ; for lower time cycle (16 * 32 * 512) 262144 idem rtl433   I and Q  
        internal const Int32 NBCOMPLEXFORRTS_433 = NBBYTEFORRTS_433 / 2;  //   /2 for real+imag
        internal const Int32 NBBUFFERFORRTS_433 = 5;  //5 buffer for shorter cycle rtl433(only 1 buffer) but 5 buffers for record one shoot
        //5 buffer for sample rate to 250000

        private UnsafeBuffer IQBuffer;
        private Complex* IQPtr;
        private Thread processThreadRtl433;
        private ClassInterfaceWithRtl433 ClassInterfaceWithRtl433;
        private readonly ComplexFifoStream floatStreamComplex = new ComplexFifoStream(BlockMode.BlockingRead);
        private ISharpControl control;
        private Rtl_433_Panel panelRtl_433;

        private Boolean consoleIsAlive = false;
        private Boolean sourceIsFile = false;
        private Boolean terminated = true;
        private Boolean _Enabled = false;
        private Double sampleRate= 0.0;
        private Int64 frequencyRtl433 = 0;
        private Int64 frequency = 0;
        private Int64 centerFrequency = 0;
        private Int32 productVersion = 0;
        #region class
        internal Rtl_433_Processor(ISharpControl control, Rtl_433_Panel panelRtl_433, ClassInterfaceWithRtl433 classInterfaceWithRtl433)
        {
            this.control = control;
            this.panelRtl_433 = panelRtl_433;
            this.ClassInterfaceWithRtl433 = classInterfaceWithRtl433;
            this.control.PropertyChanged += NotifyPropertyChangedHandler;
            this.control.RegisterStreamHook(this, ProcessorType.RawIQ);       //useful for samplerate with plugin disabled
            IQBuffer = UnsafeBuffer.Create(NBCOMPLEXFORRTS_433, sizeof(Complex));
            IQPtr = (Complex*)IQBuffer;
        }


        IntPtr stdHandle ;
        Microsoft.Win32.SafeHandles.SafeFileHandle safeFileHandle ;
        FileStream fileStream ;
        StreamWriter standardOutput ;

        [DllImport("kernel32.dll",
            EntryPoint = "GetStdHandle",
            SetLastError = true,
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetStdHandle(int nStdHandle);
        private const int STD_OUTPUT_HANDLE = -11;
        private const int MY_CODE_PAGE = 437;
        private const uint GENERIC_WRITE = 0x40000000;
        private const uint FILE_SHARE_WRITE = 0x2;
        private const uint OPEN_EXISTING = 0x3;
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr CreateFile(string lpFileName, uint
                dwDesiredAccess, uint dwShareMode, uint lpSecurityAttributes, uint
                dwCreationDisposition, uint dwFlagsAndAttributes, uint hTemplateFile);
        internal void openConsole()
        {
            if (consoleIsAlive == false)
            {
                Boolean ret = NativeMethods.AllocConsole();
                //without this seven line ok but nok if close and open console no writeLine here and crash if WriteLine in processor.send_rtl433 or panel 
                //in this case console is exec or output window visual studio

                stdHandle = CreateFile("CONOUT$", GENERIC_WRITE, FILE_SHARE_WRITE, 0,OPEN_EXISTING, 0, 0);
                safeFileHandle = new Microsoft.Win32.SafeHandles.SafeFileHandle(stdHandle, true);
                fileStream = new FileStream(safeFileHandle, System.IO.FileAccess.Write);
                //Encoding encoding = Encoding.GetEncoding(MY_CODE_PAGE);
                standardOutput = new StreamWriter(fileStream);  //, encoding
                standardOutput.AutoFlush = true;
                Console.SetOut(standardOutput);
                //Console.WriteLine("This text you can see in console window.");
                try
                {
                    //Console.BufferHeight = 5000;  //error if in visual studio 
                    Console.Title = "Verbose messages  from RTL_433";
                    consoleIsAlive = true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + "  Rtl_433_Processor->openConsole", "Error openConsole", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        internal void freeConsole()
        {
            NativeMethods.FreeConsole();  //possible error if visual studio
            consoleIsAlive = false;
            standardOutput.Close();
            standardOutput.Dispose();
            fileStream.Close();
            fileStream.Dispose();
            safeFileHandle.Close();
            safeFileHandle.Dispose();
            stdHandle=IntPtr.Zero;
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
                setFrequency();
            }
        }
        #endregion

        #region processThreadRtl433
        /// <summary>
        /// Without this thread neither floatStreamComplex decode is ok but no acces windows.
        /// setFrequency
        /// setSourceName
        /// floatStreamComplex.Open();
        /// start thread processor
        /// </summary>
        internal void Start()
        {
            floatStreamComplex.Open();
            setSourceName();
            terminated = false;
            if (processThreadRtl433 == null)
            {
                processThreadRtl433 = new Thread(ProcessRtl433);
                processThreadRtl433.Name = "Thread_Process_Rtl433";
                processThreadRtl433.Priority = ThreadPriority.Normal;
                processThreadRtl433.Start();
            }
        }

        /// <summary>
        /// if (frequencyRtl433 > 0)
        /// set control.TuningStyle and control.SetFrequency
        /// </summary>
        private void setFrequency()
        {
            if (frequencyRtl433 > 0)
            {
                control.TuningStyle = TuningStyle.Center;
                control.SetFrequency(frequencyRtl433, true);
            }
        }
        /// <summary>
        /// set typeSourceFile=true if control.SourceName Contains(".WAV")
        /// set SourceName to ClassInterfaceWithRtl433
        /// </summary>
        private void setSourceName()
        {
            sourceIsFile = false;
            if (control.SourceName.ToUpper().Contains(".WAV"))
            {
                sourceIsFile = true;
                ClassInterfaceWithRtl433.setSourceName(control.SourceName,sourceIsFile);
            }
        }

        /// <summary>
        /// terminated = true for stop thread processor
        /// </summary>
        internal void Stop(Boolean disabledPlugin)
        {
            terminated = true;    // disabledPlugin;
            floatStreamComplex.Flush();
            floatStreamComplex.Close();
        }

        private void ProcessRtl433()  //startup thread
        {
            while (control.IsPlaying && !terminated)
            {
                Int32 total = 0;
                while (control.IsPlaying && total < NBCOMPLEXFORRTS_433 && !terminated)
                {
                    Int32 len = NBCOMPLEXFORRTS_433 - total;
                    total += floatStreamComplex.Read( IQPtr, total, len);
                }
                if (!control.IsPlaying || terminated)
                    break;
                ClassInterfaceWithRtl433.send_data(IQPtr);
                sendData = true;
            }   //while
            processThreadRtl433 = null;
        }
        #endregion
        #region interfaces
        private Int32 sleep = 0;
        public double SampleRate         //IStreamProcessor
        {
            get { return sampleRate; }
            set
            {
                if (sampleRate != value)
                {
                    sampleRate = value;
                    if (ClassInterfaceWithRtl433!=null)
                        ClassInterfaceWithRtl433.SampleRateDbl = sampleRate;
                    sleep = (int)(sampleRate / 100.0);  //500 for >1632
                }
            }
        }

        /// <summary>
        /// read by sdrsharp run data in process if true
        /// </summary>
        public Boolean Enabled {
            get {
                return _Enabled;
            }
            set {
                _Enabled = value;
            }
        }  //IBaseProcessor
        private Boolean sendData = false;   //source=files .wav not useful for version SDRSharp 1632 useful for 1854
        /// <summary>
        /// WARNING:if modifie sleep for .wav,verify:
        /// -version 1632 and version > 1632
        /// -memory evolution
        /// -redim window graph
        /// -all with install\Recordings\g001_433.92M_250k_STEREO.wav and install\Recordings\g092_868M_2048k_STEREO.wav
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="length"></param>
        public void Process(Complex* buffer, Int32 length)    //IIQProcessor
        {
            if (control.IsPlaying && !terminated)
            {
                floatStreamComplex.Write(buffer, length);
                //pb redim graph if source File is .wav with version  1632

                if (sendData) //if function to classInterfaceWithRtl433 when receive device nok
                {
                    if (sourceIsFile)
                        Thread.Sleep(sleep); //with mode Source=.wav no all files: file->pb memory file (g092_868M_2048k_STEREO.wav)
                    //else
                    //    Thread.Sleep(50);  //lost data...
                    sendData = false;
                }
            }
            //else
            //    return;
        }

        private void NotifyPropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            if (sender is MainForm)
            {
                switch (e.PropertyName)
                {
                    case "StartRadio":
                        if(panelRtl_433 != null)
                         panelRtl_433.Start(true);
                         break;
                    case "StopRadio":
                        if (panelRtl_433 != null)
                            panelRtl_433.Stop(false,true);
                        break;
                    case "CenterFrequency":
                        centerFrequency = (sender as MainForm).CenterFrequency;
                        if (ClassInterfaceWithRtl433 != null)
                            ClassInterfaceWithRtl433.setCenterFrequency(centerFrequency);
                        break;
                    case "Frequency":
                        frequency = (sender as MainForm).Frequency;
                        if(ClassInterfaceWithRtl433!=null)
                            ClassInterfaceWithRtl433.setFrequency(frequency);
                         break;
                    //case "SourceName":
                    //    //_SourceName = (sender as MainForm).SourceName;
                    //    //Console.WriteLine(_SourceName);
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
                //MainForm parent = (MainForm)sender;
                //String productVersionStr = parent.ProductVersion.Replace(".", "");
                //if(! Int32.TryParse(productVersionStr, out productVersion))
                //{
                //    MessageBox.Show("  Problem reading version,force to 1.0.0.1854", "Error reading version", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    productVersion = 1001854;
                //}
                //internal Int64 getSampleRate()
                //{
                //    return control.AudioSampleRate();
                //}
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
                cleanProcessor(false);
                GC.SuppressFinalize(this);
            }
        }

        private void cleanProcessor(Boolean cleanAll)
        {
            IQPtr = null;
            if (IQBuffer != null)
                ((IDisposable)IQBuffer).Dispose();
            floatStreamComplex.Flush();
            floatStreamComplex.Close();
            ((IDisposable)floatStreamComplex).Dispose();
            Enabled = false;
             if(cleanAll)
            {
                ClassInterfaceWithRtl433 = null;
                control = null;
                panelRtl_433 = null; 
            }
        }
        #endregion
    }
}
 

