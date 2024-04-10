using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HB.Garnishments.Data.Enums;

namespace HB.Garnishments.Data.Assets
{
    public class UnknownAgent : Base.RegisteredAgent
    {
        AssetType _Type;

        public override AssetType AssetType { get { return _Type; } }

        public override string City => throw new NotImplementedException();

        public override string State => throw new NotImplementedException();

        public override string Zip => throw new NotImplementedException();

        public override string CityStateZip => throw new NotImplementedException();

        public UnknownAgent(int id, AssetType type)
        {
            this.ID = id;
            _Type = type;
        }
    }
}
