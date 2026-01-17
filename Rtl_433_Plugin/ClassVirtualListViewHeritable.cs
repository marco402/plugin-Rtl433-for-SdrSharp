#if ABSTRACTVIRTUALLISTVIEW
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace SDRSharp.Rtl_433
{
    //#if ABSTRACTVIRTUALLISTVIEW
    //public abstract class BaseVirtualListForm : BaseFormWithTopMost
    //#else
    public abstract class BaseVirtualListForm : Form
    //#endif
    {
        protected ListView listView;
        protected string NameForm = "";

        protected readonly int maxMessages;
        protected readonly bool firstToTop;
        protected readonly new Color BackColor;
        protected readonly new Color ForeColor;
        protected readonly Font font;
        protected readonly Cursor cursor;
        protected Dictionary<string, int> cacheColumns;
        protected ListViewItem[] cacheItems;
        protected int nbMessages = 0;

        protected BaseVirtualListForm(Color BackColor, Color ForeColor,Font font,Cursor cursor,int maxMessages = 100, bool firstToTop = false)
        {
            this.maxMessages = maxMessages;
            this.firstToTop = firstToTop;
            this.BackColor = BackColor;
            this.ForeColor = ForeColor;
            this.Font = Font;
            this.Cursor = Cursor;
            InitializeBaseForm();
        }

        private void InitializeBaseForm()
        {
            this.SuspendLayout();

            listView = new ListView();
            listView.RetrieveVirtualItem += ListView_RetrieveVirtualItem;
            listView.BackColor = BackColor;   //pb ambient property ???
            listView.ForeColor = ForeColor;
            listView.Font = Font;
            listView.Cursor = Cursor;
            ClassFunctionsVirtualListView.InitListView(listView);

            cacheItems = new ListViewItem[maxMessages];
            cacheColumns = new Dictionary<string, int>();

            this.Controls.Add(listView);
            this.ResumeLayout(true);
        }

        private void ListView_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            try
            {
                if (e.ItemIndex >= 0 && e.ItemIndex < cacheItems.Length)
                {
                    var item = cacheItems[e.ItemIndex];
                    if (item != null)
                        e.Item = item;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "RetrieveVirtualItem");
            }
        }

        // Appelé par les classes dérivées pour définir les colonnes initiales
        protected void InitColumns(Dictionary<string, int> initialColumns)
        {
            cacheColumns = new Dictionary<string, int>(initialColumns);

            foreach (var col in initialColumns)
                listView.Columns.Add(col.Key);

            UpdateTitle();
        }

        protected void UpdateTitle()
        {
            this.Text = $"{NameForm} (Messages received : {nbMessages}/{maxMessages})";
        }

        public void AddMessage(Dictionary<string, string> data)
        {
            if (nbMessages >= maxMessages)
                return;

            this.SuspendLayout();
            listView.BeginUpdate();

            // Ajout des colonnes si nécessaire
            ClassFunctionsVirtualListView.AddColumn(data, cacheColumns, listView, 2);

            // Création de la ligne
            string rowName = (nbMessages + 1).ToString();
            ListViewItem item = new ListViewItem(rowName);

            ClassFunctionsVirtualListView.AddNewLine(data, cacheColumns, item);

            // Ajout dans le cache
            ClassFunctionsVirtualListView.AddElemToCache(cacheItems, firstToTop, nbMessages, item);

            // Compléter les subitems
            ClassFunctionsVirtualListView.CompleteList(cacheItems, cacheColumns.Count);

            nbMessages++;

            try
            {
                listView.VirtualListSize = nbMessages;
            }
            catch { }

            ClassFunctionsVirtualListView.ResizeAllColumns(listView);

            UpdateTitle();

            listView.EndUpdate();
            this.ResumeLayout(true);
        }
    }
}
#endif