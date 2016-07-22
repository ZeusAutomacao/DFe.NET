using System;
using System.Text;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using NFe.Classes;
using NFe.Classes.Servicos.Tipos;
using NFe.Classes.Servicos.Status;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Servicos;
using NFe.Utils;

namespace NFe.Integracao
{
    public class NFeFacade
    {
        //Nome do arquivo de configurações a ser considerado na execução do app.
        private string NomeArquivoConfiguracoes = "zeus.cfg"; 

        public NFeFacade(bool booPularCarregamentoDoArquivo = false)
        {
            if (!booPularCarregamentoDoArquivo) this.CarregarArquivoDeConfiguracoes();
        }

        public retConsStatServ ConsultarStatusServico(TipoAmbiente ambiente)
        {
            var servicoNFe = new ServicosNFe(ConfiguracaoServico.Instancia);
            return servicoNFe.NfeStatusServico().Retorno;
        }

        public XmlNode EnviarNFe(Classes.NFe nfe)
        {
            return new XmlDocument(); //TODO: Implementar "EnviarNFe"
        }

        public XmlNode ConsultarRecibo(string recibo)
        {
            return new XmlDocument(); //TODO: Implementar "ConsultarRecibo"
        }

        public XmlNode CancelarNFe(string chaveAcesso, string protocolo)
        {
            return new XmlDocument(); //TODO: Implementar "CancelarNFe"
        }

        public XmlNode InutilizarNumeracao(int inicial, int final)
        {
            return new XmlDocument(); //TODO: Implementar "InutilizarNumeracao"
        }

        private void CarregarArquivoDeConfiguracoes()
        {
            //Não há motivos para usar um arquivo externo durante o desenvolvimento
            if (System.Diagnostics.Debugger.IsAttached)
            {
                ConfiguracaoServico.Instancia.Certificado.Arquivo = @"C:\WiaTI\certificado_amorim.pfx";
                ConfiguracaoServico.Instancia.Certificado.Senha = "jms2477";
                ConfiguracaoServico.Instancia.DiretorioSalvarXml = @"D:\Temp";
                ConfiguracaoServico.Instancia.DiretorioSchemas = @"D:\Temp\Schemas";
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
                ConfiguracaoServico.Instancia.VersaoRecepcaoEvento = VersaoServico.ve310;

                return; // <------- ATENÇÃO 
            }

            string strPathArquivoConfig = @"{caminho}\{zeus}".Replace("{caminho}", Directory.GetCurrentDirectory()).Replace("{zeus}", this.NomeArquivoConfiguracoes);
            List<string> listArquivoConfig = new List<string>();
            List<string> listArquivoConfigCompleto;
            try
            {
                listArquivoConfigCompleto = new List<string>(File.ReadAllLines(strPathArquivoConfig));
                foreach (string str in listArquivoConfigCompleto)
                {
                    //Eliminando comentários e linhas em branco
                    if (str.Trim() != "" && str.Substring(0, 1) != "#") listArquivoConfig.Add(str);
                }
            }
            catch (Exception ex)
            {
                throw new FileNotFoundException(string.Format("O arquivo {0} não foi encontrado",this.NomeArquivoConfiguracoes), ex);
            }

            //=======================================================================================================================================
            //Validando o arquivo de configuração
            string strMensagemErroArquivoConfig = string.Format("O arquivo \"{0}\" está incorreto.",this.NomeArquivoConfiguracoes);
            string strArquivoConfigInline = string.Join(",", listArquivoConfig);

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

            foreach (string str in listArquivoConfig)
            {
                string strChave = str.Split('=')[0];
                string strValor = str.Split('=')[1];
                int intAux = 0; //Somente para poder usar o "TryParse"

                if (strChave == "tipo_emissao") strValor = (new List<string>(Enum.GetNames(typeof(TipoEmissao))).Contains("ta" + strValor) ? "válido" : "inválido");
                if (strChave == "estado_emitente") strValor = (new List<string>(Enum.GetNames(typeof(Estado))).Contains(strValor) ? "válido" : "inválido");

                switch (strChave)
                {
                    case "modelo_documento": if (!int.TryParse(strValor, out intAux)) throw new InvalidOperationException(strMensagemErroArquivoConfig); break;
                    case "time_out": if (!int.TryParse(strValor, out intAux)) throw new InvalidOperationException(strMensagemErroArquivoConfig); break;
                    case "salvar_xml_servicos": if (strValor != "SIM" && strValor != "NÃO") throw new InvalidOperationException(strMensagemErroArquivoConfig); break;
                    case "tipo_ambiente": if (strValor != "H" && strValor != "P") throw new InvalidOperationException(strMensagemErroArquivoConfig); break;
                    case "tipo_emissao": if (strValor == "válido") throw new InvalidOperationException(strMensagemErroArquivoConfig); break;
                    case "estado_emitente": if (strValor == "válido") throw new InvalidOperationException(strMensagemErroArquivoConfig); break;
                }
            }
            //=======================================================================================================================================


            foreach (string str in listArquivoConfig)
            {
                string strChave = str.Split('=')[0];
                string strValor = str.Split('=')[1];

                switch (strChave)
                {
                    case "certificado_arquivo": ConfiguracaoServico.Instancia.Certificado.Arquivo = strValor; break;
                    case "certificado_senha": ConfiguracaoServico.Instancia.Certificado.Senha = strValor; break;
                    case "diretorio_xml": ConfiguracaoServico.Instancia.DiretorioSalvarXml = strValor; break;
                    case "diretorio_schemas": ConfiguracaoServico.Instancia.DiretorioSchemas = strValor; break;
                    case "estado_emitente": ConfiguracaoServico.Instancia.cUF = (Estado)Enum.Parse(typeof(Estado), strValor); break;
                    case "modelo_documento": ConfiguracaoServico.Instancia.ModeloDocumento = ModeloDocumento.NFe; break;
                    case "salvar_xml_servicos": ConfiguracaoServico.Instancia.SalvarXmlServicos = (strValor == "SIM"); break;
                    case "time_out": ConfiguracaoServico.Instancia.TimeOut = 5000; break;
                    case "tipo_ambiente": ConfiguracaoServico.Instancia.tpAmb = (strValor == "P" ? TipoAmbiente.taProducao : TipoAmbiente.taHomologacao); break;
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
            ConfiguracaoServico.Instancia.VersaoRecepcaoEvento = VersaoServico.ve310;
        }

        public void CriarArquivoDeConfiguracoes()
        {
            StringBuilder strBuilderArquivoConfiguracoes = new StringBuilder();
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
                string strPathArquivoConfiguracoes = @"{caminho}\{zeus}".Replace("{caminho}", Directory.GetCurrentDirectory()).Replace("{zeus}", NomeArquivoConfiguracoes);
                File.WriteAllText(strPathArquivoConfiguracoes, strBuilderArquivoConfiguracoes.ToString(), Encoding.Unicode);
            }
            catch(UnauthorizedAccessException ex)
            {
                string strMensagem = string.Format("Não foi possível criar o arquivo {0}. Você não tem as permissões necessárias.", NomeArquivoConfiguracoes);
                throw new InvalidOperationException(strMensagem, ex);
            }
            catch(PathTooLongException ex)
            {
                string strMensagem = string.Format("Não foi possível criar o arquivo {0}. O caminho até o diretório atual é muito longo.", NomeArquivoConfiguracoes);
                throw new InvalidOperationException(strMensagem, ex);
            }
            catch(Exception ex)
            {
                string strMensagem = string.Format("Não foi possível criar o arquivo {0}. Ocorreu um erro inesperado.", this.NomeArquivoConfiguracoes);
                throw new InvalidOperationException(strMensagem, ex);
            }
        }
    }
}
