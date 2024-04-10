using RecordTypes.NAN.Base;
using RecordTypes.NAN.DataTypes;
using System;

namespace RecordTypes
{
    namespace NAN
    {
        public class NewBusinessRecord : Record
        {
            #region Public Properties
            /// <summary>
            /// NAN Account Number
            /// </summary>
            public NANString NetworkAccountNumber { get; private set; }
            /// <summary>
            /// Blank
            /// </summary>
            public NANString AttorneyAccountNumber { get; private set; }
            /// <summary>
            /// 8 characters assigned by NAN to identify the Client Portfolio
            /// </summary>
            public NANString ClientNumber { get; private set; }
            /// <summary>
            /// The name of the client that is pursuing the retrieval of funds from the person (s) listed on this file
            /// </summary>
            public NANString ClientName { get; private set; }
            /// <summary>
            /// The legal title/suit title of the company pursuing the account
            /// </summary>
            public NANString CreditGrantor { get; private set; }
            /// <summary>
            /// The name of the company that granted the credit to the debtor
            /// </summary>
            public NANString SubClientNumber { get; private set; }
            /// <summary>
            /// The number of the dealer/store where the merchandise was purchased from; this is used for later court proceeding purposes. The dealer number is assigned by the client
            /// </summary>
            public NANString StoreNumber { get; private set; }
            /// <summary>
            /// The account number that the client assigns to the debtor, when the account is first opened with the client
            /// </summary>
            public NANString ClientAccountNumber { get; private set; }
            /// <summary>
            /// The name of the dealer, where the merchandise was purchased using the client loan account information
            /// </summary>
            public NANString StoreName { get; private set; }
            /// <summary>
            /// A description of the merchandise purchased by the debtor
            /// </summary>
            public NANString DescriptionOfMerchandise { get; private set; }
            /// <summary>
            /// The name of the credit bureau company that is handling the credit maintenance of the debtor
            /// </summary>
            public NANString TradeInformation { get; private set; }
            /// <summary>
            /// The date the account was opened by the debtor with the company. The format is 19990921
            /// </summary>
            public NANDate AccountOpenDate { get; private set; }
            /// <summary>
            /// The last debtor purchased merchandise with this line of credit. The format is 19990921
            /// </summary>
            public NANDate LastPurchaseDate { get; private set; }
            /// <summary>
            /// The date the account was written off by the client due to not receiving payment from debtor. The format is 19990921
            /// </summary>
            public NANDate WriteOffDate { get; private set; }
            /// <summary>
            /// The code used to track the type of account to be placed
            /// </summary>
            public NANString CycleCode { get; private set; }
            /// <summary>
            /// The amount of money that was originally financed by the debtor. This does not include interest, court costs, or any other fees. The format is 0000000.00
            /// </summary>
            public NANDecimal PrincipalAmount { get; private set; }
            /// <summary>
            /// The total amount of money being placed for collections from the debtor. This includes principal, interest, court costs and any other fees. The format is 0000000.00
            /// </summary>
            public NANDecimal TotalAmountPlaced { get; private set; }
            /// <summary>
            /// The amount of interest that has been placed with the collection firm. This is interest that has accrued over the course of the loan. The format is 0000000.00
            /// </summary>
            public NANDecimal InterestPlaced { get; private set; }
            /// <summary>
            /// The amount of fees that the attorney would collect from the debtor. The format is 0000000.00
            /// </summary>
            public NANDecimal AttorneyFeesPlaced { get; private set; }
            /// <summary>
            /// The amount of court costs that will be collected from the debtor before any other fees. The format is 0000000.00
            /// </summary>
            public NANDecimal CourtCostPlaced { get; private set; }
            /// <summary>
            /// The amount of any additional miscellaneous fees that have been accrued over the course of the loan and collections process. The format is 0000000.00
            /// </summary>
            public NANDecimal MiscFeePlaced { get; private set; }
            /// <summary>
            /// The number of months that have past since the last payment was received by the client from the debtor
            /// </summary>
            public NANNumber MonthsPastDue { get; private set; }
            /// <summary>
            /// The date that the last payment was received by the client from the debtor. The format is 19990921
            /// </summary>
            public NANDate LastPaymentDate { get; private set; }
            /// <summary>
            /// The amount of the last payment received by the client from the customer. The format is 0000000.00
            /// </summary>
            public NANDecimal LastPaymentAmount { get; private set; }
            /// <summary>
            /// The contractual commission rate between the firm and the client
            /// </summary>
            public NANDecimal CommissionFeePercentage { get; private set; }
            /// <summary>
            /// The schedule that the attorney will follow for the client based on the number of days the account is past due
            /// </summary>
            public NANString FeeScheduleType { get; private set; }
            /// <summary>
            /// The social security number of the person listed as the debtor. The format is 111-11-1111
            /// </summary>
            public NANString Debtor_SSN { get; private set; }
            /// <summary>
            /// This is the name of the person that is listed as the debtor on the above account
            /// </summary>
            public NANString Debtor_Name { get; private set; }
            /// <summary>
            /// The debtor's address for this account
            /// </summary>
            public NANString Debtor_Address1 { get; private set; }
            /// <summary>
            /// If applicable, a second address for the debtor on this account
            /// </summary>
            public NANString Debtor_Address2 { get; private set; }
            /// <summary>
            /// The city where the debtor resides
            /// </summary>
            public NANString Debtor_City { get; private set; }
            /// <summary>
            /// The 2 digit code of the state where the debtor for this account resides
            /// </summary>
            public NANString Debtor_State { get; private set; }
            /// <summary>
            /// The zip code of the debtor on this account
            /// </summary>
            public NANString Debtor_Zip { get; private set; }
            /// <summary>
            /// The county where the debtor resides
            /// </summary>
            public NANString Debtor_County { get; private set; }
            /// <summary>
            /// The debtors previous address, listed at the time the loan was obtained
            /// </summary>
            public NANString Debtor_PreviousAddress { get; private set; }
            /// <summary>
            /// The home phone number of the debtor. The format is either 111-111-1111 or 1111111111 right justified.
            /// </summary>
            public NANString Debtor_Phone_Home { get; private set; }
            /// <summary>
            /// The work phone number of the debtor. The format is either 111-111-1111 or 1111111111 right justified.
            /// </summary>
            public NANString Debtor_Phone_Work { get; private set; }
            /// <summary>
            /// The phone extension of the debtors
            /// </summary>
            public NANString Debtor_Phone_WorkExt { get; private set; }
            /// <summary>
            /// Any other phone number that the debtor may be contacted. The format is either 111-111-1111 or 1111111111 right justified.
            /// </summary>
            public NANString Debtor_Phone_Other { get; private set; }
            /// <summary>
            /// The name of the current Place Of Employment that the debtor provided on the application
            /// </summary>
            public NANString Debtor_POE { get; private set; }
            /// <summary>
            /// The address of the current Place Of Employment that the debtor provided on the account application
            /// </summary>
            public NANString Debtor_POE_Address { get; private set; }
            /// <summary>
            /// The name of the city the debtor is currently employed
            /// </summary>
            public NANString Debtor_POE_City { get; private set; }
            /// <summary>
            /// The state of the debtor's current Place Of Employment. This field will only obtain 2 character code for the State: except Georgia
            /// </summary>
            public NANString Debtor_POE_State { get; private set; }
            /// <summary>
            /// The zip code of the Place Of Employment that the debtor provided on the loan application. The format is 11111-1111
            /// </summary>
            public NANString Debtor_POE_Zip { get; private set; }
            /// <summary>
            /// The title/position of the debtor at their Place of Employment
            /// </summary>
            public NANString Debtor_POE_Position { get; private set; }
            /// <summary>
            /// The name of the bank that holds account
            /// </summary>
            public NANString Debtor_Bank { get; private set; }
            /// <summary>
            /// The account number the bank assigned to the debtor
            /// </summary>
            public NANString Debtor_Bank_Account { get; private set; }
            /// <summary>
            /// The checking account number assigned by the debtor bank
            /// </summary>
            public NANString Debtor_Bank_CheckingAccount { get; private set; }
            /// <summary>
            /// The savings account number assigned by the debtor bank
            /// </summary>
            public NANString Debtor_Bank_SavingsAccount { get; private set; }
            /// <summary>
            /// The number of the Master Card / Visa that the Debtor currently has in their possession
            /// </summary>
            public NANString Debtor_VISA_Number { get; private set; }
            /// <summary>
            /// The name of a relative that is not currently living with the debtor that was listed on the application by the debtor
            /// </summary>
            public NANString Relative_Name { get; private set; }
            /// <summary>
            /// The phone number of the relative that was listed by the debtor on the loan account application. The format is either 111-111-1111 or 1111111111 right justified.
            /// </summary>
            public NANString Relative_Phone { get; private set; }
            /// <summary>
            /// The social security number of the first applicant, if this account is filed by 2 people. The format is 111-11-1111
            /// </summary>
            public NANString Debtor1_SSN { get; private set; }
            /// <summary>
            /// The relations of the first applicant to the second applicant on a joint loan account application
            /// </summary>
            public NANString Debtor1_Relation { get; private set; }
            /// <summary>
            /// The name of the joint account applicant
            /// </summary>
            public NANString Debtor1_Name { get; private set; }
            /// <summary>
            /// The current address of the first applicant
            /// </summary>
            public NANString Debtor1_Address { get; private set; }
            /// <summary>
            /// The city where the first applicant resides
            /// </summary>
            public NANString Debtor1_City { get; private set; }
            /// <summary>
            /// The 2 digit code of the state where the first applicant resides
            /// </summary>
            public NANString Debtor1_State { get; private set; }
            /// <summary>
            /// The zip code of the city where the first applicant resides. The format is 11111-1111
            /// </summary>
            public NANString Debtor1_Zip { get; private set; }
            /// <summary>
            /// The name of the county where the first applicant resides
            /// </summary>
            public NANString Debtor1_County { get; private set; }
            /// <summary>
            /// The home number of the first applicant. The format is either 111-111-1111 or 1111111111 right justified.
            /// </summary>
            public NANString Debtor1_Phone_Home { get; private set; }
            /// <summary>
            /// The work phone number of the first applicant. The format is either 111-111-1111 or 1111111111 right justified.
            /// </summary>
            public NANString Debtor1_Phone_Work { get; private set; }
            /// <summary>
            /// The social security number of the second applicant, only if account is joint. The format is 111-11-1111
            /// </summary>
            public NANString Debtor2_SSN { get; private set; }
            /// <summary>
            /// The relations of the second applicant to the first applicant on the application
            /// </summary>
            public NANString Debtor2_Relation { get; private set; }
            /// <summary>
            /// The name of the second applicant that is listed on a joint account application
            /// </summary>
            public NANString Debtor2_Name { get; private set; }
            /// <summary>
            /// This is the current address of the second applicant listed on the joint account application
            /// </summary>
            public NANString Debtor2_Address { get; private set; }
            /// <summary>
            /// The city where the second applicant resides
            /// </summary>
            public NANString Debtor2_City { get; private set; }
            /// <summary>
            /// The 2 digit code of the state where the second customer resides, as listed on the joint loan account application
            /// </summary>
            public NANString Debtor2_State { get; private set; }
            /// <summary>
            /// The zip code of the city where the first applicant resides as listed on the joint loan account application. The format is 11111-1111
            /// </summary>
            public NANString Debtor2_Zip { get; private set; }
            /// <summary>
            /// The county where the second applicant currently resides
            /// </summary>
            public NANString Debtor2_County { get; private set; }
            /// <summary>
            /// The home phone number of the second applicant as listed on a joint loan account application. The format is either 111-111-1111 or 1111111111 right justified.
            /// </summary>
            public NANString Debtor2_Phone_Home { get; private set; }
            /// <summary>
            /// The home phone number of the second applicant, as listed on a joint loan account application. The format is either 111-111-1111 or 1111111111 right justified.
            /// </summary>
            public NANString Debtor2_Phone_Work { get; private set; }
            /// <summary>
            /// The name of the attorney that is handling the debtor's legal services
            /// </summary>
            public NANString Debtor_Attorney { get; private set; }
            /// <summary>
            /// The address of the attorney law firm that is representing the debtor
            /// </summary>
            public NANString Debtor_Attorney_Address { get; private set; }
            /// <summary>
            /// The phone number of the law firm that is handling the debtor
            /// </summary>
            public NANString Debtor_Attorney_Phone { get; private set; }
            /// <summary>
            /// The name of the person who is listed as the trustee for the debtor
            /// </summary>
            public NANString Trustee { get; private set; }
            /// <summary>
            /// The address of the person that is listed as the trustee for the debtor
            /// </summary>
            public NANString Trustee_Address { get; private set; }
            /// <summary>
            /// The phone number of the person who is listed as the trustee for the debtors
            /// </summary>
            public NANString Trustee_Phone { get; private set; }
            /// <summary>
            /// The case number that was assigned by the bankruptcy courts
            /// </summary>
            public NANString BankruptcyCaseNumber { get; private set; }
            /// <summary>
            /// The phone number of the bankruptcy court
            /// </summary>
            public NANString Bankruptcy_Court_Phone { get; private set; }
            /// <summary>
            /// The date bankruptcy was filed with the courts by the attorney. The format is 19990921
            /// </summary>
            public NANDate BankruptcyFilingDate { get; private set; }
            /// <summary>
            /// The bankruptcy chapter the debtor is having the attorney file with the courts
            /// </summary>
            public NANString BankruptcyType { get; private set; }
            /// <summary>
            /// The amount of money the courts hand down for reaffirmation from the debtor. The format is 0000000.00
            /// </summary>
            public NANDecimal ReaffirmationAmount { get; private set; }
            /// <summary>
            /// The amount of money to be paid by the debtor to the attorney for the reaffirmation proceedings. The format is 0000000.00
            /// </summary>
            public NANDecimal ReaffirmationPaymentAmount { get; private set; }
            /// <summary>
            /// The date that the reaffirmation was signed by the debtor. The format is 19990921
            /// </summary>
            public NANDate ReaffirmationSignedDate { get; private set; }
            /// <summary>
            /// The date the reaffirmation was filed with the courts by the attorney handling the case. The format is 19990921
            /// </summary>
            public NANDate ReaffirmationFiledDate { get; private set; }
            /// <summary>
            /// The date the attorney handling the case received confirmation of the case going to court. The format is 19990921
            /// </summary>
            public NANDate ConfirmationDate { get; private set; }
            /// <summary>
            /// The date that the 341 meeting was attended by both the attorney and the debtor. The format is 19990921
            /// </summary>
            public NANDate MeetingDate_341 { get; private set; }
            /// <summary>
            /// The date that the account was discharged by the courts and the attorney law firm. The format is 19990921
            /// </summary>
            public NANDate DischargeDate { get; private set; }
            /// <summary>
            /// The date the account was redeemed by the attorney that is handles the case from the courts. The format is 19990921
            /// </summary>
            public NANDate RedeemDate { get; private set; }
            /// <summary>
            /// The amount of money to be collected based on the account being redeemed for the courts. The format is 000000.00
            /// </summary>
            public NANDecimal RedeemAmount { get; private set; }
            /// <summary>
            /// The date service was attempted by the courts with the attorney that is handling the case. The format is 19990921
            /// </summary>
            public NANDate ServiceDate { get; private set; }
            /// <summary>
            /// Explanation of the services given by the courts
            /// </summary>
            public NANString ServiceType { get; private set; }
            /// <summary>
            /// The name of the person that is listed as the defendant in the case that will be served with the court papers
            /// </summary>
            public NANString DefendantServed { get; private set; }
            /// <summary>
            /// The number that is assigned by the courts
            /// </summary>
            public NANString SuitCaseNumber { get; private set; }
            /// <summary>
            /// The case number for garnishment that the courts assign to the case
            /// </summary>
            public NANString GarnishmentCaseNumber { get; private set; }
            /// <summary>
            /// The date the case will go to the courts. The format is 19990921
            /// </summary>
            public NANDate CourtDate { get; private set; }
            /// <summary>
            /// Date that the judgment was handed down by the courts. The format is 19990921
            /// </summary>
            public NANDate JudgmentDate { get; private set; }
            /// <summary>
            /// The total amount of money awarded. This includes the judgment, principal, judgment interest, judgment attorney fees and judgment court costs. The format is 0000000.00
            /// </summary>
            public NANDecimal JudgmentTotalAmount { get; private set; }
            /// <summary>
            /// The amount of principal that is to be collected from the debtor, based on the court appointed judgment. The format is 0000000.00
            /// </summary>
            public NANDecimal JudgmentPrincipal { get; private set; }
            /// <summary>
            /// The amount of interest that is to be collected based on the judgment from the debtor in this case. The format is 0000000.00
            /// </summary>
            public NANDecimal JudgmentInterest { get; private set; }
            /// <summary>
            /// The amount of fees that the debtor, is to pay to the attorney, based in the judgment that is handed down by the courts
            /// </summary>
            public NANDecimal JudgmentAttorneyFees { get; private set; }
            /// <summary>
            /// The amount of courts cost to be paid be the debtor, to the courts, based on the judgment. The format is 0000000.00
            /// </summary>
            public NANDecimal JudgmentCourtCost { get; private set; }
            /// <summary>
            /// The case number based on the type of legal information that is being pursued
            /// </summary>
            public NANString LegalCaseNumber { get; private set; }
            /// <summary>
            /// A free form text field which contains any comments that were made on the client system since the placement of this account
            /// </summary>
            public NANString Comments { get; private set; }
            /// <summary>
            /// The name of the Place Of Employment for the first debtor on the joint loan account
            /// </summary>
            public NANString Debtor1_POE { get; private set; }
            /// <summary>
            /// The name of the Place Of Employment for the second debtor on the joint loan account
            /// </summary>
            public NANString Debtor2_POE { get; private set; }
            /// <summary>
            /// This is a logical field that corresponds to the account as to whether the Co-applicant information has been collected
            /// </summary>
            public NANBool CoDebtorInformation { get; private set; }
            /// <summary>
            /// This is a logical field that corresponds to whether this account is a commercial (business) account or is it's a regular account
            /// </summary>
            public NANBool CommercialAccount { get; private set; }
            /// <summary>
            /// This field gives an explanation of any special instructions that are necessary when processing transactions for the individual account level
            /// </summary>
            public NANString SpecialInstructions { get; private set; }
            /// <summary>
            /// This is the address of the bank where the debtor has their commercial (business) account
            /// </summary>
            public NANString Commercial_Bank_Address { get; private set; }
            /// <summary>
            /// This is the phone number of the bank that the debtor has their commercial (business) account. The format is either 111-111-1111 or 1111111111 right justified.
            /// </summary>
            public NANString Commercial_Bank_Phone { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANBool POEInformation { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANBool CommentsUponForwarding { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANBool Bank_CC_Info { get; private set; }
            /// <summary>
            /// Identifies the male debtor as a junior(JR) , etc.
            /// </summary>
            public NANString GenerationCode { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANDecimal PostJudgmentInterestRate { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANString Debtor_ServiceType { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANString Debtor1_ServiceType { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANString Debtor2_ServiceType { get; private set; }
            /// <summary>
            /// This is a reserved field
            /// </summary>
            public NANDate Reserved { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANString Filler1 { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANString EarlyOutBucket1 { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANString EarlyOutBucket2 { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANString EarlyOutBucket3 { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANString EarlyOutBucket4 { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANString EarlyOutBucket5 { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANString EarlyOutBucket6 { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANString EarlyOutBucket7 { get; private set; }
            /// <summary>
            /// This is the code that is used for the client
            /// </summary>
            public NANString SubCode_Portfolio { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANString EarlyOutBucket8 { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANString EarlyOutBucket9 { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANString EarlyOutBucket10 { get; private set; }
            /// <summary>
            /// This is the total amount of money that is past due
            /// </summary>
            public NANDecimal TotalBalance { get; private set; }
            /// <summary>
            /// This is the contractual interest rate
            /// </summary>
            public NANDecimal PreJudgmentInterestRate { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANDate VintageDate { get; private set; }
            /// <summary>
            /// The date from which the law firm should begin to accrue interest
            /// </summary>
            public NANDate InterestStartDate { get; private set; }
            /// <summary>
            /// The responsible party on a commercial account
            /// </summary>
            public NANString ResponsibleParty { get; private set; }
            /// <summary>
            /// The date of birth of the responsible party.
            /// </summary>
            public NANDate BirthDate { get; private set; }
            /// <summary>
            /// The date the debtor became deceased.
            /// </summary>
            public NANDate DeathDate { get; private set; }
            /// <summary>
            /// The collection fees that are due
            /// </summary>
            public NANDecimal PlacedCollectionFees { get; private set; }
            /// <summary>
            /// The late fees due on an account.
            /// </summary>
            public NANDecimal PlacedLateChargeFees { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANDecimal NonPlacedInterest { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANDecimal NonPlacedAttorneyFees { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANDecimal NonPlacedCourtCost { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANDecimal NonPlacedMiscFees { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANDecimal NonPlacedCollectionFees { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANDecimal NonPlacedLateChargeFees { get; private set; }
            /// <summary>
            /// The payment due date on an early out account
            /// </summary>
            public NANDate BillingDate { get; private set; }
            /// <summary>
            /// The schedule that an early out account bills
            /// </summary>
            public NANString BillingCycle { get; private set; }
            /// <summary>
            /// The time of the 341 meeting
            /// </summary>
            public NANTime MeetingTime_341 { get; private set; }
            /// <summary>
            /// Collectability score received from client
            /// </summary>
            public NANNumber ClientScore { get; private set; }
            /// <summary>
            /// The amount due on an account at the time of Charge Off
            /// </summary>
            public NANDecimal ChargeOff_Amount { get; private set; }
            /// <summary>
            /// The interest due on an account at the time of Charge Off
            /// </summary>
            public NANDecimal ChargeOff_Interest { get; private set; }
            /// <summary>
            /// The fees and costs due on an account at the time of Charge Off
            /// </summary>
            public NANDecimal ChargeOff_Other { get; private set; }
            /// <summary>
            /// The principal due on an account at the time of Charge Off
            /// </summary>
            public NANDecimal ChargeOff_Principal { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANDecimal CreditLimit { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANString Filler2 { get; private set; }
            /// <summary>
            /// The date of the receipt
            /// </summary>
            public NANDate ReceiptDate { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANDate LastStatementDate { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANDate JudgmentExpirationDate { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANDate LastContactDate { get; private set; }
            /// <summary>
            /// The date of the last credit limit adjustment
            /// </summary>
            public NANDate LastCreditChange { get; private set; }
            /// <summary>
            /// The date of the last interest change made
            /// </summary>
            public NANDate LastInterestDate { get; private set; }
            /// <summary>
            /// The amount of the last purchase made on the account
            /// </summary>
            public NANDecimal LastPurchaseAmount { get; private set; }
            /// <summary>
            /// The due date for the next payment
            /// </summary>
            public NANDate NextpaymentDate { get; private set; }
            /// <summary>
            /// The number of days since last payment
            /// </summary>
            public NANNumber NumberOfDaysDelinquent { get; private set; }
            /// <summary>
            /// The debtors' monthly income
            /// </summary>
            public NANDecimal MonthlyIncome { get; private set; }
            /// <summary>
            /// The non-wage earnings of the debtor
            /// </summary>
            public NANDecimal OtherIncome { get; private set; }
            /// <summary>
            /// The total amount a debtor claims as debt
            /// </summary>
            public NANDecimal OtherObligations { get; private set; }
            /// <summary>
            /// The amount paid that was applied to the principal balance
            /// </summary>
            public NANDecimal PrincipalPaid { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANDecimal Filler3 { get; private set; }
            /// <summary>
            /// The delinquent amount due by the debtor.
            /// </summary>
            public NANDecimal TotalPastDue { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANString Filler4 { get; private set; }
            /// <summary>
            /// The promissory note date of a loan
            /// </summary>
            public NANDate PromissoryNoteDate { get; private set; }
            /// <summary>
            /// The separation date of a loan
            /// </summary>
            public NANDate SeparationDate { get; private set; }
            /// <summary>
            /// The maturity date of a loan
            /// </summary>
            public NANDate MaturityDate { get; private set; }
            /// <summary>
            /// The default date of a loan
            /// </summary>
            public NANDate LoanDefaultDate { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANString LinkedNANNumber { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANDecimal MinimumAmountDue { get; private set; }
            /// <summary>
            /// Last Caller ID (ANI) number recovered from client
            /// </summary>
            public NANString ANINumber1 { get; private set; }
            /// <summary>
            /// Caller ID (ANI) number recovered from client
            /// </summary>
            public NANString ANINumber2 { get; private set; }
            /// <summary>
            /// Caller ID (ANI) number recovered from client
            /// </summary>
            public NANString ANINumber3 { get; private set; }
            /// <summary>
            /// Caller ID (ANI) number recovered from client
            /// </summary>
            public NANString ANINumber4 { get; private set; }
            /// <summary>
            /// Caller ID (ANI) number recovered from client
            /// </summary>
            public NANString ANINumber5 { get; private set; }
            /// <summary>
            /// Caller ID (ANI) number recovered from client
            /// </summary>
            public NANString ANINumber6 { get; private set; }
            /// <summary>
            /// Caller ID (ANI) number recovered from client
            /// </summary>
            public NANString ANINumber7 { get; private set; }
            /// <summary>
            /// Caller ID (ANI) number recovered from client
            /// </summary>
            public NANString ANINumber8 { get; private set; }
            /// <summary>
            /// Caller ID (ANI) number recovered from client
            /// </summary>
            public NANString ANINumber9 { get; private set; }
            /// <summary>
            /// Caller ID (ANI) number recovered from client
            /// </summary>
            public NANString ANINumber10 { get; private set; }
            /// <summary>
            /// User defined field-Client Scores, Suit Scores, Portfolio Tracking, etc.
            /// </summary>
            public NANString Misc1 { get; private set; }
            /// <summary>
            /// User defined field-Client Scores, Suit Scores, Portfolio Tracking, etc.
            /// </summary>
            public NANString Misc2 { get; private set; }
            /// <summary>
            /// User defined field-Client Scores, Suit Scores, Portfolio Tracking, etc.
            /// </summary>
            public NANString Misc3 { get; private set; }
            /// <summary>
            /// User defined field-Client Scores, Suit Scores, Portfolio Tracking, etc.
            /// </summary>
            public NANString Misc4 { get; private set; }
            /// <summary>
            /// User defined field-Client Scores, Suit Scores, Portfolio Tracking, etc.
            /// </summary>
            public NANString Misc5 { get; private set; }
            /// <summary>
            /// User defined field-Client Scores, Suit Scores, Portfolio Tracking, etc.
            /// </summary>
            public NANString Misc6 { get; private set; }
            /// <summary>
            /// Vehicle Identification Number
            /// </summary>
            public NANString Vehicle_VIN { get; private set; }
            /// <summary>
            /// Vehicle Make
            /// </summary>
            public NANString Vehicle_Make { get; private set; }
            /// <summary>
            /// Vehicle Model
            /// </summary>
            public NANString Vehicle_Model { get; private set; }
            /// <summary>
            /// Vehicle Year
            /// </summary>
            public NANString Vehicle_Year { get; private set; }
            /// <summary>
            /// Y/N indicates if placed address was verified by approved client procedures
            /// </summary>
            public NANBool Address_Verified { get; private set; }
            /// <summary>
            /// Additional account identification number that may be provided by clients
            /// </summary>
            public NANString ClientReferenceNumber { get; private set; }
            /// <summary>
            /// 
            /// </summary>
            public NANString Filler5 { get; private set; }
            /// <summary>
            /// The date and time of the 341 meeting
            /// </summary>
            public DateTime? MeetingDateTime_341
            {
                get
                {
                    if (this.MeetingDate_341.Value == null || this.MeetingTime_341.Value == null)
                    {
                        return null;
                    }
                    else
                    {
                        DateTime D = this.MeetingDate_341.Value.Value;
                        DateTime T = this.MeetingTime_341.Value.Value;
                        return new DateTime(D.Year, D.Month, D.Day, T.Hour, T.Minute, 0);
                    }
                }
            }
            #endregion

            public NewBusinessRecord(string Record)
            {
                try
                {
                    this.NetworkAccountNumber = new NANString(15) { DataString = Record.Substring(0) };
                    this.AttorneyAccountNumber = new NANString(15) { DataString = Record.Substring(15) };
                    this.ClientNumber = new NANString(15) { DataString = Record.Substring(30) };
                    this.ClientName = new NANString(35) { DataString = Record.Substring(45) };
                    this.CreditGrantor = new NANString(35) { DataString = Record.Substring(80) };
                    this.SubClientNumber = new NANString(10) { DataString = Record.Substring(115) };
                    this.StoreNumber = new NANString(10) { DataString = Record.Substring(125) };
                    this.ClientAccountNumber = new NANString(25) { DataString = Record.Substring(135) };
                    this.StoreName = new NANString(35) { DataString = Record.Substring(160) };
                    this.DescriptionOfMerchandise = new NANString(80) { DataString = Record.Substring(195) };
                    this.TradeInformation = new NANString(80) { DataString = Record.Substring(275) };
                    this.AccountOpenDate = new NANDate() { DataString = Record.Substring(355) };
                    this.LastPurchaseDate = new NANDate() { DataString = Record.Substring(363) };
                    this.WriteOffDate = new NANDate() { DataString = Record.Substring(371) };
                    this.CycleCode = new NANString(5) { DataString = Record.Substring(379) };
                    this.PrincipalAmount = new NANDecimal(10, 2) { DataString = Record.Substring(384) };
                    this.TotalAmountPlaced = new NANDecimal(10, 2) { DataString = Record.Substring(394) };
                    this.InterestPlaced = new NANDecimal(10, 2) { DataString = Record.Substring(404) };
                    this.AttorneyFeesPlaced = new NANDecimal(10, 2) { DataString = Record.Substring(414) };
                    this.CourtCostPlaced = new NANDecimal(10, 2) { DataString = Record.Substring(424) };
                    this.MiscFeePlaced = new NANDecimal(10, 2) { DataString = Record.Substring(434) };
                    this.MonthsPastDue = new NANNumber(3) { DataString = Record.Substring(444) };
                    this.LastPaymentDate = new NANDate() { DataString = Record.Substring(447) };
                    this.LastPaymentAmount = new NANDecimal(10, 2) { DataString = Record.Substring(455) };
                    this.CommissionFeePercentage = new NANDecimal(4, 4) { DataString = Record.Substring(465) };
                    this.FeeScheduleType = new NANString(10) { DataString = Record.Substring(469) };
                    this.Debtor_SSN = new NANString(11, false) { DataString = Record.Substring(479) };
                    this.Debtor_Name = new NANString(40) { DataString = Record.Substring(490) };
                    this.Debtor_Address1 = new NANString(40) { DataString = Record.Substring(530) };
                    this.Debtor_Address2 = new NANString(40) { DataString = Record.Substring(570) };
                    this.Debtor_City = new NANString(30) { DataString = Record.Substring(610) };
                    this.Debtor_State = new NANString(2) { DataString = Record.Substring(640) };
                    this.Debtor_Zip = new NANString(10) { DataString = Record.Substring(642) };
                    this.Debtor_County = new NANString(30) { DataString = Record.Substring(652) };
                    this.Debtor_PreviousAddress = new NANString(115) { DataString = Record.Substring(682) };
                    this.Debtor_Phone_Home = new NANString(14, false) { DataString = Record.Substring(797) };
                    this.Debtor_Phone_Work = new NANString(14, false) { DataString = Record.Substring(811) };
                    this.Debtor_Phone_WorkExt = new NANString(5) { DataString = Record.Substring(825) };
                    this.Debtor_Phone_Other = new NANString(14, false) { DataString = Record.Substring(830) };
                    this.Debtor_POE = new NANString(40) { DataString = Record.Substring(844) };
                    this.Debtor_POE_Address = new NANString(40) { DataString = Record.Substring(884) };
                    this.Debtor_POE_City = new NANString(30) { DataString = Record.Substring(924) };
                    this.Debtor_POE_State = new NANString(2) { DataString = Record.Substring(954) };
                    this.Debtor_POE_Zip = new NANString(10) { DataString = Record.Substring(956) };
                    this.Debtor_POE_Position = new NANString(25) { DataString = Record.Substring(966) };
                    this.Debtor_Bank = new NANString(40) { DataString = Record.Substring(991) };
                    this.Debtor_Bank_Account = new NANString(25) { DataString = Record.Substring(1031) };
                    this.Debtor_Bank_CheckingAccount = new NANString(25) { DataString = Record.Substring(1056) };
                    this.Debtor_Bank_SavingsAccount = new NANString(25) { DataString = Record.Substring(1081) };
                    this.Debtor_VISA_Number = new NANString(25) { DataString = Record.Substring(1106) };
                    this.Relative_Name = new NANString(40) { DataString = Record.Substring(1131) };
                    this.Relative_Phone = new NANString(14, false) { DataString = Record.Substring(1171) };
                    this.Debtor1_SSN = new NANString(11, false) { DataString = Record.Substring(1185) };
                    this.Debtor1_Relation = new NANString(20) { DataString = Record.Substring(1196) };
                    this.Debtor1_Name = new NANString(40) { DataString = Record.Substring(1216) };
                    this.Debtor1_Address = new NANString(80) { DataString = Record.Substring(1256) };
                    this.Debtor1_City = new NANString(30) { DataString = Record.Substring(1336) };
                    this.Debtor1_State = new NANString(2) { DataString = Record.Substring(1366) };
                    this.Debtor1_Zip = new NANString(10) { DataString = Record.Substring(1368) };
                    this.Debtor1_County = new NANString(30) { DataString = Record.Substring(1378) };
                    this.Debtor1_Phone_Home = new NANString(14, false) { DataString = Record.Substring(1408) };
                    this.Debtor1_Phone_Work = new NANString(14, false) { DataString = Record.Substring(1422) };
                    this.Debtor2_SSN = new NANString(11, false) { DataString = Record.Substring(1436) };
                    this.Debtor2_Relation = new NANString(20) { DataString = Record.Substring(1447) };
                    this.Debtor2_Name = new NANString(40) { DataString = Record.Substring(1467) };
                    this.Debtor2_Address = new NANString(80) { DataString = Record.Substring(1507) };
                    this.Debtor2_City = new NANString(30) { DataString = Record.Substring(1587) };
                    this.Debtor2_State = new NANString(2) { DataString = Record.Substring(1617) };
                    this.Debtor2_Zip = new NANString(10) { DataString = Record.Substring(1619) };
                    this.Debtor2_County = new NANString(30) { DataString = Record.Substring(1629) };
                    this.Debtor2_Phone_Home = new NANString(14, false) { DataString = Record.Substring(1659) };
                    this.Debtor2_Phone_Work = new NANString(14, false) { DataString = Record.Substring(1673) };
                    this.Debtor_Attorney = new NANString(40) { DataString = Record.Substring(1687) };
                    this.Debtor_Attorney_Address = new NANString(122) { DataString = Record.Substring(1727) };
                    this.Debtor_Attorney_Phone = new NANString(14, false) { DataString = Record.Substring(1849) };
                    this.Trustee = new NANString(40) { DataString = Record.Substring(1863) };
                    this.Trustee_Address = new NANString(122) { DataString = Record.Substring(1903) };
                    this.Trustee_Phone = new NANString(14, false) { DataString = Record.Substring(2025) };
                    this.BankruptcyCaseNumber = new NANString(25) { DataString = Record.Substring(2039) };
                    this.Bankruptcy_Court_Phone = new NANString(14, false) { DataString = Record.Substring(2064) };
                    this.BankruptcyFilingDate = new NANDate() { DataString = Record.Substring(2078) };
                    this.BankruptcyType = new NANString(5) { DataString = Record.Substring(2086) };
                    this.ReaffirmationAmount = new NANDecimal(10, 2) { DataString = Record.Substring(2091) };
                    this.ReaffirmationPaymentAmount = new NANDecimal(10, 2) { DataString = Record.Substring(2101) };
                    this.ReaffirmationSignedDate = new NANDate() { DataString = Record.Substring(2111) };
                    this.ReaffirmationFiledDate = new NANDate() { DataString = Record.Substring(2119) };
                    this.ConfirmationDate = new NANDate() { DataString = Record.Substring(2127) };
                    this.MeetingDate_341 = new NANDate() { DataString = Record.Substring(2135) };
                    this.DischargeDate = new NANDate() { DataString = Record.Substring(2143) };
                    this.RedeemDate = new NANDate() { DataString = Record.Substring(2151) };
                    this.RedeemAmount = new NANDecimal(10, 2) { DataString = Record.Substring(2159) };
                    this.ServiceDate = new NANDate() { DataString = Record.Substring(2169) };
                    this.ServiceType = new NANString(40) { DataString = Record.Substring(2177) };
                    this.DefendantServed = new NANString(40) { DataString = Record.Substring(2217) };
                    this.SuitCaseNumber = new NANString(25) { DataString = Record.Substring(2257) };
                    this.GarnishmentCaseNumber = new NANString(25) { DataString = Record.Substring(2282) };
                    this.CourtDate = new NANDate() { DataString = Record.Substring(2307) };
                    this.JudgmentDate = new NANDate() { DataString = Record.Substring(2315) };
                    this.JudgmentTotalAmount = new NANDecimal(10, 2) { DataString = Record.Substring(2323) };
                    this.JudgmentPrincipal = new NANDecimal(10, 2) { DataString = Record.Substring(2333) };
                    this.JudgmentInterest = new NANDecimal(10, 2) { DataString = Record.Substring(2343) };
                    this.JudgmentAttorneyFees = new NANDecimal(10, 2) { DataString = Record.Substring(2353) };
                    this.JudgmentCourtCost = new NANDecimal(10, 2) { DataString = Record.Substring(2363) };
                    this.LegalCaseNumber = new NANString(25) { DataString = Record.Substring(2373) };
                    this.Comments = new NANString(1000) { DataString = Record.Substring(2398) };
                    this.Debtor1_POE = new NANString(40) { DataString = Record.Substring(3398) };
                    this.Debtor2_POE = new NANString(40) { DataString = Record.Substring(3438) };
                    this.CoDebtorInformation = new NANBool(1, "Y", "N") { DataString = Record.Substring(3478) };
                    this.CommercialAccount = new NANBool(1, "Y", "N") { DataString = Record.Substring(3479) };
                    this.SpecialInstructions = new NANString(150) { DataString = Record.Substring(3480) };
                    this.Commercial_Bank_Address = new NANString(80) { DataString = Record.Substring(3630) };
                    this.Commercial_Bank_Phone = new NANString(14, false) { DataString = Record.Substring(3710) };
                    this.POEInformation = new NANBool(1, "Y", "N") { DataString = Record.Substring(3724) };
                    this.CommentsUponForwarding = new NANBool(1, "Y", "N") { DataString = Record.Substring(3725) };
                    this.Bank_CC_Info = new NANBool(1, "Y", "N") { DataString = Record.Substring(3726) };
                    this.GenerationCode = new NANString(3) { DataString = Record.Substring(3727) };
                    this.PostJudgmentInterestRate = new NANDecimal(6, 6) { DataString = Record.Substring(3730) };
                    this.Debtor_ServiceType = new NANString(3) { DataString = Record.Substring(3736) };
                    this.Debtor1_ServiceType = new NANString(3) { DataString = Record.Substring(3739) };
                    this.Debtor2_ServiceType = new NANString(3) { DataString = Record.Substring(3742) };
                    this.Reserved = new NANDate() { DataString = Record.Substring(3745) };
                    this.Filler1 = new NANString(10) { DataString = Record.Substring(3753) };
                    this.EarlyOutBucket1 = new NANString(7) { DataString = Record.Substring(3763) };
                    this.EarlyOutBucket2 = new NANString(7) { DataString = Record.Substring(3770) };
                    this.EarlyOutBucket3 = new NANString(7) { DataString = Record.Substring(3777) };
                    this.EarlyOutBucket4 = new NANString(7) { DataString = Record.Substring(3784) };
                    this.EarlyOutBucket5 = new NANString(7) { DataString = Record.Substring(3791) };
                    this.EarlyOutBucket6 = new NANString(7) { DataString = Record.Substring(3798) };
                    this.EarlyOutBucket7 = new NANString(7) { DataString = Record.Substring(3805) };
                    this.SubCode_Portfolio = new NANString(5) { DataString = Record.Substring(3812) };
                    this.EarlyOutBucket8 = new NANString(7) { DataString = Record.Substring(3817) };
                    this.EarlyOutBucket9 = new NANString(7) { DataString = Record.Substring(3824) };
                    this.EarlyOutBucket10 = new NANString(7) { DataString = Record.Substring(3831) };
                    this.TotalBalance = new NANDecimal(10, 2) { DataString = Record.Substring(3838) };
                    this.PreJudgmentInterestRate = new NANDecimal(4, 4) { DataString = Record.Substring(3848) };
                    this.VintageDate = new NANDate() { DataString = Record.Substring(3852) };
                    this.InterestStartDate = new NANDate() { DataString = Record.Substring(3860) };
                    this.ResponsibleParty = new NANString(40) { DataString = Record.Substring(3868) };
                    this.BirthDate = new NANDate() { DataString = Record.Substring(3908) };
                    this.DeathDate = new NANDate() { DataString = Record.Substring(3916) };
                    this.PlacedCollectionFees = new NANDecimal(10, 2) { DataString = Record.Substring(3924) };
                    this.PlacedLateChargeFees = new NANDecimal(10, 2) { DataString = Record.Substring(3934) };
                    this.NonPlacedInterest = new NANDecimal(10, 2) { DataString = Record.Substring(3944) };
                    this.NonPlacedAttorneyFees = new NANDecimal(10, 2) { DataString = Record.Substring(3954) };
                    this.NonPlacedCourtCost = new NANDecimal(10, 2) { DataString = Record.Substring(3964) };
                    this.NonPlacedMiscFees = new NANDecimal(10, 2) { DataString = Record.Substring(3974) };
                    this.NonPlacedCollectionFees = new NANDecimal(10, 2) { DataString = Record.Substring(3984) };
                    this.NonPlacedLateChargeFees = new NANDecimal(10, 2) { DataString = Record.Substring(3994) };
                    this.BillingDate = new NANDate() { DataString = Record.Substring(4004) };
                    this.BillingCycle = new NANString(2) { DataString = Record.Substring(4012) };
                    this.MeetingTime_341 = new NANTime() { DataString = Record.Substring(4014) };
                    this.ClientScore = new NANNumber(7) { DataString = Record.Substring(4019) };
                    this.ChargeOff_Amount = new NANDecimal(10, 2) { DataString = Record.Substring(4026) };
                    this.ChargeOff_Interest = new NANDecimal(10, 2) { DataString = Record.Substring(4036) };
                    this.ChargeOff_Other = new NANDecimal(10, 2) { DataString = Record.Substring(4046) };
                    this.ChargeOff_Principal = new NANDecimal(10, 2) { DataString = Record.Substring(4056) };
                    this.CreditLimit = new NANDecimal(10, 2) { DataString = Record.Substring(4066) };
                    this.Filler2 = new NANString(8) { DataString = Record.Substring(4076) };
                    this.ReceiptDate = new NANDate() { DataString = Record.Substring(4084) };
                    this.LastStatementDate = new NANDate() { DataString = Record.Substring(4092) };
                    this.JudgmentExpirationDate = new NANDate() { DataString = Record.Substring(4100) };
                    this.LastContactDate = new NANDate() { DataString = Record.Substring(4108) };
                    this.LastCreditChange = new NANDate() { DataString = Record.Substring(4116) };
                    this.LastInterestDate = new NANDate() { DataString = Record.Substring(4124) };
                    this.LastPurchaseAmount = new NANDecimal(10, 2) { DataString = Record.Substring(4132) };
                    this.NextpaymentDate = new NANDate() { DataString = Record.Substring(4142) };
                    this.NumberOfDaysDelinquent = new NANNumber(3) { DataString = Record.Substring(4150) };
                    this.MonthlyIncome = new NANDecimal(10, 2) { DataString = Record.Substring(4153) };
                    this.OtherIncome = new NANDecimal(10, 2) { DataString = Record.Substring(4163) };
                    this.OtherObligations = new NANDecimal(10, 2) { DataString = Record.Substring(4173) };
                    this.PrincipalPaid = new NANDecimal(10, 2) { DataString = Record.Substring(4183) };
                    this.Filler3 = new NANDecimal(10, 2) { DataString = Record.Substring(4193) };
                    this.TotalPastDue = new NANDecimal(10, 2) { DataString = Record.Substring(4203) };
                    this.Filler4 = new NANString(20) { DataString = Record.Substring(4213) };
                    this.PromissoryNoteDate = new NANDate() { DataString = Record.Substring(4233) };
                    this.SeparationDate = new NANDate() { DataString = Record.Substring(4241) };
                    this.MaturityDate = new NANDate() { DataString = Record.Substring(4249) };
                    this.LoanDefaultDate = new NANDate() { DataString = Record.Substring(4257) };
                    this.LinkedNANNumber = new NANString(15) { DataString = Record.Substring(4265) };
                    this.MinimumAmountDue = new NANDecimal(10, 2) { DataString = Record.Substring(4280) };
                    this.ANINumber1 = new NANString(10) { DataString = Record.Substring(4290) };
                    this.ANINumber2 = new NANString(10) { DataString = Record.Substring(4300) };
                    this.ANINumber3 = new NANString(10) { DataString = Record.Substring(4310) };
                    this.ANINumber4 = new NANString(10) { DataString = Record.Substring(4320) };
                    this.ANINumber5 = new NANString(10) { DataString = Record.Substring(4330) };
                    this.ANINumber6 = new NANString(10) { DataString = Record.Substring(4340) };
                    this.ANINumber7 = new NANString(10) { DataString = Record.Substring(4350) };
                    this.ANINumber8 = new NANString(10) { DataString = Record.Substring(4360) };
                    this.ANINumber9 = new NANString(10) { DataString = Record.Substring(4370) };
                    this.ANINumber10 = new NANString(10) { DataString = Record.Substring(4380) };
                    this.Misc1 = new NANString(10) { DataString = Record.Substring(4390) };
                    this.Misc2 = new NANString(10) { DataString = Record.Substring(4400) };
                    this.Misc3 = new NANString(10) { DataString = Record.Substring(4410) };
                    this.Misc4 = new NANString(10) { DataString = Record.Substring(4420) };
                    this.Misc5 = new NANString(10) { DataString = Record.Substring(4430) };
                    this.Misc6 = new NANString(10) { DataString = Record.Substring(4440) };
                    this.Vehicle_VIN = new NANString(20) { DataString = Record.Substring(4450) };
                    this.Vehicle_Make = new NANString(25) { DataString = Record.Substring(4470) };
                    this.Vehicle_Model = new NANString(25) { DataString = Record.Substring(4495) };
                    this.Vehicle_Year = new NANString(4) { DataString = Record.Substring(4520) };
                    this.Address_Verified = new NANBool(1, "Y", "N") { DataString = Record.Substring(4524) };
                    this.ClientReferenceNumber = new NANString(25) { DataString = Record.Substring(4525) };
                    this.Filler5 = new NANString(1448) { DataString = Record.Substring(4550) };
                }
                catch
                {
                    if (this.NetworkAccountNumber == null) this.NetworkAccountNumber = new NANString(15);
                    if (this.AttorneyAccountNumber == null) this.AttorneyAccountNumber = new NANString(15);
                    if (this.ClientNumber == null) this.ClientNumber = new NANString(15);
                    if (this.ClientName == null) this.ClientName = new NANString(35);
                    if (this.CreditGrantor == null) this.CreditGrantor = new NANString(35);
                    if (this.SubClientNumber == null) this.SubClientNumber = new NANString(10);
                    if (this.StoreNumber == null) this.StoreNumber = new NANString(10);
                    if (this.ClientAccountNumber == null) this.ClientAccountNumber = new NANString(25);
                    if (this.StoreName == null) this.StoreName = new NANString(35);
                    if (this.DescriptionOfMerchandise == null) this.DescriptionOfMerchandise = new NANString(80);
                    if (this.TradeInformation == null) this.TradeInformation = new NANString(80);
                    if (this.AccountOpenDate == null) this.AccountOpenDate = new NANDate();
                    if (this.LastPurchaseDate == null) this.LastPurchaseDate = new NANDate();
                    if (this.WriteOffDate == null) this.WriteOffDate = new NANDate();
                    if (this.CycleCode == null) this.CycleCode = new NANString(5);
                    if (this.PrincipalAmount == null) this.PrincipalAmount = new NANDecimal(10, 2);
                    if (this.TotalAmountPlaced == null) this.TotalAmountPlaced = new NANDecimal(10, 2);
                    if (this.InterestPlaced == null) this.InterestPlaced = new NANDecimal(10, 2);
                    if (this.AttorneyFeesPlaced == null) this.AttorneyFeesPlaced = new NANDecimal(10, 2);
                    if (this.CourtCostPlaced == null) this.CourtCostPlaced = new NANDecimal(10, 2);
                    if (this.MiscFeePlaced == null) this.MiscFeePlaced = new NANDecimal(10, 2);
                    if (this.MonthsPastDue == null) this.MonthsPastDue = new NANNumber(3);
                    if (this.LastPaymentDate == null) this.LastPaymentDate = new NANDate();
                    if (this.LastPaymentAmount == null) this.LastPaymentAmount = new NANDecimal(10, 2);
                    if (this.CommissionFeePercentage == null) this.CommissionFeePercentage = new NANDecimal(4, 4);
                    if (this.FeeScheduleType == null) this.FeeScheduleType = new NANString(10);
                    if (this.Debtor_SSN == null) this.Debtor_SSN = new NANString(11, false);
                    if (this.Debtor_Name == null) this.Debtor_Name = new NANString(40);
                    if (this.Debtor_Address1 == null) this.Debtor_Address1 = new NANString(40);
                    if (this.Debtor_Address2 == null) this.Debtor_Address2 = new NANString(40);
                    if (this.Debtor_City == null) this.Debtor_City = new NANString(30);
                    if (this.Debtor_State == null) this.Debtor_State = new NANString(2);
                    if (this.Debtor_Zip == null) this.Debtor_Zip = new NANString(10);
                    if (this.Debtor_County == null) this.Debtor_County = new NANString(30);
                    if (this.Debtor_PreviousAddress == null) this.Debtor_PreviousAddress = new NANString(115);
                    if (this.Debtor_Phone_Home == null) this.Debtor_Phone_Home = new NANString(14, false);
                    if (this.Debtor_Phone_Work == null) this.Debtor_Phone_Work = new NANString(14, false);
                    if (this.Debtor_Phone_WorkExt == null) this.Debtor_Phone_WorkExt = new NANString(5);
                    if (this.Debtor_Phone_Other == null) this.Debtor_Phone_Other = new NANString(14, false);
                    if (this.Debtor_POE == null) this.Debtor_POE = new NANString(40);
                    if (this.Debtor_POE_Address == null) this.Debtor_POE_Address = new NANString(40);
                    if (this.Debtor_POE_City == null) this.Debtor_POE_City = new NANString(30);
                    if (this.Debtor_POE_State == null) this.Debtor_POE_State = new NANString(2);
                    if (this.Debtor_POE_Zip == null) this.Debtor_POE_Zip = new NANString(10);
                    if (this.Debtor_POE_Position == null) this.Debtor_POE_Position = new NANString(25);
                    if (this.Debtor_Bank == null) this.Debtor_Bank = new NANString(40);
                    if (this.Debtor_Bank_Account == null) this.Debtor_Bank_Account = new NANString(25);
                    if (this.Debtor_Bank_CheckingAccount == null) this.Debtor_Bank_CheckingAccount = new NANString(25);
                    if (this.Debtor_Bank_SavingsAccount == null) this.Debtor_Bank_SavingsAccount = new NANString(25);
                    if (this.Debtor_VISA_Number == null) this.Debtor_VISA_Number = new NANString(25);
                    if (this.Relative_Name == null) this.Relative_Name = new NANString(40);
                    if (this.Relative_Phone == null) this.Relative_Phone = new NANString(14, false);
                    if (this.Debtor1_SSN == null) this.Debtor1_SSN = new NANString(11, false);
                    if (this.Debtor1_Relation == null) this.Debtor1_Relation = new NANString(20);
                    if (this.Debtor1_Name == null) this.Debtor1_Name = new NANString(40);
                    if (this.Debtor1_Address == null) this.Debtor1_Address = new NANString(80);
                    if (this.Debtor1_City == null) this.Debtor1_City = new NANString(30);
                    if (this.Debtor1_State == null) this.Debtor1_State = new NANString(2);
                    if (this.Debtor1_Zip == null) this.Debtor1_Zip = new NANString(10);
                    if (this.Debtor1_County == null) this.Debtor1_County = new NANString(30);
                    if (this.Debtor1_Phone_Home == null) this.Debtor1_Phone_Home = new NANString(14, false);
                    if (this.Debtor1_Phone_Work == null) this.Debtor1_Phone_Work = new NANString(14, false);
                    if (this.Debtor2_SSN == null) this.Debtor2_SSN = new NANString(11, false);
                    if (this.Debtor2_Relation == null) this.Debtor2_Relation = new NANString(20);
                    if (this.Debtor2_Name == null) this.Debtor2_Name = new NANString(40);
                    if (this.Debtor2_Address == null) this.Debtor2_Address = new NANString(80);
                    if (this.Debtor2_City == null) this.Debtor2_City = new NANString(30);
                    if (this.Debtor2_State == null) this.Debtor2_State = new NANString(2);
                    if (this.Debtor2_Zip == null) this.Debtor2_Zip = new NANString(10);
                    if (this.Debtor2_County == null) this.Debtor2_County = new NANString(30);
                    if (this.Debtor2_Phone_Home == null) this.Debtor2_Phone_Home = new NANString(14, false);
                    if (this.Debtor2_Phone_Work == null) this.Debtor2_Phone_Work = new NANString(14, false);
                    if (this.Debtor_Attorney == null) this.Debtor_Attorney = new NANString(40);
                    if (this.Debtor_Attorney_Address == null) this.Debtor_Attorney_Address = new NANString(122);
                    if (this.Debtor_Attorney_Phone == null) this.Debtor_Attorney_Phone = new NANString(14, false);
                    if (this.Trustee == null) this.Trustee = new NANString(40);
                    if (this.Trustee_Address == null) this.Trustee_Address = new NANString(122);
                    if (this.Trustee_Phone == null) this.Trustee_Phone = new NANString(14, false);
                    if (this.BankruptcyCaseNumber == null) this.BankruptcyCaseNumber = new NANString(25);
                    if (this.Bankruptcy_Court_Phone == null) this.Bankruptcy_Court_Phone = new NANString(14, false);
                    if (this.BankruptcyFilingDate == null) this.BankruptcyFilingDate = new NANDate();
                    if (this.BankruptcyType == null) this.BankruptcyType = new NANString(5);
                    if (this.ReaffirmationAmount == null) this.ReaffirmationAmount = new NANDecimal(10, 2);
                    if (this.ReaffirmationPaymentAmount == null) this.ReaffirmationPaymentAmount = new NANDecimal(10, 2);
                    if (this.ReaffirmationSignedDate == null) this.ReaffirmationSignedDate = new NANDate();
                    if (this.ReaffirmationFiledDate == null) this.ReaffirmationFiledDate = new NANDate();
                    if (this.ConfirmationDate == null) this.ConfirmationDate = new NANDate();
                    if (this.MeetingDate_341 == null) this.MeetingDate_341 = new NANDate();
                    if (this.DischargeDate == null) this.DischargeDate = new NANDate();
                    if (this.RedeemDate == null) this.RedeemDate = new NANDate();
                    if (this.RedeemAmount == null) this.RedeemAmount = new NANDecimal(10, 2);
                    if (this.ServiceDate == null) this.ServiceDate = new NANDate();
                    if (this.ServiceType == null) this.ServiceType = new NANString(40);
                    if (this.DefendantServed == null) this.DefendantServed = new NANString(40);
                    if (this.SuitCaseNumber == null) this.SuitCaseNumber = new NANString(25);
                    if (this.GarnishmentCaseNumber == null) this.GarnishmentCaseNumber = new NANString(25);
                    if (this.CourtDate == null) this.CourtDate = new NANDate();
                    if (this.JudgmentDate == null) this.JudgmentDate = new NANDate();
                    if (this.JudgmentTotalAmount == null) this.JudgmentTotalAmount = new NANDecimal(10, 2);
                    if (this.JudgmentPrincipal == null) this.JudgmentPrincipal = new NANDecimal(10, 2);
                    if (this.JudgmentInterest == null) this.JudgmentInterest = new NANDecimal(10, 2);
                    if (this.JudgmentAttorneyFees == null) this.JudgmentAttorneyFees = new NANDecimal(10, 2);
                    if (this.JudgmentCourtCost == null) this.JudgmentCourtCost = new NANDecimal(10, 2);
                    if (this.LegalCaseNumber == null) this.LegalCaseNumber = new NANString(25);
                    if (this.Comments == null) this.Comments = new NANString(1000);
                    if (this.Debtor1_POE == null) this.Debtor1_POE = new NANString(40);
                    if (this.Debtor2_POE == null) this.Debtor2_POE = new NANString(40);
                    if (this.CoDebtorInformation == null) this.CoDebtorInformation = new NANBool(1, "Y", "N");
                    if (this.CommercialAccount == null) this.CommercialAccount = new NANBool(1, "Y", "N");
                    if (this.SpecialInstructions == null) this.SpecialInstructions = new NANString(150);
                    if (this.Commercial_Bank_Address == null) this.Commercial_Bank_Address = new NANString(80);
                    if (this.Commercial_Bank_Phone == null) this.Commercial_Bank_Phone = new NANString(14, false);
                    if (this.POEInformation == null) this.POEInformation = new NANBool(1, "Y", "N");
                    if (this.CommentsUponForwarding == null) this.CommentsUponForwarding = new NANBool(1, "Y", "N");
                    if (this.Bank_CC_Info == null) this.Bank_CC_Info = new NANBool(1, "Y", "N");
                    if (this.GenerationCode == null) this.GenerationCode = new NANString(3);
                    if (this.PostJudgmentInterestRate == null) this.PostJudgmentInterestRate = new NANDecimal(6, 6);
                    if (this.Debtor_ServiceType == null) this.Debtor_ServiceType = new NANString(3);
                    if (this.Debtor1_ServiceType == null) this.Debtor1_ServiceType = new NANString(3);
                    if (this.Debtor2_ServiceType == null) this.Debtor2_ServiceType = new NANString(3);
                    if (this.Reserved == null) this.Reserved = new NANDate();
                    if (this.Filler1 == null) this.Filler1 = new NANString(10);
                    if (this.EarlyOutBucket1 == null) this.EarlyOutBucket1 = new NANString(7);
                    if (this.EarlyOutBucket2 == null) this.EarlyOutBucket2 = new NANString(7);
                    if (this.EarlyOutBucket3 == null) this.EarlyOutBucket3 = new NANString(7);
                    if (this.EarlyOutBucket4 == null) this.EarlyOutBucket4 = new NANString(7);
                    if (this.EarlyOutBucket5 == null) this.EarlyOutBucket5 = new NANString(7);
                    if (this.EarlyOutBucket6 == null) this.EarlyOutBucket6 = new NANString(7);
                    if (this.EarlyOutBucket7 == null) this.EarlyOutBucket7 = new NANString(7);
                    if (this.SubCode_Portfolio == null) this.SubCode_Portfolio = new NANString(5);
                    if (this.EarlyOutBucket8 == null) this.EarlyOutBucket8 = new NANString(7);
                    if (this.EarlyOutBucket9 == null) this.EarlyOutBucket9 = new NANString(7);
                    if (this.EarlyOutBucket10 == null) this.EarlyOutBucket10 = new NANString(7);
                    if (this.TotalBalance == null) this.TotalBalance = new NANDecimal(10, 2);
                    if (this.PreJudgmentInterestRate == null) this.PreJudgmentInterestRate = new NANDecimal(4, 4);
                    if (this.VintageDate == null) this.VintageDate = new NANDate();
                    if (this.InterestStartDate == null) this.InterestStartDate = new NANDate();
                    if (this.ResponsibleParty == null) this.ResponsibleParty = new NANString(40);
                    if (this.BirthDate == null) this.BirthDate = new NANDate();
                    if (this.DeathDate == null) this.DeathDate = new NANDate();
                    if (this.PlacedCollectionFees == null) this.PlacedCollectionFees = new NANDecimal(10, 2);
                    if (this.PlacedLateChargeFees == null) this.PlacedLateChargeFees = new NANDecimal(10, 2);
                    if (this.NonPlacedInterest == null) this.NonPlacedInterest = new NANDecimal(10, 2);
                    if (this.NonPlacedAttorneyFees == null) this.NonPlacedAttorneyFees = new NANDecimal(10, 2);
                    if (this.NonPlacedCourtCost == null) this.NonPlacedCourtCost = new NANDecimal(10, 2);
                    if (this.NonPlacedMiscFees == null) this.NonPlacedMiscFees = new NANDecimal(10, 2);
                    if (this.NonPlacedCollectionFees == null) this.NonPlacedCollectionFees = new NANDecimal(10, 2);
                    if (this.NonPlacedLateChargeFees == null) this.NonPlacedLateChargeFees = new NANDecimal(10, 2);
                    if (this.BillingDate == null) this.BillingDate = new NANDate();
                    if (this.BillingCycle == null) this.BillingCycle = new NANString(2);
                    if (this.MeetingTime_341 == null) this.MeetingTime_341 = new NANTime();
                    if (this.ClientScore == null) this.ClientScore = new NANNumber(7);
                    if (this.ChargeOff_Amount == null) this.ChargeOff_Amount = new NANDecimal(10, 2);
                    if (this.ChargeOff_Interest == null) this.ChargeOff_Interest = new NANDecimal(10, 2);
                    if (this.ChargeOff_Other == null) this.ChargeOff_Other = new NANDecimal(10, 2);
                    if (this.ChargeOff_Principal == null) this.ChargeOff_Principal = new NANDecimal(10, 2);
                    if (this.CreditLimit == null) this.CreditLimit = new NANDecimal(10, 2);
                    if (this.Filler2 == null) this.Filler2 = new NANString(8);
                    if (this.ReceiptDate == null) this.ReceiptDate = new NANDate();
                    if (this.LastStatementDate == null) this.LastStatementDate = new NANDate();
                    if (this.JudgmentExpirationDate == null) this.JudgmentExpirationDate = new NANDate();
                    if (this.LastContactDate == null) this.LastContactDate = new NANDate();
                    if (this.LastCreditChange == null) this.LastCreditChange = new NANDate();
                    if (this.LastInterestDate == null) this.LastInterestDate = new NANDate();
                    if (this.LastPurchaseAmount == null) this.LastPurchaseAmount = new NANDecimal(10, 2);
                    if (this.NextpaymentDate == null) this.NextpaymentDate = new NANDate();
                    if (this.NumberOfDaysDelinquent == null) this.NumberOfDaysDelinquent = new NANNumber(3);
                    if (this.MonthlyIncome == null) this.MonthlyIncome = new NANDecimal(10, 2);
                    if (this.OtherIncome == null) this.OtherIncome = new NANDecimal(10, 2);
                    if (this.OtherObligations == null) this.OtherObligations = new NANDecimal(10, 2);
                    if (this.PrincipalPaid == null) this.PrincipalPaid = new NANDecimal(10, 2);
                    if (this.Filler3 == null) this.Filler3 = new NANDecimal(10, 2);
                    if (this.TotalPastDue == null) this.TotalPastDue = new NANDecimal(10, 2);
                    if (this.Filler4 == null) this.Filler4 = new NANString(20);
                    if (this.PromissoryNoteDate == null) this.PromissoryNoteDate = new NANDate();
                    if (this.SeparationDate == null) this.SeparationDate = new NANDate();
                    if (this.MaturityDate == null) this.MaturityDate = new NANDate();
                    if (this.LoanDefaultDate == null) this.LoanDefaultDate = new NANDate();
                    if (this.LinkedNANNumber == null) this.LinkedNANNumber = new NANString(15);
                    if (this.MinimumAmountDue == null) this.MinimumAmountDue = new NANDecimal(10, 2);
                    if (this.ANINumber1 == null) this.ANINumber1 = new NANString(10);
                    if (this.ANINumber2 == null) this.ANINumber2 = new NANString(10);
                    if (this.ANINumber3 == null) this.ANINumber3 = new NANString(10);
                    if (this.ANINumber4 == null) this.ANINumber4 = new NANString(10);
                    if (this.ANINumber5 == null) this.ANINumber5 = new NANString(10);
                    if (this.ANINumber6 == null) this.ANINumber6 = new NANString(10);
                    if (this.ANINumber7 == null) this.ANINumber7 = new NANString(10);
                    if (this.ANINumber8 == null) this.ANINumber8 = new NANString(10);
                    if (this.ANINumber9 == null) this.ANINumber9 = new NANString(10);
                    if (this.ANINumber10 == null) this.ANINumber10 = new NANString(10);
                    if (this.Misc1 == null) this.Misc1 = new NANString(10);
                    if (this.Misc2 == null) this.Misc2 = new NANString(10);
                    if (this.Misc3 == null) this.Misc3 = new NANString(10);
                    if (this.Misc4 == null) this.Misc4 = new NANString(10);
                    if (this.Misc5 == null) this.Misc5 = new NANString(10);
                    if (this.Misc6 == null) this.Misc6 = new NANString(10);
                    if (this.Vehicle_VIN == null) this.Vehicle_VIN = new NANString(20);
                    if (this.Vehicle_Make == null) this.Vehicle_Make = new NANString(25);
                    if (this.Vehicle_Model == null) this.Vehicle_Model = new NANString(25);
                    if (this.Vehicle_Year == null) this.Vehicle_Year = new NANString(4);
                    if (this.Address_Verified == null) this.Address_Verified = new NANBool(1, "Y", "N");
                    if (this.ClientReferenceNumber == null) this.ClientReferenceNumber = new NANString(25);
                    if (this.Filler5 == null) this.Filler5 = new NANString(1448);
                }
            }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}{33}{34}{35}{36}{37}{38}{39}{40}{41}{42}{43}{44}{45}{46}{47}{48}{49}{50}{51}{52}{53}{54}{55}{56}{57}{58}{59}{60}{61}{62}{63}{64}{65}{66}{67}{68}{69}{70}{71}{72}{73}{74}{75}{76}{77}{78}{79}{80}{81}{82}{83}{84}{85}{86}{87}{88}{89}{90}{91}{92}{93}{94}{95}{96}{97}{98}{99}{100}{101}{102}{103}{104}{105}{106}{107}{108}{109}{110}{111}{112}{113}{114}{115}{116}{117}{118}{119}{120}{121}{122}{123}{124}{125}{126}{127}{128}{129}{130}{131}{132}{133}{134}{135}{136}{137}{138}{139}{140}{141}{142}{143}{144}{145}{146}{147}{148}{149}{150}{151}{152}{153}{154}{155}{156}{157}{158}{159}{160}{161}{162}{163}{164}{165}{166}{167}{168}{169}{170}{171}{172}{173}{174}{175}{176}{177}{178}{179}{180}{181}{182}{183}{184}{185}{186}{187}{188}{189}{190}{191}{192}{193}{194}{195}{196}{197}{198}{199}{200}{201}{202}",
                    this.NetworkAccountNumber,
                    this.AttorneyAccountNumber,
                    this.ClientNumber,
                    this.ClientName,
                    this.CreditGrantor,
                    this.SubClientNumber,
                    this.StoreNumber,
                    this.ClientAccountNumber,
                    this.StoreName,
                    this.DescriptionOfMerchandise,
                    this.TradeInformation,
                    this.AccountOpenDate,
                    this.LastPurchaseDate,
                    this.WriteOffDate,
                    this.CycleCode,
                    this.PrincipalAmount,
                    this.TotalAmountPlaced,
                    this.InterestPlaced,
                    this.AttorneyFeesPlaced,
                    this.CourtCostPlaced,
                    this.MiscFeePlaced,
                    this.MonthsPastDue,
                    this.LastPaymentDate,
                    this.LastPaymentAmount,
                    this.CommissionFeePercentage,
                    this.FeeScheduleType,
                    this.Debtor_SSN,
                    this.Debtor_Name,
                    this.Debtor_Address1,
                    this.Debtor_Address2,
                    this.Debtor_City,
                    this.Debtor_State,
                    this.Debtor_Zip,
                    this.Debtor_County,
                    this.Debtor_PreviousAddress,
                    this.Debtor_Phone_Home,
                    this.Debtor_Phone_Work,
                    this.Debtor_Phone_WorkExt,
                    this.Debtor_Phone_Other,
                    this.Debtor_POE,
                    this.Debtor_POE_Address,
                    this.Debtor_POE_City,
                    this.Debtor_POE_State,
                    this.Debtor_POE_Zip,
                    this.Debtor_POE_Position,
                    this.Debtor_Bank,
                    this.Debtor_Bank_Account,
                    this.Debtor_Bank_CheckingAccount,
                    this.Debtor_Bank_SavingsAccount,
                    this.Debtor_VISA_Number,
                    this.Relative_Name,
                    this.Relative_Phone,
                    this.Debtor1_SSN,
                    this.Debtor1_Relation,
                    this.Debtor1_Name,
                    this.Debtor1_Address,
                    this.Debtor1_City,
                    this.Debtor1_State,
                    this.Debtor1_Zip,
                    this.Debtor1_County,
                    this.Debtor1_Phone_Home,
                    this.Debtor1_Phone_Work,
                    this.Debtor2_SSN,
                    this.Debtor2_Relation,
                    this.Debtor2_Name,
                    this.Debtor2_Address,
                    this.Debtor2_City,
                    this.Debtor2_State,
                    this.Debtor2_Zip,
                    this.Debtor2_County,
                    this.Debtor2_Phone_Home,
                    this.Debtor2_Phone_Work,
                    this.Debtor_Attorney,
                    this.Debtor_Attorney_Address,
                    this.Debtor_Attorney_Phone,
                    this.Trustee,
                    this.Trustee_Address,
                    this.Trustee_Phone,
                    this.BankruptcyCaseNumber,
                    this.Bankruptcy_Court_Phone,
                    this.BankruptcyFilingDate,
                    this.BankruptcyType,
                    this.ReaffirmationAmount,
                    this.ReaffirmationPaymentAmount,
                    this.ReaffirmationSignedDate,
                    this.ReaffirmationFiledDate,
                    this.ConfirmationDate,
                    this.MeetingDate_341,
                    this.DischargeDate,
                    this.RedeemDate,
                    this.RedeemAmount,
                    this.ServiceDate,
                    this.ServiceType,
                    this.DefendantServed,
                    this.SuitCaseNumber,
                    this.GarnishmentCaseNumber,
                    this.CourtDate,
                    this.JudgmentDate,
                    this.JudgmentTotalAmount,
                    this.JudgmentPrincipal,
                    this.JudgmentInterest,
                    this.JudgmentAttorneyFees,
                    this.JudgmentCourtCost,
                    this.LegalCaseNumber,
                    this.Comments,
                    this.Debtor1_POE,
                    this.Debtor2_POE,
                    this.CoDebtorInformation,
                    this.CommercialAccount,
                    this.SpecialInstructions,
                    this.Commercial_Bank_Address,
                    this.Commercial_Bank_Phone,
                    this.POEInformation,
                    this.CommentsUponForwarding,
                    this.Bank_CC_Info,
                    this.GenerationCode,
                    this.PostJudgmentInterestRate,
                    this.Debtor_ServiceType,
                    this.Debtor1_ServiceType,
                    this.Debtor2_ServiceType,
                    this.Reserved,
                    this.Filler1,
                    this.EarlyOutBucket1,
                    this.EarlyOutBucket2,
                    this.EarlyOutBucket3,
                    this.EarlyOutBucket4,
                    this.EarlyOutBucket5,
                    this.EarlyOutBucket6,
                    this.EarlyOutBucket7,
                    this.SubCode_Portfolio,
                    this.EarlyOutBucket8,
                    this.EarlyOutBucket9,
                    this.EarlyOutBucket10,
                    this.TotalBalance,
                    this.PreJudgmentInterestRate,
                    this.VintageDate,
                    this.InterestStartDate,
                    this.ResponsibleParty,
                    this.BirthDate,
                    this.DeathDate,
                    this.PlacedCollectionFees,
                    this.PlacedLateChargeFees,
                    this.NonPlacedInterest,
                    this.NonPlacedAttorneyFees,
                    this.NonPlacedCourtCost,
                    this.NonPlacedMiscFees,
                    this.NonPlacedCollectionFees,
                    this.NonPlacedLateChargeFees,
                    this.BillingDate,
                    this.BillingCycle,
                    this.MeetingTime_341,
                    this.ClientScore,
                    this.ChargeOff_Amount,
                    this.ChargeOff_Interest,
                    this.ChargeOff_Other,
                    this.ChargeOff_Principal,
                    this.CreditLimit,
                    this.Filler2,
                    this.ReceiptDate,
                    this.LastStatementDate,
                    this.JudgmentExpirationDate,
                    this.LastContactDate,
                    this.LastCreditChange,
                    this.LastInterestDate,
                    this.LastPurchaseAmount,
                    this.NextpaymentDate,
                    this.NumberOfDaysDelinquent,
                    this.MonthlyIncome,
                    this.OtherIncome,
                    this.OtherObligations,
                    this.PrincipalPaid,
                    this.Filler3,
                    this.TotalPastDue,
                    this.Filler4,
                    this.PromissoryNoteDate,
                    this.SeparationDate,
                    this.MaturityDate,
                    this.LoanDefaultDate,
                    this.LinkedNANNumber,
                    this.MinimumAmountDue,
                    this.ANINumber1,
                    this.ANINumber2,
                    this.ANINumber3,
                    this.ANINumber4,
                    this.ANINumber5,
                    this.ANINumber6,
                    this.ANINumber7,
                    this.ANINumber8,
                    this.ANINumber9,
                    this.ANINumber10,
                    this.Misc1,
                    this.Misc2,
                    this.Misc3,
                    this.Misc4,
                    this.Misc5,
                    this.Misc6,
                    this.Vehicle_VIN,
                    this.Vehicle_Make,
                    this.Vehicle_Model,
                    this.Vehicle_Year,
                    this.Address_Verified,
                    this.ClientReferenceNumber,
                    this.Filler5);
            }
        }
    }
}
