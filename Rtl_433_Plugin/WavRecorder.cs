using SDRSharp.Radio;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
namespace SDRSharp.Rtl_433
{
    internal unsafe static class WavRecorder
    {
        internal enum RecordType : Int32
        {
            RAW = 0,
            WAV
        }
        internal static void WriteBufferToWav(String filePath, Complex[] buffer, double sampleRate)
        {
            WaveHeader header = new WaveHeader();
            WaveFormatChunk<float> format;
            WaveDataChunk<float> data;
            float coefficient = ClassUtils.GetMaxiTabComplex(buffer);
            Int32 nbChannel = 2;
            format = new WaveFormatChunk<float>((short)nbChannel, (UInt32)sampleRate);
            data = new WaveDataChunk<float>((UInt32)(buffer.Length));
            if (coefficient > 0)
            {
                for (Int32 i = 0; i < (buffer.Length ); i += 2)
                {
                    data.shortArray[i] = buffer[i].Real / coefficient;  //  I    from -1 to +1
                    data.shortArray[i + 1] = buffer[i].Imag / coefficient;  //  Q   from -1 to +1
                }
            }
            if (coefficient > 0.0)
            {
                WriteFileWav(filePath, header, format, data);
            }
            else
                MessageBox.Show("No record, all values = 0", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        internal static void WriteBufferToWav(String filePath, float[] buffer,  double sampleRate)
        {
            WaveHeader header = new WaveHeader();
            WaveFormatChunk<float> format;
            WaveDataChunk<float> data;
             Int32 nbChannel = 2;
            format = new WaveFormatChunk<float>((short)nbChannel, (UInt32)sampleRate);
            data = new WaveDataChunk<float>((UInt32)(buffer.Length)); //* nbChannel
            double coefficient = ClassUtils.GetMaxiTabFloat(buffer);  //not ok with .Max() not abs value
            if (coefficient > 0.0)
            {
                for (Int32 i = 0; i < (buffer.Length); i++)
                    data.shortArray[i] = (float)(buffer[i] / coefficient);  //  I    from -1 to +1
            }
            if (coefficient > 0.0)
            {
                WriteFileWav(filePath, header, format, data);
            }
            else
                MessageBox.Show("No record, all values = 0", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        internal static void WriteBufferToWav(String filePath, byte[] buffer, double sampleRate)
        {
            WaveHeader header = new WaveHeader();
            WaveFormatChunk<float> format;
            WaveDataChunk<float> data;
            Int32 nbChannel = 2;
            format = new WaveFormatChunk<float>((short)nbChannel, (UInt32)sampleRate);
            data = new WaveDataChunk<float>((UInt32)(buffer.Length));
            if(buffer.Count()==0)
                    MessageBox.Show("No record for:" + filePath, "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                float[] tabFloat=new float[buffer.Length];
                Boolean ret = ClassUtils.ConvertCu8ToFloat(buffer, tabFloat);
                if (ret)
                {
                    data.shortArray = tabFloat; //------------------------->??? without readOnly
                     WriteFileWav(filePath, header, format, data);
                }
                //float maxi = buffer.Max() / 2;  //Max() OK all > 0
                //if (maxi > 0.0)
                //{
                //    for (Int32 i = 0; i < buffer.Length; i++)
                //        data.shortArray[i] = (buffer[i] - 127) / maxi;  //from -1 to +1   0--->-1  255--->1   127--->0
                //    writeFileWav(filePath, header, format, data);
                //}
                else
                    MessageBox.Show("No record, all values = 0", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        private static void WriteFileWav(String filePath, WaveHeader header, WaveFormatChunk<float> format,
        WaveDataChunk<float> data)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            try
            {
                using (BinaryWriter writer = new BinaryWriter(fileStream))
                {
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
                    UInt32 filesize = (UInt32)writer.BaseStream.Length;
                    writer.Write(filesize - 8);
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
  
        internal static void WriteByte(String fileName, byte[] dataCu8)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            try
            {
                using (BinaryWriter writer = new BinaryWriter(fileStream))
                {
                    writer.Write(dataCu8);
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
 
         internal static Int32 ConvertCu8ToWav(String fileName)
        {
            Complex[] _dstWavPtr;
            byte[] dataCu8;
            Int32 sampleRate = 0;
            if (File.Exists(fileName))
            {
                sampleRate = GetSampleRateFromName(fileName);

                if (sampleRate == -1)
                {
                    return -1;
                }
                using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
                {
                    dataCu8 = new byte[reader.BaseStream.Length];
                    dataCu8 = reader.ReadBytes((Int32)reader.BaseStream.Length);
                }
                Int32 maxi = dataCu8.Max();  //Max() OK all > 0
                if (maxi != 0)
                {
                    _dstWavPtr = ClassUtils.ConvertByteToComplexPtr(dataCu8);
                    String newName = fileName.Remove(fileName.Length - 4) + ".wav";
                    WriteBufferToWav(newName, _dstWavPtr, sampleRate); //dataCu8.Length / 2,
                }
                else
                    MessageBox.Show("No record, all values = 0", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return sampleRate;
        }
        internal static Int32 GetSampleRateFromName(String fileName)
        {
            String sampleRateStr;
            fileName = Path.GetFileName(fileName);
            sampleRateStr = GetString(fileName, "k");
            if (sampleRateStr != "" && Int32.TryParse(sampleRateStr, out Int32 sampleRate))
                return sampleRate * 1000;
            else
                return -1;
        }
        internal static string GetFrequencyFromName(String fileName)
        {
            string[] units = { "M", "Khz", "Hz", "k", "m", "khz", "HZ", "K" };
            String freqStr;
            fileName = Path.GetFileName(fileName);
            foreach (string unit in units)
            {
                freqStr = GetString(fileName, unit);
                if (freqStr != "") 
                    return freqStr + unit;
            }
            return "";
        }
        internal static string GetString(string fileName, string unit)
        {
            Int32 lastCar;
            Int32 startCar;
            Int32 end = fileName.Length - 3;
            string retString = "";
            for (Int32 i = end; i > 0; i--)
            {
                if (fileName.Substring(i, 1) == unit)
                {
                    lastCar = i - 1;
                    for (--i; i > 0; i--)
                    {
                        if (fileName.Substring(i, 1) == "_")
                        {
                            startCar = i + 1;
                            retString = fileName.Substring(startCar, lastCar - startCar + 1);
                            break;
                        }
                    }
                }
                if ( float.TryParse(retString, out float ret))   //found for another unit
                    break;
            }
            return retString;
        }
    }
}

