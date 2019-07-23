using System.Threading.Tasks;
using System.Xml;
using DFe.DocumentosEletronicos.Soap;

namespace DFe.DocumentosEletronicos.Common
{
    public static class RequestBuilderAndSender{
        
        /// <summary>
        /// Executa o carregamento, execução do soap e retorno do resultado
        /// </summary>
        /// <typeparam name="TCommonSoapEnvelope"></typeparam>
        /// <param name="soapEnvelope"></param>
        /// <param name="configuration"></param>
        /// <param name="requestType">Especifico para CTe</param>
        /// <param name="responseElementName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Executa o carregamento, execução do soap e retorno do resultado
        /// </summary>
        /// <typeparam name="TCommonSoapEnvelope"></typeparam>
        /// <param name="soapEnvelope"></param>
        /// <param name="configuration"></param>
        /// <param name="requestType">Especifico para CTe</param>
        /// <param name="responseElementName"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Executa o carregamento, execução do soap e retorno do resultado
        /// </summary>
        /// <typeparam name="TCommonSoapEnvelope"></typeparam>
        /// <param name="soapEnvelope"></param>
        /// <param name="configuration"></param>
        /// <param name="responseElementName"></param>
        /// <param name="actionUrn">Qual é a action (urn) a ser executada, NÃO é a url do server do sefaz! A url esta dentro da configuração(configuration)</param>
        /// <returns></returns>
        public static XmlNode Execute<TCommonSoapEnvelope>(TCommonSoapEnvelope soapEnvelope,
            WsdlConfiguracao configuration, string responseElementName, string actionUrn) where TCommonSoapEnvelope : CommonSoapEnvelope
        {
            {
                var soapUtils = new SoapUtils();
                var xmlResult = new XmlDocument();

                var xmlEnvelop = soapUtils.SerealizeDocument(soapEnvelope);

                var tes = soapUtils.SendRequest(xmlEnvelop, configuration.CertificadoDigital, configuration.Url, configuration.TimeOut, actionUrn: actionUrn);
                xmlResult.LoadXml(tes);

                return xmlResult.GetElementsByTagName(responseElementName)[0];

            }
        }
    }
}