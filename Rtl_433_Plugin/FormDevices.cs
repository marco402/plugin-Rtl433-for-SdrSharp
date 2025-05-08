
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
using GraphLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SDRSharp.Rtl_433
{
    internal partial class FormDevices : Form
    {
        #region declare
        private Int32 NumGraphs = 0;
        private readonly float _nbPointX = 0;
        private readonly ClassFormDevices classParent;
        private readonly Dictionary<String, Label> listLabelKey;
        private readonly Dictionary<String, Label> listLabelValue;
        private Int64 memoTimeMax;
        private Int64 memoTime;
        private Int64 nbMessages;
        private readonly Color memoBackColortoolStripSplitLabelRecordOneShoot;
        private readonly Color memoForeColortoolStripSplitLabelRecordOneShoot;
        private readonly String memoTexttoolStripSplitLabelRecordOneShoot;
        private readonly Color memoBackColortoolStripStatusLabelFreezeData;
        private readonly Color memoForeColortoolStripStatusLabelFreezeData;
        private readonly String memoTexttoolStripSplitLabelFreezeData;
        private float memoHeightPlotterDisplayExDevices = 0;
        private Boolean firstToTop = true;
        private readonly Dictionary<String, Int32> cacheListColumns;
        private readonly ListViewItem[] cacheListInfosMessages;
        private Dictionary<String, Int32> listRow;
        private Int32 activColumn = 1;
        private bool WidthInit = false;
        private readonly Int32 maxColumns = 0;
        private readonly Int32 maxInfoDevices = 0;
        private Int32 nbInfoDevice = 0;
        private readonly Int32[] MaxInfoWidth;
        #endregion
        #region constructor load close form
        Label theLabelModel;
        internal FormDevices(ClassFormDevices classParent)
        {
            InitializeComponent();
            this.SuspendLayout();
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
            nfi.NumberDecimalDigits = 3;
            this.classParent = classParent;
            this.Font = ClassUtils.Font;
            this.BackColor = ClassUtils.BackColor;
            this.ForeColor = ClassUtils.ForeColor;
            this.Cursor = ClassUtils.Cursor;
            listLabelKey = new Dictionary<String, Label>();
            listLabelValue = new Dictionary<String, Label>();
            this.tableLayoutPanelDeviceData.Name = "tableLayoutPanelDeviceData";
            this.tableLayoutPanelDeviceData.TabIndex = 1;
            toolStripStatusLabelPeriodeCurrent.Text = "Period: 0";
            toolStripStatusLabelPeriodeMax.Text = "Period max: 0";
            toolStripStatusLabelNbMessages.Text = "NB messages: 0";
            statusStripDevices.BackColor = this.BackColor;
            statusStripDevices.ForeColor = this.ForeColor;
            statusStripDevices.ShowItemToolTips = true;
            toolStripSplitLabelRecordOneShoot.ToolTipText = "Record data buffer Wav (checkbox on panel) \n" +
                " to directory Recordings if exist else in SdrSharp.exe directory \n" +
                " You can replay Stereo file with SdrSharp Source=Baseband File player\n";
            //this.Width = 540;      //left small in designer else no resize if width<540(if 540 in designer)
            memoBackColortoolStripSplitLabelRecordOneShoot = toolStripSplitLabelRecordOneShoot.BackColor;
            memoForeColortoolStripSplitLabelRecordOneShoot = toolStripSplitLabelRecordOneShoot.ForeColor;
            memoTexttoolStripSplitLabelRecordOneShoot = toolStripSplitLabelRecordOneShoot.Text;
            memoBackColortoolStripStatusLabelFreezeData = toolStripStatusLabelFreezeData.BackColor;
            memoForeColortoolStripStatusLabelFreezeData = toolStripStatusLabelFreezeData.ForeColor;
            memoTexttoolStripSplitLabelFreezeData = toolStripStatusLabelFreezeData.Text;
            memoHeightPlotterDisplayExDevices = plotterDisplayExDevices.Height;
            HideShowAllGraphs(false);
            DisplayWaitMessage();
            plotterDisplayExDevices.SetAmbiantProperty(this.BackColor, this.ForeColor, this.Font);
            this.MinimumSize = new System.Drawing.Size(660, 100);   //width=660 else no display end graph why? todo
            this.Size = new System.Drawing.Size(660, 600);

            theLabelModel = new Label();
            theLabelModel.Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top;
            theLabelModel.AutoSize = true;
            theLabelModel.BackColor = this.BackColor;
            theLabelModel.ForeColor = this.ForeColor;
            theLabelModel.Font = this.Font;
            listRow=new Dictionary<String, Int32>() ;


            ClassFunctionsVirtualListView.InitListView(listViewListMessages);
            listViewListMessages.BackColor = this.BackColor;   //pb ambient property ???
            listViewListMessages.ForeColor = this.ForeColor;
            listViewListMessages.Font = this.Font;
            listViewListMessages.Cursor = this.Cursor;

            maxInfoDevices = ClassConst.MAXROWCODE;
            maxColumns = ClassConst.NBMAXCOLUMNDEVICES;
            cacheListInfosMessages = new ListViewItem[this.maxInfoDevices];
            cacheListColumns = new Dictionary<String, Int32>();
            MaxInfoWidth = new Int32[maxColumns];
            for (Int32 i = 0; i < maxColumns; i++)
                MaxInfoWidth[i] = 0;
            plotterDisplayExDevices.Width = this.Width-1000;    //why?
            listViewListMessages.Width = this.Width-100;        //why?
 

            this.ResumeLayout(true);
        }
        private Label labelWaitMessage;
        private void DisplayWaitMessage()
        {
            labelWaitMessage = new Label
            {
                Anchor = System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top,
                AutoSize = true
            };
            tableLayoutPanelDeviceData.Controls.Add(labelWaitMessage, 0, 0);
            labelWaitMessage.Font = this.Font;
            labelWaitMessage.BackColor = this.BackColor;
            labelWaitMessage.ForeColor = this.ForeColor;
            labelWaitMessage.Text = $"Wait to receive new data \n max devices with graph: {ClassUtils.MaxDevicesWithGraph}\n You can click on display curves" +
                $" \n or change nbDevicesWithGraph in exe.config \n or sample rate highest for memory.";
            tableLayoutPanelDeviceData.RowStyles[0].Height = 100;
        }
        private Boolean closeByProgram = false;
        internal void CloseByProgram()
        {
            closeByProgram = true;
            this.Close();
        }
        private void FormDevices_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!closeByProgram)
                classParent.ClosingOneFormDevice(this.Text);
        }
        protected override void OnClosed(EventArgs e)
        {
            plotterDisplayExDevices.Dispose();
            base.OnClosed(e);
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            plotterDisplayExDevices.Dispose();
            base.OnClosing(e);
        }
        #endregion
        #region publics functions
        internal Boolean SetInfoDevice(Dictionary<String, String> listData)
        {
            this.SuspendLayout();
            listViewListMessages.BeginUpdate();
            this.SetInfoDevicePrivate(listData);
            listViewListMessages.EndUpdate();
            this.ResumeLayout(true);
            return true;
        }
        internal void ResetLabelRecord()
        {
            toolStripSplitLabelRecordOneShoot.BackColor = memoBackColortoolStripSplitLabelRecordOneShoot;
            toolStripSplitLabelRecordOneShoot.ForeColor = memoForeColortoolStripSplitLabelRecordOneShoot;
            toolStripSplitLabelRecordOneShoot.Text = memoTexttoolStripSplitLabelRecordOneShoot;
        }
        internal void HideShowAllGraphs(Boolean visible)
        {
            this.SuspendLayout();
            if (visible)
            {
                plotterDisplayExDevices.Visible = true;
                tableLayoutPanelDeviceData.RowStyles[0].Height = memoHeightPlotterDisplayExDevices;
            }
            else
            {
                memoHeightPlotterDisplayExDevices = tableLayoutPanelDeviceData.RowStyles[0].Height;
                tableLayoutPanelDeviceData.RowStyles[0].Height = 0;
                plotterDisplayExDevices.Visible = false;
            }
            _displayGraph = visible;  //no call property
            this.ResumeLayout(true);
        }
        internal float[] miniY = { 0, 0, 0, 0, 0 };
        internal float[] maxiY = { 1, 1, 1, 1, 1 };
        internal void AddGraph(List<PointF> tabPoints, String nameGraph, float MaxXAllData)
        {
            this.SuspendLayout();
            if (NumGraphs == 0)
            {
                labelWaitMessage.Dispose();  //     Visible = false;
                plotterDisplayExDevices.Smoothing = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                plotterDisplayExDevices.DataSources.Clear();
                plotterDisplayExDevices.PanelLayout = PlotterGraphPaneEx.LayoutMode.VerticalArranged;  //only mode with modified Graphlib
            }
            plotterDisplayExDevices.DataSources.Add(new DataSource());
            plotterDisplayExDevices.DataSources[NumGraphs].Name = nameGraph;
            plotterDisplayExDevices.DataSources[NumGraphs].OnRenderXAxisLabel += RenderXLabel;
            plotterDisplayExDevices.DataSources[NumGraphs].Length = (Int32)_nbPointX;
            plotterDisplayExDevices.DataSources[NumGraphs].AutoScaleY = false;  //keep false with modified Graphlib
            //try {
            if (tabPoints.Count > 0)
            {
                miniY[NumGraphs] = tabPoints.Min(point => point.Y);
                maxiY[NumGraphs] = tabPoints.Max(point => point.Y);
            }
            //}
            //catch (Exception ex)
            //{
            //MessageBox.Show(ex.Message, "Error  ", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //}


            plotterDisplayExDevices.DataSources[NumGraphs].SetDisplayRangeY(miniY[NumGraphs], maxiY[NumGraphs]);    //to see why flip y ?
            plotterDisplayExDevices.DataSources[NumGraphs].SetGridDistanceY((maxiY[NumGraphs] - miniY[NumGraphs]) / 5); //(maxiY- miniY)/5.0fno grid with Graphlib with OPTION
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
            plotterDisplayExDevices.SetMaxXAllData(MaxXAllData, true); //last line for refresh
            this.ResumeLayout(true);
        }
        //*************************************************************************************
        internal void SetDataGraph(List<PointF>[] points, String[] nameGraph)
        {
            if (points == null)  //for change typeForm
            {
                if (!_dataFrozen)
                {
                    plotterDisplayExDevices.Height = 70;
                    tableLayoutPanelDeviceData.RowStyles[0].Height = 70;
                    this.ResumeLayout();
                }
                return;
            }
            tableLayoutPanelDeviceData.RowStyles[0].Height = memoHeightPlotterDisplayExDevices;
            float MaxXAllData = float.MinValue;
            foreach (List<PointF> lpt in points)
            {
                if (lpt.Count > 0)
                    MaxXAllData = Math.Max(lpt.Max(point => point.X), MaxXAllData);
            }
 
            this.SuspendLayout();
            Boolean foundGraph = false;
            for (Int32 i = 0; i < points.Length; i++) 
            {
                foundGraph = false;
                Int32 indexGraph = 0;
                foreach (DataSource source in plotterDisplayExDevices.DataSources)
                {
                    if (source.Name == nameGraph[i])
                    {
                        if (!_dataFrozen)
                        {
                            RefreshPoints(points[i], MaxXAllData, indexGraph);
                        }
                        foundGraph = true;
                    }
                    indexGraph ++;
                }

                if (!foundGraph && points[i].Count > 0 && nameGraph[i] != "")
                {
                    AddGraph(points[i], nameGraph[i], MaxXAllData);
                    NumGraphs ++;
                }
            }
            this.ResumeLayout(true);
        }
        internal void RefreshPoints(List<PointF> tabPoints, float MaxXAllData, Int32 indexGraph)
        {
            if (plotterDisplayExDevices.GetEndDrawGraphEvent())
            {
                if (tabPoints.Count > 0)
                {
                    miniY[indexGraph] = tabPoints.Min(point => point.Y);
                    maxiY[indexGraph] = tabPoints.Max(point => point.Y);
                }
                else
                {
                    for (float x = 0; x < 100; x++)
                    {
                        tabPoints.Add(new PointF(x, 0));
                    }

                }
                this.SuspendLayout();
                plotterDisplayExDevices.DataSources[indexGraph].SetDisplayRangeY(miniY[indexGraph], maxiY[indexGraph]);    //(-250, 250);
                plotterDisplayExDevices.DataSources[indexGraph].SetGridDistanceY((maxiY[indexGraph] - miniY[indexGraph]) / 5);

                plotterDisplayExDevices.DataSources[indexGraph].Length = (Int32)tabPoints.Count;
                plotterDisplayExDevices.DataSources[indexGraph].Samples = tabPoints;
                if (indexGraph < (NumGraphs - 1))
                    plotterDisplayExDevices.SetMaxXAllData(MaxXAllData, false);  //pb if not always same number graph ok if true
                else
                    plotterDisplayExDevices.SetMaxXAllData(MaxXAllData, true);  //last line for refresh
                this.ResumeLayout(true);

            }
        }
        internal Boolean DisplayGraph
        {
            get { return _displayGraph; }
            set
            {
                _displayGraph = value;
                HideShowAllGraphs(value);
            }
        }
        #endregion
        #region privates functions
        private void endSetInfoDevice()
        {
            if (!WidthInit)
            {
                listViewListMessages.Columns[0].Width = MaxInfoWidth[0];
                WidthInit = true;
            }
            listViewListMessages.Columns[activColumn].Width = MaxInfoWidth[activColumn];
            listViewListMessages.VirtualListSize = nbInfoDevice;
            activColumn++;
            if (activColumn > maxColumns - 1)
                activColumn = 1;
            nbMessages++;
            toolStripStatusLabelNbMessages.Text = $"NB messages: {nbMessages.ToString()}";

        }
        private void SetInfoDevicePrivate(Dictionary<String, String> listData)
        {
            if (cacheListColumns == null)
                return;


            //**********************add column***********************************
            //change listViewDevices.HeaderStyle = ColumnHeaderStyle.None; for display column name
            ClassFunctionsVirtualListView.AddOneColumnDevices("Info", cacheListColumns, listViewListMessages, maxColumns);
            //***********************add other column if necessary*************************************
            for (Int32 i = 1; i < maxColumns; i++)
                ClassFunctionsVirtualListView.AddOneColumnDevices($"Data {i}", cacheListColumns, listViewListMessages, maxColumns);

            Int32 colWidth = 0;
            foreach (KeyValuePair<string, string> _data in listData)
            {
                if (_data.Key.ToUpper().Contains("TIME"))
                {
                    if (DateTime.TryParse(_data.Value, out DateTime maDate))
                    {
                        Int64 currentTime = maDate.ToFileTime() / 10000000;
                        if (memoTime > 0)
                        {
                            Int64 dtTime = (currentTime - memoTime);
                            if (dtTime > memoTimeMax)
                                memoTimeMax = dtTime;
                            toolStripStatusLabelPeriodeCurrent.Text = $"Period:{dtTime.ToString()}s.";
                            toolStripStatusLabelPeriodeMax.Text = $"Period max:{memoTimeMax.ToString()}s.";
                        }
                        memoTime = currentTime;
                    }
                }
                //**********************search if info name exist******************************
                ListViewItem NameInfoDevice = ClassFunctionsVirtualListView.GetItem(_data.Key, cacheListInfosMessages);
                //**************************new info name***************************
                if (NameInfoDevice == null)
                {
                    WidthInit = false;
                    if (nbInfoDevice > maxInfoDevices - 1)
                    {
                        endSetInfoDevice();
                        return;
                    }

                    NameInfoDevice = new ListViewItem(_data.Key);
                    colWidth = TextRenderer.MeasureText(_data.Key, listViewListMessages.Font).Width + 10;
                    if (colWidth > MaxInfoWidth[0])
                        MaxInfoWidth[0] = colWidth;
                    colWidth = TextRenderer.MeasureText(_data.Value, listViewListMessages.Font).Width + 10;
                    if (colWidth > MaxInfoWidth[activColumn])
                        MaxInfoWidth[activColumn] = colWidth;
                    for (Int32 i = 1; i < maxColumns; i++)
                    {
                        NameInfoDevice.SubItems.Add("");
                    }
                    NameInfoDevice.SubItems[activColumn].Text = _data.Value;
                    //**************add new line in cacheListMessages
                    ClassFunctionsVirtualListView.AddElemToCache(cacheListInfosMessages, firstToTop, nbInfoDevice, NameInfoDevice);
                    //************************************************
                    nbInfoDevice++;
                }
                //**************************refresh info***************************
                else
                {
                    NameInfoDevice.SubItems[activColumn].Text = _data.Value;
                    colWidth = TextRenderer.MeasureText(_data.Value, listViewListMessages.Font).Width + 10;
                    if (colWidth > MaxInfoWidth[activColumn])
                        MaxInfoWidth[activColumn] = colWidth;
                }
                //**************************************************************************************
            }
            endSetInfoDevice();
        }
        private void listViewListMessages_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            try
            {
                if (e.ItemIndex >= 0)
                {
                    ListViewItem lvi = cacheListInfosMessages[e.ItemIndex];
                    if (lvi != null)
                        e.Item = lvi;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "Error fct(listViewListMessages_RetrieveVirtualItem)");
            }
        }
        private Boolean _displayGraph = false;
        private String RenderXLabel(Int32 value)
        {
            if (value > 1000000)
                return String.Format("{0}s", (((float)value / 1000000f)).ToString("N1"));
            else if (value > 1000)
                return String.Format("{0}ms", (((float)value / 1000f)).ToString("N1"));
            else
                return String.Format("{0}µs", value.ToString());
        }
        private String RenderYLabel(DataSource s, float value)
        {
            return String.Format("{0:0.0}", value);
        }
        private void ApplyColorSchema(Int32 j)  //
        {
            Color[] cols = { Color.FromArgb(255,0,0),
                                Color.FromArgb(0,255,0),
                                Color.FromArgb(255,255,0),
                                Color.FromArgb(64,64,255),
                                Color.FromArgb(0,255,255) ,
                                Color.FromArgb(255,0,255),
                                Color.FromArgb(255,128,0) };
            plotterDisplayExDevices.DataSources[j].GraphColor = cols[j % 7];
            if (j == 0)
            {
                plotterDisplayExDevices.BackgroundColorTop = Color.Black;
                plotterDisplayExDevices.BackgroundColorBot = Color.Black;
                plotterDisplayExDevices.SolidGridColor = Color.DarkGray;
                plotterDisplayExDevices.DashedGridColor = Color.DarkGray;
            }
        }
        private Boolean _dataFrozen = false;
        #endregion
        #region form event
        private void ToolStripSplitLabelRecordOneShoot_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            if (toolStripSplitLabelRecordOneShoot.BackColor != memoBackColortoolStripSplitLabelRecordOneShoot)
            {
                classParent.SetRecordDevice(this.Text, false);
                ResetLabelRecord();
            }
            else
            {
                if (classParent.SetRecordDevice(this.Text, true) == true)
                {
                    toolStripSplitLabelRecordOneShoot.BackColor = Color.Green;
                    toolStripSplitLabelRecordOneShoot.ForeColor = Color.White;
                    toolStripSplitLabelRecordOneShoot.Text = "Cancel record";
                }
            }
            this.ResumeLayout(true);
        }
        private void ToolStripStatusLabelFreezeData_Click(object sender, EventArgs e)
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
            this.ResumeLayout(true);
        }
        private void FormDevices_ResizeEnd(object sender, EventArgs e)
        {
            plotterDisplayExDevices.Width = this.Width-1000;  //why?
            listViewListMessages.Width = this.Width-100;  //why?
        }
        #endregion
    }
}
