using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NFe.Classes.Servicos.DistribuicaoDFe.Schemas
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class retEvento
    {
        public retInfEvento infEvento { get; set; }

        [XmlAttribute()]
        public decimal versao { get; set; }
    }
}