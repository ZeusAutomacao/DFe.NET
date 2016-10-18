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
using System.Text;
using System.IO;
using System.Collections.Generic;
using NFe.Classes;
using NFe.Classes.Servicos.Tipos;
using NFe.Classes.Servicos.Status;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Servicos;
using NFe.Servicos.Retorno;
using NFe.Utils;
using NFe.Utils.NFe;

namespace NFe.Integracao
{
    public class NFeFacade
    {
        //Nome e path do arquivo de configurações a ser considerado na execução do app.
        private readonly string _nomeArquivoConfiguracoes;
        private readonly string _pathArquivoConfiguracoes;

        public NFeFacade(bool booPularCarregamentoDoArquivo = false)
        {
            _nomeArquivoConfiguracoes = "zeus.cfg";
            _pathArquivoConfiguracoes = @"{caminho}\{zeus}".Replace("{caminho}", Directory.GetCurrentDirectory()).Replace("{zeus}", _nomeArquivoConfiguracoes);

            if (!booPularCarregamentoDoArquivo) CarregarArquivoDeConfiguracoes();
        }

        public retConsStatServ ConsultarStatusServico()
        {
            var servicoNFe = new ServicosNFe(ConfiguracaoServico.Instancia);
            return servicoNFe.NfeStatusServico().Retorno;
        }

        public RetornoNFeAutorizacao EnviarNFe(Int32 numLote, Classes.NFe nfe)
        {
            nfe.Assina(); //não precisa validar aqui, pois o lote será validado em ServicosNFe.NFeAutorizacao
            var servicoNFe = new ServicosNFe(ConfiguracaoServico.Instancia);
            return servicoNFe.NFeAutorizacao(numLote,IndicadorSincronizacao.Assincrono, new List<Classes.NFe> { nfe });
        }

        public RetornoNFeRetAutorizacao ConsultarReciboDeEnvio(string recibo)
        {
            var servicoNFe = new ServicosNFe(ConfiguracaoServico.Instancia);
            return servicoNFe.NFeRetAutorizacao(recibo);
        }

        public RetornoRecepcaoEvento CancelarNFe(string cnpjEmitente, int numeroLote, short sequenciaEvento, string chaveAcesso, string protocolo, string justificativa)
        {
            var servicoNFe = new ServicosNFe(ConfiguracaoServico.Instancia);
            return servicoNFe.RecepcaoEventoCancelamento(numeroLote, sequenciaEvento, protocolo, chaveAcesso, justificativa, cnpjEmitente);
        }

        public RetornoNfeInutilizacao InutilizarNumeracao(int ano, string cnpj, string justificativa, int numeroInicial, int numeroFinal, int serie)
        {
            var servicoNFe = new ServicosNFe(ConfiguracaoServico.Instancia);
            return servicoNFe.NfeInutilizacao(cnpj, Convert.ToInt16(ano.ToString().Substring(2,2)), ConfiguracaoServico.Instancia.ModeloDocumento, Convert.ToInt16(serie), Convert.ToInt32(numeroInicial), Convert.ToInt32(numeroFinal), justificativa);
        }

        private void CarregarArquivoDeConfiguracoes()
        {
            //Não há motivos para usar um arquivo externo durante o desenvolvimento
            if (System.Diagnostics.Debugger.IsAttached)
            {
                ConfiguracaoServico.Instancia.Certificado.Arquivo = string.Empty;
                ConfiguracaoServico.Instancia.Certificado.Senha = string.Empty;
                ConfiguracaoServico.Instancia.DiretorioSalvarXml = string.Empty;
                ConfiguracaoServico.Instancia.DiretorioSchemas = string.Empty;
                ConfiguracaoServico.Instancia.cUF = Estado.MG;
                ConfiguracaoServico.Instancia.ModeloDocumento = ModeloDocumento.NFe;
                ConfiguracaoServico.Instancia.SalvarXmlServicos = true;
                ConfiguracaoServico.Instancia.TimeOut = 5000;
                ConfiguracaoServico.Instancia.tpAmb = TipoAmbiente.taHomologacao;
                ConfiguracaoServico.Instancia.tpEmis = TipoEmissao.teNormal;

                ConfiguracaoServico.Instancia.VersaoNfceAministracaoCSC = VersaoServico.ve310;
                ConfiguracaoServico.Instancia.VersaoNFeAutorizacao = VersaoServico.ve310;
                ConfiguracaoServico.Instancia.VersaoNfeConsultaCadastro = VersaoServico.ve310;
                ConfiguracaoServico.Instancia.VersaoNfeConsultaDest = VersaoServico.ve310;
                ConfiguracaoServico.Instancia.VersaoNfeConsultaProtocolo = VersaoServico.ve310;
                ConfiguracaoServico.Instancia.VersaoNFeDistribuicaoDFe = VersaoServico.ve310;
                ConfiguracaoServico.Instancia.VersaoNfeDownloadNF = VersaoServico.ve310;
                ConfiguracaoServico.Instancia.VersaoNfeInutilizacao = VersaoServico.ve310;
                ConfiguracaoServico.Instancia.VersaoNfeRecepcao = VersaoServico.ve310;
                ConfiguracaoServico.Instancia.VersaoNFeRetAutorizacao = VersaoServico.ve310;
                ConfiguracaoServico.Instancia.VersaoNfeRetRecepcao = VersaoServico.ve310;
                ConfiguracaoServico.Instancia.VersaoNfeStatusServico = VersaoServico.ve310;
                ConfiguracaoServico.Instancia.VersaoRecepcaoEventoCceCancelamento = VersaoServico.ve310;

                return; // <------- ATENÇÃO 
            }

            var strPathArquivoConfig = @"{caminho}\{zeus}".Replace("{caminho}", Directory.GetCurrentDirectory()).Replace("{zeus}", _nomeArquivoConfiguracoes);
            var listArquivoConfig = new List<string>();
            try
            {
                var listArquivoConfigCompleto = new List<string>(File.ReadAllLines(strPathArquivoConfig));
                foreach (var str in listArquivoConfigCompleto)
                {
                    //Eliminando comentários e linhas em branco
                    if (str.Trim() != "" && str.Substring(0, 1) != "#") listArquivoConfig.Add(str);
                }
            }
            catch (Exception ex)
            {
                throw new FileNotFoundException(string.Format("O arquivo {0} não foi encontrado",_nomeArquivoConfiguracoes), ex);
            }

            //=======================================================================================================================================
            //Validando o arquivo de configuração
            var strMensagemErroArquivoConfig = string.Format("O arquivo \"{0}\" está incorreto.",_nomeArquivoConfiguracoes);
            var strArquivoConfigInline = string.Join(",", listArquivoConfig).ToLower();

            if (listArquivoConfig.Count != 10) throw new InvalidOperationException(strMensagemErroArquivoConfig);
            if (!strArquivoConfigInline.Contains("certificado_arquivo=")) throw new InvalidOperationException(strMensagemErroArquivoConfig);
            if (!strArquivoConfigInline.Contains("certificado_senha=")) throw new InvalidOperationException(strMensagemErroArquivoConfig);
            if (!strArquivoConfigInline.Contains("diretorio_xml=")) throw new InvalidOperationException(strMensagemErroArquivoConfig);
            if (!strArquivoConfigInline.Contains("diretorio_schemas=")) throw new InvalidOperationException(strMensagemErroArquivoConfig);
            if (!strArquivoConfigInline.Contains("estado_emitente=")) throw new InvalidOperationException(strMensagemErroArquivoConfig);
            if (!strArquivoConfigInline.Contains("modelo_documento=")) throw new InvalidOperationException(strMensagemErroArquivoConfig);
            if (!strArquivoConfigInline.Contains("salvar_xml_servicos=")) throw new InvalidOperationException(strMensagemErroArquivoConfig);
            if (!strArquivoConfigInline.Contains("time_out=")) throw new InvalidOperationException(strMensagemErroArquivoConfig);
            if (!strArquivoConfigInline.Contains("tipo_ambiente=")) throw new InvalidOperationException(strMensagemErroArquivoConfig);
            if (!strArquivoConfigInline.Contains("tipo_emissao=")) throw new InvalidOperationException(strMensagemErroArquivoConfig);

            foreach (var str in listArquivoConfig)
            {
                var strChave = str.Split('=')[0].ToLower();
                var strValor = str.Split('=')[1].ToLower();
                int intAux; //Somente para poder usar o "TryParse"

                if (strChave == "tipo_emissao") strValor = (new List<string>(Enum.GetNames(typeof(TipoEmissao))).Contains("ta" + strValor) ? "válido" : "inválido");
                if (strChave == "estado_emitente") strValor = (new List<string>(Enum.GetNames(typeof(Estado))).Contains(strValor) ? "válido" : "inválido");

                switch (strChave)
                {
                    case "modelo_documento": if (!int.TryParse(strValor, out intAux)) throw new InvalidOperationException(strMensagemErroArquivoConfig); break;
                    case "time_out": if (!int.TryParse(strValor, out intAux)) throw new InvalidOperationException(strMensagemErroArquivoConfig); break;
                    case "salvar_xml_servicos": if (strValor != "sim" && strValor != "nao" && strValor != "não") throw new InvalidOperationException(strMensagemErroArquivoConfig); break;
                    case "tipo_ambiente": if (strValor != "h" && strValor != "p") throw new InvalidOperationException(strMensagemErroArquivoConfig); break;
                    case "tipo_emissao": if (strValor == "válido") throw new InvalidOperationException(strMensagemErroArquivoConfig); break;
                    case "estado_emitente": if (strValor == "válido") throw new InvalidOperationException(strMensagemErroArquivoConfig); break;
                }
            }
            //=======================================================================================================================================


            foreach (var str in listArquivoConfig)
            {
                var strChave = str.Split('=')[0];
                var strValor = str.Split('=')[1];

                switch (strChave)
                {
                    case "certificado_arquivo": ConfiguracaoServico.Instancia.Certificado.Arquivo = strValor; break;
                    case "certificado_senha": ConfiguracaoServico.Instancia.Certificado.Senha = strValor; break;
                    case "diretorio_xml": ConfiguracaoServico.Instancia.DiretorioSalvarXml = strValor; break;
                    case "diretorio_schemas": ConfiguracaoServico.Instancia.DiretorioSchemas = strValor; break;
                    case "estado_emitente": ConfiguracaoServico.Instancia.cUF = (Estado)Enum.Parse(typeof(Estado), strValor); break;
                    case "modelo_documento": ConfiguracaoServico.Instancia.ModeloDocumento = (strValor == "55" ? ModeloDocumento.NFe : ModeloDocumento.NFCe); break;
                    case "salvar_xml_servicos": ConfiguracaoServico.Instancia.SalvarXmlServicos = (strValor == "sim"); break;
                    case "time_out": ConfiguracaoServico.Instancia.TimeOut = 5000; break;
                    case "tipo_ambiente": ConfiguracaoServico.Instancia.tpAmb = (strValor == "p" ? TipoAmbiente.taProducao : TipoAmbiente.taHomologacao); break;
                    case "tipo_emissao": ConfiguracaoServico.Instancia.tpEmis = (TipoEmissao)Enum.Parse(typeof(TipoEmissao), strValor); break;
                }
            }

            //Versão atual da NFe/NFCe: 3.10
            ConfiguracaoServico.Instancia.VersaoNfceAministracaoCSC = VersaoServico.ve310;
            ConfiguracaoServico.Instancia.VersaoNFeAutorizacao = VersaoServico.ve310;
            ConfiguracaoServico.Instancia.VersaoNfeConsultaCadastro = VersaoServico.ve310;
            ConfiguracaoServico.Instancia.VersaoNfeConsultaDest = VersaoServico.ve310;
            ConfiguracaoServico.Instancia.VersaoNfeConsultaProtocolo = VersaoServico.ve310;
            ConfiguracaoServico.Instancia.VersaoNFeDistribuicaoDFe = VersaoServico.ve310;
            ConfiguracaoServico.Instancia.VersaoNfeDownloadNF = VersaoServico.ve310;
            ConfiguracaoServico.Instancia.VersaoNfeInutilizacao = VersaoServico.ve310;
            ConfiguracaoServico.Instancia.VersaoNfeRecepcao = VersaoServico.ve310;
            ConfiguracaoServico.Instancia.VersaoNFeRetAutorizacao = VersaoServico.ve310;
            ConfiguracaoServico.Instancia.VersaoNfeRetRecepcao = VersaoServico.ve310;
            ConfiguracaoServico.Instancia.VersaoNfeStatusServico = VersaoServico.ve310;
            ConfiguracaoServico.Instancia.VersaoRecepcaoEventoCceCancelamento = VersaoServico.ve310;
        }

        public void CriarArquivoDeConfiguracoes()
        {
            var strBuilderArquivoConfiguracoes = new StringBuilder();
            strBuilderArquivoConfiguracoes.AppendLine("#===========================================================================================");
            strBuilderArquivoConfiguracoes.AppendLine("#ARQUIVO DE CONFIGURAÇÕES DO ZEUS");
            strBuilderArquivoConfiguracoes.AppendLine("#");
            strBuilderArquivoConfiguracoes.AppendLine("#- Estrutura esperada -");
            strBuilderArquivoConfiguracoes.AppendLine("#");
            strBuilderArquivoConfiguracoes.AppendLine("#certificado_arquivo={Caminho do arquivo .pfx}");
            strBuilderArquivoConfiguracoes.AppendLine("#certificado_senha={Senha do certificado}");
            strBuilderArquivoConfiguracoes.AppendLine("#diretorio_xml={Caminho do diretório onde serão salvos os xml}");
            strBuilderArquivoConfiguracoes.AppendLine("#diretorio_schemas={Diretório onde estão os arquivos .xsd}");
            strBuilderArquivoConfiguracoes.AppendLine("#estado_emitente={Código IBGE do estado do emitente}");
            strBuilderArquivoConfiguracoes.AppendLine("#modelo_documento={Modelo do documento}");
            strBuilderArquivoConfiguracoes.AppendLine("#salvar_xml_servicos={SIM ou NAO}");
            strBuilderArquivoConfiguracoes.AppendLine("#time_out={Tempo de espera máximo dos serviços da sefaz em milisegundos}");
            strBuilderArquivoConfiguracoes.AppendLine("#tipo_ambiente={P ou H}");
            strBuilderArquivoConfiguracoes.AppendLine("#tipo_emissao={1, 2, 3, 4, 5, 6, 7 ou 9}");
            strBuilderArquivoConfiguracoes.AppendLine("#");
            strBuilderArquivoConfiguracoes.AppendLine("#");
            strBuilderArquivoConfiguracoes.AppendLine("# - ATENÇÃO -");
            strBuilderArquivoConfiguracoes.AppendLine("#");
            strBuilderArquivoConfiguracoes.AppendLine("#Linhas em branco ou iniciadas por # serão ignoradas.");
            strBuilderArquivoConfiguracoes.AppendLine("#Os tipos de emissao possíveis são:");
            strBuilderArquivoConfiguracoes.AppendLine("#1 - Normal");
            strBuilderArquivoConfiguracoes.AppendLine("#2 - FS-IA");
            strBuilderArquivoConfiguracoes.AppendLine("#3 - SCAN");
            strBuilderArquivoConfiguracoes.AppendLine("#4 - EPEC");
            strBuilderArquivoConfiguracoes.AppendLine("#5 - FS-DA");
            strBuilderArquivoConfiguracoes.AppendLine("#6 - SVC-AN");
            strBuilderArquivoConfiguracoes.AppendLine("#7 - SVC-RS");
            strBuilderArquivoConfiguracoes.AppendLine("#9 - Offline");
            strBuilderArquivoConfiguracoes.AppendLine("#===========================================================================================");
            strBuilderArquivoConfiguracoes.AppendLine("");
            strBuilderArquivoConfiguracoes.AppendLine("certificado_arquivo=");
            strBuilderArquivoConfiguracoes.AppendLine("certificado_senha=");
            strBuilderArquivoConfiguracoes.AppendLine("diretorio_xml=");
            strBuilderArquivoConfiguracoes.AppendLine("diretorio_schemas=");
            strBuilderArquivoConfiguracoes.AppendLine("estado_emitente=");
            strBuilderArquivoConfiguracoes.AppendLine("modelo_documento=");
            strBuilderArquivoConfiguracoes.AppendLine("salvar_xml_servicos=");
            strBuilderArquivoConfiguracoes.AppendLine("time_out=");
            strBuilderArquivoConfiguracoes.AppendLine("tipo_ambiente=");
            strBuilderArquivoConfiguracoes.AppendLine("tipo_emissao=");

            try
            {
                File.WriteAllText(_pathArquivoConfiguracoes, strBuilderArquivoConfiguracoes.ToString(), Encoding.Unicode);
            }
            catch(UnauthorizedAccessException ex)
            {
                var strMensagem = string.Format("Não foi possível criar o arquivo {0}. Você não tem as permissões necessárias.", _nomeArquivoConfiguracoes);
                throw new InvalidOperationException(strMensagem, ex);
            }
            catch(PathTooLongException ex)
            {
                var strMensagem = string.Format("Não foi possível criar o arquivo {0}. O caminho até o diretório atual é muito longo.", _nomeArquivoConfiguracoes);
                throw new InvalidOperationException(strMensagem, ex);
            }
            catch(Exception ex)
            {
                var strMensagem = string.Format("Não foi possível criar o arquivo {0}. Ocorreu um erro inesperado.", _nomeArquivoConfiguracoes);
                throw new InvalidOperationException(strMensagem, ex);
            }
        }

        public void AlterarArquivoDeConfiguracoes(string strChave, string strValor)
        {
            List<string> listArquivo;

            try
            {
                listArquivo = new List<string>(File.ReadAllLines(_pathArquivoConfiguracoes, Encoding.Unicode));
            }
            catch (UnauthorizedAccessException ex)
            {
                var strMensagem = string.Format("Não foi possível alterar o arquivo {0}. Você não tem as permissões necessárias.", _nomeArquivoConfiguracoes);
                throw new InvalidOperationException(strMensagem, ex);
            }
            catch (PathTooLongException ex)
            {
                var strMensagem = string.Format("Não foi possível alterar o arquivo {0}. O caminho até o diretório atual é muito longo.", _nomeArquivoConfiguracoes);
                throw new InvalidOperationException(strMensagem, ex);
            }
            catch (Exception ex)
            {
                var strMensagem = string.Format("Não foi possível alterar o arquivo {0}. Ocorreu um erro inesperado.", _nomeArquivoConfiguracoes);
                throw new InvalidOperationException(strMensagem, ex);
            }

            var builderStrNovoArquivo = new StringBuilder();

            foreach(var str in listArquivo)
            {
                string strLinha;

                if (str.Split('=')[0] == strChave && str.Substring(0,1) != "#")
                    strLinha = string.Format("{0}={1}", strChave, strValor);
                else
                    strLinha = str;

                builderStrNovoArquivo.AppendLine(strLinha); 
            }

            try
            {
                File.WriteAllText(_pathArquivoConfiguracoes, builderStrNovoArquivo.ToString(), Encoding.Unicode);
            }
            catch (UnauthorizedAccessException ex)
            {
                var strMensagem = string.Format("Não foi possível alterar o arquivo {0}. Você não tem as permissões necessárias.", _nomeArquivoConfiguracoes);
                throw new InvalidOperationException(strMensagem, ex);
            }
            catch (PathTooLongException ex)
            {
                var strMensagem = string.Format("Não foi possível alterar o arquivo {0}. O caminho até o diretório atual é muito longo.", _nomeArquivoConfiguracoes);
                throw new InvalidOperationException(strMensagem, ex);
            }
            catch (Exception ex)
            {
                var strMensagem = string.Format("Não foi possível alterar o arquivo {0}. Ocorreu um erro inesperado.", _nomeArquivoConfiguracoes);
                throw new InvalidOperationException(strMensagem, ex);
            }
        }

        public List<string> CapturarConteudoArquivoDeConfiguracoes()
        {
            return new List<string>(File.ReadAllLines(_pathArquivoConfiguracoes, Encoding.Unicode));
        }
    }
}