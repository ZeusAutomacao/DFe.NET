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
            //Teste do config
            //args = new string[1];
            //args[0] = "/config";

            //Teste de cancelamento
            //args = new string[7]; 
            //args[0] = "/cancelar";
            //args[1] = "20079670000152";
            //args[2] = "1234567890123456789012345678901234";
            //args[3] = "Apenas um teste";
            //args[4] = "12345678901223";
            //args[5] = "1";
            //args[6] = "1";

            //Teste de consulta do recebibo
            //args = new string[2];
            //args[0] = "/recibo";
            //args[1] = "310000048991579";

            //Teste de envio
            //args = new string[3];
            //args[0] = "/enviar";
            //args[1] = @"C:\wiati\nfe_de_teste.xml";
            //args[2] = "9999";

            //Teste do help
            //args = new string[1];
            //args[0] = "/?";

            //Teste de inutilização
            //args = new string[7];
            //args[0] = "/inutilizar";
            //args[1] = "2016";
            //args[2] = "20079670000152";
            //args[3] = "Apenas um teste";
            //args[4] = "9999";
            //args[5] = "9999";
            //args[6] = "1";

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
                "/config" - Mudar configuração
                ====================================================================================
                */
                try
                {
                    switch (args[i])
                    {
                        case "/?": listComandos.Add(new KeyValuePair<Comando, string>(Comando.Help,string.Empty)); break;
                        case "/enviar": listComandos.Add(new KeyValuePair<Comando, string>(Comando.EnviarNFe, string.Format("{0}#{1}", args[i + 1], args[i+2]))); break;
                        case "/recibo": listComandos.Add(new KeyValuePair<Comando, string>(Comando.ConsultarReciboEnvio, args[i + 1])); break;
                        case "/status": listComandos.Add(new KeyValuePair<Comando, string>(Comando.StatusServico, string.Empty)); break;
                        case "/inutilizar": listComandos.Add(new KeyValuePair<Comando, string>(Comando.InutilizarNumeracao, string.Format("{0}#{1}#{2}#{3}#{4}#{5}", args[i + 1], args[i + 2], args[i + 3], args[i + 4], args[i + 5], args[i + 6]))); break;
                        case "/cancelar": listComandos.Add(new KeyValuePair<Comando, string>(Comando.CancelarNFe, string.Format("{0}#{1}#{2}#{3}#{4}#{5}", args[i + 1], args[i + 2], args[i + 3], args[i + 4], args[i + 5], args[i + 6]))); break;
                        case "/config": listComandos.Add(new KeyValuePair<Comando, string>(Comando.Configurar,(args.Length == 1) ? "" : string.Format("{0}#{1}", args[i + 1], args[i + 2]))); break;
                        default: break;
                    }
                }
                catch
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
                    switch(tarefa.Key)
                    {
                        case Comando.Help: ImprimirHelp(); break;
                        case Comando.EnviarNFe: EnviarNFe(nfeFacade, tarefa.Value); break;
                        case Comando.ConsultarReciboEnvio: ConsultarReciboDeEnvio(nfeFacade, tarefa.Value);break;
                        case Comando.StatusServico: ConsultarStatusServico(nfeFacade); break;
                        case Comando.InutilizarNumeracao: InutilizarNumeracao(nfeFacade, tarefa.Value);break;
                        case Comando.CancelarNFe: CancelarNFe(nfeFacade, tarefa.Value);break;
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

        /// <summary>
        /// Cancela uma NFe
        /// </summary>
        /// <param name="dadosDoCancelamento">Dados para o cancelamento no formato: cnpj#justificativa#chave_de_acesso#protocolo#numero_lote#sequencia_evento</param>
        private static void CancelarNFe(NFeFacade nfeFacade, string dadosDoCancelamento)
        {
            #region Preparando dados
            string[] arrayStrDados = dadosDoCancelamento.Split('#');

            string strCnpj = string.Empty;
            string strJustificativa = string.Empty;
            string strChaveAcesso = string.Empty;
            string strProtocolo = string.Empty;
            
            int intNumeroLote = 0;
            Int16 intSequenciaEvento = 0;

            try
            {
                strCnpj = arrayStrDados[0];
                strJustificativa = arrayStrDados[1];
                strChaveAcesso = Convert.ToString(arrayStrDados[2]);
                strProtocolo = Convert.ToString(arrayStrDados[3]);
                intNumeroLote = Convert.ToInt32(arrayStrDados[4]);
                intSequenciaEvento = Convert.ToInt16(arrayStrDados[5]);
            }
            catch
            {
                Console.WriteLine("Um ou mais parâmetros estão incorretos.");
            }
            #endregion

            #region Cancelamento

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

                Console.WriteLine("Executando o cancelamento...");
                var retornoCancelamento = nfeFacade.CancelarNFe(strCnpj, intNumeroLote, intSequenciaEvento, strChaveAcesso, strProtocolo, strJustificativa);
                Console.WriteLine("#InutilizacaoEfetuada#{0}", retornoCancelamento.Retorno.retEvento[0].infEvento.xMotivo);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro não esperado.");
                Console.WriteLine(string.Format("Detalhes:{0}", ex.Message));
            }

            #endregion
        }

        /// <summary>
        /// Atribui uma cofiguração ao arquivo correspondente do zeus.
        /// </summary>
        /// <param name="dadosDaConfiguracao">Chave e valor da configuração separados por #. Exemplo: tipo_ambiente#h</param>
        private static void Configurar(NFeFacade nfeFacade, string dadosDaConfiguracao)
        {
            if(dadosDaConfiguracao == string.Empty)
            {
                IList<string> listConteudoArquivo = nfeFacade.CapturarConteudoArquivoDeConfiguracoes();

                Console.WriteLine("");

                foreach(string str in listConteudoArquivo)
                {
                    if(!string.IsNullOrWhiteSpace(str))
                        if(str.Substring(0,1) != "#")
                            Console.WriteLine(str);
                }

                Console.WriteLine("");

                return; // <------- ATENÇÃO
            }

            string strChave = string.Empty;
            string strValor = string.Empty;

            try
            {
                strChave = dadosDaConfiguracao.Split('#')[0];
                strValor = dadosDaConfiguracao.Split('#')[1];
            }
            catch
            {
                Console.WriteLine("Um ou mais parâmetros foram informados de forma incorreta.");
                return;
            }

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
                Console.WriteLine("Ocorreu um erro não esperado.");
                Console.WriteLine(string.Format("Detalhes: {0}", ex.Message));
            }
        }

        /// <summary>
        /// Imprime o help do app.
        /// </summary>
        private static void ImprimirHelp()                                                                                                                                         
        {
            Console.WriteLine("");
            Console.WriteLine("- ZEUS COMMAND LINE -");
            Console.WriteLine("");
            Console.WriteLine("Sintaxe:");
            Console.WriteLine("");
            Console.WriteLine("zeus {comando} {parametros}");
            Console.WriteLine("");
            Console.WriteLine("Comandos suportados:");
            Console.WriteLine("");
            Console.WriteLine("/?          - Help ");
            Console.WriteLine("/enviar     - Enviar NFe");
            Console.WriteLine("/recibo     - Consulta um recibo de envio");
            Console.WriteLine("/status     - Verifica status dos serviços configurados no arquivo de configurações do zeus");
            Console.WriteLine("/inutilizar - Inutiliza uma faixa de numeração");
            Console.WriteLine("/config     - Atribui uma configuração específica ao zeus");
            Console.WriteLine("");
            Console.WriteLine("Exemplos de uso de cada comando:");
            Console.WriteLine("");
            Console.WriteLine("/?          - zeus /? ");
            Console.WriteLine("");
            Console.WriteLine("/config     - zeus /config \"tipo_ambiente\" \"h\"");
            Console.WriteLine("");
            Console.WriteLine("/enviar     - zeus /enviar \"C:\\meu_arquivo_xml.xml\"");
            Console.WriteLine("");
            Console.WriteLine("/recibo     - zeus /recibo \"número_do_recibo\"");
            Console.WriteLine("");
            Console.WriteLine("/status     - zeus /status");
            Console.WriteLine("");
            Console.WriteLine("/inutilizar - zeus /inutilizar \"ano_da_operacao\"");
            Console.WriteLine("                               \"cnpj_da_operacao_sem_traços_ou_barras\"");
            Console.WriteLine("                               \"justificativa\"");
            Console.WriteLine("                               \"número_inicial\"");
            Console.WriteLine("                               \"número_final\"");
            Console.WriteLine("                               \"serie_alvo\"");
            Console.WriteLine("");
            Console.WriteLine("/cancelar   - zeus /cancelar \"cnpj\"");
            Console.WriteLine("                             \"chave_de_acesso\" ");
            Console.WriteLine("                             \"justificativa\"");
            Console.WriteLine("                             \"protocolo\"");
            Console.WriteLine("                             \"numero_lote\"");
            Console.WriteLine("                             \"sequencial_do_evento\"");
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

        /// <summary>
        /// Envia uma NFe para o servidor da sefaz.
        /// </summary>
        ///<param name="dadosDoEnvio">Informações do envio no formato: path_do_arquivo_xml_da_nfe_nao_assinada#numero_do_lote.</param>
        private static void EnviarNFe(NFeFacade nfeFacade, string dadosDoEnvio)
        {
            string strPathArquivoXml = string.Empty;
            int intNumLote = 0;
            string strConteudoArquivoXml = string.Empty;

            try
            {
                strPathArquivoXml = dadosDoEnvio.Split('#')[0];
                intNumLote = Convert.ToInt32(dadosDoEnvio.Split('#')[1]);
            }
            catch
            {
                Console.WriteLine("Um ou mais parâmetros foram informados de forma incorreta.");
                return;
            }

            try
            {
                strConteudoArquivoXml = File.ReadAllText(strPathArquivoXml);
            }
            catch (UnauthorizedAccessException ex)
            {
                string strMensagem = string.Format("Não foi possível acessar o arquivo \"{0}\"", strPathArquivoXml);
                Console.WriteLine(strMensagem);
                Console.WriteLine("Detalhes:");
                Console.WriteLine(ex.Message);
                return;
            }

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

                var nfeBuilder = new NFeBuilder(strPathArquivoXml,TipoXmlNFe.NFe);
                Console.WriteLine("Preparando a NFe...");
                var retorno = nfeFacade.EnviarNFe(1,nfeBuilder.Build());
                if (retorno.Retorno.cStat == 103)
                {
                    Console.WriteLine("#NFe#"+retorno.Retorno.infRec.nRec);
                }
                else
                {
                    Console.WriteLine("#Erro#"+retorno.Retorno.xMotivo);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Houve uma falha não esperada durante o processo de envio.");
                Console.WriteLine("Detalhes:");
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Cria o arquivo de configuração do app.
        /// </summary>
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
                Console.WriteLine("Ocorreu um erro não esperado.");
                Console.WriteLine(string.Format("Detalhes: {0}", ex.Message));
            }
        }

        private static void ConsultarReciboDeEnvio(NFeFacade nfeFacade,string numeroRecibo)
        {
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

                var retornoConsultaProtocolo = nfeFacade.ConsultarReciboDeEnvio(numeroRecibo);
                if (retornoConsultaProtocolo.Retorno.protNFe[0].infProt.nProt != null)
                {
                    Console.WriteLine("#NFe#" + retornoConsultaProtocolo.Retorno.protNFe[0].infProt.nProt);
                }
                else
                {
                    Console.WriteLine("#Erro#" + retornoConsultaProtocolo.Retorno.xMotivo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro não esperado.");
                Console.WriteLine(string.Format("Detalhes: {0}", ex.Message));
            }
        }
    }
}
