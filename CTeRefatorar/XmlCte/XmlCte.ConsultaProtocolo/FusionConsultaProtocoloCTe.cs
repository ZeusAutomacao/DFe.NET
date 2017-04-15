using System;
using System.Xml.Serialization;

namespace FusionCore.DFe.XmlCte.XmlCte.ConsultaProtocolo
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte",
        ElementName = "consSitCTe")]
    public class FusionConsultaProtocoloCTe
    {
        [XmlAttribute(AttributeName = "versao")]
        public string Versao { get; set; }

        [XmlElement(ElementName = "tpAmb")]
        public FusionTipoAmbienteCTe Ambiente { get; set; }

        [XmlElement(ElementName = "xServ")]
        public string ServicoSolicitado { get; set; } = "CONSULTAR";

        [XmlElement(ElementName = "chCTe")]
        public string Chave { get; set; }
    }
}