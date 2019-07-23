using DFe.DocumentosEletronicos.Common;
using DFe.DocumentosEletronicos.Wsdl;
using System;
using System.Xml;
using System.Xml.Serialization;

namespace NFe.Wsdl.Autorizacao
{
    public class NFeAutorizacao4 : INfeServicoAutorizacao
    {
        //Envelope SOAP para envio
        private SoapEnvelope soapEnvelope;

        //Configurações do WSDL para estabelecimento da comunicação
        private DFeSoapConfig SoapConfig { get; set; }

        public NFeAutorizacao4(DFeSoapConfig soapConfig)
        {
            throw new NotImplementedException();
            SoapConfig = soapConfig;
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

        /// <summary>
        /// Classe base para a serealização no formato do envelope SOAP.
        /// </summary>
        [XmlRoot(ElementName = "Envelope", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
        public class SoapEnvelope : CommonSoapEnvelope
        {
            [XmlElement(ElementName = "Body", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
            public ResponseBody<XmlNode> body { get; set; }
        }

        /// <summary>
        /// Classe para o corpo do Envelope SOAP
        /// </summary>
        public class ResponseBody<T> : CommonResponseBody
        {
            [XmlElement(Namespace = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4")]
            public T nfeDadosMsg { get; set; }
        }
    }
}
