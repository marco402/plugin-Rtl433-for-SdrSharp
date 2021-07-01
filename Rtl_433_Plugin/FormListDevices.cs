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
        private int _maxDevices = 0;
        private const int NBCOLUMN = 100;
        private Dictionary<String, int> cacheListColumns;
        private ListViewItem[] cacheListDevices;
        private int nbDevice = 0;
        private Rtl_433_Panel _classParent;
        public FormListDevices(Rtl_433_Panel classParent,int maxDevices)
        {
            InitializeComponent();
            _classParent = classParent;
            _maxDevices = maxDevices;
            typeof(Control).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,null, listDevices, new object[] { true });
            listDevices.View = View.Details;
            listDevices.GridLines = true;
            listDevices.FullRowSelect = true;
            listDevices.Scrollable = true;
            listDevices.MultiSelect = false;
            listDevices.HideSelection = false;
            listDevices.HeaderStyle = ColumnHeaderStyle.Clickable;
            listDevices.BackColor = System.Drawing.SystemColors.ControlDark;
            listDevices.Dock = System.Windows.Forms.DockStyle.Fill;
            listDevices.ForeColor = System.Drawing.SystemColors.ButtonFace;
            listDevices.AllowColumnReorder = false;
            listDevices.Visible = true;
            this.listDevices.VirtualMode = true;
            cacheListDevices = new ListViewItem[_maxDevices];
            cacheListColumns = new Dictionary<String, int>();
            listDevices.VirtualListSize = 0; 
            for (int i = 0; i < NBCOLUMN; i++)
            {
                listDevices.Columns.Add("");
            }
            this.Text = "Devices received : 0";
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
            for (int row = 0; row < _maxDevices; row++)
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
    //    private void FormDevices_FormClosing(object sender, FormClosingEventArgs e)
    //    {

    //}
        protected override void OnClosed(EventArgs e)
        {
             _classParent.closingFormListDevice();
             cacheListColumns=null;
             cacheListDevices=null;
             nbDevice = 0;
        }
        private void autoResizeColumns(ListView lv, int nbColumn)
        {
            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            ListView.ColumnHeaderCollection cc = lv.Columns;
            //for (int i = 0; i < cc.Count; i++)
            for(int col=0; col < nbColumn; col++)
            {
                int colWidth = TextRenderer.MeasureText(cc[col].Text, lv.Font).Width + 10;
                if (colWidth > cc[col].Width)
                {
                    cc[col].Width = colWidth;
                }
            }
        }
        //private void autoResizeColumns(ListView lv, SortedDictionary<int, string>indexColForThisDevice)
        //{
        //    lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        //    ListView.ColumnHeaderCollection cc = lv.Columns;
        //    //for (int i = 0; i < cc.Count; i++)
        //    foreach (KeyValuePair<int, string> indexCol in indexColForThisDevice)
        //    {
        //        int colWidth = TextRenderer.MeasureText(cc[indexCol.Key].Text, lv.Font).Width + 10;
        //        if (colWidth > cc[indexCol.Key].Width)
        //        {
        //            cc[indexCol.Key].Width = colWidth;
        //        }
        //    }
        //}
       private void autoResizeAllColumns(ListView lv)
        {
            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            ListView.ColumnHeaderCollection cc = lv.Columns;
            for (int i = 0; i < cc.Count; i++)
            {
                int colWidth = TextRenderer.MeasureText(cc[i].Text, lv.Font).Width + 10;
                if (colWidth > cc[i].Width)
                {
                    cc[i].Width = colWidth;
                }
            }
        }
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
            try {
            using (Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                StreamWriter str = new StreamWriter(stream);
                string line = string.Empty;
                foreach (KeyValuePair<string, int> _data in cacheListColumns)
                {
                    if (_data.Key == string.Empty)
                        line += "\t";
                    else
                        line += _data.Key;
                    line += "\t";
                }
                str.WriteLine(line);
                foreach (ListViewItem it in cacheListDevices)
                {
                    if (it == null)
                        break;
                    line = string.Empty;
                    foreach (ListViewItem.ListViewSubItem sit in it.SubItems)
                    {
                        if (sit.Text == string.Empty)
                            line += "\t";
                        else
                        {
                            line += sit.Text;
                            line += "\t";
                        }
                    }
                    str.WriteLine(line);
                }
                str.Close();
            }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error export devices fct(serializeText).File:" + fileName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void deSerializeText(string fileName)
        {
            int dev = 0;
            int col = 0;
            try
            {
                using (Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    StreamReader str = new StreamReader(stream);
                    string line = string.Empty;
                    {
                        listDevices.BeginUpdate();
                        line =str.ReadLine();
                        string[] words = line.Split('\t');
                        col = 0;
                        foreach (string word in words)
                        {
                            listDevices.Columns[col].Text = word;
                            if (word.Trim() == string.Empty)
                                break;
                            cacheListColumns.Add(word, col + 1);
                            col += 1;
                            if (col >NBCOLUMN)
                            {
                                MessageBox.Show("Maximum of column reached("+ NBCOLUMN.ToString()+")", "Import devices File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }
                        }
                        ListViewItem device = null;
                        while (str.Peek() >= 0)
                        {
                            line = str.ReadLine();
                            words = line.Split('\t');
                            bool initDevice = false;
                            foreach (string word in words)
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
                            if(dev > (_maxDevices-1))
                            {
                                MessageBox.Show("Maximum of device reached(" + _maxDevices.ToString() + ")", "Import devices File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }
                        }
                        listDevices.VirtualListSize = dev;
                        nbDevice = dev;
                        //listDevices.EndUpdate();
                    }
                    str.Close();
                }
                autoResizeAllColumns(listDevices);
                this.Text = "Devices received : " + nbDevice.ToString() + "/" + _maxDevices.ToString() + " Column:" + col.ToString() + " / " + NBCOLUMN.ToString();
                listDevices.EndUpdate();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + " col:" + col.ToString() + " dev:" + dev.ToString(), "Error import devices fct(deSerializeText).File:"+ fileName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void setInfoDevice(Dictionary<String, String> listData)
        {
            string deviceName = _classParent.getDeviceName(listData);
            if (deviceName == string.Empty)
                return;
            listDevices.BeginUpdate();
            int indexColonne=0;
            //add column device if necessary
            cacheListColumns.TryGetValue("Device", out indexColonne);
            if (indexColonne == 0)
            {
                listDevices.Columns[cacheListColumns.Count].Text = "Device";
                cacheListColumns.Add("Device", cacheListColumns.Count+1);
            }
            //add column if necessary
            foreach (KeyValuePair<string, string> _data in listData)
            {
                cacheListColumns.TryGetValue(_data.Key, out indexColonne);
                if (cacheListColumns.Count >= NBCOLUMN)
                {
                    listDevices.EndUpdate();
                    return;                     //message max dk
                }
                if (indexColonne == 0)
                {
                    listDevices.Columns[cacheListColumns.Count].Text = _data.Key;
                    cacheListColumns.Add(_data.Key, cacheListColumns.Count+1);
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
            SortedDictionary<int, string> indexCol = new SortedDictionary<int, string>();
            foreach (KeyValuePair<string, string> _data in listData)
            {
                // if (_data.Key != "device")
                //{
                    cacheListColumns.TryGetValue(_data.Key, out indexColonne);
                    indexCol.Add(indexColonne,_data.Value);
                //}
            }
            if (!find)
            {
                if (nbDevice > _maxDevices - 1)
                {
                    listDevices.EndUpdate();
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
                for (i = i; i < NBCOLUMN; i++)
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
                    device.SubItems[_data.Key-1].Text = _data.Value;
                }
            }
            this.Text = "Devices received : " + nbDevice.ToString() + "/" + _maxDevices.ToString() + " Column:" + cacheListColumns.Count.ToString() +" / " +NBCOLUMN.ToString();
            listDevices.VirtualListSize = nbDevice;
            autoResizeColumns(listDevices, cacheListColumns.Count);
           // autoResizeAllColumns(listDevices);
            listDevices.EndUpdate();
         }
#endregion
    }
}


