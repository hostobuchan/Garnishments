using System.Collections.Generic;

namespace RecordTypes.Ford
{
    public static class Dictionaries
    {
        private static Dictionary<string, Enums.FinancialTransactionCode> _FinancialTransactionCodes;
        private static Dictionary<string, string> _FinancialTransactionCodeDescriptions;
        public static Dictionary<string, Enums.FinancialTransactionCode> FinancialTransactionCodes { get { if (_FinancialTransactionCodes == null) CreateFinancialTransactionCodeDictionary(); return _FinancialTransactionCodes; } }
        public static Dictionary<string, string> FinancialTransactionCodeDescriptions { get { if (_FinancialTransactionCodeDescriptions == null) CreateFinancialTransactionCodeDescriptionsDictionary(); return _FinancialTransactionCodeDescriptions; } }

        private static void CreateFinancialTransactionCodeDictionary()
        {
            _FinancialTransactionCodes = new Dictionary<string, Enums.FinancialTransactionCode>()
            {
                { "51", Enums.FinancialTransactionCode.AA_Payment },
                { "50", Enums.FinancialTransactionCode.Customer_Payment },
                { "52", Enums.FinancialTransactionCode.Branch_Payment },
                { "53", Enums.FinancialTransactionCode.Journal_Entry },
                { "1A", Enums.FinancialTransactionCode.Court_Cost_Advanced },
                { "1R", Enums.FinancialTransactionCode.Court_Cost_Refunded },
                { "1F", Enums.FinancialTransactionCode.FeeCommission_Due },
                { "12", Enums.FinancialTransactionCode.Balance_Adjustment_1 },
                { "16", Enums.FinancialTransactionCode.Balance_Adjustment_2 },
                { "17", Enums.FinancialTransactionCode.Balance_Adjustment_3 }
            };
        }
        private static void CreateFinancialTransactionCodeDescriptionsDictionary()
        {
            _FinancialTransactionCodeDescriptions = new Dictionary<string, string>()
            {
                { "51", "A/A Payment" },
                { "50", "Customer Payment" },
                { "52", "Branch Payment" },
                { "53", "Journal Entry" },
                { "1A", "Court Cost Advanced" },
                { "1R", "Court Cost Refunded" },
                { "1F", "Fee/Commission Due" },
                { "12", "Balance Adjustment" },
                { "16", "Balance Adjustment" },
                { "17", "Balance Adjustment" }
            };
        }
    }
}
