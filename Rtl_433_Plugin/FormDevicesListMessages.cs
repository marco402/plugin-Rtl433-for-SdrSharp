using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDRSharp.Rtl_433
{
    public partial class FormDevicesListMessages : Form
    {
        public FormDevicesListMessages()
        {
            InitializeComponent();
        }
        private bool initListView = false;
        private void CreateMyListView()
        {
            // Create a new ListView control.
            //ListView listView1 = new ListView();
            //listView1.Bounds = new Rectangle(new Point(10, 10), new Size(300, 200));

            // Set the view to show details.
            //listViewListMessages.View = View.Details;
            // Allow the user to edit item text.
            //listViewListMessages.LabelEdit = false;
            // Allow the user to rearrange columns.
            //listViewListMessages.AllowColumnReorder = false;
            // Display check boxes.
            //listViewListMessages.CheckBoxes = false;
            // Select the item and subitems when selection is made.
            //listViewListMessages.FullRowSelect = false;
            // Display grid lines.
            //listViewListMessages.GridLines = true;
            // Sort the items in the list in ascending order.
            //listViewListMessages.Sorting = SortOrder.None;
        }


        public void setInfoDevice(Dictionary<String, String> listData)
        {
            if (!initListView)
            {
                // Create three items and three sets of subitems for each item.
                ListViewItem item1 = new ListViewItem("item1");
                ListViewItem item2 = new ListViewItem("item2");
                ListViewItem item3 = new ListViewItem("item3");
                // Create columns for the items and subitems.
                // Width of -2 indicates auto-size.
                listViewListMessages.Columns.Add("Item Column", -2, HorizontalAlignment.Left);
                listViewListMessages.Columns.Add("Column 2", -2, HorizontalAlignment.Left);
                listViewListMessages.Columns.Add("Column 3", -2, HorizontalAlignment.Left);
                listViewListMessages.Columns.Add("Column 4", -2, HorizontalAlignment.Center);

                //Add the items to the ListView.
                listViewListMessages.Items.AddRange(new ListViewItem[] { item1, item2, item3 });

                // Create two ImageList objects.
                //ImageList imageListSmall = new ImageList();
                //ImageList imageListLarge = new ImageList();

                // Initialize the ImageList objects with bitmaps.
                //imageListSmall.Images.Add(Bitmap.FromFile("C:\\MySmallImage1.bmp"));
                //imageListSmall.Images.Add(Bitmap.FromFile("C:\\MySmallImage2.bmp"));
                //imageListLarge.Images.Add(Bitmap.FromFile("C:\\MyLargeImage1.bmp"));
                //imageListLarge.Images.Add(Bitmap.FromFile("C:\\MyLargeImage2.bmp"));

                //Assign the ImageList objects to the ListView.
                //listViewListMessages.LargeImageList = imageListLarge;
                //listViewListMessages.SmallImageList = imageListSmall;

                // Add the ListView to the control collection.
                //this.Controls.Add(listViewListMessages);
                // Place a check mark next to the item.
                //item1.Checked = true;
                item1.SubItems.Add("1");
                item1.SubItems.Add("2");
                item1.SubItems.Add("3");

                item2.SubItems.Add("4");
                item2.SubItems.Add("5");
                item2.SubItems.Add("6");

                // Place a check mark next to the item.
                //item3.Checked = true;
                item3.SubItems.Add("7");
                item3.SubItems.Add("8");
                item3.SubItems.Add("9");


            }


        }



        //public void InitializeListView()
        //{
        //    ColumnHeader header1 = this.listView1.InsertColumn(0, "Name", 10 * listView1.Font.SizeInPoints.ToInt32(), HorizontalAlignment.Center);
        //    ColumnHeader header2 = this.listView1.InsertColumn(1, "E-mail", 20 * listView1.Font.SizeInPoints.ToInt32(), HorizontalAlignment.Center);
        //    ColumnHeader header3 = this.listView1.InsertColumn(2, "Phone", 20 * listView1.Font.SizeInPoints.ToInt32(), HorizontalAlignment.Center);
        //}
        //protected void addbutton_Click(object sender, System.EventArgs e)
        //{
        //    // create the subitems to add to the list   
        //    string[] myItems = new string[] { textBox2.Text, textBox3.Text };
        //    // insert all the items into the listview at the last available row  
        //    listView1.InsertItem(listView1.ListItems.Count, textBox1.Text, 0, myItems);
        //}
        //protected void Form1_KeyDown(object sender, System.WinForms.KeyEventArgs e)
        //{
        //    // determine the value of the key pressed. If the value is delete (46), remove all selected rows  
        //    int nKeyValue = e.KeyData.ToInt32();
        //    if (nKeyValue == 46)
        //    {
        //        for (int i = listView1.SelectedItems.Count - 1; i >= 0; i--)
        //        {
        //            ListItem li = listView1.SelectedItems[i];
        //            listView1.ListItems.Remove(li);
        //        }
        //    }
        //}
        //protected void savebutton_Click(object sender, System.EventArgs e)
        //{
        //    try
        //    {
        //        // get the file name to save the list view information in from the standard save dialog  
        //        if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
        //        {
        //            // open a stream for writing and create a StreamWriter to use to implement the stream  
        //            FileStream fs = new FileStream(@saveFileDialog1.FileName, FileMode.OpenOrCreate, FileAccess.Write);
        //            StreamWriter m_streamWriter = new StreamWriter(fs);
        //            m_streamWriter.Flush();
        //            // Write to the file using StreamWriter class  
        //            m_streamWriter.BaseStream.Seek(0, SeekOrigin.Begin);
        //            // write each row of the ListView out to a tab-delimited line in a file  
        //            for (int i = 0; i < this.listView1.ListItems.Count; i++)
        //            {
        //                m_streamWriter.WriteLine(listView1.ListItems[i].Text + "\t" + listView1.ListItems[i].SubItems[0].ToString() + "\t" + listView1.ListItems[i].SubItems[1].ToString());
        //            }
        //            // Close the file  
        //            m_streamWriter.Flush();
        //            m_streamWriter.Close();
        //        }
        //    }
        //    catch (Exception em)
        //    {
        //    }
        //}
        //protected void readbutton_Click(object sender, System.EventArgs e)
        //{
        //    try
        //    {
        //        if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
        //        {
        //            FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
        //            StreamReader m_streamReader = new StreamReader(fs);
        //            // Read to the file using StreamReader class  
        //            m_streamReader.BaseStream.Seek(0, SeekOrigin.Begin);
        //            string strLine = m_streamReader.ReadLine();
        //            int nStart = 0;
        //            int count = 0;
        //            // Read each line of the stream and parse until last line is reached  
        //            while (strLine != null)
        //            {
        //                int nPos1 = strLine.IndexOf("\t", nStart);
        //                string str1 = strLine.Substring(0, nPos1); // get first column string  

        //                nStart = nPos1 + 1;
        //                int nPos2 = strLine.IndexOf("\t", nStart);
        //                string str2 = strLine.Substring(nStart, nPos2 - nStart); // get second column string  
        //                nStart = nPos2 + 1;
        //                string str3 = strLine.Substring(nStart); // get last column string  
        //                listView1.InsertItem(count, str1, 0, new string[] { str2, str3 }); // Add the row to the ListView  
        //                count++; // increment row  
        //                nStart = 0; // reset  
        //                strLine = m_streamReader.ReadLine(); // get next line from the stream  
        //            }
        //            // Close the stream  
        //            m_streamReader.Close();
        //        }
        //    }
        //    catch (Exception em)
        //    {
        //        System.Console.WriteLine(em.Message.ToString());
        //    }
        //}
    }
}
