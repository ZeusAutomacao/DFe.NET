using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace NFe.Wsdl.Recepcao
{
    public class NfeRecepcao2 : INfeServico
    {
        public NfeRecepcao2(string url, X509Certificate certificado, int timeOut)
        {
            throw new NotImplementedException("NfeRecepcao 2.0 não está implementado para .net standard, apenas .net framework");
        }

        public nfeCabecMsg nfeCabecMsg { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public XmlNode Execute(XmlNode nfeDadosMsg)
        {
            throw new NotImplementedException();
        }
    }
}
