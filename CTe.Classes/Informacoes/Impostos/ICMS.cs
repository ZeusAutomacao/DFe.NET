using System.Xml.Serialization;
using CTe.Classes.Informacoes.Impostos.ICMS;
using CTe.Classes.Informacoes.Impostos.ICMS.Tipos;

namespace CTe.Classes.Informacoes.Impostos.Tributacao
{
    public class ICMS
    {
        [XmlElement("ICMS00", typeof(ICMS00))]
        [XmlElement("ICMS20", typeof(ICMS20))]
        [XmlElement("ICMS45", typeof(ICMS45))]
        [XmlElement("ICMS60", typeof(ICMS60))]
        [XmlElement("ICMS90", typeof(ICMS90))]
        [XmlElement("ICMSOutraUF", typeof(ICMSOutraUF))]
        [XmlElement("ICMSSN", typeof(ICMSSN))]
        public ICMSBasico TipoICMS { get; set; }
    }
}