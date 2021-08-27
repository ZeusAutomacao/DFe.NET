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
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using DFe.Classes.Flags;
using DFe.Utils;
using NFe.Classes.Servicos.Evento;
using NFe.Classes.Servicos.Tipos;
using NFe.Utils.Excecoes;

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
                falhas.AppendLine($"[{args.Severity}] - {args.Message} {args.Exception?.Message} na linha {args.Exception.LineNumber} posição {args.Exception.LinePosition} em {args.Exception.SourceUri}".ToString());
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
                throw new ValidacaoSchemaException($"Ocorreu o seguinte erro durante a validação XML: {Environment.NewLine}{falhas}");

            return falhas.ToString().Trim().Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }


    }
}