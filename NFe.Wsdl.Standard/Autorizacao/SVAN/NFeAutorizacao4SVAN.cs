using NFe.Wsdl.Autorizacao.AN;
using System.Security.Cryptography.X509Certificates;

namespace NFe.Wsdl.Autorizacao.SVAN
{
    public class NFeAutorizacao4SVAN : NFeAutorizacao4AN
    {
        public NFeAutorizacao4SVAN(string url, X509Certificate certificado, int timeOut) : base(url, certificado, timeOut)
        {

        }
    }
}
