using DFe.Utils;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace NFe.AppTeste.NetCore
{
    internal class Program
    {
        #region Console
        private const string ArquivoConfiguracao = @"\configuracao.xml";
        private static ConfiguracaoConsole _configuracoes;

        private static void Main(string[] args)
        {
            Console.WriteLine("Bem vindo ao demo do projeto NF-e com suporte ao NetStandard 2.0!");
            Console.WriteLine("Este exemplo necessita do arquivo Configuração.xml já criado.");
            Console.WriteLine("Caso necessite criar, utilize o app 'NFe.AppTeste'. e clique em 'Salvar Configuração para Arquivo'");
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
                    Console.WriteLine("1  - opcao de teste");
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

        #endregion


        #region Funcoes

        private void FuncaoStatusServico()
        {
            try
            {
                #region Status do serviço
                //Exemplo com using para chamar o método Dispose da classe.
                //Usar dessa forma, especialmente, quando for usar certificado A3 com a senha salva.
                // se usar cache você pode por um id no certificado e salvar mais de um certificado digital também na memoria com o zeus
                //_configuracoes.CfgServico.Certificado.CacheId = "1";
                using (var servicoNFe = new ServicosNFe(_configuracoes.CfgServico))
                {
                    var retornoStatus = servicoNFe.NfeStatusServico();
                    TrataRetorno(retornoStatus);
                }

                #endregion
            }
            catch (ComunicacaoException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (ValidacaoSchemaException ex)
            {
                Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                    Funcoes.Mensagem(ex.Message, "Erro", MessageBoxButton.OK);
            }
        }

        /*
        private static async Task GerarDanfePdf()
        {
            Console.WriteLine(@"Digite o caminho do .xml (ex: C:\arquivos\35199227357619000192550010090001111381546999.xml):");
            string caminho = Console.ReadLine();

            //busca arquivo xml
            string xml = Funcoes.BuscarArquivoXml(caminho);

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
        */
        #endregion
    }
}
