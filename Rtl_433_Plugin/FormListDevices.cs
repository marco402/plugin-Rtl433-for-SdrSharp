/* Written by Marc Prieur (marco40_github@sfr.fr)
                                FormListDevices.cs 
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
using System.IO;
using System.Reflection;
using System.Windows.Forms;
namespace SDRSharp.Rtl_433
{
    internal partial class FormListDevices : Form
    {
        private readonly Int32 maxDevices = 0;
        private readonly Int32 maxColumns = 0;
        private readonly Dictionary<String, Int32> cacheListColumns;
        private readonly ListViewItem[] cacheListDevices;
        private Int32 nbDevice = 0;
        private readonly ClassFormDevicesList myClassFormDevicesList;
        private Boolean firstToTop = false;
        #region private functions
        private void ListDevices_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            try
            {
                if (e.ItemIndex >= 0)
                {
                    ListViewItem lvi = cacheListDevices[e.ItemIndex];
                    if (lvi != null)
                        e.Item = lvi;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "Error fct(listDevices_RetrieveVirtualItem)");
            }
        }
 
        #endregion
        #region publics functions
        internal FormListDevices(ClassFormDevicesList myClassFormDevicesList)
        {
            InitializeComponent();
            this.myClassFormDevicesList = myClassFormDevicesList;
            this.maxDevices = ClassUtils.MaxDevicesWindows*10;
            this.maxColumns = ClassConst.NBMAXCOLUMN;
            this.Font = ClassUtils.Font;
            this.BackColor = ClassUtils.BackColor;
            this.ForeColor = ClassUtils.ForeColor;
            this.Cursor = ClassUtils.Cursor;
            listViewDevices.BackColor = this.BackColor;   //pb ambient property ???
            listViewDevices.ForeColor = this.ForeColor;
            listViewDevices.Font = this.Font;
            listViewDevices.Cursor = this.Cursor;
            this.SuspendLayout();
            this.MinimumSize = new System.Drawing.Size(0, 100); //if only title crash on listViewDevices.VirtualListSize = nbMessage;
            typeof(Control).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, listViewDevices, new object[] { true });
            ClassFunctionsVirtualListView.InitListView(listViewDevices);

            cacheListDevices = new ListViewItem[this.maxDevices];
            cacheListColumns = new Dictionary<String, Int32>();
            this.Text = "Devices received : 0";
            this.ResumeLayout(true);
        }
        internal void RefreshListDevices()
        {
            if (nbDevice > 0)
                listViewDevices.Items[nbDevice - 1].EnsureVisible();
            this.Refresh();
        }

        internal void DeSerializeText(String fileName)
        {
            Cursor.Current = Cursors.WaitCursor;
            firstToTop = !firstToTop;
            this.SuspendLayout();
            listViewDevices.BeginUpdate();
            Dictionary<String, String> listData = new Dictionary<String, String>();
            try
            {
                Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
                using (StreamReader str = new StreamReader(stream))
                {
                    //**************************init column title****************************
                    String line = str.ReadLine();
                    if (line == null)
                    {
                        MessageBox.Show("File " + ClassConst.FILELISTEDEVICES + " empty", "Import devices File", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                                 !wordsTitleCol[indice].Equals("N mes.") && wordsTitleCol[indice].Length > 0)
                                listData.Add(word, wordsData[indice]);
                            indice ++;
                        }
                        SetInfoDevice(listData);
                    }
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
        }
        /// <summary>
        /// for deserialize
        /// </summary>
        /// <param name="listData"></param>
        internal Boolean SetInfoDeviceListDevices(Dictionary<String, String> listData)
        {
             this.SuspendLayout();
            listViewDevices.BeginUpdate();
            this.SetInfoDevice(listData);
            listViewDevices.EndUpdate();
            this.ResumeLayout(true);
            return true;
        }
        private void SetInfoDevice(Dictionary<String, String> listData)
        {
            this.SuspendLayout();
            if (cacheListColumns == null)
                return;
            String deviceName = ClassUtils.GetDeviceName(listData);
            if (deviceName == String.Empty)
            {
                return;
            }

            ClassFunctionsVirtualListView.AddOneColumn("Device", cacheListColumns, listViewDevices, maxColumns);
            //**********************add column nb mes if necessary***********************************
            ClassFunctionsVirtualListView.AddOneColumn("N mes.", cacheListColumns, listViewDevices, maxColumns);
            //***********************add other column if necessary*************************************
            ClassFunctionsVirtualListView.AddColumn(listData, cacheListColumns, listViewDevices, maxColumns);
            //if (cacheListColumns.Count >= maxColumns)
            //    return;
            //**********************search device******************************
            ListViewItem device = ClassFunctionsVirtualListView.GetItem(deviceName, cacheListDevices);
            //**************************new device***************************
            if (device == null)
            {
                if (nbDevice > maxDevices - 1)
                    return;
                device = new ListViewItem(deviceName);
                ClassFunctionsVirtualListView.AddNewLine(listData, cacheListColumns, device);
                //**************add new line/device in cacheListMessages
                ClassFunctionsVirtualListView.AddElemToCache(cacheListDevices, firstToTop, nbDevice, device);
                //**************complete subItems for all line in cacheListMessages**********************
                ClassFunctionsVirtualListView.CompleteList(cacheListDevices, cacheListColumns.Count);
                //************************************************
                nbDevice ++;
                device.SubItems[1].Text = "1";
            }
            //**************************refresh device***************************
            else
            {
                ClassFunctionsVirtualListView.RefreshLine(listData, cacheListColumns, device); //ajoute voir  ajoute des elements a une ligne existante
                device.SubItems[1].Text = (Int32.Parse(device.SubItems[1].Text) + 1).ToString();
                //**************complete subItems for all line in cacheListMessages**********************
                ClassFunctionsVirtualListView.CompleteList(cacheListDevices, cacheListColumns.Count);
            }
            //**************************************************************************************
            this.Text = "Devices received : " + nbDevice.ToString() + "/" + maxDevices.ToString() + " Column:" + cacheListColumns.Count.ToString() + "/" + maxColumns.ToString();
            listViewDevices.VirtualListSize = nbDevice;
            ClassFunctionsVirtualListView.ResizeAllColumns(listViewDevices);
            //**************************************************************************************
            this.ResumeLayout(true);
        }
        #endregion

        private void FormListDevices_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (myClassFormDevicesList.PluginIsRun && myClassFormDevicesList.ChooseFormListDevice)
            {
                MessageBox.Show("Stop plugin or change form type before close.", "Close form list devices ", MessageBoxButtons.OK);
                e.Cancel = true;
            }
            else
            {
                if (MessageBox.Show("Do you want export devices list( " + ClassConst.FILELISTEDEVICES + " )", "Export devices list", MessageBoxButtons.YesNo, MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    ClassFunctionsVirtualListView.SerializeText(myClassFormDevicesList.GetDirectoryForSaveDevicesList(), cacheListColumns, cacheListDevices, false, nbDevice, true);
                myClassFormDevicesList.SetFormDevicesListCloseByUser=true;
            }
        }
    }
}


