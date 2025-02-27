/* Written by Marc Prieur (marco40_github@sfr.fr)
                           RTL_433_Panel_options.cs 
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
using System.Drawing;
using System.Windows.Forms;
namespace SDRSharp.Rtl_433
{
    public partial class Rtl_433_Panel : UserControl
    {
        private Int64 frequency = 0;
        internal void SetDataConv(Int32 statDataConv)
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
        internal Int32 GetDataConv()
        {
            if (radioButtonDataConvNative.Checked)
            {
                if (ClassInterfaceWithRtl433 != null)
                    ClassInterfaceWithRtl433.SetOption(radioButtonDataConvNative.Tag.ToString(), radioButtonDataConvNative.Text);
                return 0;
            }
            else if (radioButtonDataConvSI.Checked)
            {
                if (ClassInterfaceWithRtl433 != null)
                    ClassInterfaceWithRtl433.SetOption(radioButtonDataConvSI.Tag.ToString(), radioButtonDataConvSI.Text);
                return 1;
            }
            else
            {
                if (ClassInterfaceWithRtl433 != null)
                    ClassInterfaceWithRtl433.SetOption(radioButtonDataConvCustomary.Tag.ToString(), radioButtonDataConvCustomary.Text);
                return 2;
            }
        }

        internal Int64 GetFrequency()
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

        //internal void setCenterFrequency(Int64 centerFrequency)
        //{
        //    labelCenterFrequency.Text = centerFrequency.ToString();
        //}
        internal void SetFrequency(Int64 Frequency)
        {
            //groupBoxFrequency.Enabled = true;
            radioButtonFreqFree.Enabled = true;
            radioButtonFreq315.Enabled = true;
            radioButtonFreq345.Enabled = true;
            radioButtonFreq43392.Enabled = true;
            radioButtonFreq868.Enabled = true;
            radioButtonFreq915.Enabled = true;
            //if (!radioButtonFreqFree.Checked)
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
            //1.5.8.0 comment this 6 lines disabled if start radio with plugin enabled
            //radioButtonFreqFree.Enabled = false;
            //radioButtonFreq315.Enabled = false;
            //radioButtonFreq345.Enabled = false;
            //radioButtonFreq43392.Enabled = false;
            //radioButtonFreq868.Enabled = false;
            //radioButtonFreq915.Enabled = false;
            labelFrequency.Text = Frequency.ToString();
            if (Frequency < 300000000 || Frequency > 1000000000)
                labelFrequency.ForeColor = Color.Orange;
            else
                labelFrequency.ForeColor = labelFrequencyTxt.ForeColor;
        }

        private void GetRadioButtonSaveCu8()
        {
            if (radioButtonSnone.Checked)
                ClassInterfaceWithRtl433.SetOption(radioButtonSnone.Tag.ToString(), radioButtonSnone.Text);
            else if (radioButtonSknown.Checked)
                ClassInterfaceWithRtl433.SetOption(radioButtonSknown.Tag.ToString(), radioButtonSknown.Text);
            else if (radioButtonSunknown.Checked)
                ClassInterfaceWithRtl433.SetOption(radioButtonSunknown.Tag.ToString(), radioButtonSunknown.Text);
            else if (radioButtonSall.Checked)
                ClassInterfaceWithRtl433.SetOption(radioButtonSall.Tag.ToString(), radioButtonSall.Text);
        }
        internal void SetOptionVerboseInit()
        {
            ClassInterfaceWithRtl433.SetOption("verbose", "-v", true);
        }
    }
}
