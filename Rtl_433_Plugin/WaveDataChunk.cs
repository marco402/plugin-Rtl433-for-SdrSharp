using System;
using System.Linq;
using System.Runtime.InteropServices;
namespace SDRSharp.Rtl_433
{
    internal class WaveDataChunk<T> where T : struct, IConvertible
    {
        internal readonly String sChunkID;     // "data"
        internal readonly UInt32 dwChunkSize;    // Length of data chunk in bytes
        internal  T[] shortArray;  // 8-bit audio          readonly
        /// <summary>
        /// Initializes a new data chunk with a specified capacity.
        /// </summary>
        internal WaveDataChunk(UInt32 capacity)
        {
            shortArray = new T[capacity];
            dwChunkSize = (UInt32)(Marshal.SizeOf<T>() * capacity);
            //problem to float /complex
            sChunkID = "data";
        }
    }
}
