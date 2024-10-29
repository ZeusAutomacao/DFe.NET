using System;
using System.Xml.Serialization;
using MDFe.Classes.Informacoes.Evento;
using MDFe.Classes.Retorno.MDFeEvento;

namespace MDFe.Classes.Retorno
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