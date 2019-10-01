using NFe.Wsdl.Evento.AN;
using System.Security.Cryptography.X509Certificates;

namespace NFe.Wsdl.Evento.SVAN
{
    public class RecepcaoEvento4SVAN : RecepcaoEvento4ANSVBase
    {
        public RecepcaoEvento4SVAN(string url, X509Certificate certificado, int timeOut) : base(url, certificado, timeOut)
        {
        }
    }
}
