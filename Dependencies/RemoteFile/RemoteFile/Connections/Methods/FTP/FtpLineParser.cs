using System;
using System.Text.RegularExpressions;

namespace RemoteFile.Connections.Methods.FTP
{
    static class FtpLineParser
    {
        private static Regex unixStyle = new Regex(@"^(?<dir>[-dl])(?<ownerSec>[-r][-w][-x])(?<groupSec>[-r][-w][-x])(?<everyoneSec>[-r][-w][-x])s+(?:d)s+(?<owner>w+)s+(?<group>w+)s+(?<size>d+)s+(?<month>w+)s+(?<day>d{1,2})s+(?<hour>d{1,2}):(?<minutes>d{1,2})s+(?<name>.*)$");
        private static Regex winStyle = new Regex(@"^(?<month>d{1,2})-(?<day>d{1,2})-(?<year>d{1,2})s+(?<hour>d{1,2}):(?<minutes>d{1,2})(?<ampm>am|pm)s+(?<dir>[<]dir[>])?s+(?<size>d+)?s+(?<name>.*)$");

        public static FtpLineResult Parse(string line)
        {
            Match match = unixStyle.Match(line);

            if (match.Success)
            {
                return ParseMatch(match.Groups, Enums.FtpListStyle.Unix);
            }

            match = winStyle.Match(line);

            if (match.Success)
            {
                return ParseMatch(match.Groups, Enums.FtpListStyle.Windows);
            }

            throw new Exception("Invalid line format");
        }

        private static FtpLineResult ParseMatch(GroupCollection matchGroups, Enums.FtpListStyle style)
        {
            string dirMatch = (style == Enums.FtpListStyle.Unix ? "d" : "<dir>");
            FtpLineResult result = new FtpLineResult();
            result.Style = style;
            result.IsDirectory = matchGroups["dir"].Value.Equals(dirMatch, StringComparison.InvariantCultureIgnoreCase);
            result.Name = matchGroups["name"].Value;
            if (!result.IsDirectory)
                result.Size = long.Parse(matchGroups["size"].Value);
            return result;
        }
    }
}
