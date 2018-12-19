using System;
using System.Xml.Serialization;

namespace SMDFe.Classes.Flags
{
    [Serializable]
    public enum MDFeRespSeg
    {
        [XmlEnum("1")]
        EmitenteDoMDFe = 1,
        [XmlEnum("2")]
        ResponsavelPelaContratacao = 2
    }
}