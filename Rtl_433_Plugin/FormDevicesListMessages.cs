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
    internal partial class FormDevicesListMessages : Form
    {
        private ClassInterfaceWithRtl433 classInterfaceWithRtl433;
        private String memoName = "";
        private Int32 maxMessages = 0;
        private Dictionary<String, Int32> cacheListColumns;
        private ListViewItem[] cacheListMessages;
        private Int32 nbMessage = 0;
        private Rtl_433_Panel classParent;
		private Boolean firstToTop = false;
        internal FormDevicesListMessages(Rtl_433_Panel classParent, Int32 maxDevices,String name, ClassInterfaceWithRtl433 classInterfaceWithRtl433)
        {
            this.classInterfaceWithRtl433 = classInterfaceWithRtl433;
            InitializeComponent();
            this.classParent = classParent;
            this.maxMessages = maxDevices;
            typeof(Control).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, listViewListMessages, new object[] { true });
            this.SuspendLayout();
            ClassFunctionsVirtualListView.initListView(listViewListMessages);
            cacheListMessages = new ListViewItem[this.maxMessages]; 
            cacheListColumns = new Dictionary<String, Int32>();
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
            this.ResumeLayout(true);
         }
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
        //private Int32 FindIndexIfDeviceExist(String device)
        //{
        //    for (Int32 row = 0; row < maxMessages; row++)
        //    {
        //        ListViewItem lvi = cacheListMessages[row];
        //        if (lvi != null && lvi.Text == device)
        //            return row;
        //    }
        //    return -1;
        //}
        private void EnsureVisible(Int32 item)
        {
            listViewListMessages.Items[item].EnsureVisible();
        }
        #endregion
        #region publics functions
        internal void refresh()
        {
            if (nbMessage > 0)
                listViewListMessages.Items[nbMessage - 1].EnsureVisible();
            this.Refresh();
        }

        private Int32 maxColCurrent = 0;
        internal void setMessages(Dictionary<String, String> listData)
        {
            if (cacheListColumns == null)
                return;
            
            String deviceName = (nbMessage+1).ToString();
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
            ClassFunctionsVirtualListView.addElemToCache(cacheListMessages, firstToTop, nbMessage, device);
            //**************complete subItems for all line in cacheListMessages**********************
            ClassFunctionsVirtualListView.completeList(cacheListMessages, maxColCurrent);
            //************************************************
             nbMessage += 1;
            this.Text = memoName + " (Messages received : " + nbMessage.ToString() + "/" + maxMessages.ToString() + ")";
            //try
            //{
            listViewListMessages.VirtualListSize = nbMessage;
            //}
            //catch {
            //    Console.WriteLine(this.Text);
            //}
            ClassFunctionsVirtualListView.resizeAllColumns(listViewListMessages);
            //refresh();  // display last message when it is displayed at the bottom list
            listViewListMessages.EndUpdate();
            this.ResumeLayout(true);
        }
        #endregion
        #region Events Form
        private void toolStripStatusLabelExport_Click(object sender, EventArgs e)
        {
            String directory = classParent.getDirectoryRecording();
            String fileName = ClassFunctionsVirtualListView.valideNameFile(memoName,"_");
           if( ClassFunctionsVirtualListView.serializeText(directory + fileName + ".txt", cacheListColumns, cacheListMessages,true, nbMessage,false))
           {
                MessageBox.Show("Export OK", "Export messages", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (!closeByProgram)  //for foreach dictionary
                classParent.closingOneFormDeviceListMessages(memoName);
            //GC.Collect();
        }
         #endregion
    }
}
