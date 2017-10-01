using System.Xml;
using DFe.DocumentosEletronicos.MDFe.Classes.Retorno.ConsultaProtocolo;
using DFe.DocumentosEletronicos.NFe.Wsdl.Configuracao;
using DFe.DocumentosEletronicos.Wsdl;
using DFe.DocumentosEletronicos.Wsdl.Cabecalho;
using DFe.DocumentosEletronicos.Wsdl.Corpo;
using DFe.Wsdl;

namespace DFe.DocumentosEletronicos.NFe.Wsdl.AdmCsc
{
    public class NfceCscc : DFeSoapHttpClientProtocol
    {
        private DFeSoapConfig SoapConfig { get; set; }

        public NfceCscc(WsdlConfiguracao configuracaoWsdl)
        {
            SoapConfig = new DFeSoapConfig
            {
                DFeCorpo = new DFeCorpo("http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeConsulta", new NfeTagCorpo()),
                DFeCabecalho = new DFeCabecalho(configuracaoWsdl.EstadoUF, configuracaoWsdl.VersaoLayout, new NfeTagCabecalho(), "http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeConsulta"),
                Metodo = "http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeConsulta/mdfeConsultaMDF",
                Url = configuracaoWsdl.Url,
                Certificado = configuracaoWsdl.CertificadoDigital,
                TimeOut = configuracaoWsdl.TimeOut
            };
        }

        public retConsSitMDFe Autorizar(XmlNode xml)
        {
            SoapConfig.DFeCorpo.Xml = xml;

            var ret = Invoke(SoapConfig);

            var xmlTag = GetTagConverter(ret, nameof(retConsSitMDFe));

            var retEnvi = retConsSitMDFe.LoadXml(xmlTag);

            return retEnvi;
        }
    }
}