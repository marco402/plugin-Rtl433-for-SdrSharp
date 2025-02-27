/* Written by Marc Prieur (marco40_github@sfr.fr)
                       ClassInterfaceWithRtl433_OptionsRtl433.cs 
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDRSharp.Rtl_433
{
    internal partial class ClassInterfaceWithRtl433
    {
        #region options rtl_433

        private void InitOptionsRTL433()
        {
            listOptionsRtl433 = new Dictionary<String, String>();
            SetOption("verbose", "-v");                    //setVerbose("-v");  //for list devices details
                                                           // _owner.setMessage(Application.ProductVersion);  version sdrSharp
                                                           //call_main_Rtl_433(false);
                                                           //setOptionUniqueKey("-a 4", true);         //for graph !???
            SetOptionUniqueKey("-MProtocol", true);  //for title and key devices windows

        }

        private String[] GetArg()
        {
            String[] args = new String[listOptionsRtl433.Count() + 1];  //   +1 for .exe

            args[0] = "Rtl_433.exe"; // + @"\";
            Int32 counter = 1;
            panelRtl_433.SetMessage("-----------RTL433 OPTIONS --------------\n");
            foreach (KeyValuePair<String, String> _option in listOptionsRtl433)
            {
                panelRtl_433.SetMessage(_option.Value + "\n");
                args[counter] = _option.Value;
                counter++;
            }
            panelRtl_433.SetMessage("------------------------------------------\n");
            return args;

        }

        internal void SetOptionUniqueKey(String key, Boolean state)
        {
            if (listOptionsRtl433.ContainsKey(key))
                listOptionsRtl433.Remove(key);
            if (state)
                listOptionsRtl433.Add(key, key);
        }
        internal void SetHideOrShowDevices(List<String> listBoxSelectedDevices, Boolean hideShowDevices)
        {
            //if (listformDevice == null)
            //    listformDevice = new Dictionary<String, FormDevices>();
            //if (listformDeviceListMessages == null)
            //    listformDeviceListMessages = new Dictionary<String, FormDevicesListMessages>();

            Dictionary<String, String> copyListOptionsRtl433 = new Dictionary<String, String>();
            foreach (KeyValuePair<String, String> _option in listOptionsRtl433)
            {
                if (!(_option.Key.Contains("hide") || _option.Key.Contains("show")))
                    //keep all key not egal hide or show
                    copyListOptionsRtl433.Add(_option.Key, _option.Value);    //keep all key not egal hide or show
            }
            //-R device<protocol> show protocol
            //-R device<-1> hide protocol
            //without -R show all protocol

            listOptionsRtl433.Clear();

            foreach (KeyValuePair<String, String> _option in copyListOptionsRtl433)
            {
                listOptionsRtl433.Add(_option.Key, _option.Value);
            }
            copyListOptionsRtl433.Clear();
            foreach (String device in listBoxSelectedDevices)
            {
                if (hideShowDevices)
                {
                    listOptionsRtl433.Add("hide" + device, "-R -" + device.Trim()); //hide protocol
                }
                else
                {
                    listOptionsRtl433.Add("show" + device, "-R " + device.Trim()); //show protocol
                }
            }
            foreach (KeyValuePair<String, String> _option in copyListOptionsRtl433)
            {
                listOptionsRtl433.Add(_option.Key, _option.Value);
            }
            copyListOptionsRtl433.Clear();
        }
        internal void SetShowDevicesDisabled(Boolean showDevicesDisabled)
        {
            if (showDevicesDisabled)
                EnabledDevicesDisabled = 1;
            else
                EnabledDevicesDisabled = 0;

            if (memo_EnabledListDevices != EnabledDevicesDisabled)
            {
                initListDevice = false;     //need reload list devices
            }
            memo_EnabledListDevices = EnabledDevicesDisabled;
        }
        internal void SetOption(String Key, String value, Boolean init = false)
        {
            if (init && listOptionsRtl433.ContainsKey(Key))
            {
                if (listOptionsRtl433[Key].Contains("No ")) //if >=  1 v ok
                {
                    listOptionsRtl433.Remove(Key);
                    listOptionsRtl433.Add(Key, "-v");   //if init min one v for receive list devices
                }
            }
            else
            {
                if (listOptionsRtl433.ContainsKey(Key))
                    listOptionsRtl433.Remove(Key);
                if (!value.Contains("No "))
                    listOptionsRtl433.Add(Key, value);
            }
        }
        #endregion


    }
}
