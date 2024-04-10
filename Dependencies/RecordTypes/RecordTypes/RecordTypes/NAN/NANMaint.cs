using RecordTypes.NAN.Base;
using RecordTypes.NAN.DataTypes;
using System;

namespace RecordTypes.NAN.Maintenance
{
    public class TransactionRecord : Record
    {
        #region Public Properties
        public NANString NetworkAccountNumber { get; private set; }
        public NANEnum<Enums.TransactionCode, Enums.TransactionCodeValue> TransactionCode { get; private set; }
        public NANDateTime TransactionDateTime { get; private set; }
        public NANString Filler { get; private set; }
        public virtual NANString SubTransactionArea { get; private set; }
        #endregion

        public TransactionRecord(string AccountNumber, DateTime TransactionDate, Type SubType)
        {
            this.NetworkAccountNumber = new NANString(15) { Value = AccountNumber };
            this.TransactionCode = new NANEnum<Enums.TransactionCode, Enums.TransactionCodeValue>(4);
            this.TransactionDateTime = new NANDateTime() { Value = TransactionDate };
            this.Filler = new NANString(5);
            this.SubTransactionArea = new NANString(200);

            string TType = SubType.Name;
            Enums.TransactionCode TCode = (Enums.TransactionCode)Enum.Parse(typeof(Enums.TransactionCode), TType.Substring(0, TType.Length - 7));
            this.TransactionCode.Value = TCode;
        }
        public TransactionRecord(string Record)
        {
            this.NetworkAccountNumber = new NANString(15);
            this.TransactionCode = new NANEnum<Enums.TransactionCode, Enums.TransactionCodeValue>(4);
            this.TransactionDateTime = new NANDateTime();
            this.Filler = new NANString(5);
            this.SubTransactionArea = new NANString(200);
            try
            {
                this.NetworkAccountNumber.DataString = Record;
                this.TransactionCode.DataString = Record.Substring(15);
                this.TransactionDateTime.DataString = Record.Substring(19);
                this.Filler.DataString = Record.Substring(35);
                this.SubTransactionArea.DataString = Record.Substring(40);
            }
            catch { }
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}",
                this.NetworkAccountNumber,
                this.TransactionCode,
                this.TransactionDateTime,
                this.Filler);
        }
    }
    public class TransactionRecord<T> : TransactionRecord where T : Transactions.Transaction
    {
        public new T SubTransactionArea { get; private set; }

        public TransactionRecord(string AccountNumber, DateTime TransactionDate, T Transaction) : base(AccountNumber, TransactionDate, typeof(T))
        {
            this.SubTransactionArea = Transaction;
        }
        public TransactionRecord(string Record) : base(Record)
        {
            try
            {
                this.SubTransactionArea = (T)typeof(T).GetConstructor(new Type[] { typeof(string) }).Invoke(new Object[] { Record.Substring(40, 200) });
            }
            catch { }
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}",
                this.NetworkAccountNumber,
                this.TransactionCode,
                this.TransactionDateTime,
                this.Filler,
                this.SubTransactionArea);
        }
    }

    public class NotificationRecord : Record
    {
        #region Public Properties
        public NANString ClientNumber { get; private set; }
        public NANString NetworkAccountNumber { get; private set; }
        public NANString AttorneyAccountNumber { get; private set; }
        public NANString Reserved { get; private set; }
        public NANDate TransactionDate { get; private set; }
        public NANDecimal TransactionAmount { get; private set; }
        public NANEnum<Enums.PaymentCode> PaymentTransactionCode { get; private set; }
        public NANString PaymentDescription { get; private set; }
        public NANString Filler { get; private set; }
        #endregion

        public NotificationRecord(string Record)
        {
            this.ClientNumber = new NANString(15);
            this.NetworkAccountNumber = new NANString(15);
            this.AttorneyAccountNumber = new NANString(20);
            this.Reserved = new NANString(25);
            this.TransactionDate = new NANDate();
            this.TransactionAmount = new NANDecimal(12, 2);
            this.PaymentTransactionCode = new NANEnum<Enums.PaymentCode>(5, 3);
            this.PaymentDescription = new NANString(75);
            this.Filler = new NANString(38);
            try
            {
                this.ClientNumber.DataString = Record;
                this.NetworkAccountNumber.DataString = Record.Substring(15);
                this.AttorneyAccountNumber.DataString = Record.Substring(30);
                this.Reserved.DataString = Record.Substring(50);
                this.TransactionDate.DataString = Record.Substring(75);
                this.TransactionAmount.DataString = Record.Substring(83);
                this.PaymentTransactionCode.DataString = Record.Substring(95);
                this.PaymentDescription.DataString = Record.Substring(100);
                this.Filler.DataString = Record.Substring(175);
            }
            catch { }
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}",
                this.ClientNumber,
                this.NetworkAccountNumber,
                this.AttorneyAccountNumber,
                this.Reserved,
                this.TransactionDate,
                this.TransactionAmount,
                this.PaymentTransactionCode,
                this.PaymentDescription,
                this.Filler);
        }
    }

    namespace Transactions
    {
        public interface Transaction { }
        public class JUDGMENT_SATISFIED_RECORD : Transaction
        {
            public NANString CaseNo { get; private set; }
            public NANString Filler { get; private set; }

            public JUDGMENT_SATISFIED_RECORD(string CaseNo, string Filler)
            {
                this.CaseNo = new NANString(20) { Value = CaseNo };
                this.Filler = new NANString(180) { Value = Filler };
            }
            public JUDGMENT_SATISFIED_RECORD(string SubTransactionArea)
            {
                this.CaseNo = new NANString(20);
                this.Filler = new NANString(180);
                try
                {
                    this.CaseNo.DataString = SubTransactionArea;
                    this.Filler.DataString = SubTransactionArea.Substring(20);
                }
                catch { }
            }

            public override string ToString()
            {
                return string.Format("{0}{1}",
                    this.CaseNo,
                    this.Filler);
            }
        }

        public class PAYMENT_FINANCIAL_TRANSACTION_RECORD : Transaction
        {
            #region Public Properties
            public NANString BatchNumber { get; private set; }
            public NANString PaymentID { get; private set; }
            public NANEnum<Enums.PaymentCode, Enums.PaymentCodeValue> PaymentCode { get; private set; }
            public NANDecimal GrossPaymentAmount { get; private set; }
            public NANDecimal NetPaymentAmount { get; private set; }
            public NANDecimal CommissionPaymentAmount { get; private set; }
            public NANDecimal PreviousBalance { get; private set; }
            public NANDecimal NewBalance { get; private set; }
            public NANDate PaymentTransactionDate { get; private set; }
            public NANString Filler1 { get; private set; }
            public NANString ClientCode { get; private set; }
            public NANString ClientSubCode { get; private set; }
            public NANString CostDescription { get; private set; }
            public NANString Filler2 { get; private set; }
            #endregion

            public PAYMENT_FINANCIAL_TRANSACTION_RECORD(string BatchNumber, string PaymentID, Enums.PaymentCode PaymentCode, decimal GrossPaymentAmount, decimal NetPaymentAmount, decimal CommissionPaymentAmount, decimal PreviousBalance, decimal NewBalance, DateTime PaymentTransactionDate, string ClientCode, string ClientSubCode, string CostDescription = "")
            {
                this.BatchNumber = new NANString(10) { Value = BatchNumber };
                this.PaymentID = new NANString(15) { Value = PaymentID };
                this.PaymentCode = new NANEnum<Enums.PaymentCode, Enums.PaymentCodeValue>(5) { Value = PaymentCode };
                this.GrossPaymentAmount = new NANDecimal(12, 2, false) { Value = GrossPaymentAmount };
                this.NetPaymentAmount = new NANDecimal(12, 2, false) { Value = NetPaymentAmount };
                this.CommissionPaymentAmount = new NANDecimal(12, 2, false) { Value = CommissionPaymentAmount };
                this.PreviousBalance = new NANDecimal(12, 2, false) { Value = PreviousBalance };
                this.NewBalance = new NANDecimal(12, 2, false) { Value = NewBalance };
                this.PaymentTransactionDate = new NANDate() { Value = PaymentTransactionDate };
                this.Filler1 = new NANString(2);
                this.ClientCode = new NANString(10) { Value = ClientCode };
                this.ClientSubCode = new NANString(5) { Value = ClientSubCode };
                this.CostDescription = new NANString(25);
                this.Filler2 = new NANString(60);
            }
            public PAYMENT_FINANCIAL_TRANSACTION_RECORD(string SubTransactionArea)
            {
                this.BatchNumber = new NANString(10);
                this.PaymentID = new NANString(15);
                this.PaymentCode = new NANEnum<Enums.PaymentCode, Enums.PaymentCodeValue>(5);
                this.GrossPaymentAmount = new NANDecimal(12, 2, false);
                this.NetPaymentAmount = new NANDecimal(12, 2, false);
                this.CommissionPaymentAmount = new NANDecimal(12, 2, false);
                this.PreviousBalance = new NANDecimal(12, 2, false);
                this.NewBalance = new NANDecimal(12, 2, false);
                this.PaymentTransactionDate = new NANDate();
                this.Filler1 = new NANString(2);
                this.ClientCode = new NANString(10);
                this.ClientSubCode = new NANString(5);
                this.CostDescription = new NANString(25);
                this.Filler2 = new NANString(60);
                try
                {
                    this.BatchNumber.DataString = SubTransactionArea;
                    this.PaymentID.DataString = SubTransactionArea.Substring(10);
                    this.PaymentCode.DataString = SubTransactionArea.Substring(25);
                    this.GrossPaymentAmount.DataString = SubTransactionArea.Substring(30);
                    this.NetPaymentAmount.DataString = SubTransactionArea.Substring(42);
                    this.CommissionPaymentAmount.DataString = SubTransactionArea.Substring(54);
                    this.PreviousBalance.DataString = SubTransactionArea.Substring(66);
                    this.NewBalance.DataString = SubTransactionArea.Substring(78);
                    this.PaymentTransactionDate.DataString = SubTransactionArea.Substring(90);
                    this.Filler1.DataString = SubTransactionArea.Substring(98);
                    this.ClientCode.DataString = SubTransactionArea.Substring(100);
                    this.ClientSubCode.DataString = SubTransactionArea.Substring(110);
                    this.CostDescription.DataString = SubTransactionArea.Substring(115);
                    this.Filler2.DataString = SubTransactionArea.Substring(140);
                }
                catch { }
            }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}",
                    this.BatchNumber,
                    this.PaymentID,
                    this.PaymentCode,
                    this.GrossPaymentAmount,
                    this.NetPaymentAmount,
                    this.CommissionPaymentAmount,
                    this.PreviousBalance,
                    this.NewBalance,
                    this.PaymentTransactionDate,
                    this.Filler1,
                    this.ClientCode,
                    this.ClientSubCode,
                    this.CostDescription,
                    this.Filler2);
            }
        }

        public class LAST_PAYMENT_AMOUNT_RECORD : Transaction
        {
            public NANDecimal LastPaymentAmount { get; private set; }
            public NANString Filler { get; private set; }

            public LAST_PAYMENT_AMOUNT_RECORD(string SubTransactionArea)
            {
                try
                {
                    this.LastPaymentAmount = new NANDecimal(8, 2) { DataString = SubTransactionArea };
                    this.Filler = new NANString(192) { DataString = SubTransactionArea.Substring(8) };
                }
                catch
                {
                    if (this.LastPaymentAmount == null) this.LastPaymentAmount = new NANDecimal(8, 2);
                    if (this.Filler == null) this.Filler = new NANString(198);
                }

            }

            public override string ToString()
            {
                return string.Format("{0}{1}",
                    this.LastPaymentAmount,
                    this.Filler);
            }
        }

        public class LAST_PAYMENT_DATE_RECORD : Transaction
        {
            public NANDate LastPaymentDate { get; private set; }
            public NANString Filler { get; private set; }

            public LAST_PAYMENT_DATE_RECORD(string SubTransactionArea)
            {
                try
                {
                    this.LastPaymentDate = new NANDate() { DataString = SubTransactionArea };
                    this.Filler = new NANString(192) { DataString = SubTransactionArea.Substring(8) };
                }
                catch
                {
                    if (this.LastPaymentDate == null) this.LastPaymentDate = new NANDate();
                    if (this.Filler == null) this.Filler = new NANString(198);
                }
            }

            public override string ToString()
            {
                return string.Format("{0}{1}",
                    this.LastPaymentDate,
                    this.Filler);
            }
        }

        public class DOC_REQUEST_FOR_APPLICATION_NOT_AVAILABLE_RECORD : Transaction
        {
            public NANString Filler { get; private set; }

            public DOC_REQUEST_FOR_APPLICATION_NOT_AVAILABLE_RECORD(string SubTransactionArea)
            {
                this.Filler = new NANString(200) { DataString = SubTransactionArea };
            }

            public override string ToString()
            {
                return this.Filler.ToString();
            }
        }

        public class APPLICATIONS_MAILED_TO_ATTY_BY_CLIENT_RECORD : Transaction
        {
            public NANString Filler { get; private set; }

            public APPLICATIONS_MAILED_TO_ATTY_BY_CLIENT_RECORD(string SubTransactionArea)
            {
                this.Filler = new NANString(200) { DataString = SubTransactionArea };
            }

            public override string ToString()
            {
                return this.Filler.ToString();
            }
        }

        public class BILLING_STATEMENTS_MAILED_TO_ATTY_BY_CLIENT_RECORD : Transaction
        {
            public NANString Filler { get; private set; }

            public BILLING_STATEMENTS_MAILED_TO_ATTY_BY_CLIENT_RECORD(string SubTransactionArea)
            {
                this.Filler = new NANString(200) { DataString = SubTransactionArea };
            }

            public override string ToString()
            {
                return this.Filler.ToString();
            }
        }

        public class DOCUMENTS_MAILED_TO_ATTY_BY_CLIENT_RECORD : Transaction
        {
            public NANDate MailedDate { get; private set; }
            public NANString Filler { get; private set; }

            public DOCUMENTS_MAILED_TO_ATTY_BY_CLIENT_RECORD(string SubTransactionArea)
            {
                try
                {
                    this.MailedDate = new NANDate() { DataString = SubTransactionArea };
                    this.Filler = new NANString(192) { DataString = SubTransactionArea.Substring(8) };
                }
                catch
                {
                    if (this.MailedDate == null) this.MailedDate = new NANDate();
                    if (this.Filler == null) this.Filler = new NANString(192);
                }
            }

            public override string ToString()
            {
                return string.Format("{0}{1}",
                    this.MailedDate,
                    this.Filler);
            }
        }

        public class DEALER_NAME_CHANGE_RECORD : Transaction
        {
            public NANString DealerName { get; private set; }
            public NANString Filler { get; private set; }

            public DEALER_NAME_CHANGE_RECORD(string SubTransactionArea)
            {
                try
                {
                    this.DealerName = new NANString(35) { DataString = SubTransactionArea };
                    this.Filler = new NANString(165) { DataString = SubTransactionArea.Substring(35) };
                }
                catch
                {
                    if (this.DealerName == null) this.DealerName = new NANString(35);
                    if (this.Filler == null) this.Filler = new NANString(165);
                }
            }

            public override string ToString()
            {
                return string.Format("{0}{1}",
                    this.DealerName,
                    this.Filler);
            }
        }
    }
    namespace Enums
    {
        public enum TransactionCode
        {
            UNKNOWN = 0,
            REVIEW_CODE,
            TELEPHONE_HOME_PHONE_NO_ANSWER,
            TELEPHONE_HOME_PHONE_LEFT_MESSAGE,
            TELEPHONE_DEMAND_RESIDENCE_CONTACT_MADE,
            TELEPHONE_HOME_PHONE_DEBTOR_REFUSE_TO_PAY,
            TELEPHONE_HOME_PHONE_PROMISE_TO_PAY,
            TELEPHONE_HOME_PHONE_DISCONNECTED,
            TELEPHONE_POE_NO_ANSWER,
            TELEPHONE_POE_LEFT_MESSAGE,
            TELEPHONE_DEMAND_BUSINESS_CONTACT_MADE,
            TELEPHONE_POE_PROMISE_TO_PAY,
            VERIFIED_OWNS_AUTO,
            BANK_ACCT_VERIFIED,
            POE_VERIFIED,
            PROPERTY_OWNERSHIP_VERIFIED,
            TELEPHONE_RELATIVE_LEFT_MESSAGE,
            TELEPHONE_DEBTOR_ATTORNEY_LEFT_MESSAGE,
            TELEPHONE_DIRECTORY_ASSISTANCE,
            DEBTOR_CALLED_IN_NO_RESULT,
            DEBTOR_CALLED_IN_DISPUTES_DEBT,
            DEBTOR_CALLED_IN_PROMISE_TO_PAY,
            DEBTOR_ATTORNEY_CALLED_IN,
            _341_DATE_SCHEDULED,
            BANKRUPTCY_FILED,
            _3D0_ERROR,
            ANOTHER_DEBTOR_NOT_INVOLVED_IN_BANKRUPTCY_CAN_BE_PURSUED,
            _341_MEETING_ATTENDED,
            RETURNED_MAIL_RECEIVED,
            ADDRESS_VERIFIED,
            FLIP_ACCTS_TO_ATTY,
            ACCOUNT_CLOSED,
            ACCOUNT_REOPEN,
            APPROVED_COURT_COST,
            ATTORNEY_ACKNOWLEDGEMENT,
            ADVERSARY_DISMISSAL,
            ATTORNEY_HAS_REQUESTED_AFFIDAVIT_FROM_CLIENT,
            ATTORNEY_HAS_REQUESTED_A_CODEBTOR_AFFIDAVIT_FROM_CLIENT,
            ATTORNEY_HAS_REQUESTED_A_BUSINESS_AFFIDAVIT_FROM_CLIENT,
            AFFIDAVIT_REQUEST_DECLINED,
            AFFIDAVIT_REQUEST_ERROR,
            AFFIDAVIT_REQUEST_FORCED,
            AFFIDAVIT_MERGED_PRINTED_BY_CLIENT,
            ANSWER_FILED_NO_FUNDS,
            AFFIDAVIT_REQUEST_REQUEUED,
            SIGNED_AFFIDAVIT_RECEIVED,
            AFFIDAVIT_SENT_TO_ATTY,
            HIT_ON_ASK_REQUEST,
            DOC_REQUEST_FOR_APPLICATION_NOT_AVAILABLE,
            DEF_FILED_ANSWER_W_COUNTER_CLAIM,
            RECEIVED_ANSWER_TO_COMPLAINT,
            DEFENDANT_FILED_ANSWER_TO_SUIT,
            COUNTERCLAIM_VERIFIED,
            ADVERSARY_PROCEEDING_FILED,
            INTERNAL_ONLY_INTERNAL_IMAGE_LOCATION,
            DOC_REQUEST_FOR_APPLICATION,
            APPLICATIONS_MAILED_TO_ATTY_BY_CLIENT,
            APPLICATION_RECEIVED,
            APPLICATION_ARCHIVED_SIX_MONTH_RETENTION,
            ARBITRATION_FILED,
            ACCT_RECVD_AFTER_341_DATE,
            ANSWER_TO_GARNISHMENT_FILED,
            STRAIGHT_SKIP_TRACE_REQUEST,
            ACCOUNT_AUDITED,
            BALANCE_RECONCILIATION,
            BAR_DATE_SCHEDULED,
            UPDATE_ACCT_W_BK_ATTY_INFO,
            BK_BILLING_AMOUNT,
            BK_BILLING_FILE,
            BANKRUPTCY_CHAPTER_CONVERTED,
            BANKRUPTCY_DISMISSED,
            _3D0_ERROR2,
            BANKRUPTCY_PENDING,
            BREACH_OF_CONSENT_STIPULATION_ORDER,
            INTERNAL_ONLY_INTERNAL_IMAGE_UPDATE,
            BILLING_STATEMENTS_MAILED_TO_ATTY_BY_CLIENT,
            BILLING_STATEMENT_RECEIVED,
            DOC_REQUEST_FOR_BILLING_STATEMENT,
            BILLING_STATEMENT_ARCHIVED_SIX_MONTH_RETENTION,
            UPDATE_ACCT_W_TRUSTEE_INFO,
            CHAPTER_13_CONFIRMED,
            CASE_CONTINUED,
            CHANGE_AMOUNT_PLACED,
            CANADA_ACCOUNTS_FROM_TDM_RESERVED,
            CLIENT_BATCH_DATE,
            CREDIT_BUR_REQUESTED_REVIEWED,
            CHANGE_ACCOUNT_NUMBER,
            CHANGE_COLLECTOR_NUMBER,
            CHANGE_CLIENT_NUMBER,
            CCCS_TERMS_REJECTED,
            DTR_SUBMITTED_CCCS_TERMS,
            CASE_CONTINUED2,
            CURRENT_DUE_STAGE,
            COURT_CALENDAR_HEARING_SCHEDULED,
            CONFIRMATION_HEARING_ATTENDED,
            CONFIRMATION_HEARING_SCHEDULED,
            CLIENT_REQUEST_JUDGMENT_VACATED,
            CHANGE_COMMISSION_RATE,
            CLIENT_NAME_UPDATE,
            CHARGE_OFF_ACCOUNT_TO_AGE,
            CODEBTOR_INFORMATION_VERIFIED,
            CASE_CONTESTED_ERROR,
            RECEIVED_FILED_COMPLAINT,
            CASE_CONTESTED,
            CHARGE_OFF_REVERSAL,
            CONSENT_ORDER_STIPULATION_FILED,
            CREDIT_GRANTOR,
            CLIENT_REQUEST_SUIT_DISMISSED,
            TRADE_CREDITORS_CALLED,
            DEBTOR_ATTORNEY_INFO,
            DEBTOR_ATTY_REPRESENTED_REVOCATION,
            DEBT_SETTLEMENT_COMPANY_REPRESENTATION,
            COURT_DATE_FILED,
            DECILE_SCORE_FOR_A001_ONLY,
            DO_NOT_CALL_REVOCATIONS,
            DTR_IS_DECEASED,
            DEFAULT_JUDGMENT_REQUESTED,
            DEFENDANTS_DEPOSITION,
            DEFICIENCY_UPDATE,
            DIRECTORY_ASST_MANUAL_OR_AUTOMATED,
            DEBTOR_DISPUTES_DEBT,
            DEALER_NAME_CHANGE,
            UPDATE_DEALER_NUMBER,
            DOCUMENTS_MAILED_TO_ATTY_BY_CLIENT,
            NECESSARY_DOCUMENTATION_TO_FILE_SUIT_NOT_AVAILABLE_FROM_CLIENT,
            DEBTOR_REQUEST_NO_CONTACT_AT_HOME,
            DEBTOR_NOT_EMPLOYED,
            DOCUMENTS_REQUESTED,
            DEPOSITION_TAKEN,
            DOCUMENT_REQUEST_RECEIVED,
            BANKRUPTCY_DISCHARGED,
            RECEIVED_DISCOVERY_RESPONSE,
            DTR_INTERNAL_CODE,
            EDP_NUMBER_FIELDS,
            EMPLOYEE_DISABLED,
            REVIEW_CODE2,
            WRIT_OF_EXECUTION_ON_A_JUDGMENT_RECEIVED,
            EMPLOYEE_DISABLED2,
            GARNISHMENT_FUNDS_CONDEMNED,
            COMMENT,
            FIFA_ISSUED,
            DTR_CLAIMS_FRAUD_ON_ACCT,
            ACCOUNT_FWDED_TO_ATTY_FOR_ACTION_RESERVED,
            GARNISHMENT_RELEASED_DISMISSED,
            GARNISHED_MAXIMUM_YEARLY_LIMIT_FOR_STATE,
            GARNISHMENT_FILED,
            GARNISHMENT_UNSUCCESSFUL,
            GARNISHEE_IN_DEFAULT,
            DEBTOR_FILED_CLAIM_OF_EXEMPTION,
            GARNISHMENT_IN_LINE_BEHIND_OTHER_CREDITORS,
            JUDGMENT_AGAINST_GARNISHEE_AFTER_DEFAULT,
            GLB_NOTICE_SENT_TO_DEBTOR,
            OBTAINED_WRITTEN_PERMISSION_TO_FILE_NONDISCHAREABILITY_SUIT,
            GARNISHMENT_SERVED,
            GARNISHMENT_TRAVERSE,
            CLIENT_CODE_ONLY_TO_INSTRUCT_FIRMS_TO_HOLD_OFF_ON_FURTHER_ACTION,
            INTEREST_ACCRUED_CODE,
            DEBTOR_RESIDES_ON_INDIAN_RESERVATION,
            INTERROGATORIES_RETURNED,
            POST_JUDGMENT_INTERROGATORIES_SENT,
            IRS_HAS_LIEN_ON_ATTACHABLE_ASSETS,
            INTEREST_RATE_VERIFIED,
            SUIT_ISSUED_DATE_OUT_FOR_SERVICE,
            INTERROGATORIES_FOR_DEFENDANT,
            INTERROGATORIES_FOR_PLAINTIFF,
            DEBTOR_IS_INCARCERATED,
            JUDGMENT_OBTAINED,
            JUDGMENT_ERROR,
            JUDGMENT_REJECTED_BY_COURT,
            JUDGMENT_EXPIRATION_DATE,
            JUDGMENT_HOLDING_FIRM_ID,
            NPJD_SPECIAL_INSTRUCTIONS,
            JUDGMENT_VERIFIED,
            PROPERTY_LIEN_FILED,
            ACCOUNT_IN_LITIGATION,
            LAST_PAYMENT_AMOUNT,
            LAST_PAYMENT_DATE,
            OUT_STATE_PRE_SUIT_DEMAND_LETTER,
            OUT_STATE_RIGHT_TO_CURE_LTR,
            TRIAL_FOUND_FOR_DEFENDANT,
            PIF_LETTER_SENT,
            COLLECTION_LETTER_SENT,
            SIF_LETTER_SENT,
            PROPERTY_LEVY_FILED,
            MASS_ACCOUNT_TRANSFER_CODE,
            COPY_OF_ORDER_DENYING_MOTION_FOR_RELIEF_FROM_STAY_SENT_TO_CLIENT,
            MERCHANDISE_SURRENDERED,
            REVIEWED_BY_MANAGER,
            FILED_MOTION_TO_LIFT_STAY,
            _341_MEETING_OUTSIDE_ATTYS_AREA_PHONE_LETTERS_ONLY,
            MISC_POST_JUDGMENT_MOTION,
            MOTION_FOR_RELIEF_FROM_STAY_DENIED,
            MOTION_FOR_RELEIF_FROM_STAY_GRANTED,
            MOTION_FOR_RELIEF_HEARING_RESCHEDULED,
            COPY_OF_ORDER_GRANTING_MOTION_FOR_RELIEF_FROM_STAY_SENT_TO_CLIENT,
            ATTY_FILED_MOTION_FOR_RELIEF_OF_THE_AUTOMATIC_STAY,
            MOTION_TO_COMPEL_FILED,
            NO_HIT_ON_ASK_REQUEST,
            CALLED_NEARBYS,
            NOTIFIED_CLIENT_OF_COUNTERCLAIM,
            EMPLOYER_NOT_IN_BUSINESS,
            NOTICE_TO_PRODUCE_DOCUMENTS_BY_DEFENDANT,
            NO_HIT_ON_PSK_REQUEST,
            NOTICE_TO_PRODUCE_DOCUMENTS_BY_PLAINTIFF,
            NAN_REAFFIRM,
            NO_SUIT_APPLICABLE_AT_THIS_TIME,
            ACCOUNT_OPEN_DATE_UPDATE,
            PAYMENT_ALLOCATION_METHOD,
            TELEPHONE_HOME_PHONE_PROMISE_TO_PAY2,
            POST_DATED_CHECKS_DESTROYED,
            RECEIVED_FILED_PROOF_OF_CLAIM,
            POST_DATED_CHECKS_RECEIVED,
            PROOF_OF_CLAIM_MAILED_TO_COURT,
            PAYMENT_BATCH_DEPOSIT_VERIFICATION,
            PAY_DIRECT_PENDING,
            POST_JUDGMENT_DISCOVERY_SENT,
            PLAINTIFFS_DEPOSITION,
            PRINT_SCREEN_STATMENT_MAILED,
            PHYSICAL_FILE_REVIEWED,
            PHYSICAL_FILE_SENT,
            CODEBTOR_PHONE_VERIFIED,
            POSS_CELL_PHONE_NUMBER_DO_NOT_CALL_VIA_DIALER,
            CONFIRMED_CELL_PHONE_NUMBER_OKAY_TO_CALL_VIA_DIALER,
            HIT_ON_PSK_REQUEST,
            PHONE_NUMBER_VERIFIED,
            POST_JUDGMENT_DISCOVERY,
            PMPR_RETURNED_FROM_EXPERIAN,
            PMPE_IN_ERROR_FROM_EXPERIAN,
            PAYMENT_FINANCIAL_TRANSACTION,
            POST_PETITION_DEBT,
            PRETRIAL_SCHEDULED,
            POE_SKIP_TRACE_REQUEST,
            REQUESTED_PERMISSION_TO_FILE_SUIT,
            REAFFIRMATION_AGREEMENT_OBTAINED_AT_341_MEETING,
            COURT_APPROVED_REAF_AGREEMENT_AND_ISSUED_ORDER_APPROVING_REAF,
            RECYCLE_DATE,
            RECALL_ACCOUNT,
            RECALLED_IN_ERROR,
            DEBTOR_INDICATED_AT_THE_341_MEETING_THAT_HE_INTENDS_TO_REDEEM_THE_GOODS,
            REDEMPTION_SIGNED,
            RECEPIENT_ACCT_NO,
            ACCT_REDEEMED,
            REAF_MAILED,
            SERVICE_REATTEMPTED,
            REQUEST_FOR_ADMISSION,
            REAF_FILED_CREDIT_REINSTATED,
            REAF_FILED_WITH_COURT,
            NAN_ATTORNEY_SENT_ORIGINAL_REAF_TO_CLIENT_FOR_ANALYSIS_FILING,
            REAF_SIGNED,
            REAF_APPROVAL_HEARING_RESCHEDULED,
            APPROVAL_HEARING_RESCHEDULED,
            RECEIVED_INTERROGATORIES,
            COURT_DIDNT_APPROVE_REAF_AGREEMENT,
            RESTRAINING_NOTICE_SENT,
            COURT_ORALLY_APPROVED_REAF_BUT_NOT_ISSUING_AN_ORDER_REQUESTING_TRANSCRIPT,
            APPROVAL_ORDER_MAILED_COPY_OF_ORDER_APPROVING_REAF_SENT_TO_TDM,
            PIF_REJECT_DELETED_INTERNAL_ONLY,
            PIF_CLOSE_REJECTED_INTERNAL_ONLY,
            INVOLUNTARY_REPOSSESSION,
            REPLEVIN_FILED,
            MERCHANDISE_REPO,
            VOLUNTARY_REPOSSESSION,
            REQUEST_TO_ADMIT_FOR_DEFENDANT,
            REQUEST_TO_ADMIT_FOR_PLAINTIFF,
            REAFFIRMATION_SENT_TO_COURT_FOR_FILING,
            SIF_REJECT_DELETED_INTERNAL_ONLY,
            SIF_CLOSE_REJECTED_INTERNAL_ONLY,
            DEBTOR_DIDNT_ATTEND_341_METTING_341_RESCHEDULED,
            REQUEST_TO_ADMIT_RETURNED,
            REAF_DEBTOR_WILL_RETAIN_THE_GOODS,
            NAN_ATTORNEY_HAS_RECEIVED_A_COPY_OF_THE_REAF_HEARING_TRANSCRIPT_AND_HAS_SENT_SAME_TO_CLIENT,
            REFUSE_TO_REAFFIRM,
            WRIT_OF_EXECUTION_ON_A_JUDGMENT_REQUESTED,
            SETTLEMENT_APPROVED_BY_CLIENT,
            SETTLEMENT_DENIED_BY_CLIENT,
            SETTLEMENT_REQUESTED_BY_FIRM,
            ARCHIVE_UNARCHIVE,
            JUDGMENT_SATISFIED,
            SAC_CANCEL,
            SCRUB_PERFORMED,
            SUIT_DISMISSED_WITH_PREJUDICE,
            SENT_DISCOVERY_RESPONSE,
            SKIP_EXHAUSTED,
            SKIP_LOCATED,
            CLIENT_STATES_ACCT_IS_ELIGIBLE_FOR_SETTLEMENT,
            DOC_REQUEST_FOR_SALES_SLIP_NOT_AVAILABLE,
            STIPULATION_TO_NON_DISCHARGE,
            SUBSTITUTION_OF_COUNSEL,
            STATUTE_OF_LIMITATIONS_HAS_EXPIRED,
            SCREEN_PRINT_RECEIVED,
            SCREEN_PRINT_REQUESTED,
            ATTORNEY_CHANGE,
            SOLDIERS_AND_SAILORS_ACT,
            DTR_SUIT_SCORE,
            SALES_SLIP_MAILED_TO_ATTORNEY,
            SALES_SLIP_RECEIVED,
            SALES_SLIP_STATEMENT_REQUESTED,
            SKIP_TRACE_MISCELLANEOUS_SOURCE,
            SUIT_ERROR,
            SUMMARY_JUDGMENT_REQUESTED,
            SUPPLEMENTARY_PROCEEDING_FILED,
            SUIT_FILED,
            OBTAINED_GOOD_SERVICE,
            MERCHANDISE_TO_BE_SURRENDERED,
            _6_MONTH_RE_SKIP,
            TEMPORAY_EXEMPTION_CAPITAL_ONE_SOFT_RECALL,
            FOLLOW_UP_W_TRUSTEE_ON_PMT_DISBURSEMT,
            TRIAL_DATE_SET,
            TWENTILE_SCORE_CLIENT_SCORE,
            UNABLE_TO_ATTEND_341_HEARING,
            UNABLE_TO_SERVE_NON_EST,
            SOFT_RECALL_INTERNAL_USE_ONLY,
            VACATE_JUDGMENT,
            SEND_ACCOUNT_TO_VENDOR,
            VINTAGE_UPDATE,
            VENUE_VERIFICATION_APPROVED,
            VENUE_VERIFICATION_DENIED,
            WITNESS_REQUEST_BY_ATTORNEY,
            WITNESS_NOT_AVAILABLE,
            WON_AT_TRIAL,
            WITNESS_CONFIRMED,
            CLIENT_ADDRESS_UPDATE,
            CLIENT_COMMENT_UPDATE,
            CLIENT_COMMISSION_UPDATE,
            OBJECTION_TO_CONFIRMATION_FILED
        }
        public enum TransactionCodeValue
        {
            Nothing = 0,
            _0I0,
            _110,
            _111,
            _112,
            _113,
            _114,
            _11S,
            _120,
            _121,
            _122,
            _124,
            _12A,
            _12B,
            _12P,
            _12R,
            _131,
            _150,
            _180,
            _210,
            _211,
            _214,
            _251,
            _341,
            _3D0,
            _3DE,
            _3DX,
            _3MA,
            _40A,
            _40B,
            _5E0,
            _5H0,
            _5HR,
            ACC,
            ACK,
            ADM,
            AFA,
            AFB,
            AFC,
            AFD,
            AFE,
            AFF,
            AFM,
            AFN,
            AFQ,
            AFR,
            AFS,
            AHT,
            ANA,
            ANC,
            ANR,
            ANS,
            ANV,
            APF,
            API,
            APL,
            APM,
            APR,
            APX,
            ARB,
            ARL,
            ASG,
            ASK,
            AUD,
            BAL,
            BAR,
            BAT,
            BBA,
            BBF,
            BKC,
            BKD,
            BKE,
            BKP,
            BOC,
            BSI,
            BSM,
            BSR,
            BST,
            BSX,
            BTR,
            C13,
            CAC,
            CAMT,
            CAN,
            CBD,
            CBR,
            CCA,
            CCL,
            CCN,
            CCR,
            CCS,
            CCT,
            CDS,
            CDT,
            CHA,
            CHS,
            CJV,
            CMR,
            CNU,
            COA,
            COD,
            COE,
            COM,
            CON,
            COR,
            COS,
            CRG,
            CSD,
            CTC,
            DAT,
            DAR,
            DBS,
            DCF,
            DCL,
            DCR,
            DED,
            DEF,
            DEP,
            DFC,
            DIR,
            DIS,
            DLN,
            DLR,
            DMA,
            DNA,
            DNC,
            DNE,
            DOC,
            DPT,
            DRR,
            DSC,
            DSR,
            DTR,
            EDP,
            EMP,
            EXE,
            EXJ,
            EXP,
            FCD,
            FFT,
            FIF,
            FRD,
            FWD,
            GAD,
            GAL,
            GAR,
            GAU,
            GDF,
            GEX,
            GIL,
            GJD,
            GLB,
            GOK,
            GRS,
            GTR,
            HLD,
            IAC,
            IND,
            INR,
            INT,
            IRS,
            IRT,
            ISS,
            ITD,
            ITP,
            JAL,
            JDG,
            JGE,
            JGR,
            JGX,
            JHF,
            JMB,
            JVA,
            LEN,
            LIT,
            LPA,
            LPD,
            LR1,
            LR2,
            LST,
            LTP,
            LTR,
            LTS,
            LVY,
            MAT,
            MDM,
            MDS,
            MGR,
            MLS,
            MOA,
            MPM,
            MRD,
            MRG,
            MRH,
            MRM,
            MRS,
            MTC,
            NAH,
            NB1,
            NCC,
            NIB,
            NPD,
            NPH,
            NPP,
            NRR,
            NSA,
            OPN,
            PAL,
            PAM,
            PCD,
            PCF,
            PCK,
            PCM,
            PDEP,
            PDP,
            PDS,
            PEP,
            PFM,
            PFR,
            PFS,
            PHC,
            PHD,
            PHO,
            PHT,
            PHV,
            PJD,
            PMP,
            PMPE,
            PMT,
            PPD,
            PRE,
            PSK,
            PTS,
            RAM,
            RAP,
            RCD,
            RCL,
            RCLE,
            RDM,
            RDS,
            RECN,
            RED,
            REF,
            RET,
            RFA,
            RFC,
            RFF,
            RFM,
            RFS,
            RHR,
            RHS,
            RIT,
            RNA,
            RNS,
            ROA,
            ROM,
            RPD,
            RPF,
            RPI,
            RPL,
            RPO,
            RPV,
            RQD,
            RQP,
            RSC,
            RSD,
            RSF,
            RSH,
            RTA,
            RTN,
            RTO,
            RTR,
            RWE,
            SAC,
            SAD,
            SAR,
            SARC,
            SAT,
            SAX,
            SCB,
            SDP,
            SDR,
            SKE,
            SLC,
            SLR,
            SNA,
            SND,
            SOC,
            SOL,
            SPR,
            SPT,
            SROT,
            SSA,
            SSC,
            SSM,
            SSR,
            SSS,
            STM,
            SUE,
            SUM,
            SUP,
            SUT,
            SVC,
            TBS,
            TCK,
            TEX,
            TFU,
            TRL,
            TWN,
            UTM,
            UTS,
            UUA,
            VAJ,
            VND,
            VUD,
            VVA,
            VVD,
            WIT,
            WNA,
            WON,
            WTN,
            XCAU,
            XCCM,
            XCCU,
            XCF
        }

        //public enum PaymentCode
        //{
        //     Payment_to_Attorney = 1,
        //     Payment_to_Client = 2,
        //     Payment_to_Attorney_NSF = 3,
        //     Balance_Adjustment_Pos = 4,
        //     Balance_Adjustment_Neg = 5,
        //     Payment_to_Attorney_Void = 6,
        //     Court_Costs_Advanced_Spent = 7,
        //     Court_Costs_Collected = 8,
        //     Court_Costs_Advanced_Spent_Void = 9,
        //     Court_Cost_Collected_Void = 10,
        //     Payment_to_Attorney_on_Interest = 11,
        //     Payment_to_Attorney_on_Interest_Void = 12,
        //     Interest_Adjustment_Pos = 13,
        //     Interest_Adjustment_Neg = 14,
        //     Paid_Prior_Payment_to_Attorney_Client = 15,
        //     Paid_Prior_Payment_to_Attorney_Client_Void = 16,
        //     Overpayment = 17,
        //     Payment_to_Client_NSF = 18,
        //     Attorney_Fees_Adjustment_Pos = 19,
        //     Attorney_Fees_Adjustment_Neg = 20,
        //     Settled_In_Full_Payment = 21,
        //     Payment_to_Client_Void = 22,
        //     Direct_Pay_Court_Cost_Collected = 23,
        //     Paid_Prior_Court_Cost_Advanced_Paid = 24,
        //     Paid_Prior_Court_Cost_Collected_Returned = 25,
        //     Paid_Prior_Payment_on_Interest = 26,
        //     Paid_Prior_Payment_on_Interest_Void = 27,
        //     Paid_Prior_Court_Cost_Advanced_Spent_Void = 28,
        //     Paid_Prior_Void_Court_Cost_Collected = 29,
        //     Paid_Prior_Fees_Advanced_by_Client = 30,
        //     Paid_Prior_Attorney_Fees_Collected_Void = 31,
        //     Court_Costs_Advanced_Certified_Mail = 32,
        //     Court_Costs_Advanced_Certified_Mail_Void = 33,
        //     Payment_to_Attorney_Fees_Collected = 34,
        //     Consumer_Credit_Counseling_Service_Adjustment = 35,
        //     Consumer_Credit_Counseling_Service_Void = 36,
        //     Direct_Pay_Court_Cost_Collected_Void = 37,
        //     Attorney_Fees_Collected_Void = 38,
        //     Payment_to_Client_on_Interest = 39,
        //     Payment_to_Client_on_Interest_Void = 40,
        //     Payment_to_Client_on_Attorney_Fees = 41,
        //     Payment_to_Client_on_Attorney_Fees_Void = 42,
        //     Principal_Balance_Adjustment_from_Client_Pos = 43,
        //     Principal_Balance_Adjustment_from_Client_Neg = 44,
        //     Court_Cost_Adjustment_from_Client_Pos = 45,
        //     Court_Cost_Adjustment_from_Client_Neg = 46,
        //     Attorney_Fees_Adjustment_from_Client_Pos = 47,
        //     Attorney_Fees_Adjustment_from_Client_Neg = 48,
        //     Interest_Adjustment_from_Client_Pos = 49,
        //     Interest_Adjustment_from_Client_Neg = 50,
        //     Reimbursable_Non_Collectable_Court_Costs = 100,
        //     Reimbursable_Non_Collectable_Court_Costs_Void = 101
        //}

        public enum PaymentCode
        {
            PAYMENT_TO_ATTORNEY_ON_PRINCIPAL,
            DIRECT_PAYMENT_TO_CLIENT_ON_PRINCIPAL,
            PAYMENT_TO_ATTORNEY_ON_PRINCIPAL_NSF,
            PRINCIPAL_BALANCE_ADJUSTMENT_FROM_ATTORNEY_POS,
            PRINCIPAL_BALANCE_ADJUSTMENT_FROM_ATTORNEY_NEG,
            PAYMENT_TO_ATTORNEY_ON_PRINCIPAL_VOID,
            COURT_COST_SPENT,
            PAYMENT_TO_ATTORNEY_ON_COURT_COST,
            COURT_COST_SPENT_VOID,
            PAYMENT_TO_ATTORNEY_ON_COURT_COST_NSF_VOID,
            PAYMENT_TO_ATTORNEY_ON_INTEREST,
            PAYMENT_TO_ATTORNEY_ON_INTEREST_NSF_VOID,
            INTEREST_BALANCE_ADJUSTMENT_FROM_ATTORNEY_POS,
            INTEREST_BALANCE_ADJUSTMENT_FROM_ATTORNEY_NEG,
            NON_COMMISSIONABLE_PAYMENT_TO_CLIENT_ON_PRINCIPAL,
            NON_COMMISSIONABLE_PAYMENT_TO_CLIENT_ON_PRINCIPAL_NSF_VOID,
            OVERPAYMENT,
            DIRECT_PAYMENT_TO_CLIENT_ON_PRINCIPAL_NSF,
            ATTORNEY_FEES_BALANCE_ADJUSTMENT_FROM_ATTORNEY_POS,
            ATTORNEY_FEES_BALANCE_ADJUSTMENT_FROM_ATTORNEY_NEG,
            SETTLED_IN_FULL_PAYMENT_TO_ATTORNEY_ON_PRINCIPAL,
            DIRECT_PAYMENT_TO_CLIENT_ON_PRINCIPAL_VOID,
            DIRECT_PAYMENT_TO_CLIENT_ON_COURT_COST,
            COURT_COST_SPENT_PRIOR_TO_PLACEMENT,
            NON_COMMISSIONABLE_PAYMENT_TO_CLIENT_ON_COURT_COST,
            NON_COMMISSIONABLE_PAYMENT_TO_CLIENT_ON_INTEREST,
            NON_COMMISSIONABLE_PAYMENT_TO_CLIENT_ON_INTEREST_NSF_VOID,
            COURT_COST_SPENT_VOID_PRIOR_TO_PLACEMENT,
            NON_COMMISSIONABLE_PAYMENT_TO_CLIENT_ON_COURT_COST_NSF_VOID,
            ATTORNEY_FEES_PRIOR_TO_PLACEMENT_POS,
            ATTORNEY_FEES_PRIOR_TO_PLACEMENT_NEG,
            DIRECT_PAYMENT_TO_CLIENT_ON_COURT_COST_CERTIFIED_MAIL,
            DIRECT_PAYMENT_TO_CLIENT_ON_COURT_COST_CERTIFIED_MAIL_NSF_VOID,
            PAYMENT_TO_ATTORNEY_ON_ATTORNEY_FEES,
            CCCS_COST_ADJUSTMENT,
            CCCS_COST_REVERSAL,
            DIRECT_PAYMENT_TO_CLIENT_ON_COURT_COST_NSF_VOID,
            PAYMENT_TO_ATTORNEY_ON_ATTORNEY_FEES_NSF_VOID,
            DIRECT_PAYMENT_TO_CLIENT_ON_INTEREST,
            DIRECT_PAYMENT_TO_CLIENT_ON_INTEREST_NSF_VOID,
            DIRECT_PAYMENT_TO_CLIENT_ON_ATTORNEY_FEES,
            DIRECT_PAYMENT_TO_CLIENT_ON_ATTORNEY_FEES_NSF_VOID,
            PRINCIPAL_BALANCE_ADJUSTMENT_FROM_CLIENT_POS,
            PRINCIPAL_BALANCE_ADJUSTMENT_FROM_CLIENT_NEG,
            COURT_COST_SPENT_ADJUSTMENT_FROM_CLIENT_POS,
            COURT_COST_SPENT_ADJUSTMENT_FROM_CLIENT_NEG,
            ATTORNEY_FEES_BALANCE_ADJUSTMENT_FROM_CLIENT_POS,
            ATTORNEY_FEES_BALANCE_ADJUSTMENT_FROM_CLIENT_NEG,
            INTEREST_BALANCE_ADJUSTMENT_FROM_CLIENT_POS,
            INTEREST_BALANCE_ADJUSTMENT_FROM_CLIENT_NEG,
            REIMBURSABLE_NON_COLLECTABLE_COURT_COSTS,
            REIMBURSABLE_NON_COLLECTABLE_COURT_COSTS_VOID,
            COMM_ADJ_ON_PAYMENT_TO_ATTORNEY,
            COMM_ADJ_ON_DIRECT_PAYMENT_TO_CLIENT,
            COMM_ADJ_ON_RETURNED_CHK_TO_ATTORNEY,
            COMM_ADJ_ON_BALANCE_ADJ_NO_COMM_POS,
            COMM_ADJ_ON_BALANCE_ADJ_NO_COMM_NEG,
            COMM_ADJ_ON_PAYMENT_TO_ATTORNEY_VOID,
            COURT_COST_NOT_PAID_BY_CLIENT,
            COMM_ADJ_ON_COURT_COST_COLLECTED,
            COURT_COST_NOT_PAID_BY_CLIENT_2,
            COMM_ADJ_ON_CC_COLLECTED_VOID,
            COMM_ADJ_ON_PAYMENT_ON_INTEREST,
            COMM_ADJ_ON_INTEREST_PMT_VOIDED,
            COMM_ADJ_ON_INTEREST_ADJ_NO_COMM_POS,
            COMM_ADJ_ON_INTEREST_ADJ_NO_COMM_NEG,
            COMM_ADJ_ON_PAID_PRIOR,
            COMM_ADJ_ON_PAID_PRIOR_VOIDED,
            COMM_ADJ_ON_OVERPAYMENT,
            COMM_ADJ_ON_NSF_CHECK_TO_CLIENT,
            COMM_ADJ_ON_ATTORNEY_FEES_ADJ_POS,
            COMM_ADJ_ON_ATTORNEY_FEES_ADJ_NEG,
            COMM_ADJ_ON_SETTLED_IN_FULL_PAYMENT,
            COMM_ADJ_ON_PMT_TO_CLIENT_VOID,
            COMM_ADJ_ON_DIRECT_PAY_CC_COLLECTED,
            COMM_ADJ_ON_PAID_PRIOR_COURT_COST_ADVANCED_PAID,
            COMM_ADJ_ON_PAID_PRIOR_COURT_COST_COLLECTED,
            COMM_ADJ_ON_PAID_PRIOR_INTEREST,
            COMM_ADJ_ON_PAID_PRIOR_VOIDED_INTEREST,
            COMM_ADJ_ON_PAID_PRIOR_COURT_COST_VOIDED,
            COMM_ADJ_ON_PAID_PRIOR_VOID_CC_COLLECTED,
            COMM_ADJ_ON_PAID_PRIOR_FEES_ADVANCED_BY_CLIENT,
            COMM_ADJ_ON_ATTORNEY_FEES_ADVANCED_BY_CLIENT_PAID_PRIOR,
            COMM_ADJ_ON_COURT_COST_ADVANCED_CERTIFIED_MAIL,
            COMM_ADJ_ON_COURT_COST_VOIDED_CERTIFIED_MAIL,
            COMM_ADJ_ON_ATTORNEY_FEES_COLLECTED,
            COMM_ADJ_ON_CCCS_COST_ADJUSTMENT,
            COMM_ADJ_ON_CCCS_COST_REVERSAL,
            COMM_ADJ_ON_DIRECT_PAY_COURT_COST_COLLECT_VOID,
            COMM_ADJ_ON_ATTY_FEES_COLLECTED_VOID,
            COMM_ADJ_ON_PAY_TO_CLIENT_ON_INTEREST,
            COMM_ADJ_ON_PAY_TO_CLT_ON_INTEREST_VOID,
            COMM_ADJ_ON_PAYMENT_TO_CLT_ATTY_FEES,
            COMM_ADJ_ON_PMT_TO_CLT_ATTY_FEES_VOID_NSF,
            COMM_ADJ_ON_BALANCE_ADJ_FROM_CLT_POS,
            COMM_ADJ_ON_BALANCE_ADJ_FROM_CLT_NEG,
            COMM_ADJ_ON_COURT_COST_ADJ_FROM_CLT_POS,
            COMM_ADJ_ON_COURT_COST_ADJ_FROM_CLT_NEG,
            COMM_ADJ_ON_ATTY_FEES_ADJ_FROM_CLT_POS,
            COMM_ADJ_ON_ATTY_FEES_ADJ_FROM_CLT_NEG,
            COMM_ADJ_ON_INTEREST_ADJ_FROM_CLT_POS,
            COMM_ADJ_ON_INTEREST_ADJ_FROM_CLT_NEG,
            COMMISSION_REDUCTION,
            RETURN_OF_COMMISSION,
            CCCS_COMMISSION_REDUCTION,
            FLAT_FEE_REFUND,
            FLAT_FEE_REVERSAL_ADJ,
            RESERVED_PMT_TO_CLIENT,
            SYSTEM_CALCULATED_INTEREST_ADJ,
            SYSTEM_GENERATED_INTEREST_ADJ,
            RESERVED_NSF_TO_CLIENT,
            RESERVED_VOID_BY_CLIENT
        }
        public enum PaymentCodeValue
        {
            _001,
            _002,
            _003,
            _004,
            _005,
            _006,
            _007,
            _008,
            _009,
            _010,
            _011,
            _012,
            _013,
            _014,
            _015,
            _016,
            _017,
            _018,
            _019,
            _020,
            _021,
            _022,
            _023,
            _024,
            _025,
            _026,
            _027,
            _028,
            _029,
            _030,
            _031,
            _032,
            _033,
            _034,
            _035,
            _036,
            _037,
            _038,
            _039,
            _040,
            _041,
            _042,
            _043,
            _044,
            _045,
            _046,
            _047,
            _048,
            _049,
            _050,
            _100,
            _101,
            _201,
            _202,
            _203,
            _204,
            _205,
            _206,
            _207,
            _208,
            _209,
            _210,
            _211,
            _212,
            _213,
            _214,
            _215,
            _216,
            _217,
            _218,
            _219,
            _220,
            _221,
            _222,
            _223,
            _224,
            _225,
            _226,
            _227,
            _228,
            _229,
            _230,
            _231,
            _232,
            _233,
            _234,
            _235,
            _236,
            _237,
            _238,
            _239,
            _240,
            _241,
            _242,
            _243,
            _244,
            _245,
            _246,
            _247,
            _248,
            _249,
            _250,
            _301,
            _303,
            _306,
            _777,
            _888,
            I02,
            I13,
            I14,
            I18,
            I22
        }
    }
}
