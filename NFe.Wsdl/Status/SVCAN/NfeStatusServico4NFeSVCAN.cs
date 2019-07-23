using System.Security.Cryptography.X509Certificates;
using NFe.Wsdl.Status.AN;

namespace NFe.Wsdl.Status.SVCAN
{
    public class NfeStatusServico4NFeSVCAN: NfeStatusServico4NFeAN
    {
        public NfeStatusServico4NFeSVCAN(string url, X509Certificate certificado, int timeOut) : base(url, certificado, timeOut)
        {
        }
    }
}
