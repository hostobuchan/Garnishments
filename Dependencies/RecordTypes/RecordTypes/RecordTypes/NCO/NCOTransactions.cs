using RecordTypes.NCO.Base;
using RecordTypes.NCO.DataTypes;
using System;

namespace RecordTypes
{
    namespace NCO
    {
        namespace Transactions
        {

            public class B341A : TransactionCode
            {
                public NCODateTime MeetingDate { get; private set; }
                public NCOString Comments { get; private set; }
                public NCOString Filler { get; private set; }

                public B341A(DateTime MeetingDate, string Comments)
                {
                    this.MeetingDate = new NCODateTime() { Value = MeetingDate };
                    this.Comments = new NCOString(40) { Value = Comments };
                    this.Filler = new NCOString(444);
                }
                public B341A(string Transaction)
                {
                    try
                    {
                        this.MeetingDate = new NCODateTime() { DataString = Transaction.Substring(0) };
                        this.Comments = new NCOString(40) { DataString = Transaction.Substring(16) };
                        this.Filler = new NCOString(444) { DataString = Transaction.Substring(56) };
                    }
                    catch
                    {
                        if (this.MeetingDate == null) this.MeetingDate = new NCODateTime();
                        if (this.Comments == null) this.Comments = new NCOString(40);
                        if (this.Filler == null) this.Filler = new NCOString(444);
                    }
                }

                public override string ToString()
                {
                    return string.Format("{0}{1}{2}",
                        this.MeetingDate,
                        this.Comments,
                        this.Filler);
                }
            }

            public class SCOAA : TransactionCode
            {
                public NCODate ChargeOffDate { get; private set; }
                public NCOString ChargeOffFlag { get; private set; }
                public NCODecimal ChargeOffAmount { get; private set; }
                public NCODecimal ChargeOffPrincipal { get; private set; }
                public NCODecimal ChargeOffInterest { get; private set; }
                public NCODecimal ChargeOffOther { get; private set; }
                public NCOString Filler { get; private set; }

                public SCOAA(string Transaction)
                {
                    this.ChargeOffDate = new NCODate() { DataString = Transaction };
                    this.ChargeOffFlag = new NCOString(5) { DataString = Transaction.Substring(8) };
                    this.ChargeOffAmount = new NCODecimal(12, 2) { DataString = Transaction.Substring(13) };
                    this.ChargeOffPrincipal = new NCODecimal(12, 2) { DataString = Transaction.Substring(25) };
                    this.ChargeOffInterest = new NCODecimal(12, 2) { DataString = Transaction.Substring(37) };
                    this.ChargeOffOther = new NCODecimal(12, 2) { DataString = Transaction.Substring(49) };
                    this.Filler = new NCOString(439) { DataString = Transaction.Substring(61) };
                }

                public override string ToString()
                {
                    return string.Format("{0}{1}{2}{3}{4}{5}{6}",
                        this.ChargeOffDate,
                        this.ChargeOffFlag,
                        this.ChargeOffAmount,
                        this.ChargeOffPrincipal,
                        this.ChargeOffInterest,
                        this.ChargeOffOther,
                        this.Filler);
                }
            }
        }
    }
}
