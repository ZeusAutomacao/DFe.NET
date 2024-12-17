using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace NFe.Danfe.PdfClown.Structs
{
    // Source: https://stackoverflow.com/questions/3377036/how-can-i-xml-serialize-a-datetimeoffset-property
    /// <remarks>
    /// The default value is <c>DateTimeOffset.MinValue</c>. This is a value
    /// type and has the same hash code as <c>DateTimeOffset</c>! Implicit
    /// assignment from <c>DateTime</c> is neither implemented nor desirable!
    /// </remarks>
    public struct DateTimeOffsetIso8601 : IXmlSerializable
    {
        public DateTimeOffset DateTimeOffsetValue { private set; get; }

        public DateTimeOffsetIso8601(DateTimeOffset value)
        {
            this.DateTimeOffsetValue = value;
        }

        public static implicit operator DateTimeOffsetIso8601(DateTimeOffset value)
        {
            return new DateTimeOffsetIso8601(value);
        }

        public static implicit operator DateTimeOffset(DateTimeOffsetIso8601 instance)
        {
            return instance.DateTimeOffsetValue;
        }

        public static bool operator ==(DateTimeOffsetIso8601 a, DateTimeOffsetIso8601 b)
        {
            return a.DateTimeOffsetValue == b.DateTimeOffsetValue;
        }

        public static bool operator !=(DateTimeOffsetIso8601 a, DateTimeOffsetIso8601 b)
        {
            return a.DateTimeOffsetValue != b.DateTimeOffsetValue;
        }

        public static bool operator <(DateTimeOffsetIso8601 a, DateTimeOffsetIso8601 b)
        {
            return a.DateTimeOffsetValue < b.DateTimeOffsetValue;
        }

        public static bool operator >(DateTimeOffsetIso8601 a, DateTimeOffsetIso8601 b)
        {
            return a.DateTimeOffsetValue > b.DateTimeOffsetValue;
        }

        public override bool Equals(object o)
        {
            if (o is DateTimeOffsetIso8601)
                return DateTimeOffsetValue.Equals(((DateTimeOffsetIso8601)o).DateTimeOffsetValue);
            else if (o is DateTimeOffset)
                return DateTimeOffsetValue.Equals((DateTimeOffset)o);
            else
                return false;
        }

        public override int GetHashCode()
        {
            return DateTimeOffsetValue.GetHashCode();
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            var text = reader.ReadElementString();

            if (!string.IsNullOrWhiteSpace(text))
                DateTimeOffsetValue = XmlConvert.ToDateTimeOffset(text);
        }

        public override string ToString()
        {
            return DateTimeOffsetValue.ToString(format: "o");
        }

        public string ToString(string format)
        {
            return DateTimeOffsetValue.ToString(format);
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteString(DateTimeOffsetValue.ToString(format: "o"));
        }
    }
}
