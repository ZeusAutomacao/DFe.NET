using System.Xml.Serialization;
using DFe.Classes.Flags;
using NFe.Classes.Informacoes.Identificacao.Tipos;

namespace NFe.Classes.Servicos.AdmCsc
{
    /// <summary>
    /// AP01 - Estrutura com os dados para administrar o CSC
    /// </summary>
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class admCscNFCe
    {
        /// <summary>
        ///     AP02 - Versão do leiaute
        /// </summary>
        [XmlAttribute]
        public string versao { get; set; }

        /// <summary>
        /// AP03 - Identificação do Ambiente: 1=Produção/2=Homologação 
        /// </summary>
        public TipoAmbiente tpAmb { get; set; }

        /// <summary>
        /// AP04 - Identificador do tipo de operação: 1 - Consulta CSC Ativos / 2 - Solicita novo CSC / 3 - Revoga CSC Ativo
        /// </summary>
        public IdentificadorOperacaoCsc indOp { get; set; }

        /// <summary>
        /// AP05 - Raiz do CNPJ do contribuinte
        /// </summary>
        public string raizCNPJ { get; set; }

        /// <summary>
        /// AP06 - Dados do CSC a ser revogado
        /// </summary>
        [XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public dadosCsc dadosCsc { get; set; }
    }
}