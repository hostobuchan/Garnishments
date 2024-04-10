namespace System.Windows.Forms
{
    partial class FieldMappingInvertedDataGridView<TKey, TValue>
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
            this.dgvFieldMapping = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFieldMapping)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvFieldMapping
            // 
            this.dgvFieldMapping.AllowUserToAddRows = false;
            this.dgvFieldMapping.AllowUserToDeleteRows = false;
            this.dgvFieldMapping.AllowUserToResizeRows = false;
            this.dgvFieldMapping.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvFieldMapping.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFieldMapping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFieldMapping.Location = new System.Drawing.Point(0, 0);
            this.dgvFieldMapping.Name = "dgvFieldMapping";
            this.dgvFieldMapping.Size = new System.Drawing.Size(766, 150);
            this.dgvFieldMapping.TabIndex = 0;
            this.dgvFieldMapping.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvFieldMapping_CellValidating);
            this.dgvFieldMapping.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFieldMapping_CellValueChanged);
            this.dgvFieldMapping.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvFieldMapping_DataError);
            // 
            // FieldMappingInvertedDataGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvFieldMapping);
            this.Name = "FieldMappingInvertedDataGridView";
            this.Size = new System.Drawing.Size(766, 150);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFieldMapping)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvFieldMapping;
    }
}
