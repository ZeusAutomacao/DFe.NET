using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using DFe.Classes.Flags;
using NFe.Classes.Informacoes.Identificacao.Tipos;

namespace NFe.Classes.Servicos.AdmCsc
{
    /// <summary>
    /// AR01 - Estrutura com os dados de retorno para administrar o CSC
    /// </summary>
    [Serializable()]
    [XmlType(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    [XmlRoot("retAdmCscNFCe",Namespace = "http://www.portalfiscal.inf.br/nfe", IsNullable = false)]
    public class retAdmCscNFCe : IRetornoServico
    {
        /// <summary>
        /// AR02 - Versão do leiaute
        /// </summary>
        [XmlAttribute]
        public string versao { get; set; }

        /// <summary>
        /// AR03 - Identificação do Ambiente: 1=Produção/2=Homologação 
        /// </summary>
        public TipoAmbiente tpAmb { get; set; }

        /// <summary>
        /// AR04 - Identificador do tipo de operação: 1 - Consulta CSC Ativos / 2 - Solicita novo CSC / 3 - Revoga CSC Ativo
        /// </summary>
        public IdentificadorOperacaoCsc indOp { get; set; }

        /// <summary>
        /// AR05 - Código do resultado do processamento da solicitação
        /// </summary>
        public int cStat { get; set; }

        /// <summary>
        /// AR06 - Descrição literal do resultado do processamento da solicitação
        /// </summary>
        public string xMotivo { get; set; }

        /// <summary>
        /// AR07 - Tag de grupo para retorno dos dados de até dois CSC
        /// </summary>
        [XmlElement("dadosCsc", Namespace = "http://www.portalfiscal.inf.br/nfe")]
        public List<dadosCsc> dadosCsc { get; set; }
    }
}