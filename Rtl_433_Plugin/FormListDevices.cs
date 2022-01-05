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
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
namespace SDRSharp.Rtl_433
{
    public partial class FormListDevices : Form
    {
        private int maxDevices = 0;
        private int maxColumns = 0;
        private Dictionary<String, int> cacheListColumns;
        private ListViewItem[] cacheListDevices;
        private int nbDevice = 0;
        private Rtl_433_Panel classParent;
        public FormListDevices(Rtl_433_Panel classParent,int maxDevices,int maxColumns)
        {
            InitializeComponent();
            this.classParent = classParent;
            this.maxDevices = maxDevices;
            this.maxColumns = maxColumns;
            typeof(Control).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,null, listDevices, new object[] { true });
            ClassFunctionsListView.initListView(listDevices, this.maxColumns);
            cacheListDevices = new ListViewItem[this.maxDevices];
            cacheListColumns = new Dictionary<String, int>();
            this.Text = "Devices received : 0";
        }
        protected override void OnClosed(EventArgs e)
        {
             classParent.closingFormListDevice();
             cacheListColumns=null;
             cacheListDevices=null;
             nbDevice = 0;
        }
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
        private int FindIndexIfDeviceExist(string device)
        {
            for (int row = 0; row < maxDevices; row++)
            {
                ListViewItem lvi = cacheListDevices[row];
                if (lvi != null && lvi.Text == device)
                    return row;
            }
            return -1;
        }
        private void EnsureVisible(int item)
        {
            listDevices.Items[item].EnsureVisible();
        }
 
        //private void autoResizeColumns(ListView lv, int nbColumn)
        //{
        //    lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        //    ListView.ColumnHeaderCollection cc = lv.Columns;
        //    for(int col=0; col < nbColumn; col++)
        //    {
        //        int colWidth = TextRenderer.MeasureText(cc[col].Text, lv.Font).Width + 10;
        //        if (colWidth > cc[col].Width)
        //        {
        //            cc[col].Width = colWidth;
        //        }
        //    }
        //}
        //private void autoResizeAllColumns(ListView lv)
        //{
        //    lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        //    ListView.ColumnHeaderCollection cc = lv.Columns;
        //    for (int i = 0; i < cc.Count; i++)
        //    {
        //        int colWidth = TextRenderer.MeasureText(cc[i].Text, lv.Font).Width + 10;
        //        if (colWidth > cc[i].Width)
        //        {
        //            cc[i].Width = colWidth;
        //        }
        //    }
        //}
#endregion
        #region publics functions
        public void refresh()
        {
            if (nbDevice > 0)
                listDevices.Items[nbDevice - 1].EnsureVisible();
            this.Refresh();
        }
        public void serializeText(string fileName)
        {
            ClassFunctionsListView.serializeText(fileName,cacheListColumns,cacheListDevices,false,nbDevice,true);
        }
        public void deSerializeText(string fileName)
        {
            int dev = 0;
            int col = 0;
            Boolean oldVersion = false;
            try
            {
                using (Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    StreamReader str = new StreamReader(stream);
                    string line = string.Empty;
                    {
                        listDevices.BeginUpdate();
                        line = str.ReadLine();
                        if (line == null)
                        {
                            MessageBox.Show("File devices.txt empty", "Import devices File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            listDevices.EndUpdate();
                            return;
                        }
                        col = 0;
                        string[] words = line.Split('\t');
                        string[] wordsWithNbMes = new string[words.Length - 1];
                        if (words[1] != "N mes.")  //old version
                        {
                            oldVersion = true;
                            wordsWithNbMes[0] = words[0];
                            wordsWithNbMes[1] = "N mes.";
                            for (int i = 2; i < words.Length - 1; i++)
                                wordsWithNbMes[i] = words[i - 1];
                        }
                        else
                            wordsWithNbMes = words;
           
                            foreach (string word in wordsWithNbMes)
                        {
                             listDevices.Columns[col].Text = word;
                            if (word.Trim() == string.Empty)
                                break;
                            cacheListColumns.Add(word, col + 1);
                            col += 1;
                            
                            //if (word.Contains("device"))
                            //{
                            //    if (wordsWithNbMes[1] != "N mes.")  //old version
                            //    {
                            //        listDevices.Columns[col].Text = "N mes.";
                            //        cacheListColumns.Add("N mes.", col + 1);
                            //        col += 1;
                            //    }
                            //}
                            if (col >maxColumns)
                            {
                                MessageBox.Show("Maximum of column reached("+ maxColumns.ToString()+")", "Import devices File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }
                        }
                        ListViewItem device = null;
                        while (str.Peek() >= 0)
                        {
                            line = str.ReadLine();
                            words = line.Split('\t');
                            
                            string[] wordsWithNbMes1 = new string[words.Length - 1];
                            if (oldVersion == true)  //old version
                            {
                                wordsWithNbMes1[0] = words[0];
                                wordsWithNbMes1[1] = "0";
                                for (int i = 2; i < words.Length - 1; i++)
                                    wordsWithNbMes1[i] = words[i - 1];
                            }
                            else
                                wordsWithNbMes1 = words;
                            bool initDevice = false;

                            foreach (string word in wordsWithNbMes1)
                            {
                                if(initDevice == false)
                                {
                                    device = new ListViewItem(word);
                                    initDevice = true;
                                }
                                else
                                    device.SubItems.Add(word);
                             }
                            cacheListDevices[dev] = device;
                            dev += 1;
                            if(dev > (maxDevices-1))
                            {
                                MessageBox.Show("Maximum of device reached(" + maxDevices.ToString() + ")", "Import devices File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }
                        }
                        listDevices.VirtualListSize = dev;
                        nbDevice = dev;
                        }
                    str.Close();
                }
                ClassFunctionsListView.autoResizeAllColumns(listDevices);
                this.Text = "Devices received : " + nbDevice.ToString() + "/" + maxDevices.ToString() + " Column:" + col.ToString() + " / " + maxColumns.ToString();
                listDevices.EndUpdate();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + " col:" + col.ToString() + " dev:" + dev.ToString(), "Error import devices fct(deSerializeText).File:"+ fileName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                listDevices.EndUpdate();
            }
        }
        public void setInfoDevice(Dictionary<String, String> listData)
        {
            if (cacheListColumns == null)
                return;
            string deviceName = classParent.getDeviceName(listData);
            if (deviceName == string.Empty)
            {
                return;
            }
            this.SuspendLayout();
            listDevices.BeginUpdate();
            int indexColonne=0;
            //add column device if necessary
            cacheListColumns.TryGetValue("Device", out indexColonne);
            if (indexColonne == 0)
            {
                listDevices.Columns[cacheListColumns.Count].Text = "Device";
                cacheListColumns.Add("Device", cacheListColumns.Count+1);
            }
            //add column nb mes if necessary
            cacheListColumns.TryGetValue("N mes.", out indexColonne);
            if (indexColonne == 0)
            {
                listDevices.Columns[cacheListColumns.Count].Text = "N mes.";
                cacheListColumns.Add("N mes.", cacheListColumns.Count + 1);
            }
            //add column if necessary
            //testColumn(new KeyValuePair<string, string>("N mes.", "20"));
            //add other column if necessary
            foreach (KeyValuePair<string, string> _data in listData)
            {
                cacheListColumns.TryGetValue(_data.Key, out indexColonne);
                if (cacheListColumns.Count >= maxColumns)
                {
                    listDevices.EndUpdate();
                    this.ResumeLayout();
                    return;                     //message max dk
                }
                if (indexColonne == 0)
                {
                    listDevices.Columns[cacheListColumns.Count].Text = _data.Key;
                    cacheListColumns.Add(_data.Key, cacheListColumns.Count + 1);
                }
            }
            //refresh or new device
            ListViewItem device=null;
            bool find = false;
            foreach (ListViewItem item in cacheListDevices)
            {
                if (item == null)
                    break;
                if (item.Text == deviceName)
                {
                    find = true;
                    device = item;
                    break;
                }
            }
            //device.SubItems.Add(indexCol.ElementAt(index).Value);
            Int32 countMessage = 1;
            SortedDictionary<int, string> indexCol = new SortedDictionary<int, string>();

            indexCol.Add(2, countMessage.ToString());

            foreach (KeyValuePair<string, string> _data in listData)
            {
                cacheListColumns.TryGetValue(_data.Key, out indexColonne);
                indexCol.Add(indexColonne,_data.Value);
            }
            if (!find)
            {
                if (nbDevice > maxDevices - 1)
                {
                    listDevices.EndUpdate();
                    this.ResumeLayout();
                    return;                    //message max row
                }
                device = new ListViewItem(deviceName);
                int index = 0;
                int i = 0;
                for (i = 0; i < indexCol.ElementAt(indexCol.Count - 1).Key - 1; i++)
                {
                    for (i = i; i < (indexCol.ElementAt(index).Key - 2); i++)
                    {
                        device.SubItems.Add("");
                    }
                    device.SubItems.Add(indexCol.ElementAt(index).Value);
                    index += 1;
                }
                for (i = i; i < maxColumns; i++)
                {
                    device.SubItems.Add("");
                }
                 //************************************************
                cacheListDevices[nbDevice] = device;
                nbDevice += 1;
            }

            else   //refresh device
            {
                foreach (KeyValuePair<int, string> _data in indexCol)
                {

                    if(_data.Key!=2)
                        device.SubItems[_data.Key-1].Text = _data.Value;
                    else
                    {
                        countMessage = Int32.Parse(device.SubItems[_data.Key - 1].Text);
                        countMessage += 1;
                        device.SubItems[1].Text = countMessage.ToString();
                    }
                }
            }
            this.Text = "Devices received : " + nbDevice.ToString() + "/" + maxDevices.ToString() + " Column:" + cacheListColumns.Count.ToString() +" / " +maxColumns.ToString();
            listDevices.VirtualListSize = nbDevice;
            ClassFunctionsListView.autoResizeColumns(listDevices, cacheListColumns.Count);
            listDevices.EndUpdate();
            this.ResumeLayout();
        }
 #endregion
    }
}


