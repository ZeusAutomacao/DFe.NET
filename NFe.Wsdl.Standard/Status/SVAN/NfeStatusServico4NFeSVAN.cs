using NFe.Wsdl.Status.AN;
using System.Security.Cryptography.X509Certificates;

namespace NFe.Wsdl.Status.SVAN
{
    public class NfeStatusServico4NFeSVAN : NfeStatusServico4NFeAN
    {
        public NfeStatusServico4NFeSVAN(string url, X509Certificate certificado, int timeOut) : base(url, certificado, timeOut)
        {

        }
    }
}
