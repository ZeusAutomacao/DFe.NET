using NFe.Wsdl.Evento.AN;
using System.Security.Cryptography.X509Certificates;

namespace NFe.Wsdl.Evento.SVCAN
{
    public class RecepcaoEvento4SVCAN : RecepcaoEvento4ANSVBase
    {
        public RecepcaoEvento4SVCAN(string url, X509Certificate certificado, int timeOut) : base(url, certificado, timeOut)
        {
        }
    }
}
