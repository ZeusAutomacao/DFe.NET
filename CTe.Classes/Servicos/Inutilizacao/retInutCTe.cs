using System.Xml.Serialization;
using CTeDLL.Classes.Servicos.Tipos;
using DFe.Classes.Assinatura;
using DFe.Utils;

namespace CTeDLL.Classes.Servicos.Inutilizacao
{
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class retInutCTe : RetornoBase
    {
        /// <summary>
        ///     DR02 - Versão do leiaute
        /// </summary>
        [XmlAttribute]
        public versao versao { get; set; }

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

        public static retInutCTe LoadXml(string xml, inutCTe inutCTe)
        {
            var retorno = LoadXml(xml);
            retorno.EnvioXmlString = FuncoesXml.ClasseParaXmlString(inutCTe);

            return retorno;
        }

        private static retInutCTe LoadXml(string xml)
        {
            var retorno = FuncoesXml.XmlStringParaClasse<retInutCTe>(xml);
            retorno.RetornoXmlString = xml;

            return retorno;
        }
    }
}