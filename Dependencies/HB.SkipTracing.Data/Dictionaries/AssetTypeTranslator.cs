using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.SkipTracing.Data
{
    public static partial class Dictionaries
    {
        private static Dictionary<Enums.AssetType, Garnishments.Data.Enums.AssetType> _AssetTypeTranslator;
        public static Dictionary<Enums.AssetType, Garnishments.Data.Enums.AssetType> AssetTypeTranslator { get { if (_AssetTypeTranslator == null) LoadAssetTypeTranslator(); return _AssetTypeTranslator; } }

        private static void LoadAssetTypeTranslator()
        {
            _AssetTypeTranslator = new Dictionary<Enums.AssetType, Garnishments.Data.Enums.AssetType>()
            {
                { Enums.AssetType.Bank, Garnishments.Data.Enums.AssetType.Bank },
                { Enums.AssetType.Employer, Garnishments.Data.Enums.AssetType.Employer }
            };
        }
    }
}
