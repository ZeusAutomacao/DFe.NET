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

        #region SETUP
        [SetUp]
        public void CriarConfiguração()
        {
            var configuracaoDao = new ConfiguracaoDao();
            _configuracao = configuracaoDao.GetConfiguracao();

            Utils.Configuracoes.MDFeConfiguracao.CaminhoSchemas = _configuracao.ConfigWebService.CaminhoSchemas;
            Utils.Configuracoes.MDFeConfiguracao.CaminhoSalvarXml = _configuracao.DiretorioSalvarXml;
            Utils.Configuracoes.MDFeConfiguracao.IsSalvarXml = _configuracao.IsSalvarXml;

            Utils.Configuracoes.MDFeConfiguracao.VersaoWebService.VersaoLayout = _configuracao.ConfigWebService.VersaoLayout;

            Utils.Configuracoes.MDFeConfiguracao.VersaoWebService.TipoAmbiente = _configuracao.ConfigWebService.Ambiente;
            Utils.Configuracoes.MDFeConfiguracao.VersaoWebService.UfEmitente = _configuracao.ConfigWebService.UfEmitente;
            Utils.Configuracoes.MDFeConfiguracao.VersaoWebService.TimeOut = _configuracao.ConfigWebService.TimeOut;


        }
        #endregion

        #region Testes para a classe ExtMDFeConsReciMDFe 

        [Test]
        public void Testa_A_Criacao_De_Uma_Requisicao_Para_Consulta_Por_Recibo_Com_Parametros()
        {
            //Arrange
            var consulta = new MDFeConsReciMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                NRec = "000000000000000"
            };

            //Act
            var xmlGerado = consulta.CriaRequestWs();


            //Assert
            Assert.IsInstanceOf<XmlDocument>(xmlGerado);
        }

        [Test]
        public void Testa_A_Criacao_De_Uma_Requisicao_Para_Consulta_Por_Recibo_Sem_Parametros()
        {
            //Arrange
            var consulta = new MDFeConsReciMDFe();

            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => consulta.CriaRequestWs());

            //Assert
            Assert.IsTrue(exception.Message.Contains("error"));

        }

        [Test]
        public void Testa_A_Criacao_De_Uma_Requisicao_Para_Consulta_Por_Recibo_Sem_Versao()
        {
            //Arrange
            var consulta = new MDFeConsReciMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                NRec = "000000000000000"
            };
            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => consulta.CriaRequestWs());

            //Assert
            Assert.IsTrue(exception.Message.Contains("error"));

        }

        [Test]
        public void Testa_A_Criacao_De_Uma_Requisicao_Para_Consulta_Por_Recibo_Sem_Ambiente()
        {
            //Arrange
            var consulta = new MDFeConsReciMDFe()
            {
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                NRec = "000000000000000"
            };
            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => consulta.CriaRequestWs());

            //Assert
            Assert.IsTrue(exception.Message.Contains("error"));

        }

        [Test]
        public void Testa_A_Criacao_De_Uma_Requisicao_Para_Consulta_Por_Recibo_Sem_Ambiente_E_Versao()
        {
            //Arrange
            var consulta = new MDFeConsReciMDFe()
            {
                NRec = "000000000000000"
            };
            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => consulta.CriaRequestWs());

            //Assert
            Assert.IsTrue(exception.Message.Contains("error"));

        }

        [Test]
        public void Testa_A_Funcao_Para_Salvar_Xml_Localmente_Com_Parametros_Validos()
        {
            //Arrange
            var consulta = new MDFeConsReciMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                NRec = "000000000000000"
            };

            //Act
            consulta.SalvarXmlEmDisco();

            //Assert
            Assert.That(true);
        }

        #endregion
    }
}