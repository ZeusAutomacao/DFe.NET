using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFe.Danfe.PdfClown.Tools;

namespace DFe.Testes.Gerais
{
    /// <summary>
    /// Testes da formatação de CNPJ, CPF e chave de acesso do DANFE (PdfClown),
    /// cobrindo documentos numéricos (fluxo antigo) e o CNPJ alfanumérico da NT Conjunta 2025.001
    /// </summary>
    [TestClass]
    public class FormatadorDanfeTestes
    {
        #region FormatarCnpj

        [TestMethod]
        [DataRow("11222333000181", "11.222.333/0001-81", DisplayName = "CNPJ numérico")]
        [DataRow("00000000000191", "00.000.000/0001-91", DisplayName = "CNPJ numérico com zeros")]
        [DataRow("12ABC34501DE35", "12.ABC.345/01DE-35", DisplayName = "CNPJ alfanumérico da NT")]
        [DataRow("PC3D315K000193", "PC.3D3.15K/0001-93", DisplayName = "CNPJ alfanumérico de teste da Receita")]
        public void FormatarCnpj_ComCnpjSemMascara_AplicaAMascaraPosicional(string cnpj, string esperado)
        {
            Assert.AreEqual(esperado, Formatador.FormatarCnpj(cnpj));
        }

        [TestMethod]
        [DataRow("11.222.333/0001-81", "11.222.333/0001-81", DisplayName = "Numérico já com máscara")]
        [DataRow("12.ABC.345/01DE-35", "12.ABC.345/01DE-35", DisplayName = "Alfanumérico já com máscara")]
        public void FormatarCnpj_ComCnpjJaFormatado_MantemAMascara(string cnpj, string esperado)
        {
            Assert.AreEqual(esperado, Formatador.FormatarCnpj(cnpj));
        }

        [TestMethod]
        [DataRow("12abc34501de35", DisplayName = "Minúsculas não formatam")]
        [DataRow("12ABC34501DEA5", DisplayName = "Letra nos DVs não formata")]
        [DataRow("123", DisplayName = "Curto demais não formata")]
        public void FormatarCnpj_ComConteudoForaDoPadrao_DevolveOTextoOriginal(string cnpj)
        {
            Assert.AreEqual(cnpj, Formatador.FormatarCnpj(cnpj));
        }

        #endregion

        #region FormatarCpfCnpj

        [TestMethod]
        [DataRow("12345678901", "123.456.789-01", DisplayName = "CPF continua com máscara de CPF")]
        [DataRow("11222333000181", "11.222.333/0001-81", DisplayName = "CNPJ numérico")]
        [DataRow("12ABC34501DE35", "12.ABC.345/01DE-35", DisplayName = "CNPJ alfanumérico")]
        public void FormatarCpfCnpj_EscolheAMascaraCorreta(string documento, string esperado)
        {
            Assert.AreEqual(esperado, Formatador.FormatarCpfCnpj(documento));
        }

        #endregion

        #region FormatarChaveAcesso

        [TestMethod]
        public void FormatarChaveAcesso_ComChaveAlfanumerica_AgrupaDeQuatroEmQuatro()
        {
            var formatada = Formatador.FormatarChaveAcesso("522507PC3D315K000193550010000000011000000018");

            Assert.AreEqual("5225 07PC 3D31 5K00 0193 5500 1000 0000 0110 0000 0018", formatada);
        }

        [TestMethod]
        public void FormatarChaveAcesso_ComChaveNumerica_AgrupaDeQuatroEmQuatro()
        {
            var formatada = Formatador.FormatarChaveAcesso("23190811820016000167650010000000221100000227");

            Assert.AreEqual("2319 0811 8200 1600 0167 6500 1000 0000 2211 0000 0227", formatada);
        }

        #endregion
    }
}
