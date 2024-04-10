using RecordTypes.Banko.Base.DataTypes;
using RecordTypes.Banko.Base.Enums;
using System;

namespace RecordTypes.Banko
{
    public class BankruptcyUploadFile
    {
        #region Properties
        public BankoString CustomerAccount { get; private set; }
        public BankoString FirstName { get; private set; }
        public BankoString MiddleName { get; private set; }
        public BankoString LastName { get; private set; }
        public BankoString Suffix { get; private set; }
        public BankoString SSN { get; private set; }
        public BankoString Address { get; private set; }
        public BankoString City { get; private set; }
        public BankoString State { get; private set; }
        public BankoString Zip { get; private set; }
        public BankoNumber ClientField { get; private set; }
        public BankoDate AgreementDate { get; private set; }
        public BankoEnum<ProductCode> ProductCode { get; private set; }
        public string FileNo { get { try { return this.CustomerAccount.Value.Split('-')[0]; } catch { return this.CustomerAccount.Value; } } }
        public int DebtorNumber { get { try { return int.Parse(this.CustomerAccount.Value.Split('-')[1]); } catch { return 1; } } }
        #endregion

        public BankruptcyUploadFile(string FileNo, string FirstName, string MiddleName, string LastName, string SSN, string Address, string City, string State, string Zip, int DebtorNumber, DateTime? OriginationDate, ProductCode Product = Base.Enums.ProductCode.BankruptcyDeceased)
        {
            this.CustomerAccount = new BankoString(25) { DataString = FileNo + "-" + DebtorNumber.ToString() };
            this.FirstName = new BankoString(15) { DataString = FirstName ?? "" };
            this.MiddleName = new BankoString(10) { DataString = MiddleName ?? "" };
            this.LastName = new BankoString(25) { DataString = LastName ?? "" };
            this.Suffix = new BankoString(1);
            this.SSN = new BankoString(9) { DataString = SSN.NumbersOnly() ?? "" };
            this.Address = new BankoString(32) { DataString = Address ?? "" };
            this.City = new BankoString(30) { DataString = City ?? "" };
            this.State = new BankoString(2) { DataString = State ?? "" };
            this.Zip = new BankoString(10) { DataString = Zip.NumbersOnly() ?? "" };
            this.ClientField = new BankoNumber(8) { Value = DebtorNumber };
            this.AgreementDate = new BankoDate() { Value = OriginationDate };
            this.ProductCode = new BankoEnum<ProductCode>(2) { Value = Product };
        }
        public BankruptcyUploadFile(string record)
        {
            this.CustomerAccount = new BankoString(25) { DataString = record };
            this.FirstName = new BankoString(15) { DataString = record.Substring(25) };
            this.MiddleName = new BankoString(10) { DataString = record.Substring(40) };
            this.LastName = new BankoString(25) { DataString = record.Substring(50) };
            this.Suffix = new BankoString(1);
            this.SSN = new BankoString(9) { DataString = record.Substring(76) };
            this.Address = new BankoString(32) { DataString = record.Substring(85) };
            this.City = new BankoString(30) { DataString = record.Substring(117) };
            this.State = new BankoString(2) { DataString = record.Substring(147) };
            this.Zip = new BankoString(10) { DataString = record.Substring(149) };
            this.ClientField = new BankoNumber(8) { DataString = record.Substring(159) };
            this.AgreementDate = new BankoDate() { DataString = record.Substring(167) };
            this.ProductCode = new BankoEnum<ProductCode>(2) { DataString = record.Substring(175) };
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}",
                this.CustomerAccount,
                this.FirstName,
                this.MiddleName,
                this.LastName,
                this.Suffix,
                this.SSN,
                this.Address,
                this.City,
                this.State,
                this.Zip,
                this.ClientField,
                this.AgreementDate,
                this.ProductCode);
        }

        public string ToStringCSV()
        {
            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12}",
                this.CustomerAccount.Value,
                this.FirstName.Value,
                this.MiddleName.Value,
                this.LastName.Value,
                this.Suffix.Value,
                this.SSN.Value,
                this.Address.Value,
                this.City.Value,
                this.State.Value,
                this.Zip.Value,
                this.ClientField.DataString.Trim(),
                this.AgreementDate.DataString.Trim(),
                this.ProductCode);
        }
    }
}
