using System.Security.Cryptography.X509Certificates;
using NFe.Wsdl.Autorizacao.AN;

namespace NFe.Wsdl.Autorizacao.SVCAN
{
    public class NFeAutorizacao4SVCAN: NFeAutorizacao4AN
    {
        public NFeAutorizacao4SVCAN(string url, X509Certificate certificado, int timeOut) : base(url, certificado, timeOut)
        {
        }
    }
}
