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

using System.Text;
using SOAP.Handler.Configuracao;

namespace SOAP.Handler.Requisicao
{
    public class Envelope
    {
        /// <summary>
        /// Construção do envelope SOAP necessário para o estabelecimento de comunicação com os servidores WSDL.
        /// </summary>
        /// <param name="soapConfig"></param>
        /// <returns> string envelope</returns>
        public string Construir(SoapConfig soapConfig)
        {
            StringBuilder env = new StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");


            env.Append("<soap12:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" ");
            env.Append("xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" ");
            env.Append("xmlns:soap12=\"http://www.w3.org/2003/05/soap-envelope\">");

            // cabecalho
            env.Append("<soap12:Header>");
            env.Append(soapConfig.Cabecalho.GetXmlHeader());
            env.Append("</soap12:Header>");


            // corpo
            env.Append("<soap12:Body>");
            env.Append(soapConfig.Corpo.GetXmlBody());
            env.Append("</soap12:Body>");


            env.Append("</soap12:Envelope>");

            return env.ToString();
        }
    }
}
