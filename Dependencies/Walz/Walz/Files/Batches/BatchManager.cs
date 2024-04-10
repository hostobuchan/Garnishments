using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Walz.Data.Files.Batches
{
    public class BatchManager
    {
        protected DataHandler Manager { get; private set; }

        public BatchManager(DataHandler Manager)
        {
            this.Manager = Manager;
        }

        public BatchInfo GetBatch(int BatchID)
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.Walz))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Batches WHERE ID=@ID", conn))
                {
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = BatchID;

                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        return new BatchInfo(sdr);
                    }
                    else
                    {
                        throw new InvalidConstraintException("There is no Batch for ID# " + BatchID);
                    }
                }
            }
        }
        public BatchInfo CreateBatch(List<Recipient> Records)
        {
            return CreateBatch((short)Records.Count, Records.Sum(el=> EstimateRecipientCost(el)));
        }
        public BatchInfo CreateBatch(short Items, decimal Cost)
        {
            return new BatchInfo(Items, Cost);
        }

        public decimal EstimateRecipientWeight(Recipient recipient)
        {
            return Math.Ceiling(Math.Round(this.Manager.DetermineWeight(recipient.EnvelopeSize, recipient.LetterSize, recipient.Pages), 1));
        }
        public decimal EstimateRecipientCost(Recipient recipient)
        {
            return this.Manager.DetermineCost(recipient.EnvelopeSize, recipient.LetterSize, recipient.Pages, recipient.ReturnReceipt, recipient.RestrictedDelivery);
        }
        public decimal EstimateRecipientCost(Records.BarcodesRecord record)
        {
            return this.Manager.DetermineCost(record.FormType, record.Weight.Value, record.ReturnReceiptType, record.RestrictedDelivery);
        }
    }
}
