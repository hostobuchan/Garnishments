using RecordTypes.Dantom.NCOA.DataTypes;
using RecordTypes.Dantom.NCOA.Enums;
using RecordTypes.EDI.EDIDataTypes;
using System;

namespace RecordTypes.Dantom.NCOA
{
    namespace DataTypes
    {
        /// <summary>
        /// Basic Character String
        /// </summary>
        public class NCOAString : DataType
        {
            public new string Value { get { return base.DataString.Trim(); } set { base.DataString = value; } }

            public NCOAString(int DataLength) : base(DataLength) { }
        }
        /// <summary>
        /// Date Stored as YYMM
        /// </summary>
        public class NCOADate : DataType
        {
            public new DateTime? Value { get { try { return DateTime.ParseExact(base.Value, "yyMM", System.Globalization.CultureInfo.CurrentCulture); } catch { return null; } } set { base.DataString = value == null ? "" : value.Value.ToString("yyMM"); } }

            public NCOADate() : base(4) { }
        }
        /// <summary>
        /// Basic Number with no decimals
        /// </summary>
        public class NCOANumber : DataType
        {
            public new int? Value { get { try { return int.Parse(base.Value); } catch { return null; } } set { base.DataString = value == null || value < 0 || value > 10 * (base.DataLength + 1) ? "" : value.ToString().PadLeft(base.DataLength, '0'); } }

            public NCOANumber(int DataLength) : base(DataLength) { }
        }
        /// <summary>
        /// Boolean data determined by the presence of char/string
        /// </summary>
        public class NCOABool : DataType
        {
            private string Compare;
            private string Fail;
            public new bool Value { get { return base.Value == this.Compare; } set { base.DataString = value ? this.Compare : this.Fail; } }

            public NCOABool(int DataLength, string CompareString, string FailString = "")
                : base(DataLength)
            {
                this.Compare = CompareString;
                this.Fail = FailString;
            }
        }
        /// <summary>
        /// Data is associated with a specific list
        /// </summary>
        /// <typeparam name="T">Enumeration of List of Descriptions</typeparam>
        public class NCOAEnum<T> : DataType
        {
            public new T Value { get { try { return (T)Enum.Parse(typeof(T), base.Value); } catch { return (T)Enum.Parse(typeof(T), "0"); } } set { base.DataString = Convert.ToInt32(value).ToString().PadLeft(base.DataLength, '0'); } }

            public NCOAEnum(int DataLength) : base(DataLength) { }

            public override string ToString(string format, IFormatProvider formatProvider)
            {
                if (format == "G")
                {
                    return $"{this.Value}";
                }
                else
                {
                    return this.DataString;
                }
            }
        }
        /// <summary>
        /// Data is associated with a specific list
        /// </summary>
        /// <typeparam name="T">Enumeration of List of Descriptions</typeparam>
        /// <typeparam name="Q">Enumeration of List of Values</typeparam>
        public class NCOAEnum<T, Q> : DataType
        {
            public new T Value { get { try { return (T)Enum.Parse(typeof(Q), base.Value); } catch { return (T)Enum.Parse(typeof(T), "0"); } } set { base.DataString = value.ToString() == "Nothing" ? "".PadRight(base.DataLength, ' ') : ((Q)Enum.Parse(typeof(T), value.ToString())).ToString(); } }

            public NCOAEnum(int DataLength) : base(DataLength) { }

            public override string ToString(string format, IFormatProvider formatProvider)
            {
                if (format == "G")
                {
                    return $"{this.Value}";
                }
                else
                {
                    return this.DataString;
                }
            }
        }
    }

    namespace Enums
    {
        public enum DeliverabilityCode
        {
            Nothing = 0,
            /// <summary>
            /// Box Closed
            /// </summary>
            BoxClosed,
            /// <summary>
            /// Moved, Left No Forwarding Address (MLNF)
            /// </summary>
            NoForwardingAddress,
            /// <summary>
            /// Foreign move, No New Address Returned
            /// </summary>
            ForeignMove,
            /// <summary>
            /// Moved with Forwarding Address
            /// </summary>
            ForwardableMove,
            /// <summary>
            /// No Match on File at This Time
            /// </summary>
            NoMatch
        }

        public enum DeliverabilityCodeValues
        {
            Unknown = 0,
            G,
            K,
            F,
            M,
            N
        }

        public enum MoveType
        {
            Nothing = 0,
            Family,
            Individual,
            Business
        }

        public enum MoveTypeValues
        {
            Unknown = 0,
            F,
            I,
            B
        }

        public enum PhoneLevelMatch
        {
            Nothing = 0,
            MatchName,
            MatchAddress
        }

        public enum PhoneLevelMatchValues
        {
            Unknown = 0,
            S,
            A
        }

        public enum ErrorCode
        {
            NoError = 0,
            /// <summary>
            /// Attempted, not known
            /// </summary>
            Attempted,
            /// <summary>
            /// Returned for better address
            /// </summary>
            NeedBetterAddress,
            /// <summary>
            /// Canadian ZIP Code
            /// </summary>
            CanadianZip,
            /// <summary>
            /// Outside delivery limits
            /// </summary>
            OutsideLimits,
            /// <summary>
            /// In dispute
            /// </summary>
            InDispute,
            /// <summary>
            /// General Delivery (No +4 Zip)
            /// </summary>
            GeneralDelivery,
            /// <summary>
            /// Insufficient address
            /// </summary>
            InsufficientAddress,
            /// <summary>
            /// Illegible
            /// </summary>
            Illegible,
            /// <summary>
            /// More than one record matches the address
            /// </summary>
            MultipleRecordMatches,
            /// <summary>
            /// ZIP Code exists but does not have street names
            /// </summary>
            NoStreetName,
            /// <summary>
            /// No mail receptacle
            /// </summary>
            NoMailReceptacle,
            /// <summary>
            /// Unclaimed
            /// </summary>
            Unclaimed,
            /// <summary>
            /// Non-Deliverable Address
            /// </summary>
            NonDeliverableAddress,
            /// <summary>
            /// Address number not in range
            /// </summary>
            NumberNotInRange,
            /// <summary>
            /// Refused
            /// </summary>
            Refused,
            /// <summary>
            /// One secondary part of the address did not match correctly
            /// <para>For example, if the given address was “100 Main St” and the only addresses found were “100 E Main St” and “100 W Main St”, the error code “T” would be returned.  This code will also be returned if more than one street with the same name.</para>
            /// <para>ie: Main St, Main Ave or Main Dr. exists within the given city/state/zip.</para>
            /// </summary>
            SecondaryMismatch,
            /// <summary>
            /// No matching street names found
            /// </summary>
            NoStreetMatch,
            /// <summary>
            /// Vacant Property
            /// </summary>
            Vacant,
            /// <summary>
            /// No such office
            /// </summary>
            NoSuchOffice,
            /// <summary>
            /// Non-Deliverable Address
            /// </summary>
            NonDeliverableAddress_ERM,
            /// <summary>
            /// No such ZIP Code
            /// </summary>
            NoSuchZip
        }

        public enum ErrorCodeValues
        {
            Unknown = 0,
            A,
            B,
            C,
            D,
            E,
            G,
            I,
            L,
            M,
            N,
            O,
            P,
            Q,
            R,
            S,
            T,
            U,
            V,
            W,
            X,
            Y,
            Z
        }
    }

    public class Record
    {
        #region Public Properties
        public NIXIE NIXIE_Footnotes { get; private set; }
        public NCOAEnum<DeliverabilityCode, DeliverabilityCodeValues> NCOA_DeliverabilityCode { get; private set; }
        public NCOAString NCOA_Address { get; private set; }
        public NCOAString NCOA_City { get; private set; }
        public NCOAString NCOA_State { get; private set; }
        public NCOAString NCOA_Zip { get; private set; }
        public NCOAString NCOA_Zip_4 { get; private set; }
        public NCOANumber NCOA_DeliveryPointBarcode { get; private set; }
        public NCOAEnum<MoveType, MoveTypeValues> NCOA_MoveCode { get; private set; }
        public NCOADate NCOA_MoveEffectiveDate { get; private set; }
        public FootNotes Inquiry_FootNotes { get; private set; }
        public NCOAString Inquiry_OptionalAddress { get; private set; }
        public NCOAString Inquiry_DeliveryAddress { get; private set; }
        public NCOAString Inquiry_City { get; private set; }
        public NCOAString Inquiry_State { get; private set; }
        public NCOAString Inquiry_Zip { get; private set; }
        public NCOAString Inquiry_Zip_4 { get; private set; }
        public NCOANumber Inquiry_DeliveryPointBarcode { get; private set; }
        public NCOAString Filler1 { get; private set; }
        public NCOAString NCOA_Phone { get; private set; }
        public NCOAString Filler2 { get; private set; }
        public NCOAEnum<PhoneLevelMatch, PhoneLevelMatchValues> NCOA_Phone_LevelOfMatch { get; private set; }
        public AEC AEC_Info { get; private set; }
        public NCOAString Input_LetterCode { get; private set; }
        public NCOAString Input_FullName { get; private set; }
        public NCOAString Input_Address1 { get; private set; }
        public NCOAString Input_Address2 { get; private set; }
        public NCOAString Input_CSZ { get; private set; }
        public NCOANumber ServiceRequested { get; private set; }
        public NCOAString Input_NCOAControlField { get; private set; }
        public NCOANumber DebtorNumber { get; private set; }
        #endregion

        public Record(string Record)
        {
            try
            {
                this.NIXIE_Footnotes = new NIXIE(Record.Substring(0, 26));
                this.NCOA_DeliverabilityCode = new NCOAEnum<DeliverabilityCode, DeliverabilityCodeValues>(1) { DataString = Record.Substring(26) };
                this.NCOA_Address = new NCOAString(48) { DataString = Record.Substring(27) };
                this.NCOA_City = new NCOAString(28) { DataString = Record.Substring(75) };
                this.NCOA_State = new NCOAString(2) { DataString = Record.Substring(103) };
                this.NCOA_Zip = new NCOAString(5) { DataString = Record.Substring(105) };
                this.NCOA_Zip_4 = new NCOAString(4) { DataString = Record.Substring(110) };
                this.NCOA_DeliveryPointBarcode = new NCOANumber(3) { DataString = Record.Substring(114) };
                this.NCOA_MoveCode = new NCOAEnum<MoveType, MoveTypeValues>(1) { DataString = Record.Substring(117) };
                this.NCOA_MoveEffectiveDate = new NCOADate() { DataString = Record.Substring(118) };
                this.Inquiry_FootNotes = new FootNotes(Record.Substring(122, 10));
                this.Inquiry_OptionalAddress = new NCOAString(31) { DataString = Record.Substring(132) };
                this.Inquiry_DeliveryAddress = new NCOAString(48) { DataString = Record.Substring(163) };
                this.Inquiry_City = new NCOAString(28) { DataString = Record.Substring(211) };
                this.Inquiry_State = new NCOAString(2) { DataString = Record.Substring(239) };
                this.Inquiry_Zip = new NCOAString(5) { DataString = Record.Substring(241) };
                this.Inquiry_Zip_4 = new NCOAString(4) { DataString = Record.Substring(246) };
                this.Inquiry_DeliveryPointBarcode = new NCOANumber(3) { DataString = Record.Substring(250) };
                this.Filler1 = new NCOAString(20) { DataString = Record.Substring(253) };
                this.NCOA_Phone = new NCOAString(10) { DataString = Record.Substring(273) };
                this.Filler2 = new NCOAString(8) { DataString = Record.Substring(283) };
                this.NCOA_Phone_LevelOfMatch = new NCOAEnum<PhoneLevelMatch, PhoneLevelMatchValues>(1) { DataString = Record.Substring(291) };
                this.AEC_Info = new AEC(Record.Substring(292, 49));
                this.Input_LetterCode = new NCOAString(4) { DataString = Record.Substring(341) };
                this.Input_FullName = new NCOAString(40) { DataString = Record.Substring(345) };
                this.Input_Address1 = new NCOAString(40) { DataString = Record.Substring(385) };
                this.Input_Address2 = new NCOAString(40) { DataString = Record.Substring(425) };
                this.Input_CSZ = new NCOAString(28) { DataString = Record.Substring(465) };
                this.ServiceRequested = new NCOANumber(2) { DataString = Record.Substring(493) };
                this.Input_NCOAControlField = new NCOAString(42) { DataString = Record.Substring(495) };
                this.DebtorNumber = new NCOANumber(3) { DataString = Record.Substring(537) };
            }
            catch
            {
                if (this.NIXIE_Footnotes == null) this.NIXIE_Footnotes = new NIXIE("");
                if (this.NCOA_DeliverabilityCode == null) this.NCOA_DeliverabilityCode = new NCOAEnum<DeliverabilityCode, DeliverabilityCodeValues>(1);
                if (this.NCOA_Address == null) this.NCOA_Address = new NCOAString(48);
                if (this.NCOA_City == null) this.NCOA_City = new NCOAString(28);
                if (this.NCOA_State == null) this.NCOA_State = new NCOAString(2);
                if (this.NCOA_Zip == null) this.NCOA_Zip = new NCOAString(5);
                if (this.NCOA_Zip_4 == null) this.NCOA_Zip_4 = new NCOAString(4);
                if (this.NCOA_DeliveryPointBarcode == null) this.NCOA_DeliveryPointBarcode = new NCOANumber(3);
                if (this.NCOA_MoveCode == null) this.NCOA_MoveCode = new NCOAEnum<MoveType, MoveTypeValues>(1);
                if (this.NCOA_MoveEffectiveDate == null) this.NCOA_MoveEffectiveDate = new NCOADate();
                if (this.Inquiry_FootNotes == null) this.Inquiry_FootNotes = new FootNotes("");
                if (this.Inquiry_OptionalAddress == null) this.Inquiry_OptionalAddress = new NCOAString(31);
                if (this.Inquiry_DeliveryAddress == null) this.Inquiry_DeliveryAddress = new NCOAString(48);
                if (this.Inquiry_City == null) this.Inquiry_City = new NCOAString(28);
                if (this.Inquiry_State == null) this.Inquiry_State = new NCOAString(2);
                if (this.Inquiry_Zip == null) this.Inquiry_Zip = new NCOAString(5);
                if (this.Inquiry_Zip_4 == null) this.Inquiry_Zip_4 = new NCOAString(4);
                if (this.Inquiry_DeliveryPointBarcode == null) this.Inquiry_DeliveryPointBarcode = new NCOANumber(3);
                if (this.Filler1 == null) this.Filler1 = new NCOAString(20);
                if (this.NCOA_Phone == null) this.NCOA_Phone = new NCOAString(10);
                if (this.Filler2 == null) this.Filler2 = new NCOAString(8);
                if (this.NCOA_Phone_LevelOfMatch == null) this.NCOA_Phone_LevelOfMatch = new NCOAEnum<PhoneLevelMatch, PhoneLevelMatchValues>(1);
                if (this.AEC_Info == null) this.AEC_Info = new AEC("");
                if (this.Input_LetterCode == null) this.Input_LetterCode = new NCOAString(4);
                if (this.Input_FullName == null) this.Input_FullName = new NCOAString(40);
                if (this.Input_Address1 == null) this.Input_Address1 = new NCOAString(40);
                if (this.Input_Address2 == null) this.Input_Address2 = new NCOAString(40);
                if (this.Input_CSZ == null) this.Input_CSZ = new NCOAString(28);
                if (this.ServiceRequested == null) this.ServiceRequested = new NCOANumber(2);
                if (this.Input_NCOAControlField == null) this.Input_NCOAControlField = new NCOAString(42);
                if (this.DebtorNumber == null) this.DebtorNumber = new NCOANumber(3);
            }
        }

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}{25}{26}{27}{28}{29}{30}",
                this.NIXIE_Footnotes,
                this.NCOA_DeliverabilityCode,
                this.NCOA_Address,
                this.NCOA_City,
                this.NCOA_State,
                this.NCOA_Zip,
                this.NCOA_Zip_4,
                this.NCOA_DeliveryPointBarcode,
                this.NCOA_MoveCode,
                this.NCOA_MoveEffectiveDate,
                this.Inquiry_FootNotes,
                this.Inquiry_OptionalAddress,
                this.Inquiry_DeliveryAddress,
                this.Inquiry_City,
                this.Inquiry_State,
                this.Inquiry_Zip,
                this.Inquiry_Zip_4,
                this.Inquiry_DeliveryPointBarcode,
                this.Filler1,
                this.NCOA_Phone,
                this.Filler2,
                this.NCOA_Phone_LevelOfMatch,
                this.AEC_Info,
                this.Input_LetterCode,
                this.Input_FullName,
                this.Input_Address1,
                this.Input_Address2,
                this.Input_CSZ,
                this.ServiceRequested,
                this.Input_NCOAControlField,
                this.DebtorNumber);
        }

        public class NIXIE : IFormatProvider, IFormattable
        {
            #region Public Properties
            /// <summary>
            /// A - NCOA Match
            /// <para>The input record matched to an NCOA master file record using the current NCOA rules.  A new address could be furnished.</para>
            /// </summary>
            public NCOABool NCOA_Match { get; private set; }
            /// <summary>
            /// B - Possible NCOA Match
            /// <para>The input record was similar to an NCOA master file record.  However, an NCOA match could not be made because of one or more NCOA rules.  Additional footnotes are given to indicate reason(s) an NCOA match could not be made.</para>
            /// </summary>
            public NCOABool NCOA_PossibleMatch { get; private set; }
            /// <summary>
            /// C - No Match Available
            /// <para>The input record was not similar to an NCOA master file record.</para>
            /// </summary>
            public NCOABool NoMatch { get; private set; }
            /// <summary>
            /// D - House Number Missing
            /// <para>The input record is a street record which is missing the house number.  NCOA rules require the input house number to be exactly the same as a master file record for a match to occur.</para>
            /// </summary>
            public NCOABool HouseNumberMissing { get; private set; }
            /// <summary>
            /// E - Apartment Number Missing
            /// <para>The input record contains an apartment number.  However, the master file record does not.  NCOA rules require the master file record contain the same apartment number as the input record.</para>
            /// </summary>
            public NCOABool ApartmentNumberMissing { get; private set; }
            /// <summary>
            /// F - Surnames Do Not Match
            /// <para>The input surname is phonetically similar to the NCOA master file record.  However, an NCOA match cannot be made because first names do not match.</para>
            /// </summary>
            public NCOABool SurnamesDoNotMatch { get; private set; }
            /// <summary>
            /// G - Individual Move, First Names Do Not Match
            /// <para>The input record was compared to an individual record on the NCOA master file.  However, an NCOA match cannot be made because first names do not match.</para>
            /// </summary>
            public NCOABool FirstNamesDoNotMatch { get; private set; }
            /// <summary>
            /// H - Titles Do Not Match
            /// <para>An NCOA match was not made because either the input record and/or the master file record contain a title such as Jr. Sr. I. II or III and the title is not the same on both files.</para>
            /// </summary>
            public NCOABool TitlesDoNotMatch { get; private set; }
            /// <summary>
            /// I - Firm Names Do Not Match
            /// <para>The input record was compared to a firm record but a match could not be made.</para>
            /// </summary>
            public NCOABool FirmNamesDoNotMatch { get; private set; }
            /// <summary>
            /// J - Input File Missing First Name
            /// <para>The input record was compared to an individual record on the NCOA master file.  However, an NCOA match cannot be made because the input file does not contain a first name or initials.</para>
            /// </summary>
            public NCOABool Input_MissingFirstName { get; private set; }
            /// <summary>
            /// K - Middle Name or Initial Does Not Match
            /// <para>The NCOA master file record is for an individual move and the middle name or initial are not the same of both files.</para>
            /// </summary>
            public NCOABool MiddleNameDoesNotMatch { get; private set; }
            /// <summary>
            /// L - Different Gender
            /// <para>The NCOA master file record is for an individual move.  An NCOA match was not made because the two files do not indicate the same gender.</para>
            /// </summary>
            public NCOABool DifferentGender { get; private set; }
            /// <summary>
            /// M - Input Address Missing Box Number
            /// <para>An NCOA match was not made because the input address is a Rural Route or Highway Contract, and it does not have a Box Number.</para>
            /// </summary>
            public NCOABool Input_MissingBoxNumber { get; private set; }
            /// <summary>
            /// N - Address Not Matched
            /// <para>The input address and the NCOA master file address were compared, and because of differences in pre-directional, post-directional, or suffix, an NCOA match could not be made.</para>
            /// </summary>
            public NCOABool AddressNotMatched { get; private set; }
            /// <summary>
            /// O - Street Names Not Matched
            /// <para>The input address and the NCOA master file address were compared and the street name was not similar enough to make an NCOA match.</para>
            /// </summary>
            public NCOABool StreetNotMatched { get; private set; }
            /// <summary>
            /// P - N/A
            /// </summary>
            public NCOABool NA { get; private set; }
            /// <summary>
            /// Q - Apartment Number Not Equal
            /// <para>The apartment number on the input record does not match the apartment number on the master file record.</para>
            /// </summary>
            public NCOABool ApartmentNumberNotMatched { get; private set; }
            /// <summary>
            /// R - Individual Move
            /// <para>The input record was compared or matched to an individual record on the NCOA master file.</para>
            /// </summary>
            public NCOABool IndividualMove { get; private set; }
            /// <summary>
            /// S - Firm Move
            /// <para>The input record was compared or matched to a firm record on the NCOA master file.</para>
            /// </summary>
            public NCOABool FirmMove { get; private set; }
            /// <summary>
            /// T - Family Move
            /// <para>The input record was compared or matched to a family record on the NCOA master file.</para>
            /// </summary>
            public NCOABool FamilyMove { get; private set; }
            /// <summary>
            /// U - Apartment Number Missing, Input File
            /// <para>The master file record contains an apartment number.  However, the input record does not.  NCOA rules require the input record contain the same apartment number as the master file record.</para>
            /// </summary>
            public NCOABool Input_MissingApartment { get; private set; }
            /// <summary>
            /// V - First Name Matches First Initial, Individual Move
            /// <para>An NCOA match could not be made because of name matching rules for an individual move.  However, one file contains an initial which matches to the first name on the other file.</para>
            /// </summary>
            public NCOABool MatchingInitial { get; private set; }
            /// <summary>
            /// W - Transposition of Apartment Number
            /// <para>The last two digits of the apartment number have been transposed.</para>
            /// </summary>
            public NCOABool TransposedApartment { get; private set; }
            /// <summary>
            /// X - Exceptional Unit Designators Do Not Match
            /// <para>The exceptional unit designator (such as Basement, Pier, or Penthouse) on the input record does not match the exceptional unit designator on the NCOA master file.</para>
            /// </summary>
            public NCOABool ExceptionalUnitsDoNotMatch { get; private set; }
            public NCOAString Filler { get; private set; }
            #endregion

            public NIXIE(string Entry)
            {
                try
                {
                    this.NCOA_Match = new NCOABool(1, "A") { DataString = Entry.Substring(0) };
                    this.NCOA_PossibleMatch = new NCOABool(1, "B") { DataString = Entry.Substring(1) };
                    this.NoMatch = new NCOABool(1, "C") { DataString = Entry.Substring(2) };
                    this.HouseNumberMissing = new NCOABool(1, "D") { DataString = Entry.Substring(3) };
                    this.ApartmentNumberMissing = new NCOABool(1, "E") { DataString = Entry.Substring(4) };
                    this.SurnamesDoNotMatch = new NCOABool(1, "F") { DataString = Entry.Substring(5) };
                    this.FirstNamesDoNotMatch = new NCOABool(1, "G") { DataString = Entry.Substring(6) };
                    this.TitlesDoNotMatch = new NCOABool(1, "H") { DataString = Entry.Substring(7) };
                    this.FirmNamesDoNotMatch = new NCOABool(1, "I") { DataString = Entry.Substring(8) };
                    this.Input_MissingFirstName = new NCOABool(1, "J") { DataString = Entry.Substring(9) };
                    this.MiddleNameDoesNotMatch = new NCOABool(1, "K") { DataString = Entry.Substring(10) };
                    this.DifferentGender = new NCOABool(1, "L") { DataString = Entry.Substring(11) };
                    this.Input_MissingBoxNumber = new NCOABool(1, "M") { DataString = Entry.Substring(12) };
                    this.AddressNotMatched = new NCOABool(1, "N") { DataString = Entry.Substring(13) };
                    this.StreetNotMatched = new NCOABool(1, "O") { DataString = Entry.Substring(14) };
                    this.NA = new NCOABool(1, "P") { DataString = Entry.Substring(15) };
                    this.ApartmentNumberNotMatched = new NCOABool(1, "Q") { DataString = Entry.Substring(16) };
                    this.IndividualMove = new NCOABool(1, "R") { DataString = Entry.Substring(17) };
                    this.FirmMove = new NCOABool(1, "S") { DataString = Entry.Substring(18) };
                    this.FamilyMove = new NCOABool(1, "T") { DataString = Entry.Substring(19) };
                    this.Input_MissingApartment = new NCOABool(1, "U") { DataString = Entry.Substring(20) };
                    this.MatchingInitial = new NCOABool(1, "V") { DataString = Entry.Substring(21) };
                    this.TransposedApartment = new NCOABool(1, "W") { DataString = Entry.Substring(22) };
                    this.ExceptionalUnitsDoNotMatch = new NCOABool(1, "X") { DataString = Entry.Substring(23) };
                    this.Filler = new NCOAString(2) { DataString = Entry.Substring(24) };
                }
                catch
                {
                    if (this.NCOA_Match == null) this.NCOA_Match = new NCOABool(1, "A");
                    if (this.NCOA_PossibleMatch == null) this.NCOA_PossibleMatch = new NCOABool(1, "B");
                    if (this.NoMatch == null) this.NoMatch = new NCOABool(1, "C");
                    if (this.HouseNumberMissing == null) this.HouseNumberMissing = new NCOABool(1, "D");
                    if (this.ApartmentNumberMissing == null) this.ApartmentNumberMissing = new NCOABool(1, "E");
                    if (this.SurnamesDoNotMatch == null) this.SurnamesDoNotMatch = new NCOABool(1, "F");
                    if (this.FirmNamesDoNotMatch == null) this.FirstNamesDoNotMatch = new NCOABool(1, "G");
                    if (this.TitlesDoNotMatch == null) this.TitlesDoNotMatch = new NCOABool(1, "H");
                    if (this.FirmNamesDoNotMatch == null) this.FirmNamesDoNotMatch = new NCOABool(1, "I");
                    if (this.Input_MissingFirstName == null) this.Input_MissingFirstName = new NCOABool(1, "J");
                    if (this.MiddleNameDoesNotMatch == null) this.MiddleNameDoesNotMatch = new NCOABool(1, "K");
                    if (this.DifferentGender == null) this.DifferentGender = new NCOABool(1, "L");
                    if (this.Input_MissingBoxNumber == null) this.Input_MissingBoxNumber = new NCOABool(1, "M");
                    if (this.AddressNotMatched == null) this.AddressNotMatched = new NCOABool(1, "N");
                    if (this.StreetNotMatched == null) this.StreetNotMatched = new NCOABool(1, "O");
                    if (this.NA == null) this.NA = new NCOABool(1, "P");
                    if (this.ApartmentNumberNotMatched == null) this.ApartmentNumberNotMatched = new NCOABool(1, "Q");
                    if (this.IndividualMove == null) this.IndividualMove = new NCOABool(1, "R");
                    if (this.FirmMove == null) this.FirmMove = new NCOABool(1, "S");
                    if (this.FamilyMove == null) this.FamilyMove = new NCOABool(1, "T");
                    if (this.Input_MissingApartment == null) this.Input_MissingApartment = new NCOABool(1, "U");
                    if (this.MatchingInitial == null) this.MatchingInitial = new NCOABool(1, "V");
                    if (this.TransposedApartment == null) this.TransposedApartment = new NCOABool(1, "W");
                    if (this.ExceptionalUnitsDoNotMatch == null) this.ExceptionalUnitsDoNotMatch = new NCOABool(1, "X");
                    if (this.Filler == null) this.Filler = new NCOAString(2);
                }
            }

            public override string ToString()
            {
                return ToString(string.Empty, this);
            }

            public object GetFormat(Type formatType)
            {
                return System.Globalization.CultureInfo.CurrentCulture;
            }

            public string ToString(string format, IFormatProvider formatProvider)
            {
                if (format == "G")
                {
                    var str = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}",
                        this.NCOA_Match.Value ? "NCOA Match, " : "",
                        this.NCOA_PossibleMatch.Value ? "NCOA Possible Match, " : "",
                        this.NoMatch.Value ? "No Match, " : "",
                        this.HouseNumberMissing.Value ? "House Number Missing, " : "",
                        this.ApartmentNumberMissing.Value ? "Apartment Number Missing, " : "",
                        this.SurnamesDoNotMatch.Value ? "Surnames Do Not Match, " : "",
                        this.FirstNamesDoNotMatch.Value ? "First Names Do Not Match, " : "",
                        this.TitlesDoNotMatch.Value ? "Titles Do Not Match, " : "",
                        this.FirmNamesDoNotMatch.Value ? "Firm Names Do Not Match, " : "",
                        this.Input_MissingFirstName.Value ? "Input Missing First Name, " : "",
                        this.MiddleNameDoesNotMatch.Value ? "Middle Name Does Not Match, " : "",
                        this.DifferentGender.Value ? "Different Gender, " : "",
                        this.Input_MissingBoxNumber.Value ? "Input Missing Box Number, " : "",
                        this.AddressNotMatched.Value ? "Address Not Matched, " : "",
                        this.StreetNotMatched.Value ? "Street Not Matched, " : "",
                        this.ApartmentNumberNotMatched.Value ? "Apartment Number Not Matched, " : "",
                        this.IndividualMove.Value ? "Individual Moved, " : "",
                        this.FirmMove.Value ? "Firm Moved, " : "",
                        this.FamilyMove.Value ? "Family Moved, " : "",
                        this.Input_MissingApartment.Value ? "Input Missing Apartment Number, " : "",
                        this.MatchingInitial.Value ? "Matching Initial (not full name), " : "",
                        this.TransposedApartment.Value ? "Transposed Apartment Number, " : "",
                        this.ExceptionalUnitsDoNotMatch.Value ? "Exceptional Unit Does Not Match (basement, pier, or penthouse), " : "");
                    return str.Substring(0, str.Length > 1 ? str.Length - 2 : 0);
                }
                else
                {
                    return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}{11}{12}{13}{14}{15}{16}{17}{18}{19}{20}{21}{22}{23}{24}",
                        this.NCOA_Match,
                        this.NCOA_PossibleMatch,
                        this.NoMatch,
                        this.HouseNumberMissing,
                        this.ApartmentNumberMissing,
                        this.SurnamesDoNotMatch,
                        this.FirstNamesDoNotMatch,
                        this.TitlesDoNotMatch,
                        this.FirmNamesDoNotMatch,
                        this.Input_MissingFirstName,
                        this.MiddleNameDoesNotMatch,
                        this.DifferentGender,
                        this.Input_MissingBoxNumber,
                        this.AddressNotMatched,
                        this.StreetNotMatched,
                        this.NA,
                        this.ApartmentNumberNotMatched,
                        this.IndividualMove,
                        this.FirmMove,
                        this.FamilyMove,
                        this.Input_MissingApartment,
                        this.MatchingInitial,
                        this.TransposedApartment,
                        this.ExceptionalUnitsDoNotMatch,
                        this.Filler);
                }
            }
        }

        public class FootNotes : IFormatProvider, IFormattable
        {
            public NCOABool ZipCodeCorrected { get; private set; }
            public NCOABool CSCorrected { get; private set; }
            public NCOABool NotUsed1 { get; private set; }
            public NCOABool NotUsed2 { get; private set; }
            public NCOABool NoZip_4Avail { get; private set; }
            public NCOABool NotUsed3 { get; private set; }
            public NCOABool InsufficientData { get; private set; }
            public NCOABool StreetCorrected { get; private set; }
            public NCOABool NotUsed4 { get; private set; }
            public NCOAEnum<ErrorCode, ErrorCodeValues> ErrorCode { get; private set; }

            public FootNotes(string Entry)
            {
                try
                {
                    this.ZipCodeCorrected = new NCOABool(1, "A") { DataString = Entry.Substring(0) };
                    this.CSCorrected = new NCOABool(1, "B") { DataString = Entry.Substring(1) };
                    this.NotUsed1 = new NCOABool(1, "Y") { DataString = Entry.Substring(2) };
                    this.NotUsed2 = new NCOABool(1, "Y") { DataString = Entry.Substring(3) };
                    this.NoZip_4Avail = new NCOABool(1, "F") { DataString = Entry.Substring(4) };
                    this.NotUsed3 = new NCOABool(1, "Y") { DataString = Entry.Substring(5) };
                    this.InsufficientData = new NCOABool(1, "I") { DataString = Entry.Substring(6) };
                    this.StreetCorrected = new NCOABool(1, "T") { DataString = Entry.Substring(7) };
                    this.NotUsed4 = new NCOABool(1, "Y") { DataString = Entry.Substring(8) };
                    this.ErrorCode = new NCOAEnum<ErrorCode, ErrorCodeValues>(1) { DataString = Entry.Substring(9) };
                }
                catch
                {
                    if (this == null) this.ZipCodeCorrected = new NCOABool(1, "A");
                    if (this == null) this.CSCorrected = new NCOABool(1, "B");
                    if (this == null) this.NotUsed1 = new NCOABool(1, "Y");
                    if (this == null) this.NotUsed2 = new NCOABool(1, "Y");
                    if (this == null) this.NoZip_4Avail = new NCOABool(1, "F");
                    if (this == null) this.NotUsed3 = new NCOABool(1, "Y");
                    if (this == null) this.InsufficientData = new NCOABool(1, "I");
                    if (this == null) this.StreetCorrected = new NCOABool(1, "T");
                    if (this == null) this.NotUsed4 = new NCOABool(1, "Y");
                    if (this == null) this.ErrorCode = new NCOAEnum<ErrorCode, ErrorCodeValues>(1);
                }
            }

            public override string ToString()
            {
                return ToString(string.Empty, this);
            }
            public string ToString(string format)
            {
                return ToString(format, this);
            }

            public object GetFormat(Type formatType)
            {
                return System.Globalization.CultureInfo.CurrentCulture;
            }

            public string ToString(string format, IFormatProvider formatProvider)
            {

                if (format == "G")
                {
                    var str = string.Format("{0}{1}{2}{3}{4}{5}",
                    this.ZipCodeCorrected.Value ? "Zip Code Corrected, " : "",
                    this.CSCorrected.Value ? "City State Corrected, " : "",
                    this.NoZip_4Avail.Value ? "No Zip 4 Available, " : "",
                    this.InsufficientData.Value ? "Insufficient Data, " : "",
                    this.StreetCorrected.Value ? "Street Corrected, " : "",
                    $"{this.ErrorCode.Value}, ");
                    return str.Substring(0, str.Length > 1 ? str.Length - 2 : 0);
                }
                else
                {
                    return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}",
                        this.ZipCodeCorrected,
                        this.CSCorrected,
                        this.NotUsed1,
                        this.NotUsed2,
                        this.NoZip_4Avail,
                        this.NotUsed3,
                        this.InsufficientData,
                        this.StreetCorrected,
                        this.NotUsed4,
                        this.ErrorCode);
                }
            }
        }

        public class AEC
        {
            public NCOAString Filler1 { get; private set; }
            public NCOABool AECReturn { get; private set; }
            public NCOAString Filler2 { get; private set; }

            public AEC(string Entry)
            {
                try
                {
                    this.Filler1 = new NCOAString(3) { DataString = Entry.Substring(0) };
                    this.AECReturn = new NCOABool(1, "F") { DataString = Entry.Substring(3) };
                    this.Filler2 = new NCOAString(5) { DataString = Entry.Substring(4) };
                }
                catch
                {
                    if (this.Filler1 == null) this.Filler1 = new NCOAString(3);
                    if (this.AECReturn == null) this.AECReturn = new NCOABool(1, "F");
                    if (this.Filler2 == null) this.Filler2 = new NCOAString(5);
                }
            }

            public override string ToString()
            {
                return string.Format("{0}{1}{2}",
                    this.Filler1,
                    this.AECReturn,
                    this.Filler2);
            }
        }
    }
}
