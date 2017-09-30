using System.Xml;
using DFe.DocumentosEletronicos.MDFe.Classes.Retorno.StatusServico;
using DFe.DocumentosEletronicos.MDFe.Wsdl.Configuracao;
using DFe.DocumentosEletronicos.Wsdl;
using DFe.DocumentosEletronicos.Wsdl.Cabecalho;
using DFe.DocumentosEletronicos.Wsdl.Corpo;
using DFe.Wsdl;

namespace DFe.DocumentosEletronicos.MDFe.Wsdl.Gerado.MDFeStatusServico
{
    public class MDFeStatusServico : DFeSoapHttpClientProtocol
    {
        private DFeSoapConfig SoapConfig { get; set; }

        public MDFeStatusServico(WsdlConfiguracao configuracaoWsdl)
        {
            SoapConfig = new DFeSoapConfig
            {
                DFeCorpo = new DFeCorpo("http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeStatusServico", new MdfeTagCorpo()),
                DFeCabecalho = new DFeCabecalho(configuracaoWsdl.EstadoUF, configuracaoWsdl.VersaoLayout, new MdfeTagCabecalho(), "http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeStatusServico"),
                Metodo = "http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeStatusServico/mdfeStatusServicoMDF",
                Url = configuracaoWsdl.Url,
                Certificado = configuracaoWsdl.CertificadoDigital,
                TimeOut = configuracaoWsdl.TimeOut
            };
        }

        public retConsStatServMDFe Autorizar(XmlNode xml)
        {
            SoapConfig.DFeCorpo.Xml = xml;

            var ret = Invoke(SoapConfig);

            var xmlTag = GetTagConverter(ret, nameof(retConsStatServMDFe));

            var retEnvi = retConsStatServMDFe.LoadXml(xmlTag);

            return retEnvi;
        }
    }
}