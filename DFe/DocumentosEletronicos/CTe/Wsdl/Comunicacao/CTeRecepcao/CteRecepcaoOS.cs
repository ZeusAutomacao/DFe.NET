using System.Xml;
using DFe.DocumentosEletronicos.CTe.CTeOS.Servicos.Autorizacao;
using DFe.DocumentosEletronicos.CTe.Wsdl.Configuracao;
using DFe.DocumentosEletronicos.Wsdl;
using DFe.DocumentosEletronicos.Wsdl.Cabecalho;
using DFe.DocumentosEletronicos.Wsdl.Corpo;
using DFe.Wsdl;

namespace DFe.DocumentosEletronicos.CTe.Wsdl.Comunicacao.CTeRecepcao
{
    public class CteRecepcaoOS : DFeSoapHttpClientProtocol
    {
        private DFeSoapConfig SoapConfig { get; }

        public CteRecepcaoOS(WsdlConfiguracao configuracaoWsdl)
        {
            SoapConfig = new DFeSoapConfig
            {
                DFeCorpo = new DFeCorpo("http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcaoOS", new CteTagCorpo()),
                DFeCabecalho = new DFeCabecalho(configuracaoWsdl.EstadoUF, configuracaoWsdl.VersaoLayout, new CteTagCabecalho(), "http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcaoOS"),
                Metodo = "http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcaoOS/cteRecepcaoOS",
                Url = configuracaoWsdl.Url,
                Certificado = configuracaoWsdl.CertificadoDigital,
                TimeOut = configuracaoWsdl.TimeOut
            };
        }

        public retCTeOS Autorizar(XmlNode xml)
        {
            SoapConfig.DFeCorpo.Xml = xml;

            var ret = Invoke(SoapConfig);

            var xmlConverter = GetTagConverter(ret, "retCTeOS");

            var retCteOs = retCTeOS.LoadXml(xmlConverter);

            return retCteOs;
        }
    }
}