using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Configuration;

namespace EvaluationCriteria.CriteriaSets
{
    public class Criteria
    {
        string SQLConnection { get { return ConfigurationManager.ConnectionStrings["CriteriaDB"].ConnectionString; } }
        string SQLOptions { get { return ConfigurationManager.AppSettings["CriteriaOptions"]; } }
        string SQLCodeLists { get { return ConfigurationManager.AppSettings["CriteriaCodeLists"]; } }
        string SQLCodes { get { return ConfigurationManager.AppSettings["CriteriaCodes"]; } }
        string SQLCodeStrings { get { return ConfigurationManager.AppSettings["CriteriaCodeStrings"]; } }
        Dictionary<string, object> SQLDistinguisher { get; set; }
        string SQLDistinguisherString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (KeyValuePair<string, object> KVP in this.SQLDistinguisher)
                {
                    sb.Append(string.Format("{0}={1} AND ", KVP.Key, KVP.Value is bool ? Convert.ToByte(KVP.Value) : KVP.Value));
                }
                return sb.ToString().Substring(0, sb.Length - 5);
            }
        }
        public List<string> CriteriaItemNames { get { return this.CriteriaItems.Select(el => el.Key).ToList(); } }
        public Dictionary<string, CParam> CriteriaItems { get; set; }
        Dictionary<string, string> Value1 { get; set; }
        Dictionary<string, string> Value2 { get; set; }

        public Criteria(Dictionary<string,object> SQLDistinguisher)
        {
            this.SQLDistinguisher = SQLDistinguisher;
            this.CriteriaItems = new Dictionary<string, CParam>();
            this.Value1 = new Dictionary<string, string>();
            this.Value2 = new Dictionary<string, string>();

            this.CriteriaItems.Add("ADVA", new COptParam("Adversarial Attorney"));
            this.CriteriaItems.Add("AMT_PAID", new CBasicParam("Amount Paid"));
            this.CriteriaItems.Add("ANS_FILE", new CDateParam("Answer Filed"));
            this.CriteriaItems.Add("BALANCE", new CBasicParam("Balance"));
            this.CriteriaItems.Add("BANK_NO", new CBasicParam("Bank No."));
            this.CriteriaItems.Add("BK_CP", new CBasicParam("Bankruptcy Chapter"));
            this.CriteriaItems.Add("BK_DATE", new CDateParam("Bankruptcy Date"));
            this.CriteriaItems.Add("BK_FILE", new CSimpleParam("Bankruptcy FileNo"));
            this.CriteriaItems.Add("C_PRIORITY", new COptParam("C_Priority"));
            this.CriteriaItems.Add("CLASSIFICATION", new CClassParam("Classification"));
            this.CriteriaItems.Add("CLIENT", new CStringParam("Client"));
            this.CriteriaItems.Add("COLLECTORS", new CBoolParam("Collector"));
            this.CriteriaItems.Add("COST", new CBasicParam("Cost"));
            this.CriteriaItems.Add("COUNTY", new CStringParam("County"));
            this.CriteriaItems.Add("DATE_RECD", new CDateParam("Received Date"));
            this.CriteriaItems.Add("DEATH_DATE", new CDateParam("Death Date"));
            this.CriteriaItems.Add("DIARY_IS", new CBoolParam("Diary Requirement"));
            this.CriteriaItems.Add("DIARY_NOT", new CBoolParam("Diary Requirement 2"));
            this.CriteriaItems.Add("DIARY_ALSO", new CBoolParam("Diary Requirement 3"));
            this.CriteriaItems.Add("DOCKET", new CSimpleParam("Docket Info"));
            this.CriteriaItems.Add("DSMIS_DATE", new CDateParam("Dismissal Date"));
            this.CriteriaItems.Add("EMP_NO", new CBasicParam("Employer No."));
            this.CriteriaItems.Add("FORW_NO", new COptParam("Forwarder"));
            this.CriteriaItems.Add("GARN_DATE", new CDateParam("Garn Date"));
            this.CriteriaItems.Add("HAS_PHONE", new CSimpleParam("Has Phone"));
            this.CriteriaItems.Add("HOME_OWN", new CSimpleParam("Home Owner"));
            this.CriteriaItems.Add("JMT_AMT", new CBasicParam("Judgment Amount"));
            this.CriteriaItems.Add("JMT_DATE", new CDateParam("Judgment Date"));
            this.CriteriaItems.Add("LAST_LETTER", new CDateParam("Last Letter"));
            this.CriteriaItems.Add("LAST_PMT", new CDateParam("Last Payment"));
            this.CriteriaItems.Add("MINSIFRATE", new CBasicParam("Min Sif Rate"));
            this.CriteriaItems.Add("MIN_SUIT", new CBasicParam("Minimum Suit Balance"));
            this.CriteriaItems.Add("NSW", new CSimpleParam("Non Suit-Worthy"));
            this.CriteriaItems.Add("OPEN_DATE", new CDateParam("Opened Date"));
            this.CriteriaItems.Add("PERCENT_PAID", new CBasicParam("Percent Paid"));
            this.CriteriaItems.Add("PMT_AMT", new CBasicParam("Last Payment Amount"));
            this.CriteriaItems.Add("PREV_LETTER", new CClassParam("Previous Letter"));
            this.CriteriaItems.Add("PREV_LETTER_TYPE", new CLetterParam("Previous Letter Type"));
            this.CriteriaItems.Add("RET_MAIL", new CSimpleParam("Return Mail"));
            this.CriteriaItems.Add("SALES_NO", new CBoolParam("SalesNo"));
            this.CriteriaItems.Add("SERVICE_DATE", new CDateParam("Service Date"));
            this.CriteriaItems.Add("SHER_NO", new CBasicParam("Sheriff No."));
            this.CriteriaItems.Add("SIFRATE", new CBasicParam("Sif Rate"));
            this.CriteriaItems.Add("SSN", new CSimpleParam("SSN"));
            this.CriteriaItems.Add("STAT2_DATE", new CDateParam("Stat2 Date"));
            this.CriteriaItems.Add("STATE", new CStringParam("State"));
            this.CriteriaItems.Add("STATUS_IS", new CBoolParam("Required Status Code"));
            this.CriteriaItems.Add("STATUS_NOT", new CBoolParam("Exclusionary Status Code"));
            this.CriteriaItems.Add("STATUTE_DATE", new CDateParam("Statute Date"));
            this.CriteriaItems.Add("SUIT_DATE", new CDateParam("Suit Date"));
            this.CriteriaItems.Add("SUIT_SCORE", new CBasicParam("Suit Score"));
            this.CriteriaItems.Add("SW", new CSimpleParam("Suit-Worthy"));
            this.CriteriaItems.Add("VENUE_NO", new CBasicParam("Venue"));
            this.CriteriaItems.Add("MEDIA_PH", new CMediaParam("Pay History"));
            this.CriteriaItems.Add("MEDIA_COT", new CMediaParam("Chain of Title"));
            this.CriteriaItems.Add("MEDIA_TAC", new CMediaParam("Terms & Conds."));
            this.CriteriaItems.Add("MEDIA_CO", new CMediaParam("ChargeOff Statement"));
            this.CriteriaItems.Add("MEDIA_GS", new CMediaParam("General Statement"));
            this.CriteriaItems.Add("MEDIA_LD", new CMediaParam("Loan Documents"));
            this.CriteriaItems.Add("MEDIA_APP", new CMediaParam("Application"));
            this.CriteriaItems.Add("MEDIA_BOS", new CMediaParam("Bill of Sale"));
            this.CriteriaItems.Add("MEDIA_OTH", new CMediaParam("Other Media"));
            this.CriteriaItems.Add("MEDIA_ALL", new CMediaParam("All Media"));

            this.Value1.Add("ADVA", "AdversarialAttorney");
            this.Value1.Add("AMT_PAID", "AmountPaid");
            this.Value1.Add("ANS_FILE", "AnswerFiled");
            this.Value1.Add("BALANCE", "Balance");
            this.Value1.Add("BANK_NO", "BankNo");
            this.Value1.Add("BK_CP", "BankruptcyChapter");
            this.Value1.Add("BK_DATE", "BankruptcyDate");
            this.Value1.Add("BK_FILE", "BankruptcyFileNo");
            this.Value1.Add("C_PRIORITY", "C_Priority");
            this.Value1.Add("CLASSIFICATION", "Classification");
            this.Value1.Add("CLIENT", "ClientType");
            this.Value1.Add("COLLECTORS", "Collector");
            this.Value1.Add("COST", "Cost");
            this.Value1.Add("COUNTY", "County");
            this.Value1.Add("DATE_RECD", "DateReceived");
            this.Value1.Add("DEATH_DATE", "DeathDate");
            this.Value1.Add("DIARY_IS", "Diaries");
            this.Value1.Add("DIARY_NOT", "Diaries");
            this.Value1.Add("DIARY_ALSO", "Diaries");
            this.Value1.Add("DOCKET", "Docket");
            this.Value1.Add("DSMIS_DATE", "DismissalDate");
            this.Value1.Add("EMP_NO", "EmployerNo");
            this.Value1.Add("FORW_NO", "Forwarder");
            this.Value1.Add("GARN_DATE", "GarnishmentDate");
            this.Value1.Add("HAS_PHONE", "HasPhone");
            this.Value1.Add("HOME_OWN", "HomeOwner");
            this.Value1.Add("JMT_AMT", "JudgmentAmount");
            this.Value1.Add("JMT_DATE", "JudgmentDate");
            this.Value1.Add("LAST_LETTER", "LastLetter");
            this.Value1.Add("LAST_PMT", "LastPaymentDate");
            this.Value1.Add("MINSIFRATE", "MinSifRate");
            this.Value1.Add("MIN_SUIT", "MinSuitBalance");
            this.Value1.Add("NSW", "NonSuitWorthy");
            this.Value1.Add("OPEN_DATE", "OpenedDate");
            this.Value1.Add("PERCENT_PAID", "PercentPaid");
            this.Value1.Add("PMT_AMT", "LastPaymentAmount");
            this.Value1.Add("PREV_LETTER", "PreviousLetter");
            this.Value1.Add("PREV_LETTER_TYPE", "PreviousLetterType");
            this.Value1.Add("RET_MAIL", "ReturnMail");
            this.Value1.Add("SALES_NO", "SalesNo");
            this.Value1.Add("SERVICE_DATE", "ServiceDate");
            this.Value1.Add("SHER_NO", "SheriffNo");
            this.Value1.Add("SIFRATE", "SifRate");
            this.Value1.Add("SSN", "SSN");
            this.Value1.Add("STAT2_DATE", "Stat2Date");
            this.Value1.Add("STATE", "State");
            this.Value1.Add("STATUS_IS", "Status");
            this.Value1.Add("STATUS_NOT", "Status");
            this.Value1.Add("STATUTE_DATE", "StatuteDate");
            this.Value1.Add("SUIT_DATE", "SuitDate");
            this.Value1.Add("SUIT_SCORE", "SuitScore");
            this.Value1.Add("SW", "SuitWorthy");
            this.Value1.Add("VENUE_NO", "Venue");
            this.Value1.Add("MEDIA_PH", "MediaStatus");
            this.Value1.Add("MEDIA_COT", "MediaStatus");
            this.Value1.Add("MEDIA_TAC", "MediaStatus");
            this.Value1.Add("MEDIA_CO", "MediaStatus");
            this.Value1.Add("MEDIA_GS", "MediaStatus");
            this.Value1.Add("MEDIA_LD", "MediaStatus");
            this.Value1.Add("MEDIA_APP", "MediaStatus");
            this.Value1.Add("MEDIA_BOS", "MediaStatus");
            this.Value1.Add("MEDIA_OTH", "MediaStatus");
            this.Value1.Add("MEDIA_ALL", "MediaStatus");

            this.Value2.Add("ADVA", "");
            this.Value2.Add("AMT_PAID", "");
            this.Value2.Add("ANS_FILE", "");
            this.Value2.Add("BALANCE", "");
            this.Value2.Add("BANK_NO", "");
            this.Value2.Add("BK_CP", "");
            this.Value2.Add("BK_DATE", "");
            this.Value2.Add("BK_FILE", "");
            this.Value2.Add("C_PRIORITY", "");
            this.Value2.Add("CLASSIFICATION", "");
            this.Value2.Add("CLIENT", "");
            this.Value2.Add("COLLECTORS", "");
            this.Value2.Add("COST", "");
            this.Value2.Add("COUNTY", "");
            this.Value2.Add("DATE_RECD", "");
            this.Value2.Add("DEATH_DATE", "");
            this.Value2.Add("DIARY_IS", "");
            this.Value2.Add("DIARY_NOT", "");
            this.Value2.Add("DIARY_ALSO", "");
            this.Value2.Add("DOCKET", "");
            this.Value2.Add("DSMIS_DATE", "");
            this.Value2.Add("EMP_NO", "");
            this.Value2.Add("FORW_NO", "");
            this.Value2.Add("GARN_DATE", "");
            this.Value2.Add("HAS_PHONE", "");
            this.Value2.Add("HOME_OWN", "");
            this.Value2.Add("JMT_AMT", "");
            this.Value2.Add("JMT_DATE", "");
            this.Value2.Add("LAST_LETTER", "");
            this.Value2.Add("LAST_PMT", "");
            this.Value2.Add("MINSIFRATE", "");
            this.Value2.Add("MIN_SUIT", "");
            this.Value2.Add("NSW", "");
            this.Value2.Add("OPEN_DATE", "");
            this.Value2.Add("PERCENT_PAID", "");
            this.Value2.Add("PMT_AMT", "");
            this.Value2.Add("PREV_LETTER", "");
            this.Value2.Add("PREV_LETTER_TYPE", "PreviousLetterDate");
            this.Value2.Add("RET_MAIL", "");
            this.Value2.Add("SALES_NO", "");
            this.Value2.Add("SERVICE_DATE", "");
            this.Value2.Add("SHER_NO", "");
            this.Value2.Add("SIFRATE", "");
            this.Value2.Add("SSN", "");
            this.Value2.Add("STAT2_DATE", "");
            this.Value2.Add("STATE", "");
            this.Value2.Add("STATUS_IS", "StatusDate");
            this.Value2.Add("STATUS_NOT", "StatusDate");
            this.Value2.Add("STATUTE_DATE", "");
            this.Value2.Add("SUIT_DATE", "");
            this.Value2.Add("SUIT_SCORE", "");
            this.Value2.Add("SW", "");
            this.Value2.Add("VENUE_NO", "");
            this.Value2.Add("MEDIA_PH", "PaymentHistory");
            this.Value2.Add("MEDIA_COT", "ChainOfTitle");
            this.Value2.Add("MEDIA_TAC", "TermsAndConds");
            this.Value2.Add("MEDIA_CO", "ChargeOff");
            this.Value2.Add("MEDIA_GS", "Statement");
            this.Value2.Add("MEDIA_LD", "LoanDocs");
            this.Value2.Add("MEDIA_APP", "Application");
            this.Value2.Add("MEDIA_BOS", "BillOfSale");
            this.Value2.Add("MEDIA_OTH", "Other");
            this.Value2.Add("MEDIA_ALL", "AllMedia");
            Load();
        }

        public void Load()
        {
            using (SqlConnection conn = new SqlConnection(this.SQLConnection))
            {
                using (SqlCommand cmd = new SqlCommand(string.Format("SELECT [OPTION],[ISNOT],[COMPARE],[VALUE],[VALUE2] FROM {0} WHERE {1}", this.SQLOptions, this.SQLDistinguisherString), conn))
                {
                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        try
                        {
                            CParam Param = this.CriteriaItems[sdr["OPTION"].ToString()];
                            if (Param is CDateParam)
                            {
                                CDateParam CDP = (CDateParam)Param;
                                CDP.Param = (byte)sdr["COMPARE"];
                                CDP.ISNOT = (bool)sdr["ISNOT"];
                                CDP.Value = sdr["VALUE"] == DBNull.Value ? 0 : int.Parse(sdr["VALUE"].ToString());
                                CDP.Value2 = sdr["VALUE2"] == DBNull.Value ? 0 : int.Parse(sdr["VALUE2"].ToString());
                            }
                            else if (Param is CBasicParam)
                            {
                                CBasicParam CDP = (CBasicParam)Param;
                                CDP.Param = (byte)sdr["COMPARE"];
                                CDP.ISNOT = (bool)sdr["ISNOT"];
                                CDP.Value = sdr["VALUE"] == DBNull.Value ? 0 : int.Parse(sdr["VALUE"].ToString());
                            }
                            else if (Param is CClassParam)
                            {
                                CClassParam CDP = (CClassParam)Param;
                                CDP.Param = (byte)sdr["COMPARE"];
                                CDP.ISNOT = (bool)sdr["ISNOT"];
                                CDP.Value = sdr["VALUE"].ToString();
                            }
                            else if (Param is CSimpleParam || Param is CMediaParam)
                            {
                                Param.Param = (byte)sdr["COMPARE"];
                                Param.ISNOT = (bool)sdr["ISNOT"];
                            }
                            else if (Param is CBoolParam)
                            {
                                CBoolParam CBP = (CBoolParam)Param;
                                CBP.Param = (byte)sdr["COMPARE"];
                                CBP.ISNOT = (bool)sdr["ISNOT"];
                                CBP.Value = sdr["VALUE"] == DBNull.Value ? "" : sdr["VALUE"].ToString();
                                CBP.Value2 = sdr["VALUE2"] == DBNull.Value ? "" : sdr["VALUE2"].ToString();
                                using (SqlConnection conn2 = new SqlConnection(this.SQLConnection))
                                {
                                    using (SqlCommand cmd2 = new SqlCommand(string.Format("SELECT [CODE],[CONSTRAINT],[DAYS] FROM {0} WHERE CID={1}", this.SQLCodes, CBP.Value), conn2))
                                    {
                                        conn2.Open();
                                        SqlDataReader sdr2 = cmd2.ExecuteReader();
                                        while (sdr2.Read())
                                        {
                                            try
                                            {
                                                CBP.Elements.Add((int)sdr2["CODE"], sdr2["CONSTRAINT"] == DBNull.Value ? 0 : (byte)sdr2["CONSTRAINT"], sdr2["DAYS"] == DBNull.Value ? 0 : (int)sdr2["DAYS"]);
                                            }
                                            catch (Exception ex)
                                            {

                                            }
                                        }
                                    }
                                }
                            }
                            else if (Param is COptParam)
                            {
                                COptParam CBP = (COptParam)Param;
                                CBP.Param = (byte)sdr["COMPARE"];
                                CBP.ISNOT = (bool)sdr["ISNOT"];
                                CBP.Value = sdr["VALUE"] == DBNull.Value ? "" : sdr["VALUE"].ToString();
                                CBP.Value2 = sdr["VALUE2"] == DBNull.Value ? "" : sdr["VALUE2"].ToString();
                                using (SqlConnection conn2 = new SqlConnection(this.SQLConnection))
                                {
                                    using (SqlCommand cmd2 = new SqlCommand(string.Format("SELECT [CODE],[CONSTRAINT],[DAYS] FROM {0} WHERE CID={1}", this.SQLCodes, CBP.Value), conn2))
                                    {
                                        conn2.Open();
                                        SqlDataReader sdr2 = cmd2.ExecuteReader();
                                        while (sdr2.Read())
                                        {
                                            try
                                            {
                                                CBP.Values.Add((int)sdr2["CODE"]);
                                            }
                                            catch (Exception ex)
                                            {

                                            }
                                        }
                                    }
                                }
                            }
                            else if (Param is CStringParam)
                            {
                                CStringParam CBP = (CStringParam)Param;
                                CBP.Param = (byte)sdr["COMPARE"];
                                CBP.ISNOT = (bool)sdr["ISNOT"];
                                CBP.Value = sdr["VALUE"] == DBNull.Value ? "" : sdr["VALUE"].ToString();
                                CBP.Value2 = sdr["VALUE2"] == DBNull.Value ? "" : sdr["VALUE2"].ToString();
                                using (SqlConnection conn2 = new SqlConnection(this.SQLConnection))
                                {
                                    using (SqlCommand cmd2 = new SqlCommand(string.Format("SELECT [CODE] FROM {0} WHERE CID={1}", this.SQLCodeStrings, CBP.Value), conn2))
                                    {
                                        conn2.Open();
                                        SqlDataReader sdr2 = cmd2.ExecuteReader();
                                        while (sdr2.Read())
                                        {
                                            try
                                            {
                                                CBP.Values.Add(sdr2["CODE"].ToString());
                                            }
                                            catch (Exception ex)
                                            {

                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
        }

        public void Save()
        {
            using (SqlConnection conn = new SqlConnection(this.SQLConnection))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(string.Format("SELECT * FROM {0} WHERE {1}", this.SQLOptions, this.SQLDistinguisherString), conn))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr.Delete();
                    }

                    foreach (KeyValuePair<string, CParam> KVP in this.CriteriaItems)
                    {
                        if (KVP.Value.Param > 0)
                        {
                            DataRow dr = dt.NewRow();
                            dr["OPTION"] = KVP.Key;
                            dr["ISNOT"] = KVP.Value.ISNOT;
                            dr["COMPARE"] = KVP.Value.Param;
                            foreach (KeyValuePair<string, object> KP in this.SQLDistinguisher)
                            {
                                dr[KP.Key] = KP.Value;
                            }

                            if (KVP.Value is CDateParam)
                            {
                                dr["VALUE"] = ((CDateParam)KVP.Value).Value.ToString();
                                dr["VALUE2"] = ((CDateParam)KVP.Value).Value2.ToString();
                            }
                            else if (KVP.Value is CBasicParam)
                            {
                                dr["VALUE"] = ((CBasicParam)KVP.Value).Value.ToString();
                            }
                            else if (KVP.Value is CClassParam)
                            {
                                dr["VALUE"] = ((CClassParam)KVP.Value).Value;
                            }
                            else if (KVP.Value is CBoolParam)
                            {
                                dr["VALUE"] = ((CBoolParam)KVP.Value).Value;
                                dr["VALUE2"] = ((CBoolParam)KVP.Value).Value2;
                            }
                            else if (KVP.Value is COptParam)
                            {
                                dr["VALUE"] = ((COptParam)KVP.Value).Value;
                                dr["VALUE2"] = ((COptParam)KVP.Value).Value2;
                            }
                            else if (KVP.Value is CStringParam)
                            {
                                dr["VALUE"] = ((CStringParam)KVP.Value).Value;
                                dr["VALUE2"] = ((CStringParam)KVP.Value).Value2;
                            }
                            dt.Rows.Add(dr);
                        }
                    }

                    SqlCommandBuilder scb = new SqlCommandBuilder(sda);
                    sda.Update(dt);
                }
            }
        }

        public bool Evaluate(Evaluatees.Evaluatee Evaluatee)
        {
            foreach (KeyValuePair<string, CParam> CP in this.CriteriaItems)
            {
                if (CP.Value.Param > 0)
                {
                    object Val1 = null;
                    object Val2 = null;
                    try { Val1 = typeof(Evaluatees.Evaluatee).GetProperty(Value1[CP.Key]).GetValue(Evaluatee, null); }
                    catch { }
                    if (!string.IsNullOrEmpty(Value2[CP.Key]))
                    {
                        try { Val2 = typeof(Evaluatees.Evaluatee).GetProperty(Value2[CP.Key]).GetValue(Evaluatee, null); }
                        catch { }
                    }
                    if (!CP.Value.Evaluate(Val1, Val2)) return false;
                }
            }
            return true;
        }
        public bool Evaluate(Evaluatees2.Evaluatee2 Evaluatee)
        {
            foreach (KeyValuePair<string, CParam> CP in this.CriteriaItems)
            {
                if (CP.Value.Param > 0)
                {
                    object Val1 = null;
                    object Val2 = null;
                    try { Val1 = typeof(Evaluatees2.Evaluatee2).GetProperty(Value1[CP.Key]).GetValue(Evaluatee, null); }
                    catch { }
                    if (!string.IsNullOrEmpty(Value2[CP.Key]))
                    {
                        if (Val1 is Evaluatees2.MediaStatus)
                            Val2 = Val1.GetType().GetProperty(Value2[CP.Key]).GetValue(Val1, null);
                        else
                        {
                            try { Val2 = typeof(Evaluatees2.Evaluatee2).GetProperty(Value2[CP.Key]).GetValue(Evaluatee, null); }
                            catch { }
                        }
                    }
                    if (!CP.Value.Evaluate(Val1, Val2)) return false;
                }
            }
            return true;
        }
        public Eval EvaluateVerbose(Evaluatees.Evaluatee Evaluatee)
        {
            bool Success = true;
            StringBuilder Info = new StringBuilder();
            foreach (KeyValuePair<string, CParam> CP in this.CriteriaItems)
            {
                if (CP.Value.Param > 0)
                {
                    object Val1 = null;
                    object Val2 = null;
                    try { Val1 = typeof(Evaluatees.Evaluatee).GetProperty(Value1[CP.Key]).GetValue(Evaluatee, null); }
                    catch { }
                    if (!string.IsNullOrEmpty(Value2[CP.Key]))
                    {
                        try { Val2 = typeof(Evaluatees.Evaluatee).GetProperty(Value2[CP.Key]).GetValue(Evaluatee, null); }
                        catch { }
                    }

                    Eval Result = CP.Value.EvaluateVerbose(Val1, Val2);
                    Success &= Result.Success;

                    Info.Append(Result.Info + ", ");
                }
            }
            return new Eval(Success, Info.Length > 0 ? Info.ToString().Substring(0, Info.Length - 1) : "");
        }
        public Eval EvaluateVerbose(Evaluatees2.Evaluatee2 Evaluatee)
        {
            bool Success = true;
            StringBuilder Info = new StringBuilder();
            foreach (KeyValuePair<string, CParam> CP in this.CriteriaItems)
            {
                if (CP.Value.Param > 0)
                {
                    object Val1 = null;
                    object Val2 = null;
                    try { Val1 = typeof(Evaluatees2.Evaluatee2).GetProperty(Value1[CP.Key]).GetValue(Evaluatee, null); }
                    catch { }
                    if (!string.IsNullOrEmpty(Value2[CP.Key]))
                    {
                        if (Val1 is Evaluatees2.MediaStatus)
                            Val2 = Val1.GetType().GetProperty(Value2[CP.Key]).GetValue(Val1, null);
                        else
                        {
                            try { Val2 = typeof(Evaluatees2.Evaluatee2).GetProperty(Value2[CP.Key]).GetValue(Evaluatee, null); }
                            catch { }
                        }
                    }

                    Eval Result = CP.Value.EvaluateVerbose(Val1, Val2);
                    Success &= Result.Success;

                    Info.Append(Result.Info + ", ");
                }
            }
            return new Eval(Success, Info.Length > 0 ? Info.ToString().Substring(0, Info.Length - 1) : "");
        }

        public class Eval
        {
            public bool Success { get; private set; }
            public string Info { get; private set; }

            public Eval(bool Success, string Info)
            {
                this.Success = Success;
                this.Info = Info;
            }
        }

        public static string NumberCleanup(string theString, Type theType)
        {
            if (theType == typeof(int))
            {
                Regex regex = new Regex(@"\d");
                MatchCollection match = regex.Matches(theString);
                StringBuilder sb = new StringBuilder();
                foreach (Match m in match)
                {
                    if (m.Success)
                    {
                        sb.Append(m.Value);
                    }
                }
                return sb.ToString();
            }
            else if (theType == typeof(double))
            {
                Regex regex = new Regex(@"[\d\.]");
                MatchCollection match = regex.Matches(theString);
                StringBuilder sb = new StringBuilder();
                foreach (Match m in match)
                {
                    if (m.Success)
                    {
                        sb.Append(m.Value);
                    }
                }
                return sb.ToString();
            }
            return theString;
        }
        [Serializable]
        public abstract class CParam
        {
            public int Param { get; set; }
            public bool ISNOT { get; set; }
            public string DataName { get; set; }

            public CParam()
            {
                this.Param = 0;
                this.ISNOT = true;
                this.DataName = "";
            }

            public string NumberCleanup(string theString, Type theType)
            {
                if (theType == typeof(int))
                {
                    Regex regex = new Regex(@"\d");
                    MatchCollection match = regex.Matches(theString);
                    StringBuilder sb = new StringBuilder();
                    foreach (Match m in match)
                    {
                        if (m.Success)
                        {
                            sb.Append(m.Value);
                        }
                    }
                    return sb.ToString();
                }
                else if (theType == typeof(double))
                {
                    Regex regex = new Regex(@"[\d\.]");
                    MatchCollection match = regex.Matches(theString);
                    StringBuilder sb = new StringBuilder();
                    foreach (Match m in match)
                    {
                        if (m.Success)
                        {
                            sb.Append(m.Value);
                        }
                    }
                    return sb.ToString();
                }
                return theString;
            }
            public abstract bool Evaluate(object ValueGiven, object ConstraintGiven = null);
            public abstract Eval EvaluateVerbose(object ValueGiven, object ConstraintGiven = null);
            public override string ToString()
            {
                return GetDescription();
            }
            public abstract string GetDescription();
        }
        [Serializable]
        public class CDateParam : CParam
        {
            public int Value { get; set; }
            public int Value2 { get; set; }

            public CDateParam()
            {
                this.Value = 0;
                this.Value2 = 0;
            }
            public CDateParam(string DataName)
            {                
                this.DataName = DataName;
                this.ISNOT = true;
                this.Value = 0;
                this.Value2 = 0;
            }

            public override bool Evaluate(object DateGiven, object ConstraintGiven = null)
            {
                try
                {
                    if (DateGiven is DateTime || DateGiven is DateTime?)
                    {
                        switch (Param)
                        {
                            case 0: //Do Not Use
                                return true;
                            case 1: //Exists
                                return ISNOT;
                            case 2: //Does Not Exist
                                return !ISNOT;
                            case 3: //Greater Than _Value Days Ago
                                if (((DateTime)DateGiven).Date < DateTime.Now.AddDays(0 - Value))
                                {
                                    return ISNOT;
                                }
                                else
                                {
                                    return !ISNOT;
                                }
                            case 4: //Less Than _Value Days Ago
                                if (((DateTime)DateGiven).Date > DateTime.Now.AddDays(0 - Value))
                                {
                                    return ISNOT;
                                }
                                else
                                {
                                    return !ISNOT;
                                }
                            case 5: //Between _Value and _Value2 Days Ago
                                if (((DateTime)DateGiven).Date < DateTime.Now.AddDays(0 - Value) && ((DateTime)DateGiven).Date > DateTime.Now.AddDays(0 - Value2))
                                {
                                    return ISNOT;
                                }
                                else
                                {
                                    return !ISNOT;
                                }
                            case 6: //Exists More Than _Value Days Ago
                                if (((DateTime)DateGiven).Date < DateTime.Now.AddDays(0 - Value))
                                {
                                    return ISNOT;
                                }
                                else
                                {
                                    return !ISNOT;
                                }
                            default:
                                return true;
                        }
                    }
                    else if (DateGiven is DBNull || DateGiven == null)
                    {
                        switch (Param)
                        {
                            case 0:
                                return ISNOT;
                            case 1:
                                return !ISNOT;
                            case 2:
                                return ISNOT;
                            case 3:
                                return ISNOT;
                            case 4:
                                return !ISNOT;
                            case 5:
                                return !ISNOT;
                            case 6:
                                return !ISNOT;
                            default:
                                return ISNOT;
                        }
                    }
                    else
                        throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected Data Type \"" + DateGiven.GetType().ToString() + "\"");
                }
                catch (Exception ex) { throw new NotImplementedException(ex.Message); }
            }
            public override Eval EvaluateVerbose(object DateGiven, object ConstraintGiven = null)
            {
                bool Success = true;
                string Result = "";

                try
                {
                    if (DateGiven is DateTime || DateGiven is DateTime?)
                    {
                        switch (Param)
                        {
                            case 0: //Do Not Use
                                Success = true;
                                break;
                            case 1: //Exists
                                Success = ISNOT;
                                Result = " Exists";
                                break;
                            case 2: //Does Not Exist
                                Success = !ISNOT;
                                Result = " Exists";
                                break;
                            case 3: //Greater Than _Value Days Ago or Null
                                if (((DateTime)DateGiven).Date < DateTime.Now.AddDays(0 - Value))
                                {
                                    Success = ISNOT;
                                    Result = string.Format(" More Than {0} Days Ago ({1})", Value, ((DateTime)DateGiven).ToString("MM/dd/yyyy"));
                                    break;
                                }
                                else
                                {
                                    Success = !ISNOT;
                                    Result = string.Format(" Less Than {0} Days Ago ({1})", Value, ((DateTime)DateGiven).ToString("MM/dd/yyyy"));
                                    break;
                                }
                            case 4: //Less Than _Value Days Ago
                                if (((DateTime)DateGiven).Date > DateTime.Now.AddDays(0 - Value))
                                {
                                    Success = ISNOT;
                                    Result = string.Format(" Less Than {0} Days Ago ({1})", Value, ((DateTime)DateGiven).ToString("MM/dd/yyyy"));
                                    break;
                                }
                                else
                                {
                                    Success = !ISNOT;
                                    Result = string.Format(" More Than {0} Days Ago ({1})", Value, ((DateTime)DateGiven).ToString("MM/dd/yyyy"));
                                    break;
                                }
                            case 5: //Between _Value and _Value2 Days Ago
                                if (((DateTime)DateGiven).Date < DateTime.Now.AddDays(0 - Value) && ((DateTime)DateGiven).Date > DateTime.Now.AddDays(0 - Value2))
                                {
                                    Success = ISNOT;
                                    Result = string.Format(" Between {0} and {1} Days Ago ({2})", Value, Value2, ((DateTime)DateGiven).ToString("MM/dd/yyyy"));
                                    break;
                                }
                                else
                                {
                                    Success = !ISNOT;
                                    if (((DateTime)DateGiven).Date > DateTime.Now.AddDays(0 - Value))
                                        Result = string.Format(" Less Than {0} Days Ago ({1})", Value, ((DateTime)DateGiven).ToString("MM/dd/yyyy"));
                                    else
                                        Result = string.Format(" More Than {0} Days Ago ({1})", Value2, ((DateTime)DateGiven).ToString("MM/dd/yyyy"));
                                    break;
                                }
                            case 6: //Exists More Than _Value Days Ago
                                if (((DateTime)DateGiven).Date < DateTime.Now.AddDays(0 - Value))
                                {
                                    Success = ISNOT;
                                    Result = string.Format(" More Than {0} Days Ago ({1})", Value, ((DateTime)DateGiven).ToString("MM/dd/yyyy"));
                                    break;
                                }
                                else
                                {
                                    Success = !ISNOT;
                                    Result = string.Format(" Less Than {0} Days Ago ({1})", Value, ((DateTime)DateGiven).ToString("MM/dd/yyyy"));
                                    break;
                                }
                            default:
                                Success = true;
                                break;
                        }
                    }
                    else if (DateGiven is DBNull || DateGiven == null)
                    {
                        switch (Param)
                        {
                            case 0:
                                Success = ISNOT;
                                break;
                            case 1:
                                Success = !ISNOT;
                                Result = " Does Not Exist";
                                break;
                            case 2:
                                Success = ISNOT;
                                Result = " Does Not Exist";
                                break;
                            case 3:
                                Success = ISNOT;
                                Result = " Does Not Exist";
                                break;
                            case 4:
                                Success = !ISNOT;
                                Result = " Does Not Exist";
                                break;
                            case 5:
                                Success = !ISNOT;
                                Result = " Does Not Exist";
                                break;
                            case 6:
                                Success = !ISNOT;
                                Result = " Does Not Exist";
                                break;
                            default:
                                Success = ISNOT;
                                break;
                        }
                    }
                    else
                        throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected Data Type \"" + DateGiven.GetType().ToString() + "\"");

                    return new Eval(Success, DataName + Result);
                }
                catch (Exception ex) { throw new NotImplementedException(ex.Message); }
            }

            public override string GetDescription()
            {
                switch (this.Param)
                {
                    case 0: //Do Not Use
                        return string.Format("{0} - Not Used", this.DataName);
                    case 1: //Exists
                        return string.Format("{0} - Exists", this.DataName);
                    case 2: //Does Not Exist
                        return string.Format("{0} - Does Not Exist", this.DataName);
                    case 3: //Greater Than _Value Days Ago
                        return string.Format("{0} - Greater Than {1} Days Ago", this.DataName, this.Value.ToString());
                    case 4: //Less Than _Value Days Ago
                        return string.Format("{0} - Less Than {1} Days Ago", this.DataName, this.Value.ToString());
                    case 5: //Between _Value and _Value2 Days Ago
                        return string.Format("{0} - Between {1} and {2} Days Ago", this.DataName, this.Value.ToString(), this.Value2.ToString());
                    case 6: //Exists More Than _Value Days Ago
                        return string.Format("{0} - Exists More Than {1} Days Ago", this.DataName, this.Value.ToString());
                    default:
                        return string.Format("{0} - Unknown Setting", this.DataName);
                }
            }
        }
        [Serializable]
        public class CLetterParam : CDateParam
        {
            public int LetterType { get; set; }

            public CLetterParam()
            {
                this.LetterType = 0;
            }
            public CLetterParam(string DataName)
            {
                this.LetterType = 0;
                this.DataName = DataName;
            }

            public override bool Evaluate(object DateGiven, object LetterTypeGiven)
            {
                bool Result = base.Evaluate(DateGiven);
                if (Result)
                {
                    if (LetterTypeGiven is int)
                    {
                        if ((int)LetterTypeGiven == LetterType || LetterType == 0) return ISNOT;
                        else return !ISNOT;
                    }
                    else
                    {
                        throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected Data Type \"" + LetterTypeGiven.GetType().ToString() + "\"");
                    }
                }
                else
                {
                    return !ISNOT;
                }
            }
            public override Eval EvaluateVerbose(object DateGiven, object LetterTypeGiven)
            {
                Eval info = base.EvaluateVerbose(DateGiven);
                if (info.Success)
                {
                    if (LetterTypeGiven is int)
                    {
                        if ((int)LetterTypeGiven == LetterType || LetterType == 0)
                        {
                            return info;
                        }
                        else
                        {
                            return new Eval(!ISNOT, DataName + " Not Required Type");
                        }
                    }
                    else
                    {
                        throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected Data Type \"" + LetterTypeGiven.GetType().ToString() + "\"");
                    }
                }
                else
                {
                    return info;
                }
            }

            public override string  GetDescription()
            {
 	             return base.GetDescription();
            }
        }
        [Serializable]
        public class CBasicParam : CParam
        {
            public int Value { get; set; }

            public CBasicParam()
            {
                this.Value = 0;
            }
            public CBasicParam(string DataName)
            {
                this.Value = 0;
                this.DataName = DataName;
            }

            public override bool Evaluate(object ValueGiven, object ConstraintGiven = null)
            {
                double Value = 0;
                try
                {
                    if (ValueGiven is DBNull || ValueGiven == null)
                    {
                        switch (Param)
                        {
                            case 0:
                                return true;
                            case 6:
                                return ISNOT;
                            default:
                                return !ISNOT;
                        }
                    }
                    else if (ValueGiven is double || ValueGiven is int || ValueGiven is decimal || ValueGiven is string || ValueGiven is double? || ValueGiven is int? || ValueGiven is decimal?)
                    {
                        if (ValueGiven is double || ValueGiven is double?) Value = (double)ValueGiven;
                        else if (ValueGiven is int || ValueGiven is int?) Value = (int)ValueGiven;
                        else if (ValueGiven is decimal || ValueGiven is decimal?) Value = (double)(decimal)ValueGiven;
                        else if (ValueGiven is string) Value = double.Parse(NumberCleanup(ValueGiven.ToString(), typeof(double)));
                        switch (Param)
                        {
                            case 0: //Don't Care
                                return true;
                            case 1: //Equal
                                if (Value == this.Value) { return ISNOT; }
                                else { return !ISNOT; }
                            case 2: //Not Equal
                                if (Value != this.Value) { return ISNOT; }
                                else { return !ISNOT; }
                            case 3: //More Than
                                if (Value > this.Value) { return ISNOT; }
                                else { return !ISNOT; }
                            case 4: //Less Than
                                if (Value < this.Value) { return ISNOT; }
                                else { return !ISNOT; }
                            case 5: //Exists
                                return ISNOT;
                            case 6: //Does Not Exist
                                return !ISNOT;
                            case 7: //More Than or Equal To
                                if (Value >= this.Value) { return ISNOT; }
                                else { return !ISNOT; }
                            case 8: //Less Than or Equal To
                                if (Value <= this.Value) { return ISNOT; }
                                else { return !ISNOT; }
                            default:
                                return true;
                        }
                    }
                    else
                        throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected Data Type \"" + ValueGiven.GetType().ToString() + "\"");
                }
                catch (Exception ex) { throw new NotImplementedException(ex.Message); }
            }
            public override Eval EvaluateVerbose(object ValueGiven, object ConstraintGiven = null)
            {
                bool Success = true;
                string Result = "";
                double Value = 0;
                try
                {
                    if (ValueGiven is DBNull || ValueGiven == null)
                    {
                        switch (Param)
                        {
                            case 0:
                                Success = true;
                                break;
                            case 6:
                                Success = ISNOT;
                                Result = " Does Not Exist";
                                break;
                            default:
                                Success = !ISNOT;
                                Result = " Does Not Exist";
                                break;
                        }
                    }
                    else if (ValueGiven is double || ValueGiven is int || ValueGiven is decimal || ValueGiven is string || ValueGiven is double? || ValueGiven is int? || ValueGiven is decimal?)
                    {
                        if (ValueGiven is double || ValueGiven is double?) Value = (double)ValueGiven;
                        else if (ValueGiven is int || ValueGiven is int?) Value = (int)ValueGiven;
                        else if (ValueGiven is decimal || ValueGiven is decimal?) Value = (double)(decimal)ValueGiven;
                        else if (ValueGiven is string) Value = double.Parse(NumberCleanup(ValueGiven.ToString(), typeof(double)));
                        switch (Param)
                        {
                            case 0: //Don't Care
                                Success = true;
                                break;
                            case 1: //Equal
                                if (Value == this.Value)
                                {
                                    Success = ISNOT;
                                    Result = string.Format(" Equal To {0}", this.Value);
                                    break;
                                }
                                else
                                {
                                    Success = !ISNOT;
                                    Result = string.Format(" Not Equal To {0}", this.Value);
                                    break;
                                }
                            case 2: //Not Equal
                                if (Value != this.Value)
                                {
                                    Success = ISNOT;
                                    Result = string.Format(" Not Equal To {0}", this.Value);
                                    break;
                                }
                                else
                                {
                                    Success = !ISNOT;
                                    Result = string.Format(" Equal To {0}", this.Value);
                                    break;
                                }
                            case 3: //More Than
                                if (Value > this.Value)
                                {
                                    Success = ISNOT;
                                    Result = string.Format(" Greater Than {0}", this.Value);
                                    break;
                                }
                                else
                                {
                                    Success = !ISNOT;
                                    Result = string.Format(" Less Than {0}", this.Value);
                                    break;
                                }
                            case 4: //Less Than
                                if (Value < this.Value)
                                {
                                    Success = ISNOT;
                                    Result = string.Format(" Less Than {0}", this.Value);
                                    break;
                                }
                                else
                                {
                                    Success = !ISNOT;
                                    Result = string.Format(" Greater Than {0}", this.Value);
                                    break;
                                }
                            case 5: //Exists
                                Success = ISNOT;
                                Result = " Exists";
                                break;
                            case 6: //Does Not Exist
                                Success = !ISNOT;
                                Result = " Exists";
                                break;
                            case 7: //More Than or Equal To
                                if (Value >= this.Value)
                                {
                                    Success = ISNOT;
                                    Result = string.Format(" Greater Than or Equal To {0}", this.Value);
                                    break;
                                }
                                else
                                {
                                    Success = !ISNOT;
                                    Result = string.Format(" Less Than {0}", this.Value);
                                    break;
                                }
                            case 8: //Less Than or Equal To
                                if (Value <= this.Value)
                                {
                                    Success = ISNOT;
                                    Result = string.Format(" Less Than or Equal To {0}", this.Value);
                                    break;
                                }
                                else
                                {
                                    Success = !ISNOT;
                                    Result = string.Format(" Greater Than {0}", this.Value);
                                    break;
                                }
                            default:
                                Success = true;
                                break;
                        }
                    }
                    else
                        throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected Data Type \"" + ValueGiven.GetType().ToString() + "\"");
                }
                catch (Exception ex) { throw new NotImplementedException(ex.Message); }

                return new Eval(Success, DataName + Result);
            }

            public override string GetDescription()
            {
                switch (Param)
                {
                    case 0: //Don't Care
                        return string.Format("{0} - Not Used", this.DataName);
                    case 1: //Equal
                        return string.Format("{0} - Equals {1}", this.DataName, this.Value.ToString());
                    case 2: //Not Equal
                        return string.Format("{0} - Does Not Equal {1}", this.DataName, this.Value.ToString());
                    case 3: //More Than
                        return string.Format("{0} - More Than {1}", this.DataName, this.Value.ToString());
                    case 4: //Less Than
                        return string.Format("{0} - Less Than {1}", this.DataName, this.Value.ToString());
                    case 5: //Exists
                        return string.Format("{0} - Exists", this.DataName);
                    case 6: //Does Not Exist
                        return string.Format("{0} - Does Not Exist", this.DataName);
                    case 7: //More Than or Equal To
                        return string.Format("{0} - More Than or Equal to {1}", this.DataName, this.Value.ToString());
                    case 8: //Less Than or Equal To
                        return string.Format("{0} - Less Than or Equal to {1}", this.DataName, this.Value.ToString());
                    default:
                        return string.Format("{0} - Unknown Setting", this.DataName);
                }
            }
        }
        [Serializable]
        public class CClassParam : CParam
        {
            public string Value { get; set; }

            public CClassParam()
            {
                this.Value = "";
            }
            public CClassParam(string DataName)
            {
                this.Value = "";
                this.DataName = DataName;
            }

            public override bool Evaluate(object ValueGiven, object ConstraintGiven = null)
            {
                try
                {
                    string Value = "";
                    if (ValueGiven is string) Value = (string)ValueGiven;
                    else if (ValueGiven == null) Value = "";
                    if (ValueGiven is string || ValueGiven == null)
                    {
                        switch (Param)
                        {
                            case 0:
                                return ISNOT;
                            case 1:
                                if (Value == this.Value) return ISNOT;
                                else return !ISNOT;
                            case 2:
                                if (Value != this.Value) return ISNOT;
                                else return !ISNOT;
                            default:
                                return true;
                        }
                    }
                    else
                        throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected Data Type \"" + ValueGiven.GetType().ToString() + "\"");
                }
                catch (Exception ex)
                {
                    throw new NotImplementedException(ex.Message);
                }
            }
            public override Eval EvaluateVerbose(object ValueGiven, object ConstraintGiven = null)
            {
                bool Success = true;
                string Result = "";

                try
                {
                    string Value = "";
                    if (ValueGiven is string) Value = (string)ValueGiven;
                    else if (ValueGiven == null) Value = "";
                    if (ValueGiven is string || ValueGiven == null)
                    {
                        switch (Param)
                        {
                            case 0:
                                Success = true;
                                break;
                            case 1:
                                if (Value == this.Value)
                                {
                                    Success = ISNOT;
                                    Result = string.Format(" Equalt To \"{0}\"", this.Value);
                                    break;
                                }
                                else
                                {
                                    Success = !ISNOT;
                                    Result = string.Format(" Not Equal To \"{0}\"", this.Value);
                                    break;
                                }
                            case 2:
                                if (Value != this.Value)
                                {
                                    Success = ISNOT;
                                    Result = string.Format(" Not Equal To \"{0}\"", this.Value);
                                    break;
                                }
                                else
                                {
                                    Success = !ISNOT;
                                    Result = string.Format(" Equal To \"{0}\"", this.Value);
                                    break;
                                }
                            default:
                                Success = true;
                                break;
                        }
                    }
                    else
                        throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected Data Type \"" + ValueGiven.GetType().ToString() + "\"");
                }
                catch (Exception ex) { throw new NotImplementedException(ex.Message); }

                return new Eval(Success, DataName + Result);
            }

            public override string GetDescription()
            {
                switch (Param)
                {
                    case 0:
                        return string.Format("{0} - Not Used", this.DataName);
                    case 1:
                        return string.Format("{0} - Equal to \"{1}\"", this.DataName, this.Value);
                    case 2:
                        return string.Format("{0} - Not Equal to \"{1}\"", this.DataName, this.Value);
                    default:
                        return string.Format("{0} - Unknown Setting", this.DataName);
                }
            }
        }
        [Serializable]
        public class CSimpleParam : CParam
        {
            public CSimpleParam() { }
            public CSimpleParam(string DataName)
            {
                this.DataName = DataName;
            }

            public override bool Evaluate(object ValueGiven, object ConstraintGiven = null)
            {
                try
                {
                    string Value = "";
                    if (ValueGiven is string) Value = (string)ValueGiven;
                    if (ValueGiven is bool) { if ((bool)ValueGiven) Value = "True"; }
                    if (Value != "" && ValueGiven != null)
                    {
                        switch (Param)
                        {
                            case 0: //Do Not Use
                                return true;
                            case 1: //Exists
                                return ISNOT;
                            case 2: //Does Not Exist
                                return !ISNOT;
                            default:
                                return !ISNOT;
                        }
                    }
                    else
                    {
                        switch (Param)
                        {
                            case 0: //Do Not Use
                                return true;
                            case 1: //Exists
                                return !ISNOT;
                            case 2: //Does Not Exist
                                return ISNOT;
                            default:
                                return !ISNOT;
                        }
                    }
                }
                catch (Exception ex) { throw new NotImplementedException(ex.Message); }
            }
            public override Eval EvaluateVerbose(object ValueGiven, object ConstraintGiven = null)
            {
                bool Success = true;
                string Result = "";

                try
                {
                    string Value = "";
                    if (ValueGiven is string) Value = (string)ValueGiven;
                    if (ValueGiven is bool) { if ((bool)ValueGiven) Value = "True"; }
                    if (Value != "" && ValueGiven != null)
                    {
                        switch (Param)
                        {
                            case 0: //Do Not Use
                                Success = true;
                                break;
                            case 1: //Exists
                                Success = ISNOT;
                                Result = " Exists";
                                break;
                            case 2: //Does Not Exist
                                Success = !ISNOT;
                                Result = " Exists";
                                break;
                            default:
                                Success = !ISNOT;
                                break;
                        }
                    }
                    else
                    {
                        switch (Param)
                        {
                            case 0: //Do Not Use
                                Success = true;
                                break;
                            case 1: //Exists
                                Success = !ISNOT;
                                Result = " Does Not Exist";
                                break;
                            case 2: //Does Not Exist
                                Success = ISNOT;
                                Result = " Does Not Exist";
                                break;
                            default:
                                Success = !ISNOT;
                                break;
                        }
                    }
                }
                catch (Exception ex) { throw new NotImplementedException(ex.Message); }

                return new Eval(Success, DataName + Result);
            }

            public override string GetDescription()
            {
                switch (Param)
                {
                    case 0: //Do Not Use
                        return string.Format("{0} - Not Used", this.DataName);
                    case 1: //Exists
                        return string.Format("{0} - Exists", this.DataName);
                    case 2: //Does Not Exist
                        return string.Format("{0} - Does Not Exist", this.DataName);
                    default:
                        return string.Format("{0} - Unknown Setting", this.DataName);
                }
            }
        }
        [Serializable]
        public class CBoolParam : CParam
        {
            public string Value { get; set; }
            public string Value2 { get; set; }
            public CodeValue Elements { get; set; }

            public CBoolParam()
            {
                this.Value = "";
                this.Value2 = "";
                this.Elements = new CodeValue();
            }
            public CBoolParam(string DataName)
            {
                this.Value = "";
                this.Value2 = "";
                this.Elements = new CodeValue();
                this.DataName = DataName;
            }

            public override bool Evaluate(object ValueGiven, object DateGiven)
            {
                if (ValueGiven is List<Evaluatees.Evaluatee.DiaryCode>)
                {
                    foreach (Evaluatees.Evaluatee.DiaryCode D in (List<Evaluatees.Evaluatee.DiaryCode>)ValueGiven)
                    {
                        if (Elements.Values.Select(el => el.Code).Contains(D.Code))
                        {
                            if (this.Evaluate(D.Code, D.Date)) return ISNOT;
                        }
                    }
                    return !ISNOT;
                }
                else
                {
                    try
                    {
                        int Value = 0;
                        if (ValueGiven is int) Value = (int)ValueGiven;
                        else if (ValueGiven is double) Value = (int)(double)ValueGiven;
                        else if (ValueGiven is decimal) Value = (int)(decimal)ValueGiven;
                        else if (ValueGiven is string)
                        {
                            if (!int.TryParse(NumberCleanup((string)ValueGiven, typeof(int)), out Value))
                                throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected String Value \"" + (string)ValueGiven + "\"");
                        }
                        if (ValueGiven is double || ValueGiven is decimal || ValueGiven is int || ValueGiven is string)
                        {
                            if (DateGiven is DateTime)
                            {
                                bool Found = false;
                                bool NotFound = false;
                                for (int i = 0; i < Elements.Count; i++)
                                {
                                    switch (Elements[i].Param)
                                    {
                                        case 0: //Exists
                                            if (Value == Elements[i].Code) Found = true;
                                            break;
                                        case 1: //In The Past
                                            if (Value == Elements[i].Code && (DateTime)DateGiven < DateTime.Now.Date) Found = true;
                                            break;
                                        case 2: //In The Future
                                            if (Value == Elements[i].Code && (DateTime)DateGiven > DateTime.Now.Date) Found = true;
                                            break;
                                        case 3: //Over Days Away
                                            if (Value == Elements[i].Code && (DateTime)DateGiven > DateTime.Now.Date.AddDays((double)Elements[i].Value)) Found = true;
                                            break;
                                        case 4: //Under Days Away
                                            if (Value == Elements[i].Code && (DateTime)DateGiven <= DateTime.Now.Date.AddDays((double)Elements[i].Value)) Found = true;
                                            break;
                                        case 5: //Over Days Ago
                                            if (Value == Elements[i].Code && (DateTime)DateGiven < DateTime.Now.Date.AddDays(-(double)Elements[i].Value)) Found = true;
                                            break;
                                        case 6: //Under Days Ago
                                            if (Value == Elements[i].Code && (DateTime)DateGiven >= DateTime.Now.Date.AddDays(-(double)Elements[i].Value)) Found = true;
                                            break;
                                        case 7: // Doesn't Exist
                                            if (Value == Elements[i].Code) NotFound = true;
                                            else Found = true;
                                            break;
                                    }
                                }
                                if (Found && !NotFound) return ISNOT;
                                else return !ISNOT;
                            }
                            else if (DateGiven is DBNull || DateGiven == null)
                            {
                                bool Found = false;
                                bool NotFound = false;
                                for (int i = 0; i < Elements.Count; i++)
                                {
                                    switch (Elements[i].Param)
                                    {
                                        case 0: //Exists
                                            if (Value == Elements[i].Code) Found = true;
                                            break;
                                        case 7: //Doesn't Exist
                                            if (Value == Elements[i].Code) NotFound = true;
                                            else Found = true;
                                            break;
                                    }
                                }
                                if (Found && !NotFound) return ISNOT;
                                else return !ISNOT;
                            }
                            else
                                throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Date Due To Unexpected Data Type \"" + DateGiven.GetType().ToString() + "\"");
                        }
                        else
                            throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected Data Type \"" + ValueGiven.GetType().ToString() + "\"");
                    }
                    catch (Exception ex) { throw new NotImplementedException(ex.Message); }
                }
            }
            public override Eval EvaluateVerbose(object ValueGiven, object DateGiven)
            {
                if (ValueGiven is List<Evaluatees.Evaluatee.DiaryCode>)
                {
                    if (ISNOT)
                    {
                        foreach (Evaluatees.Evaluatee.DiaryCode D in (List<Evaluatees.Evaluatee.DiaryCode>)ValueGiven)
                        {
                            if (this.Elements.Values.Select(el => el.Code).Contains(D.Code))
                            {
                                Eval Res = this.EvaluateVerbose(D.Code, D.Date);
                                if (Res.Success) return new Eval(true, Res.Info);
                            }
                        }
                        return new Eval(false, "No Required Diaries Found");
                    }
                    else
                    {
                        bool Success = true;
                        StringBuilder Result = new StringBuilder(" Diaries Found (");
                        foreach (Evaluatees.Evaluatee.DiaryCode D in (List<Evaluatees.Evaluatee.DiaryCode>)ValueGiven)
                        {
                            if (this.Elements.Values.Select(el => el.Code).Contains(D.Code))
                            {
                                Eval Res = this.EvaluateVerbose(D.Code, D.Date);
                                if (!Res.Success)
                                {
                                    Success = false;
                                    Result.Append(D.Code.ToString() + ",");
                                }
                            }
                        }
                        if (Success)
                            return new Eval(true, "No Exclusionary Diaries Found");
                        else
                        {
                            return new Eval(false, Result.Length > 0 ? Result.ToString().Substring(0, Result.Length - 1) + ")" : "");
                        }
                    }
                }
                else
                {
                    bool Success = false;
                    string Result = "";

                    try
                    {
                        int Value = 0;
                        if (ValueGiven is int) Value = (int)ValueGiven;
                        else if (ValueGiven is double) Value = (int)(double)ValueGiven;
                        else if (ValueGiven is decimal) Value = (int)(decimal)ValueGiven;
                        else if (ValueGiven is string)
                        {
                            if (!int.TryParse(NumberCleanup((string)ValueGiven, typeof(int)), out Value))
                                throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected String Value \"" + (string)ValueGiven + "\"");
                        }
                        if (ValueGiven is double || ValueGiven is decimal || ValueGiven is int || ValueGiven is string)
                        {
                            if (DateGiven is DateTime)
                            {
                                bool Found = false;
                                bool NotFound = false;
                                for (int i = 0; i < Elements.Count; i++)
                                {
                                    switch (Elements[i].Param)
                                    {
                                        case 0: //Exists
                                            if (Value == Elements[i].Code) { Found = true; Result = " \"" + Elements[i].Code.ToString() + "\" Exists"; }
                                            break;
                                        case 1: //In The Past
                                            if (Value == Elements[i].Code && (DateTime)DateGiven < DateTime.Now.Date) { Found = true; Result = " \"" + Elements[i].Code.ToString() + "\" Exists In The Past"; }
                                            break;
                                        case 2: //In The Future
                                            if (Value == Elements[i].Code && (DateTime)DateGiven > DateTime.Now.Date) { Found = true; Result = " \"" + Elements[i].Code.ToString() + "\" Exists In The Future"; }
                                            break;
                                        case 3: //Over Days Away
                                            if (Value == Elements[i].Code && (DateTime)DateGiven > DateTime.Now.Date.AddDays((double)Elements[i].Value)) { Found = true; Result = " \"" + Elements[i].Code.ToString() + "\" Exists More Than " + Elements[i].Value.ToString() + " Days Away"; }
                                            break;
                                        case 4: //Under Days Away
                                            if (Value == Elements[i].Code && (DateTime)DateGiven <= DateTime.Now.Date.AddDays((double)Elements[i].Value)) { Found = true; Result = " \"" + Elements[i].Code.ToString() + "\" Exists Less Than " + Elements[i].Value.ToString() + " Days Away"; }
                                            break;
                                        case 5: //Over Days Ago
                                            if (Value == Elements[i].Code && (DateTime)DateGiven < DateTime.Now.Date.AddDays(-(double)Elements[i].Value)) { Found = true; Result = " \"" + Elements[i].Code.ToString() + "\" Exists More Than " + Elements[i].Value.ToString() + " Days Ago"; }
                                            break;
                                        case 6: //Under Days Ago
                                            if (Value == Elements[i].Code && (DateTime)DateGiven >= DateTime.Now.Date.AddDays(-(double)Elements[i].Value)) { Found = true; Result = " \"" + Elements[i].Code.ToString() + "\" Exists Less Than " + Elements[i].Value.ToString() + " Days Ago"; }
                                            break;
                                        case 7: // Doesn't Exist
                                            if (Value == Elements[i].Code) { NotFound = true; Result = " \"" + Elements[i].Code.ToString() + "\" Exists"; }
                                            else Found = true;
                                            break;
                                    }
                                }
                                if (Found && !NotFound) Success = ISNOT;
                                else Success = !ISNOT;
                            }
                            else if (DateGiven is DBNull || DateGiven == null)
                            {
                                bool Found = false;
                                bool NotFound = false;
                                for (int i = 0; i < Elements.Count; i++)
                                {
                                    switch (Elements[i].Param)
                                    {
                                        case 0: //Exists
                                            if (Value == Elements[i].Code) { Found = true; Result = " \"" + Elements[i].Code.ToString() + "\" Exists"; }
                                            break;
                                        case 7: //Doesn't Exist
                                            if (Value == Elements[i].Code) { NotFound = true; Result = " \"" + Elements[i].Code.ToString() + "\" Exists"; }
                                            else Found = true;
                                            break;
                                        default:
                                            Success = false;
                                            Result = " No Date Existed";
                                            break;
                                    }
                                }
                                if (Found && !NotFound) Success = ISNOT;
                                else Success = !ISNOT;
                            }
                            else
                                throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Date Due To Unexpected Data Type \"" + DateGiven.GetType().ToString() + "\"");
                        }
                        else
                            throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected Data Type \"" + ValueGiven.GetType().ToString() + "\"");
                    }
                    catch (Exception ex) { throw new NotImplementedException(ex.Message); }

                    if (Success != ISNOT) Result = " Not Found";
                    return new Eval(Success, DataName + Result);
                }
            }

            public override string GetDescription()
            {
                return string.Format("{0} List \"{1}\"", this.DataName, this.Value2);
            }

            [Serializable]
            public class CodeValue
            {
                public List<CodeElements> Values { get; private set; }
                public CodeElements this[int index]
                {
                    get { return Values[index]; }
                    set { Values[index] = value; }
                }
                public int Count
                {
                    get { return Values.Count; }
                }
                public CodeValue()
                {
                    this.Values = new List<CodeElements>();
                }
                public void Add(int Code, int Param, int Val)
                {
                    if (this.Values.Where(el => el.Code == Code).Count() == 0)
                    {
                        CodeElements temp = new CodeElements();
                        temp.Code = Code;
                        temp.Param = Param;
                        temp.Value = Val;
                        Values.Add(temp);
                    }
                }
                public void Remove(CodeElements CodeElements)
                {
                    this.Values.Remove(CodeElements);
                }
            }
            [Serializable]
            public class CodeElements
            {
                string[] Constraints = {"Exists",
                    "In The Past",
                    "In The Future",
                    "Over X Days Away",
                    "Under X Days Away",
                    "Over X Days Ago",
                    "Under X Days Ago",
                    "Doesn't Exist"};

                public int Code;
                public int Param;
                public int Value;

                public override string ToString()
                {
                    return string.Format("{0} {1}", this.Code, this.Constraints[Param].Replace("X", this.Value.ToString()));
                }
            }
        }
        [Serializable]
        public class COptParam : CParam
        {
            public List<int> Values { get; set; }
            public string Value { get; set; }
            public string Value2 { get; set; }

            public COptParam()
            {
                this.Values = new List<int>();
                this.Value = "";
                this.Value2 = "";
            }
            public COptParam(string DataName)
            {
                this.Values = new List<int>();
                this.Value = "";
                this.Value2 = "";
                this.DataName = DataName;
            }

            public override bool Evaluate(object ValueGiven, object ConstraintGiven = null)
            {
                int Value = 0;
                if (ValueGiven == null)
                {
                    switch (Param)
                    {
                        case 0: // Do Not Use
                            return ISNOT;
                        case 1: // Does Not Exist (value==0)
                            return ISNOT;
                        case 4: // Not In List
                            return ISNOT;
                        default:
                            return !ISNOT;
                    }
                }
                else if (ValueGiven is int || ValueGiven is double || ValueGiven is decimal)
                {
                    if (ValueGiven is int) Value = (int)ValueGiven;
                    else if (ValueGiven is double) Value = (int)(double)ValueGiven;
                    else if (ValueGiven is decimal) Value = (int)(decimal)ValueGiven;
                    switch (Param)
                    {
                        case 0: // Do Not Use
                            return ISNOT;
                        case 1: // Does Not Exist (value==0)
                            if (Value == 0) return ISNOT;
                            else return !ISNOT;
                        case 2: // Exists
                            if (Value > 0) return ISNOT;
                            else return !ISNOT;
                        case 3: // In List
                            foreach (int x in Values)
                            {
                                if (x == Value) return ISNOT;
                            }
                            return !ISNOT;
                        case 4: // Not In List
                            foreach (int x in Values)
                            {
                                if (x == Value) return !ISNOT;
                            }
                            return ISNOT;
                        default:
                            return true;
                    }
                }
                else
                {
                    throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected Data Type \"" + ValueGiven.GetType().ToString() + "\"");
                }
            }
            public override Eval EvaluateVerbose(object ValueGiven, object ConstraintGiven = null)
            {
                bool Success = false;
                string Result = "";

                int Value = 0;
                if (ValueGiven is int) Value = (int)ValueGiven;
                else if (ValueGiven is double) Value = (int)(double)ValueGiven;
                else if (ValueGiven is decimal) Value = (int)(decimal)ValueGiven;
                if (ValueGiven is int || ValueGiven is double || ValueGiven is decimal)
                {
                    switch (Param)
                    {
                        case 0: // Do Not Use
                            Success = ISNOT;
                            break;
                        case 1: // Does Not Exist (value==0)
                            if (Value == 0) { Success = ISNOT; Result = " Does Not Exist"; }
                            else { Success = !ISNOT; Result = " Exists"; }
                            break;
                        case 2: // Exists
                            if (Value > 0) { Success = ISNOT; Result = " Exists"; }
                            else { Success = !ISNOT; Result = " Does Not Exist"; }
                            break;
                        case 3: // In List
                            foreach (int x in Values)
                            {
                                if (x == Value) { Success = ISNOT; Result = " In List"; break; }
                            }
                            if (Result == "")
                            {
                                Success = !ISNOT;
                                Result = " Not In List";
                            }
                            break;
                        case 4: // Not In List
                            if (Value == 0) { Success = !ISNOT; Result = " Does Not Exist"; break; }
                            foreach (int x in Values)
                            {
                                if (x == Value) { Success = !ISNOT; Result = " In List"; break; }
                            }
                            if (Result == "")
                            {
                                Success = ISNOT;
                                Result = " Not In List";
                            }
                            break;
                    }
                }
                else if (ValueGiven is DBNull)
                {
                    switch (Param)
                    {
                        case 0: // Do Not Use
                            Success = ISNOT;
                            break;
                        case 1: // Does Not Exist (value==0)
                            Success = ISNOT;
                            Result = " Does Not Exist";
                            break;
                        case 4:
                            Success = !ISNOT;
                            Result = " Does Not Exist";
                            break;
                        default:
                            Success = !ISNOT;
                            Result = " Unknown Parameter";
                            break;
                    }
                }
                else
                {
                    throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected Data Type \"" + ValueGiven.GetType().ToString() + "\"");
                }

                return new Eval(Success, DataName + Result);
            }

            public override string GetDescription()
            {
                switch (Param)
                {
                    case 0: // Do Not Use
                        return string.Format("{0} - Not Used", this.DataName);
                    case 1: // Does Not Exist (value==0)
                        return string.Format("{0} - Does Not Exist", this.DataName);
                    case 2: // Exists
                        return string.Format("{0} - Exists", this.DataName);
                    case 3: // In List
                        return string.Format("{0} - In List \"{1}\"", this.DataName, this.Value2);
                    case 4: // Not In List
                        return string.Format("{0} - Not In List \"{1}\"", this.DataName, this.Value2);
                    default:
                        return string.Format("{0} - Unknown Setting", this.DataName);
                }
            }
        }
        [Serializable]
        public class CStringParam : CParam
        {
            public List<string> Values { get; set; }
            public string Value { get; set; }
            public string Value2 { get; set; }

            public CStringParam()
            {
                this.Values = new List<string>();
                this.Value = "";
                this.Value2 = "";
            }
            public CStringParam(string DataName)
            {
                this.Values = new List<string>();
                this.Value = "";
                this.Value2 = "";
                this.DataName = DataName;
            }

            public override bool Evaluate(object ValueGiven, object ConstraintGiven = null)
            {
                if (ValueGiven == null)
                {
                    return !ISNOT;
                }
                else if (ValueGiven is string)
                {
                    if (this.Values.Contains(ValueGiven.ToString()))
                        return ISNOT;
                    else
                        return !ISNOT;
                }
                else
                {
                    throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected Data Type \"" + ValueGiven.GetType().ToString() + "\"");
                }
            }
            public override Eval EvaluateVerbose(object ValueGiven, object ConstraintGiven = null)
            {
                bool Success = false;
                string Result = "";
                
                if (ValueGiven == null)
                {
                    Success = ISNOT;
                    Result = " Not In List";
                }
                else if (ValueGiven is string)
                {
                    if (this.Values.Contains(ValueGiven.ToString()))
                    {
                        Success = ISNOT;
                        Result = " In List";
                    }
                    else
                    {
                        Success = !ISNOT;
                        Result = " Not In List";
                    }
                }
                else
                {
                    throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected Data Type \"" + ValueGiven.GetType().ToString() + "\"");
                }

                return new Eval(Success, DataName + Result);
            }

            public override string GetDescription()
            {
                return string.Format("{0} - In List \"{1}\"", this.DataName, this.Value2);
            }
        }
        [Serializable]
        public class CMediaParam : CParam
        {
            public CMediaParam() { }
            public CMediaParam(string DataName) { this.DataName = DataName; }

            public override bool Evaluate(object ValueGiven, object ConstraintGiven = null)
            {
                if (ISNOT)
                    return (Evaluatees2.MediaStatus.Status)ConstraintGiven == (Evaluatees2.MediaStatus.Status)Enum.ToObject(typeof(Evaluatees2.MediaStatus.Status), this.Param);
                else
                    return (Evaluatees2.MediaStatus.Status)ConstraintGiven != (Evaluatees2.MediaStatus.Status)Enum.ToObject(typeof(Evaluatees2.MediaStatus.Status), this.Param);
            }

            public override Eval EvaluateVerbose(object ValueGiven, object ConstraintGiven = null)
            {
                bool Success;
                if (ISNOT)
                    Success = (Evaluatees2.MediaStatus.Status)ConstraintGiven == (Evaluatees2.MediaStatus.Status)Enum.ToObject(typeof(Evaluatees2.MediaStatus.Status), this.Param);
                else
                    Success = (Evaluatees2.MediaStatus.Status)ConstraintGiven != (Evaluatees2.MediaStatus.Status)Enum.ToObject(typeof(Evaluatees2.MediaStatus.Status), this.Param);
                return new Eval(Success, string.Format("{0} Media Status \"{1}\",", DataName, ConstraintGiven));
            }

            public override string GetDescription()
            {
                return string.Format("{0} - {1}", this.DataName, (Evaluatees2.MediaStatus.Status)Enum.ToObject(typeof(Evaluatees2.MediaStatus.Status), this.Param));
            }
        }
    }
}
