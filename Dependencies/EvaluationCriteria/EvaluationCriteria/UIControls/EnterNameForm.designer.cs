namespace EvaluationCriteria.CriteriaSets.Controls
{
    partial class EnterNameForm
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
            this.NAME = new System.Windows.Forms.TextBox();
            this.OK = new System.Windows.Forms.Button();
            this.CLOSE = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NAME
            // 
            this.NAME.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NAME.Location = new System.Drawing.Point(12, 12);
            this.NAME.Name = "NAME";
            this.NAME.Size = new System.Drawing.Size(222, 20);
            this.NAME.TabIndex = 0;
            this.NAME.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NAME.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NAME_KeyPress);
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(13, 39);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 23);
            this.OK.TabIndex = 1;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // CLOSE
            // 
            this.CLOSE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CLOSE.Location = new System.Drawing.Point(159, 39);
            this.CLOSE.Name = "CLOSE";
            this.CLOSE.Size = new System.Drawing.Size(75, 23);
            this.CLOSE.TabIndex = 2;
            this.CLOSE.Text = "Cancel";
            this.CLOSE.UseVisualStyleBackColor = true;
            this.CLOSE.Click += new System.EventHandler(this.CLOSE_Click);
            // 
            // EnterNameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 72);
            this.ControlBox = false;
            this.Controls.Add(this.CLOSE);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.NAME);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EnterNameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enter Name";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected internal System.Windows.Forms.TextBox NAME;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button CLOSE;
    }
}