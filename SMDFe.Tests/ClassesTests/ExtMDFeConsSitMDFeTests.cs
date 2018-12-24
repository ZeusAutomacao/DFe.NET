using System;
using System.Xml;
using MDFe.Classes.Extensoes;
using MDFe.Classes.Informacoes.ConsultaProtocolo;
using MDFe.Tests.Dao;
using MDFe.Tests.Entidades;
using Xunit;

namespace MDFe.Tests.ClassesTests
{
    
    public class ExtMDFeConsSitMDFeTests: IDisposable
    {
        #region Variáveis
        private Configuracao _configuracao;
        private MDFeConsSitMDFe _consultaProtocolo;
        private readonly string _protocolo;
        private readonly string _xmlesperado;
        #endregion

        #region SETUP
        public ExtMDFeConsSitMDFeTests()
        {
            var configuracaoDao = new ConfiguracaoDao();
            _configuracao = configuracaoDao.GetConfiguracao();

            _protocolo = "00000000000000000000000000000000000000000000";
            _xmlesperado = "xml-esperado-protocolo.xml";

            var configuracoes = new ConfiguracaoUtilsDao(_configuracao);
            configuracoes.setCongiguracoes();
        }

        public void Dispose()
        {
            _consultaProtocolo = new MDFeConsSitMDFe();
        }
        #endregion

        #region Testes para a classe ExtMDFeConsSitMDFe 

        [Fact]
        public void Deve_Criar_Uma_Requisicao_Para_Consulta_Por_Protocolo_Com_Parametros_Validos()
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
            Assert.IsType<XmlDocument>(xmlGerado);
        }

        [Fact]
        public void Deve_Criar_Uma_Requisicao_Para_Consulta_Por_Protocolo_Nao_Nula()
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
            Assert.NotNull(xmlGerado);
        }

        [Fact]
        public void Deve_Validar_A_Requisicao_Protocolo_Criada_Com_O_Xml_Esperado()
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
            Assert.Equal(xmlEsperado.InnerXml, xmlGerado.InnerXml);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Para_Criacao_De_Consulta_Por_Protocolo_Sem_Parametros()
        {
            //Arrange
            _consultaProtocolo = new MDFeConsSitMDFe();

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => _consultaProtocolo.CriaRequestWs());

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Para_Criacao_De_Consulta_Por_Protocolo_Sem_Versao()
        {
            //Arrange
            _consultaProtocolo = new MDFeConsSitMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                ChMDFe = _protocolo
            };

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => _consultaProtocolo.CriaRequestWs());

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Para_Criacao_De_Consulta_Por_Protocolo_Sem_Ambiente()
        {
            //Arrange
            _consultaProtocolo = new MDFeConsSitMDFe()
            {
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                ChMDFe = _protocolo
            };

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => _consultaProtocolo.CriaRequestWs());

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Para_Criacao_De_Consulta_Por_Recibo_Sem_Ambiente_E_Versao()
        {
            //Arrange
            _consultaProtocolo = new MDFeConsSitMDFe()
            {
                ChMDFe = _protocolo
            };

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => _consultaProtocolo.CriaRequestWs());

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Salvar_Xml_Localmente_Para_Consulta_Por_Protocolo()
        {
            //Arrange
            _consultaProtocolo = new MDFeConsSitMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                ChMDFe = _protocolo
            };

            //Act
            var result = Record.Exception(() => _consultaProtocolo.SalvarXmlEmDisco());

            //Assert
            Assert.Null(result);
        }

        #endregion
    }
}
