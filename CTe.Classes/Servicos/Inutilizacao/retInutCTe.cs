using System.Xml.Serialization;
using DFe.Classes.Assinatura;

namespace CTeDLL.Classes.Servicos.Inutilizacao
{
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class retInutCTe : RetornoBase
    {
        /// <summary>
        ///     DR02 - Versão do leiaute
        /// </summary>
        [XmlAttribute]
        public string versao { get; set; }

        /// <summary>
        ///     DR03 - Dados da resposta - TAG a ser assinada
        /// </summary>
        public infInutRet infInut { get; set; }

        /// <summary>
        ///     DR18 - Assinatura XML do grupo identificado pelo atributo “Id”
        ///     A decisão de assinar a mensagem fica a critério da UF interessada.
        /// </summary>
        [XmlElement(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public Signature Signature { get; set; }
    }
}