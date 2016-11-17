using System;
using System.Xml.Serialization;
using ManifestoDocumentoFiscalEletronico.Classes.Informacoes.Evento.CorpoEvento;
using ManifestoDocumentoFiscalEletronico.Classes.Servicos.Flags;

namespace ManifestoDocumentoFiscalEletronico.Classes.Informacoes.Evento
{
    [Serializable]
    public class MDFeDetEvento
    {
        [XmlAttribute(AttributeName = "versaoEvento")]
        public VersaoServico VersaoServico { get; set; }

        [XmlElement("evCancMDFe", typeof(MDFeEvCancMDFe), Namespace = "http://www.portalfiscal.inf.br/mdfe")]
        [XmlElement("evEncMDFe", typeof(MDFeEvEncMDFe), Namespace = "http://www.portalfiscal.inf.br/mdfe")]
        [XmlElement("evIncCondutorMDFe", typeof(MDFeEvIncCondutorMDFe), Namespace = "http://www.portalfiscal.inf.br/mdfe")]
        public MDFeEventoContainer EventoContainer { get; set; }
    }
}