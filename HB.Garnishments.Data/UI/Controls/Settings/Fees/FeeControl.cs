using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HB.Garnishments.UI.Controls.Settings.Fees
{
    public partial class FeeControl : UserControl
    {
        public Data.Venues.Fees.Fee Fee { get; private set; }

        public FeeControl(Data.Venues.Fees.Fee Fee)
        {
            this.Fee = Fee;
            InitializeComponent();
        }

        private void FeeControl_Load(object sender, EventArgs e)
        {
            this.chkCourtFee.DataBindings.Add("Checked", this.Fee, "CourtFee");
            this.chkServiceFee.DataBindings.Add("Checked", this.Fee, "ServiceFee");
            this.chkSheriff.DataBindings.Add("Checked", this.Fee, "ServiceBySheriff");
            this.chkCombineFees.DataBindings.Add("Checked", this.Fee, "CombineChecks");
            this.txtCourtFeeAmount.DataBindings.Add("Text", this.Fee, "CourtFeeAmount");
            this.txtServiceFeeAmount.DataBindings.Add("Text", this.Fee, "ServiceFeeAmount");
        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Back && !System.Text.RegularExpressions.Regex.IsMatch($"{e.KeyChar}", @"[\d\.]+"))
            {
                e.Handled = true;
            }
        }
    }
}
