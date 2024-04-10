using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Walz.Data.Enums
{
    public enum FileVersion : byte
    {
        Unknown = 0,
        FileVersion1,
        FileVersion2,
        FileVersion2r,
        FileVersion2m,
        FileVersion2rm,
        FileVersion3,
        FileVersion3r,
        FileVersion3m,
        FileVersion3rm
    }
}
