using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace NFe.Classes.Servicos.DistribuicaoDFe.Schemas
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/nfe", IsNullable = false)]
    public class protNFe
    {
        public infProt infProt { get; set; }

        [XmlAttribute()]
        public decimal versao { get; set; }
    }
}