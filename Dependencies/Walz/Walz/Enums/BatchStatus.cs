using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Walz.Data.Enums
{
    [Flags]
    public enum BatchStatus : byte
    {
        Unknown = 0,
        BatchCreated = 1,
        BatchUploaded = 2,
        BatchReceived = 4
    }
}
