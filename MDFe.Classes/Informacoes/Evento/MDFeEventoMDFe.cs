using System;
using System.Xml.Serialization;
using DFe.Classes.Assinatura;
using ManifestoDocumentoFiscalEletronico.Classes.Servicos.Flags;

namespace ManifestoDocumentoFiscalEletronico.Classes.Informacoes.Evento
{
    [Serializable]
    [XmlRoot(ElementName = "eventoMDFe", Namespace = "http://www.portalfiscal.inf.br/mdfe")]
    public class MDFeEventoMDFe
    {
        [XmlAttribute(AttributeName = "versao")]
        public VersaoServico Versao { get; set; }

        [XmlElement(ElementName = "infEvento")]
        public MDFeInfEvento InfEvento { get; set; }

        [XmlElement(ElementName = "Signature", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public Signature Signature { get; set; }
    }
}