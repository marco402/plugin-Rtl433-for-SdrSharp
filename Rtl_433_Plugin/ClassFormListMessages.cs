/* Written by Marc Prieur (marco40_github@sfr.fr)
                                ClassFormListMessages.cs 
                            project Rtl_433_Plugin
						         Plugin for SdrSharp
 **************************************************************************************
 Creative Commons Attrib Share-Alike License
 You are free to use/extend this library but please abide with the CC-BY-SA license:
 Attribution-NonCommercial-ShareAlike 4.0 International License
 http://creativecommons.org/licenses/by-nc-sa/4.0/

 All text above must be included in any redistribution.
  **********************************************************************************/
using System;
using System.Collections.Generic;

namespace SDRSharp.Rtl_433
{
    internal class ClassFormListMessages   // : IDisposable
    {
        internal ClassFormListMessages()
        {
            listFormDeviceListMessages = new Dictionary<String, FormDevicesListMessages>();
        }
 
        private Dictionary<String, FormDevicesListMessages> listFormDeviceListMessages;
        internal void TreatformListMessages(String deviceName, Dictionary<String, String> listData)
        {
            if (!listFormDeviceListMessages.ContainsKey(deviceName))
            {
                if (listFormDeviceListMessages.Count > ClassUtils.MaxDevicesWindows - 1)
                    return;
                listFormDeviceListMessages.Add(deviceName, new FormDevicesListMessages(this, deviceName)); //+2 for debug
                listFormDeviceListMessages[deviceName].Text = deviceName;
                listFormDeviceListMessages[deviceName].Visible = true;
                listFormDeviceListMessages[deviceName].SetMessages(listData);
                listFormDeviceListMessages[deviceName].Show();
            }
            else
                listFormDeviceListMessages[deviceName].SetMessages(listData);
        }
        
        internal void ClosingOneFormDeviceListMessages(String key)
        {
            if (listFormDeviceListMessages.ContainsKey(key))
                listFormDeviceListMessages.Remove(key);
        }

        public void Close()
        {
            if (listFormDeviceListMessages != null)
            {
                foreach (KeyValuePair<String, FormDevicesListMessages> _form in listFormDeviceListMessages)
                {
                    listFormDeviceListMessages[_form.Key].CloseByProgram();
                }
                listFormDeviceListMessages = null;
            }
        }
    }
}
