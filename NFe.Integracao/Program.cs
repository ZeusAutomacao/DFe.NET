using System;
using System.IO;
using System.Collections.Generic;

namespace NFe.Integracao
{
    public class Program
    {
        public static void Main(string[] args)
        {
            NFeFacade x = new NFeFacade();


            //Comandos a serem executados.
            List<KeyValuePair<Comando, string>> listComandos = new List<KeyValuePair<Comando, string>>();

            for (int i = 0; i < args.Length - 1; i++)
            {
                /*
                ====================================================================================
                Comandos aceitos pelo app:
                "/e" - Enviar NFe
                "/r" - Consultar recibo
                "/s" - Status serviço
                "/p" - Consultar protocolo
                "/i" - Inutilizar numeração
                ====================================================================================
                */
                switch (args[i])
                {
                    case "/e": listComandos.Add(new KeyValuePair<Comando,string>(Comando.EnviarNFe, args[i + 1])); break;
                    case "/r": listComandos.Add(new KeyValuePair<Comando, string>(Comando.ConsultarRecibo, args[i + 1])); break;
                    case "/s": listComandos.Add(new KeyValuePair<Comando, string>(Comando.StatusServico, string.Empty)); break;
                    case "/p": listComandos.Add(new KeyValuePair<Comando, string>(Comando.ConsultarProtocolo, args[i + 1])); break;
                    case "/i": listComandos.Add(new KeyValuePair<Comando, string>(Comando.InutilizarNumeracao, args[i + 1])); break;
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
            }

            //FIM
            //Execução das tarefas
            //---------------------------------------------------------------------------------------------------------------------------------------------
        }
    }
}
