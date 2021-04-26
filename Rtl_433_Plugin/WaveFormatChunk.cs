using System;
using System.Runtime.InteropServices;
namespace SDRSharp.Rtl_433
{
        public class WaveFormatChunk<T> where T : struct, IConvertible
        {
            public readonly string sChunkID;         // Four bytes: "fmt "
            public readonly uint dwChunkSize;        // Length of chunk in bytes
            public readonly ushort wFormatTag;       // 1 (MS PCM)
            public readonly ushort wChannels;        // Number of channels
            public readonly uint dwSamplesPerSec;    // Frequency of the audio in Hz... 44100
            public readonly uint dwAvgBytesPerSec;   // for estimating RAM allocation
            public readonly ushort wBlockAlign;      // sample frame size, in bytes
            public readonly ushort wBitsPerSample;    // bits per sample
            /// <summary>
            /// Initializes a format chunk. Supported data types: byte, short, float
            /// </summary>
            public WaveFormatChunk(short channels, uint samplesPerSec)
            {
                sChunkID = "fmt ";
                dwChunkSize = 16;
                wFormatTag = typeof(T) == typeof(float) || typeof(T) == typeof(double) ? (ushort)3 : (ushort)1;
                wChannels = (ushort)channels;
                dwSamplesPerSec = samplesPerSec;
                wBitsPerSample = (ushort)(Marshal.SizeOf<T>() * 8);
                wBlockAlign = (ushort)(wChannels * ((wBitsPerSample + 7) / 8));
                dwAvgBytesPerSec = dwSamplesPerSec * wBlockAlign;
            }
    }
}
