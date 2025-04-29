/* Written by Marc Prieur (marco40_github@sfr.fr)
                               ClassDecimation.cs 
                           project Rtl_433_Plugin
                                Plugin for SdrSharp
**************************************************************************************
Creative Commons Attrib Share-Alike License
You are free to use/extend this library but please abide with the CC-BY-SA license:
Attribution-NonCommercial-ShareAlike 4.0 International License
http://creativecommons.org/licenses/by-nc-sa/4.0/

All text above must be included in any redistribution.
 ************************************************************************************/

using SDRSharp.Radio;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDRSharp.Rtl_433
{
    public static class ClassDecimation
    {
        internal static byte[] Decimate(byte[] arrayIn, Int32 decimation)
        {
            if (decimation < 2)
                return arrayIn;

            if (arrayIn.Length < decimation)
                return arrayIn;

            Int32 lenReal = arrayIn.Length - (arrayIn.Length % 2);
            Int32 size = (lenReal / decimation) - (lenReal / decimation) % 2;
            byte[] arrayOut = new byte[size];
            for (Int32 sample = 0; sample < size * decimation; sample += (decimation * 2))
            {
                Int32 temp = 0;
                Int32 temp1 = 0;
                for (Int32 h = 0; h < decimation; h++)
                {
                    temp += arrayIn[sample + h * 2];   //2 = 1 byte I 
                    temp1 += arrayIn[1 + sample + h * 2];  //+ 1 byte Q
                }
                arrayOut[sample / decimation] = (byte)(temp / decimation);
                arrayOut[1 + (sample / decimation)] = (byte)(temp1 / decimation);
            }
            return arrayOut;
        }
        internal unsafe static Complex* DecimateMoy(Complex* array, Int32 decimation, Int32 lenIn, ref Int32 lenOut)
        {
            if (decimation < 2 ||lenIn < decimation)
            {
                lenOut = lenIn;
                return array;
            }
            Int32 lenReal = lenIn - (lenIn % 2);
            lenOut = (lenReal / decimation) - (lenReal / decimation) % 2;
            Int32 sample = 0;
            Complex temp=new Complex(0,0) ;
            for (sample = 0; sample < lenOut * decimation; sample += decimation)
            {
                for (Int32 h = 0; h < decimation; h++)
                {
                    temp.Real += (float)array[sample + h].Real;
                    temp.Imag += (float)array[sample + h].Imag;
                }
                array[sample / decimation].Real = (float)(temp.Real / decimation);
                array[sample / decimation].Imag = (float)(temp.Imag / decimation);
            }
            return array;
        }
        internal unsafe static Complex* DecimateMax(Complex* array, Int32 decimation, Int32 lenIn, ref Int32 lenOut)
        {
            if (decimation < 2 || lenIn < decimation)
            {
                lenOut = lenIn;
                return array;
            }
            Int32 lenReal = lenIn - (lenIn % 2);
            lenOut = (lenReal / decimation) - (lenReal / decimation) % 2;
             Int32 sample = 0;
            Complex temp = new Complex(0, 0);
            Complex result = new Complex(0, 0);
            for (sample = 0; sample < lenOut * decimation; sample += decimation)
            {
                temp.Real = 0;
                temp.Imag = 0;
                for (Int32 h = 0; h < decimation; h++)
                {
                    if (Math.Abs(array[sample + h].Real)> Math.Abs(temp.Real))
                    {
                        temp.Real= Math.Abs(array[sample + h].Real);
                        result.Real= array[sample + h].Real;
                    }
                    if (Math.Abs(array[sample + h].Imag) > Math.Abs(temp.Imag))
                    {
                        temp.Imag = Math.Abs(array[sample + h].Imag);
                        result.Imag = array[sample + h].Imag;
                    }
                }
                array[sample / decimation].Real = result.Real;
                array[sample / decimation].Imag = result.Imag;
             }
            return array;
        }
        internal static short[] Decimate(short[] arrayIn, Int32 decimation)
        {
            if (decimation < 2)
                return arrayIn;
            if (arrayIn.Length < decimation)
                return arrayIn;

            Int32 lenReal = (arrayIn.Length / decimation) * decimation;  //modulo
            Int32 size = (lenReal / decimation) - 1 - (lenReal / decimation) % 2;
            short[] arrayOut = new short[size]; ;

            for (Int32 sample = 0; sample < size * decimation; sample += decimation)
            {
                Int32 temp = 0;
                for (Int32 h = 0; h < decimation; h++)
                {
                    temp += arrayIn[sample + h];    //2=sizeof(Int16)
                }
                arrayOut[sample / decimation] = (short)(temp / decimation);
            }
            return arrayOut;
        }
     }
}
