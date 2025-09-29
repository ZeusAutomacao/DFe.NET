using System.Security.Cryptography;

namespace DadosDeTestes.AssinaturaDigital;

public class AssinaturaDigitalTesteDados
{
    public static IEnumerable<object[]> ObterDadosParaGeracaoDoHashSha1Bytes()
    {
        return new List<object[]>
        {
            new object[] { "92037465012398765432100011223344556677889900|2|1|F1A9B237CD8800FFE234A9912B674CFA1" },
            new object[] { "87122345099887766554433221100009988776655443" },
            new object[] { "73456789101112131415161718192021222324252627" },
            new object[] { "65829374618273645564738291028374618273645564" }
        };
    }
    
    public static IEnumerable<object[]> ObterDadosParaGeracaoDoHashSha1BytesEValorEsperado()
    {
        return new List<object[]>
        {
            new object[] { "92037465012398765432100011223344556677889900|2|1|F1A9B237CD8800FFE234A9912B674CFA1", new byte[] { 128, 55, 179, 49, 198, 97, 206, 43, 246, 208, 112, 183, 231, 3, 23, 105, 114, 184, 33, 153 }},
            new object[] { "87122345099887766554433221100009988776655443", new byte[] { 151, 153, 16, 228, 170, 100, 76, 248, 192, 58, 160, 126, 157, 224, 171, 233, 75, 23, 118, 67 }},
            new object[] { "73456789101112131415161718192021222324252627", new byte[] { 71, 100, 124, 212, 171, 46, 181, 47, 206, 96, 227, 230, 215, 8, 14, 131, 167, 214, 99, 181 }},
            new object[] { "65829374618273645564738291028374618273645564", new byte[] { 147, 88, 158, 178, 38, 247, 111, 70, 151, 152, 182, 14, 69, 18, 129, 97, 157, 47, 222, 50 }}
        };
    }
}