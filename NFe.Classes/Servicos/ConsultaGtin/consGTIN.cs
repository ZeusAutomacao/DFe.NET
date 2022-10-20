using System.Xml.Serialization;

namespace NFe.Classes.Servicos.ConsultaGtin
{
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class consGTIN
    {
        [XmlAttribute]
        public string versao { get; set; }

        public string GTIN { get; set; }
    }
}