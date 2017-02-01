using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes;
using DFe.Classes.Assinatura;

namespace CTe.Classes
{
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class CTe
    {
        [XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte")]
        public infCte infCte;

        [XmlElement(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public Signature Signature;
    }
}