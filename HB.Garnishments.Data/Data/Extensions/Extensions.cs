using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Extensions
{
    public static class Extensions
    {

        /// <summary>
        /// Groups Asset Request and Account Items by Venue County
        /// </summary>
        /// <param name="requests">Requests</param>
        /// <param name="venues">Listing of All Venues</param>
        /// <returns>Grouped Requests by Venue County</returns>
        public static IEnumerable<IGrouping<string, Data.Requests.AssetRequestAndAccount>> GroupByCounty(this IEnumerable<Data.Requests.AssetRequestAndAccount> requests, IEnumerable<Data.Accounts.Venue> venues)
        {
            return requests.GroupBy(req => venues.FirstOrDefault(venue => venue.VenueNo == req.Account.Venue).County);
        }
    }
}
