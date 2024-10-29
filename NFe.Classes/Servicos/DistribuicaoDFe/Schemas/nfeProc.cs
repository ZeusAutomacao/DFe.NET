using System;
using System.ComponentModel;
using System.Xml.Serialization;
using NFe.Classes.Informacoes;

namespace NFe.Classes.Servicos.DistribuicaoDFe.Schemas
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/nfe", IsNullable = false)]
    public partial class nfeProc
    {
        [XmlAttribute()]
        public decimal versao { get; set; }

        public infNFe NFe { get; set; }

        public protNFe protNFe { get; set; }

    }

    
    
}