using System;
using System.Xml.Serialization;
using DFe.Classes.Assinatura;

namespace MDFe.Classes
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/mdfe",
        ElementName = "MDFe")]
    public class MDFe
    {
        [XmlElement(ElementName = "infMDFe")]
        public MDFeInfMDFe InfMdFe { get; set; }

        public Signature Signature { get; set; }
    }
}