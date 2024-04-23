using SDRSharp.Rtl_433;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

//listview.item.text maxLength=259

namespace SDRSharp.Rtl_433
{
    internal partial class FormConsole : Form
    {
        private Rtl_433_Panel classParent;
        private Double maxLines = 0;
        private Int32 nbLines = 0;
        private Boolean msgBoxDisplayed = false;
        private Boolean closed = false;
        List<ListViewItem> cacheLignes = new List<ListViewItem>();
        internal FormConsole(Rtl_433_Panel classParent, Int32 maxLines)
        {
            InitializeComponent();
            this.classParent = classParent;
            this.Font = this.classParent.Font;
            this.BackColor = this.classParent.BackColor;
            this.ForeColor = this.classParent.ForeColor;
            this.Cursor = this.classParent.Cursor;
            this.maxLines = maxLines;
            typeof(Control).InvokeMember("DoubleBuffered", BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic, null, listViewConsole, new object[] { true });
            this.SuspendLayout();
            SDRSharp.Rtl_433.ClassFunctionsVirtualListView.initListView(listViewConsole);
            listViewConsole.BackColor = this.BackColor;   //pb ambient property ???
            listViewConsole.ForeColor = this.ForeColor;
            listViewConsole.Font = this.Font;
            listViewConsole.Cursor = this.Cursor;
            this.Text = "Console RTL_433---nbLigne="+"0/" + maxLines.ToString();
            listViewConsole.GridLines = false;
            listViewConsole.FullRowSelect = false;
            listViewConsole.View = View.Details;   //hide column header
            listViewConsole.MultiSelect = true;
            listViewConsole.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(listViewConsole_RetrieveVirtualItem);
            this.ResumeLayout(true);
        }
        #region private functions
        internal void listViewConsole_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            if(cacheLignes!=null)
            {
                try
                {
                    if (e.ItemIndex >= 0)
                    {
                        ListViewItem lvi = cacheLignes[e.ItemIndex];
                        if (lvi != null)
                            e.Item = lvi;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error fct(listViewConsole_RetrieveVirtualItem)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion
        #region internal functions
        internal Boolean WriteLine(Dictionary<String, String> listData)
        {
            if (cacheLignes == null)
                return false;
            this.SuspendLayout();
            listViewConsole.BeginUpdate();
            foreach (KeyValuePair<String, String> _line in listData)
            {
                Application.DoEvents();
                String theLine = _line.Key + "  " + _line.Value + "\r\n";
                if (nbLines > maxLines - 1)
                {
                    if (!msgBoxDisplayed)
                    {
                        msgBoxDisplayed = true;
                        MessageBox.Show("You have reached the maximum number of rows provided in the console(" + maxLines.ToString() + "), if necessary you can increase it in SDRSharp.config(key RTL_433_plugin.maxLinesConsole) , pay attention to the memory occupancy. ", "Console", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                        listViewConsole.EndUpdate();
                        this.ResumeLayout(true);
                    }
                    return true;                    //message max row
                }

                ListViewItem ligne = new ListViewItem(theLine);
                if (cacheLignes == null)
                    return false;   //if formclosed
                cacheLignes.Add(ligne);
                nbLines += 1;
                this.Text = "Console RTL_433---nbLigne=" + nbLines.ToString() + "/" + maxLines.ToString();
                try   //without try:Object reference not set to an instance of an object.
                {
                    listViewConsole.VirtualListSize = nbLines;
                }
                catch
                {
                }
            }
            try
            {
                listViewConsole.EndUpdate();
            this.ResumeLayout(true);
            this.Refresh();
            }
            catch
            {
            }
            if (closed)
            {
                cacheLignes = null;
                classParent.closeConsole();
            }
            return false;
        }
        #endregion
        #region Events Form
        private void copyAllItemsToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            String text = String.Empty;
            if (listViewConsole.Items.Count > 1)
            {
                for (int item = 0; item < listViewConsole.Items.Count; item++)
                    text += listViewConsole.Items[item].Text;
                Clipboard.SetText(text);
            }
        }
        private void copySelectedItemsToClipboardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            String text = String.Empty;
            ListView.SelectedIndexCollection col = listViewConsole.SelectedIndices;
            if (col.Count > 0)
            {
                foreach (int item in col)
                    text += listViewConsole.Items[item].Text;
                Clipboard.SetText(text);
            }
        }
        private void toolStripButtonAlwaysOnTop_Click(object sender, EventArgs e)
        {
            this.TopMost = !this.TopMost;
        }
        private void FormConsole_FormClosed(object sender, FormClosedEventArgs e)
        {
            cacheLignes = null;
            classParent.closeConsole();
        }
        //private Boolean closeByProgram = false;
        //internal void CloseByProgram()
        //{
        //    //closeByProgram = true;
        //    this.Close();
        //}
        #endregion

        private void FormConsole_FormClosing(object sender, FormClosingEventArgs e)
        {
            closed = true;  //data en cours
        }
    }
}