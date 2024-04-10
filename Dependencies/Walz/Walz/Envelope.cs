using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Walz.Data
{
    [DataContract(Name = "Envelope", Namespace = "")]
    public class Envelope
    {
        [DataMember(Name = "ID")]
        public byte ID { get; private set; }
        [DataMember(Name = "SIZE")]
        public Enums.FormType Size { get; private set; }
        [DataMember(Name = "DESCRIPTION")]
        public string Description { get; private set; }
        [DataMember(Name = "WEIGHT")]
        public decimal Weight { get; set; }
        [DataMember(Name = "START_COST")]
        public decimal StartingCost { get; set; }
        [DataMember(Name = "OZ_COST")]
        public decimal CostPerOz { get; set; }

        //Modifiers
        [DataMember(Name = "MODIFIERS")]
        public BindingList<Modifier> CostModifiers { get; set; }

        internal Envelope(Enums.FormType Size, string Description, decimal Weight, decimal StartingCost, decimal PerOzCost)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Walz))
            {
                using (SqlCommand cmd = new SqlCommand("EnvelopeSize_Add", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add("@SIZE", SqlDbType.TinyInt).Value = Convert.ToByte(Size);
                    cmd.Parameters.Add("@DESCRIPTION", SqlDbType.NVarChar, 50).Value = Description;
                    cmd.Parameters.Add("@WEIGHT", SqlDbType.Decimal, 4).Value = Weight;
                    cmd.Parameters.Add("@START_COST", SqlDbType.Decimal, 4).Value = StartingCost;
                    cmd.Parameters.Add("@OZ_COST", SqlDbType.Decimal, 4).Value = PerOzCost;
                    cmd.Parameters.Add("RETURN_VALUE", SqlDbType.SmallInt).Direction = ParameterDirection.ReturnValue;

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    this.ID = Convert.ToByte(cmd.Parameters["RETURN_VALUE"].Value);
                    this.Size = Size;
                    this.Description = Description;
                    this.Weight = Weight;
                    this.StartingCost = StartingCost;
                    this.CostPerOz = PerOzCost;
                }
            }
        }
        internal Envelope(IDataReader dr)
        {
            this.ID = Convert.ToByte(dr["ID"]);
            this.Size = (Enums.FormType)Enum.ToObject(typeof(Enums.FormType), Convert.ToByte(dr["SIZE"]));
            this.Description = dr["DESCRIPTION"].ToString();
            this.Weight = Convert.ToDecimal(dr["WEIGHT"]);
            this.StartingCost = Convert.ToDecimal(dr["START_COST"]);
            this.CostPerOz = Convert.ToDecimal(dr["OZ_COST"]);
        }

        internal protected void AddModifier(Modifier modifier)
        {
            lock (this)
            {
                if (CostModifiers == null) CostModifiers = new BindingList<Modifier>();
                if (!CostModifiers.Contains(modifier)) CostModifiers.Add(modifier);
            }
        }

        public void Update(Enums.FormType Size, string Description, decimal Weight, decimal StartingCost, decimal PerOzCost)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Walz))
            {
                using (SqlCommand cmd = new SqlCommand("EnvelopeSize_Update", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add("@EID", SqlDbType.TinyInt).Value = this.ID;
                    cmd.Parameters.Add("@SIZE", SqlDbType.TinyInt).Value = Convert.ToByte(Size);
                    cmd.Parameters.Add("@DESCRIPTION", SqlDbType.NVarChar, 50).Value = Description;
                    cmd.Parameters.Add("@WEIGHT", SqlDbType.Decimal, 4).Value = Weight;
                    cmd.Parameters.Add("@START_COST", SqlDbType.Decimal, 4).Value = StartingCost;
                    cmd.Parameters.Add("@OZ_COST", SqlDbType.Decimal, 4).Value = PerOzCost;

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    this.Description = Description;
                    this.Weight = Weight;
                    this.StartingCost = StartingCost;
                    this.CostPerOz = PerOzCost;
                }
            }
        }
        internal void Delete()
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Walz))
            {
                using (SqlCommand cmd = new SqlCommand("EnvelopeSize_Remove", conn) { CommandType = CommandType.StoredProcedure })
                {
                    cmd.Parameters.Add("@EID", SqlDbType.TinyInt).Value = this.ID;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public override string ToString()
        {
            return string.Format("[{0}] {1}",
                this.ID,
                this.Description);
        }

        
    }
}
