
namespace Walz.Data.UI
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
            this.grpCosts = new System.Windows.Forms.GroupBox();
            this.grpLetters = new System.Windows.Forms.GroupBox();
            this.dgvLetters = new System.Windows.Forms.DataGridView();
            this.grpEnvelopes = new System.Windows.Forms.GroupBox();
            this.dgvEnvelopes = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpModifiers = new System.Windows.Forms.GroupBox();
            this.dgvModifiers = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.grpLetters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLetters)).BeginInit();
            this.grpEnvelopes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnvelopes)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.grpModifiers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvModifiers)).BeginInit();
            this.SuspendLayout();
            // 
            // grpCosts
            // 
            this.grpCosts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCosts.Location = new System.Drawing.Point(403, 3);
            this.grpCosts.Name = "grpCosts";
            this.grpCosts.Size = new System.Drawing.Size(394, 127);
            this.grpCosts.TabIndex = 0;
            this.grpCosts.TabStop = false;
            this.grpCosts.Text = "Costs";
            // 
            // grpLetters
            // 
            this.grpLetters.Controls.Add(this.dgvLetters);
            this.grpLetters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpLetters.Location = new System.Drawing.Point(3, 3);
            this.grpLetters.Name = "grpLetters";
            this.grpLetters.Size = new System.Drawing.Size(394, 127);
            this.grpLetters.TabIndex = 1;
            this.grpLetters.TabStop = false;
            this.grpLetters.Text = "Letters";
            // 
            // dgvLetters
            // 
            this.dgvLetters.AllowUserToDeleteRows = false;
            this.dgvLetters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLetters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLetters.Location = new System.Drawing.Point(3, 16);
            this.dgvLetters.Name = "dgvLetters";
            this.dgvLetters.Size = new System.Drawing.Size(388, 108);
            this.dgvLetters.TabIndex = 0;
            // 
            // grpEnvelopes
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.grpEnvelopes, 2);
            this.grpEnvelopes.Controls.Add(this.dgvEnvelopes);
            this.grpEnvelopes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpEnvelopes.Location = new System.Drawing.Point(3, 136);
            this.grpEnvelopes.Name = "grpEnvelopes";
            this.grpEnvelopes.Size = new System.Drawing.Size(794, 127);
            this.grpEnvelopes.TabIndex = 2;
            this.grpEnvelopes.TabStop = false;
            this.grpEnvelopes.Text = "Envelopes";
            // 
            // dgvEnvelopes
            // 
            this.dgvEnvelopes.AllowUserToDeleteRows = false;
            this.dgvEnvelopes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEnvelopes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEnvelopes.Location = new System.Drawing.Point(3, 16);
            this.dgvEnvelopes.Name = "dgvEnvelopes";
            this.dgvEnvelopes.Size = new System.Drawing.Size(788, 108);
            this.dgvEnvelopes.TabIndex = 0;
            this.dgvEnvelopes.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dgvEnvelopes_RowStateChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.btnCancel, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.grpModifiers, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.grpLetters, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.grpCosts, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.grpEnvelopes, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnSave, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCancel.Location = new System.Drawing.Point(719, 413);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // grpModifiers
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.grpModifiers, 2);
            this.grpModifiers.Controls.Add(this.dgvModifiers);
            this.grpModifiers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpModifiers.Location = new System.Drawing.Point(3, 269);
            this.grpModifiers.Name = "grpModifiers";
            this.grpModifiers.Size = new System.Drawing.Size(794, 127);
            this.grpModifiers.TabIndex = 3;
            this.grpModifiers.TabStop = false;
            this.grpModifiers.Text = "Envelope Cost Modifiers";
            // 
            // dgvModifiers
            // 
            this.dgvModifiers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvModifiers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvModifiers.Location = new System.Drawing.Point(3, 16);
            this.dgvModifiers.Name = "dgvModifiers";
            this.dgvModifiers.Size = new System.Drawing.Size(788, 108);
            this.dgvModifiers.TabIndex = 0;
            this.dgvModifiers.DefaultValuesNeeded += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgvModifiers_DefaultValuesNeeded);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnSave.Location = new System.Drawing.Point(6, 413);
            this.btnSave.Margin = new System.Windows.Forms.Padding(6, 3, 3, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Certified Mail Costs";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.Shown += new System.EventHandler(this.SettingsForm_Shown);
            this.grpLetters.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLetters)).EndInit();
            this.grpEnvelopes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEnvelopes)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.grpModifiers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvModifiers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpCosts;
        private System.Windows.Forms.GroupBox grpLetters;
        private System.Windows.Forms.DataGridView dgvLetters;
        private System.Windows.Forms.GroupBox grpEnvelopes;
        private System.Windows.Forms.DataGridView dgvEnvelopes;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox grpModifiers;
        private System.Windows.Forms.DataGridView dgvModifiers;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}