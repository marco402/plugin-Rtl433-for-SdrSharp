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
using SDRSharp.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SDRSharp.Rtl_433
{
    internal class ClassFormDevices
    {
        private Dictionary<String, FormDevices> listformDevice;
        private List<String> listNamesToRecord;
        private List<String> listNamesAlreadyRecorded;
        internal void TreatFormDevices(Boolean sourceIsFile,List<PointF>[] points,String[] nameGraph, float[] dataIQForRecord, Int32 sampleRate, ISharpControl control, String frequencyStr,String deviceName, Dictionary<String, String> listData)
        {
            if (!listformDevice.ContainsKey(deviceName))
            {
                if (listformDevice.Count > ClassUtils.MaxDevicesWindows - 1)
                    return;
                listformDevice.Add(deviceName, new FormDevices(this));
                if (listformDevice.Count < ClassUtils.MaxDevicesWithGraph)
                    listformDevice[deviceName].DisplayGraph = true;
                listformDevice[deviceName].Text = deviceName;
                listformDevice[deviceName].ResetLabelRecord();  //after le load for memo...
                                                                //if (listformDevice.Count < _nbDevicesWithGraph
                listformDevice[deviceName].Show();
            }
            listformDevice[deviceName].SetInfoDevice(listData);
            if (listformDevice[deviceName].DisplayGraph)
                listformDevice[deviceName].SetDataGraph(points, nameGraph);
#if DEBUG
            Boolean recordAllOnce = true;
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
                //if ((!ClassUtils.Wav && !ClassUtils.Raw)) //|| labelSampleRate.Text != "250000"
                //{
                //    MessageBox.Show("Choice WAV or RAW", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return false;
                //}
                //else
                //{
                    if (choice)
                        listNamesToRecord.Add(name);
                    else
                        listNamesToRecord.Remove(name);
                    return true;
                //}
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
