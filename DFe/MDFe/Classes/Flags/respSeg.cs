using System;
using System.Xml.Serialization;

namespace DFe.MDFe.Classes.Flags
{
    [Serializable]
    public enum respSeg
    {
        [XmlEnum("1")]
        EmitenteDoMDFe = 1,
        [XmlEnum("2")]
        ResponsavelPelaContratacao = 2
    }
}