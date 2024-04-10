using System;
using System.Collections.Generic;

namespace RecordTypes.Utilities
{
    class CaseInsensitiveStringComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            return string.Equals(x, y, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(string obj)
        {
            return obj.ToUpperInvariant().GetHashCode();
        }
    }
}
