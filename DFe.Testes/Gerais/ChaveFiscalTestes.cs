using System;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using DFe.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DFe.Testes.Gerais
{
    /// <summary>
    /// Testes da chave de acesso do DF-e cobrindo o fluxo antigo (CNPJ 100% numérico)
    /// e o novo fluxo com CNPJ alfanumérico da NT Conjunta 2025.001
    /// </summary>
    [TestClass]
    public class ChaveFiscalTestes
    {
        //Chave real de NFC-e homologação (CNPJ numérico) usada como âncora de regressão do fluxo antigo
        private const string ChaveNumericaReal = "23190811820016000167650010000000221100000227";

        //Vetor ouro da NT: NFe de GO, emitida em 07/2025 pelo CNPJ alfanumérico de teste da Receita PC3D315K000193
        private const string ChaveAlfanumericaOuro = "522507PC3D315K000193550010000000011000000018";

        #region Fluxo novo - CNPJ alfanumérico

        [TestMethod]
        public void ObterChave_ComCnpjAlfanumerico_GeraAChaveDoVetorOuro()
        {
            // Arrange
            var dataEmissao = new DateTimeOffset(2025, 7, 15, 10, 0, 0, TimeSpan.FromHours(-3));

            // Act
            var dados = ChaveFiscal.ObterChave(Estado.GO, dataEmissao, "PC3D315K000193", ModeloDocumento.NFe, 1, 1, 1, 1);

            // Assert
            Assert.AreEqual(ChaveAlfanumericaOuro, dados.Chave);
            Assert.AreEqual((byte)8, dados.DigitoVerificador);
        }

        [TestMethod]
        public void ObterChave_ComCnpjAlfanumerico_ChaveTemLayoutCorreto()
        {
            var dados = ChaveFiscal.ObterChave(Estado.GO, new DateTime(2025, 7, 15), "PC3D315K000193", ModeloDocumento.NFe, 1, 1, 1, 1);

            Assert.AreEqual(44, dados.Chave.Length);
            Assert.AreEqual("52", dados.Chave.Substring(0, 2), "cUF");
            Assert.AreEqual("2507", dados.Chave.Substring(2, 4), "AAMM");
            Assert.AreEqual("PC3D315K000193", dados.Chave.Substring(6, 14), "CNPJ");
            Assert.AreEqual("55", dados.Chave.Substring(20, 2), "modelo");
        }

        [TestMethod]
        public void ChaveValida_ComChaveAlfanumericaDoVetorOuro_RetornaVerdadeiro()
        {
            Assert.IsTrue(ChaveFiscal.ChaveValida(ChaveAlfanumericaOuro));
        }

        [TestMethod]
        [DataRow("0")]
        [DataRow("1")]
        [DataRow("5")]
        [DataRow("9")]
        public void ChaveValida_ComChaveAlfanumericaComDvErrado_RetornaFalso(string dvErrado)
        {
            var chaveComDvErrado = ChaveAlfanumericaOuro.Substring(0, 43) + dvErrado;

            Assert.IsFalse(ChaveFiscal.ChaveValida(chaveComDvErrado));
        }

        [TestMethod]
        public void ObterChave_ComVariosCnpjsAlfanumericos_DvIgualAoDaImplementacaoDeReferenciaDaNT()
        {
            // Arrange - bases alfanuméricas com DVs calculados pela regra oficial de CNPJ
            var basesCnpj = new[] { "12ABC34501DE", "PC3D315K0001", "A1B2C3D4E5F6", "ZZZZZZZZZZZZ", "0000000000AB", "H9J8K7L6M5N4" };
            var ufs = new[] { Estado.GO, Estado.SP, Estado.CE, Estado.RS, Estado.AM, Estado.DF };
            var modelos = new[] { ModeloDocumento.NFe, ModeloDocumento.NFCe };

            for (var i = 0; i < basesCnpj.Length; i++)
            {
                var cnpj = basesCnpj[i] + CnpjFiscal.ObterDigitosVerificadores(basesCnpj[i]);

                foreach (var modelo in modelos)
                {
                    // Act
                    var dados = ChaveFiscal.ObterChave(ufs[i], new DateTime(2026, 7, 1), cnpj, modelo, i + 1, 1000 + i, 1, 77000 + i);

                    // Assert
                    var dvReferencia = CalcularDvReferenciaNt(dados.Chave.Substring(0, 43));
                    Assert.AreEqual(dvReferencia, (int)dados.DigitoVerificador, "CNPJ {0}, modelo {1}", cnpj, modelo);
                    Assert.IsTrue(ChaveFiscal.ChaveValida(dados.Chave), "ChaveValida deveria aceitar {0}", dados.Chave);
                }
            }
        }

        [TestMethod]
        public void ObterChave_ComCnpjAlfanumericoMinusculo_LancaArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                ChaveFiscal.ObterChave(Estado.GO, new DateTime(2025, 7, 15), "pc3d315k000193", ModeloDocumento.NFe, 1, 1, 1, 1));
        }

        [TestMethod]
        [DataRow(':')]
        [DataRow(';')]
        [DataRow('<')]
        [DataRow('=')]
        [DataRow('>')]
        [DataRow('?')]
        [DataRow('@')]
        public void ObterChave_ComCaractereEntreDigitosELetras_LancaArgumentException(char caractereInvalido)
        {
            //os caracteres ASCII 58 a 64 ficam entre '9' e 'A' e produziriam DV errado sem erro se aceitos
            var cnpjInvalido = "PC3D315K0001" + caractereInvalido + "3";

            Assert.ThrowsException<ArgumentException>(() =>
                ChaveFiscal.ObterChave(Estado.GO, new DateTime(2025, 7, 15), cnpjInvalido, ModeloDocumento.NFe, 1, 1, 1, 1));
        }

        [TestMethod]
        public void ChaveValida_ComLetraMinusculaNaChave_LancaArgumentException()
        {
            var chaveComMinuscula = "522507pC3D315K000193550010000000011000000018";

            Assert.ThrowsException<ArgumentException>(() => ChaveFiscal.ChaveValida(chaveComMinuscula));
        }

        #endregion

        #region Fluxo antigo - CNPJ numérico (regressão)

        [TestMethod]
        public void ObterChave_ComCnpjNumerico_GeraChaveRealConhecida()
        {
            // Arrange - componentes da chave real de homologação (NFC-e do CE, 08/2019)
            var dataEmissao = new DateTimeOffset(2019, 8, 10, 12, 0, 0, TimeSpan.FromHours(-3));

            // Act
            var dados = ChaveFiscal.ObterChave(Estado.CE, dataEmissao, "11820016000167", ModeloDocumento.NFCe, 1, 22, 1, 10000022);

            // Assert
            Assert.AreEqual(ChaveNumericaReal, dados.Chave);
            Assert.AreEqual((byte)7, dados.DigitoVerificador);
        }

        [TestMethod]
        public void ChaveValida_ComChaveNumericaRealConhecida_RetornaVerdadeiro()
        {
            Assert.IsTrue(ChaveFiscal.ChaveValida(ChaveNumericaReal));
        }

        [TestMethod]
        public void ChaveValida_ComChaveNumericaComDvErrado_RetornaFalso()
        {
            var chaveComDvErrado = ChaveNumericaReal.Substring(0, 43) + "9";

            Assert.IsFalse(ChaveFiscal.ChaveValida(chaveComDvErrado));
        }

        [TestMethod]
        public void ObterChave_ComVariosCnpjsNumericos_DvIdenticoAoAlgoritmoLegado()
        {
            // Arrange - regressão: para chave 100% numérica o DV novo deve ser idêntico ao da implementação antiga
            var cnpjs = new[] { "11222333000181", "00000000000191", "11444777000161", "99999999999999", "00000000000000", "12345678000195" };
            var ufs = new[] { Estado.SP, Estado.GO, Estado.CE, Estado.RS, Estado.MG, Estado.BA };

            for (var i = 0; i < cnpjs.Length; i++)
            {
                foreach (var modelo in new[] { ModeloDocumento.NFe, ModeloDocumento.NFCe })
                {
                    // Act
                    var dados = ChaveFiscal.ObterChave(ufs[i], new DateTime(2024, 12, 31), cnpjs[i], modelo, 883 + i, 999999990 + i, 2, 10000000 - i);

                    // Assert
                    var dvLegado = CalcularDvAlgoritmoLegado(dados.Chave.Substring(0, 43));
                    Assert.AreEqual(dvLegado, dados.DigitoVerificador.ToString(), "CNPJ {0}, modelo {1}", cnpjs[i], modelo);
                    Assert.IsTrue(ChaveFiscal.ChaveValida(dados.Chave));
                }
            }
        }

        [TestMethod]
        public void ObterChave_ComCpfDe11Posicoes_PreencheComZerosAEsquerda()
        {
            // Act
            var dados = ChaveFiscal.ObterChave(Estado.SP, new DateTime(2026, 7, 1), "12345678901", ModeloDocumento.NFe, 1, 123, 1, 55555);

            // Assert
            Assert.AreEqual(44, dados.Chave.Length);
            Assert.AreEqual("00012345678901", dados.Chave.Substring(6, 14));
            Assert.IsTrue(ChaveFiscal.ChaveValida(dados.Chave));
        }

        #endregion

        #region Validação do documento do emitente

        [TestMethod]
        [DataRow("PC3D315K0001", DisplayName = "12 posições - base de CNPJ truncada")]
        [DataRow("1122233300018", DisplayName = "13 posições")]
        [DataRow("112223330001811", DisplayName = "15 posições")]
        [DataRow("1234567890", DisplayName = "10 posições")]
        public void ObterChave_ComDocumentoDeComprimentoInvalido_LancaArgumentException(string documento)
        {
            //um CNPJ truncado não pode virar silenciosamente uma chave bem-formada porém errada
            Assert.ThrowsException<ArgumentException>(() =>
                ChaveFiscal.ObterChave(Estado.GO, new DateTime(2026, 7, 1), documento, ModeloDocumento.NFe, 1, 1, 1, 1));
        }

        [TestMethod]
        public void ObterChave_ComDocumentoVazio_LancaArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                ChaveFiscal.ObterChave(Estado.GO, new DateTime(2026, 7, 1), "", ModeloDocumento.NFe, 1, 1, 1, 1));
        }

        [TestMethod]
        public void ObterChave_ComDocumentoNulo_LancaArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
                ChaveFiscal.ObterChave(Estado.GO, new DateTime(2026, 7, 1), null, ModeloDocumento.NFe, 1, 1, 1, 1));
        }

        #endregion

        /// <summary>
        /// Implementação de referência do Anexo II da NT Conjunta 2025.001, portada para C#
        /// </summary>
        private static int CalcularDvReferenciaNt(string chave43)
        {
            var soma = 0;
            var peso = 2;
            for (var i = chave43.Length - 1; i >= 0; i--)
            {
                soma += (chave43[i] - '0') * peso;
                peso = peso == 9 ? 2 : peso + 1;
            }
            var dv = 11 - soma % 11;
            return dv >= 10 ? 0 : dv;
        }

        /// <summary>
        /// Algoritmo do DV como era antes da correção (só funciona com dígitos),
        /// usado para garantir que chaves numéricas não sofreram regressão
        /// </summary>
        private static string CalcularDvAlgoritmoLegado(string chave43)
        {
            var soma = 0;
            var peso = 2;
            for (var i = chave43.Length - 1; i != -1; i--)
            {
                var ch = Convert.ToInt32(chave43[i].ToString());
                soma += ch * peso;
                if (peso < 9)
                    peso += 1;
                else
                    peso = 2;
            }
            var mod = soma % 11;
            var dv = mod == 0 || mod == 1 ? 0 : 11 - mod;
            return dv.ToString();
        }
    }
}
