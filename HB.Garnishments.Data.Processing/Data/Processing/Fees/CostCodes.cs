using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Processing.Fees
{
    public static class Codes
    {
        private static Dictionary<Enums.CostCode, byte> _CostCodes;
        private static Dictionary<Enums.AssetType, Enums.CostCode> _CodeLookup;

        public static Dictionary<Enums.CostCode, byte> CostCodes { get { if (_CostCodes == null) CreateCostCodesDictionary(); return _CostCodes; } }
        public static Dictionary<Enums.AssetType, Enums.CostCode> CodeLookup { get { if (_CodeLookup == null) CreateCodeLookupDictionary(); return _CodeLookup; } }

        private static void CreateCostCodesDictionary()
        {
            _CostCodes = new Dictionary<Enums.CostCode, byte>()
            {
                { Enums.CostCode.BankLevy, 72 },
                { Enums.CostCode.WageGarnish, 53 },
                { Enums.CostCode.SheriffService, 50 },
                { Enums.CostCode.ProcessService, 52 }
            };
        }
        private static void CreateCodeLookupDictionary()
        {
            _CodeLookup = new Dictionary<Enums.AssetType, Enums.CostCode>()
            {
                { Enums.AssetType.Bank, Enums.CostCode.BankLevy },
                { Enums.AssetType.Employer, Enums.CostCode.WageGarnish }
            };
        }
    }
}
