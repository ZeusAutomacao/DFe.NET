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
using System.Collections.Concurrent;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using NFe.Classes.Servicos.Tipos;

namespace NFe.Utils.Validacao
{
    public static class Validador
    {
        private static readonly ConcurrentDictionary<string, XmlSchemaSet> _cache =
            new ConcurrentDictionary<string, XmlSchemaSet>();

        internal static string ObterArquivoSchema(ServicoNFe servicoNFe, VersaoServico versaoServico,
            bool loteNfe = true)
        {
            switch (servicoNFe)
            {
                case ServicoNFe.NfeRecepcao: return loteNfe ? "enviNFe_v2.00.xsd" : "nfe_v2.00.xsd";
                case ServicoNFe.RecepcaoEventoCancelmento: return "envEventoCancNFe_v1.00.xsd";
                case ServicoNFe.RecepcaoEventoCartaCorrecao: return "envCCe_v1.00.xsd";
                case ServicoNFe.RecepcaoEventoEpec: return "envEPEC_v1.00.xsd";
                case ServicoNFe.RecepcaoEventoManifestacaoDestinatario: return "envConfRecebto_v1.00.xsd";
                case ServicoNFe.NfeInutilizacao:
                    switch (versaoServico)
                    {
                        case VersaoServico.ve200: return "inutNFe_v2.00.xsd";
                        case VersaoServico.ve310: return "inutNFe_v3.10.xsd";
                        case VersaoServico.ve400: return "inutNFe_v4.00.xsd";
                        case VersaoServico.ve100:
                        default:
                            return null;
                    }
                case ServicoNFe.NfeConsultaProtocolo:
                    switch (versaoServico)
                    {
                        case VersaoServico.ve200: return "consSitNFe_v2.01.xsd";
                        case VersaoServico.ve310: return "consSitNFe_v3.10.xsd";
                        case VersaoServico.ve400: return "consSitNFe_v4.00.xsd";
                        case VersaoServico.ve100:
                        default:
                            return null;
                    }
                case ServicoNFe.NfeStatusServico:
                    switch (versaoServico)
                    {
                        case VersaoServico.ve200: return "consStatServ_v2.00.xsd";
                        case VersaoServico.ve310: return "consStatServ_v3.10.xsd";
                        case VersaoServico.ve400: return "consStatServ_v4.00.xsd";
                        case VersaoServico.ve100:
                        default:
                            return null;
                    }
                case ServicoNFe.NFeAutorizacao:
                    if (versaoServico != VersaoServico.ve400) return loteNfe ? "enviNFe_v3.10.xsd" : "nfe_v3.10.xsd";
                    return loteNfe ? "enviNFe_v4.00.xsd" : "nfe_v4.00.xsd";
                case ServicoNFe.NfeConsultaCadastro: return "consCad_v2.00.xsd";
                case ServicoNFe.NfeDownloadNF: return "downloadNFe_v1.00.xsd";
                case ServicoNFe.NFeDistribuicaoDFe: return "distDFeInt_v1.01.xsd"; // "distDFeInt_v1.00.xsd";
            }

            return null;
        }

        public static void Valida(ServicoNFe servicoNFe, VersaoServico versaoServico, XDocument xdoc,
            bool loteNfe = true, string pathSchema = null)
        {
            var arquivoSchema = string.Format("{0}\\{1}", pathSchema,
                ObterArquivoSchema(servicoNFe, versaoServico, loteNfe));

            // Define o tipo de validação
            var schemasCached = _cache.GetOrAdd(arquivoSchema, key =>
            {
                if (!Directory.Exists(pathSchema))
                    throw new Exception(string.Format("Diretório de Schemas não encontrado:\n{0}", pathSchema));

                // Carrega o arquivo de esquema
                var xmlSchemaSet = new XmlSchemaSet { XmlResolver = new XmlUrlResolver() };

                // Quando carregar o eschema, especificar o namespace que ele valida
                // e a localização do arquivo 
                xmlSchemaSet.Add(null, arquivoSchema);
                xmlSchemaSet.Compile();

                return xmlSchemaSet;
            });

            try
            {
                xdoc.Validate(schemasCached, null);
                //xdoc = null;
            }
            catch (XmlSchemaValidationException err)
            {
                // Um erro ocorre se o documento XML inclui caracteres ilegais
                // ou tags que não estão aninhadas corretamente
                throw new Exception(string.Format("Ocorreu o seguinte erro durante a validação XML:\n{0}",
                    err.Message));
            }
        }

        private static string GetSchemaPath(ConfiguracaoServico cfgServico)
        {
            return cfgServico == null || string.IsNullOrWhiteSpace(cfgServico.DiretorioSchemas)
                ? ConfiguracaoServico.Instancia.DiretorioSchemas
                : cfgServico.DiretorioSchemas;
        }

        public static void Valida(ServicoNFe servicoNFe, VersaoServico versaoServico, string stringXml,
            bool loteNfe = true, ConfiguracaoServico cfgServico = null)
        {
            Valida(servicoNFe, versaoServico, stringXml, loteNfe, GetSchemaPath(cfgServico));
        }

        public static void Valida(ServicoNFe servicoNFe, VersaoServico versaoServico, string stringXml,
            bool loteNfe = true, string pathSchema = null)
        {
            using (var reader = new StringReader(stringXml))
                Valida(servicoNFe, versaoServico, XDocument.Load(reader), loteNfe, pathSchema);
        }

        public static void Valida(ServicoNFe servicoNFe, VersaoServico versaoServico, Stream streamXml,
            bool loteNfe = true, ConfiguracaoServico cfgServico = null)
        {
            Valida(servicoNFe, versaoServico, streamXml, loteNfe, GetSchemaPath(cfgServico));
        }

        public static void Valida(ServicoNFe servicoNFe, VersaoServico versaoServico, Stream streamXml,
            bool loteNfe = true, string pathSchema = null)
        {
            Valida(servicoNFe, versaoServico, XDocument.Load(streamXml), loteNfe, pathSchema);
        }
    }
}