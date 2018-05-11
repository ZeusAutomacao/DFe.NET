using System;
using System.Security.Cryptography.X509Certificates;
//using System.Web.Services;
//using System.Web.Services.Description;
//using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace NFe.Wsdl.Inutilizacao
{
    //[WebServiceBinding(Name = "NFeInutilizacao4Service", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4")]
    public class NFeInutilizacao4 : /*SoapHttpClientProtocol,*/ INfeServico
    {

        public NFeInutilizacao4(string url, X509Certificate certificado, int timeOut)
        {
            //SoapVersion = SoapProtocolVersion.Soap12;
            //Url = url;
            //Timeout = timeOut;
            //ClientCertificates.Add(certificado);
        }

        [Obsolete("Não utilizar na nfe 4.0")]
        public nfeCabecMsg nfeCabecMsg { get; set; }

        //[SoapDocumentMethod("http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4/nfeInutilizacaoNF", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Bare)]
        //[return: XmlElement("nfeResultMsg", Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4")]
        //[WebMethod(MessageName = "nfeInutilizacaoNF")]
        public XmlNode Execute([XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4")] XmlNode nfeDadosMsg)
        {
            //var results = Invoke("nfeInutilizacaoNF", new object[] { nfeDadosMsg });
            //return (XmlNode)(results[0]);
            return null;
        }
    }
}


//namespace NFe.Wsdl.Inutilizacao
//{

//    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
//    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
//    public partial class nfeInutilizacao4NFRequest
//    {

//        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4", Order = 0)]
//        public System.Xml.XmlNode nfeDadosMsg;

//        public nfeInutilizacao4NFRequest()
//        {
//        }

//        public nfeInutilizacao4NFRequest(System.Xml.XmlNode nfeDadosMsg)
//        {
//            this.nfeDadosMsg = nfeDadosMsg;
//        }
//    }

//    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
//    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
//    public partial class nfeInutilizacao4NFResponse
//    {

//        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4", Order = 0)]
//        public System.Xml.XmlNode nfeResultMsg;

//        public nfeInutilizacao4NFResponse()
//        {
//        }

//        public nfeInutilizacao4NFResponse(System.Xml.XmlNode nfeResultMsg)
//        {
//            this.nfeResultMsg = nfeResultMsg;
//        }
//    }

//    [System.ServiceModel.ServiceContractAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4", ConfigurationName = "NFeInutilizacao4Soap12")]
//    public interface NFeInutilizacao4Soap12
//    {

//        // CODEGEN: Generating message contract since the operation nfeInutilizacaoNF is neither RPC nor document wrapped.
//        [System.ServiceModel.OperationContractAttribute(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4/nfeInutilizacaoNF", ReplyAction = "*")]
//        [System.ServiceModel.XmlSerializerFormatAttribute()]
//        nfeInutilizacaoNFResponse nfeInutilizacaoNF(nfeInutilizacaoNFRequest request);

//        [System.ServiceModel.OperationContractAttribute(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeInutilizacao4/nfeInutilizacaoNF", ReplyAction = "*")]
//        System.Threading.Tasks.Task<nfeInutilizacaoNFResponse> nfeInutilizacaoNFAsync(nfeInutilizacaoNFRequest request);
//    }

//}