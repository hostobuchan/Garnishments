namespace HB.Garnishments.UI.Controls.Settings.Results
{
    partial class ResultCodesControl
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
            this.lstCodes = new System.Windows.Forms.ListBox();
            this.pnlCodeValues = new System.Windows.Forms.Panel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstCodes
            // 
            this.lstCodes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstCodes.FormattingEnabled = true;
            this.lstCodes.IntegralHeight = false;
            this.lstCodes.Location = new System.Drawing.Point(4, 32);
            this.lstCodes.Name = "lstCodes";
            this.lstCodes.Size = new System.Drawing.Size(180, 239);
            this.lstCodes.TabIndex = 0;
            this.lstCodes.SelectedIndexChanged += new System.EventHandler(this.lstCodes_SelectedIndexChanged);
            this.lstCodes.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstCodes_KeyUp);
            // 
            // pnlCodeValues
            // 
            this.pnlCodeValues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCodeValues.Location = new System.Drawing.Point(186, 4);
            this.pnlCodeValues.Name = "pnlCodeValues";
            this.pnlCodeValues.Size = new System.Drawing.Size(326, 267);
            this.pnlCodeValues.TabIndex = 1;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(4, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(180, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add Merge Code";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // ResultCodesPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.pnlCodeValues);
            this.Controls.Add(this.lstCodes);
            this.Name = "ResultCodesPanel";
            this.Size = new System.Drawing.Size(512, 274);
            this.Load += new System.EventHandler(this.ResultCodesPanel_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstCodes;
        private System.Windows.Forms.Panel pnlCodeValues;
        private System.Windows.Forms.Button btnAdd;
    }
}
