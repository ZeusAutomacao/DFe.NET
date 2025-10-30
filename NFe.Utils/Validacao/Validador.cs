using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using DFe.Classes.Flags;
using DFe.Utils;
using NFe.Classes.Servicos.Evento;
using NFe.Classes.Servicos.Tipos;
using NFe.Utils.Excecoes;
using Shared.DFe.Utils;

namespace NFe.Utils.Validacao
{
    public static class Validador
    {
        internal static string ObterArquivoSchema(ServicoNFe servicoNFe, VersaoServico versaoServico, string stringXml, bool loteNfe = true)
        {
            switch (servicoNFe)
            {
                case ServicoNFe.NfeRecepcao:
                    return loteNfe ? "enviNFe_v2.00.xsd" : "nfe_v2.00.xsd";
                case ServicoNFe.RecepcaoEventoCancelmento:
                    var strEvento = FuncoesXml.ObterNodeDeStringXml(nameof(envEvento), stringXml);
                    var evento = FuncoesXml.XmlStringParaClasse<envEvento>(strEvento);
                    return evento.evento.FirstOrDefault()?.infEvento?.tpEvento ==
                           NFeTipoEvento.TeNfeCancelamentoSubstituicao
                        ? "envEventoCancSubst_v1.00.xsd"
                        : "envEventoCancNFe_v1.00.xsd";
                case ServicoNFe.RecepcaoEventoCartaCorrecao:
                    return "envCCe_v1.00.xsd";
                case ServicoNFe.RecepcaoEventoInsucessoEntregaNFe:
                    return "envEventoInsucessoNFe_v1.00.xsd";
                case ServicoNFe.RecepcaoEventoCancInsucessoEntregaNFe:
                    return "envEventoCancInsucessoNFe_v1.00.xsd";
                case ServicoNFe.RecepcaoEventoComprovanteEntregaNFe:
                    return "envEventoEntregaNFe_v1.00.xsd";
                case ServicoNFe.RecepcaoEventoCancComprovanteEntregaNFe:
                    return "envEventoCancEntregaNFe_v1.00.xsd";
                case ServicoNFe.RecepcaoEventoConciliacaoFinanceiraNFe:
                    return "envEventoEConf_v1.00.xsd";
                case ServicoNFe.RecepcaoEventoCancConciliacaoFinanceiraNFe:
                    return "envEventoCancEConf_v1.00.xsd";
                case ServicoNFe.RecepcaoEventoEpec:
                    return "envEPEC_v1.00.xsd";
                case ServicoNFe.RecepcaoEventoManifestacaoDestinatario:
                    return "envConfRecebto_v1.00.xsd";
                case ServicoNFe.NfeInutilizacao:
                    switch (versaoServico)
                    {
                        case VersaoServico.Versao200:
                            return "inutNFe_v2.00.xsd";
                        case VersaoServico.Versao310:
                            return "inutNFe_v3.10.xsd";
                        case VersaoServico.Versao400:
                            return "inutNFe_v4.00.xsd";
                    }
                    break;
                case ServicoNFe.NfeConsultaProtocolo:
                    switch (versaoServico)
                    {
                        case VersaoServico.Versao200:
                            return "consSitNFe_v2.01.xsd";
                        case VersaoServico.Versao310:
                            return "consSitNFe_v3.10.xsd";
                        case VersaoServico.Versao400:
                            return "consSitNFe_v4.00.xsd";
                    }
                    break;
                case ServicoNFe.NfeStatusServico:
                    switch (versaoServico)
                    {
                        case VersaoServico.Versao200:
                            return "consStatServ_v2.00.xsd";
                        case VersaoServico.Versao310:
                            return "consStatServ_v3.10.xsd";
                        case VersaoServico.Versao400:
                            return "consStatServ_v4.00.xsd";
                    }
                    break;
                case ServicoNFe.NFeAutorizacao:

                    if (versaoServico != VersaoServico.Versao400)
                    {
                        return loteNfe ? "enviNFe_v3.10.xsd" : "nfe_v3.10.xsd";
                    }

                    return loteNfe ? "enviNFe_v4.00.xsd" : "nfe_v4.00.xsd";
                case ServicoNFe.NfeConsultaCadastro:
                    return "consCad_v2.00.xsd";
                case ServicoNFe.NfeDownloadNF:
                    return "downloadNFe_v1.00.xsd";
                case ServicoNFe.NFeDistribuicaoDFe:
                    return "distDFeInt_v1.01.xsd"; // "distDFeInt_v1.00.xsd";
                case ServicoNFe.ConsultaGtin:
                    return "consGTIN_v1.00.xsd";
            }
            return null;
        }

        public static void Valida(ServicoNFe servicoNFe, VersaoServico versaoServico, string stringXml, bool loteNfe = true, ConfiguracaoServico cfgServico = null)
        {
            var pathSchema = String.Empty;

            if (cfgServico == null || (cfgServico != null && string.IsNullOrWhiteSpace(cfgServico.DiretorioSchemas)))
                pathSchema = ConfiguracaoServico.Instancia.DiretorioSchemas;
            else
                pathSchema = cfgServico.DiretorioSchemas;

            Valida(servicoNFe, versaoServico, stringXml, loteNfe, pathSchema);
        }

        public static string[] Valida(ServicoNFe servicoNFe, VersaoServico versaoServico, string stringXml, bool loteNfe = true, string pathSchema = null)
        {
            var falhas = new StringBuilder();

            if (!Directory.Exists(pathSchema))
                throw new Exception("Diretório de Schemas não encontrado: \n" + pathSchema);

            var arquivoSchema = Path.Combine(pathSchema, ObterArquivoSchema(servicoNFe, versaoServico, stringXml, loteNfe));

            // Define o tipo de validação
            var cfg = new XmlReaderSettings { ValidationType = ValidationType.Schema };

            // Carrega o arquivo de esquema
            var schemas = new XmlSchemaSet();
            schemas.XmlResolver = new XmlUrlResolver();

            cfg.Schemas = schemas;
            // Quando carregar o eschema, especificar o namespace que ele valida
            // e a localização do arquivo 
            schemas.Add(null, arquivoSchema);
            // Especifica o tratamento de evento para os erros de validacao
            cfg.ValidationEventHandler += delegate (object sender, ValidationEventArgs args)
            {
                string message = args.Message.ToLower().RemoverAcentos();

                if (!(
                    
                    //Está errado o schema. Pois o certo é ser 20 o length e não 28 como está no schema envIECTE_v4.00xsd
                    (message.Contains("hashtentativaentrega") && message.Contains("o comprimento atual nao e igual")) || 

                    //erro de orgaoibge que duplicou em alguns xsds porem a receita federal veio a arrumar posteriormente, mesmo assim alguns não atualizam os xsds
                    (message.Contains("tcorgaoibge") && message.Contains("ja foi declarado"))

                    //no futuro adicionar novos aqui...
                ))
                {
                    falhas.AppendLine($"[{args.Severity}] - {message} {args.Exception?.Message} " +
                                        $"na linha {args.Exception.LineNumber} " +
                                        $"posição {args.Exception.LinePosition} " +
                                        $"em {args.Exception.SourceUri}".ToString());
                }
            };

            // cria um leitor para validação
            var validator = XmlReader.Create(new StringReader(stringXml), cfg);
            try
            {
                // Faz a leitura de todos os dados XML
                while (validator.Read())
                {
                }
            }
            catch
            {
            }
            finally
            {
                validator.Close();
            }

            if (falhas.Length > 0)
                throw new ValidacaoSchemaException($"Ocorreu o seguinte erro durante a validação XML: {Environment.NewLine}{falhas}", stringXml);

            return falhas.ToString().Trim().Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }


    }
}