using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Processing.Fees
{
    public class CourtFeeHandler
    {
        public Zips.Zip[] Zips { get; private set; }
        public Venues.DataHandler VenueData { get; private set; }

        public static async Task<CourtFeeHandler> CreateCourtFeeHandler()
        {
            return new CourtFeeHandler()
            {
                Zips = await Data.Zips.Zip.GetAllZipsAsync(),
                VenueData = await Venues.DataHandler.CreateDataHandlerAsync()
            };
        }

        public IEnumerable<Check> GetCostCheck(System.Threading.SynchronizationContext context, Requests.Results.AssetRequestProcessingResult requestAndAccount, Accounts.Venue venue, Assets.Base.RegisteredAgent agent)
        {
            List<Check> checks = new List<Check>();
            Venues.Venue cleanVenue = this.VenueData.Venues.FirstOrDefault(ven => ven.VenueNo == venue?.VenueNo);
            if (cleanVenue == null)
            {
                throw new Exception($"Venue {requestAndAccount.Account.Venue} Is Invalid Or Has Not Been Properly Set Up");
            }
            if (agent == null)
            {
                throw new Exception($"Agent Info Not Found: {requestAndAccount.Request.Asset.Type} #{requestAndAccount.Request.RegisteredAgent}\r\n\r\nIf You Entered This Agent Today, Wait Till Tomorrow");
            }
            switch (requestAndAccount.Account.SalesNo)
            {
                case 1:
                    #region Sales 1 - AR
                    checks.Add(
                        new Check(
                            requestAndAccount.Account.FileNo, 
                            string.Equals(cleanVenue.CourtType, "DISTRICT", StringComparison.OrdinalIgnoreCase) ? 10 : 20,
                            Codes.CostCodes[Codes.CodeLookup[requestAndAccount.Request.Asset.Type]],
                            ""
                        )
                    );
                    #endregion
                    break;
                case 3:
                    #region Sales 3 - TN
                    bool inState = string.Equals(venue.State, agent.State, StringComparison.OrdinalIgnoreCase);

                    if (inState) // Registered Agent is In-State
                    {
                        bool inCounty = string.Equals(this.Zips.FirstOrDefault(z => string.Equals(z.ZipCode, agent.Zip.Length > 5 ? agent.Zip.Substring(0, 5) : agent.Zip, StringComparison.OrdinalIgnoreCase)).County, venue.County, StringComparison.OrdinalIgnoreCase);
                        Venues.Fees.Fee fee = this.VenueData.GetFee(venue.VenueNo, requestAndAccount.Request.Asset.Type == Enums.AssetType.Bank, inCounty);

                        if (fee.CourtFee)
                        {
                            if (fee.ServiceFee)
                            {
                                if (fee.CombineChecks) // Only One Check - To Court
                                {
                                    checks.Add(new Check(
                                            requestAndAccount.Account.FileNo,
                                            fee.ServiceFeeAmount + fee.CourtFeeAmount,
                                            Codes.CostCodes[Codes.CodeLookup[requestAndAccount.Request.Asset.Type]],
                                            ""
                                        )
                                    );
                                }
                                else // Must Create Separate Checks
                                {
                                    // Court Check
                                    checks.Add(new Check(
                                            requestAndAccount.Account.FileNo,
                                            fee.CourtFeeAmount,
                                            Codes.CostCodes[Codes.CodeLookup[requestAndAccount.Request.Asset.Type]],
                                            ""
                                        )
                                    );
                                    if (fee.ServiceBySheriff)
                                    {
                                        // Sheriff Check
                                        checks.Add(new Check(
                                                requestAndAccount.Account.FileNo,
                                                fee.ServiceFeeAmount,
                                                Codes.CostCodes[Enums.CostCode.SheriffService],
                                                ""
                                            )
                                        );
                                    }
                                    else
                                    {
                                        // Process Server Check
                                        checks.Add(new Check(
                                                requestAndAccount.Account.FileNo,
                                                fee.ServiceFeeAmount,
                                                Codes.CostCodes[Enums.CostCode.ProcessService],
                                                ""
                                            )
                                        );
                                    }
                                }
                            }
                            else // No Service Fees - Only Court Fee
                            {
                                checks.Add(new Check(
                                        requestAndAccount.Account.FileNo,
                                        fee.CourtFeeAmount,
                                        Codes.CostCodes[Codes.CodeLookup[requestAndAccount.Request.Asset.Type]],
                                        ""
                                    )
                                );
                            }
                        }
                        else if (fee.ServiceFee) // No Court Fee - Only Service Fees
                        {
                            if (fee.ServiceBySheriff)
                            {
                                // Sheriff Check
                                checks.Add(new Check(
                                        requestAndAccount.Account.FileNo,
                                        fee.ServiceFeeAmount,
                                        Codes.CostCodes[Enums.CostCode.SheriffService],
                                        ""
                                    )
                                );
                            }
                            else
                            {
                                // Process Server Check
                                checks.Add(new Check(
                                        requestAndAccount.Account.FileNo,
                                        fee.ServiceFeeAmount,
                                        Codes.CostCodes[Enums.CostCode.ProcessService],
                                        ""
                                    )
                                );
                            }
                        }
                    }
                    else // Registered Agent is Out-Of-State
                    {
                        context.Send((callback) =>
                        {
                            UI.Forms.Processing.Fees.UnknownCheckForm ucf = new UI.Forms.Processing.Fees.UnknownCheckForm(requestAndAccount.Request, requestAndAccount.Account, venue, inState, false);
                            System.Windows.Forms.DialogResult CheckResult = ucf.ShowDialog();
                            while (CheckResult == System.Windows.Forms.DialogResult.Retry)
                            {
                                checks.Add(new Check(requestAndAccount.Account.FileNo, ucf.CheckAmount, ucf.CostCode, ""));
                                ucf = new UI.Forms.Processing.Fees.UnknownCheckForm(requestAndAccount.Request, requestAndAccount.Account, venue, inState, false);
                                CheckResult = ucf.ShowDialog();
                            }
                            if (CheckResult == System.Windows.Forms.DialogResult.OK)
                            {
                                checks.Add(new Check(requestAndAccount.Account.FileNo, ucf.CheckAmount, ucf.CostCode, ""));
                            }
                        }, null);
                    }
                    #endregion
                    break;
                case 5:
                    #region Sales 5 - IN

                    break;
                    #endregion
            }
            return checks;
        }
    }
}
