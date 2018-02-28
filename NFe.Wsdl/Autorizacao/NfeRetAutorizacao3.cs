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
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace NFe.Wsdl.Autorizacao
{
    public class NfeRetAutorizacao3 : NfeRetAutorizacao3Soap12Client, INfeServico
    {
        public NfeRetAutorizacao3(string url, X509Certificate certificado, int timeOut)
            : base(url)
        {
            base.ClientCredentials.ClientCertificate.Certificate = (X509Certificate2)certificado;
        }

        public nfeCabecMsg nfeCabecMsg { get; set; }

        public XmlNode Execute(XmlNode nfeDadosMsg)
        {
            var result = base.nfeRetAutorizacaoLoteAsync(this.nfeCabecMsg, nfeDadosMsg).Result;
            return result.nfeRetAutorizacaoLoteResult;
        }
    }

    [System.ServiceModel.ServiceContractAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeRetAutorizacao3", ConfigurationName = "NfeRetAutorizacaoSoap12")]
    public interface NfeRetAutorizacao3Soap12
    {
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeRetAutorizacao3/nfeRetAutorizacaoLote", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        System.Threading.Tasks.Task<nfeRetAutorizacao3LoteResponse> nfeRetAutorizacaoLoteAsync(nfeRetAutorizacao3LoteRequest request);
    }

    public interface NfeRetAutorizacao3Soap12Channel : NfeRetAutorizacao3Soap12, System.ServiceModel.IClientChannel
    {
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class nfeRetAutorizacao3LoteRequest
    {

        [System.ServiceModel.MessageHeaderAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeRetAutorizacao3")]
        public nfeCabecMsg nfeCabecMsg;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeRetAutorizacao3", Order = 0)]
        public System.Xml.XmlNode nfeDadosMsg;

        public nfeRetAutorizacao3LoteRequest()
        {
        }

        public nfeRetAutorizacao3LoteRequest(nfeCabecMsg nfeCabecMsg, System.Xml.XmlNode nfeDadosMsg)
        {
            this.nfeCabecMsg = nfeCabecMsg;
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }


    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class nfeRetAutorizacao3LoteResponse
    {

        [System.ServiceModel.MessageHeaderAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeRetAutorizacao3")]
        public nfeCabecMsg nfeCabecMsg;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeRetAutorizacao3", Order = 0)]
        public System.Xml.XmlNode nfeRetAutorizacaoLoteResult;

        public nfeRetAutorizacao3LoteResponse()
        {
        }

        public nfeRetAutorizacao3LoteResponse(nfeCabecMsg nfeCabecMsg, System.Xml.XmlNode nfeRetAutorizacaoLoteResult)
        {
            this.nfeCabecMsg = nfeCabecMsg;
            this.nfeRetAutorizacaoLoteResult = nfeRetAutorizacaoLoteResult;
        }
    }

    public partial class NfeRetAutorizacao3Soap12Client : System.ServiceModel.ClientBase<NfeRetAutorizacao3Soap12>
    {

        public NfeRetAutorizacao3Soap12Client()
        {
        }

        public NfeRetAutorizacao3Soap12Client(string endpointAddressUri) :
                base(
                    new CustomBinding(new TextMessageEncodingBindingElement(MessageVersion.CreateVersion(EnvelopeVersion.Soap12, AddressingVersion.None), Encoding.UTF8),
                        new HttpsTransportBindingElement { RequireClientCertificate = true }),
                    new EndpointAddress(endpointAddressUri)
                    )
        {
        }

        public NfeRetAutorizacao3Soap12Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
                base(binding, remoteAddress)
        {
        }

        public System.Threading.Tasks.Task<nfeRetAutorizacao3LoteResponse> nfeRetAutorizacaoLoteAsync(nfeCabecMsg nfeCabecMsg, System.Xml.XmlNode nfeDadosMsg)
        {
            nfeRetAutorizacao3LoteRequest inValue = new nfeRetAutorizacao3LoteRequest
            {
                nfeCabecMsg = nfeCabecMsg,
                nfeDadosMsg = nfeDadosMsg
            };
            return this.Channel.nfeRetAutorizacaoLoteAsync(inValue);
        }
    }
}