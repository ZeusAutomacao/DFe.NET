using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using DFe.Classes.Entidades;
using DFe.Classes.Extensoes;
using DFe.Classes.Flags;
using DFe.Utils;
using SMDFe.Classes.Extencoes;
using SMDFe.Classes.Flags;
using SMDFe.Classes.Informacoes;
using SMDFe.Classes.Retorno.MDFeConsultaNaoEncerrado;
using SMDFe.Classes.Retorno.MDFeStatusServico;
using SMDFe.Servicos.ConsultaNaoEncerradosMDFe;
using SMDFe.Servicos.Enderecos.Helper;
using SMDFe.Servicos.Factory;
using SMDFe.Servicos.RecepcaoMDFe;
using SMDFe.Utils.Configuracoes;
using SMDFe.Utils.Flags;
using SMDFe.Wsdl.Configuracao;
using SMDFe.Wsdl.Gerado.MDFeConsultaNaoEncerrados;
using SMDFe.Wsdl.Gerado.MDFeStatusServico;
using MDFeEletronico = SMDFe.Classes.Informacoes.MDFe;

namespace SMDFe.TestesServicos
{
    class Testes
    {
        static void Main(string[] args)
        {
           //ConsultasNaoEnc();
           //ConsultaStatus();
        }

        private void ConsultasNaoEnc()
        {
            try
            {
                
                var config = new ConfiguracaoDao().BuscarConfiguracao();


                var consMDFeNaoEnc = ClassesFactory.CriarConsMDFeNaoEnc(config.Empresa.Cnpj);
                consMDFeNaoEnc.TpAmb = TipoAmbiente.Homologacao;
                consMDFeNaoEnc.Versao = VersaoServico.Versao300;
                consMDFeNaoEnc.ValidarSchema();



                var url = UrlHelper.ObterUrlServico(MDFeConfiguracao.VersaoWebService.TipoAmbiente = TipoAmbiente.Homologacao)
                    .MDFeConsNaoEnc; //Remover depois
                var versao = VersaoServico.Versao300.GetVersaoString();



                var configuracaoWsdl = CriaConfiguracao(url, versao, config);


                var ws = new MDFeConsNaoEnc(configuracaoWsdl);


                var retornoXml = ws.mdfeConsNaoEnc(consMDFeNaoEnc.CriaRequestWs());

                var retorno = MDFeRetConsMDFeNao.LoadXmlString(retornoXml.OuterXml, consMDFeNaoEnc);
                
                Console.WriteLine(retorno.RetornoXmlString);
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void ConsultaStatus()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();


            var consMDFeStatus = ClassesFactory.CriaConsStatServMDFe();
            consMDFeStatus.TpAmb = TipoAmbiente.Homologacao;
            consMDFeStatus.Versao = VersaoServico.Versao300;
            consMDFeStatus.ValidarSchema();



            var url = UrlHelper.ObterUrlServico(MDFeConfiguracao.VersaoWebService.TipoAmbiente = TipoAmbiente.Homologacao)
                .MDFeStatusServico; //Remover depois
            var versao = VersaoServico.Versao300.GetVersaoString();



            var configuracaoWsdl = CriaConfiguracao(url, versao, config);

            var ws = new MDFeStatusServico(configuracaoWsdl);


            var retornoXml = ws.mdfeStatusServicoMDF(consMDFeStatus.CriaRequestWs());

            var retorno = MDFeRetConsStatServ.LoadXml(retornoXml.OuterXml, consMDFeStatus);

            Console.WriteLine(retorno.RetornoXmlString);
            Console.ReadKey();


        }

        public void CriarEnviar()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            
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

            var servicoRecepcao = new ServicoMDFeRecepcao();

            // Evento executado antes do envio da mdf-e para a sefaz
            //servicoRecepcao.AntesDeEnviar += AntesEnviar;

            var retornoEnvio = servicoRecepcao.MDFeRecepcao(1, mdfe);


            config.ConfigWebService.Numeracao++;
            new ConfiguracaoDao().SalvarConfiguracao(config);
        }

        private static int GetRandom()
        {
            var rand = new Random();
            return rand.Next(11111111, 99999999);
        }

        private static WsdlConfiguracao CriaConfiguracao(string url, string versao, Configuracao confi)
        {
            var codigoEstado = confi.ConfigWebService.UfEmitente.GetCodigoIbgeEmString();
            var certificadoDigital = ObterCertificado();

            return new WsdlConfiguracao
            {
                CertificadoDigital = certificadoDigital,
                Versao = versao,
                CodigoIbgeEstado = codigoEstado,
                Url = url,
                TimeOut = 1000
            };
        }

        private static X509Certificate2 ObterCertificado()
        {
            X509Certificate2Collection lcerts;
            X509Store lStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            X509Certificate2 xcertifaCertificate2 = null;
            Object[] certificadObjects = new object[10];
            int i = 0;

            // Abre o Store
            lStore.Open(OpenFlags.ReadOnly);

            // Lista os certificados
            lcerts = lStore.Certificates;

            foreach (X509Certificate2 cert in lcerts)
            {
                if (cert.HasPrivateKey && cert.NotAfter > DateTime.Now && cert.NotBefore < DateTime.Now)
                {
                    certificadObjects[i++] = cert;

                }
            }
            lStore.Close();
            xcertifaCertificate2 = certificadObjects[0] as X509Certificate2;

            return xcertifaCertificate2;

        }
    }
}
