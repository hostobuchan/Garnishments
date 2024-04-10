using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HB.Garnishments.Data.Assets.Base;
using HB.Garnishments.Data.Enums;
using HB.Garnishments.Data.Interfaces;

namespace HB.SkipTracing.Data.Assets
{
    public class AssetInfoSurrogate<T> : Garnishments.Data.Interfaces.IAssetInfo where T : Records.DownloadRecord
    {
        T DownloadRecord { get; set; }

        AssetType IAssetInfo.Type { get { return Dictionaries.AssetTypeTranslator[this.DownloadRecord.AssetType]; } }

        string IAssetInfo.Contact { get { return this.DownloadRecord.Contact; } }

        bool? IAssetInfo.Good { get { return null; } }

        Phone[] IAssetInfo.Phones
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this.DownloadRecord.Phone))
                {
                    return new Phone[] { new Phone(0, PhoneType.Work, this.DownloadRecord.Phone) };
                }
                else
                {
                    return new Phone[0];
                }
            }
        }

        string IAddressable.Name { get { return this.DownloadRecord.Name; } }

        string IAddressable.Attention { get { return this.DownloadRecord.Contact; } }

        string IAddressable.Address1 { get { return this.DownloadRecord.Address1; } }

        string IAddressable.Address2 { get { return this.DownloadRecord.Address2; } }

        string IAddressable.City { get { return this.DownloadRecord.City; } }

        string IAddressable.State { get { return this.DownloadRecord.State; } }

        string IAddressable.Zip { get { return this.DownloadRecord.Zip; } }

        public AssetInfoSurrogate(T downloadRecord)
        {
            this.DownloadRecord = downloadRecord;
        }
    }
}
