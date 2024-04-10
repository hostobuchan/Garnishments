using RecordTypes.RMS.DataTypes;

namespace RecordTypes.RMS.Base
{
    public abstract class AccountInfo : Record
    {
        #region Public Properties
        public RMSString AccountNumber { get; private set; }
        public RMSEnum<Enums.RecordTypes, Enums.RecordTypeValues> RecordType { get; private set; }
        #endregion

        public AccountInfo(string Record)
        {
            this.AccountNumber = new RMSString(20) { DataString = Record.Substring(0, 20) };
            this.RecordType = new RMSEnum<Enums.RecordTypes, Enums.RecordTypeValues>(1) { DataString = Record.Substring(20, 1) };
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", this.AccountNumber, this.RecordType);
        }
    }
}
