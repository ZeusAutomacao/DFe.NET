using System;
using System.Xml.Serialization;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using DFe.Utils;

namespace NFe.Classes.Servicos.Status
{
    [XmlRoot(Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class retConsStatServ : IRetornoServico
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

        /// <summary>
        ///     FR08 - Data e hora de recebimento
        /// </summary>
        [XmlIgnore]
        public DateTimeOffset dhRecbto { get; set; }

        [XmlElement(ElementName = "dhRecbto")]
        public string ProxyDhRecbto
        {
            get { return dhRecbto.ParaDataHoraStringUtc(); }
            set { dhRecbto = DateTimeOffset.Parse(value); }
        }

        /// <summary>
        ///     FR09 - Tempo médio de resposta do serviço
        /// </summary>
        public int? tMed { get; set; }

        /// <summary>
        ///     FR10 - Data e hora de retorno do Web Service
        /// </summary>
        [XmlIgnore]
        public DateTimeOffset? dhRetorno { get; set; }

        [XmlElement(ElementName = "dhRetorno")]
        public string ProxydhRetorno
        {
            get { return dhRetorno.ParaDataHoraStringUtc(); }
            set { dhRetorno = DateTimeOffset.Parse(value); }
        }

        /// <summary>
        ///     FR11 - Informações adicionais para o Contribuinte
        /// </summary>
#if NET5_0_OR_GREATER//o uso de tipos de referência anuláveis não é permitido até o C# 8.0.
        public string? xObs { get; set; }
#else
        public string xObs { get; set; }
#endif
    }
}