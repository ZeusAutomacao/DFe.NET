
using System;
using DFe.Utils;
using  Xunit;
using SMDFe.Classes.Extencoes;
using SMDFe.Classes.Informacoes;
using SMDFe.Servicos.Factory;
using SMDFe.Tests.Dao;
using SMDFe.Tests.Entidades;

namespace SMDFe.Tests.ServicosTests
{

    public class ClassesFactoryTests : IDisposable
    {
        #region Variáveis
        private Configuracao _configuracao;
        private MDFeEletronicaFalsa _RepositorioFalsoMdfe;
        private MDFe _mdfe;
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
            _mdfe = new MDFe();
            _mdfe = _RepositorioFalsoMdfe.GetMdfe();
        }

        #endregion

        #region Testes para a classe ClassesFactory

        [Fact]
        public void Deve_Testar_Os_Parametros_Passados_Na_Criacao_Da_Consulta_Pelo_Metodo_CriarConsMDFeNaoEnc()
        {
            //Arrange
            var cnpj = _configuracao.Empresa.Cnpj;
            var ambiente = _configuracao.ConfigWebService.Ambiente;
            var versao = _configuracao.ConfigWebService.VersaoLayout;

            //Act
            var consultaNaoEncerrados = ClassesFactory.CriarConsMDFeNaoEnc(cnpj);

            //Assert
            Assert.NotNull(consultaNaoEncerrados);
            Assert.Equal(ambiente, consultaNaoEncerrados.TpAmb);
            Assert.Equal(versao, consultaNaoEncerrados.Versao);
            Assert.Equal(cnpj, consultaNaoEncerrados.CNPJ);
            Assert.Equal(_mensagemNaoEnce, consultaNaoEncerrados.XServ);
        }

        [Fact]
        public void Deve_Testar_Os_Parametros_Passados_Na_Criacao_Da_Consulta_Pelo_Metodo_CriarConsSitMDFe()
        {
            //Arrange
            var ambiente = _configuracao.ConfigWebService.Ambiente;
            var versao = _configuracao.ConfigWebService.VersaoLayout;
            var chave = _mdfe.InfMDFe.Id;

            //Act
            var consulta = ClassesFactory.CriarConsSitMDFe(chave);

            //Assert
            Assert.NotNull(consulta);
            Assert.Equal(ambiente, consulta.TpAmb);
            Assert.Equal(versao, consulta.Versao);
            Assert.Equal(chave, consulta.ChMDFe);
            Assert.Equal(_mensagemConsulta, consulta.XServ);
        }

        [Fact]
        public void Deve_Testar_Os_Parametros_Passados_Na_Criacao_Da_Consulta_Pelo_Metodo_CriaEvCancMDFe()
        {
            //Arrange

            //Act
            var cancelamento = ClassesFactory.CriaEvCancMDFe(_protocolo, _justificativa);

            //Assert
            Assert.NotNull(cancelamento);
            Assert.Equal(_protocolo, cancelamento.NProt);
            Assert.Equal(_justificativa, cancelamento.XJust);
            Assert.Equal(_mensagemCancelamento, cancelamento.DescEvento);
        }

        [Fact]
        public void Deve_Testar_Os_Parametros_Passados_Na_Criacao_Da_Consulta_Pelo_Metodo_CriaEvEncMDFe()
        {
            //Arrange
            var cuf = _mdfe.UFEmitente();
            var cmun = _mdfe.CodigoIbgeMunicipioEmitente();

            //Act
            var encerramento = ClassesFactory.CriaEvEncMDFe(_mdfe, _protocolo);

            //Assert
            Assert.NotNull(encerramento);
            Assert.Equal(cuf, encerramento.CUF);
            Assert.Equal(cmun, encerramento.CMun);
            Assert.Equal(_protocolo, encerramento.NProt);
            Assert.Equal(_mensagemEncerramento, encerramento.DescEvento);
        }

        [Fact]
        public void Deve_Testar_Os_Parametros_Passados_Na_Criacao_Da_Consulta_Pelo_Metodo_CriaEvIncCondutorMDFe()
        {
            //Arrange
            var nome = "NINGUEM";
            var cpf = "00000000000";

            //Act
            var incluirCondutor = ClassesFactory.CriaEvIncCondutorMDFe(nome, cpf);

            //Assert
            Assert.NotNull(incluirCondutor);
            Assert.Equal(nome, incluirCondutor.Condutor.XNome);
            Assert.Equal(cpf, incluirCondutor.Condutor.CPF);
            Assert.Equal(_mensagemIncluirCondutor, incluirCondutor.DescEvento);
        }

        [Fact]
        public void Deve_Testar_Os_Parametros_Passados_Na_Criacao_Da_Consulta_Pelo_Metodo_CriaEnviMDFe()
        {
            //Arrange
            var versao = _configuracao.ConfigWebService.VersaoLayout;

            //Act
            var enviMdfe = ClassesFactory.CriaEnviMDFe(_lote, _mdfe);

            //Assert
            Assert.NotNull(enviMdfe);
            Assert.Equal(versao, enviMdfe.Versao);
            Assert.Equal(_mdfe, enviMdfe.MDFe);
            Assert.Equal(_lote.ToString(), enviMdfe.IdLote);
        }

        [Fact]
        public void Deve_Testar_Os_Parametros_Passados_Na_Criacao_Da_Consulta_Pelo_Metodo_CriaConsReciMDFe()
        {
            //Arrange
            var ambiente = _configuracao.ConfigWebService.Ambiente;
            var versao = _configuracao.ConfigWebService.VersaoLayout;

            //Act
            var consultaRecibo = ClassesFactory.CriaConsReciMDFe(_protocolo);

            //Assert
            Assert.NotNull(consultaRecibo);
            Assert.Equal(ambiente, consultaRecibo.TpAmb);
            Assert.Equal(versao, consultaRecibo.Versao);
            Assert.Equal(_protocolo, consultaRecibo.NRec);
        }

        [Fact]
        public void Deve_Testar_Os_Parametros_Passados_Na_Criacao_Da_Consulta_Pelo_Metodo_CriaConsStatServMDFe()
        {
            //Arrange
            var ambiente = _configuracao.ConfigWebService.Ambiente;
            var versao = _configuracao.ConfigWebService.VersaoLayout;

            //Act
            var consultaStatus = ClassesFactory.CriaConsStatServMDFe();

            //Assert
            Assert.NotNull(consultaStatus);
            Assert.Equal(ambiente, consultaStatus.TpAmb);
            Assert.Equal(versao, consultaStatus.Versao);
            Assert.Equal(_mensagemStatus, consultaStatus.XServ);
        }

        #endregion
    }
}
