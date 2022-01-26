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
using System.Globalization;
using System.IO;
using System.Windows.Forms;
namespace SDRSharp.Rtl_433
{
    class ClassFunctionsVirtualListView
    {
        public static void initListView(ListView listDevices)
        {
            listDevices.View = View.Details;
            listDevices.GridLines = true;
            listDevices.FullRowSelect = true;
            listDevices.Scrollable = true;
            listDevices.MultiSelect = false;
            listDevices.HideSelection = false;
            listDevices.HeaderStyle = ColumnHeaderStyle.Clickable;
            listDevices.BackColor = System.Drawing.SystemColors.ControlDark;
            listDevices.Dock = System.Windows.Forms.DockStyle.Fill;
            listDevices.ForeColor = System.Drawing.Color.Blue;
            listDevices.AllowColumnReorder = false;
            listDevices.Visible = true;
            listDevices.VirtualMode = true;
            listDevices.VirtualListSize = 0;
        }
        /// <summary>
        /// add one device to cache for virtual mode
        /// </summary>
        /// <param name="cacheListMessages"></param>
        /// <param name="firstToTop">order place at the top or at the bottom</param>
        /// <param name="nbMessage"></param>
        /// <param name="device"></param>
        public static void addDeviceToCache(ListViewItem[] cacheListMessages,Boolean firstToTop,int nbMessage, ListViewItem device)
        {
            if (firstToTop)
            {
                cacheListMessages[nbMessage] = device;
            }
            else
            {
                //last message at the top list
                for (int m = nbMessage; m > 0; m--)
                {
                    cacheListMessages[m] = cacheListMessages[m - 1];
                }
                cacheListMessages[0] = device;
            }
        }
        /// <summary>
        /// virtual mode nb subitem(cacheListMessages)=nb subitem(listView)
        /// </summary>
        /// <param name="cacheListMessages"></param>
        /// <param name="maxColCurrent"></param>
        public static void completeList(ListViewItem[] cacheListMessages,int maxColCurrent)
        {
            foreach (ListViewItem lvi in cacheListMessages)
            {
                if (lvi != null)
                {
                    int nbToAdd = maxColCurrent - lvi.SubItems.Count;
                    for (int i = 0; i < nbToAdd; i++)
                        lvi.SubItems.Add("");
                }
            }
        }
        /// <summary>
        /// Add line item=device,add subItems as necessary
        /// </summary>
        /// <param name="listData">data in for one line</param>
        /// <param name="cacheListColumns">list colomn for virtual mode</param>
        /// <param name="device">current device</param>
        public static void addNewLine(Dictionary<String, String> listData, Dictionary<String, int> cacheListColumns, ListViewItem device)
        {
            int indexColonne = 0;
            foreach (KeyValuePair<string, string> _data in listData)
            {
                if (cacheListColumns.TryGetValue(_data.Key, out indexColonne))          //get index column for _data.key found _data.Key
                {
                    //add subitems before item at indexColonne
                    for (int i = device.SubItems.Count; i < indexColonne; i++)
                        device.SubItems.Add("");
                    device.SubItems[indexColonne - 1].Text = _data.Value;
                }
            }
        }
        /// <summary>
        /// ~10ms en debug pour 23 colonnes 
        /// </summary>
        /// <param name="listData"></param>
        /// <param name="cacheListColumns"></param>
        /// <param name="device"></param>
        /// <param name="maxColCurrent"></param>
        public static void refreshLine(Dictionary<String, String> listData, Dictionary<String, int> cacheListColumns, ListViewItem device,int maxColCurrent)
        {
            for (int i = device.SubItems.Count; i < maxColCurrent; i++)
                device.SubItems.Add("");
            int indexColonne = 0;
            foreach (KeyValuePair<string, string> _data in listData)
            {
                if (cacheListColumns.TryGetValue(_data.Key, out indexColonne))
                    device.SubItems[indexColonne-1].Text = _data.Value;
            }

        }
        /// <summary>
        /// For each element in listData Add column to listViewListMessages if no exist.
        /// </summary>
        /// <param name="listData">data in</param>
        /// <param name="cacheListColumns">in no exist add element to cacheListColumns for virtual mode</param>
        /// <param name="listViewListMessages">listView</param>
        /// <param name="maxColCurrent"> update maxColCurrent</param>
        /// <returns>return maxColCurrent</returns>
        public static int addColumn(Dictionary<String, String> listData, Dictionary<String, int> cacheListColumns, ListView listViewListMessages,int  maxColCurrent)
        {
            foreach (KeyValuePair<string, string> _data in listData)
            {
                maxColCurrent = addOneColumn(_data.Key, cacheListColumns, listViewListMessages, maxColCurrent);
            }
            return maxColCurrent;
        }
        public static int addOneColumn(String _data, Dictionary<String, int> cacheListColumns, ListView listViewListMessages, int maxColCurrent)
        {
                if (!cacheListColumns.ContainsKey(_data)) //new col
                {
                    cacheListColumns.Add(_data, cacheListColumns.Count + 1);
                    listViewListMessages.Columns.Add("");
                    if (listViewListMessages.Columns.Count > maxColCurrent)
                    {
                        maxColCurrent = listViewListMessages.Columns.Count;
                    }
                    listViewListMessages.Columns[cacheListColumns.Count - 1].Text = _data;
                }
                return maxColCurrent;
        }
        public static void autoResizeColumns(ListView lv, int nbColumn)
        {
            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            ListView.ColumnHeaderCollection cc = lv.Columns;
            for (int col = 0; col < nbColumn; col++)
            {
                //Debug.WriteLine(col.ToString()+'\t'+cc[col].Text);
                if(cc[col].Text!="")
                {
                    int colWidth = TextRenderer.MeasureText(cc[col].Text, lv.Font).Width + 10;
                    if (colWidth > cc[col].Width)
                    {
                        cc[col].Width = colWidth;
                    }
                }
            }
        }
        public static void autoResizeAllColumns(ListView lv)
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
        public static bool serializeText(string fileName, Dictionary<String, int>  cacheListColumns, ListViewItem[] cacheListDevices,bool formatNumber,int nbMessage,bool sensDirect)
        {
            NumberFormatInfo nfi = new CultureInfo(CultureInfo.CurrentUICulture.Name, false).NumberFormat;
            try
            {
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
                    ListViewItem it;
                    if (sensDirect)
                    {
                       for(int i=0;i<nbMessage;i++)
                       {
                            it = cacheListDevices[i];
                            line = processLine(it, formatNumber,nfi, cacheListColumns.Count);
                            str.WriteLine(line);
                       }
                    }
                    else
                    {
                        for (int i = nbMessage-1; i > -1; i--)
                        {
                            it = cacheListDevices[i];
                            line = processLine(it, formatNumber, nfi, cacheListColumns.Count);
                            str.WriteLine(line);
                        }
                    }
                    str.Close();
                }
                nfi = null;
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error export devices fct(serializeText).File:" + fileName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                nfi = null;
                return false;
            }

        }
        private static string processLine(ListViewItem it, bool formatNumber, NumberFormatInfo nfi,Int32 nbColumn)
        {
            string line = string.Empty;
            Int32 nColumn = 0;
            foreach (ListViewItem.ListViewSubItem sit in it.SubItems)
            {
                if (sit.Text == string.Empty)
                    line += "\t";
                else
                {
                    if (formatNumber)
                    {
                        line += (deleteUnitForCalc(sit.Text)).Replace(".", nfi.CurrencyDecimalSeparator);
                    }
                    else
                        line += sit.Text;
                    line += "\t";
                }
                nbColumn += 1;
                if (nColumn==nbColumn)
                    return line;   
            }
            return line;
        }
        public static string valideNameFile(string name, string replaceChar)
        {
            List<char> badChars = new List<char>(Path.GetInvalidFileNameChars());
            foreach (char C in badChars)
            {
                name = name.Replace(C.ToString(), replaceChar);
            }
            badChars = null;
            return name;
        }
        private  static string deleteUnitForCalc(string value)
        {
            List<string> badChars = new List<string>();
            badChars.Add(" F");
            badChars.Add(" C");
            badChars.Add(" mph");
            badChars.Add(" kph");
            badChars.Add(" mi/h");
            badChars.Add(" km/h");
            badChars.Add(" mi h");
            badChars.Add(" km h");
            badChars.Add(" inch");
            badChars.Add(" in");
            badChars.Add(" mm");

            badChars.Add(" in h");
            badChars.Add(" mm h");
            badChars.Add(" in/h");
            badChars.Add(" mm /h");

            badChars.Add(" inHg");
            badChars.Add(" hpa");

            badChars.Add(" PSI");
            badChars.Add(" kPa");

            badChars.Add(" dB");
            badChars.Add(" Mhz");
            badChars.Add(" %");
            foreach (string C in badChars)
            {
                value = value.Replace(C,"");
            }
            badChars = null;
            return value;
        }
        public static ListViewItem getDevice(String deviceName, ListViewItem[] cacheListDevices)
        {
            foreach (ListViewItem item in cacheListDevices)
            {
                if (item == null)
                    break;
                if (item.Text == deviceName)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
