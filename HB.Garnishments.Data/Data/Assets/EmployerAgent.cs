using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using HB.Garnishments.Data.Enums;

namespace HB.Garnishments.Data.Assets
{
    [DataContract(Name = "Employer", Namespace = "")]
    public class EmployerAgent : Base.RegisteredAgent
    {
        [DataMember(Name = "CS")]
        private string _CS;
        [DataMember(Name = "ZIP")]
        private string _Zip;
        private string _City;
        private string _State;

        public override AssetType AssetType { get { return AssetType.Employer; } }
        public override string City
        {
            get
            {
                try
                {
                    if (_City == null)
                    {
                        var match = System.Text.RegularExpressions.Regex.Match(_CS, @"(?<City>[\w\s]+)((,(\s+)?)|(\s+))(?<State>\w{2})(\s+(?<Zip>\d+(\-\d+)?))?$");
                        if (match.Success)
                        {
                            if (match.Groups["City"].Success) _City = match.Groups["City"].Value.Trim();
                            if (match.Groups["State"].Success) _State = match.Groups["State"].Value.Trim();
                            if (match.Groups["Zip"].Success && string.IsNullOrEmpty(_Zip)) _Zip = match.Groups["Zip"].Value.Trim();
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
                        var match = System.Text.RegularExpressions.Regex.Match(_CS, @"(?<City>[\w\s]+)((,(\s+)?)|(\s+))(?<State>\w{2})(\s+(?<Zip>\d+(\-\d+)?))?$");
                        if (match.Success)
                        {
                            if (match.Groups["City"].Success) _City = match.Groups["City"].Value.Trim();
                            if (match.Groups["State"].Success) _State = match.Groups["State"].Value.Trim();
                            if (match.Groups["Zip"].Success && string.IsNullOrEmpty(_Zip)) _Zip = match.Groups["Zip"].Value.Trim();
                        }
                        else
                        {
                            throw new NotImplementedException();
                            //return Current.Zips.ZipList.Find(el => el.ZipCode == _Zip).PrefCityStateName;
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
                        var match = System.Text.RegularExpressions.Regex.Match(_CS, @"(?<City>[\w\s]+)((,(\s+)?)|(\s+))(?<State>\w{2})(\s+(?<Zip>\d+(\-\d+)?))?$");
                        if (match.Success)
                        {
                            if (match.Groups["City"].Success) _City = match.Groups["City"].Value.Trim();
                            if (match.Groups["State"].Success) _State = match.Groups["State"].Value.Trim();
                            if (match.Groups["Zip"].Success && string.IsNullOrEmpty(_Zip)) _Zip = match.Groups["Zip"].Value.Trim();
                        }
                        else
                        {
                            throw new NotImplementedException();
                            //return Current.Zips.ZipList.Find(el => el.ZipCode == _Zip).PrefCityStateName;
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
        public override string CityStateZip { get { return City + ", " + State + " " + Zip; } }

        [DataMember(Name = "PO")]
        public string PO { get; private set; }
        [DataMember(Name = "PAYROLL")]
        public string Payroll { get; private set; }
        [DataMember(Name = "EMPL_KEY")]
        public string EmployerKey { get; private set; }
        [DataMember(Name = "SALUT")]
        public string Salut { get; private set; }
    }
}
