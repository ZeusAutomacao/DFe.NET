using System;
using System.IO;
using System.Collections.Generic;
using NFe.Classes.Informacoes.Identificacao.Tipos;

namespace NFe.Integracao
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //args = new string[1];
            //args[0] = "/status";

            //Comandos a serem executados.
            List<KeyValuePair<Comando, string>> listComandos = new List<KeyValuePair<Comando, string>>();

            for (int i = 0; i < args.Length; i++)
            {
                /*
                ====================================================================================
                Comandos aceitos pelo app:
                "/enviar" - Enviar NFe
                "/recibo" - Consultar recibo
                "/status" - Status serviço
                "/inutilizar" - Inutilizar numeração
                "/gerarcfg" - Criar arquivo de configurações
                ====================================================================================
                */
                switch (args[i])
                {
                    case "/enviar": listComandos.Add(new KeyValuePair<Comando, string>(Comando.EnviarNFe, args[i + 1])); break;
                    case "/recibo": listComandos.Add(new KeyValuePair<Comando, string>(Comando.ConsultarRecibo, args[i + 1])); break;
                    case "/status": listComandos.Add(new KeyValuePair<Comando, string>(Comando.StatusServico, string.Empty)); break;
                    case "/inutilizar": listComandos.Add(new KeyValuePair<Comando, string>(Comando.InutilizarNumeracao, args[i + 1])); break;
                    default: break;
                }
            }

            //VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV
            if (listComandos.Count == 0) return; //Se não tiver nada para fazer, encerra o app. 
            //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^



            //---------------------------------------------------------------------------------------------------------------------------------------------
            //Execução das tarefas
            //INÍCIO

            try
            {
                Console.WriteLine("Iniciando serviço de configurações do Zeus...");
                NFeFacade nfeFacade = null;

                try
                {
                    nfeFacade = new NFeFacade();
                }
                catch(FileNotFoundException)
                {
                    Console.Write("O arquivo de configurações do Zeus não foi encontrado. Deseja cria-lo?(S/N):");
                    if(Console.ReadKey().Key == ConsoleKey.S)
                    {
                        nfeFacade = new NFeFacade(true);
                        nfeFacade.CriarArquivoDeConfiguracoes();
                        Console.WriteLine("");
                        Console.WriteLine("Lembre-se atribuir os dados necessários ao arquivo que foi criado.");
                        return; // <----- FINALIZA APP
                    }
                }
                catch(InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Ocorreu um erro não esperado.");
                    Console.WriteLine(string.Format("Detalhes: {0}", ex.Message));
                }

                //VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV
                foreach (KeyValuePair<Comando, string> tarefa in listComandos)
                {
                    //TODO: Implementar a execução das tarefas na classe principal do app.

                    if (tarefa.Key == Comando.StatusServico)
                    {
                        ConsultarStatusServico(nfeFacade);
                    }
                    else if(tarefa.Key == Comando.CriarArquivoDeConfiguracoes)
                    {
                        CriarArquivoDeConfiguracoes(nfeFacade);
                    }
                }
                //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro não esperado.");
                Console.WriteLine(string.Format("Detalhes: {0}", ex.Message));
            }

            //FIM
            //Execução das tarefas
            //---------------------------------------------------------------------------------------------------------------------------------------------
        }

        private static void ConsultarStatusServico(NFeFacade nfeFacade)
        {
            try
            {
                Console.WriteLine("Acessando o serviço de status da receita...");

                var retorno = nfeFacade.ConsultarStatusServico(TipoAmbiente.taHomologacao);

                if (retorno.cStat == 107)
                    Console.WriteLine("#ServicoEmOperacao");
                else
                    Console.WriteLine("#ServicoIndisponivel");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro não esperado.");
                Console.WriteLine(string.Format("Detalhes: {0}", ex.Message));
            }
        }

        private static void CriarArquivoDeConfiguracoes(NFeFacade nfeFacade)
        {
            try
            {
                Console.WriteLine("Gerando arquivo de configurações...");
                nfeFacade.CriarArquivoDeConfiguracoes();
                Console.WriteLine("Arquivo de configurações gerado com sucesso.");
            }
            catch(InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Ocorreu um erro inesperado.");
                Console.WriteLine("Ocorreu um erro não esperado.");
                Console.WriteLine(string.Format("Detalhes: {0}", ex.Message));
            }
        }
    }
}
