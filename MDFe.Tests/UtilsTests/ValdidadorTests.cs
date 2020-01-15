using System;
using DFe.Utils;
using MDFe.Classes.Extensoes;
using MDFe.Classes.Informacoes;
using MDFe.Classes.Informacoes.ConsultaNaoEncerrados;
using MDFe.Classes.Informacoes.ConsultaProtocolo;
using MDFe.Classes.Informacoes.Evento;
using MDFe.Classes.Informacoes.Evento.CorpoEvento;
using MDFe.Classes.Informacoes.Evento.Flags;
using MDFe.Classes.Informacoes.RetRecepcao;
using MDFe.Classes.Informacoes.StatusServico;
using MDFe.Classes.Servicos.Autorizacao;
using MDFe.Servicos.EventosMDFe;
using MDFe.Tests.Dao;
using MDFe.Tests.Entidades;
using MDFe.Utils.Flags;
using MDFe.Utils.Validacao;
using Xunit;

namespace MDFe.Tests.UtilsTests
{
    
    public class ValdidadorTests:IDisposable
    {
        #region Variáveis
        private Configuracao _configuracao;
        private readonly string _schema_status;
        private readonly string _schema_encerradas;
        private readonly string _schema_recibo;
        private readonly string _schema_protocolo;
        private readonly string _schema_mdfe;
        private readonly string _schema_enviMdfe;
        private readonly string _schema_modalMdfe;
        private readonly string _schema_eventos;
        private readonly string _schema_incorreto;
        private MDFeEletronicaFalsa _RepositorioFalsoMdfe;
        private Classes.Informacoes.MDFe _mdfe;
        private MDFeEnviMDFe _enviMdFe;
        private MDFeCondutorIncluir _condutor;
        private MDFeEventoMDFe _evento;
        private readonly string _protocolo;
        private readonly string _justificativa;
        private readonly string _recibo;
        private readonly string _chaveMDFe;
        private readonly string _lote;

        #endregion

        #region SETUP
        public ValdidadorTests()
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
            _recibo = "000000000000000";
            _chaveMDFe = "00000000000000000000000000000000000000000000";
            _lote = "1";   

            _RepositorioFalsoMdfe = new MDFeEletronicaFalsa(_configuracao.Empresa);
            _mdfe = _RepositorioFalsoMdfe.GetMdfe();
            _condutor = new MDFeCondutorIncluir() { CPF = "00000000000", XNome = "NINGUEM" };

            var configcertificado = new CertificadoDao().getConfiguracaoCertificado();

            var configuracoes = new ConfiguracaoUtilsDao(_configuracao, configcertificado);
            configuracoes.setCongiguracoes();
        }

        public void Dispose()
        {
            _RepositorioFalsoMdfe = new MDFeEletronicaFalsa(_configuracao.Empresa);
            _mdfe = _RepositorioFalsoMdfe.GetMdfe();
            _enviMdFe = new MDFeEnviMDFe();
        }
        #endregion

        #region Testes para a validação do Schema Status

        [Fact]
        public void Deve_Validar_O_Xml_Com_Nome_E_Schema_Corretos_Para_StatusMDFe()
        {
            //Arrange
            var xmlEnvio = new MDFeConsStatServMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout
            };
            
            //Act
            var result = Record.Exception(() => Validador.Valida(xmlEnvio.XmlString(), _schema_status));

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_Falta_Do_Status_Para_Requisicao_StatusMDFe()
        {

            //Arrange
            var xmlEnvio = new MDFeConsStatServMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                XServ = null
            };

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xmlEnvio.XmlString(), _schema_status));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_Falta_Do_Xml_Para_Requisicao_StatusMDFe()
        {
            //Arrange

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(null, _schema_status));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_Falta_Do_Schema_Para_Requisicao_StatusMDFe()
        {
            //Arrange
            var xmlEnvio = new MDFeConsStatServMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout
            };

            //Act
            var exception =
                Assert.ThrowsAny<Exception>(() => Validador.Valida(xmlEnvio.XmlString(), null));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_XML_Incorreto_Para_Requisicao_StatusMDFe()
        {
            //Arrange
            var xmlEnvio = new MDFeConsStatServMDFe();
            
            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xmlEnvio.XmlString(), _schema_status));

            //Arrange
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_Schema_Incorreto_Para_Requisicao_StatusMDFe()
        {
            //Arrange
            var xmlEnvio = new MDFeConsStatServMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout
            };

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xmlEnvio.XmlString(), _schema_incorreto));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        #endregion

        #region Testes para a validação do Schema Consultas Não Encerradas

        [Fact]
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
            var result = Record.Exception(() => Validador.Valida(xmlEnvio.XmlString(), _schema_encerradas));

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_Falta_Do_CNPJ_Para_Requisicao_ConsultasNaoEncerradas()
        {
            //Arrange
            var xmlEnvio = new MDFeCosMDFeNaoEnc()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                CNPJ = null
            };

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xmlEnvio.XmlString(), _schema_encerradas));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_Falta_Do_Campo_Consulta_Para_Requisicao_ConsultasNaoEncerradas()
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
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xmlEnvio.XmlString(), _schema_encerradas));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_Falta_Do_Xml_Para_Requisicao_ConsultasNaoEncerradas()
        {
            //Arrange

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(null, _schema_encerradas));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_Falta_Do_Schema_Para_Requisicao_ConsultasNaoEncerradas()
        {
            //Arrange
            var xmlEnvio = new MDFeCosMDFeNaoEnc()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                CNPJ = _configuracao.Empresa.Cnpj
            };

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xmlEnvio.XmlString(), null));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_XML_Vazio_Para_Requisicao_ConsultasNaoEncerradas()
        {
            //Arrange
            var xmlEnvio = new MDFeCosMDFeNaoEnc();

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xmlEnvio.XmlString(), _schema_encerradas));

            //Arrange
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void DDeve_Gerar_Uma_Excecao_Na_Validacao_Por_Schema_Incorreto_Para_Requisicao_ConsultasNaoEncerradas()
        {
            //Arrange
            var xmlEnvio = new MDFeCosMDFeNaoEnc()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                CNPJ = _configuracao.Empresa.Cnpj
            };

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xmlEnvio.XmlString(), _schema_incorreto));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        #endregion

        #region Testes para a validação do Schema Consultas por Recibo

        [Fact]
        public void Deve_Validar_O_Xml_Com_Nome_E_Schema_Corretos_Para_ConsultaPorRecibo()
        {
            //Arrange
            var xmlEnvio = new MDFeConsReciMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                NRec = _recibo
            };
            
            //Act
            var result = Record.Exception(() => Validador.Valida(xmlEnvio.XmlString(), _schema_recibo));

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_Falta_Do_Recibo_Para_Requisicao_ConsultaPorRecibo()
        {
            //Arrange
            var xmlEnvio = new MDFeConsReciMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                NRec = null
            };

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xmlEnvio.XmlString(), _schema_recibo));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_Falta_Do_Xml_Para_Requisicao_ConsultaPorRecibos()
        {
            //Arrange

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(null, _schema_recibo));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_Falta_Do_Schema_Para_Requisicao_ConsultaPorRecibo()
        {
            //Arrange
            var xmlEnvio = new MDFeConsReciMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                NRec = _recibo
            };

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xmlEnvio.XmlString(), null));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_XML_Incorreto_Para_Requisicao_ConsultaPorRecibo()
        {
            //Arrange
            var xmlEnvio = new MDFeConsReciMDFe();

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xmlEnvio.XmlString(), _schema_recibo));

            //Arrange
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_Schema_Incorreto_Para_Requisicao_ConsultaPorRecibo()
        {
            //Arrange
            var xmlEnvio = new MDFeConsReciMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                NRec = _recibo
            };

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xmlEnvio.XmlString(), _schema_incorreto));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        #endregion

        #region Testes para a validação do Schema Consultas Por Protocolo

        [Fact]
        public void Deve_Validar_O_Xml_Com_Nome_E_Schema_Corretos_Para_Consulta_Por_Protocolo()
        {
            //Arrange
            var xmlEnvio = new MDFeConsSitMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                ChMDFe = _chaveMDFe
            };

            //Act
            var result = Record.Exception(() => Validador.Valida(xmlEnvio.XmlString(), _schema_protocolo));

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_Falta_Do_Protocolo_Para_Requisicao_Por_Protocolo()
        {
            //Arrange
            var xmlEnvio = new MDFeConsSitMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                ChMDFe = null
            };

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xmlEnvio.XmlString(), _schema_protocolo));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_Falta_Do_Servico_Para_Requisicao_Por_Protocolo()
        {
            //Arrange
            var xmlEnvio = new MDFeConsSitMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                ChMDFe = _chaveMDFe,
                XServ = null
            };

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xmlEnvio.XmlString(), _schema_protocolo));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_Falta_Do_Xml_Para_Requisicao_Por_Protocolo()
        {
            //Arrange
            
            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(null, _schema_protocolo));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_Falta_Do_Schema_Para_Requisicao_Por_Protocolo()
        {
            //Arrange
            var xmlEnvio = new MDFeConsSitMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                ChMDFe = _chaveMDFe
            };

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xmlEnvio.XmlString(), null));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_XML_Incorreto_Para_Requisicao_Por_Protocolo()
        {
            //Arrange
            var xmlEnvio = new MDFeConsSitMDFe();
            
            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xmlEnvio.XmlString(), _schema_protocolo));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_Schema_Incorreto_Para_Requisicao_Por_Protocolo()
        {
            //Arrange
            var xmlEnvio = new MDFeConsSitMDFe()
            {
                TpAmb = _configuracao.ConfigWebService.Ambiente,
                Versao = _configuracao.ConfigWebService.VersaoLayout,
                ChMDFe = _chaveMDFe
            };

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xmlEnvio.XmlString(), _schema_incorreto));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        #endregion

        #region Testes para a validação dos Schemas MDFe

        // <--------------------------------------------- MDFe testes --------------------------------------------->
        [Fact]
        public void Deve_Validar_O_Xml_Com_Nome_E_Schema_Corretos_Para_MDFe()
        {
            //Arrange
            if(_mdfe != null) Dispose();
            _mdfe.Assina();
            var xml = FuncoesXml.ClasseParaXmlString(_mdfe);

            //Act
            var result = Record.Exception(() =>Validador.Valida(xml, _schema_mdfe));

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_Falta_Do_Xml_Para_MDFe()
        {
            //Arrange

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(null, _schema_mdfe));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_Falta_Do_Schema_Para_MDFe()
        {
            //Arrange
            if (_mdfe != null) Dispose();
            _mdfe.Assina();
            var xml = FuncoesXml.ClasseParaXmlString(_mdfe);
            
            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xml, null));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_XML_Incorreto_Para_MDFe()
        {
            //Arrange
            if (_mdfe != null) Dispose();
            _mdfe.Assina();
            _mdfe.InfMDFe.Id = "";

            var xml = FuncoesXml.ClasseParaXmlString(_mdfe);

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xml, _schema_mdfe));

            //Arrange
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_Schema_Incorreto_Para_MDFe()
        {
            //Arrange
            if (_mdfe != null) Dispose();
            _mdfe.Assina();
            var xml = FuncoesXml.ClasseParaXmlString(_mdfe);

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xml, _schema_incorreto));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        // <--------------------------------------------- EnviMDFe testes --------------------------------------------->
        [Fact]
        public void Deve_Validar_O_Xml_Com_Nome_E_Schema_Corretos_Para_EnviMDFe()
        {
            //Arrange
            _enviMdFe = new MDFeEnviMDFe()
            {
                Versao = VersaoServico.Versao300,
                MDFe = _mdfe,
                IdLote = _lote
            };

            _enviMdFe.MDFe.Assina();
            var xml = FuncoesXml.ClasseParaXmlString(_enviMdFe);

            //Act
            var result = Record.Exception(() => Validador.Valida(xml, _schema_enviMdfe));

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_Falta_Do_Xml_Para_EnviMDFe()
        {
            //Arrange

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(null, _schema_enviMdfe));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_Falta_Do_Schema_Para_EnviMDFe()
        {
            //Arrange
            _enviMdFe = new MDFeEnviMDFe()
            {
                Versao = VersaoServico.Versao300,
                MDFe = _mdfe,
                IdLote = _lote
            };

            _enviMdFe.MDFe.Assina();
            var xml = FuncoesXml.ClasseParaXmlString(_enviMdFe);
            
            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xml, null));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_XML_Incorreto_Para_EnviMDFe()
        {
            //Arrange
            _enviMdFe = new MDFeEnviMDFe()
            {
                Versao = VersaoServico.Versao300,
                MDFe = _mdfe,
                IdLote = null
            };

            _mdfe.Assina();
            var xml = FuncoesXml.ClasseParaXmlString(_enviMdFe);

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xml, _schema_enviMdfe));

            //Arrange
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_Schema_Incorreto_Para_EnviMDFe()
        {
            //Arrange
            _enviMdFe = new MDFeEnviMDFe()
            {
                Versao = VersaoServico.Versao300,
                MDFe = _mdfe,
                IdLote = _lote
            };

            _enviMdFe.MDFe.Assina();
            var xml = FuncoesXml.ClasseParaXmlString(_enviMdFe);

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xml, _schema_incorreto));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        // <--------------------------------------------- InfoModal testes --------------------------------------------->
        [Fact]
        public void Deve_Validar_O_Xml_Com_Nome_E_Schema_Corretos_Para_InfoModal()
        {
            //Arrange
            if(_mdfe != null) Dispose();
            _mdfe.Assina();
            var infoModal = _mdfe.InfMDFe.InfModal;
            var xml = FuncoesXml.ClasseParaXmlString(infoModal);

            //Act
            var result = Record.Exception(() => Validador.Valida(xml, _schema_modalMdfe));

            //Assert
            Assert.Null(result);

        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_Falta_Do_Xml_Para_InfoModal()
        {
            //Arrange

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(null, _schema_modalMdfe));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_Falta_Do_Schema_Para_InfoModal()
        {
            //Arrange
            var infoModal = _mdfe.InfMDFe.InfModal;
            var xml = FuncoesXml.ClasseParaXmlString(infoModal);


            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xml, null));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_XML_Incorreto_Para_InfoModal()
        {
            //Arrange
            var infoModal = _mdfe.InfMDFe.InfModal;
            infoModal.Modal = new MDFeRodo();

            var xml = FuncoesXml.ClasseParaXmlString(infoModal);
            
            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xml, _schema_modalMdfe));

            //Arrange
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_Schema_Incorreto_Para_InfoModal()
        {
            //Arrange
            var infoModal = _mdfe.InfMDFe.InfModal;
            var xml = FuncoesXml.ClasseParaXmlString(infoModal);

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xml, _schema_incorreto));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        #endregion

        #region Testes para a validação do Schema Eventos

        //<<================================================ Incluir Condutor Evento ==============================================>>

        [Fact] 
        public void Deve_Validar_O_Xml_Com_Nome_E_Schema_Corretos_Para_Incluir_Condutor()
        {
            //Arrange
            if(_mdfe != null) Dispose();
            var condutor = new MDFeEvIncCondutorMDFe()
            {
                Condutor = _condutor
            };

            _mdfe.Assina();
            _evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.InclusaoDeCondutor, 1, condutor);
            var xml = FuncoesXml.ClasseParaXmlString(_evento);
            
            //Act
            var result = Record.Exception(() =>Validador.Valida(xml, _schema_eventos));

            //Assert
            Assert.Null(result);
        }

        [Fact] 
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_Falta_Do_Schema_Para_Incluir_Condutor()
        {
            //Arrange
            if (_mdfe != null) Dispose();
            var condutor = new MDFeEvIncCondutorMDFe()
            {
                Condutor = _condutor
            };

            _mdfe.Assina();
            _evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.InclusaoDeCondutor, 1, condutor);
            var xml = FuncoesXml.ClasseParaXmlString(_evento);

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xml, null));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact] 
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_XML_Incorreto_Para_Incluir_Evento()
        {
            //Arrange
            if (_mdfe != null) Dispose();
            var condutor = new MDFeEvIncCondutorMDFe()
            {
                Condutor = _condutor
            };

            _mdfe.Assina();
            _evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.InclusaoDeCondutor, 1, condutor);
            _evento.InfEvento.Id = null;
            var xml = FuncoesXml.ClasseParaXmlString(_evento);

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xml, _schema_eventos));

            //Arrange
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact] 
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_Schema_Incorreto_Para_Incluir_Condutor()
        {
            //Arrange
            if (_mdfe != null) Dispose();
            var condutor = new MDFeEvIncCondutorMDFe()
            {
                Condutor = _condutor
            };

            _mdfe.Assina();
            _evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.InclusaoDeCondutor, 1, condutor);
            var xml = FuncoesXml.ClasseParaXmlString(_evento);

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xml, _schema_incorreto));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);

        }

        //<<================================================ Encerrar Evento ==============================================>>

        [Fact] 
        public void Deve_Validar_O_Xml_Com_Nome_E_Schema_Corretos_Para_Encerrar_Evento()
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
            var xml = FuncoesXml.ClasseParaXmlString(_evento);

            //Act
            var result = Record.Exception(() =>Validador.Valida(xml, _schema_eventos));

            //Assert
            Assert.Null(result);
        }

        [Fact] 
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_Falta_Do_Schema_Para_Encerrar_Eventos()
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
            var xml = FuncoesXml.ClasseParaXmlString(_evento);

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xml, null));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact] 
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_XML_Incorreto_Para_Encerrar_Eventos()
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
            _evento.InfEvento.Id = null;
            var xml = FuncoesXml.ClasseParaXmlString(_evento);

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xml, _schema_eventos));

            //Arrange
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact] 
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_Schema_Incorreto_Para_Encerrar_Eventos()
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
            var xml = FuncoesXml.ClasseParaXmlString(_evento);

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xml, _schema_incorreto));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);

        }

        //<<================================================ Cancelar Evento ==============================================>>

        [Fact] 
        public void Deve_Validar_O_Xml_Com_Nome_E_Schema_Corretos_Para_Cancelar_Evento()
        {
            //Arrange
            if (_mdfe != null) Dispose();
            var cancelamento = new MDFeEvCancMDFe()
            {
                NProt = _protocolo,
                XJust = _justificativa
            };

            _mdfe.Assina();
            _evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.Cancelamento, 1, cancelamento);
            var xml = FuncoesXml.ClasseParaXmlString(_evento);

            //Act
            var result = Record.Exception(() => Validador.Valida(xml, _schema_eventos));

            //Assert
            Assert.Null(result);
        }

        [Fact] 
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_Falta_Do_Schema_Para_Cancelar_Evento()
        {
            //Arrange
            if (_mdfe != null) Dispose();
            var cancelamento = new MDFeEvCancMDFe()
            {
                NProt = _protocolo,
                XJust = _justificativa
            };

            _mdfe.Assina();
            _evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.Cancelamento, 1, cancelamento);
            var xml = FuncoesXml.ClasseParaXmlString(_evento);

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xml, null));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_XML_Incorreto_Para_Cancelar_Evento()
        {
            //Arrange
            if (_mdfe != null) Dispose();
            var cancelamento = new MDFeEvCancMDFe()
            {
                NProt = _protocolo,
                XJust = _justificativa
            };

            _mdfe.Assina();
            _evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.Cancelamento, 1, cancelamento);
            _evento.InfEvento.Id = null;
            var xml = FuncoesXml.ClasseParaXmlString(_evento);

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xml, _schema_eventos));

            //Arrange
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact] 
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_Schema_Incorreto_Para_Cancelar_Evento()
        {
            //Arrange
            if (_mdfe != null) Dispose();
            var cancelamento = new MDFeEvCancMDFe()
            {
                NProt = _protocolo,
                XJust = _justificativa
            };

            _mdfe.Assina();
            _evento = FactoryEvento.CriaEvento(_mdfe, MDFeTipoEvento.Cancelamento, 1, cancelamento);
            var xml = FuncoesXml.ClasseParaXmlString(_evento);

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(xml, _schema_incorreto));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);

        }

        //<<=========================================== Teste para Eventos de forma geral =======================================>>

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_Falta_Do_Xml_Para_O_Evento()
        {
            //Arrange
            
            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(null, _schema_eventos));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        #endregion

        #region Testes para a Validação de forma geral

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Na_Validacao_Por_Falta_Dos_Parametros()
        {
            //Arrange

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => Validador.Valida(null, null));

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        #endregion

    }
}
