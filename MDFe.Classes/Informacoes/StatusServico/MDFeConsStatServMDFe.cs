using System;
using System.Xml.Serialization;
using DFe.Classes.Flags;
using ManifestoDocumentoFiscalEletronico.Classes.Servicos.Flags;

namespace ManifestoDocumentoFiscalEletronico.Classes.Informacoes.StatusServico
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/mdfe",
        ElementName = "consStatServMDFe")]
    public class MDFeConsStatServMDFe
    {
        [XmlAttribute(AttributeName = "versao")]
        public VersaoServico Versao { get; set; }

        [XmlElement(ElementName = "TpAmb")]
        public TipoAmbiente TpAmb { get; set; }

        [XmlElement(ElementName = "xServ")]
        public string XServ { get; set; } = "STATUS";
    }
}