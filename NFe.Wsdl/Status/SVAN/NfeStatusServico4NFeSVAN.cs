using System.Security.Cryptography.X509Certificates;
using NFe.Wsdl.Status.AN;

namespace NFe.Wsdl.Status.SVAN
{
    public class NfeStatusServico4NFeSVAN : NfeStatusServico4NFeAN
    {
        public NfeStatusServico4NFeSVAN(string url, X509Certificate certificado, int timeOut) : base(url, certificado, timeOut)
        {
        }
    }
}