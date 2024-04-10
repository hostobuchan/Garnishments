using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace org.pdfclown.util.io
{
    public class TIFF
    {
        public string FileLocation { get; private set; }
        protected EndianFormatter tiff { get; set; }
        public List<IFD> IFDs { get; private set; }

        public TIFF(string FileLocation) : this(new System.IO.FileStream(FileLocation, System.IO.FileMode.Open)) { }

        public TIFF(System.IO.Stream tif)
        {
            this.IFDs = new List<IFD>();
            tif.Seek(0, System.IO.SeekOrigin.Begin);
            byte[] Header = new byte[2];

            tif.Read(Header, 0, 2);

            // Determine byte order
            if (Header[0] == (byte)0x49 && Header[1] == (byte)0x49) // Little Endian
            {
                this.tiff = new EndianFormatter(tif, Endian.LittleEndian);
            }
            else if (Header[0] == (byte)0x4D && Header[1] == (byte)0x4D) // Big Endian
            {
                this.tiff = new EndianFormatter(tif, Endian.BigEndian);
            }
            else
            {
                throw new NotImplementedException("Byte Order Could Not Be Determined!");
            }

            ushort tiffIndicator = this.tiff.GetUInt16();

            if (tiffIndicator == 42) // File is a TIFF file
            {
                long IFD_Loc = this.tiff.GetUInt32();

                while (IFD_Loc != 0)
                {
                    this.tiff.SetPosition(IFD_Loc);

                    this.IFDs.Add(new IFD(this.tiff));

                    IFD_Loc = this.tiff.GetUInt32();
                }
            }
            else
            {
                throw new NotImplementedException("File Does Not Contain TIFF!");
            }
        }

        public System.IO.Stream GetImageStream()
        {
            Field Offsets = this.IFDs[0].Fields.Find(el => el.FieldName == TiffFieldName.StripOffsets);
            Field Counts = this.IFDs[0].Fields.Find(el => el.FieldName == TiffFieldName.StripByteCounts);

            object OffsetsValues = Offsets.GetValue(this);
            object CountsValues = Counts.GetValue(this);

            System.IO.Stream ms = new System.IO.MemoryStream();

            if (OffsetsValues is uint[])
            {
                for (int i = 0; i < ((uint[])OffsetsValues).Length; i++)
                {
                    long Seek = (long)((uint[])OffsetsValues)[i];
                    byte[] bytes = new byte[(int)((uint[])CountsValues)[i]];

                    this.tiff.SourceStream.Seek(Seek, System.IO.SeekOrigin.Begin);
                    this.tiff.SourceStream.Read(bytes, 0, (int)((uint[])CountsValues)[i]);

                    ms.Write(bytes, 0, bytes.Length);
                    ms.Flush();
                }
            }
            else if (OffsetsValues is uint)
            {
                byte[] bytes = new byte[(uint)CountsValues];

                this.tiff.SourceStream.Seek((long)OffsetsValues, System.IO.SeekOrigin.Begin);
                this.tiff.SourceStream.Read(bytes, 0, (int)CountsValues);

                ms.Write(bytes, 0, bytes.Length);
                ms.Flush();
            }

            return ms;
        }

        public class IFD
        {
            public ushort Entries { get; private set; }
            public List<Field> Fields { get; private set; }

            public IFD(EndianFormatter EndianStream)
            {
                this.Fields = new List<Field>();
                this.Entries = EndianStream.GetUInt16();

                for (int i = 0; i < this.Entries; i++)
                {
                    this.Fields.Add(new Field(EndianStream));
                }
            }
        }

        public class Field
        {
            public ushort Tag { get; private set; }
            public ushort Type { get; private set; }
            public uint Values { get; private set; }
            public uint Offset { get; private set; }
            public TiffFieldName FieldName { get { try { return (TiffFieldName)Tag; } catch { return TiffFieldName.Unknown; } } }
            public TiffDataType DataType { get { return (TiffDataType)Type; } }
            public object GetValue(TIFF TiffExaminer)
            {
                EndianFormatter FileStream = TiffExaminer.tiff;
                byte[] bytes = BitConverter.GetBytes(this.Offset);
                if (DetermineByteLength(this.DataType, (int)this.Values) > 4)
                {
                    FileStream.SetPosition((long)this.Offset);
                    object Value;
                    switch (DetermineByteLength(this.DataType, 1))
                    {
                        case 1:
                            Value = new byte[Values];
                            for (int i = 0; i < Values; i++)
                            {
                                ((byte[])Value)[i] = FileStream.GetByte();
                            }
                            if (this.DataType == TiffDataType.ASCII) return StringFormatter((byte[])Value);
                            else return Value;
                        case 2:
                            Value = new ushort[Values];
                            for (int i = 0; i < Values; i++)
                            {
                                ((ushort[])Value)[i] = FileStream.GetUInt16();
                            }
                            return Value;
                        case 4:
                            Value = new uint[Values];
                            for (int i = 0; i < Values; i++)
                            {
                                ((uint[])Value)[i] = FileStream.GetUInt32();
                            }
                            return Value;
                        case 8:
                            Value = new uint[Values * 2];
                            for (int i = 0; i < Values * 2; i++)
                            {
                                ((uint[])Value)[i] = FileStream.GetUInt32();
                            }
                            return Value;
                        default:
                            return null;
                    }
                }
                else
                {
                    if (this.Values == 1)
                    {
                        switch (DetermineByteLength(this.DataType, 1))
                        {
                            case 1:
                                return bytes[0];
                            case 2:
                                return (ushort)(bytes[0] | bytes[1] << 8);
                            case 4:
                                return this.Offset;
                            default:
                                return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            public Field(EndianFormatter EndianStream)
            {
                this.Tag = EndianStream.GetUInt16();
                this.Type = EndianStream.GetUInt16();
                this.Values = EndianStream.GetUInt32();
                this.Offset = EndianStream.GetUInt32();
            }
        }

        public static int DetermineByteLength(TiffDataType Type, int Values)
        {
            switch (Type)
            {
                case TiffDataType.BYTE:
                case TiffDataType.ASCII:
                case TiffDataType.SBYTE:
                case TiffDataType.UNDEFINED:
                    return Values;
                case TiffDataType.SHORT:
                case TiffDataType.SSHORT:
                    return Values * 2;
                case TiffDataType.LONG:
                case TiffDataType.SLONG:
                case TiffDataType.FLOAT:
                    return Values * 4;
                case TiffDataType.RATIONAL:
                case TiffDataType.SRATIONAL:
                case TiffDataType.DOUBLE:
                    return Values * 8;
                default:
                    return Values;
            }
        }

        public static string StringFormatter(byte[] Chars)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte B in Chars)
            {
                sb.Append((char)B);
            }
            return sb.ToString();
        }
    }

    public enum TiffFieldName : ushort
    {
        Unknown = 0,
        NewSubfileType = 254,
        SubfileType = 255,
        ImageWidth = 256,
        ImageLength = 257,
        BitsPerSample = 258,
        Compression = 259,
        PhotometricInterpretation = 262,
        Threshholding = 263,
        CellWidth = 264,
        CellLength = 265,
        FillOrder = 266,
        DocumentName = 269,
        ImageDescription = 270,
        Make = 271,
        Model = 272,
        StripOffsets = 273,
        Orientation = 274,
        SamplesPerPixel = 277,
        RowsPerStrip = 278,
        StripByteCounts = 279,
        MinSampleValue = 280,
        MaxSampleValue = 281,
        XResolution = 282,
        YResolution = 283,
        PlanarConfiguration = 284,
        PageName = 285,
        T4Options = 292,
        T6Options = 293,
        ResolutionUnit = 296,
        PageNumber = 297,
        Software = 305,
        DateTime = 306,
        ColorMap = 320
    }

    public enum TiffDataType : ushort
    {
        BYTE = 1,
        ASCII = 2,
        SHORT = 3,
        LONG = 4,
        RATIONAL = 5,
        SBYTE = 6,
        UNDEFINED = 7,
        SSHORT = 8,
        SLONG = 9,
        SRATIONAL = 10,
        FLOAT = 11,
        DOUBLE = 12
    }
}
