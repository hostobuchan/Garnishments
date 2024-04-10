using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Walz.Data
{
    public static class Dictionaries
    {
        private static Dictionary<Enums.CostModifier, string> _CostModifiers { get; set; }
        private static Dictionary<Enums.FileType, string> _FileNamingConventions { get; set; }
        private static Dictionary<Enums.LastStatus, string> _LastStatusStrings { get; set; }
        private static Dictionary<Enums.ReturnReceiptType, string> _ReturnReceiptStrings { get; set; }
        private static Dictionary<Enums.FormType, string> _FormTypeStrings { get; set; }
        private static Dictionary<Enums.FileVersion, string> _FileVersionStrings { get; set; }
        private static Dictionary<Enums.FileVersion, int> _FileVersionSegments { get; set; }

        public static Dictionary<Enums.CostModifier, string> CostModifiers
        {
            get
            {
                if (_CostModifiers == null) LoadCostModifiers();
                return _CostModifiers;
            }
        }
        public static Dictionary<Enums.FileType, string> FileNamingConventions
        {
            get
            {
                if (_FileNamingConventions == null) LoadFileNamingConventions();
                return _FileNamingConventions;
            }
        }
        public static Dictionary<Enums.LastStatus, string> LastStatusStrings
        {
            get
            {
                if (_LastStatusStrings == null) LoadLastStatusStrings();
                return _LastStatusStrings;
            }
        }
        public static Dictionary<Enums.ReturnReceiptType, string> ReturnReceiptStrings
        {
            get
            {
                if (_ReturnReceiptStrings == null) LoadReturnReceiptStrings();
                return _ReturnReceiptStrings;
            }
        }
        public static Dictionary<Enums.FormType, string> FormTypeStrings
        {
            get
            {
                if (_FormTypeStrings == null) LoadFormTypeStrings();
                return _FormTypeStrings;
            }
        }
        public static Dictionary<Enums.FileVersion, string> FileVersionStrings
        {
            get
            {
                if (_FileVersionStrings == null) LoadFileVersionStrings();
                return _FileVersionStrings;
            }
        }
        public static Dictionary<Enums.FileVersion, int> FileVersionSegments
        {
            get
            {
                if (_FileVersionSegments == null) LoadFileVersionSegments();
                return _FileVersionSegments;
            }
        }

        private static void LoadCostModifiers()
        {
            _CostModifiers = new Dictionary<Enums.CostModifier, string>()
            {
                { Enums.CostModifier.Unknown, "Unknown Modifier" },
                { Enums.CostModifier.AddFixedCost, "Add Fixed Cost" },
                { Enums.CostModifier.AddPerOzCost, "Add Cost Per OZ" },
                { Enums.CostModifier.SetMaxCost, "Set Max Cost" }
            };
        }
        private static void LoadFileNamingConventions()
        {
            _FileNamingConventions = new Dictionary<Enums.FileType, string>()
            {
                { Enums.FileType.BarcodeFile, @"WALZ_[\d]{4}_AL_([\d]{14}?)ID[\d]{4}_[a-zA-Z0-9]{1,3}_[\d]+[\.]zip" },
                { Enums.FileType.ImagesFile, @"Extract[\d]{8}[\.]zip" },
                { Enums.FileType.ReceiptFile, @"WALZ_[\d]{4}_[\d]{14}_Mailed_ID[\d]{4}_[\d]+[\.]zip" }
            };
        }
        private static void LoadLastStatusStrings()
        {
            _LastStatusStrings = new Dictionary<Enums.LastStatus, string>()
            {
                { Enums.LastStatus.Delivered, "Delivered" },
                { Enums.LastStatus.Mailed, "Mailed" },
                { Enums.LastStatus.ReturnedToSender, "Returned to Sender" }
            };
        }
        private static void LoadReturnReceiptStrings()
        {
            _ReturnReceiptStrings = new Dictionary<Enums.ReturnReceiptType, string>()
            {
                { Enums.ReturnReceiptType.None, "None" },
                { Enums.ReturnReceiptType.ReturnReceiptHardCopy, "RR" },
                { Enums.ReturnReceiptType.ReturnReceiptElectronic, "RRE" }
            };
        }
        private static void LoadFormTypeStrings()
        {
            _FormTypeStrings = new Dictionary<Enums.FormType, string>()
            {
                { Enums.FormType.None, "None" },
                { Enums.FormType.CertifiedEnvelope_10, "10SS" },
                { Enums.FormType.CertifiedEnvelope_6x9, "6x9SSMeter" },
                { Enums.FormType.CertifiedEnvelope_9x12, "9x12SSMeter" },
                { Enums.FormType.SinglePieceMailer, "35663" }
            };
        }
        private static void LoadFileVersionStrings()
        {
            _FileVersionStrings = new Dictionary<Enums.FileVersion, string>()
            {
                { Enums.FileVersion.FileVersion1, "1" },
                { Enums.FileVersion.FileVersion2, "2" },
                { Enums.FileVersion.FileVersion2r, "2r" },
                { Enums.FileVersion.FileVersion2m, "2m" },
                { Enums.FileVersion.FileVersion2rm, "2rm" },
                { Enums.FileVersion.FileVersion3, "3" },
                { Enums.FileVersion.FileVersion3r, "3r" },
                { Enums.FileVersion.FileVersion3m, "3m" },
                { Enums.FileVersion.FileVersion3rm, "3rm" }
            };
        }
        private static void LoadFileVersionSegments()
        {
            _FileVersionSegments = new Dictionary<Enums.FileVersion, int>()
            {
                { Enums.FileVersion.FileVersion1, 0 },
                { Enums.FileVersion.FileVersion2, 0 },
                { Enums.FileVersion.FileVersion2r, 0 },
                { Enums.FileVersion.FileVersion2m, 0 },
                { Enums.FileVersion.FileVersion2rm, 0 },
                { Enums.FileVersion.FileVersion3, 17 },
                { Enums.FileVersion.FileVersion3r, 23 },
                { Enums.FileVersion.FileVersion3m, 17 },
                { Enums.FileVersion.FileVersion3rm, 23 }
            };
        }
    }
}
