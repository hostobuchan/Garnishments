using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Walz.Data
{
    [DataContract(Name = "DataHandler", Namespace = "")]
    public class DataHandler
    {
        [DataMember(Name = "Letters")]
        public List<Letter> Letters { get; private set; }
        [DataMember(Name = "Envelopes")]
        public List<Envelope> Envelopes { get; private set; }
        [DataMember(Name = "Costs")]
        public Dictionary<Enums.CostType, decimal> Costs { get; private set; }

        public DataHandler()
        {
            this.Letters = new List<Letter>();
            this.Envelopes = new List<Envelope>();
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Walz))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Costs", conn))
                {
                    if (conn.State != System.Data.ConnectionState.Open) conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        this.Costs.Add((Enums.CostType)Enum.ToObject(typeof(Enums.CostType), sdr["TYPE_ID"]), Convert.ToDecimal(sdr["AMOUNT"]));
                    }
                    sdr.Close();
                }
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM LetterSizes", conn))
                {
                    if (conn.State != System.Data.ConnectionState.Open) conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        this.Letters.Add(new Letter(sdr));
                    }
                    sdr.Close();
                }
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM EnvelopeSizes", conn))
                {
                    if (conn.State != System.Data.ConnectionState.Open) conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        this.Envelopes.Add(new Envelope(sdr));
                    }
                    sdr.Close();
                }
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM CostModifiers", conn))
                {
                    if (conn.State != System.Data.ConnectionState.Open) conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        Envelope envelope = this.Envelopes.Find(el => el.ID == Convert.ToByte(sdr["EID"]));
                        envelope.AddModifier(new Modifier(envelope, sdr));
                    }
                    sdr.Close();
                }
            }
        }

        public static async Task<DataHandler> GetDataHandlerAsync()
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Walz))
            {
                using (SqlCommand cmd = new SqlCommand("GetDataHandler_XML", conn) { CommandType = System.Data.CommandType.StoredProcedure })
                {
                    await conn.OpenAsync();

                    var xml = await cmd.ExecuteXmlReaderAsync();
                    DataContractSerializer serializer = new DataContractSerializer(typeof(DataHandler));
                    return serializer.ReadObject(xml) as DataHandler;
                }
            }
        }
        public async Task SaveSettings()
        {
            using (DataTable costTable = CreateCostTable())
            using (DataTable letterTable = CreateLetterTable())
            using (DataTable envelopeTable = CreateEnvelopeTable())
            using (DataTable modifierTable = CreateModifierTable())
            {
                using (SqlConnection conn = new SqlConnection(Settings.Connections.Walz))
                {
                    using (SqlCommand cmd =new SqlCommand("UpdateSettings", conn) { CommandType = CommandType.StoredProcedure })
                    {
                        cmd.Parameters.Add(new SqlParameter("@COSTS", SqlDbType.Structured) { TypeName = "CostTableType", Value = costTable });
                        cmd.Parameters.Add(new SqlParameter("@LETTERS", SqlDbType.Structured) { TypeName = "LetterSizeTableType", Value = letterTable });
                        cmd.Parameters.Add(new SqlParameter("@ENVELOPES", SqlDbType.Structured) { TypeName = "EnvelopeSizeTableType", Value = envelopeTable });
                        cmd.Parameters.Add(new SqlParameter("@MODIFIERS", SqlDbType.Structured) { TypeName = "CostModifierTableType", Value = modifierTable });

                        await conn.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
        }

        public decimal DetermineWeight(Envelope envelope, Letter letter, int pages)
        {
            return envelope.Weight + (letter.Weight * pages);
        }
        public decimal DetermineCost(Enums.FormType envelope, decimal weight, Enums.ReturnReceiptType ReturnReceipt, bool RestrictedDelivery)
        {
            return DetermineCost(this.Envelopes.FirstOrDefault(el => el.Size == envelope), weight, ReturnReceipt, RestrictedDelivery);
        }
        public decimal DetermineCost(Envelope envelope, Letter letter, int pages, Enums.ReturnReceiptType ReturnReceipt, bool RestrictedDelivery)
        {
            return DetermineCost(envelope, DetermineWeight(envelope, letter, pages), ReturnReceipt, RestrictedDelivery);
        }
        public decimal DetermineCost(Envelope envelope, decimal weight, Enums.ReturnReceiptType ReturnReceipt, bool RestrictedDelivery)
        {
            decimal baseCost = envelope.StartingCost + (envelope.CostPerOz * ((int)Math.Ceiling(Math.Round(weight, 1))-1));
            foreach (Modifier modifier in envelope.CostModifiers.OrderBy(el => el))
            {
                if (modifier.Weight > weight) break;
                switch (modifier.CostModifier)
                {
                    case Enums.CostModifier.AddFixedCost:
                        baseCost += modifier.Amount;
                        break;
                    case Enums.CostModifier.AddPerOzCost:
                        baseCost += ((int)weight - 1) * modifier.Amount;
                        break;
                    case Enums.CostModifier.SetMaxCost:
                        return modifier.Amount;
                    default:
                        break;
                }
            }
            baseCost += Costs[Enums.CostType.CertifiedMail]; // Certified Mail Cost
            baseCost += ReturnReceipt != Enums.ReturnReceiptType.None ? Costs[Enums.CostType.ElectronicReturnReceipt] : 0; // Return Receipt Cost
            baseCost += RestrictedDelivery ? Costs[Enums.CostType.RestrictedDelivery] : 0; // Restricted Delivery Cost

            return baseCost;
        }




        private DataTable CreateCostTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TYPE_ID", typeof(byte));
            dt.Columns.Add("AMOUNT", typeof(decimal));
            foreach (var cost in this.Costs.Where(c => c.Key != Enums.CostType.Unknown))
            {
                DataRow dr = dt.NewRow();
                dr["TYPE_ID"] = cost.Key;
                dr["AMOUNT"] = cost.Value;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        private DataTable CreateLetterTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(short));
            dt.Columns.Add("DESCRIPTION", typeof(string));
            dt.Columns.Add("WEIGHT", typeof(decimal));
            foreach (var letter in this.Letters)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = letter.ID;
                dr["DESCRIPTION"] = letter.Description;
                dr["WEIGHT"] = letter.Weight;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        private DataTable CreateEnvelopeTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(byte));
            dt.Columns.Add("SIZE", typeof(byte));
            dt.Columns.Add("DESCRIPTION", typeof(string));
            dt.Columns.Add("WEIGHT", typeof(decimal));
            dt.Columns.Add("START_COST", typeof(decimal));
            dt.Columns.Add("OZ_COST", typeof(decimal));
            foreach (var envelope in this.Envelopes.Where(e => e.Size != Enums.FormType.Unknown))
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = envelope.ID;
                dr["SIZE"] = envelope.Size;
                dr["DESCRIPTION"] = envelope.Description;
                dr["WEIGHT"] = envelope.Weight;
                dr["START_COST"] = envelope.StartingCost;
                dr["OZ_COST"] = envelope.CostPerOz;
                dt.Rows.Add(dr);
            }
            return dt;
        }
        private DataTable CreateModifierTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("EID", typeof(byte));
            dt.Columns.Add("WEIGHT", typeof(decimal));
            dt.Columns.Add("MODIFIER", typeof(byte));
            dt.Columns.Add("AMOUNT", typeof(decimal));
            foreach (var modifier in this.Envelopes.SelectMany(env => env.CostModifiers).Where(m => m.Envelope != null && m.CostModifier != Enums.CostModifier.Unknown))
            {
                DataRow dr = dt.NewRow();
                dr["EID"] = modifier.Envelope.ID;
                dr["WEIGHT"] = modifier.Weight;
                dr["MODIFIER"] = modifier.CostModifier;
                dr["AMOUNT"] = modifier.Amount;
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}
