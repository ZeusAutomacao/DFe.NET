using System.Xml.Serialization;
using CTe.Classes.Servicos.Tipos;

namespace CTe.Classes.Servicos.Evento
{
    public class detEvento
    {
        [XmlAttribute]
        public versao versaoEvento { get; set; }

        [XmlElement("evCancCTe", typeof(evCancCTe), Namespace = "http://www.portalfiscal.inf.br/cte")]
        [XmlElement("evCCeCTe", typeof(evCCeCTe), Namespace = "http://www.portalfiscal.inf.br/cte")]
        [XmlElement("evPrestDesacordo", typeof(evPrestDesacordo), Namespace = "http://www.portalfiscal.inf.br/cte")]
        public EventoContainer EventoContainer { get; set; }
    }
}