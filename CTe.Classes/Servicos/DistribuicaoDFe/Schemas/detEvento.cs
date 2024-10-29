using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace CTe.Classes.Servicos.DistribuicaoDFe.Schemas
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class detEvento
    {
      
        [XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte")]
        public evCTeAutorizadoMDFe evCTeAutorizadoMDFe { get; set; }
               

        [XmlAttribute()]
        public decimal versao { get; set; }
    }
}