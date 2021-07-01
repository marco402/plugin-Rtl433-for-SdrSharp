using System;
using System.Windows.Forms;

namespace SDRSharp.Rtl_433
{
    public partial class Rtl_433_Panel : UserControl
    {
        private long frequency=0;
        private void radioButtonFreq_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rbFreq = (RadioButton)sender;
            if (rbFreq.Checked)
            {
                _Rtl_433Processor.FrequencyRtl433 = System.Convert.ToInt64(rbFreq.Tag);
                frequency = _Rtl_433Processor.FrequencyRtl433;
            }
        }
        private int _MaxDevicesWindows = 0;
        public void MaxDevicesWindows(int MaxDevicesWindows)
        {
            _MaxDevicesWindows = MaxDevicesWindows;   //-1
        }
        private int _nbDevicesWithGraph = 0;
        public void nbDevicesWithGraph(int nbDevicesWithGraph)
        {
            _nbDevicesWithGraph = nbDevicesWithGraph + 1;
        }
        public void setDataConv(int statDataConv)
        {
            switch (statDataConv)
            {
                case 0:
                    radioButtonDataConvSI.Checked = true;   //force event change
                    radioButtonDataConvNative.Checked = true;
                    break;
                case 1:
                    radioButtonDataConvNative.Checked = true;   //force event change
                    radioButtonDataConvSI.Checked = true;
                     break;
                case 2:
                    radioButtonDataConvNative.Checked = true;   //force event change
                    radioButtonDataConvCustomary.Checked = true;
                    break;
                default:
                    radioButtonDataConvSI.Checked = true;   //force event change
                    radioButtonDataConvNative.Checked = true;
                    break;
            }
        }
        public int getDataConv()
        {
            if (radioButtonDataConvNative.Checked)
                return 0;
            else if (radioButtonDataConvSI.Checked)
                return 1;
            else
                return 2;
        }
       public int getMetaData()
        {
            if (radioButtonMLevel.Checked)
                return 1;
            else
                return 0;
        }

        public long getFrequency()
        {
            return frequency;
        }

        public void setMetaData(int statMetaData)
        {
            switch (statMetaData)
            {
                case 0:
                    radioButtonMLevel.Checked = true;   //force event change
                    radioButtonNoM.Checked = true;
                    break;
                case 1:
                    radioButtonNoM.Checked = true;  //force event change
                    radioButtonMLevel.Checked = true;
                    break;
                default:
                    radioButtonMLevel.Checked = true;  //force event change
                    radioButtonNoM.Checked = true;
                    break;
            }
        }

        public void setFrequency(long Frequency)
        {
            switch (Frequency)
            {

                case 315000000:
                    radioButtonFreqFree.Checked = true;   //force event change
                    radioButtonFreq315.Checked = true;
                    break;
                case 345000000:
                    radioButtonFreqFree.Checked = true;   //force event change
                    radioButtonFreq345.Checked = true;
                    break;
                case 433920000:
                    radioButtonFreqFree.Checked = true;   //force event change
                    radioButtonFreq43392.Checked = true;
                    break;
                case 868000000:
                    radioButtonFreqFree.Checked = true;   //force event change
                    radioButtonFreq868.Checked = true;
                    break;
                case 915000000:
                    radioButtonFreqFree.Checked = true;   //force event change
                    radioButtonFreq915.Checked = true;
                    break;
                case 0:
                    radioButtonFreq915.Checked = true;   //force event change
                    radioButtonFreqFree.Checked = true;
                    break;
                default:
                    radioButtonFreqFree.Checked = true;   //force event change
                    radioButtonFreq43392.Checked = true;
                    break;
            }
        }
        private void radioButtonOptionsRtl433_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rDataConv = (RadioButton)sender;
            if (rDataConv.Checked)
                _ClassInterfaceWithRtl433.setOption(rDataConv.Tag.ToString(), rDataConv.Text);
        }
    }
}
