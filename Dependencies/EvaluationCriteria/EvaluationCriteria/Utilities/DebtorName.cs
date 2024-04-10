using System.Linq;

namespace EvaluationCriteria
{
    public static partial class Utilities
    {
        /// <summary>
        /// Get Name Sections from Format "LAST/FIRST MIDDLE" or "LAST SUFFIX/FIRST MIDDLE"
        /// <para>Indexes</para>
        /// <para>0 - First Name</para>
        /// <para>1 - Middle Name</para>
        /// <para>2 - Last Name</para>
        /// <para>3 - Suffix</para>
        /// </summary>
        /// <param name="Name">Name</param>
        /// <returns></returns>
        public static string[] DebtorName(string Name)
        {
            string[] DName = new string[4] { "", "", "", "" };
            if (!string.IsNullOrEmpty(Name))
            {
                string[] theSplit;
                if ((theSplit = Name.Split('/')).Length > 1)
                {
                    if (theSplit[1].Contains(' '))
                    {
                        string[] Split2 = theSplit[1].Split(' ');
                        DName[0] = Split2[0];
                        DName[2] = theSplit[0];
                        if (Suffixes.Contains(Split2[1], new CaseInsensitiveStringComparer()))
                        {
                            DName[3] = Split2[1];
                        }
                        else
                        {
                            DName[1] = Split2[1];
                        }
                    }
                    else
                    {
                        DName[0] = theSplit[1];
                        DName[1] = "";
                        DName[2] = theSplit[0];
                    }
                    if (theSplit[0].Contains(' '))
                    {
                        DName[2] = theSplit[0].Split(' ')[0];
                        DName[3] = theSplit[0].Split(' ')[theSplit[0].Split(' ').GetUpperBound(0)];
                    }
                }
                else
                {
                    DName[0] = Name;
                }
            }
            return DName;
        }
    }
}
