using System.Xml.Serialization;
using CTeDLL.Classes.Servicos.Tipos;
using DFe.Classes.Assinatura;

namespace CTeDLL.Classes.Servicos.Inutilizacao
{
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class inutCTe
    {
        public inutCTe()
        {
            infInut = new infInutEnv();
        }

        /// <summary>
        ///     DP02 - Versão do leiaute
        /// </summary>
        [XmlAttribute]
        public versao versao { get; set; }

        /// <summary>
        ///     DP03 - Dados do Pedido
        ///     TAG a ser assinada
        /// </summary>
        public infInutEnv infInut { get; set; }

        /// <summary>
        ///     DP15 - Assinatura XML do grupo identificado pelo atributo “Id”
        /// </summary>
        [XmlElement(Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public Signature Signature { get; set; }
    }
}