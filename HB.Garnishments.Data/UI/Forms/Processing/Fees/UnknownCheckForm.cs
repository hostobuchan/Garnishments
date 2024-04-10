using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HB.Garnishments.UI.Forms.Processing.Fees
{
    public partial class UnknownCheckForm : Form
    {
        Data.Requests.AssetRequest Request { get; set; }
        Data.Accounts.EvaluateeAccount Account { get; set; }
        Data.Accounts.Venue Venue { get; set; }
        bool InState { get; set; }
        bool InCounty { get; set; }
        decimal _CheckAmount;
        byte _CostCode;
        public decimal CheckAmount { get { return _CheckAmount; } }
        public byte CostCode { get { return _CostCode; } }

        public UnknownCheckForm(Data.Requests.AssetRequest request, Data.Accounts.EvaluateeAccount account, Data.Accounts.Venue venue, bool inState, bool inCounty)
        {
            this.Request = request;
            this.Account = account;
            this.Venue = venue;
            this.InState = inState;
            this.InCounty = inCounty;
            InitializeComponent();
        }

        private void UnknownCheckForm_Load(object sender, EventArgs e)
        {
            this.Text = $"\"{this.Account.FileNo}\"";
            try
            {
                COUNTY.Text = this.Venue.County;
                if (InState)
                {
                    if (InCounty)
                    {
                        INFO.Text = $"In County Service - {(this.Request.Asset.Type == Data.Enums.AssetType.Bank ? "Bank" : "POE")}";
                    }
                    else
                    {
                        INFO.Text = $"Out of County Service - {(this.Request.Asset.Type == Data.Enums.AssetType.Bank ? "Bank" : "POE")}";
                    }
                }
                else
                {
                    INFO.Text = $"Out of State Service - {(this.Request.Asset.Type == Data.Enums.AssetType.Bank ? "Bank" : "POE")}";
                }
            }
            catch
            {
                if (string.IsNullOrWhiteSpace(COUNTY.Text)) COUNTY.Text = "INDETERMINABLE";
                INFO.Text = $"{(this.Request.Asset.Type == Data.Enums.AssetType.Bank ? "Bank" : "POE")}";
            }
        }

        private void ADD_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(COST.Text, out _CheckAmount) && byte.TryParse(CODE.Text, out _CostCode))
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Retry;
                this.Close();
            }
        }

        private void DONE_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(COST.Text, out _CheckAmount) && byte.TryParse(CODE.Text, out _CostCode))
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            else if (COST.Text == "" && CODE.Text == "")
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
            }
        }
    }
}
