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
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using SDRSharp.Common;
using SDRSharp.Radio;
namespace SDRSharp.Rtl_433
{
    public unsafe class Rtl_433_Processor : IIQProcessor, IStreamProcessor, IBaseProcessor
    {
        [DllImport("kernel32.dll")] [return: MarshalAs(UnmanagedType.Bool)] static extern bool AllocConsole();
        public const int NBBYTEFORRTS_433 = 50000;  //  50000 ;  // ; for lower time cycle (16 * 32 * 512) 262144 idem rtl433   I and Q  
        public const int NBCOMPLEXFORRTS_433 = NBBYTEFORRTS_433 / 2;  //   /2 for real+imag
        public const int NBBUFFERFORRTS_433 = 5;  //5 buffer for shorter cycle rtl433(only 1 buffer) but 5 buffers for record one shoot
        //5 buffer for sample rate to 250000
        private UnsafeBuffer _IQBuffer;
        private Complex* _IQPtr;
        private Thread _processThreadRtl433;
        private bool _terminated = true;
        private ClassInterfaceWithRtl433 _ClassInterfaceWithRtl433;
        private readonly ComplexFifoStream _floatStreamComplex = new ComplexFifoStream(BlockMode.BlockingRead);
        private  ISharpControl _control;
        #region class
        public Rtl_433_Processor(ISharpControl control)
        {
            _control = control;
        }
        public void SetClassInterfaceWithRtl433(ClassInterfaceWithRtl433 classInterfaceWithRtl433)
        {
            _control.PropertyChanged += NotifyPropertyChangedHandler;
            _ClassInterfaceWithRtl433 = classInterfaceWithRtl433;
            _IQBuffer = UnsafeBuffer.Create(NBCOMPLEXFORRTS_433, sizeof(Complex));
            _IQPtr = (Complex*)_IQBuffer;
            _control.RegisterStreamHook(this, ProcessorType.RawIQ);     //it's ok with DemodulatorOutput but sample rate limited to 37500
            setSourceName();
            //_control.AgcHang = true;  not the good function it's AGC panel and not tuner parameters
            //_control.UseAgc = true;  not the good function it's AGC panel and not tuner parameters
            //_AmDetector = new AmDetector();
            //Console.WriteLine(proc.PrivateMemorySize64);
        }
        public void openConsole()
        {
            bool ret = AllocConsole();
            try
            {
                 //Console.BufferHeight = 5000;  //error if in visual studio 
                Console.Title = "Verbose messages  from RTL_433";
                //in this case console is exec window
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "  Rtl_433_Processor->openConsole", "Error openConsole", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool _enableRtl433;
        public bool EnableRtl433
        {
            get { return _enableRtl433; }
            set
            {
                _enableRtl433 = value;
                if (value && _control.IsPlaying) 
                {
                     Start();
                     setSourceName();
                }
                else
                {
                    Stop();
                }
            }
        }
        private long _frequencyRtl433;
        public long FrequencyRtl433
        {
            get
            {
                return _frequencyRtl433;
            }
            set
            {
                _frequencyRtl433 = value;
               // setFrequency();
            }
        }
        #endregion
        #region processThreadRtl433
        private void Start()
        {
            _terminated = false;
            if (_processThreadRtl433 == null)
            {
                setFrequency();
                _floatStreamComplex.Open();
                _processThreadRtl433 = new Thread(ProcessRtl433);
                _processThreadRtl433.Name = "RTL_433";
                _processThreadRtl433.Start();
            }

            //_SourceName = "";
        }
        private void setFrequency()
        {
            if (_frequencyRtl433 > 0)
            {
                _control.TuningStyle = TuningStyle.Center;
                _control.SetFrequency(_frequencyRtl433, true);
            }
        }
        private void setSourceName()
        {
            if (!_control.SourceName.Contains("PublicKeyToken=null"))
                _ClassInterfaceWithRtl433.setSourceName(_control.SourceName);
        }
        public void Stop()
        {
            _terminated = true;
            if (_processThreadRtl433 != null)
            {
                _floatStreamComplex.Close();
                _processThreadRtl433.Join();
                _processThreadRtl433 = null;
             }
        }
        private void ProcessRtl433(object parameter)
        {
             while (_control.IsPlaying && !_terminated)
            {
                int total = 0;
                while (_control.IsPlaying && total < NBCOMPLEXFORRTS_433 && !_terminated)
                {
                        var len = NBCOMPLEXFORRTS_433 - total;
                        total += _floatStreamComplex.Read(_IQPtr, total, len);
                }
                if (_terminated)
                    break;
                //if (_control.SourceName != "")
                //    Thread.Sleep(1000);
                _ClassInterfaceWithRtl433.send_data(_IQPtr);
            }   //while
            _floatStreamComplex.Flush();
        }  //ProcessRtl433
        #endregion
        #region interfaces
        private double _sampleRate;
        public double SampleRate         //IStreamProcessor
        {
            get {return _sampleRate;}
            set
            {
                if (_sampleRate != value)
                {
                    _sampleRate = value;
                    _ClassInterfaceWithRtl433.SampleRateDbl = _sampleRate;
                }
            }
        }
        public bool Enabled { get; set; }  //IBaseProcessor
        public void Process(Complex* buffer, int length)    //IIQProcessor
        {
            if (_control.IsPlaying && !_terminated)
            {
                 _floatStreamComplex.Write(buffer, length);
            }
            else
                return;
        }
        private long _frequency=0;
        private long _centerFrequency=0;
        //private string _SourceName = "";
        private void NotifyPropertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            if (sender is MainForm)
            {
                switch (e.PropertyName)
                {
                    case "StartRadio":
                        _ClassInterfaceWithRtl433.Start(true);
                        break;
                    case "StopRadio":
                         _ClassInterfaceWithRtl433.Stop(true);
                        break;
                    case "CenterFrequency":
                        _centerFrequency = (sender as MainForm).CenterFrequency;
                        _ClassInterfaceWithRtl433.setCenterFrequency(_centerFrequency);
                        break;
                    case "Frequency":
                        _frequency = (sender as MainForm).Frequency;
                        _ClassInterfaceWithRtl433.setfrequency( _frequency) ;
                        break;
                    //case "SourceName":
                    //    _SourceName = (sender as MainForm).SourceName;
                    //    Console.WriteLine(_SourceName);
                    //    break;
                    //object t = _control.Source;
                    // if (_control.SourceIsWaveFile)
                    // long sizeFile = wavRecorder.getSizeDataFile();

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
                    //    int test = (sender as MainForm).TunableBandwidth;
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
        #endregion
    }
}


