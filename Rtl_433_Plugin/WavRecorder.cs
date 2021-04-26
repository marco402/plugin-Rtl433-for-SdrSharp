﻿using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SDRSharp.Radio;

namespace SDRSharp.Rtl_433
{
   unsafe public static class wavRecorder
    {
        public enum recordType
        {
            MONO=0,    //baseBandForAudacity
            STEREO      //IQ
        }
        
        //private static int cpt = 0;
        public static void WriteBufferToWav(string filePath, Complex* buffer,  int lenBuffer, double _sampleRate, recordType recordType = recordType.STEREO)
        {
            //Func<float, float> testlimit = value =>
            //{
            //    cpt++;
            //    float d = (float)(Math.Min(value, 1));
            //    return (float)(Math.Max(d, -1));
            //};
           // cpt = 0;
            WaveHeader header = new WaveHeader();
            WaveFormatChunk<float> format;
            WaveDataChunk<float> data;
            float maxi = 0;
            if (recordType == recordType.MONO)
            {
                int nbChannel = 1;
                format = new WaveFormatChunk<float>((short)nbChannel, (uint)_sampleRate);
                data = new WaveDataChunk<float>((uint)(lenBuffer * nbChannel));
                maxi = 0;
                for (var i = 0; i < lenBuffer; i++)
                {
                     if (Math.Abs(buffer[i].Modulus()) > maxi)
                        maxi = buffer[i].Modulus();
                }
                for (var i = 0; i < lenBuffer; i++)
                {
                    data.shortArray[i] = buffer[i].Modulus()/maxi;  //from -1 to +1
                }

            }
            else //if (recordType == recordType.STEREO)  data and format not assigned ??
            {
                int nbChannel = 2;
                format = new WaveFormatChunk<float>((short)nbChannel, (uint)_sampleRate);
                data = new WaveDataChunk<float>((uint)(lenBuffer * nbChannel));
                maxi = wavRecorder.getMaxi(buffer, lenBuffer);
                if(maxi!=0)
                {
                    for (var i = 0; i < lenBuffer; i++)
                    {
                        data.shortArray[i*2] = buffer[i].Real/maxi;  //from -1 to +1
                        data.shortArray[(i*2)+1] = buffer[i].Imag/maxi;  //from -1 to +1
                    }
                }
            }
            if (maxi != 0)
            {
                try
                {
                    FileStream fileStream = new FileStream(filePath, FileMode.Create);
                    BinaryWriter writer = new BinaryWriter(fileStream);
                    writer.Write(header.sGroupID.ToCharArray());
                    writer.Write(header.dwFileLength);
                    writer.Write(header.sRiffType.ToCharArray());

                    writer.Write(format.sChunkID.ToCharArray());
                    writer.Write(format.dwChunkSize);
                    writer.Write(format.wFormatTag);
                    writer.Write(format.wChannels);
                    writer.Write(format.dwSamplesPerSec);
                    writer.Write(format.dwAvgBytesPerSec);
                    writer.Write(format.wBlockAlign);
                    writer.Write(format.wBitsPerSample);

                    writer.Write(data.sChunkID.ToCharArray());
                    writer.Write(data.dwChunkSize);
                    foreach (float dataPoint in data.shortArray)
                        writer.Write(dataPoint);
                    writer.Seek(4, SeekOrigin.Begin);
                    uint filesize = (uint)writer.BaseStream.Length;
                    writer.Write(filesize - 8);
                    writer.Close();
                    fileStream.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            else
                MessageBox.Show("No record, all values = 0", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        public static float getMaxi(Complex * bufferPtr,int length)
        {
            float maxi = float.MinValue;
            for (var i = 0; i< length; i++)
            {
                if (Math.Abs(bufferPtr[i].Real) > maxi)
                    maxi = Math.Abs(bufferPtr[i].Real);
                if (Math.Abs(bufferPtr[i].Imag) > maxi)
                    maxi = Math.Abs(bufferPtr[i].Imag);
            }
            return maxi;
        }
        public static void convertCu8ToWav(string fileName,bool mono,bool stereo)
        {
            Complex* _dstWavPtr;
            UnsafeBuffer _dstWavBuffer;
            byte[] dataCu8;
           
            if (File.Exists(fileName))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
                {
                    dataCu8 = new byte[reader.BaseStream.Length];
                    dataCu8 = reader.ReadBytes((int)reader.BaseStream.Length);
                }
                _dstWavBuffer = UnsafeBuffer.Create((int)(dataCu8.Length/2), sizeof(Complex));
                _dstWavPtr = (Complex*)_dstWavBuffer;
                int maxi = dataCu8.Max();
                if (maxi != 0)
                {
                    for (int i = 0; i < dataCu8.Length; i += 2)
                    {
                        _dstWavPtr[i / 2].Real = dataCu8[i] - 127;   // 0-->-127   255->128   / maxi * 255;    // float.MaxValue;
                        _dstWavPtr[i / 2].Imag = dataCu8[i + 1] - 127;    ///maxi*float.MaxValue;
                    }
                    Int32 sampleRate = getSampleRateFromName(fileName); //lacrosse_g2750_915M_1000k.cu8,9_ford-unlock002.cu8
                    string newName = "";
                    if (stereo)
                    { 
                    newName = fileName.Replace(".cu8", "_STEREO.wav");
                    WriteBufferToWav(newName, _dstWavPtr, _dstWavBuffer.Length, sampleRate, wavRecorder.recordType.STEREO);
                    }
                    if(mono)
                    {
                    newName = fileName.Replace(".cu8", "_MONO.wav");
                    WriteBufferToWav(newName, _dstWavPtr, _dstWavBuffer.Length, sampleRate, wavRecorder.recordType.MONO);
                    }
                    MessageBox.Show("Recording is completed", "Translate cu8 to wav", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("No record, all values = 0", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _dstWavBuffer.Dispose();

            }
        }

        private static Int32 getSampleRateFromName(string fileName)
        {
            Int32 sampleRate = 250000;
            string sampleRateStr = "";
            Int32 end = fileName.Length - 5;
            Int32 start = 0;
            while (start == 0)
            {
                for (int i = end; i > 0; i--)
                {
                    if (fileName.Substring(i, 1) == "k")
                    {
                        for (i = i + 1; i > 0; i--)
                        {
                            if (fileName.Substring(i, 1) == "_")
                            {
                                start = i + 1;
                                sampleRateStr = fileName.Substring(start, end - start);
                                break;
                            }
                        }
                    }
                }
                break;
            }
            if (start > 0)
            {
                try
                {
                    sampleRate = Int32.Parse(sampleRateStr) * 1000;
                }
                catch
                {
                }

            }
            return sampleRate;
        }

    }
}
