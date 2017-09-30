using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using DFe.DocumentosEletronicos.CTe.Classes.Extensoes;
using DFe.DocumentosEletronicos.CTe.CTeOS;
using DFe.DocumentosEletronicos.CTe.Wsdl.Gerado.CTeRecepcao;
using DFe.DocumentosEletronicos.Entidades;
using DFe.DocumentosEletronicos.Flags;
using DFe.DocumentosEletronicos.Wsdl;
using DFe.DocumentosEletronicos.Wsdl.Cabecalho;
using DFe.DocumentosEletronicos.Wsdl.Corpo;
using DFe.Wsdl;
using NUnit.Framework;

namespace DFeTeste
{
    [TestFixture(Description = "Testes Para Classes Soap do projeto")]
    public class DFeSoapTest
    {
        [Test]
        public void TesteConfig()
        {
            var config = new DFeSoapConfig();
            config.DFeCabecalho = new DFeCabecalho(Estado.GO, VersaoServico.Versao300, new CteTagCabecalho(), "http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcaoOS");
            config.DFeCorpo = new DFeCorpo("http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcaoOS", new CteTagCorpo());
            config.Metodo = "http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcaoOS/cteRecepcaoOS";
            config.Url = @"https://cte-homologacao.svrs.rs.gov.br/ws/cterecepcaoos/cterecepcaoos.asmx";
            config.TimeOut = 50000;

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

            config.Certificado = new X509Certificate2(@"C:\Users\rober\Documents\Certificados\AGIL4 TECNOLOGIA LTDA  ME21025760000123.pfx", "agil4@123");
            CTeOS cteOs = CTeOS.LoadXmlArquivo(@"C:\Users\rober\Desktop\xmlcteos\21351378000100\setembro\Autorizar\Enviado\52170921351378000100670010000000081603356706-cte.xml");

            var request = new XmlDocument();
            request.LoadXml(cteOs.ObterXmlString());

            config.DFeCorpo.Xml = request;

            var xml = new CteRecepcaoOSS().Autorizar(config);


            Assert.AreEqual("http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcaoOS", config.DFeCorpo.NamespaceBody);
        }
    }
}
