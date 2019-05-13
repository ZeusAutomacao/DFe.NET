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

using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;
using DFe.DocumentosEletronicos.Common;

namespace CTe.Wsdl.DistribuicaoDFe
{

    [WebServiceBinding(Name = "CTeDistribuicaoDFeSoap", Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CTeDistribuicaoDFe")]
    public class CTeDistDFeInteresse : SoapHttpClientProtocol
    {
        public CTeDistDFeInteresse(WsdlConfiguracao configuracao)
        {

            SoapVersion = SoapProtocolVersion.Soap12;
            Url = configuracao.Url;
            Timeout = configuracao.TimeOut;
            ClientCertificates.Add(configuracao.CertificadoDigital);

            cteCabecMsg = new cteCabecMsg();
            cteCabecMsg.versaoDados = configuracao.Versao;
            cteCabecMsg.cUF = configuracao.CodigoIbgeEstado;
        }

        [XmlAttribute(Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CTeDistribuicaoDFe")]
        public cteCabecMsg cteCabecMsg { get; set; }

        [SoapHeader("cteCabecMsg")]
        [SoapDocumentMethod("http://www.portalfiscal.inf.br/cte/wsdl/CTeDistribuicaoDFe/cteDistDFeInteresse", RequestNamespace = "http://www.portalfiscal.inf.br/cte/wsdl/CTeDistribuicaoDFe", ResponseNamespace = "http://www.portalfiscal.inf.br/cte/wsdl/CTeDistribuicaoDFe", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
        [WebMethod(MessageName = "cteDistDFeInteresse")]
        [return: XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CTeDistribuicaoDFe")]
        public XmlNode Execute([XmlElement(Namespace = "http://www.portalfiscal.inf.br/cte/wsdl/CTeDistribuicaoDFe")] XmlNode cteDadosMsg)
        {
            var results = Invoke("cteDistDFeInteresse", new object[] { cteDadosMsg });
            return ((XmlNode)(results[0]));
        }

    }
}
