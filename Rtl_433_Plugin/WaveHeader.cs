//https://docs.microsoft.com/fr-fr/archive/blogs/dawate/intro-to-audio-programming-part-3-synthesizing-simple-wave-audio-using-c
using System;
using System.IO;
namespace SDRSharp.Rtl_433
{
    internal class WaveHeader
    {
        internal String sGroupID; // RIFF
        internal UInt32 dwFileLength; // total file length minus 8, which is taken up by RIFF
        internal String sRiffType; // always WAVE
        /// <summary>
        /// Initializes a WaveHeader object with the default values.
        /// </summary>
        internal WaveHeader()
        {
            dwFileLength = 0;
            sGroupID = "RIFF";
            sRiffType = "WAVE";
        }
    }
}
