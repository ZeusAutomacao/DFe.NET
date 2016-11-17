using System;
using System.Xml.Serialization;
using DFe.Classes.Assinatura;

namespace ManifestoDocumentoFiscalEletronico.Classes.Retorno.MDFeEvento
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/mdfe",
        ElementName = "retEvento")]
    public class MDFeRetEventoMDFe
    {
        [XmlAttribute(AttributeName = "versao")]
        public string Versao { get; set; }

        [XmlElement(ElementName = "infEvento")]
        public MDFeRetInfEvento InfEvento { get; set; }

        [XmlElement(ElementName = "Signature")]
        public Signature Signature { get; set; }
    }
}