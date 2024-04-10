namespace System.Windows.Forms
{
    partial class MultiProgressForm
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
            this.pbTotalProgress = new System.Windows.Forms.ProgressBar();
            this.pbSectionProgress = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // pbTotalProgress
            // 
            this.pbTotalProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbTotalProgress.Location = new System.Drawing.Point(13, 42);
            this.pbTotalProgress.Name = "pbTotalProgress";
            this.pbTotalProgress.Size = new System.Drawing.Size(259, 23);
            this.pbTotalProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbTotalProgress.TabIndex = 0;
            // 
            // pbSectionProgress
            // 
            this.pbSectionProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSectionProgress.Location = new System.Drawing.Point(13, 13);
            this.pbSectionProgress.Name = "pbSectionProgress";
            this.pbSectionProgress.Size = new System.Drawing.Size(259, 23);
            this.pbSectionProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbSectionProgress.TabIndex = 1;
            // 
            // MultiProgressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 77);
            this.ControlBox = false;
            this.Controls.Add(this.pbSectionProgress);
            this.Controls.Add(this.pbTotalProgress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MultiProgressForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);

        }

        #endregion

        private ProgressBar pbTotalProgress;
        private ProgressBar pbSectionProgress;
    }
}