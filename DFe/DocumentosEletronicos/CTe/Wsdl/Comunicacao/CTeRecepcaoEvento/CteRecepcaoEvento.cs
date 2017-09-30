using System.Xml;
using DFe.DocumentosEletronicos.CTe.Classes.Retorno.Evento;
using DFe.DocumentosEletronicos.CTe.Wsdl.Configuracao;
using DFe.DocumentosEletronicos.Wsdl;
using DFe.DocumentosEletronicos.Wsdl.Cabecalho;
using DFe.DocumentosEletronicos.Wsdl.Corpo;
using DFe.Wsdl;

namespace DFe.DocumentosEletronicos.CTe.Wsdl.Comunicacao.CTeRecepcaoEvento
{
    public class CteRecepcaoEvento : DFeSoapHttpClientProtocol
    {
        private DFeSoapConfig SoapConfig { get; set; }

        public CteRecepcaoEvento(WsdlConfiguracao configuracaoWsdl)
        {
            SoapConfig = new DFeSoapConfig
            {
                DFeCorpo = new DFeCorpo("http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcaoEvento", new CteTagCorpo()),
                DFeCabecalho = new DFeCabecalho(configuracaoWsdl.EstadoUF, configuracaoWsdl.VersaoLayout, new CteTagCabecalho(), "http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcaoEvento"),
                Metodo = "http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcaoEvento/cteRecepcaoEvento",
                Url = configuracaoWsdl.Url,
                Certificado = configuracaoWsdl.CertificadoDigital,
                TimeOut = configuracaoWsdl.TimeOut
            };
        }

        public retEventoCTe Autorizar(XmlNode xml)
        {
            SoapConfig.DFeCorpo.Xml = xml;

            var ret = Invoke(SoapConfig);

            var xmlTag = GetTagConverter(ret, nameof(retEventoCTe));

            var retEnvi = retEventoCTe.LoadXml(xmlTag);

            return retEnvi;
        }
    }
}