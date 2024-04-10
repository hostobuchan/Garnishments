using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace RecordTypes.CLS
{
    public class Account
    {
        public delegate void DuplicatedHandler(object Sender, bool Remove);
        public event DuplicatedHandler DuplicatedHandled;
        private void OnDuplicateHandled(bool Remove) { if (this.DuplicatedHandled != null) this.DuplicatedHandled(this, Remove); }

        public string FileNo { get; set; }
        public int? Forw_No { get; set; }
        public string Forw_FileNo { get; set; }
        public string Forw_RefNo { get; set; }
        public string COCO_FileNo { get; set; }
        public string DocketNo { get; set; }
        public string Plaintiff { get; set; }
        public string OrigCred { get; set; }
        public DateTime? LastPaymentDate { get; set; }
        public DateTime? JudgmentDate { get; set; }
        public DateTime? SuitDate { get; set; }
        public DateTime? OpenDate { get; set; }
        public DateTime? ChargeOffDate { get; set; }
        public List<Debtor> Debtors { get; private set; }

        public ErrorTrapping.DuplicateInfo IsDuplicate { get; set; }
        public bool Send_CollectionsAdvantage { get; set; }
        public bool Send_Banko { get; set; }
        public bool Send_DemandLetter { get; set; }
        public bool NEWBIZ_Include { get; set; }
        public bool Correct_Captions { get; set; }

        private Account()
        {
            this.DocketNo = "";
            this.Debtors = new List<Debtor>();
            this.IsDuplicate = null;
            this.Send_CollectionsAdvantage = true;
            this.Send_Banko = true;
            this.Send_DemandLetter = true;
            this.NEWBIZ_Include = true;
            this.Correct_Captions = true;
        }
        public Account(string FileNo, int? Forw_No, string Forw_FileNo, string CoCo_FileNo) : this()
        {
            this.FileNo = FileNo;
            this.Forw_No = Forw_No;
            this.Forw_FileNo = Forw_FileNo;
            this.COCO_FileNo = CoCo_FileNo;
        }
        public Account(List<RMS.Base.Record> RMSRecords) : this()
        {
            if (RMSRecords.OfType<Citi.DebtorRecord>().Count() > 0)
            {
                Citi.DebtorRecord DR = RMSRecords.OfType<Citi.DebtorRecord>().First();
                this.Forw_FileNo = DR.AccountNumber.Value;
                this.Debtors.Add(new Debtor(this, DR));
                foreach (Citi.CoMakerRecord CR in RMSRecords.OfType<Citi.CoMakerRecord>().OrderBy(el => el.SequenceNumber.Value))
                {
                    if (!CR.CompanyName.Value.ToUpper().Contains("PROPERTY")) this.Debtors.Add(new Debtor(this, CR));
                }
                Citi.LegalRecord LR = RMSRecords.OfType<Citi.LegalRecord>().FirstOrDefault();
                if (LR != null)
                {
                    this.JudgmentDate = LR.Judgment_Date.Value;
                }
            }
            else if (RMSRecords.OfType<Ford.DebtorRecord>().Count() > 0)
            {
                Ford.DebtorRecord DR = RMSRecords.OfType<Ford.DebtorRecord>().First();
                this.Forw_FileNo = DR.AccountNumber.Value;
                if (DR.AccountStatus.Value.ToUpper().StartsWith("6")) this.JudgmentDate = DateTime.MinValue;
                this.Debtors.Add(new Debtor(this, DR));
                foreach (Ford.CoMakerRecord CR in RMSRecords.OfType<Ford.CoMakerRecord>().OrderBy(el => el.SequenceNumber.Value))
                {
                    if (!CR.CompanyName.Value.ToUpper().Contains("PROPERTY")) this.Debtors.Add(new Debtor(this, CR));
                }
            }
            else if (RMSRecords.OfType<RMS.DebtorRecord>().Count() > 0)
            {
                RMS.DebtorRecord DR = RMSRecords.OfType<RMS.DebtorRecord>().First();
                this.Forw_FileNo = DR.AccountNumber.Value;
                this.Debtors.Add(new Debtor(this, DR));
                foreach (RMS.CoMakerRecord CR in RMSRecords.OfType<RMS.CoMakerRecord>().OrderBy(el => el.SequenceNumber.Value))
                {
                    if (!CR.CompanyName.Value.ToUpper().Contains("PROPERTY")) this.Debtors.Add(new Debtor(this, CR));
                }
            }
            if (RMSRecords.OfType<RMS.Maintenance.MaintenanceInfo>().Count() > 0)
            {
                RMS.Maintenance.MaintenanceInfo MR = RMSRecords.OfType<RMS.Maintenance.MaintenanceInfo>().First();
                this.Forw_FileNo = MR.AccountNumber.Value;
            }
        }
        public Account(List<NAN.Base.Record> NANRecords) : this()
        {
            if (NANRecords.OfType<NAN.NewBusinessRecord>().Count() > 0)
            {
                NAN.NewBusinessRecord NANRecord = NANRecords.OfType<NAN.NewBusinessRecord>().First();
                this.Forw_FileNo = NANRecord.ClientAccountNumber.Value;
                this.COCO_FileNo = NANRecord.NetworkAccountNumber.Value;
                #region Parse Debtor 1
                Debtor Primary = new Debtor(this);
                Primary.Number = 1;
                string[] Name = NANRecord.Debtor_Name.Value.Trim().Split(',');
                if (Name.GetUpperBound(0) > 0)
                {
                    string[] Mid = Name[1].Trim().Split(' ');
                    Primary.FirstName = Mid[0];
                    Primary.LastName = Name[0];
                    if (Mid.GetUpperBound(0) > 0)
                        Primary.MiddleName = Mid[Mid.GetUpperBound(0)];
                    else
                        Primary.MiddleName = "";
                }
                else
                {
                    Primary.Company = Name[0];
                }
                Primary.Address1 = NANRecord.Debtor_Address1.Value;
                Primary.Address2 = NANRecord.Debtor_Address2.Value;
                Primary.City = NANRecord.Debtor_City.Value;
                Primary.State = NANRecord.Debtor_State.Value;
                Primary.Zip = NANRecord.Debtor_Zip.Value.NumbersOnly();
                Primary.CustomerID = NANRecord.Debtor_SSN.Value.NumbersOnly();
                Primary.Phone_Home = NANRecord.Debtor_Phone_Home.Value.NumbersOnly();
                Primary.Phone_Work = NANRecord.Debtor_Phone_Work.Value.NumbersOnly();
                this.Debtors.Add(Primary);
                #endregion
                #region Parse Debtor 2
                if (!string.IsNullOrEmpty(NANRecord.Debtor1_Name.Value))
                {
                    Debtor Secondary = new Debtor(this);
                    Secondary.Number = 2;
                    Name = NANRecord.Debtor1_Name.Value.Trim().Split(',');
                    if (Name.GetUpperBound(0) > 0)
                    {
                        string[] Mid = Name[1].Trim().Split(' ');
                        Secondary.FirstName = Mid[0];
                        Secondary.LastName = Name[0];
                        if (Mid.GetUpperBound(0) > 0)
                            Secondary.MiddleName = Mid[Mid.GetUpperBound(0)];
                        else
                            Secondary.MiddleName = "";
                    }
                    else
                    {
                        Secondary.Company = Name[0];
                    }
                    Secondary.Address1 = NANRecord.Debtor1_Address.Value;
                    Secondary.City = NANRecord.Debtor1_City.Value;
                    Secondary.State = NANRecord.Debtor1_State.Value;
                    Secondary.Zip = NANRecord.Debtor1_Zip.Value.NumbersOnly();
                    Secondary.CustomerID = NANRecord.Debtor1_SSN.Value.NumbersOnly();
                    Secondary.Phone_Home = NANRecord.Debtor1_Phone_Home.Value.NumbersOnly();
                    Secondary.Phone_Work = NANRecord.Debtor1_Phone_Work.Value.NumbersOnly();
                    this.Debtors.Add(Secondary);
                }
                #endregion
                #region Parse Debtor 3
                if (!string.IsNullOrEmpty(NANRecord.Debtor2_Name.Value))
                {
                    Debtor Secondary = new Debtor(this);
                    Secondary.Number = 3;
                    Name = NANRecord.Debtor2_Name.Value.Trim().Split(',');
                    if (Name.GetUpperBound(0) > 0)
                    {
                        string[] Mid = Name[1].Trim().Split(' ');
                        Secondary.FirstName = Mid[0];
                        Secondary.LastName = Name[0];
                        if (Mid.GetUpperBound(0) > 0)
                            Secondary.MiddleName = Mid[Mid.GetUpperBound(0)];
                        else
                            Secondary.MiddleName = "";
                    }
                    else
                    {
                        Secondary.Company = Name[0];
                    }
                    Secondary.Address1 = NANRecord.Debtor2_Address.Value;
                    Secondary.City = NANRecord.Debtor2_City.Value;
                    Secondary.State = NANRecord.Debtor2_State.Value;
                    Secondary.Zip = NANRecord.Debtor2_Zip.Value.NumbersOnly();
                    Secondary.CustomerID = NANRecord.Debtor2_SSN.Value.NumbersOnly();
                    Secondary.Phone_Home = NANRecord.Debtor2_Phone_Home.Value.NumbersOnly();
                    Secondary.Phone_Work = NANRecord.Debtor2_Phone_Work.Value.NumbersOnly();
                    this.Debtors.Add(Secondary);
                }
                #endregion
            }
            if (NANRecords.OfType<NAN.Maintenance.TransactionRecord>().Count() > 0)
            {
                NAN.Maintenance.TransactionRecord TR = NANRecords.OfType<NAN.Maintenance.TransactionRecord>().First();
                this.COCO_FileNo = TR.NetworkAccountNumber.Value;
            }
        }
        public Account(List<NCO.Base.Record> NCORecords) : this()
        {
            NCO.RecordType01 MR = NCORecords.OfType<NCO.RecordType01>().First();
            this.Forw_FileNo = MR.CreditorAccountNumber.Value;
            this.Forw_RefNo = MR.AccountNumber.Value;
            foreach (NCO.RecordType02 DR in NCORecords.OfType<NCO.RecordType02>())
            {
                this.Debtors.Add(new Debtor(this, DR));
            }
            foreach (NCO.RecordType03 CR in NCORecords.OfType<NCO.RecordType03>().OrderBy(el => el.RelationshipCode.Value))
            {
                if (CR.RelationshipCode.Value == NCO.Enums.RelationshipCode.CoMaker1 ||
                    CR.RelationshipCode.Value == NCO.Enums.RelationshipCode.CoMaker2 ||
                    CR.RelationshipCode.Value == NCO.Enums.RelationshipCode.CoMaker3 ||
                    CR.RelationshipCode.Value == NCO.Enums.RelationshipCode.CoMaker4 ||
                    CR.RelationshipCode.Value == NCO.Enums.RelationshipCode.CoMaker5)
                {
                    this.Debtors.Add(new Debtor(this, CR));
                }
            }
        }
        public Account(List<PLX.Base.RecordTypeBase> PLXRecords) : this()
        {
            PLX.PlacementRecord PR = PLXRecords.OfType<PLX.PlacementRecord>().First();
            this.Forw_FileNo = PR.AccountNumber;
            this.COCO_FileNo = PR.AccountID.ToString();
            foreach (PLX.BorrowerRecord BR in PLXRecords.OfType<PLX.BorrowerRecord>().OrderBy(el => el.BorrowerType))
            {
                this.Debtors.Add(new Debtor(this, BR));
            }
            PLX.LegalInfo LI = PLXRecords.OfType<PLX.LegalInfo>().FirstOrDefault();
            if (LI != null)
            {
                this.JudgmentDate = LI.JudgmentDate;
            }
        }
        public Account(List<YGC.Base.YGCBase> YGCRecords) : this()
        {
            YGC.Base.YGCSender2ReceiverRecord RR = YGCRecords.OfType<YGC.Base.YGCSender2ReceiverRecord>().FirstOrDefault();
            YGC.RecordType01 PL = YGCRecords.OfType<YGC.RecordType01>().FirstOrDefault();
            this.Forw_FileNo = RR?.FORW_FILE.Value;
            this.Forw_RefNo = RR?.FILENO.Value;
            this.OpenDate = PL?.DATE_OPEN.Value;
            this.LastPaymentDate = PL?.DATE_LPAY.Value;
            if (this.LastPaymentDate == null)
                this.LastPaymentDate = PL?.LPAY_ISS_D.Value;
            YGC.RecordType02 D = YGCRecords.OfType<YGC.RecordType02>().FirstOrDefault();
            if (D != null) this.Debtors.Add(new Debtor(this, D));
            if (YGCRecords.OfType<YGC.RecordType03>().Count() > 0)
            {
                YGC.RecordType03 C = YGCRecords.OfType<YGC.RecordType03>().First();

                if (!string.IsNullOrEmpty(C.D2_NAME.Value))
                {
                    Debtor D2 = new Debtor(this);
                    D2.Number = 2;
                    #region Parse Debtor2 Data
                    string[] Name = C.D2_NAME.Value.Split('/');
                    if (Name.GetUpperBound(0) > 0)
                    {
                        string[] Mid = Name[1].Split(' ');
                        D2.FirstName = Mid[0];
                        D2.LastName = Name[0];
                        if (Mid.GetUpperBound(0) > 0)
                        {
                            D2.MiddleName = Mid[1];
                        }
                        else
                            D2.MiddleName = "";
                    }
                    else
                    {
                        D2.Company = Name[0];
                    }
                    D2.Address1 = C.D2_STREET.Value;
                    D2.Address2 = "";
                    string CY = C.D2_CSZ.Value;
                    string[] CS = CY.Split(',');
                    if (CS.GetUpperBound(0) > 0)
                    {
                        D2.City = CS[0].Trim();
                        string[] SZ = CS[1].Trim().Split(' ');
                        D2.State = SZ[0];
                        D2.Zip = SZ[1];
                    }
                    else
                    {
                        CS = CY.Split(' ');
                        int St = CS.GetUpperBound(0);
                        if (St > 0)
                        {
                            for (int i = 0; i < St - 1; i++)
                            {
                                D2.City += CS[i] + " ";
                            }
                            D2.City = D2.City.Trim();
                            D2.State = CS[St - 1];
                            D2.Zip = CS[St];
                        }
                        else
                        {
                            D2.City = "";
                            D2.State = "";
                            D2.Zip = "";
                        }
                    }
                    D2.Phone_Home = C.D2_PHONE.Value.NumbersOnly();
                    D2.CustomerID = C.D2_SSN.Value.NumbersOnly();
                    #endregion
                    this.Debtors.Add(D2);
                }
                if (!string.IsNullOrEmpty(C.D3_NAME.Value))
                {
                    Debtor D2 = new Debtor(this);
                    D2.Number = 3;
                    #region Parse Debtor3 Data
                    string[] Name = C.D3_NAME.Value.Split('/');
                    if (Name.GetUpperBound(0) > 0)
                    {
                        string[] Mid = Name[1].Trim().Split(' ');
                        D2.FirstName = Mid[0];
                        D2.LastName = Name[0];
                        if (Mid.GetUpperBound(0) > 0)
                        {
                            D2.MiddleName = Mid[1];
                        }
                        else
                            D2.MiddleName = "";
                    }
                    else
                    {
                        D2.Company = Name[0];
                    }
                    D2.Address1 = C.D3_STREET.Value;
                    D2.Address2 = "";
                    string CY = C.D3_CSZ.Value;
                    string[] CS = CY.Split(',');
                    if (CS.GetUpperBound(0) > 0)
                    {
                        D2.City = CS[0].Trim();
                        string[] SZ = CS[1].Split(' ');
                        D2.State = SZ[0];
                        D2.Zip = SZ[1];
                    }
                    else
                    {
                        CS = CY.Split(' ');
                        int St = CS.GetUpperBound(0);
                        if (St > 0)
                        {
                            for (int i = 0; i < St - 1; i++)
                            {
                                D2.City += CS[i] + " ";
                            }
                            D2.City = D2.City.Trim();
                            D2.State = CS[St - 1];
                            D2.Zip = CS[St];
                        }
                        else
                        {
                            D2.City = "";
                            D2.State = "";
                            D2.Zip = "";
                        }
                    }
                    D2.Phone_Home = C.D3_PHONE.Value.NumbersOnly();
                    D2.CustomerID = C.D3_SSN.Value.NumbersOnly();
                    #endregion
                    this.Debtors.Add(D2);
                }
            }
            YGC.RecordType07 Lgl = YGCRecords.OfType<YGC.RecordType07>().FirstOrDefault();
            if (Lgl != null)
            {
                this.JudgmentDate = Lgl.JUDGMENT_DATE.Value;
                this.DocketNo = Lgl.DOCKET_NO.Value;
            }
            // Ozark Fix
            if (PL?.CTYPE.Value.ToUpper() == "JMT")
                this.JudgmentDate = DateTime.MinValue;
            // Asset Information

        }
        public Account(List<Trak.Base.RecordTypeBase> TrakRecords) : this()
        {
            Trak.TrakPlacement Placement = TrakRecords.OfType<Trak.TrakPlacement>().First();
            this.Forw_FileNo = Placement.OriginalAccountNumber.Value;
            this.Forw_RefNo = Placement.ForwarderAccountNumber.Value.Length >= 10 ? Placement.ForwarderAccountNumber.Value.Substring(0, 10) : Placement.ForwarderAccountNumber.Value;
            this.Plaintiff = Placement.CurrentCreditor.Value;
            this.OrigCred = Placement.OriginalCreditor.Value;
            //this.LastPaymentDate = Placement.LastPaymentDate_Client.Value.HasValue ? Placement.LastPaymentDate_Client.Value : Placement.LastPaymentDate_OriginalCreditor.Value;
            this.JudgmentDate = Placement.Judgment_Date.Value;
            this.OpenDate = Placement.OpenDate.Value;
            this.ChargeOffDate = Placement.ChargeOffDate.Value;
            this.Debtors.Add(new Debtor(this, Placement.Debtor));
            if (!string.IsNullOrEmpty(Placement.CoDebtor.FirstName.Value) || !string.IsNullOrEmpty(Placement.CoDebtor.LastName.Value))
                this.Debtors.Add(new Debtor(this, Placement.CoDebtor));
        }

        public void HandleDuplicate(bool Remove)
        {
            if (Remove)
            {
                OnDuplicateHandled(true);
            }
            else
            {
                this.IsDuplicate = null;
                OnDuplicateHandled(false);
            }
        }

        public DateTime? StatuteDate
        {
            get
            {
                if (this.Debtors.Count > 0)
                {
                    if (this.StatuteStartDate != null)
                    {
                        Debtor D = this.Debtors[0];
                        switch (D.State)
                        {
                            case "AR":
                                return this.StatuteStartDate.Value.AddYears(5);
                            case "IN":
                                return this.StatuteStartDate.Value.AddYears(4);
                            case "KY":
                                return this.StatuteStartDate.Value.AddYears(3);
                            case "OH":
                                return this.StatuteStartDate.Value.AddYears(3);
                            case "TN":
                                return this.StatuteStartDate.Value.AddYears(6);
                            case "TX":
                                return this.StatuteStartDate.Value.AddYears(4);
                            default:
                                return null;
                        }
                    }
                    else
                        return null;
                }
                else
                    return null;
            }
        }
        private DateTime? StatuteStartDate { get { if (this.JudgmentDate.HasValue) return null; else { if (this.LastPaymentDate == null) { return this.OpenDate; } else { return this.LastPaymentDate; } } } }

        public class Debtor
        {
            public Account ParentAccount { get; private set; }
            public byte? Number { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string NameSuffix { get; set; }
            public string Company { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Zip { get; set; }
            public string SSN { get { return this.CustomerID; } }
            public string TaxID { get { return this.CustomerID; } }
            public string CustomerID { get; set; }
            public string Phone_Home { get; set; }
            public string Phone_Work { get; set; }

            public ErrorTrapping.DebtorCheck ErrorChecking { get; private set; }

            public Debtor(Account ParentAccount)
            {
                this.ParentAccount = ParentAccount;
                this.ErrorChecking = new ErrorTrapping.DebtorCheck(this);
            }
            public Debtor(Account ParentAccount, byte Number, string CLSName, string Address, string Address2, string City, string State, string Zip, string SSN, string Phone) : this(ParentAccount)
            {
                this.Number = Number;
                string[] DName = DebtorName(CLSName);
                this.FirstName = DName[0];
                this.MiddleName = DName[1];
                this.LastName = DName[2];
                this.Address1 = Address;
                this.Address2 = Address2;
                this.City = City;
                this.State = State;
                this.Zip = Zip.NumbersOnly();
                this.CustomerID = SSN.NumbersOnly();
                this.Phone_Home = Phone.NumbersOnly();
            }
            public Debtor(Account ParentAccount, byte Number, string FirstName, string MiddleName, string LastName, string Address, string Address2, string City, string State, string Zip, string SSN, string Phone) : this(ParentAccount)
            {
                this.Number = Number;
                this.FirstName = FirstName;
                this.MiddleName = MiddleName;
                this.LastName = LastName;
                this.Address1 = Address;
                this.Address2 = Address2;
                this.City = City;
                this.State = State;
                this.Zip = Zip.NumbersOnly();
                this.CustomerID = SSN.NumbersOnly();
                this.Phone_Home = Phone.NumbersOnly();
            }
            public Debtor(Account ParentAccount, Citi.AccountRecord AR) : this(ParentAccount)
            {
                this.LastName = AR.LastName.Value;
                string[] Names = AR.FirstName.Value.Trim().Split(' ');
                if (Names.GetUpperBound(0) == 1)
                {
                    this.FirstName = Names[0];
                    this.MiddleName = Names[1];
                }
                else
                {
                    this.FirstName = AR.FirstName.Value;
                    this.MiddleName = "";
                }
                this.Company = AR.CompanyName.Value;
                this.Address1 = AR.Address1.Value;
                this.Address2 = AR.Address2.Value;
                this.City = AR.City.Value;
                this.State = AR.State.Value;
                this.Zip = AR.Zip.Value.NumbersOnly();
                this.Phone_Home = AR.Phone_Home.Value.NumbersOnly();
                this.Phone_Work = AR.Phone_Work.Value.NumbersOnly();
                if (AR is Citi.DebtorRecord) this.CustomerID = ((Citi.DebtorRecord)AR).CustomerID.Value.NumbersOnly();
                if (AR is Citi.CoMakerRecord) this.CustomerID = ((Citi.CoMakerRecord)AR).CustomerID.Value.NumbersOnly();
                this.Number = (byte?)(this.ParentAccount.Debtors.Count + 1);
            }
            public Debtor(Account ParentAccount, Ford.AccountRecord AR) : this(ParentAccount)
            {
                this.Company = AR.CompanyName.Value;
                if ((this.Company.Contains("   ") || AR.CustomerType.Value == RMS.Enums.CustomerTypes.Individual) && AR.CustomerType.Value != RMS.Enums.CustomerTypes.Corporation)
                {
                    this.LastName = AR.LastName.Value;
                    string[] Names = AR.FirstName.Value.Trim().Split(' ');
                    if (Names.GetUpperBound(0) == 2)
                    {
                        this.FirstName = Names[0];
                        this.MiddleName = Names[1];
                        this.NameSuffix = Names[2];
                    }
                    else if (Names.GetUpperBound(0) == 1)
                    {
                        this.FirstName = Names[0];
                        if (Utilities.Suffixes.Elements.Contains(Names[1].Replace(".", ""), new Utilities.CaseInsensitiveStringComparer()))
                        {
                            this.NameSuffix = Names[1];
                        }
                        else
                        {
                            this.MiddleName = Names[1];
                        }
                    }
                    else
                    {
                        this.FirstName = AR.FirstName.Value;
                        this.MiddleName = "";
                    }
                }
                this.Address1 = AR.Address1.Value;
                this.Address2 = AR.Address2.Value;
                this.City = AR.City.Value;
                this.State = AR.State.Value;
                this.Zip = AR.Zip.Value.NumbersOnly();
                this.Phone_Home = AR.Phone_Home.Value.NumbersOnly();
                this.Phone_Work = AR.Phone_Work.Value.NumbersOnly();
                if (AR is Ford.DebtorRecord) this.CustomerID = ((Ford.DebtorRecord)AR).CustomerID.Value.NumbersOnly();
                if (AR is Ford.CoMakerRecord) this.CustomerID = ((Ford.CoMakerRecord)AR).CustomerID.Value.NumbersOnly();
                this.Number = (byte?)(this.ParentAccount.Debtors.Count + 1);
            }
            public Debtor(Account ParentAccount, RMS.AccountRecord AR) : this(ParentAccount)
            {
                this.LastName = AR.LastName.Value;
                string[] Names = AR.FirstName.Value.Trim().Split(' ');
                if (Names.GetUpperBound(0) == 1)
                {
                    this.FirstName = Names[0];
                    this.MiddleName = Names[1];
                }
                else
                {
                    this.FirstName = AR.FirstName.Value;
                    this.MiddleName = "";
                }
                this.Company = (AR.LastName.DataString + AR.FirstName.DataString).Trim();
                this.Address1 = AR.Address1.Value;
                this.Address2 = AR.Address2.Value;
                this.City = AR.City.Value;
                this.State = AR.State.Value;
                this.Zip = AR.Zip.Value.NumbersOnly();
                this.Phone_Home = AR.Phone_Home.Value.NumbersOnly();
                this.Phone_Work = AR.Phone_Work.Value.NumbersOnly();
                this.CustomerID = AR.SSN.Value.NumbersOnly();
                this.Number = (byte?)(this.ParentAccount.Debtors.Count + 1);
            }
            public Debtor(Account ParentAccount, NCO.RecordType02 DR) : this(ParentAccount)
            {
                this.LastName = DR.LastName.Value;
                this.FirstName = DR.FirstName.Value;
                this.MiddleName = DR.MiddleName.Value;
                this.Company = DR.CommericalName.Value;
                this.Address1 = DR.StreetAddress1.Value;
                this.Address2 = DR.StreetAddress2.Value;
                this.City = DR.City.Value;
                this.State = DR.State.Value;
                this.Zip = DR.Zip.Value.NumbersOnly();
                this.Phone_Home = DR.Phone_Home.Value.NumbersOnly();
                this.Phone_Work = DR.Employer_Phone.Value.NumbersOnly();
                this.CustomerID = DR.SSN.Value.NumbersOnly();
                this.Number = (byte?)(this.ParentAccount.Debtors.Count + 1);
            }
            public Debtor(Account ParentAccount, NCO.RecordType03 DR) : this(ParentAccount)
            {
                this.LastName = DR.LastName.Value;
                this.FirstName = DR.FirstName.Value;
                this.MiddleName = DR.MiddleName.Value;
                this.Address1 = DR.StreetAddress1.Value;
                this.Address2 = DR.StreetAddress2.Value;
                this.City = DR.City.Value;
                this.State = DR.State.Value;
                this.Zip = DR.Zip.Value.NumbersOnly();
                this.Phone_Home = DR.Phone_Home.Value.NumbersOnly();
                this.Phone_Work = DR.Employer_Phone.Value.NumbersOnly();
                this.CustomerID = DR.SSN.Value.NumbersOnly();
                this.Number = (byte?)(this.ParentAccount.Debtors.Count + 1);
            }
            public Debtor(Account ParentAccount, PLX.BorrowerRecord BR) : this(ParentAccount)
            {
                string[] Names = BR.LastName.Trim().Split(' ');
                if (Names.GetUpperBound(0) == 1)
                {
                    this.LastName = Names[0];
                    this.NameSuffix = Names[1];
                }
                else
                {
                    this.LastName = BR.LastName;
                    this.NameSuffix = "";
                }

                Names = BR.FirstName.Trim().Split(' ');
                if (Names.GetUpperBound(0) == 1)
                {
                    this.FirstName = Names[0];
                    this.MiddleName = Names[1];
                }
                else
                {
                    this.FirstName = BR.FirstName;
                    this.MiddleName = "";
                }
                this.FirstName = BR.FirstName;
                this.Address1 = BR.Address;
                this.Address2 = BR.Address2;
                this.City = BR.City;
                this.State = BR.State;
                this.Zip = BR.Zip.NumbersOnly();
                this.Phone_Home = BR.HomePhone.NumbersOnly();
                this.Phone_Work = BR.WorkPhone.NumbersOnly();
                this.CustomerID = BR.SSN.NumbersOnly();
                this.Number = (byte?)(this.ParentAccount.Debtors.Count + 1);
            }
            public Debtor(Account ParentAccount, YGC.RecordType02 D) : this(ParentAccount)
            {
                this.Number = 1;
                string[] Name = D.FIRSTNAME.Value.Split(' ');
                this.FirstName = Name[0];
                this.LastName = D.LASTNAME.Value;
                this.MiddleName = Name.GetUpperBound(0) > 0 ? Name[1] : "";
                Name = D.D1_NAME.Value.Trim().Split('/');
                if (Name.GetUpperBound(0) > 0)
                {
                    string[] Mid = Name[1].Trim().Split(' ');
                    if (string.IsNullOrEmpty(this.FirstName))
                        this.FirstName = Mid[0];
                    if (string.IsNullOrEmpty(this.LastName))
                        this.LastName = Name[0];
                    if (string.IsNullOrEmpty(this.MiddleName) && Mid.GetUpperBound(0) > 0)
                    {
                        this.MiddleName = Mid[1];
                    }
                }
                else
                {
                    this.Company = Name[0];
                }
                this.Address1 = D.D1_STREET.Value;
                this.Address2 = D.D1_STREET2.Value;
                this.City = D.D1_CITY.Value;
                this.State = D.D1_STATE.Value;
                this.Zip = D.D1_ZIP.Value.NumbersOnly();
                this.Phone_Home = D.D1_PHONE.Value.NumbersOnly();
                this.CustomerID = D.D1_SSN.Value.NumbersOnly();
                if (string.IsNullOrEmpty(this.City) || string.IsNullOrEmpty(this.State))
                {
                    string CY = D.D1_CS.Value;
                    string[] CS = CY.Trim().Split(',');
                    if (CS.GetUpperBound(0) > 0)
                    {
                        if (string.IsNullOrEmpty(this.City))
                            this.City = CS[0].Trim();
                        if (string.IsNullOrEmpty(this.State))
                            this.State = CS[1].Trim();
                    }
                    else
                    {
                        CS = CY.Trim().Split(' ');
                        int St = CS.GetUpperBound(0);
                        if (string.IsNullOrEmpty(this.City))
                        {
                            for (int i = 0; i < St; i++)
                            {
                                this.City += CS[i] + " ";
                            }
                            this.City = this.City.Trim();
                        }
                        if (string.IsNullOrEmpty(this.State))
                            this.State = CS[St];
                    }
                }
            }
            public Debtor(Account ParentAccount, Trak.Debtor D) : this(ParentAccount)
            {
                this.Number = 1;
                this.FirstName = D.FirstName.Value;
                this.LastName = D.LastName.Value;
                this.MiddleName = D.MiddleName.Value;
                this.Address1 = D.Address.Value;
                this.Address2 = "";
                this.City = D.City.Value;
                this.State = D.State.Value;
                this.Zip = D.Zip.Value;
                this.Phone_Home = D.Phone.Value;
                this.CustomerID = D.SSN.Value;
            }
            public Debtor(Account ParentAccount, Trak.CoDebtor D) : this(ParentAccount)
            {
                this.Number = 2;
                this.FirstName = D.FirstName.Value;
                this.LastName = D.LastName.Value;
                this.MiddleName = D.MiddleName.Value;
                this.Address1 = D.Address.Value;
                this.Address2 = "";
                this.City = D.City.Value;
                this.State = D.State.Value;
                this.Zip = D.Zip.Value;
                this.Phone_Home = D.Phone.Value;
                this.CustomerID = D.SSN.Value;
            }

            private string[] DebtorName(string Name)
            {
                string[] DName = new string[3];
                this.NameSuffix = "";
                string[] theSplit;
                if ((theSplit = Name.Split('/')).Length > 1)
                {
                    if (theSplit[1].Contains(' '))
                    {
                        string[] Split2 = theSplit[1].Split(' ');
                        DName[0] = Split2[0];
                        DName[1] = Split2[1];
                        DName[2] = theSplit[0];
                        for (int t = 2; t <= Split2.GetUpperBound(0); t++)
                        {
                            this.NameSuffix = Split2[t];
                        }
                    }
                    else
                    {
                        DName[0] = theSplit[1];
                        DName[1] = "";
                        DName[2] = theSplit[0];
                    }
                    if (theSplit[0].Contains(' '))
                    {
                        DName[2] = theSplit[0].Split(' ')[0];
                        this.NameSuffix = theSplit[0].Split(' ')[1];
                    }
                }
                else
                {
                    DName[0] = Name;
                    DName[1] = "";
                    DName[2] = "";
                }
                return DName;
            }
        }
    }

    public static class AccountLoader
    {
        /// <summary>
        /// Creates Account Records for Result Set
        /// <para>Data Reader Must Query From Debtor Table</para>
        /// </summary>
        /// <param name="dr">Results From Debtor Table</param>
        /// <returns>List of Account Records</returns>
        public static List<Account> GetAccounts(IDataReader dr)
        {
            List<Account> Accounts = new List<Account>();
            Account curAccount = null;
            while (dr.Read())
            {
                if (curAccount == null || curAccount.FileNo.ToUpper() != dr["FILENO"].ToString().ToUpper())
                {
                    curAccount = new Account(dr["FILENO"].ToString(), null, "", "");
                    Accounts.Add(curAccount);
                }
                curAccount.Debtors.Add(new
                    Account.Debtor(curAccount,
                        Convert.ToByte(dr["NUMBER"]),
                        dr["NAME"] == DBNull.Value ? "" : dr["NAME"].ToString(),
                        dr["STREET"] == DBNull.Value ? "" : dr["STREET"].ToString(),
                        dr["STREET2"] == DBNull.Value ? "" : dr["STREET2"].ToString(),
                        dr["CITY"] == DBNull.Value ? "" : dr["CITY"].ToString(),
                        dr["ST"] == DBNull.Value ? "" : dr["ST"].ToString(),
                        dr["ZIP"] == DBNull.Value ? "" : dr["ZIP"].ToString(),
                        dr["SSN"] == DBNull.Value ? "" : dr["SSN"].ToString(),
                        dr["PHONE"] == DBNull.Value ? "" : dr["PHONE"].ToString()
                    )
                );
            }
            return Accounts;
        }
    }

    namespace ErrorTrapping
    {
        public interface ErrorInfo
        {
            bool HasError();
            bool DoNotImport();
        }

        public class DuplicateInfo
        {
            public string FileNo { get; private set; }
            public string AccountNumber { get; private set; }
            public bool HasOpenDuplicate { get; private set; }
            public bool DuplicatesInPlacement { get; private set; }

            public DuplicateInfo(string FileNo, string AccountNumber, bool DuplicateIsOpen, bool DuplicatesInPlacement = false)
            {
                this.FileNo = FileNo;
                this.AccountNumber = AccountNumber;
                this.HasOpenDuplicate = DuplicateIsOpen;
                this.DuplicatesInPlacement = DuplicatesInPlacement;
            }

            //public bool HasError() { return true; }
            //public bool DoNotImport() { return true; }

            public override string ToString()
            {
                return string.Format("Account: {0} Has a duplicate in {1} claims (FileNo: {2})", this.AccountNumber, this.HasOpenDuplicate ? "Open" : "Closed", this.FileNo);
            }
        }

        public class DebtorCheck
        {
            private Account.Debtor ParentDebtor { get; set; }

            public DebtorCheck(Account.Debtor AccountToExamine)
            {
                this.ParentDebtor = AccountToExamine;
            }

            public bool HasError { get { return this.BadSSN || this.BadName || this.BadAddress; } }
            public bool PossibleCommercialClaim { get { return (this.BadSSN || this.BadName) && !this.BadAddress; } }
            public bool BadSSN { get { return this.ParentDebtor.SSN.Length != 9; } }
            public bool BadName { get { return string.IsNullOrEmpty(this.ParentDebtor.FirstName); } }
            public bool BadAddress
            {
                get
                {
                    return string.IsNullOrEmpty(this.ParentDebtor.Address1)
                        || string.IsNullOrEmpty(this.ParentDebtor.City)
                        || string.IsNullOrEmpty(this.ParentDebtor.State)
                        || string.IsNullOrEmpty(this.ParentDebtor.Zip);
                }
            }

            //public void DoNotImport(){ }
            //public void AllowImport(){ }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                if (this.BadSSN) sb.Append("Invalid SSN");
                if (this.BadName) sb.Append(", Bad Name");
                if (this.BadAddress) sb.Append(", Invalid Address");
                if (sb.Length == 0) return "No Errors Detected";
                else
                {
                    if (sb[0] == ',') return sb.ToString().Substring(2);
                    else return sb.ToString();
                }
            }
        }
    }
}
