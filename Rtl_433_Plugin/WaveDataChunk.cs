using System;
using System.Runtime.InteropServices;
using System.Linq;
namespace SDRSharp.Rtl_433
{
    internal class WaveDataChunk<T> where T : struct, IConvertible
    {
        internal readonly String sChunkID;     // "data"
        internal readonly uint dwChunkSize;    // Length of data chunk in bytes
        internal readonly T[] shortArray;  // 8-bit audio
        /// <summary>
        /// Initializes a new data chunk with a specified capacity.
        /// </summary>
        internal WaveDataChunk(uint capacity)
        {
            shortArray = new T[capacity];
            dwChunkSize = (uint)(Marshal.SizeOf<T>() * capacity);   //marc add /2 else half empty with audacity but shortArray needed capacity
            //problem to float /complex
            sChunkID = "data";
        }
    }
}
