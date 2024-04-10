using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HB.Garnishments.UI.Forms.Settings.Fees
{
    public partial class VenueForm : Form
    {
        public Data.Venues.Venue Venue { get; private set; }

        public VenueForm(Data.Venues.Venue venue)
        {
            this.Venue = venue;
            InitializeComponent();
        }

        private void VenueForm_Load(object sender, EventArgs e)
        {
            this.txtVenueNo.DataBindings.Add("Text", this.Venue, "VenueNo");
            this.txtClerkName.DataBindings.Add("Text", this.Venue, "Clerk");
            this.txtState.DataBindings.Add("Text", this.Venue, "State");
            this.txtCounty.DataBindings.Add("Text", this.Venue, "County");
            this.txtCourtType.DataBindings.Add("Text", this.Venue, "CourtType");
            this.txtCourtDesignation.DataBindings.Add("Text", this.Venue, "CourtDesignation");
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
