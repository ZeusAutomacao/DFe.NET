using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using CTeDLL.Classes.Servicos.Tipos;

namespace CTeDLL.Utils.Validacao
{
    public static class Validador
    {
        internal static string ObterArquivoSchema(ServicoCTe servicoCTe, versao versao, bool loteNfe = true)
        {
            switch (servicoCTe)
            {
                case ServicoCTe.CteRecepcao:
                    return loteNfe ? "enviCte_v2.00.xsd" : "cte_v2.00.xsd";
                case ServicoCTe.RecepcaoEventoCancelmento:
                    return "evCancCTe_v2.00.xsd";
                case ServicoCTe.RecepcaoEventoCartaCorrecao:
                    return "evCCeCTe_v2.00.xsd";
                case ServicoCTe.RecepcaoEventoEpec:
                    return "evEPECCTe_v2.00.xsd";
                case ServicoCTe.RecepcaoEventoManifestacaoDestinatario:
                    return "envConfRecebto_v1.00.xsd";
                case ServicoCTe.CteInutilizacao:
                    switch (versao)
                    {
                        case versao.ve200:
                            return "inutCTe_v2.00.xsd";
                        case versao.ve300:
                            return "inutCTe_v3.00.xsd";
                    }
                    break;
                case ServicoCTe.CteConsultaProtocolo:
                    switch (versao)
                    {
                        case versao.ve200:
                            return "consSitCte_v2.00.xsd";
                        case versao.ve300:
                            return "consSitCTe_v3.00.xsd";
                    }
                    break;
                case ServicoCTe.CteStatusServico:
                    switch (versao)
                    {
                        case versao.ve200:
                            return "consStatServCte_v2.00.xsd";
                        case versao.ve300:
                            return "consStatServCTe_v3.00.xsd";
                    }
                    break;
            }
            return null;
        }

        public static void Valida(ServicoCTe servicoCTe, versao versao, string stringXml, bool loteNfe = true, string pathSchema = "")
        {
            // todo if (string.IsNullOrEmpty(pathSchema))
               // todo  pathSchema = ConfiguracaoServico.Instancia.DiretorioSchemas;

            if (!Directory.Exists(pathSchema))
                throw new Exception("Diretório de Schemas não encontrado: \n" + pathSchema);

            var arquivoSchema = pathSchema + @"\" + ObterArquivoSchema(servicoCTe, versao, loteNfe);

            // Define o tipo de validação
            var cfg = new XmlReaderSettings { ValidationType = ValidationType.Schema };

            // Carrega o arquivo de esquema
            var schemas = new XmlSchemaSet();
            cfg.Schemas = schemas;
            // Quando carregar o eschema, especificar o namespace que ele valida
            // e a localização do arquivo 
            schemas.Add(null, arquivoSchema);
            // Especifica o tratamento de evento para os erros de validacao
            cfg.ValidationEventHandler += ValidationEventHandler;
            // cria um leitor para validação
            var validator = XmlReader.Create(new StringReader(stringXml), cfg);
            try
            {
                // Faz a leitura de todos os dados XML
                while (validator.Read())
                {
                }
            }
            catch (XmlException err)
            {
                // Um erro ocorre se o documento XML inclui caracteres ilegais
                // ou tags que não estão aninhadas corretamente
                throw new Exception("Ocorreu o seguinte erro durante a validação XML:" + "\n" + err.Message);
            }
            finally
            {
                validator.Close();
            }
        }

        internal static void ValidationEventHandler(object sender, ValidationEventArgs args)
        {
            throw new Exception("Erros da validação : " + args.Message);
        }
    }
}