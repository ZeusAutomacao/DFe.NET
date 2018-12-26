using MDFe.Servicos.Factory;
using MDFe.Tests.Dao;
using MDFe.Tests.Entidades;
using MDFe.Wsdl.Gerado.MDFeConsultaNaoEncerrados;
using MDFe.Wsdl.Gerado.MDFeConsultaProtoloco;
using MDFe.Wsdl.Gerado.MDFeEventos;
using MDFe.Wsdl.Gerado.MDFeRecepcao;
using MDFe.Wsdl.Gerado.MDFeRetRecepcao;
using MDFe.Wsdl.Gerado.MDFeStatusServico;
using Xunit;

namespace MDFe.Tests.ServicosTests
{
    
    public class WsdlFactoryTests
    {
        #region Variáveis

        private readonly Configuracao _configuracao;

        #endregion

        #region Setup

        
        public WsdlFactoryTests()
        {
            var configuracaoDao = new ConfiguracaoDao();
            _configuracao = configuracaoDao.GetConfiguracao();

            var configcertificado = new CertificadoDao().getConfiguracaoCertificado();

            var configuracoes = new ConfiguracaoUtilsDao(_configuracao, configcertificado);
            configuracoes.setCongiguracoes();
        }

        #endregion

        #region Testes para a classe WsdlFactory

        [Fact]
        public void Deve_Criar_Um_Objeto_Do_Tipo_CriaWsdlMDFeConsNaoEnc()
        {
            //Arrange

            //Act
            var consultaWsdl = WsdlFactory.CriaWsdlMDFeConsNaoEnc();

            //Assert
            Assert.IsType<MDFeConsNaoEnc>(consultaWsdl);
        }

        [Fact]
        public void Deve_Criar_Um_Objeto_Do_Tipo_CriaWsdlMDFeConsNaoEnc_Nao_Nulo()
        {
            //Arrange

            //Act
            var consultaWsdl = WsdlFactory.CriaWsdlMDFeConsNaoEnc();

            //Assert
            Assert.NotNull(consultaWsdl);
        }

        [Fact]
        public void Deve_Criar_Um_Objeto_Do_Tipo_CriaWsdlMDFeConsulta()
        {
            //Arrange

            //Act
            var consultaWsdl = WsdlFactory.CriaWsdlMDFeConsulta();

            //Assert
            Assert.IsType<MDFeConsulta>(consultaWsdl);
        }

        [Fact]
        public void Deve_Criar_Um_Objeto_Do_Tipo_CriaWsdlMDFeConsulta_Nao_Nulo()
        {
            //Arrange

            //Act
            var consultaWsdl = WsdlFactory.CriaWsdlMDFeConsulta();

            //Assert
            Assert.NotNull(consultaWsdl);
        }

        [Fact]
        public void Deve_Criar_Um_Objeto_Do_Tipo_CriaWsdlMDFeRecepcaoEvento()
        {
            //Arrange

            //Act
            var consultaWsdl = WsdlFactory.CriaWsdlMDFeRecepcaoEvento();

            //Assert
            Assert.IsType<MDFeRecepcaoEvento>(consultaWsdl);
        }

        [Fact]
        public void Deve_Criar_Um_Objeto_Do_Tipo_CriaWsdlMDFeRecepcaoEvento_Nao_Nulo()
        {
            //Arrange

            //Act
            var consultaWsdl = WsdlFactory.CriaWsdlMDFeRecepcaoEvento();

            //Assert
            Assert.NotNull(consultaWsdl);
        }

        [Fact]
        public void Deve_Criar_Um_Objeto_Do_Tipo_CriaWsdlMDFeRecepcao()
        {
            //Arrange

            //Act
            var consultaWsdl = WsdlFactory.CriaWsdlMDFeRecepcao();

            //Assert
            Assert.IsType<MDFeRecepcao>(consultaWsdl);
        }

        [Fact]
        public void Deve_Criar_Um_Objeto_Do_Tipo_CriaWsdlMDFeRecepcao_Nao_Nulo()
        {
            //Arrange

            //Act
            var consultaWsdl = WsdlFactory.CriaWsdlMDFeRecepcao();

            //Assert
            Assert.NotNull(consultaWsdl);
        }

        [Fact]
        public void Deve_Criar_Um_Objeto_Do_Tipo_CriaWsdlMDFeRetRecepcao()
        {
            //Arrange

            //Act
            var consultaWsdl = WsdlFactory.CriaWsdlMDFeRetRecepcao();

            //Assert
            Assert.IsType<MDFeRetRecepcao>(consultaWsdl);
        }

        [Fact]
        public void Deve_Criar_Um_Objeto_Do_Tipo_CriaWsdlMDFeRetRecepcao_Nao_Nulo()
        {
            //Arrange

            //Act
            var consultaWsdl = WsdlFactory.CriaWsdlMDFeRetRecepcao();

            //Assert
            Assert.NotNull(consultaWsdl);
        }

        [Fact]
        public void Deve_Criar_Um_Objeto_Do_Tipo_CriaWsdlMDFeStatusServico()
        {
            //Arrange

            //Act
            var consultaWsdl = WsdlFactory.CriaWsdlMDFeStatusServico();

            //Assert
            Assert.IsType<MDFeStatusServico>(consultaWsdl);
        }

        [Fact]
        public void Deve_Criar_Um_Objeto_Do_Tipo_CriaWsdlMDFeStatusServico_Nao_Nulo()
        {
            //Arrange

            //Act
            var consultaWsdl = WsdlFactory.CriaWsdlMDFeStatusServico();

            //Assert
            Assert.NotNull(consultaWsdl);
        }

        #endregion
    }
}
