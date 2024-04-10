using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HB.Garnishments.Data.Requests.Results
{
    [DataContract(Name = "ResultCodeHandler", Namespace = "")]
    public class ResultCodeHandler
    {
        [DataMember(Name = "Fields")]
        public Codes.MergeCodeField[] Fields { get; private set; }
        [DataMember(Name = "Infos")]
        public List<Codes.MergeCodeInfo> Infos { get; private set; }
        [DataMember(Name = "Results")]
        public List<Result> Results { get; private set; }

        public async Task<Codes.MergeCodeInfo> AddMergeCodeInfoAsync(string description, Enums.ResultInfoType infoType, string refObject, string refParam)
        {
            var newInfoValue = await DataHandler.AddMergeCodeInfoAsync(description, infoType, refObject, refParam);
            this.Infos.Add(newInfoValue);
            return newInfoValue;
        }

        public static async Task<Codes.MergeCode> AddMergeCodeAsync(Result result, byte salesNo, byte debtor, string xcode)
        {
            var newCode = await DataHandler.AddMergeCodeAsync(result, salesNo, debtor, xcode);
            var salesCodes = result.MergeCodes.Find(s => s.SalesNo == salesNo);
            if (salesCodes == null)
            {
                salesCodes = new Codes.SalesMergeCodes(salesNo);
                result.MergeCodes.Add(salesCodes);
            }
            var debtorCodes = salesCodes.DebtorCodes.Find(d => d.Number == debtor);
            if (debtorCodes == null)
            {
                debtorCodes = new Codes.DebtorMergeCodes(debtor);
                salesCodes.DebtorCodes.Add(debtorCodes);
            }
            if (result[salesNo][debtor].Codes.Count(c => c.ID == newCode.ID) == 0)
            {
                debtorCodes.Codes.Add(newCode);
            }
            return newCode;
        }
        public static async Task<Codes.MergeCodeFieldValue> UpdateMergeCodeFieldValueAsync(Codes.MergeCode code, Codes.MergeCodeField field, Codes.MergeCodeInfo value)
        {
            var newFieldValue = await DataHandler.UpdateMergeCodeFieldValueAsync(code, field, value);
            var oldField = code.Values.FirstOrDefault(v => v.Field.ID == field.ID);
            if (oldField != null)
            {
                code.Values.Remove(oldField);
            }
            code.Values.Add(newFieldValue);
            return newFieldValue;
        }
        public static async Task DeleteMergeCode(Result result, byte salesNo, byte debtor, Codes.MergeCode code)
        {
            await DataHandler.DeleteMergeCodeAsync(code);
            result[salesNo][debtor].Codes.Remove(code);
        }
        public static async Task DeleteMergeCodeFieldValueAsync(Codes.MergeCode code, Codes.MergeCodeField field, Codes.MergeCodeInfo value)
        {
            await DataHandler.DeleteMergeCodeFieldValueAsync(code, field, value);
            var oldField = code.Values.FirstOrDefault(v => v.Field.ID == field.ID);
            if (oldField != null)
            {
                code.Values.Remove(oldField);
            }
        }
        
    }
}
