using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Venues.Fees
{
    [DataContract(Name = "Fee", Namespace = "")]
    public class Fee
    {
        #region Properties

        #region Private
        [DataMember(Name = "IS_SHERIFF")]
        private bool _IsSheriff;
        [DataMember(Name = "FEE")]
        private bool _CFee;
        [DataMember(Name = "SFEE")]
        private bool _SFee;
        [DataMember(Name = "COMBINE")]
        private bool _Combine;
        [DataMember(Name = "AMT")]
        private decimal _CAmt;
        [DataMember(Name = "SAMT")]
        private decimal _SAmt;
        #endregion

        #region Public
        [DataMember(Name = "VENUE")]
        public int Venue { get; private set; }
        [DataMember(Name = "IN_COUNTY")]
        public bool InCounty { get; private set; }
        [DataMember(Name = "IS_BANK")]
        public bool IsBank { get; private set; }
        [IgnoreDataMember]
        public bool ServiceBySheriff { get { return _IsSheriff; } set { _IsSheriff = value; this.Updated = true; } }
        [IgnoreDataMember]
        public bool CourtFee { get { return _CFee; } set { _CFee = value; this.Updated = true; } }
        [IgnoreDataMember]
        public bool ServiceFee { get { return _SFee; } set { _SFee = value; this.Updated = true; } }
        [IgnoreDataMember]
        public bool CombineChecks { get { return _Combine; } set { _Combine = value; this.Updated = true; } }
        [IgnoreDataMember]
        public decimal CourtFeeAmount { get { return _CAmt; } set { _CAmt = value; this.Updated = true; } }
        [IgnoreDataMember]
        public decimal ServiceFeeAmount { get { return _SAmt; } set { _SAmt = value; this.Updated = true; } }
        [IgnoreDataMember]
        public bool Updated { get; private set; }
        #endregion

        #endregion

        #region Initializer Overloads 
        public Fee(int Venue, bool InCounty, bool IsBank)
        {
            this.Venue = Venue;
            this.InCounty = InCounty;
            this.IsBank = IsBank;
            _IsSheriff = true;
            _CFee = false;
            _SFee = false;
            _Combine = false;
            _CAmt = 0;
            _SAmt = 0;
            this.Updated = true;
        }
        public Fee(int Venue, bool InCounty, bool IsBank, bool CourtFee, decimal CourtFeeAmount, bool ServiceFee, decimal ServiceFeeAmount, bool ServiceBySheriff, bool CombineChecks)
        {
            this.Venue = Venue;
            this.InCounty = InCounty;
            this.IsBank = IsBank;
            _IsSheriff = ServiceBySheriff;
            _CFee = CourtFee;
            _SFee = ServiceFee;
            _Combine = CombineChecks;
            _CAmt = CourtFeeAmount;
            _SAmt = ServiceFeeAmount;
            this.Updated = true;
        }
        #endregion

        
        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            if (this.Updated == true)
            {
                this.Updated = false;
            }
        }
    }
}
