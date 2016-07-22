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
                "/protocolo" - Consultar protocolo
                "/inutilizar" - Inutilizar numeração
                "/gerarcfg" - Criar arquivo de configurações
                ====================================================================================
                */
                switch (args[i])
                {
                    case "/enviar": listComandos.Add(new KeyValuePair<Comando,string>(Comando.EnviarNFe, args[i + 1])); break;
                    case "/recibo": listComandos.Add(new KeyValuePair<Comando, string>(Comando.ConsultarRecibo, args[i + 1])); break;
                    case "/status": listComandos.Add(new KeyValuePair<Comando, string>(Comando.StatusServico, string.Empty)); break;
                    case "/protocolo": listComandos.Add(new KeyValuePair<Comando, string>(Comando.ConsultarProtocolo, args[i + 1])); break;
                    case "/inutilizar": listComandos.Add(new KeyValuePair<Comando, string>(Comando.InutilizarNumeracao, args[i + 1])); break;
                    case "/gerarcfg": listComandos.Add(new KeyValuePair<Comando, string>(Comando.CriarArquivoDeConfiguracoes, string.Empty)); break;
                    default: break;
                }
            }

            //VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV
            if (listComandos.Count == 0) return; //Se não tiver nada para fazer, encerra o app. 
            //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^



            //---------------------------------------------------------------------------------------------------------------------------------------------
            //Execução das tarefas
            //INÍCIO

            foreach(KeyValuePair<Comando,string> tarefa in listComandos)
            {
                //TODO: Implementar a execução das tarefas na classe principal do app.

                if(tarefa.Key == Comando.StatusServico)
                {
                    Console.WriteLine("Iniciando serviço de configurações do Zeus...");

                    try
                    {
                        var nfeFacade = new NFeFacade();

                        Console.WriteLine("Acessando o serviço de status da receita...");

                        var retorno = nfeFacade.ConsultarStatusServico(TipoAmbiente.taHomologacao);

                        if (retorno.cStat == 107)
                            Console.WriteLine("#ServicoEmOperacao");
                        else
                            Console.WriteLine("#ServicoIndisponivel");
                    }
                    catch(FileNotFoundException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch(InvalidOperationException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Ocorreu um erro não esperado.");
                        Console.WriteLine(string.Format("Detalhes: {0}",ex.Message));
                    }
                }
            }

            //FIM
            //Execução das tarefas
            //---------------------------------------------------------------------------------------------------------------------------------------------
        }
    }
}
