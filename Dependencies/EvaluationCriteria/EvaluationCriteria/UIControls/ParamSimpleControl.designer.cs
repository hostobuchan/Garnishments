namespace EvaluationCriteria.CriteriaSets.Controls
{
    partial class ParamSimpleControl
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
            this.PARAM = new System.Windows.Forms.ComboBox();
            this.DONE = new System.Windows.Forms.Button();
            this.SuspendLayout();
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
            "Exists",
            "Does Not Exist"});
            this.PARAM.Location = new System.Drawing.Point(113, 7);
            this.PARAM.Name = "PARAM";
            this.PARAM.Size = new System.Drawing.Size(156, 21);
            this.PARAM.TabIndex = 18;
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
            // ParamSimpleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PARAM);
            this.Controls.Add(this.DONE);
            this.Name = "ParamSimpleControl";
            this.Size = new System.Drawing.Size(382, 64);
            this.Load += new System.EventHandler(this.ParamSimpleControl_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox PARAM;
        private System.Windows.Forms.Button DONE;
    }
}
