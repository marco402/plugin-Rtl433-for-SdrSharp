/* Written by Marc Prieur (marco40_github@sfr.fr)
                                ClassGraphes.cs 
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
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;

namespace SDRSharp.Rtl_433
{
    internal class ClassTraceGraphe
    {
        private Rtl_433_Panel panelRtl_433;
        private  List<PointF>[] TreatOokOrFskCurves(NativeMethods.Pulse_data pulseData, short[] am_buf, short[] fm_buf, List<PointF>[] points, Int32 NumGraph, bool FSK, double sampleRate, byte[] dataIQDisplay, UInt32 lenToDisplay, Int32 startToDisplay)
        {
            double samples_per_us = 1000000.0 / sampleRate;
            float x = 0;      //current point define last point for am & fm (µs)  
            double lenToDisplayUs = lenToDisplay * samples_per_us;
            Int32 deltaStart= 0;
            if (pulseData.pulse[0] > 1000 || pulseData.gap[0] > 1000)   //test with froggitwh1080Pass14c01gfile001_433000000hz_250k_18_02_2025_14_25_20.wav
                deltaStart++;
            Int32 deltaEnd = 0;
            if (pulseData.gap[pulseData.num_pulses-1] > 1000)
                deltaEnd++;
            for (Int32 bit = deltaStart; bit < (pulseData.num_pulses- deltaEnd); bit++)   //-1 sometime last pulse nok--->ambient_weather_04_g001_915M_250k.cu8
            {
                x += (float)(pulseData.pulse[bit] * samples_per_us);
                points[0].Add(new PointF(x, 1));
                points[0].Add(new PointF(x, 0));
                x += (float)(pulseData.gap[bit] * samples_per_us);
                points[0].Add(new PointF(x, 0));
                points[0].Add(new PointF(x, 1));
                if (x > lenToDisplayUs)
                    break;
            }
            float xx = 0;      //current position (µs)  1° point >memoX last point < x
            if (NumGraph > 1)
                if (!FSK)
                {
                    for (Int32 bit = deltaStart; bit < am_buf.Length; bit++)
                    {
                        xx = (float)(bit * samples_per_us);
                        if (xx > x)  //test with schreader
                            break;
                        points[1].Add(new PointF(xx, (Int32)(am_buf[bit])));
                     }
                }
            for (Int32 bit = deltaStart; bit < am_buf.Length; bit++)
            {
                xx = (float)(bit * samples_per_us);
                if (xx > x)  //test with schreader
                    break;
                if (FSK)
                    points[1].Add(new PointF(xx, (Int32)(fm_buf[bit])));
                ////else
                ////    points[2].Add(new PointF(xx, (Int32)(fm_buf[bit])));
            }
            for (Int32 bit = deltaStart; bit < dataIQDisplay.Length; bit+=2)
            {
                xx = (float)(bit * samples_per_us/2);
                    if (xx > x)  //test with schreader
                        break;
                    ////if (FSK)
                        points[2].Add(new PointF(xx, (Int32)(dataIQDisplay[bit])));
                    ////else
                    ////     points[3].Add(new PointF(xx, (Int32)(dataIQDisplay[bit])));
            }
            return points;
        }
        private unsafe float[] GetTabIQ(Int32 start, Int32 len,Int32 sizeBufferIQ, float[] ptrTabIQ,Int32 ptrMemoDataForRs433)
        {
            float[] retourIQ=new float[len];
            ///*****************************************************************************************************************************************************************************
            ///2 buffers: le buffer tournant struct_demod_samp_grab et le buffer courant pointé par info.startPulses
            ///struct_demod_samp_grab.sg_index pointe sur l'emplacement libre pour le prochain buffer courant
            ///-si info.startPulses est négatif, les données commencent dans un buffer courant précédent.
            ///3 cas de figure:
            ///   1-les données utiles (startUsefullData à endUsefullData->lenUsefullData sont toutes dans le buffer courant  et ne debordent pas sur la fin du buffer tournant.
            ///   2-le debut des données (startUsefullData) est < 0 -->le début du buffer courant est à la fin du buffer tournant et la fin du buffer courant est au début du buffer tournant
            ///   3-la fin des données (endUsefullData) est > struct_demod_samp_grab.sg_size --> idem cas 2
            ///   4-l'index est au début du buffer startUsefullData et endUsefullData sont < 0 ne doit pas arriver
            ///
            /// le buffer courant est normalement de 131072(NBBYTEFORRTL_433) pour un sample rate de 250000->131072*4 = 524 288 microSecondes
            /// le buffer tournant est de (struct_demod_samp_grab.sg_size) pour un sample rate de 250000->3145728*4 = 12 582 912 microSecondes
            /// nota:avec un buffer tournant de 3145728 et un transfert de 131072, on ne doit pas avoir de chavauchement en in de buffer:3145728/131072=24
            ///***************************************************************************************************************************************************************************** 
            Int32 startUsefullData = (ptrMemoDataForRs433 - Rtl_433_Processor.nbByteForRtl433AfterDecime + start);
            Int32 endUsefullData = startUsefullData + len;
            //panelRtl_433.SetMessage("Start= " + start.ToString() + "  len= " + len.ToString() + " \n");
            if (startUsefullData < 0 && endUsefullData < 0)
            {
                //panelRtl_433.SetMessage("GetTabIQ case 0 \n");
                startUsefullData += sizeBufferIQ;
                endUsefullData += sizeBufferIQ;
            }
            if (startUsefullData < 0)  //case 2
            {
                //panelRtl_433.SetMessage("GetTabIQ case 2 \n");
                Int32 startToEnd = sizeBufferIQ + startUsefullData;
                Int32 partToEnd = -startUsefullData;
                Int32 partToStart = len - partToEnd;
                ClassUtils.CopyMem(retourIQ, ptrTabIQ, startToEnd, 0, partToEnd);
                ClassUtils.CopyMem(retourIQ, ptrTabIQ, 0, partToEnd, partToStart);
            }
            else if (endUsefullData > sizeBufferIQ)  //case 3
            {
                //panelRtl_433.SetMessage("GetTabIQ case 3 \n");
                Int32 startToEnd = (Int32)(startUsefullData);
                Int32 partToEnd = sizeBufferIQ - startUsefullData;
                Int32 partToStart = len - partToEnd;
                ClassUtils.CopyMem(retourIQ, ptrTabIQ, startToEnd, 0, partToEnd);
                ClassUtils.CopyMem(retourIQ, ptrTabIQ, 0, partToEnd, partToStart);
            }
            else  //case 1
            {
                //panelRtl_433.SetMessage("GetTabIQ case 1 \n");
                ClassUtils.CopyMem(retourIQ, ptrTabIQ, startUsefullData, 0, len);
            }
            return retourIQ;
        }
        internal List<PointF>[] TreatGraph(NativeMethods.R_deviceToPlugin info, Dictionary<String, String> listData, double SampleRateDecime, IntPtr ptrDemod, String FrequencyStr,out String[] nameGraph, float[] memoDataIQForRs433,Int32 ptrMemoDataForRs433, out float[] DataIQForRecord, Rtl_433_Panel owner)
        {
            this.panelRtl_433 = owner;
            List<PointF>[] points = null;
            nameGraph = null;
            byte[] dataByteForRs433 = null;
            float[] DataIQForDisplay;
            DataIQForRecord = null;
            if (ptrDemod != IntPtr.Zero)
            {
                NativeMethods.Dm_state struct_demod = new NativeMethods.Dm_state();//1.5.4.4 add this line
                try
                {
                    struct_demod = (NativeMethods.Dm_state)Marshal.PtrToStructure(ptrDemod, typeof(NativeMethods.Dm_state));
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message + "  ClassInterfaceWithRtl433->treatGraph_2");
                }
                short[] am_buf = new short[0];
                short[] fm_buf = new short[0];
                Int32 _startPlugin = 0;
                Int32 _startFsk = 0;
                NativeMethods.Samp_grab struct_demod_samp_grab;
                NativeMethods.Pulse_detect pulseDetect = new NativeMethods.Pulse_detect();
                NativeMethods.Pulse_data pulseData = new NativeMethods.Pulse_data();
                NativeMethods.Pulse_data pulseDataFsk = new NativeMethods.Pulse_data();
                if (struct_demod.ptr_samp_grab != IntPtr.Zero)
                {
                    struct_demod_samp_grab = new NativeMethods.Samp_grab();
                    try
                    {
                        struct_demod_samp_grab = (NativeMethods.Samp_grab)Marshal.PtrToStructure(struct_demod.ptr_samp_grab, typeof(NativeMethods.Samp_grab));
                        if (struct_demod_samp_grab.sg_buf != null)
                        {
#if DEBUG
                            if (ptrMemoDataForRs433 != struct_demod_samp_grab.sg_index)
                                panelRtl_433.SetMessage("ptrMemoDataForRs433 != struct_demod_samp_grab.sg_index \n");
#endif
                            if (struct_demod_samp_grab.sg_size > 0)
                            {
                                IntPtr ptrTabIQ = (System.IntPtr)0;
                                ushort[] bufTemp;
                                DataIQForRecord = GetTabIQ(info.startForRecord, info.lenForRecord, (Int32)struct_demod_samp_grab.sg_size, memoDataIQForRs433, ptrMemoDataForRs433);
                                DataIQForDisplay = GetTabIQ(info.startForDisplay, (Int32)info.lenForDisplay, (Int32)struct_demod_samp_grab.sg_size, memoDataIQForRs433, ptrMemoDataForRs433);
                                if (DataIQForDisplay == null)
                                    return null;
                                dataByteForRs433 = new byte[DataIQForDisplay.Length];
                                if ( !ClassUtils.ConvertFloatToByte(DataIQForDisplay, dataByteForRs433))
                                    return null;
                                info.lenForDisplay /= 2;
                                bufTemp = new ushort[info.lenForDisplay];

                                float avg_db = NativeMethods.envelope_detect(dataByteForRs433, bufTemp, (Int32)info.lenForDisplay);
                                NativeMethods.Filter_state stateAm = new NativeMethods.Filter_state();
                                //stateAm = struct_demod.lowpass_filter_state;
                                //{
                                //    x = new Int16[1],
                                //    y = new Int16[1]
                                //};
                                //stateAm.x[0] = 23;
                                //stateAm.y[0] = 144;
                                am_buf = new short[info.lenForDisplay];
                                NativeMethods.baseband_low_pass_filter(bufTemp, am_buf, (Int32)info.lenForDisplay, ref stateAm);
                                NativeMethods.Demodfm_state stateFm = new NativeMethods.Demodfm_state();

                                stateFm = struct_demod.demod_FM_state;
                                bool fpdm =  false;   //always FSK_PULSE_DETECT_OLD
                                fm_buf = new short[info.lenForDisplay];
                                float low_pass = struct_demod.low_pass != 0.0f ? struct_demod.low_pass : fpdm ? 0.2f : 0.1f;
                                NativeMethods.baseband_demod_FM(dataByteForRs433, fm_buf, (UInt32)info.lenForDisplay, (UInt32)SampleRateDecime, low_pass, ref stateFm);

                                pulseData.pulse = new Int32[info.lenForDisplay];
                                pulseData.gap = new Int32[info.lenForDisplay];
                                UInt32 fpdmPdp = 0;  //1 idem....always FSK_PULSE_DETECT_OLD
                                UInt64 sampleOffset = 0;
                                NativeMethods.Pulse_detect PulseDetectBefore = new NativeMethods.Pulse_detect();
                                PulseDetectBefore = (NativeMethods.Pulse_detect)Marshal.PtrToStructure(struct_demod.pulse_detect, typeof(NativeMethods.Pulse_detect));
                                pulseDetect = new NativeMethods.Pulse_detect();
                                pulseDetect = PulseDetectBefore;
                                pulseDataFsk.pulse = new Int32[info.lenForDisplay];
                                pulseDataFsk.gap = new Int32[info.lenForDisplay];
                                pulseDetect.lead_in_counter = 1025;
                                pulseDetect.data_counter = 0;
                                pulseDetect.pulse_length = 0;
                                NativeMethods.pulse_detect_package(ref pulseDetect, am_buf, fm_buf, (Int32)info.lenForDisplay, (UInt32)SampleRateDecime, sampleOffset, ref pulseData, ref pulseDataFsk, fpdmPdp, ref _startPlugin, ref _startFsk);
                            }
                            else
                                Debug.WriteLine("**********************struct_demod_samp_grab.sg_size=0");
                        }
                        else
                            Debug.WriteLine("**********************struct_demod_samp_grab.sg_buf=0");
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message + "   ClassInterfaceWithRtl433->treatGraph_3");
                    }
                }
                else
                    Debug.WriteLine("**********************struct_demod.ptr_samp_grab=0");
                if (pulseData.num_pulses > 0 && info.package_type == (ushort)Package_types.PULSE_DATA_OOK)
                {
                    nameGraph = new string[] { "Pulses", "Am", "IQ" };
                    Int32 NumGraph = nameGraph.Count();
                    points = new List<PointF>[NumGraph];
                    for (Int32 i = 0; i < NumGraph; i++)
                        points[i] = new List<PointF>();
                    points = TreatOokOrFskCurves(pulseData, am_buf, fm_buf, points, NumGraph, false, SampleRateDecime, dataByteForRs433, info.lenForDisplay, info.startForDisplay);
                    dataByteForRs433 = null;
                    if (points != null && points[0].Count > 0)
                        return points;
                    else
                        return null;
                }
                else if (pulseDataFsk.num_pulses > 0 && info.package_type == (ushort)Package_types.PULSE_DATA_FSK)
                {
                    nameGraph = new string[] { "Pulses", "Fm", "IQ" };
                    Int32 NumGraph = nameGraph.Count();
                    points = new List<PointF>[NumGraph];
                    for (Int32 i = 0; i < NumGraph; i++)
                        points[i] = new List<PointF>();
                    points = TreatOokOrFskCurves(pulseDataFsk, am_buf, fm_buf, points, NumGraph, true, SampleRateDecime, dataByteForRs433, info.lenForDisplay, info.startForDisplay);
                    dataByteForRs433 = null;
                    if (points != null && points[0].Count > 0)
                        return points;
                    else
                        return null;
                }
            }
            //if (points != null && points[0].Count > 0)
            //    return points;
            //else
            return null;
        }
    }

}
