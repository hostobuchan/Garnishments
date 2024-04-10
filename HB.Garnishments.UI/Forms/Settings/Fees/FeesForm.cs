using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace HB.Garnishments.UI.Forms.Settings.Fees
{
    public partial class FeesForm : Form
    {
        protected Data.Venues.DataHandler DataHandler { get; private set; }

        public FeesForm()
        {
            InitializeComponent();
        }

        private async void FeesForm_Load(object sender, EventArgs e)
        {
            this.DataHandler = await Data.Venues.DataHandler.CreateDataHandlerAsync();
            this.cboVenue.DataSource = this.DataHandler.Venues;
        }

        private void FeesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DataHandler.Fees.Count(fee => fee.Updated) > 0)
            {
                if (MessageBox.Show("Updates Have Been Made!\r\n\r\nDo You Wish to Close Without Saving?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        #region Form Buttons 
        private void VENUE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((sender as ComboBox).SelectedItem is Data.Venues.Venue)
            {
                Data.Venues.Venue venue = (sender as ComboBox).SelectedItem as Data.Venues.Venue;

                // Create New Controls
                Controls.Settings.Fees.FeeControl feeControlBankInCounty = new Controls.Settings.Fees.FeeControl(this.DataHandler.GetFee(venue.VenueNo, true, true));
                Controls.Settings.Fees.FeeControl feeControlBankOutCounty = new Controls.Settings.Fees.FeeControl(this.DataHandler.GetFee(venue.VenueNo, true, false));
                Controls.Settings.Fees.FeeControl feeControlEmployerInCounty = new Controls.Settings.Fees.FeeControl(this.DataHandler.GetFee(venue.VenueNo, false, true));
                Controls.Settings.Fees.FeeControl feeControlEmployerOutCounty = new Controls.Settings.Fees.FeeControl(this.DataHandler.GetFee(venue.VenueNo, false, false));
                // Remove Previous Controls
                pnlBankInCounty.Controls.Clear();
                pnlBankOutCounty.Controls.Clear();
                pnlEmployerInCounty.Controls.Clear();
                pnlEmployerOutCounty.Controls.Clear();
                // Add New Controls
                pnlBankInCounty.Controls.Add(feeControlBankInCounty);
                pnlBankOutCounty.Controls.Add(feeControlBankOutCounty);
                pnlEmployerInCounty.Controls.Add(feeControlEmployerInCounty);
                pnlEmployerOutCounty.Controls.Add(feeControlEmployerOutCounty);
                // Set Formatting of New Controls
                feeControlBankInCounty.Dock = DockStyle.Fill;
                feeControlBankOutCounty.Dock = DockStyle.Fill;
                feeControlEmployerInCounty.Dock = DockStyle.Fill;
                feeControlEmployerOutCounty.Dock = DockStyle.Fill;
            }
        }

        private void ADDVENUE_Click(object sender, EventArgs e)
        {
            EnterNumberForm enf = new EnterNumberForm("Enter Venue No.", 4);
            if (enf.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    this.DataHandler.AddVenue(enf.EnteredNumber);
                    this.cboVenue.SelectedItem = this.DataHandler.Venues.FirstOrDefault(ven => ven.VenueNo == enf.EnteredNumber);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could Not Add Venue\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void COSTS_Click(object sender, EventArgs e)
        {

        }

        private async void SAVE_Click(object sender, EventArgs e)
        {
            try
            {
                await this.DataHandler.SaveAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could Not Save Updated Info\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void CLOSE_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
