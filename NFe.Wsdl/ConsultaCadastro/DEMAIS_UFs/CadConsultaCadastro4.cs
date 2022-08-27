using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
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
            return ExecuteAsync(nfeDadosMsg).GetAwaiter().GetResult();
        }

        public async Task<XmlNode> ExecuteAsync(XmlNode nfeDadosMsg)
        {
            var result = await consultaCadastroAsync(nfeDadosMsg);
            return result.nfeResultMsg;
        }
    }


    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public partial class consultaCadastro4Request
    {

        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/CadConsultaCadastro4", Order = 0)]
        public XmlNode nfeDadosMsg;

        public consultaCadastro4Request()
        {
        }

        public consultaCadastro4Request(XmlNode nfeDadosMsg)
        {
            this.nfeDadosMsg = nfeDadosMsg;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [MessageContract(IsWrapped = false)]
    public partial class consultaCadastro4Response
    {

        [MessageBodyMember(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/CadConsultaCadastro4", Order = 0)]
        public XmlNode nfeResultMsg;

        public consultaCadastro4Response()
        {
        }

        public consultaCadastro4Response(XmlNode nfeResultMsg)
        {
            this.nfeResultMsg = nfeResultMsg;
        }
    }

    [ServiceContract(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/CadConsultaCadastro4", ConfigurationName = "CadConsultaCadastro4Soap12", SessionMode =SessionMode.Allowed)]
    public interface CadConsultaCadastro4Soap12 : IChannel
    {
        [OperationContract(Action = "http://www.portalfiscal.inf.br/nfe/wsdl/CadConsultaCadastro4/consultaCadastro", ReplyAction = "*")]
        [XmlSerializerFormat()]
        Task<consultaCadastro4Response> consultaCadastroAsync(consultaCadastro4Request request);
    }

    public partial class CadConsultaCadastro4Soap12Client : SoapBindingClient<CadConsultaCadastro4Soap12>
    {

        public CadConsultaCadastro4Soap12Client(string endpointAddressUri) :
                base(endpointAddressUri)
        {
        }

        public Task<consultaCadastro4Response> consultaCadastroAsync(XmlNode nfeDadosMsg)
        {
            consultaCadastro4Request inValue = new consultaCadastro4Request();
            inValue.nfeDadosMsg = nfeDadosMsg;
            return this.Channel.consultaCadastroAsync(inValue);
        }

    }
}