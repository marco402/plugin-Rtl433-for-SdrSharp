/* Written by Marc Prieur (marco40_github@sfr.fr)
                                ClassFormDevicesList.cs 
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
using System.IO;
using System.Windows.Forms;

namespace SDRSharp.Rtl_433
{
    internal class ClassFormDevicesList
    {
        private readonly Rtl_433_Panel classPanel;
        private FormListDevices formListDevice = null;
        internal ClassFormDevicesList(Rtl_433_Panel classPanel)
        {
            this.classPanel = classPanel;
            ChooseFormListDevice = true;
            String fileWithPath = GetDirectoryRecordingForOpenDevicesList();
            formListDevice = new FormListDevices(this);
            if (File.Exists(fileWithPath))
                if (MessageBox.Show(LanguageManager.GetString("ClassFormDevicesList_import_devices_list"), LanguageManager.GetString("ClassFormDevicesList_Import_devices_list_1"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    formListDevice.DeSerializeText(fileWithPath);
            formListDevice.Show();
        }
        internal Boolean  SetInfoDevice(Dictionary<String, String> listData)
        {
            if (formListDevice != null)
                return formListDevice.SetInfoDeviceListDevices(listData);
            else
                return false;
        }
 
        public Boolean PluginIsRun
        {
            get
            {
                 return classPanel.PluginIsRun;
            }

        }

        internal Boolean SetFormDevicesListCloseByUser
        {
            set
            {
                formListDevice = null;
                ClosingFormListDevice();
            }
        }
        internal void ClosingFormListDevice()
        {
            classPanel.ClosingFormListDevice();
        }
        private Boolean alreadyTested = false;
        internal String GetDirectoryForSaveDevicesList()
        {
            String directory = ClassConst.FOLDERRECORD;   //folder Recording to folder SDRSHARP.exe
            if (!Directory.Exists(directory))
            {
                try
                {
                    Directory.CreateDirectory(directory);
                }
                catch (Exception e)
                {
                    if (!alreadyTested)
                    {
                        MessageBox.Show(e.Message + ClassConst.CrLf + LanguageManager.GetString("ClassFormDevicesList_The_file_") + ClassConst.FILELISTEDEVICES + LanguageManager.GetString("ClassFormDevicesLists_to_SDRSharp_folder"), LanguageManager.GetString("ClassFormDevicesLists_to_folder_SDRSharp"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        directory = "";  //to folder SDRSHARP.exe
                        alreadyTested = true;
                    }
                }
            }
            return directory + ClassConst.FILELISTEDEVICES;
        }
        private String GetDirectoryRecordingForOpenDevicesList()
        {
            String directory = ClassConst.FOLDERRECORD;   //FOLDERRECORD to folder SDRSHARP.exe
            if (!Directory.Exists(directory))
                directory = "." + ClassConst.FOLDERRECORD;  //folder SDRSHARP.exe
            return directory + ClassConst.FILELISTEDEVICES;
        }
        public void Close()
        {
             if (formListDevice != null)
            {
                formListDevice.Close();
                formListDevice = null;
            }
        }

        public Boolean ChooseFormListDevice { get; set; }
    }
}
