using System.Security.Cryptography.X509Certificates;

namespace NFe.Wsdl.Evento.AN
{
    public class RecepcaoEventoManifestacaoDestinatario4AN : RecepcaoEvento4AN
    {
        public RecepcaoEventoManifestacaoDestinatario4AN(string url, X509Certificate certificado, int timeOut) : base(url, certificado, timeOut)
        {
        }
    }
}
