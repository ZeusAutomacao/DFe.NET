using System.Xml.Serialization;
using CTeDLL.Classes.Servicos.Tipos;

namespace CTeDLL.Classes.Servicos.Evento
{
    public class detEvento
    {
        [XmlAttribute]
        public versao versaoEvento { get; set; }

        [XmlElement("evCancCTe", typeof(evCancCTe), Namespace = "http://www.portalfiscal.inf.br/cte")]
        [XmlElement("evCCeCTe", typeof(evCCeCTe), Namespace = "http://www.portalfiscal.inf.br/cte")]
        public EventoContainer EventoContainer { get; set; }
    }
}