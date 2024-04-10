using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HB.SkipTracing.Data.Records;
using HB.SkipTracing.Data.RNN.Records;
using RecordTypes.MergePops;
using RecordTypes.YGC.Base;
using System.Data.SqlClient;
using System.Data;

namespace HB.SkipTracing.Data.RNN
{
    public class DataHandler : Data.DataHandler<Data.Records.UploadRecord, Data.Records.DownloadRecord>
    {
        public override string Vendor { get { return "RNN"; } }
    }
}
