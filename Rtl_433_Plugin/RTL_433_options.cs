using System;
using System.Windows.Forms;
namespace SDRSharp.Rtl_433
{
    public partial class Rtl_433_Panel : UserControl
    {
        private Int64 frequency=0;
        private Int32 MaxDevicesWindows = 0;

        internal void setMaxDevicesWindows(Int32 MaxDevicesWindows)
        {
            this.MaxDevicesWindows = MaxDevicesWindows;   //-1
        }

        private Int32 nbDevicesWithGraph = 0;

        internal void setNbDevicesWithGraph(Int32 nbDevicesWithGraph)
        {
            this.nbDevicesWithGraph = nbDevicesWithGraph + 1;
        }

        private Int32 maxLinesConsole = 0;

        internal void setMaxLinesConsole(Int32 maxLinesConsole)
        {
             this.maxLinesConsole = maxLinesConsole;
        }
        internal Int32 getNbDevicesWithGraph()
        {
            return nbDevicesWithGraph-1;
        }

        internal void setDataConv(Int32 statDataConv)
        {
            //groupBoxDataConv.Enabled = true;
            radioButtonDataConvSI.Enabled = true;
            radioButtonDataConvNative.Enabled = true;
            radioButtonDataConvCustomary.Enabled = true;
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
            radioButtonDataConvSI.Enabled = false;
            radioButtonDataConvNative.Enabled = false;
            radioButtonDataConvCustomary.Enabled = false;
            //groupBoxDataConv.Enabled = false;
        }

        internal Int32 getDataConv()
        {
            if (radioButtonDataConvNative.Checked)
            {
                if (ClassInterfaceWithRtl433 != null)
                    ClassInterfaceWithRtl433.setOption(radioButtonDataConvNative.Tag.ToString(), radioButtonDataConvNative.Text);
                return 0;
            }
            else if (radioButtonDataConvSI.Checked)
            {
                if (ClassInterfaceWithRtl433 != null)
                    ClassInterfaceWithRtl433.setOption(radioButtonDataConvSI.Tag.ToString(), radioButtonDataConvSI.Text);
                return 1;
            }
            else
            {
                if (ClassInterfaceWithRtl433 != null)
                    ClassInterfaceWithRtl433.setOption(radioButtonDataConvCustomary.Tag.ToString(), radioButtonDataConvCustomary.Text);
                return 2;
            }
        }

        internal Int32 getMetaData()
        {
            if (radioButtonMLevel.Checked)
            {
                if(ClassInterfaceWithRtl433!=null)
                     ClassInterfaceWithRtl433.setOption(radioButtonMLevel.Tag.ToString(), radioButtonMLevel.Text);
                return 1;
            }
            else
            {
                if (ClassInterfaceWithRtl433 != null)
                    ClassInterfaceWithRtl433.setOption(radioButtonNoM.Tag.ToString(), radioButtonNoM.Text);
                return 0;
             }
        }

        internal Int64 getFrequency()
        {
            if (radioButtonFreq315.Checked)
                return 315000000;
            else if (radioButtonFreq345.Checked)
                return 345000000;
            else if (radioButtonFreq43392.Checked)
                return 433920000;
            else if (radioButtonFreq868.Checked)
                return 868000000;
            else if (radioButtonFreq915.Checked)
                return 915000000;
            else //(radioButtonFreqFree.Checked)
                return 0;   
        }

        internal void setMetaData(Int32 statMetaData)
        {
            //groupBoxMetadata.Enabled = true;
            radioButtonNoM.Enabled = true;
            radioButtonMLevel.Enabled = true;
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
            radioButtonNoM.Enabled = false;
            radioButtonMLevel.Enabled = false;
            //groupBoxMetadata.Enabled = false;
        }
        //internal void setCenterFrequency(Int64 centerFrequency)
        //{
        //    labelCenterFrequency.Text = centerFrequency.ToString();
        //}
        internal void setFrequency(Int64 Frequency)
        {
            //groupBoxFrequency.Enabled = true;
            radioButtonFreqFree.Enabled = true;
            radioButtonFreq315.Enabled = true;
            radioButtonFreq345.Enabled = true;
            radioButtonFreq43392.Enabled = true;
            radioButtonFreq868.Enabled = true;
            radioButtonFreq915.Enabled = true;
            //if (radioButtonFreqFree.Checked != true)
            //{
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
                    default:
                        radioButtonFreq915.Checked = true;   //force event change
                        radioButtonFreqFree.Checked = true;
                    break;
                    //case 0:
                    //    radioButtonFreq915.Checked = true;   //force event change
                    //    radioButtonFreqFree.Checked = true;
                    //    break;
                    //default:
                    //    radioButtonFreqFree.Checked = true;   //force event change
                    //    radioButtonFreq43392.Checked = true;
                    //    break;
            }
            //}
            //else
            //{

            //}
            radioButtonFreqFree.Enabled = false; 
            radioButtonFreq315.Enabled = false;
            radioButtonFreq345.Enabled = false;
            radioButtonFreq43392.Enabled = false;
            radioButtonFreq868.Enabled = false;
            radioButtonFreq915.Enabled = false;
            labelFrequency.Text = Frequency.ToString();
            //groupBoxFrequency.Enabled = true;
        }

        //private void radioButtonOptionsRtl433_CheckedChanged(object sender, EventArgs e)
        //{
        //    RadioButton rDataConv = (RadioButton)sender;
        //    if (rDataConv.Checked && _ClassInterfaceWithRtl433!=null)
        //        _ClassInterfaceWithRtl433.setOption(rDataConv.Tag.ToString(), rDataConv.Text);
        //}

        private void getRadioButtonVerbose()
        {
        if(radioButtonNoV.Checked)
                ClassInterfaceWithRtl433.setOption(radioButtonNoV.Tag.ToString(), radioButtonNoV.Text);
         else if (radioButtonV.Checked)
                ClassInterfaceWithRtl433.setOption(radioButtonV.Tag.ToString(), radioButtonV.Text);
         else if (radioButtonVV.Checked)
                ClassInterfaceWithRtl433.setOption(radioButtonVV.Tag.ToString(), radioButtonVV.Text);
         else if (radioButtonVVV.Checked)
                ClassInterfaceWithRtl433.setOption(radioButtonVVV.Tag.ToString(), radioButtonVVV.Text);
         else if (radioButtonVVVV.Checked)
                ClassInterfaceWithRtl433.setOption(radioButtonVVVV.Tag.ToString(), radioButtonVVVV.Text);
        }

        private void getRadioButtonSaveCu8()
        {
        if(radioButtonSnone.Checked)
                ClassInterfaceWithRtl433.setOption(radioButtonSnone.Tag.ToString(), radioButtonSnone.Text);
         else if (radioButtonSknown.Checked)
                ClassInterfaceWithRtl433.setOption(radioButtonSknown.Tag.ToString(), radioButtonSknown.Text);
         else if (radioButtonSunknown.Checked)
                ClassInterfaceWithRtl433.setOption(radioButtonSunknown.Tag.ToString(), radioButtonSunknown.Text);
         else if (radioButtonSall.Checked)
                ClassInterfaceWithRtl433.setOption(radioButtonSall.Tag.ToString(), radioButtonSall.Text);
        }

        internal void setOptionVerboseInit()
        {
            //if (!radioButtonV.Checked)                  //to have the  devices list
            //    radioButtonV.Checked = true;
            //if (radioButtonNoV.Checked && _ClassInterfaceWithRtl433!=null)
            ClassInterfaceWithRtl433.setOption(radioButtonV.Tag.ToString(), radioButtonV.Text);
        }

        //private void radioButtonFreq_CheckedChanged(object sender, EventArgs e)
        //{
        //    RadioButton rbFreq = (RadioButton)sender;
        //    if (rbFreq.Checked)
        //    {
        //        if(_Rtl_433Processor!=null)
        //            _Rtl_433Processor.FrequencyRtl433 = System.Convert.ToInt64(rbFreq.Tag);
        //        frequency = System.Convert.ToInt64(rbFreq.Tag);   //_Rtl_433Processor.FrequencyRtl433;
        //        //_Rtl_433Processor.FrequencyRtl433 = System.Convert.ToInt64(rbFreq.Tag);
        //        //frequency = _Rtl_433Processor.FrequencyRtl433;
        //    }
        //}
    }
}
