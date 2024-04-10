using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Assets
{
    /// <summary>
    /// Asset Information
    /// <para>Contact &amp; Address Info</para>
    /// </summary>
    [DataContract(Name = "AssetInfo", Namespace = "", IsReference = true)]
    public class AssetInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        [DataMember(Name = "UID")]
        public ulong ID { get; private set; }
        /// <summary>
        /// Asset Name
        /// </summary>
        [DataMember(Name = "AssetName")]
        public Base.Asset AssetName { get; private set; }
        /// <summary>
        /// Asset Contact Information
        /// </summary>
        [DataMember(Name = "AssetContact")]
        public Base.Contact AssetContact { get; private set; }
        /// <summary>
        /// Asset Address Information
        /// </summary>
        [DataMember(Name = "AssetLocation")]
        public Base.Location AssetLocation { get; private set; }
        /// <summary>
        /// Entry Date
        /// </summary>
        [DataMember(Name = "DATE")]
        public DateTime DateLocated { get; private set; }
        /// <summary>
        /// User Who Entered Information
        /// </summary>
        [DataMember(Name = "UserLocated")]
        public Users.User UserLocated { get; private set; }
        /// <summary>
        /// List of Phone Numbers
        /// </summary>
        [DataMember(Name = "Phones")]
        public Base.Phone[] Phones { get; private set; }

        [OnDeserialized]
        private void OnDeserializing(StreamingContext context)
        {
            if (this.Phones == null) this.Phones = new Base.Phone[0];
        }
    }
}
