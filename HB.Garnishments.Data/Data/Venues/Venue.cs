using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Venues
{
    [DataContract(Name = "Venue", Namespace = "")]
    public class Venue
    {
        [DataMember(Name = "VENUE")]
        public int VenueNo { get; private set; }
        [DataMember(Name = "COUNTY")]
        public string County { get; private set; }
        [DataMember(Name = "STATE")]
        public string State { get; private set; }
        [DataMember(Name = "CLERK")]
        public string Clerk { get; private set; }
        [DataMember(Name = "TYPE")]
        public string CourtType { get; private set; }
        [DataMember(Name = "DESIGNATION")]
        public string CourtDesignation { get; private set; }
        [DataMember(Name = "WEFILE")]
        public bool WeFileInVenue { get; private set; }

        protected internal Venue(int venueNo, string county, string state, string clerk, string courtType, string courtDesignation, bool weFile)
        {
            this.VenueNo = venueNo;
            this.County = county;
            this.State = state;
            this.Clerk = clerk;
            this.CourtType = courtType;
            this.CourtDesignation = courtDesignation;
            this.WeFileInVenue = weFile;
        }

        public override string ToString()
        {
            return $"{VenueNo} - {Clerk}";
        }
    }
}
