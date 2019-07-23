using DFe.Utils;
using DFe.Utils.Standard;
using NFe.Servicos;
using NFe.Servicos.Retorno;
using NFe.Utils.Excecoes;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;

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
                    Console.WriteLine("1  - opcao de teste");
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
                            return;
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
                                ? new ConfiguracaoConsole()
                                : FuncoesXml.ArquivoXmlParaClasse<ConfiguracaoConsole>(path + ArquivoConfiguracao);
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
                Console.WriteLine("Issuer: " + cert.GetIssuerName());
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
                using (var servicoNFe = new ServicosNFe(_configuracoes.CfgServico))
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


        #endregion

        #region Funcoes Auxiliares

        private static void OnSucessoSync(RetornoNfeStatusServico e)
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
                return xml;
            }
        }

        #endregion
    }
}
