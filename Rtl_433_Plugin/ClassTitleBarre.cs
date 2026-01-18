#define noTESTLANGUAGE
using System;
using System.Drawing;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;

namespace SDRSharp.Rtl_433
{
#if ABSTRACTVIRTUALLISTVIEW
    public class BaseFormWithTopMost : BaseVirtualListForm
#else
    public class BaseFormWithTopMost : Form    //no abstract for designer
#endif 
    {
        private Panel titleBar;
        private Label titleLabel;
        private Button btnMin;
        private Button btnMax;
        private Button btnClose;
        private Button btnTopMost;
        private bool isTopMost = false;
#if TESTLANGUAGE
        private MenuStrip menu ;
        private ToolStripMenuItem langMenu ;
#endif
        public BaseFormWithTopMost( int maxMessages = 100, bool firstToTop = false)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Padding = new System.Windows.Forms.Padding(2);  //else no resize form no cursor
            this.DoubleBuffered = true;
            this.Font = ClassUtils.Font;
            this.BackColor = ClassUtils.BackColor;
            this.ForeColor = ClassUtils.ForeColor;
            this.Cursor = ClassUtils.Cursor;
            ApplyDefaultLanguage();
            BuildTitleBar();
            BuildButtons();
            PositionButtons();
            this.ResizeEnd += (s, e) => PositionButtons();
            this.SizeChanged += (s, e) => PositionButtons();

#if TESTLANGUAGE
        menu = new MenuStrip();
        langMenu = new ToolStripMenuItem("Langue");
            AddLang("Français", "fr-FR");
            AddLang("English", "en-US");
            AddLang("Deutsch", "de-DE");
            AddLang("Español", "es-ES");
            AddLang("Italiano", "it-IT");
            AddLang("Português (Portugal)", "pt-PT");
            AddLang("Português (Brasil)", "pt-BR");
            AddLang("Nederlands", "nl-NL");
            AddLang("Svenska", "sv-SE");
            AddLang("Norsk", "no-NO");
            AddLang("Dansk", "da-DK");
            AddLang("Suomi", "fi-FI");
            AddLang("Polski", "pl-PL");
            AddLang("Čeština", "cs-CZ");
            AddLang("Slovenčina", "sk-SK");
            AddLang("Magyar", "hu-HU");
            AddLang("Română", "ro-RO");
            AddLang("Türkçe", "tr-TR");
            AddLang("日本語", "ja-JP");
            AddLang("中文 (简体)", "zh-CN");
            menu.Items.Add(langMenu);
            this.MainMenuStrip = menu;
            this.Controls.Add(menu);
            menu.Dock = DockStyle.Top;
#endif
        }
#region ROUNDANGLES 
        //protected override void OnHandleCreated(EventArgs e)
        //{
        //    base.OnHandleCreated(e);
        //    ApplyWindowCorners();
        //}
        //protected virtual bool UseRoundedCorners => true;

        //private void ApplyWindowCorners()
        //{
        //    if (!UseRoundedCorners)
        //        return;

        //    // Windows 11 = build >= 22000
        //    if (Environment.OSVersion.Version.Build < 22000)
        //        return;

        //    const int DWMWA_WINDOW_CORNER_PREFERENCE = 33;
        //    const int DWMWCP_ROUND = 2;

        //    int preference = DWMWCP_ROUND;

        //    DwmSetWindowAttribute(
        //        this.Handle,
        //        DWMWA_WINDOW_CORNER_PREFERENCE,
        //        ref preference,
        //        sizeof(int)
        //    );
        //}

        //[DllImport("dwmapi.dll")]
        //private static extern int DwmSetWindowAttribute(
        //    IntPtr hwnd,
        //    int attr,
        //    ref int attrValue,
        //    int attrSize
        //);
#endregion

        protected TableLayoutPanel layout;
        protected void InitLayout(params (Control control, SizeType sizeType, float size)[] rows)
        {
            layout = new TableLayoutPanel();
            layout.Dock = DockStyle.Fill;
            layout.ColumnCount = 1;

            AddRow(titleBar, SizeType.Absolute, 30f);

            foreach (var row in rows)
                AddRow(row.control, row.sizeType, row.size);

            Controls.Add(layout);
        }
        // -------------------------------
        //  TITLE BAR
        // -------------------------------
        private Panel BuildTitleBar()
        {

            titleBar = new Panel
            {
                Height = 30,
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(45, 45, 48)
            };
            titleLabel = new Label();
            titleLabel.AutoSize = true;
            titleLabel.TextAlign = ContentAlignment.MiddleLeft;
            titleLabel.Dock = DockStyle.Left;
            titleLabel.ForeColor = Color.White;
            titleLabel.Font = new Font("Segoe UI", 10);
            titleBar.Controls.Add(titleLabel);
            titleBar.MouseDown += TitleBar_MouseDown;
            titleLabel.MouseDown += TitleBar_MouseDown;
            return titleBar;
        }
        public int AddRow(Control control, SizeType sizeType = SizeType.Absolute, float size = 30f)
        {
            int row = layout.RowCount;

            layout.RowCount++;

            layout.RowStyles.Add(new RowStyle(sizeType, size));

            layout.Controls.Add(control, 0, row);

            return row;
        }
        // -------------------------------
        //  BUTTONS
        // -------------------------------
        // ID 900: Réduire
        //ID 901: Agrandir
        //ID 902: Niveau sup.
        //ID 903: Niveau inf.
        //ID 904: Aide
        //ID 905: Fermer
        private ToolTip toolTip;
        private void BuildButtons()
        {
            btnTopMost = CreateButton("\uF156", ToggleTopMost, -1);
            var rm = new ResourceManager("SDRSharp.Rtl_433.Properties.Resources", typeof(BaseFormWithTopMost).Assembly);
            string txt = rm.GetString("Tooltip_AlwaysOnTop");


            toolTip = new ToolTip();
            toolTip.ShowAlways = true;
            toolTip.InitialDelay = 300;
            toolTip.ReshowDelay = 100;
            toolTip.AutoPopDelay = 5000;
            toolTip.SetToolTip(btnTopMost, txt);
            btnMin = CreateButton("\uE921", () => ShowWindow(this.Handle, SW_MINIMIZE),900);
            btnMax = CreateButton("\uE922", ToggleMaximize,901);
            btnClose = CreateButton("\uE8BB", () => this.Close(),905);
            btnClose.MouseEnter += (s, e) => btnClose.BackColor = Color.FromArgb(232, 17, 35);
            btnClose.MouseLeave += (s, e) => btnClose.BackColor = Color.Transparent;
            titleBar.Controls.Add(btnTopMost);
            titleBar.Controls.Add(btnMin);
            titleBar.Controls.Add(btnMax);
            titleBar.Controls.Add(btnClose);
        }

        private Button CreateButton(string text, Action onClick,int codeTxt)
        {
            var b = new Button();
            b.Text = text;
            b.Font = new Font("Segoe MDL2 Assets", 10);
            b.Width = 45;
            b.Height = 32;
            b.FlatStyle = FlatStyle.Flat;
            b.FlatAppearance.BorderSize = 0;
            b.BackColor = Color.Transparent;
            b.ForeColor = Color.White;
            b.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            if(codeTxt>=0)
            {
            toolTip = new ToolTip();
            toolTip.ShowAlways = true;
            toolTip.InitialDelay = 300;
            toolTip.ReshowDelay = 100;
            toolTip.AutoPopDelay = 5000;

            toolTip.SetToolTip(b, LoadSystemString((uint)codeTxt));
            }
            b.Click += (s, e) => onClick();
            return b;
        }

        public void PositionButtons()
        {
            int right = this.Width;

            btnClose.Left = right - 50;
            btnMax.Left = right - 95;
            btnMin.Left = right - 140;
            btnTopMost.Left = right - 185;
        }

        // -------------------------------
        //  TOPMOST
        // -------------------------------
        private void ToggleTopMost()
        {
            isTopMost = !isTopMost;
            SetWindowPos(this.Handle,
                isTopMost ? HWND_TOPMOST : HWND_NOTOPMOST,
                0, 0, 0, 0,
                SWP_NOMOVE | SWP_NOSIZE);

            btnTopMost.BackColor = isTopMost ? Color.Cyan : Color.Transparent;
        }

        // -------------------------------
        //  MAXIMIZE / RESTORE
        // -------------------------------
        private void ToggleMaximize()
        {
            if (this.WindowState == FormWindowState.Maximized)
                ShowWindow(this.Handle, SW_RESTORE);
            else
                ShowWindow(this.Handle, SW_MAXIMIZE);

            btnMax.Text = this.WindowState == FormWindowState.Maximized ? "\uE923" : "\uE922";
            PositionButtons();
        }

        // -------------------------------
        //  DRAG WINDOW
        // -------------------------------
        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0xA1, 0x2, 0);
            }
        }

        // -------------------------------
        //  RESIZE VIA WndProc
        // -------------------------------
        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x84;
            const int RESIZE_HANDLE_SIZE = 8;
            if (m.Msg == WM_NCHITTEST)
            {
                base.WndProc(ref m);

                Point cursor = PointToClient(new Point(m.LParam.ToInt32()));
                bool left = cursor.X <= RESIZE_HANDLE_SIZE;
                bool right = cursor.X >= this.ClientSize.Width - RESIZE_HANDLE_SIZE;
                bool top = cursor.Y <= RESIZE_HANDLE_SIZE;
                bool bottom = cursor.Y >= this.ClientSize.Height - RESIZE_HANDLE_SIZE;

                if (left && top) { m.Result = (IntPtr)13; return; }
                if (right && top) { m.Result = (IntPtr)14; return; }
                if (left && bottom) { m.Result = (IntPtr)16; return; }
                if (right && bottom) { m.Result = (IntPtr)17; return; }
                if (left) { m.Result = (IntPtr)10; return; }
                if (right) { m.Result = (IntPtr)11; return; }
                if (top) { m.Result = (IntPtr)12; return; }
                if (bottom) { m.Result = (IntPtr)15; return; }

                return;
            }

            base.WndProc(ref m);
        }

        // -------------------------------
        //  PUBLIC API
        // -------------------------------
        public string TitleText
        {
            get => titleLabel.Text;
            set => titleLabel.Text = value;
        }
 
        private static readonly System.Collections.Generic.HashSet<string> SupportedCultures = new System.Collections.Generic.HashSet<string>
        {
            "fr-FR",
            "en-US",
            "de-DE",
            "es-ES",
            "it-IT",
            "pt-PT",
            "pt-BR",
            "nl-NL",
            "sv-SE",
            "no-NO",
            "da-DK",
            "fi-FI",
            "pl-PL",
            "cs-CZ",
            "sk-SK",
            "hu-HU",
            "ro-RO",
            "tr-TR",
            "ja-JP",
            "zh-CN"
        };
        private void ApplyDefaultLanguage()
        {
            string systemCulture = Thread.CurrentThread.CurrentUICulture.Name;

            if (!SupportedCultures.Contains(systemCulture))
            {
                // fallback to english
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            }
        }
        // -------------------------------
        //  WIN32
        // -------------------------------
        [DllImport("user32.dll")] static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")] static extern void ReleaseCapture();
        [DllImport("user32.dll")] static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")] static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        static extern int LoadString(IntPtr hInstance, uint uID, StringBuilder lpBuffer, int nBufferMax);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        static extern IntPtr LoadLibrary(string lpFileName);

        string LoadSystemString(uint id)
        {
            IntPtr h = LoadLibrary("user32.dll");
            StringBuilder sb = new StringBuilder(256);
            LoadString(h, id, sb, sb.Capacity);
            return sb.ToString();
        }

        const int SW_MINIMIZE = 6;
        const int SW_MAXIMIZE = 3;
        const int SW_RESTORE = 9;

        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);

        const uint SWP_NOSIZE = 0x0001;
        const uint SWP_NOMOVE = 0x0002;

#if TESTLANGUAGE
        //var menu = new MenuStrip();
        //var langMenu = new ToolStripMenuItem("Langue");

        private void AddLang(string name, string culture)
        {
            langMenu.DropDownItems.Add(name, null, (s, e) => SetLanguage(culture));
        }

    public void SetLanguage(string culture)
    {
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
        Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);

            // Recharge l’interface
            RebuildBaseUI();
        }
        private void RebuildBaseUI()
        {
            //Controls.Clear();--->clear all
            BuildTitleBar();
            BuildButtons();
            PositionButtons();
        }
#endif
    }
}

