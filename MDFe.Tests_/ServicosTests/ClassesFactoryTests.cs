using System;
using MDFe.Classes.Extensoes;
using MDFe.Servicos.Factory;
using MDFe.Tests.Dao;
using MDFe.Tests.Entidades;
using Xunit;

namespace MDFe.Tests.ServicosTests
{

    public class ClassesFactoryTests : IDisposable
    {
        #region Variáveis
        private Configuracao _configuracao;
        private MDFeEletronicaFalsa _RepositorioFalsoMdfe;
        private Classes.Informacoes.MDFe _mdfe;
        private readonly string _mensagemNaoEnce;
        private readonly string _mensagemConsulta;
        private readonly string _mensagemCancelamento;
        private readonly string _mensagemEncerramento;
        private readonly string _mensagemIncluirCondutor;
        private readonly string _mensagemStatus;
        private readonly string _protocolo;
        private readonly string _justificativa;
        private readonly long _lote;


        #endregion

        #region Setup

        public ClassesFactoryTests()
        {
            var configuracaoDao = new ConfiguracaoDao();
            _configuracao = configuracaoDao.GetConfiguracao();

            _mensagemNaoEnce = "CONSULTAR NÃO ENCERRADOS";
            _mensagemConsulta = "CONSULTAR";
            _mensagemCancelamento = "Cancelamento";
            _mensagemEncerramento = "Encerramento";
            _mensagemIncluirCondutor = "Inclusao Condutor";
            _mensagemStatus = "STATUS";
            _protocolo = "000000000000000";
            _justificativa = "ERRO NA MATRIX";
            _lote = 1;

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

        #region Testes para a classe ClassesFactory

        [Fact]
        public void Deve_Criar_Uma_Requisicao_Da_Consulta_CriarConsMDFeNaoEnc_Nao_Nula()
        {
            //Arrange
            var cnpj = _configuracao.Empresa.Cnpj;

            //Act
            var consultaNaoEncerrados = ClassesFactory.CriarConsMDFeNaoEnc(cnpj);

            //Assert
            Assert.NotNull(consultaNaoEncerrados);
        }

        [Fact]
        public void Deve_Verificar_Os_Parametros_Passados_Na_Criacao_Da_Consulta_CriarConsMDFeNaoEnc()
        {
            //Arrange
            var cnpj = _configuracao.Empresa.Cnpj;
            var ambiente = _configuracao.ConfigWebService.Ambiente;
            var versao = _configuracao.ConfigWebService.VersaoLayout;

            //Act
            var consultaNaoEncerrados = ClassesFactory.CriarConsMDFeNaoEnc(cnpj);

            //Assert
            Assert.Equal(ambiente, consultaNaoEncerrados.TpAmb);
            Assert.Equal(versao, consultaNaoEncerrados.Versao);
            Assert.Equal(cnpj, consultaNaoEncerrados.CNPJ);
            Assert.Equal(_mensagemNaoEnce, consultaNaoEncerrados.XServ);
        }

        [Fact]
        public void Deve_Criar_Uma_Requisicao_Da_Consulta_CriarConsSitMDFe_Nao_Nula()
        {
            //Arrange
            var chave = _mdfe.InfMDFe.Id;

            //Act
            var consulta = ClassesFactory.CriarConsSitMDFe(chave);

            //Assert
            Assert.NotNull(consulta);
        }

        [Fact]
        public void Deve_Verificar_Os_Parametros_Passados_Na_Criacao_Da_Consulta_CriarConsSitMDFe()
        {
            //Arrange
            var ambiente = _configuracao.ConfigWebService.Ambiente;
            var versao = _configuracao.ConfigWebService.VersaoLayout;
            var chave = _mdfe.InfMDFe.Id;

            //Act
            var consulta = ClassesFactory.CriarConsSitMDFe(chave);

            //Assert
            Assert.Equal(ambiente, consulta.TpAmb);
            Assert.Equal(versao, consulta.Versao);
            Assert.Equal(chave, consulta.ChMDFe);
            Assert.Equal(_mensagemConsulta, consulta.XServ);
        }

        [Fact]
        public void Deve_Criar_Uma_Requisicao_Da_Consulta_CriaEvCancMDFe_Nao_Nula()
        {
            //Arrange

            //Act
            var cancelamento = ClassesFactory.CriaEvCancMDFe(_protocolo, _justificativa);

            //Assert
            Assert.NotNull(cancelamento);
        }

        [Fact]
        public void Deve_Verificar_Os_Parametros_Passados_Na_Criacao_Da_Consulta_CriaEvCancMDFe()
        {
            //Arrange

            //Act
            var cancelamento = ClassesFactory.CriaEvCancMDFe(_protocolo, _justificativa);

            //Assert
            Assert.Equal(_protocolo, cancelamento.NProt);
            Assert.Equal(_justificativa, cancelamento.XJust);
            Assert.Equal(_mensagemCancelamento, cancelamento.DescEvento);
        }

        [Fact]
        public void Deve_Criar_Uma_Requisicao_Da_Consulta_Pelo_Metodo_CriaEvEncMDFe_Nao_Nula()
        {
            //Arrange

            //Act
            var encerramento = ClassesFactory.CriaEvEncMDFe(_mdfe.UFEmitente(), _mdfe.CodigoIbgeMunicipioEmitente(), _protocolo);

            //Assert
            Assert.NotNull(encerramento);
        }

        [Fact]
        public void Deve_Verificar_Os_Parametros_Passados_Na_Criacao_Da_Consulta_Pelo_Metodo_CriaEvEncMDFe()
        {
            //Arrange
            var cuf = _mdfe.UFEmitente();
            var cmun = _mdfe.CodigoIbgeMunicipioEmitente();

            //Act
            var encerramento = ClassesFactory.CriaEvEncMDFe(cuf, cmun, _protocolo);

            //Assert
            Assert.Equal(cuf, encerramento.CUF);
            Assert.Equal(cmun, encerramento.CMun);
            Assert.Equal(_protocolo, encerramento.NProt);
            Assert.Equal(_mensagemEncerramento, encerramento.DescEvento);
        }

        [Fact]
        public void Deve_Criar_Uma_Requisicao_Da_Consulta_CriaEvIncCondutorMDFe_Nao_Nula()
        {
            //Arrange
            var nome = "NINGUEM";
            var cpf = "00000000000";

            //Act
            var incluirCondutor = ClassesFactory.CriaEvIncCondutorMDFe(nome, cpf);

            //Assert
            Assert.NotNull(incluirCondutor);
        }

        [Fact]
        public void Deve_Verificar_Os_Parametros_Passados_Na_Criacao_Da_Consulta_CriaEvIncCondutorMDFe()
        {
            //Arrange
            var nome = "NINGUEM";
            var cpf = "00000000000";

            //Act
            var incluirCondutor = ClassesFactory.CriaEvIncCondutorMDFe(nome, cpf);

            //Assert
            Assert.Equal(nome, incluirCondutor.Condutor.XNome);
            Assert.Equal(cpf, incluirCondutor.Condutor.CPF);
            Assert.Equal(_mensagemIncluirCondutor, incluirCondutor.DescEvento);
        }

        [Fact]
        public void Deve_Criar_Uma_Requisicao_Da_Consulta_CriaEnviMDFe_Nao_Nula()
        {
            //Arrange

            //Act
            var enviMdfe = ClassesFactory.CriaEnviMDFe(_lote, _mdfe);

            //Assert
            Assert.NotNull(enviMdfe);
        }

        [Fact]
        public void Deve_Verificar_Os_Parametros_Passados_Na_Criacao_Da_Consulta_CriaEnviMDFe()
        {
            //Arrange
            var versao = _configuracao.ConfigWebService.VersaoLayout;

            //Act
            var enviMdfe = ClassesFactory.CriaEnviMDFe(_lote, _mdfe);

            //Assert
            Assert.Equal(versao, enviMdfe.Versao);
            Assert.Equal(_mdfe, enviMdfe.MDFe);
            Assert.Equal(_lote.ToString(), enviMdfe.IdLote);
        }

        [Fact]
        public void Deve_Criar_Uma_Requisicao_Da_Consulta_CriaConsReciMDFe_Nao_Nula()
        {
            //Arrange

            //Act
            var consultaRecibo = ClassesFactory.CriaConsReciMDFe(_protocolo);

            //Assert
            Assert.NotNull(consultaRecibo);
        }

        [Fact]
        public void Deve_Verificar_Os_Parametros_Passados_Na_Criacao_Da_Consulta_CriaConsReciMDFe()
        {
            //Arrange
            var ambiente = _configuracao.ConfigWebService.Ambiente;
            var versao = _configuracao.ConfigWebService.VersaoLayout;

            //Act
            var consultaRecibo = ClassesFactory.CriaConsReciMDFe(_protocolo);

            //Assert
            Assert.Equal(ambiente, consultaRecibo.TpAmb);
            Assert.Equal(versao, consultaRecibo.Versao);
            Assert.Equal(_protocolo, consultaRecibo.NRec);
        }

        [Fact]
        public void Deve_Criar_Uma_Requisicao_Da_Consulta_CriaConsStatServMDFe_Nao_Nula()
        {
            //Arrange

            //Act
            var consultaStatus = ClassesFactory.CriaConsStatServMDFe();

            //Assert
            Assert.NotNull(consultaStatus);
        }

        [Fact]
        public void Deve_Verificar_Os_Parametros_Passados_Na_Criacao_Da_Consulta_CriaConsStatServMDFe()
        {
            //Arrange
            var ambiente = _configuracao.ConfigWebService.Ambiente;
            var versao = _configuracao.ConfigWebService.VersaoLayout;

            //Act
            var consultaStatus = ClassesFactory.CriaConsStatServMDFe();

            //Assert
            Assert.Equal(ambiente, consultaStatus.TpAmb);
            Assert.Equal(versao, consultaStatus.Versao);
            Assert.Equal(_mensagemStatus, consultaStatus.XServ);
        }

        #endregion
    }
}
