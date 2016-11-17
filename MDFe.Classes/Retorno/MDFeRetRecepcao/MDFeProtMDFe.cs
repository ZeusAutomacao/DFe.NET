using System;
using System.Xml.Serialization;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes;

namespace ManifestoDocumentoFiscalEletronico.Classes.Retorno.MDFeRetRecepcao
{
    [Serializable]
    [XmlRoot(ElementName = "protMDFe", Namespace = "http://www.portalfiscal.inf.br/mdfe")]
    public class MDFeProtMDFe
    {
        [XmlAttribute(AttributeName = "versao")]
        public string Versao { get; set; }

        [XmlElement(ElementName = "infProt")]
        public MDFeInfProtMDFe InfProt { get; set; }
    }
}