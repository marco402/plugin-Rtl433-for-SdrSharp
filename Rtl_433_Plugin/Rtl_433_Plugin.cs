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
using System;
using System.Windows.Forms;
using SDRSharp.Common;
using SDRSharp.Radio;
namespace SDRSharp.Rtl_433
{
    public class Rtl_433_Plugin: ISharpPlugin, IDisposable
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
            //MessageBox.Show( "Initialize RTL_433_plugin version 1.4.0.0" , "start plugin rtl433", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        #region IDisposable Support
        private bool disposedValue = false; // Pour détecter les appels redondants

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _Rtl_433Processor.Dispose();
                    _Rtl_433Processor = null;
                    _controlPanel.Dispose();
                    // TODO: supprimer l'état managé (objets managés).
                }

                // TODO: libérer les ressources non managées (objets non managés) et remplacer un finaliseur ci-dessous.
                // TODO: définir les champs de grande taille avec la valeur Null.

                disposedValue = true;
            }
        }

        // TODO: remplacer un finaliseur seulement si la fonction Dispose(bool disposing) ci-dessus a du code pour libérer les ressources non managées.
        // ~Rtl_433_Plugin() {
        //   // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
        //   Dispose(false);
        // }

        // Ce code est ajouté pour implémenter correctement le modèle supprimable.
        public void Dispose()
        {
            // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
            Dispose(true);
            // TODO: supprimer les marques de commentaire pour la ligne suivante si le finaliseur est remplacé ci-dessus.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
