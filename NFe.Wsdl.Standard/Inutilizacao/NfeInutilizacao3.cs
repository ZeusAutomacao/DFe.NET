using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace NFe.Wsdl.Inutilizacao
{
    public class NfeInutilizacao3 : INfeServico
    {
        public NfeInutilizacao3(string url, X509Certificate certificado, int timeOut)
        {
            throw new NotImplementedException("NfeInutilizacao 3.1 não está implementado para .net standard, apenas .net framework");
        }

        public nfeCabecMsg nfeCabecMsg { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public XmlNode Execute(XmlNode nfeDadosMsg)
        {
            throw new NotImplementedException();
        }
    }
}
