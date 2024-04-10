using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using HB.Garnishments.Data.Venues.Fees;

namespace HB.Garnishments.Data.Venues
{
    public class DataHandler
    {
        public List<Fee> Fees { get; private set; } = new List<Fee>();
        public BindingList<Venue> Venues { get; private set; } = new BindingList<Venue>();

        private DataHandler()
        {

        }

        public static async Task<DataHandler> CreateDataHandlerAsync()
        {
            var handler = new DataHandler();
            await handler.LoadAsync();
            return handler;
        }

        public void Load()
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("Get_CourtFees", conn) { CommandType = CommandType.StoredProcedure })
                {
                    conn.Open();

                    var xml = cmd.ExecuteXmlReader();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Fee[]));
                    var obj = serializer.ReadObject(xml);
                    this.Fees = new List<Fee>(obj as Fee[]);
                }
            }
            using (SqlConnection conn = new SqlConnection(Settings.Connections.ControlPanels))
            {
                using (SqlCommand cmd = new SqlCommand("Get_Venues_XML", conn))
                {
                    conn.Open();

                    var xml = cmd.ExecuteXmlReader();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Venue[]));
                    var obj = serializer.ReadObject(xml);
                    this.Venues = new BindingList<Venue>(new List<Venue>(obj as Venue[]));
                    //this.Venues.Add(new Venue(0, "NO VENUE / BAD VENUE", "", "NO VENUE / BAD VENUE", "", "", false));
                }
            }
        }
        public async Task LoadAsync()
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
            {
                using (SqlCommand cmd = new SqlCommand("Get_CourtFees", conn) { CommandType = CommandType.StoredProcedure })
                {
                    await conn.OpenAsync();

                    var xml = await cmd.ExecuteXmlReaderAsync();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Fee[]));
                    var obj = serializer.ReadObject(xml);
                    this.Fees = new List<Fee>(obj as Fee[]);
                }
            }
            using (SqlConnection conn = new SqlConnection(Settings.Connections.ControlPanels))
            {
                using (SqlCommand cmd = new SqlCommand("Get_Venues_XML", conn))
                {
                    await conn.OpenAsync();

                    var xml = await cmd.ExecuteXmlReaderAsync();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Venue[]));
                    var obj = serializer.ReadObject(xml);
                    this.Venues = new BindingList<Venue>(new List<Venue>(obj as Venue[]));
                    //this.Venues.Add(new Venue(0, "NO VENUE / BAD VENUE", "", "NO VENUE / BAD VENUE", "", "", false));
                }
            }
        }

        public Fee GetFee(int venue, bool isBank, bool inCounty)
        {
            var rfee = Fees.Find(fee => fee.Venue == venue && fee.IsBank == isBank && fee.InCounty == inCounty);
            if (rfee == null)
            {
                rfee = new Fee(venue, inCounty, isBank);
                this.Fees.Add(rfee);
            }
            return rfee;
        }

        public void AddVenue(int venue)
        {
            Venue newVenue = null;
            using (SqlConnection conn = new SqlConnection(Settings.Connections.ALLCLS))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT [VENUE_NO],[CLERK_NAME],[CLERK_CSZ],[CNTY_NAME],[COURT_TYPE],[DESIGNATION] FROM CLERKF WHERE[VENUE_NO]=@VENUE_NO", conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@VENUE_NO", SqlDbType.Int) { Value = venue });

                    conn.Open();
                    var sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        var state = string.Empty;
                        try
                        {
                            state = System.Text.RegularExpressions.Regex.Match(sdr["CLERK_CSZ"].ToString(), @"^(?<city>[^\,]+)((\,(\ )?)|(\ ))(?<state>\w+)(\ )(?<zip>[\d]{5}((\-)?[\d]{4})?)$").Groups["state"].Value;
                        }
                        catch { }
                        newVenue = new Venue(
                            venue,
                            sdr["CNTY_NAME"].ToString(),
                            state,
                            sdr["CLERK_NAME"].ToString(),
                            sdr["COURT_TYPE"].ToString(),
                            sdr["DESIGNATION"].ToString(),
                            true
                        );

                        this.Venues.Add(newVenue);
                        this.Fees.AddRange(new[]
                        {
                            new Fee(venue, false, false),
                            new Fee(venue, false, true),
                            new Fee(venue, true, false),
                            new Fee(venue, true, true)
                        });
                    }
                }
            }
            using (SqlConnection conn = new SqlConnection(Settings.Connections.ControlPanels))
            {
                using (SqlCommand cmd = new SqlCommand("AddUpdateVenue", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add(new SqlParameter("@VENUE_NO", SqlDbType.Int) { Value = newVenue.VenueNo });
                    cmd.Parameters.Add(new SqlParameter("@NAME", SqlDbType.NVarChar) { Value = newVenue.Clerk });
                    cmd.Parameters.Add(new SqlParameter("@STATE", SqlDbType.NChar, 2) { Value = newVenue.State });
                    cmd.Parameters.Add(new SqlParameter("@COUNTY", SqlDbType.NVarChar) { Value = newVenue.County });
                    cmd.Parameters.Add(new SqlParameter("@TYPE", SqlDbType.NVarChar) { Value = newVenue.CourtType });
                    cmd.Parameters.Add(new SqlParameter("@DESIGNATION", SqlDbType.NVarChar) { Value = newVenue.CourtDesignation });
                    cmd.Parameters.Add(new SqlParameter("@WEFILE", SqlDbType.Bit) { Value = newVenue.WeFileInVenue });

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public async Task<IEnumerable<Fee>> AddVenueAsync(int venue)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            if (this.Fees.Count(fee => fee.Updated) > 0)
            {
                using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
                {
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        foreach (var fee in this.Fees.Where(f => f.Updated))
                        {
                            using (SqlCommand cmd = new SqlCommand("Update_VenueFee", conn) { CommandType = CommandType.StoredProcedure, Transaction = tran })
                            {
                                cmd.Parameters.Add(new SqlParameter("@VENUE", SqlDbType.Int ) { Value = fee.Venue });
                                cmd.Parameters.Add(new SqlParameter("@IN_COUNTY", SqlDbType.Bit ) { Value = fee.InCounty });
                                cmd.Parameters.Add(new SqlParameter("@IS_BANK", SqlDbType.Bit ) { Value = fee.IsBank });
                                cmd.Parameters.Add(new SqlParameter("@FEE", SqlDbType.Bit ) { Value = fee.CourtFee });
                                cmd.Parameters.Add(new SqlParameter("@AMT", SqlDbType.Decimal ) { Value = fee.CourtFeeAmount });
                                cmd.Parameters.Add(new SqlParameter("@SFEE", SqlDbType.Bit ) { Value = fee.ServiceFee });
                                cmd.Parameters.Add(new SqlParameter("@SAMT", SqlDbType.Decimal ) { Value = fee.ServiceFeeAmount });
                                cmd.Parameters.Add(new SqlParameter("@COMBINE", SqlDbType.Bit ) { Value = fee.CombineChecks });
                                cmd.Parameters.Add(new SqlParameter("@IS_SHERIFF", SqlDbType.Bit ) { Value = fee.ServiceBySheriff });

                                cmd.ExecuteNonQuery();
                            }
                        }
                        tran.Commit();
                    }
                }
            }
        }
        public async Task SaveAsync()
        {
            if (this.Fees.Count(fee => fee.Updated) > 0)
            {
                using (SqlConnection conn = new SqlConnection(Settings.Connections.Garnishments))
                {
                    await conn.OpenAsync();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        foreach (var fee in this.Fees.Where(f => f.Updated))
                        {
                            using (SqlCommand cmd = new SqlCommand("Update_VenueFee", conn) { CommandType = CommandType.StoredProcedure, Transaction = tran })
                            {
                                cmd.Parameters.Add(new SqlParameter("@VENUE", SqlDbType.Int) { Value = fee.Venue });
                                cmd.Parameters.Add(new SqlParameter("@IN_COUNTY", SqlDbType.Bit) { Value = fee.InCounty });
                                cmd.Parameters.Add(new SqlParameter("@IS_BANK", SqlDbType.Bit) { Value = fee.IsBank });
                                cmd.Parameters.Add(new SqlParameter("@FEE", SqlDbType.Bit) { Value = fee.CourtFee });
                                cmd.Parameters.Add(new SqlParameter("@AMT", SqlDbType.Decimal) { Value = fee.CourtFeeAmount });
                                cmd.Parameters.Add(new SqlParameter("@SFEE", SqlDbType.Bit) { Value = fee.ServiceFee });
                                cmd.Parameters.Add(new SqlParameter("@SAMT", SqlDbType.Decimal) { Value = fee.ServiceFeeAmount });
                                cmd.Parameters.Add(new SqlParameter("@COMBINE", SqlDbType.Bit) { Value = fee.CombineChecks });
                                cmd.Parameters.Add(new SqlParameter("@IS_SHERIFF", SqlDbType.Bit) { Value = fee.ServiceBySheriff });

                                await cmd.ExecuteNonQueryAsync();
                            }
                        }
                        tran.Commit();
                    }
                }
            }
        }
    }
}
