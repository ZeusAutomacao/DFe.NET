using CTe.CTeOSDocumento.Wsdl;
using CTe.CTeOSDocumento.Wsdl.Cabecalho;
using CTe.CTeOSDocumento.Wsdl.Corpo;
using DFe.Classes.Entidades;
using DFe.Utils;
using DFe.Wsdl;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace NFe.Wsdl.Autorizacao
{
    public class NFeAutorizacao4 : DFeSoapHttpClientProtocol, INfeServicoAutorizacao
    {
        private DFeSoapConfig SoapConfig { get; set; }

        public NFeAutorizacao4(string url, X509Certificate certificado, int timeOut, bool compactarMensagem, DFe.Classes.Flags.VersaoServico versaoNfeAutorizacao, Estado estado)
        {
            SoapConfig = new DFeSoapConfig
            {
                DFeCorpo = new DFeCorpo("http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4", new NfeTagCorpo(estado.GetParametroDeEntradaWsdl(compactarMensagem))),
                DFeCabecalho = new DFeCabecalho(estado, versaoNfeAutorizacao, new TagCabecalhoVazia(), "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4"),
                Metodo = compactarMensagem ? "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4/nfeAutorizacaoLoteZIP"
                        : "http://www.portalfiscal.inf.br/nfe/wsdl/NFeAutorizacao4/nfeAutorizacaoLote",
                Url = url,
                Certificado = new X509Certificate2(certificado),
                TimeOut = timeOut
            };
        }

        [Obsolete("Não tem necessidade mais a partir da nf-e 4.0 o mesmo sera ignorado")]
        public nfeCabecMsg nfeCabecMsg { get; set; }

        public XmlNode Execute(XmlNode nfeDadosMsg)
        {
            SoapConfig.DFeCorpo.Xml = nfeDadosMsg;

            string ret = Invoke(soapConfig: SoapConfig);

            string xmlTag = GetTagConverter(ret, "retEnviNFe");

            XmlDocument documento = new XmlDocument();
            documento.LoadXml(xmlTag);

            return documento.DocumentElement;
        }

        public XmlNode ExecuteZip(string nfeDadosMsgZip)
        {
            string xml = Compressao.Unzip(Convert.FromBase64String(nfeDadosMsgZip));

            XmlDocument dadosEnvio = new XmlDocument();
            dadosEnvio.LoadXml(xml);

            SoapConfig.DFeCorpo.Xml = dadosEnvio;
            SoapConfig.DFeCorpo.XmlZip = true;

            string ret = Invoke(soapConfig: SoapConfig);

            string xmlTag = GetTagConverter(ret, "retEnviNFe");

            XmlDocument documento = new XmlDocument();
            documento.LoadXml(xmlTag);

            return documento.DocumentElement;
        }
    }
}