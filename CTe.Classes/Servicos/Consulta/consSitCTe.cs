using System.Xml.Serialization;
using CTeDLL.Classes.Informacoes.Identificacao.Tipos;
using DFe.Classes.Flags;

namespace CTeDLL.Classes.Servicos.Consulta
{
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte")]
    public class consSitCTe
    {
        public consSitCTe()
        {
            xServ = "CONSULTAR";
        }

        /// <summary>
        ///     EP02 - Versão do leiaute
        /// </summary>
        [XmlAttribute]
        public string versao { get; set; }

        /// <summary>
        ///     EP03 - Identificação do Ambiente: 1 – Produção / 2 - Homologação
        /// </summary>
        public TipoAmbiente tpAmb { get; set; }

        /// <summary>
        ///     Serviço solicitado "CONSULTAR"
        /// </summary>
        public string xServ { get; set; }

        /// <summary>
        ///     EP05 - Chave de Acesso da NF-e.
        /// </summary>
        public string chCTe { get; set; }
    }
}