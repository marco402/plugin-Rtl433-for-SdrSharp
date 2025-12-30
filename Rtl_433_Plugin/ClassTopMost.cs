/* Written by Marc Prieur (marco40_github@sfr.fr)
                                ClassTopMost.cs 
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
using System.Drawing;
using System.Windows.Forms;
namespace SDRSharp.Rtl_433
{
    public static class ClassTopMost
    {
        public static void CreateButtons(ref Panel titleBar, ref Label customTxt, ref Button customBtn, ref Button btnMin, ref Button btnMax, ref Button btnClose,FormWindowState WindowState, Int32 Width)
        {
            //window text 
            //customTxt = new Label();
            customTxt.Text =" (Messages received : 0)"; 
            customTxt.Left = 0;
            customTxt.Top = 8;
            customTxt.BackColor = Color.White;  // Color.FromArgb(45, 45, 48);
            customTxt.ForeColor = Color.Black;  // Color.White;
            customTxt.Font = new Font("Microsoft Sans Serif", 10);
            titleBar.Controls.Add(customTxt);

            // Button topMost
            //customBtn = new Button();
            customBtn.Font = new Font("Segoe MDL2 Assets", 12);
            customBtn.Text = "\uF156";  // 
            customBtn.Width = 40;
            customBtn.Height = 28;
            customBtn.Top = 2;
            customBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            customBtn.FlatStyle = FlatStyle.Flat;
            customBtn.FlatAppearance.BorderSize = 0;
            customBtn.BackColor = Color.Transparent; //Color.FromArgb(70, 70, 72);
            customBtn.ForeColor = Color.Black;  //  Color.White;
            titleBar.Controls.Add(customBtn);

            // Minimize
            //btnMin = new Button();
            btnMin.Font = new Font("Segoe MDL2 Assets", 8);
            btnMin.Text = "\uE921";
            btnMin.Top = 2;
            btnMin.Width = 45;
            btnMin.Height = 32;
            btnMin.FlatStyle = FlatStyle.Flat;
            btnMin.FlatAppearance.BorderSize = 0;
            btnMin.BackColor = Color.Transparent;
            btnMin.ForeColor = Color.Black;  // Color.White;
            titleBar.Controls.Add(btnMin);

            // Close
            //btnClose = new Button();
            btnClose.Text = "\uE8BB";
            btnClose.Font = new Font("Segoe MDL2 Assets", 8);
            btnClose.Width = 45;
            btnClose.Height = 32;
            btnClose.Top = 2;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.BackColor = Color.Transparent;
            btnClose.ForeColor = Color.Black;  //  Color.White;

            titleBar.Controls.Add(btnClose);

            // Maximize
            btnMax.Text = WindowState == FormWindowState.Maximized ? "\uE923" : "\uE922";
            btnMax.Font = new Font("Segoe MDL2 Assets", 8);
            btnMax.Width = 45;
            btnMax.Height = 32;
            btnMax.Top = 2;
            btnMax.FlatStyle = FlatStyle.Flat;
            btnMax.FlatAppearance.BorderSize = 0;
            btnMax.BackColor = Color.Transparent;
            btnMax.ForeColor = Color.Black;  //  Color.White;
            titleBar.Controls.Add(btnMax);
            moveButtons(ref customTxt, ref customBtn, ref btnMin, ref btnMax, ref btnClose,Width);
        }
        public static void moveButtons(ref Label customTxt, ref Button customBtn, ref Button btnMin, ref Button btnMax, ref Button btnClose,Int32 Width)
        {
            btnClose.Left = Width - 50;
            btnMax.Left = btnClose.Left - btnClose.Width;
            btnMin.Left = btnMax.Left - btnMax.Width; ;
            customBtn.Left = btnMin.Left - btnMin.Width; ;
            customTxt.Width = customBtn.Left;
        }
    }
}
