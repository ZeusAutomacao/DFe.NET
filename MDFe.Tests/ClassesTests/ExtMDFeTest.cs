using System;
using MDFe.Classes.Extensoes;
using MDFe.Tests.Dao;
using MDFe.Tests.Entidades;
using Xunit;

namespace MDFe.Tests.ClassesTests
{
    
    public class ExtMDFeTest: IDisposable
    {
        #region Variáveis

        private Configuracao _configuracao;
        private Classes.Informacoes.MDFe _mdfe;
        private MDFeEletronicaFalsa _RepositorioFalsoMdfe;

        #endregion

        #region Setup
        public ExtMDFeTest()
        {
            var configuracaoDao = new ConfiguracaoDao();
            _configuracao = configuracaoDao.GetConfiguracao();
            _RepositorioFalsoMdfe = new MDFeEletronicaFalsa(_configuracao.Empresa);

            _mdfe = _RepositorioFalsoMdfe.GetMdfe();

            var configcertificado = new CertificadoDao().getConfiguracaoCertificado();
            
            var configuracoes = new ConfiguracaoUtilsDao(_configuracao, configcertificado);
            configuracoes.setCongiguracoes();
        }

        public void Dispose()
        {
            _RepositorioFalsoMdfe = new MDFeEletronicaFalsa(_configuracao.Empresa);
            _mdfe = _RepositorioFalsoMdfe.GetMdfe();
        }
        #endregion

        #region Testes para a Classe ExtMDFe

        [Fact]
        public void Deve_Criar_Assinatura_MDFe_Com_Parametros_Validos()
        {
            //Arrange
            if (_mdfe != null) Dispose();

            //Act
            _mdfe.Assina();

            //Assert
            Assert.NotNull(_mdfe.Signature);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Para_Assinatura_De_MDFe_Nula()
        {
            //Arrange
            _mdfe = new Classes.Informacoes.MDFe();

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => _mdfe.Assina());

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Para_Assinatura_Com_Alguns_Parametros_Invalidos()
        {
            //Arrange
            if (_mdfe != null) Dispose();

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
        public void Deve_Gerar_Uma_Excecao_Para_Assinatura_Por_Falta_do_Certificado_Digital()
        {
            //Arrange
            if (_mdfe != null) Dispose();

            Utils.Configuracoes.MDFeConfiguracao.Instancia.ConfiguracaoCertificado = null;

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => _mdfe.Assina());

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        #endregion
    }
}
