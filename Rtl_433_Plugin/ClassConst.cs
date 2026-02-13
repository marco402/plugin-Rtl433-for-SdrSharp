using System;

namespace SDRSharp.Rtl_433
{
    class ClassConst
    {
        internal const Int32 NBMAXWindows = 150;
        internal const Int32 NBMAXCOLUMN = 1000;
        internal const Int32 NBMAXMESSAGES = 1000;
        internal const Int32 NBMAXDEVICES = 1000;
        internal const Int32 MAXROWCODE = 50;
        internal const Int32 MAXLINESCONSOLE = 3000;
        internal const Int32 NBMAXCOLUMNDEVICES = 5;
        internal const String FILELISTEDEVICES = "devices.txt";
        internal const String FOLDERRECORD = "./Recordings/";
        internal const float FLOATTOBYTE = 255f/2f;
        internal const Int32 DEFAULTFREQUENCY = 433920000;
    }
}
