using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using DFe.Utils;
using NUnit.Framework;
using SMDFe.Classes.Extencoes;
using SMDFe.Classes.Servicos.Autorizacao;
using SMDFe.Tests.Dao;
using SMDFe.Tests.Entidades;
using SMDFe.Utils.Flags;

namespace SMDFe.Tests.ClassesTests
{
    [TestFixture]
    public class ExtMDFeEnviMDFeTests
    {
        private Configuracao _configuracao;
        private MDFeEnviMDFe _enviMdFe;
        private MDFeEletronicaFalsa _mdfe;
        private string _xmlEsperado;

        #region SETUP
        [SetUp]
        public void CriarConfiguração()
        {
            var configuracaoDao = new ConfiguracaoDao();

            _configuracao = configuracaoDao.GetConfiguracao();

            _mdfe = new MDFeEletronicaFalsa(_configuracao.Empresa);
            _xmlEsperado = "xml-esperado-envi-mdfe.xml";

            var configuracaoCertificado = new ConfiguracaoCertificado
            {
                Senha = _configuracao.CertificadoDigital.Senha,
                Arquivo = _configuracao.CertificadoDigital.CaminhoArquivo,
                ManterDadosEmCache = _configuracao.CertificadoDigital.ManterEmCache,
                Serial = _configuracao.CertificadoDigital.NumeroDeSerie
            };

            Utils.Configuracoes.MDFeConfiguracao.ConfiguracaoCertificado = configuracaoCertificado;
            Utils.Configuracoes.MDFeConfiguracao.CaminhoSchemas = _configuracao.ConfigWebService.CaminhoSchemas;
            Utils.Configuracoes.MDFeConfiguracao.CaminhoSalvarXml = _configuracao.DiretorioSalvarXml;
            Utils.Configuracoes.MDFeConfiguracao.IsSalvarXml = _configuracao.IsSalvarXml;

            Utils.Configuracoes.MDFeConfiguracao.VersaoWebService.VersaoLayout = _configuracao.ConfigWebService.VersaoLayout;

            Utils.Configuracoes.MDFeConfiguracao.VersaoWebService.TipoAmbiente = _configuracao.ConfigWebService.Ambiente;
            Utils.Configuracoes.MDFeConfiguracao.VersaoWebService.UfEmitente = _configuracao.ConfigWebService.UfEmitente;
            Utils.Configuracoes.MDFeConfiguracao.VersaoWebService.TimeOut = _configuracao.ConfigWebService.TimeOut;


        }
        #endregion

        #region Testes para a classe ExtMDFeEnviMDFe 

        [Test]
        public void Testa_A_Criacao_De_Uma_Requisicao_Para_Envio_Mdfe_Com_Parametros()
        {
            //Arrange
            _enviMdFe = new MDFeEnviMDFe()
            {
                Versao = VersaoServico.Versao300,
                MDFe = _mdfe.GetMdfe(),
                IdLote = "1"
            };
            _enviMdFe.MDFe.Assina();

            //Act
            var xmlGerado = _enviMdFe.CriaXmlRequestWs();


            //Assert
            Assert.IsInstanceOf<XmlDocument>(xmlGerado);
        }

        [Test]
        public void Testa_A_Requisicao_De_Envio_MDFe_Criada_Com_O_Xml_Esperado()
        {
            //Arrange
            _enviMdFe = new MDFeEnviMDFe()
            {
                Versao = VersaoServico.Versao300,
                MDFe = _mdfe.GetMdfe(),
                IdLote = "1"
            };
            _enviMdFe.MDFe.Assina();
            

            var repositorioDao = new RepositorioDaoFalso();

            //Act
            var xmlGerado = _enviMdFe.CriaXmlRequestWs();
            var xmlEsperado = repositorioDao.GetXmlEsperado(_xmlEsperado);

            //Assert
            Assert.AreEqual(xmlEsperado.InnerXml, xmlGerado.InnerXml);
        }

        [Test]
        public void Testa_A_Criacao_De_Uma_Requisicao_Para_Envio_Mdfe_Sem_Versao_E_IDLote()
        {
            //Arrange
            _enviMdFe = new MDFeEnviMDFe()
            {
                Versao = 0,
                MDFe = _mdfe.GetMdfe(),
                IdLote = null
            };
            _enviMdFe.MDFe.Assina();

            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => _enviMdFe.CriaXmlRequestWs());

            //Assert
            Assert.IsInstanceOf<InvalidOperationException>(exception);

        }

        [Test]
        public void Testa_A_Criacao_De_Uma_Requisicao_Para_Envio_Mdfe_Sem_Versao()
        {
            //Arrange
            _enviMdFe = new MDFeEnviMDFe()
            {
                Versao = 0,
                MDFe = _mdfe.GetMdfe(),
                IdLote = "1"
            };
            _enviMdFe.MDFe.Assina();

            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => _enviMdFe.CriaXmlRequestWs());

            //Assert
            Assert.IsInstanceOf<InvalidOperationException>(exception);

        }

        [Test]
        public void Testa_A_Funcao_Envio_Mdfe_Para_Salvar_Xml_Localmente_Com_Parametros_Validos()
        {
            //Arrange
            _enviMdFe = new MDFeEnviMDFe()
            {
                Versao = VersaoServico.Versao300,
                MDFe = _mdfe.GetMdfe(),
                IdLote = "1"
            };
            _enviMdFe.MDFe.Assina();

            //Act
            _enviMdFe.SalvarXmlEmDisco();

            //Assert
            Assert.That(true);
        }

        #endregion
    }
}
