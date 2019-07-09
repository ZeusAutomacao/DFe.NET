using System.Security.Cryptography.X509Certificates;
using NFe.Wsdl.Autorizacao.AN;

namespace NFe.Wsdl.Autorizacao.SVCAN
{
    public class NfeRetAutorizacao4SVCAN: NfeRetAutorizacao4AN
    {
        public NfeRetAutorizacao4SVCAN(string url, X509Certificate certificado, int timeOut) : base(url, certificado, timeOut)
        {
        }
    }
}
