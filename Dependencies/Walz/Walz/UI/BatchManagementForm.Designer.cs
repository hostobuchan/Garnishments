namespace Walz.Data.UI
{
    partial class BatchManagementForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BatchManagementForm));
            this.label1 = new System.Windows.Forms.Label();
            this.txtBatchID = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtItems = new System.Windows.Forms.Label();
            this.btnViewItems = new System.Windows.Forms.LinkLabel();
            this.grpControl = new System.Windows.Forms.GroupBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.chkAutoDownload = new System.Windows.Forms.CheckBox();
            this.grpControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Batch ID#";
            // 
            // txtBatchID
            // 
            this.txtBatchID.AutoSize = true;
            this.txtBatchID.Location = new System.Drawing.Point(75, 13);
            this.txtBatchID.Name = "txtBatchID";
            this.txtBatchID.Size = new System.Drawing.Size(91, 13);
            this.txtBatchID.TabIndex = 1;
            this.txtBatchID.Text = "Not Yet Attributed";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Items:";
            // 
            // txtItems
            // 
            this.txtItems.AutoSize = true;
            this.txtItems.Location = new System.Drawing.Point(57, 41);
            this.txtItems.Name = "txtItems";
            this.txtItems.Size = new System.Drawing.Size(13, 13);
            this.txtItems.TabIndex = 3;
            this.txtItems.Text = "0";
            // 
            // btnViewItems
            // 
            this.btnViewItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewItems.AutoSize = true;
            this.btnViewItems.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.btnViewItems.Location = new System.Drawing.Point(208, 41);
            this.btnViewItems.Name = "btnViewItems";
            this.btnViewItems.Size = new System.Drawing.Size(58, 13);
            this.btnViewItems.TabIndex = 4;
            this.btnViewItems.TabStop = true;
            this.btnViewItems.Text = "View Items";
            this.btnViewItems.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnViewItems_LinkClicked);
            // 
            // grpControl
            // 
            this.grpControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpControl.Controls.Add(this.txtStatus);
            this.grpControl.Controls.Add(this.label3);
            this.grpControl.Controls.Add(this.btnDownload);
            this.grpControl.Controls.Add(this.btnUpload);
            this.grpControl.Location = new System.Drawing.Point(12, 98);
            this.grpControl.Name = "grpControl";
            this.grpControl.Size = new System.Drawing.Size(260, 111);
            this.grpControl.TabIndex = 5;
            this.grpControl.TabStop = false;
            // 
            // txtStatus
            // 
            this.txtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatus.Enabled = false;
            this.txtStatus.Location = new System.Drawing.Point(7, 48);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(247, 57);
            this.txtStatus.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(122, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "->";
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDownload.Location = new System.Drawing.Point(160, 19);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(75, 23);
            this.btnDownload.TabIndex = 2;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnUpload.Location = new System.Drawing.Point(25, 19);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 0;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // chkAutoDownload
            // 
            this.chkAutoDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAutoDownload.AutoSize = true;
            this.chkAutoDownload.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAutoDownload.Location = new System.Drawing.Point(79, 75);
            this.chkAutoDownload.Name = "chkAutoDownload";
            this.chkAutoDownload.Size = new System.Drawing.Size(193, 17);
            this.chkAutoDownload.TabIndex = 6;
            this.chkAutoDownload.Text = "Automatically Check for Downloads";
            this.chkAutoDownload.UseVisualStyleBackColor = true;
            // 
            // BatchManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 221);
            this.Controls.Add(this.chkAutoDownload);
            this.Controls.Add(this.grpControl);
            this.Controls.Add(this.btnViewItems);
            this.Controls.Add(this.txtItems);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBatchID);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BatchManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Walz Submission Manager";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BatchManagementForm_FormClosing);
            this.grpControl.ResumeLayout(false);
            this.grpControl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label txtBatchID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label txtItems;
        private System.Windows.Forms.LinkLabel btnViewItems;
        private System.Windows.Forms.GroupBox grpControl;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.CheckBox chkAutoDownload;
    }
}