using DFe.Classes.Flags;
using DFe.Utils;
using NFe.Classes;
using NFe.Classes.Servicos.Consulta;
using NFe.Danfe.Base.NFe;
using NFe.Danfe.Fast.Standard.NFe;
using NFe.Utils.NFe;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NFe.Danfe.AppTeste.NetCore
{
    internal class Program
    {
        #region Console

        private static void Main(string[] args)
        {
            //ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            Console.WriteLine("Bem vindo aos teste de Danfe no projeto NF-e com suporte ao NetStandard 2.0!");
            Console.WriteLine("Este exemplo necessita do arquivo Configuração.xml já criado.");
            Console.WriteLine("Caso necessite criar, utilize o app 'NFe.Danfe.AppTeste'. e clique em 'Salvar Configuração para Arquivo'");
            Console.WriteLine("Em seguida copie o 'configuração.xml' para a pasta bin\\Debug\\netcoreapp2.2 deste projeto.\n");

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
                    Console.WriteLine("4  - Gerar Danfe(Evento) PDF");
                    Console.WriteLine("5  - Gerar Danfe(Evento) HTML");
                    Console.WriteLine("6  - Gerar Danfe(Evento) Image PNG");

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
                            await GerarDanfeEventoPdf();
                            break;
                        case 5:
                            await GerarDanfeEventoHtml();
                            break;
                        case 6:
                            await GerarDanfeEventoPng();
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

        #endregion

        #region NFe

        private static async Task GerarDanfePdf()
        {
            Console.WriteLine(@"Digite o caminho do .xml (ex: C:\arquivos\35199227357619000192550010090001111381546999.xml):");
            var caminho = Console.ReadLine();

            //busca arquivo xml
            var xml = Funcoes.BuscarArquivoXml(caminho);

            using (MemoryStream stream = new MemoryStream()) // Create a stream for the report
            {
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
                finally
                {
                    stream.Dispose();
                }
            }
        }

        private static async Task GerarDanfeHtml()
        {
            Console.WriteLine(@"Digite o caminho do .xml (ex: C:\arquivos\35199227357619000192550010090001111381546999.xml):");
            var caminho = Console.ReadLine();

            //busca arquivo xml
            var xml = Funcoes.BuscarArquivoXml(caminho);

            using (MemoryStream stream = new MemoryStream()) // Create a stream for the report
            {
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
                finally
                {
                    stream.Dispose();
                }
            }
        }

        private static async Task GerarDanfePng()
        {
            Console.WriteLine(@"Digite o caminho do .xml (ex: C:\arquivos\35199227357619000192550010090001111381546999.xml):");
            var caminho = Console.ReadLine();

            //busca arquivo xml
            var xml = Funcoes.BuscarArquivoXml(caminho);

            using (MemoryStream stream = new MemoryStream()) // Create a stream for the report
            {
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
                finally
                {
                    stream.Dispose();
                }
            }
        }

        private static DanfeFrNfe GeraClasseDanfeFrNFe(string xml)
        {
            ConfiguracaoDanfeNfe configuracaoDanfeNfe = new ConfiguracaoDanfeNfe();
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
                    ExibirTotalTributos = configuracaoDanfeNfe.ExibirTotalTributos
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
            var caminho = Console.ReadLine();
            Console.Clear();
            var xml = Funcoes.BuscarArquivoXml(caminho);

            Console.WriteLine(@"Digite o caminho do evento .xml (ex: C:\arquivos\35199227357619000192550010090001111381546999.xml):");
            caminho = Console.ReadLine();
            Console.Clear();
            var xmlEvento = Funcoes.BuscarArquivoXml(caminho);

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
                finally
                {
                    stream.Dispose();
                }
            }
        }

        private static async Task GerarDanfeEventoHtml()
        {
            Console.WriteLine(@"Digite o caminho do .xml (ex: C:\arquivos\35199227357619000192550010090001111381546999.xml):");
            var caminho = Console.ReadLine();
            Console.Clear();
            var xml = Funcoes.BuscarArquivoXml(caminho);

            Console.WriteLine(@"Digite o caminho do evento .xml (ex: C:\arquivos\35199227357619000192550010090001111381546999.xml):");
            caminho = Console.ReadLine();
            Console.Clear();
            var xmlEvento = Funcoes.BuscarArquivoXml(caminho);

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
                finally
                {
                    stream.Dispose();
                }
            }
        }

        private static async Task GerarDanfeEventoPng()
        {
            Console.WriteLine(@"Digite o caminho do .xml (ex: C:\arquivos\35199227357619000192550010090001111381546999.xml):");
            var caminho = Console.ReadLine();
            Console.Clear();
            var xml = Funcoes.BuscarArquivoXml(caminho);

            Console.WriteLine(@"Digite o caminho do evento .xml (ex: C:\arquivos\35199227357619000192550010090001111381546999.xml):");
            caminho = Console.ReadLine();
            Console.Clear();
            var xmlEvento = Funcoes.BuscarArquivoXml(caminho);

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
                finally
                {
                    stream.Dispose();
                }
            }
        }

        private static DanfeFrEvento GeraClasseDanfeFrEvento(string xml, string xmlEvento)
        {
            ConfiguracaoDanfeNfe configuracaoDanfeNfe = new ConfiguracaoDanfeNfe();

            var proc = new nfeProc().CarregarDeXmlString(xml);
            if (proc.NFe.infNFe.ide.mod != ModeloDocumento.NFe)
                throw new Exception("O XML informado não é um NFe!");

            var procEvento = FuncoesXml.XmlStringParaClasse<procEventoNFe>(xmlEvento);

            var danfe = new DanfeFrEvento(proc: proc, procEventoNFe: procEvento, configuracaoDanfeNfe: new ConfiguracaoDanfeNfe()
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
                ExibirTotalTributos = configuracaoDanfeNfe.ExibirTotalTributos
            },
            desenvolvedor: "NOME DA SOFTWARE HOUSE");

            return danfe;
        }

        #endregion
    }
}
