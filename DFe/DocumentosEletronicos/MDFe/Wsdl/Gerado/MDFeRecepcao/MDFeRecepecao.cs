using System.Xml;
using DFe.DocumentosEletronicos.MDFe.Classes.Retorno.Autorizacao;
using DFe.DocumentosEletronicos.MDFe.Wsdl.Configuracao;
using DFe.DocumentosEletronicos.Wsdl;
using DFe.DocumentosEletronicos.Wsdl.Cabecalho;
using DFe.DocumentosEletronicos.Wsdl.Corpo;
using DFe.Wsdl;

namespace DFe.DocumentosEletronicos.MDFe.Wsdl.Gerado.MDFeRecepcao
{
    public class MDFeRecepecao : DFeSoapHttpClientProtocol
    {
        private DFeSoapConfig SoapConfig { get; set; }

        public MDFeRecepecao(WsdlConfiguracao configuracaoWsdl)
        {
            SoapConfig = new DFeSoapConfig
            {
                DFeCorpo = new DFeCorpo("http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeRecepcao", new MdfeTagCorpo()),
                DFeCabecalho = new DFeCabecalho(configuracaoWsdl.EstadoUF, configuracaoWsdl.VersaoLayout, new MdfeTagCabecalho(), "http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeRecepcao"),
                Metodo = "http://www.portalfiscal.inf.br/mdfe/wsdl/MDFeRecepcao/mdfeRecepcaoLote",
                Url = configuracaoWsdl.Url,
                Certificado = configuracaoWsdl.CertificadoDigital,
                TimeOut = configuracaoWsdl.TimeOut
            };
        }

        public retEnviMDFe Autorizar(XmlNode xml)
        {
            SoapConfig.DFeCorpo.Xml = xml;

            var ret = Invoke(SoapConfig);

            var xmlTag = GetTagConverter(ret, nameof(retEnviMDFe));

            var retEnvi = retEnviMDFe.LoadXml(xmlTag);

            return retEnvi;
        }
    }
}