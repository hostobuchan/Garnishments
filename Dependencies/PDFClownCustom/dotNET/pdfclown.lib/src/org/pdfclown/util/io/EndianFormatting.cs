using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace org.pdfclown.util.io
{
    public class EndianFormatter
    {
        public Endian EndianFormat { get; private set; }
        internal System.IO.Stream SourceStream { get; private set; }

        public EndianFormatter(System.IO.Stream SourceStream, Endian EndianFormat)
        {
            this.SourceStream = SourceStream;
            this.EndianFormat = EndianFormat;
        }

        public void SetPosition(long Address)
        {
            SourceStream.Seek(Address, System.IO.SeekOrigin.Begin);
        }

        public long OffSet(long OffSetAmount)
        {
            return SourceStream.Seek(OffSetAmount, System.IO.SeekOrigin.Current);
        }

        public byte GetByte()
        {
            return (byte)SourceStream.ReadByte();
        }

        public int GetInt32()
        {
            if (this.EndianFormat == Endian.BigEndian)
            {
                return (int)(SourceStream.ReadByte() << 24 | SourceStream.ReadByte() << 16 | SourceStream.ReadByte() << 8 | SourceStream.ReadByte());
            }
            else
            {
                return (int)(SourceStream.ReadByte() | SourceStream.ReadByte() << 8 | SourceStream.ReadByte() << 16 | SourceStream.ReadByte() << 24);
            }
        }

        public short GetInt16()
        {
            if (this.EndianFormat == Endian.BigEndian)
            {
                return (short)(SourceStream.ReadByte() << 8 | SourceStream.ReadByte());
            }
            else
            {
                return (short)(SourceStream.ReadByte() | SourceStream.ReadByte() << 8);
            }
        }

        public uint GetUInt32()
        {
            if (this.EndianFormat == Endian.BigEndian)
            {
                return (uint)(SourceStream.ReadByte() << 24 | SourceStream.ReadByte() << 16 | SourceStream.ReadByte() << 8 | SourceStream.ReadByte());
            }
            else
            {
                return (uint)(SourceStream.ReadByte() | SourceStream.ReadByte() << 8 | SourceStream.ReadByte() << 16 | SourceStream.ReadByte() << 24);
            }
        }

        public ushort GetUInt16()
        {
            if (this.EndianFormat == Endian.BigEndian)
            {
                return (ushort)(SourceStream.ReadByte() << 8 | SourceStream.ReadByte());
            }
            else
            {
                return (ushort)(SourceStream.ReadByte() | SourceStream.ReadByte() << 8);
            }
        }
    }

    public enum Endian
    {
        BigEndian,
        LittleEndian
    }
}
