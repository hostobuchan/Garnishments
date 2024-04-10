namespace HB.Garnishments.UI.Controls.Settings.Fees
{
    partial class FeeControl
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
            this.chkSheriff = new System.Windows.Forms.CheckBox();
            this.txtServiceFeeAmount = new System.Windows.Forms.TextBox();
            this.txtCourtFeeAmount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chkCombineFees = new System.Windows.Forms.CheckBox();
            this.chkServiceFee = new System.Windows.Forms.CheckBox();
            this.chkCourtFee = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chkSheriff
            // 
            this.chkSheriff.AutoSize = true;
            this.chkSheriff.Location = new System.Drawing.Point(224, 3);
            this.chkSheriff.Name = "chkSheriff";
            this.chkSheriff.Size = new System.Drawing.Size(108, 17);
            this.chkSheriff.TabIndex = 77;
            this.chkSheriff.Text = "Served By Sheriff";
            this.chkSheriff.UseVisualStyleBackColor = true;
            // 
            // txtServiceFeeAmount
            // 
            this.txtServiceFeeAmount.Location = new System.Drawing.Point(273, 24);
            this.txtServiceFeeAmount.Name = "txtServiceFeeAmount";
            this.txtServiceFeeAmount.Size = new System.Drawing.Size(100, 20);
            this.txtServiceFeeAmount.TabIndex = 76;
            this.txtServiceFeeAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // txtCourtFeeAmount
            // 
            this.txtCourtFeeAmount.Location = new System.Drawing.Point(71, 24);
            this.txtCourtFeeAmount.Name = "txtCourtFeeAmount";
            this.txtCourtFeeAmount.Size = new System.Drawing.Size(100, 20);
            this.txtCourtFeeAmount.TabIndex = 75;
            this.txtCourtFeeAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(194, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 74;
            this.label5.Text = "Service Fee $";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 73;
            this.label4.Text = "Court Fee $";
            // 
            // chkCombineFees
            // 
            this.chkCombineFees.AutoSize = true;
            this.chkCombineFees.Location = new System.Drawing.Point(338, 3);
            this.chkCombineFees.Name = "chkCombineFees";
            this.chkCombineFees.Size = new System.Drawing.Size(127, 17);
            this.chkCombineFees.TabIndex = 72;
            this.chkCombineFees.Text = "Combine Fee Checks";
            this.chkCombineFees.UseVisualStyleBackColor = true;
            // 
            // chkServiceFee
            // 
            this.chkServiceFee.AutoSize = true;
            this.chkServiceFee.Location = new System.Drawing.Point(108, 3);
            this.chkServiceFee.Name = "chkServiceFee";
            this.chkServiceFee.Size = new System.Drawing.Size(110, 17);
            this.chkServiceFee.TabIndex = 71;
            this.chkServiceFee.Text = "Has Service Fees";
            this.chkServiceFee.UseVisualStyleBackColor = true;
            // 
            // chkCourtFee
            // 
            this.chkCourtFee.AutoSize = true;
            this.chkCourtFee.Location = new System.Drawing.Point(3, 3);
            this.chkCourtFee.Name = "chkCourtFee";
            this.chkCourtFee.Size = new System.Drawing.Size(99, 17);
            this.chkCourtFee.TabIndex = 70;
            this.chkCourtFee.Text = "Has Court Fees";
            this.chkCourtFee.UseVisualStyleBackColor = true;
            // 
            // FeeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkSheriff);
            this.Controls.Add(this.txtServiceFeeAmount);
            this.Controls.Add(this.txtCourtFeeAmount);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkCombineFees);
            this.Controls.Add(this.chkServiceFee);
            this.Controls.Add(this.chkCourtFee);
            this.Name = "FeeControl";
            this.Size = new System.Drawing.Size(464, 50);
            this.Load += new System.EventHandler(this.FeeControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkSheriff;
        private System.Windows.Forms.TextBox txtServiceFeeAmount;
        private System.Windows.Forms.TextBox txtCourtFeeAmount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkCombineFees;
        private System.Windows.Forms.CheckBox chkServiceFee;
        private System.Windows.Forms.CheckBox chkCourtFee;
    }
}
