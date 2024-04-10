namespace HB.Garnishments.UI.Controls.Requests
{
    partial class AssetRequestControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lstRequests = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lstRequests
            // 
            this.lstRequests.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstRequests.FormattingEnabled = true;
            this.lstRequests.IntegralHeight = false;
            this.lstRequests.Location = new System.Drawing.Point(3, 3);
            this.lstRequests.Name = "lstRequests";
            this.lstRequests.Size = new System.Drawing.Size(743, 144);
            this.lstRequests.TabIndex = 0;
            this.lstRequests.DoubleClick += new System.EventHandler(this.lstRequests_DoubleClick);
            // 
            // AssetRequestControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lstRequests);
            this.Name = "AssetRequestControl";
            this.Size = new System.Drawing.Size(749, 150);
            this.Load += new System.EventHandler(this.AssetRequestControl_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstRequests;
    }
}
