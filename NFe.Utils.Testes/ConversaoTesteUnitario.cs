using DadosDeTestes.NFe.Utils;
using Xunit;

namespace NFe.Utils.Testes;

public class ConversaoTesteUnitario
{
    [Theory(DisplayName = "Dado dados em string para geração do hex sha1 de string, quando obter hex sha1 de string, então deve obter hex sha1 de string.")]
    [MemberData(nameof(ConversaoDadosDeTeste.ObterDadosParaGerarHashSha1DeStringEValorEsperado), MemberType = typeof(ConversaoDadosDeTeste))]
    public void DadoDadosEmStringParaGeracaoDoHexSha1DeStringQuandoObterHexSha1DeStringEntaoDeveObterHexSha1DeString(string dadosEmString, string stringEsperada)
    {
        // Act
        var valorRetornado = Conversao.ObterHexSha1DeString(dadosEmString);

        // Assert
        Assert.Equal(stringEsperada, valorRetornado);
    }
}