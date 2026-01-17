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
using System.Linq;

namespace SDRSharp.Rtl_433
{
    public partial class FormDevicesListMessages : BaseFormWithTopMost
    {
        #region declare
        private readonly String NameForm = "";
        private readonly Int32 maxMessages = 0;
        private readonly Dictionary<String, Int32> cacheListColumns;
        private readonly ListViewItem[] cacheListMessages;
        private Int32 nbMessage = 0;
        private readonly ClassFormListMessages classParent;
        private readonly Boolean firstToTop = false;
        private String PathAndNameFile = "";
        private System.Windows.Forms.ListView listViewListMessages;
        #endregion
        internal FormDevicesListMessages(ClassFormListMessages classParent, String name) : base(100, false)
        {
            InitializeComponent();
            // -------------------------------
            //  THIS FORM
            // -------------------------------
            this.classParent = classParent;
            this.Font = ClassUtils.Font;
            this.BackColor = ClassUtils.BackColor;
            this.ForeColor = ClassUtils.ForeColor;
            this.Cursor = ClassUtils.Cursor;
            this.maxMessages = ClassUtils.MaxDevicesWindows;
            this.Padding = new System.Windows.Forms.Padding(2);  //else no resize form no cursor
            this.MinimumSize = new System.Drawing.Size(0, 100); //if only title crash on listViewListMessages.VirtualListSize = nbMessage;
            this.SuspendLayout();

            // -------------------------------
            //  LIST VIEW MESSAGES
            // -------------------------------
            listViewListMessages = new System.Windows.Forms.ListView();
            ClassFunctionsVirtualListView.InitListView(listViewListMessages);
            listViewListMessages.BackColor = this.BackColor;   //pb ambient property ???
            listViewListMessages.ForeColor = this.ForeColor;

            listViewListMessages.Font = this.Font;
            listViewListMessages.Cursor = this.Cursor;
            cacheListMessages = new ListViewItem[this.maxMessages];
            cacheListColumns = new Dictionary<String, Int32>
            {
                { "N° Mes.", 1 }
            };
            listViewListMessages.Columns.Add("");
            listViewListMessages.Columns[0].Text = "N° Mes.";
            listViewListMessages.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.ListViewListMessages_RetrieveVirtualItem);
            typeof(Control).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, listViewListMessages, new object[] { true });
            // -------------------------------
            //  LAYOUT CONTENEUR
            // -------------------------------
            InitLayout(
            (listViewListMessages, SizeType.Percent, 100f)
            );
            // -------------------------------
            //  STATUS BARRE
            // -------------------------------
            statusStripExport.BackColor = this.BackColor;
            statusStripExport.ForeColor = this.ForeColor;
            statusStripExport.ShowItemToolTips = true;

            NameForm = name;
            PathAndNameFile = ClassUtils.GetPathAndNameFileDateAndTxt(NameForm);
            base.TitleText = name + " (Messages received : 0)";
            toolStripStatusLabelExport.ToolTipText = "Record data  \n" +
            " to directory Recordings if it exist else in SdrSharp.exe directory \n" +
            " if you create directory Recordings close this window before export" +
            " You can reload file with Calc\n" +
            " name file = title window+date.txt";

            toolStripStatusLabelExport.ForeColor = this.ForeColor;
            toolStripStatusLabelExport.BackColor = this.BackColor;
            toolStripStatusLabelExport.Visible = true;
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
#endregion
#region publics functions
        internal void RefreshListMessages()
        {
            if (nbMessage > 0)
                listViewListMessages.Items[nbMessage - 1].EnsureVisible();
            this.Refresh();
        }
        internal void SetMessages(Dictionary<String, String> listData)
        {
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
            base.TitleText = NameForm + " (Messages received : " + nbMessage.ToString() + "/" + maxMessages.ToString() + ")";
            try   //without try:Object reference not set to an instance of an object.
            {
                listViewListMessages.VirtualListSize = nbMessage;
            }
            catch
            {
                Debug.WriteLine(base.TitleText);
            }
            ClassFunctionsVirtualListView.ResizeAllColumns(listViewListMessages);
            listViewListMessages.EndUpdate();
            this.ResumeLayout(true);
        }
#endregion
#region Events Form
        private void ToolStripStatusLabelExport_Click(object sender, EventArgs e)
        {
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
            if (!closeByProgram && nbMessage > 0)  //for foreach dictionary
                classParent.ClosingOneFormDeviceListMessages(NameForm);
         }
#endregion
    }
}
