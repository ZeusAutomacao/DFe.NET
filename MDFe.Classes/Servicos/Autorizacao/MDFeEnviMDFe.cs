using System;
using System.Xml.Serialization;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes;
using ManifestoDocumentoFiscalEletronico.Classes.Servicos.Flags;

namespace ManifestoDocumentoFiscalEletronico.Classes.Servicos.Autorizacao
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/mdfe",
        ElementName = "enviMDFe")]
    public class MDFeEnviMDFe
    {
        [XmlAttribute(AttributeName = "versao")]
        public MDFeVersaoServico Versao { get; set; } = MDFeVersaoServico.Versao100;

        [XmlElement(ElementName = "idLote")]
        public string IdLote { get; set; }

        [XmlElement(ElementName = "MDFe")]
        public MDFe MDFe { get; set; }
    }
}