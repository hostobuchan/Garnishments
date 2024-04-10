using System;
using System.Collections.Generic;
using System.Linq;

namespace RecordTypes2.Experian.CollectionTriggers
{
    namespace Base
    {
        /// <summary>
        /// Base Record Type for Collection Triggers
        /// </summary>
        public class Record
        {
            private DataTypes.CTString _RecordID { get; set; }

            public string RecordID { get { return this._RecordID.Value; } }

            public Record(string Record)
            {
                this._RecordID = new DataTypes.CTString(3) { DataString = Record };
            }

            public override string ToString()
            {
                return _RecordID.ToString();
            }
        }
        /// <summary>
        /// Info for Header and Footer Records
        /// </summary>
        public class RecordInfo : Record
        {
            private DataTypes.CTString _CompanyID { get; set; }
            private DataTypes.CTString _PortfolioID { get; set; }
            private DataTypes.CTDate _NoticeDate { get; set; }

            /// <summary>
            /// Assigned Company ID
            /// <para>0373984</para>
            /// </summary>
            public string CompanyID { get { return this._CompanyID.Value; } set { this._CompanyID.Value = value; } }
            /// <summary>
            /// Portfolio ID
            /// <para>01</para>
            /// </summary>
            public string PortfolioID { get { return this._PortfolioID.Value; } set { this._PortfolioID.Value = value; } }
            /// <summary>
            /// Notice Date
            /// </summary>
            public DateTime? NoticeDate { get { return this._NoticeDate.Value; } set { this._NoticeDate.Value = value; } }

            public RecordInfo(string Record) : base(Record)
            {
                this._CompanyID = new DataTypes.CTString(7) { DataString = Record.Length > 3 ? Record.Substring(3) : "" };
                this._PortfolioID = new DataTypes.CTString(2) { DataString = Record.Length > 10 ? Record.Substring(10) : "" };
                this._NoticeDate = new DataTypes.CTDate() { DataString = Record.Length > 12 ? Record.Substring(12) : "" };
            }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}",
                    base.ToString(),
                    this._CompanyID,
                    this._PortfolioID,
                    this._NoticeDate);
            }
        }

        public class TriggerCount
        {
            private DataTypes.CTEnum<Enums.TriggerType, Enums.Values.TriggerType> _Trigger { get; set; }
            private DataTypes.CTNumber _Count { get; set; }

            public Enums.TriggerType Trigger { get { return this._Trigger.Value; } set { this._Trigger.Value = value; } }
            public int? Count { get { return this._Count.Value; } set { this._Count.Value = value; } }

            public TriggerCount(string Section)
            {
                this._Trigger = new DataTypes.CTEnum<Enums.TriggerType, Enums.Values.TriggerType>(5) { DataString = Section };
                this._Count = new DataTypes.CTNumber(10, 10) { DataString = Section.Length > 5 ? Section.Substring(5) : "" };
            }

            public override string ToString()
            {
                return string.Format("{0}{1}",
                    this._Trigger,
                    this._Count);
            }
        }
    }

    namespace DataTypes
    {
        /// <summary>
        /// Basic Character String
        /// </summary>
        public class CTString : RecordTypes.EDI.EDIDataTypes.DataType
        {
            public new string Value { get { return base.DataString.Trim(); } set { base.DataString = value; } }

            public CTString(int DataLength, bool LeftAlign = true) : base(DataLength, LeftAlign) { }
        }
        /// <summary>
        /// Date Stored as MMDDYYYY
        /// </summary>
        public class CTDate : RecordTypes.EDI.EDIDataTypes.DataType
        {
            public new DateTime? Value { get { try { return DateTime.ParseExact(base.Value, "MMddyyyy", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { base.DataString = value == null ? "" : value.Value.ToString("MMddyyyy"); } }

            public CTDate() : base(8) { }
        }
        /// <summary>
        /// Basic Number with no decimals
        /// </summary>
        public class CTNumber : RecordTypes.EDI.EDIDataTypes.DataType
        {
            private int? PadLength { get; set; }
            public new int? Value { get { try { return int.Parse(base.Value); } catch { return null; } } set { base.Value = value == null || value < 0 || value > 10 * (base.DataLength + 1) ? "" : value.ToString().PadLeft(this.PadLength.HasValue ? this.PadLength.Value : base.DataLength, '0'); } }

            public CTNumber(int DataLength, int? PadLength = null) : base(DataLength) { this.PadLength = PadLength; }
        }
        /// <summary>
        /// Number with displayed decimal
        /// </summary>
        public class CTDecimal : RecordTypes.EDI.EDIDataTypes.DataType
        {
            private int Precision;
            private bool IncludeDecimal;
            public new decimal? Value
            {
                get { try { if (this.IncludeDecimal) { return decimal.Parse(base.Value); } else { return decimal.Parse(base.Value) / (decimal)(Math.Pow(10, Precision)); } } catch { return null; } }
                set
                {
                    if (this.IncludeDecimal)
                        base.Value = value.HasValue ? "" : value.Value.ToString("F" + this.Precision.ToString());
                    else
                        base.Value = value.HasValue ? "" : (value.Value * (decimal)(Math.Pow(10, Precision))).ToString("F0");
                }
            }

            public CTDecimal(int Scale, int Precision, bool IncludeDecimal = false, bool AlignLeft = true)
                : base(Scale, AlignLeft)
            {
                this.Precision = Precision;
                this.IncludeDecimal = IncludeDecimal;
            }
        }
        /// <summary>
        /// Boolean data determined by the presence of char/string
        /// </summary>
        public class CTBool : RecordTypes.EDI.EDIDataTypes.DataType
        {
            private string Compare;
            private string Fail;
            public new bool Value { get { return base.Value == this.Compare; } set { base.DataString = value ? this.Compare : this.Fail; } }

            public CTBool(int DataLength, string CompareString, string FailString = "")
                : base(DataLength)
            {
                this.Compare = CompareString;
                this.Fail = FailString;
            }
        }
        /// <summary>
        /// String Data Determined by Dictionary Lookup
        /// </summary>
        public class CTDict : RecordTypes.EDI.EDIDataTypes.DataType
        {
            private Dictionary<string, string> Dictionary { get; set; }
            public new string Value
            {
                get
                {
                    try { return this.Dictionary[base.Value]; }
                    catch { return ""; }
                }
                set
                {
                    try { base.Value = this.Dictionary.Where(el => el.Value == value).Select(el => el.Key).First(); }
                    catch { base.Value = ""; }
                }
            }

            public CTDict(int DataLength, Dictionary<string, string> Dictionary, bool AlignLeft = true)
                : base(DataLength, AlignLeft)
            {
                this.Dictionary = Dictionary;
            }
        }
        /// <summary>
        /// String Data Determined by Dictionary Lookup
        /// </summary>
        /// <typeparam name="T">Enumeration of Possible Values</typeparam>
        public class CTDict<T> : RecordTypes.EDI.EDIDataTypes.DataType
        {
            private Dictionary<string, T> Dictionary { get; set; }
            public new T Value
            {
                get
                {
                    try { return this.Dictionary[base.Value]; }
                    catch { return (T)Enum.ToObject(typeof(T), 0); }
                }
                set
                {
                    try { base.Value = this.Dictionary.Where(el => Enum.Equals(el.Value, value)).Select(el => el.Key).First(); }
                    catch { base.Value = ""; }
                }
            }

            public CTDict(int DataLength, Dictionary<string, T> Dictionary, bool AlignLeft = true)
                : base(DataLength, AlignLeft)
            {
                this.Dictionary = Dictionary;
            }
        }
        /// <summary>
        /// Data is Associated with Specific List
        /// </summary>
        /// <typeparam name="T">Enumeration of List of Descriptions</typeparam>
        /// <typeparam name="Q">Enumeration of List of Values</typeparam>
        public class CTEnum<T, Q> : RecordTypes.EDI.EDIDataTypes.DataType
        {
            public new T Value { get { try { return (T)Enum.Parse(typeof(Q), base.Value); } catch { return (T)Enum.ToObject(typeof(T), 0); } } set { base.Value = Convert.ToInt32(value) == 0 ? "" : ((Q)Enum.Parse(typeof(T), value.ToString())).ToString(); } }

            public CTEnum(int DataLength, bool AlignLeft = true) : base(DataLength, AlignLeft) { }
        }
    }

    namespace Enums
    {
        /// <summary>
        /// Type of Public Record
        /// </summary>
        public enum PublicRecordType
        {
            Unknown = 0,
            Bankruptcy,
            CivilAction,
            Lien
        }
        /// <summary>
        /// Generation Suffix
        /// </summary>
        public enum GenerationCode
        {
            None = 0,
            Jr,
            Sr,
            II,
            III,
            IV
        }
        /// <summary>
        /// Type of Return Values
        /// </summary>
        public enum HitType
        {
            Unknown = 0,
            Trigger,
            FileOne
        }
        /// <summary>
        /// Phone Information Source
        /// </summary>
        public enum PhoneSource
        {
            Unknown = 0,
            PhoneBook,
            UpdateTapes,
            Inquiry,
            DirectoryAssistance
        }
        /// <summary>
        /// Phone Number Type
        /// </summary>
        public enum PhoneType
        {
            Unknown = 0,
            Business,
            Residence,
            Pager,
            Fax,
            Cellular,
            PayPhone,
            Institution
        }
        /// <summary>
        /// Consumer Statement Values
        /// </summary>
        public enum ConsumerStatement
        {
            None = 0,
            StatementPresent,
            SecurityAlertPresent
        }
        /// <summary>
        /// Enumeration of Trigger Business Types
        /// </summary>
        public enum KindOfBusiness
        {
            Unknown = 0,
            Auto_Rental,
            Auto_Leasing,
            Farm_Implement_Dealers,
            Truck_Dealers,
            Automobile_Dealers_New,
            Automotive_Parts,
            Auto_Repair_Body_Shops,
            Service_Stations,
            Tba_Stores_Tire_Dealers,
            Automobile_Dealers_Used,
            Automotive_Nonspecific,
            All_Banks_Nonspecific,
            Bank_Credit_Cards,
            Bank_Installment_Loans,
            Bank_Mortgage_Dept,
            Industrial_Bank,
            Coop_Bank,
            Savings_Bank,
            General_Clothing_Store,
            Specialty_Clothing_Store,
            Clothing_Store_Nonspecific,
            Complete_Department_Stores,
            Variety_Stores,
            Dept_And_Variety_Stores_Nonspecific,
            Business_Education,
            Colleges,
            Student_Loans,
            Technical_Education,
            Universities,
            Vocational_And_Trade_Schools,
            Education_Nonspecific,
            Auto_Financing_Companies,
            Mortgage_Brokers,
            Credit_Unions,
            Bail_Bonds,
            Sales_Financing_Companies,
            Investment_Firms,
            Savings_And_Loans_Mortgage,
            Mortgage_Companies,
            Personal_Loan_Companies,
            Mortgage_Reporters,
            Savings_And_Loan_Companies,
            Investment_Securities,
            Bulk_Purchase_Finance,
            Bulk_Purchase_Finance_General,
            Finance_Companies_Nonspecific,
            Dairies,
            Neighborhood_Grocers,
            Supermarkets,
            Accountants_And_Related_Svcs,
            Barber_And_Beauty_Shops,
            Equipment_Leasing,
            Dry_Cleaning_Laundry_Related,
            Engineering_All_Kinds,
            Florists,
            Photographers,
            Health_And_Fitness_Clubs,
            Detective_Service,
            Legal_And_Related_Services,
            Check_Cashing_Services,
            Restaurants_Concessions,
            Pest_Control,
            Country_Clubs,
            Employment_Screening,
            Personal_Service_Nonmedical_Nonspecific,
            Mail_Order_Houses_Nonspecific,
            Apartments,
            Office_Leasing,
            Mobile_Home_Dealers,
            Real_Estate_Sales_And_Rentals,
            Hotels,
            Motels,
            Mobile_Home_Park,
            Property_And_Property_Mgmt_Co,
            Real_Estate_Public_Accomnonspecific,
            Aircraft_Sales_And_Service,
            Boats_And_Marinas_Sales_Service,
            Motorcycles_And_Bicycles_Sales_Svc,
            Sporting_Goods_Nonspecific,
            Farm_Chem_And_Fertilizer_Stores,
            Feed_And_Feed_Stores,
            Nursery_And_Landscaping,
            Farm_And_Garden_Supplies_Svc_Nonspecific,
            Water_Utilities_Bottled_Water,
            Cable_Tv_Providers,
            Garbage_And_Rubbish_Disposal,
            Electric_Light_And_Power_Co,
            Fuel_Oil_Distributors,
            Gas_Co_Natural_And_Bottled,
            Coal_And_Wood_Suppliers,
            Long_Distance_Phone_Co,
            Online_Internet_Services,
            Cellular_And_Paging_Services,
            Waste_Recycling_Handlers,
            Satellite_Tv_Direct_Broadcast_Providers,
            Local_Telephone_Svc_Provider,
            Home_Security_Company,
            Wireless_Telephone_Service_Provider,
            Utilities_And_Fuel_Nonspecific,
            City_And_County,
            Federal_Government,
            Child_Support_Services,
            Law_Enforcement,
            State_Government,
            Court_Codes,
            Government_Nonspecific,
            Automotive_Supplies,
            Building_Supplies_Hardware,
            Clothing_And_Dry_Goods,
            Drugs_Chem_And_Related_Goods,
            Wholesale_Grocery_And_Related_Products,
            Home_Furnishings,
            Machinery_Equip_Supplies,
            Credit_Card_Processors,
            Wholesale_Nonspecific,
            Direct_Mail_List_Services,
            List_Processing_Vendors,
            Media,
            Advertising_Nonspecific,
            Collection_Dept_Acb_Credit_Bureau,
            Collection_Dept_Bank,
            Other_Collection_Agencies,
            Collection_Dept_Dept_Store,
            Collection_Dept_Loan_Co,
            Collections_Attorney,
            Collections_Nonspecific,
            Auto_Reseller,
            Credit_Report_Brokers,
            Credit_Bureaus,
            Direct_To_Consumer_Reseller,
            Employment_Reseller,
            Finance_Reseller,
            Insurance_Reseller,
            Leasing_And_Rental_Reseller,
            Manufacturing,
            Personal_Service_Reseller,
            Retail_Not_Elsewhere_Classified,
            Svcs_Not_Elsewhere_Classified,
            Tenant_Screeners_Reseller,
            Wholesale_Not_Elsewhere_Classified,
            Collection_Reseller,
            All_Others_Not_Elsewhere_Classified,
            Groceries_Nonspecific,
            Appliance_Sales_And_Service,
            Carpets_And_Floor_Coverings,
            Interior_Decorators_Designers,
            Home_Furnishing_Stores,
            Music_And_Record_Stores,
            Furniture_Rentals,
            Tv_And_Radio_Sales_And_Svc,
            Home_Furnishings_Nonspecific,
            General_Insurance,
            Life_Insurance,
            Insurance_Nonspecific,
            Jewelers,
            Computer_Sales_And_Service,
            Videotape_Rental_And_Sales,
            Jewelry_Cameras_And_Computers_Nonspecific,
            General_Contractors,
            Home_Improvement_Contr,
            Subcontractors,
            Contractors_Nonspecific,
            Air_Cond_Heat_Plumb_Elec_Sls,
            Fixture_And_Cabinet_Suppliers,
            Paint_Glass_Wallpaper_Store,
            Lumber_Bldg_Material_Hardware_Nonspecific,
            Animal_Hospitals,
            Dentists,
            Chiropractors,
            Doctors,
            Funeral_Homes,
            Medical_Group,
            Hospitals_And_Clinics,
            Cemeteries,
            Osteopaths,
            Pharmacies_And_Drugstores,
            Optometrists_And_Optical_Outlets,
            Veterinarians,
            Medical_And_Related_Health_Nonspecific,
            Airlines,
            Credit_Card_Dept_Store,
            Credit_Card_Finance_Co,
            Credit_Card_Savings_And_Loan,
            Credit_Card_Credit_Union,
            Natl_Credit_Card_Airlines_Nonspecific,
            Oil_Company_Credit_Cards,
            Oil_Companies_Nonspecific
        }
        /// <summary>
        /// Enumeration of Trigger Types
        /// </summary>
        public enum TriggerType
        {
            Unknown = 0,
            Paid_Collection,
            Paid_ChargeOff,
            Settled,
            Paid_Loan,
            Refinance,
            ChargeOff_Now_Paying,
            Home_Equity_Loan,
            Flexible_Spending_Or_Bankcard,
            New_Bank_Or_Credit_Card,
            New_Home_Equity_Loan,
            Employment_Update
        }
        /// <summary>
        /// Enumeration of Record Flag
        /// </summary>
        public enum AddDeleteFlag
        {
            Add,
            Delete,
            FullFile
        }
        /// <summary>
        /// Enumeration of Borrower Relation
        /// </summary>
        public enum Borrower
        {
            Unknown = 0,
            Primary,
            Secondary
        }
    }

    namespace Enums.Values
    {
        /// <summary>
        /// Type of Public Record (Values)
        /// </summary>
        public enum PublicRecordType
        {
            Unknown = 0,
            B,
            C,
            L
        }
        /// <summary>
        /// Type of Return Values (Values)
        /// </summary>
        public enum HitType
        {
            Unknown = 0,
            T,
            F
        }
        /// <summary>
        /// Phone Information Source (Values)
        /// </summary>
        public enum PhoneSource
        {
            Unknown = 0,
            P,
            U,
            I,
            D
        }
        /// <summary>
        /// Phone Number Type (Values)
        /// </summary>
        public enum PhoneType
        {
            Unknown = 0,
            B,
            R,
            P,
            F,
            C,
            T,
            I
        }
        /// <summary>
        /// Consumer Statement Values (Values)
        /// </summary>
        public enum ConsumerStatement
        {
            None = 0,
            S,
            A
        }
        /// <summary>
        /// Enumeration of Trigger Business Type Values
        /// </summary>
        public enum KindOfBusiness
        {
            Unknown = 0,
            AB,
            AC,
            AF,
            AL,
            AN,
            AP,
            AR,
            AS,
            AT,
            AU,
            AZ,
            BB,
            BC,
            BI,
            BM,
            BN,
            BO,
            BS,
            CG,
            CS,
            CZ,
            DC,
            DV,
            DZ,
            EB,
            EC,
            EL,
            ET,
            EU,
            EV,
            EZ,
            FA,
            FB,
            FC,
            FD,
            FF,
            FI,
            FL,
            FM,
            FP,
            FR,
            FS,
            FT,
            FU,
            FW,
            FZ,
            GD,
            GN,
            GS,
            PA,
            PB,
            PC,
            PD,
            PE,
            PF,
            PG,
            PH,
            PI,
            PL,
            PM,
            PN,
            PP,
            PR,
            PS,
            PZ,
            QZ,
            RA,
            RC,
            RD,
            RE,
            RH,
            RM,
            RP,
            RR,
            RZ,
            SA,
            SB,
            SM,
            SZ,
            TC,
            TF,
            TN,
            TZ,
            UA,
            UC,
            UD,
            UE,
            UF,
            UG,
            UH,
            UL,
            UO,
            UP,
            UR,
            US,
            UT,
            UV,
            UW,
            UZ,
            VC,
            VF,
            VK,
            VL,
            VS,
            VX,
            VZ,
            WA,
            WB,
            WC,
            WD,
            WG,
            WH,
            WM,
            WP,
            WZ,
            XD,
            XL,
            XM,
            XZ,
            YA,
            YB,
            YC,
            YD,
            YF,
            YL,
            YZ,
            ZA,
            ZB,
            ZC,
            ZD,
            ZE,
            ZF,
            ZI,
            ZL,
            ZM,
            ZP,
            ZR,
            ZS,
            ZT,
            ZW,
            ZY,
            ZZ,
            GZ,
            HA,
            HC,
            HD,
            HF,
            HM,
            HR,
            HT,
            HZ,
            IG,
            IL,
            IZ,
            JA,
            JP,
            JV,
            JZ,
            KG,
            KI,
            KS,
            KZ,
            LA,
            LF,
            LP,
            LZ,
            MA,
            MB,
            MC,
            MD,
            MF,
            MG,
            MH,
            MM,
            MO,
            MP,
            MS,
            MV,
            MZ,
            NA,
            ND,
            NF,
            NS,
            NU,
            NZ,
            OC,
            OZ
        }
        /// <summary>
        /// Enumeration of Trigger Type Values
        /// </summary>
        public enum TriggerType
        {
            Unknown = 0,
            PDCOL,
            PDCHG,
            SETTL,
            PDLON,
            REFIN,
            CHPAY,
            CAEQA,
            FLEXS,
            BKCDN,
            HMEQN,
            EMPLY
        }
        /// <summary>
        /// Enumeration of Record Flag Values
        /// </summary>
        public enum AddDeleteFlag
        {
            /// <summary>
            /// Add
            /// </summary>
            A,
            /// <summary>
            /// Delete
            /// </summary>
            D,
            /// <summary>
            /// Full File
            /// </summary>
            F
        }
        /// <summary>
        /// Enumeration of Borrower Relation Values
        /// </summary>
        public enum Borrower
        {
            Unknown = 0,
            P,
            S
        }
    }

    public static class Dictionaries
    {

        public static Dictionary<string, Enums.GenerationCode> GenerationCodeDictionary
        {
            get
            {
                return new Dictionary<string, Enums.GenerationCode>(){
                    {"J", Enums.GenerationCode.Jr},
                    {"S", Enums.GenerationCode.Sr},
                    {"2", Enums.GenerationCode.II},
                    {"3", Enums.GenerationCode.III},
                    {"4", Enums.GenerationCode.IV}
                };
            }
        }
    }
}
