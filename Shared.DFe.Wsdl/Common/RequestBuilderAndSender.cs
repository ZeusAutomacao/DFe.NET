using System.Threading.Tasks;
using System.Xml;
using DFe.DocumentosEletronicos.Soap;

namespace DFe.DocumentosEletronicos.Common
{
    public static class RequestBuilderAndSender{
        
        public static async Task<XmlNode> ExecuteAsync<TCommonSoapEnvelope>(TCommonSoapEnvelope soapEnvelope, 
            WsdlConfiguracao configuration, TipoEvento requestType, string responseElementName) where TCommonSoapEnvelope: CommonSoapEnvelope
        {
            {
                var soapUtils = new SoapUtils();
                var xmlResult = new XmlDocument();                
                
                var xmlEnvelop = soapUtils.SerealizeDocument(soapEnvelope);

                var tes = await soapUtils.SendRequestAsync(xmlEnvelop, configuration.CertificadoDigital, configuration.Url,  configuration.TimeOut, requestType);
                xmlResult.LoadXml(tes);

                return xmlResult.GetElementsByTagName(responseElementName)[0];

            }
        }

        public static XmlNode Execute<TCommonSoapEnvelope>(TCommonSoapEnvelope soapEnvelope,
            WsdlConfiguracao configuration, TipoEvento requestType, string responseElementName) where TCommonSoapEnvelope : CommonSoapEnvelope
        {
            {
                var soapUtils = new SoapUtils();
                var xmlResult = new XmlDocument();

                var xmlEnvelop = soapUtils.SerealizeDocument(soapEnvelope);

                var tes = soapUtils.SendRequest(xmlEnvelop, configuration.CertificadoDigital, configuration.Url, configuration.TimeOut, requestType);
                xmlResult.LoadXml(tes);

                return xmlResult.GetElementsByTagName(responseElementName)[0];

            }
        }
    }

}