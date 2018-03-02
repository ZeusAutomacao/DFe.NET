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

namespace NFe.Wsdl.ConsultaProtocolo
{
    public class NfeConsulta : NfeConsultaSoap12Client, INfeServico
    {
        public NfeConsulta(string url, X509Certificate certificado, int timeOut) : base(url)
        {
            base.ClientCredentials.ClientCertificate.Certificate = (X509Certificate2)certificado;
        }

        public nfeCabecMsg nfeCabecMsg { get; set; }

        public XmlNode Execute(XmlNode nfeDadosMsg)
        {
            var result = base.nfeConsultaNFAsync(this.nfeCabecMsg, nfeDadosMsg).Result;
            return result.nfeConsultaNFResult;
        }
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class nfeConsultaNFRequest
    {

        [System.ServiceModel.MessageHeaderAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeConsulta")]
        public nfeCabecMsg nfeCabecMsg;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeConsulta", Order = 0)]
        public System.Xml.XmlNode nfeDadosMsg;

        public nfeConsultaNFRequest()
        {
        }

        public nfeConsultaNFRequest(nfeCabecMsg nfeCabecMsg, System.Xml.XmlNode nfeDadosMsg)
        {
            this.nfeCabecMsg = nfeCabecMsg;
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class nfeConsultaNFResponse
    {

        [System.ServiceModel.MessageHeaderAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeConsulta")]
        public nfeCabecMsg nfeCabecMsg;

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeConsulta", Order = 0)]
        public System.Xml.XmlNode nfeConsultaNFResult;

        public nfeConsultaNFResponse()
        {
        }

        public nfeConsultaNFResponse(nfeCabecMsg nfeCabecMsg, System.Xml.XmlNode nfeConsultaNFResult)
        {
            this.nfeCabecMsg = nfeCabecMsg;
            this.nfeConsultaNFResult = nfeConsultaNFResult;
        }
    }
    
    [System.ServiceModel.ServiceContractAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeConsulta", ConfigurationName = "NfeConsultaSoap12")]
    public interface NfeConsultaSoap12 : IChannel
    {
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NfeConsulta/nfeConsultaNF", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        System.Threading.Tasks.Task<nfeConsultaNFResponse> nfeConsultaNFAsync(nfeConsultaNFRequest request);
    }

    public partial class NfeConsultaSoap12Client : SoapBindingClient<NfeConsultaSoap12>
    {
        public NfeConsultaSoap12Client(string endpointAddressUri) : base(endpointAddressUri)
        {
        }

        public System.Threading.Tasks.Task<nfeConsultaNFResponse> nfeConsultaNFAsync(nfeCabecMsg nfeCabecMsg, System.Xml.XmlNode nfeDadosMsg)
        {
            nfeConsultaNFRequest inValue = new nfeConsultaNFRequest();
            inValue.nfeCabecMsg = nfeCabecMsg;
            inValue.nfeDadosMsg = nfeDadosMsg;
            return ((NfeConsultaSoap12)(this.Channel)).nfeConsultaNFAsync(inValue);
        }
    }
}