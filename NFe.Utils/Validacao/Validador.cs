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
using System.Xml;
using System.Xml.Schema;
using NFe.Classes.Servicos.Tipos;

namespace NFe.Utils.Validacao
{
    public static class Validador
    {
        internal static string ObterArquivoSchema(ServicoNFe servicoNFe, TipoRecepcaoEvento tipoRecepcaoEvento, VersaoServico versaoServico, Boolean loteNfe = true)
        {
            switch (servicoNFe)
            {
                case ServicoNFe.NfeRecepcao:
                    return loteNfe ? "enviNFe_v2.00.xsd" : "nfe_v2.00.xsd";
                case ServicoNFe.RecepcaoEvento:
                    switch (tipoRecepcaoEvento)
                    {
                        case TipoRecepcaoEvento.Cancelmento:
                            return "envEventoCancNFe_v1.00.xsd";
                        case TipoRecepcaoEvento.CartaCorrecao:
                            return "envCCe_v1.00.xsd";
                        case TipoRecepcaoEvento.Epec:
                            return "envEPEC_v1.00.xsd";
                    }
                    break;
                case ServicoNFe.NfeInutilizacao:
                    switch (versaoServico)
                    {
                        case VersaoServico.ve200:
                            return "inutNFe_v2.00.xsd";
                        case VersaoServico.ve310:
                            return "inutNFe_v3.10.xsd";
                    }
                    break;
                case ServicoNFe.NfeConsultaProtocolo:
                    switch (versaoServico)
                    {
                        case VersaoServico.ve200:
                            return "consSitNFe_v2.01.xsd";
                        case VersaoServico.ve310:
                            return "consSitNFe_v3.10.xsd";
                    }
                    break;
                case ServicoNFe.NfeStatusServico:
                    switch (versaoServico)
                    {
                        case VersaoServico.ve200:
                            return "consStatServ_v2.00.xsd";
                        case VersaoServico.ve310:
                            return "consStatServ_v3.10.xsd";
                    }
                    break;
                case ServicoNFe.NFeAutorizacao:
                    return loteNfe ? "enviNFe_v3.10.xsd" : "nfe_v3.10.xsd";
                case ServicoNFe.NfeConsultaCadastro:
                    return "consCad_v2.00.xsd";
            }
            return null;
        }

        public static void Valida(ServicoNFe servicoNFe, TipoRecepcaoEvento tipoRecepcaoEvento, VersaoServico versaoServico, string stringXml, Boolean loteNfe = true)
        {
            var pathSchema = ConfiguracaoServico.Instancia.DiretorioSchemas;

            if (!Directory.Exists(pathSchema))
                throw new Exception("Diretório de Schemas não encontrado: \n" + pathSchema);

            var arquivoSchema = pathSchema + @"\" + ObterArquivoSchema(servicoNFe, tipoRecepcaoEvento, versaoServico, loteNfe);

            // Define o tipo de validação
            var cfg = new XmlReaderSettings {ValidationType = ValidationType.Schema};

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