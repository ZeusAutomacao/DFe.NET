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
using System.Collections.Generic;
using System.Windows.Forms;
using CTe.AppTeste.Dao;
using CTe.AppTeste.Entidades;
using CTe.AppTeste.ModelBase;
using DFe.CertificadosDigitais.Implementacao;
using DFe.DocumentosEletronicos.CTe.Classes.Extensoes;
using DFe.DocumentosEletronicos.CTe.Classes.Informacoes;
using DFe.DocumentosEletronicos.CTe.Classes.Informacoes.Destinatario;
using DFe.DocumentosEletronicos.CTe.Classes.Informacoes.Emitente;
using DFe.DocumentosEletronicos.CTe.Classes.Informacoes.infCTeNormal;
using DFe.DocumentosEletronicos.CTe.Classes.Informacoes.infCTeNormal.infCargas;
using DFe.DocumentosEletronicos.CTe.Classes.Informacoes.infCTeNormal.infModals;
using DFe.DocumentosEletronicos.CTe.Classes.Informacoes.Identificacao;
using DFe.DocumentosEletronicos.CTe.Classes.Informacoes.Impostos;
using DFe.DocumentosEletronicos.CTe.Classes.Informacoes.Impostos.ICMSCTe;
using DFe.DocumentosEletronicos.CTe.Classes.Informacoes.Remetente;
using DFe.DocumentosEletronicos.CTe.Classes.Informacoes.Tipos;
using DFe.DocumentosEletronicos.CTe.Classes.Informacoes.Valores;
using DFe.DocumentosEletronicos.CTe.Classes.Retorno;
using DFe.DocumentosEletronicos.CTe.Classes.Servicos.Autorizacao;
using DFe.DocumentosEletronicos.CTe.Classes.Servicos.Evento.CorpoEvento;
using DFe.DocumentosEletronicos.CTe.Facade;
using DFe.DocumentosEletronicos.CTe.Servicos.ConsultaLoteCTe;
using DFe.DocumentosEletronicos.CTe.Servicos.ConsultaProtocoloCTe;
using DFe.DocumentosEletronicos.CTe.Servicos.EnviarCTe;
using DFe.DocumentosEletronicos.CTe.Servicos.EventosCTe;
using DFe.DocumentosEletronicos.CTe.Servicos.EvniarLoteCTe;
using DFe.DocumentosEletronicos.CTe.Servicos.InutilizacaoCTe;
using DFe.DocumentosEletronicos.CTe.Servicos.Recepcao;
using DFe.DocumentosEletronicos.CTe.Servicos.StatusServicoCTe;
using DFe.Entidades;
using DFe.Flags;
using CteEletronico = DFe.DocumentosEletronicos.CTe.Classes.Informacoes.CTe;
using DFe.Utils;
using DFe.Utils.Assinatura;
using dest = DFe.DocumentosEletronicos.CTe.Classes.Informacoes.Destinatario.dest;
using infNFe = DFe.DocumentosEletronicos.CTe.Classes.Informacoes.infCTeNormal.infDocumentos.infNFe;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace CTe.AppTeste
{
    public class RetornoEEnvio : EventArgs
    {
        public RetornoEEnvio(RetornoBase retorno)
        {
            Envio = retorno.EnvioXmlString;
            Retorno = retorno.RetornoXmlString;
        }

        public RetornoEEnvio(string envio, string retorno)
        {
            Envio = envio;
            Retorno = retorno;
        }

        public string Envio { get; set; }
        public string Retorno { get; set; }
    }

    public class CTeTesteModel : ViewModel
    {
        public event EventHandler<RetornoEEnvio> SucessoSync;


        private string _rntrc;
        private string _cnpj;
        private string _inscricaoEstadual;
        private string _nome;
        private string _nomeFantasia;
        private string _logradouro;
        private string _numero;
        private string _complemento;
        private string _bairro;
        private long _codigoIbgeMunicipio;
        private string _nomeMunicipio;
        private string _cep;
        private Estado _siglaUf;
        private string _telefone;
        private string _email;
        private string _numeroDeSerie;
        private string _caminhoArquivo;
        private string _senha;
        private bool _manterCertificadoEmCache;
        private string _diretorioSalvarXml;
        private bool _isSalvarXml;
        private Estado _ufDestino;
        private TipoAmbiente _ambiente;
        private short _serie;
        private long _numeracao;
        private bool _ambienteProducao;
        private bool _ambienteHomologacao;
        private string _diretorioSchemas;
        private int _timeOut;
        private VersaoServico _versaoLayout;

        #region empresa

        public string Rntrc
        {
            get { return _rntrc; }
            set
            {
                _rntrc = value;
                OnPropertyChanged("Rntrc");
            }
        }

        public string Cnpj
        {
            get { return _cnpj; }
            set
            {
                _cnpj = value;
                OnPropertyChanged("Cnpj");
            }
        }

        public string InscricaoEstadual
        {
            get { return _inscricaoEstadual; }
            set
            {
                _inscricaoEstadual = value;
                OnPropertyChanged("InscricaoEstadual");
            }
        }

        public string Nome
        {
            get { return _nome; }
            set
            {
                _nome = value;
                OnPropertyChanged("Nome");
            }
        }

        public string NomeFantasia
        {
            get { return _nomeFantasia; }
            set
            {
                _nomeFantasia = value;
                OnPropertyChanged("NomeFantasia");
            }
        }

        public string Logradouro
        {
            get { return _logradouro; }
            set
            {
                _logradouro = value;
                OnPropertyChanged("Logradouro");
            }
        }

        public string Numero
        {
            get { return _numero; }
            set
            {
                _numero = value;
                OnPropertyChanged("Numero");
            }
        }

        public string Complemento
        {
            get { return _complemento; }
            set
            {
                _complemento = value;
                OnPropertyChanged("Complemento");
            }
        }

        public string Bairro
        {
            get { return _bairro; }
            set
            {
                _bairro = value;
                OnPropertyChanged("Bairro");
            }
        }

        public long CodigoIbgeMunicipio
        {
            get { return _codigoIbgeMunicipio; }
            set
            {
                _codigoIbgeMunicipio = value;
                OnPropertyChanged("CodigoIbgeMunicipio");
            }
        }

        public string NomeMunicipio
        {
            get { return _nomeMunicipio; }
            set
            {
                _nomeMunicipio = value;
                OnPropertyChanged("NomeMunicipio");
            }
        }

        public string Cep
        {
            get { return _cep; }
            set
            {
                _cep = value;
                OnPropertyChanged("Cep");
            }
        }

        public Estado SiglaUf
        {
            get { return _siglaUf; }
            set
            {
                _siglaUf = value;
                OnPropertyChanged("SiglaUf");
            }
        }

        public string Telefone
        {
            get { return _telefone; }
            set
            {
                _telefone = value;
                OnPropertyChanged("Telefone");
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged("Email");
            }
        }

        #endregion

        #region certificado

        public string NumeroDeSerie
        {
            get { return _numeroDeSerie; }
            set
            {
                _numeroDeSerie = value;
                OnPropertyChanged("NumeroDeSerie");
            }
        }

        public string CaminhoArquivo
        {
            get { return _caminhoArquivo; }
            set
            {
                _caminhoArquivo = value;
                OnPropertyChanged("CaminhoArquivo");
            }
        }

        public string Senha
        {
            get { return _senha; }
            set
            {
                _senha = value;
                OnPropertyChanged("Senha");
            }
        }

        public bool ManterCertificadoEmCache
        {
            get { return _manterCertificadoEmCache; }
            set
            {
                _manterCertificadoEmCache = value;
                OnPropertyChanged("ManterCertificadoEmCache");
            }
        }

        #endregion

        #region configWebService

        public Estado UfDestino
        {
            get { return _ufDestino; }
            set
            {
                _ufDestino = value;
                OnPropertyChanged("UfDestino");
            }
        }

        public TipoAmbiente Ambiente
        {
            get { return _ambiente; }
            set
            {
                _ambiente = value;
                OnPropertyChanged("Ambiente");
            }
        }

        public short Serie
        {
            get { return _serie; }
            set
            {
                _serie = value;
                OnPropertyChanged("Serie");
            }
        }

        public long Numeracao
        {
            get { return _numeracao; }
            set
            {
                _numeracao = value;
                OnPropertyChanged("Numeracao");
            }
        }

        public VersaoServico VersaoLayout
        {
            get { return _versaoLayout; }
            set
            {
                _versaoLayout = value;
                OnPropertyChanged("VersaoLayout");
            }
        }

        public bool AmbienteProducao
        {
            get { return _ambienteProducao; }
            set
            {
                _ambienteProducao = value;
                OnPropertyChanged("AmbienteProducao");
            }
        }

        public bool AmbienteHomologacao
        {
            get { return _ambienteHomologacao; }
            set
            {
                _ambienteHomologacao = value;
                OnPropertyChanged("AmbienteHomologacao");
            }
        }

        public string DiretorioSchemas
        {
            get { return _diretorioSchemas; }
            set
            {
                _diretorioSchemas = value;
                OnPropertyChanged("DiretorioSchemas");
            }
        }

        public int TimeOut
        {
            get { return _timeOut; }
            set
            {
                _timeOut = value;
                OnPropertyChanged("TimeOut");
            }
        }

        #endregion

        #region configuração arquivo
        public string DiretorioSalvarXml
        {
            get { return _diretorioSalvarXml; }
            set
            {
                _diretorioSalvarXml = value;
                OnPropertyChanged("DiretorioSalvarXml");
            }
        }

        public bool IsSalvarXml
        {
            get { return _isSalvarXml; }
            set
            {
                _isSalvarXml = value;
                OnPropertyChanged("IsSalvarXml");
            }
        }

        #endregion configuração arquivo

        public void SalvarConfiguracoesXml()
        {
            var configuracaoDao = new ConfiguracaoDao();
            var configuracaoAppTeste = new Configuracao
            {
                Empresa =
                    {
                        Cnpj = Cnpj,
                        Bairro = Bairro,
                        Cep = Cep,
                        CodigoIbgeMunicipio = CodigoIbgeMunicipio,
                        Complemento = Complemento,
                        Email = Email,
                        InscricaoEstadual = InscricaoEstadual,
                        Logradouro = Logradouro,
                        Nome = Nome,
                        NomeFantasia = NomeFantasia,
                        NomeMunicipio = NomeMunicipio,
                        Numero = Numero,
                        SiglaUf = SiglaUf,
                        Telefone = Telefone,
                        RNTRC = Rntrc
                    },
                CertificadoDigital =
                {
                    CaminhoArquivo = CaminhoArquivo,
                    NumeroDeSerie = NumeroDeSerie,
                    Senha = Senha,
                    ManterEmCache = ManterCertificadoEmCache
                },
                ConfigWebService =
                {
                    UfEmitente = UfDestino,
                    Numeracao = Numeracao,
                    Serie = Serie,
                    Versao = VersaoLayout,
                    CaminhoSchemas = DiretorioSchemas,
                    TimeOut = TimeOut
                },
                DiretorioSalvarXml = DiretorioSalvarXml,
                IsSalvarXml = IsSalvarXml
            };

            if (AmbienteHomologacao)
                configuracaoAppTeste.ConfigWebService.Ambiente = TipoAmbiente.Homologacao;

            if (AmbienteProducao)
                configuracaoAppTeste.ConfigWebService.Ambiente = TipoAmbiente.Producao;


            configuracaoDao.SalvarConfiguracao(configuracaoAppTeste);
        }

        public void ObterSerialCertificado()
        {
            NumeroDeSerie = CertificadoDigital.ListareObterDoRepositorio().SerialNumber;
        }

        public void ObterCertificadoArquivo()
        {
            var janelaArquivo = new OpenFileDialog
            {
                Filter = "Certificado digital(*.pfx)|*.pfx"
            };
            if (janelaArquivo.ShowDialog() == true)
            {
                CaminhoArquivo = janelaArquivo.FileName;
            }
        }

        public void CarregarConfiguracoes()
        {
            var dao = new ConfiguracaoDao();
            var config = dao.BuscarConfiguracao();

            if (config == null) return;

            Cnpj = config.Empresa.Cnpj;
            Bairro = config.Empresa.Bairro;
            Cep = config.Empresa.Cep;
            CodigoIbgeMunicipio = config.Empresa.CodigoIbgeMunicipio;
            Complemento = config.Empresa.Complemento;
            Email = config.Empresa.Email;
            InscricaoEstadual = config.Empresa.InscricaoEstadual;
            Logradouro = config.Empresa.Logradouro;
            Nome = config.Empresa.Nome;
            NomeFantasia = config.Empresa.NomeFantasia;
            NomeMunicipio = config.Empresa.NomeMunicipio;
            Numero = config.Empresa.Numero;
            SiglaUf = config.Empresa.SiglaUf;
            Telefone = config.Empresa.Telefone;
            Rntrc = config.Empresa.RNTRC;

            Senha = config.CertificadoDigital.Senha;
            ManterCertificadoEmCache = config.CertificadoDigital.ManterEmCache;
            CaminhoArquivo = config.CertificadoDigital.CaminhoArquivo;
            NumeroDeSerie = config.CertificadoDigital.NumeroDeSerie;


            AmbienteProducao = true;

            if (config.ConfigWebService.Ambiente == TipoAmbiente.Homologacao)
                AmbienteHomologacao = true;

            Numeracao = config.ConfigWebService.Numeracao;
            Serie = config.ConfigWebService.Serie;

            UfDestino = config.ConfigWebService.UfEmitente;

            VersaoLayout = config.ConfigWebService.Versao;

            DiretorioSchemas = config.ConfigWebService.CaminhoSchemas;
            DiretorioSalvarXml = config.DiretorioSalvarXml;
            IsSalvarXml = config.IsSalvarXml;
            TimeOut = 3000;

            if (config.ConfigWebService.TimeOut > 0)
            {
                TimeOut = config.ConfigWebService.TimeOut;
            }
        }

        public void BuscarDiretorioSchema()
        {
            var dlg = new FolderBrowserDialog();
            dlg.ShowDialog();
            DiretorioSchemas = dlg.SelectedPath;
        }

        public void BuscarDiretorioSalvarXml()
        {
            var dlg = new FolderBrowserDialog();
            dlg.ShowDialog();
            DiretorioSalvarXml = dlg.SelectedPath;
        }

        private static CTeConfig CarregarConfiguracoes(Configuracao config)
        {
            var cteConfig = new CTeConfig();    
            
            cteConfig.TimeOut = config.ConfigWebService.TimeOut;
            cteConfig.EstadoUf = config.ConfigWebService.UfEmitente;
            cteConfig.TipoAmbiente = config.ConfigWebService.Ambiente;
            cteConfig.VersaoServico = config.ConfigWebService.Versao;
            cteConfig.CaminhoSchemas = config.ConfigWebService.CaminhoSchemas;
            cteConfig.IsSalvarXml = config.IsSalvarXml;
            cteConfig.CaminhoSalvarXml = config.DiretorioSalvarXml;
            cteConfig.CnpjEmitente = config.Empresa.Cnpj;

            return cteConfig;
        }

        private static DFeCertificadoDigital CarregarCertitifcado(Configuracao config)
        {
            var configCert = new DFeConfigCertificadoDigital
            {
                Serial = config.CertificadoDigital.NumeroDeSerie,
                LocalArquivo = config.CertificadoDigital.CaminhoArquivo,
                Senha = config.CertificadoDigital.Senha,
                TipoCertificado = TipoCertificado.A1Repositorio
            };


            return new DFeCertificadoDigital(configCert);
        }



        public void ConsultarStatusServico2()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            var configCTe = CarregarConfiguracoes(config);
            var certificado = CarregarCertitifcado(config);

            var facade = new CTeFacade(configCTe, certificado);
            var retorno = facade.StatusConsulta();

            OnSucessoSync(new RetornoEEnvio(retorno));
        }


        protected virtual void OnSucessoSync(RetornoEEnvio e)
        {
            if (SucessoSync == null) return;

            SucessoSync.Invoke(this, e);
        }

        public void ConsultaPorProtocolo()
        {
            var porChave = MessageBoxConfirmTuche("Sim = Por chave\nNão = Por arquivo xml");
            var chave = string.Empty;

            if (porChave == DialogResult.Yes)
            {
                chave = InputBoxTuche("Digite a chave de acesso da MDF-e");
            }

            if (porChave == DialogResult.No)
            {
                chave = BuscarChave();
            }

            if (string.IsNullOrEmpty(chave))
            {
                MessageBoxTuche("Ops.. Não a oque fazer sem uma chave de acesso");
                return;
            }


            var config = new ConfiguracaoDao().BuscarConfiguracao();
            var configCTe = CarregarConfiguracoes(config);
            var certificado = CarregarCertitifcado(config);

            var facade = new CTeFacade(configCTe, certificado);
            var retorno = facade.Consulta(chave);


            OnSucessoSync(new RetornoEEnvio(retorno));

        }

        private static void MessageBoxTuche(string mensagem, MessageBoxIcon icon = MessageBoxIcon.Information)
        {
            MessageBox.Show(mensagem, @"MDF-e", MessageBoxButtons.OK, icon);
        }

        private string BuscarChave()
        {
            var chave = string.Empty;
            var caminhoArquivoXml = BuscarArquivoXml();

            if (caminhoArquivoXml != null)
            {
                if (caminhoArquivoXml.Contains("completo"))
                {
                    var enviCTe = DFe.DocumentosEletronicos.CTe.Classes.Servicos.Autorizacao.enviCTe.LoadXmlArquivo(caminhoArquivoXml);

                    chave = enviCTe.CTe[0].Chave();
                }
                else
                {
                    var cte = CteEletronico.LoadXmlArquivo(caminhoArquivoXml);

                    chave = cte.Chave();
                }
            }
            return chave;
        }

        public string BuscarArquivoXml()
        {
            var janelaArquivo = new OpenFileDialog
            {
                Filter = "XML(*.xml)|*.xml"
            };

            if (janelaArquivo.ShowDialog() == true)
            {
                var caminhoXml = janelaArquivo.FileName;

                if (caminhoXml == null) return string.Empty;

                return caminhoXml;
            }
            return string.Empty;
        }


        private static string InputBoxTuche(string titulo)
        {
            var inputBox = new InputBoxWindow
            {
                TxtValor = { Text = string.Empty },
                TxtDescricao = { Text = titulo }
            };
            inputBox.ShowDialog();

            var valor = inputBox.TxtValor.Text;

            return valor;
        }

        private static DialogResult MessageBoxConfirmTuche(string mensagem)
        {
            return MessageBox.Show(mensagem, @"CT-e", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }


        public void InutilizacaoDeNumeracao()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            var configCTe = CarregarConfiguracoes(config);
            var certificado = CarregarCertitifcado(config);

            var numeroInicial = int.Parse(InputBoxTuche("Númeração Inicial"));
            var numeroFinal = int.Parse(InputBoxTuche("Númeração Final"));
            var ano = int.Parse(InputBoxTuche("Digite o ano, apenas os ultimos dois digitos"));
            var justificativa = InputBoxTuche("Justificativa (15 digitos no minimo)");

            var configInutilizar = new ConfigInutiliza(
                config.Empresa.Cnpj,
                config.ConfigWebService.Serie,
                numeroInicial,
                numeroFinal,
                ano,
                justificativa
            );

            var facade = new CTeFacade(configCTe, certificado);
            var retorno = facade.Inutilizacao(configInutilizar);

            OnSucessoSync(new RetornoEEnvio(retorno));
        }

        public void ConsultaPorNumeroRecibo()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            var configCTe = CarregarConfiguracoes(config);
            var certificado = CarregarCertitifcado(config);

            var numeroRecibo = InputBoxTuche("Número Recibo");

            var facade = new CTeFacade(configCTe, certificado);
            var retorno = facade.ConsultaLote(numeroRecibo);

            OnSucessoSync(new RetornoEEnvio(retorno));
        }

        public void EventoCancelarCTe()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            var configCTe = CarregarConfiguracoes(config);
            var certificado = CarregarCertitifcado(config);

            var caminho = BuscarArquivoXml();

            // aqui estou fazendo um load no lote de ct-e
            var cte = enviCTe.LoadXmlArquivo(caminho).CTe[0];

            // aqui estou fazendo um load no xml de envio de um ct-e
            //var cte = CteEletronico.LoadXmlArquivo(caminho);

            var sequenciaEvento = int.Parse(InputBoxTuche("Sequencia Evento"));
            var protocolo = InputBoxTuche("Protocolo");
            var justificativa = InputBoxTuche("Justificativa mínimo 15 digitos vlw");

            var facade = new CTeFacade(configCTe, certificado);
            var retorno = facade.Cancelar(cte.Chave(), cte.infCte.emit.CNPJ, sequenciaEvento, protocolo, justificativa);

            OnSucessoSync(new RetornoEEnvio(retorno));
        }

        public void CartaCorrecao()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            var configCTe = CarregarConfiguracoes(config);
            var certificado = CarregarCertitifcado(config);

            var caminho = BuscarArquivoXml();

            // aqui estou fazendo um load no lote de ct-e
            var cte = enviCTe.LoadXmlArquivo(caminho).CTe[0];

            // aqui estou fazendo um load no xml de envio de um ct-e
            //var cte = CteEletronico.LoadXmlArquivo(caminho);

            var sequenciaEvento = int.Parse(InputBoxTuche("Sequencia Evento"));


            // correções adicionadas.. não disponibilizei tela, mas serve como exemplo
            // como vemos a correção é bem diferente da nf-e
            var correcoes = new List<infCorrecao>
            {
                new infCorrecao
                {
                    campoAlterado = "nro",
                    grupoAlterado = "enderRem",
                    valorAlterado = "170"
                },
                new infCorrecao
                {
                    campoAlterado = "fone",
                    grupoAlterado = "rem",
                    valorAlterado = "14991001000"
                }
            };

            var facade = new CTeFacade(configCTe, certificado);
            var retorno = facade.CartaCorrecao(cte.Chave(), cte.infCte.emit.CNPJ, sequenciaEvento, correcoes);

            OnSucessoSync(new RetornoEEnvio(retorno));
        }

        public void CriarEnviarCTe2e3()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            var configCTe = CarregarConfiguracoes(config);
            var certificado = CarregarCertitifcado(config);

            var cteEletronico = new CteEletronico();

            #region infCte

            cteEletronico.infCte = new infCte();
            cteEletronico.infCte.versao = config.ConfigWebService.Versao;

            #endregion
            
            #region ide

            cteEletronico.infCte.ide = new ide();
            cteEletronico.infCte.ide.cUF = config.Empresa.SiglaUf;
            cteEletronico.infCte.ide.cCT = GetRandom();
            cteEletronico.infCte.ide.CFOP = 5353;
            cteEletronico.infCte.ide.natOp = "PRESTAÇÃO DE SERVICO DE TRANSPORTE CT-E EXEMPLO";

            if (config.ConfigWebService.Versao == VersaoServico.Versao200)
            {
                cteEletronico.infCte.ide.forPag = forPag.Pago;
            }
            cteEletronico.infCte.ide.mod = ModeloDocumento.CTe;
            cteEletronico.infCte.ide.serie = config.ConfigWebService.Serie;
            cteEletronico.infCte.ide.nCT = config.ConfigWebService.Numeracao;
            cteEletronico.infCte.ide.dhEmi = DateTime.Now;
            cteEletronico.infCte.ide.tpImp = tpImp.Retrado;
            cteEletronico.infCte.ide.tpEmis = tpEmis.teNormal;
            cteEletronico.infCte.ide.tpAmb = config.ConfigWebService.Ambiente; // o serviço adicionara automaticamente isso para você
            cteEletronico.infCte.ide.tpCTe = tpCTe.Normal;
            cteEletronico.infCte.ide.procEmi = procEmi.AplicativoContribuinte;
            cteEletronico.infCte.ide.verProc = "0.0.0.1";
            cteEletronico.infCte.ide.cMunEnv = config.Empresa.CodigoIbgeMunicipio;
            cteEletronico.infCte.ide.xMunEnv = config.Empresa.NomeMunicipio;
            cteEletronico.infCte.ide.UFEnv = config.Empresa.SiglaUf;
            cteEletronico.infCte.ide.modal = modal.rodoviario;
            cteEletronico.infCte.ide.tpServ = tpServ.normal;
            cteEletronico.infCte.ide.cMunIni = config.Empresa.CodigoIbgeMunicipio;
            cteEletronico.infCte.ide.xMunIni = config.Empresa.NomeMunicipio;
            cteEletronico.infCte.ide.UFIni = config.Empresa.SiglaUf;
            cteEletronico.infCte.ide.cMunFim = config.Empresa.CodigoIbgeMunicipio;
            cteEletronico.infCte.ide.xMunFim = config.Empresa.NomeMunicipio;
            cteEletronico.infCte.ide.UFFim = config.Empresa.SiglaUf;
            cteEletronico.infCte.ide.retira = retira.Nao;

            if (config.ConfigWebService.Versao == VersaoServico.Versao300)
            {
                cteEletronico.infCte.ide.indIEToma = indIEToma.ContribuinteIcms;
            }

            if (config.ConfigWebService.Versao == VersaoServico.Versao200)
            {
                cteEletronico.infCte.ide.tomaBase3 = new toma03
                {
                    toma = toma.Remetente
                };
            }

            if (config.ConfigWebService.Versao == VersaoServico.Versao300)
            {
                cteEletronico.infCte.ide.tomaBase3 = new toma3
                {
                    toma = toma.Remetente
                };
            }


            #endregion

            #region emit

            cteEletronico.infCte.emit = new emit();
            cteEletronico.infCte.emit.CNPJ = config.Empresa.Cnpj;
            cteEletronico.infCte.emit.IE = config.Empresa.InscricaoEstadual;
            cteEletronico.infCte.emit.xNome = config.Empresa.Nome;
            cteEletronico.infCte.emit.xFant = config.Empresa.NomeFantasia;

            cteEletronico.infCte.emit.enderEmit = new enderEmit();
            cteEletronico.infCte.emit.enderEmit.xLgr = config.Empresa.Logradouro;
            cteEletronico.infCte.emit.enderEmit.nro = config.Empresa.Numero;
            cteEletronico.infCte.emit.enderEmit.xCpl = config.Empresa.Complemento;
            cteEletronico.infCte.emit.enderEmit.xBairro = config.Empresa.Bairro;
            cteEletronico.infCte.emit.enderEmit.cMun = config.Empresa.CodigoIbgeMunicipio;
            cteEletronico.infCte.emit.enderEmit.xMun = config.Empresa.NomeMunicipio;
            cteEletronico.infCte.emit.enderEmit.CEP = long.Parse(config.Empresa.Cep);
            cteEletronico.infCte.emit.enderEmit.UF = config.Empresa.SiglaUf;
            cteEletronico.infCte.emit.enderEmit.fone = config.Empresa.Telefone;

            #endregion

            // Remetente , no caso adicionei os mesmos dados do emitente.. mas seriam o do remente.
            #region rem
            cteEletronico.infCte.rem = new rem();
            cteEletronico.infCte.rem.CNPJ = config.Empresa.Cnpj;
            cteEletronico.infCte.rem.IE = config.Empresa.InscricaoEstadual;
            cteEletronico.infCte.rem.xNome = config.Empresa.Nome;
            cteEletronico.infCte.rem.xFant = config.Empresa.NomeFantasia;
            cteEletronico.infCte.rem.fone = config.Empresa.Telefone;

            cteEletronico.infCte.rem.enderReme = new enderReme();
            cteEletronico.infCte.rem.enderReme.xLgr = config.Empresa.Logradouro;
            cteEletronico.infCte.rem.enderReme.nro = config.Empresa.Numero;
            cteEletronico.infCte.rem.enderReme.xCpl = config.Empresa.Complemento;
            cteEletronico.infCte.rem.enderReme.xBairro = config.Empresa.Bairro;
            cteEletronico.infCte.rem.enderReme.cMun = config.Empresa.CodigoIbgeMunicipio;
            cteEletronico.infCte.rem.enderReme.xMun = config.Empresa.NomeMunicipio;
            cteEletronico.infCte.rem.enderReme.CEP = long.Parse(config.Empresa.Cep);
            cteEletronico.infCte.rem.enderReme.UF = config.Empresa.SiglaUf;
            #endregion

            // Destinatário
            #region dest

            cteEletronico.infCte.dest = new dest();
            cteEletronico.infCte.dest.CNPJ = config.Empresa.Cnpj;
            cteEletronico.infCte.dest.IE = config.Empresa.InscricaoEstadual;
            cteEletronico.infCte.dest.xNome = config.Empresa.Nome;
            cteEletronico.infCte.dest.fone = config.Empresa.Telefone;

            cteEletronico.infCte.dest.enderDest = new enderDest();
            cteEletronico.infCte.dest.enderDest.xLgr = config.Empresa.Logradouro;
            cteEletronico.infCte.dest.enderDest.nro = config.Empresa.Numero;
            cteEletronico.infCte.dest.enderDest.xCpl = config.Empresa.Complemento;
            cteEletronico.infCte.dest.enderDest.xBairro = config.Empresa.Bairro;
            cteEletronico.infCte.dest.enderDest.cMun = config.Empresa.CodigoIbgeMunicipio;
            cteEletronico.infCte.dest.enderDest.xMun = config.Empresa.NomeMunicipio;
            cteEletronico.infCte.dest.enderDest.CEP = long.Parse(config.Empresa.Cep);
            cteEletronico.infCte.dest.enderDest.UF = config.Empresa.SiglaUf;

            #endregion

            #region vPrest
                
            cteEletronico.infCte.vPrest = new vPrest();
            cteEletronico.infCte.vPrest.vTPrest = 100m;
            cteEletronico.infCte.vPrest.vRec = 100m;

            #endregion

            #region imp

            cteEletronico.infCte.imp = new imp();
            cteEletronico.infCte.imp.ICMS = new ICMS();

            var icmsSimplesNacional = new ICMSSN();

            cteEletronico.infCte.imp.ICMS.TipoICMS = icmsSimplesNacional;

            if (config.ConfigWebService.Versao == VersaoServico.Versao300)
            {
                icmsSimplesNacional.CST = CST.ICMS90;
            }

            #endregion

            #region infCTeNorm

            cteEletronico.infCte.infCTeNorm = new infCTeNorm();
            cteEletronico.infCte.infCTeNorm.infCarga = new infCarga();
            cteEletronico.infCte.infCTeNorm.infCarga.vCarga = 1000m;
            cteEletronico.infCte.infCTeNorm.infCarga.proPred = "Linguiça com piqui";

            cteEletronico.infCte.infCTeNorm.infCarga.infQ = new List<infQ>();
            cteEletronico.infCte.infCTeNorm.infCarga.infQ.Add(new infQ
            {
                cUnid = cUnid.KG,
                qCarga = 10000,
                tpMed = "quilos gramas"
            });

            cteEletronico.infCte.infCTeNorm.infDoc = new infDoc();
            cteEletronico.infCte.infCTeNorm.infDoc.infNFe = new List<infNFe>();
            cteEletronico.infCte.infCTeNorm.infDoc.infNFe.Add(new infNFe
            {
                chave = "52161021025760000123550010000087341557247948"
            });

            if (config.ConfigWebService.Versao == VersaoServico.Versao200)
            {
                cteEletronico.infCte.infCTeNorm.seg = new List<seg>();
                cteEletronico.infCte.infCTeNorm.seg.Add(new seg
                {
                    respSeg = respSeg.Destinatario
                });
            }

            cteEletronico.infCte.infCTeNorm.infModal = new infModal();

            if (config.ConfigWebService.Versao == VersaoServico.Versao200)
            {
                cteEletronico.infCte.infCTeNorm.infModal.versaoModal = versaoModal.veM200;
            }

            if (config.ConfigWebService.Versao == VersaoServico.Versao300)
            {
                cteEletronico.infCte.infCTeNorm.infModal.versaoModal = versaoModal.veM300;
            }

            var rodoviario = new rodo();
            rodoviario.RNTRC = config.Empresa.RNTRC;

            if (config.ConfigWebService.Versao == VersaoServico.Versao200)
            {
                rodoviario.dPrev = DateTime.Now;
                rodoviario.lota = lota.Nao;
            }
            

            cteEletronico.infCte.infCTeNorm.infModal.ContainerModal = rodoviario;
            #endregion


            var numeroLote = InputBoxTuche("Número Lote");

            var facade = new CTeFacade(configCTe, certificado);

            // Evento executado antes do envio do CT-e para o WebService
            // servicoRecepcao.AntesDeEnviar += AntesEnviarLoteCte;

            var retornoEnvio = facade.EnviarLote(int.Parse(numeroLote), new List<CteEletronico> { cteEletronico });

            OnSucessoSync(new RetornoEEnvio(retornoEnvio));

            config.ConfigWebService.Numeracao++;
            new ConfiguracaoDao().SalvarConfiguracao(config);
        }

        private void AntesEnviarLoteCte(object sender, AntesEnviarRecepcao e)
        {
            e.enviCTe.CTe.ForEach(cte =>
            {
                MessageBoxTuche(cte.Chave());
            });
        }


        private static int GetRandom()
        {
            var rand = new Random();
            return rand.Next(11111111, 99999999);
        }

        public void CriarEnviarCTeConsultaReciboAutomatico2e3()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            var configCTe = CarregarConfiguracoes(config);
            var certificado = CarregarCertitifcado(config);

            var cteEletronico = new CteEletronico();

            #region infCte

            cteEletronico.infCte = new infCte();
            cteEletronico.infCte.versao = config.ConfigWebService.Versao;

            #endregion

            #region ide

            cteEletronico.infCte.ide = new ide();
            cteEletronico.infCte.ide.cUF = config.Empresa.SiglaUf;
            cteEletronico.infCte.ide.cCT = GetRandom();
            cteEletronico.infCte.ide.CFOP = 5353;
            cteEletronico.infCte.ide.natOp = "PRESTAÇÃO DE SERVICO DE TRANSPORTE CT-E EXEMPLO";

            if (config.ConfigWebService.Versao == VersaoServico.Versao200)
            {
                cteEletronico.infCte.ide.forPag = forPag.Pago;
            }
            cteEletronico.infCte.ide.mod = ModeloDocumento.CTe;
            cteEletronico.infCte.ide.serie = config.ConfigWebService.Serie;
            cteEletronico.infCte.ide.nCT = config.ConfigWebService.Numeracao;
            cteEletronico.infCte.ide.dhEmi = DateTime.Now;
            cteEletronico.infCte.ide.tpImp = tpImp.Retrado;
            cteEletronico.infCte.ide.tpEmis = tpEmis.teNormal;
            cteEletronico.infCte.ide.tpAmb = config.ConfigWebService.Ambiente; // o serviço adicionara automaticamente isso para você
            cteEletronico.infCte.ide.tpCTe = tpCTe.Normal;
            cteEletronico.infCte.ide.procEmi = procEmi.AplicativoContribuinte;
            cteEletronico.infCte.ide.verProc = "0.0.0.1";
            cteEletronico.infCte.ide.cMunEnv = config.Empresa.CodigoIbgeMunicipio;
            cteEletronico.infCte.ide.xMunEnv = config.Empresa.NomeMunicipio;
            cteEletronico.infCte.ide.UFEnv = config.Empresa.SiglaUf;
            cteEletronico.infCte.ide.modal = modal.rodoviario;
            cteEletronico.infCte.ide.tpServ = tpServ.normal;
            cteEletronico.infCte.ide.cMunIni = config.Empresa.CodigoIbgeMunicipio;
            cteEletronico.infCte.ide.xMunIni = config.Empresa.NomeMunicipio;
            cteEletronico.infCte.ide.UFIni = config.Empresa.SiglaUf;
            cteEletronico.infCte.ide.cMunFim = config.Empresa.CodigoIbgeMunicipio;
            cteEletronico.infCte.ide.xMunFim = config.Empresa.NomeMunicipio;
            cteEletronico.infCte.ide.UFFim = config.Empresa.SiglaUf;
            cteEletronico.infCte.ide.retira = retira.Nao;

            if (config.ConfigWebService.Versao == VersaoServico.Versao300)
            {
                cteEletronico.infCte.ide.indIEToma = indIEToma.ContribuinteIcms;
            }

            if (config.ConfigWebService.Versao == VersaoServico.Versao200)
            {
                cteEletronico.infCte.ide.tomaBase3 = new toma03
                {
                    toma = toma.Remetente
                };
            }

            if (config.ConfigWebService.Versao == VersaoServico.Versao300)
            {
                cteEletronico.infCte.ide.tomaBase3 = new toma3
                {
                    toma = toma.Remetente
                };
            }


            #endregion

            #region emit

            cteEletronico.infCte.emit = new emit();
            cteEletronico.infCte.emit.CNPJ = config.Empresa.Cnpj;
            cteEletronico.infCte.emit.IE = config.Empresa.InscricaoEstadual;
            cteEletronico.infCte.emit.xNome = config.Empresa.Nome;
            cteEletronico.infCte.emit.xFant = config.Empresa.NomeFantasia;

            cteEletronico.infCte.emit.enderEmit = new enderEmit();
            cteEletronico.infCte.emit.enderEmit.xLgr = config.Empresa.Logradouro;
            cteEletronico.infCte.emit.enderEmit.nro = config.Empresa.Numero;
            cteEletronico.infCte.emit.enderEmit.xCpl = config.Empresa.Complemento;
            cteEletronico.infCte.emit.enderEmit.xBairro = config.Empresa.Bairro;
            cteEletronico.infCte.emit.enderEmit.cMun = config.Empresa.CodigoIbgeMunicipio;
            cteEletronico.infCte.emit.enderEmit.xMun = config.Empresa.NomeMunicipio;
            cteEletronico.infCte.emit.enderEmit.CEP = long.Parse(config.Empresa.Cep);
            cteEletronico.infCte.emit.enderEmit.UF = config.Empresa.SiglaUf;
            cteEletronico.infCte.emit.enderEmit.fone = config.Empresa.Telefone;

            #endregion

            // Remetente , no caso adicionei os mesmos dados do emitente.. mas seriam o do remente.
            #region rem
            cteEletronico.infCte.rem = new rem();
            cteEletronico.infCte.rem.CNPJ = config.Empresa.Cnpj;
            cteEletronico.infCte.rem.IE = config.Empresa.InscricaoEstadual;
            cteEletronico.infCte.rem.xNome = config.Empresa.Nome;
            cteEletronico.infCte.rem.xFant = config.Empresa.NomeFantasia;
            cteEletronico.infCte.rem.fone = config.Empresa.Telefone;

            cteEletronico.infCte.rem.enderReme = new enderReme();
            cteEletronico.infCte.rem.enderReme.xLgr = config.Empresa.Logradouro;
            cteEletronico.infCte.rem.enderReme.nro = config.Empresa.Numero;
            cteEletronico.infCte.rem.enderReme.xCpl = config.Empresa.Complemento;
            cteEletronico.infCte.rem.enderReme.xBairro = config.Empresa.Bairro;
            cteEletronico.infCte.rem.enderReme.cMun = config.Empresa.CodigoIbgeMunicipio;
            cteEletronico.infCte.rem.enderReme.xMun = config.Empresa.NomeMunicipio;
            cteEletronico.infCte.rem.enderReme.CEP = long.Parse(config.Empresa.Cep);
            cteEletronico.infCte.rem.enderReme.UF = config.Empresa.SiglaUf;
            #endregion

            // Destinatário
            #region dest

            cteEletronico.infCte.dest = new dest();
            cteEletronico.infCte.dest.CNPJ = config.Empresa.Cnpj;
            cteEletronico.infCte.dest.IE = config.Empresa.InscricaoEstadual;
            cteEletronico.infCte.dest.xNome = config.Empresa.Nome;
            cteEletronico.infCte.dest.fone = config.Empresa.Telefone;

            cteEletronico.infCte.dest.enderDest = new enderDest();
            cteEletronico.infCte.dest.enderDest.xLgr = config.Empresa.Logradouro;
            cteEletronico.infCte.dest.enderDest.nro = config.Empresa.Numero;
            cteEletronico.infCte.dest.enderDest.xCpl = config.Empresa.Complemento;
            cteEletronico.infCte.dest.enderDest.xBairro = config.Empresa.Bairro;
            cteEletronico.infCte.dest.enderDest.cMun = config.Empresa.CodigoIbgeMunicipio;
            cteEletronico.infCte.dest.enderDest.xMun = config.Empresa.NomeMunicipio;
            cteEletronico.infCte.dest.enderDest.CEP = long.Parse(config.Empresa.Cep);
            cteEletronico.infCte.dest.enderDest.UF = config.Empresa.SiglaUf;

            #endregion

            #region vPrest

            cteEletronico.infCte.vPrest = new vPrest();
            cteEletronico.infCte.vPrest.vTPrest = 100m;
            cteEletronico.infCte.vPrest.vRec = 100m;

            #endregion

            #region imp

            cteEletronico.infCte.imp = new imp();
            cteEletronico.infCte.imp.ICMS = new ICMS();

            var icmsSimplesNacional = new ICMSSN();

            cteEletronico.infCte.imp.ICMS.TipoICMS = icmsSimplesNacional;

            if (config.ConfigWebService.Versao == VersaoServico.Versao300)
            {
                icmsSimplesNacional.CST = CST.ICMS90;
            }

            #endregion

            #region infCTeNorm

            cteEletronico.infCte.infCTeNorm = new infCTeNorm();
            cteEletronico.infCte.infCTeNorm.infCarga = new infCarga();
            cteEletronico.infCte.infCTeNorm.infCarga.vCarga = 1000m;
            cteEletronico.infCte.infCTeNorm.infCarga.proPred = "Linguiça com piqui";

            cteEletronico.infCte.infCTeNorm.infCarga.infQ = new List<infQ>();
            cteEletronico.infCte.infCTeNorm.infCarga.infQ.Add(new infQ
            {
                cUnid = cUnid.KG,
                qCarga = 10000,
                tpMed = "quilos gramas"
            });

            cteEletronico.infCte.infCTeNorm.infDoc = new infDoc();
            cteEletronico.infCte.infCTeNorm.infDoc.infNFe = new List<infNFe>();
            cteEletronico.infCte.infCTeNorm.infDoc.infNFe.Add(new infNFe
            {
                chave = "52161021025760000123550010000087341557247948"
            });

            if (config.ConfigWebService.Versao == VersaoServico.Versao200)
            {
                cteEletronico.infCte.infCTeNorm.seg = new List<seg>();
                cteEletronico.infCte.infCTeNorm.seg.Add(new seg
                {
                    respSeg = respSeg.Destinatario
                });
            }

            cteEletronico.infCte.infCTeNorm.infModal = new infModal();

            if (config.ConfigWebService.Versao == VersaoServico.Versao200)
            {
                cteEletronico.infCte.infCTeNorm.infModal.versaoModal = versaoModal.veM200;
            }

            if (config.ConfigWebService.Versao == VersaoServico.Versao300)
            {
                cteEletronico.infCte.infCTeNorm.infModal.versaoModal = versaoModal.veM300;
            }

            var rodoviario = new rodo();
            rodoviario.RNTRC = config.Empresa.RNTRC;

            if (config.ConfigWebService.Versao == VersaoServico.Versao200)
            {
                rodoviario.dPrev = DateTime.Now;
                rodoviario.lota = lota.Nao;
            }


            cteEletronico.infCte.infCTeNorm.infModal.ContainerModal = rodoviario;
            #endregion


            var numeroLote = InputBoxTuche("Número Lote");


            var facade = new CTeFacade(configCTe, certificado);


            var retorno = facade.Enviar(Convert.ToInt32(numeroLote), cteEletronico);


            string xmlRetorno = string.Empty;

            if (retorno.CteProc != null)
                xmlRetorno = retorno.CteProc.ObterXmlString();

            if (retorno.RetConsReciCTe.protCTe[0].infProt.cStat != 100)
            {
                xmlRetorno = retorno.RetConsReciCTe.RetornoXmlString;
            }

            OnSucessoSync(new RetornoEEnvio(retorno.RetEnviCte.EnvioXmlString, xmlRetorno));

            config.ConfigWebService.Numeracao++;
            new ConfiguracaoDao().SalvarConfiguracao(config);
        }
    }
}