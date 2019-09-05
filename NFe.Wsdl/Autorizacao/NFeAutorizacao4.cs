using System;
using System.Xml;
using CTe.CTeOSDocumento.Wsdl;
using DFe.Utils;
using DFe.Wsdl;
using NFe.Utils;

namespace NFe.Wsdl.Autorizacao
{
    public class NFeAutorizacao4 : DFeSoapHttpClientProtocol, INfeServicoAutorizacao
    {
        private DFeSoapConfig SoapConfig { get; set; }

        public NFeAutorizacao4(DFeSoapConfig soapConfig)
        {
            SoapConfig = soapConfig;
        }

        [Obsolete("Não tem necessidade mais a partir da nf-e 4.0 o mesmo sera ignorado")]
        public nfeCabecMsg nfeCabecMsg { get; set; }

        public XmlNode Execute(XmlNode nfeDadosMsg)
        {
            SoapConfig.DFeCorpo.Xml = nfeDadosMsg;

            var ret = Invoke(soapConfig: SoapConfig);

            var xmlTag = GetTagConverter(ret, "retEnviNFe");

            var documento = new XmlDocument();
            documento.LoadXml(xmlTag);

            return documento.DocumentElement;
        }

        public XmlNode ExecuteZip(string nfeDadosMsgZip)
        {
            var xml = Compressao.Unzip(Convert.FromBase64String(nfeDadosMsgZip));

            var dadosEnvio = new XmlDocument();
            dadosEnvio.LoadXml(xml);

            SoapConfig.DFeCorpo.Xml = dadosEnvio;
            SoapConfig.DFeCorpo.XmlZip = true;

            var ret = Invoke(soapConfig: SoapConfig);

            var xmlTag = GetTagConverter(ret, "retEnviNFe");

            var documento = new XmlDocument();
            documento.LoadXml(xmlTag);

            return documento.DocumentElement;
        }
    }
}