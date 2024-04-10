using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Accounts
{
    /// <summary>
    /// Account
    /// </summary>
    [DataContract(Name = "Account", Namespace = "")]
    public class Account
    {
        /// <summary>
        /// Account ID
        /// <para>[ANID]</para>
        /// </summary>
        [DataMember(Name = "UID")]
        public int ID { get; private set; }
        /// <summary>
        /// CLS FileNo.
        /// </summary>
        [DataMember(Name = "FILENO")]
        public string FileNo { get; private set; }
    }
}
