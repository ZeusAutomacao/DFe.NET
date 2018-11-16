using System;
using System.Xml;
using DFe.Utils;
using NUnit.Framework;
using SMDFe.Classes.Extencoes;
using SMDFe.Classes.Informacoes;
using SMDFe.Classes.Informacoes.Evento;
using SMDFe.Classes.Informacoes.Evento.CorpoEvento;
using SMDFe.Classes.Informacoes.Evento.Flags;
using SMDFe.Tests.Dao;
using SMDFe.Tests.Entidades;
using SMDFe.Servicos;
using SMDFe.Servicos.EventosMDFe;

namespace SMDFe.Tests.ClassesTests
{
    [TestFixture]
    public class ExtMDFeEventoMDFeTests
    {
        private Configuracao _configuracao;
        private MDFeEventoMDFe _evento;
        private MDFeEletronicaFalsa _RepositorioFalsoMdfe;
        private MDFe _mdfe;
        private string _xmlEsperadoIncluir;
        private string _xmlEsperadoCancelar;
        private string _xmlEsperadoEncerrar;

        private MDFeCondutorIncluir _condutor;
        private string _protocolo;
        private string _justificativa;

        #region SETUP
        [SetUp]
        public void CriarConfiguração()
        {
            var configuracaoDao = new ConfiguracaoDao();
            _configuracao = configuracaoDao.GetConfiguracao();

            _RepositorioFalsoMdfe = new MDFeEletronicaFalsa(_configuracao.Empresa);
            _condutor = new MDFeCondutorIncluir() {CPF = "00000000000", XNome = "NINGUEM"};

            _xmlEsperadoIncluir = "xml-evento-incluir-esperado.xml";
            _xmlEsperadoCancelar = "xml-evento-cancelar-esperado.xml";
            _xmlEsperadoEncerrar = "xml-evento-encerrar-esperado.xml";

            _protocolo = "000000000000000";
            _justificativa = "Erro na Matrix";

            _mdfe = _RepositorioFalsoMdfe.GetMdfe();
            

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
            _mdfe.Assina();

        }
        #endregion

        #region Testes para a classe ExtMDFeEventoMDFe 

        // <------------------------------------------------- Evento Incluir Condutor --------------------------------------------->
        [Test]
        public void Testa_A_Criacao_De_Uma_Requisicao_Evento_incluir_Condutor_Com_Parametros_Validos()
        {
            //Arrange
            var condutor = new MDFeEvIncCondutorMDFe()
            {
                Condutor = _condutor
            };

            _evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.InclusaoDeCondutor, 1, condutor);

            //Act
            var xmlGerado = _evento.CriaXmlRequestWs();

            //Assert
            Assert.IsInstanceOf<XmlDocument>(xmlGerado);


        }

        [Test]
        public void Testa_A_Requisicao_Evento_Incluir_Condutor_Com_O_Xml_esperado()
        {
            //Arrange
            var condutor = new MDFeEvIncCondutorMDFe()
            {
                Condutor = _condutor
            };

            var repositorioDao = new RepositorioDaoFalso();
            var mdfe = _RepositorioFalsoMdfe.GetMdfe();
            mdfe.Assina();

            _evento = FactoryEvento.CriaEvento(mdfe, MDFeTipoEvento.InclusaoDeCondutor, 1, condutor);
            _evento.InfEvento.DhEvento = new DateTime(2018, 11, 14, 11, 47, 37, 03);
            _evento.Signature.SignedInfo.Reference.DigestValue = "TESTE";
            _evento.Signature.SignatureValue = "TESTE";

            //Act
            var xmlGerado = _evento.CriaXmlRequestWs();
            var xmlEsperado = repositorioDao.GetXmlEsperado(_xmlEsperadoIncluir);

            //Assert
            Assert.AreEqual(xmlEsperado.InnerXml, xmlGerado.InnerXml);
        }

        [Test]
        public void Testa_A_Criacao_De_Uma_Requisicao_Evento_incluir_Condutor_Sem_Versao()
        {
            //Arrange
            var condutor = new MDFeEvIncCondutorMDFe()
            {
                Condutor = _condutor
            };

            var mdfe = _RepositorioFalsoMdfe.GetMdfe();
            mdfe.Assina();
            _evento = FactoryEvento.CriaEvento(mdfe, MDFeTipoEvento.InclusaoDeCondutor, 1, condutor);
            _evento.Versao = 0;

            //Act

            var exception = Assert.Throws<InvalidOperationException>(() => _evento.CriaXmlRequestWs());

            //Assert
            Assert.IsInstanceOf<InvalidOperationException>(exception);
        }

        // <----------------------------------------------------- Evento Cancelar ------------------------------------------------->
        [Test]
        public void Testa_A_Criacao_De_Uma_Requisicao_Evento_Cancelar_Com_Parametros_Validos()
        {
            //Arrange
            var cancelamento = new MDFeEvCancMDFe()
            {
                NProt = _protocolo,
                XJust = _justificativa
            };

            _evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.Cancelamento, 1, cancelamento);

            //Act
            var xmlGerado = _evento.CriaXmlRequestWs();

            //Assert
            Assert.IsInstanceOf<XmlDocument>(xmlGerado);

        }

        [Test]
        public void Testa_A_Requisicao_Evento_Cancelar_Com_O_Xml_esperado()
        {
            //Arrange
            var cancelamento = new MDFeEvCancMDFe()
            {
                NProt = _protocolo,
                XJust = _justificativa
            };

            var repositorioDao = new RepositorioDaoFalso();
            var mdfe = _RepositorioFalsoMdfe.GetMdfe();
            mdfe.Assina();

            _evento = FactoryEvento.CriaEvento(mdfe, MDFeTipoEvento.Cancelamento, 1, cancelamento);
            _evento.InfEvento.DhEvento = new DateTime(2018, 11, 14, 11, 47, 37, 03);
            _evento.Signature.SignedInfo.Reference.DigestValue = "TESTE";
            _evento.Signature.SignatureValue = "TESTE";

            //Act
            var xmlGerado = _evento.CriaXmlRequestWs();
            var xmlEsperado = repositorioDao.GetXmlEsperado(_xmlEsperadoCancelar);

            //Assert
            Assert.AreEqual(xmlEsperado.InnerXml, xmlGerado.InnerXml);
        }

        [Test]
        public void Testa_A_Criacao_De_Uma_Requisicao_Evento_Cancelar_Sem_Versao()
        {
            //Arrange
            var cancelamento = new MDFeEvCancMDFe()
            {
                NProt = _protocolo,
                XJust = _justificativa
            };

            _evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.InclusaoDeCondutor, 1, cancelamento);
            _evento.Versao = 0;

            //Act

            var exception = Assert.Throws<InvalidOperationException>(() => _evento.CriaXmlRequestWs());

            //Assert
            Assert.IsInstanceOf<InvalidOperationException>(exception);
        }

        // <--------------------------------------------------- Evento Encerramento ----------------------------------------------->
        [Test]
        public void Testa_A_Criacao_De_Uma_Requisicao_Evento_Encerrar_Com_Parametros_Validos()
        {
            //Arrange

            var encerramento = new MDFeEvEncMDFe
            {
                CUF = _mdfe.UFEmitente(),
                DtEnc = new DateTime(2018,11,16),
                CMun = _mdfe.CodigoIbgeMunicipioEmitente(),
                NProt = _protocolo
            };

            _evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.Encerramento, 1, encerramento);
            //Act
            var xmlGerado = _evento.CriaXmlRequestWs();

            //Assert
            Assert.IsInstanceOf<XmlDocument>(xmlGerado);

        }

        [Test]
        public void Testa_A_Requisicao_Evento_Encerrar_Com_O_Xml_esperado()
        {
            //Arrange
            var encerramento = new MDFeEvEncMDFe
            {
                CUF = _mdfe.UFEmitente(),
                DtEnc = new DateTime(2018, 11, 16),
                CMun = _mdfe.CodigoIbgeMunicipioEmitente(),
                NProt = _protocolo
            };

            var repositorioDao = new RepositorioDaoFalso();


            _evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.Encerramento, 1, encerramento);
            _evento.InfEvento.DhEvento = new DateTime(2018, 11, 14, 11, 47, 37, 03);
            _evento.Signature.SignedInfo.Reference.DigestValue = "TESTE";
            _evento.Signature.SignatureValue = "TESTE";

            //Act
            var xmlGerado = _evento.CriaXmlRequestWs();
            var xmlEsperado = repositorioDao.GetXmlEsperado(_xmlEsperadoEncerrar);

            //Assert
            Assert.AreEqual(xmlEsperado.InnerXml, xmlGerado.InnerXml);
        }

        [Test]
        public void Testa_A_Criacao_De_Uma_Requisicao_Evento_Encerrar_Sem_Versao()
        {
            //Arrange

            var encerramento = new MDFeEvEncMDFe
            {
                CUF = _mdfe.UFEmitente(),
                DtEnc = new DateTime(2018, 11, 16),
                CMun = _mdfe.CodigoIbgeMunicipioEmitente(),
                NProt = _protocolo
            };


            _evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.InclusaoDeCondutor, 1, encerramento);
            _evento.Versao = 0;

            //Act

            var exception = Assert.Throws<InvalidOperationException>(() => _evento.CriaXmlRequestWs());

            //Assert
            Assert.IsInstanceOf<InvalidOperationException>(exception);
        }

        // <----------------------------------------------------------------------------------------------------------------------->

        [Test]
        public void Testa_A_Criacao_De_Uma_Requisicao_Evento_Sem_Parametros()
        {
            //Arrange
            _evento = new MDFeEventoMDFe();

            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => _evento.CriaXmlRequestWs());

            //Assert
            Assert.IsInstanceOf<InvalidOperationException>(exception);
        }



        #endregion
    }
}
