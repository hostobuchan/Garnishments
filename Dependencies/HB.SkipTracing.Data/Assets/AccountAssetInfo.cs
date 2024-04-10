using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.SkipTracing.Data.Assets
{
    public class AccountAssetInfo<T> where T : Records.DownloadRecord
    {
        #region Private Properties
        private bool _MatchingAssetLoaded = false;
        private bool _PreviousGarnRequestLoaded = false;
        private bool _PreviousGarnResultLoaded = false;
        private Garnishments.Data.Assets.AccountAsset _MatchingAsset;
        private Garnishments.Data.Requests.AssetRequest _PreviousGarnRequest;
        private Garnishments.Data.Requests.Results.Result _PreviousGarnResult;
        #endregion

        public Garnishments.Data.Assets.AccountAssets AccountAssets { get; private set; }
        public T DownloadRecord { get; private set; }

        #region Calculated Properties
        public bool IsNew
        {
            get
            {
                return this.MatchingAsset == null;
            }
        }
        public bool IsGood
        {
            get
            {
                return this.MatchingAsset?.CurrentInfo?.Good ?? false;
            }
        }
        public bool HasGarn
        {
            get
            {
                return this.PreviousGarnRequest != null;
            }
        }
        public bool HasActiveGarn
        {
            get
            {
                return this.PreviousGarnRequest?.IsActive ?? false;
            }
        }
        public bool GoodGarnResult
        {
            get
            {
                return this.PreviousGarnResult?.Good ?? false;
            }
        }
        public Garnishments.Data.Assets.AccountAsset MatchingAsset
        {
            get
            {
                if (!_MatchingAssetLoaded)
                {
                    _MatchingAsset = this.AccountAssets?.Assets?.FirstOrDefault(ass => ass.Asset.Type == Dictionaries.AssetTypeTranslator[this.DownloadRecord.AssetType] && ass.Name.Equals(this.DownloadRecord.Name.Trim().Replace("\t", ""), StringComparison.OrdinalIgnoreCase));
                    _MatchingAssetLoaded = true;
                }
                return _MatchingAsset;
            }
        }
        public Garnishments.Data.Requests.AssetRequest PreviousGarnRequest
        {
            get
            {
                if (!_PreviousGarnRequestLoaded)
                {
                    var info = this.MatchingAsset?.CurrentInfo;
                    if (info != null)
                    {
                        var prevRequests = Task.Run(async () => await info?.GetRequestsAsync()).Result;
                        _PreviousGarnRequest = prevRequests?.OrderByDescending(req => req.ID)?.FirstOrDefault();
                    }
                    _PreviousGarnRequestLoaded = true;
                }
                return _PreviousGarnRequest;
            }
        }
        public Garnishments.Data.Requests.Results.Result PreviousGarnResult
        {
            get
            {
                if (!_PreviousGarnResultLoaded)
                {
                    _PreviousGarnResult = this.PreviousGarnRequest?[Garnishments.Data.Enums.Status.GarnisheeResponse]?.Result;
                    _PreviousGarnResultLoaded = true;
                }
                return _PreviousGarnResult;
            }
        }
        #endregion

        public AccountAssetInfo(T downloadRecord, Garnishments.Data.Assets.AccountAssets assets)
        {
            this.DownloadRecord = downloadRecord;
            this.AccountAssets = assets;
        }

        public async Task<Garnishments.Data.Assets.AccountAsset> AddAsset()
        {
            var result = await Garnishments.Data.DataHandler.AddAccountAssetAsync(
                this.DownloadRecord.FileNo,
                this.DownloadRecord.DebtorNo,
                Dictionaries.AssetTypeTranslator[this.DownloadRecord.AssetType],
                this.DownloadRecord.Name,
                this.DownloadRecord.Contact,
                new Garnishments.Data.Assets.Base.Location(
                    0,
                    this.DownloadRecord.Address1,
                    this.DownloadRecord.Address2,
                    this.DownloadRecord.City,
                    this.DownloadRecord.State,
                    this.DownloadRecord.Zip
                ),
                DateTime.Now,
                Settings.Properties.UserName,
                new Garnishments.Data.Assets.Base.Phone[]{
                    new Garnishments.Data.Assets.Base.Phone(0, Garnishments.Data.Enums.PhoneType.Work, this.DownloadRecord.Phone)
                }.Where(ph => !string.IsNullOrWhiteSpace(ph.PhoneNumber)),
                new Dictionary<int, string>()
                );
            return (await Garnishments.Data.DataHandler.GetAccountAssetAsync(this.AccountAssets.Account.ID, this.DownloadRecord.DebtorNo, result)).Assets.FirstOrDefault();
        }
    }
}
