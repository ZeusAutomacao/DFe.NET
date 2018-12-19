using System;
using DFe.Utils;
using Xunit;
using SMDFe.Classes.Extencoes;
using SMDFe.Classes.Informacoes;
using SMDFe.Tests.Dao;
using SMDFe.Tests.Entidades;

namespace SMDFe.Tests.ClassesTests
{
    
    public class ExtMDFeTest: IDisposable
    {
        #region Variáveis

        private Configuracao _configuracao;
        private MDFe _mdfe;

        #endregion

        #region Setup
        public ExtMDFeTest()
        {
            var configuracaoDao = new ConfiguracaoDao();
            _configuracao = configuracaoDao.GetConfiguracao();

            var configcertificado = new CertificadoDao().getConfiguracaoCertificado();
            
            var configuracoes = new ConfiguracaoUtilsDao(_configuracao, configcertificado);
            configuracoes.setCongiguracoes();
        }

        public void Dispose()
        {
            _mdfe = new MDFe();
        }
        #endregion

        #region Testes para a Classe ExtMDFe

        [Fact]
        public void Deve_Testar_A_Assinatura_MDFe_Com_Parametros_Validos()
        {
            //Arrange
            var geradorMdfe = new MDFeEletronicaFalsa(_configuracao.Empresa);
            var mdfe = geradorMdfe.GetMdfe();

            //Act
            mdfe.Assina();

            //Assert
            Assert.NotNull(mdfe.Signature);
        }

        [Fact]
        public void Deve_Testar_A_Assinatura_MDFe_Nula()
        {
            //Arrange
            _mdfe = new MDFe();

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => _mdfe.Assina());

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
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
            var exception = Assert.ThrowsAny<Exception>(() => _mdfe.Assina());

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Recusar_A_Assinatura_Por_Falta_do_Certificado_Digital()
        {
            //Arrange
            var geradorMdfe = new MDFeEletronicaFalsa(_configuracao.Empresa);
            _mdfe = geradorMdfe.GetMdfe();

            Utils.Configuracoes.MDFeConfiguracao.ConfiguracaoCertificado = null;

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => _mdfe.Assina());

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        #endregion
    }
}
