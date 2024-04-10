namespace EvaluationCriteria.CriteriaSets.Controls
{
    partial class CodeListManagerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeListManagerForm));
            this.CODELISTS = new System.Windows.Forms.ListBox();
            this.CLOSE = new System.Windows.Forms.Button();
            this.REMOVE = new System.Windows.Forms.Button();
            this.ADD = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CODELISTS
            // 
            this.CODELISTS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CODELISTS.FormattingEnabled = true;
            this.CODELISTS.Location = new System.Drawing.Point(13, 13);
            this.CODELISTS.Name = "CODELISTS";
            this.CODELISTS.Size = new System.Drawing.Size(202, 186);
            this.CODELISTS.TabIndex = 0;
            this.CODELISTS.DoubleClick += new System.EventHandler(this.CODELISTS_DoubleClick);
            // 
            // CLOSE
            // 
            this.CLOSE.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.CLOSE.Location = new System.Drawing.Point(76, 232);
            this.CLOSE.Name = "CLOSE";
            this.CLOSE.Size = new System.Drawing.Size(75, 23);
            this.CLOSE.TabIndex = 1;
            this.CLOSE.Text = "Done";
            this.CLOSE.UseVisualStyleBackColor = true;
            this.CLOSE.Click += new System.EventHandler(this.CLOSE_Click);
            // 
            // REMOVE
            // 
            this.REMOVE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.REMOVE.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.REMOVE.Location = new System.Drawing.Point(192, 205);
            this.REMOVE.Name = "REMOVE";
            this.REMOVE.Size = new System.Drawing.Size(23, 23);
            this.REMOVE.TabIndex = 4;
            this.REMOVE.Text = "-";
            this.REMOVE.UseVisualStyleBackColor = true;
            this.REMOVE.Click += new System.EventHandler(this.REMOVE_Click);
            // 
            // ADD
            // 
            this.ADD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ADD.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ADD.Location = new System.Drawing.Point(166, 205);
            this.ADD.Name = "ADD";
            this.ADD.Size = new System.Drawing.Size(23, 23);
            this.ADD.TabIndex = 3;
            this.ADD.Text = "+";
            this.ADD.UseVisualStyleBackColor = true;
            this.ADD.Click += new System.EventHandler(this.ADD_Click);
            // 
            // CodeListManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 262);
            this.Controls.Add(this.REMOVE);
            this.Controls.Add(this.ADD);
            this.Controls.Add(this.CLOSE);
            this.Controls.Add(this.CODELISTS);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CodeListManagerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Code List Manager";
            this.Load += new System.EventHandler(this.CodeListManagerForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox CODELISTS;
        private System.Windows.Forms.Button CLOSE;
        private System.Windows.Forms.Button REMOVE;
        private System.Windows.Forms.Button ADD;
    }
}