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
//compilation options generales
//debug
//   noTESTDECIMATION
//release
//    


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

using SDRSharp.Common;
using SDRSharp.Radio;
using System;
using System.Diagnostics;
using System.Windows.Forms;
namespace SDRSharp.Rtl_433
{
    public class Rtl_433_Plugin : ISharpPlugin, IDisposable
    {
        private Rtl_433_Panel controlPanel;
        ~Rtl_433_Plugin()
        {
            this.Dispose(false);
        }
        #region ISharpPlugin
        //[method: CLSCompliant(false)]
        public void Initialize(ISharpControl control)   // ISharpPlugin  call by SDRSharp.MainForm
        {
            try
            {
                controlPanel = new Rtl_433_Panel(control);
                //****WARNING****if add control, add to checkBoxEnabledPlugin_CheckedChanged to panel
                controlPanel.SetDataConv(Utils.GetIntSetting("RTL_433_plugin.DataConv", 1));
                controlPanel.SetFrequency(Utils.GetLongSetting("RTL_433_plugin.Frequency", 433920000));
                ClassUtils.MaxDevicesWindows=Utils.GetIntSetting("RTL_433_plugin.maxDevicesWindows", 100);
                ClassUtils.MaxDevicesWithGraph=Utils.GetIntSetting("RTL_433_plugin.nbDevicesWithGraph", 100);
                if (ClassUtils.MaxDevicesWithGraph < 100)
                    ClassUtils.MaxDevicesWithGraph = 100;
                ClassUtils.MaxLinesConsole=Utils.GetIntSetting("RTL_433_plugin.maxLinesConsole", 3000);
                //controlPanel.Dock = DockStyle.Left; sans effet //egal version 1920 no docking-->il faut:prefered docking position. where ?
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "   Initialize RTL_433_plugin", "Error Initialize", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public UserControl Gui  //ISharpPlugin  call by SDRSharp.MainForm
        {
            get { return controlPanel; }
        }
        public string DisplayName => "RTL_433";
 
        public void Close()    //ISharpPlugin  call by SDRSharp.MainForm
        {
            //Console.WriteLine("Close plugin");
            if (controlPanel is Rtl_433_Panel)
            {
                Utils.SaveSetting("RTL_433_plugin.DataConv", controlPanel.GetDataConv());
                Utils.SaveSetting("RTL_433_plugin.Frequency", controlPanel.GetFrequency());
                Utils.SaveSetting("RTL_433_plugin.maxDevicesWindows", ClassUtils.MaxDevicesWindows);     //fixe
                Utils.SaveSetting("RTL_433_plugin.nbDevicesWithGraph", ClassUtils.MaxDevicesWithGraph);  //fixe
                Utils.SaveSetting("RTL_433_plugin.maxLinesConsole", ClassUtils.MaxLinesConsole);         //fixe
                controlPanel.Stop(true);
            }
        }
        #endregion
        #region IDisposable
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    controlPanel.Dispose();
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
