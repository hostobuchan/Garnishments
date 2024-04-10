namespace HB.Garnishments.UI.Forms.Settings.Results
{
    partial class ResultsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResultsForm));
            this.label1 = new System.Windows.Forms.Label();
            this.cboResult = new System.Windows.Forms.ComboBox();
            this.grpResultSelection = new System.Windows.Forms.GroupBox();
            this.chkUpdateAsset = new System.Windows.Forms.CheckBox();
            this.grpDisposition = new System.Windows.Forms.GroupBox();
            this.rdoBad = new System.Windows.Forms.RadioButton();
            this.rdoGood = new System.Windows.Forms.RadioButton();
            this.btnAdd = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grpCodeGroups = new System.Windows.Forms.GroupBox();
            this.cboDebtor = new System.Windows.Forms.ComboBox();
            this.cboState = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.chkMoneyExpected = new System.Windows.Forms.CheckBox();
            this.grpResultSelection.SuspendLayout();
            this.grpDisposition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grpCodeGroups.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Result";
            // 
            // cboResult
            // 
            this.cboResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboResult.FormatString = "N";
            this.cboResult.FormattingEnabled = true;
            this.cboResult.Location = new System.Drawing.Point(49, 13);
            this.cboResult.Name = "cboResult";
            this.cboResult.Size = new System.Drawing.Size(716, 21);
            this.cboResult.TabIndex = 1;
            this.cboResult.SelectedIndexChanged += new System.EventHandler(this.cboResult_SelectedIndexChanged);
            // 
            // grpResultSelection
            // 
            this.grpResultSelection.Controls.Add(this.chkMoneyExpected);
            this.grpResultSelection.Controls.Add(this.chkUpdateAsset);
            this.grpResultSelection.Controls.Add(this.grpDisposition);
            this.grpResultSelection.Controls.Add(this.btnAdd);
            this.grpResultSelection.Controls.Add(this.label1);
            this.grpResultSelection.Controls.Add(this.cboResult);
            this.grpResultSelection.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpResultSelection.Location = new System.Drawing.Point(0, 0);
            this.grpResultSelection.Name = "grpResultSelection";
            this.grpResultSelection.Size = new System.Drawing.Size(800, 100);
            this.grpResultSelection.TabIndex = 0;
            this.grpResultSelection.TabStop = false;
            // 
            // chkUpdateAsset
            // 
            this.chkUpdateAsset.AutoSize = true;
            this.chkUpdateAsset.Location = new System.Drawing.Point(261, 62);
            this.chkUpdateAsset.Name = "chkUpdateAsset";
            this.chkUpdateAsset.Size = new System.Drawing.Size(144, 17);
            this.chkUpdateAsset.TabIndex = 4;
            this.chkUpdateAsset.Text = "Update Asset Disposition";
            this.chkUpdateAsset.UseVisualStyleBackColor = true;
            this.chkUpdateAsset.CheckedChanged += new System.EventHandler(this.chkUpdateAsset_CheckedChanged);
            // 
            // grpDisposition
            // 
            this.grpDisposition.Controls.Add(this.rdoBad);
            this.grpDisposition.Controls.Add(this.rdoGood);
            this.grpDisposition.Location = new System.Drawing.Point(9, 40);
            this.grpDisposition.Name = "grpDisposition";
            this.grpDisposition.Size = new System.Drawing.Size(219, 54);
            this.grpDisposition.TabIndex = 3;
            this.grpDisposition.TabStop = false;
            // 
            // rdoBad
            // 
            this.rdoBad.AutoSize = true;
            this.rdoBad.Location = new System.Drawing.Point(139, 21);
            this.rdoBad.Name = "rdoBad";
            this.rdoBad.Size = new System.Drawing.Size(44, 17);
            this.rdoBad.TabIndex = 1;
            this.rdoBad.TabStop = true;
            this.rdoBad.Text = "Bad";
            this.rdoBad.UseVisualStyleBackColor = true;
            // 
            // rdoGood
            // 
            this.rdoGood.AutoSize = true;
            this.rdoGood.Location = new System.Drawing.Point(26, 21);
            this.rdoGood.Name = "rdoGood";
            this.rdoGood.Size = new System.Drawing.Size(51, 17);
            this.rdoGood.TabIndex = 0;
            this.rdoGood.TabStop = true;
            this.rdoGood.Text = "Good";
            this.rdoGood.UseVisualStyleBackColor = true;
            this.rdoGood.CheckedChanged += new System.EventHandler(this.rdoGood_CheckedChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Image = global::HB.Garnishments.Properties.Resources.add;
            this.btnAdd.Location = new System.Drawing.Point(771, 11);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(23, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 100);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grpCodeGroups);
            this.splitContainer1.Size = new System.Drawing.Size(800, 350);
            this.splitContainer1.SplitterDistance = 166;
            this.splitContainer1.TabIndex = 1;
            // 
            // grpCodeGroups
            // 
            this.grpCodeGroups.AutoSize = true;
            this.grpCodeGroups.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.grpCodeGroups.Controls.Add(this.cboDebtor);
            this.grpCodeGroups.Controls.Add(this.cboState);
            this.grpCodeGroups.Controls.Add(this.label2);
            this.grpCodeGroups.Controls.Add(this.lblState);
            this.grpCodeGroups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCodeGroups.Location = new System.Drawing.Point(0, 0);
            this.grpCodeGroups.Name = "grpCodeGroups";
            this.grpCodeGroups.Size = new System.Drawing.Size(166, 350);
            this.grpCodeGroups.TabIndex = 0;
            this.grpCodeGroups.TabStop = false;
            // 
            // cboDebtor
            // 
            this.cboDebtor.FormattingEnabled = true;
            this.cboDebtor.Location = new System.Drawing.Point(23, 188);
            this.cboDebtor.Name = "cboDebtor";
            this.cboDebtor.Size = new System.Drawing.Size(121, 21);
            this.cboDebtor.TabIndex = 3;
            this.cboDebtor.SelectedIndexChanged += new System.EventHandler(this.CodeSelectionBoxes_SelectedIndexChanged);
            // 
            // cboState
            // 
            this.cboState.FormattingEnabled = true;
            this.cboState.Location = new System.Drawing.Point(23, 54);
            this.cboState.Name = "cboState";
            this.cboState.Size = new System.Drawing.Size(121, 21);
            this.cboState.TabIndex = 2;
            this.cboState.SelectedIndexChanged += new System.EventHandler(this.CodeSelectionBoxes_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Debtor";
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblState.Location = new System.Drawing.Point(20, 38);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(37, 13);
            this.lblState.TabIndex = 0;
            this.lblState.Text = "State";
            // 
            // chkMoneyExpected
            // 
            this.chkMoneyExpected.AutoSize = true;
            this.chkMoneyExpected.Location = new System.Drawing.Point(446, 62);
            this.chkMoneyExpected.Name = "chkMoneyExpected";
            this.chkMoneyExpected.Size = new System.Drawing.Size(106, 17);
            this.chkMoneyExpected.TabIndex = 5;
            this.chkMoneyExpected.Text = "Money Expected";
            this.chkMoneyExpected.UseVisualStyleBackColor = true;
            this.chkMoneyExpected.CheckedChanged += new System.EventHandler(this.chkMoneyExpected_CheckedChanged);
            // 
            // ResultsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.grpResultSelection);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ResultsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ResultsForm";
            this.Load += new System.EventHandler(this.ResultsForm_Load);
            this.grpResultSelection.ResumeLayout(false);
            this.grpResultSelection.PerformLayout();
            this.grpDisposition.ResumeLayout(false);
            this.grpDisposition.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.grpCodeGroups.ResumeLayout(false);
            this.grpCodeGroups.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboResult;
        private System.Windows.Forms.GroupBox grpResultSelection;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.CheckBox chkUpdateAsset;
        private System.Windows.Forms.GroupBox grpDisposition;
        private System.Windows.Forms.RadioButton rdoBad;
        private System.Windows.Forms.RadioButton rdoGood;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox grpCodeGroups;
        private System.Windows.Forms.ComboBox cboDebtor;
        private System.Windows.Forms.ComboBox cboState;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.CheckBox chkMoneyExpected;
    }
}