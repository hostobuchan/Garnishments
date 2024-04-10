namespace HB.Garnishments.UI.Forms.Processing
{
    partial class ProcessingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessingForm));
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lstPending = new System.Windows.Forms.ListBox();
            this.lblGood = new System.Windows.Forms.Label();
            this.lstGood = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exportWorklistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rejectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAdd_Good = new System.Windows.Forms.Button();
            this.btnRemove_Good = new System.Windows.Forms.Button();
            this.btnRemove_Bad = new System.Windows.Forms.Button();
            this.btnAdd_Bad = new System.Windows.Forms.Button();
            this.lstBad = new System.Windows.Forms.ListBox();
            this.lblBad = new System.Windows.Forms.Label();
            this.btnProcess = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblCommunicationStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(13, 32);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(40, 13);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "FileNo:";
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(59, 29);
            this.txtSearch.MaxLength = 8;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(87, 20);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(152, 27);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lstPending
            // 
            this.lstPending.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstPending.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstPending.FormatString = "F";
            this.lstPending.FormattingEnabled = true;
            this.lstPending.IntegralHeight = false;
            this.lstPending.Location = new System.Drawing.Point(12, 56);
            this.lstPending.Name = "lstPending";
            this.lstPending.Size = new System.Drawing.Size(215, 459);
            this.lstPending.TabIndex = 3;
            this.lstPending.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstPending_DrawItem);
            this.lstPending.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.lstPending_MeasureItem);
            this.lstPending.SelectedIndexChanged += new System.EventHandler(this.lstPending_SelectedIndexChanged);
            // 
            // lblGood
            // 
            this.lblGood.AutoSize = true;
            this.lblGood.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGood.Location = new System.Drawing.Point(341, 40);
            this.lblGood.Name = "lblGood";
            this.lblGood.Size = new System.Drawing.Size(88, 13);
            this.lblGood.TabIndex = 4;
            this.lblGood.Text = "Good Answers";
            // 
            // lstGood
            // 
            this.lstGood.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstGood.FormattingEnabled = true;
            this.lstGood.IntegralHeight = false;
            this.lstGood.Location = new System.Drawing.Point(260, 56);
            this.lstGood.Name = "lstGood";
            this.lstGood.Size = new System.Drawing.Size(251, 212);
            this.lstGood.TabIndex = 5;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(801, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reloadDataToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exportWorklistToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // reloadDataToolStripMenuItem
            // 
            this.reloadDataToolStripMenuItem.Name = "reloadDataToolStripMenuItem";
            this.reloadDataToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.reloadDataToolStripMenuItem.Text = "Reload Data";
            this.reloadDataToolStripMenuItem.Click += new System.EventHandler(this.reloadDataToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(150, 6);
            // 
            // exportWorklistToolStripMenuItem
            // 
            this.exportWorklistToolStripMenuItem.Name = "exportWorklistToolStripMenuItem";
            this.exportWorklistToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.exportWorklistToolStripMenuItem.Text = "Export Worklist";
            this.exportWorklistToolStripMenuItem.Click += new System.EventHandler(this.exportWorklistToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rejectsToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // rejectsToolStripMenuItem
            // 
            this.rejectsToolStripMenuItem.Name = "rejectsToolStripMenuItem";
            this.rejectsToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.rejectsToolStripMenuItem.Text = "Rejects";
            this.rejectsToolStripMenuItem.Click += new System.EventHandler(this.rejectsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // btnAdd_Good
            // 
            this.btnAdd_Good.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnAdd_Good.Location = new System.Drawing.Point(233, 56);
            this.btnAdd_Good.Name = "btnAdd_Good";
            this.btnAdd_Good.Size = new System.Drawing.Size(21, 97);
            this.btnAdd_Good.TabIndex = 7;
            this.btnAdd_Good.Text = ">";
            this.btnAdd_Good.UseVisualStyleBackColor = true;
            this.btnAdd_Good.Click += new System.EventHandler(this.btnAdd_Good_Click);
            // 
            // btnRemove_Good
            // 
            this.btnRemove_Good.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnRemove_Good.Location = new System.Drawing.Point(233, 171);
            this.btnRemove_Good.Name = "btnRemove_Good";
            this.btnRemove_Good.Size = new System.Drawing.Size(21, 97);
            this.btnRemove_Good.TabIndex = 8;
            this.btnRemove_Good.Text = "<";
            this.btnRemove_Good.UseVisualStyleBackColor = true;
            this.btnRemove_Good.Click += new System.EventHandler(this.btnRemove_Good_Click);
            // 
            // btnRemove_Bad
            // 
            this.btnRemove_Bad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemove_Bad.Location = new System.Drawing.Point(233, 418);
            this.btnRemove_Bad.Name = "btnRemove_Bad";
            this.btnRemove_Bad.Size = new System.Drawing.Size(21, 97);
            this.btnRemove_Bad.TabIndex = 12;
            this.btnRemove_Bad.Text = "<";
            this.btnRemove_Bad.UseVisualStyleBackColor = true;
            this.btnRemove_Bad.Click += new System.EventHandler(this.btnRemove_Bad_Click);
            // 
            // btnAdd_Bad
            // 
            this.btnAdd_Bad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd_Bad.Location = new System.Drawing.Point(233, 303);
            this.btnAdd_Bad.Name = "btnAdd_Bad";
            this.btnAdd_Bad.Size = new System.Drawing.Size(21, 97);
            this.btnAdd_Bad.TabIndex = 11;
            this.btnAdd_Bad.Text = ">";
            this.btnAdd_Bad.UseVisualStyleBackColor = true;
            this.btnAdd_Bad.Click += new System.EventHandler(this.btnAdd_Bad_Click);
            // 
            // lstBad
            // 
            this.lstBad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lstBad.FormattingEnabled = true;
            this.lstBad.IntegralHeight = false;
            this.lstBad.Location = new System.Drawing.Point(260, 303);
            this.lstBad.Name = "lstBad";
            this.lstBad.Size = new System.Drawing.Size(251, 212);
            this.lstBad.TabIndex = 10;
            // 
            // lblBad
            // 
            this.lblBad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblBad.AutoSize = true;
            this.lblBad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBad.Location = new System.Drawing.Point(341, 287);
            this.lblBad.Name = "lblBad";
            this.lblBad.Size = new System.Drawing.Size(80, 13);
            this.lblBad.TabIndex = 9;
            this.lblBad.Text = "Bad Answers";
            // 
            // btnProcess
            // 
            this.btnProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProcess.Location = new System.Drawing.Point(585, 80);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(156, 48);
            this.btnProcess.TabIndex = 13;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(714, 492);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblCommunicationStatus,
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 518);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(801, 22);
            this.statusStrip1.TabIndex = 15;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblCommunicationStatus
            // 
            this.lblCommunicationStatus.Image = global::HB.Garnishments.UI.Properties.Resources.information;
            this.lblCommunicationStatus.Name = "lblCommunicationStatus";
            this.lblCommunicationStatus.Size = new System.Drawing.Size(16, 17);
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(52, 17);
            this.lblStatus.Text = "lblStatus";
            // 
            // ProcessingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 540);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnProcess);
            this.Controls.Add(this.btnRemove_Bad);
            this.Controls.Add(this.btnAdd_Bad);
            this.Controls.Add(this.lstBad);
            this.Controls.Add(this.lblBad);
            this.Controls.Add(this.btnRemove_Good);
            this.Controls.Add(this.btnAdd_Good);
            this.Controls.Add(this.lstGood);
            this.Controls.Add(this.lblGood);
            this.Controls.Add(this.lstPending);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ProcessingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ProcessingForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProcessingForm_FormClosing);
            this.Load += new System.EventHandler(this.ProcessingForm_Load);
            this.Shown += new System.EventHandler(this.ProcessingForm_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ListBox lstPending;
        private System.Windows.Forms.Label lblGood;
        private System.Windows.Forms.ListBox lstGood;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exportWorklistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Button btnAdd_Good;
        private System.Windows.Forms.Button btnRemove_Good;
        private System.Windows.Forms.Button btnRemove_Bad;
        private System.Windows.Forms.Button btnAdd_Bad;
        private System.Windows.Forms.ListBox lstBad;
        private System.Windows.Forms.Label lblBad;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rejectsToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel lblCommunicationStatus;
    }
}