using System;
using System.Xml.Serialization;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes;

namespace ManifestoDocumentoFiscalEletronico.Classes.Servicos.Autorizacao
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/mdfe",
        ElementName = "enviMDFe")]
    public class MDFeEnviMDFe
    {
        [XmlAttribute(AttributeName = "versao")]
        public VersaoLayout Versao { get; set; } = VersaoLayout.Versao100;

        [XmlElement(ElementName = "idLote")]
        public string IdLote { get; set; }

        [XmlElement(ElementName = "MDFe")]
        public MDFe MDFe { get; set; }
    }
}