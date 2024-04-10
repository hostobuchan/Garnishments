namespace System.Windows.Forms
{
    partial class DragSortListControl
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
            this.flpListBox = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flpListBox
            // 
            this.flpListBox.AllowDrop = true;
            this.flpListBox.AutoScroll = true;
            this.flpListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpListBox.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpListBox.Location = new System.Drawing.Point(0, 0);
            this.flpListBox.Margin = new System.Windows.Forms.Padding(0);
            this.flpListBox.Name = "flpListBox";
            this.flpListBox.Size = new System.Drawing.Size(150, 150);
            this.flpListBox.TabIndex = 0;
            this.flpListBox.WrapContents = false;
            this.flpListBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.SortableFlowLayoutPanel_DragDrop);
            this.flpListBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.SortableFlowLayoutPanel_DragEnter);
            this.flpListBox.DragOver += new System.Windows.Forms.DragEventHandler(this.SortableFlowLayoutPanel_DragOver);
            this.flpListBox.DragLeave += new System.EventHandler(this.SortableFlowLayoutPanel_DragLeave);
            this.flpListBox.Resize += new System.EventHandler(this.SortableFlowLayoutPanel_Resize);
            // 
            // DragSortListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flpListBox);
            this.DoubleBuffered = true;
            this.Name = "DragSortListControl";
            this.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.DragSortListControl_GiveFeedback);
            this.ResumeLayout(false);

        }

        #endregion

        internal FlowLayoutPanel flpListBox;

    }
}
