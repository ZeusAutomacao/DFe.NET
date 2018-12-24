using System;
using System.Xml;
using MDFe.Classes.Extensoes;
using MDFe.Classes.Informacoes.ConsultaNaoEncerrados;
using MDFe.Tests.Dao;
using MDFe.Tests.Entidades;
using Xunit;

namespace MDFe.Tests.ClassesTests
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
        public void Deve_Criar_Uma_Requisicao_Para_Consulta_Nao_Encerrados_Com_Parametros_Validos()
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
            Assert.IsType<XmlDocument>(xmlGerado);
        }


        [Fact]
        public void Deve_Criar_Uma_Requisicao_Para_Consulta_Nao_Encerrados_Nao_Nula()
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
        }

        [Fact]
        public void Deve_Validar_Requisicao_Nao_Encerradas_Criada_Com_O_Xml_Esperado()
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
            Assert.Equal(xmlEsperado.InnerXml, xmlGerado.InnerXml);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Para_Criacao_De_Consulta_Nao_Encerrados_Sem_Parametros()
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
        public void Deve_Gerar_Uma_Excecao_Para_Criacao_De_Consulta_Nao_Encerrados_Sem_Versao()
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
        public void Deve_Gerar_Uma_Excecao_Para_Criacao_De_Consulta_Nao_Encerrados_Sem_Ambiente()
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
        public void Deve_Gerar_Uma_Excecao_Para_Criacao_De_Consulta_Nao_Encerrados_Sem_Ambiente_E_Versao()
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
        public void Deve_Salvar_Xml_Localmente_Para_Consulta_Nao_Encerrados()
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
