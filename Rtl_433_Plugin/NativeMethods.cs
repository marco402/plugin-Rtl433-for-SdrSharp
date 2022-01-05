using System;
using System.Runtime.InteropServices;
namespace SDRSharp.Rtl_433
{
    public static class NativeMethods
    {
        private const string LibRtl_433 = "rtl_433";

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void receiveMessagesCallback([In]char[] text, [In]int len);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void ptrFct([In, MarshalAs(UnmanagedType.LPStr)] String message);

        [DllImport("rtl_433", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "test_dll_get_version")]
        public static extern IntPtr IntPtr_Pa_GetVersionText();

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void ptrFctInit(IntPtr _ptrCbData,
            Int32 _bufNumber,
            UInt32 _bufLength,
            IntPtr _ptrCtx,
            IntPtr _ptrCfg);

        [DllImport("rtl_433", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern void rtl_433_call_main([Out] ptrFct fctMessage, [Out] ptrFctInit fctInitCbData, [Out] UInt32 param_samp_rate, [Out] Int32 param_sample_size, [Out] UInt32 disabled, [Out] Int32 argc, String[] args);  // 

        [DllImport("rtl_433", CallingConvention = CallingConvention.StdCall)]
        public static extern void receive_buffer_cb([Out] byte[] iq_buf, [Out] UInt32 len,[Out]  IntPtr ctx);

        [DllImport("rtl_433", CallingConvention = CallingConvention.StdCall)]
        public static extern void stop_sdr([Out] IntPtr ctx);

        [DllImport("rtl_433", CallingConvention = CallingConvention.StdCall)]
        public static extern void setFrequency([Out] UInt32 frequency);

        [DllImport("rtl_433", CallingConvention = CallingConvention.StdCall)]
        public static extern void setCenterFrequency([Out] UInt32 centerFrequency);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
        public struct list
        {
            public IntPtr elems;   // void** elems;
            public UInt32 size;        //size_t
            public UInt32 len;        //size_t
        }
        //list_t;
        public enum conversion_mode_t : Int32
        {
            CONVERT_NATIVE,
            CONVERT_SI,
            CONVERT_CUSTOMARY
        }
        public enum time_mode_t : Int32
        {
            REPORT_TIME_DEFAULT,
            REPORT_TIME_DATE,
            REPORT_TIME_SAMPLES,
            REPORT_TIME_UNIX,
            REPORT_TIME_ISO,
            REPORT_TIME_OFF
        }


        /// state data for pulse_FSK_detect()
        public enum _fsk_state : Int32
        {
            PD_FSK_STATE_INIT = 0,   //< Initial frequency estimation
            PD_FSK_STATE_FH = 1,     //< High frequency (pulse)
            PD_FSK_STATE_FL = 2,     //< Low frequency (gap)
            PD_FSK_STATE_ERROR = 3   //< Error - stay here until cleared
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
        public struct pulse_FSK_state_t
        {
            public UInt32 fm_f1_est; ///< Estimate for the F1 frequency for FSK
            public Int32 fm_f2_est; ///< Estimate for the F2 frequency for FSK
            public UInt32 fsk_pulse_length; ///< Counter for internal FSK pulse detection
            public _fsk_state fsk_state;
            public Int16 var_test_max;
            public Int16 var_test_min;
            public Int16 maxx;
            public Int16 minn;
            public Int16 midd;
            public Int32 skip_samples;
    }


        public enum _ook_state : Int32
        {
            PD_OOK_STATE_IDLE = 0,
            PD_OOK_STATE_PULSE = 1,
            PD_OOK_STATE_GAP_START = 2,
            PD_OOK_STATE_GAP = 3
        }
        
        /// Internal state data for pulse_pulse_package()
        struct pulse_detect
        {
            public Int32 use_mag_est;          ///< Whether the envelope data is an amplitude or magnitude.
            public Int32 ook_fixed_high_level; ///< Manual detection level override, 0 = auto.
            public Int32 ook_min_high_level;   ///< Minimum estimate of high level (-12 dB: 1000 amp, 4000 mag).
            public Int32 ook_high_low_ratio;   ///< Default ratio between high and low (noise) level (9 dB: x8 amp, 11 dB: x3.6 mag).

            public _ook_state ook_state;
            public Int32 pulse_length; ///< Counter for internal pulse detection
            public Int32 max_pulse;    ///< Size of biggest pulse detected

            public Int32 data_counter;    ///< Counter for how much of data chunk is processed
            public Int32 lead_in_counter; ///< Counter for allowing initial noise estimate to settle

            public Int32 ook_low_estimate;  ///< Estimate for the OOK low level (base noise level) in the envelope data
            public Int32 ook_high_estimate; ///< Estimate for the OOK high level

            public Int32 verbosity; ///< Debug output verbosity, 0=None, 1=Levels, 2=Histograms

            public pulse_FSK_state_t FSK_state;
        };


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
        public struct sdr_dev
        {
            public UInt32 rtl_tcp;  //SOCKET
            public UInt32 rtl_tcp_freq; ///< last known center frequency, rtl_tcp only.
            public UInt32 rtl_tcp_rate; ///< last known sample rate, rtl_tcp only.

            //# ifdef SOAPYSDR
            //            SoapySDRDevice* soapy_dev;
            //            SoapySDRStream* soapy_stream;
            //            double fullScale;
            //#endif

            //# ifdef RTLSDR
            public IntPtr rtlsdr_dev;  //rtlsdr_dev_t*
            public IntPtr rtlsdr_cb;   //sdr_event_cb_t
            public IntPtr rtlsdr_cb_ctx;    //void*
                                            //#endif

            public String dev_info;

            public Int32 running;
            public Int32 polling;
            public IntPtr buffer;   //void*
            public UInt32  buffer_size;   //size_t

            public Int32 sample_size;
            Int32 sample_signed;
            public Int32 apply_rate;
            public Int32 apply_freq;
            public Int32 apply_corr;
            public Int32 apply_gain;
            public UInt32 sample_rate;
            public Int32 freq_correction;
            public UInt32 center_frequency;
            public String gain_str;
        }
        /// Data for a compact representation of generic pulse train.
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
        public struct pulse_data
        {
            public UInt64 offset;            ///< Offset to first pulse in number of samples from start of stream.
            public UInt32 sample_rate;       ///< Sample rate the pulses are recorded with.
            public UInt32 depth_bits;        ///< Sample depth in bits.
            public UInt32 start_ago;         ///< Start of first pulse in number of samples ago.
            public UInt32 end_ago;           ///< End of last pulse in number of samples ago.
            public UInt32 num_pulses;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1200)]
            public Int32[] pulse;   ///< Width of pulses (high) in number of samples.
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1200)]
            public Int32[] gap;     ///< Width of gaps between pulses (low) in number of samples.
            public Int32 ook_low_estimate;       ///< Estimate for the OOK low level (base noise level) at beginning of package.
            public Int32 ook_high_estimate;      ///< Estimate for the OOK high level at end of package.
            public Int32 fsk_f1_est;             ///< Estimate for the F1 frequency for FSK.
            public Int32 fsk_f2_est;             ///< Estimate for the F2 frequency for FSK.
            public Single freq1_hz;
            public Single freq2_hz;
            public Single centerfreq_hz;
            public Single range_db;
            public Single rssi_db;
            public Single snr_db;
            public Single noise_db;
        }
        //pulse_data_t;

        public const Int32 FILTER_ORDER = 1;

        /// Filter state buffer.
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
        public struct filter_state
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = FILTER_ORDER)]
            public Int16[] y;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = FILTER_ORDER)]
            public Int16[] x;
        }
       // filter_state_t;

/// FM_Demod state buffer.
     [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
public struct demodfm_state
        {
            public Int32 xr;        ///< Last I/Q sample, real part
            public Int32 xi;        ///< Last I/Q sample, imag part
            public Int32 xf;        ///< Last Instantaneous frequency
            public Int32 yf;        ///< Last Instantaneous frequency, low pass filtered
            public UInt32 rate;     ///< Current sample rate
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public Int32[] alp_16; ///< Current low pass filter A coeffs, 16 bit
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public Int32[] blp_16; ///< Current low pass filter B coeffs, 16 bit
            public Int32 bidon;   //-------------------------------------------------------<why?
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public Int64[]  alp_32; ///< Current low pass filter A coeffs, 32 bit
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public Int64[] blp_32; ///< Current low pass filter B coeffs, 32 bit
        }
        //demodfm_state_t;
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
        public struct file_info{
        public UInt32 format;
        public UInt32 raw_format;
        public UInt32 center_frequency;
        public UInt32 sample_rate;
        public String spec;
        public String path;
        public IntPtr file;        //FILE*
    }
        //file_info_t;
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
        public struct timeval
        {
            public Int32 tv_sec;         /* seconds */
            public Int32 tv_usec;        /* and microseconds */
        }
        public const Int32 MAXIMAL_BUF_LENGTH = (256 * 16384);
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 4)]
        public struct dm_state
        {
            float auto_level;
            float squelch_offset;
            public Single level_limit;
            float noise_level;
            float min_level_auto;
            public Single min_level;
            public Single min_snr;
            public Single low_pass;
            public Int32 use_mag_est;
            public Int32 detect_verbosity;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAXIMAL_BUF_LENGTH)]
            public Int16[] am_buf;  // AM demodulated signal (for OOK decoding)
            //union {
            //    // These buffers aren't used at the same time, so let's use a union to save some memory
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAXIMAL_BUF_LENGTH)]
            public Int16[] fm;  // FM demodulated signal (for FSK decoding)
                                //    [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAXIMAL_BUF_LENGTH)]
                                //    public UInt16[] temp;  // Temporary buffer (to be optimized out..)
                                //}
                                //buf;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAXIMAL_BUF_LENGTH)]
            public Byte[] u8_buf; // format conversion buffer
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAXIMAL_BUF_LENGTH)]
            public Single[] f32_buf; // format conversion buffer
            public Int32 sample_size; // CU8: 1, CS16: 2--->*2 in rtl_433
            public IntPtr  pulse_detect;        //pulse_detect_t*
            public filter_state lowpass_filter_state;
            public Int32 bidon;         //-----------------------------<why?
            public demodfm_state demod_FM_state;     //demodfm_state
            public Int32 enable_FM_demod;
            public UInt32 fsk_pulse_detect_mode;
            public UInt32 frequency;
            public IntPtr  samp_grab;        //samp_grab_t*
            public IntPtr am_analyze;          //am_analyze_t* 
            public Int32 analyze_pulses;
            public file_info load_info;
            public list dumper;

            /* Protocol states */
            public list r_devs;
            public Int32 bidon1;         //-----------------------------<why?
            public pulse_data pulse_data;       //pulse_data
            public pulse_data fsk_pulse_data;   //pulse_data
            public UInt32 frame_event_count;
            public UInt32 frame_start_ago;
            public UInt32 frame_end_ago;
            public timeval now;        //struct  int32 and not long -----------------------------<why?
            public Single sample_file_pos;
        }

    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi, Pack = 4)]
        public struct r_cfg
        {
            [FieldOffset(0)]    //,MarshalAs(UnmanagedType.LPStr)
            public String dev_query;
            [FieldOffset(4)]
            public String dev_info;
            [FieldOffset(4+4)]
            public String gain_str;
            [FieldOffset(8+4)]
            public String settings_str;
            [FieldOffset(12+4)]
            public Int32 ppm_error;
            [FieldOffset(16+4)]
            public UInt32 out_block_size;
            [FieldOffset(20+4)]
            public String test_data;
            [FieldOffset(24+4)]
            public list in_files;
            [FieldOffset(28+12)]
            public String in_filename;
            [FieldOffset(40+4)]
            public Int32 replay;
            [FieldOffset(44+4)]
            public Int32 hop_now;
            [FieldOffset(48+4)]
            public Int32 exit_async;
            [FieldOffset(52+4)]
            public Int32 exit_code; ///< 0=no err, 1=params or cmd line err, 2=sdr device read error, 3=usb init error, 5=USB error (reset), other=other error
            [FieldOffset(56+4)]
            public Int32 frequencies;
            [FieldOffset(60+4)]
            public Int32 frequency_index;
            [FieldOffset(64+4)]
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public UInt32[] frequency;
            [FieldOffset(68+32*4)]
            public UInt32 center_frequency;
            [FieldOffset(196+4)]
            public Int32 fsk_pulse_detect_mode;
            [FieldOffset(200+4)]
            public Int32 hop_times;
            [FieldOffset(204+4)]
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public Int32[] hop_time;
            [FieldOffset(208+32*4+4)]    //-----------------------------------<why +4?
            public Int64 hop_start_time;   //time_t
            [FieldOffset(340+8)]
            public Int32 duration;
            [FieldOffset(348+4)]
            public Int64 stop_time;   //time_t
            [FieldOffset(352+8)]
            public Int32 after_successful_events_flag;
            [FieldOffset(360+4)]
            public UInt32 samp_rate;
            [FieldOffset(364+4)]
            public UInt64 input_pos;
            [FieldOffset(368+8)]
            public UInt32 bytes_to_read;
            [FieldOffset(376+4)]
            public IntPtr dev;   //  struct sdr_dev *dev;
            [FieldOffset(380+4)]
            public Int32 grab_mode; ///< Signal grabber mode: 0=off, 1=all, 2=unknown, 3=known
            [FieldOffset(384+4)]
            public Int32 raw_mode; ///< Raw pulses printing mode: 0=off, 1=all, 2=unknown, 3=known
            [FieldOffset(388+4)]
            public Int32 verbosity; ///< 0=normal, 1=verbose, 2=verbose decoders, 3=debug decoders, 4=trace decoding.
            [FieldOffset(392+4)]
            public Int32 verbose_bits;
            [FieldOffset(396+4)]
            public conversion_mode_t conversion_mode;
            [FieldOffset(400+4)]
            public Int32 report_meta;
            [FieldOffset(404+4)]
            public Int32 report_noise;
            [FieldOffset(408+4)]
            public Int32 report_protocol;
            [FieldOffset(412+4)]
            public time_mode_t report_time;
            [FieldOffset(416+4)]
            public Int32 report_time_hires;
            [FieldOffset(420+4)]
            public Int32 report_time_tz;
            [FieldOffset(424+4)]
            public Int32 report_time_utc;  //commencer la
            [FieldOffset(428+4)]
            public Int32 report_description;
            [FieldOffset(432+4)]
            public Int32 report_stats;
            [FieldOffset(436+4)]
            public Int32 stats_interval;
            [FieldOffset(440+4)]
            public Int32 stats_now;
            [FieldOffset(444+4)]       //-----------------------------------<why +4?
            public Int64 stats_time;     //time_t
            [FieldOffset(448+8)] 
            public Int32 no_default_devices;
            [FieldOffset(456+4)]
            public IntPtr devices;       //public struct r_device *devices;
            [FieldOffset(460+4)]
            public UInt16 num_r_devices;
            [FieldOffset(464+4)]        //not 2 ?
            public list data_tags;
            [FieldOffset(468+12)]
            public list output_handler;
            [FieldOffset(480+12)]
            public IntPtr demod;     //public struct dm_state *demod;
            [FieldOffset(492+4)]
            public String sr_filename;
            [FieldOffset(496+4)]
            public Int32 sr_execopen;
            [FieldOffset(500+4)]
            //public Int32 old_model_keys;
            //[FieldOffset(508+4)]    //-----------------------------------<why?
            /* stats*/
            public Int64 frames_since;    //time_t
            [FieldOffset(504+8)]
            public UInt32 frames_count; ///< stats counter for interval
            [FieldOffset(512+4)]
            public UInt32 frames_fsk; ///< stats counter for interval
            [FieldOffset(516+4)]
            public UInt32 frames_events; ///< stats counter for interval
            [FieldOffset(520+4)]
            public IntPtr mgr;    //public struct mg_mgr *mgr;
        }
    }
}
