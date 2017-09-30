using System.Xml;
using DFe.DocumentosEletronicos.MDFe.Classes.Retorno.ConsultaNaoEncerrados;
using DFe.DocumentosEletronicos.MDFe.Wsdl.Configuracao;
using DFe.DocumentosEletronicos.Wsdl;
using DFe.DocumentosEletronicos.Wsdl.Cabecalho;
using DFe.DocumentosEletronicos.Wsdl.Corpo;
using DFe.Wsdl;

namespace DFe.DocumentosEletronicos.MDFe.Wsdl.Gerado.MDFeConsultaNaoEncerrados
{
    public class MDFeConsNaoEnc : DFeSoapHttpClientProtocol
    {
        private DFeSoapConfig SoapConfig { get; set; }

        public MDFeConsNaoEnc(WsdlConfiguracao configuracaoWsdl)
        {
            SoapConfig = new DFeSoapConfig
            {
                DFeCorpo = new DFeCorpo("http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeConsNaoEnc", new MdfeTagCorpo()),
                DFeCabecalho = new DFeCabecalho(configuracaoWsdl.EstadoUF, configuracaoWsdl.VersaoLayout, new MdfeTagCabecalho(), "http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeConsNaoEnc"),
                Metodo = "http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeConsNaoEnc/mdfeConsNaoEnc",
                Url = configuracaoWsdl.Url,
                Certificado = configuracaoWsdl.CertificadoDigital,
                TimeOut = configuracaoWsdl.TimeOut
            };
        }

        public retConsMDFeNaoEnc Autorizar(XmlNode xml)
        {
            SoapConfig.DFeCorpo.Xml = xml;

            var ret = Invoke(SoapConfig);

            var xmlTag = GetTagConverter(ret, nameof(retConsMDFeNaoEnc));

            var retEnvi = retConsMDFeNaoEnc.LoadXml(xmlTag);

            return retEnvi;
        }
    }
}