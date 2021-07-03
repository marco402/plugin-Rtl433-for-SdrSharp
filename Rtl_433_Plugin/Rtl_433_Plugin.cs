/* Written by Marc Prieur (marco40_github@sfr.fr)
                                Rtl_433_Plugin.cs 
                            project Rtl_433_Plugin
						         Plugin for SdrSharp
 **************************************************************************************
 Creative Commons Attrib Share-Alike License
 You are free to use/extend this library but please abide with the CC-BY-SA license:
 Attribution-NonCommercial-ShareAlike 4.0 International License
 http://creativecommons.org/licenses/by-nc-sa/4.0/

 All text above must be included in any redistribution.
  **********************************************************************************/
//references:SDRSharp.exe for MainWindow,SDRSharp.Common and SDRSharp.Radio,GraphLib

using System;
using System.Windows.Forms;
using SDRSharp.Common;
using SDRSharp.Radio;

namespace SDRSharp.Rtl_433
{
    public class Rtl_433_Plugin: ISharpPlugin
    {
        private long frequency = 433920000;
        private int MaxDevicesWindows = 100;
        private int nbDevicesWithGraph = 5;
        private const string _displayName = "RTL_433";
        private Rtl_433_Processor _Rtl_433Processor;
        private Rtl_433_Panel _controlPanel;
        public string DisplayName  //ISharpPlugin
        {
            get { return _displayName; }
        }
        public UserControl Gui  //ISharpPlugin
        {
            get { return _controlPanel; }
        }
        public void Initialize(ISharpControl control)   //ISharpPlugin
        {
#if MSGBOXDEBUG
            MessageBox.Show( "Initialize RTL_433_plugin version 1.4.0.0" , "start plugin rtl433", MessageBoxButtons.OK, MessageBoxIcon.Information);
#endif
            _Rtl_433Processor = new Rtl_433_Processor(control);
            try
            {
                _controlPanel = new Rtl_433_Panel(_Rtl_433Processor);
                int statDataConv=Utils.GetIntSetting("RTL_433_plugin.DataConv",1);
                _controlPanel.setDataConv(statDataConv);
                int statMetaData = Utils.GetIntSetting("RTL_433_plugin.MetaData", 1);
                _controlPanel.setMetaData(statMetaData);
                MaxDevicesWindows = Utils.GetIntSetting("RTL_433_plugin.maxDevicesWindows", 100);
                nbDevicesWithGraph = Utils.GetIntSetting("RTL_433_plugin.nbDevicesWithGraph", 5);
                frequency = Utils.GetLongSetting("RTL_433_plugin.Frequency",433920000);
                _controlPanel.setFrequency(frequency);
                _controlPanel.MaxDevicesWindows(MaxDevicesWindows);
                _controlPanel.nbDevicesWithGraph(nbDevicesWithGraph);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message +  "   Initialize RTL_433_plugin", "Error Initialize", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Close()    //ISharpPlugin
        {
            if(_controlPanel is Rtl_433_Panel)
            {
                //_controlPanel.saveDevicesList();
                Utils.SaveSetting("RTL_433_plugin.DataConv", _controlPanel.getDataConv());
                Utils.SaveSetting("RTL_433_plugin.MetaData", _controlPanel.getMetaData());
                Utils.SaveSetting("RTL_433_plugin.Frequency", _controlPanel.getFrequency());
                Utils.SaveSetting("RTL_433_plugin.maxDevicesWindows", MaxDevicesWindows);
                Utils.SaveSetting("RTL_433_plugin.nbDevicesWithGraph", nbDevicesWithGraph);
            }
            if (_Rtl_433Processor is Rtl_433_Processor)
                _Rtl_433Processor.Stop();
        }
    }
}
