using DFe.DocumentosEletronicos.Entidades;
using DFe.DocumentosEletronicos.Flags;
using DFe.DocumentosEletronicos.Wsdl;
using DFe.DocumentosEletronicos.Wsdl.Cabecalho;
using DFe.DocumentosEletronicos.Wsdl.Corpo;
using DFe.Wsdl;
using NUnit.Framework;

namespace DFeTeste.Soap
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


            Assert.AreEqual("http://www.portalfiscal.inf.br/cte/wsdl/CteRecepcaoOS", config.DFeCorpo.NamespaceBody);
        }
    }
}
