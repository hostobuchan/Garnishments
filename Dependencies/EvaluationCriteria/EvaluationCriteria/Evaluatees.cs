using EvaluationCriteria.Accounts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using static EvaluationCriteria.Utilities;

namespace EvaluationCriteria
{

    public class Evaluatees
    {
        private List<Evaluatee> _EvalList = new List<Evaluatee>();

        public Evaluatees(DataTable MASTER, DataTable Diary, bool CalculateBalance = false)
        {
            foreach (DataRow dr in MASTER.Rows)
            {
                if (Diary.Rows.Count > 0 && Diary.Select("FILENO='" + dr["FILENO"].ToString() + "'").Length > 0)
                    _EvalList.Add(new Evaluatee(dr, Diary.Select("FILENO='" + dr["FILENO"].ToString() + "'").CopyToDataTable(), CalculateBalance));
                else
                    _EvalList.Add(new Evaluatee(dr, new DataTable(), CalculateBalance));
            }
        }
        public Evaluatees(SqlDataReader Reader, DataTable Diary, bool CalculateBalance = false)
        {
            DataTable dt = new DataTable();
            foreach (DataRow d in Reader.GetSchemaTable().Rows)
            {
                dt.Columns.Add(new DataColumn(d["ColumnName"].ToString(), (Type)d["DataType"]));
            }
            DataRow dr = dt.NewRow();
            while (Reader.Read())
            {
                for (int e = 0; e < Reader.FieldCount; e++)
                {
                    dr[e] = Reader[e];
                }
                if (Diary.Select("FILENO='" + Reader["FILENO"].ToString() + "'").Length > 0)
                    _EvalList.Add(new Evaluatee(dr, Diary.Select("FILENO='" + dr["FILENO"].ToString() + "'").CopyToDataTable(), CalculateBalance));
                else
                    _EvalList.Add(new Evaluatee(dr, new DataTable(), CalculateBalance));
            }
        }
        public Evaluatees(IDataReader Reader, bool CalculateBalance = false)
        {
            DataTable dt = new DataTable();
            foreach (DataRow d in Reader.GetSchemaTable().Rows)
            {
                dt.Columns.Add(new DataColumn(d["ColumnName"].ToString(), (Type)d["DataType"]));
            }
            DataRow dr = dt.NewRow();
            while (Reader.Read())
            {
                for (int e = 0; e < Reader.FieldCount; e++)
                {
                    dr[e] = Reader[e];
                }
                _EvalList.Add(new Evaluatee(dr, new DataTable(), CalculateBalance));
            }
        }

        public List<Evaluatee> EvaluateeList { get { return _EvalList; } set { _EvalList = value; } }
        public List<Evaluatee> EvaluateeListUnique { get { return _EvalList.GroupBy(el => el.FileNo).Select(el => el.First()).ToList(); } }

        public class Evaluatee
        {
            #region Private Variables
            private string _FileNo;
            private string _Address;
            private string _Address2;
            private int _Adva;
            private DateTime? _AnswerFiled;
            private double _Balance;
            private int _BankNo;
            private DateTime? _BankruptcyDate;
            private DateTime? _BankruptcyDischargeDate;
            private DateTime? _BankruptcyDismissalDate;
            private int? _C_Priority;
            private string _City;
            private string _Classification;
            private string _ClientType;
            private int _Collector;
            private double _Cost;
            private DateTime _CourtDate;
            private DateTime? _DateReceived;
            private DateTime? _DeathDate;
            private int _Debtor;
            private List<DiaryCode> _Diaries = new List<DiaryCode>();
            private string _Docket;
            private DateTime? _DismissalDate;
            private int _EmpNo;
            private int _Forw;
            private DateTime? _GarnishmentDate;
            private double? _JudgmentAmount;
            private DateTime? _JudgmentDate;
            private double? _LastPaymentAmount;
            private DateTime? _LastPaymentDate;
            private int _LetterPrevType;
            private string _LetterPrev;
            private DateTime? _LetterPrevDate;
            private int _MinSifRate;
            private string[] _Name;
            private DateTime? _OpenedDate;
            private double _PercentPaid;
            private bool _ReturnMail;
            private int _SalesNo;
            private DateTime? _ServiceDate;
            private int _SherNo;
            private int _SifRate;
            private string _SSN;
            private DateTime? _Stat2Date;
            private string _State;
            private string _Status;
            private DateTime _StatusDate;
            private DateTime? _StatuteDate;
            private DateTime? _SuitDate;
            private int? _SuitScore;
            private DateTime? _Trigger;
            private int _Venue;
            private string _Zip;
            #endregion

            public Evaluatee(DataRow FileData, DataTable Diaries, bool CalculateBalance = false)
            {
                _FileNo = FileData["FILENO"].ToString();
                // Get Address
                if (FileData.Table.Columns.IndexOf("STREET") > -1)
                {
                    if (FileData["STREET"] is string) _Address = (string)FileData["STREET"];
                    else _Address = "";
                }
                else
                { _Address = ""; }
                // Get Address Line 2
                if (FileData.Table.Columns.IndexOf("STREET2") > -1)
                {
                    if (FileData["STREET2"] is string) _Address2 = (string)FileData["STREET2"];
                    else _Address2 = "";
                }
                else
                { _Address2 = ""; }
                // Get Adversarial Attorney
                if (FileData.Table.Columns.IndexOf("ADVA") > -1)
                {
                    if (FileData["ADVA"] is int) _Adva = (int)FileData["ADVA"];
                    else if (FileData["ADVA"] is double) _Adva = (int)(double)FileData["ADVA"];
                    else if (FileData["ADVA"] is decimal) _Adva = (int)(decimal)FileData["ADVA"];
                    else _Adva = 0;
                }
                else { _Adva = 0; }
                // Get Answer Filed Date
                if (FileData.Table.Columns.IndexOf("AF_DATE") > -1)
                {
                    if (FileData["AF_DATE"] is DateTime) _AnswerFiled = (DateTime)FileData["AF_DATE"];
                    else _AnswerFiled = null;
                }
                else { _AnswerFiled = null; }
                // Get Balance
                if (CalculateBalance)
                {
                    if (FileData.Table.Columns.IndexOf("BALANCE") > -1)
                    {
                        if (FileData["BALANCE"] is double) _Balance = (double)FileData["BALANCE"];
                        else if (FileData["BALANCE"] is decimal) _Balance = (double)(decimal)FileData["BALANCE"];
                        else _Balance = 0;
                        try
                        {
                            _Balance += CalcDue((DateTime)FileData["INT_DATE"], (double)FileData["STAT_FEE"], (double)FileData["COST_EXP"], (double)FileData["COST_RECOVERED"], (double)FileData["STAT_EARN"], (double)FileData["BALANCE"], (double)FileData["PRE_J_RATE"], (double)FileData["POST_J_RATE"], (double)FileData["STORED_INT"], (double)FileData["JMT_AMT"], (double)FileData["INT_COLL"]);
                        }
                        catch { }
                    }
                    else { _Balance = 0; }
                }
                else
                {
                    if (FileData.Table.Columns.IndexOf("BALANCE") > -1)
                    {
                        if (FileData["BALANCE"] is double) _Balance = (double)FileData["BALANCE"];
                        else if (FileData["BALANCE"] is decimal) _Balance = (double)(decimal)FileData["BALANCE"];
                        else _Balance = 0;
                    }
                    else { _Balance = 0; }
                }
                // Get Bank (Related Party)
                if (FileData.Table.Columns.IndexOf("BANK_NO") > -1)
                {
                    if (FileData["BANK_NO"] is double) _BankNo = (int)(double)FileData["BANK_NO"];
                    else if (FileData["BANK_NO"] is decimal) _BankNo = (int)(decimal)FileData["BANK_NO"];
                    else if (FileData["BANK_NO"] is int) _BankNo = (int)FileData["BANK_NO"];
                    else _BankNo = 0;
                }
                else
                {
                    _BankNo = 0;
                }
                // Get Bankruptcy Date
                if (FileData.Table.Columns.IndexOf("BKCY_FILED_DATE") > -1)
                {
                    if (FileData["BKCY_FILED_DATE"] is DateTime) _BankruptcyDate = (DateTime)FileData["BKCY_FILED_DATE"];
                    else
                    {
                        if (FileData.Table.Columns.IndexOf("BKCY_CHAPTER") > -1 && FileData.Table.Columns.IndexOf("BKCY_FILENO") > -1)
                        {
                            if (FileData["BKCY_FILED_DATE"] is DBNull && FileData["BKCY_CHAPTER"] is DBNull && FileData["BKCY_FILENO"] is DBNull) _BankruptcyDate = null;
                            else _BankruptcyDate = DateTime.MinValue;
                        }
                        else { _BankruptcyDate = null; }
                    }
                }
                else { _BankruptcyDate = null; }
                // Get Bankruptcy Discharge Date
                if (FileData.Table.Columns.IndexOf("BKCY_DSCHG_DATE") > -1)
                {
                    if (FileData["BKCY_DSCHG_DATE"] is DateTime) _BankruptcyDischargeDate = (DateTime)FileData["BKCY_DSCHG_DATE"];
                    else _BankruptcyDischargeDate = null;
                }
                else { _BankruptcyDischargeDate = null; }
                // Get Bankruptcy Dismissal Date
                if (FileData.Table.Columns.IndexOf("BKCY_DSMIS_DATE") > -1)
                {
                    if (FileData["BKCY_DSMIS_DATE"] is DateTime) _BankruptcyDismissalDate = (DateTime)FileData["BKCY_DSMIS_DATE"];
                    else _BankruptcyDismissalDate = null;
                }
                else { _BankruptcyDismissalDate = null; }
                // Get C_Priority
                if (FileData.Table.Columns.IndexOf("C_PRIORITY") > -1)
                {
                    try
                    {
                        if (FileData["C_PRIORITY"] != DBNull.Value) _C_Priority = int.Parse(Criteria.NumberCleanup((string)FileData["C_PRIORITY"], typeof(int)));
                        else _C_Priority = null;
                    }
                    catch { _C_Priority = null; }
                }
                else
                {
                    _C_Priority = null;
                }
                // Get City
                if (FileData.Table.Columns.IndexOf("CITY") > -1)
                {
                    if (FileData["CITY"] is string) _City = (string)FileData["CITY"];
                    else _City = "";
                }
                else { _City = ""; }
                // Get Classification
                if (FileData.Table.Columns.IndexOf("CLASSIFICATION") > -1)
                {
                    _Classification = FileData["CLASSIFICATION"].ToString();
                }
                else { _Classification = ""; }
                // Get Client Type
                if (FileData.Table.Columns.IndexOf("CL_TYPE") > -1)
                {
                    _ClientType = FileData["CL_TYPE"].ToString();
                }
                else { _ClientType = ""; }
                // Get Collector
                if (FileData.Table.Columns.IndexOf("COLLECTOR") > -1)
                {
                    if (FileData["COLLECTOR"] is double) _Collector = (int)(double)FileData["COLLECTOR"];
                    else if (FileData["COLLECTOR"] is decimal) _Collector = (int)(decimal)FileData["COLLECTOR"];
                    else if (FileData["COLLECTOR"] is int) _Collector = (int)FileData["COLLECTOR"];
                    else _Collector = 0;
                }
                else { _Collector = 0; }
                // Get Cost
                if (FileData.Table.Columns.IndexOf("COST_EXP") > -1)
                {
                    if (FileData["COST_EXP"] is double) _Cost = (double)FileData["COST_EXP"];
                    else if (FileData["COST_EXP"] is decimal) _Cost = (double)(decimal)FileData["COST_EXP"];
                    else _Cost = 0;
                }
                else { _Cost = 0; }
                // Get Court Date
                if (FileData.Table.Columns.IndexOf("COURT_DATE") > -1)
                {
                    if (FileData["COURT_DATE"] is DateTime) _CourtDate = (DateTime)FileData["COURT_DATE"];
                    else
                    {
                        if (FileData.Table.Columns.IndexOf("COURT_DATE2") > -1)
                        {
                            if (FileData["COURT_DATE2"] is DateTime) _CourtDate = (DateTime)FileData["COURT_DATE2"];
                            else
                            {
                                if (FileData.Table.Columns.IndexOf("SUIT_DATE") > -1)
                                {
                                    if (FileData["SUIT_DATE"] is DateTime) _CourtDate = (DateTime)FileData["SUIT_DATE"];
                                    else _CourtDate = new DateTime(2007, 1, 1);
                                }
                                else { _CourtDate = new DateTime(2007, 1, 1); }
                            }
                        }
                        else
                        {
                            if (FileData.Table.Columns.IndexOf("SUIT_DATE") > -1)
                            {
                                if (FileData["SUIT_DATE"] is DateTime) _CourtDate = (DateTime)FileData["SUIT_DATE"];
                                else _CourtDate = new DateTime(2007, 1, 1);
                            }
                            else { _CourtDate = new DateTime(2007, 1, 1); }
                        }
                    }
                }
                else
                {
                    if (FileData.Table.Columns.IndexOf("SUIT_DATE") > -1)
                    {
                        if (FileData["SUIT_DATE"] is DateTime) _CourtDate = (DateTime)FileData["SUIT_DATE"];
                    }
                    else { _CourtDate = new DateTime(2007, 1, 1); }
                }

                // Get Date Received
                if (FileData.Table.Columns.IndexOf("DATE_RECD") > -1)
                {
                    if (FileData["DATE_RECD"] is DateTime) _DateReceived = (DateTime)FileData["DATE_RECD"];
                    else _DateReceived = null;
                }
                else { _DateReceived = null; }
                // Get Date of Death
                if (FileData.Table.Columns.IndexOf("DEATH_DATE") > -1)
                {
                    if (FileData["DEATH_DATE"] is DateTime) _DeathDate = (DateTime)FileData["DEATH_DATE"];
                    else _DeathDate = null;
                }
                else { _DeathDate = null; }
                // Get Diaries
                foreach (DataRow dr in Diaries.Rows)
                {
                    _Diaries.Add(new DiaryCode(dr));
                }
                // Get Debtor Number
                if (FileData.Table.Columns.IndexOf("NUMBER") > -1)
                {
                    if (FileData["NUMBER"] is double) _Debtor = (int)(double)FileData["NUMBER"];
                    else if (FileData["NUMBER"] is decimal) _Debtor = (int)(decimal)FileData["NUMBER"];
                    else if (FileData["NUMBER"] is int) _Debtor = (int)FileData["NUMBER"];
                    else _Debtor = 1;
                }
                else { _Debtor = 1; }
                // Get Docket
                if (FileData.Table.Columns.IndexOf("DOCKET_NO") > -1)
                {
                    if (FileData["DOCKET_NO"] is string) _Docket = (string)FileData["DOCKET_NO"];
                    else _Docket = "";
                }
                else { _Docket = ""; }
                // Get Dismissal Date
                if (FileData.Table.Columns.IndexOf("DSMIS_DATE") > -1)
                {
                    if (FileData["DSMIS_DATE"] is DateTime) _DismissalDate = (DateTime)FileData["DSMIS_DATE"];
                    else _DismissalDate = null;
                }
                else { _DismissalDate = null; }
                // Get Employer (Related Party)
                if (FileData.Table.Columns.IndexOf("EMP_NO") > -1)
                {
                    if (FileData["EMP_NO"] is double) _EmpNo = (int)(double)FileData["EMP_NO"];
                    else if (FileData["EMP_NO"] is decimal) _EmpNo = (int)(decimal)FileData["EMP_NO"];
                    else if (FileData["EMP_NO"] is int) _EmpNo = (int)FileData["EMP_NO"];
                    else _EmpNo = 0;
                }
                else
                {
                    _EmpNo = 0;
                }
                // Get Forwarder
                if (FileData.Table.Columns.IndexOf("FORW_NO") > -1)
                {
                    if (FileData["FORW_NO"] is double) _Forw = (int)(double)FileData["FORW_NO"];
                    else if (FileData["FORW_NO"] is decimal) _Forw = (int)(decimal)FileData["FORW_NO"];
                    else _Forw = 0;
                }
                // Get Garnishment Date
                if (FileData.Table.Columns.IndexOf("GARN_DATE") > -1)
                {
                    if (FileData["GARN_DATE"] is DateTime) _GarnishmentDate = (DateTime)FileData["GARN_DATE"];
                    else _GarnishmentDate = null;
                }
                else { _GarnishmentDate = null; }
                // Get Judgment Amount
                if (FileData.Table.Columns.IndexOf("JMT_AMT") > -1)
                {
                    if (FileData["JMT_AMT"] is double) _JudgmentAmount = (double)FileData["JMT_AMT"];
                    else if (FileData["JMT_AMT"] is decimal) _JudgmentAmount = (double)(decimal)FileData["JMT_AMT"];
                    else _JudgmentAmount = null;
                }
                else { _JudgmentAmount = null; }
                // Get Judgment Date
                if (FileData.Table.Columns.IndexOf("JMT_DATE") > -1)
                {
                    if (FileData["JMT_DATE"] is DateTime) _JudgmentDate = (DateTime)FileData["JMT_DATE"];
                    else _JudgmentDate = null;
                }
                else { _JudgmentDate = null; }
                // Get Last Payment Date
                if (FileData.Table.Columns.IndexOf("LPAYMNT_DATE") > -1)
                {
                    if (FileData["LPAYMNT_DATE"] is DateTime) _LastPaymentDate = (DateTime)FileData["LPAYMNT_DATE"];
                    else _LastPaymentDate = null;
                }
                else { _LastPaymentDate = null; }
                // Get Last Payment Amount
                if (FileData.Table.Columns.IndexOf("PMT_AMT") > -1)
                {
                    if (FileData["PMT_AMT"] is double) _LastPaymentAmount = (double)FileData["PMT_AMT"];
                    else if (FileData["PMT_AMT"] is decimal) _LastPaymentAmount = (double)(decimal)FileData["PMT_AMT"];
                    else _LastPaymentAmount = null;
                }
                else { _LastPaymentAmount = null; }
                // Get Debtor Name
                if (FileData.Table.Columns.IndexOf("NAME") > -1)
                {
                    if (FileData["NAME"] is string)
                    {
                        _Name = DebtorName((string)FileData["NAME"]);
                        NameSuffix = Suffix((string)FileData["NAME"]);
                    }
                    else
                    {
                        _Name = new string[3];
                        NameSuffix = "";
                    }
                }
                else
                { _Name = new string[3]; }
                // Get Previous Letter
                if (FileData.Table.Columns.IndexOf("PREV_LETTER") > -1)
                {
                    if (FileData["PREV_LETTER"] is string) _LetterPrev = (string)FileData["PREV_LETTER"];
                    else _LetterPrev = "";
                }
                else
                {
                    _LetterPrev = "";
                }
                // Get Previous Letter Type
                if (FileData.Table.Columns.IndexOf("PREV_LETTER_TYPE") > -1)
                {
                    if (FileData["PREV_LETTER_TYPE"] is int) _LetterPrevType = (int)FileData["PREV_LETTER_TYPE"];
                    else _LetterPrevType = 0;
                    if (FileData.Table.Columns.IndexOf("PREV_LETTER_DATE") > -1)
                    {
                        if (FileData["PREV_LETTER_DATE"] is DateTime)
                            _LetterPrevDate = (DateTime)FileData["PREV_LETTER_DATE"];
                        else _LetterPrevDate = null;
                    }
                    else
                    {
                        _LetterPrevDate = null;
                    }
                }
                else
                {
                    _LetterPrevType = 0;
                    _LetterPrevDate = null;
                }
                // Get Account Opened Date
                if (FileData.Table.Columns.IndexOf("OPENED_DATE") > -1)
                {
                    if (FileData["OPENED_DATE"] is DateTime) _OpenedDate = (DateTime)FileData["OPENED_DATE"];
                    else _OpenedDate = null;
                }
                else
                {
                    _OpenedDate = null;
                }
                // Get Percent Paid
                _PercentPaid = 0;

                // Get Return Mail
                if (FileData.Table.Columns.IndexOf("RET_MAIL") > -1)
                {
                    if (FileData["RET_MAIL"] is DBNull) _ReturnMail = false;
                    else _ReturnMail = true;
                }
                else { _ReturnMail = false; }
                // Get Sales No
                if (FileData.Table.Columns.IndexOf("SALES_NO") > -1)
                {
                    if (FileData["SALES_NO"] is double) _SalesNo = (int)(double)FileData["SALES_NO"];
                    else if (FileData["SALES_NO"] is decimal) _SalesNo = (int)(decimal)FileData["SALES_NO"];
                    else if (FileData["SALES_NO"] is int) _SalesNo = (int)FileData["SALES_NO"];
                    else _SalesNo = 0;
                }
                else { _SalesNo = 0; }
                // Get Service Date
                if (FileData.Table.Columns.IndexOf("SERVICE_DATE") > -1)
                {
                    if (FileData["SERVICE_DATE"] is DateTime) _ServiceDate = (DateTime)FileData["SERVICE_DATE"];
                    else _ServiceDate = null;
                }
                else { _ServiceDate = null; }
                // Get Sheriff No
                if (FileData.Table.Columns.IndexOf("SHER_NO") > -1)
                {
                    if (FileData["SHER_NO"] is double) _SherNo = (int)(double)FileData["SHER_NO"];
                    else if (FileData["SHER_NO"] is decimal) _SherNo = (int)(decimal)FileData["SHER_NO"];
                    else if (FileData["SHER_NO"] is int) _SherNo = (int)FileData["SHER_NO"];
                    else _SherNo = 0;
                }
                else { _SherNo = 0; }
                // Get SSN
                if (FileData.Table.Columns.IndexOf("SSN") > -1)
                {
                    if (FileData["SSN"] is string) _SSN = (string)FileData["SSN"];
                    else _SSN = "";
                }
                else
                {
                    _SSN = "";
                }
                // Get Stat2 Date
                if (FileData.Table.Columns.IndexOf("STAT2_DATE") > -1)
                {
                    if (FileData["STAT2_DATE"] is DateTime) _Stat2Date = (DateTime)FileData["STAT2_DATE"];
                    else _Stat2Date = null;
                }
                else { _Stat2Date = null; }
                // Get State
                if (FileData.Table.Columns.IndexOf("STATE") > -1)
                {
                    if (FileData["STATE"] is string) _State = FileData["STATE"].ToString();
                    else _State = "";
                }
                else { _State = ""; }
                // Get Status
                if (FileData.Table.Columns.IndexOf("STAT1_CODE") > -1)
                {
                    if (FileData["STAT1_CODE"] is string) _Status = FileData["STAT1_CODE"].ToString();
                    else _Status = "";
                }
                else { _Status = ""; }
                // Get Status Date
                if (FileData.Table.Columns.IndexOf("STAT1_DATE") > -1)
                {
                    if (FileData["STAT1_DATE"] is DateTime) _StatusDate = (DateTime)FileData["STAT1_DATE"];
                    else _StatusDate = DateTime.MinValue;
                }
                else { _StatusDate = DateTime.MinValue; }
                // Get Statute Date
                _StatuteDate = null;

                // Get Suit Date
                if (FileData.Table.Columns.IndexOf("SUIT_DATE") > -1)
                {
                    if (FileData["SUIT_DATE"] is DateTime) _SuitDate = (DateTime)FileData["SUIT_DATE"];
                    else _SuitDate = null;
                }
                else { _SuitDate = null; }
                // Get Suit Score
                if (FileData.Table.Columns.IndexOf("SUIT_SCORE") > -1)
                {
                    int Score;
                    if (FileData["SUIT_SCORE"] is double) _SuitScore = (int)(double)FileData["SUIT_SCORE"];
                    else if (FileData["SUIT_SCORE"] is decimal) _SuitScore = (int)(decimal)FileData["SUIT_SCORE"];
                    else if (FileData["SUIT_SCORE"] is int) _SuitScore = (int)FileData["SUIT_SCORE"];
                    else if (int.TryParse(FileData["SUIT_SCORE"].ToString(), out Score)) _SuitScore = Score;
                    else _SuitScore = null;
                }
                else { _SuitScore = null; }
                // Get Sif Rate
                if (FileData.Table.Columns.IndexOf("SIFRATE") > -1)
                {
                    if (FileData["SIFRATE"] is string) _SifRate = SifRateFinder((string)FileData["SIFRATE"], this);
                    else _SifRate = 100;
                }
                else
                {
                    _SifRate = 100;
                }
                // Get Min Sif Rate
                if (FileData.Table.Columns.IndexOf("MINSIFRATE") > -1)
                {
                    if (FileData["MINSIFRATE"] is string) _MinSifRate = SifRateFinder((string)FileData["MINSIFRATE"], this);
                    else _MinSifRate = 100;
                }
                else
                {
                    _MinSifRate = 100;
                }
                // Get Trigger Hit
                if (FileData.Table.Columns.IndexOf("TRIGGER") > -1)
                {
                    if (FileData["TRIGGER"] is DateTime) _Trigger = (DateTime)FileData["TRIGGER"];
                    else _Trigger = null;
                }
                else
                {
                    _Trigger = null;
                }
                // Get Venue
                if (FileData.Table.Columns.IndexOf("VENUE1_NO") > -1)
                {
                    if (FileData["VENUE1_NO"] is double) _Venue = (int)(double)FileData["VENUE1_NO"];
                    else if (FileData["VENUE1_NO"] is decimal) _Venue = (int)(decimal)FileData["VENUE1_NO"];
                    else if (FileData["VENUE1_NO"] is int) _Venue = (int)FileData["VENUE1_NO"];
                    else _Venue = 0;
                }
                else { _Venue = 0; }
                // Get Zip Code (and remove everything but numbers)
                if (FileData.Table.Columns.IndexOf("ZIP") > -1)
                {
                    if (FileData["ZIP"] is string) _Zip = Criteria.NumberCleanup((string)FileData["ZIP"], typeof(int));
                    else _Zip = "";
                }
                else { _Zip = ""; }
            }
            /// <summary>
            ///  WARNING!!! Initializes an Empty Evaluatee.  You Must Manually Set All Properties
            /// </summary>
            public Evaluatee() { }

            #region Public Variables
            public string FileNo { get { return _FileNo; } set { _FileNo = value; } }
            public string Address { get { return _Address; } set { _Address = value; } }
            public string Address2 { get { return _Address2; } set { _Address2 = value; } }
            public int AdversarialAttorney { get { return _Adva; } }
            public DateTime? AnswerFiled { get { return _AnswerFiled; } }
            public double Balance { get { return _Balance; } set { _Balance = value; } }
            public int BankNo { get { return _BankNo; } set { _BankNo = value; } }
            public DateTime? BankruptcyDate { get { return _BankruptcyDate; } set { _BankruptcyDate = value; } }
            public DateTime? BankruptcyDischargeDate { get { return _BankruptcyDischargeDate; } set { _BankruptcyDischargeDate = value; } }
            public DateTime? BankruptcyDismissalDate { get { return _BankruptcyDismissalDate; } set { _BankruptcyDismissalDate = value; } }
            public int? C_Priority { get { return _C_Priority; } set { _C_Priority = value; } }
            public string City { get { return _City; } set { _City = value; } }
            public string Classification { get { return _Classification; } set { _Classification = value; } }
            public string ClientType { get { return _ClientType; } set { _ClientType = value; } }
            public int Collector { get { return _Collector; } set { _Collector = value; } }
            public double Cost { get { return _Cost; } set { _Cost = value; } }
            public DateTime CourtDate { get { return _CourtDate; } set { _CourtDate = value; } }
            public DateTime? DateReceived { get { return _DateReceived; } set { _DateReceived = value; } }
            public DateTime? DeathDate { get { return _DeathDate; } set { _DeathDate = value; } }
            public int Debtor { get { return _Debtor; } set { _Debtor = value; } }
            public List<DiaryCode> Diaries { get { return _Diaries; } set { _Diaries = value; } }
            public string DisplayName { get { return _Name[0] + " " + _Name[1] + " " + _Name[2] + " (D" + _Debtor.ToString() + ")"; } }
            public DateTime DispositionDate
            {
                get
                {
                    if (_JudgmentDate != null)
                        return _JudgmentDate.Value;
                    else
                    {
                        if (_ServiceDate != null)
                            return _ServiceDate.Value;
                        else
                        {
                            if (_SuitDate != null)
                                return _SuitDate.Value;
                            else
                            {
                                if (_DateReceived != null)
                                    return _DateReceived.Value;
                                else
                                    return DateTime.MinValue;
                            }
                        }
                    }
                }
            }
            public string Docket { get { return _Docket; } }
            public DateTime? DismissalDate { get { return _DismissalDate; } set { _DismissalDate = value; } }
            public int EmployerNo { get { return _EmpNo; } set { _EmpNo = value; } }
            public int Forwarder { get { return _Forw; } set { _Forw = value; } }
            public DateTime? GarnishmentDate { get { return _GarnishmentDate; } set { _GarnishmentDate = value; } }
            public double? JudgmentAmount { get { return _JudgmentAmount; } set { _JudgmentAmount = value; } }
            public DateTime? JudgmentDate { get { return _JudgmentDate; } set { _JudgmentDate = value; } }
            public DateTime? LastPaymentDate { get { return _LastPaymentDate; } set { _LastPaymentDate = value; } }
            public int MinSifRate { get { return _MinSifRate; } }
            public string[] Name { get { return _Name; } set { _Name = value; } }
            public string NameFirst { get { return this.Name[0]; } }
            public string NameMiddle { get { return this.Name[1]; } }
            public string NameLast { get { return this.Name[2]; } }
            public string NameSuffix { get; private set; }
            public DateTime? OpenedDate { get { return this._OpenedDate; } set { this._OpenedDate = value; } }
            public double PercentPaid { get { return _PercentPaid; } set { _PercentPaid = value; } }
            public string PreviousLetter { get { return _LetterPrev; } }
            public int PreviousLetterType { get { return _LetterPrevType; } }
            public DateTime? PreviousLetterDate { get { return _LetterPrevDate; } }
            public double? LastPaymentAmount { get { return _LastPaymentAmount; } }
            public bool ReturnMail { get { return _ReturnMail; } }
            public int SalesNo { get { return _SalesNo; } set { _SalesNo = value; } }
            public DateTime? ServiceDate { get { return _ServiceDate; } set { _ServiceDate = value; } }
            public int SheriffNo { get { return _SherNo; } set { _SherNo = value; } }
            public int SifRate { get { return _SifRate; } }
            public string SSN { get { return _SSN; } set { _SSN = value; } }
            public DateTime? Stat2Date { get { return _Stat2Date; } set { _Stat2Date = value; } }
            public string State { get { return _State; } set { _State = value; } }
            public string Status { get { return _Status; } }
            public DateTime StatusDate { get { return _StatusDate; } }
            public DateTime? StatuteDate { get { return _StatuteDate; } set { _StatuteDate = value; } }
            public DateTime? SuitDate { get { return _SuitDate; } set { _SuitDate = value; } }
            public int? SuitScore { get { return _SuitScore; } }
            public DateTime? TriggerHit { get { return _Trigger; } set { _Trigger = value; } }
            public int Venue { get { return _Venue; } set { _Venue = value; } }
            public string Zip { get { return _Zip; } set { _Zip = value; } }
            #endregion

            public string AccountDescription
            {
                get
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(_FileNo + "\r\n");
                    sb.Append("Adversarial Atty: " + _Adva.ToString() + "\r\n");
                    if (_AnswerFiled != null)
                        sb.Append("Answer Filed: " + ((DateTime)_AnswerFiled).ToString("MM/dd/yyyy") + "\r\n");
                    sb.Append("Balance: " + _Balance.ToString("$#,##0.00;-$#,##0.00") + "\r\n");
                    if (_BankruptcyDate != null)
                        sb.Append("Bankruptcy: " + ((DateTime)_BankruptcyDate).ToString("MM/dd/yyyy") + "\r\n");
                    if (_Classification != "")
                        sb.Append("Classification: " + _Classification + "\r\n");
                    if (_ClientType != "")
                        sb.Append("Client Type: " + _ClientType + "\r\n");
                    sb.Append("Collector: " + _Collector.ToString() + "\r\n");
                    if (_Cost > 0)
                        sb.Append("Cost: " + _Cost.ToString("$#,##0.00;-$#,##0.00") + "\r\n");
                    if (_DateReceived != null)
                        sb.Append("Date Received: " + ((DateTime)_DateReceived).ToString("MM/dd/yyyy") + "\r\n");
                    if (_Docket != "")
                        sb.Append("Docket: " + _Docket + "\r\n");
                    if (_DismissalDate != null)
                        sb.Append("Dismissal: " + ((DateTime)_DismissalDate).ToString("MM/dd/yyyy") + "\r\n");
                    if (_Forw > 0)
                        sb.Append("Forwarder: " + _Forw.ToString() + "\r\n");
                    if (_GarnishmentDate != null)
                        sb.Append("Garn Date: " + ((DateTime)_GarnishmentDate).ToString("MM/dd/yyyy") + "\r\n"); ;
                    if (_JudgmentAmount != null)
                        sb.Append("Judgment Amount: " + ((double)_JudgmentAmount).ToString("$#,##0.00;-$#,##0.00") + "\r\n");
                    if (_JudgmentDate != null)
                        sb.Append("Judgment Date: " + ((DateTime)_JudgmentDate).ToString("MM/dd/yyyy") + "\r\n");
                    if (_LastPaymentAmount != null)
                        sb.Append("Last Payment: " + ((double)_LastPaymentAmount).ToString("$#,##0.00;-$#,##0.00") + "\r\n");
                    if (_LastPaymentDate != null)
                        sb.Append("Last Payment Date: " + ((DateTime)_LastPaymentDate).ToString("MM/dd/yyyy") + "\r\n");
                    if (_LetterPrev != "")
                        sb.Append("Previous Letter: " + _LetterPrev + "\r\n");
                    if (_LetterPrevType > 0)
                        sb.Append("Previous Letter Type: " + _LetterPrevType + "\r\n");
                    if (_LetterPrevDate != null)
                        sb.Append("Previous Letter Date: " + ((DateTime)_LetterPrevDate).ToString("MM/dd/yyyy") + "\r\n");
                    if (_PercentPaid > 0)
                        sb.Append("Balance Paid: " + (_PercentPaid / (double)100).ToString("0.00%") + "\r\n");
                    if (_ReturnMail)
                        sb.Append("Return Mail On Account\r\n");
                    if (_SalesNo > 0)
                        sb.Append("Sales No.: " + _SalesNo.ToString() + "\r\n");
                    if (_ServiceDate != null)
                        sb.Append("Service Date: " + ((DateTime)_ServiceDate).ToString("MM/dd/yyyy") + "\r\n");
                    if (_SifRate > 0)
                        sb.Append("SIF Rate: " + (_SifRate / (double)100).ToString("0%") + "\r\n");
                    if (_MinSifRate > 0)
                        sb.Append("Minimum SIF Rate: " + (_MinSifRate / (double)100).ToString("0%") + "\r\n");
                    if (_Stat2Date != null)
                        sb.Append("Stat2 Date: " + ((DateTime)_Stat2Date).ToString("MM/dd/yyyy") + "\r\n");
                    if (_State != "")
                        sb.Append("State: " + _State + "\r\n");
                    sb.Append("Account Status: " + _Status + "\r\n");
                    sb.Append("Status Date: " + _StatusDate.ToString("MM/dd/yyyy") + "\r\n");
                    if (_StatuteDate != null)
                        sb.Append("Statute Date: " + ((DateTime)_StatuteDate).ToString("MM/dd/yyyy") + "\r\n");
                    if (_SuitDate != null)
                        sb.Append("Suit Date: " + ((DateTime)_SuitDate).ToString("MM/dd/yyyy") + "\r\n");
                    if (_SuitScore != null)
                        sb.Append("Suit Score: " + ((int)_SuitScore).ToString() + "\r\n");

                    return sb.ToString();
                }
            }

            private double CalcDue(DateTime INT_DATE, double STAT_FEE, double COST_EXP, double COST_RECOVERED, double STAT_EARN, double BALANCE, double PRE_J_RATE, double POST_J_RATE, double STORED_INT, double JMT_AMT, double INT_COLL)
            {
                // Predefined variables which store the values to calculate the accrued interest
                DateTime Today = DateTime.Now;
                int NumDays;
                double PreJBase;
                double PreJPerDiem;
                double PostJBase;
                double PostJPerDiem;
                double AccruPre;
                double AccruPost;
                double DuePre;
                double DuePost;
                bool Calc_Int_On_PreJudgment_Costs;
                bool Calc_Int_On_PostJudgment_Stat_Fees;
                bool Calc_Int_On_PostJudgment_Costs;
                bool Calc_Int_On_PreJudgment_Stat_Fees;
                int IntBase_PreJCost;
                int IntBase_PostJCost;
                int IntBase_PreJStat;
                int IntBase_PostJStat;
                // Get todays date and store it in Today
                // Interest Calculation Setup
                // Change Y/N values as needed to
                // match setting in 1-S-4 setup
                // for interest math
                Calc_Int_On_PreJudgment_Costs = false;
                Calc_Int_On_PostJudgment_Costs = true;
                Calc_Int_On_PreJudgment_Stat_Fees = false;
                Calc_Int_On_PostJudgment_Stat_Fees = true;
                //  Int calc'ed always on prin
                if ((!Calc_Int_On_PreJudgment_Costs))
                {
                    IntBase_PreJCost = 1;
                }
                else
                {
                    IntBase_PreJCost = 0;
                }
                if ((Calc_Int_On_PostJudgment_Costs))
                {
                    IntBase_PostJCost = 1;
                }
                else
                {
                    IntBase_PostJCost = 0;
                }
                if ((!Calc_Int_On_PreJudgment_Stat_Fees))
                {
                    IntBase_PreJStat = 1;
                }
                else
                {
                    IntBase_PreJStat = 0;
                }
                if ((Calc_Int_On_PostJudgment_Stat_Fees))
                {
                    IntBase_PostJStat = 1;
                }
                else
                {
                    IntBase_PostJStat = 0;
                }
                // now that we have all the field values we can compute the accrued interest
                NumDays = (Today - INT_DATE).Days;
                if (NumDays < 0) NumDays *= -1;
                PreJBase = (BALANCE - (((STAT_FEE - STAT_EARN) * IntBase_PreJStat) + ((COST_EXP - COST_RECOVERED) * IntBase_PreJCost)));
                PostJBase = (BALANCE - (((STAT_FEE - STAT_EARN) * IntBase_PostJStat) + ((COST_EXP - COST_RECOVERED) * IntBase_PostJCost)));
                PreJPerDiem = ((PRE_J_RATE / 100) * (PreJBase / 365));
                PostJPerDiem = ((POST_J_RATE / 100) * (PostJBase / 365));
                if ((JMT_AMT > 0))
                {
                    AccruPost = ((NumDays * PostJPerDiem) + STORED_INT);
                    // Accrued-Post
                    DuePost = (AccruPost - INT_COLL);
                    // Due-Post
                    return Math.Round(DuePost, 2);
                }
                else
                {
                    AccruPre = ((NumDays * PreJPerDiem) + STORED_INT);
                    // Accrued-Pre
                    DuePre = (AccruPre - INT_COLL);
                    //  Due-Pre
                    return Math.Round(DuePre, 2);
                }
            }
            public static string Suffix(string Name)
            {
                return DebtorName(Name)[3];
            }
        }
    }
}
