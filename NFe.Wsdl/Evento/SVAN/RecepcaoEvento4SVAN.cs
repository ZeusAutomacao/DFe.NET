using System.Security.Cryptography.X509Certificates;
using NFe.Wsdl.Evento.AN;

namespace NFe.Wsdl.Evento.SVAN
{
    public class RecepcaoEvento4SVAN : RecepcaoEvento4AN
    {
        public RecepcaoEvento4SVAN(string url, X509Certificate certificado, int timeOut) : base(url, certificado, timeOut)
        {
        }
    }
}