using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using DFe.Utils;
using DFe.Utils.Assinatura;
using SMDFe.Classes.Flags;
using SMDFe.Classes.Informacoes;
using SMDFe.Classes.Retorno;
using SMDFe.Classes.Servicos.Autorizacao;
using SMDFe.Damdfe.Base;
//using SMDFe.Damdfe.Fast;
using SMDFe.Servicos.ConsultaNaoEncerradosMDFe;
using SMDFe.Servicos.ConsultaProtocoloMDFe;
using SMDFe.Servicos.EventosMDFe;
using SMDFe.Servicos.RecepcaoMDFe;
using SMDFe.Servicos.RetRecepcaoMDFe;
using SMDFe.Servicos.StatusServicoMDFe;
using SMDFe.Utils.Configuracoes;
using SMDFe.Utils.Flags;
using MDFeEletronico = SMDFe.Classes.Informacoes.MDFe;

namespace SMDFe.TestesServicos
{
    class Testes
    {
        MDFeProcMDFe _mdfeProc;
        ConfiguracaoDamdfe _confiDamdfe;
        private static ConfiguracaoCertificado _configuracaoCertificado;

        Configuracao _configuracao;

        static void Main(string[] args)
        {
            //_configuracaoCertificado = ObterConfiguracaoCertificadoPorArray();
            CarregarCertificado();
            //ConsultasNaoEnc(); //Okay
            //ConsultaStatus(); //Okay
            //CriarEnviar(); //OKay
            //ConsultaPorRecibo(); //Okay
            //ConsultaPorProtocolo(); //Okay
            //EventoIncluirCondutor(); //Okay
            //EventoEncerramento(); //Okay
            //EventoCancelar(); //Okay

            //ExportaRelatorio();


        }

        private static ConfiguracaoCertificado ObterConfiguracaoCertificadoPorArray()
        {


            var chave = new X509Certificate2(@"certificado.pfx", "zeus123", X509KeyStorageFlags.Exportable);
            var array = chave.Export(X509ContentType.Pfx);
            var config = new ConfiguracaoCertificado()
            {
                TipoCertificado = TipoCertificado.A1ByteArray,
                ArrayBytesArquivo = array,
                //TipoCertificado = TipoCertificado.A1Arquivo,
                //Arquivo = "Index_eCNPJ_29102018.pfx",
            };

            var chave2 = CertificadoDigital.ObterCertificado(config);
            File.WriteAllBytes("certificado.txt", array);
            return config;
        }

        private static void CarregarCertificado()
        {
            var array = File.ReadAllBytes("certificado.txt");
            var config = new ConfiguracaoCertificado()
            {
                TipoCertificado = TipoCertificado.A1ByteArray,
                ArrayBytesArquivo = array,
                //TipoCertificado = TipoCertificado.A1Arquivo,
                //Arquivo = "Index_eCNPJ_29102018.pfx",
            };

            var chave2 = CertificadoDigital.ObterCertificado(config);
            Console.ReadKey();

        }
/*
        private static void ExportaRelatorio()
        {
            var _confiDamdfe = new ConfiguracaoDamdfe()
            {
                Desenvolvedor = "NINGUEM",
                DocumentoCancelado = false,
                DocumentoEncerrado = true,
                QuebrarLinhasObservacao = true
            };


            var _mdfeProc = FuncoesXml.ArquivoXmlParaClasse<MDFeProcMDFe>(
                @"C:\Users\Usuario\DFe.NET\DFe.NET\SMDFe.TestesServicos\proc.xml");


            DamdfeFrMDFe rpt = new DamdfeFrMDFe(proc: _mdfeProc,
                config: _confiDamdfe,
                arquivoRelatorio: @"D:\Usuario\Desktop\SMDFeRetrato.frx");
            //Act
            rpt.ExportarHTML("report_teste.html");
        }
*/
        private static void ConsultasNaoEnc()
        {
            try
            {

                var config = new ConfiguracaoDao().BuscarConfiguracao();

                CarregarConfiguracoesMDFe(config, _configuracaoCertificado);

                var servicoConsultaNaoEncerrados = new ServicoMDFeConsultaNaoEncerrados();
                var retorno = servicoConsultaNaoEncerrados.MDFeConsultaNaoEncerrados(config.Empresa.Cnpj);


                Console.WriteLine(retorno.RetornoXmlString);
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }

        private static void ConsultaStatus()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            CarregarConfiguracoesMDFe(config, _configuracaoCertificado);

            var servicoStatusServico = new ServicoMDFeStatusServico();
            var retorno = servicoStatusServico.MDFeStatusServico();

            Console.WriteLine(retorno.RetornoXmlString);
            Console.ReadKey();


        }

        public static void CriarEnviar()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            CarregarConfiguracoesMDFe(config, _configuracaoCertificado);
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



            // Evento executado antes do envio da mdf-e para a sefaz
            //servicoRecepcao.AntesDeEnviar += AntesEnviar;

            var servicoRecepcao = new ServicoMDFeRecepcao();

            var retornoEnvio = servicoRecepcao.MDFeRecepcao(1, mdfe);

            Console.WriteLine(retornoEnvio.RetornoXmlString);
            config.ConfigWebService.Numeracao++;
            Console.ReadKey();
            new ConfiguracaoDao().SalvarConfiguracao(config);
        }

        public static void ConsultaPorRecibo()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            CarregarConfiguracoesMDFe(config, _configuracaoCertificado);

            var recibo = "289000006323157";

            var servicoRecibo = new ServicoMDFeRetRecepcao();
            var retorno = servicoRecibo.MDFeRetRecepcao(recibo);

            Console.WriteLine(retorno.RetornoXmlString);
            Console.ReadKey();
        }

        public static void ConsultaPorProtocolo()
        {

            var chave = "28181007703290000189587500000000021826712210";

            var config = new ConfiguracaoDao().BuscarConfiguracao();
            CarregarConfiguracoesMDFe(config, _configuracaoCertificado);

            var servicoConsultaProtocolo = new ServicoMDFeConsultaProtocolo();
            var retorno = servicoConsultaProtocolo.MDFeConsultaProtocolo(chave);

            Console.WriteLine(retorno.RetornoXmlString);
            Console.ReadKey();

        }

        public static void EventoIncluirCondutor()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            CarregarConfiguracoesMDFe(config, _configuracaoCertificado);


            var evento = new ServicoMDFeEvento();

            MDFeEletronico mdfe;
            var caminhoXml =
                "C://Users//Usuario//DFe.NET//DFe.NET//SMDFe.TestesServicos//bin//Debug//netcoreapp2.1//28181007703290000189587500000000061225992170-mdfe.xml";

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

            var nomeCondutor = "Ojuara Abacarajucaiba";
            var cpfCondutor = "00011122233";

            var retorno = evento.MDFeEventoIncluirCondutor(mdfe, 1, nomeCondutor, cpfCondutor);
            Console.WriteLine(retorno.RetornoXmlString);
            Console.ReadKey();


        }

        public static void EventoEncerramento()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            CarregarConfiguracoesMDFe(config, _configuracaoCertificado);

            MDFeEletronico mdfe;
            var caminhoXml =
                "C://Users//Usuario//DFe.NET//DFe.NET//SMDFe.TestesServicos//bin//Debug//netcoreapp2.1//28181007703290000189587500000000061225992170-mdfe.xml";

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

            var protocolo = "28181007703290000189587500000000021826712210";


            var retorno = evento.MDFeEventoEncerramentoMDFeEventoEncerramento(mdfe, 1, protocolo);
            Console.WriteLine(retorno.RetornoXmlString);
            Console.ReadKey();

        }

        public static void EventoCancelar()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            CarregarConfiguracoesMDFe(config, _configuracaoCertificado);

            var evento = new ServicoMDFeEvento();

            MDFeEletronico mdfe;
            var caminhoXml =
                "C://Users//Usuario//DFe.NET//DFe.NET//SMDFe.TestesServicos//bin//Debug//netcoreapp2.1//28181007703290000189587500000000061225992170-mdfe.xml";

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

            var protocolo = "28181007703290000189587500000000021826712210";

            if (string.IsNullOrEmpty(protocolo))
            {
                return;
            }

            var justificativa = "Problemas na codificação";

            if (string.IsNullOrEmpty(justificativa))
            {
                return;
            }

            var retorno = evento.MDFeEventoCancelar(mdfe, 1, protocolo, justificativa);

            Console.WriteLine(retorno.RetornoXmlString);
            Console.ReadKey();
        }

        private static int GetRandom()
        {
            var rand = new Random();
            return rand.Next(11111111, 99999999);
        }

        private static void CarregarConfiguracoesMDFe(Configuracao config, ConfiguracaoCertificado configuracaoCertificado)
        {
            //var configuracaoCertificado = new ConfiguracaoCertificado
            //{
            //    Senha = config.CertificadoDigital.Senha,
            //    Arquivo = config.CertificadoDigital.CaminhoArquivo,
            //    ManterDadosEmCache = config.CertificadoDigital.ManterEmCache,
            //    Serial = config.CertificadoDigital.NumeroDeSerie

            //};

            Utils.Configuracoes.MDFeConfiguracao.ConfiguracaoCertificado = configuracaoCertificado;
            Utils.Configuracoes.MDFeConfiguracao.CaminhoSchemas = config.ConfigWebService.CaminhoSchemas;
            Utils.Configuracoes.MDFeConfiguracao.CaminhoSalvarXml = config.DiretorioSalvarXml;
            Utils.Configuracoes.MDFeConfiguracao.IsSalvarXml = config.IsSalvarXml;

            Utils.Configuracoes.MDFeConfiguracao.VersaoWebService.VersaoLayout = config.ConfigWebService.VersaoLayout;

            Utils.Configuracoes.MDFeConfiguracao.VersaoWebService.TipoAmbiente = config.ConfigWebService.Ambiente;
            Utils.Configuracoes.MDFeConfiguracao.VersaoWebService.UfEmitente = config.ConfigWebService.UfEmitente;
            Utils.Configuracoes.MDFeConfiguracao.VersaoWebService.TimeOut = config.ConfigWebService.TimeOut;
        }
    }
}
