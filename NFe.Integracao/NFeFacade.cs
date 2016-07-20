using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using NFe.Classes;
using NFe.Classes.Servicos.Tipos;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Utils;
using NFe.Utils.Assinatura;

namespace NFe.Integracao
{
    public class NFeFacade
    {
        public NFeFacade()
        {
            //Não há motivos para usar um arquivo externo durante o desenvolvimento
            if (System.Diagnostics.Debugger.IsAttached)
            {
                ConfiguracaoServico.Instancia.Certificado.Arquivo = "";
                ConfiguracaoServico.Instancia.Certificado.Senha = "";
                ConfiguracaoServico.Instancia.DiretorioSalvarXml = "";
                ConfiguracaoServico.Instancia.DiretorioSchemas = "";
                ConfiguracaoServico.Instancia.cUF = Estado.MG;
                ConfiguracaoServico.Instancia.ModeloDocumento = ModeloDocumento.NFe;
                ConfiguracaoServico.Instancia.SalvarXmlServicos = true;
                ConfiguracaoServico.Instancia.TimeOut = 5000;
                ConfiguracaoServico.Instancia.tpAmb = TipoAmbiente.taHomologacao;
                ConfiguracaoServico.Instancia.tpEmis = TipoEmissao.teNormal;

                return; // <------- ATENÇÃO 
            }

            string strPathArquivoConfig = "zeus.cfg";
            List<string> listArquivoConfig;
            try
            {
                listArquivoConfig = new List<string>(File.ReadAllLines(strPathArquivoConfig));
            }
            catch(Exception ex)
            {
                throw new FileNotFoundException("O arquivo zeus.cfg não foi encontrado",ex);
            }

            //=======================================================================================================================================
            //Validando o arquivo zeus.config
            string strMensagemErroArquivoConfig = "O arquivo \"zeus.cfg\" está incorreto.";
            string strArquivoConfigInline = string.Join(",", listArquivoConfig);

            if (listArquivoConfig.Count != 10) throw new InvalidOperationException(strMensagemErroArquivoConfig);
            if (!strArquivoConfigInline.Contains("Certificado.Arquivo=")) throw new InvalidOperationException(strMensagemErroArquivoConfig);
            if (!strArquivoConfigInline.Contains("Certificado.Senha=")) throw new InvalidOperationException(strMensagemErroArquivoConfig);
            if (!strArquivoConfigInline.Contains("DiretorioSalvarXml=")) throw new InvalidOperationException(strMensagemErroArquivoConfig);
            if (!strArquivoConfigInline.Contains("DiretorioSchemas=")) throw new InvalidOperationException(strMensagemErroArquivoConfig);
            if (!strArquivoConfigInline.Contains("cUF=")) throw new InvalidOperationException(strMensagemErroArquivoConfig);
            if (!strArquivoConfigInline.Contains("ModeloDocumento=")) throw new InvalidOperationException(strMensagemErroArquivoConfig);
            if (!strArquivoConfigInline.Contains("SalvarXmlServicos=")) throw new InvalidOperationException(strMensagemErroArquivoConfig);
            if (!strArquivoConfigInline.Contains("TimeOut=")) throw new InvalidOperationException(strMensagemErroArquivoConfig);
            if (!strArquivoConfigInline.Contains("tpAmb=")) throw new InvalidOperationException(strMensagemErroArquivoConfig);
            if (!strArquivoConfigInline.Contains("tpEmis=")) throw new InvalidOperationException(strMensagemErroArquivoConfig);

            foreach (string str in listArquivoConfig)
            {
                string strChave = str.Split('=')[0];
                string strValor = str.Split('=')[1];
                int intAux = 0; //Somente para poder usar o "TryParse"

                if (strChave == "tpEmis") strValor = (new List<string>(Enum.GetNames(typeof(TipoEmissao))).Contains("ta"+strValor) ? "válido" : "inválido");
                if (strChave == "cUF") strValor = (new List<string>(Enum.GetNames(typeof(Estado))).Contains(strValor) ? "válido" : "inválido");

                switch (strChave)
                {
                    case "ModeloDocumento": if (!int.TryParse(strValor,out intAux))         throw new InvalidOperationException(strMensagemErroArquivoConfig); break;
                    case "TimeOut": if (!int.TryParse(strValor, out intAux))                throw new InvalidOperationException(strMensagemErroArquivoConfig); break;
                    case "DiretorioSalvarXml": if (strValor != "SIM" && strValor != "NÃO")  throw new InvalidOperationException(strMensagemErroArquivoConfig); break;
                    case "SalvarXmlServicos": if (strValor != "SIM" && strValor != "NÃO")   throw new InvalidOperationException(strMensagemErroArquivoConfig); break;
                    case "tpAmb": if (strValor != "H" && strValor != "P")                   throw new InvalidOperationException(strMensagemErroArquivoConfig); break;
                    case "tpEmis": if(strValor == "válido")                                 throw new InvalidOperationException(strMensagemErroArquivoConfig); break;
                    case "cUF": if (strValor == "válido")                                   throw new InvalidOperationException(strMensagemErroArquivoConfig); break;
                }
            }
            //=======================================================================================================================================


            foreach (string str in listArquivoConfig)
            {
                string strChave = str.Split('=')[0];
                string strValor = str.Split('=')[1];

                switch(strChave)
                {
                    case "Certificado.Arquivo": ConfiguracaoServico.Instancia.Certificado.Arquivo = strValor; break;
                    case "Certificado.Senha": ConfiguracaoServico.Instancia.Certificado.Senha = strValor; break;
                    case "DiretorioSalvarXml": ConfiguracaoServico.Instancia.DiretorioSalvarXml = strValor; break;
                    case "DiretorioSchemas": ConfiguracaoServico.Instancia.DiretorioSchemas = strValor; break;
                    case "cUF": ConfiguracaoServico.Instancia.cUF = (Estado) Enum.Parse(typeof(Estado), strValor); break;
                    case "ModeloDocumento": ConfiguracaoServico.Instancia.ModeloDocumento = ModeloDocumento.NFe; break;
                    case "SalvarXmlServicos": ConfiguracaoServico.Instancia.SalvarXmlServicos = (strValor == "SIM"); break;
                    case "TimeOut": ConfiguracaoServico.Instancia.TimeOut = 5000; break;
                    case "tpAmb": ConfiguracaoServico.Instancia.tpAmb = (strValor == "P" ? TipoAmbiente.taProducao : TipoAmbiente.taHomologacao); break;
                    case "tpEmis": ConfiguracaoServico.Instancia.tpEmis = (TipoEmissao) Enum.Parse(typeof(TipoEmissao), strValor); break;
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

        public XmlNode ConsultarStatusServico(TipoAmbiente ambiente)
        {
            return new XmlDocument(); //TODO: Implementar "ConsultarStatusServico"
        }

        public XmlNode EnviarNFe(Classes.NFe nfe)
        {
            return new XmlDocument(); //TODO: Implementar "EnviarNFe"
        }

        public XmlNode ConsultarRecibo(string recibo)
        {
            return new XmlDocument(); //TODO: Implementar "ConsultarRecibo"
        }

        public XmlNode InutilizarNumeracao(int inicial, int final)
        {
            return new XmlDocument(); //TODO: Implementar "InutilizarNumeracao"
        }

        public XmlNode ConsultarProtocolo(string protocolo)
        {
            return new XmlDocument(); //TODO: Implementar "ConsultarProtocolo"
        }
    }
}
