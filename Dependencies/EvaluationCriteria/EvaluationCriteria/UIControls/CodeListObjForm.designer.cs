namespace EvaluationCriteria.CriteriaSets.Controls
{
    partial class CodeListObjForm<T>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeListListForm));
            this.DaysLbl = new System.Windows.Forms.Label();
            this.ADD_CODE = new System.Windows.Forms.Button();
            this.DAYS_IN = new System.Windows.Forms.TextBox();
            this.CONST_IN = new System.Windows.Forms.ComboBox();
            this.CODE_IN = new System.Windows.Forms.TextBox();
            this.CODES = new System.Windows.Forms.ListBox();
            this.CLOSE = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DaysLbl
            // 
            this.DaysLbl.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.DaysLbl.AutoSize = true;
            this.DaysLbl.Location = new System.Drawing.Point(33, 43);
            this.DaysLbl.Name = "DaysLbl";
            this.DaysLbl.Size = new System.Drawing.Size(34, 13);
            this.DaysLbl.TabIndex = 10;
            this.DaysLbl.Text = "Days:";
            // 
            // ADD_CODE
            // 
            this.ADD_CODE.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.ADD_CODE.Location = new System.Drawing.Point(122, 39);
            this.ADD_CODE.Name = "ADD_CODE";
            this.ADD_CODE.Size = new System.Drawing.Size(72, 20);
            this.ADD_CODE.TabIndex = 9;
            this.ADD_CODE.Text = "ADD";
            this.ADD_CODE.UseVisualStyleBackColor = true;
            this.ADD_CODE.Click += new System.EventHandler(this.ADD_CODE_Click);
            // 
            // DAYS_IN
            // 
            this.DAYS_IN.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.DAYS_IN.Location = new System.Drawing.Point(73, 39);
            this.DAYS_IN.MaxLength = 3;
            this.DAYS_IN.Name = "DAYS_IN";
            this.DAYS_IN.Size = new System.Drawing.Size(42, 20);
            this.DAYS_IN.TabIndex = 8;
            this.DAYS_IN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CODE_IN_KeyPress);
            // 
            // CONST_IN
            // 
            this.CONST_IN.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.CONST_IN.FormattingEnabled = true;
            this.CONST_IN.Items.AddRange(new object[] {
            "Exists",
            "In The Past",
            "In The Future",
            "Over X Days Away",
            "Under X Days Away",
            "Over X Days Ago",
            "Under X Days Ago",
            "Doesn\'t Exist"});
            this.CONST_IN.Location = new System.Drawing.Point(73, 12);
            this.CONST_IN.Name = "CONST_IN";
            this.CONST_IN.Size = new System.Drawing.Size(122, 21);
            this.CONST_IN.TabIndex = 7;
            // 
            // CODE_IN
            // 
            this.CODE_IN.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.CODE_IN.Location = new System.Drawing.Point(24, 12);
            this.CODE_IN.MaxLength = 5;
            this.CODE_IN.Name = "CODE_IN";
            this.CODE_IN.Size = new System.Drawing.Size(42, 20);
            this.CODE_IN.TabIndex = 6;
            this.CODE_IN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CODE_IN_KeyPress);
            // 
            // CODES
            // 
            this.CODES.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CODES.FormattingEnabled = true;
            this.CODES.Location = new System.Drawing.Point(24, 65);
            this.CODES.Name = "CODES";
            this.CODES.Size = new System.Drawing.Size(170, 238);
            this.CODES.TabIndex = 11;
            this.CODES.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CODES_KeyDown);
            // 
            // CLOSE
            // 
            this.CLOSE.Location = new System.Drawing.Point(72, 310);
            this.CLOSE.Name = "CLOSE";
            this.CLOSE.Size = new System.Drawing.Size(75, 23);
            this.CLOSE.TabIndex = 12;
            this.CLOSE.Text = "Done";
            this.CLOSE.UseVisualStyleBackColor = true;
            this.CLOSE.Click += new System.EventHandler(this.CLOSE_Click);
            // 
            // CodeListListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(219, 345);
            this.Controls.Add(this.CLOSE);
            this.Controls.Add(this.CODES);
            this.Controls.Add(this.DaysLbl);
            this.Controls.Add(this.ADD_CODE);
            this.Controls.Add(this.DAYS_IN);
            this.Controls.Add(this.CONST_IN);
            this.Controls.Add(this.CODE_IN);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CodeListListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Code Listing";
            this.Load += new System.EventHandler(this.CodeListListForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label DaysLbl;
        private System.Windows.Forms.Button ADD_CODE;
        private System.Windows.Forms.TextBox DAYS_IN;
        private System.Windows.Forms.ComboBox CONST_IN;
        private System.Windows.Forms.TextBox CODE_IN;
        private System.Windows.Forms.ListBox CODES;
        private System.Windows.Forms.Button CLOSE;
    }
}