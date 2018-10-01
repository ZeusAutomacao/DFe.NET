using System.Security.Cryptography.X509Certificates;
using NFe.Wsdl.Autorizacao.AN;

namespace NFe.Wsdl.Autorizacao.SVAN
{
    public class NFeAutorizacao4SVAN : NFeAutorizacao4AN
    {
        public NFeAutorizacao4SVAN(string url, X509Certificate certificado, int timeOut) : base(url, certificado, timeOut)
        {
        }
    }
}