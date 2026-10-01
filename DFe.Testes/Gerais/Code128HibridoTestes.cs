using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFe.Danfe.PdfClown.Tools;

namespace DFe.Testes.Gerais
{
    /// <summary>
    /// Testes do codificador CODE-128 híbrido (subconjuntos C e A) da seção 6 da NT Conjunta 2025.001,
    /// usado no código de barras da chave de acesso do DANFE
    /// </summary>
    [TestClass]
    public class Code128HibridoTestes
    {
        private const byte StartC = 105;
        private const byte TrocaParaCodeA = 101;
        private const byte TrocaParaCodeC = 99;
        private const byte Stop = 106;

        #region Exemplos literais da NT Conjunta 2025.001 - seção 6

        [TestMethod]
        public void ObterSimbolos_ExemploLiteralDaNT_5225AB83()
        {
            // Arrange - sequência completa dada pela NT: start, dados, DV 30 e stop
            var esperado = new byte[] { 105, 52, 25, 101, 33, 34, 99, 83, 30, 106 };

            // Act
            var simbolos = Code128Hibrido.ObterSimbolos("5225AB83");

            // Assert
            CollectionAssert.AreEqual(esperado, simbolos);
        }

        [TestMethod]
        public void ObterSimbolos_ExemploLiteralDaNT_123A()
        {
            // Arrange - "12" em Code C, troca para o Code A (101), '3' (19) e 'A' (33) no Code A
            var dadosEsperados = new byte[] { 105, 12, 101, 19, 33 };

            // Act
            var simbolos = Code128Hibrido.ObterSimbolos("123A");

            // Assert
            CollectionAssert.AreEqual(dadosEsperados, simbolos.Take(5).ToArray());
            AssertDigitoVerificadorEStop(simbolos);
        }

        #endregion

        #region Chave de acesso com CNPJ alfanumérico

        [TestMethod]
        public void ObterSimbolos_ChaveDeAcessoAlfanumericaDoVetorOuro_CodificaConformeAsRegrasDaNT()
        {
            // Arrange
            //6 dígitos iniciais em Code C (3 pares); letras e corridas curtas do CNPJ no Code A;
            //corrida final de 30 dígitos de volta ao Code C (15 pares)
            var dadosEsperados = new byte[]
            {
                105,            //Start C
                52, 25, 7,      //"522507"
                101,            //troca para Code A
                48, 35,         //'P' 'C'
                19,             //'3' (corrida de 1 dígito fica no Code A)
                36,             //'D'
                19, 17, 21,     //'3' '1' '5' (corrida de 3 dígitos fica no Code A)
                43,             //'K'
                99,             //volta ao Code C (corrida final par)
                0, 1, 93, 55, 0, 10, 0, 0, 0, 1, 10, 0, 0, 0, 18
            };

            // Act
            var simbolos = Code128Hibrido.ObterSimbolos("522507PC3D315K000193550010000000011000000018");

            // Assert
            CollectionAssert.AreEqual(dadosEsperados, simbolos.Take(dadosEsperados.Length).ToArray());
            AssertDigitoVerificadorEStop(simbolos);
        }

        #endregion

        #region Regressão - conteúdo 100% numérico deve gerar exatamente o CODE-128 C puro

        [TestMethod]
        public void ObterSimbolos_ComChavesNumericas_IdenticoAoCode128CPuroDaImplementacaoAntiga()
        {
            var chavesNumericas = new[]
            {
                "23190811820016000167650010000000221100000227",
                "11222333000181112223330001811122233300018111",
                "00000000000000000000000000000000000000000000",
                "99999999999999999999999999999999999999999999",
                "35150300822602000124550010009923461099234656"
            };

            foreach (var chave in chavesNumericas)
            {
                // Act
                var simbolos = Code128Hibrido.ObterSimbolos(chave);

                // Assert
                CollectionAssert.AreEqual(CodificarCode128CPuroLegado(chave), simbolos, "Divergência na chave {0}", chave);
            }
        }

        #endregion

        #region Regras de troca de subconjunto

        [TestMethod]
        public void ObterSimbolos_ComLetraInicial_TrocaParaCodeAAposOStartC()
        {
            var simbolos = Code128Hibrido.ObterSimbolos("A");

            CollectionAssert.AreEqual(new byte[] { StartC, TrocaParaCodeA, 33 }, simbolos.Take(3).ToArray());
            AssertDigitoVerificadorEStop(simbolos);
        }

        [TestMethod]
        public void ObterSimbolos_CorridaDe4DigitosEntreLetras_VoltaAoCodeC()
        {
            //"AB" no Code A, "1234" volta ao Code C (2 pares), "CD" troca novamente para o Code A
            var simbolos = Code128Hibrido.ObterSimbolos("AB1234CD");

            CollectionAssert.AreEqual(new byte[] { StartC, TrocaParaCodeA, 33, 34, TrocaParaCodeC, 12, 34, TrocaParaCodeA, 35, 36 }, simbolos.Take(10).ToArray());
            AssertDigitoVerificadorEStop(simbolos);
        }

        [TestMethod]
        public void ObterSimbolos_CorridaDe3DigitosEntreLetras_PermaneceNoCodeA()
        {
            //corrida curta (menos de 4 dígitos) não justifica a troca
            var simbolos = Code128Hibrido.ObterSimbolos("AB123C");

            CollectionAssert.AreEqual(new byte[] { StartC, TrocaParaCodeA, 33, 34, 17, 18, 19, 35 }, simbolos.Take(8).ToArray());
            AssertDigitoVerificadorEStop(simbolos);
        }

        [TestMethod]
        public void ObterSimbolos_CorridaImparDe5DigitosAntesDeLetra_UltimoDigitoFicaNoCodeA()
        {
            //"12345" antes de letra: pares "12" "34" no Code C e o dígito ímpar '5' fica no Code A
            var simbolos = Code128Hibrido.ObterSimbolos("AB12345C");

            CollectionAssert.AreEqual(new byte[] { StartC, TrocaParaCodeA, 33, 34, TrocaParaCodeC, 12, 34, TrocaParaCodeA, 21, 35 }, simbolos.Take(10).ToArray());
            AssertDigitoVerificadorEStop(simbolos);
        }

        [TestMethod]
        public void ObterSimbolos_CorridaFinalParDe2Digitos_VoltaAoCodeC()
        {
            //final com quantidade par de dígitos volta ao Code C, como no exemplo 5225AB83 da NT
            var simbolos = Code128Hibrido.ObterSimbolos("AB12");

            CollectionAssert.AreEqual(new byte[] { StartC, TrocaParaCodeA, 33, 34, TrocaParaCodeC, 12 }, simbolos.Take(6).ToArray());
            AssertDigitoVerificadorEStop(simbolos);
        }

        [TestMethod]
        public void ObterSimbolos_CorridaFinalImparDe3Digitos_PermaneceNoCodeA()
        {
            var simbolos = Code128Hibrido.ObterSimbolos("AB123");

            CollectionAssert.AreEqual(new byte[] { StartC, TrocaParaCodeA, 33, 34, 17, 18, 19 }, simbolos.Take(7).ToArray());
            AssertDigitoVerificadorEStop(simbolos);
        }

        [TestMethod]
        public void ObterSimbolos_CorridaFinalImparDe5Digitos_PrimeiroDigitoNoCodeAEDemaisNoCodeC()
        {
            //corrida final ímpar de 4+ dígitos: um dígito fica no Code A para a parte restante ser par
            var simbolos = Code128Hibrido.ObterSimbolos("AB12345");

            CollectionAssert.AreEqual(new byte[] { StartC, TrocaParaCodeA, 33, 34, 17, TrocaParaCodeC, 23, 45 }, simbolos.Take(8).ToArray());
            AssertDigitoVerificadorEStop(simbolos);
        }

        [TestMethod]
        public void ObterSimbolos_NumericoImpar_ParesNoCodeCEUltimoDigitoNoCodeA()
        {
            var simbolos = Code128Hibrido.ObterSimbolos("123");

            CollectionAssert.AreEqual(new byte[] { StartC, 12, TrocaParaCodeA, 19 }, simbolos.Take(4).ToArray());
            AssertDigitoVerificadorEStop(simbolos);
        }

        #endregion

        #region Entradas inválidas

        [TestMethod]
        [DataRow("")]
        [DataRow(null)]
        public void ObterSimbolos_ComCodigoVazioOuNulo_LancaArgumentException(string codigo)
        {
            Assert.ThrowsException<ArgumentException>(() => Code128Hibrido.ObterSimbolos(codigo));
        }

        [TestMethod]
        [DataRow("12a4", DisplayName = "Letra minúscula")]
        [DataRow("12-4", DisplayName = "Hífen")]
        [DataRow("12 34", DisplayName = "Espaço")]
        [DataRow("12@34", DisplayName = "Caractere @")]
        public void ObterSimbolos_ComCaractereForaDeDigitosELetrasMaiusculas_LancaArgumentException(string codigo)
        {
            Assert.ThrowsException<ArgumentException>(() => Code128Hibrido.ObterSimbolos(codigo));
        }

        #endregion

        /// <summary>
        /// Confere as duas últimas posições da sequência: o DV módulo 103
        /// (soma ponderada com o start valendo peso 1) e o símbolo de Stop (106)
        /// </summary>
        private static void AssertDigitoVerificadorEStop(byte[] simbolos)
        {
            Assert.AreEqual(Stop, simbolos[simbolos.Length - 1], "O último símbolo deve ser o Stop (106)");

            var soma = (int)simbolos[0];
            for (var i = 1; i < simbolos.Length - 2; i++)
                soma += i * simbolos[i];

            Assert.AreEqual((byte)(soma % 103), simbolos[simbolos.Length - 2], "Dígito verificador módulo 103 incorreto");
        }

        /// <summary>
        /// Comportamento da implementação antiga (Barcode128C do DANFE PdfClown): Start C,
        /// dados aos pares, DV módulo 103 e Stop - referência de regressão para conteúdo numérico
        /// </summary>
        private static byte[] CodificarCode128CPuroLegado(string codigo)
        {
            var codeBytes = new List<byte> { 105 };

            for (var i = 0; i < codigo.Length; i += 2)
                codeBytes.Add(byte.Parse(codigo.Substring(i, 2)));

            var cd = 105;
            for (var i = 1; i < codeBytes.Count; i++)
            {
                cd += i * codeBytes[i];
                cd %= 103;
            }

            codeBytes.Add((byte)cd);
            codeBytes.Add(106);

            return codeBytes.ToArray();
        }
    }
}
