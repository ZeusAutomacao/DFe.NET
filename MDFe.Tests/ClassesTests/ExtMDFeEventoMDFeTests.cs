using System;
using System.Xml;
using MDFe.Classes.Extensoes;
using MDFe.Classes.Informacoes.Evento;
using MDFe.Classes.Informacoes.Evento.CorpoEvento;
using MDFe.Classes.Informacoes.Evento.Flags;
using MDFe.Servicos.EventosMDFe;
using MDFe.Tests.Dao;
using MDFe.Tests.Entidades;
using Xunit;

namespace MDFe.Tests.ClassesTests
{
   
    public class ExtMDFeEventoMDFeTests: IDisposable
    {
        #region Variáveis
        private Configuracao _configuracao;
        private MDFeEventoMDFe _evento;
        private MDFeEletronicaFalsa _RepositorioFalsoMdfe;
        private Classes.Informacoes.MDFe _mdfe;
        private readonly string _xmlEsperadoIncluir;
        private readonly string _xmlEsperadoCancelar;
        private readonly string _xmlEsperadoEncerrar;

        private MDFeCondutorIncluir _condutor;
        private readonly string _protocolo;
        private readonly string _justificativa;
        private readonly string _valueKey;

        #endregion

        #region SETUP

        public ExtMDFeEventoMDFeTests()
        {
            var configuracaoDao = new ConfiguracaoDao();
            _configuracao = configuracaoDao.GetConfiguracao();

            _RepositorioFalsoMdfe = new MDFeEletronicaFalsa(_configuracao.Empresa);
            _condutor = new MDFeCondutorIncluir() {CPF = "00000000000", XNome = "NINGUEM"};

            _xmlEsperadoIncluir = "xml-evento-incluir-esperado.xml";
            _xmlEsperadoCancelar = "xml-evento-cancelar-esperado.xml";
            _xmlEsperadoEncerrar = "xml-evento-encerrar-esperado.xml";
            _valueKey = "TESTE";
            _protocolo = "000000000000000";
            _justificativa = "Erro na Matrix";

            _mdfe = _RepositorioFalsoMdfe.GetMdfe();

            var configcertificado = new CertificadoDao().getConfiguracaoCertificado();

            var configuracoes = new ConfiguracaoUtilsDao(_configuracao, configcertificado);
            configuracoes.setCongiguracoes();
        }

        public void Dispose()
        {
            _RepositorioFalsoMdfe = new MDFeEletronicaFalsa(_configuracao.Empresa);
            _mdfe = _RepositorioFalsoMdfe.GetMdfe();
            _evento = new MDFeEventoMDFe();
        }
        #endregion

        #region Testes para a classe ExtMDFeEventoMDFe 

        // <------------------------------------------------- Evento Incluir Condutor --------------------------------------------->
        [Fact]
        public void Deve_Criar_Uma_Requisicao_Evento_incluir_Condutor_Com_Parametros_Validos()
        {
            //Arrange
            var condutor = new MDFeEvIncCondutorMDFe()
            {
                Condutor = _condutor
            };

            _mdfe.Assina();

            var evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.InclusaoDeCondutor, 1, condutor);

            //Act
            var xmlGerado = evento.CriaXmlRequestWs();

            //Assert
            Assert.IsType<XmlDocument>(xmlGerado);
        }

        [Fact]
        public void Deve_Criar_Uma_Requisicao_Evento_incluir_Condutor_Nao_Nula()
        {
            //Arrange
            if (_mdfe != null) Dispose();
            var condutor = new MDFeEvIncCondutorMDFe()
            {
                Condutor = _condutor
            };

            _mdfe.Assina();

            var evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.InclusaoDeCondutor, 1, condutor);

            //Act
            var xmlGerado = evento.CriaXmlRequestWs();

            //Assert
            Assert.NotNull(xmlGerado);
        }

        [Fact]
        public void Deve_Validar_A_Requisicao_Evento_Incluir_Condutor_Com_O_Xml_esperado()
        {
            //Arrange
            if (_mdfe != null) Dispose();
            var condutor = new MDFeEvIncCondutorMDFe()
            {
                Condutor = _condutor
            };

            var repositorioDao = new RepositorioDaoFalso();
            _mdfe.Assina();

            var evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.InclusaoDeCondutor, 1, condutor);
            evento.InfEvento.DhEvento = new DateTime(2018, 11, 14, 11, 47, 37, 03);
            evento.Signature.SignedInfo.Reference.DigestValue = _valueKey;
            evento.Signature.SignatureValue = _valueKey;
            evento.Signature.KeyInfo.X509Data.X509Certificate = _valueKey;

            //Act
            var xmlGerado = evento.CriaXmlRequestWs();
            var xmlEsperado = repositorioDao.GetXmlEsperado(_xmlEsperadoIncluir);

            //Assert
            Assert.Equal(xmlEsperado.InnerXml, xmlGerado.InnerXml);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Para_Criacao_De_Evento_incluir_Condutor_Sem_Versao()
        {
            //Arrange
            if (_mdfe != null) Dispose();
            var condutor = new MDFeEvIncCondutorMDFe()
            {
                Condutor = _condutor
            };

            _mdfe.Assina();

            _evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.InclusaoDeCondutor, 1, condutor);
            _evento.Versao = 0;

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => _evento.CriaXmlRequestWs());

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Salvar_Xml_Localmente_Para_Requisicao_Evento_incluir_Condutor()
        {
            //Arrange
            if (_mdfe != null) Dispose();
            var condutor = new MDFeEvIncCondutorMDFe()
            {
                Condutor = _condutor
            };

            _mdfe.Assina();

            var evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.InclusaoDeCondutor, 1, condutor);

            //Act
            var exception = Record.Exception(() => evento.SalvarXmlEmDisco(_mdfe.Chave()+"1"));

            //Assert
            Assert.Null(exception);
        }

        // <----------------------------------------------------- Evento Cancelar ------------------------------------------------->
        [Fact]
        public void Deve_Criar_Uma_Requisicao_Evento_Cancelar_Com_Parametros_Validos()
        {
            //Arrange
            if (_mdfe != null) Dispose();
            var cancelamento = new MDFeEvCancMDFe()
            {
                NProt = _protocolo,
                XJust = _justificativa
            };

            _mdfe.Assina();

            var evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.Cancelamento, 1, cancelamento);

            //Act
            var xmlGerado = evento.CriaXmlRequestWs();

            //Assert
            Assert.IsType<XmlDocument>(xmlGerado);
        }

        [Fact]
        public void Deve_Criar_Uma_Requisicao_Evento_Cancelar_Nao_Nula()
        {
            //Arrange
            if (_mdfe != null) Dispose();
            var cancelamento = new MDFeEvCancMDFe()
            {
                NProt = _protocolo,
                XJust = _justificativa
            };

            _mdfe.Assina();

            var evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.Cancelamento, 1, cancelamento);

            //Act
            var xmlGerado = evento.CriaXmlRequestWs();

            //Assert
            Assert.NotNull(xmlGerado);
        }

        [Fact]
        public void Deve_Validar_A_Requisicao_Evento_Cancelar_Com_O_Xml_esperado()
        {
            //Arrange
            if (_mdfe != null) Dispose();
            var cancelamento = new MDFeEvCancMDFe()
            {
                NProt = _protocolo,
                XJust = _justificativa
            };

            var repositorioDao = new RepositorioDaoFalso();
            _mdfe.Assina();

            _evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.Cancelamento, 1, cancelamento);
            _evento.InfEvento.DhEvento = new DateTime(2018, 11, 14, 11, 47, 37, 03);
            _evento.Signature.SignedInfo.Reference.DigestValue = _valueKey;
            _evento.Signature.SignatureValue = _valueKey;
            _evento.Signature.KeyInfo.X509Data.X509Certificate = _valueKey;

            //Act
            var xmlGerado = _evento.CriaXmlRequestWs();
            var xmlEsperado = repositorioDao.GetXmlEsperado(_xmlEsperadoCancelar);

            //Assert
            Assert.Equal(xmlEsperado.InnerXml, xmlGerado.InnerXml);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Para_Criacao_De_Evento_Cancelar_Sem_Versao()
        {
            //Arrange
            if (_mdfe != null) Dispose();
            var cancelamento = new MDFeEvCancMDFe()
            {
                NProt = _protocolo,
                XJust = _justificativa
            };

            _mdfe.Assina();

            _evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.InclusaoDeCondutor, 1, cancelamento);
            _evento.Versao = 0;

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => _evento.CriaXmlRequestWs());

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Salvar_Xml_Localmente_Para_Requisicao_Evento_Cancelar()
        {
            //Arrange
            if (_mdfe != null) Dispose();
            var cancelamento = new MDFeEvCancMDFe()
            {
                NProt = _protocolo,
                XJust = _justificativa
            };

            _mdfe.Assina();

            var evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.Cancelamento, 1, cancelamento);

            //Act
            var exception = Record.Exception(() => evento.SalvarXmlEmDisco(_mdfe.Chave() + "2"));

            //Assert
            Assert.Null(exception);
        }

        // <--------------------------------------------------- Evento Encerramento ----------------------------------------------->
        [Fact]
        public void Deve_Criar_Uma_Requisicao_Evento_Encerrar_Com_Parametros_Validos()
        {
            //Arrange
            if (_mdfe != null) Dispose();
            var encerramento = new MDFeEvEncMDFe
            {
                CUF = _mdfe.UFEmitente(),
                DtEnc = new DateTime(2018,11,16),
                CMun = _mdfe.CodigoIbgeMunicipioEmitente(),
                NProt = _protocolo
            };

            _mdfe.Assina();

            _evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.Encerramento, 1, encerramento);
            //Act
            var xmlGerado = _evento.CriaXmlRequestWs();

            //Assert
            Assert.IsType<XmlDocument>(xmlGerado);
        }

        [Fact]
        public void Deve_Criar_Uma_Requisicao_Evento_Encerrar_Nao_Nula()
        {
            //Arrange
            if (_mdfe != null) Dispose();
            var encerramento = new MDFeEvEncMDFe
            {
                CUF = _mdfe.UFEmitente(),
                DtEnc = new DateTime(2018, 11, 16),
                CMun = _mdfe.CodigoIbgeMunicipioEmitente(),
                NProt = _protocolo
            };

            _mdfe.Assina();

            _evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.Encerramento, 1, encerramento);
            //Act
            var xmlGerado = _evento.CriaXmlRequestWs();

            //Assert
            Assert.NotNull(xmlGerado);
        }

        [Fact]
        public void Deve_Testar_A_Requisicao_Evento_Encerrar_Com_O_Xml_esperado()
        {
            //Arrange
            if (_mdfe != null) Dispose();
            var encerramento = new MDFeEvEncMDFe
            {
                CUF = _mdfe.UFEmitente(),
                DtEnc = new DateTime(2018, 11, 16),
                CMun = _mdfe.CodigoIbgeMunicipioEmitente(),
                NProt = _protocolo
            };

            var repositorioDao = new RepositorioDaoFalso();

            _mdfe.Assina();

            _evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.Encerramento, 1, encerramento);
            _evento.InfEvento.DhEvento = new DateTime(2018, 11, 14, 11, 47, 37, 03);
            _evento.Signature.SignedInfo.Reference.DigestValue = _valueKey;
            _evento.Signature.SignatureValue = _valueKey;
            _evento.Signature.KeyInfo.X509Data.X509Certificate = _valueKey;

            //Act
            var xmlGerado = _evento.CriaXmlRequestWs();
            var xmlEsperado = repositorioDao.GetXmlEsperado(_xmlEsperadoEncerrar);

            //Assert
            Assert.Equal(xmlEsperado.InnerXml, xmlGerado.InnerXml);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Para_Criacao_De_Evento_Encerrar_Sem_Versao()
        {
            //Arrange
            if (_mdfe != null) Dispose();
            var encerramento = new MDFeEvEncMDFe
            {
                CUF = _mdfe.UFEmitente(),
                DtEnc = new DateTime(2018, 11, 16),
                CMun = _mdfe.CodigoIbgeMunicipioEmitente(),
                NProt = _protocolo
            };

            _mdfe.Assina();
            
            _evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.InclusaoDeCondutor, 1, encerramento);
            _evento.Versao = 0;

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => _evento.CriaXmlRequestWs());

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Salvar_Xml_Localmente_Para_Requisicao_Evento_Encerramento()
        {
            //Arrange
            if (_mdfe != null) Dispose();
            var encerramento = new MDFeEvEncMDFe
            {
                CUF = _mdfe.UFEmitente(),
                DtEnc = new DateTime(2018, 11, 16),
                CMun = _mdfe.CodigoIbgeMunicipioEmitente(),
                NProt = _protocolo
            };

            _mdfe.Assina();

            _evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.Encerramento, 1, encerramento);

            //Act
            var exception = Record.Exception(() => _evento.SalvarXmlEmDisco(_mdfe.Chave() + "3"));

            //Assert
            Assert.Null(exception);
        }

        // <----------------------------------------------------------------------------------------------------------------------->

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Para_Criacao_De_Evento_Sem_Parametros()
        {
            //Arrange
            var evento = new MDFeEventoMDFe();

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => evento.CriaXmlRequestWs());

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        #endregion
    }
}
