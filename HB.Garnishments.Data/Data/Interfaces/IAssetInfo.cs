using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Interfaces
{
    /// <summary>
    /// Interface - Items That Can Be Garned
    /// </summary>
    public interface IAssetInfo : IAddressable
    {
        /// <summary>
        /// Asset Type
        /// <para>(Bank or Employer)</para>
        /// </summary>
        Enums.AssetType Type { get; }
        /// <summary>
        /// Contact Name
        /// </summary>
        string Contact { get; }
        /// <summary>
        /// Information Disposition
        /// <para>Known, Successful Capital Generator</para>
        /// </summary>
        bool? Good { get; }
        /// <summary>
        /// List of Phone Numbers
        /// </summary>
        Assets.Base.Phone[] Phones { get; }
    }
}
