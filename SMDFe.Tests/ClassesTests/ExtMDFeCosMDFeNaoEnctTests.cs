using System;
using System.Xml;
using Xunit;
using SMDFe.Classes.Extencoes;
using SMDFe.Classes.Informacoes.ConsultaNaoEncerrados;
using SMDFe.Tests.Dao;
using SMDFe.Tests.Entidades;

namespace SMDFe.Tests.ClassesTests
{
    
    public class ExtMDFeCosMDFeNaoEnctTests : IDisposable
    {
        #region variáveis
        private Configuracao _configuracao;
        private MDFeCosMDFeNaoEnc _consultaMdFeNaoEnc;
        private readonly string _cnpj;
        private readonly string _xmlEsperado;
        #endregion

        #region SETUP

        public ExtMDFeCosMDFeNaoEnctTests()
        {
            var configuracaoDao = new ConfiguracaoDao();
            _configuracao = configuracaoDao.GetConfiguracao();

            _cnpj = _configuracao.Empresa.Cnpj;
            _xmlEsperado = "xml-esperado-nao-encerrados.xml";

            var configuracoes = new ConfiguracaoUtilsDao(_configuracao);
            configuracoes.setCongiguracoes();
        }

        public void Dispose()
        {
            _consultaMdFeNaoEnc = new MDFeCosMDFeNaoEnc();
        }
        #endregion

        #region Testes para a classe ExtMDFeCosMDFeNaoEnct

        [Fact]
        public void Deve_Testar_A_Criacao_De_Uma_Requisicao_Para_Consulta_Nao_Encerrados_Com_Parametros()
        {
            //Arrange
            _consultaMdFeNaoEnc = new MDFeCosMDFeNaoEnc()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                CNPJ = _cnpj
            };

            //Act
            var xmlGerado = _consultaMdFeNaoEnc.CriaRequestWs();
            
            //Assert
            Assert.NotNull(xmlGerado);
            Assert.IsType<XmlDocument>(xmlGerado);
        }
        
        [Fact]
        public void Deve_Testar_A_Requisicao_Nao_Encerradas_Criada_Com_O_Xml_Esperado()
        {
            //Arrange
            _consultaMdFeNaoEnc = new MDFeCosMDFeNaoEnc()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                CNPJ = _cnpj
            };

            var repositorioDao = new RepositorioDaoFalso();

            //Act
            var xmlGerado = _consultaMdFeNaoEnc.CriaRequestWs();
            var xmlEsperado = repositorioDao.GetXmlEsperado(_xmlEsperado);

            //Assert
            Assert.NotNull(xmlEsperado);
            Assert.NotNull(xmlGerado);
            Assert.Equal(xmlEsperado.InnerXml, xmlGerado.InnerXml);
        }

        [Fact]
        public void Deve_Testar_A_Criacao_De_Uma_Requisicao_Para_Consulta_Nao_Encerrados_Sem_Parametros()
        {
            //Arrange
            _consultaMdFeNaoEnc = new MDFeCosMDFeNaoEnc();

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => _consultaMdFeNaoEnc.CriaRequestWs());

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Testar_A_Criacao_De_Uma_Requisicao_Para_Consulta_Nao_Encerrados_Sem_Versao()
        {
            //Arrange
            _consultaMdFeNaoEnc = new MDFeCosMDFeNaoEnc()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                CNPJ = _cnpj
            };

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => _consultaMdFeNaoEnc.CriaRequestWs());

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Testar_A_Criacao_De_Uma_Requisicao_Para_Consulta_Nao_Encerrados_Sem_Ambiente()
        {
            //Arrange
            _consultaMdFeNaoEnc = new MDFeCosMDFeNaoEnc()
            {
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                CNPJ = _cnpj
            };

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => _consultaMdFeNaoEnc.CriaRequestWs());

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Testar_A_Criacao_De_Uma_Requisicao_Para_Consulta_Nao_Encerrados_Sem_Ambiente_E_Versao()
        {
            //Arrange
            _consultaMdFeNaoEnc = new MDFeCosMDFeNaoEnc()
            {
                CNPJ = _cnpj
            };

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => _consultaMdFeNaoEnc.CriaRequestWs());

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Testar_A_Funcao_Nao_Encerrados_Para_Salvar_Xml_Localmente_Com_Parametros_Validos()
        {
            //Arrange
            _consultaMdFeNaoEnc = new MDFeCosMDFeNaoEnc()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                CNPJ = _cnpj
            };

            //Act
            var result = Record.Exception(() => _consultaMdFeNaoEnc.SalvarXmlEmDisco());

            //Assert
            Assert.Null(result);
        }

        #endregion
    }
}
