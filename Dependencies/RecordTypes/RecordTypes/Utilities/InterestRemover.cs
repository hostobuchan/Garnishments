using RecordTypes.Functions.InterestRemovers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RecordTypes.Functions
{
    namespace Interfaces
    {
        public interface IInterestRemover<T>
        {
            void RemoveInterest(List<T> OriginalAccounts);
        }
    }

    public static class InterestRemover
    {
        public static void RemoveInterest(RecordTypes.SupportedEDITypes EDIType, object OriginalAccounts)
        {
            MethodInfo MI = typeof(InterestRemover).GetMethod("RemoveInterest", BindingFlags.Static | BindingFlags.NonPublic).MakeGenericMethod(new Type[] { RecordTypes.EDITypeFinder.GetBaseRecord(EDIType) });
            MethodInfo MI2 = typeof(InterestRemover).GetMethod("OfType", BindingFlags.Static | BindingFlags.NonPublic).MakeGenericMethod(new Type[] { RecordTypes.EDITypeFinder.GetBaseRecord(EDIType) });
            MI.Invoke(null, new object[] { EDIType, MI2.Invoke(null, new object[] { OriginalAccounts }) });
        }
        private static void RemoveInterest<T>(RecordTypes.SupportedEDITypes EDIType, List<T> OriginalAccounts)
        {
            Interfaces.IInterestRemover<T> Remover;
            switch (EDIType)
            {
                case SupportedEDITypes.Citi:
                    Remover = (Interfaces.IInterestRemover<T>)new CitiInterestRemover();
                    break;
                case SupportedEDITypes.Ford:
                    Remover = (Interfaces.IInterestRemover<T>)new FordInterestRemover();
                    break;
                case SupportedEDITypes.NAN:
                    Remover = (Interfaces.IInterestRemover<T>)new NANInterestRemover();
                    break;
                case SupportedEDITypes.NCO:
                    Remover = (Interfaces.IInterestRemover<T>)new NCOInterestRemover();
                    break;
                case SupportedEDITypes.PLX:
                    Remover = (Interfaces.IInterestRemover<T>)new PLXInterestRemover();
                    break;
                case SupportedEDITypes.RMS:
                    Remover = (Interfaces.IInterestRemover<T>)new RMSInterestRemover();
                    break;
                case SupportedEDITypes.YGC:
                    Remover = (Interfaces.IInterestRemover<T>)new YGCInterestRemover();
                    break;
                default:
                    return;
            }
            Remover.RemoveInterest(OriginalAccounts);
        }
        private static List<T> OfType<T>(List<object> List)
        {
            return List.OfType<T>().ToList();
        }
    }

    namespace InterestRemovers
    {
        class YGCInterestRemover : Interfaces.IInterestRemover<RecordTypes.YGC.Base.YGCBase>
        {
            public void RemoveInterest(List<RecordTypes.YGC.Base.YGCBase> OriginalAccounts)
            {
                foreach (RecordTypes.YGC.RecordType01 Record in OriginalAccounts.OfType<RecordTypes.YGC.RecordType01>())
                {
                    Record.ORIG_INT.Value = 0;
                    Record.ORIG_INT_D.Value = null;
                    Record.RATES_PRE.Value = 0;
                    Record.RATES_POST.Value = 0;
                }
            }
        }

        class RMSInterestRemover : Interfaces.IInterestRemover<RecordTypes.RMS.Base.Record>
        {
            public void RemoveInterest(List<RMS.Base.Record> OriginalAccounts)
            {
                foreach (RecordTypes.RMS.DebtorRecord Record in OriginalAccounts.OfType<RecordTypes.RMS.DebtorRecord>())
                {
                    Record.InterestRate.Value = 0;
                    Record.AccruedInterest.Value = null;
                    Record.LastInterestDate.Value = null;
                }
            }
        }

        class FordInterestRemover : Interfaces.IInterestRemover<RecordTypes.RMS.Base.Record>
        {
            public void RemoveInterest(List<RMS.Base.Record> OriginalAccounts)
            {
                foreach (RecordTypes.Ford.DebtorRecord Record in OriginalAccounts.OfType<RecordTypes.Ford.DebtorRecord>())
                {
                    Record.InterestRate.Value = 0;
                    Record.AccruedInterest.Value = 0;
                }
            }
        }

        class CitiInterestRemover : Interfaces.IInterestRemover<RecordTypes.RMS.Base.Record>
        {
            public void RemoveInterest(List<RMS.Base.Record> OriginalAccounts)
            {

            }
        }

        class NANInterestRemover : Interfaces.IInterestRemover<RecordTypes.NAN.Base.Record>
        {
            public void RemoveInterest(List<NAN.Base.Record> OriginalAccounts)
            {

            }
        }

        class NCOInterestRemover : Interfaces.IInterestRemover<RecordTypes.NCO.Base.Record>
        {
            public void RemoveInterest(List<NCO.Base.Record> OriginalAccounts)
            {

            }
        }

        public class PLXInterestRemover : Interfaces.IInterestRemover<RecordTypes.PLX.Base.RecordTypeBase>
        {
            public void RemoveInterest(List<PLX.Base.RecordTypeBase> OriginalAccounts)
            {
                foreach (PLX.LegalInfo Legal in OriginalAccounts.OfType<PLX.LegalInfo>())
                {
                    if (Legal != null && !Legal.JudgmentDate.HasValue)
                    {
                        foreach (PLX.AccountBalanceRecord Bal in OriginalAccounts.OfType<PLX.AccountBalanceRecord>().Where(el => el.AccountNumber == Legal.AccountNumber))
                        {
                            Bal.CurrentInterestRate = 0;
                            Bal.DailyInterestAccrual = 0;
                        }
                    }
                }
            }
        }
    }
}
