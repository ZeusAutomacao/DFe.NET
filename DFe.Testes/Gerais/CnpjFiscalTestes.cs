using System;
using DFe.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DFe.Testes.Gerais
{
    /// <summary>
    /// Testes do cálculo de dígitos verificadores e validação de CNPJ,
    /// cobrindo CNPJs numéricos (fluxo antigo) e alfanuméricos (NT Conjunta 2025.001)
    /// </summary>
    [TestClass]
    public class CnpjFiscalTestes
    {
        #region ObterDigitosVerificadores - vetores ouro

        [TestMethod]
        [DataRow("112223330001", "81", DisplayName = "Base numérica")]
        [DataRow("12ABC34501DE", "35", DisplayName = "Exemplo da NT Conjunta 2025.001")]
        [DataRow("PC3D315K0001", "93", DisplayName = "CNPJ de teste da Receita Federal")]
        [DataRow("000000000000", "00", DisplayName = "Base zerada - o DV confere, o bloqueio é do Valido")]
        public void ObterDigitosVerificadores_ComBaseDe12Posicoes_RetornaDvsEsperados(string baseCnpj, string dvsEsperados)
        {
            Assert.AreEqual(dvsEsperados, CnpjFiscal.ObterDigitosVerificadores(baseCnpj));
        }

        [TestMethod]
        [DataRow("000000000001", "91", DisplayName = "Banco do Brasil")]
        [DataRow("114447770001", "61", DisplayName = "CNPJ numérico clássico de testes")]
        [DataRow("123456780001", "95", DisplayName = "CNPJ numérico sequencial")]
        public void ObterDigitosVerificadores_ComBasesNumericasHistoricas_MantemComportamentoAntigo(string baseCnpj, string dvsEsperados)
        {
            Assert.AreEqual(dvsEsperados, CnpjFiscal.ObterDigitosVerificadores(baseCnpj));
        }

        #endregion

        #region ObterDigitosVerificadores - entradas inválidas

        [TestMethod]
        [DataRow("pc3d315k0001", DisplayName = "Minúsculas")]
        [DataRow("12ABC34501D", DisplayName = "11 posições")]
        [DataRow("12ABC34501DEF", DisplayName = "13 posições")]
        [DataRow("12ABC34501D@", DisplayName = "Caractere @ (ASCII 64)")]
        [DataRow("12ABC34501D:", DisplayName = "Caractere : (ASCII 58)")]
        [DataRow("12.ABC.345/01", DisplayName = "Com máscara")]
        [DataRow("", DisplayName = "Vazia")]
        public void ObterDigitosVerificadores_ComBaseInvalida_LancaArgumentException(string baseCnpj)
        {
            Assert.ThrowsException<ArgumentException>(() => CnpjFiscal.ObterDigitosVerificadores(baseCnpj));
        }

        [TestMethod]
        public void ObterDigitosVerificadores_ComBaseNula_LancaArgumentNullException()
        {
            Assert.ThrowsException<ArgumentNullException>(() => CnpjFiscal.ObterDigitosVerificadores(null));
        }

        #endregion

        #region Valido - CNPJs aceitos

        [TestMethod]
        [DataRow("11222333000181", DisplayName = "Numérico do vetor ouro")]
        [DataRow("12ABC34501DE35", DisplayName = "Alfanumérico do exemplo da NT")]
        [DataRow("PC3D315K000193", DisplayName = "Alfanumérico de teste da Receita")]
        [DataRow("00000000000191", DisplayName = "Numérico clássico - Banco do Brasil")]
        [DataRow("11444777000161", DisplayName = "Numérico clássico de testes")]
        public void Valido_ComCnpjCorreto_RetornaVerdadeiro(string cnpj)
        {
            Assert.IsTrue(CnpjFiscal.Valido(cnpj));
        }

        #endregion

        #region Valido - CNPJs rejeitados

        [TestMethod]
        public void Valido_ComCnpjZerado_RetornaFalso()
        {
            //o DV do CNPJ zerado confere, mas a NT veda o seu uso
            Assert.IsFalse(CnpjFiscal.Valido("00000000000000"));
        }

        [TestMethod]
        [DataRow("11222333000182", DisplayName = "Numérico com DV errado")]
        [DataRow("12ABC34501DE36", DisplayName = "Alfanumérico com segundo DV errado")]
        [DataRow("12ABC34501DE45", DisplayName = "Alfanumérico com primeiro DV errado")]
        [DataRow("PC3D315K000139", DisplayName = "Alfanumérico com DVs trocados")]
        public void Valido_ComDvIncorreto_RetornaFalso(string cnpj)
        {
            Assert.IsFalse(CnpjFiscal.Valido(cnpj));
        }

        [TestMethod]
        [DataRow("pc3d315k000193", DisplayName = "Minúsculas")]
        [DataRow("PC3D315K00019", DisplayName = "13 posições")]
        [DataRow("PC3D315K0001935", DisplayName = "15 posições")]
        [DataRow("12ABC34501DEA5", DisplayName = "Letra na posição dos DVs")]
        [DataRow("12ABC34501D:93", DisplayName = "Caractere : (ASCII 58)")]
        [DataRow("12ABC34501D@93", DisplayName = "Caractere @ (ASCII 64)")]
        [DataRow("11.222.333/0001-81", DisplayName = "Com máscara")]
        [DataRow("", DisplayName = "Vazio")]
        [DataRow(null, DisplayName = "Nulo")]
        public void Valido_ComFormatoInvalido_RetornaFalso(string cnpj)
        {
            Assert.IsFalse(CnpjFiscal.Valido(cnpj));
        }

        #endregion

        #region Coerência entre ObterDigitosVerificadores e Valido

        [TestMethod]
        public void Valido_ComDvsCalculadosPelaPropriaClasse_RetornaVerdadeiro()
        {
            var bases = new[] { "112223330001", "12ABC34501DE", "PC3D315K0001", "A1B2C3D4E5F6", "ZZZZZZZZZZZZ", "0000000000AB", "999999999999" };

            foreach (var baseCnpj in bases)
            {
                var cnpj = baseCnpj + CnpjFiscal.ObterDigitosVerificadores(baseCnpj);

                Assert.IsTrue(CnpjFiscal.Valido(cnpj), "O CNPJ {0} deveria ser válido", cnpj);
            }
        }

        #endregion
    }
}
