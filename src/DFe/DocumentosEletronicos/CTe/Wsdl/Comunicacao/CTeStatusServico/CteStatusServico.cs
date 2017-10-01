using System.Xml;
using DFe.DocumentosEletronicos.CTe.Classes.Retorno.StatusServico;
using DFe.DocumentosEletronicos.CTe.Wsdl.Configuracao;
using DFe.DocumentosEletronicos.Wsdl;
using DFe.DocumentosEletronicos.Wsdl.Cabecalho;
using DFe.DocumentosEletronicos.Wsdl.Corpo;
using DFe.Wsdl;

namespace DFe.DocumentosEletronicos.CTe.Wsdl.Comunicacao.CTeStatusServico
{
    public class CteStatusServico : DFeSoapHttpClientProtocol
    {
        private DFeSoapConfig SoapConfig { get; set; }

        public CteStatusServico(WsdlConfiguracao configuracaoWsdl)
        {
            SoapConfig = new DFeSoapConfig
            {
                DFeCorpo = new DFeCorpo("http://www.portalfiscal.inf.br/cte/wsdl/CteStatusServico", new CteTagCorpo()),
                DFeCabecalho = new DFeCabecalho(configuracaoWsdl.EstadoUF, configuracaoWsdl.VersaoLayout, new CteTagCabecalho(), "http://www.portalfiscal.inf.br/cte/wsdl/CteStatusServico"),
                Metodo = "http://www.portalfiscal.inf.br/cte/wsdl/CteStatusServico/cteStatusServicoCT",
                Url = configuracaoWsdl.Url,
                Certificado = configuracaoWsdl.CertificadoDigital,
                TimeOut = configuracaoWsdl.TimeOut
            };
        }

        public retConsStatServCte Autorizar(XmlNode xml)
        {
            SoapConfig.DFeCorpo.Xml = xml;

            var ret = Invoke(SoapConfig);

            var xmlTag = GetTagConverter(ret, nameof(retConsStatServCte));

            var retEnvi = retConsStatServCte.LoadXml(xmlTag);

            return retEnvi;
        }
    }
}