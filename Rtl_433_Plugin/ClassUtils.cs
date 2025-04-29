/* Written by Marc Prieur (marco40_github@sfr.fr)
                                ClassUtils.cs 
                            project Rtl_433_Plugin
						         Plugin for SdrSharp
 **************************************************************************************
 Creative Commons Attrib Share-Alike License
 You are free to use/extend this library but please abide with the CC-BY-SA license:
 Attribution-NonCommercial-ShareAlike 4.0 International License
 http://creativecommons.org/licenses/by-nc-sa/4.0/

 All text above must be included in any redistribution.
  **********************************************************************************/
#define noGENERETESTFILEWAV  //for test
using SDRSharp.Radio;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace SDRSharp.Rtl_433
{
    internal class ClassUtils
    {
        internal static string FirstCharToUpper(String s)
        {
            if (String.IsNullOrEmpty(s))
            {
                 return s;
            }
            return s[0].ToString().ToUpper() + s.Substring(1);
        }


        internal static double GetMaxiTabFloat(float[] bufferPtr)
        {
            double maxi = float.MinValue;
            for (var i = 0; i < bufferPtr.Length; i++)
                if (Math.Abs(bufferPtr[i]) > maxi)
                    maxi = Math.Abs(bufferPtr[i]);
            return maxi;
        }

        /// <summary>
        /// maximum absolu of real and imag
        /// </summary>
        /// <param name="bufferPtr"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        internal unsafe static float GetMaxiPtrComplex(Complex* bufferPtr, Int32 length)
        {
            float maxi = float.MinValue;
            for (var i = 0; i < length; i++)
            {
                if (Math.Abs(bufferPtr[i].Real) > maxi)
                    maxi = Math.Abs(bufferPtr[i].Real);
                if (Math.Abs(bufferPtr[i].Imag) > maxi)
                    maxi = Math.Abs(bufferPtr[i].Imag);
            }
            return maxi;
        }
        internal static float GetMaxiTabComplex(Complex[] bufferPtr)
        {
            float maxi = float.MinValue;
            for (var i = 0; i < bufferPtr.Length; i++)
            {
                if (Math.Abs(bufferPtr[i].Real) > maxi)
                    maxi = Math.Abs(bufferPtr[i].Real);
                if (Math.Abs(bufferPtr[i].Imag) > maxi)
                    maxi = Math.Abs(bufferPtr[i].Imag);
            }
            return maxi;
        }
#if DEBUG
        internal static void TestcopyMem()
        {

        float[] src = new float[10];
        for (Int32 i = 0; i < 10; i++)
             src[i] = i;

        float[] dst = new float[10];
        CopyMem(dst, src, 0, 0, 10);

        float[] dst1 = new float[5];
        CopyMem(dst1, src, 5, 0, 5);

        float[] dst2 = new float[5];
        CopyMem(dst2, src, 0, 5, 5);
        } 
#endif
        internal static unsafe Boolean CopyMem(float[] retourIQ, float[] ptrTabIQ, Int32 decaleSrc, Int32 decaleDst, Int32 len)
        {

            if ((decaleDst + len) > retourIQ.Length)
                decaleDst = retourIQ.Length - len;
            if (decaleDst < 0)
            {
                decaleDst = 0;
                len = retourIQ.Length;   //plantage nbrepeat=12 supression de nbrepeat->decoder_util.c->decoder_output_data
            }
            if (decaleDst + len < 0)
                return false;
            if ((decaleSrc + len) > ptrTabIQ.Length)
            {
                Int32 lenEnd = ptrTabIQ.Length - decaleSrc;
                Int32 lenStart = len - lenEnd;
                fixed (float* pointeurSrc = &ptrTabIQ[decaleSrc])
                {
                    fixed (float* pointeurDst = &retourIQ[decaleDst])
                    {
                        Utils.Memcpy(pointeurDst, pointeurSrc, lenEnd * sizeof(float));
                    }
                }
                fixed (float* pointeurSrc = &ptrTabIQ[0])
                {
                    fixed (float* pointeurDst = &retourIQ[decaleDst + lenEnd])
                    {
                        Utils.Memcpy(pointeurDst, pointeurSrc, lenStart * sizeof(float));
                    }
                }
            }
            else
            {
                fixed (float* pointeurSrc = &ptrTabIQ[decaleSrc])
                {
                    fixed (float* pointeurDst = &retourIQ[decaleDst])
                    {
                        Utils.Memcpy(pointeurDst, pointeurSrc, len * sizeof(float));
                    }
                }
            }
            return true;
        }
#if DEBUG
        internal unsafe static void TestComplexToFloatArray()
        {
            try
            {
                Complex[] src = new Complex[10];
                fixed (Complex* ptrSrc = src)
                {
                    for (Int32 i = 0; i < 10; i++)
                         ptrSrc[i] = new Complex(i+1, (i+1) * 10);
                    float[] dst = new float[20];
                    ComplexToFloatArray(ptrSrc, dst, 0, 20);

                    float[] dst1 = new float[20];
                    ComplexToFloatArray(ptrSrc, dst1, 10, 10);

                    float[] dst2 = new float[20];
                    ComplexToFloatArray(ptrSrc, dst2, 0, 10);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "   testComplexToFloatArray", "Error Initialize", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
#endif
        internal unsafe static void ComplexToFloatArray(Complex* tabComplex, float[] retFloat, Int32 Start, Int32 tabFloatSize)
        {
            IntPtr floatPointer = (IntPtr)tabComplex;
            Marshal.Copy(floatPointer, retFloat, Start, tabFloatSize);
        }

        internal static String GetPathAndNameFileDateAndTxt(String Name)
        {
            String Path=  ClassFunctionsVirtualListView.ValideNameFile(Name + "_" + DateTime.Now.ToString() + ".txt", "_").Replace(" ", "_");
            Path=Path.Replace("__", "_");
            if (Path.StartsWith("_"))
                Path=Path.Remove(0, 1);
            Path = GetDirectoryRecording().Replace(" ", "_") + Path;
            return Path;
        }

        internal static String GetDirectoryRecording()
        {
            String directory = ClassConst.FOLDERRECORD;   //SDRSHARP.exe to SDRSHARP
            if (!Directory.Exists(directory))
            {
                directory = "." + ClassConst.FOLDERRECORD;  //SDRSHARP.exe to bin
                if (!Directory.Exists(directory))
                    directory = "";
            }
            return directory; //+ ClassConst.FILELISTEDEVICES;
        }
        //internal static Boolean CreateFolderRecord()
        //{
        //    if (!Directory.Exists(ClassConst.FOLDERRECORD))
        //    {
        //        try
        //        {
        //            Directory.CreateDirectory(ClassConst.FOLDERRECORD);
        //        }
        //        catch (Exception e)
        //        {
        //            MessageBox.Show(e.Message, "Error create folder " + ClassConst.FOLDERRECORD, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            return false;
        //        }
        //    }
        //    return true;
        //}
        internal static Int32 RecordDevice(String name, byte[] dataIQ,Int32 sampleRate, String frequency = "", Boolean Wav = true)
        {

            //if (name.ToLower().Contains("o"))
            //    Debug.WriteLine("o: " + name);
            //name=name.Replace("o", "a");
            //name=name.Replace("O", "A");
            String directory = GetDirectoryRecording();
            name = name.Replace(" ", "_");
            name = name.Replace(":", "_");
            String _nameFile ;
            Int32 sampleRateFromFileName = 0;
            if (sampleRate == 0 && frequency == "")
            {
                string fileName = Path.GetFileName(name);
                if (sampleRate == 0)
                {
                    sampleRateFromFileName = WavRecorder.GetSampleRateFromName(fileName); //lacrosse_g2750_915M_1000k.cu8,9_ford-unlock002.cu8
                    if (sampleRateFromFileName == -1)
                    {
                        sampleRateFromFileName = 250;
                        sampleRate = 250000;
                        //MessageBox.Show("No sample rate detected in the file name", "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //return -1;
                    }
                    else
                        sampleRate = sampleRateFromFileName;
                }
                string frequencyFromFileName = "";
                String newfileName = fileName.Replace(".cu8", "_");
                if (frequency == "")
                {
                    frequencyFromFileName = WavRecorder.GetFrequencyFromName(fileName);
                    newfileName=newfileName.Replace("_"+frequencyFromFileName, "");
                }
                if (frequencyFromFileName == "")
                {
                    if (sampleRateFromFileName > 0)
                    { 
                        if (frequency == "")
                            frequency = "_433000000";  //add for cu8 without freq and samplerate
                        _nameFile = directory + newfileName + frequency + "hz_" + sampleRateFromFileName + "k_";  //add sampleRateFromFileName for cu8 without freq and samplerate
                    }
                    else
                        _nameFile = directory + newfileName + frequency + "hz_" + (sampleRate / 1000).ToString() + "k_";
                }
                else
                {
                    if (frequencyFromFileName.EndsWith("M"))  //for cu8 download issue13   M not ok for SDRSharp 1632
                    {
                        frequencyFromFileName = frequencyFromFileName.Replace(".", ",");
                        double.TryParse(frequencyFromFileName.Substring(0,frequencyFromFileName.Length-1), out double freq);
                        Int32 IntFrequencyFromFileName = (Int32)(freq * 1000000.0);
                        frequencyFromFileName = IntFrequencyFromFileName.ToString("D") + "hz";
                    }

                    if (sampleRateFromFileName > 0)
                        _nameFile = directory + newfileName + frequencyFromFileName + "_" ;
                    else
                        _nameFile = directory + newfileName + frequencyFromFileName + "_"  + (sampleRate / 1000).ToString() + "k_";
                }
            }
            else
                _nameFile = directory + name + "_" + frequency + "_" + (sampleRate / 1000).ToString() + "k_";
            _nameFile += (DateTime.Now.Date.ToString("d").Replace("/", "_") + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second);
            //if (sampleRate == 0)
            //    sampleRate = sampleRateFromFileName*1000;


            //if(sampleRate==250)
            //    Debug.WriteLine("250: " + name);


            if (dataIQ != null)
            {
                if (Wav)
                {
                    _nameFile += ".wav";
                    Debug.WriteLine(sampleRate.ToString()+"  "+_nameFile);
                    WavRecorder.WriteBufferToWav(_nameFile, dataIQ, sampleRate);
                }
                else
                {
                    _nameFile += ".cu8";
                    WavRecorder.WriteByte(_nameFile, dataIQ);
                }
            }
            return sampleRate;
        }
 
        internal static Int32 RecordDevice(String name, float[] dataIQForRecord, Int32 sampleRate = 0, String frequency = "")
        {
            if (dataIQForRecord.Length < 2)
                return -1;
            String directory = GetDirectoryRecording();
            name = name.Replace(" ", "_");
            name = name.Replace(":", "_");
            String _nameFile = "";
            Int32 sampleRateFromFileName = 0;
            if (sampleRate == 0 && frequency == "")
            {
                string fileName = Path.GetFileName(name);
                if (sampleRate == 0)
                {
                    sampleRateFromFileName = WavRecorder.GetSampleRateFromName(fileName); //lacrosse_g2750_915M_1000k.cu8,9_ford-unlock002.cu8
                    if (sampleRateFromFileName == -1)
                    {
                        //MessageBox.Show("No sample rate detected in the file name", "Cancel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return -1;
                    }
                }
                string frequencyFromFileName = "";
                if (frequency == "")
                    frequencyFromFileName = WavRecorder.GetFrequencyFromName(fileName);
                String newfileName = fileName.Replace(".cu8", "_");
                if (frequencyFromFileName == "")
                {
                    if (sampleRateFromFileName > 0)
                        _nameFile = directory + newfileName + frequency + "hz_";
                    else
                        _nameFile = directory + newfileName + frequency + "hz_" + (sampleRate / 1000).ToString() + "k_";
                }
                else
                {
                    if (sampleRateFromFileName > 0)
                        _nameFile = directory + newfileName + "_";
                    else
                        _nameFile = directory + newfileName + (sampleRate / 1000).ToString() + "k_";
                }
            }
            else
                _nameFile = directory + name + "_" + frequency + "_" + (sampleRate / 1000).ToString() + "k_";
            _nameFile += (DateTime.Now.Date.ToString("d").Replace("/", "_") + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second);
            if (sampleRate == 0)
                sampleRate = sampleRateFromFileName;

                //if (ClassUtils.Wav)
                //{
                    _nameFile += ".wav";
#if !GENERETESTFILEWAV
                WavRecorder.WriteBufferToWav(_nameFile, dataIQForRecord, sampleRate);
#else
            MessageBox.Show("genere file test wav -127 to 128", "test", MessageBoxButtons.OK, MessageBoxIcon.Warning);
           float[] dataIQ = new float[256];
                //for (Int32 i = -127; i < 129; i++)
                //    dataIQ[i+127] = System.Convert.ToSingle(i);
                //wavRecorder.WriteBufferToWav("C:\\marc\\tnt\\sources\\SDRSharp\\SDRSharp\\bin\\Debug\\Recordings\\testFichierWav.wav", dataIQ, 250000);

           Byte[] tabByte = new Byte[256];
           for (Int32 i = 0; i < 256; i++)
                    tabByte[i] = System.Convert.ToByte(i);
           ClassUtils.convertCu8ToFloat(tabByte, dataIQ);
           //tabByte = null;
           //wavRecorder.WriteBufferToWav("C:\\marc\\tnt\\sources\\SDRSharp\\SDRSharp\\bin\\Debug\\Recordings\\testFichierWav.wav", dataIQ, 250000);
           Byte[] tabByteOut = new byte[dataIQ.Length];
           Boolean ret=ClassUtils.convertFloatToByte1(dataIQ, tabByteOut);
           //tabByteOut = null;          //62=63 et 191=192 precision...
                for (Int32 i = 0; i < 256; i++)
                    Debug.WriteLine(i + "  " + (tabByte[i] - tabByteOut[i]).ToString());
            

           //Byte[] tabByteOut = ClassUtils.convertFloatToByte(dataIQ, 1);
#endif
            //}
            //else
            //    {
            //        _nameFile += ".cu8";
            //    //float coefficient = ClassUtils.getMaxiTabFloat(dataIQForRecord);
            //    //Byte[] dataIQ = convertFloatToByte(dataIQForRecord, coefficient);
            //    Byte[] dataIQ = new byte[dataIQForRecord.Length];
            //    Boolean ret= ConvertFloatToByte(dataIQForRecord, dataIQ);
            //    if(ret)
            //        WavRecorder.WriteByte(_nameFile, dataIQ);
 
            //    dataIQ = null;
            //    }

            return sampleRate;
        }
        internal static Boolean ConvertFloatToByte(float[] dataFloat,byte[] dataByte)
        {
            double coefficient = ClassUtils.GetMaxiTabFloat(dataFloat);
            if (coefficient <= 0.0)
                return false;
            Thread.BeginCriticalRegion();
            for (Int32 i = 0; i < dataFloat.Length; i++)
                try
                {
                    dataByte[i] = System.Convert.ToByte(ClassConst.FLOATTOBYTE + (dataFloat[i] / coefficient) * ClassConst.FLOATTOBYTE); 
                }
                catch
                {
                    if((ClassConst.FLOATTOBYTE + (dataFloat[i] / coefficient) * ClassConst.FLOATTOBYTE) <0)
                        dataByte[i] = 0;
                    else
                        dataByte[i] = 255;
                    //Debug.WriteLine("classUtils--> " + (127f + (dataFloat[i] / coefficient) * 127f));
                }
            Thread.EndCriticalRegion();
            return true;
        }

        internal static String GetDeviceName(Dictionary<String, String> listData)
        {
            String key = "";
            String model = "";
            String protocol = "";
            String channel = "";
            String idDEvice = "";
            foreach (KeyValuePair<String, String> _line in listData)
            {
                if (_line.Key.ToUpper().Contains("CHANNEL") && channel == "" && _line.Value != "")
                {
                    channel = _line.Value.Replace(" ", "");
                    key += (" Channel:" + channel);
                }
                else if (_line.Key.ToUpper().Contains("PROTOCOL") && protocol == "" && _line.Value != "")  //protect humidity contain id
                {
                    protocol = _line.Value;
                    key += " Protocol:" + _line.Value.Replace(" ", ""); ;
                }
                else if (_line.Key.ToUpper().Contains("MODEL") && model == "" && _line.Value != "")
                {
                    model = _line.Value;
                    key += " Model: " + _line.Value.Replace(" ", ""); ;
                }
                else if (_line.Key.ToUpper().Contains("IDS") && idDEvice == "" && _line.Value != "")  //ids for honeywell if id pb for tpms
                {
                    idDEvice = _line.Value.Replace(" ", "");
                    key += " Ids: " + idDEvice;
                }
            }
#if TESTWINDOWS
            if (key.Trim() != "")   //test device windows
                key += cptDevicesForTest.ToString();   //test device windows
#endif
            return key;
        }

        internal static Complex[] ConvertByteToComplexPtr(byte[] dataComplexByByte)
        {
            Complex[] ptrRetComplex = new Complex[dataComplexByByte.Length];
            for (Int32 i = 0; i < dataComplexByByte.Length; i += 2)
            {
                ptrRetComplex[i / 2].Real = (float)(dataComplexByByte[i] - 127);   // 0-->-127   255->128   / maxi * 255;    // float.MaxValue;
                ptrRetComplex[i / 2].Imag = (float)(dataComplexByByte[i + 1] - 127);
            }
            return ptrRetComplex;
        }

        internal static Boolean ConvertCu8ToFloat(byte[] bufferByte, float[] bufferFloat)
        {
            double maxi = bufferByte.Max() / 2;  //Max() OK all > 0
            if (maxi > 0.0)
            {
                for (Int32 i = 0; i < bufferByte.Length; i++)
                    bufferFloat[i] = (float)((bufferByte[i] - 127) / maxi);  //from -1 to +1   0--->-1  255--->1   127--->0
                return true;
            }
            return false;
        }
            internal static Int32 ConvertCu8ToWav(string fileName)
        {
            Int32 sampleRate = 0;
            byte[] dataIQ;
            if (File.Exists(fileName))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
                {
                    dataIQ = new byte[reader.BaseStream.Length];
                    dataIQ = reader.ReadBytes((Int32)reader.BaseStream.Length);
                }
                RecordDevice(fileName, dataIQ,sampleRate, "", true);
            }
            return sampleRate;
        }
        //init by RTL_433_Plugin when start SDRSharp
        internal static Int32 MaxDevicesWindows { get; set; }
        internal static Int32 MaxDevicesWithGraph { get; set; }
        internal static Int32 MaxLinesConsole { get; set; }
//init by checkBox enabled plugin if change theme
        internal static Color BackColor { get; set; }
        internal static Color ForeColor { get; set; }
        internal static Font Font { get; set; }
        internal static Cursor Cursor { get; set; }
//init by checkBox enabled plugin and when change radioButton
        //internal static Boolean Wav { get; set; }
        //internal static Boolean Raw { get; set; }
    }
}
