using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Requests
{
    public class AssetRequestAndAccount : AssetRequest, IFormatProvider, IFormattable
    {
        public Accounts.EvaluateeAccount Account { get; private set; }
        public int SalesNo { get { return this.Account.SalesNo; } }

        public AssetRequestAndAccount(AssetRequest assetRequest, Accounts.EvaluateeAccount account)
        {
            this.ID = assetRequest.ID;
            this.AssetInfoID = assetRequest.AssetInfoID;
            this.RegisteredAgent = assetRequest.RegisteredAgent;
            this.History = assetRequest.History;
            this.Asset = assetRequest.Asset;
            this.Account = account;
        }

        public new string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case "F":
                    return $"{Asset.Account.FileNo} - {Asset.AssetName.Type}";
                case "E":
                    return $"[{CurrentStatus.Date:yyyy-MM-dd}] {CurrentStatus.Type} by {CurrentStatus.User.DisplayName} -- [Garn ID: {ID}][Asset ID: {Asset.Info.ID}] -- {Asset.Address1} {Asset.Address2}, {Asset.City}, {Asset.State} {Asset.Zip}";
                default:
                    return $"[{ID}] {CurrentStatus.Type} by {CurrentStatus.User.DisplayName}";
            }
        }
    }
}
