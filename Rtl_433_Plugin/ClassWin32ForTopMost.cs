/* Written by Marc Prieur (marco40_github@sfr.fr)
                                ClassWin32ForTopMost.cs 
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
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
#if TOPMOST
namespace SDRSharp.Rtl_433
{
    public static class ClassWin32ForTopMost
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetWindowPos(
     IntPtr hWnd,
     IntPtr hWndInsertAfter,
     int X,
     int Y,
     int cx,
     int cy,
     uint uFlags);

        public static readonly IntPtr HWND_TOP = new IntPtr(0);
        public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);

        public const uint SWP_NOSIZE = 0x0001;
        public const uint SWP_NOMOVE = 0x0002;
        public const uint SWP_NOZORDER = 0x0004;
        public const uint SWP_FRAMECHANGED = 0x0020;
        public const uint SWP_SHOWWINDOW = 0x0040;

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        public const int SW_MINIMIZE = 6;
        public const int SW_MAXIMIZE = 3;
        public const int SW_RESTORE = 9;

        [DllImport("user32.dll")]
        public static extern void ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
    }
}
#endif