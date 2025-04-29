using System;
using System.Runtime.InteropServices;
namespace SDRSharp.Rtl_433
{
    internal static class NativeMethods //NativeMethods safeNativeMethods ...
    {
        //private const Int32 MINIMAL_BUF_LENGTH = 512;
        //private const Int32 MAXIMAL_BUF_LENGTH = (256 * 16384);
        private const Int32 DEFAULT_BUF_LENGTH = (16 * 32 * 512); // librtlsdr default
        private const Int32 SIGNAL_GRABBER_BUFFER = (12 * DEFAULT_BUF_LENGTH);
        //1 complex = 2 IQ--->  /2
        internal const Int32 SIZE_BUFFER_MEMO_COMPLEX_IQ = SIGNAL_GRABBER_BUFFER ;

        //private const Int32 OOK_EST_LOW_RATIO = 2000;

        //private const String LibRtl_433 = "rtl_433";
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate void receiveMessagesCallback([In] char[] text, [In] Int32 len);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate void ptrReceiveMessagesCallback([In, MarshalAs(UnmanagedType.LPStr)] String message);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate void ptrReceiveRecordOrder([In] char[] text, [In] Int32 len);
#if ANALYZE

        private const Int32 MAX_HIST_BINS = 16;

        /// Histogram data for single bin
        internal struct hist_bin_t{
            internal UInt32 count;
            internal Int32 sum;
            internal Int32 mean;
            internal Int32 min;
            internal Int32 max;
        }

        /// Histogram data for all bins
        internal struct histogram_t{
            internal UInt32 bins_count;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_HIST_BINS)]
            internal hist_bin_t[] bins;
        } 
 
        [DllImport("rtl_433", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "pulse_analyzerPlugin")]
        internal static extern void pulse_analyzerPlugin([In, Out] ref Pulse_data pulsesData, [In, Out] ref histogram_t hist_pulse_bins, [In, Out] ref histogram_t hist_gap_bins, [In, Out] ref histogram_t hist_periods_bins, [In, Out] ref histogram_t hist_timings_bins);

        //internal static extern void pulse_analyzerPlugin([In, Out] ref Pulse_data pulsesData, [In, Out] ref Int32 hist_pulses_bins_count, [In, Out] ref Int32 hist_gap_bins_count, [In, Out] ref Int32 hist_periods_bins_count);
#endif
        [DllImport("rtl_433", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "baseband_low_pass_filter")]
        internal static extern void baseband_low_pass_filter([In, Out, MarshalAs(UnmanagedType.LPArray)] ushort[] x_buf, [In, Out, MarshalAs(UnmanagedType.LPArray)] short[] y_buf, [Out] Int32 len, [In] ref Filter_state state);
        [DllImport("rtl_433", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "envelope_detect")]
        internal static extern float envelope_detect([In, Out, MarshalAs(UnmanagedType.LPArray)] Byte[] iq_buf, [In, Out, MarshalAs(UnmanagedType.LPArray)] ushort[] y_buf, [Out] Int32 len);
        [DllImport("rtl_433", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "baseband_demod_FM")]
        internal static extern void baseband_demod_FM([In, Out, MarshalAs(UnmanagedType.LPArray)] Byte[] x_buf, [In, Out, MarshalAs(UnmanagedType.LPArray)] short[] y_buf, [Out] UInt64 num_samples, [Out] UInt32 samp_rate, [Out] float low_pass, [In, Out] ref Demodfm_state state);
        [DllImport("rtl_433", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "pulse_detect_package")]
        internal static extern Int32 pulse_detect_package([In, Out] ref Pulse_detect pulsesDetect, [In, Out, MarshalAs(UnmanagedType.LPArray)] Int16[] envelope_data, [In, Out, MarshalAs(UnmanagedType.LPArray)] Int16[] fm_data, Int32 len, UInt32 samp_rate, UInt64 sample_offset, [In, Out] ref Pulse_data pulsesData, [In, Out] ref Pulse_data fskPulses, UInt32 fpdm, [In, Out] ref Int32 start, [In, Out] ref Int32 startFsk);
        [DllImport("rtl_433", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "test_dll_get_version")]
        internal static extern IntPtr IntPtr_Pa_GetVersionText();
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate void ptrFctInit(IntPtr _ptrCbData,
            IntPtr _ptrCtx,
            IntPtr _ptrDemod);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate void ptrReceiveStructDevicesCallback(IntPtr ptrInfosToPlugin);
        //internal delegate void ptrReceiveStructDevicesCallback([In, MarshalAs(UnmanagedType.LPStruct)] R_deviceToPlugin deviceToPlugin); //[In] NativeMethods.r_deviceToPlugin infosToPlugin
        [DllImport("rtl_433", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        internal static extern void rtl_433_call_main([Out] ptrReceiveMessagesCallback fctMessage, [Out] ptrFctInit fctInitCbData, [Out] ptrReceiveStructDevicesCallback fctStructDevices, [Out] UInt32 param_samp_rate, [Out] Int32 param_sample_size, [Out] UInt32 disabled,Boolean sourceIsFile, [Out] Int32 argc, String[] args);
        [DllImport("rtl_433", CallingConvention = CallingConvention.StdCall)]
        internal static extern void receive_buffer_cb([Out] byte[] iq_buf, [Out] UInt32 len, [Out] IntPtr ctx);
        [DllImport("rtl_433", CallingConvention = CallingConvention.StdCall)]
        internal static extern void stop_sdr([Out] IntPtr ctx);
        [DllImport("rtl_433", CallingConvention = CallingConvention.StdCall)]
        internal static extern void setFrequency([Out] UInt32 frequency);
        [DllImport("rtl_433", CallingConvention = CallingConvention.StdCall)]
        internal static extern void setCenterFrequency([Out] UInt32 centerFrequency);
        [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory")]
        internal static extern void CopyMemory(Byte[] Destination, IntPtr Source, UInt32 Length);
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)] // , Pack = 8
        internal struct List  //list.h
        {
            internal IntPtr elems;   // void** elems;
            internal UInt32 size;        //size_t
            internal UInt32 len;        //size_t
        }
        //list_t;
        internal enum Conversion_mode_t : Int32 //rtl_433.h
        {
            CONVERT_NATIVE,
            CONVERT_SI,
            CONVERT_CUSTOMARY
        }
        internal enum Time_mode_t : Int32 //rtl_433.h
        {
            REPORT_TIME_DEFAULT,
            REPORT_TIME_DATE,
            REPORT_TIME_SAMPLES,
            REPORT_TIME_UNIX,
            REPORT_TIME_ISO,
            REPORT_TIME_OFF
        }
        /// state data for pulse_FSK_detect()
        internal enum Fsk_state : Int32  //pulse_FSK_detect_fsk.h
        {
            PD_FSK_STATE_INIT = 0,   //< Initial frequency estimation
            PD_FSK_STATE_FH = 1,     //< High frequency (pulse)
            PD_FSK_STATE_FL = 2,     //< Low frequency (gap)
            PD_FSK_STATE_ERROR = 3   //< Error - stay here until cleared
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)] // , Pack = 8
        internal struct Pulse_detect_fsk_t  //pulse_FSK_detect_fsk.h
        {
            internal UInt32 fsk_pulse_length; ///< Counter for internal FSK pulse detection
            internal Fsk_state fsk_state;
            internal Int32 fm_f1_est; ///< Estimate for the F1 frequency for FSK
            internal Int32 fm_f2_est; ///< Estimate for the F2 frequency for FSK
            internal Int16 var_test_max;
            internal Int16 var_test_min;
            internal Int16 maxx;
            internal Int16 minn;
            internal Int16 midd;
            internal Int32 skip_samples;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)] // , Pack = 8
        internal struct Samp_grab   //samp_grab.h
        {
            internal IntPtr frequency;
            internal IntPtr samp_rate;
            internal IntPtr sample_size;
            internal UInt32 sg_counter;  //file save indice 
            internal IntPtr sg_buf;
            internal UInt32 sg_size;
            internal UInt32 sg_index;
            internal UInt32 sg_len;
        }
        internal enum Ook_state : Int32    //pulse_detect.c
        {
            PD_OOK_STATE_IDLE = 0,
            PD_OOK_STATE_PULSE = 1,
            PD_OOK_STATE_GAP_START = 2,
            PD_OOK_STATE_GAP = 3
        }
        /// Internal state data for pulse_pulse_package()
        internal struct Pulse_detect      //pulse_detect.c
        {
            internal Int32 use_mag_est;          ///< Whether the envelope data is an amplitude or magnitude.
            internal Int32 ook_fixed_high_level; ///< Manual detection level override, 0 = auto.
            internal Int32 ook_min_high_level;   ///< Minimum estimate of high level (-12 dB: 1000 amp, 4000 mag).
            internal Int32 ook_high_low_ratio;   ///< Default ratio between high and low (noise) level (9 dB: x8 amp, 11 dB: x3.6 mag).
            internal Ook_state ook_state;
            internal Int32 pulse_length; ///< Counter for internal pulse detection
            internal Int32 max_pulse;    ///< Size of biggest pulse detected
            internal Int32 data_counter;    ///< Counter for how much of data chunk is processed
            internal Int32 lead_in_counter; ///< Counter for allowing initial noise estimate to settle
            internal Int32 ook_low_estimate;  ///< Estimate for the OOK low level (base noise level) in the envelope data
            internal Int32 ook_high_estimate; ///< Estimate for the OOK high level
            internal Int32 verbosity; ///< Debug output verbosity, 0=None, 1=Levels, 2=Histograms
            internal Pulse_detect_fsk_t FSK_state;
        };
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)] // , Pack = 8
        internal struct Sdr_dev         //sdr.c
        {
            internal UInt32 rtl_tcp;  //SOCKET
            internal UInt32 rtl_tcp_freq; ///< last known center frequency, rtl_tcp only.
            internal UInt32 rtl_tcp_rate; ///< last known sample rate, rtl_tcp only.
            //# ifdef SOAPYSDR
            //            SoapySDRDevice* soapy_dev;
            //            SoapySDRStream* soapy_stream;
            //            double fullScale;
            //#endif

            //# ifdef RTLSDR
            internal IntPtr rtlsdr_dev;  //rtlsdr_dev_t*
            internal IntPtr rtlsdr_cb;   //sdr_event_cb_t
            internal IntPtr rtlsdr_cb_ctx;    //void*
            internal Int32 running;
            internal Int32 polling;
            internal IntPtr buffer;   //void*
            internal UInt32 buffer_size;   //size_t
            internal UInt32 buffer_pos;  ///< sdr data buffer next write position
            internal Int32 sample_size;
            internal Int32 sample_signed;
            //internal Int32 apply_rate;
            internal Int32 apply_freq;
            //internal Int32 apply_corr;
            //internal Int32 apply_gain;
            internal UInt32 sample_rate;
            //internal Int32 freq_correction;
            internal UInt32 center_frequency;
            //internal String gain_str;
        }
        /// Data for a compact representation of generic pulse train.
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)] // , Pack = 8
        internal struct Pulse_data    //pulse_data.h
        {
            internal UInt64 offset;            ///< Offset to first pulse in number of samples from start of stream.
            internal UInt32 sample_rate;       ///< Sample rate the pulses are recorded with.
            internal UInt32 depth_bits;        ///< Sample depth in bits.
            internal UInt32 start_ago;         ///< Start of first pulse in number of samples ago.
            internal UInt32 end_ago;           ///< End of last pulse in number of samples ago.
            internal UInt32 num_pulses;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1200)]
            internal Int32[] pulse;   ///< Width of pulses (high) in number of samples.
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1200)]
            internal Int32[] gap;     ///< Width of gaps between pulses (low) in number of samples.
            internal Int32 ook_low_estimate;       ///< Estimate for the OOK low level (base noise level) at beginning of package.
            internal Int32 ook_high_estimate;      ///< Estimate for the OOK high level at end of package.
            internal Int32 fsk_f1_est;             ///< Estimate for the F1 frequency for FSK.
            internal Int32 fsk_f2_est;             ///< Estimate for the F2 frequency for FSK.
            internal float freq1_hz;
            internal float freq2_hz;
            internal float centerfreq_hz;
            internal float range_db;
            internal float rssi_db;
            internal float snr_db;
            internal float noise_db;
        }
        //pulse_data_t;
        internal const Int32 FILTER_ORDER = 1;  //baseband.h
        /// Filter state buffer.
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)] // , Pack = 8
        internal struct Filter_state  //baseband.h
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = FILTER_ORDER)]
            internal Int16[] y;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = FILTER_ORDER)]
            internal Int16[] x;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = FILTER_ORDER)]
            internal Int16[] memoy;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = FILTER_ORDER)]
            internal Int16[] memox;
        }
        // filter_state_t;
        /// FM_Demod state buffer.
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)] // , Pack = 4
        internal struct Demodfm_state  //baseband.h
        {
            internal Int32 xr;        ///< Last I/Q sample, real part
            internal Int32 xi;        ///< Last I/Q sample, imag part
            internal Int32 xf;        ///< Last Instantaneous frequency
            internal Int32 yf;        ///< Last Instantaneous frequency, low pass filtered
            internal UInt32 rate;     ///< Current sample rate
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            internal Int32[] alp_16; ///< Current low pass filter A coeffs, 16 bit
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            internal Int32[] blp_16; ///< Current low pass filter B coeffs, 16 bit
            internal Int32 bidon;   //-------------------------------------------------------<why?
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            internal Int64[] alp_32; ///< Current low pass filter A coeffs, 32 bit
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            internal Int64[] blp_32; ///< Current low pass filter B coeffs, 32 bit
        }
        //demodfm_state_t;
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)] // , Pack = 8
        internal struct File_info   //fileformat.h
        {
            internal UInt32 format;
            internal UInt32 raw_format;
            internal UInt32 center_frequency;
            internal UInt32 sample_rate;
            internal String spec;
            internal String path;
            internal IntPtr file;        //FILE*
        }
        //file_info_t;
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)] // , Pack = 8
        internal struct Timeval   //
        {
            internal Int32 tv_sec;         /* seconds */
            internal Int32 tv_usec;        /* and microseconds */
        }
        internal const Int32 MAXIMAL_BUF_LENGTH = (256 * 16384);   //rtl_433.h
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)] // , Pack = 8
        internal struct Dm_state    //r_private.h
        {
            internal float auto_level;
            internal float squelch_offset;
            internal float level_limit;
            internal float noise_level;
            internal float min_level_auto;
            internal float min_level;
            internal float min_snr;
            internal float low_pass;
            internal Int32 use_mag_est;
            internal Int32 detect_verbosity;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAXIMAL_BUF_LENGTH)]
            internal Int16[] am_buf;  // AM demodulated signal (for OOK decoding)
            //union {
            //    // These buffers aren't used at the same time, so let's use a union to save some memory
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAXIMAL_BUF_LENGTH)]
            internal Int16[] fm;  // FM demodulated signal (for FSK decoding)
                                  //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAXIMAL_BUF_LENGTH)]
                                  //    internal UInt16[] temp;  // Temporary buffer (to be optimized out..)
                                  //}
                                  //buf;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAXIMAL_BUF_LENGTH)]
            internal Byte[] u8_buf; // format conversion buffer
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAXIMAL_BUF_LENGTH)]
            internal float[] f32_buf; // format conversion buffer
            internal Int32 sample_size; // CU8: 1, CS16: 2--->*2 in rtl_433
            internal IntPtr pulse_detect;        //pulse_detect_t*
            internal Filter_state lowpass_filter_state;
            internal Demodfm_state demod_FM_state;     //demodfm_state
            internal Demodfm_state memo_demod_FM_state;     //demodfm_state
            internal Int32 enable_FM_demod;
            internal UInt32 fsk_pulse_detect_mode;
            internal UInt32 frequency;
            internal IntPtr ptr_samp_grab;        //samp_grab_t*
            internal IntPtr am_analyze;          //am_analyze_t* 
            internal Int32 analyze_pulses;
            internal File_info load_info;
            internal List dumper;
            /* Protocol states */
            internal List r_devs;
            internal Int32 bidon1;         //-----------------------------<why?
            internal Pulse_data pulse_data;       //pulse_data
            internal Pulse_data fsk_pulse_data;   //pulse_data
            internal UInt32 frame_event_count;
            internal UInt32 frame_start_ago;
            internal UInt32 frame_end_ago;
            internal Timeval now;        //struct  int32 and not Int64 -----------------------------<why?
            internal float sample_file_pos;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)] // , Pack = 8
        internal struct R_cfg   //rtl_433.h
        {
            internal IntPtr demod;     //internal struct dm_state *demod;
            //[FieldOffset(0)]    //,MarshalAs(UnmanagedType.LPStr)
           // internal IntPtr dev_query;
           // //[FieldOffset(4)]
           // internal IntPtr dev_info;
           // //[FieldOffset(4 + 4)]
           // internal IntPtr gain_str;
           // //[FieldOffset(8 + 4)]
           // internal IntPtr settings_str;
           // //[FieldOffset(12 + 4)]
           // internal Int32 ppm_error;
           // //[FieldOffset(16 + 4)]
           // internal UInt32 out_block_size;
           // //[FieldOffset(20 + 4)]
           // internal IntPtr test_data;
           // //[FieldOffset(24 + 4)]
           // internal List in_files;
           // //[FieldOffset(28 + 12)]
           // internal IntPtr in_filename;
           // //[FieldOffset(40 + 4)]
           // internal Int32 replay;
           // //[FieldOffset(44 + 4)]
           // internal Int32 hop_now;
           // //[FieldOffset(48 + 4)]
           // internal Int32 exit_async;
           // //[FieldOffset(52 + 4)]
           // internal Int32 exit_code; ///< 0=no err, 1=params or cmd line err, 2=sdr device read error, 3=usb init error, 5=USB error (reset), other=other error
           // //[FieldOffset(56 + 4)]
           // internal Int32 frequencies;
           // //[FieldOffset(60 + 4)]
           // internal Int32 frequency_index;
           // //[FieldOffset(64 + 4)]
           // [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
           // internal UInt32[] frequency;
           // //[FieldOffset(68 + 32 * 4)]
           // internal UInt32 center_frequency;
           // //[FieldOffset(196 + 4)]
           // internal Int32 fsk_pulse_detect_mode;
           // //[FieldOffset(200 + 4)]
           // internal Int32 hop_times;
           // //[FieldOffset(204 + 4)]
           // [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
           // internal Int32[] hop_time;
           // //[FieldOffset(208 + 32 * 4 + 4)]    //-----------------------------------<why +4?--->bidon
           // internal readonly Int32  bidon;
           // internal Int64 hop_start_time;   //time_t
           // //[FieldOffset(340 + 8)]
           // internal Int32 duration;
           // //[FieldOffset(348 + 4)]
           // internal Int64 stop_time;   //time_t
           // //[FieldOffset(352 + 8)]
           // internal Int32 after_successful_events_flag;
           // //[FieldOffset(360 + 4)]
           // internal UInt32 samp_rate;
           // //[FieldOffset(364 + 4)]
           // internal UInt64 input_pos;
           // //[FieldOffset(368 + 8)]
           // internal UInt32 bytes_to_read;
           // //[FieldOffset(376 + 4)]
           // internal IntPtr dev;   //  struct sdr_dev *dev;
           //                        //[FieldOffset(380 + 4)]
           // internal Int32 grab_mode; ///< Signal grabber mode: 0=off, 1=all, 2=unknown, 3=known
           ////[FieldOffset(384 + 4)]
           // internal Int32 raw_mode; ///< Raw pulses printing mode: 0=off, 1=all, 2=unknown, 3=known
           ////[FieldOffset(388 + 4)]
           // internal Int32 verbosity; ///< 0=normal, 1=verbose, 2=verbose decoders, 3=debug decoders, 4=trace decoding.
           ////[FieldOffset(392 + 4)]
           // internal Int32 verbose_bits;
           // //[FieldOffset(396 + 4)]
           // internal Conversion_mode_t conversion_mode;
           // //[FieldOffset(400 + 4)]
           // internal Int32 report_meta;
           // //[FieldOffset(404 + 4)]
           // internal Int32 report_noise;
           // //[FieldOffset(408 + 4)]
           // internal Int32 report_protocol;
           // //[FieldOffset(412 + 4)]
           // internal Time_mode_t report_time;
           // //[FieldOffset(416 + 4)]
           // internal Int32 report_time_hires;
           // //[FieldOffset(420 + 4)]
           // internal Int32 report_time_tz;
           // //[FieldOffset(424 + 4)]
           // internal Int32 report_time_utc;  //commencer la
           //                                  //[FieldOffset(428 + 4)]
           // internal Int32 report_description;
           // //[FieldOffset(432 + 4)]
           // internal Int32 report_stats;
           // //[FieldOffset(436 + 4)]
           // internal Int32 stats_interval;
           // //[FieldOffset(440 + 4)]
           // internal Int32 stats_now;
           // //[FieldOffset(444 + 4)]       //-----------------------------------<why +4?
           // internal Int64 stats_time;     //time_t
           //                                //[FieldOffset(448 + 8)]
           // internal Int32 no_default_devices;
           // //[FieldOffset(456 + 4)]
           // internal IntPtr devices;       //internal struct r_device *devices;
           //                                //[FieldOffset(460 + 4)]
           // internal UInt16 num_r_devices;
           // //[FieldOffset(464 + 4)]        //not 2 ?
           // internal List data_tags;
           // //[FieldOffset(468 + 12)]
           // internal List output_handler;
           // //[FieldOffset(480 + 12)]
           // internal List raw_handler;         //ajout 01/2023
           //                                    //[FieldOffset(492 + 12)]
           // internal Int32 has_logout;         //ajout 01/2023
           //                                    //[FieldOffset(504 + 4)]
           //// internal IntPtr demod;     //internal struct dm_state *demod;
           //                            //[FieldOffset(508 + 4)]
           // internal IntPtr sr_filename;
           // //[FieldOffset(512 + 4)]
           // internal Int32 sr_execopen;
           // //[FieldOffset(516 + 4)]
           // //internal Int32 old_model_keys;
           // //[FieldOffset(508+4)]    //-----------------------------------<why?
           // /* stats*/


           // internal Int64 frames_since;    //time_t
           //                                 //[FieldOffset(520 + 8)]
           // internal UInt32 frames_count; ///< stats counter for interval
           ////[FieldOffset(528 + 4)]
           // internal UInt32 frames_fsk; ///< stats counter for interval
           ////[FieldOffset(532 + 4)]
           // internal UInt32 frames_events; ///< stats counter for interval
           ////[FieldOffset(536 + 4)]
           // internal IntPtr mgr;    //internal struct mg_mgr *mgr;
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)] // , Pack = 8
        internal unsafe struct R_deviceToPlugin
        {
            internal IntPtr Key_Device;
            internal IntPtr Value_Device;
            internal UInt32 lenForDisplay;
            internal Int32 lenForRecord;
            internal Int32 startForRecord;
            internal Int32 startForDisplay;
            internal UInt16 nbInfosDevice;
            internal UInt16 package_type;   //pulse_detect_package called by classInterfaceWithRtl433 return always 0 (Out of data)
                                            //https://stackoverflow.com/questions/1498931/marshalling-array-of-strings-to-char-in-c-sharp
                                            //c to c# marshaling string array
#if ANALYZE
            internal String row_bits;
            internal String row_bitsBarre; 
#endif
            ////[MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
            ////internal String[] Key_Device;
            ////[MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
            ////internal String[] Value_Device;
            //internal char[] Key_Device;
            //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
            //internal String[] Value_Device;
            //internal char[] Value_Device;
            //internal IntPtr Value_Device;
        }
    }
}