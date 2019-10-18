using NFe.Wsdl.Status.AN;
using System.Security.Cryptography.X509Certificates;

namespace NFe.Wsdl.Status.SVCAN
{
    public class NfeStatusServico4NFeSVCAN : NfeStatusServico4NFeAN
    {
        public NfeStatusServico4NFeSVCAN(string url, X509Certificate certificado, int timeOut) : base(url, certificado, timeOut)
        {
        }
    }
}
