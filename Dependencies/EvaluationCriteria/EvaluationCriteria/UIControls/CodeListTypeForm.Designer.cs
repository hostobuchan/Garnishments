namespace EvaluationCriteria.CriteriaSets.Controls
{
    partial class CodeListTypeForm
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
            this.NUMBER = new System.Windows.Forms.Button();
            this.STRING = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NUMBER
            // 
            this.NUMBER.Location = new System.Drawing.Point(61, 12);
            this.NUMBER.Name = "NUMBER";
            this.NUMBER.Size = new System.Drawing.Size(101, 23);
            this.NUMBER.TabIndex = 2;
            this.NUMBER.Text = "Numeric";
            this.NUMBER.UseVisualStyleBackColor = true;
            this.NUMBER.Click += new System.EventHandler(this.NUMBER_Click);
            // 
            // STRING
            // 
            this.STRING.Location = new System.Drawing.Point(61, 45);
            this.STRING.Name = "STRING";
            this.STRING.Size = new System.Drawing.Size(101, 23);
            this.STRING.TabIndex = 3;
            this.STRING.Text = "Text";
            this.STRING.UseVisualStyleBackColor = true;
            this.STRING.Click += new System.EventHandler(this.STRING_Click);
            // 
            // CodeListTypeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(223, 80);
            this.ControlBox = false;
            this.Controls.Add(this.STRING);
            this.Controls.Add(this.NUMBER);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CodeListTypeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button NUMBER;
        private System.Windows.Forms.Button STRING;
    }
}