
/* Written by Marc Prieur (marco40_github@sfr.fr)
                                FormDevices.cs 
                            project Rtl_433_Plugin
						         Plugin for SdrSharp
 **************************************************************************************
 Creative Commons Attrib Share-Alike License
 You are free to use/extend this library but please abide with the CC-BY-SA license:
 Attribution-NonCommercial-ShareAlike 4.0 International License
 http://creativecommons.org/licenses/by-nc-sa/4.0/

//History : V1.00 2021-04-01 - First release
//          V1.10 2021-20-April

 All text above must be included in any redistribution.
 **********************************************************************************/
//all cu8-->https://github.com/merbanan/rtl_433_tests/tree/master/tests

/*use part of library GraphLib:*/
/* Copyright (c) 2008-2014 DI Zimmermann Stephan (stefan.zimmermann@tele2.at)
 *   
 * Permission is hereby granted, free of charge, to any person obtaining a copy 
 * of this software and associated documentation files (the "Software"), to 
 * deal in the Software without restriction, including without limitation the 
 * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or 
 * sell copies of the Software, and to permit persons to whom the Software is 
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in 
 * all copies or substantial portions of the Software. 
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN 
 * THE SOFTWARE.
 */

#define LISTPOINTS

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Linq;
using GraphLib;
using System.Globalization;
namespace SDRSharp.Rtl_433
{
    public partial class FormDevices : Form
    {
#region declare
        private int NumGraphs = 0;
        private float _nbPointX = 0;
        private Rtl_433_Panel _classParent;
        private Dictionary<String, Label> listLabelKey;
        private Dictionary<String, Label> listLabelValue;
        private long memoTimeMax;
        private long memoTime;
        private long nbMessages;
        private Color memoBackColortoolStripSplitLabelRecordOneShoot;
        private Color memoForeColortoolStripSplitLabelRecordOneShoot;
        private string memoTexttoolStripSplitLabelRecordOneShoot;
        private Color memoBackColortoolStripStatusLabelDisplayCurves;
        private Color memoForeColortoolStripStatusLabelDisplayCurves;
        private string memoTexttoolStripSplitLabelDisplayCurves;
        private Color memoBackColortoolStripStatusLabelFreezeData;
        private Color memoForeColortoolStripStatusLabelFreezeData;
        private string memoTexttoolStripSplitLabelFreezeData;
        private float memoHeightPlotterDisplayExDevices = 0;
        private int activeColumnForData = 1;
        #endregion
#region constructor load close form
        public FormDevices(Rtl_433_Panel classParent)
        {
            InitializeComponent();
            this.SuspendLayout();
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            nfi.NumberDecimalDigits = 3;
            _classParent = classParent;
            listLabelKey = new Dictionary<String, Label>();
            listLabelValue = new Dictionary<String, Label>();
            this.tableLayoutPanelDeviceData.Name = "tableLayoutPanelDeviceData";
            this.tableLayoutPanelDeviceData.TabIndex = 1;
            //toolStripSplitLabelRecordOneShoot.Visible = false;
            toolStripStatusLabelPeriodeCurrent.Text = "Period: 0";
            toolStripStatusLabelPeriodeMax.Text = "Period max: 0";
            toolStripStatusLabelNbMessages.Text = "NB messages: 0";
            statusStripDevices.ShowItemToolTips=true;
            toolStripSplitLabelRecordOneShoot.ToolTipText = "Record data buffer Mono Stereo or both(checkbox on panel) \n" +
                " to directory Recordings if exist else in SdrSharp.exe directory \n" +
                " You can replay Stereo file with SdrSharp\n" +
                " or load Mono Stereo with Audacity...";
            this.Width = 540;      //left small in designer else no resize if width<540(if 540 in designer)
            memoBackColortoolStripSplitLabelRecordOneShoot = toolStripSplitLabelRecordOneShoot.BackColor;
            memoForeColortoolStripSplitLabelRecordOneShoot = toolStripSplitLabelRecordOneShoot.ForeColor;
            memoTexttoolStripSplitLabelRecordOneShoot = toolStripSplitLabelRecordOneShoot.Text;
            memoBackColortoolStripStatusLabelDisplayCurves = toolStripStatusLabelDisplayCurves.BackColor;
            memoForeColortoolStripStatusLabelDisplayCurves = toolStripStatusLabelDisplayCurves.ForeColor;
            memoTexttoolStripSplitLabelDisplayCurves = toolStripStatusLabelDisplayCurves.Text;
            memoBackColortoolStripStatusLabelFreezeData = toolStripStatusLabelFreezeData.BackColor;
            memoForeColortoolStripStatusLabelFreezeData = toolStripStatusLabelFreezeData.ForeColor;
            memoTexttoolStripSplitLabelFreezeData = toolStripStatusLabelFreezeData.Text;
            memoHeightPlotterDisplayExDevices =  plotterDisplayExDevices.Height;
            hideShowAllGraphs(false);
            displayWaitMessage();
            this.ResumeLayout();
        }
        //private void FormDevices_Load(object sender, EventArgs e)
        //{

        //}
        private Label labelWaitMessage;
        private void displayWaitMessage()
        {
            labelWaitMessage = new Label();
            labelWaitMessage.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top;
            labelWaitMessage.AutoSize = true;
            labelWaitMessage.BackColor = System.Drawing.SystemColors.HighlightText;
            labelWaitMessage.ForeColor = System.Drawing.SystemColors.Highlight;
            tableLayoutPanelDeviceData.Controls.Add(labelWaitMessage, 0, 0);
            labelWaitMessage.Font = new System.Drawing.Font("Segoe UI", 12F);
            labelWaitMessage.Text = "Wait to receive new data";
        }
        private void FormDevices_FormClosing(object sender, FormClosingEventArgs e)
        {
            _classParent.closingOneFormDevice(this.Text);
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            plotterDisplayExDevices.Dispose();
            base.OnClosing(e);
        }
#endregion
#region publics functions
        public void setInfoDevice(Dictionary<String, String> listData)
        {
            this.SuspendLayout();
            if (NumGraphs == 0)
                tableLayoutPanelDeviceData.RowStyles[0].Height = 0;   //50
            foreach (KeyValuePair<string, string> _data in listData)
            {
                if(_data.Key.ToUpper().Contains("TIME"))
                {
                    DateTime maDate;
                    if (DateTime.TryParse(_data.Value, out maDate))
                    {
                        long currentTime = maDate.ToFileTime() / 10000000;
                        if (memoTime > 0)
                        {
                            long dtTime = (currentTime - memoTime);
                            if (dtTime > memoTimeMax)
                                memoTimeMax = dtTime;
                            toolStripStatusLabelPeriodeCurrent.Text = "Period: " + dtTime.ToString() + "s.";
                            toolStripStatusLabelPeriodeMax.Text = "Period max: " + memoTimeMax.ToString() + "s.";
                        }
                        memoTime = currentTime;
                    }
                }
                if (!listLabelKey.ContainsKey(_data.Key))
                {

                    tableLayoutPanelDeviceData.RowCount += 1;
                    tableLayoutPanelDeviceData.RowStyles.Add(new System.Windows.Forms.RowStyle());
                    tableLayoutPanelDeviceData.RowStyles[tableLayoutPanelDeviceData.RowCount - 1].Height = 20;
                    tableLayoutPanelDeviceData.Padding= new Padding( 0,3,0,0);
                    Label theLabelKey = new Label();
                    listLabelKey.Add(_data.Key, theLabelKey);
                    theLabelKey.Anchor = System.Windows.Forms.AnchorStyles.Left|System.Windows.Forms.AnchorStyles.Top;
                    theLabelKey.AutoSize = true;
                    theLabelKey.BackColor = System.Drawing.SystemColors.ControlDarkDark;
                    theLabelKey.ForeColor = System.Drawing.Color.Blue;
                    tableLayoutPanelDeviceData.Controls.Add(theLabelKey, 0, tableLayoutPanelDeviceData.RowCount-1);
                    theLabelKey.Text = _data.Key;
                    for(int col=1; col < 5; col++)
                    {
                        Label theLabelValue = new Label();
                        listLabelValue.Add(_data.Key + col.ToString(), theLabelValue);
                        theLabelValue.Tag = tableLayoutPanelDeviceData.RowCount;
                        theLabelValue.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top;
                        theLabelValue.AutoSize = true;
                        theLabelValue.BackColor = System.Drawing.SystemColors.ControlDarkDark;
                        theLabelValue.ForeColor = System.Drawing.Color.Blue;
                        tableLayoutPanelDeviceData.Controls.Add(theLabelValue, col, tableLayoutPanelDeviceData.RowCount-1);
                    }
                }
                listLabelValue[_data.Key + activeColumnForData.ToString()].Text = _data.Value;
            }
            nbMessages += 1;
            toolStripStatusLabelNbMessages.Text ="NB messages: " +  nbMessages.ToString();
            activeColumnForData += 1;
            if (activeColumnForData == 5)
                activeColumnForData = 1;
            this.ResumeLayout();
        }
        public void resetLabelRecord()
        {
            toolStripSplitLabelRecordOneShoot.BackColor = memoBackColortoolStripSplitLabelRecordOneShoot;
            toolStripSplitLabelRecordOneShoot.ForeColor = memoForeColortoolStripSplitLabelRecordOneShoot;
            toolStripSplitLabelRecordOneShoot.Text = memoTexttoolStripSplitLabelRecordOneShoot;
        }
        //public void resetDisplayCurves()
        //{
        //    hideShowAllGraphs(false);
        //}
        public void hideShowAllGraphs(bool visible)
        {
            this.SuspendLayout();
            if (visible)
            {
                toolStripStatusLabelDisplayCurves.BackColor = memoBackColortoolStripStatusLabelDisplayCurves;
                toolStripStatusLabelDisplayCurves.ForeColor = memoForeColortoolStripStatusLabelDisplayCurves;
                toolStripStatusLabelDisplayCurves.Text = memoTexttoolStripSplitLabelDisplayCurves;
                plotterDisplayExDevices.Visible = true;
                tableLayoutPanelDeviceData.RowStyles[0].Height = memoHeightPlotterDisplayExDevices;
            }
            else
            {
                toolStripStatusLabelDisplayCurves.BackColor = Color.Green;
                toolStripStatusLabelDisplayCurves.ForeColor = Color.White;
                toolStripStatusLabelDisplayCurves.Text = "Display curves";
                memoHeightPlotterDisplayExDevices = tableLayoutPanelDeviceData.RowStyles[0].Height;
                tableLayoutPanelDeviceData.RowStyles[0].Height = 0;
                plotterDisplayExDevices.Visible = false;
            }
            _displayGraph = visible;  //no call property
            this.ResumeLayout();
        }
        public void addGraph(List<PointF> tabPoints, String nameGraph,float MaxXAllData)
        {
            this.SuspendLayout();
            if (NumGraphs == 0)
            {
                labelWaitMessage.Dispose();  //     Visible = false;
                plotterDisplayExDevices.Smoothing = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                plotterDisplayExDevices.DataSources.Clear();
                plotterDisplayExDevices.PanelLayout = PlotterGraphPaneEx.LayoutMode.VERTICAL_ARRANGED;  //only mode with modified Graphlib
            }
            plotterDisplayExDevices.DataSources.Add(new DataSource());
            plotterDisplayExDevices.DataSources[NumGraphs].Name = nameGraph;
            plotterDisplayExDevices.DataSources[NumGraphs].OnRenderXAxisLabel += RenderXLabel;
            plotterDisplayExDevices.DataSources[NumGraphs].Length = (int)_nbPointX;
            plotterDisplayExDevices.DataSources[NumGraphs].AutoScaleY = false;  //keep false with modified Graphlib
            float miniY = 0;
            float maxiY = 1;
            if (tabPoints.Count > 0)
            {
                    miniY = tabPoints.Min(point => point.Y);
                    maxiY = tabPoints.Max(point => point.Y);
            }
            plotterDisplayExDevices.DataSources[NumGraphs].SetDisplayRangeY(miniY, maxiY);    //to see why flip y ?
            plotterDisplayExDevices.DataSources[NumGraphs].SetGridDistanceY((maxiY-miniY)/5); //(maxiY- miniY)/5.0fno grid with Graphlib with OPTION
            plotterDisplayExDevices.DataSources[NumGraphs].Samples = tabPoints;
            plotterDisplayExDevices.DataSources[NumGraphs].OnRenderYAxisLabel = RenderYLabel;
            ApplyColorSchema(NumGraphs);  
            plotterDisplayExDevices.Height = 100 + ((NumGraphs + 1) * 90);
            tableLayoutPanelDeviceData.RowStyles[0].Height = 100 + ((NumGraphs + 1) * 90);
            if (NumGraphs == 0)
                plotterDisplayExDevices.SetDisplayRangeX(0, MaxXAllData);
            plotterDisplayExDevices.SetGridDistanceX(0);
            plotterDisplayExDevices.SetMarkerXPixels(100);
            ApplyColorSchema(NumGraphs);
            plotterDisplayExDevices.SetMaxXAllData(MaxXAllData,true); //last line for refresh
            this.ResumeLayout();
        }
         //*************************************************************************************
        public void setDataGraph(List<PointF>[] points, string[] nameGraph)
        {

            float MaxXAllData = float.MinValue;
            foreach (List<PointF> lpt in points)
            {
                if (lpt.Count > 0)
                    MaxXAllData = Math.Max(MaxXAllData, lpt.Max(point => point.X));
            }

            bool foundGraph = false;
            for( int i=0;i<nameGraph.Length;i++)
            {
                    foundGraph = false;
                int indexGraph = 0;
                    foreach (DataSource source in plotterDisplayExDevices.DataSources)
                    {
                        if(source.Name==nameGraph[i])
                        {
                            if (!_dataFrozen)
                            {                          
                                refreshPoints(points[i],MaxXAllData, indexGraph);
                            }
                            foundGraph = true;
                        }
                    indexGraph += 1;
                    }
                
                if(!foundGraph && points[i].Count>0)
                {
                    addGraph(points[i], nameGraph[i],MaxXAllData);
                    NumGraphs += 1;
                }
            }
        }

        private bool _displayGraph = false;
        public bool displayGraph
        {
            get { return _displayGraph; }
            set { _displayGraph = value;
                hideShowAllGraphs(value);
            }
        }
        #endregion
        #region privates functions
        public void refreshPoints(List<PointF> tabPoints,float MaxXAllData,int indexGraph)
        {
            if (plotterDisplayExDevices.getEndDrawGraphEvent())
            {
                this.SuspendLayout();
                float miniY = 0;
                float maxiY = 0;
                if (tabPoints.Count > 0)
                {
                    miniY = tabPoints.Min(point => point.Y);
                    maxiY = tabPoints.Max(point => point.Y);
                }
                plotterDisplayExDevices.DataSources[indexGraph].SetDisplayRangeY(miniY, maxiY);    //(-250, 250);
                plotterDisplayExDevices.DataSources[indexGraph].SetGridDistanceY((maxiY - miniY) / 5);

                plotterDisplayExDevices.DataSources[indexGraph].Length = (int)tabPoints.Count;
                plotterDisplayExDevices.DataSources[indexGraph].Samples = tabPoints;
                if (indexGraph < (NumGraphs-1))
                    plotterDisplayExDevices.SetMaxXAllData(MaxXAllData, false);
                else
                    plotterDisplayExDevices.SetMaxXAllData(MaxXAllData, true);  //last line for refresh
                this.ResumeLayout();
            }
        }
        private String RenderXLabel( int value)
        { 
            if (value > 1000000)
                return string.Format("{0}s",(((float)value / 1000000f)).ToString("N1"));
            else if (value > 1000)
                return string.Format("{0}ms", (((float)value / 1000f)).ToString("N1"));
            else
                return string.Format("{0}µs", value.ToString());
        }
        private String RenderYLabel(DataSource s, float value)
        {
            return String.Format("{0:0.0}", value);
        }
        private void ApplyColorSchema(int j)  //
        {
            Color[] cols = { Color.FromArgb(255,0,0),
                                Color.FromArgb(0,255,0),
                                Color.FromArgb(255,255,0),
                                Color.FromArgb(64,64,255),
                                Color.FromArgb(0,255,255) ,
                                Color.FromArgb(255,0,255),
                                Color.FromArgb(255,128,0) };
            plotterDisplayExDevices.DataSources[j].GraphColor = cols[j % 7];
            if(j==0)
            {
            plotterDisplayExDevices.BackgroundColorTop = Color.Black;
            plotterDisplayExDevices.BackgroundColorBot = Color.Black;
            plotterDisplayExDevices.SolidGridColor = Color.DarkGray;
            plotterDisplayExDevices.DashedGridColor = Color.DarkGray;
            }
        }
        private bool _dataFrozen = false;
#endregion
#region form event
        private void toolStripStatusLabelDisplayCurves_Click(object sender, EventArgs e)
        {
            if (toolStripStatusLabelDisplayCurves.BackColor == memoBackColortoolStripStatusLabelDisplayCurves)
            {
                //toolStripStatusLabelDisplayCurves.BackColor = Color.Green;
                //toolStripStatusLabelDisplayCurves.ForeColor = Color.White;
                //toolStripStatusLabelDisplayCurves.Text = "Display curves";
                hideShowAllGraphs(false);
            }
            else
            {
                //toolStripStatusLabelDisplayCurves.BackColor = memoBackColortoolStripStatusLabelDisplayCurves;
                //toolStripStatusLabelDisplayCurves.ForeColor = memoForeColortoolStripStatusLabelDisplayCurves;
                //toolStripStatusLabelDisplayCurves.Text = memoTexttoolStripSplitLabelDisplayCurves;
                hideShowAllGraphs(true);
            }
        }
        private void toolStripSplitLabelRecordOneShoot_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            if (toolStripSplitLabelRecordOneShoot.BackColor != memoBackColortoolStripSplitLabelRecordOneShoot)
            {
                _classParent.setRecordDevice(this.Text, false);
                resetLabelRecord();
            }
            else
            {
               if( _classParent.setRecordDevice(this.Text, true)==true)
                {
                toolStripSplitLabelRecordOneShoot.BackColor = Color.Green;
                toolStripSplitLabelRecordOneShoot.ForeColor = Color.White;
                toolStripSplitLabelRecordOneShoot.Text = "Cancel record";
                }
            }
            this.ResumeLayout();
        }
        private void toolStripStatusLabelFreezeData_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            if (toolStripStatusLabelFreezeData.BackColor == memoBackColortoolStripStatusLabelFreezeData)
            {
                toolStripStatusLabelFreezeData.BackColor = Color.Green;
                toolStripStatusLabelFreezeData.ForeColor = Color.White;
                toolStripStatusLabelFreezeData.Text = "Refresh data";
                _dataFrozen = true;
            }
            else
            {
                toolStripStatusLabelFreezeData.BackColor = memoBackColortoolStripStatusLabelFreezeData;
                toolStripStatusLabelFreezeData.ForeColor = memoForeColortoolStripStatusLabelFreezeData;
                toolStripStatusLabelFreezeData.Text = memoTexttoolStripSplitLabelFreezeData;
                _dataFrozen = false;
            }
            this.ResumeLayout();
        }
        #endregion
    }
}
