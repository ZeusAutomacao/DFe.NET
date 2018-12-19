using System;
using System.Collections.Generic;
using System.Text;
using DFe.Utils;
using NUnit.Framework;
using SMDFe.Classes.Retorno;
using SMDFe.Damdfe.Base;
using SMDFe.Damdfe.Fast;
using SMDFe.Tests.Dao;
using SMDFe.Tests.Entidades;


namespace SMDFe.Tests.DamdfeTests
{
    [TestFixture]
    public class DamdfeFastTest
    {
        #region Variáveis

        private MDFeProcMDFe _mdfeProc;
        private ConfiguracaoDamdfe _confiDamdfe;
        private string _nomeArquivo;
        private Configuracao _configuracao;
        private RepositorioDaoFalso _repositorioDao;
        #endregion

        #region Setup

        [SetUp]
        public void CriarConfiguração()
        {
            var configuracaoDao = new ConfiguracaoDao();
            _configuracao = configuracaoDao.GetConfiguracao();

            _repositorioDao = new RepositorioDaoFalso();

            _confiDamdfe = new ConfiguracaoDamdfe()
            {
                Desenvolvedor = "NINGUEM",
                DocumentoCancelado = false,
                DocumentoEncerrado = true,
                QuebrarLinhasObservacao = true
            };

            _nomeArquivo = "";

            var configuracaoCertificado = new ConfiguracaoCertificado
            {
                Senha = _configuracao.CertificadoDigital.Senha,
                Arquivo = _configuracao.CertificadoDigital.CaminhoArquivo,
                ManterDadosEmCache = _configuracao.CertificadoDigital.ManterEmCache,
                Serial = _configuracao.CertificadoDigital.NumeroDeSerie
            };

            Utils.Configuracoes.MDFeConfiguracao.Instancia.ConfiguracaoCertificado = configuracaoCertificado;
            Utils.Configuracoes.MDFeConfiguracao.Instancia.CaminhoSchemas = _configuracao.ConfigWebService.CaminhoSchemas;
            Utils.Configuracoes.MDFeConfiguracao.Instancia.CaminhoSalvarXml = _configuracao.DiretorioSalvarXml;
            Utils.Configuracoes.MDFeConfiguracao.Instancia.IsSalvarXml = _configuracao.IsSalvarXml;

            Utils.Configuracoes.MDFeConfiguracao.Instancia.VersaoWebService.VersaoLayout = _configuracao.ConfigWebService.VersaoLayout;

            Utils.Configuracoes.MDFeConfiguracao.Instancia.VersaoWebService.TipoAmbiente = _configuracao.ConfigWebService.Ambiente;
            Utils.Configuracoes.MDFeConfiguracao.Instancia.VersaoWebService.UfEmitente = _configuracao.ConfigWebService.UfEmitente;
            Utils.Configuracoes.MDFeConfiguracao.Instancia.VersaoWebService.TimeOut = _configuracao.ConfigWebService.TimeOut;

        }

        #endregion

        #region Testes para a Classe DamdfeFast

        [Test]
        public void Testa_A_Leitura_Com_Fast_Report()
        {
            //Arrange
            var xml = _repositorioDao.GetXmlEsperado("xml-mdfe-proc.xml");
            _mdfeProc = FuncoesXml.XmlStringParaClasse<MDFeProcMDFe>(xml.InnerXml);

            var rpt = new DamdfeFrMDFe(proc: _mdfeProc,
                config: _confiDamdfe,
                arquivoRelatorio: _nomeArquivo);

            //Act


            //Assert
        }

        #endregion

    }
}
