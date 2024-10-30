using System.Collections.Generic;
using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using NFe.Classes.Protocolo;

namespace NFe.Classes.Servicos.Consulta
{
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class retConsSitNFe : IRetornoServico
    {
        /// <summary>
        ///     ER02 - Versão do leiaute
        /// </summary>
        [XmlAttribute]
        public string versao { get; set; }

        /// <summary>
        ///     ER03 - Identificação do Ambiente: 1 – Produção / 2 – Homologação
        /// </summary>
        public TipoAmbiente tpAmb { get; set; }

        /// <summary>
        ///     Versão do Aplicativo que processou a consulta. A versão deve ser iniciada com a sigla da UF nos casos de WS próprio
        ///     ou a sigla SCAN, SVAN ou SVRS nos demais casos
        /// </summary>
        public string verAplic { get; set; }

        /// <summary>
        ///     ER05 - Código do status da resposta.
        /// </summary>
        public int cStat { get; set; }

        /// <summary>
        ///     ER06 - Descrição literal do status da resposta.
        /// </summary>
        public string xMotivo { get; set; }

        /// <summary>
        ///     ER07 - Código da UF que atendeu a solicitação
        /// </summary>
        public Estado cUF { get; set; }

        /// <summary>
        ///     EP07a - Chave de Acesso da NF-e consultada
        /// </summary>
        public string chNFe { get; set; }

        /// <summary>
        ///     ER08 - Protocolo de autorização ou denegação de uso da NF-e (vide item 4.2.2).
        ///     Informar se localizado uma NF-e com cStat = 100 (uso autorizado) ou 110 (uso denegado).
        /// </summary>
        public protNFe protNFe { get; set; }

        /// <summary>
        ///     ER10 - Informação do evento e respectivo Protocolo de registro de Evento
        /// </summary>
        [XmlElement("procEventoNFe")]
        public List<procEventoNFe> procEventoNFe { get; set; }
    }
}