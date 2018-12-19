using  System;
using System.IO;
using DFe.Utils;
using NUnit.Framework;
using SMDFe.Classes.Extencoes;
using SMDFe.Classes.Informacoes;
using SMDFe.Classes.Informacoes.ConsultaNaoEncerrados;
using SMDFe.Classes.Informacoes.ConsultaProtocolo;
using SMDFe.Classes.Informacoes.Evento;
using SMDFe.Classes.Informacoes.Evento.CorpoEvento;
using SMDFe.Classes.Informacoes.Evento.Flags;
using SMDFe.Classes.Informacoes.RetRecepcao;
using SMDFe.Classes.Informacoes.StatusServico;
using SMDFe.Classes.Servicos.Autorizacao;
using SMDFe.Servicos.EventosMDFe;
using SMDFe.Tests.Dao;
using SMDFe.Tests.Entidades;
using SMDFe.Utils.Flags;
using SMDFe.Utils.Validacao;


namespace SMDFe.Tests.UtilsTests
{
    [TestFixture]
    public class ValdidadorTests
    {
        #region Variáveis
        private Configuracao _configuracao;
        private string _schema_status;
        private string _schema_encerradas;
        private string _schema_recibo;
        private string _schema_protocolo;
        private string _schema_mdfe;
        private string _schema_enviMdfe;
        private string _schema_modalMdfe;
        private string _schema_eventos;
        private string _schema_incorreto;
        private MDFeEletronicaFalsa _RepositorioFalsoMdfe;
        private MDFe _mdfe;
        private MDFeEnviMDFe _enviMdFe;
        private MDFeCondutorIncluir _condutor;
        private MDFeEventoMDFe _evento;
        private string _protocolo;
        private string _justificativa;

        #endregion


        #region SETUP
        [SetUp]
        public void CriarConfiguração()
        {
            var configuracaoDao = new ConfiguracaoDao();
            _configuracao = configuracaoDao.GetConfiguracao();

            _schema_status = "consStatServMDFe_v3.00.xsd";
            _schema_encerradas = "consMDFeNaoEnc_v3.00.xsd";
            _schema_recibo = "consReciMdfe_v3.00.xsd";
            _schema_protocolo = "consSitMdfe_v3.00.xsd";
            _schema_mdfe = "MDFe_v3.00.xsd";
            _schema_enviMdfe = "enviMDFe_v3.00.xsd";
            _schema_modalMdfe = "MDFeModalRodoviario_v3.00.xsd";
            _schema_eventos = "eventoMDFe_v3.00.xsd";
            _schema_incorreto = "schema_falso_v3.00.xsd";
            _protocolo = "000000000000000";
            _justificativa = "Erro na Matrix";

            var configuracaoCertificado = new ConfiguracaoCertificado
            {
                Senha = _configuracao.CertificadoDigital.Senha,
                Arquivo = _configuracao.CertificadoDigital.CaminhoArquivo,
                ManterDadosEmCache = _configuracao.CertificadoDigital.ManterEmCache,
                Serial = _configuracao.CertificadoDigital.NumeroDeSerie
            };

            _RepositorioFalsoMdfe = new MDFeEletronicaFalsa(_configuracao.Empresa);
            _mdfe = _RepositorioFalsoMdfe.GetMdfe();
            _condutor = new MDFeCondutorIncluir() { CPF = "00000000000", XNome = "NINGUEM" };

            Utils.Configuracoes.MDFeConfiguracao.Instancia.ConfiguracaoCertificado = configuracaoCertificado;

            Utils.Configuracoes.MDFeConfiguracao.Instancia.CaminhoSchemas = _configuracao.ConfigWebService.CaminhoSchemas;
            Utils.Configuracoes.MDFeConfiguracao.Instancia.CaminhoSalvarXml = _configuracao.DiretorioSalvarXml;
            Utils.Configuracoes.MDFeConfiguracao.Instancia.IsSalvarXml = _configuracao.IsSalvarXml;

            Utils.Configuracoes.MDFeConfiguracao.Instancia.VersaoWebService.VersaoLayout = _configuracao.ConfigWebService.VersaoLayout;

            Utils.Configuracoes.MDFeConfiguracao.Instancia.VersaoWebService.TipoAmbiente = _configuracao.ConfigWebService.Ambiente;
            Utils.Configuracoes.MDFeConfiguracao.Instancia.VersaoWebService.UfEmitente = _configuracao.ConfigWebService.UfEmitente;
            Utils.Configuracoes.MDFeConfiguracao.Instancia.VersaoWebService.TimeOut = _configuracao.ConfigWebService.TimeOut;

            _mdfe.Assina();

        }
        #endregion


        #region Testes para a validação do Schema Status

        [Test]
        public void Deve_Validar_O_Xml_Com_Nome_E_Schema_Corretos_Para_StatusMDFe()
        {

            //Arrange

            var xmlEnvio = new MDFeConsStatServMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout
            };

          
            //Act
            Validador.Valida(xmlEnvio.XmlString(), _schema_status);

            //Assert
            Assert.That(true);
        }

        [Test]
        public void Deve_Recusar_O_Xml_Por_Falta_Do_Status_Para_StatusMDFe()
        {

            //Arrange

            var xmlEnvio = new MDFeConsStatServMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                XServ = null
            };

            
            //Act
            var exception = Assert.Throws<Exception>(() => Validador.Valida(xmlEnvio.XmlString(), _schema_status));

            //Assert
            Assert.IsInstanceOf<Exception>(exception);
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Xml_Para_StatusMDFe()
        {
            //Arrange


            //Act
            var exception = Assert.Throws<ArgumentNullException>(() => Validador.Valida(null, _schema_status));

            //Assert
            Assert.IsInstanceOf<ArgumentNullException>(exception);

        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Schema_Para_StatusMDFe()
        {
            //Arrange
            var xmlEnvio = new MDFeConsStatServMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout
            };

            //Act
            var exception =
                Assert.Throws<DirectoryNotFoundException>(() => Validador.Valida(xmlEnvio.XmlString(), null));

            //Assert
            Assert.IsInstanceOf<DirectoryNotFoundException>(exception);
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_XML_Incorreto_Para_StatusMDFe()
        {
            //Arrange
            var xmlEnvio = new MDFeConsStatServMDFe();
            

            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => Validador.Valida(xmlEnvio.XmlString(), _schema_status));

            //Arrange
            Assert.IsInstanceOf<InvalidOperationException>(exception);
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Schema_Incorreto_Para_StatusMDFe()
        {
            //Arrange
            var xmlEnvio = new MDFeConsStatServMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout
            };

            

            //Act
            var exception = Assert.Throws<FileNotFoundException>(() => Validador.Valida(xmlEnvio.XmlString(), _schema_incorreto));

            //Assert
            Assert.IsInstanceOf<FileNotFoundException>(exception);
        }


        #endregion

        #region Testes para a validação do Schema Consultas Não Encerradas

        [Test]
        public void Deve_Validar_O_Xml_Com_Nome_E_Schema_Corretos_Para_ConsultasNaoEncerradas()
        {

            //Arrange

            var xmlEnvio = new MDFeCosMDFeNaoEnc()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                CNPJ = _configuracao.Empresa.Cnpj
            };


            //Act
            Validador.Valida(xmlEnvio.XmlString(), _schema_encerradas);

            //Assert
            Assert.That(true);


        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Campo_CNPJ_Para_ConsultasNaoEncerradas()
        {
            //Arrange
            var xmlEnvio = new MDFeCosMDFeNaoEnc()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                CNPJ = null
            };



            //Act
            var exception = Assert.Throws<Exception>(() => Validador.Valida(xmlEnvio.XmlString(), _schema_encerradas));

            //Assert
            Assert.IsInstanceOf<Exception>(exception);

        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Campo_Consulta_Para_ConsultasNaoEncerradas()
        {
            //Arrange
            var xmlEnvio = new MDFeCosMDFeNaoEnc()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                CNPJ = _configuracao.Empresa.Cnpj,
                XServ = null
            };



            //Act
            var exception = Assert.Throws<Exception>(() => Validador.Valida(xmlEnvio.XmlString(), _schema_encerradas));

            //Assert
            Assert.IsInstanceOf<Exception>(exception);

        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Xml_Para_ConsultasNaoEncerradas()
        {
            //Arrange


            //Act
            var exception = Assert.Throws<ArgumentNullException>(() => Validador.Valida(null, _schema_encerradas));

            //Assert
            Assert.IsInstanceOf<ArgumentNullException>(exception);

        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Schema_Para_ConsultasNaoEncerradas()
        {
            //Arrange
            var xmlEnvio = new MDFeCosMDFeNaoEnc()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                CNPJ = _configuracao.Empresa.Cnpj
            };

            //Act
            var exception = Assert.Throws<DirectoryNotFoundException>(() => Validador.Valida(xmlEnvio.XmlString(), null));

            //Assert
            Assert.IsInstanceOf<DirectoryNotFoundException>(exception);
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_XML_Incorreto_Para_ConsultasNaoEncerradas()
        {
            //Arrange
            var xmlEnvio = new MDFeCosMDFeNaoEnc();


            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => Validador.Valida(xmlEnvio.XmlString(), _schema_encerradas));

            //Arrange
            Assert.IsInstanceOf<InvalidOperationException>(exception);
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Schema_Incorreto_Para_ConsultasNaoEncerradas()
        {
            //Arrange
            var xmlEnvio = new MDFeCosMDFeNaoEnc()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                CNPJ = _configuracao.Empresa.Cnpj
            };

            //Act
            var exception = Assert.Throws<FileNotFoundException>(() => Validador.Valida(xmlEnvio.XmlString(), _schema_incorreto));

            //Assert
            Assert.IsInstanceOf<FileNotFoundException>(exception);
        }


        #endregion

        #region Testes para a validação do Schema Consultas por Recibo

        [Test]
        public void Deve_Validar_O_Xml_Com_Nome_E_Schema_Corretos_Para_ConsultaPorRecibo()
        {

            //Arrange
            
            var xmlEnvio = new MDFeConsReciMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                NRec = "000000000000000"
            };


            //Act
            Validador.Valida(xmlEnvio.XmlString(), _schema_recibo);

            //Assert
            Assert.That(true);
        }

        [Test]
        public void Deve_Recusar_O_Xml_Por_Falta_Do_Recibo_Para_ConsultaPorRecibo()
        {

            //Arrange

            var xmlEnvio = new MDFeConsReciMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                NRec = null
            };

            //Act
            var exception = Assert.Throws<Exception>(() => Validador.Valida(xmlEnvio.XmlString(), _schema_recibo));

            //Assert
            Assert.IsInstanceOf<Exception>(exception);
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Xml_Para_ConsultaPorRecibos()
        {
            //Arrange


            //Act
            var exception = Assert.Throws<ArgumentNullException>(() => Validador.Valida(null, _schema_recibo));

            //Assert
            Assert.IsInstanceOf<ArgumentNullException>(exception);

        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Schema_Para_ConsultaPorRecibo()
        {
            //Arrange
            var xmlEnvio = new MDFeConsReciMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                NRec = "000000000000000"
            };

            //Act
            var exception = Assert.Throws<DirectoryNotFoundException>(() => Validador.Valida(xmlEnvio.XmlString(), null));

            //Assert
            Assert.IsInstanceOf<DirectoryNotFoundException>(exception);
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_XML_Incorreto_Para_ConsultaPorRecibo()
        {
            //Arrange
            var xmlEnvio = new MDFeConsReciMDFe();


            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => Validador.Valida(xmlEnvio.XmlString(), _schema_recibo));

            //Arrange
            Assert.IsInstanceOf<InvalidOperationException>(exception);
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Schema_Incorreto_Para_ConsultaPorRecibo()
        {
            //Arrange
            var xmlEnvio = new MDFeConsReciMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                NRec = "000000000000000"
            };

            

            //Act
            var exception = Assert.Throws<FileNotFoundException>(() => Validador.Valida(xmlEnvio.XmlString(), _schema_incorreto));

            //Assert
            Assert.IsInstanceOf<FileNotFoundException>(exception);
        }

        #endregion

        #region Testes para a validação do Schema Consultas Por Protocolo

        [Test]
        public void Deve_Validar_O_Xml_Com_Nome_E_Schema_Corretos_Para_Consulta_Por_Protocolo()
        {

            //Arrange

            var xmlEnvio = new MDFeConsSitMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                ChMDFe = "00000000000000000000000000000000000000000000"
            };


            //Act
            Validador.Valida(xmlEnvio.XmlString(), _schema_protocolo);

            //Assert
            Assert.That(true);


        }

        [Test]
        public void Deve_Recusar_O_Xml_Por_Falta_Do_Protocolo_Para_Consulta_Por_Protocolo()
        {

            //Arrange

            var xmlEnvio = new MDFeConsSitMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                ChMDFe = null
            };


            //Act
            var exception = Assert.Throws<Exception>(() => Validador.Valida(xmlEnvio.XmlString(), _schema_protocolo));

            //Assert
            Assert.IsInstanceOf<Exception>(exception);


        }

        [Test]
        public void Deve_Recusar_O_Xml_Por_Falta_Do_Servico_Para_Consulta_Por_Protocolo()
        {

            //Arrange

            var xmlEnvio = new MDFeConsSitMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                ChMDFe = "00000000000000000000000000000000000000000000",
                XServ = null
            };


            //Act
            var exception = Assert.Throws<Exception>(() => Validador.Valida(xmlEnvio.XmlString(), _schema_protocolo));

            //Assert
            Assert.IsInstanceOf<Exception>(exception);


        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Xml_Para_Consulta_Por_Protocolo()
        {
            //Arrange


            //Act
            var exception = Assert.Throws<ArgumentNullException>(() => Validador.Valida(null, _schema_protocolo));

            //Assert
            Assert.IsInstanceOf<ArgumentNullException>(exception);

        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Schema_Para_Consulta_Por_Protocolo()
        {
            //Arrange
            var xmlEnvio = new MDFeConsSitMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                ChMDFe = "00000000000000000000000000000000000000000000"
            };

            //Act
            var exception = Assert.Throws<DirectoryNotFoundException>(() => Validador.Valida(xmlEnvio.XmlString(), null));

            //Assert
            Assert.IsInstanceOf<DirectoryNotFoundException>(exception);
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_XML_Incorreto_Para_Consulta_Por_Protocolo()
        {
            //Arrange
            var xmlEnvio = new MDFeConsSitMDFe();


            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => Validador.Valida(xmlEnvio.XmlString(), _schema_protocolo));

            //Arrange
            Assert.IsInstanceOf<InvalidOperationException>(exception);
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Schema_Incorreto_Para_Consulta_Por_Protocolo()
        {
            //Arrange
            var xmlEnvio = new MDFeConsSitMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                ChMDFe = "00000000000000000000000000000000000000000000"
            };

            //Act
            var exception = Assert.Throws<FileNotFoundException>(() => Validador.Valida(xmlEnvio.XmlString(), _schema_incorreto));

            //Assert
            Assert.IsInstanceOf<FileNotFoundException>(exception);
        }

        #endregion

        #region Testes para a validação dos Schemas MDFe

        // <--------------------------------------------- MDFe testes --------------------------------------------->
        [Test]
        public void Deve_Validar_O_Xml_Com_Nome_E_Schema_Corretos_Para_MDFe()
        {

            //Arrange
            var xml = FuncoesXml.ClasseParaXmlString(_mdfe);

            //Act
            Validador.Valida(xml, _schema_mdfe);

            //Assert
            Assert.That(true);
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Xml_Para_MDFe()
        {
            //Arrange


            //Act
            var exception = Assert.Throws<ArgumentNullException>(() => Validador.Valida(null, _schema_mdfe));

            //Assert
            Assert.IsInstanceOf<Exception>(exception);

        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Schema_Para_MDFe()
        {
            //Arrange
            var xml = FuncoesXml.ClasseParaXmlString(_mdfe);


            //Act
            var exception = Assert.Throws<DirectoryNotFoundException>(() => Validador.Valida(xml, null));

            //Assert
            Assert.IsInstanceOf<DirectoryNotFoundException>(exception);
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_XML_Incorreto_Para_MDFe()
        {
            //Arrange

            _mdfe.InfMDFe.Id = "";

            var xml = FuncoesXml.ClasseParaXmlString(_mdfe);

            //Act
            var exception = Assert.Throws<Exception>(() => Validador.Valida(xml, _schema_mdfe));

            //Arrange
            Assert.IsInstanceOf<Exception>(exception);
            
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Schema_Incorreto_Para_MDFe()
        {
            //Arrange
            var xml = FuncoesXml.ClasseParaXmlString(_mdfe);

            //Act
            var exception = Assert.Throws<FileNotFoundException>(() => Validador.Valida(xml, _schema_incorreto));

            //Assert
            Assert.IsInstanceOf<FileNotFoundException>(exception);

        }

        // <--------------------------------------------- EnviMDFe testes --------------------------------------------->
        [Test]
        public void Deve_Validar_O_Xml_Com_Nome_E_Schema_Corretos_Para_EnviMDFe()
        {

            //Arrange
            _enviMdFe = new MDFeEnviMDFe()
            {
                Versao = VersaoServico.Versao300,
                MDFe = _mdfe,
                IdLote = "1"
            };
            _enviMdFe.MDFe.Assina();

            var xml = FuncoesXml.ClasseParaXmlString(_enviMdFe);

            //Act
            Validador.Valida(xml, _schema_enviMdfe);

            //Assert
            Assert.That(true);

        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Xml_Para_EnviMDFe()
        {
            //Arrange


            //Act
            var exception = Assert.Throws<ArgumentNullException>(() => Validador.Valida(null, _schema_enviMdfe));

            //Assert
            Assert.IsInstanceOf<ArgumentNullException>(exception);

        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Schema_Para_EnviMDFe()
        {
            //Arrange
            _enviMdFe = new MDFeEnviMDFe()
            {
                Versao = VersaoServico.Versao300,
                MDFe = _mdfe,
                IdLote = "1"
            };
            _enviMdFe.MDFe.Assina();

            var xml = FuncoesXml.ClasseParaXmlString(_enviMdFe);


            //Act
            var exception = Assert.Throws<DirectoryNotFoundException>(() => Validador.Valida(xml, null));

            //Assert
            Assert.IsInstanceOf<DirectoryNotFoundException>(exception);
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_XML_Incorreto_Para_EnviMDFe()
        {
            //Arrange
            _enviMdFe = new MDFeEnviMDFe()
            {
                Versao = VersaoServico.Versao300,
                MDFe = _mdfe,
                IdLote = null
            };
            _enviMdFe.MDFe.Assina();

            var xml = FuncoesXml.ClasseParaXmlString(_enviMdFe);

            //Act
            var exception = Assert.Throws<Exception>(() => Validador.Valida(xml, _schema_enviMdfe));

            //Arrange
            Assert.IsInstanceOf<Exception>(exception);
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Schema_Incorreto_Para_EnviMDFe()
        {
            //Arrange
            _enviMdFe = new MDFeEnviMDFe()
            {
                Versao = VersaoServico.Versao300,
                MDFe = _mdfe,
                IdLote = "1"
            };
            _enviMdFe.MDFe.Assina();

            var xml = FuncoesXml.ClasseParaXmlString(_enviMdFe);

            //Act
            var exception = Assert.Throws<FileNotFoundException>(() => Validador.Valida(xml, _schema_incorreto));

            //Assert
            Assert.IsInstanceOf<FileNotFoundException>(exception);

        }

        // <--------------------------------------------- InfoModal testes --------------------------------------------->
        [Test]
        public void Deve_Validar_O_Xml_Com_Nome_E_Schema_Corretos_Para_InfoModal()
        {

            //Arrange
            var infoModal = _mdfe.InfMDFe.InfModal;


            var xml = FuncoesXml.ClasseParaXmlString(infoModal);

            //Act
            Validador.Valida(xml, _schema_modalMdfe);

            //Assert
            Assert.That(true);

        }


        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Xml_Para_InfoModal()
        {
            //Arrange


            //Act
            var exception = Assert.Throws<ArgumentNullException>(() => Validador.Valida(null, _schema_modalMdfe));

            //Assert
            Assert.IsInstanceOf<ArgumentNullException>(exception);

        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Schema_Para_InfoModal()
        {
            //Arrange
            var infoModal = _mdfe.InfMDFe.InfModal;
            var xml = FuncoesXml.ClasseParaXmlString(infoModal);


            //Act
            var exception = Assert.Throws<DirectoryNotFoundException>(() => Validador.Valida(xml, null));

            //Assert
            Assert.IsInstanceOf<DirectoryNotFoundException>(exception);
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_XML_Incorreto_Para_InfoModal()
        {
            //Arrange
            var infoModal = _mdfe.InfMDFe.InfModal;
            infoModal.Modal = new MDFeRodo();

            var xml = FuncoesXml.ClasseParaXmlString(infoModal);


            //Act
            var exception = Assert.Throws<Exception>(() => Validador.Valida(xml, _schema_modalMdfe));

            //Arrange
            Assert.IsInstanceOf<Exception>(exception);
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Schema_Incorreto_Para_InfoModal()
        {
            //Arrange
            var infoModal = _mdfe.InfMDFe.InfModal;


            var xml = FuncoesXml.ClasseParaXmlString(infoModal);

            //Act
            var exception = Assert.Throws<FileNotFoundException>(() => Validador.Valida(xml, _schema_incorreto));

            //Assert
            Assert.IsInstanceOf<FileNotFoundException>(exception);

        }

        #endregion

        #region Testes para a validação do Schema Eventos

        [Test] // Incluir Condutor Evento
        public void Deve_Validar_O_Xml_Com_Nome_E_Schema_Corretos_Para_Incluir_Condutor()
        {

            //Arrange
            var condutor = new MDFeEvIncCondutorMDFe()
            {
                Condutor = _condutor
            };

            _evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.InclusaoDeCondutor, 1, condutor);
            var xml = FuncoesXml.ClasseParaXmlString(_evento);
            //Act
            Validador.Valida(xml, _schema_eventos);

            //Assert
            Assert.That(true);
        }

        [Test] // Encerrar Evento
        public void Deve_Validar_O_Xml_Com_Nome_E_Schema_Corretos_Para_Encerrar_Evento()
        {

            //Arrange
            var encerramento = new MDFeEvEncMDFe
            {
                CUF = _mdfe.UFEmitente(),
                DtEnc = new DateTime(2018, 11, 16),
                CMun = _mdfe.CodigoIbgeMunicipioEmitente(),
                NProt = _protocolo
            };

            _evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.Encerramento, 1, encerramento);

            var xml = FuncoesXml.ClasseParaXmlString(_evento);
            //Act
            Validador.Valida(xml, _schema_eventos);

            //Assert
            Assert.That(true);
        }

        [Test] // Cancelar Evento
        public void Deve_Validar_O_Xml_Com_Nome_E_Schema_Corretos_Para_Cancelar_Evento()
        {

            //Arrange
            var cancelamento = new MDFeEvCancMDFe()
            {
                NProt = _protocolo,
                XJust = _justificativa
            };

            _evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.Cancelamento, 1, cancelamento);
            var xml = FuncoesXml.ClasseParaXmlString(_evento);
            //Act
            Validador.Valida(xml, _schema_eventos);

            //Assert
            Assert.That(true);
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Xml_Para_O_Evento()
        {
            //Arrange


            //Act
            var exception = Assert.Throws<ArgumentNullException>(() => Validador.Valida(null, _schema_eventos));

            //Assert
            Assert.IsInstanceOf<ArgumentNullException>(exception);

        }

        [Test] // Incluir Condutor Evento
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Schema_Para_Incluir_Condutor()
        {
            //Arrange
            var condutor = new MDFeEvIncCondutorMDFe()
            {
                Condutor = _condutor
            };

            _evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.InclusaoDeCondutor, 1, condutor);
            var xml = FuncoesXml.ClasseParaXmlString(_evento);

            //Act
            var exception = Assert.Throws<DirectoryNotFoundException>(() => Validador.Valida(xml, null));

            //Assert
            Assert.IsInstanceOf<DirectoryNotFoundException>(exception);
        }

        [Test] // Cancelar Evento
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Schema_Para_Cancelar_Evento()
        {
            //Arrange
            var cancelamento = new MDFeEvCancMDFe()
            {
                NProt = _protocolo,
                XJust = _justificativa
            };

            _evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.Cancelamento, 1, cancelamento);
            var xml = FuncoesXml.ClasseParaXmlString(_evento);

            //Act
            var exception = Assert.Throws<DirectoryNotFoundException>(() => Validador.Valida(xml, null));

            //Assert
            Assert.IsInstanceOf<DirectoryNotFoundException>(exception);
        }

        [Test] // Encerrar Evento
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Schema_Para_Encerrar_Eventos()
        {
            //Arrange
            var encerramento = new MDFeEvEncMDFe
            {
                CUF = _mdfe.UFEmitente(),
                DtEnc = new DateTime(2018, 11, 16),
                CMun = _mdfe.CodigoIbgeMunicipioEmitente(),
                NProt = _protocolo
            };

            _evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.Encerramento, 1, encerramento);

            var xml = FuncoesXml.ClasseParaXmlString(_evento);


            //Act
            var exception = Assert.Throws<DirectoryNotFoundException>(() => Validador.Valida(xml, null));

            //Assert
            Assert.IsInstanceOf<DirectoryNotFoundException>(exception);
        }


        [Test] // Incluir Condutor Evento
        public void Deve_Recusar_A_Validacao_Por_XML_Incorreto_Para_Incluir_Evento()
        {
            //Arrange
            var condutor = new MDFeEvIncCondutorMDFe()
            {
                Condutor = _condutor
            };

            _evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.InclusaoDeCondutor, 1, condutor);
            _evento.InfEvento.Id = null;
            var xml = FuncoesXml.ClasseParaXmlString(_evento);

            //Act
            var exception = Assert.Throws<Exception>(() => Validador.Valida(xml, _schema_eventos));

            //Arrange
            Assert.IsInstanceOf<Exception>(exception);
        }

        [Test] // Cancelar Evento
        public void Deve_Recusar_A_Validacao_Por_XML_Incorreto_Para_Cancelar_Evento()
        {
            //Arrange
            var cancelamento = new MDFeEvCancMDFe()
            {
                NProt = _protocolo,
                XJust = _justificativa
            };

            _evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.Cancelamento, 1, cancelamento);
            _evento.InfEvento.Id = null;

            var xml = FuncoesXml.ClasseParaXmlString(_evento);

            //Act
            var exception = Assert.Throws<Exception>(() => Validador.Valida(xml, _schema_eventos));

            //Arrange
            Assert.IsInstanceOf<Exception>(exception);
        }

        [Test] // Encerrar Evento
        public void Deve_Recusar_A_Validacao_Por_XML_Incorreto_Para_Encerrar_Eventos()
        {
            //Arrange
            var encerramento = new MDFeEvEncMDFe
            {
                CUF = _mdfe.UFEmitente(),
                DtEnc = new DateTime(2018, 11, 16),
                CMun = _mdfe.CodigoIbgeMunicipioEmitente(),
                NProt = _protocolo
            };

            _evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.Encerramento, 1, encerramento);
            _evento.InfEvento.Id = null;

            var xml = FuncoesXml.ClasseParaXmlString(_evento);
            
            //Act
            var exception = Assert.Throws<Exception>(() => Validador.Valida(xml, _schema_eventos));

            //Arrange
            Assert.IsInstanceOf<Exception>(exception);
        }

        [Test] // Incluir Condutor Evento
        public void Deve_Recusar_A_Validacao_Por_Schema_Incorreto_Para_Incluir_Condutor()
        {
            //Arrange
            var condutor = new MDFeEvIncCondutorMDFe()
            {
                Condutor = _condutor
            };

            _evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.InclusaoDeCondutor, 1, condutor);
            var xml = FuncoesXml.ClasseParaXmlString(_evento);

            //Act
            var exception = Assert.Throws<FileNotFoundException>(() => Validador.Valida(xml, _schema_incorreto));

            //Assert
            Assert.IsInstanceOf<FileNotFoundException>(exception);

        }

        [Test] // Cancelar Evento
        public void Deve_Recusar_A_Validacao_Por_Schema_Incorreto_Para_Cancelar_Evento()
        {
            //Arrange
            var cancelamento = new MDFeEvCancMDFe()
            {
                NProt = _protocolo,
                XJust = _justificativa
            };

            _evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.Cancelamento, 1, cancelamento);
            var xml = FuncoesXml.ClasseParaXmlString(_evento);

            //Act
            var exception = Assert.Throws<FileNotFoundException>(() => Validador.Valida(xml, _schema_incorreto));

            //Assert
            Assert.IsInstanceOf<FileNotFoundException>(exception);

        }

        [Test] // Encerramento Evento
        public void Deve_Recusar_A_Validacao_Por_Schema_Incorreto_Para_Encerrar_Eventos()
        {
            //Arrange
            //Arrange
            var encerramento = new MDFeEvEncMDFe
            {
                CUF = _mdfe.UFEmitente(),
                DtEnc = new DateTime(2018, 11, 16),
                CMun = _mdfe.CodigoIbgeMunicipioEmitente(),
                NProt = _protocolo
            };

            _evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.Encerramento, 1, encerramento);

            var xml = FuncoesXml.ClasseParaXmlString(_evento);

            //Act
            var exception = Assert.Throws<FileNotFoundException>(() => Validador.Valida(xml, _schema_incorreto));

            //Assert
            Assert.IsInstanceOf<FileNotFoundException>(exception);

        }

        #endregion


        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Dos_Parametros()
        {
            //Arrange

            //Act
            var exception = Assert.Throws<DirectoryNotFoundException>(() => Validador.Valida(null, null));

            //Assert
            Assert.IsInstanceOf<DirectoryNotFoundException>(exception);
        }
    }
}
