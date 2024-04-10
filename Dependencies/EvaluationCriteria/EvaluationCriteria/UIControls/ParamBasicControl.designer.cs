namespace EvaluationCriteria.CriteriaSets.Controls
{
    partial class ParamBasicControl
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
            this.PARAM = new System.Windows.Forms.ComboBox();
            this.VALUE = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // DONE
            // 
            this.DONE.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.DONE.Location = new System.Drawing.Point(154, 38);
            this.DONE.Name = "DONE";
            this.DONE.Size = new System.Drawing.Size(75, 23);
            this.DONE.TabIndex = 12;
            this.DONE.Text = "Done";
            this.DONE.UseVisualStyleBackColor = true;
            this.DONE.Click += new System.EventHandler(this.DONE_Click);
            // 
            // PARAM
            // 
            this.PARAM.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.PARAM.FormattingEnabled = true;
            this.PARAM.Items.AddRange(new object[] {
            "Not Used",
            "Is Equal To",
            "Not Equal To",
            "Greater Than",
            "Less Than",
            "Exists",
            "Does Not Exist",
            "More Than or Equal To",
            "Less Than or Equal To"});
            this.PARAM.Location = new System.Drawing.Point(51, 11);
            this.PARAM.Name = "PARAM";
            this.PARAM.Size = new System.Drawing.Size(156, 21);
            this.PARAM.TabIndex = 13;
            this.PARAM.SelectedIndexChanged += new System.EventHandler(this.PARAM_SelectedIndexChanged);
            // 
            // VALUE
            // 
            this.VALUE.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.VALUE.Location = new System.Drawing.Point(231, 11);
            this.VALUE.Name = "VALUE";
            this.VALUE.Size = new System.Drawing.Size(100, 20);
            this.VALUE.TabIndex = 14;
            this.VALUE.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.VALUE_KeyPress);
            // 
            // ParamBasicControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.VALUE);
            this.Controls.Add(this.PARAM);
            this.Controls.Add(this.DONE);
            this.Name = "ParamBasicControl";
            this.Size = new System.Drawing.Size(382, 64);
            this.Load += new System.EventHandler(this.ParamBasicControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button DONE;
        private System.Windows.Forms.ComboBox PARAM;
        private System.Windows.Forms.TextBox VALUE;
    }
}
