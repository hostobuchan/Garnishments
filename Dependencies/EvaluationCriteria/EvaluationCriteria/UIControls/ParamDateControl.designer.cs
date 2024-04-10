namespace EvaluationCriteria.CriteriaSets.Controls
{
    partial class ParamDateControl
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
            this.VALUE = new System.Windows.Forms.TextBox();
            this.lblMID = new System.Windows.Forms.Label();
            this.VALUE2 = new System.Windows.Forms.TextBox();
            this.lblEnd = new System.Windows.Forms.Label();
            this.DONE = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PARAM
            // 
            this.PARAM.FormattingEnabled = true;
            this.PARAM.Items.AddRange(new object[] {
            "Not Used",
            "Exists",
            "Does Not Exist",
            "Null or More Than",
            "Less Than",
            "Between",
            "Exists More Than"});
            this.PARAM.Location = new System.Drawing.Point(3, 12);
            this.PARAM.Name = "PARAM";
            this.PARAM.Size = new System.Drawing.Size(156, 21);
            this.PARAM.TabIndex = 6;
            this.PARAM.SelectedIndexChanged += new System.EventHandler(this.PARAM_SelectedIndexChanged);
            // 
            // VALUE
            // 
            this.VALUE.Location = new System.Drawing.Point(166, 12);
            this.VALUE.Name = "VALUE";
            this.VALUE.Size = new System.Drawing.Size(62, 20);
            this.VALUE.TabIndex = 7;
            this.VALUE.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.VALUE_KeyPress);
            // 
            // lblMID
            // 
            this.lblMID.AutoSize = true;
            this.lblMID.Location = new System.Drawing.Point(234, 15);
            this.lblMID.Name = "lblMID";
            this.lblMID.Size = new System.Drawing.Size(31, 13);
            this.lblMID.TabIndex = 8;
            this.lblMID.Text = "Days";
            // 
            // VALUE2
            // 
            this.VALUE2.Location = new System.Drawing.Point(275, 12);
            this.VALUE2.Name = "VALUE2";
            this.VALUE2.Size = new System.Drawing.Size(62, 20);
            this.VALUE2.TabIndex = 9;
            this.VALUE2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.VALUE_KeyPress);
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Location = new System.Drawing.Point(343, 15);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(31, 13);
            this.lblEnd.TabIndex = 10;
            this.lblEnd.Text = "Days";
            // 
            // DONE
            // 
            this.DONE.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.DONE.Location = new System.Drawing.Point(154, 38);
            this.DONE.Name = "DONE";
            this.DONE.Size = new System.Drawing.Size(75, 23);
            this.DONE.TabIndex = 11;
            this.DONE.Text = "Done";
            this.DONE.UseVisualStyleBackColor = true;
            this.DONE.Click += new System.EventHandler(this.DONE_Click);
            // 
            // ParamDateControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DONE);
            this.Controls.Add(this.lblEnd);
            this.Controls.Add(this.VALUE2);
            this.Controls.Add(this.lblMID);
            this.Controls.Add(this.VALUE);
            this.Controls.Add(this.PARAM);
            this.Name = "ParamDateControl";
            this.Size = new System.Drawing.Size(382, 64);
            this.Load += new System.EventHandler(this.ParamDateControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox PARAM;
        private System.Windows.Forms.TextBox VALUE;
        private System.Windows.Forms.Label lblMID;
        private System.Windows.Forms.TextBox VALUE2;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.Button DONE;
    }
}
