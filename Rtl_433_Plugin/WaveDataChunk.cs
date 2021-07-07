using System;
using System.Runtime.InteropServices;
using System.Linq;
namespace SDRSharp.Rtl_433
{
    public class WaveDataChunk<T> where T : struct, IConvertible
    {
        public readonly string sChunkID;     // "data"
        public readonly uint dwChunkSize;    // Length of data chunk in bytes
        public readonly T[] shortArray;  // 8-bit audio
        /// <summary>
        /// Initializes a new data chunk with a specified capacity.
        /// </summary>
        public WaveDataChunk(uint capacity)
        {
            shortArray = new T[capacity];
            dwChunkSize = (uint)(Marshal.SizeOf<T>() * capacity/2);   //marc add /2 else half empty with audacity but shortArray needed capacity
            //problem to float /complex
            sChunkID = "data";
        }
    }
}
