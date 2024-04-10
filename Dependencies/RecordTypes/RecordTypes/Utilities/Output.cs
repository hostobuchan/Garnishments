using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecordTypes
{
    public static class Output
    {
        private static string _YGCLocation;
        private static string _MergeLocation;
        public static string YGCLocation
        {
            get
            {
                if (_YGCLocation == null)
                {
                    try
                    {
                        _YGCLocation = System.Configuration.ConfigurationManager.AppSettings["YGC-IMP"];
                    }
                    catch { throw new NullReferenceException("App Settings Missing \"YGC-IMP\" Entry"); }
                }
                return _YGCLocation;
            }
            set
            {
                _YGCLocation = value;
            }
        }
        public static string MergeLocation
        {
            get
            {
                if (_MergeLocation == null)
                {
                    try
                    {
                        _MergeLocation = System.Configuration.ConfigurationManager.AppSettings["WP-IMP"];
                    }
                    catch { throw new NullReferenceException("App Settings Missing \"WP-IMP\" Entry"); }
                }
                return _MergeLocation;
            }
            set
            {
                _MergeLocation = value;
            }
        }
        /// <summary>
        /// Writes New File in YGC Import Folder
        /// <para>This method only supports YGC Record Types 09 &amp; 95</para>
        /// </summary>
        /// <param name="Records">List of YGC Record Type 09 &amp; 95 or Both</param>
        public static void Send_YGC_Imp(IEnumerable<RecordTypes.YGC.Base.YGCBase> Records)
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(System.IO.Path.Combine(YGCLocation, "Updates_" + Environment.UserName + "_" + DateTime.Now.ToString("yyyyMMdd-HHmmss")), true))
            {
                foreach (RecordTypes.YGC.Base.YGCBase R in Records)
                {
                    sw.WriteLine(R);
                }
                sw.Flush();
                sw.Close();
            }
        }
        /// <summary>
        /// Writes New File in YGC Import Folder
        /// <para>This method only supports YGC Record Types 09 &amp; 95</para>
        /// </summary>
        /// <param name="Records">List of YGC Record Type 09 &amp; 95 or Both</param>
        public static async Task Send_YGC_Imp_Async(IEnumerable<RecordTypes.YGC.Base.YGCBase> Records)
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(System.IO.Path.Combine(YGCLocation, "Updates_" + Environment.UserName + "_" + DateTime.Now.ToString("yyyyMMdd-HHmmss")), true))
            {
                foreach (RecordTypes.YGC.Base.YGCBase R in Records)
                {
                    await sw.WriteLineAsync(R.ToString());
                }
                await sw.FlushAsync();
                sw.Close();
            }
        }
        /// <summary>
        /// Writes New File in WP Import Folder
        /// </summary>
        /// <param name="Merges">List of Merge Records</param>
        /// <param name="Progress">*Optional Progress Callback</param>
        public static void Send_WP_Imp(IEnumerable<RecordTypes.MergePops.MergePop> Merges, Action<int> Progress = null)
        {
            using (MergePops.FileWriter fileWriter = new MergePops.FileWriter(System.IO.Path.Combine(MergeLocation, "Updates_" + Environment.UserName + "_" + DateTime.Now.ToString("yyyyMMdd-HHmmss"))))
            {
                fileWriter.WriteFile(Merges, Progress);
            }
        }
        /// <summary>
        /// Writes New File in WP Import Folder
        /// </summary>
        /// <param name="Merges">List of Merge Records</param>
        /// <param name="Progress">*Optional Progress Callback</param>
        public static async Task Send_WP_Imp_Async(IEnumerable<RecordTypes.MergePops.MergePop> Merges, Action<int> Progress = null)
        {
            using (MergePops.FileWriter fileWriter = new MergePops.FileWriter(System.IO.Path.Combine(MergeLocation, "Updates_" + Environment.UserName + "_" + DateTime.Now.ToString("yyyyMMdd-HHmmss"))))
            {
                await fileWriter.WriteFileAsync(Merges, Progress);
            }
        }
    }
}
