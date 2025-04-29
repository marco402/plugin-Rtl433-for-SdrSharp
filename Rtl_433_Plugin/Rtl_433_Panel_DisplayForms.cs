/* Written by Marc Prieur (marco40_github@sfr.fr)
                           Rtl_433_Panel_DisplayForms.cs 
                            project Rtl_433_Plugin
						         Plugin for SdrSharp
 **************************************************************************************
 Creative Commons Attrib Share-Alike License
 You are free to use/extend this library but please abide with the CC-BY-SA license:
 Attribution-NonCommercial-ShareAlike 4.0 International License
 http://creativecommons.org/licenses/by-nc-sa/4.0/

 All text above must be included in any redistribution.
  **********************************************************************************/
#define noTESTWINDOWS    //for test memory

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace SDRSharp.Rtl_433
{
    public partial class Rtl_433_Panel  //no internal for this partial  vs2017?
    {
        private ClassFormListMessages myClassFormListMessages = null;
        private ClassFormDevicesList myClassFormDevicesList = null;
        private ClassFormDevices myClassFormDevices = null; 
        private Boolean shownFormDeviceList = false;    //wait end of deSerializeText formListDevices
        private Boolean selectFormGraph;

        internal Boolean SetSelectFormGraph
        {
            set
            {
                selectFormGraph = value;
            }
        }
        //private Boolean recordText;
        //internal Boolean SetRecordText
        //{
        //    set { recordText = value; }
        //}

#if TESTBOUCLEREPLAYMARC
        private String memoFileOK = "";

        internal void controlStopRadio()
        {
        control.StopRadio();
        }
#endif
#if TESTBOUCLEREPLAYMARC
        String lastName = ""; 
 #endif

        internal void TreatForms(Dictionary<String, String> listData, List<PointF>[] points = null,String[] nameGraph = null, float[] dataIQForRecord = null, Int32 sampleRate = 0, string frequencyStr = "")
        {
            if (InvokeRequired)
            {
                BeginInvoke((Action)delegate
                {
                    TreatForms(listData, points, nameGraph, dataIQForRecord, sampleRate, frequencyStr);
                });
            }
            else
            {
                bool analyze = false;
#if ANALYZE
                analyze = true;
#endif
                if (ClassInterfaceWithRtl433 == null)
                    return;
                stopwDisplay.Restart();
                String deviceName = ClassUtils.GetDeviceName(listData);

            if (deviceName.Trim() != "")
            {
#if TESTWINDOWS
                    cptDevicesForTest ++;                //test device windows
#endif
                if (selectFormGraph && !analyze)
                {
                    if (myClassFormDevices == null)
                    {
                        myClassFormDevices = new ClassFormDevices();
                        myClassFormDevices.ClearListNamesToRecord();
                        myClassFormDevices.Start();
                    }
                    myClassFormDevices.TreatFormDevices(sourceIsFile, points, nameGraph,  dataIQForRecord,  sampleRate, control, frequencyStr, deviceName, listData);
                }
                else if (selectFormListDevice || analyze)
                {
                            if (myClassFormDevicesList == null && !shownFormDeviceList)
                            {
                                shownFormDeviceList = true;

                                myClassFormDevicesList = new ClassFormDevicesList(this);
                            }
                            if (shownFormDeviceList && myClassFormDevicesList != null)
                            { 
                                Boolean ret=myClassFormDevicesList.SetInfoDevice(listData);
                            }
                }
                else  //TYPEFORM.LISTMES
                {
                        if (myClassFormListMessages == null)
                            myClassFormListMessages = new ClassFormListMessages();
                        myClassFormListMessages.TreatformListMessages(deviceName,listData);
                    }
                }
                stopwDisplay.Stop();
                TimeDisplay = stopwDisplay.ElapsedMilliseconds;
            }
        }
#region formListMessages
#endregion

#region formDevicesList
        private Boolean selectFormListDevice;
        private Boolean SetSelectFormListDevice
        {
            set
            {
                selectFormListDevice = value;
                //if (myClassFormDevicesList != null)
                //    myClassFormDevicesList.ChooseformDevicesList = value;
            }
        }
        internal void ClosingFormListDevice()
        {
            if (myClassFormDevicesList != null)
            {
                shownFormDeviceList = false;
                myClassFormDevicesList.Close();
                myClassFormDevicesList = null;
            }
        }
#endregion
    }
}
