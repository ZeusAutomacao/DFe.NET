using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml;
using CTe.CTeOSDocumento.Common;

namespace DFe.Wsdl.Common
{
    public interface IRequestSefaz
    {
        XmlDocument SerealizeDocument<T>(T soapEnvelope);

        Task<string> SendRequestAsync(XmlDocument xmlEnvelop, X509Certificate2 certificadoDigital, string url,
            int timeOut, TipoEvento? tipoEvento = null, string actionUrn = "");

        string SendRequest(XmlDocument xmlEnvelop, X509Certificate2 certificadoDigital, string url, int timeOut,
            TipoEvento? tipoEvento = null, string actionUrn = "");
    }
}