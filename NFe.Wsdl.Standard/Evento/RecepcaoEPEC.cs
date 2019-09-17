using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace NFe.Wsdl.Evento
{
    public class RecepcaoEPEC : INfeServico
    {
        public RecepcaoEPEC(string url, X509Certificate certificado, int timeOut)
        {
            throw new NotImplementedException("RecepcaoEPEC não está implementado para .net standard, apenas .net framework");
        }

        public nfeCabecMsg nfeCabecMsg { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public XmlNode Execute(XmlNode nfeDadosMsg)
        {
            throw new NotImplementedException();
        }
    }
}
