

using System;
using System.Windows.Forms;

namespace SDRSharp.Rtl_433
{
    public class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeBaseStyles();
        }

        private void InitializeBaseStyles()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer
                        | ControlStyles.AllPaintingInWmPaint
                        | ControlStyles.UserPaint, true);

            this.DoubleBuffered = true;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            InitializeBaseStyles();
            OnBaseHandleReady();
        }

        protected virtual void OnBaseHandleReady()
        {
            // Hook pour les classes dérivées
        }
    }

}
