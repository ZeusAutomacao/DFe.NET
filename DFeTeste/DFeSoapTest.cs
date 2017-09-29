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
            config.DFeCabecalho = new DFeCabecalho(Estado.GO, VersaoServico.Versao300);
            config.NamespaceBody = "http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcaoOS";
            config.NamespaceHeader = "http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcaoOS";
            config.Metodo = "http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcaoOS/cteRecepcaoOS";
            config.Url = @"https://cte-homologacao.svrs.rs.gov.br/ws/cterecepcaoos/cterecepcaoos.asmx";
            config.TimeOut = 50000;
            config.TagTagCabecalho = new CteTagCabecalho();
            config.TagCorpo = new CteTagCorpo();

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;


            Assert.AreEqual("http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcaoOS", config.NamespaceBody);
        }
    }
}
