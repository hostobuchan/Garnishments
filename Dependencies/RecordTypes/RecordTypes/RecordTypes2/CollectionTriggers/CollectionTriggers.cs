using RecordTypes;
using RecordTypes2.Experian.CollectionTriggers.Base;
using RecordTypes2.Experian.CollectionTriggers.DataTypes;
using RecordTypes2.Experian.CollectionTriggers.Enums;
using System;
using System.Text;

namespace RecordTypes2.Experian.CollectionTriggers
{
    public class Header : RecordInfo
    {
        #region Private Properties
        private CTString _Filler { get; set; }
        private CTString _Name { get; set; }
        private CTString _Customer { get; set; }
        private CTString _Version { get; set; }
        private CTString _Filler2 { get; set; }
        private CTString _Filler3 { get; set; }
        private CTString _CampaignID { get; set; }
        private CTString _Filler4 { get; set; }
        #endregion

        #region Public Properties
        public string Filler { get { return this._Filler.Value; } set { this._Filler.Value = value; } }
        /// <summary>
        /// Name of the File
        /// <para>Constant of ‘EXPERIAN COLLECTION TRIGGER NOTICES’</para>
        /// </summary>
        public string FileName { get { return this._Name.Value; } set { this._Name.Value = value; } }
        /// <summary>
        /// Customer Name
        /// <para>HOSTO</para>
        /// </summary>
        public string CustomerName { get { return this._Customer.Value; } set { this._Customer.Value = value; } }
        /// <summary>
        /// Version Number
        /// <para>Notice Version from Campaign Options Table</para>
        /// </summary>
        public string Version { get { return this._Version.Value; } set { this._Version.Value = value; } }
        public string Filler2 { get { return this._Filler2.Value; } set { this._Filler2.Value = value; } }
        public string Filler3 { get { return this._Filler3.Value; } set { this._Filler3.Value = value; } }
        /// <summary>
        /// Campaign ID
        /// <para>Identifies the set of options selected by the customer.</para>
        /// </summary>
        public string CampaignID { get { return this._CampaignID.Value; } set { this._CampaignID.Value = value; } }
        public string Filler4 { get { return this._Filler4.Value; } set { this._Filler4.Value = value; } }
        #endregion

        public Header(string Record) : base(Record)
        {
            this._Filler = new CTString(6) { DataString = Record.Length > 20 ? Record.Substring(20) : "" };
            this._Name = new CTString(40) { DataString = Record.Length > 26 ? Record.Substring(26) : "" };
            this._Customer = new CTString(40) { DataString = Record.Length > 66 ? Record.Substring(66) : "" };
            this._Version = new CTString(3) { DataString = Record.Length > 106 ? Record.Substring(106) : "" };
            this._Filler2 = new CTString(8) { DataString = Record.Length > 109 ? Record.Substring(109) : "" };
            this._Filler3 = new CTString(6) { DataString = Record.Length > 117 ? Record.Substring(117) : "" };
            this._CampaignID = new CTString(10) { DataString = Record.Length > 123 ? Record.Substring(123) : "" };
            this._Filler4 = new CTString(2435) { DataString = Record.Length > 133 ? Record.Substring(133) : "" };
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}",
                base.ToString(),
                this._Filler,
                this._Name,
                this._Customer,
                this._Version,
                this._Filler2,
                this._Filler3,
                this._CampaignID,
                this._Filler4);
        }
    }

    public class TriggerTotals : RecordInfo
    {
        #region Private Properties
        private CTString _Filler { get; set; }
        private CTString _Filler2 { get; set; }
        private CTString _Filler3 { get; set; }
        #endregion

        #region Public Properties
        public string Filler { get { return this._Filler.Value; } set { this._Filler.Value = value; } }
        public string Filler2 { get { return this._Filler2.Value; } set { this._Filler2.Value = value; } }
        public TriggerCount[] TriggerCounts { get; private set; }
        public string Filler3 { get { return this._Filler3.Value; } set { this._Filler3.Value = value; } }
        #endregion

        public TriggerTotals(string Record) : base(Record)
        {
            this._Filler = new CTString(6) { DataString = Record.Length > 20 ? Record.Substring(20) : "" };
            this._Filler2 = new CTString(26) { DataString = Record.Length > 26 ? Record.Substring(26) : "" };
            this.TriggerCounts = new TriggerCount[24];
            for (int i = 0; i < 24; i++)
            {
                if (Record.Length > (52 + (i * 15)))
                    this.TriggerCounts[i] = new TriggerCount(Record.Substring(52 + (i * 15)));
                else
                    this.TriggerCounts[i] = new TriggerCount("");
            }
            this._Filler3 = new CTString(2156) { DataString = Record.Length > 412 ? Record.Substring(412) : "" };
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(
                string.Format("{0}{1}{2}",
                    base.ToString(),
                    this._Filler,
                    this._Filler2));
            for (int i = 0; i < 24; i++)
            {
                sb.Append(this.TriggerCounts[i]);
            }
            sb.Append(this._Filler3);
            return sb.ToString();
        }
    }

    public class Footer : RecordInfo
    {
        #region Private Properties
        private CTString _Filler { get; set; }
        private CTString _Filler2 { get; set; }
        private CTNumber _Total { get; set; }
        private CTNumber _StatementCount { get; set; }
        private CTString _CampaignID { get; set; }
        private CTString _Filler3 { get; set; }
        #endregion

        #region Public Properties
        public string Filler { get { return this._Filler.Value; } set { this._Filler.Value = value; } }
        public string Filler2 { get { return this._Filler2.Value; } set { this._Filler2.Value = value; } }
        /// <summary>
        /// Total Number of Triggers
        /// </summary>
        public int? Total { get { return this._Total.Value; } set { this._Total.Value = value; } }
        /// <summary>
        /// Count of the number of unique PINs which have the Consumer Statement Indicator = S or A
        /// </summary>
        public int? StatementCount { get { return this._StatementCount.Value; } set { this._StatementCount.Value = value; } }
        /// <summary>
        /// Campaign ID
        /// </summary>
        public string CampaignID { get { return this._CampaignID.Value; } set { this._CampaignID.Value = value; } }
        public string Filler3 { get { return this._Filler3.Value; } set { this._Filler3.Value = value; } }
        #endregion

        public Footer(string Record) : base(Record)
        {
            this._Filler = new CTString(6) { DataString = Record.Length > 20 ? Record.Substring(20) : "" };
            this._Filler2 = new CTString(14) { DataString = Record.Length > 26 ? Record.Substring(26) : "" };
            this._Total = new CTNumber(10, 10) { DataString = Record.Length > 40 ? Record.Substring(40) : "" };
            this._StatementCount = new CTNumber(10, 10) { DataString = Record.Length > 50 ? Record.Substring(50) : "" };
            this._CampaignID = new CTString(10) { DataString = Record.Length > 60 ? Record.Substring(60) : "" };
            this._Filler3 = new CTString(2498) { DataString = Record.Length > 70 ? Record.Substring(70) : "" };
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}",
                base.ToString(),
                this._Filler,
                this._Filler2,
                this._Total,
                this._StatementCount,
                this._CampaignID,
                this._Filler3);
        }
    }

    public class Trigger : Record
    {
        #region Private Properties
        private CTString _Filler { get; set; }
        #endregion

        #region Public Properties
        public Segments.TriggerInfo Info { get; private set; }
        public Segments.SupplementalInfo SupplementalInfo { get; private set; }
        public Segments.SuppliedNameAddress SuppliedNameAddress { get; private set; }
        public Segments.FileOneName ReturnedName1 { get; private set; }
        public Segments.FileOneName ReturnedName2 { get; private set; }
        public Segments.FileOneName ReturnedName3 { get; private set; }
        public Segments.FileOneAddress ReturnedAddress1 { get; private set; }
        public Segments.FileOneAddress ReturnedAddress2 { get; private set; }
        public Segments.FileOneAddress ReturnedAddress3 { get; private set; }
        public Segments.SuppliedInfo SuppliedInfo { get; private set; }
        public Segments.SubscriberNameAddress SubscriberNameAddress { get; private set; }
        public Segments.FileOneEmployment Employer1 { get; private set; }
        public Segments.FileOneEmployment Employer2 { get; private set; }
        public Segments.FileOneEmployment Employer3 { get; private set; }
        public Segments.Telephone Telephone1 { get; private set; }
        public Segments.Telephone Telephone2 { get; private set; }
        public Segments.Telephone Telephone3 { get; private set; }
        public string Filler { get { return this._Filler.Value; } set { this._Filler.Value = value; } }
        #endregion

        public Trigger(string Record) : base(Record)
        {
            this.Info = new Segments.TriggerInfo(Record.Length > 3 ? Record.Substring(3) : "");
            this.SupplementalInfo = new Segments.SupplementalInfo(Record.Length > 168 ? Record.Substring(168) : "");
            this.SuppliedNameAddress = new Segments.SuppliedNameAddress(Record.Length > 333 ? Record.Substring(333) : "");
            this.ReturnedName1 = new Segments.FileOneName(Record.Length > 563 ? Record.Substring(563) : "");
            this.ReturnedName2 = new Segments.FileOneName(Record.Length > 718 ? Record.Substring(718) : "");
            this.ReturnedName3 = new Segments.FileOneName(Record.Length > 873 ? Record.Substring(873) : "");
            this.ReturnedAddress1 = new Segments.FileOneAddress(Record.Length > 1028 ? Record.Substring(1028) : "");
            this.ReturnedAddress2 = new Segments.FileOneAddress(Record.Length > 1228 ? Record.Substring(1228) : "");
            this.ReturnedAddress3 = new Segments.FileOneAddress(Record.Length > 1428 ? Record.Substring(1428) : "");
            this.SuppliedInfo = new Segments.SuppliedInfo(Record.Length > 1628 ? Record.Substring(1628) : "");
            this.SubscriberNameAddress = new Segments.SubscriberNameAddress(Record.Length > 1728 ? Record.Substring(1728) : "");
            this.Employer1 = new Segments.FileOneEmployment(Record.Length > 1878 ? Record.Substring(1878) : "");
            this.Employer2 = new Segments.FileOneEmployment(Record.Length > 2043 ? Record.Substring(2043) : "");
            this.Employer3 = new Segments.FileOneEmployment(Record.Length > 2208 ? Record.Substring(2208) : "");
            this.Telephone1 = new Segments.Telephone(Record.Length > 2373 ? Record.Substring(2373) : "");
            this.Telephone2 = new Segments.Telephone(Record.Length > 2413 ? Record.Substring(2413) : "");
            this.Telephone3 = new Segments.Telephone(Record.Length > 2453 ? Record.Substring(2453) : "");
            this._Filler = new CTString(75) { DataString = Record.Length > 2493 ? Record.Substring(2493) : "" };
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}",
                base.ToString(),
                Info,
                SupplementalInfo,
                SuppliedNameAddress,
                ReturnedName1,
                ReturnedName2,
                ReturnedName3,
                ReturnedAddress1,
                ReturnedAddress2,
                ReturnedAddress3,
                SuppliedInfo,
                SubscriberNameAddress,
                Employer1,
                Employer2,
                Employer3,
                Telephone1,
                Telephone2,
                Telephone3,
                _Filler);
        }
    }

    public class Upload
    {
        #region Private Properties
        private CTString _Filler { get; set; }
        private CTString _Surname { get; set; }
        private CTString _FirstName { get; set; }
        private CTString _MiddleName { get; set; }
        private CTString _SecondSurname { get; set; }
        private CTDict<Enums.GenerationCode> _GenerationCode { get; set; }
        private CTString _Spouse_FirstInitial { get; set; }
        private CTString _HouseNumber { get; set; }
        private CTString _Street_Directional { get; set; }
        private CTString _Street_Name_Apartment { get; set; }
        private CTString _Address_Line2 { get; set; }
        private CTString _City { get; set; }
        private CTString _State { get; set; }
        private CTString _Zip { get; set; }
        private CTString _SSN { get; set; }
        private CTEnum<Enums.AddDeleteFlag, Enums.Values.AddDeleteFlag> _AddDeleteFlag { get; set; }
        private CTEnum<Enums.Borrower, Enums.Values.Borrower> _Borrower { get; set; }
        private CTString _AccountNumber { get; set; }
        private CTString _Subcode { get; set; }
        private CTString _CompanyID { get; set; }
        private CTString _PortfolioID { get; set; }
        private CTNumber _CollectionGroupID { get; set; }
        private CTDecimal _DebtAmount { get; set; }
        private CTDate _ExpirationDate { get; set; }
        private CTString _Filler2 { get; set; }
        private CTString _CustomerTextData { get; set; }
        #endregion

        #region Public Properties
        public string Filler { get { return this._Filler.Value; } set { this._Filler.Value = value; } }
        public string Surname { get { return this._Surname.Value; } set { this._Surname.Value = value; } }
        public string FirstName { get { return this._FirstName.Value; } set { this._FirstName.Value = value; } }
        public string MiddleName { get { return this._MiddleName.Value; } set { this._MiddleName.Value = value; } }
        public string SecondSurname { get { return this._SecondSurname.Value; } set { this._SecondSurname.Value = value; } }
        public Enums.GenerationCode GenerationCode { get { return this._GenerationCode.Value; } set { this._GenerationCode.Value = value; } }
        public string Spouse_FirstInitial { get { return this._Spouse_FirstInitial.Value; } set { this._Spouse_FirstInitial.Value = value; } }
        public string HouseNumber { get { return this._HouseNumber.Value; } set { this._HouseNumber.Value = value; } }
        public string Street_Directional { get { return this._Street_Directional.Value; } set { this._Street_Directional.Value = value; } }
        public string Street_Name_Apartment { get { return this._Street_Name_Apartment.Value; } set { this._Street_Name_Apartment.Value = value; } }
        public string Address_Line2 { get { return this._Address_Line2.Value; } set { this._Address_Line2.Value = value; } }
        public string City { get { return this._City.Value; } set { this._City.Value = value; } }
        public string State { get { return this._State.Value; } set { this._State.Value = value; } }
        public string Zip { get { return this._Zip.Value; } set { this._Zip.Value = value.NumbersOnly(); } }
        public string SSN { get { return this._SSN.Value; } set { this._SSN.Value = value.NumbersOnly(); } }
        public Enums.AddDeleteFlag AddDeleteFlag { get { return this._AddDeleteFlag.Value; } set { this._AddDeleteFlag.Value = value; } }
        public Enums.Borrower Borrower { get { return this._Borrower.Value; } set { this._Borrower.Value = value; } }
        public string AccountNumber { get { return this._AccountNumber.Value; } set { this._AccountNumber.Value = value; } }
        public string Subcode { get { return this._Subcode.Value; } set { this._Subcode.Value = value; } }
        public string CompanyID { get { return this._CompanyID.Value; } set { this._CompanyID.Value = value; } }
        public string PortfolioID { get { return this._PortfolioID.Value; } set { this._PortfolioID.Value = value; } }
        public int? CollectionGroupID { get { return this._CollectionGroupID.Value; } set { this._CollectionGroupID.Value = value; } }
        public decimal? DebtAmount { get { return this._DebtAmount.Value; } set { this._DebtAmount.Value = value; } }
        public Elements.Telephone Phone1 { get; set; }
        public Elements.Telephone Phone2 { get; set; }
        public Elements.Telephone Phone3 { get; set; }
        public Elements.Telephone Phone4 { get; set; }
        public Elements.Telephone Phone5 { get; set; }
        public Elements.Telephone Phone6 { get; set; }
        public Elements.Telephone Phone7 { get; set; }
        public Elements.Telephone Phone8 { get; set; }
        public Elements.Telephone Phone9 { get; set; }
        public Elements.Telephone Phone10 { get; set; }
        public string Filler2 { get { return this._Filler2.Value; } set { this._Filler2.Value = value; } }
        public string CustomerTextData { get { return this._CustomerTextData.Value; } set { this._CustomerTextData.Value = value; } }
        #endregion

        public Upload()
        {
            this._Filler = new CTString(7);
            this._Surname = new CTString(32);
            this._FirstName = new CTString(32);
            this._MiddleName = new CTString(32);
            this._SecondSurname = new CTString(32);
            this._GenerationCode = new CTDict<GenerationCode>(1, Dictionaries.GenerationCodeDictionary);
            this._Spouse_FirstInitial = new CTString(1);
            this._HouseNumber = new CTString(7);
            this._Street_Directional = new CTString(3);
            this._Street_Name_Apartment = new CTString(28);
            this._Address_Line2 = new CTString(28);
            this._City = new CTString(15);
            this._State = new CTString(2);
            this._Zip = new CTString(9);
            this._SSN = new CTString(9);
            this._AddDeleteFlag = new CTEnum<AddDeleteFlag, Enums.Values.AddDeleteFlag>(1);
            this._Borrower = new CTEnum<Borrower, Enums.Values.Borrower>(1);
            this._AccountNumber = new CTString(40);
            this._Subcode = new CTString(7);
            this._CompanyID = new CTString(7);
            this._PortfolioID = new CTString(2);
            this._CollectionGroupID = new CTNumber(4, 4);
            this._DebtAmount = new CTDecimal(9, 2, false, false);
            this._ExpirationDate = new CTDate();
            this.Phone1 = new Elements.Telephone("");
            this.Phone1 = new Elements.Telephone("");
            this.Phone1 = new Elements.Telephone("");
            this.Phone1 = new Elements.Telephone("");
            this.Phone1 = new Elements.Telephone("");
            this.Phone1 = new Elements.Telephone("");
            this.Phone1 = new Elements.Telephone("");
            this.Phone1 = new Elements.Telephone("");
            this.Phone1 = new Elements.Telephone("");
            this.Phone1 = new Elements.Telephone("");
            this._Filler2 = new CTString(41);
            this._CustomerTextData = new CTString(100);
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}{33}{34}{35}",
                this._Filler,
                this._Surname,
                this._FirstName,
                this._MiddleName,
                this._SecondSurname,
                this._GenerationCode,
                this._Spouse_FirstInitial,
                this._HouseNumber,
                this._Street_Directional,
                this._Street_Name_Apartment,
                this._Address_Line2,
                this._City,
                this._State,
                this._Zip,
                this._SSN,
                this._AddDeleteFlag,
                this._Borrower,
                this._AccountNumber,
                this._Subcode,
                this._CompanyID,
                this._PortfolioID,
                this._CollectionGroupID,
                this._DebtAmount,
                this._ExpirationDate,
                this.Phone1,
                this.Phone2,
                this.Phone3,
                this.Phone4,
                this.Phone5,
                this.Phone6,
                this.Phone7,
                this.Phone8,
                this.Phone9,
                this.Phone10,
                this._Filler2,
                this._CustomerTextData
                );
        }
    }

    namespace Segments
    {
        /// <summary>
        /// Notice Trigger Info Segment
        /// <para>SEGTRG</para>
        /// </summary>
        public class TriggerInfo
        {
            #region Private Properties
            private CTString _UID { get; set; }
            private CTEnum<Enums.TriggerType, Enums.Values.TriggerType> _DisplayCode { get; set; }
            private CTEnum<Enums.KindOfBusiness, Enums.Values.KindOfBusiness> _KOB { get; set; }
            private CTDate _Date { get; set; }
            private CTDecimal _Amount { get; set; }
            private CTEnum<Enums.ConsumerStatement, Enums.Values.ConsumerStatement> _Statement { get; set; }
            private CTString _AccountNumber { get; set; }
            private CTString _CollectionGroupID { get; set; }
            private CTString _Filler { get; set; }
            private CTDate _NoticeDate { get; set; }
            private CTString _Filler2 { get; set; }
            #endregion

            #region Public Properties
            /// <summary>
            /// "~Blank filled if there isn’t an alert or statement ~If alert or consumer statement is present, this field will contain the same consumer identifier as the matching alert or consumer statement."
            /// </summary>
            public string UniqueID { get { return this._UID.Value; } set { this._UID.Value = value; } }
            /// <summary>
            /// Client Trigger Display Code 
            /// </summary>
            public Enums.TriggerType DisplayCode { get { return this._DisplayCode.Value; } set { this._DisplayCode.Value = value; } }
            /// <summary>
            /// Trigger Kind of Business 
            /// </summary>
            public Enums.KindOfBusiness KindOfBusiness { get { return this._KOB.Value; } set { this._KOB.Value = value; } }
            /// <summary>
            /// <para>• Balance Date for trade trigger</para>
            /// <para>• Date Filed for Public Record trigger</para>
            /// <para>• Inquiry Date for inquiry trigger</para>
            /// <para>• Update Date for Address, Employment or Phone trigger</para>
            /// </summary>
            public DateTime? Date { get { return this._Date.Value; } set { this._Date.Value = value; } }
            /// <summary>
            /// <para>• Balance Amount on Trade</para>
            /// <para>• Public Record Amount on Public Record</para>
            /// <para>• Blank for all other triggers</para>
            /// </summary>
            public decimal? Amount { get { return this._Amount.Value; } set { this._Amount.Value = value; } }
            /// <summary>
            /// <para>S - Consumer Statement present</para>
            /// <para>A - Consumer Security Alert present.</para>
            /// <para>Otherwise, field will be blank.</para>
            /// </summary>
            public Enums.ConsumerStatement StatementIndicator { get { return this._Statement.Value; } set { this._Statement.Value = value; } }
            /// <summary>
            /// Client’s account number for the consumer Left justified, blank filled to the right
            /// </summary>
            public string AccountNumber { get { return this._AccountNumber.Value; } set { this._AccountNumber.Value = value; } }
            /// <summary>
            /// Client’s group identifier for the consumer/account.
            /// </summary>
            public string CollectionGroupID { get { return this._CollectionGroupID.Value; } set { this._CollectionGroupID.Value = value; } }
            public string Filler { get { return this._Filler.Value; } set { this._Filler.Value = value; } }
            /// <summary>
            /// Notice Date
            /// <para>MMDDYYYY</para>
            /// </summary>
            public DateTime? NoticeDate { get { return this._NoticeDate.Value; } set { this._NoticeDate.Value = value; } }
            public string Filler2 { get { return this._Filler2.Value; } set { this._Filler2.Value = value; } }
            /// <summary>
            /// CLS FileNo
            /// <para>AccountNumber [Format: "FILENO-DEBTOR"]</para>
            /// </summary>
            public string FileNo
            {
                get
                {
                    string[] AN = this.AccountNumber.Split('-');
                    return AN[0];
                }
                set
                {
                    string[] AN = this.AccountNumber.Split('-');
                    if (AN.GetUpperBound(0) > 0)
                    {
                        this.AccountNumber = value.ToUpper() + "-" + AN[1];
                    }
                    else
                    {
                        this.AccountNumber = value.ToUpper() + "-1";
                    }
                }
            }
            /// <summary>
            /// CLS Debtor Number
            /// <para>AccountNumber [Format: "FILENO-DEBTOR"]</para>
            /// </summary>
            public byte Debtor
            {
                get
                {
                    string[] AN = this.AccountNumber.Split('-');
                    if (AN.GetUpperBound(0) > 0)
                    {
                        return Convert.ToByte(AN[1]);
                    }
                    else
                    {
                        return 1;
                    }
                }
                set
                {
                    string[] AN = this.AccountNumber.Split('-');
                    this.AccountNumber = AN[0] + "-" + value.ToString();
                }
            }
            #endregion

            public TriggerInfo(string Segment)
            {
                this._UID = new CTString(17) { DataString = Segment };
                this._DisplayCode = new CTEnum<TriggerType, Enums.Values.TriggerType>(5) { DataString = Segment.Length > 17 ? Segment.Substring(17) : "" };
                this._KOB = new CTEnum<KindOfBusiness, Enums.Values.KindOfBusiness>(2) { DataString = Segment.Length > 22 ? Segment.Substring(22) : "" };
                this._Date = new CTDate() { DataString = Segment.Length > 24 ? Segment.Substring(24) : "" };
                this._Amount = new CTDecimal(8, 0, false, false) { DataString = Segment.Length > 32 ? Segment.Substring(32) : "" };
                this._Statement = new CTEnum<ConsumerStatement, Enums.Values.ConsumerStatement>(1) { DataString = Segment.Length > 40 ? Segment.Substring(40) : "" };
                this._AccountNumber = new CTString(40) { DataString = Segment.Length > 41 ? Segment.Substring(41) : "" };
                this._CollectionGroupID = new CTString(4) { DataString = Segment.Length > 81 ? Segment.Substring(81) : "" };
                this._Filler = new CTString(62) { DataString = Segment.Length > 85 ? Segment.Substring(85) : "" };
                this._NoticeDate = new CTDate() { DataString = Segment.Length > 147 ? Segment.Substring(147) : "" };
                this._Filler2 = new CTString(10) { DataString = Segment.Length > 155 ? Segment.Substring(155) : "" };
            }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}",
                    this._UID,
                    this._DisplayCode,
                    this._KOB,
                    this._Date,
                    this._Amount,
                    this._Statement,
                    this._AccountNumber,
                    this._CollectionGroupID,
                    this._Filler,
                    this._NoticeDate,
                    this._Filler2);
            }
        }
        /// <summary>
        /// Notice Collection Supplemental Info
        /// <para>SEGCSI</para>
        /// </summary>
        public class SupplementalInfo
        {
            #region Private Properties
            private CTString _BankruptcyChapter { get; set; }
            private CTEnum<Enums.PublicRecordType, Enums.Values.PublicRecordType> _PublicRecordType { get; set; }
            private CTString _PublicRecordStatus { get; set; }
            private CTDate _DateOpened { get; set; }
            private CTDecimal _TradeCurrentBalance { get; set; }
            private CTDecimal _TradeCreditLimit { get; set; }
            private CTNumber _BankruptcyChapter7 { get; set; }
            private CTNumber _BankruptcyChapter11 { get; set; }
            private CTNumber _BankruptcyChapter12 { get; set; }
            private CTNumber _BankruptcyWithdrawn { get; set; }
            private CTNumber _AcctClosedConsumerDispute { get; set; }
            private CTNumber _AcctClosedConsumerDisputeResolved { get; set; }
            private CTNumber _BankruptcyChapter7_AcctClosed { get; set; }
            private CTNumber _BankruptcyChapter11_AcctClosed { get; set; }
            private CTNumber _BankruptcyChapter12_AcctClosed { get; set; }
            private CTNumber _FCRA { get; set; }
            private CTString _Filler { get; set; }
            private CTString _Filler2 { get; set; }
            #endregion

            #region Public Properties
            /// <summary>
            /// Chapter of Bankruptcy if the trigger is a Bankruptcy trigger. Else, blank filled.
            /// </summary>
            public string BankruptcyChapter { get { return this._BankruptcyChapter.Value; } set { this._BankruptcyChapter.Value = value; } }
            /// <summary>
            /// Public Record Type
            /// <para>B = Bankruptcy</para>
            /// <para>C = Civil Action</para>
            /// <para>L = Lien</para>
            /// <para>Blank if trigger is not a public record trigger.</para>
            /// </summary>
            public Enums.PublicRecordType PublicRecordType { get { return this._PublicRecordType.Value; } set { this._PublicRecordType.Value = value; } }
            /// <summary>
            /// Public Record Status for public record trigger. (Status codes are listed in Experian Technical Manual.) Blank if trigger is not a public record trigger.
            /// </summary>
            public string PublicRecordStatus { get { return this._PublicRecordStatus.Value; } set { this._PublicRecordStatus.Value = value; } }
            /// <summary>
            /// Date that trigger trade was opened. Blank if trigger is not a trade.
            /// </summary>
            public DateTime? DateOpened { get { return this._DateOpened.Value; } set { this._DateOpened.Value = value; } }
            /// <summary>
            /// Current balance of trigger trade. Blank if trigger is not a trade.
            /// </summary>
            public decimal? TradeCurrentBalance { get { return this._TradeCurrentBalance.Value; } set { this._TradeCurrentBalance.Value = value; } }
            /// <summary>
            /// Credit Limit of trigger trade Blank if trigger is not a trade.
            /// </summary>
            public decimal? TradeCreditLimit { get { return this._TradeCreditLimit.Value; } set { this._TradeCreditLimit.Value = value; } }
            /// <summary>
            /// 
            /// </summary>
            public int? BankruptcyChapter7 { get { return this._BankruptcyChapter7.Value; } set { this._BankruptcyChapter7.Value = value; } }
            /// <summary>
            /// 
            /// </summary>
            public int? BankruptcyChapter11 { get { return this._BankruptcyChapter11.Value; } set { this._BankruptcyChapter11.Value = value; } }
            /// <summary>
            /// 
            /// </summary>
            public int? BankruptcyChapter12 { get { return this._BankruptcyChapter12.Value; } set { this._BankruptcyChapter12.Value = value; } }
            /// <summary>
            /// 
            /// </summary>
            public int? BankruptcyWithdrawn { get { return this._BankruptcyWithdrawn.Value; } set { this._BankruptcyWithdrawn.Value = value; } }
            /// <summary>
            /// 
            /// </summary>
            public int? AcctClosedConsumerDispute { get { return this._AcctClosedConsumerDispute.Value; } set { this._AcctClosedConsumerDispute.Value = value; } }
            /// <summary>
            /// 
            /// </summary>
            public int? AcctClosedConsumerDisputeResolved { get { return this._AcctClosedConsumerDisputeResolved.Value; } set { this._AcctClosedConsumerDisputeResolved.Value = value; } }
            /// <summary>
            /// 
            /// </summary>
            public int? BankruptcyChapter7_AcctClosed { get { return this._BankruptcyChapter7_AcctClosed.Value; } set { this._BankruptcyChapter7_AcctClosed.Value = value; } }
            /// <summary>
            /// 
            /// </summary>
            public int? BankruptcyChapter11_AcctClosed { get { return this._BankruptcyChapter11_AcctClosed.Value; } set { this._BankruptcyChapter11_AcctClosed.Value = value; } }
            /// <summary>
            /// 
            /// </summary>
            public int? BankruptcyChapter12_AcctClosed { get { return this._BankruptcyChapter12_AcctClosed.Value; } set { this._BankruptcyChapter12_AcctClosed.Value = value; } }
            /// <summary>
            /// 
            /// </summary>
            public int? FCRA { get { return this._FCRA.Value; } set { this._FCRA.Value = value; } }
            /// <summary>
            /// 
            /// </summary>
            public string Filler { get { return this._Filler.Value; } set { this._Filler.Value = value; } }
            /// <summary>
            /// 
            /// </summary>
            public string Filler2 { get { return this._Filler2.Value; } set { this._Filler2.Value = value; } }
            #endregion

            public SupplementalInfo(string Segment)
            {
                this._BankruptcyChapter = new CTString(2) { DataString = Segment };
                this._PublicRecordType = new CTEnum<PublicRecordType, Enums.Values.PublicRecordType>(1) { DataString = Segment.Length > 2 ? Segment.Substring(2) : "" };
                this._PublicRecordStatus = new CTString(2) { DataString = Segment.Length > 3 ? Segment.Substring(3) : "" };
                this._DateOpened = new CTDate() { DataString = Segment.Length > 5 ? Segment.Substring(5) : "" };
                this._TradeCurrentBalance = new CTDecimal(10, 2) { DataString = Segment.Length > 13 ? Segment.Substring(13) : "" };
                this._TradeCreditLimit = new CTDecimal(10, 2) { DataString = Segment.Length > 23 ? Segment.Substring(23) : "" };
                this._BankruptcyChapter7 = new CTNumber(1) { DataString = Segment.Length > 33 ? Segment.Substring(33) : "" };
                this._BankruptcyChapter11 = new CTNumber(1) { DataString = Segment.Length > 34 ? Segment.Substring(34) : "" };
                this._BankruptcyChapter12 = new CTNumber(1) { DataString = Segment.Length > 35 ? Segment.Substring(35) : "" };
                this._BankruptcyWithdrawn = new CTNumber(1) { DataString = Segment.Length > 36 ? Segment.Substring(36) : "" };
                this._AcctClosedConsumerDispute = new CTNumber(1) { DataString = Segment.Length > 37 ? Segment.Substring(37) : "" };
                this._AcctClosedConsumerDisputeResolved = new CTNumber(1) { DataString = Segment.Length > 38 ? Segment.Substring(38) : "" };
                this._BankruptcyChapter7_AcctClosed = new CTNumber(1) { DataString = Segment.Length > 39 ? Segment.Substring(39) : "" };
                this._BankruptcyChapter11_AcctClosed = new CTNumber(1) { DataString = Segment.Length > 40 ? Segment.Substring(40) : "" };
                this._BankruptcyChapter12_AcctClosed = new CTNumber(1) { DataString = Segment.Length > 41 ? Segment.Substring(41) : "" };
                this._FCRA = new CTNumber(1) { DataString = Segment.Length > 42 ? Segment.Substring(42) : "" };
                this._Filler = new CTString(22) { DataString = Segment.Length > 43 ? Segment.Substring(43) : "" };
                this._Filler2 = new CTString(100) { DataString = Segment.Length > 65 ? Segment.Substring(65) : "" };
            }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}",
                    this._BankruptcyChapter,
                    this._PublicRecordType,
                    this._PublicRecordStatus,
                    this._DateOpened,
                    this._TradeCurrentBalance,
                    this._TradeCreditLimit,
                    this._BankruptcyChapter7,
                    this._BankruptcyChapter11,
                    this._BankruptcyChapter12,
                    this._BankruptcyWithdrawn,
                    this._AcctClosedConsumerDispute,
                    this._AcctClosedConsumerDisputeResolved,
                    this._BankruptcyChapter7_AcctClosed,
                    this._BankruptcyChapter11_AcctClosed,
                    this._BankruptcyChapter12_AcctClosed,
                    this._FCRA,
                    this._Filler,
                    this._Filler2
                    );
            }
        }
        /// <summary>
        /// Customer Supplied Full Name and Address
        /// <para>SEGCNA</para>
        /// </summary>
        public class SuppliedNameAddress
        {
            #region Private Properties
            private CTString _Surname { get; set; }
            private CTString _FirstName { get; set; }
            private CTString _MiddleName { get; set; }
            private CTString _Second_Surname { get; set; }
            private CTDict<Enums.GenerationCode> _GenerationCode { get; set; }
            private CTString _HouseNumber { get; set; }
            private CTString _Street_Directional { get; set; }
            private CTString _Street_Name { get; set; }
            private CTString _AddressLine2 { get; set; }
            private CTString _City { get; set; }
            private CTString _State { get; set; }
            private CTString _Zip { get; set; }
            private CTString _SSN { get; set; }
            #endregion

            #region Public Properties
            public string Surname { get { return this._Surname.Value; } set { this._Surname.Value = value; } }
            public string FirstName { get { return this._FirstName.Value; } set { this._FirstName.Value = value; } }
            public string MiddleName { get { return this._MiddleName.Value; } set { this._MiddleName.Value = value; } }
            public string Second_Surname { get { return this._Second_Surname.Value; } set { this._Second_Surname.Value = value; } }
            public Enums.GenerationCode GenerationCode { get { return this._GenerationCode.Value; } set { this._GenerationCode.Value = value; } }
            public string HouseNumber { get { return this._HouseNumber.Value; } set { this._HouseNumber.Value = value; } }
            public string Street_Directional { get { return this._Street_Directional.Value; } set { this._Street_Directional.Value = value; } }
            public string Street_Name { get { return this._Street_Name.Value; } set { this._Street_Name.Value = value; } }
            public string AddressLine2 { get { return this._AddressLine2.Value; } set { this._AddressLine2.Value = value; } }
            public string City { get { return this._City.Value; } set { this._City.Value = value; } }
            public string State { get { return this._State.Value; } set { this._State.Value = value; } }
            public string Zip { get { return this._Zip.Value; } set { this._Zip.Value = value.NumbersOnly(); } }
            public string SSN { get { return this._SSN.Value; } set { this._SSN.Value = value.NumbersOnly(); } }
            #endregion

            public SuppliedNameAddress(string Segment)
            {
                this._Surname = new CTString(32) { DataString = Segment };
                this._FirstName = new CTString(32) { DataString = Segment.Length > 32 ? Segment.Substring(32) : "" };
                this._MiddleName = new CTString(32) { DataString = Segment.Length > 64 ? Segment.Substring(64) : "" };
                this._Second_Surname = new CTString(32) { DataString = Segment.Length > 96 ? Segment.Substring(96) : "" };
                this._GenerationCode = new CTDict<Enums.GenerationCode>(1, Dictionaries.GenerationCodeDictionary) { DataString = Segment.Length > 128 ? Segment.Substring(128) : "" };
                this._HouseNumber = new CTString(7) { DataString = Segment.Length > 129 ? Segment.Substring(129) : "" };
                this._Street_Directional = new CTString(3) { DataString = Segment.Length > 136 ? Segment.Substring(136) : "" };
                this._Street_Name = new CTString(28) { DataString = Segment.Length > 139 ? Segment.Substring(139) : "" };
                this._AddressLine2 = new CTString(28) { DataString = Segment.Length > 167 ? Segment.Substring(167) : "" };
                this._City = new CTString(15) { DataString = Segment.Length > 195 ? Segment.Substring(195) : "" };
                this._State = new CTString(2) { DataString = Segment.Length > 210 ? Segment.Substring(210) : "" };
                this._Zip = new CTString(9) { DataString = Segment.Length > 212 ? Segment.Substring(212) : "" };
                this._SSN = new CTString(9) { DataString = Segment.Length > 221 ? Segment.Substring(221) : "" };
            }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}",
                    this._Surname,
                    this._FirstName,
                    this._MiddleName,
                    this._Second_Surname,
                    this._GenerationCode,
                    this._HouseNumber,
                    this._Street_Directional,
                    this._Street_Name,
                    this._AddressLine2,
                    this._City,
                    this._State,
                    this._Zip,
                    this._SSN
                    );
            }
        }
        /// <summary>
        /// File One Name Segment
        /// <para>SEGNME</para>
        /// </summary>
        public class FileOneName
        {
            #region Private Properties
            private CTString _FirstName { get; set; }
            private CTString _MiddleName { get; set; }
            private CTString _Surname { get; set; }
            private CTString _Second_Surname { get; set; }
            private CTDict<Enums.GenerationCode> _GenerationCode { get; set; }
            private CTString _SSN { get; set; }
            private CTDate _DateOfBirth { get; set; }
            private CTString _Filler { get; set; }
            #endregion

            #region Public Properties
            public string FirstName { get { return this._FirstName.Value; } set { this._FirstName.Value = value; } }
            public string MiddleName { get { return this._MiddleName.Value; } set { this._MiddleName.Value = value; } }
            public string Surname { get { return this._Surname.Value; } set { this._Surname.Value = value; } }
            public string Second_Surname { get { return this._Second_Surname.Value; } set { this._Second_Surname.Value = value; } }
            public Enums.GenerationCode GenerationCode { get { return this._GenerationCode.Value; } set { this._GenerationCode.Value = value; } }
            public string SSN { get { return this._SSN.Value; } set { this._SSN.Value = value.NumbersOnly(); } }
            public DateTime? DateOfBirth { get { return this._DateOfBirth.Value; } set { this._DateOfBirth.Value = value; } }
            public string Filler { get { return this._Filler.Value; } set { this._Filler.Value = value; } }
            #endregion

            public FileOneName(string Segment)
            {
                this._FirstName = new CTString(32) { DataString = Segment };
                this._MiddleName = new CTString(32) { DataString = Segment.Length > 32 ? Segment.Substring(32) : "" };
                this._Surname = new CTString(32) { DataString = Segment.Length > 64 ? Segment.Substring(64) : "" };
                this._Second_Surname = new CTString(32) { DataString = Segment.Length > 96 ? Segment.Substring(96) : "" };
                this._GenerationCode = new CTDict<Enums.GenerationCode>(1, Dictionaries.GenerationCodeDictionary) { DataString = Segment.Length > 128 ? Segment.Substring(128) : "" };
                this._SSN = new CTString(9) { DataString = Segment.Length > 129 ? Segment.Substring(129) : "" };
                this._DateOfBirth = new CTDate() { DataString = Segment.Length > 138 ? Segment.Substring(138) : "" };
                this._Filler = new CTString(9) { DataString = Segment.Length > 146 ? Segment.Substring(146) : "" };
            }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                    this._FirstName,
                    this._MiddleName,
                    this._Surname,
                    this._Second_Surname,
                    this._GenerationCode,
                    this._SSN,
                    this._DateOfBirth,
                    this._Filler
                    );
            }
        }
        /// <summary>
        /// File One Address Segment
        /// <para>SEGADR</para>
        /// </summary>
        public class FileOneAddress
        {
            #region Private Properties
            private CTEnum<Enums.HitType, Enums.Values.HitType> _SegmentIndicator { get; set; }
            private CTString _PrimaryStreetID { get; set; }
            private CTString _PreDirectional { get; set; }
            private CTString _StreetName { get; set; }
            private CTString _PostDirectional { get; set; }
            private CTString _StreetSuffix { get; set; }
            private CTString _UnitType { get; set; }
            private CTString _UnitID { get; set; }
            private CTString _City { get; set; }
            private CTString _State { get; set; }
            private CTString _Zip { get; set; }
            private CTString _NonStandardAddress { get; set; }
            private CTString _Filler { get; set; }
            private CTString _CensusGeoCode { get; set; }
            private CTString _GeoStateCode { get; set; }
            private CTString _GeoCountyCode { get; set; }
            private CTDate _FirstReportedDate { get; set; }
            private CTDate _LastUpdatedDate { get; set; }
            private CTString _OriginationCode { get; set; }
            private CTNumber _NumberTimesReported { get; set; }
            private CTString _Filler2 { get; set; }
            #endregion

            #region Public Properties
            /// <summary>
            /// Address Segment Indicator
            /// <para>T = Trigger</para>
            /// <para>F = File One</para>
            /// </summary>
            public Enums.HitType SegmentIndicator { get { return this._SegmentIndicator.Value; } set { this._SegmentIndicator.Value = value; } }
            /// <summary>
            /// Data that preceded the street name word.  Most commonly includes house number.  House Number may be hyphenated.  PO boxes, rural routes, and any other type of accommodation address is spelled out.
            /// </summary>
            public string PrimaryStreetID { get { return this._PrimaryStreetID.Value; } set { this._PrimaryStreetID.Value = value; } }
            /// <summary>
            /// Pre Street Direction
            /// <para>N, S, E, W, NE, NW, SE, SW</para>
            /// </summary>
            public string PreDirectional { get { return this._PreDirectional.Value; } set { this._PreDirectional.Value = value; } }
            /// <summary>
            /// Street Name
            /// </summary>
            public string StreetName { get { return this._StreetName.Value; } set { this._StreetName.Value = value; } }
            /// <summary>
            /// Post Street Direction
            /// <para>N, S, E, W, NE, NW, SE, SW</para>
            /// </summary>
            public string PostDirectional { get { return this._PostDirectional.Value; } set { this._PostDirectional.Value = value; } }
            /// <summary>
            /// Street Suffix
            /// <para>ST, AVE, BLVD, etc.</para>
            /// </summary>
            public string StreetSuffix { get { return this._StreetSuffix.Value; } set { this._StreetSuffix.Value = value; } }
            /// <summary>
            /// Words such as apartment, suite, etc. as reported to Experian.  May be an abbreviation.
            /// </summary>
            public string UnitType { get { return this._UnitType.Value; } set { this._UnitType.Value = value; } }
            /// <summary>
            /// Identity associated with the Unit Type.  May be a number or a letter and may include blanks.
            /// </summary>
            public string UnitID { get { return this._UnitID.Value; } set { this._UnitID.Value = value; } }
            /// <summary>
            /// City
            /// </summary>
            public string City { get { return this._City.Value; } set { this._City.Value = value; } }
            /// <summary>
            /// State
            /// </summary>
            public string State { get { return this._State.Value; } set { this._State.Value = value; } }
            /// <summary>
            /// Zip Code
            /// </summary>
            public string Zip { get { return this._Zip.Value; } set { this._Zip.Value = value.NumbersOnly(); } }
            /// <summary>
            /// The following fields will be blank if Experian was unable to standardize the address: Primary Street ID, Pre Directional, Street Suffix, Post Directional, Unit Type and Unit ID.
            /// </summary>
            public string NonStandardAddress { get { return this._NonStandardAddress.Value; } set { this._NonStandardAddress.Value = value; } }
            /// <summary>
            /// 
            /// </summary>
            public string Filler { get { return this._Filler.Value; } set { this._Filler.Value = value; } }
            /// <summary>
            /// 
            /// </summary>
            public string CensusGeoCode { get { return this._CensusGeoCode.Value; } set { this._CensusGeoCode.Value = value; } }
            /// <summary>
            /// 
            /// </summary>
            public string GeoStateCode { get { return this._GeoStateCode.Value; } set { this._GeoStateCode.Value = value; } }
            /// <summary>
            /// 
            /// </summary>
            public string GeoCountyCode { get { return this._GeoCountyCode.Value; } set { this._GeoCountyCode.Value = value; } }
            /// <summary>
            /// Date address first reported
            /// </summary>
            public DateTime? FirstReportedDate { get { return this._FirstReportedDate.Value; } set { this._FirstReportedDate.Value = value; } }
            /// <summary>
            /// Date address last reported
            /// </summary>
            public DateTime? LastUpdatedDate { get { return this._LastUpdatedDate.Value; } set { this._LastUpdatedDate.Value = value; } }
            /// <summary>
            /// 
            /// </summary>
            public string OriginationCode { get { return this._OriginationCode.Value; } set { this._OriginationCode.Value = value; } }
            /// <summary>
            /// 
            /// </summary>
            public int? NumberTimesReported { get { return this._NumberTimesReported.Value; } set { this._NumberTimesReported.Value = value; } }
            /// <summary>
            /// 
            /// </summary>
            public string Filler2 { get { return this._Filler2.Value; } set { this._Filler2.Value = value; } }
            #endregion

            public FileOneAddress(string Segment)
            {
                this._SegmentIndicator = new CTEnum<HitType, Enums.Values.HitType>(1) { DataString = Segment };
                this._PrimaryStreetID = new CTString(10) { DataString = Segment.Length > 1 ? Segment.Substring(1) : "" };
                this._PreDirectional = new CTString(2) { DataString = Segment.Length > 11 ? Segment.Substring(11) : "" };
                this._StreetName = new CTString(32) { DataString = Segment.Length > 13 ? Segment.Substring(13) : "" };
                this._PostDirectional = new CTString(2) { DataString = Segment.Length > 45 ? Segment.Substring(45) : "" };
                this._StreetSuffix = new CTString(4) { DataString = Segment.Length > 47 ? Segment.Substring(47) : "" };
                this._UnitType = new CTString(4) { DataString = Segment.Length > 51 ? Segment.Substring(51) : "" };
                this._UnitID = new CTString(8) { DataString = Segment.Length > 55 ? Segment.Substring(55) : "" };
                this._City = new CTString(24) { DataString = Segment.Length > 63 ? Segment.Substring(63) : "" };
                this._State = new CTString(2) { DataString = Segment.Length > 87 ? Segment.Substring(87) : "" };
                this._Zip = new CTString(9) { DataString = Segment.Length > 89 ? Segment.Substring(89) : "" };
                this._NonStandardAddress = new CTString(60) { DataString = Segment.Length > 98 ? Segment.Substring(98) : "" };
                this._Filler = new CTString(1) { DataString = Segment.Length > 158 ? Segment.Substring(158) : "" };
                this._CensusGeoCode = new CTString(7) { DataString = Segment.Length > 159 ? Segment.Substring(159) : "" };
                this._GeoStateCode = new CTString(2) { DataString = Segment.Length > 166 ? Segment.Substring(166) : "" };
                this._GeoCountyCode = new CTString(3) { DataString = Segment.Length > 168 ? Segment.Substring(168) : "" };
                this._FirstReportedDate = new CTDate() { DataString = Segment.Length > 171 ? Segment.Substring(171) : "" };
                this._LastUpdatedDate = new CTDate() { DataString = Segment.Length > 179 ? Segment.Substring(179) : "" };
                this._OriginationCode = new CTString(1) { DataString = Segment.Length > 187 ? Segment.Substring(187) : "" };
                this._NumberTimesReported = new CTNumber(2) { DataString = Segment.Length > 188 ? Segment.Substring(188) : "" };
                this._Filler2 = new CTString(10) { DataString = Segment.Length > 190 ? Segment.Substring(190) : "" };
            }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}",
                    this._SegmentIndicator,
                    this._PrimaryStreetID,
                    this._PreDirectional,
                    this._StreetName,
                    this._PostDirectional,
                    this._StreetSuffix,
                    this._UnitType,
                    this._UnitID,
                    this._City,
                    this._State,
                    this._Zip,
                    this._NonStandardAddress,
                    this._Filler,
                    this._CensusGeoCode,
                    this._GeoStateCode,
                    this._GeoCountyCode,
                    this._FirstReportedDate,
                    this._LastUpdatedDate,
                    this._OriginationCode,
                    this._NumberTimesReported,
                    this._Filler2
                    );
            }
        }
        /// <summary>
        /// Client Supplied Info Segment
        /// <para>SEGCSD</para>
        /// </summary>
        public class SuppliedInfo
        {
            #region Private Properties
            private CTString _CustomerTextData { get; set; }
            #endregion

            #region Public Properties
            public string CustomerTextData { get { return this._CustomerTextData.Value; } set { this._CustomerTextData.Value = value; } }
            #endregion

            public SuppliedInfo(string Segment)
            {
                this._CustomerTextData = new CTString(100) { DataString = Segment };
            }

            public override string ToString()
            {
                return string.Format("{0}",
                    this._CustomerTextData
                    );
            }
        }
        /// <summary>
        /// Subscriber / Reporter Name and Address
        /// <para>SEGSNA</para>
        /// </summary>
        public class SubscriberNameAddress
        {
            #region Private Properties
            private CTString _Name { get; set; }
            private CTString _Address { get; set; }
            private CTString _City { get; set; }
            private CTString _State { get; set; }
            private CTString _Zip { get; set; }
            private CTString _Phone { get; set; }
            private CTString _Filler { get; set; }
            #endregion

            #region Public Properties
            public string Name { get { return this._Name.Value; } set { this._Name.Value = value; } }
            public string Address { get { return this._Address.Value; } set { this._Address.Value = value; } }
            public string City { get { return this._City.Value; } set { this._City.Value = value; } }
            public string State { get { return this._State.Value; } set { this._State.Value = value; } }
            public string Zip { get { return this._Zip.Value; } set { this._Zip.Value = value.NumbersOnly(); } }
            public string Phone { get { return this._Phone.Value; } set { this._Phone.Value = value.NumbersOnly(); } }
            public string Filler { get { return this._Filler.Value; } set { this._Filler.Value = value; } }
            #endregion

            public SubscriberNameAddress(string Segment)
            {
                this._Name = new CTString(30) { DataString = Segment };
                this._Address = new CTString(30) { DataString = Segment.Length > 30 ? Segment.Substring(30) : "" };
                this._City = new CTString(30) { DataString = Segment.Length > 60 ? Segment.Substring(60) : "" };
                this._State = new CTString(2) { DataString = Segment.Length > 90 ? Segment.Substring(90) : "" };
                this._Zip = new CTString(9) { DataString = Segment.Length > 92 ? Segment.Substring(92) : "" };
                this._Phone = new CTString(10) { DataString = Segment.Length > 101 ? Segment.Substring(101) : "" };
                this._Filler = new CTString(39) { DataString = Segment.Length > 111 ? Segment.Substring(111) : "" };
            }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}",
                    this._Name,
                    this._Address,
                    this._City,
                    this._State,
                    this._Zip,
                    this._Phone,
                    this._Filler
                    );
            }
        }
        /// <summary>
        /// Notice File One Employment
        /// <para>SEGEMP</para>
        /// </summary>
        public class FileOneEmployment
        {
            #region Private Properties
            private CTEnum<Enums.HitType, Enums.Values.HitType> _SegmentIndicator { get; set; }
            private CTString _Name { get; set; }
            private CTString _Address_Line1 { get; set; }
            private CTString _Address_Line2 { get; set; }
            private CTString _Address_ExtraLine { get; set; }
            private CTString _Zip { get; set; }
            private CTDate _FirstReportedDate { get; set; }
            private CTDate _LastReportedDate { get; set; }
            private CTString _OriginationCode { get; set; }
            private CTString _Filler { get; set; }
            #endregion

            #region Public Properties
            /// <summary>
            /// Employment Segment Indicator
            /// <para>T = Trigger</para>
            /// <para>F = File One</para>
            /// </summary>
            public Enums.HitType SegmentIndicator { get { return this._SegmentIndicator.Value; } set { this._SegmentIndicator.Value = value; } }
            public string Name { get { return this._Name.Value; } set { this._Name.Value = value; } }
            public string Address_Line1 { get { return this._Address_Line1.Value; } set { this._Address_Line1.Value = value; } }
            public string Address_Line2 { get { return this._Address_Line2.Value; } set { this._Address_Line2.Value = value; } }
            public string Address_ExtraLine { get { return this._Address_ExtraLine.Value; } set { this._Address_ExtraLine.Value = value; } }
            public string Zip { get { return this._Zip.Value; } set { this._Zip.Value = value.NumbersOnly(); } }
            public DateTime? FirstReportedDate { get { return this._FirstReportedDate.Value; } set { this._FirstReportedDate.Value = value; } }
            public DateTime? LastReportedDate { get { return this._LastReportedDate.Value; } set { this._LastReportedDate.Value = value; } }
            public string OriginationCode { get { return this._OriginationCode.Value; } set { this._OriginationCode.Value = value; } }
            public string Filler { get { return this._Filler.Value; } set { this._Filler.Value = value; } }
            #endregion

            public FileOneEmployment(string Segment)
            {
                this._SegmentIndicator = new CTEnum<HitType, Enums.Values.HitType>(1) { DataString = Segment };
                this._Name = new CTString(32) { DataString = Segment.Length > 1 ? Segment.Substring(1) : "" };
                this._Address_Line1 = new CTString(32) { DataString = Segment.Length > 33 ? Segment.Substring(33) : "" };
                this._Address_Line2 = new CTString(32) { DataString = Segment.Length > 65 ? Segment.Substring(65) : "" };
                this._Address_ExtraLine = new CTString(32) { DataString = Segment.Length > 97 ? Segment.Substring(97) : "" };
                this._Zip = new CTString(9) { DataString = Segment.Length > 129 ? Segment.Substring(129) : "" };
                this._FirstReportedDate = new CTDate() { DataString = Segment.Length > 138 ? Segment.Substring(138) : "" };
                this._LastReportedDate = new CTDate() { DataString = Segment.Length > 146 ? Segment.Substring(146) : "" };
                this._OriginationCode = new CTString(1) { DataString = Segment.Length > 154 ? Segment.Substring(154) : "" };
                this._Filler = new CTString(10) { DataString = Segment.Length > 155 ? Segment.Substring(155) : "" };
            }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}",
                    this._SegmentIndicator,
                    this._Name,
                    this._Address_Line1,
                    this._Address_Line2,
                    this._Address_ExtraLine,
                    this._Zip,
                    this._FirstReportedDate,
                    this._LastReportedDate,
                    this._OriginationCode,
                    this._Filler
                    );
            }
        }
        /// <summary>
        /// Notice Telephone Number Segment
        /// <para>SEGTPH</para>
        /// </summary>
        public class Telephone
        {
            #region Private Properties
            private CTEnum<Enums.HitType, Enums.Values.HitType> _SegmentIndicator { get; set; }
            private CTString _Phone { get; set; }
            private CTEnum<Enums.PhoneSource, Enums.Values.PhoneSource> _Phone_Source { get; set; }
            private CTEnum<Enums.PhoneType, Enums.Values.PhoneType> _Phone_Type { get; set; }
            private CTDate _Phone_DateAdded { get; set; }
            private CTDate _Phone_DateUpdated { get; set; }
            private CTString _Filler { get; set; }
            #endregion

            #region Public Properties
            public Enums.HitType SegmentIndicator { get { return this._SegmentIndicator.Value; } set { this._SegmentIndicator.Value = value; } }
            public string PhoneNumber { get { return this._Phone.Value; } set { this._Phone.Value = value.NumbersOnly(); } }
            public Enums.PhoneSource Source { get { return this._Phone_Source.Value; } set { this._Phone_Source.Value = value; } }
            public Enums.PhoneType PhoneType { get { return this._Phone_Type.Value; } set { this._Phone_Type.Value = value; } }
            public DateTime? DateAdded { get { return this._Phone_DateAdded.Value; } set { this._Phone_DateAdded.Value = value; } }
            public DateTime? DateUpdated { get { return this._Phone_DateUpdated.Value; } set { this._Phone_DateUpdated.Value = value; } }
            public string Filler { get { return this._Filler.Value; } set { this._Filler.Value = value; } }
            #endregion

            public Telephone(string Segment)
            {
                this._SegmentIndicator = new CTEnum<HitType, Enums.Values.HitType>(1) { DataString = Segment };
                this._Phone = new CTString(10) { DataString = Segment.Length > 1 ? Segment.Substring(1) : "" };
                this._Phone_Source = new CTEnum<PhoneSource, Enums.Values.PhoneSource>(1) { DataString = Segment.Length > 11 ? Segment.Substring(11) : "" };
                this._Phone_Type = new CTEnum<PhoneType, Enums.Values.PhoneType>(1) { DataString = Segment.Length > 12 ? Segment.Substring(12) : "" };
                this._Phone_DateAdded = new CTDate() { DataString = Segment.Length > 13 ? Segment.Substring(13) : "" };
                this._Phone_DateUpdated = new CTDate() { DataString = Segment.Length > 21 ? Segment.Substring(21) : "" };
                this._Filler = new CTString(11) { DataString = Segment.Length > 29 ? Segment.Substring(29) : "" };
            }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}",
                    this._SegmentIndicator,
                    this._Phone,
                    this._Phone_Source,
                    this._Phone_Type,
                    this._Phone_DateAdded,
                    this._Phone_DateUpdated,
                    this._Filler
                    );
            }
        }
    }

    namespace Elements
    {
        public class Telephone
        {
            #region Private Properties
            private CTString _AreaCode { get; set; }
            private CTString _Phone { get; set; }
            #endregion

            #region Public Properties
            public string AreaCode { get { return this._AreaCode.Value; } set { this._AreaCode.Value = value.NumbersOnly(); } }
            public string Phone { get { return this._Phone.Value; } set { this._Phone.Value = value.NumbersOnly(); } }
            public string FullPhone { get { return this._AreaCode.Value + this._Phone.Value; } set { if (value.Length == 10) { this._AreaCode.Value = value; this._Phone.Value = value.Substring(3); } else if (value.Length == 7) { this._AreaCode.Value = ""; this._Phone.Value = value; } } }
            #endregion

            public Telephone(string Phone)
            {
                Phone = Phone.NumbersOnly();
                this._AreaCode = new CTString(3) { DataString = Phone.Length > 7 ? Phone : "" };
                this._Phone = new CTString(7) { DataString = Phone.Length > 7 ? Phone.Substring(3) : Phone };
            }

            public override string ToString()
            {
                return string.Format("{0}{1}",
                    this._AreaCode,
                    this._Phone);
            }
        }
    }
}
