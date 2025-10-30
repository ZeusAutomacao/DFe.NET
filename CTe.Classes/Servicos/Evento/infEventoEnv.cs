using System;
using System.Xml.Serialization;
using CTe.Classes.Servicos.Evento.Flags;
using CTe.Classes.Servicos.Tipos;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using DFe.Utils;

namespace CTe.Classes.Servicos.Evento
{
    public class infEventoEnv
    {
        public infEventoEnv(ConfiguracaoServico configuracaoServico = null)
        {
            _configuracaoServico = configuracaoServico ?? ConfiguracaoServico.Instancia;
        }

        public infEventoEnv()
        {
        }

        [XmlIgnore]
        private ConfiguracaoServico _configuracaoServico;

        /// <summary>
        ///     HP07 - Grupo de informações do registro do Evento
        /// </summary>
        [XmlAttribute]
        public string Id { get; set; }

        /// <summary>
        ///     HP08 - Código do órgão de recepção do Evento.
        /// </summary>
        public Estado cOrgao { get; set; }

        /// <summary>
        ///     HP09 - Identificação do Ambiente: 1=Produção /2=Homologação
        /// </summary>
        public TipoAmbiente tpAmb { get; set; }

        /// <summary>
        ///     HP10 - CNPJ do autor do Evento
        /// </summary>
        public string CNPJ { get; set; }

        public string CPF { get; set; }

        /// <summary>
        ///     HP12 - Chave de Acesso da NF-e vinculada ao Evento
        /// </summary>
        public string chCTe { get; set; }

        /// <summary>
        ///     HP13 - Data e hora do evento no formato AAAA-MM-DDThh:mm:ssTZD (UTC - Universal Coordinated Time)
        /// </summary>
        [XmlIgnore]
        public DateTimeOffset dhEvento { get; set; }

        [XmlElement(ElementName = "dhEvento")]
        public string ProxydhEvento
        {
            get
            {
                if (_configuracaoServico == null)
                    _configuracaoServico = ConfiguracaoServico.Instancia;
                switch (_configuracaoServico.VersaoLayout)
                {
                    case versao.ve200:
                        return dhEvento.ParaDataHoraStringSemUtc();
                    case versao.ve400:
                    case versao.ve300:
                        return dhEvento.ParaDataHoraStringUtc();
                    default:
                        throw new InvalidOperationException("Versão Inválida para CT-e");
                }
            }

            set { dhEvento = DateTimeOffset.Parse(value); }
        }

        /// <summary>
        ///     HP14 - Código do evento
        /// </summary>
        public CTeTipoEvento tpEvento { get; set; }

        /// <summary>
        ///     HP15 - Sequencial do evento para o mesmo tipo de evento.
        /// </summary>
        public int nSeqEvento { get; set; }

        /// <summary>
        ///     HP17 - Informações do Pedido de Cancelamento
        /// </summary>
        public detEvento detEvento { get; set; }
    }
}