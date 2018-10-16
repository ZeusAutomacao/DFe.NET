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
/* Zeus dev Tecnologia LTDA ME - adenilton@zeusautomacao.com.br                  */
/* http://www.zeusautomacao.com.br/                                             */
/* Rua Comendador Francisco josé da Cunha, 111 - Itabaiana - SE - 49500-000     */
/********************************************************************************/

/********************************************************************************/
/*Contribuições:                                                                
 * Andrews Ferreira Bárbara andrews.fb87@gmail.com
 * Valnei Batista Santos Filho v_marinpietri@yaho.com.br 
/********************************************************************************/

using System;
using System.IO;
using NFe.Classes.Servicos.ConsultaCadastro;
using NFe.Integracao.Enums;

namespace NFe.Integracao
{
    public class Program
    {

        public static void Main(string[] args)
        {
            //Uncomment for debug
            //args = new[] { "consultarCadastro","BA","00000000000" };
            //args = new[] { "recibo","00000000000","","" };
            //args = new[] {"enviar","","",""};
            //args = new[] { "help", "", "", "" };
            //args = new[] { "imprimirnfce", @"C:\WiaTI\NFC-e\Exemplo.xml", @"C:\WiaTI\NFC-e\Exemplo.jpeg", "", "" };
            //args = new[] { "configurar" };
            //args = new[] { "status" };

            try
            {
                var arg = args[0].Length == 0 ? string.Empty : args[0];
                //CaseInsesitive
                var command = string.IsNullOrWhiteSpace(arg) ? string.Empty : arg.ToLower();

                switch (command)
                {

                    case "?":
                    case "help":
                        ImprimirHelp();
                        break;
                    case "enviar"://Enviar nota fiscal
                        var param1 = string.Format("{0}#{1}#{2}", args[1], args[2], args[3]);
                        EnviarNFe(param1);
                        break;
                    case "recibo"://Consultar recibo
                        var param2 = string.Format("{0}", args[1]);
                        ConsultarReciboDeEnvio(param2);
                        break;
                    case "consultarcadastro"://Consulta cadastro contribuinte
                        var param3 = string.Format("{0}#{1}", args[1], args[2]);
                        ConsultarCadastro(param3);
                        break;
                    case "status"://Consulta status nota fiscal
                        ConsultarStatusServico();
                        break;
                    case "inutilizar"://Inutilizar nota fiscal
                        var param4 = string.Format("{0}#{1}#{2}#{3}#{4}#{5}", args[1], args[2],
                            args[3], args[4], args[5], args[6]);
                        InutilizarNumeracao(param4); break;
                    case "cancelar"://Cancelar nota fiscal
                        var param5 = string.Format("{0}#{1}#{2}#{3}#{4}#{5}", args[1], args[2], args[3],
                            args[4], args[5], args[6]);
                        CancelarNFe(param5);
                        break;
                    case "configurar":
                    case "config":
                        Configurar();
                        break;
                    case "exibirconfig":
                        ExibirConfig();
                        break;
                    case "imprimirnfce":
                        var param6 = string.Format("{0}#{1}#{2}#{3}", args[1], args[2], arg[3], arg[4]);
                        ImprimirNFCe(param6);
                        break;
                    default:
                        Console.Write("Comando não reconhecido ou parâmetro inválido. Digite help para acessar ajuda.");
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Um ou mais parâmetros foram informados incorretamente.");
            }

            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Exibe uma lista contendo dados de configuração
        /// </summary>
        private static void ExibirConfig()
        {
            try
            {
                Console.Clear();
                var list = NFeFacade.GetConfiguracao();
                foreach (var item in list)
                {
                    Console.WriteLine(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Não foi possível exibir dados de configuração.\nErro: {0}", ex.Message));
            }

        }

        private static NFeFacade GetFacade()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Iniciando serviço de configurações do Zeus...");
                Console.ResetColor();
                var nfeFacade = new NFeFacade();
                //Verfiicar status das variaveis de configuracao
                var isconfig = nfeFacade.IsConfigured();
                if (!isconfig)
                {
                    Console.Write("");
                    Console.WriteLine("Foi detectado campos obrigatórios não informados\n" +
                        "Informe dados de configuração");
                    Configurar();
                }
                
                return nfeFacade;
            }
            catch (Exception ex)
            {

                Console.WriteLine("Ocorreu um erro não esperado.");
                Console.WriteLine("Detalhes: {0}", ex.Message);
                return null; //stop execute routine
            }
        }
        /// <summary>
        ///     Configurar dados
        /// </summary>
        private static void Configurar()
        {
            //Usuario entra com dados             
            Console.WriteLine("");
            Console.WriteLine("Informe dados do arquivo de configuração");

            //Variável do arquivo de configurações, Descrição
            Console.WriteLine("Caminho do arquivo .pfx");
            var pathcertificado = Console.ReadLine();
            Console.WriteLine("Senha do certificado");
            var certificadosenha = Console.ReadLine();
            Console.WriteLine("Caminho do diretório onde serão salvos os xml");
            var pathxml = Console.ReadLine();
            Console.WriteLine("Diretório onde estão os arquivos de schame tipo .xsd");
            var pathchema = Console.ReadLine();
            Console.WriteLine("Código IBGE do estado do emitente");
            var emitente = Console.ReadLine();
            Console.WriteLine("Modelo do documento 55 ou 65");
            var modelodocumento = Console.ReadLine();
            Console.WriteLine("Salvar arquivo XML {1 - Sim | 0 - Nao}");
            var salvarxmlservico = Console.ReadLine();
            Console.WriteLine("Tempo de espera máximo dos serviços da sefaz em milisegundos");
            var timeout = Console.ReadLine();
            Console.WriteLine("Ambiente { 1 - Producão | 2 - Homologação }");
            var tmpamb = Console.ReadLine();
            Console.WriteLine("Tipo de emissão 1 (Normal) | 2 (FS-IA)| 3 (SCAN)| 4 (EPEC)| 5 (FS-DA)| 6 (SVC-AN)| 7 (SVC-RS) | 9 (Offline) ");
            var tmpemissao = Console.ReadLine();
            Console.WriteLine("Versão da NFe { 3.10 | 4.00 } ");
            var versaoNFe = Console.ReadLine();

            //Set dados
            NFeFacade.SetConfiguracoes(pathcertificado, certificadosenha, pathxml,
                pathchema, emitente, modelodocumento, salvarxmlservico, timeout, tmpamb, tmpemissao, versaoNFe);

            Console.Clear();
            Console.WriteLine("Dados configurados !");
        }
        /// <summary>
        ///     Cancelar uma NFe
        /// </summary>
        /// <param name="dadosDoCancelamento">Dados para o cancelamento no formato: cnpj#justificativa#chave_de_acesso#protocolo#numero_lote#sequencia_evento</param>
        private static void CancelarNFe(string dadosDoCancelamento)
        {
            var nfeFacade = GetFacade();
            if (nfeFacade == null) return;

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
                var booStatusOk = ConsultarStatusServico();

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
        ///     Imprime o help do app.
        /// </summary>
        private static void ImprimirHelp()
        {
            Console.Clear();
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Gerenciador Nfe - Nota Fiscal Eletrônica by - Zeus Tecnologia LTDA ME");
            Console.ResetColor();
            Console.WriteLine("");
            Console.Write("Sintaxe: "); ;
            Console.WriteLine("NFe.Integracao {comando} {parametros}");
            Console.WriteLine("");
            Console.WriteLine("Comandos suportados:");
            Console.Write("-----------------------------------------------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("?                   - Help ");
            Console.WriteLine("ENVIAR              - Enviar NFe");
            Console.WriteLine("RECIBO              - Consulta um recibo de envio");
            Console.WriteLine("STATUS              - Verifica status dos serviços configurados no arquivo de configurações do zeus");
            Console.WriteLine("CONSULTARCADASTRO   - Serviço para consultar o cadastro de contribuintes do ICMS da unidade federada.");
            Console.WriteLine("INUTILIZAR          - Inutiliza uma faixa de numeração");
            Console.WriteLine("CONFIGURAR          - Configurar parâmetros");
            Console.WriteLine("EXIBIRCONFIG        - Exibe configurações gerais");
            Console.WriteLine("IMPRIMIRNFCE        - Gera um arquivo jpeg relacionado ao xml de um NFCe");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------");
            Console.Write("");
            Console.WriteLine("Exemplos de uso de cada comando:");
            Console.WriteLine("");
            Console.WriteLine("NFe.Integracao CONFIGURAR (sem argumentos)");
            Console.WriteLine("");
            Console.Write("NFe.Integracao ENVIAR (3 argumentos separados por espaço em branco) ");
            Console.WriteLine("{Caminho do arquivo} {Numero do Lote} {Tipo do documento (D=Destinatário - nfeProc; N=Nfe; L=Lote - enviNFe3}");
            Console.WriteLine("ex: NFe.Integracao ENVIAR c:/arquivo.xml 0002 L");
            Console.WriteLine("");
            Console.Write("NFe.Integracao RECIBO (1 argumento) ");
            Console.WriteLine("{Numero do recibo}");
            Console.WriteLine("ex: NFe.Integracao RECIBO 000221210");
            Console.WriteLine("");
            Console.Write("NFe.Integracao CONSULTARCADASTRO (2 argumentos separados por espaço em branco) ");
            Console.WriteLine("{UF CNPJ/CPF} Nota: Use SU como prefixo para suframa em substituição a UF");
            Console.WriteLine("ex: NFe.Integracao CONSULTARCADASTRO BA 000000000000");
            Console.WriteLine("");
            Console.Write("NFe.Integracao INUTILIZAR (6 argumentos separados por espaço em branco) ");
            Console.WriteLine("{Ano de operação} {CNPJ} {Justificativa (sem espaço em branco!)} {Número Inicial} {Número Final} {Série Alvo} ");
            Console.WriteLine("ex: NFe.Integracao INUTILIZAR 2016 00000000000 justificativa 0002 0001 1");
            Console.WriteLine("");
            Console.Write("NFe.Integracao CANCELAR (6 argumentos separados por espaço em branco) ");
            Console.WriteLine("{CNPJ} {Chave de acesso} {Justificativa} {Protocolo} {Numero lote} {Sequencial do Evento} ");
            Console.WriteLine("ex: NFe.Integracao CANCELAR 00000000000 021512121512121 justificativa 00002 0222 00001");
            Console.WriteLine("");
            Console.Write("NFe.Integracao IMPRIMIRNFCE (4 argumentos separados por espaço em branco) ");
            Console.WriteLine("{Path do arquivo xml} {Path onde salvar o jpeg gerado} {Id do Token} {CSC}");
            Console.WriteLine(@"ex: NFe.Integracao CANCELAR C:\NFCe\exemplo.xml C:\NFCe\exemplo.jpeg");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------");
        }
        /// <summary>
        ///     Consulta a situação cadastral, com base na UF/Documento
        ///     <para>O documento pode ser: CPF ou CNPJ. O serviço avaliará o tamanho da 
        /// string passada e determinará se a coonsulta será por CPF ou por CNPJ</para>
        /// </summary>
        /// <param name="dadosconsulta">Dados da consulta</param>
        /// <returns> Um arquivo XML na dentro da pasta padrão corrente</returns>
        private static void ConsultarCadastro(string dadosconsulta)
        {
            var nfeFacade = GetFacade();
            if (nfeFacade == null) return;
            #region Preparando dados
            var arrayStrDados = dadosconsulta.Split('#');

            var strUf = string.Empty;
            var documento = string.Empty;

            try
            {

                strUf = arrayStrDados[0];
                documento = arrayStrDados[1];
            }
            catch
            {
                Console.WriteLine("Um ou mais parâmetros estão incorretos.");
            }
            #endregion

            #region Consultando dados
            try
            {
                //Escolher o tipo de documento
                Console.WriteLine("Qual tipo de documento? [0 - CNPJ | 1 - CPF | 2 - IE]");
                var tipodocumento = Console.ReadLine();
                if (tipodocumento != "0" & tipodocumento != "1" & tipodocumento != "2")
                    return;

                Console.WriteLine("Consultando o cadastro na receita...");
                if (tipodocumento == "0")
                    nfeFacade.ConsultaCadastro(strUf, ConsultaCadastroTipoDocumento.Cnpj, documento);
                if (tipodocumento == "1")
                    nfeFacade.ConsultaCadastro(strUf, ConsultaCadastroTipoDocumento.Cpf, documento);
                if (tipodocumento == "2")
                    nfeFacade.ConsultaCadastro(strUf, ConsultaCadastroTipoDocumento.Ie, documento);
            }
            catch (Exception ex)
            {

                Console.WriteLine("Ocorreu um erro não esperado.");
                Console.WriteLine(string.Format("Detalhes: {0}", ex.Message));
            }
            #endregion

        }
        /// <summary>
        ///     Inutiliza uma faixa de numeração de NFe.
        /// </summary>
        /// <param name="dadosDaInutilizacao">String composta no seguinte formato: ano#cnpj#justificativa#numero_inicial#numero_final#serie</param>
        private static void InutilizarNumeracao(string dadosDaInutilizacao)
        {
            var nfeFacade = GetFacade();
            if (nfeFacade == null) return;
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
                var booStatusOk = ConsultarStatusServico();

                if (!booStatusOk)
                {
                    return;
                }

                Console.WriteLine("Executando a inutilização...");
                var retornoInutilizacao = nfeFacade.InutilizarNumeracao(intAno, strCnpj, strJustificativa, intNumeroInicial, intNumeroFinal, intSerie);

                if (retornoInutilizacao.Retorno.infInut.cStat == 102)
                {
                    Console.WriteLine("#InutilizacaoEfetuada#{0}", retornoInutilizacao.Retorno.infInut.nProt);
                }
                else
                {
                    Console.WriteLine("#Erro#{0}", retornoInutilizacao.Retorno.infInut.xMotivo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro não esperado.");
                Console.WriteLine(string.Format("Detalhes:{0}", ex.Message));
            }

            #endregion
        }
        /// <summary>
        ///     Consulta o status dos webservices relacionados as informações presentes no arquivo de configuração.
        /// </summary>
        /// <returns>True - Online, False - Offline</returns>
        private static bool ConsultarStatusServico()
        {

            try
            {
                var nfeFacade = GetFacade();
                if (nfeFacade == null) return false;
                Console.WriteLine("Acessando o serviço de status da receita...");

                var retorno = nfeFacade.ConsultarStatusServico();

                if (retorno.cStat == 107)
                {
                    Console.WriteLine("");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("> Serviço em Operação");
                    Console.ResetColor();
                    return true;
                }

                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("> Serviço Indisponível");
                Console.ResetColor();
                return false;
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
        ///      Envia uma NFe para o servidor da sefaz.
        ///  </summary>
        /// <param name="dadosDoEnvio">Informações do envio no formato: path_do_arquivo_xml_da_nfe_nao_assinada#numero_do_lote.</param>
        private static void EnviarNFe(string dadosDoEnvio)
        {
            var nfeFacade = GetFacade();
            if (nfeFacade == null) return;

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
                var booStatusOk = ConsultarStatusServico();

                if (!booStatusOk)
                {
                    return;
                }

                var nfeBuilder = new NFeBuilder(strPathArquivoXml, tipoXml);
                Console.WriteLine("Preparando a NFe...");
                var retorno = nfeFacade.EnviarNFe(numeroLote, nfeBuilder.Build());
                if (retorno.Retorno.cStat == 103)
                {
                    Console.WriteLine("#NFe#" + retorno.Retorno.infRec.nRec);
                }
                else
                {
                    Console.WriteLine("#Erro#" + retorno.Retorno.xMotivo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Houve uma falha não esperada durante o processo de envio.");
                Console.WriteLine("Detalhes:");
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Imprime um NFC-e em um arquivo jpeg.
        /// </summary>
        /// <param name="dadosDaImpressao">Informações da impressão no formato: "pathDoArquivoXml#localOndeSalvarOJpeg#idToken#Csc"</param>
        private static void ImprimirNFCe(string dadosDaImpressao)
        {
            try
            {
                var dados = dadosDaImpressao.Split('#');
                GetFacade().ImprimirNFCe(dados[0], dados[1], dados[2], dados[3]);
                Console.WriteLine("Arquivo jpeg gerado com sucesso.");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Houve uma falha não esperada durante o processo de impressão.");
                Console.WriteLine("Detalhes:");
                Console.WriteLine(ex.Message);
            }           
        }

        /// <summary>
        ///  Consulta a situação de um determinado recibo de envio.
        /// </summary>
        /// <param name="nfeFacade"></param>
        /// <param name="numeroRecibo"></param>
        private static void ConsultarReciboDeEnvio(string numeroRecibo)
        {
            try
            {
                var nfeFacade = GetFacade();
                if (nfeFacade == null) return;

                var booStatusOk = ConsultarStatusServico();

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