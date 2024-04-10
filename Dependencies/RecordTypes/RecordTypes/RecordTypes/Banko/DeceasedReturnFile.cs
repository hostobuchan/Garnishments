using RecordTypes.Banko.Base.DataTypes;

namespace RecordTypes.Banko
{
    public class DeceasedReturnFile
    {
        #region Properties
        public BankoString CustomerAccount { get; private set; }
        public BankoString FirstName { get; private set; }
        public BankoString LastName { get; private set; }
        public BankoString SSN { get; private set; }
        public BankoString DeceasedFirstName { get; private set; }
        public BankoString DeceasedLastName { get; private set; }
        public BankoString State { get; private set; }
        public BankoString Zip { get; private set; }
        public BankoDate DateOfBirth { get; private set; }
        public BankoDate DateOfDeath { get; private set; }
        public BankoMatches MatchCode { get; private set; }
        public string FileNo { get { try { return this.CustomerAccount.Value.Split('-')[0]; } catch { return this.CustomerAccount.Value; } } }
        public int DebtorNumber { get { try { return int.Parse(this.CustomerAccount.Value.Split('-')[1]); } catch { return 1; } } }
        #endregion

        public DeceasedReturnFile(string Record)
        {
            try
            {
                this.CustomerAccount = new BankoString(25) { DataString = Record };
                this.FirstName = new BankoString(15) { DataString = Record.Substring(25) };
                this.LastName = new BankoString(25) { DataString = Record.Substring(40) };
                this.SSN = new BankoString(9) { DataString = Record.Substring(65) };
                this.DeceasedFirstName = new BankoString(15) { DataString = Record.Substring(74) };
                this.DeceasedLastName = new BankoString(25) { DataString = Record.Substring(89) };
                this.State = new BankoString(2) { DataString = Record.Substring(114) };
                this.Zip = new BankoString(10) { DataString = Record.Substring(116) };
                this.DateOfBirth = new BankoDate("yyyyMMdd") { DataString = Record.Substring(126) };
                this.DateOfDeath = new BankoDate("yyyyMMdd") { DataString = Record.Substring(134) };
                this.MatchCode = new BankoMatches(5) { DataString = Record.Substring(142) };
            }
            catch
            {
                if (this.CustomerAccount == null) this.CustomerAccount = new BankoString(25);
                if (this.FirstName == null) this.FirstName = new BankoString(15);
                if (this.LastName == null) this.LastName = new BankoString(25);
                if (this.SSN == null) this.SSN = new BankoString(9);
                if (this.DeceasedFirstName == null) this.DeceasedFirstName = new BankoString(15);
                if (this.DeceasedLastName == null) this.DeceasedLastName = new BankoString(25);
                if (this.State == null) this.State = new BankoString(2);
                if (this.Zip == null) this.Zip = new BankoString(10);
                if (this.DateOfBirth == null) this.DateOfBirth = new BankoDate();
                if (this.DateOfDeath == null) this.DateOfDeath = new BankoDate();
                if (this.MatchCode == null) this.MatchCode = new BankoMatches(5);
            }
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}",
                this.CustomerAccount,
                this.FirstName,
                this.LastName,
                this.SSN,
                this.DeceasedFirstName,
                this.DeceasedLastName,
                this.State,
                this.Zip,
                this.DateOfBirth,
                this.DateOfDeath,
                this.MatchCode);
        }
    }
}
