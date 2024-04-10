namespace EvaluationCriteria.CriteriaSets.Controls
{
    partial class ParamOptControl
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
            this.btnCLM = new System.Windows.Forms.LinkLabel();
            this.VALUE = new System.Windows.Forms.ComboBox();
            this.PARAM = new System.Windows.Forms.ComboBox();
            this.DONE = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCLM
            // 
            this.btnCLM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCLM.AutoSize = true;
            this.btnCLM.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.btnCLM.Location = new System.Drawing.Point(283, 44);
            this.btnCLM.Name = "btnCLM";
            this.btnCLM.Size = new System.Drawing.Size(96, 13);
            this.btnCLM.TabIndex = 20;
            this.btnCLM.TabStop = true;
            this.btnCLM.Text = "Code List Manager";
            this.btnCLM.VisitedLinkColor = System.Drawing.Color.Blue;
            this.btnCLM.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnCLM_LinkClicked);
            // 
            // VALUE
            // 
            this.VALUE.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.VALUE.AutoCompleteCustomSource.AddRange(new string[] {
            "Not Used",
            "Equal To",
            "Not Equal To"});
            this.VALUE.FormattingEnabled = true;
            this.VALUE.Items.AddRange(new object[] {
            "Not Used",
            "Is Equal To",
            "Not Equal To"});
            this.VALUE.Location = new System.Drawing.Point(194, 7);
            this.VALUE.Name = "VALUE";
            this.VALUE.Size = new System.Drawing.Size(156, 21);
            this.VALUE.TabIndex = 19;
            // 
            // PARAM
            // 
            this.PARAM.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.PARAM.AutoCompleteCustomSource.AddRange(new string[] {
            "Not Used",
            "Equal To",
            "Not Equal To"});
            this.PARAM.FormattingEnabled = true;
            this.PARAM.Items.AddRange(new object[] {
            "Not Used",
            "Does Not Exist",
            "Exists",
            "In List",
            "Not In List"});
            this.PARAM.Location = new System.Drawing.Point(32, 7);
            this.PARAM.Name = "PARAM";
            this.PARAM.Size = new System.Drawing.Size(156, 21);
            this.PARAM.TabIndex = 18;
            this.PARAM.SelectedIndexChanged += new System.EventHandler(this.PARAM_SelectedIndexChanged);
            // 
            // DONE
            // 
            this.DONE.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.DONE.Location = new System.Drawing.Point(154, 34);
            this.DONE.Name = "DONE";
            this.DONE.Size = new System.Drawing.Size(75, 23);
            this.DONE.TabIndex = 17;
            this.DONE.Text = "Done";
            this.DONE.UseVisualStyleBackColor = true;
            this.DONE.Click += new System.EventHandler(this.DONE_Click);
            // 
            // ParamOptControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCLM);
            this.Controls.Add(this.VALUE);
            this.Controls.Add(this.PARAM);
            this.Controls.Add(this.DONE);
            this.Name = "ParamOptControl";
            this.Size = new System.Drawing.Size(382, 64);
            this.Load += new System.EventHandler(this.ParamOptControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox PARAM;
        private System.Windows.Forms.Button DONE;
        private System.Windows.Forms.ComboBox VALUE;
        private System.Windows.Forms.LinkLabel btnCLM;
    }
}
