using System;
using System.Collections.Generic;
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
            listDevices.ForeColor = System.Drawing.SystemColors.ButtonFace;
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
        public static void serializeText(string fileName, Dictionary<String, int>  cacheListColumns, ListViewItem[] cacheListDevices)
        {
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
    }
}
