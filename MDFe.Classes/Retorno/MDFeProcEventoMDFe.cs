using System;
using System.Xml.Serialization;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes.Evento;
using ManifestoDocumentoFiscalEletronico.Classes.Retorno.MDFeEvento;

namespace ManifestoDocumentoFiscalEletronico.Classes.Retorno
{
    [Serializable]
    [XmlRoot(ElementName = "procEventoMDFe", Namespace = "http://www.portalfiscal.inf.br/mdfe")]
    public class MDFeProcEventoMDFe
    {
        [XmlAttribute(AttributeName = "versao")]
        public string Versao { get; set; }

        [XmlElement(ElementName = "eventoMDFe")]
        public MDFeEventoMDFe EventoMDFe { get; set; }

        [XmlElement(ElementName = "retEventoMDFe")]
        public MDFeRetEventoMDFe RetEventoMDFe { get; set; }
    }
}