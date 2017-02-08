using System;
using System.IO;
using CTeDLL.Classes.Informacoes.Identificacao.Tipos;
using CTeDLL.Classes.Servicos.Tipos;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using DFe.Utils;

namespace CTeDLL
{
    public sealed class ConfiguracaoServico
    {
        private static volatile ConfiguracaoServico _instancia;
        private static readonly object SyncRoot = new object();
        private string _diretorioSchemas;
        private bool _salvarXmlServicos;
        private bool _salvarXmlOrganizados;

        private ConfiguracaoServico()
        {
            Certificado = new ConfiguracaoCertificado();
        }

        /// <summary>
        ///     Configurações relativas ao Certificado Digital
        /// </summary>
        public ConfiguracaoCertificado Certificado { get; set; }

        /// <summary>
        ///     Tempo máximo de espera pela resposta do webservice, em milisegundos
        /// </summary>
        public int TimeOut { get; set; }

        /// <summary>
        ///     Estado de destino do webservice
        /// </summary>
        public Estado cUF { get; set; }

        /// <summary>
        ///     Versão do Layout do Serviço
        /// </summary>
        public string VersaoServico { get; set; }

        /// <summary>
        ///     Tipo de ambiente do webservice (Produção, Homologação)
        /// </summary>
        public TipoAmbiente tpAmb { get; set; }

        /// <summary>
        ///     Tipo de Emissão da NF-e
        /// </summary>
        public TipoEmissao tpEmis { get; set; }

        /// <summary>
        ///     Tipo de documento que está sendo referenciado nos webservices
        /// </summary>
        public ModeloDocumento ModeloDocumento { get; set; }

        /// <summary>
        ///     Versão do serviço RecepcaoEvento
        /// </summary>
        public VersaoServico VersaoRecepcaoEvento { get; set; }

        /// <summary>
        ///     Versão do serviço RecepcaoEvento para Carta de Correção e Cancelamento
        /// </summary>
        public VersaoServico VersaoRecepcaoEventoCceCancelamento { get; set; }

        /// <summary>
        ///     Versão do serviço RecepcaoEvento para EPEC
        /// </summary>
        public VersaoServico VersaoRecepcaoEventoEpec { get; set; }

        /// <summary>
        ///     Versão do serviço RecepcaoEvento para Manifestação do destinatário
        /// </summary>
        public VersaoServico VersaoRecepcaoEventoManifestacaoDestinatario { get; set; }

        /// <summary>
        ///     Versão do serviço NfeRecepcao
        /// </summary>
        public VersaoServico VersaoCteRecepcao { get; set; }

        /// <summary>
        ///     Versão do serviço NfeRetRecepcao
        /// </summary>
        public VersaoServico VersaoCteRetRecepcao { get; set; }

        /// <summary>
        ///     Versão do serviço NfeConsultaCadastro
        /// </summary>
        public VersaoServico VersaoCteConsultaCadastro { get; set; }

        /// <summary>
        ///     Versão do serviço NfeInutilizacao
        /// </summary>
        public VersaoServico VersaoCteInutilizacao { get; set; }

        /// <summary>
        ///     Versão do serviço NfeConsultaProtocolo
        /// </summary>
        public VersaoServico VersaoCteConsultaProtocolo { get; set; }

        /// <summary>
        ///     Versão do serviço NfeStatusServico
        /// </summary>
        public VersaoServico VersaoCteStatusServico { get; set; }

        /// <summary>
        ///     Versão do serviço NFeAutorizacao
        /// </summary>
        public VersaoServico VersaoCteAutorizacao { get; set; }

        /// <summary>
        ///     Versão do serviço NFeRetAutorizacao
        /// </summary>
        public VersaoServico VersaoCteRetAutorizacao { get; set; }

        /// <summary>
        ///     Versão do serviço NFeDistribuicaoDFe
        /// </summary>
        public VersaoServico VersaoCteDistribuicaoDFe { get; set; }

        /// <summary>
        ///     Versão do serviço NfeConsultaDest
        /// </summary>
        public VersaoServico VersaoCteConsultaDest { get; set; }

        /// <summary>
        ///     Versão do serviço NfeDownloadNF
        /// </summary>
        public VersaoServico VersaoCteDownloadNF { get; set; }

        /// <summary>
        ///     Diretório onde estão aramazenados os schemas para validação
        /// </summary>
        public string DiretorioSchemas
        {
            get { return _diretorioSchemas; }
            set
            {
                if (!string.IsNullOrEmpty(value) && !Directory.Exists(value))
                    throw new Exception("Diretório " + value + " não encontrado!");
                _diretorioSchemas = value;
            }
        }

        /// <summary>
        ///     Informar se a biblioteca deve salvar o xml de envio e de retorno
        /// </summary>
        public bool SalvarXmlServicos
        {
            get { return _salvarXmlServicos; }
            set
            {
                _salvarXmlServicos = value;
            }
        }

        /// <summary>
        ///     Diretório onde os xmls de envio/retorno devem ser salvos
        /// </summary>
        public string DiretorioSalvarXml { get; set; }

        /// <summary>
        ///     Informar se a biblioteca deve salvar o xml de envio e de retorno
        /// </summary>
        public bool SalvarXmlOrganizados
        {
            get { return _salvarXmlOrganizados; }
            set
            {
                _salvarXmlOrganizados = value;
            }
        }

        /// <summary>
        ///     Chave de Licença da DLL
        /// </summary>
        public string licencaDLL { get; set; }        

        /// <summary>
        ///     Instância do Singleton de ConfiguracaoServico
        /// </summary>
        public static ConfiguracaoServico Instancia
        {
            get
            {
                if (_instancia != null) return _instancia;
                lock (SyncRoot)
                {
                    if (_instancia != null) return _instancia;
                    _instancia = new ConfiguracaoServico();
                }

                return _instancia;
            }
        }
    }
}