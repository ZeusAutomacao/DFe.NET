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
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SOAP.Handler.Configuracao;
using SOAP.Handler.Utils;

namespace SOAP.Handler.Requisicao
{
    public class RequestWsdl
    {
        /// <summary>
        /// Envio dos dados no formato SOAP para o servidor WSDL requisitado.
        /// </summary>
        /// <param name="config"></param>
        /// <returns>Task<string> resposta </returns>
        public async Task<string> SendRequest(SoapConfig config)
        {
            var resposta = string.Empty;
            try
            {
                var url = config.Url;
                HttpClient client;

                //Url especifica do cabeçalho da requisição.
                var soapUrl = url;

                //Inclusão do certificado requistidado para o estabelecimento da comunicação.
                HttpClientHandler clientHandler;
                
                //Meio de inclusão do certificado para o Target .NET STANDARD.
#if NETSTANDARD
                clientHandler = new HttpClientHandler();
                clientHandler.ClientCertificates.Add(config.Certificado);
#endif
                //Meio de inclusão do certificado para o Target .NET FRAMEWORK 4.5.
#if NET45
                clientHandler = new WebRequestHandler();
                ((WebRequestHandler)clientHandler).ClientCertificates.Add(config.Certificado);

#endif
                //Requisição a ser enviada.
                using (client = new HttpClient(clientHandler))
                {
                    //Construção do envelope SOAP a ser enviado.
                    var soapString = new Envelope().Construir(config);

                    //Cabeçalhos da Requisição.
                    client.DefaultRequestHeaders.Add(HttpHeader.ACTION, soapUrl);

                    //Encapsulamento dos dados.
                    var content = new StringContent(soapString, Encoding.UTF8, HttpHeader.CONTETTYPE);

                    //Envio da requisição.
                    using (var response = await client.PostAsync(url, content))
                    {
                        //Resposta da Requisição.
                        var soapResponse = await response.Content.ReadAsStringAsync();
                        {
                            return soapResponse;
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
