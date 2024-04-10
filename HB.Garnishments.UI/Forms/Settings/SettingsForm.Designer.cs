namespace HB.Garnishments.UI.Forms.Settings
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.btnRestrictions = new System.Windows.Forms.Button();
            this.btnResults = new System.Windows.Forms.Button();
            this.btnFees = new System.Windows.Forms.Button();
            this.btnOverride = new System.Windows.Forms.Button();
            this.btnLetterhead = new System.Windows.Forms.Button();
            this.btnWalz = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRestrictions
            // 
            this.btnRestrictions.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnRestrictions.Location = new System.Drawing.Point(51, 34);
            this.btnRestrictions.Name = "btnRestrictions";
            this.btnRestrictions.Size = new System.Drawing.Size(182, 58);
            this.btnRestrictions.TabIndex = 0;
            this.btnRestrictions.Text = "Restrictions";
            this.btnRestrictions.UseVisualStyleBackColor = true;
            this.btnRestrictions.Click += new System.EventHandler(this.btnRestrictions_Click);
            // 
            // btnResults
            // 
            this.btnResults.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnResults.Location = new System.Drawing.Point(51, 98);
            this.btnResults.Name = "btnResults";
            this.btnResults.Size = new System.Drawing.Size(182, 58);
            this.btnResults.TabIndex = 1;
            this.btnResults.Text = "Results / Codes";
            this.btnResults.UseVisualStyleBackColor = true;
            this.btnResults.Click += new System.EventHandler(this.btnResults_Click);
            // 
            // btnFees
            // 
            this.btnFees.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnFees.Location = new System.Drawing.Point(51, 162);
            this.btnFees.Name = "btnFees";
            this.btnFees.Size = new System.Drawing.Size(182, 58);
            this.btnFees.TabIndex = 2;
            this.btnFees.Text = "Court / Sheriff Fees";
            this.btnFees.UseVisualStyleBackColor = true;
            this.btnFees.Click += new System.EventHandler(this.btnFees_Click);
            // 
            // btnOverride
            // 
            this.btnOverride.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnOverride.Location = new System.Drawing.Point(51, 354);
            this.btnOverride.Name = "btnOverride";
            this.btnOverride.Size = new System.Drawing.Size(182, 58);
            this.btnOverride.TabIndex = 4;
            this.btnOverride.Text = "Override Garn Status";
            this.btnOverride.UseVisualStyleBackColor = true;
            this.btnOverride.Click += new System.EventHandler(this.btnOverride_Click);
            // 
            // btnLetterhead
            // 
            this.btnLetterhead.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnLetterhead.Location = new System.Drawing.Point(51, 226);
            this.btnLetterhead.Name = "btnLetterhead";
            this.btnLetterhead.Size = new System.Drawing.Size(182, 58);
            this.btnLetterhead.TabIndex = 3;
            this.btnLetterhead.Text = "Update Firm Letterhead";
            this.btnLetterhead.UseVisualStyleBackColor = true;
            this.btnLetterhead.Click += new System.EventHandler(this.btnLetterhead_Click);
            // 
            // btnWalz
            // 
            this.btnWalz.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnWalz.Location = new System.Drawing.Point(51, 290);
            this.btnWalz.Name = "btnWalz";
            this.btnWalz.Size = new System.Drawing.Size(182, 58);
            this.btnWalz.TabIndex = 5;
            this.btnWalz.Text = "Walz Settings";
            this.btnWalz.UseVisualStyleBackColor = true;
            this.btnWalz.Click += new System.EventHandler(this.btnWalz_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 468);
            this.Controls.Add(this.btnWalz);
            this.Controls.Add(this.btnLetterhead);
            this.Controls.Add(this.btnOverride);
            this.Controls.Add(this.btnFees);
            this.Controls.Add(this.btnResults);
            this.Controls.Add(this.btnRestrictions);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SettingsForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRestrictions;
        private System.Windows.Forms.Button btnResults;
        private System.Windows.Forms.Button btnFees;
        private System.Windows.Forms.Button btnOverride;
        private System.Windows.Forms.Button btnLetterhead;
        private System.Windows.Forms.Button btnWalz;
    }
}