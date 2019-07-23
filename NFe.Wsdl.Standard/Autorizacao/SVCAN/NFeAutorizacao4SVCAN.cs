using NFe.Wsdl.Autorizacao.AN;
using System.Security.Cryptography.X509Certificates;

namespace NFe.Wsdl.Autorizacao.SVCAN
{
    public class NFeAutorizacao4SVCAN : NFeAutorizacao4AN
    {
        public NFeAutorizacao4SVCAN(string url, X509Certificate certificado, int timeOut) : base(url, certificado, timeOut)
        {

        }
    }
}
