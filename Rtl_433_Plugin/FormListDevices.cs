/* Written by Marc Prieur (marco40_github@sfr.fr)
                                FormDevices.cs 
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
//using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
namespace SDRSharp.Rtl_433
{
    internal partial class FormListDevices : Form
    {
        private Int32 maxDevices = 0;
        private Int32 maxColumns = 0;
        private Dictionary<String, Int32> cacheListColumns;
        private ListViewItem[] cacheListDevices;
        private Int32 nbDevice = 0;
        private Rtl_433_Panel classParent;
        private Boolean firstToTop = false;
        private Int32 maxColCurrent = 0;
#region events form
#endregion
#region private functions
        private void listDevices_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            try {
                if (e.ItemIndex >= 0)
                {
                    ListViewItem lvi = cacheListDevices[e.ItemIndex];
                    if (lvi != null)
                        e.Item = lvi;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error fct(listDevices_RetrieveVirtualItem)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private Int32 FindIndexIfDeviceExist(String device)
        {
            for (Int32 row = 0; row < maxDevices; row++)
            {
                ListViewItem lvi = cacheListDevices[row];
                if (lvi != null && lvi.Text == device)
                    return row;
            }
            return -1;
        }
        private void EnsureVisible(Int32 item)
        {
            listViewDevices.Items[item].EnsureVisible();
        }
        #endregion
        #region publics functions
        internal FormListDevices(Rtl_433_Panel classParent, Int32 maxDevices, Int32 maxColumns)
        {
            InitializeComponent();
            this.classParent = classParent;
            this.maxDevices = maxDevices;
            this.maxColumns = maxColumns;
            this.SuspendLayout();
            this.MinimumSize = new System.Drawing.Size(0, 100); //if only title crash on listViewDevices.VirtualListSize = nbMessage;
            typeof(Control).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, listViewDevices, new object[] { true });
            ClassFunctionsVirtualListView.initListView(listViewDevices);
            cacheListDevices = new ListViewItem[this.maxDevices];
            cacheListColumns = new Dictionary<String, Int32>();
            this.Text = "Devices received : 0";
            this.ResumeLayout(true);
            //stopw = new Stopwatch();
        }
        internal void refresh()
        {
            if (nbDevice > 0)
                listViewDevices.Items[nbDevice - 1].EnsureVisible();
            this.Refresh();
        }
        internal void serializeText(String fileName)
        {
            ClassFunctionsVirtualListView.serializeText(fileName,cacheListColumns,cacheListDevices,false,nbDevice,true);
        }
        internal void deSerializeText(String fileName)
        {
            Cursor.Current = Cursors.WaitCursor;
            firstToTop = !firstToTop;
            this.SuspendLayout();
            listViewDevices.BeginUpdate();
            Dictionary<String, String> listData = new Dictionary<String, String>();
            Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
            try
            {
               using (StreamReader str = new StreamReader(stream))
               {
                    //**************************init column title****************************
                    String line = str.ReadLine();
                    if (line == null)
                    {
                        MessageBox.Show("File devices.txt empty", "Import devices File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        listViewDevices.EndUpdate();
                        this.ResumeLayout(true);
                        firstToTop = !firstToTop;
                        Cursor.Current = Cursors.Default;
                        return;
                    }
                    String[] wordsTitleCol = line.Split('\t');
                    for (Int32 i = 2; i < wordsTitleCol.Length - 1; i++)  //start=1 no device
                      if (wordsTitleCol[i - 1].Length > 0)
                        listData.Add(wordsTitleCol[i - 1], "");
                      //***********************transfer devices*********************************
                    String[] wordsData = line.Split('\t');
                    while (str.Peek() >= 0)
                    {
                        listData.Clear();
                        line = str.ReadLine();
                        wordsData = line.Split('\t');
                        Int32 indice = 0;
                        foreach (String word in wordsTitleCol)
                        {
                            if (!wordsTitleCol[indice].Equals("device") && !wordsTitleCol[indice].Equals("Device") &&
                                 !wordsTitleCol[indice].Equals("N mes.") && wordsTitleCol[indice].Length>0)
                                    listData.Add(word, wordsData[indice]);
                            indice += 1;
                        }
                        _setInfoDevice(listData); 
                     }
                    //*****************************************************************************
                    str.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error import devices fct(deSerializeText).File:" + fileName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            listViewDevices.EndUpdate();
            this.ResumeLayout(true);
            firstToTop = !firstToTop;
            Cursor.Current = Cursors.Default;
            listData.Clear();
            listData = null;
        }
        /// <summary>
        /// for deserialize
        /// </summary>
        /// <param name="listData"></param>
        internal void setInfoDevice(Dictionary<String, String> listData)
        {
            this.SuspendLayout();
            listViewDevices.BeginUpdate();
            _setInfoDevice(listData);
            listViewDevices.EndUpdate();
            this.ResumeLayout(true);
        }
        internal void _setInfoDevice(Dictionary<String, String> listData)
        {
            this.SuspendLayout();
            if (cacheListColumns == null)
                return;
            String deviceName = classParent.getDeviceName(listData);
            if (deviceName == String.Empty)
            {
                return;
            }
            maxColCurrent = ClassFunctionsVirtualListView.addOneColumn("Device", cacheListColumns, listViewDevices, maxColCurrent);
            //**********************add column nb mes if necessary***********************************
            maxColCurrent = ClassFunctionsVirtualListView.addOneColumn("N mes.", cacheListColumns, listViewDevices, maxColCurrent);
            //***********************add other column if necessary*************************************
            maxColCurrent = ClassFunctionsVirtualListView.addColumn(listData, cacheListColumns, listViewDevices, maxColCurrent);
            if (cacheListColumns.Count >= maxColumns)
                 return; 
             //**********************search device******************************
            ListViewItem device =  ClassFunctionsVirtualListView.getItem(deviceName, cacheListDevices);
            //**************************new device***************************
            if (device==null)
            {
                if (nbDevice > maxDevices - 1)
                       return;
                device = new ListViewItem(deviceName);
                ClassFunctionsVirtualListView.addNewLine(listData, cacheListColumns, device);
                //**************add new line/device in cacheListMessages
                ClassFunctionsVirtualListView.addElemToCache(cacheListDevices, firstToTop, nbDevice, device);
                //**************complete subItems for all line in cacheListMessages**********************
                ClassFunctionsVirtualListView.completeList(cacheListDevices, maxColCurrent);
                //************************************************
                nbDevice += 1;
                device.SubItems[1].Text = "1";
            }
            //**************************refresh device***************************
            else
            {
                ClassFunctionsVirtualListView.refreshLine(listData, cacheListColumns, device, maxColCurrent);
                device.SubItems[1].Text = (Int32.Parse(device.SubItems[1].Text) + 1).ToString();
                //**************complete subItems for all line in cacheListMessages**********************
                ClassFunctionsVirtualListView.completeList(cacheListDevices, maxColCurrent);
            }
 
            //**************************************************************************************
            this.Text = "Devices received : " + nbDevice.ToString() + "/" + maxDevices.ToString() + " Column:" + cacheListColumns.Count.ToString() +" / " +maxColumns.ToString();
            listViewDevices.VirtualListSize = nbDevice;
            ClassFunctionsVirtualListView.resizeAllColumns(listViewDevices);
            //**************************************************************************************
            this.ResumeLayout(true);
        }
        #endregion
        private void FormListDevices_FormClosed(object sender, FormClosedEventArgs e)
        {
            classParent.closingFormListDevice();
            //cacheListColumns = null;    pb with framework 6.0
            //cacheListDevices = null;
            //nbDevice = 0;
        }
    }
}


