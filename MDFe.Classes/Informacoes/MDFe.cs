using System;
using System.Xml.Serialization;
using DFe.Classes.Assinatura;
using DFe.Utils;

namespace MDFe.Classes.Informacoes
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

        [XmlElement(ElementName = "infMDFeSupl")]
        public infMDFeSupl infMDFeSupl { get; set; }

        [XmlElement(ElementName = "Signature", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public Signature Signature { get; set; }

        public static MDFe LoadXmlString(string xml)
        {
            return FuncoesXml.XmlStringParaClasse<MDFe>(xml);
        }

        public static MDFe LoadXmlArquivo(string caminhoArquivoXml)
        {
            return FuncoesXml.ArquivoXmlParaClasse<MDFe>(caminhoArquivoXml);
        }
    }
}