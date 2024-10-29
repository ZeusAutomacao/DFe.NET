using System.Xml.Serialization;

namespace NFe.Classes.Servicos.Inutilizacao
{
    /// <summary>
    /// Pedido de inutilização de numeração de NF-e processado
    /// </summary>
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class procInutNFe
    {
        /// <summary>
        /// Versão do pedido de inutilização
        /// </summary>
        [XmlAttribute]
        public string versao { get; set; }

        /// <summary>
        /// Mensagem de solicitação de inutilização de numeração de NFe. 
        /// </summary>
        public inutNFe inutNFe { get; set; }

        /// <summary>
        /// Mensagem de retorno da solicitação de inutilização de numeração de NF-e.
        /// </summary>
        public retInutNFe retInutNFe { get; set; }
    }
}