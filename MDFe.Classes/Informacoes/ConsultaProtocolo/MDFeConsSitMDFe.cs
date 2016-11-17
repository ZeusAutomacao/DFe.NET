using System;
using System.Xml.Serialization;
using DFe.Classes.Flags;

namespace ManifestoDocumentoFiscalEletronico.Classes.Informacoes.ConsultaProtocolo
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/mdfe",
        ElementName = "consSitMDFe")]
    public class MDFeConsSitMDFe
    {
        [XmlAttribute(AttributeName = "versao")]
        public string Versao { get; set; }

        [XmlElement(ElementName = "tpAmb")]
        public TipoAmbiente TpAmb { get; set; }

        [XmlElement(ElementName = "xServ")]
        public string XServ { get; set; } = "CONSULTAR";

        [XmlElement(ElementName = "chMDFe")]
        public string ChMDFe { get; set; }
    }
}