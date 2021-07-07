namespace SDRSharp.Rtl_433
{
    partial class FormListDevices
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listDevices = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // listDevices
            // 
            this.listDevices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listDevices.HideSelection = false;
            this.listDevices.Location = new System.Drawing.Point(0, 0);
            this.listDevices.Name = "listDevices";
            this.listDevices.Size = new System.Drawing.Size(800, 450);
            this.listDevices.TabIndex = 0;
            this.listDevices.UseCompatibleStateImageBehavior = false;
            this.listDevices.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.listDevices_RetrieveVirtualItem);
            // 
            // FormListDevices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listDevices);
            this.DoubleBuffered = true;
            this.Name = "FormListDevices";
            this.Text = "FormListDevices";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listDevices;
    }
}