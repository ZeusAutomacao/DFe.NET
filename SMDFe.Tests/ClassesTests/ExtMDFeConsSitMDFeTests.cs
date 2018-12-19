
using System;
using System.Xml;
using NUnit.Framework;
using SMDFe.Classes.Extencoes;
using SMDFe.Classes.Informacoes.ConsultaProtocolo;
using SMDFe.Tests.Dao;
using SMDFe.Tests.Entidades;

namespace SMDFe.Tests.ClassesTests
{
    [TestFixture]
    public class ExtMDFeConsSitMDFeTests
    {
        private Configuracao _configuracao;
        private MDFeConsSitMDFe _consultaProtocolo;
        private string _protocolo;
        private string _xmlesperado;

        #region SETUP
        [SetUp]
        public void CriarConfiguração()
        {
            var configuracaoDao = new ConfiguracaoDao();
            _configuracao = configuracaoDao.GetConfiguracao();

            _protocolo = "00000000000000000000000000000000000000000000";
            _xmlesperado = "xml-esperado-protocolo.xml";

            Utils.Configuracoes.MDFeConfiguracao.Instancia.CaminhoSchemas = _configuracao.ConfigWebService.CaminhoSchemas;
            Utils.Configuracoes.MDFeConfiguracao.Instancia.CaminhoSalvarXml = _configuracao.DiretorioSalvarXml;
            Utils.Configuracoes.MDFeConfiguracao.Instancia.IsSalvarXml = _configuracao.IsSalvarXml;

            Utils.Configuracoes.MDFeConfiguracao.Instancia.VersaoWebService.VersaoLayout = _configuracao.ConfigWebService.VersaoLayout;

            Utils.Configuracoes.MDFeConfiguracao.Instancia.VersaoWebService.TipoAmbiente = _configuracao.ConfigWebService.Ambiente;
            Utils.Configuracoes.MDFeConfiguracao.Instancia.VersaoWebService.UfEmitente = _configuracao.ConfigWebService.UfEmitente;
            Utils.Configuracoes.MDFeConfiguracao.Instancia.VersaoWebService.TimeOut = _configuracao.ConfigWebService.TimeOut;


        }
        #endregion

        #region Testes para a classe ExtMDFeConsSitMDFe 

        [Test]
        public void Testa_A_Criacao_De_Uma_Requisicao_Para_Consulta_Por_Protocolo_Com_Parametros()
        {
            //Arrange
            _consultaProtocolo = new MDFeConsSitMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                ChMDFe = _protocolo
            };

            //Act
            var xmlGerado = _consultaProtocolo.CriaRequestWs();


            //Assert
            Assert.IsInstanceOf<XmlDocument>(xmlGerado);
        }

        [Test]
        public void Testa_A_Requisicao_Protocolo_Criada_Com_O_Xml_Esperado()
        {
            //Arrange
            _consultaProtocolo = new MDFeConsSitMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                ChMDFe = _protocolo
            };

            var repositorioDao = new RepositorioDaoFalso();

            //Act
            var xmlGerado = _consultaProtocolo.CriaRequestWs();
            var xmlEsperado = repositorioDao.GetXmlEsperado(_xmlesperado);

            //Assert
            Assert.AreEqual(xmlEsperado.InnerXml, xmlGerado.InnerXml);
        }

        [Test]
        public void Testa_A_Criacao_De_Uma_Requisicao_Para_Consulta_Por_Protocolo_Sem_Parametros()
        {
            //Arrange
            _consultaProtocolo = new MDFeConsSitMDFe();

            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => _consultaProtocolo.CriaRequestWs());

            //Assert
            Assert.IsInstanceOf<InvalidOperationException>(exception);

        }

        [Test]
        public void Testa_A_Criacao_De_Uma_Requisicao_Para_Consulta_Por_Protocolo_Sem_Versao()
        {
            //Arrange
            _consultaProtocolo = new MDFeConsSitMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                ChMDFe = _protocolo
            };
            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => _consultaProtocolo.CriaRequestWs());

            //Assert
            Assert.IsInstanceOf<InvalidOperationException>(exception);

        }

        [Test]
        public void Testa_A_Criacao_De_Uma_Requisicao_Para_Consulta_Por_Protocolo_Sem_Ambiente()
        {
            //Arrange
            _consultaProtocolo = new MDFeConsSitMDFe()
            {
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                ChMDFe = _protocolo
            };
            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => _consultaProtocolo.CriaRequestWs());

            //Assert
            Assert.IsInstanceOf<InvalidOperationException>(exception);

        }

        [Test]
        public void Testa_A_Criacao_De_Uma_Requisicao_Para_Consulta_Por_Recibo_Sem_Ambiente_E_Versao()
        {
            //Arrange
            _consultaProtocolo = new MDFeConsSitMDFe()
            {
                ChMDFe = _protocolo
            };
            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => _consultaProtocolo.CriaRequestWs());

            //Assert
            Assert.IsInstanceOf<InvalidOperationException>(exception);

        }

        [Test]
        public void Testa_A_Funcao_Por_Protocolo_Para_Salvar_Xml_Localmente_Com_Parametros_Validos()
        {
            //Arrange
            _consultaProtocolo = new MDFeConsSitMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                ChMDFe = _protocolo
            };

            //Act
            _consultaProtocolo.SalvarXmlEmDisco();

            //Assert
            Assert.That(true);
        }

        #endregion
    }
}
