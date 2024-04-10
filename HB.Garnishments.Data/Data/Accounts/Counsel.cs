using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using HB.Garnishments.Data.Interfaces;

namespace HB.Garnishments.Data.Accounts
{
    [DataContract(Name = "Adva", Namespace = "")]
    public class Counsel : Interfaces.IAddressable
    {
        string _City;
        string _State;
        string _Zip;

        [DataMember(Name = "ADVA_ATTYNO")]
        public int ID { get; private set; }
        [DataMember(Name = "FIRM_NAME")]
        public string Name { get; private set; }
        [DataMember(Name = "FIRM_NAME2")]
        public string Attention { get; private set; }
        [DataMember(Name = "FIRM_STREET")]
        public string Address1 { get; private set; }
        [IgnoreDataMember]
        public string Address2 { get; private set; }
        [DataMember(Name = "FIRM_CSZ")]
        public string CityStateZip { get; private set; }
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
