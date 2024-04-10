namespace EvaluationCriteria.CriteriaSets.Controls
{
    partial class CriteriaOptionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CriteriaOptionForm));
            this.IS = new System.Windows.Forms.ListBox();
            this.NOT = new System.Windows.Forms.ListBox();
            this.REMOVE_IS = new System.Windows.Forms.Button();
            this.ADD_IS = new System.Windows.Forms.Button();
            this.REMOVE_NOT = new System.Windows.Forms.Button();
            this.ADD_NOT = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CLOSE = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // IS
            // 
            this.IS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IS.FormattingEnabled = true;
            this.IS.Location = new System.Drawing.Point(12, 38);
            this.IS.Name = "IS";
            this.IS.Size = new System.Drawing.Size(260, 121);
            this.IS.TabIndex = 0;
            this.IS.DoubleClick += new System.EventHandler(this.IS_DoubleClick);
            // 
            // NOT
            // 
            this.NOT.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NOT.FormattingEnabled = true;
            this.NOT.Location = new System.Drawing.Point(12, 204);
            this.NOT.Name = "NOT";
            this.NOT.Size = new System.Drawing.Size(260, 121);
            this.NOT.TabIndex = 1;
            this.NOT.DoubleClick += new System.EventHandler(this.NOT_DoubleClick);
            // 
            // REMOVE_IS
            // 
            this.REMOVE_IS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.REMOVE_IS.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.REMOVE_IS.Location = new System.Drawing.Point(249, 165);
            this.REMOVE_IS.Name = "REMOVE_IS";
            this.REMOVE_IS.Size = new System.Drawing.Size(23, 23);
            this.REMOVE_IS.TabIndex = 4;
            this.REMOVE_IS.Text = "-";
            this.REMOVE_IS.UseVisualStyleBackColor = true;
            this.REMOVE_IS.Click += new System.EventHandler(this.REMOVE_IS_Click);
            // 
            // ADD_IS
            // 
            this.ADD_IS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ADD_IS.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ADD_IS.Location = new System.Drawing.Point(223, 165);
            this.ADD_IS.Name = "ADD_IS";
            this.ADD_IS.Size = new System.Drawing.Size(23, 23);
            this.ADD_IS.TabIndex = 3;
            this.ADD_IS.Text = "+";
            this.ADD_IS.UseVisualStyleBackColor = true;
            this.ADD_IS.Click += new System.EventHandler(this.ADD_IS_Click);
            // 
            // REMOVE_NOT
            // 
            this.REMOVE_NOT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.REMOVE_NOT.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.REMOVE_NOT.Location = new System.Drawing.Point(249, 331);
            this.REMOVE_NOT.Name = "REMOVE_NOT";
            this.REMOVE_NOT.Size = new System.Drawing.Size(23, 23);
            this.REMOVE_NOT.TabIndex = 6;
            this.REMOVE_NOT.Text = "-";
            this.REMOVE_NOT.UseVisualStyleBackColor = true;
            this.REMOVE_NOT.Click += new System.EventHandler(this.REMOVE_NOT_Click);
            // 
            // ADD_NOT
            // 
            this.ADD_NOT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ADD_NOT.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ADD_NOT.Location = new System.Drawing.Point(223, 331);
            this.ADD_NOT.Name = "ADD_NOT";
            this.ADD_NOT.Size = new System.Drawing.Size(23, 23);
            this.ADD_NOT.TabIndex = 5;
            this.ADD_NOT.Text = "+";
            this.ADD_NOT.UseVisualStyleBackColor = true;
            this.ADD_NOT.Click += new System.EventHandler(this.ADD_NOT_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Accept Reasons";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Reject Reasons";
            // 
            // CLOSE
            // 
            this.CLOSE.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.CLOSE.Location = new System.Drawing.Point(105, 352);
            this.CLOSE.Name = "CLOSE";
            this.CLOSE.Size = new System.Drawing.Size(75, 23);
            this.CLOSE.TabIndex = 9;
            this.CLOSE.Text = "Done";
            this.CLOSE.UseVisualStyleBackColor = true;
            this.CLOSE.Click += new System.EventHandler(this.CLOSE_Click);
            // 
            // CriteriaOptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 387);
            this.Controls.Add(this.CLOSE);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.REMOVE_NOT);
            this.Controls.Add(this.ADD_NOT);
            this.Controls.Add(this.REMOVE_IS);
            this.Controls.Add(this.ADD_IS);
            this.Controls.Add(this.NOT);
            this.Controls.Add(this.IS);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CriteriaOptionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Selected Options";
            this.Load += new System.EventHandler(this.CriteriaOptionForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox IS;
        private System.Windows.Forms.ListBox NOT;
        private System.Windows.Forms.Button REMOVE_IS;
        private System.Windows.Forms.Button ADD_IS;
        private System.Windows.Forms.Button REMOVE_NOT;
        private System.Windows.Forms.Button ADD_NOT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button CLOSE;
    }
}