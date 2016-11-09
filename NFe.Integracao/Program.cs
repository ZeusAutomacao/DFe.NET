/********************************************************************************/
/* Projeto: Biblioteca ZeusNFe                                                  */
/* Biblioteca C# para emissão de Nota Fiscal Eletrônica - NFe e Nota Fiscal de  */
/* Consumidor Eletrônica - NFC-e (http://www.nfe.fazenda.gov.br)                */
/*                                                                              */
/* Direitos Autorais Reservados (c) 2014 Adenilton Batista da Silva             */
/*                                       Zeusdev Tecnologia LTDA ME             */
/*                                                                              */
/*  Você pode obter a última versão desse arquivo no GitHub                     */
/* localizado em https://github.com/adeniltonbs/Zeus.Net.NFe.NFCe               */
/*                                                                              */
/*                                                                              */
/*  Esta biblioteca é software livre; você pode redistribuí-la e/ou modificá-la */
/* sob os termos da Licença Pública Geral Menor do GNU conforme publicada pela  */
/* Free Software Foundation; tanto a versão 2.1 da Licença, ou (a seu critério) */
/* qualquer versão posterior.                                                   */
/*                                                                              */
/*  Esta biblioteca é distribuída na expectativa de que seja útil, porém, SEM   */
/* NENHUMA GARANTIA; nem mesmo a garantia implícita de COMERCIABILIDADE OU      */
/* ADEQUAÇÃO A UMA FINALIDADE ESPECÍFICA. Consulte a Licença Pública Geral Menor*/
/* do GNU para mais detalhes. (Arquivo LICENÇA.TXT ou LICENSE.TXT)              */
/*                                                                              */
/*  Você deve ter recebido uma cópia da Licença Pública Geral Menor do GNU junto*/
/* com esta biblioteca; se não, escreva para a Free Software Foundation, Inc.,  */
/* no endereço 59 Temple Street, Suite 330, Boston, MA 02111-1307 USA.          */
/* Você também pode obter uma copia da licença em:                              */
/* http://www.opensource.org/licenses/lgpl-license.php                          */
/*                                                                              */
/* Zeusdev Tecnologia LTDA ME - adenilton@zeusautomacao.com.br                  */
/* http://www.zeusautomacao.com.br/                                             */
/* Rua Comendador Francisco josé da Cunha, 111 - Itabaiana - SE - 49500-000     */
/********************************************************************************/

using System;
using System.IO;
using System.Collections.Generic;
using NFe.Integracao.Enums;

namespace NFe.Integracao
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Comandos a serem executados.
            var listComandos = new List<KeyValuePair<Comando, string>>();

            for (var i = 0; i < args.Length; i++)
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
                        case "/enviar": listComandos.Add(new KeyValuePair<Comando, string>(Comando.EnviarNFe, string.Format("{0}#{1}#{2}", args[i + 1], args[i+2], args[i + 3]))); break;
                        case "/recibo": listComandos.Add(new KeyValuePair<Comando, string>(Comando.ConsultarReciboEnvio, args[i + 1])); break;
                        case "/status": listComandos.Add(new KeyValuePair<Comando, string>(Comando.StatusServico, string.Empty)); break;
                        case "/inutilizar": listComandos.Add(new KeyValuePair<Comando, string>(Comando.InutilizarNumeracao, string.Format("{0}#{1}#{2}#{3}#{4}#{5}", args[i + 1], args[i + 2], args[i + 3], args[i + 4], args[i + 5], args[i + 6]))); break;
                        case "/cancelar": listComandos.Add(new KeyValuePair<Comando, string>(Comando.CancelarNFe, string.Format("{0}#{1}#{2}#{3}#{4}#{5}", args[i + 1], args[i + 2], args[i + 3], args[i + 4], args[i + 5], args[i + 6]))); break;
                        case "/config": listComandos.Add(new KeyValuePair<Comando, string>(Comando.Configurar,(args.Length == 1) ? "" : string.Format("{0}#{1}", args[i + 1], args[i + 2]))); break;
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
                foreach (var tarefa in listComandos)
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
        /// <param name="nfeFacade"></param>
        /// <param name="dadosDoCancelamento">Dados para o cancelamento no formato: cnpj#justificativa#chave_de_acesso#protocolo#numero_lote#sequencia_evento</param>
        private static void CancelarNFe(NFeFacade nfeFacade, string dadosDoCancelamento)
        {
            #region Preparando dados
            var arrayStrDados = dadosDoCancelamento.Split('#');

            var strCnpj = string.Empty;
            var strJustificativa = string.Empty;
            var strChaveAcesso = string.Empty;
            var strProtocolo = string.Empty;
            
            var intNumeroLote = 0;
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
                var booStatusOk = ConsultarStatusServico(nfeFacade);

                if (!booStatusOk)
                {
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
        /// <param name="nfeFacade"></param>
        /// <param name="dadosDaConfiguracao">Chave e valor da configuração separados por #. Exemplo: tipo_ambiente#h</param>
        private static void Configurar(NFeFacade nfeFacade, string dadosDaConfiguracao)
        {
            if(dadosDaConfiguracao == string.Empty)
            {
                IList<string> listConteudoArquivo = nfeFacade.CapturarConteudoArquivoDeConfiguracoes();

                Console.WriteLine("");

                foreach(var str in listConteudoArquivo)
                {
                    if(!string.IsNullOrWhiteSpace(str))
                        if(str.Substring(0,1) != "#")
                            Console.WriteLine(str);
                }

                Console.WriteLine("");

                return; // <------- ATENÇÃO
            }

            string strChave;
            string strValor;

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
            Console.WriteLine("/enviar     - zeus /enviar \"C:\\meu_arquivo_xml.xml\" \"numero_do_lote\" \"tipo_documento(D=Destinatário - nfeProc; N=Nfe; L=Lote - enviNFe3\"");
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
        /// <param name="nfeFacade"></param>
        /// <param name="dadosDaInutilizacao">String composta no seguinte formato: ano#cnpj#justificativa#numero_inicial#numero_final#serie</param>
        private static void InutilizarNumeracao(NFeFacade nfeFacade, string dadosDaInutilizacao)
        {
            #region Preparando dados
            var arrayStrDados = dadosDaInutilizacao.Split('#');

            var strCnpj = string.Empty;
            var strJustificativa = string.Empty;

            var intNumeroInicial = 0;
            var intNumeroFinal = 0;
            var intSerie = 0;
            var intAno = 0;

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
                var booStatusOk = ConsultarStatusServico(nfeFacade);

                if (!booStatusOk)
                {
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
        private static bool ConsultarStatusServico(NFeFacade nfeFacade)
        {
            try
            {
                Console.WriteLine("Acessando o serviço de status da receita...");

                var retorno = nfeFacade.ConsultarStatusServico();

                if (retorno.cStat == 107)
                {
                    Console.WriteLine("#ServicoEmOperacao");
                    return true;
                }
                else
                {
                    Console.WriteLine("#ServicoIndisponivel");
                    return false;
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro não esperado.");
                Console.WriteLine(string.Format("Detalhes: {0}", ex.Message));
                return false;
            }
        }

        ///  <summary>
        ///  Envia uma NFe para o servidor da sefaz.
        ///  </summary>
        /// <param name="nfeFacade"></param>
        /// <param name="dadosDoEnvio">Informações do envio no formato: path_do_arquivo_xml_da_nfe_nao_assinada#numero_do_lote.</param>
        private static void EnviarNFe(NFeFacade nfeFacade, string dadosDoEnvio)
        {
            string strPathArquivoXml;
            int numeroLote;
            TipoXmlNFe tipoXml;

            try
            {
                var arrayStrDados = dadosDoEnvio.Split('#');
                strPathArquivoXml = arrayStrDados[0];
                numeroLote = int.Parse(arrayStrDados[1]);
                switch (arrayStrDados[2])
                {
                    case "D": tipoXml = TipoXmlNFe.Destinatario;
                        break;
                    case "N":
                        tipoXml = TipoXmlNFe.NFe;
                        break;
                    case "L":
                        tipoXml = TipoXmlNFe.Lote;
                        break;
                    default:
                        throw new ArgumentException();
                }
            }
            catch
            {
                Console.WriteLine("Um ou mais parâmetros foram informados de forma incorreta.");
                return;
            }

            try
            {
                File.ReadAllText(strPathArquivoXml);
            }
            catch (UnauthorizedAccessException ex)
            {
                var strMensagem = string.Format("Não foi possível acessar o arquivo \"{0}\"", strPathArquivoXml);
                Console.WriteLine(strMensagem);
                Console.WriteLine("Detalhes:");
                Console.WriteLine(ex.Message);
                return;
            }

            try
            {
                var booStatusOk = ConsultarStatusServico(nfeFacade);

                if (!booStatusOk)
                {
                    return;
                }

                var nfeBuilder = new NFeBuilder(strPathArquivoXml, tipoXml);
                Console.WriteLine("Preparando a NFe...");
                var retorno = nfeFacade.EnviarNFe(numeroLote, nfeBuilder.Build());
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

        /// <summary>
        /// Consulta a situação de um determinado recibo de envio.
        /// </summary>
        private static void ConsultarReciboDeEnvio(NFeFacade nfeFacade,string numeroRecibo)
        {
            try
            {
                var booStatusOk = ConsultarStatusServico(nfeFacade);

                if (!booStatusOk)
                {
                    return;
                }

                var retornoConsultaProtocolo = nfeFacade.ConsultarReciboDeEnvio(numeroRecibo);
                if (retornoConsultaProtocolo.Retorno.protNFe[0].infProt.nProt != null)
                {
                    Console.WriteLine("#NFe#" + retornoConsultaProtocolo.Retorno.protNFe[0].infProt.nProt);
                }
                if (retornoConsultaProtocolo.Retorno.protNFe[0].infProt != null)
                {
                    Console.WriteLine("#Erro#" + retornoConsultaProtocolo.Retorno.protNFe[0].infProt.xMotivo);
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