using MDFe.Classes.Informacoes.Evento.CorpoEvento;
using MDFe.Utils.Flags;
using System;
using System.Xml.Serialization;

namespace MDFe.Classes.Informacoes.Evento
{
    [Serializable]
    public class MDFeDetEvento
    {
        public MDFeDetEvento()
        {
            VersaoServico = VersaoServico.Versao100;
        }

        [XmlAttribute(AttributeName = "versaoEvento")]
        public VersaoServico VersaoServico { get; set; }

        [XmlElement("evCancMDFe", typeof(MDFeEvCancMDFe), Namespace = "http://www.portalfiscal.inf.br/mdfe")]
        [XmlElement("evEncMDFe", typeof(MDFeEvEncMDFe), Namespace = "http://www.portalfiscal.inf.br/mdfe")]
        [XmlElement("evIncCondutorMDFe", typeof(MDFeEvIncCondutorMDFe), Namespace = "http://www.portalfiscal.inf.br/mdfe")]
        [XmlElement("evIncDFeMDFe", typeof(MDFeEvIncDFeMDFe), Namespace = "http://www.portalfiscal.inf.br/mdfe")]
        [XmlElement("evPagtoOperMDFe", typeof(evPagtoOperMDFe), Namespace = "http://www.portalfiscal.inf.br/mdfe")]
        public MDFeEventoContainer EventoContainer { get; set; }
    }
}