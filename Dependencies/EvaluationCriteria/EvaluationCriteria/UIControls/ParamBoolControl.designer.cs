namespace EvaluationCriteria.CriteriaSets.Controls
{
    partial class ParamBoolControl
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
            this.DONE = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.VALUE = new System.Windows.Forms.ComboBox();
            this.btnCLM = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // DONE
            // 
            this.DONE.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.DONE.Location = new System.Drawing.Point(154, 38);
            this.DONE.Name = "DONE";
            this.DONE.Size = new System.Drawing.Size(75, 23);
            this.DONE.TabIndex = 16;
            this.DONE.Text = "Done";
            this.DONE.UseVisualStyleBackColor = true;
            this.DONE.Click += new System.EventHandler(this.DONE_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Select List:";
            // 
            // VALUE
            // 
            this.VALUE.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.VALUE.FormattingEnabled = true;
            this.VALUE.Location = new System.Drawing.Point(122, 10);
            this.VALUE.Name = "VALUE";
            this.VALUE.Size = new System.Drawing.Size(204, 21);
            this.VALUE.TabIndex = 18;
            // 
            // btnCLM
            // 
            this.btnCLM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCLM.AutoSize = true;
            this.btnCLM.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.btnCLM.Location = new System.Drawing.Point(286, 48);
            this.btnCLM.Name = "btnCLM";
            this.btnCLM.Size = new System.Drawing.Size(96, 13);
            this.btnCLM.TabIndex = 19;
            this.btnCLM.TabStop = true;
            this.btnCLM.Text = "Code List Manager";
            this.btnCLM.VisitedLinkColor = System.Drawing.Color.Blue;
            this.btnCLM.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnCLM_LinkClicked);
            // 
            // ParamBoolControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCLM);
            this.Controls.Add(this.VALUE);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DONE);
            this.Name = "ParamBoolControl";
            this.Size = new System.Drawing.Size(382, 64);
            this.Load += new System.EventHandler(this.ParamBoolControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button DONE;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox VALUE;
        private System.Windows.Forms.LinkLabel btnCLM;
    }
}
