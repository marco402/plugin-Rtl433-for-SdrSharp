using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDRSharp.Rtl_433
{
    public partial class FormDevicesListMessages : Form
    {
        private int maxMessages = 0;
        private Dictionary<String, int> cacheListColumns;
        private ListViewItem[] cacheListDevices;
        private int nbMessage = 0;
        private Rtl_433_Panel classParent;
        int nbColumn = 0;
        public FormDevicesListMessages(Rtl_433_Panel classParent, int maxDevices,int nbColumn)
        {
            this.nbColumn = nbColumn;
            InitializeComponent();
            this.classParent = classParent;
            this.maxMessages = maxDevices;
            typeof(Control).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, listViewListMessages, new object[] { true });

            ClassFunctionsListView.initListView(listViewListMessages, nbColumn);
            cacheListDevices = new ListViewItem[this.maxMessages];
            cacheListColumns = new Dictionary<String, int>();
            this.Text = "Messages received : 0";
        }
        protected override void OnClosed(EventArgs e)
        {
            classParent.closingFormListDevice();
            cacheListColumns = null;
            cacheListDevices = null;
            nbMessage = 0;
        }
        #region private functions
        private void listDevices_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
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
                MessageBox.Show(ex.Message, "Error fct(listDevices_RetrieveVirtualItem)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private int FindIndexIfDeviceExist(string device)
        {
            for (int row = 0; row < maxMessages; row++)
            {
                ListViewItem lvi = cacheListDevices[row];
                if (lvi != null && lvi.Text == device)
                    return row;
            }
            return -1;
        }
        private void EnsureVisible(int item)
        {
            listViewListMessages.Items[item].EnsureVisible();
        }
        #endregion
        #region publics functions
        public void refresh()
        {
            if (nbMessage > 0)
                listViewListMessages.Items[nbMessage - 1].EnsureVisible();
            this.Refresh();
        }
        public void serializeText(string fileName)
        {
            ClassFunctionsListView.serializeText(fileName, cacheListColumns, cacheListDevices);
        }
        public void setMessages(Dictionary<String, String> listData)
        {
            string deviceName = nbMessage.ToString();  // classParent.getDeviceName(listData);
            //if (deviceName == string.Empty)
            //    return;
            listViewListMessages.BeginUpdate();
            int indexColonne = 0;
            //add column device if necessary
            //cacheListColumns.TryGetValue("Device", out indexColonne);
            //if (indexColonne == 0)
            //{
            //    listViewListMessages.Columns[cacheListColumns.Count].Text = "Device";
            //    cacheListColumns.Add("Device", cacheListColumns.Count + 1);
            //}
            //add column if necessary
            foreach (KeyValuePair<string, string> _data in listData)
            {
                cacheListColumns.TryGetValue(_data.Key, out indexColonne);
                if (cacheListColumns.Count >= nbColumn)
                {
                    listViewListMessages.EndUpdate();
                    return;                     //message max dk
                }
                if (indexColonne == 0)
                {
                    listViewListMessages.Columns[cacheListColumns.Count].Text = _data.Key;
                    cacheListColumns.Add(_data.Key, cacheListColumns.Count + 1);
                }
            }
            //refresh or new device
            ListViewItem device = null;
            //bool find = false;
            //foreach (ListViewItem item in cacheListDevices)
            //{
            //    if (item == null)
            //        break;
            //    if (item.Text == deviceName)
            //    {
            //        find = true;
            //        device = item;
            //        break;
            //    }
            //}
            SortedDictionary<int, string> indexCol = new SortedDictionary<int, string>();
            foreach (KeyValuePair<string, string> _data in listData)
            {
                cacheListColumns.TryGetValue(_data.Key, out indexColonne);
                indexCol.Add(indexColonne, _data.Value);
            }
            //if (!find)
            //{
                if (nbMessage > maxMessages - 1)
                {
                    listViewListMessages.EndUpdate();
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
                for (i = i; i < nbColumn; i++)
                {
                    device.SubItems.Add("");
                }
                //************************************************
                cacheListDevices[nbMessage] = device;
                nbMessage += 1;
            //}

            //else   //refresh device
            //{
            //    foreach (KeyValuePair<int, string> _data in indexCol)
            //    {
            //        device.SubItems[_data.Key - 1].Text = _data.Value;
            //    }
            //}
            this.Text = "Messages received : " + nbMessage.ToString() + "/" + maxMessages.ToString() ;
            listViewListMessages.VirtualListSize = nbMessage;
            ClassFunctionsListView.autoResizeColumns(listViewListMessages, cacheListColumns.Count);
            listViewListMessages.EndUpdate();
        }
        #endregion
    }
}
