namespace SDRSharp.Rtl_433
{
    partial class FormDevicesListMessages
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
            this.listViewListMessages = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // listViewListMessages
            // 
            this.listViewListMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewListMessages.GridLines = true;
            this.listViewListMessages.HideSelection = false;
            this.listViewListMessages.Location = new System.Drawing.Point(0, 0);
            this.listViewListMessages.Name = "listViewListMessages";
            this.listViewListMessages.Size = new System.Drawing.Size(800, 450);
            this.listViewListMessages.TabIndex = 0;
            this.listViewListMessages.UseCompatibleStateImageBehavior = false;
            this.listViewListMessages.View = System.Windows.Forms.View.List;
            // 
            // FormDevicesListMessages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listViewListMessages);
            this.Name = "FormDevicesListMessages";
            this.Text = "FormDevicesListMessages";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewListMessages;
    }
}