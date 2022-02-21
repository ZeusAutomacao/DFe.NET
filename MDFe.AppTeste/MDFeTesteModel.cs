/********************************************************************************/
/* Projeto: Biblioteca ZeusMDFe                                                 */
/* Biblioteca C# para emissão de Manifesto Eletrônico Fiscal de Documentos      */
/* (https://mdfe-portal.sefaz.rs.gov.br/                                        */
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
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using DFe.Utils;
using MDFe.AppTeste.Dao;
using MDFe.AppTeste.Entidades;
using MDFe.AppTeste.ModelBase;
using MDFe.Classes.Extencoes;
using MDFe.Classes.Flags;
using MDFe.Classes.Informacoes;
using MDFe.Classes.Informacoes.Evento.CorpoEvento;
using MDFe.Classes.Retorno;
using MDFe.Classes.Servicos.Autorizacao;
using MDFe.Servicos.ConsultaNaoEncerradosMDFe;
using MDFe.Servicos.ConsultaProtocoloMDFe;
using MDFe.Servicos.EventosMDFe;
using MDFe.Servicos.RecepcaoMDFe;
using MDFe.Servicos.RetRecepcaoMDFe;
using MDFe.Servicos.StatusServicoMDFe;
using MDFe.Utils.Configuracoes;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MDFeEletronico = MDFe.Classes.Informacoes.MDFe;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using VersaoServico = MDFe.Utils.Flags.VersaoServico;

namespace MDFe.AppTeste
{
    public class RetornoEEnvio : EventArgs
    {
        public RetornoEEnvio(RetornoBase retorno)
        {
            Envio = retorno.EnvioXmlString;
            Retorno = retorno.RetornoXmlString;
        }

        public string Envio { get; set; }
        public string Retorno { get; set; }
    }


    public class MDFeTesteModel : ViewModel
    {
        public event EventHandler<RetornoEEnvio> SucessoSync;

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
        private Estado _ufDestino;
        private TipoAmbiente _ambiente;
        private short _serie;
        private long _numeracao;
        private VersaoServico _versaoMdFeRecepcao;
        private VersaoServico _versaoMdFeRetRecepcao;
        private VersaoServico _versaoMdFeRecepcaoEvento;
        private VersaoServico _versaoMdFeConsulta;
        private VersaoServico _versaoMdFeStatusServico;
        private VersaoServico _versaoMdFeConsNaoEnc;
        private bool _ambienteProducao;
        private bool _ambienteHomologacao;
        private string _rntrc;
        private string _diretorioSchemas;
        private string _diretorioSalvarXml;
        private bool _isSalvarXml;
        private int _timeOut;
        private bool _manterCertificadoEmCache;
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
                    VersaoLayout = VersaoLayout,
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
            NumeroDeSerie = CertificadoDigitalUtils.ListareObterDoRepositorio().SerialNumber;
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

        public void CarregarConfiguracoesMDFe()
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

            VersaoLayout = config.ConfigWebService.VersaoLayout;

            DiretorioSchemas = config.ConfigWebService.CaminhoSchemas;
            DiretorioSalvarXml = config.DiretorioSalvarXml;
            IsSalvarXml = config.IsSalvarXml;
            TimeOut = 3000;

            if (config.ConfigWebService.TimeOut > 0)
            {
                TimeOut = config.ConfigWebService.TimeOut;
            }
        }

        public void CriarEnviar()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            CarregarConfiguracoesMDFe(config);
            var mdfe = new MDFeEletronico();

            #region (ide)
            mdfe.InfMDFe.Ide.CUF = config.ConfigWebService.UfEmitente;
            mdfe.InfMDFe.Ide.TpAmb = config.ConfigWebService.Ambiente;
            mdfe.InfMDFe.Ide.TpEmit = MDFeTipoEmitente.PrestadorServicoDeTransporte;
            mdfe.InfMDFe.Ide.Mod = ModeloDocumento.MDFe;
            mdfe.InfMDFe.Ide.Serie = 750;
            mdfe.InfMDFe.Ide.NMDF = ++config.ConfigWebService.Numeracao;
            mdfe.InfMDFe.Ide.CMDF = GetRandom();
            mdfe.InfMDFe.Ide.Modal = MDFeModal.Rodoviario;
            mdfe.InfMDFe.Ide.DhEmi = DateTime.Now;
            mdfe.InfMDFe.Ide.TpEmis = MDFeTipoEmissao.Normal;
            mdfe.InfMDFe.Ide.ProcEmi = MDFeIdentificacaoProcessoEmissao.EmissaoComAplicativoContribuinte;
            mdfe.InfMDFe.Ide.VerProc = "versao28383";
            mdfe.InfMDFe.Ide.UFIni = Estado.GO;
            mdfe.InfMDFe.Ide.UFFim = Estado.MT;


            mdfe.InfMDFe.Ide.InfMunCarrega.Add(new MDFeInfMunCarrega
            {
                CMunCarrega = "5211701",
                XMunCarrega = "JANDAIA"
            });

            mdfe.InfMDFe.Ide.InfMunCarrega.Add(new MDFeInfMunCarrega
            {
                CMunCarrega = "5209952",
                XMunCarrega = "INDIARA"
            });

            mdfe.InfMDFe.Ide.InfMunCarrega.Add(new MDFeInfMunCarrega
            {
                CMunCarrega = "5200134",
                XMunCarrega = "ACREUNA"
            });

            #endregion (ide)

            #region dados emitente (emit)
            mdfe.InfMDFe.Emit.CNPJ = config.Empresa.Cnpj;
            mdfe.InfMDFe.Emit.IE = config.Empresa.InscricaoEstadual;
            mdfe.InfMDFe.Emit.XNome = config.Empresa.Nome;
            mdfe.InfMDFe.Emit.XFant = config.Empresa.NomeFantasia;

            mdfe.InfMDFe.Emit.EnderEmit.XLgr = config.Empresa.Logradouro;
            mdfe.InfMDFe.Emit.EnderEmit.Nro = config.Empresa.Numero;
            mdfe.InfMDFe.Emit.EnderEmit.XCpl = config.Empresa.Complemento;
            mdfe.InfMDFe.Emit.EnderEmit.XBairro = config.Empresa.Bairro;
            mdfe.InfMDFe.Emit.EnderEmit.CMun = config.Empresa.CodigoIbgeMunicipio;
            mdfe.InfMDFe.Emit.EnderEmit.XMun = config.Empresa.NomeMunicipio;
            mdfe.InfMDFe.Emit.EnderEmit.CEP = long.Parse(config.Empresa.Cep);
            mdfe.InfMDFe.Emit.EnderEmit.UF = config.Empresa.SiglaUf;
            mdfe.InfMDFe.Emit.EnderEmit.Fone = config.Empresa.Telefone;
            mdfe.InfMDFe.Emit.EnderEmit.Email = config.Empresa.Email;
            #endregion dados emitente (emit)

            #region modal
            if (MDFeConfiguracao.VersaoWebService.VersaoLayout == VersaoServico.Versao100)
            {
                mdfe.InfMDFe.InfModal.Modal = new MDFeRodo
                {
                    RNTRC = config.Empresa.RNTRC,
                    VeicTracao = new MDFeVeicTracao
                    {
                        Placa = "KKK9888",
                        RENAVAM = "888888888",
                        UF = Estado.GO,
                        Tara = 222,
                        CapM3 = 222,
                        CapKG = 22,
                        Condutor = new List<MDFeCondutor>
                    {
                        new MDFeCondutor
                        {
                            CPF = "11392381754",
                            XNome = "Ricardão"
                        }
                    },
                        TpRod = MDFeTpRod.Outros,
                        TpCar = MDFeTpCar.NaoAplicavel
                    }
                };
            }


            if (MDFeConfiguracao.VersaoWebService.VersaoLayout == VersaoServico.Versao300)
            {
                mdfe.InfMDFe.InfModal.Modal = new MDFeRodo
                {
                    infANTT = new MDFeInfANTT
                    {
                        RNTRC = config.Empresa.RNTRC,

                        // não é obrigatorio
                        infCIOT = new List<infCIOT>
                        {
                            new infCIOT
                            {
                                CIOT = "123456789123",
                                CNPJ = "21025760000123"
                            }
                        }
                        /*valePed = new MDFeValePed
                        {
                            Disp = new List<MDFeDisp>
                                    {
                                        new MDFeDisp
                                        {
                                            CNPJForn = "21025760000123",
                                            CNPJPg = "21025760000123",
                                            NCompra = "838388383",
                                            vValePed = 100.33m
                                        }
                                    }
                        }*/
                    },

                    VeicTracao = new MDFeVeicTracao
                    {
                        Placa = "KKK9888",
                        RENAVAM = "888888888",
                        UF = Estado.GO,
                        Tara = 222,
                        CapM3 = 222,
                        CapKG = 22,
                        Condutor = new List<MDFeCondutor>
                        {
                            new MDFeCondutor
                            {
                                CPF = "11392381754",
                                XNome = "Ricardão"
                            }
                        },
                        TpRod = MDFeTpRod.Outros,
                        TpCar = MDFeTpCar.NaoAplicavel
                    },

                    lacRodo = new List<MDFeLacre>
                    {
                        new MDFeLacre
                        {
                            NLacre = "lacre01"
                        }
                    }
                };
            }

            #endregion modal

            #region infMunDescarga
            mdfe.InfMDFe.InfDoc.InfMunDescarga = new List<MDFeInfMunDescarga>
            {
                new MDFeInfMunDescarga
                {
                    XMunDescarga = "CUIABA",
                    CMunDescarga = "5103403",
                    InfCTe = new List<MDFeInfCTe>
                    {
                        new MDFeInfCTe
                        {
                            ChCTe = "52161021351378000100577500000000191194518006"
                        }
                    }
                }
            };


            if (MDFeConfiguracao.VersaoWebService.VersaoLayout == VersaoServico.Versao300)
            {
                mdfe.InfMDFe.InfDoc.InfMunDescarga[0].InfCTe[0].Peri = new List<MDFePeri>
                {
                    new MDFePeri
                    {
                        NONU = "1111",
                        QTotProd = "quantidade 20"
                    }
                };
            }

            #endregion infMunDescarga

            #region seg

            if (MDFeConfiguracao.VersaoWebService.VersaoLayout == VersaoServico.Versao300)
            {
                mdfe.InfMDFe.Seg = new List<MDFeSeg>();

                mdfe.InfMDFe.Seg.Add(new MDFeSeg
                {
                    InfResp = new MDFeInfResp
                    {
                        CNPJ = "21025760000123",
                        RespSeg = MDFeRespSeg.EmitenteDoMDFe
                    },
                    InfSeg = new MDFeInfSeg
                    {
                        CNPJ = "21025760000123",
                        XSeg = "aaaaaaaaaa"
                    },
                    NApol = "aaaaaaaaaa",
                    NAver = new List<string>
                        {
                            "aaaaaaaa"
                        }
                });
            }

            #endregion

            #region Produto Predominante

            if (MDFeConfiguracao.VersaoWebService.VersaoLayout == VersaoServico.Versao300)
            {
                mdfe.InfMDFe.prodPred = new prodPred
                {
                    tpCarga = tpCarga.CargaGeral,
                    xProd = "aaaaaaaaaaaaaaaaaaaaa",
                    infLotacao = new infLotacao
                    {
                        infLocalCarrega = new infLocalCarrega
                        {
                            CEP = "75950000"
                        },
                        infLocalDescarrega = new infLocalDescarrega
                        {
                            CEP = "75950000"
                        }
                    }
                };
            }

            #endregion

            #region Totais (tot)
            mdfe.InfMDFe.Tot.QCTe = 1;
            mdfe.InfMDFe.Tot.vCarga = 500.00m;
            mdfe.InfMDFe.Tot.CUnid = MDFeCUnid.KG;
            mdfe.InfMDFe.Tot.QCarga = 100.0000m;
            #endregion Totais (tot)

            #region informações adicionais (infAdic)
            mdfe.InfMDFe.InfAdic = new MDFeInfAdic
            {
                InfCpl = "aaaaaaaaaaaaaaaa"
            };
            #endregion

            #region dados responsavel tecnico 

            mdfe.InfMDFe.infRespTec = new infRespTec
            {
                CNPJ = "21025760000123",
                email = "robertoalvespereira18@gmail.com",
                fone = "64981081602",
                xContato = "roberto alves"
            };
            #endregion  

            var servicoRecepcao = new ServicoMDFeRecepcao();

            var retornoEnvio = servicoRecepcao.MDFeRecepcao(1, mdfe);

            OnSucessoSync(new RetornoEEnvio(retornoEnvio));

            config.ConfigWebService.Numeracao++;
            new ConfiguracaoDao().SalvarConfiguracao(config);
        }

        private static int GetRandom()
        {
            var rand = new Random();
            return rand.Next(11111111, 99999999);
        }

        public void BuscarDiretorioSchema()
        {
            var dlg = new FolderBrowserDialog();
            dlg.ShowDialog();
            DiretorioSchemas = dlg.SelectedPath;
        }

        public void GerarESalvar()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            CarregarConfiguracoesMDFe(config);
            var mdfe = new MDFeEletronico();

            #region (ide)
            mdfe.InfMDFe.Ide.CUF = config.ConfigWebService.UfEmitente;
            mdfe.InfMDFe.Ide.TpAmb = config.ConfigWebService.Ambiente;
            mdfe.InfMDFe.Ide.TpEmit = MDFeTipoEmitente.PrestadorServicoDeTransporte;
            mdfe.InfMDFe.Ide.Mod = ModeloDocumento.MDFe;
            mdfe.InfMDFe.Ide.Serie = 751;
            mdfe.InfMDFe.Ide.NMDF = ++config.ConfigWebService.Numeracao;
            mdfe.InfMDFe.Ide.CMDF = GetRandom();
            mdfe.InfMDFe.Ide.Modal = MDFeModal.Rodoviario;
            mdfe.InfMDFe.Ide.DhEmi = DateTime.Now;
            mdfe.InfMDFe.Ide.TpEmis = MDFeTipoEmissao.Normal;
            mdfe.InfMDFe.Ide.ProcEmi = MDFeIdentificacaoProcessoEmissao.EmissaoComAplicativoContribuinte;
            mdfe.InfMDFe.Ide.VerProc = "versao28383";
            mdfe.InfMDFe.Ide.UFIni = Estado.GO;
            mdfe.InfMDFe.Ide.UFFim = Estado.MT;


            mdfe.InfMDFe.Ide.InfMunCarrega.Add(new MDFeInfMunCarrega
            {
                CMunCarrega = "5211701",
                XMunCarrega = "JANDAIA"
            });

            mdfe.InfMDFe.Ide.InfMunCarrega.Add(new MDFeInfMunCarrega
            {
                CMunCarrega = "5209952",
                XMunCarrega = "INDIARA"
            });

            mdfe.InfMDFe.Ide.InfMunCarrega.Add(new MDFeInfMunCarrega
            {
                CMunCarrega = "5200134",
                XMunCarrega = "ACREUNA"
            });

            #endregion (ide)

            #region dados emitente (emit)
            mdfe.InfMDFe.Emit.CNPJ = config.Empresa.Cnpj;
            mdfe.InfMDFe.Emit.IE = config.Empresa.InscricaoEstadual;
            mdfe.InfMDFe.Emit.XNome = config.Empresa.Nome;
            mdfe.InfMDFe.Emit.XFant = config.Empresa.NomeFantasia;

            mdfe.InfMDFe.Emit.EnderEmit.XLgr = config.Empresa.Logradouro;
            mdfe.InfMDFe.Emit.EnderEmit.Nro = config.Empresa.Numero;
            mdfe.InfMDFe.Emit.EnderEmit.XCpl = config.Empresa.Complemento;
            mdfe.InfMDFe.Emit.EnderEmit.XBairro = config.Empresa.Bairro;
            mdfe.InfMDFe.Emit.EnderEmit.CMun = config.Empresa.CodigoIbgeMunicipio;
            mdfe.InfMDFe.Emit.EnderEmit.XMun = config.Empresa.NomeMunicipio;
            mdfe.InfMDFe.Emit.EnderEmit.CEP = long.Parse(config.Empresa.Cep);
            mdfe.InfMDFe.Emit.EnderEmit.UF = config.Empresa.SiglaUf;
            mdfe.InfMDFe.Emit.EnderEmit.Fone = config.Empresa.Telefone;
            mdfe.InfMDFe.Emit.EnderEmit.Email = config.Empresa.Email;
            #endregion dados emitente (emit)

            #region modal
            if (MDFeConfiguracao.VersaoWebService.VersaoLayout == VersaoServico.Versao100)
            {
                mdfe.InfMDFe.InfModal.VersaoModal = MDFeVersaoModal.Versao100;
                mdfe.InfMDFe.InfModal.Modal = new MDFeRodo
                {
                    RNTRC = config.Empresa.RNTRC,
                    VeicTracao = new MDFeVeicTracao
                    {
                        Placa = "KKK9888",
                        RENAVAM = "888888888",
                        UF = Estado.GO,
                        Tara = 222,
                        CapM3 = 222,
                        CapKG = 22,
                        Condutor = new List<MDFeCondutor>
                    {
                        new MDFeCondutor
                        {
                            CPF = "11392381754",
                            XNome = "Ricardão"
                        }
                    },
                        TpRod = MDFeTpRod.Outros,
                        TpCar = MDFeTpCar.NaoAplicavel
                    }
                };
            }


            if (MDFeConfiguracao.VersaoWebService.VersaoLayout == VersaoServico.Versao300)
            {
                mdfe.InfMDFe.InfModal.VersaoModal = MDFeVersaoModal.Versao300;
                mdfe.InfMDFe.InfModal.Modal = new MDFeRodo
                {
                    infANTT = new MDFeInfANTT
                    {
                        RNTRC = config.Empresa.RNTRC,

                        // não é obrigatorio
                        infCIOT = new List<infCIOT>
                        {
                            new infCIOT
                            {
                                CIOT = "123456789123",
                                CNPJ = "21025760000123"
                            }
                        },
                        valePed = new MDFeValePed
                        {
                            Disp = new List<MDFeDisp>
                                    {
                                        new MDFeDisp
                                        {
                                            CNPJForn = "21025760000123",
                                            CNPJPg = "21025760000123",
                                            NCompra = "838388383",
                                            vValePed = 100.33m
                                        }
                                    }
                        }
                    },

                    VeicTracao = new MDFeVeicTracao
                    {
                        Placa = "KKK9888",
                        RENAVAM = "888888888",
                        UF = Estado.GO,
                        Tara = 222,
                        CapM3 = 222,
                        CapKG = 22,
                        Condutor = new List<MDFeCondutor>
                        {
                            new MDFeCondutor
                            {
                                CPF = "11392381754",
                                XNome = "Ricardão"
                            }
                        },
                        TpRod = MDFeTpRod.Outros,
                        TpCar = MDFeTpCar.NaoAplicavel
                    },

                    lacRodo = new List<MDFeLacre>
                    {
                        new MDFeLacre
                        {
                            NLacre = "lacre01"
                        }
                    }

                };
            }

            #endregion modal

            #region infMunDescarga
            mdfe.InfMDFe.InfDoc.InfMunDescarga = new List<MDFeInfMunDescarga>
            {
                new MDFeInfMunDescarga
                {
                    XMunDescarga = "CUIABA",
                    CMunDescarga = "5103403",
                    InfCTe = new List<MDFeInfCTe>
                    {
                        new MDFeInfCTe
                        {
                            ChCTe = "52161021351378000100577500000000191194518006"
                        }
                    }
                }
            };


            if (MDFeConfiguracao.VersaoWebService.VersaoLayout == VersaoServico.Versao300)
            {
                mdfe.InfMDFe.InfDoc.InfMunDescarga[0].InfCTe[0].Peri = new List<MDFePeri>
                {
                    new MDFePeri
                    {
                        NONU = "1111",
                        QTotProd = "quantidade 20"
                    }
                };
            }

            #endregion infMunDescarga

            #region seg

            if (MDFeConfiguracao.VersaoWebService.VersaoLayout == VersaoServico.Versao300)
            {
                mdfe.InfMDFe.Seg = new List<MDFeSeg>();

                mdfe.InfMDFe.Seg.Add(new MDFeSeg
                {
                    InfResp = new MDFeInfResp
                    {
                        CNPJ = "21025760000123",
                        RespSeg = MDFeRespSeg.EmitenteDoMDFe
                    },
                    InfSeg = new MDFeInfSeg
                    {
                        CNPJ = "21025760000123",
                        XSeg = "aaaaaaaaaa"
                    },
                    NApol = "aaaaaaaaaa",
                    NAver = new List<string>
                        {
                            "aaaaaaaa"
                        }
                });
            }

            #endregion

            #region Totais (tot)
            mdfe.InfMDFe.Tot.QCTe = 1;
            mdfe.InfMDFe.Tot.vCarga = 500.00m;
            mdfe.InfMDFe.Tot.CUnid = MDFeCUnid.KG;
            mdfe.InfMDFe.Tot.QCarga = 100.0000m;
            #endregion Totais (tot)

            #region informações adicionais (infAdic)
            mdfe.InfMDFe.InfAdic = new MDFeInfAdic
            {
                InfCpl = "aaaaaaaaaaaaaaaa"
            };
            #endregion

            mdfe = mdfe.Assina();
            mdfe = mdfe.Valida();

            mdfe.SalvarXmlEmDisco();
        }

        public void BuscarDiretorioSalvarXml()
        {
            var dlg = new FolderBrowserDialog();
            dlg.ShowDialog();
            DiretorioSalvarXml = dlg.SelectedPath;
        }

        public void ConsultaPorRecibo()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            CarregarConfiguracoesMDFe(config);

            var recibo = InputBoxTuche("Digite o recibo");

            if (string.IsNullOrEmpty(recibo))
            {
                MessageBoxTuche("Recibo inválido");
                return;
            }

            var servicoRecibo = new ServicoMDFeRetRecepcao();
            var retorno = servicoRecibo.MDFeRetRecepcao(recibo);

            OnSucessoSync(new RetornoEEnvio(retorno));
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
                chave = BuscarChaveMDFe();
            }

            if (string.IsNullOrEmpty(chave))
            {
                MessageBoxTuche("Ops.. Não a oque fazer sem uma chave de acesso");
                return;
            }

            var config = new ConfiguracaoDao().BuscarConfiguracao();
            CarregarConfiguracoesMDFe(config);

            var servicoConsultaProtocolo = new ServicoMDFeConsultaProtocolo();
            var retorno = servicoConsultaProtocolo.MDFeConsultaProtocolo(chave);


            OnSucessoSync(new RetornoEEnvio(retorno));
        }

        private string BuscarChaveMDFe()
        {
            var chave = string.Empty;
            var caminhoArquivoXml = BuscarArquivoXmlMDFe();

            if (caminhoArquivoXml != null)
            {
                try
                {
                    var enviMDFe = MDFeEnviMDFe.LoadXmlArquivo(caminhoArquivoXml);

                    chave = enviMDFe.MDFe.Chave();
                }
                catch
                {
                    try
                    {
                        chave = MDFeEletronico.LoadXmlArquivo(caminhoArquivoXml).Chave();
                    }
                    catch
                    {
                        var proc = FuncoesXml.ArquivoXmlParaClasse<MDFeProcMDFe>(caminhoArquivoXml);
                        chave = proc.MDFe.Chave();
                    }
                }

            }
            return chave;
        }

        public string BuscarArquivoXmlMDFe()
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
            return MessageBox.Show(mensagem, @"MDF-e", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        private static void MessageBoxTuche(string mensagem, MessageBoxIcon icon = MessageBoxIcon.Information)
        {
            MessageBox.Show(mensagem, @"MDF-e", MessageBoxButtons.OK, icon);
        }

        public void ConsultaStatusServico()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            CarregarConfiguracoesMDFe(config);

            var servicoStatusServico = new ServicoMDFeStatusServico();
            var retorno = servicoStatusServico.MDFeStatusServico();

            OnSucessoSync(new RetornoEEnvio(retorno));

        }

        public void ConsultaNaoEncerrados()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            CarregarConfiguracoesMDFe(config);

            var servicoConsultaNaoEncerrados = new ServicoMDFeConsultaNaoEncerrados();
            var retorno = servicoConsultaNaoEncerrados.MDFeConsultaNaoEncerrados(config.Empresa.Cnpj);

            OnSucessoSync(new RetornoEEnvio(retorno));
        }

        public void EventoIncluirCondutor()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            CarregarConfiguracoesMDFe(config);


            var evento = new ServicoMDFeEvento();

            MDFeEletronico mdfe;
            var caminhoXml = BuscarArquivoXmlMDFe();

            try
            {
                var enviMDFe = MDFeEnviMDFe.LoadXmlArquivo(caminhoXml);

                mdfe = enviMDFe.MDFe;
            }
            catch
            {
                try
                {
                    mdfe = MDFeEletronico.LoadXmlArquivo(caminhoXml);
                }
                catch
                {
                    var proc = FuncoesXml.ArquivoXmlParaClasse<MDFeProcMDFe>(caminhoXml);
                    mdfe = proc.MDFe;
                }
            }

            var nomeCondutor = InputBoxTuche("Nome condutor");
            var cpfCondutor = InputBoxTuche("Cpf condutor");

            if (string.IsNullOrEmpty(nomeCondutor))
            {
                MessageBoxTuche("Nome do condutor não pode ser vazio ou nulo");
                return;
            }

            if (string.IsNullOrEmpty(cpfCondutor))
            {
                MessageBoxTuche("CPF do condutor não pode ser vazio ou nulo");
                return;
            }

            var retorno = evento.MDFeEventoIncluirCondutor(mdfe, 1, nomeCondutor, cpfCondutor);

            OnSucessoSync(new RetornoEEnvio(retorno));
        }

        public void EventoEncerramento()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            CarregarConfiguracoesMDFe(config);

            MDFeEletronico mdfe;
            var caminhoXml = BuscarArquivoXmlMDFe();

            try
            {
                var enviMDFe = MDFeEnviMDFe.LoadXmlArquivo(caminhoXml);

                mdfe = enviMDFe.MDFe;
            }
            catch
            {
                try
                {
                    mdfe = MDFeEletronico.LoadXmlArquivo(caminhoXml);
                }
                catch
                {
                    var proc = FuncoesXml.ArquivoXmlParaClasse<MDFeProcMDFe>(caminhoXml);
                    mdfe = proc.MDFe;
                }
            }

            var evento = new ServicoMDFeEvento();

            var protocolo = InputBoxTuche("Digite um protocolo");

            if (string.IsNullOrEmpty(protocolo))
            {
                MessageBoxTuche("O protocolo não pode ser vazio ou nulo");
                return;
            }

            var retorno = evento.MDFeEventoEncerramentoMDFeEventoEncerramento(mdfe, 1, protocolo);

            OnSucessoSync(new RetornoEEnvio(retorno));
        }

        public void EventoCancelar()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            CarregarConfiguracoesMDFe(config);

            var evento = new ServicoMDFeEvento();

            MDFeEletronico mdfe;
            var caminhoXml = BuscarArquivoXmlMDFe();

            try
            {
                var enviMDFe = MDFeEnviMDFe.LoadXmlArquivo(caminhoXml);

                mdfe = enviMDFe.MDFe;
            }
            catch
            {
                try
                {
                    mdfe = MDFeEletronico.LoadXmlArquivo(caminhoXml);
                }
                catch
                {
                    var proc = FuncoesXml.ArquivoXmlParaClasse<MDFeProcMDFe>(caminhoXml);
                    mdfe = proc.MDFe;
                }
            }

            var protocolo = InputBoxTuche("Digite um protocolo");

            if (string.IsNullOrEmpty(protocolo))
            {
                MessageBoxTuche("O protocolo não pode ser vazio ou nulo");
                return;
            }

            var justificativa = InputBoxTuche("Digite uma justificativa (minimo 15 digitos)");

            if (string.IsNullOrEmpty(justificativa))
            {
                MessageBoxTuche("A justificativa não pode ser vazio ou nulo");
                return;
            }

            var retorno = evento.MDFeEventoCancelar(mdfe, 1, protocolo, justificativa);

            OnSucessoSync(new RetornoEEnvio(retorno));
        }

        private static void CarregarConfiguracoesMDFe(Configuracao config)
        {
            var configuracaoCertificado = new ConfiguracaoCertificado
            {
                TipoCertificado = TipoCertificado.A1Repositorio,
                Serial = config.CertificadoDigital.NumeroDeSerie,
                Senha = config.CertificadoDigital.Senha,
                //Arquivo = config.CertificadoDigital.CaminhoArquivo,
                ManterDadosEmCache = config.CertificadoDigital.ManterEmCache,
            };

            MDFeConfiguracao.ConfiguracaoCertificado = configuracaoCertificado;
            MDFeConfiguracao.CaminhoSchemas = config.ConfigWebService.CaminhoSchemas;
            MDFeConfiguracao.CaminhoSalvarXml = config.DiretorioSalvarXml;
            MDFeConfiguracao.IsSalvarXml = config.IsSalvarXml;

            MDFeConfiguracao.VersaoWebService.VersaoLayout = config.ConfigWebService.VersaoLayout;

            MDFeConfiguracao.VersaoWebService.TipoAmbiente = config.ConfigWebService.Ambiente;
            MDFeConfiguracao.VersaoWebService.UfEmitente = config.ConfigWebService.UfEmitente;
            MDFeConfiguracao.VersaoWebService.TimeOut = config.ConfigWebService.TimeOut;
            MDFeConfiguracao.IsAdicionaQrCode = true;
        }

        protected virtual void OnSucessoSync(RetornoEEnvio e)
        {
            if (SucessoSync == null) return;

            SucessoSync.Invoke(this, e);
        }

        public void EventoIncluirDFe()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            CarregarConfiguracoesMDFe(config);

            var evento = new ServicoMDFeEvento();
            MDFeEletronico mdfe;
            var caminhoXml = BuscarArquivoXmlMDFe();
            try
            {
                var enviMDFe = MDFeEnviMDFe.LoadXmlArquivo(caminhoXml);
                mdfe = enviMDFe.MDFe;
            }
            catch
            {
                try
                {
                    mdfe = MDFeEletronico.LoadXmlArquivo(caminhoXml);
                }
                catch
                {
                    var proc = FuncoesXml.ArquivoXmlParaClasse<MDFeProcMDFe>(caminhoXml);
                    mdfe = proc.MDFe;
                }
            }
            var protocolo = InputBoxTuche("Protocolo");
            var codigoMunicipioCarregamento = InputBoxTuche("Código do Município de Carregamento");
            var nomeMunicipioCarregamento = InputBoxTuche("Nome do Município de Carregamento");
            var cmunDescarga = InputBoxTuche("Código do Município de Descarga");
            var xmunDescarga = InputBoxTuche("Nome do Município de Descarga");
            var chNFe = InputBoxTuche("Chave da NFe");
            if (string.IsNullOrEmpty(codigoMunicipioCarregamento))
            {
                MessageBoxTuche("Código do Município de Carregamento não pode ser vazio ou nulo");
                return;
            }
            if (string.IsNullOrEmpty(nomeMunicipioCarregamento))
            {
                MessageBoxTuche("Nome do Município de Carregamento não pode ser vazio ou nulo");
                return;
            }
            if (string.IsNullOrEmpty(cmunDescarga))
            {
                MessageBoxTuche("Nome do Município de Descarga não pode ser vazio ou nulo");
                return;
            }
            if (string.IsNullOrEmpty(xmunDescarga))
            {
                MessageBoxTuche("Nome do Município de Descarga não pode ser vazio ou nulo");
                return;
            }
            if (string.IsNullOrEmpty(chNFe))
            {
                MessageBoxTuche("Chave NFe não pode ser vazio ou nulo");
                return;
            }
            var informacoesDocumentos = new List<MDFeInfDocInc>
            {
                new MDFeInfDocInc
                {
                    CMunDescarga = cmunDescarga,
                    XMunDescarga = xmunDescarga,
                    ChNFe = chNFe
                }
            };
            var retorno = evento.MDFeEventoIncluirDFe(mdfe, 1, protocolo, codigoMunicipioCarregamento, nomeMunicipioCarregamento, informacoesDocumentos);
            OnSucessoSync(new RetornoEEnvio(retorno));
        }
    }
}
