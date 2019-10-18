using System.Xml.Serialization;
using CTe.Classes.Informacoes;
using DFe.Classes.Assinatura;
using DFe.Classes.Flags;
using CTe.CTeOSDocumento.CTe.CTeOS.Informacoes;
using DFe.Utils;

namespace CTe.CTeOSClasses
{
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class CTeOS
    {
        public CTeOS()
        {
            versao = VersaoServico.Versao300;
        }

        [XmlAttribute]
        public VersaoServico versao { get; set; }

        [XmlElement(ElementName = "infCte", Namespace = "http://www.portalfiscal.inf.br/cte")]
        public infCteOS InfCte { get; set; }

        [XmlElement(ElementName = "infCTeSupl")]
        public infCTeSupl infCTeSupl { get; set; }

        [XmlElement(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public Signature Signature { get; set; }

        public static CTeOS LoadXmlString(string xml)
        {
            return FuncoesXml.XmlStringParaClasse<CTeOS>(xml);
        }

        public static CTeOS LoadXmlArquivo(string caminhoArquivoXml)
        {
            return FuncoesXml.ArquivoXmlParaClasse<CTeOS>(caminhoArquivoXml);
        }
    }
}