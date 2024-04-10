using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace HB.Garnishments.UI.Forms.Settings.Overrides
{
    public partial class ChangeRegisteredAgentForm : Form
    {
        Data.Enums.AssetType AssetType { get; set; }
        Data.Assets.Base.RegisteredAgent FromAgent { get; set; }
        Data.Assets.Base.RegisteredAgent ToAgent { get; set; }

        private ChangeRegisteredAgentForm(Data.Enums.AssetType assetType)
        {
            this.AssetType = assetType;
            InitializeComponent();
        }
        public ChangeRegisteredAgentForm(Data.Enums.AssetType assetType, int from, int to) : this(assetType)
        {
            this.FromAgent = UI.Settings.Properties.RegisteredAgents.FirstOrDefault(agent => agent.ID == from && agent.AssetType == assetType);
            this.ToAgent = UI.Settings.Properties.RegisteredAgents.FirstOrDefault(agent => agent.ID == to && agent.AssetType == assetType);
        }
        public ChangeRegisteredAgentForm(Data.Enums.AssetType assetType, Data.Assets.Base.RegisteredAgent from, Data.Assets.Base.RegisteredAgent to) : this(assetType)
        {
            this.FromAgent = from;
            this.ToAgent = to;
        }

        private void ChangeRegisteredAgentForm_Load(object sender, EventArgs e)
        {
            if (this.FromAgent != null)
            {
                UI.Controls.Assets.RegisteredAgentControl fromControl = new Controls.Assets.RegisteredAgentControl(this.FromAgent);
                ElementHost hostFrom = new ElementHost()
                {
                    Child = fromControl
                };
                this.tableLayoutPanel1.Controls.Add(hostFrom);
                this.tableLayoutPanel1.SetRow(hostFrom, 1);
                hostFrom.Dock = DockStyle.Fill;
            }
            if (this.ToAgent != null)
            {
                UI.Controls.Assets.RegisteredAgentControl toControl = new Controls.Assets.RegisteredAgentControl(this.ToAgent);
                ElementHost hostTo = new ElementHost()
                {
                    Child = toControl
                };
                this.tableLayoutPanel1.Controls.Add(hostTo);
                this.tableLayoutPanel1.SetColumn(hostTo, 1);
                this.tableLayoutPanel1.SetRow(hostTo, 1);
                hostTo.Dock = DockStyle.Fill;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
