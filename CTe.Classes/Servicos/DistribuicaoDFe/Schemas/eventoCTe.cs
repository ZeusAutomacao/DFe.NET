using DFe.Classes.Assinatura;
using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace CTe.Classes.Servicos.DistribuicaoDFe.Schemas
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class eventoCTe
    {
  
        [XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte", ElementName = "infEvento")]
        public eventoInfEvento infEvento { get; set; }

        [XmlAttribute()]
        public decimal versao { get; set; }

        /// <summary>
        ///     HP22 - Assinatura Digital do documento XML, a assinatura deverá ser aplicada no elemento infEvento
        /// </summary>
        [XmlElement(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public Signature Signature { get; set; }

        
    }
}