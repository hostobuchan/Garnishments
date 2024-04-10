using RecordTypes.Banko.Base.DataTypes;
using RecordTypes.Banko.Base.Enums;
using System.Linq;

namespace RecordTypes.Banko
{
    public class BankruptcyReturnFile
    {
        #region Properties
        public BankoString CustomerAccount { get; private set; }
        public BankoEnum<HitType> HitType { get; private set; }
        public BankoString Case { get; private set; }
        public BankoEnum<ChapterCode> Chapter { get; private set; }
        public BankoDate Filed_Date { get; private set; }
        public BankoDate StatusDate { get; private set; }
        public BankoEnum<DispositionCode> Disposition { get; private set; }
        public BankoString Suffix { get; private set; }
        public BankoString Address { get; private set; }
        public BankoString Zip { get; private set; }
        public BankoString SSN { get; private set; }
        public BankoString Filed_City { get; private set; }
        public BankoString Filed_State { get; private set; }
        public BankoString County { get; private set; }
        public BankoString FirstName { get; private set; }
        public BankoString MiddleName { get; private set; }
        public BankoString LastName { get; private set; }
        public BankoString Business { get; private set; }
        public BankoString Business1 { get; private set; }
        public BankoString Business2 { get; private set; }
        public BankoString Business3 { get; private set; }
        public BankoString City { get; private set; }
        public BankoString State { get; private set; }
        public BankoEnum<ECOA, ECOAValues> ECOA { get; private set; }
        public BankoString LawFirm { get; private set; }
        public BankoString Attorney { get; private set; }
        public BankoString Attorney_Address { get; private set; }
        public BankoString Attorney_City { get; private set; }
        public BankoString Attorney_State { get; private set; }
        public BankoString Attorney_Zip { get; private set; }
        public BankoString Attorney_Phone { get; private set; }
        public BankoDateTime Meeting341_DateTime { get; private set; }
        public BankoString Meeting341_Location { get; private set; }
        public BankoString Trustee { get; private set; }
        public BankoString Trustee_Address { get; private set; }
        public BankoString Trustee_City { get; private set; }
        public BankoString Trustee_State { get; private set; }
        public BankoString Trustee_Zip { get; private set; }
        public BankoString Trustee_Phone { get; private set; }
        public BankoString JudgeInitials { get; private set; }
        public BankoBool FundsAvailable { get; private set; }
        public BankoDate DischargeComplaintBarDate { get; private set; }
        public BankoMatches MatchCode { get; private set; }
        public BankoNumber ClientField { get; private set; }
        public BankoString Court_District { get; private set; }
        public BankoString Court_Address1 { get; private set; }
        public BankoString Court_Address2 { get; private set; }
        public BankoString Court_City_Mailing { get; private set; }
        public BankoString Court_Zip { get; private set; }
        public BankoString Court_Phone { get; private set; }
        public BankoString Phone { get; private set; }
        public BankoEnum<Congeniality, CongenialityValues> DismissalVoluntary { get; private set; }
        public BankoDate ProofOfClaimBarDate { get; private set; }
        public string FileNo { get { try { return this.CustomerAccount.Value.Split('-')[0]; } catch { return this.CustomerAccount.Value; } } }
        public int DebtorNumber { get { try { return int.Parse(this.CustomerAccount.Value.Split('-')[1]); } catch { return 1; } } }
        #endregion

        public BankruptcyReturnFile(string Record)
        {
            #region Instantiate Properties
            this.CustomerAccount = new BankoString(25);
            this.HitType = new BankoEnum<HitType>(1);
            this.Case = new BankoString(12);
            this.Chapter = new BankoEnum<ChapterCode>(2);
            this.Filed_Date = new BankoDate();
            this.StatusDate = new BankoDate();
            this.Disposition = new BankoEnum<DispositionCode>(2);
            this.Suffix = new BankoString(1);
            this.Address = new BankoString(32);
            this.Zip = new BankoString(10);
            this.SSN = new BankoString(9);
            this.Filed_City = new BankoString(20);
            this.Filed_State = new BankoString(2);
            this.County = new BankoString(25);
            this.FirstName = new BankoString(15);
            this.MiddleName = new BankoString(10);
            this.LastName = new BankoString(25);
            this.Business = new BankoString(45);
            this.Business1 = new BankoString(45);
            this.Business2 = new BankoString(45);
            this.Business3 = new BankoString(45);
            this.City = new BankoString(30);
            this.State = new BankoString(2);
            this.ECOA = new BankoEnum<ECOA, ECOAValues>(1);
            this.LawFirm = new BankoString(50);
            this.Attorney = new BankoString(35);
            this.Attorney_Address = new BankoString(32);
            this.Attorney_City = new BankoString(25);
            this.Attorney_State = new BankoString(2);
            this.Attorney_Zip = new BankoString(10);
            this.Attorney_Phone = new BankoString(10);
            this.Meeting341_DateTime = new BankoDateTime();
            this.Meeting341_Location = new BankoString(50);
            this.Trustee = new BankoString(40);
            this.Trustee_Address = new BankoString(32);
            this.Trustee_City = new BankoString(25);
            this.Trustee_State = new BankoString(2);
            this.Trustee_Zip = new BankoString(10);
            this.Trustee_Phone = new BankoString(10);
            this.JudgeInitials = new BankoString(16);
            this.FundsAvailable = new BankoBool(1, "Y", "N");
            this.DischargeComplaintBarDate = new BankoDate();
            this.MatchCode = new BankoMatches(10);
            this.ClientField = new BankoNumber(8);
            this.Court_District = new BankoString(30);
            this.Court_Address1 = new BankoString(35);
            this.Court_Address2 = new BankoString(35);
            this.Court_City_Mailing = new BankoString(20);
            this.Court_Zip = new BankoString(5);
            this.Court_Phone = new BankoString(10);
            this.Phone = new BankoString(10);
            this.DismissalVoluntary = new BankoEnum<Congeniality, CongenialityValues>(1);
            this.ProofOfClaimBarDate = new BankoDate();
            #endregion

            if (Record.Count(el => el == ',') >= 52) // CSV Inputted
            {

            }
            else // Fixed Width Inputted
            {
                this.CustomerAccount.DataString = Record.Substring(0);
                this.HitType.DataString = Record.Substring(25);
                this.Case.DataString = Record.Substring(26);
                this.Chapter.DataString = Record.Substring(38);
                this.Filed_Date.DataString = Record.Substring(40);
                this.StatusDate.DataString = Record.Substring(48);
                this.Disposition.DataString = Record.Substring(56);
                this.Suffix.DataString = Record.Substring(58);
                this.Address.DataString = Record.Substring(59);
                this.Zip.DataString = Record.Substring(91);
                this.SSN.DataString = Record.Substring(101);
                this.Filed_City.DataString = Record.Substring(110);
                this.Filed_State.DataString = Record.Substring(130);
                this.County.DataString = Record.Substring(132);
                this.FirstName.DataString = Record.Substring(157);
                this.MiddleName.DataString = Record.Substring(172);
                this.LastName.DataString = Record.Substring(182);
                this.Business.DataString = Record.Substring(207);
                this.Business1.DataString = Record.Substring(252);
                this.Business2.DataString = Record.Substring(297);
                this.Business3.DataString = Record.Substring(342);
                this.City.DataString = Record.Substring(387);
                this.State.DataString = Record.Substring(417);
                this.ECOA.DataString = Record.Substring(419);
                this.LawFirm.DataString = Record.Substring(420);
                this.Attorney.DataString = Record.Substring(470);
                this.Attorney_Address.DataString = Record.Substring(505);
                this.Attorney_City.DataString = Record.Substring(537);
                this.Attorney_State.DataString = Record.Substring(562);
                this.Attorney_Zip.DataString = Record.Substring(564);
                this.Attorney_Phone.DataString = Record.Substring(574);
                this.Meeting341_DateTime.DataString = Record.Substring(584);
                this.Meeting341_Location.DataString = Record.Substring(597);
                this.Trustee.DataString = Record.Substring(647);
                this.Trustee_Address.DataString = Record.Substring(687);
                this.Trustee_City.DataString = Record.Substring(719);
                this.Trustee_State.DataString = Record.Substring(744);
                this.Trustee_Zip.DataString = Record.Substring(746);
                this.Trustee_Phone.DataString = Record.Substring(756);
                this.JudgeInitials.DataString = Record.Substring(766);
                this.FundsAvailable.DataString = Record.Substring(782);
                this.DischargeComplaintBarDate.DataString = Record.Substring(783);
                this.MatchCode.DataString = Record.Substring(791);
                this.ClientField.DataString = Record.Substring(801);
                this.Court_District.DataString = Record.Substring(809);
                this.Court_Address1.DataString = Record.Substring(839);
                this.Court_Address2.DataString = Record.Substring(874);
                this.Court_City_Mailing.DataString = Record.Substring(909);
                this.Court_Zip.DataString = Record.Substring(929);
                this.Court_Phone.DataString = Record.Substring(934);
                this.Phone.DataString = Record.Substring(944);
                this.DismissalVoluntary.DataString = Record.Substring(954);
                this.ProofOfClaimBarDate.DataString = Record.Substring(955);
            }
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}{31}{32}{33}{34}{35}{36}{37}{38}{39}{40}{41}{42}{43}{44}{45}{46}{47}{48}{49}{50}{51}{52}",
                this.CustomerAccount,
                this.HitType,
                this.Case,
                this.Chapter,
                this.Filed_Date,
                this.StatusDate,
                this.Disposition,
                this.Suffix,
                this.Address,
                this.Zip,
                this.SSN,
                this.Filed_City,
                this.Filed_State,
                this.County,
                this.FirstName,
                this.MiddleName,
                this.LastName,
                this.Business,
                this.Business1,
                this.Business2,
                this.Business3,
                this.City,
                this.State,
                this.ECOA,
                this.LawFirm,
                this.Attorney,
                this.Attorney_Address,
                this.Attorney_City,
                this.Attorney_State,
                this.Attorney_Zip,
                this.Attorney_Phone,
                this.Meeting341_DateTime,
                this.Meeting341_Location,
                this.Trustee,
                this.Trustee_Address,
                this.Trustee_City,
                this.Trustee_State,
                this.Trustee_Zip,
                this.Trustee_Phone,
                this.JudgeInitials,
                this.FundsAvailable,
                this.DischargeComplaintBarDate,
                this.MatchCode,
                this.ClientField,
                this.Court_District,
                this.Court_Address1,
                this.Court_Address2,
                this.Court_City_Mailing,
                this.Court_Zip,
                this.Court_Phone,
                this.Phone,
                this.DismissalVoluntary,
                this.ProofOfClaimBarDate);
        }
    }
}
