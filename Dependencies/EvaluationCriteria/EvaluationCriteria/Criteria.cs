using EvaluationCriteria.Accounts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;

namespace EvaluationCriteria
{
    [Serializable]
    public class Criteria
    {
        private byte[] _Disposition = new byte[1];
        private string SQL;
        private DataTable ST = new DataTable();
        public DataTable CL = new DataTable();
        private string CriteriaStr;
        private string ValuesStr;
        private string CodesStr;
        private string ClientStr;
        private string SqlQualifier = "";
        public COptParam ADVA = new COptParam("Adversarial Attorney");
        public CDateParam ANS_FILE = new CDateParam("Answer Filed");
        public CBasicParam BALANCE = new CBasicParam("Balance");
        public CBasicParam BANK_NO = new CBasicParam("Bank No.");
        public CDateParam BK_DATE = new CDateParam("Bankruptcy Date");
        public COptParam C_PRIORITY = new COptParam("C_Priority");
        public CClassParam CLASSIFICATION = new CClassParam("Classification");
        public CBoolParam CLIENT = new CBoolParam("Client");
        public CBoolParam COLLECTORS = new CBoolParam("Collector");
        public CBasicParam COST = new CBasicParam("Cost");
        public CDateParam DATE_RECD = new CDateParam("Received Date");
        public CDateParam DEATH_DATE = new CDateParam("Death Date");
        public CBoolParam DIARY_IS = new CBoolParam("Required Diary");
        public CBoolParam DIARY_NOT = new CBoolParam("Exclusionary Diary");
        public CSimpleParam DOCKET = new CSimpleParam("Docket Info");
        public CDateParam DSMIS_DATE = new CDateParam("Dismissal Date");
        public CBasicParam EMP_NO = new CBasicParam("Employer No.");
        public COptParam FORW_NO = new COptParam("Forwarder");
        public CDateParam GARN_DATE = new CDateParam("Garn Date");
        public CBasicParam JMT_AMT = new CBasicParam("Judgment Amount");
        public CDateParam JMT_DATE = new CDateParam("Judgment Date");
        public CDateParam LAST_PMT = new CDateParam("Last Payment");
        public CBasicParam MINSIFRATE = new CBasicParam("Min Sif Rate");
        public CBasicParam PERCENT_PAID = new CBasicParam("Percent Paid");
        public CBasicParam PMT_AMT = new CBasicParam("Last Payment Amount");
        public CClassParam PREV_LETTER = new CClassParam("Previous Letter");
        public CLetterParam PREV_LETTER_TYPE = new CLetterParam("Previous Letter Type");
        public CSimpleParam RET_MAIL = new CSimpleParam("Return Mail");
        public CBoolParam SALES_NO = new CBoolParam("SalesNo");
        public CDateParam SERVICE_DATE = new CDateParam("Service Date");
        public CBasicParam SHER_NO = new CBasicParam("Sheriff No.");
        public CBasicParam SIFRATE = new CBasicParam("Sif Rate");
        public CSimpleParam SSN = new CSimpleParam("SSN");
        public CDateParam STAT2_DATE = new CDateParam("Stat2 Date");
        public CBoolParam STATE = new CBoolParam("State");
        public CBoolParam STATUS_IS = new CBoolParam("Required Status Code");
        public CBoolParam STATUS_NOT = new CBoolParam("Exclusionary Status Code");
        public CDateParam STATUTE_DATE = new CDateParam("Statute Date");
        public CDateParam SUIT_DATE = new CDateParam("Suit Date");
        public CBasicParam SUIT_SCORE = new CBasicParam("Suit Score");
        public CBasicParam VENUE_NO = new CBasicParam("Venue");
        public bool Enabled;

        public Criteria(string theDB)
        {
            try
            {
                CriteriaStr = ConfigurationManager.AppSettings.Get("Criteria");
                ValuesStr = ConfigurationManager.AppSettings.Get("Values");
                CodesStr = ConfigurationManager.AppSettings.Get("Codes");
                ClientStr = ConfigurationManager.AppSettings.Get("Clients");
                SQL = ConfigurationManager.ConnectionStrings[theDB].ConnectionString;
            }
            catch
            {
                try
                {
                    CriteriaStr = System.Web.Configuration.WebConfigurationManager.AppSettings.Get("Criteria");
                    ValuesStr = System.Web.Configuration.WebConfigurationManager.AppSettings.Get("Values");
                    CodesStr = System.Web.Configuration.WebConfigurationManager.AppSettings.Get("Codes");
                    ClientStr = System.Web.Configuration.WebConfigurationManager.AppSettings.Get("Clients");
                    SQL = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[theDB].ConnectionString;
                }
                catch { }
            }
        }

        private void Initialize()
        {
            using (SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM States", SQL))
            {
                sda.Fill(ST);
            }
            if (ClientStr != "" && ClientStr != null)
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(ClientStr + SqlQualifier, SQL))
                {
                    sda.Fill(CL);
                }
            }
            using (SqlDataAdapter sda = new SqlDataAdapter(CriteriaStr + SqlQualifier, SQL))
            {
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    try { ADVA.Param = (int)dt.Rows[0]["ADVA"]; }
                    catch { ADVA.Param = 0; }
                    try { ANS_FILE.Param = (int)dt.Rows[0]["ANS_FILE"]; }
                    catch { ANS_FILE.Param = 0; }
                    try { BALANCE.Param = (int)dt.Rows[0]["BALANCE"]; }
                    catch { BALANCE.Param = 0; }
                    try { BANK_NO.Param = (int)dt.Rows[0]["BANK_NO"]; }
                    catch { BANK_NO.Param = 0; }
                    try { BK_DATE.Param = (int)dt.Rows[0]["BK_DATE"]; }
                    catch { BK_DATE.Param = 0; }
                    try { CLASSIFICATION.Param = (int)dt.Rows[0]["CLASSIFICATION"]; }
                    catch { CLASSIFICATION.Param = 0; }
                    try { CLIENT.Param = (bool)dt.Rows[0]["CLIENT"]; }
                    catch { CLIENT.Param = false; }
                    try { COLLECTORS.Param = (bool)dt.Rows[0]["COLLECTORS"]; }
                    catch { COLLECTORS.Param = false; }
                    try { COST.Param = (int)dt.Rows[0]["COST"]; }
                    catch { COST.Param = 0; }
                    try { DATE_RECD.Param = (int)dt.Rows[0]["DATE_RECD"]; }
                    catch { DATE_RECD.Param = 0; }
                    try { DEATH_DATE.Param = (int)dt.Rows[0]["DEATH_DATE"]; }
                    catch { DEATH_DATE.Param = 0; }
                    try { DIARY_IS.Param = (bool)dt.Rows[0]["DIARY_IS"]; }
                    catch { DIARY_IS.Param = false; }
                    try { DIARY_NOT.Param = (bool)dt.Rows[0]["DIARY_NOT"]; }
                    catch { DIARY_NOT.Param = false; }
                    try { DOCKET.Param = (int)dt.Rows[0]["DOCKET"]; }
                    catch { DOCKET.Param = 0; }
                    try { DSMIS_DATE.Param = (int)dt.Rows[0]["DSMIS_DATE"]; }
                    catch { DSMIS_DATE.Param = 0; }
                    try { EMP_NO.Param = (int)dt.Rows[0]["EMP_NO"]; }
                    catch { EMP_NO.Param = 0; }
                    try { FORW_NO.Param = (int)dt.Rows[0]["FORW_NO"]; }
                    catch { FORW_NO.Param = 0; }
                    try { GARN_DATE.Param = (int)dt.Rows[0]["GARN_DATE"]; }
                    catch { GARN_DATE.Param = 0; }
                    try { JMT_AMT.Param = (int)dt.Rows[0]["JMT_AMT"]; }
                    catch { JMT_AMT.Param = 0; }
                    try { JMT_DATE.Param = (int)dt.Rows[0]["JMT_DATE"]; }
                    catch { JMT_DATE.Param = 0; }
                    try { LAST_PMT.Param = (int)dt.Rows[0]["LAST_PMT"]; }
                    catch { LAST_PMT.Param = 0; }
                    try { PERCENT_PAID.Param = (int)dt.Rows[0]["PERCENT_PAID"]; }
                    catch { PERCENT_PAID.Param = 0; }
                    try { PMT_AMT.Param = (int)dt.Rows[0]["PMT_AMT"]; }
                    catch { PMT_AMT.Param = 0; }
                    try { PREV_LETTER_TYPE.Param = (int)dt.Rows[0]["PREV_LETTER_TYPE"]; }
                    catch { PREV_LETTER_TYPE.Param = 0; }
                    try { C_PRIORITY.Param = (int)dt.Rows[0]["PRIORITY"]; }
                    catch { C_PRIORITY.Param = 0; }
                    try { RET_MAIL.Param = (int)dt.Rows[0]["RET_MAIL"]; }
                    catch { RET_MAIL.Param = 0; }
                    try { SALES_NO.Param = (bool)dt.Rows[0]["SALES_NO"]; }
                    catch { SALES_NO.Param = false; }
                    try { SERVICE_DATE.Param = (int)dt.Rows[0]["SERVICE_DATE"]; }
                    catch { SERVICE_DATE.Param = 0; }
                    try { SSN.Param = (int)dt.Rows[0]["SSN"]; }
                    catch { SSN.Param = 0; }
                    try { STAT2_DATE.Param = (int)dt.Rows[0]["STAT2_DATE"]; }
                    catch { STAT2_DATE.Param = 0; }
                    try { STATE.Param = (bool)dt.Rows[0]["STATE"]; }
                    catch { STATE.Param = false; }
                    try { STATUS_IS.Param = (bool)dt.Rows[0]["STATUS_IS"]; }
                    catch { STATUS_IS.Param = false; }
                    try { STATUS_NOT.Param = (bool)dt.Rows[0]["STATUS_NOT"]; }
                    catch { STATUS_NOT.Param = false; }
                    try { STATUTE_DATE.Param = (int)dt.Rows[0]["STATUTE_DATE"]; }
                    catch { STATUTE_DATE.Param = 0; }
                    try { SUIT_DATE.Param = (int)dt.Rows[0]["SUIT_DATE"]; }
                    catch { SUIT_DATE.Param = 0; }
                    try { SUIT_SCORE.Param = (int)dt.Rows[0]["SUIT_SCORE"]; }
                    catch { SUIT_SCORE.Param = 0; }
                    try { Enabled = (bool)dt.Rows[0]["Enabled"]; }
                    catch { Enabled = false; }
                }
                else
                {
                    throw new RowNotInTableException();
                }
            }
            using (SqlDataAdapter sda = new SqlDataAdapter(ValuesStr + SqlQualifier, SQL))
            {
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    try { ANS_FILE.Value = (int)dt.Rows[0]["ANS_FILE"]; }
                    catch { }
                    try { ANS_FILE.Value2 = (int)dt.Rows[0]["ANS_FILE2"]; }
                    catch { }
                    try { BALANCE.Value = (int)dt.Rows[0]["BALANCE"]; }
                    catch { }
                    try { BANK_NO.Value = (int)dt.Rows[0]["BANK_NO"]; }
                    catch { BANK_NO.Value = 0; }
                    try { BK_DATE.Value = (int)dt.Rows[0]["BK_DATE"]; }
                    catch { }
                    try { BK_DATE.Value2 = (int)dt.Rows[0]["BK_DATE2"]; }
                    catch { }
                    try { CLASSIFICATION.Value = (string)dt.Rows[0]["CLASSIFICATION"]; }
                    catch { }
                    try { COST.Value = (int)dt.Rows[0]["COST"]; }
                    catch { }
                    try { DATE_RECD.Value = (int)dt.Rows[0]["DATE_RECD"]; }
                    catch { }
                    try { DATE_RECD.Value2 = (int)dt.Rows[0]["DATE_RECD2"]; }
                    catch { }
                    try { DEATH_DATE.Value = (int)dt.Rows[0]["DEATH_DATE"]; }
                    catch { DEATH_DATE.Value = 0; }
                    try { DEATH_DATE.Value2 = (int)dt.Rows[0]["DEATH_DATE2"]; }
                    catch { DEATH_DATE.Value2 = 0; }
                    try { DSMIS_DATE.Value = (int)dt.Rows[0]["DSMIS_DATE"]; }
                    catch { }
                    try { DSMIS_DATE.Value2 = (int)dt.Rows[0]["DSMIS_DATE2"]; }
                    catch { }
                    try { EMP_NO.Value = (int)dt.Rows[0]["EMP_NO"]; }
                    catch { EMP_NO.Value = 0; }
                    try { GARN_DATE.Value = (int)dt.Rows[0]["GARN_DATE"]; }
                    catch { }
                    try { GARN_DATE.Value2 = (int)dt.Rows[0]["GARN_DATE2"]; }
                    catch { }
                    try { JMT_AMT.Value = (int)dt.Rows[0]["JMT_AMT"]; }
                    catch { }
                    try { JMT_DATE.Value = (int)dt.Rows[0]["JMT_DATE"]; }
                    catch { }
                    try { JMT_DATE.Value2 = (int)dt.Rows[0]["JMT_DATE2"]; }
                    catch { }
                    try { LAST_PMT.Value = (int)dt.Rows[0]["LAST_PMT"]; }
                    catch { }
                    try { LAST_PMT.Value2 = (int)dt.Rows[0]["LAST_PMT2"]; }
                    catch { }
                    try { PERCENT_PAID.Value = (int)dt.Rows[0]["PERCENT_PAID"]; }
                    catch { }
                    try { PMT_AMT.Value = (int)dt.Rows[0]["PMT_AMT"]; }
                    catch { }
                    try { PREV_LETTER_TYPE.Value = (int)dt.Rows[0]["PREV_LETTER"]; }
                    catch { }
                    try { PREV_LETTER_TYPE.Value2 = (int)dt.Rows[0]["PREV_LETTER2"]; }
                    catch { }
                    try { PREV_LETTER_TYPE.LetterType = (int)dt.Rows[0]["PREV_LETTER_TYPE"]; }
                    catch { }
                    try { SERVICE_DATE.Value = (int)dt.Rows[0]["SERVICE_DATE"]; }
                    catch { }
                    try { SERVICE_DATE.Value2 = (int)dt.Rows[0]["SERVICE_DATE2"]; }
                    catch { }
                    try { STAT2_DATE.Value = (int)dt.Rows[0]["STAT2_DATE"]; }
                    catch { }
                    try { STAT2_DATE.Value2 = (int)dt.Rows[0]["STAT2_DATE2"]; }
                    catch { }
                    try { STATUTE_DATE.Value = (int)dt.Rows[0]["STATUTE_DATE"]; }
                    catch { }
                    try { STATUTE_DATE.Value2 = (int)dt.Rows[0]["STATUTE_DATE2"]; }
                    catch { }
                    try { SUIT_DATE.Value = (int)dt.Rows[0]["SUIT_DATE"]; }
                    catch { }
                    try { SUIT_DATE.Value2 = (int)dt.Rows[0]["SUIT_DATE2"]; }
                    catch { }
                    try { SUIT_SCORE.Value = (int)dt.Rows[0]["SUIT_SCORE"]; }
                    catch { }
                }
                else
                {
                    throw new RowNotInTableException();
                }
            }
            using (SqlDataAdapter sda = new SqlDataAdapter(CodesStr + SqlQualifier, SQL))
            {
                DataTable dt = new DataTable();
                sda.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    switch (((string)dt.Rows[i]["TYPE"]).Trim())
                    {
                        case "A": //Adva Number
                            ADVA.Values.Add((int)dt.Rows[i]["CODE"]);
                            break;
                        case "C": //Collector
                            COLLECTORS.Elements.Add((int)dt.Rows[i]["CODE"], (bool)dt.Rows[i]["ISNOT"], 0, 0);
                            break;
                        case "D": //Diary Code
                            if ((bool)dt.Rows[i]["ISNOT"])
                            {
                                try { DIARY_IS.Elements.Add((int)dt.Rows[i]["CODE"], (bool)dt.Rows[i]["ISNOT"], (int)dt.Rows[i]["CONSTR"], (int)dt.Rows[i]["DAYS"]); }
                                catch { DIARY_IS.Elements.Add((int)dt.Rows[i]["CODE"], (bool)dt.Rows[i]["ISNOT"], (int)dt.Rows[i]["CONSTR"], 0); }
                            }
                            else
                            {
                                try { DIARY_NOT.Elements.Add((int)dt.Rows[i]["CODE"], (bool)dt.Rows[i]["ISNOT"], (int)dt.Rows[i]["CONSTR"], (int)dt.Rows[i]["DAYS"]); }
                                catch { DIARY_NOT.Elements.Add((int)dt.Rows[i]["CODE"], (bool)dt.Rows[i]["ISNOT"], (int)dt.Rows[i]["CONSTR"], 0); }
                            }
                            break;
                        case "F": //Forwarder Number
                            FORW_NO.Values.Add((int)dt.Rows[i]["CODE"]);
                            break;
                        case "P": //Priority Score
                            C_PRIORITY.Values.Add((int)dt.Rows[i]["CODE"]);
                            break;
                        case "S": //Status Code
                            if ((bool)dt.Rows[i]["ISNOT"])
                            {
                                try { STATUS_IS.Elements.Add((int)dt.Rows[i]["CODE"], (bool)dt.Rows[i]["ISNOT"], (int)dt.Rows[i]["CONSTR"], (int)dt.Rows[i]["DAYS"]); }
                                catch { STATUS_IS.Elements.Add((int)dt.Rows[i]["CODE"], (bool)dt.Rows[i]["ISNOT"], (int)dt.Rows[i]["CONSTR"], 0); }
                            }
                            else
                            {
                                try { STATUS_NOT.Elements.Add((int)dt.Rows[i]["CODE"], (bool)dt.Rows[i]["ISNOT"], (int)dt.Rows[i]["CONSTR"], (int)dt.Rows[i]["DAYS"]); }
                                catch { STATUS_NOT.Elements.Add((int)dt.Rows[i]["CODE"], (bool)dt.Rows[i]["ISNOT"], (int)dt.Rows[i]["CONSTR"], 0); }
                            }
                            break;
                        case "T": //State
                            STATE.Elements.Add((int)dt.Rows[i]["CODE"], (bool)dt.Rows[i]["ISNOT"], 0, 0);
                            break;
                        case "V": //Sales Number
                            SALES_NO.Elements.Add((int)dt.Rows[i]["CODE"], (bool)dt.Rows[i]["ISNOT"], 0, 0);
                            break;
                    }
                }
            }
        }

        public bool Evaluate(DataRow dr, DataTable dd)
        {
            if (ADVA.Param > 0) if (!ADVA.Evaluate(dr["ADVA"])) return false;
            if (ANS_FILE.Param > 0) if (!ANS_FILE.Evaluate(dr["AF_DATE"])) return false;
            if (BALANCE.Param > 0) if (!BALANCE.Evaluate(dr["BALANCE"])) return false;
            if (BANK_NO.Param > 0) if (!BANK_NO.Evaluate(dr["EMP_NO"])) return false;
            if (BK_DATE.Param > 0) if (!BK_DATE.Evaluate(dr["BKCY_FILED_DATE"])) return false;
            if (CLASSIFICATION.Param > 0) if (!CLASSIFICATION.Evaluate(dr["CLASSIFICATION"])) return false;
            if (CLIENT.Param)
            {
                try
                {
                    if (dr["CL_Type"] is string)
                    { if (!CheckClientCrit((string)dr["CL_Type"])) return false; }
                    if (dr["CL_Type"] is DBNull)
                        return false;
                    if (!(dr["CL_Type"] is string) && !(dr["CL_Type"] is DBNull))
                        throw new NotImplementedException("Could Not Evaluate \"Client Type\" Due To Unexpected Data Type \"" + dr["CL_Type"].GetType().ToString() + "\"");
                }
                catch (Exception ex) { throw new NotImplementedException(ex.Message); }
            }
            if (COLLECTORS.Param) if (!COLLECTORS.Evaluate(dr["COLLECTOR"], null)) return false;
            if (COST.Param > 0) if (!COST.Evaluate(dr["COST_EXP"])) return false;
            if (DATE_RECD.Param > 0) if (!DATE_RECD.Evaluate(dr["DATE_RECD"])) return false;
            if (DIARY_IS.Param)
            {
                if (!CheckDiaryCrit((string)dr["FILENO"], dd, true, DIARY_IS.Elements)) return false;
            }
            if (DIARY_NOT.Param)
            {
                if (!CheckDiaryCrit((string)dr["FILENO"], dd, false, DIARY_NOT.Elements)) return false;
            }
            if (DOCKET.Param > 0) if (!DOCKET.Evaluate(dr["DOCKET_NO"])) return false;
            if (DSMIS_DATE.Param > 0) if (!DSMIS_DATE.Evaluate(dr["DSMIS_DATE"])) return false;
            if (EMP_NO.Param > 0) if (!EMP_NO.Evaluate(dr["EMP_NO"])) return false;
            if (FORW_NO.Param > 0) if (!FORW_NO.Evaluate(dr["FORW_NO"])) return false;
            if (GARN_DATE.Param > 0) if (!GARN_DATE.Evaluate(dr["GARN_DATE"])) return false;
            if (JMT_AMT.Param > 0) if (!JMT_AMT.Evaluate(dr["JMT_AMT"])) return false;
            if (JMT_DATE.Param > 0) if (!JMT_DATE.Evaluate(dr["JMT_DATE"])) return false;
            if (LAST_PMT.Param > 0) if (!LAST_PMT.Evaluate(dr["LPAYMNT_DATE"])) return false;
            if (PERCENT_PAID.Param > 0) if (!PERCENT_PAID.Evaluate(dr["PERCENT_PAID"])) return false;
            if (PMT_AMT.Param > 0) if (!PMT_AMT.Evaluate(dr["PMT_AMT"])) return false;
            if (RET_MAIL.Param > 0) if (!RET_MAIL.Evaluate(dr["RET_MAIL"])) return false;
            if (SALES_NO.Param) if (!SALES_NO.Evaluate(dr["SALES_NO"], null)) return false;
            if (SERVICE_DATE.Param > 0) if (!SERVICE_DATE.Evaluate(dr["SERVICE_DATE"])) return false;
            if (STAT2_DATE.Param > 0) if (!STAT2_DATE.Evaluate(dr["STAT2_DATE"])) return false;
            if (STATE.Param)
            {
                if (dr["STATE"] is string)
                { if (!CheckStateCrit((string)dr["STATE"], STATE.Elements)) return false; }
                else
                    if (!CheckStateCrit("", STATE.Elements)) return false;
            }
            if (STATUS_IS.Param) if (!STATUS_IS.Evaluate(dr["STAT1_CODE"], dr["STAT1_DATE"])) return false;
            if (STATUS_NOT.Param) if (!STATUS_NOT.Evaluate(dr["STAT1_CODE"], dr["STAT1_DATE"])) return false;
            if (STATUTE_DATE.Param > 0) if (!STATUTE_DATE.Evaluate(dr["STATUTE_DATE"])) return false;
            if (SUIT_DATE.Param > 0) if (!SUIT_DATE.Evaluate(dr["SUIT_DATE"])) return false;
            if (SUIT_SCORE.Param > 0) if (!SUIT_SCORE.Evaluate(dr["C_PRIORITY"])) return false;

            return true;
        }
        public bool Evaluate(Evaluatees.Evaluatee EvalMe)
        {
            if (ADVA.Param > 0) if (!ADVA.Evaluate(EvalMe.AdversarialAttorney)) return false;
            if (ANS_FILE.Param > 0) if (!ANS_FILE.Evaluate(EvalMe.AnswerFiled)) return false;
            if (BALANCE.Param > 0) if (!BALANCE.Evaluate(EvalMe.Balance)) return false;
            if (BANK_NO.Param > 0) if (!BANK_NO.Evaluate(EvalMe.BankNo)) return false;
            if (BK_DATE.Param > 0) if (!BK_DATE.Evaluate(EvalMe.BankruptcyDate)) return false;
            if (CLASSIFICATION.Param > 0) if (!CLASSIFICATION.Evaluate(EvalMe.Classification)) return false;
            if (CLIENT.Param) if (!CheckClientCrit(EvalMe.ClientType)) return false;
            if (COLLECTORS.Param) if (!COLLECTORS.Evaluate(EvalMe.Collector, null)) return false;
            if (COST.Param > 0) if (!COST.Evaluate(EvalMe.Cost)) return false;
            if (DATE_RECD.Param > 0) if (!DATE_RECD.Evaluate(EvalMe.DateReceived)) return false;
            if (DEATH_DATE.Param > 0) if (!DEATH_DATE.Evaluate(EvalMe.DeathDate)) return false;
            if (DIARY_IS.Param)
            {
                bool Found = false;
                foreach (DiaryCode C in EvalMe.Diaries)
                {
                    if (DIARY_IS.Evaluate(C.Code, C.Date)) Found = true;
                }
                if (!Found) return false;
            }
            if (DIARY_NOT.Param)
            {
                bool Found = false;
                foreach (DiaryCode C in EvalMe.Diaries)
                {
                    if (!DIARY_NOT.Evaluate(C.Code, C.Date)) Found = true;
                }
                if (Found) return false;
            }
            if (DOCKET.Param > 0) if (!DOCKET.Evaluate(EvalMe.Docket)) return false;
            if (DSMIS_DATE.Param > 0) if (!DSMIS_DATE.Evaluate(EvalMe.DismissalDate)) return false;
            if (EMP_NO.Param > 0) if (!EMP_NO.Evaluate(EvalMe.EmployerNo)) return false;
            if (FORW_NO.Param > 0) if (!FORW_NO.Evaluate(EvalMe.Forwarder)) return false;
            if (GARN_DATE.Param > 0) if (!GARN_DATE.Evaluate(EvalMe.GarnishmentDate)) return false;
            if (JMT_AMT.Param > 0) if (!JMT_AMT.Evaluate(EvalMe.JudgmentAmount)) return false;
            if (JMT_DATE.Param > 0) if (!JMT_DATE.Evaluate(EvalMe.JudgmentDate)) return false;
            if (LAST_PMT.Param > 0) if (!LAST_PMT.Evaluate(EvalMe.LastPaymentDate)) return false;
            if (MINSIFRATE.Param > 0) if (!MINSIFRATE.Evaluate(EvalMe.MinSifRate)) return false;
            if (PERCENT_PAID.Param > 0) if (!PERCENT_PAID.Evaluate(EvalMe.PercentPaid)) return false;
            if (PMT_AMT.Param > 0) if (!PMT_AMT.Evaluate(EvalMe.LastPaymentAmount)) return false;
            if (PREV_LETTER.Param > 0) if (!PREV_LETTER.Evaluate(EvalMe.PreviousLetter)) return false;
            if (PREV_LETTER_TYPE.Param > 0) if (!PREV_LETTER_TYPE.Evaluate(EvalMe.PreviousLetterDate, EvalMe.PreviousLetterType)) return false;
            if (RET_MAIL.Param > 0) if (!RET_MAIL.Evaluate(EvalMe.ReturnMail)) return false;
            if (SALES_NO.Param) if (!SALES_NO.Evaluate(EvalMe.SalesNo, null)) return false;
            if (SERVICE_DATE.Param > 0) if (!SERVICE_DATE.Evaluate(EvalMe.ServiceDate)) return false;
            if (SIFRATE.Param > 0) if (!SIFRATE.Evaluate(EvalMe.SifRate)) return false;
            if (SSN.Param > 0) if (!SSN.Evaluate(EvalMe.SSN)) return false;
            if (STAT2_DATE.Param > 0) if (!STAT2_DATE.Evaluate(EvalMe.Stat2Date)) return false;
            if (STATUS_IS.Param) if (!STATUS_IS.Evaluate(EvalMe.Status, EvalMe.StatusDate)) return false;
            if (STATUS_NOT.Param) if (!STATUS_NOT.Evaluate(EvalMe.Status, EvalMe.StatusDate)) return false;
            if (STATUTE_DATE.Param > 0) if (!STATUTE_DATE.Evaluate(EvalMe.StatuteDate)) return false;
            if (SUIT_DATE.Param > 0) if (!SUIT_DATE.Evaluate(EvalMe.SuitDate)) return false;
            if (SUIT_SCORE.Param > 0) if (!SUIT_SCORE.Evaluate(EvalMe.SuitScore)) return false;
            if (STATE.Param) if (!CheckStateCrit(EvalMe.State, STATE.Elements)) return false;

            return true;
        }
        public bool EvaluateOR(Evaluatees.Evaluatee EvalMe)
        {
            if (DIARY_NOT.Param)
            {
                bool Found = false;
                foreach (DiaryCode C in EvalMe.Diaries)
                {
                    if (!DIARY_NOT.Evaluate(C.Code, C.Date)) Found = true;
                }
                if (Found) return false;
            }
            if (STAT2_DATE.Param > 0) if (!STAT2_DATE.Evaluate(EvalMe.Stat2Date)) return false;
            if (FORW_NO.Param > 0) if (!FORW_NO.Evaluate(EvalMe.Forwarder)) return false;
            if (ADVA.Param > 0) if (ADVA.Evaluate(EvalMe.AdversarialAttorney)) return true;
            if (ANS_FILE.Param > 0) if (ANS_FILE.Evaluate(EvalMe.AnswerFiled)) return true;
            if (BK_DATE.Param > 0) if (BK_DATE.Evaluate(EvalMe.BankruptcyDate)) return true;
            if (DATE_RECD.Param > 0) if (DATE_RECD.Evaluate(EvalMe.DateReceived)) return true;
            if (DSMIS_DATE.Param > 0) if (DSMIS_DATE.Evaluate(EvalMe.DismissalDate)) return true;
            if (GARN_DATE.Param > 0) if (GARN_DATE.Evaluate(EvalMe.GarnishmentDate)) return true;
            if (JMT_DATE.Param > 0) if (JMT_DATE.Evaluate(EvalMe.JudgmentDate)) return true;
            if (LAST_PMT.Param > 0) if (LAST_PMT.Evaluate(EvalMe.LastPaymentDate)) return true;
            if (PREV_LETTER.Param > 0) if (PREV_LETTER.Evaluate(EvalMe.PreviousLetter)) return true;
            if (PREV_LETTER_TYPE.Param > 0) if (PREV_LETTER_TYPE.Evaluate(EvalMe.PreviousLetterDate, EvalMe.PreviousLetterType)) return true;
            if (MINSIFRATE.Param > 0) if (MINSIFRATE.Evaluate(EvalMe.MinSifRate)) return true;
            if (SIFRATE.Param > 0) if (SIFRATE.Evaluate(EvalMe.SifRate)) return true;
            if (SERVICE_DATE.Param > 0) if (SERVICE_DATE.Evaluate(EvalMe.ServiceDate)) return true;
            if (STATUTE_DATE.Param > 0) if (STATUTE_DATE.Evaluate(EvalMe.StatuteDate)) return true;
            if (SUIT_DATE.Param > 0) if (SUIT_DATE.Evaluate(EvalMe.SuitDate)) return true;
            if (BALANCE.Param > 0) if (BALANCE.Evaluate(EvalMe.Balance)) return true;
            if (COST.Param > 0) if (COST.Evaluate(EvalMe.Cost)) return true;
            if (DOCKET.Param > 0) if (DOCKET.Evaluate(EvalMe.Docket)) return true;
            if (JMT_AMT.Param > 0) if (JMT_AMT.Evaluate(EvalMe.JudgmentAmount)) return true;
            if (PERCENT_PAID.Param > 0) if (PERCENT_PAID.Evaluate(EvalMe.PercentPaid)) return true;
            if (PMT_AMT.Param > 0) if (PMT_AMT.Evaluate(EvalMe.LastPaymentAmount)) return true;
            if (SUIT_SCORE.Param > 0) if (SUIT_SCORE.Evaluate(EvalMe.SuitScore)) return true;
            if (CLASSIFICATION.Param > 0) if (CLASSIFICATION.Evaluate(EvalMe.Classification)) return true;
            if (CLIENT.Param) if (CheckClientCrit(EvalMe.ClientType)) return true;
            if (COLLECTORS.Param) if (COLLECTORS.Evaluate(EvalMe.Collector, null)) return true;
            if (SALES_NO.Param) if (SALES_NO.Evaluate(EvalMe.SalesNo, null)) return true;
            if (STATUS_IS.Param) if (STATUS_IS.Evaluate(EvalMe.Status, EvalMe.StatusDate)) return true;
            if (STATUS_NOT.Param) if (STATUS_NOT.Evaluate(EvalMe.Status, EvalMe.StatusDate)) return true;
            if (DIARY_IS.Param)
            {
                bool Found = false;
                foreach (DiaryCode C in EvalMe.Diaries)
                {
                    if (DIARY_IS.Evaluate(C.Code, C.Date)) Found = true;
                }
                if (Found) return true;
            }
            if (STATE.Param) if (CheckStateCrit(EvalMe.State, STATE.Elements)) return true;
            if (RET_MAIL.Param > 0) if (RET_MAIL.Evaluate(EvalMe.ReturnMail)) return true;

            return false;
        }

        public Eval EvaluateVerbose(DataRow dr, DataTable dd)
        {
            bool Success = true;
            StringBuilder Info = new StringBuilder();

            if (ADVA.Param > 0)
            {
                Eval temp = ADVA.EvaluateVerbose(dr["ADVA"]);
                if (!temp.Success)
                {
                    Success = false;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (ANS_FILE.Param > 0)
            {
                Eval temp = ANS_FILE.EvaluateVerbose(dr["AF_DATE"]);
                if (!temp.Success)
                {
                    Success = false;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (BK_DATE.Param > 0)
            {
                Eval temp = BK_DATE.EvaluateVerbose(dr["BKCY_FILED_DATE"]);
                if (!temp.Success)
                {
                    Success = false;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (DATE_RECD.Param > 0)
            {
                Eval temp = DATE_RECD.EvaluateVerbose(dr["DATE_RECD"]);
                if (!temp.Success)
                {
                    Success = false;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (DSMIS_DATE.Param > 0)
            {
                Eval temp = DSMIS_DATE.EvaluateVerbose(dr["DSMIS_DATE"]);
                if (!temp.Success)
                {
                    Success = false;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (FORW_NO.Param > 0)
            {
                Eval temp = FORW_NO.EvaluateVerbose(dr["FORW_NO"]);
                if (!temp.Success)
                {
                    Success = false;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (GARN_DATE.Param > 0)
            {
                Eval temp = GARN_DATE.EvaluateVerbose(dr["GARN_DATE"]);
                if (!temp.Success)
                {
                    Success = false;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (JMT_DATE.Param > 0)
            {
                Eval temp = JMT_DATE.EvaluateVerbose(dr["JMT_DATE"]);
                if (!temp.Success)
                {
                    Success = false;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (LAST_PMT.Param > 0)
            {
                Eval temp = LAST_PMT.EvaluateVerbose(dr["LPAYMNT_DATE"]);
                if (!temp.Success)
                {
                    Success = false;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (SERVICE_DATE.Param > 0)
            {
                Eval temp = SERVICE_DATE.EvaluateVerbose(dr["SERVICE_DATE"]);
                if (!temp.Success)
                {
                    Success = false;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (STAT2_DATE.Param > 0)
            {
                Eval temp = STAT2_DATE.EvaluateVerbose(dr["STAT2_DATE"]);
                if (!temp.Success)
                {
                    Success = false;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (STATUTE_DATE.Param > 0)
            {
                Eval temp = STATUTE_DATE.EvaluateVerbose(dr["STATUTE_DATE"]);
                if (!temp.Success)
                {
                    Success = false;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (SUIT_DATE.Param > 0)
            {
                Eval temp = SUIT_DATE.EvaluateVerbose(dr["SUIT_DATE"]);
                if (!temp.Success)
                {
                    Success = false;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (BALANCE.Param > 0)
            {
                Eval temp = BALANCE.EvaluateVerbose(dr["BALANCE"]);
                if (!temp.Success)
                {
                    Success = false;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (COST.Param > 0)
            {
                Eval temp = COST.EvaluateVerbose(dr["COST_EXP"]);
                if (!temp.Success)
                {
                    Success = false;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (DOCKET.Param > 0)
            {
                Eval temp = DOCKET.EvaluateVerbose(dr["DOCKET_NO"]);
                if (!temp.Success)
                {
                    Success = false;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (JMT_AMT.Param > 0)
            {
                Eval temp = JMT_AMT.EvaluateVerbose(dr["JMT_AMT"]);
                if (!temp.Success)
                {
                    Success = false;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (PERCENT_PAID.Param > 0)
            {
                Eval temp = PERCENT_PAID.EvaluateVerbose(dr["PERCENT_PAID"]);
                if (!temp.Success)
                {
                    Success = false;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (PMT_AMT.Param > 0)
            {
                Eval temp = PMT_AMT.EvaluateVerbose(dr["PMT_AMT"]);
                if (!temp.Success)
                {
                    Success = false;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (SUIT_SCORE.Param > 0)
            {
                Eval temp = SUIT_SCORE.EvaluateVerbose(dr["C_PRIORITY"]);
                if (!temp.Success)
                {
                    Success = false;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (CLASSIFICATION.Param > 0)
            {
                Eval temp = CLASSIFICATION.EvaluateVerbose(dr["CLASSIFICATION"]);
                if (!temp.Success)
                {
                    Success = false;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (CLIENT.Param)
            {
                try
                {
                    if (dr["CL_Type"] is string)
                    {
                        if (!CheckClientCrit((string)dr["CL_Type"]))
                        {
                            Success = false;
                            Info.Append("Client Type Req. Not Met, ");
                        }
                    }
                    if (dr["CL_Type"] is DBNull)
                    {
                        Success = false;
                        Info.Append("Client Type Req. Not Met, ");
                    }
                    if (!(dr["CL_Type"] is string) && !(dr["CL_Type"] is DBNull))
                    {
                        Success = false;
                        Info.Append("Could Not Evaluate \"Client Type\" Due To Unexpected Data Type \"" + dr["CL_Type"].GetType().ToString() + "\", ");
                    }
                }
                catch (Exception ex) { throw new NotImplementedException(ex.Message); }
            }
            if (COLLECTORS.Param)
            {
                Eval temp = COLLECTORS.EvaluateVerbose(dr["COLLECTOR"], null);
                if (!temp.Success)
                {
                    Success = false;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (SALES_NO.Param)
            {
                Eval temp = SALES_NO.EvaluateVerbose(dr["SALES_NO"], null);
                if (!temp.Success)
                {
                    Success = false;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (STATUS_IS.Param)
            {
                Eval temp = STATUS_IS.EvaluateVerbose(dr["STAT1_CODE"], dr["STAT1_DATE"]);
                if (!temp.Success)
                {
                    Success = false;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (STATUS_NOT.Param)
            {
                Eval temp = STATUS_NOT.EvaluateVerbose(dr["STAT1_CODE"], dr["STAT1_DATE"]);
                if (!temp.Success)
                {
                    Success = false;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (DIARY_IS.Param)
            {
                if (!CheckDiaryCrit((string)dr["FILENO"], dd, true, DIARY_IS.Elements))
                {
                    Success = false;
                    Info.Append("Diary Code Reqs. Not Met, ");
                }
            }
            if (DIARY_NOT.Param)
            {
                bool Found = false;
                string Results = "";
                foreach (DataRow mdr in dd.Rows)
                {
                    if (!DIARY_NOT.Evaluate((double)mdr["CODE"], (DateTime)mdr["DATE"])) { Found = true; Results += mdr["CODE"].ToString() + ","; }
                }
                if (Found)
                {
                    Success = false;
                    Info.Append(string.Format("Diaries Found ({0}), ", Results.Substring(0, Results.Length - 1)));
                }
                else
                {
                    Info.Append("No Exclusionary Diary Codes, ");
                }
            }
            if (STATE.Param)
            {
                if (dr["STATE"] is string)
                {
                    if (!CheckStateCrit((string)dr["STATE"], STATE.Elements))
                    {
                        Success = false;
                        Info.Append("Debtor State Req. Not Met, ");
                    }
                }
                else
                {
                    if (!CheckStateCrit("", STATE.Elements))
                    {
                        Success = false;
                        Info.Append("Debtor State Req. Not Met, ");
                    }
                }
            }
            if (RET_MAIL.Param > 0)
            {
                Eval temp = RET_MAIL.EvaluateVerbose(dr["RET_MAIL"]);
                if (!temp.Success)
                {
                    Success = false;
                    Info.Append(temp.Info + ", ");
                }
            }

            if (Info.Length >= 2)
                return new Eval(Success, Info.ToString().Substring(0, Info.Length - 2));
            else
                return new Eval(Success, "");
        }
        public Eval EvaluateVerbose(Evaluatees.Evaluatee EvalMe, bool FailureOnly = false)
        {
            bool Success = true;
            StringBuilder Info = new StringBuilder();

            if (ADVA.Param > 0)
            {
                Eval temp = ADVA.EvaluateVerbose(EvalMe.AdversarialAttorney);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (ANS_FILE.Param > 0)
            {
                Eval temp = ANS_FILE.EvaluateVerbose(EvalMe.AnswerFiled);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (BANK_NO.Param > 0)
            {
                Eval temp = BANK_NO.EvaluateVerbose(EvalMe.BankNo);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (BK_DATE.Param > 0)
            {
                Eval temp = BK_DATE.EvaluateVerbose(EvalMe.BankruptcyDate);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (DATE_RECD.Param > 0)
            {
                Eval temp = DATE_RECD.EvaluateVerbose(EvalMe.DateReceived);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (DEATH_DATE.Param > 0)
            {
                Eval temp = DEATH_DATE.EvaluateVerbose(EvalMe.DeathDate);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (DSMIS_DATE.Param > 0)
            {
                Eval temp = DSMIS_DATE.EvaluateVerbose(EvalMe.DismissalDate);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (EMP_NO.Param > 0)
            {
                Eval temp = EMP_NO.EvaluateVerbose(EvalMe.EmployerNo);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (FORW_NO.Param > 0)
            {
                Eval temp = FORW_NO.EvaluateVerbose(EvalMe.Forwarder);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (GARN_DATE.Param > 0)
            {
                Eval temp = GARN_DATE.EvaluateVerbose(EvalMe.GarnishmentDate);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (JMT_DATE.Param > 0)
            {
                Eval temp = JMT_DATE.EvaluateVerbose(EvalMe.JudgmentDate);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (LAST_PMT.Param > 0)
            {
                Eval temp = LAST_PMT.EvaluateVerbose(EvalMe.LastPaymentDate);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (PREV_LETTER.Param > 0)
            {
                Eval temp = PREV_LETTER.EvaluateVerbose(EvalMe.PreviousLetter);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (PREV_LETTER_TYPE.Param > 0)
            {
                Eval temp = PREV_LETTER_TYPE.EvaluateVerbose(EvalMe.PreviousLetterDate, EvalMe.PreviousLetterType);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (C_PRIORITY.Param > 0)
            {
                Eval temp = C_PRIORITY.EvaluateVerbose(EvalMe.C_Priority);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (MINSIFRATE.Param > 0)
            {
                Eval temp = MINSIFRATE.EvaluateVerbose(EvalMe.MinSifRate);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (SIFRATE.Param > 0)
            {
                Eval temp = SIFRATE.EvaluateVerbose(EvalMe.SifRate);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (SERVICE_DATE.Param > 0)
            {
                Eval temp = SERVICE_DATE.EvaluateVerbose(EvalMe.ServiceDate);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (SHER_NO.Param > 0)
            {
                Eval temp = SHER_NO.EvaluateVerbose(EvalMe.SheriffNo);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (SSN.Param > 0)
            {
                Eval temp = SSN.EvaluateVerbose(EvalMe.SSN);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (STAT2_DATE.Param > 0)
            {
                Eval temp = STAT2_DATE.EvaluateVerbose(EvalMe.Stat2Date);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (STATUTE_DATE.Param > 0)
            {
                Eval temp = STATUTE_DATE.EvaluateVerbose(EvalMe.StatuteDate);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (SUIT_DATE.Param > 0)
            {
                Eval temp = SUIT_DATE.EvaluateVerbose(EvalMe.SuitDate);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (BALANCE.Param > 0)
            {
                Eval temp = BALANCE.EvaluateVerbose(EvalMe.Balance);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (COST.Param > 0)
            {
                Eval temp = COST.EvaluateVerbose(EvalMe.Cost);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (DOCKET.Param > 0)
            {
                Eval temp = DOCKET.EvaluateVerbose(EvalMe.Docket);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (JMT_AMT.Param > 0)
            {
                Eval temp = JMT_AMT.EvaluateVerbose(EvalMe.JudgmentAmount);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (PERCENT_PAID.Param > 0)
            {
                Eval temp = PERCENT_PAID.EvaluateVerbose(EvalMe.PercentPaid);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (PMT_AMT.Param > 0)
            {
                Eval temp = PMT_AMT.EvaluateVerbose(EvalMe.LastPaymentAmount);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (SUIT_SCORE.Param > 0)
            {
                Eval temp = SUIT_SCORE.EvaluateVerbose(EvalMe.SuitScore);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (VENUE_NO.Param > 0)
            {
                Eval temp = VENUE_NO.EvaluateVerbose(EvalMe.Venue);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (CLASSIFICATION.Param > 0)
            {
                Eval temp = CLASSIFICATION.EvaluateVerbose(EvalMe.Classification);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (CLIENT.Param)
            {
                if (!CheckClientCrit(EvalMe.ClientType))
                {
                    Success = false;
                    Info.Append("Client Type Req. Not Met, ");
                }
                else
                    Info.Append("Client is " + EvalMe.ClientType + ", ");
            }
            if (COLLECTORS.Param)
            {
                Eval temp = COLLECTORS.EvaluateVerbose(EvalMe.Collector, null);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (SALES_NO.Param)
            {
                Eval temp = SALES_NO.EvaluateVerbose(EvalMe.SalesNo, null);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (STATUS_IS.Param)
            {
                Eval temp = STATUS_IS.EvaluateVerbose(EvalMe.Status, EvalMe.StatusDate);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (STATUS_NOT.Param)
            {
                Eval temp = STATUS_NOT.EvaluateVerbose(EvalMe.Status, EvalMe.StatusDate);
                if (!temp.Success) Success = false;
                if (!temp.Success || !FailureOnly)
                    Info.Append(temp.Info + ", ");
            }
            if (DIARY_IS.Param)
            {
                bool Found = false;
                string Results = "";
                foreach (DiaryCode C in EvalMe.Diaries)
                {
                    if (DIARY_IS.Evaluate(C.Code, C.Date)) { Found = true; Results += C.Code.ToString() + ","; }
                }
                if (!Found)
                {
                    Success = false;
                    Info.Append("Diary Code Reqs. Not Met, ");
                }
                else if (!FailureOnly)
                {
                    Info.Append(string.Format("Required Diaries Found ({0}), ", Results.Substring(0, Results.Length - 1)));
                }
            }
            if (DIARY_NOT.Param)
            {
                bool Found = false;
                string Results = "";
                foreach (DiaryCode C in EvalMe.Diaries)
                {
                    if (!DIARY_NOT.Evaluate(C.Code, C.Date)) { Found = true; Results += C.Code.ToString() + ","; }
                }
                if (Found)
                {
                    Success = false;
                    Info.Append(string.Format("Exclusionary Diaries Found ({0}), ", Results.Substring(0, Results.Length - 1)));
                }
                else if (!FailureOnly)
                {
                    Info.Append("No Exclusionary Diary Codes, ");
                }
            }
            if (STATE.Param)
            {
                if (!CheckStateCrit(EvalMe.State, STATE.Elements))
                {
                    Success = false;
                    Info.Append(string.Format("Debtor State \"{0}\", ", EvalMe.State));
                }
                else if (!FailureOnly)
                {
                    Info.Append(string.Format("Debtor State \"{0}\", ", EvalMe.State));
                }
            }
            if (RET_MAIL.Param > 0)
            {
                Eval temp = RET_MAIL.EvaluateVerbose(EvalMe.ReturnMail);
                if (!temp.Success)
                {
                    Success = false;
                    Info.Append(temp.Info + ", ");
                }
            }

            return new Eval(Success, Info.Length > 0 ? Info.ToString().Substring(0, Info.Length - 2) : "");
        }
        public Eval EvaluateORVerbose(Evaluatees.Evaluatee EvalMe)
        {
            bool Success = false;
            StringBuilder Info = new StringBuilder();

            if (ADVA.Param > 0)
            {
                Eval temp = ADVA.EvaluateVerbose(EvalMe.AdversarialAttorney);
                if (temp.Success)
                {
                    Success = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (ANS_FILE.Param > 0)
            {
                Eval temp = ANS_FILE.EvaluateVerbose(EvalMe.AnswerFiled);
                if (temp.Success)
                {
                    Success = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (BK_DATE.Param > 0)
            {
                Eval temp = BK_DATE.EvaluateVerbose(EvalMe.BankruptcyDate);
                if (temp.Success)
                {
                    Success = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (DATE_RECD.Param > 0)
            {
                Eval temp = DATE_RECD.EvaluateVerbose(EvalMe.DateReceived);
                if (temp.Success)
                {
                    Success = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (DSMIS_DATE.Param > 0)
            {
                Eval temp = DSMIS_DATE.EvaluateVerbose(EvalMe.DismissalDate);
                if (temp.Success)
                {
                    Success = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (GARN_DATE.Param > 0)
            {
                Eval temp = GARN_DATE.EvaluateVerbose(EvalMe.GarnishmentDate);
                if (temp.Success)
                {
                    Success = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (JMT_DATE.Param > 0)
            {
                Eval temp = JMT_DATE.EvaluateVerbose(EvalMe.JudgmentDate);
                if (temp.Success)
                {
                    Success = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (PREV_LETTER.Param > 0)
            {
                Eval temp = PREV_LETTER.EvaluateVerbose(EvalMe.PreviousLetter);
                if (temp.Success)
                {
                    Success = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (PREV_LETTER_TYPE.Param > 0)
            {
                Eval temp = PREV_LETTER_TYPE.EvaluateVerbose(EvalMe.PreviousLetterDate, EvalMe.PreviousLetterType);
                if (temp.Success)
                {
                    Success = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (MINSIFRATE.Param > 0)
            {
                Eval temp = MINSIFRATE.EvaluateVerbose(EvalMe.MinSifRate);
                if (temp.Success)
                {
                    Success = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (SIFRATE.Param > 0)
            {
                Eval temp = SIFRATE.EvaluateVerbose(EvalMe.SifRate);
                if (temp.Success)
                {
                    Success = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (SERVICE_DATE.Param > 0)
            {
                Eval temp = SERVICE_DATE.EvaluateVerbose(EvalMe.ServiceDate);
                if (temp.Success)
                {
                    Success = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (STATUTE_DATE.Param > 0)
            {
                Eval temp = STATUTE_DATE.EvaluateVerbose(EvalMe.StatuteDate);
                if (temp.Success)
                {
                    Success = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (SUIT_DATE.Param > 0)
            {
                Eval temp = SUIT_DATE.EvaluateVerbose(EvalMe.SuitDate);
                if (temp.Success)
                {
                    Success = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (BALANCE.Param > 0)
            {
                Eval temp = BALANCE.EvaluateVerbose(EvalMe.Balance);
                if (temp.Success)
                {
                    Success = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (COST.Param > 0)
            {
                Eval temp = COST.EvaluateVerbose(EvalMe.Cost);
                if (temp.Success)
                {
                    Success = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (DOCKET.Param > 0)
            {
                Eval temp = DOCKET.EvaluateVerbose(EvalMe.Docket);
                if (temp.Success)
                {
                    Success = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (JMT_AMT.Param > 0)
            {
                Eval temp = JMT_AMT.EvaluateVerbose(EvalMe.JudgmentAmount);
                if (temp.Success)
                {
                    Success = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (PERCENT_PAID.Param > 0)
            {
                Eval temp = PERCENT_PAID.EvaluateVerbose(EvalMe.PercentPaid);
                if (temp.Success)
                {
                    Success = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (PMT_AMT.Param > 0)
            {
                Eval temp = PMT_AMT.EvaluateVerbose(EvalMe.LastPaymentAmount);
                if (temp.Success)
                {
                    Success = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (SUIT_SCORE.Param > 0)
            {
                Eval temp = SUIT_SCORE.EvaluateVerbose(EvalMe.SuitScore);
                if (temp.Success)
                {
                    Success = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (CLASSIFICATION.Param > 0)
            {
                Eval temp = CLASSIFICATION.EvaluateVerbose(EvalMe.Classification);
                if (temp.Success)
                {
                    Success = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (CLIENT.Param)
            {
                if (CheckClientCrit(EvalMe.ClientType))
                {
                    Success = true;
                    Info.Append(string.Format("Client is {0}, ", EvalMe.ClientType));
                }
            }
            if (COLLECTORS.Param)
            {
                Eval temp = COLLECTORS.EvaluateVerbose(EvalMe.Collector, null);
                if (temp.Success)
                {
                    Success = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (SALES_NO.Param)
            {
                Eval temp = SALES_NO.EvaluateVerbose(EvalMe.SalesNo, null);
                if (temp.Success)
                {
                    Success = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (STATUS_IS.Param)
            {
                Eval temp = STATUS_IS.EvaluateVerbose(EvalMe.Status, EvalMe.StatusDate);
                if (temp.Success)
                {
                    Success = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (DIARY_IS.Param)
            {
                bool Found = false;
                string Results = "";
                foreach (DiaryCode C in EvalMe.Diaries)
                {
                    if (DIARY_IS.Evaluate(C.Code, C.Date)) { Found = true; Results += C.Code.ToString() + ","; }
                }
                if (Found)
                {
                    Success = true;
                    Info.Append(string.Format("Diaries Found ({0}), ", Results.Substring(0, Results.Length - 1)));
                }
            }
            if (STATE.Param)
            {
                if (CheckStateCrit(EvalMe.State, STATE.Elements))
                {
                    Success = true;
                    Info.Append("Debtor State Req. Not Met, ");
                }
            }
            if (RET_MAIL.Param > 0)
            {
                Eval temp = RET_MAIL.EvaluateVerbose(EvalMe.ReturnMail);
                if (temp.Success)
                {
                    Success = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (LAST_PMT.Param > 0)
            {
                Eval temp = LAST_PMT.EvaluateVerbose(EvalMe.LastPaymentDate);
                if (temp.Success) Success = true;
                else Success = false;
                Info.Append(temp.Info + ", ");
            }
            if (FORW_NO.Param > 0)
            {
                Eval temp = FORW_NO.EvaluateVerbose(EvalMe.Forwarder);
                if (temp.Success) Success = true;
                else Success = false;
                Info.Append(temp.Info + ", ");
            }
            if (STAT2_DATE.Param > 0)
            {
                Eval temp = STAT2_DATE.EvaluateVerbose(EvalMe.Stat2Date);
                if (!temp.Success) Success = false;
                Info.Append(temp.Info + ", ");
            }
            if (STATUS_NOT.Param)
            {
                Eval temp = STATUS_NOT.EvaluateVerbose(EvalMe.Status, EvalMe.StatusDate);
                if (!temp.Success)
                {
                    Success = false;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (DIARY_NOT.Param)
            {
                bool Found = false;
                string Results = "";
                foreach (DiaryCode C in EvalMe.Diaries)
                {
                    if (!DIARY_NOT.Evaluate(C.Code, C.Date)) { Found = true; Results += C.Code.ToString() + ","; }
                }
                if (Found)
                {
                    Success = false;
                    Info.Append(string.Format("Diaries Found ({0}), ", Results.Substring(0, Results.Length - 1)));
                }
                else
                {
                    Info.Append("No Exclusionary Diary Codes, ");
                }
            }

            return new Eval(Success, Info.ToString().Substring(0, Info.Length - 2));
        }
        public Eval EvaluateRemovalsVerbose(Evaluatees.Evaluatee EvalMe)
        {
            bool Success = false;
            bool SecondSuccess = false;
            bool ThirdSuccess = false;
            bool HadCritSet = false;
            StringBuilder Info = new StringBuilder();
            Eval temp;

            // Last Payment is the Key
            if (LAST_PMT.Param > 0)
            {
                temp = LAST_PMT.EvaluateVerbose(EvalMe.LastPaymentDate);
                if (temp.Success)
                {
                    Success = true;
                }
                Info.Append(temp.Info + ", ");
            }
            else
            {
                Success = true;
            }

            if (BK_DATE.Param > 0)
            {
                HadCritSet = true;
                temp = BK_DATE.EvaluateVerbose(EvalMe.BankruptcyDate);
                if (temp.Success)
                {
                    SecondSuccess = true;
                }
            }

            if (ADVA.Param > 0)
            {
                HadCritSet = true;
                temp = ADVA.EvaluateVerbose(EvalMe.AdversarialAttorney);
                if (temp.Success)
                {
                    SecondSuccess = true;
                    Info.Append(temp.Info);
                }
            }

            if (ANS_FILE.Param > 0)
            {
                HadCritSet = true;
                temp = ANS_FILE.EvaluateVerbose(EvalMe.AnswerFiled);
                if (temp.Success)
                {
                    SecondSuccess = true;
                    Info.Append(temp.Info);
                }
            }
            if (JMT_DATE.Param > 0)
            {
                HadCritSet = true;
                temp = JMT_DATE.EvaluateVerbose(EvalMe.JudgmentDate);
                if (temp.Success)
                {
                    SecondSuccess = true;
                    Info.Append(temp.Info);
                }
            }
            if (SUIT_DATE.Param > 0)
            {
                HadCritSet = true;
                temp = SUIT_DATE.EvaluateVerbose(EvalMe.SuitDate);
                if (temp.Success)
                {
                    SecondSuccess = true;
                    Info.Append(temp.Info);
                }
            }
            if (SERVICE_DATE.Param > 0)
            {
                HadCritSet = true;
                temp = SERVICE_DATE.EvaluateVerbose(EvalMe.ServiceDate);
                if (temp.Success)
                {
                    SecondSuccess = true;
                    Info.Append(temp.Info);
                }
            }

            if (DATE_RECD.Param > 0)
            {
                HadCritSet = true;
                temp = DATE_RECD.EvaluateVerbose(EvalMe.DateReceived);
                if (temp.Success)
                {
                    SecondSuccess = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (DSMIS_DATE.Param > 0)
            {
                HadCritSet = true;
                temp = DSMIS_DATE.EvaluateVerbose(EvalMe.DismissalDate);
                if (temp.Success)
                {
                    SecondSuccess = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (GARN_DATE.Param > 0)
            {
                HadCritSet = true;
                temp = GARN_DATE.EvaluateVerbose(EvalMe.GarnishmentDate);
                if (temp.Success)
                {
                    SecondSuccess = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (C_PRIORITY.Param > 0)
            {
                HadCritSet = true;
                temp = C_PRIORITY.EvaluateVerbose(EvalMe.C_Priority);
                if (temp.Success)
                {
                    SecondSuccess = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (STATUTE_DATE.Param > 0)
            {
                HadCritSet = true;
                temp = STATUTE_DATE.EvaluateVerbose(EvalMe.StatuteDate);
                if (temp.Success)
                {
                    SecondSuccess = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (BALANCE.Param > 0)
            {
                HadCritSet = true;
                temp = BALANCE.EvaluateVerbose(EvalMe.Balance);
                if (temp.Success)
                {
                    SecondSuccess = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (COST.Param > 0)
            {
                HadCritSet = true;
                temp = COST.EvaluateVerbose(EvalMe.Cost);
                if (temp.Success)
                {
                    SecondSuccess = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (DOCKET.Param > 0)
            {
                HadCritSet = true;
                temp = DOCKET.EvaluateVerbose(EvalMe.Docket);
                if (temp.Success)
                {
                    SecondSuccess = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (SUIT_SCORE.Param > 0)
            {
                HadCritSet = true;
                temp = SUIT_SCORE.EvaluateVerbose(EvalMe.SuitScore);
                if (temp.Success)
                {
                    SecondSuccess = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (CLASSIFICATION.Param > 0)
            {
                HadCritSet = true;
                temp = CLASSIFICATION.EvaluateVerbose(EvalMe.Classification);
                if (temp.Success)
                {
                    SecondSuccess = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (CLIENT.Param)
            {
                HadCritSet = true;
                if (CheckClientCrit(EvalMe.ClientType))
                {
                    SecondSuccess = true;
                    Info.Append(string.Format("Client is {0}, ", EvalMe.ClientType));
                }
            }
            if (COLLECTORS.Param)
            {
                HadCritSet = true;
                temp = COLLECTORS.EvaluateVerbose(EvalMe.Collector, null);
                if (temp.Success)
                {
                    SecondSuccess = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (STATUS_IS.Param)
            {
                HadCritSet = true;
                temp = STATUS_IS.EvaluateVerbose(EvalMe.Status, EvalMe.StatusDate);
                if (temp.Success)
                {
                    SecondSuccess = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            if (DIARY_IS.Param)
            {
                HadCritSet = true;
                bool Found = false;
                string Results = "";
                foreach (DiaryCode C in EvalMe.Diaries)
                {
                    if (DIARY_IS.Evaluate(C.Code, C.Date)) { Found = true; Results += C.Code.ToString() + ","; }
                }
                if (Found)
                {
                    SecondSuccess = true;
                    Info.Append(string.Format("Removal Diaries Found ({0}), ", Results.Substring(0, Results.Length - 1)));
                }
            }
            if (STATE.Param)
            {
                HadCritSet = true;
                if (CheckStateCrit(EvalMe.State, STATE.Elements))
                {
                    SecondSuccess = true;
                    Info.Append("Debtor State Req. Not Met, ");
                }
            }

            if (STAT2_DATE.Param > 0)
            {
                HadCritSet = true;
                temp = STAT2_DATE.EvaluateVerbose(EvalMe.Stat2Date);
                if (temp.Success)
                {
                    SecondSuccess = true;
                }
                Info.Append(temp.Info + ", ");
            }

            if (!HadCritSet) SecondSuccess = true;

            if (STATUS_NOT.Param)
            {
                temp = STATUS_NOT.EvaluateVerbose(EvalMe.Status, EvalMe.StatusDate);
                if (temp.Success)
                {
                    ThirdSuccess = true;
                    Info.Append(temp.Info + ", ");
                }
            }
            else
            {
                ThirdSuccess = true;
            }

            // All Accounts Must Not Have Keeper Codes (If Required)
            if (DIARY_NOT.Param)
            {
                bool Found = false;
                string Results = "";
                foreach (DiaryCode C in EvalMe.Diaries)
                {
                    if (!DIARY_NOT.Evaluate(C.Code, C.Date)) { Found = true; Results += C.Code.ToString() + ","; }
                }
                if (Found)
                {
                    Info.Append(string.Format("Exclusionary Diaries Found ({0}), ", Results.Substring(0, Results.Length - 1)));
                    ThirdSuccess = false;
                }
                else
                {
                    ThirdSuccess = true;
                    Info.Append("No Exclusionary Diary Codes, ");
                }
            }
            else
            {
                ThirdSuccess = true;
            }

            return new Eval(Success && SecondSuccess && ThirdSuccess, Info.ToString().Substring(0, Info.Length - 2));
        }

        public class Eval
        {
            private bool _Success;
            private string _Info;

            public Eval(bool Success, string Info)
            {
                _Success = Success;
                _Info = Info;
            }

            public bool Success
            {
                get { return _Success; }
            }
            public string Info
            {
                get { return _Info; }
            }
        }

        public class EvaluateeEval
        {
            public Eval Result { get; private set; }
            public string FileNo { get; private set; }

            public EvaluateeEval(string FileNo, Eval Result)
            {
                this.Result = Result;
                this.FileNo = FileNo;
            }
            public EvaluateeEval(Evaluatees.Evaluatee Evaluatee, Eval Result) : this(Evaluatee.FileNo, Result) { }
        }

        public bool CheckDiaryCrit(string FileNo, DataTable Compare, bool isn, Criteria.CBoolParam.Value Codes)
        {
            DataRow[] FileCode = Compare.Select("FILENO='" + FileNo + "'");
            if (isn)
            {
                for (int x = 0; x < Codes.Count; x++)
                {
                    foreach (DataRow FC in FileCode)
                    {
                        switch (Codes[x].Param)
                        {
                            case 0: //Exists
                                if ((double)FC["CODE"] == Codes[x].Code) return true;
                                break;
                            case 1: //In The Past
                                if ((double)FC["CODE"] == Codes[x].Code && (DateTime)FC["DATE"] < DateTime.Now.Date) return true;
                                break;
                            case 2: //In The Future
                                if ((double)FC["CODE"] == Codes[x].Code && (DateTime)FC["DATE"] > DateTime.Now.Date) return true;
                                break;
                            case 3: //Over Days Away
                                if ((double)FC["CODE"] == Codes[x].Code && (DateTime)FC["DATE"] > DateTime.Now.Date.AddDays((double)Codes[x].Value)) return true;
                                break;
                            case 4: //Under Days Away
                                if ((double)FC["CODE"] == Codes[x].Code && (DateTime)FC["DATE"] <= DateTime.Now.Date.AddDays((double)Codes[x].Value)) return true;
                                break;
                            case 5: //Over Days Ago
                                if ((double)FC["CODE"] == Codes[x].Code && (DateTime)FC["DATE"] < DateTime.Now.Date.AddDays(-(double)Codes[x].Value)) return true;
                                break;
                            case 6: //Under Days Ago
                                if ((double)FC["CODE"] == Codes[x].Code && (DateTime)FC["DATE"] >= DateTime.Now.Date.AddDays(-(double)Codes[x].Value)) return true;
                                break;
                        }
                    }
                }
                return false;
            }
            else
            {
                for (int x = 0; x < Codes.Count; x++)
                {
                    for (int y = 0; y < FileCode.Length; y++)
                    {
                        switch (Codes[x].Param)
                        {
                            case 0: //Exists
                                if ((double)FileCode[y]["CODE"] == Codes[x].Code) return false;
                                break;
                            case 1: //In The Past
                                if ((double)FileCode[y]["CODE"] == Codes[x].Code && (DateTime)FileCode[y]["DATE"] < DateTime.Now.Date) return false;
                                break;
                            case 2: //In The Future
                                if ((double)FileCode[y]["CODE"] == Codes[x].Code && (DateTime)FileCode[y]["DATE"] > DateTime.Now.Date) return false;
                                break;
                            case 3: //Over Days Away
                                if ((double)FileCode[y]["CODE"] == Codes[x].Code && (DateTime)FileCode[y]["DATE"] > DateTime.Now.Date.AddDays((double)Codes[x].Value)) return false;
                                break;
                            case 4: //Under Days Away
                                if ((double)FileCode[y]["CODE"] == Codes[x].Code && (DateTime)FileCode[y]["DATE"] <= DateTime.Now.Date.AddDays((double)Codes[x].Value)) return false;
                                break;
                            case 5: //Over Days Ago
                                if ((double)FileCode[y]["CODE"] == Codes[x].Code && (DateTime)FileCode[y]["DATE"] < DateTime.Now.Date.AddDays(-(double)Codes[x].Value)) return false;
                                break;
                            case 6: //Under Days Ago
                                if ((double)FileCode[y]["CODE"] == Codes[x].Code && (DateTime)FileCode[y]["DATE"] >= DateTime.Now.Date.AddDays(-(double)Codes[x].Value)) return false;
                                break;
                        }
                    }
                }
                return true;
            }
        }

        public bool CheckStateCrit(string eState, Criteria.CBoolParam.Value States)
        {
            bool HadIs = false;
            bool[,] SList = new bool[States.Count, 2];
            for (int i = 0; i < States.Count; i++)
            {
                DataRow[] Result = ST.Select("SID=" + States[i].Code);
                SList[i, 0] = States[i].ISNOT;
                SList[i, 1] = (eState == (string)Result[0]["State"]);
            }
            for (int i = 0; i < States.Count; i++) //If Any States Marked as "Not In" Had A Match, Return False
            {
                if (!SList[i, 0] && SList[i, 1]) return false;
            }
            for (int i = 0; i < States.Count; i++) //If Any States Marked as "In" Had A Match, Return True
            {
                if (SList[i, 0])
                {
                    if (SList[i, 1]) return true;
                    else HadIs = true;
                }
            }
            return !HadIs; //If No States Were Matched AND Should Have: Return False; Should Not Have: Return True
        }

        public bool CheckClientCrit(string eClient)
        {

            for (int i = 0; i < CL.Rows.Count; i++)
            {
                if (eClient.Equals((string)CL.Rows[i]["Client"])) return true;
            }
            return false;
        }

        public byte getDisp()
        {
            return _Disposition[0];
        }

        public void Load()
        {
            SqlQualifier = "";
            Initialize();
        }
        public void Load(int UID)
        {
            SqlQualifier = " WHERE UID=" + UID;
            Initialize();
        }

        public void Save()
        {
            SqlQualifier = "";
            SaveAll();
        }
        public void Save(int UID)
        {
            SqlQualifier = " WHERE UID=" + UID;
            SaveAll();
        }

        public void Load_Disposition(byte Disp)
        {
            _Disposition[0] = Disp;
            SqlQualifier = " WHERE DISPOSITION=0x" + BitConverter.ToString(_Disposition);
            Initialize();
        }
        public void Save_Disposition(byte Disp)
        {
            _Disposition[0] = Disp;
            SqlQualifier = " WHERE DISPOSITION=0x" + BitConverter.ToString(_Disposition);
            SaveAll();
        }

        public void Load_GarnRequest(byte Step, byte AssetType, byte Sales)
        {
            SqlQualifier = string.Format(" WHERE STEP={0} AND ASSETTYPE={1} AND SALES={2}", Step, AssetType, Sales);
            Initialize();
        }
        public void Save_GarnRequest(byte Step, byte AssetType, byte Sales)
        {
            SqlQualifier = string.Format(" WHERE STEP={0} AND ASSETTYPE={1} AND SALES={2}", Step, AssetType, Sales);
            SaveAll();
        }

        public void Load_Vendor(int Vendor)
        {
            SqlQualifier = " WHERE VENDOR=" + Vendor;
            Initialize();
        }
        public void Save_Vendor(int Vendor)
        {
            SqlQualifier = " WHERE VENDOR=" + Vendor;
            SaveAll();
        }

        public void Load_Report(int Report)
        {
            SqlQualifier = " WHERE REPORT=" + Report;
            Initialize();
        }
        public void Save_Report(int Report)
        {
            SqlQualifier = " WHERE REPORT=" + Report;
            SaveAll();
        }

        public void Load_Exception(int Exception)
        {
            SqlQualifier = " WHERE [ID]=" + Exception;
            try
            {
                Initialize();
            }
            catch
            {
                using (SqlConnection conn = new SqlConnection(SQL))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO " + ValuesStr.Substring(ValuesStr.LastIndexOf(' ')) + " (ID) VALUES(" + Exception + ")", conn))
                    {
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (SqlException sx)
                        {
                            if (sx.Number != 2627)
                                throw new NotImplementedException(sx.Message);
                        }
                    }
                }
                Initialize();
            }
        }
        public void Save_Exception(int Exception)
        {
            SqlQualifier = " WHERE [ID]=" + Exception;
            SaveAll();
        }

        public void Load_Routine(int Routine, bool Placement, bool Override)
        {
            SqlQualifier = " WHERE ROUTINE=" + Routine + " AND PLACEMENT=" + Convert.ToByte(Placement) + " AND OVERRIDE=" + Convert.ToByte(Override);
            try
            {
                Initialize();
            }
            catch
            {
                using (SqlConnection conn = new SqlConnection(SQL))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO " + CriteriaStr.Substring(CriteriaStr.LastIndexOf(' ')) + " (ROUTINE, PLACEMENT, OVERRIDE) VALUES(" + Routine + "," + Convert.ToByte(Placement) + "," + Convert.ToByte(Override) + ")", conn))
                    {
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (SqlException sx)
                        {
                            if (sx.Number != 2627)
                                throw new NotImplementedException(sx.Message);
                        }
                    }
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO " + ValuesStr.Substring(ValuesStr.LastIndexOf(' ')) + " (ROUTINE, PLACEMENT, OVERRIDE) VALUES(" + Routine + "," + Convert.ToByte(Placement) + "," + Convert.ToByte(Override) + ")", conn))
                    {
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (SqlException sx)
                        {
                            if (sx.Number != 2627)
                                throw new NotImplementedException(sx.Message);
                        }
                    }
                }
                Initialize();
            }
        }
        public void Save_Routine(int Routine, bool Placement, bool Override)
        {
            SqlQualifier = " WHERE ROUTINE=" + Routine + " AND PLACEMENT=" + Convert.ToByte(Placement) + " AND OVERRIDE=" + Convert.ToByte(Override);
            SaveAll();
        }

        public void Load_Priority(int thePriority)
        {
            using (SqlDataAdapter sda = new SqlDataAdapter(CriteriaStr + " WHERE PRIORITY=" + thePriority, SQL))
            {
                DataTable dt = new DataTable();
                sda.Fill(dt);
                _Disposition = (byte[])dt.Rows[0]["DISPOSITION"];
            }
            SqlQualifier = " WHERE DISPOSITION=0x" + BitConverter.ToString(_Disposition);
            Initialize();
        }
        public void Save_Priority(int thePriority)
        {
            using (SqlDataAdapter sda = new SqlDataAdapter(CriteriaStr + " WHERE PRIORITY=" + thePriority, SQL))
            {
                DataTable dt = new DataTable();
                sda.Fill(dt);
                _Disposition = (byte[])dt.Rows[0]["DISPOSITION"];
            }
            SqlQualifier = " WHERE DISPOSITION=0x" + BitConverter.ToString(_Disposition);
            SaveAll();
        }

        public void Load_LetterSeries(int Profile, int LetterType, int Accounts, int Clients, int Forwarder)
        {
            SqlQualifier = string.Format(" WHERE Profile={0} AND Letter_Type={1} AND Accts={2} AND Clients={3} AND Forw_no={4}", Profile, LetterType, Accounts, Clients, Forwarder);
            try
            {
                Initialize();
            }
            catch (RowNotInTableException)
            {
                using (SqlConnection conn = new SqlConnection(SQL))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(string.Format("INSERT INTO Letters_Criteria (Profile,Letter_Type,Accts,Clients,Forw_No) VALUES({0},{1},{2},{3},{4})", Profile, LetterType, Accounts, Clients, Forwarder), conn);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException sx)
                    {
                        if (sx.Number != 2627)
                            throw new NotImplementedException(sx.Message);
                    }
                    cmd = new SqlCommand(string.Format("INSERT INTO Letters_Values (Profile,Letter_Type,Accts,Clients,Forw_No) VALUES({0},{1},{2},{3},{4})", Profile, LetterType, Accounts, Clients, Forwarder), conn);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException sx)
                    {
                        if (sx.Number != 2627)
                            throw new NotImplementedException(sx.Message);
                    }
                }
                Initialize();
            }
        }
        public void Save_LetterSeries(int Profile, int LetterType, int Accounts, int Clients, int Forwarder)
        {
            SqlQualifier = string.Format(" WHERE Profile={0} AND Letter_Type={1} AND Accts={2} AND Clients={3} AND Forw_no={4}", Profile, LetterType, Accounts, Clients, Forwarder);
            SaveAll();
        }
        public void Load_LetterSeriesLetter(int Profile, int LetterType, string Letter, int Accounts, int Clients, int Forwarder)
        {
            CriteriaStr = "SELECT * FROM Letters_Letter_Criteria";
            ValuesStr = "SELECT * FROM Letters_Letter_Values";
            CodesStr = "SELECT * FROM Letters_Letter_Codes";
            SqlQualifier = string.Format(" WHERE Profile={0} AND Letter_Type={1} AND Letter='{5}' AND Accts={2} AND Clients={3} AND Forw_no={4}", Profile, LetterType, Accounts, Clients, Forwarder, Letter);
            try
            {
                Initialize();
            }
            catch (RowNotInTableException)
            {
                using (SqlConnection conn = new SqlConnection(SQL))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(string.Format("INSERT INTO Letters_Letter_Criteria (Profile,Letter_Type,Letter,Accts,Clients,Forw_No) VALUES({0},{1},'{2}',{3},{4},{5})", Profile, LetterType, Letter, Accounts, Clients, Forwarder), conn);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException sx)
                    {
                        if (sx.Number != 2627)
                            throw new NotImplementedException(sx.Message);
                    }
                    cmd = new SqlCommand(string.Format("INSERT INTO Letters_Letter_Values (Profile,Letter_Type,Letter,Accts,Clients,Forw_No) VALUES({0},{1},'{2}',{3},{4},{5})", Profile, LetterType, Letter, Accounts, Clients, Forwarder), conn);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException sx)
                    {
                        if (sx.Number != 2627)
                            throw new NotImplementedException(sx.Message);
                    }
                }
                Initialize();
            }
        }
        public void Save_LetterSeriesLetter(int Profile, int LetterType, string Letter, int Accounts, int Clients, int Forwarder)
        {
            SqlQualifier = string.Format(" WHERE Profile={0} AND Letter_Type={1} AND Letter='{5}' AND Accts={2} AND Clients={3} AND Forw_no={4}", Profile, LetterType, Accounts, Clients, Forwarder, Letter);
            SaveAll();
        }

        public void Load_CBR(string ClientType, int Forwarder)
        {
            SqlQualifier = string.Format(" WHERE CL_Type='{0}' AND Forw_no={1}", ClientType, Forwarder);
            try
            {
                Initialize();
            }
            catch (RowNotInTableException)
            {
                using (SqlConnection conn = new SqlConnection(SQL))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(string.Format("INSERT INTO CBR_Criteria (CL_Type,Forw_No) VALUES('{0}',{1})", ClientType, Forwarder), conn);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException sx)
                    {
                        if (sx.Number != 2627)
                            throw new NotImplementedException(sx.Message);
                    }
                    cmd = new SqlCommand(string.Format("INSERT INTO CBR_Values (CL_Type,Forw_No) VALUES('{0}',{1})", ClientType, Forwarder), conn);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SqlException sx)
                    {
                        if (sx.Number != 2627)
                            throw new NotImplementedException(sx.Message);
                    }
                }
                Initialize();
            }
        }
        public void Save_CBR(string ClientType, int Forwarder)
        {
            SqlQualifier = string.Format(" WHERE CL_Type='{0}' AND Forw_no={1}", ClientType, Forwarder);
            SaveAll();
        }

        private void SaveAll()
        {
            using (SqlDataAdapter sda = new SqlDataAdapter(CriteriaStr + SqlQualifier, SQL))
            {
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    dt.Rows[0].BeginEdit();
                    try { dt.Rows[0]["ADVA"] = ADVA.Param; }
                    catch { }
                    try { dt.Rows[0]["ANS_FILE"] = ANS_FILE.Param; }
                    catch { }
                    try { dt.Rows[0]["BALANCE"] = BALANCE.Param; }
                    catch { }
                    try { dt.Rows[0]["BANK_NO"] = BANK_NO.Param; }
                    catch { }
                    try { dt.Rows[0]["BK_DATE"] = BK_DATE.Param; }
                    catch { }
                    try { dt.Rows[0]["CLASSIFICATION"] = CLASSIFICATION.Param; }
                    catch { }
                    try { dt.Rows[0]["CLIENT"] = CLIENT.Param; }
                    catch { }
                    try { dt.Rows[0]["COLLECTORS"] = COLLECTORS.Param; }
                    catch { }
                    try { dt.Rows[0]["COST"] = COST.Param; }
                    catch { }
                    try { dt.Rows[0]["DATE_RECD"] = DATE_RECD.Param; }
                    catch { }
                    try { dt.Rows[0]["DIARY_IS"] = DIARY_IS.Param; }
                    catch { }
                    try { dt.Rows[0]["DIARY_NOT"] = DIARY_NOT.Param; }
                    catch { }
                    try { dt.Rows[0]["DOCKET"] = DOCKET.Param; }
                    catch { }
                    try { dt.Rows[0]["DSMIS_DATE"] = DSMIS_DATE.Param; }
                    catch { }
                    try { dt.Rows[0]["EMP_NO"] = EMP_NO.Param; }
                    catch { }
                    try { dt.Rows[0]["FORW_NO"] = FORW_NO.Param; }
                    catch { }
                    try { dt.Rows[0]["GARN_DATE"] = GARN_DATE.Param; }
                    catch { }
                    try { dt.Rows[0]["JMT_AMT"] = JMT_AMT.Param; }
                    catch { }
                    try { dt.Rows[0]["JMT_DATE"] = JMT_DATE.Param; }
                    catch { }
                    try { dt.Rows[0]["LAST_PMT"] = LAST_PMT.Param; }
                    catch { }
                    try { dt.Rows[0]["PERCENT_PAID"] = PERCENT_PAID.Param; }
                    catch { }
                    try { dt.Rows[0]["PMT_AMT"] = PMT_AMT.Param; }
                    catch { }
                    try { dt.Rows[0]["PREV_LETTER_TYPE"] = PREV_LETTER_TYPE.Param; }
                    catch { }
                    try { dt.Rows[0]["PRIORITY"] = C_PRIORITY.Param; }
                    catch { }
                    try { dt.Rows[0]["RET_MAIL"] = RET_MAIL.Param; }
                    catch { }
                    try { dt.Rows[0]["SALES_NO"] = SALES_NO.Param; }
                    catch { }
                    try { dt.Rows[0]["SERVICE_DATE"] = SERVICE_DATE.Param; }
                    catch { }
                    try { dt.Rows[0]["STAT2_DATE"] = STAT2_DATE.Param; }
                    catch { }
                    try { dt.Rows[0]["STATE"] = STATE.Param; }
                    catch { }
                    try { dt.Rows[0]["STATUS_IS"] = STATUS_IS.Param; }
                    catch { }
                    try { dt.Rows[0]["STATUS_NOT"] = STATUS_NOT.Param; }
                    catch { }
                    try { dt.Rows[0]["STATUTE_DATE"] = STATUTE_DATE.Param; }
                    catch { }
                    try { dt.Rows[0]["SUIT_DATE"] = SUIT_DATE.Param; }
                    catch { }
                    try { dt.Rows[0]["SUIT_SCORE"] = SUIT_SCORE.Param; }
                    catch { }
                    try { dt.Rows[0]["Enabled"] = Enabled; }
                    catch { }
                    dt.Rows[0].EndEdit();

                    SqlCommandBuilder sql = new SqlCommandBuilder(sda);
                    sda.Update(dt);
                }
                else
                {
                    throw new RowNotInTableException();
                }
            }
            using (SqlDataAdapter sda = new SqlDataAdapter(ValuesStr + SqlQualifier, SQL))
            {
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    dt.Rows[0].BeginEdit();
                    try { dt.Rows[0]["ANS_FILE"] = ANS_FILE.Value; }
                    catch { }
                    try { dt.Rows[0]["ANS_FILE2"] = ANS_FILE.Value2; }
                    catch { }
                    try { dt.Rows[0]["BALANCE"] = BALANCE.Value; }
                    catch { }
                    try { dt.Rows[0]["BANK_NO"] = BANK_NO.Value; }
                    catch { }
                    try { dt.Rows[0]["BK_DATE"] = BK_DATE.Value; }
                    catch { }
                    try { dt.Rows[0]["BK_DATE2"] = BK_DATE.Value2; }
                    catch { }
                    try { dt.Rows[0]["CLASSIFICATION"] = CLASSIFICATION.Value; }
                    catch { }
                    try { dt.Rows[0]["COST"] = COST.Value; }
                    catch { }
                    try { dt.Rows[0]["DATE_RECD"] = DATE_RECD.Value; }
                    catch { }
                    try { dt.Rows[0]["DATE_RECD2"] = DATE_RECD.Value2; }
                    catch { }
                    try { dt.Rows[0]["DSMIS_DATE"] = DSMIS_DATE.Value; }
                    catch { }
                    try { dt.Rows[0]["DSMIS_DATE2"] = DSMIS_DATE.Value2; }
                    catch { }
                    try { dt.Rows[0]["EMP_NO"] = EMP_NO.Value; }
                    catch { }
                    try { dt.Rows[0]["GARN_DATE"] = GARN_DATE.Value; }
                    catch { }
                    try { dt.Rows[0]["GARN_DATE2"] = GARN_DATE.Value2; }
                    catch { }
                    try { dt.Rows[0]["JMT_AMT"] = JMT_AMT.Value; }
                    catch { }
                    try { dt.Rows[0]["JMT_DATE"] = JMT_DATE.Value; }
                    catch { }
                    try { dt.Rows[0]["JMT_DATE2"] = JMT_DATE.Value2; }
                    catch { }
                    try { dt.Rows[0]["LAST_PMT"] = LAST_PMT.Value; }
                    catch { }
                    try { dt.Rows[0]["LAST_PMT2"] = LAST_PMT.Value2; }
                    catch { }
                    try { dt.Rows[0]["PERCENT_PAID"] = PERCENT_PAID.Value; }
                    catch { }
                    try { dt.Rows[0]["PMT_AMT"] = PMT_AMT.Value; }
                    catch { }
                    try { dt.Rows[0]["PREV_LETTER"] = PREV_LETTER_TYPE.Value; }
                    catch { }
                    try { dt.Rows[0]["PREV_LETTER2"] = PREV_LETTER_TYPE.Value2; }
                    catch { }
                    try { dt.Rows[0]["PREV_LETTER_TYPE"] = PREV_LETTER_TYPE.LetterType; }
                    catch { }
                    try { dt.Rows[0]["SERVICE_DATE"] = SERVICE_DATE.Value; }
                    catch { }
                    try { dt.Rows[0]["SERVICE_DATE2"] = SERVICE_DATE.Value2; }
                    catch { }
                    try { dt.Rows[0]["STAT2_DATE"] = STAT2_DATE.Value; }
                    catch { }
                    try { dt.Rows[0]["STAT2_DATE2"] = STAT2_DATE.Value2; }
                    catch { }
                    try { dt.Rows[0]["STATUTE_DATE"] = STATUTE_DATE.Value; }
                    catch { }
                    try { dt.Rows[0]["STATUTE_DATE2"] = STATUTE_DATE.Value2; }
                    catch { }
                    try { dt.Rows[0]["SUIT_DATE"] = SUIT_DATE.Value; }
                    catch { }
                    try { dt.Rows[0]["SUIT_DATE2"] = SUIT_DATE.Value2; }
                    catch { }
                    try { dt.Rows[0]["SUIT_SCORE"] = SUIT_SCORE.Value; }
                    catch { }
                    dt.Rows[0].EndEdit();
                    SqlCommandBuilder sql = new SqlCommandBuilder(sda);
                    sda.Update(dt);
                }
                else
                {
                    throw new RowNotInTableException();
                }
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
        public class CParam
        {
            private int _Param = 0;
            private string _DataName = "";
            public int Param { get { return _Param; } set { _Param = value; } }
            public string DataName { get { return _DataName; } set { _DataName = value; } }
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
        }
        [Serializable]
        public class CDateParam : CParam
        {
            private int _Value = 0;
            private int _Value2 = 0;

            public CDateParam() { }
            public CDateParam(string DataName)
            {
                this.DataName = DataName;
            }

            public bool Evaluate(object DateGiven)
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
                                return true;
                            case 2: //Does Not Exist
                                return false;
                            case 3: //Greater Than _Value Days Ago
                                if (((DateTime)DateGiven).Date < DateTime.Now.AddDays(0 - _Value))
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            case 4: //Less Than _Value Days Ago
                                if (((DateTime)DateGiven).Date > DateTime.Now.AddDays(0 - _Value))
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            case 5: //Between _Value and _Value2 Days Ago
                                if (((DateTime)DateGiven).Date < DateTime.Now.AddDays(0 - _Value) && ((DateTime)DateGiven).Date > DateTime.Now.AddDays(0 - _Value2))
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            case 6: //Exists More Than _Value Days Ago
                                if (((DateTime)DateGiven).Date < DateTime.Now.AddDays(0 - _Value))
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
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
                                return true;
                            case 1:
                                return false;
                            case 2:
                                return true;
                            case 3:
                                return true;
                            case 4:
                                return false;
                            case 5:
                                return false;
                            case 6:
                                return false;
                            default:
                                return true;
                        }
                    }
                    else
                        throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected Data Type \"" + DateGiven.GetType().ToString() + "\"");
                }
                catch (Exception ex) { throw new NotImplementedException(ex.Message); }
            }
            public Eval EvaluateVerbose(object DateGiven)
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
                                Success = true;
                                Result = " Exists";
                                break;
                            case 2: //Does Not Exist
                                Success = false;
                                Result = " Exists";
                                break;
                            case 3: //Greater Than _Value Days Ago or Null
                                if (((DateTime)DateGiven).Date < DateTime.Now.AddDays(0 - _Value))
                                {
                                    Success = true;
                                    Result = string.Format(" More Than {0} Days Ago ({1})", _Value, ((DateTime)DateGiven).ToString("MM/dd/yyyy"));
                                    break;
                                }
                                else
                                {
                                    Success = false;
                                    Result = string.Format(" Less Than {0} Days Ago ({1})", _Value, ((DateTime)DateGiven).ToString("MM/dd/yyyy"));
                                    break;
                                }
                            case 4: //Less Than _Value Days Ago
                                if (((DateTime)DateGiven).Date > DateTime.Now.AddDays(0 - _Value))
                                {
                                    Success = true;
                                    Result = string.Format(" Less Than {0} Days Ago ({1})", _Value, ((DateTime)DateGiven).ToString("MM/dd/yyyy"));
                                    break;
                                }
                                else
                                {
                                    Success = false;
                                    Result = string.Format(" More Than {0} Days Ago ({1})", _Value, ((DateTime)DateGiven).ToString("MM/dd/yyyy"));
                                    break;
                                }
                            case 5: //Between _Value and _Value2 Days Ago
                                if (((DateTime)DateGiven).Date < DateTime.Now.AddDays(0 - _Value) && ((DateTime)DateGiven).Date > DateTime.Now.AddDays(0 - _Value2))
                                {
                                    Success = true;
                                    Result = string.Format(" Between {0} and {1} Days Ago ({2})", _Value, _Value2, ((DateTime)DateGiven).ToString("MM/dd/yyyy"));
                                    break;
                                }
                                else
                                {
                                    Success = false;
                                    if (((DateTime)DateGiven).Date > DateTime.Now.AddDays(0 - _Value))
                                        Result = string.Format(" Less Than {0} Days Ago ({1})", _Value, ((DateTime)DateGiven).ToString("MM/dd/yyyy"));
                                    else
                                        Result = string.Format(" More Than {0} Days Ago ({1})", _Value2, ((DateTime)DateGiven).ToString("MM/dd/yyyy"));
                                    break;
                                }
                            case 6: //Exists More Than _Value Days Ago
                                if (((DateTime)DateGiven).Date < DateTime.Now.AddDays(0 - _Value))
                                {
                                    Success = true;
                                    Result = string.Format(" More Than {0} Days Ago ({1})", _Value, ((DateTime)DateGiven).ToString("MM/dd/yyyy"));
                                    break;
                                }
                                else
                                {
                                    Success = false;
                                    Result = string.Format(" Less Than {0} Days Ago ({1})", _Value, ((DateTime)DateGiven).ToString("MM/dd/yyyy"));
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
                                Success = true;
                                break;
                            case 1:
                                Success = false;
                                Result = " Does Not Exist";
                                break;
                            case 2:
                                Success = true;
                                Result = " Does Not Exist";
                                break;
                            case 3:
                                Success = true;
                                Result = " Does Not Exist";
                                break;
                            case 4:
                                Success = false;
                                Result = " Does Not Exist";
                                break;
                            case 5:
                                Success = false;
                                Result = " Does Not Exist";
                                break;
                            case 6:
                                Success = false;
                                Result = " Does Not Exist";
                                break;
                            default:
                                Success = true;
                                break;
                        }
                    }
                    else
                        throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected Data Type \"" + DateGiven.GetType().ToString() + "\"");

                    return new Eval(Success, DataName + Result);
                }
                catch (Exception ex) { throw new NotImplementedException(ex.Message); }
            }

            public int Value
            {
                get { return _Value; }
                set { _Value = value; }
            }
            public int Value2
            {
                get { return _Value2; }
                set { _Value2 = value; }
            }
        }
        [Serializable]
        public class CLetterParam : CDateParam
        {
            private int _Letter = 0;

            public CLetterParam() { }
            public CLetterParam(string DataName)
            {
                this.DataName = DataName;
            }

            public bool Evaluate(object DateGiven, object LetterTypeGiven)
            {
                bool Result = this.Evaluate(DateGiven);
                if (Result)
                {
                    if (LetterTypeGiven is int)
                    {
                        if ((int)LetterTypeGiven == _Letter || _Letter == 0) return true;
                        else return false;
                    }
                    else
                    {
                        throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected Data Type \"" + LetterTypeGiven.GetType().ToString() + "\"");
                    }
                }
                else
                {
                    return false;
                }
            }
            public Eval EvaluateVerbose(object DateGiven, object LetterTypeGiven)
            {
                Eval info = this.EvaluateVerbose(DateGiven);
                if (info.Success)
                {
                    if (LetterTypeGiven is int)
                    {
                        if ((int)LetterTypeGiven == _Letter || _Letter == 0)
                        {
                            return info;
                        }
                        else
                        {
                            return new Eval(false, DataName + " Not Required Type");
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

            public int LetterType { get { return _Letter; } set { _Letter = value; } }
        }
        [Serializable]
        public class CBasicParam : CParam
        {
            private int _Value = 0;

            public CBasicParam() { }
            public CBasicParam(string DataName)
            {
                this.DataName = DataName;
            }

            public bool Evaluate(object ValueGiven)
            {
                double Value = 0;
                try
                {
                    if (ValueGiven is double || ValueGiven is double?) Value = (double)ValueGiven;
                    else if (ValueGiven is int || ValueGiven is int?) Value = (int)ValueGiven;
                    else if (ValueGiven is decimal || ValueGiven is decimal?) Value = (double)(decimal)ValueGiven;
                    else if (ValueGiven is string) Value = double.Parse(NumberCleanup(ValueGiven.ToString(), typeof(double)));
                    if (ValueGiven is double || ValueGiven is int || ValueGiven is decimal || ValueGiven is string || ValueGiven is double? || ValueGiven is int? || ValueGiven is decimal?)
                    {
                        switch (Param)
                        {
                            case 0: //Don't Care
                                return true;
                            case 1: //Equal
                                if (Value == _Value) { return true; }
                                else { return false; }
                            case 2: //Not Equal
                                if (Value != _Value) { return true; }
                                else { return false; }
                            case 3: //More Than
                                if (Value > _Value) { return true; }
                                else { return false; }
                            case 4: //Less Than
                                if (Value < _Value) { return true; }
                                else { return false; }
                            case 5: //Exists
                                return true;
                            case 6: //Does Not Exist
                                return false;
                            case 7: //More Than or Equal To
                                if (Value >= _Value) { return true; }
                                else { return false; }
                            case 8: //Less Than or Equal To
                                if (Value <= _Value) { return true; }
                                else { return false; }
                            default:
                                return true;
                        }
                    }
                    else if (ValueGiven is DBNull || ValueGiven == null)
                    {
                        switch (Param)
                        {
                            case 0:
                                return true;
                            case 6:
                                return true;
                            default:
                                return false;
                        }
                    }
                    else
                        throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected Data Type \"" + ValueGiven.GetType().ToString() + "\"");
                }
                catch (Exception ex) { throw new NotImplementedException(ex.Message); }
            }

            public Eval EvaluateVerbose(object ValueGiven)
            {
                bool Success = true;
                string Result = "";
                double Value = 0;
                try
                {
                    if (ValueGiven is double || ValueGiven is double?) Value = (double)ValueGiven;
                    else if (ValueGiven is int || ValueGiven is int?) Value = (int)ValueGiven;
                    else if (ValueGiven is decimal || ValueGiven is decimal?) Value = (double)(decimal)ValueGiven;
                    else if (ValueGiven is string) Value = double.Parse(NumberCleanup(ValueGiven.ToString(), typeof(double)));
                    if (ValueGiven is double || ValueGiven is int || ValueGiven is decimal || ValueGiven is string || ValueGiven is double? || ValueGiven is int? || ValueGiven is decimal?)
                    {
                        switch (Param)
                        {
                            case 0: //Don't Care
                                Success = true;
                                break;
                            case 1: //Equal
                                if (Value == _Value)
                                {
                                    Success = true;
                                    Result = string.Format(" Equal To {0}", _Value);
                                    break;
                                }
                                else
                                {
                                    Success = false;
                                    Result = string.Format(" Not Equal To {0}", _Value);
                                    break;
                                }
                            case 2: //Not Equal
                                if (Value != _Value)
                                {
                                    Success = true;
                                    Result = string.Format(" Not Equal To {0}", _Value);
                                    break;
                                }
                                else
                                {
                                    Success = false;
                                    Result = string.Format(" Equal To {0}", _Value);
                                    break;
                                }
                            case 3: //More Than
                                if (Value > _Value)
                                {
                                    Success = true;
                                    Result = string.Format(" Greater Than {0}", _Value);
                                    break;
                                }
                                else
                                {
                                    Success = false;
                                    Result = string.Format(" Less Than {0}", _Value);
                                    break;
                                }
                            case 4: //Less Than
                                if (Value < _Value)
                                {
                                    Success = true;
                                    Result = string.Format(" Less Than {0}", _Value);
                                    break;
                                }
                                else
                                {
                                    Success = false;
                                    Result = string.Format(" Greater Than {0}", _Value);
                                    break;
                                }
                            case 5: //Exists
                                Success = true;
                                Result = " Exists";
                                break;
                            case 6: //Does Not Exist
                                Success = false;
                                Result = " Exists";
                                break;
                            case 7: //More Than or Equal To
                                if (Value >= _Value)
                                {
                                    Success = true;
                                    Result = string.Format(" Greater Than or Equal To {0}", _Value);
                                    break;
                                }
                                else
                                {
                                    Success = false;
                                    Result = string.Format(" Less Than {0}", _Value);
                                    break;
                                }
                            case 8: //Less Than or Equal To
                                if (Value <= _Value)
                                {
                                    Success = true;
                                    Result = string.Format(" Less Than or Equal To {0}", _Value);
                                    break;
                                }
                                else
                                {
                                    Success = false;
                                    Result = string.Format(" Greater Than {0}", _Value);
                                    break;
                                }
                            default:
                                Success = true;
                                break;
                        }
                    }
                    else if (ValueGiven is DBNull || ValueGiven == null)
                    {
                        switch (Param)
                        {
                            case 0:
                                Success = true;
                                break;
                            case 6:
                                Success = true;
                                Result = " Does Not Exist";
                                break;
                            default:
                                Success = false;
                                Result = " Does Not Exist";
                                break;
                        }
                    }
                    else
                        throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected Data Type \"" + ValueGiven.GetType().ToString() + "\"");
                }
                catch (Exception ex) { throw new NotImplementedException(ex.Message); }

                return new Eval(Success, DataName + Result);
            }

            public int Value
            {
                get { return _Value; }
                set { _Value = value; }
            }
        }
        [Serializable]
        public class CClassParam : CParam
        {
            private string _Value = "";

            public string Value
            {
                get { return _Value; }
                set { _Value = value; }
            }

            public CClassParam() { }
            public CClassParam(string DataName)
            {
                this.DataName = DataName;
            }

            public bool Evaluate(object ValueGiven)
            {
                try
                {
                    string Value = "";
                    if (ValueGiven is string) Value = (string)ValueGiven;
                    else if (ValueGiven is DBNull) Value = "";
                    if (ValueGiven is string || ValueGiven is DBNull)
                    {
                        switch (Param)
                        {
                            case 0:
                                return true;
                            case 1:
                                if (Value == _Value) return true;
                                else return false;
                            case 2:
                                if (Value != _Value) return true;
                                else return false;
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

            public Eval EvaluateVerbose(object ValueGiven)
            {
                bool Success = true;
                string Result = "";

                try
                {
                    string Value = "";
                    if (ValueGiven is string) Value = (string)ValueGiven;
                    else if (ValueGiven is DBNull) Value = "";
                    if (ValueGiven is string || ValueGiven is DBNull)
                    {
                        switch (Param)
                        {
                            case 0:
                                Success = true;
                                break;
                            case 1:
                                if (Value == _Value)
                                {
                                    Success = true;
                                    Result = string.Format(" Equalt To \"{0}\"", _Value);
                                    break;
                                }
                                else
                                {
                                    Success = false;
                                    Result = string.Format(" Not Equal To \"{0}\"", _Value);
                                    break;
                                }
                            case 2:
                                if (Value != _Value)
                                {
                                    Success = true;
                                    Result = string.Format(" Not Equal To \"{0}\"", _Value);
                                    break;
                                }
                                else
                                {
                                    Success = false;
                                    Result = string.Format(" Equal To \"{0}\"", _Value);
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
        }
        [Serializable]
        public class CSimpleParam : CParam
        {
            public CSimpleParam() { }
            public CSimpleParam(string DataName)
            {
                this.DataName = DataName;
            }

            public bool Evaluate(object ValueGiven)
            {
                try
                {
                    string Value = "";
                    if (ValueGiven is string) Value = (string)ValueGiven;
                    if (ValueGiven is bool) { if ((bool)ValueGiven) Value = "True"; }
                    if (Value != "" && !(ValueGiven is DBNull))
                    {
                        switch (Param)
                        {
                            case 0: //Do Not Use
                                return true;
                            case 1: //Exists
                                return true;
                            case 2: //Does Not Exist
                                return false;
                            default:
                                return false;
                        }
                    }
                    else
                    {
                        switch (Param)
                        {
                            case 0: //Do Not Use
                                return true;
                            case 1: //Exists
                                return false;
                            case 2: //Does Not Exist
                                return true;
                            default:
                                return false;
                        }
                    }
                }
                catch (Exception ex) { throw new NotImplementedException(ex.Message); }
            }

            public Eval EvaluateVerbose(object ValueGiven)
            {
                bool Success = true;
                string Result = "";

                try
                {
                    string Value = "";
                    if (ValueGiven is string) Value = (string)ValueGiven;
                    if (ValueGiven is bool) { if ((bool)ValueGiven) Value = "True"; }
                    if (Value != "" && !(ValueGiven is DBNull))
                    {
                        switch (Param)
                        {
                            case 0: //Do Not Use
                                Success = true;
                                break;
                            case 1: //Exists
                                Success = true;
                                Result = " Exists";
                                break;
                            case 2: //Does Not Exist
                                Success = false;
                                Result = " Exists";
                                break;
                            default:
                                Success = false;
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
                                Success = false;
                                Result = " Does Not Exist";
                                break;
                            case 2: //Does Not Exist
                                Success = true;
                                Result = " Does Not Exist";
                                break;
                            default:
                                Success = false;
                                break;
                        }
                    }
                }
                catch (Exception ex) { throw new NotImplementedException(ex.Message); }

                return new Eval(Success, DataName + Result);
            }
        }
        [Serializable]
        public class CBoolParam : CParam
        {
            private bool _Param = false;
            public new bool Param
            {
                get { return _Param; }
                set { _Param = value; }
            }
            public Value Elements = new Value();

            public CBoolParam() { }
            public CBoolParam(string DataName)
            {
                this.DataName = DataName;
            }

            public bool Evaluate(object ValueGiven, object DateGiven)
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
                            bool hadPos = false;
                            for (int i = 0; i < Elements.Count; i++)
                            {
                                if (Elements[i].ISNOT)
                                {
                                    switch (Elements[i].Param)
                                    {
                                        case 0: //Exists
                                            if (Value == Elements[i].Code) return true;
                                            hadPos = true;
                                            break;
                                        case 1: //In The Past
                                            if (Value == Elements[i].Code && (DateTime)DateGiven < DateTime.Now.Date) return true;
                                            hadPos = true;
                                            break;
                                        case 2: //In The Future
                                            if (Value == Elements[i].Code && (DateTime)DateGiven > DateTime.Now.Date) return true;
                                            hadPos = true;
                                            break;
                                        case 3: //Over Days Away
                                            if (Value == Elements[i].Code && (DateTime)DateGiven > DateTime.Now.Date.AddDays((double)Elements[i].Value)) return true;
                                            hadPos = true;
                                            break;
                                        case 4: //Under Days Away
                                            if (Value == Elements[i].Code && (DateTime)DateGiven <= DateTime.Now.Date.AddDays((double)Elements[i].Value)) return true;
                                            hadPos = true;
                                            break;
                                        case 5: //Over Days Ago
                                            if (Value == Elements[i].Code && (DateTime)DateGiven < DateTime.Now.Date.AddDays(-(double)Elements[i].Value)) return true;
                                            hadPos = true;
                                            break;
                                        case 6: //Under Days Ago
                                            if (Value == Elements[i].Code && (DateTime)DateGiven >= DateTime.Now.Date.AddDays(-(double)Elements[i].Value)) return true;
                                            hadPos = true;
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (Elements[i].Param)
                                    {
                                        case 0: //Exists
                                            if (Value == Elements[i].Code) return false;
                                            break;
                                        case 1: //In The Past
                                            if (Value == Elements[i].Code && (DateTime)DateGiven < DateTime.Now.Date) return false;
                                            break;
                                        case 2: //In The Future
                                            if (Value == Elements[i].Code && (DateTime)DateGiven > DateTime.Now.Date) return false;
                                            break;
                                        case 3: //Over Days Away
                                            if (Value == Elements[i].Code && (DateTime)DateGiven > DateTime.Now.Date.AddDays((double)Elements[i].Value)) return false;
                                            break;
                                        case 4: //Under Days Away
                                            if (Value == Elements[i].Code && (DateTime)DateGiven <= DateTime.Now.Date.AddDays((double)Elements[i].Value)) return false;
                                            break;
                                        case 5: //Over Days Ago
                                            if (Value == Elements[i].Code && (DateTime)DateGiven < DateTime.Now.Date.AddDays(-(double)Elements[i].Value)) return false;
                                            break;
                                        case 6: //Under Days Ago
                                            if (Value == Elements[i].Code && (DateTime)DateGiven >= DateTime.Now.Date.AddDays(-(double)Elements[i].Value)) return false;
                                            break;
                                    }
                                }
                            }
                            if (hadPos) return false;
                            else return true;
                        }
                        else if (DateGiven is DBNull || DateGiven == null)
                        {
                            bool hadPos = false;
                            for (int i = 0; i < Elements.Count; i++)
                            {
                                if (Elements[i].ISNOT)
                                {
                                    switch (Elements[i].Param)
                                    {
                                        case 0: //Exists
                                            if (Value == Elements[i].Code) return true;
                                            hadPos = true;
                                            break;
                                        default:
                                            return false;
                                    }
                                }
                                else
                                {
                                    if (Value == Elements[i].Code) return false;
                                }
                            }
                            if (hadPos) return false;
                            else return true;
                        }
                        else
                            throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Date Due To Unexpected Data Type \"" + DateGiven.GetType().ToString() + "\"");
                    }
                    else
                        throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected Data Type \"" + ValueGiven.GetType().ToString() + "\"");
                }
                catch (Exception ex) { throw new NotImplementedException(ex.Message); }
            }
            public Eval EvaluateVerbose(object ValueGiven, object DateGiven)
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
                            bool hadPos = false;
                            for (int i = 0; i < Elements.Count; i++)
                            {
                                if (Elements[i].ISNOT)
                                {
                                    switch (Elements[i].Param)
                                    {
                                        case 0: //Exists
                                            if (Value == Elements[i].Code) { Success = true; Result = " \"" + Elements[i].Code.ToString() + "\" Exists"; }
                                            hadPos = true;
                                            break;
                                        case 1: //In The Past
                                            if (Value == Elements[i].Code && (DateTime)DateGiven < DateTime.Now.Date) { Success = true; Result = " \"" + Elements[i].Code.ToString() + "\" Exists In The Past"; }
                                            hadPos = true;
                                            break;
                                        case 2: //In The Future
                                            if (Value == Elements[i].Code && (DateTime)DateGiven > DateTime.Now.Date) { Success = true; Result = " \"" + Elements[i].Code.ToString() + "\" Exists In The Future"; }
                                            hadPos = true;
                                            break;
                                        case 3: //Over Days Away
                                            if (Value == Elements[i].Code && (DateTime)DateGiven > DateTime.Now.Date.AddDays((double)Elements[i].Value)) { Success = true; Result = " \"" + Elements[i].Code.ToString() + "\" Exists More Than " + Elements[i].Value.ToString() + " Days Away"; }
                                            hadPos = true;
                                            break;
                                        case 4: //Under Days Away
                                            if (Value == Elements[i].Code && (DateTime)DateGiven <= DateTime.Now.Date.AddDays((double)Elements[i].Value)) { Success = true; Result = " \"" + Elements[i].Code.ToString() + "\" Exists Less Than " + Elements[i].Value.ToString() + " Days Away"; }
                                            hadPos = true;
                                            break;
                                        case 5: //Over Days Ago
                                            if (Value == Elements[i].Code && (DateTime)DateGiven < DateTime.Now.Date.AddDays(-(double)Elements[i].Value)) { Success = true; Result = " \"" + Elements[i].Code.ToString() + "\" Exists More Than " + Elements[i].Value.ToString() + " Days Ago"; }
                                            hadPos = true;
                                            break;
                                        case 6: //Under Days Ago
                                            if (Value == Elements[i].Code && (DateTime)DateGiven >= DateTime.Now.Date.AddDays(-(double)Elements[i].Value)) { Success = true; Result = " \"" + Elements[i].Code.ToString() + "\" Exists Less Than " + Elements[i].Value.ToString() + " Days Ago"; }
                                            hadPos = true;
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (Elements[i].Param)
                                    {
                                        case 0: //Exists
                                            if (Value == Elements[i].Code) { Success = false; Result = " \"" + Elements[i].Code.ToString() + "\" Exists"; }
                                            break;
                                        case 1: //In The Past
                                            if (Value == Elements[i].Code && (DateTime)DateGiven < DateTime.Now.Date) { Success = false; Result = " \"" + Elements[i].Code.ToString() + "\" Exists In The Past"; }
                                            break;
                                        case 2: //In The Future
                                            if (Value == Elements[i].Code && (DateTime)DateGiven > DateTime.Now.Date) { Success = false; Result = " \"" + Elements[i].Code.ToString() + "\" Exists In The Future"; }
                                            break;
                                        case 3: //Over Days Away
                                            if (Value == Elements[i].Code && (DateTime)DateGiven > DateTime.Now.Date.AddDays((double)Elements[i].Value)) { Success = false; Result = " \"" + Elements[i].Code.ToString() + "\" Exists More Than " + Elements[i].Value.ToString() + " Days Away"; }
                                            break;
                                        case 4: //Under Days Away
                                            if (Value == Elements[i].Code && (DateTime)DateGiven <= DateTime.Now.Date.AddDays((double)Elements[i].Value)) { Success = false; Result = " \"" + Elements[i].Code.ToString() + "\" Exists Less Than " + Elements[i].Value.ToString() + " Days Away"; }
                                            break;
                                        case 5: //Over Days Ago
                                            if (Value == Elements[i].Code && (DateTime)DateGiven < DateTime.Now.Date.AddDays(-(double)Elements[i].Value)) { Success = false; Result = " \"" + Elements[i].Code.ToString() + "\" Exists More Than " + Elements[i].Value.ToString() + " Days Ago"; }
                                            break;
                                        case 6: //Under Days Ago
                                            if (Value == Elements[i].Code && (DateTime)DateGiven >= DateTime.Now.Date.AddDays(-(double)Elements[i].Value)) { Success = false; Result = " \"" + Elements[i].Code.ToString() + "\" Exists Less Than " + Elements[i].Value.ToString() + " Days Ago"; }
                                            break;
                                    }
                                }
                            }
                            if (!hadPos) Success = true;
                        }
                        else if (DateGiven is DBNull || DateGiven == null)
                        {
                            bool hadPos = false;
                            for (int i = 0; i < Elements.Count; i++)
                            {
                                if (Elements[i].ISNOT)
                                {
                                    switch (Elements[i].Param)
                                    {
                                        case 0: //Exists
                                            if (Value == Elements[i].Code) { Success = true; Result = " \"" + Elements[i].Code.ToString() + "\" Found"; }
                                            hadPos = true;
                                            break;
                                        default:
                                            Success = false;
                                            Result = " No Date Existed";
                                            break;
                                    }
                                }
                                else
                                {
                                    if (Value == Elements[i].Code) { Success = false; Result = " \"" + Elements[i].Code.ToString() + "\" Exists"; }
                                }
                            }
                            if (!hadPos) Success = true;
                            else if (hadPos && !Success)
                                Result = " \"" + Value.ToString() + "\" Found";
                        }
                        else
                            throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Date Due To Unexpected Data Type \"" + DateGiven.GetType().ToString() + "\"");
                    }
                    else
                        throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected Data Type \"" + ValueGiven.GetType().ToString() + "\"");
                }
                catch (Exception ex) { throw new NotImplementedException(ex.Message); }

                return new Eval(Success, DataName + Result);
            }

            [Serializable]
            public class Value
            {
                private List<CodeElements> _Value = new List<CodeElements>();
                public CodeElements this[int index]
                {
                    get { return _Value[index]; }
                    set { _Value[index] = value; }
                }
                public List<CodeElements> Values { get { return _Value; } set { _Value = value; } }
                public int Count
                {
                    get { return _Value.Count; }
                }
                public void Add(int Code, bool isn, int Param, int Val)
                {
                    CodeElements temp = new CodeElements();
                    temp.Code = Code;
                    temp.ISNOT = isn;
                    temp.Param = Param;
                    temp.Value = Val;
                    _Value.Add(temp);
                }
            }
            [Serializable]
            public class CodeElements
            {
                public int Code;
                public bool ISNOT;
                public int Param;
                public int Value;
            }
        }
        [Serializable]
        public class COptParam : CParam
        {
            private List<int> _Values = new List<int>();
            public List<int> Values { get { return _Values; } set { _Values = value; } }

            public COptParam() { }
            public COptParam(string DataName)
            {
                this.DataName = DataName;
            }

            public bool Evaluate(object ValueGiven)
            {
                int Value = 0;
                if (ValueGiven is int) Value = (int)ValueGiven;
                else if (ValueGiven is double) Value = (int)(double)ValueGiven;
                else if (ValueGiven is decimal) Value = (int)(decimal)ValueGiven;
                if (ValueGiven is int || ValueGiven is double || ValueGiven is decimal)
                {
                    switch (Param)
                    {
                        case 0: // Do Not Use
                            return true;
                        case 1: // Does Not Exist (value==0)
                            if (Value == 0) return true;
                            else return false;
                        case 2: // Exists
                            if (Value > 0) return true;
                            else return false;
                        case 3: // In List
                            foreach (int x in Values)
                            {
                                if (x == Value) return true;
                            }
                            return false;
                        case 4: // Not In List
                            foreach (int x in Values)
                            {
                                if (x == Value) return false;
                            }
                            return true;
                        default:
                            return true;
                    }
                }
                else if (ValueGiven is DBNull)
                {
                    switch (Param)
                    {
                        case 0: // Do Not Use
                            return true;
                        case 1: // Does Not Exist (value==0)
                            return true;
                        case 4: // Not In List
                            return true;
                        default:
                            return false;
                    }
                }
                else
                {
                    throw new NotImplementedException("Could Not Evaluate \"" + DataName + "\" Due To Unexpected Data Type \"" + ValueGiven.GetType().ToString() + "\"");
                }
            }
            public Eval EvaluateVerbose(object ValueGiven)
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
                            Success = true;
                            break;
                        case 1: // Does Not Exist (value==0)
                            if (Value == 0) { Success = true; Result = " Does Not Exist"; }
                            else { Success = false; Result = " Exists"; }
                            break;
                        case 2: // Exists
                            if (Value > 0) { Success = true; Result = " Exists"; }
                            else { Success = false; Result = " Does Not Exist"; }
                            break;
                        case 3: // In List
                            foreach (int x in Values)
                            {
                                if (x == Value) { Success = true; Result = " In List"; break; }
                            }
                            if (Result == "")
                            {
                                Success = false;
                                Result = " Not In List";
                            }
                            break;
                        case 4: // Not In List
                            foreach (int x in Values)
                            {
                                if (x == Value) { Success = false; Result = " In List"; break; }
                            }
                            if (Result == "")
                            {
                                Success = true;
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
                            Success = true;
                            break;
                        case 1: // Does Not Exist (value==0)
                            Success = true;
                            Result = " Does Not Exist";
                            break;
                        case 4:
                            Success = true;
                            Result = " Does Not Exist";
                            break;
                        default:
                            Success = false;
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

        }
    }


    public class Serializer
    {
        public Serializer() { }

        public void SerializeObject(string filename, Criteria objectToSerialize)
        {
            System.IO.Stream stream = System.IO.File.Open(filename, System.IO.FileMode.Create);
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            bFormatter.Serialize(stream, objectToSerialize);
            stream.Close();
        }

        public Criteria DeSerializeObject(string filename)
        {
            Criteria objectToSerialize;
            System.IO.Stream stream = System.IO.File.Open(filename, System.IO.FileMode.Open);
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            objectToSerialize = (Criteria)bFormatter.Deserialize(stream);
            stream.Close();
            return objectToSerialize;
        }
    }

    public static class MyExtensions
    {
        public static string NumbersOnly(this string str)
        {
            Regex regex = new Regex(@"\d");
            MatchCollection match = regex.Matches(str);
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

        public static string RemoveSpecialCharacters(this string str)
        {
            Regex regex = new Regex(@"[0-9a-zA-Z\ ]");
            MatchCollection match = regex.Matches(str);
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

        public static string SSNFormat(this string input)
        {
            string temp = input.NumbersOnly();
            if (temp.Length == 9)
                return string.Format("{0}-{1}-{2}", temp.Substring(0, 3), temp.Substring(3, 2), temp.Substring(5, 4));
            else
                return temp;
        }
    }
}
