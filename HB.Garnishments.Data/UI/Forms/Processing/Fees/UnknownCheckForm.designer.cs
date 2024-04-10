namespace HB.Garnishments.UI.Forms.Processing.Fees
{
    public partial class UnknownCheckForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UnknownCheckForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.COST = new System.Windows.Forms.TextBox();
            this.CODE = new System.Windows.Forms.TextBox();
            this.ADD = new System.Windows.Forms.Button();
            this.DONE = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.COUNTY = new System.Windows.Forms.Label();
            this.INFO = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Check Amount";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(110, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Cost Code";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "$";
            // 
            // COST
            // 
            this.COST.Location = new System.Drawing.Point(31, 103);
            this.COST.MaxLength = 6;
            this.COST.Name = "COST";
            this.COST.Size = new System.Drawing.Size(58, 20);
            this.COST.TabIndex = 3;
            // 
            // CODE
            // 
            this.CODE.Location = new System.Drawing.Point(108, 102);
            this.CODE.MaxLength = 2;
            this.CODE.Name = "CODE";
            this.CODE.Size = new System.Drawing.Size(58, 20);
            this.CODE.TabIndex = 4;
            // 
            // ADD
            // 
            this.ADD.Location = new System.Drawing.Point(176, 101);
            this.ADD.Name = "ADD";
            this.ADD.Size = new System.Drawing.Size(75, 23);
            this.ADD.TabIndex = 5;
            this.ADD.Text = "Add Check";
            this.ADD.UseVisualStyleBackColor = true;
            this.ADD.Click += new System.EventHandler(this.ADD_Click);
            // 
            // DONE
            // 
            this.DONE.Location = new System.Drawing.Point(257, 101);
            this.DONE.Name = "DONE";
            this.DONE.Size = new System.Drawing.Size(75, 23);
            this.DONE.TabIndex = 6;
            this.DONE.Text = "Done";
            this.DONE.UseVisualStyleBackColor = true;
            this.DONE.Click += new System.EventHandler(this.DONE_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(70, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(262, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "* Click \"Add\" For More Than One Check. Otherwise Click Done";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(5, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(335, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Could Not Determine Checks For Accounting Spreadsheet";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "County:";
            // 
            // COUNTY
            // 
            this.COUNTY.AutoSize = true;
            this.COUNTY.Location = new System.Drawing.Point(80, 37);
            this.COUNTY.Name = "COUNTY";
            this.COUNTY.Size = new System.Drawing.Size(0, 13);
            this.COUNTY.TabIndex = 10;
            // 
            // INFO
            // 
            this.INFO.AutoSize = true;
            this.INFO.Location = new System.Drawing.Point(31, 60);
            this.INFO.Name = "INFO";
            this.INFO.Size = new System.Drawing.Size(25, 13);
            this.INFO.TabIndex = 11;
            this.INFO.Text = "Info";
            // 
            // UnknownCheckForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 155);
            this.Controls.Add(this.INFO);
            this.Controls.Add(this.COUNTY);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DONE);
            this.Controls.Add(this.ADD);
            this.Controls.Add(this.CODE);
            this.Controls.Add(this.COST);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UnknownCheckForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UnknownCheckForm";
            this.Load += new System.EventHandler(this.UnknownCheckForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox COST;
        private System.Windows.Forms.TextBox CODE;
        private System.Windows.Forms.Button ADD;
        private System.Windows.Forms.Button DONE;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label COUNTY;
        private System.Windows.Forms.Label INFO;
    }
}