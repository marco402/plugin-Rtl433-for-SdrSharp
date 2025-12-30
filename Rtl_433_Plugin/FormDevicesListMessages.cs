/* Written by Marc Prieur (marco40_github@sfr.fr)
                                FormDevicesListMessages.cs 
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
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;

namespace SDRSharp.Rtl_433
{
    internal partial class FormDevicesListMessages : Form
    {
        #region declare
        private readonly String NameForm = "";
        private readonly Int32 maxMessages = 0;
        private readonly Dictionary<String, Int32> cacheListColumns;
        private readonly ListViewItem[] cacheListMessages;
        private Int32 nbMessage = 0;
        private readonly ClassFormListMessages classParent;
        private readonly Boolean firstToTop = false;
        //private Boolean recordTxt = false;
        //private String directory = "";
        private String PathAndNameFile = "";
#if TOPMOST
        private Boolean topMost = false;
        private Panel titleBar;
        private Label customTxt;
        private Button btnMax;
        private Button btnMin;
        private Button btnClose;
        private Button customBtn;
#endif
        #endregion
        internal FormDevicesListMessages(ClassFormListMessages classParent, String name)
        {
            InitializeComponent();
            //this.recordTxt = recordTxt;
            this.classParent = classParent;
            this.Font = ClassUtils.Font;
            this.BackColor = ClassUtils.BackColor;
            this.ForeColor = ClassUtils.ForeColor;
            this.Cursor = ClassUtils.Cursor;
            this.maxMessages = ClassUtils.MaxDevicesWindows;
            this.MinimumSize = new System.Drawing.Size(0, 100); //if only title crash on listViewListMessages.VirtualListSize = nbMessage;
            typeof(Control).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, listViewListMessages, new object[] { true });
            this.SuspendLayout();
            ClassFunctionsVirtualListView.InitListView(listViewListMessages);
            listViewListMessages.BackColor = this.BackColor;   //pb ambient property ???
            listViewListMessages.ForeColor = this.ForeColor;
            statusStripExport.BackColor = this.BackColor;
            statusStripExport.ForeColor = this.ForeColor;
            statusStripExport.ShowItemToolTips = true;
            listViewListMessages.Font = this.Font;
            listViewListMessages.Cursor = this.Cursor;
            cacheListMessages = new ListViewItem[this.maxMessages];
            cacheListColumns = new Dictionary<String, Int32>
            {
                { "N° Mes.", 1 }
            };
            listViewListMessages.Columns.Add("");
            listViewListMessages.Columns[0].Text = "N° Mes.";
            NameForm = name;
            //PathAndNameFile = ClassUtils.GetDirectoryRecording().Replace(" ", "_") + ClassFunctionsVirtualListView.ValideNameFile(  NameForm + "_" + DateTime.Now.ToString() + ".txt", "_").Replace(" ", "_");
            PathAndNameFile = ClassUtils.GetPathAndNameFileDateAndTxt(NameForm);
            this.Text = name + " (Messages received : 0)";
            toolStripStatusLabelExport.ToolTipText = "Record data  \n" +
            " to directory Recordings if it exist else in SdrSharp.exe directory \n" +
            " if you create directory Recordings close this window before export" +
            " You can reload file with Calc\n" +
            " name file = title window+date.txt";

            toolStripStatusLabelExport.ForeColor = this.ForeColor;
            toolStripStatusLabelExport.BackColor = this.BackColor;
            toolStripStatusLabelExport.Visible = true;
#if TOPMOST
            this.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;
            this.Padding = new System.Windows.Forms.Padding(2);  //else no resize form no cursor
            this.ResizeEnd += new System.EventHandler(this.FormDevicesListMessages_ResizeEnd);
            this.Load += new System.EventHandler(this.FormDevicesListMessages_FormLoad);
            titleBar = new Panel();
            titleBar.Height = 32;
            titleBar.Dock = DockStyle.Top;
            titleBar.BackColor = Color.White;  // Color.FromArgb(45, 45, 48);
            titleBar.MouseDown += TitleBar_MouseDown;
            // this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea; //move if maximized
            this.Controls.Add(titleBar);
            CreateButtons();
#endif
            this.ResumeLayout(true);
        }
        #region private functions
        private void ListViewListMessages_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            try
            {
                if (e.ItemIndex >= 0)
                {
                    ListViewItem lvi = cacheListMessages[e.ItemIndex];
                    if (lvi != null)
                        e.Item = lvi;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "Error fct(listViewListMessages_RetrieveVirtualItem)");
            }
        }
        //private void EnsureVisible(Int32 item)
        //{
        //    listViewListMessages.Items[item].EnsureVisible();
        //}
#endregion
#region publics functions
        internal void RefreshListMessages()
        {
            if (nbMessage > 0)
                listViewListMessages.Items[nbMessage - 1].EnsureVisible();
            this.Refresh();
        }
  
        //private Int32 maxColCurrent = 0;
        internal void SetMessages(Dictionary<String, String> listData)
        {
            //if (this.recordTxt)
            //{
            //    //if(PathAndNameFile=="")
            //    //{
            //    //    //String directory = ClassUtils.GetDirectoryRecording().Replace(" ", "_");
            //    //    PathAndNameFile = ClassFunctionsVirtualListView.ValideNameFile(DateTime.Now.ToString() + NameForm + ".txt", "_").Replace(" ", "_") + ".txt";

            //    //}
            //    if (nbMessage == 0)
            //    {
            //       //entete dans cacheListColumns
            //        //disabled export button
            //        //init entete dans text file
            //        initSerializeOK = InitSerialize(PathAndNameFile);
            //        if (initSerializeOK != "")
            //        {
            //            MessageBox.Show(initSerializeOK, "Error init export txt. File:" + PathAndNameFile.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            toolStripStatusLabelExport.ToolTipText = "Record data  \n" +
            //            " to directory Recordings if exist else in SdrSharp.exe directory \n" +
            //            " You can reload file with Calc\n" +
            //            " WARNING the file is replaced if it exists\n" +
            //            " name file = title window";
            //            toolStripStatusLabelExport.Visible = true;
            //            toolStripStatusLabelDevices.Visible = true;
            //        }
            //        else
            //        {
            //            AddColumn(listData);
            //            AddNewLine(listData, true);
            //        }
            //    }
            //    else
            //        AddNewLine(listData, true);
            //    //Save message listData
            //}
            if (cacheListColumns == null)
                return;
            String deviceName = (nbMessage + 1).ToString();
            if (nbMessage > maxMessages - 1)
                return;                    //message max row
            this.SuspendLayout();
            listViewListMessages.BeginUpdate();
            //*********add name column if necessary in listViewListMessages and in cacheListColumns****************
            //*********memorize maxColCurrent***************
            ClassFunctionsVirtualListView.AddColumn(listData, cacheListColumns, listViewListMessages, ClassConst.NBMAXCOLUMN);
            //*************************Add new line*********************************
            ListViewItem device = new ListViewItem(deviceName);
            ClassFunctionsVirtualListView.AddNewLine(listData, cacheListColumns, device);
            //**************add new line/device in cacheListMessages
            ClassFunctionsVirtualListView.AddElemToCache(cacheListMessages, firstToTop, nbMessage, device);
            //**************complete subItems for all line in cacheListMessages**********************
            ClassFunctionsVirtualListView.CompleteList(cacheListMessages, cacheListColumns.Count);
            //************************************************
            nbMessage ++;
#if !TOPMOST
            this.Text = NameForm + " (Messages received : " + nbMessage.ToString() + "/" + maxMessages.ToString() + ")";
#else
            customTxt.Text = NameForm + " (Messages received : " + nbMessage.ToString() + "/" + maxMessages.ToString() + ")";
#endif

            try   //without try:Object reference not set to an instance of an object.
            {
                listViewListMessages.VirtualListSize = nbMessage;
            }
            catch
            {
                //if(withConsole)
                //    Console.WriteLine(this.Text);
            }
            ClassFunctionsVirtualListView.ResizeAllColumns(listViewListMessages);
            //refresh();  // display last message when it is displayed at the bottom list
            listViewListMessages.EndUpdate();
            this.ResumeLayout(true);
        }
#endregion
#region Events Form
        private void ToolStripStatusLabelExport_Click(object sender, EventArgs e)
        {
            //String directory = ClassUtils.GetDirectoryRecording();
            //String fileName = ClassFunctionsVirtualListView.ValideNameFile(memoName, "_");
            if (nbMessage>0)
            { 
                if (ClassFunctionsVirtualListView.SerializeText(PathAndNameFile , cacheListColumns, cacheListMessages, true, nbMessage, false))
                     MessageBox.Show("Export--> "+ NameForm , "Export messages OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private Boolean closeByProgram = false;
        internal void CloseByProgram()
        {
            closeByProgram = true;
            this.Close();
        }
        private void FormDevicesListMessages_FormClosed(object sender, FormClosedEventArgs e)
        {
            //cacheListColumns = null;  pb with sdrsharp framework 6.0 listViewListMessages_RetrieveVirtualItem
            //cacheListMessages = null;    pb close window since framework 6 listViewListMessages_RetrieveVirtualItem
            if (!closeByProgram && nbMessage > 0)  //for foreach dictionary
                classParent.ClosingOneFormDeviceListMessages(NameForm);
            //Dispose();
            //if (str != null)
            //{
            //    str.Flush();
            //    str.Close();//close text file
            //}
            //GC.Collect();
        }
#endregion
#region SERIALIZE
        //private String initSerializeOK = "";
        private StreamWriter str;
        private NumberFormatInfo nfi = new CultureInfo(CultureInfo.CurrentUICulture.Name, false).NumberFormat;
        private String InitSerialize(String fileName)
        {
            try
            {
                Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
                str = new StreamWriter(stream);
                return "";
            }
            catch (Exception e)
            {
                //this.recordTxt = false;
                nfi = null;
                return e.Message;
            }
        }

        private void AddColumn(Dictionary<String, String> listData)
        {
            String line = String.Empty;
            foreach (KeyValuePair<String, String> _data in listData)
            {
                //if (_data.Key == listViewListMessages.Columns[0].Text)
                //    continue;
                if (_data.Key == String.Empty)
                    line += "\t";
                else
                {
                    line += _data.Key.Replace(":", "");
                    //for (Int32 i = 0; i < _data.Value.Length - _data.Key.Length; i++)
                    //    line += " ";

                    line = line.PadRight(line.Length + _data.Value.Length - _data.Key.Length);
                    //Int32 l = line.Length;
                    //if ((_data.Value.Length - _data.Key.Length) > 0)
                    //    line =  line.PadRight(_data.Value.Length); 
                    //else
                    //    line =  line.PadRight(_data.Key.Length);
                }
                line += "\t";
            }
            str.WriteLine(line);
        }
        private void AddNewLine(Dictionary<String, String> listData, Boolean formatNumber)
        {
            String line = ClassFunctionsVirtualListView.ProcessLineTxt(listData, formatNumber, nfi, cacheListColumns.Count);
            str.WriteLine(line);
        }
        #endregion
        #region TOPMOST
#if TOPMOST
        private void FormDevicesListMessages_FormLoad(object sender, EventArgs e)
        {
            ClassTopMost.moveButtons(ref customTxt, ref customBtn, ref btnMin, ref btnMax, ref btnClose, this.Width);
        }

        private void FormDevicesListMessages_ResizeEnd(object sender, EventArgs e)
        {
            ClassTopMost.moveButtons(ref customTxt, ref customBtn, ref btnMin, ref btnMax, ref btnClose, this.Width);
        }

        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ClassWin32ForTopMost.ReleaseCapture();
                ClassWin32ForTopMost.SendMessage(this.Handle, 0xA1, 0x2, 0);
            }
        }
        private void customTxt_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ClassWin32ForTopMost.ReleaseCapture();
                ClassWin32ForTopMost.SendMessage(this.Handle, 0xA1, 0x2, 0);
            }
        }
        private void CreateButtons()
        {
            //window text 
            customTxt = new Label();
            customTxt.MouseDown += customTxt_MouseDown;

            // Button topMost
            customBtn = new Button();
            customBtn.Click += (s, e) =>
            {
                if (!topMost)
                {
                    setTopMost(ClassWin32ForTopMost.HWND_TOPMOST);
                    customBtn.BackColor = Color.Cyan;
                }
                else
                {
                    setTopMost(ClassWin32ForTopMost.HWND_NOTOPMOST);
                    customBtn.BackColor = Color.Transparent;  // Color.FromArgb(70, 70, 72);
                }
                topMost = !topMost;
            };
            // Minimize
            btnMin = new Button();
            btnMin.Click += (s, e) =>
            {
                ClassWin32ForTopMost.ShowWindow(this.Handle, ClassWin32ForTopMost.SW_MINIMIZE);
            };
            // Close
            btnClose = new Button();
            btnClose.Click += (s, e) => this.Close();
            btnClose.MouseEnter += (s, e) => btnClose.BackColor = Color.FromArgb(232, 17, 35);
            btnClose.MouseLeave += (s, e) => btnClose.BackColor = Color.Transparent;
            // Maximize
            btnMax = new Button();
            btnMax.Click += (s, e) =>
            {
                if (this.WindowState == FormWindowState.Maximized)
                    ClassWin32ForTopMost.ShowWindow(this.Handle, ClassWin32ForTopMost.SW_RESTORE);
                else
                    ClassWin32ForTopMost.ShowWindow(this.Handle, ClassWin32ForTopMost.SW_MAXIMIZE);
                ClassTopMost.moveButtons(ref customTxt, ref customBtn, ref btnMin, ref btnMax, ref btnClose, this.Width);
            };
            ClassTopMost.CreateButtons(ref titleBar, ref customTxt, ref customBtn, ref btnMin, ref btnMax, ref btnClose, this.WindowState,this.Width);
       }
 
        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x84;
            const int HTLEFT = 10;
            const int HTRIGHT = 11;
            const int HTTOP = 12;
            const int HTTOPLEFT = 13;
            const int HTTOPRIGHT = 14;
            const int HTBOTTOM = 15;
            const int HTBOTTOMLEFT = 16;
            const int HTBOTTOMRIGHT = 17;
            const int RESIZE_HANDLE_SIZE = 8;
            if (m.Msg == WM_NCHITTEST)
            {
                base.WndProc(ref m);
                Point cursor = PointToClient(new Point(m.LParam.ToInt32()));
                bool left = cursor.X <= RESIZE_HANDLE_SIZE;
                bool right = cursor.X >= this.ClientSize.Width - RESIZE_HANDLE_SIZE;
                bool top = cursor.Y <= RESIZE_HANDLE_SIZE;
                bool bottom = cursor.Y >= this.ClientSize.Height - RESIZE_HANDLE_SIZE;
                if (left && top)
                {
                    m.Result = (IntPtr)HTTOPLEFT;
                    return;
                }
                else if (right && top)
                {
                    m.Result = (IntPtr)HTTOPRIGHT;
                    return;
                }
                else if (left && bottom)
                {
                    m.Result = (IntPtr)HTBOTTOMLEFT;
                    return;
                }
                else if (right && bottom)
                {
                    m.Result = (IntPtr)HTBOTTOMRIGHT;
                    return;
                }
                else if (left)
                {
                    m.Result = (IntPtr)HTLEFT;
                    return;
                }
                else if (right)
                {
                    m.Result = (IntPtr)HTRIGHT;
                    return;
                }
                else if (top)
                {
                    m.Result = (IntPtr)HTTOP;
                    return;
                }
                else if (bottom)
                {
                    m.Result = (IntPtr)HTBOTTOM;
                    return;
                }
                return;
            }
            base.WndProc(ref m);
        }
        private void setTopMost(IntPtr choose)
        {
            IntPtr hwnd = this.Handle;
            ClassWin32ForTopMost.SetWindowPos(
                hwnd,
                choose,
            0, 0, 0, 0,
            ClassWin32ForTopMost.SWP_NOMOVE | ClassWin32ForTopMost.SWP_NOSIZE);
        }
#endif
#endregion
    }
}
