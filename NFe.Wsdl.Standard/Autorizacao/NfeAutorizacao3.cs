using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace NFe.Wsdl.Autorizacao
{
    public class NfeAutorizacao3 : INfeServicoAutorizacao
    {
        public NfeAutorizacao3(string url, X509Certificate certificado, int timeOut)
        {
            throw new NotImplementedException("Nfe Autorização de Servico 3.x não está implementado para .net standard, apenas .net framework");
        }
        public nfeCabecMsg nfeCabecMsg { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public XmlNode Execute(XmlNode nfeDadosMsg)
        {
            throw new NotImplementedException();
        }

        public XmlNode ExecuteZip(string nfeDadosMsgZip)
        {
            throw new NotImplementedException();
        }
    }
}
