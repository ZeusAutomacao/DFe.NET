using System;
using System.Xml.Serialization;
using DFe.Classes.Assinatura;

namespace ManifestoDocumentoFiscalEletronico.Classes
{
    [Serializable]
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/mdfe",
        ElementName = "MDFe")]
    public class MDFe
    {
        public MDFe()
        {
            InfMDFe = new MDFeInfMDFe();

        }

        [XmlElement(ElementName = "infMDFe")]
        public MDFeInfMDFe InfMDFe { get; set; }

        public Signature Signature { get; set; }
    }
}