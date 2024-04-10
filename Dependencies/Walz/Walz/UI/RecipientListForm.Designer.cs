namespace Walz.Data.UI
{
    partial class RecipientListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RecipientListForm));
            this.dgvRecipients = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecipients)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvRecipients
            // 
            this.dgvRecipients.AllowUserToAddRows = false;
            this.dgvRecipients.AllowUserToDeleteRows = false;
            this.dgvRecipients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecipients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRecipients.Location = new System.Drawing.Point(0, 0);
            this.dgvRecipients.Name = "dgvRecipients";
            this.dgvRecipients.Size = new System.Drawing.Size(730, 262);
            this.dgvRecipients.TabIndex = 0;
            this.dgvRecipients.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRecipients_CellValueChanged);
            // 
            // RecipientListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 262);
            this.Controls.Add(this.dgvRecipients);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RecipientListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Recipients";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecipients)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvRecipients;
    }
}