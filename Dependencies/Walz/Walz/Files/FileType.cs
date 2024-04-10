using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Walz.Data.Files
{
    public class FileType
    {
        public string FileName { get; private set; }
        public Enums.FileType Type { get; private set; }

        internal protected FileType(string FileName) : this(FileName, DetermineFileType(FileName)) { }
        internal protected FileType(string FileName, Enums.FileType FileType)
        {
            this.FileName = FileName;
            this.Type = FileType;
        }
        protected FileType(FileType FileType)
        {
            this.FileName = FileType.FileName;
            this.Type = FileType.Type;
        }

        internal static Enums.FileType DetermineFileType(string FileName)
        {
            foreach (Enums.FileType type in Enum.GetValues(typeof(Enums.FileType)))
            {
                if (type == Enums.FileType.Unknown) continue;
                if (System.Text.RegularExpressions.Regex.IsMatch(FileName, Dictionaries.FileNamingConventions[type], System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                    return type;
            }
            return Enums.FileType.Unknown;
        }

        public override string ToString()
        {
            return string.Format("[{0}] {1}",
                this.Type,
                this.FileName);
        }
    }
}
