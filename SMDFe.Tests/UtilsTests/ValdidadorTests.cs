using  System;
using System.IO;
using DFe.Utils;
using NUnit.Framework;
using SMDFe.Classes.Extencoes;
using SMDFe.Classes.Informacoes;
using SMDFe.Classes.Informacoes.ConsultaNaoEncerrados;
using SMDFe.Classes.Informacoes.ConsultaProtocolo;
using SMDFe.Classes.Informacoes.RetRecepcao;
using SMDFe.Classes.Informacoes.StatusServico;
using SMDFe.Tests.Dao;
using SMDFe.Tests.Entidades;
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

            Utils.Configuracoes.MDFeConfiguracao.CaminhoSchemas = _configuracao.ConfigWebService.CaminhoSchemas;
            Utils.Configuracoes.MDFeConfiguracao.CaminhoSalvarXml = _configuracao.DiretorioSalvarXml;
            Utils.Configuracoes.MDFeConfiguracao.IsSalvarXml = _configuracao.IsSalvarXml;

            Utils.Configuracoes.MDFeConfiguracao.VersaoWebService.VersaoLayout = _configuracao.ConfigWebService.VersaoLayout;

            Utils.Configuracoes.MDFeConfiguracao.VersaoWebService.TipoAmbiente = _configuracao.ConfigWebService.Ambiente;
            Utils.Configuracoes.MDFeConfiguracao.VersaoWebService.UfEmitente = _configuracao.ConfigWebService.UfEmitente;
            Utils.Configuracoes.MDFeConfiguracao.VersaoWebService.TimeOut = _configuracao.ConfigWebService.TimeOut;


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
            var mdfeDaoFalsa = new RepositorioDaoFalso();
            var mdfe = mdfeDaoFalsa.GetMdFeEletronica();
            var xml = FuncoesXml.ClasseParaXmlString(mdfe);

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
            var mdfeDaoFalsa = new RepositorioDaoFalso();
            var mdfe = mdfeDaoFalsa.GetMdFeEletronica();
            var xml = FuncoesXml.ClasseParaXmlString(mdfe);


            //Act
            var exception = Assert.Throws<DirectoryNotFoundException>(() => Validador.Valida(xml, null));

            //Assert
            Assert.IsInstanceOf<DirectoryNotFoundException>(exception);
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_XML_Incorreto_Para_MDFe()
        {
            //Arrange
            var mdfeDaoFalsa = new RepositorioDaoFalso();
            var mdfe = mdfeDaoFalsa.GetMdFeEletronica();
            mdfe.InfMDFe.Id = "";

            var xml = FuncoesXml.ClasseParaXmlString(mdfe);

            //Act
            var exception = Assert.Throws<Exception>(() => Validador.Valida(xml, _schema_mdfe));

            //Arrange
            Assert.IsInstanceOf<Exception>(exception);
            
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Schema_Incorreto_Para_MDFe()
        {
            //Arrange
            var mdfeDaoFalsa = new RepositorioDaoFalso();
            var mdfe = mdfeDaoFalsa.GetMdFeEletronica();
            var xml = FuncoesXml.ClasseParaXmlString(mdfe);

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
            var envimdfeDaoFalsa = new RepositorioDaoFalso();
            var envi_mdfe = envimdfeDaoFalsa.GetEnviMdFe();
            var xml = FuncoesXml.ClasseParaXmlString(envi_mdfe);

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
            var envimdfeDaoFalsa = new RepositorioDaoFalso();
            var envi_mdfe = envimdfeDaoFalsa.GetEnviMdFe();
            var xml = FuncoesXml.ClasseParaXmlString(envi_mdfe);


            //Act
            var exception = Assert.Throws<DirectoryNotFoundException>(() => Validador.Valida(xml, null));

            //Assert
            Assert.IsInstanceOf<DirectoryNotFoundException>(exception);
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_XML_Incorreto_Para_EnviMDFe()
        {
            //Arrange
            var envimdfeDaoFalsa = new RepositorioDaoFalso();
            var envi_mdfe = envimdfeDaoFalsa.GetEnviMdFe();
            envi_mdfe.IdLote = "";

            var xml = FuncoesXml.ClasseParaXmlString(envi_mdfe);

            //Act
            var exception = Assert.Throws<Exception>(() => Validador.Valida(xml, _schema_enviMdfe));

            //Arrange
            Assert.IsInstanceOf<Exception>(exception);
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Schema_Incorreto_Para_EnviMDFe()
        {
            //Arrange
            var envimdfeDaoFalsa = new RepositorioDaoFalso();
            var envi_mdfe = envimdfeDaoFalsa.GetEnviMdFe();
            var xml = FuncoesXml.ClasseParaXmlString(envi_mdfe);

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
            var envimdfeDaoFalsa = new RepositorioDaoFalso();
            var infoModal = envimdfeDaoFalsa.GetEnviMdFe().MDFe.InfMDFe.InfModal;


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
            var envimdfeDaoFalsa = new RepositorioDaoFalso();
            var infoModal = envimdfeDaoFalsa.GetEnviMdFe().MDFe.InfMDFe.InfModal;
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
            var envimdfeDaoFalsa = new RepositorioDaoFalso();
            var infoModal = envimdfeDaoFalsa.GetEnviMdFe().MDFe.InfMDFe.InfModal;
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
            var envimdfeDaoFalsa = new RepositorioDaoFalso();
            var infoModal = envimdfeDaoFalsa.GetEnviMdFe().MDFe.InfMDFe.InfModal;


            var xml = FuncoesXml.ClasseParaXmlString(infoModal);

            //Act
            var exception = Assert.Throws<FileNotFoundException>(() => Validador.Valida(xml, _schema_incorreto));

            //Assert
            Assert.IsInstanceOf<FileNotFoundException>(exception);

        }

        #endregion

        #region Testes para a validação do Schema Eventos

        [TestCase(1)] // Incluir Condutor Evento
        [TestCase(2)] // Cancelar Evento
        [TestCase(3)] // Encerramento Evento
        public void Deve_Validar_O_Xml_Com_Nome_E_Schema_Corretos_Para_Os_Eventos(int tipo)
        {

            //Arrange
            var mdfeDaoFalsa = new RepositorioDaoFalso();
            var evento = mdfeDaoFalsa.GetEvento(tipo);
            var xml = FuncoesXml.ClasseParaXmlString(evento);

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

        [TestCase(1)] // Incluir Condutor Evento
        [TestCase(2)] // Cancelar Evento
        [TestCase(3)] // Encerramento Evento
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Schema_Para_Os_Eventos(int tipo)
        {
            //Arrange
            var mdfeDaoFalsa = new RepositorioDaoFalso();
            var evento = mdfeDaoFalsa.GetEvento(tipo);
            var xml = FuncoesXml.ClasseParaXmlString(evento);


            //Act
            var exception = Assert.Throws<DirectoryNotFoundException>(() => Validador.Valida(xml, null));

            //Assert
            Assert.IsInstanceOf<DirectoryNotFoundException>(exception);
        }

        [TestCase(1)] // Incluir Condutor Evento
        [TestCase(2)] // Cancelar Evento
        [TestCase(3)] // Encerramento Evento
        public void Deve_Recusar_A_Validacao_Por_XML_Incorreto_Para_Os_Eventos(int tipo)
        {
            //Arrange
            var mdfeDaoFalsa = new RepositorioDaoFalso();
            var evento = mdfeDaoFalsa.GetEvento(tipo);
            evento.InfEvento.ChMDFe = "";

            var xml = FuncoesXml.ClasseParaXmlString(evento);

            //Act
            var exception = Assert.Throws<Exception>(() => Validador.Valida(xml, _schema_eventos));

            //Arrange
            Assert.IsInstanceOf<Exception>(exception);
        }

        [TestCase(1)] // Incluir Condutor Evento
        [TestCase(2)] // Cancelar Evento
        [TestCase(3)] // Encerramento Evento
        public void Deve_Recusar_A_Validacao_Por_Schema_Incorreto_Para_Os_Eventos(int tipo)
        {
            //Arrange
            var mdfeDaoFalsa = new RepositorioDaoFalso();
            var evento = mdfeDaoFalsa.GetEvento(tipo);
            var xml = FuncoesXml.ClasseParaXmlString(evento);

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
