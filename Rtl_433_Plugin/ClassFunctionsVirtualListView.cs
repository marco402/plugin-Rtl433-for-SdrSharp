/* Written by Marc Prieur (marco40_github@sfr.fr)
                         ClassFunctionsVirtualListView.cs 
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
    internal static class ClassFunctionsVirtualListView
    {
        internal static void InitListView(ListView lv)
        {
            lv.View = View.Details;
            lv.GridLines = true;
            lv.FullRowSelect = true;
            lv.Scrollable = true;
            lv.MultiSelect = false;
            lv.HideSelection = false;
            lv.HeaderStyle = ColumnHeaderStyle.Clickable;
            lv.Dock = System.Windows.Forms.DockStyle.Fill;
            lv.AllowColumnReorder = false;
            lv.Visible = true;
            lv.VirtualMode = true;
            lv.VirtualListSize = 0;
            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
        /// <summary>
        /// add one item to cache for virtual mode
        /// </summary>
        /// <param name="cacheLv"></param>
        /// <param name="firstToTop">order place at the top or at the bottom</param>
        /// <param name="nb"></param>
        /// <param name="lv"></param>
        internal static void AddElemToCache(ListViewItem[] cacheLv, Boolean firstToTop, Int32 nb, ListViewItem lv)
        {
            if (firstToTop)
            {
                cacheLv[nb] = lv;
            }
            else
            {
                //last message at the top list
                for (Int32 m = nb; m > 0; m--)
                {
                    cacheLv[m] = cacheLv[m - 1];
                }
                cacheLv[0] = lv;
            }
        }
        /// <summary>
        /// virtual mode nb subitem(cacheListMessages)=nb subitem(listView)
        /// </summary>
        /// <param name="cacheLv"></param>
        /// <param name="maxColCurrent"></param>
        internal static void CompleteList(ListViewItem[] cacheLv, Int32 maxColCurrent)
        {
            foreach (ListViewItem lvi in cacheLv)
            {
                if (lvi != null)
                {
                    Int32 nbToAdd = maxColCurrent - lvi.SubItems.Count;
                    for (Int32 i = 0; i < nbToAdd; i++)
                        lvi.SubItems.Add("");
                }
            }
        }
        /// <summary>
        /// Add line item,add subItems as necessary
        /// </summary>
        /// <param name="listData">data in for one line</param>
        /// <param name="cacheListColumns">list colomn for virtual mode</param>
        /// <param name="lv">current item</param>
        internal static void AddNewLine(Dictionary<String, String> listData, Dictionary<String, Int32> cacheListColumns, ListViewItem lv)
        {
            foreach (KeyValuePair<String, String> _data in listData)
            {
                if (cacheListColumns.TryGetValue(_data.Key, out Int32 indexColonne))          //get index column for _data.key found _data.Key
                {
                    //add subitems before item at indexColonne
                    for (Int32 i = lv.SubItems.Count; i < indexColonne; i++)
                        lv.SubItems.Add("");
                    lv.SubItems[indexColonne - 1].Text = _data.Value;
                }
            }
        }
        /// <summary>
        /// ~10ms en debug pour 23 colonnes 
        /// </summary>
        /// <param name="listData"></param>
        /// <param name="cacheListColumns"></param>
        /// <param name="lv"></param>

        internal static void RefreshLine(Dictionary<String, String> listData, Dictionary<String, Int32> cacheListColumns, ListViewItem lv)
        {
            for (Int32 i = lv.SubItems.Count; i < cacheListColumns.Count; i++)
                lv.SubItems.Add("");
            foreach (KeyValuePair<String, String> _data in listData)
            {
                if (cacheListColumns.TryGetValue(_data.Key, out Int32 indexColonne))
                    lv.SubItems[indexColonne - 1].Text = _data.Value;
            }

        }
        /// <summary>
        /// For each item in listData Add column to listViewListMessages if no exist.
        /// </summary>
        /// <param name="listData">data in</param>
        /// <param name="cacheListColumns">in no exist add item to cacheListColumns for virtual mode</param>
        /// <param name="listViewListMessages">listView</param>
        internal static void AddColumn(Dictionary<String, String> listData, Dictionary<String, Int32> cacheListColumns, ListView lv,Int32 maxColumn)
        {
            foreach (KeyValuePair<String, String> _data in listData)
            {
                Boolean ret = AddOneColumn(_data.Key, cacheListColumns, lv,maxColumn);
                if (ret)
                    return;
            }
            return ;
        }
        internal static Boolean AddOneColumn(String _data, Dictionary<String, Int32> cacheListColumns, ListView lv,Int32 maxColumn)
        {
            if (!cacheListColumns.ContainsKey(_data)) //new col
            {
                if (cacheListColumns.Count >= maxColumn)
                    return true;
                cacheListColumns.Add(_data, cacheListColumns.Count + 1);
                lv.Columns.Add("");
                lv.Columns[cacheListColumns.Count - 1].Text = _data;
            }
            return false;
        }
        internal static Boolean AddOneColumnDevices(String _data, Dictionary<String, Int32> cacheListColumns, ListView lv, Int32 maxColumn)
        {
            if (!cacheListColumns.ContainsKey(_data)) //new col
            {
                if (cacheListColumns.Count >= maxColumn)
                    return true;
                cacheListColumns.Add(_data, cacheListColumns.Count + 1);
                lv.Columns.Add(_data);
            }
            return false;
        }
        internal static void ResizeAllColumns(ListView lv)
        {
            ListView.ColumnHeaderCollection cc = lv.Columns;
            for (Int32 col = 0; col < cc.Count; col++)
            {
                if (cc[col].Text != "") 
                {
                    Int32 colWidth = TextRenderer.MeasureText(cc[col].Text, lv.Font).Width + 10;
                    if (colWidth > cc[col].Width)
                    {
                        cc[col].Width = colWidth;
                    }
                }
            }
        }

        internal static Boolean SerializeText(String fileName, Dictionary<String, Int32> cacheListColumns, ListViewItem[] cacheLv, Boolean formatNumber, Int32 nbMessage, Boolean sensDirect)
        {
            NumberFormatInfo nfi = new CultureInfo(CultureInfo.CurrentUICulture.Name, false).NumberFormat;
            try
            {
                using (Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    StreamWriter str = new StreamWriter(stream);
                    String line = String.Empty;
                    foreach (KeyValuePair<String, Int32> _data in cacheListColumns)
                    {
                        if (_data.Key == String.Empty)
                            line += "\t\t";
                        else
                            line += (_data.Key + "\t");
                    }
                    str.WriteLine(line);
                    ListViewItem it;
                    if (sensDirect)
                    {
                        for (Int32 i = 0; i < nbMessage; i++)
                        {
                            it = cacheLv[i];
                            line = ProcessLine(it, formatNumber, nfi, cacheListColumns.Count);
                            str.WriteLine(line);
                        }
                    }
                    else
                    {
                        for (Int32 i = nbMessage - 1; i > -1; i--)
                        {
                            it = cacheLv[i];
                            line = ProcessLine(it, formatNumber, nfi, cacheListColumns.Count);
                            str.WriteLine(line);
                        }
                    }
                    str.Flush();
                }
                 return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error export item fct(serializeText).File:" + fileName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }
        internal static String ProcessLine(ListViewItem it, Boolean formatNumber, NumberFormatInfo nfi, Int32 nbColumn)
        {
            String line = String.Empty;
            Int32 nColumn = 0;
            foreach (ListViewItem.ListViewSubItem sit in it.SubItems)
            {
                if (sit.Text == String.Empty)
                    line += "\t";
                else
                {
                    if (formatNumber)
                    {
                        line += (DeleteUnitForCalc(sit.Text)).Replace(".", nfi.CurrencyDecimalSeparator);
                    }
                    else
                        line += sit.Text;
                    line += "\t";
                }
                nbColumn ++;
                if (nColumn == nbColumn)
                    return line;
            }
            return line;
        }
        internal static String ProcessLineTxt(Dictionary<String, String> listData, Boolean formatNumber, NumberFormatInfo nfi, Int32 nbColumn)
        {
            String line = String.Empty;
            Int32 nColumn = 0;
            foreach (KeyValuePair<String, String> _data in listData)
            {
                if (_data.Key == String.Empty)
                    line += "\t";
                else
                {
                    if (formatNumber)
                        line += (DeleteUnitForCalc(_data.Value)).Replace(".", nfi.CurrencyDecimalSeparator);
                    else
                        line += _data.Value;

                    line = line.PadRight(line.Length + _data.Key.Length - _data.Value.Length) + "\t"; ;
                }
                nbColumn ++;
                if (nColumn == nbColumn)
                    return line;
            }
            return line;
        }
        internal static String ValideNameFile(String name, String replaceChar)
        {
            List<char> badChars = new List<char>(Path.GetInvalidFileNameChars());
            foreach (char C in badChars)
            {
                name = name.Replace(C.ToString(), replaceChar);
            }
            //badChars = null;
            return name;
        }
        internal static String DeleteUnitForCalc(String value)
        {
            List<String> badChars = new List<String>
            {
                " F",
                " C",
                " mph",
                " kph",
                " mi/h",
                " km/h",
                " mi h",
                " km h",
                " inch",
                " in",
                " mm",

                " in h",
                " mm h",
                " in/h",
                " mm /h",

                " inHg",
                " hpa",

                " PSI",
                " kPa",

                " dB",
                " Mhz",
                " %"
            };
            foreach (String C in badChars)
            {
                value = value.Replace(C, "");
            }
            return value;
        }
        internal static ListViewItem GetItem(String name, ListViewItem[] cacheLv)
        {
            foreach (ListViewItem item in cacheLv)
            {
                if (item == null)
                    break;
                if (item.Text == name)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
