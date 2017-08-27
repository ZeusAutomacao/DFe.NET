using System.Net;
using DFe.Configuracao;
using DFe.DocumentosEletronicos.Flags;
using DFe.DocumentosEletronicos.NFe.Classes.Informacoes.Identificacao.Tipos;
using DFe.DocumentosEletronicos.NFe.Flags;

namespace DFe.DocumentosEletronicos.NFe.Configuracao
{
    public abstract class NFeBaseConfig : DFeConfig
    {
        public abstract TipoEmissao TipoEmissao { get; set; }
        public abstract ModeloDocumento ModeloDocumento { get; set; }
        public abstract ServicoNFe ServicoNFe { get; set; }

        /// <summary>
        ///     Versão do serviço RecepcaoEvento para Carta de Correção e Cancelamento
        /// </summary>
        public abstract VersaoServico VersaoRecepcaoEventoCceCancelamento { get; set; }

        /// <summary>
        ///     Versão do serviço RecepcaoEvento para EPEC
        /// </summary>
        public abstract VersaoServico VersaoRecepcaoEventoEpec { get; set; }

        /// <summary>
        ///     Versão do serviço RecepcaoEvento para Manifestação do destinatário
        /// </summary>
        public VersaoServico VersaoRecepcaoEventoManifestacaoDestinatario { get; set; }

        /// <summary>
        ///     Versão do serviço NfeRecepcao
        /// </summary>
        public abstract VersaoServico VersaoNfeRecepcao { get; set; }

        /// <summary>
        ///     Versão do serviço NfeRetRecepcao
        /// </summary>
        public abstract VersaoServico VersaoNfeRetRecepcao { get; set; }

        /// <summary>
        ///     Versão do serviço NfeConsultaCadastro
        /// </summary>
        public abstract VersaoServico VersaoNfeConsultaCadastro { get; set; }

        /// <summary>
        ///     Versão do serviço NfeInutilizacao
        /// </summary>
        public abstract VersaoServico VersaoNfeInutilizacao { get; set; }

        /// <summary>
        ///     Versão do serviço NfeConsultaProtocolo
        /// </summary>
        public abstract VersaoServico VersaoNfeConsultaProtocolo { get; set; }

        /// <summary>
        ///     Versão do serviço NfeStatusServico
        /// </summary>
        public abstract VersaoServico VersaoNfeStatusServico { get; set; }

        /// <summary>
        ///     Versão do serviço NFeAutorizacao
        /// </summary>
        public abstract VersaoServico VersaoNFeAutorizacao { get; set; }

        /// <summary>
        ///     Versão do serviço NFeRetAutorizacao
        /// </summary>
        public abstract VersaoServico VersaoNFeRetAutorizacao { get; set; }

        /// <summary>
        ///     Versão do serviço NFeDistribuicaoDFe
        /// </summary>
        public abstract VersaoServico VersaoNFeDistribuicaoDFe { get; set; }

        /// <summary>
        ///     Versão do serviço NfeConsultaDest
        /// </summary>
        public abstract VersaoServico VersaoNfeConsultaDest { get; set; }

        /// <summary>
        ///     Versão do serviço NfeDownloadNF
        /// </summary>
        public abstract VersaoServico VersaoNfeDownloadNF { get; set; }

        /// <summary>
        ///     Versão do serviço admCscNFCe
        /// </summary>
        public abstract VersaoServico VersaoNfceAministracaoCSC { get; set; }
    }
}