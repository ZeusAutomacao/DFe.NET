using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFe.Classes.Servicos.Tipos;
using NFe.Utils.Evento;
using NFe.Utils.Inutilizacao;

namespace DFe.Testes.Gerais
{
    /// <summary>
    ///     Vetores ouro do Id assinável de evento (infEvento/@Id) e de inutilização (infInut/@Id).
    ///     <para>
    ///         Esses Ids também são calculados internamente pelos métodos que assinam (ServicosNFe). Se o formato
    ///         divergir, o digest da assinatura não bate e o SEFAZ rejeita — por isso os valores esperados aqui são
    ///         literais do layout, e não o resultado do próprio método.
    ///     </para>
    /// </summary>
    [TestClass]
    public class ObterIdTestes
    {
        /// <summary>Chave de NF-e (modelo 55) com CNPJ alfanumérico, conforme NT Conjunta 2025.001</summary>
        private const string ChaveNFe = "522507PC3D315K000193550010000000011000000018";

        /// <summary>Chave de NFC-e (modelo 65), 100% numérica</summary>
        private const string ChaveNFCe = "23190811820016000167650010000000221100000227";

        private const string CnpjNumerico = "11820016000167";
        private const string CnpjAlfanumerico = "PC3D315K000193";

        [TestMethod]
        [DataRow(NFeTipoEvento.TeNfeCancelamento, ChaveNFe, 1, "ID110111" + ChaveNFe + "01",
            DisplayName = "Cancelamento, chave com CNPJ alfanumérico")]
        [DataRow(NFeTipoEvento.TeNfeCartaCorrecao, ChaveNFe, 2, "ID110110" + ChaveNFe + "02",
            DisplayName = "Carta de Correção, 2ª sequência")]
        [DataRow(NFeTipoEvento.TeMdCienciaDaOperacao, ChaveNFCe, 1, "ID210210" + ChaveNFCe + "01",
            DisplayName = "Ciência da Operação, chave numérica")]
        [DataRow(NFeTipoEvento.TeMdCienciaDaOperacao, ChaveNFCe, 11, "ID210210" + ChaveNFCe + "11",
            DisplayName = "Sequência com 2 dígitos não recebe zero à esquerda")]
        public void ObterIdEvento_SegueOLayout(NFeTipoEvento tpEvento, string chNFe, int nSeqEvento, string esperado)
        {
            Assert.AreEqual(esperado, Extevento.ObterId(tpEvento, chNFe, nSeqEvento));
        }

        [TestMethod]
        [DataRow(ModeloDocumento.NFe, CnpjNumerico, 1, 1, 10,
            "ID3525" + CnpjNumerico + "55" + "001" + "000000001" + "000000010",
            DisplayName = "NF-e, modelo 55")]
        [DataRow(ModeloDocumento.NFCe, CnpjNumerico, 1, 1, 10,
            "ID3525" + CnpjNumerico + "65" + "001" + "000000001" + "000000010",
            DisplayName = "NFC-e, modelo 65")]
        [DataRow(ModeloDocumento.NFe, CnpjAlfanumerico, 12, 5, 5,
            "ID3525" + CnpjAlfanumerico + "55" + "012" + "000000005" + "000000005",
            DisplayName = "CNPJ alfanumérico, faixa de um número só")]
        public void ObterIdInutilizacao_SegueOLayout(ModeloDocumento modelo, string cnpj, int serie,
            int numeroInicial, int numeroFinal, string esperado)
        {
            Assert.AreEqual(esperado,
                ExtinutNFe.ObterId(Estado.SP, 25, cnpj, modelo, serie, numeroInicial, numeroFinal));
        }
    }
}
