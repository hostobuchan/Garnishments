using System;

namespace RecordTypes.RMS.DataTypes
{
    /// <summary>
    /// Data is associated with a specific list
    /// </summary>
    /// <typeparam name="T">Enumeration of List of Descriptions</typeparam>
    public class RMSEnum<T> : RecordTypes.EDI.EDIDataTypes.DataType
    {
        public new T Value { get { try { return (T)Enum.Parse(typeof(T), base.Value); } catch { return (T)Enum.Parse(typeof(T), "0"); } } set { base.DataString = Convert.ToInt32(value).ToString().PadLeft(base.DataLength, '0'); } }

        public RMSEnum(int DataLength) : base(DataLength) { }
    }

    /// <summary>
    /// Data is associated with a specific list
    /// </summary>
    /// <typeparam name="T">Enumeration of List of Descriptions</typeparam>
    /// <typeparam name="Q">Enumeration of List of Values</typeparam>
    public class RMSEnum<T, Q> : RecordTypes.EDI.EDIDataTypes.DataType
    {
        public new T Value { get { try { return (T)Enum.Parse(typeof(Q), base.Value); } catch { return (T)Enum.Parse(typeof(T), "0"); } } set { base.DataString = value.ToString() == "Nothing" ? "".PadRight(base.DataLength, ' ') : ((Q)Enum.Parse(typeof(T), value.ToString())).ToString(); } }

        public RMSEnum(int DataLength) : base(DataLength) { }
    }
}
