/********************************************************************************/
/* Projeto: Biblioteca ZeusNFe                                                  */
/* Biblioteca C# para emissão de Nota Fiscal Eletrônica - NFe e Nota Fiscal de  */
/* Consumidor Eletrônica - NFC-e (http://www.nfe.fazenda.gov.br)                */
/*                                                                              */
/* Direitos Autorais Reservados (c) 2014 Adenilton Batista da Silva             */
/*                                       Zeusdev Tecnologia LTDA ME             */
/*                                                                              */
/*  Você pode obter a última versão desse arquivo no GitHub                     */
/* localizado em https://github.com/adeniltonbs/Zeus.Net.NFe.NFCe               */
/*                                                                              */
/*                                                                              */
/*  Esta biblioteca é software livre; você pode redistribuí-la e/ou modificá-la */
/* sob os termos da Licença Pública Geral Menor do GNU conforme publicada pela  */
/* Free Software Foundation; tanto a versão 2.1 da Licença, ou (a seu critério) */
/* qualquer versão posterior.                                                   */
/*                                                                              */
/*  Esta biblioteca é distribuída na expectativa de que seja útil, porém, SEM   */
/* NENHUMA GARANTIA; nem mesmo a garantia implícita de COMERCIABILIDADE OU      */
/* ADEQUAÇÃO A UMA FINALIDADE ESPECÍFICA. Consulte a Licença Pública Geral Menor*/
/* do GNU para mais detalhes. (Arquivo LICENÇA.TXT ou LICENSE.TXT)              */
/*                                                                              */
/*  Você deve ter recebido uma cópia da Licença Pública Geral Menor do GNU junto*/
/* com esta biblioteca; se não, escreva para a Free Software Foundation, Inc.,  */
/* no endereço 59 Temple Street, Suite 330, Boston, MA 02111-1307 USA.          */
/* Você também pode obter uma copia da licença em:                              */
/* http://www.opensource.org/licenses/lgpl-license.php                          */
/*                                                                              */
/* Zeusdev Tecnologia LTDA ME - adenilton@zeusautomacao.com.br                  */
/* http://www.zeusautomacao.com.br/                                             */
/* Rua Comendador Francisco josé da Cunha, 111 - Itabaiana - SE - 49500-000     */
/********************************************************************************/
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using DFe.Utils;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Classes.Servicos.Tipos;
using NFe.Utils.Annotations;
using NFe.Utils.Enderecos;

namespace NFe.Utils
{
    public sealed class ConfiguracaoServico : INotifyPropertyChanged
    {
        private static volatile ConfiguracaoServico _instancia;
        private static readonly object SyncRoot = new object();
        private string _diretorioSchemas;
        private bool _salvarXmlServicos;
        private VersaoServico _versaoLayout;
        private Estado _cUf;
        private TipoAmbiente _tpAmb;
        private TipoEmissao _tpEmis;
        private ModeloDocumento _modeloDocumento;
        private bool _defineVersaoServicosAutomaticamente = true;
        private VersaoServico _versaoRecepcaoEventoCceCancelamento;
        private VersaoServico _versaoRecepcaoEventoEpec;
        private VersaoServico _versaoRecepcaoEventoManifestacaoDestinatario;
        private VersaoServico _versaoNfeRecepcao;
        private VersaoServico _versaoNfeRetRecepcao;
        private VersaoServico _versaoNfeConsultaCadastro;
        private VersaoServico _versaoNfeInutilizacao;
        private VersaoServico _versaoNfeConsultaProtocolo;
        private VersaoServico _versaoNfeStatusServico;
        private VersaoServico _versaoNFeAutorizacao;
        private VersaoServico _versaoNFeRetAutorizacao;
        private VersaoServico _versaoNFeDistribuicaoDFe;
        private VersaoServico _versaoNfeConsultaDest;
        private VersaoServico _versaoNfeDownloadNf;
        private VersaoServico _versaoNfceAministracaoCsc;

        public ConfiguracaoServico()
        {
            Certificado =
                new ConfiguracaoCertificado
                {
                    SignatureMethodSignedXml = "http://www.w3.org/2000/09/xmldsig#rsa-sha1",
                    DigestMethodReference = "http://www.w3.org/2000/09/xmldsig#sha1"
                };

            cUF = Estado.AC;
        }

        static ConfiguracaoServico()
        {
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
        public Estado cUF
        {
            get { return _cUf; }
            set
            {
                if (value == _cUf) return;
                _cUf = value;
                OnPropertyChanged();
                AtualizaVersoes();
            }
        }

        /// <summary>
        ///     Tipo de ambiente do webservice (Produção, Homologação)
        /// </summary>
        public TipoAmbiente tpAmb
        {
            get { return _tpAmb; }
            set
            {
                if (value == _tpAmb) return;
                _tpAmb = value;
                OnPropertyChanged();
                AtualizaVersoes();
            }
        }

        /// <summary>
        ///     Tipo de Emissão da NF-e
        /// </summary>
        public TipoEmissao tpEmis
        {
            get { return _tpEmis; }
            set
            {
                if (value == _tpEmis) return;
                _tpEmis = value;
                OnPropertyChanged();
                AtualizaVersoes();
            }
        }

        /// <summary>
        ///     Tipo de documento que está sendo referenciado nos webservices
        /// </summary>
        public ModeloDocumento ModeloDocumento
        {
            get { return _modeloDocumento; }
            set
            {
                if (value == _modeloDocumento) return;
                _modeloDocumento = value;
                OnPropertyChanged();
                AtualizaVersoes();
            }
        }

        public VersaoServico VersaoLayout
        {
            get { return _versaoLayout; }
            set
            {
                if (value == _versaoLayout) return;
                _versaoLayout = value;
                OnPropertyChanged();
                AtualizaVersoes();
            }
        }

        /// <summary>
        /// Determina se as versões dos serviços serão atualizadas automaticamente quando um dos seguintes atributos forem alterados:
        /// <para><see cref="VersaoLayout"/></para>
        /// <para><see cref="cUF"/></para>
        /// <para><see cref="tpAmb"/></para>
        /// <para><see cref="ModeloDocumento"/></para>
        /// <para><see cref="tpEmis"/></para>
        /// </summary>
        public bool DefineVersaoServicosAutomaticamente
        {
            get
            {
                return _defineVersaoServicosAutomaticamente;
            }
            set
            {
                if (value == _defineVersaoServicosAutomaticamente) return;
                _defineVersaoServicosAutomaticamente = value;
                OnPropertyChanged();
                AtualizaVersoes();
            }
        }

        /// <summary>
        /// Atualiza as versões dos serviços
        /// <para>Obs: As versões do serviços podem variar em função  da UF(<see cref="Estado"/>), do tipo de ambiente(<see cref="TipoAmbiente"/>), do modelo de documento(<see cref="DFe.Classes.Flags.ModeloDocumento"/>) e da forma de emissão(<see cref="TipoEmissao"/>)</para>
        /// </summary>
        private void AtualizaVersoes()
        {
            if (!_defineVersaoServicosAutomaticamente) return;

                var enderecosMaisecentes =
                Enderecador.ObterEnderecoServicosMaisRecentes(VersaoLayout, cUF, tpAmb, ModeloDocumento, tpEmis);

            var obterVersao = new Func<ServicoNFe, VersaoServico>(servico =>
                enderecosMaisecentes.Where(n => n.ServicoNFe == servico).Select(n => n.VersaoServico).DefaultIfEmpty(VersaoServico.Versao100).FirstOrDefault());


            if (enderecosMaisecentes.Any())
            {
                VersaoRecepcaoEventoCceCancelamento = obterVersao(ServicoNFe.RecepcaoEventoCancelmento);
                VersaoRecepcaoEventoEpec = obterVersao(ServicoNFe.RecepcaoEventoEpec);
                VersaoRecepcaoEventoManifestacaoDestinatario = obterVersao(ServicoNFe.RecepcaoEventoManifestacaoDestinatario);
                VersaoNfeRecepcao = obterVersao(ServicoNFe.NfeRecepcao);
                VersaoNfeRetRecepcao = obterVersao(ServicoNFe.NfeRetRecepcao);
                VersaoNfeConsultaCadastro = obterVersao(ServicoNFe.NfeConsultaCadastro);
                VersaoNfeInutilizacao = obterVersao(ServicoNFe.NfeInutilizacao);
                VersaoNfeConsultaProtocolo = obterVersao(ServicoNFe.NfeConsultaProtocolo);
                VersaoNfeStatusServico = obterVersao(ServicoNFe.NfeStatusServico);
                VersaoNFeAutorizacao = obterVersao(ServicoNFe.NFeAutorizacao);
                VersaoNFeRetAutorizacao = obterVersao(ServicoNFe.NFeRetAutorizacao);
                VersaoNFeDistribuicaoDFe = obterVersao(ServicoNFe.NFeDistribuicaoDFe);
                VersaoNfeConsultaDest = obterVersao(ServicoNFe.NfeConsultaDest);
                VersaoNfeDownloadNF = obterVersao(ServicoNFe.NfeDownloadNF);
                VersaoNfceAministracaoCSC = obterVersao(ServicoNFe.NfceAdministracaoCSC);
            }
        }

        #region Versões dos serviços

        /// <summary>
        ///     Versão do serviço RecepcaoEvento para Carta de Correção e Cancelamento
        /// </summary>
        public VersaoServico VersaoRecepcaoEventoCceCancelamento
        {
            get { return _versaoRecepcaoEventoCceCancelamento; }
            set
            {
                if (value == _versaoRecepcaoEventoCceCancelamento) return;
                _versaoRecepcaoEventoCceCancelamento = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Versão do serviço RecepcaoEvento para EPEC
        /// </summary>
        public VersaoServico VersaoRecepcaoEventoEpec
        {
            get { return _versaoRecepcaoEventoEpec; }
            set
            {
                if (value == _versaoRecepcaoEventoEpec) return;
                _versaoRecepcaoEventoEpec = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Versão do serviço RecepcaoEvento para Manifestação do destinatário
        /// </summary>
        public VersaoServico VersaoRecepcaoEventoManifestacaoDestinatario
        {
            get { return _versaoRecepcaoEventoManifestacaoDestinatario; }
            set
            {
                if (value == _versaoRecepcaoEventoManifestacaoDestinatario) return;
                _versaoRecepcaoEventoManifestacaoDestinatario = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Versão do serviço NfeRecepcao
        /// </summary>
        public VersaoServico VersaoNfeRecepcao
        {
            get { return _versaoNfeRecepcao; }
            set
            {
                if (value == _versaoNfeRecepcao) return;
                _versaoNfeRecepcao = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Versão do serviço NfeRetRecepcao
        /// </summary>
        public VersaoServico VersaoNfeRetRecepcao
        {
            get { return _versaoNfeRetRecepcao; }
            set
            {
                if (value == _versaoNfeRetRecepcao) return;
                _versaoNfeRetRecepcao = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Versão do serviço NfeConsultaCadastro
        /// </summary>
        public VersaoServico VersaoNfeConsultaCadastro
        {
            get { return _versaoNfeConsultaCadastro; }
            set
            {
                if (value == _versaoNfeConsultaCadastro) return;
                _versaoNfeConsultaCadastro = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Versão do serviço NfeInutilizacao
        /// </summary>
        public VersaoServico VersaoNfeInutilizacao
        {
            get { return _versaoNfeInutilizacao; }
            set
            {
                if (value == _versaoNfeInutilizacao) return;
                _versaoNfeInutilizacao = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Versão do serviço NfeConsultaProtocolo
        /// </summary>
        public VersaoServico VersaoNfeConsultaProtocolo
        {
            get { return _versaoNfeConsultaProtocolo; }
            set
            {
                if (value == _versaoNfeConsultaProtocolo) return;
                _versaoNfeConsultaProtocolo = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Versão do serviço NfeStatusServico
        /// </summary>
        public VersaoServico VersaoNfeStatusServico
        {
            get { return _versaoNfeStatusServico; }
            set
            {
                if (value == _versaoNfeStatusServico) return;
                _versaoNfeStatusServico = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Versão do serviço NFeAutorizacao
        /// </summary>
        public VersaoServico VersaoNFeAutorizacao
        {
            get { return _versaoNFeAutorizacao; }
            set
            {
                if (value == _versaoNFeAutorizacao) return;
                _versaoNFeAutorizacao = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Versão do serviço NFeRetAutorizacao
        /// </summary>
        public VersaoServico VersaoNFeRetAutorizacao
        {
            get { return _versaoNFeRetAutorizacao; }
            set
            {
                if (value == _versaoNFeRetAutorizacao) return;
                _versaoNFeRetAutorizacao = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Versão do serviço NFeDistribuicaoDFe
        /// </summary>
        public VersaoServico VersaoNFeDistribuicaoDFe
        {
            get { return _versaoNFeDistribuicaoDFe; }
            set
            {
                if (value == _versaoNFeDistribuicaoDFe) return;
                _versaoNFeDistribuicaoDFe = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Versão do serviço NfeConsultaDest
        /// </summary>
        public VersaoServico VersaoNfeConsultaDest
        {
            get { return _versaoNfeConsultaDest; }
            set
            {
                if (value == _versaoNfeConsultaDest) return;
                _versaoNfeConsultaDest = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Versão do serviço NfeDownloadNF
        /// </summary>
        public VersaoServico VersaoNfeDownloadNF
        {
            get { return _versaoNfeDownloadNf; }
            set
            {
                if (value == _versaoNfeDownloadNf) return;
                _versaoNfeDownloadNf = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///     Versão do serviço admCscNFCe
        /// </summary>
        public VersaoServico VersaoNfceAministracaoCSC
        {
            get { return _versaoNfceAministracaoCsc; }
            set
            {
                if (value == _versaoNfceAministracaoCsc) return;
                _versaoNfceAministracaoCsc = value;
                OnPropertyChanged();
            }
        }

        #endregion

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

        /// <summary>
        /// Determina se o cerificado do servidor deve ser verificado
        /// </summary>
        public bool ValidarCertificadoDoServidor { get; set; }

        /// <summary>
        /// Determina se os schemas devem ser validados antes do envio ao WebService
        /// O comportamento padrão é true!
        /// </summary>
        public bool ValidarSchemas { get; set; } = true;

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

        public bool RemoverAcentos { get; set; }

        /// <summary>
        ///     Limpa a instancia atual caso exista
        /// </summary>
        public static void LimparIntancia()
        {
            _instancia = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}