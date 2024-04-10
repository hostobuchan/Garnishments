using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Accounts
{
    [DataContract(Name = "Venue", Namespace = "")]
    public class Venue : Interfaces.IAddressable
    {
        private string _City;
        private string _State;
        private string _Zip;
        [DataMember(Name = "INACTIVE_FLAG")]
        private string _Inactive;

        [DataMember(Name = "VENUE_NO")]
        public int VenueNo { get; private set; }
        [DataMember(Name = "CLERK_NAME")]
        public string Name { get; private set; }
        [DataMember(Name = "CNTY_NAME")]
        public string County { get; private set; }
        [DataMember(Name = "CLERK_STREET")]
        public string Address1 { get; private set; }
        [DataMember(Name = "CLERK_CSZ")]
        public string CityStateZip { get; private set; }
        [IgnoreDataMember]
        public bool Inactive { get { return !string.IsNullOrWhiteSpace(_Inactive) && string.Equals(_Inactive, "Y", StringComparison.OrdinalIgnoreCase); } }
        [IgnoreDataMember]
        public string Attention { get { return null; } }
        [IgnoreDataMember]
        public string Address2 { get { return null; } }
        [IgnoreDataMember]
        public string City
        {
            get
            {
                if (_City == null)
                {
                    var match = System.Text.RegularExpressions.Regex.Match(this.CityStateZip, @"(?<City>[\w\s]+)((,(\s+)?)|(\s+))(?<State>\w{2})(\s+(?<Zip>\d+(\-\d+)?))?$");
                    if (match.Success)
                    {
                        if (match.Groups["City"].Success) _City = match.Groups["City"].Value.Trim();
                        if (match.Groups["State"].Success) _State = match.Groups["State"].Value.Trim();
                        if (match.Groups["Zip"].Success) _Zip = match.Groups["Zip"].Value.Trim();
                    }
                    else
                    {
                        throw new NotImplementedException();
                        //return Current.Zips.ZipList.Find(el => el.ZipCode == _Zip).PrefCityStateName;
                    }
                }
                return _City;
            }
        }
        [IgnoreDataMember]
        public string State
        {
            get
            {
                if (_State == null)
                {
                    var match = System.Text.RegularExpressions.Regex.Match(this.CityStateZip, @"(?<City>[\w\s]+)((,(\s+)?)|(\s+))(?<State>\w{2})(\s+(?<Zip>\d+(\-\d+)?))?$");
                    if (match.Success)
                    {
                        if (match.Groups["City"].Success) _City = match.Groups["City"].Value.Trim();
                        if (match.Groups["State"].Success) _State = match.Groups["State"].Value.Trim();
                        if (match.Groups["Zip"].Success) _Zip = match.Groups["Zip"].Value.Trim();
                    }
                    else
                    {
                        throw new NotImplementedException();
                        //return Current.Zips.ZipList.Find(el => el.ZipCode == _Zip).PrefCityStateName;
                    }
                }
                return _State;
            }
        }
        [IgnoreDataMember]
        public string Zip
        {
            get
            {
                if (_Zip == null)
                {
                    var match = System.Text.RegularExpressions.Regex.Match(this.CityStateZip, @"(?<City>[\w\s]+)((,(\s+)?)|(\s+))(?<State>\w{2})(\s+(?<Zip>\d+(\-\d+)?))?$");
                    if (match.Success)
                    {
                        if (match.Groups["City"].Success) _City = match.Groups["City"].Value.Trim();
                        if (match.Groups["State"].Success) _State = match.Groups["State"].Value.Trim();
                        if (match.Groups["Zip"].Success) _Zip = match.Groups["Zip"].Value.Trim();
                    }
                    else
                    {
                        throw new NotImplementedException();
                        //return Current.Zips.ZipList.Find(el => el.ZipCode == _Zip).PrefCityStateName;
                    }
                }
                return _Zip;
            }
        }
    }
}
