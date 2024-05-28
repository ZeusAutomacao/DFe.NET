using System;
using System.Xml;
using MDFe.Classes.Extensoes;
using MDFe.Classes.Informacoes.StatusServico;
using MDFe.Tests.Dao;
using MDFe.Tests.Entidades;
using Xunit;

namespace MDFe.Tests.ClassesTests
{
    public class ExtMDFeConsStatServMDFeTests: IDisposable
    {
        #region Variáveis
        private Configuracao _configuracao;
        private MDFeConsStatServMDFe _consultaStatus;
        private readonly string _xmlEsperado;
        #endregion

        #region SETUP
        public ExtMDFeConsStatServMDFeTests()
        {
            var configuracaoDao = new ConfiguracaoDao();
            _configuracao = configuracaoDao.GetConfiguracao();
            _xmlEsperado = "xml-esperado-status-servico.xml";

            var configuracoes = new ConfiguracaoUtilsDao(_configuracao);
            configuracoes.setCongiguracoes();
        }

        public void Dispose()
        {
            _consultaStatus = new MDFeConsStatServMDFe();
        }
        #endregion

        #region Testes para a classe ExtMDFeConsStatServMDFe

        [Fact]
        public void Deve_Criar_Uma_Requisicao_Para_Consulta_Por_Status_Com_Parametros_Validos()
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
            Assert.IsType<XmlDocument>(xmlGerado);
        }

        [Fact]
        public void Deve_Criar_Uma_Requisicao_Para_Consulta_Por_Status_Nao_Nula()
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
            Assert.NotNull(xmlGerado);
        }

        [Fact]
        public void Deve_Validar_A_Requisicao_Status_Criada_Com_O_Xml_Esperado()
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
            Assert.Equal(xmlEsperado.InnerXml, xmlGerado.InnerXml);
        }


        [Fact]
        public void Deve_Gerar_Uma_Excecao_Para_Criacao_De_Consulta_Por_Status_Sem_Parametros()
        {
            //Arrange
            _consultaStatus = new MDFeConsStatServMDFe();

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => _consultaStatus.CriaRequestWs());

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Para_Criacao_De_Consulta_Por_Status_Sem_Versao()
        {
            //Arrange
            _consultaStatus = new MDFeConsStatServMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente
            };

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => _consultaStatus.CriaRequestWs());

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Para_Criacao_De_Consulta_Por_Status_Sem_Ambiente()
        {
            //Arrange
            _consultaStatus = new MDFeConsStatServMDFe()
            {
                Versao = _configuracao.ConfigWebService.VersaoLayout
            };

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => _consultaStatus.CriaRequestWs());

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Salvar_Xml_Localmente_Para_Consulta_Por_Status()
        {
            //Arrange
            _consultaStatus = new MDFeConsStatServMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout
            };

            //Act
            var result = Record.Exception(() =>_consultaStatus.SalvarXmlEmDisco());

            //Assert
            Assert.Null(result);
        }

        #endregion
    }
}
