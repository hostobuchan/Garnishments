using RecordTypes.Banko.Base.DataTypes;

namespace RecordTypes.Banko
{
    public class LitigiousCase
    {
        #region Properties
        public BankoString PartyAttorneys { get; private set; }
        public BankoString Cause { get; private set; }
        public BankoString DefendantsAttorney { get; private set; }
        public BankoString Defendants { get; private set; }
        public BankoDecimal Demand { get; private set; }
        public BankoString DocketNumber { get; private set; }
        public BankoDate FilingDate { get; private set; }
        public BankoString FilingType { get; private set; }
        public BankoString Judge { get; private set; }
        public BankoString Jurisdiction { get; private set; }
        public BankoString JuryDemanded { get; private set; }
        public BankoString DocketNumber_Lead { get; private set; }
        public BankoString NatureOfSuit { get; private set; }
        public BankoString DocketNumber_Other { get; private set; }
        public BankoString PlaintiffsAttorney { get; private set; }
        public BankoString Plaintiffs { get; private set; }
        public BankoString Court { get; private set; }
        public BankoString Status { get; private set; }
        public LitDateTime DateCourtUpdateRecord { get; private set; }
        public BankoString CourtOfFiling { get; private set; }
        public BankoString CaseTitle { get; private set; }
        #endregion

        public LitigiousCase(string Section)
        {
            this.PartyAttorneys = new BankoString(250) { DataString = string.IsNullOrEmpty(Section) ? "" : Section };
            this.Cause = new BankoString(100) { DataString = Section.Length > 250 ? Section.Substring(250) : "" };
            this.DefendantsAttorney = new BankoString(250) { DataString = Section.Length > 350 ? Section.Substring(350) : "" };
            this.Defendants = new BankoString(120) { DataString = Section.Length > 600 ? Section.Substring(600) : "" };
            this.Demand = new BankoDecimal(30, 2) { DataString = Section.Length > 720 ? Section.Substring(720) : "" };
            this.DocketNumber = new BankoString(30) { DataString = Section.Length > 750 ? Section.Substring(750) : "" };
            this.FilingDate = new BankoDate() { DataString = Section.Length > 780 ? Section.Substring(780) : "" };
            this.FilingType = new BankoString(50) { DataString = Section.Length > 788 ? Section.Substring(788) : "" };
            this.Judge = new BankoString(100) { DataString = Section.Length > 838 ? Section.Substring(838) : "" };
            this.Jurisdiction = new BankoString(30) { DataString = Section.Length > 938 ? Section.Substring(938) : "" };
            this.JuryDemanded = new BankoString(25) { DataString = Section.Length > 968 ? Section.Substring(968) : "" };
            this.DocketNumber_Lead = new BankoString(30) { DataString = Section.Length > 993 ? Section.Substring(993) : "" };
            this.NatureOfSuit = new BankoString(150) { DataString = Section.Length > 1023 ? Section.Substring(1023) : "" };
            this.DocketNumber_Other = new BankoString(50) { DataString = Section.Length > 1173 ? Section.Substring(1173) : "" };
            this.PlaintiffsAttorney = new BankoString(250) { DataString = Section.Length > 1223 ? Section.Substring(1223) : "" };
            this.Plaintiffs = new BankoString(120) { DataString = Section.Length > 1473 ? Section.Substring(1473) : "" };
            this.Court = new BankoString(150) { DataString = Section.Length > 1593 ? Section.Substring(1593) : "" };
            this.Status = new BankoString(30) { DataString = Section.Length > 1743 ? Section.Substring(1743) : "" };
            this.DateCourtUpdateRecord = new LitDateTime() { DataString = Section.Length > 1773 ? Section.Substring(1773) : "" };
            this.CourtOfFiling = new BankoString(150) { DataString = Section.Length > 1785 ? Section.Substring(1785) : "" };
            this.CaseTitle = new BankoString(150) { DataString = Section.Length > 1935 ? Section.Substring(1935) : "" };
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}",
                this.PartyAttorneys,
                this.Cause,
                this.DefendantsAttorney,
                this.Defendants,
                this.Demand,
                this.DocketNumber,
                this.FilingDate,
                this.FilingType,
                this.Judge,
                this.Jurisdiction,
                this.JuryDemanded,
                this.DocketNumber_Lead,
                this.NatureOfSuit,
                this.DocketNumber_Other,
                this.PlaintiffsAttorney,
                this.Plaintiffs,
                this.Court,
                this.Status,
                this.DateCourtUpdateRecord,
                this.CourtOfFiling,
                this.CaseTitle
                );
        }
    }
}
