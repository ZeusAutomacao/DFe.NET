using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using NUnit.Framework;
using SMDFe.Classes.Extencoes;
using SMDFe.Classes.Informacoes.ConsultaNaoEncerrados;
using SMDFe.Tests.Dao;
using SMDFe.Tests.Entidades;

namespace SMDFe.Tests.ClassesTests
{
    [TestFixture]
    public class ExtMDFeCosMDFeNaoEnctTests
    {
        private Configuracao _configuracao;
        private MDFeCosMDFeNaoEnc _consultaMdFeNaoEnc;
        private string _cnpj;
        private string _xmlEsperado;


        #region SETUP
        [SetUp]
        public void CriarConfiguração()
        {
            var configuracaoDao = new ConfiguracaoDao();
            _configuracao = configuracaoDao.GetConfiguracao();

            _cnpj = _configuracao.Empresa.Cnpj;
            _xmlEsperado = "xml-esperado-nao-encerrados.xml";

            Utils.Configuracoes.MDFeConfiguracao.CaminhoSchemas = _configuracao.ConfigWebService.CaminhoSchemas;
            Utils.Configuracoes.MDFeConfiguracao.CaminhoSalvarXml = _configuracao.DiretorioSalvarXml;
            Utils.Configuracoes.MDFeConfiguracao.IsSalvarXml = _configuracao.IsSalvarXml;

            Utils.Configuracoes.MDFeConfiguracao.VersaoWebService.VersaoLayout = _configuracao.ConfigWebService.VersaoLayout;

            Utils.Configuracoes.MDFeConfiguracao.VersaoWebService.TipoAmbiente = _configuracao.ConfigWebService.Ambiente;
            Utils.Configuracoes.MDFeConfiguracao.VersaoWebService.UfEmitente = _configuracao.ConfigWebService.UfEmitente;
            Utils.Configuracoes.MDFeConfiguracao.VersaoWebService.TimeOut = _configuracao.ConfigWebService.TimeOut;


        }
        #endregion

        #region Testes para a classe ExtMDFeCosMDFeNaoEnct

        [Test]
        public void Testa_A_Criacao_De_Uma_Requisicao_Para_Consulta_Nao_Encerrados_Com_Parametros()
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
            Assert.IsInstanceOf<XmlDocument>(xmlGerado);
        }


        [Test]
        public void Testa_A_Requisicao_Nao_Encerradas_Criada_Com_O_Xml_Esperado()
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
            Assert.AreEqual(xmlEsperado.InnerXml, xmlGerado.InnerXml);
        }

        [Test]
        public void Testa_A_Criacao_De_Uma_Requisicao_Para_Consulta_Nao_Encerrados_Sem_Parametros()
        {
            //Arrange
            _consultaMdFeNaoEnc = new MDFeCosMDFeNaoEnc();

            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => _consultaMdFeNaoEnc.CriaRequestWs());

            //Assert
            Assert.IsInstanceOf<InvalidOperationException>(exception);

        }

        [Test]
        public void Testa_A_Criacao_De_Uma_Requisicao_Para_Consulta_Nao_Encerrados_Sem_Versao()
        {
            //Arrange
            _consultaMdFeNaoEnc = new MDFeCosMDFeNaoEnc()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                CNPJ = _cnpj
            };
            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => _consultaMdFeNaoEnc.CriaRequestWs());

            //Assert
            Assert.IsInstanceOf<InvalidOperationException>(exception);

        }

        [Test]
        public void Testa_A_Criacao_De_Uma_Requisicao_Para_Consulta_Nao_Encerrados_Sem_Ambiente()
        {
            //Arrange
            _consultaMdFeNaoEnc = new MDFeCosMDFeNaoEnc()
            {
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                CNPJ = _cnpj
            };
            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => _consultaMdFeNaoEnc.CriaRequestWs());

            //Assert
            Assert.IsInstanceOf<InvalidOperationException>(exception);

        }

        [Test]
        public void Testa_A_Criacao_De_Uma_Requisicao_Para_Consulta_Nao_Encerrados_Sem_Ambiente_E_Versao()
        {
            //Arrange
            _consultaMdFeNaoEnc = new MDFeCosMDFeNaoEnc()
            {
                CNPJ = _cnpj
            };
            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => _consultaMdFeNaoEnc.CriaRequestWs());

            //Assert
            Assert.IsInstanceOf<InvalidOperationException>(exception);

        }

        [Test]
        public void Testa_A_Funcao_Nao_Encerrados_Para_Salvar_Xml_Localmente_Com_Parametros_Validos()
        {
            //Arrange
            _consultaMdFeNaoEnc = new MDFeCosMDFeNaoEnc()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                CNPJ = _cnpj
            };

            //Act
            _consultaMdFeNaoEnc.SalvarXmlEmDisco();

            //Assert
            Assert.That(true);
        }

        #endregion
    }
}
