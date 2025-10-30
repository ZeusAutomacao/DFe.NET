using System;
using System.Xml.Serialization;

namespace MDFe.Classes.Flags
{
    [Serializable]
    public enum MDFeCUnid
    {
        [XmlEnum("01")]
        KG = 01,
        [XmlEnum("02")]
        TON = 02
    }
}