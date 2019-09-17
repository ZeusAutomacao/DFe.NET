using NFe.Wsdl.Autorizacao.AN;
using System.Security.Cryptography.X509Certificates;

namespace NFe.Wsdl.Autorizacao.SVCAN
{
    public class NfeRetAutorizacao4SVCAN : NfeRetAutorizacao4AN
    {
        public NfeRetAutorizacao4SVCAN(string url, X509Certificate certificado, int timeOut) : base(url, certificado, timeOut)
        {
        }
    }
}
