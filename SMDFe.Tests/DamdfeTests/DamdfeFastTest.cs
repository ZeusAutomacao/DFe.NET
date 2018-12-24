using System;
using DFe.Utils;
using MDFe.Classes.Retorno;
using MDFe.Damdfe.Base;
using MDFe.Damdfe.Fast;
using MDFe.Tests.Dao;
using MDFe.Tests.Entidades;
using Xunit;

namespace MDFe.Tests.DamdfeTests
{
    
    public class DamdfeFastTest : IDisposable
    {
        #region Variáveis

        private MDFeProcMDFe _mdfeProc;
        private readonly ConfiguracaoDamdfe _confiDamdfe;
        private readonly string _nomeArquivo;
        private readonly Configuracao _configuracao;
        private readonly RepositorioDaoFalso _repositorioDao;
        #endregion

        #region Setup

     
        public DamdfeFastTest()
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

        }

        public void Dispose()
        {
            _mdfeProc = new MDFeProcMDFe();
        }

        #endregion

#region Testes para a Classe DamdfeFast


        [Fact]
        public void Deve_Criar_Um_Relatorio_Com_Fast_Report()
        {
            //Arrange
            var xml = _repositorioDao.GetXmlEsperado("proc.xml");
            _mdfeProc = FuncoesXml.XmlStringParaClasse<MDFeProcMDFe>(xml.InnerXml);

            //Act
            var rpt = new DamdfeFrMDFe(proc: _mdfeProc,
                config: _confiDamdfe,
                arquivoRelatorio: _nomeArquivo);

            //Assert
            Assert.NotNull(rpt);
        }

        [Fact]
        public void Deve_Exportar_Relatorio_Para_Html()
        {
            //Arrange
            var xml = _repositorioDao.GetXmlEsperado("proc.xml");
            _mdfeProc = FuncoesXml.XmlStringParaClasse<MDFeProcMDFe>(xml.InnerXml);

            var rpt = new DamdfeFrMDFe(proc: _mdfeProc,
                config: _confiDamdfe,
                arquivoRelatorio: null);

            //Act
            var exception = Record.Exception(() => rpt.ExportarHTML("teste.html"));

            //Assert
            Assert.Null(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Para_Criacao_Do_Relatorio_Com_Configuracao_Nula()
        {
            //Arrange
            var xml = _repositorioDao.GetXmlEsperado("proc.xml");
            _mdfeProc = FuncoesXml.XmlStringParaClasse<MDFeProcMDFe>(xml.InnerXml);

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => new DamdfeFrMDFe(_mdfeProc, null, null));

            //Assert
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void Deve_Gerar_Uma_Excecao_Para_Criacao_Do_Relatorio_Com_Arquivo_Inexistente()
        {
            //Arrange
            var xml = _repositorioDao.GetXmlEsperado("proc.xml");
            _mdfeProc = FuncoesXml.XmlStringParaClasse<MDFeProcMDFe>(xml.InnerXml);

            //Act
            var exception = Assert.ThrowsAny<Exception>(() => new DamdfeFrMDFe(_mdfeProc, _confiDamdfe, "inexiste.frx"));

            //Assert
            Assert.IsAssignableFrom<Exception>(exception);
        }

#endregion
    }
}
