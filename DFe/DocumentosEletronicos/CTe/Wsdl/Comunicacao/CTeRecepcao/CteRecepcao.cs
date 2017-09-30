using System.Xml;
using DFe.DocumentosEletronicos.CTe.Classes.Retorno.Autorizacao;
using DFe.DocumentosEletronicos.CTe.Wsdl.Configuracao;
using DFe.DocumentosEletronicos.Wsdl;
using DFe.DocumentosEletronicos.Wsdl.Cabecalho;
using DFe.DocumentosEletronicos.Wsdl.Corpo;
using DFe.Wsdl;

namespace DFe.DocumentosEletronicos.CTe.Wsdl.Comunicacao.CTeRecepcao
{

    public class CteRecepcao : DFeSoapHttpClientProtocol
    {
        private DFeSoapConfig SoapConfig { get; set; }

        public CteRecepcao(WsdlConfiguracao configuracaoWsdl)
        {
            SoapConfig = new DFeSoapConfig
            {
                DFeCorpo = new DFeCorpo("http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcao", new CteTagCorpo()),
                DFeCabecalho = new DFeCabecalho(configuracaoWsdl.EstadoUF, configuracaoWsdl.VersaoLayout, new CteTagCabecalho(), "http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcao"),
                Metodo = "http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcao/cteRecepcaoLote",
                Url = configuracaoWsdl.Url,
                Certificado = configuracaoWsdl.CertificadoDigital,
                TimeOut = configuracaoWsdl.TimeOut
            };
        }

        public retEnviCte Autorizar(XmlNode xml)
        {
            SoapConfig.DFeCorpo.Xml = xml;

            var ret = Invoke(SoapConfig);

            var xmlTag = GetTagConverter(ret, nameof(retEnviCte));

            var retEnvi = retEnviCte.LoadXml(xmlTag);

            return retEnvi;
        }
    }
}