using DFe.Classes.Flags;
using DFe.Utils;
using DFe.Utils.Assinatura;
using DFe.Utils.Standard;
using NFe.Classes;
using NFe.Classes.Informacoes;
using NFe.Classes.Informacoes.Cobranca;
using NFe.Classes.Informacoes.Destinatario;
using NFe.Classes.Informacoes.Detalhe;
using NFe.Classes.Informacoes.Detalhe.Tributacao;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Estadual.Tipos;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal;
using NFe.Classes.Informacoes.Detalhe.Tributacao.Federal.Tipos;
using NFe.Classes.Informacoes.Emitente;
using NFe.Classes.Informacoes.Identificacao;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Classes.Informacoes.Observacoes;
using NFe.Classes.Informacoes.Pagamento;
using NFe.Classes.Informacoes.Total;
using NFe.Classes.Informacoes.Transporte;
using NFe.Classes.Servicos.ConsultaCadastro;
using NFe.Classes.Servicos.Tipos;
using NFe.Servicos;
using NFe.Servicos.Retorno;
using NFe.Utils;
using NFe.Utils.Excecoes;
using NFe.Utils.NFe;
using NFe.Utils.Tributacao.Estadual;
using NFe.Utils.Tributacao.Federal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NFe.AppTeste.NetCore
{
    internal class Program
    {
        #region Console
        private const string ArquivoConfiguracao = @"\configuracao.xml";
        private static ConfiguracaoApp _configuracoes;

        private static void Main(string[] args)
        {
            Console.WriteLine("Bem vindo ao demo do projeto NF-e com suporte ao NetStandard 2.0!");
            Console.WriteLine("Este exemplo necessita do arquivo Configuração.xml já criado.");
            Console.WriteLine("Caso necessite criar, utilize o app 'NFe.AppTeste'. e clique em 'Salvar Configuração para Arquivo'");
            Console.WriteLine("Em seguida copie o 'configuração.xml' para a pasta bin\\Debug\\net5 deste projeto.\n");
            Console.ReadKey();

            //inicializa configuracoes bases (podem ser carregadas novas aqui posteriormente com a opção 99)
            _configuracoes = new ConfiguracaoApp();

            Menu();
        }

        private static async void Menu()
        {
            while (true)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("Escolha uma das opções abaixo:");
                    Console.WriteLine("0  - Sair");
                    Console.WriteLine("1  - Consulta Status");
                    Console.WriteLine("2  - Consulta Cadastro Contribuinte");
                    Console.WriteLine("3  - Envia Nfe (assincrono)");
                    Console.WriteLine("4  - Listar NSU"); //Util para mostrar as notas emitida contra o CNPJ da empresa
                    Console.WriteLine("5  - Manifestar ciência da operação");
                    Console.WriteLine("6  - Download NFe"); 
                    Console.WriteLine($"98 - Carrega certificado (.pfx) A1");
                    Console.WriteLine($"99 - Carrega Configuracoes do arquivo {ArquivoConfiguracao}");

                    string option = Console.ReadLine();
                    Console.WriteLine();
                    Console.Clear();
                    Console.WriteLine("Aguarde... ");

                    switch (Convert.ToInt32(option))
                    {
                        case 0:
                            return;
                        case 1:
                            await FuncaoStatusServico();
                            break;
                        case 2:
                            await FuncaoConsultaCadastroContribuinte();
                            break;
                        case 3:
                            await FuncaoEnviaNfeAssincrono();
                            break;
                        case 4:
                            await CarregarNSUs();
                            break;
                        case 5:
                            await ManifestarCienciaOperacao();
                            break;
                        case 6:
                            await DownloadXml();
                            break;
                        case 98:
                            await CarregaDadosCertificado();
                            break;
                        case 99:
                            await CarregarConfiguracao();
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine(e);
                    Console.WriteLine("Digite algo para continuar...");
                    Console.ReadKey();
                }
            }
        }

        private static async Task CarregarConfiguracao()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            try
            {
                _configuracoes = !File.Exists(path + ArquivoConfiguracao)
                                ? new ConfiguracaoApp()
                                : FuncoesXml.ArquivoXmlParaClasse<ConfiguracaoApp>(path + ArquivoConfiguracao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static async Task CarregaDadosCertificado()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Escreva o caminho do certificado com a extensão .pfx:");
                string caminho = Console.ReadLine();

                Console.Clear();
                Console.WriteLine("Escreva a senha do certificado:");
                string password = Console.ReadLine();

                Console.Clear();
                var cert = CertificadoDigitaoUtil.ObterDoCaminho(caminho, password);
                _configuracoes.CfgServico.Certificado.Serial = cert.SerialNumber;
                Console.WriteLine("Certificado encontrado e carregado...");
                Console.WriteLine("Issuer: " + cert.IssuerName);
                Console.WriteLine("Validade: " + cert.GetExpirationDateString());
                Console.WriteLine("\nPressione para voltar..");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Funcoes

        private static async Task FuncaoStatusServico()
        {
            try
            {
                #region Status do serviço
                using (ServicosNFe servicoNFe = new ServicosNFe(_configuracoes.CfgServico))
                {
                    var retornoStatus = servicoNFe.NfeStatusServico();
                    OnSucessoSync(retornoStatus);
                }
                #endregion
            }
            catch (ComunicacaoException ex)
            {
                throw ex;
            }
            catch (ValidacaoSchemaException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static async Task FuncaoConsultaCadastroContribuinte()
        {
            try
            {
                #region Consulta Cadastro

                Console.Clear();
                Console.WriteLine("UF do Documento a ser Consultado:");
                string uf = Console.ReadLine();
                if (string.IsNullOrEmpty(uf))
                {
                    throw new Exception("A UF deve ser informada!");
                }

                if (uf.Length != 2)
                {
                    throw new Exception("UF deve conter 2 caracteres!");
                }

                Console.Clear();
                Console.WriteLine("Tipo de documento a ser consultado: (0 - IE; 1 - CNPJ; 2 - CPF):");
                string tipoDocumento = Console.ReadLine();
                if (string.IsNullOrEmpty(tipoDocumento))
                {
                    throw new Exception("O Tipo de documento deve ser informado!");
                }

                if (tipoDocumento.Length != 1)
                {
                    throw new Exception("O Tipo de documento deve conter um apenas um número!");
                }

                if (!tipoDocumento.All(char.IsDigit))
                {
                    throw new Exception("O Tipo de documento deve ser um número inteiro");
                }

                int intTipoDocumento = int.Parse(tipoDocumento);
                if (!(intTipoDocumento >= 0 && intTipoDocumento <= 2))
                {
                    throw new Exception("Tipos válidos: (0 - IE; 1 - CNPJ; 2 - CPF)");
                }

                Console.Clear();
                Console.WriteLine("Documento(IE/CNPJ/CPF):");
                string documento = Console.ReadLine();
                if (string.IsNullOrEmpty(documento))
                {
                    throw new Exception("O Documento(IE/CNPJ/CPF) deve ser informado!");
                }

                //efetua req de consulta
                ServicosNFe servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var retornoConsulta = servicoNFe.NfeConsultaCadastro(uf, (ConsultaCadastroTipoDocumento)intTipoDocumento, documento);
                OnSucessoSync(retornoConsulta);

                #endregion
            }
            catch (ComunicacaoException ex)
            {
                throw ex;
            }
            catch (ValidacaoSchemaException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static async Task FuncaoEnviaNfeAssincrono()
        {
            try
            {
                #region Cria e Envia NFe

                /*var numero = Funcoes.InpuBox(this, "Criar e Enviar NFe", "Número da Nota:");
                if (string.IsNullOrEmpty(numero)) throw new Exception("O Número deve ser informado!");

                var lote = Funcoes.InpuBox(this, "Criar e Enviar NFe", "Id do Lote:");
                if (string.IsNullOrEmpty(lote)) throw new Exception("A Id do lote deve ser informada!");*/

                //parametros
                string numero = "123";
                string lote = "321";
                var versaoServico = _configuracoes.CfgServico.VersaoNFeAutorizacao;
                var modelo = _configuracoes.CfgServico.ModeloDocumento;

                //gera o objeto NFe                
                var nfe = GetNf(Convert.ToInt32(numero), modelo, versaoServico);
                nfe.Assina();
                //apenas para nfce
                /*if (nfe.infNFe.ide.mod == ModeloDocumento.NFCe)
                {
                    nfe.infNFeSupl = new infNFeSupl();
                    if (versaoServico == VersaoServico.Versao400)
                        nfe.infNFeSupl.urlChave = nfe.infNFeSupl.ObterUrlConsulta(nfe, _configuracoes.ConfiguracaoDanfeNfce.VersaoQrCode);
                    nfe.infNFeSupl.qrCode = nfe.infNFeSupl.ObterUrlQrCode(nfe, _configuracoes.ConfiguracaoDanfeNfce.VersaoQrCode, configuracaoCsc.CIdToken, configuracaoCsc.Csc);
                }*/
                nfe.Valida();

                //envia via req
                ServicosNFe servicoNFe = new ServicosNFe(_configuracoes.CfgServico);
                var retornoEnvio = servicoNFe.NFeAutorizacao(Convert.ToInt32(lote), IndicadorSincronizacao.Assincrono, new List<Classes.NFe> { nfe }, false/*Envia a mensagem compactada para a SEFAZ*/);

                OnSucessoSync(retornoEnvio);

                #endregion
            }
            catch (ComunicacaoException ex)
            {
                //Faça o tratamento de contingência OffLine aqui.
                throw ex;
            }
            catch (ValidacaoSchemaException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static async Task CarregarNSUs()
        {
            try
            {
                Console.WriteLine("Informe o ultimo número de NSU que você tem. Se não tiver, informe 0 (zero):");
                string ultimoNsu = Console.ReadLine();

                Console.WriteLine("Informe a UF do autor");
                string uf = Console.ReadLine();

                do
                {
                    RetornoNfeDistDFeInt retornoNFeDistDFe = null;
                    using (var _certificado = CertificadoDigital.ObterCertificado(_configuracoes.CfgServico.Certificado))
                    using (var servicoNFe = new ServicosNFe(_configuracoes.CfgServico, _certificado))
                        retornoNFeDistDFe = servicoNFe.NfeDistDFeInteresse(ufAutor: _configuracoes.Emitente.enderEmit.UF.ToString(),
                                                                           documento: _configuracoes.Emitente.CNPJ,
                                                                           ultNSU: ultimoNsu.ToString());

                    var lote = retornoNFeDistDFe.Retorno.loteDistDFeInt;
                    if (lote == null || !lote.Any())
                        break;

                    Console.WriteLine($"{"NSU".PadRight(44, ' ')} | Xml");

                    foreach (var item in lote)
                    {
                        string linha = string.Empty;

                        string xmlStr = string.Empty;

                        if (item.XmlNfe != null)
                        {
                            xmlStr = Compressao.Unzip(item.XmlNfe);

                            Console.WriteLine($"{item.NSU.ToString().PadRight(44, ' ')} | {xmlStr}");
                        }
                    }

                    await Task.Delay(2000); //https://github.com/ZeusAutomacao/DFe.NET/issues/568#issuecomment-339862458

                } while (true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static async Task ManifestarCienciaOperacao()
        {
            try
            {
                Console.WriteLine("Informe a chave da NFe para download:");
                string chave = Console.ReadLine();

                using (var _certificado = CertificadoDigital.ObterCertificado(_configuracoes.CfgServico.Certificado))
                using (var servicoNFe = new ServicosNFe(_configuracoes.CfgServico, _certificado))
                {
                    var retornoManifestacao = servicoNFe.RecepcaoEventoManifestacaoDestinatario(idlote: 1,
                                                                                            sequenciaEvento: 1,
                                                                                            chavesNFe: new string[] { chave },
                                                                                            nFeTipoEventoManifestacaoDestinatario: NFeTipoEvento.TeMdCienciaDaOperacao,
                                                                                            cpfcnpj: _configuracoes.Emitente.CNPJ,
                                                                                            justificativa: null);

                    Console.WriteLine($"Retorno da manifestação: {retornoManifestacao.RetornoStr}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static async Task DownloadXml()
        {
            try
            {
                Console.WriteLine("Informe a chave da NFe para download:");
                string chave = Console.ReadLine();

                Console.WriteLine("Deseja manifestar a NFe? (S/N)");
                bool manifestar = string.Equals(Console.ReadLine().Trim().ToLower(), "s");


                using (var _certificado = CertificadoDigital.ObterCertificado(_configuracoes.CfgServico.Certificado))
                using (var servicoNFe = new ServicosNFe(_configuracoes.CfgServico, _certificado))
                {
                    if (manifestar)
                    {
                        try
                        {
                            var retornoManifestacao = servicoNFe.RecepcaoEventoManifestacaoDestinatario(idlote: 1,
                                                                                                    sequenciaEvento: 1,
                                                                                                    chavesNFe: new string[] { chave },
                                                                                                    nFeTipoEventoManifestacaoDestinatario: NFeTipoEvento.TeMdCienciaDaOperacao,
                                                                                                    cpfcnpj: _configuracoes.Emitente.CNPJ,
                                                                                                    justificativa: null);

                            Console.WriteLine($"Retorno da manifestação: {retornoManifestacao.RetornoStr}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Manifestação: {ex.Message}");
                        }
                    }

                    var retornoNFeDistDFe = servicoNFe.NfeDistDFeInteresse(ufAutor: _configuracoes.EnderecoEmitente.UF.ToString(), documento: _configuracoes.Emitente.CNPJ, chNFE: chave);
                    if (retornoNFeDistDFe.Retorno.loteDistDFeInt == null)
                    {
                        await Task.Delay(2000); //https://github.com/ZeusAutomacao/DFe.NET/issues/568#issuecomment-339862458

                        retornoNFeDistDFe = servicoNFe.NfeDistDFeInteresse(ufAutor: _configuracoes.EnderecoEmitente.UF.ToString(), documento: _configuracoes.Emitente.CNPJ, chNFE: chave);

                        if (retornoNFeDistDFe.Retorno.loteDistDFeInt == null)
                            throw new Exception(retornoNFeDistDFe.Retorno.xMotivo);
                    }

                    if ((retornoNFeDistDFe.Retorno.loteDistDFeInt.Count()) > 0)
                    {
                        var xmlBytes = retornoNFeDistDFe.Retorno.loteDistDFeInt[0].XmlNfe;
                        string xmlStr = Compressao.Unzip(xmlBytes);

                        Console.WriteLine($"Xml: {xmlStr}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion

        #region Funcoes Auxiliares

        private static void OnSucessoSync(RetornoBasico e)
        {
            Console.Clear();
            if (!string.IsNullOrEmpty(e.EnvioStr))
            {
                Console.WriteLine("Xml Envio:");
                Console.WriteLine(FormatXml(e.EnvioStr) + "\n");
            }

            if (!string.IsNullOrEmpty(e.RetornoStr))
            {
                Console.WriteLine("Xml Retorno:");
                Console.WriteLine(FormatXml(e.RetornoStr) + "\n");
            }

            if (!string.IsNullOrEmpty(e.RetornoCompletoStr))
            {
                Console.WriteLine("Xml Retorno Completo:");
                Console.WriteLine(FormatXml(e.RetornoCompletoStr) + "\n");
            }
            Console.ReadKey();
        }

        private static string FormatXml(string xml)
        {
            try
            {
                XDocument doc = XDocument.Parse(xml);
                return doc.ToString();
            }
            catch (Exception)
            {
                return xml;
            }
        }

        #endregion

        #region Criar NFe

        private static Classes.NFe GetNf(int numero, ModeloDocumento modelo, VersaoServico versao)
        {
            Classes.NFe nf = new Classes.NFe { infNFe = GetInf(numero, modelo, versao) };
            return nf;
        }

        private static infNFe GetInf(int numero, ModeloDocumento modelo, VersaoServico versao)
        {
            infNFe infNFe = new infNFe
            {
                versao = versao.VersaoServicoParaString(),
                ide = GetIdentificacao(numero, modelo, versao),
                emit = GetEmitente(),
                dest = GetDestinatario(versao, modelo),
                transp = GetTransporte()
            };

            for (int i = 0; i < 5; i++)
            {
                infNFe.det.Add(GetDetalhe(i, infNFe.emit.CRT, modelo));
            }

            infNFe.total = GetTotal(versao, infNFe.det);

            if (infNFe.ide.mod == ModeloDocumento.NFe & (versao == VersaoServico.Versao310 || versao == VersaoServico.Versao400))
            {
                infNFe.cobr = GetCobranca(infNFe.total.ICMSTot); //V3.00 e 4.00 Somente
            }

            if (infNFe.ide.mod == ModeloDocumento.NFCe || (infNFe.ide.mod == ModeloDocumento.NFe & versao == VersaoServico.Versao400))
            {
                infNFe.pag = GetPagamento(infNFe.total.ICMSTot, versao); //NFCe Somente  
            }

            if (infNFe.ide.mod == ModeloDocumento.NFCe & versao != VersaoServico.Versao400)
            {
                infNFe.infAdic = new infAdic() { infCpl = "Troco: 10,00" }; //Susgestão para impressão do troco em NFCe
            }

            return infNFe;
        }

        private static ide GetIdentificacao(int numero, ModeloDocumento modelo, VersaoServico versao)
        {
            ide ide = new ide
            {
                cUF = _configuracoes.EnderecoEmitente.UF,
                natOp = "VENDA",
                mod = modelo,
                serie = 1,
                nNF = numero,
                tpNF = TipoNFe.tnSaida,
                cMunFG = _configuracoes.EnderecoEmitente.cMun,
                tpEmis = _configuracoes.CfgServico.tpEmis,
                tpImp = TipoImpressao.tiRetrato,
                cNF = "1234",
                tpAmb = _configuracoes.CfgServico.tpAmb,
                finNFe = FinalidadeNFe.fnNormal,
                verProc = "3.000"
            };

            if (ide.tpEmis != TipoEmissao.teNormal)
            {
                ide.dhCont = DateTime.Now;
                ide.xJust = "TESTE DE CONTIGÊNCIA PARA NFe/NFCe";
            }

            #region V2.00

            if (versao == VersaoServico.Versao200)
            {
                ide.dEmi = DateTime.Today; //Mude aqui para enviar a nfe vinculada ao EPEC, V2.00
                ide.dSaiEnt = DateTime.Today;
            }

            #endregion

            #region V3.00

            if (versao == VersaoServico.Versao200)
            {
                return ide;
            }

            if (versao == VersaoServico.Versao310)
            {
                ide.indPag = IndicadorPagamento.ipVista;
            }


            ide.idDest = DestinoOperacao.doInterna;
            ide.dhEmi = DateTime.Now;
            //Mude aqui para enviar a nfe vinculada ao EPEC, V3.10
            if (ide.mod == ModeloDocumento.NFe)
            {
                ide.dhSaiEnt = DateTime.Now;
            }
            else
            {
                ide.tpImp = TipoImpressao.tiNFCe;
            }

            ide.procEmi = ProcessoEmissao.peAplicativoContribuinte;
            ide.indFinal = ConsumidorFinal.cfConsumidorFinal; //NFCe: Tem que ser consumidor Final
            ide.indPres = PresencaComprador.pcPresencial; //NFCe: deve ser 1 ou 4

            #endregion

            return ide;
        }

        private static emit GetEmitente()
        {
            var emit = _configuracoes.Emitente; // new emit
            //{
            //    //CPF = "12345678912",
            //    CNPJ = "12345678000189",
            //    xNome = "RAZAO SOCIAL LTDA",
            //    xFant = "FANTASIA LTRA",
            //    IE = "123456789",
            //};
            emit.enderEmit = GetEnderecoEmitente();
            return emit;
        }

        private static enderEmit GetEnderecoEmitente()
        {
            var enderEmit = _configuracoes.EnderecoEmitente; // new enderEmit
            //{
            //    xLgr = "RUA TESTE DE ENREREÇO",
            //    nro = "123",
            //    xCpl = "1 ANDAR",
            //    xBairro = "CENTRO",
            //    cMun = 2802908,
            //    xMun = "ITABAIANA",
            //    UF = "SE",
            //    CEP = 49500000,
            //    fone = 79123456789
            //};
            enderEmit.cPais = 1058;
            enderEmit.xPais = "BRASIL";
            return enderEmit;
        }

        private static dest GetDestinatario(VersaoServico versao, ModeloDocumento modelo)
        {
            dest dest = new dest(versao)
            {
                CNPJ = "99999999000191",
                //CPF = "99999999999",
            };
            dest.xNome = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL"; //Obrigatório para NFe e opcional para NFCe
            dest.enderDest = GetEnderecoDestinatario(); //Obrigatório para NFe e opcional para NFCe

            //if (versao == VersaoServico.Versao200)
            //    dest.IE = "ISENTO";
            if (versao == VersaoServico.Versao200)
            {
                return dest;
            }

            dest.indIEDest = indIEDest.NaoContribuinte; //NFCe: Tem que ser não contribuinte V3.00 Somente
            dest.email = "teste@gmail.com"; //V3.00 Somente
            return dest;
        }

        private static enderDest GetEnderecoDestinatario()
        {
            enderDest enderDest = new enderDest
            {
                xLgr = "RUA ...",
                nro = "S/N",
                xBairro = "CENTRO",
                cMun = 2802908,
                xMun = "ITABAIANA",
                UF = "SE",
                CEP = "49500000",
                cPais = 1058,
                xPais = "BRASIL"
            };
            return enderDest;
        }

        private static det GetDetalhe(int i, CRT crt, ModeloDocumento modelo)
        {
            det det = new det
            {
                nItem = i + 1,
                prod = GetProduto(i + 1),
                imposto = new imposto
                {
                    vTotTrib = 0.17m,

                    ICMS = new ICMS
                    {
                        //Se você já tem os dados de toda a tributação persistida no banco em uma única tabela, utilize a linha comentada abaixo para preencher as tags do ICMS
                        //TipoICMS = ObterIcmsBasico(crt),

                        //Caso você resolva utilizar método ObterIcmsBasico(), comente esta proxima linha
                        TipoICMS =
                            crt == CRT.SimplesNacional
                                ? InformarCSOSN(Csosnicms.Csosn102)
                                : InformarICMS(Csticms.Cst00, VersaoServico.Versao310)
                    },

                    //ICMSUFDest = new ICMSUFDest()
                    //{
                    //    pFCPUFDest = 0,
                    //    pICMSInter = 12,
                    //    pICMSInterPart = 0,
                    //    pICMSUFDest = 0,
                    //    vBCUFDest = 0,
                    //    vFCPUFDest = 0,
                    //    vICMSUFDest = 0,
                    //    vICMSUFRemet = 0
                    //},

                    COFINS = new COFINS
                    {
                        //Se você já tem os dados de toda a tributação persistida no banco em uma única tabela, utilize a linha comentada abaixo para preencher as tags do COFINS
                        //TipoCOFINS = ObterCofinsBasico(),

                        //Caso você resolva utilizar método ObterCofinsBasico(), comente esta proxima linha
                        TipoCOFINS = new COFINSOutr { CST = CSTCOFINS.cofins99, pCOFINS = 0, vBC = 0, vCOFINS = 0 }
                    },

                    PIS = new PIS
                    {
                        //Se você já tem os dados de toda a tributação persistida no banco em uma única tabela, utilize a linha comentada abaixo para preencher as tags do PIS
                        //TipoPIS = ObterPisBasico(),

                        //Caso você resolva utilizar método ObterPisBasico(), comente esta proxima linha
                        TipoPIS = new PISOutr { CST = CSTPIS.pis99, pPIS = 0, vBC = 0, vPIS = 0 }
                    }
                }
            };

            if (modelo == ModeloDocumento.NFe) //NFCe não aceita grupo "IPI"
            {
                det.imposto.IPI = new IPI()
                {
                    cEnq = 999,

                    //Se você já tem os dados de toda a tributação persistida no banco em uma única tabela, utilize a linha comentada abaixo para preencher as tags do IPI
                    //TipoIPI = ObterIPIBasico(),

                    //Caso você resolva utilizar método ObterIPIBasico(), comente esta proxima linha
                    TipoIPI = new IPITrib() { CST = CSTIPI.ipi00, pIPI = 5, vBC = 1, vIPI = 0.05m }
                };
            }

            //det.impostoDevol = new impostoDevol() { IPI = new IPIDevolvido() { vIPIDevol = 10 }, pDevol = 100 };

            return det;
        }

        private static prod GetProduto(int i)
        {
            prod p = new prod
            {
                cProd = i.ToString().PadLeft(5, '0'),
                cEAN = "7770000000012",
                xProd = i == 1 ? "NOTA FISCAL EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL" : "ABRACADEIRA NYLON 6.6 BRANCA 91X92 " + i,
                NCM = "84159090",
                CFOP = 5102,
                uCom = "UNID",
                qCom = 1,
                vUnCom = 1.1m,
                vProd = 1.1m,
                vDesc = 0.10m,
                cEANTrib = "7770000000012",
                uTrib = "UNID",
                qTrib = 1,
                vUnTrib = 1.1m,
                indTot = IndicadorTotal.ValorDoItemCompoeTotalNF,
                //NVE = {"AA0001", "AB0002", "AC0002"},
                //CEST = ?

                //ProdutoEspecifico = new arma
                //{
                //    tpArma = TipoArma.UsoPermitido,
                //    nSerie = "123456",
                //    nCano = "123456",
                //    descr = "TESTE DE ARMA"
                //}
            };
            return p;
        }

        private static ICMSBasico InformarICMS(Csticms CST, VersaoServico versao)
        {
            ICMS20 icms20 = new ICMS20
            {
                orig = OrigemMercadoria.OmNacional,
                CST = Csticms.Cst20,
                modBC = DeterminacaoBaseIcms.DbiValorOperacao,
                vBC = 1.1m,
                pICMS = 18,
                vICMS = 0.20m,
                motDesICMS = MotivoDesoneracaoIcms.MdiTaxi
            };
            if (versao == VersaoServico.Versao310)
            {
                icms20.vICMSDeson = 0.10m; //V3.00 ou maior Somente
            }

            switch (CST)
            {
                case Csticms.Cst00:
                    return new ICMS00
                    {
                        CST = Csticms.Cst00,
                        modBC = DeterminacaoBaseIcms.DbiValorOperacao,
                        orig = OrigemMercadoria.OmNacional,
                        pICMS = 18,
                        vBC = 1.1m,
                        vICMS = 0.20m
                    };
                case Csticms.Cst20:
                    return icms20;
                    //Outros casos aqui
            }

            return new ICMS10();
        }

        private static ICMSBasico ObterIcmsBasico(CRT crt)
        {
            //Leia os dados de seu banco de dados e em seguida alimente o objeto ICMSGeral, como no exemplo abaixo.
            ICMSGeral icmsGeral = new ICMSGeral
            {
                orig = OrigemMercadoria.OmNacional,
                CST = Csticms.Cst00,
                modBC = DeterminacaoBaseIcms.DbiValorOperacao,
                vBC = 1.1m,
                pICMS = 18,
                vICMS = 0.20m,
                motDesICMS = MotivoDesoneracaoIcms.MdiTaxi
            };
            return icmsGeral.ObterICMSBasico(crt);
        }

        private static PISBasico ObterPisBasico()
        {
            //Leia os dados de seu banco de dados e em seguida alimente o objeto PISGeral, como no exemplo abaixo.
            PISGeral pisGeral = new PISGeral()
            {
                CST = CSTPIS.pis01,
                vBC = 1.1m,
                pPIS = 1.65m,
                vPIS = 0.01m,
                vAliqProd = 0
            };

            return pisGeral.ObterPISBasico();
        }

        private static COFINSBasico ObterCofinsBasico()
        {
            //Leia os dados de seu banco de dados e em seguida alimente o objeto COFINSGeral, como no exemplo abaixo.
            COFINSGeral cofinsGeral = new COFINSGeral()
            {
                CST = CSTCOFINS.cofins01,
                vBC = 1.1m,
                pCOFINS = 1.65m,
                vCOFINS = 0.01m,
                vAliqProd = 0
            };

            return cofinsGeral.ObterCOFINSBasico();
        }

        private static IPIBasico ObterIPIBasico()
        {
            //Leia os dados de seu banco de dados e em seguida alimente o objeto IPIGeral, como no exemplo abaixo.
            IPIGeral ipiGeral = new IPIGeral()
            {
                CST = CSTIPI.ipi01,
                vBC = 1.1m,
                pIPI = 5m,
                vIPI = 0.05m
            };

            return ipiGeral.ObterIPIBasico();
        }

        private static ICMSBasico InformarCSOSN(Csosnicms CST)
        {
            switch (CST)
            {
                case Csosnicms.Csosn101:
                    return new ICMSSN101
                    {
                        CSOSN = Csosnicms.Csosn101,
                        orig = OrigemMercadoria.OmNacional
                    };
                case Csosnicms.Csosn102:
                    return new ICMSSN102
                    {
                        CSOSN = Csosnicms.Csosn102,
                        orig = OrigemMercadoria.OmNacional
                    };
                //Outros casos aqui
                default:
                    return new ICMSSN201();
            }
        }

        private static total GetTotal(VersaoServico versao, List<det> produtos)
        {
            ICMSTot icmsTot = new ICMSTot
            {
                vProd = produtos.Sum(p => p.prod.vProd),
                vDesc = produtos.Sum(p => p.prod.vDesc ?? 0),
                vTotTrib = produtos.Sum(p => p.imposto.vTotTrib ?? 0),
            };

            if (versao == VersaoServico.Versao310 || versao == VersaoServico.Versao400)
            {
                icmsTot.vICMSDeson = 0;
            }

            if (versao == VersaoServico.Versao400)
            {
                icmsTot.vFCPUFDest = 0;
                icmsTot.vICMSUFDest = 0;
                icmsTot.vICMSUFRemet = 0;
                icmsTot.vFCP = 0;
                icmsTot.vFCPST = 0;
                icmsTot.vFCPSTRet = 0;
                icmsTot.vIPIDevol = 0;
            }

            foreach (var produto in produtos)
            {
                if (produto.imposto.IPI != null && produto.imposto.IPI.TipoIPI.GetType() == typeof(IPITrib))
                {
                    icmsTot.vIPI = icmsTot.vIPI + ((IPITrib)produto.imposto.IPI.TipoIPI).vIPI ?? 0;
                }

                if (produto.imposto.ICMS.TipoICMS.GetType() == typeof(ICMS00))
                {
                    icmsTot.vBC = icmsTot.vBC + ((ICMS00)produto.imposto.ICMS.TipoICMS).vBC;
                    icmsTot.vICMS = icmsTot.vICMS + ((ICMS00)produto.imposto.ICMS.TipoICMS).vICMS;
                }
                if (produto.imposto.ICMS.TipoICMS.GetType() == typeof(ICMS20))
                {
                    icmsTot.vBC = icmsTot.vBC + ((ICMS20)produto.imposto.ICMS.TipoICMS).vBC;
                    icmsTot.vICMS = icmsTot.vICMS + ((ICMS20)produto.imposto.ICMS.TipoICMS).vICMS;
                }
                //Outros Ifs aqui, caso vá usar as classes ICMS00, ICMS10 para totalizar
            }

            //** Regra de validação W16-10 que rege sobre o Total da NF **//
            icmsTot.vNF =
                icmsTot.vProd
                - icmsTot.vDesc
                - icmsTot.vICMSDeson.GetValueOrDefault()
                + icmsTot.vST
                + icmsTot.vFCPST.GetValueOrDefault()
                + icmsTot.vFrete
                + icmsTot.vSeg
                + icmsTot.vOutro
                + icmsTot.vII
                + icmsTot.vIPI
                + icmsTot.vIPIDevol.GetValueOrDefault();

            total t = new total { ICMSTot = icmsTot };
            return t;
        }

        private static transp GetTransporte()
        {
            //var volumes = new List<vol> {GetVolume(), GetVolume()};

            transp t = new transp
            {
                modFrete = ModalidadeFrete.mfSemFrete //NFCe: Não pode ter frete
                //vol = volumes 
            };

            return t;
        }

        private static vol GetVolume()
        {
            vol v = new vol
            {
                esp = "teste de espécie",
                lacres = new List<lacres> { new lacres { nLacre = "123456" } }
            };

            return v;
        }

        private static cobr GetCobranca(ICMSTot icmsTot)
        {
            decimal valorParcela = (icmsTot.vNF / 2).Arredondar(2);
            cobr c = new cobr
            {
                fat = new fat { nFat = "12345678910", vLiq = icmsTot.vNF, vOrig = icmsTot.vNF, vDesc = 0m },
                dup = new List<dup>
                {
                    new dup {nDup = "001", dVenc = DateTime.Now.AddDays(30), vDup = valorParcela},
                    new dup {nDup = "002", dVenc = DateTime.Now.AddDays(60), vDup = icmsTot.vNF - valorParcela}
                }
            };

            return c;
        }

        private static List<pag> GetPagamento(ICMSTot icmsTot, VersaoServico versao)
        {
            decimal valorPagto = (icmsTot.vNF / 2).Arredondar(2);

            if (versao != VersaoServico.Versao400) // difernte de versão 4 retorna isso
            {
                List<pag> p = new List<pag>
                {
                    new pag {tPag = FormaPagamento.fpDinheiro, vPag = valorPagto},
                    new pag {tPag = FormaPagamento.fpCheque, vPag = icmsTot.vNF - valorPagto}
                };
                return p;
            }


            // igual a versão 4 retorna isso
            List<pag> p4 = new List<pag>
            {
                //new pag {detPag = new detPag {tPag = FormaPagamento.fpDinheiro, vPag = valorPagto}},
                //new pag {detPag = new detPag {tPag = FormaPagamento.fpCheque, vPag = icmsTot.vNF - valorPagto}}
                new pag
                {
                    detPag = new List<detPag>
                    {
                        new detPag {tPag = FormaPagamento.fpCreditoLoja, vPag = valorPagto},
                        new detPag {tPag = FormaPagamento.fpCreditoLoja, vPag = icmsTot.vNF - valorPagto}
                    }
                }
            };


            return p4;
        }


        #endregion
    }
}
