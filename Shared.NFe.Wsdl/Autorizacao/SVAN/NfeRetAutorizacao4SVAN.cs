using System.Security.Cryptography.X509Certificates;
using NFe.Wsdl.Autorizacao.AN;

namespace NFe.Wsdl.Autorizacao.SVAN
{
    public class NfeRetAutorizacao4SVAN : NfeRetAutorizacao4AN
    {
        public NfeRetAutorizacao4SVAN(string url, X509Certificate certificado, int timeOut) : base(url, certificado, timeOut)
        {
        }
    }
}