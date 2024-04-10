using System.Linq;

namespace EvaluationCriteria
{
    public static partial class Utilities
    {
        public static int SifRateFinder(string SifString, Evaluatees.Evaluatee evaluatee)
        {
            if (SifString == "CONTACT PORTFOLIO ANALYST")
            {
                return 100;
            }
            else if (SifString.Contains('+'))
            {
                return int.Parse(Criteria.NumberCleanup(SifString, typeof(int)));
            }
            else if (SifString.Contains(';'))
            {
                string[] Rates = SifString.Split(';');
                if (evaluatee.JudgmentDate != null)
                    return int.Parse(Criteria.NumberCleanup(Rates[2], typeof(int)));
                else if (evaluatee.SuitDate != null || evaluatee.ServiceDate != null)
                    return int.Parse(Criteria.NumberCleanup(Rates[1], typeof(int)));
                else
                    return int.Parse(Criteria.NumberCleanup(Rates[0], typeof(int)));
            }
            else
                return 100;
        }
        public static int SifRateFinder(string SifString, Evaluatees2.Evaluatee2 evaluatee)
        {
            if (SifString == "CONTACT PORTFOLIO ANALYST")
            {
                return 100;
            }
            else if (SifString.Contains('+'))
            {
                return int.Parse(Criteria.NumberCleanup(SifString, typeof(int)));
            }
            else if (SifString.Contains(';'))
            {
                string[] Rates = SifString.Split(';');
                if (evaluatee.JudgmentDate != null)
                    return int.Parse(Criteria.NumberCleanup(Rates[2], typeof(int)));
                else if (evaluatee.SuitDate != null || evaluatee.ServiceDate != null)
                    return int.Parse(Criteria.NumberCleanup(Rates[1], typeof(int)));
                else
                    return int.Parse(Criteria.NumberCleanup(Rates[0], typeof(int)));
            }
            else
                return 100;
        }
        public static int SifRateFinder(string SifString, Enums.Disposition disposition)
        {
            if (SifString == "CONTACT PORTFOLIO ANALYST")
            {
                return 100;
            }
            else if (SifString.Contains('+'))
            {
                return int.Parse(Criteria.NumberCleanup(SifString, typeof(int)));
            }
            else if (SifString.Contains(';'))
            {
                string[] Rates = SifString.Split(';');
                if (disposition == Enums.Disposition.Judgment)
                    return int.Parse(Criteria.NumberCleanup(Rates[2], typeof(int)));
                else if (disposition == Enums.Disposition.PostSuit)
                    return int.Parse(Criteria.NumberCleanup(Rates[1], typeof(int)));
                else
                    return int.Parse(Criteria.NumberCleanup(Rates[0], typeof(int)));
            }
            else
                return 100;
        }
    }
}
