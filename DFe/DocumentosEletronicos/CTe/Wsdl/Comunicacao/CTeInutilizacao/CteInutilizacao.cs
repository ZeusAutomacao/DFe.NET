using System.Xml;
using DFe.DocumentosEletronicos.CTe.Classes.Retorno.Inutilizacao;
using DFe.DocumentosEletronicos.CTe.Wsdl.Configuracao;
using DFe.DocumentosEletronicos.Wsdl;
using DFe.DocumentosEletronicos.Wsdl.Cabecalho;
using DFe.DocumentosEletronicos.Wsdl.Corpo;
using DFe.Wsdl;

namespace DFe.DocumentosEletronicos.CTe.Wsdl.Comunicacao.CTeInutilizacao
{
    public class CteInutilizacao : DFeSoapHttpClientProtocol
    {
        private DFeSoapConfig SoapConfig { get; set; }

        public CteInutilizacao(WsdlConfiguracao configuracaoWsdl)
        {
            SoapConfig = new DFeSoapConfig
            {
                DFeCorpo = new DFeCorpo("http://www.portalfiscal.inf.br/cte/wsdl/CteInutilizacao", new CteTagCorpo()),
                DFeCabecalho = new DFeCabecalho(configuracaoWsdl.EstadoUF, configuracaoWsdl.VersaoLayout, new CteTagCabecalho(), "http://www.portalfiscal.inf.br/cte/wsdl/CteInutilizacao"),
                Metodo = "http://www.portalfiscal.inf.br/cte/wsdl/CteInutilizacao/cteInutilizacaoCT",
                Url = configuracaoWsdl.Url,
                Certificado = configuracaoWsdl.CertificadoDigital,
                TimeOut = configuracaoWsdl.TimeOut
            };
        }

        public retInutCTe Autorizar(XmlNode xml)
        {
            SoapConfig.DFeCorpo.Xml = xml;

            var ret = Invoke(SoapConfig);

            var xmlTag = GetTagConverter(ret, nameof(retInutCTe));

            var retInutCte = retInutCTe.LoadXml(xmlTag);

            return retInutCte;
        }
    }
}