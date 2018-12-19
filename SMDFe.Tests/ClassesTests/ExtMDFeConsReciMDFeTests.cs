using System;
using System.Xml;
using  NUnit.Framework;
using SMDFe.Classes.Extencoes;
using SMDFe.Classes.Informacoes.RetRecepcao;
using SMDFe.Tests.Dao;
using SMDFe.Tests.Entidades;

namespace SMDFe.Tests.ClassesTests
{
    [TestFixture]
    public class ExtMDFeConsReciMDFeTests
    {

        private Configuracao _configuracao;
        private string _recibo;
        private string _xmlEsperado;
        private MDFeConsReciMDFe _consultaRecibo;

        #region SETUP
        [SetUp]
        public void CriarConfiguração()
        {
            var configuracaoDao = new ConfiguracaoDao();
            _configuracao = configuracaoDao.GetConfiguracao();


            _recibo = "000000000000000";
            _xmlEsperado = "xml-esperado-consulta-recibo.xml";

            Utils.Configuracoes.MDFeConfiguracao.Instancia.CaminhoSchemas = _configuracao.ConfigWebService.CaminhoSchemas;
            Utils.Configuracoes.MDFeConfiguracao.Instancia.CaminhoSalvarXml = _configuracao.DiretorioSalvarXml;
            Utils.Configuracoes.MDFeConfiguracao.Instancia.IsSalvarXml = _configuracao.IsSalvarXml;

            Utils.Configuracoes.MDFeConfiguracao.Instancia.VersaoWebService.VersaoLayout = _configuracao.ConfigWebService.VersaoLayout;

            Utils.Configuracoes.MDFeConfiguracao.Instancia.VersaoWebService.TipoAmbiente = _configuracao.ConfigWebService.Ambiente;
            Utils.Configuracoes.MDFeConfiguracao.Instancia.VersaoWebService.UfEmitente = _configuracao.ConfigWebService.UfEmitente;
            Utils.Configuracoes.MDFeConfiguracao.Instancia.VersaoWebService.TimeOut = _configuracao.ConfigWebService.TimeOut;


        }
        #endregion

        #region Testes para a classe ExtMDFeConsReciMDFe 

        [Test]
        public void Testa_A_Criacao_De_Uma_Requisicao_Para_Consulta_Por_Recibo_Com_Parametros()
        {
            //Arrange
            _consultaRecibo = new MDFeConsReciMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                NRec = _recibo
            };

            //Act
            var xmlGerado = _consultaRecibo.CriaRequestWs();


            //Assert
            Assert.IsInstanceOf<XmlDocument>(xmlGerado);
        }

        [Test]
        public void Testa_A_Requisicao_Recibo_Criada_Com_O_Xml_Esperado()
        {
            //Arrange
            _consultaRecibo = new MDFeConsReciMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                NRec = _recibo
            };
            var repositorioDao = new RepositorioDaoFalso();

            //Act
            var xmlGerado = _consultaRecibo.CriaRequestWs();
            var xmlEsperado = repositorioDao.GetXmlEsperado(_xmlEsperado);

            //Assert
            Assert.AreEqual(xmlEsperado.InnerXml, xmlGerado.InnerXml);
        }

        [Test]
        public void Testa_A_Criacao_De_Uma_Requisicao_Para_Consulta_Por_Recibo_Sem_Parametros()
        {
            //Arrange
            _consultaRecibo = new MDFeConsReciMDFe();

            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => _consultaRecibo.CriaRequestWs());

            //Assert
            Assert.IsInstanceOf<InvalidOperationException>(exception);

        }

        [Test]
        public void Testa_A_Criacao_De_Uma_Requisicao_Para_Consulta_Por_Recibo_Sem_Versao()
        {
            //Arrange
            _consultaRecibo = new MDFeConsReciMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                NRec = _recibo
            };
            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => _consultaRecibo.CriaRequestWs());

            //Assert
            Assert.IsInstanceOf<InvalidOperationException>(exception);

        }

        [Test]
        public void Testa_A_Criacao_De_Uma_Requisicao_Para_Consulta_Por_Recibo_Sem_Ambiente()
        {
            //Arrange
            _consultaRecibo = new MDFeConsReciMDFe()
            {
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                NRec = _recibo
            };
            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => _consultaRecibo.CriaRequestWs());

            //Assert
            Assert.IsInstanceOf<InvalidOperationException>(exception);

        }

        [Test]
        public void Testa_A_Criacao_De_Uma_Requisicao_Para_Consulta_Por_Recibo_Sem_Ambiente_E_Versao()
        {
            //Arrange
            _consultaRecibo = new MDFeConsReciMDFe()
            {
                NRec = _recibo
            };
            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => _consultaRecibo.CriaRequestWs());

            //Assert
            Assert.IsInstanceOf<InvalidOperationException>(exception);

        }

        [Test]
        public void Testa_A_Funcao_Por_Recibo_Para_Salvar_Xml_Localmente_Com_Parametros_Validos()
        {
            //Arrange
            _consultaRecibo = new MDFeConsReciMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                NRec = _recibo
            };

            //Act
            _consultaRecibo.SalvarXmlEmDisco();

            //Assert
            Assert.That(true);
        }

        #endregion
    }
}