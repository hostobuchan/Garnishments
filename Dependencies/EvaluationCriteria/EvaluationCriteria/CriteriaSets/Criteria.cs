using EvaluationCriteria.CriteriaSets.CriteriaParameters;
using EvaluationCriteria.CriteriaSets.CriteriaParameters.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using static EvaluationCriteria.Utilities;

namespace EvaluationCriteria.CriteriaSets
{
    public class Criteria
    {
        protected Dictionary<string, object> SQLDistinguisher { get; set; }
        protected string SQLDistinguisherString { get { return SQLDistinguisherString(this.SQLDistinguisher); } }
        public List<string> CriteriaItemNames { get { return this.CriteriaItems.Select(el => el.Key).ToList(); } }
        public Dictionary<string, CParam> CriteriaItems { get; set; }
        protected Dictionary<string, string> Value1 { get; set; }
        protected Dictionary<string, string> Value2 { get; set; }

        protected Criteria(Dictionary<string, object> SQLDistinguisher, bool ignoreLoadSettings)
        {
            this.SQLDistinguisher = SQLDistinguisher;
            this.CriteriaItems = new Dictionary<string, CParam>();
            this.Value1 = new Dictionary<string, string>();
            this.Value2 = new Dictionary<string, string>();
        }
        public Criteria(Dictionary<string, object> SQLDistinguisher) : this(SQLDistinguisher, false)
        {
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
            this.CriteriaItems.Add("COST_CODE", new CBoolParam("Cost Code"));
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
            this.Value1.Add("COST_CODE", "CostCodes");
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
            this.Value2.Add("COST_CODE", "");
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

        public virtual void Load()
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.CriteriaDB))
            {
                using (SqlCommand cmd = new SqlCommand(string.Format("SELECT [OPTION],[ISNOT],[COMPARE],[VALUE],[VALUE2] FROM {0} WHERE {1}", Settings.Properties.SQLOptions, this.SQLDistinguisherString), conn))
                {
                    conn.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        try
                        {
                            CParam Param = this.CriteriaItems[sdr["OPTION"].ToString()];
                            Param.Param = (byte)sdr["COMPARE"];
                            Param.ISNOT = (bool)sdr["ISNOT"];

                            if (Param is CDateParam)
                            {
                                CDateParam CDP = (CDateParam)Param;
                                CDP.Value = sdr["VALUE"] == DBNull.Value ? 0 : int.Parse(sdr["VALUE"].ToString());
                                CDP.Value2 = sdr["VALUE2"] == DBNull.Value ? 0 : int.Parse(sdr["VALUE2"].ToString());
                            }
                            else if (Param is CBasicParam)
                            {
                                CBasicParam CDP = (CBasicParam)Param;
                                CDP.Value = sdr["VALUE"] == DBNull.Value ? 0 : int.Parse(sdr["VALUE"].ToString());
                            }
                            else if (Param is CClassParam)
                            {
                                CClassParam CDP = (CClassParam)Param;
                                CDP.Value = sdr["VALUE"].ToString();
                            }
                            else if (Param is CSimpleParam || Param is CMediaParam)
                            {
                            }
                            else if (Param is CBoolParam)
                            {
                                CBoolParam CBP = (CBoolParam)Param;
                                CBP.Value = sdr["VALUE"] == DBNull.Value ? "" : sdr["VALUE"].ToString();
                                CBP.Value2 = sdr["VALUE2"] == DBNull.Value ? "" : sdr["VALUE2"].ToString();
                                using (SqlConnection conn2 = new SqlConnection(Settings.Connections.CriteriaDB))
                                {
                                    using (SqlCommand cmd2 = new SqlCommand(string.Format("SELECT [CODE],[CONSTRAINT],[DAYS] FROM {0} WHERE CID={1}", Settings.Properties.SQLCodes, CBP.Value), conn2))
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
                                CBP.Value = sdr["VALUE"] == DBNull.Value ? "" : sdr["VALUE"].ToString();
                                CBP.Value2 = sdr["VALUE2"] == DBNull.Value ? "" : sdr["VALUE2"].ToString();
                                using (SqlConnection conn2 = new SqlConnection(Settings.Connections.CriteriaDB))
                                {
                                    using (SqlCommand cmd2 = new SqlCommand(string.Format("SELECT [CODE],[CONSTRAINT],[DAYS] FROM {0} WHERE CID={1}", Settings.Properties.SQLCodes, CBP.Value), conn2))
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
                                CBP.Value = sdr["VALUE"] == DBNull.Value ? "" : sdr["VALUE"].ToString();
                                CBP.Value2 = sdr["VALUE2"] == DBNull.Value ? "" : sdr["VALUE2"].ToString();
                                using (SqlConnection conn2 = new SqlConnection(Settings.Connections.CriteriaDB))
                                {
                                    using (SqlCommand cmd2 = new SqlCommand(string.Format("SELECT [CODE] FROM {0} WHERE CID={1}", Settings.Properties.SQLCodeStrings, CBP.Value), conn2))
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
                            else if (Param.GetType() == typeof(CEnumParam<>))
                            {

                            }
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
        }

        public virtual void Save()
        {
            using (SqlConnection conn = new SqlConnection(Settings.Connections.CriteriaDB))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(string.Format("SELECT * FROM {0} WHERE {1}", Settings.Properties.SQLOptions, this.SQLDistinguisherString), conn))
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
    }
}
