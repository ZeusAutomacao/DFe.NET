using System;
using System.Xml;
using NUnit.Framework;
using SMDFe.Classes.Extencoes;
using SMDFe.Classes.Informacoes.Evento;
using SMDFe.Tests.Dao;
using SMDFe.Tests.Entidades;


namespace SMDFe.Tests.ClassesTests
{
    [TestFixture]
    public class ExtMDFeEventoMDFeTests
    {
        private Configuracao _configuracao;
        private MDFeEventoMDFe _evento;
        

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

        #region Testes para a classe ExtMDFeEventoMDFe 

        // <------------------------------------------------- Evento Incluir Condutor --------------------------------------------->


        // <----------------------------------------------------- Evento Canelar -------------------------------------------------->

        // <--------------------------------------------------- Evento Encerramento ----------------------------------------------->

        #endregion
    }
}
