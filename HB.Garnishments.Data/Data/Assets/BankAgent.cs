using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using HB.Garnishments.Data.Enums;

namespace HB.Garnishments.Data.Assets
{
    [DataContract(Name = "Bank", Namespace = "")]
    public class BankAgent : Base.RegisteredAgent
    {
        [DataMember(Name = "CSZ")]
        private string _CSZ;
        private string _City;
        private string _State;
        private string _Zip;

        public override AssetType AssetType { get { return AssetType.Bank; } }
        public override string City
        {
            get
            {
                try
                {
                    if (_City == null)
                    {
                        var match = System.Text.RegularExpressions.Regex.Match(_CSZ, @"(?<City>[\w\s]+)((,(\s+)?)|(\s+))(?<State>\w{2})(\s+(?<Zip>\d+(\-\d+)?))?$");
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
                catch
                {
                    return "";
                }
            }
        }
        public override string State
        {
            get
            {
                try
                {
                    if (_State == null)
                    {
                        var match = System.Text.RegularExpressions.Regex.Match(_CSZ, @"(?<City>[\w\s]+)((,(\s+)?)|(\s+))(?<State>\w{2})(\s+(?<Zip>\d+(\-\d+)?))?$");
                        if (match.Success)
                        {
                            if (match.Groups["City"].Success) _City = match.Groups["City"].Value.Trim();
                            if (match.Groups["State"].Success) _State = match.Groups["State"].Value.Trim();
                            if (match.Groups["Zip"].Success) _Zip = match.Groups["Zip"].Value.Trim();
                        }
                        else
                        {
                            throw new NotImplementedException();
                            //return Current.Zips.ZipList.Find(el => el.ZipCode == _Zip).StateAbrev;
                        }
                    }
                    return _State;
                }
                catch
                {
                    return "";
                }
            }
        }
        public override string Zip
        {
            get
            {
                try
                {
                    if (_Zip == null)
                    {
                        var match = System.Text.RegularExpressions.Regex.Match(_CSZ, @"(?<City>[\w\s]+)((,(\s+)?)|(\s+))(?<State>\w{2})(\s+(?<Zip>\d+(\-\d+)?))?$");
                        if (match.Success)
                        {
                            if (match.Groups["City"].Success) _City = match.Groups["City"].Value.Trim();
                            if (match.Groups["State"].Success) _State = match.Groups["State"].Value.Trim();
                            if (match.Groups["Zip"].Success) _Zip = match.Groups["Zip"].Value.Trim();
                        }
                        else
                        {
                            throw new NotImplementedException();
                            //return Current.Zips.ZipList.Find(el => el.ZipCode == _Zip).StateAbrev;
                        }
                    }
                    return _Zip;
                }
                catch
                {
                    return "";
                }
            }
        }
        public override string CityStateZip { get { return _CSZ; } }

        [DataMember(Name = "BANK_KEY")]
        public string BankKey { get; private set; }
        [DataMember(Name = "ABA_NO")]
        public string ABANumber { get; private set; }
        public string FullZip { get { try { return _CSZ.Substring(_CSZ.IndexOf(_Zip)).Trim(); } catch { return ""; } } }
    }
}
