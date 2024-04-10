using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Walz.Data.Files.Records
{
    public class BaseRecord
    {
        protected string[] Segments { get; private set; }

        public BaseRecord(string Record)
        {
            this.Segments = Record.Split('\t');
        }
        protected BaseRecord(int Segments)
        {
            this.Segments = new string[Segments];
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.Segments.Length; i++)
            {
                if (i < this.Segments.Length - 1)
                    sb.AppendFormat("{0}\t", this.Segments[i]);
                else
                    sb.Append(this.Segments[i]);
            }
            return sb.ToString();
        }
    }
}
