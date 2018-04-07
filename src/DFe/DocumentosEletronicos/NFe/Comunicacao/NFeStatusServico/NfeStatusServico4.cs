using System;
using System.Xml;
using DFe.DocumentosEletronicos.NFe.Classes.Retorno.Status;
using DFe.DocumentosEletronicos.NFe.Wsdl;
using DFe.DocumentosEletronicos.NFe.Wsdl.Configuracao;
using DFe.DocumentosEletronicos.Wsdl;
using DFe.DocumentosEletronicos.Wsdl.Cabecalho;
using DFe.DocumentosEletronicos.Wsdl.Corpo;
using DFe.Wsdl;

namespace DFe.DocumentosEletronicos.NFe.Comunicacao.NFeStatusServico
{
    public class NfeStatusServico4 : DFeSoapHttpClientProtocol, INfeServico
    {
        private DFeSoapConfig SoapConfig { get; set; }

        public NfeStatusServico4(WsdlConfiguracao configuracaoWsdl)
        {
            SoapConfig = new DFeSoapConfig
            {
                DFeCorpo = new DFeCorpo("http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4", new NfeTagCorpo()),
                DFeCabecalho = new DFeCabecalho(configuracaoWsdl.EstadoUF, configuracaoWsdl.VersaoLayout, new TagCabecalhoVazia(), "http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4"),
                Metodo = "http://www.portalfiscal.inf.br/nfe/wsdl/NFeStatusServico4/nfeStatusServicoNF",
                Url = configuracaoWsdl.Url,
                Certificado = configuracaoWsdl.CertificadoDigital,
                TimeOut = configuracaoWsdl.TimeOut
            };
        }

        [Obsolete("Vai ser descontinuado")]
        public nfeCabecMsg nfeCabecMsg { get; set; }


        public XmlNode Execute(XmlNode xml)
        {
            SoapConfig.DFeCorpo.Xml = xml;

            var ret = Invoke(SoapConfig);

            var xmlTag = GetTagConverter(ret, nameof(retConsStatServ));

            var documento = new XmlDocument();
            documento.LoadXml(xmlTag);

            return documento.DocumentElement;
        }
    }
}