using System;
using System.Runtime.InteropServices;
namespace SDRSharp.Rtl_433
{
    internal class WaveFormatChunk<T> where T : struct, IConvertible
        {
        internal readonly String sChunkID;         // Four bytes: "fmt "
        internal readonly uint dwChunkSize;        // Length of chunk in bytes
        internal readonly ushort wFormatTag;       // 1 (MS PCM)
        internal readonly ushort wChannels;        // Number of channels
        internal readonly uint dwSamplesPerSec;    // Frequency of the audio in Hz... 44100
        internal readonly uint dwAvgBytesPerSec;   // for estimating RAM allocation
        internal readonly ushort wBlockAlign;      // sample frame size, in bytes
        internal readonly ushort wBitsPerSample;    // bits per sample
                                                    /// <summary>
                                                    /// Initializes a format chunk. Supported data types: byte, short, float
                                                    /// </summary>
        internal WaveFormatChunk(short channels, uint samplesPerSec)
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
