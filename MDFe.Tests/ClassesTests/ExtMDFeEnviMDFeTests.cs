using System;
using System.Xml;
using MDFe.Classes.Extensoes;
using MDFe.Classes.Servicos.Autorizacao;
using MDFe.Tests.Dao;
using MDFe.Tests.Entidades;
using MDFe.Utils.Flags;
using Xunit;

namespace MDFe.Tests.ClassesTests
{
    
    public class ExtMDFeEnviMDFeTests: IDisposable
    {
        #region Variáveis
        private Configuracao _configuracao;
        private MDFeEletronicaFalsa _mdfe;
        private readonly string _xmlEsperado;
        private readonly string _lote;
        private readonly string _valueKey;
        #endregion

        #region SETUP
        
        public ExtMDFeEnviMDFeTests()
        {
            var configuracaoDao = new ConfiguracaoDao();

            _configuracao = configuracaoDao.GetConfiguracao();

            _mdfe = new MDFeEletronicaFalsa(_configuracao.Empresa);
            _xmlEsperado = "xml-esperado-envi-mdfe.xml";
            _lote = "1";
            _valueKey = "TESTE";

            var configcertificado = new CertificadoDao().getConfiguracaoCertificado();

            var configuracoes = new ConfiguracaoUtilsDao(_configuracao, configcertificado);
            configuracoes.setCongiguracoes();

        }

        public void Dispose()
        {
            _mdfe = new MDFeEletronicaFalsa(_configuracao.Empresa);
        }
        #endregion

        #region Testes para a classe ExtMDFeEnviMDFe 

        [Fact]
        public void Deve_Criar_Uma_Requisicao_Para_Envio_Mdfe_Com_Parametros_Validos()
        {
            //Arrange
            var enviMdFe = new MDFeEnviMDFe()
            {
                Versao = VersaoServico.Versao300,
                MDFe = _mdfe.GetMdfe(),
                IdLote = _lote
            };

            //Act
            var xmlGerado = enviMdFe.CriaXmlRequestWs();
            
            //Assert
            Assert.NotNull(xmlGerado);
            Assert.IsType<XmlDocument>(xmlGerado);
        }

        [Fact]
        public void Deve_Criar_Uma_Requisicao_Para_Envio_Mdfe_Nao_Nula()
        {
            //Arrange
            var enviMdFe = new MDFeEnviMDFe()
            {
                Versao = VersaoServico.Versao300,
                MDFe = _mdfe.GetMdfe(),
                IdLote = _lote
            };

            //Act
            var xmlGerado = enviMdFe.CriaXmlRequestWs();

            //Assert
            Assert.NotNull(xmlGerado);
        }

        [Fact]
        public void Deve_Validar_A_Requisicao_De_Envio_MDFe_Criada_Com_O_Xml_Esperado()
        {
            //Arrange
            if(_mdfe != null) Dispose();
            var mdfe = _mdfe.GetMdfe();
            mdfe.Assina();
            mdfe.Signature.SignatureValue = _valueKey;
            mdfe.Signature.SignedInfo.Reference.DigestValue = _valueKey;
            mdfe.Signature.KeyInfo.X509Data.X509Certificate = _valueKey;

            var enviMdFe = new MDFeEnviMDFe()
            {
                Versao = VersaoServico.Versao300,
                MDFe = mdfe,
                IdLote = _lote
            };
            
            var repositorioDao = new RepositorioDaoFalso();

            //Act
            var xmlGerado = enviMdFe.CriaXmlRequestWs();
            var xmlEsperado = repositorioDao.GetXmlEsperado(_xmlEsperado);

            //Assert
            Assert.Equal(xmlEsperado.InnerXml, xmlGerado.InnerXml);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Para_Criacao_De_Envio_Mdfe_Sem_Versao_E_IDLote()
        {
            //Arrange
            var enviMdFe = new MDFeEnviMDFe()
            {
                Versao = 0,
                MDFe = _mdfe.GetMdfe(),
                IdLote = null
            };

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => enviMdFe.CriaXmlRequestWs());

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Para_Criacao_De_Envio_Mdfe_Sem_Versao()
        {
            //Arrange
            var enviMdFe = new MDFeEnviMDFe()
            {
                Versao = 0,
                MDFe = _mdfe.GetMdfe(),
                IdLote = _lote
            };

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => enviMdFe.CriaXmlRequestWs());

            //Assert
            Assert.NotNull(exception);
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Salvar_Xml_Localmente_Para_Envio_Mdfe()
        {
            //Arrange
            if (_mdfe != null) Dispose();
            var enviMdFe = new MDFeEnviMDFe()
            {
                Versao = VersaoServico.Versao300,
                MDFe = _mdfe.GetMdfe(),
                IdLote = _lote
            };

            enviMdFe.MDFe.Assina();

            //Act
            var result = Record.Exception(() => enviMdFe.SalvarXmlEmDisco());

            //Assert
            Assert.Null(result);
        }

        #endregion
    }
}
