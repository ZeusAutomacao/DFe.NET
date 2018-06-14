using System;
using System.IO;
using System.Net;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using DFe.Utils;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Classes.Servicos.Tipos;
using TipoAmbiente = NFe.Classes.Informacoes.Identificacao.Tipos.TipoAmbiente;

namespace NFe.Utils
{
    public sealed class ConfiguracaoServico
    {
        private string _diretorioSchemas;
        private bool _salvarXmlServicos;
        
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
        public VersaoServico VersaoNfeRecepcao { get; set; }

        /// <summary>
        ///     Versão do serviço NfeRetRecepcao
        /// </summary>
        public VersaoServico VersaoNfeRetRecepcao { get; set; }

        /// <summary>
        ///     Versão do serviço NfeConsultaCadastro
        /// </summary>
        public VersaoServico VersaoNfeConsultaCadastro { get; set; }

        /// <summary>
        ///     Versão do serviço NfeInutilizacao
        /// </summary>
        public VersaoServico VersaoNfeInutilizacao { get; set; }

        /// <summary>
        ///     Versão do serviço NfeConsultaProtocolo
        /// </summary>
        public VersaoServico VersaoNfeConsultaProtocolo { get; set; }

        /// <summary>
        ///     Versão do serviço NfeStatusServico
        /// </summary>
        public VersaoServico VersaoNfeStatusServico { get; set; }

        /// <summary>
        ///     Versão do serviço NFeAutorizacao
        /// </summary>
        public VersaoServico VersaoNFeAutorizacao { get; set; }

        /// <summary>
        ///     Versão do serviço NFeRetAutorizacao
        /// </summary>
        public VersaoServico VersaoNFeRetAutorizacao { get; set; }

        /// <summary>
        ///     Versão do serviço NFeDistribuicaoDFe
        /// </summary>
        public VersaoServico VersaoNFeDistribuicaoDFe { get; set; }

        /// <summary>
        ///     Versão do serviço NfeConsultaDest
        /// </summary>
        public VersaoServico VersaoNfeConsultaDest { get; set; }

        /// <summary>
        ///     Versão do serviço NfeDownloadNF
        /// </summary>
        public VersaoServico VersaoNfeDownloadNF { get; set; }

        /// <summary>
        ///     Versão do serviço admCscNFCe
        /// </summary>
        public VersaoServico VersaoNfceAministracaoCSC { get; set; }

        /// <summary>
        ///     Protocolo de segurança que deve ser utilizado no consumo dos webservices
        /// </summary>
        public SecurityProtocolType ProtocoloDeSeguranca { get; set; }

        /// <summary>
        ///     Diretório onde estão armazenados os schemas para validação
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
                if (!value)
                    DiretorioSalvarXml = "";
                _salvarXmlServicos = value;
            }
        }

        /// <summary>
        ///     Diretório onde os xmls de envio/retorno devem ser salvos
        /// </summary>
        public string DiretorioSalvarXml { get; set; }
    }
}