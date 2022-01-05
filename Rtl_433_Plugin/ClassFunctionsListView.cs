using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDRSharp.Rtl_433
{
    class ClassFunctionsListView
    {
        public static void initListView(ListView listDevices,int nbCol)
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
            for (int i = 0; i < nbCol; i++)
            {
                listDevices.Columns.Add("");
            }
        }
        public static void autoResizeColumns(ListView lv, int nbColumn)
        {
            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            ListView.ColumnHeaderCollection cc = lv.Columns;
            for (int col = 0; col < nbColumn; col++)
            {
                int colWidth = TextRenderer.MeasureText(cc[col].Text, lv.Font).Width + 10;
                if (colWidth > cc[col].Width)
                {
                    cc[col].Width = colWidth;
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
                       // foreach (ListViewItem it in cacheListDevices)
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
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error export devices fct(serializeText).File:" + fileName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        line += (valideNumberForCalc(sit.Text)).Replace(".", nfi.CurrencyDecimalSeparator);
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
            return name;
        }
        private  static string valideNumberForCalc(string value)
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
            return value;
        }
    }
}
