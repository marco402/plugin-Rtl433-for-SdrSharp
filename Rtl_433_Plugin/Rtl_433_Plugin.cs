
//references:SDRSharp.exe for MainWindow,SDRSharp.Common and SDRSharp.Radio,GraphLib

using System;
using System.Windows.Forms;
using SDRSharp.Common;
using SDRSharp.Radio;

namespace SDRSharp.Rtl_433
{
    public class Rtl_433_Plugin: ISharpPlugin
    {
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
            _Rtl_433Processor = new Rtl_433_Processor(control);
            try
            {
                _controlPanel = new Rtl_433_Panel(_Rtl_433Processor);
                _controlPanel.statDataConvNative = Utils.GetBooleanSetting("DataConvNative");
                _controlPanel.statDataConvSI = Utils.GetBooleanSetting("DataConvSI");
                _controlPanel.statDataConvCustomary = Utils.GetBooleanSetting("DataConvNativeCustomary");
            }
            catch (Exception)
            {
            }
        }
        public void Close()    //ISharpPlugin
        {
            Utils.SaveSetting("DataConvNative", _controlPanel.statDataConvNative);
            Utils.SaveSetting("DataConvSI", _controlPanel.statDataConvSI);
            Utils.SaveSetting("DataConvNativeCustomary", _controlPanel.statDataConvCustomary);
            if (_Rtl_433Processor is Rtl_433_Processor)
                _Rtl_433Processor.Stop();
        }
    }
}
