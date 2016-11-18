using System;
using System.Xml.Serialization;
using DFe.Classes.Flags;
using ManifestoDocumentoFiscalEletronico.Classes.Servicos.Flags;

namespace ManifestoDocumentoFiscalEletronico.Classes.Informacoes.ConsultaNaoEncerrados
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/mdfe",
        ElementName = "consMDFeNaoEnc")]
    public class MDFeCosMDFeNaoEnc
    {
        [XmlAttribute(AttributeName = "versao")]
        public VersaoServico Versao { get; set; }

        [XmlElement(ElementName = "tpAmb")]
        public TipoAmbiente TpAmb { get; set; }

        [XmlElement(ElementName = "xServ")]
        public string XServ { get; set; } = "CONSULTAR NÃO ENCERRADOS";

        [XmlElement(ElementName = "CNPJ")]
        public string CNPJ { get; set; }
    }
}