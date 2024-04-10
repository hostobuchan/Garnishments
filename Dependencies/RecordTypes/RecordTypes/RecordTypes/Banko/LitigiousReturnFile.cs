using RecordTypes.Banko.Base.DataTypes;
using RecordTypes.Banko.Base.Enums;

namespace RecordTypes.Banko
{
    public class LitigiousReturnFile
    {
        #region Properties
        public BankoString CustomerAccount { get; private set; }
        public BankoEnum<LitigiousFlag> LitigiousFlag { get; private set; }
        public BankoBool FDCPA { get; private set; }
        public BankoBool FCRA { get; private set; }
        public BankoBool TCPA { get; private set; }
        public LitigiousCase CaseFound1 { get; private set; }
        public LitigiousCase CaseFound2 { get; private set; }
        public LitigiousCase CaseFound3 { get; private set; }
        public LitigiousCase CaseFound4 { get; private set; }
        public LitigiousCase CaseFound5 { get; private set; }
        public LitigiousCase CaseFound6 { get; private set; }
        public string FileNo { get { try { return this.CustomerAccount.Value.Split('-')[0]; } catch { return this.CustomerAccount.Value; } } }
        public int DebtorNumber { get { try { return int.Parse(this.CustomerAccount.Value.Split('-')[1]); } catch { return 1; } } }
        #endregion

        public LitigiousReturnFile(string Record)
        {
            this.CustomerAccount = new BankoString(25) { DataString = string.IsNullOrEmpty(Record) ? "" : Record };
            this.LitigiousFlag = new BankoEnum<LitigiousFlag>(1) { DataString = Record.Length > 25 ? Record.Substring(25) : "" };
            this.FDCPA = new BankoBool(1, "Y", "N") { DataString = Record.Length > 26 ? Record.Substring(26) : "" };
            this.FCRA = new BankoBool(1, "Y", "N") { DataString = Record.Length > 27 ? Record.Substring(27) : "" };
            this.TCPA = new BankoBool(1, "Y", "N") { DataString = Record.Length > 28 ? Record.Substring(28) : "" };
            this.CaseFound1 = new LitigiousCase(Record.Length > 29 ? Record.Substring(29) : "");
            this.CaseFound2 = new LitigiousCase(Record.Length > 2114 ? Record.Substring(2114) : "");
            this.CaseFound3 = new LitigiousCase(Record.Length > 4199 ? Record.Substring(4199) : "");
            this.CaseFound4 = new LitigiousCase(Record.Length > 6284 ? Record.Substring(6284) : "");
            this.CaseFound5 = new LitigiousCase(Record.Length > 8369 ? Record.Substring(8369) : "");
            this.CaseFound6 = new LitigiousCase(Record.Length > 10454 ? Record.Substring(10454) : "");
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}",
                this.CustomerAccount,
                this.LitigiousFlag,
                this.FDCPA,
                this.FCRA,
                this.TCPA,
                this.CaseFound1,
                this.CaseFound2,
                this.CaseFound3,
                this.CaseFound4,
                this.CaseFound5,
                this.CaseFound6
                );
        }
    }
}
