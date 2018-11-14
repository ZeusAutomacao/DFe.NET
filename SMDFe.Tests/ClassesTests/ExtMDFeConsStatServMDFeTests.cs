using System;
using System.Xml;
using NUnit.Framework;
using SMDFe.Classes.Extencoes;
using SMDFe.Classes.Informacoes.StatusServico;
using SMDFe.Tests.Dao;
using SMDFe.Tests.Entidades;

namespace SMDFe.Tests.ClassesTests
{
    [TestFixture]
    public class ExtMDFeConsStatServMDFeTests
    {
        private Configuracao _configuracao;
        private MDFeConsStatServMDFe _consultaStatus;
        private string _xmlEsperado;

        #region SETUP
        [SetUp]
        public void CriarConfiguração()
        {
            var configuracaoDao = new ConfiguracaoDao();
            _configuracao = configuracaoDao.GetConfiguracao();
            _xmlEsperado = "xml-esperado-status-servico.xml";

            Utils.Configuracoes.MDFeConfiguracao.CaminhoSchemas = _configuracao.ConfigWebService.CaminhoSchemas;
            Utils.Configuracoes.MDFeConfiguracao.CaminhoSalvarXml = _configuracao.DiretorioSalvarXml;
            Utils.Configuracoes.MDFeConfiguracao.IsSalvarXml = _configuracao.IsSalvarXml;

            Utils.Configuracoes.MDFeConfiguracao.VersaoWebService.VersaoLayout = _configuracao.ConfigWebService.VersaoLayout;

            Utils.Configuracoes.MDFeConfiguracao.VersaoWebService.TipoAmbiente = _configuracao.ConfigWebService.Ambiente;
            Utils.Configuracoes.MDFeConfiguracao.VersaoWebService.UfEmitente = _configuracao.ConfigWebService.UfEmitente;
            Utils.Configuracoes.MDFeConfiguracao.VersaoWebService.TimeOut = _configuracao.ConfigWebService.TimeOut;

        }
        #endregion

        #region Testes para a classe ExtMDFeConsStatServMDFe

        [Test]
        public void Testa_A_Criacao_De_Uma_Requisicao_Para_Consulta_Por_Status_Com_Parametros()
        {
            //Arrange
            _consultaStatus = new MDFeConsStatServMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout
            };

            //Act
            var xmlGerado = _consultaStatus.CriaRequestWs();


            //Assert
            Assert.IsInstanceOf<XmlDocument>(xmlGerado);
        }

        [Test]
        public void Testa_A_Requisicao_Status_Criada_Com_O_Xml_Esperado()
        {
            //Arrange
            _consultaStatus = new MDFeConsStatServMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout
            };

            var repositorioDao = new RepositorioDaoFalso();

            //Act
            var xmlGerado = _consultaStatus.CriaRequestWs();
            var xmlEsperado = repositorioDao.GetXmlEsperado(_xmlEsperado);

            //Assert
            Assert.AreEqual(xmlEsperado.InnerXml, xmlGerado.InnerXml);
        }


        [Test]
        public void Testa_A_Criacao_De_Uma_Requisicao_Para_Consulta_Por_Status_Sem_Parametros()
        {
            //Arrange
            _consultaStatus = new MDFeConsStatServMDFe();

            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => _consultaStatus.CriaRequestWs());

            //Assert
            Assert.IsInstanceOf<InvalidOperationException>(exception);

        }

        [Test]
        public void Testa_A_Criacao_De_Uma_Requisicao_Para_Consulta_Por_Status_Sem_Versao()
        {
            //Arrange
            _consultaStatus = new MDFeConsStatServMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente
            };
            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => _consultaStatus.CriaRequestWs());

            //Assert
            Assert.IsInstanceOf<InvalidOperationException>(exception);

        }

        [Test]
        public void Testa_A_Criacao_De_Uma_Requisicao_Para_Consulta_Por_Status_Sem_Ambiente()
        {
            //Arrange
            _consultaStatus = new MDFeConsStatServMDFe()
            {
                Versao = _configuracao.ConfigWebService.VersaoLayout
            };
            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => _consultaStatus.CriaRequestWs());

            //Assert
            Assert.IsInstanceOf<InvalidOperationException>(exception);

        }

        [Test]
        public void Testa_A_Funcao_Por_Status_Para_Salvar_Xml_Localmente_Com_Parametros_Validos()
        {
            //Arrange
            _consultaStatus = new MDFeConsStatServMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout
            };

            //Act
            _consultaStatus.SalvarXmlEmDisco();

            //Assert
            Assert.That(true);
        }

        #endregion
    }
}
