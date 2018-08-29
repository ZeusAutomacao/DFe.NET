using System.Security.Cryptography.X509Certificates;
using NFe.Wsdl.Evento.AN;

namespace NFe.Wsdl.Evento.SVCAN
{
    public class RecepcaoEvento4SVCAN: RecepcaoEvento4AN
    {
        public RecepcaoEvento4SVCAN(string url, X509Certificate certificado, int timeOut) : base(url, certificado, timeOut)
        {
        }
    }
}
