using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Channels;
using System.Xml;

namespace NFe.Wsdl.ConsultaCadastro.DEMAIS_UFs
{

    public class CadConsultaCadastro4 : CadConsultaCadastro4Soap12Client, INfeServico
    {

        public CadConsultaCadastro4(string url, X509Certificate certificado, int timeOut) : base(url)
        {
            base.ClientCredentials.ClientCertificate.Certificate = (X509Certificate2)certificado;
        }

        public nfeCabecMsg nfeCabecMsg { get; set; }

        public XmlNode Execute(XmlNode nfeDadosMsg)
        {
            var result = base.consultaCadastroAsync(nfeDadosMsg).Result;
            return result.nfeResultMsg;
        }
    }


    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class consultaCadastro4Request
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/CadConsultaCadastro4", Order = 0)]
        public System.Xml.XmlNode nfeDadosMsg;

        public consultaCadastro4Request()
        {
        }

        public consultaCadastro4Request(System.Xml.XmlNode nfeDadosMsg)
        {
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }

    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class consultaCadastro4Response
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/CadConsultaCadastro4", Order = 0)]
        public System.Xml.XmlNode nfeResultMsg;

        public consultaCadastro4Response()
        {
        }

        public consultaCadastro4Response(System.Xml.XmlNode nfeResultMsg)
        {
            this.nfeResultMsg = nfeResultMsg;
        }
    }

    [System.ServiceModel.ServiceContractAttribute(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/CadConsultaCadastro4", ConfigurationName = "CadConsultaCadastro4Soap12", SessionMode =System.ServiceModel.SessionMode.Allowed)]
    public interface CadConsultaCadastro4Soap12 : IChannel
    {
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/CadConsultaCadastro4/consultaCadastro", ReplyAction = "*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        System.Threading.Tasks.Task<consultaCadastro4Response> consultaCadastroAsync(consultaCadastro4Request request);
    }

    public partial class CadConsultaCadastro4Soap12Client : SoapBindingClient<CadConsultaCadastro4Soap12>
    {

        public CadConsultaCadastro4Soap12Client(string endpointAddressUri) :
                base(endpointAddressUri)
        {
        }

        public System.Threading.Tasks.Task<consultaCadastro4Response> consultaCadastroAsync(System.Xml.XmlNode nfeDadosMsg)
        {
            consultaCadastro4Request inValue = new consultaCadastro4Request();
            inValue.nfeDadosMsg = nfeDadosMsg;
            return this.Channel.consultaCadastroAsync(inValue);
        }

    }
}