using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using DFe.Classes.Flags;

namespace NFe.Classes.Servicos.Download
{
    /// <summary>
    /// JR01 - Estrutura com as NF-e encontradas 
    /// </summary>
    [Serializable()]
    [XmlType(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    [XmlRoot("retDownloadNFe", Namespace = "http://www.portalfiscal.inf.br/nfe", IsNullable = false)]
    public class retDownloadNFe : IRetornoServico
    {
        public retDownloadNFe()
        {
            retNFe = new List<retNFe>();
        }
        
        /// <summary>
        ///     JR02 - Versão do leiaute
        /// </summary>
        [XmlAttribute]
        public string versao { get; set; }

        /// <summary>
        /// JR03 - Identificação do Ambiente: 1=Produção/2=Homologação
        /// </summary>
        public TipoAmbiente tpAmb { get; set; }

        /// <summary>
        /// JR04 - Versão do Aplicativo que processou a consulta. JR05
        /// </summary>
        public string verAplic { get; set; }

        /// <summary>
        /// JR05 - Código do status da resposta
        /// </summary>
        public int cStat { get; set; }

        /// <summary>
        /// JR06 - Descrição literal do status da resposta
        /// </summary>
        public string xMotivo { get; set; }

        /// <summary>
        /// JR07 - Data e Hora da mensagem de resposta
        /// </summary>
        public DateTime dhResp { get; set; }

        /// <summary>
        /// JR08 - Conjunto de informações da NF-e
        /// </summary>
        [XmlElement("retNFe")]
        public List<retNFe> retNFe { get; set; }
    }

}