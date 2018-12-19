using System;
using System.Collections.Generic;
using System.Text;
using DFe.Utils;
using NUnit.Framework;
using SMDFe.Classes.Extencoes;
using SMDFe.Classes.Informacoes;
using SMDFe.Classes.Informacoes.Evento.CorpoEvento;
using SMDFe.Tests.Dao;
using SMDFe.Tests.Entidades;

namespace SMDFe.Tests.ClassesTests
{
    [TestFixture]
    public class ExtMDFeTest
    {
        #region Variáveis

        private Configuracao _configuracao;
        private MDFe _mdfe;

        #endregion

        #region Setup
        [SetUp]
        public void CriarConfiguração()
        {
            var configuracaoDao = new ConfiguracaoDao();
            _configuracao = configuracaoDao.GetConfiguracao();

            var configuracaoCertificado = new ConfiguracaoCertificado
            {
                Senha = _configuracao.CertificadoDigital.Senha,
                Arquivo = _configuracao.CertificadoDigital.CaminhoArquivo,
                ManterDadosEmCache = _configuracao.CertificadoDigital.ManterEmCache,
                Serial = _configuracao.CertificadoDigital.NumeroDeSerie
            };

            Utils.Configuracoes.MDFeConfiguracao.Instancia.ConfiguracaoCertificado = configuracaoCertificado;

            Utils.Configuracoes.MDFeConfiguracao.Instancia.CaminhoSchemas = _configuracao.ConfigWebService.CaminhoSchemas;
            Utils.Configuracoes.MDFeConfiguracao.Instancia.CaminhoSalvarXml = _configuracao.DiretorioSalvarXml;
            Utils.Configuracoes.MDFeConfiguracao.Instancia.IsSalvarXml = _configuracao.IsSalvarXml;

            Utils.Configuracoes.MDFeConfiguracao.Instancia.VersaoWebService.VersaoLayout = _configuracao.ConfigWebService.VersaoLayout;

            Utils.Configuracoes.MDFeConfiguracao.Instancia.VersaoWebService.TipoAmbiente = _configuracao.ConfigWebService.Ambiente;
            Utils.Configuracoes.MDFeConfiguracao.Instancia.VersaoWebService.UfEmitente = _configuracao.ConfigWebService.UfEmitente;
            Utils.Configuracoes.MDFeConfiguracao.Instancia.VersaoWebService.TimeOut = _configuracao.ConfigWebService.TimeOut;


        }
        #endregion

        #region Testes para a Classe ExtMDFe

        [Test]
        public void Testa_A_Assinatura_MDFe_Com_Parametros_Validos()
        {
            //Arrange
            var geradorMdfe = new MDFeEletronicaFalsa(_configuracao.Empresa);
            _mdfe = geradorMdfe.GetMdfe();

            //Act
            _mdfe.Assina();

            //Assert
            Assert.IsNotNull(_mdfe.Signature);
        }

        [Test]
        public void Testa_A_Assinatura_MDFe_Nula()
        {
            //Arrange
            _mdfe = new MDFe();

            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => _mdfe.Assina());

            //Assert
            Assert.IsInstanceOf<InvalidOperationException>(exception);
        }

        [Test]
        public void Deve_Recusar_A_Assinatura_Com_Alguns_Parametros_Invalidos()
        {
            //Arrange
            var geradorMdfe = new MDFeEletronicaFalsa(_configuracao.Empresa);
            _mdfe = geradorMdfe.GetMdfe();

            _mdfe.InfMDFe.Ide.Mod = 0;
            _mdfe.InfMDFe.Ide.TpEmis = 0;
            _mdfe.InfMDFe.Ide.CMDF = 0;
            _mdfe.InfMDFe.Emit.CNPJ = null;

            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => _mdfe.Assina());

            //Assert
            Assert.IsInstanceOf<InvalidOperationException>(exception);

        }

        [Test]
        public void Deve_Recusar_A_Assinatura_Por_Falta_do_Certificado_Digital()
        {
            //Arrange
            var geradorMdfe = new MDFeEletronicaFalsa(_configuracao.Empresa);
            _mdfe = geradorMdfe.GetMdfe();

            Utils.Configuracoes.MDFeConfiguracao.Instancia.ConfiguracaoCertificado = null;

            //Act
            var exception = Assert.Throws<NullReferenceException>(() => _mdfe.Assina());

            //Assert
            Assert.IsInstanceOf<NullReferenceException>(exception);
        }

        #endregion
    }
}
