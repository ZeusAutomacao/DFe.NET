using System.Xml;
using DFe.DocumentosEletronicos.MDFe.Classes.Retorno.Evento;
using DFe.DocumentosEletronicos.MDFe.Wsdl.Configuracao;
using DFe.DocumentosEletronicos.Wsdl;
using DFe.DocumentosEletronicos.Wsdl.Cabecalho;
using DFe.DocumentosEletronicos.Wsdl.Corpo;
using DFe.Wsdl;

namespace DFe.DocumentosEletronicos.MDFe.Wsdl.Comunicacao.MDFeEventos
{
    public class MDFeRecepcaoEvento : DFeSoapHttpClientProtocol
    {
        private DFeSoapConfig SoapConfig { get; set; }

        public MDFeRecepcaoEvento(WsdlConfiguracao configuracaoWsdl)
        {
            SoapConfig = new DFeSoapConfig
            {
                DFeCorpo = new DFeCorpo("http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeRecepcaoEvento", new MdfeTagCorpo()),
                DFeCabecalho = new DFeCabecalho(configuracaoWsdl.EstadoUF, configuracaoWsdl.VersaoLayout, new MdfeTagCabecalho(), "http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeRecepcaoEvento"),
                Metodo = "http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeRecepcaoEvento/mdfeRecepcaoEvento",
                Url = configuracaoWsdl.Url,
                Certificado = configuracaoWsdl.CertificadoDigital,
                TimeOut = configuracaoWsdl.TimeOut
            };
        }

        public retEventoMDFe Autorizar(XmlNode xml)
        {
            SoapConfig.DFeCorpo.Xml = xml;

            var ret = Invoke(SoapConfig);

            var xmlTag = GetTagConverter(ret, nameof(retEventoMDFe));

            var retEnvi = retEventoMDFe.LoadXml(xmlTag);

            return retEnvi;
        }
    }
}