using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NFe.Classes.Servicos.DistribuicaoDFe.Schemas
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/nfe", IsNullable = false)]
    public class procEventoNFe
    {
        [XmlAttribute()]
        public decimal versao { get; set; }

        [XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public evento evento { get; set; }

        public retEvento retEvento { get; set; }
    }
}