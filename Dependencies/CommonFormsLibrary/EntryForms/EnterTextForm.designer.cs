﻿namespace System.Windows.Forms
{
    partial class EnterTextForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnterTextForm));
            this.ENTRY = new System.Windows.Forms.TextBox();
            this.OK = new System.Windows.Forms.Button();
            this.CANCEL = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ENTRY
            // 
            this.ENTRY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ENTRY.Location = new System.Drawing.Point(12, 12);
            this.ENTRY.MaxLength = 256;
            this.ENTRY.Multiline = true;
            this.ENTRY.Name = "ENTRY";
            this.ENTRY.Size = new System.Drawing.Size(287, 133);
            this.ENTRY.TabIndex = 0;
            this.ENTRY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ENTRY_KeyPress);
            // 
            // OK
            // 
            this.OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OK.Location = new System.Drawing.Point(12, 151);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 23);
            this.OK.TabIndex = 1;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // CANCEL
            // 
            this.CANCEL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CANCEL.Location = new System.Drawing.Point(224, 151);
            this.CANCEL.Name = "CANCEL";
            this.CANCEL.Size = new System.Drawing.Size(75, 23);
            this.CANCEL.TabIndex = 2;
            this.CANCEL.Text = "Cancel";
            this.CANCEL.UseVisualStyleBackColor = true;
            this.CANCEL.Click += new System.EventHandler(this.CANCEL_Click);
            // 
            // EnterTextForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CANCEL;
            this.ClientSize = new System.Drawing.Size(311, 186);
            this.Controls.Add(this.CANCEL);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.ENTRY);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EnterTextForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ENTRY;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button CANCEL;
    }
}