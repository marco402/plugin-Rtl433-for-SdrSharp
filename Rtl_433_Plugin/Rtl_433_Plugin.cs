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
//I transferred a specific version for versions 1777 and 1784 with reference SDRSharp 1777.
//for information, these 2 versions look for SDRSharpMerged and not SDRSharp.
//the executable is SDRSharp.exe but they changed an identity property: SDRSharpMerged instead of SDRSharp.
//check the csproj on change of reference, it's not clear, I had to leave visual studio to have the right one in the csproj
//version sdrsharp-->https://www.iz3mez.it/software/SDRSharp/
//test file .cu8-->https://github.com/merbanan/rtl_433_tests/tree/master/tests
//help rtl_433-->https://triq.org/

//dotnet versions 
//   last 4.0-->1784
//   last 5.0-->1831
//   >    6.0

//devices:
//TFA-TwinPlus sign when temperature <0.
//alectro température /10 ?

using System;
using System.Windows.Forms;
using SDRSharp.Common;
using SDRSharp.Radio;
namespace SDRSharp.Rtl_433
{
    public class Rtl_433_Plugin: ISharpPlugin
    {
        private Rtl_433_Panel controlPanel;
 
        public void Initialize(ISharpControl control)   // ISharpPlugin  call by SDRSharp.MainForm
        {
            try
            {
                controlPanel = new Rtl_433_Panel(control);
                //****WARNING****if add control, add to checkBoxEnabledPlugin_CheckedChanged to panel
                controlPanel.setDataConv(Utils.GetIntSetting("RTL_433_plugin.DataConv", 1));
                controlPanel.setMetaData(Utils.GetIntSetting("RTL_433_plugin.MetaData", 1));
                controlPanel.setFrequency(Utils.GetLongSetting("RTL_433_plugin.Frequency", 433920000));
                controlPanel.setMaxDevicesWindows(Utils.GetIntSetting("RTL_433_plugin.maxDevicesWindows", 100));
                controlPanel.setNbDevicesWithGraph(Utils.GetIntSetting("RTL_433_plugin.nbDevicesWithGraph", 5));
                controlPanel.setMaxLinesConsole(Utils.GetIntSetting("RTL_433_plugin.maxLinesConsole", 1000));
               //controlPanel.Dock = DockStyle.Left; sans effet //egal version 1920 no docking-->il faut:prefered docking position. where ?
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message +  "   Initialize RTL_433_plugin", "Error Initialize", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public UserControl Gui  //ISharpPlugin  call by SDRSharp.MainForm
        {
            get { return controlPanel; }
        }

        public String DisplayName  //ISharpPlugin    call by SDRSharp.MainForm
        {
            get { return "RTL_433  "+ controlPanel.VERSION; }
        }

        public void Close()    //ISharpPlugin  call by SDRSharp.MainForm
        {
            //Console.WriteLine("Close plugin");
            if(controlPanel is Rtl_433_Panel)
            {
                Utils.SaveSetting("RTL_433_plugin.DataConv", controlPanel.getDataConv());
                Utils.SaveSetting("RTL_433_plugin.MetaData", controlPanel.getMetaData());
                Utils.SaveSetting("RTL_433_plugin.Frequency", controlPanel.getFrequency());
                Utils.SaveSetting("RTL_433_plugin.maxDevicesWindows", Utils.GetIntSetting("RTL_433_plugin.maxDevicesWindows", 100));
                Utils.SaveSetting("RTL_433_plugin.nbDevicesWithGraph", Utils.GetIntSetting("RTL_433_plugin.nbDevicesWithGraph", 5));
                Utils.SaveSetting("RTL_433_plugin.maxLinesConsole", Utils.GetIntSetting("RTL_433_plugin.maxLinesConsole", 1000));
                controlPanel.Stop(true);
            }
        }
    }
}
