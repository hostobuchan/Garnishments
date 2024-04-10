using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Requests.Results
{
    /// <summary>
    /// Status Result Information &amp; Account and Asset Information
    /// </summary>
    public class AssetRequestProcessingResult
    {
        /// <summary>
        /// Request &amp; Asset Info
        /// </summary>
        public AssetRequest Request { get; private set; }
        /// <summary>
        /// Account Information from CLS
        /// </summary>
        public Accounts.EvaluateeAccount Account { get; set; }
        /// <summary>
        /// Status Result
        /// </summary>
        public Result SelectedResult { get; set; }
        /// <summary>
        /// Additional Values for Result
        /// </summary>
        public Dictionary<string, object> AdditionalValues { get; set; }
        /// <summary>
        /// Notes
        /// </summary>
        public string Note { get; set; }

        public AssetRequestProcessingResult(AssetRequest request)
        {
            this.Request = request;
        }
        public AssetRequestProcessingResult(AssetRequestAndAccount request)
        {
            this.Request = request;
            this.Account = request.Account;
        }



        public override string ToString()
        {
            return $"{Request.ToString("F")} - {SelectedResult?.Description}";
        }
    }
}
