using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace CTe.Classes.Servicos.DistribuicaoDFe
{
    [Serializable()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/cte")]
    [XmlRoot("retDistDFeInt", Namespace = "http://www.portalfiscal.inf.br/cte", IsNullable = false)]
    public class retDistDFeInt
    {
        /// <summary>
        /// B02 - Versão do leiaute
        /// </summary>
        [XmlAttribute()]
        public decimal versao { get; set; }

        /// <summary>
        /// B03 - Identificação do Ambiente: 1=Produção /2=Homologação
        /// </summary>
        public byte tpAmb { get; set; }

        /// <summary>
        ///  B04 - Versão do aplicativo que processou a consulta
        /// </summary>
        public string verAplic { get; set; }

        /// <summary>
        /// B05 - Código do status da resposta (vide item 5)
        /// </summary>
        public int cStat { get; set; }

        /// <summary>
        /// B06 - Descrição literal do status da resposta
        /// </summary>
        public string xMotivo { get; set; }

        /// <summary>
        /// B07 - Data e hora da mensagem de Resposta
        /// </summary>
        public DateTime dhResp { get; set; }

        /// <summary>
        /// B08 - Último NSU pesquisado no Ambiente Nacional. Se for o caso, o solicitante pode continuar a consulta a partir 
        /// deste NSU para obter novos resultados.
        /// </summary>
        public long ultNSU { get; set; }

        /// <summary>
        /// B09 - Maior NSU existente no Ambiente Nacional para o CNPJ/CPF informado
        /// </summary>
        public long maxNSU { get; set; }

        [XmlArrayItem("docZip", IsNullable = false)]
        public loteDistDFeInt[] loteDistDFeInt { get; set; }
    }
}