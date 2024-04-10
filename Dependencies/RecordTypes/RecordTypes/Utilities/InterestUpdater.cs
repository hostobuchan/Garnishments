using RecordTypes.Functions.InterestUpdaters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RecordTypes.Functions
{
    namespace Interfaces
    {
        public interface IInterestUpdater<T>
        {
            void UpdateInterest(decimal Rate, List<T> OriginalAccounts);
        }
    }

    public static class InterestUpdater
    {
        public static void UpdateInterest(RecordTypes.SupportedEDITypes EDIType, decimal Rate, object OriginalAccounts)
        {
            MethodInfo MI = typeof(InterestUpdater).GetMethod("UpdateInterest", BindingFlags.Static | BindingFlags.NonPublic).MakeGenericMethod(new Type[] { RecordTypes.EDITypeFinder.GetBaseRecord(EDIType) });
            MethodInfo MI2 = typeof(InterestUpdater).GetMethod("OfType", BindingFlags.Static | BindingFlags.NonPublic).MakeGenericMethod(new Type[] { RecordTypes.EDITypeFinder.GetBaseRecord(EDIType) });
            MI.Invoke(null, new object[] { EDIType, Rate, MI2.Invoke(null, new object[] { OriginalAccounts }) });
        }
        private static void UpdateInterest<T>(RecordTypes.SupportedEDITypes EDIType, decimal Rate, List<T> OriginalAccounts)
        {
            Interfaces.IInterestUpdater<T> Updater;
            switch (EDIType)
            {
                case SupportedEDITypes.Citi:
                    Updater = (Interfaces.IInterestUpdater<T>)new CitiInterestUpdater();
                    break;
                case SupportedEDITypes.Ford:
                    Updater = (Interfaces.IInterestUpdater<T>)new FordInterestUpdater();
                    break;
                case SupportedEDITypes.NAN:
                    Updater = (Interfaces.IInterestUpdater<T>)new NANInterestUpdater();
                    break;
                case SupportedEDITypes.NCO:
                    Updater = (Interfaces.IInterestUpdater<T>)new NCOInterestUpdater();
                    break;
                case SupportedEDITypes.PLX:
                    Updater = (Interfaces.IInterestUpdater<T>)new PLXInterestUpdater();
                    break;
                case SupportedEDITypes.RMS:
                    Updater = (Interfaces.IInterestUpdater<T>)new RMSInterestUpdater();
                    break;
                case SupportedEDITypes.YGC:
                    Updater = (Interfaces.IInterestUpdater<T>)new YGCInterestUpdater();
                    break;
                default:
                    return;
            }
            Updater.UpdateInterest(Rate, OriginalAccounts);
        }
        private static List<T> OfType<T>(List<object> List)
        {
            return List.OfType<T>().ToList();
        }
    }

    namespace InterestUpdaters
    {
        class YGCInterestUpdater : Interfaces.IInterestUpdater<RecordTypes.YGC.Base.YGCBase>
        {
            public void UpdateInterest(decimal Rate, List<RecordTypes.YGC.Base.YGCBase> OriginalAccounts)
            {
                foreach (RecordTypes.YGC.RecordType01 Record in OriginalAccounts.OfType<RecordTypes.YGC.RecordType01>())
                {
                    RecordTypes.YGC.RecordType07 Jmt = OriginalAccounts.OfType<RecordTypes.YGC.RecordType07>().Where(el => el.FORW_FILE.Value == Record.FORW_FILE.Value).FirstOrDefault();
                    if (Jmt != null && Jmt.JUDGMENT_DATE.Value.HasValue)
                        break;
                    Record.RATES_PRE.Value = Rate / (decimal)100;
                    //Record.ORIG_INT_D.Value = LastAccrualDate;
                }
            }
        }

        class RMSInterestUpdater : Interfaces.IInterestUpdater<RecordTypes.RMS.Base.Record>
        {
            public void UpdateInterest(decimal Rate, List<RecordTypes.RMS.Base.Record> OriginalAccounts)
            {
                foreach (RecordTypes.RMS.DebtorRecord Record in OriginalAccounts.OfType<RecordTypes.RMS.DebtorRecord>())
                {
                    Record.InterestRate.Value = Rate;
                    //Record.LastInterestDate.Value = LastAccrualDate;
                }
            }
        }

        class FordInterestUpdater : Interfaces.IInterestUpdater<RecordTypes.RMS.Base.Record>
        {
            public void UpdateInterest(decimal Rate, List<RecordTypes.RMS.Base.Record> OriginalAccounts)
            {
                foreach (RecordTypes.Ford.DebtorRecord Record in OriginalAccounts.OfType<RecordTypes.Ford.DebtorRecord>())
                {
                    Record.InterestRate.Value = Rate;
                    if (!Record.LastInterestDate.Value.HasValue)
                        Record.LastInterestDate.Value = Record.ChargeoffDate.Value;
                    //Record.LastInterestDate.Value = LastAccrualDate;
                }
            }
        }

        class CitiInterestUpdater : Interfaces.IInterestUpdater<RecordTypes.RMS.Base.Record>
        {
            public void UpdateInterest(decimal Rate, List<RecordTypes.RMS.Base.Record> OriginalAccounts)
            {

            }
        }

        class NANInterestUpdater : Interfaces.IInterestUpdater<RecordTypes.NAN.Base.Record>
        {
            public void UpdateInterest(decimal Rate, List<RecordTypes.NAN.Base.Record> OriginalAccounts)
            {

            }
        }

        class NCOInterestUpdater : Interfaces.IInterestUpdater<RecordTypes.NCO.Base.Record>
        {
            public void UpdateInterest(decimal Rate, List<RecordTypes.NCO.Base.Record> OriginalAccounts)
            {

            }
        }

        class PLXInterestUpdater : Interfaces.IInterestUpdater<RecordTypes.PLX.Base.RecordTypeBase>
        {
            public void UpdateInterest(decimal Rate, List<RecordTypes.PLX.Base.RecordTypeBase> OriginalAccounts)
            {

            }
        }
    }
}
