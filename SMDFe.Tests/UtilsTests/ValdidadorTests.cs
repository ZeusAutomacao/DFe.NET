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
using SMDFe.Utils.Flags;
using SMDFe.Utils.Validacao;

namespace SMDFe.Tests.UtilsTests
{
    [TestFixture]
    public class ValdidadorTests
    {
        private Configuracao _configuracao;

        #region SETUP
        [SetUp]
        public void CriarConfiguração()
        {
            var configuracaoDao = new ConfiguracaoDao();
            _configuracao = configuracaoDao.GetConfiguracao();

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

            var schema = "consStatServMDFe_v3.00.xsd";

            //Act
            Validador.Valida(xmlEnvio.XmlString(), schema);

            //Assert
            Assert.That(true);


        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Xml_Para_StatusMDFe()
        {
            //Arrange
            var schema = "consStatServMDFe_v3.00.xsd";

            //Act
            var exception = Assert.Throws<ArgumentNullException>(() => Validador.Valida(null, schema));

            //Assert
            Assert.IsTrue(exception.Message.Contains("Value cannot be null"));

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
            var exception = Assert.Throws<DirectoryNotFoundException>(() => Validador.Valida(xmlEnvio.XmlString(), null));

            //Assert
            Assert.IsTrue(exception.Message.Contains("Could not find"));
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_XML_Incorreto_Para_StatusMDFe()
        {
            //Arrange
            var xmlEnvio = new MDFeConsStatServMDFe();
            var schema = "consStatServMDFe_v3.00.xsd";

            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => Validador.Valida(xmlEnvio.XmlString(), schema));

            //Arrange
            Assert.IsTrue(exception.Message.Contains(""));
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

            var schema = "consStatServMD_v3.00.xsd";

            //Act
            var exception = Assert.Throws<FileNotFoundException>(() => Validador.Valida(xmlEnvio.XmlString(), schema));

            //Assert
            Assert.IsTrue(exception.Message.Contains("Could not find"));
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

            var schema = "consMDFeNaoEnc_v3.00.xsd";

            //Act
            Validador.Valida(xmlEnvio.XmlString(), schema);

            //Assert
            Assert.That(true);


        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Xml_Para_ConsultasNaoEncerradas()
        {
            //Arrange
            var schema = "consMDFeNaoEnc_v3.00.xsd";

            //Act
            var exception = Assert.Throws<ArgumentNullException>(() => Validador.Valida(null, schema));

            //Assert
            Assert.IsTrue(exception.Message.Contains("Value cannot be null"));

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
            Assert.IsTrue(exception.Message.Contains("Could not find"));
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_XML_Incorreto_Para_ConsultasNaoEncerradas()
        {
            //Arrange
            var xmlEnvio = new MDFeCosMDFeNaoEnc();
            var schema = "consMDFeNaoEnc_v3.00.xsd";

            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => Validador.Valida(xmlEnvio.XmlString(), schema));

            //Arrange
            Assert.IsTrue(exception.Message.Contains(""));
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

            var schema = "consMDFeNaoEnc_v0.00.xsd";

            //Act
            var exception = Assert.Throws<FileNotFoundException>(() => Validador.Valida(xmlEnvio.XmlString(), schema));

            //Assert
            Assert.IsTrue(exception.Message.Contains("Could not find"));
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

            var schema = "consReciMdfe_v3.00.xsd";

            //Act
            Validador.Valida(xmlEnvio.XmlString(), schema);

            //Assert
            Assert.That(true);


        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Xml_Para_ConsultaPorRecibos()
        {
            //Arrange
            var schema = "consReciMdfe_v3.00.xsd";

            //Act
            var exception = Assert.Throws<ArgumentNullException>(() => Validador.Valida(null, schema));

            //Assert
            Assert.IsTrue(exception.Message.Contains("Value cannot be null"));

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
            Assert.IsTrue(exception.Message.Contains("Could not find"));
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_XML_Incorreto_Para_ConsultaPorRecibo()
        {
            //Arrange
            var xmlEnvio = new MDFeConsReciMDFe();
            var schema = "consReciMdfe_v3.00.xsd";

            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => Validador.Valida(xmlEnvio.XmlString(), schema));

            //Arrange
            Assert.IsTrue(exception.Message.Contains(""));
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

            var schema = "consReciMdfe_v0.00.xsd";

            //Act
            var exception = Assert.Throws<FileNotFoundException>(() => Validador.Valida(xmlEnvio.XmlString(), schema));

            //Assert
            Assert.IsTrue(exception.Message.Contains("Could not find"));
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

            var schema = "consSitMdfe_v3.00.xsd";

            //Act
            Validador.Valida(xmlEnvio.XmlString(), schema);

            //Assert
            Assert.That(true);


        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Xml_Para_Consulta_Por_Protocolo()
        {
            //Arrange
            var schema = "consSitMdfe_v3.00.xsd";

            //Act
            var exception = Assert.Throws<ArgumentNullException>(() => Validador.Valida(null, schema));

            //Assert
            Assert.IsTrue(exception.Message.Contains("Value cannot be null"));

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
            Assert.IsTrue(exception.Message.Contains("Could not find"));
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_XML_Incorreto_Para_Consulta_Por_Protocolo()
        {
            //Arrange
            var xmlEnvio = new MDFeConsSitMDFe();
            var schema = "consSitMdfe_v3.00.xsd";

            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => Validador.Valida(xmlEnvio.XmlString(), schema));

            //Arrange
            Assert.IsTrue(exception.Message.Contains(""));
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

            var schema = "consSitMdf_v3.00.xsd";

            //Act
            var exception = Assert.Throws<FileNotFoundException>(() => Validador.Valida(xmlEnvio.XmlString(), schema));

            //Assert
            Assert.IsTrue(exception.Message.Contains("Could not find"));
        }

        #endregion

        #region Testes para a validação dos Schemas MDFe

        // <--------------------------------------------- MDFe testes --------------------------------------------->
        [Test]
        public void Deve_Validar_O_Xml_Com_Nome_E_Schema_Corretos_Para_MDFe()
        {

            //Arrange
            var mdfeDaoFalsa = new MDfeEletronicaDaoFalsa();
            var mdfe = mdfeDaoFalsa.GetMdFeEletronica();
            var schema = "MDFe_v3.00.xsd";
            var xml = FuncoesXml.ClasseParaXmlString(mdfe);

            //Act
            Validador.Valida(xml, schema);

            //Assert
            Assert.That(true);
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Xml_Para_MDFe()
        {
            //Arrange
            var schema = "MDFe_v3.00.xsd";

            //Act
            var exception = Assert.Throws<ArgumentNullException>(() => Validador.Valida(null, schema));

            //Assert
            Assert.IsTrue(exception.Message.Contains("Value cannot be null"));

        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Schema_Para_MDFe()
        {
            //Arrange
            var mdfeDaoFalsa = new MDfeEletronicaDaoFalsa();
            var mdfe = mdfeDaoFalsa.GetMdFeEletronica();
            var xml = FuncoesXml.ClasseParaXmlString(mdfe);


            //Act
            var exception = Assert.Throws<DirectoryNotFoundException>(() => Validador.Valida(xml, null));

            //Assert
            Assert.IsTrue(exception.Message.Contains("Could not find"));
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_XML_Incorreto_Para_MDFe()
        {
            //Arrange
            var mdfeDaoFalsa = new MDfeEletronicaDaoFalsa();
            var mdfe = mdfeDaoFalsa.GetMdFeEletronica();
            mdfe.InfMDFe.Id = "";

            var schema = "MDFe_v3.00.xsd";

            var xml = FuncoesXml.ClasseParaXmlString(mdfe);

            //Act
            var exception = Assert.Throws<Exception>(() => Validador.Valida(xml, schema));

            //Arrange
            Assert.IsTrue(exception.Message.Contains(""));
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Schema_Incorreto_Para_MDFe()
        {
            //Arrange
            var mdfeDaoFalsa = new MDfeEletronicaDaoFalsa();
            var mdfe = mdfeDaoFalsa.GetMdFeEletronica();
            var schema = "MDFe_v3.00.xd";
            var xml = FuncoesXml.ClasseParaXmlString(mdfe);

            //Act
            var exception = Assert.Throws<FileNotFoundException>(() => Validador.Valida(xml, schema));

            //Assert
            Assert.IsTrue(exception.Message.Contains("Could not find"));

        }

        // <--------------------------------------------- EnviMDFe testes --------------------------------------------->
        [Test]
        public void Deve_Validar_O_Xml_Com_Nome_E_Schema_Corretos_Para_EnviMDFe()
        {

            //Arrange
            var envimdfeDaoFalsa = new MDfeEletronicaDaoFalsa();
            var envi_mdfe = envimdfeDaoFalsa.GetEnviMdFe();
            var schema = "enviMDFe_v3.00.xsd";
            var xml = FuncoesXml.ClasseParaXmlString(envi_mdfe);

            //Act
            Validador.Valida(xml, schema);

            //Assert
            Assert.That(true);

        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Xml_Para_EnviMDFe()
        {
            //Arrange
            var schema = "enviMDFe_v3.00.xsd";

            //Act
            var exception = Assert.Throws<ArgumentNullException>(() => Validador.Valida(null, schema));

            //Assert
            Assert.IsTrue(exception.Message.Contains("Value cannot be null"));

        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Schema_Para_EnviMDFe()
        {
            //Arrange
            var envimdfeDaoFalsa = new MDfeEletronicaDaoFalsa();
            var envi_mdfe = envimdfeDaoFalsa.GetEnviMdFe();
            var xml = FuncoesXml.ClasseParaXmlString(envi_mdfe);


            //Act
            var exception = Assert.Throws<DirectoryNotFoundException>(() => Validador.Valida(xml, null));

            //Assert
            Assert.IsTrue(exception.Message.Contains("Could not find"));
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_XML_Incorreto_Para_EnviMDFe()
        {
            //Arrange
            var envimdfeDaoFalsa = new MDfeEletronicaDaoFalsa();
            var envi_mdfe = envimdfeDaoFalsa.GetEnviMdFe();
            envi_mdfe.IdLote = "";

            var schema = "enviMDFe_v3.00.xsd";

            var xml = FuncoesXml.ClasseParaXmlString(envi_mdfe);

            //Act
            var exception = Assert.Throws<Exception>(() => Validador.Valida(xml, schema));

            //Arrange
            Assert.IsTrue(exception.Message.Contains(""));
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Schema_Incorreto_Para_EnviMDFe()
        {
            //Arrange
            var envimdfeDaoFalsa = new MDfeEletronicaDaoFalsa();
            var envi_mdfe = envimdfeDaoFalsa.GetEnviMdFe();
            var schema = "enviMDFe_v3.00.d";
            var xml = FuncoesXml.ClasseParaXmlString(envi_mdfe);

            //Act
            var exception = Assert.Throws<FileNotFoundException>(() => Validador.Valida(xml, schema));

            //Assert
            Assert.IsTrue(exception.Message.Contains("Could not find"));

        }

        // <--------------------------------------------- InfoModal testes --------------------------------------------->
        [Test]
        public void Deve_Validar_O_Xml_Com_Nome_E_Schema_Corretos_Para_InfoModal()
        {

            //Arrange
            var envimdfeDaoFalsa = new MDfeEletronicaDaoFalsa();
            var infoModal = envimdfeDaoFalsa.GetEnviMdFe().MDFe.InfMDFe.InfModal;

            var schema = "MDFeModalRodoviario_v3.00.xsd";

            var xml = FuncoesXml.ClasseParaXmlString(infoModal);

            //Act
            Validador.Valida(xml, schema);

            //Assert
            Assert.That(true);

        }


        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Xml_Para_InfoModal()
        {
            //Arrange
            var schema = "MDFeModalRodoviario_v3.00.xsd";

            //Act
            var exception = Assert.Throws<ArgumentNullException>(() => Validador.Valida(null, schema));

            //Assert
            Assert.IsTrue(exception.Message.Contains("Value cannot be null"));

        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Schema_Para_InfoModal()
        {
            //Arrange
            var envimdfeDaoFalsa = new MDfeEletronicaDaoFalsa();
            var infoModal = envimdfeDaoFalsa.GetEnviMdFe().MDFe.InfMDFe.InfModal;
            var xml = FuncoesXml.ClasseParaXmlString(infoModal);


            //Act
            var exception = Assert.Throws<DirectoryNotFoundException>(() => Validador.Valida(xml, null));

            //Assert
            Assert.IsTrue(exception.Message.Contains("Could not find"));
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_XML_Incorreto_Para_InfoModal()
        {
            //Arrange
            var envimdfeDaoFalsa = new MDfeEletronicaDaoFalsa();
            var infoModal = envimdfeDaoFalsa.GetEnviMdFe().MDFe.InfMDFe.InfModal;
            infoModal.Modal = new MDFeRodo();

            var schema = "MDFeModalRodoviario_v3.00.xsd";
            var xml = FuncoesXml.ClasseParaXmlString(infoModal);


            //Act
            var exception = Assert.Throws<Exception>(() => Validador.Valida(xml, schema));

            //Arrange
            Assert.IsTrue(exception.Message.Contains(""));
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Schema_Incorreto_Para_InfoModal()
        {
            //Arrange
            var envimdfeDaoFalsa = new MDfeEletronicaDaoFalsa();
            var infoModal = envimdfeDaoFalsa.GetEnviMdFe().MDFe.InfMDFe.InfModal;

            var schema = "MDFeModalRodoviario_v3.00.xd";

            var xml = FuncoesXml.ClasseParaXmlString(infoModal);

            //Act
            var exception = Assert.Throws<FileNotFoundException>(() => Validador.Valida(xml, schema));

            //Assert
            Assert.IsTrue(exception.Message.Contains("Could not find"));

        }

        #endregion

        #region Testes para a validação do Schema Eventos
        //<------------------------------------------------- Evento Incluir Condutor ----------------------------------------------------->
        [Test]
        public void Deve_Validar_O_Xml_Com_Nome_E_Schema_Corretos_Para_EventoIncluirCondutor()
        {

            //Arrange
            var mdfeDaoFalsa = new MDfeEletronicaDaoFalsa();
            var evento = mdfeDaoFalsa.GetEvento(1);
            var schema = "eventoMDFe_v3.00.xsd";
            var xml = FuncoesXml.ClasseParaXmlString(evento);

            //Act
            Validador.Valida(xml, schema);

            //Assert
            Assert.That(true);
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Xml_Para_EventoIncluir()
        {
            //Arrange
            var schema = "eventoMDFe_v3.00.xsd";

            //Act
            var exception = Assert.Throws<ArgumentNullException>(() => Validador.Valida(null, schema));

            //Assert
            Assert.IsTrue(exception.Message.Contains("Value cannot be null"));

        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Schema_Para_EventoIncluir()
        {
            //Arrange
            var mdfeDaoFalsa = new MDfeEletronicaDaoFalsa();
            var evento = mdfeDaoFalsa.GetEvento(1);
            var xml = FuncoesXml.ClasseParaXmlString(evento);


            //Act
            var exception = Assert.Throws<DirectoryNotFoundException>(() => Validador.Valida(xml, null));

            //Assert
            Assert.IsTrue(exception.Message.Contains("Could not find"));
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_XML_Incorreto_Para_EventoIncluir()
        {
            //Arrange
            var mdfeDaoFalsa = new MDfeEletronicaDaoFalsa();
            var evento = mdfeDaoFalsa.GetEvento(1);
            evento.InfEvento.ChMDFe = "";

            var xml = FuncoesXml.ClasseParaXmlString(evento);

            var schema = "eventoMDFe_v3.00.xsd";

            

            //Act
            var exception = Assert.Throws<Exception>(() => Validador.Valida(xml, schema));

            //Arrange
            Assert.IsTrue(exception.Message.Contains(""));
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Schema_Incorreto_Para_EventoIncluir()
        {
            //Arrange
            var mdfeDaoFalsa = new MDfeEletronicaDaoFalsa();
            var evento = mdfeDaoFalsa.GetEvento(1);
            var schema = "eventoMDFe_v3.xsd";
            var xml = FuncoesXml.ClasseParaXmlString(evento);

            //Act
            var exception = Assert.Throws<FileNotFoundException>(() => Validador.Valida(xml, schema));

            //Assert
            Assert.IsTrue(exception.Message.Contains("Could not find"));

        }
        //<----------------------------------------------------- Evento Cancelar --------------------------------------------------------->
        [Test]
        public void Deve_Validar_O_Xml_Com_Nome_E_Schema_Corretos_Para_EventoCancelar()
        {

            //Arrange
            var mdfeDaoFalsa = new MDfeEletronicaDaoFalsa();
            var evento = mdfeDaoFalsa.GetEvento(2);
            var schema = "eventoMDFe_v3.00.xsd";
            var xml = FuncoesXml.ClasseParaXmlString(evento);

            //Act
            Validador.Valida(xml, schema);

            //Assert
            Assert.That(true);
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Xml_Para_EventoCancelar()
        {
            //Arrange
            var schema = "eventoMDFe_v3.00.xsd";

            //Act
            var exception = Assert.Throws<ArgumentNullException>(() => Validador.Valida(null, schema));

            //Assert
            Assert.IsTrue(exception.Message.Contains("Value cannot be null"));

        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Schema_Para_EventoCancelar()
        {
            //Arrange
            var mdfeDaoFalsa = new MDfeEletronicaDaoFalsa();
            var evento = mdfeDaoFalsa.GetEvento(2);
            var xml = FuncoesXml.ClasseParaXmlString(evento);


            //Act
            var exception = Assert.Throws<DirectoryNotFoundException>(() => Validador.Valida(xml, null));

            //Assert
            Assert.IsTrue(exception.Message.Contains("Could not find"));
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_XML_Incorreto_Para_EventoCancelar()
        {
            //Arrange
            var mdfeDaoFalsa = new MDfeEletronicaDaoFalsa();
            var evento = mdfeDaoFalsa.GetEvento(2);
            evento.InfEvento.ChMDFe = "";

            var xml = FuncoesXml.ClasseParaXmlString(evento);

            var schema = "eventoMDFe_v3.00.xsd";



            //Act
            var exception = Assert.Throws<Exception>(() => Validador.Valida(xml, schema));

            //Arrange
            Assert.IsTrue(exception.Message.Contains(""));
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Schema_Incorreto_Para_EventoCancelar()
        {
            //Arrange
            var mdfeDaoFalsa = new MDfeEletronicaDaoFalsa();
            var evento = mdfeDaoFalsa.GetEvento(2);
            var schema = "eventoMDFe_v3.xsd";
            var xml = FuncoesXml.ClasseParaXmlString(evento);

            //Act
            var exception = Assert.Throws<FileNotFoundException>(() => Validador.Valida(xml, schema));

            //Assert
            Assert.IsTrue(exception.Message.Contains("Could not find"));

        }

        //<---------------------------------------------------- Evento Encerramento ------------------------------------------------------>

        [Test]
        public void Deve_Validar_O_Xml_Com_Nome_E_Schema_Corretos_Para_EventoEncerramento()
        {

            //Arrange
            var mdfeDaoFalsa = new MDfeEletronicaDaoFalsa();
            var evento = mdfeDaoFalsa.GetEvento(3);
            var schema = "eventoMDFe_v3.00.xsd";
            var xml = FuncoesXml.ClasseParaXmlString(evento);

            //Act
            Validador.Valida(xml, schema);

            //Assert
            Assert.That(true);
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Xml_Para_EventoEncerramento()
        {
            //Arrange
            var schema = "eventoMDFe_v3.00.xsd";

            //Act
            var exception = Assert.Throws<ArgumentNullException>(() => Validador.Valida(null, schema));

            //Assert
            Assert.IsTrue(exception.Message.Contains("Value cannot be null"));

        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Do_Schema_Para_EventoEncerramento()
        {
            //Arrange
            var mdfeDaoFalsa = new MDfeEletronicaDaoFalsa();
            var evento = mdfeDaoFalsa.GetEvento(3);
            var xml = FuncoesXml.ClasseParaXmlString(evento);


            //Act
            var exception = Assert.Throws<DirectoryNotFoundException>(() => Validador.Valida(xml, null));

            //Assert
            Assert.IsTrue(exception.Message.Contains("Could not find"));
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_XML_Incorreto_Para_EventoEncerramento()
        {
            //Arrange
            var mdfeDaoFalsa = new MDfeEletronicaDaoFalsa();
            var evento = mdfeDaoFalsa.GetEvento(3);
            evento.InfEvento.ChMDFe = "";

            var xml = FuncoesXml.ClasseParaXmlString(evento);

            var schema = "eventoMDFe_v3.00.xsd";



            //Act
            var exception = Assert.Throws<Exception>(() => Validador.Valida(xml, schema));

            //Arrange
            Assert.IsTrue(exception.Message.Contains(""));
        }

        [Test]
        public void Deve_Recusar_A_Validacao_Por_Schema_Incorreto_Para_EventoEncerramento()
        {
            //Arrange
            var mdfeDaoFalsa = new MDfeEletronicaDaoFalsa();
            var evento = mdfeDaoFalsa.GetEvento(3);
            var schema = "eventoMDFe_v3.xsd";
            var xml = FuncoesXml.ClasseParaXmlString(evento);

            //Act
            var exception = Assert.Throws<FileNotFoundException>(() => Validador.Valida(xml, schema));

            //Assert
            Assert.IsTrue(exception.Message.Contains("Could not find"));

        }

        #endregion


        [Test]
        public void Deve_Recusar_A_Validacao_Por_Falta_Dos_Parametros()
        {
            //Arrange

            //Act
            var exception = Assert.Throws<DirectoryNotFoundException>(() => Validador.Valida(null, null));

            //Assert
            Assert.IsTrue(exception.Message.Contains("Could not find"));
        }
    }
}
