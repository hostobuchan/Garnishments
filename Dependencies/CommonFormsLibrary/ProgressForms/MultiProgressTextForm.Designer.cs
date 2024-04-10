namespace System.Windows.Forms
{
    partial class MultiProgressTextForm
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
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtSubDescription = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // pbTotalProgress
            // 
            this.pbTotalProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbTotalProgress.Location = new System.Drawing.Point(13, 98);
            this.pbTotalProgress.Name = "pbTotalProgress";
            this.pbTotalProgress.Size = new System.Drawing.Size(259, 23);
            this.pbTotalProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbTotalProgress.TabIndex = 0;
            // 
            // pbSectionProgress
            // 
            this.pbSectionProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSectionProgress.Location = new System.Drawing.Point(13, 69);
            this.pbSectionProgress.Name = "pbSectionProgress";
            this.pbSectionProgress.Size = new System.Drawing.Size(259, 23);
            this.pbSectionProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pbSectionProgress.TabIndex = 1;
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.BackColor = System.Drawing.SystemColors.Control;
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.Location = new System.Drawing.Point(13, 12);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(259, 13);
            this.txtDescription.TabIndex = 2;
            this.txtDescription.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtSubDescription
            // 
            this.txtSubDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubDescription.BackColor = System.Drawing.SystemColors.Control;
            this.txtSubDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSubDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubDescription.Location = new System.Drawing.Point(12, 41);
            this.txtSubDescription.Name = "txtSubDescription";
            this.txtSubDescription.Size = new System.Drawing.Size(259, 13);
            this.txtSubDescription.TabIndex = 3;
            this.txtSubDescription.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // MultiProgressTextForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 133);
            this.ControlBox = false;
            this.Controls.Add(this.txtSubDescription);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.pbSectionProgress);
            this.Controls.Add(this.pbTotalProgress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MultiProgressTextForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ProgressBar pbTotalProgress;
        private ProgressBar pbSectionProgress;
        private TextBox txtDescription;
        private TextBox txtSubDescription;
    }
}