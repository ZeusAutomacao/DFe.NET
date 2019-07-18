using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using CTe.AppTeste.Dao;
using CTe.AppTeste.Entidades;
using CTe.Classes;
using CTe.Classes.Informacoes;
using CTe.Classes.Informacoes.Destinatario;
using CTe.Classes.Informacoes.Emitente;
using CTe.Classes.Informacoes.Identificacao;
using CTe.Classes.Informacoes.Impostos;
using CTe.Classes.Informacoes.Impostos.ICMS;
using CTe.Classes.Informacoes.Impostos.Tributacao;
using CTe.Classes.Informacoes.infCTeNormal;
using CTe.Classes.Informacoes.infCTeNormal.infCargas;
using CTe.Classes.Informacoes.infCTeNormal.infModals;
using CTe.Classes.Informacoes.Remetente;
using CTe.Classes.Informacoes.Tipos;
using CTe.Classes.Informacoes.Valores;
using CTe.Classes.Servicos;
using CTe.Classes.Servicos.Evento;
using CTe.Classes.Servicos.Recepcao;
using CTe.Classes.Servicos.Tipos;
using CTe.Servicos.ConsultaProtocolo;
using CTe.Servicos.ConsultaRecibo;
using CTe.Servicos.ConsultaStatus;
using CTe.Servicos.DistribuicaoDFe;
using CTe.Servicos.EnviarCte;
using CTe.Servicos.Eventos;
using CTe.Servicos.Inutilizacao;
using CTe.Servicos.Recepcao;
using CTe.Utils.CTe;
using DFe.Classes.Flags;
using DFe.Utils;
using CteEletronico = CTe.Classes.CTe;
using dest = CTe.Classes.Informacoes.Destinatario.dest;
using infNFe = CTe.Classes.Informacoes.infCTeNormal.infDocumentos.infNFe;

namespace CTe.AppTeste.NetCore
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            Console.WriteLine("Bem vindo aos teste de CT-e com suporte ao NetStandard 2.0!");
            Console.WriteLine("Este exemplo necesita do arquivo Configuração.xml já criado.");
            Console.WriteLine("Caso necessite criar, utilize o app 'CTe.AppTeste'.");
            Console.WriteLine("Em seguida copie o Configuração.xml para a pasta bin\\Debug\\netcoreapp2.2 deste projeto.\n");
            try
            {
                Menu();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static async void Menu()
        {
            while (true)
            {
                Console.WriteLine("Escolha uma das opções abaixo:");
                Console.WriteLine("0  - Sair");
                Console.WriteLine("1  - Consultar Status Serviço");
                Console.WriteLine("2  - Consulta Por Protocolo");
                Console.WriteLine("3  - Inutilizacao de Númeração");
                Console.WriteLine("4  - Consulta Por Número Recibo");
                Console.WriteLine("5  - Evento Cancelar CT-e");
                Console.WriteLine("6  - Carta Correção");
                Console.WriteLine("7  - Criar e Enviar CT-e 2.00 Ou 3.00");
                Console.WriteLine("8  - Criar e Enviar/Consulta Recibo Automatico CT-e 2.00 Ou 3.00");
                Console.WriteLine("9  - CTeDistribuicaoDFe 1.00");
                Console.WriteLine("10 - Evento Desacordo CT-e");
                var option = Console.ReadLine();
                Console.WriteLine();
                switch (Convert.ToInt32(option))
                {
                    case 0:
                        break;
                    case 1:
                        await ConsultarStatusServico2();
                        break;
                    case 2:
                        await ConsultaPorProtocolo();
                        break;
                    case 3:
                        await InutilizacaoDeNumeracao();
                        break;
                    case 4:
                        await ConsultaPorNumeroRecibo();
                        break;
                    case 5:
                        await EventoCancelarCTe();
                        break;
                    case 6:
                        await CartaCorrecao();
                        break;
                    case 7:
                        await CriarEnviarCTe2e3();
                        break;
                    case 8:
                        await CriarEnviarCTeConsultaReciboAutomatico2e3();
                        break;
                    case 9:
                        await DistribuicaoDFe();
                        break;
                    case 10:
                        await EventoDesacordoCTe();
                        break;
                }

                if (Convert.ToInt32(option) > 0)
                    continue;
                break;
            }
        }

        protected class RetornoEEnvio : EventArgs
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


        private static void CarregarConfiguracoes(Configuracao config)
        {
            var configuracaoCertificado = new ConfiguracaoCertificado
            {
                TipoCertificado = TipoCertificado.A1ByteArray,
                ArrayBytesArquivo = GetArrayBytesCertificado(),
                //ManterDadosEmCache = config.CertificadoDigital.ManterEmCache,
                //Serial = config.CertificadoDigital.NumeroDeSerie
                Senha = config.CertificadoDigital.Senha
            };


            ConfiguracaoServico.Instancia.ConfiguracaoCertificado = configuracaoCertificado;
            ConfiguracaoServico.Instancia.TimeOut = config.ConfigWebService.TimeOut;
            ConfiguracaoServico.Instancia.cUF = config.ConfigWebService.UfEmitente;
            ConfiguracaoServico.Instancia.tpAmb = config.ConfigWebService.Ambiente;
            ConfiguracaoServico.Instancia.VersaoLayout = config.ConfigWebService.Versao;
            ConfiguracaoServico.Instancia.DiretorioSchemas = config.ConfigWebService.CaminhoSchemas;
            ConfiguracaoServico.Instancia.IsSalvarXml = config.IsSalvarXml;
            ConfiguracaoServico.Instancia.DiretorioSalvarXml = config.DiretorioSalvarXml;
        }

        private static ConfiguracaoServico MontarConfiguracoes(Configuracao config)
        {
            return new ConfiguracaoServico
            {
                ConfiguracaoCertificado =
                {
                    TipoCertificado = TipoCertificado.A1ByteArray,
                    ArrayBytesArquivo = GetArrayBytesCertificado(),
                    //Arquivo = config.CertificadoDigital.CaminhoArquivo,
                    Senha = config.CertificadoDigital.Senha
                },
                TimeOut = config.ConfigWebService.TimeOut,
                cUF = config.ConfigWebService.UfEmitente,
                tpAmb = config.ConfigWebService.Ambiente,
                VersaoLayout = config.ConfigWebService.Versao,
                DiretorioSchemas = config.ConfigWebService.CaminhoSchemas,
                IsSalvarXml = config.IsSalvarXml,
                DiretorioSalvarXml = config.DiretorioSalvarXml
            };
        }

        private static async Task ConsultarStatusServico2()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            //CarregarConfiguracoes(config);
            
            var configuracaoServico = MontarConfiguracoes(config);

            var statusServico = new StatusServico();
            var retorno = await statusServico.ConsultaStatusAsync(configuracaoServico);

            OnSucessoSync(new RetornoEEnvio(retorno));
        }


        protected static void OnSucessoSync(RetornoEEnvio e)
        {
            Console.WriteLine();
            if (!string.IsNullOrEmpty(e.Envio))
            {
                Console.WriteLine("Xml Envio:");
                Console.WriteLine(FormatXml(e.Envio) + "\n");
            }

            if (!string.IsNullOrEmpty(e.Retorno))
            {
                Console.WriteLine("Xml Retorno:");
                Console.WriteLine(FormatXml(e.Retorno) + "\n");
            }
        }

        private static async Task ConsultaPorProtocolo()
        {
            Console.WriteLine("1 - Por chave");
            Console.WriteLine("2 - Por arquivo xml");
            var option = Console.ReadLine();
            string chave;
            switch (Convert.ToInt32(option))
            {
                case 1:
                    Console.WriteLine("Digite a chave de acesso da CT-e:");
                    chave = Console.ReadLine();
                    break;
                case 2:
                    chave = BuscarChave();
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    return;
            }


            if (string.IsNullOrEmpty(chave))
            {
                Console.WriteLine("Ops.. Não há o que fazer sem uma chave de acesso.");
                return;
            }


            var config = new ConfiguracaoDao().BuscarConfiguracao();
            //CarregarConfiguracoes(config);
            var configuracaoServico = MontarConfiguracoes(config);

            var servicoConsultaProtocolo = new ConsultaProtcoloServico();
            var retorno = await servicoConsultaProtocolo.ConsultaProtocoloAsync(chave, configuracaoServico);


            OnSucessoSync(new RetornoEEnvio(retorno));
        }

        private static string BuscarChave()
        {
            var chave = string.Empty;
            var caminhoArquivoXml = BuscarArquivoXml();

            if (caminhoArquivoXml == null) return chave;
            
            if (caminhoArquivoXml.Contains("completo"))
            {
                var enviCTe = Classes.Servicos.Recepcao.enviCTe.LoadXmlArquivo(caminhoArquivoXml);

                chave = enviCTe.CTe[0].Chave();
            }
            else
            {
                var cte = CteEletronico.LoadXmlArquivo(caminhoArquivoXml);

                chave = cte.Chave();
            }

            return chave;
        }

        private static string BuscarArquivoXml()
        {
            Console.WriteLine("Digite o caminho completo do arquivo de envio do XML:");
            var caminhoXml = Console.ReadLine();
            return caminhoXml ?? string.Empty;
        }


        private static string RequisitarInput(string titulo)
        {
            Console.WriteLine(titulo);
            var valor = Console.ReadLine();
            return valor ?? string.Empty;
        }

        private static async Task InutilizacaoDeNumeracao()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            //CarregarConfiguracoes(config);
            var configuracaoServico = MontarConfiguracoes(config);

            var numeroInicial = int.Parse(RequisitarInput("Númeração Inicial"));
            var numeroFinal = int.Parse(RequisitarInput("Númeração Final"));
            var ano = int.Parse(RequisitarInput("Digite o ano, apenas os ultimos dois digitos"));
            var justificativa = RequisitarInput("Justificativa (15 digitos no minimo)");

            var configInutilizar = new ConfigInutiliza(
                config.Empresa.Cnpj,
                config.ConfigWebService.Serie,
                numeroInicial,
                numeroFinal,
                ano,
                justificativa
            );

            var statusServico = new InutilizacaoServico(configInutilizar);
            var retorno = await statusServico.InutilizarAsync(configuracaoServico);

            OnSucessoSync(new RetornoEEnvio(retorno));
        }

        private static async Task ConsultaPorNumeroRecibo()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            //CarregarConfiguracoes(config);
            var configuracaoServico = MontarConfiguracoes(config);

            var numeroRecibo = RequisitarInput("Número Recibo");

            var consultaReciboServico = new ConsultaReciboServico(numeroRecibo);
            var retorno = await consultaReciboServico.ConsultarAsync(configuracaoServico);

            OnSucessoSync(new RetornoEEnvio(retorno));
        }

        private static async Task EventoCancelarCTe()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            //CarregarConfiguracoes(config);
            var configuracaoServico = MontarConfiguracoes(config);
            
            var caminho = BuscarArquivoXml();

            // aqui estou fazendo um load no lote de ct-e
            var cte = enviCTe.LoadXmlArquivo(caminho).CTe[0];

            // aqui estou fazendo um load no xml de envio de um ct-e
            //var cte = CteEletronico.LoadXmlArquivo(caminho);

            var sequenciaEvento = int.Parse(RequisitarInput("Sequencia Evento"));
            var protocolo = RequisitarInput("Protocolo");
            var justificativa = RequisitarInput("Justificativa mínimo 15 digitos vlw");

            var servico = new EventoCancelamento(cte, sequenciaEvento, protocolo, justificativa);
            var retorno = await servico.CancelarAsync(configuracaoServico);

            OnSucessoSync(new RetornoEEnvio(retorno));
        }

        private static async Task EventoDesacordoCTe()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            //CarregarConfiguracoes(config);
            var configuracaoServico = MontarConfiguracoes(config);
            
            var cnpj = RequisitarInput("CNPJ Tomador");
            var chave = RequisitarInput("Chave CTe");
            var sequenciaEvento = int.Parse(RequisitarInput("Sequencia Evento"));
            var indPres = RequisitarInput("Indicador da prestação (1)");
            var obs = RequisitarInput("Observação (mínimo 15 digitos)");

            var servico = new EventoDesacordo(sequenciaEvento, chave, cnpj, indPres, obs);
            var retorno = await servico.DiscordarAsync(configuracaoServico);

            OnSucessoSync(new RetornoEEnvio(retorno));
        }

        private static async Task CartaCorrecao()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            //CarregarConfiguracoes(config);
            var configuracaoServico = MontarConfiguracoes(config);

            var caminho = BuscarArquivoXml();

            // aqui estou fazendo um load no lote de ct-e
            var cte = enviCTe.LoadXmlArquivo(caminho).CTe[0];

            // aqui estou fazendo um load no xml de envio de um ct-e
            //var cte = CteEletronico.LoadXmlArquivo(caminho);

            var sequenciaEvento = int.Parse(RequisitarInput("Sequencia Evento"));


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

            var servico = new EventoCartaCorrecao(cte, sequenciaEvento, correcoes);
            var retorno = await servico.AdicionarCorrecoesAsync(configuracaoServico);

            OnSucessoSync(new RetornoEEnvio(retorno));
        }

        private static async Task CriarEnviarCTe2e3()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            //CarregarConfiguracoes(config);
            var configuracaoServico = MontarConfiguracoes(config);

            #region infCte
            
            var cteEletronico = new CteEletronico
            {
                infCte = new infCte
                {
                    versao = config.ConfigWebService.Versao,
                    ide = new ide(configuracaoServico)
                    {
                        cUF = config.Empresa.SiglaUf,
                        cCT = GetRandom(),
                        CFOP = 5353,
                        natOp = "PRESTAÇÃO DE SERVICO DE TRANSPORTE CT-E EXEMPLO"
                    }
                }
            };

            #endregion

            #region ide


            if (config.ConfigWebService.Versao == versao.ve200) 
                cteEletronico.infCte.ide.forPag = forPag.Pago;
            
            cteEletronico.infCte.ide.mod = ModeloDocumento.CTe;
            cteEletronico.infCte.ide.serie = config.ConfigWebService.Serie;
            cteEletronico.infCte.ide.nCT = config.ConfigWebService.Numeracao;
            cteEletronico.infCte.ide.dhEmi = DateTimeOffset.Now;
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

            switch (config.ConfigWebService.Versao)
            {
                case versao.ve300:
                    cteEletronico.infCte.ide.indIEToma = indIEToma.ContribuinteIcms;
                    break;
                case versao.ve200:
                    cteEletronico.infCte.ide.tomaBase3 = new toma03
                    {
                        toma = toma.Remetente
                    };
                    break;
            }

            if (config.ConfigWebService.Versao == versao.ve300)
                cteEletronico.infCte.ide.tomaBase3 = new toma3
                {
                    toma = toma.Remetente
                };

            #endregion

            #region emit

            cteEletronico.infCte.emit = new emit
            {
                CNPJ = config.Empresa.Cnpj,
                IE = config.Empresa.InscricaoEstadual,
                xNome = config.Empresa.Nome,
                xFant = config.Empresa.NomeFantasia,
                enderEmit = new enderEmit
                {
                    xLgr = config.Empresa.Logradouro,
                    nro = config.Empresa.Numero,
                    xCpl = config.Empresa.Complemento,
                    xBairro = config.Empresa.Bairro,
                    cMun = config.Empresa.CodigoIbgeMunicipio,
                    xMun = config.Empresa.NomeMunicipio,
                    CEP = long.Parse(config.Empresa.Cep),
                    UF = config.Empresa.SiglaUf,
                    fone = config.Empresa.Telefone
                }
            };


            #endregion

            // Remetente , no caso adicionei os mesmos dados do emitente.. mas seriam o do remente.

            #region rem

            cteEletronico.infCte.rem = new rem
            {
                CNPJ = config.Empresa.Cnpj,
                IE = config.Empresa.InscricaoEstadual,
                xNome = config.Empresa.Nome,
                xFant = config.Empresa.NomeFantasia,
                fone = config.Empresa.Telefone,
                enderReme = new enderReme
                {
                    xLgr = config.Empresa.Logradouro,
                    nro = config.Empresa.Numero,
                    xCpl = config.Empresa.Complemento,
                    xBairro = config.Empresa.Bairro,
                    cMun = config.Empresa.CodigoIbgeMunicipio,
                    xMun = config.Empresa.NomeMunicipio,
                    CEP = long.Parse(config.Empresa.Cep),
                    UF = config.Empresa.SiglaUf
                }
            };


            #endregion

            // Destinatário

            #region dest

            cteEletronico.infCte.dest = new dest
            {
                CNPJ = config.Empresa.Cnpj,
                IE = config.Empresa.InscricaoEstadual,
                xNome = config.Empresa.Nome,
                fone = config.Empresa.Telefone,
                enderDest = new enderDest
                {
                    xLgr = config.Empresa.Logradouro,
                    nro = config.Empresa.Numero,
                    xCpl = config.Empresa.Complemento,
                    xBairro = config.Empresa.Bairro,
                    cMun = config.Empresa.CodigoIbgeMunicipio,
                    xMun = config.Empresa.NomeMunicipio,
                    CEP = long.Parse(config.Empresa.Cep),
                    UF = config.Empresa.SiglaUf
                }
            };


            #endregion

            #region vPrest

            cteEletronico.infCte.vPrest = new vPrest {vTPrest = 100m, vRec = 100m};

            #endregion

            #region imp

            cteEletronico.infCte.imp = new imp {ICMS = new ICMS()};

            var icmsSimplesNacional = new ICMSSN();

            cteEletronico.infCte.imp.ICMS.TipoICMS = icmsSimplesNacional;

            if (config.ConfigWebService.Versao == versao.ve300) icmsSimplesNacional.CST = CST.ICMS90;

            #endregion

            #region infCTeNorm

            cteEletronico.infCte.infCTeNorm = new infCTeNorm
            {
                infCarga = new infCarga
                {
                    vCarga = 1000m,
                    proPred = "Linguiça com piqui",
                    infQ = new List<infQ> {new infQ {cUnid = cUnid.KG, qCarga = 10000, tpMed = "quilos gramas"}}
                },
                infDoc = new infDoc
                {
                    infNFe = new List<infNFe>
                    {
                        new infNFe {chave = "52161021025760000123550010000087341557247948"}
                    }
                }
            };



            if (config.ConfigWebService.Versao == versao.ve200)
            {
                cteEletronico.infCte.infCTeNorm.seg = new List<seg> {new seg {respSeg = respSeg.Destinatario}};
            }

            cteEletronico.infCte.infCTeNorm.infModal = new infModal();

            switch (config.ConfigWebService.Versao)
            {
                case versao.ve200:
                    cteEletronico.infCte.infCTeNorm.infModal.versaoModal = versaoModal.veM200;
                    break;
                case versao.ve300:
                    cteEletronico.infCte.infCTeNorm.infModal.versaoModal = versaoModal.veM300;
                    break;
            }

            var rodoviario = new rodo {RNTRC = config.Empresa.RNTRC};

            if (config.ConfigWebService.Versao == versao.ve200)
            {
                rodoviario.dPrev = DateTime.Now;
                rodoviario.lota = lota.Nao;
            }


            cteEletronico.infCte.infCTeNorm.infModal.ContainerModal = rodoviario;

            #endregion


            var numeroLote = RequisitarInput("Número Lote");

            var servicoRecepcao = new ServicoCTeRecepcao();

            // Evento executado antes do envio do CT-e para o WebService
            // servicoRecepcao.AntesDeEnviar += AntesEnviarLoteCte;

            var retornoEnvio =
                await servicoRecepcao.CTeRecepcaoAsync(int.Parse(numeroLote), new List<CteEletronico> {cteEletronico}, configuracaoServico);

            OnSucessoSync(new RetornoEEnvio(retornoEnvio));

            config.ConfigWebService.Numeracao++;
            new ConfiguracaoDao().SalvarConfiguracao(config);
        }

        private void AntesEnviarLoteCte(object sender, AntesEnviarRecepcao e)
        {
            e.enviCTe.CTe.ForEach(cte => { Console.WriteLine(cte.Chave()); });
        }


        private static int GetRandom()
        {
            var rand = new Random();
            return rand.Next(11111111, 99999999);
        }

        private static async Task CriarEnviarCTeConsultaReciboAutomatico2e3()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            //CarregarConfiguracoes(config);
            var configuracaoServico = MontarConfiguracoes(config);

            #region infCte
            
            var cteEletronico = new CteEletronico
            {
                infCte = new infCte
                {
                    versao = config.ConfigWebService.Versao,
                    ide = new ide(configuracaoServico)
                    {
                        cUF = config.Empresa.SiglaUf,
                        cCT = GetRandom(),
                        CFOP = 5353,
                        natOp = "PRESTAÇÃO DE SERVICO DE TRANSPORTE CT-E EXEMPLO"
                    }
                }
            };

            #endregion

            #region ide

            if (config.ConfigWebService.Versao == versao.ve200) cteEletronico.infCte.ide.forPag = forPag.Pago;
            cteEletronico.infCte.ide.mod = ModeloDocumento.CTe;
            cteEletronico.infCte.ide.serie = config.ConfigWebService.Serie;
            cteEletronico.infCte.ide.nCT = config.ConfigWebService.Numeracao;
            cteEletronico.infCte.ide.dhEmi = DateTimeOffset.Now;
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

            switch (config.ConfigWebService.Versao)
            {
                case versao.ve300:
                    cteEletronico.infCte.ide.indIEToma = indIEToma.ContribuinteIcms;
                    cteEletronico.infCte.ide.tomaBase3 = new toma3
                    {
                        toma = toma.Remetente
                    };
                    break;
                case versao.ve200:
                    cteEletronico.infCte.ide.tomaBase3 = new toma03
                    {
                        toma = toma.Remetente
                    };
                    break;
            }

            #endregion

            #region emit

            cteEletronico.infCte.emit = new emit
            {
                CNPJ = config.Empresa.Cnpj,
                IE = config.Empresa.InscricaoEstadual,
                xNome = config.Empresa.Nome,
                xFant = config.Empresa.NomeFantasia,
                enderEmit = new enderEmit
                {
                    xLgr = config.Empresa.Logradouro,
                    nro = config.Empresa.Numero,
                    xCpl = config.Empresa.Complemento,
                    xBairro = config.Empresa.Bairro,
                    cMun = config.Empresa.CodigoIbgeMunicipio,
                    xMun = config.Empresa.NomeMunicipio,
                    CEP = long.Parse(config.Empresa.Cep),
                    UF = config.Empresa.SiglaUf,
                    fone = config.Empresa.Telefone
                }
            };

            #endregion

            // Remetente , no caso adicionei os mesmos dados do emitente.. mas seriam o do remente.

            #region rem

            cteEletronico.infCte.rem = new rem
            {
                CNPJ = config.Empresa.Cnpj,
                IE = config.Empresa.InscricaoEstadual,
                xNome = config.Empresa.Nome,
                xFant = config.Empresa.NomeFantasia,
                fone = config.Empresa.Telefone,
                enderReme = new enderReme
                {
                    xLgr = config.Empresa.Logradouro,
                    nro = config.Empresa.Numero,
                    xCpl = config.Empresa.Complemento,
                    xBairro = config.Empresa.Bairro,
                    cMun = config.Empresa.CodigoIbgeMunicipio,
                    xMun = config.Empresa.NomeMunicipio,
                    CEP = long.Parse(config.Empresa.Cep),
                    UF = config.Empresa.SiglaUf
                }
            };

            #endregion

            // Destinatário

            #region dest

            cteEletronico.infCte.dest = new dest
            {
                CNPJ = config.Empresa.Cnpj,
                IE = config.Empresa.InscricaoEstadual,
                xNome = config.Empresa.Nome,
                fone = config.Empresa.Telefone,
                enderDest = new enderDest
                {
                    xLgr = config.Empresa.Logradouro,
                    nro = config.Empresa.Numero,
                    xCpl = config.Empresa.Complemento,
                    xBairro = config.Empresa.Bairro,
                    cMun = config.Empresa.CodigoIbgeMunicipio,
                    xMun = config.Empresa.NomeMunicipio,
                    CEP = long.Parse(config.Empresa.Cep),
                    UF = config.Empresa.SiglaUf
                }
            };

            #endregion

            #region vPrest

            cteEletronico.infCte.vPrest = new vPrest {vTPrest = 100m, vRec = 100m};

            #endregion

            #region imp

            cteEletronico.infCte.imp = new imp {ICMS = new ICMS()};

            var icmsSimplesNacional = new ICMSSN();

            cteEletronico.infCte.imp.ICMS.TipoICMS = icmsSimplesNacional;

            if (config.ConfigWebService.Versao == versao.ve300) icmsSimplesNacional.CST = CST.ICMS90;

            #endregion

            #region infCTeNorm

            cteEletronico.infCte.infCTeNorm = new infCTeNorm
            {
                infCarga = new infCarga
                {
                    vCarga = 1000m,
                    proPred = "Linguiça com piqui",
                    infQ = new List<infQ> {new infQ {cUnid = cUnid.KG, qCarga = 10000, tpMed = "quilos gramas"}}
                }
            };


            cteEletronico.infCte.infCTeNorm.infDoc = new infDoc
            {
                infNFe = new List<infNFe> {new infNFe {chave = "52161021025760000123550010000087341557247948"}}
            };

            if (config.ConfigWebService.Versao == versao.ve200)
                cteEletronico.infCte.infCTeNorm.seg = new List<seg> {new seg {respSeg = respSeg.Destinatario}};

            cteEletronico.infCte.infCTeNorm.infModal = new infModal();

            switch (config.ConfigWebService.Versao)
            {
                case versao.ve200:
                    cteEletronico.infCte.infCTeNorm.infModal.versaoModal = versaoModal.veM200;
                    break;
                case versao.ve300:
                    cteEletronico.infCte.infCTeNorm.infModal.versaoModal = versaoModal.veM300;
                    break;
            }

            var rodoviario = new rodo {RNTRC = config.Empresa.RNTRC};

            if (config.ConfigWebService.Versao == versao.ve200)
            {
                rodoviario.dPrev = DateTime.Now;
                rodoviario.lota = lota.Nao;
            }


            cteEletronico.infCte.infCTeNorm.infModal.ContainerModal = rodoviario;

            #endregion


            var numeroLote = RequisitarInput("Número Lote");

            var servico = new ServicoEnviarCte();

            var retorno = await servico.EnviarAsync(Convert.ToInt32(numeroLote), cteEletronico, configuracaoServico);

            var xmlRetorno = string.Empty;

            if (retorno.CteProc != null)
                xmlRetorno = retorno.CteProc.ObterXmlString();

            if (retorno.RetConsReciCTe.protCTe[0].infProt.cStat != 100)
                xmlRetorno = retorno.RetConsReciCTe.RetornoXmlString;

            OnSucessoSync(new RetornoEEnvio(retorno.RetEnviCte.EnvioXmlString, xmlRetorno));

            config.ConfigWebService.Numeracao++;
            new ConfiguracaoDao().SalvarConfiguracao(config);
        }

        private static async Task DistribuicaoDFe()
        {
            var config = new ConfiguracaoDao().BuscarConfiguracao();
            //CarregarConfiguracoes(config);
            var configuracaoServico = MontarConfiguracoes(config);

            #region CTeDistribuicaoDFe

            var cnpj = RequisitarInput("CNPJ do destinatário do CTE:");
            if (string.IsNullOrEmpty(cnpj)) throw new Exception("O CNPJ deve ser informado!");
            if (cnpj.Length != 14) throw new Exception("O CNPJ deve conter 14 caracteres!");


            var ultNSU = RequisitarInput("Ultimo NSU NSU ");
            if (string.IsNullOrEmpty(ultNSU))
                ultNSU = "0";

            if (int.Parse(ultNSU) < 0) throw new Exception("ultNSU deve ser maior ou igual a 0");


            var nsu = RequisitarInput("NSU faltante");
            if (string.IsNullOrEmpty(nsu))
                nsu = "0";

            if (int.Parse(nsu) < 0) throw new Exception("NSU deve ser maior ou igual a 0");


            var servicoCTe = new ServicoCTeDistribuicaoDFe();
            var retornoCTeDistDFe =
                await servicoCTe.CTeDistDFeInteresseAsync(config.Empresa.SiglaUf.ToString(), cnpj, ultNSU, nsu, configuracaoServico);

            OnSucessoSync(new RetornoEEnvio(retornoCTeDistDFe.EnvioStr, retornoCTeDistDFe.RetornoStr));

            #endregion
        }

        private static string FormatXml(string xml)
        {
            try
            {
                var doc = XDocument.Parse(xml);
                return doc.ToString();
            }
            catch (Exception)
            {
                // Handle and throw if fatal exception here; don't just ignore them
                return xml;
            }
        }

        private static byte[] GetArrayBytesCertificado()
        {
            throw new  Exception("Coloque aqui a string do seu certificado, ou modifique " +
                                 "o tipo de certificado nas configurações desta classe.");
            return Convert.FromBase64String(
                "String do Certificado em base64");
        }
    }
}