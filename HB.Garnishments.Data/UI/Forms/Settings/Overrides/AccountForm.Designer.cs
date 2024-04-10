namespace HB.Garnishments.UI.Forms.Settings.Overrides
{
    partial class AccountForm
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
            this.grpRequests = new System.Windows.Forms.GroupBox();
            this.lstAssets = new System.Windows.Forms.ListBox();
            this.pnlAsset = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // grpRequests
            // 
            this.grpRequests.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpRequests.Location = new System.Drawing.Point(13, 228);
            this.grpRequests.Name = "grpRequests";
            this.grpRequests.Size = new System.Drawing.Size(775, 210);
            this.grpRequests.TabIndex = 1;
            this.grpRequests.TabStop = false;
            // 
            // lstAssets
            // 
            this.lstAssets.FormattingEnabled = true;
            this.lstAssets.IntegralHeight = false;
            this.lstAssets.Location = new System.Drawing.Point(13, 13);
            this.lstAssets.Name = "lstAssets";
            this.lstAssets.Size = new System.Drawing.Size(256, 209);
            this.lstAssets.TabIndex = 2;
            this.lstAssets.SelectedIndexChanged += new System.EventHandler(this.lstAssets_SelectedIndexChanged);
            // 
            // pnlAsset
            // 
            this.pnlAsset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlAsset.Location = new System.Drawing.Point(276, 13);
            this.pnlAsset.Name = "pnlAsset";
            this.pnlAsset.Size = new System.Drawing.Size(512, 209);
            this.pnlAsset.TabIndex = 3;
            // 
            // AccountForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlAsset);
            this.Controls.Add(this.lstAssets);
            this.Controls.Add(this.grpRequests);
            this.Icon = Properties.Resources.clipboard_icon;
            this.Name = "AccountForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Account Form";
            this.Load += new System.EventHandler(this.AccountForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpRequests;
        private System.Windows.Forms.ListBox lstAssets;
        private System.Windows.Forms.Panel pnlAsset;
    }
}