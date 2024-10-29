using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NFe.Classes.Servicos.DistribuicaoDFe.Schemas
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class evento
    {
        [XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe", ElementName = "infEvento")]
        public eventoInfEvento infEvento { get; set; }

        [XmlAttribute()]
        public decimal versao { get; set; }
    }
}