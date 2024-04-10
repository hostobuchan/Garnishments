namespace HB.Garnishments.UI.Forms.Processing
{
    partial class RegisteredAgentSelectionForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabSelection = new System.Windows.Forms.TabPage();
            this.btnDeny = new System.Windows.Forms.Button();
            this.btnSkip = new System.Windows.Forms.Button();
            this.btnUseAgent = new System.Windows.Forms.Button();
            this.tabSearch = new System.Windows.Forms.TabPage();
            this.btnExpandSearch = new System.Windows.Forms.Button();
            this.txtUseNumber = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnUseNumber = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.pnlSelectedAgent = new System.Windows.Forms.Panel();
            this.lstAgents = new System.Windows.Forms.ListBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.txtDebtor = new System.Windows.Forms.Label();
            this.pnlRequestedAsset = new System.Windows.Forms.Panel();
            this.lblRequestedAsset = new System.Windows.Forms.Label();
            this.txtRequestedBy = new System.Windows.Forms.Label();
            this.lblMatches = new System.Windows.Forms.Label();
            this.lblRequested = new System.Windows.Forms.Label();
            this.grpPreviousAssetInfo = new System.Windows.Forms.GroupBox();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.pnlAccountHistory = new System.Windows.Forms.Panel();
            this.grpAllAccounts = new System.Windows.Forms.GroupBox();
            this.pnlInfoHistory = new System.Windows.Forms.Panel();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.lblCurrentSelection = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabSelection.SuspendLayout();
            this.tabSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.grpPreviousAssetInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.grpAllAccounts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabSelection);
            this.tabControl1.Controls.Add(this.tabSearch);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(278, 325);
            this.tabControl1.TabIndex = 0;
            // 
            // tabSelection
            // 
            this.tabSelection.BackColor = System.Drawing.SystemColors.Control;
            this.tabSelection.Controls.Add(this.btnDeny);
            this.tabSelection.Controls.Add(this.btnSkip);
            this.tabSelection.Controls.Add(this.btnUseAgent);
            this.tabSelection.Location = new System.Drawing.Point(4, 22);
            this.tabSelection.Name = "tabSelection";
            this.tabSelection.Padding = new System.Windows.Forms.Padding(3);
            this.tabSelection.Size = new System.Drawing.Size(270, 299);
            this.tabSelection.TabIndex = 0;
            this.tabSelection.Text = "Selection";
            // 
            // btnDeny
            // 
            this.btnDeny.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnDeny.Location = new System.Drawing.Point(41, 240);
            this.btnDeny.Name = "btnDeny";
            this.btnDeny.Size = new System.Drawing.Size(188, 23);
            this.btnDeny.TabIndex = 2;
            this.btnDeny.Text = "Deny Garnishment";
            this.btnDeny.UseVisualStyleBackColor = true;
            this.btnDeny.Click += new System.EventHandler(this.btnDeny_Click);
            // 
            // btnSkip
            // 
            this.btnSkip.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSkip.Location = new System.Drawing.Point(41, 131);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(188, 23);
            this.btnSkip.TabIndex = 1;
            this.btnSkip.Text = "Skip Account";
            this.btnSkip.UseVisualStyleBackColor = true;
            this.btnSkip.Click += new System.EventHandler(this.btnSkip_Click);
            // 
            // btnUseAgent
            // 
            this.btnUseAgent.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnUseAgent.Location = new System.Drawing.Point(41, 22);
            this.btnUseAgent.Name = "btnUseAgent";
            this.btnUseAgent.Size = new System.Drawing.Size(188, 23);
            this.btnUseAgent.TabIndex = 0;
            this.btnUseAgent.Text = "Use Selected Agent";
            this.btnUseAgent.UseVisualStyleBackColor = true;
            this.btnUseAgent.Click += new System.EventHandler(this.btnUseAgent_Click);
            // 
            // tabSearch
            // 
            this.tabSearch.BackColor = System.Drawing.SystemColors.Control;
            this.tabSearch.Controls.Add(this.btnExpandSearch);
            this.tabSearch.Controls.Add(this.txtUseNumber);
            this.tabSearch.Controls.Add(this.btnSearch);
            this.tabSearch.Controls.Add(this.label5);
            this.tabSearch.Controls.Add(this.btnUseNumber);
            this.tabSearch.Controls.Add(this.label6);
            this.tabSearch.Controls.Add(this.txtSearch);
            this.tabSearch.Location = new System.Drawing.Point(4, 22);
            this.tabSearch.Name = "tabSearch";
            this.tabSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tabSearch.Size = new System.Drawing.Size(270, 299);
            this.tabSearch.TabIndex = 1;
            this.tabSearch.Text = "Alternate Search";
            // 
            // btnExpandSearch
            // 
            this.btnExpandSearch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnExpandSearch.Location = new System.Drawing.Point(69, 179);
            this.btnExpandSearch.Name = "btnExpandSearch";
            this.btnExpandSearch.Size = new System.Drawing.Size(136, 47);
            this.btnExpandSearch.TabIndex = 15;
            this.btnExpandSearch.Text = "Expand Search To Include Other States";
            this.btnExpandSearch.UseVisualStyleBackColor = true;
            this.btnExpandSearch.Click += new System.EventHandler(this.btnExpandSearch_Click);
            // 
            // txtUseNumber
            // 
            this.txtUseNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUseNumber.Location = new System.Drawing.Point(19, 36);
            this.txtUseNumber.Name = "txtUseNumber";
            this.txtUseNumber.Size = new System.Drawing.Size(206, 20);
            this.txtUseNumber.TabIndex = 11;
            this.txtUseNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUseNumber_KeyPress);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(231, 73);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(31, 23);
            this.btnSearch.TabIndex = 14;
            this.btnSearch.Text = "OK";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(16, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Use This Number";
            // 
            // btnUseNumber
            // 
            this.btnUseNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUseNumber.Location = new System.Drawing.Point(231, 34);
            this.btnUseNumber.Name = "btnUseNumber";
            this.btnUseNumber.Size = new System.Drawing.Size(31, 23);
            this.btnUseNumber.TabIndex = 12;
            this.btnUseNumber.Text = "OK";
            this.btnUseNumber.UseVisualStyleBackColor = true;
            this.btnUseNumber.Click += new System.EventHandler(this.btnUseNumber_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(16, 59);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "New Search";
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(19, 75);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(206, 20);
            this.txtSearch.TabIndex = 13;
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearch_KeyPress);
            // 
            // pnlSelectedAgent
            // 
            this.pnlSelectedAgent.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSelectedAgent.Location = new System.Drawing.Point(0, 25);
            this.pnlSelectedAgent.Name = "pnlSelectedAgent";
            this.pnlSelectedAgent.Size = new System.Drawing.Size(278, 245);
            this.pnlSelectedAgent.TabIndex = 0;
            // 
            // lstAgents
            // 
            this.lstAgents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstAgents.FormattingEnabled = true;
            this.lstAgents.IntegralHeight = false;
            this.lstAgents.Location = new System.Drawing.Point(335, 53);
            this.lstAgents.Name = "lstAgents";
            this.lstAgents.Size = new System.Drawing.Size(293, 300);
            this.lstAgents.TabIndex = 1;
            this.lstAgents.SelectedIndexChanged += new System.EventHandler(this.lstAgents_SelectedIndexChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Size = new System.Drawing.Size(913, 599);
            this.splitContainer1.SplitterDistance = 631;
            this.splitContainer1.TabIndex = 2;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.txtDebtor);
            this.splitContainer2.Panel1.Controls.Add(this.pnlRequestedAsset);
            this.splitContainer2.Panel1.Controls.Add(this.lblRequestedAsset);
            this.splitContainer2.Panel1.Controls.Add(this.txtRequestedBy);
            this.splitContainer2.Panel1.Controls.Add(this.lblMatches);
            this.splitContainer2.Panel1.Controls.Add(this.lblRequested);
            this.splitContainer2.Panel1.Controls.Add(this.lstAgents);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.grpPreviousAssetInfo);
            this.splitContainer2.Size = new System.Drawing.Size(631, 599);
            this.splitContainer2.SplitterDistance = 356;
            this.splitContainer2.TabIndex = 0;
            // 
            // txtDebtor
            // 
            this.txtDebtor.AutoSize = true;
            this.txtDebtor.Location = new System.Drawing.Point(144, 56);
            this.txtDebtor.Name = "txtDebtor";
            this.txtDebtor.Size = new System.Drawing.Size(50, 13);
            this.txtDebtor.TabIndex = 7;
            this.txtDebtor.Text = "txtDebtor";
            // 
            // pnlRequestedAsset
            // 
            this.pnlRequestedAsset.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlRequestedAsset.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlRequestedAsset.Location = new System.Drawing.Point(12, 82);
            this.pnlRequestedAsset.Name = "pnlRequestedAsset";
            this.pnlRequestedAsset.Size = new System.Drawing.Size(317, 271);
            this.pnlRequestedAsset.TabIndex = 6;
            // 
            // lblRequestedAsset
            // 
            this.lblRequestedAsset.AutoSize = true;
            this.lblRequestedAsset.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRequestedAsset.Location = new System.Drawing.Point(119, 37);
            this.lblRequestedAsset.Name = "lblRequestedAsset";
            this.lblRequestedAsset.Size = new System.Drawing.Size(103, 13);
            this.lblRequestedAsset.TabIndex = 5;
            this.lblRequestedAsset.Text = "Requested Asset";
            // 
            // txtRequestedBy
            // 
            this.txtRequestedBy.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtRequestedBy.AutoSize = true;
            this.txtRequestedBy.Location = new System.Drawing.Point(433, 13);
            this.txtRequestedBy.Name = "txtRequestedBy";
            this.txtRequestedBy.Size = new System.Drawing.Size(82, 13);
            this.txtRequestedBy.TabIndex = 4;
            this.txtRequestedBy.Text = "txtRequestedBy";
            // 
            // lblMatches
            // 
            this.lblMatches.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblMatches.AutoSize = true;
            this.lblMatches.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatches.Location = new System.Drawing.Point(428, 37);
            this.lblMatches.Name = "lblMatches";
            this.lblMatches.Size = new System.Drawing.Size(106, 13);
            this.lblMatches.TabIndex = 3;
            this.lblMatches.Text = "Possible Matches";
            // 
            // lblRequested
            // 
            this.lblRequested.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblRequested.AutoSize = true;
            this.lblRequested.Location = new System.Drawing.Point(350, 13);
            this.lblRequested.Name = "lblRequested";
            this.lblRequested.Size = new System.Drawing.Size(77, 13);
            this.lblRequested.TabIndex = 2;
            this.lblRequested.Text = "Requested By:";
            // 
            // grpPreviousAssetInfo
            // 
            this.grpPreviousAssetInfo.Controls.Add(this.splitContainer4);
            this.grpPreviousAssetInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpPreviousAssetInfo.Location = new System.Drawing.Point(0, 0);
            this.grpPreviousAssetInfo.Name = "grpPreviousAssetInfo";
            this.grpPreviousAssetInfo.Size = new System.Drawing.Size(631, 239);
            this.grpPreviousAssetInfo.TabIndex = 0;
            this.grpPreviousAssetInfo.TabStop = false;
            this.grpPreviousAssetInfo.Text = "Previous Asset Info";
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(3, 16);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.pnlAccountHistory);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.grpAllAccounts);
            this.splitContainer4.Size = new System.Drawing.Size(625, 220);
            this.splitContainer4.SplitterDistance = 311;
            this.splitContainer4.TabIndex = 0;
            // 
            // pnlAccountHistory
            // 
            this.pnlAccountHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAccountHistory.Location = new System.Drawing.Point(0, 0);
            this.pnlAccountHistory.Name = "pnlAccountHistory";
            this.pnlAccountHistory.Size = new System.Drawing.Size(311, 220);
            this.pnlAccountHistory.TabIndex = 0;
            // 
            // grpAllAccounts
            // 
            this.grpAllAccounts.Controls.Add(this.pnlInfoHistory);
            this.grpAllAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpAllAccounts.Location = new System.Drawing.Point(0, 0);
            this.grpAllAccounts.Name = "grpAllAccounts";
            this.grpAllAccounts.Size = new System.Drawing.Size(310, 220);
            this.grpAllAccounts.TabIndex = 1;
            this.grpAllAccounts.TabStop = false;
            this.grpAllAccounts.Text = "Across All Accounts";
            // 
            // pnlInfoHistory
            // 
            this.pnlInfoHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlInfoHistory.Location = new System.Drawing.Point(3, 16);
            this.pnlInfoHistory.Name = "pnlInfoHistory";
            this.pnlInfoHistory.Size = new System.Drawing.Size(304, 201);
            this.pnlInfoHistory.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.lblCurrentSelection);
            this.splitContainer3.Panel1.Controls.Add(this.pnlSelectedAgent);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer3.Size = new System.Drawing.Size(278, 599);
            this.splitContainer3.SplitterDistance = 270;
            this.splitContainer3.TabIndex = 0;
            // 
            // lblCurrentSelection
            // 
            this.lblCurrentSelection.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblCurrentSelection.AutoSize = true;
            this.lblCurrentSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentSelection.Location = new System.Drawing.Point(95, 9);
            this.lblCurrentSelection.Name = "lblCurrentSelection";
            this.lblCurrentSelection.Size = new System.Drawing.Size(88, 13);
            this.lblCurrentSelection.TabIndex = 3;
            this.lblCurrentSelection.Text = "Current Selection";
            // 
            // RegisteredAgentSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(913, 599);
            this.Controls.Add(this.splitContainer1);
            this.Icon = global::HB.Garnishments.UI.Properties.Resources.contact_icon;
            this.Name = "RegisteredAgentSelectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Registered Agent";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RegisteredAgentSelectionForm_FormClosing);
            this.Load += new System.EventHandler(this.RegisteredAgentSelectionForm_Load);
            this.Shown += new System.EventHandler(this.RegisteredAgentSelectionForm_Shown);
            this.tabControl1.ResumeLayout(false);
            this.tabSelection.ResumeLayout(false);
            this.tabSearch.ResumeLayout(false);
            this.tabSearch.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.grpPreviousAssetInfo.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.grpAllAccounts.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabSelection;
        private System.Windows.Forms.Panel pnlSelectedAgent;
        private System.Windows.Forms.TabPage tabSearch;
        private System.Windows.Forms.ListBox lstAgents;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label lblMatches;
        private System.Windows.Forms.Label lblRequested;
        private System.Windows.Forms.GroupBox grpPreviousAssetInfo;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Label lblCurrentSelection;
        private System.Windows.Forms.Label txtRequestedBy;
        private System.Windows.Forms.Panel pnlRequestedAsset;
        private System.Windows.Forms.Label lblRequestedAsset;
        private System.Windows.Forms.Button btnDeny;
        private System.Windows.Forms.Button btnSkip;
        private System.Windows.Forms.Button btnUseAgent;
        private System.Windows.Forms.Button btnExpandSearch;
        private System.Windows.Forms.TextBox txtUseNumber;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnUseNumber;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label txtDebtor;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Panel pnlAccountHistory;
        private System.Windows.Forms.Panel pnlInfoHistory;
        private System.Windows.Forms.GroupBox grpAllAccounts;
    }
}