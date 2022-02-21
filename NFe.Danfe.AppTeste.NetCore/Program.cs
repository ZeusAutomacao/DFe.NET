using DFe.Classes.Flags;
using DFe.Utils;
using NFe.Classes;
using NFe.Classes.Servicos.Consulta;
using NFe.Danfe.Base.NFCe;
using NFe.Danfe.Base.NFe;
using NFe.Danfe.Fast.Standard.NFCe;
using NFe.Danfe.Fast.Standard.NFe;
using NFe.Utils.NFe;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace NFe.Danfe.AppTeste.NetCore
{
    internal class Program
    {
        #region Console
        private const string ArquivoConfiguracao = @"\configuracao.xml";
        private static ConfiguracaoConsole _configuracoes;

        private static void Main(string[] args)
        {
            Console.WriteLine("Bem vindo aos teste de Danfe no projeto NF-e com suporte ao NetStandard 2.0!");
            Console.WriteLine("Este exemplo necessita do arquivo Configuração.xml já criado.");
            Console.WriteLine("Caso necessite criar, utilize o app 'NFe.Danfe.AppTeste'. e clique em 'Salvar Configuração para Arquivo'");
            Console.WriteLine("Em seguida copie o 'configuração.xml' para a pasta bin\\Debug\\net5 deste projeto.");
            Console.WriteLine("Atenção: É necessario trocar o node principal desse arquivo 'configuracao.xml' de 'ConfiguracaoApp' para 'ConfiguracaoConsole'");
            Console.ReadKey();

            //inicializa configuracoes bases (podem ser carregadas novas aqui posteriormente com a opção 99)
            _configuracoes = new ConfiguracaoConsole();

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
                    Console.WriteLine("1  - Gerar Danfe PDF");
                    Console.WriteLine("2  - Gerar Danfe HTML");
                    Console.WriteLine("3  - Gerar Danfe Image PNG");
                    Console.WriteLine("4  - Gerar Danfe Simplificado PDF");
                    Console.WriteLine("5  - Gerar Danfe Simplificado HTML");
                    Console.WriteLine("6  - Gerar Danfe Simplificado Image PNG");
                    Console.WriteLine("7  - Gerar Danfe(Evento) PDF");
                    Console.WriteLine("8  - Gerar Danfe(Evento) HTML");
                    Console.WriteLine("9  - Gerar Danfe(Evento) Image PNG");
                    Console.WriteLine("10  - Gerar Danfe NFCe PDF");
                    Console.WriteLine("11  - Gerar Danfe NFCe HTML");
                    Console.WriteLine("12  - Gerar Danfe NFCe Image PNG");
                    Console.WriteLine("13  - Teste Performance Danfe HTML");
                    Console.WriteLine("14  - Teste Performance Danfe Simplificado HTML");
                    Console.WriteLine($"98 - Carrega logo especifica para configuração");
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
                            await GerarDanfePdf();
                            break;
                        case 2:
                            await GerarDanfeHtml();
                            break;
                        case 3:
                            await GerarDanfePng();
                            break;
                        case 4:
                            await GerarDanfeSimplificadoPdf();
                            break;
                        case 5:
                            await GerarDanfeSimplificadoHtml();
                            break;
                        case 6:
                            await GerarDanfeSimplificadoPng();
                            break;
                        case 7:
                            await GerarDanfeEventoPdf();
                            break;
                        case 8:
                            await GerarDanfeEventoHtml();
                            break;
                        case 9:
                            await GerarDanfeEventoPng();
                            break;
                        case 10:
                            await GerarDanfeNFcePdf();
                            break;
                        case 11:
                            await GerarDanfeNFceHtml();
                            break;
                        case 12:
                            await GerarDanfeNFcePng();
                            break;
                        case 13:
                            await TestePerformanceDanfeHtml();
                            break;
                        case 14:
                            await TestePerformanceDanfeSimplificadoHtml();
                            break;
                        case 98:
                            await CarregarLogoConfiguracao();
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
                                ? new ConfiguracaoConsole()
                                : FuncoesXml.ArquivoXmlParaClasse<ConfiguracaoConsole>(path + ArquivoConfiguracao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static async Task CarregarLogoConfiguracao()
        {
            Console.WriteLine(@"Digite o caminho da logo (ex: C:\arquivos\logo.png):");
            string path = Console.ReadLine();

            try

            {
                _configuracoes.ConfiguracaoDanfeNfe.Logomarca = !File.Exists(path)
                                ? throw new Exception("Logo não encontrada")
                                : File.ReadAllBytes(path);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region NFe

        private static async Task GerarDanfePdf()
        {
            Console.WriteLine(@"Digite o caminho do .xml (ex: C:\arquivos\35199227357619000192550010090001111381546999.xml):");
            string caminho = Console.ReadLine();

            //busca arquivo xml
            string xml = Funcoes.BuscarArquivoXml(caminho);

            try
            {
                var report = GeraClasseDanfeFrNFe(xml);
                byte[] bytes = report.ExportarPdf();
                Funcoes.SalvaArquivoGerado(caminho, ".pdf", bytes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static async Task GerarDanfeHtml()
        {
            Console.WriteLine(@"Digite o caminho do .xml (ex: C:\arquivos\35199227357619000192550010090001111381546999.xml):");
            string caminho = Console.ReadLine();

            //busca arquivo xml
            string xml = Funcoes.BuscarArquivoXml(caminho);

            try
            {
                var report = GeraClasseDanfeFrNFe(xml);
                byte[] bytes = report.ExportarHtml();
                Funcoes.SalvaArquivoGerado(caminho, ".html", bytes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static async Task GerarDanfePng()
        {
            Console.WriteLine(@"Digite o caminho do .xml (ex: C:\arquivos\35199227357619000192550010090001111381546999.xml):");
            string caminho = Console.ReadLine();

            //busca arquivo xml
            string xml = Funcoes.BuscarArquivoXml(caminho);

            try
            {
                var report = GeraClasseDanfeFrNFe(xml);
                byte[] bytes = report.ExportarPng();
                Funcoes.SalvaArquivoGerado(caminho, ".png", bytes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static async Task TestePerformanceDanfeHtml()
        {
            Console.WriteLine(@"Digite o caminho do .xml (ex: C:\arquivos\35199227357619000192550010090001111381546999.xml):");
            string caminho = Console.ReadLine();

            Console.WriteLine(@"Quantidade de relatorios: (ex: 1000)");
            long quantidade = long.Parse(Console.ReadLine());

            Console.WriteLine(@"Salva Arquivos ?: (true = SIM, false = NAO)");
            bool salvaarquivos = bool.Parse(Console.ReadLine());

            //busca arquivo xml
            string xml = Funcoes.BuscarArquivoXml(caminho);

            using (MemoryStream stream = new MemoryStream()) // Create a stream for the report
            {
                try
                {
                    for (int i = 1; i <= quantidade; i++)
                    {
                        Console.Write("\n" + i);
                        //no futuro é interessante calcular o tempo dos metodos abaixo... por enquanto é só visual.
                        var report = GeraClasseDanfeFrNFe(xml);
                        byte[] bytes = report.ExportarHtml();
                        if (salvaarquivos)
                        {
                            Funcoes.SalvaArquivoGerado(caminho, ".html", bytes);
                        }

                        Console.WriteLine("...OK");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private static DanfeFrNfe GeraClasseDanfeFrNFe(string xml)
        {
            var configuracaoDanfeNfe = _configuracoes.ConfiguracaoDanfeNfe;
            try
            {
                #region Carrega um XML com nfeProc para a variável
                nfeProc proc = null;
                try
                {
                    proc = new nfeProc().CarregarDeXmlString(xml);
                }
                catch //Carregar NFe ainda não transmitida à sefaz, como uma pré-visualização.
                {
                    proc = new nfeProc() { NFe = new Classes.NFe().CarregarDeXmlString(xml), protNFe = new Classes.Protocolo.protNFe() };
                }

                if (proc.NFe.infNFe.ide.mod != ModeloDocumento.NFe)
                {
                    throw new Exception("O XML informado não é um NFe!");
                }
                #endregion

                DanfeFrNfe danfe = new DanfeFrNfe(proc: proc, configuracaoDanfeNfe: new ConfiguracaoDanfeNfe()
                {
                    Logomarca = configuracaoDanfeNfe.Logomarca,
                    DuasLinhas = false,
                    DocumentoCancelado = false,
                    QuebrarLinhasObservacao = configuracaoDanfeNfe.QuebrarLinhasObservacao,
                    ExibirResumoCanhoto = configuracaoDanfeNfe.ExibirResumoCanhoto,
                    ResumoCanhoto = configuracaoDanfeNfe.ResumoCanhoto,
                    ChaveContingencia = configuracaoDanfeNfe.ChaveContingencia,
                    ExibeCampoFatura = configuracaoDanfeNfe.ExibeCampoFatura,
                    ImprimirISSQN = configuracaoDanfeNfe.ImprimirISSQN,
                    ImprimirDescPorc = configuracaoDanfeNfe.ImprimirDescPorc,
                    ImprimirTotalLiquido = configuracaoDanfeNfe.ImprimirTotalLiquido,
                    ImprimirUnidQtdeValor = configuracaoDanfeNfe.ImprimirUnidQtdeValor,
                    ExibirTotalTributos = configuracaoDanfeNfe.ExibirTotalTributos,
                    ExibeRetencoes = configuracaoDanfeNfe.ExibeRetencoes
                },
                desenvolvedor: "NOME DA SOFTWARE HOUSE",
                arquivoRelatorio: string.Empty);

                return danfe;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static DanfeFrNfce GeraClasseDanfeFrNFce(string xml)
        {
            var configuracaoDanfeNfe = _configuracoes.ConfiguracaoDanfeNfe;
            try
            {
                #region Carrega um XML com nfeProc para a variável
                nfeProc proc = null;
                try
                {
                    proc = new nfeProc().CarregarDeXmlString(xml);
                }
                catch //Carregar NFCe ainda não transmitida à sefaz, como uma pré-visualização.
                {
                    proc = new nfeProc() { NFe = new Classes.NFe().CarregarDeXmlString(xml), protNFe = new Classes.Protocolo.protNFe() };
                }

                if (proc.NFe.infNFe.ide.mod != ModeloDocumento.NFCe)
                {
                    throw new Exception("O XML informado não é um NFCe!");
                }
                #endregion

                DanfeFrNfce danfe = new DanfeFrNfce(proc: proc, configuracaoDanfeNfce: new ConfiguracaoDanfeNfce()
                {
                    Logomarca = configuracaoDanfeNfe.Logomarca,
                    DocumentoCancelado = false,
                },
                cIdToken: _configuracoes.CIdToken,
                csc: _configuracoes.Csc,
                arquivoRelatorio: "C:\\PainelInformatica\\Relatorios\\NFCe.frx"); // string.Empty);

                return danfe;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region NFCe
        private static async Task GerarDanfeNFcePdf()
        {
            Console.WriteLine(@"Digite o caminho do .xml (ex: C:\arquivos\35199227357619000192550010090001111381546999.xml):");
            string caminho = Console.ReadLine();

            //busca arquivo xml
            string xml = Funcoes.BuscarArquivoXml(caminho);

            try
            {
                var report = GeraClasseDanfeFrNFce(xml);
                byte[] bytes = report.ExportarPdf();
                Funcoes.SalvaArquivoGerado(caminho, ".pdf", bytes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static async Task GerarDanfeNFceHtml()
        {
            Console.WriteLine(@"Digite o caminho do .xml (ex: C:\arquivos\35199227357619000192550010090001111381546999.xml):");
            string caminho = Console.ReadLine();

            //busca arquivo xml
            string xml = Funcoes.BuscarArquivoXml(caminho);

            try
            {
                var report = GeraClasseDanfeFrNFce(xml);
                byte[] bytes = report.ExportarHtml();
                Funcoes.SalvaArquivoGerado(caminho, ".html", bytes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static async Task GerarDanfeNFcePng()
        {
            Console.WriteLine(@"Digite o caminho do .xml (ex: C:\arquivos\35199227357619000192550010090001111381546999.xml):");
            string caminho = Console.ReadLine();

            //busca arquivo xml
            string xml = Funcoes.BuscarArquivoXml(caminho);

            try
            {
                var report = GeraClasseDanfeFrNFce(xml);
                byte[] bytes = report.ExportarPng();
                Funcoes.SalvaArquivoGerado(caminho, ".png", bytes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region NFe Simplificado

        private static async Task GerarDanfeSimplificadoPdf()
        {
            Console.WriteLine(@"Digite o caminho do .xml (ex: C:\arquivos\35199227357619000192550010090001111381546999.xml):");
            string caminho = Console.ReadLine();

            //busca arquivo xml
            string xml = Funcoes.BuscarArquivoXml(caminho);

            try
            {
                var report = GeraClasseDanfeSimplificadoFrNFe(xml);
                byte[] bytes = report.ExportarPdf();
                Funcoes.SalvaArquivoGerado(caminho, ".pdf", bytes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static async Task GerarDanfeSimplificadoHtml()
        {
            Console.WriteLine(@"Digite o caminho do .xml (ex: C:\arquivos\35199227357619000192550010090001111381546999.xml):");
            string caminho = Console.ReadLine();

            //busca arquivo xml
            string xml = Funcoes.BuscarArquivoXml(caminho);

            try
            {
                var report = GeraClasseDanfeSimplificadoFrNFe(xml);
                byte[] bytes = report.ExportarHtml();
                Funcoes.SalvaArquivoGerado(caminho, ".html", bytes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static async Task GerarDanfeSimplificadoPng()
        {
            Console.WriteLine(@"Digite o caminho do .xml (ex: C:\arquivos\35199227357619000192550010090001111381546999.xml):");
            string caminho = Console.ReadLine();

            //busca arquivo xml
            string xml = Funcoes.BuscarArquivoXml(caminho);

            try
            {
                var report = GeraClasseDanfeSimplificadoFrNFe(xml);
                byte[] bytes = report.ExportarPng();
                Funcoes.SalvaArquivoGerado(caminho, ".png", bytes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static async Task TestePerformanceDanfeSimplificadoHtml()
        {
            Console.WriteLine(@"Digite o caminho do .xml (ex: C:\arquivos\35199227357619000192550010090001111381546999.xml):");
            string caminho = Console.ReadLine();

            Console.WriteLine(@"Quantidade de relatorios: (ex: 1000)");
            long quantidade = long.Parse(Console.ReadLine());

            Console.WriteLine(@"Salva Arquivos ?: (true = SIM, false = NAO)");
            bool salvaarquivos = bool.Parse(Console.ReadLine());

            //busca arquivo xml
            string xml = Funcoes.BuscarArquivoXml(caminho);

            using (MemoryStream stream = new MemoryStream()) // Create a stream for the report
            {
                try
                {
                    for (int i = 1; i <= quantidade; i++)
                    {
                        Console.Write("\n" + i);
                        //no futuro é interessante calcular o tempo dos metodos abaixo... por enquanto é só visual.
                        var report = GeraClasseDanfeSimplificadoFrNFe(xml);
                        byte[] bytes = report.ExportarHtml();
                        if (salvaarquivos)
                        {
                            Funcoes.SalvaArquivoGerado(caminho, ".html", bytes);
                        }

                        Console.WriteLine("...OK");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private static DanfeFrSimplificado GeraClasseDanfeSimplificadoFrNFe(string xml)
        {
            var configuracaoDanfeNfe = _configuracoes.ConfiguracaoDanfeNfe;
            try
            {
                #region Carrega um XML com nfeProc para a variável
                nfeProc proc = null;
                try
                {
                    proc = new nfeProc().CarregarDeXmlString(xml);
                }
                catch //Carregar NFe ainda não transmitida à sefaz, como uma pré-visualização.
                {
                    proc = new nfeProc() { NFe = new Classes.NFe().CarregarDeXmlString(xml), protNFe = new Classes.Protocolo.protNFe() };
                }

                if (proc.NFe.infNFe.ide.mod != ModeloDocumento.NFe)
                {
                    throw new Exception("O XML informado não é um NFe!");
                }
                #endregion

                DanfeFrSimplificado danfe = new DanfeFrSimplificado(proc: proc, configuracaoDanfeNfe: new ConfiguracaoDanfeNfe()
                {
                    Logomarca = configuracaoDanfeNfe.Logomarca,
                    DuasLinhas = false,
                    DocumentoCancelado = false,
                    QuebrarLinhasObservacao = configuracaoDanfeNfe.QuebrarLinhasObservacao,
                    ExibirResumoCanhoto = configuracaoDanfeNfe.ExibirResumoCanhoto,
                    ResumoCanhoto = configuracaoDanfeNfe.ResumoCanhoto,
                    ChaveContingencia = configuracaoDanfeNfe.ChaveContingencia,
                    ExibeCampoFatura = configuracaoDanfeNfe.ExibeCampoFatura,
                    ImprimirISSQN = configuracaoDanfeNfe.ImprimirISSQN,
                    ImprimirDescPorc = configuracaoDanfeNfe.ImprimirDescPorc,
                    ImprimirTotalLiquido = configuracaoDanfeNfe.ImprimirTotalLiquido,
                    ImprimirUnidQtdeValor = configuracaoDanfeNfe.ImprimirUnidQtdeValor,
                    ExibirTotalTributos = configuracaoDanfeNfe.ExibirTotalTributos,
                    ExibeRetencoes = configuracaoDanfeNfe.ExibeRetencoes
                },
                desenvolvedor: "NOME DA SOFTWARE HOUSE",
                arquivoRelatorio: string.Empty);

                return danfe;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region NFe Evento

        private static async Task GerarDanfeEventoPdf()
        {
            Console.WriteLine(@"Digite o caminho do .xml (ex: C:\arquivos\35199227357619000192550010090001111381546999.xml):");
            string caminho = Console.ReadLine();
            Console.Clear();
            string xml = Funcoes.BuscarArquivoXml(caminho);

            Console.WriteLine(@"Digite o caminho do evento .xml (ex: C:\arquivos\35199227357619000192550010090001111381546999.xml):");
            caminho = Console.ReadLine();
            Console.Clear();
            string xmlEvento = Funcoes.BuscarArquivoXml(caminho);

            using (MemoryStream stream = new MemoryStream()) // Create a stream for the report
            {
                try
                {
                    var report = GeraClasseDanfeFrEvento(xml, xmlEvento);
                    byte[] bytes = report.ExportarPdf();
                    Funcoes.SalvaArquivoGerado(caminho, ".pdf", bytes);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private static async Task GerarDanfeEventoHtml()
        {
            Console.WriteLine(@"Digite o caminho do .xml (ex: C:\arquivos\35199227357619000192550010090001111381546999.xml):");
            string caminho = Console.ReadLine();
            Console.Clear();
            string xml = Funcoes.BuscarArquivoXml(caminho);

            Console.WriteLine(@"Digite o caminho do evento .xml (ex: C:\arquivos\35199227357619000192550010090001111381546999.xml):");
            caminho = Console.ReadLine();
            Console.Clear();
            string xmlEvento = Funcoes.BuscarArquivoXml(caminho);

            using (MemoryStream stream = new MemoryStream()) // Create a stream for the report
            {
                try
                {
                    var report = GeraClasseDanfeFrEvento(xml, xmlEvento);
                    byte[] bytes = report.ExportarHtml();
                    Funcoes.SalvaArquivoGerado(caminho, ".html", bytes);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private static async Task GerarDanfeEventoPng()
        {
            Console.WriteLine(@"Digite o caminho do .xml (ex: C:\arquivos\35199227357619000192550010090001111381546999.xml):");
            string caminho = Console.ReadLine();
            Console.Clear();
            string xml = Funcoes.BuscarArquivoXml(caminho);

            Console.WriteLine(@"Digite o caminho do evento .xml (ex: C:\arquivos\35199227357619000192550010090001111381546999.xml):");
            caminho = Console.ReadLine();
            Console.Clear();
            string xmlEvento = Funcoes.BuscarArquivoXml(caminho);

            using (MemoryStream stream = new MemoryStream()) // Create a stream for the report
            {
                try
                {
                    var report = GeraClasseDanfeFrEvento(xml, xmlEvento);
                    byte[] bytes = report.ExportarPng();
                    Funcoes.SalvaArquivoGerado(caminho, ".png", bytes);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private static DanfeFrEvento GeraClasseDanfeFrEvento(string xml, string xmlEvento)
        {
            var configuracaoDanfeNfe = _configuracoes.ConfiguracaoDanfeNfe;

            var proc = new nfeProc().CarregarDeXmlString(xml);
            if (proc.NFe.infNFe.ide.mod != ModeloDocumento.NFe)
            {
                throw new Exception("O XML informado não é um NFe!");
            }

            var procEvento = FuncoesXml.XmlStringParaClasse<procEventoNFe>(xmlEvento);

            DanfeFrEvento danfe = new DanfeFrEvento(proc: proc, procEventoNFe: procEvento, configuracaoDanfeNfe: new ConfiguracaoDanfeNfe()
            {
                Logomarca = configuracaoDanfeNfe.Logomarca,
                DuasLinhas = false,
                DocumentoCancelado = false,
                QuebrarLinhasObservacao = configuracaoDanfeNfe.QuebrarLinhasObservacao,
                ExibirResumoCanhoto = configuracaoDanfeNfe.ExibirResumoCanhoto,
                ResumoCanhoto = configuracaoDanfeNfe.ResumoCanhoto,
                ChaveContingencia = configuracaoDanfeNfe.ChaveContingencia,
                ExibeCampoFatura = configuracaoDanfeNfe.ExibeCampoFatura,
                ImprimirISSQN = configuracaoDanfeNfe.ImprimirISSQN,
                ImprimirDescPorc = configuracaoDanfeNfe.ImprimirDescPorc,
                ImprimirTotalLiquido = configuracaoDanfeNfe.ImprimirTotalLiquido,
                ImprimirUnidQtdeValor = configuracaoDanfeNfe.ImprimirUnidQtdeValor,
                ExibirTotalTributos = configuracaoDanfeNfe.ExibirTotalTributos,
                ExibeRetencoes = configuracaoDanfeNfe.ExibeRetencoes,
                
            },
            desenvolvedor: "NOME DA SOFTWARE HOUSE");

            return danfe;
        }

        #endregion
    }
}
