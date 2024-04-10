using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Users
{
    [DataContract(Name = "User", Namespace = "", IsReference = true)]
    public class User
    {
        private bool _Loaded = false;
        private System.DirectoryServices.AccountManagement.UserPrincipal _Principal;

        [DataMember(Name = "UID")]
        public int ID { get; private set; }
        [DataMember(Name = "USERNAME")]
        public string UserName { get; private set; }

        [IgnoreDataMember]
        public string DisplayName
        {
            get
            {
                if (!_Loaded)
                {
                    _Principal = System.DirectoryServices.AccountManagement.UserPrincipal.FindByIdentity(new System.DirectoryServices.AccountManagement.PrincipalContext(System.DirectoryServices.AccountManagement.ContextType.Domain), this.UserName);
                    _Loaded = true;
                }
                return _Principal?.DisplayName ?? this.UserName;
            }
        }
    }
}
