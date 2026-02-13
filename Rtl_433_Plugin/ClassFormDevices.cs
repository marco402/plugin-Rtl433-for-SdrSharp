/* Written by Marc Prieur (marco40_github@sfr.fr)
                                ClassFormDevices.cs 
                            project Rtl_433_Plugin
						         Plugin for SdrSharp
 **************************************************************************************
 Creative Commons Attrib Share-Alike License
 You are free to use/extend this library but please abide with the CC-BY-SA license:
 Attribution-NonCommercial-ShareAlike 4.0 International License
 http://creativecommons.org/licenses/by-nc-sa/4.0/

 All text above must be included in any redistribution.
  **********************************************************************************/
#define noTESTGRAPH
using SDRSharp.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
namespace SDRSharp.Rtl_433
{
    internal class ClassFormDevices
    {
        private Dictionary<String, FormDevices> listformDevice;
        private List<String> listNamesToRecord;
        private List<String> listNamesAlreadyRecorded;
        internal void TreatFormDevices(Boolean sourceIsFile,List<PointF>[] points,String[] nameGraph, float[] dataIQForRecord, Int32 sampleRate, ISharpControl control, String frequencyStr,String deviceName, Dictionary<String, String> listData)
        {
#if TESTGRAPH
            deviceName += ($"  N {listformDevice.Count}");
#endif
        if (!listformDevice.ContainsKey(deviceName))
        {
            if (listformDevice.Count >= ClassConst.NBMAXWindows) //plantage a 384 fenêtres graph pb createHandle limit système
                return;
            try
            {
            listformDevice.Add(deviceName, new FormDevices(this));
            listformDevice[deviceName].Text = deviceName;
            listformDevice[deviceName].ResetLabelRecord();  //after le load for memo...
            listformDevice[deviceName].Show();
            }
            catch (Win32Exception ex)
            {
                Debug.WriteLine($"...Win32Exception: {ex.Message}, Code={ex.NativeErrorCode}");
                throw;
            }
        }
        listformDevice[deviceName].SetInfoDevice(listData);
        listformDevice[deviceName].SetDataGraph(points, nameGraph);
#if DEBUG
            Boolean recordAllOnce = false;  //put true for record
#else
            Boolean recordAllOnce = false;
#endif
            if (!recordAllOnce)
                foreach (String deviceNameToRecord in listNamesToRecord)
                {
                    if (deviceNameToRecord == deviceName && dataIQForRecord!=null)
                    {
                        if (frequencyStr == "")
                            frequencyStr = control.Frequency.ToString();
                        ClassUtils.RecordDevice(deviceName, dataIQForRecord, sampleRate, frequencyStr + "hz");
                        listNamesToRecord.Remove(deviceName);
                        listformDevice[deviceName].ResetLabelRecord();
                        break;
                    }
                }
#if DEBUG
            else if (dataIQForRecord != null)
#else
            else if (!sourceIsFile && dataIQForRecord != null)
#endif
            {

                Boolean found = false;
                foreach (String deviceNameToRecord in listNamesAlreadyRecorded)
                {
                    if (deviceNameToRecord == deviceName)
                    { 
                        found = true;
                        break;
                    }
                }
#if DEBUG
                if (!found)
#else
                if (found)  
#endif
                { 
                    listNamesAlreadyRecorded.Add(deviceName);
                    if (frequencyStr == "")
                        frequencyStr = control.Frequency.ToString();
                    ClassUtils.RecordDevice(deviceName, dataIQForRecord,  sampleRate, frequencyStr + "hz");
                    listformDevice[deviceName].ResetLabelRecord();
                } 
            }
        }

        internal void ClosingOneFormDevice(String key)
        {
            if (listformDevice.ContainsKey(key))
                listformDevice.Remove(key);
        }
        internal void ResetLabelRecord(String deviceName)
        {
            listformDevice[deviceName].ResetLabelRecord();
        }

        internal Boolean SetRecordDevice(String name, Boolean choice)
        {
            if (choice)
            {
                if (choice)
                    listNamesToRecord.Add(name);
                else
                    listNamesToRecord.Remove(name);
                return true;
            }
            else
            {
                return true;
            }
        }
        internal void ClearListNamesToRecord()
        {
            if (listNamesToRecord == null)
                listNamesToRecord = new List<String>();
            if (listNamesAlreadyRecorded == null)
                listNamesAlreadyRecorded = new List<String>();
            listNamesToRecord.Clear();
            listNamesAlreadyRecorded.Clear();
        }
        internal void Start()
        {
            if (listformDevice == null)
                listformDevice = new Dictionary<String, FormDevices>();
        }

        public void Close()
        {
            listNamesToRecord = null;
            listNamesAlreadyRecorded = null;
            if (listformDevice != null)
            {
                foreach (KeyValuePair<String, FormDevices> _form in listformDevice)
                {
                    listformDevice[_form.Key].CloseByProgram();
                }
                listformDevice = null;
            }
        }
    }
}
