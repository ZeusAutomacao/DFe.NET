using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFe.Danfe.PdfClown;
using NFe.Danfe.PdfClown.Modelo;

namespace DFe.Testes.Gerais
{
    /// <summary>
    /// Smoke test do DANFE PdfClown: o PDF deve ser gerado com o código de barras CODE-128 híbrido
    /// tanto para chave 100% numérica (fluxo antigo) quanto para chave com CNPJ alfanumérico
    /// </summary>
    [TestClass]
    public class DanfePdfClownChaveAlfanumericaTestes
    {
        private static DanfeViewModel CriarViewModelMinimo(string chaveAcesso)
        {
            var model = new DanfeViewModel
            {
                ChaveAcesso = chaveAcesso,
                NfNumero = 1,
                NfSerie = 1,
                TipoNF = 1,
                TipoAmbiente = 2,
                DataHoraEmissao = new DateTime(2025, 7, 15),
                NaturezaOperacao = "VENDA",
                ProtocoloAutorizacao = "352250000000001 15/07/2025 10:00:00"
            };

            model.Emitente.RazaoSocial = "EMITENTE DE TESTE LTDA";
            model.Emitente.CnpjCpf = chaveAcesso.Substring(6, 14);
            model.Emitente.EnderecoLogadrouro = "RUA DE TESTE";
            model.Emitente.EnderecoNumero = "1";
            model.Emitente.EnderecoBairro = "CENTRO";
            model.Emitente.Municipio = "GOIANIA";
            model.Emitente.EnderecoUf = "GO";
            model.Emitente.EnderecoCep = "74000000";

            model.Destinatario.RazaoSocial = "DESTINATARIO DE TESTE";
            model.Destinatario.CnpjCpf = "11222333000181";
            model.Destinatario.EnderecoLogadrouro = "AVENIDA DE TESTE";
            model.Destinatario.EnderecoNumero = "2";
            model.Destinatario.EnderecoBairro = "CENTRO";
            model.Destinatario.Municipio = "GOIANIA";
            model.Destinatario.EnderecoUf = "GO";
            model.Destinatario.EnderecoCep = "74000001";

            return model;
        }

        [TestMethod]
        [DataRow("522507PC3D315K000193550010000000011000000018", DisplayName = "Chave com CNPJ alfanumérico")]
        [DataRow("23190811820016000167650010000000221100000227", DisplayName = "Chave numérica (regressão)")]
        public void Gerar_DanfeComChave_ProduzPdf(string chaveAcesso)
        {
            // Arrange
            var model = CriarViewModelMinimo(chaveAcesso);

            using (var danfe = new DanfeDoc(model))
            {
                // Act
                danfe.Gerar();

                using (var ms = new MemoryStream())
                {
                    danfe.Salvar(ms);

                    // Assert - PDF gerado, não vazio e com o cabeçalho %PDF
                    var bytes = ms.ToArray();
                    Assert.IsTrue(bytes.Length > 1000, "O PDF gerado está vazio ou pequeno demais ({0} bytes)", bytes.Length);
                    Assert.AreEqual("%PDF", System.Text.Encoding.ASCII.GetString(bytes, 0, 4));
                }
            }
        }
    }
}
