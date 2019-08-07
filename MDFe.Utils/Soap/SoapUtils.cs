/********************************************************************************/
/* Projeto: Biblioteca ZeusMDFe                                                 */
/* Biblioteca C# para emissão de Manifesto Eletrônico Fiscal de Documentos      */
/* (https://mdfe-portal.sefaz.rs.gov.br/                                        */
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
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using MDFe.Utils.Cabeçalho;
using static MDFe.Utils.Enums.Enums;

namespace MDFe.Utils.Soap
{
    /// <summary>
    /// Classe utilitária resposável pela serialização das classes em um envelope do tipo
    /// SOAP e envio das requisições para os respectivos Web Services.
    /// </summary>
    public class SoapUtils
    {
        /// <summary>
        /// Serializa a estrutura do envelope contida no objeto para um XmlDocument.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="soapEnvelope"></param>
        /// <returns></returns>
        public XmlDocument SerealizeDocument<T>(T soapEnvelope)
        {
            // instancia do objeto responsável pela serialização
            var soapserializer = new XmlSerializer(typeof(T));

            // Armazena os dados em memória para manipulação
            var memoryStream = new MemoryStream();
            var xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
            
            //Serializa o objeto de acordo com o formato
            soapserializer.Serialize(xmlTextWriter, soapEnvelope);
            xmlTextWriter.Formatting = Formatting.None;

            var xmlDocument = new XmlDocument();
            
            //Remove o caractere especial BOM (byte order mark)
            var output = Encoding.UTF8.GetString(memoryStream.ToArray());
            var _byteOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
            if (output.StartsWith(_byteOrderMarkUtf8))
            {
                output = output.Remove(0, _byteOrderMarkUtf8.Length);
            }

            //Carrega os dados na instancia do XmlDocument
            xmlDocument.LoadXml(output);

            return xmlDocument;
        }
        /// <summary>
        /// Cria e envia a requisição HttpClient, retronano a resposta obtida do WebService.
        /// </summary>
        /// <param name="xmlEnvelop"></param>
        /// <param name="certificadoDigital"></param>
        /// <param name="url"></param>
        /// <param name="consultaTipo"></param>
        /// <returns></returns>
        public async Task<string> SendRequest(XmlDocument xmlEnvelop, X509Certificate2 certificadoDigital, string url, Tipo consultaTipo)
        {
            var resposta = "";
            try
            {
                HttpClient client;

                //Url especifica do cabeçalho da requisição
                var soapUrl = new SoapUrls().GetSoapUrl(consultaTipo);

                //Inclusão do certificado requistidado para o estabelecimento da comunicação
                HttpClientHandler clientHandler;
#if NETSTANDARD
                clientHandler = new HttpClientHandler();
                clientHandler.ClientCertificates.Add(certificadoDigital);
#endif
#if NET45
                clientHandler = new WebRequestHandler();
                ((WebRequestHandler)clientHandler).ClientCertificates.Add(certificadoDigital);

#endif
                //Requisição a ser enviada.
                using (client = new HttpClient(clientHandler))
                {
                    //Mensagem da Requisição
                    var soapString = xmlEnvelop.InnerXml;
                    
                    //Cabeçalhos da Requisição
                    client.DefaultRequestHeaders.Add(HttpHeader.ACTION, soapUrl);

                    //Encapsulamento dos dados
                    var content = new StringContent(soapString, Encoding.UTF8, HttpHeader.CONTETTYPE);

                    //Envio da requisição
                    using (var response = await client.PostAsync(url, content))
                    {
                        //Resposta da Requisição
                        var soapResponse = await response.Content.ReadAsStringAsync();
                        {
                            resposta = soapResponse;
                            return resposta;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return resposta;
        }
    }

}
