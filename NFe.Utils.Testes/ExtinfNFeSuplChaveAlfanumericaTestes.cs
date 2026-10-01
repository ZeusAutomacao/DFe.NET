using System;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using NFe.Classes;
using NFe.Classes.Informacoes;
using NFe.Classes.Informacoes.Identificacao;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Classes.Informacoes.Total;
using NFe.Utils.InformacoesSuplementares;
using Xunit;

namespace NFe.Utils.Testes
{
    /// <summary>
    /// Testes do QR-Code da NFC-e com chave de acesso contendo CNPJ alfanumérico (NT Conjunta 2025.001)
    /// </summary>
    public class ExtinfNFeSuplChaveAlfanumericaTestes
    {
        //NFC-e de GO com o CNPJ alfanumérico de teste da Receita (PC3D315K000193); DV da chave = 0
        private const string ChaveAlfanumerica = "522507PC3D315K000193650010000000011000000010";

        private static Classes.NFe CriarNfce(string chave)
        {
            return new Classes.NFe
            {
                infNFe = new infNFe
                {
                    Id = "NFe" + chave,
                    versao = "4.00",
                    ide = new ide
                    {
                        tpAmb = TipoAmbiente.Homologacao,
                        cUF = Estado.GO,
                        tpEmis = TipoEmissao.teNormal,
                        dhEmi = new DateTimeOffset(2025, 7, 15, 10, 0, 0, TimeSpan.FromHours(-3))
                    },
                    total = new total
                    {
                        ICMSTot = new ICMSTot { vNF = 10.50m }
                    }
                }
            };
        }

        [Fact]
        public void ObterUrlQrCode2_ComChaveAlfanumerica_MontaAUrlComAChaveSemConversaoNumerica()
        {
            // Arrange
            var nfce = CriarNfce(ChaveAlfanumerica);

            // Act
            var url = new infNFeSupl().ObterUrlQrCode(nfce, VersaoQrCode.QrCodeVersao2, "000001", "CSC-DE-TESTE");

            // Assert
            Assert.Contains(ChaveAlfanumerica, url);

            //formato dos parâmetros: chave|versão do QR-Code|ambiente|idCsc|hash SHA-1
            var parametros = url.Substring(url.IndexOf("p=", StringComparison.Ordinal) + 2).Split('|');
            Assert.Equal(5, parametros.Length);
            Assert.Equal(ChaveAlfanumerica, parametros[0]);
            Assert.Equal("2", parametros[1]);
            Assert.Equal("2", parametros[2]);
            Assert.Equal("1", parametros[3]);
            Assert.Equal(40, parametros[4].Trim().Length);
        }

        [Fact]
        public void ObterUrlQrCode2_ComChaveNumerica_MantemComportamentoAntigo()
        {
            // Arrange
            var nfce = CriarNfce("23190811820016000167650010000000221100000227");

            // Act
            var url = new infNFeSupl().ObterUrlQrCode(nfce, VersaoQrCode.QrCodeVersao2, "000001", "CSC-DE-TESTE");

            // Assert
            var parametros = url.Substring(url.IndexOf("p=", StringComparison.Ordinal) + 2).Split('|');
            Assert.Equal("23190811820016000167650010000000221100000227", parametros[0]);
            Assert.Equal(40, parametros[4].Trim().Length);
        }
    }
}
