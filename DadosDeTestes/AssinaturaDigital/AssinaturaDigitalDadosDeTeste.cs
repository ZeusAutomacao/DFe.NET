namespace DadosDeTestes.AssinaturaDigital;

public class AssinaturaDigitalTesteDados
{
    public static IEnumerable<object[]> ObterDadosParaGeracaoDoHashSha1Bytes()
    {
        return new List<object[]>
        {
            new object[] { "28250632876302000114650010000122331289270262|2|2|1020228753214A38CE050010AB5C066A9" },
            new object[] { "28250332876302000114550010000010471000000758" },
            new object[] { "28250332876302000114550010000010201000000070" },
            new object[] { "28250332876302000114550010000010871000081770" }
        };
    }
}