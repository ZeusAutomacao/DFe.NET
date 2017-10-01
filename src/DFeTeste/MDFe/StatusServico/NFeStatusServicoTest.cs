using DFe.Configuracao;
using DFe.DocumentosEletronicos.Flags;
using DFe.DocumentosEletronicos.MDFe.Classes.Extensoes;
using DFe.DocumentosEletronicos.MDFe.Classes.Servicos.StatusServico;
using DFe.DocumentosEletronicos.MDFe.Servicos.Factory;
using NUnit.Framework;

namespace DFeTeste.MDFe.StatusServico
{
    [TestFixture]
    public class NFeStatusServicoTest
    {
        private DFeConfig _config;
        private consStatServMDFe _consStatServMDFe;

        [SetUp]
        public void InicializaTestes()
        {
            _config = MDFeConfiguracaoFacotory.CriaConfiguracao();
            _consStatServMDFe = ClassesFactory.CriaConsStatServMDFe(_config);
        }

        [Test]
        public void ObterVersaoComoFoiSetadoNaConfiguracao()
        {
            Assert.AreEqual(VersaoServico.Versao300, _consStatServMDFe.versao);
        }

        [Test]
        public void ObterXServPadrao()
        {
            Assert.AreEqual("STATUS", _consStatServMDFe.xServ);
        }

        [Test]
        public void ObterAmbienteComoFoiSetadoNaConfiguracao()
        {
            Assert.AreEqual(TipoAmbiente.Homologacao, _consStatServMDFe.tpAmb);
        }

        [Test]
        public void ClasseEnviServicoGeracaoXml()
        {
            const string resultadoEsperado = "<consStatServMDFe versao=\"3.00\" xmlns=\"http://www.portalfiscal.inf.br/mdfe\"><tpAmb>2</tpAmb><xServ>STATUS</xServ></consStatServMDFe>";

            Assert.AreEqual(resultadoEsperado, _consStatServMDFe.GetXml());
        }
    }
}