using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Requests
{
    /// <summary>
    /// Request &amp; Account Asset Info
    /// </summary>
    [DataContract(Name = "Request", Namespace = "")]
    public class AssetRequest : Request, IFormattable, IFormatProvider
    {
        [DataMember(Name = "Asset")]
        Assets.AccountAssetInfo _Asset;

        /// <summary>
        /// Account Asset Info
        /// </summary>
        [IgnoreDataMember]
        public Assets.AccountAssetInfo Asset
        {
            get
            {
                return GetAssetInfo();
            }
            set
            {
                _Asset = value;
            }
        }

        //internal AssetRequest(Serialization.Surrogates.AssetRequestsSurrogate surrogateHandler, Serialization.Surrogates.RequestSurrogate request)
        //{
        //    this.ID = request.ID;
        //    this.RegisteredAgent = request.RegisteredAgentID;
        //    this.History = surrogateHandler._Statuses.Where(s => s.RequestID == this.ID).Select(s => new Status(surrogateHandler, s)).ToArray();
        //}

        public Assets.AccountAssetInfo GetAssetInfo()
        {
            var task = Task.Run(GetAssetInfoAsync);
            task.Wait();
            return task.Result;
        }
        public async Task<Assets.AccountAssetInfo> GetAssetInfoAsync()
        {
            if (_Asset == null)
            {
                try
                {
                    _Asset = (await DataHandler.GetAccountAssetAsync(this.ID))?.Assets?.FirstOrDefault()?.History?.FirstOrDefault(aai => aai.Info.ID == this.AssetInfoID);
                }
                catch(Exception ex)
                {

                }
            }
            return _Asset;
        }
        public async Task SetRegisteredAgentAsync(int? agentId)
        {
            await DataHandler.UpdateRegisteredAgentAsync(this.ID, agentId);
            this.RegisteredAgent = agentId;
        }


        #region IFormattable, IFormatProvider
        public override string ToString(string format)
        {
            return ToString(format, this);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case "G":
                    return $"[Garn ID: {ID}] {Asset.Type} {Asset.Name} - {CurrentStatus.Type} {CurrentStatus.Date:yyyy-MM-dd}";
                case "F":
                    return $"{Asset.Account.FileNo} - {Asset.AssetName.Type}";
                case "E":
                    return $"[{CurrentStatus.Date:yyyy-MM-dd}] {CurrentStatus.Type} by {CurrentStatus.User.DisplayName} -- [Garn ID: {ID}][Asset ID: {Asset.Info.ID}] -- {Asset.Address1} {Asset.Address2}, {Asset.City}, {Asset.State} {Asset.Zip}";
                default:
                    return $"[{ID}] {CurrentStatus.Type} by {CurrentStatus.User.DisplayName}";
            }
        }

        public object GetFormat(Type formatType)
        {
            return this;
        }
        #endregion
    }
}
