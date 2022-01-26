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
using System.Reflection;
using System.Windows.Forms;
namespace SDRSharp.Rtl_433
{
    public partial class FormDevicesListMessages : Form
    {
        private ClassInterfaceWithRtl433 classInterfaceWithRtl433;
        private string memoName = "";
        private int maxMessages = 0;
        private Dictionary<String, int> cacheListColumns;
        private ListViewItem[] cacheListMessages;
        private int nbMessage = 0;
        private Rtl_433_Panel classParent;
		private bool firstToTop = false;
        public FormDevicesListMessages(Rtl_433_Panel classParent, int maxDevices,string name, ClassInterfaceWithRtl433 classInterfaceWithRtl433)
        {
            this.classInterfaceWithRtl433 = classInterfaceWithRtl433;
            InitializeComponent();
            this.classParent = classParent;
            this.maxMessages = maxDevices;
            typeof(Control).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, listViewListMessages, new object[] { true });

            ClassFunctionsVirtualListView.initListView(listViewListMessages);
            cacheListMessages = new ListViewItem[this.maxMessages]; 
            cacheListColumns = new Dictionary<String, int>();
            cacheListColumns.Add("N° Mes.",  1);
            listViewListMessages.Columns.Add("");
            listViewListMessages.Columns[0].Text = "N° Mes.";
            memoName = name;
            this.Text = name + " (Messages received : 0)";
            statusStripExport.ShowItemToolTips=true;
            toolStripStatusLabelExport.ToolTipText = "Record data  \n" +
                " to directory Recordings if exist else in SdrSharp.exe directory \n" +
                " You can reload file with Calc\n" +
                " WARNING the file is replaced if it exists\n" +
                " name file = title window";
         }
        //protected override void OnClosed(EventArgs e)
        //protected override void OnClosed(EventArgs e)
        //{
        //    classParent.closingFormListDevice();
        //    cacheListColumns = null;
        //    cacheListDevices = null;
        //    nbMessage = 0;
        //}
        #region private functions
        private void listViewListMessages_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            //if (cacheListMessages != null)  not enough
            //{
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
                    MessageBox.Show(ex.Message, "Error fct(listViewListMessages_RetrieveVirtualItem)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            //}
        }
        //private int FindIndexIfDeviceExist(string device)
        //{
        //    for (int row = 0; row < maxMessages; row++)
        //    {
        //        ListViewItem lvi = cacheListMessages[row];
        //        if (lvi != null && lvi.Text == device)
        //            return row;
        //    }
        //    return -1;
        //}
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

        private int maxColCurrent = 0;
        public void setMessages(Dictionary<String, String> listData)
        {
            if (cacheListColumns == null)
                return;
            
            string deviceName = (nbMessage+1).ToString();
            if (nbMessage > maxMessages - 1)
                return;                    //message max row
            this.SuspendLayout();
            listViewListMessages.BeginUpdate();
            //*********add name column if necessary in listViewListMessages and in cacheListColumns****************
            //*********memorize maxColCurrent***************
            maxColCurrent = ClassFunctionsVirtualListView.addColumn(listData, cacheListColumns, listViewListMessages, maxColCurrent);
             //*************************Add new line*********************************
            ListViewItem device = new ListViewItem(deviceName);
            ClassFunctionsVirtualListView.addNewLine(listData, cacheListColumns, device);
            //**************add new line/device in cacheListMessages
            ClassFunctionsVirtualListView.addDeviceToCache(cacheListMessages, firstToTop, nbMessage, device);
            //**************complete subItems for all line in cacheListMessages**********************
            ClassFunctionsVirtualListView.completeList(cacheListMessages, maxColCurrent);
            //************************************************
             nbMessage += 1;
            this.Text = memoName + " (Messages received : " + nbMessage.ToString() + "/" + maxMessages.ToString() + ")";
            try
            {
            listViewListMessages.VirtualListSize = nbMessage;
            }
            catch {
                Console.WriteLine(this.Text);
            }
            ClassFunctionsVirtualListView.autoResizeColumns(listViewListMessages, cacheListColumns.Count);
            //refresh();  // display last message when it is displayed at the bottom list
            listViewListMessages.EndUpdate();
            this.ResumeLayout();
        }
        #endregion
        #region Events Form
        private void toolStripStatusLabelExport_Click(object sender, EventArgs e)
        {
            string directory = classInterfaceWithRtl433.getDirectoryRecording();
            string fileName = ClassFunctionsVirtualListView.valideNameFile(memoName,"_");
           if( ClassFunctionsVirtualListView.serializeText(directory + fileName + ".txt", cacheListColumns, cacheListMessages,true, nbMessage,false))
           {
                MessageBox.Show("Export OK", "Export messages", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void FormDevicesListMessages_FormClosed(object sender, FormClosedEventArgs e)
        {
            //cacheListColumns = null;  pb with sdrsharp framework 6.0 listViewListMessages_RetrieveVirtualItem
            //cacheListMessages = null;    pb close window since framework 6 listViewListMessages_RetrieveVirtualItem
            classParent.closingOneFormDeviceListMessages(memoName);
            //GC.Collect();
        }
        #endregion


    }
}
