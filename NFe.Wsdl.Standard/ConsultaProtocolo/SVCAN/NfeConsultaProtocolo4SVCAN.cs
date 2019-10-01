using NFe.Wsdl.ConsultaProtocolo.AN;
using System.Security.Cryptography.X509Certificates;

namespace NFe.Wsdl.ConsultaProtocolo.SVCAN
{
    public class NfeConsultaProtocolo4SVCAN : NfeConsultaProtocolo4AN
    {
        public NfeConsultaProtocolo4SVCAN(string url, X509Certificate certificado, int timeOut) : base(url, certificado, timeOut)
        {
        }
    }
}
