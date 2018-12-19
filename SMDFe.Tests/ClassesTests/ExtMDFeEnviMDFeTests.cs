using System;
using System.Xml;
using DFe.Utils;
using Xunit;
using SMDFe.Classes.Extencoes;
using SMDFe.Classes.Servicos.Autorizacao;
using SMDFe.Tests.Dao;
using SMDFe.Tests.Entidades;
using SMDFe.Utils.Flags;

namespace SMDFe.Tests.ClassesTests
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
        public void Deve_Testar_A_Criacao_De_Uma_Requisicao_Para_Envio_Mdfe_Com_Parametros()
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
        public void Deve_Testar_A_Requisicao_De_Envio_MDFe_Criada_Com_O_Xml_Esperado()
        {
            //Arrange
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
            Assert.NotNull(xmlEsperado);
            Assert.NotNull(xmlGerado);
            Assert.Equal(xmlEsperado.InnerXml, xmlGerado.InnerXml);
        }

        [Fact]
        public void Deve_Testar_A_Criacao_De_Uma_Requisicao_Para_Envio_Mdfe_Sem_Versao_E_IDLote()
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
        public void Deve_Testar_A_Criacao_De_Uma_Requisicao_Para_Envio_Mdfe_Sem_Versao()
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
        public void Deve_Testar_A_Funcao_Envio_Mdfe_Para_Salvar_Xml_Localmente_Com_Parametros_Validos()
        {
            //Arrange
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
