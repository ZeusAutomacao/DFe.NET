using System.Runtime.InteropServices;
using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;

namespace CTeDLL.Classes.Servicos.Status
{
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/cte")]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("CTeDLL.retConsStatServ")]
    [ComVisible(true)]
    public class retConsStatServCte : IRetornoServico
    {
        /// <summary>
        ///     FR02 - Versão do leiaute
        /// </summary>
        [XmlAttribute]
        public string versao { get; set; }

        /// <summary>
        ///     FR03 - Identificação do Ambiente: 1 – Produção / 2 - Homologação
        /// </summary>
        public TipoAmbiente tpAmb { get; set; }

        /// <summary>
        ///     FR04 - Versão do Aplicativo que processou a consulta. A versão deve ser iniciada com a sigla da UF nos casos de WS
        ///     próprio ou a sigla SCAN, SVAN ou SVRS nos demais casos.
        /// </summary>
        public string verAplic { get; set; }

        /// <summary>
        ///     FR05 - Código do status da resposta.
        /// </summary>
        public int cStat { get; set; }

        /// <summary>
        ///     FR06 - Descrição literal do status da resposta.
        /// </summary>
        public string xMotivo { get; set; }

        /// <summary>
        ///     FR07 - Código da UF que atendeu a solicitação
        /// </summary>
        public Estado cUF { get; set; }
    }
}