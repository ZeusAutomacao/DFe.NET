using System;
using DFe.Utils;
using DFe.Classes.Entidades;
using DFe.Classes.Flags;
using NFe.Utils.Testes.Dados;
using Xunit;

namespace NFe.Utils.Testes;

public class ChaveFiscalTesteUnitario
{
    [Theory(DisplayName = "Dado cnpj alfanumérico, quando obter chave fiscal, então não deve lançar exceção.")]
    [InlineData("T6J3XFX0IVDD47")]
    [InlineData("t6j3xfx0ivdd47")]
    public void DadoCnpjAlfanumericoQuandoObterChaveFiscalEntaoNaoDeveLancarExcecao(string cnpj)
    {
        // Arrange
        var estado = Estado.SE;
        var data = DateTime.Now;
        var modelo = ModeloDocumento.NFe;
        var serie = 100;
        var numero = 12345;
        var tipoEmissao = 1;
        var cNf = 12345678;

        // Act
        var excecaoCapturada = Record.Exception(() => ChaveFiscal.ObterChave(estado, data, cnpj, modelo, serie, numero, tipoEmissao, cNf));

        // Assert
        Assert.Null(excecaoCapturada);
    }

    [Theory(DisplayName = "Dado caractere, quando obter valor do caractere, então deve retornar o valor esperado.")]
    [MemberData(nameof(ChaveFiscalDadosDeTeste.ObterCaracteresEValores), MemberType = typeof(ChaveFiscalDadosDeTeste))]
    public void DadoCaractereQuandoObterValorEntaoDeveRetornarValorEsperado(char caractere, int valorEsperado)
    {
        // Act
        var valorObtido = ChaveFiscal.ObterValorDoCaractere(caractere);
        
        // Assert
        Assert.Equal(valorEsperado, valorObtido);
    }
}
