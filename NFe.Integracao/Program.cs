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
                ====================================================================================
                */
                try
                {
                    switch (args[i])
                    {
                        case "/enviar": listComandos.Add(new KeyValuePair<Comando, string>(Comando.EnviarNFe, args[i + 1])); break;
                        case "/recibo": listComandos.Add(new KeyValuePair<Comando, string>(Comando.ConsultarRecibo, args[i + 1])); break;
                        case "/status": listComandos.Add(new KeyValuePair<Comando, string>(Comando.StatusServico, string.Empty)); break;
                        case "/inutilizar": listComandos.Add(new KeyValuePair<Comando, string>(Comando.InutilizarNumeracao, string.Format("{0}#{1}#{2}#{3}#{4}#{5}", args[i + 1], args[i + 2], args[i + 3], args[i + 4], args[i + 5], args[i + 6]))); break;
                        case "/config": listComandos.Add(new KeyValuePair<Comando, string>(Comando.Configurar, string.Format("{0}#{1}", args[i + 1], args[i + 2]))); break;
                        default: break;
                    }
                }
                catch(Exception)
                {
                    Console.WriteLine("Um ou mais parâmetros não foram informados corretamente.");
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
                    //TODO: Continuar a implementar a execução das tarefas na classe principal do app.

                    switch(tarefa.Key)
                    {
                        case Comando.StatusServico: ConsultarStatusServico(nfeFacade); break;
                        case Comando.InutilizarNumeracao: InutilizarNumeracao(nfeFacade, tarefa.Value);break;
                        case Comando.CriarArquivoDeConfiguracoes: CriarArquivoDeConfiguracoes(nfeFacade); break;
                        case Comando.Configurar: Configurar(nfeFacade, tarefa.Value); break;
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

        private static void Configurar(NFeFacade nfeFacade, string dadosDaConfiguracao)
        {
            string strChave = dadosDaConfiguracao.Split('#')[0];
            string strValor = dadosDaConfiguracao.Split('#')[1];

            try
            {
                Console.WriteLine("Executando a alteração no arquivo de configurações...");
                nfeFacade.AlterarArquivoDeConfiguracoes(strChave, strValor);
                Console.WriteLine("Alteração efetuada com sucesso.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro inesperado.");
                Console.WriteLine("Ocorreu um erro não esperado.");
                Console.WriteLine(string.Format("Detalhes: {0}", ex.Message));
            }
        }

        /// <summary>
        /// Inutiliza uma faixa de numeração de NFe.
        /// </summary>
        /// <param name="dadosDaInutilizacao">String composta no seguinte formato: ano#cnpj#justificativa#numero_inicial#numero_final#serie</param>
        private static void InutilizarNumeracao(NFeFacade nfeFacade, string dadosDaInutilizacao)
        {
            #region Preparando dados
            string[] arrayStrDados = dadosDaInutilizacao.Split('#');

            string strCnpj = string.Empty;
            string strJustificativa = string.Empty;

            int intNumeroInicial = 0;
            int intNumeroFinal = 0;
            int intSerie = 0;
            int intAno = 0;

            try
            {

                strCnpj = arrayStrDados[1];
                strJustificativa = arrayStrDados[2];
                intNumeroInicial = Convert.ToInt32(arrayStrDados[3]);
                intNumeroFinal = Convert.ToInt32(arrayStrDados[4]);
                intSerie = Convert.ToInt32(arrayStrDados[5]);
                intAno = Convert.ToInt16(arrayStrDados[0]);
            }
            catch
            {
                Console.WriteLine("Um ou mais parâmetros estão incorretos.");
            }
            #endregion

            #region Inutilização

            try
            {
                Console.WriteLine("Consultando status do serviço...");
                var retornoStatus = nfeFacade.ConsultarStatusServico(TipoAmbiente.taHomologacao);

                if (retornoStatus.cStat == 107)
                {
                    Console.WriteLine("#ServicoEmOperacao");
                }
                else
                {
                    Console.WriteLine("#ServicoIndisponivel");
                    return;
                }

                Console.WriteLine("Executando a inutilização...");
                var retornoInutilizacao = nfeFacade.InutilizarNumeracao(intAno, strCnpj, strJustificativa, intNumeroInicial, intNumeroFinal, intSerie);

                if(retornoInutilizacao.Retorno.infInut.cStat == 102)
                {
                    Console.WriteLine("#InutilizacaoEfetuada#{0}",retornoInutilizacao.Retorno.infInut.nProt);
                }
                else
                {
                    Console.WriteLine("#Erro#{0}", retornoInutilizacao.Retorno.infInut.xMotivo);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Ocorreu um erro não esperado.");
                Console.WriteLine(string.Format("Detalhes:{0}",ex.Message));
            }

            #endregion
        }

        /// <summary>
        /// Consulta o status dos webservices relacionados as informações presentes no arquivo de configuração.
        /// </summary>
        /// <returns>True - Online, False - Offline</returns>
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
