using System;
using System.Xml;
using Xunit;
using SMDFe.Classes.Extencoes;
using SMDFe.Classes.Informacoes.RetRecepcao;
using SMDFe.Tests.Dao;
using SMDFe.Tests.Entidades;

namespace SMDFe.Tests.ClassesTests
{

    public class ExtMDFeConsReciMDFeTests: IDisposable
    {
        #region Variáveis
        private Configuracao _configuracao;
        private readonly string _recibo;
        private readonly string _xmlEsperado;
        private MDFeConsReciMDFe _consultaRecibo;
        #endregion

        #region SETUP
        
        public ExtMDFeConsReciMDFeTests()
        {
            var configuracaoDao = new ConfiguracaoDao();
            _configuracao = configuracaoDao.GetConfiguracao();

            _recibo = "000000000000000";
            _xmlEsperado = "xml-esperado-consulta-recibo.xml";

            var configuracoes = new ConfiguracaoUtilsDao(_configuracao);
            configuracoes.setCongiguracoes();
        }

        public void Dispose()
        {
            _consultaRecibo = new MDFeConsReciMDFe();
        }

        #endregion

        #region Testes para a classe ExtMDFeConsReciMDFe 

        [Fact]
        public void Deve_Testar_A_Criacao_De_Uma_Requisicao_Para_Consulta_Por_Recibo_Com_Parametros()
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
            Assert.NotNull(xmlGerado);
            Assert.IsType<XmlDocument>(xmlGerado);
        }

        [Fact]
        public void Deve_Testar_A_Requisicao_Recibo_Criada_Com_O_Xml_Esperado()
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
            Assert.NotNull(xmlEsperado);
            Assert.NotNull(xmlGerado);
            Assert.Equal(xmlEsperado.InnerXml, xmlGerado.InnerXml);
        }

        [Fact]
        public void Deve_Testar_A_Criacao_De_Uma_Requisicao_Para_Consulta_Por_Recibo_Sem_Parametros()
        {
            //Arrange
            _consultaRecibo = new MDFeConsReciMDFe();

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => _consultaRecibo.CriaRequestWs());

            //Assert
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Testar_A_Criacao_De_Uma_Requisicao_Para_Consulta_Por_Recibo_Sem_Versao()
        {
            //Arrange
            _consultaRecibo = new MDFeConsReciMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                NRec = _recibo
            };
            //Act
            var exception = Assert.ThrowsAny<Exception>(() => _consultaRecibo.CriaRequestWs());

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Testar_A_Criacao_De_Uma_Requisicao_Para_Consulta_Por_Recibo_Sem_Ambiente()
        {
            //Arrange
            _consultaRecibo = new MDFeConsReciMDFe()
            {
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                NRec = _recibo
            };
            //Act
            var exception = Assert.ThrowsAny<Exception>(() => _consultaRecibo.CriaRequestWs());

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Testar_A_Criacao_De_Uma_Requisicao_Para_Consulta_Por_Recibo_Sem_Ambiente_E_Versao()
        {
            //Arrange
            _consultaRecibo = new MDFeConsReciMDFe()
            {
                NRec = _recibo
            };
            //Act
            var exception = Assert.ThrowsAny<Exception>(() => _consultaRecibo.CriaRequestWs());

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Testar_A_Funcao_Por_Recibo_Para_Salvar_Xml_Localmente_Com_Parametros_Validos()
        {
            //Arrange
            _consultaRecibo = new MDFeConsReciMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                NRec = _recibo
            };

            //Act
            var result = Record.Exception(() => _consultaRecibo.SalvarXmlEmDisco());

            //Assert
            Assert.Null(result);
        }

        #endregion

        
    }
}