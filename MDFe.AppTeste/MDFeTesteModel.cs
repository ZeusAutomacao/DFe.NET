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
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DFe.CertificadosDigitais.Implementacao;
using DFe.DocumentosEletronicos.MDFe.Classes.Extensoes;
using DFe.DocumentosEletronicos.MDFe.Classes.Flags;
using DFe.DocumentosEletronicos.MDFe.Classes.Informacoes.Adicionais;
using DFe.DocumentosEletronicos.MDFe.Classes.Informacoes.DocumentosFiscais;
using DFe.DocumentosEletronicos.MDFe.Classes.Informacoes.Identificacao;
using DFe.DocumentosEletronicos.MDFe.Classes.Informacoes.Modal.Rodoviario;
using DFe.DocumentosEletronicos.MDFe.Classes.Informacoes.Seguro;
using DFe.DocumentosEletronicos.MDFe.Classes.Retorno;
using DFe.DocumentosEletronicos.MDFe.Facade;
using DFe.DocumentosEletronicos.MDFe.Servicos.ConsultaNaoEncerradosMDFe;
using DFe.DocumentosEletronicos.MDFe.Servicos.ConsultaProtocoloMDFe;
using DFe.DocumentosEletronicos.MDFe.Servicos.EventosMDFe;
using DFe.DocumentosEletronicos.MDFe.Servicos.RecepcaoMDFe;
using DFe.Entidades;
using DFe.Flags;
using DFe.ManipuladorDeXml;
using DFe.Utils;
using DFe.Utils.Assinatura;
using MDFe.AppTeste.Dao;
using MDFe.AppTeste.Entidades;
using MDFe.AppTeste.ModelBase;
using MDFeEletronico = DFe.DocumentosEletronicos.MDFe.Classes.Informacoes.MDFe;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

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
            var configuracaoAppTeste = new Configuracao {
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

            var configMdfe = CarregarConfiguracoesMDFe(config);
            var configCert = CarregarCertitifcado(config);

            var mdfe = new MDFeEletronico();

            #region (ide)
            mdfe.InfMDFe.ide.cUF = config.ConfigWebService.UfEmitente;
            mdfe.InfMDFe.ide.tpAmb = config.ConfigWebService.Ambiente;
            mdfe.InfMDFe.ide.tpEmit = tpEmit.PrestadorServicoDeTransporte;
            mdfe.InfMDFe.ide.mod = ModeloDocumento.MDFe;
            mdfe.InfMDFe.ide.serie = 750;
            mdfe.InfMDFe.ide.nMDF = ++config.ConfigWebService.Numeracao;
            mdfe.InfMDFe.ide.cMDF = GetRandom();
            mdfe.InfMDFe.ide.modal = modal.Rodoviario;
            mdfe.InfMDFe.ide.dhEmi = DateTime.Now;
            mdfe.InfMDFe.ide.tpEmis = tpEmis.Normal;
            mdfe.InfMDFe.ide.procEmi = procEmi.EmissaoComAplicativoContribuinte;
            mdfe.InfMDFe.ide.verProc = "versao28383";
            mdfe.InfMDFe.ide.UFIni = Estado.GO;
            mdfe.InfMDFe.ide.UFFim = Estado.MT;


            mdfe.InfMDFe.ide.infMunCarrega.Add(new infMunCarrega
            {
                cMunCarrega = "5211701",
                xMunCarrega = "JANDAIA"
            });

            mdfe.InfMDFe.ide.infMunCarrega.Add(new infMunCarrega
            {
                cMunCarrega = "5209952",
                xMunCarrega = "INDIARA"
            });

            mdfe.InfMDFe.ide.infMunCarrega.Add(new infMunCarrega
            {
                cMunCarrega = "5200134",
                xMunCarrega = "ACREUNA"
            });

            #endregion (ide)

            #region dados emitente (emit)
            mdfe.InfMDFe.emit.CNPJ = config.Empresa.Cnpj;
            mdfe.InfMDFe.emit.IE = config.Empresa.InscricaoEstadual;
            mdfe.InfMDFe.emit.xNome = config.Empresa.Nome;
            mdfe.InfMDFe.emit.xFant = config.Empresa.NomeFantasia;

            mdfe.InfMDFe.emit.enderEmit.xLgr = config.Empresa.Logradouro;
            mdfe.InfMDFe.emit.enderEmit.nro = config.Empresa.Numero;
            mdfe.InfMDFe.emit.enderEmit.xCpl = config.Empresa.Complemento;
            mdfe.InfMDFe.emit.enderEmit.xBairro = config.Empresa.Bairro;
            mdfe.InfMDFe.emit.enderEmit.cMun = config.Empresa.CodigoIbgeMunicipio;
            mdfe.InfMDFe.emit.enderEmit.xMun = config.Empresa.NomeMunicipio;
            mdfe.InfMDFe.emit.enderEmit.CEP = long.Parse(config.Empresa.Cep);
            mdfe.InfMDFe.emit.enderEmit.UF = config.Empresa.SiglaUf;
            mdfe.InfMDFe.emit.enderEmit.fone = config.Empresa.Telefone;
            mdfe.InfMDFe.emit.enderEmit.email = config.Empresa.Email;
            #endregion dados emitente (emit)

            #region modal
            if (configMdfe.VersaoServico == VersaoServico.Versao100)
            {
                mdfe.InfMDFe.infModal.Modal = new rodo
                {
                    RNTRC = config.Empresa.RNTRC,
                    veicTracao = new veicTracao
                    {
                        placa = "KKK9888",
                        RENAVAM = "888888888",
                        UF = Estado.GO,
                        tara = 222,
                        capM3 = 222,
                        capKG = 22,
                        condutor = new List<condutor>
                    {
                        new condutor
                        {
                            CPF = "11392381754",
                            xNome = "Ricardão"
                        }
                    },
                        tpRod = tpRod.Outros,
                        tpCar = tpCar.NaoAplicavel
                    }
                };
            }


            if (configMdfe.VersaoServico == VersaoServico.Versao300)
            {
                mdfe.InfMDFe.infModal.Modal = new rodo
                {
                    infANTT = new infANTT
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
                        valePed = new valePed
                        {
                            disp = new List<disp>
                                    {
                                        new disp
                                        {
                                            CNPJForn = "21025760000123",
                                            CNPJPg = "21025760000123",
                                            nCompra = "838388383",
                                            vValePed = 100.33m
                                        }
                                    }
                        }
                    },

                    veicTracao = new veicTracao
                        {
                            placa = "KKK9888",
                            RENAVAM = "888888888",
                            UF = Estado.GO,
                            tara = 222,
                            capM3 = 222,
                            capKG = 22,
                            condutor = new List<condutor>
                        {
                            new condutor
                            {
                                CPF = "11392381754",
                                xNome = "Ricardão"
                            }
                        },
                            tpRod = tpRod.Outros,
                            tpCar = tpCar.NaoAplicavel
                        },

                    lacRodo = new List<lacRodo>
                    {
                        new lacRodo
                        {
                            nLacre = "lacre01"
                        }
                    }

                };
            }

            #endregion modal

            #region infMunDescarga
            mdfe.InfMDFe.infDoc.infMunDescarga = new List<infMunDescarga>
            {
                new infMunDescarga
                {
                    xMunDescarga = "CUIABA",
                    cMunDescarga = "5103403",
                    infCTe = new List<infCTe>
                    {
                        new infCTe
                        {
                            chCTe = "52161021351378000100577500000000191194518006"
                        }
                    }
                }
            };


            if (configMdfe.VersaoServico == VersaoServico.Versao300)
            {
                mdfe.InfMDFe.infDoc.infMunDescarga[0].infCTe[0].Peri = new List<peri>
                {
                    new peri
                    {
                        nONU = "1111",
                        qTotProd = "quantidade 20"
                    }
                };
            }

            #endregion infMunDescarga

            #region seg

            if (configMdfe.VersaoServico == VersaoServico.Versao300)
            {
                mdfe.InfMDFe.Seg = new List<seg>();

                mdfe.InfMDFe.Seg.Add(new seg
                {
                    infResp = new infResp
                    {
                        CNPJ = "21025760000123",
                        respSeg = respSeg.EmitenteDoMDFe
                    },
                    infSeg = new infSeg
                    {
                        CNPJ = "21025760000123",
                        xSeg = "aaaaaaaaaa"
                    },
                    nApol = "aaaaaaaaaa",
                    nAver = new List<string>
                        {
                            "aaaaaaaa"
                        }
                });
            }

            #endregion

            #region Totais (tot)
            mdfe.InfMDFe.tot.qCTe = 1;
            mdfe.InfMDFe.tot.vCarga = 500.00m;
            mdfe.InfMDFe.tot.cUnid = cUnid.KG;
            mdfe.InfMDFe.tot.qCarga = 100.0000m;
            #endregion Totais (tot)

            #region informações adicionais (infAdic)
            mdfe.InfMDFe.infAdic = new infAdic
            {
                infCpl = "aaaaaaaaaaaaaaaa"
            };
            #endregion

            var facade = new MDFeFacade(configMdfe, configCert);

            // Evento executado antes do envio da mdf-e para a sefaz
            // servicoRecepcao.AntesDeEnviar += AntesEnviar;

            var retornoEnvio = facade.EnviarLote(1, mdfe);

            OnSucessoSync(new RetornoEEnvio(retornoEnvio));

            config.ConfigWebService.Numeracao++;
            new ConfiguracaoDao().SalvarConfiguracao(config);
        }

        private void AntesEnviar(object sender, AntesDeEnviar e)
        {
            MessageBoxTuche(e.enviMdFe.MDFe.Chave());
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

            var configMdfe = CarregarConfiguracoesMDFe(config);
            var configCert = CarregarCertitifcado(config);

            var mdfe = new MDFeEletronico();

            #region (ide)
            mdfe.InfMDFe.ide.cUF = config.ConfigWebService.UfEmitente;
            mdfe.InfMDFe.ide.tpAmb = config.ConfigWebService.Ambiente;
            mdfe.InfMDFe.ide.tpEmit = tpEmit.PrestadorServicoDeTransporte;
            mdfe.InfMDFe.ide.mod = ModeloDocumento.MDFe;
            mdfe.InfMDFe.ide.serie = 751;
            mdfe.InfMDFe.ide.nMDF = ++config.ConfigWebService.Numeracao;
            mdfe.InfMDFe.ide.cMDF = GetRandom();
            mdfe.InfMDFe.ide.modal = modal.Rodoviario;
            mdfe.InfMDFe.ide.dhEmi = DateTime.Now;
            mdfe.InfMDFe.ide.tpEmis = tpEmis.Normal;
            mdfe.InfMDFe.ide.procEmi = procEmi.EmissaoComAplicativoContribuinte;
            mdfe.InfMDFe.ide.verProc = "versao28383";
            mdfe.InfMDFe.ide.UFIni = Estado.GO;
            mdfe.InfMDFe.ide.UFFim = Estado.MT;


            mdfe.InfMDFe.ide.infMunCarrega.Add(new infMunCarrega
            {
                cMunCarrega = "5211701",
                xMunCarrega = "JANDAIA"
            });

            mdfe.InfMDFe.ide.infMunCarrega.Add(new infMunCarrega
            {
                cMunCarrega = "5209952",
                xMunCarrega = "INDIARA"
            });

            mdfe.InfMDFe.ide.infMunCarrega.Add(new infMunCarrega
            {
                cMunCarrega = "5200134",
                xMunCarrega = "ACREUNA"
            });

            #endregion (ide)

            #region dados emitente (emit)
            mdfe.InfMDFe.emit.CNPJ = config.Empresa.Cnpj;
            mdfe.InfMDFe.emit.IE = config.Empresa.InscricaoEstadual;
            mdfe.InfMDFe.emit.xNome = config.Empresa.Nome;
            mdfe.InfMDFe.emit.xFant = config.Empresa.NomeFantasia;

            mdfe.InfMDFe.emit.enderEmit.xLgr = config.Empresa.Logradouro;
            mdfe.InfMDFe.emit.enderEmit.nro = config.Empresa.Numero;
            mdfe.InfMDFe.emit.enderEmit.xCpl = config.Empresa.Complemento;
            mdfe.InfMDFe.emit.enderEmit.xBairro = config.Empresa.Bairro;
            mdfe.InfMDFe.emit.enderEmit.cMun = config.Empresa.CodigoIbgeMunicipio;
            mdfe.InfMDFe.emit.enderEmit.xMun = config.Empresa.NomeMunicipio;
            mdfe.InfMDFe.emit.enderEmit.CEP = long.Parse(config.Empresa.Cep);
            mdfe.InfMDFe.emit.enderEmit.UF = config.Empresa.SiglaUf;
            mdfe.InfMDFe.emit.enderEmit.fone = config.Empresa.Telefone;
            mdfe.InfMDFe.emit.enderEmit.email = config.Empresa.Email;
            #endregion dados emitente (emit)

            #region modal
            if (configMdfe.VersaoServico == VersaoServico.Versao100)
            {
                mdfe.InfMDFe.infModal.versaoModal = versaoModal.Versao100;
                mdfe.InfMDFe.infModal.Modal = new rodo
                {
                    RNTRC = config.Empresa.RNTRC,
                    veicTracao = new veicTracao
                    {
                        placa = "KKK9888",
                        RENAVAM = "888888888",
                        UF = Estado.GO,
                        tara = 222,
                        capM3 = 222,
                        capKG = 22,
                        condutor = new List<condutor>
                    {
                        new condutor
                        {
                            CPF = "11392381754",
                            xNome = "Ricardão"
                        }
                    },
                        tpRod = tpRod.Outros,
                        tpCar = tpCar.NaoAplicavel
                    }
                };
            }


            if (configMdfe.VersaoServico == VersaoServico.Versao300)
            {
                mdfe.InfMDFe.infModal.versaoModal = versaoModal.Versao300;
                mdfe.InfMDFe.infModal.Modal = new rodo
                {
                    infANTT = new infANTT
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
                        valePed = new valePed
                        {
                            disp = new List<disp>
                                    {
                                        new disp
                                        {
                                            CNPJForn = "21025760000123",
                                            CNPJPg = "21025760000123",
                                            nCompra = "838388383",
                                            vValePed = 100.33m
                                        }
                                    }
                        }
                    },

                    veicTracao = new veicTracao
                    {
                        placa = "KKK9888",
                        RENAVAM = "888888888",
                        UF = Estado.GO,
                        tara = 222,
                        capM3 = 222,
                        capKG = 22,
                        condutor = new List<condutor>
                        {
                            new condutor
                            {
                                CPF = "11392381754",
                                xNome = "Ricardão"
                            }
                        },
                        tpRod = tpRod.Outros,
                        tpCar = tpCar.NaoAplicavel
                    },

                    lacRodo = new List<lacRodo>
                    {
                        new lacRodo
                        {
                            nLacre = "lacre01"
                        }
                    }

                };
            }

            #endregion modal

            #region infMunDescarga
            mdfe.InfMDFe.infDoc.infMunDescarga = new List<infMunDescarga>
            {
                new infMunDescarga
                {
                    xMunDescarga = "CUIABA",
                    cMunDescarga = "5103403",
                    infCTe = new List<infCTe>
                    {
                        new infCTe
                        {
                            chCTe = "52161021351378000100577500000000191194518006"
                        }
                    }
                }
            };


            if (configMdfe.VersaoServico == VersaoServico.Versao300)
            {
                mdfe.InfMDFe.infDoc.infMunDescarga[0].infCTe[0].Peri = new List<peri>
                {
                    new peri
                    {
                        nONU = "1111",
                        qTotProd = "quantidade 20"
                    }
                };
            }

            #endregion infMunDescarga

            #region seg

            if (configMdfe.VersaoServico == VersaoServico.Versao300)
            {
                mdfe.InfMDFe.Seg = new List<seg>();

                mdfe.InfMDFe.Seg.Add(new seg
                {
                    infResp = new infResp
                    {
                        CNPJ = "21025760000123",
                        respSeg = respSeg.EmitenteDoMDFe
                    },
                    infSeg = new infSeg
                    {
                        CNPJ = "21025760000123",
                        xSeg = "aaaaaaaaaa"
                    },
                    nApol = "aaaaaaaaaa",
                    nAver = new List<string>
                        {
                            "aaaaaaaa"
                        }
                });
            }

            #endregion

            #region Totais (tot)
            mdfe.InfMDFe.tot.qCTe = 1;
            mdfe.InfMDFe.tot.vCarga = 500.00m;
            mdfe.InfMDFe.tot.cUnid = cUnid.KG;
            mdfe.InfMDFe.tot.qCarga = 100.0000m;
            #endregion Totais (tot)

            #region informações adicionais (infAdic)
            mdfe.InfMDFe.infAdic = new infAdic
            {
                infCpl = "aaaaaaaaaaaaaaaa"
            };
            #endregion

            mdfe = mdfe.Assina(configMdfe, configCert);
            mdfe = mdfe.Valida(configMdfe);

            mdfe.SalvarXmlEmDisco(configMdfe);
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

            var configMdfe = CarregarConfiguracoesMDFe(config);
            var configCert = CarregarCertitifcado(config);

            var recibo = InputBoxTuche("Digite o recibo");

            if (string.IsNullOrEmpty(recibo))
            {
                MessageBoxTuche("Recibo inválido");
                return;
            }

            var facade = new MDFeFacade(configMdfe, configCert);
            var retorno = facade.ConsultaLote(recibo);

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

            var configMdfe = CarregarConfiguracoesMDFe(config);
            var configCert = CarregarCertitifcado(config);

            var facade = new MDFeFacade(configMdfe, configCert);
            var retorno = facade.Consulta(chave);


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
                    var enviMDFe = DFe.DocumentosEletronicos.MDFe.Classes.Servicos.Autorizacao.enviMDFe.LoadXmlArquivo(caminhoArquivoXml);

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
                        var proc = FuncoesXml.ArquivoXmlParaClasse<mdfeProc>(caminhoArquivoXml);
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
                TxtValor = {Text = string.Empty},
                TxtDescricao = {Text = titulo}
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

            var configMdfe = CarregarConfiguracoesMDFe(config);
            var configCert = CarregarCertitifcado(config);

            var facade = new MDFeFacade(configMdfe, configCert);

            var retorno = facade.StatusConsulta();

            OnSucessoSync(new RetornoEEnvio(retorno));

        }

        public void ConsultaNaoEncerrados()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();

            var configMdfe = CarregarConfiguracoesMDFe(config);
            var configCert = CarregarCertitifcado(config);

            var facade = new MDFeFacade(configMdfe, configCert);

            var retorno = facade.ConsultaNaoEncerradas(config.Empresa.Cnpj);

            OnSucessoSync(new RetornoEEnvio(retorno));
        }

        public void EventoIncluirCondutor()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            var configMdfe = CarregarConfiguracoesMDFe(config);
            var configCert = CarregarCertitifcado(config);


            var facade = new MDFeFacade(configMdfe, configCert);

            MDFeEletronico mdfe;
            var caminhoXml = BuscarArquivoXmlMDFe();

            try
            {
                var enviMDFe = DFe.DocumentosEletronicos.MDFe.Classes.Servicos.Autorizacao.enviMDFe.LoadXmlArquivo(caminhoXml);

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
                    var proc = FuncoesXml.ArquivoXmlParaClasse<mdfeProc>(caminhoXml);
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

            var retorno = facade.IncluirCondutor(mdfe.Chave(), mdfe.CNPJEmitente(), 1, nomeCondutor, cpfCondutor);

            OnSucessoSync(new RetornoEEnvio(retorno));
        }

        public void EventoEncerramento()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            var configMdfe = CarregarConfiguracoesMDFe(config);
            var configCert = CarregarCertitifcado(config);

            MDFeEletronico mdfe;
            var caminhoXml = BuscarArquivoXmlMDFe();

            try
            {
                var enviMDFe = DFe.DocumentosEletronicos.MDFe.Classes.Servicos.Autorizacao.enviMDFe.LoadXmlArquivo(caminhoXml);

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
                    var proc = FuncoesXml.ArquivoXmlParaClasse<mdfeProc>(caminhoXml);
                    mdfe = proc.MDFe;
                }
            }

            var facade = new MDFeFacade(configMdfe, configCert);

            var protocolo = InputBoxTuche("Digite um protocolo");

            if (string.IsNullOrEmpty(protocolo))
            {
                MessageBoxTuche("O protocolo não pode ser vazio ou nulo");
                return;
            }

            var retorno = facade.Encerrar(mdfe.Chave(), mdfe.CNPJEmitente(), mdfe.CodigoIbgeMunicipioEmitente(), 1, protocolo);

            OnSucessoSync(new RetornoEEnvio(retorno));
        }

        public void EventoCancelar()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            var configMdfe = CarregarConfiguracoesMDFe(config);
            var configCert = CarregarCertitifcado(config);

            var facade = new MDFeFacade(configMdfe, configCert);

            MDFeEletronico mdfe;
            var caminhoXml = BuscarArquivoXmlMDFe();

            try
            {
                var enviMDFe = DFe.DocumentosEletronicos.MDFe.Classes.Servicos.Autorizacao.enviMDFe.LoadXmlArquivo(caminhoXml);

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
                    var proc = FuncoesXml.ArquivoXmlParaClasse<mdfeProc>(caminhoXml);
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

            var retorno = facade.Cancelar(mdfe.Chave(), mdfe.CNPJEmitente(), 1, protocolo, justificativa);

            OnSucessoSync(new RetornoEEnvio(retorno));
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

        private static MDFeConfig CarregarConfiguracoesMDFe(Configuracao config)
        {
            var mdfeConfig = new MDFeConfig
            {
                CaminhoSchemas = config.ConfigWebService.CaminhoSchemas,
                CaminhoSalvarXml = config.DiretorioSalvarXml,
                IsSalvarXml = config.IsSalvarXml,
                VersaoServico = config.ConfigWebService.VersaoLayout,
                TipoAmbiente = config.ConfigWebService.Ambiente,
                EstadoUf = config.ConfigWebService.UfEmitente,
                TimeOut = config.ConfigWebService.TimeOut,
                CnpjEmitente = config.Empresa.Cnpj,
                IsEfetuarCacheCertificadoDigital = true
            };




            return mdfeConfig;
        }

        protected virtual void OnSucessoSync(RetornoEEnvio e)
        {
            if (SucessoSync == null) return;

            SucessoSync.Invoke(this, e);
        }
    }
}
