using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Interfaces
{
    /// <summary>
    /// Interface - Items That Can Be Mailed
    /// </summary>
    public interface IAddressable
    {
        /// <summary>
        /// Name
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Address
        /// <para>Attention</para>
        /// </summary>
        string Attention { get; }
        /// <summary>
        /// Address
        /// <para>Line 1</para>
        /// </summary>
        string Address1 { get; }
        /// <summary>
        /// Address
        /// <para>Line 2</para>
        /// </summary>
        string Address2 { get; }
        /// <summary>
        /// Address
        /// <para>City</para>
        /// </summary>
        string City { get; }
        /// <summary>
        /// Address
        /// <para>State</para>
        /// </summary>
        string State { get; }
        /// <summary>
        /// Address
        /// <para>Zip</para>
        /// </summary>
        string Zip { get; }
    }
}
