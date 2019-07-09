using System.Security.Cryptography.X509Certificates;
using NFe.Wsdl.ConsultaProtocolo.AN;

namespace NFe.Wsdl.ConsultaProtocolo.SVAN
{
    public class NfeConsultaProtocolo4SVAN: NfeConsultaProtocolo4AN
    {
        public NfeConsultaProtocolo4SVAN(string url, X509Certificate certificado, int timeOut) : base(url, certificado, timeOut)
        {
        }
    }
}
